using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic;
using HCSAnalyzer.Classes.General_Types;
using System.Windows.Forms;
using LibPlateAnalysis;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
using HCSAnalyzer.Classes.Base_Classes.Viewers._1D;
using System.Windows.Forms.DataVisualization.Charting;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using System.IO;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    class cViewerListWellsTreeView : cDataDisplay
    {
        List<cListWells> Input;
        TreeView TV;

        public cViewerListWellsTreeView()
        {
            this.Title = "List Wells Tree View";
        }

        public void SetInputData(List<cListWells> Input)
        {
            this.Input = Input;
        }

        public cFeedBackMessage Run()
        {
            try
            {
                if (this.Input == null)
                {
                    base.FeedBackMessage.IsSucceed = false;
                    base.FeedBackMessage.Message = "No Input Data !";
                    return base.FeedBackMessage;
                }

                this.CurrentPanel = new cExtendedControl();
                this.CurrentPanel.Title = this.Title;
                this.CurrentPanel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                            | System.Windows.Forms.AnchorStyles.Left
                                            | System.Windows.Forms.AnchorStyles.Right);

                TV = new TreeView();

                TV.FullRowSelect = true;
                TV.CheckBoxes = true;
                //  TV.MouseClick += new MouseEventHandler(TV_MouseClick);
                TV.NodeMouseClick += new TreeNodeMouseClickEventHandler(TV_NodeMouseClick);
                // TV.MouseMove += new MouseEventHandler(TV_MouseMove);    

                foreach (var item in this.Input)
                {
                    TV.Nodes.Add(item.GetAssociatedTreeNode());
                }

                TV.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                            | System.Windows.Forms.AnchorStyles.Left
                                            | System.Windows.Forms.AnchorStyles.Right);


                CurrentPanel.Controls.Add(TV);

            }
            catch (Exception e)
            {
                cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText("Component [" + this.Title + "] error:\n" + e.Message + "\n\n");
                base.FeedBackMessage.IsSucceed = false;
            }

            return base.FeedBackMessage;
        }

        void TV_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Clicks != 1) return;

            if (e.Button != MouseButtons.Right) return;

            ContextMenuStrip NewMenu = new ContextMenuStrip();

            foreach (var item in GetBasicMenu())
            {
                NewMenu.Items.Add(item);
            }



            NewMenu.Items.Add(new ToolStripSeparator());
            if (e.Node.Tag == null) return;

            if (e.Node.Tag.GetType() == typeof(cWell))
            {
                foreach (var item in ((cWell)e.Node.Tag).GetExtendedContextMenu())
                    NewMenu.Items.Add(item);
            }
            else if (e.Node.Tag.GetType() == typeof(cListWells))
            {
                NewMenu.Items.Add(((cListWells)e.Node.Tag).GetExtendedContextMenu());
            }
            NewMenu.DropShadowEnabled = true;
            NewMenu.Show(Control.MousePosition);

        }

        cListListWells BuildListListWells()
        {
            cListListWells ToBeReturned = new cListListWells();

            foreach (TreeNode item in TV.Nodes)
            {
                if (item.Checked)
                {
                    cListWells TmpList = new cListWells();
                    TmpList.Tag = item;
                    TmpList.Name = item.Text;
                    foreach (TreeNode Subitem in item.Nodes)
                    {
                        if (Subitem.Checked)
                            TmpList.Add((cWell)Subitem.Tag);
                    }

                    ToBeReturned.Add(TmpList);
                }

            }

            return ToBeReturned;
        }

        public List<ToolStripMenuItem> GetBasicMenu()
        {

            List<ToolStripMenuItem> ToBeReturned = new List<ToolStripMenuItem>();


            int NumberOfselectedNodes = 0;
            foreach (TreeNode item in this.TV.Nodes)
            {
                if (item.Checked) NumberOfselectedNodes++;
            }
            if (NumberOfselectedNodes > 0)
            {
                ToolStripMenuItem NewMenuGroupOperations = new ToolStripMenuItem(NumberOfselectedNodes + " Groups Operations");

                ToolStripMenuItem ToolStripMenuItem_DRCAnalysis = new ToolStripMenuItem("DRC Analysis (" + cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor().GetName() + ")");
                ToolStripMenuItem_DRCAnalysis.Click += new EventHandler(ToolStripMenuItem_DRCAnalysis_Click);
                NewMenuGroupOperations.DropDownItems.Add(ToolStripMenuItem_DRCAnalysis);

                ToolStripMenuItem ToolStripMenuItem_DRCAnalysisMultiDesc = new ToolStripMenuItem("DRC Analysis (" + cGlobalInfo.CurrentScreening.GetActiveDescriptors().Count + " desc.)");
                ToolStripMenuItem_DRCAnalysisMultiDesc.Click += new EventHandler(ToolStripMenuItem_DRCAnalysisMultiDesc_Click);
                NewMenuGroupOperations.DropDownItems.Add(ToolStripMenuItem_DRCAnalysisMultiDesc);

                ToolStripMenuItem ToolStripMenuItem_3DDRCDisplay = new ToolStripMenuItem("3D DRC Display");
                ToolStripMenuItem_3DDRCDisplay.Click += new EventHandler(_ToolStripMenuItem_3DDRCDisplay);
                NewMenuGroupOperations.DropDownItems.Add(ToolStripMenuItem_3DDRCDisplay);

                NewMenuGroupOperations.DropDownItems.Add(new ToolStripSeparator());

                ToolStripMenuItem ToolStripMenuItem_createGroups = new ToolStripMenuItem("Generate group ID");
                ToolStripMenuItem_createGroups.Click += new EventHandler(_ToolStripMenuItem_createGroups);
                NewMenuGroupOperations.DropDownItems.Add(ToolStripMenuItem_createGroups);

                ToBeReturned.Add(NewMenuGroupOperations);
            }

            ToolStripMenuItem NewMenuTreeView = new ToolStripMenuItem("Tree View");

            ToolStripMenuItem ToolStripMenuItem_SelectAll_Click = new ToolStripMenuItem("Select All");
            ToolStripMenuItem_SelectAll_Click.Click += new EventHandler(_ToolStripMenuItem_SelectAll_Click);
            NewMenuTreeView.DropDownItems.Add(ToolStripMenuItem_SelectAll_Click);

            ToolStripMenuItem ToolStripMenuItem_UnSelectAll_Click = new ToolStripMenuItem("UnSelect All");
            ToolStripMenuItem_UnSelectAll_Click.Click += new EventHandler(_ToolStripMenuItem_UnSelectAll_Click);
            NewMenuTreeView.DropDownItems.Add(ToolStripMenuItem_UnSelectAll_Click);

            NewMenuTreeView.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_CollapseAll = new ToolStripMenuItem("Collapse All");
            ToolStripMenuItem_CollapseAll.Click += new EventHandler(ToolStripMenuItem_CollapseAll_Click);
            NewMenuTreeView.DropDownItems.Add(ToolStripMenuItem_CollapseAll);

            ToolStripMenuItem ToolStripMenuItem_ExpandAll = new ToolStripMenuItem("Expand All");
            ToolStripMenuItem_ExpandAll.Click += new EventHandler(ToolStripMenuItem_ExpandAll_Click);
            NewMenuTreeView.DropDownItems.Add(ToolStripMenuItem_ExpandAll);
            ToBeReturned.Add(NewMenuTreeView);


            return ToBeReturned;
        }

        void TV_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1) return;

            if (e.Button != MouseButtons.Right) return;

            ContextMenuStrip NewMenu = new ContextMenuStrip();

            foreach (var item in GetBasicMenu())
            {
                NewMenu.Items.Add(item);
            }

            NewMenu.DropShadowEnabled = true;
            NewMenu.Show(Control.MousePosition);
        }

        void _ToolStripMenuItem_createGroups(object sender, EventArgs e)
        {

            // resest groups list
            cGlobalInfo.CurrentScreening.ListGroups.Clear();
            int IdxGroup = 0;

            foreach (var item in this.Input)
            {
                foreach (cWell TmpWell in item)
                    TmpWell.SetGroupIdx(IdxGroup);

                cGroup TmpGroup = new cGroup();
                //    // TmpGroup = (cListWells)item;
                //    //TmpGroup.Add(item);
                TmpGroup.ID = IdxGroup;
                cGlobalInfo.CurrentScreening.ListGroups.Add(TmpGroup);
                IdxGroup++;

                //    ET.ListRowNames.Add(item.Name);
                //    ET.ListTags.Add(item);
                //    ET[0].Add(item.Count);
            }

            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
        }

        void ToolStripMenuItem_DRCAnalysisMultiDesc_Click(object sender, EventArgs e)
        {
            List<cDescriptorType> ListSelectedDescs = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors();


            cListListWells CurrentSelectedGroups = this.BuildListListWells();

            #region select folder
            var dlg1 = new Ionic.Utils.FolderBrowserDialogEx();
            dlg1.Description = "Select the folder containing your databases.";
            dlg1.ShowNewFolderButton = true;
            dlg1.ShowEditBox = true;
            dlg1.ShowFullPathInEditBox = true;

            DialogResult result = dlg1.ShowDialog();
            if (result != DialogResult.OK) return;

            string Path = dlg1.SelectedPath;
            if (Directory.Exists(Path) == false) return;

            string FolderName = dlg1.SelectedPath;
            #endregion

            foreach (var item in ListSelectedDescs)
            {
                string TmpFolder = FolderName + "\\" + item.GetName();
                Directory.CreateDirectory(TmpFolder);


                int IdxNode = 0;

                List<cDescriptorType> ListSelectedDesc = new List<cDescriptorType>();
                ListSelectedDesc.Add(item);

                cExtendedTable TableForGeneralResuts = new cExtendedTable();
                TableForGeneralResuts.Add(new cExtendedList("p-Value"));
                TableForGeneralResuts.Add(new cExtendedList("EC50"));
                TableForGeneralResuts.Add(new cExtendedList("Slope"));
                TableForGeneralResuts.Add(new cExtendedList("Bottom"));
                TableForGeneralResuts.Add(new cExtendedList("Top"));
                TableForGeneralResuts.Add(new cExtendedList("Window"));
                TableForGeneralResuts.Add(new cExtendedList("Area Under Curve"));

                TableForGeneralResuts.ListRowNames = new List<string>();
                TableForGeneralResuts.ListTags = new List<object>();

                foreach (cListWells TmpListWells in CurrentSelectedGroups)
                {
                    // List<cDescriptorType> LType = new List<cDescriptorType>();
                    //  LType.Add(ListSelectedDesc[i]);
                    cExtendedTable CompleteTable = TmpListWells.GetAverageDescriptorValues(ListSelectedDesc, true, false);

                    //cExtendedTable CompleteTable = GLP.GetOutPut();

                    cCurveForGraph CFG = new cCurveForGraph();
                    CFG.SetInputData(CompleteTable);
                    CFG.Run();

                    cSigmoidFitting SF = new cSigmoidFitting();
                    SF.SetInputData(CompleteTable);
                    if (SF.Run().IsSucceed == false) continue;

                    // double Ratio = LR.GetOutPut()[0][LR.GetOutPut().Count - 1] / SF.GetOutPut()[0][SF.GetOutPut().Count - 1];

                    cANOVA A = new cANOVA();


                    cExtendedTable NewTable = CFG.ListPtValues.Crop(0, CFG.ListPtValues.Count - 1, 1, CFG.ListPtValues[0].Count - 1);
                    A.SignificanceThreshold = 1E-11;
                    A.SetInputData(NewTable);
                    A.Run();

                    cExtendedTable Sigmoid = SF.GetFittedRawValues(CFG.GetListXValues());
                    CompleteTable[0] = Sigmoid[1];
                    CompleteTable[0].Name = ListSelectedDesc[0].GetName() + "\n" + Sigmoid[1].Name;
                    cDesignerSplitter DS = new cDesignerSplitter();

                    //cViewerTableAsRichText VT = new cViewerTableAsRichText();
                    cViewerTable VT = new cViewerTable();
                    cExtendedTable TableResults = SF.GetOutPut();

                    if ((A.GetOutPut() != null) && (A.GetOutPut().Count > 0))
                    {
                        TableResults[0].Add(A.GetOutPut()[0][0]);
                        TableResults[0].Add(A.GetOutPut()[0][1]);
                        TableResults.ListRowNames.Add("p-Value");
                        TableResults.ListRowNames.Add("Rejected?");

                        TableForGeneralResuts[0].Add(A.GetOutPut()[0][0]);
                    }
                    else
                    {
                        TableForGeneralResuts[0].Add(1);
                    }

                    TableResults.Name = TV.Nodes[IdxNode].Text;
                    TableResults[0].Name = "Fitting Parameters";
                    VT.SetInputData(TableResults);
                    VT.DigitNumber = -1;
                    VT.Run();

                    cViewerGraph1D VS1 = new cViewerGraph1D();

                    cExtendedTable MyTable = new cExtendedTable(Sigmoid[1]);
                    MyTable.Name = TmpListWells.Name;// TV.Nodes[IdxNode].Text;
                    VS1.SetInputData(MyTable);
                    VS1.AddCurve(CFG);

                    VS1.Chart.X_AxisValues = Sigmoid[0];
                    VS1.Chart.IsLogAxis = true;
                    VS1.Chart.IsLine = true;
                    VS1.Chart.IsShadow = false;
                    VS1.Chart.Opacity = 210;
                    VS1.Chart.LineWidth = 3;
                    VS1.Chart.MarkerSize = 8;
                    VS1.Chart.IsDisplayValues = cGlobalInfo.OptionsWindow.FFAllOptions.checkBoxDRCDisplayValues.Checked;
                    VS1.Chart.LabelAxisX = "Concentration";
                    VS1.Chart.LabelAxisY = CompleteTable[1].Name;
                    VS1.Chart.XAxisFormatDigitNumber = cGlobalInfo.OptionsWindow.FFAllOptions.GetDRCNumberOfDigit();
                    VS1.Chart.IsZoomableX = true;

                    Classes.Base_Classes.General.cLineVerticalForGraph VLForEC50 = new Classes.Base_Classes.General.cLineVerticalForGraph(SF.GetOutPut()[0][2]);
                    //VLForEC50.AddText("EC50: " + SF.GetOutPut()[0][2].ToString("e3")/* + "\nError Ratio:" + Ratio.ToString("N4")*/);
                    VS1.Chart.ListVerticalLines.Add(VLForEC50);

                    TableForGeneralResuts.ListRowNames.Add(TV.Nodes[IdxNode].Text);
                    TableForGeneralResuts.ListTags.Add((Chart)VS1.Chart);//ListSelectedDesc[0]);

                    //EC50
                    TableForGeneralResuts[1].Add(SF.GetOutPut()[0][2]);

                    // Slope
                    TableForGeneralResuts[2].Add(SF.GetOutPut()[0][3]);

                    // Bottom
                    TableForGeneralResuts[3].Add(SF.GetOutPut()[0][0]);

                    // Top
                    TableForGeneralResuts[4].Add(SF.GetOutPut()[0][1]);

                    // Window
                    double Window = SF.GetOutPut()[0][1] / SF.GetOutPut()[0][0];
                    TableForGeneralResuts[5].Add(Window);

                    TableForGeneralResuts[6].Add(SF.GetOutPut()[0][5]);


                    VS1.Chart.ArraySeriesInfo = new cSerieInfoDesign[CompleteTable.Count];

                    for (int IdxCurve = 0; IdxCurve < CompleteTable.Count; IdxCurve++)
                    {
                        cSerieInfoDesign TmpSerieInfo = new cSerieInfoDesign();
                        TmpSerieInfo.color = cGlobalInfo.ListCellularPhenotypes[IdxCurve % cGlobalInfo.ListCellularPhenotypes.Count].ColourForDisplay;
                        TmpSerieInfo.markerStyle = MarkerStyle.None;

                        VS1.Chart.ArraySeriesInfo[IdxCurve] = TmpSerieInfo;
                    }

                    VS1.Run();

                    //DS.SetInputData(VS1.GetOutPut());
                    //DS.SetInputData(VT.GetOutPut());
                    //DS.Orientation = Orientation.Horizontal;
                    //DS.Title = TV.Nodes[IdxNode++].Text; //TmpListWells.ti;// ListSelectedDesc[0].GetName();
                    //DS.Run();


                    IdxNode++;

                    //   DT.SetInputData(DS.GetOutPut());
                }
                cTableToHTML THTML = new cTableToHTML();
                TableForGeneralResuts.Name = item.GetName();
                THTML.SetInputData(TableForGeneralResuts);
                THTML.IsDisplayUIForFilePath = false;
                THTML.FolderName = TmpFolder;

                THTML.ListProperties.FindByName("Open HTML File ?").SetNewValue((bool)false);
                THTML.ListProperties.FindByName("Open HTML File ?").IsGUIforValue = false;

                THTML.Run();
            }

            System.Diagnostics.Process.Start(FolderName);

        }

        void ToolStripMenuItem_DRCAnalysis_Click(object sender, EventArgs e)
        {

            List<cDescriptorType> ListSelectedDesc = new List<cDescriptorType>();// GlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors();        
            ListSelectedDesc.Add(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor());

            //if (GlobalInfo == null) GlobalInfo = cGlobalInfo.CurrentScreening.GlobalInfo;
            //if (cGlobalInfo == null) return;

            cDesignerMultiChoices DT = new cDesignerMultiChoices();

            cExtendedTable TableForGeneralResuts = new cExtendedTable();
            TableForGeneralResuts.Add(new cExtendedList("p-Value"));
            TableForGeneralResuts.Add(new cExtendedList("EC50"));
            TableForGeneralResuts.Add(new cExtendedList("Slope"));
            TableForGeneralResuts.Add(new cExtendedList("Bottom"));
            TableForGeneralResuts.Add(new cExtendedList("Top"));
            TableForGeneralResuts.Add(new cExtendedList("Window"));
            TableForGeneralResuts.Add(new cExtendedList("Area Under Curve"));

            TableForGeneralResuts.ListRowNames = new List<string>();
            TableForGeneralResuts.ListTags = new List<object>();

            cDesignerSplitter MainSplitter = new cDesignerSplitter();

            int IdxNode = 0;
            cListListWells CurrentSelectedGroups = this.BuildListListWells();


            foreach (cListWells TmpListWells in CurrentSelectedGroups)
            {
                // List<cDescriptorType> LType = new List<cDescriptorType>();
                //  LType.Add(ListSelectedDesc[i]);
                cExtendedTable CompleteTable = TmpListWells.GetAverageDescriptorValues(ListSelectedDesc, true, false);

                //cExtendedTable CompleteTable = GLP.GetOutPut();

                cCurveForGraph CFG = new cCurveForGraph();
                CFG.SetInputData(CompleteTable);
                CFG.Run();

                cSigmoidFitting SF = new cSigmoidFitting();
                SF.SetInputData(CompleteTable);
                if (SF.Run().IsSucceed == false) continue;

                // double Ratio = LR.GetOutPut()[0][LR.GetOutPut().Count - 1] / SF.GetOutPut()[0][SF.GetOutPut().Count - 1];

                cANOVA A = new cANOVA();


                cExtendedTable NewTable = CFG.ListPtValues.Crop(0, CFG.ListPtValues.Count - 1, 1, CFG.ListPtValues[0].Count - 1);
                A.SignificanceThreshold = 1E-11;
                A.SetInputData(NewTable);
                A.Run();

                cExtendedTable Sigmoid = SF.GetFittedRawValues(CFG.GetListXValues());
                CompleteTable[0] = Sigmoid[1];
                CompleteTable[0].Name = ListSelectedDesc[0].GetName() + "\n" + Sigmoid[1].Name;
                cDesignerSplitter DS = new cDesignerSplitter();

                //cViewerTableAsRichText VT = new cViewerTableAsRichText();
                cViewerTable VT = new cViewerTable();
                cExtendedTable TableResults = SF.GetOutPut();

                if ((A.GetOutPut() != null) && (A.GetOutPut().Count > 0))
                {
                    TableResults[0].Add(A.GetOutPut()[0][0]);
                    TableResults[0].Add(A.GetOutPut()[0][1]);
                    TableResults.ListRowNames.Add("p-Value");
                    TableResults.ListRowNames.Add("Rejected?");

                    TableForGeneralResuts[0].Add(A.GetOutPut()[0][0]);
                }
                else
                {
                    TableForGeneralResuts[0].Add(1);
                }

                TableResults.Name = TV.Nodes[IdxNode].Text;
                TableResults[0].Name = "Fitting Parameters";
                VT.SetInputData(TableResults);
                VT.DigitNumber = -1;
                VT.Run();

                cViewerGraph1D VS1 = new cViewerGraph1D();

                cExtendedTable MyTable = new cExtendedTable(Sigmoid[1]);
                MyTable.Name = TmpListWells.Name;// TV.Nodes[IdxNode].Text;
                VS1.SetInputData(MyTable);
                VS1.AddCurve(CFG);

                VS1.Chart.X_AxisValues = Sigmoid[0];
                VS1.Chart.IsLogAxis = true;
                VS1.Chart.IsLine = true;
                VS1.Chart.IsShadow = false;
                VS1.Chart.Opacity = 210;
                VS1.Chart.LineWidth = 3;
                VS1.Chart.MarkerSize = 8;
                VS1.Chart.IsDisplayValues = cGlobalInfo.OptionsWindow.FFAllOptions.checkBoxDRCDisplayValues.Checked;
                VS1.Chart.LabelAxisX = "Concentration";
                VS1.Chart.LabelAxisY = CompleteTable[1].Name;
                VS1.Chart.XAxisFormatDigitNumber = cGlobalInfo.OptionsWindow.FFAllOptions.GetDRCNumberOfDigit();
                VS1.Chart.IsZoomableX = true;

                Classes.Base_Classes.General.cLineVerticalForGraph VLForEC50 = new Classes.Base_Classes.General.cLineVerticalForGraph(SF.GetOutPut()[0][2]);
                //VLForEC50.AddText("EC50: " + SF.GetOutPut()[0][2].ToString("e3")/* + "\nError Ratio:" + Ratio.ToString("N4")*/);
                VS1.Chart.ListVerticalLines.Add(VLForEC50);

                TableForGeneralResuts.ListRowNames.Add(TV.Nodes[IdxNode].Text);
                TableForGeneralResuts.ListTags.Add((Chart)VS1.Chart);//ListSelectedDesc[0]);

                //EC50
                TableForGeneralResuts[1].Add(SF.GetOutPut()[0][2]);

                // Slope
                TableForGeneralResuts[2].Add(SF.GetOutPut()[0][3]);

                // Bottom
                TableForGeneralResuts[3].Add(SF.GetOutPut()[0][0]);

                // Top
                TableForGeneralResuts[4].Add(SF.GetOutPut()[0][1]);

                // Window
                double Window = SF.GetOutPut()[0][1] / SF.GetOutPut()[0][0];
                TableForGeneralResuts[5].Add(Window);

                TableForGeneralResuts[6].Add(SF.GetOutPut()[0][5]);


                VS1.Chart.ArraySeriesInfo = new cSerieInfoDesign[CompleteTable.Count];

                for (int IdxCurve = 0; IdxCurve < CompleteTable.Count; IdxCurve++)
                {
                    cSerieInfoDesign TmpSerieInfo = new cSerieInfoDesign();
                    TmpSerieInfo.color = cGlobalInfo.ListCellularPhenotypes[IdxCurve % cGlobalInfo.ListCellularPhenotypes.Count].ColourForDisplay;
                    TmpSerieInfo.markerStyle = MarkerStyle.None;

                    VS1.Chart.ArraySeriesInfo[IdxCurve] = TmpSerieInfo;
                }

                VS1.Run();

                DS.SetInputData(VS1.GetOutPut());
                DS.SetInputData(VT.GetOutPut());
                DS.Orientation = Orientation.Horizontal;
                DS.Title = TV.Nodes[IdxNode++].Text; //TmpListWells.ti;// ListSelectedDesc[0].GetName();
                DS.Run();
                DT.SetInputData(DS.GetOutPut());
            }

            DT.Run();
            cExtendedControl TextEC = DT.GetOutPut();
            if (TextEC == null) return;

            TextEC.Width = 0;
            TextEC.Height = 0;

            TextEC.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                    | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);

            MainSplitter.SetInputData(TextEC);



            string DRCsortingMethod = cGlobalInfo.OptionsWindow.FFAllOptions.comboBoxDRCSorting.SelectedItem.ToString();
            //comboBoxDRCSorting
            int IdxColSorting = 0;
            cExtendedTable RT = TableForGeneralResuts;

            switch (DRCsortingMethod)
            {
                case "ANOVA":
                    IdxColSorting = 0;
                    break;
                case "EC50":
                    IdxColSorting = 1;
                    break;
                case "Window":
                    IdxColSorting = 5;
                    break;
                case "AUC":
                    IdxColSorting = 6;
                    break;
                case "None":
                    IdxColSorting = -1;
                    break;
                default:
                    break;

            }

            if (IdxColSorting != -1)
            {
                cSort S = new cSort();
                S.ColumnIndexForSorting = IdxColSorting;
                S.SetInputData(TableForGeneralResuts);
                S.IsAscending = true;
                S.Run();
                RT = S.GetOutPut();
            }
            cViewerTable VTForEC50 = new cViewerTable();

            RT.Name = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor().GetName();
            VTForEC50.SetInputData(RT);
            VTForEC50.DigitNumber = -1;
            VTForEC50.Run();

            MainSplitter.SetInputData(VTForEC50.GetOutPut());

            MainSplitter.Orientation = Orientation.Vertical;
            MainSplitter.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(MainSplitter.GetOutPut());
            DTW.Title = "DRC analysis [" + cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor().GetName() + "]";

            DTW.Run();
            DTW.Display();
        }

        void ToolStripMenuItem_ExpandAll_Click(object sender, EventArgs e)
        {
            TV.ExpandAll();
        }

        void ToolStripMenuItem_CollapseAll_Click(object sender, EventArgs e)
        {
            TV.CollapseAll();
        }

        void _ToolStripMenuItem_SelectAll_Click(object sender, EventArgs e)
        {
            foreach (TreeNode item in TV.Nodes)
            {
                item.Checked = true;

                foreach (TreeNode Subitem in item.Nodes)
                {
                    Subitem.Checked = true;
                }
            }
        }

        void _ToolStripMenuItem_UnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (TreeNode item in TV.Nodes)
            {
                item.Checked = false;

                foreach (TreeNode Subitem in item.Nodes)
                {
                    Subitem.Checked = false;
                }
            }
        }

        void _ToolStripMenuItem_3DDRCDisplay(object sender, EventArgs e)
        {
            this.BuildListListWells().BuildAssociated3DDRC();
        }


    }
}
