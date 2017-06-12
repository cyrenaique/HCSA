using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using System.Windows.Forms;
using System.Data;
using LibPlateAnalysis;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes.DataManip;
using HCSAnalyzer.Classes.Base_Classes.Viewers._2D;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes._3D;
using HCSAnalyzer.Classes.Base_Classes.Viewers._3D.ComplexObjects;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    public class cViewerTable : cDataDisplay
    {
        cExtendedTable Input = null;
        DataGridView GridView = new DataGridView();
        ContextMenuStrip ColumnContextMenu;

        public int DigitNumber = 2;
       
        public cExtendedTable GetLiveListValues()
        {
            cExtendedTable CET = new cExtendedTable();
            CET.Name = Input.Name;
            //foreach (DataGridViewColumn item in GridView.SelectedColumns)

            for (int ColumnSelected = 0; ColumnSelected < GridView.SelectedColumns.Count; ColumnSelected++)
            {
                CET.Add(new cExtendedList());
                for (int i = 0; i < GridView.Rows.Count - 1; i++)
                {
                    CET[ColumnSelected].Add(double.Parse(GridView[GridView.SelectedColumns[ColumnSelected].Index, i].Value.ToString()));

                }
                //GridView[item.Index,
                //CET.Add();
            }
            //GridView.SelectedColumns
            return CET;
        }

        public cViewerTable()
        {
            Title = "Table Viewer";
          //  this.GridView.ShowCellToolTips = false;

            this.GridView.CellMouseClick += new DataGridViewCellMouseEventHandler(GridView_CellMouseClick);
            //this.GridView.CellMouseEnter += new DataGridViewCellEventHandler(GridView_CellMouseEnter);
        }

        void GridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //ToolTip tt = new ToolTip();
            

            //if (e.RowIndex == -1 && e.ColumnIndex != -1)
            //{
            //    tt.SetToolTip(this.GridView, this.GridView.Columns[e.ColumnIndex].HeaderCell.FormattedValue.ToString());
            //}
            //else
            //{
            //    tt.Hide(this.GridView);
            //}

            //tt.Dispose();
        }

        private void GridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Clicks != 1) return;
            if (e.Button != MouseButtons.Right) return;

            ContextMenuStrip GridViewContextMenu = new ContextMenuStrip();

            #region Context Menu

            #region Display as
            ToolStripMenuItem ToolStripMenuItem_DisplayAS = new ToolStripMenuItem("Display as...");

            ToolStripMenuItem ToolStripMenuItem_DisplayHeatMap = new ToolStripMenuItem("Heat Map");
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_DisplayHeatMap);
            ToolStripMenuItem_DisplayHeatMap.Click += new System.EventHandler(this.DisplayHeatMap);

            ToolStripMenuItem ToolStripMenuItem_DisplayElevationMap = new ToolStripMenuItem("Elevation Map");
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_DisplayElevationMap);
            ToolStripMenuItem_DisplayElevationMap.Click += new System.EventHandler(this.DisplayElevationMap);


            if (GridView.SelectedColumns.Count >= 2)
            {
                ToolStripMenuItem ToolStripMenuItem_Display2DScatterGraph = new ToolStripMenuItem("2D Scatter Graph");
                ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_Display2DScatterGraph);
                ToolStripMenuItem_Display2DScatterGraph.Click += new System.EventHandler(this.ToolStripMenuItem_Display2DScatterGraph);
            }

            if(GridView.SelectedColumns.Count == 3)
            {
                ToolStripMenuItem ToolStripMenuItem_Display3DScatterGraph = new ToolStripMenuItem("3D Scatter Graph");
                ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_Display3DScatterGraph);
                ToolStripMenuItem_Display3DScatterGraph.Click += new System.EventHandler(this.ToolStripMenuItem_Display3DScatterGraph);
            }

            ToolStripMenuItem ToolStripMenuItem_DisplayAsImage = new ToolStripMenuItem("Image");
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_DisplayAsImage);
            ToolStripMenuItem_DisplayAsImage.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayAsImage);
            #endregion

            ToolStripMenuItem_DisplayAS.DropDownItems.Add(new ToolStripSeparator());

            #region save as

            ToolStripMenuItem ToolStripExportAs = new ToolStripMenuItem("Export as...");
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripExportAs);


            ToolStripMenuItem ToolStripMenuItem_SaveAsCSV = new ToolStripMenuItem("CSV file");
            ToolStripExportAs.DropDownItems.Add(ToolStripMenuItem_SaveAsCSV);
            ToolStripMenuItem_SaveAsCSV.Click += new System.EventHandler(this.ToolStripMenuItem_SaveAsCSV);

            ToolStripMenuItem ToolStripMenuItem_SaveAsARFF = new ToolStripMenuItem("ARFF file");
            ToolStripExportAs.DropDownItems.Add(ToolStripMenuItem_SaveAsARFF);
            ToolStripMenuItem_SaveAsARFF.Click += new System.EventHandler(this.ToolStripMenuItem_SaveAsARFF);


            ToolStripMenuItem ToolStripMenuItem_SaveAsHTML = new ToolStripMenuItem("HTML file");
            ToolStripExportAs.DropDownItems.Add(ToolStripMenuItem_SaveAsHTML);
            ToolStripMenuItem_SaveAsHTML.Click += new System.EventHandler(this.ToolStripMenuItem_SaveAsHTML);

            ToolStripMenuItem ToolStripMenuItem_ToClipBoard = new ToolStripMenuItem("Copy to ClipBoard");
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_ToClipBoard);
            ToolStripMenuItem_ToClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_ToClipBoard);
            #endregion

            GridViewContextMenu.Items.Add(ToolStripMenuItem_DisplayAS);

            #region Operations
            ToolStripMenuItem ToolStripMenuItem_Operations = new ToolStripMenuItem("Operations");
            GridViewContextMenu.Items.Add(ToolStripMenuItem_Operations);

            ToolStripMenuItem ToolStripMenuItem_OperationsAbs = new ToolStripMenuItem("Abs.");
            ToolStripMenuItem_Operations.DropDownItems.Add(ToolStripMenuItem_OperationsAbs);
            ToolStripMenuItem_OperationsAbs.Click += new System.EventHandler(this.ToolStripMenuItem_OperationsAbs);

            ToolStripMenuItem ToolStripMenuItem_OperationsSquare = new ToolStripMenuItem("Square");
            ToolStripMenuItem_Operations.DropDownItems.Add(ToolStripMenuItem_OperationsSquare);
            ToolStripMenuItem_OperationsSquare.Click += new System.EventHandler(this.ToolStripMenuItem_OperationsSquare);

            ToolStripMenuItem ToolStripMenuItem_OperationsStatistics = new ToolStripMenuItem("Statistics (Full Table)");
            ToolStripMenuItem_Operations.DropDownItems.Add(ToolStripMenuItem_OperationsStatistics);
            ToolStripMenuItem_OperationsStatistics.Click += new System.EventHandler(this.ToolStripMenuItem_OperationsStatistics);


            ToolStripMenuItem ToolStripMenuItem_Statistics = new ToolStripMenuItem("Statistics (Col. by Col.)");
            ToolStripMenuItem_Operations.DropDownItems.Add(ToolStripMenuItem_Statistics);
            ToolStripMenuItem_Statistics.Click += new System.EventHandler(this.ToolStripMenuItem_Statistics);

            ToolStripMenuItem ToolStripMenuItem_TestNormality = new ToolStripMenuItem("Normality Test");
            ToolStripMenuItem_Operations.DropDownItems.Add(ToolStripMenuItem_TestNormality);
            ToolStripMenuItem_TestNormality.Click += new System.EventHandler(this.ToolStripMenuItem_NormalityTest);

            ToolStripMenuItem_Operations.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_OperationsClustering = new ToolStripMenuItem("Clustering");
            ToolStripMenuItem_Operations.DropDownItems.Add(ToolStripMenuItem_OperationsClustering);
            ToolStripMenuItem_OperationsClustering.Click += new System.EventHandler(this.ToolStripMenuItem_OperationsClustering);

            ToolStripMenuItem_Operations.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_OperationsCropSelection = new ToolStripMenuItem("Crop Selection");
            ToolStripMenuItem_Operations.DropDownItems.Add(ToolStripMenuItem_OperationsCropSelection);
            ToolStripMenuItem_OperationsCropSelection.Click += new System.EventHandler(this.ToolStripMenuItem_OperationsCropSelection);

            ToolStripMenuItem ToolStripMenuItem_OperationsTranspose = new ToolStripMenuItem("Transpose");
            ToolStripMenuItem_Operations.DropDownItems.Add(ToolStripMenuItem_OperationsTranspose);
            ToolStripMenuItem_OperationsTranspose.Click += new System.EventHandler(this.ToolStripMenuItem_OperationsTranspose);


            if (this.Input.Count == this.Input[0].Count)
            {
                ToolStripMenuItem ToolStripMenuItem_OperationsInverse = new ToolStripMenuItem("Inverse");
                ToolStripMenuItem_Operations.DropDownItems.Add(ToolStripMenuItem_OperationsInverse);
                ToolStripMenuItem_OperationsInverse.Click += new System.EventHandler(this.ToolStripMenuItem_OperationsInverse);
            }

            if (GridView.SelectedColumns.Count >= 1)
            {
                ToolStripMenuItem ToolStripMenuItem_ColumnNormalization = new ToolStripMenuItem("Normalization");

                ToolStripMenuItem ToolStripMenuItem_NormMinMax = new ToolStripMenuItem("Min-Max");
                ToolStripMenuItem_ColumnNormalization.DropDownItems.Add(ToolStripMenuItem_NormMinMax);
                ToolStripMenuItem_NormMinMax.Click += new System.EventHandler(this.ToolStripMenuItem_NormMinMax);

                ToolStripMenuItem ToolStripMenuItem_NormStandardize = new ToolStripMenuItem("Standardize");
                ToolStripMenuItem_ColumnNormalization.DropDownItems.Add(ToolStripMenuItem_NormStandardize);
                ToolStripMenuItem_NormStandardize.Click += new System.EventHandler(this.ToolStripMenuItem_NormStandardize);

                GridViewContextMenu.Items.Add(ToolStripMenuItem_ColumnNormalization);

                DataGridViewColumn Column = GridView.SelectedColumns[0];

                ToolStripMenuItem ColumnContextMenu = new ToolStripMenuItem("Column [" + Column.Name+"]");

                ToolStripMenuItem ToolStripMenuItem_DisplayGraph = new ToolStripMenuItem("Display Graph");
                ColumnContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayGraph);
                ToolStripMenuItem_DisplayGraph.Click += new System.EventHandler(this.DisplayGraph);

                ToolStripMenuItem ToolStripMenuItem_DisplayHisto = new ToolStripMenuItem("Display Histogram");
                ColumnContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayHisto);
                ToolStripMenuItem_DisplayHisto.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayHisto);


                ToolStripMenuItem ToolStripMenuItem_DataManipulation = new ToolStripMenuItem("Data Manipulations");

                ToolStripMenuItem ToolStripMenuItem_AscendingSorting = new ToolStripMenuItem("Ascending Sorting");
                ToolStripMenuItem_DataManipulation.DropDownItems.Add(ToolStripMenuItem_AscendingSorting);
                ToolStripMenuItem_AscendingSorting.Click += new System.EventHandler(this.ToolStripMenuItem_AscendingSorting);

                ToolStripMenuItem ToolStripMenuItem_DescendingSorting = new ToolStripMenuItem("Descending Sorting");
                ToolStripMenuItem_DataManipulation.DropDownItems.Add(ToolStripMenuItem_DescendingSorting);
                ToolStripMenuItem_DescendingSorting.Click += new System.EventHandler(this.ToolStripMenuItem_DescendingSorting);

                ColumnContextMenu.DropDownItems.Add(ToolStripMenuItem_DataManipulation);

                if ((this.Input[Column.Index].Tag != null) && (this.Input[Column.Index].Tag.GetType() == typeof(cDescriptorType)))
                {
                    cDescriptorType TmpDescType = (cDescriptorType)this.Input[Column.Index].Tag;

                    List<ToolStripMenuItem> MenuItemForDesc = TmpDescType.GetExtendedContextMenu();
                    if (MenuItemForDesc != null)
                        foreach (var item in MenuItemForDesc) ColumnContextMenu.DropDownItems.Add(item);
                }

                if ((this.Input[Column.Index].Tag != null) && (this.Input[Column.Index].Tag.GetType() == typeof(cDescriptorsLinearCombination)))
                {
                    cDescriptorsLinearCombination TmpDescType = (cDescriptorsLinearCombination)this.Input[Column.Index].Tag;

                    List<ToolStripMenuItem> MenuItemForDesc = TmpDescType.GetContextMenu();
                    if (MenuItemForDesc != null)
                        foreach (var item in MenuItemForDesc) ColumnContextMenu.DropDownItems.Add(item);
                }

                ColumnContextMenu.DropDownItems.Add(new ToolStripSeparator());
                ToolStripMenuItem ToolStripMenuItem_DisplayInfo = new ToolStripMenuItem("Info");
                ColumnContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayInfo);
                ToolStripMenuItem_DisplayInfo.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayInfo);


                GridViewContextMenu.Items.Add(ColumnContextMenu);

                if (GridView.SelectedColumns.Count >= 2)
                {
                    ToolStripMenuItem MultiColumnContextMenu = new ToolStripMenuItem("Cross Columns Operations");

                    ToolStripMenuItem ToolStripMenuItem_SimilarityMeasures = new ToolStripMenuItem("Similarity Measures");

                    ToolStripMenuItem ToolStripMenuItem_ZFactor = new ToolStripMenuItem("Z-Factor");
                    ToolStripMenuItem_SimilarityMeasures.DropDownItems.Add(ToolStripMenuItem_ZFactor);
                    ToolStripMenuItem_ZFactor.Click += new System.EventHandler(this.ToolStripMenuItem_ZFactor);

                    ToolStripMenuItem ToolStripMenuItem_DotProduct = new ToolStripMenuItem("Normalized Dot Product");
                    ToolStripMenuItem_SimilarityMeasures.DropDownItems.Add(ToolStripMenuItem_DotProduct);
                    ToolStripMenuItem_DotProduct.Click += new System.EventHandler(this.ToolStripMenuItem_DotProduct);

                    ToolStripMenuItem ToolStripMenuItem_DistEuclidean = new ToolStripMenuItem("Euclidean");
                    ToolStripMenuItem_SimilarityMeasures.DropDownItems.Add(ToolStripMenuItem_DistEuclidean);
                    ToolStripMenuItem_DistEuclidean.Click += new System.EventHandler(this.ToolStripMenuItem_DistEuclidean);

                    MultiColumnContextMenu.DropDownItems.Add(ToolStripMenuItem_SimilarityMeasures);


                    ToolStripMenuItem ToolStripMenuItem_StatTests = new ToolStripMenuItem("Tests");

                    ToolStripMenuItem ToolStripMenuItem_TestsFTest = new ToolStripMenuItem("Dispersion F-test (2 Tails)");
                    ToolStripMenuItem_StatTests.DropDownItems.Add(ToolStripMenuItem_TestsFTest);
                    ToolStripMenuItem_TestsFTest.Click += new System.EventHandler(this.ToolStripMenuItem_TestsFTest);

                    ToolStripMenuItem ToolStripMenuItem_TestsWilcoxonTest = new ToolStripMenuItem("Median Wilcoxon signed-rank (2 Tails)");
                    ToolStripMenuItem_StatTests.DropDownItems.Add(ToolStripMenuItem_TestsWilcoxonTest);
                    ToolStripMenuItem_TestsWilcoxonTest.Click += new System.EventHandler(this.ToolStripMenuItem_TestsWilcoxonTest);

                    ToolStripMenuItem ToolStripMenuItem_TestsSignTest = new ToolStripMenuItem("Median Sign Test (2 Tails)");
                    ToolStripMenuItem_StatTests.DropDownItems.Add(ToolStripMenuItem_TestsSignTest);
                    ToolStripMenuItem_TestsSignTest.Click += new System.EventHandler(this.ToolStripMenuItem_TestsSignTest);

                    ToolStripMenuItem ToolStripMenuItem_Tests2SamplesTTest = new ToolStripMenuItem("Means Two-sample Unpooled T-test (2 Tails)");
                    ToolStripMenuItem_StatTests.DropDownItems.Add(ToolStripMenuItem_Tests2SamplesTTest);
                    ToolStripMenuItem_Tests2SamplesTTest.Click += new System.EventHandler(this.ToolStripMenuItem_Tests2SamplesTTest);

                    ToolStripMenuItem ToolStripMenuItem_TestsOneWayANOVA = new ToolStripMenuItem("One way ANOVA");
                    ToolStripMenuItem_StatTests.DropDownItems.Add(ToolStripMenuItem_TestsOneWayANOVA);
                    ToolStripMenuItem_TestsOneWayANOVA.Click += new System.EventHandler(this.ToolStripMenuItem_TestsOneWayANOVA);


                    MultiColumnContextMenu.DropDownItems.Add(ToolStripMenuItem_StatTests);

                    ToolStripMenuItem ToolStripMenuItem_CorrelationAnalysis = new ToolStripMenuItem("Correlation Analysis");

                    ToolStripMenuItem ToolStripMenuItem_CorrelationMatrix = new ToolStripMenuItem("Correlation Matrix");
                    ToolStripMenuItem_CorrelationAnalysis.DropDownItems.Add(ToolStripMenuItem_CorrelationMatrix);
                    ToolStripMenuItem_CorrelationMatrix.Click += new System.EventHandler(this.ToolStripMenuItem_CorrelationMatrix);

                    ToolStripMenuItem ToolStripMenuItem_MINEAnalysis = new ToolStripMenuItem("MINE Analysis");
                    ToolStripMenuItem_CorrelationAnalysis.DropDownItems.Add(ToolStripMenuItem_MINEAnalysis);
                    ToolStripMenuItem_MINEAnalysis.Click += new System.EventHandler(this.ToolStripMenuItem_MINEAnalysis);

                    MultiColumnContextMenu.DropDownItems.Add(ToolStripMenuItem_CorrelationAnalysis);

                  

                    GridViewContextMenu.Items.Add(MultiColumnContextMenu);
                }
            }
            #endregion

            // specific menu for the clicked cell
            if (/*(e.ColumnIndex > -1)&&*/(e.RowIndex > -1)&&(Input.ListTags != null))
            {
                if (Input.ListTags[e.RowIndex].GetType() == typeof(cWellClassType))
                {
                    cWellClassType TmpClass = ((cWellClassType)Input.ListTags[e.RowIndex]);
                    GridViewContextMenu.Items.Add(TmpClass.GetExtendedContextMenu());
                }
                else if (Input.ListTags[e.RowIndex].GetType() == typeof(cWell))
                {
                    cWell TmpClass = ((cWell)Input.ListTags[e.RowIndex]);

                    foreach (var item in TmpClass.GetExtendedContextMenu())
                        GridViewContextMenu.Items.Add(item);
                }
                else if (Input.ListTags[e.RowIndex].GetType() == typeof(cListWells))
                {
                    cListWells TmpClass = ((cListWells)Input.ListTags[e.RowIndex]);

                    //foreach (var item in TmpClass.GetContextMenu())
                        GridViewContextMenu.Items.Add(TmpClass.GetExtendedContextMenu());
                }

            }
            #endregion

            GridViewContextMenu.DropShadowEnabled = true;
            GridViewContextMenu.Show(Control.MousePosition);
        }

        public void SetInputData(cExtendedTable MyData)
        {
            this.Input = MyData;
        }

        public cFeedBackMessage Run()
        {
            GridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            GridView.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;

            GridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;


            foreach (var Column in Input)
            {
                DataGridViewColumn DC = new DataGridViewColumn(new DataGridViewTextBoxCell());
                DC.CellTemplate = new DataGridViewTextBoxCell();

                if (this.DigitNumber < 0)
                    DC.DefaultCellStyle.Format = "G";
                else
                    DC.DefaultCellStyle.Format = "N" + this.DigitNumber;

                DC.HeaderText = Column.Name;
                DC.SortMode = DataGridViewColumnSortMode.NotSortable;
                DC.Name = Column.Name;
                //DC.Tag

                GridView.Columns.Add(DC);
                GridView.Columns[GridView.Columns.Count - 1].SortMode = DataGridViewColumnSortMode.NotSortable;

                this.Title = this.Input.Name;
            }

            if (Input.Count < 1)
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message = "Input Table is empty";
                return base.FeedBackMessage;
            }

            for (int IdxRow = 0; IdxRow < Input[0].Count; IdxRow++)
                GridView.Rows.Add();

            if ((Input.ListRowNames!=null)&&(Input.ListRowNames.Count > 0))
            {
                for (int IdxRow = 0; IdxRow < Input.ListRowNames.Count; IdxRow++)
                    GridView.Rows[IdxRow].HeaderCell.Value = Input.ListRowNames[IdxRow];
            }
            else if ((GridView.Columns.Count == 1) && (Input[0].ListTags != null))
            {
                for (int IdxRow = 0; IdxRow < Input[0].ListTags.Count; IdxRow++)
                    GridView.Rows[IdxRow].HeaderCell.Value = ((cGeneralComponent)(Input[0].ListTags[IdxRow])).GetShortInfo();
            }

            for (int IdxRow = 0; IdxRow < Input[0].Count; IdxRow++)
            {
                for (int Col = 0; Col < Input.Count; Col++)
                {
                    if ((Input[Col].ListTags != null) && (Input[Col].ListTags.Count > IdxRow) && (Input.ListTags != null) && (Input.ListTags[IdxRow].GetType() == typeof(cWellClassType)))
                    {
                        //GridView[Col, IdxRow].Format = "N" + this.DigitNumber;
                        GridView[Col, IdxRow].Value = Input[Col][IdxRow].ToString();
                        GridView[Col, IdxRow].Style.BackColor = ((cWellClassType)(Input[Col].ListTags[IdxRow])).ColourForDisplay;

                        //     if(Sender!=null)
                        //        GridView[Col, IdxRow].ToolTipText = this.Sender.GetInfo();
                    }
                    else
                    {
                        if(Input[Col].Count>IdxRow)
                        GridView[Col, IdxRow].Value = Input[Col][IdxRow];
                    }
                }
            }
            //CurrentPanel = new cExtendedControl();           

            GridView.TopLeftHeaderCell.Value =this.Title;
            //GridView.row[0].HeaderText = "aaaa";

            CurrentPanel.Title = this.Title;

            this.CurrentPanel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
            | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);

            GridView.Width = CurrentPanel.Width;
            GridView.Height = CurrentPanel.Height;
            this.GridView.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
            | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);

            GridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            GridView.AllowUserToAddRows = false;

            CurrentPanel.Controls.Add(GridView);

            return base.FeedBackMessage;
        }

        #region Column based operations

        private void DisplayGraph(object sender, EventArgs e)
        {
            cViewerGraph1D VG = new cViewerGraph1D();
            VG.Chart.IsLine = true;
            VG.Chart.IsSelectable = false;
            VG.Chart.IsZoomableX = true;
            VG.Chart.IsLegend = true;

            cExtendedTable CET = new cExtendedTable();
            CET.Name = Input.Name;
            CET.ListRowNames = new List<string>();

            foreach (DataGridViewColumn item in GridView.SelectedColumns)
                CET.Add(Input[item.Index]);

            foreach (var item in this.Input.ListRowNames)
                CET.ListRowNames.Add(item);

            if (this.Input.ListTags != null)
            {
                CET.ListTags = new List<object>();
                for (int i = 0; i < GridView.SelectedColumns.Count; i++)
                    CET[i].ListTags = new List<object>();
            }

            if(this.Input.ListTags!=null)
            foreach (var item in this.Input.ListTags)
            {
                CET.ListTags.Add(item);
            }

            //for (int i = 0; i < GridView.SelectedColumns.Count; i++)
            {
                //if (this.Input[i].ListTags != null)
                //{
                //    int NumTags= this.Input[i].ListTags.Count;
                //    for (int Idx = 0; Idx < NumTags; Idx++)
                //    {
                //        CET[i].ListTags.Add(this.Input[i].ListTags[Idx]);
                //    }
                //}
                    //foreach (var item in this.Input[i].ListTags)
                    //CET[i].ListTags.Add(item);
            }



            VG.SetInputData(CET);
            VG.Run();

            cDesignerSinglePanel CD = new cDesignerSinglePanel();
            CD.SetInputData(VG.GetOutPut());
            CD.Run();

            cDisplayToWindow DW = new cDisplayToWindow();
            DW.Title = CET.Name + " - Graph Viewer";
            DW.SetInputData(CD.GetOutPut());
            DW.Run();
            DW.Display();
        }

        private void ToolStripMenuItem_DisplayInfo(object sender, EventArgs e)
        {
            cDisplayText DT = new cDisplayText();
            DT.SetInputData("["+ this.Input[GridView.SelectedColumns[0].Index].Name + "]\n\n"+this.Input[GridView.SelectedColumns[0].Index].GetInfo());
            DT.Run();
        }
        
        private void ToolStripMenuItem_DisplayHisto(object sender, EventArgs e)
        {
            cExtendedTable CET = new cExtendedTable();
            CET.Name = Input.Name;
            foreach (DataGridViewColumn item in GridView.SelectedColumns)
                CET.Add(Input[item.Index]);


            cViewerHistogram VH = new cViewerHistogram();
            VH.SetInputData(CET);
            VH.Title = "Histogram";
            //CV1.Chart.IsBar = true;
            //CV1.Chart.ISPoint = false;
            // CV1.Chart.IsDisplayValues = true;
            VH.Run();

            cDisplayToWindow DW = new cDisplayToWindow();
            DW.Title = CET.Name + " - Histogram Viewer";
            DW.SetInputData(VH.GetOutPut());
            DW.Run();
            DW.Display();
        }

        private void ToolStripMenuItem_CorrelationMatrix(object sender, EventArgs e)
        {
            cExtendedTable CET = new cExtendedTable();
            foreach (DataGridViewColumn item in GridView.SelectedColumns)
                CET.Add(Input[item.Index]);

            cDisplayCorrelationMatrix DCM = new cDisplayCorrelationMatrix();
            DCM.SetInputData(CET);
            DCM.SetCorrelationType(DataAnalysis.eCorrelationType.PEARSON);
            DCM.Title = this.Title + " - Pearson correlation matrix";
            DCM.Run();
        }

        private void ToolStripMenuItem_MINEAnalysis(object sender, EventArgs e)
        {
            cExtendedTable CET = new cExtendedTable();
            foreach (DataGridViewColumn item in GridView.SelectedColumns)
                CET.Add(Input[item.Index]);

            cMineAnalysis MA = new cMineAnalysis();
            MA.SetInputData(CET);
            MA.Is_BriefReport = true;
            MA.Run();

            cDesignerTab SubDT = new cDesignerTab();
            foreach (var item in MA.GetOutPut())
            {
                cViewerTable SubTable = new cViewerTable();

                SubTable.Title = "MINE - " + item.Name;
                SubTable.SetInputData(item);
                SubTable.Run();

                SubDT.SetInputData(SubTable.GetOutPut());
            }
            SubDT.Run();

            cDisplayToWindow DW = new cDisplayToWindow();
            DW.SetInputData(SubDT.GetOutPut());
            DW.Title = CET.Name + " - MINE analysis";
            DW.Run();
            DW.Display();

        }

        private void ToolStripMenuItem_AscendingSorting(object sender, EventArgs e)
        {
            if (GridView.SelectedColumns.Count == 0) return;

            cSort S = new cSort();
            S.SetInputData(this.Input);
            S.ColumnIndexForSorting = GridView.SelectedColumns[0].Index;
            S.Run();

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.SetInputData(S.GetOutPut());
            CDT.Run();

        }

        private void ToolStripMenuItem_DescendingSorting(object sender, EventArgs e)
        {
            if (GridView.SelectedColumns.Count == 0) return;

            cSort S = new cSort();
            S.SetInputData(this.Input);
            S.IsAscending = false;
            S.ColumnIndexForSorting = GridView.SelectedColumns[0].Index;
            S.Run();

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.SetInputData(S.GetOutPut());
            CDT.Run();

        }

        private void ToolStripMenuItem_NormStandardize(object sender, EventArgs e)
        {
            cExtendedTable CET = new cExtendedTable();
            foreach (DataGridViewColumn item in GridView.SelectedColumns)
                CET.Add(Input[item.Index].Normalize(eNormalizationType.STANDARDIZE));

            CET.Name = "Standardize(" + this.Input.Name + ")";
            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.SetInputData(CET);
            CDT.Run();
        }

        private void ToolStripMenuItem_NormMinMax(object sender, EventArgs e)
        {
            cExtendedTable CET = new cExtendedTable();

            foreach (DataGridViewColumn item in GridView.SelectedColumns)
            {
                CET.Add(Input[item.Index].Normalize(eNormalizationType.MIN_MAX));
            }

            CET.Name = "Min-Max(" + this.Input.Name + ")";



            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.SetInputData(CET);

            CDT.Run();
        }

        private void ToolStripMenuItem_DistEuclidean(object sender, EventArgs e)
        {
            cExtendedTable CET = new cExtendedTable();
            foreach (DataGridViewColumn item in GridView.SelectedColumns)
                CET.Add(Input[item.Index]);

            cDistances CD = new cDistances();
            CD.DistanceType = eDistances.EUCLIDEAN;
            CD.SetInputData(CET);
            CD.Run();

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.SetInputData(CD.GetOutPut());
            CDT.Run();
        }

        private void ToolStripMenuItem_ZFactor(object sender, EventArgs e)
        {
            cExtendedTable CET = new cExtendedTable();
            foreach (DataGridViewColumn item in GridView.SelectedColumns)
                CET.Add(Input[item.Index]);

            cZFactor S = new cZFactor();
            S.SetInputData(CET);
            S.Run();

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.SetInputData(S.GetOutPut());
            CDT.Run();
        }

        private void ToolStripMenuItem_TestsWilcoxonTest(object sender, EventArgs e)
        {
            cExtendedTable CET = new cExtendedTable();
            foreach (DataGridViewColumn item in GridView.SelectedColumns)
                CET.Add(Input[item.Index]);

            cWilcoxonTest S = new cWilcoxonTest();
            S.SetInputData(CET);
            S.FTestTails = eFTestTails.BOTH;
            S.Run();

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.DigitNumber = -1;
            CDT.SetInputData(S.GetOutPut());
            CDT.Run();
        }

        private void ToolStripMenuItem_TestsSignTest(object sender, EventArgs e)
        {
            cExtendedTable CET = new cExtendedTable();
            foreach (DataGridViewColumn item in GridView.SelectedColumns)
                CET.Add(Input[item.Index]);

            cSignTest S = new cSignTest();
            S.SetInputData(CET);
            S.FTestTails = eFTestTails.BOTH;
            S.Run();

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.DigitNumber = -1;
            CDT.SetInputData(S.GetOutPut());
            CDT.Run();
        }

        private void ToolStripMenuItem_Tests2SamplesTTest(object sender, EventArgs e)
        {
            cExtendedTable CET = new cExtendedTable();
            foreach (DataGridViewColumn item in GridView.SelectedColumns)
                CET.Add(Input[item.Index]);

            cTwoSampleUnpooledTTest S = new cTwoSampleUnpooledTTest();
            S.SetInputData(CET);
            S.FTestTails = eFTestTails.BOTH;
            S.Run();

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.DigitNumber = -1;
            CDT.SetInputData(S.GetOutPut());
            CDT.Run();
        }

        private void ToolStripMenuItem_TestsOneWayANOVA(object sender, EventArgs e)
        {
            cExtendedTable CET = new cExtendedTable();
            foreach (DataGridViewColumn item in GridView.SelectedColumns)
                CET.Add(Input[item.Index]);

            cANOVA S = new cANOVA();
            S.SetInputData(CET);
            S.Run();

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.DigitNumber = -1;
            CDT.SetInputData(S.GetOutPut());
            CDT.Run();
        }

        private void ToolStripMenuItem_TestsFTest(object sender, EventArgs e)
        {
            cExtendedTable CET = new cExtendedTable();
            foreach (DataGridViewColumn item in GridView.SelectedColumns)
                CET.Add(Input[item.Index]);

            cTwoSampleFTest S = new cTwoSampleFTest();
            S.SetInputData(CET);
            S.FTestTails = eFTestTails.BOTH;
            S.Run();

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.DigitNumber = -1;
            CDT.SetInputData(S.GetOutPut());
            CDT.Run();
        }

        private void ToolStripMenuItem_DotProduct(object sender, EventArgs e)
        {
            cExtendedTable CET = new cExtendedTable();
            foreach (DataGridViewColumn item in GridView.SelectedColumns)
                CET.Add(Input[item.Index]);

            cDotProduct S = new cDotProduct();
            S.SetInputData(CET);
            S.Run();

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.SetInputData(S.GetOutPut());
            CDT.Run();
        }

        private void ToolStripMenuItem_Statistics(object sender, EventArgs e)
        {
            cExtendedTable CET = new cExtendedTable();
            foreach (DataGridViewColumn item in GridView.SelectedColumns)
                CET.Add(Input[item.Index]);

            cStatistics S = new cStatistics();
            S.SetInputData(CET);
            S.Run();

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.SetInputData(S.GetOutPut());
            CDT.Run();
        }

        #endregion

        #region global table operations

        private void ToolStripMenuItem_OperationsAbs(object sender, EventArgs e)
        {
            cArithmetic_Abs CAA = new cArithmetic_Abs();
            CAA.SetInputData(this.Input);
            CAA.Run();

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.SetInputData(CAA.GetOutPut());
            CDT.Run();
        }

        private void ToolStripMenuItem_OperationsSquare(object sender, EventArgs e)
        {
            cArithmetic_Power CAP = new cArithmetic_Power();

            CAP.SetInputData(this.Input);
            CAP.Power = 2;
            CAP.Run();

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.SetInputData(CAP.GetOutPut());
            CDT.Run();
        }

        private void ToolStripMenuItem_OperationsStatistics(object sender, EventArgs e)
        {
            cExtendedTable ET = new cExtendedTable();
            ET.Name = " Statistics(" + this.Input.Name + ")";

            cLinearize L = new cLinearize();
            L.SetInputData(this.Input);
            L.Run();

            cStatistics S = new cStatistics();
            S.SetInputData(L.GetOutPut());
            S.Run();

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.SetInputData(S.GetOutPut());
            CDT.Run();
        }


        private void ToolStripMenuItem_NormalityTest(object sender, EventArgs e)
        {
            cExtendedTable CET = new cExtendedTable();
            foreach (DataGridViewColumn item in GridView.SelectedColumns)
                CET.Add(Input[item.Index]);

            cComputeAndDisplayNormalPlot CDNP = new cComputeAndDisplayNormalPlot();
            CDNP.SetInputData(CET);
            CDNP.Run();


            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(CDNP.GetOutPut());
            DTW.Title = "Normality Test";
            DTW.Run();
            
            DTW.Display();

            //cNormalityJarqueBera NJB = new cNormalityJarqueBera();
            //NJB.SetInputData(CET);
            //cFeedBackMessage FM = NJB.Run();
            //if(FM.IsSucceed==false)
            //{
            //    System.Windows.Forms.MessageBox.Show(FM.Message, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
           
            //cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            //CDT.SetInputData(NJB.GetOutPut());
            //CDT.Run();
        }

        private void ToolStripMenuItem_OperationsClustering(object sender, EventArgs e)
        {
            cClustering CC = new cClustering();

            cExtendedTable CET = new cExtendedTable();
            foreach (DataGridViewColumn item in GridView.SelectedColumns)
                CET.Add(Input[item.Index]);

            if (this.Input.ListRowNames != null)
            {
                CET.ListRowNames = new List<string>();
                foreach (var item in this.Input.ListRowNames)
                    CET.ListRowNames.Add(item);
            }
            if (this.Input.ListTags != null)
            {
                CET.ListTags = new List<object>();
                foreach (var item in this.Input.ListTags)
                    CET.ListTags.Add(item);
            }

            CET.Name = "Sub["+this.Input.Name+"]";

            CC.SetInputData(CET);
            //CC.ListProperties.FindByName("Number of Clusters").SetNewValue((int)3);
            //CC.ListProperties.FindByName("Number of Clusters").IsGUIforValue = true;

            if (!CC.Run().IsSucceed) return;

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.SetInputData(CC.GetOutPut());
            CDT.Run();

        }



        private void ToolStripMenuItem_OperationsCropSelection(object sender, EventArgs e)
        {
            int MinX = 0;
            int MaxX = 0;
            int MinY = 0;
            int MaxY = 0;

            DataGridViewCell item = GridView.SelectedCells[0];
            MinX = MaxX = item.ColumnIndex;
            MinY = MaxY = item.RowIndex;

            for (int i = 1; i < GridView.SelectedCells.Count; i++)
            {
                item = GridView.SelectedCells[i];
                if (item.ColumnIndex < MinX) MinX = item.ColumnIndex;
                if (item.RowIndex < MinY) MinY = item.RowIndex;

                if (item.ColumnIndex > MaxX) MaxX = item.ColumnIndex;
                if (item.RowIndex > MaxY) MaxY = item.RowIndex;
            }

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.SetInputData(this.Input.Crop(MinX, MaxX, MinY, MaxY));
            CDT.Run();
        }

        private void ToolStripMenuItem_OperationsTranspose(object sender, EventArgs e)
        {
            cTranspose CI = new cTranspose();
            CI.SetInputData(this.Input);
            CI.Run();

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.SetInputData(CI.GetOutPut());
            CDT.Run();
        }

        private void ToolStripMenuItem_OperationsInverse(object sender, EventArgs e)
        {
            cInverse CI = new cInverse();
            CI.SetInputData(this.Input);
            CI.Run();

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.SetInputData(CI.GetOutPut());
            CDT.Run();
        }

        private void ToolStripMenuItem_Display3DScatterGraph(object sender, EventArgs e)
        {
            cExtendedTable CET = new cExtendedTable();
            
            foreach (DataGridViewColumn item in GridView.SelectedColumns)
            {
                CET.Add(Input[item.Index]);
            }

            if (this.Input.ListTags != null)
            {
                CET.ListTags = new List<object>();
                foreach (var item in this.Input.ListTags)
                {
                    CET.ListTags.Add(item);    
                }
            }

            cListExtendedTable LET = new cListExtendedTable();
            LET.Add(CET);

            //List<cListWells> ListWells = new List<cListWells>();
            //ListWells.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListActiveWells);


            cViewer3D V3D = new cViewer3D();
            c3DNewWorld MyWorld = new c3DNewWorld(new cPoint3D(1, 1, 1), new cPoint3D(1, 1, 1));

            c3DObjectScatterPoints _3DScatterPt = new c3DObjectScatterPoints();
            _3DScatterPt.ListProperties.FindByName("Draw Axis ?").SetNewValue((bool)true);
            _3DScatterPt.ListProperties.FindByName("Draw Axis ?").IsGUIforValue = true;
            _3DScatterPt.ListProperties.FindByName("Normalized ?").SetNewValue((bool)true);
            _3DScatterPt.ListProperties.FindByName("Normalized ?").IsGUIforValue = true;
            _3DScatterPt.ListProperties.FindByName("Link Points ?").SetNewValue((bool)false);
            _3DScatterPt.ListProperties.FindByName("Link Points ?").IsGUIforValue = true;

            //_3DScatterPt.GlobalInfo = cGlobalInfo;
            _3DScatterPt.SetInputData(LET);
            if (_3DScatterPt.Run(MyWorld).IsSucceed == false) return;


            cListGeometric3DObject GlobalList = _3DScatterPt.GetOutPut();
          
            foreach (var item in GlobalList)
            {
                MyWorld.AddGeometric3DObject(item);
            }
                     

            V3D.SetInputData(MyWorld);
            V3D.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(V3D.GetOutPut());
            DTW.Title = "3D Scatter Graph ("+ this.Input.Name +")";
            DTW.Run();
            DTW.Display();
        }

        private void ToolStripMenuItem_Display2DScatterGraph(object sender, EventArgs e)
        {
            cViewer2DScatterPoint V2DSG = new cViewer2DScatterPoint();
            V2DSG.SetInputData(this.Input);
            V2DSG.Run();

            cDesignerSinglePanel CD = new cDesignerSinglePanel();
            CD.SetInputData(V2DSG.GetOutPut());
            CD.Run();

            cDisplayToWindow DW = new cDisplayToWindow();
            DW.SetInputData(CD.GetOutPut());
            DW.Title = "2D Scatter Graph (" + this.Input.Name +")";
            DW.Run();
            DW.Display();
        }

        private void ToolStripMenuItem_DisplayAsImage(object sender, EventArgs e)
        {
            cConvertToImage CI = new cConvertToImage();
            CI.SetInputData(this.Input);
            CI.Run();

            cDisplaySingleImage DSI = new cDisplaySingleImage();
            DSI.SetInputData(CI.GetOutPut());
            DSI.Run();
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

        private void ToolStripMenuItem_SaveAsARFF(object sender, EventArgs e)
        {
            cTableToFile TCSV = new cTableToFile();
            TCSV.SetInputData(this.Input);
            TCSV.IsDisplayUIForFilePath = true;
            TCSV.FileType = eFileType.ARFF;
            TCSV.Run();
        }

        private void ToolStripMenuItem_SaveAsCSV(object sender, EventArgs e)
        {
            cTableToFile TCSV = new cTableToFile();
            TCSV.SetInputData(this.Input);
            TCSV.IsDisplayUIForFilePath = true;
            TCSV.IsRunEXCEL = true;
            TCSV.Run();
        }


        private void ToolStripMenuItem_SaveAsHTML(object sender, EventArgs e)
        {
            cTableToHTML THTML = new cTableToHTML();
            THTML.SetInputData(this.Input);
            THTML.IsDisplayUIForFilePath = true;

            THTML.ListProperties.FindByName("Open HTML File ?").SetNewValue((bool)true);
            THTML.ListProperties.FindByName("Open HTML File ?").IsGUIforValue = true;

           // THTML.IsDisplayResult = true;
            THTML.Run();
        }

        private void ToolStripMenuItem_ToClipBoard(object sender, EventArgs e)
        {
            cTableToClipBoard TCLB = new cTableToClipBoard();
            TCLB.SetInputData(this.Input);
            TCLB.Run();
        }



        private void DisplayHeatMap(object sender, EventArgs e)
        {
            cViewerHeatMap VHM = new cViewerHeatMap();
            VHM.SetInputData(this.Input);
            VHM.Run();

            cDesignerSinglePanel CD = new cDesignerSinglePanel();
            CD.SetInputData(VHM.GetOutPut());
            CD.Run();

            cDisplayToWindow DW = new cDisplayToWindow();
            DW.SetInputData(CD.GetOutPut());
            DW.Title = "Heat Map [" + this.Input.Name + "]";
            DW.Run();
            DW.Display();
        }
        #endregion
    }
}
