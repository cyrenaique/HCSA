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
using HCSAnalyzer.Classes.Base_Classes.General;
using System.Runtime.Serialization.Formatters.Binary;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{


    public abstract class cGraphGeneral : Chart
    {
        // protected ToolStripMenuItem SpecificContextMenu = null;
        public cExtendedTable InputSimpleData;
        public cListExtendedTable ListInput = null;

        protected ChartArea CurrentChartArea = new ChartArea("ChartArea");
        protected List<Series> CurrentSeries = new List<Series>();

        public Color BackgroundColor = Color.FromArgb(255, 255, 255);
        public Color GridColor = Color.Silver;
        public bool IsShadow = false;
        public bool IsBorder = true;

        public bool IsXGrid = false;
        public bool IsYGrid = false;

        public bool IsDetachable = true;

        public string LabelAxisX = "";
        public string LabelAxisY = "";

        public int XAxisFormatDigitNumber = 2;
        public int YAxisFormatDigitNumber = 4;

        public bool IsZoomableX = false;
        public bool IsZoomableY = false;
        public bool IsSelectable = false;
        public bool IsDisplayValues = false;
        public bool IsLegend = false;

        public cExtendedList DefaultAxisXMin = null;
        public cExtendedList DefaultAxisYMin = null;
        public cExtendedList DefaultAxisXMax = null;
        public cExtendedList DefaultAxisYMax = null;

        public List<cLineVerticalForGraph> ListVerticalLines = new List<cLineVerticalForGraph>();
        public List<cLineHorizontalForGraph> ListHorizontalLines = new List<cLineHorizontalForGraph>();

        //FormForSingleSlider SliderForMinX = new FormForSingleSlider("Minimum X");
        FormForXYMinMax WindowForXYMinMax = new FormForXYMinMax();
        // protected cLUT CurrentLUT = new cLUT();



        protected bool IsAllowDisplayValue = true;
        protected bool IsAllowDisplayTable = true;

        public Title CurrentTitle = new Title();
        protected object GraphBelow;

        public cGraphGeneral()
        {
            this.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                        | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);


        }


        protected void Run()
        {
            base.ChartAreas.Clear();

            this.BackColor = this.BackgroundColor;
            CurrentChartArea.BackColor = this.BackgroundColor;

            if (InputSimpleData != null)
                CurrentTitle.Text = InputSimpleData.Name;
            else if (ListInput != null)
                CurrentTitle.Text = ListInput.Name;
            else
                CurrentTitle.Text = "Curves 1D";

            CurrentTitle.Font = new System.Drawing.Font("Arial", 11, FontStyle.Bold);
            this.Titles.Clear();
            this.Titles.Add(CurrentTitle);

            if (this.XAxisFormatDigitNumber < 0)
                CurrentChartArea.AxisX.LabelStyle.Format = "G";
            else
                CurrentChartArea.AxisX.LabelStyle.Format = "N" + XAxisFormatDigitNumber;

          //  CurrentChartArea.AxisX.Interval = 20000;
            //CurrentChartArea.AxisY.LabelStyle.p
          //  CurrentChartArea.AxisX.MajorTickMark.Interval = 30000;
            
            


            if (this.YAxisFormatDigitNumber < 0)
                CurrentChartArea.AxisY.LabelStyle.Format = "G";
            else
                CurrentChartArea.AxisY.LabelStyle.Format = "N" + YAxisFormatDigitNumber;

            CurrentChartArea.Axes[0].MajorGrid.Enabled = this.IsYGrid;
            CurrentChartArea.Axes[0].LabelStyle.Enabled = true;
            //CurrentChartArea.Axes[0].MinorTickMark.Enabled = true;
            CurrentChartArea.Axes[0].MajorGrid.LineColor = GridColor;

            if (this.LabelAxisX != "")
                CurrentChartArea.Axes[0].Title = this.LabelAxisX;
            if (this.LabelAxisY != "")
                CurrentChartArea.Axes[1].Title = this.LabelAxisY;

            CurrentChartArea.Axes[1].LabelStyle.Enabled = true;
            CurrentChartArea.Axes[1].MajorGrid.Enabled = this.IsXGrid;
            CurrentChartArea.Axes[1].MajorGrid.LineColor = GridColor;

            this.ChartAreas.Add(CurrentChartArea);
            this.Series.Clear();

            foreach (var item in this.CurrentSeries)
            {
                if (this.IsShadow)
                    item.ShadowOffset = 1;
                else
                    item.ShadowOffset = 0;

                if (this.IsBorder)
                    item.BorderWidth = 1;
                else
                    item.BorderWidth = 0;

                this.Series.Add(item);
            }

            if (this.IsDisplayValues)
            {
                foreach (var item in this.Series)
                {
                    if (item.Name.Contains("ComplexD_ataTable")) continue;
                    foreach (var Pt in item.Points)
                    {
                        Pt.LabelFormat = "N2";
                        Pt.IsValueShownAsLabel = true;
                        Pt.Font = new Font("Arial", 8);
                    }
                }
            }

            CurrentChartArea.CursorX.IsUserSelectionEnabled = this.IsSelectable;
            CurrentChartArea.CursorY.IsUserSelectionEnabled = this.IsSelectable;

            CurrentChartArea.CursorX.SelectionColor = Color.Black;
            CurrentChartArea.CursorY.SelectionColor = Color.Black;

            CurrentChartArea.CursorX.LineColor = Color.Black;
            CurrentChartArea.CursorY.LineColor = Color.Black;
            CurrentChartArea.CursorX.LineWidth = 1;
            CurrentChartArea.CursorY.LineWidth = 1;

            if (this.IsZoomableX)
            {
                this.ChartAreas[0].AxisX.ScaleView.Zoomable = this.IsZoomableX;
                CurrentChartArea.CursorX.IsUserSelectionEnabled = this.IsZoomableX;
            }
            if (this.IsZoomableX)
            {
                this.ChartAreas[0].AxisY.ScaleView.Zoomable = this.IsZoomableY;
                CurrentChartArea.CursorY.IsUserSelectionEnabled = this.IsZoomableY;
            }
            if (this.IsSelectable)
            {
                this.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
                CurrentChartArea.CursorX.IsUserSelectionEnabled = true;
                this.ChartAreas[0].AxisY.ScaleView.Zoomable = false;
                CurrentChartArea.CursorY.IsUserSelectionEnabled = true;
            }

            CurrentChartArea.AxisX.IsLabelAutoFit = true;

            if (this.IsLegend)
            {
                Legend MyLegend = new Legend();
                this.Legends.Clear();
                this.Legends.Add(MyLegend);
                this.Legends[0].ShadowOffset = 5;
                this.CustomizeLegend += new EventHandler<CustomizeLegendEventArgs>(Chart_CustomizeLegend);
            }

            this.Annotations.Clear();
            foreach (var item in ListVerticalLines) item.Run(this);
            foreach (var item in ListHorizontalLines) item.Run(this);

            if (this.IsSelectable)
            {
                CurrentChartArea.CursorX.Interval = 0.01;
                CurrentChartArea.CursorY.Interval = 0.01;
            }

            if (this.IsZoomableX)
            {
                CurrentChartArea.CursorX.Interval = double.Epsilon;
            }
            //if (this.IsZoomableX)
            //{
            //    CurrentChartArea.CursorX.Interval = 1;
            //}
            //if (this.IsZoomableY)
            //{
            //    CurrentChartArea.CursorY.Interval = double.Epsilon;
            //}

            if (DefaultAxisXMin != null)
                this.CurrentChartArea.AxisX.Minimum = DefaultAxisXMin[0];
            //else
                //this.CurrentChartArea.AxisX.auto

            if (DefaultAxisXMax != null)
                this.CurrentChartArea.AxisX.Maximum = DefaultAxisXMax[0];

            if (DefaultAxisYMin != null)
                this.CurrentChartArea.AxisY.Minimum = DefaultAxisYMin[0];

            if (DefaultAxisYMax != null)
                this.CurrentChartArea.AxisY.Maximum = DefaultAxisYMax[0];

        }

        public string GetInfo()
        {
            string ToReturn = "";
            ToReturn += this.Titles[0].Text+"\n\n";
            ToReturn += "Number of Series: " + this.CurrentSeries.Count + " \n";

            ToReturn += "X-Axis:\n";
            ToReturn += "\tMinimum: " + this.CurrentChartArea.AxisX.Minimum + "\n";
            ToReturn += "\tMaximum: " + this.CurrentChartArea.AxisX.Maximum + "\n";
            ToReturn += "Y-Axis:\n";
            ToReturn += "\tMinimum: " + this.CurrentChartArea.AxisY.Minimum + "\n";
            ToReturn += "\tMaximum: " + this.CurrentChartArea.AxisY.Maximum + "\n";
            ToReturn += "\n";
            return ToReturn;
        }


        void Chart_CustomizeLegend(object sender, CustomizeLegendEventArgs e)
        {
            e.LegendItems.Clear();
            int IdxSerie = 0;
            foreach (var item in this.Series)
            {
                if (item.Name.Contains("ComplexD_ataTable")) continue;
                if (item.Name == "TrendLine") continue;

                //if (item.Tag != null)
                //{
                LegendItem newItem = new LegendItem();
                newItem.ImageStyle = LegendImageStyle.Marker;
                newItem.MarkerStyle = MarkerStyle.Square;
                newItem.MarkerSize = 8;
                newItem.ShadowColor = Color.Black;
                newItem.ShadowOffset = 1;

                if (item.Tag != null)
                {
                    if (item.Tag.GetType() == typeof(cWellClassType))
                        newItem.MarkerBorderColor = newItem.MarkerColor = ((cWellClassType)(item.Tag)).ColourForDisplay;
                    else if (item.Tag.GetType() == typeof(cCellularPhenotype))
                        newItem.MarkerBorderColor = newItem.MarkerColor = ((cCellularPhenotype)(item.Tag)).ColourForDisplay;
                    else
                        newItem.MarkerBorderColor = newItem.MarkerColor= item.Color;
                }
                else
                {
                    cLUTProcess LUTProcess = new cLUTProcess(cGlobalInfo.GraphsLUT);

                    int IdxColor = IdxSerie % LUTProcess.GetNumberOfColors();
                    newItem.MarkerBorderColor = newItem.MarkerColor = LUTProcess.GetColor(IdxColor);
                }


                newItem.Cells.Add(LegendCellType.SeriesSymbol, "", ContentAlignment.MiddleLeft);
                newItem.Cells.Add(LegendCellType.Text, item.Name, ContentAlignment.MiddleLeft);
                e.LegendItems.Add(newItem);
                IdxSerie++;
                //}
            }
        }

        public List<ToolStripMenuItem> GetContextMenu(MouseEventArgs e)
        {
            List<ToolStripMenuItem> ToBeReturned = new List<ToolStripMenuItem>();

            #region General Display Menu
            ToolStripMenuItem SpecificContextMenu = new ToolStripMenuItem("General display");

            ToolStripMenuItem ToolStripMenuItem_BackGroundColor = new ToolStripMenuItem("Background Color");
            ToolStripMenuItem_BackGroundColor.Click += new System.EventHandler(this.ToolStripMenuItem_BackGroundColor);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_BackGroundColor);

            ToolStripMenuItem ToolStripMenuItem_IsShadow = new ToolStripMenuItem("Shadow");
            ToolStripMenuItem_IsShadow.CheckOnClick = true;
            ToolStripMenuItem_IsShadow.Checked = this.IsShadow;
            ToolStripMenuItem_IsShadow.Click += new System.EventHandler(this.ToolStripMenuItem_IsShadow);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_IsShadow);

            ToolStripMenuItem ToolStripMenuItem_IsBorder = new ToolStripMenuItem("Border");
            ToolStripMenuItem_IsBorder.CheckOnClick = true;
            ToolStripMenuItem_IsBorder.Checked = this.IsBorder;
            ToolStripMenuItem_IsBorder.Click += new System.EventHandler(this.ToolStripMenuItem_IsBorder);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_IsBorder);

            ToolStripMenuItem ToolStripMenuItem_IsLegend = new ToolStripMenuItem("Legend");
            ToolStripMenuItem_IsLegend.CheckOnClick = true;
            ToolStripMenuItem_IsLegend.Checked = this.IsLegend;
            ToolStripMenuItem_IsLegend.Click += new System.EventHandler(this.ToolStripMenuItem_IsLegend);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_IsLegend);

            if (IsAllowDisplayValue)
            {
                ToolStripMenuItem ToolStripMenuItem_IsDisplayValues = new ToolStripMenuItem("Values");
                ToolStripMenuItem_IsDisplayValues.CheckOnClick = true;
                ToolStripMenuItem_IsDisplayValues.Checked = this.IsDisplayValues;
                ToolStripMenuItem_IsDisplayValues.Click += new System.EventHandler(this.ToolStripMenuItem_IsDisplayValues);
                SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_IsDisplayValues);
            }

            SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_XAxis = new ToolStripMenuItem("X-Grid");
            ToolStripMenuItem_XAxis.CheckOnClick = true;
            ToolStripMenuItem_XAxis.Checked = this.IsXGrid;
            ToolStripMenuItem_XAxis.Click += new System.EventHandler(this.ToolStripMenuItem_XAxis);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_XAxis);

            ToolStripMenuItem ToolStripMenuItem_YAxis = new ToolStripMenuItem("Y-Grid");
            ToolStripMenuItem_YAxis.CheckOnClick = true;
            ToolStripMenuItem_YAxis.Checked = this.IsYGrid;
            ToolStripMenuItem_YAxis.Click += new System.EventHandler(this.ToolStripMenuItem_YAxis);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_YAxis);

            SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_CopyToClipBoard = new ToolStripMenuItem("Copy To Clipboard");
            ToolStripMenuItem_CopyToClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_CopyToClipBoard);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_CopyToClipBoard);


            if (IsDetachable)
            {
                ToolStripMenuItem ToolStripMenuItem_Detach = new ToolStripMenuItem("Detach");
                ToolStripMenuItem_Detach.Click += new System.EventHandler(this.ToolStripMenuItem_Detach);
                SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Detach);
            }

            if (IsAllowDisplayTable)
            {
                ToolStripMenuItem ToolStripMenuItem_DisplayDataTable = new ToolStripMenuItem("Display Data Table");
                ToolStripMenuItem_DisplayDataTable.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayDataTable);
                SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayDataTable);
            }

            SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_Axis = new ToolStripMenuItem("Axis Min-Max");
            ToolStripMenuItem_Axis.Click += new System.EventHandler(this.ToolStripMenuItem_Axis);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Axis);

            ToBeReturned.Add(SpecificContextMenu);
            #endregion

            #region manage context menu on the graph elements
            HitTestResult Res = this.HitTest(e.X, e.Y, ChartElementType.DataPoint);
            if (Res.Series != null)
            {
                DataPoint PtToTransfer = Res.Series.Points[Res.PointIndex];
                //Res.Series.Tag

                if (PtToTransfer.Tag != null)
                {


                    if (PtToTransfer.Tag.GetType() == typeof(cWell))
                    {
                        cWell TmpWell = (cWell)(PtToTransfer.Tag);
                        foreach (var item in TmpWell.GetExtendedContextMenu())
                            ToBeReturned.Add(item);
                    }
                    if (PtToTransfer.Tag.GetType() == typeof(cSingleBiologicalObject))
                    {
                        cSingleBiologicalObject TmpBiologicalObject = (cSingleBiologicalObject)(PtToTransfer.Tag);
                        foreach (var item in TmpBiologicalObject.GetExtendedContextMenu())
                            ToBeReturned.Add(item);

                        //ToolStripMenuItem ClassMenu = new ToolStripMenuItem("Class [" + TmpBiologicalObject.GetAssociatedPhenotype().Name+"]");
                        //ClassMenu.DropDownItems.Add(TmpBiologicalObject.GetAssociatedPhenotype().GetExtendedContextMenu());    
                        //ToBeReturned.Add(ClassMenu);

                    }
                    if (PtToTransfer.Tag.GetType() == typeof(cDescriptorType))
                    {
                        cDescriptorType TmpDesc = (cDescriptorType)(PtToTransfer.Tag);
                        foreach (var itemDesc in TmpDesc.GetExtendedContextMenu())
                            ToBeReturned.Add(itemDesc);
                    }
                    if (PtToTransfer.Tag.GetType() == typeof(cPlate))
                    {
                        cPlate TmpPlate = (cPlate)(PtToTransfer.Tag);
                        ToBeReturned.Add(TmpPlate.GetExtendedContextMenu());
                    }
                    if (PtToTransfer.Tag.GetType() == typeof(cWellClassType))
                    {
                        cWellClassType TmpClass = (cWellClassType)(PtToTransfer.Tag);
                        ToBeReturned.Add(TmpClass.GetExtendedContextMenu());
                    }
                    if (PtToTransfer.Tag.GetType() == typeof(cCellularPhenotype))
                    {
                        cCellularPhenotype TmpCellularPhenotype = (cCellularPhenotype)(PtToTransfer.Tag);
                        ToBeReturned.Add(TmpCellularPhenotype.GetContextMenu());
                    }

                    ToolStripMenuItem TSMI = new ToolStripMenuItem("Serie [" + Res.Series.Name + "]");

                    ToolStripMenuItem ToolStripMenuItem_DisplaySerieData = new ToolStripMenuItem("Display Data Table");
                    ToolStripMenuItem_DisplaySerieData.Tag = Res;
                    ToolStripMenuItem_DisplaySerieData.Click += new System.EventHandler(this.ToolStripMenuItem_DisplaySerieData);
                    TSMI.DropDownItems.Add(ToolStripMenuItem_DisplaySerieData);


                    ToolStripMenuItem ToolStripMenuItem_CopyToclipboardSerieData = new ToolStripMenuItem("Copy to Clipboard");
                    ToolStripMenuItem_CopyToclipboardSerieData.Tag = Res;
                    ToolStripMenuItem_CopyToclipboardSerieData.Click += new System.EventHandler(this.ToolStripMenuItem_CopyToclipboardSerieData);
                    TSMI.DropDownItems.Add(ToolStripMenuItem_CopyToclipboardSerieData);

                    //TSMI.DropDownItems.Add(new ToolStripSeparator());

                    ToBeReturned.Add(TSMI);

                }
            }

            HitTestResult ResForTitle = this.HitTest(e.X, e.Y, ChartElementType.Title);
            if ((ResForTitle != null) && (ResForTitle.Object != null))
            {
                Title TmpTitle = (Title)ResForTitle.Object;

                if ((TmpTitle.Tag != null) && (TmpTitle.Tag.GetType() == typeof(cPlate)))
                {
                    cPlate TmpPlate = (cPlate)(TmpTitle.Tag);
                    if (TmpPlate.GetContextMenu() != null)
                        ToBeReturned.Add(TmpPlate.GetContextMenu());
                }
            }

            //  HitTestResult ResForLegend = this.HitTest(e.X, e.Y, ChartElementType.LegendArea);
            ////  if (ResForTitle.Series != null)
            //  {
            //      MemoryStream ms = new MemoryStream();
            //      this.SaveImage(ms, ChartImageFormat.Bmp);
            //      Bitmap bm = new Bitmap(ms);

            //      Rectangle Rec = new Rectangle((int)this.Legends[0].Position.X,
            //                                    (int)this.Legends[0].Position.Y,
            //                                    (int)this.Legends[0].Position.Width,
            //                                    (int)this.Legends[0].Position.Height);

            //      System.Drawing.Imaging.PixelFormat format = bm.PixelFormat;
            //      Bitmap cloneBitmap = bm.Clone(Rec, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            //      Clipboard.SetImage(cloneBitmap);

            //  }

            #endregion

            return ToBeReturned;
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
            //Run();

            if (this.GraphBelow != null)
            {
                if(this.GraphBelow.GetType()==typeof(cChart1DGraph))
                {
                  //  ((cChart1DGraph)this.GraphBelow).Run();
                }
            }



         //   ListInput = new cListExtendedTable();
          //  ListInput.Add(ET);

          ///  this.Run();

           // cExtendedTable doc = sender.GetType();
           
        }

        private void ToolStripMenuItem_CopyToclipboardSerieData(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(ToolStripMenuItem)) return;

            ToolStripMenuItem TSMI = (ToolStripMenuItem)sender;
            if (TSMI.Tag.GetType() != typeof(HitTestResult)) return;

            HitTestResult HTR = (HitTestResult)TSMI.Tag;


            if (HTR.Series != null)
            {
                int NumValues = HTR.Series.Points[0].YValues.Length;

                cExtendedTable NewTable = new cExtendedTable(NumValues + 1, HTR.Series.Points.Count, 0);

                for (int i = 0; i < HTR.Series.Points.Count; i++)
                {
                    DataPoint DP = HTR.Series.Points[i];
                    NewTable[0][i] = DP.XValue;

                    for (int j = 0; j < NumValues; j++)
                    {
                        NewTable[j + 1][i] = DP.YValues[j];
                    }

                }

             //   NewTable.Tag = HTR.Series;

                DataFormats.Format format = DataFormats.GetFormat(typeof(cExtendedTable).FullName);

                //now copy to clipboard 
                Clipboard.Clear();
                IDataObject dataObj = new DataObject();
                dataObj.SetData(format.Name, false, NewTable);
                Clipboard.SetDataObject(dataObj, false);

                //DataPoint PtToTransfer = HTR.Series.Points[Res.PointIndex];
                //Res.Series.Tag
               
              //  Clipboard.SetDataObject(NewTable, false);
            }




            // Clipboard.SetData(format, users);
            //SeriesForClip SFC = new SeriesForClip();
            //SFC = (SeriesForClip)HTR.Series;
           // cExtendedTable AT = new cExtendedTable();

          //  IsSerializable(AT);


            //Clipboard.SetData("HCSAnalyzer2DChartDataSeries", SFC);

        }

        // using System.Runtime.Serialization.Formatters.Binary;
        private static bool IsSerializable(object obj)
        {
            System.IO.MemoryStream mem = new System.IO.MemoryStream();
            BinaryFormatter bin = new BinaryFormatter();
            try
            {
                bin.Serialize(mem, obj);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Your object cannot be serialized." +
                                 " The reason is: " + ex.ToString());
                return false;
            }
        }

        private void ToolStripMenuItem_DisplaySerieData(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(ToolStripMenuItem)) return;

            ToolStripMenuItem TSMI = (ToolStripMenuItem)sender;
            if (TSMI.Tag.GetType() != typeof(HitTestResult)) return;

            HitTestResult HTR = (HitTestResult)TSMI.Tag;
            cExtendedList ELX = new cExtendedList(this.ChartAreas[0].AxisX.Title);
            cExtendedList ELY = new cExtendedList(this.ChartAreas[0].AxisY.Title);
            cExtendedTable ET = new cExtendedTable();
            ET.ListTags = new List<object>();
            ET.Name = "Serie [" + HTR.Series.Name + "]";


            foreach (var item in HTR.Series.Points)
            {
                ELX.Add(item.XValue);
                ELY.Add(item.YValues[0]);
                ET.ListTags.Add(item.Tag);
            }

            ET.Add(ELX);
            ET.Add(ELY);

            cDisplayExtendedTable DET = new cDisplayExtendedTable();
            DET.SetInputData(ET);
            DET.Run();

        }

        public double GetAxisXMin()
        { 
            return this.CurrentChartArea.AxisX.Minimum;
        }

        public double GetAxisXMax()
        {
            return this.CurrentChartArea.AxisX.Maximum;
        }

        public double GetAxisYMin()
        {
            return this.CurrentChartArea.AxisY.Minimum;
        }

        public double GetAxisYMax()
        {
            return this.CurrentChartArea.AxisY.Maximum;
        }

        private void ToolStripMenuItem_Axis(object sender, EventArgs e)
        {
            WindowForXYMinMax.numericUpDownXMin.Value = (decimal)this.CurrentChartArea.AxisX.Minimum;
            WindowForXYMinMax.numericUpDownXMax.Value = (decimal)this.CurrentChartArea.AxisX.Maximum;

            WindowForXYMinMax.numericUpDownYMin.Value = (decimal)this.CurrentChartArea.AxisY.Minimum;
            WindowForXYMinMax.numericUpDownYMax.Value = (decimal)this.CurrentChartArea.AxisY.Maximum;

            WindowForXYMinMax.numericUpDownXInterval.Value = (decimal)this.CurrentChartArea.AxisX.Interval;
            WindowForXYMinMax.numericUpDownYInterval.Value = (decimal)this.CurrentChartArea.AxisY.Interval;

            if (WindowForXYMinMax.ShowDialog() == DialogResult.OK)
            {
                this.CurrentChartArea.AxisX.Minimum = (double)WindowForXYMinMax.numericUpDownXMin.Value;
                this.CurrentChartArea.AxisX.Maximum = (double)WindowForXYMinMax.numericUpDownXMax.Value;

                this.CurrentChartArea.AxisY.Minimum = (double)WindowForXYMinMax.numericUpDownYMin.Value;
                this.CurrentChartArea.AxisY.Maximum = (double)WindowForXYMinMax.numericUpDownYMax.Value;

                if (WindowForXYMinMax.checkBoxXAutomated.Checked)
                    this.CurrentChartArea.AxisX.Minimum = this.CurrentChartArea.AxisX.Maximum = double.NaN;

                if (WindowForXYMinMax.checkBoxYAutomated.Checked)
                    this.CurrentChartArea.AxisY.Minimum = this.CurrentChartArea.AxisY.Maximum = double.NaN;


                this.CurrentChartArea.AxisX.Interval = (double)WindowForXYMinMax.numericUpDownXInterval.Value;
                this.CurrentChartArea.AxisY.Interval = (double)WindowForXYMinMax.numericUpDownYInterval.Value;

                if (WindowForXYMinMax.checkBoxXintervalAutomated.Checked)
                    this.CurrentChartArea.AxisX.Interval = double.NaN;

                if (WindowForXYMinMax.checkBoxYintervalAutomated.Checked)
                    this.CurrentChartArea.AxisY.Interval = double.NaN;
            }
        }

        private void ToolStripMenuItem_CopyToClipBoard(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            this.SaveImage(ms, ChartImageFormat.Bmp);
            Bitmap bm = new Bitmap(ms);
            Clipboard.SetImage(bm);
        }

        private void ToolStripMenuItem_Detach(object sender, EventArgs e)
        {
            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.Title = this.CurrentTitle + " (Detached)";
            cExtendedControl EC = new cExtendedControl();
            EC.Controls.Add(this);
            DTW.SetInputData(EC);
            DTW.Run();
            DTW.Display();
        }

        private void ToolStripMenuItem_DisplayDataTable(object sender, EventArgs e)
        {
            cDisplayExtendedTable CDE = new cDisplayExtendedTable();
            CDE.SetInputData(this.InputSimpleData);
            CDE.Run();
        }

        private void ToolStripMenuItem_XAxis(object sender, EventArgs e)
        {
            this.IsXGrid = !this.IsXGrid;
            this.CurrentChartArea.Axes[1].MajorGrid.Enabled = IsXGrid;
        }

        private void ToolStripMenuItem_YAxis(object sender, EventArgs e)
        {
            this.IsYGrid = !this.IsYGrid;
            this.CurrentChartArea.Axes[0].MajorGrid.Enabled = IsYGrid;
        }

        private void ToolStripMenuItem_IsShadow(object sender, EventArgs e)
        {
            this.IsShadow = !this.IsShadow;
            if (IsShadow)
                foreach (var item in this.Series)
                    item.ShadowOffset = 1;
            else
                foreach (var item in this.Series)
                    item.ShadowOffset = 0;
        }

        private void ToolStripMenuItem_IsLegend(object sender, EventArgs e)
        {
            this.IsLegend = !this.IsLegend;
            this.Legends.Clear();
            if (this.IsLegend)
            {
                Legend MyLegend = new Legend();
                this.Legends.Add(MyLegend);
                this.Legends[0].ShadowOffset = 5;
                this.CustomizeLegend += new EventHandler<CustomizeLegendEventArgs>(Chart_CustomizeLegend);
            }
        }

        protected void ToolStripMenuItem_IsDisplayValues(object sender, EventArgs e)
        {
            this.IsDisplayValues = !this.IsDisplayValues;
            foreach (var item in this.Series)
                foreach (var Pt in item.Points)
                {
                    Pt.LabelFormat = "N2";
                    Pt.IsValueShownAsLabel = this.IsDisplayValues;
                }
        }

        private void ToolStripMenuItem_IsBorder(object sender, EventArgs e)
        {
            this.IsBorder = !this.IsBorder;
            if (IsBorder)
                foreach (var item in this.Series)
                    foreach (var Pt in item.Points)
                    {
                        Pt.BorderColor = Color.Black;
                        Pt.BorderWidth = 1;
                    }
            else
                foreach (var item in this.Series)
                    foreach (var Pt in item.Points)
                    {
                        Pt.BorderWidth = 0;
                    }

            this.Update();
        }

        private void ToolStripMenuItem_BackGroundColor(object sender, EventArgs e)
        {
            ColorDialog CD = new ColorDialog();
            if (CD.ShowDialog() != DialogResult.OK) return;
            this.BackgroundColor = CD.Color;
            this.CurrentChartArea.BackColor = this.BackgroundColor;
            this.BackColor = this.BackgroundColor;

            this.Update();
        }
    }


}
