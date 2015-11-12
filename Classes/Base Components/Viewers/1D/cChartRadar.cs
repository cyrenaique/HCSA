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

    public class cChartRadar : cGraphGeneral
    {
        protected ToolStripMenuItem SpecificContextMenu = null;
        cExtendedTable CurrentData = null;


        public cChartRadar()
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

            //for (int IdxSerie = 0; IdxSerie < InputSimpleData.Count; IdxSerie++)
            //{
            //    if (InputSimpleData[IdxSerie].Count == 0) continue;

            //    Series NewSerie = new System.Windows.Forms.DataVisualization.Charting.Series(base.InputSimpleData[IdxSerie].Name + "_Histogram");
            //    NewSerie.Tag = base.InputSimpleData[IdxSerie].Tag;

            //    //if (ISPoint)
                //    NewSerie.ChartType = SeriesChartType.Point;
                //if (IsLine)
                //    NewSerie.ChartType = SeriesChartType.Line;
                //if (IsBar)
               // NewSerie.ChartType = SeriesChartType.Column;

                //NewSerie.ChartType = SeriesChartType.StackedColumn; 
              //  base.LabelAxisY = "Frequency %";


            //    for (int j = 0; j < CurrentHistogram[0].Count; j++)
            //    {
            //        double[] Value = new double[1];
            //        Value[0] = CurrentHistogram[1][j];
                    
            //        DataPoint DP = new DataPoint();
            //        DP.SetValueXY(/*Step * j + GlobalMinX*/ CurrentHistogram[0][j], Value[0]);
                    
            //        DP.Tag = InputSimpleData[IdxSerie].Tag;

            //        if (IsBorder)
            //        {
            //            DP.MarkerBorderColor = Color.Black;
            //            DP.MarkerBorderWidth = 1;
            //            DP.BorderColor = Color.Black;
            //            DP.BorderWidth = 1;
            //        }
            //        else
            //        {
            //            DP.BorderWidth = 0;
            //            DP.MarkerBorderWidth = 0;
            //        }

            //        if (InputSimpleData[IdxSerie].Tag != null)
            //        {
            //            if (InputSimpleData[IdxSerie].Tag.GetType() == typeof(cWellClassType))
            //             {
            //                 DP.Color = ((cWellClassType)(InputSimpleData[IdxSerie].Tag)).ColourForDisplay;
            //                 DP.ToolTip = ((cWellClassType)(InputSimpleData[IdxSerie].Tag)).Name + "\n";
            //             }
            //            if (InputSimpleData[IdxSerie].Tag.GetType() == typeof(cCellularPhenotype))
            //            {
            //                DP.Color = ((cCellularPhenotype)(InputSimpleData[IdxSerie].Tag)).ColourForDisplay;
            //                DP.ToolTip = ((cCellularPhenotype)(InputSimpleData[IdxSerie].Tag)).Name + "\n";
            //            }
            //        }
                   
            //        DP.ToolTip += DP.XValue.ToString("N2") + " :\n" + DP.YValues[0];
                    
            //        NewSerie.Points.Add(DP);
            //    }
            //    base.CurrentSeries.Add(NewSerie);
            //}
         //   base.LabelAxisY = "Frequency";
            base.Update();

        }

        public void Run()
        {
           //base.IsYGrid = true;
            base.IsAllowDisplayValue = true;
            base.IsAllowDisplayTable = true;
            this.Refresh();
          
            base.Run();
        }



        public ToolStripMenuItem GetContextMenu()
        {
            SpecificContextMenu = new ToolStripMenuItem("Graph Options");

            //ToolStripMenuItem ToolStripMenuItem_ChartLine = new ToolStripMenuItem("Line");
            //ToolStripMenuItem_ChartLine.CheckOnClick = true;
            //ToolStripMenuItem_ChartLine.Click += new System.EventHandler(this.ToolStripMenuItem_ChartLine);
            //SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChartLine);



            //SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            //ToolStripMenuItem ToolStripMenuItem_ChartOpacity = new ToolStripMenuItem("Opacity");
            //ToolStripMenuItem_ChartOpacity.Click += new System.EventHandler(this.ToolStripMenuItem_ChartOpacity);
            //SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChartOpacity);


            return this.SpecificContextMenu;
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


    }
}
