using System.Windows.Forms;
namespace HCSAnalyzer
{
    partial class HCSAnalyzer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Classification Tree");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Classification", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Correlation Matrix and Ranking");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Systematic Errors Table");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Z-Factors");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Quality Control", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Pathway Analysis");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("siRNA screening", new System.Windows.Forms.TreeNode[] {
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Weka .Arff File");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Misc", new System.Windows.Forms.TreeNode[] {
            treeNode9});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HCSAnalyzer));
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageDImRed = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownNewDimension = new System.Windows.Forms.NumericUpDown();
            this.radioButtonDimRedSupervised = new System.Windows.Forms.RadioButton();
            this.radioButtonDimRedUnsupervised = new System.Windows.Forms.RadioButton();
            this.buttonReduceDim = new System.Windows.Forms.Button();
            this.groupBoxUnsupervised = new System.Windows.Forms.GroupBox();
            this.richTextBoxUnsupervisedDimRec = new System.Windows.Forms.RichTextBox();
            this.comboBoxReduceDimSingleClass = new System.Windows.Forms.ComboBox();
            this.groupBoxSupervised = new System.Windows.Forms.GroupBox();
            this.comboBoxDimReductionNeutralClass = new System.Windows.Forms.ComboBox();
            this.richTextBoxSupervisedDimRec = new System.Windows.Forms.RichTextBox();
            this.comboBoxReduceDimMultiClass = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPageQualityQtrl = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonRejectPlates = new System.Windows.Forms.Button();
            this.comboBoxRejectionPositiveCtrl = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxRejectionNegativeCtrl = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.numericUpDownRejectionThreshold = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.richTextBoxInformationRejection = new System.Windows.Forms.RichTextBox();
            this.comboBoxRejection = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBoxInformationForPlateCorrection = new System.Windows.Forms.RichTextBox();
            this.comboBoxMethodForCorrection = new System.Windows.Forms.ComboBox();
            this.buttonCorrectionPlateByPlate = new System.Windows.Forms.Button();
            this.tabPageNormalization = new System.Windows.Forms.TabPage();
            this.buttonNormalize = new System.Windows.Forms.Button();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.comboBoxNormalizationPositiveCtrl = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxNormalizationNegativeCtrl = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBoxInfoForNormalization = new System.Windows.Forms.RichTextBox();
            this.comboBoxMethodForNormalization = new System.Windows.Forms.ComboBox();
            this.tabPageClassification = new System.Windows.Forms.TabPage();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.ButtonClustering = new System.Windows.Forms.Button();
            this.richTextBoxInfoClustering = new System.Windows.Forms.RichTextBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.panelTMPForFeedBack = new System.Windows.Forms.Panel();
            this.buttonNewClassificationProcess = new System.Windows.Forms.Button();
            this.comboBoxNeutralClassForClassif = new System.Windows.Forms.ComboBox();
            this.buttonStartClassification = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.richTextBoxInfoClassif = new System.Windows.Forms.RichTextBox();
            this.comboBoxCLassificationMethod = new System.Windows.Forms.ComboBox();
            this.tabPageExport = new System.Windows.Forms.TabPage();
            this.treeViewSelectionForExport = new System.Windows.Forms.TreeView();
            this.checkBoxExportFullScreen = new System.Windows.Forms.CheckBox();
            this.checkBoxExportPlateFormat = new System.Windows.Forms.CheckBox();
            this.buttonExport = new System.Windows.Forms.Button();
            this.richTextBoxForScreeningInformation = new System.Windows.Forms.RichTextBox();
            this.imageListForTab = new System.Windows.Forms.ImageList(this.components);
            this.panelForTools = new System.Windows.Forms.Panel();
            this.groupBoxQC = new System.Windows.Forms.GroupBox();
            this.contextMenuStripForQC = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.defineClassesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxQC = new System.Windows.Forms.ComboBox();
            this.labelQC = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBoxClass = new System.Windows.Forms.ComboBox();
            this.labelNumClasses = new System.Windows.Forms.Label();
            this.panelForPlate = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.menuStripFile = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importScreenDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cellByCellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.buildDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.harmonyDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.exportAsCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateScreenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.univariateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multivariateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singleCellsSimulatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.fromImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveScreentoCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentPlateTomtrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toARFFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appendDescriptorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLoadProperty = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemLoadImage = new System.Windows.Forms.ToolStripMenuItem();
            this.loadExtendedTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAverageValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAverageValuesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.swapClassesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.applySelectionToScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemDescManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorPaste = new System.Windows.Forms.ToolStripSeparator();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.platesManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.plateViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descriptorViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.classViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.averageViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pieViewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.ThreeDVisualizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripcomboBoxPlateList = new System.Windows.Forms.ToolStripComboBox();
            this.visualizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stackedHistogramsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.distanceMatrixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hierarchicalTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singleCellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scatterPlotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OneDScatterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItemScatterPlot2D = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItemScatterPlot3D = new System.Windows.Forms.ToolStripMenuItem();
            this.StatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qualityControlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zScoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mannWithneyTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aNOVAToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.normalProbabilityPlotToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ftestdescBasedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.systematicErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.correlationAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.correlationMatrixToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.covarianceMatrixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qualityControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sSMDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.correlationMatrixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descriptorEvolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.classesDistributionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createAveragePlateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pCAToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lDAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hitsIdentificationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mahalanobisDistanceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.marginalManualClusteringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.hitsDistributionMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validationToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fTestToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aNOVAToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.samplesTTestToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.studentTTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.groupingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wellsMergingToolsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.resetGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.replicateScatterPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.dRCAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dRCDesignerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemGeneAnalysis = new System.Windows.Forms.ToolStripMenuItem();
            this.findGeneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pahtwaysAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findPathwayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.betaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dRCAnalysisToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.convertDRCToWellToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.currentPlate3DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.distributionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.distributionsModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayReferenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.newOptionMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heatMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testRStatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testBoxPlotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testLinearRegressionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testMultiScatterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testPieChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testPPTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testReplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testPubMedSOAPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sigmoidFittToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mDBTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawSingleDRCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spiralToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.basic3DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wellsMergingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dRC3DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutHCSAnalyzerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkedListBoxActiveDescriptors = new System.Windows.Forms.CheckedListBox();
            this.comboBoxDescriptorToDisplay = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.contextMenuStripForLUT = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.tabControlMainView = new System.Windows.Forms.TabControl();
            this.tabPageMainView = new System.Windows.Forms.TabPage();
            this.tabPageDataView = new System.Windows.Forms.TabPage();
            this.panelForTableView = new System.Windows.Forms.Panel();
            this.tabPage2DView = new System.Windows.Forms.TabPage();
            this.tabPage1DView = new System.Windows.Forms.TabPage();
            this.tabPage3DPlatesView = new System.Windows.Forms.TabPage();
            this.buttonPreviousPlate = new System.Windows.Forms.Button();
            this.buttonNextPlate = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControlForScreening = new System.Windows.Forms.TabControl();
            this.tabPageListWells = new System.Windows.Forms.TabPage();
            this.listViewForListWell = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderConcentration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderObjNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageClassHistory = new System.Windows.Forms.TabPage();
            this.listViewClassHistory = new System.Windows.Forms.ListView();
            this.columnHeaderHistoryName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderHistoryDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageScreeningInfo = new System.Windows.Forms.TabPage();
            this.tabPageConsole = new System.Windows.Forms.TabPage();
            this.richTextBoxConsole = new System.Windows.Forms.RichTextBox();
            this.tabPage3DWorld = new System.Windows.Forms.TabPage();
            this.listView3DWorld = new System.Windows.Forms.ListView();
            this.columnHeader3DWorldName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3DWorldInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButtonApplyClass = new System.Windows.Forms.ToolStripDropDownButton();
            this.globalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalIfOnlyActiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButtonProcessMode = new System.Windows.Forms.ToolStripDropDownButton();
            this.ProcessModeCurrentPlateOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProcessModeplateByPlateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProcessModeEntireScreeningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripDropDownButtonDisplayMode = new System.Windows.Forms.ToolStripDropDownButton();
            this.tabControlMain.SuspendLayout();
            this.tabPageDImRed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNewDimension)).BeginInit();
            this.groupBoxUnsupervised.SuspendLayout();
            this.groupBoxSupervised.SuspendLayout();
            this.tabPageQualityQtrl.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRejectionThreshold)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabPageNormalization.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.tabPageClassification.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.tabPageExport.SuspendLayout();
            this.panelForTools.SuspendLayout();
            this.groupBoxQC.SuspendLayout();
            this.contextMenuStripForQC.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panelForPlate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.menuStripFile.SuspendLayout();
            this.contextMenuStripForLUT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.tabControlMainView.SuspendLayout();
            this.tabPageMainView.SuspendLayout();
            this.tabPageDataView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlForScreening.SuspendLayout();
            this.tabPageListWells.SuspendLayout();
            this.tabPageClassHistory.SuspendLayout();
            this.tabPageScreeningInfo.SuspendLayout();
            this.tabPageConsole.SuspendLayout();
            this.tabPage3DWorld.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.AllowDrop = true;
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageDImRed);
            this.tabControlMain.Controls.Add(this.tabPageQualityQtrl);
            this.tabControlMain.Controls.Add(this.tabPageNormalization);
            this.tabControlMain.Controls.Add(this.tabPageClassification);
            this.tabControlMain.Controls.Add(this.tabPageExport);
            this.tabControlMain.Location = new System.Drawing.Point(5, 3);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(869, 241);
            this.tabControlMain.TabIndex = 5;
            this.tabControlMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.tabControlMain_DragDrop);
            this.tabControlMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.tabControlMain_DragEnter);
            // 
            // tabPageDImRed
            // 
            this.tabPageDImRed.Controls.Add(this.label3);
            this.tabPageDImRed.Controls.Add(this.numericUpDownNewDimension);
            this.tabPageDImRed.Controls.Add(this.radioButtonDimRedSupervised);
            this.tabPageDImRed.Controls.Add(this.radioButtonDimRedUnsupervised);
            this.tabPageDImRed.Controls.Add(this.buttonReduceDim);
            this.tabPageDImRed.Controls.Add(this.groupBoxUnsupervised);
            this.tabPageDImRed.Controls.Add(this.groupBoxSupervised);
            this.tabPageDImRed.ImageIndex = 5;
            this.tabPageDImRed.Location = new System.Drawing.Point(4, 22);
            this.tabPageDImRed.Name = "tabPageDImRed";
            this.tabPageDImRed.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDImRed.Size = new System.Drawing.Size(861, 215);
            this.tabPageDImRed.TabIndex = 8;
            this.tabPageDImRed.Text = "Dimensionality Reduction";
            this.tabPageDImRed.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "New Dimension";
            // 
            // numericUpDownNewDimension
            // 
            this.numericUpDownNewDimension.Location = new System.Drawing.Point(21, 55);
            this.numericUpDownNewDimension.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNewDimension.Name = "numericUpDownNewDimension";
            this.numericUpDownNewDimension.Size = new System.Drawing.Size(83, 20);
            this.numericUpDownNewDimension.TabIndex = 1;
            this.numericUpDownNewDimension.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // radioButtonDimRedSupervised
            // 
            this.radioButtonDimRedSupervised.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonDimRedSupervised.AutoSize = true;
            this.radioButtonDimRedSupervised.Location = new System.Drawing.Point(479, 6);
            this.radioButtonDimRedSupervised.Name = "radioButtonDimRedSupervised";
            this.radioButtonDimRedSupervised.Size = new System.Drawing.Size(78, 17);
            this.radioButtonDimRedSupervised.TabIndex = 3;
            this.radioButtonDimRedSupervised.TabStop = true;
            this.radioButtonDimRedSupervised.Text = "Supervised";
            this.radioButtonDimRedSupervised.UseVisualStyleBackColor = true;
            this.radioButtonDimRedSupervised.CheckedChanged += new System.EventHandler(this.radioButtonDimRedSupervised_CheckedChanged);
            // 
            // radioButtonDimRedUnsupervised
            // 
            this.radioButtonDimRedUnsupervised.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonDimRedUnsupervised.AutoSize = true;
            this.radioButtonDimRedUnsupervised.Checked = true;
            this.radioButtonDimRedUnsupervised.Location = new System.Drawing.Point(199, 6);
            this.radioButtonDimRedUnsupervised.Name = "radioButtonDimRedUnsupervised";
            this.radioButtonDimRedUnsupervised.Size = new System.Drawing.Size(90, 17);
            this.radioButtonDimRedUnsupervised.TabIndex = 2;
            this.radioButtonDimRedUnsupervised.TabStop = true;
            this.radioButtonDimRedUnsupervised.Text = "Unsupervised";
            this.radioButtonDimRedUnsupervised.UseVisualStyleBackColor = true;
            this.radioButtonDimRedUnsupervised.CheckedChanged += new System.EventHandler(this.radioButtonDimRedUnsupervised_CheckedChanged);
            // 
            // buttonReduceDim
            // 
            this.buttonReduceDim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReduceDim.Enabled = false;
            this.buttonReduceDim.Location = new System.Drawing.Point(721, 172);
            this.buttonReduceDim.Name = "buttonReduceDim";
            this.buttonReduceDim.Size = new System.Drawing.Size(134, 37);
            this.buttonReduceDim.TabIndex = 9;
            this.buttonReduceDim.Text = "Reduce Dimensionality";
            this.buttonReduceDim.UseVisualStyleBackColor = true;
            this.buttonReduceDim.Click += new System.EventHandler(this.buttonReduceDim_Click);
            // 
            // groupBoxUnsupervised
            // 
            this.groupBoxUnsupervised.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxUnsupervised.Controls.Add(this.richTextBoxUnsupervisedDimRec);
            this.groupBoxUnsupervised.Controls.Add(this.comboBoxReduceDimSingleClass);
            this.groupBoxUnsupervised.Location = new System.Drawing.Point(117, 29);
            this.groupBoxUnsupervised.Name = "groupBoxUnsupervised";
            this.groupBoxUnsupervised.Size = new System.Drawing.Size(263, 183);
            this.groupBoxUnsupervised.TabIndex = 7;
            this.groupBoxUnsupervised.TabStop = false;
            // 
            // richTextBoxUnsupervisedDimRec
            // 
            this.richTextBoxUnsupervisedDimRec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxUnsupervisedDimRec.Location = new System.Drawing.Point(6, 89);
            this.richTextBoxUnsupervisedDimRec.Name = "richTextBoxUnsupervisedDimRec";
            this.richTextBoxUnsupervisedDimRec.ReadOnly = true;
            this.richTextBoxUnsupervisedDimRec.Size = new System.Drawing.Size(251, 87);
            this.richTextBoxUnsupervisedDimRec.TabIndex = 5;
            this.richTextBoxUnsupervisedDimRec.Text = "";
            this.richTextBoxUnsupervisedDimRec.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxUnsupervisedDimRec_LinkClicked);
            // 
            // comboBoxReduceDimSingleClass
            // 
            this.comboBoxReduceDimSingleClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxReduceDimSingleClass.FormattingEnabled = true;
            this.comboBoxReduceDimSingleClass.Items.AddRange(new object[] {
            "PCA",
            "Greedy Stepwise"});
            this.comboBoxReduceDimSingleClass.Location = new System.Drawing.Point(45, 25);
            this.comboBoxReduceDimSingleClass.Name = "comboBoxReduceDimSingleClass";
            this.comboBoxReduceDimSingleClass.Size = new System.Drawing.Size(169, 21);
            this.comboBoxReduceDimSingleClass.TabIndex = 4;
            this.comboBoxReduceDimSingleClass.SelectedIndexChanged += new System.EventHandler(this.comboBoxReduceDimSingleClass_SelectedIndexChanged);
            // 
            // groupBoxSupervised
            // 
            this.groupBoxSupervised.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxSupervised.Controls.Add(this.comboBoxDimReductionNeutralClass);
            this.groupBoxSupervised.Controls.Add(this.richTextBoxSupervisedDimRec);
            this.groupBoxSupervised.Controls.Add(this.comboBoxReduceDimMultiClass);
            this.groupBoxSupervised.Controls.Add(this.label6);
            this.groupBoxSupervised.Enabled = false;
            this.groupBoxSupervised.Location = new System.Drawing.Point(392, 29);
            this.groupBoxSupervised.Name = "groupBoxSupervised";
            this.groupBoxSupervised.Size = new System.Drawing.Size(263, 183);
            this.groupBoxSupervised.TabIndex = 8;
            this.groupBoxSupervised.TabStop = false;
            // 
            // comboBoxDimReductionNeutralClass
            // 
            this.comboBoxDimReductionNeutralClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxDimReductionNeutralClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBoxDimReductionNeutralClass.FormattingEnabled = true;
            this.comboBoxDimReductionNeutralClass.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.comboBoxDimReductionNeutralClass.Location = new System.Drawing.Point(111, 57);
            this.comboBoxDimReductionNeutralClass.Name = "comboBoxDimReductionNeutralClass";
            this.comboBoxDimReductionNeutralClass.Size = new System.Drawing.Size(133, 21);
            this.comboBoxDimReductionNeutralClass.TabIndex = 7;
            this.comboBoxDimReductionNeutralClass.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxDimReductionNeutralClass_DrawItem);
            // 
            // richTextBoxSupervisedDimRec
            // 
            this.richTextBoxSupervisedDimRec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxSupervisedDimRec.Location = new System.Drawing.Point(6, 89);
            this.richTextBoxSupervisedDimRec.Name = "richTextBoxSupervisedDimRec";
            this.richTextBoxSupervisedDimRec.ReadOnly = true;
            this.richTextBoxSupervisedDimRec.Size = new System.Drawing.Size(251, 87);
            this.richTextBoxSupervisedDimRec.TabIndex = 8;
            this.richTextBoxSupervisedDimRec.Text = "";
            this.richTextBoxSupervisedDimRec.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxSupervisedDimRec_LinkClicked);
            // 
            // comboBoxReduceDimMultiClass
            // 
            this.comboBoxReduceDimMultiClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxReduceDimMultiClass.FormattingEnabled = true;
            this.comboBoxReduceDimMultiClass.Items.AddRange(new object[] {
            "InfoGain",
            "OneR",
            "Greedy"});
            this.comboBoxReduceDimMultiClass.Location = new System.Drawing.Point(40, 25);
            this.comboBoxReduceDimMultiClass.Name = "comboBoxReduceDimMultiClass";
            this.comboBoxReduceDimMultiClass.Size = new System.Drawing.Size(182, 21);
            this.comboBoxReduceDimMultiClass.TabIndex = 6;
            this.comboBoxReduceDimMultiClass.SelectedIndexChanged += new System.EventHandler(this.comboBoxReduceDimMultiClass_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Neutral Class";
            // 
            // tabPageQualityQtrl
            // 
            this.tabPageQualityQtrl.Controls.Add(this.groupBox1);
            this.tabPageQualityQtrl.Controls.Add(this.groupBox2);
            this.tabPageQualityQtrl.ImageIndex = 1;
            this.tabPageQualityQtrl.Location = new System.Drawing.Point(4, 22);
            this.tabPageQualityQtrl.Name = "tabPageQualityQtrl";
            this.tabPageQualityQtrl.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageQualityQtrl.Size = new System.Drawing.Size(861, 215);
            this.tabPageQualityQtrl.TabIndex = 7;
            this.tabPageQualityQtrl.Text = "Systematic Error Correction";
            this.tabPageQualityQtrl.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonRejectPlates);
            this.groupBox1.Controls.Add(this.comboBoxRejectionPositiveCtrl);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.comboBoxRejectionNegativeCtrl);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.numericUpDownRejectionThreshold);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.richTextBoxInformationRejection);
            this.groupBox1.Controls.Add(this.comboBoxRejection);
            this.groupBox1.Location = new System.Drawing.Point(277, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 202);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rejection";
            // 
            // buttonRejectPlates
            // 
            this.buttonRejectPlates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRejectPlates.Enabled = false;
            this.buttonRejectPlates.Location = new System.Drawing.Point(65, 162);
            this.buttonRejectPlates.Name = "buttonRejectPlates";
            this.buttonRejectPlates.Size = new System.Drawing.Size(150, 34);
            this.buttonRejectPlates.TabIndex = 14;
            this.buttonRejectPlates.Text = "Reject Plates";
            this.buttonRejectPlates.UseVisualStyleBackColor = true;
            this.buttonRejectPlates.Click += new System.EventHandler(this.buttonRejectPlates_Click);
            // 
            // comboBoxRejectionPositiveCtrl
            // 
            this.comboBoxRejectionPositiveCtrl.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBoxRejectionPositiveCtrl.FormattingEnabled = true;
            this.comboBoxRejectionPositiveCtrl.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.comboBoxRejectionPositiveCtrl.Location = new System.Drawing.Point(161, 87);
            this.comboBoxRejectionPositiveCtrl.Name = "comboBoxRejectionPositiveCtrl";
            this.comboBoxRejectionPositiveCtrl.Size = new System.Drawing.Size(108, 21);
            this.comboBoxRejectionPositiveCtrl.TabIndex = 31;
            this.comboBoxRejectionPositiveCtrl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxRejectionPositiveCtrl_DrawItem);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(172, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "Positive Control";
            // 
            // comboBoxRejectionNegativeCtrl
            // 
            this.comboBoxRejectionNegativeCtrl.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBoxRejectionNegativeCtrl.FormattingEnabled = true;
            this.comboBoxRejectionNegativeCtrl.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.comboBoxRejectionNegativeCtrl.Location = new System.Drawing.Point(27, 87);
            this.comboBoxRejectionNegativeCtrl.Name = "comboBoxRejectionNegativeCtrl";
            this.comboBoxRejectionNegativeCtrl.Size = new System.Drawing.Size(110, 21);
            this.comboBoxRejectionNegativeCtrl.TabIndex = 30;
            this.comboBoxRejectionNegativeCtrl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxRejectionNegativeCtrl_DrawItem);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(39, 71);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 13);
            this.label12.TabIndex = 32;
            this.label12.Text = "Negative Control";
            // 
            // numericUpDownRejectionThreshold
            // 
            this.numericUpDownRejectionThreshold.DecimalPlaces = 2;
            this.numericUpDownRejectionThreshold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownRejectionThreshold.Location = new System.Drawing.Point(117, 44);
            this.numericUpDownRejectionThreshold.Name = "numericUpDownRejectionThreshold";
            this.numericUpDownRejectionThreshold.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownRejectionThreshold.TabIndex = 6;
            this.numericUpDownRejectionThreshold.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(52, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Threshold";
            // 
            // richTextBoxInformationRejection
            // 
            this.richTextBoxInformationRejection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxInformationRejection.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBoxInformationRejection.ForeColor = System.Drawing.SystemColors.WindowText;
            this.richTextBoxInformationRejection.Location = new System.Drawing.Point(6, 114);
            this.richTextBoxInformationRejection.Name = "richTextBoxInformationRejection";
            this.richTextBoxInformationRejection.ReadOnly = true;
            this.richTextBoxInformationRejection.Size = new System.Drawing.Size(279, 42);
            this.richTextBoxInformationRejection.TabIndex = 4;
            this.richTextBoxInformationRejection.Text = "";
            this.richTextBoxInformationRejection.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxInformationRejection_LinkClicked);
            // 
            // comboBoxRejection
            // 
            this.comboBoxRejection.FormattingEnabled = true;
            this.comboBoxRejection.Items.AddRange(new object[] {
            "Z-Factor"});
            this.comboBoxRejection.Location = new System.Drawing.Point(54, 16);
            this.comboBoxRejection.Name = "comboBoxRejection";
            this.comboBoxRejection.Size = new System.Drawing.Size(182, 21);
            this.comboBoxRejection.TabIndex = 3;
            this.comboBoxRejection.SelectedIndexChanged += new System.EventHandler(this.comboBoxRejection_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.richTextBoxInformationForPlateCorrection);
            this.groupBox2.Controls.Add(this.comboBoxMethodForCorrection);
            this.groupBox2.Controls.Add(this.buttonCorrectionPlateByPlate);
            this.groupBox2.Location = new System.Drawing.Point(9, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(262, 202);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Correction";
            // 
            // richTextBoxInformationForPlateCorrection
            // 
            this.richTextBoxInformationForPlateCorrection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxInformationForPlateCorrection.Location = new System.Drawing.Point(5, 64);
            this.richTextBoxInformationForPlateCorrection.Name = "richTextBoxInformationForPlateCorrection";
            this.richTextBoxInformationForPlateCorrection.ReadOnly = true;
            this.richTextBoxInformationForPlateCorrection.Size = new System.Drawing.Size(251, 92);
            this.richTextBoxInformationForPlateCorrection.TabIndex = 4;
            this.richTextBoxInformationForPlateCorrection.Text = "";
            this.richTextBoxInformationForPlateCorrection.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxInformationForPlateCorrection_LinkClicked);
            // 
            // comboBoxMethodForCorrection
            // 
            this.comboBoxMethodForCorrection.FormattingEnabled = true;
            this.comboBoxMethodForCorrection.Items.AddRange(new object[] {
            "B-Score",
            "Diffusion Model"});
            this.comboBoxMethodForCorrection.Location = new System.Drawing.Point(38, 32);
            this.comboBoxMethodForCorrection.Name = "comboBoxMethodForCorrection";
            this.comboBoxMethodForCorrection.Size = new System.Drawing.Size(182, 21);
            this.comboBoxMethodForCorrection.TabIndex = 3;
            this.comboBoxMethodForCorrection.SelectedIndexChanged += new System.EventHandler(this.comboBoxMethodForCorrection_SelectedIndexChanged);
            // 
            // buttonCorrectionPlateByPlate
            // 
            this.buttonCorrectionPlateByPlate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCorrectionPlateByPlate.Enabled = false;
            this.buttonCorrectionPlateByPlate.Location = new System.Drawing.Point(56, 162);
            this.buttonCorrectionPlateByPlate.Name = "buttonCorrectionPlateByPlate";
            this.buttonCorrectionPlateByPlate.Size = new System.Drawing.Size(150, 34);
            this.buttonCorrectionPlateByPlate.TabIndex = 5;
            this.buttonCorrectionPlateByPlate.Text = "Plate-by-plate Correction";
            this.buttonCorrectionPlateByPlate.UseVisualStyleBackColor = true;
            this.buttonCorrectionPlateByPlate.Click += new System.EventHandler(this.buttonCorrectionPlateByPlate_Click);
            // 
            // tabPageNormalization
            // 
            this.tabPageNormalization.Controls.Add(this.buttonNormalize);
            this.tabPageNormalization.Controls.Add(this.groupBox15);
            this.tabPageNormalization.ImageIndex = 2;
            this.tabPageNormalization.Location = new System.Drawing.Point(4, 22);
            this.tabPageNormalization.Name = "tabPageNormalization";
            this.tabPageNormalization.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNormalization.Size = new System.Drawing.Size(861, 215);
            this.tabPageNormalization.TabIndex = 3;
            this.tabPageNormalization.Text = "Normalization";
            this.tabPageNormalization.UseVisualStyleBackColor = true;
            // 
            // buttonNormalize
            // 
            this.buttonNormalize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNormalize.Enabled = false;
            this.buttonNormalize.Location = new System.Drawing.Point(705, 175);
            this.buttonNormalize.Name = "buttonNormalize";
            this.buttonNormalize.Size = new System.Drawing.Size(150, 34);
            this.buttonNormalize.TabIndex = 5;
            this.buttonNormalize.Text = "Normalize";
            this.buttonNormalize.UseVisualStyleBackColor = true;
            this.buttonNormalize.Click += new System.EventHandler(this.buttonNormalize_Click);
            // 
            // groupBox15
            // 
            this.groupBox15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox15.Controls.Add(this.comboBoxNormalizationPositiveCtrl);
            this.groupBox15.Controls.Add(this.label7);
            this.groupBox15.Controls.Add(this.comboBoxNormalizationNegativeCtrl);
            this.groupBox15.Controls.Add(this.label4);
            this.groupBox15.Controls.Add(this.richTextBoxInfoForNormalization);
            this.groupBox15.Controls.Add(this.comboBoxMethodForNormalization);
            this.groupBox15.Location = new System.Drawing.Point(6, 6);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(476, 203);
            this.groupBox15.TabIndex = 8;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Normalization";
            // 
            // comboBoxNormalizationPositiveCtrl
            // 
            this.comboBoxNormalizationPositiveCtrl.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBoxNormalizationPositiveCtrl.FormattingEnabled = true;
            this.comboBoxNormalizationPositiveCtrl.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.comboBoxNormalizationPositiveCtrl.Location = new System.Drawing.Point(335, 56);
            this.comboBoxNormalizationPositiveCtrl.Name = "comboBoxNormalizationPositiveCtrl";
            this.comboBoxNormalizationPositiveCtrl.Size = new System.Drawing.Size(120, 21);
            this.comboBoxNormalizationPositiveCtrl.TabIndex = 3;
            this.comboBoxNormalizationPositiveCtrl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxNormalizationPositiveCtrl_DrawItem);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(257, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Positive Class";
            // 
            // comboBoxNormalizationNegativeCtrl
            // 
            this.comboBoxNormalizationNegativeCtrl.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBoxNormalizationNegativeCtrl.FormattingEnabled = true;
            this.comboBoxNormalizationNegativeCtrl.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.comboBoxNormalizationNegativeCtrl.Location = new System.Drawing.Point(100, 56);
            this.comboBoxNormalizationNegativeCtrl.Name = "comboBoxNormalizationNegativeCtrl";
            this.comboBoxNormalizationNegativeCtrl.Size = new System.Drawing.Size(120, 21);
            this.comboBoxNormalizationNegativeCtrl.TabIndex = 2;
            this.comboBoxNormalizationNegativeCtrl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxNormalizationNegativeCtrl_DrawItem);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Negative Class";
            // 
            // richTextBoxInfoForNormalization
            // 
            this.richTextBoxInfoForNormalization.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxInfoForNormalization.Location = new System.Drawing.Point(6, 83);
            this.richTextBoxInfoForNormalization.Name = "richTextBoxInfoForNormalization";
            this.richTextBoxInfoForNormalization.ReadOnly = true;
            this.richTextBoxInfoForNormalization.Size = new System.Drawing.Size(464, 114);
            this.richTextBoxInfoForNormalization.TabIndex = 4;
            this.richTextBoxInfoForNormalization.Text = "";
            this.richTextBoxInfoForNormalization.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxInfoForNormalization_LinkClicked);
            // 
            // comboBoxMethodForNormalization
            // 
            this.comboBoxMethodForNormalization.FormattingEnabled = true;
            this.comboBoxMethodForNormalization.Items.AddRange(new object[] {
            "Percent of control ",
            "Normalized percent inhibition",
            "Z-score",
            "Min-Max"});
            this.comboBoxMethodForNormalization.Location = new System.Drawing.Point(147, 21);
            this.comboBoxMethodForNormalization.Name = "comboBoxMethodForNormalization";
            this.comboBoxMethodForNormalization.Size = new System.Drawing.Size(182, 21);
            this.comboBoxMethodForNormalization.TabIndex = 1;
            this.comboBoxMethodForNormalization.SelectedIndexChanged += new System.EventHandler(this.comboBoxMethodForNormalization_SelectedIndexChanged);
            // 
            // tabPageClassification
            // 
            this.tabPageClassification.Controls.Add(this.groupBox12);
            this.tabPageClassification.Controls.Add(this.groupBox11);
            this.tabPageClassification.ImageIndex = 3;
            this.tabPageClassification.Location = new System.Drawing.Point(4, 22);
            this.tabPageClassification.Name = "tabPageClassification";
            this.tabPageClassification.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageClassification.Size = new System.Drawing.Size(861, 215);
            this.tabPageClassification.TabIndex = 4;
            this.tabPageClassification.Text = "Clustering & Classification";
            this.tabPageClassification.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox12.Controls.Add(this.ButtonClustering);
            this.groupBox12.Controls.Add(this.richTextBoxInfoClustering);
            this.groupBox12.Location = new System.Drawing.Point(9, 6);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(406, 203);
            this.groupBox12.TabIndex = 5;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Clustering";
            // 
            // ButtonClustering
            // 
            this.ButtonClustering.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonClustering.Location = new System.Drawing.Point(7, 163);
            this.ButtonClustering.Name = "ButtonClustering";
            this.ButtonClustering.Size = new System.Drawing.Size(88, 34);
            this.ButtonClustering.TabIndex = 29;
            this.ButtonClustering.Text = "Clustering";
            this.ButtonClustering.UseVisualStyleBackColor = true;
            this.ButtonClustering.Click += new System.EventHandler(this.buttonClustering_Click);
            // 
            // richTextBoxInfoClustering
            // 
            this.richTextBoxInfoClustering.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxInfoClustering.Location = new System.Drawing.Point(106, 10);
            this.richTextBoxInfoClustering.Name = "richTextBoxInfoClustering";
            this.richTextBoxInfoClustering.ReadOnly = true;
            this.richTextBoxInfoClustering.Size = new System.Drawing.Size(294, 187);
            this.richTextBoxInfoClustering.TabIndex = 0;
            this.richTextBoxInfoClustering.Text = "";
            // 
            // groupBox11
            // 
            this.groupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox11.Controls.Add(this.panelTMPForFeedBack);
            this.groupBox11.Controls.Add(this.buttonNewClassificationProcess);
            this.groupBox11.Controls.Add(this.comboBoxNeutralClassForClassif);
            this.groupBox11.Controls.Add(this.buttonStartClassification);
            this.groupBox11.Controls.Add(this.label5);
            this.groupBox11.Controls.Add(this.richTextBoxInfoClassif);
            this.groupBox11.Controls.Add(this.comboBoxCLassificationMethod);
            this.groupBox11.Location = new System.Drawing.Point(421, 6);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(381, 203);
            this.groupBox11.TabIndex = 5;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Classification";
            // 
            // panelTMPForFeedBack
            // 
            this.panelTMPForFeedBack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTMPForFeedBack.Location = new System.Drawing.Point(259, 16);
            this.panelTMPForFeedBack.Name = "panelTMPForFeedBack";
            this.panelTMPForFeedBack.Size = new System.Drawing.Size(116, 179);
            this.panelTMPForFeedBack.TabIndex = 33;
            // 
            // buttonNewClassificationProcess
            // 
            this.buttonNewClassificationProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonNewClassificationProcess.Location = new System.Drawing.Point(158, 161);
            this.buttonNewClassificationProcess.Name = "buttonNewClassificationProcess";
            this.buttonNewClassificationProcess.Size = new System.Drawing.Size(88, 34);
            this.buttonNewClassificationProcess.TabIndex = 28;
            this.buttonNewClassificationProcess.Text = "New Classification";
            this.buttonNewClassificationProcess.UseVisualStyleBackColor = true;
            this.buttonNewClassificationProcess.Click += new System.EventHandler(this.buttonNewClassificationProcess_Click);
            // 
            // comboBoxNeutralClassForClassif
            // 
            this.comboBoxNeutralClassForClassif.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBoxNeutralClassForClassif.FormattingEnabled = true;
            this.comboBoxNeutralClassForClassif.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.comboBoxNeutralClassForClassif.Location = new System.Drawing.Point(113, 80);
            this.comboBoxNeutralClassForClassif.Name = "comboBoxNeutralClassForClassif";
            this.comboBoxNeutralClassForClassif.Size = new System.Drawing.Size(133, 21);
            this.comboBoxNeutralClassForClassif.TabIndex = 26;
            this.comboBoxNeutralClassForClassif.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxNeutralClassForClassif_DrawItem);
            // 
            // buttonStartClassification
            // 
            this.buttonStartClassification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStartClassification.Enabled = false;
            this.buttonStartClassification.Location = new System.Drawing.Point(27, 161);
            this.buttonStartClassification.Name = "buttonStartClassification";
            this.buttonStartClassification.Size = new System.Drawing.Size(88, 34);
            this.buttonStartClassification.TabIndex = 1;
            this.buttonStartClassification.Text = "Classify";
            this.buttonStartClassification.UseVisualStyleBackColor = true;
            this.buttonStartClassification.Click += new System.EventHandler(this.buttonStartClassification_Click_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "To Be Classified";
            // 
            // richTextBoxInfoClassif
            // 
            this.richTextBoxInfoClassif.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxInfoClassif.Location = new System.Drawing.Point(6, 110);
            this.richTextBoxInfoClassif.Name = "richTextBoxInfoClassif";
            this.richTextBoxInfoClassif.ReadOnly = true;
            this.richTextBoxInfoClassif.Size = new System.Drawing.Size(251, 47);
            this.richTextBoxInfoClassif.TabIndex = 0;
            this.richTextBoxInfoClassif.Text = "";
            this.richTextBoxInfoClassif.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxInfoClassif_LinkClicked);
            // 
            // comboBoxCLassificationMethod
            // 
            this.comboBoxCLassificationMethod.FormattingEnabled = true;
            this.comboBoxCLassificationMethod.Items.AddRange(new object[] {
            "C4.5",
            "Support Vector Machine",
            "Neural Network",
            "K Nearest Neighbor(s)",
            "Random Forest"});
            this.comboBoxCLassificationMethod.Location = new System.Drawing.Point(40, 20);
            this.comboBoxCLassificationMethod.Name = "comboBoxCLassificationMethod";
            this.comboBoxCLassificationMethod.Size = new System.Drawing.Size(182, 21);
            this.comboBoxCLassificationMethod.TabIndex = 19;
            this.comboBoxCLassificationMethod.SelectedIndexChanged += new System.EventHandler(this.comboBoxCLassificationMethod_SelectedIndexChanged);
            // 
            // tabPageExport
            // 
            this.tabPageExport.Controls.Add(this.treeViewSelectionForExport);
            this.tabPageExport.Controls.Add(this.checkBoxExportFullScreen);
            this.tabPageExport.Controls.Add(this.checkBoxExportPlateFormat);
            this.tabPageExport.Controls.Add(this.buttonExport);
            this.tabPageExport.ImageIndex = 4;
            this.tabPageExport.Location = new System.Drawing.Point(4, 22);
            this.tabPageExport.Name = "tabPageExport";
            this.tabPageExport.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExport.Size = new System.Drawing.Size(861, 215);
            this.tabPageExport.TabIndex = 5;
            this.tabPageExport.Text = "Report Export*";
            this.tabPageExport.UseVisualStyleBackColor = true;
            // 
            // treeViewSelectionForExport
            // 
            this.treeViewSelectionForExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewSelectionForExport.CheckBoxes = true;
            this.treeViewSelectionForExport.FullRowSelect = true;
            this.treeViewSelectionForExport.Location = new System.Drawing.Point(9, 56);
            this.treeViewSelectionForExport.Name = "treeViewSelectionForExport";
            treeNode1.Name = "NodeClassifTree";
            treeNode1.Text = "Classification Tree";
            treeNode2.Name = "NodeClassification";
            treeNode2.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode2.Text = "Classification";
            treeNode3.Checked = true;
            treeNode3.Name = "NodeCorrelationMatRank";
            treeNode3.Text = "Correlation Matrix and Ranking";
            treeNode4.Checked = true;
            treeNode4.Name = "NodeSystematicError";
            treeNode4.Text = "Systematic Errors Table";
            treeNode5.Checked = true;
            treeNode5.Name = "NodeZfactor";
            treeNode5.Text = "Z-Factors";
            treeNode6.Checked = true;
            treeNode6.Name = "NodeQualityControl";
            treeNode6.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode6.Text = "Quality Control";
            treeNode7.Name = "NodePathwayAnalysis";
            treeNode7.Text = "Pathway Analysis";
            treeNode8.Name = "NodesiRNA";
            treeNode8.Text = "siRNA screening";
            treeNode9.Name = "NodeWekaArff";
            treeNode9.Text = "Weka .Arff File";
            treeNode10.Name = "NodeMisc";
            treeNode10.Text = "Misc";
            this.treeViewSelectionForExport.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode6,
            treeNode8,
            treeNode10});
            this.treeViewSelectionForExport.Size = new System.Drawing.Size(322, 153);
            this.treeViewSelectionForExport.TabIndex = 16;
            // 
            // checkBoxExportFullScreen
            // 
            this.checkBoxExportFullScreen.AutoSize = true;
            this.checkBoxExportFullScreen.Checked = true;
            this.checkBoxExportFullScreen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxExportFullScreen.Location = new System.Drawing.Point(25, 33);
            this.checkBoxExportFullScreen.Name = "checkBoxExportFullScreen";
            this.checkBoxExportFullScreen.Size = new System.Drawing.Size(79, 17);
            this.checkBoxExportFullScreen.TabIndex = 9;
            this.checkBoxExportFullScreen.Text = "Full Screen";
            this.checkBoxExportFullScreen.UseVisualStyleBackColor = true;
            // 
            // checkBoxExportPlateFormat
            // 
            this.checkBoxExportPlateFormat.AutoSize = true;
            this.checkBoxExportPlateFormat.Checked = true;
            this.checkBoxExportPlateFormat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxExportPlateFormat.Location = new System.Drawing.Point(123, 33);
            this.checkBoxExportPlateFormat.Name = "checkBoxExportPlateFormat";
            this.checkBoxExportPlateFormat.Size = new System.Drawing.Size(85, 17);
            this.checkBoxExportPlateFormat.TabIndex = 10;
            this.checkBoxExportPlateFormat.Text = "Plate Format";
            this.checkBoxExportPlateFormat.UseVisualStyleBackColor = true;
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.Enabled = false;
            this.buttonExport.Location = new System.Drawing.Point(1041, 184);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(124, 39);
            this.buttonExport.TabIndex = 0;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // richTextBoxForScreeningInformation
            // 
            this.richTextBoxForScreeningInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxForScreeningInformation.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxForScreeningInformation.Name = "richTextBoxForScreeningInformation";
            this.richTextBoxForScreeningInformation.Size = new System.Drawing.Size(339, 231);
            this.richTextBoxForScreeningInformation.TabIndex = 0;
            this.richTextBoxForScreeningInformation.Text = "";
            this.richTextBoxForScreeningInformation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBoxForScreeningInformation_KeyDown);
            this.richTextBoxForScreeningInformation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.richTextBoxForScreeningInformation_MouseDown);
            // 
            // imageListForTab
            // 
            this.imageListForTab.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListForTab.ImageStream")));
            this.imageListForTab.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListForTab.Images.SetKeyName(0, "Picture1.png");
            this.imageListForTab.Images.SetKeyName(1, "Picture2.png");
            this.imageListForTab.Images.SetKeyName(2, "Picture3.png");
            this.imageListForTab.Images.SetKeyName(3, "Picture4.png");
            this.imageListForTab.Images.SetKeyName(4, "Picture5.png");
            this.imageListForTab.Images.SetKeyName(5, "Picture6.png");
            // 
            // panelForTools
            // 
            this.panelForTools.AutoScroll = true;
            this.panelForTools.BackColor = System.Drawing.Color.Transparent;
            this.panelForTools.Controls.Add(this.groupBoxQC);
            this.panelForTools.Controls.Add(this.groupBox3);
            this.panelForTools.Location = new System.Drawing.Point(5, 3);
            this.panelForTools.Name = "panelForTools";
            this.panelForTools.Size = new System.Drawing.Size(119, 446);
            this.panelForTools.TabIndex = 34;
            // 
            // groupBoxQC
            // 
            this.groupBoxQC.ContextMenuStrip = this.contextMenuStripForQC;
            this.groupBoxQC.Controls.Add(this.comboBoxQC);
            this.groupBoxQC.Controls.Add(this.labelQC);
            this.groupBoxQC.Location = new System.Drawing.Point(4, 310);
            this.groupBoxQC.Name = "groupBoxQC";
            this.groupBoxQC.Size = new System.Drawing.Size(110, 65);
            this.groupBoxQC.TabIndex = 35;
            this.groupBoxQC.TabStop = false;
            this.groupBoxQC.Text = "QC";
            // 
            // contextMenuStripForQC
            // 
            this.contextMenuStripForQC.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defineClassesToolStripMenuItem});
            this.contextMenuStripForQC.Name = "contextMenuStripForQC";
            this.contextMenuStripForQC.Size = new System.Drawing.Size(150, 26);
            // 
            // defineClassesToolStripMenuItem
            // 
            this.defineClassesToolStripMenuItem.Name = "defineClassesToolStripMenuItem";
            this.defineClassesToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.defineClassesToolStripMenuItem.Text = "Define Classes";
            this.defineClassesToolStripMenuItem.Click += new System.EventHandler(this.defineClassesToolStripMenuItem_Click);
            // 
            // comboBoxQC
            // 
            this.comboBoxQC.FormattingEnabled = true;
            this.comboBoxQC.Items.AddRange(new object[] {
            "Z\'",
            "RZ\'"});
            this.comboBoxQC.Location = new System.Drawing.Point(5, 19);
            this.comboBoxQC.Name = "comboBoxQC";
            this.comboBoxQC.Size = new System.Drawing.Size(101, 21);
            this.comboBoxQC.TabIndex = 31;
            this.comboBoxQC.Text = "Z\'";
            this.comboBoxQC.SelectedIndexChanged += new System.EventHandler(this.comboBoxQC_SelectedIndexChanged);
            // 
            // labelQC
            // 
            this.labelQC.AutoSize = true;
            this.labelQC.Location = new System.Drawing.Point(6, 43);
            this.labelQC.Name = "labelQC";
            this.labelQC.Size = new System.Drawing.Size(28, 13);
            this.labelQC.TabIndex = 33;
            this.labelQC.Text = "###";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBoxClass);
            this.groupBox3.Controls.Add(this.labelNumClasses);
            this.groupBox3.Location = new System.Drawing.Point(4, 239);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(110, 65);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Active Class";
            // 
            // comboBoxClass
            // 
            this.comboBoxClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBoxClass.FormattingEnabled = true;
            this.comboBoxClass.Location = new System.Drawing.Point(5, 19);
            this.comboBoxClass.Name = "comboBoxClass";
            this.comboBoxClass.Size = new System.Drawing.Size(101, 21);
            this.comboBoxClass.TabIndex = 31;
            this.comboBoxClass.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxClass_DrawItem_1);
            this.comboBoxClass.SelectedIndexChanged += new System.EventHandler(this.comboBoxClass_SelectedIndexChanged);
            // 
            // labelNumClasses
            // 
            this.labelNumClasses.AutoSize = true;
            this.labelNumClasses.Location = new System.Drawing.Point(6, 43);
            this.labelNumClasses.Name = "labelNumClasses";
            this.labelNumClasses.Size = new System.Drawing.Size(28, 13);
            this.labelNumClasses.TabIndex = 33;
            this.labelNumClasses.Text = "###";
            // 
            // panelForPlate
            // 
            this.panelForPlate.AllowDrop = true;
            this.panelForPlate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForPlate.AutoScroll = true;
            this.panelForPlate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(37)))), ((int)(((byte)(63)))));
            this.panelForPlate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelForPlate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelForPlate.Controls.Add(this.pictureBox3);
            this.panelForPlate.Location = new System.Drawing.Point(6, 3);
            this.panelForPlate.Name = "panelForPlate";
            this.panelForPlate.Size = new System.Drawing.Size(786, 411);
            this.panelForPlate.TabIndex = 0;
            this.panelForPlate.Paint += new System.Windows.Forms.PaintEventHandler(this.panelForPlate_Paint);
            this.panelForPlate.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelForPlate_MouseDoubleClick);
            this.panelForPlate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelForPlate_MouseDown);
            this.panelForPlate.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelForPlate_MouseMove);
            this.panelForPlate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelForPlate_MouseUp);
            this.panelForPlate.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.panelForPlate_MouseWheel);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.Image = global::HCSAnalyzer.Properties.Resources.DarkLogo;
            this.pictureBox3.Location = new System.Drawing.Point(655, 287);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(124, 117);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // menuStripFile
            // 
            this.menuStripFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.copyAverageValuesToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolStripcomboBoxPlateList,
            this.visualizationToolStripMenuItem,
            this.StatisticsToolStripMenuItem,
            this.toolStripMenuItemGeneAnalysis,
            this.betaToolStripMenuItem,
            this.pluginsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStripFile.Location = new System.Drawing.Point(0, 0);
            this.menuStripFile.Name = "menuStripFile";
            this.menuStripFile.Size = new System.Drawing.Size(1236, 27);
            this.menuStripFile.TabIndex = 12;
            this.menuStripFile.Text = "File";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.importScreenDirectoryToolStripMenuItem,
            this.cellByCellToolStripMenuItem,
            this.generateScreenToolStripMenuItem1,
            this.toolStripSeparator2,
            this.exportToolStripMenuItem,
            this.appendDescriptorsToolStripMenuItem,
            this.linkToolStripMenuItem,
            this.toolStripMenuItemLoadProperty,
            this.toolStripSeparator17,
            this.toolStripMenuItemLoadImage,
            this.loadExtendedTableToolStripMenuItem,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Image = global::HCSAnalyzer.Properties.Resources.db_comit;
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.importToolStripMenuItem.Text = "Import Screen";
            this.importToolStripMenuItem.ToolTipText = "Load screen from regular format";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // importScreenDirectoryToolStripMenuItem
            // 
            this.importScreenDirectoryToolStripMenuItem.Name = "importScreenDirectoryToolStripMenuItem";
            this.importScreenDirectoryToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.importScreenDirectoryToolStripMenuItem.Text = "Import Screen (Directory)";
            this.importScreenDirectoryToolStripMenuItem.Click += new System.EventHandler(this.importScreenDirectoryToolStripMenuItem_Click);
            // 
            // cellByCellToolStripMenuItem
            // 
            this.cellByCellToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDBToolStripMenuItem,
            this.toolStripSeparator19,
            this.buildDatabaseToolStripMenuItem,
            this.toolStripSeparator11,
            this.exportAsCSVToolStripMenuItem});
            this.cellByCellToolStripMenuItem.Name = "cellByCellToolStripMenuItem";
            this.cellByCellToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.cellByCellToolStripMenuItem.Text = "Cell by Cell";
            // 
            // loadDBToolStripMenuItem
            // 
            this.loadDBToolStripMenuItem.Name = "loadDBToolStripMenuItem";
            this.loadDBToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadDBToolStripMenuItem.Text = "Load Database";
            this.loadDBToolStripMenuItem.Click += new System.EventHandler(this.loadDBToolStripMenuItem_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(149, 6);
            // 
            // buildDatabaseToolStripMenuItem
            // 
            this.buildDatabaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cSVDBToolStripMenuItem,
            this.harmonyDBToolStripMenuItem});
            this.buildDatabaseToolStripMenuItem.Name = "buildDatabaseToolStripMenuItem";
            this.buildDatabaseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.buildDatabaseToolStripMenuItem.Text = "Build Database";
            // 
            // cSVDBToolStripMenuItem
            // 
            this.cSVDBToolStripMenuItem.Name = "cSVDBToolStripMenuItem";
            this.cSVDBToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.cSVDBToolStripMenuItem.Text = "Flat Files -> DB";
            this.cSVDBToolStripMenuItem.Click += new System.EventHandler(this.cSVDBToolStripMenuItem_Click);
            // 
            // harmonyDBToolStripMenuItem
            // 
            this.harmonyDBToolStripMenuItem.Name = "harmonyDBToolStripMenuItem";
            this.harmonyDBToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.harmonyDBToolStripMenuItem.Text = "Harmony -> DB";
            this.harmonyDBToolStripMenuItem.Click += new System.EventHandler(this.harmonyDBToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(149, 6);
            // 
            // exportAsCSVToolStripMenuItem
            // 
            this.exportAsCSVToolStripMenuItem.Enabled = false;
            this.exportAsCSVToolStripMenuItem.Name = "exportAsCSVToolStripMenuItem";
            this.exportAsCSVToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportAsCSVToolStripMenuItem.Text = "Export as CSV";
            this.exportAsCSVToolStripMenuItem.Click += new System.EventHandler(this.exportAsCSVToolStripMenuItem_Click);
            // 
            // generateScreenToolStripMenuItem1
            // 
            this.generateScreenToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.univariateToolStripMenuItem,
            this.multivariateToolStripMenuItem,
            this.singleCellsSimulatorToolStripMenuItem,
            this.toolStripSeparator10,
            this.fromImagesToolStripMenuItem});
            this.generateScreenToolStripMenuItem1.Name = "generateScreenToolStripMenuItem1";
            this.generateScreenToolStripMenuItem1.Size = new System.Drawing.Size(207, 22);
            this.generateScreenToolStripMenuItem1.Text = "Generate Screen";
            // 
            // univariateToolStripMenuItem
            // 
            this.univariateToolStripMenuItem.Name = "univariateToolStripMenuItem";
            this.univariateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.univariateToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.univariateToolStripMenuItem.Text = "Univariate";
            this.univariateToolStripMenuItem.Click += new System.EventHandler(this.univariateToolStripMenuItem_Click);
            // 
            // multivariateToolStripMenuItem
            // 
            this.multivariateToolStripMenuItem.Name = "multivariateToolStripMenuItem";
            this.multivariateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.G)));
            this.multivariateToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.multivariateToolStripMenuItem.Text = "Multivariate";
            this.multivariateToolStripMenuItem.Click += new System.EventHandler(this.multivariateToolStripMenuItem_Click);
            // 
            // singleCellsSimulatorToolStripMenuItem
            // 
            this.singleCellsSimulatorToolStripMenuItem.Name = "singleCellsSimulatorToolStripMenuItem";
            this.singleCellsSimulatorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.G)));
            this.singleCellsSimulatorToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.singleCellsSimulatorToolStripMenuItem.Text = "Single Cells Simulator";
            this.singleCellsSimulatorToolStripMenuItem.ToolTipText = "Simulator to generate multivariate cell-by-cell based screening.";
            this.singleCellsSimulatorToolStripMenuItem.Click += new System.EventHandler(this.singleCellsSimulatorToolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(259, 6);
            // 
            // fromImagesToolStripMenuItem
            // 
            this.fromImagesToolStripMenuItem.Name = "fromImagesToolStripMenuItem";
            this.fromImagesToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.fromImagesToolStripMenuItem.Text = "From Images";
            this.fromImagesToolStripMenuItem.Click += new System.EventHandler(this.fromImagesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(204, 6);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveScreentoCSVToolStripMenuItem,
            this.currentPlateTomtrToolStripMenuItem,
            this.toARFFToolStripMenuItem});
            this.exportToolStripMenuItem.Enabled = false;
            this.exportToolStripMenuItem.Image = global::HCSAnalyzer.Properties.Resources.db_update;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.exportToolStripMenuItem.Text = "Save Screen";
            // 
            // SaveScreentoCSVToolStripMenuItem
            // 
            this.SaveScreentoCSVToolStripMenuItem.Name = "SaveScreentoCSVToolStripMenuItem";
            this.SaveScreentoCSVToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.SaveScreentoCSVToolStripMenuItem.Text = "To CSV";
            this.SaveScreentoCSVToolStripMenuItem.Click += new System.EventHandler(this.toExcelToolStripMenuItem_Click);
            // 
            // currentPlateTomtrToolStripMenuItem
            // 
            this.currentPlateTomtrToolStripMenuItem.Name = "currentPlateTomtrToolStripMenuItem";
            this.currentPlateTomtrToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.currentPlateTomtrToolStripMenuItem.Text = "To MTR";
            this.currentPlateTomtrToolStripMenuItem.ToolTipText = "Warning: only the selected descriptor will be saved in this format";
            this.currentPlateTomtrToolStripMenuItem.Click += new System.EventHandler(this.currentPlateTomtrToolStripMenuItem_Click);
            // 
            // toARFFToolStripMenuItem
            // 
            this.toARFFToolStripMenuItem.Name = "toARFFToolStripMenuItem";
            this.toARFFToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.toARFFToolStripMenuItem.Text = "To ARFF";
            this.toARFFToolStripMenuItem.Click += new System.EventHandler(this.toARFFToolStripMenuItem_Click);
            // 
            // appendDescriptorsToolStripMenuItem
            // 
            this.appendDescriptorsToolStripMenuItem.Enabled = false;
            this.appendDescriptorsToolStripMenuItem.Image = global::HCSAnalyzer.Properties.Resources.db_add;
            this.appendDescriptorsToolStripMenuItem.Name = "appendDescriptorsToolStripMenuItem";
            this.appendDescriptorsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.appendDescriptorsToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.appendDescriptorsToolStripMenuItem.Text = "Add Plates";
            this.appendDescriptorsToolStripMenuItem.Click += new System.EventHandler(this.appendAssayToolStripMenuItem_Click);
            // 
            // linkToolStripMenuItem
            // 
            this.linkToolStripMenuItem.Image = global::HCSAnalyzer.Properties.Resources.insert_link;
            this.linkToolStripMenuItem.Name = "linkToolStripMenuItem";
            this.linkToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.linkToolStripMenuItem.Text = "Link Compound Names";
            this.linkToolStripMenuItem.Click += new System.EventHandler(this.linkToolStripMenuItem_Click);
            // 
            // toolStripMenuItemLoadProperty
            // 
            this.toolStripMenuItemLoadProperty.Enabled = false;
            this.toolStripMenuItemLoadProperty.Image = global::HCSAnalyzer.Properties.Resources.insert_link;
            this.toolStripMenuItemLoadProperty.Name = "toolStripMenuItemLoadProperty";
            this.toolStripMenuItemLoadProperty.Size = new System.Drawing.Size(207, 22);
            this.toolStripMenuItemLoadProperty.Text = "Load Property";
            this.toolStripMenuItemLoadProperty.Click += new System.EventHandler(this.toolStripMenuItemLoadCellLines_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(204, 6);
            // 
            // toolStripMenuItemLoadImage
            // 
            this.toolStripMenuItemLoadImage.Name = "toolStripMenuItemLoadImage";
            this.toolStripMenuItemLoadImage.Size = new System.Drawing.Size(207, 22);
            this.toolStripMenuItemLoadImage.Text = "Load Image";
            this.toolStripMenuItemLoadImage.ToolTipText = "Load and display an image";
            this.toolStripMenuItemLoadImage.Click += new System.EventHandler(this.toolStripMenuItemLoadImage_Click);
            // 
            // loadExtendedTableToolStripMenuItem
            // 
            this.loadExtendedTableToolStripMenuItem.Name = "loadExtendedTableToolStripMenuItem";
            this.loadExtendedTableToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.loadExtendedTableToolStripMenuItem.Text = "Load Table";
            this.loadExtendedTableToolStripMenuItem.ToolTipText = "Load and display a regular table containing row and column headers";
            this.loadExtendedTableToolStripMenuItem.Click += new System.EventHandler(this.loadExtendedTableToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(204, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::HCSAnalyzer.Properties.Resources.application_exit;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // copyAverageValuesToolStripMenuItem
            // 
            this.copyAverageValuesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyAverageValuesToolStripMenuItem1,
            this.copyPropertyToolStripMenuItem,
            this.swapClassesToolStripMenuItem,
            this.toolStripSeparator3,
            this.applySelectionToScreenToolStripMenuItem,
            this.toolStripSeparator9,
            this.toolStripMenuItemDescManagement,
            this.newToolStripMenuItem,
            this.toolStripSeparator1,
            this.optionsToolStripMenuItem,
            this.toolStripSeparatorPaste,
            this.pasteToolStripMenuItem});
            this.copyAverageValuesToolStripMenuItem.Name = "copyAverageValuesToolStripMenuItem";
            this.copyAverageValuesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyAverageValuesToolStripMenuItem.Size = new System.Drawing.Size(39, 23);
            this.copyAverageValuesToolStripMenuItem.Text = "Edit";
            this.copyAverageValuesToolStripMenuItem.DropDownOpening += new System.EventHandler(this.copyAverageValuesToolStripMenuItem_DropDownOpening);
            // 
            // copyAverageValuesToolStripMenuItem1
            // 
            this.copyAverageValuesToolStripMenuItem1.Enabled = false;
            this.copyAverageValuesToolStripMenuItem1.Name = "copyAverageValuesToolStripMenuItem1";
            this.copyAverageValuesToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.copyAverageValuesToolStripMenuItem1.Size = new System.Drawing.Size(293, 22);
            this.copyAverageValuesToolStripMenuItem1.Text = "Copy Values to Clipboard";
            this.copyAverageValuesToolStripMenuItem1.ToolTipText = "Copy the average values of the current plate and descriptor to the clipboard";
            this.copyAverageValuesToolStripMenuItem1.Click += new System.EventHandler(this.copyAverageValuesToolStripMenuItem1_Click);
            // 
            // copyPropertyToolStripMenuItem
            // 
            this.copyPropertyToolStripMenuItem.Enabled = false;
            this.copyPropertyToolStripMenuItem.Name = "copyPropertyToolStripMenuItem";
            this.copyPropertyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.copyPropertyToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.copyPropertyToolStripMenuItem.Text = "Copy Property to Clipboard";
            this.copyPropertyToolStripMenuItem.ToolTipText = "Copy property values to the clipboard";
            this.copyPropertyToolStripMenuItem.Click += new System.EventHandler(this.copyPropertyToolStripMenuItem_Click);
            // 
            // swapClassesToolStripMenuItem
            // 
            this.swapClassesToolStripMenuItem.Enabled = false;
            this.swapClassesToolStripMenuItem.Name = "swapClassesToolStripMenuItem";
            this.swapClassesToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.swapClassesToolStripMenuItem.Text = "Swap Classes";
            this.swapClassesToolStripMenuItem.Click += new System.EventHandler(this.swapClassesToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(290, 6);
            // 
            // applySelectionToScreenToolStripMenuItem
            // 
            this.applySelectionToScreenToolStripMenuItem.Enabled = false;
            this.applySelectionToScreenToolStripMenuItem.Name = "applySelectionToScreenToolStripMenuItem";
            this.applySelectionToScreenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.applySelectionToScreenToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.applySelectionToScreenToolStripMenuItem.Text = "Apply selection To screen";
            this.applySelectionToScreenToolStripMenuItem.ToolTipText = "Apply the current plate classes to all the rest of the screen";
            this.applySelectionToScreenToolStripMenuItem.Click += new System.EventHandler(this.applySelectionToScreenToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(290, 6);
            // 
            // toolStripMenuItemDescManagement
            // 
            this.toolStripMenuItemDescManagement.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.toolStripMenuItemDescManagement.Enabled = false;
            this.toolStripMenuItemDescManagement.Name = "toolStripMenuItemDescManagement";
            this.toolStripMenuItemDescManagement.Size = new System.Drawing.Size(293, 22);
            this.toolStripMenuItemDescManagement.Text = "Descriptors";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(57, 6);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dDisplayToolStripMenuItem,
            this.imageToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.newToolStripMenuItem.Text = "New...";
            // 
            // dDisplayToolStripMenuItem
            // 
            this.dDisplayToolStripMenuItem.Name = "dDisplayToolStripMenuItem";
            this.dDisplayToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.dDisplayToolStripMenuItem.Text = "3D World";
            this.dDisplayToolStripMenuItem.Click += new System.EventHandler(this.dDisplayToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.imageToolStripMenuItem.Text = "Image";
            this.imageToolStripMenuItem.Click += new System.EventHandler(this.imageToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(290, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Image = global::HCSAnalyzer.Properties.Resources.configure_4;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // toolStripSeparatorPaste
            // 
            this.toolStripSeparatorPaste.Name = "toolStripSeparatorPaste";
            this.toolStripSeparatorPaste.Size = new System.Drawing.Size(290, 6);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Enabled = false;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.platesManagerToolStripMenuItem,
            this.toolStripSeparator6,
            this.plateViewToolStripMenuItem,
            this.descriptorViewToolStripMenuItem,
            this.toolStripSeparator7,
            this.classViewToolStripMenuItem,
            this.toolStripSeparator13,
            this.averageViewToolStripMenuItem,
            this.histogramViewToolStripMenuItem,
            this.pieViewToolStripMenuItem1,
            this.imageViewToolStripMenuItem,
            this.toolStripSeparator12,
            this.ThreeDVisualizationToolStripMenuItem});
            this.viewToolStripMenuItem.Enabled = false;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(114, 23);
            this.viewToolStripMenuItem.Text = "Windows && Views";
            // 
            // platesManagerToolStripMenuItem
            // 
            this.platesManagerToolStripMenuItem.Enabled = false;
            this.platesManagerToolStripMenuItem.Name = "platesManagerToolStripMenuItem";
            this.platesManagerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            this.platesManagerToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.platesManagerToolStripMenuItem.Text = "Plates Manager";
            this.platesManagerToolStripMenuItem.Click += new System.EventHandler(this.platesManagerToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(189, 6);
            // 
            // plateViewToolStripMenuItem
            // 
            this.plateViewToolStripMenuItem.Name = "plateViewToolStripMenuItem";
            this.plateViewToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.plateViewToolStripMenuItem.Text = "Plate Window";
            this.plateViewToolStripMenuItem.Click += new System.EventHandler(this.plateViewToolStripMenuItem_Click);
            // 
            // descriptorViewToolStripMenuItem
            // 
            this.descriptorViewToolStripMenuItem.Name = "descriptorViewToolStripMenuItem";
            this.descriptorViewToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.descriptorViewToolStripMenuItem.Text = "Descriptor Window";
            this.descriptorViewToolStripMenuItem.Click += new System.EventHandler(this.descriptorViewToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(189, 6);
            // 
            // classViewToolStripMenuItem
            // 
            this.classViewToolStripMenuItem.CheckOnClick = true;
            this.classViewToolStripMenuItem.Name = "classViewToolStripMenuItem";
            this.classViewToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.classViewToolStripMenuItem.Text = "Class View";
            this.classViewToolStripMenuItem.Click += new System.EventHandler(this.classViewToolStripMenuItem_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(189, 6);
            // 
            // averageViewToolStripMenuItem
            // 
            this.averageViewToolStripMenuItem.Checked = true;
            this.averageViewToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.averageViewToolStripMenuItem.Name = "averageViewToolStripMenuItem";
            this.averageViewToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.averageViewToolStripMenuItem.Text = "Average View";
            this.averageViewToolStripMenuItem.Click += new System.EventHandler(this.averageViewToolStripMenuItem_Click);
            // 
            // histogramViewToolStripMenuItem
            // 
            this.histogramViewToolStripMenuItem.Name = "histogramViewToolStripMenuItem";
            this.histogramViewToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.histogramViewToolStripMenuItem.Text = "Histogram View";
            this.histogramViewToolStripMenuItem.Click += new System.EventHandler(this.histogramViewToolStripMenuItem_Click);
            // 
            // pieViewToolStripMenuItem1
            // 
            this.pieViewToolStripMenuItem1.CheckOnClick = true;
            this.pieViewToolStripMenuItem1.Name = "pieViewToolStripMenuItem1";
            this.pieViewToolStripMenuItem1.Size = new System.Drawing.Size(192, 22);
            this.pieViewToolStripMenuItem1.Text = "Pie View";
            this.pieViewToolStripMenuItem1.Click += new System.EventHandler(this.pieViewToolStripMenuItem1_Click);
            // 
            // imageViewToolStripMenuItem
            // 
            this.imageViewToolStripMenuItem.Enabled = false;
            this.imageViewToolStripMenuItem.Name = "imageViewToolStripMenuItem";
            this.imageViewToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.imageViewToolStripMenuItem.Text = "Image View";
            this.imageViewToolStripMenuItem.Click += new System.EventHandler(this.imageViewToolStripMenuItem_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(189, 6);
            // 
            // ThreeDVisualizationToolStripMenuItem
            // 
            this.ThreeDVisualizationToolStripMenuItem.CheckOnClick = true;
            this.ThreeDVisualizationToolStripMenuItem.Name = "ThreeDVisualizationToolStripMenuItem";
            this.ThreeDVisualizationToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.ThreeDVisualizationToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.ThreeDVisualizationToolStripMenuItem.Text = "3D Visualization";
            this.ThreeDVisualizationToolStripMenuItem.Click += new System.EventHandler(this.ThreeDVisualizationToolStripMenuItem_Click);
            // 
            // toolStripcomboBoxPlateList
            // 
            this.toolStripcomboBoxPlateList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.toolStripcomboBoxPlateList.AutoToolTip = true;
            this.toolStripcomboBoxPlateList.DropDownWidth = 121;
            this.toolStripcomboBoxPlateList.MaxDropDownItems = 10;
            this.toolStripcomboBoxPlateList.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.toolStripcomboBoxPlateList.Name = "toolStripcomboBoxPlateList";
            this.toolStripcomboBoxPlateList.Size = new System.Drawing.Size(300, 23);
            this.toolStripcomboBoxPlateList.SelectedIndexChanged += new System.EventHandler(this.toolStripcomboBoxPlateList_SelectedIndexChanged);
            // 
            // visualizationToolStripMenuItem
            // 
            this.visualizationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stackedHistogramsToolStripMenuItem,
            this.distanceMatrixToolStripMenuItem,
            this.hierarchicalTreeToolStripMenuItem,
            this.singleCellToolStripMenuItem,
            this.scatterPlotsToolStripMenuItem});
            this.visualizationToolStripMenuItem.Enabled = false;
            this.visualizationToolStripMenuItem.Name = "visualizationToolStripMenuItem";
            this.visualizationToolStripMenuItem.Size = new System.Drawing.Size(112, 23);
            this.visualizationToolStripMenuItem.Text = "Data Visualization";
            this.visualizationToolStripMenuItem.Click += new System.EventHandler(this.visualizationToolStripMenuItem_Click);
            // 
            // stackedHistogramsToolStripMenuItem
            // 
            this.stackedHistogramsToolStripMenuItem.Name = "stackedHistogramsToolStripMenuItem";
            this.stackedHistogramsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.stackedHistogramsToolStripMenuItem.Text = "Stacked histograms";
            this.stackedHistogramsToolStripMenuItem.ToolTipText = "Display stacked histograms of the active descriptor";
            this.stackedHistogramsToolStripMenuItem.Click += new System.EventHandler(this.stackedHistogramsToolStripMenuItem_Click);
            // 
            // distanceMatrixToolStripMenuItem
            // 
            this.distanceMatrixToolStripMenuItem.Name = "distanceMatrixToolStripMenuItem";
            this.distanceMatrixToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.distanceMatrixToolStripMenuItem.Text = "Distance Matrix*";
            this.distanceMatrixToolStripMenuItem.Click += new System.EventHandler(this.distanceMatrixToolStripMenuItem_Click);
            // 
            // hierarchicalTreeToolStripMenuItem
            // 
            this.hierarchicalTreeToolStripMenuItem.Name = "hierarchicalTreeToolStripMenuItem";
            this.hierarchicalTreeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.hierarchicalTreeToolStripMenuItem.Text = "Hierarchical Tree*";
            this.hierarchicalTreeToolStripMenuItem.Click += new System.EventHandler(this.hierarchicalTreeToolStripMenuItem_Click);
            // 
            // singleCellToolStripMenuItem
            // 
            this.singleCellToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.histogramsToolStripMenuItem});
            this.singleCellToolStripMenuItem.Name = "singleCellToolStripMenuItem";
            this.singleCellToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.singleCellToolStripMenuItem.Text = "Single Cell";
            // 
            // histogramsToolStripMenuItem
            // 
            this.histogramsToolStripMenuItem.Name = "histogramsToolStripMenuItem";
            this.histogramsToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.histogramsToolStripMenuItem.Text = "Histograms";
            this.histogramsToolStripMenuItem.Click += new System.EventHandler(this.histogramsToolStripMenuItem_Click);
            // 
            // scatterPlotsToolStripMenuItem
            // 
            this.scatterPlotsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OneDScatterToolStripMenuItem,
            this.dToolStripMenuItemScatterPlot2D,
            this.dToolStripMenuItemScatterPlot3D});
            this.scatterPlotsToolStripMenuItem.Name = "scatterPlotsToolStripMenuItem";
            this.scatterPlotsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.scatterPlotsToolStripMenuItem.Text = "Scatter Plots";
            // 
            // OneDScatterToolStripMenuItem
            // 
            this.OneDScatterToolStripMenuItem.Name = "OneDScatterToolStripMenuItem";
            this.OneDScatterToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
            this.OneDScatterToolStripMenuItem.Text = "1D";
            this.OneDScatterToolStripMenuItem.Click += new System.EventHandler(this.dToolStripMenuItem_Click);
            // 
            // dToolStripMenuItemScatterPlot2D
            // 
            this.dToolStripMenuItemScatterPlot2D.Name = "dToolStripMenuItemScatterPlot2D";
            this.dToolStripMenuItemScatterPlot2D.Size = new System.Drawing.Size(88, 22);
            this.dToolStripMenuItemScatterPlot2D.Text = "2D";
            this.dToolStripMenuItemScatterPlot2D.Click += new System.EventHandler(this.dToolStripMenuItem1_Click);
            // 
            // dToolStripMenuItemScatterPlot3D
            // 
            this.dToolStripMenuItemScatterPlot3D.Name = "dToolStripMenuItemScatterPlot3D";
            this.dToolStripMenuItemScatterPlot3D.Size = new System.Drawing.Size(88, 22);
            this.dToolStripMenuItemScatterPlot3D.Text = "3D";
            this.dToolStripMenuItemScatterPlot3D.Click += new System.EventHandler(this.dToolStripMenuItemScatterPlot3D_Click);
            // 
            // StatisticsToolStripMenuItem
            // 
            this.StatisticsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.qualityControlsToolStripMenuItem,
            this.correlationAnalysisToolStripMenuItem,
            this.qualityControlToolStripMenuItem,
            this.projectionsToolStripMenuItem1,
            this.hitsIdentificationToolStripMenuItem,
            this.toolStripSeparator18,
            this.groupingToolStripMenuItem,
            this.toolStripSeparator20,
            this.dRCAnalysisToolStripMenuItem});
            this.StatisticsToolStripMenuItem.Enabled = false;
            this.StatisticsToolStripMenuItem.Name = "StatisticsToolStripMenuItem";
            this.StatisticsToolStripMenuItem.Size = new System.Drawing.Size(124, 23);
            this.StatisticsToolStripMenuItem.Text = "Statistics && Analysis";
            this.StatisticsToolStripMenuItem.DropDownOpened += new System.EventHandler(this.StatisticsToolStripMenuItem_DropDownOpened);
            // 
            // qualityControlsToolStripMenuItem
            // 
            this.qualityControlsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zScoreToolStripMenuItem,
            this.tTestToolStripMenuItem,
            this.mannWithneyTestToolStripMenuItem,
            this.aNOVAToolStripMenuItem1,
            this.toolStripSeparator8,
            this.normalProbabilityPlotToolStripMenuItem2,
            this.ftestdescBasedToolStripMenuItem,
            this.statisticsToolStripMenuItem1,
            this.toolStripSeparator22,
            this.systematicErrorsToolStripMenuItem});
            this.qualityControlsToolStripMenuItem.Name = "qualityControlsToolStripMenuItem";
            this.qualityControlsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.qualityControlsToolStripMenuItem.Text = "Quality Controls";
            // 
            // zScoreToolStripMenuItem
            // 
            this.zScoreToolStripMenuItem.Name = "zScoreToolStripMenuItem";
            this.zScoreToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.zScoreToolStripMenuItem.Text = "Z\' (Regular)";
            this.zScoreToolStripMenuItem.Click += new System.EventHandler(this.zScoreToolStripMenuItem_Click);
            this.zScoreToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.zScoreToolStripMenuItem_MouseDown);
            // 
            // tTestToolStripMenuItem
            // 
            this.tTestToolStripMenuItem.Name = "tTestToolStripMenuItem";
            this.tTestToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.tTestToolStripMenuItem.Text = "t-Test";
            this.tTestToolStripMenuItem.Click += new System.EventHandler(this.tTestToolStripMenuItem_Click);
            // 
            // mannWithneyTestToolStripMenuItem
            // 
            this.mannWithneyTestToolStripMenuItem.Name = "mannWithneyTestToolStripMenuItem";
            this.mannWithneyTestToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.mannWithneyTestToolStripMenuItem.Text = "Mann-Withney Test";
            this.mannWithneyTestToolStripMenuItem.Click += new System.EventHandler(this.mannWithneyTestToolStripMenuItem_Click);
            // 
            // aNOVAToolStripMenuItem1
            // 
            this.aNOVAToolStripMenuItem1.Name = "aNOVAToolStripMenuItem1";
            this.aNOVAToolStripMenuItem1.Size = new System.Drawing.Size(253, 22);
            this.aNOVAToolStripMenuItem1.Text = "ANOVA";
            this.aNOVAToolStripMenuItem1.Click += new System.EventHandler(this.aNOVAToolStripMenuItem1_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(250, 6);
            // 
            // normalProbabilityPlotToolStripMenuItem2
            // 
            this.normalProbabilityPlotToolStripMenuItem2.Name = "normalProbabilityPlotToolStripMenuItem2";
            this.normalProbabilityPlotToolStripMenuItem2.Size = new System.Drawing.Size(253, 22);
            this.normalProbabilityPlotToolStripMenuItem2.Text = "Normal Probability Plot";
            this.normalProbabilityPlotToolStripMenuItem2.Click += new System.EventHandler(this.normalProbabilityPlotToolStripMenuItem2_Click);
            // 
            // ftestdescBasedToolStripMenuItem
            // 
            this.ftestdescBasedToolStripMenuItem.Name = "ftestdescBasedToolStripMenuItem";
            this.ftestdescBasedToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.ftestdescBasedToolStripMenuItem.Text = "F-test (desc. based)";
            this.ftestdescBasedToolStripMenuItem.Click += new System.EventHandler(this.ftestdescBasedToolStripMenuItem_Click);
            // 
            // statisticsToolStripMenuItem1
            // 
            this.statisticsToolStripMenuItem1.Name = "statisticsToolStripMenuItem1";
            this.statisticsToolStripMenuItem1.Size = new System.Drawing.Size(253, 22);
            this.statisticsToolStripMenuItem1.Text = "Statistics (Coefficient of Variation)";
            this.statisticsToolStripMenuItem1.Click += new System.EventHandler(this.statisticsToolStripMenuItem1_Click_1);
            this.statisticsToolStripMenuItem1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.statisticsToolStripMenuItem1_MouseDown);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(250, 6);
            // 
            // systematicErrorsToolStripMenuItem
            // 
            this.systematicErrorsToolStripMenuItem.Name = "systematicErrorsToolStripMenuItem";
            this.systematicErrorsToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.systematicErrorsToolStripMenuItem.Text = "Systematic Errors Identification";
            this.systematicErrorsToolStripMenuItem.Click += new System.EventHandler(this.systematicErrorsToolStripMenuItem_Click);
            // 
            // correlationAnalysisToolStripMenuItem
            // 
            this.correlationAnalysisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aToolStripMenuItem,
            this.correlationMatrixToolStripMenuItem1,
            this.covarianceMatrixToolStripMenuItem});
            this.correlationAnalysisToolStripMenuItem.Name = "correlationAnalysisToolStripMenuItem";
            this.correlationAnalysisToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.correlationAnalysisToolStripMenuItem.Text = "Correlation analysis";
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.aToolStripMenuItem.Text = "MINE analysis";
            this.aToolStripMenuItem.Click += new System.EventHandler(this.aToolStripMenuItem_Click_1);
            // 
            // correlationMatrixToolStripMenuItem1
            // 
            this.correlationMatrixToolStripMenuItem1.Name = "correlationMatrixToolStripMenuItem1";
            this.correlationMatrixToolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
            this.correlationMatrixToolStripMenuItem1.Text = "Correlation matrix";
            this.correlationMatrixToolStripMenuItem1.Click += new System.EventHandler(this.correlationMatrixToolStripMenuItem1_Click);
            // 
            // covarianceMatrixToolStripMenuItem
            // 
            this.covarianceMatrixToolStripMenuItem.Name = "covarianceMatrixToolStripMenuItem";
            this.covarianceMatrixToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.covarianceMatrixToolStripMenuItem.Text = "Covariance matrix";
            this.covarianceMatrixToolStripMenuItem.Click += new System.EventHandler(this.covarianceMatrixToolStripMenuItem_Click);
            // 
            // qualityControlToolStripMenuItem
            // 
            this.qualityControlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sSMDToolStripMenuItem,
            this.correlationMatrixToolStripMenuItem,
            this.descriptorEvolutionToolStripMenuItem,
            this.classesDistributionToolStripMenuItem,
            this.createAveragePlateToolStripMenuItem});
            this.qualityControlToolStripMenuItem.Name = "qualityControlToolStripMenuItem";
            this.qualityControlToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.qualityControlToolStripMenuItem.Text = "Full Screen*";
            // 
            // sSMDToolStripMenuItem
            // 
            this.sSMDToolStripMenuItem.Name = "sSMDToolStripMenuItem";
            this.sSMDToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.sSMDToolStripMenuItem.Text = "SSMD*";
            // 
            // correlationMatrixToolStripMenuItem
            // 
            this.correlationMatrixToolStripMenuItem.Name = "correlationMatrixToolStripMenuItem";
            this.correlationMatrixToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.M)));
            this.correlationMatrixToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.correlationMatrixToolStripMenuItem.Text = "Correlation Matrix";
            this.correlationMatrixToolStripMenuItem.Click += new System.EventHandler(this.correlationMatrixToolStripMenuItem_Click);
            // 
            // descriptorEvolutionToolStripMenuItem
            // 
            this.descriptorEvolutionToolStripMenuItem.Name = "descriptorEvolutionToolStripMenuItem";
            this.descriptorEvolutionToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.descriptorEvolutionToolStripMenuItem.Text = "Descriptor Evolutions (Global)";
            this.descriptorEvolutionToolStripMenuItem.Click += new System.EventHandler(this.descriptorEvolutionToolStripMenuItem_Click);
            this.descriptorEvolutionToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.descriptorEvolutionToolStripMenuItem_MouseDown);
            // 
            // classesDistributionToolStripMenuItem
            // 
            this.classesDistributionToolStripMenuItem.Name = "classesDistributionToolStripMenuItem";
            this.classesDistributionToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.classesDistributionToolStripMenuItem.Text = "Classes Distribution*";
            this.classesDistributionToolStripMenuItem.Click += new System.EventHandler(this.classesDistributionToolStripMenuItem_Click);
            // 
            // createAveragePlateToolStripMenuItem
            // 
            this.createAveragePlateToolStripMenuItem.Name = "createAveragePlateToolStripMenuItem";
            this.createAveragePlateToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.createAveragePlateToolStripMenuItem.Text = "Create Average Plate";
            this.createAveragePlateToolStripMenuItem.Click += new System.EventHandler(this.createAveragePlateToolStripMenuItem_Click);
            // 
            // projectionsToolStripMenuItem1
            // 
            this.projectionsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pCAToolStripMenuItem1,
            this.lDAToolStripMenuItem});
            this.projectionsToolStripMenuItem1.Name = "projectionsToolStripMenuItem1";
            this.projectionsToolStripMenuItem1.Size = new System.Drawing.Size(177, 22);
            this.projectionsToolStripMenuItem1.Text = "Projections";
            // 
            // pCAToolStripMenuItem1
            // 
            this.pCAToolStripMenuItem1.Name = "pCAToolStripMenuItem1";
            this.pCAToolStripMenuItem1.Size = new System.Drawing.Size(97, 22);
            this.pCAToolStripMenuItem1.Text = "PCA";
            this.pCAToolStripMenuItem1.ToolTipText = "Principal Component Analysis";
            this.pCAToolStripMenuItem1.Click += new System.EventHandler(this.pCAToolStripMenuItem1_Click);
            // 
            // lDAToolStripMenuItem
            // 
            this.lDAToolStripMenuItem.Name = "lDAToolStripMenuItem";
            this.lDAToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.lDAToolStripMenuItem.Text = "LDA";
            this.lDAToolStripMenuItem.ToolTipText = "Linear Decomposition Analysis";
            this.lDAToolStripMenuItem.Click += new System.EventHandler(this.lDAToolStripMenuItem_Click);
            // 
            // hitsIdentificationToolStripMenuItem
            // 
            this.hitsIdentificationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mahalanobisDistanceToolStripMenuItem1,
            this.marginalManualClusteringToolStripMenuItem,
            this.toolStripSeparator15,
            this.hitsDistributionMapToolStripMenuItem,
            this.validationToolStripMenuItem1});
            this.hitsIdentificationToolStripMenuItem.Name = "hitsIdentificationToolStripMenuItem";
            this.hitsIdentificationToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.hitsIdentificationToolStripMenuItem.Text = "Hits Identification";
            // 
            // mahalanobisDistanceToolStripMenuItem1
            // 
            this.mahalanobisDistanceToolStripMenuItem1.Name = "mahalanobisDistanceToolStripMenuItem1";
            this.mahalanobisDistanceToolStripMenuItem1.Size = new System.Drawing.Size(190, 22);
            this.mahalanobisDistanceToolStripMenuItem1.Text = "Mahalanobis Distance";
            this.mahalanobisDistanceToolStripMenuItem1.Click += new System.EventHandler(this.mahalanobisDistanceToolStripMenuItem_Click);
            // 
            // marginalManualClusteringToolStripMenuItem
            // 
            this.marginalManualClusteringToolStripMenuItem.Name = "marginalManualClusteringToolStripMenuItem";
            this.marginalManualClusteringToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.marginalManualClusteringToolStripMenuItem.Text = "Manual Hits Selection";
            this.marginalManualClusteringToolStripMenuItem.Click += new System.EventHandler(this.marginalManualClusteringToolStripMenuItem_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(187, 6);
            // 
            // hitsDistributionMapToolStripMenuItem
            // 
            this.hitsDistributionMapToolStripMenuItem.Name = "hitsDistributionMapToolStripMenuItem";
            this.hitsDistributionMapToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.hitsDistributionMapToolStripMenuItem.Text = "Hits Distribution Map";
            this.hitsDistributionMapToolStripMenuItem.Click += new System.EventHandler(this.hitsDistributionMapToolStripMenuItem_Click);
            // 
            // validationToolStripMenuItem1
            // 
            this.validationToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fTestToolStripMenuItem1,
            this.aNOVAToolStripMenuItem2,
            this.samplesTTestToolStripMenuItem1,
            this.studentTTestToolStripMenuItem});
            this.validationToolStripMenuItem1.Name = "validationToolStripMenuItem1";
            this.validationToolStripMenuItem1.Size = new System.Drawing.Size(190, 22);
            this.validationToolStripMenuItem1.Text = "Validation";
            // 
            // fTestToolStripMenuItem1
            // 
            this.fTestToolStripMenuItem1.Name = "fTestToolStripMenuItem1";
            this.fTestToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.fTestToolStripMenuItem1.Text = "F-Test";
            this.fTestToolStripMenuItem1.Click += new System.EventHandler(this.fTestToolStripMenuItem1_Click);
            // 
            // aNOVAToolStripMenuItem2
            // 
            this.aNOVAToolStripMenuItem2.Name = "aNOVAToolStripMenuItem2";
            this.aNOVAToolStripMenuItem2.Size = new System.Drawing.Size(164, 22);
            this.aNOVAToolStripMenuItem2.Text = "ANOVA";
            this.aNOVAToolStripMenuItem2.Click += new System.EventHandler(this.aNOVAToolStripMenuItem2_Click);
            // 
            // samplesTTestToolStripMenuItem1
            // 
            this.samplesTTestToolStripMenuItem1.Name = "samplesTTestToolStripMenuItem1";
            this.samplesTTestToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.samplesTTestToolStripMenuItem1.Text = "2 Samples T-Test";
            this.samplesTTestToolStripMenuItem1.Click += new System.EventHandler(this.samplesTTestToolStripMenuItem1_Click);
            // 
            // studentTTestToolStripMenuItem
            // 
            this.studentTTestToolStripMenuItem.Enabled = false;
            this.studentTTestToolStripMenuItem.Name = "studentTTestToolStripMenuItem";
            this.studentTTestToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.studentTTestToolStripMenuItem.Text = "Student T-Test";
            this.studentTTestToolStripMenuItem.Click += new System.EventHandler(this.studentTTestToolStripMenuItem_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(174, 6);
            // 
            // groupingToolStripMenuItem
            // 
            this.groupingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wellsMergingToolsToolStripMenuItem1,
            this.resetGroupsToolStripMenuItem,
            this.toolStripSeparator5,
            this.replicateScatterPointsToolStripMenuItem});
            this.groupingToolStripMenuItem.Name = "groupingToolStripMenuItem";
            this.groupingToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.groupingToolStripMenuItem.Text = "Grouping";
            // 
            // wellsMergingToolsToolStripMenuItem1
            // 
            this.wellsMergingToolsToolStripMenuItem1.Name = "wellsMergingToolsToolStripMenuItem1";
            this.wellsMergingToolsToolStripMenuItem1.Size = new System.Drawing.Size(197, 22);
            this.wellsMergingToolsToolStripMenuItem1.Text = "Wells Merging Tool";
            this.wellsMergingToolsToolStripMenuItem1.Click += new System.EventHandler(this.wellsMergingToolsToolStripMenuItem1_Click);
            // 
            // resetGroupsToolStripMenuItem
            // 
            this.resetGroupsToolStripMenuItem.Name = "resetGroupsToolStripMenuItem";
            this.resetGroupsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.resetGroupsToolStripMenuItem.Text = "Reset Groups";
            this.resetGroupsToolStripMenuItem.Click += new System.EventHandler(this.resetGroupsToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(194, 6);
            // 
            // replicateScatterPointsToolStripMenuItem
            // 
            this.replicateScatterPointsToolStripMenuItem.Name = "replicateScatterPointsToolStripMenuItem";
            this.replicateScatterPointsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.replicateScatterPointsToolStripMenuItem.Text = "Replicate Scatter Points";
            this.replicateScatterPointsToolStripMenuItem.Click += new System.EventHandler(this.replicateScatterPointsToolStripMenuItem_Click);
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(174, 6);
            // 
            // dRCAnalysisToolStripMenuItem
            // 
            this.dRCAnalysisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dRCDesignerToolStripMenuItem});
            this.dRCAnalysisToolStripMenuItem.Name = "dRCAnalysisToolStripMenuItem";
            this.dRCAnalysisToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.dRCAnalysisToolStripMenuItem.Text = "DRC Analysis";
            // 
            // dRCDesignerToolStripMenuItem
            // 
            this.dRCDesignerToolStripMenuItem.Name = "dRCDesignerToolStripMenuItem";
            this.dRCDesignerToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.dRCDesignerToolStripMenuItem.Text = "DRC Designer";
            this.dRCDesignerToolStripMenuItem.Click += new System.EventHandler(this.dRCDesignerToolStripMenuItem_Click);
            // 
            // toolStripMenuItemGeneAnalysis
            // 
            this.toolStripMenuItemGeneAnalysis.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findGeneToolStripMenuItem,
            this.pahtwaysAnalysisToolStripMenuItem,
            this.findPathwayToolStripMenuItem});
            this.toolStripMenuItemGeneAnalysis.Enabled = false;
            this.toolStripMenuItemGeneAnalysis.Name = "toolStripMenuItemGeneAnalysis";
            this.toolStripMenuItemGeneAnalysis.Size = new System.Drawing.Size(113, 23);
            this.toolStripMenuItemGeneAnalysis.Text = "Genomic Analysis";
            // 
            // findGeneToolStripMenuItem
            // 
            this.findGeneToolStripMenuItem.Name = "findGeneToolStripMenuItem";
            this.findGeneToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F)));
            this.findGeneToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.findGeneToolStripMenuItem.Text = "Find Gene";
            this.findGeneToolStripMenuItem.Click += new System.EventHandler(this.findGeneToolStripMenuItem_Click);
            // 
            // pahtwaysAnalysisToolStripMenuItem
            // 
            this.pahtwaysAnalysisToolStripMenuItem.Name = "pahtwaysAnalysisToolStripMenuItem";
            this.pahtwaysAnalysisToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.pahtwaysAnalysisToolStripMenuItem.Text = "Pathways analysis";
            this.pahtwaysAnalysisToolStripMenuItem.ToolTipText = "Compute Pathway redundancies among one class ";
            this.pahtwaysAnalysisToolStripMenuItem.Click += new System.EventHandler(this.pahtwaysAnalysisToolStripMenuItem_Click);
            // 
            // findPathwayToolStripMenuItem
            // 
            this.findPathwayToolStripMenuItem.Enabled = false;
            this.findPathwayToolStripMenuItem.Name = "findPathwayToolStripMenuItem";
            this.findPathwayToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.findPathwayToolStripMenuItem.Text = "Find Pathway";
            // 
            // betaToolStripMenuItem
            // 
            this.betaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dRCAnalysisToolStripMenuItem2,
            this.distributionsToolStripMenuItem,
            this.toolStripMenuItem3,
            this.newOptionMenuToolStripMenuItem,
            this.testDisplayToolStripMenuItem,
            this.wellsMergingToolStripMenuItem,
            this.dRC3DToolStripMenuItem});
            this.betaToolStripMenuItem.Name = "betaToolStripMenuItem";
            this.betaToolStripMenuItem.Size = new System.Drawing.Size(42, 23);
            this.betaToolStripMenuItem.Text = "Beta";
            this.betaToolStripMenuItem.ToolTipText = "At your own risk!!!";
            // 
            // dRCAnalysisToolStripMenuItem2
            // 
            this.dRCAnalysisToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertDRCToWellToolStripMenuItem1,
            this.toolStripSeparator14,
            this.currentPlate3DToolStripMenuItem});
            this.dRCAnalysisToolStripMenuItem2.Name = "dRCAnalysisToolStripMenuItem2";
            this.dRCAnalysisToolStripMenuItem2.Size = new System.Drawing.Size(181, 22);
            this.dRCAnalysisToolStripMenuItem2.Text = "DRC Analysis";
            // 
            // convertDRCToWellToolStripMenuItem1
            // 
            this.convertDRCToWellToolStripMenuItem1.Name = "convertDRCToWellToolStripMenuItem1";
            this.convertDRCToWellToolStripMenuItem1.Size = new System.Drawing.Size(249, 22);
            this.convertDRCToWellToolStripMenuItem1.Text = "Convert DRC To Well";
            this.convertDRCToWellToolStripMenuItem1.Click += new System.EventHandler(this.convertDRCToWellToolStripMenuItem1_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(246, 6);
            // 
            // currentPlate3DToolStripMenuItem
            // 
            this.currentPlate3DToolStripMenuItem.Name = "currentPlate3DToolStripMenuItem";
            this.currentPlate3DToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.currentPlate3DToolStripMenuItem.Text = "XYZ Scatter Pts with Connections";
            this.currentPlate3DToolStripMenuItem.Click += new System.EventHandler(this.currentPlate3DToolStripMenuItem_Click);
            // 
            // distributionsToolStripMenuItem
            // 
            this.distributionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.distributionsModeToolStripMenuItem,
            this.displayReferenceToolStripMenuItem});
            this.distributionsToolStripMenuItem.Name = "distributionsToolStripMenuItem";
            this.distributionsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.distributionsToolStripMenuItem.Text = "Histograms Analysis";
            // 
            // distributionsModeToolStripMenuItem
            // 
            this.distributionsModeToolStripMenuItem.CheckOnClick = true;
            this.distributionsModeToolStripMenuItem.Name = "distributionsModeToolStripMenuItem";
            this.distributionsModeToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.distributionsModeToolStripMenuItem.Text = "Histogram Mode";
            this.distributionsModeToolStripMenuItem.Click += new System.EventHandler(this.distributionsModeToolStripMenuItem_Click);
            // 
            // displayReferenceToolStripMenuItem
            // 
            this.displayReferenceToolStripMenuItem.Name = "displayReferenceToolStripMenuItem";
            this.displayReferenceToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.displayReferenceToolStripMenuItem.Text = "Display Reference";
            this.displayReferenceToolStripMenuItem.Click += new System.EventHandler(this.displayReferenceToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(181, 22);
            this.toolStripMenuItem3.Text = "Load FACS data";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // newOptionMenuToolStripMenuItem
            // 
            this.newOptionMenuToolStripMenuItem.Name = "newOptionMenuToolStripMenuItem";
            this.newOptionMenuToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.newOptionMenuToolStripMenuItem.Text = "NewOptionMenu";
            this.newOptionMenuToolStripMenuItem.Click += new System.EventHandler(this.newOptionMenuToolStripMenuItem_Click);
            // 
            // testDisplayToolStripMenuItem
            // 
            this.testDisplayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.heatMapToolStripMenuItem,
            this.testRStatsToolStripMenuItem,
            this.testBoxPlotToolStripMenuItem,
            this.testLinearRegressionToolStripMenuItem,
            this.testMultiScatterToolStripMenuItem,
            this.testPieChartToolStripMenuItem,
            this.testPPTToolStripMenuItem,
            this.testReplicateToolStripMenuItem,
            this.testPubMedSOAPToolStripMenuItem,
            this.sigmoidFittToolStripMenuItem,
            this.memoryTestToolStripMenuItem,
            this.mDBTestToolStripMenuItem,
            this.drawSingleDRCToolStripMenuItem,
            this.spiralToolStripMenuItem,
            this.simpleTestToolStripMenuItem,
            this.basic3DToolStripMenuItem});
            this.testDisplayToolStripMenuItem.Name = "testDisplayToolStripMenuItem";
            this.testDisplayToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.testDisplayToolStripMenuItem.Text = "Test Display";
            // 
            // heatMapToolStripMenuItem
            // 
            this.heatMapToolStripMenuItem.Name = "heatMapToolStripMenuItem";
            this.heatMapToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.heatMapToolStripMenuItem.Text = "Heat Map";
            this.heatMapToolStripMenuItem.Click += new System.EventHandler(this.heatMapToolStripMenuItem_Click);
            // 
            // testRStatsToolStripMenuItem
            // 
            this.testRStatsToolStripMenuItem.Name = "testRStatsToolStripMenuItem";
            this.testRStatsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.testRStatsToolStripMenuItem.Text = "Test-R-Stats";
            this.testRStatsToolStripMenuItem.Click += new System.EventHandler(this.testRStatsToolStripMenuItem_Click);
            // 
            // testBoxPlotToolStripMenuItem
            // 
            this.testBoxPlotToolStripMenuItem.Name = "testBoxPlotToolStripMenuItem";
            this.testBoxPlotToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.testBoxPlotToolStripMenuItem.Text = "Test BoxPlot";
            this.testBoxPlotToolStripMenuItem.Click += new System.EventHandler(this.testBoxPlotToolStripMenuItem_Click);
            // 
            // testLinearRegressionToolStripMenuItem
            // 
            this.testLinearRegressionToolStripMenuItem.Name = "testLinearRegressionToolStripMenuItem";
            this.testLinearRegressionToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.testLinearRegressionToolStripMenuItem.Text = "Test Linear Regression";
            this.testLinearRegressionToolStripMenuItem.Click += new System.EventHandler(this.testLinearRegressionToolStripMenuItem_Click);
            // 
            // testMultiScatterToolStripMenuItem
            // 
            this.testMultiScatterToolStripMenuItem.Name = "testMultiScatterToolStripMenuItem";
            this.testMultiScatterToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.testMultiScatterToolStripMenuItem.Text = "Test Multi Scatter";
            this.testMultiScatterToolStripMenuItem.Click += new System.EventHandler(this.testMultiScatterToolStripMenuItem_Click);
            // 
            // testPieChartToolStripMenuItem
            // 
            this.testPieChartToolStripMenuItem.Name = "testPieChartToolStripMenuItem";
            this.testPieChartToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.testPieChartToolStripMenuItem.Text = "Test PieChart";
            this.testPieChartToolStripMenuItem.Click += new System.EventHandler(this.testPieChartToolStripMenuItem_Click);
            // 
            // testPPTToolStripMenuItem
            // 
            this.testPPTToolStripMenuItem.Name = "testPPTToolStripMenuItem";
            this.testPPTToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.testPPTToolStripMenuItem.Text = "TestPPT";
            this.testPPTToolStripMenuItem.Click += new System.EventHandler(this.testPPTToolStripMenuItem_Click);
            // 
            // testReplicateToolStripMenuItem
            // 
            this.testReplicateToolStripMenuItem.Name = "testReplicateToolStripMenuItem";
            this.testReplicateToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.testReplicateToolStripMenuItem.Text = "TestReplicate";
            this.testReplicateToolStripMenuItem.Click += new System.EventHandler(this.testReplicateToolStripMenuItem_Click);
            // 
            // testPubMedSOAPToolStripMenuItem
            // 
            this.testPubMedSOAPToolStripMenuItem.Name = "testPubMedSOAPToolStripMenuItem";
            this.testPubMedSOAPToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.testPubMedSOAPToolStripMenuItem.Text = "TestPubMed SOAP";
            this.testPubMedSOAPToolStripMenuItem.Click += new System.EventHandler(this.testPubMedSOAPToolStripMenuItem_Click);
            // 
            // sigmoidFittToolStripMenuItem
            // 
            this.sigmoidFittToolStripMenuItem.Name = "sigmoidFittToolStripMenuItem";
            this.sigmoidFittToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.sigmoidFittToolStripMenuItem.Text = "SigmoidFitt";
            this.sigmoidFittToolStripMenuItem.Click += new System.EventHandler(this.sigmoidFittToolStripMenuItem_Click);
            // 
            // memoryTestToolStripMenuItem
            // 
            this.memoryTestToolStripMenuItem.Name = "memoryTestToolStripMenuItem";
            this.memoryTestToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.memoryTestToolStripMenuItem.Text = "Memory Test";
            this.memoryTestToolStripMenuItem.Click += new System.EventHandler(this.memoryTestToolStripMenuItem_Click);
            // 
            // mDBTestToolStripMenuItem
            // 
            this.mDBTestToolStripMenuItem.Name = "mDBTestToolStripMenuItem";
            this.mDBTestToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.mDBTestToolStripMenuItem.Text = "MDB test";
            this.mDBTestToolStripMenuItem.Click += new System.EventHandler(this.mDBTestToolStripMenuItem_Click);
            // 
            // drawSingleDRCToolStripMenuItem
            // 
            this.drawSingleDRCToolStripMenuItem.Name = "drawSingleDRCToolStripMenuItem";
            this.drawSingleDRCToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.drawSingleDRCToolStripMenuItem.Text = "DrawSingleDRC";
            this.drawSingleDRCToolStripMenuItem.Click += new System.EventHandler(this.drawSingleDRCToolStripMenuItem_Click);
            // 
            // spiralToolStripMenuItem
            // 
            this.spiralToolStripMenuItem.Name = "spiralToolStripMenuItem";
            this.spiralToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.spiralToolStripMenuItem.Text = "Test New Agents";
            this.spiralToolStripMenuItem.Click += new System.EventHandler(this.spiralToolStripMenuItem_Click);
            // 
            // simpleTestToolStripMenuItem
            // 
            this.simpleTestToolStripMenuItem.Name = "simpleTestToolStripMenuItem";
            this.simpleTestToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.simpleTestToolStripMenuItem.Text = "Simple Test";
            this.simpleTestToolStripMenuItem.Click += new System.EventHandler(this.simpleTestToolStripMenuItem_Click);
            // 
            // basic3DToolStripMenuItem
            // 
            this.basic3DToolStripMenuItem.Name = "basic3DToolStripMenuItem";
            this.basic3DToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.basic3DToolStripMenuItem.Text = "Basic3D";
            this.basic3DToolStripMenuItem.Click += new System.EventHandler(this.basic3DToolStripMenuItem_Click);
            // 
            // wellsMergingToolStripMenuItem
            // 
            this.wellsMergingToolStripMenuItem.Name = "wellsMergingToolStripMenuItem";
            this.wellsMergingToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.wellsMergingToolStripMenuItem.Text = "Wells Merging";
            this.wellsMergingToolStripMenuItem.Click += new System.EventHandler(this.wellsMergingToolStripMenuItem_Click);
            // 
            // dRC3DToolStripMenuItem
            // 
            this.dRC3DToolStripMenuItem.Name = "dRC3DToolStripMenuItem";
            this.dRC3DToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.dRC3DToolStripMenuItem.Text = "DRC3D";
            this.dRC3DToolStripMenuItem.Click += new System.EventHandler(this.dRC3DToolStripMenuItem_Click);
            // 
            // pluginsToolStripMenuItem
            // 
            this.pluginsToolStripMenuItem.Enabled = false;
            this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
            this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(63, 23);
            this.pluginsToolStripMenuItem.Text = "Plug-ins";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem,
            this.toolStripSeparator21,
            this.aboutHCSAnalyzerToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.viewHelpToolStripMenuItem.Text = "What\'s New";
            this.viewHelpToolStripMenuItem.Click += new System.EventHandler(this.viewHelpToolStripMenuItem_Click);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(176, 6);
            // 
            // aboutHCSAnalyzerToolStripMenuItem
            // 
            this.aboutHCSAnalyzerToolStripMenuItem.Image = global::HCSAnalyzer.Properties.Resources.help_about;
            this.aboutHCSAnalyzerToolStripMenuItem.Name = "aboutHCSAnalyzerToolStripMenuItem";
            this.aboutHCSAnalyzerToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.aboutHCSAnalyzerToolStripMenuItem.Text = "About HCS analyzer";
            this.aboutHCSAnalyzerToolStripMenuItem.Click += new System.EventHandler(this.aboutHCSAnalyzerToolStripMenuItem_Click);
            // 
            // checkedListBoxActiveDescriptors
            // 
            this.checkedListBoxActiveDescriptors.AllowDrop = true;
            this.checkedListBoxActiveDescriptors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxActiveDescriptors.CheckOnClick = true;
            this.checkedListBoxActiveDescriptors.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBoxActiveDescriptors.FormattingEnabled = true;
            this.checkedListBoxActiveDescriptors.Location = new System.Drawing.Point(3, 61);
            this.checkedListBoxActiveDescriptors.Name = "checkedListBoxActiveDescriptors";
            this.checkedListBoxActiveDescriptors.Size = new System.Drawing.Size(226, 381);
            this.checkedListBoxActiveDescriptors.TabIndex = 8;
            this.checkedListBoxActiveDescriptors.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxDescriptorActive_SelectedIndexChanged);
            this.checkedListBoxActiveDescriptors.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.checkedListBoxActiveDescriptors_MouseDoubleClick);
            this.checkedListBoxActiveDescriptors.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkedListBoxActiveDescriptors_MouseDown);
            // 
            // comboBoxDescriptorToDisplay
            // 
            this.comboBoxDescriptorToDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDescriptorToDisplay.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDescriptorToDisplay.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDescriptorToDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDescriptorToDisplay.FormattingEnabled = true;
            this.comboBoxDescriptorToDisplay.Location = new System.Drawing.Point(3, 19);
            this.comboBoxDescriptorToDisplay.Name = "comboBoxDescriptorToDisplay";
            this.comboBoxDescriptorToDisplay.Size = new System.Drawing.Size(226, 20);
            this.comboBoxDescriptorToDisplay.TabIndex = 9;
            this.comboBoxDescriptorToDisplay.SelectedIndexChanged += new System.EventHandler(this.comboBoxDescriptorToDisplay_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "Current Descriptor";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "Descriptor List";
            // 
            // contextMenuStripForLUT
            // 
            this.contextMenuStripForLUT.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStripForLUT.Name = "contextMenuStripForLUT";
            this.contextMenuStripForLUT.Size = new System.Drawing.Size(170, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItem1.Text = "Copy to clipboard";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMain.Location = new System.Drawing.Point(128, 3);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.tabControlMainView);
            this.splitContainerMain.Panel1.Controls.Add(this.buttonPreviousPlate);
            this.splitContainerMain.Panel1.Controls.Add(this.buttonNextPlate);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.checkedListBoxActiveDescriptors);
            this.splitContainerMain.Panel2.Controls.Add(this.label2);
            this.splitContainerMain.Panel2.Controls.Add(this.comboBoxDescriptorToDisplay);
            this.splitContainerMain.Panel2.Controls.Add(this.label8);
            this.splitContainerMain.Size = new System.Drawing.Size(1105, 449);
            this.splitContainerMain.SplitterDistance = 863;
            this.splitContainerMain.TabIndex = 35;
            // 
            // tabControlMainView
            // 
            this.tabControlMainView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMainView.Controls.Add(this.tabPageMainView);
            this.tabControlMainView.Controls.Add(this.tabPageDataView);
            this.tabControlMainView.Controls.Add(this.tabPage2DView);
            this.tabControlMainView.Controls.Add(this.tabPage1DView);
            this.tabControlMainView.Controls.Add(this.tabPage3DPlatesView);
            this.tabControlMainView.Location = new System.Drawing.Point(29, 3);
            this.tabControlMainView.Name = "tabControlMainView";
            this.tabControlMainView.SelectedIndex = 0;
            this.tabControlMainView.Size = new System.Drawing.Size(803, 443);
            this.tabControlMainView.TabIndex = 3;
            this.tabControlMainView.SelectedIndexChanged += new System.EventHandler(this.tabControlMainView_SelectedIndexChanged);
            // 
            // tabPageMainView
            // 
            this.tabPageMainView.Controls.Add(this.panelForPlate);
            this.tabPageMainView.Location = new System.Drawing.Point(4, 22);
            this.tabPageMainView.Name = "tabPageMainView";
            this.tabPageMainView.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMainView.Size = new System.Drawing.Size(795, 417);
            this.tabPageMainView.TabIndex = 0;
            this.tabPageMainView.Text = "Plate View";
            this.tabPageMainView.UseVisualStyleBackColor = true;
            // 
            // tabPageDataView
            // 
            this.tabPageDataView.Controls.Add(this.panelForTableView);
            this.tabPageDataView.Location = new System.Drawing.Point(4, 22);
            this.tabPageDataView.Name = "tabPageDataView";
            this.tabPageDataView.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDataView.Size = new System.Drawing.Size(795, 417);
            this.tabPageDataView.TabIndex = 1;
            this.tabPageDataView.Text = "DataView";
            this.tabPageDataView.UseVisualStyleBackColor = true;
            // 
            // panelForTableView
            // 
            this.panelForTableView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForTableView.Location = new System.Drawing.Point(0, 0);
            this.panelForTableView.Name = "panelForTableView";
            this.panelForTableView.Size = new System.Drawing.Size(795, 417);
            this.panelForTableView.TabIndex = 0;
            // 
            // tabPage2DView
            // 
            this.tabPage2DView.Location = new System.Drawing.Point(4, 22);
            this.tabPage2DView.Name = "tabPage2DView";
            this.tabPage2DView.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2DView.Size = new System.Drawing.Size(795, 417);
            this.tabPage2DView.TabIndex = 2;
            this.tabPage2DView.Text = "2D Scatter";
            this.tabPage2DView.UseVisualStyleBackColor = true;
            // 
            // tabPage1DView
            // 
            this.tabPage1DView.Location = new System.Drawing.Point(4, 22);
            this.tabPage1DView.Name = "tabPage1DView";
            this.tabPage1DView.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1DView.Size = new System.Drawing.Size(795, 417);
            this.tabPage1DView.TabIndex = 3;
            this.tabPage1DView.Text = "1D Scatter";
            this.tabPage1DView.UseVisualStyleBackColor = true;
            // 
            // tabPage3DPlatesView
            // 
            this.tabPage3DPlatesView.Location = new System.Drawing.Point(4, 22);
            this.tabPage3DPlatesView.Name = "tabPage3DPlatesView";
            this.tabPage3DPlatesView.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3DPlatesView.Size = new System.Drawing.Size(795, 417);
            this.tabPage3DPlatesView.TabIndex = 4;
            this.tabPage3DPlatesView.Text = "Screening View";
            this.tabPage3DPlatesView.UseVisualStyleBackColor = true;
            // 
            // buttonPreviousPlate
            // 
            this.buttonPreviousPlate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPreviousPlate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPreviousPlate.Location = new System.Drawing.Point(4, 3);
            this.buttonPreviousPlate.Name = "buttonPreviousPlate";
            this.buttonPreviousPlate.Size = new System.Drawing.Size(19, 443);
            this.buttonPreviousPlate.TabIndex = 2;
            this.buttonPreviousPlate.Text = "<";
            this.buttonPreviousPlate.UseVisualStyleBackColor = true;
            this.buttonPreviousPlate.Click += new System.EventHandler(this.buttonPreviousPlate_Click);
            // 
            // buttonNextPlate
            // 
            this.buttonNextPlate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNextPlate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNextPlate.Location = new System.Drawing.Point(839, 3);
            this.buttonNextPlate.Name = "buttonNextPlate";
            this.buttonNextPlate.Size = new System.Drawing.Size(19, 443);
            this.buttonNextPlate.TabIndex = 1;
            this.buttonNextPlate.Text = ">";
            this.buttonNextPlate.UseVisualStyleBackColor = true;
            this.buttonNextPlate.Click += new System.EventHandler(this.buttonNextPlate_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 55);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainerMain);
            this.splitContainer1.Panel1.Controls.Add(this.panelForTools);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlForScreening);
            this.splitContainer1.Panel2.Controls.Add(this.tabControlMain);
            this.splitContainer1.Size = new System.Drawing.Size(1236, 710);
            this.splitContainer1.SplitterDistance = 458;
            this.splitContainer1.TabIndex = 36;
            // 
            // tabControlForScreening
            // 
            this.tabControlForScreening.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlForScreening.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControlForScreening.Controls.Add(this.tabPageListWells);
            this.tabControlForScreening.Controls.Add(this.tabPageClassHistory);
            this.tabControlForScreening.Controls.Add(this.tabPageScreeningInfo);
            this.tabControlForScreening.Controls.Add(this.tabPageConsole);
            this.tabControlForScreening.Controls.Add(this.tabPage3DWorld);
            this.tabControlForScreening.Location = new System.Drawing.Point(880, 3);
            this.tabControlForScreening.Name = "tabControlForScreening";
            this.tabControlForScreening.SelectedIndex = 0;
            this.tabControlForScreening.Size = new System.Drawing.Size(353, 245);
            this.tabControlForScreening.TabIndex = 11;
            this.tabControlForScreening.SelectedIndexChanged += new System.EventHandler(this.tabControlForScreening_SelectedIndexChanged);
            // 
            // tabPageListWells
            // 
            this.tabPageListWells.Controls.Add(this.listViewForListWell);
            this.tabPageListWells.Location = new System.Drawing.Point(4, 25);
            this.tabPageListWells.Name = "tabPageListWells";
            this.tabPageListWells.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageListWells.Size = new System.Drawing.Size(345, 216);
            this.tabPageListWells.TabIndex = 0;
            this.tabPageListWells.Text = "List Wells";
            this.tabPageListWells.UseVisualStyleBackColor = true;
            // 
            // listViewForListWell
            // 
            this.listViewForListWell.AllowColumnReorder = true;
            this.listViewForListWell.AllowDrop = true;
            this.listViewForListWell.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewForListWell.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderConcentration,
            this.columnHeaderType,
            this.columnHeaderObjNumber});
            this.listViewForListWell.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewForListWell.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewForListWell.Location = new System.Drawing.Point(4, 4);
            this.listViewForListWell.MultiSelect = false;
            this.listViewForListWell.Name = "listViewForListWell";
            this.listViewForListWell.ShowItemToolTips = true;
            this.listViewForListWell.Size = new System.Drawing.Size(336, 209);
            this.listViewForListWell.TabIndex = 10;
            this.listViewForListWell.UseCompatibleStateImageBehavior = false;
            this.listViewForListWell.View = System.Windows.Forms.View.Details;
            this.listViewForListWell.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewForListWell_DragDrop);
            this.listViewForListWell.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewForListWell_DragEnter);
            this.listViewForListWell.DoubleClick += new System.EventHandler(this.listViewForListWell_DoubleClick);
            this.listViewForListWell.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewForListWell_KeyDown);
            this.listViewForListWell.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewForListWell_MouseDown);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 144;
            // 
            // columnHeaderConcentration
            // 
            this.columnHeaderConcentration.Text = "Concentration";
            this.columnHeaderConcentration.Width = 87;
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "Class";
            this.columnHeaderType.Width = 50;
            // 
            // columnHeaderObjNumber
            // 
            this.columnHeaderObjNumber.Text = "#Objects";
            // 
            // tabPageClassHistory
            // 
            this.tabPageClassHistory.Controls.Add(this.listViewClassHistory);
            this.tabPageClassHistory.Location = new System.Drawing.Point(4, 25);
            this.tabPageClassHistory.Name = "tabPageClassHistory";
            this.tabPageClassHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageClassHistory.Size = new System.Drawing.Size(345, 216);
            this.tabPageClassHistory.TabIndex = 1;
            this.tabPageClassHistory.Text = "History";
            this.tabPageClassHistory.UseVisualStyleBackColor = true;
            // 
            // listViewClassHistory
            // 
            this.listViewClassHistory.AllowColumnReorder = true;
            this.listViewClassHistory.AllowDrop = true;
            this.listViewClassHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewClassHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderHistoryName,
            this.columnHeaderHistoryDate});
            this.listViewClassHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewClassHistory.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewClassHistory.Location = new System.Drawing.Point(4, 4);
            this.listViewClassHistory.MultiSelect = false;
            this.listViewClassHistory.Name = "listViewClassHistory";
            this.listViewClassHistory.ShowItemToolTips = true;
            this.listViewClassHistory.Size = new System.Drawing.Size(336, 228);
            this.listViewClassHistory.TabIndex = 11;
            this.listViewClassHistory.UseCompatibleStateImageBehavior = false;
            this.listViewClassHistory.View = System.Windows.Forms.View.Details;
            this.listViewClassHistory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewClassHistory_KeyDown);
            this.listViewClassHistory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewClassHistory_MouseDoubleClick);
            this.listViewClassHistory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewClassHistory_MouseDown);
            // 
            // columnHeaderHistoryName
            // 
            this.columnHeaderHistoryName.Text = "Name";
            this.columnHeaderHistoryName.Width = 176;
            // 
            // columnHeaderHistoryDate
            // 
            this.columnHeaderHistoryDate.Text = "Date";
            this.columnHeaderHistoryDate.Width = 149;
            // 
            // tabPageScreeningInfo
            // 
            this.tabPageScreeningInfo.Controls.Add(this.richTextBoxForScreeningInformation);
            this.tabPageScreeningInfo.Location = new System.Drawing.Point(4, 25);
            this.tabPageScreeningInfo.Name = "tabPageScreeningInfo";
            this.tabPageScreeningInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScreeningInfo.Size = new System.Drawing.Size(345, 216);
            this.tabPageScreeningInfo.TabIndex = 2;
            this.tabPageScreeningInfo.Text = "Info";
            this.tabPageScreeningInfo.UseVisualStyleBackColor = true;
            // 
            // tabPageConsole
            // 
            this.tabPageConsole.Controls.Add(this.richTextBoxConsole);
            this.tabPageConsole.Location = new System.Drawing.Point(4, 25);
            this.tabPageConsole.Name = "tabPageConsole";
            this.tabPageConsole.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConsole.Size = new System.Drawing.Size(345, 216);
            this.tabPageConsole.TabIndex = 3;
            this.tabPageConsole.Text = "Console";
            this.tabPageConsole.UseVisualStyleBackColor = true;
            // 
            // richTextBoxConsole
            // 
            this.richTextBoxConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxConsole.Location = new System.Drawing.Point(6, 6);
            this.richTextBoxConsole.Name = "richTextBoxConsole";
            this.richTextBoxConsole.Size = new System.Drawing.Size(333, 226);
            this.richTextBoxConsole.TabIndex = 0;
            this.richTextBoxConsole.Text = "";
            // 
            // tabPage3DWorld
            // 
            this.tabPage3DWorld.Controls.Add(this.listView3DWorld);
            this.tabPage3DWorld.Location = new System.Drawing.Point(4, 25);
            this.tabPage3DWorld.Name = "tabPage3DWorld";
            this.tabPage3DWorld.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3DWorld.Size = new System.Drawing.Size(345, 216);
            this.tabPage3DWorld.TabIndex = 4;
            this.tabPage3DWorld.Text = "3D Worlds";
            this.tabPage3DWorld.UseVisualStyleBackColor = true;
            // 
            // listView3DWorld
            // 
            this.listView3DWorld.AllowColumnReorder = true;
            this.listView3DWorld.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView3DWorld.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3DWorldName,
            this.columnHeader3DWorldInfo});
            this.listView3DWorld.Location = new System.Drawing.Point(6, 3);
            this.listView3DWorld.MultiSelect = false;
            this.listView3DWorld.Name = "listView3DWorld";
            this.listView3DWorld.Size = new System.Drawing.Size(333, 219);
            this.listView3DWorld.TabIndex = 0;
            this.listView3DWorld.UseCompatibleStateImageBehavior = false;
            this.listView3DWorld.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3DWorldName
            // 
            this.columnHeader3DWorldName.Text = "Name";
            this.columnHeader3DWorldName.Width = 114;
            // 
            // columnHeader3DWorldInfo
            // 
            this.columnHeader3DWorldInfo.Text = "Info";
            // 
            // toolStripMain
            // 
            this.toolStripMain.AllowItemReorder = true;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator16,
            this.toolStripDropDownButtonApplyClass,
            this.toolStripDropDownButtonProcessMode});
            this.toolStripMain.Location = new System.Drawing.Point(0, 27);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(1236, 25);
            this.toolStripMain.TabIndex = 37;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButtonApplyClass
            // 
            this.toolStripDropDownButtonApplyClass.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButtonApplyClass.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.globalToolStripMenuItem,
            this.globalIfOnlyActiveToolStripMenuItem});
            this.toolStripDropDownButtonApplyClass.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonApplyClass.Image")));
            this.toolStripDropDownButtonApplyClass.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonApplyClass.Name = "toolStripDropDownButtonApplyClass";
            this.toolStripDropDownButtonApplyClass.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButtonApplyClass.Text = "Class Application";
            // 
            // globalToolStripMenuItem
            // 
            this.globalToolStripMenuItem.Name = "globalToolStripMenuItem";
            this.globalToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.globalToolStripMenuItem.Text = "Global";
            this.globalToolStripMenuItem.Click += new System.EventHandler(this.globalToolStripMenuItem_Click);
            // 
            // globalIfOnlyActiveToolStripMenuItem
            // 
            this.globalIfOnlyActiveToolStripMenuItem.Name = "globalIfOnlyActiveToolStripMenuItem";
            this.globalIfOnlyActiveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.globalIfOnlyActiveToolStripMenuItem.Text = "Global (only if active)";
            this.globalIfOnlyActiveToolStripMenuItem.Click += new System.EventHandler(this.globalIfOnlyActiveToolStripMenuItem_Click);
            // 
            // toolStripDropDownButtonProcessMode
            // 
            this.toolStripDropDownButtonProcessMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButtonProcessMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProcessModeCurrentPlateOnlyToolStripMenuItem,
            this.ProcessModeplateByPlateToolStripMenuItem,
            this.ProcessModeEntireScreeningToolStripMenuItem});
            this.toolStripDropDownButtonProcessMode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripDropDownButtonProcessMode.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonProcessMode.Image")));
            this.toolStripDropDownButtonProcessMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonProcessMode.Name = "toolStripDropDownButtonProcessMode";
            this.toolStripDropDownButtonProcessMode.Size = new System.Drawing.Size(89, 22);
            this.toolStripDropDownButtonProcessMode.Text = "Current Plate";
            // 
            // ProcessModeCurrentPlateOnlyToolStripMenuItem
            // 
            this.ProcessModeCurrentPlateOnlyToolStripMenuItem.AutoToolTip = true;
            this.ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked = true;
            this.ProcessModeCurrentPlateOnlyToolStripMenuItem.CheckOnClick = true;
            this.ProcessModeCurrentPlateOnlyToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ProcessModeCurrentPlateOnlyToolStripMenuItem.Name = "ProcessModeCurrentPlateOnlyToolStripMenuItem";
            this.ProcessModeCurrentPlateOnlyToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.ProcessModeCurrentPlateOnlyToolStripMenuItem.Text = "Current Plate";
            this.ProcessModeCurrentPlateOnlyToolStripMenuItem.ToolTipText = "Processes will by performed on the current plate only";
            this.ProcessModeCurrentPlateOnlyToolStripMenuItem.Click += new System.EventHandler(this.processModeToolStripMenuItem_Click);
            // 
            // ProcessModeplateByPlateToolStripMenuItem
            // 
            this.ProcessModeplateByPlateToolStripMenuItem.AutoToolTip = true;
            this.ProcessModeplateByPlateToolStripMenuItem.CheckOnClick = true;
            this.ProcessModeplateByPlateToolStripMenuItem.Name = "ProcessModeplateByPlateToolStripMenuItem";
            this.ProcessModeplateByPlateToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.ProcessModeplateByPlateToolStripMenuItem.Text = "Plate by Plate";
            this.ProcessModeplateByPlateToolStripMenuItem.ToolTipText = "Processes will be performed plate by plate";
            this.ProcessModeplateByPlateToolStripMenuItem.Click += new System.EventHandler(this.plateByPlateToolStripMenuItem_Click);
            // 
            // ProcessModeEntireScreeningToolStripMenuItem
            // 
            this.ProcessModeEntireScreeningToolStripMenuItem.AutoToolTip = true;
            this.ProcessModeEntireScreeningToolStripMenuItem.CheckOnClick = true;
            this.ProcessModeEntireScreeningToolStripMenuItem.Name = "ProcessModeEntireScreeningToolStripMenuItem";
            this.ProcessModeEntireScreeningToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.ProcessModeEntireScreeningToolStripMenuItem.Text = "Entire Screening";
            this.ProcessModeEntireScreeningToolStripMenuItem.ToolTipText = "Processes will be performed on the overvall screening";
            this.ProcessModeEntireScreeningToolStripMenuItem.Click += new System.EventHandler(this.entireScreeningToolStripMenuItem_Click);
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButtonDisplayMode});
            this.statusStripMain.Location = new System.Drawing.Point(0, 768);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(1236, 22);
            this.statusStripMain.TabIndex = 38;
            // 
            // toolStripDropDownButtonDisplayMode
            // 
            this.toolStripDropDownButtonDisplayMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButtonDisplayMode.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonDisplayMode.Image")));
            this.toolStripDropDownButtonDisplayMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonDisplayMode.Name = "toolStripDropDownButtonDisplayMode";
            this.toolStripDropDownButtonDisplayMode.Size = new System.Drawing.Size(92, 20);
            this.toolStripDropDownButtonDisplayMode.Text = "Display Mode";
            this.toolStripDropDownButtonDisplayMode.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStripDropDownButtonDisplayMode_MouseDown);
            // 
            // HCSAnalyzer
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 790);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStripFile);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStripFile;
            this.Name = "HCSAnalyzer";
            this.Text = "HCS analyzer v1.3";
            this.Load += new System.EventHandler(this.HCSAnalyzer_Load);
            this.Shown += new System.EventHandler(this.HCSAnalyzer_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.HCSAnalyzer_DragDrop);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HCSAnalyzer_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HCSAnalyzer_KeyPress);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.panelForPlate_MouseWheel);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageDImRed.ResumeLayout(false);
            this.tabPageDImRed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNewDimension)).EndInit();
            this.groupBoxUnsupervised.ResumeLayout(false);
            this.groupBoxSupervised.ResumeLayout(false);
            this.groupBoxSupervised.PerformLayout();
            this.tabPageQualityQtrl.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRejectionThreshold)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tabPageNormalization.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.tabPageClassification.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.tabPageExport.ResumeLayout(false);
            this.tabPageExport.PerformLayout();
            this.panelForTools.ResumeLayout(false);
            this.groupBoxQC.ResumeLayout(false);
            this.groupBoxQC.PerformLayout();
            this.contextMenuStripForQC.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panelForPlate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.menuStripFile.ResumeLayout(false);
            this.menuStripFile.PerformLayout();
            this.contextMenuStripForLUT.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.tabControlMainView.ResumeLayout(false);
            this.tabPageMainView.ResumeLayout(false);
            this.tabPageDataView.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControlForScreening.ResumeLayout(false);
            this.tabPageListWells.ResumeLayout(false);
            this.tabPageClassHistory.ResumeLayout(false);
            this.tabPageScreeningInfo.ResumeLayout(false);
            this.tabPageConsole.ResumeLayout(false);
            this.tabPage3DWorld.ResumeLayout(false);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripFile;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        public System.Windows.Forms.CheckedListBox checkedListBoxActiveDescriptors;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currentPlateTomtrToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAverageValuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAverageValuesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem platesManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveScreentoCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem applySelectionToScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutHCSAnalyzerToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageNormalization;
        private System.Windows.Forms.TabPage tabPageClassification;
        private System.Windows.Forms.TabPage tabPageExport;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonCorrectionPlateByPlate;
        private System.Windows.Forms.Button buttonNormalize;
        private System.Windows.Forms.Button buttonStartClassification;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.ToolStripMenuItem linkToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageQualityQtrl;
        private System.Windows.Forms.Panel panelForTools;
        private System.Windows.Forms.ToolStripMenuItem appendDescriptorsToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBoxForScreeningInformation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.CheckBox checkBoxExportPlateFormat;
        private System.Windows.Forms.CheckBox checkBoxExportFullScreen;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ImageList imageListForTab;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonReduceDim;
        private System.Windows.Forms.GroupBox groupBoxUnsupervised;
        private System.Windows.Forms.NumericUpDown numericUpDownNewDimension;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPageDImRed;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.RichTextBox richTextBoxInfoClustering;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.RichTextBox richTextBoxInfoClassif;
        private System.Windows.Forms.ComboBox comboBoxCLassificationMethod;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBoxSupervised;
        private System.Windows.Forms.ComboBox comboBoxReduceDimMultiClass;
        private System.Windows.Forms.ComboBox comboBoxReduceDimSingleClass;
        private System.Windows.Forms.RichTextBox richTextBoxSupervisedDimRec;
        private System.Windows.Forms.RichTextBox richTextBoxUnsupervisedDimRec;
        private System.Windows.Forms.RadioButton radioButtonDimRedSupervised;
        private System.Windows.Forms.RadioButton radioButtonDimRedUnsupervised;
        private System.Windows.Forms.ComboBox comboBoxDimReductionNeutralClass;
        private System.Windows.Forms.ComboBox comboBoxNeutralClassForClassif;
        private System.Windows.Forms.ComboBox comboBoxMethodForCorrection;
        private System.Windows.Forms.RichTextBox richTextBoxInformationForPlateCorrection;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.RichTextBox richTextBoxInfoForNormalization;
        private System.Windows.Forms.ComboBox comboBoxMethodForNormalization;
        private System.Windows.Forms.ComboBox comboBoxNormalizationPositiveCtrl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxNormalizationNegativeCtrl;
        private System.Windows.Forms.Label label4;
        private Label label8;
        private ToolStripMenuItem swapClassesToolStripMenuItem;
        public ToolStripComboBox toolStripcomboBoxPlateList;
        private Button buttonRejectPlates;
        private GroupBox groupBox1;
        private RichTextBox richTextBoxInformationRejection;
        private ComboBox comboBoxRejection;
        private NumericUpDown numericUpDownRejectionThreshold;
        private Label label9;
        private TreeView treeViewSelectionForExport;
        private ToolStripMenuItem generateScreenToolStripMenuItem1;
        private ToolStripMenuItem univariateToolStripMenuItem;
        private ToolStripMenuItem multivariateToolStripMenuItem;
        private ContextMenuStrip contextMenuStripForLUT;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem pluginsToolStripMenuItem;
        private ToolStripMenuItem toARFFToolStripMenuItem;
        private ComboBox comboBoxRejectionPositiveCtrl;
        private Label label11;
        private ComboBox comboBoxRejectionNegativeCtrl;
        private Label label12;
        private ToolStripMenuItem betaToolStripMenuItem;
        private ToolStripMenuItem dRCAnalysisToolStripMenuItem2;
        private ToolStripMenuItem convertDRCToWellToolStripMenuItem1;
        private ToolStripMenuItem distributionsToolStripMenuItem;
        private ToolStripMenuItem distributionsModeToolStripMenuItem;
        private ToolStripMenuItem displayReferenceToolStripMenuItem;
        public TabControl tabControlMain;
        private SplitContainer splitContainerMain;
        public Panel panelForPlate;
        private Button buttonNextPlate;
        private Button buttonPreviousPlate;
        private ToolStripMenuItem visualizationToolStripMenuItem;
        private ToolStripMenuItem distanceMatrixToolStripMenuItem;
        private ToolStripMenuItem StatisticsToolStripMenuItem;
        private ToolStripMenuItem qualityControlToolStripMenuItem;
        private ToolStripMenuItem sSMDToolStripMenuItem;
        private ToolStripMenuItem correlationMatrixToolStripMenuItem;
        private ToolStripMenuItem descriptorEvolutionToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItemGeneAnalysis;
        private ToolStripMenuItem findGeneToolStripMenuItem;
        private ToolStripMenuItem pahtwaysAnalysisToolStripMenuItem;
        private ToolStripMenuItem findPathwayToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem classesDistributionToolStripMenuItem;
        private ToolStripMenuItem hierarchicalTreeToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem plateViewToolStripMenuItem;
        private ToolStripMenuItem descriptorViewToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem pieViewToolStripMenuItem1;
        public ToolStripMenuItem ThreeDVisualizationToolStripMenuItem;
        private ToolStripMenuItem cellByCellToolStripMenuItem;
        private ToolStripMenuItem loadDBToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator12;
        private ToolStripMenuItem classViewToolStripMenuItem;
        private ToolStripMenuItem averageViewToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator13;
        private ToolStripMenuItem histogramViewToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator14;
        private ToolStripMenuItem currentPlate3DToolStripMenuItem;
        private ToolStripMenuItem newOptionMenuToolStripMenuItem;
        private ToolTip toolTip;
        private ToolStripMenuItem singleCellsSimulatorToolStripMenuItem;
        private Button ButtonClustering;
        private Button buttonNewClassificationProcess;
        private Panel panelTMPForFeedBack;
        private SplitContainer splitContainer1;
        private ToolStripMenuItem testDisplayToolStripMenuItem;
        private ToolStripMenuItem heatMapToolStripMenuItem;
        private ToolStripMenuItem createAveragePlateToolStripMenuItem;
        private ToolStripMenuItem testRStatsToolStripMenuItem;
        private ToolStrip toolStripMain;
        private ToolStripDropDownButton toolStripDropDownButtonApplyClass;
        private ToolStripMenuItem globalToolStripMenuItem;
        private ToolStripMenuItem globalIfOnlyActiveToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator16;
        public ToolStripMenuItem ProcessModeplateByPlateToolStripMenuItem;
        public ToolStripMenuItem ProcessModeEntireScreeningToolStripMenuItem;
        public ComboBox comboBoxDescriptorToDisplay;
        private ToolStripMenuItem testBoxPlotToolStripMenuItem;
        private ToolStripMenuItem qualityControlsToolStripMenuItem;
        private ToolStripMenuItem zScoreToolStripMenuItem;
        private ToolStripMenuItem normalProbabilityPlotToolStripMenuItem2;
        private ToolStripMenuItem systematicErrorsToolStripMenuItem;
        private ToolStripMenuItem correlationAnalysisToolStripMenuItem;
        private ToolStripMenuItem aToolStripMenuItem;
        private ToolStripMenuItem correlationMatrixToolStripMenuItem1;
        private ToolStripMenuItem ftestdescBasedToolStripMenuItem;
        private ToolStripMenuItem stackedHistogramsToolStripMenuItem;
        public ComboBox comboBoxClass;
        private ToolStripMenuItem testLinearRegressionToolStripMenuItem;
        private ToolStripMenuItem covarianceMatrixToolStripMenuItem;
        private ToolStripMenuItem projectionsToolStripMenuItem1;
        private ToolStripMenuItem pCAToolStripMenuItem1;
        private ToolStripMenuItem lDAToolStripMenuItem;
        private ToolStripMenuItem testMultiScatterToolStripMenuItem;
        public ToolStripDropDownButton toolStripDropDownButtonProcessMode;
        private ToolStripMenuItem statisticsToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator17;
        private ToolStripMenuItem loadExtendedTableToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItemLoadImage;
        private ToolStripMenuItem testPieChartToolStripMenuItem;
        private ToolStripMenuItem testPPTToolStripMenuItem;
        public ListView listViewForListWell;
        private ColumnHeader columnHeaderConcentration;
        private ColumnHeader columnHeaderType;
        private ColumnHeader columnHeaderName;
        private ToolStripMenuItem testReplicateToolStripMenuItem;
        private TabControl tabControlForScreening;
        private TabPage tabPageListWells;
        private TabPage tabPageClassHistory;
        public ListView listViewClassHistory;
        private ColumnHeader columnHeaderHistoryName;
        private ColumnHeader columnHeaderHistoryDate;
        private TabPage tabPageScreeningInfo;
        private TabPage tabPageConsole;
        public RichTextBox richTextBoxConsole;
        private ToolStripMenuItem wellsMergingToolStripMenuItem;
        private ToolStripMenuItem dRC3DToolStripMenuItem;
        private ToolStripMenuItem testPubMedSOAPToolStripMenuItem;
        private ColumnHeader columnHeaderObjNumber;
        private ToolStripMenuItem tTestToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripMenuItem mannWithneyTestToolStripMenuItem;
        private ToolStripMenuItem histogramsToolStripMenuItem;
        private ToolStripMenuItem aNOVAToolStripMenuItem1;
        private ToolStripMenuItem sigmoidFittToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripMenuItem toolStripMenuItemDescManagement;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem hitsIdentificationToolStripMenuItem;
        private ToolStripMenuItem mahalanobisDistanceToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator15;
        private ToolStripMenuItem hitsDistributionMapToolStripMenuItem;
        private ToolStripMenuItem marginalManualClusteringToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator18;
        private ToolStripMenuItem validationToolStripMenuItem1;
        private ToolStripMenuItem fTestToolStripMenuItem1;
        private ToolStripMenuItem aNOVAToolStripMenuItem2;
        private ToolStripMenuItem samplesTTestToolStripMenuItem1;
        private ToolStripMenuItem studentTTestToolStripMenuItem;
        private ToolStripMenuItem groupingToolStripMenuItem;
        private ToolStripMenuItem wellsMergingToolsToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator20;
        private ToolStripMenuItem dRCAnalysisToolStripMenuItem;
        private ToolStripMenuItem dRCDesignerToolStripMenuItem;
        public ToolStripMenuItem ProcessModeCurrentPlateOnlyToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem replicateScatterPointsToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItemLoadProperty;
        private GroupBox groupBox3;
        public StatusStrip statusStripMain;
        public ToolStripDropDownButton toolStripDropDownButtonDisplayMode;
        public Label labelNumClasses;
        private ToolStripMenuItem imageViewToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripMenuItem fromImagesToolStripMenuItem;
        private ToolStripMenuItem memoryTestToolStripMenuItem;
        private ToolStripMenuItem mDBTestToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripMenuItem exportAsCSVToolStripMenuItem;
        private ToolStripMenuItem scatterPlotsToolStripMenuItem;
        private ToolStripMenuItem OneDScatterToolStripMenuItem;
        private ToolStripMenuItem dToolStripMenuItemScatterPlot2D;
        private ToolStripMenuItem dToolStripMenuItemScatterPlot3D;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem dDisplayToolStripMenuItem;
        private ToolStripMenuItem viewHelpToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator21;
        private ToolStripSeparator toolStripSeparator22;
        private ToolStripMenuItem importScreenDirectoryToolStripMenuItem;
        private ToolStripMenuItem resetGroupsToolStripMenuItem;
        private ToolStripMenuItem drawSingleDRCToolStripMenuItem;
        private ToolStripMenuItem spiralToolStripMenuItem;
        private TabPage tabPage3DWorld;
        public ListView listView3DWorld;
        private ColumnHeader columnHeader3DWorldName;
        private ColumnHeader columnHeader3DWorldInfo;
        private ToolStripMenuItem simpleTestToolStripMenuItem;
        private ToolStripMenuItem imageToolStripMenuItem;
        private ToolStripSeparator toolStripSeparatorPaste;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem basic3DToolStripMenuItem;
        private TabPage tabPageMainView;
        private TabPage tabPageDataView;
        public Panel panelForTableView;
        public TabControl tabControlMainView;
        public TabPage tabPage2DView;
        public TabPage tabPage1DView;
        public TabPage tabPage3DPlatesView;
        private GroupBox groupBoxQC;
        public ComboBox comboBoxQC;
        public Label labelQC;
        private ContextMenuStrip contextMenuStripForQC;
        private ToolStripMenuItem defineClassesToolStripMenuItem;
        public ToolStripMenuItem singleCellToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator19;
        private ToolStripMenuItem buildDatabaseToolStripMenuItem;
        private ToolStripMenuItem cSVDBToolStripMenuItem;
        private ToolStripMenuItem harmonyDBToolStripMenuItem;
    }
}

