namespace HCSAnalyzer.Cell_by_Cell_and_DB.Simulator.Forms
{
    partial class FormForModelsHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForModelsHistory));
            this.listViewForClassifHistory = new System.Windows.Forms.ListView();
            this.columnHeaderModelName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderNumFolds = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMeanError = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonOk = new System.Windows.Forms.Button();
            this.richTextBoxModel = new System.Windows.Forms.RichTextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageModel = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.richTextBoxCV = new System.Windows.Forms.RichTextBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tabControl.SuspendLayout();
            this.tabPageModel.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewForClassifHistory
            // 
            this.listViewForClassifHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewForClassifHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderModelName,
            this.columnHeaderNumFolds,
            this.columnHeaderMeanError});
            this.listViewForClassifHistory.FullRowSelect = true;
            this.listViewForClassifHistory.GridLines = true;
            this.listViewForClassifHistory.Location = new System.Drawing.Point(3, 3);
            this.listViewForClassifHistory.MultiSelect = false;
            this.listViewForClassifHistory.Name = "listViewForClassifHistory";
            this.listViewForClassifHistory.Size = new System.Drawing.Size(287, 426);
            this.listViewForClassifHistory.TabIndex = 10;
            this.listViewForClassifHistory.UseCompatibleStateImageBehavior = false;
            this.listViewForClassifHistory.View = System.Windows.Forms.View.Details;
            this.listViewForClassifHistory.SelectedIndexChanged += new System.EventHandler(this.listViewForCellPopulations_SelectedIndexChanged);
            this.listViewForClassifHistory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewForCellPopulations_MouseDown);
            // 
            // columnHeaderModelName
            // 
            this.columnHeaderModelName.Text = "Name";
            this.columnHeaderModelName.Width = 101;
            // 
            // columnHeaderNumFolds
            // 
            this.columnHeaderNumFolds.Text = "CV Num. Folds";
            this.columnHeaderNumFolds.Width = 84;
            // 
            // columnHeaderMeanError
            // 
            this.columnHeaderMeanError.Text = "Mean Error";
            this.columnHeaderMeanError.Width = 94;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(488, 437);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(106, 23);
            this.buttonOk.TabIndex = 11;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // richTextBoxModel
            // 
            this.richTextBoxModel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxModel.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxModel.Name = "richTextBoxModel";
            this.richTextBoxModel.Size = new System.Drawing.Size(274, 394);
            this.richTextBoxModel.TabIndex = 12;
            this.richTextBoxModel.Text = "";
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageModel);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(3, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(288, 426);
            this.tabControl.TabIndex = 13;
            // 
            // tabPageModel
            // 
            this.tabPageModel.Controls.Add(this.richTextBoxModel);
            this.tabPageModel.Location = new System.Drawing.Point(4, 22);
            this.tabPageModel.Name = "tabPageModel";
            this.tabPageModel.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageModel.Size = new System.Drawing.Size(280, 400);
            this.tabPageModel.TabIndex = 0;
            this.tabPageModel.Text = "Model";
            this.tabPageModel.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.richTextBoxCV);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(280, 400);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Cross-validation";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // richTextBoxCV
            // 
            this.richTextBoxCV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxCV.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxCV.Name = "richTextBoxCV";
            this.richTextBoxCV.Size = new System.Drawing.Size(274, 394);
            this.richTextBoxCV.TabIndex = 13;
            this.richTextBoxCV.Text = "";
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(3, 4);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.listViewForClassifHistory);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabControl);
            this.splitContainer.Size = new System.Drawing.Size(591, 432);
            this.splitContainer.SplitterDistance = 293;
            this.splitContainer.TabIndex = 14;
            // 
            // FormForModelsHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 463);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.buttonOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForModelsHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Models History";
            this.tabControl.ResumeLayout(false);
            this.tabPageModel.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView listViewForClassifHistory;
        private System.Windows.Forms.ColumnHeader columnHeaderModelName;
        private System.Windows.Forms.ColumnHeader columnHeaderNumFolds;
        private System.Windows.Forms.ColumnHeader columnHeaderMeanError;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.RichTextBox richTextBoxModel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageModel;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox richTextBoxCV;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}