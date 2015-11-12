namespace ChemSpiderClient
{
    partial class frmElementsSearch
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
            this.chkIncludeAll = new System.Windows.Forms.CheckBox();
            this.txtIncludeElements = new System.Windows.Forms.TextBox();
            this.txtExcludeElements = new System.Windows.Forms.TextBox();
            this.lblIncludeElements = new System.Windows.Forms.Label();
            this.lblExcludeElements = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkIncludeAll
            // 
            this.chkIncludeAll.AutoSize = true;
            this.chkIncludeAll.Location = new System.Drawing.Point(15, 68);
            this.chkIncludeAll.Name = "chkIncludeAll";
            this.chkIncludeAll.Size = new System.Drawing.Size(75, 17);
            this.chkIncludeAll.TabIndex = 4;
            this.chkIncludeAll.Text = "Include All";
            this.chkIncludeAll.UseVisualStyleBackColor = true;
            this.chkIncludeAll.CheckedChanged += new System.EventHandler(this.chkIncludeAll_CheckedChanged);
            // 
            // txtIncludeElements
            // 
            this.txtIncludeElements.Location = new System.Drawing.Point(106, 12);
            this.txtIncludeElements.Name = "txtIncludeElements";
            this.txtIncludeElements.Size = new System.Drawing.Size(166, 20);
            this.txtIncludeElements.TabIndex = 2;
            // 
            // txtExcludeElements
            // 
            this.txtExcludeElements.Location = new System.Drawing.Point(106, 38);
            this.txtExcludeElements.Name = "txtExcludeElements";
            this.txtExcludeElements.Size = new System.Drawing.Size(166, 20);
            this.txtExcludeElements.TabIndex = 2;
            // 
            // lblIncludeElements
            // 
            this.lblIncludeElements.AutoSize = true;
            this.lblIncludeElements.Location = new System.Drawing.Point(12, 15);
            this.lblIncludeElements.Name = "lblIncludeElements";
            this.lblIncludeElements.Size = new System.Drawing.Size(88, 13);
            this.lblIncludeElements.TabIndex = 1;
            this.lblIncludeElements.Text = "Include Elements";
            // 
            // lblExcludeElements
            // 
            this.lblExcludeElements.AutoSize = true;
            this.lblExcludeElements.Location = new System.Drawing.Point(12, 41);
            this.lblExcludeElements.Name = "lblExcludeElements";
            this.lblExcludeElements.Size = new System.Drawing.Size(91, 13);
            this.lblExcludeElements.TabIndex = 3;
            this.lblExcludeElements.Text = "Exclude Elements";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(198, 91);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(117, 91);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmElementsSearch
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(285, 126);
            this.ControlBox = false;
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblExcludeElements);
            this.Controls.Add(this.lblIncludeElements);
            this.Controls.Add(this.txtExcludeElements);
            this.Controls.Add(this.txtIncludeElements);
            this.Controls.Add(this.chkIncludeAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmElementsSearch";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Elements Search Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkIncludeAll;
        private System.Windows.Forms.TextBox txtIncludeElements;
        private System.Windows.Forms.TextBox txtExcludeElements;
        private System.Windows.Forms.Label lblIncludeElements;
        private System.Windows.Forms.Label lblExcludeElements;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}