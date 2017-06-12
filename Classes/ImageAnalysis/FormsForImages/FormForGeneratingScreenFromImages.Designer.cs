namespace HCSAnalyzer.Classes.ImageAnalysis.FormsForImages
{
    partial class FormForGeneratingScreenFromImages
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForGeneratingScreenFromImages));
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxImageRoot = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPlateForm = new System.Windows.Forms.TextBox();
            this.numericUpDownChannelNumber = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownFieldNumber = new System.Windows.Forms.NumericUpDown();
            this.treeViewForScreenInspection = new System.Windows.Forms.TreeView();
            this.buttonInspect = new System.Windows.Forms.Button();
            this.richTextBoxReport = new System.Windows.Forms.RichTextBox();
            this.buttonParse = new System.Windows.Forms.Button();
            this.tabControlForInfo = new System.Windows.Forms.TabControl();
            this.tabPageImageProp = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChannelNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFieldNumber)).BeginInit();
            this.tabControlForInfo.SuspendLayout();
            this.tabPageImageProp.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonGenerate.Location = new System.Drawing.Point(611, 330);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(98, 23);
            this.buttonGenerate.TabIndex = 0;
            this.buttonGenerate.Text = "Start";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Image Root";
            // 
            // textBoxImageRoot
            // 
            this.textBoxImageRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxImageRoot.Location = new System.Drawing.Point(80, 39);
            this.textBoxImageRoot.Name = "textBoxImageRoot";
            this.textBoxImageRoot.Size = new System.Drawing.Size(598, 20);
            this.textBoxImageRoot.TabIndex = 2;
            this.textBoxImageRoot.TextChanged += new System.EventHandler(this.textBoxImageRoot_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Platform";
            // 
            // textBoxPlateForm
            // 
            this.textBoxPlateForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPlateForm.Location = new System.Drawing.Point(80, 12);
            this.textBoxPlateForm.Name = "textBoxPlateForm";
            this.textBoxPlateForm.Size = new System.Drawing.Size(629, 20);
            this.textBoxPlateForm.TabIndex = 4;
            // 
            // numericUpDownChannelNumber
            // 
            this.numericUpDownChannelNumber.Location = new System.Drawing.Point(56, 29);
            this.numericUpDownChannelNumber.Name = "numericUpDownChannelNumber";
            this.numericUpDownChannelNumber.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownChannelNumber.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Channel Number(s)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Field Number(s)";
            // 
            // numericUpDownFieldNumber
            // 
            this.numericUpDownFieldNumber.Location = new System.Drawing.Point(56, 94);
            this.numericUpDownFieldNumber.Name = "numericUpDownFieldNumber";
            this.numericUpDownFieldNumber.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownFieldNumber.TabIndex = 7;
            // 
            // treeViewForScreenInspection
            // 
            this.treeViewForScreenInspection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewForScreenInspection.CheckBoxes = true;
            this.treeViewForScreenInspection.Location = new System.Drawing.Point(115, 70);
            this.treeViewForScreenInspection.Name = "treeViewForScreenInspection";
            this.treeViewForScreenInspection.Size = new System.Drawing.Size(213, 283);
            this.treeViewForScreenInspection.TabIndex = 9;
            this.treeViewForScreenInspection.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewForScreenInspection_AfterCheck);
            this.treeViewForScreenInspection.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewForScreenInspection_NodeMouseClick);
            this.treeViewForScreenInspection.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewForScreenInspection_NodeMouseDoubleClick);
            // 
            // buttonInspect
            // 
            this.buttonInspect.Location = new System.Drawing.Point(15, 70);
            this.buttonInspect.Name = "buttonInspect";
            this.buttonInspect.Size = new System.Drawing.Size(94, 23);
            this.buttonInspect.TabIndex = 10;
            this.buttonInspect.Text = "Parse Root";
            this.buttonInspect.UseVisualStyleBackColor = true;
            this.buttonInspect.Click += new System.EventHandler(this.buttonInspect_Click);
            // 
            // richTextBoxReport
            // 
            this.richTextBoxReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxReport.Location = new System.Drawing.Point(334, 70);
            this.richTextBoxReport.Name = "richTextBoxReport";
            this.richTextBoxReport.Size = new System.Drawing.Size(198, 283);
            this.richTextBoxReport.TabIndex = 11;
            this.richTextBoxReport.Text = "";
            // 
            // buttonParse
            // 
            this.buttonParse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonParse.Location = new System.Drawing.Point(684, 37);
            this.buttonParse.Name = "buttonParse";
            this.buttonParse.Size = new System.Drawing.Size(25, 23);
            this.buttonParse.TabIndex = 12;
            this.buttonParse.Text = "...";
            this.buttonParse.UseVisualStyleBackColor = true;
            this.buttonParse.Click += new System.EventHandler(this.buttonParse_Click);
            // 
            // tabControlForInfo
            // 
            this.tabControlForInfo.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControlForInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlForInfo.Controls.Add(this.tabPageImageProp);
            this.tabControlForInfo.Controls.Add(this.tabPage2);
            this.tabControlForInfo.Location = new System.Drawing.Point(538, 68);
            this.tabControlForInfo.Multiline = true;
            this.tabControlForInfo.Name = "tabControlForInfo";
            this.tabControlForInfo.SelectedIndex = 0;
            this.tabControlForInfo.Size = new System.Drawing.Size(171, 256);
            this.tabControlForInfo.TabIndex = 13;
            // 
            // tabPageImageProp
            // 
            this.tabPageImageProp.Controls.Add(this.label3);
            this.tabPageImageProp.Controls.Add(this.numericUpDownChannelNumber);
            this.tabPageImageProp.Controls.Add(this.label4);
            this.tabPageImageProp.Controls.Add(this.numericUpDownFieldNumber);
            this.tabPageImageProp.Location = new System.Drawing.Point(4, 4);
            this.tabPageImageProp.Name = "tabPageImageProp";
            this.tabPageImageProp.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageImageProp.Size = new System.Drawing.Size(144, 248);
            this.tabPageImageProp.TabIndex = 0;
            this.tabPageImageProp.Text = "Images";
            this.tabPageImageProp.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(144, 248);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Plate";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FormForGeneratingScreenFromImages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 359);
            this.Controls.Add(this.tabControlForInfo);
            this.Controls.Add(this.buttonParse);
            this.Controls.Add(this.richTextBoxReport);
            this.Controls.Add(this.buttonInspect);
            this.Controls.Add(this.treeViewForScreenInspection);
            this.Controls.Add(this.textBoxPlateForm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxImageRoot);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonGenerate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(718, 235);
            this.Name = "FormForGeneratingScreenFromImages";
            this.Text = "Generate Screen From Images";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChannelNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFieldNumber)).EndInit();
            this.tabControlForInfo.ResumeLayout(false);
            this.tabPageImageProp.ResumeLayout(false);
            this.tabPageImageProp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxImageRoot;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPlateForm;
        private System.Windows.Forms.NumericUpDown numericUpDownChannelNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownFieldNumber;
        private System.Windows.Forms.TreeView treeViewForScreenInspection;
        private System.Windows.Forms.Button buttonInspect;
        private System.Windows.Forms.RichTextBox richTextBoxReport;
        private System.Windows.Forms.Button buttonParse;
        private System.Windows.Forms.TabControl tabControlForInfo;
        private System.Windows.Forms.TabPage tabPageImageProp;
        private System.Windows.Forms.TabPage tabPage2;
    }
}