namespace ChemSpiderClient
{
    partial class frmLASSOSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLASSOSearch));
            this.cboFamilyMin = new System.Windows.Forms.ComboBox();
            this.nudThresholdMax = new System.Windows.Forms.NumericUpDown();
            this.nudThresholdMin = new System.Windows.Forms.NumericUpDown();
            this.lblFamilyMin = new System.Windows.Forms.Label();
            this.lblFamilyMax = new System.Windows.Forms.Label();
            this.lblThresholdMin = new System.Windows.Forms.Label();
            this.lblThresholdMax = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboFamilyMax = new Symyx.CustomUIControls.ComboControls.CheckableCombo();
            ((System.ComponentModel.ISupportInitialize)(this.nudThresholdMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThresholdMin)).BeginInit();
            this.SuspendLayout();
            // 
            // cboFamilyMin
            // 
            this.cboFamilyMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFamilyMin.FormattingEnabled = true;
            this.cboFamilyMin.IntegralHeight = false;
            this.cboFamilyMin.Items.AddRange(new object[] {
            "ACE",
            "AChE",
            "ADA",
            "ALR2",
            "AmpC",
            "AR",
            "CDK2",
            "COMT",
            "COX-1",
            "COX-2",
            "DHFR",
            "EGFr",
            "ER1",
            "ER2",
            "FGFr1",
            "FXa",
            "GART",
            "GPB",
            "GR",
            "HIVPR",
            "HIVRT",
            "HMGR",
            "HSP90",
            "InhA",
            "MR",
            "NA",
            "P38 MAP",
            "PARP",
            "PDE5",
            "PDGFrb",
            "PNP",
            "PPARg",
            "PR",
            "RXRa",
            "SAHH",
            "SRC",
            "Thrombin",
            "TK",
            "Trypsin",
            "VEGFr2"});
            this.cboFamilyMin.Location = new System.Drawing.Point(74, 13);
            this.cboFamilyMin.Name = "cboFamilyMin";
            this.cboFamilyMin.Size = new System.Drawing.Size(198, 21);
            this.cboFamilyMin.TabIndex = 3;
            // 
            // nudThresholdMax
            // 
            this.nudThresholdMax.DecimalPlaces = 2;
            this.nudThresholdMax.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudThresholdMax.Location = new System.Drawing.Point(226, 68);
            this.nudThresholdMax.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            131072});
            this.nudThresholdMax.Name = "nudThresholdMax";
            this.nudThresholdMax.Size = new System.Drawing.Size(45, 20);
            this.nudThresholdMax.TabIndex = 7;
            // 
            // nudThresholdMin
            // 
            this.nudThresholdMin.DecimalPlaces = 2;
            this.nudThresholdMin.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudThresholdMin.Location = new System.Drawing.Point(92, 68);
            this.nudThresholdMin.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            131072});
            this.nudThresholdMin.Name = "nudThresholdMin";
            this.nudThresholdMin.Size = new System.Drawing.Size(45, 20);
            this.nudThresholdMin.TabIndex = 5;
            // 
            // lblFamilyMin
            // 
            this.lblFamilyMin.AutoSize = true;
            this.lblFamilyMin.Location = new System.Drawing.Point(12, 16);
            this.lblFamilyMin.Name = "lblFamilyMin";
            this.lblFamilyMin.Size = new System.Drawing.Size(56, 13);
            this.lblFamilyMin.TabIndex = 0;
            this.lblFamilyMin.Text = "Family Min";
            // 
            // lblFamilyMax
            // 
            this.lblFamilyMax.AutoSize = true;
            this.lblFamilyMax.Location = new System.Drawing.Point(12, 44);
            this.lblFamilyMax.Name = "lblFamilyMax";
            this.lblFamilyMax.Size = new System.Drawing.Size(59, 13);
            this.lblFamilyMax.TabIndex = 2;
            this.lblFamilyMax.Text = "Family Max";
            // 
            // lblThresholdMin
            // 
            this.lblThresholdMin.AutoSize = true;
            this.lblThresholdMin.Location = new System.Drawing.Point(12, 70);
            this.lblThresholdMin.Name = "lblThresholdMin";
            this.lblThresholdMin.Size = new System.Drawing.Size(74, 13);
            this.lblThresholdMin.TabIndex = 4;
            this.lblThresholdMin.Text = "Threshold Min";
            // 
            // lblThresholdMax
            // 
            this.lblThresholdMax.AutoSize = true;
            this.lblThresholdMax.Location = new System.Drawing.Point(143, 70);
            this.lblThresholdMax.Name = "lblThresholdMax";
            this.lblThresholdMax.Size = new System.Drawing.Size(77, 13);
            this.lblThresholdMax.TabIndex = 6;
            this.lblThresholdMax.Text = "Threshold Max";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(116, 99);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(197, 99);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cboFamilyMax
            // 
            this.cboFamilyMax.Category = "";
            this.cboFamilyMax.CheckBoxes = true;
            this.cboFamilyMax.HideSelection = false;
            this.cboFamilyMax.Items = ((System.Collections.ObjectModel.ObservableCollection<string>)(resources.GetObject("cboFamilyMax.Items")));
            this.cboFamilyMax.ItemsArray = ((System.Collections.ArrayList)(resources.GetObject("cboFamilyMax.ItemsArray")));
            this.cboFamilyMax.Location = new System.Drawing.Point(74, 40);
            this.cboFamilyMax.Name = "cboFamilyMax";
            this.cboFamilyMax.ReadOnly = true;
            this.cboFamilyMax.Size = new System.Drawing.Size(198, 22);
            this.cboFamilyMax.TabIndex = 10;
            this.cboFamilyMax.TreeImageList = null;
            // 
            // frmLASSOSearch
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 134);
            this.ControlBox = false;
            this.Controls.Add(this.cboFamilyMax);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblThresholdMax);
            this.Controls.Add(this.lblThresholdMin);
            this.Controls.Add(this.lblFamilyMax);
            this.Controls.Add(this.lblFamilyMin);
            this.Controls.Add(this.nudThresholdMin);
            this.Controls.Add(this.nudThresholdMax);
            this.Controls.Add(this.cboFamilyMin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmLASSOSearch";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LASSO Search";
            ((System.ComponentModel.ISupportInitialize)(this.nudThresholdMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThresholdMin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboFamilyMin;
        private System.Windows.Forms.NumericUpDown nudThresholdMax;
        private System.Windows.Forms.NumericUpDown nudThresholdMin;
        private System.Windows.Forms.Label lblFamilyMin;
        private System.Windows.Forms.Label lblFamilyMax;
        private System.Windows.Forms.Label lblThresholdMin;
        private System.Windows.Forms.Label lblThresholdMax;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private Symyx.CustomUIControls.ComboControls.CheckableCombo cboFamilyMax;
    }
}