namespace ChemSpiderClient
{
    partial class frmResults
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResults));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbTransfer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMoveFirst = new System.Windows.Forms.ToolStripButton();
            this.tsbMovePrevious = new System.Windows.Forms.ToolStripButton();
            this.tsbMoveNext = new System.Windows.Forms.ToolStripButton();
            this.tsbMoveLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOpenInBrowser = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 445);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(672, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(50, 17);
            this.toolStripStatusLabel1.Text = "Found...";
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(0, 25);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(672, 420);
            this.webBrowser1.TabIndex = 1;
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbTransfer,
            this.toolStripSeparator2,
            this.tsbMoveFirst,
            this.tsbMovePrevious,
            this.tsbMoveNext,
            this.tsbMoveLast,
            this.toolStripSeparator1,
            this.tsbOpenInBrowser});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(672, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbTransfer
            // 
            this.tsbTransfer.Image = ((System.Drawing.Image)(resources.GetObject("tsbTransfer.Image")));
            this.tsbTransfer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTransfer.Name = "tsbTransfer";
            this.tsbTransfer.Size = new System.Drawing.Size(70, 22);
            this.tsbTransfer.Text = "Transfer";
            this.tsbTransfer.Click += new System.EventHandler(this.tsbTransfer_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbMoveFirst
            // 
            this.tsbMoveFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMoveFirst.Image = ((System.Drawing.Image)(resources.GetObject("tsbMoveFirst.Image")));
            this.tsbMoveFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMoveFirst.Name = "tsbMoveFirst";
            this.tsbMoveFirst.Size = new System.Drawing.Size(23, 22);
            this.tsbMoveFirst.Text = "Move First";
            this.tsbMoveFirst.ToolTipText = "Move First";
            this.tsbMoveFirst.Click += new System.EventHandler(this.tsbMoveFirst_Click);
            // 
            // tsbMovePrevious
            // 
            this.tsbMovePrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMovePrevious.Image = ((System.Drawing.Image)(resources.GetObject("tsbMovePrevious.Image")));
            this.tsbMovePrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMovePrevious.Name = "tsbMovePrevious";
            this.tsbMovePrevious.Size = new System.Drawing.Size(23, 22);
            this.tsbMovePrevious.Text = "Move Previous";
            this.tsbMovePrevious.ToolTipText = "Move Previous";
            this.tsbMovePrevious.Click += new System.EventHandler(this.tsbMovePrevious_Click);
            // 
            // tsbMoveNext
            // 
            this.tsbMoveNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMoveNext.Image = ((System.Drawing.Image)(resources.GetObject("tsbMoveNext.Image")));
            this.tsbMoveNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMoveNext.Name = "tsbMoveNext";
            this.tsbMoveNext.Size = new System.Drawing.Size(23, 22);
            this.tsbMoveNext.Text = "Move Next";
            this.tsbMoveNext.ToolTipText = "Move Next";
            this.tsbMoveNext.Click += new System.EventHandler(this.tsbMoveNext_Click);
            // 
            // tsbMoveLast
            // 
            this.tsbMoveLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMoveLast.Image = ((System.Drawing.Image)(resources.GetObject("tsbMoveLast.Image")));
            this.tsbMoveLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMoveLast.Name = "tsbMoveLast";
            this.tsbMoveLast.Size = new System.Drawing.Size(23, 22);
            this.tsbMoveLast.Text = "Move Last";
            this.tsbMoveLast.ToolTipText = "Move Last";
            this.tsbMoveLast.Click += new System.EventHandler(this.tsbMoveLast_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbOpenInBrowser
            // 
            this.tsbOpenInBrowser.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenInBrowser.Image")));
            this.tsbOpenInBrowser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenInBrowser.Name = "tsbOpenInBrowser";
            this.tsbOpenInBrowser.Size = new System.Drawing.Size(114, 22);
            this.tsbOpenInBrowser.Text = "Open in Browser";
            this.tsbOpenInBrowser.Click += new System.EventHandler(this.tsbOpenInBrowser_Click);
            // 
            // frmResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 467);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmResults";
            this.Text = "ChemSpider Search Results";
            this.Shown += new System.EventHandler(this.frmResults_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbMoveFirst;
        private System.Windows.Forms.ToolStripButton tsbMovePrevious;
        private System.Windows.Forms.ToolStripButton tsbMoveNext;
        private System.Windows.Forms.ToolStripButton tsbMoveLast;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbTransfer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbOpenInBrowser;
    }
}