namespace HCSAnalyzer.Classes.Machine_Learning.ClassificationInfo
{
    partial class PanelForParamLogistic
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
            this.checkBoxUseConjugateGradientDescent = new System.Windows.Forms.CheckBox();
            this.panel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownRidge = new System.Windows.Forms.NumericUpDown();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRidge)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxUseConjugateGradientDescent
            // 
            this.checkBoxUseConjugateGradientDescent.AutoSize = true;
            this.checkBoxUseConjugateGradientDescent.Location = new System.Drawing.Point(15, 65);
            this.checkBoxUseConjugateGradientDescent.Name = "checkBoxUseConjugateGradientDescent";
            this.checkBoxUseConjugateGradientDescent.Size = new System.Drawing.Size(160, 17);
            this.checkBoxUseConjugateGradientDescent.TabIndex = 0;
            this.checkBoxUseConjugateGradientDescent.Text = "Conjugate Gradient Descent";
            this.checkBoxUseConjugateGradientDescent.UseVisualStyleBackColor = true;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.label1);
            this.panel.Controls.Add(this.numericUpDownRidge);
            this.panel.Controls.Add(this.checkBoxUseConjugateGradientDescent);
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(196, 181);
            this.panel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ridge";
            // 
            // numericUpDownRidge
            // 
            this.numericUpDownRidge.DecimalPlaces = 9;
            this.numericUpDownRidge.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownRidge.Location = new System.Drawing.Point(56, 24);
            this.numericUpDownRidge.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownRidge.Name = "numericUpDownRidge";
            this.numericUpDownRidge.Size = new System.Drawing.Size(113, 20);
            this.numericUpDownRidge.TabIndex = 6;
            this.numericUpDownRidge.Value = new decimal(new int[] {
            1,
            0,
            0,
            524288});
            // 
            // PanelForParamLogistic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamLogistic";
            this.Size = new System.Drawing.Size(243, 283);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRidge)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxUseConjugateGradientDescent;
        public System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDownRidge;
    }
}
