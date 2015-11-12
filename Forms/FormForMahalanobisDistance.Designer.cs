namespace HCSAnalyzer.Forms
{
    partial class FormForMahalanobisDistance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForMahalanobisDistance));
            this.buttonProcess = new System.Windows.Forms.Button();
            this.panelForSourceCloud = new System.Windows.Forms.Panel();
            this.checkBoxDistAsDesc = new System.Windows.Forms.CheckBox();
            this.panelHitClass = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownPValue = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPValue)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonProcess
            // 
            this.buttonProcess.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonProcess.Location = new System.Drawing.Point(289, 276);
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Size = new System.Drawing.Size(98, 29);
            this.buttonProcess.TabIndex = 0;
            this.buttonProcess.Text = "Process";
            this.buttonProcess.UseVisualStyleBackColor = true;
            // 
            // panelForSourceCloud
            // 
            this.panelForSourceCloud.AutoScroll = true;
            this.panelForSourceCloud.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelForSourceCloud.Location = new System.Drawing.Point(6, 6);
            this.panelForSourceCloud.Name = "panelForSourceCloud";
            this.panelForSourceCloud.Size = new System.Drawing.Size(199, 262);
            this.panelForSourceCloud.TabIndex = 1;
            // 
            // checkBoxDistAsDesc
            // 
            this.checkBoxDistAsDesc.AutoSize = true;
            this.checkBoxDistAsDesc.Checked = true;
            this.checkBoxDistAsDesc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDistAsDesc.Location = new System.Drawing.Point(237, 200);
            this.checkBoxDistAsDesc.Name = "checkBoxDistAsDesc";
            this.checkBoxDistAsDesc.Size = new System.Drawing.Size(151, 17);
            this.checkBoxDistAsDesc.TabIndex = 3;
            this.checkBoxDistAsDesc.Text = "Add distance as descriptor";
            this.checkBoxDistAsDesc.UseVisualStyleBackColor = true;
            // 
            // panelHitClass
            // 
            this.panelHitClass.AutoScroll = true;
            this.panelHitClass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHitClass.Location = new System.Drawing.Point(6, 6);
            this.panelHitClass.Name = "panelHitClass";
            this.panelHitClass.Size = new System.Drawing.Size(199, 262);
            this.panelHitClass.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(7, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(219, 297);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelForSourceCloud);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(211, 271);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Distance to";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panelHitClass);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(211, 271);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Outliers Class";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(253, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "p-Value";
            // 
            // numericUpDownPValue
            // 
            this.numericUpDownPValue.DecimalPlaces = 4;
            this.numericUpDownPValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownPValue.Location = new System.Drawing.Point(303, 116);
            this.numericUpDownPValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericUpDownPValue.Name = "numericUpDownPValue";
            this.numericUpDownPValue.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownPValue.TabIndex = 7;
            this.numericUpDownPValue.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(231, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Hits will be classified at Class 1";
            // 
            // FormForMahalanobisDistance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 324);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownPValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.checkBoxDistAsDesc);
            this.Controls.Add(this.buttonProcess);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForMahalanobisDistance";
            this.Text = "Hit Identification";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonProcess;
        public System.Windows.Forms.Panel panelForSourceCloud;
        public System.Windows.Forms.CheckBox checkBoxDistAsDesc;
        public System.Windows.Forms.Panel panelHitClass;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDownPValue;
        private System.Windows.Forms.Label label2;
    }
}