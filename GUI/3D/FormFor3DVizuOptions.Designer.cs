namespace HCSAnalyzer.Forms._3D
{
    partial class FormFor3DVizuOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFor3DVizuOptions));
            this.numericUpDownRadiusSphere = new System.Windows.Forms.NumericUpDown();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownFontSize = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownSphereOpacity = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageObjectProperties = new System.Windows.Forms.TabPage();
            this.tabPageLighting = new System.Windows.Forms.TabPage();
            this.radioButtonLightAutomated = new System.Windows.Forms.RadioButton();
            this.radioButtonLightManual = new System.Windows.Forms.RadioButton();
            this.panelManualLight = new System.Windows.Forms.Panel();
            this.checkBoxLightDisplaySource = new System.Windows.Forms.CheckBox();
            this.numericUpDownLightIntensity = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownLightAmbient = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLightDiffuse = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownLightSpecular = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadiusSphere)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSphereOpacity)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageObjectProperties.SuspendLayout();
            this.tabPageLighting.SuspendLayout();
            this.panelManualLight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLightIntensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLightAmbient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLightDiffuse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLightSpecular)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownRadiusSphere
            // 
            this.numericUpDownRadiusSphere.DecimalPlaces = 1;
            this.numericUpDownRadiusSphere.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownRadiusSphere.Location = new System.Drawing.Point(117, 42);
            this.numericUpDownRadiusSphere.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numericUpDownRadiusSphere.Name = "numericUpDownRadiusSphere";
            this.numericUpDownRadiusSphere.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownRadiusSphere.TabIndex = 5;
            this.numericUpDownRadiusSphere.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownRadiusSphere.ValueChanged += new System.EventHandler(this.numericUpDownRadiusSphere_ValueChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(129, 275);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(108, 27);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Sphere Radius";
            // 
            // numericUpDownFontSize
            // 
            this.numericUpDownFontSize.DecimalPlaces = 1;
            this.numericUpDownFontSize.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownFontSize.Location = new System.Drawing.Point(117, 16);
            this.numericUpDownFontSize.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numericUpDownFontSize.Name = "numericUpDownFontSize";
            this.numericUpDownFontSize.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownFontSize.TabIndex = 20;
            this.numericUpDownFontSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Font Size";
            // 
            // numericUpDownSphereOpacity
            // 
            this.numericUpDownSphereOpacity.DecimalPlaces = 1;
            this.numericUpDownSphereOpacity.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownSphereOpacity.Location = new System.Drawing.Point(117, 68);
            this.numericUpDownSphereOpacity.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSphereOpacity.Name = "numericUpDownSphereOpacity";
            this.numericUpDownSphereOpacity.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownSphereOpacity.TabIndex = 22;
            this.numericUpDownSphereOpacity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSphereOpacity.ValueChanged += new System.EventHandler(this.numericUpDownSphereOpacity_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Sphere Opacity";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageObjectProperties);
            this.tabControl1.Controls.Add(this.tabPageLighting);
            this.tabControl1.Location = new System.Drawing.Point(6, 7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(231, 262);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPageObjectProperties
            // 
            this.tabPageObjectProperties.Controls.Add(this.numericUpDownFontSize);
            this.tabPageObjectProperties.Controls.Add(this.numericUpDownSphereOpacity);
            this.tabPageObjectProperties.Controls.Add(this.label3);
            this.tabPageObjectProperties.Controls.Add(this.label1);
            this.tabPageObjectProperties.Controls.Add(this.numericUpDownRadiusSphere);
            this.tabPageObjectProperties.Controls.Add(this.label4);
            this.tabPageObjectProperties.Location = new System.Drawing.Point(4, 22);
            this.tabPageObjectProperties.Name = "tabPageObjectProperties";
            this.tabPageObjectProperties.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageObjectProperties.Size = new System.Drawing.Size(223, 236);
            this.tabPageObjectProperties.TabIndex = 0;
            this.tabPageObjectProperties.Text = "Objects";
            this.tabPageObjectProperties.UseVisualStyleBackColor = true;
            // 
            // tabPageLighting
            // 
            this.tabPageLighting.Controls.Add(this.panelManualLight);
            this.tabPageLighting.Controls.Add(this.radioButtonLightManual);
            this.tabPageLighting.Controls.Add(this.radioButtonLightAutomated);
            this.tabPageLighting.Location = new System.Drawing.Point(4, 22);
            this.tabPageLighting.Name = "tabPageLighting";
            this.tabPageLighting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLighting.Size = new System.Drawing.Size(223, 236);
            this.tabPageLighting.TabIndex = 1;
            this.tabPageLighting.Text = "Lighting";
            this.tabPageLighting.UseVisualStyleBackColor = true;
            // 
            // radioButtonLightAutomated
            // 
            this.radioButtonLightAutomated.AutoSize = true;
            this.radioButtonLightAutomated.Checked = true;
            this.radioButtonLightAutomated.Location = new System.Drawing.Point(16, 8);
            this.radioButtonLightAutomated.Name = "radioButtonLightAutomated";
            this.radioButtonLightAutomated.Size = new System.Drawing.Size(76, 17);
            this.radioButtonLightAutomated.TabIndex = 0;
            this.radioButtonLightAutomated.TabStop = true;
            this.radioButtonLightAutomated.Text = "Automated";
            this.radioButtonLightAutomated.UseVisualStyleBackColor = true;
            this.radioButtonLightAutomated.CheckedChanged += new System.EventHandler(this.radioButtonLightAutomated_CheckedChanged);
            // 
            // radioButtonLightManual
            // 
            this.radioButtonLightManual.AutoSize = true;
            this.radioButtonLightManual.Location = new System.Drawing.Point(16, 33);
            this.radioButtonLightManual.Name = "radioButtonLightManual";
            this.radioButtonLightManual.Size = new System.Drawing.Size(60, 17);
            this.radioButtonLightManual.TabIndex = 1;
            this.radioButtonLightManual.Text = "Manual";
            this.radioButtonLightManual.UseVisualStyleBackColor = true;
            this.radioButtonLightManual.CheckedChanged += new System.EventHandler(this.radioButtonLightManual_CheckedChanged);
            // 
            // panelManualLight
            // 
            this.panelManualLight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelManualLight.Controls.Add(this.numericUpDownLightSpecular);
            this.panelManualLight.Controls.Add(this.label7);
            this.panelManualLight.Controls.Add(this.numericUpDownLightDiffuse);
            this.panelManualLight.Controls.Add(this.label6);
            this.panelManualLight.Controls.Add(this.numericUpDownLightAmbient);
            this.panelManualLight.Controls.Add(this.label5);
            this.panelManualLight.Controls.Add(this.numericUpDownLightIntensity);
            this.panelManualLight.Controls.Add(this.label2);
            this.panelManualLight.Controls.Add(this.checkBoxLightDisplaySource);
            this.panelManualLight.Enabled = false;
            this.panelManualLight.Location = new System.Drawing.Point(13, 56);
            this.panelManualLight.Name = "panelManualLight";
            this.panelManualLight.Size = new System.Drawing.Size(200, 162);
            this.panelManualLight.TabIndex = 2;
            // 
            // checkBoxLightDisplaySource
            // 
            this.checkBoxLightDisplaySource.AutoSize = true;
            this.checkBoxLightDisplaySource.Location = new System.Drawing.Point(8, 8);
            this.checkBoxLightDisplaySource.Name = "checkBoxLightDisplaySource";
            this.checkBoxLightDisplaySource.Size = new System.Drawing.Size(97, 17);
            this.checkBoxLightDisplaySource.TabIndex = 0;
            this.checkBoxLightDisplaySource.Text = "Display Source";
            this.checkBoxLightDisplaySource.UseVisualStyleBackColor = true;
            // 
            // numericUpDownLightIntensity
            // 
            this.numericUpDownLightIntensity.DecimalPlaces = 1;
            this.numericUpDownLightIntensity.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownLightIntensity.Location = new System.Drawing.Point(105, 37);
            this.numericUpDownLightIntensity.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLightIntensity.Name = "numericUpDownLightIntensity";
            this.numericUpDownLightIntensity.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownLightIntensity.TabIndex = 24;
            this.numericUpDownLightIntensity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLightIntensity.ValueChanged += new System.EventHandler(this.numericUpDownLightIntensity_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Intensity";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Ambient";
            // 
            // numericUpDownLightAmbient
            // 
            this.numericUpDownLightAmbient.DecimalPlaces = 1;
            this.numericUpDownLightAmbient.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownLightAmbient.Location = new System.Drawing.Point(105, 66);
            this.numericUpDownLightAmbient.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLightAmbient.Name = "numericUpDownLightAmbient";
            this.numericUpDownLightAmbient.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownLightAmbient.TabIndex = 26;
            this.numericUpDownLightAmbient.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLightAmbient.ValueChanged += new System.EventHandler(this.numericUpDownLightAmbient_ValueChanged);
            // 
            // numericUpDownLightDiffuse
            // 
            this.numericUpDownLightDiffuse.DecimalPlaces = 1;
            this.numericUpDownLightDiffuse.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownLightDiffuse.Location = new System.Drawing.Point(105, 92);
            this.numericUpDownLightDiffuse.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLightDiffuse.Name = "numericUpDownLightDiffuse";
            this.numericUpDownLightDiffuse.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownLightDiffuse.TabIndex = 28;
            this.numericUpDownLightDiffuse.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLightDiffuse.ValueChanged += new System.EventHandler(this.numericUpDownLightDiffuse_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Diffuse";
            // 
            // numericUpDownLightSpecular
            // 
            this.numericUpDownLightSpecular.DecimalPlaces = 1;
            this.numericUpDownLightSpecular.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownLightSpecular.Location = new System.Drawing.Point(105, 118);
            this.numericUpDownLightSpecular.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLightSpecular.Name = "numericUpDownLightSpecular";
            this.numericUpDownLightSpecular.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownLightSpecular.TabIndex = 30;
            this.numericUpDownLightSpecular.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLightSpecular.ValueChanged += new System.EventHandler(this.numericUpDownLightSpecular_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Specular";
            // 
            // FormFor3DVizuOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 309);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFor3DVizuOptions";
            this.Text = "Options Visualization 3D";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadiusSphere)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSphereOpacity)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageObjectProperties.ResumeLayout(false);
            this.tabPageObjectProperties.PerformLayout();
            this.tabPageLighting.ResumeLayout(false);
            this.tabPageLighting.PerformLayout();
            this.panelManualLight.ResumeLayout(false);
            this.panelManualLight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLightIntensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLightAmbient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLightDiffuse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLightSpecular)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.NumericUpDown numericUpDownRadiusSphere;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown numericUpDownFontSize;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown numericUpDownSphereOpacity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageObjectProperties;
        private System.Windows.Forms.TabPage tabPageLighting;
        private System.Windows.Forms.Panel panelManualLight;
        private System.Windows.Forms.RadioButton radioButtonLightManual;
        public System.Windows.Forms.CheckBox checkBoxLightDisplaySource;
        public System.Windows.Forms.RadioButton radioButtonLightAutomated;
        public System.Windows.Forms.NumericUpDown numericUpDownLightSpecular;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown numericUpDownLightDiffuse;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.NumericUpDown numericUpDownLightAmbient;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.NumericUpDown numericUpDownLightIntensity;
        private System.Windows.Forms.Label label2;
    }
}