namespace HCSAnalyzer.Forms
{
    partial class FormForSplashScreen
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForSplashScreen));
            this.richTextBoxForProcess = new System.Windows.Forms.RichTextBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxForProcess
            // 
            this.richTextBoxForProcess.Location = new System.Drawing.Point(160, 12);
            this.richTextBoxForProcess.Name = "richTextBoxForProcess";
            this.richTextBoxForProcess.Size = new System.Drawing.Size(235, 248);
            this.richTextBoxForProcess.TabIndex = 0;
            this.richTextBoxForProcess.Text = "";
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::HCSAnalyzer.Properties.Resources.Logo;
            this.pictureBox.InitialImage = global::HCSAnalyzer.Properties.Resources.Logo;
            this.pictureBox.Location = new System.Drawing.Point(8, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(146, 248);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // FormForSplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(404, 272);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.richTextBoxForProcess);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormForSplashScreen";
            this.Text = "FormForSplashScreen";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox richTextBoxForProcess;
        private System.Windows.Forms.PictureBox pictureBox;

    }
}