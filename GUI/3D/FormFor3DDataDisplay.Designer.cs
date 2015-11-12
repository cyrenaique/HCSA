namespace HCSAnalyzer.Forms
{
    partial class FormFor3DDataDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFor3DDataDisplay));
            this.renderWindowControl1 = new Kitware.VTK.RenderWindowControl();
            this.comboBoxDescriptorX = new System.Windows.Forms.ComboBox();
            this.comboBoxDescriptorY = new System.Windows.Forms.ComboBox();
            this.comboBoxDescriptorZ = new System.Windows.Forms.ComboBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedDescriptorsAsActiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.panelForClasses = new System.Windows.Forms.Panel();
            this.checkBoxForDisplayAxesInformation = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // renderWindowControl1
            // 
            this.renderWindowControl1.AddTestActors = false;
            this.renderWindowControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.renderWindowControl1.Location = new System.Drawing.Point(3, 3);
            this.renderWindowControl1.Name = "renderWindowControl1";
            this.renderWindowControl1.Size = new System.Drawing.Size(690, 566);
            this.renderWindowControl1.TabIndex = 0;
            this.renderWindowControl1.TestText = null;
            this.renderWindowControl1.Load += new System.EventHandler(this.renderWindowControl1_Load);
            // 
            // comboBoxDescriptorX
            // 
            this.comboBoxDescriptorX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDescriptorX.FormattingEnabled = true;
            this.comboBoxDescriptorX.Location = new System.Drawing.Point(23, 34);
            this.comboBoxDescriptorX.Name = "comboBoxDescriptorX";
            this.comboBoxDescriptorX.Size = new System.Drawing.Size(193, 21);
            this.comboBoxDescriptorX.TabIndex = 14;
            this.comboBoxDescriptorX.SelectedIndexChanged += new System.EventHandler(this.comboBoxDescriptorX_SelectedIndexChanged);
            // 
            // comboBoxDescriptorY
            // 
            this.comboBoxDescriptorY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDescriptorY.FormattingEnabled = true;
            this.comboBoxDescriptorY.Location = new System.Drawing.Point(23, 65);
            this.comboBoxDescriptorY.Name = "comboBoxDescriptorY";
            this.comboBoxDescriptorY.Size = new System.Drawing.Size(193, 21);
            this.comboBoxDescriptorY.TabIndex = 15;
            this.comboBoxDescriptorY.SelectedIndexChanged += new System.EventHandler(this.comboBoxDescriptorY_SelectedIndexChanged);
            // 
            // comboBoxDescriptorZ
            // 
            this.comboBoxDescriptorZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDescriptorZ.FormattingEnabled = true;
            this.comboBoxDescriptorZ.Location = new System.Drawing.Point(23, 96);
            this.comboBoxDescriptorZ.Name = "comboBoxDescriptorZ";
            this.comboBoxDescriptorZ.Size = new System.Drawing.Size(193, 21);
            this.comboBoxDescriptorZ.TabIndex = 16;
            this.comboBoxDescriptorZ.SelectedIndexChanged += new System.EventHandler(this.comboBoxDescriptorZ_SelectedIndexChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(929, 24);
            this.menuStrip.TabIndex = 17;
            this.menuStrip.Text = "menuStrip1";
            // 
            // displayToolStripMenuItem
            // 
            this.displayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.refreshToolStripMenuItem,
            this.axisToolStripMenuItem});
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            this.displayToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.displayToolStripMenuItem.Text = "Display";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(132, 22);
            this.toolStripMenuItem1.Text = "Options";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // axisToolStripMenuItem
            // 
            this.axisToolStripMenuItem.CheckOnClick = true;
            this.axisToolStripMenuItem.Name = "axisToolStripMenuItem";
            this.axisToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.axisToolStripMenuItem.Text = "Axis";
            this.axisToolStripMenuItem.Click += new System.EventHandler(this.axisToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectedDescriptorsAsActiveToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // selectedDescriptorsAsActiveToolStripMenuItem
            // 
            this.selectedDescriptorsAsActiveToolStripMenuItem.Name = "selectedDescriptorsAsActiveToolStripMenuItem";
            this.selectedDescriptorsAsActiveToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.selectedDescriptorsAsActiveToolStripMenuItem.Text = "Selected Descriptors as Active";
            this.selectedDescriptorsAsActiveToolStripMenuItem.Click += new System.EventHandler(this.selectedDescriptorsAsActiveToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Z";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Y";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.Location = new System.Drawing.Point(141, 3);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonRefresh.TabIndex = 19;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // panelForClasses
            // 
            this.panelForClasses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panelForClasses.Location = new System.Drawing.Point(6, 155);
            this.panelForClasses.Name = "panelForClasses";
            this.panelForClasses.Size = new System.Drawing.Size(140, 414);
            this.panelForClasses.TabIndex = 20;
            // 
            // checkBoxForDisplayAxesInformation
            // 
            this.checkBoxForDisplayAxesInformation.AutoSize = true;
            this.checkBoxForDisplayAxesInformation.Checked = true;
            this.checkBoxForDisplayAxesInformation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxForDisplayAxesInformation.Location = new System.Drawing.Point(6, 132);
            this.checkBoxForDisplayAxesInformation.Name = "checkBoxForDisplayAxesInformation";
            this.checkBoxForDisplayAxesInformation.Size = new System.Drawing.Size(104, 17);
            this.checkBoxForDisplayAxesInformation.TabIndex = 21;
            this.checkBoxForDisplayAxesInformation.Text = "Axes Information";
            this.checkBoxForDisplayAxesInformation.UseVisualStyleBackColor = true;
            this.checkBoxForDisplayAxesInformation.CheckedChanged += new System.EventHandler(this.checkBoxForDisplayAxesInformation_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.renderWindowControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.buttonRefresh);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxForDisplayAxesInformation);
            this.splitContainer1.Panel2.Controls.Add(this.comboBoxDescriptorX);
            this.splitContainer1.Panel2.Controls.Add(this.panelForClasses);
            this.splitContainer1.Panel2.Controls.Add(this.comboBoxDescriptorY);
            this.splitContainer1.Panel2.Controls.Add(this.comboBoxDescriptorZ);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Size = new System.Drawing.Size(923, 574);
            this.splitContainer1.SplitterDistance = 698;
            this.splitContainer1.TabIndex = 22;
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Controls.Add(this.splitContainer1);
            this.MainPanel.Location = new System.Drawing.Point(0, 27);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(929, 580);
            this.MainPanel.TabIndex = 23;
            // 
            // FormFor3DDataDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 607);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormFor3DDataDisplay";
            this.Text = "3D Data Visualization";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox comboBoxDescriptorX;
        public System.Windows.Forms.ComboBox comboBoxDescriptorY;
        public System.Windows.Forms.ComboBox comboBoxDescriptorZ;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem displayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem axisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedDescriptorsAsActiveToolStripMenuItem;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Panel panelForClasses;
        private System.Windows.Forms.CheckBox checkBoxForDisplayAxesInformation;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public Kitware.VTK.RenderWindowControl renderWindowControl1;
        public System.Windows.Forms.Panel MainPanel;
    }
}