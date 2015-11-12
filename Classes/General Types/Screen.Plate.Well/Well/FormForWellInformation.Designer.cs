namespace HCSAnalyzer.Forms
{
    partial class FormForWellInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForWellInformation));
            this.chartForFormWell = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonUpdateAndClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLocusID = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxConcentration = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageDescriptors = new System.Windows.Forms.TabPage();
            this.panelForDescValues = new System.Windows.Forms.Panel();
            this.tabPageDesc = new System.Windows.Forms.TabPage();
            this.richTextBoxDescription = new System.Windows.Forms.RichTextBox();
            this.tabPageHisto = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.chartForFormWell)).BeginInit();
            this.tabControlMain.SuspendLayout();
            this.tabPageDescriptors.SuspendLayout();
            this.tabPageDesc.SuspendLayout();
            this.tabPageHisto.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartForFormWell
            // 
            this.chartForFormWell.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chartForFormWell.Location = new System.Drawing.Point(6, 6);
            this.chartForFormWell.Name = "chartForFormWell";
            this.chartForFormWell.Size = new System.Drawing.Size(508, 410);
            this.chartForFormWell.TabIndex = 1;
            this.chartForFormWell.Text = "chart1";
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInfo.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxInfo.Location = new System.Drawing.Point(91, 493);
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.Size = new System.Drawing.Size(449, 22);
            this.textBoxInfo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 498);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Info";
            // 
            // buttonUpdateAndClose
            // 
            this.buttonUpdateAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdateAndClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonUpdateAndClose.Location = new System.Drawing.Point(408, 519);
            this.buttonUpdateAndClose.Name = "buttonUpdateAndClose";
            this.buttonUpdateAndClose.Size = new System.Drawing.Size(132, 50);
            this.buttonUpdateAndClose.TabIndex = 4;
            this.buttonUpdateAndClose.Text = "Update";
            this.buttonUpdateAndClose.UseVisualStyleBackColor = true;
            this.buttonUpdateAndClose.Click += new System.EventHandler(this.buttonUpdateAndClose_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 552);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Locus ID";
            // 
            // textBoxLocusID
            // 
            this.textBoxLocusID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLocusID.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLocusID.Location = new System.Drawing.Point(91, 547);
            this.textBoxLocusID.Multiline = true;
            this.textBoxLocusID.Name = "textBoxLocusID";
            this.textBoxLocusID.ReadOnly = true;
            this.textBoxLocusID.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxLocusID.Size = new System.Drawing.Size(307, 20);
            this.textBoxLocusID.TabIndex = 5;
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxName.Location = new System.Drawing.Point(91, 466);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(449, 22);
            this.textBoxName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 471);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Cpd Name";
            // 
            // textBoxConcentration
            // 
            this.textBoxConcentration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConcentration.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxConcentration.Location = new System.Drawing.Point(91, 520);
            this.textBoxConcentration.Name = "textBoxConcentration";
            this.textBoxConcentration.Size = new System.Drawing.Size(307, 22);
            this.textBoxConcentration.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 525);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Concentration";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageDescriptors);
            this.tabControlMain.Controls.Add(this.tabPageDesc);
            this.tabControlMain.Controls.Add(this.tabPageHisto);
            this.tabControlMain.Location = new System.Drawing.Point(12, 10);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(528, 448);
            this.tabControlMain.TabIndex = 9;
            // 
            // tabPageDescriptors
            // 
            this.tabPageDescriptors.Controls.Add(this.panelForDescValues);
            this.tabPageDescriptors.Location = new System.Drawing.Point(4, 22);
            this.tabPageDescriptors.Name = "tabPageDescriptors";
            this.tabPageDescriptors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDescriptors.Size = new System.Drawing.Size(520, 422);
            this.tabPageDescriptors.TabIndex = 3;
            this.tabPageDescriptors.Text = "Descriptors";
            this.tabPageDescriptors.UseVisualStyleBackColor = true;
            // 
            // panelForDescValues
            // 
            this.panelForDescValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForDescValues.Location = new System.Drawing.Point(3, 6);
            this.panelForDescValues.Name = "panelForDescValues";
            this.panelForDescValues.Size = new System.Drawing.Size(514, 413);
            this.panelForDescValues.TabIndex = 0;
            // 
            // tabPageDesc
            // 
            this.tabPageDesc.Controls.Add(this.richTextBoxDescription);
            this.tabPageDesc.Location = new System.Drawing.Point(4, 22);
            this.tabPageDesc.Name = "tabPageDesc";
            this.tabPageDesc.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDesc.Size = new System.Drawing.Size(520, 422);
            this.tabPageDesc.TabIndex = 1;
            this.tabPageDesc.Text = "Properties";
            this.tabPageDesc.UseVisualStyleBackColor = true;
            // 
            // richTextBoxDescription
            // 
            this.richTextBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxDescription.Location = new System.Drawing.Point(6, 6);
            this.richTextBoxDescription.Name = "richTextBoxDescription";
            this.richTextBoxDescription.Size = new System.Drawing.Size(508, 410);
            this.richTextBoxDescription.TabIndex = 0;
            this.richTextBoxDescription.Text = "";
            // 
            // tabPageHisto
            // 
            this.tabPageHisto.Controls.Add(this.chartForFormWell);
            this.tabPageHisto.Location = new System.Drawing.Point(4, 22);
            this.tabPageHisto.Name = "tabPageHisto";
            this.tabPageHisto.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHisto.Size = new System.Drawing.Size(520, 422);
            this.tabPageHisto.TabIndex = 0;
            this.tabPageHisto.Text = "Histogram";
            this.tabPageHisto.UseVisualStyleBackColor = true;
            // 
            // FormForWellInformation
            // 
            this.AcceptButton = this.buttonUpdateAndClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 575);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxLocusID);
            this.Controls.Add(this.buttonUpdateAndClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxConcentration);
            this.Controls.Add(this.textBoxInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(299, 279);
            this.Name = "FormForWellInformation";
            this.Text = "Info";
            ((System.ComponentModel.ISupportInitialize)(this.chartForFormWell)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageDescriptors.ResumeLayout(false);
            this.tabPageDesc.ResumeLayout(false);
            this.tabPageHisto.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart chartForFormWell;
        public System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonUpdateAndClose;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxLocusID;
        public System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textBoxConcentration;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageHisto;
        private System.Windows.Forms.TabPage tabPageDesc;
        public System.Windows.Forms.RichTextBox richTextBoxDescription;
        private System.Windows.Forms.TabPage tabPageDescriptors;
        public System.Windows.Forms.Panel panelForDescValues;
    }
}