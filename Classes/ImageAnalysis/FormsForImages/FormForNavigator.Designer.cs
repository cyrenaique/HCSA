namespace HCSAnalyzer.Classes.ImageAnalysis.FormsForImages
{
    partial class FormForNavigator
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForNavigator));
            this.trackBarForZPos = new System.Windows.Forms.TrackBar();
            this.numericUpDownZPos = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStripNavigator = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exctractSliceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarForZPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZPos)).BeginInit();
            this.contextMenuStripNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBarForZPos
            // 
            this.trackBarForZPos.ContextMenuStrip = this.contextMenuStripNavigator;
            this.trackBarForZPos.Location = new System.Drawing.Point(11, 4);
            this.trackBarForZPos.Name = "trackBarForZPos";
            this.trackBarForZPos.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarForZPos.Size = new System.Drawing.Size(45, 141);
            this.trackBarForZPos.TabIndex = 0;
            this.trackBarForZPos.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarForZPos.Scroll += new System.EventHandler(this.trackBarForZPos_Scroll);
            // 
            // numericUpDownZPos
            // 
            this.numericUpDownZPos.ContextMenuStrip = this.contextMenuStripNavigator;
            this.numericUpDownZPos.Location = new System.Drawing.Point(11, 148);
            this.numericUpDownZPos.Name = "numericUpDownZPos";
            this.numericUpDownZPos.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownZPos.TabIndex = 1;
            this.numericUpDownZPos.ValueChanged += new System.EventHandler(this.numericUpDownZPos_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 179);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMenuStripNavigator
            // 
            this.contextMenuStripNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exctractSliceToolStripMenuItem});
            this.contextMenuStripNavigator.Name = "contextMenuStripNavigator";
            this.contextMenuStripNavigator.Size = new System.Drawing.Size(143, 26);
            // 
            // exctractSliceToolStripMenuItem
            // 
            this.exctractSliceToolStripMenuItem.Name = "exctractSliceToolStripMenuItem";
            this.exctractSliceToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.exctractSliceToolStripMenuItem.Text = "Exctract Slice";
            this.exctractSliceToolStripMenuItem.Click += new System.EventHandler(this.exctractSliceToolStripMenuItem_Click);
            // 
            // FormForNavigator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(67, 209);
            this.ContextMenuStrip = this.contextMenuStripNavigator;
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numericUpDownZPos);
            this.Controls.Add(this.trackBarForZPos);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormForNavigator";
            this.Text = "Navigator";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarForZPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZPos)).EndInit();
            this.contextMenuStripNavigator.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TrackBar trackBarForZPos;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.NumericUpDown numericUpDownZPos;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripNavigator;
        private System.Windows.Forms.ToolStripMenuItem exctractSliceToolStripMenuItem;
    }
}