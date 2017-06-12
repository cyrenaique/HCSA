namespace HCSAnalyzer.Cell_by_Cell_and_DB
{
    partial class FormForSubPopulationId
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForSubPopulationId));
            this.numericUpDownKernelSize = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonProcess = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownMapWidth = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownMapHeight = new System.Windows.Forms.NumericUpDown();
            this.buttonApply = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.checkBoxNormalized = new System.Windows.Forms.CheckBox();
            this.checkBoxUpdatePhenotypeName = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayResultingImage = new System.Windows.Forms.CheckBox();
            this.pictureBoxForImage = new System.Windows.Forms.PictureBox();
            this.radioButton2D = new System.Windows.Forms.RadioButton();
            this.radioButton3D = new System.Windows.Forms.RadioButton();
            this.numericUpDownMapDepth = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxModifyColors = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKernelSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMapWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMapHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMapDepth)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownKernelSize
            // 
            this.numericUpDownKernelSize.Location = new System.Drawing.Point(99, 42);
            this.numericUpDownKernelSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownKernelSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownKernelSize.Name = "numericUpDownKernelSize";
            this.numericUpDownKernelSize.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownKernelSize.TabIndex = 1;
            this.numericUpDownKernelSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Kernel Size";
            // 
            // buttonProcess
            // 
            this.buttonProcess.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonProcess.Location = new System.Drawing.Point(12, 124);
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Size = new System.Drawing.Size(103, 25);
            this.buttonProcess.TabIndex = 3;
            this.buttonProcess.Text = "Process";
            this.buttonProcess.UseVisualStyleBackColor = true;
            this.buttonProcess.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Map Dimensions";
            // 
            // numericUpDownMapWidth
            // 
            this.numericUpDownMapWidth.Location = new System.Drawing.Point(99, 71);
            this.numericUpDownMapWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownMapWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMapWidth.Name = "numericUpDownMapWidth";
            this.numericUpDownMapWidth.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownMapWidth.TabIndex = 5;
            this.numericUpDownMapWidth.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(154, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "x";
            // 
            // numericUpDownMapHeight
            // 
            this.numericUpDownMapHeight.Location = new System.Drawing.Point(166, 71);
            this.numericUpDownMapHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownMapHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMapHeight.Name = "numericUpDownMapHeight";
            this.numericUpDownMapHeight.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownMapHeight.TabIndex = 7;
            this.numericUpDownMapHeight.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // buttonApply
            // 
            this.buttonApply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonApply.Enabled = false;
            this.buttonApply.Location = new System.Drawing.Point(12, 249);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(103, 25);
            this.buttonApply.TabIndex = 8;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Threshold";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 4;
            this.numericUpDown1.Location = new System.Drawing.Point(99, 168);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(79, 20);
            this.numericUpDown1.TabIndex = 9;
            this.numericUpDown1.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // checkBoxNormalized
            // 
            this.checkBoxNormalized.AutoSize = true;
            this.checkBoxNormalized.Checked = true;
            this.checkBoxNormalized.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNormalized.Location = new System.Drawing.Point(14, 99);
            this.checkBoxNormalized.Name = "checkBoxNormalized";
            this.checkBoxNormalized.Size = new System.Drawing.Size(72, 17);
            this.checkBoxNormalized.TabIndex = 11;
            this.checkBoxNormalized.Text = "Normalize";
            this.checkBoxNormalized.UseVisualStyleBackColor = true;
            // 
            // checkBoxUpdatePhenotypeName
            // 
            this.checkBoxUpdatePhenotypeName.AutoSize = true;
            this.checkBoxUpdatePhenotypeName.Checked = true;
            this.checkBoxUpdatePhenotypeName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUpdatePhenotypeName.Location = new System.Drawing.Point(14, 198);
            this.checkBoxUpdatePhenotypeName.Name = "checkBoxUpdatePhenotypeName";
            this.checkBoxUpdatePhenotypeName.Size = new System.Drawing.Size(151, 17);
            this.checkBoxUpdatePhenotypeName.TabIndex = 12;
            this.checkBoxUpdatePhenotypeName.Text = "Update Phenotype Names";
            this.checkBoxUpdatePhenotypeName.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayResultingImage
            // 
            this.checkBoxDisplayResultingImage.AutoSize = true;
            this.checkBoxDisplayResultingImage.Checked = true;
            this.checkBoxDisplayResultingImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDisplayResultingImage.Location = new System.Drawing.Point(14, 223);
            this.checkBoxDisplayResultingImage.Name = "checkBoxDisplayResultingImage";
            this.checkBoxDisplayResultingImage.Size = new System.Drawing.Size(139, 17);
            this.checkBoxDisplayResultingImage.TabIndex = 13;
            this.checkBoxDisplayResultingImage.Text = "Display Resulting Image";
            this.checkBoxDisplayResultingImage.UseVisualStyleBackColor = true;
            // 
            // pictureBoxForImage
            // 
            this.pictureBoxForImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxForImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxForImage.Location = new System.Drawing.Point(335, 11);
            this.pictureBoxForImage.Name = "pictureBoxForImage";
            this.pictureBoxForImage.Size = new System.Drawing.Size(256, 256);
            this.pictureBoxForImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxForImage.TabIndex = 14;
            this.pictureBoxForImage.TabStop = false;
            // 
            // radioButton2D
            // 
            this.radioButton2D.AutoSize = true;
            this.radioButton2D.Checked = true;
            this.radioButton2D.Location = new System.Drawing.Point(15, 11);
            this.radioButton2D.Name = "radioButton2D";
            this.radioButton2D.Size = new System.Drawing.Size(39, 17);
            this.radioButton2D.TabIndex = 15;
            this.radioButton2D.TabStop = true;
            this.radioButton2D.Text = "2D";
            this.radioButton2D.UseVisualStyleBackColor = true;
            this.radioButton2D.CheckedChanged += new System.EventHandler(this.radioButton2D_CheckedChanged);
            // 
            // radioButton3D
            // 
            this.radioButton3D.AutoSize = true;
            this.radioButton3D.Location = new System.Drawing.Point(99, 12);
            this.radioButton3D.Name = "radioButton3D";
            this.radioButton3D.Size = new System.Drawing.Size(39, 17);
            this.radioButton3D.TabIndex = 16;
            this.radioButton3D.Text = "3D";
            this.radioButton3D.UseVisualStyleBackColor = true;
            this.radioButton3D.CheckedChanged += new System.EventHandler(this.radioButton3D_CheckedChanged);
            // 
            // numericUpDownMapDepth
            // 
            this.numericUpDownMapDepth.Enabled = false;
            this.numericUpDownMapDepth.Location = new System.Drawing.Point(234, 71);
            this.numericUpDownMapDepth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownMapDepth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMapDepth.Name = "numericUpDownMapDepth";
            this.numericUpDownMapDepth.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownMapDepth.TabIndex = 17;
            this.numericUpDownMapDepth.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(221, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "x";
            // 
            // checkBoxModifyColors
            // 
            this.checkBoxModifyColors.AutoSize = true;
            this.checkBoxModifyColors.Checked = true;
            this.checkBoxModifyColors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxModifyColors.Location = new System.Drawing.Point(104, 100);
            this.checkBoxModifyColors.Name = "checkBoxModifyColors";
            this.checkBoxModifyColors.Size = new System.Drawing.Size(93, 17);
            this.checkBoxModifyColors.TabIndex = 19;
            this.checkBoxModifyColors.Text = "Update Colors";
            this.checkBoxModifyColors.UseVisualStyleBackColor = true;
            // 
            // FormForSubPopulationId
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 281);
            this.Controls.Add(this.checkBoxModifyColors);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownMapDepth);
            this.Controls.Add(this.radioButton3D);
            this.Controls.Add(this.radioButton2D);
            this.Controls.Add(this.pictureBoxForImage);
            this.Controls.Add(this.checkBoxDisplayResultingImage);
            this.Controls.Add(this.checkBoxUpdatePhenotypeName);
            this.Controls.Add(this.checkBoxNormalized);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.numericUpDownMapHeight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownMapWidth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonProcess);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownKernelSize);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(619, 317);
            this.Name = "FormForSubPopulationId";
            this.Text = "Sub-Population Identification";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKernelSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMapWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMapHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMapDepth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonProcess;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDownMapWidth;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown numericUpDownMapHeight;
        public System.Windows.Forms.NumericUpDown numericUpDownKernelSize;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.CheckBox checkBoxNormalized;
        private System.Windows.Forms.CheckBox checkBoxUpdatePhenotypeName;
        private System.Windows.Forms.CheckBox checkBoxDisplayResultingImage;
        private System.Windows.Forms.PictureBox pictureBoxForImage;
        public System.Windows.Forms.RadioButton radioButton2D;
        public System.Windows.Forms.RadioButton radioButton3D;
        public System.Windows.Forms.NumericUpDown numericUpDownMapDepth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxModifyColors;
    }
}