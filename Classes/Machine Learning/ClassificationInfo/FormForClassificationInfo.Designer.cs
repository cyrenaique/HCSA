namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{
    partial class FormForClassificationInfo
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("J48");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Random Forest");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Random Tree");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Trees", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("K*");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("KNN");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Lazy", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("SVM");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Perceptron");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Logistic");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Functions", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("ZeroR");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("OneR");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Rules", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Naive Bayes");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Bayes", new System.Windows.Forms.TreeNode[] {
            treeNode15});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForClassificationInfo));
            this.richTextBoxForInfo = new System.Windows.Forms.RichTextBox();
            this.panelForDisplay = new System.Windows.Forms.Panel();
            this.treeViewForOptions = new System.Windows.Forms.TreeView();
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkBoxCrossValidation = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.numericUpDownFoldNumber = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFoldNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxForInfo
            // 
            this.richTextBoxForInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.richTextBoxForInfo.Location = new System.Drawing.Point(8, 290);
            this.richTextBoxForInfo.Name = "richTextBoxForInfo";
            this.richTextBoxForInfo.ReadOnly = true;
            this.richTextBoxForInfo.Size = new System.Drawing.Size(446, 81);
            this.richTextBoxForInfo.TabIndex = 11;
            this.richTextBoxForInfo.Text = "";
            // 
            // panelForDisplay
            // 
            this.panelForDisplay.AutoScroll = true;
            this.panelForDisplay.Location = new System.Drawing.Point(188, 9);
            this.panelForDisplay.Name = "panelForDisplay";
            this.panelForDisplay.Size = new System.Drawing.Size(266, 276);
            this.panelForDisplay.TabIndex = 10;
            // 
            // treeViewForOptions
            // 
            this.treeViewForOptions.Location = new System.Drawing.Point(8, 9);
            this.treeViewForOptions.Name = "treeViewForOptions";
            treeNode1.Name = "Node0";
            treeNode1.Tag = "J48";
            treeNode1.Text = "J48";
            treeNode2.Name = "Node0";
            treeNode2.Tag = "RandomForest";
            treeNode2.Text = "Random Forest";
            treeNode3.Name = "Node0";
            treeNode3.Tag = "RandomTree";
            treeNode3.Text = "Random Tree";
            treeNode4.Name = "Node1";
            treeNode4.Text = "Trees";
            treeNode5.Name = "Node3";
            treeNode5.Tag = "KStar";
            treeNode5.Text = "K*";
            treeNode6.Name = "Node1";
            treeNode6.Tag = "KNN";
            treeNode6.Text = "KNN";
            treeNode7.Name = "Node2";
            treeNode7.Text = "Lazy";
            treeNode8.Name = "Node2";
            treeNode8.Tag = "SVM";
            treeNode8.Text = "SVM";
            treeNode9.Name = "Node3";
            treeNode9.Tag = "Perceptron";
            treeNode9.Text = "Perceptron";
            treeNode10.Name = "Node0";
            treeNode10.Tag = "Logistic";
            treeNode10.Text = "Logistic";
            treeNode11.Name = "Node0";
            treeNode11.Text = "Functions";
            treeNode12.Name = "Node0";
            treeNode12.Tag = "ZeroR";
            treeNode12.Text = "ZeroR";
            treeNode13.Name = "Node1";
            treeNode13.Tag = "OneR";
            treeNode13.Text = "OneR";
            treeNode14.Name = "Node0";
            treeNode14.Tag = "";
            treeNode14.Text = "Rules";
            treeNode15.Name = "Node1";
            treeNode15.Tag = "NaiveBayes";
            treeNode15.Text = "Naive Bayes";
            treeNode16.Name = "Node0";
            treeNode16.Text = "Bayes";
            this.treeViewForOptions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode7,
            treeNode11,
            treeNode14,
            treeNode16});
            this.treeViewForOptions.Size = new System.Drawing.Size(174, 276);
            this.treeViewForOptions.TabIndex = 9;
            this.treeViewForOptions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewForOptions_AfterSelect);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(381, 379);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // checkBoxCrossValidation
            // 
            this.checkBoxCrossValidation.AutoSize = true;
            this.checkBoxCrossValidation.Location = new System.Drawing.Point(8, 379);
            this.checkBoxCrossValidation.Name = "checkBoxCrossValidation";
            this.checkBoxCrossValidation.Size = new System.Drawing.Size(101, 17);
            this.checkBoxCrossValidation.TabIndex = 12;
            this.checkBoxCrossValidation.Text = "Cross Validation";
            this.toolTip.SetToolTip(this.checkBoxCrossValidation, "Perform cross validation of the model");
            this.checkBoxCrossValidation.UseVisualStyleBackColor = true;
            this.checkBoxCrossValidation.CheckedChanged += new System.EventHandler(this.checkBoxCrossValidation_CheckedChanged);
            // 
            // numericUpDownFoldNumber
            // 
            this.numericUpDownFoldNumber.Location = new System.Drawing.Point(115, 377);
            this.numericUpDownFoldNumber.Name = "numericUpDownFoldNumber";
            this.numericUpDownFoldNumber.Size = new System.Drawing.Size(65, 20);
            this.numericUpDownFoldNumber.TabIndex = 13;
            this.toolTip.SetToolTip(this.numericUpDownFoldNumber, "Fold number for K-folds cross validation");
            this.numericUpDownFoldNumber.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // FormForClassificationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 407);
            this.Controls.Add(this.numericUpDownFoldNumber);
            this.Controls.Add(this.checkBoxCrossValidation);
            this.Controls.Add(this.richTextBoxForInfo);
            this.Controls.Add(this.panelForDisplay);
            this.Controls.Add(this.treeViewForOptions);
            this.Controls.Add(this.buttonOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForClassificationInfo";
            this.Text = "Classification parameters";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFoldNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxForInfo;
        private System.Windows.Forms.Panel panelForDisplay;
        public System.Windows.Forms.TreeView treeViewForOptions;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckBox checkBoxCrossValidation;
        private System.Windows.Forms.ToolTip toolTip;
        public System.Windows.Forms.NumericUpDown numericUpDownFoldNumber;
    }
}