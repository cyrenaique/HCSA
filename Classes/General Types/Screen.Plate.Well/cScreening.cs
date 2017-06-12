using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using HCSAnalyzer.Forms;
using HCSAnalyzer;
using weka.core;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Classes._3D;
using System.Data.SQLite;
using weka.classifiers;
using weka.classifiers.trees;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes.General_Types.Screen;
using HCSAnalyzer.Classes.Base_Classes.GUI;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Classes.Base_Classes.Viewers;

namespace LibPlateAnalysis
{
    //public class cListPlates : List<cPlate>
    //{
    //    public cPlate GetPlate(string PlateName)
    //    {
    //        for (int Idx = 0; Idx < this.Count; Idx++)
    //        {
    //            if (PlateName == this[Idx].Name)
    //                return this[Idx];
    //        }
    //        return null;
    //    }

    //    public cPlate GetPlate(int Idx)
    //    {
    //        if (Idx < 0) return null;
    //        if (this.Count == 0) return null;
    //        if (Idx >= this.Count) return null;
    //        return this[Idx];
    //    }
    //}

    public class cInfoForHierarchical
    {
        public weka.core.Instances ListInstances = null;
        public List<cWell> ListIndexedWells = new List<cWell>();
        public List<double> ListMin = new List<double>();
        public List<double> ListMax = new List<double>();

        public void UpDateMinMax(cScreening CurrentScreen)
        {
            for (int iDesc = 0; iDesc < CurrentScreen.ListDescriptors.Count; iDesc++)
            {
                if (CurrentScreen.ListDescriptors[iDesc].IsActive() == false) continue;
                double MinVal = double.MaxValue;
                double MaxVal = double.MinValue;

                foreach (cWell CurrentWell in ListIndexedWells)
                {
                    double TmpValue = CurrentWell.ListSignatures[iDesc].GetValue();
                    if (TmpValue < MinVal) MinVal = TmpValue;
                    else if (TmpValue > MaxVal) MaxVal = TmpValue;
                }

                ListMin.Add(MinVal);
                ListMax.Add(MaxVal);
            }
        }

        public void UpDateMinMaxDescByDesc()
        {
            ListMin.Clear();
            ListMax.Clear();

            for (int iDesc = 0; iDesc < ListInstances.numAttributes(); iDesc++)
            {
                double MinVal = double.MaxValue;
                double MaxVal = double.MinValue;
                foreach (Instance item in ListInstances)
                {
                    double TmpValue = item.value(iDesc);
                    if (TmpValue < MinVal) MinVal = TmpValue;
                    else if (TmpValue > MaxVal) MaxVal = TmpValue;
                }
                ListMin.Add(MinVal);
                ListMax.Add(MaxVal);
            }
        }

        public void UpDateMinMaxGlobal(double Min, double Max)
        {
            ListMin.Clear();
            ListMax.Clear();

            for (int iDesc = 0; iDesc < ListInstances.numAttributes(); iDesc++)
            {
                ListMin.Add(Min);
                ListMax.Add(Max);
            }
        }


    }

    public class cCellBasedClassification
    {
        public Classifier ClassificationModel_CellBased;
        public Evaluation evaluation;
        public J48 J48Model { get; private set; }
        public int NumClasses { get; private set; }

        public void SetJ48Tree(J48 J48Model, int NumClasses)
        {
            this.J48Model = J48Model;
            this.NumClasses = NumClasses;
        }

        public Instances CreateInstancesWithoutClass(DataTable dt)
        {
            weka.core.FastVector atts = new FastVector();
            int columnNo = 0;

            // Descriptors loop
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                //if (ParentScreening.ListDescriptors[i].IsActive() == false) continue;
                atts.addElement(new weka.core.Attribute(dt.Columns[i].ColumnName));
                columnNo++;
            }
            // weka.core.FastVector attVals = new FastVector();
            Instances data1 = new Instances("MyRelation", atts, 0);

            for (int IdxRow = 0; IdxRow < dt.Rows.Count; IdxRow++)
            {
                double[] vals = new double[data1.numAttributes()];
                for (int Col = 0; Col < columnNo; Col++)
                {
                    //if (ParentScreening.ListDescriptors[Col].IsActive() == false) continue;
                    vals[Col] = double.Parse(dt.Rows[IdxRow][Col].ToString());
                }
                data1.add(new DenseInstance(1.0, vals));
            }

            return data1;
        }

        public Instances CreateInstancesWithoutClass(cExtendedTable Input)
        {
            weka.core.FastVector atts = new FastVector();
            int columnNo = 0;

            // Descriptors loop
            for (int i = 0; i < Input.Count; i++)
            {
                //if (ParentScreening.ListDescriptors[i].IsActive() == false) continue;
                atts.addElement(new weka.core.Attribute(Input[i].Name));
                columnNo++;
            }
            // weka.core.FastVector attVals = new FastVector();
            Instances data1 = new Instances("MyRelation", atts, 0);

            for (int IdxRow = 0; IdxRow < Input[0].Count; IdxRow++)
            {
                double[] vals = new double[data1.numAttributes()];
                for (int Col = 0; Col < columnNo; Col++)
                {
                    //if (ParentScreening.ListDescriptors[Col].IsActive() == false) continue;
                    vals[Col] = Input[Col][IdxRow];
                }
                data1.add(new DenseInstance(1.0, vals));
            }

            return data1;
        }

