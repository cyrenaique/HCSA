namespace HCSAnalyzer.Forms.FormsForOptions
{
    partial class FormOptionsForPlateName
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.radioButtonNameFromFolder = new System.Windows.Forms.RadioButton();
            this.radioButtonNameFromFile = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "No \"Plate Name\" feature has been defined !";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(249, 7);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(89, 26);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel ?";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonContinue
            // 
            this.buttonContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonContinue.Location = new System.Drawing.Point(17, 58);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(89, 26);
            this.buttonContinue.TabIndex = 2;
            this.buttonContinue.Text = "Continue ->";
            this.buttonContinue.UseVisualStyleBackColor = true;
            // 
            // radioButtonNameFromFolder
            // 
            this.radioButtonNameFromFolder.AutoSize = true;
            this.radioButtonNameFromFolder.Checked = true;
            this.radioButtonNameFromFolder.Location = new System.Drawing.Point(242, 51);
            this.radioButtonNameFromFolder.Name = "radioButtonNameFromFolder";
            this.radioButtonNameFromFolder.Size = new System.Drawing.Size(88, 17);
            this.radioButtonNameFromFolder.TabIndex = 3;
            this.radioButtonNameFromFolder.TabStop = true;
            this.radioButtonNameFromFolder.Text = "Parent Folder";
            this.radioButtonNameFromFolder.UseVisualStyleBackColor = true;
            // 
            // radioButtonNameFromFile
            // 
            this.radioButtonNameFromFile.AutoSize = true;
            this.radioButtonNameFromFile.Location = new System.Drawing.Point(242, 74);
            this.radioButtonNameFromFile.Name = "radioButtonNameFromFile";
            this.radioButtonNameFromFile.Size = new System.Drawing.Size(72, 17);
            this.radioButtonNameFromFile.TabIndex = 4;
            this.radioButtonNameFromFile.Text = "File Name";
            this.radioButtonNameFromFile.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "\"Plate Name\" \r\nwill be defined by:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormOptionsForPlateName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 107);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButtonNameFromFile);
            this.Controls.Add(this.radioButtonNameFromFolder);
            this.Controls.Add(this.buttonContinue);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOptionsForPlateName";
            this.Text = "Warning!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonContinue;
        public System.Windows.Forms.RadioButton radioButtonNameFromFolder;
        public System.Windows.Forms.RadioButton radioButtonNameFromFile;
        private System.Windows.Forms.Label label2;
    }
}