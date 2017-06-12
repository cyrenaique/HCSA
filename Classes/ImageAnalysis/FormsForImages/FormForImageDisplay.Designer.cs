namespace HCSAnalyzer.Forms.FormsForImages
{
    partial class FormForImageDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForImageDisplay));
            this.panelForImage = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelForImage
            // 
            this.panelForImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForImage.AutoScroll = true;
            this.panelForImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelForImage.Location = new System.Drawing.Point(12, 12);
            this.panelForImage.Name = "panelForImage";
            this.panelForImage.Size = new System.Drawing.Size(605, 502);
            this.panelForImage.TabIndex = 0;
            this.panelForImage.Paint += new System.Windows.Forms.PaintEventHandler(this.panelForImage_Paint);
            this.panelForImage.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelForImage_MouseDoubleClick);
            this.panelForImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelForImage_MouseDown);
            this.panelForImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelForImage_MouseMove);
            // 
            // FormForImageDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(629, 526);
            this.Controls.Add(this.panelForImage);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormForImageDisplay";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormForImageDisplay_FormClosed);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.panelForImage_MouseWheel);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panelForImage;
        protected NumericUpDownStripItem numericUpDownStripItem1;
      
        //private System.Windows.Forms.MouseEventHandler panelForImage_MouseWheel;





      
    }
}