        public FormForClassificationTree DisplayTree(cGlobalInfo GlobalInfo)
        {
            FormForClassificationTree WindowForTree = new FormForClassificationTree();
            if (J48Model == null) return null;
            string StringForTree = J48Model.graph().Remove(0, J48Model.graph().IndexOf("{") + 2);
            WindowForTree.gViewerForTreeClassif.Graph = cGlobalInfo.WindowHCSAnalyzer.ComputeAndDisplayGraph(StringForTree.Remove(StringForTree.Length - 3, 3), false);
            WindowForTree.richTextBoxConsoleForClassification.Clear();
            if (evaluation != null)
            {
                WindowForTree.richTextBoxConsoleForClassification.AppendText(evaluation.toSummaryString());
                WindowForTree.richTextBoxConsoleForClassification.AppendText(evaluation.toMatrixString());
            }
            return WindowForTree;
        }

    }

    public class cScreening
    {
        public cListGroups ListGroups = new cListGroups();

        public bool ISLoading = true;
        public cReference Reference = null;
        public cCellBasedClassification CellBasedClassification = new cCellBasedClassification();
        public c3DWorld _3DWorldForPlateDisplay;

        public Point ptOriginal = new Point();
        public Point ptLast = new Point();

        public Point ClientPosFirst = new Point();
        public Point ClientPosLast = new Point();

        public List<string> ListPlateBaseddescriptorNames;

        private string Name;

        public string GetName()
        {
            return Name;
        }

        public void SetName(string NewName)
        {
            this.Name = NewName;
            UpDateName();
        }

        void UpDateName()
        {
            cGlobalInfo.WindowHCSAnalyzer.Text = cGlobalInfo.WindowName + " - [" + this.Name + "]";
        }

        public cListPlates ListPlatesActive;
        public cListPlates ListPlatesAvailable;

        public int SelectedClass;
        public int Columns;
        public int Rows;
        public bool IsSelectionApplyToAllPlates = false;
        public int CurrentDisplayPlateIdx = 0;

       // public cGlobalInfo GlobalInfo;
        public cListDescriptorTypes ListDescriptors;

        public cListWellPropertyType ListWellPropertyTypes = null;
        public cListPlatePropertyType ListPlatePropertyTypes = null;

        public Label LabelForMin;
        public Label LabelForMax;
        public Panel PanelForLUT;
        public Panel PanelForPlate;

        public int CurrentHistoryClassStatusIndex = 0;

        #region Constructors
        public cScreening()
        {
           
        }

        public cScreening(string Name)
        {
          //  if (cGlobalInfo != null)
            {
                //this.GlobalInfo = GlobalInfo;
                this.ListDescriptors = new cListDescriptorTypes(cGlobalInfo.CheckedListBoxForDescActive, cGlobalInfo.ComboForSelectedDesc);
            }

            this.Name = Name;

            ListPlatesAvailable = new cListPlates(null);

            this.ListPlateBaseddescriptorNames = new List<string>();
            this.ListPlateBaseddescriptorNames.Add("Row_Pos");
            this.ListPlateBaseddescriptorNames.Add("Col_Pos");
            this.ListPlateBaseddescriptorNames.Add("Dist_To_Border");
            this.ListPlateBaseddescriptorNames.Add("Dist_To_Center");

            #region Well properies initialization
            
            
            this.ListWellPropertyTypes = new cListWellPropertyType(this);

            foreach (var item in cGlobalInfo.ListDefaultPropertyTypes)
            {
                this.ListWellPropertyTypes.AddNewType(item);
            }
            #endregion

            cGlobalInfo.WindowHCSAnalyzer.toolStripDropDownButtonDisplayMode.DropDownItems.Add(new ToolStripSeparator());


            ToolStripMenuItem CurrentDescriptorValueItem = new ToolStripMenuItem("Current Descriptor Value");
            CurrentDescriptorValueItem.CheckOnClick = true;
            CurrentDescriptorValueItem.Checked = false;
            CurrentDescriptorValueItem.CheckedChanged += new EventHandler(this.ListWellPropertyTypes.NewItem_CheckedChanged);
            cGlobalInfo.WindowHCSAnalyzer.toolStripDropDownButtonDisplayMode.DropDownItems.Add(CurrentDescriptorValueItem);

            cGlobalInfo.WindowHCSAnalyzer.toolStripDropDownButtonDisplayMode.DropDownItems.Add(new ToolStripSeparator());



            ToolStripMenuItem InactiveItem = new ToolStripMenuItem("Inactive");
            InactiveItem.CheckOnClick = true;
            InactiveItem.Checked = true;
            //InactiveItem.CheckedChanged += new EventHandler(InactiveItem_CheckedChanged);
            cGlobalInfo.WindowHCSAnalyzer.toolStripDropDownButtonDisplayMode.DropDownItems.Add(InactiveItem);



            this.ListPlatePropertyTypes = new cListPlatePropertyType(this);
            this.ListPlatePropertyTypes.AddNewType(new cPropertyType("Quality", eDataType.DOUBLE));
            this.ListPlatePropertyTypes.AddNewType(new cPropertyType("Name", eDataType.STRING));


            cGlobalInfo.WindowHCSAnalyzer.Text = cGlobalInfo.WindowName + " - [" + this.Name + "]";
            cGlobalInfo.OptionsWindow.FFAllOptions.textBoxScreeningName.Enabled = true;
            cGlobalInfo.OptionsWindow.FFAllOptions.textBoxScreeningName.Text = this.Name;
        }

        //public void InactiveItem_CheckedChanged(object sender, EventArgs e)
        //{


        //}

        //public void CurrentDescriptorValueItem_CheckedChanged(object sender, EventArgs e)
        //{
        //    //foreach (var item in cGlobalInfo.WindowHCSAnalyzer.toolStripDropDownButtonDisplayMode.DropDownItems)
        //    //{
        //    //    if (item.GetType() == typeof(ToolStripMenuItem))
        //    //    {
        //    //        ToolStripMenuItem TmpMenuItem = ((ToolStripMenuItem)item);
        //    //        TmpMenuItem.CheckedChanged -= new EventHandler(CurrentDescriptorValueItem_CheckedChanged);
        //    //        TmpMenuItem.Checked = false;
        //    //        TmpMenuItem.CheckedChanged += new EventHandler(CurrentDescriptorValueItem_CheckedChanged);
        //    //    }
        //    //}
        //   // ((ToolStripMenuItem)(sender)).CheckedChanged -= new EventHandler(CurrentDescriptorValueItem_CheckedChanged);
        // //   ((ToolStripMenuItem)(sender)).Checked = true;
        ////    ((ToolStripMenuItem)(sender)).CheckedChanged += new EventHandler(CurrentDescriptorValueItem_CheckedChanged);

        ////    cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);

        //}

        #endregion

        public List<cDescriptorType> GetActiveDescriptors()
        {
            List<cDescriptorType> LDT = new List<cDescriptorType>();

            foreach (var item in this.ListDescriptors)
            {
                if (item.IsActive() == false) continue;
                LDT.Add(item);
            }

            return LDT;
        }

        public void CopyValuestoClipBoard()
        {
            cGUI_ListPlates GUI_ListPlates = new cGUI_ListPlates();
            GUI_ListPlates.IsCheckBoxes = true;
            cFeedBackMessage FBM = GUI_ListPlates.Run();
            cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(FBM.Message);
            if (!FBM.IsSucceed) return;

            cListPlates LP = GUI_ListPlates.GetOutPut();


            StringBuilder sb = new StringBuilder();

            sb.Append(String.Format(this.ListDescriptors.GetActiveDescriptor().GetName() + "\n\n"));

            foreach (cPlate Plate in LP)
            {
                //sb.Append("\t");
                sb.Append(String.Format(Plate.GetName() + "\t"));

                for (int i = 0; i < this.Columns - 1; i++)
                {
                    int IdxCol = i + 1;
                    sb.Append(String.Format("{0}\t", IdxCol));
                }
                sb.Append(String.Format("{0}", this.Columns));
                sb.AppendLine();

                for (int j = 0; j < this.Rows; j++)
                {
                    byte[] strArray = new byte[1];
                    strArray[0] = (byte)(j + 65);

                    string Chara = Encoding.UTF7.GetString(strArray);
                    sb.Append(String.Format("{0}\t", Chara));

                    for (int i = 0; i < this.Columns - 1; i++)
                    {
                        cWell CurrentWell = Plate.GetWell(i, j, false);
                        if (CurrentWell == null)
                            sb.Append("\t");
                        else
                            sb.Append(String.Format("{0}\t", CurrentWell.ListSignatures[this.ListDescriptors.CurrentSelectedDescriptorIdx].GetValue()));
                    }


                    cWell CurrentWellFinal = Plate.GetWell(this.Columns - 1, j, false);
                    if (CurrentWellFinal == null)
                        sb.Append("\t");
                    else
                        sb.Append(String.Format("{0}\t", CurrentWellFinal.ListSignatures[this.ListDescriptors.CurrentSelectedDescriptorIdx].GetValue()));


                    //  sb.Append(String.Format("{0}", CompleteScreening.GetPlate(0).GetWell(CompleteScreening.Columns-1, j).ListDescriptors[(int)numericUpDownDescriptorIndex.Value].AverageValue));
                    sb.AppendLine();
                }

                sb.Append("\n");
            }
            Clipboard.SetText(sb.ToString());
        }

        private void ToolStripMenuItem_Info(object sender, EventArgs e)
        {
            cDisplayText DT = new cDisplayText();
            DT.SetInputData(this.GetInfo());
            DT.Run();

        }



        private void ToolStripMenuItem_ExportToExcel(object sender, EventArgs e)
        {
            cExtendedTable ET = this.GetCurrentDisplayPlate().GetAverageValueTable(this.ListDescriptors.GetActiveDescriptor(), false);
            cTableToFile TTF = new cTableToFile();
            TTF.SetInputData(ET);
            TTF.IsDisplayUIForFilePath = true;
            TTF.IsTagToBeSaved = true;
            TTF.IsRunEXCEL = true;

            TTF.Run();
        }


        private void ToolStripMenuItem_CopyValuestoClipBoard(object sender, EventArgs e)
        {
            CopyValuestoClipBoard();
        }

        private void ToolStripMenuItem_CopyPropertyToClipBoard(object sender, EventArgs e)
        {
            cGUI_ListWellProperty GUI_ListWellProperty = new cGUI_ListWellProperty();
            GUI_ListWellProperty.IsCheckBoxes = false;
            if (GUI_ListWellProperty.Run().IsSucceed == false) return;
            List<cPropertyType> ListSelectedProp = GUI_ListWellProperty.GetOutPut();


            cGUI_ListPlates GUI_ListPlates = new cGUI_ListPlates();
            GUI_ListPlates.IsCheckBoxes = true;
            cFeedBackMessage FBM = GUI_ListPlates.Run();
            cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(FBM.Message);
            if (!FBM.IsSucceed) return;

            cListPlates LP = GUI_ListPlates.GetOutPut();

            CopyPropertyToClipBoard(ListSelectedProp, LP);
        }

        public void CopyPropertyToClipBoard(List<cPropertyType> ListSelectedProp, cListPlates LP)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(String.Format(this.Name + " [" + ListSelectedProp[0].Name + "]\n\n"));

            foreach (cPlate Plate in LP)
            {
                sb.Append(String.Format(Plate.GetName() + "\t"));

                for (int i = 0; i < this.Columns - 1; i++)
                {
                    int IdxCol = i + 1;
                    sb.Append(String.Format("{0}\t", IdxCol));
                }
                sb.Append(String.Format("{0}", this.Columns));
                sb.AppendLine();

                for (int j = 0; j < this.Rows; j++)
                {
                    byte[] strArray = new byte[1];
                    strArray[0] = (byte)(j + 65);

                    string Chara = Encoding.UTF7.GetString(strArray);
                    sb.Append(String.Format("{0}\t", Chara));

                    for (int i = 0; i < this.Columns - 1; i++)
                    {
                        cWell CurrentWell = Plate.GetWell(i, j, false);
                        if (CurrentWell == null)
                            sb.Append("\t");
                        else
                        {
                            object TmpOBj = CurrentWell.ListProperties.FindValueByName(ListSelectedProp[0].Name);
                            if (TmpOBj == null)
                                sb.Append("n.a.\t");
                            else
                                sb.Append(TmpOBj.ToString() + "\t");
                        }
                    }

                    cWell CurrentWellFinal = Plate.GetWell(this.Columns - 1, j, false);
                    if (CurrentWellFinal == null)
                        sb.Append("\t");
                    else
                    {
                        object TmpOBj = CurrentWellFinal.ListProperties.FindValueByName(ListSelectedProp[0].Name);
                        if (TmpOBj == null)
                            sb.Append("n.a.\t");
                        else
                            sb.Append(TmpOBj.ToString() + "\t");
                    }

                    sb.AppendLine();

                }
                sb.Append("\n");
            }
            Clipboard.SetText(sb.ToString());

        }

        public ToolStripMenuItem GetExtendedContextMenu()
        {

            #region Context Menu
            ToolStripMenuItem SpecificContextMenu = new ToolStripMenuItem("Screening [" + this.Name + "]");
            // ToolStripSeparator Sep = new ToolStripSeparator();
            // base.SpecificContextMenu.Items.Add(Sep);


            //ToolStripMenuItem ToolStripMenuItem_Info = new ToolStripMenuItem("Test Automated Menu");

            //base.SpecificContextMenu.Items.Add(ToolStripMenuItem_Info);

            ////   contextMenuStrip.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem_Info, ToolStripMenuItem_Histo, ToolStripSep, ToolStripMenuItem_Kegg, ToolStripSep1, ToolStripMenuItem_Copy });

            ////ToolStripSeparator SepratorStrip = new ToolStripSeparator();
            //// contextMenuStrip.Show(Control.MousePosition);
            //ToolStripMenuItem_Info.Click += new System.EventHandler(this.DisplayInfo);



            ToolStripMenuItem ToolStripMenuItem_CopyValuestoClipBoard = new ToolStripMenuItem("Copy Values to Clipboard");
            ToolStripMenuItem_CopyValuestoClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_CopyValuestoClipBoard);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_CopyValuestoClipBoard);

            //ToolStripMenuItem ToolStripMenuItem_CopyClassToClipBoard = new ToolStripMenuItem("Copy Classes to Clipboard");
            //ToolStripMenuItem_CopyClassToClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_CopyClassToClipBoard);
            //SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_CopyClassToClipBoard);


            ToolStripMenuItem ToolStripMenuItem_CopyPropertyToClipBoard = new ToolStripMenuItem("Copy Property to Clipboard");
            ToolStripMenuItem_CopyPropertyToClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_CopyPropertyToClipBoard);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_CopyPropertyToClipBoard);

            ToolStripMenuItem ToolStripMenuItem_ExportToExcel = new ToolStripMenuItem("Export To Excel");
            ToolStripMenuItem_ExportToExcel.Click += new System.EventHandler(this.ToolStripMenuItem_ExportToExcel);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ExportToExcel);


            SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            if (this.ListPlatesActive.GetListActiveWells().Count > 0)
            {
                SpecificContextMenu.DropDownItems.Add(this.ListPlatesActive.GetListActiveWells().GetContextMenu());
                SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());
            }

            ToolStripMenuItem ToolStripMenuItem_Info = new ToolStripMenuItem("Info");
            ToolStripMenuItem_Info.Click += new System.EventHandler(this.ToolStripMenuItem_Info);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Info);

            //base.SpecificContextMenu.DropDownItems.Add(this.ListActiveWells.GetContextMenu());


            //ToolStripMenuItem ToolStripMenuItem_CopyWellsToList = new ToolStripMenuItem("Copy Well(s) to List Wells");
            //ToolStripMenuItem_CopyWellsToList.Click += new System.EventHandler(this.ToolStripMenuItem_CopyWellsToList);
            //base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_CopyWellsToList);


            //ToolStripSeparator ToolStripSep = new ToolStripSeparator();
            //base.SpecificContextMenu.DropDownItems.Add(ToolStripSep);


            //ToolStripMenuItem ToolStripMenuItem_SetAsActivePlate = new ToolStripMenuItem("Set as Active");
            //ToolStripMenuItem_SetAsActivePlate.Click += new System.EventHandler(this.ToolStripMenuItem_SetAsActivePlate);
            //base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_SetAsActivePlate);

            //ToolStripMenuItem ToolStripMenuItem_Clustering = new ToolStripMenuItem("Cluster");
            //ToolStripMenuItem_Clustering.Click += new System.EventHandler(this.ToolStripMenuItem_Clustering);
            //base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Clustering);


            //base.SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            //ToolStripMenuItem ToolStripMenuItem_DisplayImages = new ToolStripMenuItem("Display Images");
            //ToolStripMenuItem_DisplayImages.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayImages);
            //ToolStripMenuItem_DisplayImages.Enabled = false;
            //base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayImages);

            //ToolStripMenuItem DisplayAsContextMenu = new ToolStripMenuItem("Display as...");

            //ToolStripMenuItem ToolStripMenuItem_DisplayAs2DScatterPoints = new ToolStripMenuItem("2D Scatter Points");
            //ToolStripMenuItem_DisplayAs2DScatterPoints.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayAs2DScatterPoints);
            //DisplayAsContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayAs2DScatterPoints);

            //ToolStripMenuItem ToolStripMenuItem_DisplayAsStackedHistograms = new ToolStripMenuItem("Stacked Histograms");
            //ToolStripMenuItem_DisplayAsStackedHistograms.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayAsStackedHistograms);
            //DisplayAsContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayAsStackedHistograms);

            //ToolStripMenuItem ToolStripMenuItem_DisplayDataTable = new ToolStripMenuItem("Data Table");
            //ToolStripMenuItem_DisplayDataTable.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayDataTable);
            //DisplayAsContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayDataTable);

            //DisplayAsContextMenu.DropDownItems.Add(new ToolStripSeparator());

            //ToolStripMenuItem ToolStripMenuItem_DisplayDataTableDistance = new ToolStripMenuItem("Distance Matrix (Euclidean)");
            //ToolStripMenuItem_DisplayDataTableDistance.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayDataTableDistance);
            //DisplayAsContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayDataTableDistance);

            //base.SpecificContextMenu.DropDownItems.Add(DisplayAsContextMenu);


            //base.SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            //ToolStripMenuItem ToolStripMenuItem_PlateInfo = new ToolStripMenuItem("Info");
            //ToolStripMenuItem_PlateInfo.Click += new System.EventHandler(this.ToolStripMenuItem_PlateInfo);
            //base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_PlateInfo);


            //ToolStripMenuItem ToolStripMenuItem_RemovePlate = new ToolStripMenuItem("Remove");
            //ToolStripMenuItem_RemovePlate.Click += new System.EventHandler(this.ToolStripMenuItem_RemovePlate);
            //base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_RemovePlate);




            //ToolStripMenuItem ToolStripMenuItem_Histo = new ToolStripMenuItem("Histogram");
            //ToolStripMenuItem_Histo.Click += new System.EventHandler(this.DisplayHisto);
            //SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Histo);

            //if (this.LocusID != -1.0)
            //{
            //    ToolStripMenuItem ToolStripMenuItem_Kegg = new ToolStripMenuItem("Kegg");
            //    ToolStripMenuItem_Kegg.Click += new System.EventHandler(this.DisplayPathways);
            //    SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Kegg);
            //}

            //if (this.SQLTableName != "")
            //{
            //    ToolStripSeparator ToolStripSep = new ToolStripSeparator();
            //    SpecificContextMenu.DropDownItems.Add(ToolStripSep);

            //    ToolStripMenuItem ToolStripMenuItem_DisplayData = new ToolStripMenuItem("Display Single Object Data");
            //    ToolStripMenuItem_DisplayData.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayData);
            //    SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayData);

            //    ToolStripMenuItem ToolStripMenuItem_AddToSingleCellAnalysis = new ToolStripMenuItem("Add to Single Object Analysis");
            //    ToolStripMenuItem_AddToSingleCellAnalysis.Click += new System.EventHandler(this.ToolStripMenuItem_AddToSingleCellAnalysis);
            //    SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_AddToSingleCellAnalysis);
            //}

            //ToolStripSeparator ToolStripSep1 = new ToolStripSeparator();
            //ToolStripMenuItem ToolStripMenuItem_Copy = new ToolStripMenuItem("Copy Visu.");

            //ToolStripSeparator SepratorStrip = new ToolStripSeparator();
            //base.SpecificContextMenu.Show(Control.MousePosition);
            //ToolStripMenuItem_Copy.Click += new System.EventHandler(this.CopyVisu);
            #endregion

            return SpecificContextMenu;
        }

        public List<cScreeningClassStatus> ListClassHistoryStatus = new List<cScreeningClassStatus>();

        public void SetClassStatus(int HistoryIdx)
        {
            if ((HistoryIdx > this.ListClassHistoryStatus.Count) || (HistoryIdx < 0)) return;
            CurrentHistoryClassStatusIndex = HistoryIdx;

            foreach (cPlate TmpPlate in this.ListPlatesAvailable)
            {
                foreach (cWell TmpWell in TmpPlate.ListWells)
                {
                    //if (HistoryIdx < TmpWell.ListClass.Count)

                    int NewClass = TmpWell.GetHistoricalClass(HistoryIdx);
                    if (NewClass >= -1)
                        TmpWell.SetClass(NewClass);
                    //TmpWell.SaveCurrentClassStatus(CurrentClass);
                }
            }

            this.GetCurrentDisplayPlate().DisplayDistribution(this.ListDescriptors.GetActiveDescriptor(), false);
        }

        public void SaveCurrentClassStatus()
        {
            cScreeningClassStatus SCS = new HCSAnalyzer.Classes.General_Types.cScreeningClassStatus("Class Status " + this.ListClassHistoryStatus.Count);
            ListClassHistoryStatus.Add(SCS);
            this.CurrentHistoryClassStatusIndex = this.ListClassHistoryStatus.Count;

            foreach (cPlate TmpPlate in this.ListPlatesAvailable)
            {
                foreach (cWell TmpWell in TmpPlate.ListWells)
                {
                    //int CurrentClass = TmpWell.GetCurrentClassIdx();
                    TmpWell.SaveCurrentClassStatus(/*CurrentClass*/);
                }
            }


            List<string> names = new List<string>();
            names.Add(SCS.Name);
            names.Add(SCS.Time.ToString());

            ListViewItem NewItem = new ListViewItem(names.ToArray());

            NewItem.Tag = this;
            cGlobalInfo.WindowHCSAnalyzer.listViewClassHistory.Items.Add(NewItem);
        }

        /// <summary>
        /// Return a list containing all the active wells from the active plates
        /// </summary>
        /// <returns></returns>
        public cListWells GetListWells()
        {
            cListWells ListWells = new cListWells(null);
            foreach (cPlate TmpPlate in this.ListPlatesActive)
            {
                foreach (cWell item in TmpPlate.ListActiveWells)
                {
                    if (item != null)
                        ListWells.Add(item);
                }
            }
            return ListWells;
        }

        public void Close3DView()
        {
            if (_3DWorldForPlateDisplay != null)
            {
                _3DWorldForPlateDisplay.ren1.RemoveAllViewProps();
                _3DWorldForPlateDisplay.Terminate();
                _3DWorldForPlateDisplay = null;
            }
        }

        public int GetNumberOfActiveDescriptor()
        {
            int Res = 0;
            for (int i = 0; i < this.ListDescriptors.Count; i++)
                if (this.ListDescriptors[i].IsActive() == true) Res++;

            return Res;
        }

        /// <summary>
        /// Check if at least one descriptor is checked
        /// </summary>
        /// <returns>true if at leaset one descriptor is checked</returns>
        public bool IsSelectedDescriptors()
        {
            bool ToReturn = false;
            for (int i = 0; i < this.ListDescriptors.Count; i++)
            {
                if (this.ListDescriptors[i].IsActive()) return true;
            }
            return ToReturn;
        }

        #region Weka based clustering and classification

        /// <summary>
        /// Create an instances structure without classes for unsupervised methods
        /// </summary>
        /// <returns>a weka Instances object</returns>
        public Instances CreateInstancesWithoutClass()
        {
            weka.core.FastVector atts = new FastVector();
            int columnNo = 0;

            // Descriptors loop
            for (int i = 0; i < this.ListDescriptors.Count; i++)
            {
                if (this.ListDescriptors[i].IsActive() == false) continue;
                atts.addElement(new weka.core.Attribute(this.ListDescriptors[i].GetName()));
                columnNo++;
            }
            weka.core.FastVector attVals = new FastVector();
            Instances data1 = new Instances("MyRelation", atts, 0);

            foreach (cPlate CurrentPlate in this.ListPlatesActive)
            {
                //   foreach (cWell CurrentWell in CurrentPlate.ListActiveWells)
                //  {
                for (int j = 0; j < this.Columns; j++)
                    for (int i = 0; i < this.Rows; i++)
                    {
                        cWell CurrentWell = CurrentPlate.GetWell(j, i, false);

                        if (CurrentWell == null) continue;

                        double[] vals = new double[data1.numAttributes()];
                        int IdxRealCol = 0;

                        for (int Col = 0; Col < this.ListDescriptors.Count; Col++)
                        {
                            if (this.ListDescriptors[Col].IsActive() == false) continue;
                            vals[IdxRealCol++] = CurrentWell.ListSignatures[Col].GetValue();
                        }
                        data1.add(new DenseInstance(1.0, vals));
                    }
                // }
            }
            return data1;
        }

        public cInfoForHierarchical CreateInstancesWithUniqueClasse()
        {
            cInfoForHierarchical InfoForHierarchical = new cInfoForHierarchical();

            weka.core.FastVector atts = new FastVector();
            int columnNo = 0;

            for (int i = 0; i < this.ListDescriptors.Count; i++)
            {
                if (ListDescriptors[i].IsActive() == false) continue;
                atts.addElement(new weka.core.Attribute(ListDescriptors[i].GetName()));
                columnNo++;
            }

            weka.core.FastVector attVals = new FastVector();
            atts.addElement(new weka.core.Attribute("Class", attVals));

            InfoForHierarchical.ListInstances = new Instances("MyRelation", atts, 0);
            int IdxWell = 0;
            foreach (cPlate CurrentPlate in this.ListPlatesActive)
            {
                foreach (cWell CurrentWell in CurrentPlate.ListActiveWells)
                {
                    if (CurrentWell.GetCurrentClassIdx() == -1) continue;
                    attVals.addElement("Class" + (IdxWell).ToString());
                    InfoForHierarchical.ListIndexedWells.Add(CurrentWell);
                    double[] vals = new double[InfoForHierarchical.ListInstances.numAttributes()];

                    int IdxCol = 0;
                    for (int Col = 0; Col < this.ListDescriptors.Count; Col++)
                    {
                        if (this.ListDescriptors[Col].IsActive() == false) continue;
                        vals[IdxCol++] = CurrentWell.ListSignatures[Col].GetValue();
                    }
                    vals[columnNo] = IdxWell;
                    InfoForHierarchical.ListInstances.add(new DenseInstance(1.0, vals));
                    IdxWell++;
                }
            }
            InfoForHierarchical.ListInstances.setClassIndex((InfoForHierarchical.ListInstances.numAttributes() - 1));
            return InfoForHierarchical;
        }

        /// <summary>
        /// Create an instances structure with classes for supervised methods
        /// </summary>
        /// <param name="NumClass"></param>
        /// <returns></returns>
        public Instances CreateInstancesWithClasses(cInfoClass InfoClass, int NeutralClass)
        {
            weka.core.FastVector atts = new FastVector();

            int columnNo = 0;

            for (int i = 0; i < this.ListDescriptors.Count; i++)
            {
                if (this.ListDescriptors[i].IsActive() == false) continue;
                atts.addElement(new weka.core.Attribute(this.ListDescriptors[i].GetName()));
                columnNo++;
            }

            weka.core.FastVector attVals = new FastVector();

            for (int i = 0; i < InfoClass.NumberOfClass; i++)
                attVals.addElement("Class__" + (i).ToString());

            atts.addElement(new weka.core.Attribute("Class__", attVals));

            Instances data1 = new Instances("MyRelation", atts, 0);
            int IdxWell = 0;
            foreach (cPlate CurrentPlate in this.ListPlatesActive)
            {
                foreach (cWell CurrentWell in CurrentPlate.ListActiveWells)
                {
                    if (CurrentWell.GetCurrentClassIdx() == NeutralClass) continue;
                    double[] vals = new double[data1.numAttributes()];

                    int IdxCol = 0;
                    for (int Col = 0; Col < this.ListDescriptors.Count; Col++)
                    {
                        if (this.ListDescriptors[Col].IsActive() == false) continue;
                        vals[IdxCol++] = CurrentWell.ListSignatures[Col].GetValue();
                    }
                    vals[columnNo] = InfoClass.CorrespondanceTable[CurrentWell.GetCurrentClassIdx()];
                    data1.add(new DenseInstance(1.0, vals));
                    IdxWell++;
                }
            }
            data1.setClassIndex((data1.numAttributes() - 1));
            return data1;
        }

        /// <summary>
        /// Assign a class to each well based on table
        /// </summary>
        /// <param name="ListClasses">Table containing the classes</param>
        public void AssignClass(double[] ListClasses)
        {
            int idxClass = 0;
            foreach (cPlate CurrentPlate in this.ListPlatesActive)
            {
                //foreach (cWell CurrentWell in CurrentPlate.ListActiveWells)
                //{

                for (int j = 0; j < this.Columns; j++)
                    for (int i = 0; i < this.Rows; i++)
                    {
                        cWell CurrentWell = CurrentPlate.GetWell(j, i, false);

                        if (CurrentWell == null) continue;


                        CurrentWell.SetClass((int)ListClasses[idxClass++]);
                    }
                CurrentPlate.UpDateWellsSelection();
            }
        }


        public void UpdateListActiveWell()
        {
            foreach (cPlate CurrentPlate in this.ListPlatesActive)
            {
                CurrentPlate.ListActiveWells.Clear();
                for (int j = 0; j < this.Rows; j++)
                    for (int i = 0; i < this.Columns; i++)
                    {
                        cWell TempWell = CurrentPlate.GetWell(i, j, false);
                        if ((TempWell == null) || (TempWell.GetCurrentClassIdx() == -1)) continue;
                        CurrentPlate.ListActiveWells.Add(TempWell);
                    }
            }
            return;
        }

        public cInfoDescriptors BuildInfoDesc()
        {
            // int[] ListClasses = UpdateNumberOfClass();

            cInfoDescriptors InfoDescriptors = new cInfoDescriptors();
            InfoDescriptors.CorrespondanceTable = new int[this.ListDescriptors.Count];
            int Idx = 0;
            for (int i = 0; i < InfoDescriptors.CorrespondanceTable.Length; i++)
            {
                if (this.ListDescriptors[i].IsActive())
                {
                    InfoDescriptors.CorrespondanceTable[i] = Idx++;
                    InfoDescriptors.ListBackAssociation.Add(i);
                }
                else
                    InfoDescriptors.CorrespondanceTable[i] = -1;
            }

            return InfoDescriptors;
        }
        #endregion

        public int GetNumberOfClasses()
        {
            int NumberOfPlates = this.ListPlatesActive.Count;
            int[] CompleteListClasses = new int[11];

            foreach (cPlate CurrentPlateToProcess in this.ListPlatesActive)
            {
                int[] ListClasses = CurrentPlateToProcess.UpdateNumberOfClass();
                for (int i = 0; i < ListClasses.Length; i++) CompleteListClasses[i] += ListClasses[i];
            }

            int NumberOfClasses = 0;
            for (int i = 1; i < CompleteListClasses.Length; i++)
                if (CompleteListClasses[i] > 0) NumberOfClasses++;
            return NumberOfClasses;
        }

        public int[] GetClassPopulation()
        {
            int[] ListClass = new int[cGlobalInfo.ListWellClasses.Count];
            foreach (cPlate CurrentPlateToProcess in this.ListPlatesActive)
            {
                foreach (cWell TmpWell in CurrentPlateToProcess.ListActiveWells)
                {
                    int Class = TmpWell.GetCurrentClassIdx();
                    if (Class >= 0)
                        ListClass[Class]++;
                    else
                    {
                    }
                }
            }


            return ListClass;
        }

        public int GetNumberOfActiveWells()
        {
            int TotalWells = 0;
            foreach (cPlate CurrentPlateToProcess in this.ListPlatesActive)
            {
                TotalWells += CurrentPlateToProcess.GetNumberOfActiveWells();
            }
            return TotalWells;
        }

        public cInfoClass GetNumberOfClassesBut(int NeutralClass)
        {
            NeutralClass++;
            int NumberOfPlates = this.ListPlatesActive.Count;
            int[] CompleteListClasses = new int[cGlobalInfo.ListWellClasses.Count + 1];

            foreach (cPlate CurrentPlateToProcess in this.ListPlatesActive)
            {
                int[] ListClasses = CurrentPlateToProcess.UpdateNumberOfClass();
                for (int i = 0; i < ListClasses.Length; i++) CompleteListClasses[i] += ListClasses[i];
            }

            cInfoClass InfoClass = new cInfoClass();
            InfoClass.CorrespondanceTable = new int[cGlobalInfo.ListWellClasses.Count];


            // int NumberOfClasses = 0;
            for (int i = 1; i < CompleteListClasses.Length; i++)
            {
                if ((CompleteListClasses[i] > 0) && (i != NeutralClass))
                {
                    InfoClass.CorrespondanceTable[i - 1] = InfoClass.NumberOfClass++;
                    InfoClass.ListBackAssociation.Add(i - 1);
                }
                else
                    InfoClass.CorrespondanceTable[i - 1] = -1;
            }
            return InfoClass;
        }

        public int GetNumberOfOriginalPlates()
        {
            return ListPlatesAvailable.Count;
        }

        public int GetSelectionType()
        {
            return this.SelectedClass;
        }

        public void SetSelectionType(int State)
        {
            this.SelectedClass = State;
        }

        public void AddPlate(cPlate Plate)
        {
            ListPlatesAvailable.Add(Plate);
        }

        public cPlate GetCurrentDisplayPlate()
        {
            if (ListPlatesActive == null) return null;

            return ListPlatesActive.GetPlate(CurrentDisplayPlateIdx);
        }

        /// <summary>
        /// This function tranfers all the available plates to the activated plate list
        /// </summary>
        public void UpDatePlateListWithFullAvailablePlate()
        {
            this.ListPlatesActive = new cListPlates();
            foreach (cPlate Plate in ListPlatesAvailable) this.ListPlatesActive.Add(Plate);

            // this.ListPlatesAvailable = new cListPlates();
            //  foreach (cPlate Plate in ListPlatesAvailable) this.ListPlatesAvailable.Add(Plate);

        }

        public cPlate GetPlateIfNameIsContainIn(string PlateName)
        {
            for (int Idx = 0; Idx < ListPlatesAvailable.Count; Idx++)
            {
                if (this.ListPlatesAvailable[Idx].GetName() == PlateName)
                    return ListPlatesAvailable[Idx];
            }
            return null;
        }

        public void LoadData(string Path, int col, int row)
        {
            this.Columns = col;
            this.Rows = row;

            IEnumerable<string> ListDirectories = Directory.EnumerateDirectories(Path);

            foreach (string DirectoryName in ListDirectories)
            {
                string PlateName = DirectoryName.Remove(0, DirectoryName.LastIndexOf("\\") + 1);

                cPlate CurrentPlate = new cPlate(PlateName, this);
                CurrentPlate.LoadFromDisk(DirectoryName);
                ListPlatesAvailable.Add(CurrentPlate);

            }

            //   for (int Desc = 0; Desc < this.ListDescriptors.Count; Desc++)
            //       ListDescriptors.Add(true);

            //    this.GlobalInfo.CurrentRichTextBox.AppendText(ListPlatesAvailable.Count + " plates processed");

            UpDatePlateListWithFullAvailablePlate();

        }

        #region Nested Classes
        private class CsvRow : List<string>
        {
            public string LineText { get; set; }
        }

        private class CsvFileReader : StreamReader
        {
            public CsvFileReader(Stream stream)
                : base(stream)
            {
            }

            public CsvFileReader(string filename)
                : base(filename)
            {
            }

            /// <summary>
            /// Reads a row of data from a CSV file
            /// </summary>
            /// <param name="row"></param>
            /// <returns></returns>
            public bool ReadRow(CsvRow row)
            {
                //if (row.LineText == null) return false;
                row.LineText = ReadLine();
                if (String.IsNullOrEmpty(row.LineText))
                    return false;

                int pos = 0;
                int rows = 0;

                while (pos < row.LineText.Length)
                {
                    string value;

                    // Special handling for quoted field
                    if (row.LineText[pos] == '"')
                    {
                        // Skip initial quote
                        pos++;

                        // Parse quoted value
                        int start = pos;
                        while (pos < row.LineText.Length)
                        {
                            // Test for quote character
                            if (row.LineText[pos] == '"')
                            {
                                // Found one
                                pos++;

                                // If two quotes together, keep one
                                // Otherwise, indicates end of value
                                if (pos >= row.LineText.Length || row.LineText[pos] != '"')
                                {
                                    pos--;
                                    break;
                                }
                            }
                            pos++;
                        }
                        value = row.LineText.Substring(start, pos - start);
                        value = value.Replace("\"\"", "\"");
                    }
                    else
                    {
                        // Parse unquoted value
                        int start = pos;
                        while (pos < row.LineText.Length && row.LineText[pos] != ',')
                            pos++;
                        value = row.LineText.Substring(start, pos - start);
                    }

                    // Add field to list
                    if (rows < row.Count)
                        row[rows] = value;
                    else
                        row.Add(value);
                    rows++;

                    // Eat up to and including next comma
                    while (pos < row.LineText.Length && row.LineText[pos] != ',')
                        pos++;
                    if (pos < row.LineText.Length)
                        pos++;
                }
                // Delete any unused items
                while (row.Count > rows)
                    row.RemoveAt(rows);

                // Return true if any columns read
                //return (row.Count > 0);
                return true;
            }
        }
        #endregion

        private int[] ConvertPosition(string PosString)
        {
            int[] Pos = new int[2];

            Pos[1] = Convert.ToInt16(PosString[0]) - 64;
            Pos[0] = Convert.ToInt16(PosString.Remove(0, 1));

            return Pos;
        }

        public void ImportFromMTR(string[] FileNames)
        {
            int RejectedWells = 0;
            int WellLoaded = 0;
            this.ListDescriptors.Clean();
            this.ListDescriptors.AddNew(new cDescriptorType("Descriptor", true, 1));

            ListPlatesAvailable = new cListPlates();

            for (int i = 0; i < FileNames.Length; i++)
            {
                string FileName = FileNames[i];


                StreamReader sr = new StreamReader(FileName);
                string line = sr.ReadLine();

                int Idx = line.IndexOf(" ");

                // if (Idx == -1) Idx = 0;
                string PlateNumber = line.Remove(Idx);
                int NumberOfPlate = Convert.ToInt32(PlateNumber);

                string NewLine = line.Remove(0, Idx + 1);
                line = NewLine;

                Idx = line.IndexOf(" ");
                string sRow = line.Remove(Idx);
                this.Rows = Convert.ToInt32(sRow);

                NewLine = line.Remove(0, Idx + 1);
                line = NewLine;

                this.Columns = Convert.ToInt32(line);

                string[] Sep = new string[1];
                Sep[0] = "\\";

                for (int IdxPlate = 0; IdxPlate < NumberOfPlate; IdxPlate++)
                {
                    string[] SplittedName = FileNames[i].Split(Sep, StringSplitOptions.None );
                    string SafeFileName = SplittedName[SplittedName.Length - 1]; 

                    cPlate CurrentPlate = new cPlate(SafeFileName.Remove(SafeFileName.Length - 4, 4) + "_Plate_" + IdxPlate, this);
                    this.AddPlate(CurrentPlate);
                }

                int IdxWell = 0;

                while (line != null)
                {
                    line = sr.ReadLine();
                    //IdxWell = 1;
                    int CurrentRow = IdxWell % this.Rows;
                    int CurrentCol = IdxWell / this.Rows;

                    if (line != null)
                    {
                        for (int IdxPlate = 0; IdxPlate < NumberOfPlate; IdxPlate++)
                        {
                        NEXT: ;

                            Idx = line.IndexOf("\t");
                            if (Idx != -1) NewLine = line.Remove(Idx);

                            double CurrentValue;

                            if (!double.TryParse(NewLine, out CurrentValue) || (double.IsNaN(CurrentValue)))
                            {
                                RejectedWells++;
                                goto NEXT;
                            }

                            cSignature Desc = new cSignature(CurrentValue, this.ListDescriptors[0], this);
                            cPlate CurrentPlate = ListPlatesAvailable[ListPlatesAvailable.Count - IdxPlate - 1];
                            cWell CurrentWell = new cWell(Desc, CurrentCol + 1, CurrentRow + 1, this, CurrentPlate);
                            CurrentPlate.AddWell(CurrentWell);
                            WellLoaded++;

                            NewLine = line.Remove(0, Idx + 1);
                            line = NewLine;
                        }
                    }
                    IdxWell++;
                }
                sr.Close();
            }

            UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < this.ListPlatesActive.Count; idxP++)
                ListPlatesActive[idxP].UpDataMinMax();

            MessageBox.Show("MTR file loaded:\n" + WellLoaded + " well(s) loaded\n" + RejectedWells + " well(s) rejected.", "Process finished !", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return;
        }

        public double[] ImportFromCSV(string FileName, bool IsAppend, int NumCol, int NumRow)
        {
            double[] ProcessedWell = new double[2];
            int Mode = 2;
            if (cGlobalInfo.OptionsWindow.radioButtonWellPosModeSingle.Checked) Mode = 1;
            int RejectedWells = 0;
            int WellLoaded = 0;

            if (IsAppend == false)
            {
                // FormForPlateDimensions PlateDim = new FormForPlateDimensions();
                // if (PlateDim.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                //     return null;
                this.Rows = NumRow;
                this.Columns = NumCol;
                this.ListDescriptors.CurrentSelectedDescriptorIdx = 0;
                //ListPlate = new List<cPlate>();
                ListPlatesAvailable = new cListPlates();
            }

            string namefirst = "";

            //   for (int IdxPlate = 0; IdxPlate < FileNames.Length; IdxPlate++)
            {
                CsvFileReader CSVsr = new CsvFileReader(FileName);
                List<bool> ListIsDuplicate = new List<bool>();

                #region Initialization of the descriptors list
                namefirst = CSVsr.ReadLine();
                CSVsr.Close();
                CSVsr = new CsvFileReader(FileName);
                CsvRow ListDesc = new CsvRow();
                CSVsr.ReadRow(ListDesc);

                if (IsAppend == false)
                {
                    // this.ListDescriptorName = new List<string>();
                    this.ListDescriptors.Clean();

                    //   for (int i = 1 + Mode; i < ListDesc.Count; i++) this.ListDescriptors.Add(new cDescriptorsType(ListDesc[i],true,true);
                }
                else
                {
                    for (int i = 1 + Mode; i < ListDesc.Count; i++)
                    {
                        bool IsArleadyInList = false;
                        for (int idxDesc = 0; idxDesc < this.ListDescriptors.Count; idxDesc++)
                        {
                            if (ListDesc[i] == this.ListDescriptors[idxDesc].GetName())
                            {
                                IsArleadyInList = true;
                                break;
                            }
                        }
                        if (IsArleadyInList) ListIsDuplicate.Add(IsArleadyInList);
                    }
                    if (ListIsDuplicate.Count != this.ListDescriptors.Count) return null;
                }
                #endregion

                CsvRow CurrentDesc = new CsvRow();

                while (CSVsr.EndOfStream != true)
                {
                NEXT: ;

                    if (CSVsr.ReadRow(CurrentDesc) == false) break;

                    string PlateName = CurrentDesc[0];

                    // check if the plate exist already
                    cPlate CurrentPlate = GetPlateIfNameIsContainIn(PlateName);
                    if (CurrentPlate == null)
                    {
                        CurrentPlate = new cPlate(PlateName, this);
                        this.AddPlate(CurrentPlate);
                    }

                    // Create the descriptor list and add it to the well, then add the well
                    cListSignature LDesc = new cListSignature();
                    for (int i = 1 + Mode; i < CurrentDesc.Count; i++)
                    {
                        cSignature Desc = null;
                        if (CurrentDesc[i] == null)
                        {
                            RejectedWells++;
                            goto NEXT;
                        }
                        double CurrentValue;

                        if (!double.TryParse(CurrentDesc[i], out CurrentValue) || (double.IsNaN(CurrentValue)))
                        {
                            RejectedWells++;
                            goto NEXT;
                        }

                        Desc = new cSignature(CurrentValue, this.ListDescriptors[i - (1 + Mode)], this);
                        LDesc.Add(Desc);
                    }
                    int[] Pos = new int[2];
                    if (Mode == 1)
                    {
                        Pos = ConvertPosition(CurrentDesc[1]);
                    }
                    else
                    {
                        Pos[0] = Convert.ToInt16(CurrentDesc[1]);
                        Pos[1] = Convert.ToInt16(CurrentDesc[2]);
                    }

                    cWell CurrentWell = new cWell(LDesc, Pos[0], Pos[1], this, CurrentPlate);
                    CurrentPlate.AddWell(CurrentWell);
                    WellLoaded++;
                }
                CSVsr.Close();
            }

            UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < this.ListPlatesActive.Count; idxP++)
                ListPlatesActive[idxP].UpDataMinMax();

            ProcessedWell[0] = WellLoaded;
            ProcessedWell[1] = RejectedWells;

            return ProcessedWell;
        }

        public void ImportFromTXT(string[] FileNames,int NumCol, int NumRow)
        {
            int RejectedWells = 0;
            int WellLoaded = 0;

            this.ListDescriptors.Clean();
            //  this.ListDescriptors.AddNew(new cDescriptorsType("Descriptor", true, 1));

            // ListPlate = new List<cPlate>();
            bool IsFirstLoop = true;

            ListPlatesAvailable = new cListPlates();
            for (int IdxPlate = 0; IdxPlate < FileNames.Length; IdxPlate++)
            {
                StreamReader sr = new StreamReader(FileNames[IdxPlate]);
                string line = sr.ReadLine();

                int Idx = line.IndexOf("\t");
                string TmLine = line.Remove(0, 1);
                line = TmLine;
                // this.ListDescriptorName = new List<string>();
                // this.ListDescriptors.Clean();
                if (IsFirstLoop)
                {
                    while (Idx != -1)
                    {
                        Idx = line.IndexOf("\t");
                        if (Idx != -1)
                        {
                            string DescriptorName = line.Remove(Idx, line.Length - Idx);
                            this.ListDescriptors.AddNew(new cDescriptorType(DescriptorName, true, 1));
                            TmLine = line.Remove(0, Idx + 1);
                            line = TmLine;
                        }
                        else if (line.Length > 0)
                        {
                            string DescriptorName = line;
                            this.ListDescriptors.AddNew(new cDescriptorType(DescriptorName, true, 1));
                        }
                    }
                    IsFirstLoop = false;
                }
                line = sr.ReadLine();
                this.Rows = NumRow;
                this.Columns = NumCol;
                this.ListDescriptors.CurrentSelectedDescriptorIdx = 0;


                string[] Sep = new string[1];
                Sep[0] = "\\";

                string[] SplittedName = FileNames[IdxPlate].Split(Sep, StringSplitOptions.None);
                    string SafeFileName = SplittedName[SplittedName.Length - 1];

                string PlateName = SafeFileName.Remove(SafeFileName.Length - 4);
                cPlate CurrentPlate = new cPlate(PlateName, this);
                this.AddPlate(CurrentPlate);
                int IdxWell = 0;

                string NewLine;

                while (line != null)
                {
                    if (line != null)
                    {
                        int CurrentRow = Convert.ToInt16(line[0]) - 64;
                        NewLine = line.Remove(0, 1);

                        TmLine = NewLine.Remove(2);
                        int CurrentCol = Convert.ToInt16(TmLine);
                        line = NewLine.Remove(0, 3);

                        cListSignature LDesc = new cListSignature();
                        for (int i = 0; i < this.ListDescriptors.Count; i++)
                        {
                            Idx = line.IndexOf("\t");
                            if (Idx > -1) NewLine = line.Remove(Idx);

                            double CurrentValue;

                            if ((!double.TryParse(NewLine, out CurrentValue)) || (double.IsNaN(CurrentValue)))
                            {
                                RejectedWells++;
                                goto NEXT;
                            }

                            cSignature Desc = new cSignature(Convert.ToDouble(NewLine), this.ListDescriptors[i], this);
                            LDesc.Add(Desc);

                            NewLine = line.Remove(0, Idx + 1);
                            line = NewLine;
                        }
                        cWell CurrentWell = new cWell(LDesc, CurrentCol, CurrentRow, this, ListPlatesAvailable[IdxPlate]);
                        ListPlatesAvailable[IdxPlate].AddWell(CurrentWell);
                        WellLoaded++;
                    }
                NEXT:
                    IdxWell++;
                    line = sr.ReadLine();
                }
                sr.Close();


            }
            UpDatePlateListWithFullAvailablePlate();
            for (int idxP = 0; idxP < this.ListPlatesActive.Count; idxP++)
                ListPlatesActive[idxP].UpDataMinMax();
            MessageBox.Show("TXT file loaded:\n" + WellLoaded + " well(s) loaded\n" + RejectedWells + " well(s) rejected.", "Process finished !", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return;
        }

        public void ApplyCurrentClassesToAllPlates()
        {
            for (int IdxPlate = 0; IdxPlate < this.ListPlatesActive.Count; IdxPlate++)
            {
                if (IdxPlate == this.CurrentDisplayPlateIdx) continue;
                cPlate TmpPlate = this.ListPlatesActive[IdxPlate];

                for (int col = 0; col < this.Columns; col++)
                    for (int row = 0; row < this.Rows; row++)
                    {
                        cWell CurrWellWithClass = ListPlatesActive[CurrentDisplayPlateIdx].GetWell(col, row, false);
                        if (CurrWellWithClass == null) continue;

                        cWell CurrWell = TmpPlate.GetWell(col, row, false);
                        if (CurrWell == null) continue;
                        int Clss = CurrWellWithClass.GetCurrentClassIdx();

                        if (Clss == -1)
                            CurrWell.SetAsNoneSelected();
                        else
                            CurrWell.SetClass(Clss);
                    }
            }


        }

        public string GetInfo()
        {

            string TmpText = "Name: " + this.Name+"\n\n";



            TmpText += "Plate dimensions: " + this.Columns + " x " + this.Rows + "\n\n\n";
            //richTextBoxForScreeningInformation.AppendText(TmpText);

            TmpText += "Number of plates: " + this.ListPlatesActive.Count + " (/ " + this.ListPlatesAvailable.Count + ")\n\n";
            int TotalWells = 0;
            for (int PlateIdx = 1; PlateIdx <= this.ListPlatesActive.Count; PlateIdx++)
            {
                cPlate CurrentPlateToProcess = this.ListPlatesActive.GetPlate(PlateIdx - 1);
                TmpText += "Plate " + PlateIdx + " :\t" + CurrentPlateToProcess.GetName() + "\n";

                if(CurrentPlateToProcess.DBConnection!=null)
                TmpText += "DB Name: " + CurrentPlateToProcess.DBConnection.SQLFileDBName+"\n";

                TmpText += "\t" + CurrentPlateToProcess.GetNumberOfActiveWells() + " active wells / " + CurrentPlateToProcess.GetNumberOfClasses() + " classes.\n";
                TotalWells += CurrentPlateToProcess.GetNumberOfActiveWells();
            }
            TmpText += "\n";
            //richTextBoxForScreeningInformation.AppendText(TmpText + "\n");

            TmpText += "Number of active wells: " + TotalWells+ "\n\n";
            //richTextBoxForScreeningInformation.AppendText(TmpText + "\n\n");

            TmpText += "Number of active descriptors: " + this.GetNumberOfActiveDescriptor() + " (/ " + this.ListDescriptors.Count + ")\n\n";
            for (int Desc = 1; Desc <= this.ListDescriptors.Count; Desc++)
            {
                if (this.ListDescriptors[Desc - 1].IsActive() == false) continue;
                TmpText += "Descriptor " + Desc + " :\t" + this.ListDescriptors[Desc - 1].GetName() + "\n";
            }
            TmpText += "\n";
            //richTextBoxForScreeningInformation.AppendText(TmpText + "\n");

            int[] ListClass = this.GetClassPopulation();

            TmpText += "List Classes:\n\n";
            for (int IdxClass = 0; IdxClass < ListClass.Length; IdxClass++)
            {
                TmpText += cGlobalInfo.ListWellClasses[IdxClass].Name;
                double Percent = (100 * ListClass[IdxClass]) / (double)TotalWells;
                TmpText += " : " + ListClass[IdxClass] + "\t <=>\t " + Percent.ToString("N3") + " %\n";

            }

            return TmpText;

        }
    }
}
