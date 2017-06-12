namespace HCSAnalyzer.Forms.ClusteringForms
{
    partial class FormForEMInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForEMInfo));
            this.buttonOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownMaxIterations = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinStdev = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownSeedNumber = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinStdev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeedNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(78, 164);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(141, 32);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Maximum Iterations";
            // 
            // numericUpDownMaxIterations
            // 
            this.numericUpDownMaxIterations.Location = new System.Drawing.Point(147, 31);
            this.numericUpDownMaxIterations.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numericUpDownMaxIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxIterations.Name = "numericUpDownMaxIterations";
            this.numericUpDownMaxIterations.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownMaxIterations.TabIndex = 2;
            this.numericUpDownMaxIterations.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDownMinStdev
            // 
            this.numericUpDownMinStdev.DecimalPlaces = 7;
            this.numericUpDownMinStdev.Increment = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.numericUpDownMinStdev.Location = new System.Drawing.Point(147, 69);
            this.numericUpDownMinStdev.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numericUpDownMinStdev.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            458752});
            this.numericUpDownMinStdev.Name = "numericUpDownMinStdev";
            this.numericUpDownMinStdev.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownMinStdev.TabIndex = 2;
            this.numericUpDownMinStdev.Value = new decimal(new int[] {
            1,
            0,
            0,
            393216});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Minimum StdDev";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Seed Number";
            // 
            // numericUpDownSeedNumber
            // 
            this.numericUpDownSeedNumber.Location = new System.Drawing.Point(147, 109);
            this.numericUpDownSeedNumber.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numericUpDownSeedNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSeedNumber.Name = "numericUpDownSeedNumber";
            this.numericUpDownSeedNumber.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownSeedNumber.TabIndex = 2;
            this.numericUpDownSeedNumber.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // FormForEMInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 208);
            this.Controls.Add(this.numericUpDownMinStdev);
            this.Controls.Add(this.numericUpDownSeedNumber);
            this.Controls.Add(this.numericUpDownMaxIterations);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormForEMInfo";
            this.Text = "EM Options";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinStdev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeedNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDownMaxIterations;
        public System.Windows.Forms.NumericUpDown numericUpDownMinStdev;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown numericUpDownSeedNumber;
    }
}