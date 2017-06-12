namespace HCSAnalyzer.Forms.FormsForOptions.ClusteringInfo
{
    partial class PanelForParamKMeans
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxNormalize = new System.Windows.Forms.CheckBox();
            this.comboBoxDistance = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.numericUpDownSeedNumber = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMaxIterations = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownNumClasses = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTipForInfo = new System.Windows.Forms.ToolTip(this.components);
            this.panel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeedNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumClasses)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.groupBox1);
            this.panel.Controls.Add(this.numericUpDownSeedNumber);
            this.panel.Controls.Add(this.numericUpDownMaxIterations);
            this.panel.Controls.Add(this.label3);
            this.panel.Controls.Add(this.label4);
            this.panel.Controls.Add(this.numericUpDownNumClasses);
            this.panel.Controls.Add(this.label1);
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(200, 260);
            this.panel.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxNormalize);
            this.groupBox1.Controls.Add(this.comboBoxDistance);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Location = new System.Drawing.Point(4, 133);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 86);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Distance";
            this.toolTipForInfo.SetToolTip(this.groupBox1, "Distance function");
            // 
            // checkBoxNormalize
            // 
            this.checkBoxNormalize.AutoSize = true;
            this.checkBoxNormalize.Checked = true;
            this.checkBoxNormalize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNormalize.Location = new System.Drawing.Point(12, 56);
            this.checkBoxNormalize.Name = "checkBoxNormalize";
            this.checkBoxNormalize.Size = new System.Drawing.Size(72, 17);
            this.checkBoxNormalize.TabIndex = 19;
            this.checkBoxNormalize.Text = "Normalize";
            this.toolTipForInfo.SetToolTip(this.checkBoxNormalize, "Are the values normalized");
            this.checkBoxNormalize.UseVisualStyleBackColor = true;
            // 
            // comboBoxDistance
            // 
            this.comboBoxDistance.FormattingEnabled = true;
            this.comboBoxDistance.Items.AddRange(new object[] {
            "Euclidean",
            "Manhattan"});
            this.comboBoxDistance.Location = new System.Drawing.Point(75, 24);
            this.comboBoxDistance.Name = "comboBoxDistance";
            this.comboBoxDistance.Size = new System.Drawing.Size(102, 21);
            this.comboBoxDistance.TabIndex = 18;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 27);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(31, 13);
            this.label17.TabIndex = 17;
            this.label17.Text = "Type";
            // 
            // numericUpDownSeedNumber
            // 
            this.numericUpDownSeedNumber.Location = new System.Drawing.Point(79, 93);
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
            this.numericUpDownSeedNumber.Size = new System.Drawing.Size(102, 20);
            this.numericUpDownSeedNumber.TabIndex = 15;
            this.toolTipForInfo.SetToolTip(this.numericUpDownSeedNumber, "Random seed value");
            this.numericUpDownSeedNumber.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDownMaxIterations
            // 
            this.numericUpDownMaxIterations.Location = new System.Drawing.Point(79, 64);
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
            this.numericUpDownMaxIterations.Size = new System.Drawing.Size(102, 20);
            this.numericUpDownMaxIterations.TabIndex = 16;
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
            this.label3.Location = new System.Drawing.Point(12, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Seed";
            this.toolTipForInfo.SetToolTip(this.label3, "Random seed value");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Max It.";
            this.toolTipForInfo.SetToolTip(this.label4, "Maximum Iterations");
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
            this.numericUpDownNumClasses.Size = new System.Drawing.Size(102, 20);
            this.numericUpDownNumClasses.TabIndex = 12;
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
            this.label1.TabIndex = 11;
            this.label1.Text = "Classes";
            this.toolTipForInfo.SetToolTip(this.label1, "Number of classes");
            // 
            // PanelForParamKMeans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamKMeans";
            this.Size = new System.Drawing.Size(207, 266);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeedNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumClasses)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        public System.Windows.Forms.NumericUpDown numericUpDownSeedNumber;
        public System.Windows.Forms.NumericUpDown numericUpDownMaxIterations;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown numericUpDownNumClasses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTipForInfo;
        public System.Windows.Forms.ComboBox comboBoxDistance;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxNormalize;
    }
}
