namespace HCSAnalyzer.Forms
{
    partial class FormForManualClustering
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForManualClustering));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.panelMainFilterDesc = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageFilterDesc = new System.Windows.Forms.TabPage();
            this.tabPageFitlersProperties = new System.Windows.Forms.TabPage();
            this.panelMainFilterProp = new System.Windows.Forms.Panel();
            this.tabPageWellClasses = new System.Windows.Forms.TabPage();
            this.TabPagePlates = new System.Windows.Forms.TabPage();
            this.tabPageHits = new System.Windows.Forms.TabPage();
            this.checkBoxIsRejectedClass = new System.Windows.Forms.CheckBox();
            this.checkBoxTransferToHitList = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxHitClass = new System.Windows.Forms.GroupBox();
            this.panelForHitClass = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelForNonHitClass = new System.Windows.Forms.Panel();
            this.tabControl.SuspendLayout();
            this.tabPageFilterDesc.SuspendLayout();
            this.tabPageFitlersProperties.SuspendLayout();
            this.tabPageHits.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxHitClass.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(501, 329);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(85, 27);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Apply";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonAdd.FlatAppearance.BorderSize = 2;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.Location = new System.Drawing.Point(8, 331);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(34, 31);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "+";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemove.Enabled = false;
            this.buttonRemove.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonRemove.FlatAppearance.BorderSize = 2;
            this.buttonRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRemove.Location = new System.Drawing.Point(48, 331);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(34, 31);
            this.buttonRemove.TabIndex = 2;
            this.buttonRemove.Text = "-";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // panelMainFilterDesc
            // 
            this.panelMainFilterDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMainFilterDesc.AutoScroll = true;
            this.panelMainFilterDesc.Location = new System.Drawing.Point(6, 6);
            this.panelMainFilterDesc.Name = "panelMainFilterDesc";
            this.panelMainFilterDesc.Size = new System.Drawing.Size(458, 277);
            this.panelMainFilterDesc.TabIndex = 3;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageFilterDesc);
            this.tabControl.Controls.Add(this.tabPageFitlersProperties);
            this.tabControl.Controls.Add(this.tabPageWellClasses);
            this.tabControl.Controls.Add(this.TabPagePlates);
            this.tabControl.Controls.Add(this.tabPageHits);
            this.tabControl.Location = new System.Drawing.Point(8, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(578, 313);
            this.tabControl.TabIndex = 4;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageFilterDesc
            // 
            this.tabPageFilterDesc.Controls.Add(this.panelMainFilterDesc);
            this.tabPageFilterDesc.Location = new System.Drawing.Point(4, 22);
            this.tabPageFilterDesc.Name = "tabPageFilterDesc";
            this.tabPageFilterDesc.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFilterDesc.Size = new System.Drawing.Size(570, 287);
            this.tabPageFilterDesc.TabIndex = 0;
            this.tabPageFilterDesc.Text = "Filters (Descriptors)";
            this.tabPageFilterDesc.ToolTipText = "Filter wells based on descriptor values";
            this.tabPageFilterDesc.UseVisualStyleBackColor = true;
            // 
            // tabPageFitlersProperties
            // 
            this.tabPageFitlersProperties.Controls.Add(this.panelMainFilterProp);
            this.tabPageFitlersProperties.Location = new System.Drawing.Point(4, 22);
            this.tabPageFitlersProperties.Name = "tabPageFitlersProperties";
            this.tabPageFitlersProperties.Size = new System.Drawing.Size(570, 287);
            this.tabPageFitlersProperties.TabIndex = 4;
            this.tabPageFitlersProperties.Text = "Filters (Properties)";
            this.tabPageFitlersProperties.UseVisualStyleBackColor = true;
            // 
            // panelMainFilterProp
            // 
            this.panelMainFilterProp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMainFilterProp.AutoScroll = true;
            this.panelMainFilterProp.Location = new System.Drawing.Point(6, 5);
            this.panelMainFilterProp.Name = "panelMainFilterProp";
            this.panelMainFilterProp.Size = new System.Drawing.Size(558, 277);
            this.panelMainFilterProp.TabIndex = 4;
            // 
            // tabPageWellClasses
            // 
            this.tabPageWellClasses.AutoScroll = true;
            this.tabPageWellClasses.Location = new System.Drawing.Point(4, 22);
            this.tabPageWellClasses.Name = "tabPageWellClasses";
            this.tabPageWellClasses.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWellClasses.Size = new System.Drawing.Size(570, 287);
            this.tabPageWellClasses.TabIndex = 1;
            this.tabPageWellClasses.Text = "Well Classes";
            this.tabPageWellClasses.UseVisualStyleBackColor = true;
            // 
            // TabPagePlates
            // 
            this.TabPagePlates.AutoScroll = true;
            this.TabPagePlates.Location = new System.Drawing.Point(4, 22);
            this.TabPagePlates.Name = "TabPagePlates";
            this.TabPagePlates.Padding = new System.Windows.Forms.Padding(3);
            this.TabPagePlates.Size = new System.Drawing.Size(570, 287);
            this.TabPagePlates.TabIndex = 2;
            this.TabPagePlates.Text = "Plates";
            this.TabPagePlates.UseVisualStyleBackColor = true;
            // 
            // tabPageHits
            // 
            this.tabPageHits.Controls.Add(this.checkBoxIsRejectedClass);
            this.tabPageHits.Controls.Add(this.checkBoxTransferToHitList);
            this.tabPageHits.Controls.Add(this.splitContainer1);
            this.tabPageHits.Location = new System.Drawing.Point(4, 22);
            this.tabPageHits.Name = "tabPageHits";
            this.tabPageHits.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHits.Size = new System.Drawing.Size(570, 287);
            this.tabPageHits.TabIndex = 3;
            this.tabPageHits.Text = "Hits Class";
            this.tabPageHits.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsRejectedClass
            // 
            this.checkBoxIsRejectedClass.AutoSize = true;
            this.checkBoxIsRejectedClass.Location = new System.Drawing.Point(227, 6);
            this.checkBoxIsRejectedClass.Name = "checkBoxIsRejectedClass";
            this.checkBoxIsRejectedClass.Size = new System.Drawing.Size(97, 17);
            this.checkBoxIsRejectedClass.TabIndex = 4;
            this.checkBoxIsRejectedClass.Text = "Rejected Class";
            this.checkBoxIsRejectedClass.UseVisualStyleBackColor = true;
            this.checkBoxIsRejectedClass.CheckedChanged += new System.EventHandler(this.checkBoxIsRejectedClass_CheckedChanged);
            // 
            // checkBoxTransferToHitList
            // 
            this.checkBoxTransferToHitList.AutoSize = true;
            this.checkBoxTransferToHitList.Location = new System.Drawing.Point(440, 6);
            this.checkBoxTransferToHitList.Name = "checkBoxTransferToHitList";
            this.checkBoxTransferToHitList.Size = new System.Drawing.Size(125, 17);
            this.checkBoxTransferToHitList.TabIndex = 3;
            this.checkBoxTransferToHitList.Text = "Transfer to Wells List";
            this.checkBoxTransferToHitList.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxHitClass);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(431, 280);
            this.splitContainer1.SplitterDistance = 207;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBoxHitClass
            // 
            this.groupBoxHitClass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxHitClass.Controls.Add(this.panelForHitClass);
            this.groupBoxHitClass.Location = new System.Drawing.Point(3, 3);
            this.groupBoxHitClass.Name = "groupBoxHitClass";
            this.groupBoxHitClass.Size = new System.Drawing.Size(201, 274);
            this.groupBoxHitClass.TabIndex = 0;
            this.groupBoxHitClass.TabStop = false;
            this.groupBoxHitClass.Text = "Hit Class";
            // 
            // panelForHitClass
            // 
            this.panelForHitClass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForHitClass.Location = new System.Drawing.Point(6, 19);
            this.panelForHitClass.Name = "panelForHitClass";
            this.panelForHitClass.Size = new System.Drawing.Size(189, 249);
            this.panelForHitClass.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panelForNonHitClass);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(214, 274);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "                              ";
            // 
            // panelForNonHitClass
            // 
            this.panelForNonHitClass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForNonHitClass.Enabled = false;
            this.panelForNonHitClass.Location = new System.Drawing.Point(6, 19);
            this.panelForNonHitClass.Name = "panelForNonHitClass";
            this.panelForNonHitClass.Size = new System.Drawing.Size(202, 249);
            this.panelForNonHitClass.TabIndex = 0;
            // 
            // FormForManualClustering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 360);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(340, 233);
            this.Name = "FormForManualClustering";
            this.Text = "Manual Hits Selection";
            this.tabControl.ResumeLayout(false);
            this.tabPageFilterDesc.ResumeLayout(false);
            this.tabPageFitlersProperties.ResumeLayout(false);
            this.tabPageHits.ResumeLayout(false);
            this.tabPageHits.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxHitClass.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Panel panelMainFilterDesc;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageFilterDesc;
        public System.Windows.Forms.TabPage tabPageWellClasses;
        public System.Windows.Forms.TabPage TabPagePlates;
        public System.Windows.Forms.TabPage tabPageHits;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBoxHitClass;
        public System.Windows.Forms.Panel panelForHitClass;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Panel panelForNonHitClass;
        private System.Windows.Forms.TabPage tabPageFitlersProperties;
        private System.Windows.Forms.Panel panelMainFilterProp;
        public System.Windows.Forms.CheckBox checkBoxTransferToHitList;
        private System.Windows.Forms.CheckBox checkBoxIsRejectedClass;
    }
}