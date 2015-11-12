namespace HCSAnalyzer.Controls
{
    partial class cWindowToDisplayGeneralArray
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cWindowToDisplayGeneralArray));
            this.panelForPlates = new System.Windows.Forms.Panel();
            this.buttonIncrease = new System.Windows.Forms.Button();
            this.buttonReduce = new System.Windows.Forms.Button();
            this.checkBoxGlobalNormalization = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayClasses = new System.Windows.Forms.CheckBox();
            this.comboBoxDistances = new System.Windows.Forms.ComboBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.menuStripOptions = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelForPlates
            // 
            this.panelForPlates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForPlates.AutoScroll = true;
            this.panelForPlates.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelForPlates.Location = new System.Drawing.Point(9, 27);
            this.panelForPlates.Name = "panelForPlates";
            this.panelForPlates.Size = new System.Drawing.Size(851, 417);
            this.panelForPlates.TabIndex = 0;
            // 
            // buttonIncrease
            // 
            this.buttonIncrease.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonIncrease.Location = new System.Drawing.Point(939, 372);
            this.buttonIncrease.Name = "buttonIncrease";
            this.buttonIncrease.Size = new System.Drawing.Size(58, 23);
            this.buttonIncrease.TabIndex = 1;
            this.buttonIncrease.Text = "+";
            this.buttonIncrease.UseVisualStyleBackColor = true;
            this.buttonIncrease.Click += new System.EventHandler(this.buttonIncrease_Click);
            // 
            // buttonReduce
            // 
            this.buttonReduce.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReduce.Location = new System.Drawing.Point(875, 372);
            this.buttonReduce.Name = "buttonReduce";
            this.buttonReduce.Size = new System.Drawing.Size(58, 23);
            this.buttonReduce.TabIndex = 1;
            this.buttonReduce.Text = "-";
            this.buttonReduce.UseVisualStyleBackColor = true;
            this.buttonReduce.Click += new System.EventHandler(this.buttonReduce_Click);
            // 
            // checkBoxGlobalNormalization
            // 
            this.checkBoxGlobalNormalization.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxGlobalNormalization.AutoSize = true;
            this.checkBoxGlobalNormalization.Location = new System.Drawing.Point(875, 338);
            this.checkBoxGlobalNormalization.Name = "checkBoxGlobalNormalization";
            this.checkBoxGlobalNormalization.Size = new System.Drawing.Size(122, 17);
            this.checkBoxGlobalNormalization.TabIndex = 2;
            this.checkBoxGlobalNormalization.Text = "Global Normalization";
            this.checkBoxGlobalNormalization.UseVisualStyleBackColor = true;
            this.checkBoxGlobalNormalization.CheckedChanged += new System.EventHandler(this.checkBoxGlobalNormalization_CheckedChanged);
            // 
            // checkBoxDisplayClasses
            // 
            this.checkBoxDisplayClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDisplayClasses.AutoSize = true;
            this.checkBoxDisplayClasses.Location = new System.Drawing.Point(887, 304);
            this.checkBoxDisplayClasses.Name = "checkBoxDisplayClasses";
            this.checkBoxDisplayClasses.Size = new System.Drawing.Size(96, 17);
            this.checkBoxDisplayClasses.TabIndex = 3;
            this.checkBoxDisplayClasses.Text = "DisplayClasses";
            this.checkBoxDisplayClasses.UseVisualStyleBackColor = true;
            this.checkBoxDisplayClasses.CheckedChanged += new System.EventHandler(this.checkBoxDisplayClasses_CheckedChanged);
            // 
            // comboBoxDistances
            // 
            this.comboBoxDistances.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDistances.FormattingEnabled = true;
            this.comboBoxDistances.Items.AddRange(new object[] {
            "Euclidean",
            "Manhattan",
            "VectorCos.",
            "Bhattacharyya",
            "EMD"});
            this.comboBoxDistances.Location = new System.Drawing.Point(874, 50);
            this.comboBoxDistances.Name = "comboBoxDistances";
            this.comboBoxDistances.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDistances.TabIndex = 4;
            this.comboBoxDistances.SelectedIndexChanged += new System.EventHandler(this.comboBoxDistances_SelectedIndexChanged);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(875, 421);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(120, 23);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // menuStripOptions
            // 
            this.menuStripOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.optionsToolStripMenuItem});
            this.menuStripOptions.Location = new System.Drawing.Point(0, 0);
            this.menuStripOptions.Name = "menuStripOptions";
            this.menuStripOptions.Size = new System.Drawing.Size(1009, 24);
            this.menuStripOptions.TabIndex = 6;
            this.menuStripOptions.Text = "Options";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // displayToolStripMenuItem
            // 
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            this.displayToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.displayToolStripMenuItem.Text = "Display";
            this.displayToolStripMenuItem.Click += new System.EventHandler(this.displayToolStripMenuItem_Click);
            // 
            // cWindowToDisplayGeneralArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1009, 456);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.comboBoxDistances);
            this.Controls.Add(this.checkBoxDisplayClasses);
            this.Controls.Add(this.checkBoxGlobalNormalization);
            this.Controls.Add(this.buttonReduce);
            this.Controls.Add(this.buttonIncrease);
            this.Controls.Add(this.panelForPlates);
            this.Controls.Add(this.menuStripOptions);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripOptions;
            this.Name = "cWindowToDisplayGeneralArray";
            this.Text = "Screening display";
            this.menuStripOptions.ResumeLayout(false);
            this.menuStripOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Panel panelForPlates;
        private System.Windows.Forms.Button buttonIncrease;
        private System.Windows.Forms.Button buttonReduce;
        public System.Windows.Forms.ComboBox comboBoxDistances;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.MenuStrip menuStripOptions;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayToolStripMenuItem;
        public System.Windows.Forms.CheckBox checkBoxDisplayClasses;
        public System.Windows.Forms.CheckBox checkBoxGlobalNormalization;



    }
}