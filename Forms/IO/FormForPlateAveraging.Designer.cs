namespace HCSAnalyzer.Forms.IO
{
    partial class FormForPlateAveraging
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForPlateAveraging));
            this.buttonOk = new System.Windows.Forms.Button();
            this.panelForPlateList = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxWeightedSum = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(210, 387);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(96, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // panelForPlateList
            // 
            this.panelForPlateList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForPlateList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelForPlateList.Location = new System.Drawing.Point(12, 29);
            this.panelForPlateList.Name = "panelForPlateList";
            this.panelForPlateList.Size = new System.Drawing.Size(290, 321);
            this.panelForPlateList.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "List Plates";
            // 
            // checkBoxWeightedSum
            // 
            this.checkBoxWeightedSum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxWeightedSum.AutoSize = true;
            this.checkBoxWeightedSum.Location = new System.Drawing.Point(14, 359);
            this.checkBoxWeightedSum.Name = "checkBoxWeightedSum";
            this.checkBoxWeightedSum.Size = new System.Drawing.Size(138, 17);
            this.checkBoxWeightedSum.TabIndex = 3;
            this.checkBoxWeightedSum.Text = "Quality based weighting";
            this.checkBoxWeightedSum.UseVisualStyleBackColor = true;
            // 
            // FormForPlateAveraging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 417);
            this.Controls.Add(this.checkBoxWeightedSum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelForPlateList);
            this.Controls.Add(this.buttonOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(250, 177);
            this.Name = "FormForPlateAveraging";
            this.Text = "Plate Averaging";
            this.Load += new System.EventHandler(this.FormForPlateAveraging_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Panel panelForPlateList;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox checkBoxWeightedSum;
    }
}