namespace HCSAnalyzer.Forms.FormsForOptions.ClusteringInfo
{
    partial class PanelForParamEM
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
            this.panel = new System.Windows.Forms.Panel();
            this.numericUpDownMinStdev = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSeedNumber = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMaxIterations = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxAutomatedClassNum = new System.Windows.Forms.CheckBox();
            this.numericUpDownNumClasses = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTipForInfo = new System.Windows.Forms.ToolTip(this.components);
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinStdev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeedNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumClasses)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.numericUpDownMinStdev);
            this.panel.Controls.Add(this.numericUpDownSeedNumber);
            this.panel.Controls.Add(this.numericUpDownMaxIterations);
            this.panel.Controls.Add(this.label3);
            this.panel.Controls.Add(this.label2);
            this.panel.Controls.Add(this.label4);
            this.panel.Controls.Add(this.checkBoxAutomatedClassNum);
            this.panel.Controls.Add(this.numericUpDownNumClasses);
            this.panel.Controls.Add(this.label1);
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(231, 264);
            this.panel.TabIndex = 0;
            // 
            // numericUpDownMinStdev
            // 
            this.numericUpDownMinStdev.DecimalPlaces = 7;
            this.numericUpDownMinStdev.Increment = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.numericUpDownMinStdev.Location = new System.Drawing.Point(78, 92);
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
            this.numericUpDownMinStdev.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownMinStdev.TabIndex = 8;
            this.toolTipForInfo.SetToolTip(this.numericUpDownMinStdev, "Minimum standard deviation");
            this.numericUpDownMinStdev.Value = new decimal(new int[] {
            1,
            0,
            0,
            393216});
            // 
            // numericUpDownSeedNumber
            // 
            this.numericUpDownSeedNumber.Location = new System.Drawing.Point(78, 123);
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
            this.numericUpDownSeedNumber.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownSeedNumber.TabIndex = 9;
            this.toolTipForInfo.SetToolTip(this.numericUpDownSeedNumber, "Random seed value");
            this.numericUpDownSeedNumber.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDownMaxIterations
            // 
            this.numericUpDownMaxIterations.Location = new System.Drawing.Point(78, 62);
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
            this.numericUpDownMaxIterations.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownMaxIterations.TabIndex = 10;
            this.toolTipForInfo.SetToolTip(this.numericUpDownMaxIterations, "Maximum Iterations");
            this.numericUpDownMaxIterations.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Seed";
            this.toolTipForInfo.SetToolTip(this.label3, "Random seed value");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Min. Stdv";
            this.toolTipForInfo.SetToolTip(this.label2, "Minimum standard deviation");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Max It.";
            this.toolTipForInfo.SetToolTip(this.label4, "Maximum Iterations");
            // 
            // checkBoxAutomatedClassNum
            // 
            this.checkBoxAutomatedClassNum.AutoSize = true;
            this.checkBoxAutomatedClassNum.Location = new System.Drawing.Point(170, 19);
            this.checkBoxAutomatedClassNum.Name = "checkBoxAutomatedClassNum";
            this.checkBoxAutomatedClassNum.Size = new System.Drawing.Size(48, 17);
            this.checkBoxAutomatedClassNum.TabIndex = 4;
            this.checkBoxAutomatedClassNum.Text = "Auto";
            this.toolTipForInfo.SetToolTip(this.checkBoxAutomatedClassNum, "The class number will be automatically estimated.\r\n\r\nWarning: this process is tim" +
                    "e consuming, and\r\ncan result in a class number higher the number\r\nof phenotypes " +
                    "allowed.");
            this.checkBoxAutomatedClassNum.UseVisualStyleBackColor = true;
            this.checkBoxAutomatedClassNum.CheckedChanged += new System.EventHandler(this.checkBoxAutomatedClassNum_CheckedChanged);
            // 
            // numericUpDownNumClasses
            // 
            this.numericUpDownNumClasses.Location = new System.Drawing.Point(79, 17);
            this.numericUpDownNumClasses.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownNumClasses.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNumClasses.Name = "numericUpDownNumClasses";
            this.numericUpDownNumClasses.Size = new System.Drawing.Size(79, 20);
            this.numericUpDownNumClasses.TabIndex = 3;
            this.toolTipForInfo.SetToolTip(this.numericUpDownNumClasses, "Number of classes");
            this.numericUpDownNumClasses.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Classes";
            this.toolTipForInfo.SetToolTip(this.label1, "Number of classes");
            // 
            // PanelForParamEM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamEM";
            this.Size = new System.Drawing.Size(238, 271);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinStdev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeedNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumClasses)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        public System.Windows.Forms.NumericUpDown numericUpDownNumClasses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxAutomatedClassNum;
        private System.Windows.Forms.ToolTip toolTipForInfo;
        public System.Windows.Forms.NumericUpDown numericUpDownMinStdev;
        public System.Windows.Forms.NumericUpDown numericUpDownSeedNumber;
        public System.Windows.Forms.NumericUpDown numericUpDownMaxIterations;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}
