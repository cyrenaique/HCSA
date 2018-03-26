﻿namespace HCSAnalyzer
{
    partial class FormForOptionsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("1D");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("2D");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("3D");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Scatter Plots", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("DRC");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Curves", new System.Windows.Forms.TreeNode[] {
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Histograms");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Z\' graphs");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("QC", new System.Windows.Forms.TreeNode[] {
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("HTML export");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Graphs", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode6,
            treeNode7,
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Plate Design");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("World Defaults");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("3D Engine", new System.Windows.Forms.TreeNode[] {
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Image Viewer");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Well");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Screening", new System.Windows.Forms.TreeNode[] {
            treeNode16});
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Harmony");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Cellomics");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("ImageXPress");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("PlateForm", new System.Windows.Forms.TreeNode[] {
            treeNode18,
            treeNode19,
            treeNode20});
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Single Cell Options");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForOptionsWindow));
            this.tabControlWindowOption = new System.Windows.Forms.TabControl();
            this.tabPageImport = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxSetActiveOnlyForNamed = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonWellPosModeDouble = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonWellPosModeSingle = new System.Windows.Forms.RadioButton();
            this.tabPageDisplay = new System.Windows.Forms.TabPage();
            this.panelForSpecificOption = new System.Windows.Forms.Panel();
            this.treeViewOptions = new System.Windows.Forms.TreeView();
            this.tabPageClustering = new System.Windows.Forms.TabPage();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.comboBoxHierarchicalLinkType = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.comboBoxHierarchicalDistance = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tabPageClassification = new System.Windows.Forms.TabPage();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.numericUpDownJ48MinNumObjects = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.numericUpDownKofKNN = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.checkBoxCorrelationRankChangeColorForActiveDesc = new System.Windows.Forms.CheckBox();
            this.checkBoxCorrelationMatrixDisplayRanking = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonMIC = new System.Windows.Forms.RadioButton();
            this.radioButtonSpearman = new System.Windows.Forms.RadioButton();
            this.radioButtonPearson = new System.Windows.Forms.RadioButton();
            this.tabPageMiscInfo = new System.Windows.Forms.TabPage();
            this.label42 = new System.Windows.Forms.Label();
            this.richTextBoxSystemInfo = new System.Windows.Forms.RichTextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.radioButtonGoogle = new System.Windows.Forms.RadioButton();
            this.radioButtonIE = new System.Windows.Forms.RadioButton();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.numericUpDownEdgeEffectDeltaMult = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownEdgeEffectMaxMult = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownEdgeEffectDeltaShift = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownEdgeEffectMinMult = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownEdgeEffectMaxShift = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.numericUpDownEdgeEffectMinShift = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.numericUpDownEdgeEffectMaximumIteration = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.numericUpDownSystErrorIdentKMeansClasses = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownSystemMinWellRatio = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSystErrorAndersonDarlingThreshold = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.numericUpDownGenerateScreenDiffusion = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.numericUpDownGenerateScreenRatioXY = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDownGenerateScreenRowEffectShift = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.numericUpDownGenerateScreenNoiseStdDev = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox25 = new System.Windows.Forms.GroupBox();
            this.checkBoxConnectDRCPts = new System.Windows.Forms.CheckBox();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.radioButtonReplicatesAverageStdev = new System.Windows.Forms.RadioButton();
            this.radioButtonReplicateAllValues = new System.Windows.Forms.RadioButton();
            this.buttonDRCPlateDesign = new System.Windows.Forms.Button();
            this.tabPage3D = new System.Windows.Forms.TabPage();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.checkBox3DPlateInformation = new System.Windows.Forms.CheckBox();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.checkBox3DComputeThinPlate = new System.Windows.Forms.CheckBox();
            this.numericUpDown3DThinPlateRegularization = new System.Windows.Forms.NumericUpDown();
            this.label28 = new System.Windows.Forms.Label();
            this.checkBox3DDisplayThinPlate = new System.Windows.Forms.CheckBox();
            this.checkBox3DDisplayIsoRatioCurves = new System.Windows.Forms.CheckBox();
            this.checkBox3DDisplayIsoboles = new System.Windows.Forms.CheckBox();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.numericUpDownDRCOpacity = new System.Windows.Forms.NumericUpDown();
            this.label26 = new System.Windows.Forms.Label();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.numericUpDownWellSize = new System.Windows.Forms.NumericUpDown();
            this.label27 = new System.Windows.Forms.Label();
            this.numericUpDownWellOpacity = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.label31 = new System.Windows.Forms.Label();
            this.groupBox26 = new System.Windows.Forms.GroupBox();
            this.radioButtonHistoDisplayAdjusted = new System.Windows.Forms.RadioButton();
            this.numericUpDownManualMax = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownManualMin = new System.Windows.Forms.NumericUpDown();
            this.label33 = new System.Windows.Forms.Label();
            this.numericUpDownAutomatedMax = new System.Windows.Forms.NumericUpDown();
            this.label32 = new System.Windows.Forms.Label();
            this.numericUpDownAutomatedMin = new System.Windows.Forms.NumericUpDown();
            this.radioButtonHistoDisplayManualMinMax = new System.Windows.Forms.RadioButton();
            this.radioButtonHistoDisplayAutomatedMinMax = new System.Windows.Forms.RadioButton();
            this.groupBox24 = new System.Windows.Forms.GroupBox();
            this.radioButtonDistributionMetricEMD = new System.Windows.Forms.RadioButton();
            this.radioButtonDistributionMetricBhattacharyya = new System.Windows.Forms.RadioButton();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.radioButtonDistributionMetricCosine = new System.Windows.Forms.RadioButton();
            this.radioButtonDistributionMetricManhattan = new System.Windows.Forms.RadioButton();
            this.radioButtonDistributionMetricEuclidean = new System.Windows.Forms.RadioButton();
            this.tabPageClasses = new System.Windows.Forms.TabPage();
            this.groupBoxPhenotypes = new System.Windows.Forms.GroupBox();
            this.panelForCellularPhenotypes = new System.Windows.Forms.Panel();
            this.groupBox28 = new System.Windows.Forms.GroupBox();
            this.panelForWellClasses = new System.Windows.Forms.Panel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panelImageAccess = new System.Windows.Forms.Panel();
            this.panelForOptionsImagingPlateform = new System.Windows.Forms.Panel();
            this.treeViewForChoice = new System.Windows.Forms.TreeView();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.panelBoundingBox = new System.Windows.Forms.Panel();
            this.comboBoxDescForBoundingMaxX = new System.Windows.Forms.ComboBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.comboBoxDescForBoundingMaxY = new System.Windows.Forms.ComboBox();
            this.comboBoxDescForBoundingMinX = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.comboBoxDescForBoundingMinY = new System.Windows.Forms.ComboBox();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.comboBoxDescriptorForPosX = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.comboBoxDescriptorForPosY = new System.Windows.Forms.ComboBox();
            this.radioButtonObjPosBoundingBox = new System.Windows.Forms.RadioButton();
            this.radioButtonObjPosCenter = new System.Windows.Forms.RadioButton();
            this.comboBoxDescriptorForField = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.radioButtonImageAccessManual = new System.Windows.Forms.RadioButton();
            this.radioButtonImageAccessDefined = new System.Windows.Forms.RadioButton();
            this.groupBoxManual = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBoxDefined = new System.Windows.Forms.GroupBox();
            this.radioButtonImageAccessBuiltIn = new System.Windows.Forms.RadioButton();
            this.radioButtonImageAccessCV7000 = new System.Windows.Forms.RadioButton();
            this.radioButtonImageAccessINCell = new System.Windows.Forms.RadioButton();
            this.radioButtonImageAccessCellomics = new System.Windows.Forms.RadioButton();
            this.label35 = new System.Windows.Forms.Label();
            this.numericUpDownDefaultField = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownImageAccessNumberOfFields = new System.Windows.Forms.NumericUpDown();
            this.radioButtonImageAccessHarmony35 = new System.Windows.Forms.RadioButton();
            this.radioButtonImageAccessImageXpress = new System.Windows.Forms.RadioButton();
            this.buttonUpdateImagePath = new System.Windows.Forms.Button();
            this.textBoxImageAccesImagePath = new System.Windows.Forms.TextBox();
            this.radioButtonImageAccessHarmony = new System.Windows.Forms.RadioButton();
            this.labelPath = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.numericUpDownImageAccessNumberOfChannels = new System.Windows.Forms.NumericUpDown();
            this.tabPageImageDispProperties = new System.Windows.Forms.TabPage();
            this.panelForCurrentLUTList = new System.Windows.Forms.Panel();
            this.contextMenuStripForImageDisplay = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonOk = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.buttonApply = new System.Windows.Forms.Button();
            this.menuStripIO = new System.Windows.Forms.MenuStrip();
            this.iOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlWindowOption.SuspendLayout();
            this.tabPageImport.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageDisplay.SuspendLayout();
            this.tabPageClustering.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.tabPageClassification.SuspendLayout();
            this.groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJ48MinNumObjects)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKofKNN)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPageMiscInfo.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeEffectDeltaMult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeEffectMaxMult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeEffectDeltaShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeEffectMinMult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeEffectMaxShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeEffectMinShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeEffectMaximumIteration)).BeginInit();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSystErrorIdentKMeansClasses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSystemMinWellRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSystErrorAndersonDarlingThreshold)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGenerateScreenDiffusion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGenerateScreenRatioXY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGenerateScreenRowEffectShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGenerateScreenNoiseStdDev)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.groupBox25.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.tabPage3D.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.groupBox22.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3DThinPlateRegularization)).BeginInit();
            this.groupBox20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDRCOpacity)).BeginInit();
            this.groupBox21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWellSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWellOpacity)).BeginInit();
            this.tabPage8.SuspendLayout();
            this.groupBox26.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownManualMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownManualMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutomatedMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutomatedMin)).BeginInit();
            this.groupBox24.SuspendLayout();
            this.tabPageClasses.SuspendLayout();
            this.groupBoxPhenotypes.SuspendLayout();
            this.groupBox28.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panelImageAccess.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.panelBoundingBox.SuspendLayout();
            this.panelCenter.SuspendLayout();
            this.groupBoxManual.SuspendLayout();
            this.groupBoxDefined.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDefaultField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageAccessNumberOfFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageAccessNumberOfChannels)).BeginInit();
            this.tabPageImageDispProperties.SuspendLayout();
            this.contextMenuStripForImageDisplay.SuspendLayout();
            this.menuStripIO.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlWindowOption
            // 
            this.tabControlWindowOption.Controls.Add(this.tabPageImport);
            this.tabControlWindowOption.Controls.Add(this.tabPageDisplay);
            this.tabControlWindowOption.Controls.Add(this.tabPageClustering);
            this.tabControlWindowOption.Controls.Add(this.tabPageClassification);
            this.tabControlWindowOption.Controls.Add(this.tabPage1);
            this.tabControlWindowOption.Controls.Add(this.tabPageMiscInfo);
            this.tabControlWindowOption.Controls.Add(this.tabPage4);
            this.tabControlWindowOption.Controls.Add(this.tabPage5);
            this.tabControlWindowOption.Controls.Add(this.tabPage6);
            this.tabControlWindowOption.Controls.Add(this.tabPage3D);
            this.tabControlWindowOption.Controls.Add(this.tabPage8);
            this.tabControlWindowOption.Controls.Add(this.tabPageClasses);
            this.tabControlWindowOption.Controls.Add(this.tabPage3);
            this.tabControlWindowOption.Controls.Add(this.tabPageImageDispProperties);
            this.tabControlWindowOption.Location = new System.Drawing.Point(12, 27);
            this.tabControlWindowOption.Multiline = true;
            this.tabControlWindowOption.Name = "tabControlWindowOption";
            this.tabControlWindowOption.SelectedIndex = 0;
            this.tabControlWindowOption.Size = new System.Drawing.Size(404, 463);
            this.tabControlWindowOption.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControlWindowOption.TabIndex = 0;
            this.tabControlWindowOption.SelectedIndexChanged += new System.EventHandler(this.tabControlWindowOption_SelectedIndexChanged);
            // 
            // tabPageImport
            // 
            this.tabPageImport.Controls.Add(this.groupBox2);
            this.tabPageImport.Controls.Add(this.groupBox1);
            this.tabPageImport.Location = new System.Drawing.Point(4, 58);
            this.tabPageImport.Name = "tabPageImport";
            this.tabPageImport.Size = new System.Drawing.Size(396, 401);
            this.tabPageImport.TabIndex = 4;
            this.tabPageImport.Text = "Import / Export";
            this.tabPageImport.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxSetActiveOnlyForNamed);
            this.groupBox2.Location = new System.Drawing.Point(4, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(389, 62);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Import Names";
            // 
            // checkBoxSetActiveOnlyForNamed
            // 
            this.checkBoxSetActiveOnlyForNamed.AutoSize = true;
            this.checkBoxSetActiveOnlyForNamed.Location = new System.Drawing.Point(118, 29);
            this.checkBoxSetActiveOnlyForNamed.Name = "checkBoxSetActiveOnlyForNamed";
            this.checkBoxSetActiveOnlyForNamed.Size = new System.Drawing.Size(152, 17);
            this.checkBoxSetActiveOnlyForNamed.TabIndex = 0;
            this.checkBoxSetActiveOnlyForNamed.Text = "If not named, then inactive";
            this.checkBoxSetActiveOnlyForNamed.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.radioButtonWellPosModeDouble);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radioButtonWellPosModeSingle);
            this.groupBox1.Location = new System.Drawing.Point(4, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 95);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Well position mode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "1   1";
            // 
            // radioButtonWellPosModeDouble
            // 
            this.radioButtonWellPosModeDouble.AutoSize = true;
            this.radioButtonWellPosModeDouble.Location = new System.Drawing.Point(227, 58);
            this.radioButtonWellPosModeDouble.Name = "radioButtonWellPosModeDouble";
            this.radioButtonWellPosModeDouble.Size = new System.Drawing.Size(89, 17);
            this.radioButtonWellPosModeDouble.TabIndex = 2;
            this.radioButtonWellPosModeDouble.Text = "Double Mode";
            this.radioButtonWellPosModeDouble.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "A01";
            // 
            // radioButtonWellPosModeSingle
            // 
            this.radioButtonWellPosModeSingle.AutoSize = true;
            this.radioButtonWellPosModeSingle.Checked = true;
            this.radioButtonWellPosModeSingle.Location = new System.Drawing.Point(74, 58);
            this.radioButtonWellPosModeSingle.Name = "radioButtonWellPosModeSingle";
            this.radioButtonWellPosModeSingle.Size = new System.Drawing.Size(84, 17);
            this.radioButtonWellPosModeSingle.TabIndex = 0;
            this.radioButtonWellPosModeSingle.TabStop = true;
            this.radioButtonWellPosModeSingle.Text = "Single Mode";
            this.radioButtonWellPosModeSingle.UseVisualStyleBackColor = true;
            // 
            // tabPageDisplay
            // 
            this.tabPageDisplay.Controls.Add(this.panelForSpecificOption);
            this.tabPageDisplay.Controls.Add(this.treeViewOptions);
            this.tabPageDisplay.Location = new System.Drawing.Point(4, 58);
            this.tabPageDisplay.Name = "tabPageDisplay";
            this.tabPageDisplay.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDisplay.Size = new System.Drawing.Size(396, 401);
            this.tabPageDisplay.TabIndex = 0;
            this.tabPageDisplay.Text = "Display";
            this.tabPageDisplay.UseVisualStyleBackColor = true;
            // 
            // panelForSpecificOption
            // 
            this.panelForSpecificOption.AutoScroll = true;
            this.panelForSpecificOption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelForSpecificOption.Location = new System.Drawing.Point(6, 265);
            this.panelForSpecificOption.Name = "panelForSpecificOption";
            this.panelForSpecificOption.Size = new System.Drawing.Size(384, 130);
            this.panelForSpecificOption.TabIndex = 21;
            // 
            // treeViewOptions
            // 
            this.treeViewOptions.Location = new System.Drawing.Point(6, 6);
            this.treeViewOptions.Name = "treeViewOptions";
            treeNode1.Name = "NodeScatterPlot1D";
            treeNode1.Text = "1D";
            treeNode2.Name = "NodeScatterPlot2D";
            treeNode2.Text = "2D";
            treeNode3.Name = "NodeScatterPlot3D";
            treeNode3.Text = "3D";
            treeNode4.Name = "NodeScatterPlots";
            treeNode4.Text = "Scatter Plots";
            treeNode5.Name = "NodeCurvesDRC";
            treeNode5.Text = "DRC";
            treeNode6.Name = "NodeCurves";
            treeNode6.Text = "Curves";
            treeNode7.Name = "NodeHistograms";
            treeNode7.Text = "Histograms";
            treeNode8.Name = "ZscoreGraph";
            treeNode8.Text = "Z\' graphs";
            treeNode9.Name = "Node0";
            treeNode9.Text = "QC";
            treeNode10.Name = "NodeHTMLExport";
            treeNode10.Text = "HTML export";
            treeNode11.Name = "NodeGraphs";
            treeNode11.Text = "Graphs";
            treeNode12.Name = "PlateDesign";
            treeNode12.Text = "Plate Design";
            treeNode13.Name = "Node3DWorld";
            treeNode13.Text = "World Defaults";
            treeNode14.Name = "Node3D";
            treeNode14.Text = "3D Engine";
            treeNode15.Name = "ImageViewer";
            treeNode15.Text = "Image Viewer";
            treeNode16.Name = "NodeWell";
            treeNode16.Text = "Well";
            treeNode17.Name = "NodeScreening";
            treeNode17.Text = "Screening";
            this.treeViewOptions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12,
            treeNode14,
            treeNode15,
            treeNode17});
            this.treeViewOptions.Size = new System.Drawing.Size(384, 253);
            this.treeViewOptions.TabIndex = 20;
            this.treeViewOptions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewOptions_AfterSelect);
            // 
            // tabPageClustering
            // 
            this.tabPageClustering.Controls.Add(this.groupBox18);
            this.tabPageClustering.Location = new System.Drawing.Point(4, 58);
            this.tabPageClustering.Name = "tabPageClustering";
            this.tabPageClustering.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageClustering.Size = new System.Drawing.Size(396, 401);
            this.tabPageClustering.TabIndex = 2;
            this.tabPageClustering.Text = "Clustering";
            this.tabPageClustering.UseVisualStyleBackColor = true;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.comboBoxHierarchicalLinkType);
            this.groupBox18.Controls.Add(this.label18);
            this.groupBox18.Controls.Add(this.comboBoxHierarchicalDistance);
            this.groupBox18.Controls.Add(this.label17);
            this.groupBox18.Location = new System.Drawing.Point(6, 6);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(384, 92);
            this.groupBox18.TabIndex = 2;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Hierarchical";
            // 
            // comboBoxHierarchicalLinkType
            // 
            this.comboBoxHierarchicalLinkType.FormattingEnabled = true;
            this.comboBoxHierarchicalLinkType.Items.AddRange(new object[] {
            "SINGLE",
            "COMPLETE",
            "AVERAGE",
            "MEAN",
            "CENTROID",
            "WARD",
            "ADJCOMLPETE"});
            this.comboBoxHierarchicalLinkType.Location = new System.Drawing.Point(177, 53);
            this.comboBoxHierarchicalLinkType.Name = "comboBoxHierarchicalLinkType";
            this.comboBoxHierarchicalLinkType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxHierarchicalLinkType.TabIndex = 5;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(98, 56);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(54, 13);
            this.label18.TabIndex = 4;
            this.label18.Text = "Link Type";
            // 
            // comboBoxHierarchicalDistance
            // 
            this.comboBoxHierarchicalDistance.FormattingEnabled = true;
            this.comboBoxHierarchicalDistance.Items.AddRange(new object[] {
            "Euclidean",
            "Manhattan",
            "Chebyshev"});
            this.comboBoxHierarchicalDistance.Location = new System.Drawing.Point(176, 24);
            this.comboBoxHierarchicalDistance.Name = "comboBoxHierarchicalDistance";
            this.comboBoxHierarchicalDistance.Size = new System.Drawing.Size(121, 21);
            this.comboBoxHierarchicalDistance.TabIndex = 3;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(97, 27);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(49, 13);
            this.label17.TabIndex = 2;
            this.label17.Text = "Distance";
            // 
            // tabPageClassification
            // 
            this.tabPageClassification.Controls.Add(this.groupBox15);
            this.tabPageClassification.Controls.Add(this.groupBox8);
            this.tabPageClassification.Location = new System.Drawing.Point(4, 58);
            this.tabPageClassification.Name = "tabPageClassification";
            this.tabPageClassification.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageClassification.Size = new System.Drawing.Size(396, 401);
            this.tabPageClassification.TabIndex = 3;
            this.tabPageClassification.Text = "Classification";
            this.tabPageClassification.UseVisualStyleBackColor = true;
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.numericUpDownJ48MinNumObjects);
            this.groupBox15.Controls.Add(this.label16);
            this.groupBox15.Location = new System.Drawing.Point(6, 88);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(384, 86);
            this.groupBox15.TabIndex = 1;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "C4.5";
            // 
            // numericUpDownJ48MinNumObjects
            // 
            this.numericUpDownJ48MinNumObjects.Location = new System.Drawing.Point(204, 36);
            this.numericUpDownJ48MinNumObjects.Maximum = new decimal(new int[] {
            -469762048,
            -590869294,
            5421010,
            0});
            this.numericUpDownJ48MinNumObjects.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownJ48MinNumObjects.Name = "numericUpDownJ48MinNumObjects";
            this.numericUpDownJ48MinNumObjects.Size = new System.Drawing.Size(92, 20);
            this.numericUpDownJ48MinNumObjects.TabIndex = 3;
            this.numericUpDownJ48MinNumObjects.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(41, 38);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(139, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Minimum Number of Objects";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.numericUpDownKofKNN);
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.Location = new System.Drawing.Point(6, 6);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(384, 76);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "K Nearest Neighbors";
            // 
            // numericUpDownKofKNN
            // 
            this.numericUpDownKofKNN.Location = new System.Drawing.Point(158, 32);
            this.numericUpDownKofKNN.Maximum = new decimal(new int[] {
            -469762048,
            -590869294,
            5421010,
            0});
            this.numericUpDownKofKNN.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownKofKNN.Name = "numericUpDownKofKNN";
            this.numericUpDownKofKNN.Size = new System.Drawing.Size(92, 20);
            this.numericUpDownKofKNN.TabIndex = 1;
            this.numericUpDownKofKNN.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(101, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "K";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox16);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 58);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(396, 401);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "Correlation matrix";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.checkBoxCorrelationRankChangeColorForActiveDesc);
            this.groupBox16.Controls.Add(this.checkBoxCorrelationMatrixDisplayRanking);
            this.groupBox16.Location = new System.Drawing.Point(8, 88);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(382, 92);
            this.groupBox16.TabIndex = 2;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Correlation Ranking";
            // 
            // checkBoxCorrelationRankChangeColorForActiveDesc
            // 
            this.checkBoxCorrelationRankChangeColorForActiveDesc.AutoSize = true;
            this.checkBoxCorrelationRankChangeColorForActiveDesc.Checked = true;
            this.checkBoxCorrelationRankChangeColorForActiveDesc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCorrelationRankChangeColorForActiveDesc.Location = new System.Drawing.Point(140, 43);
            this.checkBoxCorrelationRankChangeColorForActiveDesc.Name = "checkBoxCorrelationRankChangeColorForActiveDesc";
            this.checkBoxCorrelationRankChangeColorForActiveDesc.Size = new System.Drawing.Size(185, 17);
            this.checkBoxCorrelationRankChangeColorForActiveDesc.TabIndex = 2;
            this.checkBoxCorrelationRankChangeColorForActiveDesc.Text = "Change color for active descriptor";
            this.checkBoxCorrelationRankChangeColorForActiveDesc.UseVisualStyleBackColor = true;
            // 
            // checkBoxCorrelationMatrixDisplayRanking
            // 
            this.checkBoxCorrelationMatrixDisplayRanking.AutoSize = true;
            this.checkBoxCorrelationMatrixDisplayRanking.Checked = true;
            this.checkBoxCorrelationMatrixDisplayRanking.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCorrelationMatrixDisplayRanking.Location = new System.Drawing.Point(57, 43);
            this.checkBoxCorrelationMatrixDisplayRanking.Name = "checkBoxCorrelationMatrixDisplayRanking";
            this.checkBoxCorrelationMatrixDisplayRanking.Size = new System.Drawing.Size(60, 17);
            this.checkBoxCorrelationMatrixDisplayRanking.TabIndex = 1;
            this.checkBoxCorrelationMatrixDisplayRanking.Text = "Display";
            this.checkBoxCorrelationMatrixDisplayRanking.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonMIC);
            this.groupBox3.Controls.Add(this.radioButtonSpearman);
            this.groupBox3.Controls.Add(this.radioButtonPearson);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(384, 77);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Type";
            // 
            // radioButtonMIC
            // 
            this.radioButtonMIC.AutoSize = true;
            this.radioButtonMIC.Location = new System.Drawing.Point(283, 31);
            this.radioButtonMIC.Name = "radioButtonMIC";
            this.radioButtonMIC.Size = new System.Drawing.Size(44, 17);
            this.radioButtonMIC.TabIndex = 1;
            this.radioButtonMIC.Text = "MIC";
            this.radioButtonMIC.UseVisualStyleBackColor = true;
            // 
            // radioButtonSpearman
            // 
            this.radioButtonSpearman.AutoSize = true;
            this.radioButtonSpearman.Location = new System.Drawing.Point(162, 31);
            this.radioButtonSpearman.Name = "radioButtonSpearman";
            this.radioButtonSpearman.Size = new System.Drawing.Size(73, 17);
            this.radioButtonSpearman.TabIndex = 0;
            this.radioButtonSpearman.Text = "Spearman";
            this.radioButtonSpearman.UseVisualStyleBackColor = true;
            // 
            // radioButtonPearson
            // 
            this.radioButtonPearson.AutoSize = true;
            this.radioButtonPearson.Checked = true;
            this.radioButtonPearson.Location = new System.Drawing.Point(57, 31);
            this.radioButtonPearson.Name = "radioButtonPearson";
            this.radioButtonPearson.Size = new System.Drawing.Size(64, 17);
            this.radioButtonPearson.TabIndex = 0;
            this.radioButtonPearson.TabStop = true;
            this.radioButtonPearson.Text = "Pearson";
            this.radioButtonPearson.UseVisualStyleBackColor = true;
            // 
            // tabPageMiscInfo
            // 
            this.tabPageMiscInfo.Controls.Add(this.label42);
            this.tabPageMiscInfo.Controls.Add(this.richTextBoxSystemInfo);
            this.tabPageMiscInfo.Controls.Add(this.groupBox9);
            this.tabPageMiscInfo.Location = new System.Drawing.Point(4, 58);
            this.tabPageMiscInfo.Name = "tabPageMiscInfo";
            this.tabPageMiscInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMiscInfo.Size = new System.Drawing.Size(396, 401);
            this.tabPageMiscInfo.TabIndex = 6;
            this.tabPageMiscInfo.Text = "Misc.";
            this.tabPageMiscInfo.UseVisualStyleBackColor = true;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(6, 77);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(41, 13);
            this.label42.TabIndex = 2;
            this.label42.Text = "System";
            // 
            // richTextBoxSystemInfo
            // 
            this.richTextBoxSystemInfo.Location = new System.Drawing.Point(8, 93);
            this.richTextBoxSystemInfo.Name = "richTextBoxSystemInfo";
            this.richTextBoxSystemInfo.ReadOnly = true;
            this.richTextBoxSystemInfo.Size = new System.Drawing.Size(381, 302);
            this.richTextBoxSystemInfo.TabIndex = 1;
            this.richTextBoxSystemInfo.Text = "";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.radioButtonGoogle);
            this.groupBox9.Controls.Add(this.radioButtonIE);
            this.groupBox9.Location = new System.Drawing.Point(6, 6);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(384, 64);
            this.groupBox9.TabIndex = 0;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Internet Browser";
            // 
            // radioButtonGoogle
            // 
            this.radioButtonGoogle.AutoSize = true;
            this.radioButtonGoogle.Location = new System.Drawing.Point(210, 24);
            this.radioButtonGoogle.Name = "radioButtonGoogle";
            this.radioButtonGoogle.Size = new System.Drawing.Size(98, 17);
            this.radioButtonGoogle.TabIndex = 1;
            this.radioButtonGoogle.Text = "Google Chrome";
            this.radioButtonGoogle.UseVisualStyleBackColor = true;
            // 
            // radioButtonIE
            // 
            this.radioButtonIE.AutoSize = true;
            this.radioButtonIE.Checked = true;
            this.radioButtonIE.Location = new System.Drawing.Point(76, 24);
            this.radioButtonIE.Name = "radioButtonIE";
            this.radioButtonIE.Size = new System.Drawing.Size(102, 17);
            this.radioButtonIE.TabIndex = 0;
            this.radioButtonIE.TabStop = true;
            this.radioButtonIE.Text = "Internet Explorer";
            this.radioButtonIE.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox13);
            this.tabPage4.Controls.Add(this.groupBox12);
            this.tabPage4.Location = new System.Drawing.Point(4, 58);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(396, 401);
            this.tabPage4.TabIndex = 7;
            this.tabPage4.Text = "Errors Identif. & Correct.";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.label24);
            this.groupBox13.Controls.Add(this.label21);
            this.groupBox13.Controls.Add(this.label23);
            this.groupBox13.Controls.Add(this.label20);
            this.groupBox13.Controls.Add(this.numericUpDownEdgeEffectDeltaMult);
            this.groupBox13.Controls.Add(this.numericUpDownEdgeEffectMaxMult);
            this.groupBox13.Controls.Add(this.numericUpDownEdgeEffectDeltaShift);
            this.groupBox13.Controls.Add(this.numericUpDownEdgeEffectMinMult);
            this.groupBox13.Controls.Add(this.numericUpDownEdgeEffectMaxShift);
            this.groupBox13.Controls.Add(this.label22);
            this.groupBox13.Controls.Add(this.numericUpDownEdgeEffectMinShift);
            this.groupBox13.Controls.Add(this.label19);
            this.groupBox13.Controls.Add(this.numericUpDownEdgeEffectMaximumIteration);
            this.groupBox13.Controls.Add(this.label11);
            this.groupBox13.Location = new System.Drawing.Point(6, 153);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(384, 217);
            this.groupBox13.TabIndex = 8;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Edge Effect";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(85, 191);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(80, 13);
            this.label24.TabIndex = 7;
            this.label24.Text = "Increment Mult.";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(85, 106);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(78, 13);
            this.label21.TabIndex = 7;
            this.label21.Text = "Increment Shift";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(88, 166);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(77, 13);
            this.label23.TabIndex = 7;
            this.label23.Text = "Maximum Mult.";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(88, 81);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(75, 13);
            this.label20.TabIndex = 7;
            this.label20.Text = "Maximum Shift";
            // 
            // numericUpDownEdgeEffectDeltaMult
            // 
            this.numericUpDownEdgeEffectDeltaMult.DecimalPlaces = 2;
            this.numericUpDownEdgeEffectDeltaMult.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownEdgeEffectDeltaMult.Location = new System.Drawing.Point(209, 189);
            this.numericUpDownEdgeEffectDeltaMult.Maximum = new decimal(new int[] {
            -1593835520,
            466537709,
            54210,
            0});
            this.numericUpDownEdgeEffectDeltaMult.Minimum = new decimal(new int[] {
            -1486618624,
            232830643,
            0,
            -2147483648});
            this.numericUpDownEdgeEffectDeltaMult.Name = "numericUpDownEdgeEffectDeltaMult";
            this.numericUpDownEdgeEffectDeltaMult.Size = new System.Drawing.Size(103, 20);
            this.numericUpDownEdgeEffectDeltaMult.TabIndex = 8;
            this.numericUpDownEdgeEffectDeltaMult.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDownEdgeEffectMaxMult
            // 
            this.numericUpDownEdgeEffectMaxMult.DecimalPlaces = 2;
            this.numericUpDownEdgeEffectMaxMult.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownEdgeEffectMaxMult.Location = new System.Drawing.Point(209, 164);
            this.numericUpDownEdgeEffectMaxMult.Maximum = new decimal(new int[] {
            -1593835520,
            466537709,
            54210,
            0});
            this.numericUpDownEdgeEffectMaxMult.Minimum = new decimal(new int[] {
            -1486618624,
            232830643,
            0,
            -2147483648});
            this.numericUpDownEdgeEffectMaxMult.Name = "numericUpDownEdgeEffectMaxMult";
            this.numericUpDownEdgeEffectMaxMult.Size = new System.Drawing.Size(103, 20);
            this.numericUpDownEdgeEffectMaxMult.TabIndex = 8;
            this.numericUpDownEdgeEffectMaxMult.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // numericUpDownEdgeEffectDeltaShift
            // 
            this.numericUpDownEdgeEffectDeltaShift.DecimalPlaces = 2;
            this.numericUpDownEdgeEffectDeltaShift.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownEdgeEffectDeltaShift.Location = new System.Drawing.Point(209, 104);
            this.numericUpDownEdgeEffectDeltaShift.Maximum = new decimal(new int[] {
            -1593835520,
            466537709,
            54210,
            0});
            this.numericUpDownEdgeEffectDeltaShift.Minimum = new decimal(new int[] {
            -1486618624,
            232830643,
            0,
            -2147483648});
            this.numericUpDownEdgeEffectDeltaShift.Name = "numericUpDownEdgeEffectDeltaShift";
            this.numericUpDownEdgeEffectDeltaShift.Size = new System.Drawing.Size(103, 20);
            this.numericUpDownEdgeEffectDeltaShift.TabIndex = 8;
            this.numericUpDownEdgeEffectDeltaShift.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDownEdgeEffectMinMult
            // 
            this.numericUpDownEdgeEffectMinMult.DecimalPlaces = 2;
            this.numericUpDownEdgeEffectMinMult.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownEdgeEffectMinMult.Location = new System.Drawing.Point(209, 139);
            this.numericUpDownEdgeEffectMinMult.Maximum = new decimal(new int[] {
            -1593835520,
            466537709,
            54210,
            0});
            this.numericUpDownEdgeEffectMinMult.Minimum = new decimal(new int[] {
            -1486618624,
            232830643,
            0,
            -2147483648});
            this.numericUpDownEdgeEffectMinMult.Name = "numericUpDownEdgeEffectMinMult";
            this.numericUpDownEdgeEffectMinMult.Size = new System.Drawing.Size(103, 20);
            this.numericUpDownEdgeEffectMinMult.TabIndex = 8;
            // 
            // numericUpDownEdgeEffectMaxShift
            // 
            this.numericUpDownEdgeEffectMaxShift.DecimalPlaces = 2;
            this.numericUpDownEdgeEffectMaxShift.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownEdgeEffectMaxShift.Location = new System.Drawing.Point(209, 79);
            this.numericUpDownEdgeEffectMaxShift.Maximum = new decimal(new int[] {
            -1593835520,
            466537709,
            54210,
            0});
            this.numericUpDownEdgeEffectMaxShift.Minimum = new decimal(new int[] {
            -1486618624,
            232830643,
            0,
            -2147483648});
            this.numericUpDownEdgeEffectMaxShift.Name = "numericUpDownEdgeEffectMaxShift";
            this.numericUpDownEdgeEffectMaxShift.Size = new System.Drawing.Size(103, 20);
            this.numericUpDownEdgeEffectMaxShift.TabIndex = 8;
            this.numericUpDownEdgeEffectMaxShift.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(91, 141);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(74, 13);
            this.label22.TabIndex = 7;
            this.label22.Text = "Minimum Mult.";
            // 
            // numericUpDownEdgeEffectMinShift
            // 
            this.numericUpDownEdgeEffectMinShift.DecimalPlaces = 2;
            this.numericUpDownEdgeEffectMinShift.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownEdgeEffectMinShift.Location = new System.Drawing.Point(209, 54);
            this.numericUpDownEdgeEffectMinShift.Maximum = new decimal(new int[] {
            -1593835520,
            466537709,
            54210,
            0});
            this.numericUpDownEdgeEffectMinShift.Minimum = new decimal(new int[] {
            -1486618624,
            232830643,
            0,
            -2147483648});
            this.numericUpDownEdgeEffectMinShift.Name = "numericUpDownEdgeEffectMinShift";
            this.numericUpDownEdgeEffectMinShift.Size = new System.Drawing.Size(103, 20);
            this.numericUpDownEdgeEffectMinShift.TabIndex = 8;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(91, 56);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(72, 13);
            this.label19.TabIndex = 7;
            this.label19.Text = "Minimum Shift";
            // 
            // numericUpDownEdgeEffectMaximumIteration
            // 
            this.numericUpDownEdgeEffectMaximumIteration.Location = new System.Drawing.Point(209, 22);
            this.numericUpDownEdgeEffectMaximumIteration.Maximum = new decimal(new int[] {
            -1486618624,
            232830643,
            0,
            0});
            this.numericUpDownEdgeEffectMaximumIteration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownEdgeEffectMaximumIteration.Name = "numericUpDownEdgeEffectMaximumIteration";
            this.numericUpDownEdgeEffectMaximumIteration.Size = new System.Drawing.Size(103, 20);
            this.numericUpDownEdgeEffectMaximumIteration.TabIndex = 8;
            this.numericUpDownEdgeEffectMaximumIteration.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(71, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Maximum Iteration";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.numericUpDownSystErrorIdentKMeansClasses);
            this.groupBox12.Controls.Add(this.label8);
            this.groupBox12.Controls.Add(this.numericUpDownSystemMinWellRatio);
            this.groupBox12.Controls.Add(this.numericUpDownSystErrorAndersonDarlingThreshold);
            this.groupBox12.Controls.Add(this.label10);
            this.groupBox12.Controls.Add(this.label9);
            this.groupBox12.Location = new System.Drawing.Point(6, 6);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(384, 141);
            this.groupBox12.TabIndex = 7;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Systematic Errors Identification";
            // 
            // numericUpDownSystErrorIdentKMeansClasses
            // 
            this.numericUpDownSystErrorIdentKMeansClasses.Location = new System.Drawing.Point(209, 29);
            this.numericUpDownSystErrorIdentKMeansClasses.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownSystErrorIdentKMeansClasses.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownSystErrorIdentKMeansClasses.Name = "numericUpDownSystErrorIdentKMeansClasses";
            this.numericUpDownSystErrorIdentKMeansClasses.Size = new System.Drawing.Size(103, 20);
            this.numericUpDownSystErrorIdentKMeansClasses.TabIndex = 2;
            this.numericUpDownSystErrorIdentKMeansClasses.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(75, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "K-Means Classes";
            // 
            // numericUpDownSystemMinWellRatio
            // 
            this.numericUpDownSystemMinWellRatio.Location = new System.Drawing.Point(209, 98);
            this.numericUpDownSystemMinWellRatio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSystemMinWellRatio.Name = "numericUpDownSystemMinWellRatio";
            this.numericUpDownSystemMinWellRatio.Size = new System.Drawing.Size(103, 20);
            this.numericUpDownSystemMinWellRatio.TabIndex = 6;
            this.numericUpDownSystemMinWellRatio.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // numericUpDownSystErrorAndersonDarlingThreshold
            // 
            this.numericUpDownSystErrorAndersonDarlingThreshold.DecimalPlaces = 2;
            this.numericUpDownSystErrorAndersonDarlingThreshold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownSystErrorAndersonDarlingThreshold.Location = new System.Drawing.Point(209, 62);
            this.numericUpDownSystErrorAndersonDarlingThreshold.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.numericUpDownSystErrorAndersonDarlingThreshold.Name = "numericUpDownSystErrorAndersonDarlingThreshold";
            this.numericUpDownSystErrorAndersonDarlingThreshold.Size = new System.Drawing.Size(103, 20);
            this.numericUpDownSystErrorAndersonDarlingThreshold.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(58, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(105, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Minimum Wells Ratio";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(138, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Anderson-Darling Threshold";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox14);
            this.tabPage5.Location = new System.Drawing.Point(4, 58);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(396, 401);
            this.tabPage5.TabIndex = 8;
            this.tabPage5.Text = "Generate Screening";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.numericUpDownGenerateScreenDiffusion);
            this.groupBox14.Controls.Add(this.label15);
            this.groupBox14.Controls.Add(this.numericUpDownGenerateScreenRatioXY);
            this.groupBox14.Controls.Add(this.label13);
            this.groupBox14.Controls.Add(this.numericUpDownGenerateScreenRowEffectShift);
            this.groupBox14.Controls.Add(this.label12);
            this.groupBox14.Controls.Add(this.numericUpDownGenerateScreenNoiseStdDev);
            this.groupBox14.Controls.Add(this.label14);
            this.groupBox14.Location = new System.Drawing.Point(6, 10);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(384, 207);
            this.groupBox14.TabIndex = 8;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Variable parameter steps";
            // 
            // numericUpDownGenerateScreenDiffusion
            // 
            this.numericUpDownGenerateScreenDiffusion.Location = new System.Drawing.Point(196, 148);
            this.numericUpDownGenerateScreenDiffusion.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.numericUpDownGenerateScreenDiffusion.Name = "numericUpDownGenerateScreenDiffusion";
            this.numericUpDownGenerateScreenDiffusion.Size = new System.Drawing.Size(103, 20);
            this.numericUpDownGenerateScreenDiffusion.TabIndex = 9;
            this.numericUpDownGenerateScreenDiffusion.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(95, 151);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(48, 13);
            this.label15.TabIndex = 10;
            this.label15.Text = "Diffusion";
            // 
            // numericUpDownGenerateScreenRatioXY
            // 
            this.numericUpDownGenerateScreenRatioXY.DecimalPlaces = 2;
            this.numericUpDownGenerateScreenRatioXY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownGenerateScreenRatioXY.Location = new System.Drawing.Point(196, 110);
            this.numericUpDownGenerateScreenRatioXY.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.numericUpDownGenerateScreenRatioXY.Name = "numericUpDownGenerateScreenRatioXY";
            this.numericUpDownGenerateScreenRatioXY.Size = new System.Drawing.Size(103, 20);
            this.numericUpDownGenerateScreenRatioXY.TabIndex = 7;
            this.numericUpDownGenerateScreenRatioXY.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(95, 113);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Ratio X/Y";
            // 
            // numericUpDownGenerateScreenRowEffectShift
            // 
            this.numericUpDownGenerateScreenRowEffectShift.DecimalPlaces = 2;
            this.numericUpDownGenerateScreenRowEffectShift.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownGenerateScreenRowEffectShift.Location = new System.Drawing.Point(196, 72);
            this.numericUpDownGenerateScreenRowEffectShift.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.numericUpDownGenerateScreenRowEffectShift.Name = "numericUpDownGenerateScreenRowEffectShift";
            this.numericUpDownGenerateScreenRowEffectShift.Size = new System.Drawing.Size(103, 20);
            this.numericUpDownGenerateScreenRowEffectShift.TabIndex = 5;
            this.numericUpDownGenerateScreenRowEffectShift.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(76, 74);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Row effect shift";
            // 
            // numericUpDownGenerateScreenNoiseStdDev
            // 
            this.numericUpDownGenerateScreenNoiseStdDev.DecimalPlaces = 2;
            this.numericUpDownGenerateScreenNoiseStdDev.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownGenerateScreenNoiseStdDev.Location = new System.Drawing.Point(196, 36);
            this.numericUpDownGenerateScreenNoiseStdDev.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.numericUpDownGenerateScreenNoiseStdDev.Name = "numericUpDownGenerateScreenNoiseStdDev";
            this.numericUpDownGenerateScreenNoiseStdDev.Size = new System.Drawing.Size(103, 20);
            this.numericUpDownGenerateScreenNoiseStdDev.TabIndex = 3;
            this.numericUpDownGenerateScreenNoiseStdDev.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(26, 38);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(131, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Compound noise Std Dev.";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.groupBox25);
            this.tabPage6.Controls.Add(this.groupBox19);
            this.tabPage6.Controls.Add(this.buttonDRCPlateDesign);
            this.tabPage6.Location = new System.Drawing.Point(4, 58);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(396, 401);
            this.tabPage6.TabIndex = 9;
            this.tabPage6.Text = "DRC analysis";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // groupBox25
            // 
            this.groupBox25.Controls.Add(this.checkBoxConnectDRCPts);
            this.groupBox25.Location = new System.Drawing.Point(6, 136);
            this.groupBox25.Name = "groupBox25";
            this.groupBox25.Size = new System.Drawing.Size(384, 70);
            this.groupBox25.TabIndex = 3;
            this.groupBox25.TabStop = false;
            this.groupBox25.Text = "XYZ scatter points";
            // 
            // checkBoxConnectDRCPts
            // 
            this.checkBoxConnectDRCPts.AutoSize = true;
            this.checkBoxConnectDRCPts.Location = new System.Drawing.Point(128, 30);
            this.checkBoxConnectDRCPts.Name = "checkBoxConnectDRCPts";
            this.checkBoxConnectDRCPts.Size = new System.Drawing.Size(123, 17);
            this.checkBoxConnectDRCPts.TabIndex = 2;
            this.checkBoxConnectDRCPts.Text = "Connect DRC points";
            this.checkBoxConnectDRCPts.UseVisualStyleBackColor = true;
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.radioButtonReplicatesAverageStdev);
            this.groupBox19.Controls.Add(this.radioButtonReplicateAllValues);
            this.groupBox19.Location = new System.Drawing.Point(6, 74);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(384, 56);
            this.groupBox19.TabIndex = 1;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Replicates Display";
            // 
            // radioButtonReplicatesAverageStdev
            // 
            this.radioButtonReplicatesAverageStdev.AutoSize = true;
            this.radioButtonReplicatesAverageStdev.Checked = true;
            this.radioButtonReplicatesAverageStdev.Location = new System.Drawing.Point(54, 23);
            this.radioButtonReplicatesAverageStdev.Name = "radioButtonReplicatesAverageStdev";
            this.radioButtonReplicatesAverageStdev.Size = new System.Drawing.Size(117, 17);
            this.radioButtonReplicatesAverageStdev.TabIndex = 1;
            this.radioButtonReplicatesAverageStdev.TabStop = true;
            this.radioButtonReplicatesAverageStdev.Text = "Average and Stdev";
            this.radioButtonReplicatesAverageStdev.UseVisualStyleBackColor = true;
            // 
            // radioButtonReplicateAllValues
            // 
            this.radioButtonReplicateAllValues.AutoSize = true;
            this.radioButtonReplicateAllValues.Location = new System.Drawing.Point(260, 23);
            this.radioButtonReplicateAllValues.Name = "radioButtonReplicateAllValues";
            this.radioButtonReplicateAllValues.Size = new System.Drawing.Size(71, 17);
            this.radioButtonReplicateAllValues.TabIndex = 0;
            this.radioButtonReplicateAllValues.Text = "All Values";
            this.radioButtonReplicateAllValues.UseVisualStyleBackColor = true;
            // 
            // buttonDRCPlateDesign
            // 
            this.buttonDRCPlateDesign.Location = new System.Drawing.Point(271, 6);
            this.buttonDRCPlateDesign.Name = "buttonDRCPlateDesign";
            this.buttonDRCPlateDesign.Size = new System.Drawing.Size(119, 31);
            this.buttonDRCPlateDesign.TabIndex = 0;
            this.buttonDRCPlateDesign.Text = "Plate Design";
            this.buttonDRCPlateDesign.UseVisualStyleBackColor = true;
            this.buttonDRCPlateDesign.Click += new System.EventHandler(this.buttonDRCPlateDesign_Click);
            // 
            // tabPage3D
            // 
            this.tabPage3D.Controls.Add(this.groupBox23);
            this.tabPage3D.Controls.Add(this.groupBox22);
            this.tabPage3D.Controls.Add(this.groupBox20);
            this.tabPage3D.Controls.Add(this.groupBox21);
            this.tabPage3D.Location = new System.Drawing.Point(4, 58);
            this.tabPage3D.Name = "tabPage3D";
            this.tabPage3D.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3D.Size = new System.Drawing.Size(396, 401);
            this.tabPage3D.TabIndex = 10;
            this.tabPage3D.Text = "3D";
            this.tabPage3D.UseVisualStyleBackColor = true;
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.checkBox3DPlateInformation);
            this.groupBox23.Location = new System.Drawing.Point(6, 13);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(381, 63);
            this.groupBox23.TabIndex = 22;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Plate Display";
            // 
            // checkBox3DPlateInformation
            // 
            this.checkBox3DPlateInformation.AutoSize = true;
            this.checkBox3DPlateInformation.Checked = true;
            this.checkBox3DPlateInformation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3DPlateInformation.Location = new System.Drawing.Point(119, 26);
            this.checkBox3DPlateInformation.Name = "checkBox3DPlateInformation";
            this.checkBox3DPlateInformation.Size = new System.Drawing.Size(142, 17);
            this.checkBox3DPlateInformation.TabIndex = 5;
            this.checkBox3DPlateInformation.Text = "Display Plate Information";
            this.checkBox3DPlateInformation.UseVisualStyleBackColor = true;
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.checkBox3DComputeThinPlate);
            this.groupBox22.Controls.Add(this.numericUpDown3DThinPlateRegularization);
            this.groupBox22.Controls.Add(this.label28);
            this.groupBox22.Controls.Add(this.checkBox3DDisplayThinPlate);
            this.groupBox22.Controls.Add(this.checkBox3DDisplayIsoRatioCurves);
            this.groupBox22.Controls.Add(this.checkBox3DDisplayIsoboles);
            this.groupBox22.Location = new System.Drawing.Point(6, 248);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(382, 107);
            this.groupBox22.TabIndex = 21;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "                                 ";
            // 
            // checkBox3DComputeThinPlate
            // 
            this.checkBox3DComputeThinPlate.AutoSize = true;
            this.checkBox3DComputeThinPlate.Location = new System.Drawing.Point(9, -1);
            this.checkBox3DComputeThinPlate.Name = "checkBox3DComputeThinPlate";
            this.checkBox3DComputeThinPlate.Size = new System.Drawing.Size(134, 17);
            this.checkBox3DComputeThinPlate.TabIndex = 22;
            this.checkBox3DComputeThinPlate.Text = "Surface Analysis          ";
            this.checkBox3DComputeThinPlate.UseVisualStyleBackColor = true;
            // 
            // numericUpDown3DThinPlateRegularization
            // 
            this.numericUpDown3DThinPlateRegularization.DecimalPlaces = 2;
            this.numericUpDown3DThinPlateRegularization.Location = new System.Drawing.Point(279, 28);
            this.numericUpDown3DThinPlateRegularization.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown3DThinPlateRegularization.Name = "numericUpDown3DThinPlateRegularization";
            this.numericUpDown3DThinPlateRegularization.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown3DThinPlateRegularization.TabIndex = 5;
            this.numericUpDown3DThinPlateRegularization.Value = new decimal(new int[] {
            15,
            0,
            0,
            131072});
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(192, 30);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(74, 13);
            this.label28.TabIndex = 4;
            this.label28.Text = "Regularization";
            // 
            // checkBox3DDisplayThinPlate
            // 
            this.checkBox3DDisplayThinPlate.AutoSize = true;
            this.checkBox3DDisplayThinPlate.Checked = true;
            this.checkBox3DDisplayThinPlate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3DDisplayThinPlate.Location = new System.Drawing.Point(70, 28);
            this.checkBox3DDisplayThinPlate.Name = "checkBox3DDisplayThinPlate";
            this.checkBox3DDisplayThinPlate.Size = new System.Drawing.Size(100, 17);
            this.checkBox3DDisplayThinPlate.TabIndex = 4;
            this.checkBox3DDisplayThinPlate.Text = "Display Surface";
            this.checkBox3DDisplayThinPlate.UseVisualStyleBackColor = true;
            // 
            // checkBox3DDisplayIsoRatioCurves
            // 
            this.checkBox3DDisplayIsoRatioCurves.AutoSize = true;
            this.checkBox3DDisplayIsoRatioCurves.Checked = true;
            this.checkBox3DDisplayIsoRatioCurves.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3DDisplayIsoRatioCurves.Location = new System.Drawing.Point(195, 66);
            this.checkBox3DDisplayIsoRatioCurves.Name = "checkBox3DDisplayIsoRatioCurves";
            this.checkBox3DDisplayIsoRatioCurves.Size = new System.Drawing.Size(135, 17);
            this.checkBox3DDisplayIsoRatioCurves.TabIndex = 3;
            this.checkBox3DDisplayIsoRatioCurves.Text = "Display Iso ratio curves";
            this.checkBox3DDisplayIsoRatioCurves.UseVisualStyleBackColor = true;
            // 
            // checkBox3DDisplayIsoboles
            // 
            this.checkBox3DDisplayIsoboles.AutoSize = true;
            this.checkBox3DDisplayIsoboles.Checked = true;
            this.checkBox3DDisplayIsoboles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3DDisplayIsoboles.Location = new System.Drawing.Point(68, 66);
            this.checkBox3DDisplayIsoboles.Name = "checkBox3DDisplayIsoboles";
            this.checkBox3DDisplayIsoboles.Size = new System.Drawing.Size(102, 17);
            this.checkBox3DDisplayIsoboles.TabIndex = 3;
            this.checkBox3DDisplayIsoboles.Text = "Display Isoboles";
            this.checkBox3DDisplayIsoboles.UseVisualStyleBackColor = true;
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.checkBox1);
            this.groupBox20.Controls.Add(this.numericUpDownDRCOpacity);
            this.groupBox20.Controls.Add(this.label26);
            this.groupBox20.Location = new System.Drawing.Point(6, 165);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(382, 70);
            this.groupBox20.TabIndex = 20;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "                                              ";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(10, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(138, 17);
            this.checkBox1.TabIndex = 23;
            this.checkBox1.Text = "Dose Response Curves";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // numericUpDownDRCOpacity
            // 
            this.numericUpDownDRCOpacity.DecimalPlaces = 2;
            this.numericUpDownDRCOpacity.Location = new System.Drawing.Point(196, 30);
            this.numericUpDownDRCOpacity.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDRCOpacity.Name = "numericUpDownDRCOpacity";
            this.numericUpDownDRCOpacity.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownDRCOpacity.TabIndex = 3;
            this.numericUpDownDRCOpacity.Value = new decimal(new int[] {
            7,
            0,
            0,
            65536});
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(123, 33);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(67, 13);
            this.label26.TabIndex = 2;
            this.label26.Text = "DRC opacity";
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.numericUpDownWellSize);
            this.groupBox21.Controls.Add(this.label27);
            this.groupBox21.Controls.Add(this.numericUpDownWellOpacity);
            this.groupBox21.Controls.Add(this.label25);
            this.groupBox21.Location = new System.Drawing.Point(6, 82);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(382, 76);
            this.groupBox21.TabIndex = 19;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Wells Display";
            // 
            // numericUpDownWellSize
            // 
            this.numericUpDownWellSize.DecimalPlaces = 2;
            this.numericUpDownWellSize.Location = new System.Drawing.Point(278, 33);
            this.numericUpDownWellSize.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWellSize.Name = "numericUpDownWellSize";
            this.numericUpDownWellSize.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownWellSize.TabIndex = 5;
            this.numericUpDownWellSize.Value = new decimal(new int[] {
            9,
            0,
            0,
            65536});
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(208, 37);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(49, 13);
            this.label27.TabIndex = 4;
            this.label27.Text = "Well size";
            // 
            // numericUpDownWellOpacity
            // 
            this.numericUpDownWellOpacity.DecimalPlaces = 2;
            this.numericUpDownWellOpacity.Location = new System.Drawing.Point(108, 33);
            this.numericUpDownWellOpacity.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWellOpacity.Name = "numericUpDownWellOpacity";
            this.numericUpDownWellOpacity.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownWellOpacity.TabIndex = 1;
            this.numericUpDownWellOpacity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(38, 37);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(65, 13);
            this.label25.TabIndex = 0;
            this.label25.Text = "Well opacity";
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.label31);
            this.tabPage8.Controls.Add(this.groupBox26);
            this.tabPage8.Controls.Add(this.groupBox24);
            this.tabPage8.Location = new System.Drawing.Point(4, 58);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(396, 401);
            this.tabPage8.TabIndex = 11;
            this.tabPage8.Text = "Histogram Analysis && Display";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(6, 162);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(108, 13);
            this.label31.TabIndex = 4;
            this.label31.Text = "Histogram Display";
            // 
            // groupBox26
            // 
            this.groupBox26.Controls.Add(this.radioButtonHistoDisplayAdjusted);
            this.groupBox26.Controls.Add(this.numericUpDownManualMax);
            this.groupBox26.Controls.Add(this.numericUpDownManualMin);
            this.groupBox26.Controls.Add(this.label33);
            this.groupBox26.Controls.Add(this.numericUpDownAutomatedMax);
            this.groupBox26.Controls.Add(this.label32);
            this.groupBox26.Controls.Add(this.numericUpDownAutomatedMin);
            this.groupBox26.Controls.Add(this.radioButtonHistoDisplayManualMinMax);
            this.groupBox26.Controls.Add(this.radioButtonHistoDisplayAutomatedMinMax);
            this.groupBox26.Location = new System.Drawing.Point(9, 178);
            this.groupBox26.Name = "groupBox26";
            this.groupBox26.Size = new System.Drawing.Size(376, 131);
            this.groupBox26.TabIndex = 1;
            this.groupBox26.TabStop = false;
            this.groupBox26.Text = "Scale";
            // 
            // radioButtonHistoDisplayAdjusted
            // 
            this.radioButtonHistoDisplayAdjusted.AutoSize = true;
            this.radioButtonHistoDisplayAdjusted.Location = new System.Drawing.Point(25, 101);
            this.radioButtonHistoDisplayAdjusted.Name = "radioButtonHistoDisplayAdjusted";
            this.radioButtonHistoDisplayAdjusted.Size = new System.Drawing.Size(124, 17);
            this.radioButtonHistoDisplayAdjusted.TabIndex = 7;
            this.radioButtonHistoDisplayAdjusted.Text = "Well-by-well adjusted";
            this.radioButtonHistoDisplayAdjusted.UseVisualStyleBackColor = true;
            // 
            // numericUpDownManualMax
            // 
            this.numericUpDownManualMax.DecimalPlaces = 3;
            this.numericUpDownManualMax.Location = new System.Drawing.Point(281, 70);
            this.numericUpDownManualMax.Maximum = new decimal(new int[] {
            -1304428544,
            434162106,
            542,
            0});
            this.numericUpDownManualMax.Minimum = new decimal(new int[] {
            -1593835520,
            466537709,
            54210,
            -2147483648});
            this.numericUpDownManualMax.Name = "numericUpDownManualMax";
            this.numericUpDownManualMax.Size = new System.Drawing.Size(89, 20);
            this.numericUpDownManualMax.TabIndex = 6;
            this.numericUpDownManualMax.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numericUpDownManualMax.ValueChanged += new System.EventHandler(this.numericUpDownManualMax_ValueChanged);
            // 
            // numericUpDownManualMin
            // 
            this.numericUpDownManualMin.DecimalPlaces = 3;
            this.numericUpDownManualMin.Location = new System.Drawing.Point(171, 70);
            this.numericUpDownManualMin.Maximum = new decimal(new int[] {
            -1304428544,
            434162106,
            542,
            0});
            this.numericUpDownManualMin.Minimum = new decimal(new int[] {
            -1593835520,
            466537709,
            54210,
            -2147483648});
            this.numericUpDownManualMin.Name = "numericUpDownManualMin";
            this.numericUpDownManualMin.Size = new System.Drawing.Size(89, 20);
            this.numericUpDownManualMin.TabIndex = 5;
            this.numericUpDownManualMin.ValueChanged += new System.EventHandler(this.numericUpDownManualMin_ValueChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(312, 16);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(27, 13);
            this.label33.TabIndex = 4;
            this.label33.Text = "Max";
            // 
            // numericUpDownAutomatedMax
            // 
            this.numericUpDownAutomatedMax.DecimalPlaces = 3;
            this.numericUpDownAutomatedMax.Location = new System.Drawing.Point(281, 37);
            this.numericUpDownAutomatedMax.Maximum = new decimal(new int[] {
            -1304428544,
            434162106,
            542,
            0});
            this.numericUpDownAutomatedMax.Minimum = new decimal(new int[] {
            -1593835520,
            466537709,
            54210,
            -2147483648});
            this.numericUpDownAutomatedMax.Name = "numericUpDownAutomatedMax";
            this.numericUpDownAutomatedMax.ReadOnly = true;
            this.numericUpDownAutomatedMax.Size = new System.Drawing.Size(89, 20);
            this.numericUpDownAutomatedMax.TabIndex = 3;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(203, 16);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(24, 13);
            this.label32.TabIndex = 2;
            this.label32.Text = "Min";
            // 
            // numericUpDownAutomatedMin
            // 
            this.numericUpDownAutomatedMin.DecimalPlaces = 3;
            this.numericUpDownAutomatedMin.Location = new System.Drawing.Point(171, 37);
            this.numericUpDownAutomatedMin.Maximum = new decimal(new int[] {
            -1304428544,
            434162106,
            542,
            0});
            this.numericUpDownAutomatedMin.Minimum = new decimal(new int[] {
            -1593835520,
            466537709,
            54210,
            -2147483648});
            this.numericUpDownAutomatedMin.Name = "numericUpDownAutomatedMin";
            this.numericUpDownAutomatedMin.ReadOnly = true;
            this.numericUpDownAutomatedMin.Size = new System.Drawing.Size(89, 20);
            this.numericUpDownAutomatedMin.TabIndex = 1;
            // 
            // radioButtonHistoDisplayManualMinMax
            // 
            this.radioButtonHistoDisplayManualMinMax.AutoSize = true;
            this.radioButtonHistoDisplayManualMinMax.Location = new System.Drawing.Point(25, 70);
            this.radioButtonHistoDisplayManualMinMax.Name = "radioButtonHistoDisplayManualMinMax";
            this.radioButtonHistoDisplayManualMinMax.Size = new System.Drawing.Size(60, 17);
            this.radioButtonHistoDisplayManualMinMax.TabIndex = 0;
            this.radioButtonHistoDisplayManualMinMax.Text = "Manual";
            this.radioButtonHistoDisplayManualMinMax.UseVisualStyleBackColor = true;
            // 
            // radioButtonHistoDisplayAutomatedMinMax
            // 
            this.radioButtonHistoDisplayAutomatedMinMax.AutoSize = true;
            this.radioButtonHistoDisplayAutomatedMinMax.Checked = true;
            this.radioButtonHistoDisplayAutomatedMinMax.Location = new System.Drawing.Point(25, 37);
            this.radioButtonHistoDisplayAutomatedMinMax.Name = "radioButtonHistoDisplayAutomatedMinMax";
            this.radioButtonHistoDisplayAutomatedMinMax.Size = new System.Drawing.Size(76, 17);
            this.radioButtonHistoDisplayAutomatedMinMax.TabIndex = 0;
            this.radioButtonHistoDisplayAutomatedMinMax.TabStop = true;
            this.radioButtonHistoDisplayAutomatedMinMax.Text = "Automated";
            this.radioButtonHistoDisplayAutomatedMinMax.UseVisualStyleBackColor = true;
            // 
            // groupBox24
            // 
            this.groupBox24.Controls.Add(this.radioButtonDistributionMetricEMD);
            this.groupBox24.Controls.Add(this.radioButtonDistributionMetricBhattacharyya);
            this.groupBox24.Controls.Add(this.label30);
            this.groupBox24.Controls.Add(this.label29);
            this.groupBox24.Controls.Add(this.radioButtonDistributionMetricCosine);
            this.groupBox24.Controls.Add(this.radioButtonDistributionMetricManhattan);
            this.groupBox24.Controls.Add(this.radioButtonDistributionMetricEuclidean);
            this.groupBox24.Location = new System.Drawing.Point(9, 6);
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.Size = new System.Drawing.Size(376, 152);
            this.groupBox24.TabIndex = 0;
            this.groupBox24.TabStop = false;
            this.groupBox24.Text = "Metric";
            // 
            // radioButtonDistributionMetricEMD
            // 
            this.radioButtonDistributionMetricEMD.AutoSize = true;
            this.radioButtonDistributionMetricEMD.Location = new System.Drawing.Point(151, 116);
            this.radioButtonDistributionMetricEMD.Name = "radioButtonDistributionMetricEMD";
            this.radioButtonDistributionMetricEMD.Size = new System.Drawing.Size(128, 17);
            this.radioButtonDistributionMetricEMD.TabIndex = 6;
            this.radioButtonDistributionMetricEMD.Text = "Earth Mover Distance";
            this.radioButtonDistributionMetricEMD.UseVisualStyleBackColor = true;
            // 
            // radioButtonDistributionMetricBhattacharyya
            // 
            this.radioButtonDistributionMetricBhattacharyya.AutoSize = true;
            this.radioButtonDistributionMetricBhattacharyya.Location = new System.Drawing.Point(151, 84);
            this.radioButtonDistributionMetricBhattacharyya.Name = "radioButtonDistributionMetricBhattacharyya";
            this.radioButtonDistributionMetricBhattacharyya.Size = new System.Drawing.Size(146, 17);
            this.radioButtonDistributionMetricBhattacharyya.TabIndex = 5;
            this.radioButtonDistributionMetricBhattacharyya.Text = "Bhattacharyya Coefficient";
            this.radioButtonDistributionMetricBhattacharyya.UseVisualStyleBackColor = true;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(43, 119);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(59, 13);
            this.label30.TabIndex = 4;
            this.label30.Text = "Cross-bin";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(43, 16);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(61, 13);
            this.label29.TabIndex = 3;
            this.label29.Text = "Bin-to-bin";
            // 
            // radioButtonDistributionMetricCosine
            // 
            this.radioButtonDistributionMetricCosine.AutoSize = true;
            this.radioButtonDistributionMetricCosine.Location = new System.Drawing.Point(151, 61);
            this.radioButtonDistributionMetricCosine.Name = "radioButtonDistributionMetricCosine";
            this.radioButtonDistributionMetricCosine.Size = new System.Drawing.Size(121, 17);
            this.radioButtonDistributionMetricCosine.TabIndex = 2;
            this.radioButtonDistributionMetricCosine.Text = "Vector Cosine Angle";
            this.radioButtonDistributionMetricCosine.UseVisualStyleBackColor = true;
            // 
            // radioButtonDistributionMetricManhattan
            // 
            this.radioButtonDistributionMetricManhattan.AutoSize = true;
            this.radioButtonDistributionMetricManhattan.Location = new System.Drawing.Point(151, 38);
            this.radioButtonDistributionMetricManhattan.Name = "radioButtonDistributionMetricManhattan";
            this.radioButtonDistributionMetricManhattan.Size = new System.Drawing.Size(76, 17);
            this.radioButtonDistributionMetricManhattan.TabIndex = 1;
            this.radioButtonDistributionMetricManhattan.Text = "Manhattan";
            this.radioButtonDistributionMetricManhattan.UseVisualStyleBackColor = true;
            // 
            // radioButtonDistributionMetricEuclidean
            // 
            this.radioButtonDistributionMetricEuclidean.AutoSize = true;
            this.radioButtonDistributionMetricEuclidean.Checked = true;
            this.radioButtonDistributionMetricEuclidean.Location = new System.Drawing.Point(151, 15);
            this.radioButtonDistributionMetricEuclidean.Name = "radioButtonDistributionMetricEuclidean";
            this.radioButtonDistributionMetricEuclidean.Size = new System.Drawing.Size(72, 17);
            this.radioButtonDistributionMetricEuclidean.TabIndex = 0;
            this.radioButtonDistributionMetricEuclidean.TabStop = true;
            this.radioButtonDistributionMetricEuclidean.Text = "Euclidean";
            this.radioButtonDistributionMetricEuclidean.UseVisualStyleBackColor = true;
            // 
            // tabPageClasses
            // 
            this.tabPageClasses.Controls.Add(this.groupBoxPhenotypes);
            this.tabPageClasses.Controls.Add(this.groupBox28);
            this.tabPageClasses.Location = new System.Drawing.Point(4, 58);
            this.tabPageClasses.Name = "tabPageClasses";
            this.tabPageClasses.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageClasses.Size = new System.Drawing.Size(396, 401);
            this.tabPageClasses.TabIndex = 12;
            this.tabPageClasses.Text = "Classes";
            this.tabPageClasses.UseVisualStyleBackColor = true;
            // 
            // groupBoxPhenotypes
            // 
            this.groupBoxPhenotypes.Controls.Add(this.panelForCellularPhenotypes);
            this.groupBoxPhenotypes.Location = new System.Drawing.Point(201, 6);
            this.groupBoxPhenotypes.Name = "groupBoxPhenotypes";
            this.groupBoxPhenotypes.Size = new System.Drawing.Size(190, 375);
            this.groupBoxPhenotypes.TabIndex = 2;
            this.groupBoxPhenotypes.TabStop = false;
            this.groupBoxPhenotypes.Text = "Cellular Phenotypes";
            // 
            // panelForCellularPhenotypes
            // 
            this.panelForCellularPhenotypes.Location = new System.Drawing.Point(6, 19);
            this.panelForCellularPhenotypes.Name = "panelForCellularPhenotypes";
            this.panelForCellularPhenotypes.Size = new System.Drawing.Size(178, 338);
            this.panelForCellularPhenotypes.TabIndex = 0;
            // 
            // groupBox28
            // 
            this.groupBox28.Controls.Add(this.panelForWellClasses);
            this.groupBox28.Location = new System.Drawing.Point(3, 6);
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.Size = new System.Drawing.Size(190, 375);
            this.groupBox28.TabIndex = 1;
            this.groupBox28.TabStop = false;
            this.groupBox28.Text = "Classes";
            // 
            // panelForWellClasses
            // 
            this.panelForWellClasses.Location = new System.Drawing.Point(6, 19);
            this.panelForWellClasses.Name = "panelForWellClasses";
            this.panelForWellClasses.Size = new System.Drawing.Size(178, 338);
            this.panelForWellClasses.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panelImageAccess);
            this.tabPage3.Location = new System.Drawing.Point(4, 58);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(396, 401);
            this.tabPage3.TabIndex = 13;
            this.tabPage3.Text = "Image Access";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panelImageAccess
            // 
            this.panelImageAccess.AutoScroll = true;
            this.panelImageAccess.Controls.Add(this.panelForOptionsImagingPlateform);
            this.panelImageAccess.Controls.Add(this.treeViewForChoice);
            this.panelImageAccess.Controls.Add(this.groupBox10);
            this.panelImageAccess.Controls.Add(this.radioButtonImageAccessManual);
            this.panelImageAccess.Controls.Add(this.radioButtonImageAccessDefined);
            this.panelImageAccess.Controls.Add(this.groupBoxManual);
            this.panelImageAccess.Controls.Add(this.groupBoxDefined);
            this.panelImageAccess.Location = new System.Drawing.Point(0, 3);
            this.panelImageAccess.Name = "panelImageAccess";
            this.panelImageAccess.Size = new System.Drawing.Size(396, 398);
            this.panelImageAccess.TabIndex = 6;
            // 
            // panelForOptionsImagingPlateform
            // 
            this.panelForOptionsImagingPlateform.Enabled = false;
            this.panelForOptionsImagingPlateform.Location = new System.Drawing.Point(147, 549);
            this.panelForOptionsImagingPlateform.Name = "panelForOptionsImagingPlateform";
            this.panelForOptionsImagingPlateform.Size = new System.Drawing.Size(218, 123);
            this.panelForOptionsImagingPlateform.TabIndex = 7;
            // 
            // treeViewForChoice
            // 
            this.treeViewForChoice.Enabled = false;
            this.treeViewForChoice.Location = new System.Drawing.Point(6, 549);
            this.treeViewForChoice.Name = "treeViewForChoice";
            treeNode18.Name = "NodeHarmony";
            treeNode18.Text = "Harmony";
            treeNode19.Name = "NodeCellomics";
            treeNode19.Text = "Cellomics";
            treeNode20.Name = "NodeImageXPress";
            treeNode20.Text = "ImageXPress";
            treeNode21.Name = "Node0";
            treeNode21.Text = "PlateForm";
            treeNode22.Name = "NodeSingleCellOptions";
            treeNode22.Text = "Single Cell Options";
            this.treeViewForChoice.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode21,
            treeNode22});
            this.treeViewForChoice.Size = new System.Drawing.Size(134, 120);
            this.treeViewForChoice.TabIndex = 6;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.panelBoundingBox);
            this.groupBox10.Controls.Add(this.panelCenter);
            this.groupBox10.Controls.Add(this.radioButtonObjPosBoundingBox);
            this.groupBox10.Controls.Add(this.radioButtonObjPosCenter);
            this.groupBox10.Controls.Add(this.comboBoxDescriptorForField);
            this.groupBox10.Controls.Add(this.label38);
            this.groupBox10.Location = new System.Drawing.Point(6, 264);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(362, 276);
            this.groupBox10.TabIndex = 2;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Single Object Information";
            // 
            // panelBoundingBox
            // 
            this.panelBoundingBox.Controls.Add(this.comboBoxDescForBoundingMaxX);
            this.panelBoundingBox.Controls.Add(this.label40);
            this.panelBoundingBox.Controls.Add(this.label41);
            this.panelBoundingBox.Controls.Add(this.comboBoxDescForBoundingMaxY);
            this.panelBoundingBox.Controls.Add(this.comboBoxDescForBoundingMinX);
            this.panelBoundingBox.Controls.Add(this.label4);
            this.panelBoundingBox.Controls.Add(this.label39);
            this.panelBoundingBox.Controls.Add(this.comboBoxDescForBoundingMinY);
            this.panelBoundingBox.Location = new System.Drawing.Point(6, 139);
            this.panelBoundingBox.Name = "panelBoundingBox";
            this.panelBoundingBox.Size = new System.Drawing.Size(350, 120);
            this.panelBoundingBox.TabIndex = 19;
            // 
            // comboBoxDescForBoundingMaxX
            // 
            this.comboBoxDescForBoundingMaxX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDescForBoundingMaxX.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDescForBoundingMaxX.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDescForBoundingMaxX.FormattingEnabled = true;
            this.comboBoxDescForBoundingMaxX.Location = new System.Drawing.Point(68, 64);
            this.comboBoxDescForBoundingMaxX.Name = "comboBoxDescForBoundingMaxX";
            this.comboBoxDescForBoundingMaxX.Size = new System.Drawing.Size(280, 21);
            this.comboBoxDescForBoundingMaxX.TabIndex = 15;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(2, 67);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(37, 13);
            this.label40.TabIndex = 14;
            this.label40.Text = "Max X";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(2, 94);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(37, 13);
            this.label41.TabIndex = 16;
            this.label41.Text = "Max Y";
            // 
            // comboBoxDescForBoundingMaxY
            // 
            this.comboBoxDescForBoundingMaxY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDescForBoundingMaxY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDescForBoundingMaxY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDescForBoundingMaxY.FormattingEnabled = true;
            this.comboBoxDescForBoundingMaxY.Location = new System.Drawing.Point(68, 91);
            this.comboBoxDescForBoundingMaxY.Name = "comboBoxDescForBoundingMaxY";
            this.comboBoxDescForBoundingMaxY.Size = new System.Drawing.Size(280, 21);
            this.comboBoxDescForBoundingMaxY.TabIndex = 17;
            // 
            // comboBoxDescForBoundingMinX
            // 
            this.comboBoxDescForBoundingMinX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDescForBoundingMinX.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDescForBoundingMinX.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDescForBoundingMinX.FormattingEnabled = true;
            this.comboBoxDescForBoundingMinX.Location = new System.Drawing.Point(67, 3);
            this.comboBoxDescForBoundingMinX.Name = "comboBoxDescForBoundingMinX";
            this.comboBoxDescForBoundingMinX.Size = new System.Drawing.Size(280, 21);
            this.comboBoxDescForBoundingMinX.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Min X";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(1, 33);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(34, 13);
            this.label39.TabIndex = 12;
            this.label39.Text = "Min Y";
            // 
            // comboBoxDescForBoundingMinY
            // 
            this.comboBoxDescForBoundingMinY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDescForBoundingMinY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDescForBoundingMinY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDescForBoundingMinY.FormattingEnabled = true;
            this.comboBoxDescForBoundingMinY.Location = new System.Drawing.Point(67, 30);
            this.comboBoxDescForBoundingMinY.Name = "comboBoxDescForBoundingMinY";
            this.comboBoxDescForBoundingMinY.Size = new System.Drawing.Size(280, 21);
            this.comboBoxDescForBoundingMinY.TabIndex = 13;
            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.comboBoxDescriptorForPosX);
            this.panelCenter.Controls.Add(this.label36);
            this.panelCenter.Controls.Add(this.label37);
            this.panelCenter.Controls.Add(this.comboBoxDescriptorForPosY);
            this.panelCenter.Enabled = false;
            this.panelCenter.Location = new System.Drawing.Point(6, 79);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(350, 54);
            this.panelCenter.TabIndex = 18;
            // 
            // comboBoxDescriptorForPosX
            // 
            this.comboBoxDescriptorForPosX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDescriptorForPosX.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDescriptorForPosX.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDescriptorForPosX.FormattingEnabled = true;
            this.comboBoxDescriptorForPosX.Location = new System.Drawing.Point(67, 3);
            this.comboBoxDescriptorForPosX.Name = "comboBoxDescriptorForPosX";
            this.comboBoxDescriptorForPosX.Size = new System.Drawing.Size(280, 21);
            this.comboBoxDescriptorForPosX.TabIndex = 11;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(1, 6);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(48, 13);
            this.label36.TabIndex = 0;
            this.label36.Text = "Center X";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(1, 33);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(48, 13);
            this.label37.TabIndex = 12;
            this.label37.Text = "Center Y";
            // 
            // comboBoxDescriptorForPosY
            // 
            this.comboBoxDescriptorForPosY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDescriptorForPosY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDescriptorForPosY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDescriptorForPosY.FormattingEnabled = true;
            this.comboBoxDescriptorForPosY.Location = new System.Drawing.Point(67, 30);
            this.comboBoxDescriptorForPosY.Name = "comboBoxDescriptorForPosY";
            this.comboBoxDescriptorForPosY.Size = new System.Drawing.Size(280, 21);
            this.comboBoxDescriptorForPosY.TabIndex = 13;
            // 
            // radioButtonObjPosBoundingBox
            // 
            this.radioButtonObjPosBoundingBox.AutoSize = true;
            this.radioButtonObjPosBoundingBox.Checked = true;
            this.radioButtonObjPosBoundingBox.Location = new System.Drawing.Point(116, 55);
            this.radioButtonObjPosBoundingBox.Name = "radioButtonObjPosBoundingBox";
            this.radioButtonObjPosBoundingBox.Size = new System.Drawing.Size(91, 17);
            this.radioButtonObjPosBoundingBox.TabIndex = 17;
            this.radioButtonObjPosBoundingBox.TabStop = true;
            this.radioButtonObjPosBoundingBox.Text = "Bounding Box";
            this.radioButtonObjPosBoundingBox.UseVisualStyleBackColor = true;
            this.radioButtonObjPosBoundingBox.CheckedChanged += new System.EventHandler(this.radioButtonObjPosBoundingBox_CheckedChanged);
            // 
            // radioButtonObjPosCenter
            // 
            this.radioButtonObjPosCenter.AutoSize = true;
            this.radioButtonObjPosCenter.Location = new System.Drawing.Point(11, 55);
            this.radioButtonObjPosCenter.Name = "radioButtonObjPosCenter";
            this.radioButtonObjPosCenter.Size = new System.Drawing.Size(96, 17);
            this.radioButtonObjPosCenter.TabIndex = 16;
            this.radioButtonObjPosCenter.Text = "Center Position";
            this.radioButtonObjPosCenter.UseVisualStyleBackColor = true;
            this.radioButtonObjPosCenter.CheckedChanged += new System.EventHandler(this.radioButtonObjPosCenter_CheckedChanged);
            // 
            // comboBoxDescriptorForField
            // 
            this.comboBoxDescriptorForField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDescriptorForField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDescriptorForField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDescriptorForField.FormattingEnabled = true;
            this.comboBoxDescriptorForField.Location = new System.Drawing.Point(76, 19);
            this.comboBoxDescriptorForField.Name = "comboBoxDescriptorForField";
            this.comboBoxDescriptorForField.Size = new System.Drawing.Size(280, 21);
            this.comboBoxDescriptorForField.TabIndex = 15;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(10, 22);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(29, 13);
            this.label38.TabIndex = 14;
            this.label38.Text = "Field";
            // 
            // radioButtonImageAccessManual
            // 
            this.radioButtonImageAccessManual.AutoSize = true;
            this.radioButtonImageAccessManual.Location = new System.Drawing.Point(18, 6);
            this.radioButtonImageAccessManual.Name = "radioButtonImageAccessManual";
            this.radioButtonImageAccessManual.Size = new System.Drawing.Size(60, 17);
            this.radioButtonImageAccessManual.TabIndex = 1;
            this.radioButtonImageAccessManual.Text = "Manual";
            this.radioButtonImageAccessManual.UseVisualStyleBackColor = true;
            this.radioButtonImageAccessManual.CheckedChanged += new System.EventHandler(this.radioButtonImageAccessManual_CheckedChanged);
            // 
            // radioButtonImageAccessDefined
            // 
            this.radioButtonImageAccessDefined.AutoSize = true;
            this.radioButtonImageAccessDefined.Checked = true;
            this.radioButtonImageAccessDefined.Location = new System.Drawing.Point(17, 60);
            this.radioButtonImageAccessDefined.Name = "radioButtonImageAccessDefined";
            this.radioButtonImageAccessDefined.Size = new System.Drawing.Size(81, 17);
            this.radioButtonImageAccessDefined.TabIndex = 5;
            this.radioButtonImageAccessDefined.TabStop = true;
            this.radioButtonImageAccessDefined.Text = "Pre-Defined";
            this.radioButtonImageAccessDefined.UseVisualStyleBackColor = true;
            this.radioButtonImageAccessDefined.CheckedChanged += new System.EventHandler(this.radioButtonImageAccessDefined_CheckedChanged);
            // 
            // groupBoxManual
            // 
            this.groupBoxManual.Controls.Add(this.label6);
            this.groupBoxManual.Location = new System.Drawing.Point(6, 8);
            this.groupBoxManual.Name = "groupBoxManual";
            this.groupBoxManual.Size = new System.Drawing.Size(362, 49);
            this.groupBoxManual.TabIndex = 0;
            this.groupBoxManual.TabStop = false;
            this.groupBoxManual.Text = "                      ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(281, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "The image path is defined by the \"Info\" value of each well";
            // 
            // groupBoxDefined
            // 
            this.groupBoxDefined.Controls.Add(this.radioButtonImageAccessBuiltIn);
            this.groupBoxDefined.Controls.Add(this.radioButtonImageAccessCV7000);
            this.groupBoxDefined.Controls.Add(this.radioButtonImageAccessINCell);
            this.groupBoxDefined.Controls.Add(this.radioButtonImageAccessCellomics);
            this.groupBoxDefined.Controls.Add(this.label35);
            this.groupBoxDefined.Controls.Add(this.numericUpDownDefaultField);
            this.groupBoxDefined.Controls.Add(this.label7);
            this.groupBoxDefined.Controls.Add(this.numericUpDownImageAccessNumberOfFields);
            this.groupBoxDefined.Controls.Add(this.radioButtonImageAccessHarmony35);
            this.groupBoxDefined.Controls.Add(this.radioButtonImageAccessImageXpress);
            this.groupBoxDefined.Controls.Add(this.buttonUpdateImagePath);
            this.groupBoxDefined.Controls.Add(this.textBoxImageAccesImagePath);
            this.groupBoxDefined.Controls.Add(this.radioButtonImageAccessHarmony);
            this.groupBoxDefined.Controls.Add(this.labelPath);
            this.groupBoxDefined.Controls.Add(this.label34);
            this.groupBoxDefined.Controls.Add(this.numericUpDownImageAccessNumberOfChannels);
            this.groupBoxDefined.Enabled = false;
            this.groupBoxDefined.Location = new System.Drawing.Point(6, 62);
            this.groupBoxDefined.Name = "groupBoxDefined";
            this.groupBoxDefined.Size = new System.Drawing.Size(362, 196);
            this.groupBoxDefined.TabIndex = 1;
            this.groupBoxDefined.TabStop = false;
            this.groupBoxDefined.Text = "                       ";
            // 
            // radioButtonImageAccessBuiltIn
            // 
            this.radioButtonImageAccessBuiltIn.AutoSize = true;
            this.radioButtonImageAccessBuiltIn.Location = new System.Drawing.Point(180, 44);
            this.radioButtonImageAccessBuiltIn.Name = "radioButtonImageAccessBuiltIn";
            this.radioButtonImageAccessBuiltIn.Size = new System.Drawing.Size(56, 17);
            this.radioButtonImageAccessBuiltIn.TabIndex = 14;
            this.radioButtonImageAccessBuiltIn.Text = "Built in";
            this.radioButtonImageAccessBuiltIn.UseVisualStyleBackColor = true;
            this.radioButtonImageAccessBuiltIn.CheckedChanged += new System.EventHandler(this.radioButtonImageAccessBuiltIn_CheckedChanged);
            // 
            // radioButtonImageAccessCV7000
            // 
            this.radioButtonImageAccessCV7000.AutoSize = true;
            this.radioButtonImageAccessCV7000.Checked = true;
            this.radioButtonImageAccessCV7000.Location = new System.Drawing.Point(83, 44);
            this.radioButtonImageAccessCV7000.Name = "radioButtonImageAccessCV7000";
            this.radioButtonImageAccessCV7000.Size = new System.Drawing.Size(63, 17);
            this.radioButtonImageAccessCV7000.TabIndex = 13;
            this.radioButtonImageAccessCV7000.TabStop = true;
            this.radioButtonImageAccessCV7000.Text = "CV7000";
            this.radioButtonImageAccessCV7000.UseVisualStyleBackColor = true;
            this.radioButtonImageAccessCV7000.CheckedChanged += new System.EventHandler(this.radioButtonImageAccessCV7000_CheckedChanged);
            // 
            // radioButtonImageAccessINCell
            // 
            this.radioButtonImageAccessINCell.AutoSize = true;
            this.radioButtonImageAccessINCell.Location = new System.Drawing.Point(13, 44);
            this.radioButtonImageAccessINCell.Name = "radioButtonImageAccessINCell";
            this.radioButtonImageAccessINCell.Size = new System.Drawing.Size(56, 17);
            this.radioButtonImageAccessINCell.TabIndex = 12;
            this.radioButtonImageAccessINCell.Text = "IN Cell";
            this.radioButtonImageAccessINCell.UseVisualStyleBackColor = true;
            this.radioButtonImageAccessINCell.CheckedChanged += new System.EventHandler(this.radioButtonImageAccessINCell_CheckedChanged);
            // 
            // radioButtonImageAccessCellomics
            // 
            this.radioButtonImageAccessCellomics.AutoSize = true;
            this.radioButtonImageAccessCellomics.Location = new System.Drawing.Point(273, 23);
            this.radioButtonImageAccessCellomics.Name = "radioButtonImageAccessCellomics";
            this.radioButtonImageAccessCellomics.Size = new System.Drawing.Size(69, 17);
            this.radioButtonImageAccessCellomics.TabIndex = 11;
            this.radioButtonImageAccessCellomics.Text = "Cellomics";
            this.radioButtonImageAccessCellomics.UseVisualStyleBackColor = true;
            this.radioButtonImageAccessCellomics.CheckedChanged += new System.EventHandler(this.radioButtonImageAccessCellomics_CheckedChanged);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(219, 118);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(66, 13);
            this.label35.TabIndex = 10;
            this.label35.Text = "Default Field";
            // 
            // numericUpDownDefaultField
            // 
            this.numericUpDownDefaultField.Location = new System.Drawing.Point(292, 115);
            this.numericUpDownDefaultField.Name = "numericUpDownDefaultField";
            this.numericUpDownDefaultField.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownDefaultField.TabIndex = 9;
            this.numericUpDownDefaultField.ValueChanged += new System.EventHandler(this.numericUpDownDefaultField_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Number of Fields";
            // 
            // numericUpDownImageAccessNumberOfFields
            // 
            this.numericUpDownImageAccessNumberOfFields.Location = new System.Drawing.Point(119, 114);
            this.numericUpDownImageAccessNumberOfFields.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownImageAccessNumberOfFields.Name = "numericUpDownImageAccessNumberOfFields";
            this.numericUpDownImageAccessNumberOfFields.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownImageAccessNumberOfFields.TabIndex = 7;
            this.numericUpDownImageAccessNumberOfFields.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownImageAccessNumberOfFields.ValueChanged += new System.EventHandler(this.numericUpDownImageAccessNumberOfFields_ValueChanged);
            // 
            // radioButtonImageAccessHarmony35
            // 
            this.radioButtonImageAccessHarmony35.AutoSize = true;
            this.radioButtonImageAccessHarmony35.Location = new System.Drawing.Point(83, 22);
            this.radioButtonImageAccessHarmony35.Name = "radioButtonImageAccessHarmony35";
            this.radioButtonImageAccessHarmony35.Size = new System.Drawing.Size(91, 17);
            this.radioButtonImageAccessHarmony35.TabIndex = 6;
            this.radioButtonImageAccessHarmony35.Text = "Harmony v3.5";
            this.radioButtonImageAccessHarmony35.UseVisualStyleBackColor = true;
            this.radioButtonImageAccessHarmony35.CheckedChanged += new System.EventHandler(this.radioButtonImageAccessOperetta35_CheckedChanged);
            // 
            // radioButtonImageAccessImageXpress
            // 
            this.radioButtonImageAccessImageXpress.AutoSize = true;
            this.radioButtonImageAccessImageXpress.Location = new System.Drawing.Point(180, 23);
            this.radioButtonImageAccessImageXpress.Name = "radioButtonImageAccessImageXpress";
            this.radioButtonImageAccessImageXpress.Size = new System.Drawing.Size(87, 17);
            this.radioButtonImageAccessImageXpress.TabIndex = 5;
            this.radioButtonImageAccessImageXpress.Text = "ImageXPress";
            this.radioButtonImageAccessImageXpress.UseVisualStyleBackColor = true;
            this.radioButtonImageAccessImageXpress.CheckedChanged += new System.EventHandler(this.radioButtonImageAccessImageXpress_CheckedChanged);
            // 
            // buttonUpdateImagePath
            // 
            this.buttonUpdateImagePath.Location = new System.Drawing.Point(329, 160);
            this.buttonUpdateImagePath.Name = "buttonUpdateImagePath";
            this.buttonUpdateImagePath.Size = new System.Drawing.Size(27, 23);
            this.buttonUpdateImagePath.TabIndex = 4;
            this.buttonUpdateImagePath.Text = "...";
            this.buttonUpdateImagePath.UseVisualStyleBackColor = true;
            this.buttonUpdateImagePath.Click += new System.EventHandler(this.buttonUpdateImagePath_Click);
            // 
            // textBoxImageAccesImagePath
            // 
            this.textBoxImageAccesImagePath.Location = new System.Drawing.Point(13, 162);
            this.textBoxImageAccesImagePath.Name = "textBoxImageAccesImagePath";
            this.textBoxImageAccesImagePath.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxImageAccesImagePath.Size = new System.Drawing.Size(310, 20);
            this.textBoxImageAccesImagePath.TabIndex = 3;
            this.textBoxImageAccesImagePath.Text = "Z:\\BTSData\\MeasurementData\\DMD-IC50-VL";
            // 
            // radioButtonImageAccessHarmony
            // 
            this.radioButtonImageAccessHarmony.AutoSize = true;
            this.radioButtonImageAccessHarmony.Location = new System.Drawing.Point(13, 21);
            this.radioButtonImageAccessHarmony.Name = "radioButtonImageAccessHarmony";
            this.radioButtonImageAccessHarmony.Size = new System.Drawing.Size(67, 17);
            this.radioButtonImageAccessHarmony.TabIndex = 1;
            this.radioButtonImageAccessHarmony.Text = "Harmony";
            this.radioButtonImageAccessHarmony.UseVisualStyleBackColor = true;
            this.radioButtonImageAccessHarmony.CheckedChanged += new System.EventHandler(this.radioButtonImageAccessOperetta_CheckedChanged);
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(10, 146);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(61, 13);
            this.labelPath.TabIndex = 2;
            this.labelPath.Text = "Image Path";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(10, 89);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(103, 13);
            this.label34.TabIndex = 1;
            this.label34.Text = "Number of Channels";
            // 
            // numericUpDownImageAccessNumberOfChannels
            // 
            this.numericUpDownImageAccessNumberOfChannels.Location = new System.Drawing.Point(119, 87);
            this.numericUpDownImageAccessNumberOfChannels.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownImageAccessNumberOfChannels.Name = "numericUpDownImageAccessNumberOfChannels";
            this.numericUpDownImageAccessNumberOfChannels.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownImageAccessNumberOfChannels.TabIndex = 0;
            this.numericUpDownImageAccessNumberOfChannels.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tabPageImageDispProperties
            // 
            this.tabPageImageDispProperties.Controls.Add(this.panelForCurrentLUTList);
            this.tabPageImageDispProperties.Location = new System.Drawing.Point(4, 58);
            this.tabPageImageDispProperties.Name = "tabPageImageDispProperties";
            this.tabPageImageDispProperties.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageImageDispProperties.Size = new System.Drawing.Size(396, 401);
            this.tabPageImageDispProperties.TabIndex = 14;
            this.tabPageImageDispProperties.Text = "Image Display";
            this.tabPageImageDispProperties.UseVisualStyleBackColor = true;
            // 
            // panelForCurrentLUTList
            // 
            this.panelForCurrentLUTList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForCurrentLUTList.AutoScroll = true;
            this.panelForCurrentLUTList.ContextMenuStrip = this.contextMenuStripForImageDisplay;
            this.panelForCurrentLUTList.Location = new System.Drawing.Point(6, 6);
            this.panelForCurrentLUTList.Name = "panelForCurrentLUTList";
            this.panelForCurrentLUTList.Size = new System.Drawing.Size(384, 389);
            this.panelForCurrentLUTList.TabIndex = 0;
            // 
            // contextMenuStripForImageDisplay
            // 
            this.contextMenuStripForImageDisplay.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem});
            this.contextMenuStripForImageDisplay.Name = "contextMenuStripForImageDisplay";
            this.contextMenuStripForImageDisplay.Size = new System.Drawing.Size(102, 26);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(271, 496);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(144, 32);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(12, 496);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(144, 32);
            this.buttonApply.TabIndex = 2;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // menuStripIO
            // 
            this.menuStripIO.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iOToolStripMenuItem});
            this.menuStripIO.Location = new System.Drawing.Point(0, 0);
            this.menuStripIO.Name = "menuStripIO";
            this.menuStripIO.Size = new System.Drawing.Size(427, 24);
            this.menuStripIO.TabIndex = 3;
            this.menuStripIO.Text = "menuStrip1";
            // 
            // iOToolStripMenuItem
            // 
            this.iOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveDefaultToolStripMenuItem});
            this.iOToolStripMenuItem.Name = "iOToolStripMenuItem";
            this.iOToolStripMenuItem.Size = new System.Drawing.Size(31, 20);
            this.iOToolStripMenuItem.Text = "IO";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click_1);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // saveDefaultToolStripMenuItem
            // 
            this.saveDefaultToolStripMenuItem.Name = "saveDefaultToolStripMenuItem";
            this.saveDefaultToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveDefaultToolStripMenuItem.Text = "Save as default";
            this.saveDefaultToolStripMenuItem.Click += new System.EventHandler(this.saveDefaultToolStripMenuItem_Click);
            // 
            // FormForOptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 535);
            this.ControlBox = false;
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.menuStripIO);
            this.Controls.Add(this.tabControlWindowOption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripIO;
            this.MaximizeBox = false;
            this.Name = "FormForOptionsWindow";
            this.Text = "Options";
            this.tabControlWindowOption.ResumeLayout(false);
            this.tabPageImport.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageDisplay.ResumeLayout(false);
            this.tabPageClustering.ResumeLayout(false);
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.tabPageClassification.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJ48MinNumObjects)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKofKNN)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPageMiscInfo.ResumeLayout(false);
            this.tabPageMiscInfo.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeEffectDeltaMult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeEffectMaxMult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeEffectDeltaShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeEffectMinMult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeEffectMaxShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeEffectMinShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeEffectMaximumIteration)).EndInit();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSystErrorIdentKMeansClasses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSystemMinWellRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSystErrorAndersonDarlingThreshold)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGenerateScreenDiffusion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGenerateScreenRatioXY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGenerateScreenRowEffectShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGenerateScreenNoiseStdDev)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.groupBox25.ResumeLayout(false);
            this.groupBox25.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.tabPage3D.ResumeLayout(false);
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3DThinPlateRegularization)).EndInit();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDRCOpacity)).EndInit();
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWellSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWellOpacity)).EndInit();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.groupBox26.ResumeLayout(false);
            this.groupBox26.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownManualMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownManualMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutomatedMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutomatedMin)).EndInit();
            this.groupBox24.ResumeLayout(false);
            this.groupBox24.PerformLayout();
            this.tabPageClasses.ResumeLayout(false);
            this.groupBoxPhenotypes.ResumeLayout(false);
            this.groupBox28.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panelImageAccess.ResumeLayout(false);
            this.panelImageAccess.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.panelBoundingBox.ResumeLayout(false);
            this.panelBoundingBox.PerformLayout();
            this.panelCenter.ResumeLayout(false);
            this.panelCenter.PerformLayout();
            this.groupBoxManual.ResumeLayout(false);
            this.groupBoxManual.PerformLayout();
            this.groupBoxDefined.ResumeLayout(false);
            this.groupBoxDefined.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDefaultField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageAccessNumberOfFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageAccessNumberOfChannels)).EndInit();
            this.tabPageImageDispProperties.ResumeLayout(false);
            this.contextMenuStripForImageDisplay.ResumeLayout(false);
            this.menuStripIO.ResumeLayout(false);
            this.menuStripIO.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageDisplay;
        private System.Windows.Forms.TabPage tabPageClassification;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.RadioButton radioButtonWellPosModeDouble;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.RadioButton radioButtonWellPosModeSingle;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.CheckBox checkBoxSetActiveOnlyForNamed;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.CheckBox checkBoxCorrelationMatrixDisplayRanking;
        public System.Windows.Forms.RadioButton radioButtonPearson;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.NumericUpDown numericUpDownKofKNN;
        private System.Windows.Forms.TabPage tabPageMiscInfo;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RadioButton radioButtonGoogle;
        public System.Windows.Forms.RadioButton radioButtonIE;
        private System.Windows.Forms.TabPage tabPage4;
        public System.Windows.Forms.NumericUpDown numericUpDownSystErrorIdentKMeansClasses;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.NumericUpDown numericUpDownSystemMinWellRatio;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.NumericUpDown numericUpDownSystErrorAndersonDarlingThreshold;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.GroupBox groupBox12;
        public System.Windows.Forms.NumericUpDown numericUpDownEdgeEffectMaximumIteration;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBox14;
        public System.Windows.Forms.NumericUpDown numericUpDownGenerateScreenNoiseStdDev;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.NumericUpDown numericUpDownGenerateScreenRowEffectShift;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.NumericUpDown numericUpDownGenerateScreenRatioXY;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.NumericUpDown numericUpDownGenerateScreenDiffusion;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox15;
        public System.Windows.Forms.NumericUpDown numericUpDownJ48MinNumObjects;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox16;
        public System.Windows.Forms.CheckBox checkBoxCorrelationRankChangeColorForActiveDesc;
        private System.Windows.Forms.GroupBox groupBox18;
        public System.Windows.Forms.ComboBox comboBoxHierarchicalDistance;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.ComboBox comboBoxHierarchicalLinkType;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label20;
        public System.Windows.Forms.NumericUpDown numericUpDownEdgeEffectDeltaMult;
        public System.Windows.Forms.NumericUpDown numericUpDownEdgeEffectMaxMult;
        public System.Windows.Forms.NumericUpDown numericUpDownEdgeEffectDeltaShift;
        public System.Windows.Forms.NumericUpDown numericUpDownEdgeEffectMinMult;
        public System.Windows.Forms.NumericUpDown numericUpDownEdgeEffectMaxShift;
        private System.Windows.Forms.Label label22;
        public System.Windows.Forms.NumericUpDown numericUpDownEdgeEffectMinShift;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button buttonDRCPlateDesign;
        private System.Windows.Forms.GroupBox groupBox19;
        public System.Windows.Forms.RadioButton radioButtonReplicatesAverageStdev;
        public System.Windows.Forms.RadioButton radioButtonReplicateAllValues;
        private System.Windows.Forms.GroupBox groupBox21;
        public System.Windows.Forms.NumericUpDown numericUpDownDRCOpacity;
        private System.Windows.Forms.Label label26;
        public System.Windows.Forms.NumericUpDown numericUpDownWellOpacity;
        private System.Windows.Forms.Label label25;
        public System.Windows.Forms.NumericUpDown numericUpDownWellSize;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.GroupBox groupBox22;
        public System.Windows.Forms.CheckBox checkBox3DDisplayIsoRatioCurves;
        public System.Windows.Forms.CheckBox checkBox3DDisplayIsoboles;
        private System.Windows.Forms.GroupBox groupBox20;
        public System.Windows.Forms.CheckBox checkBox3DDisplayThinPlate;
        public System.Windows.Forms.NumericUpDown numericUpDown3DThinPlateRegularization;
        private System.Windows.Forms.Label label28;
        public System.Windows.Forms.CheckBox checkBox3DComputeThinPlate;
        public System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox23;
        public System.Windows.Forms.CheckBox checkBox3DPlateInformation;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.GroupBox groupBox24;
        public System.Windows.Forms.RadioButton radioButtonDistributionMetricManhattan;
        public System.Windows.Forms.RadioButton radioButtonDistributionMetricEuclidean;
        public System.Windows.Forms.RadioButton radioButtonSpearman;
        public System.Windows.Forms.RadioButton radioButtonMIC;
        public System.Windows.Forms.RadioButton radioButtonDistributionMetricCosine;
        public System.Windows.Forms.RadioButton radioButtonDistributionMetricBhattacharyya;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        public System.Windows.Forms.RadioButton radioButtonDistributionMetricEMD;
        public System.Windows.Forms.TabControl tabControlWindowOption;
        public System.Windows.Forms.TabPage tabPage3D;
        public System.Windows.Forms.TabPage tabPageClustering;
        public System.Windows.Forms.TabPage tabPageImport;
        private System.Windows.Forms.GroupBox groupBox25;
        public System.Windows.Forms.CheckBox checkBoxConnectDRCPts;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.GroupBox groupBox26;
        private System.Windows.Forms.Label label33;
        public System.Windows.Forms.NumericUpDown numericUpDownAutomatedMax;
        private System.Windows.Forms.Label label32;
        public System.Windows.Forms.NumericUpDown numericUpDownAutomatedMin;
        public System.Windows.Forms.RadioButton radioButtonHistoDisplayAutomatedMinMax;
        public System.Windows.Forms.NumericUpDown numericUpDownManualMax;
        public System.Windows.Forms.NumericUpDown numericUpDownManualMin;
        public System.Windows.Forms.RadioButton radioButtonHistoDisplayManualMinMax;
        public System.Windows.Forms.RadioButton radioButtonHistoDisplayAdjusted;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.TabPage tabPageClasses;
        public System.Windows.Forms.Panel panelForWellClasses;
        private System.Windows.Forms.GroupBox groupBoxPhenotypes;
        public System.Windows.Forms.Panel panelForCellularPhenotypes;
        private System.Windows.Forms.GroupBox groupBox28;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBoxDefined;
        public System.Windows.Forms.RadioButton radioButtonImageAccessHarmony;
        private System.Windows.Forms.GroupBox groupBoxManual;
        public System.Windows.Forms.RadioButton radioButtonImageAccessManual;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonUpdateImagePath;
        public System.Windows.Forms.TextBox textBoxImageAccesImagePath;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Label label34;
        public System.Windows.Forms.NumericUpDown numericUpDownImageAccessNumberOfChannels;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label36;
        public System.Windows.Forms.ComboBox comboBoxDescriptorForPosX;
        public System.Windows.Forms.ComboBox comboBoxDescriptorForPosY;
        private System.Windows.Forms.Label label37;
        public System.Windows.Forms.ComboBox comboBoxDescriptorForField;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.MenuStrip menuStripIO;
        private System.Windows.Forms.ToolStripMenuItem iOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        public System.Windows.Forms.RadioButton radioButtonImageAccessDefined;
        public System.Windows.Forms.RadioButton radioButtonImageAccessImageXpress;
        private System.Windows.Forms.Panel panelImageAccess;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.RadioButton radioButtonObjPosBoundingBox;
        private System.Windows.Forms.RadioButton radioButtonObjPosCenter;
        private System.Windows.Forms.Panel panelBoundingBox;
        public System.Windows.Forms.ComboBox comboBoxDescForBoundingMaxX;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        public System.Windows.Forms.ComboBox comboBoxDescForBoundingMaxY;
        public System.Windows.Forms.ComboBox comboBoxDescForBoundingMinX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label39;
        public System.Windows.Forms.ComboBox comboBoxDescForBoundingMinY;
        public System.Windows.Forms.RadioButton radioButtonImageAccessHarmony35;
        private System.Windows.Forms.ToolStripMenuItem saveDefaultToolStripMenuItem;
        private System.Windows.Forms.Label label35;
        public System.Windows.Forms.NumericUpDown numericUpDownDefaultField;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown numericUpDownImageAccessNumberOfFields;
        private System.Windows.Forms.TabPage tabPageImageDispProperties;
        public System.Windows.Forms.Panel panelForCurrentLUTList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForImageDisplay;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        public System.Windows.Forms.RadioButton radioButtonImageAccessCellomics;
        private System.Windows.Forms.Label label42;
        public System.Windows.Forms.RichTextBox richTextBoxSystemInfo;
        private System.Windows.Forms.Panel panelForOptionsImagingPlateform;
        private System.Windows.Forms.TreeView treeViewForChoice;
        private System.Windows.Forms.TreeView treeViewOptions;
        public System.Windows.Forms.Panel panelForSpecificOption;
        public System.Windows.Forms.RadioButton radioButtonImageAccessINCell;
        public System.Windows.Forms.RadioButton radioButtonImageAccessCV7000;
        public System.Windows.Forms.RadioButton radioButtonImageAccessBuiltIn;
    }
}