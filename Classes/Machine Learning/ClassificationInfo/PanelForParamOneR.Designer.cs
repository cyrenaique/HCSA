namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{
    partial class PanelForParamOneR
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
            this.numericUpDownMinBucketSize = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinBucketSize)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.numericUpDownMinBucketSize);
            this.panel.Controls.Add(this.label4);
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(200, 271);
            this.panel.TabIndex = 4;
            // 
            // numericUpDownMinBucketSize
            // 
            this.numericUpDownMinBucketSize.Location = new System.Drawing.Point(107, 25);
            this.numericUpDownMinBucketSize.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numericUpDownMinBucketSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinBucketSize.Name = "numericUpDownMinBucketSize";
            this.numericUpDownMinBucketSize.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownMinBucketSize.TabIndex = 11;
            this.toolTip.SetToolTip(this.numericUpDownMinBucketSize, "Minimum bucket size used for discretizing numeric attributes");
            this.numericUpDownMinBucketSize.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Min. Bucket Size";
            this.toolTip.SetToolTip(this.label4, "Minimum bucket size used for discretizing numeric attributes");
            // 
            // PanelForParamOneR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamOneR";
            this.Size = new System.Drawing.Size(209, 281);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinBucketSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        public System.Windows.Forms.NumericUpDown numericUpDownMinBucketSize;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label4;
    }
}
