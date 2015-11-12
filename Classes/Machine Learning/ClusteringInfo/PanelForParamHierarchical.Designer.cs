namespace HCSAnalyzer.Forms.FormsForOptions.ClusteringInfo
{
    partial class PanelForParamHierarchical
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
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxDistance = new System.Windows.Forms.ComboBox();
            this.comboBoxLinkType = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.numericUpDownNumClasses = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTipForInfo = new System.Windows.Forms.ToolTip(this.components);
            this.panel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumClasses)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.groupBox1);
            this.panel.Controls.Add(this.comboBoxLinkType);
            this.panel.Controls.Add(this.label18);
            this.panel.Controls.Add(this.numericUpDownNumClasses);
            this.panel.Controls.Add(this.label1);
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(200, 271);
            this.panel.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxNormalize);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBoxDistance);
            this.groupBox1.Location = new System.Drawing.Point(4, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 86);
            this.groupBox1.TabIndex = 21;
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
            this.checkBoxNormalize.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Type";
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
            this.comboBoxDistance.TabIndex = 3;
            // 
            // comboBoxLinkType
            // 
            this.comboBoxLinkType.FormattingEnabled = true;
            this.comboBoxLinkType.Items.AddRange(new object[] {
            "SINGLE",
            "COMPLETE",
            "AVERAGE",
            "MEAN",
            "CENTROID",
            "WARD",
            "ADJCOMLPETE"});
            this.comboBoxLinkType.Location = new System.Drawing.Point(79, 161);
            this.comboBoxLinkType.Name = "comboBoxLinkType";
            this.comboBoxLinkType.Size = new System.Drawing.Size(102, 21);
            this.comboBoxLinkType.TabIndex = 5;
            this.toolTipForInfo.SetToolTip(this.comboBoxLinkType, "Link type");
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(11, 164);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 13);
            this.label18.TabIndex = 4;
            this.label18.Text = "Link type";
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
            this.numericUpDownNumClasses.TabIndex = 1;
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
            this.label1.TabIndex = 0;
            this.label1.Text = "Classes";
            this.toolTipForInfo.SetToolTip(this.label1, "Number of classes");
            // 
            // PanelForParamHierarchical
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamHierarchical";
            this.Size = new System.Drawing.Size(209, 282);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumClasses)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDownNumClasses;
        public System.Windows.Forms.Panel panel;
        public System.Windows.Forms.ComboBox comboBoxLinkType;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.ComboBox comboBoxDistance;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxNormalize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTipForInfo;
    }
}
