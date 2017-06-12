namespace HCSAnalyzer.Forms.ClusteringForms
{
    partial class FormForHierarchical
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForHierarchical));
            this.buttonDisplay = new System.Windows.Forms.Button();
            this.richTextBoxWarning = new System.Windows.Forms.RichTextBox();
            this.radioButtonCurrentPlate = new System.Windows.Forms.RadioButton();
            this.radioButtonFullScreen = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonOptions = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonDisplay
            // 
            this.buttonDisplay.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonDisplay.Location = new System.Drawing.Point(70, 180);
            this.buttonDisplay.Name = "buttonDisplay";
            this.buttonDisplay.Size = new System.Drawing.Size(159, 26);
            this.buttonDisplay.TabIndex = 0;
            this.buttonDisplay.Text = "Display Tree";
            this.buttonDisplay.UseVisualStyleBackColor = true;
            // 
            // richTextBoxWarning
            // 
            this.richTextBoxWarning.Location = new System.Drawing.Point(11, 12);
            this.richTextBoxWarning.Name = "richTextBoxWarning";
            this.richTextBoxWarning.ReadOnly = true;
            this.richTextBoxWarning.Size = new System.Drawing.Size(277, 96);
            this.richTextBoxWarning.TabIndex = 1;
            this.richTextBoxWarning.Text = "";
            // 
            // radioButtonCurrentPlate
            // 
            this.radioButtonCurrentPlate.AutoSize = true;
            this.radioButtonCurrentPlate.Checked = true;
            this.radioButtonCurrentPlate.Location = new System.Drawing.Point(11, 21);
            this.radioButtonCurrentPlate.Name = "radioButtonCurrentPlate";
            this.radioButtonCurrentPlate.Size = new System.Drawing.Size(86, 17);
            this.radioButtonCurrentPlate.TabIndex = 2;
            this.radioButtonCurrentPlate.TabStop = true;
            this.radioButtonCurrentPlate.Text = "Current Plate";
            this.radioButtonCurrentPlate.UseVisualStyleBackColor = true;
            // 
            // radioButtonFullScreen
            // 
            this.radioButtonFullScreen.AutoSize = true;
            this.radioButtonFullScreen.Location = new System.Drawing.Point(105, 21);
            this.radioButtonFullScreen.Name = "radioButtonFullScreen";
            this.radioButtonFullScreen.Size = new System.Drawing.Size(78, 17);
            this.radioButtonFullScreen.TabIndex = 2;
            this.radioButtonFullScreen.Text = "Full Screen";
            this.radioButtonFullScreen.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonFullScreen);
            this.groupBox1.Controls.Add(this.radioButtonCurrentPlate);
            this.groupBox1.Location = new System.Drawing.Point(12, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 51);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // buttonOptions
            // 
            this.buttonOptions.Location = new System.Drawing.Point(213, 114);
            this.buttonOptions.Name = "buttonOptions";
            this.buttonOptions.Size = new System.Drawing.Size(75, 45);
            this.buttonOptions.TabIndex = 4;
            this.buttonOptions.Text = "Options";
            this.buttonOptions.UseVisualStyleBackColor = true;
            this.buttonOptions.Click += new System.EventHandler(this.buttonOptions_Click);
            // 
            // FormForHierarchical
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 222);
            this.Controls.Add(this.buttonOptions);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.richTextBoxWarning);
            this.Controls.Add(this.buttonDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormForHierarchical";
            this.Text = "Hierarchical Tree";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDisplay;
        public System.Windows.Forms.RichTextBox richTextBoxWarning;
        public System.Windows.Forms.RadioButton radioButtonCurrentPlate;
        public System.Windows.Forms.RadioButton radioButtonFullScreen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonOptions;
    }
}