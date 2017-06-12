namespace HCSAnalyzer.Classes.Base_Classes.GUI
{
    partial class FormForStatDesc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForStatDesc));
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelForSubPopulation = new System.Windows.Forms.Panel();
            this.textBoxDescName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxStatistics = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(310, 324);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(97, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelForSubPopulation);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 346);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sub-populations";
            // 
            // panelForSubPopulation
            // 
            this.panelForSubPopulation.AutoScroll = true;
            this.panelForSubPopulation.Location = new System.Drawing.Point(6, 15);
            this.panelForSubPopulation.Name = "panelForSubPopulation";
            this.panelForSubPopulation.Size = new System.Drawing.Size(178, 325);
            this.panelForSubPopulation.TabIndex = 0;
            // 
            // textBoxDescName
            // 
            this.textBoxDescName.Location = new System.Drawing.Point(205, 25);
            this.textBoxDescName.Name = "textBoxDescName";
            this.textBoxDescName.Size = new System.Drawing.Size(202, 20);
            this.textBoxDescName.TabIndex = 2;
            this.textBoxDescName.Text = "Average";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(202, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "New Descriptor Name";
            // 
            // comboBoxStatistics
            // 
            this.comboBoxStatistics.FormattingEnabled = true;
            this.comboBoxStatistics.Items.AddRange(new object[] {
            "Average",
            "Stdev",
            "Sum",
            "Median",
            "MAD",
            "CV%"});
            this.comboBoxStatistics.Location = new System.Drawing.Point(205, 83);
            this.comboBoxStatistics.Name = "comboBoxStatistics";
            this.comboBoxStatistics.Size = new System.Drawing.Size(202, 21);
            this.comboBoxStatistics.TabIndex = 4;
            this.comboBoxStatistics.Text = "Average";
            this.comboBoxStatistics.SelectedIndexChanged += new System.EventHandler(this.comboBoxStatistics_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Statistics";
            // 
            // FormForStatDesc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 352);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxStatistics);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDescName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForStatDesc";
            this.Text = "Sub-population Statistics";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Panel panelForSubPopulation;
        public System.Windows.Forms.TextBox textBoxDescName;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox comboBoxStatistics;
        private System.Windows.Forms.Label label2;
    }
}