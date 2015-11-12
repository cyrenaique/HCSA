namespace HCSAnalyzer.Classes.ImageAnalysis._3D_Engine.VolumeRendering
{
    partial class FormForVolumeRenderingOption
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForVolumeRenderingOption));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackBarMaxOpacityPos = new System.Windows.Forms.TrackBar();
            this.trackBarMinOpacityPos = new System.Windows.Forms.TrackBar();
            this.numericUpDownMaxOpacity = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinOpacity = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonInterpolationNN = new System.Windows.Forms.RadioButton();
            this.radioButtonInterpolationLinear = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMaxOpacityPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMinOpacityPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxOpacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinOpacity)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trackBarMaxOpacityPos);
            this.groupBox1.Controls.Add(this.trackBarMinOpacityPos);
            this.groupBox1.Controls.Add(this.numericUpDownMaxOpacity);
            this.groupBox1.Controls.Add(this.numericUpDownMinOpacity);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 99);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opacity";
            // 
            // trackBarMaxOpacityPos
            // 
            this.trackBarMaxOpacityPos.AutoSize = false;
            this.trackBarMaxOpacityPos.Location = new System.Drawing.Point(166, 63);
            this.trackBarMaxOpacityPos.Maximum = 100;
            this.trackBarMaxOpacityPos.Name = "trackBarMaxOpacityPos";
            this.trackBarMaxOpacityPos.Size = new System.Drawing.Size(126, 30);
            this.trackBarMaxOpacityPos.TabIndex = 4;
            this.trackBarMaxOpacityPos.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMaxOpacityPos.Value = 100;
            this.trackBarMaxOpacityPos.Scroll += new System.EventHandler(this.trackBarMaxOpacityPos_Scroll);
            // 
            // trackBarMinOpacityPos
            // 
            this.trackBarMinOpacityPos.AutoSize = false;
            this.trackBarMinOpacityPos.Location = new System.Drawing.Point(166, 23);
            this.trackBarMinOpacityPos.Maximum = 100;
            this.trackBarMinOpacityPos.Name = "trackBarMinOpacityPos";
            this.trackBarMinOpacityPos.Size = new System.Drawing.Size(126, 29);
            this.trackBarMinOpacityPos.TabIndex = 3;
            this.trackBarMinOpacityPos.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMinOpacityPos.Scroll += new System.EventHandler(this.trackBarMinOpacityPos_Scroll);
            // 
            // numericUpDownMaxOpacity
            // 
            this.numericUpDownMaxOpacity.DecimalPlaces = 3;
            this.numericUpDownMaxOpacity.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownMaxOpacity.Location = new System.Drawing.Point(74, 62);
            this.numericUpDownMaxOpacity.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxOpacity.Name = "numericUpDownMaxOpacity";
            this.numericUpDownMaxOpacity.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownMaxOpacity.TabIndex = 2;
            this.numericUpDownMaxOpacity.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.numericUpDownMaxOpacity.ValueChanged += new System.EventHandler(this.numericUpDownMaxOpacity_ValueChanged);
            // 
            // numericUpDownMinOpacity
            // 
            this.numericUpDownMinOpacity.DecimalPlaces = 3;
            this.numericUpDownMinOpacity.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownMinOpacity.Location = new System.Drawing.Point(74, 24);
            this.numericUpDownMinOpacity.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinOpacity.Name = "numericUpDownMinOpacity";
            this.numericUpDownMinOpacity.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownMinOpacity.TabIndex = 1;
            this.numericUpDownMinOpacity.ValueChanged += new System.EventHandler(this.numericUpDownMinOpacity_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Maximum";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Minimum";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(241, 178);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonInterpolationLinear);
            this.groupBox2.Controls.Add(this.radioButtonInterpolationNN);
            this.groupBox2.Location = new System.Drawing.Point(7, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(308, 58);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Interpolation";
            // 
            // radioButtonInterpolationNN
            // 
            this.radioButtonInterpolationNN.AutoSize = true;
            this.radioButtonInterpolationNN.Checked = true;
            this.radioButtonInterpolationNN.Location = new System.Drawing.Point(50, 27);
            this.radioButtonInterpolationNN.Name = "radioButtonInterpolationNN";
            this.radioButtonInterpolationNN.Size = new System.Drawing.Size(108, 17);
            this.radioButtonInterpolationNN.TabIndex = 0;
            this.radioButtonInterpolationNN.TabStop = true;
            this.radioButtonInterpolationNN.Text = "Nearest Neighbor";
            this.radioButtonInterpolationNN.UseVisualStyleBackColor = true;
            this.radioButtonInterpolationNN.CheckedChanged += new System.EventHandler(this.radioButtonInterpolationNN_CheckedChanged);
            // 
            // radioButtonInterpolationLinear
            // 
            this.radioButtonInterpolationLinear.AutoSize = true;
            this.radioButtonInterpolationLinear.Location = new System.Drawing.Point(204, 27);
            this.radioButtonInterpolationLinear.Name = "radioButtonInterpolationLinear";
            this.radioButtonInterpolationLinear.Size = new System.Drawing.Size(54, 17);
            this.radioButtonInterpolationLinear.TabIndex = 1;
            this.radioButtonInterpolationLinear.Text = "Linear";
            this.radioButtonInterpolationLinear.UseVisualStyleBackColor = true;
            this.radioButtonInterpolationLinear.CheckedChanged += new System.EventHandler(this.radioButtonInterpolationLinear_CheckedChanged);
            // 
            // FormForVolumeRenderingOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 208);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormForVolumeRenderingOption";
            this.Text = "Volume Rendering Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMaxOpacityPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMinOpacityPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxOpacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinOpacity)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar trackBarMaxOpacityPos;
        public System.Windows.Forms.TrackBar trackBarMinOpacityPos;
        public System.Windows.Forms.NumericUpDown numericUpDownMaxOpacity;
        public System.Windows.Forms.NumericUpDown numericUpDownMinOpacity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonInterpolationLinear;
        private System.Windows.Forms.RadioButton radioButtonInterpolationNN;
    }
}