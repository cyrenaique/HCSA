namespace HCSAnalyzer.Forms.IO
{
    partial class FormForMultipleClassSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForMultipleClassSelection));
            this.buttonOk = new System.Windows.Forms.Button();
            this.splitContainerForClassSelection = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerForClassSelection)).BeginInit();
            this.splitContainerForClassSelection.Panel2.SuspendLayout();
            this.splitContainerForClassSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(9, 10);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(127, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // splitContainerForClassSelection
            // 
            this.splitContainerForClassSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerForClassSelection.Location = new System.Drawing.Point(3, 4);
            this.splitContainerForClassSelection.Name = "splitContainerForClassSelection";
            this.splitContainerForClassSelection.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerForClassSelection.Panel2
            // 
            this.splitContainerForClassSelection.Panel2.Controls.Add(this.buttonOk);
            this.splitContainerForClassSelection.Panel2MinSize = 30;
            this.splitContainerForClassSelection.Size = new System.Drawing.Size(145, 298);
            this.splitContainerForClassSelection.SplitterDistance = 250;
            this.splitContainerForClassSelection.TabIndex = 1;
            // 
            // FormForMultipleClassSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(151, 306);
            this.Controls.Add(this.splitContainerForClassSelection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormForMultipleClassSelection";
            this.Text = "Select Classes";
            this.splitContainerForClassSelection.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerForClassSelection)).EndInit();
            this.splitContainerForClassSelection.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.SplitContainer splitContainerForClassSelection;
    }
}