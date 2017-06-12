namespace IM3_Plugin3
{
    partial class FormForInfoBeforeStarting
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownResolutionX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownResolutionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownResolutionZ = new System.Windows.Forms.NumericUpDown();
            this.buttonResetResolutions = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResolutionX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResolutionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResolutionZ)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(204, 205);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(115, 25);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(22, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 12);
            this.label5.TabIndex = 23;
            this.label5.Text = "Resolution X";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(22, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "Resolution Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(22, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "Resolution Z";
            // 
            // numericUpDownResolutionX
            // 
            this.numericUpDownResolutionX.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownResolutionX.DecimalPlaces = 3;
            this.numericUpDownResolutionX.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownResolutionX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownResolutionX.Location = new System.Drawing.Point(120, 16);
            this.numericUpDownResolutionX.Name = "numericUpDownResolutionX";
            this.numericUpDownResolutionX.Size = new System.Drawing.Size(84, 20);
            this.numericUpDownResolutionX.TabIndex = 25;
            // 
            // numericUpDownResolutionY
            // 
            this.numericUpDownResolutionY.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownResolutionY.DecimalPlaces = 3;
            this.numericUpDownResolutionY.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownResolutionY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownResolutionY.Location = new System.Drawing.Point(120, 50);
            this.numericUpDownResolutionY.Name = "numericUpDownResolutionY";
            this.numericUpDownResolutionY.Size = new System.Drawing.Size(84, 20);
            this.numericUpDownResolutionY.TabIndex = 26;
            // 
            // numericUpDownResolutionZ
            // 
            this.numericUpDownResolutionZ.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownResolutionZ.DecimalPlaces = 3;
            this.numericUpDownResolutionZ.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownResolutionZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownResolutionZ.Location = new System.Drawing.Point(120, 84);
            this.numericUpDownResolutionZ.Name = "numericUpDownResolutionZ";
            this.numericUpDownResolutionZ.Size = new System.Drawing.Size(84, 20);
            this.numericUpDownResolutionZ.TabIndex = 26;
            // 
            // buttonResetResolutions
            // 
            this.buttonResetResolutions.Location = new System.Drawing.Point(239, 44);
            this.buttonResetResolutions.Name = "buttonResetResolutions";
            this.buttonResetResolutions.Size = new System.Drawing.Size(53, 34);
            this.buttonResetResolutions.TabIndex = 27;
            this.buttonResetResolutions.Text = "Reset";
            this.buttonResetResolutions.UseVisualStyleBackColor = true;
            this.buttonResetResolutions.Click += new System.EventHandler(this.buttonResetResolutions_Click);
            // 
            // FormForInfoBeforeStarting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(331, 242);
            this.Controls.Add(this.buttonResetResolutions);
            this.Controls.Add(this.numericUpDownResolutionZ);
            this.Controls.Add(this.numericUpDownResolutionY);
            this.Controls.Add(this.numericUpDownResolutionX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonOk);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormForInfoBeforeStarting";
            this.Text = "Process Information";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResolutionX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResolutionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResolutionZ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDownResolutionX;
        public System.Windows.Forms.NumericUpDown numericUpDownResolutionY;
        public System.Windows.Forms.NumericUpDown numericUpDownResolutionZ;
        private System.Windows.Forms.Button buttonResetResolutions;
    }
}