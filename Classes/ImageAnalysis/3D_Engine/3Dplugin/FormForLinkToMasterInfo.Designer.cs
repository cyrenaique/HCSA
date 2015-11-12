namespace IM3_Plugin3
{
    partial class FormForLinkToMasterInfo
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownDistanceMaxToMaster = new System.Windows.Forms.NumericUpDown();
            this.checkBoxDrawLinkToMasterCenter = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayBranchToCenterDistance = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayBranchToEdgesDistance = new System.Windows.Forms.CheckBox();
            this.checkBoxDrawLinkToMasterEdges = new System.Windows.Forms.CheckBox();
            this.groupBoxCentroid = new System.Windows.Forms.GroupBox();
            this.groupBoxEdges = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDistanceMaxToMaster)).BeginInit();
            this.groupBoxCentroid.SuspendLayout();
            this.groupBoxEdges.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonOK.Location = new System.Drawing.Point(55, 259);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(146, 30);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Distance Maximum";
            // 
            // numericUpDownDistanceMaxToMaster
            // 
            this.numericUpDownDistanceMaxToMaster.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownDistanceMaxToMaster.DecimalPlaces = 1;
            this.numericUpDownDistanceMaxToMaster.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownDistanceMaxToMaster.Location = new System.Drawing.Point(146, 21);
            this.numericUpDownDistanceMaxToMaster.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDownDistanceMaxToMaster.Name = "numericUpDownDistanceMaxToMaster";
            this.numericUpDownDistanceMaxToMaster.Size = new System.Drawing.Size(88, 20);
            this.numericUpDownDistanceMaxToMaster.TabIndex = 2;
            this.numericUpDownDistanceMaxToMaster.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // checkBoxDrawLinkToMasterCenter
            // 
            this.checkBoxDrawLinkToMasterCenter.AutoSize = true;
            this.checkBoxDrawLinkToMasterCenter.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxDrawLinkToMasterCenter.Location = new System.Drawing.Point(11, 28);
            this.checkBoxDrawLinkToMasterCenter.Name = "checkBoxDrawLinkToMasterCenter";
            this.checkBoxDrawLinkToMasterCenter.Size = new System.Drawing.Size(188, 17);
            this.checkBoxDrawLinkToMasterCenter.TabIndex = 3;
            this.checkBoxDrawLinkToMasterCenter.Text = "Draw Branches to Master Centroid";
            this.checkBoxDrawLinkToMasterCenter.UseVisualStyleBackColor = true;
            this.checkBoxDrawLinkToMasterCenter.CheckedChanged += new System.EventHandler(this.checkBoxDrawLinkToMasterCenter_CheckedChanged);
            // 
            // checkBoxDisplayBranchToCenterDistance
            // 
            this.checkBoxDisplayBranchToCenterDistance.AutoSize = true;
            this.checkBoxDisplayBranchToCenterDistance.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxDisplayBranchToCenterDistance.Location = new System.Drawing.Point(29, 52);
            this.checkBoxDisplayBranchToCenterDistance.Name = "checkBoxDisplayBranchToCenterDistance";
            this.checkBoxDisplayBranchToCenterDistance.Size = new System.Drawing.Size(121, 17);
            this.checkBoxDisplayBranchToCenterDistance.TabIndex = 4;
            this.checkBoxDisplayBranchToCenterDistance.Text = "Display distance too";
            this.checkBoxDisplayBranchToCenterDistance.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayBranchToEdgesDistance
            // 
            this.checkBoxDisplayBranchToEdgesDistance.AutoSize = true;
            this.checkBoxDisplayBranchToEdgesDistance.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxDisplayBranchToEdgesDistance.Location = new System.Drawing.Point(29, 55);
            this.checkBoxDisplayBranchToEdgesDistance.Name = "checkBoxDisplayBranchToEdgesDistance";
            this.checkBoxDisplayBranchToEdgesDistance.Size = new System.Drawing.Size(121, 17);
            this.checkBoxDisplayBranchToEdgesDistance.TabIndex = 6;
            this.checkBoxDisplayBranchToEdgesDistance.Text = "Display distance too";
            this.checkBoxDisplayBranchToEdgesDistance.UseVisualStyleBackColor = true;
            // 
            // checkBoxDrawLinkToMasterEdges
            // 
            this.checkBoxDrawLinkToMasterEdges.AutoSize = true;
            this.checkBoxDrawLinkToMasterEdges.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxDrawLinkToMasterEdges.Location = new System.Drawing.Point(20, 29);
            this.checkBoxDrawLinkToMasterEdges.Name = "checkBoxDrawLinkToMasterEdges";
            this.checkBoxDrawLinkToMasterEdges.Size = new System.Drawing.Size(179, 17);
            this.checkBoxDrawLinkToMasterEdges.TabIndex = 5;
            this.checkBoxDrawLinkToMasterEdges.Text = "Draw Branches to Master Edges";
            this.checkBoxDrawLinkToMasterEdges.UseVisualStyleBackColor = true;
            // 
            // groupBoxCentroid
            // 
            this.groupBoxCentroid.Controls.Add(this.checkBoxDrawLinkToMasterCenter);
            this.groupBoxCentroid.Controls.Add(this.checkBoxDisplayBranchToCenterDistance);
            this.groupBoxCentroid.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBoxCentroid.Location = new System.Drawing.Point(12, 57);
            this.groupBoxCentroid.Name = "groupBoxCentroid";
            this.groupBoxCentroid.Size = new System.Drawing.Size(238, 87);
            this.groupBoxCentroid.TabIndex = 7;
            this.groupBoxCentroid.TabStop = false;
            this.groupBoxCentroid.Text = "Centroid";
            this.groupBoxCentroid.Visible = false;
            // 
            // groupBoxEdges
            // 
            this.groupBoxEdges.Controls.Add(this.checkBoxDrawLinkToMasterEdges);
            this.groupBoxEdges.Controls.Add(this.checkBoxDisplayBranchToEdgesDistance);
            this.groupBoxEdges.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBoxEdges.Location = new System.Drawing.Point(12, 150);
            this.groupBoxEdges.Name = "groupBoxEdges";
            this.groupBoxEdges.Size = new System.Drawing.Size(238, 87);
            this.groupBoxEdges.TabIndex = 8;
            this.groupBoxEdges.TabStop = false;
            this.groupBoxEdges.Text = "Edges";
            this.groupBoxEdges.Visible = false;
            // 
            // FormForLinkToMasterInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(262, 313);
            this.Controls.Add(this.groupBoxEdges);
            this.Controls.Add(this.groupBoxCentroid);
            this.Controls.Add(this.numericUpDownDistanceMaxToMaster);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOK);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormForLinkToMasterInfo";
            this.ShowIcon = false;
            this.Text = "Master - Information";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDistanceMaxToMaster)).EndInit();
            this.groupBoxCentroid.ResumeLayout(false);
            this.groupBoxCentroid.PerformLayout();
            this.groupBoxEdges.ResumeLayout(false);
            this.groupBoxEdges.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDownDistanceMaxToMaster;
        public System.Windows.Forms.CheckBox checkBoxDrawLinkToMasterCenter;
        public System.Windows.Forms.CheckBox checkBoxDisplayBranchToCenterDistance;
        public System.Windows.Forms.CheckBox checkBoxDisplayBranchToEdgesDistance;
        public System.Windows.Forms.CheckBox checkBoxDrawLinkToMasterEdges;
        public System.Windows.Forms.GroupBox groupBoxCentroid;
        public System.Windows.Forms.GroupBox groupBoxEdges;
    }
}