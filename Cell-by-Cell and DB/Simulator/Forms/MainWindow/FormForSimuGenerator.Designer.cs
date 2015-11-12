namespace HCSAnalyzer.Simulator.Forms
{
    partial class FormForSimuGenerator
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Dimensions");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Cell Types");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("World", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Cell Populations");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Model Initialization", new System.Windows.Forms.TreeNode[] {
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Plate Design");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Export", new System.Windows.Forms.TreeNode[] {
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("3D");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Visualization", new System.Windows.Forms.TreeNode[] {
            treeNode9});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForSimuGenerator));
            this.buttonRun = new System.Windows.Forms.Button();
            this.treeViewForOptions = new System.Windows.Forms.TreeView();
            this.contextMenuStripForTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.collapseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelForDisplay = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.contextMenuStripForVisu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItemDisp3D = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItemDisp2D = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.displayToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cellTypesRelationshipsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.dToolStripMenuItemDisplay2D = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItemDisplay3D = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCellTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.dataTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripForTreeView.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.contextMenuStripForVisu.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(370, 287);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(98, 25);
            this.buttonRun.TabIndex = 0;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // treeViewForOptions
            // 
            this.treeViewForOptions.ContextMenuStrip = this.contextMenuStripForTreeView;
            this.treeViewForOptions.Location = new System.Drawing.Point(7, 31);
            this.treeViewForOptions.Name = "treeViewForOptions";
            treeNode1.Name = "Node0";
            treeNode1.Tag = "General";
            treeNode1.Text = "General";
            treeNode2.Name = "Node1";
            treeNode2.Tag = "WorldDimensions";
            treeNode2.Text = "Dimensions";
            treeNode3.Name = "Node0";
            treeNode3.Tag = "CellTypes";
            treeNode3.Text = "Cell Types";
            treeNode4.Name = "Node0";
            treeNode4.Tag = "";
            treeNode4.Text = "World";
            treeNode5.Name = "Node3";
            treeNode5.Tag = "CellPopulations";
            treeNode5.Text = "Cell Populations";
            treeNode6.Name = "Node2";
            treeNode6.Text = "Model Initialization";
            treeNode7.Name = "Node5";
            treeNode7.Tag = "PlateDesign";
            treeNode7.Text = "Plate Design";
            treeNode8.Name = "Node4";
            treeNode8.Text = "Export";
            treeNode9.Name = "Node1";
            treeNode9.Tag = "3D";
            treeNode9.Text = "3D";
            treeNode10.Name = "Node0";
            treeNode10.Tag = "Visualization";
            treeNode10.Text = "Visualization";
            this.treeViewForOptions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode4,
            treeNode6,
            treeNode8,
            treeNode10});
            this.treeViewForOptions.Size = new System.Drawing.Size(179, 283);
            this.treeViewForOptions.TabIndex = 10;
            this.treeViewForOptions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewForOptions_AfterSelect);
            // 
            // contextMenuStripForTreeView
            // 
            this.contextMenuStripForTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.toolStripSeparator2,
            this.collapseToolStripMenuItem,
            this.expandAllToolStripMenuItem});
            this.contextMenuStripForTreeView.Name = "contextMenuStripForTreeView";
            this.contextMenuStripForTreeView.Size = new System.Drawing.Size(137, 76);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(133, 6);
            // 
            // collapseToolStripMenuItem
            // 
            this.collapseToolStripMenuItem.Name = "collapseToolStripMenuItem";
            this.collapseToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.collapseToolStripMenuItem.Text = "Collapse All";
            this.collapseToolStripMenuItem.Click += new System.EventHandler(this.collapseToolStripMenuItem_Click);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.expandAllToolStripMenuItem.Text = "Expand All";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // panelForDisplay
            // 
            this.panelForDisplay.AutoScroll = true;
            this.panelForDisplay.BackColor = System.Drawing.SystemColors.Control;
            this.panelForDisplay.Location = new System.Drawing.Point(192, 32);
            this.panelForDisplay.Name = "panelForDisplay";
            this.panelForDisplay.Size = new System.Drawing.Size(275, 249);
            this.panelForDisplay.TabIndex = 11;
            // 
            // statusStrip
            // 
            this.statusStrip.ContextMenuStrip = this.contextMenuStripForVisu;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 320);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(473, 22);
            this.statusStrip.Stretch = false;
            this.statusStrip.TabIndex = 12;
            // 
            // contextMenuStripForVisu
            // 
            this.contextMenuStripForVisu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayToolStripMenuItem});
            this.contextMenuStripForVisu.Name = "contextMenuStripForVisu";
            this.contextMenuStripForVisu.Size = new System.Drawing.Size(153, 48);
            // 
            // displayToolStripMenuItem
            // 
            this.displayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dToolStripMenuItemDisp3D,
            this.dToolStripMenuItemDisp2D,
            this.toolStripSeparator3,
            this.dataTableToolStripMenuItem});
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            this.displayToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.displayToolStripMenuItem.Text = "Display";
            // 
            // dToolStripMenuItemDisp3D
            // 
            this.dToolStripMenuItemDisp3D.Name = "dToolStripMenuItemDisp3D";
            this.dToolStripMenuItemDisp3D.Size = new System.Drawing.Size(152, 22);
            this.dToolStripMenuItemDisp3D.Text = "3D";
            this.dToolStripMenuItemDisp3D.Click += new System.EventHandler(this.dToolStripMenuItemDisp3D_Click);
            // 
            // dToolStripMenuItemDisp2D
            // 
            this.dToolStripMenuItemDisp2D.Name = "dToolStripMenuItemDisp2D";
            this.dToolStripMenuItemDisp2D.Size = new System.Drawing.Size(152, 22);
            this.dToolStripMenuItemDisp2D.Text = "2D";
            this.dToolStripMenuItemDisp2D.Click += new System.EventHandler(this.dToolStripMenuItemDisp2D_Click);
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayToolStripMenuItem1,
            this.toolsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(473, 24);
            this.menuStrip.TabIndex = 13;
            this.menuStrip.Text = "menuStrip1";
            // 
            // displayToolStripMenuItem1
            // 
            this.displayToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cellTypesRelationshipsToolStripMenuItem,
            this.toolStripSeparator1,
            this.dToolStripMenuItemDisplay2D,
            this.dToolStripMenuItemDisplay3D});
            this.displayToolStripMenuItem1.Name = "displayToolStripMenuItem1";
            this.displayToolStripMenuItem1.Size = new System.Drawing.Size(57, 20);
            this.displayToolStripMenuItem1.Text = "Display";
            // 
            // cellTypesRelationshipsToolStripMenuItem
            // 
            this.cellTypesRelationshipsToolStripMenuItem.Name = "cellTypesRelationshipsToolStripMenuItem";
            this.cellTypesRelationshipsToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.cellTypesRelationshipsToolStripMenuItem.Text = "Cell Types Relationships";
            this.cellTypesRelationshipsToolStripMenuItem.Click += new System.EventHandler(this.cellTypesRelationshipsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // dToolStripMenuItemDisplay2D
            // 
            this.dToolStripMenuItemDisplay2D.Enabled = false;
            this.dToolStripMenuItemDisplay2D.Name = "dToolStripMenuItemDisplay2D";
            this.dToolStripMenuItemDisplay2D.Size = new System.Drawing.Size(201, 22);
            this.dToolStripMenuItemDisplay2D.Text = "2D";
            this.dToolStripMenuItemDisplay2D.Click += new System.EventHandler(this.dToolStripMenuItemDisplay2D_Click);
            // 
            // dToolStripMenuItemDisplay3D
            // 
            this.dToolStripMenuItemDisplay3D.Name = "dToolStripMenuItemDisplay3D";
            this.dToolStripMenuItemDisplay3D.Size = new System.Drawing.Size(201, 22);
            this.dToolStripMenuItemDisplay3D.Text = "3D";
            this.dToolStripMenuItemDisplay3D.Click += new System.EventHandler(this.dToolStripMenuItemDisplay3D_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCellTypeToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // newCellTypeToolStripMenuItem
            // 
            this.newCellTypeToolStripMenuItem.Name = "newCellTypeToolStripMenuItem";
            this.newCellTypeToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.newCellTypeToolStripMenuItem.Text = "New cell type";
            this.newCellTypeToolStripMenuItem.Click += new System.EventHandler(this.newCellTypeToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // dataTableToolStripMenuItem
            // 
            this.dataTableToolStripMenuItem.Name = "dataTableToolStripMenuItem";
            this.dataTableToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dataTableToolStripMenuItem.Text = "Data Table";
            this.dataTableToolStripMenuItem.Click += new System.EventHandler(this.dataTableToolStripMenuItem_Click);
            // 
            // FormForSimuGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(473, 342);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.panelForDisplay);
            this.Controls.Add(this.treeViewForOptions);
            this.Controls.Add(this.buttonRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "FormForSimuGenerator";
            this.Text = "Cells Simulator";
            this.contextMenuStripForTreeView.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.contextMenuStripForVisu.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRun;
        public System.Windows.Forms.TreeView treeViewForOptions;
        private System.Windows.Forms.Panel panelForDisplay;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForVisu;
        private System.Windows.Forms.ToolStripMenuItem displayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItemDisp3D;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItemDisp2D;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem displayToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItemDisplay3D;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItemDisplay2D;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForTreeView;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newCellTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cellTypesRelationshipsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem collapseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem dataTableToolStripMenuItem;
    }
}