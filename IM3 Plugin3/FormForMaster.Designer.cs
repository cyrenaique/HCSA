namespace IM3_Plugin3
{
    partial class FormForMaster
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
            this.checkBoxDrawAssociatedDelaunay = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBoxDrawAssociatedDelaunay
            // 
            this.checkBoxDrawAssociatedDelaunay.AutoSize = true;
            this.checkBoxDrawAssociatedDelaunay.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxDrawAssociatedDelaunay.Location = new System.Drawing.Point(68, 51);
            this.checkBoxDrawAssociatedDelaunay.Name = "checkBoxDrawAssociatedDelaunay";
            this.checkBoxDrawAssociatedDelaunay.Size = new System.Drawing.Size(108, 16);
            this.checkBoxDrawAssociatedDelaunay.TabIndex = 4;
            this.checkBoxDrawAssociatedDelaunay.Text = "Draw Delaunay";
            this.checkBoxDrawAssociatedDelaunay.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonOK.Location = new System.Drawing.Point(51, 86);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(146, 30);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.AutoCompleteCustomSource.AddRange(new string[] {
            "Nucleus",
            "Centriole",
            "Foci",
            "Nucleolus"});
            this.textBoxName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxName.BackColor = System.Drawing.SystemColors.ControlDark;
            this.textBoxName.ForeColor = System.Drawing.SystemColors.Control;
            this.textBoxName.Location = new System.Drawing.Point(68, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(165, 20);
            this.textBoxName.TabIndex = 7;
            this.textBoxName.Text = "Cell";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name";
            // 
            // FormForMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(245, 139);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.checkBoxDrawAssociatedDelaunay);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormForMaster";
            this.ShowIcon = false;
            this.Text = "Master";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox checkBoxDrawAssociatedDelaunay;
        private System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
    }
}