using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using HCSAnalyzer.Classes.Base_Classes.General;
using System.Windows.Forms;
using HCSAnalyzer.Classes.MetaComponents;
using LibPlateAnalysis;
using System.IO;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    class cViewerHeatMap : cDataDisplay
    {
        public cViewerHeatMap()
        {
            base.Title = "Heat Map Viewer";
            CurrentLUT = LUT.LUT_JET;
        }

        public cGlobalInfo GlobalInfo;

        ToolStripMenuItem ToolStripMenuItem_DisplayOptionsDispValues;
        Series CurrentSeries;
        Chart ChartToBeIncluded;
        cExtendedTable Input;
        //cExtendedControl CurrentPanel;
        cLookUpTable LUT = new cLookUpTable();
        public byte[][] CurrentLUT;
        public bool IsWellClassLegend = false;
        public bool IsCellularPhenotypeLegend = false;



        #region public Parameters
        public int DigitNumber = 2;
        public int SquareSize = 35;
        public bool IsDisplayValues = false;

        public double Max = 1;
        public double Min = 0;
        public bool IsAutomatedMinMax = true;

        public MarkerStyle Marker_Style = MarkerStyle.Square;
        #endregion

        public void SetInputData(cExtendedTable InputTable)
        {
            base.Title += ": " + InputTable.Name;
            this.Input = InputTable;
        }


        public cFeedBackMessage Run()
        {

            ChartToBeIncluded = GenerateGraph();
            ChartToBeIncluded.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart_MouseDown);

            ChartToBeIncluded.Width = base.CurrentPanel.Width;
            ChartToBeIncluded.Height = base.CurrentPanel.Height;

            base.CurrentPanel.Title = this.Title;
            base.CurrentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                        | System.Windows.Forms.AnchorStyles.Left)
                                        | System.Windows.Forms.AnchorStyles.Right)));

            CurrentPanel.Controls.Add(ChartToBeIncluded);
            //  CurrentPanel.Height = ChartToBeIncluded.Height;
            //  CurrentPanel.Width = ChartToBeIncluded.Width;

            return base.FeedBackMessage;
        }


        private void chart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                CompleteMenu = new ContextMenuStrip();

                ToolStripMenuItem ToolStripMenuItem_DisplayTable = new ToolStripMenuItem("Display Table");
                CompleteMenu.Items.Add(ToolStripMenuItem_DisplayTable);
                ToolStripMenuItem_DisplayTable.Click += new System.EventHandler(this.DisplayTable);

                ToolStripMenuItem ToolStripMenuItem_DisplayElevationMap = new ToolStripMenuItem("Display Elevation Map");
                CompleteMenu.Items.Add(ToolStripMenuItem_DisplayElevationMap);
                ToolStripMenuItem_DisplayElevationMap.Click += new System.EventHandler(this.DisplayElevationMap);


                #region display options

                ToolStripMenuItem ToolStripMenuItem_DisplayOptions = new ToolStripMenuItem("Display options");
                CompleteMenu.Items.Add(ToolStripMenuItem_DisplayOptions);

                this.ToolStripMenuItem_DisplayOptionsDispValues = new ToolStripMenuItem("Display values");
                ToolStripMenuItem_DisplayOptionsDispValues.CheckOnClick = true;
                ToolStripMenuItem_DisplayOptionsDispValues.Checked = this.IsDisplayValues;
                ToolStripMenuItem_DisplayOptions.DropDownItems.Add(ToolStripMenuItem_DisplayOptionsDispValues);
                ToolStripMenuItem_DisplayOptionsDispValues.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayOptionsDisValues);

                #endregion

                HitTestResult Res = this.ChartToBeIncluded.HitTest(e.X, e.Y, ChartElementType.DataPoint);

                if (Res.Series != null)
                {
                    DataPoint PtToTransfer = Res.Series.Points[Res.PointIndex];

                    if ((PtToTransfer.Tag != null) && (PtToTransfer.Tag.GetType() == typeof(cWell)))
                    {
                        cWell TmpWell = (cWell)(PtToTransfer.Tag);
                        foreach (var item in TmpWell.GetExtendedContextMenu())
                            CompleteMenu.Items.Add(item);
                    }
                }
                CompleteMenu.Show(Control.MousePosition);
            }
        }

        private void DisplayTable(object sender, EventArgs e)
        {
            cDisplayExtendedTable CDET = new cDisplayExtendedTable();
            CDET.SetInputData(this.Input);
            CDET.Title = this.Title;
            CDET.Run();
        }

        private void ToolStripMenuItem_DisplayOptionsDisValues(object sender, EventArgs e)
        {
            this.IsDisplayValues = ToolStripMenuItem_DisplayOptionsDispValues.Checked;

            foreach (var item in this.ChartToBeIncluded.Series)
                foreach (var Pt in item.Points)
                {
                    Pt.LabelFormat = "N" + DigitNumber;
                    Pt.IsValueShownAsLabel = this.IsDisplayValues;
                }
        }

        private void DisplayElevationMap(object sender, EventArgs e)
        {
            cViewerElevationMap3D VE = new cViewerElevationMap3D();
            VE.SetInputData(new cListExtendedTable(this.Input));
            if (VE.Run().IsSucceed == false) return;

            cDesignerSinglePanel CD = new cDesignerSinglePanel();
            CD.SetInputData(VE.GetOutPut());
            if (CD.Run().IsSucceed == false) return;

            cDisplayToWindow CDW = new cDisplayToWindow();
            CDW.SetInputData(CD.GetOutPut());
            CDW.Title = this.Title + ": Elevation Map";
            if (CDW.Run().IsSucceed == false) return;
            CDW.Display();
        }

        private Chart GenerateGraph()
        {
            Chart ChartToBeReturned = new Chart();
            int IdxValue = 0;

            if (IsAutomatedMinMax)
            {
                Max = this.Input.Max();
                Min = this.Input.Min();
            }

            CurrentSeries = new Series();
            CurrentSeries.ChartType = SeriesChartType.Point;
            // loop on all the desciptors
            for (int idxCol = 0; idxCol < Input.Count; idxCol++)
            {
                for (int idxRow = 0; idxRow < Input[idxCol].Count; idxRow++)
                {
                    CurrentSeries.Points.AddXY(idxCol + 1, idxRow + 1);
                    CurrentSeries.Points[IdxValue].MarkerStyle = Marker_Style;
                    CurrentSeries.Points[IdxValue].MarkerSize = SquareSize;
                    CurrentSeries.Points[IdxValue].BorderColor = Color.Black;
                    CurrentSeries.Points[IdxValue].BorderWidth = 1;
                    if (Input[idxCol].ListTags != null)
                        CurrentSeries.Points[IdxValue].Tag = Input[idxCol].ListTags[idxRow];

                    double Value = this.Input[idxCol][idxRow];

                    if (this.IsDisplayValues)
                        CurrentSeries.Points[IdxValue].Label = Value.ToString("N" + DigitNumber);// string.Format("{0:0.###}", Math.Abs(Value));
                    else
                        CurrentSeries.Points[IdxValue].Label = "";

                    string TmpToolTip = this.Input[idxCol].Name ;

                    if (idxRow < this.Input.ListRowNames.Count)
                    {
                        TmpToolTip +=  "\n vs.\n" + this.Input.ListRowNames[idxRow] + "\n\n" + Value.ToString("N" + DigitNumber);
                    }

                    CurrentSeries.Points[IdxValue].ToolTip = TmpToolTip;
                    if ((Max != Min) && (double.IsNaN(Value) == false) && (Value != -1))
                    {
                        int ConvertedValue = (int)((Value - Min) / (Max - Min) * (CurrentLUT[0].Length - 1));
                        CurrentSeries.Points[IdxValue].Color = Color.FromArgb(CurrentLUT[0][ConvertedValue], CurrentLUT[1][ConvertedValue], CurrentLUT[2][ConvertedValue]);

                        if (this.IsDisplayValues)
                        {
                            CurrentSeries.Points[IdxValue].LabelFormat = "N" + DigitNumber;
                            CurrentSeries.Points[IdxValue].IsValueShownAsLabel = true;
                            CurrentSeries.Points[IdxValue].Font = new Font("Arial", 8);
                        }
                    }
                    else
                        CurrentSeries.Points[IdxValue].Color = Color.Transparent;

                    CurrentSeries.Points[IdxValue++].AxisLabel = this.Input[idxCol].Name;
                }
            }


            SmartLabelStyle SStyle = new SmartLabelStyle();

            ChartArea CurrentChartArea = new ChartArea("Default");
            for (int i = 0; i < this.Input.ListRowNames.Count; i++)
            {
                CustomLabel lblY = new CustomLabel();
                lblY.ToPosition = i * 2 + 2;
                lblY.Text = this.Input.ListRowNames[i];
                CurrentChartArea.AxisY.CustomLabels.Add(lblY);
            }

            CurrentChartArea.AxisY.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep30;
            CurrentChartArea.BorderColor = Color.Black;
            ChartToBeReturned.ChartAreas.Add(CurrentChartArea);
            CurrentSeries.SmartLabelStyle.Enabled = true;

            ChartToBeReturned.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            ChartToBeReturned.Series.Add(CurrentSeries);

            CurrentChartArea.Axes[0].MajorGrid.Enabled = false;
            CurrentChartArea.Axes[0].Minimum = 0;
            CurrentChartArea.Axes[0].Maximum = this.Input.Count + 1;
            CurrentChartArea.Axes[1].MajorGrid.Enabled = false;
            CurrentChartArea.Axes[1].Minimum = 0;
            CurrentChartArea.Axes[1].Maximum = this.Input.ListRowNames.Count + 1;
            CurrentChartArea.AxisX.Interval = 1;
            CurrentChartArea.AxisY.Interval = 1;

            Title CurrentTitle = new Title(this.Input.Name);
            ChartToBeReturned.Titles.Add(CurrentTitle);
            ChartToBeReturned.Titles[0].Font = new Font("Arial", 9);

            ChartToBeReturned.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                       | System.Windows.Forms.AnchorStyles.Left
                                       | System.Windows.Forms.AnchorStyles.Right);

            if ((IsWellClassLegend) || (IsCellularPhenotypeLegend))
            {
                Legend MyLegend = new Legend();
                ChartToBeReturned.Legends.Add(MyLegend);
                ChartToBeReturned.Legends[0].ShadowOffset = 5;
                ChartToBeReturned.CustomizeLegend += new EventHandler<CustomizeLegendEventArgs>(ChartToBeReturned_CustomizeLegend);
            }
            return ChartToBeReturned;
        }

        void ChartToBeReturned_CustomizeLegend(object sender, CustomizeLegendEventArgs e)
        {


            e.LegendItems.Clear();

            if (IsWellClassLegend)
            {
                foreach (var item in cGlobalInfo.ListWellClasses)
                {
                    LegendItem newItem = new LegendItem();
                    newItem.ImageStyle = LegendImageStyle.Marker;
                    newItem.MarkerStyle = MarkerStyle.Square;
                    newItem.MarkerSize = 8;
                    newItem.MarkerBorderColor = newItem.MarkerColor = item.ColourForDisplay;
                    newItem.ShadowColor = Color.Black;
                    newItem.ShadowOffset = 1;
                    newItem.Cells.Add(LegendCellType.SeriesSymbol, "", ContentAlignment.MiddleLeft);
                    newItem.Cells.Add(LegendCellType.Text, item.Name, ContentAlignment.MiddleLeft);
                    e.LegendItems.Add(newItem);
                }
            }
            if (IsCellularPhenotypeLegend)
            {
                foreach (var item in cGlobalInfo.ListCellularPhenotypes)
                {
                    LegendItem newItem = new LegendItem();
                    newItem.ImageStyle = LegendImageStyle.Marker;
                    newItem.MarkerStyle = MarkerStyle.Square;
                    newItem.MarkerSize = 8;
                    newItem.MarkerBorderColor = newItem.MarkerColor = item.ColourForDisplay;
                    newItem.ShadowColor = Color.Black;
                    newItem.ShadowOffset = 1;
                    newItem.Cells.Add(LegendCellType.SeriesSymbol, "", ContentAlignment.MiddleLeft);
                    newItem.Cells.Add(LegendCellType.Text, item.Name, ContentAlignment.MiddleLeft);
                    e.LegendItems.Add(newItem);
                }


            }

            //e.LegendItems.Add(

        }
    }
}
