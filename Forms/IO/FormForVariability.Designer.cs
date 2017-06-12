namespace HCSAnalyzer.Forms.IO
{
    partial class FormForVariability
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForVariability));
            this.buttonOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownVariability = new System.Windows.Forms.NumericUpDown();
            this.checkBoxVariableAlongTheColumns = new System.Windows.Forms.CheckBox();
            this.checkBoxVariableAlongTheRows = new System.Windows.Forms.CheckBox();
            this.radioButtonVariableAlongTheColumnsPositive = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonVariableAlongTheRowsPositive = new System.Windows.Forms.RadioButton();
            this.radioButtonVariableAlongTheColumnsNegative = new System.Windows.Forms.RadioButton();
            this.radioButtonVariableAlongTheRowsNegative = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVariability)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(117, 185);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(102, 27);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Standard Deviation";
            // 
            // numericUpDownVariability
            // 
            this.numericUpDownVariability.DecimalPlaces = 2;
            this.numericUpDownVariability.Location = new System.Drawing.Point(165, 17);
            this.numericUpDownVariability.Name = "numericUpDownVariability";
            this.numericUpDownVariability.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownVariability.TabIndex = 2;
            // 
            // checkBoxVariableAlongTheColumns
            // 
            this.checkBoxVariableAlongTheColumns.AutoSize = true;
            this.checkBoxVariableAlongTheColumns.Location = new System.Drawing.Point(26, 24);
            this.checkBoxVariableAlongTheColumns.Name = "checkBoxVariableAlongTheColumns";
            this.checkBoxVariableAlongTheColumns.Size = new System.Drawing.Size(64, 17);
            this.checkBoxVariableAlongTheColumns.TabIndex = 3;
            this.checkBoxVariableAlongTheColumns.Text = "Variable";
            this.checkBoxVariableAlongTheColumns.UseVisualStyleBackColor = true;
            // 
            // checkBoxVariableAlongTheRows
            // 
            this.checkBoxVariableAlongTheRows.AutoSize = true;
            this.checkBoxVariableAlongTheRows.Location = new System.Drawing.Point(26, 22);
            this.checkBoxVariableAlongTheRows.Name = "checkBoxVariableAlongTheRows";
            this.checkBoxVariableAlongTheRows.Size = new System.Drawing.Size(64, 17);
            this.checkBoxVariableAlongTheRows.TabIndex = 4;
            this.checkBoxVariableAlongTheRows.Text = "Variable";
            this.checkBoxVariableAlongTheRows.UseVisualStyleBackColor = true;
            // 
            // radioButtonVariableAlongTheColumnsPositive
            // 
            this.radioButtonVariableAlongTheColumnsPositive.AutoSize = true;
            this.radioButtonVariableAlongTheColumnsPositive.Checked = true;
            this.radioButtonVariableAlongTheColumnsPositive.Location = new System.Drawing.Point(124, 23);
            this.radioButtonVariableAlongTheColumnsPositive.Name = "radioButtonVariableAlongTheColumnsPositive";
            this.radioButtonVariableAlongTheColumnsPositive.Size = new System.Drawing.Size(62, 17);
            this.radioButtonVariableAlongTheColumnsPositive.TabIndex = 5;
            this.radioButtonVariableAlongTheColumnsPositive.Text = "Positive";
            this.radioButtonVariableAlongTheColumnsPositive.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonVariableAlongTheColumnsNegative);
            this.groupBox1.Controls.Add(this.checkBoxVariableAlongTheColumns);
            this.groupBox1.Controls.Add(this.radioButtonVariableAlongTheColumnsPositive);
            this.groupBox1.Location = new System.Drawing.Point(12, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 54);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Columns based";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonVariableAlongTheRowsNegative);
            this.groupBox2.Controls.Add(this.radioButtonVariableAlongTheRowsPositive);
            this.groupBox2.Controls.Add(this.checkBoxVariableAlongTheRows);
            this.groupBox2.Location = new System.Drawing.Point(12, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 54);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rows based";
            // 
            // radioButtonVariableAlongTheRowsPositive
            // 
            this.radioButtonVariableAlongTheRowsPositive.AutoSize = true;
            this.radioButtonVariableAlongTheRowsPositive.Checked = true;
            this.radioButtonVariableAlongTheRowsPositive.Location = new System.Drawing.Point(124, 21);
            this.radioButtonVariableAlongTheRowsPositive.Name = "radioButtonVariableAlongTheRowsPositive";
            this.radioButtonVariableAlongTheRowsPositive.Size = new System.Drawing.Size(62, 17);
            this.radioButtonVariableAlongTheRowsPositive.TabIndex = 6;
            this.radioButtonVariableAlongTheRowsPositive.TabStop = true;
            this.radioButtonVariableAlongTheRowsPositive.Text = "Positive";
            this.radioButtonVariableAlongTheRowsPositive.UseVisualStyleBackColor = true;
            // 
            // radioButtonVariableAlongTheColumnsNegative
            // 
            this.radioButtonVariableAlongTheColumnsNegative.AutoSize = true;
            this.radioButtonVariableAlongTheColumnsNegative.Location = new System.Drawing.Point(220, 23);
            this.radioButtonVariableAlongTheColumnsNegative.Name = "radioButtonVariableAlongTheColumnsNegative";
            this.radioButtonVariableAlongTheColumnsNegative.Size = new System.Drawing.Size(68, 17);
            this.radioButtonVariableAlongTheColumnsNegative.TabIndex = 6;
            this.radioButtonVariableAlongTheColumnsNegative.Text = "Negative";
            this.radioButtonVariableAlongTheColumnsNegative.UseVisualStyleBackColor = true;
            // 
            // radioButtonVariableAlongTheRowsNegative
            // 
            this.radioButtonVariableAlongTheRowsNegative.AutoSize = true;
            this.radioButtonVariableAlongTheRowsNegative.Location = new System.Drawing.Point(220, 21);
            this.radioButtonVariableAlongTheRowsNegative.Name = "radioButtonVariableAlongTheRowsNegative";
            this.radioButtonVariableAlongTheRowsNegative.Size = new System.Drawing.Size(68, 17);
            this.radioButtonVariableAlongTheRowsNegative.TabIndex = 7;
            this.radioButtonVariableAlongTheRowsNegative.Text = "Negative";
            this.radioButtonVariableAlongTheRowsNegative.UseVisualStyleBackColor = true;
            // 
            // FormForVariability
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 228);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.numericUpDownVariability);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormForVariability";
            this.Text = "FormForVariability";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVariability)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDownVariability;
        public System.Windows.Forms.CheckBox checkBoxVariableAlongTheColumns;
        public System.Windows.Forms.CheckBox checkBoxVariableAlongTheRows;
        public System.Windows.Forms.RadioButton radioButtonVariableAlongTheColumnsPositive;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.RadioButton radioButtonVariableAlongTheRowsPositive;
        public System.Windows.Forms.RadioButton radioButtonVariableAlongTheColumnsNegative;
        public System.Windows.Forms.RadioButton radioButtonVariableAlongTheRowsNegative;
    }
}