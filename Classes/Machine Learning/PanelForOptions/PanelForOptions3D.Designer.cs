namespace HCSAnalyzer.Forms.FormsForOptions.PanelForOptions
{
    partial class PanelForOptions3D
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
            this.panel = new System.Windows.Forms.Panel();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.numericUpDownWellSize = new System.Windows.Forms.NumericUpDown();
            this.label27 = new System.Windows.Forms.Label();
            this.numericUpDownWellOpacity = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.groupBoxForDRC = new System.Windows.Forms.GroupBox();
            this.checkBoxDRC = new System.Windows.Forms.CheckBox();
            this.numericUpDownDRCOpacity = new System.Windows.Forms.NumericUpDown();
            this.label26 = new System.Windows.Forms.Label();
            this.groupBoxForSurfaceAnalysis = new System.Windows.Forms.GroupBox();
            this.checkBox3DComputeThinPlate = new System.Windows.Forms.CheckBox();
            this.numericUpDown3DThinPlateRegularization = new System.Windows.Forms.NumericUpDown();
            this.label28 = new System.Windows.Forms.Label();
            this.checkBox3DDisplayThinPlate = new System.Windows.Forms.CheckBox();
            this.checkBox3DDisplayIsoRatioCurves = new System.Windows.Forms.CheckBox();
            this.checkBox3DDisplayIsoboles = new System.Windows.Forms.CheckBox();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.checkBox3DPlateInformation = new System.Windows.Forms.CheckBox();
            this.panel.SuspendLayout();
            this.groupBox21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWellSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWellOpacity)).BeginInit();
            this.groupBoxForDRC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDRCOpacity)).BeginInit();
            this.groupBoxForSurfaceAnalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3DThinPlateRegularization)).BeginInit();
            this.groupBox23.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.checkBox3DComputeThinPlate);
            this.panel.Controls.Add(this.checkBoxDRC);
            this.panel.Controls.Add(this.groupBox21);
            this.panel.Controls.Add(this.groupBoxForDRC);
            this.panel.Controls.Add(this.groupBoxForSurfaceAnalysis);
            this.panel.Controls.Add(this.groupBox23);
            this.panel.Location = new System.Drawing.Point(-1, -2);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(266, 335);
            this.panel.TabIndex = 0;
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.numericUpDownWellSize);
            this.groupBox21.Controls.Add(this.label27);
            this.groupBox21.Controls.Add(this.numericUpDownWellOpacity);
            this.groupBox21.Controls.Add(this.label25);
            this.groupBox21.Location = new System.Drawing.Point(5, 62);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(255, 76);
            this.groupBox21.TabIndex = 24;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Wells Display";
            // 
            // numericUpDownWellSize
            // 
            this.numericUpDownWellSize.DecimalPlaces = 2;
            this.numericUpDownWellSize.Location = new System.Drawing.Point(91, 47);
            this.numericUpDownWellSize.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWellSize.Name = "numericUpDownWellSize";
            this.numericUpDownWellSize.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownWellSize.TabIndex = 5;
            this.numericUpDownWellSize.Value = new decimal(new int[] {
            9,
            0,
            0,
            65536});
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(8, 51);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(49, 13);
            this.label27.TabIndex = 4;
            this.label27.Text = "Well size";
            // 
            // numericUpDownWellOpacity
            // 
            this.numericUpDownWellOpacity.DecimalPlaces = 2;
            this.numericUpDownWellOpacity.Location = new System.Drawing.Point(91, 21);
            this.numericUpDownWellOpacity.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWellOpacity.Name = "numericUpDownWellOpacity";
            this.numericUpDownWellOpacity.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownWellOpacity.TabIndex = 1;
            this.numericUpDownWellOpacity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(8, 25);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(65, 13);
            this.label25.TabIndex = 0;
            this.label25.Text = "Well opacity";
            // 
            // groupBoxForDRC
            // 
            this.groupBoxForDRC.Controls.Add(this.numericUpDownDRCOpacity);
            this.groupBoxForDRC.Controls.Add(this.label26);
            this.groupBoxForDRC.Enabled = false;
            this.groupBoxForDRC.Location = new System.Drawing.Point(5, 145);
            this.groupBoxForDRC.Name = "groupBoxForDRC";
            this.groupBoxForDRC.Size = new System.Drawing.Size(255, 56);
            this.groupBoxForDRC.TabIndex = 25;
            this.groupBoxForDRC.TabStop = false;
            this.groupBoxForDRC.Text = "                                              ";
            // 
            // checkBoxDRC
            // 
            this.checkBoxDRC.AutoSize = true;
            this.checkBoxDRC.Location = new System.Drawing.Point(15, 144);
            this.checkBoxDRC.Name = "checkBoxDRC";
            this.checkBoxDRC.Size = new System.Drawing.Size(138, 17);
            this.checkBoxDRC.TabIndex = 23;
            this.checkBoxDRC.Text = "Dose Response Curves";
            this.checkBoxDRC.UseVisualStyleBackColor = true;
            this.checkBoxDRC.CheckedChanged += new System.EventHandler(this.checkBoxDRC_CheckedChanged);
            // 
            // numericUpDownDRCOpacity
            // 
            this.numericUpDownDRCOpacity.DecimalPlaces = 2;
            this.numericUpDownDRCOpacity.Location = new System.Drawing.Point(102, 23);
            this.numericUpDownDRCOpacity.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDRCOpacity.Name = "numericUpDownDRCOpacity";
            this.numericUpDownDRCOpacity.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownDRCOpacity.TabIndex = 3;
            this.numericUpDownDRCOpacity.Value = new decimal(new int[] {
            7,
            0,
            0,
            65536});
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(20, 26);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(67, 13);
            this.label26.TabIndex = 2;
            this.label26.Text = "DRC opacity";
            // 
            // groupBoxForSurfaceAnalysis
            // 
            this.groupBoxForSurfaceAnalysis.Controls.Add(this.numericUpDown3DThinPlateRegularization);
            this.groupBoxForSurfaceAnalysis.Controls.Add(this.label28);
            this.groupBoxForSurfaceAnalysis.Controls.Add(this.checkBox3DDisplayThinPlate);
            this.groupBoxForSurfaceAnalysis.Controls.Add(this.checkBox3DDisplayIsoRatioCurves);
            this.groupBoxForSurfaceAnalysis.Controls.Add(this.checkBox3DDisplayIsoboles);
            this.groupBoxForSurfaceAnalysis.Enabled = false;
            this.groupBoxForSurfaceAnalysis.Location = new System.Drawing.Point(5, 207);
            this.groupBoxForSurfaceAnalysis.Name = "groupBoxForSurfaceAnalysis";
            this.groupBoxForSurfaceAnalysis.Size = new System.Drawing.Size(255, 121);
            this.groupBoxForSurfaceAnalysis.TabIndex = 26;
            this.groupBoxForSurfaceAnalysis.TabStop = false;
            this.groupBoxForSurfaceAnalysis.Text = "                           ";
            // 
            // checkBox3DComputeThinPlate
            // 
            this.checkBox3DComputeThinPlate.AutoSize = true;
            this.checkBox3DComputeThinPlate.Location = new System.Drawing.Point(15, 207);
            this.checkBox3DComputeThinPlate.Name = "checkBox3DComputeThinPlate";
            this.checkBox3DComputeThinPlate.Size = new System.Drawing.Size(104, 17);
            this.checkBox3DComputeThinPlate.TabIndex = 22;
            this.checkBox3DComputeThinPlate.Text = "Surface Analysis";
            this.checkBox3DComputeThinPlate.UseVisualStyleBackColor = true;
            this.checkBox3DComputeThinPlate.CheckedChanged += new System.EventHandler(this.checkBox3DComputeThinPlate_CheckedChanged);
            // 
            // numericUpDown3DThinPlateRegularization
            // 
            this.numericUpDown3DThinPlateRegularization.DecimalPlaces = 2;
            this.numericUpDown3DThinPlateRegularization.Location = new System.Drawing.Point(134, 46);
            this.numericUpDown3DThinPlateRegularization.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown3DThinPlateRegularization.Name = "numericUpDown3DThinPlateRegularization";
            this.numericUpDown3DThinPlateRegularization.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown3DThinPlateRegularization.TabIndex = 5;
            this.numericUpDown3DThinPlateRegularization.Value = new decimal(new int[] {
            15,
            0,
            0,
            131072});
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(54, 48);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(74, 13);
            this.label28.TabIndex = 4;
            this.label28.Text = "Regularization";
            // 
            // checkBox3DDisplayThinPlate
            // 
            this.checkBox3DDisplayThinPlate.AutoSize = true;
            this.checkBox3DDisplayThinPlate.Checked = true;
            this.checkBox3DDisplayThinPlate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3DDisplayThinPlate.Location = new System.Drawing.Point(20, 24);
            this.checkBox3DDisplayThinPlate.Name = "checkBox3DDisplayThinPlate";
            this.checkBox3DDisplayThinPlate.Size = new System.Drawing.Size(100, 17);
            this.checkBox3DDisplayThinPlate.TabIndex = 4;
            this.checkBox3DDisplayThinPlate.Text = "Display Surface";
            this.checkBox3DDisplayThinPlate.UseVisualStyleBackColor = true;
            // 
            // checkBox3DDisplayIsoRatioCurves
            // 
            this.checkBox3DDisplayIsoRatioCurves.AutoSize = true;
            this.checkBox3DDisplayIsoRatioCurves.Checked = true;
            this.checkBox3DDisplayIsoRatioCurves.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3DDisplayIsoRatioCurves.Location = new System.Drawing.Point(19, 74);
            this.checkBox3DDisplayIsoRatioCurves.Name = "checkBox3DDisplayIsoRatioCurves";
            this.checkBox3DDisplayIsoRatioCurves.Size = new System.Drawing.Size(135, 17);
            this.checkBox3DDisplayIsoRatioCurves.TabIndex = 3;
            this.checkBox3DDisplayIsoRatioCurves.Text = "Display Iso ratio curves";
            this.checkBox3DDisplayIsoRatioCurves.UseVisualStyleBackColor = true;
            // 
            // checkBox3DDisplayIsoboles
            // 
            this.checkBox3DDisplayIsoboles.AutoSize = true;
            this.checkBox3DDisplayIsoboles.Checked = true;
            this.checkBox3DDisplayIsoboles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3DDisplayIsoboles.Location = new System.Drawing.Point(19, 97);
            this.checkBox3DDisplayIsoboles.Name = "checkBox3DDisplayIsoboles";
            this.checkBox3DDisplayIsoboles.Size = new System.Drawing.Size(102, 17);
            this.checkBox3DDisplayIsoboles.TabIndex = 3;
            this.checkBox3DDisplayIsoboles.Text = "Display Isoboles";
            this.checkBox3DDisplayIsoboles.UseVisualStyleBackColor = true;
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.checkBox3DPlateInformation);
            this.groupBox23.Location = new System.Drawing.Point(3, 3);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(257, 55);
            this.groupBox23.TabIndex = 23;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Plate Display";
            // 
            // checkBox3DPlateInformation
            // 
            this.checkBox3DPlateInformation.AutoSize = true;
            this.checkBox3DPlateInformation.Checked = true;
            this.checkBox3DPlateInformation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3DPlateInformation.Location = new System.Drawing.Point(11, 23);
            this.checkBox3DPlateInformation.Name = "checkBox3DPlateInformation";
            this.checkBox3DPlateInformation.Size = new System.Drawing.Size(142, 17);
            this.checkBox3DPlateInformation.TabIndex = 5;
            this.checkBox3DPlateInformation.Text = "Display Plate Information";
            this.checkBox3DPlateInformation.UseVisualStyleBackColor = true;
            // 
            // PanelForOptions3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForOptions3D";
            this.Size = new System.Drawing.Size(486, 568);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWellSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWellOpacity)).EndInit();
            this.groupBoxForDRC.ResumeLayout(false);
            this.groupBoxForDRC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDRCOpacity)).EndInit();
            this.groupBoxForSurfaceAnalysis.ResumeLayout(false);
            this.groupBoxForSurfaceAnalysis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3DThinPlateRegularization)).EndInit();
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        private System.Windows.Forms.GroupBox groupBox23;
        public System.Windows.Forms.CheckBox checkBox3DPlateInformation;
        private System.Windows.Forms.GroupBox groupBox21;
        public System.Windows.Forms.NumericUpDown numericUpDownWellSize;
        private System.Windows.Forms.Label label27;
        public System.Windows.Forms.NumericUpDown numericUpDownWellOpacity;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.GroupBox groupBoxForDRC;
        public System.Windows.Forms.CheckBox checkBoxDRC;
        public System.Windows.Forms.NumericUpDown numericUpDownDRCOpacity;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.GroupBox groupBoxForSurfaceAnalysis;
        public System.Windows.Forms.CheckBox checkBox3DComputeThinPlate;
        public System.Windows.Forms.NumericUpDown numericUpDown3DThinPlateRegularization;
        private System.Windows.Forms.Label label28;
        public System.Windows.Forms.CheckBox checkBox3DDisplayThinPlate;
        public System.Windows.Forms.CheckBox checkBox3DDisplayIsoRatioCurves;
        public System.Windows.Forms.CheckBox checkBox3DDisplayIsoboles;

    }
}
