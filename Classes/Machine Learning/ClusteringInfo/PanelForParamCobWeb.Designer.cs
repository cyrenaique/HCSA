namespace HCSAnalyzer.Forms.FormsForOptions.ClusteringInfo
{
    partial class PanelForParamCobWeb
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
            this.numericUpDownAcuity = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSeedNumber = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownCutOff = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTipForInfo = new System.Windows.Forms.ToolTip(this.components);
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAcuity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeedNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCutOff)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.numericUpDownCutOff);
            this.panel.Controls.Add(this.label4);
            this.panel.Controls.Add(this.numericUpDownAcuity);
            this.panel.Controls.Add(this.numericUpDownSeedNumber);
            this.panel.Controls.Add(this.label3);
            this.panel.Controls.Add(this.label2);
            this.panel.Location = new System.Drawing.Point(0, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(231, 264);
            this.panel.TabIndex = 1;
            // 
            // numericUpDownAcuity
            // 
            this.numericUpDownAcuity.DecimalPlaces = 1;
            this.numericUpDownAcuity.Increment = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.numericUpDownAcuity.Location = new System.Drawing.Point(78, 18);
            this.numericUpDownAcuity.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numericUpDownAcuity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            458752});
            this.numericUpDownAcuity.Name = "numericUpDownAcuity";
            this.numericUpDownAcuity.Size = new System.Drawing.Size(107, 20);
            this.numericUpDownAcuity.TabIndex = 8;
            this.toolTipForInfo.SetToolTip(this.numericUpDownAcuity, "Acuity: Set the minimum standard deviation for numeric attributes");
            this.numericUpDownAcuity.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            // 
            // numericUpDownSeedNumber
            // 
            this.numericUpDownSeedNumber.Location = new System.Drawing.Point(78, 79);
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
            this.numericUpDownSeedNumber.Size = new System.Drawing.Size(107, 20);
            this.numericUpDownSeedNumber.TabIndex = 9;
            this.numericUpDownSeedNumber.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Seed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Acuity";
            this.toolTipForInfo.SetToolTip(this.label2, "set the minimum standard deviation for numeric attributes");
            // 
            // numericUpDownCutOff
            // 
            this.numericUpDownCutOff.DecimalPlaces = 12;
            this.numericUpDownCutOff.Increment = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.numericUpDownCutOff.Location = new System.Drawing.Point(78, 48);
            this.numericUpDownCutOff.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numericUpDownCutOff.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            458752});
            this.numericUpDownCutOff.Name = "numericUpDownCutOff";
            this.numericUpDownCutOff.Size = new System.Drawing.Size(107, 20);
            this.numericUpDownCutOff.TabIndex = 11;
            this.toolTipForInfo.SetToolTip(this.numericUpDownCutOff, "Cutoff: Set the category utility threshold by which to prune nodes");
            this.numericUpDownCutOff.Value = new decimal(new int[] {
            833273639,
            6568031,
            0,
            1245184});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "CutOff";
            this.toolTipForInfo.SetToolTip(this.label4, "Set the category utility threshold by which to prune nodes");
            // 
            // PanelForParamCobWeb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamCobWeb";
            this.Size = new System.Drawing.Size(235, 271);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAcuity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeedNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCutOff)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        public System.Windows.Forms.NumericUpDown numericUpDownAcuity;
        public System.Windows.Forms.NumericUpDown numericUpDownSeedNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDownCutOff;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTipForInfo;
    }
}
