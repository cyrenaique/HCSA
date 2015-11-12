namespace HCSAnalyzer.Simulator.Forms
{
    partial class FormFor3DVisu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFor3DVisu));
            this.renderWindowControlFor3D = new Kitware.VTK.RenderWindowControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonDisplayVolumes = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // renderWindowControlFor3D
            // 
            this.renderWindowControlFor3D.AddTestActors = false;
            this.renderWindowControlFor3D.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.renderWindowControlFor3D.Location = new System.Drawing.Point(12, 12);
            this.renderWindowControlFor3D.Name = "renderWindowControlFor3D";
            this.renderWindowControlFor3D.Size = new System.Drawing.Size(745, 535);
            this.renderWindowControlFor3D.TabIndex = 1;
            this.renderWindowControlFor3D.TestText = null;
            this.renderWindowControlFor3D.Load += new System.EventHandler(this.renderWindowControlFor3D_Load);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonDisplayVolumes);
            this.groupBox1.Location = new System.Drawing.Point(763, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 543);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Info";
            // 
            // buttonDisplayVolumes
            // 
            this.buttonDisplayVolumes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDisplayVolumes.Location = new System.Drawing.Point(25, 31);
            this.buttonDisplayVolumes.Name = "buttonDisplayVolumes";
            this.buttonDisplayVolumes.Size = new System.Drawing.Size(106, 23);
            this.buttonDisplayVolumes.TabIndex = 0;
            this.buttonDisplayVolumes.Text = "Display Volumes";
            this.buttonDisplayVolumes.UseVisualStyleBackColor = true;
            this.buttonDisplayVolumes.Click += new System.EventHandler(this.buttonDisplayVolumes_Click);
            // 
            // FormFor3DVisu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 559);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.renderWindowControlFor3D);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFor3DVisu";
            this.Text = "Simulation 3D Display";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public Kitware.VTK.RenderWindowControl renderWindowControlFor3D;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonDisplayVolumes;

    }
}