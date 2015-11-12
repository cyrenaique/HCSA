namespace HCSAnalyzer.Forms.IO
{
    partial class FormForDoubleProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForDoubleProgress));
            this.label1 = new System.Windows.Forms.Label();
            this.labelWell0 = new System.Windows.Forms.Label();
            this.progressBarPlate = new System.Windows.Forms.ProgressBar();
            this.progressBarWell = new System.Windows.Forms.ProgressBar();
            this.labelWellIdx = new System.Windows.Forms.Label();
            this.labelPlateIdx = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Plate Idx";
            // 
            // labelWell0
            // 
            this.labelWell0.AutoSize = true;
            this.labelWell0.Location = new System.Drawing.Point(18, 68);
            this.labelWell0.Name = "labelWell0";
            this.labelWell0.Size = new System.Drawing.Size(31, 13);
            this.labelWell0.TabIndex = 0;
            this.labelWell0.Text = "Well Idx";
            // 
            // progressBarPlate
            // 
            this.progressBarPlate.Location = new System.Drawing.Point(69, 11);
            this.progressBarPlate.Name = "progressBarPlate";
            this.progressBarPlate.Size = new System.Drawing.Size(193, 23);
            this.progressBarPlate.TabIndex = 1;
            this.progressBarPlate.UseWaitCursor = true;
            // 
            // progressBarWell
            // 
            this.progressBarWell.Location = new System.Drawing.Point(69, 63);
            this.progressBarWell.Name = "progressBarWell";
            this.progressBarWell.Size = new System.Drawing.Size(193, 23);
            this.progressBarWell.TabIndex = 2;
            // 
            // labelWellIdx
            // 
            this.labelWellIdx.AutoSize = true;
            this.labelWellIdx.Location = new System.Drawing.Point(149, 89);
            this.labelWellIdx.Name = "labelWellIdx";
            this.labelWellIdx.Size = new System.Drawing.Size(30, 13);
            this.labelWellIdx.TabIndex = 0;
            this.labelWellIdx.Text = "0 / 0";
            this.labelWellIdx.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelPlateIdx
            // 
            this.labelPlateIdx.AutoSize = true;
            this.labelPlateIdx.Location = new System.Drawing.Point(149, 37);
            this.labelPlateIdx.Name = "labelPlateIdx";
            this.labelPlateIdx.Size = new System.Drawing.Size(30, 13);
            this.labelPlateIdx.TabIndex = 0;
            this.labelPlateIdx.Text = "0 / 0";
            this.labelPlateIdx.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FormForDoubleProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 123);
            this.ControlBox = false;
            this.Controls.Add(this.progressBarWell);
            this.Controls.Add(this.progressBarPlate);
            this.Controls.Add(this.labelPlateIdx);
            this.Controls.Add(this.labelWellIdx);
            this.Controls.Add(this.labelWell0);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormForDoubleProgress";
            this.Text = "In Progress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelWell0;
        public System.Windows.Forms.ProgressBar progressBarPlate;
        public System.Windows.Forms.ProgressBar progressBarWell;
        public System.Windows.Forms.Label labelWellIdx;
        public System.Windows.Forms.Label labelPlateIdx;
    }
}