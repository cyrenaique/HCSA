namespace IM3_Plugin3
{
    partial class FormForPostProcessings
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.checkBoxDisplayBottomPlate = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonChangeColorPositive = new System.Windows.Forms.Button();
            this.checkBoxRemoveUnAssociatedObjects = new System.Windows.Forms.CheckBox();
            this.checkBoxExportMetaObjectSignatures = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(225, 188);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(115, 25);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // checkBoxDisplayBottomPlate
            // 
            this.checkBoxDisplayBottomPlate.AutoSize = true;
            this.checkBoxDisplayBottomPlate.Checked = true;
            this.checkBoxDisplayBottomPlate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDisplayBottomPlate.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxDisplayBottomPlate.Location = new System.Drawing.Point(22, 22);
            this.checkBoxDisplayBottomPlate.Name = "checkBoxDisplayBottomPlate";
            this.checkBoxDisplayBottomPlate.Size = new System.Drawing.Size(138, 16);
            this.checkBoxDisplayBottomPlate.TabIndex = 5;
            this.checkBoxDisplayBottomPlate.Text = "Display Bottom Plate";
            this.checkBoxDisplayBottomPlate.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(20, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 12);
            this.label2.TabIndex = 25;
            this.label2.Text = "BackGroundColor";
            // 
            // buttonChangeColorPositive
            // 
            this.buttonChangeColorPositive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.buttonChangeColorPositive.FlatAppearance.BorderSize = 0;
            this.buttonChangeColorPositive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonChangeColorPositive.ForeColor = System.Drawing.Color.Transparent;
            this.buttonChangeColorPositive.Location = new System.Drawing.Point(137, 153);
            this.buttonChangeColorPositive.Name = "buttonChangeColorPositive";
            this.buttonChangeColorPositive.Size = new System.Drawing.Size(20, 20);
            this.buttonChangeColorPositive.TabIndex = 24;
            this.buttonChangeColorPositive.UseVisualStyleBackColor = false;
            this.buttonChangeColorPositive.Click += new System.EventHandler(this.buttonChangeColorPositive_Click);
            // 
            // checkBoxRemoveUnAssociatedObjects
            // 
            this.checkBoxRemoveUnAssociatedObjects.AutoSize = true;
            this.checkBoxRemoveUnAssociatedObjects.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxRemoveUnAssociatedObjects.Location = new System.Drawing.Point(22, 55);
            this.checkBoxRemoveUnAssociatedObjects.Name = "checkBoxRemoveUnAssociatedObjects";
            this.checkBoxRemoveUnAssociatedObjects.Size = new System.Drawing.Size(199, 16);
            this.checkBoxRemoveUnAssociatedObjects.TabIndex = 26;
            this.checkBoxRemoveUnAssociatedObjects.Text = "Remove Un-Associated Objects";
            this.checkBoxRemoveUnAssociatedObjects.UseVisualStyleBackColor = true;
            // 
            // checkBoxExportMetaObjectSignatures
            // 
            this.checkBoxExportMetaObjectSignatures.AutoSize = true;
            this.checkBoxExportMetaObjectSignatures.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxExportMetaObjectSignatures.Location = new System.Drawing.Point(22, 88);
            this.checkBoxExportMetaObjectSignatures.Name = "checkBoxExportMetaObjectSignatures";
            this.checkBoxExportMetaObjectSignatures.Size = new System.Drawing.Size(194, 16);
            this.checkBoxExportMetaObjectSignatures.TabIndex = 27;
            this.checkBoxExportMetaObjectSignatures.Text = "Export Meta-Object Signatures";
            this.checkBoxExportMetaObjectSignatures.UseVisualStyleBackColor = true;
            // 
            // FormForPostProcessings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(352, 225);
            this.Controls.Add(this.checkBoxExportMetaObjectSignatures);
            this.Controls.Add(this.checkBoxRemoveUnAssociatedObjects);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonChangeColorPositive);
            this.Controls.Add(this.checkBoxDisplayBottomPlate);
            this.Controls.Add(this.buttonOk);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormForPostProcessings";
            this.Text = "Post Processings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.CheckBox checkBoxDisplayBottomPlate;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button buttonChangeColorPositive;
        public System.Windows.Forms.CheckBox checkBoxRemoveUnAssociatedObjects;
        public System.Windows.Forms.CheckBox checkBoxExportMetaObjectSignatures;
    }
}