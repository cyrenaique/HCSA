namespace HCSAnalyzer.Classes.Base_Classes.GUI
{
    partial class FormForXYMinMax
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForXYMinMax));
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBoxX = new System.Windows.Forms.GroupBox();
            this.checkBoxXAutomated = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownXMax = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownXMin = new System.Windows.Forms.NumericUpDown();
            this.groupBoxY = new System.Windows.Forms.GroupBox();
            this.checkBoxYAutomated = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownYMax = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownYMin = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxXintervalAutomated = new System.Windows.Forms.CheckBox();
            this.numericUpDownXInterval = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxYintervalAutomated = new System.Windows.Forms.CheckBox();
            this.numericUpDownYInterval = new System.Windows.Forms.NumericUpDown();
            this.groupBoxX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXMin)).BeginInit();
            this.groupBoxY.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYMin)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXInterval)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(196, 223);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(81, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // groupBoxX
            // 
            this.groupBoxX.Controls.Add(this.checkBoxXAutomated);
            this.groupBoxX.Controls.Add(this.label2);
            this.groupBoxX.Controls.Add(this.numericUpDownXMax);
            this.groupBoxX.Controls.Add(this.label1);
            this.groupBoxX.Controls.Add(this.numericUpDownXMin);
            this.groupBoxX.Location = new System.Drawing.Point(9, 8);
            this.groupBoxX.Name = "groupBoxX";
            this.groupBoxX.Size = new System.Drawing.Size(131, 115);
            this.groupBoxX.TabIndex = 2;
            this.groupBoxX.TabStop = false;
            this.groupBoxX.Text = "X";
            // 
            // checkBoxXAutomated
            // 
            this.checkBoxXAutomated.AutoSize = true;
            this.checkBoxXAutomated.Checked = true;
            this.checkBoxXAutomated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxXAutomated.Location = new System.Drawing.Point(27, 88);
            this.checkBoxXAutomated.Name = "checkBoxXAutomated";
            this.checkBoxXAutomated.Size = new System.Drawing.Size(77, 17);
            this.checkBoxXAutomated.TabIndex = 7;
            this.checkBoxXAutomated.Text = "Automated";
            this.checkBoxXAutomated.UseVisualStyleBackColor = true;
            this.checkBoxXAutomated.CheckedChanged += new System.EventHandler(this.checkBoxXAutomated_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Max.";
            // 
            // numericUpDownXMax
            // 
            this.numericUpDownXMax.DecimalPlaces = 4;
            this.numericUpDownXMax.Enabled = false;
            this.numericUpDownXMax.Location = new System.Drawing.Point(40, 53);
            this.numericUpDownXMax.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            0});
            this.numericUpDownXMax.Minimum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            -2147483648});
            this.numericUpDownXMax.Name = "numericUpDownXMax";
            this.numericUpDownXMax.Size = new System.Drawing.Size(81, 20);
            this.numericUpDownXMax.TabIndex = 5;
            this.numericUpDownXMax.ValueChanged += new System.EventHandler(this.numericUpDownXMax_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Min.";
            // 
            // numericUpDownXMin
            // 
            this.numericUpDownXMin.DecimalPlaces = 4;
            this.numericUpDownXMin.Enabled = false;
            this.numericUpDownXMin.Location = new System.Drawing.Point(40, 21);
            this.numericUpDownXMin.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            0});
            this.numericUpDownXMin.Minimum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            -2147483648});
            this.numericUpDownXMin.Name = "numericUpDownXMin";
            this.numericUpDownXMin.Size = new System.Drawing.Size(81, 20);
            this.numericUpDownXMin.TabIndex = 3;
            this.numericUpDownXMin.ValueChanged += new System.EventHandler(this.numericUpDownXMin_ValueChanged);
            // 
            // groupBoxY
            // 
            this.groupBoxY.Controls.Add(this.checkBoxYAutomated);
            this.groupBoxY.Controls.Add(this.label3);
            this.groupBoxY.Controls.Add(this.numericUpDownYMax);
            this.groupBoxY.Controls.Add(this.label4);
            this.groupBoxY.Controls.Add(this.numericUpDownYMin);
            this.groupBoxY.Location = new System.Drawing.Point(146, 8);
            this.groupBoxY.Name = "groupBoxY";
            this.groupBoxY.Size = new System.Drawing.Size(131, 115);
            this.groupBoxY.TabIndex = 7;
            this.groupBoxY.TabStop = false;
            this.groupBoxY.Text = "Y";
            // 
            // checkBoxYAutomated
            // 
            this.checkBoxYAutomated.AutoSize = true;
            this.checkBoxYAutomated.Checked = true;
            this.checkBoxYAutomated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxYAutomated.Location = new System.Drawing.Point(27, 88);
            this.checkBoxYAutomated.Name = "checkBoxYAutomated";
            this.checkBoxYAutomated.Size = new System.Drawing.Size(77, 17);
            this.checkBoxYAutomated.TabIndex = 8;
            this.checkBoxYAutomated.Text = "Automated";
            this.checkBoxYAutomated.UseVisualStyleBackColor = true;
            this.checkBoxYAutomated.CheckedChanged += new System.EventHandler(this.checkBoxYAutomated_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Max.";
            // 
            // numericUpDownYMax
            // 
            this.numericUpDownYMax.DecimalPlaces = 4;
            this.numericUpDownYMax.Enabled = false;
            this.numericUpDownYMax.Location = new System.Drawing.Point(40, 53);
            this.numericUpDownYMax.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            0});
            this.numericUpDownYMax.Minimum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            -2147483648});
            this.numericUpDownYMax.Name = "numericUpDownYMax";
            this.numericUpDownYMax.Size = new System.Drawing.Size(81, 20);
            this.numericUpDownYMax.TabIndex = 5;
            this.numericUpDownYMax.ValueChanged += new System.EventHandler(this.numericUpDownYMax_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Min.";
            // 
            // numericUpDownYMin
            // 
            this.numericUpDownYMin.DecimalPlaces = 4;
            this.numericUpDownYMin.Enabled = false;
            this.numericUpDownYMin.Location = new System.Drawing.Point(40, 21);
            this.numericUpDownYMin.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            0});
            this.numericUpDownYMin.Minimum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            -2147483648});
            this.numericUpDownYMin.Name = "numericUpDownYMin";
            this.numericUpDownYMin.Size = new System.Drawing.Size(81, 20);
            this.numericUpDownYMin.TabIndex = 3;
            this.numericUpDownYMin.ValueChanged += new System.EventHandler(this.numericUpDownYMin_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxXintervalAutomated);
            this.groupBox1.Controls.Add(this.numericUpDownXInterval);
            this.groupBox1.Location = new System.Drawing.Point(9, 129);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 84);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Interval X";
            // 
            // checkBoxXintervalAutomated
            // 
            this.checkBoxXintervalAutomated.AutoSize = true;
            this.checkBoxXintervalAutomated.Checked = true;
            this.checkBoxXintervalAutomated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxXintervalAutomated.Location = new System.Drawing.Point(27, 54);
            this.checkBoxXintervalAutomated.Name = "checkBoxXintervalAutomated";
            this.checkBoxXintervalAutomated.Size = new System.Drawing.Size(77, 17);
            this.checkBoxXintervalAutomated.TabIndex = 7;
            this.checkBoxXintervalAutomated.Text = "Automated";
            this.checkBoxXintervalAutomated.UseVisualStyleBackColor = true;
            this.checkBoxXintervalAutomated.CheckedChanged += new System.EventHandler(this.checkBoxXintervalAutomated_CheckedChanged);
            // 
            // numericUpDownXInterval
            // 
            this.numericUpDownXInterval.DecimalPlaces = 4;
            this.numericUpDownXInterval.Enabled = false;
            this.numericUpDownXInterval.Location = new System.Drawing.Point(10, 24);
            this.numericUpDownXInterval.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            0});
            this.numericUpDownXInterval.Minimum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            -2147483648});
            this.numericUpDownXInterval.Name = "numericUpDownXInterval";
            this.numericUpDownXInterval.Size = new System.Drawing.Size(111, 20);
            this.numericUpDownXInterval.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxYintervalAutomated);
            this.groupBox2.Controls.Add(this.numericUpDownYInterval);
            this.groupBox2.Location = new System.Drawing.Point(146, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(131, 84);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Interval Y";
            // 
            // checkBoxYintervalAutomated
            // 
            this.checkBoxYintervalAutomated.AutoSize = true;
            this.checkBoxYintervalAutomated.Checked = true;
            this.checkBoxYintervalAutomated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxYintervalAutomated.Location = new System.Drawing.Point(27, 54);
            this.checkBoxYintervalAutomated.Name = "checkBoxYintervalAutomated";
            this.checkBoxYintervalAutomated.Size = new System.Drawing.Size(77, 17);
            this.checkBoxYintervalAutomated.TabIndex = 7;
            this.checkBoxYintervalAutomated.Text = "Automated";
            this.checkBoxYintervalAutomated.UseVisualStyleBackColor = true;
            this.checkBoxYintervalAutomated.CheckedChanged += new System.EventHandler(this.checkBoxYintervalAutomated_CheckedChanged);
            // 
            // numericUpDownYInterval
            // 
            this.numericUpDownYInterval.DecimalPlaces = 4;
            this.numericUpDownYInterval.Enabled = false;
            this.numericUpDownYInterval.Location = new System.Drawing.Point(10, 24);
            this.numericUpDownYInterval.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            0});
            this.numericUpDownYInterval.Minimum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            -2147483648});
            this.numericUpDownYInterval.Name = "numericUpDownYInterval";
            this.numericUpDownYInterval.Size = new System.Drawing.Size(111, 20);
            this.numericUpDownYInterval.TabIndex = 3;
            // 
            // FormForXYMinMax
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 257);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxY);
            this.Controls.Add(this.groupBoxX);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForXYMinMax";
            this.Text = "(X,Y) min-max";
            this.groupBoxX.ResumeLayout(false);
            this.groupBoxX.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXMin)).EndInit();
            this.groupBoxY.ResumeLayout(false);
            this.groupBoxY.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYMin)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXInterval)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYInterval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.GroupBox groupBoxX;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDownXMax;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDownXMin;
        private System.Windows.Forms.GroupBox groupBoxY;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown numericUpDownYMax;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown numericUpDownYMin;
        public System.Windows.Forms.CheckBox checkBoxXAutomated;
        public System.Windows.Forms.CheckBox checkBoxYAutomated;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.CheckBox checkBoxXintervalAutomated;
        public System.Windows.Forms.NumericUpDown numericUpDownXInterval;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.CheckBox checkBoxYintervalAutomated;
        public System.Windows.Forms.NumericUpDown numericUpDownYInterval;
    }
}