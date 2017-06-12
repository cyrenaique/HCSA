using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes;
using weka.core;
using weka.clusterers;
using weka.classifiers;
using LibPlateAnalysis;
using HCSAnalyzer.Forms.IO;
using System.Threading.Tasks;
using HCSAnalyzer.Forms.FormsForOptions.ClassForOptions;
using HCSAnalyzer.Forms.FormsForOptions;
using HCSAnalyzer.Forms.FormsForOptions.ClusteringInfo;
using Microsoft.Msagl.GraphViewerGdi;
using HCSAnalyzer.Forms.FormsForOptions.ClassForOptions.Children;
using HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo;
using weka.classifiers.trees;
using weka.classifiers.lazy;
using weka.classifiers.functions;
using weka.classifiers.functions.supportVector;
using weka.classifiers.rules;
using weka.classifiers.bayes;
using HCSAnalyzer.Classes.Machine_Learning;
using HCSAnalyzer.Cell_by_Cell_and_DB.Simulator.Forms;
using HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Classes.Base_Classes.Data;
using ImageAnalysis;
using ImageAnalysisFiltering;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
using HCSAnalyzer.Cell_by_Cell_and_DB;
using System.Windows.Forms.DataVisualization.Charting;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes.Base_Classes.GUI;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;

namespace HCSAnalyzer.Forms.FormsForGraphsDisplay
{
    public partial class FormForSingleCellsDisplay : Form
    {
        //private DataTable dt;
        public cExtendedTable InputTable;
        public cGlobalInfo GlobalInfo;
        public cMachineLearning MachineLearning;
        FormForModelsHistory WindowForModelHistory = new FormForModelsHistory();
        ToolTip ToolTipForX = new ToolTip();
        ToolTip ToolTipForY = new ToolTip();
        ToolTip ToolTipForVolume = new ToolTip();
        double[] SecondListClassesForValidation = null;
        FormForPointSize WindowPtSize = new FormForPointSize();
        bool IsDisplayBorder = true;
        bool IsAntialias = false;
        bool IsFastDisplay = false;
        FormForSingleSlider SliderForOpacity = new FormForSingleSlider("Marker Opacity");
        public int Opacity = 255;

        public double MaxX, MaxY, MinX, MinY;
        public cListWells AssociatedListWells = null;

        public PanelForClassSelection PanelPhenotypeSelection = null;

        ToolStripMenuItem SpecificContextMenu;

        cExtendedList ListVolumes;

        static cViewerStackedHistogram VSH;


        void PanelPhenotypeSelection_SelectionChanged(object sender, EventArgs e)
        {
            PanelForClassSelection PFCS = ((PanelForClassSelection)sender);
            this.ListPhenotypeVisible = PFCS.GetListSelectedClass();
            this.ReFreshMainGraph();
            this.RedrawHistoHorizontal(true);
            UpdateText();
        }

        public void UpdateText()
        {
            this.Text = AssociatedListWells.Count + " selected wells - " + this.GetActiveSignatures(false)[0].Count + " / " + this.InputTable[0].Count + " points.";
        }

        public FormForSingleCellsDisplay(cExtendedTable InputTable, cExtendedList ListClasses)
        {
            InitializeComponent();

            SliderForOpacity.numericUpDown.Maximum = 255;
            SliderForOpacity.numericUpDown.Value = this.Opacity;

            this.InputTable = InputTable;

            // build the panel for the phenotypic selection
            PanelPhenotypeSelection = new PanelForClassSelection(true, eClassType.PHENOTYPE);
            PanelPhenotypeSelection.SelectionChanged += new PanelForClassSelection.ChangedEventHandler(PanelPhenotypeSelection_SelectionChanged);
            PanelPhenotypeSelection.Height = this.panelForClasses.Height;
            this.panelForClasses.Controls.Add(PanelPhenotypeSelection);


            #region initialize histograms display
            splitContainerHorizontal.Panel1Collapsed = true;
            splitContainerVertical.Panel2Collapsed = true;

            System.Drawing.Image ImageOriginal = (System.Drawing.Image)(Properties.Resources.Arrow);
            if (splitContainerVertical.Panel2Collapsed)
                ImageOriginal.RotateFlip(RotateFlipType.Rotate270FlipNone);
            else
                ImageOriginal.RotateFlip(RotateFlipType.Rotate90FlipNone);

            buttonCollapseVertical.BackgroundImage = ImageOriginal;
            #endregion

            MachineLearning = new cMachineLearning(GlobalInfo);

            MachineLearning.Classes.AddRange(ListClasses);

            WindowForModelHistory.Show();
            WindowForModelHistory.Visible = false;

            ToolTipForX.AutoPopDelay = ToolTipForY.AutoPopDelay = 5000;
            ToolTipForX.InitialDelay = ToolTipForY.InitialDelay = 500;
            ToolTipForX.ReshowDelay = ToolTipForY.ReshowDelay = 500;
            ToolTipForX.ShowAlways = ToolTipForY.ShowAlways = true;
            ToolTipForX.SetToolTip(comboBoxAxeX, comboBoxAxeX.Text);
            ToolTipForY.SetToolTip(comboBoxAxeY, comboBoxAxeY.Text);
            ToolTipForVolume.SetToolTip(comboBoxVolume, comboBoxVolume.Text);

            this.chartForPoints.ChartAreas[0].CursorX.Interval = double.Epsilon;
            this.chartForPoints.ChartAreas[0].CursorY.Interval = double.Epsilon;

            this.chartForPoints.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AssociatedChart_MouseClick);
            for (int i = 0; i < cGlobalInfo.ListCellularPhenotypes.Count; i++)
            {
                this.ListPhenotypeVisible.Add(true);
            }

        }

        public ToolStripMenuItem GetContextMenu()
        {
            SpecificContextMenu = new ToolStripMenuItem("Change Phenotype");

            ToolStripMenuItem ToolStripMenuItem_SwapPhenotype = new ToolStripMenuItem("Swap Phenotype");
            ToolStripMenuItem_SwapPhenotype.Click += new System.EventHandler(this.ToolStripMenuItem_SwapPhenotype);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_SwapPhenotype);

