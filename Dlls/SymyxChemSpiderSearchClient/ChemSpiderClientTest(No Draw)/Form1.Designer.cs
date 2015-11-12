namespace ChemSpiderClientTest_No_Draw_
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.elementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.structureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intrinsicPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lassoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.predictedPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.similarityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subStructureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setTokenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(20, 64);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(376, 220);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 43);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Result:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 301);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(409, 26);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.elementsToolStripMenuItem,
            this.structureToolStripMenuItem,
            this.intrinsicPropertyToolStripMenuItem,
            this.lassoToolStripMenuItem,
            this.predictedPropertyToolStripMenuItem,
            this.similarityToolStripMenuItem,
            this.subStructureToolStripMenuItem,
            this.simpleToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.setTokenToolStripMenuItem});
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(69, 24);
            this.toolStripSplitButton1.Text = "Search";
            this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
            // 
            // elementsToolStripMenuItem
            // 
            this.elementsToolStripMenuItem.Name = "elementsToolStripMenuItem";
            this.elementsToolStripMenuItem.Size = new System.Drawing.Size(201, 24);
            this.elementsToolStripMenuItem.Text = "Elements";
            this.elementsToolStripMenuItem.Click += new System.EventHandler(this.elementsToolStripMenuItem_Click);
            // 
            // structureToolStripMenuItem
            // 
            this.structureToolStripMenuItem.Name = "structureToolStripMenuItem";
            this.structureToolStripMenuItem.Size = new System.Drawing.Size(201, 24);
            this.structureToolStripMenuItem.Text = "Exact Structure";
            this.structureToolStripMenuItem.Click += new System.EventHandler(this.structureToolStripMenuItem_Click);
            // 
            // intrinsicPropertyToolStripMenuItem
            // 
            this.intrinsicPropertyToolStripMenuItem.Name = "intrinsicPropertyToolStripMenuItem";
            this.intrinsicPropertyToolStripMenuItem.Size = new System.Drawing.Size(201, 24);
            this.intrinsicPropertyToolStripMenuItem.Text = "Intrinsic Property";
            this.intrinsicPropertyToolStripMenuItem.Click += new System.EventHandler(this.intrinsicPropertyToolStripMenuItem_Click);
            // 
            // lassoToolStripMenuItem
            // 
            this.lassoToolStripMenuItem.Name = "lassoToolStripMenuItem";
            this.lassoToolStripMenuItem.Size = new System.Drawing.Size(201, 24);
            this.lassoToolStripMenuItem.Text = "LASSO";
            this.lassoToolStripMenuItem.Click += new System.EventHandler(this.lassoToolStripMenuItem_Click);
            // 
            // predictedPropertyToolStripMenuItem
            // 
            this.predictedPropertyToolStripMenuItem.Name = "predictedPropertyToolStripMenuItem";
            this.predictedPropertyToolStripMenuItem.Size = new System.Drawing.Size(201, 24);
            this.predictedPropertyToolStripMenuItem.Text = "Predicted Property";
            this.predictedPropertyToolStripMenuItem.Click += new System.EventHandler(this.predictedPropertyToolStripMenuItem_Click);
            // 
            // similarityToolStripMenuItem
            // 
            this.similarityToolStripMenuItem.Name = "similarityToolStripMenuItem";
            this.similarityToolStripMenuItem.Size = new System.Drawing.Size(201, 24);
            this.similarityToolStripMenuItem.Text = "Similarity";
            this.similarityToolStripMenuItem.Click += new System.EventHandler(this.similarityToolStripMenuItem_Click);
            // 
            // subStructureToolStripMenuItem
            // 
            this.subStructureToolStripMenuItem.Name = "subStructureToolStripMenuItem";
            this.subStructureToolStripMenuItem.Size = new System.Drawing.Size(201, 24);
            this.subStructureToolStripMenuItem.Text = "Substructure";
            this.subStructureToolStripMenuItem.Click += new System.EventHandler(this.subStructureToolStripMenuItem_Click);
            // 
            // simpleToolStripMenuItem
            // 
            this.simpleToolStripMenuItem.Name = "simpleToolStripMenuItem";
            this.simpleToolStripMenuItem.Size = new System.Drawing.Size(201, 24);
            this.simpleToolStripMenuItem.Text = "Text";
            this.simpleToolStripMenuItem.Click += new System.EventHandler(this.simpleToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(201, 24);
            this.optionsToolStripMenuItem.Text = "Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // setTokenToolStripMenuItem
            // 
            this.setTokenToolStripMenuItem.Name = "setTokenToolStripMenuItem";
            this.setTokenToolStripMenuItem.Size = new System.Drawing.Size(201, 24);
            this.setTokenToolStripMenuItem.Text = "Set Token...";
            this.setTokenToolStripMenuItem.Click += new System.EventHandler(this.setTokenToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Input String:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(107, 7);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(288, 22);
            this.textBox1.TabIndex = 9;
            this.toolTip1.SetToolTip(this.textBox1, "Type some a name to search for or paste in a SMILES string...");
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(303, 306);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(106, 17);
            this.linkLabel1.TabIndex = 11;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Search Options";
            this.toolTip1.SetToolTip(this.linkLabel1, "Learn about ChemSpider search options...");
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 327);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "ChemSpider Test Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem elementsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem structureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intrinsicPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lassoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem predictedPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem similarityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subStructureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simpleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem setTokenToolStripMenuItem;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

