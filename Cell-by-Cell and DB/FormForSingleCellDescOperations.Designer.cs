namespace HCSAnalyzer.Cell_by_Cell_and_DB
{
    partial class FormForDescOperations
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForDescOperations));
            this.buttonOk = new System.Windows.Forms.Button();
            this.comboBoxDescriptor1 = new System.Windows.Forms.ComboBox();
            this.comboBoxDescriptor2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.domainUpDown1 = new System.Windows.Forms.DomainUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxNewDescName = new System.Windows.Forms.TextBox();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageBinary = new System.Windows.Forms.TabPage();
            this.tabPageUnary = new System.Windows.Forms.TabPage();
            this.radioButtonEXP = new System.Windows.Forms.RadioButton();
            this.radioButtonABS = new System.Windows.Forms.RadioButton();
            this.richTextBoxInfo = new System.Windows.Forms.RichTextBox();
            this.radioButtonSQRT = new System.Windows.Forms.RadioButton();
            this.radioButtonLog = new System.Windows.Forms.RadioButton();
            this.panelPostProcess = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownPostProcessValue = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.domainUpDownPostProcessOperator = new System.Windows.Forms.DomainUpDown();
            this.checkBoxActivePostProcess = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControlMain.SuspendLayout();
            this.tabPageBinary.SuspendLayout();
            this.tabPageUnary.SuspendLayout();
            this.panelPostProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPostProcessValue)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(246, 347);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(120, 26);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // comboBoxDescriptor1
            // 
            this.comboBoxDescriptor1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDescriptor1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDescriptor1.FormattingEnabled = true;
            this.comboBoxDescriptor1.Location = new System.Drawing.Point(8, 27);
            this.comboBoxDescriptor1.Name = "comboBoxDescriptor1";
            this.comboBoxDescriptor1.Size = new System.Drawing.Size(354, 21);
            this.comboBoxDescriptor1.TabIndex = 10;
            this.comboBoxDescriptor1.SelectedIndexChanged += new System.EventHandler(this.comboBoxDescriptor1_SelectedIndexChanged);
            // 
            // comboBoxDescriptor2
            // 
            this.comboBoxDescriptor2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDescriptor2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDescriptor2.FormattingEnabled = true;
            this.comboBoxDescriptor2.Location = new System.Drawing.Point(6, 75);
            this.comboBoxDescriptor2.Name = "comboBoxDescriptor2";
            this.comboBoxDescriptor2.Size = new System.Drawing.Size(340, 21);
            this.comboBoxDescriptor2.TabIndex = 11;
            this.comboBoxDescriptor2.SelectedIndexChanged += new System.EventHandler(this.comboBoxDescriptor2_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Descriptor A";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Descriptor B";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(93, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Operator";
            // 
            // domainUpDown1
            // 
            this.domainUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.domainUpDown1.Items.Add("*");
            this.domainUpDown1.Items.Add("+");
            this.domainUpDown1.Items.Add("-");
            this.domainUpDown1.Items.Add("/");
            this.domainUpDown1.Location = new System.Drawing.Point(152, 21);
            this.domainUpDown1.Name = "domainUpDown1";
            this.domainUpDown1.Size = new System.Drawing.Size(84, 20);
            this.domainUpDown1.TabIndex = 15;
            this.domainUpDown1.Text = "*";
            this.domainUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.domainUpDown1.SelectedItemChanged += new System.EventHandler(this.domainUpDown1_SelectedItemChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "New Descriptor Name";
            // 
            // textBoxNewDescName
            // 
            this.textBoxNewDescName.Location = new System.Drawing.Point(8, 225);
            this.textBoxNewDescName.Name = "textBoxNewDescName";
            this.textBoxNewDescName.Size = new System.Drawing.Size(354, 20);
            this.textBoxNewDescName.TabIndex = 17;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageBinary);
            this.tabControlMain.Controls.Add(this.tabPageUnary);
            this.tabControlMain.Location = new System.Drawing.Point(4, 56);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(362, 137);
            this.tabControlMain.TabIndex = 18;
            this.tabControlMain.SelectedIndexChanged += new System.EventHandler(this.tabControlMain_SelectedIndexChanged);
            // 
            // tabPageBinary
            // 
            this.tabPageBinary.Controls.Add(this.comboBoxDescriptor2);
            this.tabPageBinary.Controls.Add(this.domainUpDown1);
            this.tabPageBinary.Controls.Add(this.label2);
            this.tabPageBinary.Controls.Add(this.label3);
            this.tabPageBinary.Location = new System.Drawing.Point(4, 22);
            this.tabPageBinary.Name = "tabPageBinary";
            this.tabPageBinary.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBinary.Size = new System.Drawing.Size(354, 111);
            this.tabPageBinary.TabIndex = 0;
            this.tabPageBinary.Text = "Binary";
            this.tabPageBinary.UseVisualStyleBackColor = true;
            // 
            // tabPageUnary
            // 
            this.tabPageUnary.Controls.Add(this.radioButtonEXP);
            this.tabPageUnary.Controls.Add(this.radioButtonABS);
            this.tabPageUnary.Controls.Add(this.richTextBoxInfo);
            this.tabPageUnary.Controls.Add(this.radioButtonSQRT);
            this.tabPageUnary.Controls.Add(this.radioButtonLog);
            this.tabPageUnary.Location = new System.Drawing.Point(4, 22);
            this.tabPageUnary.Name = "tabPageUnary";
            this.tabPageUnary.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUnary.Size = new System.Drawing.Size(354, 111);
            this.tabPageUnary.TabIndex = 1;
            this.tabPageUnary.Text = "Unary";
            this.tabPageUnary.UseVisualStyleBackColor = true;
            // 
            // radioButtonEXP
            // 
            this.radioButtonEXP.AutoSize = true;
            this.radioButtonEXP.Location = new System.Drawing.Point(14, 79);
            this.radioButtonEXP.Name = "radioButtonEXP";
            this.radioButtonEXP.Size = new System.Drawing.Size(43, 17);
            this.radioButtonEXP.TabIndex = 4;
            this.radioButtonEXP.Text = "Exp";
            this.radioButtonEXP.UseVisualStyleBackColor = true;
            this.radioButtonEXP.CheckedChanged += new System.EventHandler(this.radioButtonEXP_CheckedChanged);
            // 
            // radioButtonABS
            // 
            this.radioButtonABS.AutoSize = true;
            this.radioButtonABS.Location = new System.Drawing.Point(14, 56);
            this.radioButtonABS.Name = "radioButtonABS";
            this.radioButtonABS.Size = new System.Drawing.Size(43, 17);
            this.radioButtonABS.TabIndex = 3;
            this.radioButtonABS.Text = "Abs";
            this.radioButtonABS.UseVisualStyleBackColor = true;
            this.radioButtonABS.CheckedChanged += new System.EventHandler(this.radioButtonABS_CheckedChanged);
            // 
            // richTextBoxInfo
            // 
            this.richTextBoxInfo.Location = new System.Drawing.Point(118, 6);
            this.richTextBoxInfo.Name = "richTextBoxInfo";
            this.richTextBoxInfo.ReadOnly = true;
            this.richTextBoxInfo.Size = new System.Drawing.Size(230, 99);
            this.richTextBoxInfo.TabIndex = 2;
            this.richTextBoxInfo.Text = "Return the base e logarithm of the values.\n\nAs the software requires double value" +
                "s, if the original data value is lower or equal to 0, the log value will be defi" +
                "ned as Double.Epsilon.";
            // 
            // radioButtonSQRT
            // 
            this.radioButtonSQRT.AutoSize = true;
            this.radioButtonSQRT.Location = new System.Drawing.Point(14, 33);
            this.radioButtonSQRT.Name = "radioButtonSQRT";
            this.radioButtonSQRT.Size = new System.Drawing.Size(44, 17);
            this.radioButtonSQRT.TabIndex = 1;
            this.radioButtonSQRT.Text = "Sqrt";
            this.radioButtonSQRT.UseVisualStyleBackColor = true;
            this.radioButtonSQRT.CheckedChanged += new System.EventHandler(this.radioButtonSQRT_CheckedChanged);
            // 
            // radioButtonLog
            // 
            this.radioButtonLog.AutoSize = true;
            this.radioButtonLog.Checked = true;
            this.radioButtonLog.Location = new System.Drawing.Point(14, 10);
            this.radioButtonLog.Name = "radioButtonLog";
            this.radioButtonLog.Size = new System.Drawing.Size(43, 17);
            this.radioButtonLog.TabIndex = 0;
            this.radioButtonLog.TabStop = true;
            this.radioButtonLog.Text = "Log";
            this.radioButtonLog.UseVisualStyleBackColor = true;
            this.radioButtonLog.CheckedChanged += new System.EventHandler(this.radioButtonLog_CheckedChanged);
            // 
            // panelPostProcess
            // 
            this.panelPostProcess.Controls.Add(this.label6);
            this.panelPostProcess.Controls.Add(this.numericUpDownPostProcessValue);
            this.panelPostProcess.Controls.Add(this.label5);
            this.panelPostProcess.Controls.Add(this.domainUpDownPostProcessOperator);
            this.panelPostProcess.Enabled = false;
            this.panelPostProcess.Location = new System.Drawing.Point(55, 16);
            this.panelPostProcess.Name = "panelPostProcess";
            this.panelPostProcess.Size = new System.Drawing.Size(253, 60);
            this.panelPostProcess.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(166, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Value";
            // 
            // numericUpDownPostProcessValue
            // 
            this.numericUpDownPostProcessValue.DecimalPlaces = 3;
            this.numericUpDownPostProcessValue.Location = new System.Drawing.Point(129, 30);
            this.numericUpDownPostProcessValue.Maximum = new decimal(new int[] {
            -159383553,
            46653770,
            5421,
            0});
            this.numericUpDownPostProcessValue.Minimum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            -2147483648});
            this.numericUpDownPostProcessValue.Name = "numericUpDownPostProcessValue";
            this.numericUpDownPostProcessValue.Size = new System.Drawing.Size(109, 20);
            this.numericUpDownPostProcessValue.TabIndex = 18;
            this.numericUpDownPostProcessValue.ThousandsSeparator = true;
            this.numericUpDownPostProcessValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPostProcessValue.ValueChanged += new System.EventHandler(this.numericUpDownPostProcessValue_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Operator";
            // 
            // domainUpDownPostProcessOperator
            // 
            this.domainUpDownPostProcessOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.domainUpDownPostProcessOperator.Items.Add("*");
            this.domainUpDownPostProcessOperator.Items.Add("+");
            this.domainUpDownPostProcessOperator.Items.Add("-");
            this.domainUpDownPostProcessOperator.Items.Add("/");
            this.domainUpDownPostProcessOperator.Location = new System.Drawing.Point(23, 30);
            this.domainUpDownPostProcessOperator.Name = "domainUpDownPostProcessOperator";
            this.domainUpDownPostProcessOperator.Size = new System.Drawing.Size(84, 20);
            this.domainUpDownPostProcessOperator.TabIndex = 17;
            this.domainUpDownPostProcessOperator.Text = "*";
            this.domainUpDownPostProcessOperator.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.domainUpDownPostProcessOperator.SelectedItemChanged += new System.EventHandler(this.domainUpDownPostProcessOperator_SelectedItemChanged);
            // 
            // checkBoxActivePostProcess
            // 
            this.checkBoxActivePostProcess.AutoSize = true;
            this.checkBoxActivePostProcess.Location = new System.Drawing.Point(18, 253);
            this.checkBoxActivePostProcess.Name = "checkBoxActivePostProcess";
            this.checkBoxActivePostProcess.Size = new System.Drawing.Size(102, 17);
            this.checkBoxActivePostProcess.TabIndex = 18;
            this.checkBoxActivePostProcess.Text = "Post Processing";
            this.checkBoxActivePostProcess.UseVisualStyleBackColor = true;
            this.checkBoxActivePostProcess.CheckedChanged += new System.EventHandler(this.checkBoxActivePostProcess_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelPostProcess);
            this.groupBox1.Location = new System.Drawing.Point(8, 255);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 89);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "                             ";
            // 
            // FormForDescOperations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 376);
            this.Controls.Add(this.checkBoxActivePostProcess);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBoxDescriptor1);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxNewDescName);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForDescOperations";
            this.Text = "Single Cell Operations";
            this.tabControlMain.ResumeLayout(false);
            this.tabPageBinary.ResumeLayout(false);
            this.tabPageBinary.PerformLayout();
            this.tabPageUnary.ResumeLayout(false);
            this.tabPageUnary.PerformLayout();
            this.panelPostProcess.ResumeLayout(false);
            this.panelPostProcess.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPostProcessValue)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.ComboBox comboBoxDescriptor1;
        public System.Windows.Forms.ComboBox comboBoxDescriptor2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox textBoxNewDescName;
        public System.Windows.Forms.DomainUpDown domainUpDown1;
        private System.Windows.Forms.TabPage tabPageBinary;
        private System.Windows.Forms.TabPage tabPageUnary;
        public System.Windows.Forms.TabControl tabControlMain;
        public System.Windows.Forms.RadioButton radioButtonSQRT;
        public System.Windows.Forms.RadioButton radioButtonLog;
        private System.Windows.Forms.RichTextBox richTextBoxInfo;
        public System.Windows.Forms.RadioButton radioButtonEXP;
        public System.Windows.Forms.RadioButton radioButtonABS;
        private System.Windows.Forms.Panel panelPostProcess;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.DomainUpDown domainUpDownPostProcessOperator;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.NumericUpDown numericUpDownPostProcessValue;
        public System.Windows.Forms.CheckBox checkBoxActivePostProcess;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}