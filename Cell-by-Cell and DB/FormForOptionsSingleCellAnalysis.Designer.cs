namespace HCSAnalyzer.Forms.IO
{
    partial class FormForOptionsSingleCellAnalysis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForOptionsSingleCellAnalysis));
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxInitPhenoClass = new System.Windows.Forms.GroupBox();
            this.radioButtonInitPhenoClassDesc = new System.Windows.Forms.RadioButton();
            this.radioButtonInitPhenoClassWell = new System.Windows.Forms.RadioButton();
            this.radioButtonInitPhenoClassDB = new System.Windows.Forms.RadioButton();
            this.groupBoxInitPhenoClass.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(108, 118);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // groupBoxInitPhenoClass
            // 
            this.groupBoxInitPhenoClass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBoxInitPhenoClass.Controls.Add(this.radioButtonInitPhenoClassDesc);
            this.groupBoxInitPhenoClass.Controls.Add(this.radioButtonInitPhenoClassWell);
            this.groupBoxInitPhenoClass.Controls.Add(this.radioButtonInitPhenoClassDB);
            this.groupBoxInitPhenoClass.Location = new System.Drawing.Point(14, 8);
            this.groupBoxInitPhenoClass.Name = "groupBoxInitPhenoClass";
            this.groupBoxInitPhenoClass.Size = new System.Drawing.Size(169, 100);
            this.groupBoxInitPhenoClass.TabIndex = 34;
            this.groupBoxInitPhenoClass.TabStop = false;
            this.groupBoxInitPhenoClass.Text = "Initial phenotypic class";
            // 
            // radioButtonInitPhenoClassDesc
            // 
            this.radioButtonInitPhenoClassDesc.AutoSize = true;
            this.radioButtonInitPhenoClassDesc.Location = new System.Drawing.Point(36, 71);
            this.radioButtonInitPhenoClassDesc.Name = "radioButtonInitPhenoClassDesc";
            this.radioButtonInitPhenoClassDesc.Size = new System.Drawing.Size(110, 17);
            this.radioButtonInitPhenoClassDesc.TabIndex = 2;
            this.radioButtonInitPhenoClassDesc.TabStop = true;
            this.radioButtonInitPhenoClassDesc.Text = "Current Descriptor";
            this.radioButtonInitPhenoClassDesc.UseVisualStyleBackColor = true;
            // 
            // radioButtonInitPhenoClassWell
            // 
            this.radioButtonInitPhenoClassWell.AutoSize = true;
            this.radioButtonInitPhenoClassWell.Location = new System.Drawing.Point(36, 48);
            this.radioButtonInitPhenoClassWell.Name = "radioButtonInitPhenoClassWell";
            this.radioButtonInitPhenoClassWell.Size = new System.Drawing.Size(74, 17);
            this.radioButtonInitPhenoClassWell.TabIndex = 1;
            this.radioButtonInitPhenoClassWell.TabStop = true;
            this.radioButtonInitPhenoClassWell.Text = "Well Class";
            this.radioButtonInitPhenoClassWell.UseVisualStyleBackColor = true;
            // 
            // radioButtonInitPhenoClassDB
            // 
            this.radioButtonInitPhenoClassDB.AutoSize = true;
            this.radioButtonInitPhenoClassDB.Checked = true;
            this.radioButtonInitPhenoClassDB.Location = new System.Drawing.Point(36, 25);
            this.radioButtonInitPhenoClassDB.Name = "radioButtonInitPhenoClassDB";
            this.radioButtonInitPhenoClassDB.Size = new System.Drawing.Size(97, 17);
            this.radioButtonInitPhenoClassDB.TabIndex = 0;
            this.radioButtonInitPhenoClassDB.TabStop = true;
            this.radioButtonInitPhenoClassDB.Text = "From Database";
            this.radioButtonInitPhenoClassDB.UseVisualStyleBackColor = true;
            // 
            // FormForOptionsSingleCellAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(195, 151);
            this.Controls.Add(this.groupBoxInitPhenoClass);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForOptionsSingleCellAnalysis";
            this.Text = "Options";
            this.groupBoxInitPhenoClass.ResumeLayout(false);
            this.groupBoxInitPhenoClass.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupBoxInitPhenoClass;
        public System.Windows.Forms.RadioButton radioButtonInitPhenoClassDesc;
        public System.Windows.Forms.RadioButton radioButtonInitPhenoClassWell;
        public System.Windows.Forms.RadioButton radioButtonInitPhenoClassDB;
    }
}