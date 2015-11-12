using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibPlateAnalysis;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Forms.FormsForGraphsDisplay
{
    class cWindowToDisplayHisto : SimpleForm
    {
        public ChartArea CurrentChartArea;
       // double BinValue;
        cScreening CompleteScreening;
        cExtendedList RawValues;
        Series SerieForHisto;
        double BinNumber = 100;

        public cWindowToDisplayHisto(cScreening CompleteScreening0, cExtendedList RawValues0)
        {
            this.CompleteScreening = CompleteScreening0;

            this.parametersToolStripMenuItem.Click += new System.EventHandler(this.parametersToolStripMenuItem_Click);
            
            RequestWindow.label3.Text = "Bin Number";

            this.RawValues = RawValues0;

            CurrentChartArea = new ChartArea();
            CurrentChartArea.BorderColor = Color.Black;

            this.chartForSimpleForm.ChartAreas.Add(CurrentChartArea);
            CurrentChartArea.Axes[0].MajorGrid.Enabled = false;
            if(CompleteScreening!=null)
                CurrentChartArea.Axes[0].Title = CompleteScreening.ListDescriptors[CompleteScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
            CurrentChartArea.Axes[1].Title = "Sum";
            CurrentChartArea.AxisX.LabelStyle.Format = "N2";

            this.chartForSimpleForm.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            
            if (CompleteScreening != null)
                CurrentChartArea.BackColor = Color.White;

            this.chartForSimpleForm.ChartAreas[0].CursorX.IsUserEnabled = true;
            this.chartForSimpleForm.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            this.chartForSimpleForm.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            this.chartForSimpleForm.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            if ((CompleteScreening != null) && (cGlobalInfo.OptionsWindow.FFAllOptions.checkBoxHistoDisplayStats.Checked))
            {
                StripLine AverageLine = new StripLine();
                AverageLine.BackColor = Color.Black;
                AverageLine.IntervalOffset = RawValues.Mean();
                AverageLine.StripWidth = double.Epsilon;
                CurrentChartArea.AxisX.StripLines.Add(AverageLine);
                AverageLine.Text = String.Format("{0:0.###}", AverageLine.IntervalOffset);

                StripLine StdLine = new StripLine();
                StdLine.BackColor = Color.FromArgb(64, Color.Black);
                double Std = RawValues.Std();
                StdLine.IntervalOffset = AverageLine.IntervalOffset - 0.5 * Std;
                StdLine.StripWidth = Std;
                CurrentChartArea.AxisX.StripLines.Add(StdLine);
                AverageLine.StripWidth = 0.0001;
            }

            SerieForHisto = new Series();
            SerieForHisto.ShadowOffset = 1;
            SerieForHisto.ChartType = SeriesChartType.Column;
            if (CompleteScreening != null)
            SerieForHisto.Color = cGlobalInfo.ListWellClasses[1].ColourForDisplay;

            List<double[]> HistoPos = RawValues.CreateHistogram(this.BinNumber, false);
            if (HistoPos.Count == 0) return;

            for (int IdxValue = 0; IdxValue < HistoPos[0].Length; IdxValue++)
            {
                SerieForHisto.Points.AddXY(HistoPos[0][IdxValue], HistoPos[1][IdxValue]);
                SerieForHisto.Points[IdxValue].ToolTip = HistoPos[1][IdxValue].ToString();
                if (CompleteScreening != null)
                {
                    if (CompleteScreening.SelectedClass == -1)
                        SerieForHisto.Points[IdxValue].Color = Color.Black;
                    else
                        SerieForHisto.Points[IdxValue].Color = cGlobalInfo.ListWellClasses[CompleteScreening.SelectedClass].ColourForDisplay;
                }
            }
            this.chartForSimpleForm.Series.Add(SerieForHisto);
        }



        private void parametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.chartForSimpleForm.Series[0].Points.Count >= 1)
                RequestWindow.numericUpDownMarkerSize.Value = (decimal)this.BinNumber;

            RequestWindow.numericUpDownMax.Value = (decimal)this.chartForSimpleForm.ChartAreas[0].AxisY.Maximum;
            RequestWindow.numericUpDownMin.Value = (decimal)this.chartForSimpleForm.ChartAreas[0].AxisY.Minimum;

            if (RequestWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            if (RequestWindow.numericUpDownMax.Value <= RequestWindow.numericUpDownMin.Value) return;

            this.chartForSimpleForm.ChartAreas[0].AxisY.Maximum = (double)RequestWindow.numericUpDownMax.Value;
            this.chartForSimpleForm.ChartAreas[0].AxisY.Minimum = (double)RequestWindow.numericUpDownMin.Value;
            this.BinNumber = (double)RequestWindow.numericUpDownMarkerSize.Value;

            DrawHisto();
        }


        private void DrawHisto()
        {
            List<double[]> HistoPos = RawValues.CreateHistogram(this.BinNumber, false);
            if (HistoPos.Count == 0) return;

            SerieForHisto.Points.Clear();

            for (int IdxValue = 0; IdxValue < HistoPos[0].Length; IdxValue++)
            {
                SerieForHisto.Points.AddXY(HistoPos[0][IdxValue], HistoPos[1][IdxValue]);
                SerieForHisto.Points[IdxValue].ToolTip = HistoPos[1][IdxValue].ToString();
                if (CompleteScreening.SelectedClass == -1)
                    SerieForHisto.Points[IdxValue].Color = Color.Black;
                else
                    SerieForHisto.Points[IdxValue].Color = cGlobalInfo.ListWellClasses[CompleteScreening.SelectedClass].ColourForDisplay;
            }
        
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.chartForSimpleForm)).BeginInit();
            this.SuspendLayout();
            // 
            // RequestWindow
            // 
            this.RequestWindow.Location = new System.Drawing.Point(25, 25);
            // 
            // cWindowToDisplayHisto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(990, 491);
            this.Name = "cWindowToDisplayHisto";
            ((System.ComponentModel.ISupportInitialize)(this.chartForSimpleForm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


    }
}
