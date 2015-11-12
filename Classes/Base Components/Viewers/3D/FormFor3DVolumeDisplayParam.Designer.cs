namespace HCSAnalyzer.Classes.Base_Components.Viewers._3D
{
    partial class FormFor3DVolumeDisplayParam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFor3DVolumeDisplayParam));
            this.panelFor3DOptions = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonInterpolationLinear = new System.Windows.Forms.RadioButton();
            this.radioButtonInterpolationNN = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownColorMaxPos = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownColorMinPos = new System.Windows.Forms.NumericUpDown();
            this.panelColorLast = new System.Windows.Forms.Panel();
            this.panelColor = new System.Windows.Forms.Panel();
            this.Opacity = new System.Windows.Forms.GroupBox();
            this.numericUpDownOpacityMaxValue = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownOpacityMinValue = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownOpacityMaxPos = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownOpacityMinPos = new System.Windows.Forms.NumericUpDown();
            this.GroupBoxPosition = new System.Windows.Forms.GroupBox();
            this.numericUpDownPosZ = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownPosY = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownPosX = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panelFor3DOptions.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColorMaxPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColorMinPos)).BeginInit();
            this.Opacity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpacityMaxValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpacityMinValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpacityMaxPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpacityMinPos)).BeginInit();
            this.GroupBoxPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosX)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFor3DOptions
            // 
            this.panelFor3DOptions.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelFor3DOptions.Controls.Add(this.groupBox2);
            this.panelFor3DOptions.Controls.Add(this.groupBox1);
            this.panelFor3DOptions.Controls.Add(this.Opacity);
            this.panelFor3DOptions.Controls.Add(this.GroupBoxPosition);
            this.panelFor3DOptions.Location = new System.Drawing.Point(3, 5);
            this.panelFor3DOptions.Name = "panelFor3DOptions";
            this.panelFor3DOptions.Size = new System.Drawing.Size(597, 236);
            this.panelFor3DOptions.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonInterpolationLinear);
            this.groupBox2.Controls.Add(this.radioButtonInterpolationNN);
            this.groupBox2.Location = new System.Drawing.Point(3, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(193, 82);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Interpolation";
            // 
            // radioButtonInterpolationLinear
            // 
            this.radioButtonInterpolationLinear.AutoSize = true;
            this.radioButtonInterpolationLinear.Location = new System.Drawing.Point(68, 49);
            this.radioButtonInterpolationLinear.Name = "radioButtonInterpolationLinear";
            this.radioButtonInterpolationLinear.Size = new System.Drawing.Size(54, 17);
            this.radioButtonInterpolationLinear.TabIndex = 1;
            this.radioButtonInterpolationLinear.Text = "Linear";
            this.radioButtonInterpolationLinear.UseVisualStyleBackColor = true;
            this.radioButtonInterpolationLinear.CheckedChanged += new System.EventHandler(this.radioButtonInterpolationLinear_CheckedChanged);
            // 
            // radioButtonInterpolationNN
            // 
            this.radioButtonInterpolationNN.AutoSize = true;
            this.radioButtonInterpolationNN.Checked = true;
            this.radioButtonInterpolationNN.Location = new System.Drawing.Point(41, 21);
            this.radioButtonInterpolationNN.Name = "radioButtonInterpolationNN";
            this.radioButtonInterpolationNN.Size = new System.Drawing.Size(108, 17);
            this.radioButtonInterpolationNN.TabIndex = 0;
            this.radioButtonInterpolationNN.TabStop = true;
            this.radioButtonInterpolationNN.Text = "Nearest Neighbor";
            this.radioButtonInterpolationNN.UseVisualStyleBackColor = true;
            this.radioButtonInterpolationNN.CheckedChanged += new System.EventHandler(this.radioButtonInterpolationNN_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownColorMaxPos);
            this.groupBox1.Controls.Add(this.numericUpDownColorMinPos);
            this.groupBox1.Controls.Add(this.panelColorLast);
            this.groupBox1.Controls.Add(this.panelColor);
            this.groupBox1.Location = new System.Drawing.Point(202, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 84);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color";
            // 
            // numericUpDownColorMaxPos
            // 
            this.numericUpDownColorMaxPos.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownColorMaxPos.Location = new System.Drawing.Point(115, 50);
            this.numericUpDownColorMaxPos.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownColorMaxPos.Name = "numericUpDownColorMaxPos";
            this.numericUpDownColorMaxPos.Size = new System.Drawing.Size(72, 20);
            this.numericUpDownColorMaxPos.TabIndex = 3;
            this.numericUpDownColorMaxPos.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownColorMaxPos.ValueChanged += new System.EventHandler(this.numericUpDownColorMaxPos_ValueChanged);
            // 
            // numericUpDownColorMinPos
            // 
            this.numericUpDownColorMinPos.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownColorMinPos.Location = new System.Drawing.Point(24, 50);
            this.numericUpDownColorMinPos.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownColorMinPos.Name = "numericUpDownColorMinPos";
            this.numericUpDownColorMinPos.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownColorMinPos.TabIndex = 2;
            this.numericUpDownColorMinPos.ValueChanged += new System.EventHandler(this.numericUpDownColorMinPos_ValueChanged);
            // 
            // panelColorLast
            // 
            this.panelColorLast.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColorLast.Location = new System.Drawing.Point(140, 20);
            this.panelColorLast.Name = "panelColorLast";
            this.panelColorLast.Size = new System.Drawing.Size(24, 23);
            this.panelColorLast.TabIndex = 1;
            this.panelColorLast.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelColorLast_MouseDoubleClick);
            // 
            // panelColor
            // 
            this.panelColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColor.Location = new System.Drawing.Point(46, 20);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(24, 23);
            this.panelColor.TabIndex = 0;
            this.panelColor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDoubleClick);
            // 
            // Opacity
            // 
            this.Opacity.Controls.Add(this.numericUpDownOpacityMaxValue);
            this.Opacity.Controls.Add(this.numericUpDownOpacityMinValue);
            this.Opacity.Controls.Add(this.label7);
            this.Opacity.Controls.Add(this.numericUpDownOpacityMaxPos);
            this.Opacity.Controls.Add(this.label6);
            this.Opacity.Controls.Add(this.label5);
            this.Opacity.Controls.Add(this.label4);
            this.Opacity.Controls.Add(this.numericUpDownOpacityMinPos);
            this.Opacity.Location = new System.Drawing.Point(202, 93);
            this.Opacity.Name = "Opacity";
            this.Opacity.Size = new System.Drawing.Size(210, 99);
            this.Opacity.TabIndex = 1;
            this.Opacity.TabStop = false;
            this.Opacity.Text = "Opacity";
            // 
            // numericUpDownOpacityMaxValue
            // 
            this.numericUpDownOpacityMaxValue.DecimalPlaces = 2;
            this.numericUpDownOpacityMaxValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownOpacityMaxValue.Location = new System.Drawing.Point(131, 37);
            this.numericUpDownOpacityMaxValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownOpacityMaxValue.Name = "numericUpDownOpacityMaxValue";
            this.numericUpDownOpacityMaxValue.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownOpacityMaxValue.TabIndex = 11;
            this.numericUpDownOpacityMaxValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownOpacityMaxValue.ValueChanged += new System.EventHandler(this.numericUpDownOpacityMaxValue_ValueChanged);
            // 
            // numericUpDownOpacityMinValue
            // 
            this.numericUpDownOpacityMinValue.DecimalPlaces = 2;
            this.numericUpDownOpacityMinValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownOpacityMinValue.Location = new System.Drawing.Point(52, 37);
            this.numericUpDownOpacityMinValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownOpacityMinValue.Name = "numericUpDownOpacityMinValue";
            this.numericUpDownOpacityMinValue.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownOpacityMinValue.TabIndex = 10;
            this.numericUpDownOpacityMinValue.ValueChanged += new System.EventHandler(this.numericUpDownOpacityMinValue_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(151, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Max";
            // 
            // numericUpDownOpacityMaxPos
            // 
            this.numericUpDownOpacityMaxPos.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownOpacityMaxPos.Location = new System.Drawing.Point(131, 66);
            this.numericUpDownOpacityMaxPos.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownOpacityMaxPos.Name = "numericUpDownOpacityMaxPos";
            this.numericUpDownOpacityMaxPos.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownOpacityMaxPos.TabIndex = 8;
            this.numericUpDownOpacityMaxPos.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownOpacityMaxPos.ValueChanged += new System.EventHandler(this.numericUpDownOpacityMaxPos_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Value";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Pos";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(70, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Min";
            // 
            // numericUpDownOpacityMinPos
            // 
            this.numericUpDownOpacityMinPos.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownOpacityMinPos.Location = new System.Drawing.Point(52, 66);
            this.numericUpDownOpacityMinPos.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownOpacityMinPos.Name = "numericUpDownOpacityMinPos";
            this.numericUpDownOpacityMinPos.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownOpacityMinPos.TabIndex = 4;
            this.numericUpDownOpacityMinPos.ValueChanged += new System.EventHandler(this.numericUpDownOpacityMinPos_ValueChanged);
            // 
            // GroupBoxPosition
            // 
            this.GroupBoxPosition.Controls.Add(this.numericUpDownPosZ);
            this.GroupBoxPosition.Controls.Add(this.label3);
            this.GroupBoxPosition.Controls.Add(this.numericUpDownPosY);
            this.GroupBoxPosition.Controls.Add(this.label2);
            this.GroupBoxPosition.Controls.Add(this.numericUpDownPosX);
            this.GroupBoxPosition.Controls.Add(this.label1);
            this.GroupBoxPosition.Location = new System.Drawing.Point(3, 3);
            this.GroupBoxPosition.Name = "GroupBoxPosition";
            this.GroupBoxPosition.Size = new System.Drawing.Size(193, 101);
            this.GroupBoxPosition.TabIndex = 0;
            this.GroupBoxPosition.TabStop = false;
            this.GroupBoxPosition.Text = "Position";
            // 
            // numericUpDownPosZ
            // 
            this.numericUpDownPosZ.DecimalPlaces = 4;
            this.numericUpDownPosZ.Location = new System.Drawing.Point(51, 69);
            this.numericUpDownPosZ.Maximum = new decimal(new int[] {
            1874919424,
            2328306,
            0,
            0});
            this.numericUpDownPosZ.Minimum = new decimal(new int[] {
            276447232,
            23283,
            0,
            -2147483648});
            this.numericUpDownPosZ.Name = "numericUpDownPosZ";
            this.numericUpDownPosZ.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownPosZ.TabIndex = 5;
            this.numericUpDownPosZ.ValueChanged += new System.EventHandler(this.numericUpDownPosZ_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Z";
            // 
            // numericUpDownPosY
            // 
            this.numericUpDownPosY.DecimalPlaces = 4;
            this.numericUpDownPosY.Location = new System.Drawing.Point(51, 43);
            this.numericUpDownPosY.Maximum = new decimal(new int[] {
            1874919424,
            2328306,
            0,
            0});
            this.numericUpDownPosY.Minimum = new decimal(new int[] {
            276447232,
            23283,
            0,
            -2147483648});
            this.numericUpDownPosY.Name = "numericUpDownPosY";
            this.numericUpDownPosY.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownPosY.TabIndex = 3;
            this.numericUpDownPosY.ValueChanged += new System.EventHandler(this.numericUpDownPosY_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y";
            // 
            // numericUpDownPosX
            // 
            this.numericUpDownPosX.DecimalPlaces = 4;
            this.numericUpDownPosX.Location = new System.Drawing.Point(51, 18);
            this.numericUpDownPosX.Maximum = new decimal(new int[] {
            1874919424,
            2328306,
            0,
            0});
            this.numericUpDownPosX.Minimum = new decimal(new int[] {
            276447232,
            23283,
            0,
            -2147483648});
            this.numericUpDownPosX.Name = "numericUpDownPosX";
            this.numericUpDownPosX.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownPosX.TabIndex = 1;
            this.numericUpDownPosX.ValueChanged += new System.EventHandler(this.numericUpDownPosX_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(525, 247);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // FormFor3DVolumeDisplayParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 274);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.panelFor3DOptions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFor3DVolumeDisplayParam";
            this.Text = "3D Volume Display Options";
            this.panelFor3DOptions.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColorMaxPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColorMinPos)).EndInit();
            this.Opacity.ResumeLayout(false);
            this.Opacity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpacityMaxValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpacityMinValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpacityMaxPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpacityMinPos)).EndInit();
            this.GroupBoxPosition.ResumeLayout(false);
            this.GroupBoxPosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelFor3DOptions;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panelColor;
        private System.Windows.Forms.GroupBox Opacity;
        private System.Windows.Forms.GroupBox GroupBoxPosition;
        public System.Windows.Forms.NumericUpDown numericUpDownPosZ;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown numericUpDownPosY;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDownPosX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.NumericUpDown numericUpDownColorMinPos;
        private System.Windows.Forms.Panel panelColorLast;
        private System.Windows.Forms.NumericUpDown numericUpDownColorMaxPos;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonInterpolationLinear;
        private System.Windows.Forms.RadioButton radioButtonInterpolationNN;
        private System.Windows.Forms.NumericUpDown numericUpDownOpacityMaxValue;
        private System.Windows.Forms.NumericUpDown numericUpDownOpacityMinValue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownOpacityMaxPos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownOpacityMinPos;
    }
}