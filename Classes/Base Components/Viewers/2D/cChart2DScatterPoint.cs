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
using ImageAnalysis;
using HCSAnalyzer.Classes.ImageAnalysis.FormsForImages;
using HCSAnalyzer.Forms.IO;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{

    public class cChart2DScatterPoint : cGraphGeneral
    {
        protected ToolStripMenuItem SpecificContextMenu = null;
        public bool IsLine = false;
        public bool IsBar = false;
        public bool ISPoint = true;
        FormForSingleSlider SliderForMarkerSize = new FormForSingleSlider("Marker Size");
        FormForSingleSlider SliderForOpacity = new FormForSingleSlider("Marker Opacity");
        public int Opacity = 255;
        public int MarkerSize = 10;
        public bool IsDisplayTrendLine = false;


        public cChart2DScatterPoint()
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
            RefreshDisplay();
        }


        public int IdxDesc0 = 0;
        public int IdxDesc1 = 1;
        public int IdxDescForMarkerSize = -1;
        public int IdxDescForMarkerColor = -1;

        public void RefreshDisplay()
        {
            if (InputSimpleData.Count == 0) return;

            Series NewSerie = new System.Windows.Forms.DataVisualization.Charting.Series(base.InputSimpleData.Name);

            base.LabelAxisX = InputSimpleData[IdxDesc0].Name;
            if (this.InputSimpleData.Count <= IdxDesc1) IdxDesc1 = this.InputSimpleData.Count - 1;
            base.LabelAxisY = InputSimpleData[IdxDesc1].Name;

            NewSerie.ChartType = SeriesChartType.Point;

            double _MinY = double.MaxValue;
            double _MinX = double.MaxValue;
            double _MaxX = double.MinValue;
            double _MaxY = double.MinValue;

            double TmpMinX, TmpMinY, TmpMaxX, TmpMaxY;
            double TMp;

            double MinVolume = 0;
            double MaxVolume = 0;

            double MinColor = 0;
            double MaxColor = 0;


            if (this.IdxDescForMarkerSize >= 0)
            {
                MinVolume = this.InputSimpleData[this.IdxDescForMarkerSize].Min();
                MaxVolume = this.InputSimpleData[this.IdxDescForMarkerSize].Max();
            }
            if (this.IdxDescForMarkerColor >= 0)
            {
                MinColor = this.InputSimpleData[this.IdxDescForMarkerColor].Min();
                MaxColor = this.InputSimpleData[this.IdxDescForMarkerColor].Max();
            }

            cLUTProcess LUTProcess = new cLUTProcess(cGlobalInfo.GraphsLUT);

            for (int j = 0; j < this.InputSimpleData[0].Count; j++)
            {
                DataPoint DP = new DataPoint();

                TMp = this.InputSimpleData[IdxDesc0][j];
                if (TMp < _MinX) _MinX = TMp;
                else if (TMp > _MaxX) _MaxX = TMp;
                DP.XValue = TMp;

                double[] Value = new double[1];
                TMp = this.InputSimpleData[IdxDesc1][j];

                if (TMp < _MinY) _MinY = TMp;
                else if (TMp > _MaxY) _MaxY = TMp;

                Value[0] = TMp;
                DP.YValues = Value;
                if (this.IdxDescForMarkerSize < 0)
                    DP.MarkerSize = this.MarkerSize;
                else
                {
                    //   DP.MarkerSize = this.MarkerSize * 2;
                    int MarkerArea = (int)((50 * (this.InputSimpleData[this.IdxDescForMarkerSize][j] - MinVolume)) / (MaxVolume - MinVolume));
                    DP.MarkerSize = MarkerArea + this.MarkerSize;

                }

                DP.MarkerStyle = MarkerStyle.Circle;

                if (IsBorder)
                {
                    DP.MarkerBorderColor = Color.Black;
                    DP.MarkerBorderWidth = 1;
                }

                int IdxColor = j % LUTProcess.GetNumberOfColors();

                if (this.InputSimpleData.ListTags != null)
                {
                    if (j >= this.InputSimpleData.ListTags.Count) continue;
                    DP.Tag = this.InputSimpleData.ListTags[j];

                    if (DP.Tag.GetType() == typeof(cWell))
                    {
                        if (IdxDescForMarkerColor == -1) DP.Color = Color.FromArgb(this.Opacity, ((cWell)(DP.Tag)).GetClassColor());
                        DP.ToolTip = ((cWell)(DP.Tag)).GetShortInfo() + Value[0];
                    }
                    if (DP.Tag.GetType() == typeof(cSingleBiologicalObject))
                    {
                        if (IdxDescForMarkerColor == -1) DP.Color = Color.FromArgb(this.Opacity, ((cSingleBiologicalObject)(DP.Tag)).GetColor());
                        DP.ToolTip = ((cSingleBiologicalObject)(DP.Tag)).GetAssociatedPhenotype().Name + "\nValue: (" + DP.XValue.ToString("N2") + ":" + DP.YValues[0].ToString("N2") + ")";
                    }
                    if (DP.Tag.GetType() == typeof(cDescriptorType))
                    {
                        if (IdxDescForMarkerColor == -1) DP.Color = Color.FromArgb(this.Opacity, LUTProcess.GetColor(IdxColor));//((cWell)(DP.Tag)).GetClassColor();
                        DP.ToolTip = ((cDescriptorType)(DP.Tag)).GetShortInfo() + Value[0];
                        DP.AxisLabel = ((cDescriptorType)(DP.Tag)).GetName();
                        base.CurrentChartArea.AxisX.Interval = 1;
                    }
                    if (DP.Tag.GetType() == typeof(cPlate))
                    {
                        // DP.Color = ((cWell)(DP.Tag)).GetClassColor();
                        if (IdxDescForMarkerColor == -1) DP.Color = Color.FromArgb(this.Opacity, LUTProcess.GetColor(IdxColor));
                        DP.ToolTip = ((cPlate)(DP.Tag)).GetName() + " : " + Value[0];
                        DP.AxisLabel = ((cPlate)(DP.Tag)).GetName();
                        base.CurrentChartArea.AxisX.Interval = 1;
                    }
                    if (DP.Tag.GetType() == typeof(string))
                    {
                        if (IdxDescForMarkerColor == -1) DP.Color = Color.FromArgb(this.Opacity, LUTProcess.GetColor(IdxColor)); ;
                        DP.ToolTip = (string)(DP.Tag);
                    }
                }


                if (IdxDescForMarkerColor != -1)
                {
                    int MarkerColor = (int)(((LUTProcess.GetNumberOfColors() - 1) * (this.InputSimpleData[this.IdxDescForMarkerColor][j] - MinColor)) / (MaxColor - MinColor));
                    DP.Color = Color.FromArgb(this.Opacity, LUTProcess.GetColor(MarkerColor));

                }
                //if (this.InputSimpleData[0].ListTags != null)
                //{
                //    if (j >= this.InputSimpleData[0].ListTags.Count) continue;
                //    DP.Tag = this.InputSimpleData[0].ListTags[j];

                //    if (DP.Tag.GetType() == typeof(cWell))
                //    {
                //        DP.Color = ((cWell)(DP.Tag)).GetClassColor();
                //        DP.ToolTip = ((cWell)(DP.Tag)).GetShortInfo() + Value[0];
                //    }
                //    if (DP.Tag.GetType() == typeof(cSingleBiologicalObject))
                //    {
                //        DP.Color = ((cSingleBiologicalObject)(DP.Tag)).GetColor();
                //        DP.ToolTip = ((cSingleBiologicalObject)(DP.Tag)).GetAssociatedPhenotype().Name + "\nValue: (" + DP.XValue.ToString("N2") + ":" + DP.YValues[0].ToString("N2") + ")";
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
            }

            base.CurrentSeries.Clear();
            base.CurrentSeries.Add(NewSerie);
            base.Run();

            if (_MinX == _MaxX) _MaxX = _MinX + 1;
            if (_MinY == _MaxY) _MaxY = _MinY + 1;

            base.ChartAreas[0].AxisX.Minimum = _MinX;
            base.ChartAreas[0].AxisY.Minimum = _MinY;

            base.ChartAreas[0].AxisX.Maximum = _MaxX;
            base.ChartAreas[0].AxisY.Maximum = _MaxY;

            if (IsDisplayTrendLine)
            {
                this.Series.Add("TrendLine");
                this.Series["TrendLine"].ChartType = SeriesChartType.Line;
                this.Series["TrendLine"].BorderWidth = 1;
                this.Series["TrendLine"].Color = Color.Red;
                string typeRegression = "Linear";//"Exponential";//
                string forecasting = "1";
                string error = "false";
                string forecastingError = "false";
                string parameters = typeRegression + ',' + forecasting + ',' + error + ',' + forecastingError;
                this.Series[0].Sort(PointSortOrder.Ascending, "X");
                this.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, parameters, this.Series[0], this.Series["TrendLine"]);
            }

            base.CurrentTitle.Text = InputSimpleData.Name + " - " + base.LabelAxisX + " vs. " + base.LabelAxisY + " (" + this.InputSimpleData[0].Count + " points)";
        }

        #region Context Menu
        public ToolStripMenuItem GetContextMenu()
        {
            SpecificContextMenu = new ToolStripMenuItem("Graph Options");

            //ToolStripMenuItem ToolStripMenuItem_ChartLine = new ToolStripMenuItem("Line");
            //ToolStripMenuItem_ChartLine.CheckOnClick = true;
            //ToolStripMenuItem_ChartLine.Click += new System.EventHandler(this.ToolStripMenuItem_ChartLine);
            //SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChartLine);

            //ToolStripMenuItem ToolStripMenuItem_ChartBar = new ToolStripMenuItem("Bar");
            //ToolStripMenuItem_ChartBar.CheckOnClick = true;
            //ToolStripMenuItem_ChartBar.Click += new System.EventHandler(this.ToolStripMenuItem_ChartBar);
            //SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChartBar);

            //ToolStripMenuItem ToolStripMenuItem_ChartPoint = new ToolStripMenuItem("Point");
            //ToolStripMenuItem_ChartPoint.CheckOnClick = true;
            //ToolStripMenuItem_ChartPoint.Click += new System.EventHandler(this.ToolStripMenuItem_ChartPoint);
            //SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChartPoint);

            // SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_ChartOpacity = new ToolStripMenuItem("Opacity");
            ToolStripMenuItem_ChartOpacity.Click += new System.EventHandler(this.ToolStripMenuItem_ChartOpacity);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChartOpacity);

            ToolStripMenuItem ToolStripMenuItem_MarkerSize = new ToolStripMenuItem("Marker Size");
            ToolStripMenuItem_MarkerSize.Click += new System.EventHandler(this.ToolStripMenuItem_MarkerSize);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_MarkerSize);

            SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_XAxis = new ToolStripMenuItem("X-Axis");
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_XAxis);

            int IdxDesc = 0;
            foreach (var item in this.InputSimpleData)
            {
                ToolStripMenuItem ToolStripMenuItem_DescX = new ToolStripMenuItem(item.Name);
                ToolStripMenuItem_DescX.Tag = IdxDesc++;
                ToolStripMenuItem_DescX.Click += new System.EventHandler(this.ToolStripMenuItem_DescX);
                ToolStripMenuItem_XAxis.DropDownItems.Add(ToolStripMenuItem_DescX);

            }

            ToolStripMenuItem ToolStripMenuItem_YAxis = new ToolStripMenuItem("Y-Axis");
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_YAxis);

            IdxDesc = 0;
            foreach (var item in this.InputSimpleData)
            {
                ToolStripMenuItem ToolStripMenuItem_DescY = new ToolStripMenuItem(item.Name);
                ToolStripMenuItem_DescY.Tag = IdxDesc++;
                ToolStripMenuItem_DescY.Click += new System.EventHandler(this.ToolStripMenuItem_DescY);
                ToolStripMenuItem_YAxis.DropDownItems.Add(ToolStripMenuItem_DescY);

            }

            #region Marker Size
            ToolStripMenuItem ToolStripMenuItem_MarkerSizeType = new ToolStripMenuItem("Marker Size Style");
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_MarkerSizeType);

            IdxDesc = -1;

            ToolStripMenuItem ToolStripMenuItem_DescMarkerSize = new ToolStripMenuItem("Constant");
            ToolStripMenuItem_DescMarkerSize.Tag = IdxDesc++;
            ToolStripMenuItem_DescMarkerSize.Click += new System.EventHandler(this.ToolStripMenuItem_DescMarkerSize);
            ToolStripMenuItem_MarkerSizeType.DropDownItems.Add(ToolStripMenuItem_DescMarkerSize);

            ToolStripMenuItem_MarkerSizeType.DropDownItems.Add(new ToolStripSeparator());

            foreach (var item in this.InputSimpleData)
            {
                ToolStripMenuItem_DescMarkerSize = new ToolStripMenuItem(item.Name);
                ToolStripMenuItem_DescMarkerSize.Tag = IdxDesc++;
                ToolStripMenuItem_DescMarkerSize.Click += new System.EventHandler(this.ToolStripMenuItem_DescMarkerSize);
                ToolStripMenuItem_MarkerSizeType.DropDownItems.Add(ToolStripMenuItem_DescMarkerSize);

            }
            #endregion

            #region Marker Color
            ToolStripMenuItem ToolStripMenuItem_MarkerColorStyle = new ToolStripMenuItem("Marker Color Style");
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_MarkerColorStyle);

            IdxDesc = -1;

            ToolStripMenuItem ToolStripMenuItem_DescMarkerColor = new ToolStripMenuItem("Automated");
            ToolStripMenuItem_DescMarkerColor.Tag = IdxDesc++;
            ToolStripMenuItem_DescMarkerColor.Click += new System.EventHandler(this.ToolStripMenuItem_DescMarkerColor);
            ToolStripMenuItem_MarkerColorStyle.DropDownItems.Add(ToolStripMenuItem_DescMarkerColor);


            ToolStripMenuItem_MarkerColorStyle.DropDownItems.Add(new ToolStripSeparator());

            foreach (var item in this.InputSimpleData)
            {
                ToolStripMenuItem_DescMarkerColor = new ToolStripMenuItem(item.Name);
                ToolStripMenuItem_DescMarkerColor.Tag = IdxDesc++;
                ToolStripMenuItem_DescMarkerColor.Click += new System.EventHandler(this.ToolStripMenuItem_DescMarkerColor);
                ToolStripMenuItem_MarkerColorStyle.DropDownItems.Add(ToolStripMenuItem_DescMarkerColor);

            }
            #endregion

            return this.SpecificContextMenu;
        }

        private void ToolStripMenuItem_DescMarkerColor(object sender, EventArgs e)
        {
            this.IdxDescForMarkerColor = (int)(((ToolStripMenuItem)(sender)).Tag);
            RefreshDisplay();
        }

        private void ToolStripMenuItem_DescMarkerSize(object sender, EventArgs e)
        {
            this.IdxDescForMarkerSize = (int)(((ToolStripMenuItem)(sender)).Tag);
            RefreshDisplay();
        }

        private void ToolStripMenuItem_DescX(object sender, EventArgs e)
        {
            this.IdxDesc0 = (int)(((ToolStripMenuItem)(sender)).Tag);
            RefreshDisplay();
        }

        private void ToolStripMenuItem_DescY(object sender, EventArgs e)
        {
            this.IdxDesc1 = (int)(((ToolStripMenuItem)(sender)).Tag);
            RefreshDisplay();
        }

        private void ToolStripMenuItem_MarkerSize(object sender, EventArgs e)
        {
            if (this.SliderForMarkerSize.ShowDialog() != DialogResult.OK) return;
            this.MarkerSize = (int)this.SliderForMarkerSize.numericUpDown.Value;
            RefreshDisplay();
        }

        private void ToolStripMenuItem_ChartOpacity(object sender, EventArgs e)
        {
            if (this.SliderForOpacity.ShowDialog() != DialogResult.OK) return;
            this.Opacity = (int)this.SliderForOpacity.numericUpDown.Value;
            RefreshDisplay();
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

        double MaxX, MaxY, MinX, MinY;

        private void AssociatedChart_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1) return;
            if (e.Button != MouseButtons.Right) return;

            ContextMenuStrip NewMenu = new ContextMenuStrip();
            foreach (var item in base.GetContextMenu(e))
                NewMenu.Items.Add(item);

            NewMenu.Items.Add(this.GetContextMenu());

            #region Selection process
            MaxX = this.ChartAreas[0].CursorX.SelectionEnd;
            MinX = this.ChartAreas[0].CursorX.SelectionStart;

            if (MaxX < MinX)
            {
                MinX = this.ChartAreas[0].CursorX.SelectionEnd;
                MaxX = this.ChartAreas[0].CursorX.SelectionStart;
            }

            MaxY = this.ChartAreas[0].CursorY.SelectionEnd;
            MinY = this.ChartAreas[0].CursorY.SelectionStart;

            if (MaxY < MinY)
            {
                MinY = this.ChartAreas[0].CursorY.SelectionEnd;
                MaxY = this.ChartAreas[0].CursorY.SelectionStart;
            }

            cListWells ListWells = new cListWells(this);
            List<DataPoint> LDP = new List<DataPoint>();
            //cListWell ListWellsToProcess = new cListWell();


            foreach (DataPoint item in this.Series[0].Points)
            {
                if ((item.XValue >= MinX) && (item.XValue <= MaxX) && (item.YValues[0] >= MinY) && (item.YValues[0] <= MaxY))
                {
                    if ((item.Tag != null) && (item.Tag.GetType() == typeof(cWell)))
                    {
                        ListWells.Add((cWell)(item.Tag));
                        LDP.Add(item);
                    }
                }
            }

            if (LDP.Count > 0)
            {
                ToolStripMenuItem SpecificContextMenu = ListWells.GetContextMenu(); //new ToolStripMenuItem("List " + LDP.Count + " wells");
                //ToolStripMenuItem ToolStripMenuItem_ChangeClass = ListWells.GetContextMenu();// new ToolStripMenuItem("Classes");
                //ToolStripMenuItem_CopyClassToClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_CopyClassToClipBoard);
                //SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChangeClass);

                //cWell TmpWell = (cWell)(LDP[0].Tag);

                //for (int i = 0; i < TmpWell.cGlobalInfo.ListWellClasses.Count; i++)
                //{
                //    ToolStripMenuItem ToolStripMenuItem_NewClass = new ToolStripMenuItem(TmpWell.cGlobalInfo.ListWellClasses[i].Name);
                //    ToolStripMenuItem_NewClass.Click += new System.EventHandler(this.ToolStripMenuItem_NewClass);
                //    ToolStripMenuItem_NewClass.Tag = LDP;
                //    ToolStripMenuItem_ChangeClass.DropDownItems.Add(ToolStripMenuItem_NewClass);
                //}
                NewMenu.Items.Add(SpecificContextMenu);
            }

            if ((MinX < MaxX) && (MaxY > MinY))
            {
                NewMenu.Items.Add(new ToolStripSeparator());

                int NumPtsSelected = 0;
                List<int> ListIdx = new List<int>();


                for (int j = 0; j < this.InputSimpleData[0].Count; j++)
                {
                    if ((this.InputSimpleData[IdxDesc0][j] <= MaxX) && (this.InputSimpleData[IdxDesc0][j] >= MinX) && (this.InputSimpleData[IdxDesc1][j] <= MaxY) && (this.InputSimpleData[IdxDesc1][j] >= MinY))
                    {
                        ListIdx.Add(j);

                        NumPtsSelected++;
                    }
                }

                if (NumPtsSelected > 0)
                {
                    ToolStripMenuItem TSItemSubPopSelection = new ToolStripMenuItem("List " + NumPtsSelected + " Objects");

                    ToolStripMenuItem TSItemSubPopSelectionDataTable = new ToolStripMenuItem("Display Data Table");
                    TSItemSubPopSelectionDataTable.Click += new System.EventHandler(this.TSItemSubPopSelectionDataTable);
                    TSItemSubPopSelectionDataTable.Tag = ListIdx;
                    TSItemSubPopSelection.DropDownItems.Add(TSItemSubPopSelectionDataTable);

                    ToolStripMenuItem TSItemSubPopSelectionDataTableImages = new ToolStripMenuItem("Display Data Table (inc. Images)");
                    TSItemSubPopSelectionDataTableImages.Click += new System.EventHandler(this.TSItemSubPopSelectionDataTableImages);
                    TSItemSubPopSelectionDataTableImages.Tag = ListIdx;
                    TSItemSubPopSelection.DropDownItems.Add(TSItemSubPopSelectionDataTableImages);

                    NewMenu.Items.Add(TSItemSubPopSelection);
                }



                ToolStripMenuItem TSItemZoom = new ToolStripMenuItem("Zoom");
                TSItemZoom.Click += new System.EventHandler(this.TSItemZoom);
                NewMenu.Items.Add(TSItemZoom);




            }


            #endregion

            NewMenu.DropShadowEnabled = true;
            NewMenu.Show(Control.MousePosition);
        }



        private void TSItemSubPopSelectionDataTable(object sender, EventArgs e)
        {
            ToolStripMenuItem ParentTag = (ToolStripMenuItem)(sender);
            List<int> MyList = ((List<int>)(ParentTag.Tag));

            cExtendedTable ET = new cExtendedTable(this.InputSimpleData.Count, MyList.Count, 0);


            for (int i = 0; i < this.InputSimpleData.Count; i++)
            {
                ET[i].Name = this.InputSimpleData[i].Name;
            }

            ET.ListRowNames = new List<string>();
            for (int i = 0; i < MyList.Count; i++)
            {
                ET.ListRowNames.Add(i.ToString());
            }

            int IdxPt = 0;
            foreach (var item in MyList)
            {
                for (int i = 0; i < this.InputSimpleData.Count; i++)
                {
                    ET[i][IdxPt] = this.InputSimpleData[i][MyList[IdxPt]];
                }
                IdxPt++;
            }
            ET.Name = "Data Table - " + MyList.Count + " Objects";
            cDisplayExtendedTable DET = new cDisplayExtendedTable();
            DET.SetInputData(ET);
            DET.Run();
        }


        private void TSItemSubPopSelectionDataTableImages(object sender, EventArgs e)
        {
            ToolStripMenuItem ParentTag = (ToolStripMenuItem)(sender);
            List<int> MyList = ((List<int>)(ParentTag.Tag));

            cExtendedTable ET = new cExtendedTable(this.InputSimpleData.Count, MyList.Count, 0);


            for (int i = 0; i < this.InputSimpleData.Count; i++)
            {
                ET[i].Name = this.InputSimpleData[i].Name;
            }

            ET.ListRowNames = new List<string>();
            for (int i = 0; i < MyList.Count; i++)
            {
                ET.ListRowNames.Add(i.ToString());
            }


            if (this.InputSimpleData.ListTags != null) ET.ListTags = new List<object>();

            int IdxPt = 0;

            FormForProgress FP = new FormForProgress();
            FP.Show();

            int MaxProgress = MyList.Count;
            FP.progressBar.Maximum = MaxProgress;

            foreach (var item in MyList)
            {
                for (int i = 0; i < this.InputSimpleData.Count; i++)
                {
                    ET[i][IdxPt] = this.InputSimpleData[i][MyList[IdxPt]];
                }

                if ((this.InputSimpleData.ListTags != null) && (this.InputSimpleData.ListTags[IdxPt] != null))
                {
                    if (this.InputSimpleData.ListTags[MyList[IdxPt]].GetType() == typeof(cSingleBiologicalObject))
                    {
                        cSingleBiologicalObject TmpBioObj = ((cSingleBiologicalObject)this.InputSimpleData.ListTags[MyList[IdxPt]]);
                        List<cImageMetaInfo> ListMeta = cGlobalInfo.ImageAccessor.GetImageInfo(TmpBioObj);
                        if (ListMeta==null) continue;
                        cImage Image = new cImage(ListMeta);
                        TmpBioObj.BD_BoxMax.Z = TmpBioObj.BD_BoxMin.Z = 0;
                        cImage CroppedImaged = Image.Crop(TmpBioObj.BD_BoxMin, TmpBioObj.BD_BoxMax);

                        if((cGlobalInfo.TmpImageDisplayProperties==null)||(cGlobalInfo.TmpImageDisplayProperties.ListMin.Count != CroppedImaged.GetNumChannels()))
                        {
                            ET.ListTags.Add(CroppedImaged.GetBitmap(1f, null, null));
                        }
                        else
                        {
                            cImageDisplayProperties IP = cGlobalInfo.TmpImageDisplayProperties;
                            ET.ListTags.Add(CroppedImaged.GetBitmap(1f, IP, null));
                        }

                        FP.richTextBoxForComment.AppendText("Object "+ IdxPt +" / "+MyList.Count +"\n");
                    }

                }
                FP.progressBar.Value = IdxPt;
                
                FP.Refresh();
                IdxPt++;
            }

            FP.Close();

            ET.Name = "Data Table - " + MyList.Count + " Objects";
            cDisplayExtendedTable DET = new cDisplayExtendedTable();
            DET.SetInputData(ET);
            DET.Run();



            //DataPoint DP = new DataPoint();

            //if (this.InputSimpleData[0].ListTags != null)
            //{
            //    if (j >= this.InputSimpleData[0].ListTags.Count) continue;
            //    DP.Tag = this.InputSimpleData[0].ListTags[j];

            //    //    if (DP.Tag.GetType() == typeof(cWell))
            //    //    {
            //    //        DP.Color = ((cWell)(DP.Tag)).GetClassColor();
            //    //        DP.ToolTip = ((cWell)(DP.Tag)).GetShortInfo() + Value[0];
            //    //    }
            //    if (DP.Tag.GetType() == typeof(cSingleBiologicalObject))
            //    {
            //        //        DP.Color = ((cSingleBiologicalObject)(DP.Tag)).GetColor();
            //        //        DP.ToolTip = ((cSingleBiologicalObject)(DP.Tag)).GetAssociatedPhenotype().Name + "\nValue: (" + DP.XValue.ToString("N2") + ":" + DP.YValues[0].ToString("N2") + ")";

            //    }
            //}

        }


        private void TSItemZoom(object sender, EventArgs e)
        {
            base.ChartAreas[0].AxisX.Minimum = MinX;
            base.ChartAreas[0].AxisX.Maximum = MaxX;
            base.ChartAreas[0].AxisY.Minimum = MinY;
            base.ChartAreas[0].AxisY.Maximum = MaxY;
            base.Update();
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
