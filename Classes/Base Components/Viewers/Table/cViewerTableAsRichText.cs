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

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    public class cViewerTableAsRichText : cDataDisplay
    {
        cExtendedTable Input = null;
     //   DataGridView GridView = new DataGridView();
        ContextMenuStrip ColumnContextMenu;
        RichTextBox RichText = new RichTextBox();

        public int DigitNumber = 2;

        //public cExtendedTable GetLiveListValues()
        //{
        //    cExtendedTable CET = new cExtendedTable();
        //    CET.Name = Input.Name;
        //    //foreach (DataGridViewColumn item in GridView.SelectedColumns)

        //    for (int ColumnSelected = 0; ColumnSelected < GridView.SelectedColumns.Count; ColumnSelected++)
        //    {
        //        CET.Add(new cExtendedList());
        //        for (int i = 0; i < GridView.Rows.Count - 1; i++)
        //        {
        //            CET[ColumnSelected].Add(double.Parse(GridView[GridView.SelectedColumns[ColumnSelected].Index, i].Value.ToString()));

        //        }
        //        //GridView[item.Index,
        //        //CET.Add();
        //    }
        //    //GridView.SelectedColumns
        //    return CET;
        //}


        public cViewerTableAsRichText()
        {
            Title = "Rich Text Table Viewer";

        }

        public void SetInputData(cExtendedTable MyData)
        {
            this.Input = MyData;
        }

        public cFeedBackMessage Run()
        {
          
            ContextMenuStrip GridViewContextMenu = new ContextMenuStrip();

            ToolStripMenuItem ToolStripMenuItem_DisplayAS = new ToolStripMenuItem("Display as...");

            ToolStripMenuItem ToolStripMenuItem_DisplayHeatMap = new ToolStripMenuItem("Heat Map");
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_DisplayHeatMap);
            ToolStripMenuItem_DisplayHeatMap.Click += new System.EventHandler(this.DisplayHeatMap);

            ToolStripMenuItem ToolStripMenuItem_DisplayElevationMap = new ToolStripMenuItem("Elevation Map");
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_DisplayElevationMap);
            ToolStripMenuItem_DisplayElevationMap.Click += new System.EventHandler(this.DisplayElevationMap);

            ToolStripMenuItem ToolStripMenuItem_Display2DScatterGraph = new ToolStripMenuItem("2D Scatter Graph");
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_Display2DScatterGraph);
            ToolStripMenuItem_Display2DScatterGraph.Click += new System.EventHandler(this.ToolStripMenuItem_Display2DScatterGraph);

            ToolStripMenuItem ToolStripMenuItem_DisplayAsImage = new ToolStripMenuItem("Image");
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_DisplayAsImage);
            ToolStripMenuItem_DisplayAsImage.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayAsImage);

            ToolStripMenuItem_DisplayAS.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_SaveAsCSV = new ToolStripMenuItem("CSV file");
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_SaveAsCSV);
            ToolStripMenuItem_SaveAsCSV.Click += new System.EventHandler(this.ToolStripMenuItem_SaveAsCSV);

            ToolStripMenuItem ToolStripMenuItem_ToClipBoard = new ToolStripMenuItem("CSV to ClipBoard");
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_ToClipBoard);
            ToolStripMenuItem_ToClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_ToClipBoard);

            GridViewContextMenu.Items.Add(ToolStripMenuItem_DisplayAS);

          
            CurrentPanel.Title = this.Title;

            CurrentPanel.Width = 0;
            CurrentPanel.Height = 0;

            RichText.Width = CurrentPanel.Width;
            RichText.Height = CurrentPanel.Height;
            
            this.CurrentPanel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
            | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);


            RichText.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
            | System.Windows.Forms.AnchorStyles.Left| System.Windows.Forms.AnchorStyles.Right);


            cTableToClipBoard TTC = new cTableToClipBoard();
            //TTC.IsRTFFormat = true;
            TTC.SetInputData(this.Input);
            TTC.Run();

            RichText.AppendText(Clipboard.GetText());
            //RichText.Rtf = Clipboard.GetText();



            CurrentPanel.Controls.Add(RichText);

            return base.FeedBackMessage;
        }


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

        private void ToolStripMenuItem_OperationsInverse(object sender, EventArgs e)
        {
            cInverse CI = new cInverse();
            CI.SetInputData(this.Input);
            CI.Run();

            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            CDT.SetInputData(CI.GetOutPut());
            CDT.Run();
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
            DW.Run();
            DW.Display();
        }

        private void ToolStripMenuItem_DisplayAsImage(object sender, EventArgs e)
        {
            cConvertToImage CI = new cConvertToImage();
            CI.SetInputData(this.Input);
            CI.Run();

            cViewerImage VI = new cViewerImage();
            VI.SetInputData(CI.GetOutPut());
            VI.Run();

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


        private void ToolStripMenuItem_SaveAsCSV(object sender, EventArgs e)
        {
            cTableToFile TCSV = new cTableToFile();
            TCSV.SetInputData(this.Input);
            TCSV.IsDisplayUIForFilePath = true;
            TCSV.IsRunEXCEL = true;
            TCSV.Run();
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
            DW.Run();
            DW.Display();
        }
        #endregion
    }
}
