namespace HCSAnalyzer.Forms.FormsForOptions.PanelForOptions
{
    partial class PanelForPlatesandWells
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.numericUpDownGutter = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxForWellInfo = new System.Windows.Forms.GroupBox();
            this.radioButtonWellInfoDescValue = new System.Windows.Forms.RadioButton();
            this.radioButtonWellInfoConcentration = new System.Windows.Forms.RadioButton();
            this.radioButtonWellInfoLocusID = new System.Windows.Forms.RadioButton();
            this.radioButtonWellInfoInfo = new System.Windows.Forms.RadioButton();
            this.radioButtonWellInfoName = new System.Windows.Forms.RadioButton();
            this.checkBoxDisplayWellInformation = new System.Windows.Forms.CheckBox();
            this.panel.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGutter)).BeginInit();
            this.groupBoxForWellInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.checkBoxDisplayWellInformation);
            this.panel.Controls.Add(this.groupBox6);
            this.panel.Controls.Add(this.groupBoxForWellInfo);
            this.panel.Location = new System.Drawing.Point(45, 21);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(269, 263);
            this.panel.TabIndex = 1;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.numericUpDownGutter);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Location = new System.Drawing.Point(6, 150);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(257, 53);
            this.groupBox6.TabIndex = 20;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Plate Design";
            // 
            // numericUpDownGutter
            // 
            this.numericUpDownGutter.Location = new System.Drawing.Point(50, 21);
            this.numericUpDownGutter.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownGutter.Name = "numericUpDownGutter";
            this.numericUpDownGutter.Size = new System.Drawing.Size(97, 20);
            this.numericUpDownGutter.TabIndex = 1;
            this.numericUpDownGutter.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Gutter";
            // 
            // groupBoxForWellInfo
            // 
            this.groupBoxForWellInfo.Controls.Add(this.radioButtonWellInfoDescValue);
            this.groupBoxForWellInfo.Controls.Add(this.radioButtonWellInfoConcentration);
            this.groupBoxForWellInfo.Controls.Add(this.radioButtonWellInfoLocusID);
            this.groupBoxForWellInfo.Controls.Add(this.radioButtonWellInfoInfo);
            this.groupBoxForWellInfo.Controls.Add(this.radioButtonWellInfoName);
            this.groupBoxForWellInfo.Enabled = false;
            this.groupBoxForWellInfo.Location = new System.Drawing.Point(6, 8);
            this.groupBoxForWellInfo.Name = "groupBoxForWellInfo";
            this.groupBoxForWellInfo.Size = new System.Drawing.Size(257, 136);
            this.groupBoxForWellInfo.TabIndex = 19;
            this.groupBoxForWellInfo.TabStop = false;
            this.groupBoxForWellInfo.Text = "                                            ";
            // 
            // radioButtonWellInfoDescValue
            // 
            this.radioButtonWellInfoDescValue.AutoSize = true;
            this.radioButtonWellInfoDescValue.Location = new System.Drawing.Point(11, 42);
            this.radioButtonWellInfoDescValue.Name = "radioButtonWellInfoDescValue";
            this.radioButtonWellInfoDescValue.Size = new System.Drawing.Size(82, 17);
            this.radioButtonWellInfoDescValue.TabIndex = 20;
            this.radioButtonWellInfoDescValue.Text = "Mean Value";
            this.radioButtonWellInfoDescValue.UseVisualStyleBackColor = true;
            // 
            // radioButtonWellInfoConcentration
            // 
            this.radioButtonWellInfoConcentration.AutoSize = true;
            this.radioButtonWellInfoConcentration.Location = new System.Drawing.Point(11, 111);
            this.radioButtonWellInfoConcentration.Name = "radioButtonWellInfoConcentration";
            this.radioButtonWellInfoConcentration.Size = new System.Drawing.Size(91, 17);
            this.radioButtonWellInfoConcentration.TabIndex = 19;
            this.radioButtonWellInfoConcentration.Text = "Concentration";
            this.radioButtonWellInfoConcentration.UseVisualStyleBackColor = true;
            // 
            // radioButtonWellInfoLocusID
            // 
            this.radioButtonWellInfoLocusID.AutoSize = true;
            this.radioButtonWellInfoLocusID.Location = new System.Drawing.Point(11, 88);
            this.radioButtonWellInfoLocusID.Name = "radioButtonWellInfoLocusID";
            this.radioButtonWellInfoLocusID.Size = new System.Drawing.Size(68, 17);
            this.radioButtonWellInfoLocusID.TabIndex = 19;
            this.radioButtonWellInfoLocusID.Text = "Locus ID";
            this.radioButtonWellInfoLocusID.UseVisualStyleBackColor = true;
            // 
            // radioButtonWellInfoInfo
            // 
            this.radioButtonWellInfoInfo.AutoSize = true;
            this.radioButtonWellInfoInfo.Location = new System.Drawing.Point(11, 65);
            this.radioButtonWellInfoInfo.Name = "radioButtonWellInfoInfo";
            this.radioButtonWellInfoInfo.Size = new System.Drawing.Size(43, 17);
            this.radioButtonWellInfoInfo.TabIndex = 19;
            this.radioButtonWellInfoInfo.Text = "Info";
            this.radioButtonWellInfoInfo.UseVisualStyleBackColor = true;
            // 
            // radioButtonWellInfoName
            // 
            this.radioButtonWellInfoName.AutoSize = true;
            this.radioButtonWellInfoName.Checked = true;
            this.radioButtonWellInfoName.Location = new System.Drawing.Point(11, 19);
            this.radioButtonWellInfoName.Name = "radioButtonWellInfoName";
            this.radioButtonWellInfoName.Size = new System.Drawing.Size(53, 17);
            this.radioButtonWellInfoName.TabIndex = 19;
            this.radioButtonWellInfoName.TabStop = true;
            this.radioButtonWellInfoName.Text = "Name";
            this.radioButtonWellInfoName.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayWellInformation
            // 
            this.checkBoxDisplayWellInformation.AutoSize = true;
            this.checkBoxDisplayWellInformation.Location = new System.Drawing.Point(16, 5);
            this.checkBoxDisplayWellInformation.Name = "checkBoxDisplayWellInformation";
            this.checkBoxDisplayWellInformation.Size = new System.Drawing.Size(135, 17);
            this.checkBoxDisplayWellInformation.TabIndex = 18;
            this.checkBoxDisplayWellInformation.Text = "Display well information";
            this.checkBoxDisplayWellInformation.UseVisualStyleBackColor = true;
            this.checkBoxDisplayWellInformation.CheckedChanged += new System.EventHandler(this.checkBoxDisplayWellInformation_CheckedChanged);
            // 
            // PanelForPlatesandWells
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForPlatesandWells";
            this.Size = new System.Drawing.Size(539, 428);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGutter)).EndInit();
            this.groupBoxForWellInfo.ResumeLayout(false);
            this.groupBoxForWellInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        private System.Windows.Forms.GroupBox groupBox6;
        public System.Windows.Forms.NumericUpDown numericUpDownGutter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxForWellInfo;
        public System.Windows.Forms.RadioButton radioButtonWellInfoDescValue;
        public System.Windows.Forms.RadioButton radioButtonWellInfoConcentration;
        public System.Windows.Forms.RadioButton radioButtonWellInfoLocusID;
        public System.Windows.Forms.RadioButton radioButtonWellInfoInfo;
        public System.Windows.Forms.RadioButton radioButtonWellInfoName;
        public System.Windows.Forms.CheckBox checkBoxDisplayWellInformation;

    }
}
