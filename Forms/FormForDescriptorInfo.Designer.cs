namespace LibPlateAnalysis
{
    public partial class FormForDescriptorInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForDescriptorInfo));
            this.buttonOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNameDescriptor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelDataType = new System.Windows.Forms.Label();
            this.labelDataBaseConnection = new System.Windows.Forms.Label();
            this.panelForColor = new System.Windows.Forms.Panel();
            this.numericUpDownBinValue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBoxDescription = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBinValue)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(145, 412);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(142, 30);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // textBoxNameDescriptor
            // 
            this.textBoxNameDescriptor.Location = new System.Drawing.Point(67, 16);
            this.textBoxNameDescriptor.Name = "textBoxNameDescriptor";
            this.textBoxNameDescriptor.Size = new System.Drawing.Size(220, 20);
            this.textBoxNameDescriptor.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 327);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Data Type";
            // 
            // labelDataType
            // 
            this.labelDataType.AutoSize = true;
            this.labelDataType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDataType.Location = new System.Drawing.Point(109, 329);
            this.labelDataType.Name = "labelDataType";
            this.labelDataType.Size = new System.Drawing.Size(31, 13);
            this.labelDataType.TabIndex = 3;
            this.labelDataType.Text = "###";
            this.labelDataType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDataBaseConnection
            // 
            this.labelDataBaseConnection.AutoSize = true;
            this.labelDataBaseConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDataBaseConnection.Location = new System.Drawing.Point(64, 367);
            this.labelDataBaseConnection.Name = "labelDataBaseConnection";
            this.labelDataBaseConnection.Size = new System.Drawing.Size(134, 13);
            this.labelDataBaseConnection.TabIndex = 5;
            this.labelDataBaseConnection.Text = "DataBase Connection.";
            this.labelDataBaseConnection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelForColor
            // 
            this.panelForColor.Location = new System.Drawing.Point(218, 365);
            this.panelForColor.Name = "panelForColor";
            this.panelForColor.Size = new System.Drawing.Size(22, 18);
            this.panelForColor.TabIndex = 6;
            // 
            // numericUpDownBinValue
            // 
            this.numericUpDownBinValue.Location = new System.Drawing.Point(183, 325);
            this.numericUpDownBinValue.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownBinValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBinValue.Name = "numericUpDownBinValue";
            this.numericUpDownBinValue.Size = new System.Drawing.Size(104, 20);
            this.numericUpDownBinValue.TabIndex = 7;
            this.numericUpDownBinValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Description";
            // 
            // richTextBoxDescription
            // 
            this.richTextBoxDescription.Location = new System.Drawing.Point(12, 71);
            this.richTextBoxDescription.Name = "richTextBoxDescription";
            this.richTextBoxDescription.Size = new System.Drawing.Size(275, 235);
            this.richTextBoxDescription.TabIndex = 9;
            this.richTextBoxDescription.Text = "";
            // 
            // FormForDescriptorInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 454);
            this.Controls.Add(this.richTextBoxDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownBinValue);
            this.Controls.Add(this.panelForColor);
            this.Controls.Add(this.labelDataBaseConnection);
            this.Controls.Add(this.labelDataType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxNameDescriptor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormForDescriptorInfo";
            this.Text = "Info Descriptor";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBinValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNameDescriptor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelDataType;
        private System.Windows.Forms.Label labelDataBaseConnection;
        public System.Windows.Forms.Panel panelForColor;
        public System.Windows.Forms.NumericUpDown numericUpDownBinValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBoxDescription;
    }
}