            for (int i = 0; i < cGlobalInfo.ListCellularPhenotypes.Count; i++)
            {
                ToolStripMenuItem ToolStripMenuItem_ChangePhenotype = new ToolStripMenuItem(cGlobalInfo.ListCellularPhenotypes[i].Name);
                ToolStripMenuItem_ChangePhenotype.Click += new System.EventHandler(this.ToolStripMenuItem_ChangePhenotype);
                ToolStripMenuItem_ChangePhenotype.Tag = i;
                ToolStripMenuItem_ChangePhenotype.BackColor = cGlobalInfo.ListCellularPhenotypes[i].ColourForDisplay;
                SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChangePhenotype);
            }


            if (MachineLearning.CurrentClassifier != null)
            {
                SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

                ToolStripMenuItem ToolStripMenuItem_ApplyCurrentModel = new ToolStripMenuItem("Apply Current Model");
                ToolStripMenuItem_ApplyCurrentModel.Click += new System.EventHandler(this.ToolStripMenuItem_ApplyCurrentModel);
                SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ApplyCurrentModel);
            }
            return this.SpecificContextMenu;
        }

        private void AssociatedChart_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1) return;
            if (e.Button != MouseButtons.Right) return;

            //NewMenu.DropShadowEnabled = true;
            ContextMenuStrip NewMenu = buildMenu(e);
            NewMenu.Show(Control.MousePosition);

        }

        ContextMenuStrip buildMenu(MouseEventArgs e)
        {
            ContextMenuStrip NewMenu = new ContextMenuStrip();

            #region Display context menu
            ToolStripMenuItem DisplayContextMenu = new ToolStripMenuItem("Display");

            ToolStripMenuItem ToolStripMenuItem_MarkerSize = new ToolStripMenuItem("Marker Size");
            ToolStripMenuItem_MarkerSize.Click += new System.EventHandler(this.pointSizeToolStripMenuItem_Click);
            DisplayContextMenu.DropDownItems.Add(ToolStripMenuItem_MarkerSize);

            ToolStripMenuItem ToolStripMenuItem_MarkerColor = new ToolStripMenuItem("Phenotype Colors");
            ToolStripMenuItem_MarkerColor.Click += new System.EventHandler(this.colorsToolStripMenuItem_Click);
            DisplayContextMenu.DropDownItems.Add(ToolStripMenuItem_MarkerColor);

            ToolStripMenuItem ToolStripMenuItem_MarkerOpacity = new ToolStripMenuItem("Opacity");
            ToolStripMenuItem_MarkerOpacity.Click += new System.EventHandler(this.ToolStripMenuItem_MarkerOpacity);
            DisplayContextMenu.DropDownItems.Add(ToolStripMenuItem_MarkerOpacity);

            ToolStripMenuItem ToolStripMenuItem_MarkerBorder = new ToolStripMenuItem("Marker Border");
            ToolStripMenuItem_MarkerBorder.Click += new System.EventHandler(this.markerBorderToolStripMenuItem_Click);
            ToolStripMenuItem_MarkerBorder.CheckOnClick = true;
            ToolStripMenuItem_MarkerBorder.Checked = IsDisplayBorder;
            DisplayContextMenu.DropDownItems.Add(ToolStripMenuItem_MarkerBorder);

            ToolStripMenuItem ToolStripMenuItem_Antialiasing = new ToolStripMenuItem("Anti-aliasing");
            ToolStripMenuItem_Antialiasing.Click += new System.EventHandler(this.ToolStripMenuItem_Antialiasing);
            ToolStripMenuItem_Antialiasing.CheckOnClick = true;
            ToolStripMenuItem_Antialiasing.Checked = IsAntialias;
            DisplayContextMenu.DropDownItems.Add(ToolStripMenuItem_Antialiasing);

            ToolStripMenuItem ToolStripMenuItem_FastDisplay = new ToolStripMenuItem("Fast Display");
            ToolStripMenuItem_FastDisplay.Click += new System.EventHandler(this.ToolStripMenuItem_FastDisplay);
            ToolStripMenuItem_FastDisplay.CheckOnClick = true;
            ToolStripMenuItem_FastDisplay.Checked = IsFastDisplay;
            DisplayContextMenu.DropDownItems.Add(ToolStripMenuItem_FastDisplay);


            //DisplayContextMenu.DropDownItems.Add(new ToolStripSeparator());

            //ToolStripMenuItem ToolStripMenuItem_PhenotypeVisibility = new ToolStripMenuItem("Phenotypes");

            //for (int i = 0; i < cGlobalInfo.ListCellularPhenotypes.Count; i++)
            //{
            //    ToolStripMenuItem ToolStripMenuItem_CurrentPhenoVisibility = new ToolStripMenuItem(cGlobalInfo.ListCellularPhenotypes[i].Name);
            //    ToolStripMenuItem_CurrentPhenoVisibility.Tag = i;
            //    ToolStripMenuItem_CurrentPhenoVisibility.Click += new System.EventHandler(this.ToolStripMenuItem_CurrentPhenoVisibility);
            //    ToolStripMenuItem_CurrentPhenoVisibility.CheckOnClick = true;
            //    ToolStripMenuItem_CurrentPhenoVisibility.Checked = ListPhenotypeVisible[i];
            //    ToolStripMenuItem_CurrentPhenoVisibility.BackColor = cGlobalInfo.ListCellularPhenotypes[i].ColourForDisplay;
            //    ToolStripMenuItem_PhenotypeVisibility.DropDownItems.Add(ToolStripMenuItem_CurrentPhenoVisibility);
            //}


            //DisplayContextMenu.DropDownItems.Add(ToolStripMenuItem_PhenotypeVisibility);



            NewMenu.Items.Add(DisplayContextMenu);
            #endregion

            #region Analysis context menu
            ToolStripMenuItem AnalysisContextMenu = new ToolStripMenuItem("Analysis");

            ToolStripMenuItem ToolStripMenuItem_AnalysisCorrelationMatrix = new ToolStripMenuItem("Correlation Matrix");
            ToolStripMenuItem_AnalysisCorrelationMatrix.Click += new System.EventHandler(this.correlationMatrixToolStripMenuItem_Click);
            AnalysisContextMenu.DropDownItems.Add(ToolStripMenuItem_AnalysisCorrelationMatrix);

            ToolStripMenuItem ToolStripMenuItem_AnalysisLDA = new ToolStripMenuItem("LDA");
            ToolStripMenuItem_AnalysisLDA.Click += new System.EventHandler(this.ToolStripMenuItem_AnalysisLDA);
            AnalysisContextMenu.DropDownItems.Add(ToolStripMenuItem_AnalysisLDA);

            ToolStripMenuItem ToolStripMenuItem_AnalysisDisplayDataTable = new ToolStripMenuItem("Display Data Table");
            ToolStripMenuItem_AnalysisDisplayDataTable.Click += new System.EventHandler(this.ToolStripMenuItem_AnalysisDisplayDataTable);
            AnalysisContextMenu.DropDownItems.Add(ToolStripMenuItem_AnalysisDisplayDataTable);

            NewMenu.Items.Add(AnalysisContextMenu);
            #endregion

            NewMenu.Items.Add(this.GetContextMenu());

            #region Selection process
            MaxX = this.chartForPoints.ChartAreas[0].CursorX.SelectionEnd;
            MinX = this.chartForPoints.ChartAreas[0].CursorX.SelectionStart;

            if (MaxX < MinX)
            {
                MinX = this.chartForPoints.ChartAreas[0].CursorX.SelectionEnd;
                MaxX = this.chartForPoints.ChartAreas[0].CursorX.SelectionStart;
            }

            MaxY = this.chartForPoints.ChartAreas[0].CursorY.SelectionEnd;
            MinY = this.chartForPoints.ChartAreas[0].CursorY.SelectionStart;

            if (MaxY < MinY)
            {
                MinY = this.chartForPoints.ChartAreas[0].CursorY.SelectionEnd;
                MaxY = this.chartForPoints.ChartAreas[0].CursorY.SelectionStart;
            }

            if ((MinX < MaxX) && (MaxY > MinY))
            {
                NewMenu.Items.Add(new ToolStripSeparator());

                ToolStripMenuItem ToolStripMenuItem_Zoom = new ToolStripMenuItem("Zoom");
                ToolStripMenuItem_Zoom.Click += new System.EventHandler(this.ToolStripMenuItem_Zoom);
                NewMenu.Items.Add(ToolStripMenuItem_Zoom);

                NewMenu.Items.Add(new ToolStripSeparator());

                ToolStripMenuItem ToolStripMenuItem_ModifySelection = new ToolStripMenuItem("Modify Selection");
                ToolStripMenuItem_ModifySelection.Click += new System.EventHandler(this.ToolStripMenuItem_ModifySelection);
                NewMenu.Items.Add(ToolStripMenuItem_ModifySelection);

            }
            #endregion

            //if(this.MachineLearning.Classes
            if (SecondListClassesForValidation != null)
            {

                NewMenu.Items.Add(new ToolStripSeparator());
                ToolStripMenuItem ToolStripMenuItem_ApplyTraining = new ToolStripMenuItem("Apply Training");
                ToolStripMenuItem_ApplyTraining.Click += new System.EventHandler(this.ToolStripMenuItem_ApplyTraining);
                NewMenu.Items.Add(ToolStripMenuItem_ApplyTraining);

                ToolStripMenuItem ToolStripMenuItem_ResetTraining = new ToolStripMenuItem("Reset Training");
                ToolStripMenuItem_ResetTraining.Click += new System.EventHandler(this.ToolStripMenuItem_ResetTraining);
                NewMenu.Items.Add(ToolStripMenuItem_ResetTraining);


            }

            NewMenu.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_CopyToClipBoard = new ToolStripMenuItem("Copy To Clipboard");
            ToolStripMenuItem_CopyToClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_CopyToClipBoard);
            NewMenu.Items.Add(ToolStripMenuItem_CopyToClipBoard);

            #region manage context menu on the graph elements
            HitTestResult Res = null;

            if(e!=null)
                Res = this.chartForPoints.HitTest(e.X, e.Y, ChartElementType.DataPoint);

            if ((Res!=null)&&(Res.Series != null))
            {
                DataPoint PtToTransfer = Res.Series.Points[Res.PointIndex];
                //Res.Series.Tag

                if (PtToTransfer.Tag != null)
                {
                    //if (PtToTransfer.Tag.GetType() == typeof(cWell))
                    //{
                    //    cWell TmpWell = (cWell)(PtToTransfer.Tag);
                    //    //foreach (var item in TmpWell.GetExtendedContextMenu())
                    //    //    ToBeReturned.Add(item);
                    //}
                    if (PtToTransfer.Tag.GetType() == typeof(cSingleBiologicalObject))
                    {
                        cSingleBiologicalObject TmpBiologicalObject = (cSingleBiologicalObject)(PtToTransfer.Tag);
                        foreach (var item in TmpBiologicalObject.GetExtendedContextMenu())
                            NewMenu.Items.Add(item);
                    }
                    //if (PtToTransfer.Tag.GetType() == typeof(cDescriptorType))
                    //{
                    //    cDescriptorType TmpDesc = (cDescriptorType)(PtToTransfer.Tag);
                    //    foreach (var itemDesc in TmpDesc.GetExtendedContextMenu())
                    //        ToBeReturned.Add(itemDesc);
                    //}
                    //if (PtToTransfer.Tag.GetType() == typeof(cPlate))
                    //{
                    //    cPlate TmpPlate = (cPlate)(PtToTransfer.Tag);
                    //    ToBeReturned.Add(TmpPlate.GetExtendedContextMenu());
                    //}
                    //if (PtToTransfer.Tag.GetType() == typeof(cWellClassType))
                    //{
                    //    cWellClassType TmpClass = (cWellClassType)(PtToTransfer.Tag);
                    //    ToBeReturned.Add(TmpClass.GetExtendedContextMenu());
                    //}
                    //if (PtToTransfer.Tag.GetType() == typeof(cCellularPhenotype))
                    //{
                    //    cCellularPhenotype TmpCellularPhenotype = (cCellularPhenotype)(PtToTransfer.Tag);
                    //    //foreach (var item in TmpCellularPhenotype.GetContextMenu())
                    //    ToBeReturned.Add(TmpCellularPhenotype.GetContextMenu());
                    //}

                    //ToolStripMenuItem TSMI = new ToolStripMenuItem("Serie [" + Res.Series.Name + "]");

                    //ToolStripMenuItem ToolStripMenuItem_DisplaySerieData = new ToolStripMenuItem("Display Data Table");
                    //ToolStripMenuItem_DisplaySerieData.Tag = Res;
                    //ToolStripMenuItem_DisplaySerieData.Click += new System.EventHandler(this.ToolStripMenuItem_DisplaySerieData);
                    //TSMI.DropDownItems.Add(ToolStripMenuItem_DisplaySerieData);


                    //ToolStripMenuItem ToolStripMenuItem_CopyToclipboardSerieData = new ToolStripMenuItem("Copy to Clipboard");
                    //ToolStripMenuItem_CopyToclipboardSerieData.Tag = Res;
                    //ToolStripMenuItem_CopyToclipboardSerieData.Click += new System.EventHandler(this.ToolStripMenuItem_CopyToclipboardSerieData);
                    //TSMI.DropDownItems.Add(ToolStripMenuItem_CopyToclipboardSerieData);

                    ////TSMI.DropDownItems.Add(new ToolStripSeparator());

                    //ToBeReturned.Add(TSMI);

                }
            }
            #endregion


            return NewMenu;
        }

        private void ToolStripMenuItem_ApplyTraining(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MachineLearning.Classes.Count; i++)
            {
                this.MachineLearning.Classes[i] = this.SecondListClassesForValidation[i];
            }
            this.SecondListClassesForValidation = null;
            ReDraw();
        }

        private void ToolStripMenuItem_ResetTraining(object sender, EventArgs e)
        {
            this.SecondListClassesForValidation = null;
            ReDraw();
        }

        private void ToolStripMenuItem_CopyToClipBoard(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            this.chartForPoints.SaveImage(ms, ChartImageFormat.Bmp);
            Bitmap bm = new Bitmap(ms);
            Clipboard.SetImage(bm);
        }

        List<bool> ListPhenotypeVisible = new List<bool>();

        //private void ToolStripMenuItem_CurrentPhenoVisibility(object sender, EventArgs e)
        //{
        //    int SelectedIdx = (int)(((ToolStripMenuItem)sender).Tag);
        //    if (ListPhenotypeVisible[SelectedIdx]) ListPhenotypeVisible[SelectedIdx] = false;
        //    else ListPhenotypeVisible[SelectedIdx] = true;

        //    this.ReFreshMainGraph();
        //}

        private void ToolStripMenuItem_ApplyCurrentModel(object sender, EventArgs e)
        {
            Instances InstancesList = cGlobalInfo.CurrentScreening.CellBasedClassification.CreateInstancesWithoutClass(/*dt*/this.InputTable);

            FastVector attValsWithoutClasses = new FastVector();


            for (int i = 0; i < cGlobalInfo.ListCellularPhenotypes.Count; i++)
                attValsWithoutClasses.addElement(cGlobalInfo.ListCellularPhenotypes[i].Name);


            InstancesList.insertAttributeAt(new weka.core.Attribute("Class", attValsWithoutClasses), InstancesList.numAttributes());
            //int A = Classes.Count;
            for (int i = 0; i < MachineLearning.Classes.Count; i++)
                InstancesList.get(i).setValue(InstancesList.numAttributes() - 1, MachineLearning.Classes[i]);

            InstancesList.setClassIndex(InstancesList.numAttributes() - 1);




            // Instances ListInstancesTOClassify = ListInstances;
            SecondListClassesForValidation = new double[InstancesList.numInstances()];
            //// ListInstances.setClassIndex(ListInstances.numAttributes() - 1);
            for (int i = 0; i < InstancesList.numInstances(); i++)
            {
                SecondListClassesForValidation[i] = MachineLearning.CurrentClassifier.classifyInstance(InstancesList.instance(i));
            }

            buttonClassify.Enabled = true;
            ReDraw();


        }

        private void ToolStripMenuItem_SwapPhenotype(object sender, EventArgs e)
        {
            cGUI_2ClassesSelection GUI_ListClasses = new cGUI_2ClassesSelection();
            GUI_ListClasses.ClassType = eClassType.PHENOTYPE;
            GUI_ListClasses.PanelLeft_IsCheckBoxes = true;
            GUI_ListClasses.PanelRight_IsCheckBoxes = false;
            GUI_ListClasses.Title = "Swap Phenotypes";
            if (GUI_ListClasses.Run(this.GlobalInfo).IsSucceed == false) return;

            int IdxNewPhenotype = 0;
            for (int IdxPheno = 0; IdxPheno < GUI_ListClasses.GetOutPut()[0].Count; IdxPheno++)
            {
                if (GUI_ListClasses.GetOutPut()[1][IdxPheno] == 1)
                    IdxNewPhenotype = IdxPheno;
            }

            int IDxPt = 0;
            foreach (DataPoint item in this.chartForPoints.Series[0].Points)
            {
                //    if ((item.XValue >= MinX) && (item.XValue <= MaxX) && (item.YValues[0] >= MinY) && (item.YValues[0] <= MaxY))
                //    {
                //  if ((item.Tag != null) && (item.Tag.GetType() == typeof(cSingleBiologicalObject)))
                {
                    if (GUI_ListClasses.GetOutPut()[0][(int)MachineLearning.Classes[IDxPt]/*((cSingleBiologicalObject)(item.Tag)).GetAssociatedPhenotype().Idx*/] == 1)
                        MachineLearning.Classes[IDxPt] = IdxNewPhenotype;


                    //            LDP.Add(item);
                }
                //    }

                IDxPt++;
            }

            this.ReFreshMainGraph();
            this.RedrawHistoHorizontal(true);
            /*
            int Idx = 0;


            // int OriginalIdx = WindowSwapClasses.comboBoxOriginalClass.SelectedIndex - 1;
            int DestinatonIdx = WindowSwapClasses.comboBoxDestinationClass.SelectedIndex - 1;

            // loop on all the plate
            for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
            {
                cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(PlateIdx);

                for (int IdxValue = 0; IdxValue < cGlobalInfo.CurrentScreening.Columns; IdxValue++)
                    for (int IdxValue0 = 0; IdxValue0 < cGlobalInfo.CurrentScreening.Rows; IdxValue0++)
                    {
                        cWell TmpWell = CurrentPlateToProcess.GetWell(IdxValue, IdxValue0, false);
                        if (TmpWell == null) continue;

                        if ((TmpWell.GetCurrentClassIdx() > -1) && (ClassSelectionPanel.GetListSelectedClass()[TmpWell.GetCurrentClassIdx()]))
                        {
                            if (DestinatonIdx == -1)
                                TmpWell.SetAsNoneSelected();
                            else
                                TmpWell.SetClass(DestinatonIdx);

                            Idx++;
                        }

                    }
            }
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
            MessageBox.Show(Idx + " wells have been swapped !", "Process over !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
             * */
        }

        private void ToolStripMenuItem_ChangePhenotype(object sender, EventArgs e)
        {

            //cGUI_ListClasses GUIClasses = null;

            ////if (Control.ModifierKeys == Keys.Control)
            //{
            //    GUIClasses = new cGUI_ListClasses();
            //    GUIClasses.IsCheckBoxes = true;
            //    GUIClasses.IsSelectAll = true;
            //    GUIClasses.ClassType = eClassType.PHENOTYPE;
            //    if (GUIClasses.Run().IsSucceed == false) return;
            //}

            double MaxX = this.chartForPoints.ChartAreas[0].CursorX.SelectionEnd;
            double MinX = this.chartForPoints.ChartAreas[0].CursorX.SelectionStart;

            if (MaxX < MinX)
            {
                MinX = this.chartForPoints.ChartAreas[0].CursorX.SelectionEnd;
                MaxX = this.chartForPoints.ChartAreas[0].CursorX.SelectionStart;
            }

            double MaxY = this.chartForPoints.ChartAreas[0].CursorY.SelectionEnd;
            double MinY = this.chartForPoints.ChartAreas[0].CursorY.SelectionStart;

            if (MaxY < MinY)
            {
                MinY = this.chartForPoints.ChartAreas[0].CursorY.SelectionEnd;
                MaxY = this.chartForPoints.ChartAreas[0].CursorY.SelectionStart;
            }

            //  List<DataPoint> LDP = new List<DataPoint>();
            //  cListSingleBiologicalObjects LSBO = new cListSingleBiologicalObjects();

            int IDxPt = 0;
            int IdxNewPhenotype = -1;

            for (int i = 0; i < cGlobalInfo.ListCellularPhenotypes.Count; i++)
            {
                if (cGlobalInfo.ListCellularPhenotypes[i].Name == sender.ToString())
                {
                    IdxNewPhenotype = i;
                    break;
                }
            }

            if (IdxNewPhenotype == -1) return;

            foreach (DataPoint item in this.chartForPoints.Series[0].Points)
            {
                if ((item.XValue >= MinX) && (item.XValue <= MaxX) && (item.YValues[0] >= MinY) && (item.YValues[0] <= MaxY))
                {
                    // if ((item.Tag != null) && (item.Tag.GetType() == typeof(cSingleBiologicalObject)))
                    {
                        // LSBO.Add((cSingleBiologicalObject)(item.Tag));
                        //  cSingleBiologicalObject TmpObject = (cSingleBiologicalObject)(item.Tag);
                        //LSBO[LSBO.Count-1].GetAssociatedPhenotype().

                        if (!this.PanelPhenotypeSelection.GetListSelectedClass()[(int)MachineLearning.Classes[IDxPt]])
                        //  if ((GUIClasses == null) || (GUIClasses.GetOutPut()[0][(int)MachineLearning.Classes[IDxPt]] == 0))
                        {
                            IDxPt++;
                            continue;
                        }
                        MachineLearning.Classes[IDxPt] = IdxNewPhenotype;


                        // LDP.Add(item);
                    }
                }

                IDxPt++;
            }

            //ReDraw();
            this.ReFreshMainGraph();
            this.RedrawHistoHorizontal(true);
        }

        public void ReFreshMainGraph()
        {
            int BorderSize = 1;

            //  this.chartForPoints.
            if (!IsDisplayBorder) BorderSize = 0;

            if (MachineLearning.Classes.Count > 0)
            {
                for (int j = 0; j < this.chartForPoints.Series[0].Points.Count; j++)
                {
                    //int ConvertedValue = (int)(((Classes[j] - 0) * (LUT[0].Length - 1)) / (eval.getNumClusters() - 0));
                    //  this.chartForPoints.Series[0].Points[j].MarkerColor = GlobalInfo.ListCellularPhenotypes[(int)MachineLearning.Classes[j]].ColourForDisplay;
                    int ClassId = (int)MachineLearning.Classes[j];
                    if ((ClassId >= 0) && ListPhenotypeVisible[ClassId])
                    {
                        this.chartForPoints.Series[0].Points[j].MarkerColor = Color.FromArgb(this.Opacity, cGlobalInfo.ListCellularPhenotypes[ClassId].ColourForDisplay);
                        this.chartForPoints.Series[0].Points[j].MarkerBorderWidth = BorderSize;
                        //this.chartForPoints.Series[0].Points[j].Tag = new cSingleBiologicalObject(
                    }
                    else
                    {
                        this.chartForPoints.Series[0].Points[j].MarkerColor = Color.FromArgb(0, cGlobalInfo.ListCellularPhenotypes[ClassId].ColourForDisplay);
                        this.chartForPoints.Series[0].Points[j].MarkerBorderWidth = BorderSize;
                    }
                }
                if (checkBoxIsVolumeConstant.Checked)
                {
                    for (int j = 0; j < this.chartForPoints.Series[0].Points.Count; j++)
                    {
                        this.chartForPoints.Series[0].Points[j].MarkerSize = WindowPtSize.trackBarPointSize.Value;
                    }
                }

                if (SecondListClassesForValidation != null)
                {
                    // int NumBadAssociation = 0;

                    for (int j = 0; j < this.chartForPoints.Series[0].Points.Count; j++)
                    {
                        int ClassificationClass = (int)SecondListClassesForValidation[j];
                        this.chartForPoints.Series[0].Points[j].MarkerBorderColor = cGlobalInfo.ListCellularPhenotypes[ClassificationClass].ColourForDisplay;
                        this.chartForPoints.Series[0].Points[j].MarkerBorderWidth = BorderSize * (this.chartForPoints.Series[0].Points[j].MarkerSize / 3);

                        //if (SecondListClassesForValidation[j] != Classes[j])
                        //    NumBadAssociation++;
                    }
                    //  this.richTextBoxForResults.AppendText(NumBadAssociation + " bad associations");
                }
            }
            else
            {
                for (int j = 0; j < this.chartForPoints.Series[0].Points.Count; j++)
                {
                    //int ConvertedValue = (int)(((Classes[j] - 0) * (LUT[0].Length - 1)) / (eval.getNumClusters() - 0));

                    int WellClass = 0; // int.Parse(dt.Rows[j][dt.Columns.Count - 1].ToString());

                    //this.chartForPoints.Series[0].Points[j].MarkerColor = ;
                    this.chartForPoints.Series[0].Points[j].MarkerColor = Color.FromArgb(128, cGlobalInfo.ListWellClasses[WellClass].ColourForDisplay);
                    this.chartForPoints.Series[0].Points[j].MarkerSize = WindowPtSize.trackBarPointSize.Value;
                }

                if (checkBoxIsVolumeConstant.Checked)
                    for (int j = 0; j < this.chartForPoints.Series[0].Points.Count; j++)
                        this.chartForPoints.Series[0].Points[j].MarkerSize = WindowPtSize.trackBarPointSize.Value;
            }
            //this.chartForPoints.ChartAreas[0].CursorX.IsUserEnabled = false;
            //this.chartForPoints.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            //this.chartForPoints.ChartAreas[0].CursorY.IsUserEnabled = false;
            //this.chartForPoints.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;


            //        this.chartForPoints.Series.SuspendUpdates();
            //        this.chartForPoints.Series.ResumeUpdates();

            //  this.chartForPoints.Series[0].ChartType = SeriesChartType.FastPoint;

            //    this.chartForPoints.Series[0].IsXValueIndexed = false;


            if (!checkBoxIsVolumeConstant.Checked)
            {
                double MaxVolume = ListVolumes.Max();
                double MinVolume = ListVolumes.Min();

                for (int j = 0; j < this.InputTable[0].Count/* dt.Rows.Count*/; j++)
                {
                    int MarkerArea = (int)((50 * (ListVolumes[j] - MinVolume)) / (MaxVolume - MinVolume));
                    this.chartForPoints.Series[0].Points[j].MarkerSize = MarkerArea + 3;
                }
            }

            if (IsAntialias)
                this.chartForPoints.AntiAliasing = AntiAliasingStyles.All;
            else
                this.chartForPoints.AntiAliasing = AntiAliasingStyles.None;

            if (IsFastDisplay)
                this.chartForPoints.Series[0].ChartType = SeriesChartType.FastPoint;
            else
                this.chartForPoints.Series[0].ChartType = SeriesChartType.Point;


        }

        public void ReDraw()
        {
            cExtendedList ListX = new cExtendedList();
            cExtendedList ListY = new cExtendedList();

            if (this.comboBoxAxeY.SelectedIndex == -1) return;
            if (this.comboBoxVolume.SelectedIndex == -1) return;

            ListVolumes = new cExtendedList();

            for (int j = 0; j < InputTable[0].Count; j++)
            {
                ListX.Add(InputTable[this.comboBoxAxeX.SelectedIndex][j]);
                ListY.Add(InputTable[this.comboBoxAxeY.SelectedIndex][j]);
                ListVolumes.Add(InputTable[this.comboBoxVolume.SelectedIndex][j]);
            }

            this.chartForPoints.ChartAreas[0].AxisX.Title = this.comboBoxAxeX.SelectedItem.ToString();
            this.chartForPoints.ChartAreas[0].AxisY.Title = this.comboBoxAxeY.SelectedItem.ToString();
            this.chartForPoints.Series[0].Points.DataBindXY(ListX, ListY);

            this.chartForPoints.ChartAreas[0].AxisX.Minimum = ListX.Min();
            this.chartForPoints.ChartAreas[0].AxisX.Maximum = ListX.Max();
            if (this.chartForPoints.ChartAreas[0].AxisX.Minimum == this.chartForPoints.ChartAreas[0].AxisX.Maximum)
                this.chartForPoints.ChartAreas[0].AxisX.Maximum += 1;


            this.chartForPoints.ChartAreas[0].AxisY.Minimum = ListY.Min();
            this.chartForPoints.ChartAreas[0].AxisY.Maximum = ListY.Max();
            if (this.chartForPoints.ChartAreas[0].AxisY.Minimum == this.chartForPoints.ChartAreas[0].AxisY.Maximum)
                this.chartForPoints.ChartAreas[0].AxisY.Maximum += 1;

            //for (int j = 0; j < dt.Rows.Count; j++)
            //    this.chartForPoints.Series[0].Points[j].Tag = dataGridViewForTable.Rows[j];//dt.Rows[j];

            this.chartForPoints.ChartAreas[0].AxisX.LabelStyle.Format = "N2";
            this.chartForPoints.ChartAreas[0].AxisY.LabelStyle.Format = "N2";


            this.chartForPoints.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            this.chartForPoints.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

            this.chartForPoints.ChartAreas[0].CursorX.LineWidth = 1;
            this.chartForPoints.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.Dash;
            this.chartForPoints.ChartAreas[0].CursorY.LineWidth = 1;
            this.chartForPoints.ChartAreas[0].CursorY.LineDashStyle = ChartDashStyle.Dash;
            this.chartForPoints.ChartAreas[0].CursorX.LineColor = Color.Black;
            this.chartForPoints.ChartAreas[0].CursorY.LineColor = Color.Black;
            //this.chartForPoints.ChartAreas[0].CursorX.IntervalType = DateTimeIntervalType.
            //this.chartForPoints.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.Dash;

            this.chartForPoints.ChartAreas[0].CursorX.Interval = double.Epsilon;// (ListX.Max() - ListX.Min()) / 1000.0;
            this.chartForPoints.ChartAreas[0].CursorY.Interval = double.Epsilon;// (ListY.Max() - ListY.Min()) / 1000.0;

            this.chartForPoints.ChartAreas[0].CursorX.SelectionColor = Color.Tomato;
            this.chartForPoints.ChartAreas[0].CursorY.SelectionColor = Color.Tomato;

            this.chartForPoints.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            this.chartForPoints.ChartAreas[0].AxisY.ScaleView.Zoomable = false;

            byte[][] LUT = cGlobalInfo.CurrentPlateLUT;

            ReFreshMainGraph();

            if (this.InputTable.ListTags != null)
            {
                for (int j = 0; j < this.chartForPoints.Series[0].Points.Count; j++)
                    this.chartForPoints.Series[0].Points[j].Tag = this.InputTable.ListTags[j];
            }

        }

        private void comboBoxAxeX_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReDraw();

            if (comboBoxAxeX.Text == "")
                ToolTipForX.SetToolTip(comboBoxAxeX, comboBoxAxeX.Items[0].ToString());
            else
                ToolTipForX.SetToolTip(comboBoxAxeX, comboBoxAxeX.Text);

            if (splitContainerVertical.Panel2Collapsed) return;
            RedrawHistoHorizontal(false);
        }

        private void comboBoxAxeY_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReDraw();
            if (comboBoxAxeY.Text == "")
                ToolTipForY.SetToolTip(comboBoxAxeY, comboBoxAxeY.Items[0].ToString());
            else
                ToolTipForY.SetToolTip(comboBoxAxeY, comboBoxAxeY.Text);

            RedrawHistoVertical();
        }

        private void RedrawHistoVertical()
        {
            cExtendedList ListValue = new cExtendedList();

            for (int Idx = 0; Idx < this.InputTable[0].Count; Idx++)
            {
                double TmpValue = this.InputTable[comboBoxAxeY.SelectedIndex][Idx];
                if ((TmpValue <= this.chartForPoints.ChartAreas[0].AxisY.Maximum) && (TmpValue >= this.chartForPoints.ChartAreas[0].AxisY.Minimum))
                    ListValue.Add(TmpValue);
            }

            cPanelHisto PanelHisto = new cPanelHisto(ListValue, eGraphType.HISTOGRAM, eOrientation.VERTICAL);
            PanelHisto.WindowForPanelHisto.UserEnable = false;
            PanelHisto.WindowForPanelHisto.panelForGraphContainer.BackColor = Color.White;
            PanelHisto.WindowForPanelHisto.panelForGraphContainer.Size = new System.Drawing.Size(splitContainerHorizontal.Panel1.Width, splitContainerVertical.Panel1.Height);
            PanelHisto.WindowForPanelHisto.panelForGraphContainer.Location = new Point(0, 0);

            PanelHisto.WindowForPanelHisto.panelForGraphContainer.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            PanelHisto.WindowForPanelHisto.panelForGraphContainer.BorderStyle = BorderStyle.None;

            PanelHisto.WindowForPanelHisto.CurrentChartArea.AxisX.Minimum = chartForPoints.ChartAreas[0].AxisY.Minimum;
            PanelHisto.WindowForPanelHisto.CurrentChartArea.AxisX.Maximum = chartForPoints.ChartAreas[0].AxisY.Maximum;

            splitContainerHorizontal.Panel1.Controls.Clear();
            splitContainerHorizontal.Panel1.Controls.Add(PanelHisto.WindowForPanelHisto.panelForGraphContainer);

        }

        private void RedrawHistoHorizontal(bool KeepBinning)
        {
            cExtendedTable FinalTable = new cExtendedTable();

            int Idx = 0;
            foreach (var item in cGlobalInfo.ListCellularPhenotypes)
            {
                FinalTable.Add(new cExtendedList());
                FinalTable[Idx].Name = item.Name;
                FinalTable[Idx].Tag = item;
                Idx++;
            }

            int IdxPt = 0;
            for (int j = 0; j < this.chartForPoints.Series[0].Points.Count; j++)
            {
                cSingleBiologicalObject TmpObj = (cSingleBiologicalObject)this.chartForPoints.Series[0].Points[j].Tag;

                if (this.ListPhenotypeVisible[(int)MachineLearning.Classes[j]])
                {
                    FinalTable[(int)MachineLearning.Classes[j]/*TmpObj.GetAssociatedPhenotype().Idx*/].Add(this.chartForPoints.Series[0].Points[j].XValue);
                    IdxPt++;
                }
            }

            FinalTable.Name = "Stacked Histogram: " + IdxPt + " points";

            int CurrentNumberOfBin = 100;

            if (KeepBinning && (VSH != null))
            {
                CurrentNumberOfBin = VSH.Chart.BinNumber;// (int)VSH.ListProperties.FindByName("Bin Number").GetValue();
            }


            VSH = new cViewerStackedHistogram();
            VSH.SetInputData(FinalTable);//new cExtendedTable(ListValue));
            VSH.Chart.LabelAxisY = "";
            VSH.Chart.IsBorder = false;
            VSH.Chart.IsShadow = false;
            VSH.Chart.BackgroundColor = Color.White;
            VSH.Chart.DefaultAxisXMin = new cExtendedList(this.chartForPoints.ChartAreas[0].AxisX.Minimum);
            VSH.Chart.DefaultAxisXMax = new cExtendedList(this.chartForPoints.ChartAreas[0].AxisX.Maximum);

            if (KeepBinning)
            {
                VSH.ListProperties.FindByName("Bin Number").SetNewValue(CurrentNumberOfBin);
            }

            VSH.Run();

            splitContainerVertical.Panel2.Controls.Clear();


            cExtendedControl TextEC = VSH.GetOutPut();
            VSH.Chart.Width = 0;// splitContainerVertical.Panel2.Width - 23;
            VSH.Chart.Height = 0;// splitContainerVertical.Panel2.Height;
            TextEC.Width = splitContainerVertical.Panel2.Width - 23;
            TextEC.Height = splitContainerVertical.Panel2.Height;
            TextEC.Location = new Point(23, 0);

            VSH.Chart.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                    | System.Windows.Forms.AnchorStyles.Left
                    | System.Windows.Forms.AnchorStyles.Right);

            TextEC.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                    | System.Windows.Forms.AnchorStyles.Left
                    | System.Windows.Forms.AnchorStyles.Right);



            //VSH.Chart.Width = splitContainerVertical.Panel2.Width - 23;
            //VSH.Chart.Height = splitContainerVertical.Panel2.Height;
            splitContainerVertical.Panel2.Controls.Add(TextEC);

            return;

            //cPanelHisto PanelHisto = new cPanelHisto(ListValue, eGraphType.HISTOGRAM, eOrientation.HORIZONTAL);
            //PanelHisto.WindowForPanelHisto.UserEnable = false;
            //PanelHisto.WindowForPanelHisto.panelForGraphContainer.Size = new System.Drawing.Size(splitContainerVertical.Panel2.Width - 23, splitContainerVertical.Panel2.Height);
            //PanelHisto.WindowForPanelHisto.panelForGraphContainer.BackColor = Color.White;
            //PanelHisto.WindowForPanelHisto.panelForGraphContainer.Location = new Point(23, 0);

            //PanelHisto.WindowForPanelHisto.panelForGraphContainer.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            //PanelHisto.WindowForPanelHisto.panelForGraphContainer.BorderStyle = BorderStyle.None;

            //PanelHisto.WindowForPanelHisto.CurrentChartArea.AxisX.Minimum = chartForPoints.ChartAreas[0].AxisX.Minimum;
            //PanelHisto.WindowForPanelHisto.CurrentChartArea.AxisX.Maximum = chartForPoints.ChartAreas[0].AxisX.Maximum;

            //splitContainerVertical.Panel2.Controls.Clear();
            //splitContainerVertical.Panel2.Controls.Add(PanelHisto.WindowForPanelHisto.panelForGraphContainer);
        }

        private void buttonStartCluster_Click(object sender, EventArgs e)
        {
            // check if all the data point are visible
            if (PanelPhenotypeSelection.GetListIndexSelectedClass().Count != cGlobalInfo.ListCellularPhenotypes.Count)
            {
                // if not, display a warning message.
                if (MessageBox.Show("The clustering will be performed only on " + PanelPhenotypeSelection.GetListIndexSelectedClass().Count + " classes !\nThis can generate unexpected results.\nDo you want continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                    return;
            }

            this.Cursor = Cursors.WaitCursor;

            // -------------------------- Clustering -------------------------------

            cParamAlgo ParaAlgo = MachineLearning.AskAndGetClusteringAlgo();
            if (ParaAlgo == null)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            MachineLearning.SelectedClusterer = MachineLearning.BuildClusterer(ParaAlgo, this.GetActiveSignatures(false));

            //if (MachineLearning.SelectedClusterer == null)
            //{
            //    MessageBox.Show("Clustering failed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.Cursor = Cursors.Default;
            //    return;
            //}

            if (MachineLearning.SelectedClusterer.numberOfClusters() > cGlobalInfo.ListCellularPhenotypes.Count)
            {
                MessageBox.Show("Number of identifed clusters (" + MachineLearning.SelectedClusterer.numberOfClusters() + ") not handled by the application !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
                return;
            }
            SecondListClassesForValidation = null;

            if (MachineLearning.SelectedClusterer != null)
            {
                double[] Assign = MachineLearning.EvaluteAndDisplayClusterer(this.richTextBoxForResults,
                                        this.panelForGraphicalResults,
                                        MachineLearning.CreateInstancesWithoutClass(this.InputTable)).getClusterAssignments();

                //   MachineLearning.Classes = new cExtendedList();

                for (int i = 0; i < MachineLearning.Classes.Count; i++)
                {
                    // if the point is active then change its class
                    if (this.ListPhenotypeVisible[(int)MachineLearning.Classes[i]])
                    {
                        MachineLearning.Classes[i] = Assign[i];
                    }

                }

                //MachineLearning.Classes.AddRange(Assign);

            }
            buttonTraining.Enabled = true;
            // ReDraw();
            this.ReFreshMainGraph();
            this.RedrawHistoHorizontal(true);
            this.Cursor = Cursors.Default;

        }

        private void buttonTraining_Click(object sender, EventArgs e)
        {
            // ----------------------- Training ------------------------------
            //if (MessageBox.Show("Do you want perform a j48 training process ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes) return;

            //InstancesList = GlobalInfo.CurrentScreen.CellBasedClassification.CreateInstancesWithoutClass(dt);

            weka.classifiers.Evaluation ModelEvaluation = null;

            Instances InstancesList = cGlobalInfo.CurrentScreening.CellBasedClassification.CreateInstancesWithoutClass(/*dt*/this.InputTable);

            FormForClassificationInfo WinClassifInfo = MachineLearning.AskAndGetClassifAlgo();
            if (WinClassifInfo == null)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            MachineLearning.PerformTraining(WinClassifInfo,
                                            InstancesList,
                //MachineLearning.NumberOfClusters,
                                            this.richTextBoxForResults,
                                            this.panelForGraphicalResults,
                                            out ModelEvaluation, true);

            if (MachineLearning.CurrentClassifier == null) return;

            #region Add Object to history (if the model has been cross validated)
            if (ModelEvaluation != null)
            {
                cClusteringObject NewObjectFOrHistory = new cClusteringObject(MachineLearning.CurrentClassifier, ModelEvaluation, (int)WinClassifInfo.numericUpDownFoldNumber.Value);

                List<string> ListNamesForItem = new List<string>();
                ListNamesForItem.Add(WinClassifInfo.GetSelectedAlgoAndParameters().Name);
                ListNamesForItem.Add(NewObjectFOrHistory.FoldNumber.ToString());
                ListNamesForItem.Add(NewObjectFOrHistory.Evaluation.meanAbsoluteError().ToString());
                ListViewItem NewItem = new ListViewItem(ListNamesForItem.ToArray());

                NewItem.Tag = NewObjectFOrHistory;
                WindowForModelHistory.listViewForClassifHistory.Items.Add(NewItem);
                NewItem.ToolTipText = MachineLearning.CurrentClassifier.ToString();
            }
            #endregion

            //Instances ListInstancesTOClassify = ListInstances;
            SecondListClassesForValidation = new double[InstancesList.numInstances()];
            //// ListInstances.setClassIndex(ListInstances.numAttributes() - 1);
            for (int i = 0; i < InstancesList.numInstances(); i++)
            {
                SecondListClassesForValidation[i] = MachineLearning.CurrentClassifier.classifyInstance(InstancesList.instance(i));
            }

            buttonClassify.Enabled = true;

            this.ReFreshMainGraph();
            this.RedrawHistoHorizontal(true);
            //ReDraw();

            this.Cursor = Cursors.Default;
        }

        private void buttonClassify_Click(object sender, EventArgs e)
        {
            // FormForCellbyCellClassif WindowFormForCellbyCellClassif = new FormForCellbyCellClassif();

            //  if (WindowFormForCellbyCellClassif.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            MachineLearning.PerformClassification();
        }

        //private cExtendedTable ExtractCellsValuesList(bool SelectedDescriptorsOnly)
        //{
        //    int NumDesc = dt.Columns.Count;

        //    if (SelectedDescriptorsOnly)
        //    {
        //        // int NumberOfPlates = CompleteScreening.ListPlatesActive.Count;
        //        cExtendedTable ListValueDesc = new cExtendedTable();

        //        //new List<double>[GlobalInfo.CurrentScreen.ListDescriptors.GetListNameActives().Count];

        //        for (int i = 0; i < GlobalInfo.CurrentScreen.ListDescriptors.GetListNameActives().Count; i++)
        //            ListValueDesc[i] = new cExtendedList();

        //        // loop on all the plate
        //        for (int RowIdx = 0; RowIdx < dt.Rows.Count; RowIdx++)
        //        {
        //            int Idx = 0;
        //            for (int ColIdx = 0; ColIdx < dt.Columns.Count; ColIdx++)
        //                if (GlobalInfo.CurrentScreen.ListDescriptors[ColIdx].IsActive())
        //                {
        //                    ListValueDesc[Idx++].Add((double)dt.Rows[RowIdx][ColIdx]);
        //                }
        //        }
        //        return ListValueDesc;
        //    }

        //    else
        //    {
        //        // int NumberOfPlates = CompleteScreening.ListPlatesActive.Count;
        //        cExtendedTable ListValueDesc = new cExtendedTable();

        //        for (int i = 0; i < NumDesc; i++) ListValueDesc.Add(new cExtendedList());
        //        for (int i = 0; i < NumDesc; i++) ListValueDesc[i].Name = dt.Columns[i].ColumnName;

        //        // loop on all the plate
        //        for (int RowIdx = 0; RowIdx < dt.Rows.Count; RowIdx++)
        //        {
        //            for (int ColIdx = 0; ColIdx < dt.Columns.Count; ColIdx++)
        //                ListValueDesc[ColIdx].Add((double)dt.Rows[RowIdx][ColIdx]);
        //        }
        //        return ListValueDesc;
        //    }
        //}

        private void pointSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.chartForPoints.Series[0].Points.Count == 0) return;
            int CurrentMarkerSize = this.chartForPoints.Series[0].Points[0].MarkerSize;
            WindowPtSize.labelForPointSize.Text = CurrentMarkerSize.ToString();

            WindowPtSize.trackBarPointSize.Value = CurrentMarkerSize;

            if (WindowPtSize.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var item in this.chartForPoints.Series[0].Points)
                    item.MarkerSize = WindowPtSize.trackBarPointSize.Value;
            }
            WindowPtSize.Visible = false;
        }

        private void cleanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxForResults.Clear();
        }

        private void colorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cListOptions ListOptions = new cListOptions(GlobalInfo);

            FormForGlobalInfoOptions WindowForOptions = new FormForGlobalInfoOptions(ListOptions);
            WindowForOptions.SelectOption("Cellular Phenotypes");
            if (WindowForOptions.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // WindowForOptions.Close();
                ReDraw();

            }

            this.ReFreshMainGraph();
            this.RedrawHistoHorizontal(true);
            //ReDraw();

        }

        private void ToolStripMenuItem_ModifySelection(object sender, EventArgs e)
        {
            FormManualGating FMG = new FormManualGating(this);
            FMG.textBoxDesc1.Text = comboBoxAxeX.Text;
            FMG.textBoxDesc2.Text = comboBoxAxeY.Text;

            double MinX = this.chartForPoints.ChartAreas[0].CursorX.SelectionStart;
            double MaxX = this.chartForPoints.ChartAreas[0].CursorX.SelectionEnd;
            if (MinX > MaxX)
            {
                MaxX = this.chartForPoints.ChartAreas[0].CursorX.SelectionStart;
                MinX = this.chartForPoints.ChartAreas[0].CursorX.SelectionEnd;
            }

            FMG.numericUpDownDesc1Min.Value = (decimal)MinX;
            FMG.numericUpDownDesc1Max.Value = (decimal)MaxX;
            FMG.numericUpDownDesc1Min.Minimum = FMG.numericUpDownDesc1Max.Minimum = (decimal)this.chartForPoints.ChartAreas[0].AxisX.Minimum;
            FMG.numericUpDownDesc1Min.Maximum = FMG.numericUpDownDesc1Max.Maximum = (decimal)this.chartForPoints.ChartAreas[0].AxisX.Maximum;


            MinX = this.chartForPoints.ChartAreas[0].CursorY.SelectionStart;
            MaxX = this.chartForPoints.ChartAreas[0].CursorY.SelectionEnd;
            if (MinX > MaxX)
            {
                MaxX = this.chartForPoints.ChartAreas[0].CursorY.SelectionStart;
                MinX = this.chartForPoints.ChartAreas[0].CursorY.SelectionEnd;
            }

            FMG.numericUpDownDesc2Min.Value = (decimal)MinX;
            FMG.numericUpDownDesc2Max.Value = (decimal)MaxX;
            FMG.numericUpDownDesc2Min.Minimum = FMG.numericUpDownDesc2Max.Minimum = (decimal)this.chartForPoints.ChartAreas[0].AxisY.Minimum;
            FMG.numericUpDownDesc2Min.Maximum = FMG.numericUpDownDesc2Max.Maximum = (decimal)this.chartForPoints.ChartAreas[0].AxisY.Maximum;

            FMG.ShowDialog();
        }

        private void ToolStripMenuItem_Zoom(object sender, EventArgs e)
        {
            this.chartForPoints.ChartAreas[0].AxisX.Minimum = MinX;
            this.chartForPoints.ChartAreas[0].AxisX.Maximum = MaxX;
            this.chartForPoints.ChartAreas[0].AxisY.Minimum = MinY;
            this.chartForPoints.ChartAreas[0].AxisY.Maximum = MaxY;
            this.chartForPoints.Update();
            RedrawHistoHorizontal(true);
            RedrawHistoVertical();
        }

        private void ToolStripMenuItem_MarkerOpacity(object sender, EventArgs e)
        {
            if (this.SliderForOpacity.ShowDialog() != DialogResult.OK) return;
            this.Opacity = (int)this.SliderForOpacity.numericUpDown.Value;

            this.ReFreshMainGraph();

            //this.ReDraw();

            //for (int j = 0; j < this.input.Count; j++)
            //foreach (var item in this.chartForPoints.Series[0].Points)
            //{
            //    Color C = item.Color;
            //    item.Color = Color.FromArgb(this.Opacity, C);
            //}

            //this.chartForPoints.Update();
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowForModelHistory.Visible = true;
        }

        private void checkBoxIsVolumeConstant_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxVolume.Enabled = !checkBoxIsVolumeConstant.Checked;
            this.ReFreshMainGraph();
            //ReDraw();
        }

        private void comboBoxVolume_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReDraw();

            if (comboBoxVolume.Text == "")
                ToolTipForVolume.SetToolTip(comboBoxVolume, comboBoxVolume.Items[0].ToString());
            else
                ToolTipForVolume.SetToolTip(comboBoxVolume, comboBoxVolume.Text);

        }

        private void buttonCollapseVertical_Click(object sender, EventArgs e)
        {
            splitContainerVertical.Panel2Collapsed = !splitContainerVertical.Panel2Collapsed;

            if ((!splitContainerVertical.Panel2Collapsed) && (splitContainerVertical.Panel2.Controls.Count == 0))
            {
                RedrawHistoHorizontal(true);
            }

            System.Drawing.Image ImageOriginal = (System.Drawing.Image)(Properties.Resources.Arrow);
            if (splitContainerVertical.Panel2Collapsed)
            {
                ImageOriginal.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            else
            {
                ImageOriginal.RotateFlip(RotateFlipType.Rotate90FlipNone);
                RedrawHistoHorizontal(true);
            }
            buttonCollapseVertical.BackgroundImage = ImageOriginal;
        }

        private void buttonCollapseHorizontal_Click(object sender, EventArgs e)
        {
            splitContainerHorizontal.Panel1Collapsed = !splitContainerHorizontal.Panel1Collapsed;
            System.Drawing.Image ImageOriginal = (System.Drawing.Image)(Properties.Resources.Arrow);
            if (splitContainerHorizontal.Panel1Collapsed)
            {
                // ImageOriginal.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            else
            {
                ImageOriginal.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            buttonCollapseHorizontal.BackgroundImage = ImageOriginal;
        }

        private void chartForPoints_Resize(object sender, EventArgs e)
        {
            if (splitContainerHorizontal.Panel1.Controls.Count == 0) return;
            splitContainerHorizontal.Panel1.Controls[0].Size = new System.Drawing.Size(splitContainerHorizontal.Panel1.Width, splitContainerVertical.Panel1.Height);

        }

        private void markerBorderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsDisplayBorder = !IsDisplayBorder;// markerBorderToolStripMenuItem.Checked;
            this.ReFreshMainGraph();
        }

        private void ToolStripMenuItem_FastDisplay(object sender, EventArgs e)
        {
            IsFastDisplay = !IsFastDisplay;// markerBorderToolStripMenuItem.Checked;
            this.ReFreshMainGraph();
        }

        private void ToolStripMenuItem_Antialiasing(object sender, EventArgs e)
        {
            IsAntialias = !IsAntialias;// markerBorderToolStripMenuItem.Checked;
            this.ReFreshMainGraph();
        }

        private void correlationMatrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cDisplayCorrelationMatrix ComputeAndDisplay_SingleCorrelationMatrix = new cDisplayCorrelationMatrix();
            ComputeAndDisplay_SingleCorrelationMatrix.SetInputData(this.GetActiveSignatures(false));
            ComputeAndDisplay_SingleCorrelationMatrix.Run();
        }

        public cExtendedTable GetActiveSignatures(bool IsIncludeClasses)
        {
            cExtendedTable FinalTable = new cExtendedTable();
            int Idx = 0;
            foreach (var item in this.InputTable)
            {
                FinalTable.Add(new cExtendedList());
                FinalTable[Idx].Name = item.Name;
                FinalTable[Idx].Tag = item.Tag;
                Idx++;
            }

            int IdxPt = 0;

            if (IsIncludeClasses)
            {
                FinalTable.Add(new cExtendedList());
                FinalTable[Idx].Name = "Phenotype";

                for (int j = 0; j < this.chartForPoints.Series[0].Points.Count; j++)
                {
                    if (this.ListPhenotypeVisible[(int)MachineLearning.Classes[j]])
                    {
                        int i = 0;
                        for (i = 0; i < this.InputTable.Count; i++)
                            FinalTable[i].Add(this.InputTable[i][j]);

                        FinalTable[i].Add(MachineLearning.Classes[j]);

                        IdxPt++;
                    }
                }
            }
            else
            {
                for (int j = 0; j < this.chartForPoints.Series[0].Points.Count; j++)
                {
                    if (this.ListPhenotypeVisible[(int)MachineLearning.Classes[j]])
                    {
                        for (int i = 0; i < this.InputTable.Count; i++)
                            FinalTable[i].Add(this.InputTable[i][j]);

                        IdxPt++;
                    }
                }
            }
            FinalTable.Name = "Phenotypes: " + IdxPt + " points";
            return FinalTable;
        }

        private void ToolStripMenuItem_AnalysisLDA(object sender, EventArgs e)
        {
            int NumActiveClasses = 0;

            foreach (var item in this.ListPhenotypeVisible)
            {
                if (item) NumActiveClasses++;
            }

            if (NumActiveClasses < 2)
            {
                MessageBox.Show("At least two classes have to be selected to perfom a LDA.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cDisplayToWindow vD = new cDisplayToWindow();

            cExtendedTable NewTable = this.GetActiveSignatures(true);

            cProjectorLDA LDA = new cProjectorLDA();
            LDA.SetInputData(NewTable);
            cFeedBackMessage FM = LDA.Run();
            if (!FM.IsSucceed)
            {
                MessageBox.Show(FM.Message, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cViewerTable VHM = new cViewerTable();
            cExtendedTable CT = LDA.GetOutPut();

            foreach (var item in CT)
            {
                cDescriptorsLinearCombination DLC = new cDescriptorsLinearCombination(item);
                foreach (cDescriptorType Desc in item.ListTags)
                    DLC.Add(Desc);
                item.Tag = DLC;
            }
            VHM.SetInputData(CT);
            VHM.Run();

            vD.SetInputData(VHM.GetOutPut());
            vD.Title = "LDA - " + NumActiveClasses + " phenotypes : " + NewTable[0].Count + " points";

            vD.Run();
            vD.Display();
        }

        private void ToolStripMenuItem_AnalysisDisplayDataTable(object sender, EventArgs e)
        {
            cDisplayExtendedTable DET = new cDisplayExtendedTable();
            DET.SetInputData(this.GetActiveSignatures(false));
            DET.DigitNumber = -1;
            DET.Run();
        }

        private void displayDensityMapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormForSubPopulationId WindowForPopID = new FormForSubPopulationId(this);
            WindowForPopID.Show();
        }

        [Serializable]
        public class cMyClassifierModel
        {
            public Classifier ClassifModel;
            public List<string> ListDescNames = new List<string>();
        }

        private void saveCurrentModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MachineLearning.CurrentClassifier == null)
            {
                MessageBox.Show("No Model Avalaible !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SaveFileDialog CurrSavefileDialog = new SaveFileDialog();
            CurrSavefileDialog.Filter = "Classification Model files (*.model)|*.model";
            DialogResult Res = CurrSavefileDialog.ShowDialog();
            if (Res != DialogResult.OK) return;

            if (CurrSavefileDialog.FileName == "") return;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(CurrSavefileDialog.FileName,
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);


            cMyClassifierModel MyModel = new cMyClassifierModel();
            MyModel.ClassifModel = MachineLearning.CurrentClassifier;
            foreach (var item in this.comboBoxAxeX.Items)
            {
                MyModel.ListDescNames.Add(item.ToString());
            }

            formatter.Serialize(stream, MyModel);
            stream.Close();
        }

        private void loadModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog CurrOpenFileDialog = new OpenFileDialog();


            CurrOpenFileDialog.Filter = "Classification Model files (*.model)|*.model";
            DialogResult Res = CurrOpenFileDialog.ShowDialog();
            if (Res != DialogResult.OK) return;

            if (CurrOpenFileDialog.FileName == "") return;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(CurrOpenFileDialog.FileName,
                                      FileMode.Open,
                                      FileAccess.Read,
                                      FileShare.Read);

            cMyClassifierModel obj = (cMyClassifierModel)formatter.Deserialize(stream);
            stream.Close();

            if (this.comboBoxAxeX.Items.Count != obj.ListDescNames.Count)
            {
                string ListDescRequired = obj.ListDescNames.Count + " descriptors are required:\n";
                foreach (var item in obj.ListDescNames)
                {
                    ListDescRequired += item + "\n";
                }

                MessageBox.Show(ListDescRequired, "Descriptor list does not match !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int IdxDesc = 0;
            foreach (var item in this.comboBoxAxeX.Items)
            {
                if (obj.ListDescNames[IdxDesc++] != item.ToString())
                {
                    MessageBox.Show("Descriptor list does not match !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            #region Display associated info and graphs

            this.richTextBoxForResults.Clear();
            this.richTextBoxForResults.AppendText(obj.ClassifModel.ToString());

            if (obj.ClassifModel.GetType() == typeof(J48))
            {
                GViewer GraphView = MachineLearning.DisplayTree(GlobalInfo, (J48)obj.ClassifModel, true).gViewerForTreeClassif;
                GraphView.Size = new System.Drawing.Size(this.panelForGraphicalResults.Width, this.panelForGraphicalResults.Height);
                GraphView.Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                this.panelForGraphicalResults.Controls.Clear();
                this.panelForGraphicalResults.Controls.Add(GraphView);

            }

            this.MachineLearning.CurrentClassifier = obj.ClassifModel;

            Instances ListInstancesTOClassify = cGlobalInfo.CurrentScreening.CellBasedClassification.CreateInstancesWithoutClass(/*dt*/this.InputTable);

            SecondListClassesForValidation = null;

            FastVector attVals = new FastVector();
            for (int i = 0; i < MachineLearning.NumberOfClusters; i++)
                attVals.addElement(i.ToString());

            ListInstancesTOClassify.insertAttributeAt(new weka.core.Attribute("Class", attVals), ListInstancesTOClassify.numAttributes());
            ListInstancesTOClassify.setClassIndex(ListInstancesTOClassify.numAttributes() - 1);

            for (int i = 0; i < ListInstancesTOClassify.numInstances(); i++)
            {
                this.MachineLearning.Classes[i] = MachineLearning.CurrentClassifier.classifyInstance(ListInstancesTOClassify.instance(i));
            }

            buttonClassify.Enabled = true;
            ReDraw();

            #endregion
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cDisplayText DT = new cDisplayText();

            string infoToDisp = "";

            if (AssociatedListWells != null)
            {
                foreach (var item in this.AssociatedListWells)
                {
                    infoToDisp += item.GetShortInfo() + "\n";
                }
            }

            infoToDisp += "\n" + AssociatedListWells.Count + " selected wells - " + this.GetActiveSignatures(false)[0].Count + " / " + this.InputTable[0].Count + " points.";

            DT.Title = "Single cell analysis info";
            DT.SetInputData(infoToDisp);
            DT.Run();


        }

        private void processToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            processToolStripMenuItem.DropDownItems.Clear();
            ToolStripItemCollection TSIC = this.buildMenu(null).Items;

            for (int i = 0; i < TSIC.Count; i++)
            {
                 processToolStripMenuItem.DropDownItems.Add(TSIC[i]);
            }
        }

    }
}
