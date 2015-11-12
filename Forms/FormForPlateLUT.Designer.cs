namespace HCSAnalyzer.Forms
{
    partial class FormForPlateLUT
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
            this.components = new System.ComponentModel.Container();
            this.panelForPlateLUT = new System.Windows.Forms.Panel();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.currentPlateOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.selectLUTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxLUT = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.activeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxForLUT = new System.Windows.Forms.PictureBox();
            this.numericUpDownGeneralMin = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownGeneralMax = new System.Windows.Forms.NumericUpDown();
            this.trackBarForPlateLUTMin = new System.Windows.Forms.TrackBar();
            this.labelMax = new System.Windows.Forms.Label();
            this.trackBarForPlateLUTMax = new System.Windows.Forms.TrackBar();
            this.labelMin = new System.Windows.Forms.Label();
            this.dataTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelForPlateLUT.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForLUT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGeneralMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGeneralMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarForPlateLUTMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarForPlateLUTMax)).BeginInit();
            this.SuspendLayout();
            // 
            // panelForPlateLUT
            // 
            this.panelForPlateLUT.ContextMenuStrip = this.contextMenuStrip;
            this.panelForPlateLUT.Controls.Add(this.pictureBoxForLUT);
            this.panelForPlateLUT.Controls.Add(this.numericUpDownGeneralMin);
            this.panelForPlateLUT.Controls.Add(this.numericUpDownGeneralMax);
            this.panelForPlateLUT.Controls.Add(this.trackBarForPlateLUTMin);
            this.panelForPlateLUT.Controls.Add(this.labelMax);
            this.panelForPlateLUT.Controls.Add(this.trackBarForPlateLUTMax);
            this.panelForPlateLUT.Controls.Add(this.labelMin);
            this.panelForPlateLUT.Location = new System.Drawing.Point(0, 0);
            this.panelForPlateLUT.Name = "panelForPlateLUT";
            this.panelForPlateLUT.Size = new System.Drawing.Size(118, 239);
            this.panelForPlateLUT.TabIndex = 37;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentPlateOnlyToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.toolStripSeparator2,
            this.selectLUTToolStripMenuItem,
            this.toolStripSeparator1,
            this.activeToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(172, 126);
            // 
            // currentPlateOnlyToolStripMenuItem
            // 
            this.currentPlateOnlyToolStripMenuItem.Checked = true;
            this.currentPlateOnlyToolStripMenuItem.CheckOnClick = true;
            this.currentPlateOnlyToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.currentPlateOnlyToolStripMenuItem.Name = "currentPlateOnlyToolStripMenuItem";
            this.currentPlateOnlyToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.currentPlateOnlyToolStripMenuItem.Text = "Current Plate Only";
            this.currentPlateOnlyToolStripMenuItem.Click += new System.EventHandler(this.currentPlateOnlyToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(168, 6);
            // 
            // selectLUTToolStripMenuItem
            // 
            this.selectLUTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxLUT,
            this.dataTableToolStripMenuItem});
            this.selectLUTToolStripMenuItem.Name = "selectLUTToolStripMenuItem";
            this.selectLUTToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.selectLUTToolStripMenuItem.Text = "Current LUT";
            // 
            // toolStripComboBoxLUT
            // 
            this.toolStripComboBoxLUT.Items.AddRange(new object[] {
            "JET",
            "HSV",
            "FIRE",
            "GREEN_TO_RED",
            "HOT",
            "COOL",
            "SPRING",
            "SUMMER",
            "AUTUMN",
            "WINTER",
            "BONE",
            "COPPER",
            "LINEAR",
            "GD"});
            this.toolStripComboBoxLUT.Name = "toolStripComboBoxLUT";
            this.toolStripComboBoxLUT.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxLUT.Text = "JET";
            this.toolStripComboBoxLUT.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxLUT_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(168, 6);
            // 
            // activeToolStripMenuItem
            // 
            this.activeToolStripMenuItem.Checked = true;
            this.activeToolStripMenuItem.CheckOnClick = true;
            this.activeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.activeToolStripMenuItem.Name = "activeToolStripMenuItem";
            this.activeToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.activeToolStripMenuItem.Text = "Active";
            this.activeToolStripMenuItem.Click += new System.EventHandler(this.activeToolStripMenuItem_Click);
            // 
            // pictureBoxForLUT
            // 
            this.pictureBoxForLUT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxForLUT.ContextMenuStrip = this.contextMenuStrip;
            this.pictureBoxForLUT.Location = new System.Drawing.Point(49, 52);
            this.pictureBoxForLUT.Name = "pictureBoxForLUT";
            this.pictureBoxForLUT.Size = new System.Drawing.Size(21, 138);
            this.pictureBoxForLUT.TabIndex = 38;
            this.pictureBoxForLUT.TabStop = false;
            // 
            // numericUpDownGeneralMin
            // 
            this.numericUpDownGeneralMin.DecimalPlaces = 3;
            this.numericUpDownGeneralMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownGeneralMin.Location = new System.Drawing.Point(27, 215);
            this.numericUpDownGeneralMin.Maximum = new decimal(new int[] {
            -1486618624,
            232830643,
            0,
            0});
            this.numericUpDownGeneralMin.Minimum = new decimal(new int[] {
            -1486618624,
            232830643,
            0,
            -2147483648});
            this.numericUpDownGeneralMin.Name = "numericUpDownGeneralMin";
            this.numericUpDownGeneralMin.Size = new System.Drawing.Size(72, 18);
            this.numericUpDownGeneralMin.TabIndex = 37;
            this.numericUpDownGeneralMin.ValueChanged += new System.EventHandler(this.numericUpDownGeneralMin_ValueChanged_1);
            // 
            // numericUpDownGeneralMax
            // 
            this.numericUpDownGeneralMax.DecimalPlaces = 3;
            this.numericUpDownGeneralMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownGeneralMax.Location = new System.Drawing.Point(27, 8);
            this.numericUpDownGeneralMax.Maximum = new decimal(new int[] {
            -1486618624,
            232830643,
            0,
            0});
            this.numericUpDownGeneralMax.Minimum = new decimal(new int[] {
            -1486618624,
            232830643,
            0,
            -2147483648});
            this.numericUpDownGeneralMax.Name = "numericUpDownGeneralMax";
            this.numericUpDownGeneralMax.Size = new System.Drawing.Size(72, 18);
            this.numericUpDownGeneralMax.TabIndex = 36;
            this.numericUpDownGeneralMax.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownGeneralMax.ValueChanged += new System.EventHandler(this.numericUpDownGeneralMax_ValueChanged);
            // 
            // trackBarForPlateLUTMin
            // 
            this.trackBarForPlateLUTMin.AutoSize = false;
            this.trackBarForPlateLUTMin.ContextMenuStrip = this.contextMenuStrip;
            this.trackBarForPlateLUTMin.Location = new System.Drawing.Point(10, 40);
            this.trackBarForPlateLUTMin.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarForPlateLUTMin.Maximum = 100;
            this.trackBarForPlateLUTMin.Name = "trackBarForPlateLUTMin";
            this.trackBarForPlateLUTMin.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarForPlateLUTMin.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBarForPlateLUTMin.Size = new System.Drawing.Size(30, 163);
            this.trackBarForPlateLUTMin.TabIndex = 35;
            this.trackBarForPlateLUTMin.ValueChanged += new System.EventHandler(this.trackBarForPlateLUTMin_ValueChanged);
            // 
            // labelMax
            // 
            this.labelMax.AutoSize = true;
            this.labelMax.ContextMenuStrip = this.contextMenuStrip;
            this.labelMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMax.Location = new System.Drawing.Point(71, 31);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(20, 12);
            this.labelMax.TabIndex = 10;
            this.labelMax.Text = "###";
            this.labelMax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trackBarForPlateLUTMax
            // 
            this.trackBarForPlateLUTMax.AutoSize = false;
            this.trackBarForPlateLUTMax.ContextMenuStrip = this.contextMenuStrip;
            this.trackBarForPlateLUTMax.Location = new System.Drawing.Point(78, 40);
            this.trackBarForPlateLUTMax.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarForPlateLUTMax.Maximum = 100;
            this.trackBarForPlateLUTMax.Name = "trackBarForPlateLUTMax";
            this.trackBarForPlateLUTMax.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarForPlateLUTMax.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBarForPlateLUTMax.Size = new System.Drawing.Size(30, 162);
            this.trackBarForPlateLUTMax.TabIndex = 34;
            this.trackBarForPlateLUTMax.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarForPlateLUTMax.Value = 100;
            this.trackBarForPlateLUTMax.ValueChanged += new System.EventHandler(this.trackBarForPlateLUTMax_ValueChanged);
            // 
            // labelMin
            // 
            this.labelMin.AutoSize = true;
            this.labelMin.ContextMenuStrip = this.contextMenuStrip;
            this.labelMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMin.Location = new System.Drawing.Point(22, 200);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(20, 12);
            this.labelMin.TabIndex = 11;
            this.labelMin.Text = "###";
            this.labelMin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataTableToolStripMenuItem
            // 
            this.dataTableToolStripMenuItem.Name = "dataTableToolStripMenuItem";
            this.dataTableToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.dataTableToolStripMenuItem.Text = "Data Table";
            this.dataTableToolStripMenuItem.Click += new System.EventHandler(this.dataTableToolStripMenuItem_Click);
            // 
            // FormForPlateLUT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 369);
            this.Controls.Add(this.panelForPlateLUT);
            this.Name = "FormForPlateLUT";
            this.Text = "FormForPlateLUT";
            this.panelForPlateLUT.ResumeLayout(false);
            this.panelForPlateLUT.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForLUT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGeneralMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGeneralMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarForPlateLUTMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarForPlateLUTMax)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelForPlateLUT;
        public System.Windows.Forms.NumericUpDown numericUpDownGeneralMin;
        public System.Windows.Forms.NumericUpDown numericUpDownGeneralMax;
        private System.Windows.Forms.TrackBar trackBarForPlateLUTMin;
        public System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.TrackBar trackBarForPlateLUTMax;
        public System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currentPlateOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem activeToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBoxForLUT;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem selectLUTToolStripMenuItem;
        public System.Windows.Forms.ToolStripComboBox toolStripComboBoxLUT;
        private System.Windows.Forms.ToolStripMenuItem dataTableToolStripMenuItem;
    }
}