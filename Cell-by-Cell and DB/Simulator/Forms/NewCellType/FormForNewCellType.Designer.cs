namespace HCSAnalyzer.Simulator.Forms.NewCellType
{
    partial class FormForNewCellType
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
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Transition Values");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Cell Cycle");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Velocity");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForNewCellType));
            this.treeViewForOptions = new System.Windows.Forms.TreeView();
            this.panelForDisplay = new System.Windows.Forms.Panel();
            this.buttonOk = new System.Windows.Forms.Button();
            this.contextMenuStripForTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripForTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewForOptions
            // 
            this.treeViewForOptions.Location = new System.Drawing.Point(3, 5);
            this.treeViewForOptions.Name = "treeViewForOptions";
            treeNode5.Name = "Node0";
            treeNode5.Tag = "General";
            treeNode5.Text = "General";
            treeNode6.Name = "Node0";
            treeNode6.Tag = "TransitionMatrix";
            treeNode6.Text = "Transition Values";
            treeNode7.Name = "Node1";
            treeNode7.Tag = "CellCycle";
            treeNode7.Text = "Cell Cycle";
            treeNode8.Name = "Node2";
            treeNode8.Tag = "Velocity";
            treeNode8.Text = "Velocity";
            this.treeViewForOptions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            this.treeViewForOptions.Size = new System.Drawing.Size(188, 276);
            this.treeViewForOptions.TabIndex = 11;
            this.treeViewForOptions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewForOptions_AfterSelect);
            // 
            // panelForDisplay
            // 
            this.panelForDisplay.AutoScroll = true;
            this.panelForDisplay.BackColor = System.Drawing.SystemColors.Control;
            this.panelForDisplay.Location = new System.Drawing.Point(197, 5);
            this.panelForDisplay.Name = "panelForDisplay";
            this.panelForDisplay.Size = new System.Drawing.Size(252, 276);
            this.panelForDisplay.TabIndex = 12;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(351, 286);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(98, 25);
            this.buttonOk.TabIndex = 13;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // contextMenuStripForTree
            // 
            this.contextMenuStripForTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.collapseAllToolStripMenuItem,
            this.expandAllToolStripMenuItem});
            this.contextMenuStripForTree.Name = "contextMenuStripForTree";
            this.contextMenuStripForTree.Size = new System.Drawing.Size(153, 70);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.collapseAllToolStripMenuItem.Text = "Collapse All";
            this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.expandAllToolStripMenuItem.Text = "Expand All";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // FormForNewCellType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 313);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.panelForDisplay);
            this.Controls.Add(this.treeViewForOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForNewCellType";
            this.Text = "New Cell Type";
            this.contextMenuStripForTree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TreeView treeViewForOptions;
        private System.Windows.Forms.Panel panelForDisplay;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForTree;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
    }
}