namespace HCSAnalyzer.Forms.FormsForOptions
{
    partial class FormForGlobalInfoOptions
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Plates and Wells");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Cellular Phenotypes");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Well Classes");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Colors", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Display", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("3D View");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("3D", new System.Windows.Forms.TreeNode[] {
            treeNode6});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForGlobalInfoOptions));
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.treeViewForOptions = new System.Windows.Forms.TreeView();
            this.contextMenuStripForOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelForDisplay = new System.Windows.Forms.Panel();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.contextMenuStripForOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(7, 351);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 0;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(426, 351);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // treeViewForOptions
            // 
            this.treeViewForOptions.ContextMenuStrip = this.contextMenuStripForOptions;
            this.treeViewForOptions.Location = new System.Drawing.Point(7, 7);
            this.treeViewForOptions.Name = "treeViewForOptions";
            treeNode1.Name = "Node0";
            treeNode1.Tag = "Plates and Wells";
            treeNode1.Text = "Plates and Wells";
            treeNode2.Name = "Node1";
            treeNode2.Tag = "Cellular Phenotypes";
            treeNode2.Text = "Cellular Phenotypes";
            treeNode3.Name = "Node2";
            treeNode3.Tag = "Well Classes";
            treeNode3.Text = "Well Classes";
            treeNode4.Name = "Node0";
            treeNode4.Text = "Colors";
            treeNode5.Name = "Node0";
            treeNode5.Text = "Display";
            treeNode6.Name = "NodeFor3DView";
            treeNode6.Tag = "3D";
            treeNode6.Text = "3D View";
            treeNode7.Name = "Node3D";
            treeNode7.Text = "3D";
            this.treeViewForOptions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode7});
            this.treeViewForOptions.Size = new System.Drawing.Size(202, 338);
            this.treeViewForOptions.TabIndex = 2;
            this.treeViewForOptions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewForOptions_AfterSelect);
            // 
            // contextMenuStripForOptions
            // 
            this.contextMenuStripForOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.toolStripSeparator1,
            this.collapseAllToolStripMenuItem,
            this.expandAllToolStripMenuItem});
            this.contextMenuStripForOptions.Name = "contextMenuStripForOptions";
            this.contextMenuStripForOptions.Size = new System.Drawing.Size(137, 98);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.collapseAllToolStripMenuItem.Text = "Collapse All";
            this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
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
            this.panelForDisplay.Location = new System.Drawing.Point(215, 7);
            this.panelForDisplay.Name = "panelForDisplay";
            this.panelForDisplay.Size = new System.Drawing.Size(286, 338);
            this.panelForDisplay.TabIndex = 3;
            // 
            // FormForGlobalInfoOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 377);
            this.Controls.Add(this.panelForDisplay);
            this.Controls.Add(this.treeViewForOptions);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonApply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForGlobalInfoOptions";
            this.Text = "Options";
            this.contextMenuStripForOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Panel panelForDisplay;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForOptions;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.TreeView treeViewForOptions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
    }
}