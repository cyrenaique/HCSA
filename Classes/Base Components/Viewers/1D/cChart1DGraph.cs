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
using HCSAnalyzer.Classes.Base_Classes.Viewers._1D;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{

    public class cChart1DGraph : cGraphGeneral
    {
        protected ToolStripMenuItem SpecificContextMenu = null;
        public bool IsLine = false;
        public bool IsBar = false;
        public bool ISFastPoint = false;
        public bool ISPoint = true;
        FormForSingleSlider SliderForMarkerSize = new FormForSingleSlider("Marker Size");
        FormForSingleSlider SliderForOpacity = new FormForSingleSlider("Marker Opacity");
        FormForSingleSlider SliderForLineWidth = new FormForSingleSlider("Line Width");
        public int Opacity = 255;
        public int MarkerSize = 8;
        public int LineWidth = 2;
        public bool IsLogAxis = false;
        public cSerieInfoDesign[] ArraySeriesInfo = null;

        public cExtendedList X_AxisValues;
        public List<cCurveForGraph> ListCurves = new List<cCurveForGraph>();

        public cChart1DGraph()
        {
            base.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AssociatedChart_MouseClick);
           
            base.GraphBelow = this;
        }



        public void Run()
        {
            this.SliderForMarkerSize.trackBar.Value = this.MarkerSize;
            this.SliderForMarkerSize.numericUpDown.Value = this.MarkerSize;

            this.SliderForLineWidth.trackBar.Value = this.LineWidth;
            this.SliderForLineWidth.numericUpDown.Value = this.LineWidth;

            this.SliderForOpacity.numericUpDown.Maximum = this.SliderForOpacity.trackBar.Maximum = 255;
            this.SliderForOpacity.trackBar.Value = this.Opacity;
            this.SliderForOpacity.numericUpDown.Value = this.Opacity;

            if ((InputSimpleData==null)||((X_AxisValues != null) && (X_AxisValues.Count != InputSimpleData[0].Count))) return;
            if ((X_AxisValues != null) && (X_AxisValues.Min() <= 0) && (IsLogAxis)) return;

            #region multiple readouts
            if (ListCurves.Count > 0)
            {
                //for (int IdxSimpleReadoutCurve = 0; IdxSimpleReadoutCurve < input.Count; IdxSimpleReadoutCurve++)

                foreach (cCurveForGraph item in ListCurves)
                {
                    if (item == null) continue;
                    Series NewSerie = new System.Windows.Forms.DataVisualization.Charting.Series(item.Title);

                    //NewSerie.ChartType = SeriesChartType.ErrorBar;
                    NewSerie.ChartType = SeriesChartType.Point;

                //    NewSerie.ChartType = SeriesChartType.Point;

                    //NewSerie.ChartType = SeriesChartType.SplineRange;
                    NewSerie.ShadowOffset = 0;
                    //NewSerie.ShadowColor = Color.Transparent;

                    #region loop over the multireadouts curves
                    for (int j = 0; j < item.ListPtValues.Count; j++)
                    {
                        //this.chartForPoints.Series[0].Points[j].MarkerColor = Color.FromArgb(128, GlobalInfo.ListCellularPhenotypes[(int)MachineLearning.Classes[j]].ColourForDisplay);
                       
                        cExtendedList ListValues = new cExtendedList();
                        for (int IdxValue = 1; IdxValue < item.ListPtValues[j].Count; IdxValue++)
                        {
                            ListValues.Add(item.ListPtValues[j][IdxValue]);
                        }

                        double[] Values = new double[2];

                        double Mean = ListValues.Mean();
                        double Std = ListValues.Std();

                        if (NewSerie.ChartType == SeriesChartType.ErrorBar)
                        {
                            Values = new double[3];
                            Values[0] = Mean;
                            Values[1] = Mean - Std;
                            Values[2] = Mean + Std;
                            DataPoint DP = new DataPoint();
                            DP.XValue = item.ListPtValues[j][0];
                            DP.YValues = Values;
                            DP.Color = Color.AliceBlue;
                            NewSerie.Points.Add(DP);
                        }
                        else if (NewSerie.ChartType == SeriesChartType.SplineRange)
                        {
                            Values[0] = Mean - Std;
                            Values[1] = Mean + Std;
                            DataPoint DP = new DataPoint();
                            DP.XValue = item.ListPtValues[j][0];
                            DP.YValues = Values;
                            DP.Color = Color.FromArgb(200, Color.Tomato);
                           // DP.MarkerSize = 10;
                            NewSerie.Points.Add(DP);
                        }
                        else
                        {
                           // Values = ListValues.ToArray();
                            for (int i = 0; i < ListValues.Count; i++)
                            {
                                 DataPoint DP = new DataPoint();
                                 DP.SetValueXY(item.ListPtValues[j][0], ListValues[i]);
                                 DP.Color = Color.FromArgb(190, Color.Tomato);
                                 DP.BorderColor = Color.Black;
                                 
                                 DP.BorderWidth = 1;
                                 DP.MarkerSize = 8;
                                 DP.MarkerStyle = MarkerStyle.Circle;
                                 NewSerie.Points.Add(DP);
                            }
                        
                        }
                        
                       // DP.Tag = DataSerie.Tag;
                        ////   Value[0] = item.ListPtValues[j];
                        //for (int IdxValue = 1; IdxValue < item.ListPtValues[j].Count; IdxValue++)
                        //{
                        //    ListValues.Add(item.ListPtValues[j][IdxValue]);
                        //}

                        //double[] Values = new double[2];
                        ////Values[0] = ListValues.Mean();
                        //double Mean = ListValues.Mean();
                        //double Std = ListValues.Std();
                        //Values[0] = Mean - Std;
                        //Values[1] = Mean + Std;

                        //DP.YValues = Values;

                       

                        //if (IsBorder)
                        //{
                        //    DP.MarkerBorderColor = Color.Black;
                        //    DP.MarkerBorderWidth = 1;
                        //}

                        //if ((ArraySeriesInfo != null) && (ArraySeriesInfo[IdxSimpleReadoutCurve] != null))
                        //{
                        //    DP.Color = Color.FromArgb(this.Opacity, ArraySeriesInfo[IdxSimpleReadoutCurve].color);
                        //    NewSerie.Tag = ArraySeriesInfo[IdxSimpleReadoutCurve];
                        //    DP.MarkerStyle = ArraySeriesInfo[IdxSimpleReadoutCurve].markerStyle;
                        //    DP.MarkerSize = this.MarkerSize;
                        //}
                        //else
                       // {
                            //DP.MarkerStyle = MarkerStyle.Diamond;
                            // DP.MarkerSize = 4;
                       // }

                        // DP.BorderWidth = 2;

                        //if (item.ListPtValues[0].ListTags != null)
                        //{
                        //    if (j >= item.ListPtValues[0].ListTags.Count) continue;
                        //    DP.Tag = item.ListPtValues[0].ListTags[j];

                        //    if (DP.Tag.GetType() == typeof(cWell))
                        //    {
                        //        DP.Color = ((cWell)(DP.Tag)).GetClassColor();
                        //        // DP.ToolTip = ((cWell)(DP.Tag)).GetShortInfo() + Value[0];
                        //    }
                        //    if (DP.Tag.GetType() == typeof(cDescriptorsType))
                        //    {
                        //        // DP.Color = ((cWell)(DP.Tag)).GetClassColor();
                        //        //  DP.ToolTip = ((cDescriptorsType)(DP.Tag)).GetShortInfo() + Value[0];
                        //        DP.AxisLabel = ((cDescriptorsType)(DP.Tag)).GetName();
                        //        base.CurrentChartArea.AxisX.Interval = 1;

                        //    }
                        //    if (DP.Tag.GetType() == typeof(cPlate))
                        //    {
                        //        // DP.Color = ((cWell)(DP.Tag)).GetClassColor();
                        //        //  DP.ToolTip = ((cPlate)(DP.Tag)).Name + " : " + Value[0];
                        //        DP.AxisLabel = ((cPlate)(DP.Tag)).Name;
                        //        base.CurrentChartArea.AxisX.Interval = 1;

                        //    }

                        //}
                        //NewSerie.Points.Add(DP);
                    }
                    #endregion

                    base.CurrentSeries.Add(NewSerie);
                }
            }

            if (base.ListInput != null)
            {
                foreach (cExtendedTable DataSerie in base.ListInput)
                {
                    Series NewSerie = new System.Windows.Forms.DataVisualization.Charting.Series("ComplexD_ataTable"+DataSerie.Name);
                    NewSerie.Tag = DataSerie.Tag;
                    
                    //NewSerie.ChartType = SeriesChartType.SplineRange;
                    NewSerie.ChartType = SeriesChartType.ErrorBar;

                    for (int IdxPt = 0; IdxPt < DataSerie.Count; IdxPt++)
                    {
                        DataPoint DP = new DataPoint();
                        DP.XValue = DataSerie[IdxPt][0];

                        cExtendedList ListValues = new cExtendedList();
                        for (int i = 1; i < DataSerie[IdxPt].Count; i++)
                        {
                            ListValues.Add(DataSerie[IdxPt][i]);
                        }

                        if (ListValues.Count == 0) continue;
                        double[] Values = new double[2];
                        
                        double Mean = ListValues.Mean();
                        double Std = ListValues.Std();
                        if (ListValues.Count == 1)
                            Std = 0;

                        if (NewSerie.ChartType == SeriesChartType.ErrorBar)
                        {
                            Values = new double[3];
                            Values[0] = Mean;
                            Values[1] = Mean - Std;
                            Values[2] = Mean + Std;
                        }
                        else
                        {
                            Values[0] = Mean - Std;
                            Values[1] = Mean + Std;
                        }
                        DP.YValues = Values;
                        DP.Tag = DataSerie.Tag;

                        DP.ToolTip = "Mean: " + Mean + "\nStdev: " + Std;

                        if (DP.Tag.GetType() == typeof(cWellClassType))
                        {
                            DP.Color = Color.FromArgb(200, ((cWellClassType)(DP.Tag)).ColourForDisplay);
                        }
                        //if (DP.Tag.GetType() == typeof(cWell))
                        //{
                        //    DP.Color = ((cWell)(DP.Tag)).GetClassColor();

                        //}
                       // DP.Color = Color.FromArgb(200, Color.Tomato);

                        NewSerie.Points.Add(DP);
                    }

                    base.CurrentSeries.Add(NewSerie);

                }

            }


            #endregion

            #region simple readout curves
            if (InputSimpleData != null)
            {
                cLUTProcess LUTProcess = new cLUTProcess(cGlobalInfo.GraphsLUT);

                for (int IdxSimpleReadoutCurve = 0; IdxSimpleReadoutCurve < InputSimpleData.Count; IdxSimpleReadoutCurve++)
                {
                    Series NewSerie = new System.Windows.Forms.DataVisualization.Charting.Series(base.InputSimpleData[IdxSimpleReadoutCurve].Name);
                    NewSerie.Tag = base.InputSimpleData[IdxSimpleReadoutCurve].Tag;

                    if (ISPoint)
                        NewSerie.ChartType = SeriesChartType.Point;
                    if (ISFastPoint)
                        NewSerie.ChartType = SeriesChartType.FastPoint;
                    if (IsLine)
                        NewSerie.ChartType = SeriesChartType.Line;
                    if (IsBar)
                        NewSerie.ChartType = SeriesChartType.Column;


                    if ((ArraySeriesInfo != null) && (ArraySeriesInfo[IdxSimpleReadoutCurve] != null))
                    {
                        NewSerie.ChartType = SeriesChartType.Spline;
                        //NewSerie.ChartType = SeriesChartType.Line;
                    }


                    #region loop over the simple readout curves
                    for (int j = 0; j < this.InputSimpleData[IdxSimpleReadoutCurve].Count; j++)
                    {
                        //this.chartForPoints.Series[0].Points[j].MarkerColor = Color.FromArgb(128, GlobalInfo.ListCellularPhenotypes[(int)MachineLearning.Classes[j]].ColourForDisplay);
                        DataPoint DP = new DataPoint();
                        double[] Value = new double[1];
                        Value[0] = this.InputSimpleData[IdxSimpleReadoutCurve][j];
                        if (double.IsNaN(Value[0])) continue;
                        DP.YValues = Value;

                        if (X_AxisValues != null)
                            DP.XValue = X_AxisValues[j];
                        else
                            DP.XValue = j;

                      //  DP.AxisLabel = this.InputSimpleData.ListRowNames[j].ToString();

                        if (IsBorder)
                        {
                            DP.MarkerBorderColor = Color.Black;
                            DP.MarkerBorderWidth = 0;// 1;
                        }

                        if ((ArraySeriesInfo != null) && (ArraySeriesInfo[IdxSimpleReadoutCurve] != null))
                        {
                            DP.Color = Color.FromArgb(this.Opacity, ArraySeriesInfo[IdxSimpleReadoutCurve].color);
                            NewSerie.Tag = ArraySeriesInfo[IdxSimpleReadoutCurve];
                            DP.MarkerStyle = ArraySeriesInfo[IdxSimpleReadoutCurve].markerStyle;
                            DP.MarkerSize = this.MarkerSize;
                        }
                        else
                        {
                            int IdxColor = IdxSimpleReadoutCurve % LUTProcess.GetNumberOfColors();
                            NewSerie.Color = LUTProcess.GetColor(IdxColor);
                            DP.Color = Color.FromArgb(this.Opacity, LUTProcess.GetColor(IdxColor));
                            DP.MarkerStyle = MarkerStyle.Circle;
                            DP.MarkerSize = this.MarkerSize;
                        }

                        DP.BorderWidth = this.LineWidth;

                        if (this.InputSimpleData[IdxSimpleReadoutCurve].ListTags != null)
                        {
                            if (j >= this.InputSimpleData[IdxSimpleReadoutCurve].ListTags.Count)
                            { 
                                DP.Tag = this.InputSimpleData[IdxSimpleReadoutCurve];
                                NewSerie.Points.Add(DP);
                               
                                continue;
                            }
                            DP.Tag = this.InputSimpleData[IdxSimpleReadoutCurve].ListTags[j];

                            if (DP.Tag.GetType() == typeof(string))
                            {
                                DP.ToolTip = (string)(DP.Tag);
                            }

                            if (DP.Tag.GetType() == typeof(cWellClassType))
                            {
                                DP.Color = ((cWellClassType)(DP.Tag)).ColourForDisplay;
                                DP.ToolTip = ((cWellClassType)(DP.Tag)).GetShortInfo() + Value[0];
                                //  DP.AxisLabel = ((cWellClass)(DP.Tag)).Name;
                                base.CurrentChartArea.AxisX.Interval = 1;

                                if (this.InputSimpleData.ListRowNames != null)
                                    DP.AxisLabel = this.InputSimpleData.ListRowNames[j];

                            }
                            if (DP.Tag.GetType() == typeof(cWell))
                            {
                                DP.Color = ((cWell)(DP.Tag)).GetClassColor();
                                DP.ToolTip = ((cWell)(DP.Tag)).GetShortInfo() + Value[0];

                                if ((this.InputSimpleData.ListRowNames != null) && (j < this.InputSimpleData.ListRowNames.Count))
                                    DP.AxisLabel = this.InputSimpleData.ListRowNames[j];
                            }
                            if (DP.Tag.GetType() == typeof(cDescriptorType))
                            {
                                // DP.Color = ((cWell)(DP.Tag)).GetClassColor();
                                DP.ToolTip = ((cDescriptorType)(DP.Tag)).GetShortInfo() + Value[0];
                                DP.AxisLabel = ((cDescriptorType)(DP.Tag)).GetName();
                                base.CurrentChartArea.AxisX.Interval = 1;

                            }
                            if (DP.Tag.GetType() == typeof(cPlate))
                            {
                                // DP.Color = ((cWell)(DP.Tag)).GetClassColor();
                                DP.ToolTip = ((cPlate)(DP.Tag)).GetName() + " : " + Value[0];
                                DP.AxisLabel = ((cPlate)(DP.Tag)).GetName();
                                base.CurrentChartArea.AxisX.Interval = 1;
                            }
                        }
                        NewSerie.Points.Add(DP);
                    }
                    #endregion


                    base.CurrentSeries.Add(NewSerie);
                }
            }
            #endregion

            base.Run();
            base.ChartAreas[0].AxisX.IsLogarithmic = IsLogAxis;
        }

        #region Context Menu
        public ToolStripMenuItem GetContextMenu()
        {
            SpecificContextMenu = new ToolStripMenuItem("Graph Options");

            ToolStripMenuItem ToolStripMenuItem_ChartLine = new ToolStripMenuItem("Line");
            ToolStripMenuItem_ChartLine.CheckOnClick = true;
            ToolStripMenuItem_ChartLine.Click += new System.EventHandler(this.ToolStripMenuItem_ChartLine);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChartLine);

            ToolStripMenuItem ToolStripMenuItem_ChartBar = new ToolStripMenuItem("Bar");
            ToolStripMenuItem_ChartBar.CheckOnClick = true;
            ToolStripMenuItem_ChartBar.Click += new System.EventHandler(this.ToolStripMenuItem_ChartBar);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChartBar);

            ToolStripMenuItem ToolStripMenuItem_ChartPoint = new ToolStripMenuItem("Point");
            ToolStripMenuItem_ChartPoint.CheckOnClick = true;
            ToolStripMenuItem_ChartPoint.Click += new System.EventHandler(this.ToolStripMenuItem_ChartPoint);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChartPoint);

            ToolStripMenuItem ToolStripMenuItem_ChartFastPoint = new ToolStripMenuItem("Fast Point");
            ToolStripMenuItem_ChartFastPoint.CheckOnClick = true;
            ToolStripMenuItem_ChartFastPoint.Click += new System.EventHandler(this.ToolStripMenuItem_ChartFastPoint);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChartFastPoint);

            SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_ChartOpacity = new ToolStripMenuItem("Opacity");
            ToolStripMenuItem_ChartOpacity.Click += new System.EventHandler(this.ToolStripMenuItem_ChartOpacity);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChartOpacity);

            ToolStripMenuItem ToolStripMenuItem_MarkerSize = new ToolStripMenuItem("Marker Size");
            ToolStripMenuItem_MarkerSize.Click += new System.EventHandler(this.ToolStripMenuItem_MarkerSize);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_MarkerSize);

            ToolStripMenuItem ToolStripMenuItem_LineWidth = new ToolStripMenuItem("Line Width");
            ToolStripMenuItem_LineWidth.Click += new System.EventHandler(this.ToolStripMenuItem_LineWidth);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_LineWidth);



            cExtendedTable doc = null;
            IDataObject dataObj = Clipboard.GetDataObject();
            string format = typeof(cExtendedTable).FullName;

            if (dataObj.GetDataPresent(format))
            {
                doc = dataObj.GetData(format) as cExtendedTable;

                ToolStripMenuItem ToolStripMenuItem_PasteFromClipboard = new ToolStripMenuItem("Paste Serie from Clipboard");
                ToolStripMenuItem_PasteFromClipboard.Click += new System.EventHandler(this.ToolStripMenuItem_PasteFromClipboard);
                ToolStripMenuItem_PasteFromClipboard.Tag = doc;
                SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_PasteFromClipboard);
            }


            return this.SpecificContextMenu;
        }


        private void ToolStripMenuItem_PasteFromClipboard(object sender, EventArgs e)
        {
            ToolStripMenuItem TMI = (ToolStripMenuItem)sender;
            cExtendedTable ET = (cExtendedTable)TMI.Tag;


            Series TmpSerie = new Series();


            for (int i = 0; i < ET[0].Count; i++)
            {
                DataPoint DP = new DataPoint();
                // DP.XValue = ET[0][i];

                List<double> LD = new List<double>();

                for (int j = 1; j < ET.Count; j++)
                {
                    LD.Add(ET[j][i]);
                }

                DP.SetValueY(LD.ToArray());

                TmpSerie.Points.Add(DP);

                //TmpSerie.
            }



            CurrentSeries.Add(TmpSerie);

            this.Run();

            //Run();

            //if (this.GraphBelow != null)
            //{
            //    if (this.GraphBelow.GetType() == typeof(cChart1DGraph))
            //    {
            //        //  ((cChart1DGraph)this.GraphBelow).Run();
            //    }
            //}



            //   ListInput = new cListExtendedTable();
            //  ListInput.Add(ET);

            ///  this.Run();

            // cExtendedTable doc = sender.GetType();

        }




        private void ToolStripMenuItem_MarkerSize(object sender, EventArgs e)
        {
            if (this.SliderForMarkerSize.ShowDialog() != DialogResult.OK) return;
            this.MarkerSize = (int)this.SliderForMarkerSize.numericUpDown.Value;
            if (this.InputSimpleData == null) return;

            for (int j = 0; j < this.InputSimpleData.Count; j++)
            {
                foreach (var item in this.Series[j].Points)
                    item.MarkerSize = this.MarkerSize;
            }
        }

        private void ToolStripMenuItem_LineWidth(object sender, EventArgs e)
        {
            if (this.SliderForLineWidth.ShowDialog() != DialogResult.OK) return;
            this.LineWidth = (int)this.SliderForLineWidth.numericUpDown.Value;
            if (this.InputSimpleData == null) return;

            for (int j = 0; j < this.InputSimpleData.Count; j++)
            {
                foreach (var item in this.Series[j].Points)
                    item.BorderWidth = this.LineWidth;
            }
        }

        private void ToolStripMenuItem_ChartOpacity(object sender, EventArgs e)
        {
            if (this.SliderForOpacity.ShowDialog() != DialogResult.OK) return;
            this.Opacity = (int)this.SliderForOpacity.numericUpDown.Value;
            if (this.InputSimpleData == null) return;

            for (int j = 0; j < this.InputSimpleData.Count; j++)
                foreach (var item in this.Series[j].Points)
                {
                    Color C = item.Color;
                    item.Color = Color.FromArgb(this.Opacity, C);
                }

        }

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

        private void ToolStripMenuItem_ChartFastPoint(object sender, EventArgs e)
        {
            ISPoint = !ISPoint;

            for (int IdxSerie = 0; IdxSerie < base.CurrentSeries.Count; IdxSerie++)
            {
                // Series NewSerie = new System.Windows.Forms.DataVisualization.Charting.Series(base.input[IdxSerie].Name);
                base.CurrentSeries[IdxSerie].ChartType = SeriesChartType.FastPoint;
             //   base.CurrentSeries[IdxSerie].ChartType = SeriesChartType.FastPoint;

                //foreach (var item in base.CurrentSeries[IdxSerie].Points)
                //{
                //    item.MarkerStyle = MarkerStyle.Circle;
                //}
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
             //   base.CurrentSeries[IdxSerie].ChartType = SeriesChartType.FastPoint;

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
        #endregion
    }

}
