using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using LibPlateAnalysis;

namespace HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic
{
    public partial class FormPanelForHisto : Form
    {
        //int BinSize = 10;

        List<cExtendedList> ListValues;

        FormForMaxMinRequest RequestWindowForYAxis = new FormForMaxMinRequest();
        FormForMaxMinRequest RequestWindowForXAxis = new FormForMaxMinRequest();
        public ChartArea CurrentChartArea;
        bool IsStacked;
        eOrientation HistoOrientation;
        eGraphType GraphType = eGraphType.HISTOGRAM;
        bool IsMarkerBorder = true;

        int PointSize = 10;


        ToolStripMenuItem ToolStripMenuItem_Stacked;
        ToolStripMenuItem ToolStripMenuItem_BinNumber;
        int NumBin = 100;

        private void ChangeStackedState(object sender, EventArgs e)
        {
            IsStacked = ToolStripMenuItem_Stacked.Checked;
            RedrawHisto();
            //((cWindowToDisplayEntireScreening)ParentWindow).RemovePlate(this.PlateToDisplay);
            //WindowToDisplayEntireScreening.RemovePlate(this.PlateToDisplay);
        }

        public FormPanelForHisto(List<cExtendedList> ListValues, bool IsStacked, eGraphType GraphType, eOrientation HistoOrientation)
        {
            InitializeComponent();
            this.IsStacked = IsStacked;
            this.HistoOrientation = HistoOrientation;
            this.ListValues = ListValues;

            #region adapt the context menu
            if (this.ListValues.Count > 1)
            {
                ToolStripMenuItem_Stacked = new ToolStripMenuItem("Stacked");
                ToolStripMenuItem_Stacked.CheckOnClick = true;
                ToolStripMenuItem_Stacked.Checked = IsStacked;
                ToolStripMenuItem_Stacked.Click += new System.EventHandler(this.ChangeStackedState);

                contextMenuStrip.Items.Add(ToolStripMenuItem_Stacked);
            }

            this.GraphType = GraphType;
            if (this.GraphType == eGraphType.HISTOGRAM)
            {
                ToolStripMenuItem_BinNumber = new ToolStripMenuItem("Bin Number");
                // ToolStripMenuItem_Stacked.CheckOnClick = true;
                // ToolStripMenuItem_Stacked.Checked = IsStacked;
                ToolStripMenuItem_BinNumber.Click += new System.EventHandler(this.ChangeBinNumber);

                contextMenuStrip.Items.Add(ToolStripMenuItem_BinNumber);

            }
            #endregion

            CurrentChartArea = new ChartArea();
            CurrentChartArea.BorderColor = Color.Black;

            this.chart.ChartAreas.Add(CurrentChartArea);

            this.chart.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart_MouseDoubleClick);
            this.chart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart_MouseClick);
            RedrawHisto();
        }


        public bool IsShadow = true;
        public bool UserEnable = true;
        public bool IsDisplayStat = false;

        void RedrawHisto()
        {
            int IdxSerie = 0;
            this.chart.Series.Clear();
            //this.chart.ChartAreas.Clear();
            //this.chart.Legends[0] = new System.Windows.Forms.DataVisualization.Charting.Legend("Test");

            //CurrentChartArea.Axes[0].MajorGrid.Enabled = false;
            //if (CompleteScreening != null)
            //    CurrentChartArea.Axes[0].Title = CompleteScreening.ListDescriptors[CompleteScreening.ListDescriptors.CurrentSelectedDescriptor].GetName();
            if (this.GraphType == eGraphType.LINE)
                CurrentChartArea.Axes[1].Title = "Value";
            else if (this.GraphType == eGraphType.HISTOGRAM)
                CurrentChartArea.Axes[1].Title = "Sum";

            CurrentChartArea.AxisX.LabelStyle.Format = "N2";

            this.chart.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            CurrentChartArea.BackGradientStyle = GradientStyle.TopBottom;
            //if (CompleteScreening != null)
            CurrentChartArea.BackColor = Color.FromArgb(255, 230, 230, 230);
            CurrentChartArea.BackSecondaryColor = Color.White;

            CurrentChartArea.CursorX.IsUserEnabled = this.UserEnable;
            CurrentChartArea.CursorX.IsUserSelectionEnabled = this.UserEnable;
            CurrentChartArea.AxisX.ScaleView.Zoomable = this.UserEnable;
            CurrentChartArea.AxisX.ScrollBar.IsPositionedInside = true;

            //if ((CompleteScreening != null) && (CompleteScreening.GlobalInfo.OptionsWindow.checkBoxDisplayHistoStats.Checked))
            //{
            //    StripLine AverageLine = new StripLine();
            //    AverageLine.BackColor = Color.Black;
            //    AverageLine.IntervalOffset = RawValues.Mean();
            //    AverageLine.StripWidth = double.Epsilon;
            //    CurrentChartArea.AxisX.StripLines.Add(AverageLine);
            //    AverageLine.Text = String.Format("{0:0.###}", AverageLine.IntervalOffset);

            //    StripLine StdLine = new StripLine();
            //    StdLine.BackColor = Color.FromArgb(64, Color.Black);
            //    double Std = RawValues.Std();
            //    StdLine.IntervalOffset = AverageLine.IntervalOffset - 0.5 * Std;
            //    StdLine.StripWidth = Std;
            //    CurrentChartArea.AxisX.StripLines.Add(StdLine);
            //    AverageLine.StripWidth = 0.0001;
            //}


            //   if (CompleteScreening != null)
            //       SerieForHisto.Color = CompleteScreening.GlobalInfo.ListWellClasses[1].ColourForDisplay;

            //  List<double[]> HistoPos = RawValues.CreateHistogram(this.BinNumber);
            //   if (HistoPos.Count == 0) return;


            chart.Legends.Clear();
            foreach (var CurrentList in this.ListValues)
            {
                List<double[]> ListValuesHisto = null;

                if (this.GraphType == eGraphType.HISTOGRAM) ListValuesHisto = CurrentList.CreateHistogram(NumBin, false);
                else if (this.GraphType == eGraphType.LINE)
                {
                    ListValuesHisto = new List<double[]>();
                    ListValuesHisto.Add(CurrentList.ToArray());
                }
                //  CurrentChartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
                Series SerieForHisto = null;
                if (CurrentList.Name != null)
                    SerieForHisto = new Series(CurrentList.Name);
                else
                    SerieForHisto = new Series();

                if (IsShadow)
                    SerieForHisto.ShadowOffset = 1;
                else
                    SerieForHisto.ShadowOffset = 0;

                if (this.GraphType == eGraphType.LINE)
                {
                    SerieForHisto.ChartType = SeriesChartType.Line;
                }
                else
                {
                    if (this.HistoOrientation == eOrientation.HORIZONTAL)
                    {
                        if (IsStacked)
                            SerieForHisto.ChartType = SeriesChartType.StackedColumn;
                        else
                            SerieForHisto.ChartType = SeriesChartType.Column;
                    }
                    else if (this.HistoOrientation == eOrientation.VERTICAL)
                    {
                        if (IsStacked)
                            SerieForHisto.ChartType = SeriesChartType.StackedBar;
                        else
                            SerieForHisto.ChartType = SeriesChartType.Bar;
                    }
                }

                if ((CurrentList.Name != null) && (CurrentList.Name != ""))
                {
                    Legend CurrentLegend = new Legend(CurrentList.Name);
                    chart.Legends.Add(CurrentLegend);
                }


                int MarkerBorderSize = 0;
                if (this.IsMarkerBorder)
                    MarkerBorderSize = 1;
                else
                    MarkerBorderSize = 0;

                if (this.GraphType == eGraphType.HISTOGRAM)
                {
                    if (ListValuesHisto.Count == 0) continue;
                    for (int IdxValue = 0; IdxValue < ListValuesHisto[0].Length; IdxValue++)
                    {
                        SerieForHisto.Points.AddXY(ListValuesHisto[0][IdxValue], ListValuesHisto[1][IdxValue]);
                        SerieForHisto.Points[IdxValue].ToolTip = ListValuesHisto[1][IdxValue].ToString();

                        SerieForHisto.Points[IdxValue].BorderWidth = MarkerBorderSize;
                        SerieForHisto.Points[IdxValue].BorderColor = Color.Black;
                    }

                    if ((this.IsDisplayStat) && (this.ListValues.Count == 1))
                    {
                        StripLine AverageLine = new StripLine();
                        AverageLine.BackColor = Color.Black;
                        AverageLine.IntervalOffset = this.ListValues[0].Mean();
                        AverageLine.StripWidth = double.Epsilon;
                        CurrentChartArea.AxisX.StripLines.Add(AverageLine);
                        AverageLine.Text = AverageLine.IntervalOffset.ToString("N2");

                        StripLine StdLine = new StripLine();
                        StdLine.BackColor = Color.FromArgb(64, Color.Black);
                        double Std = this.ListValues[0].Std();
                        StdLine.IntervalOffset = AverageLine.IntervalOffset - 0.5 * Std;
                        StdLine.StripWidth = Std;
                        CurrentChartArea.AxisX.StripLines.Add(StdLine);
                        AverageLine.StripWidth = 0.0001;
                    }
                }
                else if (this.GraphType == eGraphType.LINE)
                {
                    for (int IdxValue = 0; IdxValue < ListValuesHisto[0].Length; IdxValue++)
                    {
                        SerieForHisto.Points.Add(ListValuesHisto[0][IdxValue]);
                        SerieForHisto.Points[IdxValue].ToolTip = ListValuesHisto[0][IdxValue].ToString("N2");
                        SerieForHisto.Points[IdxValue].MarkerStyle = MarkerStyle.Circle;
                        SerieForHisto.Points[IdxValue].MarkerSize = PointSize;

                        if ((this.ListValues[0].ListTags != null) && (this.ListValues[0].ListTags[IdxValue].GetType() == typeof(cWell)))
                        {
                            cWell TmpWell = (cWell)(this.ListValues[0].ListTags[IdxValue]);

                            SerieForHisto.Points[IdxValue].MarkerColor = TmpWell.GetClassColor();
                            SerieForHisto.Points[IdxValue].Tag = TmpWell;
                        }
                        SerieForHisto.Points[IdxValue].BorderWidth = MarkerBorderSize;
                        SerieForHisto.Points[IdxValue].BorderColor = Color.Black;
                    }
                }

                this.chart.Series.Add(SerieForHisto);
                this.chart.Series[IdxSerie].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.UInt32;
                IdxSerie++;
            }



        }

        //private void numericUpDownBinSize_ValueChanged(object sender, EventArgs e)
        //{
        //    //  RedrawHisto();
        //}



        private void ChangeBinNumber(object sender, EventArgs e)
        {
            FormForPointSize WindowForValue = new FormForPointSize();
            WindowForValue.trackBarPointSize.Maximum = 500;
            WindowForValue.trackBarPointSize.Value = this.NumBin;

            WindowForValue.Text = "Bin Number";

            if (WindowForValue.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            this.NumBin = (int)WindowForValue.trackBarPointSize.Value;

            RedrawHisto();
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            this.chart.SaveImage(ms, ChartImageFormat.Bmp);
            Bitmap bm = new Bitmap(ms);
            Clipboard.SetImage(bm);
        }

        private void backColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorDialog ColorDia = new ColorDialog();
            ColorDia.ShowDialog();

            CurrentChartArea.BackColor = ColorDia.Color;
            this.chart.BackColor = ColorDia.Color;
        }

        private void majorXAxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentChartArea.AxisX.MajorGrid.Enabled = majorXAxisToolStripMenuItem.Checked;
        }

        private void majorYAxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentChartArea.AxisY.MajorGrid.Enabled = majorYAxisToolStripMenuItem.Checked;
        }

        private void yAxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RequestWindowForYAxis.numericUpDownMax.Value = (decimal)CurrentChartArea.AxisY.Maximum;
            RequestWindowForYAxis.numericUpDownMin.Value = (decimal)CurrentChartArea.AxisY.Minimum;

            if (this.GraphType == eGraphType.HISTOGRAM)
            {
                RequestWindowForYAxis.label3.Visible = false;
                RequestWindowForYAxis.numericUpDownMarkerSize.Visible = false;
            }

            if (RequestWindowForYAxis.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            if (RequestWindowForYAxis.numericUpDownMax.Value <= RequestWindowForYAxis.numericUpDownMin.Value) return;

            CurrentChartArea.AxisY.Maximum = (double)RequestWindowForYAxis.numericUpDownMax.Value;
            CurrentChartArea.AxisY.Minimum = (double)RequestWindowForYAxis.numericUpDownMin.Value;

            RedrawHisto();
        }

        private void xAxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RequestWindowForXAxis.numericUpDownMax.Value = (decimal)CurrentChartArea.AxisX.Maximum;
            RequestWindowForXAxis.numericUpDownMin.Value = (decimal)CurrentChartArea.AxisX.Minimum;

            if (this.GraphType == eGraphType.HISTOGRAM)
            {
                RequestWindowForXAxis.label3.Visible = false;
                RequestWindowForXAxis.numericUpDownMarkerSize.Visible = false;
            }


            if (RequestWindowForXAxis.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            if (RequestWindowForXAxis.numericUpDownMax.Value <= RequestWindowForXAxis.numericUpDownMin.Value) return;

            CurrentChartArea.AxisX.Maximum = (double)RequestWindowForXAxis.numericUpDownMax.Value;
            CurrentChartArea.AxisX.Minimum = (double)RequestWindowForXAxis.numericUpDownMin.Value;

            RedrawHisto();
        }

        private void shadowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.IsShadow = shadowToolStripMenuItem.Checked;
            RedrawHisto();
        }

        private void markerBorderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.IsMarkerBorder = markerBorderToolStripMenuItem.Checked;
            RedrawHisto();
        }

        private void displayStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.IsDisplayStat = displayStatisticsToolStripMenuItem.Checked;
            RedrawHisto();
        }


        private void chart_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
              //  contextMenuStrip = new System.Windows.Forms.ContextMenuStrip();


                HitTestResult Res = this.chart.HitTest(e.X, e.Y, ChartElementType.DataPoint);
                if (Res.Series == null) return;



                DataPoint PtToTransfer = Res.Series.Points[Res.PointIndex];

                if ((PtToTransfer.Tag != null) && (PtToTransfer.Tag.GetType() == typeof(cWell)))
                {
                    cWell TmpWell = (cWell)(PtToTransfer.Tag);
                    //ContextMenuStrip  NewMenu = new System.Windows.Forms.ContextMenuStrip();

                    foreach (var item in TmpWell.GetExtendedContextMenu())
                        contextMenuStrip.Items.Add(item);

                    contextMenuStrip.Show(Control.MousePosition);
                }
            }
        }

        private void chart_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HitTestResult Res = this.chart.HitTest(e.X, e.Y, ChartElementType.DataPoint);
            if (Res.Series == null) return;

            DataPoint PtToTransfer = Res.Series.Points[Res.PointIndex];
            cExtendedList CurrentList = new cExtendedList();
            CurrentList.Name = Res.Series.Name;

            foreach (var item in Res.Series.Points)
            {
                CurrentList.Add(item.YValues[0]);
            }

            cExtendedTable CT = new cExtendedTable(CurrentList);
            CT.ListRowNames = new List<string>();
            //foreach (var item in Res.Series.Points)
            //{
            //}



            cViewerTable MyTable = new cViewerTable();
            MyTable.SetInputData(CT);
            MyTable.Run();

            cDesignerSinglePanel MyDesigner = new cDesignerSinglePanel();
            MyDesigner.SetInputData(MyTable.GetOutPut());
            MyDesigner.Run();

            cDisplayToWindow MyDisplay = new cDisplayToWindow();
            MyDisplay.SetInputData(MyDesigner.GetOutPut());
            MyDisplay.Run();
            MyDisplay.Display();
        }



    }
}
