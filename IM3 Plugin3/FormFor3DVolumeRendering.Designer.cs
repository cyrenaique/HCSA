namespace IM3_Plugin3
{
    partial class FormFor3DVolumeRendering
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
            this.trackBarOpacityStrength = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacityStrength)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarOpacityStrength
            // 
            this.trackBarOpacityStrength.Location = new System.Drawing.Point(128, 23);
            this.trackBarOpacityStrength.Maximum = 30;
            this.trackBarOpacityStrength.Name = "trackBarOpacityStrength";
            this.trackBarOpacityStrength.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBarOpacityStrength.Size = new System.Drawing.Size(139, 45);
            this.trackBarOpacityStrength.TabIndex = 32;
            this.trackBarOpacityStrength.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarOpacityStrength.Value = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(58, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 12);
            this.label11.TabIndex = 31;
            this.label11.Text = "Opacity";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(195, 151);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(115, 25);
            this.buttonOk.TabIndex = 33;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // FormFor3DVolumeRendering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(322, 188);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.trackBarOpacityStrength);
            this.Controls.Add(this.label11);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormFor3DVolumeRendering";
            this.Text = "FormFor3DVolumeRendering";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacityStrength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TrackBar trackBarOpacityStrength;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonOk;
    }
}