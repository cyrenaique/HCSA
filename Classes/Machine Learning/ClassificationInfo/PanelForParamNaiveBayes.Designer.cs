namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{
    partial class PanelForParamNaiveBayes
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
            this.checkBoxKernelEstimator = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.checkBoxKernelEstimator);
            this.panel.Location = new System.Drawing.Point(3, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(200, 271);
            this.panel.TabIndex = 5;
            this.toolTip.SetToolTip(this.panel, "Use a kernel estimator for numeric attributes rather than a normal distribution.");
            // 
            // checkBoxKernelEstimator
            // 
            this.checkBoxKernelEstimator.AutoSize = true;
            this.checkBoxKernelEstimator.Location = new System.Drawing.Point(40, 26);
            this.checkBoxKernelEstimator.Name = "checkBoxKernelEstimator";
            this.checkBoxKernelEstimator.Size = new System.Drawing.Size(124, 17);
            this.checkBoxKernelEstimator.TabIndex = 0;
            this.checkBoxKernelEstimator.Text = "Use Kernel Estimator";
            this.checkBoxKernelEstimator.UseVisualStyleBackColor = true;
            // 
            // PanelForParamNaiveBayes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamNaiveBayes";
            this.Size = new System.Drawing.Size(209, 275);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        private System.Windows.Forms.CheckBox checkBoxKernelEstimator;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
