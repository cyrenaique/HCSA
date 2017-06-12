namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{
    partial class PanelForParamKStar
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
            this.panel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownGlobalBlend = new System.Windows.Forms.NumericUpDown();
            this.checkBoxBlendAuto = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGlobalBlend)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.groupBox1);
            this.panel.Location = new System.Drawing.Point(3, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(200, 271);
            this.panel.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownGlobalBlend);
            this.groupBox1.Controls.Add(this.checkBoxBlendAuto);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(3, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 100);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Blend";
            // 
            // numericUpDownGlobalBlend
            // 
            this.numericUpDownGlobalBlend.Location = new System.Drawing.Point(74, 28);
            this.numericUpDownGlobalBlend.Name = "numericUpDownGlobalBlend";
            this.numericUpDownGlobalBlend.Size = new System.Drawing.Size(90, 20);
            this.numericUpDownGlobalBlend.TabIndex = 4;
            this.toolTip.SetToolTip(this.numericUpDownGlobalBlend, "Parameter for global blending");
            this.numericUpDownGlobalBlend.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // checkBoxBlendAuto
            // 
            this.checkBoxBlendAuto.AutoSize = true;
            this.checkBoxBlendAuto.Location = new System.Drawing.Point(38, 65);
            this.checkBoxBlendAuto.Name = "checkBoxBlendAuto";
            this.checkBoxBlendAuto.Size = new System.Drawing.Size(119, 17);
            this.checkBoxBlendAuto.TabIndex = 5;
            this.checkBoxBlendAuto.Text = "Automated Entropic";
            this.toolTip.SetToolTip(this.checkBoxBlendAuto, "Whether entropy-based blending is to be used");
            this.checkBoxBlendAuto.UseVisualStyleBackColor = true;
            this.checkBoxBlendAuto.CheckedChanged += new System.EventHandler(this.checkBoxBlendAuto_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Global";
            this.toolTip.SetToolTip(this.label3, "Parameter for global blending");
            // 
            // PanelForParamKStar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamKStar";
            this.Size = new System.Drawing.Size(209, 276);
            this.panel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGlobalBlend)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel;
        private System.Windows.Forms.CheckBox checkBoxBlendAuto;
        public System.Windows.Forms.NumericUpDown numericUpDownGlobalBlend;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
