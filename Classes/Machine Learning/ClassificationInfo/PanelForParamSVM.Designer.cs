namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{
    partial class PanelForParamSVM
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
            this.textBoxForKernelType = new System.Windows.Forms.TextBox();
            this.buttonEditKernel = new System.Windows.Forms.Button();
            this.numericUpDownSeed = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownC = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownC)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.textBoxForKernelType);
            this.panel.Controls.Add(this.buttonEditKernel);
            this.panel.Controls.Add(this.numericUpDownSeed);
            this.panel.Controls.Add(this.label2);
            this.panel.Controls.Add(this.numericUpDownC);
            this.panel.Controls.Add(this.label3);
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(233, 207);
            this.panel.TabIndex = 4;
            // 
            // textBoxForKernelType
            // 
            this.textBoxForKernelType.Location = new System.Drawing.Point(83, 50);
            this.textBoxForKernelType.Name = "textBoxForKernelType";
            this.textBoxForKernelType.ReadOnly = true;
            this.textBoxForKernelType.Size = new System.Drawing.Size(134, 20);
            this.textBoxForKernelType.TabIndex = 9;
            this.textBoxForKernelType.Text = "Polynomial";
            this.toolTip.SetToolTip(this.textBoxForKernelType, "Kernel type");
            // 
            // buttonEditKernel
            // 
            this.buttonEditKernel.Location = new System.Drawing.Point(16, 48);
            this.buttonEditKernel.Name = "buttonEditKernel";
            this.buttonEditKernel.Size = new System.Drawing.Size(54, 23);
            this.buttonEditKernel.TabIndex = 8;
            this.buttonEditKernel.Text = "Kernel";
            this.toolTip.SetToolTip(this.buttonEditKernel, "Kernel type");
            this.buttonEditKernel.UseVisualStyleBackColor = true;
            this.buttonEditKernel.Click += new System.EventHandler(this.buttonEditKernel_Click);
            // 
            // numericUpDownSeed
            // 
            this.numericUpDownSeed.Location = new System.Drawing.Point(83, 124);
            this.numericUpDownSeed.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownSeed.Name = "numericUpDownSeed";
            this.numericUpDownSeed.Size = new System.Drawing.Size(134, 20);
            this.numericUpDownSeed.TabIndex = 7;
            this.toolTip.SetToolTip(this.numericUpDownSeed, "Random seed value");
            this.numericUpDownSeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Seed";
            this.toolTip.SetToolTip(this.label2, "Random seed value");
            // 
            // numericUpDownC
            // 
            this.numericUpDownC.DecimalPlaces = 1;
            this.numericUpDownC.Location = new System.Drawing.Point(83, 18);
            this.numericUpDownC.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownC.Name = "numericUpDownC";
            this.numericUpDownC.Size = new System.Drawing.Size(134, 20);
            this.numericUpDownC.TabIndex = 4;
            this.toolTip.SetToolTip(this.numericUpDownC, "Complexity parameter");
            this.numericUpDownC.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Complexity";
            this.toolTip.SetToolTip(this.label3, "Complexity parameter");
            // 
            // PanelForParamSVM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamSVM";
            this.Size = new System.Drawing.Size(239, 214);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        public System.Windows.Forms.NumericUpDown numericUpDownC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip;
        public System.Windows.Forms.NumericUpDown numericUpDownSeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxForKernelType;
        private System.Windows.Forms.Button buttonEditKernel;
    }
}
