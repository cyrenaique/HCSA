namespace HCSAnalyzer.Simulator.Forms.Panels
{
    partial class PanelForParamWorldDimensions
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
            this.panel = new System.Windows.Forms.Panel();
            this.numericUpDownWorldDimensionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWorldDimensionX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWorldDimensionZ = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWorldDimensionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWorldDimensionX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWorldDimensionZ)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.label3);
            this.panel.Controls.Add(this.label2);
            this.panel.Controls.Add(this.label1);
            this.panel.Controls.Add(this.numericUpDownWorldDimensionY);
            this.panel.Controls.Add(this.numericUpDownWorldDimensionX);
            this.panel.Controls.Add(this.numericUpDownWorldDimensionZ);
            this.panel.Location = new System.Drawing.Point(1, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(187, 238);
            this.panel.TabIndex = 0;
            // 
            // numericUpDownWorldDimensionY
            // 
            this.numericUpDownWorldDimensionY.Location = new System.Drawing.Point(52, 39);
            this.numericUpDownWorldDimensionY.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDownWorldDimensionY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWorldDimensionY.Name = "numericUpDownWorldDimensionY";
            this.numericUpDownWorldDimensionY.Size = new System.Drawing.Size(109, 20);
            this.numericUpDownWorldDimensionY.TabIndex = 1;
            this.numericUpDownWorldDimensionY.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            // 
            // numericUpDownWorldDimensionX
            // 
            this.numericUpDownWorldDimensionX.Location = new System.Drawing.Point(52, 13);
            this.numericUpDownWorldDimensionX.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDownWorldDimensionX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWorldDimensionX.Name = "numericUpDownWorldDimensionX";
            this.numericUpDownWorldDimensionX.Size = new System.Drawing.Size(109, 20);
            this.numericUpDownWorldDimensionX.TabIndex = 0;
            this.numericUpDownWorldDimensionX.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            // 
            // numericUpDownWorldDimensionZ
            // 
            this.numericUpDownWorldDimensionZ.Location = new System.Drawing.Point(52, 65);
            this.numericUpDownWorldDimensionZ.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDownWorldDimensionZ.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWorldDimensionZ.Name = "numericUpDownWorldDimensionZ";
            this.numericUpDownWorldDimensionZ.Size = new System.Drawing.Size(109, 20);
            this.numericUpDownWorldDimensionZ.TabIndex = 2;
            this.numericUpDownWorldDimensionZ.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Height";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Depth";
            // 
            // PanelForParamWorldDimensions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamWorldDimensions";
            this.Size = new System.Drawing.Size(188, 241);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWorldDimensionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWorldDimensionX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWorldDimensionZ)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownWorldDimensionZ;
        private System.Windows.Forms.NumericUpDown numericUpDownWorldDimensionY;
        private System.Windows.Forms.NumericUpDown numericUpDownWorldDimensionX;
        public System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
