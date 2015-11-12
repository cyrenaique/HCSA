namespace HCSAnalyzer.Simulator.Forms
{
    partial class FormForVariableDef
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForVariableDef));
            this.buttonOk = new System.Windows.Forms.Button();
            this.radioButtonConstant = new System.Windows.Forms.RadioButton();
            this.radioButtonVariable = new System.Windows.Forms.RadioButton();
            this.groupBoxVariableSpec = new System.Windows.Forms.GroupBox();
            this.numericUpDownIncrement = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxRandom = new System.Windows.Forms.CheckBox();
            this.checkBoxProportionalToRow = new System.Windows.Forms.CheckBox();
            this.checkBoxProportionalToCol = new System.Windows.Forms.CheckBox();
            this.groupBoxVariableSpec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIncrement)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(87, 211);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // radioButtonConstant
            // 
            this.radioButtonConstant.AutoSize = true;
            this.radioButtonConstant.Checked = true;
            this.radioButtonConstant.Location = new System.Drawing.Point(17, 13);
            this.radioButtonConstant.Name = "radioButtonConstant";
            this.radioButtonConstant.Size = new System.Drawing.Size(67, 17);
            this.radioButtonConstant.TabIndex = 1;
            this.radioButtonConstant.TabStop = true;
            this.radioButtonConstant.Text = "Constant";
            this.radioButtonConstant.UseVisualStyleBackColor = true;
            this.radioButtonConstant.CheckedChanged += new System.EventHandler(this.radioButtonConstant_CheckedChanged);
            // 
            // radioButtonVariable
            // 
            this.radioButtonVariable.AutoSize = true;
            this.radioButtonVariable.Location = new System.Drawing.Point(18, 48);
            this.radioButtonVariable.Name = "radioButtonVariable";
            this.radioButtonVariable.Size = new System.Drawing.Size(63, 17);
            this.radioButtonVariable.TabIndex = 2;
            this.radioButtonVariable.Text = "Variable";
            this.radioButtonVariable.UseVisualStyleBackColor = true;
            this.radioButtonVariable.CheckedChanged += new System.EventHandler(this.radioButtonVariable_CheckedChanged);
            // 
            // groupBoxVariableSpec
            // 
            this.groupBoxVariableSpec.Controls.Add(this.numericUpDownIncrement);
            this.groupBoxVariableSpec.Controls.Add(this.label1);
            this.groupBoxVariableSpec.Controls.Add(this.checkBoxRandom);
            this.groupBoxVariableSpec.Controls.Add(this.checkBoxProportionalToRow);
            this.groupBoxVariableSpec.Controls.Add(this.checkBoxProportionalToCol);
            this.groupBoxVariableSpec.Enabled = false;
            this.groupBoxVariableSpec.Location = new System.Drawing.Point(3, 70);
            this.groupBoxVariableSpec.Name = "groupBoxVariableSpec";
            this.groupBoxVariableSpec.Size = new System.Drawing.Size(159, 132);
            this.groupBoxVariableSpec.TabIndex = 3;
            this.groupBoxVariableSpec.TabStop = false;
            this.groupBoxVariableSpec.Text = "Variable Specifications";
            // 
            // numericUpDownIncrement
            // 
            this.numericUpDownIncrement.DecimalPlaces = 2;
            this.numericUpDownIncrement.Location = new System.Drawing.Point(70, 97);
            this.numericUpDownIncrement.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericUpDownIncrement.Minimum = new decimal(new int[] {
            1215752192,
            23,
            0,
            -2147483648});
            this.numericUpDownIncrement.Name = "numericUpDownIncrement";
            this.numericUpDownIncrement.Size = new System.Drawing.Size(67, 20);
            this.numericUpDownIncrement.TabIndex = 4;
            this.numericUpDownIncrement.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Increment";
            // 
            // checkBoxRandom
            // 
            this.checkBoxRandom.AutoSize = true;
            this.checkBoxRandom.Location = new System.Drawing.Point(16, 69);
            this.checkBoxRandom.Name = "checkBoxRandom";
            this.checkBoxRandom.Size = new System.Drawing.Size(66, 17);
            this.checkBoxRandom.TabIndex = 2;
            this.checkBoxRandom.Text = "Random";
            this.checkBoxRandom.UseVisualStyleBackColor = true;
            this.checkBoxRandom.CheckedChanged += new System.EventHandler(this.checkBoxRandom_CheckedChanged);
            // 
            // checkBoxProportionalToRow
            // 
            this.checkBoxProportionalToRow.AutoSize = true;
            this.checkBoxProportionalToRow.Location = new System.Drawing.Point(16, 46);
            this.checkBoxProportionalToRow.Name = "checkBoxProportionalToRow";
            this.checkBoxProportionalToRow.Size = new System.Drawing.Size(83, 17);
            this.checkBoxProportionalToRow.TabIndex = 1;
            this.checkBoxProportionalToRow.Text = "Row related";
            this.checkBoxProportionalToRow.UseVisualStyleBackColor = true;
            // 
            // checkBoxProportionalToCol
            // 
            this.checkBoxProportionalToCol.AutoSize = true;
            this.checkBoxProportionalToCol.Checked = true;
            this.checkBoxProportionalToCol.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProportionalToCol.Location = new System.Drawing.Point(16, 23);
            this.checkBoxProportionalToCol.Name = "checkBoxProportionalToCol";
            this.checkBoxProportionalToCol.Size = new System.Drawing.Size(96, 17);
            this.checkBoxProportionalToCol.TabIndex = 0;
            this.checkBoxProportionalToCol.Text = "Column related";
            this.checkBoxProportionalToCol.UseVisualStyleBackColor = true;
            // 
            // FormForVariableDef
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(167, 243);
            this.Controls.Add(this.groupBoxVariableSpec);
            this.Controls.Add(this.radioButtonVariable);
            this.Controls.Add(this.radioButtonConstant);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForVariableDef";
            this.Text = "Variable";
            this.groupBoxVariableSpec.ResumeLayout(false);
            this.groupBoxVariableSpec.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIncrement)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.RadioButton radioButtonConstant;
        private System.Windows.Forms.RadioButton radioButtonVariable;
        private System.Windows.Forms.GroupBox groupBoxVariableSpec;
        private System.Windows.Forms.CheckBox checkBoxRandom;
        private System.Windows.Forms.CheckBox checkBoxProportionalToRow;
        private System.Windows.Forms.CheckBox checkBoxProportionalToCol;
        private System.Windows.Forms.NumericUpDown numericUpDownIncrement;
        private System.Windows.Forms.Label label1;
    }
}