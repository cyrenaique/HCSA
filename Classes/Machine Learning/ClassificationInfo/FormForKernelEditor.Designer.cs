namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{
    partial class FormForKernelEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForKernelEditor));
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBoxPolyKernel = new System.Windows.Forms.GroupBox();
            this.numericUpDownPolyExponent = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.radioButtonPolyKernel = new System.Windows.Forms.RadioButton();
            this.radioButtonPearson = new System.Windows.Forms.RadioButton();
            this.groupBoxPearson = new System.Windows.Forms.GroupBox();
            this.numericUpDownPearsonSigma = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownPearsonOmega = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonRBF = new System.Windows.Forms.RadioButton();
            this.groupBoxRBF = new System.Windows.Forms.GroupBox();
            this.numericUpDownRBFGamma = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxPolyKernel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPolyExponent)).BeginInit();
            this.groupBoxPearson.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPearsonSigma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPearsonOmega)).BeginInit();
            this.groupBoxRBF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRBFGamma)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(160, 251);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // groupBoxPolyKernel
            // 
            this.groupBoxPolyKernel.Controls.Add(this.numericUpDownPolyExponent);
            this.groupBoxPolyKernel.Controls.Add(this.label4);
            this.groupBoxPolyKernel.Location = new System.Drawing.Point(49, 8);
            this.groupBoxPolyKernel.Name = "groupBoxPolyKernel";
            this.groupBoxPolyKernel.Size = new System.Drawing.Size(186, 66);
            this.groupBoxPolyKernel.TabIndex = 1;
            this.groupBoxPolyKernel.TabStop = false;
            this.groupBoxPolyKernel.Text = "Polynomial";
            this.toolTip.SetToolTip(this.groupBoxPolyKernel, "K(x, y) = <x, y>^p");
            // 
            // numericUpDownPolyExponent
            // 
            this.numericUpDownPolyExponent.DecimalPlaces = 1;
            this.numericUpDownPolyExponent.Location = new System.Drawing.Point(83, 27);
            this.numericUpDownPolyExponent.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownPolyExponent.Name = "numericUpDownPolyExponent";
            this.numericUpDownPolyExponent.Size = new System.Drawing.Size(92, 20);
            this.numericUpDownPolyExponent.TabIndex = 3;
            this.numericUpDownPolyExponent.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Exponent";
            // 
            // radioButtonPolyKernel
            // 
            this.radioButtonPolyKernel.AutoSize = true;
            this.radioButtonPolyKernel.Checked = true;
            this.radioButtonPolyKernel.Location = new System.Drawing.Point(23, 37);
            this.radioButtonPolyKernel.Name = "radioButtonPolyKernel";
            this.radioButtonPolyKernel.Size = new System.Drawing.Size(14, 13);
            this.radioButtonPolyKernel.TabIndex = 2;
            this.radioButtonPolyKernel.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonPolyKernel, "K(x, y) = <x, y>^p");
            this.radioButtonPolyKernel.UseVisualStyleBackColor = true;
            this.radioButtonPolyKernel.CheckedChanged += new System.EventHandler(this.radioButtonPolyKernel_CheckedChanged);
            // 
            // radioButtonPearson
            // 
            this.radioButtonPearson.AutoSize = true;
            this.radioButtonPearson.Location = new System.Drawing.Point(23, 116);
            this.radioButtonPearson.Name = "radioButtonPearson";
            this.radioButtonPearson.Size = new System.Drawing.Size(14, 13);
            this.radioButtonPearson.TabIndex = 4;
            this.toolTip.SetToolTip(this.radioButtonPearson, "Pearson VII function-based universal kernel.");
            this.radioButtonPearson.UseVisualStyleBackColor = true;
            this.radioButtonPearson.CheckedChanged += new System.EventHandler(this.radioButtonPearson_CheckedChanged);
            // 
            // groupBoxPearson
            // 
            this.groupBoxPearson.Controls.Add(this.numericUpDownPearsonSigma);
            this.groupBoxPearson.Controls.Add(this.label3);
            this.groupBoxPearson.Controls.Add(this.numericUpDownPearsonOmega);
            this.groupBoxPearson.Controls.Add(this.label2);
            this.groupBoxPearson.Enabled = false;
            this.groupBoxPearson.Location = new System.Drawing.Point(49, 80);
            this.groupBoxPearson.Name = "groupBoxPearson";
            this.groupBoxPearson.Size = new System.Drawing.Size(186, 87);
            this.groupBoxPearson.TabIndex = 3;
            this.groupBoxPearson.TabStop = false;
            this.groupBoxPearson.Text = "Pearson VII";
            this.toolTip.SetToolTip(this.groupBoxPearson, "Pearson VII function-based universal kernel.");
            // 
            // numericUpDownPearsonSigma
            // 
            this.numericUpDownPearsonSigma.DecimalPlaces = 1;
            this.numericUpDownPearsonSigma.Location = new System.Drawing.Point(88, 51);
            this.numericUpDownPearsonSigma.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownPearsonSigma.Name = "numericUpDownPearsonSigma";
            this.numericUpDownPearsonSigma.Size = new System.Drawing.Size(92, 20);
            this.numericUpDownPearsonSigma.TabIndex = 5;
            this.numericUpDownPearsonSigma.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Sigma";
            // 
            // numericUpDownPearsonOmega
            // 
            this.numericUpDownPearsonOmega.DecimalPlaces = 1;
            this.numericUpDownPearsonOmega.Location = new System.Drawing.Point(88, 23);
            this.numericUpDownPearsonOmega.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownPearsonOmega.Name = "numericUpDownPearsonOmega";
            this.numericUpDownPearsonOmega.Size = new System.Drawing.Size(92, 20);
            this.numericUpDownPearsonOmega.TabIndex = 3;
            this.numericUpDownPearsonOmega.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Gamma";
            // 
            // radioButtonRBF
            // 
            this.radioButtonRBF.AutoSize = true;
            this.radioButtonRBF.Location = new System.Drawing.Point(23, 203);
            this.radioButtonRBF.Name = "radioButtonRBF";
            this.radioButtonRBF.Size = new System.Drawing.Size(14, 13);
            this.radioButtonRBF.TabIndex = 6;
            this.toolTip.SetToolTip(this.radioButtonRBF, "K(x, y) = e^-(gamma * <x-y, x-y>^2)");
            this.radioButtonRBF.UseVisualStyleBackColor = true;
            this.radioButtonRBF.CheckedChanged += new System.EventHandler(this.radioButtonRBF_CheckedChanged);
            // 
            // groupBoxRBF
            // 
            this.groupBoxRBF.Controls.Add(this.numericUpDownRBFGamma);
            this.groupBoxRBF.Controls.Add(this.label1);
            this.groupBoxRBF.Enabled = false;
            this.groupBoxRBF.Location = new System.Drawing.Point(49, 173);
            this.groupBoxRBF.Name = "groupBoxRBF";
            this.groupBoxRBF.Size = new System.Drawing.Size(186, 72);
            this.groupBoxRBF.TabIndex = 5;
            this.groupBoxRBF.TabStop = false;
            this.groupBoxRBF.Text = "Radial Basis Function";
            this.toolTip.SetToolTip(this.groupBoxRBF, "K(x, y) = e^-(gamma * <x-y, x-y>^2)");
            // 
            // numericUpDownRBFGamma
            // 
            this.numericUpDownRBFGamma.DecimalPlaces = 2;
            this.numericUpDownRBFGamma.Location = new System.Drawing.Point(83, 30);
            this.numericUpDownRBFGamma.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownRBFGamma.Name = "numericUpDownRBFGamma";
            this.numericUpDownRBFGamma.Size = new System.Drawing.Size(92, 20);
            this.numericUpDownRBFGamma.TabIndex = 1;
            this.numericUpDownRBFGamma.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gamma";
            // 
            // FormForKernelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 280);
            this.Controls.Add(this.radioButtonRBF);
            this.Controls.Add(this.groupBoxRBF);
            this.Controls.Add(this.radioButtonPearson);
            this.Controls.Add(this.radioButtonPolyKernel);
            this.Controls.Add(this.groupBoxPearson);
            this.Controls.Add(this.groupBoxPolyKernel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForKernelEditor";
            this.Text = "Kernel Editor";
            this.groupBoxPolyKernel.ResumeLayout(false);
            this.groupBoxPolyKernel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPolyExponent)).EndInit();
            this.groupBoxPearson.ResumeLayout(false);
            this.groupBoxPearson.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPearsonSigma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPearsonOmega)).EndInit();
            this.groupBoxRBF.ResumeLayout(false);
            this.groupBoxRBF.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRBFGamma)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.GroupBox groupBoxPolyKernel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.RadioButton radioButtonPolyKernel;
        private System.Windows.Forms.RadioButton radioButtonPearson;
        private System.Windows.Forms.GroupBox groupBoxPearson;
        private System.Windows.Forms.RadioButton radioButtonRBF;
        private System.Windows.Forms.NumericUpDown numericUpDownPearsonOmega;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownRBFGamma;
        private System.Windows.Forms.GroupBox groupBoxRBF;
        private System.Windows.Forms.NumericUpDown numericUpDownPearsonSigma;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownPolyExponent;
        private System.Windows.Forms.Label label4;
    }
}