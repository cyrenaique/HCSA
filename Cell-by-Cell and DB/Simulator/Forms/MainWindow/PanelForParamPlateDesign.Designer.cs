namespace HCSAnalyzer.Simulator.Forms.Panels
{
    partial class PanelForParamPlateDesign
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
            this.checkBoxExportToDB = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownNumCols = new System.Windows.Forms.NumericUpDown();
            this.groupBoxPlateDesign = new System.Windows.Forms.GroupBox();
            this.numericUpDownNumRows = new System.Windows.Forms.NumericUpDown();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumCols)).BeginInit();
            this.groupBoxPlateDesign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumRows)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.groupBoxPlateDesign);
            this.panel.Controls.Add(this.checkBoxExportToDB);
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(187, 232);
            this.panel.TabIndex = 1;
            // 
            // checkBoxExportToDB
            // 
            this.checkBoxExportToDB.AutoSize = true;
            this.checkBoxExportToDB.Location = new System.Drawing.Point(38, 21);
            this.checkBoxExportToDB.Name = "checkBoxExportToDB";
            this.checkBoxExportToDB.Size = new System.Drawing.Size(117, 17);
            this.checkBoxExportToDB.TabIndex = 12;
            this.checkBoxExportToDB.Text = "Export to Database";
            this.checkBoxExportToDB.UseVisualStyleBackColor = true;
            this.checkBoxExportToDB.CheckedChanged += new System.EventHandler(this.checkBoxExportToDB_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Columns";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Rows";
            // 
            // numericUpDownNumCols
            // 
            this.numericUpDownNumCols.Location = new System.Drawing.Point(73, 29);
            this.numericUpDownNumCols.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownNumCols.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNumCols.Name = "numericUpDownNumCols";
            this.numericUpDownNumCols.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownNumCols.TabIndex = 15;
            this.numericUpDownNumCols.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBoxPlateDesign
            // 
            this.groupBoxPlateDesign.Controls.Add(this.numericUpDownNumRows);
            this.groupBoxPlateDesign.Controls.Add(this.numericUpDownNumCols);
            this.groupBoxPlateDesign.Controls.Add(this.label1);
            this.groupBoxPlateDesign.Controls.Add(this.label2);
            this.groupBoxPlateDesign.Enabled = false;
            this.groupBoxPlateDesign.Location = new System.Drawing.Point(4, 56);
            this.groupBoxPlateDesign.Name = "groupBoxPlateDesign";
            this.groupBoxPlateDesign.Size = new System.Drawing.Size(179, 95);
            this.groupBoxPlateDesign.TabIndex = 16;
            this.groupBoxPlateDesign.TabStop = false;
            this.groupBoxPlateDesign.Text = "Plate Design";
            // 
            // numericUpDownNumRows
            // 
            this.numericUpDownNumRows.Location = new System.Drawing.Point(73, 60);
            this.numericUpDownNumRows.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownNumRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNumRows.Name = "numericUpDownNumRows";
            this.numericUpDownNumRows.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownNumRows.TabIndex = 18;
            this.numericUpDownNumRows.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // PanelForParamPlateDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamPlateDesign";
            this.Size = new System.Drawing.Size(193, 240);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumCols)).EndInit();
            this.groupBoxPlateDesign.ResumeLayout(false);
            this.groupBoxPlateDesign.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumRows)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox checkBoxExportToDB;
        private System.Windows.Forms.GroupBox groupBoxPlateDesign;
        private System.Windows.Forms.NumericUpDown numericUpDownNumRows;
        private System.Windows.Forms.NumericUpDown numericUpDownNumCols;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
