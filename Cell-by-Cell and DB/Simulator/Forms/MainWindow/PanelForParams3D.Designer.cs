namespace HCSAnalyzer.Simulator.Forms.Panels
{
    partial class PanelForParams3D
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
            this.checkBoxDisplayText = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayCellPath = new System.Windows.Forms.CheckBox();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.checkBoxDisplayCellPath);
            this.panel.Controls.Add(this.checkBoxDisplayText);
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(175, 142);
            this.panel.TabIndex = 0;
            // 
            // checkBoxDisplayText
            // 
            this.checkBoxDisplayText.AutoSize = true;
            this.checkBoxDisplayText.Location = new System.Drawing.Point(32, 17);
            this.checkBoxDisplayText.Name = "checkBoxDisplayText";
            this.checkBoxDisplayText.Size = new System.Drawing.Size(118, 17);
            this.checkBoxDisplayText.TabIndex = 0;
            this.checkBoxDisplayText.Text = "Display Type Name";
            this.checkBoxDisplayText.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayCellPath
            // 
            this.checkBoxDisplayCellPath.AutoSize = true;
            this.checkBoxDisplayCellPath.Location = new System.Drawing.Point(32, 53);
            this.checkBoxDisplayCellPath.Name = "checkBoxDisplayCellPath";
            this.checkBoxDisplayCellPath.Size = new System.Drawing.Size(110, 17);
            this.checkBoxDisplayCellPath.TabIndex = 1;
            this.checkBoxDisplayCellPath.Text = "Display Cell Paths";
            this.checkBoxDisplayCellPath.UseVisualStyleBackColor = true;
            // 
            // PanelForParams3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParams3D";
            this.Size = new System.Drawing.Size(182, 152);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        private System.Windows.Forms.CheckBox checkBoxDisplayText;
        private System.Windows.Forms.CheckBox checkBoxDisplayCellPath;
    }
}
