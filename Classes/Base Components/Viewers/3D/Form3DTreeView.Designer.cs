namespace HCSAnalyzer.Classes.Base_Components.Viewers._3D
{
    partial class Form3DTreeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3DTreeView));
            this.treeViewFor3DObjects = new System.Windows.Forms.TreeView();
            this.MainsplitContainer = new System.Windows.Forms.SplitContainer();
            this.richTextBoxFor3DTreeViewInfo = new System.Windows.Forms.RichTextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MainsplitContainer)).BeginInit();
            this.MainsplitContainer.Panel1.SuspendLayout();
            this.MainsplitContainer.Panel2.SuspendLayout();
            this.MainsplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewFor3DObjects
            // 
            this.treeViewFor3DObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewFor3DObjects.Location = new System.Drawing.Point(3, 3);
            this.treeViewFor3DObjects.Name = "treeViewFor3DObjects";
            this.treeViewFor3DObjects.Size = new System.Drawing.Size(274, 378);
            this.treeViewFor3DObjects.TabIndex = 0;
            this.treeViewFor3DObjects.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewFor3DObjects_NodeMouseClick);
            // 
            // MainsplitContainer
            // 
            this.MainsplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MainsplitContainer.Location = new System.Drawing.Point(3, 5);
            this.MainsplitContainer.Name = "MainsplitContainer";
            // 
            // MainsplitContainer.Panel1
            // 
            this.MainsplitContainer.Panel1.Controls.Add(this.treeViewFor3DObjects);
            // 
            // MainsplitContainer.Panel2
            // 
            this.MainsplitContainer.Panel2.Controls.Add(this.richTextBoxFor3DTreeViewInfo);
            this.MainsplitContainer.Size = new System.Drawing.Size(467, 384);
            this.MainsplitContainer.SplitterDistance = 280;
            this.MainsplitContainer.TabIndex = 1;
            // 
            // richTextBoxFor3DTreeViewInfo
            // 
            this.richTextBoxFor3DTreeViewInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxFor3DTreeViewInfo.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxFor3DTreeViewInfo.Name = "richTextBoxFor3DTreeViewInfo";
            this.richTextBoxFor3DTreeViewInfo.Size = new System.Drawing.Size(177, 378);
            this.richTextBoxFor3DTreeViewInfo.TabIndex = 0;
            this.richTextBoxFor3DTreeViewInfo.Text = "";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(395, 395);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // Form3DTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 425);
            this.ControlBox = false;
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.MainsplitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form3DTreeView";
            this.Text = "3D Objects Manager";
            this.MainsplitContainer.Panel1.ResumeLayout(false);
            this.MainsplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainsplitContainer)).EndInit();
            this.MainsplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewFor3DObjects;
        private System.Windows.Forms.SplitContainer MainsplitContainer;
        private System.Windows.Forms.RichTextBox richTextBoxFor3DTreeViewInfo;
        private System.Windows.Forms.Button buttonClose;
    }
}