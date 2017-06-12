namespace HCSAnalyzer.Simulator.Forms.Panels
{
    partial class PanelForParamCellTypes
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel = new System.Windows.Forms.Panel();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewCellTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCurrentTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.panelForColor = new System.Windows.Forms.Panel();
            this.comboBoxCellTypes = new System.Windows.Forms.ComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveCellTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCellTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.ContextMenuStrip = this.contextMenuStrip;
            this.panel.Controls.Add(this.richTextBox);
            this.panel.Controls.Add(this.panelForColor);
            this.panel.Controls.Add(this.comboBoxCellTypes);
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(233, 188);
            this.panel.TabIndex = 0;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewCellTypeToolStripMenuItem,
            this.editCurrentTypeToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveCellTypeToolStripMenuItem,
            this.loadCellTypeToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(167, 120);
            // 
            // addNewCellTypeToolStripMenuItem
            // 
            this.addNewCellTypeToolStripMenuItem.Name = "addNewCellTypeToolStripMenuItem";
            this.addNewCellTypeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.addNewCellTypeToolStripMenuItem.Text = "New Cell Type";
            this.addNewCellTypeToolStripMenuItem.Click += new System.EventHandler(this.addNewCellTypeToolStripMenuItem_Click);
            // 
            // editCurrentTypeToolStripMenuItem
            // 
            this.editCurrentTypeToolStripMenuItem.Name = "editCurrentTypeToolStripMenuItem";
            this.editCurrentTypeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.editCurrentTypeToolStripMenuItem.Text = "Edit Current Type";
            this.editCurrentTypeToolStripMenuItem.Click += new System.EventHandler(this.editCurrentTypeToolStripMenuItem_Click);
            // 
            // richTextBox
            // 
            this.richTextBox.ContextMenuStrip = this.contextMenuStrip;
            this.richTextBox.Location = new System.Drawing.Point(12, 36);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(205, 144);
            this.richTextBox.TabIndex = 2;
            this.richTextBox.Text = "";
            // 
            // panelForColor
            // 
            this.panelForColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelForColor.ContextMenuStrip = this.contextMenuStrip;
            this.panelForColor.Location = new System.Drawing.Point(139, 6);
            this.panelForColor.Name = "panelForColor";
            this.panelForColor.Size = new System.Drawing.Size(78, 21);
            this.panelForColor.TabIndex = 1;
            this.panelForColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelForColor_MouseClick);
            // 
            // comboBoxCellTypes
            // 
            this.comboBoxCellTypes.ContextMenuStrip = this.contextMenuStrip;
            this.comboBoxCellTypes.FormattingEnabled = true;
            this.comboBoxCellTypes.Location = new System.Drawing.Point(12, 6);
            this.comboBoxCellTypes.Name = "comboBoxCellTypes";
            this.comboBoxCellTypes.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCellTypes.TabIndex = 0;
            this.comboBoxCellTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxCellTypes_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(163, 6);
            // 
            // saveCellTypeToolStripMenuItem
            // 
            this.saveCellTypeToolStripMenuItem.Name = "saveCellTypeToolStripMenuItem";
            this.saveCellTypeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.saveCellTypeToolStripMenuItem.Text = "Save Cell Type";
            this.saveCellTypeToolStripMenuItem.Click += new System.EventHandler(this.saveCellTypeToolStripMenuItem_Click);
            // 
            // loadCellTypeToolStripMenuItem
            // 
            this.loadCellTypeToolStripMenuItem.Name = "loadCellTypeToolStripMenuItem";
            this.loadCellTypeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.loadCellTypeToolStripMenuItem.Text = "Load Cell Type";
            this.loadCellTypeToolStripMenuItem.Click += new System.EventHandler(this.loadCellTypeToolStripMenuItem_Click);
            // 
            // PanelForParamCellTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamCellTypes";
            this.Size = new System.Drawing.Size(241, 194);
            this.panel.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Panel panelForColor;
        private System.Windows.Forms.ComboBox comboBoxCellTypes;
        public System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addNewCellTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCurrentTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveCellTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadCellTypeToolStripMenuItem;
    }
}
