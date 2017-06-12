namespace HCSAnalyzer
{
    partial class FormForImportExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForImportExcel));
            this.buttonOk = new System.Windows.Forms.Button();
            this.dataGridViewForImport = new System.Windows.Forms.DataGridView();
            this.contextMenuStripDataGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDownRows = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownColumns = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxPlateDim = new System.Windows.Forms.GroupBox();
            this.contextMenuStripForPlateDim = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem96 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem384 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1536 = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxConvertNaNTo0 = new System.Windows.Forms.CheckBox();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewForImport)).BeginInit();
            this.contextMenuStripDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumns)).BeginInit();
            this.groupBoxPlateDim.SuspendLayout();
            this.contextMenuStripForPlateDim.SuspendLayout();
            this.groupBoxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(438, 546);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(126, 27);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // dataGridViewForImport
            // 
            this.dataGridViewForImport.AllowUserToAddRows = false;
            this.dataGridViewForImport.AllowUserToDeleteRows = false;
            this.dataGridViewForImport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewForImport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewForImport.ContextMenuStrip = this.contextMenuStripDataGrid;
            this.dataGridViewForImport.Location = new System.Drawing.Point(8, 93);
            this.dataGridViewForImport.Name = "dataGridViewForImport";
            this.dataGridViewForImport.Size = new System.Drawing.Size(556, 447);
            this.dataGridViewForImport.TabIndex = 3;
            // 
            // contextMenuStripDataGrid
            // 
            this.contextMenuStripDataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.unselectAllToolStripMenuItem});
            this.contextMenuStripDataGrid.Name = "contextMenuStripDataGrid";
            this.contextMenuStripDataGrid.Size = new System.Drawing.Size(137, 48);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // unselectAllToolStripMenuItem
            // 
            this.unselectAllToolStripMenuItem.Name = "unselectAllToolStripMenuItem";
            this.unselectAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.unselectAllToolStripMenuItem.Text = "Unselect All";
            this.unselectAllToolStripMenuItem.Click += new System.EventHandler(this.unselectAllToolStripMenuItem_Click);
            // 
            // numericUpDownRows
            // 
            this.numericUpDownRows.Location = new System.Drawing.Point(70, 51);
            this.numericUpDownRows.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericUpDownRows.Name = "numericUpDownRows";
            this.numericUpDownRows.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownRows.TabIndex = 2;
            this.numericUpDownRows.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // numericUpDownColumns
            // 
            this.numericUpDownColumns.Location = new System.Drawing.Point(70, 21);
            this.numericUpDownColumns.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownColumns.Name = "numericUpDownColumns";
            this.numericUpDownColumns.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownColumns.TabIndex = 1;
            this.numericUpDownColumns.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Rows";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Columns";
            // 
            // groupBoxPlateDim
            // 
            this.groupBoxPlateDim.ContextMenuStrip = this.contextMenuStripForPlateDim;
            this.groupBoxPlateDim.Controls.Add(this.numericUpDownColumns);
            this.groupBoxPlateDim.Controls.Add(this.numericUpDownRows);
            this.groupBoxPlateDim.Controls.Add(this.label1);
            this.groupBoxPlateDim.Controls.Add(this.label2);
            this.groupBoxPlateDim.Location = new System.Drawing.Point(8, 3);
            this.groupBoxPlateDim.Name = "groupBoxPlateDim";
            this.groupBoxPlateDim.Size = new System.Drawing.Size(168, 84);
            this.groupBoxPlateDim.TabIndex = 38;
            this.groupBoxPlateDim.TabStop = false;
            this.groupBoxPlateDim.Text = "Plate Dimensions";
            // 
            // contextMenuStripForPlateDim
            // 
            this.contextMenuStripForPlateDim.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem96,
            this.toolStripMenuItem384,
            this.toolStripMenuItem1536});
            this.contextMenuStripForPlateDim.Name = "contextMenuStripForPlateDim";
            this.contextMenuStripForPlateDim.Size = new System.Drawing.Size(99, 70);
            // 
            // toolStripMenuItem96
            // 
            this.toolStripMenuItem96.Name = "toolStripMenuItem96";
            this.toolStripMenuItem96.Size = new System.Drawing.Size(98, 22);
            this.toolStripMenuItem96.Text = "96";
            this.toolStripMenuItem96.Click += new System.EventHandler(this.toolStripMenuItem96_Click);
            // 
            // toolStripMenuItem384
            // 
            this.toolStripMenuItem384.Name = "toolStripMenuItem384";
            this.toolStripMenuItem384.Size = new System.Drawing.Size(98, 22);
            this.toolStripMenuItem384.Text = "384";
            this.toolStripMenuItem384.Click += new System.EventHandler(this.toolStripMenuItem384_Click);
            // 
            // toolStripMenuItem1536
            // 
            this.toolStripMenuItem1536.Name = "toolStripMenuItem1536";
            this.toolStripMenuItem1536.Size = new System.Drawing.Size(98, 22);
            this.toolStripMenuItem1536.Text = "1536";
            this.toolStripMenuItem1536.Click += new System.EventHandler(this.toolStripMenuItem1536_Click);
            // 
            // checkBoxConvertNaNTo0
            // 
            this.checkBoxConvertNaNTo0.AutoSize = true;
            this.checkBoxConvertNaNTo0.Checked = true;
            this.checkBoxConvertNaNTo0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxConvertNaNTo0.Location = new System.Drawing.Point(12, 20);
            this.checkBoxConvertNaNTo0.Name = "checkBoxConvertNaNTo0";
            this.checkBoxConvertNaNTo0.Size = new System.Drawing.Size(163, 17);
            this.checkBoxConvertNaNTo0.TabIndex = 40;
            this.checkBoxConvertNaNTo0.Text = "Convert undefined value to 0";
            this.checkBoxConvertNaNTo0.UseVisualStyleBackColor = true;
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.checkBoxConvertNaNTo0);
            this.groupBoxOptions.Location = new System.Drawing.Point(182, 3);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(185, 84);
            this.groupBoxOptions.TabIndex = 41;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Options";
            // 
            // FormForImportExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 578);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.groupBoxPlateDim);
            this.Controls.Add(this.dataGridViewForImport);
            this.Controls.Add(this.buttonOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(529, 198);
            this.Name = "FormForImportExcel";
            this.Text = "Import";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewForImport)).EndInit();
            this.contextMenuStripDataGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumns)).EndInit();
            this.groupBoxPlateDim.ResumeLayout(false);
            this.groupBoxPlateDim.PerformLayout();
            this.contextMenuStripForPlateDim.ResumeLayout(false);
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.DataGridView dataGridViewForImport;
        public System.Windows.Forms.NumericUpDown numericUpDownRows;
        public System.Windows.Forms.NumericUpDown numericUpDownColumns;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxPlateDim;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDataGrid;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unselectAllToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForPlateDim;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem96;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem384;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1536;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        public System.Windows.Forms.CheckBox checkBoxConvertNaNTo0;
    }
}