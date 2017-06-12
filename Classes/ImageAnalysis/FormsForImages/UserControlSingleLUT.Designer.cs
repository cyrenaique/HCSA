namespace HCSAnalyzer.Forms.FormsForImages
{
    partial class UserControlSingleLUT
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
            this.checkBoxIsActive = new System.Windows.Forms.CheckBox();
            this.numericUpDownMaxValue = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinValue = new System.Windows.Forms.NumericUpDown();
            this.comboBoxForLUT = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBoxForColorSample = new System.Windows.Forms.PictureBox();
            this.textBoxForName = new System.Windows.Forms.TextBox();
            this.trackBarGamma = new System.Windows.Forms.TrackBar();
            this.contextMenuStripForGamma = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemGammaDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.trackBarOpacity = new System.Windows.Forms.TrackBar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForColorSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGamma)).BeginInit();
            this.contextMenuStripForGamma.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxIsActive
            // 
            this.checkBoxIsActive.AutoSize = true;
            this.checkBoxIsActive.Checked = true;
            this.checkBoxIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIsActive.Location = new System.Drawing.Point(9, 8);
            this.checkBoxIsActive.Name = "checkBoxIsActive";
            this.checkBoxIsActive.Size = new System.Drawing.Size(56, 17);
            this.checkBoxIsActive.TabIndex = 0;
            this.checkBoxIsActive.Text = "Active";
            this.checkBoxIsActive.UseVisualStyleBackColor = true;
            // 
            // numericUpDownMaxValue
            // 
            this.numericUpDownMaxValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownMaxValue.DecimalPlaces = 3;
            this.numericUpDownMaxValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownMaxValue.Location = new System.Drawing.Point(9, 52);
            this.numericUpDownMaxValue.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.numericUpDownMaxValue.Minimum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            -2147483648});
            this.numericUpDownMaxValue.Name = "numericUpDownMaxValue";
            this.numericUpDownMaxValue.Size = new System.Drawing.Size(62, 14);
            this.numericUpDownMaxValue.TabIndex = 6;
            this.numericUpDownMaxValue.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // numericUpDownMinValue
            // 
            this.numericUpDownMinValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownMinValue.DecimalPlaces = 3;
            this.numericUpDownMinValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownMinValue.Location = new System.Drawing.Point(9, 32);
            this.numericUpDownMinValue.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.numericUpDownMinValue.Minimum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            -2147483648});
            this.numericUpDownMinValue.Name = "numericUpDownMinValue";
            this.numericUpDownMinValue.Size = new System.Drawing.Size(62, 14);
            this.numericUpDownMinValue.TabIndex = 7;
            // 
            // comboBoxForLUT
            // 
            this.comboBoxForLUT.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxForLUT.FormattingEnabled = true;
            this.comboBoxForLUT.Items.AddRange(new object[] {
            "Linear",
            "HSV",
            "Fire",
            "Green to Red",
            "Jet",
            "Hot",
            "Cool",
            "Spring",
            "Summer",
            "Automn",
            "Winter",
            "Bone",
            "Copper",
            "GD"});
            this.comboBoxForLUT.Location = new System.Drawing.Point(115, 32);
            this.comboBoxForLUT.Name = "comboBoxForLUT";
            this.comboBoxForLUT.Size = new System.Drawing.Size(106, 20);
            this.comboBoxForLUT.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "LUT";
            // 
            // pictureBoxForColorSample
            // 
            this.pictureBoxForColorSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxForColorSample.Location = new System.Drawing.Point(86, 58);
            this.pictureBoxForColorSample.Name = "pictureBoxForColorSample";
            this.pictureBoxForColorSample.Size = new System.Drawing.Size(135, 15);
            this.pictureBoxForColorSample.TabIndex = 10;
            this.pictureBoxForColorSample.TabStop = false;
            // 
            // textBoxForName
            // 
            this.textBoxForName.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.textBoxForName.Location = new System.Drawing.Point(84, 7);
            this.textBoxForName.Name = "textBoxForName";
            this.textBoxForName.Size = new System.Drawing.Size(137, 18);
            this.textBoxForName.TabIndex = 11;
            // 
            // trackBarGamma
            // 
            this.trackBarGamma.AutoSize = false;
            this.trackBarGamma.BackColor = System.Drawing.Color.White;
            this.trackBarGamma.ContextMenuStrip = this.contextMenuStripForGamma;
            this.trackBarGamma.Location = new System.Drawing.Point(6, 7);
            this.trackBarGamma.Maximum = 200;
            this.trackBarGamma.Name = "trackBarGamma";
            this.trackBarGamma.Size = new System.Drawing.Size(203, 26);
            this.trackBarGamma.TabIndex = 12;
            this.trackBarGamma.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarGamma.Value = 100;
            // 
            // contextMenuStripForGamma
            // 
            this.contextMenuStripForGamma.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemGammaDefault});
            this.contextMenuStripForGamma.Name = "contextMenuStripForGamma";
            this.contextMenuStripForGamma.Size = new System.Drawing.Size(90, 26);
            // 
            // toolStripMenuItemGammaDefault
            // 
            this.toolStripMenuItemGammaDefault.Name = "toolStripMenuItemGammaDefault";
            this.toolStripMenuItemGammaDefault.Size = new System.Drawing.Size(89, 22);
            this.toolStripMenuItemGammaDefault.Text = "1.0";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 79);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(223, 63);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.trackBarOpacity);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(215, 37);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Opacity";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // trackBarOpacity
            // 
            this.trackBarOpacity.AutoSize = false;
            this.trackBarOpacity.BackColor = System.Drawing.Color.White;
            this.trackBarOpacity.ContextMenuStrip = this.contextMenuStripForGamma;
            this.trackBarOpacity.Location = new System.Drawing.Point(6, 8);
            this.trackBarOpacity.Maximum = 100;
            this.trackBarOpacity.Name = "trackBarOpacity";
            this.trackBarOpacity.Size = new System.Drawing.Size(203, 26);
            this.trackBarOpacity.TabIndex = 13;
            this.trackBarOpacity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarOpacity.Value = 100;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.trackBarGamma);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(215, 37);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Gamma";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // UserControlSingleLUT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.textBoxForName);
            this.Controls.Add(this.pictureBoxForColorSample);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxForLUT);
            this.Controls.Add(this.numericUpDownMinValue);
            this.Controls.Add(this.numericUpDownMaxValue);
            this.Controls.Add(this.checkBoxIsActive);
            this.Name = "UserControlSingleLUT";
            this.Size = new System.Drawing.Size(229, 147);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForColorSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGamma)).EndInit();
            this.contextMenuStripForGamma.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox checkBoxIsActive;
        public System.Windows.Forms.NumericUpDown numericUpDownMaxValue;
        public System.Windows.Forms.NumericUpDown numericUpDownMinValue;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxForName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForGamma;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemGammaDefault;
        public System.Windows.Forms.TrackBar trackBarGamma;
        public System.Windows.Forms.ComboBox comboBoxForLUT;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.TrackBar trackBarOpacity;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.PictureBox pictureBoxForColorSample;
    }
}
