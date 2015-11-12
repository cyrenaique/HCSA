namespace IM3_Plugin3
{
    partial class FormForPreProcessings
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.checkBoxMedianIsDisabled = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.numericUpDownMedianKernel = new System.Windows.Forms.NumericUpDown();
            this.checkBoxBlurBackgroundIsDisable = new System.Windows.Forms.CheckBox();
            this.numericUpDownBlurForBackground = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownShiftZ = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownShiftY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownShiftX = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMedianKernel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlurForBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShiftZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShiftY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShiftX)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(198, 161);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(115, 25);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // checkBoxMedianIsDisabled
            // 
            this.checkBoxMedianIsDisabled.AutoSize = true;
            this.checkBoxMedianIsDisabled.Checked = true;
            this.checkBoxMedianIsDisabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMedianIsDisabled.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxMedianIsDisabled.Location = new System.Drawing.Point(238, 55);
            this.checkBoxMedianIsDisabled.Name = "checkBoxMedianIsDisabled";
            this.checkBoxMedianIsDisabled.Size = new System.Drawing.Size(65, 16);
            this.checkBoxMedianIsDisabled.TabIndex = 50;
            this.checkBoxMedianIsDisabled.Text = "Disable";
            this.checkBoxMedianIsDisabled.UseVisualStyleBackColor = true;
            this.checkBoxMedianIsDisabled.CheckedChanged += new System.EventHandler(this.checkBoxMedianIsDisabled_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.SystemColors.Control;
            this.label14.Location = new System.Drawing.Point(51, 57);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 12);
            this.label14.TabIndex = 48;
            this.label14.Text = "Median Kernel";
            // 
            // numericUpDownMedianKernel
            // 
            this.numericUpDownMedianKernel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownMedianKernel.Enabled = false;
            this.numericUpDownMedianKernel.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownMedianKernel.Location = new System.Drawing.Point(160, 54);
            this.numericUpDownMedianKernel.Maximum = new decimal(new int[] {
            1661992960,
            1808227885,
            5,
            0});
            this.numericUpDownMedianKernel.Name = "numericUpDownMedianKernel";
            this.numericUpDownMedianKernel.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownMedianKernel.TabIndex = 49;
            this.numericUpDownMedianKernel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkBoxBlurBackgroundIsDisable
            // 
            this.checkBoxBlurBackgroundIsDisable.AutoSize = true;
            this.checkBoxBlurBackgroundIsDisable.Checked = true;
            this.checkBoxBlurBackgroundIsDisable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBlurBackgroundIsDisable.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxBlurBackgroundIsDisable.Location = new System.Drawing.Point(238, 27);
            this.checkBoxBlurBackgroundIsDisable.Name = "checkBoxBlurBackgroundIsDisable";
            this.checkBoxBlurBackgroundIsDisable.Size = new System.Drawing.Size(65, 16);
            this.checkBoxBlurBackgroundIsDisable.TabIndex = 46;
            this.checkBoxBlurBackgroundIsDisable.Text = "Disable";
            this.checkBoxBlurBackgroundIsDisable.UseVisualStyleBackColor = true;
            this.checkBoxBlurBackgroundIsDisable.CheckedChanged += new System.EventHandler(this.checkBoxBlurBackgroundIsDisable_CheckedChanged);
            // 
            // numericUpDownBlurForBackground
            // 
            this.numericUpDownBlurForBackground.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownBlurForBackground.Enabled = false;
            this.numericUpDownBlurForBackground.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownBlurForBackground.Location = new System.Drawing.Point(160, 25);
            this.numericUpDownBlurForBackground.Maximum = new decimal(new int[] {
            1661992960,
            1808227885,
            5,
            0});
            this.numericUpDownBlurForBackground.Name = "numericUpDownBlurForBackground";
            this.numericUpDownBlurForBackground.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownBlurForBackground.TabIndex = 47;
            this.numericUpDownBlurForBackground.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(25, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 12);
            this.label4.TabIndex = 45;
            this.label4.Text = "Background Removal";
            // 
            // numericUpDownShiftZ
            // 
            this.numericUpDownShiftZ.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownShiftZ.DecimalPlaces = 2;
            this.numericUpDownShiftZ.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownShiftZ.Location = new System.Drawing.Point(238, 114);
            this.numericUpDownShiftZ.Name = "numericUpDownShiftZ";
            this.numericUpDownShiftZ.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownShiftZ.TabIndex = 54;
            // 
            // numericUpDownShiftY
            // 
            this.numericUpDownShiftY.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownShiftY.DecimalPlaces = 2;
            this.numericUpDownShiftY.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownShiftY.Location = new System.Drawing.Point(146, 114);
            this.numericUpDownShiftY.Name = "numericUpDownShiftY";
            this.numericUpDownShiftY.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownShiftY.TabIndex = 53;
            // 
            // numericUpDownShiftX
            // 
            this.numericUpDownShiftX.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownShiftX.DecimalPlaces = 2;
            this.numericUpDownShiftX.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownShiftX.Location = new System.Drawing.Point(61, 114);
            this.numericUpDownShiftX.Name = "numericUpDownShiftX";
            this.numericUpDownShiftX.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownShiftX.TabIndex = 52;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.Control;
            this.label10.Location = new System.Drawing.Point(25, 116);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 12);
            this.label10.TabIndex = 51;
            this.label10.Text = "Shift";
            // 
            // FormForPreProcessings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(325, 198);
            this.Controls.Add(this.numericUpDownShiftZ);
            this.Controls.Add(this.numericUpDownShiftY);
            this.Controls.Add(this.numericUpDownShiftX);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.checkBoxMedianIsDisabled);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.numericUpDownMedianKernel);
            this.Controls.Add(this.checkBoxBlurBackgroundIsDisable);
            this.Controls.Add(this.numericUpDownBlurForBackground);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonOk);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FormForPreProcessings";
            this.Text = "Pre-processing";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMedianKernel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlurForBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShiftZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShiftY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShiftX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.CheckBox checkBoxMedianIsDisabled;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.NumericUpDown numericUpDownMedianKernel;
        public System.Windows.Forms.CheckBox checkBoxBlurBackgroundIsDisable;
        public System.Windows.Forms.NumericUpDown numericUpDownBlurForBackground;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown numericUpDownShiftZ;
        public System.Windows.Forms.NumericUpDown numericUpDownShiftY;
        public System.Windows.Forms.NumericUpDown numericUpDownShiftX;
        private System.Windows.Forms.Label label10;
    }
}