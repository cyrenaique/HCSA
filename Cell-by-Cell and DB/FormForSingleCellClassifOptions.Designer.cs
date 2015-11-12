namespace HCSAnalyzer.Cell_by_Cell_and_DB
{
    partial class FormForSingleCellClassifOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForSingleCellClassifOptions));
            this.buttonRun = new System.Windows.Forms.Button();
            this.panelPhenoToBeClassified = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelWellToBeClassified = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabPagePlates = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRun
            // 
            this.buttonRun.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRun.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonRun.Location = new System.Drawing.Point(149, 392);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(104, 23);
            this.buttonRun.TabIndex = 0;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            // 
            // panelPhenoToBeClassified
            // 
            this.panelPhenoToBeClassified.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPhenoToBeClassified.AutoScroll = true;
            this.panelPhenoToBeClassified.Location = new System.Drawing.Point(3, 3);
            this.panelPhenoToBeClassified.Name = "panelPhenoToBeClassified";
            this.panelPhenoToBeClassified.Size = new System.Drawing.Size(222, 316);
            this.panelPhenoToBeClassified.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPagePlates);
            this.tabControl.Location = new System.Drawing.Point(5, 19);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(236, 348);
            this.tabControl.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelPhenoToBeClassified);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(228, 322);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Phenotype";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panelWellToBeClassified);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(228, 322);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Well";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panelWellToBeClassified
            // 
            this.panelWellToBeClassified.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelWellToBeClassified.AutoScroll = true;
            this.panelWellToBeClassified.Location = new System.Drawing.Point(3, 3);
            this.panelWellToBeClassified.Name = "panelWellToBeClassified";
            this.panelWellToBeClassified.Size = new System.Drawing.Size(222, 316);
            this.panelWellToBeClassified.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl);
            this.groupBox1.Location = new System.Drawing.Point(8, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(245, 374);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "To Be Classified";
            // 
            // tabPagePlates
            // 
            this.tabPagePlates.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlates.Name = "tabPagePlates";
            this.tabPagePlates.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlates.Size = new System.Drawing.Size(228, 322);
            this.tabPagePlates.TabIndex = 2;
            this.tabPagePlates.Text = "Plates";
            this.tabPagePlates.UseVisualStyleBackColor = true;
            // 
            // FormForSingleCellClassifOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 421);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForSingleCellClassifOptions";
            this.Text = "Single Object Classification";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRun;
        public System.Windows.Forms.Panel panelPhenoToBeClassified;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.Panel panelWellToBeClassified;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TabPage tabPagePlates;
        public System.Windows.Forms.TabControl tabControl;
    }
}