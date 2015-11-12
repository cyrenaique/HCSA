namespace ChemSpiderClient
{
    partial class frmCommonSearchOptions
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
            this.cboComplexity = new System.Windows.Forms.ComboBox();
            this.cboIsotopic = new System.Windows.Forms.ComboBox();
            this.chkHasPatents = new System.Windows.Forms.CheckBox();
            this.chkHasSpectra = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboComplexity
            // 
            this.cboComplexity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboComplexity.FormattingEnabled = true;
            this.cboComplexity.Items.AddRange(new object[] {
            "Any",
            "Multi",
            "Single"});
            this.cboComplexity.Location = new System.Drawing.Point(75, 12);
            this.cboComplexity.Name = "cboComplexity";
            this.cboComplexity.Size = new System.Drawing.Size(197, 21);
            this.cboComplexity.TabIndex = 2;
            // 
            // cboIsotopic
            // 
            this.cboIsotopic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIsotopic.FormattingEnabled = true;
            this.cboIsotopic.Items.AddRange(new object[] {
            "Any",
            "Labeled",
            "NotLabeled"});
            this.cboIsotopic.Location = new System.Drawing.Point(75, 39);
            this.cboIsotopic.Name = "cboIsotopic";
            this.cboIsotopic.Size = new System.Drawing.Size(197, 21);
            this.cboIsotopic.TabIndex = 4;
            // 
            // chkHasPatents
            // 
            this.chkHasPatents.AutoSize = true;
            this.chkHasPatents.Location = new System.Drawing.Point(75, 66);
            this.chkHasPatents.Name = "chkHasPatents";
            this.chkHasPatents.Size = new System.Drawing.Size(84, 17);
            this.chkHasPatents.TabIndex = 5;
            this.chkHasPatents.Text = "Has Patents";
            this.chkHasPatents.UseVisualStyleBackColor = true;
            // 
            // chkHasSpectra
            // 
            this.chkHasSpectra.AutoSize = true;
            this.chkHasSpectra.Location = new System.Drawing.Point(187, 66);
            this.chkHasSpectra.Name = "chkHasSpectra";
            this.chkHasSpectra.Size = new System.Drawing.Size(85, 17);
            this.chkHasSpectra.TabIndex = 6;
            this.chkHasSpectra.Text = "Has Spectra";
            this.chkHasSpectra.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Complexity";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Isotopic";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(197, 94);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(116, 94);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmCommonSearchOptions
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 129);
            this.ControlBox = false;
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkHasSpectra);
            this.Controls.Add(this.chkHasPatents);
            this.Controls.Add(this.cboIsotopic);
            this.Controls.Add(this.cboComplexity);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmCommonSearchOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChemSpider Common Search Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboComplexity;
        private System.Windows.Forms.ComboBox cboIsotopic;
        private System.Windows.Forms.CheckBox chkHasPatents;
        private System.Windows.Forms.CheckBox chkHasSpectra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}