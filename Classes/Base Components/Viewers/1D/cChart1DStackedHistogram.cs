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
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
using HCSAnalyzer.Classes.Base_Components.GUI;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{

    public class cChart1DStackedHistogram : cGraphGeneral
    {
        protected ToolStripMenuItem SpecificContextMenu = null;
        public bool IsLine = false;
        public bool IsBar = false;
        public bool ISPoint = true;
       // FormForSingleSlider SliderForMarkerSize = new FormForSingleSlider("Marker Size");
       // FormForSingleSlider SliderForMarkerSizeBinSize = new FormForSingleSlider("Bin number");
        FormForHistoBinOptions SliderForMarkerSizeBinSize = new FormForHistoBinOptions(100);
       // FormForSingleSlider SliderForOpacity = new FormForSingleSlider("Marker Opacity");
      //  public int Opacity = 255;
       // public int MarkerSize = 10;
        public int BinNumber = 100;
        public bool Is100 = false;


        cExtendedTable CurrentHistogram = null;
        

        public cChart1DStackedHistogram()
        {
            base.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AssociatedChart_MouseClick);
            base.IsZoomableX = true;
            base.IsZoomableY = false;
            base.IsAllowDisplayValue = false;
        }
        
        void Refresh()
        {
            base.CurrentSeries.Clear();
           
            double GlobalMinX = 0;
            double GlobalMaxX = 0;

            if (this.SliderForMarkerSizeBinSize.radioButtonMinMaxAutomated.Checked)
            {
                GlobalMinX = this.InputSimpleData.Min();
                GlobalMaxX = this.InputSimpleData.Max();
            }
            else
            {
                GlobalMinX = (double)this.SliderForMarkerSizeBinSize.numericUpDownMin.Value;
                GlobalMaxX = (double)this.SliderForMarkerSizeBinSize.numericUpDownMax.Value;
            
            }

            for (int IdxSerie = 0; IdxSerie < InputSimpleData.Count; IdxSerie++)
            {
                if (InputSimpleData[IdxSerie].Count == 0) continue;

                Series NewSerie = new System.Windows.Forms.DataVisualization.Charting.Series(base.InputSimpleData[IdxSerie].Name + "_Histogram");
                NewSerie.Tag = base.InputSimpleData[IdxSerie].Tag;

                cHistogramBuilder HB = new cHistogramBuilder();
                HB.SetInputData(new cExtendedTable(InputSimpleData[IdxSerie]));

                if (this.SliderForMarkerSizeBinSize.radioButtonMinMaxAutomated.Checked)
                {
                    HB.Min = GlobalMinX;
                    HB.Max = GlobalMaxX;
                }
                else
                {
                    HB.Min = (double)this.SliderForMarkerSizeBinSize.numericUpDownMin.Value;
                    HB.Max = (double)this.SliderForMarkerSizeBinSize.numericUpDownMax.Value;
                }

                HB.IsBinNumberMode = this.SliderForMarkerSizeBinSize.radioButtonBinNumber.Checked;
                HB.BinSize = (double)this.SliderForMarkerSizeBinSize.numericUpDownBinSize.Value;

                if(this.BinNumber==-1)
                    HB.BinNumber = HB.Max - HB.Min+1;
                else
                    HB.BinNumber = this.BinNumber;


                HB.Run();

                CurrentHistogram = HB.GetOutPut();

                //if (ISPoint)
                //    NewSerie.ChartType = SeriesChartType.Point;
                //if (IsLine)
                //    NewSerie.ChartType = SeriesChartType.Line;
                //if (IsBar)
               // NewSerie.ChartType = SeriesChartType.Column;

                //NewSerie.ChartType = SeriesChartType.StackedColumn;
                if (this.Is100)
                {
                    NewSerie.ChartType = SeriesChartType.StackedColumn100;
                    base.LabelAxisY = "Frequency %";
                    base.CurrentChartArea.AxisY.Minimum = 0;
                    base.CurrentChartArea.AxisY.Maximum = 100;
                }
                else
                {

                    NewSerie.ChartType = SeriesChartType.StackedColumn;
                    base.LabelAxisY = "Frequency";
                    base.CurrentChartArea.AxisY.Minimum = Double.NaN;
                    base.CurrentChartArea.AxisY.Maximum = Double.NaN;
                 //   base.CurrentChartArea.AxisY.
                }
                //SeriesPos[i].ChartType = SeriesChartType.StackedColumn;

             //   double Step = (GlobalMaxX - GlobalMinX + 1) / CurrentHistogram[0].Count;




                for (int j = 0; j < CurrentHistogram[0].Count; j++)
                {
                    double[] Value = new double[1];
                    Value[0] = CurrentHistogram[1][j];
                    
                    DataPoint DP = new DataPoint();
                    DP.SetValueXY(/*Step * j + GlobalMinX*/ CurrentHistogram[0][j], Value[0]);
                    
                    DP.Tag = InputSimpleData[IdxSerie].Tag;

                    if (IsBorder)
                    {
                        DP.MarkerBorderColor = Color.Black;
                        DP.MarkerBorderWidth = 1;
                        DP.BorderColor = Color.Black;
                        DP.BorderWidth = 1;
                    }
                    else
                    {
                        DP.BorderWidth = 0;
                        DP.MarkerBorderWidth = 0;
                    }

                    if (InputSimpleData[IdxSerie].Tag != null)
                    {
                        if (InputSimpleData[IdxSerie].Tag.GetType() == typeof(cWellClassType))
                         {
                             DP.Color = ((cWellClassType)(InputSimpleData[IdxSerie].Tag)).ColourForDisplay;
                             DP.ToolTip = ((cWellClassType)(InputSimpleData[IdxSerie].Tag)).Name + "\n";
                         }
                        if (InputSimpleData[IdxSerie].Tag.GetType() == typeof(cCellularPhenotype))
                        {
                            DP.Color = ((cCellularPhenotype)(InputSimpleData[IdxSerie].Tag)).ColourForDisplay;
                            DP.ToolTip = ((cCellularPhenotype)(InputSimpleData[IdxSerie].Tag)).Name + "\n";
                        }
                    }
                   
                    DP.ToolTip += DP.XValue.ToString("N2") + " :\n" + DP.YValues[0];
                    
                    NewSerie.Points.Add(DP);
                }
                base.CurrentSeries.Add(NewSerie);
            }
         //   base.LabelAxisY = "Frequency";
            base.Update();

        }

        public void Run()
        {
            this.SliderForMarkerSizeBinSize.numericUpDown.Maximum = this.SliderForMarkerSizeBinSize.trackBar.Maximum = 1000;
            this.SliderForMarkerSizeBinSize.numericUpDown.Minimum = -1;

            if(this.BinNumber==-1)
                this.SliderForMarkerSizeBinSize.trackBar.Value = 0;
            else
                this.SliderForMarkerSizeBinSize.trackBar.Value = this.BinNumber;
            
            this.SliderForMarkerSizeBinSize.numericUpDown.Value = this.BinNumber;

            base.IsYGrid = true;
            base.IsAllowDisplayValue = true;
            base.IsAllowDisplayTable = true;
            this.Refresh();
          
            base.Run();
        }

        ToolStripMenuItem ToolStripMenuItem_100Stacked = new ToolStripMenuItem("100% Stacked");

        public ToolStripMenuItem GetContextMenu()
        {
            SpecificContextMenu = new ToolStripMenuItem("Graph Options");

            ToolStripMenuItem ToolStripMenuItem_ChartLine = new ToolStripMenuItem("Line");
            ToolStripMenuItem_ChartLine.CheckOnClick = true;
            ToolStripMenuItem_ChartLine.Click += new System.EventHandler(this.ToolStripMenuItem_ChartLine);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChartLine);

            ToolStripMenuItem ToolStripMenuItem_ChartBar = new ToolStripMenuItem("Column");
            ToolStripMenuItem_ChartBar.CheckOnClick = true;
            ToolStripMenuItem_ChartBar.Click += new System.EventHandler(this.ToolStripMenuItem_ChartBar);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChartBar);

            ToolStripMenuItem ToolStripMenuItem_ChartPoint = new ToolStripMenuItem("Point");
            ToolStripMenuItem_ChartPoint.CheckOnClick = true;
            ToolStripMenuItem_ChartPoint.Click += new System.EventHandler(this.ToolStripMenuItem_ChartPoint);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChartPoint);

            //SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            //ToolStripMenuItem ToolStripMenuItem_ChartOpacity = new ToolStripMenuItem("Opacity");
            //ToolStripMenuItem_ChartOpacity.Click += new System.EventHandler(this.ToolStripMenuItem_ChartOpacity);
            //SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChartOpacity);

            //ToolStripMenuItem ToolStripMenuItem_MarkerSize = new ToolStripMenuItem("Marker Size");
            //ToolStripMenuItem_MarkerSize.Click += new System.EventHandler(this.ToolStripMenuItem_MarkerSize);
            //SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_MarkerSize);


            SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_BinNumber = new ToolStripMenuItem("Binning");
            ToolStripMenuItem_BinNumber.Click += new System.EventHandler(this.ToolStripMenuItem_BinNumber);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_BinNumber);


            ToolStripMenuItem_100Stacked.CheckOnClick = true;
            
            ToolStripMenuItem_100Stacked.Checked = this.Is100;
            ToolStripMenuItem_100Stacked.Click += new System.EventHandler(this._ToolStripMenuItem_100Stacked);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_100Stacked);


            SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());
            ToolStripMenuItem ToolStripMenuItem_Info = new ToolStripMenuItem("Info");
            ToolStripMenuItem_Info.Click += new System.EventHandler(this.ToolStripMenuItem_Info);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Info);


            //ToolStripMenuItem ToolStripMenuItem_BinSize = new ToolStripMenuItem("Bin Size");
            //ToolStripMenuItem_BinSize.Click += new System.EventHandler(this.ToolStripMenuItem_BinSize);
            //SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_BinSize);




            //SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            //ToolStripMenuItem ToolStripMenuItem_DispDataTable = new ToolStripMenuItem("Display Data Table");
            //ToolStripMenuItem_DispDataTable.Click += new System.EventHandler(this.ToolStripMenuItem_DispDataTable);
            //SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DispDataTable);



            return this.SpecificContextMenu;
        }

        private void ToolStripMenuItem_DispDataTable(object sender, EventArgs e)
        {
           // base.da
        }

        public string GetInfo()
        {
            string ToReturn = base.GetInfo();
            ToReturn += "Stacked Histogram:\n";
            ToReturn += "\tBin Number: " + this.BinNumber + "\n";
            ToReturn += "\tBin Size: " + (this.GetAxisXMax() - this.GetAxisXMin()) / (double)this.BinNumber + "\n";
            ToReturn += "Stacked 100%: " + this.Is100;

            return ToReturn;
        }

        private void ToolStripMenuItem_Info(object sender, EventArgs e)
        {
            cDisplayText DT = new cDisplayText();
            DT.SetInputData(this.GetInfo());
            DT.Title = "Info["+this.CurrentTitle.Text+"]";
            DT.Run();
         }

        private void _ToolStripMenuItem_100Stacked(object sender, EventArgs e)
        {
            this.Is100 = ToolStripMenuItem_100Stacked.Checked;
         
            this.Run();
        }

        private void ToolStripMenuItem_BinNumber(object sender, EventArgs e)
        {
            this.SliderForMarkerSizeBinSize.numericUpDown.Minimum = -1;
            if (this.SliderForMarkerSizeBinSize.ShowDialog() != DialogResult.OK) return;
            this.BinNumber = (int)this.SliderForMarkerSizeBinSize.numericUpDown.Value;
          
            this.Run();

            //base.Run();
        }

        //private void ToolStripMenuItem_BinSize(object sender, EventArgs e)
        //{
        //   // if (this.SliderForMarkerSizeBinSize.ShowDialog() != DialogResult.OK) return;
        //   // this.BinNumber = (int)this.SliderForMarkerSizeBinSize.numericUpDown.Value;
        //    double BinSize = -1;


            
        //    this.Run();
        //    //base.Run();
        //}
        //private void ToolStripMenuItem_MarkerSize(object sender, EventArgs e)
        //{
        //    if (this.SliderForMarkerSize.ShowDialog() != DialogResult.OK) return;
        //    this.MarkerSize = (int)this.SliderForMarkerSize.numericUpDown.Value;

        //    for (int j = 0; j < this.input.Count; j++)
        //    {
        //        foreach (var item in this.Series[j].Points)
        //            item.MarkerSize = this.MarkerSize;
        //    }
        //}

        //private void ToolStripMenuItem_ChartOpacity(object sender, EventArgs e)
        //{
        //    if (this.SliderForOpacity.ShowDialog() != DialogResult.OK) return;
        //    this.Opacity = (int)this.SliderForOpacity.numericUpDown.Value;


        //    for (int j = 0; j < this.input.Count; j++)
        //        foreach (var item in this.Series[j].Points)
        //        {
        //            Color C = item.Color;
        //            item.Color = Color.FromArgb(this.Opacity, C);
        //        }

        //}

        private void ToolStripMenuItem_ChartBar(object sender, EventArgs e)
        {
            IsBar = !IsBar;
            for (int IdxSerie = 0; IdxSerie < base.CurrentSeries.Count; IdxSerie++)
            {
                // Series NewSerie = new System.Windows.Forms.DataVisualization.Charting.Series(base.input[IdxSerie].Name);
                base.CurrentSeries[IdxSerie].ChartType = SeriesChartType.Column;
            }
            base.Update();
        }

        private void ToolStripMenuItem_ChartPoint(object sender, EventArgs e)
        {
            ISPoint = !ISPoint;

            for (int IdxSerie = 0; IdxSerie < base.CurrentSeries.Count; IdxSerie++)
            {
                // Series NewSerie = new System.Windows.Forms.DataVisualization.Charting.Series(base.input[IdxSerie].Name);
                base.CurrentSeries[IdxSerie].ChartType = SeriesChartType.Point;

                foreach (var item in base.CurrentSeries[IdxSerie].Points)
                {
                    item.MarkerStyle = MarkerStyle.Circle;
                }
            }
            base.Update();
        }

        private void ToolStripMenuItem_ChartLine(object sender, EventArgs e)
        {
            IsLine = !IsLine;

            for (int IdxSerie = 0; IdxSerie < base.CurrentSeries.Count; IdxSerie++)
            {
                // Series NewSerie = new System.Windows.Forms.DataVisualization.Charting.Series(base.input[IdxSerie].Name);
                base.CurrentSeries[IdxSerie].ChartType = SeriesChartType.Line;
            }
            base.Update();
        }

        private void AssociatedChart_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1) return;

            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip NewMenu = new ContextMenuStrip();
                foreach (var item in base.GetContextMenu(e))
                {
                    if(item!=null) NewMenu.Items.Add(item);
                }

                NewMenu.Items.Add(this.GetContextMenu());
                NewMenu.DropShadowEnabled = true;
                NewMenu.Show(Control.MousePosition);
            }
            else
            {
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

                if (double.IsNaN(this.ChartAreas[0].CursorX.Position) == false)
                {
                }
            }

                //cListWell ListWells = new cListWell();
                //List<DataPoint> LDP = new List<DataPoint>();

                //foreach (DataPoint item in this.Series[0].Points)
                //{
                //    if ((item.XValue >= MinX) && (item.XValue <= MaxX) && (item.YValues[0] >= MinY) && (item.YValues[0] <= MaxY))
                //    {
                //        if ((item.Tag != null) && (item.Tag.GetType() == typeof(cWell)))
                //        {
                //            // ListWells.Add((cWell)(item.Tag));
                //            LDP.Add(item);

                //            //((cWell)(item.Tag)).SetClass(5);
                //            //item.Color = ((cWell)(item.Tag)).GetClassColor();
                //        }
                //        if ((item.Tag != null) && (item.Tag.GetType() == typeof(cWellClass)))
                //        {
                //            // ListWells.Add((cWell)(item.Tag));
                //            LDP.Add(item);

                //            //((cWell)(item.Tag)).SetClass(5);
                //            //item.Color = ((cWell)(item.Tag)).GetClassColor();
                //        }

                //    }
                //}

                //if (LDP.Count > 0)
                //{
                //    ToolStripMenuItem SpecificContextMenu = new ToolStripMenuItem("List " + LDP.Count + " wells");
                //    ToolStripMenuItem ToolStripMenuItem_ChangeClass = new ToolStripMenuItem("Classes");
                //    //ToolStripMenuItem_CopyClassToClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_CopyClassToClipBoard);
                //    SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChangeClass);

                //    cWell TmpWell = (cWell)(LDP[0].Tag);

                //    for (int i = 0; i < TmpWell.cGlobalInfo.ListWellClasses.Count; i++)
                //    {
                //        ToolStripMenuItem ToolStripMenuItem_NewClass = new ToolStripMenuItem(TmpWell.cGlobalInfo.ListWellClasses[i].Name);
                //        ToolStripMenuItem_NewClass.Click += new System.EventHandler(this.ToolStripMenuItem_NewClass);
                //        ToolStripMenuItem_NewClass.Tag = LDP;
                //        ToolStripMenuItem_ChangeClass.DropDownItems.Add(ToolStripMenuItem_NewClass);
                //    }
                //    NewMenu.Items.Add(SpecificContextMenu);
                //}
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
