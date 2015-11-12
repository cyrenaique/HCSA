namespace HCSAnalyzer.Classes.Base_Components.GUI
{
    partial class FormForHistoBinOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForHistoBinOptions));
            this.buttonOk = new System.Windows.Forms.Button();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.groupBoxBinNumber = new System.Windows.Forms.GroupBox();
            this.radioButtonBinNumber = new System.Windows.Forms.RadioButton();
            this.groupBoxBinSize = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownBinSize = new System.Windows.Forms.NumericUpDown();
            this.radioButtonBinSize = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonMinMaxManual = new System.Windows.Forms.RadioButton();
            this.radioButtonMinMaxAutomated = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownMin = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownMax = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.groupBoxBinNumber.SuspendLayout();
            this.groupBoxBinSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBinSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMax)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(253, 272);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(81, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // numericUpDown
            // 
            this.numericUpDown.Location = new System.Drawing.Point(228, 33);
            this.numericUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(81, 20);
            this.numericUpDown.TabIndex = 4;
            this.numericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(6, 22);
            this.trackBar.Maximum = 5000;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(216, 45);
            this.trackBar.TabIndex = 3;
            this.trackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar.Value = 10;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // groupBoxBinNumber
            // 
            this.groupBoxBinNumber.Controls.Add(this.trackBar);
            this.groupBoxBinNumber.Controls.Add(this.numericUpDown);
            this.groupBoxBinNumber.Location = new System.Drawing.Point(5, 10);
            this.groupBoxBinNumber.Name = "groupBoxBinNumber";
            this.groupBoxBinNumber.Size = new System.Drawing.Size(328, 78);
            this.groupBoxBinNumber.TabIndex = 5;
            this.groupBoxBinNumber.TabStop = false;
            this.groupBoxBinNumber.Text = "                             ";
            // 
            // radioButtonBinNumber
            // 
            this.radioButtonBinNumber.AutoSize = true;
            this.radioButtonBinNumber.Checked = true;
            this.radioButtonBinNumber.Location = new System.Drawing.Point(16, 8);
            this.radioButtonBinNumber.Name = "radioButtonBinNumber";
            this.radioButtonBinNumber.Size = new System.Drawing.Size(80, 17);
            this.radioButtonBinNumber.TabIndex = 7;
            this.radioButtonBinNumber.TabStop = true;
            this.radioButtonBinNumber.Text = "Bin Number";
            this.radioButtonBinNumber.UseVisualStyleBackColor = true;
            this.radioButtonBinNumber.CheckedChanged += new System.EventHandler(this.radioButtonBinNumber_CheckedChanged);
            // 
            // groupBoxBinSize
            // 
            this.groupBoxBinSize.Controls.Add(this.label1);
            this.groupBoxBinSize.Controls.Add(this.numericUpDownBinSize);
            this.groupBoxBinSize.Enabled = false;
            this.groupBoxBinSize.Location = new System.Drawing.Point(5, 95);
            this.groupBoxBinSize.Name = "groupBoxBinSize";
            this.groupBoxBinSize.Size = new System.Drawing.Size(327, 64);
            this.groupBoxBinSize.TabIndex = 6;
            this.groupBoxBinSize.TabStop = false;
            this.groupBoxBinSize.Text = "                       ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Size";
            // 
            // numericUpDownBinSize
            // 
            this.numericUpDownBinSize.DecimalPlaces = 5;
            this.numericUpDownBinSize.Location = new System.Drawing.Point(65, 25);
            this.numericUpDownBinSize.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numericUpDownBinSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.numericUpDownBinSize.Name = "numericUpDownBinSize";
            this.numericUpDownBinSize.Size = new System.Drawing.Size(81, 20);
            this.numericUpDownBinSize.TabIndex = 5;
            this.numericUpDownBinSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // radioButtonBinSize
            // 
            this.radioButtonBinSize.AutoSize = true;
            this.radioButtonBinSize.Location = new System.Drawing.Point(15, 94);
            this.radioButtonBinSize.Name = "radioButtonBinSize";
            this.radioButtonBinSize.Size = new System.Drawing.Size(63, 17);
            this.radioButtonBinSize.TabIndex = 8;
            this.radioButtonBinSize.TabStop = true;
            this.radioButtonBinSize.Text = "Bin Size";
            this.radioButtonBinSize.UseVisualStyleBackColor = true;
            this.radioButtonBinSize.CheckedChanged += new System.EventHandler(this.radioButtonBinSize_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.radioButtonMinMaxManual);
            this.groupBox1.Controls.Add(this.radioButtonMinMaxAutomated);
            this.groupBox1.Location = new System.Drawing.Point(7, 166);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 95);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Min / Max";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numericUpDownMax);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.numericUpDownMin);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(79, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 36);
            this.panel1.TabIndex = 2;
            // 
            // radioButtonMinMaxManual
            // 
            this.radioButtonMinMaxManual.AutoSize = true;
            this.radioButtonMinMaxManual.Location = new System.Drawing.Point(19, 55);
            this.radioButtonMinMaxManual.Name = "radioButtonMinMaxManual";
            this.radioButtonMinMaxManual.Size = new System.Drawing.Size(60, 17);
            this.radioButtonMinMaxManual.TabIndex = 1;
            this.radioButtonMinMaxManual.Text = "Manual";
            this.radioButtonMinMaxManual.UseVisualStyleBackColor = true;
            this.radioButtonMinMaxManual.CheckedChanged += new System.EventHandler(this.radioButtonMinMaxManual_CheckedChanged);
            // 
            // radioButtonMinMaxAutomated
            // 
            this.radioButtonMinMaxAutomated.AutoSize = true;
            this.radioButtonMinMaxAutomated.Checked = true;
            this.radioButtonMinMaxAutomated.Location = new System.Drawing.Point(19, 21);
            this.radioButtonMinMaxAutomated.Name = "radioButtonMinMaxAutomated";
            this.radioButtonMinMaxAutomated.Size = new System.Drawing.Size(76, 17);
            this.radioButtonMinMaxAutomated.TabIndex = 0;
            this.radioButtonMinMaxAutomated.TabStop = true;
            this.radioButtonMinMaxAutomated.Text = "Automated";
            this.radioButtonMinMaxAutomated.UseVisualStyleBackColor = true;
            this.radioButtonMinMaxAutomated.CheckedChanged += new System.EventHandler(this.radioButtonMinMaxAutomated_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Min";
            // 
            // numericUpDownMin
            // 
            this.numericUpDownMin.DecimalPlaces = 5;
            this.numericUpDownMin.Location = new System.Drawing.Point(29, 8);
            this.numericUpDownMin.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.numericUpDownMin.Minimum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            -2147483648});
            this.numericUpDownMin.Name = "numericUpDownMin";
            this.numericUpDownMin.Size = new System.Drawing.Size(81, 20);
            this.numericUpDownMin.TabIndex = 7;
            this.numericUpDownMin.ValueChanged += new System.EventHandler(this.numericUpDownMin_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(124, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Max";
            // 
            // numericUpDownMax
            // 
            this.numericUpDownMax.DecimalPlaces = 5;
            this.numericUpDownMax.Location = new System.Drawing.Point(155, 8);
            this.numericUpDownMax.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.numericUpDownMax.Minimum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            -2147483648});
            this.numericUpDownMax.Name = "numericUpDownMax";
            this.numericUpDownMax.Size = new System.Drawing.Size(81, 20);
            this.numericUpDownMax.TabIndex = 11;
            this.numericUpDownMax.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownMax.ValueChanged += new System.EventHandler(this.numericUpDownMax_ValueChanged);
            // 
            // FormForHistoBinOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 301);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.radioButtonBinNumber);
            this.Controls.Add(this.radioButtonBinSize);
            this.Controls.Add(this.groupBoxBinSize);
            this.Controls.Add(this.groupBoxBinNumber);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForHistoBinOptions";
            this.Text = "Histogram Options";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.groupBoxBinNumber.ResumeLayout(false);
            this.groupBoxBinNumber.PerformLayout();
            this.groupBoxBinSize.ResumeLayout(false);
            this.groupBoxBinSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBinSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.NumericUpDown numericUpDown;
        public System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.GroupBox groupBoxBinNumber;
        private System.Windows.Forms.GroupBox groupBoxBinSize;
        private System.Windows.Forms.RadioButton radioButtonBinSize;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDownBinSize;
        public System.Windows.Forms.RadioButton radioButtonBinNumber;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.RadioButton radioButtonMinMaxManual;
        public System.Windows.Forms.RadioButton radioButtonMinMaxAutomated;
        public System.Windows.Forms.NumericUpDown numericUpDownMax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDownMin;
    }
}