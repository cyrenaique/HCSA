namespace HCSAnalyzer.Forms.IO
{
    partial class FormForDescFiltering
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForDescFiltering));
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkBoxCaseSensitive = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTextFilter = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonIsUnSelect = new System.Windows.Forms.RadioButton();
            this.radioButtonIsSelect = new System.Windows.Forms.RadioButton();
            this.buttonApply = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxSingle = new System.Windows.Forms.CheckBox();
            this.checkBoxAverage = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(158, 203);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // checkBoxCaseSensitive
            // 
            this.checkBoxCaseSensitive.AutoSize = true;
            this.checkBoxCaseSensitive.Location = new System.Drawing.Point(15, 41);
            this.checkBoxCaseSensitive.Name = "checkBoxCaseSensitive";
            this.checkBoxCaseSensitive.Size = new System.Drawing.Size(96, 17);
            this.checkBoxCaseSensitive.TabIndex = 1;
            this.checkBoxCaseSensitive.Text = "Case Sensitive";
            this.checkBoxCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Text Filter";
            // 
            // textBoxTextFilter
            // 
            this.textBoxTextFilter.Location = new System.Drawing.Point(81, 13);
            this.textBoxTextFilter.Name = "textBoxTextFilter";
            this.textBoxTextFilter.Size = new System.Drawing.Size(151, 20);
            this.textBoxTextFilter.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonIsUnSelect);
            this.groupBox1.Controls.Add(this.radioButtonIsSelect);
            this.groupBox1.Location = new System.Drawing.Point(12, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 57);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Action";
            // 
            // radioButtonIsUnSelect
            // 
            this.radioButtonIsUnSelect.AutoSize = true;
            this.radioButtonIsUnSelect.Location = new System.Drawing.Point(129, 22);
            this.radioButtonIsUnSelect.Name = "radioButtonIsUnSelect";
            this.radioButtonIsUnSelect.Size = new System.Drawing.Size(67, 17);
            this.radioButtonIsUnSelect.TabIndex = 1;
            this.radioButtonIsUnSelect.Text = "Unselect";
            this.radioButtonIsUnSelect.UseVisualStyleBackColor = true;
            // 
            // radioButtonIsSelect
            // 
            this.radioButtonIsSelect.AutoSize = true;
            this.radioButtonIsSelect.Checked = true;
            this.radioButtonIsSelect.Location = new System.Drawing.Point(31, 22);
            this.radioButtonIsSelect.Name = "radioButtonIsSelect";
            this.radioButtonIsSelect.Size = new System.Drawing.Size(55, 17);
            this.radioButtonIsSelect.TabIndex = 0;
            this.radioButtonIsSelect.TabStop = true;
            this.radioButtonIsSelect.Text = "Select";
            this.radioButtonIsSelect.UseVisualStyleBackColor = true;
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonApply.Location = new System.Drawing.Point(12, 203);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 5;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxAverage);
            this.groupBox2.Controls.Add(this.checkBoxSingle);
            this.groupBox2.Location = new System.Drawing.Point(12, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 60);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Type";
            // 
            // checkBoxSingle
            // 
            this.checkBoxSingle.AutoSize = true;
            this.checkBoxSingle.Checked = true;
            this.checkBoxSingle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSingle.Location = new System.Drawing.Point(30, 26);
            this.checkBoxSingle.Name = "checkBoxSingle";
            this.checkBoxSingle.Size = new System.Drawing.Size(75, 17);
            this.checkBoxSingle.TabIndex = 0;
            this.checkBoxSingle.Text = "Single Cell";
            this.checkBoxSingle.UseVisualStyleBackColor = true;
            // 
            // checkBoxAverage
            // 
            this.checkBoxAverage.AutoSize = true;
            this.checkBoxAverage.Checked = true;
            this.checkBoxAverage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAverage.Location = new System.Drawing.Point(125, 26);
            this.checkBoxAverage.Name = "checkBoxAverage";
            this.checkBoxAverage.Size = new System.Drawing.Size(66, 17);
            this.checkBoxAverage.TabIndex = 1;
            this.checkBoxAverage.Text = "Average";
            this.checkBoxAverage.UseVisualStyleBackColor = true;
            // 
            // FormForDescFiltering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 232);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxTextFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxCaseSensitive);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForDescFiltering";
            this.Text = "Descriptors Filtering";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.CheckBox checkBoxCaseSensitive;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxTextFilter;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.RadioButton radioButtonIsUnSelect;
        public System.Windows.Forms.RadioButton radioButtonIsSelect;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.CheckBox checkBoxAverage;
        public System.Windows.Forms.CheckBox checkBoxSingle;
    }
}