namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{
    partial class PanelForParamRandomTree
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
            this.checkBoxMaxDepthUnlimited = new System.Windows.Forms.CheckBox();
            this.numericUpDownMaxDepth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSeed = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.numericUpDownMinWeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxIsBackfitting = new System.Windows.Forms.CheckBox();
            this.numericUpDownBackFittingFolds = new System.Windows.Forms.NumericUpDown();
            this.panel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinWeight)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBackFittingFolds)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.groupBox2);
            this.panel.Controls.Add(this.numericUpDownMinWeight);
            this.panel.Controls.Add(this.label1);
            this.panel.Controls.Add(this.groupBox1);
            this.panel.Controls.Add(this.numericUpDownSeed);
            this.panel.Controls.Add(this.label4);
            this.panel.Location = new System.Drawing.Point(3, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(200, 271);
            this.panel.TabIndex = 3;
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
            this.toolTip.SetToolTip(this.groupBox1, "Maximum depth of the tree");
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
            // numericUpDownSeed
            // 
            this.numericUpDownSeed.Location = new System.Drawing.Point(101, 217);
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
            this.toolTip.SetToolTip(this.numericUpDownSeed, "Random seed value");
            this.numericUpDownSeed.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Seed";
            this.toolTip.SetToolTip(this.label4, "Random seed value");
            // 
            // numericUpDownMinWeight
            // 
            this.numericUpDownMinWeight.DecimalPlaces = 1;
            this.numericUpDownMinWeight.Location = new System.Drawing.Point(101, 97);
            this.numericUpDownMinWeight.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numericUpDownMinWeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinWeight.Name = "numericUpDownMinWeight";
            this.numericUpDownMinWeight.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownMinWeight.TabIndex = 14;
            this.toolTip.SetToolTip(this.numericUpDownMinWeight, "Minimum total weight of the instances in a leaf");
            this.numericUpDownMinWeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Min. Weight";
            this.toolTip.SetToolTip(this.label1, "Minimum total weight of the instances in a leaf");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxIsBackfitting);
            this.groupBox2.Controls.Add(this.numericUpDownBackFittingFolds);
            this.groupBox2.Location = new System.Drawing.Point(3, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 61);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Backfitting folds";
            this.toolTip.SetToolTip(this.groupBox2, "Amount of data used for backfitting. \r\nOne fold is used for backfitting, the rest" +
                    " for growing the tree. \r\n");
            // 
            // checkBoxIsBackfitting
            // 
            this.checkBoxIsBackfitting.AutoSize = true;
            this.checkBoxIsBackfitting.Location = new System.Drawing.Point(16, 25);
            this.checkBoxIsBackfitting.Name = "checkBoxIsBackfitting";
            this.checkBoxIsBackfitting.Size = new System.Drawing.Size(56, 17);
            this.checkBoxIsBackfitting.TabIndex = 2;
            this.checkBoxIsBackfitting.Text = "Active";
            this.checkBoxIsBackfitting.UseVisualStyleBackColor = true;
            this.checkBoxIsBackfitting.CheckedChanged += new System.EventHandler(this.checkBoxIsBackfitting_CheckedChanged);
            // 
            // numericUpDownBackFittingFolds
            // 
            this.numericUpDownBackFittingFolds.Enabled = false;
            this.numericUpDownBackFittingFolds.Location = new System.Drawing.Point(98, 24);
            this.numericUpDownBackFittingFolds.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownBackFittingFolds.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownBackFittingFolds.Name = "numericUpDownBackFittingFolds";
            this.numericUpDownBackFittingFolds.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownBackFittingFolds.TabIndex = 1;
            this.numericUpDownBackFittingFolds.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // PanelForParamRandomTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamRandomTree";
            this.Size = new System.Drawing.Size(206, 275);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinWeight)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBackFittingFolds)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxMaxDepthUnlimited;
        public System.Windows.Forms.NumericUpDown numericUpDownMaxDepth;
        public System.Windows.Forms.NumericUpDown numericUpDownSeed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip;
        public System.Windows.Forms.NumericUpDown numericUpDownMinWeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxIsBackfitting;
        public System.Windows.Forms.NumericUpDown numericUpDownBackFittingFolds;
    }
}
