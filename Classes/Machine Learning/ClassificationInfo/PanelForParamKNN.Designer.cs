namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{
    partial class PanelForParamKNN
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
            this.comboBoxDistanceWeight = new System.Windows.Forms.ComboBox();
            this.numericUpDownKNN = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxNormalize = new System.Windows.Forms.CheckBox();
            this.comboBoxDistance = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKNN)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.groupBox1);
            this.panel.Controls.Add(this.comboBoxDistanceWeight);
            this.panel.Controls.Add(this.numericUpDownKNN);
            this.panel.Controls.Add(this.label1);
            this.panel.Controls.Add(this.label3);
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(200, 271);
            this.panel.TabIndex = 4;
            // 
            // comboBoxDistanceWeight
            // 
            this.comboBoxDistanceWeight.FormattingEnabled = true;
            this.comboBoxDistanceWeight.Items.AddRange(new object[] {
            "No Weighting",
            "1/Distance",
            "1-Distance"});
            this.comboBoxDistanceWeight.Location = new System.Drawing.Point(80, 64);
            this.comboBoxDistanceWeight.Name = "comboBoxDistanceWeight";
            this.comboBoxDistanceWeight.Size = new System.Drawing.Size(104, 21);
            this.comboBoxDistanceWeight.TabIndex = 5;
            this.toolTip.SetToolTip(this.comboBoxDistanceWeight, "Distance weighting method used");
            // 
            // numericUpDownKNN
            // 
            this.numericUpDownKNN.Location = new System.Drawing.Point(80, 19);
            this.numericUpDownKNN.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownKNN.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownKNN.Name = "numericUpDownKNN";
            this.numericUpDownKNN.Size = new System.Drawing.Size(104, 20);
            this.numericUpDownKNN.TabIndex = 4;
            this.toolTip.SetToolTip(this.numericUpDownKNN, "Number of neighbours");
            this.numericUpDownKNN.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Dist. Weight.";
            this.toolTip.SetToolTip(this.label1, "Distance weighting method used");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "KNN";
            this.toolTip.SetToolTip(this.label3, "Number of neighbours");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxNormalize);
            this.groupBox1.Controls.Add(this.comboBoxDistance);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Location = new System.Drawing.Point(4, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 86);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Distance";
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
            this.checkBoxNormalize.UseVisualStyleBackColor = true;
            // 
            // comboBoxDistance
            // 
            this.comboBoxDistance.FormattingEnabled = true;
            this.comboBoxDistance.Items.AddRange(new object[] {
            "Euclidean",
            "Manhattan",
            "Chebyshev"});
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
            // PanelForParamKNN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamKNN";
            this.Size = new System.Drawing.Size(209, 279);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKNN)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        public System.Windows.Forms.NumericUpDown numericUpDownKNN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ComboBox comboBoxDistanceWeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxNormalize;
        public System.Windows.Forms.ComboBox comboBoxDistance;
        private System.Windows.Forms.Label label17;
    }
}
