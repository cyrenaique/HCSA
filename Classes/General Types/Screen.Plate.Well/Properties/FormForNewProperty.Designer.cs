namespace HCSAnalyzer.Classes.General_Types.Screen.Plate.Well.Properties
{
    partial class FormForNewProperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForNewProperty));
            this.buttonOk = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelForOptionsNumber = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownNumberMin = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownNumberMax = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.panelForOptionsNumber.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberMax)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(197, 213);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(71, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(120, 20);
            this.textBoxName.TabIndex = 1;
            this.textBoxName.Text = "New Property";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // buttonCheck
            // 
            this.buttonCheck.Location = new System.Drawing.Point(197, 10);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(49, 23);
            this.buttonCheck.TabIndex = 3;
            this.buttonCheck.Text = "Check";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Type";
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(71, 48);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(120, 21);
            this.comboBoxType.TabIndex = 5;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelForOptionsNumber);
            this.groupBox1.Location = new System.Drawing.Point(12, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 132);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Specific Properties";
            // 
            // panelForOptionsNumber
            // 
            this.panelForOptionsNumber.Controls.Add(this.numericUpDownNumberMax);
            this.panelForOptionsNumber.Controls.Add(this.numericUpDownNumberMin);
            this.panelForOptionsNumber.Controls.Add(this.label4);
            this.panelForOptionsNumber.Controls.Add(this.label3);
            this.panelForOptionsNumber.Location = new System.Drawing.Point(6, 42);
            this.panelForOptionsNumber.Name = "panelForOptionsNumber";
            this.panelForOptionsNumber.Size = new System.Drawing.Size(248, 84);
            this.panelForOptionsNumber.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Min.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Max.";
            // 
            // numericUpDownNumberMin
            // 
            this.numericUpDownNumberMin.DecimalPlaces = 4;
            this.numericUpDownNumberMin.Location = new System.Drawing.Point(53, 32);
            this.numericUpDownNumberMin.Name = "numericUpDownNumberMin";
            this.numericUpDownNumberMin.Size = new System.Drawing.Size(175, 20);
            this.numericUpDownNumberMin.TabIndex = 1;
            // 
            // numericUpDownNumberMax
            // 
            this.numericUpDownNumberMax.DecimalPlaces = 4;
            this.numericUpDownNumberMax.Location = new System.Drawing.Point(53, 56);
            this.numericUpDownNumberMax.Name = "numericUpDownNumberMax";
            this.numericUpDownNumberMax.Size = new System.Drawing.Size(175, 20);
            this.numericUpDownNumberMax.TabIndex = 2;
            // 
            // FormForNewProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 241);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForNewProperty";
            this.Text = "New Property";
            this.groupBox1.ResumeLayout(false);
            this.panelForOptionsNumber.ResumeLayout(false);
            this.panelForOptionsNumber.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCheck;
        public System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panelForOptionsNumber;
        public System.Windows.Forms.NumericUpDown numericUpDownNumberMax;
        public System.Windows.Forms.NumericUpDown numericUpDownNumberMin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox comboBoxType;
    }
}