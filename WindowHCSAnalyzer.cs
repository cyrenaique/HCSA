#region Version Header
/************************************************************************
 *		$Id: Window.cs, 177+1 2010/10/20 08:28:39 HongKee $
 *		$Description: Plugin Template for IM 3.0 $
 *		$Author: HongKee $
 *		$Date: 2010/10/20 08:28:39 $
 *		$Revision: 177+1 $
 /************************************************************************/
#endregion

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using LibPlateAnalysis;
//using weka.core;
using System.IO;
using HCSAnalyzer.Forms;
using HCSAnalyzer.Controls;
using System.Reflection;
using System.Collections;
using HCSAnalyzer.Classes;
using System.Resources;
using HCSPlugin;
using HCSAnalyzer.Forms.FormsForOptions;
using HCSAnalyzer.Forms.FormsForDRCAnalysis;
using analysis;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using System.Linq;
//using Emgu.CV;
//using Emgu.CV.Structure;
//using Emgu.CV.CvEnum;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Data.SQLite;
using HCSAnalyzer.Forms.ClusteringForms;
using System.Diagnostics;
using System.Threading.Tasks;
using HCSAnalyzer.Forms.IO;
using ImageAnalysis;
using HCSAnalyzer.Forms.FormsForGraphsDisplay.ForClassSelection;
using HCSAnalyzer.Simulator.Classes;
using HCSAnalyzer.Simulator.Forms;
using HCSAnalyzer.Forms.FormsForOptions.ClassForOptions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using HCSAnalyzer.Classes.Machine_Learning;
using HCSAnalyzer.Forms.FormsForOptions.ClassForOptions.Children;
using HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.Base_Classes.Data;
//using RDotNet;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Classes.Base_Classes.GUI;
using HCSAnalyzer.Classes.DataAnalysis;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes.Base_Classes.Viewers._1D;
using System.Net;
using HCSAnalyzer.Cell_by_Cell_and_DB;
using ImageAnalysisFiltering;
using HCSAnalyzer.Classes.ImageAnalysis._3D_Engine;
using HCSAnalyzer.Classes._3D;
using HCSAnalyzer.Classes.ImageAnalysis._3D_Engine.Detection;
using IM3_Plugin3;
using HCSAnalyzer.Classes.Base_Classes.Viewers._3D.ComplexObjects;
using eUtils;
using HCSAnalyzer.Classes.Base_Components.GUI;
using HCSAnalyzer.Forms.ComplexForms;
using System.Globalization;
using HCSAnalyzer.Classes.ImageAnalysis.FormsForImages;
using System.Data.OleDb;
using Kitware.VTK;
using HCSAnalyzer.Cell_by_Cell_and_DB.Simulator.Classes;
//using FolderBrowser;

//using HCSAnalyzer.Classes._3D;
//////////////////////////////////////////////////////////////////////////
// If you want to change Menu & Name of plugin
// Go to "Properties->Resources" in Solution Explorer
// Change Menu & Name
//
// You can also use your own Painter & Mouse event handler
// 
//////////////////////////////////////////////////////////////////////////

namespace HCSAnalyzer
{
    public partial class HCSAnalyzer : Form
    {

        Boolean bHaveMouse;
        //static cScreening CurrentScreening;
        public PlatesListForm PlateListWindow;
        cGlobalInfo GlobalInfo;
        CheckBox checkBoxDisplayClasses = new CheckBox();
        CheckBox checkBoxApplyTo_AllPlates = new CheckBox();

        ContextMenuStrip contextMenuStripStatOptions = new ContextMenuStrip();
        ToolStripMenuItem _StatCVItem;
        ToolStripMenuItem _StatMeanItem;
        ToolStripMenuItem _StatSumItem;
        ToolStripMenuItem _StatJarqueBeraItem;

        ContextMenuStrip ContextMenuStripDescEvolOptions = new ContextMenuStrip();
        ToolStripMenuItem _DescEvolGlobalItem;
        ToolStripMenuItem _DescEvolCellByCellItem;

        ContextMenuStrip ContextMenuStripQCOptions = new ContextMenuStrip();
        ToolStripMenuItem _QCZRegularItem;
        ToolStripMenuItem _QCZRobustItem;


        TrackBar TrackBarForZoom = new TrackBar();


        public HCSAnalyzer()
        {
            InitializeComponent();



            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;

            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.radioButtonDimRedUnsupervised, "Unsupervised feature selection.\nThese approaches use all the active wells as data for the dimensionality reduction.");
            toolTip1.SetToolTip(this.radioButtonDimRedSupervised, "Supervised feature selection.\nThese approaches have learning porcess based on the well classes except for the neutral class.");

            comboBoxMethodForCorrection.SelectedIndex = 0;
            comboBoxCLassificationMethod.SelectedIndex = 0;
            comboBoxNeutralClassForClassif.SelectedIndex = 2;
            comboBoxReduceDimSingleClass.SelectedIndex = 0;
            comboBoxReduceDimMultiClass.SelectedIndex = 0;
            comboBoxDimReductionNeutralClass.SelectedIndex = 2;

            comboBoxMethodForNormalization.SelectedIndex = 1;
            comboBoxNormalizationNegativeCtrl.SelectedIndex = 1;
            comboBoxNormalizationPositiveCtrl.SelectedIndex = 0;

            comboBoxRejectionNegativeCtrl.SelectedIndex = 1;
            comboBoxRejectionPositiveCtrl.SelectedIndex = 0;

            comboBoxRejection.SelectedIndex = 0;

            buttonReduceDim.Focus();
            buttonReduceDim.Select();



            //  ToolStripMenuItem UnselectItem = new ToolStripMenuItem("Options");
            //UnselectItem.Click += new System.EventHandler(this.UnselectItem);
            // contextMenuStripStatOptions.Items.Add(UnselectItem);

            // contextMenuStripStatOptions.Items.Add(new ToolStripSeparator());

            _StatMeanItem = new ToolStripMenuItem("Mean");
            _StatMeanItem.CheckOnClick = true;
            _StatMeanItem.Click += new System.EventHandler(this.StatMeanItem);
            contextMenuStripStatOptions.Items.Add(_StatMeanItem);

            _StatCVItem = new ToolStripMenuItem("Coefficient of Variation");
            _StatCVItem.CheckOnClick = true;
            _StatCVItem.CheckState = CheckState.Checked;
            _StatCVItem.Click += new System.EventHandler(this.StatCVItem);
            contextMenuStripStatOptions.Items.Add(_StatCVItem);


            _StatSumItem = new ToolStripMenuItem("Sum");
            _StatSumItem.CheckOnClick = true;
            _StatSumItem.Click += new System.EventHandler(this.StatSumItem);
            contextMenuStripStatOptions.Items.Add(_StatSumItem);

            _StatJarqueBeraItem = new ToolStripMenuItem("Normality Test (Jarque-Bera)");
            _StatJarqueBeraItem.CheckOnClick = true;
            _StatJarqueBeraItem.Click += new System.EventHandler(this.StatJarqueBeraItem);
            contextMenuStripStatOptions.Items.Add(_StatJarqueBeraItem);


            _DescEvolGlobalItem = new ToolStripMenuItem("Global");
            _DescEvolGlobalItem.CheckOnClick = true;
            _DescEvolGlobalItem.Click += new System.EventHandler(this.DescEvolGlobalItem);
            ContextMenuStripDescEvolOptions.Items.Add(_DescEvolGlobalItem);

            _DescEvolCellByCellItem = new ToolStripMenuItem("Cell-by-Cell");
            _DescEvolCellByCellItem.CheckOnClick = true;
            _DescEvolCellByCellItem.Click += new System.EventHandler(this.DescEvolCellByCellItem);
            ContextMenuStripDescEvolOptions.Items.Add(_DescEvolCellByCellItem);

            _QCZRegularItem = new ToolStripMenuItem("Regular");
            _QCZRegularItem.CheckOnClick = true;
            _QCZRegularItem.Click += new System.EventHandler(this.QCZRegularItem);
            ContextMenuStripQCOptions.Items.Add(_QCZRegularItem);

            _QCZRobustItem = new ToolStripMenuItem("Robust");
            _QCZRobustItem.CheckOnClick = true;
            _QCZRobustItem.Click += new System.EventHandler(this.QCZRobustItem);
            ContextMenuStripQCOptions.Items.Add(_QCZRobustItem);



            this.FormClosing += new FormClosingEventHandler(HCSAnalyzer_FormClosing);

            //HCSAnalyzer.Window_Closing += new HCSAnalyzer.Window_Closing(

        }


        private void HCSAnalyzer_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want close HCS-Analyzer ?", "Warning !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                e.Cancel = false;
            else
            {
                e.Cancel = true;
                return;
            }

