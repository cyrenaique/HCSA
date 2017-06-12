namespace HCSAnalyzer.Forms.IO
{
    partial class FormInfoForFileImporter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInfoForFileImporter));
            this.numericUpDownHeaderSize = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.richTextBoxTextHeader = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonSpace = new System.Windows.Forms.RadioButton();
            this.radioButtonSemiColon = new System.Windows.Forms.RadioButton();
            this.radioButtonTab = new System.Windows.Forms.RadioButton();
            this.radioButtonComma = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownPreviewSize = new System.Windows.Forms.NumericUpDown();
            this.groupBoxPositionMode = new System.Windows.Forms.GroupBox();
            this.radioButtonWellPosModeA_2 = new System.Windows.Forms.RadioButton();
            this.radioButtonWellPosMode1_2 = new System.Windows.Forms.RadioButton();
            this.radioButtonWellPosModeA_02 = new System.Windows.Forms.RadioButton();
            this.radioButtonWellPosModeA02 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeaderSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPreviewSize)).BeginInit();
            this.groupBoxPositionMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // numericUpDownHeaderSize
            // 
            this.numericUpDownHeaderSize.Location = new System.Drawing.Point(15, 32);
            this.numericUpDownHeaderSize.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownHeaderSize.Name = "numericUpDownHeaderSize";
            this.numericUpDownHeaderSize.Size = new System.Drawing.Size(121, 20);
            this.numericUpDownHeaderSize.TabIndex = 0;
            this.numericUpDownHeaderSize.ValueChanged += new System.EventHandler(this.numericUpDownHeaderSize_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Header Size";
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonNext.Location = new System.Drawing.Point(316, 280);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 2;
            this.buttonNext.Text = "Next ->";
            this.buttonNext.UseVisualStyleBackColor = true;
            // 
            // richTextBoxTextHeader
            // 
            this.richTextBoxTextHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxTextHeader.Location = new System.Drawing.Point(6, 149);
            this.richTextBoxTextHeader.Name = "richTextBoxTextHeader";
            this.richTextBoxTextHeader.ReadOnly = true;
            this.richTextBoxTextHeader.Size = new System.Drawing.Size(385, 126);
            this.richTextBoxTextHeader.TabIndex = 3;
            this.richTextBoxTextHeader.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonSpace);
            this.groupBox1.Controls.Add(this.radioButtonSemiColon);
            this.groupBox1.Controls.Add(this.radioButtonTab);
            this.groupBox1.Controls.Add(this.radioButtonComma);
            this.groupBox1.Location = new System.Drawing.Point(153, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(110, 131);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Delimiters";
            // 
            // radioButtonSpace
            // 
            this.radioButtonSpace.AutoSize = true;
            this.radioButtonSpace.Location = new System.Drawing.Point(18, 101);
            this.radioButtonSpace.Name = "radioButtonSpace";
            this.radioButtonSpace.Size = new System.Drawing.Size(56, 17);
            this.radioButtonSpace.TabIndex = 3;
            this.radioButtonSpace.Text = "Space";
            this.radioButtonSpace.UseVisualStyleBackColor = true;
            // 
            // radioButtonSemiColon
            // 
            this.radioButtonSemiColon.AutoSize = true;
            this.radioButtonSemiColon.Location = new System.Drawing.Point(18, 74);
            this.radioButtonSemiColon.Name = "radioButtonSemiColon";
            this.radioButtonSemiColon.Size = new System.Drawing.Size(74, 17);
            this.radioButtonSemiColon.TabIndex = 2;
            this.radioButtonSemiColon.Text = "Semicolon";
            this.radioButtonSemiColon.UseVisualStyleBackColor = true;
            // 
            // radioButtonTab
            // 
            this.radioButtonTab.AutoSize = true;
            this.radioButtonTab.Location = new System.Drawing.Point(18, 47);
            this.radioButtonTab.Name = "radioButtonTab";
            this.radioButtonTab.Size = new System.Drawing.Size(44, 17);
            this.radioButtonTab.TabIndex = 1;
            this.radioButtonTab.Text = "Tab";
            this.radioButtonTab.UseVisualStyleBackColor = true;
            // 
            // radioButtonComma
            // 
            this.radioButtonComma.AutoSize = true;
            this.radioButtonComma.Checked = true;
            this.radioButtonComma.Location = new System.Drawing.Point(18, 20);
            this.radioButtonComma.Name = "radioButtonComma";
            this.radioButtonComma.Size = new System.Drawing.Size(60, 17);
            this.radioButtonComma.TabIndex = 0;
            this.radioButtonComma.TabStop = true;
            this.radioButtonComma.Text = "Comma";
            this.radioButtonComma.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Preview Size";
            // 
            // numericUpDownPreviewSize
            // 
            this.numericUpDownPreviewSize.Location = new System.Drawing.Point(15, 83);
            this.numericUpDownPreviewSize.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownPreviewSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPreviewSize.Name = "numericUpDownPreviewSize";
            this.numericUpDownPreviewSize.Size = new System.Drawing.Size(121, 20);
            this.numericUpDownPreviewSize.TabIndex = 5;
            this.numericUpDownPreviewSize.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // groupBoxPositionMode
            // 
            this.groupBoxPositionMode.Controls.Add(this.radioButtonWellPosModeA_2);
            this.groupBoxPositionMode.Controls.Add(this.radioButtonWellPosMode1_2);
            this.groupBoxPositionMode.Controls.Add(this.radioButtonWellPosModeA_02);
            this.groupBoxPositionMode.Controls.Add(this.radioButtonWellPosModeA02);
            this.groupBoxPositionMode.Location = new System.Drawing.Point(273, 12);
            this.groupBoxPositionMode.Name = "groupBoxPositionMode";
            this.groupBoxPositionMode.Size = new System.Drawing.Size(119, 131);
            this.groupBoxPositionMode.TabIndex = 5;
            this.groupBoxPositionMode.TabStop = false;
            this.groupBoxPositionMode.Text = "Well Position Mode";
            // 
            // radioButtonWellPosModeA_2
            // 
            this.radioButtonWellPosModeA_2.AutoSize = true;
            this.radioButtonWellPosModeA_2.Location = new System.Drawing.Point(18, 101);
            this.radioButtonWellPosModeA_2.Name = "radioButtonWellPosModeA_2";
            this.radioButtonWellPosModeA_2.Size = new System.Drawing.Size(53, 17);
            this.radioButtonWellPosModeA_2.TabIndex = 3;
            this.radioButtonWellPosModeA_2.Text = "[A - 2]";
            this.radioButtonWellPosModeA_2.UseVisualStyleBackColor = true;
            // 
            // radioButtonWellPosMode1_2
            // 
            this.radioButtonWellPosMode1_2.AutoSize = true;
            this.radioButtonWellPosMode1_2.Location = new System.Drawing.Point(18, 74);
            this.radioButtonWellPosMode1_2.Name = "radioButtonWellPosMode1_2";
            this.radioButtonWellPosMode1_2.Size = new System.Drawing.Size(52, 17);
            this.radioButtonWellPosMode1_2.TabIndex = 2;
            this.radioButtonWellPosMode1_2.Text = "[1] [2]";
            this.radioButtonWellPosMode1_2.UseVisualStyleBackColor = true;
            // 
            // radioButtonWellPosModeA_02
            // 
            this.radioButtonWellPosModeA_02.AutoSize = true;
            this.radioButtonWellPosModeA_02.Location = new System.Drawing.Point(18, 47);
            this.radioButtonWellPosModeA_02.Name = "radioButtonWellPosModeA_02";
            this.radioButtonWellPosModeA_02.Size = new System.Drawing.Size(59, 17);
            this.radioButtonWellPosModeA_02.TabIndex = 1;
            this.radioButtonWellPosModeA_02.Text = "[A] [02]";
            this.radioButtonWellPosModeA_02.UseVisualStyleBackColor = true;
            // 
            // radioButtonWellPosModeA02
            // 
            this.radioButtonWellPosModeA02.AutoSize = true;
            this.radioButtonWellPosModeA02.Checked = true;
            this.radioButtonWellPosModeA02.Location = new System.Drawing.Point(18, 20);
            this.radioButtonWellPosModeA02.Name = "radioButtonWellPosModeA02";
            this.radioButtonWellPosModeA02.Size = new System.Drawing.Size(50, 17);
            this.radioButtonWellPosModeA02.TabIndex = 0;
            this.radioButtonWellPosModeA02.TabStop = true;
            this.radioButtonWellPosModeA02.Text = "[A02]";
            this.radioButtonWellPosModeA02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonWellPosModeA02.UseVisualStyleBackColor = true;
            // 
            // FormInfoForFileImporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 307);
            this.Controls.Add(this.groupBoxPositionMode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownPreviewSize);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.richTextBoxTextHeader);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownHeaderSize);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(415, 345);
            this.Name = "FormInfoForFileImporter";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Importer Options";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeaderSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPreviewSize)).EndInit();
            this.groupBoxPositionMode.ResumeLayout(false);
            this.groupBoxPositionMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.NumericUpDown numericUpDownHeaderSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.RichTextBox richTextBoxTextHeader;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.RadioButton radioButtonSpace;
        public System.Windows.Forms.RadioButton radioButtonSemiColon;
        public System.Windows.Forms.RadioButton radioButtonTab;
        public System.Windows.Forms.RadioButton radioButtonComma;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDownPreviewSize;
        public System.Windows.Forms.RadioButton radioButtonWellPosMode1_2;
        public System.Windows.Forms.RadioButton radioButtonWellPosModeA_02;
        public System.Windows.Forms.RadioButton radioButtonWellPosModeA02;
        public System.Windows.Forms.RadioButton radioButtonWellPosModeA_2;
        public System.Windows.Forms.GroupBox groupBoxPositionMode;
    }
}