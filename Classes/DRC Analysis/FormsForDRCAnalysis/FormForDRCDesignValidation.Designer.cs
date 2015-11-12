namespace HCSAnalyzer.Classes.DRC_Analysis.FormsForDRCAnalysis
{
    partial class FormForDRCDesignValidation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForDRCDesignValidation));
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.panelForPlatesName = new System.Windows.Forms.Panel();
            this.checkBoxUpdateGroupID = new System.Windows.Forms.CheckBox();
            this.label = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(267, 304);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(88, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.panelForPlatesName);
            this.groupBox.Location = new System.Drawing.Point(12, 10);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(249, 318);
            this.groupBox.TabIndex = 1;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Plate To Be Processed";
            // 
            // panelForPlatesName
            // 
            this.panelForPlatesName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForPlatesName.AutoScroll = true;
            this.panelForPlatesName.Location = new System.Drawing.Point(6, 19);
            this.panelForPlatesName.Name = "panelForPlatesName";
            this.panelForPlatesName.Size = new System.Drawing.Size(237, 293);
            this.panelForPlatesName.TabIndex = 0;
            // 
            // checkBoxUpdateGroupID
            // 
            this.checkBoxUpdateGroupID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxUpdateGroupID.AutoSize = true;
            this.checkBoxUpdateGroupID.Checked = true;
            this.checkBoxUpdateGroupID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUpdateGroupID.Location = new System.Drawing.Point(277, 268);
            this.checkBoxUpdateGroupID.Name = "checkBoxUpdateGroupID";
            this.checkBoxUpdateGroupID.Size = new System.Drawing.Size(69, 30);
            this.checkBoxUpdateGroupID.TabIndex = 2;
            this.checkBoxUpdateGroupID.Text = "Update \r\nGroup ID";
            this.checkBoxUpdateGroupID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxUpdateGroupID.UseVisualStyleBackColor = true;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(267, 18);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(78, 91);
            this.label.TabIndex = 3;
            this.label.Text = "Warning:\r\nThis operation\r\n could modify\r\n the current \r\n concentration \r\n values!" +
                "\r\n\r\n";
            // 
            // FormForDRCDesignValidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 334);
            this.Controls.Add(this.label);
            this.Controls.Add(this.checkBoxUpdateGroupID);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.buttonOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(366, 207);
            this.Name = "FormForDRCDesignValidation";
            this.Text = "DRC Design Validation";
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.GroupBox groupBox;
        public System.Windows.Forms.Panel panelForPlatesName;
        public System.Windows.Forms.CheckBox checkBoxUpdateGroupID;
        public System.Windows.Forms.Label label;
    }
}