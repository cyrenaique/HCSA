namespace HCSAnalyzer.Forms
{
    partial class FormForPanelFilterHits
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
            this.panelFilterDesc = new System.Windows.Forms.Panel();
            this.comboBoxComparison = new System.Windows.Forms.ComboBox();
            this.numericUpDownZScoreValue = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownManualValue = new System.Windows.Forms.NumericUpDown();
            this.radioButtonZScore = new System.Windows.Forms.RadioButton();
            this.radioButtonManual = new System.Windows.Forms.RadioButton();
            this.comboBoxForDescName = new System.Windows.Forms.ComboBox();
            this.panelFilterProperties = new System.Windows.Forms.Panel();
            this.comboBoxPropertyComparison = new System.Windows.Forms.ComboBox();
            this.numericUpDownValuePropToBeCompared = new System.Windows.Forms.NumericUpDown();
            this.comboBoxForPropertyName = new System.Windows.Forms.ComboBox();
            this.TextBoxValuePropToBeCompared = new System.Windows.Forms.TextBox();
            this.panelFilterDesc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZScoreValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownManualValue)).BeginInit();
            this.panelFilterProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValuePropToBeCompared)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFilterDesc
            // 
            this.panelFilterDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFilterDesc.Controls.Add(this.comboBoxComparison);
            this.panelFilterDesc.Controls.Add(this.numericUpDownZScoreValue);
            this.panelFilterDesc.Controls.Add(this.numericUpDownManualValue);
            this.panelFilterDesc.Controls.Add(this.radioButtonZScore);
            this.panelFilterDesc.Controls.Add(this.radioButtonManual);
            this.panelFilterDesc.Controls.Add(this.comboBoxForDescName);
            this.panelFilterDesc.Location = new System.Drawing.Point(2, 3);
            this.panelFilterDesc.Name = "panelFilterDesc";
            this.panelFilterDesc.Size = new System.Drawing.Size(432, 64);
            this.panelFilterDesc.TabIndex = 0;
            // 
            // comboBoxComparison
            // 
            this.comboBoxComparison.FormattingEnabled = true;
            this.comboBoxComparison.Items.AddRange(new object[] {
            ">",
            "<",
            ">=",
            "<=",
            "=",
            "!="});
            this.comboBoxComparison.Location = new System.Drawing.Point(248, 18);
            this.comboBoxComparison.Name = "comboBoxComparison";
            this.comboBoxComparison.Size = new System.Drawing.Size(55, 21);
            this.comboBoxComparison.TabIndex = 5;
            this.comboBoxComparison.Text = ">";
            // 
            // numericUpDownZScoreValue
            // 
            this.numericUpDownZScoreValue.DecimalPlaces = 4;
            this.numericUpDownZScoreValue.Enabled = false;
            this.numericUpDownZScoreValue.Location = new System.Drawing.Point(313, 33);
            this.numericUpDownZScoreValue.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.numericUpDownZScoreValue.Minimum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            -2147483648});
            this.numericUpDownZScoreValue.Name = "numericUpDownZScoreValue";
            this.numericUpDownZScoreValue.Size = new System.Drawing.Size(109, 20);
            this.numericUpDownZScoreValue.TabIndex = 4;
            // 
            // numericUpDownManualValue
            // 
            this.numericUpDownManualValue.DecimalPlaces = 4;
            this.numericUpDownManualValue.Location = new System.Drawing.Point(313, 6);
            this.numericUpDownManualValue.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.numericUpDownManualValue.Minimum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            -2147483648});
            this.numericUpDownManualValue.Name = "numericUpDownManualValue";
            this.numericUpDownManualValue.Size = new System.Drawing.Size(109, 20);
            this.numericUpDownManualValue.TabIndex = 3;
            // 
            // radioButtonZScore
            // 
            this.radioButtonZScore.AutoSize = true;
            this.radioButtonZScore.Enabled = false;
            this.radioButtonZScore.Location = new System.Drawing.Point(183, 33);
            this.radioButtonZScore.Name = "radioButtonZScore";
            this.radioButtonZScore.Size = new System.Drawing.Size(61, 17);
            this.radioButtonZScore.TabIndex = 2;
            this.radioButtonZScore.TabStop = true;
            this.radioButtonZScore.Text = "Z-score";
            this.radioButtonZScore.UseVisualStyleBackColor = true;
            this.radioButtonZScore.CheckedChanged += new System.EventHandler(this.radioButtonZScore_CheckedChanged);
            // 
            // radioButtonManual
            // 
            this.radioButtonManual.AutoSize = true;
            this.radioButtonManual.Checked = true;
            this.radioButtonManual.Location = new System.Drawing.Point(183, 6);
            this.radioButtonManual.Name = "radioButtonManual";
            this.radioButtonManual.Size = new System.Drawing.Size(47, 17);
            this.radioButtonManual.TabIndex = 1;
            this.radioButtonManual.TabStop = true;
            this.radioButtonManual.Text = "Raw";
            this.radioButtonManual.UseVisualStyleBackColor = true;
            this.radioButtonManual.CheckedChanged += new System.EventHandler(this.radioButtonManual_CheckedChanged);
            // 
            // comboBoxForDescName
            // 
            this.comboBoxForDescName.FormattingEnabled = true;
            this.comboBoxForDescName.Location = new System.Drawing.Point(7, 18);
            this.comboBoxForDescName.Name = "comboBoxForDescName";
            this.comboBoxForDescName.Size = new System.Drawing.Size(164, 21);
            this.comboBoxForDescName.TabIndex = 0;
            // 
            // panelFilterProperties
            // 
            this.panelFilterProperties.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFilterProperties.Controls.Add(this.TextBoxValuePropToBeCompared);
            this.panelFilterProperties.Controls.Add(this.comboBoxPropertyComparison);
            this.panelFilterProperties.Controls.Add(this.numericUpDownValuePropToBeCompared);
            this.panelFilterProperties.Controls.Add(this.comboBoxForPropertyName);
            this.panelFilterProperties.Location = new System.Drawing.Point(2, 83);
            this.panelFilterProperties.Name = "panelFilterProperties";
            this.panelFilterProperties.Size = new System.Drawing.Size(432, 64);
            this.panelFilterProperties.TabIndex = 1;
            // 
            // comboBoxPropertyComparison
            // 
            this.comboBoxPropertyComparison.FormattingEnabled = true;
            this.comboBoxPropertyComparison.Items.AddRange(new object[] {
            ">",
            "<",
            ">=",
            "<=",
            "=",
            "!="});
            this.comboBoxPropertyComparison.Location = new System.Drawing.Point(183, 17);
            this.comboBoxPropertyComparison.Name = "comboBoxPropertyComparison";
            this.comboBoxPropertyComparison.Size = new System.Drawing.Size(55, 21);
            this.comboBoxPropertyComparison.TabIndex = 5;
            this.comboBoxPropertyComparison.Text = ">";
            // 
            // numericUpDownValuePropToBeCompared
            // 
            this.numericUpDownValuePropToBeCompared.DecimalPlaces = 4;
            this.numericUpDownValuePropToBeCompared.Location = new System.Drawing.Point(248, 17);
            this.numericUpDownValuePropToBeCompared.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.numericUpDownValuePropToBeCompared.Minimum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            -2147483648});
            this.numericUpDownValuePropToBeCompared.Name = "numericUpDownValuePropToBeCompared";
            this.numericUpDownValuePropToBeCompared.Size = new System.Drawing.Size(174, 20);
            this.numericUpDownValuePropToBeCompared.TabIndex = 3;
            // 
            // comboBoxForPropertyName
            // 
            this.comboBoxForPropertyName.FormattingEnabled = true;
            this.comboBoxForPropertyName.Location = new System.Drawing.Point(7, 17);
            this.comboBoxForPropertyName.Name = "comboBoxForPropertyName";
            this.comboBoxForPropertyName.Size = new System.Drawing.Size(164, 21);
            this.comboBoxForPropertyName.TabIndex = 0;
            this.comboBoxForPropertyName.SelectedIndexChanged += new System.EventHandler(this.comboBoxForPropertyName_SelectedIndexChanged);
            // 
            // TextBoxValuePropToBeCompared
            // 
            this.TextBoxValuePropToBeCompared.Location = new System.Drawing.Point(248, 17);
            this.TextBoxValuePropToBeCompared.Name = "TextBoxValuePropToBeCompared";
            this.TextBoxValuePropToBeCompared.Size = new System.Drawing.Size(174, 20);
            this.TextBoxValuePropToBeCompared.TabIndex = 2;
            this.TextBoxValuePropToBeCompared.Visible = false;
            // 
            // FormForPanelFilterHits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 314);
            this.Controls.Add(this.panelFilterProperties);
            this.Controls.Add(this.panelFilterDesc);
            this.Name = "FormForPanelFilterHits";
            this.Text = "FormForPanelFilterHits";
            this.panelFilterDesc.ResumeLayout(false);
            this.panelFilterDesc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZScoreValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownManualValue)).EndInit();
            this.panelFilterProperties.ResumeLayout(false);
            this.panelFilterProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValuePropToBeCompared)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelFilterDesc;
        public System.Windows.Forms.NumericUpDown numericUpDownZScoreValue;
        public System.Windows.Forms.NumericUpDown numericUpDownManualValue;
        public System.Windows.Forms.RadioButton radioButtonZScore;
        public System.Windows.Forms.RadioButton radioButtonManual;
        public System.Windows.Forms.ComboBox comboBoxForDescName;
        public System.Windows.Forms.ComboBox comboBoxComparison;
        public System.Windows.Forms.Panel panelFilterProperties;
        public System.Windows.Forms.ComboBox comboBoxPropertyComparison;
        public System.Windows.Forms.NumericUpDown numericUpDownValuePropToBeCompared;
        public System.Windows.Forms.ComboBox comboBoxForPropertyName;
        public System.Windows.Forms.TextBox TextBoxValuePropToBeCompared;
    }
}