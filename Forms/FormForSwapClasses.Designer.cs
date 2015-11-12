namespace HCSAnalyzer.Forms
{
    partial class FormForSwapClasses
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForSwapClasses));
            this.buttonSwapClass = new System.Windows.Forms.Button();
            this.comboBoxDestinationClass = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelToBeSwapped = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSwapClass
            // 
            this.buttonSwapClass.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSwapClass.Location = new System.Drawing.Point(39, 276);
            this.buttonSwapClass.Name = "buttonSwapClass";
            this.buttonSwapClass.Size = new System.Drawing.Size(168, 25);
            this.buttonSwapClass.TabIndex = 3;
            this.buttonSwapClass.Text = "Swap Classes";
            this.buttonSwapClass.UseVisualStyleBackColor = true;
            // 
            // comboBoxDestinationClass
            // 
            this.comboBoxDestinationClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBoxDestinationClass.FormattingEnabled = true;
            this.comboBoxDestinationClass.Items.AddRange(new object[] {
            "Unselected (-1)",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.comboBoxDestinationClass.Location = new System.Drawing.Point(133, 124);
            this.comboBoxDestinationClass.Name = "comboBoxDestinationClass";
            this.comboBoxDestinationClass.Size = new System.Drawing.Size(103, 21);
            this.comboBoxDestinationClass.TabIndex = 5;
            this.comboBoxDestinationClass.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxDestinationClass_DrawItem);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Destination Class";
            // 
            // panelToBeSwapped
            // 
            this.panelToBeSwapped.Location = new System.Drawing.Point(8, 19);
            this.panelToBeSwapped.Name = "panelToBeSwapped";
            this.panelToBeSwapped.Size = new System.Drawing.Size(100, 239);
            this.panelToBeSwapped.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelToBeSwapped);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(117, 264);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "To be Swapped";
            // 
            // FormForSwapClasses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 305);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxDestinationClass);
            this.Controls.Add(this.buttonSwapClass);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormForSwapClasses";
            this.Text = "Swap Classes";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonSwapClass;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox comboBoxDestinationClass;
        public System.Windows.Forms.Panel panelToBeSwapped;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}