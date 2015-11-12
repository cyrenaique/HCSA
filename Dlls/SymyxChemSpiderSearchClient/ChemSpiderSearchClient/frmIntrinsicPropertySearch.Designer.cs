namespace ChemSpiderClient
{
    partial class frmIntrinsicPropertySearch
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMolWeightMin = new System.Windows.Forms.Label();
            this.lblEmpiricalFormula = new System.Windows.Forms.Label();
            this.txtEmpiricalFormula = new System.Windows.Forms.TextBox();
            this.lblMolWeightMax = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.ntNominalMassMax = new ChemSpiderClient.NumericTextBox();
            this.ntNominalMassMin = new ChemSpiderClient.NumericTextBox();
            this.ntMonoisotopicMassMax = new ChemSpiderClient.NumericTextBox();
            this.ntMonoisotopicMassMin = new ChemSpiderClient.NumericTextBox();
            this.ntAverageMassMax = new ChemSpiderClient.NumericTextBox();
            this.ntAverageMassMin = new ChemSpiderClient.NumericTextBox();
            this.ntMolWeightMax = new ChemSpiderClient.NumericTextBox();
            this.ntMolWeightMin = new ChemSpiderClient.NumericTextBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(103, 168);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(184, 168);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblMolWeightMin
            // 
            this.lblMolWeightMin.AutoSize = true;
            this.lblMolWeightMin.Location = new System.Drawing.Point(12, 63);
            this.lblMolWeightMin.Name = "lblMolWeightMin";
            this.lblMolWeightMin.Size = new System.Drawing.Size(90, 13);
            this.lblMolWeightMin.TabIndex = 21;
            this.lblMolWeightMin.Text = "Molweight Range";
            // 
            // lblEmpiricalFormula
            // 
            this.lblEmpiricalFormula.AutoSize = true;
            this.lblEmpiricalFormula.Location = new System.Drawing.Point(12, 15);
            this.lblEmpiricalFormula.Name = "lblEmpiricalFormula";
            this.lblEmpiricalFormula.Size = new System.Drawing.Size(89, 13);
            this.lblEmpiricalFormula.TabIndex = 20;
            this.lblEmpiricalFormula.Text = "Empirical Formula";
            // 
            // txtEmpiricalFormula
            // 
            this.txtEmpiricalFormula.Location = new System.Drawing.Point(114, 12);
            this.txtEmpiricalFormula.Name = "txtEmpiricalFormula";
            this.txtEmpiricalFormula.Size = new System.Drawing.Size(143, 20);
            this.txtEmpiricalFormula.TabIndex = 0;
            // 
            // lblMolWeightMax
            // 
            this.lblMolWeightMax.AutoSize = true;
            this.lblMolWeightMax.Location = new System.Drawing.Point(196, 63);
            this.lblMolWeightMax.Name = "lblMolWeightMax";
            this.lblMolWeightMax.Size = new System.Drawing.Size(16, 13);
            this.lblMolWeightMax.TabIndex = 22;
            this.lblMolWeightMax.Text = "to";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(196, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "to";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Average Mass Range";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(196, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "to";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Monoisotopic Mass Range";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(196, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "to";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "Nominal Mass Range";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(216, 43);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(27, 13);
            this.label28.TabIndex = 83;
            this.label28.Text = "Max";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(148, 43);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(24, 13);
            this.label27.TabIndex = 82;
            this.label27.Text = "Min";
            // 
            // ntNominalMassMax
            // 
            this.ntNominalMassMax.AllowSpace = false;
            this.ntNominalMassMax.IntegerOnly = false;
            this.ntNominalMassMax.Location = new System.Drawing.Point(218, 138);
            this.ntNominalMassMax.Name = "ntNominalMassMax";
            this.ntNominalMassMax.Size = new System.Drawing.Size(39, 20);
            this.ntNominalMassMax.TabIndex = 8;
            // 
            // ntNominalMassMin
            // 
            this.ntNominalMassMin.AllowSpace = false;
            this.ntNominalMassMin.IntegerOnly = false;
            this.ntNominalMassMin.Location = new System.Drawing.Point(151, 138);
            this.ntNominalMassMin.Name = "ntNominalMassMin";
            this.ntNominalMassMin.Size = new System.Drawing.Size(39, 20);
            this.ntNominalMassMin.TabIndex = 7;
            // 
            // ntMonoisotopicMassMax
            // 
            this.ntMonoisotopicMassMax.AllowSpace = false;
            this.ntMonoisotopicMassMax.IntegerOnly = false;
            this.ntMonoisotopicMassMax.Location = new System.Drawing.Point(218, 112);
            this.ntMonoisotopicMassMax.Name = "ntMonoisotopicMassMax";
            this.ntMonoisotopicMassMax.Size = new System.Drawing.Size(39, 20);
            this.ntMonoisotopicMassMax.TabIndex = 6;
            // 
            // ntMonoisotopicMassMin
            // 
            this.ntMonoisotopicMassMin.AllowSpace = false;
            this.ntMonoisotopicMassMin.IntegerOnly = false;
            this.ntMonoisotopicMassMin.Location = new System.Drawing.Point(151, 112);
            this.ntMonoisotopicMassMin.Name = "ntMonoisotopicMassMin";
            this.ntMonoisotopicMassMin.Size = new System.Drawing.Size(39, 20);
            this.ntMonoisotopicMassMin.TabIndex = 5;
            // 
            // ntAverageMassMax
            // 
            this.ntAverageMassMax.AllowSpace = false;
            this.ntAverageMassMax.IntegerOnly = false;
            this.ntAverageMassMax.Location = new System.Drawing.Point(218, 86);
            this.ntAverageMassMax.Name = "ntAverageMassMax";
            this.ntAverageMassMax.Size = new System.Drawing.Size(39, 20);
            this.ntAverageMassMax.TabIndex = 4;
            // 
            // ntAverageMassMin
            // 
            this.ntAverageMassMin.AllowSpace = false;
            this.ntAverageMassMin.IntegerOnly = false;
            this.ntAverageMassMin.Location = new System.Drawing.Point(151, 86);
            this.ntAverageMassMin.Name = "ntAverageMassMin";
            this.ntAverageMassMin.Size = new System.Drawing.Size(39, 20);
            this.ntAverageMassMin.TabIndex = 3;
            // 
            // ntMolWeightMax
            // 
            this.ntMolWeightMax.AllowSpace = false;
            this.ntMolWeightMax.IntegerOnly = false;
            this.ntMolWeightMax.Location = new System.Drawing.Point(218, 60);
            this.ntMolWeightMax.Name = "ntMolWeightMax";
            this.ntMolWeightMax.Size = new System.Drawing.Size(39, 20);
            this.ntMolWeightMax.TabIndex = 2;
            // 
            // ntMolWeightMin
            // 
            this.ntMolWeightMin.AllowSpace = false;
            this.ntMolWeightMin.IntegerOnly = false;
            this.ntMolWeightMin.Location = new System.Drawing.Point(151, 60);
            this.ntMolWeightMin.Name = "ntMolWeightMin";
            this.ntMolWeightMin.Size = new System.Drawing.Size(39, 20);
            this.ntMolWeightMin.TabIndex = 1;
            // 
            // frmIntrinsicPropertySearch
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(271, 203);
            this.ControlBox = false;
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.ntNominalMassMax);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ntNominalMassMin);
            this.Controls.Add(this.ntMonoisotopicMassMax);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ntMonoisotopicMassMin);
            this.Controls.Add(this.ntAverageMassMax);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ntAverageMassMin);
            this.Controls.Add(this.ntMolWeightMax);
            this.Controls.Add(this.lblMolWeightMax);
            this.Controls.Add(this.lblMolWeightMin);
            this.Controls.Add(this.lblEmpiricalFormula);
            this.Controls.Add(this.txtEmpiricalFormula);
            this.Controls.Add(this.ntMolWeightMin);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmIntrinsicPropertySearch";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Intrinsic Property Search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMolWeightMin;
        private System.Windows.Forms.Label lblEmpiricalFormula;
        private System.Windows.Forms.Label lblMolWeightMax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        internal System.Windows.Forms.TextBox txtEmpiricalFormula;
        internal NumericTextBox ntMolWeightMin;
        internal NumericTextBox ntMolWeightMax;
        internal NumericTextBox ntAverageMassMax;
        internal NumericTextBox ntAverageMassMin;
        internal NumericTextBox ntMonoisotopicMassMax;
        internal NumericTextBox ntMonoisotopicMassMin;
        internal NumericTextBox ntNominalMassMax;
        internal NumericTextBox ntNominalMassMin;
    }
}