            if (cGlobalInfo.CurrentScreening != null)
                cGlobalInfo.CurrentScreening.Close3DView();
            this.Dispose();

        }

        //    FormForSplashScreen SplashScreen = new FormForSplashScreen();
        //  public delegate void UpdateFormText(string text);
        //     /{
        // /       }/

        //private void UpdateLabel(string text)
        //{
        //    if (!SplashScreen.richTextBoxForProcess.InvokeRequired)
        //    {
        //        SplashScreen.richTextBoxForProcess.AppendText(text);

        //    }
        //    else
        //    {
        //        var s = new UpdateFormText(UpdateLabel);
        //        Invoke(s, new object[] { text });
        //    }
        //}

        private void MyLoad()
        {
            //  SplashScreen.Visible = false;
            // SplashScreen.Visible = true;
            //  SplashScreen.richTextBoxForProcess.AppendText("GUI initialization... ");
            //SplashScreen.richTextBoxForProcess.AppendText(" OK !\n");

            // SplashScreen.richTextBoxForProcess.AppendText("3D engine... ");
            //UpdateLabel("3D engine... ");

            //c3DWorld MyWorld = new c3DWorld(new cPoint3D(1, 1, 1), new cPoint3D(1, 1, 1), null, null, null);
            //UpdateLabel(" OK !\n");


            //SplashScreen.Visible = false;

        }


        public ExtendedTrackBar XTrackBarForZoom = new ExtendedTrackBar();

        private void HCSAnalyzer_Load(object sender, EventArgs e)
        {
            //System.Threading.Thread thdWorker = new ... (MethodToCall);
            // System.Threading.Thread thdWorker = new System.Threading.Thread(MyLoad);
            // thdWorker.Start();

            GlobalInfo = new cGlobalInfo(this);

            panelForTools.Controls.Add(cGlobalInfo.GUIPlateLUT.AssociatedPanel);


            this.comboBoxClass.Items.Add("Inactive");
            foreach (var item in cGlobalInfo.ListWellClasses)
            {
                this.comboBoxClass.Items.Add(item.Name);
                //this.comboBoxClass.Items[this.comboBoxClass.Items.Count-1].
            }


            // GlobalInfo.WindowName = this.Text;
            this.Text = cGlobalInfo.WindowName + " (Scalar Mode)";

            cGlobalInfo.OptionsWindow.Visible = false;
            cGlobalInfo.ComboForSelectedDesc = this.comboBoxDescriptorToDisplay;
            cGlobalInfo.CheckedListBoxForDescActive = this.checkedListBoxActiveDescriptors;

            //new Kitware.VTK.RenderWindowControl novelRender = new RenderWindowControl

            cGlobalInfo.renderWindowControlForVTK = null;//renderWindowControlForVTK;

            PlateListWindow = new PlatesListForm();
            cGlobalInfo.PlateListWindow = PlateListWindow;

            cGlobalInfo.panelForPlate = this.panelForPlate;

            comboBoxClass.SelectedIndex = 1;


            // CheckBox cb = new CheckBox();
            //cb.Text = "test";
            //cb.CheckStateChanged += (s, ex) =&gt;
            //this.Text = cb.CheckState.ToString();
            //ToolStripControlHost host = new ToolStripControlHost(cb);
            //toolStrip1.Items.Insert(0,host);



            // this.toolStripMain.DataBindings.Add("Checked", this.checkBox1, "Checked");


            checkBoxDisplayClasses.Text = "Display Class";
            checkBoxDisplayClasses.Appearance = Appearance.Button;
            checkBoxDisplayClasses.FlatStyle = FlatStyle.Popup;
            checkBoxDisplayClasses.CheckedChanged += new EventHandler(checkBoxDisplayClasses_CheckedChanged);

            ToolStripControlHost host = new ToolStripControlHost(checkBoxDisplayClasses);
            toolStripMain.Items.Insert(0, host);

            checkBoxApplyTo_AllPlates.Text = "Apply to all plates";
            checkBoxApplyTo_AllPlates.Appearance = Appearance.Button;
            checkBoxApplyTo_AllPlates.FlatStyle = FlatStyle.Popup;
            checkBoxApplyTo_AllPlates.Checked = true;
            checkBoxApplyTo_AllPlates.CheckedChanged += new EventHandler(checkBoxApplyTo_AllPlates_CheckedChanged);

            ToolStripControlHost hostcheckBoxApplyTo_AllPlates = new ToolStripControlHost(checkBoxApplyTo_AllPlates);
            toolStripMain.Items.Insert(3, hostcheckBoxApplyTo_AllPlates);

            ToolStripControlHost Host_TrackBarForZoom = new ToolStripControlHost(XTrackBarForZoom.panel);
            statusStripMain.Items.Insert(1, Host_TrackBarForZoom);

            // check command line arguments and process them
            if (System.Environment.GetCommandLineArgs().Length > 1)
            {
                List<string> LArg = new List<string>();
                int FileMode = 0;
                foreach (string arg in System.Environment.GetCommandLineArgs())
                {
                    if (arg.ToLower() == "-todb")
                    {
                        FileMode = 1;
                    }
                    else
                    {
                        LArg.Add(System.Environment.GetCommandLineArgs()[1]);
                    }
                }

                if (FileMode == 0)
                {
                    FormForImportExcel CSVFeedBackWindow = LoadCSVAssay(LArg.ToArray(), false);
                    if (CSVFeedBackWindow == null) return;
                    if (CSVFeedBackWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
                    ProcessOK(CSVFeedBackWindow);

                    UpdateUIAfterLoading();
                    return;
                }
                else if (FileMode == 1)
                {
                    CSVtoDB(LArg[0],",","");

                    return;
                }
            }
            // delete the current Log file
            File.Delete("HCSA.log");
            //  this.BringToFront();
            //MyLoad();
        }

        void checkBoxDisplayClasses_CheckedChanged(object sender, EventArgs e)
        {
            cGlobalInfo.IsDisplayClassOnly = checkBoxDisplayClasses.Checked;
            classViewToolStripMenuItem.Checked = checkBoxDisplayClasses.Checked;

            if (cGlobalInfo.CurrentScreening == null) return;
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);

        }

        void checkBoxApplyTo_AllPlates_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxApplyTo_AllPlates.Checked)
                this.Cursor = Cursors.Default;
            else
                this.Cursor = Cursors.Cross;
            if (cGlobalInfo.CurrentScreening == null) return;
            cGlobalInfo.CurrentScreening.IsSelectionApplyToAllPlates = checkBoxApplyTo_AllPlates.Checked;

        }

        //private void HCSAnalyzer_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    if (MessageBox.Show("Are you sure you want close HCS-Analyzer ?", "Warning !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes)
        //    {
        //        return;
        //    }

        //    if (GlobalInfo.CurrentScreening != null)
        //        GlobalInfo.CurrentScreening.Close3DView();
        //    this.Dispose();
        //}

        //public void DisplayMINE(List<double>[] ListValueDesc)
        //{
        //    int NumDesc = ListValueDesc.Length;

        //    double[,] CorrelationMatrix = new double[NumDesc, NumDesc];

        //    double[][] dataset1 = new double[NumDesc][];
        //    string[] VarNames = new string[NumDesc];

        //    for (int iDesc = 0; iDesc < NumDesc; iDesc++)
        //    {
        //        dataset1[iDesc] = new double[ListValueDesc[iDesc].Count];

        //        Array.Copy(ListValueDesc[iDesc].ToArray(), dataset1[iDesc], ListValueDesc[iDesc].Count);
        //        VarNames[iDesc] = iDesc.ToString();
        //    }
        //    data.Dataset data1 = new data.Dataset(dataset1, VarNames, 0);
        //    VarPairQueue Qu = new VarPairQueue(data1);

        //    for (int iDesc = 0; iDesc < NumDesc; iDesc++)
        //        for (int jDesc = 0; jDesc < iDesc; jDesc++)
        //        {
        //            Qu.addPair(iDesc, jDesc);
        //        }
        //    Analysis ana = new Analysis(data1, Qu);
        //    AnalysisParameters param = new AnalysisParameters();
        //    double resparam = param.commonValsThreshold;

        //    //    analysis.results.FullResult Full = new analysis.results.FullResult();
        //    //List<analysis.results.BriefResult> Brief = new List<analysis.results.BriefResult>();
        //    //analysis.results.BriefResult Brief = new analysis.results.BriefResult();



        //    java.lang.Class t = java.lang.Class.forName("analysis.results.BriefResult");

        //    //java.lang.Class restype = null;
        //    ana.analyzePairs(t, param);

        //    //   object o =  (ana.varPairQueue().peek());
        //    //   ana.getClass();
        //    //  int resNum = ana.numResults();
        //    analysis.results.Result[] res = ana.getSortedResults();

        //    List<string[]> ListValues = new List<string[]>();
        //    List<string> NameX = CompleteScreening.ListDescriptors.GetListNameActives();

        //    List<bool> ListIscolor = new List<bool>();

        //    for (int Idx = 0; Idx < res.Length; Idx++)
        //    {
        //        ListValues.Add(res[Idx].toString().Split(','));
        //        ListValues[Idx][0] = NameX[int.Parse(ListValues[Idx][0])];
        //        ListValues[Idx][1] = NameX[int.Parse(ListValues[Idx][1])];
        //    }
        //    string[] ListNames = res[0].getHeader().Split(',');


        //    ListNames[0] = "Descriptor A";
        //    ListNames[1] = "Descriptor B";


        //    for (int NIdx = 0; NIdx < ListNames.Length; NIdx++)
        //    {
        //        if (NIdx == 0) ListIscolor.Add(false);
        //        else if (NIdx == 1) ListIscolor.Add(false);
        //        else ListIscolor.Add(true);

        //    }

        //    cDisplayTable DisplayForTable = new cDisplayTable("MINE Analysis results", ListNames, ListValues, GlobalInfo, true);

        //}

        //private void findPathwayToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //    if (CompleteScreening == null) return;
        //    FormForNameRequest FormForRequest = new FormForNameRequest();
        //    if (FormForRequest.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
        //    int NumberOfPlates = CompleteScreening.ListPlatesActive.Count;

        //    FormForKeggGene KeggWin = new FormForKeggGene();
        //    KEGG ServKegg = new KEGG();

        //    string[] intersection_gene_pathways = new string[1];
        //    string[] Pathways = { FormForRequest.textBoxForName.Text };
        //    intersection_gene_pathways = ServKegg.get_genes_by_pathway("path:" + Pathways[0]);
        //    if ((Pathways == null) || (Pathways.Length == 0) || (Pathways[0] == ""))
        //    {
        //        MessageBox.Show("No pathway founded !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        return;
        //    }

        //    string[] fg_list = { "black" };
        //    string[] bg_list = { "orange" };

        //    string pathway_map_html = "";
        //    //  KEGG ServKegg = new KEGG();
        //    string[] ListGenesinPathway = ServKegg.get_genes_by_pathway("path:" + Pathways[0]);
        //    if (ListGenesinPathway.Length == 0)
        //    {
        //        return;
        //    }
        //    double[] ListValues = new double[ListGenesinPathway.Length];
        //    int IDxGeneOfInterest = 0;
        //    foreach (cPlate CurrentPlate in CompleteScreening.ListPlatesActive)
        //    {
        //        foreach (cWell CurrentWell in CurrentPlate.ListActiveWells)
        //        {
        //            string CurrentLID = "hsa:" + (int)CurrentWell.LocusID;

        //            for (int IdxGene = 0; IdxGene < ListGenesinPathway.Length; IdxGene++)
        //            {

        //                if (CurrentLID == intersection_gene_pathways[0])
        //                    IDxGeneOfInterest = IdxGene;

        //                if (CurrentLID == ListGenesinPathway[IdxGene])
        //                {
        //                    ListValues[IdxGene] = CurrentWell.ListDescriptors[CompleteScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetValue();
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    bg_list = new string[ListGenesinPathway.Length];
        //    fg_list = new string[ListGenesinPathway.Length];

        //    double MinValue = ListValues.Min();
        //    double MaxValue = ListValues.Max();

        //    for (int IdxCol = 0; IdxCol < bg_list.Length; IdxCol++)
        //    {

        //        int ConvertedValue = (int)((((CompleteScreening.GlobalInfo.LUTs.LUT_GREEN_TO_RED[0].Length - 1) * (ListValues[IdxCol] - MinValue)) / (MaxValue - MinValue)));

        //        Color Coul = Color.FromArgb(CompleteScreening.GlobalInfo.LUTs.LUT_GREEN_TO_RED[0][ConvertedValue], CompleteScreening.GlobalInfo.LUTs.LUT_GREEN_TO_RED[1][ConvertedValue], CompleteScreening.GlobalInfo.LUTs.LUT_GREEN_TO_RED[2][ConvertedValue]);

        //        if (IdxCol == IDxGeneOfInterest)
        //            fg_list[IdxCol] = "white";
        //        else
        //            fg_list[IdxCol] = "#000000";
        //        bg_list[IdxCol] = "#" + Coul.Name.Remove(0, 2);

        //    }

        //    //  foreach (string item in ListP.listBoxPathways.SelectedItems)
        //    {
        //        pathway_map_html = ServKegg.get_html_of_colored_pathway_by_objects(Pathways[0], ListGenesinPathway, fg_list, bg_list);
        //    }

        //    pathway_map_html = ServKegg.get_html_of_colored_pathway_by_objects((string)(Pathways[0]), intersection_gene_pathways, fg_list, bg_list);

        //    // FormForKegg KeggWin = new FormForKegg();
        //    if (pathway_map_html.Length == 0) return;

        //    //
        //    //KeggWin.Show();
        //    //ListP.listBoxPathways.MouseDoubleClick += new MouseEventHandler(listBox1_MouseDoubleClick);
        //    KeggWin.webBrowser.Navigate(pathway_map_html);

        //    KeggWin.Show();
        //}

        private void panelForPlate_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;


            int ScrollShiftY = this.panelForPlate.VerticalScroll.Value;
            int ScrollShiftX = this.panelForPlate.HorizontalScroll.Value;
            int Gutter = (int)cGlobalInfo.OptionsWindow.FFAllOptions.numericUpDownGutter.Value;

            int PosX = (int)((e.X - ScrollShiftX) / (cGlobalInfo.SizeHistoWidth + Gutter));
            int PosY = (int)((e.Y - ScrollShiftY) / (cGlobalInfo.SizeHistoHeight + Gutter));


            bool OnlyOnSelected = false;
            if ((PosX == 0) && (PosY == 0))
            {
                GlobalSelection(false);
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
                return;
            }


            if ((PosX == 0) && (PosY > 0))
            {
                for (int col = 0; col < cGlobalInfo.CurrentScreening.Columns; col++)
                {
                    if (cGlobalInfo.CurrentScreening.IsSelectionApplyToAllPlates)
                    {
                        int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;

                        for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
                        {
                            cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(PlateIdx);
                            cWell TmpWell = CurrentPlateToProcess.GetWell(col, PosY - 1, OnlyOnSelected);
                            if (TmpWell == null) continue;

                            if (cGlobalInfo.CurrentScreening.GetSelectionType() == -1)
                                TmpWell.SetAsNoneSelected();
                            else
                                TmpWell.SetClass(cGlobalInfo.CurrentScreening.GetSelectionType());
                        }
                    }
                    else
                    {
                        cWell TmpWell = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetWell(col, PosY - 1, OnlyOnSelected);
                        if (TmpWell != null)
                        {
                            if (cGlobalInfo.CurrentScreening.GetSelectionType() == -1) TmpWell.SetAsNoneSelected();
                            else
                                TmpWell.SetClass(cGlobalInfo.CurrentScreening.GetSelectionType());
                        }
                    }
                }
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().UpdateNumberOfClass();
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
                return;
            }

            if ((PosY == 0) && (PosX > 0))
            {
                for (int row = 0; row < cGlobalInfo.CurrentScreening.Rows; row++)
                {
                    if (cGlobalInfo.CurrentScreening.IsSelectionApplyToAllPlates)
                    {
                        int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;

                        for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
                        {
                            cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(PlateIdx);
                            cWell TmpWell = CurrentPlateToProcess.GetWell(PosX - 1, row, OnlyOnSelected);
                            if (TmpWell == null) continue;

                            if (cGlobalInfo.CurrentScreening.GetSelectionType() == -1)
                                TmpWell.SetAsNoneSelected();
                            else
                                TmpWell.SetClass(cGlobalInfo.CurrentScreening.GetSelectionType());
                        }
                    }
                    else
                    {
                        cWell TmpWell = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetWell(PosX - 1, row, OnlyOnSelected);
                        if (TmpWell != null)
                        {
                            if (cGlobalInfo.CurrentScreening.GetSelectionType() == -1) TmpWell.SetAsNoneSelected();
                            else
                                TmpWell.SetClass(cGlobalInfo.CurrentScreening.GetSelectionType());
                        }
                    }
                }
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().UpdateNumberOfClass();
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
                return;
            }
        }

        private void displayGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cListPlates ListToProcess = new cListPlates();
            ListToProcess.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());
            ComputeAndDisplayLDA(ListToProcess);
        }

        private void displayGraphToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            cListPlates ListToProcess = new cListPlates();
            ListToProcess.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());
            ComputeAndDisplayLDA(ListToProcess);
        }

        //private void displayGraphToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    ComputeAndDisplayLDA(CompleteScreening.ListPlatesActive);
        //}

        private void buttonNextPlate_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            if ((toolStripcomboBoxPlateList.SelectedIndex == -1) && (cGlobalInfo.CurrentScreening.ListPlatesActive.Count > 1))
            {
                toolStripcomboBoxPlateList.SelectedIndex = 1;
                return;
            }
            if (toolStripcomboBoxPlateList.SelectedIndex >= (toolStripcomboBoxPlateList.Items.Count - 1))
            {
                return;
            }

            toolStripcomboBoxPlateList.SelectedIndex++;
        }

        private void buttonPreviousPlate_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            if (toolStripcomboBoxPlateList.SelectedIndex <= 0) return;

            toolStripcomboBoxPlateList.SelectedIndex--;
        }

        private void distanceMatrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cPlate CurrentPlate = cGlobalInfo.CurrentScreening.ListPlatesActive[cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx];

            //double[][] Values = new double[CurrentPlate.ListActiveWells.Count][];

            //for (int i = 0; i < Values.Length; i++)
            //    Values[i] = new double[CurrentPlate.ListActiveWells.Count];


            //for (int j = 0; j < Values.Length; j++)
            //{
            //    cWell SourceWell = CurrentPlate.ListActiveWells[j];
            //    for (int i = j; i < Values[0].Length; i++)
            //    {
            //        cWell DestinationWell = CurrentPlate.ListActiveWells[i];
            //        Values[i][j] = Values[j][i] = SourceWell.DistanceTo(DestinationWell, CompleteScreening.ListDescriptors.CurrentSelectedDescriptor, eDistances.EUCLIDEAN);
            //    }
            //}
            FormToDisplayDistanceMap SingleMatrix = new FormToDisplayDistanceMap(CurrentPlate);
            cWindowToDisplaySingleMatrix WindowForSingleArray = new cWindowToDisplaySingleMatrix(SingleMatrix, eDistances.EUCLIDEAN);
        }

        private void buttonDisplayWellsSelectionData_Click(object sender, EventArgs e)
        {
            cGlobalInfo.ListSelectedWell.SingleCellBasedAnalysis();
        }

        private void comboBoxClassForWellSelection_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            SolidBrush BrushForColor = new SolidBrush(cGlobalInfo.ListWellClasses[e.Index].ColourForDisplay);
            e.Graphics.FillRectangle(BrushForColor, e.Bounds.X + 1, e.Bounds.Y + 1, 10, 10);
            e.Graphics.DrawString(comboBoxNeutralClassForClassif.Items[e.Index].ToString(), comboBoxNeutralClassForClassif.Font,
                System.Drawing.Brushes.Black, new RectangleF(e.Bounds.X + 15, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            e.DrawFocusRectangle();
        }

        //private void cellBasedClassificationTreeToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (cGlobalInfo.CurrentScreening.CellBasedClassification.J48Model == null) return;
        //    cGlobalInfo.CurrentScreening.CellBasedClassification.DisplayTree(GlobalInfo).Show();
        //}

        private void comboBoxClass_DrawItem_1(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index > 0)
            {
                SolidBrush BrushForColor = new SolidBrush(cGlobalInfo.ListWellClasses[e.Index - 1].ColourForDisplay);
                e.Graphics.FillRectangle(BrushForColor, e.Bounds.X + 1, e.Bounds.Y + 1, 10, 10);
            }
            if (e.Index == 0)
                e.Graphics.DrawString("Inactive", comboBoxClass.Font,
                                   System.Drawing.Brushes.Black, new RectangleF(e.Bounds.X + 15, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            else
                e.Graphics.DrawString(cGlobalInfo.ListWellClasses[e.Index - 1].Name, comboBoxClass.Font,
                                        System.Drawing.Brushes.Black, new RectangleF(e.Bounds.X + 15, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));

            e.DrawFocusRectangle();
        }

        private void classesDistributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormForPie WindowForClassesDistribution = new FormForPie();
            WindowForClassesDistribution.Text = "Classes Distributions";

            int[] ListClasses = cGlobalInfo.CurrentScreening.GetClassPopulation();

            Series CurrentSeries = WindowForClassesDistribution.chartForPie.Series[0];

            int NumberOfWells = cGlobalInfo.CurrentScreening.GetNumberOfActiveWells();
            int IdxPt = 0;
            //  CurrentSeries.CustomProperties = "PieLabelStyle=Outside";
            for (int Idx = 0; Idx < ListClasses.Length; Idx++)
            {

                if (ListClasses[Idx] == 0)
                {

                    continue;
                }
                CurrentSeries.Points.Add(ListClasses[Idx]);
                CurrentSeries.Points[IdxPt].Color = cGlobalInfo.ListWellClasses[Idx].ColourForDisplay;
                CurrentSeries.Points[IdxPt].Label = String.Format("{0:0.###}", ((100.0 * ListClasses[Idx]) / NumberOfWells)) + " %";

                CurrentSeries.Points[IdxPt].LegendText = "Class " + Idx;
                CurrentSeries.Points[IdxPt].ToolTip = ListClasses[Idx] + " / " + NumberOfWells;
                IdxPt++;
            }

            WindowForClassesDistribution.Show();
        }

        private void hierarchicalTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormForHierarchical WindowHierarchical = new FormForHierarchical(this.GlobalInfo);
            WindowHierarchical.richTextBoxWarning.AppendText("Warning:\nHierarchical tree visualization is not adpated for large number of experiments !\nIt can rapidly generate out-of-memory exception!");

            System.Windows.Forms.DialogResult Res = WindowHierarchical.ShowDialog();// MessageBox.Show("Hierarchical tree is not adpated for large number of experiments !\n It can rapidly generate out-of-memory exception!\n Proceed anyway ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Res != System.Windows.Forms.DialogResult.OK) return;
            cDendoGram DendoGram = new cDendoGram(GlobalInfo, WindowHierarchical.radioButtonFullScreen.Checked, 1);
            //cDendoGram DendoGram = new cDendoGram(GlobalInfo,
            //    CompleteScreening.ListPlatesActive[CompleteScreening.CurrentDisplayPlateIdx].CreateInstancesWithoutClass(),
            //    1);
            return;
        }

        private void plateViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            List<cPanelForDisplayArray> ListPlates = new List<cPanelForDisplayArray>();

            foreach (cPlate CurrentPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
            {
                ListPlates.Add(new FormToDisplayPlate(CurrentPlate));
            }

            cWindowToDisplayEntireScreening WindowToDisplayArray = new cWindowToDisplayEntireScreening(ListPlates, cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName(), 6);

            WindowToDisplayArray.Show();
        }

        private void descriptorViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDescriptorsWindow();
        }

        private void ThreeDVisualizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cGlobalInfo._Is3DVisualization = ThreeDVisualizationToolStripMenuItem.Checked;

            if (!ThreeDVisualizationToolStripMenuItem.Checked)
                cGlobalInfo.CurrentScreening.Close3DView();
            else
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().Display3DDistributionOnly(cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
            //            CompleteScreening.GetCurrentDisplayPlate().DisplayDistribution(CompleteScreening.ListDescriptors.CurrentSelectedDescriptor, false);
        }



        private void createAveragePlateToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FormForPlateAveraging FFPA = new FormForPlateAveraging();

            PanelForPlatesSelection PlatesSelectionPanel = new PanelForPlatesSelection(true, null, true);
            PlatesSelectionPanel.Height = FFPA.panelForPlateList.Height;

            FFPA.panelForPlateList.Controls.Add(PlatesSelectionPanel);
            if (FFPA.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            cListPlates LP = PlatesSelectionPanel.GetListSelectedPlates();


            cPlate NewPlate = new cPlate("Average Plate", cGlobalInfo.CurrentScreening);
            cWell TmpWell;

            #region QC report
            cExtendedList TmpReportForQuality = new cExtendedList("Weight");
            TmpReportForQuality.ListTags = new List<object>();
            cExtendedTable TableForQCReport = new cExtendedTable(TmpReportForQuality);
            TableForQCReport.ListRowNames = new List<string>();
            TableForQCReport.ListTags = new List<object>();
            foreach (cPlate TmpPlate in LP)
            {
                object QualityValue = TmpPlate.ListProperties.FindValueByName("Quality");
                if ((QualityValue != null) && (FFPA.checkBoxWeightedSum.Checked))
                    TableForQCReport[0].Add((double)QualityValue);
                else
                {
                    this.richTextBoxConsole.AppendText(TmpPlate.GetName() + " - Quality is NULL. Weight set to 1.\n");
                    TableForQCReport[0].Add(1);
                }

                TableForQCReport.ListRowNames.Add(TmpPlate.GetName());
                TableForQCReport.ListTags.Add(TmpPlate);
                TableForQCReport[0].ListTags.Add(TmpPlate);

            }

            cDisplayExtendedTable DET = new cDisplayExtendedTable();
            DET.SetInputData(TableForQCReport);
            DET.Title = "QC report";
            TableForQCReport.Name = "QC report";
            DET.Run();
            #endregion


            for (int X = 0; X < cGlobalInfo.CurrentScreening.Columns; X++)
                for (int Y = 0; Y < cGlobalInfo.CurrentScreening.Rows; Y++)
                {
                    cListSignature LDesc = new cListSignature();

                    for (int i = 0; i < cGlobalInfo.CurrentScreening.ListDescriptors.Count; i++)
                    {
                        double Value = 0;
                        double NormFactor = 0;

                        foreach (cPlate TmpPlate in LP)
                        {
                            double Weight = 1;
                            object QualityValue = TmpPlate.ListProperties.FindValueByName("Quality");
                            if ((QualityValue != null) && (FFPA.checkBoxWeightedSum.Checked))
                                Weight = (double)QualityValue;

                            NormFactor += Weight;
                            TmpWell = TmpPlate.GetWell(X, Y, false);
                            if (TmpWell != null)
                            {
                                Value += Weight * TmpWell.ListSignatures[i].GetValue();
                            }
                        }

                        if (NormFactor > 0)
                        {
                            cSignature Desc = new cSignature(Value / NormFactor, cGlobalInfo.CurrentScreening.ListDescriptors[i], cGlobalInfo.CurrentScreening);
                            LDesc.Add(Desc);
                        }
                    }
                    cWell NewWell = new cWell(LDesc, X + 1, Y + 1, cGlobalInfo.CurrentScreening, NewPlate);
                    NewWell.SetCpdName("Average Well [" + (X + 1) + ":" + (Y + 1) + "]");
                    NewPlate.AddWell(NewWell);

                }

            cGlobalInfo.CurrentScreening.AddPlate(NewPlate);
            cGlobalInfo.CurrentScreening.ListPlatesActive.Add(NewPlate);
            toolStripcomboBoxPlateList.Items.Add(NewPlate.GetName());
            cGlobalInfo.CurrentScreening.ListPlatesActive[cGlobalInfo.CurrentScreening.ListPlatesActive.Count - 1].UpDataMinMax();
            cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx = 0;
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), true);
        }


        private cConvertCSVtoDB CSVtoDB(string PathName, string Delimiter, string DBDirectory)
        {
            FormForImportExcel CSVWindow = CellByCellFromCSV(PathName, Delimiter.ToCharArray()[0]);
            CSVWindow.Separator = Delimiter.ToCharArray()[0];
            if (CSVWindow == null) return null;
            if (CSVWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK) return null;

            if (cGlobalInfo.CurrentScreening != null) cGlobalInfo.CurrentScreening.Close3DView();

            string[] ListNames = PathName.Split('\\');

            string MainName = "";
            for (int i = 0; i < ListNames.Length - 1; i++)
            {
                MainName += ListNames[i] + "\\";
            }

            if (DBDirectory == "")
            {

                FolderBrowserDialog WorkingFolderDialog = new FolderBrowserDialog();
                WorkingFolderDialog.SelectedPath = MainName;
                WorkingFolderDialog.ShowNewFolderButton = true;
                WorkingFolderDialog.Description = "Select working directory";
                if (WorkingFolderDialog.ShowDialog() != DialogResult.OK) return null;
                DBDirectory = WorkingFolderDialog.SelectedPath;
            }


            int NumPlateName = 0;
            int NumRow = 0;
            int NumCol = 0;
            int NumWellPos = 0;
            int NumPhenotypeClass = 0;
            // int NumLocusID = 0;
            // int NumConcentration = 0;
            //  int NumName = 0;
            //   int NumInfo = 0;
            //   int NumClass = 0;

            int numDescritpor = 0;

            for (int i = 0; i < CSVWindow.dataGridViewForImport.Rows.Count; i++)
            {
                string CurrentVal = CSVWindow.dataGridViewForImport.Rows[i].Cells[2].Value.ToString();
                if ((CurrentVal == "Plate name") && ((bool)CSVWindow.dataGridViewForImport.Rows[i].Cells[1].Value))
                    NumPlateName++;
                if ((CurrentVal == "Row") && ((bool)CSVWindow.dataGridViewForImport.Rows[i].Cells[1].Value))
                    NumRow++;
                if ((CurrentVal == "Column") && ((bool)CSVWindow.dataGridViewForImport.Rows[i].Cells[1].Value))
                    NumCol++;
                if ((CurrentVal == "Well position") && ((bool)CSVWindow.dataGridViewForImport.Rows[i].Cells[1].Value))
                    NumWellPos++;
                if ((CurrentVal == "Descriptor") && ((bool)CSVWindow.dataGridViewForImport.Rows[i].Cells[1].Value))
                    numDescritpor++;
                if ((CurrentVal == "Phenotype Class") && ((bool)CSVWindow.dataGridViewForImport.Rows[i].Cells[1].Value))
                    NumPhenotypeClass++;
            }
            string[] Sep = new string[1];
            Sep[0] = "\\";

            string[] ResSplit = PathName.Split(Sep, StringSplitOptions.None);
            string GeneratedName = ResSplit[ResSplit.Length - 1].Remove(ResSplit[ResSplit.Length - 1].Length - 4);

            if (NumPlateName != 1)
            {
                if (NumPlateName == 0)
                {
                    if (MessageBox.Show("No \"Plate Name\" has been defined.\nDo you want generate automatically one single plate with\n\"" + GeneratedName + "\" name?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                        return null;
                }
                else
                {
                    MessageBox.Show("Only one \"Plate Name\" has to be selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            if ((NumRow != 1) && (cGlobalInfo.OptionsWindow.radioButtonWellPosModeDouble.Checked == true))
            {
                MessageBox.Show("One and only one \"Row\" has to be selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            if ((NumCol != 1) && (cGlobalInfo.OptionsWindow.radioButtonWellPosModeDouble.Checked == true))
            {
                MessageBox.Show("One and only one \"Column\" has to be selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            if ((NumWellPos != 1) && (cGlobalInfo.OptionsWindow.radioButtonWellPosModeSingle.Checked == true))
            {
                MessageBox.Show("One and only one \"Well position\" has to be selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if ((numDescritpor < 1) && (CSVWindow.IsImportCSV))
            {
                MessageBox.Show("You need to select at least one \"Descriptor\" !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int Mode = 2;
            if (cGlobalInfo.OptionsWindow.radioButtonWellPosModeSingle.Checked) Mode = 1;

            cConvertCSVtoDB ConvertCSVtoDB = new cConvertCSVtoDB(PathName, Mode);
            ConvertCSVtoDB.GlobalInfo = GlobalInfo;
            ConvertCSVtoDB.CSVWindow = CSVWindow;
            ConvertCSVtoDB.Delimiter = Delimiter;
            ConvertCSVtoDB.Run();
            ConvertCSVtoDB.SelectedPath = DBDirectory;

            return ConvertCSVtoDB;
        }

        public class cConvertCSVtoDB
        {
            string PathName;
            int Mode;
            BackgroundWorker backgroundWorkerForCSVtoDB = new BackgroundWorker();
            public cGlobalInfo GlobalInfo;
            public FormForImportExcel CSVWindow;
            public string SelectedPath;
            public string Delimiter = ",";
            CsvFileReader CSVsr;

            cExtendedTable ReportTable = new cExtendedTable();
            string CompleteReportString = "Process over!\n";

            public cConvertCSVtoDB(string PathName, int Mode)
            {
                this.PathName = PathName;
                this.Mode = Mode;

                backgroundWorkerForCSVtoDB.WorkerReportsProgress = true;
                //backgroundWorkerForCSVtoDB.WorkerSupportsCancellation = true;

                backgroundWorkerForCSVtoDB.DoWork += new DoWorkEventHandler(backgroundWorkerForCSVtoDB_DoWork);
                backgroundWorkerForCSVtoDB.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorkerForCSVtoDB_RunWorkerCompleted);
                backgroundWorkerForCSVtoDB.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerForCSVtoDB_ProgressChanged);
            }

            public void Run()
            {
                ReportTable.Add(new cExtendedList("Object Number"));
                ReportTable.ListRowNames = new List<string>();
                ReportTable[0].ListTags = new List<object>();
                ReportTable.Name = "Number of successfully loaded objects";

                if (backgroundWorkerForCSVtoDB.IsBusy != true)
                {
                    // Start the asynchronous operation.
                    backgroundWorkerForCSVtoDB.RunWorkerAsync();
                }
            }

            private string ConvertPosition(int PosX, int PosY)
            {
                char character = (char)(PosY + 64);
                string ToReturn = character.ToString() + PosX.ToString("00");
                return ToReturn;
            }

            private int[] ConvertPosition(string PosString)
            {
                int[] Pos = new int[2];

                Pos[1] = Convert.ToInt16(PosString[0].ToString().ToUpper().ToCharArray()[0]) - 64;
                string PosY = PosString.Remove(0, 1);
                bool IsTrue = int.TryParse(PosY, out Pos[0]);

                if (!IsTrue) return null;

                return Pos;
            }

            private int GetColIdxFor(string StringToBeDetected, FormForImportExcel FromExcel)
            {
                int Pos = -1;

                for (int i = 0; i < FromExcel.dataGridViewForImport.Rows.Count; i++)
                {
                    string CurrentVal = FromExcel.dataGridViewForImport.Rows[i].Cells[2].Value.ToString();
                    bool IsSelected = (bool)FromExcel.dataGridViewForImport.Rows[i].Cells[1].Value;

                    if ((CurrentVal == StringToBeDetected) && (IsSelected == true)) Pos = i;
                }

                return Pos;
            }

            private int[] GetColsIdxFor(string StringToBeDetected, FormForImportExcel FromExcel)
            {
                List<int> Pos = new List<int>();
                for (int i = 0; i < FromExcel.dataGridViewForImport.Rows.Count; i++)
                {
                    string CurrentVal = FromExcel.dataGridViewForImport.Rows[i].Cells[2].Value.ToString();
                    bool IsSelected = (bool)FromExcel.dataGridViewForImport.Rows[i].Cells[1].Value;

                    if ((CurrentVal == StringToBeDetected) && (IsSelected == true))
                        Pos.Add(i);
                }

                return Pos.ToArray();
            }

            private void backgroundWorkerForCSVtoDB_DoWork(object sender, DoWorkEventArgs e)
            {
                BackgroundWorker worker = sender as BackgroundWorker;

                CSVsr = new CsvFileReader(PathName);
                CSVsr.Separator = this.Delimiter.ToCharArray()[0];
                CsvRow OriginalNames = new CsvRow();
                
                if (!CSVsr.ReadRow(OriginalNames))
                {
                    CSVsr.Close();
                    return;
                }

                int ColPlateName = GetColIdxFor("Plate name", CSVWindow);
                int ColCol = GetColIdxFor("Column", CSVWindow);
                int ColRow = GetColIdxFor("Row", CSVWindow);
                int ColWellPos = GetColIdxFor("Well position", CSVWindow);
                int ColPhenotypeClass = GetColIdxFor("Phenotype Class", CSVWindow);
                int[] ColsForDescriptors = GetColsIdxFor("Descriptor", CSVWindow);

                FormForProgress ProgressWindow = new FormForProgress();
                ProgressWindow.Text = "CSV -> DB : processing";
                ProgressWindow.Show();
                CsvRow CurrentDesc = new CsvRow();
                int TotalPlateNumber = 0;
                int TotalObjectNumber = 0;
                int TotalWellNumber = 0;
                string OriginalPlatePlateName;
                string CurrentPlateName;
                string ConvertedName;


                this.CompleteReportString += "Source file: " + PathName + "\n";
                this.CompleteReportString += OriginalNames.Count + " features selected.\n";
                this.CompleteReportString += "Time stamp: " + DateTime.Now.ToString() + " \n";

                if (CSVsr.ReadRow(CurrentDesc) == false) return;
                do
                {
                    if (ColPlateName == -1)
                    {
                        ConvertedName = OriginalPlatePlateName = CurrentPlateName = "Generated Plate Name";
                    }
                    else
                    {
                        OriginalPlatePlateName = CurrentDesc[ColPlateName];
                        CurrentPlateName = CurrentDesc[ColPlateName];
                        ConvertedName = "";

                        foreach (var c in System.IO.Path.GetInvalidFileNameChars())
                            ConvertedName = OriginalPlatePlateName.Replace(c, '-');
                    }

                    List<string> ListNameSignature = new List<string>();

                    for (int idxDesc = 0/*Mode + 1*/; idxDesc < ColsForDescriptors.Length/* + Mode + 1*/; idxDesc++)
                    {
                        string TmpSignature = OriginalNames[ColsForDescriptors[idxDesc]];
                        TmpSignature = TmpSignature.Replace('[', '_');
                        TmpSignature = TmpSignature.Replace(']', '_');

                        ListNameSignature.Add(TmpSignature);
                    }
                    ListNameSignature.Add("Phenotype_Class");
                    ListNameSignature.Add("Phenotype_Confidence");
                    cSQLiteDatabase SQDB = new cSQLiteDatabase(SelectedPath + "\\" + ConvertedName, ListNameSignature, true);

                    this.CompleteReportString += "\n" + CurrentPlateName + ":\n";
                    TotalPlateNumber++;

                    do
                    {
                        string OriginalWellPos;
                        int[] Pos = new int[2];

                        if (Mode == 1)
                        {
                            Pos = ConvertPosition(CurrentDesc[ColWellPos]);
                            if (Pos == null)
                            {
                                if (MessageBox.Show("Error in converting the current well position.\nGo to Edit->Options->Import-Export->Well Position Mode to fix this.\nDo you want continue ?", "Loading error !", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.No)
                                {
                                    CSVsr.Close();
                                    return;
                                }
                                //else
                                //    goto NEXTLOOP;
                            }
                            OriginalWellPos = CurrentDesc[ColWellPos];
                        }
                        else
                        {
                            if (int.TryParse(CurrentDesc[ColCol], out Pos[0]) == false)
                                goto NEXTLOOP;
                            if (int.TryParse(CurrentDesc[ColRow], out Pos[1]) == false)
                                goto NEXTLOOP;

                            OriginalWellPos = ConvertPosition(int.Parse(CurrentDesc[ColCol]), int.Parse(CurrentDesc[ColRow]));// "("+CurrentDesc[ColCol]+","+CurrentDesc[ColRow]+")";
                        }

                        string CurrentWellPos = OriginalWellPos;

                        cWellForDatabase WellForDB = new cWellForDatabase(OriginalPlatePlateName, Pos[0], Pos[1]);
                        cExtendedTable ListData = new cExtendedTable();
                        //   for (int idxDesc = 0; idxDesc < ColsForDescriptors.Length; idxDesc++)
                        //   ListData[idxDesc] = new List<double>();

                        ProgressWindow.richTextBoxForComment.AppendText(CurrentPlateName + " : " + CurrentWellPos + "\n");
                        //ProgressWindow.label.Refresh();
                        ProgressWindow.Refresh();

                        do
                        {
                            //  CurrentWellPos = CurrentDesc[ColWellPos];
                            cExtendedList Signature = new cExtendedList();

                            for (int idxDesc = 0; idxDesc < ColsForDescriptors.Length; idxDesc++)
                            {
                                double Value;

                                if (double.TryParse(CurrentDesc[ColsForDescriptors[idxDesc]], NumberStyles.Any, CultureInfo.InvariantCulture/*.CreateSpecificCulture("en-US")*/, out Value))
                                {
                                    if (double.IsNaN(Value))
                                        Signature.Add(0);
                                    else
                                        Signature.Add(Value);
                                }
                                else
                                {
                                    Signature.Add(0);
                                }

                            }
                            // if the class of the phenotype is defined in the file then use it
                            // if not, put it at 0
                            double ValueClass;
                            if ((ColPhenotypeClass != -1) && (double.TryParse(CurrentDesc[ColPhenotypeClass], out ValueClass) == true))
                            {
                                double IntValue = (int)(ValueClass) % cGlobalInfo.ListCellularPhenotypes.Count;
                                Signature.Add(IntValue);
                            }
                            else
                            {
                                Signature.Add(0);
                            }

                            // finally add the classification confidence column (1 by default)
                            Signature.Add(1);

                            ListData.Add(Signature);
                            // WellForDB.AddSignature(Signature);
                            // manage the end of the file
                            if (CSVsr.ReadRow(CurrentDesc) == false)
                            {
                                WellForDB.AddListSignatures(ListData);
                                cFeedBackMessage FeedBackMessage = SQDB.AddNewWell(WellForDB);
                                SQDB.CloseConnection();
                                // this.CompleteReportString += FeedBackMessage.Message + "\n";
                                goto NEXTLOOP;
                            }
                            if (ColPlateName == -1)
                                CurrentPlateName = "Generated Plate Name";
                            else
                                CurrentPlateName = CurrentDesc[ColPlateName];

                            if (Mode == 1)
                                CurrentWellPos = CurrentDesc[ColWellPos];
                            else
                            {
                                int ResCol;
                                int ResRow;
                                if ((!int.TryParse(CurrentDesc[ColCol], out ResCol)) || (!int.TryParse(CurrentDesc[ColRow], out ResRow))) goto NEXTLOOP;

                                CurrentWellPos = ConvertPosition(ResCol, ResRow);
                            }

                            TotalObjectNumber++;

                            // NEXTSIGNATURE: ;

                        } while (CurrentWellPos == OriginalWellPos);

                        TotalWellNumber++;

                        WellForDB.AddListSignatures(ListData);
                        SQDB.AddNewWell(WellForDB);

                        ReportTable.ListRowNames.Add(CurrentPlateName + " : " + CurrentWellPos);
                        ReportTable[0].Add(ListData.Count);
                        ReportTable[0].ListTags.Add(CurrentPlateName + " : " + CurrentWellPos + "\n" + ListData.Count + " objects");
                        this.CompleteReportString += "\t" + CurrentWellPos + " : " + ListData.Count + " objects\n";

                    NEXTSIGNATURE: ;

                    } while (OriginalPlatePlateName == CurrentPlateName);

                    SQDB.CloseConnection();
                } while (true);

            NEXTLOOP: ;
                ProgressWindow.Close();

                this.CompleteReportString += "\n-----------------------------\n";
                this.CompleteReportString += TotalPlateNumber + " plates\n";
                this.CompleteReportString += TotalWellNumber + " wells\n";
                this.CompleteReportString += TotalObjectNumber + " objects\n";
                this.CompleteReportString += "\nDataBase location:\n" + SelectedPath;
                //    worker.ReportProgress(10);
            }

            private void backgroundWorkerForCSVtoDB_ProgressChanged(object sender, ProgressChangedEventArgs e)
            {
                //resultLabel.Text = (e.ProgressPercentage.ToString() + "%");
            }

            private void backgroundWorkerForCSVtoDB_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                if (e.Error != null)
                {
                    System.Windows.Forms.MessageBox.Show("Error while processing the data", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //    //FormForPlateDimensions PlateDim = new FormForPlateDimensions();
                    //    //PlateDim.Text = "Load generated screening";
                    //    //PlateDim.checkBoxAddCellNumber.Visible = true;
                    //    //PlateDim.checkBoxIsOmitFirstColumn.Visible = true;
                    //    //PlateDim.labelHisto.Visible = true;
                    //    //PlateDim.numericUpDownHistoSize.Visible = true;

                    //    //if (PlateDim.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    //    //    return;
                    //    // LoadCellByCellDB(PlateDim, this.SelectedPath);

                    cDesignerSplitter DS = new cDesignerSplitter();
                    DS.Orientation = Orientation.Vertical;


                    cViewertext VT = new cViewertext();
                    VT.SetInputData(this.CompleteReportString);
                    VT.Run();

                    cViewerGraph1D VG1 = new cViewerGraph1D();
                    VG1.Chart.LabelAxisX = "Well Index";
                    VG1.Chart.LabelAxisY = "Number of objects";
                    VG1.Chart.IsLine = true;
                    VG1.Chart.IsZoomableX = true;
                    VG1.Chart.IsXGrid = true;
                    VG1.SetInputData(ReportTable);
                    VG1.Run();

                    DS.SetInputData(VT.GetOutPut());
                    DS.SetInputData(VG1.GetOutPut());
                    DS.Run();

                    cDisplayToWindow DTW = new cDisplayToWindow();
                    DTW.Title = "CSV->DB report";
                    DTW.IsModal = false;
                    DTW.SetInputData(DS.GetOutPut());
                    DTW.Run();
                    DTW.Display();

                    if (MessageBox.Show("Database successfully created.\nDo you want load it?", "Question !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;
                    else
                        cGlobalInfo.WindowHCSAnalyzer.LoadDB(this.SelectedPath, false);




                }
            }
        }

        private void loadDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening != null) cGlobalInfo.CurrentScreening.Close3DView();

            LoadDB("", true);
        }

        public void LoadDB(string DefaultPath, bool IsDisplayPlateSelection)
        {
            if (!Directory.Exists(DefaultPath)) DefaultPath = "";

            string SelectedPath = DefaultPath;
            if (DefaultPath == "")
            {
                var dlg1 = new Ionic.Utils.FolderBrowserDialogEx();
                dlg1.Description = "Select the folder containing your databases.";
                dlg1.ShowNewFolderButton = true;
                dlg1.ShowEditBox = true;
                if (DefaultPath != "")
                    dlg1.SelectedPath = DefaultPath;
                //dlg1.NewStyle = false;
                //dlg1.SelectedPath = txtExtractDirectory.Text;
                dlg1.ShowFullPathInEditBox = true;
                dlg1.RootFolder = System.Environment.SpecialFolder.Desktop;

                // Show the FolderBrowserDialog.
                DialogResult result = dlg1.ShowDialog();
                if (result != DialogResult.OK) return;

                SelectedPath = dlg1.SelectedPath;
            }
            if (Directory.Exists(SelectedPath) == false) return;

            FormForPlateDimensions PlateDim = new FormForPlateDimensions();
            PlateDim.checkBoxAddCellNumber.Checked = false;
            PlateDim.checkBoxAddCellNumber.Visible = true;
            PlateDim.checkBoxIsOmitFirstColumn.Visible = true;
            PlateDim.labelHisto.Visible = true;
            PlateDim.numericUpDownHistoSize.Visible = true;

            if (PlateDim.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            LoadCellByCellDB(PlateDim, SelectedPath, IsDisplayPlateSelection);
        }

        private void classViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            checkBoxDisplayClasses.Checked = classViewToolStripMenuItem.Checked;

            // CompleteScreening.GlobalInfo.IsDisplayClassOnly = checkBoxDisplayClasses.Checked;
            //CompleteScreening.GetCurrentDisplayPlate().DisplayDistribution(CompleteScreening.ListDescriptors.CurrentSelectedDescriptor, false);
        }

        private void pieViewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            cGlobalInfo.ViewMode = eViewMode.PIE;
            averageViewToolStripMenuItem.Checked = false;
            pieViewToolStripMenuItem1.Checked = true;
            histogramViewToolStripMenuItem.Checked = false;
            imageViewToolStripMenuItem.Checked = false;
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
        }

        private void HCSAnalyzer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            if (e.KeyChar == 'a')
            {
                cGlobalInfo.ViewMode = eViewMode.AVERAGE;
                pieViewToolStripMenuItem1.Checked = false;
                averageViewToolStripMenuItem.Checked = true;
                histogramViewToolStripMenuItem.Checked = false;
                imageViewToolStripMenuItem.Checked = false;
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
            }
            if (e.KeyChar == 'h')
            {
                cGlobalInfo.ViewMode = eViewMode.DISTRIBUTION;
                pieViewToolStripMenuItem1.Checked = false;
                averageViewToolStripMenuItem.Checked = false;
                histogramViewToolStripMenuItem.Checked = true;
                imageViewToolStripMenuItem.Checked = false;
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
            }
            if (e.KeyChar == 'c')
            {
                checkBoxDisplayClasses.Checked = true;
            }
            if (e.KeyChar == 'p')
            {
                cGlobalInfo.ViewMode = eViewMode.PIE;
                pieViewToolStripMenuItem1.Checked = true;
                averageViewToolStripMenuItem.Checked = false;
                histogramViewToolStripMenuItem.Checked = false;
                imageViewToolStripMenuItem.Checked = false;

                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
            }

        }


        private void imageViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cGlobalInfo.ViewMode = eViewMode.IMAGE;
            pieViewToolStripMenuItem1.Checked = false;
            averageViewToolStripMenuItem.Checked = false;
            histogramViewToolStripMenuItem.Checked = false;
            imageViewToolStripMenuItem.Checked = true;
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
        }


        private void averageViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cGlobalInfo.ViewMode = eViewMode.AVERAGE;
            pieViewToolStripMenuItem1.Checked = false;
            averageViewToolStripMenuItem.Checked = true;
            histogramViewToolStripMenuItem.Checked = false;
            imageViewToolStripMenuItem.Checked = false;
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
        }

        private void histogramViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cGlobalInfo.ViewMode = eViewMode.DISTRIBUTION;
            pieViewToolStripMenuItem1.Checked = false;
            averageViewToolStripMenuItem.Checked = false;
            histogramViewToolStripMenuItem.Checked = true;
            imageViewToolStripMenuItem.Checked = false;
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
        }

        private void currentPlate3DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            cGlobalInfo.OptionsWindow.checkBoxConnectDRCPts.Checked = true;

            FormFor3DDataDisplay FormToDisplayXYZ = new FormFor3DDataDisplay(false, cGlobalInfo.CurrentScreening);
            for (int i = 0; i < (int)cGlobalInfo.CurrentScreening.ListDescriptors.Count; i++)
            {
                FormToDisplayXYZ.comboBoxDescriptorX.Items.Add(cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName());
                FormToDisplayXYZ.comboBoxDescriptorY.Items.Add(cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName());
                FormToDisplayXYZ.comboBoxDescriptorZ.Items.Add(cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName());
            }
            FormToDisplayXYZ.Show();
            FormToDisplayXYZ.comboBoxDescriptorX.Text = cGlobalInfo.CurrentScreening.ListDescriptors[0].GetName() + " ";
            FormToDisplayXYZ.comboBoxDescriptorY.Text = cGlobalInfo.CurrentScreening.ListDescriptors[0].GetName() + " ";
            FormToDisplayXYZ.comboBoxDescriptorZ.Text = cGlobalInfo.CurrentScreening.ListDescriptors[0].GetName() + " ";
            return;
        }

        private void newOptionMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cListOptions ListOptions = new cListOptions(GlobalInfo);
            FormForGlobalInfoOptions WindowForOptions = new FormForGlobalInfoOptions(ListOptions);
            WindowForOptions.ShowDialog();
        }

        private void singleCellsSimulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormForSimuGenerator WindowForSimulation = new FormForSimuGenerator(GlobalInfo);
            WindowForSimulation.Show();
        }

        private void hierarchicalTreeToolStripMenuItemFullScreen_Click(object sender, EventArgs e)
        {
            FormForHierarchical WindowHierarchical = new FormForHierarchical(this.GlobalInfo);
            WindowHierarchical.richTextBoxWarning.AppendText("Warning:\nHierarchical tree visualization is not adpated for large number of experiments !\nIt can rapidly generate out-of-memory exception!");

            System.Windows.Forms.DialogResult Res = WindowHierarchical.ShowDialog();// MessageBox.Show("Hierarchical tree is not adpated for large number of experiments !\n It can rapidly generate out-of-memory exception!\n Proceed anyway ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Res != System.Windows.Forms.DialogResult.OK) return;
            // cDendoGram DendoGram = new cDendoGram(GlobalInfo, WindowHierarchical.radioButtonFullScreen.Checked,1);
            cDendoGram DendoGram = new cDendoGram(GlobalInfo,
               true,
                1);
            return;
        }

        #region Clustering
        private void buttonClustering_Click(object sender, EventArgs e)
        {

            if (cGlobalInfo.CurrentScreening == null) return;

            List<cPlate> ListPlatesToProcess = new List<cPlate>();

            if (this.ProcessModeplateByPlateToolStripMenuItem.Checked)
            {
                foreach (cPlate item in cGlobalInfo.CurrentScreening.ListPlatesActive)
                    ListPlatesToProcess.Add(item);

                PerformScreeningClustering(ListPlatesToProcess, true);
            }
            else if (this.ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                foreach (cPlate item in cGlobalInfo.CurrentScreening.ListPlatesActive)
                    ListPlatesToProcess.Add(item);
                PerformScreeningClustering(ListPlatesToProcess, false);
            }
            else if (this.ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
            {
                ListPlatesToProcess.Add(cGlobalInfo.CurrentScreening.ListPlatesActive[cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx]);
                PerformScreeningClustering(ListPlatesToProcess, true);
            }
        }

        public void PerformScreeningClustering(List<cPlate> ListPlatesToProcess, bool IsOneByOne)
        {


            this.Cursor = Cursors.WaitCursor;
            cMachineLearning MachineLearning = new cMachineLearning(this.GlobalInfo);
            cParamAlgo ParamAlgoForClustering = MachineLearning.AskAndGetClusteringAlgo();
            if (ParamAlgoForClustering == null)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            //DataTable dt = new DataTable();
            cExtendedTable dt = new cExtendedTable();


            for (int IdxDesc = 0; IdxDesc < cGlobalInfo.CurrentScreening.ListDescriptors.Count; IdxDesc++)
            {
                if (cGlobalInfo.CurrentScreening.ListDescriptors[IdxDesc].IsActive())
                    dt.Add(new cExtendedList(cGlobalInfo.CurrentScreening.ListDescriptors[IdxDesc].GetName()));
            }
            if (IsOneByOne)
            {
                foreach (cPlate itemPlate in ListPlatesToProcess)
                {
                    cListWells ListWell = new cListWells(null);

                    foreach (var item in itemPlate.ListActiveWells)
                        ListWell.Add(item);

                    dt = ListWell.GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors(), false, false);

                    MachineLearning.SelectedClusterer = MachineLearning.BuildClusterer(ParamAlgoForClustering, dt);

                    if (MachineLearning.SelectedClusterer != null)
                    {
                        double[] Assign = MachineLearning.EvaluteAndDisplayClusterer(richTextBoxInfoClustering,
                                                                null,
                                                                MachineLearning.CreateInstancesWithoutClass(dt)).getClusterAssignments();

                        MachineLearning.Classes = new cExtendedList();
                        MachineLearning.Classes.AddRange(Assign);
                    }
                    if (MachineLearning.Classes.Max() >= cGlobalInfo.ListWellClasses.Count)
                    {
                        MessageBox.Show("The number of clusters is higher than the supported number of classes. Operation cancelled !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    if (MachineLearning.Classes.IsContainNegative() || (MachineLearning.Classes.Count == 0))
                    {
                        MessageBox.Show("Negative or null cluster index identified. Operation cancelled !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    // ----- update well classes ------
                    int IdxWell = 0;
                    foreach (cWell item in itemPlate.ListActiveWells)
                        item.SetClass((int)MachineLearning.Classes[IdxWell++]);
                }
            }
            else
            {
                cListWells ListWell = new cListWells(null);
                foreach (cPlate itemPlate in ListPlatesToProcess)
                {
                    foreach (cWell item in itemPlate.ListActiveWells)
                    {
                        //cExtendedList ListValues = item.GetAverageValuesList(false);
                        //dt.Rows.Add();
                        //int RealIdx = 0;
                        //for (int IdxDesc = 0; IdxDesc < CompleteScreening.ListDescriptors.Count; IdxDesc++)
                        //{
                        //    if (CompleteScreening.ListDescriptors[IdxDesc].IsActive())
                        //        dt.Rows[dt.Rows.Count - 1][RealIdx++] = ListValues[IdxDesc];
                        ListWell.Add(item);
                        //}
                    }
                }

                dt = ListWell.GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors(), false, false);



                MachineLearning.SelectedClusterer = MachineLearning.BuildClusterer(ParamAlgoForClustering, dt);

                if (MachineLearning.SelectedClusterer != null)
                {
                    double[] Assign = MachineLearning.EvaluteAndDisplayClusterer(richTextBoxInfoClustering,
                                                            null,
                                                            MachineLearning.CreateInstancesWithoutClass(dt)).getClusterAssignments();

                    MachineLearning.Classes = new cExtendedList();
                    MachineLearning.Classes.AddRange(Assign);
                }
                if (MachineLearning.Classes.Max() >= cGlobalInfo.ListWellClasses.Count)
                {
                    MessageBox.Show("The number of cluster is higher than the supported number of classes. Operation cancelled !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                    return;
                }
                if (MachineLearning.Classes.IsContainNegative() || (MachineLearning.Classes.Count == 0))
                {
                    MessageBox.Show("Negative or null cluster index identified. Operation cancelled !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                    return;
                }
                int IdxWell = 0;
                // ----- update well classes ------
                foreach (cPlate itemPlate in ListPlatesToProcess)
                {
                    foreach (cWell item in itemPlate.ListActiveWells)
                        item.SetClass((int)MachineLearning.Classes[IdxWell++]);
                }
            }
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
            this.Cursor = Cursors.Default;
        }
        #endregion



        private void testRStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //foreach (string path in cGlobalInfo.RStat_engine.Evaluate(".libPaths()").AsCharacter())
            //{
            //    //Console.WriteLine(path);
            //}


            //string CurrentWorkingDirectory = Directory.GetCurrentDirectory();

            //cGlobalInfo.RStat_engine.Evaluate(".libPaths(\"D:/HCS-Analyzer/RLibs\")");

            //foreach (string path in cGlobalInfo.RStat_engine.Evaluate(".libPaths()").AsCharacter())
            //{
            //    //Console.WriteLine(path);
            //}

            //cRandomGenerator NewRNDData = new cRandomGenerator(2, 10);
            //NewRNDData.Run();

            //NewRNDData.GetOutPut()[0].Name = "group1";
            //NewRNDData.GetOutPut()[1].Name = "group2";

            //NumericMatrix Test = NewRNDData.GetOutPut().CopyTo_R_StatMatrix();

            //NumericVector group1 = NewRNDData.GetOutPut()[0].CopyTo_R_StatVector();
            //NumericVector group2 = NewRNDData.GetOutPut()[1].CopyTo_R_StatVector();


            //SymbolicExpression SE = cGlobalInfo.RStat_engine.Evaluate("library(\"LPCM\")");
            //foreach (string path in SE.AsCharacter())
            //{
            //    cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(path + "\n");
            //}


            ////GenericVector testResult0 = cGlobalInfo.RStat_engine.Evaluate("kmeans(group2,2)").AsList();
            //GenericVector testResult0 = cGlobalInfo.RStat_engine.Evaluate("ms(group2, h=0.05)").AsList();


            //// cGlobalInfo.RStat_engine.Evaluate("kmeans(group2,2)");

            //// DynamicVector testResult1 = cGlobalInfo.RStat_engine.Evaluate("hclust(group2)").AsVector();
            //// testResult0.
            //// cExtendedList L = new cExtendedList(testResult0);


            ////   testResult0[1].

            //GenericVector testResult = cGlobalInfo.RStat_engine.Evaluate("t.test(group1, group2)").AsList();
            //double p = testResult["p.value"].AsNumeric().First();

            //richTextBoxConsole.AppendText("\nP-value = " + p);
        }

        private void globalToolStripMenuItem_Click(object sender, EventArgs e)
        {

            GlobalSelection(false);
            // CompleteScreening.GlobalInfo.IsDisplayClassOnly = checkBoxDisplayClasses.Checked;
            if (cGlobalInfo.CurrentScreening != null)
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
        }

        private void globalIfOnlyActiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalSelection(true);

            if (cGlobalInfo.CurrentScreening != null)
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
        }

        private void HCSAnalyzer_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files[0].Remove(0, files[0].Length - 4) == ".csv")
                {
                    LoadCSVAssay(files, false);
                    UpdateUIAfterLoading();
                }
            }
            return;
        }

        void UpDateProcessModefromGUI()
        {
            if (ProcessModeplateByPlateToolStripMenuItem.Checked)
                cGlobalInfo.ProcessMode = eProcessMode.PLATE_BY_PLATE;
            else if (ProcessModeEntireScreeningToolStripMenuItem.Checked)
                cGlobalInfo.ProcessMode = eProcessMode.ENTIRE_SCREENING;
            else
                cGlobalInfo.ProcessMode = eProcessMode.SINGLE_PLATE;
        }

        private void processModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessModeplateByPlateToolStripMenuItem.Checked = false;
            ProcessModeEntireScreeningToolStripMenuItem.Checked = false;
            toolStripDropDownButtonProcessMode.Text = "Current Plate";

        }

        private void plateByPlateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessModeEntireScreeningToolStripMenuItem.Checked = false;
            ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked = false;
            toolStripDropDownButtonProcessMode.Text = "Plate by Plate";
        }

        private void entireScreeningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessModeplateByPlateToolStripMenuItem.Checked = false;
            ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked = false;
            toolStripDropDownButtonProcessMode.Text = "Entire Screening";
        }

        private void ToolStripMenuItem_ManualSelect(object sender, EventArgs e)
        {
            FormForManualClustering FormWindow = new FormForManualClustering(this.GlobalInfo);
            FormWindow.checkBoxTransferToHitList.Checked = true;
            FormWindow.Show();

        }

        private void ToolStripMenuItem_Clear(object sender, EventArgs e)
        {
            //listBoxSelectedWells.Items.Clear();
            this.listViewForListWell.Items.Clear();
            cGlobalInfo.ListSelectedWell.Clear();
        }

        //private void ToolStripMenuItem_SingleCellAnalysis(object sender, EventArgs e)
        //{
        //    GlobalInfo.ListSelectedWell.SingleCellBasedAnalysis();
        //}


        private void ToolStripMenuItem_RemoveFromList(object sender, EventArgs e)
        {
            ToolStripMenuItem TSMI = ((ToolStripMenuItem)sender);
            this.listViewForListWell.Items.Remove((ListViewItem)TSMI.Tag);
        }


        private void ToolStripMenuItem_CrossZFactor(object sender, EventArgs e)
        {
            cGUI_ListSingleClasse GUI_ListClasses = new cGUI_ListSingleClasse();
            GUI_ListClasses.IsCheckBoxes = true;

            if (GUI_ListClasses.Run().IsSucceed == false) return;
            cExtendedList ListClassSelected = GUI_ListClasses.GetOutPut();


            int ClassId = 0;
            //int NumActiveDesc = 0;
            //foreach (cDescriptorType DescType in this.GlobalInfo.CurrentScreening.ListDescriptors)
            //{
            //    if (DescType.IsActive() == false) continue;
            //    NumActiveDesc++;
            //}


            cExtendedTable FinalListValues = new cExtendedTable();
            cExtendedTable TmpTable = new cExtendedTable();



            TmpTable.Add(new cExtendedList("Sorted Z'"));
            TmpTable.ListRowNames = new List<string>();
            TmpTable[0].ListTags = new List<object>();
            //   TmpTable.ListTags = new List<object>();
            for (int IdxDesc = 0; IdxDesc < cGlobalInfo.CurrentScreening.ListDescriptors.Count; IdxDesc++)
            {
                cDescriptorType CurrentDescType = cGlobalInfo.CurrentScreening.ListDescriptors[IdxDesc];
                if (CurrentDescType.IsActive() == false) continue;

                cExtendedTable ListSource = new cExtendedTable();

                // int IdxClasse =0;
                foreach (var item in cGlobalInfo.ListWellClasses)
                {
                    // if (ListClassSelected[0][IdxClasse] == 1)
                    ListSource.Add(new cExtendedList(item.Name));

                    // if (ListClassSelected[1][IdxClasse] == 1)
                    //ListDestination.Add(new cExtendedList(item.Name));

                    // IdxClasse++;
                }

                foreach (cWell TmpWell in cGlobalInfo.ListSelectedWell)
                {
                    ClassId = TmpWell.GetCurrentClassIdx();
                    if (ClassId > -1)
                    {
                        if (GUI_ListClasses.GetOutPut()[ClassId] == 1)
                            ListSource[ClassId].Add(TmpWell.GetAverageValue(CurrentDescType));

                        //if (GUI_ListClasses.GetOutPut()[1][ClassId] == 1)
                        //ListDestination[ClassId].Add(TmpWell.GetAverageValue(CurrentDescType));
                    }
                }

                int InitCount = ListSource.Count;
                for (int i = 0; i < ListSource.Count; i++)
                {
                    if (ListSource[i].Count < 2)
                    {
                        ListSource.RemoveAt(i);
                        i--;
                    }
                }

                // if (ListSource.Count == 0) return;

                cZFactor ZF = new cZFactor();
                ZF.SetInputData(ListSource);
                cFeedBackMessage FM = ZF.Run();
                richTextBoxConsole.AppendText(FM.Message);
                if (!FM.IsSucceed) return;

                cExtendedTable ListValues = ZF.GetOutPut();

                for (int i = 0; i < ListValues.Count; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        TmpTable[0].Add(ListValues[i][j]);
                        TmpTable.ListRowNames.Add(ListValues[i].Name + " vs. " + ListValues[j].Name + " - [" + CurrentDescType.GetName() + "]");
                        TmpTable[0].ListTags.Add(CurrentDescType);
                    }
                }

            }
            cSort S = new cSort();
            S.SetInputData(TmpTable);
            S.IsAscending = false;
            S.Run();

            //cViewerTable VT1 = new cViewerTable();
            //VT1.SetInputData(ListValues);
            //VT1.Run();

            cViewerTable VT2 = new cViewerTable();
            VT2.SetInputData(S.GetOutPut());
            VT2.Run();

            //cDesignerSplitter DS = new cDesignerSplitter();
            //DS.Orientation = Orientation.Vertical;
            //DS.SetInputData(VT2.GetOutPut());
            //DS.SetInputData(VT1.GetOutPut());
            //DS.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.Title = "Cross Classes Z'";
            DTW.SetInputData(VT2.GetOutPut());
            DTW.Run();
            DTW.Display();
        }

        private void ToolStripMenuItem_ZFactor(object sender, EventArgs e)
        {
            cGUI_2ClassesSelection GUI_ListClasses = new cGUI_2ClassesSelection();

            if (GUI_ListClasses.Run(this.GlobalInfo).IsSucceed == false) return;
            cExtendedTable ListClassSelected = GUI_ListClasses.GetOutPut();

            int IdxClassNeg = -1;
            int IdxClassPos = -1;
            for (int IdxC = 0; IdxC < ListClassSelected[0].Count; IdxC++)
            {
                if (ListClassSelected[0][IdxC] == 1) IdxClassNeg = IdxC;
                if (ListClassSelected[1][IdxC] == 1) IdxClassPos = IdxC;
            }

            string SubTitle = "Z-Factor";
            cListWells ListWellsToProcess1 = new cListWells(null);
            cListWells ListWellsToProcess2 = new cListWells(null);

            foreach (cWell item in cGlobalInfo.ListSelectedWell)
            {
                if (item.GetCurrentClassIdx() != -1)
                {
                    if (ListClassSelected[0][item.GetCurrentClassIdx()] == 1)
                        ListWellsToProcess1.Add(item);
                    if (ListClassSelected[1][item.GetCurrentClassIdx()] == 1)
                        ListWellsToProcess2.Add(item);
                }
            }

            cExtendedTable NewTable1 = new cExtendedTable(ListWellsToProcess1, true);
            cExtendedTable NewTable2 = new cExtendedTable(ListWellsToProcess2, true);

            if ((NewTable1.Count == 0) || (NewTable1[0].Count < 2) || (NewTable2.Count == 0) || (NewTable2[0].Count < 2))
            {
                if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                {
                    MessageBox.Show("Insufficient number of control wells", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            cExtendedList ListZ = new cExtendedList();
            List<cDescriptorType> ListDescForZFactor = new List<cDescriptorType>();
            List<string> ListNames = new List<string>();
            int RealIdx = 0;
            for (int IDxDesc = 0; IDxDesc < cGlobalInfo.CurrentScreening.ListDescriptors.Count; IDxDesc++)
            {
                if (!cGlobalInfo.CurrentScreening.ListDescriptors[IDxDesc].IsActive()) continue;

                cExtendedTable TableForZ = new cExtendedTable();

                TableForZ.Add(NewTable1[RealIdx]);
                TableForZ.Add(NewTable2[RealIdx]);
                RealIdx++;

                cZFactor ZF = new cZFactor();
                ZF.SetInputData(TableForZ);
                ZF.IsRobust = _QCZRobustItem.Checked;
                ZF.Run();
                ListZ.Add(ZF.GetOutPut()[0][1]);

                ListDescForZFactor.Add(cGlobalInfo.CurrentScreening.ListDescriptors[IDxDesc]);
            }

            cExtendedTable ET = new cExtendedTable(new cExtendedTable(ListZ));
            ET[0].ListTags = new List<object>();
            ET[0].ListTags.AddRange(ListDescForZFactor);
            ET.Name = SubTitle + " - " + cGlobalInfo.ListWellClasses[IdxClassNeg].Name + " (" + NewTable1[0].Count + " wells) vs. " + cGlobalInfo.ListWellClasses[IdxClassPos].Name + " (" + NewTable2[0].Count + " wells)";
            ET[0].Name = ET.Name;

            cSort S = new cSort();
            S.SetInputData(ET);
            S.ColumnIndexForSorting = 0;
            S.Run();

            cViewerGraph1D VG1 = new cViewerGraph1D();
            VG1.SetInputData(S.GetOutPut());

            VG1.Chart.LabelAxisY = SubTitle;
            VG1.Chart.LabelAxisX = "Descriptor";
            VG1.Chart.IsZoomableX = true;
            VG1.Chart.IsBar = true;
            VG1.Chart.IsBorder = true;
            VG1.Chart.IsDisplayValues = true;
            VG1.Chart.IsShadow = true;
            VG1.Chart.MarkerSize = 4;
            VG1.Chart.DefaultAxisYMax = new cExtendedList();
            VG1.Chart.DefaultAxisYMax.Add(1);
            //VG1.Title = TmpPlate.Name;

            Classes.Base_Classes.General.cLineHorizontalForGraph VLZ05 = new Classes.Base_Classes.General.cLineHorizontalForGraph(.5);
            VLZ05.IsAllowMoving = true;
            VG1.Chart.ListHorizontalLines.Add(VLZ05);

            VG1.Run();

            cDesignerSplitter DS = new cDesignerSplitter();
            DS.Orientation = Orientation.Vertical;
            DS.SetInputData(VG1.GetOutPut());

            cSort S1 = new cSort();
            S1.SetInputData(ET);
            S1.ColumnIndexForSorting = 0;
            S1.IsAscending = false;
            S1.Run();

            cViewerTable VT = new cViewerTable();
            VT.SetInputData(S1.GetOutPut());
            VT.Run();

            DS.SetInputData(VT.GetOutPut());
            DS.Run();

            cDisplayToWindow CDW = new cDisplayToWindow();
            CDW.SetInputData(DS.GetOutPut());//VG1.GetOutPut());

            CDW.Title = "List Wells - Z Factor";
            CDW.Run();
            CDW.Display();
        }

        void RefreshListViewForWellList()
        {

            for (int i = 0; i < listViewForListWell.Items.Count; i++)
            {
                ListViewItem item = listViewForListWell.Items[i];
                cWell TmpWell = (cWell)item.Tag;


                item.SubItems[0].Text = TmpWell.AssociatedPlate.GetName() + " : " + TmpWell.GetPosX() + "x" + TmpWell.GetPosY();
                object ObjConcentration = TmpWell.ListProperties.FindValueByName("Concentration");
                if (ObjConcentration == null)
                    item.SubItems[1].Text = "n.a.";
                else
                    item.SubItems[1].Text = ((double)ObjConcentration).ToString();
                item.SubItems[2].Text = TmpWell.GetCurrentClassIdx().ToString();
                item.BackColor = TmpWell.GetClassColor();

            }

        }

        private void ToolStripMenuItem_Refresh(object sender, EventArgs e)
        {
            RefreshListViewForWellList();
        }

        private void testBoxPlotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            if (GUI_ListClasses.Run().IsSucceed == false) return;
            cExtendedList ListClassSelected = GUI_ListClasses.GetOutPut()[0];

            if (ListClassSelected.Sum() < 1)
            {
                MessageBox.Show("At least one classe has to be selected.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            cPlate TmpPlate = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate();
            cListWells ListWellsToProcess = new cListWells(null);
            // cExtendedList ListClasses = new cExtendedList();
            // ListClasses.Name = "Classes";
            foreach (cWell item in TmpPlate.ListActiveWells)
            {
                if (item.GetCurrentClassIdx() != -1)
                {
                    if (ListClassSelected[item.GetCurrentClassIdx()] == 1)
                    {
                        ListWellsToProcess.Add(item);
                        // ListClasses.Add(item.GetClassIdx());
                    }
                }
            }

            cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);
            // NewTable.Add(ListClasses);


            cViewerBoxPlot CV = new cViewerBoxPlot();
            CV.SetInputData(NewTable);
            CV.Run();

            //cDesignerSinglePanel CDP = new cDesignerSinglePanel();
            //CDP.SetInputData(CV.GetOutPut());
            //CDP.Run();

            cDisplayToWindow CDW = new cDisplayToWindow();
            CDW.SetInputData(CV.GetOutPut());
            CDW.Title = CV.Title;
            CDW.Run();
            CDW.Display();

        }

        private void zScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region obsolete
            //List<double> Pos = new List<double>();
            //List<double> Neg = new List<double>();
            //List<cSimpleSignature> ZFactorList = new List<cSimpleSignature>();

            //int NumDesc = CompleteScreening.ListDescriptors.Count;

            //cWell TempWell;
            //// loop on all the desciptors
            //for (int Desc = 0; Desc < NumDesc; Desc++)
            //{
            //    Pos.Clear();
            //    Neg.Clear();

            //    if (CompleteScreening.ListDescriptors[Desc].IsActive() == false) continue;

            //    for (int row = 0; row < CompleteScreening.Rows; row++)
            //        for (int col = 0; col < CompleteScreening.Columns; col++)
            //        {
            //            TempWell = CompleteScreening.GetCurrentDisplayPlate().GetWell(col, row, true);
            //            if (TempWell == null) continue;
            //            else
            //            {
            //                if (TempWell.GetClassIdx() == 0)
            //                    Pos.Add(TempWell.ListDescriptors[Desc].GetValue());
            //                if (TempWell.GetClassIdx() == 1)
            //                    Neg.Add(TempWell.ListDescriptors[Desc].GetValue());
            //            }
            //        }
            //    if (Pos.Count < 3)
            //    {
            //        MessageBox.Show("No or not enough positive controls !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return;
            //    }
            //    if (Neg.Count < 3)
            //    {
            //        MessageBox.Show("No or not enough negative controls !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return;
            //    }


            //    double ZScore = 1 - 3 * (std(Pos.ToArray()) + std(Neg.ToArray())) / (Math.Abs(Mean(Pos.ToArray()) - Mean(Neg.ToArray())));
            //    GlobalInfo.ConsoleWriteLine(CompleteScreening.ListDescriptors[Desc].GetName() + ", Z-Score = " + ZScore);
            //    cSimpleSignature TmpDesc = new cSimpleSignature(CompleteScreening.ListDescriptors[Desc].GetName(), ZScore);
            //    ZFactorList.Add(TmpDesc);
            //}

            //ZFactorList.Sort(delegate(cSimpleSignature p1, cSimpleSignature p2) { return p1.AverageValue.CompareTo(p2.AverageValue); });

            //Series CurrentSeries = new Series();
            //CurrentSeries.ChartType = SeriesChartType.Column;
            //CurrentSeries.ShadowOffset = 1;

            //Series SeriesLine = new Series();
            //SeriesLine.Name = "SeriesLine";
            //SeriesLine.ShadowOffset = 1;
            //SeriesLine.ChartType = SeriesChartType.Line;

            //int RealIdx = 0;
            //for (int IdxValue = 0; IdxValue < ZFactorList.Count; IdxValue++)
            //{
            //    if (double.IsNaN(ZFactorList[IdxValue].AverageValue)) continue;
            //    if (double.IsInfinity(ZFactorList[IdxValue].AverageValue)) continue;

            //    CurrentSeries.Points.Add(ZFactorList[IdxValue].AverageValue);
            //    CurrentSeries.Points[RealIdx].Label = ZFactorList[IdxValue].AverageValue.ToString("N2");
            //    CurrentSeries.Points[RealIdx].Font = new Font("Arial", 10);
            //    CurrentSeries.Points[RealIdx].ToolTip = ZFactorList[IdxValue].Name;
            //    CurrentSeries.Points[RealIdx].AxisLabel = ZFactorList[IdxValue].Name;

            //    SeriesLine.Points.Add(ZFactorList[IdxValue].AverageValue);
            //    SeriesLine.Points[RealIdx].BorderColor = Color.Black;
            //    SeriesLine.Points[RealIdx].MarkerStyle = MarkerStyle.Circle;
            //    SeriesLine.Points[RealIdx].MarkerSize = 4;
            //    RealIdx++;
            //}

            //SimpleForm NewWindow = new SimpleForm(CompleteScreening);
            //int thisWidth = 200 * RealIdx;
            //if (thisWidth > (int)GlobalInfo.OptionsWindow.numericUpDownMaximumWidth.Value) thisWidth = (int)GlobalInfo.OptionsWindow.numericUpDownMaximumWidth.Value;
            //NewWindow.Width = thisWidth;
            //NewWindow.Height = 400;
            //NewWindow.Text = "Z-factors";

            //ChartArea CurrentChartArea = new ChartArea();
            //CurrentChartArea.BorderColor = Color.Black;
            //CurrentChartArea.AxisX.Interval = 1;
            //NewWindow.chartForSimpleForm.Series.Add(CurrentSeries);
            //NewWindow.chartForSimpleForm.Series.Add(SeriesLine);

            //CurrentChartArea.AxisX.IsLabelAutoFit = true;
            //NewWindow.chartForSimpleForm.ChartAreas.Add(CurrentChartArea);

            //CurrentChartArea.Axes[1].Maximum = 2;
            //CurrentChartArea.Axes[1].IsMarksNextToAxis = true;
            //CurrentChartArea.Axes[0].MajorGrid.Enabled = false;
            //CurrentChartArea.Axes[1].MajorGrid.Enabled = false;

            //NewWindow.chartForSimpleForm.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            //CurrentChartArea.BackGradientStyle = GradientStyle.TopBottom;
            //CurrentChartArea.BackColor = CompleteScreening.GlobalInfo.OptionsWindow.panel1.BackColor;
            //CurrentChartArea.BackSecondaryColor = Color.White;

            //CurrentChartArea.AxisX.ScaleView.Zoomable = true;
            //CurrentChartArea.AxisY.ScaleView.Zoomable = true;

            //Title CurrentTitle = new Title(CompleteScreening.GetCurrentDisplayPlate().Name + " Z-factors");
            //CurrentTitle.Font = new System.Drawing.Font("Arial", 11, FontStyle.Bold);
            //NewWindow.chartForSimpleForm.Titles.Add(CurrentTitle);

            //NewWindow.Show();
            //NewWindow.chartForSimpleForm.Update();
            //NewWindow.chartForSimpleForm.Show();
            //NewWindow.AutoScroll = true;
            //NewWindow.Controls.AddRange(new System.Windows.Forms.Control[] { NewWindow.chartForSimpleForm });
            #endregion


            cGUI_2ClassesSelection GUI_ListClasses = new cGUI_2ClassesSelection();
            if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
            {
                GUI_ListClasses.PanelRight_IsCheckBoxes = true;
            }


            if (GUI_ListClasses.Run(this.GlobalInfo).IsSucceed == false) return;
            cExtendedTable ListClassSelected = GUI_ListClasses.GetOutPut();

            int IdxClassNeg = -1;
            int IdxClassPos = -1;
            for (int IdxC = 0; IdxC < ListClassSelected[0].Count; IdxC++)
            {
                if (ListClassSelected[0][IdxC] == 1) IdxClassNeg = IdxC;
                if (ListClassSelected[1][IdxC] == 1) IdxClassPos = IdxC;
            }

            string SubTitle = "Z-Factor";

            if (_QCZRobustItem.Checked)
                SubTitle += " (Robust)";


            #region Single plate
            cDesignerTab DT = new cDesignerTab();
            //   cDesignerMultiChoices DT = new cDesignerMultiChoices();
            if ((ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)/*||(ProcessModeEntireScreeningToolStripMenuItem.Checked)*/)
            {
                List<cPlate> ListPlatesToProcess = new List<cPlate>();

                cPlate TmpPlate = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate();

                // foreach (cPlate TmpPlate in ListPlatesToProcess)
                // GUI_ListClasses.Get
                // ListClassSelected

                List<int> ListCheckBoxes = new List<int>();
                for (int i = 0; i < ListClassSelected[1].Count; i++)
                {
                    if (ListClassSelected[1][i] > 0)
                        ListCheckBoxes.Add(i);
                }

                for (int IdxClassSelected = 0; IdxClassSelected < ListCheckBoxes.Count; IdxClassSelected++)
                {
                    cListWells ListWellsToProcess1 = new cListWells(null);
                    cListWells ListWellsToProcess2 = new cListWells(null);

                    int CurrentClassTobeProcessed = ListCheckBoxes[IdxClassSelected];

                    foreach (cWell item in TmpPlate.ListActiveWells)
                    {
                        if (item.GetCurrentClassIdx() != -1)
                        {
                            if (ListClassSelected[0][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess1.Add(item);

                            if (item.GetCurrentClassIdx() == CurrentClassTobeProcessed)
                                //                            if (ListClassSelected[1][ListCheckBoxes[IdxClassSelected]] == 1)
                                ListWellsToProcess2.Add(item);
                        }
                    }

                    cExtendedTable NewTable1 = new cExtendedTable(ListWellsToProcess1, true);
                    cExtendedTable NewTable2 = new cExtendedTable(ListWellsToProcess2, true);

                    if ((NewTable1.Count == 0) || (NewTable1[0].Count < 2) || (NewTable2.Count == 0) || (NewTable2[0].Count < 2))
                    {
                        if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                        {
                            MessageBox.Show("Insufficient number of control wells", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //else
                        //    continue;
                    }

                    cExtendedList ListZ = new cExtendedList();
                    List<cDescriptorType> ListDescForZFactor = new List<cDescriptorType>();
                    List<string> ListNames = new List<string>();
                    int RealIdx = 0;
                    for (int IDxDesc = 0; IDxDesc < cGlobalInfo.CurrentScreening.ListDescriptors.Count; IDxDesc++)
                    {
                        if (!cGlobalInfo.CurrentScreening.ListDescriptors[IDxDesc].IsActive()) continue;

                        cExtendedTable TableForZ = new cExtendedTable();

                        TableForZ.Add(NewTable1[RealIdx]);
                        TableForZ.Add(NewTable2[RealIdx]);
                        RealIdx++;

                        cZFactor ZF = new cZFactor();
                        ZF.SetInputData(TableForZ);
                        ZF.IsRobust = _QCZRobustItem.Checked;
                        ZF.Run();
                        ListZ.Add(ZF.GetOutPut()[0][1]);

                        ListDescForZFactor.Add(cGlobalInfo.CurrentScreening.ListDescriptors[IDxDesc]);

                    }

                    cExtendedTable ET = new cExtendedTable(new cExtendedTable(ListZ));
                    ET[0].ListTags = new List<object>();
                    ET[0].ListTags.AddRange(ListDescForZFactor);
                    ET.Name = TmpPlate.GetName() + "\n" + SubTitle + " - " + cGlobalInfo.ListWellClasses[IdxClassNeg].Name + " (" + NewTable1[0].Count + " wells) vs. " + cGlobalInfo.ListWellClasses[CurrentClassTobeProcessed].Name + " (" + NewTable2[0].Count + " wells)";
                    ET[0].Name = ET.Name;

                    cSort S = new cSort();
                    S.SetInputData(ET);
                    S.ColumnIndexForSorting = 0;
                    S.Run();

                    //ZFactorList.Sort(delegate(cSimpleSignature p1, cSimpleSignature p2) { return p1.AverageValue.CompareTo(p2.AverageValue); });

                    cViewerGraph1D VG1 = new cViewerGraph1D();
                    VG1.SetInputData(S.GetOutPut());

                    VG1.Chart.LabelAxisY = SubTitle;
                    VG1.Chart.LabelAxisX = "Descriptor";
                    VG1.Chart.IsZoomableX = true;
                    VG1.Chart.IsBar = true;
                    VG1.Chart.IsBorder = true;
                    VG1.Chart.IsDisplayValues = true;
                    VG1.Chart.IsShadow = true;
                    VG1.Chart.MarkerSize = 4;
                    VG1.Chart.DefaultAxisYMax = new cExtendedList();
                    VG1.Chart.DefaultAxisYMax.Add(1);

                    if (!cGlobalInfo.OptionsWindow.FFAllOptions.checkBoxZscoreMinValueAutomated.Checked)
                    {
                        VG1.Chart.DefaultAxisYMin = new cExtendedList();
                        VG1.Chart.DefaultAxisYMin.Add((double)cGlobalInfo.OptionsWindow.FFAllOptions.numericUpDownZscoreMinValue.Value);
                    }

                    VG1.Title = TmpPlate.GetName();

                    Classes.Base_Classes.General.cLineHorizontalForGraph VLZ05 = new Classes.Base_Classes.General.cLineHorizontalForGraph(.5);
                    VLZ05.IsAllowMoving = true;
                    VG1.Chart.ListHorizontalLines.Add(VLZ05);

                    VG1.Run();

                    cDesignerSplitter DS = new cDesignerSplitter();
                    DS.Orientation = Orientation.Vertical;
                    DS.SetInputData(VG1.GetOutPut());

                    cSort S1 = new cSort();
                    S1.SetInputData(ET);
                    S1.ColumnIndexForSorting = 0;
                    S1.IsAscending = false;
                    S1.Run();

                    cViewerTable VT = new cViewerTable();
                    VT.SetInputData(S1.GetOutPut());
                    VT.Run();

                    DS.SetInputData(VT.GetOutPut());
                    DS.Title = cGlobalInfo.ListWellClasses[IdxClassNeg].Name + " vs. " + cGlobalInfo.ListWellClasses[CurrentClassTobeProcessed].Name;
                    DS.Run();

                    DT.SetInputData(DS.GetOutPut());
                }
                DT.Run();

                cDisplayToWindow CDW = new cDisplayToWindow();
                CDW.SetInputData(DT.GetOutPut());//VG1.GetOutPut());



                CDW.Title = SubTitle + " - " + TmpPlate.GetName();


                CDW.Run();
                CDW.Display();
            }

            #endregion

            #region plate by plate

            DT = new cDesignerTab();
            //   cDesignerMultiChoices DT = new cDesignerMultiChoices();
            if ((ProcessModeplateByPlateToolStripMenuItem.Checked)/*||(ProcessModeEntireScreeningToolStripMenuItem.Checked)*/)
            {
                List<cPlate> ListPlatesToProcess = new List<cPlate>();
                if ((ProcessModeplateByPlateToolStripMenuItem.Checked)/*||(ProcessModeEntireScreeningToolStripMenuItem.Checked)*/)
                {
                    foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                        ListPlatesToProcess.Add(TmpPlate);
                }


                foreach (cPlate TmpPlate in ListPlatesToProcess)
                {
                    cListWells ListWellsToProcess1 = new cListWells(null);
                    cListWells ListWellsToProcess2 = new cListWells(null);

                    foreach (cWell item in TmpPlate.ListActiveWells)
                    {
                        if (item.GetCurrentClassIdx() != -1)
                        {
                            if (ListClassSelected[0][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess1.Add(item);
                            if (ListClassSelected[1][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess2.Add(item);
                        }
                    }

                    cExtendedTable NewTable1 = new cExtendedTable(ListWellsToProcess1, true);
                    cExtendedTable NewTable2 = new cExtendedTable(ListWellsToProcess2, true);

                    if ((NewTable1.Count == 0) || (NewTable1[0].Count < 2) || (NewTable2.Count == 0) || (NewTable2[0].Count < 2))
                    {
                        if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                        {
                            MessageBox.Show("Insufficient number of control wells", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                            continue;
                    }

                    cExtendedList ListZ = new cExtendedList();
                    List<cDescriptorType> ListDescForZFactor = new List<cDescriptorType>();
                    List<string> ListNames = new List<string>();
                    int RealIdx = 0;
                    for (int IDxDesc = 0; IDxDesc < cGlobalInfo.CurrentScreening.ListDescriptors.Count; IDxDesc++)
                    {
                        if (!cGlobalInfo.CurrentScreening.ListDescriptors[IDxDesc].IsActive()) continue;

                        cExtendedTable TableForZ = new cExtendedTable();

                        TableForZ.Add(NewTable1[RealIdx]);
                        TableForZ.Add(NewTable2[RealIdx]);
                        RealIdx++;

                        cZFactor ZF = new cZFactor();
                        ZF.SetInputData(TableForZ);
                        ZF.IsRobust = _QCZRobustItem.Checked;
                        ZF.Run();
                        ListZ.Add(ZF.GetOutPut()[0][1]);

                        ListDescForZFactor.Add(cGlobalInfo.CurrentScreening.ListDescriptors[IDxDesc]);

                    }

                    cExtendedTable ET = new cExtendedTable(new cExtendedTable(ListZ));
                    ET[0].ListTags = new List<object>();
                    ET[0].ListTags.AddRange(ListDescForZFactor);
                    ET.Name = TmpPlate.GetName() + "\n" + SubTitle + " - " + cGlobalInfo.ListWellClasses[IdxClassNeg].Name + " (" + NewTable1[0].Count + " wells) vs. " + cGlobalInfo.ListWellClasses[IdxClassPos].Name + " (" + NewTable2[0].Count + " wells)";
                    ET[0].Name = ET.Name;

                    cSort S = new cSort();
                    S.SetInputData(ET);
                    S.ColumnIndexForSorting = 0;
                    S.Run();

                    //ZFactorList.Sort(delegate(cSimpleSignature p1, cSimpleSignature p2) { return p1.AverageValue.CompareTo(p2.AverageValue); });

                    cViewerGraph1D VG1 = new cViewerGraph1D();
                    VG1.SetInputData(S.GetOutPut());

                    VG1.Chart.LabelAxisY = SubTitle;
                    VG1.Chart.LabelAxisX = "Descriptor";
                    VG1.Chart.IsZoomableX = true;
                    VG1.Chart.IsBar = true;
                    VG1.Chart.IsBorder = true;
                    VG1.Chart.IsDisplayValues = true;
                    VG1.Chart.IsShadow = true;
                    VG1.Chart.MarkerSize = 4;
                    VG1.Chart.DefaultAxisYMax = new cExtendedList();
                    VG1.Chart.DefaultAxisYMax.Add(1);

                    if (!cGlobalInfo.OptionsWindow.FFAllOptions.checkBoxZscoreMinValueAutomated.Checked)
                    {
                        VG1.Chart.DefaultAxisYMin = new cExtendedList();
                        VG1.Chart.DefaultAxisYMin.Add((double)cGlobalInfo.OptionsWindow.FFAllOptions.numericUpDownZscoreMinValue.Value);
                    }

                    VG1.Title = TmpPlate.GetName();

                    Classes.Base_Classes.General.cLineHorizontalForGraph VLZ05 = new Classes.Base_Classes.General.cLineHorizontalForGraph(.5);
                    VLZ05.IsAllowMoving = true;
                    VG1.Chart.ListHorizontalLines.Add(VLZ05);

                    VG1.Run();

                    cDesignerSplitter DS = new cDesignerSplitter();
                    DS.Orientation = Orientation.Vertical;
                    DS.SetInputData(VG1.GetOutPut());

                    cSort S1 = new cSort();
                    S1.SetInputData(ET);
                    S1.ColumnIndexForSorting = 0;
                    S1.IsAscending = false;
                    S1.Run();

                    cViewerTable VT = new cViewerTable();
                    VT.SetInputData(S1.GetOutPut());
                    VT.Run();

                    DS.SetInputData(VT.GetOutPut());
                    DS.Title = TmpPlate.GetName();
                    DS.Run();

                    DT.SetInputData(DS.GetOutPut());
                }
                DT.Run();

                cDisplayToWindow CDW = new cDisplayToWindow();
                CDW.SetInputData(DT.GetOutPut());//VG1.GetOutPut());



                CDW.Title = SubTitle + " - " + ListPlatesToProcess.Count + " plates";

                CDW.Run();
                CDW.Display();
            }
            #endregion

            #region entire screening
            else if (ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                List<cPlate> ListPlatesToProcess = new List<cPlate>();
                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                    ListPlatesToProcess.Add(TmpPlate);

                cExtendedList ListZ = new cExtendedList();
                List<cPlate> ListPlatesForZFactor = new List<cPlate>();
                foreach (cPlate TmpPlate in ListPlatesToProcess)
                {
                    cListWells ListWellsToProcess1 = new cListWells(null);
                    cListWells ListWellsToProcess2 = new cListWells(null);

                    foreach (cWell item in TmpPlate.ListActiveWells)
                    {
                        if (item.GetCurrentClassIdx() != -1)
                        {
                            if (ListClassSelected[0][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess1.Add(item);
                            if (ListClassSelected[1][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess2.Add(item);
                        }
                    }

                    cExtendedTable NewTable1 = new cExtendedTable(ListWellsToProcess1, cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor()));
                    cExtendedTable NewTable2 = new cExtendedTable(ListWellsToProcess2, cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor()));
                    //cExtendedTable NewTable2 = new cExtendedTable(ListWellsToProcess2, false);

                    if ((NewTable1.Count == 0) || (NewTable1[0].Count < 2) || (NewTable2.Count == 0) || (NewTable2[0].Count < 2))
                    {
                        if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                        {
                            MessageBox.Show("Insufficient number of control wells", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                            continue;
                    }

                    cExtendedTable TableForZ = new cExtendedTable();

                    TableForZ.Add(NewTable1[0]);
                    TableForZ.Add(NewTable2[0]);

                    cZFactor ZF = new cZFactor();
                    ZF.IsRobust = _QCZRobustItem.Checked;
                    ZF.SetInputData(TableForZ);
                    ZF.Run();
                    double Zfactor = ZF.GetOutPut()[0][1];
                    ListZ.Add(Zfactor);

                    // update plate quality
                    TmpPlate.ListProperties.UpdateValueByName("Quality", Math.Exp(Zfactor - 1));
                    cProperty Prop = TmpPlate.ListProperties.FindByName("Quality");
                    Prop.Info = ZF.GetInfo();

                    ListPlatesForZFactor.Add(TmpPlate);
                }


                cExtendedTable ET = new cExtendedTable(new cExtendedTable(ListZ));
                ET[0].ListTags = new List<object>();
                ET[0].ListTags.AddRange(ListPlatesForZFactor);
                ET.Name = SubTitle + " - " + cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName() + " - " + cGlobalInfo.ListWellClasses[IdxClassNeg].Name + /*" (" + NewTable1[0].Count + " wells)*/ " vs. " + cGlobalInfo.ListWellClasses[IdxClassPos].Name;// +" (" + NewTable2[0].Count + " wells)";
                ET[0].Name = ET.Name;

                cViewerGraph1D VG1 = new cViewerGraph1D();
                VG1.SetInputData(ET);

                VG1.Chart.LabelAxisY = SubTitle;
                VG1.Chart.LabelAxisX = "Plate";
                VG1.Chart.IsZoomableX = true;
                VG1.Chart.IsBar = true;
                VG1.Chart.IsBorder = true;
                VG1.Chart.IsDisplayValues = true;
                VG1.Chart.IsShadow = true;
                VG1.Chart.MarkerSize = 4;
                VG1.Chart.DefaultAxisYMax = new cExtendedList();
                VG1.Chart.DefaultAxisYMax.Add(1);

                if (!cGlobalInfo.OptionsWindow.FFAllOptions.checkBoxZscoreMinValueAutomated.Checked)
                {
                    VG1.Chart.DefaultAxisYMin = new cExtendedList();
                    VG1.Chart.DefaultAxisYMin.Add((double)cGlobalInfo.OptionsWindow.FFAllOptions.numericUpDownZscoreMinValue.Value);
                }


                Classes.Base_Classes.General.cLineHorizontalForGraph VLZ05 = new Classes.Base_Classes.General.cLineHorizontalForGraph(.5);
                VLZ05.IsAllowMoving = true;
                VG1.Chart.ListHorizontalLines.Add(VLZ05);

                VG1.Title = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
                VG1.Run();

                cDisplayToWindow CDW = new cDisplayToWindow();
                CDW.SetInputData(VG1.GetOutPut());
                CDW.Title = SubTitle + " - " + ListPlatesToProcess.Count + " plates";
                CDW.Run();
                CDW.Display();
            #endregion
            }
        }

        private void normalProbabilityPlotToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            GUI_ListClasses.IsCheckBoxes = true;
            GUI_ListClasses.IsSelectAll = true;

            if (GUI_ListClasses.Run().IsSucceed == false) return;
            cExtendedTable ListClassSelected = new cExtendedTable(GUI_ListClasses.GetOutPut());// GetOutPut();

            int IdxClass = -1;
            for (int IdxC = 0; IdxC < ListClassSelected[0].Count; IdxC++)
            {
                if (ListClassSelected[0][IdxC] == 1) IdxClass = IdxC;
            }

            #region single plate and plate by plate
            cDescriptorType CurrentDesc = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor();
            cDesignerTab DT = new cDesignerTab();
            if ((ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked) || (ProcessModeplateByPlateToolStripMenuItem.Checked)/*||(ProcessModeEntireScreeningToolStripMenuItem.Checked)*/)
            {
                List<cPlate> ListPlatesToProcess = new List<cPlate>();
                if ((ProcessModeplateByPlateToolStripMenuItem.Checked))
                {
                    foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                        ListPlatesToProcess.Add(TmpPlate);
                }
                else
                    ListPlatesToProcess.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());

                foreach (cPlate TmpPlate in ListPlatesToProcess)
                {
                    cListWells ListWellsToProcess1 = new cListWells(null);
                    foreach (cWell item in TmpPlate.ListActiveWells)
                    {
                        if (item.GetCurrentClassIdx() != -1)
                        {
                            if (ListClassSelected[0][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess1.Add(item);
                        }
                    }

                    cComputeAndDisplayNormalPlot CADP = new cComputeAndDisplayNormalPlot();
                    CADP.SetInputData(ListWellsToProcess1);
                    CADP.Run();
                    cExtendedControl ControlForTab = CADP.GetOutPut();

                    ControlForTab.Title = TmpPlate.GetName();

                    DT.SetInputData(ControlForTab);
                }
                DT.Run();

                cDisplayToWindow CDW = new cDisplayToWindow();
                CDW.SetInputData(DT.GetOutPut());//VG1.GetOutPut());


                if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                    CDW.Title = "Normal Probability Plot - " + CurrentDesc.GetName() + " - " + ListPlatesToProcess[0].GetName();
                else
                    CDW.Title = "Normal Probability Plot - " + CurrentDesc.GetName() + " - " + ListPlatesToProcess.Count + " plates";

                CDW.Run();
                CDW.Display();
            }
            #endregion
            //#region entire screening
            else if (ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                List<cPlate> ListPlatesToProcess = new List<cPlate>();
                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                    ListPlatesToProcess.Add(TmpPlate);

                cListWells ListWellsToProcess1 = new cListWells(null);

                foreach (cPlate TmpPlate in ListPlatesToProcess)
                {

                    foreach (cWell item in TmpPlate.ListActiveWells)
                    {
                        if (item.GetCurrentClassIdx() != -1)
                        {
                            if (ListClassSelected[0][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess1.Add(item);
                        }
                    }
                }

                cComputeAndDisplayNormalPlot CADP = new cComputeAndDisplayNormalPlot();
                CADP.SetInputData(ListWellsToProcess1);
                CADP.Run();

                cExtendedControl ControlForTab = CADP.GetOutPut();

                cDisplayToWindow CDW = new cDisplayToWindow();
                CDW.SetInputData(ControlForTab);//VG1.GetOutPut());

                CDW.Title = "Normal Probability Plot - " + CurrentDesc.GetName() + " - " + ListPlatesToProcess.Count + " plates";

                CDW.Run();
                CDW.Display();
            }


            //  ComputeAndDisplayMormalProbabilityPlot(false);
        }

        private void systematicErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            ComputeSystematicErrorsTable();
            //System.Windows.Forms.DialogResult Res = MessageBox.Show("By applying this process, classes will be definitively modified ! Proceed ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //if (Res == System.Windows.Forms.DialogResult.No) return;

            //if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
            //{
            //    List<string> Result = GenerateArtifactMessage(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate(), comboBoxDescriptorToDisplay.SelectedIndex);
            //    MessageBox.Show(Result[1], "Systematic Error Identification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
            //    DataTable ResultSystematicError = ComputeSystematicErrorsTable();
            //    dataGridViewForQualityControl.DataSource = ResultSystematicError;
            //    dataGridViewForQualityControl.Update();
            //}
        }

        private void aToolStripMenuItem_Click_1(object sender, EventArgs e)
        {


            //if (CompleteScreening.ListDescriptors.GetListNameActives().Count <= 1)
            //{
            //    MessageBox.Show("MINE Analysis requires at least two activated descriptors\n", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //bool IsFullScreen = false;
            //List<double>[] ListValueDesc = ExtractDesciptorAverageValuesList(IsFullScreen);

            //DisplayMINE(ListValueDesc);

            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            if (GUI_ListClasses.Run().IsSucceed == false) return;
            cExtendedList ListClassSelected = GUI_ListClasses.GetOutPut()[0];

            if (ProcessModeplateByPlateToolStripMenuItem.Checked)
            {
                cDesignerTab DT = new cDesignerTab();

                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                {
                    cListWells ListWellsToProcess = new cListWells(null);
                    foreach (cWell item in TmpPlate.ListActiveWells)
                    {
                        if (item.GetCurrentClassIdx() != -1)
                        {
                            if (ListClassSelected[item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess.Add(item);
                        }
                    }

                    cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);

                    cMineAnalysis MA = new cMineAnalysis();
                    MA.SetInputData(NewTable);
                    MA.Is_BriefReport = true;
                    MA.CurrentScreening = cGlobalInfo.CurrentScreening;
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
                    SubDT.Title = TmpPlate.GetName();
                    SubDT.Run();
                    DT.SetInputData(SubDT.GetOutPut());
                }

                DT.Run();
                cDisplayToWindow TmpvD = new cDisplayToWindow();

                TmpvD.SetInputData(DT.GetOutPut());
                TmpvD.Title = "MINE analysis - " + cGlobalInfo.CurrentScreening.ListPlatesActive.Count + " plates";
                TmpvD.Run();
                TmpvD.Display();
            }
            else if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
            {
                cPlate TmpPlate = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate();

                cListWells ListWellsToProcess = new cListWells(null);
                foreach (cWell item in TmpPlate.ListActiveWells)
                {
                    if (item.GetCurrentClassIdx() != -1)
                    {
                        if (ListClassSelected[item.GetCurrentClassIdx()] == 1)
                            ListWellsToProcess.Add(item);
                    }
                }

                cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);

                cMineAnalysis MA = new cMineAnalysis();
                MA.SetInputData(NewTable);
                MA.Is_BriefReport = true;
                MA.CurrentScreening = cGlobalInfo.CurrentScreening;
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
                SubDT.Title = TmpPlate.GetName();
                SubDT.Run();

                cDisplayToWindow TmpvD = new cDisplayToWindow();

                TmpvD.SetInputData(SubDT.GetOutPut());
                TmpvD.Title = "MINE analysis - " + TmpPlate.GetName() + " : " + ListWellsToProcess.Count + " wells";
                TmpvD.Run();
                TmpvD.Display();
            }
            else if (ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                cListWells ListWellsToProcess = new cListWells(null);

                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                    foreach (cWell item in TmpPlate.ListActiveWells)
                        if (item.GetCurrentClassIdx() != -1)
                            if (ListClassSelected[item.GetCurrentClassIdx()] == 1) ListWellsToProcess.Add(item);

                cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);

                cMineAnalysis MA = new cMineAnalysis();
                MA.SetInputData(NewTable);
                MA.Is_BriefReport = true;
                MA.CurrentScreening = cGlobalInfo.CurrentScreening;
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

                cDisplayToWindow TmpvD = new cDisplayToWindow();

                TmpvD.SetInputData(SubDT.GetOutPut());
                TmpvD.Title = "MINE analysis : " + ListWellsToProcess.Count + " wells";
                TmpvD.Run();
                TmpvD.Display();
            }

        }

        private void correlationMatrixToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (checkedListBoxActiveDescriptors.CheckedItems.Count <= 1)
            {
                MessageBox.Show("At least two descriptors have to be selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (checkedListBoxActiveDescriptors.CheckedItems.Count <= 1)
            {
                MessageBox.Show("At least two descriptors have to be selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            GUI_ListClasses.IsCheckBoxes = true;
            GUI_ListClasses.IsSelectAll = true;

            if (GUI_ListClasses.Run().IsSucceed == false) return;
            cExtendedList ListClassSelected = GUI_ListClasses.GetOutPut()[0];

            if (ListClassSelected.Sum() < 1)
            {
                MessageBox.Show("At least one classe has to be selected.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            cDesignerTab DT = new cDesignerTab();

            if (this.ProcessModeplateByPlateToolStripMenuItem.Checked)
            {
                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                {
                    //ListWellsToProcess.AddRange(TmpPlate.ListActiveWells);
                    cListWells ListWellsToProcess = new cListWells(null);

                    foreach (cWell item in TmpPlate.ListActiveWells)
                    {
                        if ((item.GetCurrentClassIdx() != -1) && (ListClassSelected[item.GetCurrentClassIdx()] == 1)) ListWellsToProcess.Add(item);
                    }

                    cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);

                    cCorrelationMatrix CM = new cCorrelationMatrix();
                    CM.SetInputData(NewTable);
                    CM.Run();


                    cExtendedTable CorrelationMatrix = CM.GetOutPut();
                    weka.core.Instances Insts = CorrelationMatrix.CreateWekaInstances();


                    weka.clusterers.HierarchicalClusterer ClustererToReturn = new weka.clusterers.HierarchicalClusterer();
                    // string OptionDistance = " -N " + (int)Parameters.ListDoubleValues.Get("numericUpDownNumClasses").Value;

                    //string DistanceType = (string)Parameters.ListTextValues.Get("comboBoxDistance").Value;
                    //OptionDistance += " -A \"weka.core.";
                    //switch (DistanceType)
                    //{
                    //    case "Euclidean":
                    //        OptionDistance += "EuclideanDistance";
                    //        break;
                    //    case "Manhattan":
                    //        OptionDistance += "ManhattanDistance";
                    //        break;
                    //    case "Chebyshev":
                    //        OptionDistance += "ChebyshevDistance";
                    //        break;
                    //    default:
                    //        break;
                    //}

                    //if (!(bool)Parameters.ListCheckValues.Get("checkBoxNormalize").Value)
                    //    OptionDistance += " -D";
                    //OptionDistance += " -R ";


                    //OptionDistance += "first-last\"";
                    //string WekaOption = "-L " + (string)Parameters.ListTextValues.Get("comboBoxLinkType").Value + OptionDistance;
                    ((weka.clusterers.HierarchicalClusterer)ClustererToReturn).setOptions(weka.core.Utils.splitOptions("-N 1 -L SINGLE -P -A \"weka.core.EuclideanDistance -R first-last\" "));

                    ClustererToReturn.buildClusterer(Insts);
                    //this.NumberOfClusters = ClustererToReturn.numberOfClusters();
                    cInfoForHierarchical IFH = new cInfoForHierarchical();
                    IFH.ListInstances = Insts;

                    cDendoGram DENDO = new cDendoGram(((weka.clusterers.HierarchicalClusterer)ClustererToReturn), IFH, GlobalInfo);



                    //cViewerHeatMap VHM = new cViewerHeatMap();
                    cViewerTable VHM = new cViewerTable();
                    VHM.SetInputData(CM.GetOutPut());
                    //VHM.IsDisplayValues = true;
                    VHM.Title = "Correlation - " + TmpPlate.GetName() + " (" + ListWellsToProcess.Count + " wells)";
                    VHM.Run();
                    VHM.GetOutPut().Title = TmpPlate.GetName();
                    DT.SetInputData(VHM.GetOutPut());
                }
            }
            else if (this.ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                cListWells ListWellsToProcess = new cListWells(null);

                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                {
                    foreach (cWell item in TmpPlate.ListActiveWells)
                        if ((item.GetCurrentClassIdx() != -1) && (ListClassSelected[item.GetCurrentClassIdx()] == 1)) ListWellsToProcess.Add(item);
                }

                cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);

                cCorrelationMatrix CM = new cCorrelationMatrix();
                CM.SetInputData(NewTable);
                CM.Run();

                //cViewerHeatMap VHM = new cViewerHeatMap();
                cViewerTable VHM = new cViewerTable();
                VHM.SetInputData(CM.GetOutPut());
                //VHM.IsDisplayValues = true;
                VHM.Title = "Correlation - Entire screening (" + ListWellsToProcess.Count + " wells)";
                VHM.Run();

                DT.SetInputData(VHM.GetOutPut());
            }
            else
            {
                cListWells ListWellsToProcess = new cListWells(null);

                foreach (cWell item in cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListActiveWells)
                    if ((item.GetCurrentClassIdx() != -1) && (ListClassSelected[item.GetCurrentClassIdx()] == 1)) ListWellsToProcess.Add(item);

                cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);

                cCorrelationMatrix CM = new cCorrelationMatrix();
                CM.SetInputData(NewTable);
                CM.Run();

                //cViewerHeatMap VHM = new cViewerHeatMap();
                cViewerTable VHM = new cViewerTable();
                VHM.SetInputData(CM.GetOutPut());
                //VHM.IsDisplayValues = true;
                VHM.Title = "Correlation - " + cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetName() + " (" + ListWellsToProcess.Count + " wells)";
                VHM.Run();

                DT.SetInputData(VHM.GetOutPut());
            }

            DT.Run();

            //  DT.SetInputData(VT.GetOutPut());

            //cDesignerColumn DC = new cDesignerColumn();  
            //DC.SetInputData(VHM.GetOutPut());
            //DC.SetInputData(VT.GetOutPut());
            //DC.Run();

            //cDisplayDesigner DD = new cDisplayDesigner();
            // DD.SetInputData(VHM.GetOutPut());
            // DD.Run();

            cDisplayToWindow vD = new cDisplayToWindow();
            vD.SetInputData(DT.GetOutPut());
            vD.Title = "Pearson Correlation";
            vD.Run();
            vD.Display();

            //ComputeAndDisplayCorrelationMatrix(false, true, null);
        }

        private void ftestdescBasedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkedListBoxActiveDescriptors.CheckedItems.Count <= 1)
            {
                MessageBox.Show("At least two descriptors have to be selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cDesignerTab DT = new cDesignerTab();

            cTwoSampleFTest CM = new cTwoSampleFTest();
            CM.FTestTails = eFTestTails.BOTH;

            if (this.ProcessModeplateByPlateToolStripMenuItem.Checked)
            {
                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                {
                    cListWells ListWellsToProcess = new cListWells(null);
                    foreach (cWell item in TmpPlate.ListActiveWells)
                        if (item.GetCurrentClassIdx() != -1) ListWellsToProcess.Add(item);

                    cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);


                    CM.SetInputData(NewTable);
                    CM.Run();

                    cViewerHeatMap VHM = new cViewerHeatMap();
                    VHM.SetInputData(CM.GetOutPut());
                    VHM.IsDisplayValues = true;
                    VHM.Title = "F-Test - " + TmpPlate.GetName() + " (" + ListWellsToProcess.Count + " wells)";
                    VHM.Run();

                    DT.SetInputData(VHM.GetOutPut());
                }
            }
            else if (this.ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                cListWells ListWellsToProcess = new cListWells(null);

                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                {
                    foreach (cWell item in TmpPlate.ListActiveWells)
                        if (item.GetCurrentClassIdx() != -1) ListWellsToProcess.Add(item);
                }

                cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);

                //  cTwoSampleFTest CM = new cTwoSampleFTest();
                CM.SetInputData(NewTable);
                CM.Run();

                cViewerHeatMap VHM = new cViewerHeatMap();
                VHM.SetInputData(CM.GetOutPut());
                VHM.IsDisplayValues = true;
                VHM.Title = "F-Test - Entire screening (" + ListWellsToProcess.Count + " wells)";
                VHM.Run();

                DT.SetInputData(VHM.GetOutPut());
            }
            else
            {
                cListWells ListWellsToProcess = new cListWells(null);

                foreach (cWell item in cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListActiveWells)
                    if (item.GetCurrentClassIdx() != -1) ListWellsToProcess.Add(item);

                cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);

                // cTwoSampleFTest CM = new cTwoSampleFTest();
                CM.SetInputData(NewTable);
                CM.Run();

                cViewerHeatMap VHM = new cViewerHeatMap();
                VHM.SetInputData(CM.GetOutPut());
                VHM.IsDisplayValues = true;
                VHM.Title = "F-Test - " + cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetName() + " (" + ListWellsToProcess.Count + " wells)";
                VHM.Run();

                DT.SetInputData(VHM.GetOutPut());
            }

            DT.Run();
            cDisplayToWindow vD = new cDisplayToWindow();
            vD.SetInputData(DT.GetOutPut());
            vD.Title = "F-Test";
            vD.Run();
            vD.Display();
        }

        private void stackedHistogramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            GUI_ListClasses.IsCheckBoxes = true;
            GUI_ListClasses.IsSelectAll = true;

            if (GUI_ListClasses.Run().IsSucceed == false) return;
            cExtendedList ListClassSelected = GUI_ListClasses.GetOutPut()[0];

            if (ListClassSelected.Sum() < 1)
            {
                MessageBox.Show("At least one classe has to be selected.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cDisplayToWindow CDW1 = new cDisplayToWindow();

            if ((ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked) || (ProcessModeEntireScreeningToolStripMenuItem.Checked))
            {
                cListWells ListWellsToProcess = new cListWells(null);
                List<cPlate> PlateList = new List<cPlate>();

                if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                    PlateList.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());
                else
                {
                    foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive) PlateList.Add(TmpPlate);
                }

                foreach (cPlate TmpPlate in PlateList)
                    foreach (cWell item in TmpPlate.ListActiveWells)
                        if ((item.GetCurrentClassIdx() != -1) && (ListClassSelected[item.GetCurrentClassIdx()] == 1)) ListWellsToProcess.Add(item);


                if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                    CDW1.Title = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName() + " - Stacked Histogram (" + PlateList[0].GetName() + ")";
                else
                    CDW1.Title = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName() + " - Stacked Histogram - " + PlateList.Count + " plates";

                cExtendedTable NewTable = ListWellsToProcess.GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
                NewTable.Name = CDW1.Title;

                cViewerStackedHistogram CV1 = new cViewerStackedHistogram();
                CV1.SetInputData(NewTable);
                CV1.Chart.LabelAxisX = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
                CV1.Run();

                CDW1.SetInputData(CV1.GetOutPut());
            }
            else if (ProcessModeplateByPlateToolStripMenuItem.Checked)
            {
                cDesignerTab CDT = new cDesignerTab();
                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                {
                    cListWells ListWellsToProcess = new cListWells(null);
                    foreach (cWell item in TmpPlate.ListActiveWells)
                        if ((item.GetCurrentClassIdx() != -1) && (ListClassSelected[item.GetCurrentClassIdx()] == 1)) ListWellsToProcess.Add(item);

                    cExtendedTable NewTable = ListWellsToProcess.GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
                    NewTable.Name = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName() + " - " + TmpPlate.GetName();


                    cViewerStackedHistogram CV1 = new cViewerStackedHistogram();
                    CV1.SetInputData(NewTable);
                    CV1.Chart.LabelAxisX = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
                    CV1.Title = TmpPlate.GetName();
                    CV1.Run();

                    CDT.SetInputData(CV1.GetOutPut());
                }
                CDT.Run();
                CDW1.SetInputData(CDT.GetOutPut());
                CDW1.Title = "Stacked Histogram - " + cGlobalInfo.CurrentScreening.ListPlatesActive.Count + " plates";
            }

            CDW1.Run();
            CDW1.Display();
        }

        private void testLinearRegressionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cListWells ListWellsToProcess = new cListWells(null);

            foreach (cWell item in cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListActiveWells)
                if (item.GetCurrentClassIdx() != -1) ListWellsToProcess.Add(item);

            cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);

            cLinearRegression LR = new cLinearRegression();
            LR.SetInputData(NewTable);
            LR.Run();



            cViewerTable VT = new cViewerTable();
            VT.SetInputData(LR.GetOutPut());
            VT.Run();


            cDisplayToWindow vD = new cDisplayToWindow();
            vD.SetInputData(VT.GetOutPut());
            vD.Title = "Linear regression (Test)";
            vD.Run();
            vD.Display();

        }

        private void covarianceMatrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkedListBoxActiveDescriptors.CheckedItems.Count <= 1)
            {
                MessageBox.Show("At least two descriptors have to be selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            GUI_ListClasses.IsCheckBoxes = true;
            GUI_ListClasses.IsSelectAll = true;

            if (GUI_ListClasses.Run().IsSucceed == false) return;
            cExtendedList ListClassSelected = GUI_ListClasses.GetOutPut()[0];

            if (ListClassSelected.Sum() < 1)
            {
                MessageBox.Show("At least one classe has to be selected.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            cDisplayToWindow vD = new cDisplayToWindow();
            cDesignerTab DT = new cDesignerTab();

            cCovarianceMatrix CM = new cCovarianceMatrix();
            // CM.FTestTails = eFTestTails.BOTH;

            if (this.ProcessModeplateByPlateToolStripMenuItem.Checked)
            {
                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                {
                    cListWells ListWellsToProcess = new cListWells(null);
                    foreach (cWell item in TmpPlate.ListActiveWells)
                        if ((item.GetCurrentClassIdx() != -1) && (ListClassSelected[item.GetCurrentClassIdx()] == 1)) ListWellsToProcess.Add(item);

                    cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);


                    CM.SetInputData(NewTable);
                    CM.Run();

                    //cViewerHeatMap VHM = new cViewerHeatMap();
                    cViewerTable VHM = new cViewerTable();
                    VHM.SetInputData(CM.GetOutPut());
                    //VHM.IsDisplayValues = true;
                    vD.Title = "Covariance - " + TmpPlate.GetName() + " (" + ListWellsToProcess.Count + " wells)";
                    VHM.Run();

                    DT.SetInputData(VHM.GetOutPut());
                }
            }
            else if (this.ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                cListWells ListWellsToProcess = new cListWells(null);

                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                {
                    foreach (cWell item in TmpPlate.ListActiveWells)
                        if ((item.GetCurrentClassIdx() != -1) && (ListClassSelected[item.GetCurrentClassIdx()] == 1)) ListWellsToProcess.Add(item);
                }

                cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);

                //  cTwoSampleFTest CM = new cTwoSampleFTest();
                CM.SetInputData(NewTable);
                CM.Run();

                // cViewerHeatMap VHM = new cViewerHeatMap();
                cViewerTable VHM = new cViewerTable();
                VHM.SetInputData(CM.GetOutPut());
                //VHM.IsDisplayValues = true;
                vD.Title = "Covariance - Entire screening (" + ListWellsToProcess.Count + " wells)";
                VHM.Run();

                DT.SetInputData(VHM.GetOutPut());
            }
            else
            {
                cListWells ListWellsToProcess = new cListWells(null);

                foreach (cWell item in cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListActiveWells)
                    if ((item.GetCurrentClassIdx() != -1) && (ListClassSelected[item.GetCurrentClassIdx()] == 1)) ListWellsToProcess.Add(item);

                cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);

                // cTwoSampleFTest CM = new cTwoSampleFTest();
                CM.SetInputData(NewTable);
                CM.Run();

                //cViewerHeatMap VHM = new cViewerHeatMap();
                cViewerTable VHM = new cViewerTable();
                VHM.SetInputData(CM.GetOutPut());
                //VHM.IsDisplayValues = true;
                vD.Title = "Covariance - " + cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetName() + " (" + ListWellsToProcess.Count + " wells)";
                VHM.Run();

                DT.SetInputData(VHM.GetOutPut());
            }

            DT.Run();

            vD.SetInputData(DT.GetOutPut());
            // vD.Title = "F-Test";
            vD.Run();
            vD.Display();
        }



        private void pCAToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            GUI_ListClasses.IsCheckBoxes = true;
            if (GUI_ListClasses.Run().IsSucceed == false) return;
            cExtendedList ListClassSelected = GUI_ListClasses.GetOutPut()[0];

            cDisplayToWindow vD = new cDisplayToWindow();

            if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
            {
                cPlate TmpPlate = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate();
                cListWells ListWellsToProcess = new cListWells(null);
                //    cExtendedList ListClasses = new cExtendedList();
                //    ListClasses.Name = "Classes";
                foreach (cWell item in TmpPlate.ListActiveWells)
                    if ((item.GetCurrentClassIdx() != -1) && (ListClassSelected[item.GetCurrentClassIdx()] == 1)) ListWellsToProcess.Add(item);

                cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);
                // NewTable.Add(ListClasses);

                cProjectorPCA PCA = new cProjectorPCA();
                PCA.SetInputData(NewTable);
                cFeedBackMessage FM = PCA.Run();
                if (!FM.IsSucceed)
                {
                    MessageBox.Show(FM.Message, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cViewerTable VHM = new cViewerTable();
                cExtendedTable CT = PCA.GetOutPut();

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
                vD.Title = "PCA - " + TmpPlate.GetName() + " : " + ListWellsToProcess.Count + " wells";
            }
            else if (ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                cListWells ListWellsToProcess = new cListWells(null);
                // cExtendedList ListClasses = new cExtendedList();
                //  ListClasses.Name = "Classes";

                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                    foreach (cWell item in TmpPlate.ListActiveWells)
                        if ((item.GetCurrentClassIdx() != -1) && (ListClassSelected[item.GetCurrentClassIdx()] == 1)) ListWellsToProcess.Add(item);

                cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);
                //NewTable.Add(ListClasses);
                cProjectorPCA PCA = new cProjectorPCA();
                PCA.SetInputData(NewTable);
                cFeedBackMessage FM = PCA.Run();
                if (!FM.IsSucceed)
                {
                    MessageBox.Show(FM.Message, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cViewerTable VHM = new cViewerTable();
                cExtendedTable CT = PCA.GetOutPut();
                foreach (var item in CT)
                {
                    cDescriptorsLinearCombination DLC = new cDescriptorsLinearCombination(item);
                    foreach (cDescriptorType Desc in item.ListTags)
                        DLC.Add(Desc);
                    item.Tag = DLC;
                }

                VHM.SetInputData(CT);
                //VHM.SetInputData(PCA.GetOutPut());
                VHM.Run();

                vD.SetInputData(VHM.GetOutPut());
                vD.Title = "PCA - " + ListWellsToProcess.Count + " wells.";
            }
            else if (ProcessModeplateByPlateToolStripMenuItem.Checked)
            {
                cDesignerTab CDT = new cDesignerTab();

                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                {
                    cListWells ListWellsToProcess = new cListWells(null);
                    //cExtendedList ListClasses = new cExtendedList();
                    //ListClasses.Name = "Classes";

                    foreach (cWell item in TmpPlate.ListActiveWells)
                        if ((item.GetCurrentClassIdx() != -1) && (ListClassSelected[item.GetCurrentClassIdx()] == 1)) ListWellsToProcess.Add(item);

                    cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);
                    //NewTable.Add(ListClasses);
                    NewTable.Name = TmpPlate.GetName();

                    cProjectorPCA PCA = new cProjectorPCA();
                    PCA.SetInputData(NewTable);
                    cFeedBackMessage FM = PCA.Run();
                    if (!FM.IsSucceed)
                    {
                        MessageBox.Show(FM.Message, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    cViewerTable VHM = new cViewerTable();
                    cExtendedTable CT = PCA.GetOutPut();

                    foreach (var item in CT)
                    {
                        cDescriptorsLinearCombination DLC = new cDescriptorsLinearCombination(item);
                        foreach (cDescriptorType Desc in item.ListTags)
                            DLC.Add(Desc);
                        item.Tag = DLC;
                    }

                    VHM.SetInputData(CT);

                    //VHM.SetInputData(PCA.GetOutPut());
                    VHM.Run();
                    CDT.SetInputData(VHM.GetOutPut());
                }
                CDT.Run();
                vD.SetInputData(CDT.GetOutPut());
            }
            else
                return;

            vD.Run();
            vD.Display();
        }

        private void lDAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            if (GUI_ListClasses.Run().IsSucceed == false) return;
            cExtendedList ListClassSelected = GUI_ListClasses.GetOutPut()[0];

            if (ListClassSelected.Sum() < 2)
            {
                MessageBox.Show("At least two classes have to be selected to perfom a LDA.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cDisplayToWindow vD = new cDisplayToWindow();

            if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
            {
                cPlate TmpPlate = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate();
                cListWells ListWellsToProcess = new cListWells(null);
                cExtendedList ListClasses = new cExtendedList();
                ListClasses.Name = "Classes";
                foreach (cWell item in TmpPlate.ListActiveWells)
                    if ((item.GetCurrentClassIdx() != -1) && (ListClassSelected[item.GetCurrentClassIdx()] == 1))
                    {
                        ListWellsToProcess.Add(item);
                        ListClasses.Add(item.GetCurrentClassIdx());
                    }

                cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);
                NewTable.Add(ListClasses);

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
                vD.Title = "LDA - " + TmpPlate.GetName() + " : " + ListWellsToProcess.Count + " wells";
            }
            else if (ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                cListWells ListWellsToProcess = new cListWells(null);
                cExtendedList ListClasses = new cExtendedList();
                ListClasses.Name = "Classes";

                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                {
                    foreach (cWell item in TmpPlate.ListActiveWells)
                    {
                        if (item.GetCurrentClassIdx() != -1)
                        {
                            if (ListClassSelected[item.GetCurrentClassIdx()] == 1)
                            {
                                ListWellsToProcess.Add(item);
                                ListClasses.Add(item.GetCurrentClassIdx());
                            }
                        }
                    }
                }

                cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);
                NewTable.Add(ListClasses);
                cProjectorLDA LDA = new cProjectorLDA();
                LDA.SetInputData(NewTable);
                cFeedBackMessage FM = LDA.Run();
                if (!FM.IsSucceed)
                {
                    MessageBox.Show(FM.Message, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cExtendedTable CT = LDA.GetOutPut();

                foreach (var item in CT)
                {
                    cDescriptorsLinearCombination DLC = new cDescriptorsLinearCombination(item);

                    foreach (cDescriptorType Desc in item.ListTags)
                        DLC.Add(Desc);

                    item.Tag = DLC;
                }

                cViewerTable VHM = new cViewerTable();
                VHM.SetInputData(CT);

                VHM.Run();

                vD.SetInputData(VHM.GetOutPut());
                vD.Title = "LDA - " + ListWellsToProcess.Count + " wells.";
            }
            else if (ProcessModeplateByPlateToolStripMenuItem.Checked)
            {
                cDesignerTab CDT = new cDesignerTab();

                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                {
                    cListWells ListWellsToProcess = new cListWells(null);
                    cExtendedList ListClasses = new cExtendedList();
                    ListClasses.Name = "Classes";


                    foreach (cWell item in TmpPlate.ListActiveWells)
                    {
                        if (item.GetCurrentClassIdx() != -1)
                        {
                            if (ListClassSelected[item.GetCurrentClassIdx()] == 1)
                            {
                                ListWellsToProcess.Add(item);
                                ListClasses.Add(item.GetCurrentClassIdx());
                            }
                        }
                    }

                    cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);
                    NewTable.Add(ListClasses);
                    NewTable.Name = TmpPlate.GetName();

                    cProjectorLDA LDA = new cProjectorLDA();
                    LDA.SetInputData(NewTable);
                    cFeedBackMessage FM = LDA.Run();
                    if (!FM.IsSucceed)
                    {
                        MessageBox.Show(FM.Message, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    cExtendedTable CT = LDA.GetOutPut();

                    foreach (var item in CT)
                    {
                        cDescriptorsLinearCombination DLC = new cDescriptorsLinearCombination(item);

                        foreach (cDescriptorType Desc in item.ListTags)
                            DLC.Add(Desc);

                        item.Tag = DLC;
                    }

                    cViewerTable VHM = new cViewerTable();
                    VHM.SetInputData(CT);
                    VHM.Run();
                    CDT.SetInputData(VHM.GetOutPut());
                }
                CDT.Run();
                vD.SetInputData(CDT.GetOutPut());
            }
            else
                return;

            vD.Run();
            vD.Display();
        }

        private void testMultiScatterToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //V1D.Chart.LabelAxisX = "Well Index";
            //V1D.Chart.LabelAxisY = CompleteScreening.ListDescriptors[CompleteScreening.ListDescriptors.CurrentSelectedDescriptor].GetName();
            //V1D.Chart.BackgroundColor = Color.LightYellow;
            //V1D.Chart.IsXAxis = true;


            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            GUI_ListClasses.IsCheckBoxes = true;
            GUI_ListClasses.IsSelectAll = true;

            if (GUI_ListClasses.Run().IsSucceed == false) return;
            cExtendedList ListClassSelected = GUI_ListClasses.GetOutPut()[0];

            if (ListClassSelected.Sum() < 1)
            {
                MessageBox.Show("At least one classe has to be selected.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            cDesignerSplitter CDC = new cDesignerSplitter();
            cListWells ListWellsToProcess = new cListWells(null);

            //foreach (cPlate TmpPlate in CompleteScreening.ListPlatesActive)
            foreach (cWell item in cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListActiveWells)
                if (item.GetCurrentClassIdx() != -1)
                    if (ListClassSelected[item.GetCurrentClassIdx()] == 1) ListWellsToProcess.Add(item);

            cExtendedTable DataFromPlate = new cExtendedTable(ListWellsToProcess, true);
            DataFromPlate.Name = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetName();

            //if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
            {


                for (int IdxDesc0 = 0; IdxDesc0 < DataFromPlate.Count; IdxDesc0++)
                    for (int IdxDesc1 = 1; IdxDesc1 < DataFromPlate.Count; IdxDesc1++)
                    {


                        cViewer2DScatterPoint V1D = new cViewer2DScatterPoint();
                        V1D.Chart.CurrentTitle.Tag = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate();
                        V1D.Chart.IdxDesc0 = IdxDesc0;
                        V1D.Chart.IdxDesc1 = IdxDesc1;
                        V1D.SetInputData(DataFromPlate);
                        V1D.Run();
                        V1D.Chart.Width = 0;
                        V1D.Chart.Height = 0;

                        cDesignerSinglePanel Designer0 = new cDesignerSinglePanel();
                        Designer0.SetInputData(V1D.GetOutPut());
                        Designer0.Run();

                        CDC.SetInputData(Designer0.GetOutPut());
                    }
            }

            CDC.Run();

            cDisplayToWindow Disp0 = new cDisplayToWindow();
            Disp0.SetInputData(CDC.GetOutPut());
            Disp0.Title = "2D Scatter points graph - wells.";
            if (!Disp0.Run().IsSucceed) return;
            Disp0.Display();

        }

        #region Statistics options
        private void statisticsToolStripMenuItem1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button != System.Windows.Forms.MouseButtons.Right) || (cGlobalInfo.CurrentScreening == null)) return;

            contextMenuStripStatOptions.Show(Control.MousePosition);
        }

        void StatMeanItem(object sender, EventArgs e)
        {
            statisticsToolStripMenuItem1.Text = "Statistics (Mean)";
            _StatMeanItem.Checked = true;
            _StatCVItem.Checked = false;
            _StatSumItem.Checked = false;
            _StatJarqueBeraItem.Checked = false;

        }

        void StatCVItem(object sender, EventArgs e)
        {
            statisticsToolStripMenuItem1.Text = "Statistics (Coefficient of Variation)";
            _StatCVItem.Checked = true;
            _StatMeanItem.Checked = false;
            _StatSumItem.Checked = false;
            _StatJarqueBeraItem.Checked = false;
        }

        void StatSumItem(object sender, EventArgs e)
        {
            statisticsToolStripMenuItem1.Text = "Statistics (Sum)";
            _StatSumItem.Checked = true;
            _StatCVItem.Checked = false;
            _StatMeanItem.Checked = false;
            _StatJarqueBeraItem.Checked = false;
        }

        void StatJarqueBeraItem(object sender, EventArgs e)
        {
            statisticsToolStripMenuItem1.Text = "Normality Test (Jarque-Bera)";
            _StatJarqueBeraItem.Checked = true;
            _StatSumItem.Checked = false;
            _StatCVItem.Checked = false;
            _StatMeanItem.Checked = false;
        }

        void DescEvolCellByCellItem(object sender, EventArgs e)
        {
            descriptorEvolutionToolStripMenuItem.Text = "Descriptor Evolutions (Cell-by-Cell)";
            _DescEvolCellByCellItem.Checked = true;
            _DescEvolGlobalItem.Checked = false;
        }

        void DescEvolGlobalItem(object sender, EventArgs e)
        {
            descriptorEvolutionToolStripMenuItem.Text = "Descriptor Evolutions (Global)";
            _DescEvolCellByCellItem.Checked = false;
            _DescEvolGlobalItem.Checked = true;
        }

        void QCZRobustItem(object sender, EventArgs e)
        {
            zScoreToolStripMenuItem.Text = "Z' (Robust)";
            _QCZRobustItem.Checked = true;
            _QCZRegularItem.Checked = false;
        }

        void QCZRegularItem(object sender, EventArgs e)
        {
            zScoreToolStripMenuItem.Text = "Z' (Regular)";
            _QCZRegularItem.Checked = true;
            _QCZRobustItem.Checked = false;
        }


        private void statisticsToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            string NameFunction = "";

            if (_StatCVItem.Checked)
                NameFunction = "Coeff. of Variation";
            else if (_StatMeanItem.Checked)
                NameFunction = "Mean";
            else if (_StatSumItem.Checked)
                NameFunction = "Sum";
            else if (_StatJarqueBeraItem.Checked)
                NameFunction = "Jarque-Bera";

            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();

            GUI_ListClasses.IsCheckBoxes = true;
            GUI_ListClasses.IsSelectAll = true;

            if (GUI_ListClasses.Run().IsSucceed == false) return;
            cExtendedTable ListClassSelected = new cExtendedTable(GUI_ListClasses.GetOutPut());// GetOutPut();


            int IdxClass = -1;
            for (int IdxC = 0; IdxC < ListClassSelected[0].Count; IdxC++)
            {
                if (ListClassSelected[0][IdxC] == 1) IdxClass = IdxC;
            }

            #region single plate and plate by plate

            cDesignerTab DT = new cDesignerTab();
            if ((ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked) || (ProcessModeplateByPlateToolStripMenuItem.Checked)/*||(ProcessModeEntireScreeningToolStripMenuItem.Checked)*/)
            {
                List<cPlate> ListPlatesToProcess = new List<cPlate>();
                if ((ProcessModeplateByPlateToolStripMenuItem.Checked)/*||(ProcessModeEntireScreeningToolStripMenuItem.Checked)*/)
                {
                    foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                        ListPlatesToProcess.Add(TmpPlate);
                }
                else
                    ListPlatesToProcess.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());

                foreach (cPlate TmpPlate in ListPlatesToProcess)
                {
                    cListWells ListWellsToProcess1 = new cListWells(null);

                    foreach (cWell item in TmpPlate.ListActiveWells)
                    {
                        if (item.GetCurrentClassIdx() != -1)
                        {
                            if (ListClassSelected[0][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess1.Add(item);
                        }
                    }

                    cExtendedTable NewTable1 = new cExtendedTable(ListWellsToProcess1, true);

                    if ((NewTable1.Count == 0) || (NewTable1[0].Count < 3))
                    {
                        if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                        {
                            MessageBox.Show("Insufficient number of control wells", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                            continue;
                    }

                    cExtendedList ListValues = new cExtendedList();
                    List<cDescriptorType> ListDescs = new List<cDescriptorType>();
                    List<string> ListNames = new List<string>();
                    int RealIdx = 0;
                    for (int IDxDesc = 0; IDxDesc < cGlobalInfo.CurrentScreening.ListDescriptors.Count; IDxDesc++)
                    {
                        if (!cGlobalInfo.CurrentScreening.ListDescriptors[IDxDesc].IsActive()) continue;

                        cExtendedTable TableForValues = new cExtendedTable();

                        TableForValues.Add(NewTable1[RealIdx]);

                        RealIdx++;

                        if (_StatJarqueBeraItem.Checked)
                        {
                            cNormalityJarqueBera JB = new cNormalityJarqueBera();
                            JB.SetInputData(TableForValues);
                            JB.Run();
                            ListValues.Add(JB.GetOutPut()[0][0]);
                        }
                        else
                        {
                            cStatistics CS = new cStatistics();
                            CS.UnselectAll();

                            if (_StatCVItem.Checked)
                                CS.IsCV = true;
                            else if (_StatMeanItem.Checked)
                                CS.IsMean = true;
                            else if (_StatSumItem.Checked)
                                CS.IsSum = true;

                            CS.SetInputData(TableForValues);
                            CS.Run();
                            ListValues.Add(CS.GetOutPut()[0][0]);
                        }

                        ListDescs.Add(cGlobalInfo.CurrentScreening.ListDescriptors[IDxDesc]);

                    }

                    cExtendedTable ET = new cExtendedTable(new cExtendedTable(ListValues));
                    ET[0].ListTags = new List<object>();
                    ET[0].ListTags.AddRange(ListDescs);
                    ET.Name = TmpPlate.GetName() + "\n" + NameFunction + " - " + cGlobalInfo.ListWellClasses[IdxClass].Name + " (" + NewTable1[0].Count + " wells)";
                    ET[0].Name = ET.Name;

                    cSort S = new cSort();
                    S.SetInputData(ET);
                    S.ColumnIndexForSorting = 0;
                    S.Run();

                    //ZFactorList.Sort(delegate(cSimpleSignature p1, cSimpleSignature p2) { return p1.AverageValue.CompareTo(p2.AverageValue); });
                    cViewerGraph1D VG1 = new cViewerGraph1D();
                    VG1.SetInputData(S.GetOutPut());

                    VG1.Chart.LabelAxisY = NameFunction;
                    VG1.Chart.LabelAxisX = "Descriptor";
                    VG1.Chart.IsZoomableX = true;
                    VG1.Chart.IsBar = true;
                    VG1.Chart.IsBorder = true;
                    VG1.Chart.IsDisplayValues = true;
                    VG1.Chart.IsShadow = true;
                    VG1.Chart.MarkerSize = 4;
                    VG1.Title = TmpPlate.GetName();
                    VG1.Run();

                    DT.SetInputData(VG1.GetOutPut());
                }
                DT.Run();

                cDisplayToWindow CDW = new cDisplayToWindow();
                CDW.SetInputData(DT.GetOutPut());//VG1.GetOutPut());


                if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                    CDW.Title = NameFunction + " - " + ListPlatesToProcess[0].GetName();
                else
                    CDW.Title = NameFunction + " - " + ListPlatesToProcess.Count + " plates";

                CDW.Run();
                CDW.Display();
            }
            #endregion
            #region entire screening
            else if (ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                List<cPlate> ListPlatesToProcess = new List<cPlate>();
                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                    ListPlatesToProcess.Add(TmpPlate);

                cExtendedList ListZ = new cExtendedList();
                List<cPlate> ListPlatesForZFactor = new List<cPlate>();
                foreach (cPlate TmpPlate in ListPlatesToProcess)
                {
                    cListWells ListWellsToProcess1 = new cListWells(null);

                    foreach (cWell item in TmpPlate.ListActiveWells)
                    {
                        if (item.GetCurrentClassIdx() != -1)
                        {
                            if (ListClassSelected[0][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess1.Add(item);
                        }
                    }


                    cExtendedTable NewTable1 = new cExtendedTable(ListWellsToProcess1, cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor()));


                    if ((NewTable1.Count == 0) || (NewTable1[0].Count < 3))
                    {
                        if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                        {
                            MessageBox.Show("Insufficient number of control wells", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                            continue;
                    }


                    if (_StatJarqueBeraItem.Checked)
                    {
                        cNormalityJarqueBera JB = new cNormalityJarqueBera();
                        JB.SetInputData(NewTable1);
                        JB.Run();
                        ListZ.Add(JB.GetOutPut()[0][0]);
                    }
                    else
                    {
                        cStatistics CS = new cStatistics();
                        CS.UnselectAll();

                        if (_StatCVItem.Checked)
                            CS.IsCV = true;
                        else if (_StatMeanItem.Checked)
                            CS.IsMean = true;
                        else if (_StatSumItem.Checked)
                            CS.IsSum = true;

                        CS.SetInputData(NewTable1);
                        CS.Run();
                        ListZ.Add(CS.GetOutPut()[0][0]);
                    }
                    ListPlatesForZFactor.Add(TmpPlate);
                }
            #endregion

                cExtendedTable ET = new cExtendedTable(new cExtendedTable(ListZ));
                ET[0].ListTags = new List<object>();
                ET[0].ListTags.AddRange(ListPlatesForZFactor);
                ET.Name = NameFunction + " - " + cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();// +" - " +cGlobalInfo.ListWellClasses[IdxClassNeg].Name + " (" + NewTable1[0].Count + " wells) vs. " +cGlobalInfo.ListWellClasses[IdxClassPos].Name + " (" + NewTable2[0].Count + " wells)";
                ET[0].Name = ET.Name;

                cViewerGraph1D VG1 = new cViewerGraph1D();
                VG1.SetInputData(ET);

                VG1.Chart.LabelAxisY = NameFunction;
                VG1.Chart.LabelAxisX = "Plate";
                VG1.Chart.IsZoomableX = true;
                VG1.Chart.IsBar = true;
                VG1.Chart.IsBorder = true;
                VG1.Chart.IsDisplayValues = true;
                VG1.Chart.IsShadow = true;
                VG1.Chart.MarkerSize = 4;
                VG1.Title = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
                VG1.Run();

                cDisplayToWindow CDW = new cDisplayToWindow();
                CDW.SetInputData(VG1.GetOutPut());
                CDW.Title = NameFunction + " - " + ListPlatesToProcess.Count + " plates";
                CDW.Run();
                CDW.Display();
            }
        }
        #endregion

        private void loadExtendedTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cCSVToTable CSVT = new cCSVToTable();
            CSVT.IsDisplayUIForFilePath = true;
            CSVT.IsContainRowNames = true;
            CSVT.IsContainColumnHeaders = true;
            CSVT.Run();

            cDisplayExtendedTable DT = new Classes.MetaComponents.cDisplayExtendedTable();
            DT.SetInputData(CSVT.GetOutPut());
            DT.Run();
        }

        private void toolStripMenuItemLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog CurrOpenFileDialog = new OpenFileDialog();
            CurrOpenFileDialog.Filter = "TIF Files (*.tif)|*.tif|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|BMP Files (*.bmp)|*.bmp|Cellomics Files (*.c01)|*.c01|Zeiss LSM Files (*.lsm)|*.lsm|MetaMorph Stack  STK Files (*.stk)|*.stk";
            //CurrOpenFileDialog.Filter = "Tif files (*.tif)|*.tif";
            DialogResult Res = CurrOpenFileDialog.ShowDialog();
            if (Res != DialogResult.OK) return;
            cImage NewIm = new cImage(CurrOpenFileDialog/*CurrOpenFileDialog.FileName*/);

            cDisplaySingleImage NewView = new cDisplaySingleImage();
            NewView.SetInputData(NewIm);
            NewView.Run();
            //if((NewIm.Width>500)||(NewIm.Height>500))
            //    NewView.ConstraintImageSize = new Point(500, 500);

            //GlobalInfo.DisplayViewer(NewView);
            return;
        }


        void SaveProperty()
        {
            cGUI_ListWellProperty GUI_ListWellProperty = new cGUI_ListWellProperty();
            GUI_ListWellProperty.IsCheckBoxes = false;

            if (GUI_ListWellProperty.Run().IsSucceed == false) return;
            List<cPropertyType> ListSelectedProp = GUI_ListWellProperty.GetOutPut();

            cExtendedTable ToBeSaved = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetPropertyAsTable(ListSelectedProp[0]);

            cTableToFile CTF = new cTableToFile();
            CTF.IsDisplayUIForFilePath = true;
            CTF.IsTagToBeSaved = true;
            CTF.SetInputData(ToBeSaved);
            CTF.IsRunEXCEL = true;
            CTF.Run();
        }



        void LoadProperty()
        {
            cGUI_ListWellProperty GUI_ListWellProperty = new cGUI_ListWellProperty();
            GUI_ListWellProperty.IsCheckBoxes = false;

            if (GUI_ListWellProperty.Run().IsSucceed == false) return;
            List<cPropertyType> ListSelectedProp = GUI_ListWellProperty.GetOutPut();

            cCSVToTable CSVT = new cCSVToTable();
            CSVT.IsDisplayUIForFilePath = true;
            CSVT.IsContainRowNames = true;
            CSVT.IsContainColumnHeaders = true;
            CSVT.AddAsObject = true;
            CSVT.Run();

            cExtendedTable PlateDesign = CSVT.GetOutPut();
            if (PlateDesign == null) return;


            cListPlates LP = new cListPlates();

            if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
            {
                LP.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());
            }
            else if (ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                LP = cGlobalInfo.CurrentScreening.ListPlatesActive;
            }
            else
            {
                cGUI_ListPlates GUIListPlate = new cGUI_ListPlates();
                if (GUIListPlate.Run().IsSucceed == false) return;
                LP = GUIListPlate.GetOutPut();
            }


            for (int Col = 0; Col < cGlobalInfo.CurrentScreening.Columns; Col++)
                for (int Row = 0; Row < cGlobalInfo.CurrentScreening.Rows; Row++)
                {
                    if ((Col < PlateDesign.Count) && (Row < PlateDesign[Col].ListTags.Count))
                    {
                        object TmpObj = PlateDesign[Col].ListTags[Row];

                        //string TmpVal = "";

                        //if (TmpObj != null)
                        //    TmpVal = (string)TmpObj;

                        foreach (cPlate CurrentPlate in LP)
                        {
                            cWell TmpWell = CurrentPlate.GetWell(Col, Row, false);
                            if ((TmpWell != null))
                            {
                                if (TmpObj == null)
                                {
                                    TmpWell.ListProperties.UpdateValueByName(ListSelectedProp[0].Name, null);
                                }
                                else
                                {
                                    if (/*(TmpObj.GetType() == typeof(string)) &&*/ (ListSelectedProp[0].Type == eDataType.STRING))
                                        TmpWell.ListProperties.UpdateValueByName(ListSelectedProp[0].Name, (string)TmpObj);
                                    else if (/*(TmpObj.GetType() == typeof(double)) &&*/ (ListSelectedProp[0].Type == eDataType.DOUBLE))
                                    {
                                        double Res;
                                        if (double.TryParse((string)TmpObj, out Res))
                                            TmpWell.ListProperties.UpdateValueByName(ListSelectedProp[0].Name, Res);
                                        else
                                            TmpWell.ListProperties.UpdateValueByName(ListSelectedProp[0].Name, null);
                                    }
                                    else if (/*(TmpObj.GetType() == typeof(int)) &&*/ (ListSelectedProp[0].Type == eDataType.INTEGER))
                                    {
                                        int IntRes;
                                        if (int.TryParse((string)TmpObj, out IntRes))
                                            TmpWell.ListProperties.UpdateValueByName(ListSelectedProp[0].Name, IntRes);
                                        else
                                            TmpWell.ListProperties.UpdateValueByName(ListSelectedProp[0].Name, null);

                                    }
                                }

                                // TmpWell.ListProperties.UpdateValueByName(ListSelectedProp[0].Name, (int)Convert.ToDouble(TmpObj.ToString()));

                                //else if (TmpObj.GetType() == typeof(int) && (ListSelectedProp[0].Type == eDataType.INTEGER))
                                //  TmpWell.ListProperties.UpdateValueByName(ListSelectedProp[0].Name, (int)TmpObj);
                            }

                        }
                    }
                }

            richTextBoxConsole.AppendText(LP.Count + " plates updated !\n");

            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);

        }


        private void toolStripMenuItemLoadCellLines_Click(object sender, EventArgs e)
        {
            LoadProperty();
        }


        private void testPieChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cListWells LW = cGlobalInfo.CurrentScreening.GetListWells();
            cExtendedTable T = LW.GetListWellClasses();

            cViewerPie VP = new cViewerPie();
            VP.SetInputData(T);
            VP.Run();


            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(VP.GetOutPut());
            DTW.Run();
            DTW.Display();

            //cDisplayExtendedTable DET = new cDisplayExtendedTable();
            //DET.SetInputData(T);
            //DET.Run();
        }

        private void descriptorEvolutionToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button != System.Windows.Forms.MouseButtons.Right) || (cGlobalInfo.CurrentScreening == null)) return;

            ContextMenuStripDescEvolOptions.Show(Control.MousePosition);
        }

        public enum eTestType { F_TEST, ANOVA, TWO_SAMPLES_T_TEST, STUDENT_T_TEST };

        private void PerformTestType(eTestType TestType)
        {

            #region extract classes of interest
            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            GUI_ListClasses.IsCheckBoxes = !_DescEvolCellByCellItem.Checked;
            GUI_ListClasses.IsSelectAll = true;

            if (GUI_ListClasses.Run().IsSucceed == false) return;
            cExtendedTable ListClassSelected = GUI_ListClasses.GetOutPut();

            if (ListClassSelected.Sum() < 1)
            {
                MessageBox.Show("At least one classe has to be selected.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            cDesignerTab DT = new cDesignerTab();

            for (int i = 0; i < cGlobalInfo.CurrentScreening.ListDescriptors.Count; i++)
            {

                if (cGlobalInfo.CurrentScreening.ListDescriptors[i].IsActive() == false) continue;

                cDesignerSplitter DS = new cDesignerSplitter();
                DS.Orientation = Orientation.Vertical;
                cViewerTable VTable = new cViewerTable();

                cViewertext VT = new cViewertext();

                cExtendedTable FinalTable = new cExtendedTable(cGlobalInfo.ListWellClasses.Count, 0, 0);
                for (int j = 0; j < cGlobalInfo.ListWellClasses.Count; j++)
                {
                    FinalTable[j].Tag = cGlobalInfo.ListWellClasses[j];
                    FinalTable[j].Name = cGlobalInfo.ListWellClasses[j].Name;
                }

                cListWells ListWellsToProcess = new cListWells(null);
                cDescriptorType CurrentDesc = cGlobalInfo.CurrentScreening.ListDescriptors[i];
                // build the table
                foreach (cPlate CurrentPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                {
                    foreach (cWell CurrentWell in CurrentPlate.ListActiveWells)
                    {
                        if ((CurrentWell.GetCurrentClassIdx() < 0) || (ListClassSelected[0][CurrentWell.GetCurrentClassIdx()] == 0)) continue;
                        {
                            FinalTable[CurrentWell.GetCurrentClassIdx()].Add(CurrentWell.GetAverageValue(CurrentDesc));
                            ListWellsToProcess.Add(CurrentWell);
                        }
                    }
                }

                cClean C = new cClean();
                C.SetInputData(FinalTable);
                C.Run();
                cExtendedTable NewTable = C.GetOutPut();

                cTwoSampleFTest TSFTest = null;
                cANOVA Anova = null;
                cTwoSampleUnpooledTTest TwoSampleTTest = null;
                cStudent_tTest StudentTTest = null;

                if (TestType == eTestType.F_TEST)
                {
                    TSFTest = new cTwoSampleFTest();
                    TSFTest.SetInputData(NewTable);
                    TSFTest.Run();
                    VTable.SetInputData(TSFTest.GetOutPut());
                    VT.SetInputData(TSFTest.GetInfo());
                }
                else if (TestType == eTestType.ANOVA)
                {
                    Anova = new cANOVA();
                    Anova.SetInputData(NewTable);
                    Anova.Run();
                    VTable.SetInputData(Anova.GetOutPut());
                    VT.SetInputData(Anova.GetInfo());
                }
                else if (TestType == eTestType.TWO_SAMPLES_T_TEST)
                {
                    TwoSampleTTest = new cTwoSampleUnpooledTTest();
                    TwoSampleTTest.SetInputData(NewTable);
                    TwoSampleTTest.Run();
                    VTable.SetInputData(TwoSampleTTest.GetOutPut());
                    VT.SetInputData(TwoSampleTTest.GetInfo());
                }
                else if (TestType == eTestType.STUDENT_T_TEST)
                {
                    StudentTTest = new cStudent_tTest();
                    StudentTTest.SetInputData(NewTable);
                    StudentTTest.Run();
                    VTable.SetInputData(StudentTTest.GetOutPut());
                    VT.SetInputData(StudentTTest.GetInfo());
                }
                VTable.DigitNumber = -1;
                VTable.Run();
                DS.SetInputData(VTable.GetOutPut());

                VT.Run();
                DS.SetInputData(VT.GetOutPut());
                DS.Run();
                DS.Title = cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName();

                cDesignerSplitter DSMain = new cDesignerSplitter();
                DSMain.Orientation = Orientation.Horizontal;
                DSMain.SetInputData(DS.GetOutPut());

                // Compute and display associated Stacked histogram

                //CDW1.Title = CompleteScreening.ListDescriptors[CompleteScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName() + " - Stacked Histogram - " + PlateList.Count + " plates";

                cExtendedTable TableForHisto = ListWellsToProcess.GetAverageDescriptorValues(i);
                TableForHisto.Name = "";

                cViewerStackedHistogram CV1 = new cViewerStackedHistogram();
                CV1.SetInputData(TableForHisto);
                CV1.Chart.LabelAxisX = cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName();
                CV1.Chart.IsLegend = true;
                CV1.Run();

                DSMain.SetInputData(CV1.GetOutPut());
                DSMain.Title = cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName();
                DSMain.Run();
                DT.SetInputData(DSMain.GetOutPut());
            }

            DT.Run();
            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(DT.GetOutPut());
            DTW.Title = "Classification Significance - " + TestType.ToString();
            DTW.Run();
            DTW.Display();

        }


        private void zScoreToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button != System.Windows.Forms.MouseButtons.Right) || (cGlobalInfo.CurrentScreening == null)) return;
            ContextMenuStripQCOptions.Show(Control.MousePosition);
        }

        private void listViewForListWell_DragDrop(object sender, DragEventArgs e)
        {
            cListWells List_Wells = (cListWells)e.Data.GetData(typeof(cListWells));

            foreach (var item in List_Wells)
            {
                item.AddToListWellsGUI();
            }
        }

        private void listViewForListWell_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(cListWells)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void listViewForListWell_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) RefreshListViewForWellList();
        }

        private void testReplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormForPlateManager WindowForPlateManager = new FormForPlateManager(cGlobalInfo.CurrentScreening);
            WindowForPlateManager.ShowDialog();
        }

        private void saveCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cGlobalInfo.CurrentScreening.SaveCurrentClassStatus();
        }

        private void previousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            cGlobalInfo.CurrentScreening.SetClassStatus(cGlobalInfo.CurrentScreening.CurrentHistoryClassStatusIndex - 1);
        }

        private void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            cGlobalInfo.CurrentScreening.SetClassStatus(cGlobalInfo.CurrentScreening.CurrentHistoryClassStatusIndex + 1);
        }


        private void listViewClassHistory_SaveCurrent(object sender, EventArgs e)
        {
            cGlobalInfo.CurrentScreening.SaveCurrentClassStatus();
        }

        private void listViewClassHistory_Clear(object sender, EventArgs e)
        {
            this.listViewClassHistory.Items.Clear();
            cGlobalInfo.CurrentScreening.ListClassHistoryStatus.Clear();

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell TmpWell in TmpPlate.ListActiveWells)
                {
                    //int History = TmpWell.ListClass.Count - 1;
                    //TmpWell.ListClass.RemoveRange(1, History);


                    TmpWell.CleanClassHistory();

                }
            }
            cGlobalInfo.CurrentScreening.ListClassHistoryStatus.Clear();
            cGlobalInfo.CurrentScreening.SetClassStatus(0);
        }

        private void listViewClassHistory_MouseDown(object sender, MouseEventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip NewMenu = new ContextMenuStrip();

                if (cGlobalInfo.CurrentScreening.ListClassHistoryStatus.Count > 0)
                {
                    ToolStripMenuItem listViewClassHistory_Clear = new ToolStripMenuItem("Clear");
                    listViewClassHistory_Clear.Click += new System.EventHandler(this.listViewClassHistory_Clear);
                    NewMenu.Items.Add(listViewClassHistory_Clear);
                }

                ToolStripMenuItem listViewClassHistory_SaveCurrent = new ToolStripMenuItem("Save Current Status");
                listViewClassHistory_SaveCurrent.Click += new System.EventHandler(this.listViewClassHistory_SaveCurrent);
                listViewClassHistory_SaveCurrent.ShowShortcutKeys = true;
                listViewClassHistory_SaveCurrent.ShortcutKeys = Keys.F5;
                NewMenu.Items.Add(listViewClassHistory_SaveCurrent);

                NewMenu.Show(Control.MousePosition);
            }
        }

        private void listViewClassHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedListViewItemCollection SLIVC = this.listViewClassHistory.SelectedItems;

            if (SLIVC.Count == 1)
            {
                ListViewItem lvItem = SLIVC[0];
                if (lvItem.Index < cGlobalInfo.CurrentScreening.ListClassHistoryStatus.Count)
                    cGlobalInfo.CurrentScreening.SetClassStatus(lvItem.Index);
            }
        }

        private void tabControlForScreening_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshInfoScreeningRichBox();
        }

        private void richTextBoxForScreeningInformation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) RefreshInfoScreeningRichBox();
        }

        private void richTextBoxForScreeningInformation_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip NewMenu = new ContextMenuStrip();

                ToolStripMenuItem ToolStripMenuItem_Clear = new ToolStripMenuItem("Clear");
                ToolStripMenuItem_Clear.Click += new System.EventHandler(this.ToolStripMenuItem_ClearScreeningInfo);
                NewMenu.Items.Add(ToolStripMenuItem_Clear);

                ToolStripMenuItem ToolStripMenuItem_Refresh = new ToolStripMenuItem("Refresh");
                ToolStripMenuItem_Refresh.Click += new System.EventHandler(this.ToolStripMenuItem_RefreshScreeningInfo);
                NewMenu.Items.Add(ToolStripMenuItem_Refresh);

                NewMenu.Show(Control.MousePosition);
            }
        }

        private void ToolStripMenuItem_RefreshScreeningInfo(object sender, EventArgs e)
        {
            RefreshInfoScreeningRichBox();
        }

        private void ToolStripMenuItem_ClearScreeningInfo(object sender, EventArgs e)
        {
            richTextBoxForScreeningInformation.Clear();
            // RefreshInfoScreeningRichBox();
        }

        private void wellsMergingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dRC3DToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors().Count < 3)
            {
                MessageBox.Show("At least 3 descriptors are required for this operation !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cListExtendedTable LET = new cListExtendedTable();

            //List<cListWells> ListWells = new List<cListWells>();
            //ListWells.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListActiveWells);

            // firstly, wells are grouped by class
            //foreach (var item in cGlobalInfo.ListWellClasses)
            //{
            //    List<cWellClassType> TypeForFilter = new List<cWellClassType>();
            //    TypeForFilter.Add(item);
            //    cListWells TmpListWells = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListActiveWells.Filter(TypeForFilter);
            //    cGeneralDRC gDRC = new cGeneralDRC(TmpListWells);
            //    LET.Add(gDRC.ListAverageValues);
            //}


            for (int i = 0; i < 3; i++)
            {
                List<int> GroupForFilter = new List<int>();
                GroupForFilter.Add(i);
                cListWells TmpListWells = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListActiveWells.FilterByGroup(GroupForFilter);
                cGeneralDRC gDRC = new cGeneralDRC(TmpListWells);
                LET.Add(gDRC.ListAverageValues);
            }



            cViewer3D V3D = new cViewer3D();
            c3DNewWorld MyWorld = new c3DNewWorld(new cPoint3D(1, 1, 1), new cPoint3D(1, 1, 1));

            c3DObjectScatterPoints _3DScatterPt = new c3DObjectScatterPoints();
            //_3DScatterPt.SetInputData(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListActiveWells.GetDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors(), false));
            //  _3DScatterPt.GlobalInfo = this.GlobalInfo;
            _3DScatterPt.SetInputData(LET);
            // _3DScatterPt.IsLinked = true;
            _3DScatterPt.Run(MyWorld);

            cListGeometric3DObject GlobalList = _3DScatterPt.GetOutPut();

            foreach (var item in GlobalList)
            {
                MyWorld.AddGeometric3DObject(item);
            }


            MyWorld.BackGroundColor = cGlobalInfo.OptionsWindow.FFAllOptions.panelFor3DBackColor.BackColor;

            V3D.SetInputData(MyWorld);
            V3D.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(V3D.GetOutPut());
            DTW.Title = "3D Dose Response Visualization";
            DTW.Run();
            DTW.Display();
        }

        private void testPubMedSOAPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                eUtilsServiceSoapClient serv = new eUtilsServiceSoapClient();
                // call NCBI EInfo utility
                eInfoResult res = serv.run_eInfo(new eInfoRequest());
                // results output

                string a = "";
                for (int i = 0; i < res.DbList.Items.Length; i++)
                {
                    a += res.DbList.Items[i] + "\r\n";
                }

                richTextBoxConsole.AppendText(a + "\n");

            }
            catch (Exception eee)
            {
                //textBox1.Text = eee.ToString();
            }
        }





        private void tTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cGUI_2ClassesSelection GUI_ListClasses = new cGUI_2ClassesSelection();

            if (GUI_ListClasses.Run(this.GlobalInfo).IsSucceed == false) return;
            cExtendedTable ListClassSelected = GUI_ListClasses.GetOutPut();

            int IdxClassNeg = -1;
            int IdxClassPos = -1;
            for (int IdxC = 0; IdxC < ListClassSelected[0].Count; IdxC++)
            {
                if (ListClassSelected[0][IdxC] == 1) IdxClassNeg = IdxC;
                if (ListClassSelected[1][IdxC] == 1) IdxClassPos = IdxC;
            }

            string SubTitle = "t-Test (both tails - unequal variance)";


            #region single plate and plate by plate

            cDesignerTab DT = new cDesignerTab();
            if ((ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked) || (ProcessModeplateByPlateToolStripMenuItem.Checked)/*||(ProcessModeEntireScreeningToolStripMenuItem.Checked)*/)
            {
                List<cPlate> ListPlatesToProcess = new List<cPlate>();
                if ((ProcessModeplateByPlateToolStripMenuItem.Checked)/*||(ProcessModeEntireScreeningToolStripMenuItem.Checked)*/)
                {
                    foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                        ListPlatesToProcess.Add(TmpPlate);
                }
                else
                    ListPlatesToProcess.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());

                foreach (cPlate TmpPlate in ListPlatesToProcess)
                {
                    cListWells ListWellsToProcess1 = new cListWells(null);
                    cListWells ListWellsToProcess2 = new cListWells(null);

                    foreach (cWell item in TmpPlate.ListActiveWells)
                    {
                        if (item.GetCurrentClassIdx() != -1)
                        {
                            if (ListClassSelected[0][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess1.Add(item);
                            if (ListClassSelected[1][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess2.Add(item);
                        }
                    }

                    cExtendedTable NewTable1 = new cExtendedTable(ListWellsToProcess1, true);
                    cExtendedTable NewTable2 = new cExtendedTable(ListWellsToProcess2, true);

                    if ((NewTable1.Count == 0) || (NewTable1[0].Count < 2) || (NewTable2.Count == 0) || (NewTable2[0].Count < 2))
                    {
                        if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                        {
                            MessageBox.Show("Insufficient number of control wells", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                            continue;
                    }

                    cExtendedList ListZ = new cExtendedList();
                    List<cDescriptorType> ListDescForZFactor = new List<cDescriptorType>();
                    List<string> ListNames = new List<string>();
                    int RealIdx = 0;
                    for (int IDxDesc = 0; IDxDesc < cGlobalInfo.CurrentScreening.ListDescriptors.Count; IDxDesc++)
                    {
                        if (!cGlobalInfo.CurrentScreening.ListDescriptors[IDxDesc].IsActive()) continue;

                        cExtendedTable TableForZ = new cExtendedTable();

                        TableForZ.Add(NewTable1[RealIdx]);
                        TableForZ.Add(NewTable2[RealIdx]);
                        RealIdx++;

                        cStudent_tTest ZF = new cStudent_tTest();
                        ZF.SetInputData(TableForZ);
                        ZF.Run();
                        ListZ.Add(ZF.GetOutPut()[0][1]);

                        ListDescForZFactor.Add(cGlobalInfo.CurrentScreening.ListDescriptors[IDxDesc]);

                    }

                    cExtendedTable ET = new cExtendedTable(new cExtendedTable(ListZ));
                    ET[0].ListTags = new List<object>();
                    ET[0].ListTags.AddRange(ListDescForZFactor);
                    ET.Name = TmpPlate.GetName() + "\n" + SubTitle + " - " + cGlobalInfo.ListWellClasses[IdxClassNeg].Name + " (" + NewTable1[0].Count + " wells) vs. " + cGlobalInfo.ListWellClasses[IdxClassPos].Name + " (" + NewTable2[0].Count + " wells)";
                    ET[0].Name = ET.Name;

                    cSort S = new cSort();
                    S.SetInputData(ET);
                    S.ColumnIndexForSorting = 0;
                    S.Run();

                    //ZFactorList.Sort(delegate(cSimpleSignature p1, cSimpleSignature p2) { return p1.AverageValue.CompareTo(p2.AverageValue); });

                    cViewerGraph1D VG1 = new cViewerGraph1D();
                    VG1.SetInputData(S.GetOutPut());

                    VG1.Chart.LabelAxisY = SubTitle;
                    VG1.Chart.LabelAxisX = "Descriptor";
                    VG1.Chart.IsZoomableX = true;
                    VG1.Chart.IsBar = true;
                    VG1.Chart.IsBorder = true;
                    VG1.Chart.IsDisplayValues = true;
                    VG1.Chart.IsShadow = true;
                    VG1.Chart.MarkerSize = 4;

                    //VG1.Chart.Max
                    VG1.Title = TmpPlate.GetName();

                    Classes.Base_Classes.General.cLineHorizontalForGraph VLZ05 = new Classes.Base_Classes.General.cLineHorizontalForGraph(.05);
                    VLZ05.IsAllowMoving = true;
                    VG1.Chart.ListHorizontalLines.Add(VLZ05);

                    VG1.Run();

                    cDesignerSplitter DS = new cDesignerSplitter();
                    DS.Orientation = Orientation.Vertical;
                    DS.SetInputData(VG1.GetOutPut());

                    cSort S1 = new cSort();
                    S1.SetInputData(ET);
                    S1.ColumnIndexForSorting = 0;
                    S1.IsAscending = false;
                    S1.Run();

                    cViewerTable VT = new cViewerTable();
                    VT.SetInputData(S1.GetOutPut());
                    VT.DigitNumber = -1;
                    VT.Run();

                    DS.SetInputData(VT.GetOutPut());
                    DS.Title = TmpPlate.GetName();
                    DS.Run();

                    DT.SetInputData(DS.GetOutPut());
                }
                DT.Run();

                cDisplayToWindow CDW = new cDisplayToWindow();
                CDW.SetInputData(DT.GetOutPut());//VG1.GetOutPut());


                if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                    CDW.Title = SubTitle + " - " + ListPlatesToProcess[0].GetName();
                else
                    CDW.Title = SubTitle + " - " + ListPlatesToProcess.Count + " plates";

                CDW.Run();
                CDW.Display();
            }
            #endregion

            #region entire screening
            else if (ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                List<cPlate> ListPlatesToProcess = new List<cPlate>();
                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                    ListPlatesToProcess.Add(TmpPlate);

                cExtendedList ListZ = new cExtendedList();
                List<cPlate> ListPlatesForZFactor = new List<cPlate>();
                foreach (cPlate TmpPlate in ListPlatesToProcess)
                {
                    cListWells ListWellsToProcess1 = new cListWells(null);
                    cListWells ListWellsToProcess2 = new cListWells(null);

                    foreach (cWell item in TmpPlate.ListActiveWells)
                    {
                        if (item.GetCurrentClassIdx() != -1)
                        {
                            if (ListClassSelected[0][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess1.Add(item);
                            if (ListClassSelected[1][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess2.Add(item);
                        }
                    }

                    cExtendedTable NewTable1 = new cExtendedTable(ListWellsToProcess1, cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor()));
                    cExtendedTable NewTable2 = new cExtendedTable(ListWellsToProcess2, cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor()));
                    //cExtendedTable NewTable2 = new cExtendedTable(ListWellsToProcess2, false);

                    if ((NewTable1.Count == 0) || (NewTable1[0].Count < 2) || (NewTable2.Count == 0) || (NewTable2[0].Count < 2))
                    {
                        if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                        {
                            MessageBox.Show("Insufficient number of control wells", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                            continue;
                    }

                    cExtendedTable TableForZ = new cExtendedTable();

                    TableForZ.Add(NewTable1[0]);
                    TableForZ.Add(NewTable2[0]);

                    cStudent_tTest ZF = new cStudent_tTest();
                    ZF.SetInputData(TableForZ);
                    ZF.Run();
                    double Zfactor = ZF.GetOutPut()[0][1];
                    ListZ.Add(Zfactor);

                    // update plate quality
                    //TmpPlate.ListProperties.UpdateValueByName("Quality", Math.Exp(Zfactor - 1));
                    //cProperty Prop = TmpPlate.ListProperties.FindByName("Quality");
                    //Prop.Info = ZF.GetInfo();

                    ListPlatesForZFactor.Add(TmpPlate);
                }


                cExtendedTable ET = new cExtendedTable(new cExtendedTable(ListZ));
                ET[0].ListTags = new List<object>();
                ET[0].ListTags.AddRange(ListPlatesForZFactor);
                ET.Name = SubTitle + " - " + cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName() + " - " + cGlobalInfo.ListWellClasses[IdxClassNeg].Name + /*" (" + NewTable1[0].Count + " wells)*/ " vs. " + cGlobalInfo.ListWellClasses[IdxClassPos].Name;// +" (" + NewTable2[0].Count + " wells)";
                ET[0].Name = ET.Name;

                cViewerGraph1D VG1 = new cViewerGraph1D();
                VG1.SetInputData(ET);

                VG1.Chart.LabelAxisY = SubTitle;
                VG1.Chart.LabelAxisX = "Plate";
                VG1.Chart.IsZoomableX = true;
                VG1.Chart.IsBar = true;
                VG1.Chart.IsBorder = true;
                VG1.Chart.IsDisplayValues = true;
                VG1.Chart.IsShadow = true;
                VG1.Chart.MarkerSize = 4;

                Classes.Base_Classes.General.cLineHorizontalForGraph VLZ05 = new Classes.Base_Classes.General.cLineHorizontalForGraph(.05);
                VLZ05.IsAllowMoving = true;
                VG1.Chart.ListHorizontalLines.Add(VLZ05);

                VG1.Title = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
                VG1.Run();

                cDisplayToWindow CDW = new cDisplayToWindow();
                CDW.SetInputData(VG1.GetOutPut());
                CDW.Title = SubTitle + " - " + ListPlatesToProcess.Count + " plates";
                CDW.Run();
                CDW.Display();
            #endregion
            }
        }

        private void mannWithneyTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cGUI_2ClassesSelection GUI_ListClasses = new cGUI_2ClassesSelection();

            if (GUI_ListClasses.Run(this.GlobalInfo).IsSucceed == false) return;
            cExtendedTable ListClassSelected = GUI_ListClasses.GetOutPut();

            int IdxClassNeg = -1;
            int IdxClassPos = -1;
            for (int IdxC = 0; IdxC < ListClassSelected[0].Count; IdxC++)
            {
                if (ListClassSelected[0][IdxC] == 1) IdxClassNeg = IdxC;
                if (ListClassSelected[1][IdxC] == 1) IdxClassPos = IdxC;
            }

            string SubTitle = "Mann-Withney (both tails)";


            #region single plate and plate by plate

            cDesignerTab DT = new cDesignerTab();
            if ((ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked) || (ProcessModeplateByPlateToolStripMenuItem.Checked)/*||(ProcessModeEntireScreeningToolStripMenuItem.Checked)*/)
            {
                List<cPlate> ListPlatesToProcess = new List<cPlate>();
                if ((ProcessModeplateByPlateToolStripMenuItem.Checked)/*||(ProcessModeEntireScreeningToolStripMenuItem.Checked)*/)
                {
                    foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                        ListPlatesToProcess.Add(TmpPlate);
                }
                else
                    ListPlatesToProcess.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());

                foreach (cPlate TmpPlate in ListPlatesToProcess)
                {
                    cListWells ListWellsToProcess1 = new cListWells(null);
                    cListWells ListWellsToProcess2 = new cListWells(null);

                    foreach (cWell item in TmpPlate.ListActiveWells)
                    {
                        if (item.GetCurrentClassIdx() != -1)
                        {
                            if (ListClassSelected[0][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess1.Add(item);
                            if (ListClassSelected[1][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess2.Add(item);
                        }
                    }

                    cExtendedTable NewTable1 = new cExtendedTable(ListWellsToProcess1, true);
                    cExtendedTable NewTable2 = new cExtendedTable(ListWellsToProcess2, true);

                    if ((NewTable1.Count == 0) || (NewTable1[0].Count < 5) || (NewTable2.Count == 0) || (NewTable2[0].Count < 5))
                    {
                        if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                        {
                            MessageBox.Show("Insufficient number of control wells", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                            continue;
                    }

                    cExtendedList ListZ = new cExtendedList();
                    List<cDescriptorType> ListDescForZFactor = new List<cDescriptorType>();
                    List<string> ListNames = new List<string>();
                    int RealIdx = 0;
                    for (int IDxDesc = 0; IDxDesc < cGlobalInfo.CurrentScreening.ListDescriptors.Count; IDxDesc++)
                    {
                        if (!cGlobalInfo.CurrentScreening.ListDescriptors[IDxDesc].IsActive()) continue;

                        cExtendedTable TableForZ = new cExtendedTable();

                        TableForZ.Add(NewTable1[RealIdx]);
                        TableForZ.Add(NewTable2[RealIdx]);
                        RealIdx++;

                        cMannWithneyTest ZF = new cMannWithneyTest();
                        ZF.SetInputData(TableForZ);
                        ZF.Run();
                        ListZ.Add(ZF.GetOutPut()[0][1]);

                        ListDescForZFactor.Add(cGlobalInfo.CurrentScreening.ListDescriptors[IDxDesc]);

                    }

                    cExtendedTable ET = new cExtendedTable(new cExtendedTable(ListZ));
                    ET[0].ListTags = new List<object>();
                    ET[0].ListTags.AddRange(ListDescForZFactor);
                    ET.Name = TmpPlate.GetName() + "\n" + SubTitle + " - " + cGlobalInfo.ListWellClasses[IdxClassNeg].Name + " (" + NewTable1[0].Count + " wells) vs. " + cGlobalInfo.ListWellClasses[IdxClassPos].Name + " (" + NewTable2[0].Count + " wells)";
                    ET[0].Name = ET.Name;

                    cSort S = new cSort();
                    S.SetInputData(ET);
                    S.ColumnIndexForSorting = 0;
                    S.Run();

                    //ZFactorList.Sort(delegate(cSimpleSignature p1, cSimpleSignature p2) { return p1.AverageValue.CompareTo(p2.AverageValue); });

                    cViewerGraph1D VG1 = new cViewerGraph1D();
                    VG1.SetInputData(S.GetOutPut());

                    VG1.Chart.LabelAxisY = SubTitle;
                    VG1.Chart.LabelAxisX = "Descriptor";
                    VG1.Chart.IsZoomableX = true;
                    VG1.Chart.IsBar = true;
                    VG1.Chart.IsBorder = true;
                    VG1.Chart.IsDisplayValues = true;
                    VG1.Chart.IsShadow = true;
                    VG1.Chart.MarkerSize = 4;

                    //VG1.Chart.Max
                    VG1.Title = TmpPlate.GetName();

                    Classes.Base_Classes.General.cLineHorizontalForGraph VLZ05 = new Classes.Base_Classes.General.cLineHorizontalForGraph(.05);
                    VLZ05.IsAllowMoving = true;
                    VG1.Chart.ListHorizontalLines.Add(VLZ05);

                    VG1.Run();

                    cDesignerSplitter DS = new cDesignerSplitter();
                    DS.Orientation = Orientation.Vertical;
                    DS.SetInputData(VG1.GetOutPut());

                    cSort S1 = new cSort();
                    S1.SetInputData(ET);
                    S1.ColumnIndexForSorting = 0;
                    S1.IsAscending = false;
                    S1.Run();

                    cViewerTable VT = new cViewerTable();
                    VT.SetInputData(S1.GetOutPut());
                    VT.DigitNumber = -1;
                    VT.Run();

                    DS.SetInputData(VT.GetOutPut());
                    DS.Title = TmpPlate.GetName();
                    DS.Run();

                    DT.SetInputData(DS.GetOutPut());
                }
                DT.Run();

                cDisplayToWindow CDW = new cDisplayToWindow();
                CDW.SetInputData(DT.GetOutPut());//VG1.GetOutPut());


                if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                    CDW.Title = SubTitle + " - " + ListPlatesToProcess[0].GetName();
                else
                    CDW.Title = SubTitle + " - " + ListPlatesToProcess.Count + " plates";

                CDW.Run();
                CDW.Display();
            }
            #endregion

            #region entire screening
            else if (ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                List<cPlate> ListPlatesToProcess = new List<cPlate>();
                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                    ListPlatesToProcess.Add(TmpPlate);

                cExtendedList ListZ = new cExtendedList();
                List<cPlate> ListPlatesForZFactor = new List<cPlate>();
                foreach (cPlate TmpPlate in ListPlatesToProcess)
                {
                    cListWells ListWellsToProcess1 = new cListWells(null);
                    cListWells ListWellsToProcess2 = new cListWells(null);

                    foreach (cWell item in TmpPlate.ListActiveWells)
                    {
                        if (item.GetCurrentClassIdx() != -1)
                        {
                            if (ListClassSelected[0][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess1.Add(item);
                            if (ListClassSelected[1][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess2.Add(item);
                        }
                    }

                    cExtendedTable NewTable1 = new cExtendedTable(ListWellsToProcess1, cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor()));
                    cExtendedTable NewTable2 = new cExtendedTable(ListWellsToProcess2, cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor()));
                    //cExtendedTable NewTable2 = new cExtendedTable(ListWellsToProcess2, false);

                    if ((NewTable1.Count == 0) || (NewTable1[0].Count < 5) || (NewTable2.Count == 0) || (NewTable2[0].Count < 5))
                    {
                        if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                        {
                            MessageBox.Show("Insufficient number of control wells", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                            continue;
                    }

                    cExtendedTable TableForZ = new cExtendedTable();

                    TableForZ.Add(NewTable1[0]);
                    TableForZ.Add(NewTable2[0]);

                    cMannWithneyTest ZF = new cMannWithneyTest();
                    ZF.SetInputData(TableForZ);
                    ZF.Run();
                    double Zfactor = ZF.GetOutPut()[0][1];
                    ListZ.Add(Zfactor);

                    // update plate quality
                    //TmpPlate.ListProperties.UpdateValueByName("Quality", Math.Exp(Zfactor - 1));
                    //cProperty Prop = TmpPlate.ListProperties.FindByName("Quality");
                    //Prop.Info = ZF.GetInfo();

                    ListPlatesForZFactor.Add(TmpPlate);
                }


                cExtendedTable ET = new cExtendedTable(new cExtendedTable(ListZ));
                ET[0].ListTags = new List<object>();
                ET[0].ListTags.AddRange(ListPlatesForZFactor);
                ET.Name = SubTitle + " - " + cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName() + " - " + cGlobalInfo.ListWellClasses[IdxClassNeg].Name + /*" (" + NewTable1[0].Count + " wells)*/ " vs. " + cGlobalInfo.ListWellClasses[IdxClassPos].Name;// +" (" + NewTable2[0].Count + " wells)";
                ET[0].Name = ET.Name;

                cViewerGraph1D VG1 = new cViewerGraph1D();
                VG1.SetInputData(ET);

                VG1.Chart.LabelAxisY = SubTitle;
                VG1.Chart.LabelAxisX = "Plate";
                VG1.Chart.IsZoomableX = true;
                VG1.Chart.IsBar = true;
                VG1.Chart.IsBorder = true;
                VG1.Chart.IsDisplayValues = true;
                VG1.Chart.IsShadow = true;
                VG1.Chart.MarkerSize = 4;


                Classes.Base_Classes.General.cLineHorizontalForGraph VLZ05 = new Classes.Base_Classes.General.cLineHorizontalForGraph(.05);
                VLZ05.IsAllowMoving = true;
                VG1.Chart.ListHorizontalLines.Add(VLZ05);

                VG1.Title = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
                VG1.Run();

                cDisplayToWindow CDW = new cDisplayToWindow();
                CDW.SetInputData(VG1.GetOutPut());
                CDW.Title = SubTitle + " - " + ListPlatesToProcess.Count + " plates";
                CDW.Run();
                CDW.Display();
            #endregion
            }
        }

        private void histogramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cGUI_ListClasses GUIClasses = new cGUI_ListClasses();
            GUIClasses.IsCheckBoxes = false;
            GUIClasses.ClassType = eClassType.PHENOTYPE;
            if (!GUIClasses.Run().IsSucceed) return;

            cGUI_ListClasses GUIClassesWells = new cGUI_ListClasses();
            GUIClassesWells.IsCheckBoxes = true;
            GUIClassesWells.ClassType = eClassType.WELL;
            if (!GUIClassesWells.Run().IsSucceed) return;

            List<cWellClassType> ListClass = new List<cWellClassType>();
            for (int i = 0; i < GUIClassesWells.GetOutPut()[0].Count; i++)
            {
                if (GUIClassesWells.GetOutPut()[0][i] == 1)
                    ListClass.Add(cGlobalInfo.ListWellClasses[i]);
            }


            cGUI_ListPlates GUIListPlates = new cGUI_ListPlates();
            GUIListPlates.IsCheckBoxes = true;
            if (!GUIListPlates.Run().IsSucceed) return;

            List<cDescriptorType> LCDT = new List<cDescriptorType>();
            LCDT.Add(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor());


            cDesignerTab DT = new cDesignerTab();
            DT.IsMultiline = false;

            foreach (var TmpPlate in GUIListPlates.GetOutPut())
            {
                cListWells CurrentPlateListWells = TmpPlate.ListWells.Filter(ListClass);

                cExtendedTable FinalTable = new cExtendedTable();
                FinalTable.Name = TmpPlate.GetName() + " - " + CurrentPlateListWells.Count + " wells";

                for (int i = 0; i < GUIClasses.GetOutPut()[0].Count; i++)
                {
                    if (GUIClasses.GetOutPut()[0][i] == 1)
                    {
                        FinalTable.Add(new cExtendedList());
                        FinalTable[0].Name = cGlobalInfo.ListCellularPhenotypes[i].Name;
                        FinalTable[0].Tag = cGlobalInfo.ListCellularPhenotypes[i];
                    }
                }

                foreach (cWell TmpWell in CurrentPlateListWells)
                {
                    TmpWell.AssociatedPlate.DBConnection = new cDBConnection(TmpWell.AssociatedPlate, TmpWell.SQLTableName);

                    for (int i = 0; i < GUIClasses.GetOutPut()[0].Count; i++)
                    {
                        if (GUIClasses.GetOutPut()[0][i] == 1)
                        {
                            List<cCellularPhenotype> ListCellularPhenotypesToBeSelected = new List<cCellularPhenotype>();
                            ListCellularPhenotypesToBeSelected.Add(cGlobalInfo.ListCellularPhenotypes[i]);

                            cExtendedTable TmpET = TmpWell.AssociatedPlate.DBConnection.GetWellValues(TmpWell,
                                                     LCDT, ListCellularPhenotypesToBeSelected);

                            if (TmpET.Count > 0) FinalTable[0].AddRange(TmpET[0]);
                            break;
                        }
                    }
                    TmpWell.AssociatedPlate.DBConnection.CloseConnection();
                }

                cViewerHistogram VH = new cViewerHistogram();
                //cViewerStackedHistogram VSH = new cViewerStackedHistogram();
                VH.SetInputData(FinalTable);
                //VH.Chart.BinNumber = -1;
                //VSH.Chart.Is

                //VH.ListProperties.FindByName("Bin Number").SetNewValue((int)100);
                //VH.ListProperties.FindByName("Bin Number").IsGUIforValue = true;

                //  VSH.Chart.BinNumber = LCDT[0].GetBinNumber();
                VH.Chart.IsShadow = false;
                VH.Chart.IsBorder = true;
                VH.Chart.IsXGrid = true;
                VH.Chart.IsHistoNormalized = true;
                VH.Chart.IsBar = true;

                VH.Chart.IsYGrid = true;
                VH.Chart.LabelAxisX = LCDT[0].GetName();
                VH.Title = TmpPlate.GetName();
                VH.Run();
                VH.Chart.Width = 0;
                VH.Chart.Height = 0;


                cDesignerSplitter DS = new cDesignerSplitter();
                DS.Orientation = Orientation.Vertical;
                DS.SetInputData(VH.GetOutPut());

                cHistogramBuilder HB = new cHistogramBuilder();
                HB.SetInputData(FinalTable);
                HB.BinNumber = -1;
                HB.IsNormalized = true;
                HB.Run();

                cViewerTable VT = new cViewerTable();
                VT.SetInputData(HB.GetOutPut());
                VT.Run();

                DS.SetInputData(VT.GetOutPut());
                DS.Title = TmpPlate.GetName();
                DS.Run();
                //DS.SetInputData(HB.Get


                DT.SetInputData(DS.GetOutPut());

            }

            DT.Run();
            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(DT.GetOutPut());
            DTW.Title = "Single cell based Histograms";
            DTW.Run();
            DTW.Display();


        }

        private void aNOVAToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            GUI_ListClasses.IsCheckBoxes = true;
            GUI_ListClasses.IsSelectAll = true;


            if (GUI_ListClasses.Run().IsSucceed == false) return;
            cExtendedTable ListClassSelected = GUI_ListClasses.GetOutPut();

            string SubTitle = "ANOVA (One-Way)";


            #region single plate and plate by plate

            cDesignerTab DT = new cDesignerTab();
            if ((ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked) || (ProcessModeplateByPlateToolStripMenuItem.Checked)/*||(ProcessModeEntireScreeningToolStripMenuItem.Checked)*/)
            {
                List<cPlate> ListPlatesToProcess = new List<cPlate>();
                if ((ProcessModeplateByPlateToolStripMenuItem.Checked)/*||(ProcessModeEntireScreeningToolStripMenuItem.Checked)*/)
                {
                    foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                        ListPlatesToProcess.Add(TmpPlate);
                }
                else
                    ListPlatesToProcess.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());

                foreach (cPlate TmpPlate in ListPlatesToProcess)
                {
                    cExtendedTable NewTable1 = new cExtendedTable();
                    List<cListWells> ListListWells = new List<cListWells>();


                    int NumWells = 0;

                    for (int IdxClassWell = 0; IdxClassWell < ListClassSelected[0].Count; IdxClassWell++)
                    {
                        if (ListClassSelected[0][IdxClassWell] >= 1)
                        {
                            List<cWellClassType> LCT = new List<cWellClassType>();
                            LCT.Add(cGlobalInfo.ListWellClasses[IdxClassWell]);

                            cListWells TmpList = TmpPlate.ListWells.Filter(LCT);

                            if (TmpList.Count >= 3)
                            {
                                ListListWells.Add(TmpList);
                                NumWells += TmpList.Count;
                            }
                        }
                    }

                    if (ListListWells.Count <= 1)
                    {
                        if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                        {
                            MessageBox.Show("Insufficient number of control wells", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                            continue;
                    }


                    cExtendedTable CompleteReport = null;
                    cExtendedList ListZ = new cExtendedList();
                    List<cDescriptorType> ListDescForZFactor = new List<cDescriptorType>();
                    List<string> ListNames = new List<string>();
                    int RealIdx = 0;
                    for (int IDxDesc = 0; IDxDesc < cGlobalInfo.CurrentScreening.ListDescriptors.Count; IDxDesc++)
                    {
                        if (!cGlobalInfo.CurrentScreening.ListDescriptors[IDxDesc].IsActive()) continue;

                        cExtendedTable TableForZ = new cExtendedTable();

                        foreach (var item in ListListWells)
                        {
                            List<cDescriptorType> LType = new List<cDescriptorType>();
                            LType.Add(cGlobalInfo.CurrentScreening.ListDescriptors[IDxDesc]);
                            cExtendedTable TTable = item.GetAverageDescriptorValues(LType, false, false);
                            TableForZ.Add(TTable[0]);
                        }
                        RealIdx++;

                        cANOVA ZF = new cANOVA();
                        ZF.SetInputData(TableForZ);
                        ZF.Run();
                        ListZ.Add(ZF.GetOutPut()[0][0]);

                        ZF.GetOutPut()[0].Name = cGlobalInfo.CurrentScreening.ListDescriptors[IDxDesc].GetName();

                        if (CompleteReport == null)
                            CompleteReport = new cExtendedTable(ZF.GetOutPut());
                        else
                        {
                            cMerge M = new cMerge();
                            M.IsHorizontal = true;
                            M.SetInputData(CompleteReport, ZF.GetOutPut());
                            M.Run();
                            CompleteReport = M.GetOutPut();
                        }

                        ListDescForZFactor.Add(cGlobalInfo.CurrentScreening.ListDescriptors[IDxDesc]);
                    }

                    cExtendedTable ET = new cExtendedTable(new cExtendedTable(ListZ));
                    ET[0].ListTags = new List<object>();
                    ET[0].ListTags.AddRange(ListDescForZFactor);
                    ET.Name = TmpPlate.GetName() + "\n" + SubTitle + " - " + NumWells + " wells";// +" - " +cGlobalInfo.ListWellClasses[IdxClassNeg].Name + " (" + NewTable1[0].Count + " wells) vs. " +cGlobalInfo.ListWellClasses[IdxClassPos].Name + " (" + NewTable2[0].Count + " wells)";
                    ET[0].Name = ET.Name;

                    cSort S = new cSort();
                    S.SetInputData(ET);
                    S.IsAscending = false;
                    S.ColumnIndexForSorting = 0;
                    S.Run();

                    //ZFactorList.Sort(delegate(cSimpleSignature p1, cSimpleSignature p2) { return p1.AverageValue.CompareTo(p2.AverageValue); });

                    cViewerGraph1D VG1 = new cViewerGraph1D();
                    VG1.SetInputData(S.GetOutPut());

                    VG1.Chart.LabelAxisY = SubTitle;
                    VG1.Chart.LabelAxisX = "Descriptor";
                    VG1.Chart.IsZoomableX = true;
                    VG1.Chart.IsBar = true;
                    VG1.Chart.IsBorder = true;
                    VG1.Chart.IsDisplayValues = true;
                    VG1.Chart.IsShadow = true;
                    VG1.Chart.MarkerSize = 4;

                    //VG1.Chart.Max
                    VG1.Title = TmpPlate.GetName();

                    // Classes.Base_Classes.General.cLineHorizontalForGraph VLZ05 = new Classes.Base_Classes.General.cLineHorizontalForGraph(.05);
                    // VLZ05.IsAllowMoving = true;
                    //  VG1.Chart.ListHorizontalLines.Add(VLZ05);

                    VG1.Run();

                    cDesignerSplitter DS = new cDesignerSplitter();
                    DS.Orientation = Orientation.Vertical;
                    DS.SetInputData(VG1.GetOutPut());


                    cTranspose T = new cTranspose();
                    T.SetInputData(CompleteReport);
                    T.Run();
                    CompleteReport = T.GetOutPut();
                    CompleteReport[0].Name = "P-Value";
                    CompleteReport[1].Name = "Is significant ?";

                    cSort S1 = new cSort();
                    S1.SetInputData(CompleteReport);
                    S1.ColumnIndexForSorting = 0;
                    S1.IsAscending = true;
                    S1.Run();



                    cViewerTable VT = new cViewerTable();
                    VT.SetInputData(S1.GetOutPut());
                    VT.DigitNumber = -1;
                    VT.Run();

                    DS.SetInputData(VT.GetOutPut());
                    DS.Title = TmpPlate.GetName();
                    DS.Run();

                    DT.SetInputData(DS.GetOutPut());
                }
                DT.Run();

                cDisplayToWindow CDW = new cDisplayToWindow();
                CDW.SetInputData(DT.GetOutPut());//VG1.GetOutPut());


                if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                    CDW.Title = SubTitle + " - " + ListPlatesToProcess[0].GetName();
                else
                    CDW.Title = SubTitle + " - " + ListPlatesToProcess.Count + " plates";

                CDW.Run();
                CDW.Display();
            }
            #endregion

            #region entire screening
            else if (ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                List<cPlate> ListPlatesToProcess = new List<cPlate>();
                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                    ListPlatesToProcess.Add(TmpPlate);

                cExtendedList ListZ = new cExtendedList();
                List<cPlate> ListPlatesForZFactor = new List<cPlate>();
                foreach (cPlate TmpPlate in ListPlatesToProcess)
                {
                    cListWells ListWellsToProcess1 = new cListWells(null);
                    cListWells ListWellsToProcess2 = new cListWells(null);

                    foreach (cWell item in TmpPlate.ListActiveWells)
                    {
                        if (item.GetCurrentClassIdx() != -1)
                        {
                            if (ListClassSelected[0][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess1.Add(item);
                            if (ListClassSelected[1][item.GetCurrentClassIdx()] == 1)
                                ListWellsToProcess2.Add(item);
                        }
                    }

                    cExtendedTable NewTable1 = new cExtendedTable(ListWellsToProcess1, cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor()));
                    cExtendedTable NewTable2 = new cExtendedTable(ListWellsToProcess2, cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor()));
                    //cExtendedTable NewTable2 = new cExtendedTable(ListWellsToProcess2, false);

                    if ((NewTable1.Count == 0) || (NewTable1[0].Count < 2) || (NewTable2.Count == 0) || (NewTable2[0].Count < 2))
                    {
                        if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                        {
                            MessageBox.Show("Insufficient number of control wells", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                            continue;
                    }

                    cExtendedTable TableForZ = new cExtendedTable();

                    TableForZ.Add(NewTable1[0]);
                    TableForZ.Add(NewTable2[0]);

                    cStudent_tTest ZF = new cStudent_tTest();
                    ZF.SetInputData(TableForZ);
                    ZF.Run();
                    double Zfactor = ZF.GetOutPut()[0][1];
                    ListZ.Add(Zfactor);

                    // update plate quality
                    //TmpPlate.ListProperties.UpdateValueByName("Quality", Math.Exp(Zfactor - 1));
                    //cProperty Prop = TmpPlate.ListProperties.FindByName("Quality");
                    //Prop.Info = ZF.GetInfo();

                    ListPlatesForZFactor.Add(TmpPlate);
                }


                cExtendedTable ET = new cExtendedTable(new cExtendedTable(ListZ));
                ET[0].ListTags = new List<object>();
                ET[0].ListTags.AddRange(ListPlatesForZFactor);
                ET.Name = SubTitle + " - " + cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();// +" - " +cGlobalInfo.ListWellClasses[IdxClassNeg].Name + /*" (" + NewTable1[0].Count + " wells)*/ " vs. " +cGlobalInfo.ListWellClasses[IdxClassPos].Name;// +" (" + NewTable2[0].Count + " wells)";
                ET[0].Name = ET.Name;

                cViewerGraph1D VG1 = new cViewerGraph1D();
                VG1.SetInputData(ET);

                VG1.Chart.LabelAxisY = SubTitle;
                VG1.Chart.LabelAxisX = "Plate";
                VG1.Chart.IsZoomableX = true;
                VG1.Chart.IsBar = true;
                VG1.Chart.IsBorder = true;
                VG1.Chart.IsDisplayValues = true;
                VG1.Chart.IsShadow = true;
                VG1.Chart.MarkerSize = 4;



                VG1.Title = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
                VG1.Run();

                cDisplayToWindow CDW = new cDisplayToWindow();
                CDW.SetInputData(VG1.GetOutPut());
                CDW.Title = SubTitle + " - " + ListPlatesToProcess.Count + " plates";
                CDW.Run();
                CDW.Display();
            #endregion
            }

        }

        private void sigmoidFittToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int NumberOfReplicate = 4;
            int NumberOfCurves = 30;
            int DRCNumberofPoints = 12;

            cExtendedTable FullResults = new cExtendedTable();
            FullResults.ListRowNames = new List<string>();

            double RealEC50 = 0.0001;
            int NumberOfDiffEC50 = 20;
            int NumberOfSlopes = 100;

            //  for (int EC50 = 0; EC50 < NumberOfDiffEC50; EC50++)
            {




                //double TmpSlopeValue = 0.0;
                for (int EC50 = 0; EC50 < NumberOfDiffEC50; EC50++)
                //for (int SlopeTmp = 0; SlopeTmp < NumberOfSlopes; SlopeTmp++)
                {
                    RealEC50 *= 2;
                    //TmpSlopeValue += 0.1;
                    cExtendedList ListPValues = new cExtendedList("EC50 - " + RealEC50/*TmpSlopeValue*/);


                    cDesignerTab DT = new cDesignerTab();
                    DT.IsMultiline = false;

                    cDataGeneratorTitration GT = new cDataGeneratorTitration();
                    GT.NumberOfPoint = DRCNumberofPoints;
                    GT.Start = 5;
                    GT.DilutionFactor = 2;
                    GT.Run();


                    Random RND = new Random();

                    //cExtendedTable EXT = new cExtendedTable();
                    //EXT.ListRowNames = new List<string>();
                    //EXT.Name = "Quality fitting Report";
                    //EXT.Add(new cExtendedList("Noise Level"));
                    //EXT.Add(new cExtendedList("Original EC50"));
                    //EXT.Add(new cExtendedList("Estimated EC50"));
                    //EXT.Add(new cExtendedList("p-Value"));

                    for (int i = 0; i < NumberOfCurves; i++)
                    {
                        cExtendedTable FinalTable = new cExtendedTable();

                        cDataGeneratorSigmoid DGS = null;
                        DGS = new cDataGeneratorSigmoid();
                        DGS.SetInputData(GT.GetOutPut()[0]);
                        DGS.EC50 = RealEC50;// 0.01;// 1;// 0.1;// Math.Pow((double)10, -2);
                        DGS.Bottom = 0;
                        DGS.Top = 100;
                        DGS.Slope = 2;// TmpSlopeValue;//1;
                        DGS.Run();

                        cAddNoise AN = new cAddNoise();
                        AN.DistributionType = eRandDistributionType.GAUSSIAN;

                        double NoiseLevel = (i + 1) * 5;
                        //   EXT[0].Add(NoiseLevel);
                        //   EXT[1].Add(DGS.EC50);

                        #region loop over the replicates
                        for (int NumReplic = 0; NumReplic < NumberOfReplicate; NumReplic++)
                        {
                            //AN.Mean = 0;
                            //AN.Stdv = 10;

                            //AN.SetInputData(DGS.GetOutPut());
                            //AN.Run();

                            cExtendedList NewL = DGS.GetOutPut()[1];
                            for (int k = 0; k < NewL.Count; k++)
                            {
                                NewL[k] += RND.NextDouble() * NoiseLevel;
                            }

                            cExtendedTable TmpTable = new cExtendedTable();
                            TmpTable.Add(GT.GetOutPut()[0]);
                            TmpTable.Add(/*AN.GetOutPut()[1]*/NewL);

                            if (NumReplic >= 1)
                            {
                                cMerge M = new cMerge();
                                M.IsHorizontal = false;
                                M.SetInputData(TmpTable, FinalTable);
                                M.Run();
                                FinalTable = M.GetOutPut();
                            }
                            else
                                FinalTable = new cExtendedTable(TmpTable);

                        }
                        #endregion

                        //cListWell LW = new cListWell(null);
                        //foreach (cWell item in CompleteScreening.GetCurrentDisplayPlate().ListActiveWells)
                        //{
                        //    if (item.GetClassIdx() == 0)
                        //        LW.Add(item);
                        //}
                        //cExtendedTable ET = LW.GetDescriptorValues(CompleteScreening.ListDescriptors.GetActiveDescriptors(), true);


                        cCurveForGraph CFG = new cCurveForGraph();
                        CFG.SetInputData(FinalTable);
                        CFG.Run();

                        // compute ANOVA
                        cANOVA S = new cANOVA();
                        cExtendedTable NewTable = CFG.ListPtValues.Crop(0, CFG.ListPtValues.Count - 1, 1, CFG.ListPtValues[0].Count - 1);
                        S.SignificanceThreshold = 1E-11;
                        S.SetInputData(NewTable);
                        S.Run();

                        //cLinearRegression LR = new cLinearRegression();
                        //LR.SetInputData(FinalTable);
                        //LR.Run();

                        cSigmoidFitting SF = new cSigmoidFitting();
                        SF.SetInputData(FinalTable);
                        if (SF.Run().IsSucceed == false) continue;

                        // double Ratio = LR.GetOutPut()[0][LR.GetOutPut().Count - 1] / SF.GetOutPut()[0][SF.GetOutPut().Count - 1];


                        cExtendedTable Sigmoid = SF.GetFittedRawValues(GT.GetOutPut()[0]);
                        FinalTable[0] = Sigmoid[1];

                        cDesignerSplitter DS = new cDesignerSplitter();

                        ////cViewerTableAsRichText VT = new cViewerTableAsRichText();
                        cViewerTable VT = new cViewerTable();
                        cExtendedTable TableResults = SF.GetOutPut();
                        TableResults[0].Add(S.GetOutPut()[0][0]);

                        ListPValues.Add(S.GetOutPut()[0][0]);

                        TableResults[0].Add(S.GetOutPut()[0][1]);
                        TableResults.ListRowNames.Add("p-Value");
                        TableResults.ListRowNames.Add("Null hyp. rejected?");

                        VT.SetInputData(TableResults);
                        VT.DigitNumber = -1;
                        VT.Run();

                        cViewerGraph1D VS1 = new cViewerGraph1D();

                        VS1.SetInputData(new cExtendedTable(Sigmoid[1]));

                        VS1.AddCurve(CFG);

                        VS1.Chart.X_AxisValues = Sigmoid[0];//DGS.GetOutPut()[0];
                        VS1.Chart.IsLogAxis = true;
                        VS1.Chart.IsLine = true;
                        VS1.Chart.IsShadow = true;
                        VS1.Chart.Opacity = 210;
                        VS1.Chart.LineWidth = 3;

                        VS1.Chart.LabelAxisX = "Concentration";
                        VS1.Chart.LabelAxisY = "Readout";
                        VS1.Chart.XAxisFormatDigitNumber = -1;
                        VS1.Chart.IsZoomableX = true;

                        //Classes.Base_Classes.General.cLineVerticalForGraph VLForEC50 = new Classes.Base_Classes.General.cLineVerticalForGraph(SF.GetOutPut()[0][2]);
                        //VLForEC50.AddText("EC50: " + SF.GetOutPut()[0][2].ToString("e3")/* + "\nError Ratio:" + Ratio.ToString("N4")*/);
                        //VS1.Chart.ListVerticalLines.Add(VLForEC50);
                        //VS1.Chart.ArraySeriesInfo = new cSerieInfoDesign[FinalTable.Count];

                        //EXT[2].Add(SF.GetOutPut()[0][2]);
                        //EXT[3].Add(S.GetOutPut()[0][0]);

                        //for (int IdxCurve = 0; IdxCurve < FinalTable.Count; IdxCurve++)
                        //{
                        //    cSerieInfoDesign TmpSerieInfo = new cSerieInfoDesign();
                        //    TmpSerieInfo.color = Color.FromArgb(100, GlobalInfo.ListCellularPhenotypes[IdxCurve % GlobalInfo.ListCellularPhenotypes.Count].ColourForDisplay);

                        //    TmpSerieInfo.markerStyle = MarkerStyle.Circle;

                        //    VS1.Chart.ArraySeriesInfo[IdxCurve] = TmpSerieInfo;
                        //}

                        VS1.Run();

                        DS.SetInputData(VS1.GetOutPut());
                        DS.SetInputData(VT.GetOutPut());
                        DS.Orientation = Orientation.Horizontal;
                        DS.Title = "Noise Stdev " + NoiseLevel;
                        DS.Run();
                        DT.SetInputData(DS.GetOutPut());


                        //  EXT.ListRowNames.Add(DS.Title);

                    }

                    DT.Run();

                    //cDisplayToWindow DTW = new cDisplayToWindow();
                    //DTW.SetInputData(DT.GetOutPut());
                    //DTW.Run();
                    //DTW.Display();

                    //cDisplayExtendedTable DET = new cDisplayExtendedTable();
                    //DET.SetInputData(EXT);
                    //DET.DigitNumber = -1;
                    //DET.Run();  


                    FullResults.Add(ListPValues);
                }

            }

            cDisplayExtendedTable DET = new cDisplayExtendedTable();
            DET.SetInputData(FullResults);
            DET.DigitNumber = -1;
            DET.Run();

        }

        private void checkedListBoxActiveDescriptors_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point locationOnForm = checkedListBoxActiveDescriptors.FindForm().PointToClient(Control.MousePosition);
            int IdxItem = checkedListBoxActiveDescriptors.IndexFromPoint(e.Location);
            cGlobalInfo.CurrentScreening.ListDescriptors[IdxItem].WindowDescriptorInfo.ShowDialog();
            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
        }


        private void copyAverageValuesToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            toolStripMenuItemDescManagement.DropDownItems.Clear();

            List<ToolStripMenuItem> ListMenus = GetGeneralListDescMenu();
            foreach (var item in ListMenus) toolStripMenuItemDescManagement.DropDownItems.Add(item);

            pasteToolStripMenuItem.Enabled = Clipboard.ContainsText();

        }


        private void mahalanobisDistanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormForMahalanobisDistance MainWindow = new FormForMahalanobisDistance();

            PanelForClassSelection ClassSelectionPanel = new PanelForClassSelection(true, eClassType.WELL);
            ClassSelectionPanel.Height = MainWindow.panelForSourceCloud.Height;
            MainWindow.panelForSourceCloud.Controls.Add(ClassSelectionPanel);

            PanelForClassSelection HitClassSelectionPanel = new PanelForClassSelection(false, eClassType.WELL);
            HitClassSelectionPanel.Height = MainWindow.panelHitClass.Height;
            HitClassSelectionPanel.ListRadioButtons[1].Checked = true;
            MainWindow.panelHitClass.Controls.Add(HitClassSelectionPanel);

            if (MainWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            cExtendedList ListClassSelected = new cExtendedList();
            for (int i = 0; i < ClassSelectionPanel.ListCheckBoxes.Count; i++)
                if (ClassSelectionPanel.ListCheckBoxes[i].Checked) ListClassSelected.Add(1);
                else
                    ListClassSelected.Add(0);

            int IdxClassForOutliers = 0;
            for (int i = 0; i < HitClassSelectionPanel.ListRadioButtons.Count; i++)
                if (HitClassSelectionPanel.ListRadioButtons[i].Checked)
                {
                    IdxClassForOutliers = i;
                    break;
                }

            if (ListClassSelected.Sum() < 1)
            {
                MessageBox.Show("At least one classe has to be selected.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // start by computing the inversed covariance matrix
            //if (checkedListBoxActiveDescriptors.CheckedItems.Count <= 1)
            //{
            //    MessageBox.Show("At least two descriptors have to be selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}


            cCovarianceMatrix CM = new cCovarianceMatrix();
            cExtendedTable NewTable = null;

            if (this.ProcessModeplateByPlateToolStripMenuItem.Checked)
            {
                //foreach (cPlate TmpPlate in CompleteScreening.ListPlatesActive)
                //{
                //    cListWells ListWellsToProcess = new cListWells(null);
                //    foreach (cWell item in TmpPlate.ListActiveWells)
                //        if ((item.GetClassIdx() != -1) && (ListClassSelected[item.GetClassIdx()] == 1)) ListWellsToProcess.Add(item);

                //    cExtendedTable NewTable = new cExtendedTable(ListWellsToProcess, true);

                //    CM.SetInputData(NewTable);
                //}
            }
            else if (this.ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                cListWells ListWellsToProcess = new cListWells(null);

                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                    foreach (cWell item in TmpPlate.ListActiveWells)
                        if ((item.GetCurrentClassIdx() != -1) && (ListClassSelected[item.GetCurrentClassIdx()] == 1)) ListWellsToProcess.Add(item);

                NewTable = new cExtendedTable(ListWellsToProcess, true);

                //  cTwoSampleFTest CM = new cTwoSampleFTest();
                CM.SetInputData(NewTable);
            }
            else
            {
                cListWells ListWellsToProcess = new cListWells(null);

                foreach (cWell item in cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListActiveWells)
                    if ((item.GetCurrentClassIdx() != -1) && (ListClassSelected[item.GetCurrentClassIdx()] == 1)) ListWellsToProcess.Add(item);

                NewTable = new cExtendedTable(ListWellsToProcess, true);

                // cTwoSampleFTest CM = new cTwoSampleFTest();
                CM.SetInputData(NewTable);
            }

            CM.Run();

            cInverse cI = new cInverse();
            cI.SetInputData(CM.GetOutPut());
            cI.Run();

            // get the cloud center
            cStatistics cstat = new cStatistics();
            cstat.UnselectAll();
            cstat.IsMean = true;
            cstat.SetInputData(NewTable);
            cstat.Run();

            if (cstat.GetOutPut() == null) return;

            cExtendedList ListMeans = cstat.GetOutPut().GetRow(0);
            cDescriptorType MahalanobisType = new cDescriptorType("Mahalanobis Distance", true, 1);

            #region Compute the Threshold
            int DegreeOfFreedom = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors().Count;
            cExtendedTable T = new cExtendedTable();
            T.Add(new cExtendedList());

            T[0].Add((double)MainWindow.numericUpDownPValue.Value);

            cFunctions F = new cFunctions();
            F.SetInputData(T);
            F.IsInverse = true;
            F.DegreeOfFreedom = DegreeOfFreedom;
            F.Run();

            double ThresholdForMahalanobis = Math.Sqrt(F.GetOutPut()[1][0]);

            #endregion


            int IdxClassForNonOutliers = 1;

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                for (int Col = 0; Col < cGlobalInfo.CurrentScreening.Columns; Col++)
                    for (int Row = 0; Row < cGlobalInfo.CurrentScreening.Rows; Row++)
                    {
                        cWell TmpWell = TmpPlate.GetWell(Col, Row, false);
                        if (TmpWell == null) continue;

                        double ValueDistance = TmpWell.GetAverageValuesList(true)[0].Dist_Mahalanobis(ListMeans, cI.GetOutPut());
                        if (ValueDistance > ThresholdForMahalanobis || double.IsNaN(ValueDistance) )        // that's an outlier
                        {
                            TmpWell.SetClass(IdxClassForOutliers);
                        }
                        else
                        {
                            TmpWell.SetClass(IdxClassForNonOutliers);
                        }

                        if (MainWindow.checkBoxDistAsDesc.Checked)
                        {
                            cListSignature LDesc = new cListSignature();
                            cSignature NewDesc = new cSignature(ValueDistance, MahalanobisType, cGlobalInfo.CurrentScreening);
                            LDesc.Add(NewDesc);
                            TmpWell.AddSignatures(LDesc);
                        }
                    }
            }

            if (MainWindow.checkBoxDistAsDesc.Checked)
            {
                cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(MahalanobisType);
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }

        private void hitsDistributionMapToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (cGlobalInfo.CurrentScreening == null) return;
            List<cPanelForDisplayArray> ListPlates = new List<cPanelForDisplayArray>();

            foreach (cPlate CurrentPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
            {
                ListPlates.Add(new FormToDisplayPlate(CurrentPlate));
            }

            cWindowToDisplayEntireScreening WindowToDisplayArray = new cWindowToDisplayEntireScreening(ListPlates, cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName(), 6);
            WindowToDisplayArray.checkBoxDisplayClasses.Checked = true;
            WindowToDisplayArray.Text = "Generate Hits Distribution Maps";

            WindowToDisplayArray.Show();


            System.Windows.Forms.DialogResult ResWin = MessageBox.Show("By applying this process, the current screening will be entirely updated ! Proceed ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (ResWin == System.Windows.Forms.DialogResult.No)
            {
                WindowToDisplayArray.Close();
                return;
            }

            WindowToDisplayArray.Close();
            if (cGlobalInfo.CurrentScreening != null) cGlobalInfo.CurrentScreening.Close3DView();

            //   CompleteScreening.ListDescriptors.RemoveDesc(CompleteScreening.ListDescriptors[IntToTransfer], CompleteScreening);
            cScreening MergedScreening = new cScreening("Class Screen");
            MergedScreening.PanelForPlate = this.panelForPlate;

            MergedScreening.Rows = cGlobalInfo.CurrentScreening.Rows;
            MergedScreening.Columns = cGlobalInfo.CurrentScreening.Columns;
            MergedScreening.ListPlatesAvailable = new cListPlates();

            // create the descriptor
            MergedScreening.ListDescriptors.Clean();

            List<cDescriptorType> ListDescType = new List<cDescriptorType>();
            List<int[][]> Values = new List<int[][]>();

            for (int i = 0; i < cGlobalInfo.ListWellClasses.Count; i++)
            {
                cDescriptorType DescClass = new cDescriptorType("Class_" + i, true, 1);
                ListDescType.Add(DescClass);
                MergedScreening.ListDescriptors.AddNew(DescClass);

                int[][] TMpVal = new int[MergedScreening.Columns][];
                for (int ii = 0; ii < MergedScreening.Columns; ii++)
                    TMpVal[ii] = new int[MergedScreening.Rows];

                Values.Add(TMpVal);
            }

            MergedScreening.ListDescriptors.CurrentSelectedDescriptorIdx = 0;

            foreach (cPlate CurrentPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
            {
                foreach (cWell TmpWell in CurrentPlate.ListActiveWells)
                {
                    int Class = TmpWell.GetCurrentClassIdx();
                    if (Class >= 0)
                        Values[Class][TmpWell.GetPosX() - 1][TmpWell.GetPosY() - 1]++;
                }
            }

            cPlate NewPlate = new cPlate(cGlobalInfo.CurrentScreening.GetName(), MergedScreening);

            for (int X = 0; X < cGlobalInfo.CurrentScreening.Columns; X++)
                for (int Y = 0; Y < cGlobalInfo.CurrentScreening.Rows; Y++)
                {
                    cListSignature LDesc = new cListSignature();
                    for (int i = 0; i < cGlobalInfo.ListWellClasses.Count; i++)
                    {
                        cSignature Desc = new cSignature(Values[i][X][Y], ListDescType[i], cGlobalInfo.CurrentScreening);
                        LDesc.Add(Desc);

                    }
                    cWell NewWell = new cWell(LDesc, X + 1, Y + 1, MergedScreening, NewPlate);
                   // NewWell.SetCpdName("Well [" + (X + 1) + ":" + (Y + 1) + "]");
                    NewPlate.AddWell(NewWell);

                }

            // check if the plate exist already
            MergedScreening.AddPlate(NewPlate);
            MergedScreening.ListPlatesActive = new cListPlates();

            cGlobalInfo.WindowHCSAnalyzer.toolStripcomboBoxPlateList.Items.Clear();

            for (int i = 0; i < MergedScreening.ListPlatesAvailable.Count; i++)
            {
                MergedScreening.ListPlatesActive.Add(MergedScreening.ListPlatesAvailable[i]);
                cGlobalInfo.WindowHCSAnalyzer.toolStripcomboBoxPlateList.Items.Add(NewPlate.GetName());
            }

            cGlobalInfo.CurrentScreening.ListDescriptors = MergedScreening.ListDescriptors;
            cGlobalInfo.CurrentScreening.ListPlatesAvailable = MergedScreening.ListPlatesAvailable;
            cGlobalInfo.CurrentScreening.ListPlatesActive = MergedScreening.ListPlatesActive;

            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();
            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();

            cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx = 0;
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), true);

            ListPlates = new List<cPanelForDisplayArray>();
            for (int DescIdx = 0; DescIdx < cGlobalInfo.CurrentScreening.ListDescriptors.Count; DescIdx++)
            {
                if (cGlobalInfo.CurrentScreening.ListDescriptors[DescIdx].IsActive())
                    ListPlates.Add(new FormToDisplayDescriptorPlate(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate(), cGlobalInfo.CurrentScreening, DescIdx));
            }

            cWindowToDisplayEntireDescriptors WindowToDisplayDesc = new cWindowToDisplayEntireDescriptors(ListPlates, cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetName(), cGlobalInfo.ListWellClasses.Count);
            WindowToDisplayDesc.checkBoxGlobalNormalization.Checked = true;

            WindowToDisplayDesc.Show();
        }

        private void marginalManualClusteringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormForManualClustering FormWindow = new FormForManualClustering(this.GlobalInfo);
            FormWindow.checkBoxTransferToHitList.Checked = false;
            FormWindow.Show();
        }


        private void fTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PerformTestType(eTestType.F_TEST);
        }

        private void aNOVAToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            PerformTestType(eTestType.ANOVA);
        }

        private void samplesTTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PerformTestType(eTestType.TWO_SAMPLES_T_TEST);
        }

        private void studentTTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformTestType(eTestType.STUDENT_T_TEST);
        }

        private void wellsMergingToolsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cListWells ListWellsToProcess = new cListWells();

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                foreach (cWell TmpWell in TmpPlate.ListActiveWells)
                {

                    if (TmpWell.GetCurrentClassIdx() == -1) continue;
                    ListWellsToProcess.Add(TmpWell);
                }

            ListWellsToProcess.MergeWells();

        }

        private void dRCDesignerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.WindowForDRCDesign.IsDisposed) cGlobalInfo.WindowForDRCDesign = new FormForDRCDesign();
            cGlobalInfo.WindowForDRCDesign.Visible = true;
        }

        private void replicateScatterPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //cDisplayExtendedTable DET = new cDisplayExtendedTable();
            //DET.SetInputData(ET);
            //DET.DigitNumber = 0;
            //DET.Title = ET.Name;
            //DET.Run();

            //int NumberOfReplicate = 3;

            //cExtendedTable TableForReplicate = new cExtendedTable();

            //for (int i = 0; i < NumberOfReplicate; i++)
            //{
            //    TableForReplicate.Add(new cExtendedList("Replicate " + (i + 1)));
            //    TableForReplicate[i].ListTags = new List<object>();
            //}

            //TableForReplicate.ListTags = new List<object>();

            //cDescriptorType DescType = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor();
            //foreach (var item in ListMergedWells)
            //{
            //    if (item.Count < NumberOfReplicate) continue;

            //    for (int Rep = 0; Rep < NumberOfReplicate; Rep++)
            //    {
            //        double CurrentValue = item[Rep].GetAverageValue(DescType);
            //        TableForReplicate[Rep].Add(CurrentValue);
            //        TableForReplicate[Rep].ListTags.Add(item[Rep]);
            //    }
            //    TableForReplicate.ListTags.Add(item);
            //}



            //cViewer2DScatterPoint VS = new cViewer2DScatterPoint();
            //VS.SetInputData(TableForReplicate);
            //VS.Title = DescType.GetName() + " - " + TableForReplicate[0].Count + " points";
            //cFeedBackMessage FBM = VS.Run();

            //cDisplayToWindow DTW = new cDisplayToWindow();
            //DTW.SetInputData(VS.GetOutPut());
            //DTW.Title = "Replicate 2D scatter points graph";
            //FBM = DTW.Run();
            //DTW.Display();
        }

        private void toolStripDropDownButtonDisplayMode_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == System.Windows.Forms.MouseButtons.Right) && (cGlobalInfo.CurrentScreening != null))
            {
                ContextMenuStrip CompleteMenu = new ContextMenuStrip();

                ToolStripMenuItem ToolStripMenuItem_PropertyManager = new ToolStripMenuItem("Property Manager");
                CompleteMenu.Items.Add(ToolStripMenuItem_PropertyManager);
                ToolStripMenuItem_PropertyManager.Click += new System.EventHandler(this.ToolStripMenuItem_PropertyManager);

                ToolStripMenuItem ToolStripMenuItem_LoadProperty = new ToolStripMenuItem("Load Property");
                CompleteMenu.Items.Add(ToolStripMenuItem_LoadProperty);
                ToolStripMenuItem_LoadProperty.Click += new System.EventHandler(this.ToolStripMenuItem_LoadProperty);

                ToolStripMenuItem ToolStripMenuItem_SaveProperty = new ToolStripMenuItem("Save Property");
                CompleteMenu.Items.Add(ToolStripMenuItem_SaveProperty);
                ToolStripMenuItem_SaveProperty.Click += new System.EventHandler(this.ToolStripMenuItem_SaveProperty);

                CompleteMenu.Show(Control.MousePosition);
            }
        }

        private void ToolStripMenuItem_LoadProperty(object sender, EventArgs e)
        {
            LoadProperty();
        }

        private void ToolStripMenuItem_SaveProperty(object sender, EventArgs e)
        {
            SaveProperty();
        }

        private void ToolStripMenuItem_PropertyManager(object sender, EventArgs e)
        {
            FormForPropertiesManager PropManager = new FormForPropertiesManager();
            PropManager.Show();
        }

        private void fromImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormForGeneratingScreenFromImages FFGS = new FormForGeneratingScreenFromImages();
            if (FFGS.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
        }

        private void memoryTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var before = System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64;

            // performs operations here

            for (int Idx = 0; Idx < 100; Idx++)
            {
                cImage Im = new cImage(30000, 5000, 1, 1);

                for (int i = 0; i < Im.SliceSize; i++)
                {
                    Im.SingleChannelImage[0].Data[i] = i;
                }
                //  Im.Dispose();
                GC.Collect();
            }

            var after = System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64;
            var Mem = after - before;
        }

        private static OleDbConnection GetConnection()
        {
            OleDbConnection conn = new OleDbConnection();
            try
            {
                String connectionString = @"Provider=Microsoft.JET.OlEDB.4.0;"
               + @"Data Source=D:\Test\Cellinsight\MFGTMP-PC140320150001\MFGTMP-PC140320150001.MDB";
                conn = new OleDbConnection(connectionString);
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return conn;
        }


        private void mDBTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OleDbConnection MyConnection = GetConnection();

            DataSet ds = new DataSet();
            // OleDbConnection conn = GetConnection();
            OleDbDataAdapter da = new OleDbDataAdapter("Select * FROM asnFormFactor", MyConnection);
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            foreach (DataRow rows in dt.Rows)
            {
                int Numcol = int.Parse(rows["pCols"].ToString());
                int NumRow = int.Parse(rows["pRows"].ToString());
            }

            da = new OleDbDataAdapter("Select * FROM asnProtocolChannel", MyConnection);
            ds = new DataSet();
            da.Fill(ds);

            dt = ds.Tables[0];
            int NumChannels = dt.Rows.Count - 1;
            for (int i = 1; i <= NumChannels; i++)
            {
                string ChannelName = dt.Rows[i]["Dye"].ToString();
            }

            da = new OleDbDataAdapter("Select * FROM asnPlate", MyConnection);
            ds = new DataSet();
            da.Fill(ds);

            dt = ds.Tables[0];
            string PlateName = dt.Rows[0]["Name"].ToString();



            MyConnection.Close();

        }

        private void listViewForListWell_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection SLIVC = this.listViewForListWell.SelectedItems;

            if (SLIVC.Count == 1)
            {
                ListViewItem lvItem = SLIVC[0];

                if ((lvItem.Tag != null) && (lvItem.Tag.GetType() == typeof(cWell)))
                {
                    cWell TmpWell = ((cWell)(lvItem.Tag));
                    TmpWell.DisplayInfoWindow(cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor()));
                }

                //cGlobalInfo.CurrentScreening.SetClassStatus(lvItem.Index);
            }



        }


        private void listViewForListWell_MouseDown(object sender, MouseEventArgs e)
        {

            if (cGlobalInfo.CurrentScreening == null) return;
            if (e.Button != MouseButtons.Right) return;

            ContextMenuStrip NewMenu = new ContextMenuStrip();

            ToolStripMenuItem ToolStripMenuItem_ManualSelect = new ToolStripMenuItem("Manual Selection");
            ToolStripMenuItem_ManualSelect.Click += new System.EventHandler(this.ToolStripMenuItem_ManualSelect);
            NewMenu.Items.Add(ToolStripMenuItem_ManualSelect);


            if ((e.Button == MouseButtons.Right) && (cGlobalInfo.ListSelectedWell.Count > 0))
            {

                ToolStripMenuItem ToolStripMenuItem_Clear = new ToolStripMenuItem("Clear");
                ToolStripMenuItem_Clear.Click += new System.EventHandler(this.ToolStripMenuItem_Clear);
                NewMenu.Items.Add(ToolStripMenuItem_Clear);

                ToolStripMenuItem ToolStripMenuItem_Refresh = new ToolStripMenuItem("Refresh");
                ToolStripMenuItem_Refresh.Click += new System.EventHandler(this.ToolStripMenuItem_Refresh);
                NewMenu.Items.Add(ToolStripMenuItem_Refresh);

                NewMenu.Items.Add(new ToolStripSeparator());

                ToolStripMenuItem ToolStripMenuItem_ZFactor = new ToolStripMenuItem("Z'");
                ToolStripMenuItem_ZFactor.Click += new System.EventHandler(this.ToolStripMenuItem_ZFactor);
                NewMenu.Items.Add(ToolStripMenuItem_ZFactor);

                ToolStripMenuItem ToolStripMenuItem_CrossZFactor = new ToolStripMenuItem("Cross Classes Z'");
                ToolStripMenuItem_CrossZFactor.Click += new System.EventHandler(this.ToolStripMenuItem_CrossZFactor);
                NewMenu.Items.Add(ToolStripMenuItem_CrossZFactor);

                NewMenu.Items.Add(cGlobalInfo.ListSelectedWell.GetContextMenu());

                ListView.SelectedListViewItemCollection SLIVC = listViewForListWell.SelectedItems;

                if (SLIVC.Count == 1)
                {
                    cWell SelectedWell = (cWell)SLIVC[0].Tag;

                    if (SelectedWell != null)
                    {
                        foreach (var item in SelectedWell.GetExtendedContextMenu())
                            NewMenu.Items.Add(item);
                    }

                    NewMenu.Items.Add(new ToolStripSeparator());

                    ToolStripMenuItem _ToolStripMenuItem_RemoveFromList = new ToolStripMenuItem("Remove From List");
                    _ToolStripMenuItem_RemoveFromList.Tag = SLIVC[0];
                    _ToolStripMenuItem_RemoveFromList.Click += new System.EventHandler(this.ToolStripMenuItem_RemoveFromList);
                    NewMenu.Items.Add(_ToolStripMenuItem_RemoveFromList);

                }

            }

            NewMenu.Show(Control.MousePosition);
        }

        private void exportAsCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cGlobalInfo.CurrentScreening.GetListWells().ExportDBAsCSV("");
        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cFeedBackMessage MessageReturned;

            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            GUI_ListClasses.IsCheckBoxes = true;
            GUI_ListClasses.IsSelectAll = true;

            if (GUI_ListClasses.Run().IsSucceed == false) return;
            cExtendedList ListClassSelected = GUI_ListClasses.GetOutPut()[0];

            List<cWellClassType> ListWellClassesSelected = new List<cWellClassType>();
            foreach (var item in ListClassSelected.ListTags)
            {
                ListWellClassesSelected.Add((cWellClassType)(item));
            }

            cViewerGraph1D V1D = new cViewerGraph1D();
            V1D.Chart.IsSelectable = true;
            V1D.Chart.LabelAxisX = "Well Index";
            V1D.Chart.LabelAxisY = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();

            V1D.Chart.IsXGrid = true;



            if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
            {
                cExtendedTable DataFromPlate = new cExtendedTable(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListActiveWells,
                                                cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx, ListClassSelected);

                DataFromPlate.Name = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetName();

                V1D.Chart.IsShadow = true;
                V1D.Chart.IsBorder = true;
                //V1D.Chart.IsSelectable = true;
                V1D.Chart.CurrentTitle.Tag = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate();
                V1D.SetInputData(DataFromPlate);
                V1D.Run();

                cDesignerSplitter DS = new cDesignerSplitter();
                DS.Orientation = System.Windows.Forms.Orientation.Horizontal;
                DS.SetInputData(V1D.GetOutPut());

                cExtendedTable NewTable = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListActiveWells.Filter(ListWellClassesSelected).GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
                NewTable.Name = "Histogram";

                cViewerStackedHistogram CV1 = new cViewerStackedHistogram();
                CV1.SetInputData(NewTable);
                CV1.Chart.LabelAxisX = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
                CV1.Run();
                DS.SetInputData(CV1.GetOutPut());
                DS.Run();


                cDisplayToWindow Disp0 = new cDisplayToWindow();
                Disp0.SetInputData(DS.GetOutPut());
                Disp0.Title = "Scatter points graph - " + DataFromPlate[0].Count + " wells.";
                if (!Disp0.Run().IsSucceed) return;
                Disp0.Display();
            }
            else if (ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                V1D.Chart.MarkerSize = 5;
                V1D.Chart.IsBorder = false;
                V1D.Chart.IsShadow = false;

                cListWells ListWell = new cListWells(null);
                int IdxWellForPlateSeparator = 0;
                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                {
                    foreach (cWell TmpWell in TmpPlate.ListActiveWells)
                    {
                        int ClassTmp = TmpWell.GetCurrentClassIdx();
                        if ((ClassTmp == -1) || (GUI_ListClasses.GetOutPut()[0][TmpWell.GetCurrentClassIdx()] == 0)) continue;
                        ListWell.Add(TmpWell);
                        IdxWellForPlateSeparator++;
                    }


                    if (cGlobalInfo.OptionsWindow.FFAllOptions.checkBoxDisplayPlatesVerticalLines.Checked)
                    {
                        Classes.Base_Classes.General.cLineVerticalForGraph VL = new Classes.Base_Classes.General.cLineVerticalForGraph(IdxWellForPlateSeparator + 0.5);
                        VL.IsAllowMoving = false;
                        V1D.Chart.ListVerticalLines.Add(VL);
                    }

                }
                cExtendedTable DataFromPlate = new cExtendedTable(ListWell,
                                                cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx, ListClassSelected);

                DataFromPlate.Name = cGlobalInfo.CurrentScreening.GetName() + " - " + cGlobalInfo.CurrentScreening.ListPlatesActive.Count + " plates";

                int MaxNumberOfPts = (int)cGlobalInfo.OptionsWindow.FFAllOptions.numericUpDownMinNumPointForFastDisp.Value;
                if (ListWell.Count > MaxNumberOfPts)
                {
                    cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText("\n" + V1D.Title + ": Number of Wells is Higher than " + MaxNumberOfPts + " => Switching to FastPoints Mode.\n");
                    V1D.Chart.ISFastPoint = true;
                }

                V1D.SetInputData(DataFromPlate);
                V1D.Run();

                cDesignerSplitter DS = new cDesignerSplitter();
                DS.Orientation = System.Windows.Forms.Orientation.Horizontal;
                DS.SetInputData(V1D.GetOutPut());

                cExtendedTable NewTable = ListWell.GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
                NewTable.Name = "Histogram";

                cViewerStackedHistogram CV1 = new cViewerStackedHistogram();
                CV1.SetInputData(NewTable);
                CV1.Chart.LabelAxisX = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
                CV1.Run();
                DS.SetInputData(CV1.GetOutPut());
                DS.Run();

                cDisplayToWindow Disp0 = new cDisplayToWindow();
                Disp0.SetInputData(DS.GetOutPut());
                Disp0.Title = "Scatter points graph - " + DataFromPlate[0].Count + " wells.";
                if (!Disp0.Run().IsSucceed) return;
                Disp0.Display();

            }
            else if (ProcessModeplateByPlateToolStripMenuItem.Checked)
            {
                cDesignerTab CDT = new cDesignerTab();

                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                {
                    cExtendedTable DataFromPlate = new cExtendedTable(TmpPlate.ListActiveWells,
                                                cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx, ListClassSelected);

                    DataFromPlate.Name = TmpPlate.GetName();

                    V1D = new cViewerGraph1D();
                    V1D.Chart.IsSelectable = true;
                    V1D.Chart.LabelAxisX = "Well Index";
                    V1D.Chart.LabelAxisY = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
                    V1D.Chart.IsXGrid = true;
                    V1D.Chart.CurrentTitle.Tag = TmpPlate;
                    V1D.SetInputData(DataFromPlate);
                    V1D.Title = TmpPlate.GetName();
                    V1D.Run();

                    cDesignerSplitter DS = new cDesignerSplitter();
                    DS.Orientation = System.Windows.Forms.Orientation.Horizontal;
                    DS.SetInputData(V1D.GetOutPut());

                    cExtendedTable NewTable = TmpPlate.ListActiveWells.Filter(ListWellClassesSelected).GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
                    NewTable.Name = "Histogram";

                    cViewerStackedHistogram CV1 = new cViewerStackedHistogram();
                    CV1.SetInputData(NewTable);
                    CV1.Chart.LabelAxisX = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
                    CV1.Run();

                    DS.Title = TmpPlate.GetName();
                    DS.SetInputData(CV1.GetOutPut());
                    DS.Run();

                    CDT.SetInputData(DS.GetOutPut());
                }

                CDT.Run();

                cDisplayToWindow Disp0 = new cDisplayToWindow();
                Disp0.SetInputData(CDT.GetOutPut());
                Disp0.Title = "Scatter points graphs";
                if (!Disp0.Run().IsSucceed) return;
                Disp0.Display();
            }

        }

        #region XY scatter points
        private void dToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cFeedBackMessage MessageReturned;

            cViewer2DScatterPoint V1D = new cViewer2DScatterPoint();
            V1D.Chart.IsSelectable = true;
            //V1D.Chart.LabelAxisX = "Well Index";
            //V1D.Chart.LabelAxisY = CompleteScreening.ListDescriptors[CompleteScreening.ListDescriptors.CurrentSelectedDescriptor].GetName();
            //V1D.Chart.BackgroundColor = Color.LightYellow;
            //V1D.Chart.IsXAxis = true;


            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            GUI_ListClasses.IsCheckBoxes = true;
            GUI_ListClasses.IsSelectAll = true;

            if (GUI_ListClasses.Run().IsSucceed == false) return;
            cExtendedList ListClassSelected = GUI_ListClasses.GetOutPut()[0];

            if (ListClassSelected.Sum() < 1)
            {
                MessageBox.Show("At least one classe has to be selected.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //cDisplayToWindow CDW1 = new cDisplayToWindow();
            // cListWell ListWellsToProcess = new cListWell();

            if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
            {
                //cExtendedTable DataFromPlate = new cExtendedTable(CompleteScreening.GetCurrentDisplayPlate().ListActiveWells,
                //                                CompleteScreening.ListDescriptors.CurrentSelectedDescriptor);
                cListWells ListWellsToProcess = new cListWells(null);


                //foreach (cPlate TmpPlate in CompleteScreening.ListPlatesActive)
                foreach (cWell item in cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListActiveWells)
                    if (item.GetCurrentClassIdx() != -1)
                        if (ListClassSelected[item.GetCurrentClassIdx()] == 1) ListWellsToProcess.Add(item);

                cExtendedTable DataFromPlate = new cExtendedTable(ListWellsToProcess, true);
                DataFromPlate.Name = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetName();

                V1D.Chart.IsShadow = true;
                V1D.Chart.IsBorder = true;
                V1D.Chart.IsSelectable = true;
                V1D.Chart.CurrentTitle.Tag = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate();
                V1D.SetInputData(DataFromPlate);
                MessageReturned = V1D.Run();
                if (MessageReturned.IsSucceed == false)
                {
                    MessageBox.Show(MessageReturned.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cDesignerSinglePanel Designer0 = new cDesignerSinglePanel();
                Designer0.SetInputData(V1D.GetOutPut());
                Designer0.Run();

                cDisplayToWindow Disp0 = new cDisplayToWindow();
                Disp0.SetInputData(Designer0.GetOutPut());
                Disp0.Title = "2D Scatter points graph - " + DataFromPlate[0].Count + " wells.";
                if (!Disp0.Run().IsSucceed) return;
                Disp0.Display();
            }
            else if (ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                V1D.Chart.MarkerSize = 5;
                V1D.Chart.IsBorder = false;

                //List<cWell> ListWell = new List<cWell>();
                //foreach (cPlate TmpPlate in CompleteScreening.ListPlatesActive)
                //    foreach (cWell TmpWell in TmpPlate.ListActiveWells)
                //        ListWell.Add(TmpWell);

                //cExtendedTable DataFromPlate = new cExtendedTable(ListWell,
                //                                CompleteScreening.ListDescriptors.CurrentSelectedDescriptor);

                cListWells ListWellsToProcess = new cListWells(null);

                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                    foreach (cWell item in TmpPlate.ListActiveWells)
                        if (item.GetCurrentClassIdx() != -1)
                            if (ListClassSelected[item.GetCurrentClassIdx()] == 1) ListWellsToProcess.Add(item);

                cExtendedTable DataFromPlate = new cExtendedTable(ListWellsToProcess, true);

                DataFromPlate.Name = cGlobalInfo.CurrentScreening.GetName() + " - " + cGlobalInfo.CurrentScreening.ListPlatesActive.Count + " plates";

                V1D.SetInputData(DataFromPlate);
                V1D.Run();

                cDesignerSinglePanel Designer0 = new cDesignerSinglePanel();
                Designer0.SetInputData(V1D.GetOutPut());
                Designer0.Run();

                cDisplayToWindow Disp0 = new cDisplayToWindow();
                Disp0.SetInputData(Designer0.GetOutPut());
                Disp0.Title = "2D Scatter points graph - " + DataFromPlate[0].Count + " wells.";
                if (!Disp0.Run().IsSucceed) return;
                Disp0.Display();

               
                //dislin.metafl("xwin");

                
                //dislin.disini();
                
                

                //dislin.name("X-axis", "X");
                //dislin.name("Y-axis", "Y");
                

                
                //dislin.title();

                
                //dislin.axslen(1000, 1000);


                ////dislin.graf(0.0, 1.0, 0.0, 0.1, 0.0, 1.0, 0.0, 0.1);

                
                ////dislin.crvmat(func, n, n, 1, 1);

               
                //dislin.disfin();

            }
            else if (ProcessModeplateByPlateToolStripMenuItem.Checked)
            {
                cDesignerTab CDT = new cDesignerTab();

                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                {
                    //cExtendedTable DataFromPlate = new cExtendedTable(TmpPlate.ListActiveWells,
                    //                            CompleteScreening.ListDescriptors.CurrentSelectedDescriptor);

                    cListWells ListWellsToProcess = new cListWells(null);

                    foreach (cWell item in TmpPlate.ListActiveWells)
                        if (item.GetCurrentClassIdx() != -1)
                            if (ListClassSelected[item.GetCurrentClassIdx()] == 1) ListWellsToProcess.Add(item);

                    cExtendedTable DataFromPlate = new cExtendedTable(ListWellsToProcess, true);

                    DataFromPlate.Name = TmpPlate.GetName();

                    V1D = new cViewer2DScatterPoint();
                    V1D.Chart.IsSelectable = true;
                    V1D.Chart.LabelAxisX = "Well Index";
                    V1D.Chart.LabelAxisY = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
                    V1D.Chart.BackgroundColor = Color.LightYellow;
                    V1D.Chart.IsXGrid = true;
                    V1D.Chart.CurrentTitle.Tag = TmpPlate;
                    V1D.SetInputData(DataFromPlate);
                    V1D.Title = TmpPlate.GetName();
                    V1D.Run();

                    CDT.SetInputData(V1D.GetOutPut());
                }

                CDT.Run();

                cDisplayToWindow Disp0 = new cDisplayToWindow();
                Disp0.SetInputData(CDT.GetOutPut());
                Disp0.Title = "2D Scatter points graphs";
                if (!Disp0.Run().IsSucceed) return;
                Disp0.Display();

               
            }


            //if (CompleteScreening == null) return;

            //SimpleFormForXY FormToDisplayXY = new SimpleFormForXY(false);
            //FormToDisplayXY.CompleteScreening = CompleteScreening;

            //for (int i = 0; i < (int)CompleteScreening.ListDescriptors.Count; i++)
            //{
            //    FormToDisplayXY.comboBoxDescriptorX.Items.Add(CompleteScreening.ListDescriptors[i].GetName());
            //    FormToDisplayXY.comboBoxDescriptorY.Items.Add(CompleteScreening.ListDescriptors[i].GetName());
            //}

            //FormToDisplayXY.comboBoxDescriptorX.SelectedIndex = 0;
            //FormToDisplayXY.comboBoxDescriptorY.SelectedIndex = 0;


            //FormToDisplayXY.DisplayXY();
            //FormToDisplayXY.ShowDialog();

            //return;
        }


        #endregion

        private void dToolStripMenuItemScatterPlot3D_Click(object sender, EventArgs e)
        {
            int MaxNumberOfPts = (int)cGlobalInfo.OptionsWindow.FFAllOptions.numericUpDownMinNumPointForFastDisp.Value;

            if (cGlobalInfo.CurrentScreening.ListPlatesActive.GetListActiveWells().Count > MaxNumberOfPts)
            {
                cExtendedTable ET = cGlobalInfo.CurrentScreening.ListPlatesActive.GetListActiveWells().GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors(), false, false);
                cNormalize N = new cNormalize();
                N.SetInputData(ET);
                N.NormalizationType = eNormalizationType.MIN_MAX;
                N.Run();
                cExtendedTable NormTable = N.GetOutPut();

                cViewer3D V3D = new cViewer3D();
                c3DPointCloud _3DPtCloud = new c3DPointCloud(NormTable);
                _3DPtCloud.AutomatedPtColorMode = 1;
                _3DPtCloud.Create(new cPoint3D(0, 0, 0));
                _3DPtCloud.SetName("_3DPtCloud");
                cListGeometric3DObject GlobalList = new cListGeometric3DObject("3D Point Cloud MetaObject");

                GlobalList.Add(_3DPtCloud);

                c3DObject_Axis Axis = new c3DObject_Axis();
                cExtendedTable T = new cExtendedTable();
                T.Add(new cExtendedList(ET[0].Name));

                T[0].Tag = ET[0].Tag;
                T[0].Add(0);
                T[0].Add(1);
                T.Add(new cExtendedList(ET[1].Name));
                T[1].Tag = ET[1].Tag;
                T[1].Add(0);
                T[1].Add(1);

                if (ET.Count > 2)
                {
                    T.Add(new cExtendedList(ET[2].Name));
                    T[2].Tag = ET[2].Tag;
                    T[2].Add(0);
                    T[2].Add(1);
                }

                Axis.SetInputData(T);

                c3DNewWorld MyWorld = new c3DNewWorld(new cPoint3D(1, 1, 1), new cPoint3D(1, 1, 1));
                Axis.Run(MyWorld);

                GlobalList.AddRange(Axis.GetOutPut());

                // GlobalList.Add(Axis);

                foreach (var item in GlobalList)
                {
                    MyWorld.AddGeometric3DObject(item);
                }

                //  MyWorld.BackGroundColor = cGlobalInfo.OptionsWindow.FFAllOptions.panelFor3DBackColor.BackColor;

                V3D.SetInputData(MyWorld);
                V3D.Run();

                cDisplayToWindow DTW = new cDisplayToWindow();
                DTW.SetInputData(V3D.GetOutPut());
                DTW.Title = "3D Cloud Point - " + ET[0].Count + " points";
                DTW.Run();

                DTW.Display();
            }
            else
            {
                cGlobalInfo.OptionsWindow.checkBoxConnectDRCPts.Checked = false;
                FormFor3DDataDisplay FormToDisplayXYZ = new FormFor3DDataDisplay(ProcessModeEntireScreeningToolStripMenuItem.Checked, cGlobalInfo.CurrentScreening);
                for (int i = 0; i < (int)cGlobalInfo.CurrentScreening.ListDescriptors.Count; i++)
                {
                    FormToDisplayXYZ.comboBoxDescriptorX.Items.Add(cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName());
                    FormToDisplayXYZ.comboBoxDescriptorY.Items.Add(cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName());
                    FormToDisplayXYZ.comboBoxDescriptorZ.Items.Add(cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName());
                }
                FormToDisplayXYZ.Show();
                FormToDisplayXYZ.comboBoxDescriptorX.Text = cGlobalInfo.CurrentScreening.ListDescriptors[0].GetName() + " ";
                FormToDisplayXYZ.comboBoxDescriptorY.Text = cGlobalInfo.CurrentScreening.ListDescriptors[0].GetName() + " ";
                FormToDisplayXYZ.comboBoxDescriptorZ.Text = cGlobalInfo.CurrentScreening.ListDescriptors[0].GetName() + " ";
            }
            return;
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // System.Diagnostics.Process proc = new System.Diagnostics.Process();
            //  proc.StartInfo.FileName = "excel";
            //     proc.StartInfo.Arguments = "WhatsNew.docx";

            try
            {
                Process.Start("WhatsNew.docx");
            }
            catch (Exception)
            {
                MessageBox.Show("WhatsNew.docx\ncan not be loaded in MS Word.\n", "Open error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cViewer3D V3D = new cViewer3D();
            c3DNewWorld MyWorld = new c3DNewWorld(new cPoint3D(1, 1, 1), new cPoint3D(1, 1, 1));

            MyWorld.BackGroundColor = cGlobalInfo.OptionsWindow.FFAllOptions.panelFor3DBackColor.BackColor;

            V3D.SetInputData(MyWorld);
            V3D.Run();


            cDesignerSinglePanel CD = new cDesignerSinglePanel();
            CD.SetInputData(V3D.GetOutPut());
            if (CD.Run().IsSucceed == false) return;

            cDisplayToWindow CDW = new cDisplayToWindow();
            CDW.SetInputData(CD.GetOutPut());
            CDW.Title = "3D world";
            if (CDW.Run().IsSucceed == false) return;
            CDW.Display();
        }

        private void listViewClassHistory_KeyDown(object sender, KeyEventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            if (e.KeyCode == Keys.F5)
                cGlobalInfo.CurrentScreening.SaveCurrentClassStatus();
        }

        private void StatisticsToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            //for (int i = 0; i < StatisticsToolStripMenuItem.DropDownItems.Count; i++)
            //{
            //    if (StatisticsToolStripMenuItem.DropDownItems[i].Text == cGlobalInfo.CurrentScreening.GetExtendedContextMenu().Text)
            //        StatisticsToolStripMenuItem.DropDownItems.Remove(StatisticsToolStripMenuItem.DropDownItems[i]);

            //    if (StatisticsToolStripMenuItem.DropDownItems[i].Text == cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetExtendedContextMenu().Text)
            //        StatisticsToolStripMenuItem.DropDownItems.Remove(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetExtendedContextMenu());
            //}

            //StatisticsToolStripMenuItem.DropDownItems.Add(cGlobalInfo.CurrentScreening.GetExtendedContextMenu());
            //StatisticsToolStripMenuItem.DropDownItems.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetExtendedContextMenu());

        }

        private void importScreenDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg1 = new Ionic.Utils.FolderBrowserDialogEx();
            dlg1.Description = "Select the folder containing your files";
            dlg1.ShowNewFolderButton = true;
            dlg1.ShowEditBox = true;
            dlg1.ShowFullPathInEditBox = true;
            dlg1.RootFolder = System.Environment.SpecialFolder.Desktop;

            DialogResult result = dlg1.ShowDialog();
            if (result != DialogResult.OK) return;

            string Path = dlg1.SelectedPath;
            if (Directory.Exists(Path) == false) return;

            string[] ListFiles = null;

            try
            {
                ListFiles = Directory.GetFiles(Path, "*.csv", SearchOption.AllDirectories);
            }
            catch (System.Exception excep)
            {
                MessageBox.Show(excep.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ListFiles.Length == 0)
            {
                MessageBox.Show("No CSV files found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FormForPlateSelection FFP = new FormForPlateSelection(ListFiles,true);
            if (FFP.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            string[] ListFilesForPlates = FFP.GetListPlatesSelected();

            ImportFiles(ListFilesForPlates);


        }

        private void resetGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cGlobalInfo.CurrentScreening.ListGroups.Clear();

            cListWells ListWellActive = cGlobalInfo.CurrentScreening.ListPlatesActive.GetListActiveWells();


            foreach (cWell item in ListWellActive)
            {
                item.ListProperties.FindByName("Group").SetNewValue(null);
            }



            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
        }

        private void drawSingleDRCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int NumberOfReplicate = 4;
            int DRCNumberofPoints = 20;

            cExtendedTable FullResults = new cExtendedTable();
            FullResults.ListRowNames = new List<string>();



            cExtendedList ListPValues = new cExtendedList("DRC");


            cDataGeneratorTitration GT = new cDataGeneratorTitration();
            GT.NumberOfPoint = DRCNumberofPoints;
            GT.Start = 100;
            GT.DilutionFactor = 3;
            GT.Run();


            Random RND = new Random();

            cExtendedTable FinalTable = new cExtendedTable();

            cDataGeneratorSigmoid DGS = null;
            DGS = new cDataGeneratorSigmoid();
            DGS.SetInputData(GT.GetOutPut()[0]);
            DGS.EC50 = 0.01;
            DGS.Bottom = 10;
            DGS.Top = 110;
            DGS.Slope = 1;
            DGS.Run();

            // cAddNoise AN = new cAddNoise();
            // AN.DistributionType = eRandDistributionType.GAUSSIAN;

            double NoiseLevel = 10;
            //   EXT[0].Add(NoiseLevel);
            //   EXT[1].Add(DGS.EC50);

            #region loop over the replicates
            for (int NumReplic = 0; NumReplic < NumberOfReplicate; NumReplic++)
            {

                cExtendedList NewL = DGS.GetOutPut()[1];
                for (int k = 0; k < NewL.Count; k++)
                {
                    NewL[k] += RND.NextDouble() * NoiseLevel;
                }

                cExtendedTable TmpTable = new cExtendedTable();
                TmpTable.Add(GT.GetOutPut()[0]);
                TmpTable.Add(NewL);

                if (NumReplic >= 1)
                {
                    cMerge M = new cMerge();
                    M.IsHorizontal = false;
                    M.SetInputData(TmpTable, FinalTable);
                    M.Run();
                    FinalTable = M.GetOutPut();
                }
                else
                    FinalTable = new cExtendedTable(TmpTable);

            }
            #endregion


            cSigmoidFitting SF = new cSigmoidFitting();
            SF.SetInputData(FinalTable);
            SF.Run();

            cGlobalInfo.ConsoleWriteLine("Slope :" + DGS.Slope + " - Evaluated:" + SF.GetOutPut()[0][3]);
            cGlobalInfo.ConsoleWriteLine("Top :" + DGS.Top + " - Evaluated:" + SF.GetOutPut()[0][1]);
            cGlobalInfo.ConsoleWriteLine("Bottom :" + DGS.Bottom + " - Evaluated:" + SF.GetOutPut()[0][0]);
            cGlobalInfo.ConsoleWriteLine("EC50 :" + DGS.EC50 + " - Evaluated:" + SF.GetOutPut()[0][2]);


            // return;


            cCurveForGraph CFG = new cCurveForGraph();
            CFG.SetInputData(FinalTable);
            CFG.Run();

            cExtendedTable Sigmoid = SF.GetFittedRawValues(GT.GetOutPut()[0]);
            FinalTable[0] = Sigmoid[1];


            cViewerGraph1D VS1 = new cViewerGraph1D();
            VS1.SetInputData(new cExtendedTable(Sigmoid[1]));
            VS1.AddCurve(CFG);
            VS1.Chart.X_AxisValues = Sigmoid[0];//DGS.GetOutPut()[0];
            VS1.Chart.IsLogAxis = true;
            VS1.Chart.IsLine = true;
            VS1.Chart.IsShadow = true;
            VS1.Chart.Opacity = 210;
            VS1.Chart.LineWidth = 3;
            VS1.Chart.LabelAxisX = "Concentration";
            VS1.Chart.LabelAxisY = "Readout";
            VS1.Chart.XAxisFormatDigitNumber = -1;
            VS1.Chart.IsZoomableX = true;
            VS1.Chart.IsZoomableY = true;
            VS1.Run();

            cDesignerSinglePanel CD = new cDesignerSinglePanel();
            CD.SetInputData(VS1.GetOutPut());
            CD.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(CD.GetOutPut());//DS.GetOutPut());
            DTW.Run();
            DTW.Display();

        }

        private void spiralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // first let's define all the stimuli
            #region Build the stimuli
            cStimuli ListCellStimuli = new cStimuli();

            cStimulus_Physical Gravity = new cStimulus_Physical("Gravity");
            ListCellStimuli.Add(Gravity);
            //cStimulus_Physical WorldEdges = new cStimulus_Physical("World Edges");
            //ListCellStimuli.Add(WorldEdges);
            cStimulus_Physical BrownianMotion = new cStimulus_Physical("Brownian Motion");
            ListCellStimuli.Add(BrownianMotion);


            cStimulus_Chemical ToxicCompound1 = new cStimulus_Chemical("ToxicCompound1");
            ListCellStimuli.Add(ToxicCompound1);
            #endregion


            #region build the cell agents
            #endregion

            cNewAgent BiologicalExperiment_DRC = new cNewAgent(new cPoint3D(0, 0, 0), new cPoint3D(5, 5, 5), "DRC");

            double Concentration = 0.01;
            for (int i = 0; i < 2; i++)
            {
                double WellSpacing = 1;
                cNewAgent BiologicalExperiment_Well = new cNewAgent(new cPoint3D(WellSpacing, 0, 0), new cPoint3D(1, 1, 1), "Well " + i);

                // add a new Property to the well
                cInternalProperty Cpd1Concentration = new cInternalProperty();
                Cpd1Concentration.Name = "Compound 1 Concentration";
                BiologicalExperiment_Well.AddProperty(Cpd1Concentration);

                // add the cellular agents in the well
                int RegularCellsNumber = 20;
                for (int CellIdx = 0; CellIdx < RegularCellsNumber; CellIdx++)
                {
                    cAgent_Cell Agent_RegularCell = new cAgent_Cell(new cPoint3D(0.02 * CellIdx, 0.2, 0.0200 * i), new cPoint3D(0.0100, 0.0100, 0.0100), "Regular Cell " + CellIdx);
                    Agent_RegularCell.AssociatedStimuli = ListCellStimuli;
                    BiologicalExperiment_Well.AddNewAgent(Agent_RegularCell);
                }

                int CancerCellsNumber = 30;
                for (int CellIdx = 0; CellIdx < CancerCellsNumber; CellIdx++)
                {
                    cAgent_Cell Agent_CancerCell = new cAgent_Cell(new cPoint3D(0.0100 * i, 0.0100 * CellIdx, 0.1000), new cPoint3D(0.0200, 0.0200, 0.0200), "Cancer Cell " + CellIdx);
                    Agent_CancerCell.AssociatedStimuli = ListCellStimuli;
                    BiologicalExperiment_Well.AddNewAgent(Agent_CancerCell);
                }


                // add the well agent in the DRC
                BiologicalExperiment_DRC.AddNewAgent(BiologicalExperiment_Well);

                Concentration *= 2;
            }

            for (int Iteration = 0; Iteration < 1; Iteration++)
            {
                BiologicalExperiment_DRC.Run();
            }

            // display objects in 3D
            cViewer3D V3D = new cViewer3D();
            // c3DNewWorld MyWorld = new c3DNewWorld((cPoint3D)BiologicalExperiment_DRC.InternalProperties["Volume"], new cPoint3D(1, 1, 1));
            c3DNewWorld MyWorld = new c3DNewWorld(new cPoint3D(1, 1, 1), new cPoint3D(1, 1, 1));
            V3D.SetInputData(MyWorld);
            V3D.Run();

            cDesignerSinglePanel CD = new cDesignerSinglePanel();
            CD.SetInputData(V3D.GetOutPut());
            if (CD.Run().IsSucceed == false) return;

            cListGeometric3DObject GlobalList = new cListGeometric3DObject("Global List");
            cListGeometric3DObject CellsList = new cListGeometric3DObject("Cells");

            cInternalProperty TmpProp;

            foreach (cNewAgent TmpWell in BiologicalExperiment_DRC)
            {

                cPoint3D ObjectPos = TmpWell.GetAbsoluteGetPosition();

                c3DCube SpaceCube = new c3DCube();
                TmpProp = TmpWell.InternalProperties["Volume"];
                cPoint3D VOlume = new cPoint3D(0, 0, 0);
                SpaceCube.Create(ObjectPos, VOlume, Color.Blue);
                SpaceCube.SetOpacity(0.2);
                GlobalList.AddObject(SpaceCube);

                foreach (cNewAgent TmpCell in TmpWell)
                {
                    ObjectPos = TmpCell.GetAbsoluteGetPosition();
                    c3DSphere _3DSphere = new c3DSphere(ObjectPos, 0.05, Color.Red);
                    TmpProp = TmpCell.InternalProperties["Volume"];
                    VOlume = new cPoint3D(0, 0, 0);// (cPoint3D)TmpCell.InternalProperties["Volume"];
                    //_3DSphere.(ObjectPos, 0.5, Color.Red);
                    _3DSphere.SetName(TmpCell.GetName());
                    _3DSphere.SetOpacity(1);

                    CellsList.AddObject(_3DSphere);
                }
            }


            GlobalList.AddRange(CellsList);

            foreach (var item in GlobalList)
                MyWorld.AddGeometric3DObject(item);

            cDisplayToWindow CDW = new cDisplayToWindow();
            CDW.SetInputData(CD.GetOutPut());
            CDW.Title = "3D world";
            if (CDW.Run().IsSucceed == false) return;
            CDW.Display();
            return;
        }

        private void simpleTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random RNd = new Random();

            int NumRow = 8;
            double InitialValue = 10;
            int NumCol = 12;


            cExtendedTable FinalTable = null;
            for (int Col = 1; Col <= NumCol; Col++)
            {


                for (int Repeat = 1; Repeat <= NumRow; Repeat++)
                {
                    cExtendedTable GeneratedTable = new cExtendedTable(1, 1000, InitialValue);

                    cAddNoise AN = new cAddNoise();
                    AN.DistributionType = eRandDistributionType.GAUSSIAN;
                    AN.Mean = 20;// Col*1.2;
                    AN.Stdv = 0.5 * Col;
                    AN.SetInputData(GeneratedTable);
                    AN.Run();
                    GeneratedTable = AN.GetOutPut();



                    //for (int i = 0; i < GeneratedTable[0].Count; i++)
                    //{
                    //    GeneratedTable[0][i] = Col;
                    //    GeneratedTable[1][i] = Repeat;
                    //}

                    GeneratedTable.ListRowNames = new List<string>();
                    for (int i = 0; i < GeneratedTable[0].Count; i++)
                    {
                        GeneratedTable.ListRowNames.Add(ConvertPosition(Col, Repeat));
                    }

                    if (FinalTable == null)
                        FinalTable = new cExtendedTable(GeneratedTable);
                    else
                    {
                        cMerge M = new cMerge();
                        M.IsHorizontal = false;
                        M.SetInputData(FinalTable, GeneratedTable);
                        M.Run();
                        FinalTable = M.GetOutPut();
                    }

                }


            }

            FinalTable.Name = "generated screen";
            FinalTable[0].Name = "Volume";
            //  FinalTable[1].Name = "Row";
            //  FinalTable[2].Name = "Volume";
            cDisplayExtendedTable DET = new cDisplayExtendedTable();
            //DET.SetInputData(FinalTable);
            //DET.Run();

            cTableToFile TTF = new cTableToFile();
            TTF.SetInputData(FinalTable);
            TTF.IsDisplayUIForFilePath = true;
            TTF.IsRunEXCEL = true;
            TTF.IsAppend = false;
            TTF.Run();



        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {

            cDisplayNewImage DNI = new cDisplayNewImage();
            DNI.ListProperties.FindByName("Width").SetNewValue((int)256);
            DNI.ListProperties.FindByName("Width").IsGUIforValue = true;
            DNI.ListProperties.FindByName("Height").SetNewValue((int)256);
            DNI.ListProperties.FindByName("Height").IsGUIforValue = true;
            DNI.ListProperties.FindByName("Depth").SetNewValue((int)1);
            DNI.ListProperties.FindByName("Depth").IsGUIforValue = true;
            DNI.Run();

        }

        private void HCSAnalyzer_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.V) && e.Control)
            {
                PasteTableFromClipboard();
            }
        }

        void PasteTableFromClipboard()
        {
            //Clipboard.ContainsData();
            string Values = Clipboard.GetText();
            if (Values.Length > 0)
            {

                string[] Sep = new string[1];
                Sep[0] = "\r\n";

                string[] ListValues = Values.Split(Sep, StringSplitOptions.None);
                if (ListValues.Length == 0) return;

                // let's compute the table width
                Sep[0] = "\t";
                string[] subListValues = ListValues[0].Split(Sep, StringSplitOptions.None);
                if (subListValues.Length == 0) return;


                cExtendedTable ET = new cExtendedTable(subListValues.Length, ListValues.Length - 1, 0);
                ET.ListRowNames = new List<string>();
                ET.Name = "From Clipboard [" + subListValues.Length.ToString() + "x" + (ListValues.Length - 1).ToString() + "]";

                if (subListValues.Length * (ListValues.Length - 1) > 10000)
                {
                    if (MessageBox.Show("The table will contain " + (subListValues.Length * (ListValues.Length - 1)) + " elements.\nDo you want proceed?", "Warning !", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.No) return;
                }

                for (int j = 0; j < subListValues.Length; j++)
                    ET[j].Name = j.ToString();


                for (int i = 0; i < ListValues.Length - 1; i++)
                {
                    ET.ListRowNames.Add(i.ToString());
                    subListValues = ListValues[i].Split(Sep, StringSplitOptions.None);
                    for (int j = 0; j < subListValues.Length; j++)
                    {
                        double Res;
                        bool IsSuccess = double.TryParse(subListValues[j], out Res);
                        if (IsSuccess)
                            ET[j][i] = Res;
                        else
                            ET[j][i] = double.NaN;
                    }
                }

                cDisplayExtendedTable DET = new cDisplayExtendedTable();
                DET.SetInputData(ET);
                DET.Run();
            }

        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteTableFromClipboard();
        }

        private void basic3DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cViewer3D V3D = new cViewer3D();
            c3DNewWorld MyWorld = new c3DNewWorld(new cPoint3D(1, 1, 1), new cPoint3D(1, 1, 1));

            c3DTexturedPlan Plan = new c3DTexturedPlan(new cPoint3D(0, 0, 0), new cImage(256, 256, 1, 1));
            Plan.Run();
            MyWorld.AddGeometric3DObject(Plan);

            V3D.SetInputData(MyWorld);
            V3D.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(V3D.GetOutPut());
            DTW.Run();
            DTW.Display();
        }

        private void tabControlMainView_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshViews(tabControlMainView);
        }


        public void RefreshViews(object sender)
        {

            if (cGlobalInfo.CurrentScreening == null) return;

            if (tabControlMainView.SelectedTab.Text == "DataView")
            {
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().RefreshTableView();
            }
            else if (tabControlMainView.SelectedTab.Text == "2D Scatter")
            {
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().Refresh2DScatter();
            }
            else if (tabControlMainView.SelectedTab.Text == "1D Scatter")
            {
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().Refresh1DScatter();
            }       
            else if ((tabControlMainView.SelectedTab.Text == "Screening View"))//||((sender!=null)&&(sender.GetType().Name=="Panel")))
            {
                if(sender==null) return;

                //if (sender.GetType().Name == "Panel")
                //{
                //    string SenderName = ((Panel)sender).Name;
                //    if (SenderName == "panelForPlate")
                //    {
                //        cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().Refresh3DScreenView();
                //    }
                //}
                if (sender.GetType().Name == "ComboBox")
                {
                    string SenderName = ((ComboBox)sender).Name;
                    if (SenderName == "comboBoxDescriptorToDisplay") return;
                }
                else if (sender.GetType().Name == "ToolStripComboBox")
                {
                    string SenderName = ((ToolStripComboBox)sender).Name;
                    if (SenderName == "toolStripcomboBoxPlateList") return;
                }
                else if(sender.GetType().Name == "CheckedListBox")
                {
                    string SenderName = ((CheckedListBox)sender).Name;
                    if (SenderName == "checkedListBoxActiveDescriptors") return;
                }

                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().Refresh3DScreenView();
            }
        }


        private void comboBoxQC_SelectedIndexChanged(object sender, EventArgs e)
        {
            cGlobalInfo.WindowHCSAnalyzer.UpdateQCDisplay();
        }

        private void defineClassesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cGUI_2ClassesSelection GUI_ListClasses = new cGUI_2ClassesSelection();
            GUI_ListClasses.ClassType = eClassType.WELL;

            if (GUI_ListClasses.Run(this.GlobalInfo).IsSucceed == false) return;
            cExtendedTable ListClassSelected = GUI_ListClasses.GetOutPut();


            for (int i = 0; i < ListClassSelected[0].Count; i++)
                if (ListClassSelected[0][i] > 0) cGlobalInfo.CtrlNeg = cGlobalInfo.ListWellClasses[i];
            for (int i = 0; i < ListClassSelected[1].Count; i++)
                if (ListClassSelected[1][i] > 0) cGlobalInfo.CtrlPos = cGlobalInfo.ListWellClasses[i];

            cGlobalInfo.WindowHCSAnalyzer.UpdateQCDisplay();
        }

        private void visualizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(cGlobalInfo.CurrentScreening==null) return;

            singleCellToolStripMenuItem.Enabled = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor().IsConnectedToDatabase;
        }

        private void cSVDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog CurrOpenFileDialog = new OpenFileDialog();
            CurrOpenFileDialog.Filter = "Csv Files (*.csv)|*.csv |All Files (*.*)|*.*";//|db files (*.db)|*.db|nc files (*.nc)|*.nc
            CurrOpenFileDialog.Multiselect = false;

            DialogResult Res = CurrOpenFileDialog.ShowDialog();
            if (Res != DialogResult.OK) return;

            FormInfoForFileImporter CSVFeedBackWindow = new FormInfoForFileImporter(CurrOpenFileDialog.FileNames[0]);
            CSVFeedBackWindow.groupBoxPositionMode.Visible = false;
            if (CSVFeedBackWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;


            // if (CSVFeedBackWindow == null) return;
            // if (CSVFeedBackWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            cConvertCSVtoDB CCTODB = CSVtoDB(CurrOpenFileDialog.FileNames[0], CSVFeedBackWindow.GetDelimiter(),"");
            if (CCTODB == null) return;


            //  LoadDB(CCTODB.SelectedPath);
        }

        private void harmonyDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HarmonyToDB("");
        }


        private void HarmonyToDB(string DefaultPath)
        {
            #region Get the directory
            if (!Directory.Exists(DefaultPath)) DefaultPath = "";

            var dlg1 = new Ionic.Utils.FolderBrowserDialogEx();
            dlg1.Description = "Select the folder of your experiment.";
            dlg1.ShowNewFolderButton = true;
            dlg1.ShowEditBox = true;
            if (DefaultPath != "")
                dlg1.SelectedPath = DefaultPath;
            //dlg1.NewStyle = false;
            //dlg1.SelectedPath = txtExtractDirectory.Text;
            dlg1.ShowFullPathInEditBox = true;
            dlg1.RootFolder = System.Environment.SpecialFolder.Desktop;

            // Show the FolderBrowserDialog.
            DialogResult result = dlg1.ShowDialog();
            if (result != DialogResult.OK) return;

            string Path = dlg1.SelectedPath;

            if (Directory.Exists(Path) == false) return;
            #endregion

            #region Get the different files

            string[] ListFilesForPlates = null;
            try
            {
                ListFilesForPlates = Directory.GetFiles(Path, "Objects_Population - *.txt", SearchOption.AllDirectories);
            }
            catch (System.Exception excep)
            {
                MessageBox.Show(excep.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (ListFilesForPlates.Length == 0)
            {
                MessageBox.Show("The selected directory do not contain any Objects_Population files !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string[] Sep = new string[1];
            Sep[0] = "\\";

            Dictionary<string, string> CurrentPlateDico = new Dictionary<string, string>();
          //  string[] FirstListImages = Directory.GetFiles(PlateDirectories[i], TmpPlateName + "_*.C01", SearchOption.AllDirectories);

            foreach (var item in ListFilesForPlates)
            {
                string[] Res = item.Split(Sep, StringSplitOptions.RemoveEmptyEntries);
                string CurrentName = Res[Res.Length-1];

                if (CurrentPlateDico.ContainsKey(CurrentName.Remove(CurrentName.Length - 4))) continue;

                CurrentPlateDico.Add(CurrentName.Remove(CurrentName.Length-4), item);

            }

            string[] ListTypes = CurrentPlateDico.Keys.ToArray();

            // plates selection GUI
            FormForPlateSelection FFP = new FormForPlateSelection(ListTypes,false);
            FFP.Text = "Object Types Selection";

            if (FFP.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            ListFilesForPlates = FFP.GetListPlatesSelected();

            #endregion

            if (ListFilesForPlates.Length == 0) return;

            ListFilesForPlates = Directory.GetFiles(Path, ListFilesForPlates[0] + ".txt", SearchOption.AllDirectories);

            #region And now let's analyse the files adn create the CSV

            CsvRow CurrentRow = new CsvRow();

            bool IsHeaderWritten = false;
            string OriginalHeader = "";

            Sep[0] = "\\";
            string[] TmpSplit = ListFilesForPlates[0].Split(Sep, StringSplitOptions.RemoveEmptyEntries);

           //TmpSplit[TmpSplit.Length-1].Replace(".txt",".csv");
            string TPath = Path + "\\" + TmpSplit[TmpSplit.Length - 1].Replace(".txt", ".csv");
                
            Sep[0] = ",";  // specifically for the bounding box processing

            FormForProgress MyProgressBar = new FormForProgress();

            MyProgressBar.progressBar.Maximum = ListFilesForPlates.Length;
            MyProgressBar.Show();


            for (int i = 0; i < ListFilesForPlates.Length ; i++)
            {
                MyProgressBar.richTextBoxForComment.AppendText(ListFilesForPlates[i]);
                MyProgressBar.richTextBoxForComment.Update();

                StreamWriter stream = new StreamWriter(TPath, true, Encoding.ASCII);
                
                #region process the header
                string CurrentFile = ListFilesForPlates[i];
                CsvFileReader CSVReader = new CsvFileReader(CurrentFile);
                CSVReader.Separator = '\t';
                
                CSVReader.ReadRow(CurrentRow);
                // let's take care of the header first
                while (CurrentRow[0]!="Plate Name")
                {
                    CSVReader.ReadRow(CurrentRow);
                }

                string PlateName = CurrentRow[1];

                // skip the rest of the header
                while (CurrentRow[0] != "[Data]")
                {
                    CSVReader.ReadRow(CurrentRow);
                }

                // read the columns names
                CSVReader.ReadRow(CurrentRow);
                List<string> Descs = CurrentRow;

                string TobeWritten = "Plate Name,";
                int IdxBoundingBox = -1;
                int IndexCol = -1;
                int IndexRow = -1;
                int IndexCompound = -1;
                int IndexConcentration = -1;
                int IndexCellcount = -1;
                
                int NumDesc = Descs.Count;

                for (int j = 0; j < Descs.Count; j++)
			    {
                    if (Descs[j] == "Bounding Box")
                    {
                        TobeWritten += "X_Min,Y_Min,X_Max,Y_Max,";
                        IdxBoundingBox = j;
                       // NumDesc += 3; 
                    }
                    else if (Descs[j] == "Row")
                    {
                        IndexRow = j;
                        TobeWritten += "Well Position,";
                    }
                    else if (Descs[j] == "Column")
                    {
                        IndexCol = j;
                    }
                    else if (Descs[j] == "Compound")
                    {
                        // skipped
                        IndexCompound = j;
                    }
                    else if (Descs[j] == "Concentration")
                    {
                        // skipped
                        IndexConcentration = j;
                    }
                    else if (Descs[j] == "Cell Count")
                    {
                        // skipped
                        IndexCellcount = j;
                    }
                    else
                        TobeWritten += Descs[j] + ",";
			 
			    }

                TobeWritten = TobeWritten.Remove(TobeWritten.Length - 1);

                if (IsHeaderWritten == false)
                {
                    OriginalHeader = TobeWritten;
                    stream.WriteLine(TobeWritten);
                    IsHeaderWritten = true;
                }
                else
                {
                    // inconsistency between the headers... skip the plate
                    if (TobeWritten != OriginalHeader)
                    {
                        continue;
                    }
                }

                #endregion

                // now let's process the data
                int IdxRow = 0;
                while (!CSVReader.EndOfStream)
                {
                    CSVReader.ReadRow(CurrentRow);
                    TobeWritten = PlateName+",";
                    for (int j = 0; j < Descs.Count; j++)
                    {
                        if ((IdxBoundingBox > -1) && (j == IdxBoundingBox))
                        {
                            // let's process the bounding box
                            string BB = CurrentRow[j];
                            BB = BB.Remove(BB.Length - 1);
                            BB = BB.Remove(0, 1);

                            string[] Splitted = BB.Split(Sep, StringSplitOptions.None);
                            TobeWritten += Splitted[0] + "," + Splitted[1] + "," + Splitted[2] + "," + Splitted[3] + ",";
                            // j += 3;
                        }
                        else if (j == IndexRow)
                        {
                            TobeWritten += ConvertPosition(int.Parse(CurrentRow[IndexCol]), int.Parse(CurrentRow[IndexRow]))+",";
                        }
                        else if ((j == IndexCol)||(j==IndexCellcount)||(j==IndexCompound)||(j==IndexConcentration))
                        {
                            // do nothing
                        }
                        else
                        {
                            if (CurrentRow[j] != "")
                                TobeWritten += CurrentRow[j] + ",";
                            else
                                TobeWritten += "NaN,";
                        }

                    }
                    TobeWritten = TobeWritten.Remove(TobeWritten.Length - 1);
                    stream.WriteLine(TobeWritten );
                    IdxRow++;
                }

                CSVReader.Close();
                stream.Dispose();
                MyProgressBar.progressBar.Value++;
                MyProgressBar.progressBar.Update();
            }
            MyProgressBar.Close();
            #endregion

            #region let's build the database now

            // if (CSVFeedBackWindow == null) return;
            // if (CSVFeedBackWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            string DBPath = Path + "\\DB";
            Directory.CreateDirectory(DBPath);
            cConvertCSVtoDB CCTODB = CSVtoDB(TPath, ",", DBPath);
            if (CCTODB == null)
            {
                return;
            }
            else
            {
                // update image accessor
                cGlobalInfo.OptionsWindow.radioButtonImageAccessDefined.Checked = false;
                cGlobalInfo.OptionsWindow.radioButtonImageAccessHarmony35.Checked = true;
                cGlobalInfo.OptionsWindow.textBoxImageAccesImagePath.Text = Path;

                cGlobalInfoToBeExported GlobalInfoToBeExported = new cGlobalInfoToBeExported();
                cGlobalInfo.OptionsWindow.TmpOptionPath = GlobalInfoToBeExported.Save(DBPath+"\\Options.opt");

                //cGlobalInfo.OptionsWindow.sav
            }

            #endregion
        }


 

        //void UpDateColorSampleImage()
        //{
        //    Bitmap bmp = new Bitmap(pictureBoxForColorSample.Width, pictureBoxForColorSample.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

        //    ColorPalette ncp = bmp.Palette;
        //    for (int i = 0; i < 256; i++)
        //    {
        //        int RealIdx = (int)((i * this.SelectedLUT[0].Length) / 256);
        //        ncp.Entries[i] = Color.FromArgb(255, this.SelectedLUT[0][RealIdx], this.SelectedLUT[1][RealIdx], this.SelectedLUT[2][RealIdx]);
        //    }
        //    bmp.Palette = ncp;

        //    var BoundsRect = new Rectangle(0, 0, pictureBoxForColorSample.Width, pictureBoxForColorSample.Height);
        //    BitmapData bmpData = bmp.LockBits(BoundsRect, ImageLockMode.WriteOnly, bmp.PixelFormat);

        //    IntPtr ptr = bmpData.Scan0;

        //    int bytes = bmpData.Stride * bmp.Height;
        //    var rgbValues = new byte[bytes];

        //    // fill in rgbValues, e.g. with a for loop over an input array
        //    for (int j = 0; j < pictureBoxForColorSample.Height; j++)
        //        for (int i = 0; i < pictureBoxForColorSample.Width; i++)
        //        {
        //            rgbValues[i + j * bmpData.Stride] = (byte)((i * 256) / (pictureBoxForColorSample.Width));
        //        }

        //    Marshal.Copy(rgbValues, 0, ptr, bytes);
        //    bmp.UnlockBits(bmpData);

        //    pictureBoxForColorSample.Image = (Image)bmp;
        //}


    }

    /// <summary>
    /// If you want keep your version information,
    /// Put your version information in the AssemblyInfo.cs file
    /// [assembly: AssemblyVersion("1.0.*")]
    /// [assembly: AssemblyFileVersion("1.0.0.0")]
    /// </summary>
    public static class PluginVersion
    {
        public static string Info
        {
            get
            {
                System.Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                bool isDaylightSavingsTime = TimeZone.CurrentTimeZone.IsDaylightSavingTime(DateTime.Now);
                DateTime MyTime = new DateTime(2000, 1, 1).AddDays(v.Build).AddSeconds(v.Revision * 2).AddHours(isDaylightSavingsTime ? 1 : 0);

                return string.Format("Version:{0}.{1} - Compiled:{2:s}", v.Major, v.MajorRevision, MyTime);
            }
        }
    }
}