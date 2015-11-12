namespace HCSAnalyzer.Cell_by_Cell_and_DB
{
    partial class FormManualGating
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormManualGating));
            this.buttonOk = new System.Windows.Forms.Button();
            this.textBoxDesc1 = new System.Windows.Forms.TextBox();
            this.numericUpDownDesc1Min = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownDesc1Max = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownDesc2Max = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownDesc2Min = new System.Windows.Forms.NumericUpDown();
            this.textBoxDesc2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDesc1Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDesc1Max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDesc2Max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDesc2Min)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(239, 152);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(106, 29);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Close";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // textBoxDesc1
            // 
            this.textBoxDesc1.Location = new System.Drawing.Point(9, 8);
            this.textBoxDesc1.Name = "textBoxDesc1";
            this.textBoxDesc1.ReadOnly = true;
            this.textBoxDesc1.Size = new System.Drawing.Size(335, 20);
            this.textBoxDesc1.TabIndex = 1;
            // 
            // numericUpDownDesc1Min
            // 
            this.numericUpDownDesc1Min.DecimalPlaces = 4;
            this.numericUpDownDesc1Min.Location = new System.Drawing.Point(44, 44);
            this.numericUpDownDesc1Min.Maximum = new decimal(new int[] {
            -469762049,
            -590869294,
            5421010,
            0});
            this.numericUpDownDesc1Min.Minimum = new decimal(new int[] {
            -469762049,
            -590869294,
            5421010,
            -2147483648});
            this.numericUpDownDesc1Min.Name = "numericUpDownDesc1Min";
            this.numericUpDownDesc1Min.Size = new System.Drawing.Size(116, 20);
            this.numericUpDownDesc1Min.TabIndex = 2;
            this.numericUpDownDesc1Min.ValueChanged += new System.EventHandler(this.numericUpDownDesc1Min_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Min";
            // 
            // numericUpDownDesc1Max
            // 
            this.numericUpDownDesc1Max.DecimalPlaces = 4;
            this.numericUpDownDesc1Max.Location = new System.Drawing.Point(228, 44);
            this.numericUpDownDesc1Max.Maximum = new decimal(new int[] {
            -469762049,
            -590869294,
            5421010,
            0});
            this.numericUpDownDesc1Max.Minimum = new decimal(new int[] {
            -469762049,
            -590869294,
            5421010,
            -2147483648});
            this.numericUpDownDesc1Max.Name = "numericUpDownDesc1Max";
            this.numericUpDownDesc1Max.Size = new System.Drawing.Size(116, 20);
            this.numericUpDownDesc1Max.TabIndex = 4;
            this.numericUpDownDesc1Max.ValueChanged += new System.EventHandler(this.numericUpDownDesc1Max_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Max";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(197, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Max";
            // 
            // numericUpDownDesc2Max
            // 
            this.numericUpDownDesc2Max.DecimalPlaces = 4;
            this.numericUpDownDesc2Max.Location = new System.Drawing.Point(227, 113);
            this.numericUpDownDesc2Max.Maximum = new decimal(new int[] {
            -469762049,
            -590869294,
            5421010,
            0});
            this.numericUpDownDesc2Max.Minimum = new decimal(new int[] {
            -469762049,
            -590869294,
            5421010,
            -2147483648});
            this.numericUpDownDesc2Max.Name = "numericUpDownDesc2Max";
            this.numericUpDownDesc2Max.Size = new System.Drawing.Size(116, 20);
            this.numericUpDownDesc2Max.TabIndex = 9;
            this.numericUpDownDesc2Max.ValueChanged += new System.EventHandler(this.numericUpDownDesc2Max_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Min";
            // 
            // numericUpDownDesc2Min
            // 
            this.numericUpDownDesc2Min.DecimalPlaces = 4;
            this.numericUpDownDesc2Min.Location = new System.Drawing.Point(43, 113);
            this.numericUpDownDesc2Min.Maximum = new decimal(new int[] {
            -469762049,
            -590869294,
            5421010,
            0});
            this.numericUpDownDesc2Min.Minimum = new decimal(new int[] {
            -469762049,
            -590869294,
            5421010,
            -2147483648});
            this.numericUpDownDesc2Min.Name = "numericUpDownDesc2Min";
            this.numericUpDownDesc2Min.Size = new System.Drawing.Size(116, 20);
            this.numericUpDownDesc2Min.TabIndex = 7;
            this.numericUpDownDesc2Min.ValueChanged += new System.EventHandler(this.numericUpDownDesc2Min_ValueChanged);
            // 
            // textBoxDesc2
            // 
            this.textBoxDesc2.Location = new System.Drawing.Point(8, 77);
            this.textBoxDesc2.Name = "textBoxDesc2";
            this.textBoxDesc2.ReadOnly = true;
            this.textBoxDesc2.Size = new System.Drawing.Size(335, 20);
            this.textBoxDesc2.TabIndex = 6;
            // 
            // FormManualGating
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 187);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownDesc2Max);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownDesc2Min);
            this.Controls.Add(this.textBoxDesc2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownDesc1Max);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownDesc1Min);
            this.Controls.Add(this.textBoxDesc1);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormManualGating";
            this.Text = "Manual Gating";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDesc1Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDesc1Max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDesc2Max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDesc2Min)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.TextBox textBoxDesc1;
        public System.Windows.Forms.NumericUpDown numericUpDownDesc1Min;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDownDesc1Max;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown numericUpDownDesc2Max;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown numericUpDownDesc2Min;
        public System.Windows.Forms.TextBox textBoxDesc2;
    }
}