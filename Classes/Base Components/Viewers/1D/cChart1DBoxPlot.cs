using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Drawing;
using LibPlateAnalysis;
using System.IO;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes.Base_Classes.GUI;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{

    public class cChart1DBoxPlot : cGraphGeneral
    {
        protected ToolStripMenuItem SpecificContextMenu = null;

        FormForSingleSlider SliderForMarkerSize = new FormForSingleSlider("Marker Size");
        FormForSingleSlider SliderForOpacity = new FormForSingleSlider("Marker Opacity");
        public int Opacity = 255;
        public int MarkerSize = 10;

        public cChart1DBoxPlot()
        {
            base.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AssociatedChart_MouseClick);
        }

        public void Run()
        {
            this.SliderForMarkerSize.trackBar.Value = this.MarkerSize;
            this.SliderForMarkerSize.numericUpDown.Value = this.MarkerSize;
            this.SliderForOpacity.numericUpDown.Maximum = this.SliderForOpacity.trackBar.Maximum = 255;
            this.SliderForOpacity.trackBar.Value = this.Opacity;
            this.SliderForOpacity.numericUpDown.Value = this.Opacity;

            for (int IdxSerie = 0; IdxSerie < InputSimpleData.Count; IdxSerie++)
            {
                Series NewSerie = new System.Windows.Forms.DataVisualization.Charting.Series(base.InputSimpleData[IdxSerie].Name);


                NewSerie.ChartType = SeriesChartType.BoxPlot;
                NewSerie["BoxPlotShowMedian"] = "true";
                NewSerie["BoxPlotShowAverage"] = "true";
                NewSerie["BoxPlotShowUnusualValues"] = "true";


                //Chart1.Series["Series1"]["BoxPlotShowMedian"] = "false";
                //Chart1.Series["Series1"]["BoxPlotShowAverage"] = "false";


                //for (int j = 0; j < this.input[IdxSerie].Count; j++)
                {
                    //this.chartForPoints.Series[0].Points[j].MarkerColor = Color.FromArgb(128, GlobalInfo.ListCellularPhenotypes[(int)MachineLearning.Classes[j]].ColourForDisplay);
                    DataPoint DP = new DataPoint();
                   // double[] Value = new double[1];
                   // Value[0] = this.input[IdxSerie][j];
                    DP.YValues =  this.InputSimpleData[IdxSerie].ToArray();

                   // NewSerie.YValuesPerPoint = 6;

                    //NewSerie.Points.DataBindY(this.input[IdxSerie].ToArray());




                    // Specify data series name for the Box Plot.
                   // Chart1.Series["BoxPlotSeries"]["BoxPlotSeries"] = "DataSeries";


                    DP.XValue = IdxSerie;

                    DP.MarkerSize = this.MarkerSize;
                    DP.MarkerStyle = MarkerStyle.Circle;

                    if (IsBorder)
                    {
                        DP.MarkerBorderColor = Color.Black;
                        DP.MarkerBorderWidth = 1;
                    }

                    //if (this.input[IdxSerie].ListTags != null)
                    //{
                    //    if (j >= this.input[IdxSerie].ListTags.Count) continue;
                    //    DP.Tag = this.input[IdxSerie].ListTags[j];

                    //    if (DP.Tag.GetType() == typeof(cWell))
                    //    {
                    //        DP.Color = ((cWell)(DP.Tag)).GetClassColor();
                    //        DP.ToolTip = ((cWell)(DP.Tag)).GetShortInfo() + Value[0];
                    //    }
                    //    if (DP.Tag.GetType() == typeof(cDescriptorsType))
                    //    {
                    //        // DP.Color = ((cWell)(DP.Tag)).GetClassColor();
                    //        DP.ToolTip = ((cDescriptorsType)(DP.Tag)).GetShortInfo() + Value[0];
                    //        DP.AxisLabel = ((cDescriptorsType)(DP.Tag)).GetName();
                    //        base.CurrentChartArea.AxisX.Interval = 1;

                    //    }
                    //    if (DP.Tag.GetType() == typeof(cPlate))
                    //    {
                    //        // DP.Color = ((cWell)(DP.Tag)).GetClassColor();
                    //        DP.ToolTip = ((cPlate)(DP.Tag)).Name + " : " + Value[0];
                    //        DP.AxisLabel = ((cPlate)(DP.Tag)).Name;
                    //        base.CurrentChartArea.AxisX.Interval = 1;

                    //    }

                    //}
                    NewSerie.Points.Add(DP);
                    //  if (Input[idxCol].ListTags != null)
                    //NewSerie.Points[IdxValue].Tag = Input[idxCol].ListTags[idxRow];
                    // if (this.IsDisplayValues) CurrentSeries.Points[IdxValue].Label = Value.ToString("N2");// string.Format("{0:0.###}", Math.Abs(Value));
                    //this.chartForPoints.Series[0].Points[j].MarkerBorderWidth = BorderSize;
                }
                base.CurrentSeries.Add(NewSerie);
            }
            base.Run();
        }


        public ToolStripMenuItem GetContextMenu()
        {
            SpecificContextMenu = new ToolStripMenuItem("Graph Options");


            //SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_ChartOpacity = new ToolStripMenuItem("Opacity");
            ToolStripMenuItem_ChartOpacity.Click += new System.EventHandler(this.ToolStripMenuItem_ChartOpacity);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChartOpacity);

            ToolStripMenuItem ToolStripMenuItem_MarkerSize = new ToolStripMenuItem("Marker Size");
            ToolStripMenuItem_MarkerSize.Click += new System.EventHandler(this.ToolStripMenuItem_MarkerSize);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_MarkerSize);


          
            return this.SpecificContextMenu;
        }



        private void ToolStripMenuItem_MarkerSize(object sender, EventArgs e)
        {
            if (this.SliderForMarkerSize.ShowDialog() != DialogResult.OK) return;
            this.MarkerSize = (int)this.SliderForMarkerSize.numericUpDown.Value;

            for (int j = 0; j < this.InputSimpleData.Count; j++)
            {
                foreach (var item in this.Series[j].Points)
                    item.MarkerSize = this.MarkerSize;
            }
        }

        private void ToolStripMenuItem_ChartOpacity(object sender, EventArgs e)
        {
            if (this.SliderForOpacity.ShowDialog() != DialogResult.OK) return;
            this.Opacity = (int)this.SliderForOpacity.numericUpDown.Value;


            for (int j = 0; j < this.InputSimpleData.Count; j++)
                foreach (var item in this.Series[j].Points)
                {
                    Color C = item.Color;
                    item.Color = Color.FromArgb(this.Opacity, C);
                }

        }




        private void AssociatedChart_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1) return;

            if (e.Button != MouseButtons.Right) return;

            ContextMenuStrip NewMenu = new ContextMenuStrip();
            foreach (var item in base.GetContextMenu(e))
                NewMenu.Items.Add(item);
            NewMenu.Items.Add(this.GetContextMenu());

            #region Selection process
            double MaxX = this.ChartAreas[0].CursorX.SelectionEnd;
            double MinX = this.ChartAreas[0].CursorX.SelectionStart;

            if (MaxX < MinX)
            {
                MinX = this.ChartAreas[0].CursorX.SelectionEnd;
                MaxX = this.ChartAreas[0].CursorX.SelectionStart;
            }

            double MaxY = this.ChartAreas[0].CursorY.SelectionEnd;
            double MinY = this.ChartAreas[0].CursorY.SelectionStart;

            if (MaxY < MinY)
            {
                MinY = this.ChartAreas[0].CursorY.SelectionEnd;
                MaxY = this.ChartAreas[0].CursorY.SelectionStart;
            }

            //cListWell ListWells = new cListWell();
            List<DataPoint> LDP = new List<DataPoint>();

            foreach (DataPoint item in this.Series[0].Points)
            {
                if ((item.XValue >= MinX) && (item.XValue <= MaxX) && (item.YValues[0] >= MinY) && (item.YValues[0] <= MaxY))
                {
                    if ((item.Tag != null) && (item.Tag.GetType() == typeof(cWell)))
                    {
                        // ListWells.Add((cWell)(item.Tag));
                        LDP.Add(item);

                        //((cWell)(item.Tag)).SetClass(5);
                        //item.Color = ((cWell)(item.Tag)).GetClassColor();
                    }
                }
            }

            if (LDP.Count > 0)
            {
                ToolStripMenuItem SpecificContextMenu = new ToolStripMenuItem("List " + LDP.Count + " wells");
                ToolStripMenuItem ToolStripMenuItem_ChangeClass = new ToolStripMenuItem("Classes");
                //ToolStripMenuItem_CopyClassToClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_CopyClassToClipBoard);
                SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChangeClass);

                cWell TmpWell = (cWell)(LDP[0].Tag);

                for (int i = 0; i < cGlobalInfo.ListWellClasses.Count; i++)
                {
                    ToolStripMenuItem ToolStripMenuItem_NewClass = new ToolStripMenuItem(cGlobalInfo.ListWellClasses[i].Name);
                    ToolStripMenuItem_NewClass.Click += new System.EventHandler(this.ToolStripMenuItem_NewClass);
                    ToolStripMenuItem_NewClass.Tag = LDP;
                    ToolStripMenuItem_ChangeClass.DropDownItems.Add(ToolStripMenuItem_NewClass);
                }
                NewMenu.Items.Add(SpecificContextMenu);
            }
            #endregion

            NewMenu.DropShadowEnabled = true;
            NewMenu.Show(Control.MousePosition);
        }


        private void ToolStripMenuItem_NewClass(object sender, EventArgs e)
        {
            //CopyValuestoClipBoard();
            ToolStripMenuItem ParentMenu = (ToolStripMenuItem)(sender);
            int Classe = 0;
            int ResultClasse = -1;

            List<DataPoint> DP = (List<DataPoint>)(ParentMenu.Tag);


            foreach (var Class in cGlobalInfo.ListWellClasses)
            {
                if (Class.Name == sender.ToString())
                {
                    ResultClasse = Classe;
                    break;
                }

                Classe++;
            }

            foreach (var item in DP)
            {
                ((cWell)(item.Tag)).SetClass(ResultClasse);
                item.Color = ((cWell)(item.Tag)).GetClassColor();
            }

            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);

        }




    }

}
