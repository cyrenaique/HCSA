namespace HCSAnalyzer.Forms.FormsForOptions.PanelForOptions
{
    partial class PanelForWellClassesColor
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
            this.groupBox28 = new System.Windows.Forms.GroupBox();
            this.panelForWellClasses = new System.Windows.Forms.Panel();
            this.panel.SuspendLayout();
            this.groupBox28.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.groupBox28);
            this.panel.Location = new System.Drawing.Point(17, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(269, 295);
            this.panel.TabIndex = 2;
            // 
            // groupBox28
            // 
            this.groupBox28.Controls.Add(this.panelForWellClasses);
            this.groupBox28.Location = new System.Drawing.Point(3, 3);
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.Size = new System.Drawing.Size(263, 288);
            this.groupBox28.TabIndex = 2;
            this.groupBox28.TabStop = false;
            this.groupBox28.Text = "Well Classes";
            // 
            // panelForWellClasses
            // 
            this.panelForWellClasses.Location = new System.Drawing.Point(6, 19);
            this.panelForWellClasses.Name = "panelForWellClasses";
            this.panelForWellClasses.Size = new System.Drawing.Size(251, 264);
            this.panelForWellClasses.TabIndex = 0;
            // 
            // PanelForWellClassesColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForWellClassesColor";
            this.Size = new System.Drawing.Size(303, 441);
            this.panel.ResumeLayout(false);
            this.groupBox28.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        private System.Windows.Forms.GroupBox groupBox28;
        public System.Windows.Forms.Panel panelForWellClasses;
    }
}
