using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using IM.Imaging;
using IM.GlobalTools;

using IM;
using IM.Plugin;
using IM.Library.Tools;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

using IM.Library.IO;
using IM.Library.Mathematics;
using System.Windows.Forms.DataVisualization.Charting;

namespace ImHeap
{
    public partial class MainClassHeap : IM.Plugin.Plugin
    {
        private Sequence sTmp;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainClassHeap));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.listViewHeap = new System.Windows.Forms.ListView();
            this.columnHeaderIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSelection = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderPictures = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripForHeap = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.activateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.depthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelToolStripMenuItemSplit = new System.Windows.Forms.ToolStripMenuItem();
            this.depthSplitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marginalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.substractToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.divideToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sqrtToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.multiplyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.anscombeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.complexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.substractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multiplyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.divideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acumulateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filteringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gaussianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gaussianAndDivideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medianAndDivideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNoiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gaussianNoiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uniformNoiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.poissonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.distancesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meanSquareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.segmentationEvaluationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kappaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hausdorffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pSNRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unitImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nullImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vectorFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.isoLevelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vGradientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hGardientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centralGradientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayInfosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meanValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maxValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gradSmoothnessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleStationarityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stationirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastStationarityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.combineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveHeapPicturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListForHeap = new System.Windows.Forms.ImageList(this.components);
            this.textBoxInfoPicture = new System.Windows.Forms.TextBox();
            this.textBoxImageName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStripHistogram = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveToFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.barToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageInfos = new System.Windows.Forms.TabPage();
            this.numericUpDownZRes = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownYRes = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownXRes = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPageHisto = new System.Windows.Forms.TabPage();
            this.numericUpDownMinimumHisto = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMaximumHisto = new System.Windows.Forms.NumericUpDown();
            this.chartHisto = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownChannelForHisto = new System.Windows.Forms.NumericUpDown();
            this.buttonOperaFactor = new System.Windows.Forms.Button();
            this.numericUpDownMinHisto = new System.Windows.Forms.NumericUpDown();
            this.labelPosition = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSetToMax = new System.Windows.Forms.Button();
            this.labelValueHisto = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSetToMin = new System.Windows.Forms.Button();
            this.numericUpDownStepHisto = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMaxHisto = new System.Windows.Forms.NumericUpDown();
            this.checkBoxHistoLog = new System.Windows.Forms.CheckBox();
            this.radioButtonHistoTypeCumulated = new System.Windows.Forms.RadioButton();
            this.radioButtonHistoTypeClassic = new System.Windows.Forms.RadioButton();
            this.tabPageLineProfil = new System.Windows.Forms.TabPage();
            this.chartLineProfile = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.contextMenuStripLineProfil = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonDepthLine = new System.Windows.Forms.RadioButton();
            this.radioButtonLine = new System.Windows.Forms.RadioButton();
            this.numericUpDownHistoLinePosition = new System.Windows.Forms.NumericUpDown();
            this.radioButtonColumn = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPageJointHisto = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButtonImage1Channel1 = new System.Windows.Forms.RadioButton();
            this.radioButtonImage1Channel2 = new System.Windows.Forms.RadioButton();
            this.radioButtonImage1Channel3 = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButtonImage0Channel1 = new System.Windows.Forms.RadioButton();
            this.radioButtonImage0Channel2 = new System.Windows.Forms.RadioButton();
            this.radioButtonImage0Channel3 = new System.Windows.Forms.RadioButton();
            this.panelJointHisto = new System.Windows.Forms.Panel();
            this.checkBoxGetOnlyDisplayedZ = new System.Windows.Forms.CheckBox();
            this.checkBoxLoadAllTheSequence = new System.Windows.Forms.CheckBox();
            this.sphereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripForHeap.SuspendLayout();
            this.contextMenuStripHistogram.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageInfos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZRes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYRes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXRes)).BeginInit();
            this.tabPageHisto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimumHisto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaximumHisto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHisto)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChannelForHisto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinHisto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStepHisto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxHisto)).BeginInit();
            this.tabPageLineProfil.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartLineProfile)).BeginInit();
            this.contextMenuStripLineProfil.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHistoLinePosition)).BeginInit();
            this.tabPageJointHisto.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewHeap
            // 
            this.listViewHeap.AllowDrop = true;
            this.listViewHeap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewHeap.AutoArrange = false;
            this.listViewHeap.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderIndex,
            this.columnHeaderSelection,
            this.columnHeaderPictures});
            this.listViewHeap.ContextMenuStrip = this.contextMenuStripForHeap;
            this.listViewHeap.LargeImageList = this.imageListForHeap;
            this.listViewHeap.Location = new System.Drawing.Point(6, 12);
            this.listViewHeap.Name = "listViewHeap";
            this.listViewHeap.Size = new System.Drawing.Size(128, 502);
            this.listViewHeap.TabIndex = 2;
            this.listViewHeap.UseCompatibleStateImageBehavior = false;
            this.listViewHeap.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listViewHeap_ItemDrag);
            this.listViewHeap.SelectedIndexChanged += new System.EventHandler(this.listViewHeap_SelectedIndexChanged);
            this.listViewHeap.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewHeap_DragDrop);
            this.listViewHeap.DragOver += new System.Windows.Forms.DragEventHandler(this.listViewHeap_DragOver);
            this.listViewHeap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewHeap_KeyDown);
            this.listViewHeap.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewHeap_MouseDoubleClick);
            // 
            // columnHeaderIndex
            // 
            this.columnHeaderIndex.Text = "Index";
            // 
            // columnHeaderSelection
            // 
            this.columnHeaderSelection.Text = "Selection";
            // 
            // columnHeaderPictures
            // 
            this.columnHeaderPictures.Text = "Images";
            // 
            // contextMenuStripForHeap
            // 
            this.contextMenuStripForHeap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activateToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.deleteAllToolStripMenuItem,
            this.duplicateToolStripMenuItem,
            this.mergeToolStripMenuItem,
            this.splitByToolStripMenuItem,
            this.operationsToolStripMenuItem,
            this.distancesToolStripMenuItem,
            this.createToolStripMenuItem,
            this.infosToolStripMenuItem,
            this.combineToolStripMenuItem,
            this.saveHeapPicturesToolStripMenuItem});
            this.contextMenuStripForHeap.Name = "contextMenuStripForHeap";
            this.contextMenuStripForHeap.Size = new System.Drawing.Size(175, 290);
            // 
            // activateToolStripMenuItem
            // 
            this.activateToolStripMenuItem.Name = "activateToolStripMenuItem";
            this.activateToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.activateToolStripMenuItem.Text = "Activate";
            this.activateToolStripMenuItem.Click += new System.EventHandler(this.activateToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.deleteToolStripMenuItem.Text = "delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // deleteAllToolStripMenuItem
            // 
            this.deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
            this.deleteAllToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.deleteAllToolStripMenuItem.Text = "Delete all";
            this.deleteAllToolStripMenuItem.Click += new System.EventHandler(this.deleteAllToolStripMenuItem_Click);
            // 
            // duplicateToolStripMenuItem
            // 
            this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.duplicateToolStripMenuItem.Text = "Duplicate";
            this.duplicateToolStripMenuItem.Click += new System.EventHandler(this.duplicateToolStripMenuItem_Click);
            // 
            // mergeToolStripMenuItem
            // 
            this.mergeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.channelToolStripMenuItem,
            this.depthToolStripMenuItem,
            this.timeToolStripMenuItem});
            this.mergeToolStripMenuItem.Name = "mergeToolStripMenuItem";
            this.mergeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.mergeToolStripMenuItem.Text = "Merge by";
            // 
            // channelToolStripMenuItem
            // 
            this.channelToolStripMenuItem.Name = "channelToolStripMenuItem";
            this.channelToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.channelToolStripMenuItem.Text = "Channel";
            this.channelToolStripMenuItem.Click += new System.EventHandler(this.channelToolStripMenuItem_Click);
            // 
            // depthToolStripMenuItem
            // 
            this.depthToolStripMenuItem.Name = "depthToolStripMenuItem";
            this.depthToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.depthToolStripMenuItem.Text = "Depth";
            this.depthToolStripMenuItem.Click += new System.EventHandler(this.depthToolStripMenuItem_Click);
            // 
            // timeToolStripMenuItem
            // 
            this.timeToolStripMenuItem.Name = "timeToolStripMenuItem";
            this.timeToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.timeToolStripMenuItem.Text = "Time";
            this.timeToolStripMenuItem.Click += new System.EventHandler(this.timeToolStripMenuItem_Click);
            // 
            // splitByToolStripMenuItem
            // 
            this.splitByToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.channelToolStripMenuItemSplit,
            this.depthSplitToolStripMenuItem});
            this.splitByToolStripMenuItem.Name = "splitByToolStripMenuItem";
            this.splitByToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.splitByToolStripMenuItem.Text = "Split by";
            // 
            // channelToolStripMenuItemSplit
            // 
            this.channelToolStripMenuItemSplit.Name = "channelToolStripMenuItemSplit";
            this.channelToolStripMenuItemSplit.Size = new System.Drawing.Size(123, 22);
            this.channelToolStripMenuItemSplit.Text = "Channels";
            this.channelToolStripMenuItemSplit.Click += new System.EventHandler(this.channelToolStripMenuItem1_Click);
            // 
            // depthSplitToolStripMenuItem
            // 
            this.depthSplitToolStripMenuItem.Name = "depthSplitToolStripMenuItem";
            this.depthSplitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.depthSplitToolStripMenuItem.Text = "Depth";
            this.depthSplitToolStripMenuItem.Click += new System.EventHandler(this.depthSplitToolStripMenuItem_Click);
            // 
            // operationsToolStripMenuItem
            // 
            this.operationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.marginalToolStripMenuItem,
            this.complexToolStripMenuItem,
            this.acumulateToolStripMenuItem,
            this.filteringToolStripMenuItem,
            this.addNoiseToolStripMenuItem});
            this.operationsToolStripMenuItem.Name = "operationsToolStripMenuItem";
            this.operationsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.operationsToolStripMenuItem.Text = "Operations";
            // 
            // marginalToolStripMenuItem
            // 
            this.marginalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem1,
            this.substractToolStripMenuItem1,
            this.divideToolStripMenuItem1,
            this.sqrtToolStripMenuItem1,
            this.multiplyToolStripMenuItem1,
            this.lnToolStripMenuItem,
            this.anscombeToolStripMenuItem,
            this.normalizeToolStripMenuItem});
            this.marginalToolStripMenuItem.Name = "marginalToolStripMenuItem";
            this.marginalToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.marginalToolStripMenuItem.Text = "Marginal";
            // 
            // addToolStripMenuItem1
            // 
            this.addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            this.addToolStripMenuItem1.Size = new System.Drawing.Size(131, 22);
            this.addToolStripMenuItem1.Text = "Add";
            this.addToolStripMenuItem1.Click += new System.EventHandler(this.addToolStripMenuItem1_Click);
            // 
            // substractToolStripMenuItem1
            // 
            this.substractToolStripMenuItem1.Name = "substractToolStripMenuItem1";
            this.substractToolStripMenuItem1.Size = new System.Drawing.Size(131, 22);
            this.substractToolStripMenuItem1.Text = "Substract";
            this.substractToolStripMenuItem1.Click += new System.EventHandler(this.substractToolStripMenuItem1_Click);
            // 
            // divideToolStripMenuItem1
            // 
            this.divideToolStripMenuItem1.Name = "divideToolStripMenuItem1";
            this.divideToolStripMenuItem1.Size = new System.Drawing.Size(131, 22);
            this.divideToolStripMenuItem1.Text = "Divide";
            this.divideToolStripMenuItem1.Click += new System.EventHandler(this.divideToolStripMenuItem1_Click);
            // 
            // sqrtToolStripMenuItem1
            // 
            this.sqrtToolStripMenuItem1.Name = "sqrtToolStripMenuItem1";
            this.sqrtToolStripMenuItem1.Size = new System.Drawing.Size(131, 22);
            this.sqrtToolStripMenuItem1.Text = "Sqrt";
            this.sqrtToolStripMenuItem1.Click += new System.EventHandler(this.sqrtToolStripMenuItem1_Click);
            // 
            // multiplyToolStripMenuItem1
            // 
            this.multiplyToolStripMenuItem1.Name = "multiplyToolStripMenuItem1";
            this.multiplyToolStripMenuItem1.Size = new System.Drawing.Size(131, 22);
            this.multiplyToolStripMenuItem1.Text = "Multiply";
            this.multiplyToolStripMenuItem1.Click += new System.EventHandler(this.multiplyToolStripMenuItem1_Click);
            // 
            // lnToolStripMenuItem
            // 
            this.lnToolStripMenuItem.Name = "lnToolStripMenuItem";
            this.lnToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.lnToolStripMenuItem.Text = "ln";
            this.lnToolStripMenuItem.Click += new System.EventHandler(this.lnToolStripMenuItem_Click);
            // 
            // anscombeToolStripMenuItem
            // 
            this.anscombeToolStripMenuItem.Name = "anscombeToolStripMenuItem";
            this.anscombeToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.anscombeToolStripMenuItem.Text = "Anscombe";
            this.anscombeToolStripMenuItem.Click += new System.EventHandler(this.anscombeToolStripMenuItem_Click);
            // 
            // normalizeToolStripMenuItem
            // 
            this.normalizeToolStripMenuItem.Name = "normalizeToolStripMenuItem";
            this.normalizeToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.normalizeToolStripMenuItem.Text = "Normalize";
            this.normalizeToolStripMenuItem.Click += new System.EventHandler(this.normalizeToolStripMenuItem_Click);
            // 
            // complexToolStripMenuItem
            // 
            this.complexToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.substractToolStripMenuItem,
            this.multiplyToolStripMenuItem,
            this.divideToolStripMenuItem});
            this.complexToolStripMenuItem.Name = "complexToolStripMenuItem";
            this.complexToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.complexToolStripMenuItem.Text = "Complex (a+ib)";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // substractToolStripMenuItem
            // 
            this.substractToolStripMenuItem.Name = "substractToolStripMenuItem";
            this.substractToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.substractToolStripMenuItem.Text = "Substract";
            this.substractToolStripMenuItem.Click += new System.EventHandler(this.substractToolStripMenuItem_Click);
            // 
            // multiplyToolStripMenuItem
            // 
            this.multiplyToolStripMenuItem.Name = "multiplyToolStripMenuItem";
            this.multiplyToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.multiplyToolStripMenuItem.Text = "Multiply";
            this.multiplyToolStripMenuItem.Click += new System.EventHandler(this.multiplyToolStripMenuItem_Click);
            // 
            // divideToolStripMenuItem
            // 
            this.divideToolStripMenuItem.Name = "divideToolStripMenuItem";
            this.divideToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.divideToolStripMenuItem.Text = "Divide";
            this.divideToolStripMenuItem.Click += new System.EventHandler(this.divideToolStripMenuItem_Click);
            // 
            // acumulateToolStripMenuItem
            // 
            this.acumulateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.medianToolStripMenuItem});
            this.acumulateToolStripMenuItem.Name = "acumulateToolStripMenuItem";
            this.acumulateToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.acumulateToolStripMenuItem.Text = "acumulate";
            // 
            // medianToolStripMenuItem
            // 
            this.medianToolStripMenuItem.Name = "medianToolStripMenuItem";
            this.medianToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.medianToolStripMenuItem.Text = "Median";
            this.medianToolStripMenuItem.Click += new System.EventHandler(this.medianToolStripMenuItem_Click);
            // 
            // filteringToolStripMenuItem
            // 
            this.filteringToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gaussianToolStripMenuItem,
            this.gaussianAndDivideToolStripMenuItem,
            this.medianAndDivideToolStripMenuItem});
            this.filteringToolStripMenuItem.Name = "filteringToolStripMenuItem";
            this.filteringToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.filteringToolStripMenuItem.Text = "Filtering";
            // 
            // gaussianToolStripMenuItem
            // 
            this.gaussianToolStripMenuItem.Name = "gaussianToolStripMenuItem";
            this.gaussianToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gaussianToolStripMenuItem.Text = "Gaussian";
            this.gaussianToolStripMenuItem.Click += new System.EventHandler(this.gaussianToolStripMenuItem_Click);
            // 
            // gaussianAndDivideToolStripMenuItem
            // 
            this.gaussianAndDivideToolStripMenuItem.Name = "gaussianAndDivideToolStripMenuItem";
            this.gaussianAndDivideToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gaussianAndDivideToolStripMenuItem.Text = "Gaussian and Divide";
            this.gaussianAndDivideToolStripMenuItem.Click += new System.EventHandler(this.gaussianAndDivideToolStripMenuItem_Click);
            // 
            // medianAndDivideToolStripMenuItem
            // 
            this.medianAndDivideToolStripMenuItem.Name = "medianAndDivideToolStripMenuItem";
            this.medianAndDivideToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.medianAndDivideToolStripMenuItem.Text = "Median and Divide";
            this.medianAndDivideToolStripMenuItem.Click += new System.EventHandler(this.medianAndDivideToolStripMenuItem_Click);
            // 
            // addNoiseToolStripMenuItem
            // 
            this.addNoiseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gaussianNoiseToolStripMenuItem,
            this.uniformNoiseToolStripMenuItem,
            this.poissonToolStripMenuItem});
            this.addNoiseToolStripMenuItem.Name = "addNoiseToolStripMenuItem";
            this.addNoiseToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.addNoiseToolStripMenuItem.Text = "Add Noise";
            // 
            // gaussianNoiseToolStripMenuItem
            // 
            this.gaussianNoiseToolStripMenuItem.Name = "gaussianNoiseToolStripMenuItem";
            this.gaussianNoiseToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.gaussianNoiseToolStripMenuItem.Text = "Gaussian";
            this.gaussianNoiseToolStripMenuItem.Click += new System.EventHandler(this.gaussianNoiseToolStripMenuItem_Click);
            // 
            // uniformNoiseToolStripMenuItem
            // 
            this.uniformNoiseToolStripMenuItem.Name = "uniformNoiseToolStripMenuItem";
            this.uniformNoiseToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.uniformNoiseToolStripMenuItem.Text = "Uniform";
            this.uniformNoiseToolStripMenuItem.Click += new System.EventHandler(this.uniformNoiseToolStripMenuItem_Click);
            // 
            // poissonToolStripMenuItem
            // 
            this.poissonToolStripMenuItem.Name = "poissonToolStripMenuItem";
            this.poissonToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.poissonToolStripMenuItem.Text = "Poisson";
            this.poissonToolStripMenuItem.Click += new System.EventHandler(this.poissonToolStripMenuItem_Click);
            // 
            // distancesToolStripMenuItem
            // 
            this.distancesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.meanSquareToolStripMenuItem,
            this.segmentationEvaluationToolStripMenuItem,
            this.kappaToolStripMenuItem,
            this.hausdorffToolStripMenuItem,
            this.pSNRToolStripMenuItem});
            this.distancesToolStripMenuItem.Name = "distancesToolStripMenuItem";
            this.distancesToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.distancesToolStripMenuItem.Text = "Distances";
            // 
            // meanSquareToolStripMenuItem
            // 
            this.meanSquareToolStripMenuItem.Name = "meanSquareToolStripMenuItem";
            this.meanSquareToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.meanSquareToolStripMenuItem.Text = "Mean square";
            this.meanSquareToolStripMenuItem.Click += new System.EventHandler(this.meanSquareToolStripMenuItem_Click);
            // 
            // segmentationEvaluationToolStripMenuItem
            // 
            this.segmentationEvaluationToolStripMenuItem.Name = "segmentationEvaluationToolStripMenuItem";
            this.segmentationEvaluationToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.segmentationEvaluationToolStripMenuItem.Text = "Segmentation Evaluation";
            this.segmentationEvaluationToolStripMenuItem.Click += new System.EventHandler(this.segmentationEvaluationToolStripMenuItem_Click);
            // 
            // kappaToolStripMenuItem
            // 
            this.kappaToolStripMenuItem.Name = "kappaToolStripMenuItem";
            this.kappaToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.kappaToolStripMenuItem.Text = "Kappa";
            this.kappaToolStripMenuItem.Click += new System.EventHandler(this.kappaToolStripMenuItem_Click);
            // 
            // hausdorffToolStripMenuItem
            // 
            this.hausdorffToolStripMenuItem.Name = "hausdorffToolStripMenuItem";
            this.hausdorffToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.hausdorffToolStripMenuItem.Text = "Hausdorff";
            this.hausdorffToolStripMenuItem.Click += new System.EventHandler(this.hausdorffToolStripMenuItem_Click);
            // 
            // pSNRToolStripMenuItem
            // 
            this.pSNRToolStripMenuItem.Name = "pSNRToolStripMenuItem";
            this.pSNRToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.pSNRToolStripMenuItem.Text = "PSNR";
            this.pSNRToolStripMenuItem.Click += new System.EventHandler(this.pSNRToolStripMenuItem_Click);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unitImageToolStripMenuItem,
            this.nullImageToolStripMenuItem,
            this.vectorFieldToolStripMenuItem,
            this.createReportToolStripMenuItem,
            this.sightToolStripMenuItem,
            this.isoLevelsToolStripMenuItem,
            this.vGradientToolStripMenuItem,
            this.hGardientToolStripMenuItem,
            this.centralGradientToolStripMenuItem,
            this.sphereToolStripMenuItem});
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.createToolStripMenuItem.Text = "Create";
            // 
            // unitImageToolStripMenuItem
            // 
            this.unitImageToolStripMenuItem.Name = "unitImageToolStripMenuItem";
            this.unitImageToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.unitImageToolStripMenuItem.Text = "Unit Image";
            this.unitImageToolStripMenuItem.Click += new System.EventHandler(this.unitImageToolStripMenuItem_Click_1);
            // 
            // nullImageToolStripMenuItem
            // 
            this.nullImageToolStripMenuItem.Name = "nullImageToolStripMenuItem";
            this.nullImageToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.nullImageToolStripMenuItem.Text = "Null Image";
            this.nullImageToolStripMenuItem.Click += new System.EventHandler(this.nullImageToolStripMenuItem_Click);
            // 
            // vectorFieldToolStripMenuItem
            // 
            this.vectorFieldToolStripMenuItem.Name = "vectorFieldToolStripMenuItem";
            this.vectorFieldToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.vectorFieldToolStripMenuItem.Text = "Vector field";
            this.vectorFieldToolStripMenuItem.Click += new System.EventHandler(this.vectorFieldToolStripMenuItem_Click);
            // 
            // createReportToolStripMenuItem
            // 
            this.createReportToolStripMenuItem.Name = "createReportToolStripMenuItem";
            this.createReportToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.createReportToolStripMenuItem.Text = "Report";
            this.createReportToolStripMenuItem.Click += new System.EventHandler(this.createReportToolStripMenuItem_Click_1);
            // 
            // sightToolStripMenuItem
            // 
            this.sightToolStripMenuItem.Name = "sightToolStripMenuItem";
            this.sightToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.sightToolStripMenuItem.Text = "Sight";
            this.sightToolStripMenuItem.Click += new System.EventHandler(this.sightToolStripMenuItem_Click);
            // 
            // isoLevelsToolStripMenuItem
            // 
            this.isoLevelsToolStripMenuItem.Name = "isoLevelsToolStripMenuItem";
            this.isoLevelsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.isoLevelsToolStripMenuItem.Text = "Iso-Levels";
            this.isoLevelsToolStripMenuItem.Click += new System.EventHandler(this.isoLevelsToolStripMenuItem_Click);
            // 
            // vGradientToolStripMenuItem
            // 
            this.vGradientToolStripMenuItem.Name = "vGradientToolStripMenuItem";
            this.vGradientToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.vGradientToolStripMenuItem.Text = "V. Gradient";
            this.vGradientToolStripMenuItem.Click += new System.EventHandler(this.vGradientToolStripMenuItem_Click);
            // 
            // hGardientToolStripMenuItem
            // 
            this.hGardientToolStripMenuItem.Name = "hGardientToolStripMenuItem";
            this.hGardientToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.hGardientToolStripMenuItem.Text = "H. Gradient";
            this.hGardientToolStripMenuItem.Click += new System.EventHandler(this.hGardientToolStripMenuItem_Click);
            // 
            // centralGradientToolStripMenuItem
            // 
            this.centralGradientToolStripMenuItem.Name = "centralGradientToolStripMenuItem";
            this.centralGradientToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.centralGradientToolStripMenuItem.Text = "Central Gradient";
            this.centralGradientToolStripMenuItem.Click += new System.EventHandler(this.centralGradientToolStripMenuItem_Click);
            // 
            // infosToolStripMenuItem
            // 
            this.infosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayInfosToolStripMenuItem,
            this.meanValuesToolStripMenuItem,
            this.maxValuesToolStripMenuItem,
            this.gradSmoothnessToolStripMenuItem,
            this.simpleStationarityToolStripMenuItem,
            this.stationirToolStripMenuItem,
            this.lastStationarityToolStripMenuItem,
            this.sMLToolStripMenuItem});
            this.infosToolStripMenuItem.Name = "infosToolStripMenuItem";
            this.infosToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.infosToolStripMenuItem.Text = "Infos";
            // 
            // displayInfosToolStripMenuItem
            // 
            this.displayInfosToolStripMenuItem.Name = "displayInfosToolStripMenuItem";
            this.displayInfosToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.displayInfosToolStripMenuItem.Text = "Display Infos";
            this.displayInfosToolStripMenuItem.Click += new System.EventHandler(this.displayInfosToolStripMenuItem_Click);
            // 
            // meanValuesToolStripMenuItem
            // 
            this.meanValuesToolStripMenuItem.Name = "meanValuesToolStripMenuItem";
            this.meanValuesToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.meanValuesToolStripMenuItem.Text = "Mean Values";
            this.meanValuesToolStripMenuItem.Click += new System.EventHandler(this.meanValuesToolStripMenuItem_Click);
            // 
            // maxValuesToolStripMenuItem
            // 
            this.maxValuesToolStripMenuItem.Name = "maxValuesToolStripMenuItem";
            this.maxValuesToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.maxValuesToolStripMenuItem.Text = "Max Values";
            this.maxValuesToolStripMenuItem.Click += new System.EventHandler(this.maxValuesToolStripMenuItem_Click);
            // 
            // gradSmoothnessToolStripMenuItem
            // 
            this.gradSmoothnessToolStripMenuItem.Name = "gradSmoothnessToolStripMenuItem";
            this.gradSmoothnessToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.gradSmoothnessToolStripMenuItem.Text = "Grad Smoothness";
            this.gradSmoothnessToolStripMenuItem.Click += new System.EventHandler(this.gradSmoothnessToolStripMenuItem_Click);
            // 
            // simpleStationarityToolStripMenuItem
            // 
            this.simpleStationarityToolStripMenuItem.Enabled = false;
            this.simpleStationarityToolStripMenuItem.Name = "simpleStationarityToolStripMenuItem";
            this.simpleStationarityToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.simpleStationarityToolStripMenuItem.Text = "Simple Stationarity";
            this.simpleStationarityToolStripMenuItem.Click += new System.EventHandler(this.simpleStationarityToolStripMenuItem_Click);
            // 
            // stationirToolStripMenuItem
            // 
            this.stationirToolStripMenuItem.Enabled = false;
            this.stationirToolStripMenuItem.Name = "stationirToolStripMenuItem";
            this.stationirToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.stationirToolStripMenuItem.Text = "Stationarity";
            this.stationirToolStripMenuItem.Click += new System.EventHandler(this.stationirToolStripMenuItem_Click);
            // 
            // lastStationarityToolStripMenuItem
            // 
            this.lastStationarityToolStripMenuItem.Name = "lastStationarityToolStripMenuItem";
            this.lastStationarityToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.lastStationarityToolStripMenuItem.Text = "Last stationarity";
            this.lastStationarityToolStripMenuItem.Click += new System.EventHandler(this.lastStationarityToolStripMenuItem_Click);
            // 
            // sMLToolStripMenuItem
            // 
            this.sMLToolStripMenuItem.Name = "sMLToolStripMenuItem";
            this.sMLToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.sMLToolStripMenuItem.Text = "SML";
            this.sMLToolStripMenuItem.Click += new System.EventHandler(this.sMLToolStripMenuItem_Click);
            // 
            // combineToolStripMenuItem
            // 
            this.combineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.horizontalToolStripMenuItem});
            this.combineToolStripMenuItem.Name = "combineToolStripMenuItem";
            this.combineToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.combineToolStripMenuItem.Text = "Combine";
            // 
            // horizontalToolStripMenuItem
            // 
            this.horizontalToolStripMenuItem.Name = "horizontalToolStripMenuItem";
            this.horizontalToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.horizontalToolStripMenuItem.Text = "Horizontal";
            this.horizontalToolStripMenuItem.Click += new System.EventHandler(this.horizontalToolStripMenuItem_Click);
            // 
            // saveHeapPicturesToolStripMenuItem
            // 
            this.saveHeapPicturesToolStripMenuItem.Name = "saveHeapPicturesToolStripMenuItem";
            this.saveHeapPicturesToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.saveHeapPicturesToolStripMenuItem.Text = "Save Heap Pictures";
            this.saveHeapPicturesToolStripMenuItem.Click += new System.EventHandler(this.saveHeapPicturesToolStripMenuItem_Click);
            // 
            // imageListForHeap
            // 
            this.imageListForHeap.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListForHeap.ImageStream")));
            this.imageListForHeap.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListForHeap.Images.SetKeyName(0, "");
            this.imageListForHeap.Images.SetKeyName(1, "");
            // 
            // textBoxInfoPicture
            // 
            this.textBoxInfoPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInfoPicture.Location = new System.Drawing.Point(6, 6);
            this.textBoxInfoPicture.Multiline = true;
            this.textBoxInfoPicture.Name = "textBoxInfoPicture";
            this.textBoxInfoPicture.ReadOnly = true;
            this.textBoxInfoPicture.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInfoPicture.Size = new System.Drawing.Size(608, 356);
            this.textBoxInfoPicture.TabIndex = 3;
            // 
            // textBoxImageName
            // 
            this.textBoxImageName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxImageName.Location = new System.Drawing.Point(178, 12);
            this.textBoxImageName.Name = "textBoxImageName";
            this.textBoxImageName.Size = new System.Drawing.Size(586, 20);
            this.textBoxImageName.TabIndex = 4;
            this.textBoxImageName.TextChanged += new System.EventHandler(this.textBoxImageName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(137, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Name";
            // 
            // contextMenuStripHistogram
            // 
            this.contextMenuStripHistogram.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToFileToolStripMenuItem1,
            this.copyToClipboardToolStripMenuItem,
            this.displayToolStripMenuItem});
            this.contextMenuStripHistogram.Name = "contextMenuStripHistogram";
            this.contextMenuStripHistogram.Size = new System.Drawing.Size(172, 70);
            // 
            // saveToFileToolStripMenuItem1
            // 
            this.saveToFileToolStripMenuItem1.Name = "saveToFileToolStripMenuItem1";
            this.saveToFileToolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.saveToFileToolStripMenuItem1.Text = "Save to File";
            this.saveToFileToolStripMenuItem1.Click += new System.EventHandler(this.saveToFileToolStripMenuItem1_Click);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to Clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // displayToolStripMenuItem
            // 
            this.displayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pointsToolStripMenuItem1,
            this.barToolStripMenuItem});
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            this.displayToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.displayToolStripMenuItem.Text = "Display";
            // 
            // pointsToolStripMenuItem1
            // 
            this.pointsToolStripMenuItem1.CheckOnClick = true;
            this.pointsToolStripMenuItem1.Name = "pointsToolStripMenuItem1";
            this.pointsToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.pointsToolStripMenuItem1.Text = "Points";
            this.pointsToolStripMenuItem1.Click += new System.EventHandler(this.pointsToolStripMenuItem1_Click);
            // 
            // barToolStripMenuItem
            // 
            this.barToolStripMenuItem.Checked = true;
            this.barToolStripMenuItem.CheckOnClick = true;
            this.barToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.barToolStripMenuItem.Name = "barToolStripMenuItem";
            this.barToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.barToolStripMenuItem.Text = "Bar";
            this.barToolStripMenuItem.Click += new System.EventHandler(this.barToolStripMenuItem_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageInfos);
            this.tabControlMain.Controls.Add(this.tabPageHisto);
            this.tabControlMain.Controls.Add(this.tabPageLineProfil);
            this.tabControlMain.Controls.Add(this.tabPageJointHisto);
            this.tabControlMain.Location = new System.Drawing.Point(140, 66);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(628, 448);
            this.tabControlMain.TabIndex = 11;
            this.tabControlMain.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageInfos
            // 
            this.tabPageInfos.Controls.Add(this.numericUpDownZRes);
            this.tabPageInfos.Controls.Add(this.label9);
            this.tabPageInfos.Controls.Add(this.numericUpDownYRes);
            this.tabPageInfos.Controls.Add(this.label8);
            this.tabPageInfos.Controls.Add(this.numericUpDownXRes);
            this.tabPageInfos.Controls.Add(this.label7);
            this.tabPageInfos.Controls.Add(this.textBoxInfoPicture);
            this.tabPageInfos.Location = new System.Drawing.Point(4, 22);
            this.tabPageInfos.Name = "tabPageInfos";
            this.tabPageInfos.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInfos.Size = new System.Drawing.Size(620, 422);
            this.tabPageInfos.TabIndex = 0;
            this.tabPageInfos.Text = "Infos";
            this.tabPageInfos.UseVisualStyleBackColor = true;
            // 
            // numericUpDownZRes
            // 
            this.numericUpDownZRes.DecimalPlaces = 3;
            this.numericUpDownZRes.Location = new System.Drawing.Point(283, 390);
            this.numericUpDownZRes.Name = "numericUpDownZRes";
            this.numericUpDownZRes.Size = new System.Drawing.Size(96, 20);
            this.numericUpDownZRes.TabIndex = 7;
            this.numericUpDownZRes.ValueChanged += new System.EventHandler(this.numericUpDownZRes_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(313, 371);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "ZRes";
            // 
            // numericUpDownYRes
            // 
            this.numericUpDownYRes.DecimalPlaces = 3;
            this.numericUpDownYRes.Location = new System.Drawing.Point(154, 390);
            this.numericUpDownYRes.Name = "numericUpDownYRes";
            this.numericUpDownYRes.Size = new System.Drawing.Size(96, 20);
            this.numericUpDownYRes.TabIndex = 7;
            this.numericUpDownYRes.ValueChanged += new System.EventHandler(this.numericUpDownYRes_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(179, 371);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "YRes";
            // 
            // numericUpDownXRes
            // 
            this.numericUpDownXRes.DecimalPlaces = 3;
            this.numericUpDownXRes.Location = new System.Drawing.Point(26, 390);
            this.numericUpDownXRes.Name = "numericUpDownXRes";
            this.numericUpDownXRes.Size = new System.Drawing.Size(96, 20);
            this.numericUpDownXRes.TabIndex = 5;
            this.numericUpDownXRes.ValueChanged += new System.EventHandler(this.numericUpDownXRes_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(54, 371);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "XRes";
            // 
            // tabPageHisto
            // 
            this.tabPageHisto.Controls.Add(this.numericUpDownMinimumHisto);
            this.tabPageHisto.Controls.Add(this.numericUpDownMaximumHisto);
            this.tabPageHisto.Controls.Add(this.chartHisto);
            this.tabPageHisto.Controls.Add(this.groupBox3);
            this.tabPageHisto.Controls.Add(this.checkBoxHistoLog);
            this.tabPageHisto.Controls.Add(this.radioButtonHistoTypeCumulated);
            this.tabPageHisto.Controls.Add(this.radioButtonHistoTypeClassic);
            this.tabPageHisto.Location = new System.Drawing.Point(4, 22);
            this.tabPageHisto.Name = "tabPageHisto";
            this.tabPageHisto.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHisto.Size = new System.Drawing.Size(620, 422);
            this.tabPageHisto.TabIndex = 1;
            this.tabPageHisto.Text = "Histogram";
            this.tabPageHisto.UseVisualStyleBackColor = true;
            // 
            // numericUpDownMinimumHisto
            // 
            this.numericUpDownMinimumHisto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownMinimumHisto.Location = new System.Drawing.Point(12, 257);
            this.numericUpDownMinimumHisto.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.numericUpDownMinimumHisto.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericUpDownMinimumHisto.Name = "numericUpDownMinimumHisto";
            this.numericUpDownMinimumHisto.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownMinimumHisto.TabIndex = 29;
            this.numericUpDownMinimumHisto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDownMinimumHisto_KeyPress);
            // 
            // numericUpDownMaximumHisto
            // 
            this.numericUpDownMaximumHisto.Location = new System.Drawing.Point(12, 44);
            this.numericUpDownMaximumHisto.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.numericUpDownMaximumHisto.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericUpDownMaximumHisto.Name = "numericUpDownMaximumHisto";
            this.numericUpDownMaximumHisto.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownMaximumHisto.TabIndex = 28;
            this.numericUpDownMaximumHisto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDownMaximumHisto_KeyPress);
            // 
            // chartHisto
            // 
            this.chartHisto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chartHisto.ChartAreas.Add(chartArea1);
            this.chartHisto.ContextMenuStrip = this.contextMenuStripHistogram;
            this.chartHisto.Location = new System.Drawing.Point(61, 33);
            this.chartHisto.Name = "chartHisto";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.chartHisto.Series.Add(series1);
            this.chartHisto.Size = new System.Drawing.Size(511, 244);
            this.chartHisto.TabIndex = 27;
            this.chartHisto.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartHisto_MouseMove);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.numericUpDownChannelForHisto);
            this.groupBox3.Controls.Add(this.buttonOperaFactor);
            this.groupBox3.Controls.Add(this.numericUpDownMinHisto);
            this.groupBox3.Controls.Add(this.labelPosition);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.buttonSetToMax);
            this.groupBox3.Controls.Add(this.labelValueHisto);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.buttonSetToMin);
            this.groupBox3.Controls.Add(this.numericUpDownStepHisto);
            this.groupBox3.Controls.Add(this.numericUpDownMaxHisto);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(3, 283);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(614, 136);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Channel";
            // 
            // numericUpDownChannelForHisto
            // 
            this.numericUpDownChannelForHisto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownChannelForHisto.Location = new System.Drawing.Point(58, 104);
            this.numericUpDownChannelForHisto.Name = "numericUpDownChannelForHisto";
            this.numericUpDownChannelForHisto.Size = new System.Drawing.Size(71, 20);
            this.numericUpDownChannelForHisto.TabIndex = 25;
            this.numericUpDownChannelForHisto.ValueChanged += new System.EventHandler(this.numericUpDownChannelForHisto_ValueChanged);
            // 
            // buttonOperaFactor
            // 
            this.buttonOperaFactor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonOperaFactor.Location = new System.Drawing.Point(271, 57);
            this.buttonOperaFactor.Name = "buttonOperaFactor";
            this.buttonOperaFactor.Size = new System.Drawing.Size(68, 24);
            this.buttonOperaFactor.TabIndex = 24;
            this.buttonOperaFactor.Text = "Opera";
            this.buttonOperaFactor.UseVisualStyleBackColor = true;
            this.buttonOperaFactor.Click += new System.EventHandler(this.buttonOperaFactor_Click);
            // 
            // numericUpDownMinHisto
            // 
            this.numericUpDownMinHisto.DecimalPlaces = 3;
            this.numericUpDownMinHisto.Location = new System.Drawing.Point(9, 31);
            this.numericUpDownMinHisto.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.numericUpDownMinHisto.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericUpDownMinHisto.Name = "numericUpDownMinHisto";
            this.numericUpDownMinHisto.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownMinHisto.TabIndex = 14;
            this.numericUpDownMinHisto.ValueChanged += new System.EventHandler(this.numericUpDownMinHisto_ValueChanged);
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(232, 107);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(0, 13);
            this.labelPosition.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(405, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 17;
            // 
            // buttonSetToMax
            // 
            this.buttonSetToMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetToMax.Location = new System.Drawing.Point(540, 57);
            this.buttonSetToMax.Name = "buttonSetToMax";
            this.buttonSetToMax.Size = new System.Drawing.Size(68, 24);
            this.buttonSetToMax.TabIndex = 22;
            this.buttonSetToMax.Text = "Max";
            this.buttonSetToMax.UseVisualStyleBackColor = true;
            this.buttonSetToMax.Click += new System.EventHandler(this.buttonSetToMax_Click);
            // 
            // labelValueHisto
            // 
            this.labelValueHisto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelValueHisto.AutoSize = true;
            this.labelValueHisto.Location = new System.Drawing.Point(405, 106);
            this.labelValueHisto.Name = "labelValueHisto";
            this.labelValueHisto.Size = new System.Drawing.Size(0, 13);
            this.labelValueHisto.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(365, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Value";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(182, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Position";
            // 
            // buttonSetToMin
            // 
            this.buttonSetToMin.Location = new System.Drawing.Point(9, 57);
            this.buttonSetToMin.Name = "buttonSetToMin";
            this.buttonSetToMin.Size = new System.Drawing.Size(68, 24);
            this.buttonSetToMin.TabIndex = 23;
            this.buttonSetToMin.Text = "Min";
            this.buttonSetToMin.UseVisualStyleBackColor = true;
            this.buttonSetToMin.Click += new System.EventHandler(this.buttonSetToMin_Click);
            // 
            // numericUpDownStepHisto
            // 
            this.numericUpDownStepHisto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDownStepHisto.DecimalPlaces = 3;
            this.numericUpDownStepHisto.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownStepHisto.Location = new System.Drawing.Point(271, 31);
            this.numericUpDownStepHisto.Maximum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            0});
            this.numericUpDownStepHisto.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericUpDownStepHisto.Name = "numericUpDownStepHisto";
            this.numericUpDownStepHisto.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericUpDownStepHisto.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownStepHisto.TabIndex = 19;
            this.numericUpDownStepHisto.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownStepHisto.ValueChanged += new System.EventHandler(this.numericUpDownStepHisto_ValueChanged);
            // 
            // numericUpDownMaxHisto
            // 
            this.numericUpDownMaxHisto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownMaxHisto.DecimalPlaces = 3;
            this.numericUpDownMaxHisto.Location = new System.Drawing.Point(541, 31);
            this.numericUpDownMaxHisto.Maximum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            0});
            this.numericUpDownMaxHisto.Minimum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            -2147483648});
            this.numericUpDownMaxHisto.Name = "numericUpDownMaxHisto";
            this.numericUpDownMaxHisto.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericUpDownMaxHisto.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownMaxHisto.TabIndex = 15;
            this.numericUpDownMaxHisto.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownMaxHisto.ValueChanged += new System.EventHandler(this.numericUpDownMaxHisto_ValueChanged);
            // 
            // checkBoxHistoLog
            // 
            this.checkBoxHistoLog.AutoSize = true;
            this.checkBoxHistoLog.Location = new System.Drawing.Point(28, 9);
            this.checkBoxHistoLog.Name = "checkBoxHistoLog";
            this.checkBoxHistoLog.Size = new System.Drawing.Size(69, 17);
            this.checkBoxHistoLog.TabIndex = 16;
            this.checkBoxHistoLog.Text = "Log view";
            this.checkBoxHistoLog.UseVisualStyleBackColor = true;
            this.checkBoxHistoLog.CheckedChanged += new System.EventHandler(this.checkBoxHistoLog_CheckedChanged);
            // 
            // radioButtonHistoTypeCumulated
            // 
            this.radioButtonHistoTypeCumulated.AutoSize = true;
            this.radioButtonHistoTypeCumulated.Location = new System.Drawing.Point(188, 8);
            this.radioButtonHistoTypeCumulated.Name = "radioButtonHistoTypeCumulated";
            this.radioButtonHistoTypeCumulated.Size = new System.Drawing.Size(75, 17);
            this.radioButtonHistoTypeCumulated.TabIndex = 13;
            this.radioButtonHistoTypeCumulated.Text = "Cumulated";
            this.radioButtonHistoTypeCumulated.UseVisualStyleBackColor = true;
            this.radioButtonHistoTypeCumulated.CheckedChanged += new System.EventHandler(this.radioButtonHistoTypeCumulated_CheckedChanged);
            // 
            // radioButtonHistoTypeClassic
            // 
            this.radioButtonHistoTypeClassic.AutoSize = true;
            this.radioButtonHistoTypeClassic.Checked = true;
            this.radioButtonHistoTypeClassic.Location = new System.Drawing.Point(117, 8);
            this.radioButtonHistoTypeClassic.Name = "radioButtonHistoTypeClassic";
            this.radioButtonHistoTypeClassic.Size = new System.Drawing.Size(58, 17);
            this.radioButtonHistoTypeClassic.TabIndex = 12;
            this.radioButtonHistoTypeClassic.TabStop = true;
            this.radioButtonHistoTypeClassic.Text = "Classic";
            this.radioButtonHistoTypeClassic.UseVisualStyleBackColor = true;
            this.radioButtonHistoTypeClassic.CheckedChanged += new System.EventHandler(this.radioButtonHistoTypeClassic_CheckedChanged);
            // 
            // tabPageLineProfil
            // 
            this.tabPageLineProfil.Controls.Add(this.chartLineProfile);
            this.tabPageLineProfil.Controls.Add(this.groupBox2);
            this.tabPageLineProfil.Location = new System.Drawing.Point(4, 22);
            this.tabPageLineProfil.Name = "tabPageLineProfil";
            this.tabPageLineProfil.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLineProfil.Size = new System.Drawing.Size(620, 422);
            this.tabPageLineProfil.TabIndex = 2;
            this.tabPageLineProfil.Text = "Line Profil";
            this.tabPageLineProfil.UseVisualStyleBackColor = true;
            // 
            // chartLineProfile
            // 
            this.chartLineProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.chartLineProfile.ChartAreas.Add(chartArea2);
            this.chartLineProfile.ContextMenuStrip = this.contextMenuStripLineProfil;
            this.chartLineProfile.Location = new System.Drawing.Point(5, 3);
            this.chartLineProfile.Name = "chartLineProfile";
            series2.ChartArea = "ChartArea1";
            series2.Name = "Series1";
            this.chartLineProfile.Series.Add(series2);
            this.chartLineProfile.Size = new System.Drawing.Size(572, 271);
            this.chartLineProfile.TabIndex = 13;
            this.chartLineProfile.Text = "chart1";
            // 
            // contextMenuStripLineProfil
            // 
            this.contextMenuStripLineProfil.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToFileToolStripMenuItem,
            this.displayGridToolStripMenuItem});
            this.contextMenuStripLineProfil.Name = "contextMenuStripLineProfil";
            this.contextMenuStripLineProfil.Size = new System.Drawing.Size(138, 48);
            // 
            // saveToFileToolStripMenuItem
            // 
            this.saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
            this.saveToFileToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.saveToFileToolStripMenuItem.Text = "Save to file";
            this.saveToFileToolStripMenuItem.Click += new System.EventHandler(this.saveToFileToolStripMenuItem_Click);
            // 
            // displayGridToolStripMenuItem
            // 
            this.displayGridToolStripMenuItem.Checked = true;
            this.displayGridToolStripMenuItem.CheckOnClick = true;
            this.displayGridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.displayGridToolStripMenuItem.Name = "displayGridToolStripMenuItem";
            this.displayGridToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.displayGridToolStripMenuItem.Text = "Display Grid";
            this.displayGridToolStripMenuItem.Click += new System.EventHandler(this.displayGridToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonDepthLine);
            this.groupBox2.Controls.Add(this.radioButtonLine);
            this.groupBox2.Controls.Add(this.numericUpDownHistoLinePosition);
            this.groupBox2.Controls.Add(this.radioButtonColumn);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 274);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(575, 145);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Line options";
            // 
            // radioButtonDepthLine
            // 
            this.radioButtonDepthLine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButtonDepthLine.AutoSize = true;
            this.radioButtonDepthLine.Location = new System.Drawing.Point(343, 36);
            this.radioButtonDepthLine.Name = "radioButtonDepthLine";
            this.radioButtonDepthLine.Size = new System.Drawing.Size(32, 17);
            this.radioButtonDepthLine.TabIndex = 12;
            this.radioButtonDepthLine.Text = "Z";
            this.radioButtonDepthLine.UseVisualStyleBackColor = true;
            // 
            // radioButtonLine
            // 
            this.radioButtonLine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButtonLine.AutoSize = true;
            this.radioButtonLine.Checked = true;
            this.radioButtonLine.Location = new System.Drawing.Point(193, 36);
            this.radioButtonLine.Name = "radioButtonLine";
            this.radioButtonLine.Size = new System.Drawing.Size(32, 17);
            this.radioButtonLine.TabIndex = 8;
            this.radioButtonLine.TabStop = true;
            this.radioButtonLine.Text = "X";
            this.radioButtonLine.UseVisualStyleBackColor = true;
            this.radioButtonLine.CheckedChanged += new System.EventHandler(this.radioButtonLine_CheckedChanged);
            // 
            // numericUpDownHistoLinePosition
            // 
            this.numericUpDownHistoLinePosition.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericUpDownHistoLinePosition.Location = new System.Drawing.Point(271, 84);
            this.numericUpDownHistoLinePosition.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numericUpDownHistoLinePosition.Name = "numericUpDownHistoLinePosition";
            this.numericUpDownHistoLinePosition.Size = new System.Drawing.Size(83, 20);
            this.numericUpDownHistoLinePosition.TabIndex = 11;
            this.numericUpDownHistoLinePosition.ValueChanged += new System.EventHandler(this.numericUpDownHistoLinePosition_ValueChanged);
            // 
            // radioButtonColumn
            // 
            this.radioButtonColumn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButtonColumn.AutoSize = true;
            this.radioButtonColumn.Location = new System.Drawing.Point(268, 36);
            this.radioButtonColumn.Name = "radioButtonColumn";
            this.radioButtonColumn.Size = new System.Drawing.Size(32, 17);
            this.radioButtonColumn.TabIndex = 9;
            this.radioButtonColumn.Text = "Y";
            this.radioButtonColumn.UseVisualStyleBackColor = true;
            this.radioButtonColumn.CheckedChanged += new System.EventHandler(this.radioButtonColumn_CheckedChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(221, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Position";
            // 
            // tabPageJointHisto
            // 
            this.tabPageJointHisto.Controls.Add(this.groupBox5);
            this.tabPageJointHisto.Controls.Add(this.groupBox4);
            this.tabPageJointHisto.Controls.Add(this.panelJointHisto);
            this.tabPageJointHisto.Location = new System.Drawing.Point(4, 22);
            this.tabPageJointHisto.Name = "tabPageJointHisto";
            this.tabPageJointHisto.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageJointHisto.Size = new System.Drawing.Size(620, 422);
            this.tabPageJointHisto.TabIndex = 4;
            this.tabPageJointHisto.Text = "Joint Histo";
            this.tabPageJointHisto.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.radioButtonImage1Channel1);
            this.groupBox5.Controls.Add(this.radioButtonImage1Channel2);
            this.groupBox5.Controls.Add(this.radioButtonImage1Channel3);
            this.groupBox5.Location = new System.Drawing.Point(152, 294);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(140, 39);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Image 1 channel";
            // 
            // radioButtonImage1Channel1
            // 
            this.radioButtonImage1Channel1.AutoSize = true;
            this.radioButtonImage1Channel1.Checked = true;
            this.radioButtonImage1Channel1.Location = new System.Drawing.Point(6, 14);
            this.radioButtonImage1Channel1.Name = "radioButtonImage1Channel1";
            this.radioButtonImage1Channel1.Size = new System.Drawing.Size(31, 17);
            this.radioButtonImage1Channel1.TabIndex = 8;
            this.radioButtonImage1Channel1.TabStop = true;
            this.radioButtonImage1Channel1.Text = "1";
            this.radioButtonImage1Channel1.UseVisualStyleBackColor = true;
            this.radioButtonImage1Channel1.CheckedChanged += new System.EventHandler(this.radioButtonImage1Channel1_CheckedChanged);
            // 
            // radioButtonImage1Channel2
            // 
            this.radioButtonImage1Channel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButtonImage1Channel2.AutoSize = true;
            this.radioButtonImage1Channel2.Location = new System.Drawing.Point(54, 14);
            this.radioButtonImage1Channel2.Name = "radioButtonImage1Channel2";
            this.radioButtonImage1Channel2.Size = new System.Drawing.Size(31, 17);
            this.radioButtonImage1Channel2.TabIndex = 9;
            this.radioButtonImage1Channel2.Text = "2";
            this.radioButtonImage1Channel2.UseVisualStyleBackColor = true;
            this.radioButtonImage1Channel2.CheckedChanged += new System.EventHandler(this.radioButtonImage1Channel2_CheckedChanged);
            // 
            // radioButtonImage1Channel3
            // 
            this.radioButtonImage1Channel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonImage1Channel3.AutoSize = true;
            this.radioButtonImage1Channel3.Location = new System.Drawing.Point(97, 14);
            this.radioButtonImage1Channel3.Name = "radioButtonImage1Channel3";
            this.radioButtonImage1Channel3.Size = new System.Drawing.Size(31, 17);
            this.radioButtonImage1Channel3.TabIndex = 10;
            this.radioButtonImage1Channel3.Text = "3";
            this.radioButtonImage1Channel3.UseVisualStyleBackColor = true;
            this.radioButtonImage1Channel3.CheckedChanged += new System.EventHandler(this.radioButtonImage1Channel3_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.radioButtonImage0Channel1);
            this.groupBox4.Controls.Add(this.radioButtonImage0Channel2);
            this.groupBox4.Controls.Add(this.radioButtonImage0Channel3);
            this.groupBox4.Location = new System.Drawing.Point(6, 294);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(133, 39);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Image 0 channel";
            // 
            // radioButtonImage0Channel1
            // 
            this.radioButtonImage0Channel1.AutoSize = true;
            this.radioButtonImage0Channel1.Checked = true;
            this.radioButtonImage0Channel1.Location = new System.Drawing.Point(6, 14);
            this.radioButtonImage0Channel1.Name = "radioButtonImage0Channel1";
            this.radioButtonImage0Channel1.Size = new System.Drawing.Size(31, 17);
            this.radioButtonImage0Channel1.TabIndex = 8;
            this.radioButtonImage0Channel1.TabStop = true;
            this.radioButtonImage0Channel1.Text = "1";
            this.radioButtonImage0Channel1.UseVisualStyleBackColor = true;
            this.radioButtonImage0Channel1.CheckedChanged += new System.EventHandler(this.radioButtonImage0Channel1_CheckedChanged);
            // 
            // radioButtonImage0Channel2
            // 
            this.radioButtonImage0Channel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButtonImage0Channel2.AutoSize = true;
            this.radioButtonImage0Channel2.Location = new System.Drawing.Point(50, 14);
            this.radioButtonImage0Channel2.Name = "radioButtonImage0Channel2";
            this.radioButtonImage0Channel2.Size = new System.Drawing.Size(31, 17);
            this.radioButtonImage0Channel2.TabIndex = 9;
            this.radioButtonImage0Channel2.Text = "2";
            this.radioButtonImage0Channel2.UseVisualStyleBackColor = true;
            this.radioButtonImage0Channel2.CheckedChanged += new System.EventHandler(this.radioButtonImage0Channel2_CheckedChanged);
            // 
            // radioButtonImage0Channel3
            // 
            this.radioButtonImage0Channel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonImage0Channel3.AutoSize = true;
            this.radioButtonImage0Channel3.Location = new System.Drawing.Point(90, 14);
            this.radioButtonImage0Channel3.Name = "radioButtonImage0Channel3";
            this.radioButtonImage0Channel3.Size = new System.Drawing.Size(31, 17);
            this.radioButtonImage0Channel3.TabIndex = 10;
            this.radioButtonImage0Channel3.Text = "3";
            this.radioButtonImage0Channel3.UseVisualStyleBackColor = true;
            this.radioButtonImage0Channel3.CheckedChanged += new System.EventHandler(this.radioButtonImage0Channel3_CheckedChanged);
            // 
            // panelJointHisto
            // 
            this.panelJointHisto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelJointHisto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelJointHisto.Location = new System.Drawing.Point(6, 6);
            this.panelJointHisto.Name = "panelJointHisto";
            this.panelJointHisto.Size = new System.Drawing.Size(286, 282);
            this.panelJointHisto.TabIndex = 9;
            this.panelJointHisto.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDoubleClick);
            // 
            // checkBoxGetOnlyDisplayedZ
            // 
            this.checkBoxGetOnlyDisplayedZ.AutoSize = true;
            this.checkBoxGetOnlyDisplayedZ.Location = new System.Drawing.Point(140, 43);
            this.checkBoxGetOnlyDisplayedZ.Name = "checkBoxGetOnlyDisplayedZ";
            this.checkBoxGetOnlyDisplayedZ.Size = new System.Drawing.Size(126, 17);
            this.checkBoxGetOnlyDisplayedZ.TabIndex = 12;
            this.checkBoxGetOnlyDisplayedZ.Text = "Get Only Displayed Z";
            this.checkBoxGetOnlyDisplayedZ.UseVisualStyleBackColor = true;
            // 
            // checkBoxLoadAllTheSequence
            // 
            this.checkBoxLoadAllTheSequence.AutoSize = true;
            this.checkBoxLoadAllTheSequence.Location = new System.Drawing.Point(310, 43);
            this.checkBoxLoadAllTheSequence.Name = "checkBoxLoadAllTheSequence";
            this.checkBoxLoadAllTheSequence.Size = new System.Drawing.Size(131, 17);
            this.checkBoxLoadAllTheSequence.TabIndex = 13;
            this.checkBoxLoadAllTheSequence.Text = "Load all the sequence";
            this.checkBoxLoadAllTheSequence.UseVisualStyleBackColor = true;
            // 
            // sphereToolStripMenuItem
            // 
            this.sphereToolStripMenuItem.Name = "sphereToolStripMenuItem";
            this.sphereToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.sphereToolStripMenuItem.Text = "Sphere";
            this.sphereToolStripMenuItem.Click += new System.EventHandler(this.sphereToolStripMenuItem_Click);
            // 
            // MainClassHeap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(774, 520);
            this.Controls.Add(this.checkBoxLoadAllTheSequence);
            this.Controls.Add(this.checkBoxGetOnlyDisplayedZ);
            this.Controls.Add(this.textBoxImageName);
            this.Controls.Add(this.listViewHeap);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "MainClassHeap";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Heap v2.0.2";
            this.Load += new System.EventHandler(this.MainClassHeap_Load);
            this.ResizeEnd += new System.EventHandler(this.MainClassHeap_ResizeEnd);
            this.contextMenuStripForHeap.ResumeLayout(false);
            this.contextMenuStripHistogram.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageInfos.ResumeLayout(false);
            this.tabPageInfos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZRes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYRes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXRes)).EndInit();
            this.tabPageHisto.ResumeLayout(false);
            this.tabPageHisto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimumHisto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaximumHisto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHisto)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChannelForHisto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinHisto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStepHisto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxHisto)).EndInit();
            this.tabPageLineProfil.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartLineProfile)).EndInit();
            this.contextMenuStripLineProfil.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHistoLinePosition)).EndInit();
            this.tabPageJointHisto.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #region PrivateValues
        private float RatioXY = 0f;
        private System.ComponentModel.IContainer components;
        private ListView listViewHeap;
        private ImageList imageListForHeap;
        private TextBox textBoxInfoPicture;
        private ContextMenuStrip contextMenuStripForHeap;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem deleteAllToolStripMenuItem;
        private ToolStripMenuItem activateToolStripMenuItem;
        private ToolStripMenuItem mergeToolStripMenuItem;
        private ToolStripMenuItem channelToolStripMenuItem;
        private ToolStripMenuItem splitByToolStripMenuItem;
        private ToolStripMenuItem channelToolStripMenuItemSplit;
        private ToolStripMenuItem operationsToolStripMenuItem;
        private ToolStripMenuItem depthToolStripMenuItem;
        private TextBox textBoxImageName;
        private System.Windows.Forms.Label label1;
        private ColumnHeader columnHeaderPictures;
        private ColumnHeader columnHeaderSelection;
        private TabControl tabControlMain;
        private TabPage tabPageInfos;
        private TabPage tabPageHisto;
        private RadioButton radioButtonHistoTypeCumulated;
        private RadioButton radioButtonHistoTypeClassic;
        private CheckBox checkBoxHistoLog;
        private NumericUpDown numericUpDownMaxHisto;
        private NumericUpDown numericUpDownMinHisto;
        private System.Windows.Forms.Label labelValueHisto;
        private System.Windows.Forms.Label label2;
        private NumericUpDown numericUpDownStepHisto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelPosition;
        private Button buttonSetToMin;
        private Button buttonSetToMax;
        private ToolStripMenuItem distancesToolStripMenuItem;
        private ToolStripMenuItem meanSquareToolStripMenuItem;
        private TabPage tabPageLineProfil;
        private NumericUpDown numericUpDownHistoLinePosition;
        private System.Windows.Forms.Label label5;
        private RadioButton radioButtonColumn;
        private RadioButton radioButtonLine;
        private ContextMenuStrip contextMenuStripLineProfil;
        private ToolStripMenuItem saveToFileToolStripMenuItem;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private ToolStripMenuItem segmentationEvaluationToolStripMenuItem;
        private ToolStripMenuItem kappaToolStripMenuItem;
        private ToolStripMenuItem hausdorffToolStripMenuItem;
        private Button buttonOperaFactor;
        private ToolStripMenuItem createToolStripMenuItem;
        private ToolStripMenuItem vectorFieldToolStripMenuItem;
        private ToolStripMenuItem createReportToolStripMenuItem;
        private ContextMenuStrip contextMenuStripHistogram;
        private ToolStripMenuItem saveToFileToolStripMenuItem1;
        private ToolStripMenuItem depthSplitToolStripMenuItem;
        private ToolStripMenuItem marginalToolStripMenuItem;
        private ToolStripMenuItem addToolStripMenuItem1;
        private ToolStripMenuItem substractToolStripMenuItem1;
        private ToolStripMenuItem divideToolStripMenuItem1;
        private ToolStripMenuItem sqrtToolStripMenuItem1;
        private ToolStripMenuItem multiplyToolStripMenuItem1;
        private ToolStripMenuItem complexToolStripMenuItem;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem substractToolStripMenuItem;
        private ToolStripMenuItem multiplyToolStripMenuItem;
        private ToolStripMenuItem divideToolStripMenuItem;
        private TabPage tabPageJointHisto;
        private Panel panelJointHisto;
        private GroupBox groupBox5;
        private RadioButton radioButtonImage1Channel1;
        private RadioButton radioButtonImage1Channel2;
        private RadioButton radioButtonImage1Channel3;
        private GroupBox groupBox4;
        private RadioButton radioButtonImage0Channel1;
        private RadioButton radioButtonImage0Channel2;
        private RadioButton radioButtonImage0Channel3;
        private ToolStripMenuItem nullImageToolStripMenuItem;
        private ToolStripMenuItem unitImageToolStripMenuItem;
        private ToolStripMenuItem timeToolStripMenuItem;
        private ToolStripMenuItem lnToolStripMenuItem;
        private ToolStripMenuItem sightToolStripMenuItem;
        private CheckBox checkBoxGetOnlyDisplayedZ;
        private ToolStripMenuItem acumulateToolStripMenuItem;
        private ToolStripMenuItem medianToolStripMenuItem;
        private ToolStripMenuItem isoLevelsToolStripMenuItem;
        private RadioButton radioButtonDepthLine;
        private ToolStripMenuItem anscombeToolStripMenuItem;
        private ToolStripMenuItem infosToolStripMenuItem;
        private ToolStripMenuItem meanValuesToolStripMenuItem;
        private ToolStripMenuItem maxValuesToolStripMenuItem;
        private ToolStripMenuItem combineToolStripMenuItem;
        private ToolStripMenuItem horizontalToolStripMenuItem;
        private ToolStripMenuItem stationirToolStripMenuItem;
        private ToolStripMenuItem gradSmoothnessToolStripMenuItem;
        private ToolStripMenuItem simpleStationarityToolStripMenuItem;
        private ToolStripMenuItem lastStationarityToolStripMenuItem;
        private ToolStripMenuItem filteringToolStripMenuItem;
        private ToolStripMenuItem gaussianToolStripMenuItem;
        private ToolStripMenuItem duplicateToolStripMenuItem;
        private CheckBox checkBoxLoadAllTheSequence;
        private ToolStripMenuItem gaussianAndDivideToolStripMenuItem;
        private ToolStripMenuItem medianAndDivideToolStripMenuItem;
        private ColumnHeader columnHeaderIndex;
        private ToolStripMenuItem displayInfosToolStripMenuItem;
        private ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private ToolStripMenuItem pSNRToolStripMenuItem;
        private NumericUpDown numericUpDownChannelForHisto;
        private System.Windows.Forms.Label label6;
        private ToolStripMenuItem sMLToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHisto;
        private ToolStripMenuItem displayToolStripMenuItem;
        private ToolStripMenuItem pointsToolStripMenuItem1;
        private ToolStripMenuItem barToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartLineProfile;
        private ToolStripMenuItem displayGridToolStripMenuItem;
        private NumericUpDown numericUpDownMaximumHisto;
        private NumericUpDown numericUpDownMinimumHisto;
        private ToolStripMenuItem normalizeToolStripMenuItem;
        private ToolStripMenuItem addNoiseToolStripMenuItem;
        private ToolStripMenuItem gaussianNoiseToolStripMenuItem;
        private ToolStripMenuItem uniformNoiseToolStripMenuItem;
        private ToolStripMenuItem poissonToolStripMenuItem;
        private ToolStripMenuItem vGradientToolStripMenuItem;
        private ToolStripMenuItem hGardientToolStripMenuItem;
        private ToolStripMenuItem centralGradientToolStripMenuItem;
        private NumericUpDown numericUpDownZRes;
        private System.Windows.Forms.Label label9;
        private NumericUpDown numericUpDownYRes;
        private System.Windows.Forms.Label label8;
        private NumericUpDown numericUpDownXRes;
        private System.Windows.Forms.Label label7;
        private ToolStripMenuItem saveHeapPicturesToolStripMenuItem;
        private ToolStripMenuItem sphereToolStripMenuItem;
        private float RatioYZ = 0f;

        #endregion

        public MainClassHeap()
            : base()
        {
            InitializeComponent();
            listViewHeap.ListViewItemSorter = new ListViewItemSorter();
            Show();
            //IMGlobal.CurrentSequenceChange += new IMGlobal.CurrentSequenceChangeDelegate(IMGlobal_CurrentSequenceChange);
        }


        bool IsFirstPos = true;
        int FirstX, FirstY;

        public override void View_MouseDoubleClick(Sequence source, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (checkBoxGetOnlyDisplayedZ.Checked)
                {

                    if (checkBoxLoadAllTheSequence.Checked)
                    {
                        for (int T = 0; T < IMGlobal.CurrentSequence.SequenceSize; T++)
                        {
                            Image3D OnlyOneSlice = new Image3D(IMGlobal.CurrentSequence.Width,
                                IMGlobal.CurrentSequence.Height,
                                1,
                                IMGlobal.CurrentSequence.NumBands);

                            for (int Band = 0; Band < IMGlobal.CurrentSequence.NumBands; Band++)
                            {
                                Array.Copy(IMGlobal.CurrentSequence[T].Data[Band],
                                    IMGlobal.CurrentSequence.DisplayedZ * IMGlobal.CurrentSequence.ScanSliceSize, OnlyOneSlice.Data[Band], 0, IMGlobal.CurrentSequence.ScanSliceSize);
                            }

                            AddImageToHeap(OnlyOneSlice, 0);
                        }
                    }
                    else
                    {
                        Image3D OnlyOneSlice = new Image3D(IMGlobal.CurrentSequence.Width,
                            IMGlobal.CurrentSequence.Height,
                            1,
                            IMGlobal.CurrentSequence.NumBands);

                        for (int Band = 0; Band < IMGlobal.CurrentSequence.NumBands; Band++)
                        {
                            Array.Copy(IMGlobal.CurrentSequence[IMGlobal.CurrentSequence.DisplayedT].Data[Band],
                                IMGlobal.CurrentSequence.DisplayedZ * IMGlobal.CurrentSequence.ScanSliceSize, OnlyOneSlice.Data[Band], 0, IMGlobal.CurrentSequence.ScanSliceSize);
                        }

                        AddImageToHeap(OnlyOneSlice, 0);


                    }
                }
                else
                {
                    if (checkBoxLoadAllTheSequence.Checked)
                    {
                        for (int T = 0; T < IMGlobal.CurrentSequence.SequenceSize; T++)
                        {
                            AddImageToHeap(IMGlobal.CurrentSequence[T], 0);
                        }
                    }
                    else
                    {
                        AddImageToHeap(IMGlobal.CurrentSequence[IMGlobal.CurrentSequence.DisplayedT], IMGlobal.CurrentSequence.DisplayedZ);
                    }
                }

                return;
            }
        }

        #region Crop


        //private void RefreshPainterForMouse(int FirstX, int FirstY, int X, int Y)
        //{
        //    //MyPainter p = new MyPainter(FirstX,
        //    //                            X,
        //    //                            FirstY,
        //    //                            Y,
        //    //                            0,
        //    //                            0,
        //    //                            tabControlMain.SelectedTab.Name);

        //    IMGlobal.CurrentSequence.RemoveAllPainters();
        //    //IMGlobal.CurrentSequence.AddPainter(p);
        //    IMGlobal.CurrentSequence.Refresh();
        //}
        bool IsCodeChanging;

        //private void RefreshPainter()
        //{
        //    //MyPainter p = new MyPainter((int)numericUpDownMinX.Value,
        //    //                            (int)numericUpDownMaxX.Value,
        //    //                            (int)numericUpDownMinY.Value,
        //    //                            (int)numericUpDownMaxY.Value,
        //    //                            (int)numericUpDownMinZ.Value,
        //    //                            (int)numericUpDownMaxZ.Value,
        //    //                            tabControlMain.SelectedTab.Name);

        //    IMGlobal.CurrentSequence.RemoveAllPainters();
        //   // IMGlobal.CurrentSequence.AddPainter(p);
        //    IMGlobal.CurrentSequence.Refresh();
        //}

        private void SplitAndProcessChannels_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IMGlobal.CurrentSequence == null) return;

            //  IMGlobal.CurrentSequence.RemoveAllPainters();
        }

        private void buttonRefreshPainter_Click(object sender, EventArgs e)
        {
            // RefreshPainter();
        }
        #endregion

        private void MainClassHeap_Load(object sender, EventArgs e)
        {
            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 3000;
            toolTip1.InitialDelay = 300;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;


            toolTip1.SetToolTip(this.panelJointHisto, "Double click to create the joint histogram sequence");

            //// Set up the ToolTip text for the Button and Checkbox.
            //toolTip1.SetToolTip(this.checkBoxLinkXY, "Constraint proportions between Width and Height");
            //toolTip1.SetToolTip(this.checkBoxLinkYZ, "Constraint proportions between Height and Depth");
            //toolTip1.SetToolTip(this.richTextBoxImageName, "Selected sequence name");
            ////toolTip1.SetToolTip(this.buttonUpdateImage, "Update the selected sequence");
            //toolTip1.SetToolTip(this.buttonApply, "Apply the image stretching");
            //toolTip1.SetToolTip(this.buttonSplitChannels, "Create a new sequence for each channel");
            //toolTip1.SetToolTip(this.buttonSplitSequence, "Create a new sequence for each time position");
            //toolTip1.SetToolTip(this.buttonSplitDepth, "Create a new sequence for each Z layer");
            //toolTip1.SetToolTip(this.labelMeanColor, "Double click here to get the mean value of the selected area");

            //comboBoxResampleAlgorithm.SelectedIndex = 0;
        }

        int SmallImageWidth = 48;
        int SmallImageHeight = 48;


        private class XImage3D
        {
            public Image3D Image;
            public string Name;

        }

        #region Heap operations

        private void displayInfosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateInfoPicture();
        }

        private void AddImageToHeap(Image3D ImageToAdd, int DispZ)
        {
            //return;
            Image3D image256 = new Image3D(ImageToAdd.Width, ImageToAdd.Height, ImageToAdd.Depth, ImageToAdd.NumBands);

            try
            {
                new IM.Library.Mathematics.MathTools().Rescale(ImageToAdd, image256, 255);
            }
            catch (Exception) { }

            Bitmap Bmp = new Bitmap(ImageToAdd.Width, ImageToAdd.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            #region LUT

            //16->8
            byte[][] LutWork = LutMaker.CreateLutNx16(ImageToAdd.NumBands);
            for (int Band = 0; Band < ImageToAdd.NumBands; Band++)
                LutMaker.LinearScale(LutWork[Band], 0, 255.0f, 0, 255.0f, false);

            //8->3x8
            byte[][][] LutView = LutMaker.CreateLutNx3x8(ImageToAdd.NumBands);

            if (ImageToAdd.NumBands == 1)
            {
                LutMaker.SetLutColor(LutView[0], Color.White);
            }
            else if (ImageToAdd.NumBands == 2)
            {
                LutMaker.SetLutColor(LutView[0], Color.Red);
                LutMaker.SetLutColor(LutView[1], Color.Lime);
            }
            else if (ImageToAdd.NumBands == 3)
            {
                LutMaker.SetLutColor(LutView[0], Color.Red);
                LutMaker.SetLutColor(LutView[1], Color.Lime);
                LutMaker.SetLutColor(LutView[2], Color.Blue);
            }

            #endregion

            image256.CopyIntoARGBBitmap(Bmp, IM.Imaging.Axis.Z, DispZ, LutWork, LutView, false);


            Bitmap BmpImagette = new Bitmap(SmallImageWidth, SmallImageHeight);

            Graphics g = Graphics.FromImage(BmpImagette);
            g.DrawImage(Bmp, 0, 0, SmallImageWidth, SmallImageHeight);

            string Clef = ImageToAdd.GetHashCode().ToString();
            imageListForHeap.Images.SetKeyName(imageListForHeap.Images.Add(BmpImagette, Color.Transparent), Clef);

            listViewHeap.Items.Add(ImageToAdd.ToString(), Clef).Tag = ImageToAdd;

            numericUpDownXRes.Value = (decimal)ImageToAdd.XResolution;
            numericUpDownYRes.Value = (decimal)ImageToAdd.YResolution;
            numericUpDownZRes.Value = (decimal)ImageToAdd.ZResolution;


            // UpdateInfoPicture(ImageToAdd);
        }

        private void InsertImageToHeap(Image3D ImageToAdd, int DispZ, int HeapPos)
        {
            Image3D image256 = new Image3D(ImageToAdd.Width, ImageToAdd.Height, ImageToAdd.Depth, ImageToAdd.NumBands);

            try
            {
                new IM.Library.Mathematics.MathTools().Rescale(ImageToAdd, image256, 255);
            }
            catch (Exception) { }

            Bitmap Bmp = new Bitmap(ImageToAdd.Width, ImageToAdd.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            #region LUT

            //16->8
            byte[][] LutWork = LutMaker.CreateLutNx16(ImageToAdd.NumBands);
            for (int Band = 0; Band < ImageToAdd.NumBands; Band++)
                LutMaker.LinearScale(LutWork[Band], 0, 255.0f, 0, 255.0f, false);

            //8->3x8
            byte[][][] LutView = LutMaker.CreateLutNx3x8(ImageToAdd.NumBands);

            if (ImageToAdd.NumBands == 1)
            {
                LutMaker.SetLutColor(LutView[0], Color.White);
            }
            else if (ImageToAdd.NumBands == 2)
            {
                LutMaker.SetLutColor(LutView[0], Color.Red);
                LutMaker.SetLutColor(LutView[1], Color.Lime);
            }
            else if (ImageToAdd.NumBands == 3)
            {
                LutMaker.SetLutColor(LutView[0], Color.Red);
                LutMaker.SetLutColor(LutView[1], Color.Lime);
                LutMaker.SetLutColor(LutView[2], Color.Blue);
            }

            #endregion

            image256.CopyIntoARGBBitmap(Bmp, IM.Imaging.Axis.Z, DispZ, LutWork, LutView, false);


            Bitmap BmpImagette = new Bitmap(SmallImageWidth, SmallImageHeight);

            Graphics g = Graphics.FromImage(BmpImagette);
            g.DrawImage(Bmp, 0, 0, SmallImageWidth, SmallImageHeight);

            string Clef = ImageToAdd.GetHashCode().ToString();
            imageListForHeap.Images.SetKeyName(imageListForHeap.Images.Add(BmpImagette, Color.Transparent), Clef);

            listViewHeap.Items.Insert(HeapPos, ImageToAdd.ToString(), Clef).Tag = ImageToAdd;
            listViewHeap.Sort();

        }

        private void textBoxImageName_TextChanged(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 1)
            {
                //((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Name = textBoxImageName.Text;
            }
            return;

        }

        private string UpdateInfoPicture(Image3D ImageToProcess)
        {
            string textBoxInfoPictureText;

            if (ImageToProcess == null)
            {
                textBoxInfoPicture.Text = "No picture selected";
                textBoxImageName.Enabled = false;
                numericUpDownChannelForHisto.Enabled = false;
                radioButtonHistoTypeClassic.Enabled = false;
                radioButtonHistoTypeCumulated.Enabled = false;
                UpdateHisto(null, 0);
                return "";
            }

            textBoxImageName.Enabled = true;
            //textBoxImageName.Text = ImageToProcess.Name;
            textBoxInfoPictureText = "Width : " + ImageToProcess.Width.ToString() + "\r\n";
            textBoxInfoPictureText += "Height : " + ImageToProcess.Height.ToString() + "\r\n";
            textBoxInfoPictureText += "Depth : " + ImageToProcess.Depth.ToString() + "\r\n";
            textBoxInfoPictureText += "Number of channels : " + ImageToProcess.NumBands.ToString() + "\r\n";
            textBoxInfoPictureText += "X Scale : " + ImageToProcess.XResolution + "\r\n";
            textBoxInfoPictureText += "Y Scale : " + ImageToProcess.YResolution + "\r\n";
            textBoxInfoPictureText += "Z Scale : " + ImageToProcess.ZResolution + "\r\n";

            float TmpValue;
            for (int channel = 0; channel < ImageToProcess.NumBands; channel++)
            {
                textBoxInfoPictureText += "----------------------\r\n";

                textBoxInfoPictureText += "Channel " + channel.ToString() + "\r\n";
                float minValue = new IM.Library.Mathematics.MathTools().Min(ImageToProcess.Data[channel]);
                textBoxInfoPictureText += "Min : " + minValue.ToString() + "\r\n";

                float maxValue = new IM.Library.Mathematics.MathTools().Max(ImageToProcess.Data[channel]);
                textBoxInfoPictureText += "Max : " + maxValue.ToString() + "\r\n";

                TmpValue = new IM.Library.Mathematics.MathTools().Mean(ImageToProcess.Data[channel]);
                textBoxInfoPictureText += "Mean : " + TmpValue.ToString() + "\r\n";

                TmpValue = new IM.Library.Mathematics.MathTools().Std(ImageToProcess.Data[channel]);
                textBoxInfoPictureText += "Std : " + TmpValue.ToString() + "\r\n";

                TmpValue = new IM.Library.Mathematics.MathTools().MAD(ImageToProcess.Data[channel], true);
                textBoxInfoPictureText += "MAD : " + TmpValue.ToString() + "\r\n";

                TmpValue = new IM.Library.Mathematics.MathTools().Median(ImageToProcess.Data[channel], 0, ImageToProcess.ImageSize - 1);
                textBoxInfoPictureText += "Median : " + TmpValue.ToString() + "\r\n";

                TmpValue = new IM.Library.Mathematics.MathTools().Skew(ImageToProcess.Data[channel]);
                textBoxInfoPictureText += "Skewness : " + TmpValue.ToString() + "\r\n";

                TmpValue = new IM.Library.Mathematics.MathTools().Kurt(ImageToProcess.Data[channel]);
                textBoxInfoPictureText += "Kurtosis : " + TmpValue.ToString() + "\r\n";



            }

            UpdateHisto(ImageToProcess, (int)numericUpDownChannelForHisto.Value);
            textBoxInfoPicture.Text = textBoxInfoPictureText;

            return textBoxInfoPictureText;
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab.Name == "tabPageJointHisto")
            {
                ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
                if (indexes.Count != 2)
                {
                    UpdateJointHisto(null, null);
                    return;
                }
                UpdateJointHisto((Image3D)(this.listViewHeap.Items[indexes[0]].Tag), (Image3D)(this.listViewHeap.Items[indexes[1]].Tag));
            }
            if (tabControlMain.SelectedTab.Name == "tabPageHisto")
            {
                ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
                if (indexes.Count == 0)
                {
                    UpdateHisto(null, 0);
                    return;
                }
                UpdateHisto((Image3D)(this.listViewHeap.Items[indexes[0]].Tag), (int)numericUpDownChannelForHisto.Value);
            }
            if (tabControlMain.SelectedTab.Name == "tabPageColor3D")
            {
                ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
                if (indexes.Count == 0)
                {
                    //UpdateColorSpace(null);
                    return;
                }
                //  UpdateColorSpace(indexes);
            }
        }

        private void checkBoxOperaModeFor3D_CheckedChanged(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0)
            {
                // UpdateColorSpace(null);
                return;
            }
            //UpdateColorSpace(indexes);
        }

        private void checkBoxHistoLog_CheckedChanged(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0)
            {
                UpdateHisto(null, 0);
                return;
            }
            UpdateHisto((Image3D)(this.listViewHeap.Items[indexes[0]].Tag), (int)numericUpDownChannelForHisto.Value);
        }

        Bitmap HistoBMP;
        Bitmap ProfilBMP;

        // Paint control
        protected override void OnPaint(PaintEventArgs e)
        {

            if (tabControlMain.SelectedTab.Name == "tabPageJointHisto")
            {
                ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
                if (indexes.Count != 2)
                {
                    UpdateJointHisto(null, null);
                    return;
                }
                UpdateJointHisto((Image3D)(this.listViewHeap.Items[indexes[0]].Tag), (Image3D)(this.listViewHeap.Items[indexes[1]].Tag));
                base.OnPaint(e);
            }

            //if ((tabControlMain.SelectedTab.Name == "tabPageHisto") && (HistoBMP != null))
            //{
            //    Graphics g1 = pictureBoxHistogram.CreateGraphics();
            //    g1.DrawImage(HistoBMP, 0, 0);
            //    // Calling the base class OnPaint
            //    base.OnPaint(e);
            //}
            //else if ((tabControlMain.SelectedTab.Name == "tabPageLineProfil") && (ProfilBMP != null))
            //{
            //    Graphics g2 = pictureBoxLineProfil.CreateGraphics();
            //    g2.DrawImage(ProfilBMP, 0, 0);
            //    // Calling the base class OnPaint
            //    base.OnPaint(e);

            //}



        }

        private void UpdateInfoPicture()
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0)
            {
                UpdateInfoPicture(null);
                return;
            }
            UpdateInfoPicture((Image3D)(this.listViewHeap.Items[indexes[0]].Tag));

        }

        bool DoNotDisplay = false;

        private void listViewHeap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            Sequence NewSeq = new Sequence();
            Image3D TmpIm = this.listViewHeap.Items[indexes[0]].Tag as Image3D;

            NewSeq.XResolution = 1;
            NewSeq.YResolution = 1;
            NewSeq.ZResolution = 1;


            NewSeq.Add(TmpIm);
            //NewSeq.Name = TmpIm.Name;

            DoNotDisplay = true;
            //IMGlobal.AddSequence(NewSeq,TmpIm.Name);

            NewSeq.XResolution = (double)numericUpDownXRes.Value;
            NewSeq.YResolution = (double)numericUpDownYRes.Value;
            NewSeq.ZResolution = (double)numericUpDownZRes.Value;

            IMGlobal.AddSequence(NewSeq);
            DoNotDisplay = false;


        }

        private void listViewHeap_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;

            if (tabControlMain.SelectedTab.Name == "tabPageJointHisto")
            {
                if (indexes.Count != 2)
                {
                    UpdateJointHisto(null, null);
                    return;
                }
                UpdateJointHisto((Image3D)(this.listViewHeap.Items[indexes[0]].Tag), (Image3D)(this.listViewHeap.Items[indexes[1]].Tag));
                Invalidate();
            }
            if ((tabControlMain.SelectedTab.Name == "tabPageHisto") /*&& (HistoBMP != null)*/)
            {
                if (indexes.Count == 0)
                {
                    UpdateHisto(null, 0);
                    return;
                }
                UpdateHisto((Image3D)(this.listViewHeap.Items[indexes[0]].Tag), (int)numericUpDownChannelForHisto.Value);
                Invalidate();
            }
            else if (tabControlMain.SelectedTab.Name == "tabPageInfos")
            {
                //if (indexes.Count == 0)
                //{
                //    UpdateInfoPicture(null);
                //    return;
                //}
                //UpdateInfoPicture((Image3D)(this.listViewHeap.Items[indexes[0]].Tag));
            }
            else if (tabControlMain.SelectedTab.Name == "tabPageColor3D")
            {
                if (indexes.Count == 0)
                {
                    //UpdateColorSpace(null);
                    return;
                }
                //UpdateColorSpace(indexes);
            }

        }
        #endregion

        #region ToolStripMenu


        private void depthSplitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            int indexesCount = indexes.Count;
            int count = 0;

            for (int i = 0; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));
                int DepthNumber = TmpIm.Depth;

                for (int indexIm = 0; indexIm < DepthNumber; indexIm++)
                {
                    Image3D Result = new Image3D(TmpIm.Width,
                                                 TmpIm.Height,
                                                 1,
                                                 TmpIm.NumBands);
                    //Result.Name = "Depth split" + count.ToString();
                    count++;
                    for (int Channel = 0; Channel < TmpIm.NumBands; Channel++)
                    {
                        Array.Copy(TmpIm.Data[Channel], indexIm * TmpIm.ScanSliceSize, Result.Data[Channel], 0, Result.ImageSize);
                    }
                    AddImageToHeap(Result, 0);
                }
            }
            indexes.Clear();
        }

        private void channelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            int indexesCount = indexes.Count;
            int count = 0;

            for (int i = 0; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));
                int ChannelNumber = TmpIm.NumBands;

                for (int indexIm = 0; indexIm < ChannelNumber; indexIm++)
                {
                    Image3D Result = new Image3D(TmpIm.Width,
                                                 TmpIm.Height,
                                                 TmpIm.Depth,
                                                 1);
                    //Result.Name = "Channel split" + count.ToString();
                    count++;
                    Array.Copy(TmpIm.Data[indexIm], Result.Data[0], Result.ImageSize);
                    AddImageToHeap(Result, 0);
                }
            }
            indexes.Clear();

        }

        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProfileToFile(IMGlobal.CurrentSequence[IMGlobal.CurrentSequence.DisplayedT], IMGlobal.CurrentSequence.DisplayedZ);

        }

        private void displayInChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertToImage(IMGlobal.CurrentSequence[IMGlobal.CurrentSequence.DisplayedT], IMGlobal.CurrentSequence.DisplayedZ);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            int indexesCount = indexes.Count;
            for (int i = 0; i < indexesCount; i++)
            {
                listViewHeap.Items.RemoveAt(indexes[0]);
            }
            listViewHeap.Sort();
            // UpdateInfoPicture();

        }


        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            DuplicateRequest RQuest = new DuplicateRequest();

            if (RQuest.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            int DuplicateTime = RQuest.numericUpDownDuplicateValue;

            int indexesCount = indexes.Count;

            Sequence NewSeq = null;

            for (int i = 0; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));

                int NumChannel = TmpIm.NumBands;

                if (RQuest.checkBoxIsSequenceBool)
                {
                    NewSeq = new Sequence();
                }



                for (int Iter = 0; Iter < DuplicateTime; Iter++)
                {
                    Image3D Result = new Image3D(TmpIm.Width, TmpIm.Height, TmpIm.Depth, NumChannel);

                    for (int Band = 0; Band < NumChannel; Band++)
                    {
                        Array.Copy(TmpIm.Data[Band], Result.Data[0], TmpIm.ImageSize);
                    }



                    if (RQuest.checkBoxIsSequenceBool)
                    {
                        NewSeq.Add(Result);
                    }
                    else
                    {
                        AddImageToHeap(Result, 0);
                    }
                }


                if (RQuest.checkBoxIsSequenceBool)
                {
                    IMGlobal.AddSequence(NewSeq, "Duplicate x" + DuplicateTime);
                }

            }


            indexes.Clear();
        }



        private void deleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This action will clear the whole heap, are you sure ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            listViewHeap.Items.Clear();
            //    UpdateInfoPicture();
        }

        private void activateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            for (int i = 0; i < indexes.Count; i++)
            {
                Sequence NewSeq = new Sequence();
                Image3D TmpIm = (Image3D)(this.listViewHeap.Items[indexes[i]].Tag);
                NewSeq.Add(TmpIm);
                //NewSeq.Name = TmpIm.Name;
                //if (TmpIm.Name == "")
                IMGlobal.AddSequence(NewSeq);
                //else
                //    IMGlobal.AddSequence(NewSeq, TmpIm.Name);
            }

        }

        private bool CheckDimensions()
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return false;
            if (indexes.Count == 1) return true;

            int indexesCount = indexes.Count;
            int PrevSizeX = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width;
            int PrevSizeY = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height;
            int PrevSizeZ = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth;

            for (int i = 1; i < indexesCount; i++)
            {
                if (PrevSizeX != ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Width)
                    return false;
                PrevSizeX = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Width;

                if (PrevSizeY != ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Height)
                    return false;
                PrevSizeY = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Height;

                if (PrevSizeZ != ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Depth)
                    return false;
                PrevSizeZ = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Depth;

            }
            return true;
        }

        private bool CheckHeight()
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return false;
            if (indexes.Count == 1) return true;

            int indexesCount = indexes.Count;
            int PrevSizeY = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height;

            for (int i = 1; i < indexesCount; i++)
            {
                if (PrevSizeY != ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Height)
                    return false;
                PrevSizeY = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Height;
            }
            return true;
        }

        private bool CheckNumBands()
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return false;
            if (indexes.Count == 1) return true;

            int indexesCount = indexes.Count;
            int PrevNumBands = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands;

            for (int i = 1; i < indexesCount; i++)
            {
                if (PrevNumBands != ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).NumBands)
                    return false;
                PrevNumBands = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).NumBands;
            }
            return true;
        }

        private bool CheckDimensions2D()
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return false;
            if (indexes.Count == 1) return true;

            int indexesCount = indexes.Count;
            int PrevSizeX = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width;
            int PrevSizeY = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height;

            for (int i = 1; i < indexesCount; i++)
            {
                if (PrevSizeX != ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Width)
                    return false;
                PrevSizeX = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Width;

                if (PrevSizeY != ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Height)
                    return false;
                PrevSizeY = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Height;
            }
            return true;
        }

        private bool CheckChannelCompatibility()
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return false;
            if (indexes.Count == 1) return true;
            int indexesCount = indexes.Count;
            int PrevSizeChannel = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands;

            for (int i = 1; i < indexesCount; i++)
            {
                if (PrevSizeChannel != ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).NumBands)
                    return false;
                PrevSizeChannel = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).NumBands;
            }

            return true;
        }

        /// <summary>
        /// Create null image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nullImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count > 1)
            {
                MessageBox.Show("Too many images selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int DimX = 256;
            int DimY = 256;
            int DimZ = 1;
            int Band = 1;
            float Value = 0.0f;

            if (indexes.Count == 1)
            {
                DimX = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width;
                DimY = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height;
                DimZ = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth;
                Band = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands;

            }
            RequestImage RQuest = new RequestImage();
            //RQuest.ShowDialog();
            Control[] ButtonControl = RQuest.Controls.Find("numericUpDownDimX", false);
            NumericUpDown TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = DimX;

            ButtonControl = RQuest.Controls.Find("numericUpDownDimY", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = DimY;

            ButtonControl = RQuest.Controls.Find("numericUpDownDimZ", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = DimZ;

            ButtonControl = RQuest.Controls.Find("numericUpDownBand", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = Band;

            ButtonControl = RQuest.Controls.Find("numericUpDownIntensity", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = (decimal)Value;


            if (RQuest.ShowDialog() != DialogResult.OK)
            {
                return;
            }


            CreateNewImage(RQuest.numericUpDownDimXValue,
                RQuest.numericUpDownDimYValue,
                RQuest.numericUpDownDimZValue,
                RQuest.numericUpDownBandValue,
                RQuest.numericUpDownIntensityValue);

            indexes.Clear();

        }

        private void CreateNewImage(int DimX, int DimY, int DimZ, int Channel, float Value)
        {
            Image3D UnitImage = new Image3D(DimX, DimY, DimZ, Channel);

            for (int TmpChannel = 0; TmpChannel < UnitImage.NumBands; TmpChannel++)
            {
                for (int ijk = 0; ijk < UnitImage.ImageSize; ijk++)
                    UnitImage.Data[TmpChannel][ijk] = Value;
            }

            //UnitImage.Name = "Unit Image";
            AddImageToHeap(UnitImage, 0);




        }


        private void gaussianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //RequestInfo
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count > 1)
            {
                MessageBox.Show("Too many images selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RequestInfo RQuest = new RequestInfo();




            if (RQuest.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            float StartSigma = RQuest.numericUpDownStartSigmaValue;
            float EndSigma = RQuest.numericUpDownEndSigmaValue;
            float StepSigma = RQuest.numericUpDownSigmaStepValue;
            Image3D CurrentIM = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag));

            Sequence ResSeq = new Sequence();

            for (float CurrentSigma = StartSigma; CurrentSigma <= EndSigma; CurrentSigma += StepSigma)
            {
                Image3D BlurredIM = new Image3D(CurrentIM.Width, CurrentIM.Height, CurrentIM.Depth, CurrentIM.NumBands);
                for (int Channel = 0; Channel < CurrentIM.NumBands; Channel++)
                {
                    new Convolution().ConvolveFast(CurrentIM, Channel, BlurredIM, Channel, new IM.Library.Filtering.KernelMaker().MakeGaussianKernel(CurrentSigma),
                        new IM.Library.Filtering.KernelMaker().MakeGaussianKernel(CurrentSigma), IM.Library.BoundaryConditions.Mirror);
                }
                ResSeq.Add(BlurredIM);
            }

            IMGlobal.AddSequence(ResSeq, "Blurred Images :(" + StartSigma + "..." + EndSigma + ")");


        }




        private void unitImageToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count > 1)
            {
                MessageBox.Show("Too many images selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int DimX = 256;
            int DimY = 256;
            int DimZ = 1;
            int Band = 1;
            float Value = 1.0f;

            if (indexes.Count == 1)
            {
                DimX = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width;
                DimY = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height;
                DimZ = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth;
                Band = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands;

            }
            RequestImage RQuest = new RequestImage();
            //RQuest.ShowDialog();
            Control[] ButtonControl = RQuest.Controls.Find("numericUpDownDimX", false);
            NumericUpDown TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = DimX;

            ButtonControl = RQuest.Controls.Find("numericUpDownDimY", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = DimY;

            ButtonControl = RQuest.Controls.Find("numericUpDownDimZ", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = DimZ;

            ButtonControl = RQuest.Controls.Find("numericUpDownBand", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = Band;

            ButtonControl = RQuest.Controls.Find("numericUpDownIntensity", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = (decimal)Value;


            if (RQuest.ShowDialog() != DialogResult.OK)
            {
                return;
            }


            CreateNewImage(RQuest.numericUpDownDimXValue,
                RQuest.numericUpDownDimYValue,
                RQuest.numericUpDownDimZValue,
                RQuest.numericUpDownBandValue,
                RQuest.numericUpDownIntensityValue);

            indexes.Clear();

        }






        /// <summary>
        /// Merge by channels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void channelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;
            if (indexes.Count == 1) return;

            int NewBandNumber = 0;
            int PrevSizeX = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width;
            int PrevSizeY = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height;
            int PrevSizeZ = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth;

            int indexesCount = indexes.Count;

            for (int i = 0; i < indexesCount; i++)
            {
                NewBandNumber += ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).NumBands;
            }

            //if (NewBandNumber > 3)
            //{
            //    MessageBox.Show("Too many channels to merge", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            if (CheckDimensions() == false)
            {
                MessageBox.Show("Image dimensions do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // first of all, copy the current sequence
            Image3D MergedImage = new Image3D(PrevSizeX,
                            PrevSizeY,
                            PrevSizeZ,
                            NewBandNumber);

            int Channel = 0;

            for (int T = 0; T < indexesCount; T++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[T]].Tag));
                for (int TmpChannel = 0; TmpChannel < TmpIm.NumBands; Channel++, TmpChannel++)
                    Array.Copy(TmpIm.Data[TmpChannel], MergedImage.Data[Channel], MergedImage.Data[0].Length);
            }

            //MergedImage.Name = "Merged picture";
            AddImageToHeap(MergedImage, 0);
            indexes.Clear();

        }

        /// <summary>
        /// Merge by time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;
            if (indexes.Count == 1) return;

            int PrevSizeX = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width;
            int PrevSizeY = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height;
            int PrevNbrChannels = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands;

            int indexesCount = indexes.Count;
            if (CheckDimensions() == false)
            {
                MessageBox.Show("Image dimensions do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (CheckChannelCompatibility() == false)
            {
                MessageBox.Show("Image channels do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Sequence NewSeq = new Sequence();

            for (int T = 0; T < indexesCount; T++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[T]].Tag));
                NewSeq.Add(TmpIm);
            }

            IMGlobal.AddSequence(NewSeq);

        }




        /// <summary>
        /// Merge by depth
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void depthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;
            if (indexes.Count == 1) return;

            int NewDepth = 0;
            int PrevSizeX = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width;
            int PrevSizeY = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height;
            int PrevNbrChannels = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands;

            int indexesCount = indexes.Count;

            for (int i = 0; i < indexesCount; i++)
            {
                NewDepth += ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Depth;
            }

            if (CheckDimensions2D() == false)
            {
                MessageBox.Show("Image dimensions do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (CheckChannelCompatibility() == false)
            {
                MessageBox.Show("Image channels do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // first of all, copy the current sequence
            Image3D MergedImage = new Image3D(PrevSizeX,
                            PrevSizeY,
                            NewDepth,
                            PrevNbrChannels);

            int DestIndex = 0;

            for (int T = 0; T < indexesCount; T++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[T]].Tag));
                for (int TmpChannel = 0; TmpChannel < TmpIm.NumBands; TmpChannel++)
                {
                    Array.Copy(TmpIm.Data[TmpChannel], 0, MergedImage.Data[TmpChannel], DestIndex, TmpIm.Data[TmpChannel].Length);
                }
                DestIndex += TmpIm.ImageSize;
            }

            //MergedImage.Name = "Merged picture";
            AddImageToHeap(MergedImage, 0);
            indexes.Clear();
        }
        #endregion

        private void listViewHeap_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                if (MessageBox.Show("This action will delete permanently pictures, are you sure ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;

                ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
                if (indexes.Count == 0) return;

                int indexesCount = indexes.Count;
                for (int i = 0; i < indexesCount; i++)
                {
                    listViewHeap.Items.RemoveAt(indexes[0]);
                }
                //  UpdateInfoPicture();
            }
            if ((e.KeyCode == Keys.Return) || (e.KeyCode == Keys.Enter))
            {
                ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
                if (indexes.Count == 0) return;

                for (int i = 0; i < indexes.Count; i++)
                {
                    Sequence NewSeq = new Sequence();
                    Image3D TmpIm = (Image3D)(this.listViewHeap.Items[indexes[i]].Tag);
                    NewSeq.Add(TmpIm);
                    //if(TmpIm.Name=="")
                    IMGlobal.AddSequence(NewSeq);
                    //else
                    //IMGlobal.AddSequence(NewSeq,TmpIm.Name);
                }
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                channelToolStripMenuItem1_Click(sender, e);
            }
            if (e.Alt && e.KeyCode == Keys.C)
            {
                channelToolStripMenuItem_Click(sender, e);
            }
            if (e.KeyCode == Keys.F5)
            {
                UpdateInfoPicture();
            }


        }

        #region Basic Operations





        private void meanSquareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count != 2)
            {
                MessageBox.Show("Please select 2 images", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckDimensions() == false)
            {
                MessageBox.Show("Image dimensions do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckChannelCompatibility() == false)
            {
                MessageBox.Show("Number of channel does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Image3D TmpIm0 = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag));
            Image3D TmpIm1 = ((Image3D)(this.listViewHeap.Items[indexes[1]].Tag));

            float ResultChannel = 0.0f;

            for (int channel = 0; channel < TmpIm0.NumBands; channel++)
            {
                ResultChannel = new IM.Library.Mathematics.ArrayDistance().Euclidian(TmpIm0.Data[channel], TmpIm1.Data[channel]);
                ResultChannel /= (float)TmpIm0.ImageSize;
                Console.WriteLine("Mean square distance on channel " + channel.ToString() + " = " + ResultChannel.ToString());
            }


        }
        private void pSNRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count != 2)
            {
                MessageBox.Show("Please select 2 images", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckDimensions() == false)
            {
                MessageBox.Show("Image dimensions do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckChannelCompatibility() == false)
            {
                MessageBox.Show("Number of channel does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Image3D TmpIm0 = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag));
            Image3D TmpIm1 = ((Image3D)(this.listViewHeap.Items[indexes[1]].Tag));

            float ResultChannel = 0.0f;

            for (int channel = 0; channel < TmpIm0.NumBands; channel++)
            {
                ResultChannel = new IM.Library.Mathematics.ArrayDistance().PSNR(TmpIm0, channel, TmpIm1, channel);
                Console.Write("PSNR. on channel " + channel.ToString() + " = " + ResultChannel.ToString());
            }
        }

        private void kappaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count != 2)
            {
                MessageBox.Show("Please select 2 images", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckDimensions() == false)
            {
                MessageBox.Show("Image dimensions do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckChannelCompatibility() == false)
            {
                MessageBox.Show("Number of channel does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Image3D TmpIm0 = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag));
            Image3D TmpIm1 = ((Image3D)(this.listViewHeap.Items[indexes[1]].Tag));

            float ResultChannel = 0.0f;

            for (int channel = 0; channel < TmpIm0.NumBands; channel++)
            {
                ResultChannel = new IM.Library.Mathematics.ArrayDistance().KappaCoefficient(TmpIm0.Data[channel], TmpIm1.Data[channel]);
                Console.Write("Kappa coeff. on channel " + channel.ToString() + " = " + ResultChannel.ToString());
            }
        }

        private void segmentationEvaluationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count != 2)
            {
                MessageBox.Show("Please select 2 images", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckDimensions() == false)
            {
                MessageBox.Show("Image dimensions do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckChannelCompatibility() == false)
            {
                MessageBox.Show("Number of channel does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Image3D TmpIm0 = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag));
            Image3D TmpIm1 = ((Image3D)(this.listViewHeap.Items[indexes[1]].Tag));

            float ResultChannel = 0.0f;

            for (int channel = 0; channel < TmpIm0.NumBands; channel++)
            {
                ResultChannel = new IM.Library.Mathematics.ArrayDistance().SimpleSegmentationEvaluation(TmpIm0.Data[channel], TmpIm1.Data[channel]);
                Console.Write("Segmentation evaluation on channel " + channel.ToString() + " = " + ResultChannel.ToString());
            }
        }

        private void hausdorffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count != 2)
            {
                MessageBox.Show("Please select 2 images", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckChannelCompatibility() == false)
            {
                MessageBox.Show("Number of channel does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Image3D TmpIm0 = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag));
            Image3D TmpIm1 = ((Image3D)(this.listViewHeap.Items[indexes[1]].Tag));

            float ResultChannel = 0.0f;

            for (int channel = 0; channel < TmpIm0.NumBands; channel++)
            {
                ResultChannel = new IM.Library.Mathematics.ArrayDistance().Hausdorff2D(TmpIm0, channel, TmpIm1, channel);
                Console.Write("Hausdorff on channel " + channel.ToString() + " = " + ResultChannel.ToString());
            }

        }

        #endregion

        int numericUpDownHistoLinePositionValue;

        //void IMGlobal_CurrentSequenceChange()
        //{

        //    //UpDateControls();
        //    try
        //    {
        //        sTmp.MouseDown -= new Sequence.MouseDownDelegate(sTmp_MouseDown);
        //        sTmp.MouseOver -= new Sequence.MouseOverDelegate(sTmp_MouseOver);
        //        sTmp.MouseUp -= new Sequence.MouseUpDelegate(sTmp_MouseUp);
        //    }
        //    catch (Exception) { }


        //    IsFirstPos = true;
        //    sTmp = IMGlobal.CurrentSequence;



        //    if ((this.Visible) && (tabControlMain.SelectedTab.Name == "tabPageLineProfil") && (DoNotDisplay==false))
        //    {
        //        radioButtonColumn_CheckedChanged(null, null);
        //        DoNotDisplay = false;
        //        //UpDateSmallControls(sTmp);
        //        //sTmp.RemoveAllPainters();
        //        //    IMGlobal.CurrentSequence.Refresh();
        //    }

        //    if (sTmp != null)
        //    {
        //        sTmp.MouseDown += new Sequence.MouseDownDelegate(sTmp_MouseDown);
        //        sTmp.MouseOver += new Sequence.MouseOverDelegate(sTmp_MouseOver);
        //        sTmp.MouseUp += new Sequence.MouseUpDelegate(sTmp_MouseUp);
        //    }
        //}

        private void MainClassHeap_ResizeEnd(object sender, EventArgs e)
        {


            if (tabControlMain.SelectedTab.Name == "tabPageJointHisto")
            {
                ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
                if (indexes.Count != 2)
                {
                    UpdateJointHisto(null, null);
                    return;
                }

                UpdateJointHisto((Image3D)(this.listViewHeap.Items[indexes[0]].Tag), (Image3D)(this.listViewHeap.Items[indexes[1]].Tag));
                Invalidate();
            }


            if ((tabControlMain.SelectedTab.Name == "tabPageLineProfil") && (ProfilBMP != null))
            {
                radioButtonColumn_CheckedChanged(null, null);
                Invalidate();
                //Refresh();
            }

            if ((tabControlMain.SelectedTab.Name == "tabPageHisto") && (HistoBMP != null))
            {
                ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
                if (indexes.Count == 0)
                {
                    UpdateHisto(null, 0);
                    return;
                }
                UpdateHisto((Image3D)(this.listViewHeap.Items[indexes[0]].Tag), (int)numericUpDownChannelForHisto.Value);
                Invalidate();
            }
            if (tabControlMain.SelectedTab.Name == "tabPageColor3D")
            {
                ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
                if (indexes.Count == 0)
                {
                    // UpdateColorSpace(null);
                    return;
                }
                //UpdateColorSpace(indexes);
                //Invalidate();
            }
        }

        private void saveViewToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        #region Create Report
        private void createReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0)
            {
                return;
            }
            else
            {

                Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);


                ReportWriter rw = new ReportWriter();
                rw.create("tellme.xml");
                rw.setInfo("First Report", "Don't know name", "Description", 5, 100);

                int indexesCount = indexes.Count;
                for (int i = 0; i < indexesCount; i++)
                {
                    Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));
                    rw.addThumbnailImage(TmpIm, "Test" + i, "", "essai");
                    //rw.addThumbnailImage(TmpIm, "Test"+i, TmpIm.Name, "essai");
                }
                rw.close();


            }

        }

        #endregion

        private void vectorFieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count != 2)
            {
                MessageBox.Show("This operation requires 2 images", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int indexesCount = indexes.Count;

            if (CheckDimensions() == false)
            {
                MessageBox.Show("Image dimensions do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            request RQuest = new request();
            Control[] ButtonControl = RQuest.Controls.Find("buttonCreateGrid", false);
            ButtonControl[0].Text = "Create Field";

            if (RQuest.ShowDialog() != DialogResult.OK)
            {
                return;
            }



            Image3D VectorField = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                                            ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                                            ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                                            ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);


            new IM.Library.Tools.Drawing().VectorField2D((Image3D)(this.listViewHeap.Items[indexes[0]].Tag), 0,
                                                        (Image3D)(this.listViewHeap.Items[indexes[1]].Tag), 0,
                                                        VectorField, 0, 0.5f, (int)RQuest.numericUpDownGridSizeValue);
            Sequence NewSeq = new Sequence();
            NewSeq.Add(VectorField);
            IMGlobal.AddSequence(NewSeq, "Vector field");
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;
            if (indexes.Count == 1) return;

            if (CheckDimensions() == false)
            {
                MessageBox.Show("Image dimensions do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckChannelCompatibility() == false)
            {
                MessageBox.Show("Number of channel does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);

            for (int channel = 0; channel < Result.NumBands; channel++)
                Array.Copy(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Data[channel],
                    Result.Data[channel],
                    Result.ImageSize);

            int indexesCount = indexes.Count;
            for (int i = 1; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));
                new IM.Library.Mathematics.MathTools().Add(ref Result, Result, TmpIm);
            }
            //Result.Name = "Addition result";

            AddImageToHeap(Result, 0);
            indexes.Clear();
        }

        private void substractToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;
            if (indexes.Count == 1) return;

            if (CheckDimensions() == false)
            {
                MessageBox.Show("Image dimensions do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckChannelCompatibility() == false)
            {
                MessageBox.Show("Number of channel does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);

            for (int channel = 0; channel < Result.NumBands; channel++)
                Array.Copy(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Data[channel],
                    Result.Data[channel],
                    Result.ImageSize);

            int indexesCount = indexes.Count;
            for (int i = 1; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));
                new IM.Library.Mathematics.MathTools().Subtract(ref Result, Result, TmpIm);
            }
            //Result.Name = "Substraction result";

            AddImageToHeap(Result, 0);
            indexes.Clear();
        }

        private void divideToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;
            if (indexes.Count == 1) return;

            if (CheckDimensions() == false)
            {
                MessageBox.Show("Image dimensions do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckChannelCompatibility() == false)
            {
                MessageBox.Show("Number of channel does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);

            for (int channel = 0; channel < Result.NumBands; channel++)
                Array.Copy(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Data[channel],
                    Result.Data[channel],
                    Result.ImageSize);

            int indexesCount = indexes.Count;
            for (int i = 1; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));
                new IM.Library.Mathematics.MathTools().Divide(ref Result, Result, TmpIm);
            }

            //Result.Name = "Division result";

            AddImageToHeap(Result, 0);
            indexes.Clear();
        }

        private void sqrtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);


            int indexesCount = indexes.Count;
            for (int i = 0; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));
                new IM.Library.Mathematics.MathTools().Pow(ref Result, TmpIm, 0.5f);
                //Result.Name = "Sqrt result";
                AddImageToHeap(Result, 0);
            }

            indexes.Clear();
        }

        private void multiplyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;
            if (indexes.Count == 1) return;

            if (CheckDimensions() == false)
            {
                MessageBox.Show("Image dimensions do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckChannelCompatibility() == false)
            {
                MessageBox.Show("Number of channel does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);

            for (int channel = 0; channel < Result.NumBands; channel++)
                Array.Copy(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Data[channel],
                    Result.Data[channel],
                    Result.ImageSize);

            int indexesCount = indexes.Count;
            for (int i = 1; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));
                new IM.Library.Mathematics.MathTools().Multiply(ref Result, Result, TmpIm);
            }
            //Result.Name = "Multiplication result";

            AddImageToHeap(Result, 0);
            indexes.Clear();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;
            if (indexes.Count == 1) return;

            if (CheckDimensions() == false)
            {
                MessageBox.Show("Image dimensions do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckChannelCompatibility() == false)
            {
                MessageBox.Show("Number of channel does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);

            for (int channel = 0; channel < Result.NumBands; channel++)
                Array.Copy(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Data[channel],
                    Result.Data[channel],
                    Result.ImageSize);

            int indexesCount = indexes.Count;
            for (int i = 1; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));
                new IM.Library.Mathematics.MathTools().Add(ref Result, Result, TmpIm);
            }
            // Result.Name = "Addition result";

            AddImageToHeap(Result, 0);
            indexes.Clear();
        }

        private void substractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;
            if (indexes.Count == 1) return;

            if (CheckDimensions() == false)
            {
                MessageBox.Show("Image dimensions do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckChannelCompatibility() == false)
            {
                MessageBox.Show("Number of channel does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);

            for (int channel = 0; channel < Result.NumBands; channel++)
                Array.Copy(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Data[channel],
                    Result.Data[channel],
                    Result.ImageSize);

            int indexesCount = indexes.Count;
            for (int i = 1; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));
                new IM.Library.Mathematics.MathTools().Subtract(ref Result, Result, TmpIm);
            }
            //Result.Name = "Substraction result";

            AddImageToHeap(Result, 0);
            indexes.Clear();
        }

        private void multiplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count != 2) return;

            if (CheckDimensions() == false)
            {
                MessageBox.Show("Image dimensions do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int PrevSizeChannel = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands;

            if (PrevSizeChannel != 2)
            {
                MessageBox.Show("Number of channel must equal 2", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                2);

            Image3D TmpIm0 = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag));
            Image3D TmpIm1 = ((Image3D)(this.listViewHeap.Items[indexes[1]].Tag));

            for (int i = 0; i < TmpIm0.ImageSize; i++)
            {
                Result.Data[0][i] = TmpIm0.Data[0][i] * TmpIm1.Data[0][i] - TmpIm0.Data[1][i] * TmpIm1.Data[1][i];
                Result.Data[1][i] = TmpIm1.Data[0][i] * TmpIm0.Data[1][i] + TmpIm0.Data[0][i] * TmpIm1.Data[1][i];
            }

            //Result.Name = "Cplx Multiplication result";

            AddImageToHeap(Result, 0);
            indexes.Clear();
        }

        private void divideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count != 2) return;

            if (CheckDimensions() == false)
            {
                MessageBox.Show("Image dimensions do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int PrevSizeChannel = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands;

            if (PrevSizeChannel != 2)
            {
                MessageBox.Show("Number of channel must equal 2", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                2);

            Image3D TmpIm0 = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag));
            Image3D TmpIm1 = ((Image3D)(this.listViewHeap.Items[indexes[1]].Tag));

            float Denom = 0.0f;

            for (int i = 0; i < TmpIm0.ImageSize; i++)
            {
                Denom = TmpIm1.Data[0][i] * TmpIm1.Data[0][i] - TmpIm1.Data[1][i] * TmpIm1.Data[1][i];
                if (Denom != 0.0f)
                {
                    Result.Data[0][i] = TmpIm0.Data[0][i] * TmpIm1.Data[0][i] + TmpIm0.Data[1][i] * TmpIm1.Data[1][i];
                    Result.Data[0][i] /= Denom;
                    Result.Data[1][i] = TmpIm0.Data[1][i] * TmpIm1.Data[0][i] - TmpIm1.Data[1][i] * TmpIm0.Data[0][i];
                    Result.Data[1][i] /= Denom;
                }
            }

            //Result.Name = "Cplx Division result";

            AddImageToHeap(Result, 0);
            indexes.Clear();
        }


        private void UpdateJointHisto()
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count != 2)
            {
                UpdateJointHisto(null, null);
                return;
            }

            UpdateJointHisto((Image3D)(this.listViewHeap.Items[indexes[0]].Tag), (Image3D)(this.listViewHeap.Items[indexes[1]].Tag));
            Invalidate();
        }

        private void radioButtonImage0Channel1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateJointHisto();
        }

        private void radioButtonImage0Channel2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateJointHisto();
        }

        private void radioButtonImage0Channel3_CheckedChanged(object sender, EventArgs e)
        {
            UpdateJointHisto();
        }

        private void radioButtonImage1Channel1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateJointHisto();
        }

        private void radioButtonImage1Channel2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateJointHisto();
        }

        private void radioButtonImage1Channel3_CheckedChanged(object sender, EventArgs e)
        {
            UpdateJointHisto();
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ImHistoConj == null) return;

            Sequence NewSeq = new Sequence();
            NewSeq.Add(ImHistoConj);
            IMGlobal.AddSequence(NewSeq, "Conjoint Histogram");
        }

        private void lnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            //Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
            //    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
            //    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
            //    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);


            int indexesCount = indexes.Count;
            for (int i = 0; i < indexesCount; i++)
            {
                Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Width,
                                ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Height,
                                ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Depth,
                                ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).NumBands);

                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));
                Image3D TmpIm2 = new Image3D(TmpIm.Width, TmpIm.Height, TmpIm.Depth, TmpIm.NumBands);

                //for(int Idx=0;Idx<TmpIm.im
                for (int Channel = 0; Channel < TmpIm.NumBands; Channel++)
                {
                    new IM.Library.Mathematics.MathTools().CutLow(TmpIm.Data[Channel], TmpIm2.Data[Channel], 1.0f, 1.0f);
                }

                new IM.Library.Mathematics.MathTools().Log(ref Result, TmpIm2);
                AddImageToHeap(Result, 0);
            }

            indexes.Clear();
        }

        private void anscombeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            //Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
            //    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
            //    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
            //    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);


            int indexesCount = indexes.Count;
            for (int i = 0; i < indexesCount; i++)
            {
                int NumChannel = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).NumBands;
                Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Width,
                                ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Height,
                                ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Depth,
                                NumChannel);


                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));

                for (int Band = 0; Band < NumChannel; Band++)
                    new IM.Library.Mathematics.MathTools().AnscombeTransform(TmpIm.Data[Band], ref Result.Data[Band]);

                AddImageToHeap(Result, 0);
            }

            indexes.Clear();

        }

        private void sightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            request RQuest = new request();
            if (RQuest.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count != 1) return;

            // first of all, copy the current sequence
            Image3D SightIm = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                            ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                            ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                            ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);

            //int Channel = 0;
            PointF StartPt = new PointF();
            PointF EndPt = new PointF();

            for (int TmpChannel = 0; TmpChannel < SightIm.NumBands; TmpChannel++)
            {
                for (int j = 0; j < SightIm.Height; j += (int)RQuest.numericUpDownGridSizeValue)
                {
                    StartPt.X = 0.0f;
                    StartPt.Y = j;
                    EndPt.X = SightIm.Width;
                    EndPt.Y = j;
                    new IM.Library.Tools.Drawing().Draw_Line2D(SightIm, TmpChannel, StartPt, EndPt, 255.0f);
                }
                for (int i = 0; i < SightIm.Width; i += (int)RQuest.numericUpDownGridSizeValue)
                {
                    StartPt.X = i;
                    StartPt.Y = 0.0f;
                    EndPt.X = i;
                    EndPt.Y = SightIm.Height;
                    new IM.Library.Tools.Drawing().Draw_Line2D(SightIm, TmpChannel, StartPt, EndPt, 255.0f);
                }


            }

            //UnitImage.Name = "Unit Image";
            AddImageToHeap(SightIm, 0);
            indexes.Clear();

        }

        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;
            if (indexes.Count == 1) return;
            if (indexes.Count == 2) return;

            if (CheckDimensions() == false)
            {
                MessageBox.Show("Image dimensions do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckChannelCompatibility() == false)
            {
                MessageBox.Show("Number of channel does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);

            //for (int channel = 0; channel < Result.NumBands; channel++)
            //    Array.Copy(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Data[channel],
            //        Result.Data[channel],
            //        Result.ImageSize);

            int indexesCount = indexes.Count;
            float[] Line = new float[indexesCount];
            for (int channel = 0; channel < Result.NumBands; channel++)
            {

                for (int Idx = 0; Idx < Result.ImageSize; Idx++)
                {
                    for (int i = 0; i < indexesCount; i++)
                    {
                        Line[i] = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Data[channel][Idx];
                    }
                    Result.Data[channel][Idx] = new IM.Library.Mathematics.MathTools().Median(Line, false);
                }
            }
            //Result.Name = "Addition result";

            AddImageToHeap(Result, 0);
            indexes.Clear();
        }

        private void isoLevelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count != 1)
            {
                MessageBox.Show("This operation requires 1 image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Image3D TmpIm = (Image3D)(this.listViewHeap.Items[indexes[0]].Tag);

            if (TmpIm.NumBands != 1)
            {
                MessageBox.Show("This operation requires a single channel image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int indexesCount = indexes.Count;

            request RQuest = new request();
            Control[] ButtonControl = RQuest.Controls.Find("buttonCreateGrid", false);
            ButtonControl[0].Text = "Create Iso-Map";

            Control[] ButtonControl1 = RQuest.Controls.Find("label1", false);
            ButtonControl1[0].Text = "Step";




            if (RQuest.ShowDialog() != DialogResult.OK)
            {
                return;
            }



            Image3D IsoMap = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                                            ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                                            1,
                                            2);



            Array.Copy(TmpIm.Data[0], IsoMap.Data[0], IsoMap.ImageSize);

            new IM.Library.Tools.Drawing().IsoMap2D((Image3D)(this.listViewHeap.Items[indexes[0]].Tag), 0,
                                                    IsoMap, 1, (float)RQuest.numericUpDownGridSizeValue);

            Sequence NewSeq = new Sequence();
            NewSeq.Add(IsoMap);
            IMGlobal.AddSequence(NewSeq, "Iso Map");
        }

        private void createReportToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;

            int indexesCount = indexes.Count;

            if (indexesCount == 0) return;

            request RQuest = new request();
            Control[] ButtonControl = RQuest.Controls.Find("buttonCreateGrid", false);
            ButtonControl[0].Text = "Create Report";

            Control[] ButtonControl1 = RQuest.Controls.Find("label1", false);
            ButtonControl1[0].Text = "Zoom (%)";

            ButtonControl = RQuest.Controls.Find("numericUpDownGridSize", false);
            NumericUpDown TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Maximum = 100;
            TmpNum.Value = 100;

            ButtonControl = RQuest.Controls.Find("numericUpDownImPerLine", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Visible = true;
            TmpNum.Maximum = TmpNum.Value = indexesCount;

            ButtonControl = RQuest.Controls.Find("label2", false);
            System.Windows.Forms.Label Tmplabel = (System.Windows.Forms.Label)ButtonControl[0];
            Tmplabel.Visible = true;


            if (RQuest.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string sTime = DateTime.Now.ToString("yyyy-MM-dd mm-ss");
            ReportWriter rw = new ReportWriter(sTime);

            rw.create("HeapReport" + sTime + ".xml");
            rw.setInfo("Image Heap Report", "Report" + sTime, "", (int)TmpNum.Value, (int)RQuest.numericUpDownGridSizeValue);


            for (int i = 0; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));

                rw.addThumbnailImage(TmpIm, "Index" + i, "Image " + i, UpdateInfoPicture(TmpIm));
            }

            rw.close();
            rw.openReport();

        }

        private void meanValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            //Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
            //    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
            //    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
            //    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);


            int indexesCount = indexes.Count;
            string sMeanValues;
            float CurrentMean;
            Console.WriteLine("Mean values:");
            for (int i = 0; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));

                int NumChannel = TmpIm.NumBands;
                sMeanValues = "";
                for (int Band = 0; Band < NumChannel; Band++)
                {
                    CurrentMean = new IM.Library.Mathematics.MathTools().Mean(TmpIm.Data[Band]);
                    sMeanValues += CurrentMean.ToString();
                    sMeanValues += " ";
                }
                Console.WriteLine(sMeanValues);

            }

        }

        private void maxValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            //Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
            //    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
            //    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
            //    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);


            int indexesCount = indexes.Count;
            string sMeanValues;
            float CurrentMean;
            Console.WriteLine("Max values:");
            for (int i = 0; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));

                int NumChannel = TmpIm.NumBands;
                sMeanValues = "";
                for (int Band = 0; Band < NumChannel; Band++)
                {
                    CurrentMean = new IM.Library.Mathematics.MathTools().Max(TmpIm.Data[Band]);
                    sMeanValues += CurrentMean.ToString();
                    sMeanValues += " ";
                }
                Console.WriteLine(sMeanValues);

            }
        }


        #region Combine Images
        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count < 2) return;

            if (CheckHeight() == false)
            {
                MessageBox.Show("Image heights do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckNumBands() == false)
            {
                MessageBox.Show("Image channel number does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int SizeX = 0;

            for (int T = 0; T < indexes.Count; T++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[T]].Tag));
                SizeX += TmpIm.Width;
            }


            Image3D Result = new Image3D(SizeX,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);


            int StartPos = 0;
            for (int T = 0; T < indexes.Count; T++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[T]].Tag));

                for (int Band = 0; Band < TmpIm.NumBands; Band++)
                {
                    for (int Z = 0; Z < Result.Depth; Z++)
                        for (int j = 0; j < TmpIm.Height; j++)
                        {
                            for (int i = 0; i < TmpIm.Width; i++)
                            {
                                Result.Data[Band][i + StartPos + SizeX * j + Z * Result.ScanSliceSize] = TmpIm.Data[Band][i + j * TmpIm.Width + Z * TmpIm.ScanSliceSize];
                            }
                        }
                }

                StartPos += TmpIm.Width;
            }



            AddImageToHeap(Result, 0);
            indexes.Clear();
        }
        #endregion


        private float[] ComputePseudoMonteCarloStatioUnderPoissonNoiseByEmpiricalModeDecomp(Image3D Input, out float Mean, out float Std, int IterNum)
        {

            float[] Table = new float[IterNum];

            Random r = new Random();
            for (int Iter = 0; Iter < IterNum; Iter++)
            {
                float[] TmpTable = new float[Iter + 1];
                for (int RandIdx = 0; RandIdx < Iter + 1; RandIdx++)
                {
                    TmpTable[RandIdx] = Input.Data[0][(int)r.Next(0, Input.ImageSize)];
                }

                Table[Iter] = new IM.Library.Mathematics.MathTools().Mean(TmpTable);
            }

            Mean = new IM.Library.Mathematics.MathTools().Mean(Table);
            Std = new IM.Library.Mathematics.MathTools().Std(Table);
            return Table;

        }

        private float[] ComputePseudoMonteCarloStatioUnderPoissonNoiseByEmpiricalModeDecomp(Image3D Input, out float Mean, out float Std, int StartIter, int EndIter)
        {

            float[] Table = new float[EndIter - StartIter + 1];

            Random r = new Random();
            for (int Iter = StartIter; Iter <= EndIter; Iter++)
            {
                float[] TmpTable = new float[Iter];
                for (int RandIdx = 0; RandIdx < Iter; RandIdx++)
                {
                    TmpTable[RandIdx] = Input.Data[0][(int)r.Next(0, Input.ImageSize)];
                }

                Table[Iter - StartIter] = new IM.Library.Mathematics.MathTools().Mean(TmpTable);
            }

            Mean = new IM.Library.Mathematics.MathTools().Mean(Table);
            Std = new IM.Library.Mathematics.MathTools().Std(Table);
            return Table;

        }

        private void stationirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            float Mean, Std;
            int indexesCount = indexes.Count;
            int NumIter = 100;

            for (int i = 0; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));

                int StartIter = 30;
                int EndIter = TmpIm.ImageSize / 100;


                Image3D ResImage = new Image3D(EndIter - StartIter + 1, 1, 1, 1);

                //  ResImage.Data[0] = ComputePseudoMonteCarloStatioUnderPoissonNoiseByEmpiricalModeDecomp(TmpIm,out Mean, out Std, NumIter);
                ResImage.Data[0] = ComputePseudoMonteCarloStatioUnderPoissonNoiseByEmpiricalModeDecomp(TmpIm, out Mean, out Std, StartIter, EndIter);
                new IM.Library.IO.DataToTextWriter().WriteInColumn(ResImage.Data[0], "D:\\resStation" + i, true);
                //Sequence TmpSeq = new Sequence();
                //TmpSeq.Add(ResImage);
                //IMGlobal.AddSequence(TmpSeq);



            }
        }

        private void gradSmoothnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            float Smoothness;
            int indexesCount = indexes.Count;

            for (int i = 0; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));
                Image3D GradImRho = new Image3D(TmpIm.Width, TmpIm.Height, 1, 1);
                Image3D GradImTeta = new Image3D(TmpIm.Width, TmpIm.Height, 1, 1);




                new IM.Library.Mathematics.Gradient().ComputeGradient2D(TmpIm, 0, GradImRho, GradImTeta);

                new IM.Library.Mathematics.MathTools().Abs(GradImRho, GradImRho);

                //AddImageToHeap(GradImRho, 0);
                //indexes.Clear();

                Smoothness = 100.0f * (new IM.Library.Mathematics.MathTools().Sum(GradImRho.Data[0]) / new IM.Library.Mathematics.MathTools().Sum(TmpIm.Data[0]));

                Console.WriteLine(/*"Image " + i + " : " + */Smoothness);
                //new IM.Library.IO.DataToTextWriter().WriteInColumn(ResImage.Data[0], "D:\\resStation" + i, true);
                //Sequence TmpSeq = new Sequence();
                //TmpSeq.Add(ResImage);
                //IMGlobal.AddSequence(TmpSeq);



            }
        }

        private float[] ComputeSimpleStationarity(Image3D Input, out float Std, int NumIter, int NumPoint)
        {

            float[] Table = new float[NumIter];
            float[] TmpTable = new float[NumPoint];

            Random r = new Random();
            for (int Iter = 0; Iter < NumIter; Iter++)
            {

                for (int RandIdx = 0; RandIdx < NumPoint; RandIdx++)
                {
                    TmpTable[RandIdx] = Input.Data[0][(int)r.Next(0, Input.ImageSize)];
                }

                Table[Iter] = new IM.Library.Mathematics.MathTools().Mean(TmpTable);
            }

            Std = new IM.Library.Mathematics.MathTools().Std(Table);
            return Table;
        }

        private void simpleStationarityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            float Std;
            int indexesCount = indexes.Count;
            int NumIter = 100;
            int NumPoint = 100000;

            for (int i = 0; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));


                new IM.Library.IO.DataToTextWriter().WriteInColumn(ComputeSimpleStationarity(TmpIm, out Std, NumIter, NumPoint), "D:\\resSimpleStation" + i, true);

                Console.WriteLine(Std);


            }
        }

        private void lastStationarityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            bool IsDisplayLevels = true;


            float Mean, Std;
            int indexesCount = indexes.Count;
            int NumIter = 4;

            int WindowsWidth;
            int WindowsHeight;
            float factor = 2;


            float[] ResTotal0 = new float[indexesCount];
            float[] ResTotal1 = new float[indexesCount];
            for (int i = 0; i < indexesCount; i++)
            {
                ResTotal0[i] = ResTotal1[i] = 0.0f;

                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));
                for (int Iter = 1; Iter <= NumIter; Iter++)
                {


                    factor = (float)Math.Pow(2, (double)Iter);
                    WindowsWidth = (int)(TmpIm.Width / factor);
                    WindowsHeight = (int)(TmpIm.Height / factor);

                    float[] ROI = new float[WindowsWidth * WindowsHeight];
                    float[] StdTable = new float[(int)factor * (int)factor];
                    float[] MeanTable = new float[(int)factor * (int)factor];

                    int PosROI = 0;
                    for (int StartWindowsY = 0; StartWindowsY < factor; StartWindowsY++)
                    {
                        int StartPosY = StartWindowsY * WindowsHeight;

                        for (int StartWindowsX = 0; StartWindowsX < factor; StartWindowsX++)
                        {
                            int IdxRoi = 0;
                            int StartPosX = StartWindowsX * WindowsWidth;
                            for (int RealY = StartPosY; RealY < StartPosY + WindowsHeight; RealY++)
                            {
                                for (int RealX = StartPosX; RealX < StartPosX + WindowsWidth; RealX++)
                                {
                                    ROI[IdxRoi++] = TmpIm.Data[0][RealX + RealY * TmpIm.Width];
                                }
                            }

                            //Console.WriteLine("Region ("+StartWindowsX+";"+StartWindowsY+")="+(new IM.Library.Mathematics.MathTools().Mean(ROI)).ToString());
                            StdTable[PosROI] = new IM.Library.Mathematics.MathTools().Std(ROI);
                            MeanTable[PosROI++] = new IM.Library.Mathematics.MathTools().Mean(ROI);


                        }

                    }
                    float cv0;
                    float GlobalMean0 = new IM.Library.Mathematics.MathTools().Mean(MeanTable);
                    float GlobalStd0 = new IM.Library.Mathematics.MathTools().Std(MeanTable);

                    float cv1;
                    float GlobalMean1 = new IM.Library.Mathematics.MathTools().Mean(StdTable);
                    float GlobalStd1 = new IM.Library.Mathematics.MathTools().Std(StdTable);

                    cv0 = GlobalStd0 / GlobalMean0;
                    cv1 = GlobalStd1 / GlobalMean1;
                    if (IsDisplayLevels)
                    {
                        Console.WriteLine("Level " + Iter + ", cv_mean = " + cv0);
                        Console.WriteLine("Level " + Iter + ", cv_std = " + cv1);

                    }
                    ResTotal0[i] += (cv0 / (float)Iter);
                    ResTotal1[i] += (cv1 / (float)Iter);

                    //Image3D ResImage = new Image3D(TmpIm.Width,TmpIm.Height,1,2);

                    //new IM.Library.IO.DataToTextWriter().WriteInColumn(ResImage.Data[0], "D:\\resStation" + i, true);
                    //Sequence TmpSeq = new Sequence();
                    //TmpSeq.Add(ResImage);
                    //IMGlobal.AddSequence(TmpSeq);
                }
                ResTotal0[i] *= 100.0f;
                ResTotal1[i] *= 100.0f;


            }



            for (int i = 0; i < indexesCount; i++)
                Console.WriteLine(/*"Stationarity " + NumIter + " levels (mean) = " + */ResTotal0[i]);

            Console.WriteLine(" ");

            for (int i = 0; i < indexesCount; i++)
                Console.WriteLine(/*"Stationarity " + NumIter + " levels (std) = " + */ResTotal1[i]);

        }

        private void gaussianAndDivideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //RequestInfo
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count > 1)
            {
                MessageBox.Show("Too many images selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RequestInfo RQuest = new RequestInfo();




            if (RQuest.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            float StartSigma = RQuest.numericUpDownStartSigmaValue;
            float EndSigma = RQuest.numericUpDownEndSigmaValue;
            float StepSigma = RQuest.numericUpDownSigmaStepValue;
            bool IsDisplayLevels = RQuest.checkBoxDisplayLevelsBool;
            Image3D CurrentIM = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag));

            Sequence ResSeq = new Sequence();

            Console.WriteLine("Kernel Std");

            for (float CurrentSigma = StartSigma; CurrentSigma <= EndSigma; CurrentSigma += StepSigma)
            {
                Image3D BlurredIM = new Image3D(CurrentIM.Width, CurrentIM.Height, CurrentIM.Depth, CurrentIM.NumBands);
                Image3D ResultIM = new Image3D(CurrentIM.Width, CurrentIM.Height, CurrentIM.Depth, CurrentIM.NumBands);

                for (int Channel = 0; Channel < CurrentIM.NumBands; Channel++)
                {
                    new Convolution().ConvolveFast(CurrentIM, Channel, BlurredIM, Channel, new IM.Library.Filtering.KernelMaker().MakeGaussianKernel(CurrentSigma),
                        new IM.Library.Filtering.KernelMaker().MakeGaussianKernel(CurrentSigma), IM.Library.BoundaryConditions.Mirror);
                    new IM.Library.Mathematics.MathTools().Divide(ref ResultIM.Data[Channel], CurrentIM.Data[Channel], BlurredIM.Data[Channel]);
                }

                Console.WriteLine(CurrentSigma);

                // new IM.Library.Mathematics.MathTools().Multiply(ref ResultIM, ResultIM, 100.0f);
                AddImageToHeap(ResultIM, 0);
                indexes.Clear();

                ResSeq.Add(BlurredIM);
            }

            IMGlobal.AddSequence(ResSeq, "Blurred Images :(" + StartSigma + "..." + EndSigma + ")");


        }

        private void medianAndDivideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //RequestInfo
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count > 1)
            {
                MessageBox.Show("Too many images selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RequestInfo RQuest = new RequestInfo();

            if (RQuest.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            int StartSigma = (int)RQuest.numericUpDownStartSigmaValue;
            int EndSigma = (int)RQuest.numericUpDownEndSigmaValue;
            float StepSigma = RQuest.numericUpDownSigmaStepValue;
            bool IsDisplayLevels = RQuest.checkBoxDisplayLevelsBool;
            Image3D CurrentIM = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag));

            Sequence ResSeq = new Sequence();
            int CurrentKernelSize;
            int HistoSize = 256;

            Console.WriteLine("Kernel Size");

            for (float CurrentSigma = StartSigma; CurrentSigma <= EndSigma; CurrentSigma += StepSigma)
            {
                CurrentKernelSize = (int)CurrentSigma;

                Image3D BlurredIM = new Image3D(CurrentIM.Width, CurrentIM.Height, CurrentIM.Depth, CurrentIM.NumBands);
                Image3D ResultIM = new Image3D(CurrentIM.Width, CurrentIM.Height, CurrentIM.Depth, CurrentIM.NumBands);

                for (int Channel = 0; Channel < CurrentIM.NumBands; Channel++)
                {
                    new IM.Library.Filtering.Median().Median2D_Huang(CurrentIM, Channel, BlurredIM, Channel, CurrentKernelSize, HistoSize, true);
                    new IM.Library.Mathematics.MathTools().Divide(ref ResultIM.Data[Channel], CurrentIM.Data[Channel], BlurredIM.Data[Channel]);
                }

                Console.WriteLine(CurrentKernelSize);
                //new IM.Library.Mathematics.MathTools().Multiply(ref ResultIM, ResultIM, 100.0f);

                AddImageToHeap(ResultIM, 0);
                indexes.Clear();
                ResSeq.Add(BlurredIM);
            }

            IMGlobal.AddSequence(ResSeq, "Denoised Images :(" + StartSigma + "..." + EndSigma + ")");


        }

        #region Drag and Drop

        private void listViewHeap_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            Image3D CurrentIM = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag));
            if (CurrentIM == null) return;
            listViewHeap.DoDragDrop(CurrentIM, DragDropEffects.All);
            return;
        }

        private void listViewHeap_DragDrop(object sender, DragEventArgs e)
        {
            Image3D Im = (Image3D)e.Data.GetData(typeof(Image3D));
            Point initPos = listViewHeap.PointToClient(new Point(e.X, e.Y));
            ListViewItem Item = listViewHeap.FindNearestItem(SearchDirectionHint.Up, initPos);
            if (Item == null) return;
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;

            if (e.Effect == DragDropEffects.Copy)
            {
                for (int idx = 0; idx < indexes.Count; idx++)
                {
                    Image3D CurrentIM = ((Image3D)(this.listViewHeap.Items[indexes[indexes.Count - idx - 1]].Tag));
                    InsertImageToHeap(CurrentIM, 0, Item.Index + 1);
                }
            }
            else if (e.Effect == DragDropEffects.Move)
            {
                //string[] KeyList = new string[indexes.Count];
                //for (int idx = 0; idx < indexes.Count; idx++)
                //{
                //    Image3D CurrentIM = ((Image3D)(this.listViewHeap.Items[indexes[indexes.Count - idx - 1]].Tag));
                //    KeyList[idx] = this.listViewHeap.Items[indexes[indexes.Count - idx - 1]].ImageKey;
                //    InsertImageToHeap(CurrentIM, 0, Item.Index + 1);
                //}
                //RemoveImagesFromList(KeyList);
                //int NewIdx;
                //for (int idx = 0; idx < indexes.Count; idx++)
                //{
                //    NewIdx = Item.Index + 1 + idx;
                //    ListViewItem item = listViewHeap.Items[indexes[idx]];
                //    listViewHeap.Items.
                //    this.listViewHeap.Items[indexes[indexes.Count - idx - 1]]
                //}

                listViewHeap.BeginUpdate();
                ListView.SelectedIndexCollection col = listViewHeap.SelectedIndices;
                for (int i = listViewHeap.Items.Count - 1; i >= 0; i--)
                {
                    if ((col.Contains(i)) && (i >= 0))
                    {
                        Image3D CurrentIM = ((Image3D)(this.listViewHeap.Items[i].Tag));
                        listViewHeap.Items.RemoveAt(i);
                        InsertImageToHeap(CurrentIM, 0, Item.Index + 1);
                        //this.listViewHeap.Items.Insert(i + NewIdx, o);
                    }
                }
                listViewHeap.EndUpdate();
            }

            listViewHeap.Sort();
            // UpdateInfoPicture();

        }

        private void RemoveImagesFromList(String[] KeyList)
        {
            int[] Idx = new int[KeyList.Length];
            for (int i = 0; i < KeyList.Length; i++)
            {
                for (int Pos = 0; Pos < listViewHeap.Items.Count; Pos++)
                {
                    //this.listViewHeap.Items.RemoveByKey(KeyList[i]);
                    if (KeyList[i] == listViewHeap.Items[Pos].ImageKey)
                    {
                        Idx[i] = Pos;
                        listViewHeap.Items.RemoveAt(Pos);
                        //UpdateInfoPicture();
                    }
                    //Idx[i]=
                    //Idx = listViewHeap.Items.IndexOfKey();
                    //listViewHeap.Items.RemoveAt(Idx);
                }
            }
            //for (int i = 0; i < Idx.Length; i++)
            //    listViewHeap.Items.RemoveAt(Idx[i]);
        }

        private void listViewHeap_DragOver(object sender, DragEventArgs e)
        {
            Point initPos = listViewHeap.PointToClient(new Point(e.X, e.Y));
            int MiddlePos = (listViewHeap.Right - listViewHeap.Left) / 2;
            ListViewItem Item = listViewHeap.GetItemAt(MiddlePos, initPos.Y);
            if (Item == null)
            {
                if ((e.KeyState & 8) == 8)
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.Move;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        #endregion

        private void sMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            float Smoothness;
            int indexesCount = indexes.Count;

            Image3D CurrentIM = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag));

            for (int i = 0; i < indexesCount; i++)
            {
                Image3D ResIm = new Image3D(CurrentIM.Width, CurrentIM.Height, CurrentIM.Depth, CurrentIM.NumBands);

                for (int Channel = 0; Channel < CurrentIM.NumBands; Channel++)
                {
                    new IM.Library.Filtering.Focus().SML_3D(CurrentIM, Channel, ResIm, Channel, 3, true);
                }


                for (int Channel = 0; Channel < CurrentIM.NumBands; Channel++)
                {
                    float ResSML = 0.0f;
                    for (int Pix = 0; Pix < ResIm.ImageSize; Pix++)
                    {
                        ResSML += ResIm.Data[Channel][Pix];

                    }
                    Console.WriteLine("SML band " + Channel + " = " + ResSML);
                }
            }
        }

        private void displayGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (displayGridToolStripMenuItem.Checked == false)
            {
                chartLineProfile.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                chartLineProfile.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            }
            else
            {
                chartLineProfile.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                chartLineProfile.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            }
        }




        private void normalizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            //Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
            //    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
            //    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
            //    ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);


            int indexesCount = indexes.Count;
            for (int i = 0; i < indexesCount; i++)
            {
                Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Width,
                                ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Height,
                                ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).Depth,
                                ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag)).NumBands);

                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));

                //for(int Idx=0;Idx<TmpIm.im
                for (int Channel = 0; Channel < TmpIm.NumBands; Channel++)
                {
                    new IM.Library.Mathematics.MathTools().MeanCenteringStdStandarization(TmpIm, Channel, Result, Channel);
                    // new IM.Library.Mathematics.MathTools().CutLow(], 1.0f, 1.0f);
                }

                //new IM.Library.Mathematics.MathTools().Log(ref Result, TmpIm2);
                AddImageToHeap(Result, 0);
            }

            indexes.Clear();
        }

        private void gaussianNoiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RequestForNoise RQuest = new RequestForNoise();
            RQuest.Text = "Gaussian Noise";
            if (RQuest.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);


            int indexesCount = indexes.Count;
            for (int i = 0; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));

                for (int Band = 0; Band < TmpIm.NumBands; Band++)
                {
                    Array.Copy(TmpIm.Data[Band], Result.Data[Band], TmpIm.ImageSize);

                }

                //    numericUpDownGaussMeanValue
                IM.Library.Tools.GaussianNoiseGenerator GaussNoise = new IM.Library.Tools.GaussianNoiseGenerator((float)RQuest.numericUpDownGaussianNoiseMean.Value, (float)RQuest.numericUpDownGaussianNoiseStdv.Value);
                GaussNoise.Disturb(Result);

                //Result.Name = "Sqrt result";
                AddImageToHeap(Result, 0);
            }

            indexes.Clear();


            //Sequence NewSeq = new Sequence();

            //for (int T = 0; T < IMGlobal.CurrentSequence.SequenceSize; T++)
            //{
            //    Image3D CurrentIm = IMGlobal.CurrentSequence[T];
            //    Image3D ResultIm = new Image3D(CurrentIm.Width, CurrentIm.Height, CurrentIm.Depth, CurrentIm.NumBands);

            //    for (int Channel = 0; Channel < CurrentIm.NumBands; Channel++)
            //    {
            //        Array.Copy(CurrentIm.Data[Channel], ResultIm.Data[Channel], ResultIm.ImageSize);
            //    }

            //    IM.Library.Tools.GaussianNoiseGenerator GaussNoise = new IM.Library.Tools.GaussianNoiseGenerator(numericUpDownGaussMeanValue, numericUpDownGaussStdValue);
            //    GaussNoise.Disturb(ResultIm);
            //    NewSeq.Add(ResultIm);
            //}
            //IMGlobal.AddSequence(NewSeq, "Gaussian Noise (" + numericUpDownGaussMeanValue + ";" + numericUpDownGaussStdValue + ")");
        }

        private void uniformNoiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RequestForNoise RQuest = new RequestForNoise();
            RQuest.Text = "Uniform Noise";
            RQuest.label1.Text = "Min.";
            RQuest.label2.Text = "Max.";
            if (RQuest.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);


            Random r = new Random();
            float RandVal = 0.0f;
            float Range = (float)RQuest.numericUpDownGaussianNoiseStdv.Value - (float)RQuest.numericUpDownGaussianNoiseMean.Value;


            int indexesCount = indexes.Count;
            for (int i = 0; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));

                for (int Band = 0; Band < TmpIm.NumBands; Band++)
                {

                    Array.Copy(TmpIm.Data[Band], Result.Data[Band], TmpIm.ImageSize);
                    for (int idx = 0; idx < Result.ImageSize; idx++)
                    {
                        RandVal = Range * (float)r.NextDouble() + (float)RQuest.numericUpDownGaussianNoiseMean.Value;
                        Result.Data[Band][idx] += RandVal;
                    }

                }
                //Result.Name = "Sqrt result";
                AddImageToHeap(Result, 0);
            }

            indexes.Clear();
        }

        private void poissonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);


            int indexesCount = indexes.Count;
            for (int i = 0; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));

                for (int Band = 0; Band < TmpIm.NumBands; Band++)
                {
                    Array.Copy(TmpIm.Data[Band], Result.Data[Band], TmpIm.ImageSize);

                }

                //    numericUpDownGaussMeanValue
                IM.Library.Tools.PoissonNoiseGenerator PoissonNoise = new IM.Library.Tools.PoissonNoiseGenerator(12);
                PoissonNoise.Disturb(Result);

                //Result.Name = "Sqrt result";
                AddImageToHeap(Result, 0);
            }

            indexes.Clear();
        }

        private void vGradientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count > 1)
            {
                MessageBox.Show("Too many images selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int DimX = 24;
            int DimY = 16;
            int DimZ = 1;
            int Band = 1;
            float Value = 1.0f;

            if (indexes.Count == 1)
            {
                DimX = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width;
                DimY = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height;
                DimZ = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth;
                Band = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands;

            }
            RequestImage RQuest = new RequestImage();
            //RQuest.ShowDialog();
            Control[] ButtonControl = RQuest.Controls.Find("numericUpDownDimX", false);
            NumericUpDown TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = DimX;

            ButtonControl = RQuest.Controls.Find("numericUpDownDimY", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = DimY;

            ButtonControl = RQuest.Controls.Find("numericUpDownDimZ", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = DimZ;

            ButtonControl = RQuest.Controls.Find("numericUpDownBand", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = Band;

            ButtonControl = RQuest.Controls.Find("numericUpDownIntensity", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = (decimal)Value;


            if (RQuest.ShowDialog() != DialogResult.OK)
            {
                return;
            }


            Image3D UnitImage = new Image3D(DimX, DimY, DimZ, 1);

            for (int j = 0; j < UnitImage.Height; j++)
            {
                for (int i = 0; i < UnitImage.Width; i++)
                {
                    UnitImage.Data[0][i + j * UnitImage.Width] = j;
                }
            }
            //UnitImage.Name = "Unit Image";
            AddImageToHeap(UnitImage, 0);

            indexes.Clear();
        }

        private void hGardientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count > 1)
            {
                MessageBox.Show("Too many images selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int DimX = 24;
            int DimY = 16;
            int DimZ = 1;
            int Band = 1;
            float Value = 1.0f;

            if (indexes.Count == 1)
            {
                DimX = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width;
                DimY = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height;
                DimZ = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth;
                Band = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands;

            }
            RequestImage RQuest = new RequestImage();
            //RQuest.ShowDialog();
            Control[] ButtonControl = RQuest.Controls.Find("numericUpDownDimX", false);
            NumericUpDown TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = DimX;

            ButtonControl = RQuest.Controls.Find("numericUpDownDimY", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = DimY;

            ButtonControl = RQuest.Controls.Find("numericUpDownDimZ", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = DimZ;

            ButtonControl = RQuest.Controls.Find("numericUpDownBand", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = Band;

            ButtonControl = RQuest.Controls.Find("numericUpDownIntensity", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = (decimal)Value;


            if (RQuest.ShowDialog() != DialogResult.OK)
            {
                return;
            }


            Image3D UnitImage = new Image3D(DimX, DimY, DimZ, 1);

            for (int j = 0; j < UnitImage.Height; j++)
            {
                for (int i = 0; i < UnitImage.Width; i++)
                {
                    UnitImage.Data[0][i + j * UnitImage.Width] = i;
                }
            }
            //UnitImage.Name = "Unit Image";
            AddImageToHeap(UnitImage, 0);

            indexes.Clear();
        }

        private void centralGradientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count > 1)
            {
                MessageBox.Show("Too many images selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int DimX = 24;
            int DimY = 16;
            int DimZ = 1;
            int Band = 1;
            float Value = 1.0f;

            if (indexes.Count == 1)
            {
                DimX = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width;
                DimY = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height;
                DimZ = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth;
                Band = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands;

            }
            RequestImage RQuest = new RequestImage();
            //RQuest.ShowDialog();
            Control[] ButtonControl = RQuest.Controls.Find("numericUpDownDimX", false);
            NumericUpDown TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = DimX;

            ButtonControl = RQuest.Controls.Find("numericUpDownDimY", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = DimY;

            ButtonControl = RQuest.Controls.Find("numericUpDownDimZ", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = DimZ;

            ButtonControl = RQuest.Controls.Find("numericUpDownBand", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = Band;

            ButtonControl = RQuest.Controls.Find("numericUpDownIntensity", false);
            TmpNum = (NumericUpDown)ButtonControl[0];
            TmpNum.Value = (decimal)Value;


            if (RQuest.ShowDialog() != DialogResult.OK)
            {
                return;
            }


            Image3D UnitImage = new Image3D(DimX, DimY, DimZ, 1);

            for (int j = 0; j < UnitImage.Height; j++)
            {
                for (int i = 0; i < UnitImage.Width; i++)
                {
                    UnitImage.Data[0][i + j * UnitImage.Width] = Value * (float)Math.Sqrt((i - DimX / 2.0f) * (i - DimX / 2.0f) + (j - DimY / 2.0f) * (j - DimY / 2.0f));
                }
            }
            //UnitImage.Name = "Unit Image";
            AddImageToHeap(UnitImage, 0);

            indexes.Clear();
        }

        private void numericUpDownZRes_ValueChanged(object sender, EventArgs e)
        {
            UpdateImageResolution();
        }

        private void numericUpDownYRes_ValueChanged(object sender, EventArgs e)
        {
            UpdateImageResolution();
        }

        private void numericUpDownXRes_ValueChanged(object sender, EventArgs e)
        {
            UpdateImageResolution();
        }


        private void UpdateImageResolution()
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0) return;

            Image3D Result = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth,
                ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);

            int indexesCount = indexes.Count;
            for (int i = 0; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));
                TmpIm.XResolution = (double)numericUpDownXRes.Value;
                TmpIm.YResolution = (double)numericUpDownYRes.Value;
                TmpIm.ZResolution = (double)numericUpDownZRes.Value;

            }

            indexes.Clear();


        }

        private void saveHeapPicturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count == 0)
            {
                MessageBox.Show("Select at least a picture", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            FolderBrowserDialog OpenFolderDialog = new FolderBrowserDialog();

            if (OpenFolderDialog.ShowDialog() != DialogResult.OK) return;
            string Path = OpenFolderDialog.SelectedPath;

            if (Path == "") return;
            int indexesCount = indexes.Count;
            for (int i = 0; i < indexesCount; i++)
            {
                Image3D TmpIm = ((Image3D)(this.listViewHeap.Items[indexes[i]].Tag));

                Sequence Seq = new Sequence();
                Seq.Add(TmpIm);

                byte[][] LutWork = LutMaker.CreateLutNx16(3);
                for (int Band = 0; Band < 3; Band++)
                    LutMaker.LinearScale(LutWork[Band], 0, 700.0f, 0, 700.0f, false);

                //8->3x8
                byte[][][] LutView = LutMaker.CreateLutNx3x8(3);


                LutMaker.SetLutColor(LutView[0], Color.Red);
                LutMaker.SetLutColor(LutView[1], Color.Yellow);
                LutMaker.SetLutColor(LutView[2], Color.LimeGreen);

                new IM.IO.FileWriter().WriteSequenceView(Seq, Path + "\\Image_" + i + ".jpg", IM.IO.ImageFormat.jpg, 0, 0, LutWork, LutView, false);
            }
            System.Diagnostics.Process.Start(Path);

        }

        private void sphereToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ListView.SelectedIndexCollection indexes = this.listViewHeap.SelectedIndices;
            if (indexes.Count > 1)
            {
                MessageBox.Show("Too many images selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }




            FormForSphere WindowForSphere = new FormForSphere();
            WindowForSphere.numericUpDownPosX.Value = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width / 2;
            WindowForSphere.numericUpDownPosY.Value = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height / 2;
            WindowForSphere.numericUpDownPosZ.Value = ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth / 2;





            if (WindowForSphere.ShowDialog() != DialogResult.OK)
            {
                return;
            }


            double CenterX = (double)WindowForSphere.numericUpDownPosX.Value;
            double CenterY = (double)WindowForSphere.numericUpDownPosY.Value;
            double CenterZ = (double)WindowForSphere.numericUpDownPosZ.Value;
            double DistMax = (double)WindowForSphere.numericUpDownRadius.Value;
            float NewIntensity = (float)WindowForSphere.numericUpDownIntensity.Value;

            Image3D UnitImage = new Image3D(((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Width, ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Height, ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).Depth, ((Image3D)(this.listViewHeap.Items[indexes[0]].Tag)).NumBands);


            Image3D TmpIm = (Image3D)(this.listViewHeap.Items[indexes[0]].Tag);

            for (int Band = 0; Band < TmpIm.NumBands; Band++)
            {
                Array.Copy(TmpIm.Data[Band], UnitImage.Data[Band], UnitImage.ImageSize);


                for (int z = 0; z < UnitImage.Depth; z++)
                    for (int y = 0; y < UnitImage.Height; y++)
                        for (int x = 0; x < UnitImage.Width; x++)
                        {
                            double Dist = Math.Sqrt((z - CenterZ) * (z - CenterZ) + (y - CenterY) * (y - CenterY) + (x - CenterX) * (x - CenterX));
                            if (Dist < DistMax)
                                UnitImage.Data[Band][x + y * UnitImage.Width + z * UnitImage.ScanSliceSize] += NewIntensity;
                        }



            }

            AddImageToHeap(UnitImage, 0);

            indexes.Clear();

        }























    }

    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewItemSorter : IComparer
    {
        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        private SortOrder OrderOfSort;

        /// <summary>
        /// Class constructor.  Initializes various elements
        /// </summary>
        public ListViewItemSorter()
        {
            // Initialize the sort order to 'none'
            OrderOfSort = SortOrder.Ascending;
        }

        /// <summary>
        /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            compareResult = listviewX.Index - listviewY.Index;
            // Compare the two items
            //compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }



    }

}

