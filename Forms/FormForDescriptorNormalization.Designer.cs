namespace HCSAnalyzer.Forms
{
    partial class FormForDescriptorNormalization
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForDescriptorNormalization));
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonOperationSubstract = new System.Windows.Forms.RadioButton();
            this.radioButtonOperationAdd = new System.Windows.Forms.RadioButton();
            this.radioButtonOperationMultiply = new System.Windows.Forms.RadioButton();
            this.radioButtonOperationDivide = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDownValueConst = new System.Windows.Forms.NumericUpDown();
            this.radioButtonValueConstant = new System.Windows.Forms.RadioButton();
            this.radioButtonValueCurrentActiveValue = new System.Windows.Forms.RadioButton();
            this.textBoxForDescName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValueConst)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(214, 157);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(102, 26);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonOperationSubstract);
            this.groupBox1.Controls.Add(this.radioButtonOperationAdd);
            this.groupBox1.Controls.Add(this.radioButtonOperationMultiply);
            this.groupBox1.Controls.Add(this.radioButtonOperationDivide);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(110, 145);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation";
            // 
            // radioButtonOperationSubstract
            // 
            this.radioButtonOperationSubstract.AutoSize = true;
            this.radioButtonOperationSubstract.Location = new System.Drawing.Point(22, 117);
            this.radioButtonOperationSubstract.Name = "radioButtonOperationSubstract";
            this.radioButtonOperationSubstract.Size = new System.Drawing.Size(70, 17);
            this.radioButtonOperationSubstract.TabIndex = 3;
            this.radioButtonOperationSubstract.Text = "Substract";
            this.radioButtonOperationSubstract.UseVisualStyleBackColor = true;
            // 
            // radioButtonOperationAdd
            // 
            this.radioButtonOperationAdd.AutoSize = true;
            this.radioButtonOperationAdd.Location = new System.Drawing.Point(22, 86);
            this.radioButtonOperationAdd.Name = "radioButtonOperationAdd";
            this.radioButtonOperationAdd.Size = new System.Drawing.Size(44, 17);
            this.radioButtonOperationAdd.TabIndex = 2;
            this.radioButtonOperationAdd.Text = "Add";
            this.radioButtonOperationAdd.UseVisualStyleBackColor = true;
            // 
            // radioButtonOperationMultiply
            // 
            this.radioButtonOperationMultiply.AutoSize = true;
            this.radioButtonOperationMultiply.Location = new System.Drawing.Point(22, 55);
            this.radioButtonOperationMultiply.Name = "radioButtonOperationMultiply";
            this.radioButtonOperationMultiply.Size = new System.Drawing.Size(60, 17);
            this.radioButtonOperationMultiply.TabIndex = 1;
            this.radioButtonOperationMultiply.Text = "Multiply";
            this.radioButtonOperationMultiply.UseVisualStyleBackColor = true;
            // 
            // radioButtonOperationDivide
            // 
            this.radioButtonOperationDivide.AutoSize = true;
            this.radioButtonOperationDivide.Checked = true;
            this.radioButtonOperationDivide.Location = new System.Drawing.Point(22, 24);
            this.radioButtonOperationDivide.Name = "radioButtonOperationDivide";
            this.radioButtonOperationDivide.Size = new System.Drawing.Size(55, 17);
            this.radioButtonOperationDivide.TabIndex = 0;
            this.radioButtonOperationDivide.TabStop = true;
            this.radioButtonOperationDivide.Text = "Divide";
            this.radioButtonOperationDivide.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxForDescName);
            this.groupBox2.Controls.Add(this.numericUpDownValueConst);
            this.groupBox2.Controls.Add(this.radioButtonValueConstant);
            this.groupBox2.Controls.Add(this.radioButtonValueCurrentActiveValue);
            this.groupBox2.Location = new System.Drawing.Point(123, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(193, 142);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Value";
            // 
            // numericUpDownValueConst
            // 
            this.numericUpDownValueConst.DecimalPlaces = 4;
            this.numericUpDownValueConst.Location = new System.Drawing.Point(90, 81);
            this.numericUpDownValueConst.Maximum = new decimal(new int[] {
            -469762048,
            -590869294,
            5421010,
            0});
            this.numericUpDownValueConst.Name = "numericUpDownValueConst";
            this.numericUpDownValueConst.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownValueConst.TabIndex = 2;
            this.numericUpDownValueConst.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // radioButtonValueConstant
            // 
            this.radioButtonValueConstant.AutoSize = true;
            this.radioButtonValueConstant.Location = new System.Drawing.Point(17, 82);
            this.radioButtonValueConstant.Name = "radioButtonValueConstant";
            this.radioButtonValueConstant.Size = new System.Drawing.Size(67, 17);
            this.radioButtonValueConstant.TabIndex = 1;
            this.radioButtonValueConstant.Text = "Constant";
            this.radioButtonValueConstant.UseVisualStyleBackColor = true;
            // 
            // radioButtonValueCurrentActiveValue
            // 
            this.radioButtonValueCurrentActiveValue.AutoSize = true;
            this.radioButtonValueCurrentActiveValue.Checked = true;
            this.radioButtonValueCurrentActiveValue.Location = new System.Drawing.Point(17, 21);
            this.radioButtonValueCurrentActiveValue.Name = "radioButtonValueCurrentActiveValue";
            this.radioButtonValueCurrentActiveValue.Size = new System.Drawing.Size(140, 17);
            this.radioButtonValueCurrentActiveValue.TabIndex = 0;
            this.radioButtonValueCurrentActiveValue.TabStop = true;
            this.radioButtonValueCurrentActiveValue.Text = "Current active descriptor";
            this.radioButtonValueCurrentActiveValue.UseVisualStyleBackColor = true;
            // 
            // textBoxForDescName
            // 
            this.textBoxForDescName.Location = new System.Drawing.Point(38, 44);
            this.textBoxForDescName.Name = "textBoxForDescName";
            this.textBoxForDescName.ReadOnly = true;
            this.textBoxForDescName.Size = new System.Drawing.Size(143, 20);
            this.textBoxForDescName.TabIndex = 4;
            // 
            // FormForDescriptorNormalization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 188);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForDescriptorNormalization";
            this.Text = "Descriptors Normalization";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValueConst)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.RadioButton radioButtonOperationSubstract;
        public System.Windows.Forms.RadioButton radioButtonOperationAdd;
        public System.Windows.Forms.RadioButton radioButtonOperationMultiply;
        public System.Windows.Forms.RadioButton radioButtonOperationDivide;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.NumericUpDown numericUpDownValueConst;
        public System.Windows.Forms.RadioButton radioButtonValueConstant;
        public System.Windows.Forms.RadioButton radioButtonValueCurrentActiveValue;
        public System.Windows.Forms.TextBox textBoxForDescName;
    }
}