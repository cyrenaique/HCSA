namespace IM3_Plugin3
{
    partial class FormForSpotDetection
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
            this.numericUpDownSpotLocality = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownSpotRadius = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownSphereDisplayRadius = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDownIntensityThreshold = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpotLocality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpotRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSphereDisplayRadius)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntensityThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOk.Location = new System.Drawing.Point(134, 253);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(134, 23);
            this.buttonOk.TabIndex = 6;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // numericUpDownSpotLocality
            // 
            this.numericUpDownSpotLocality.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownSpotLocality.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownSpotLocality.Location = new System.Drawing.Point(152, 102);
            this.numericUpDownSpotLocality.Name = "numericUpDownSpotLocality";
            this.numericUpDownSpotLocality.Size = new System.Drawing.Size(87, 20);
            this.numericUpDownSpotLocality.TabIndex = 10;
            this.numericUpDownSpotLocality.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(78, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "Locality";
            // 
            // numericUpDownSpotRadius
            // 
            this.numericUpDownSpotRadius.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownSpotRadius.DecimalPlaces = 2;
            this.numericUpDownSpotRadius.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownSpotRadius.Location = new System.Drawing.Point(152, 71);
            this.numericUpDownSpotRadius.Name = "numericUpDownSpotRadius";
            this.numericUpDownSpotRadius.Size = new System.Drawing.Size(87, 20);
            this.numericUpDownSpotRadius.TabIndex = 8;
            this.numericUpDownSpotRadius.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(84, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Radius";
            // 
            // numericUpDownSphereDisplayRadius
            // 
            this.numericUpDownSphereDisplayRadius.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownSphereDisplayRadius.DecimalPlaces = 1;
            this.numericUpDownSphereDisplayRadius.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownSphereDisplayRadius.Location = new System.Drawing.Point(126, 19);
            this.numericUpDownSphereDisplayRadius.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDownSphereDisplayRadius.Name = "numericUpDownSphereDisplayRadius";
            this.numericUpDownSphereDisplayRadius.Size = new System.Drawing.Size(88, 20);
            this.numericUpDownSphereDisplayRadius.TabIndex = 12;
            this.numericUpDownSphereDisplayRadius.Value = new decimal(new int[] {
            6,
            0,
            0,
            65536});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "Sphere Radius";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownIntensityThreshold);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numericUpDownSpotRadius);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.numericUpDownSpotLocality);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Location = new System.Drawing.Point(7, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 150);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detection";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDownSphereDisplayRadius);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Location = new System.Drawing.Point(7, 178);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(260, 56);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Display";
            // 
            // numericUpDownIntensityThreshold
            // 
            this.numericUpDownIntensityThreshold.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownIntensityThreshold.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownIntensityThreshold.Location = new System.Drawing.Point(151, 34);
            this.numericUpDownIntensityThreshold.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownIntensityThreshold.Name = "numericUpDownIntensityThreshold";
            this.numericUpDownIntensityThreshold.Size = new System.Drawing.Size(88, 20);
            this.numericUpDownIntensityThreshold.TabIndex = 29;
            this.numericUpDownIntensityThreshold.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(16, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 12);
            this.label3.TabIndex = 28;
            this.label3.Text = "Intensity Threshold";
            // 
            // FormForSpotDetection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(280, 288);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOk);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FormForSpotDetection";
            this.Text = "Spot Detection";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpotLocality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpotRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSphereDisplayRadius)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntensityThreshold)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.NumericUpDown numericUpDownSpotLocality;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown numericUpDownSpotRadius;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDownSphereDisplayRadius;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.NumericUpDown numericUpDownIntensityThreshold;
        private System.Windows.Forms.Label label3;
    }
}