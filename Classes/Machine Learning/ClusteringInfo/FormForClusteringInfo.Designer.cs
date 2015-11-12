namespace HCSAnalyzer.Forms.FormsForOptions
{
    partial class FormForClusteringInfo
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("EM");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("K-Means");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Hierarchical");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Farthest First");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("CobWeb");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForClusteringInfo));
            this.panelForDisplay = new System.Windows.Forms.Panel();
            this.treeViewForOptions = new System.Windows.Forms.TreeView();
            this.buttonOK = new System.Windows.Forms.Button();
            this.richTextBoxForInfo = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // panelForDisplay
            // 
            this.panelForDisplay.AutoScroll = true;
            this.panelForDisplay.Location = new System.Drawing.Point(187, 9);
            this.panelForDisplay.Name = "panelForDisplay";
            this.panelForDisplay.Size = new System.Drawing.Size(266, 276);
            this.panelForDisplay.TabIndex = 6;
            // 
            // treeViewForOptions
            // 
            this.treeViewForOptions.Location = new System.Drawing.Point(7, 9);
            this.treeViewForOptions.Name = "treeViewForOptions";
            treeNode1.Name = "Node0";
            treeNode1.Tag = "EM";
            treeNode1.Text = "EM";
            treeNode2.Name = "Node1";
            treeNode2.Tag = "K-Means";
            treeNode2.Text = "K-Means";
            treeNode3.Name = "Node0";
            treeNode3.Tag = "Hierarchical";
            treeNode3.Text = "Hierarchical";
            treeNode4.Name = "Node0";
            treeNode4.Tag = "FarthestFirst";
            treeNode4.Text = "Farthest First";
            treeNode5.Name = "Node1";
            treeNode5.Tag = "CobWeb";
            treeNode5.Text = "CobWeb";
            this.treeViewForOptions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            this.treeViewForOptions.ShowRootLines = false;
            this.treeViewForOptions.Size = new System.Drawing.Size(174, 276);
            this.treeViewForOptions.TabIndex = 5;
            this.treeViewForOptions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewForOptions_AfterSelect);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(378, 377);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // richTextBoxForInfo
            // 
            this.richTextBoxForInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.richTextBoxForInfo.Location = new System.Drawing.Point(7, 293);
            this.richTextBoxForInfo.Name = "richTextBoxForInfo";
            this.richTextBoxForInfo.ReadOnly = true;
            this.richTextBoxForInfo.Size = new System.Drawing.Size(446, 81);
            this.richTextBoxForInfo.TabIndex = 7;
            this.richTextBoxForInfo.Text = "";
            this.richTextBoxForInfo.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxForInfo_LinkClicked);
            // 
            // FormForClusteringInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 404);
            this.Controls.Add(this.richTextBoxForInfo);
            this.Controls.Add(this.panelForDisplay);
            this.Controls.Add(this.treeViewForOptions);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForClusteringInfo";
            this.Text = "Clustering parameters";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelForDisplay;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.RichTextBox richTextBoxForInfo;
        public System.Windows.Forms.TreeView treeViewForOptions;
    }
}