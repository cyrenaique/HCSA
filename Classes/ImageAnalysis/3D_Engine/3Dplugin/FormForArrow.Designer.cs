namespace IM3_Plugin3
{
    partial class FormForArrow
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
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownArrowScale = new System.Windows.Forms.NumericUpDown();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonChangeColorPositive = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArrowScale)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(52, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scale";
            // 
            // numericUpDownArrowScale
            // 
            this.numericUpDownArrowScale.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownArrowScale.DecimalPlaces = 2;
            this.numericUpDownArrowScale.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownArrowScale.Location = new System.Drawing.Point(141, 38);
            this.numericUpDownArrowScale.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownArrowScale.Name = "numericUpDownArrowScale";
            this.numericUpDownArrowScale.Size = new System.Drawing.Size(106, 20);
            this.numericUpDownArrowScale.TabIndex = 1;
            this.numericUpDownArrowScale.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(90, 140);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(115, 25);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonChangeColorPositive
            // 
            this.buttonChangeColorPositive.BackColor = System.Drawing.Color.Aquamarine;
            this.buttonChangeColorPositive.FlatAppearance.BorderSize = 0;
            this.buttonChangeColorPositive.Location = new System.Drawing.Point(176, 80);
            this.buttonChangeColorPositive.Name = "buttonChangeColorPositive";
            this.buttonChangeColorPositive.Size = new System.Drawing.Size(29, 27);
            this.buttonChangeColorPositive.TabIndex = 22;
            this.buttonChangeColorPositive.UseVisualStyleBackColor = false;
            this.buttonChangeColorPositive.Click += new System.EventHandler(this.buttonChangeColorPositive_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(52, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "Color";
            // 
            // FormForArrow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(295, 181);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonChangeColorPositive);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.numericUpDownArrowScale);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormForArrow";
            this.Text = "FormForArrow";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArrowScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDownArrowScale;
        private System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonChangeColorPositive;
        private System.Windows.Forms.Label label2;
    }
}