namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{
    partial class PanelForParamPerceptron
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PanelForParamPerceptron));
            this.panel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownMomentum = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLearningRate = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSeed = new System.Windows.Forms.NumericUpDown();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.numericUpDownTrainingTime = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxNormAttribute = new System.Windows.Forms.CheckBox();
            this.checkBoxNormNumericClasses = new System.Windows.Forms.CheckBox();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMomentum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLearningRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTrainingTime)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.checkBoxNormNumericClasses);
            this.panel.Controls.Add(this.checkBoxNormAttribute);
            this.panel.Controls.Add(this.label2);
            this.panel.Controls.Add(this.label1);
            this.panel.Controls.Add(this.label4);
            this.panel.Controls.Add(this.label3);
            this.panel.Controls.Add(this.numericUpDownMomentum);
            this.panel.Controls.Add(this.numericUpDownLearningRate);
            this.panel.Controls.Add(this.numericUpDownTrainingTime);
            this.panel.Controls.Add(this.numericUpDownSeed);
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(200, 271);
            this.panel.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Momentum";
            this.toolTip.SetToolTip(this.label2, "Momentum applied to the weights during updating\r\n");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Learning Rate";
            this.toolTip.SetToolTip(this.label1, "Amount the weights that are updated");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Seed";
            // 
            // numericUpDownMomentum
            // 
            this.numericUpDownMomentum.DecimalPlaces = 1;
            this.numericUpDownMomentum.Location = new System.Drawing.Point(98, 49);
            this.numericUpDownMomentum.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownMomentum.Name = "numericUpDownMomentum";
            this.numericUpDownMomentum.Size = new System.Drawing.Size(90, 20);
            this.numericUpDownMomentum.TabIndex = 4;
            this.toolTip.SetToolTip(this.numericUpDownMomentum, "Momentum applied to the weights during updating\r\n");
            this.numericUpDownMomentum.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // numericUpDownLearningRate
            // 
            this.numericUpDownLearningRate.DecimalPlaces = 1;
            this.numericUpDownLearningRate.Location = new System.Drawing.Point(98, 23);
            this.numericUpDownLearningRate.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownLearningRate.Name = "numericUpDownLearningRate";
            this.numericUpDownLearningRate.Size = new System.Drawing.Size(90, 20);
            this.numericUpDownLearningRate.TabIndex = 4;
            this.toolTip.SetToolTip(this.numericUpDownLearningRate, "Amount the weights that are updated");
            this.numericUpDownLearningRate.Value = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            // 
            // numericUpDownSeed
            // 
            this.numericUpDownSeed.Location = new System.Drawing.Point(98, 176);
            this.numericUpDownSeed.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownSeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSeed.Name = "numericUpDownSeed";
            this.numericUpDownSeed.Size = new System.Drawing.Size(90, 20);
            this.numericUpDownSeed.TabIndex = 4;
            this.numericUpDownSeed.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // numericUpDownTrainingTime
            // 
            this.numericUpDownTrainingTime.Location = new System.Drawing.Point(98, 75);
            this.numericUpDownTrainingTime.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownTrainingTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTrainingTime.Name = "numericUpDownTrainingTime";
            this.numericUpDownTrainingTime.Size = new System.Drawing.Size(90, 20);
            this.numericUpDownTrainingTime.TabIndex = 4;
            this.toolTip.SetToolTip(this.numericUpDownTrainingTime, "Number of epochs to train through. \r\nIf the validation set is non-zero then it ca" +
                    "n terminate the network early");
            this.numericUpDownTrainingTime.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Training time";
            this.toolTip.SetToolTip(this.label4, "Number of epochs to train through. \r\nIf the validation set is non-zero then it ca" +
                    "n terminate the network early");
            // 
            // checkBoxNormAttribute
            // 
            this.checkBoxNormAttribute.AutoSize = true;
            this.checkBoxNormAttribute.Checked = true;
            this.checkBoxNormAttribute.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNormAttribute.Location = new System.Drawing.Point(15, 111);
            this.checkBoxNormAttribute.Name = "checkBoxNormAttribute";
            this.checkBoxNormAttribute.Size = new System.Drawing.Size(119, 17);
            this.checkBoxNormAttribute.TabIndex = 5;
            this.checkBoxNormAttribute.Text = "Normalize Attributes";
            this.toolTip.SetToolTip(this.checkBoxNormAttribute, resources.GetString("checkBoxNormAttribute.ToolTip"));
            this.checkBoxNormAttribute.UseVisualStyleBackColor = true;
            // 
            // checkBoxNormNumericClasses
            // 
            this.checkBoxNormNumericClasses.AutoSize = true;
            this.checkBoxNormNumericClasses.Checked = true;
            this.checkBoxNormNumericClasses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNormNumericClasses.Location = new System.Drawing.Point(15, 139);
            this.checkBoxNormNumericClasses.Name = "checkBoxNormNumericClasses";
            this.checkBoxNormNumericClasses.Size = new System.Drawing.Size(150, 17);
            this.checkBoxNormNumericClasses.TabIndex = 6;
            this.checkBoxNormNumericClasses.Text = "Normalize numeric classes";
            this.toolTip.SetToolTip(this.checkBoxNormNumericClasses, resources.GetString("checkBoxNormNumericClasses.ToolTip"));
            this.checkBoxNormNumericClasses.UseVisualStyleBackColor = true;
            // 
            // PanelForParamPerceptron
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamPerceptron";
            this.Size = new System.Drawing.Size(207, 278);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMomentum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLearningRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTrainingTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        public System.Windows.Forms.NumericUpDown numericUpDownSeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDownMomentum;
        public System.Windows.Forms.NumericUpDown numericUpDownLearningRate;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown numericUpDownTrainingTime;
        private System.Windows.Forms.CheckBox checkBoxNormAttribute;
        private System.Windows.Forms.CheckBox checkBoxNormNumericClasses;
    }
}
