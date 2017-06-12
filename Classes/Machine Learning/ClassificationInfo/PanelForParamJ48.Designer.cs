namespace HCSAnalyzer.Forms.FormsForOptions.PanelForOptions
{
    partial class PanelForParamJ48
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
            this.numericUpDownSeedNumber = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxLaplacianSmoothing = new System.Windows.Forms.CheckBox();
            this.checkBoxUnPruned = new System.Windows.Forms.CheckBox();
            this.numericUpDownNumFolds = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownConfFactor = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinInstLeaf = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxSubTreeRaising = new System.Windows.Forms.CheckBox();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeedNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumFolds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownConfFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinInstLeaf)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.checkBoxSubTreeRaising);
            this.panel.Controls.Add(this.numericUpDownSeedNumber);
            this.panel.Controls.Add(this.label4);
            this.panel.Controls.Add(this.checkBoxLaplacianSmoothing);
            this.panel.Controls.Add(this.checkBoxUnPruned);
            this.panel.Controls.Add(this.numericUpDownNumFolds);
            this.panel.Controls.Add(this.label3);
            this.panel.Controls.Add(this.numericUpDownConfFactor);
            this.panel.Controls.Add(this.numericUpDownMinInstLeaf);
            this.panel.Controls.Add(this.label2);
            this.panel.Controls.Add(this.label1);
            this.panel.Location = new System.Drawing.Point(3, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(200, 271);
            this.panel.TabIndex = 1;
            // 
            // numericUpDownSeedNumber
            // 
            this.numericUpDownSeedNumber.Location = new System.Drawing.Point(101, 203);
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
            this.numericUpDownSeedNumber.TabIndex = 11;
            this.toolTip.SetToolTip(this.numericUpDownSeedNumber, "Random seed value");
            this.numericUpDownSeedNumber.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Seed";
            this.toolTip.SetToolTip(this.label4, "Random seed value");
            // 
            // checkBoxLaplacianSmoothing
            // 
            this.checkBoxLaplacianSmoothing.AutoSize = true;
            this.checkBoxLaplacianSmoothing.Location = new System.Drawing.Point(15, 134);
            this.checkBoxLaplacianSmoothing.Name = "checkBoxLaplacianSmoothing";
            this.checkBoxLaplacianSmoothing.Size = new System.Drawing.Size(123, 17);
            this.checkBoxLaplacianSmoothing.TabIndex = 6;
            this.checkBoxLaplacianSmoothing.Text = "Laplacian smoothing";
            this.toolTip.SetToolTip(this.checkBoxLaplacianSmoothing, "Whether counts at leaves are smoothed based on Laplace");
            this.checkBoxLaplacianSmoothing.UseVisualStyleBackColor = true;
            // 
            // checkBoxUnPruned
            // 
            this.checkBoxUnPruned.AutoSize = true;
            this.checkBoxUnPruned.Location = new System.Drawing.Point(15, 111);
            this.checkBoxUnPruned.Name = "checkBoxUnPruned";
            this.checkBoxUnPruned.Size = new System.Drawing.Size(74, 17);
            this.checkBoxUnPruned.TabIndex = 5;
            this.checkBoxUnPruned.Text = "UnPruned";
            this.toolTip.SetToolTip(this.checkBoxUnPruned, "Whether pruning is performed");
            this.checkBoxUnPruned.UseVisualStyleBackColor = true;
            // 
            // numericUpDownNumFolds
            // 
            this.numericUpDownNumFolds.Location = new System.Drawing.Point(105, 69);
            this.numericUpDownNumFolds.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownNumFolds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNumFolds.Name = "numericUpDownNumFolds";
            this.numericUpDownNumFolds.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownNumFolds.TabIndex = 4;
            this.toolTip.SetToolTip(this.numericUpDownNumFolds, "Determines the amount of data used for reduced-error pruning. \r\nOne fold is used " +
                    "for pruning, the rest for growing the tree.");
            this.numericUpDownNumFolds.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Num. Folds";
            this.toolTip.SetToolTip(this.label3, "Determines the amount of data used for reduced-error pruning. \r\nOne fold is used " +
                    "for pruning, the rest for growing the tree.");
            // 
            // numericUpDownConfFactor
            // 
            this.numericUpDownConfFactor.DecimalPlaces = 2;
            this.numericUpDownConfFactor.Location = new System.Drawing.Point(105, 43);
            this.numericUpDownConfFactor.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownConfFactor.Name = "numericUpDownConfFactor";
            this.numericUpDownConfFactor.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownConfFactor.TabIndex = 2;
            this.toolTip.SetToolTip(this.numericUpDownConfFactor, "Confidence factor used for pruning (smaller values incur more pruning).");
            this.numericUpDownConfFactor.Value = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            // 
            // numericUpDownMinInstLeaf
            // 
            this.numericUpDownMinInstLeaf.Location = new System.Drawing.Point(105, 17);
            this.numericUpDownMinInstLeaf.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownMinInstLeaf.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinInstLeaf.Name = "numericUpDownMinInstLeaf";
            this.numericUpDownMinInstLeaf.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownMinInstLeaf.TabIndex = 1;
            this.toolTip.SetToolTip(this.numericUpDownMinInstLeaf, "Minimum number of instances per leaf.");
            this.numericUpDownMinInstLeaf.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Conf. Factor";
            this.toolTip.SetToolTip(this.label2, "Confidence factor used for pruning (smaller values incur more pruning).");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Min. Inst. Leaf";
            this.toolTip.SetToolTip(this.label1, "Minimum number of instances per leaf.");
            // 
            // checkBoxSubTreeRaising
            // 
            this.checkBoxSubTreeRaising.AutoSize = true;
            this.checkBoxSubTreeRaising.Checked = true;
            this.checkBoxSubTreeRaising.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSubTreeRaising.Location = new System.Drawing.Point(15, 157);
            this.checkBoxSubTreeRaising.Name = "checkBoxSubTreeRaising";
            this.checkBoxSubTreeRaising.Size = new System.Drawing.Size(101, 17);
            this.checkBoxSubTreeRaising.TabIndex = 12;
            this.checkBoxSubTreeRaising.Text = "Subtree Raising";
            this.toolTip.SetToolTip(this.checkBoxSubTreeRaising, "Whether to consider the subtree raising operation when pruning");
            this.checkBoxSubTreeRaising.UseVisualStyleBackColor = true;
            // 
            // PanelForParamJ48
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamJ48";
            this.Size = new System.Drawing.Size(207, 276);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeedNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumFolds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownConfFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinInstLeaf)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        public System.Windows.Forms.NumericUpDown numericUpDownMinInstLeaf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip;
        public System.Windows.Forms.NumericUpDown numericUpDownConfFactor;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDownNumFolds;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxUnPruned;
        private System.Windows.Forms.CheckBox checkBoxLaplacianSmoothing;
        public System.Windows.Forms.NumericUpDown numericUpDownSeedNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxSubTreeRaising;
    }
}
