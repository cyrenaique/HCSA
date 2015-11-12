namespace HCSAnalyzer.Forms.FormsForGraphsDisplay
{
    partial class FormForPointSize
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForPointSize));
            this.buttonApply = new System.Windows.Forms.Button();
            this.trackBarPointSize = new System.Windows.Forms.TrackBar();
            this.labelForPointSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPointSize)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonApply
            // 
            this.buttonApply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonApply.Location = new System.Drawing.Point(69, 68);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(102, 28);
            this.buttonApply.TabIndex = 0;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            // 
            // trackBarPointSize
            // 
            this.trackBarPointSize.Location = new System.Drawing.Point(12, 12);
            this.trackBarPointSize.Maximum = 100;
            this.trackBarPointSize.Minimum = 1;
            this.trackBarPointSize.Name = "trackBarPointSize";
            this.trackBarPointSize.Size = new System.Drawing.Size(163, 45);
            this.trackBarPointSize.TabIndex = 1;
            this.trackBarPointSize.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarPointSize.Value = 8;
            this.trackBarPointSize.ValueChanged += new System.EventHandler(this.trackBarPointSize_ValueChanged);
            // 
            // labelForPointSize
            // 
            this.labelForPointSize.AutoSize = true;
            this.labelForPointSize.Location = new System.Drawing.Point(195, 26);
            this.labelForPointSize.Name = "labelForPointSize";
            this.labelForPointSize.Size = new System.Drawing.Size(13, 13);
            this.labelForPointSize.TabIndex = 2;
            this.labelForPointSize.Text = "0";
            this.labelForPointSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormForPointSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 104);
            this.Controls.Add(this.labelForPointSize);
            this.Controls.Add(this.trackBarPointSize);
            this.Controls.Add(this.buttonApply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormForPointSize";
            this.Text = "Marker Size";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPointSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonApply;
        public System.Windows.Forms.TrackBar trackBarPointSize;
        public System.Windows.Forms.Label labelForPointSize;
    }
}