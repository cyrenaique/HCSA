namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{
    partial class PanelForParamRandomForest
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
            this.numericUpDownSeed = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownNumTrees = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMaxDepth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxMaxDepthUnlimited = new System.Windows.Forms.CheckBox();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumTrees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxDepth)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.groupBox1);
            this.panel.Controls.Add(this.numericUpDownSeed);
            this.panel.Controls.Add(this.label4);
            this.panel.Controls.Add(this.numericUpDownNumTrees);
            this.panel.Controls.Add(this.label2);
            this.panel.Location = new System.Drawing.Point(3, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(200, 271);
            this.panel.TabIndex = 2;
            // 
            // numericUpDownSeed
            // 
            this.numericUpDownSeed.Location = new System.Drawing.Point(101, 136);
            this.numericUpDownSeed.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numericUpDownSeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSeed.Name = "numericUpDownSeed";
            this.numericUpDownSeed.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownSeed.TabIndex = 11;
            this.numericUpDownSeed.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Seed";
            // 
            // numericUpDownNumTrees
            // 
            this.numericUpDownNumTrees.Location = new System.Drawing.Point(101, 100);
            this.numericUpDownNumTrees.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownNumTrees.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNumTrees.Name = "numericUpDownNumTrees";
            this.numericUpDownNumTrees.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownNumTrees.TabIndex = 4;
            this.toolTip.SetToolTip(this.numericUpDownNumTrees, "Number of trees to be generated");
            this.numericUpDownNumTrees.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericUpDownMaxDepth
            // 
            this.numericUpDownMaxDepth.Enabled = false;
            this.numericUpDownMaxDepth.Location = new System.Drawing.Point(98, 24);
            this.numericUpDownMaxDepth.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownMaxDepth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxDepth.Name = "numericUpDownMaxDepth";
            this.numericUpDownMaxDepth.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownMaxDepth.TabIndex = 1;
            this.numericUpDownMaxDepth.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Num. Trees";
            this.toolTip.SetToolTip(this.label2, "Number of trees to be generated");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxMaxDepthUnlimited);
            this.groupBox1.Controls.Add(this.numericUpDownMaxDepth);
            this.groupBox1.Location = new System.Drawing.Point(3, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 61);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Maximum Depth";
            this.toolTip.SetToolTip(this.groupBox1, "Maximum depth of the trees");
            // 
            // checkBoxMaxDepthUnlimited
            // 
            this.checkBoxMaxDepthUnlimited.AutoSize = true;
            this.checkBoxMaxDepthUnlimited.Checked = true;
            this.checkBoxMaxDepthUnlimited.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMaxDepthUnlimited.Location = new System.Drawing.Point(16, 25);
            this.checkBoxMaxDepthUnlimited.Name = "checkBoxMaxDepthUnlimited";
            this.checkBoxMaxDepthUnlimited.Size = new System.Drawing.Size(69, 17);
            this.checkBoxMaxDepthUnlimited.TabIndex = 2;
            this.checkBoxMaxDepthUnlimited.Text = "Unlimited";
            this.checkBoxMaxDepthUnlimited.UseVisualStyleBackColor = true;
            this.checkBoxMaxDepthUnlimited.CheckedChanged += new System.EventHandler(this.checkBoxMaxDepthUnlimited_CheckedChanged);
            // 
            // PanelForParamRandomForest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamRandomForest";
            this.Size = new System.Drawing.Size(209, 277);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumTrees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxDepth)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        public System.Windows.Forms.NumericUpDown numericUpDownSeed;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown numericUpDownNumTrees;
        public System.Windows.Forms.NumericUpDown numericUpDownMaxDepth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxMaxDepthUnlimited;
    }
}
