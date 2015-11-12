namespace HCSAnalyzer.Forms
{
    partial class FormForPlateDimensions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForPlateDimensions));
            this.buttonOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownColumns = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRows = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHistoSize = new System.Windows.Forms.NumericUpDown();
            this.labelHisto = new System.Windows.Forms.Label();
            this.checkBoxIsOmitFirstColumn = new System.Windows.Forms.CheckBox();
            this.checkBoxAddCellNumber = new System.Windows.Forms.CheckBox();
            this.radioButtonDataHDDB = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonDataMemory = new System.Windows.Forms.RadioButton();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem96 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem384 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1536 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHistoSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(39, 268);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(124, 32);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Columns";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Rows";
            // 
            // numericUpDownColumns
            // 
            this.numericUpDownColumns.Location = new System.Drawing.Point(96, 15);
            this.numericUpDownColumns.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownColumns.Minimum = new decimal(new int[] {
            1,
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
            // numericUpDownRows
            // 
            this.numericUpDownRows.Location = new System.Drawing.Point(96, 41);
            this.numericUpDownRows.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericUpDownRows.Minimum = new decimal(new int[] {
            1,
            0,
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
            // numericUpDownHistoSize
            // 
            this.numericUpDownHistoSize.Location = new System.Drawing.Point(96, 86);
            this.numericUpDownHistoSize.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericUpDownHistoSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownHistoSize.Name = "numericUpDownHistoSize";
            this.numericUpDownHistoSize.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownHistoSize.TabIndex = 5;
            this.numericUpDownHistoSize.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numericUpDownHistoSize.Visible = false;
            // 
            // labelHisto
            // 
            this.labelHisto.AutoSize = true;
            this.labelHisto.Location = new System.Drawing.Point(21, 88);
            this.labelHisto.Name = "labelHisto";
            this.labelHisto.Size = new System.Drawing.Size(57, 13);
            this.labelHisto.TabIndex = 4;
            this.labelHisto.Text = "Histo. Size";
            this.labelHisto.Visible = false;
            // 
            // checkBoxIsOmitFirstColumn
            // 
            this.checkBoxIsOmitFirstColumn.AutoSize = true;
            this.checkBoxIsOmitFirstColumn.Checked = true;
            this.checkBoxIsOmitFirstColumn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIsOmitFirstColumn.Location = new System.Drawing.Point(48, 120);
            this.checkBoxIsOmitFirstColumn.Name = "checkBoxIsOmitFirstColumn";
            this.checkBoxIsOmitFirstColumn.Size = new System.Drawing.Size(107, 17);
            this.checkBoxIsOmitFirstColumn.TabIndex = 6;
            this.checkBoxIsOmitFirstColumn.Text = "Omit First Column";
            this.checkBoxIsOmitFirstColumn.UseVisualStyleBackColor = true;
            this.checkBoxIsOmitFirstColumn.Visible = false;
            // 
            // checkBoxAddCellNumber
            // 
            this.checkBoxAddCellNumber.AutoSize = true;
            this.checkBoxAddCellNumber.Checked = true;
            this.checkBoxAddCellNumber.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAddCellNumber.Location = new System.Drawing.Point(45, 146);
            this.checkBoxAddCellNumber.Name = "checkBoxAddCellNumber";
            this.checkBoxAddCellNumber.Size = new System.Drawing.Size(113, 17);
            this.checkBoxAddCellNumber.TabIndex = 7;
            this.checkBoxAddCellNumber.Text = "Add \"Cell number\"";
            this.checkBoxAddCellNumber.UseVisualStyleBackColor = true;
            this.checkBoxAddCellNumber.Visible = false;
            // 
            // radioButtonDataHDDB
            // 
            this.radioButtonDataHDDB.AutoSize = true;
            this.radioButtonDataHDDB.Checked = true;
            this.radioButtonDataHDDB.Location = new System.Drawing.Point(47, 24);
            this.radioButtonDataHDDB.Name = "radioButtonDataHDDB";
            this.radioButtonDataHDDB.Size = new System.Drawing.Size(90, 17);
            this.radioButtonDataHDDB.TabIndex = 8;
            this.radioButtonDataHDDB.TabStop = true;
            this.radioButtonDataHDDB.Text = "HD Database";
            this.radioButtonDataHDDB.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonDataMemory);
            this.groupBox1.Controls.Add(this.radioButtonDataHDDB);
            this.groupBox1.Location = new System.Drawing.Point(9, 178);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(185, 84);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Access";
            // 
            // radioButtonDataMemory
            // 
            this.radioButtonDataMemory.AutoSize = true;
            this.radioButtonDataMemory.Enabled = false;
            this.radioButtonDataMemory.Location = new System.Drawing.Point(61, 47);
            this.radioButtonDataMemory.Name = "radioButtonDataMemory";
            this.radioButtonDataMemory.Size = new System.Drawing.Size(62, 17);
            this.radioButtonDataMemory.TabIndex = 9;
            this.radioButtonDataMemory.Text = "Memory";
            this.radioButtonDataMemory.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem96,
            this.toolStripMenuItem384,
            this.toolStripMenuItem1536});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(153, 92);
            // 
            // toolStripMenuItem96
            // 
            this.toolStripMenuItem96.Name = "toolStripMenuItem96";
            this.toolStripMenuItem96.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem96.Text = "96";
            this.toolStripMenuItem96.Click += new System.EventHandler(this.toolStripMenuItem96_Click);
            // 
            // toolStripMenuItem384
            // 
            this.toolStripMenuItem384.Name = "toolStripMenuItem384";
            this.toolStripMenuItem384.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem384.Text = "384";
            this.toolStripMenuItem384.Click += new System.EventHandler(this.toolStripMenuItem384_Click);
            // 
            // toolStripMenuItem1536
            // 
            this.toolStripMenuItem1536.Name = "toolStripMenuItem1536";
            this.toolStripMenuItem1536.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1536.Text = "1536";
            this.toolStripMenuItem1536.Click += new System.EventHandler(this.toolStripMenuItem1536_Click);
            // 
            // FormForPlateDimensions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 311);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBoxAddCellNumber);
            this.Controls.Add(this.checkBoxIsOmitFirstColumn);
            this.Controls.Add(this.numericUpDownHistoSize);
            this.Controls.Add(this.labelHisto);
            this.Controls.Add(this.numericUpDownRows);
            this.Controls.Add(this.numericUpDownColumns);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormForPlateDimensions";
            this.Text = "Plate Dimensions";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHistoSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDownColumns;
        public System.Windows.Forms.NumericUpDown numericUpDownRows;
        public System.Windows.Forms.NumericUpDown numericUpDownHistoSize;
        public System.Windows.Forms.Label labelHisto;
        public System.Windows.Forms.CheckBox checkBoxIsOmitFirstColumn;
        public System.Windows.Forms.CheckBox checkBoxAddCellNumber;
        public System.Windows.Forms.RadioButton radioButtonDataHDDB;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.RadioButton radioButtonDataMemory;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem96;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem384;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1536;
    }
}