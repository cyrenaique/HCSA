namespace IM3_Plugin3
{
    partial class Plugin3D
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Plugin3D));
            this.buttonProcessImage = new System.Windows.Forms.Button();
            this.panelFor3DControls = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.imageListForThumbnail = new System.Windows.Forms.ImageList(this.components);
            this.panelLine = new System.Windows.Forms.Panel();
            this.contextMenuStripFor3D = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.postProcessingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonAddObject = new System.Windows.Forms.Button();
            this.buttonRemoveObject = new System.Windows.Forms.Button();
            this.textBoxNumberOfObjects = new System.Windows.Forms.TextBox();
            this.contextMenuStripFor3D.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonProcessImage
            // 
            this.buttonProcessImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonProcessImage.Location = new System.Drawing.Point(631, 775);
            this.buttonProcessImage.Name = "buttonProcessImage";
            this.buttonProcessImage.Size = new System.Drawing.Size(150, 32);
            this.buttonProcessImage.TabIndex = 2;
            this.buttonProcessImage.Text = "Process Image";
            this.buttonProcessImage.UseVisualStyleBackColor = true;
            this.buttonProcessImage.Click += new System.EventHandler(this.buttonProcessImage_Click);
            // 
            // panelFor3DControls
            // 
            this.panelFor3DControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFor3DControls.AutoScroll = true;
            this.panelFor3DControls.Location = new System.Drawing.Point(12, 34);
            this.panelFor3DControls.Name = "panelFor3DControls";
            this.panelFor3DControls.Size = new System.Drawing.Size(769, 717);
            this.panelFor3DControls.TabIndex = 3;
            this.panelFor3DControls.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelFor3DControls_MouseDown);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(24, 785);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Number of Objects";
            // 
            // imageListForThumbnail
            // 
            this.imageListForThumbnail.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListForThumbnail.ImageStream")));
            this.imageListForThumbnail.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListForThumbnail.Images.SetKeyName(0, "nucleus.jpg");
            this.imageListForThumbnail.Images.SetKeyName(1, "centriole.jpg");
            this.imageListForThumbnail.Images.SetKeyName(2, "Foci.jpg");
            this.imageListForThumbnail.Images.SetKeyName(3, "Nucleolus.jpg");
            this.imageListForThumbnail.Images.SetKeyName(4, "What.png");
            this.imageListForThumbnail.Images.SetKeyName(5, "Centriole.png");
            this.imageListForThumbnail.Images.SetKeyName(6, "Foci.png");
            this.imageListForThumbnail.Images.SetKeyName(7, "Nucleolus.png");
            this.imageListForThumbnail.Images.SetKeyName(8, "Nucleus.png");
            this.imageListForThumbnail.Images.SetKeyName(9, "master.png");
            // 
            // panelLine
            // 
            this.panelLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLine.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.panelLine.Location = new System.Drawing.Point(141, 761);
            this.panelLine.Name = "panelLine";
            this.panelLine.Size = new System.Drawing.Size(510, 1);
            this.panelLine.TabIndex = 6;
            // 
            // contextMenuStripFor3D
            // 
            this.contextMenuStripFor3D.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.postProcessingsToolStripMenuItem});
            this.contextMenuStripFor3D.Name = "contextMenuStripFor3D";
            this.contextMenuStripFor3D.Size = new System.Drawing.Size(163, 26);
            // 
            // postProcessingsToolStripMenuItem
            // 
            this.postProcessingsToolStripMenuItem.Name = "postProcessingsToolStripMenuItem";
            this.postProcessingsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.postProcessingsToolStripMenuItem.Text = "Post Processings";
            this.postProcessingsToolStripMenuItem.Click += new System.EventHandler(this.postProcessingsToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(793, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveConfigurationToolStripMenuItem,
            this.loadConfigurationToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
            this.exportToolStripMenuItem.Text = "I/O";
            // 
            // saveConfigurationToolStripMenuItem
            // 
            this.saveConfigurationToolStripMenuItem.Name = "saveConfigurationToolStripMenuItem";
            this.saveConfigurationToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.saveConfigurationToolStripMenuItem.Text = "Save configuration";
            this.saveConfigurationToolStripMenuItem.Click += new System.EventHandler(this.saveConfigurationToolStripMenuItem_Click);
            // 
            // loadConfigurationToolStripMenuItem
            // 
            this.loadConfigurationToolStripMenuItem.Name = "loadConfigurationToolStripMenuItem";
            this.loadConfigurationToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.loadConfigurationToolStripMenuItem.Text = "Load configuration";
            this.loadConfigurationToolStripMenuItem.Click += new System.EventHandler(this.loadConfigurationToolStripMenuItem_Click);
            // 
            // buttonAddObject
            // 
            this.buttonAddObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddObject.Location = new System.Drawing.Point(418, 777);
            this.buttonAddObject.Name = "buttonAddObject";
            this.buttonAddObject.Size = new System.Drawing.Size(75, 32);
            this.buttonAddObject.TabIndex = 8;
            this.buttonAddObject.Text = "Add Object";
            this.buttonAddObject.UseVisualStyleBackColor = true;
            this.buttonAddObject.Click += new System.EventHandler(this.buttonAddObject_Click);
            // 
            // buttonRemoveObject
            // 
            this.buttonRemoveObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveObject.Location = new System.Drawing.Point(326, 777);
            this.buttonRemoveObject.Name = "buttonRemoveObject";
            this.buttonRemoveObject.Size = new System.Drawing.Size(75, 32);
            this.buttonRemoveObject.TabIndex = 9;
            this.buttonRemoveObject.Text = "Remove Object";
            this.buttonRemoveObject.UseVisualStyleBackColor = true;
            this.buttonRemoveObject.Click += new System.EventHandler(this.buttonRemoveObject_Click);
            // 
            // textBoxNumberOfObjects
            // 
            this.textBoxNumberOfObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxNumberOfObjects.Location = new System.Drawing.Point(139, 782);
            this.textBoxNumberOfObjects.Name = "textBoxNumberOfObjects";
            this.textBoxNumberOfObjects.ReadOnly = true;
            this.textBoxNumberOfObjects.Size = new System.Drawing.Size(100, 20);
            this.textBoxNumberOfObjects.TabIndex = 10;
            this.textBoxNumberOfObjects.Text = "0";
            // 
            // Plugin3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(793, 821);
            this.Controls.Add(this.textBoxNumberOfObjects);
            this.Controls.Add(this.buttonRemoveObject);
            this.Controls.Add(this.buttonAddObject);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panelLine);
            this.Controls.Add(this.panelFor3DControls);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonProcessImage);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Plugin3D";
            this.Text = "3D plugin";
            this.contextMenuStripFor3D.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Button buttonProcessImage;
        private System.Windows.Forms.Panel panelFor3DControls;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ImageList imageListForThumbnail;
        public System.Windows.Forms.Panel panelLine;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFor3D;
        private System.Windows.Forms.ToolStripMenuItem postProcessingsToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadConfigurationToolStripMenuItem;
        private System.Windows.Forms.Button buttonAddObject;
        private System.Windows.Forms.Button buttonRemoveObject;
        private System.Windows.Forms.TextBox textBoxNumberOfObjects;
    }
}

