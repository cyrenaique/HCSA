namespace HCSAnalyzer.Simulator.Forms.Panels
{
    partial class PanelForParamGeneral
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
            this.numericUpDownRunIterations = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxMemory = new System.Windows.Forms.CheckBox();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRunIterations)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.checkBoxMemory);
            this.panel.Controls.Add(this.numericUpDownRunIterations);
            this.panel.Controls.Add(this.label1);
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(171, 242);
            this.panel.TabIndex = 0;
            // 
            // numericUpDownRunIterations
            // 
            this.numericUpDownRunIterations.Location = new System.Drawing.Point(68, 15);
            this.numericUpDownRunIterations.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.numericUpDownRunIterations.Name = "numericUpDownRunIterations";
            this.numericUpDownRunIterations.Size = new System.Drawing.Size(84, 20);
            this.numericUpDownRunIterations.TabIndex = 4;
            this.numericUpDownRunIterations.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Iterations";
            // 
            // checkBoxMemory
            // 
            this.checkBoxMemory.AutoSize = true;
            this.checkBoxMemory.Location = new System.Drawing.Point(13, 63);
            this.checkBoxMemory.Name = "checkBoxMemory";
            this.checkBoxMemory.Size = new System.Drawing.Size(144, 17);
            this.checkBoxMemory.TabIndex = 6;
            this.checkBoxMemory.Text = "Keep previous cell states";
            this.checkBoxMemory.UseVisualStyleBackColor = true;
            // 
            // PanelForParamGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamGeneral";
            this.Size = new System.Drawing.Size(177, 248);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRunIterations)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        private System.Windows.Forms.NumericUpDown numericUpDownRunIterations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxMemory;
    }
}
