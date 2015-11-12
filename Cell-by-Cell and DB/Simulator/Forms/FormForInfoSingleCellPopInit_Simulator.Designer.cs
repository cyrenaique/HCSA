namespace HCSAnalyzer.Simulator.Forms
{
    partial class FormForInfoSingleCellPopInit_Simulator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForInfoSingleCellPopInit_Simulator));
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelForCellNumber = new System.Windows.Forms.Label();
            this.numericUpDownInitialCellNumber = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxCellType = new System.Windows.Forms.ComboBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxCellPos = new System.Windows.Forms.GroupBox();
            this.panelManualPos = new System.Windows.Forms.Panel();
            this.labelZ = new System.Windows.Forms.Label();
            this.numericUpDownManualZ = new System.Windows.Forms.NumericUpDown();
            this.labelY = new System.Windows.Forms.Label();
            this.numericUpDownManualY = new System.Windows.Forms.NumericUpDown();
            this.labelX = new System.Windows.Forms.Label();
            this.numericUpDownManualX = new System.Windows.Forms.NumericUpDown();
            this.radioButtonPosRandom = new System.Windows.Forms.RadioButton();
            this.radioButtonPosManual = new System.Windows.Forms.RadioButton();
            this.contextMenuStripForManual = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.worldCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radioButtonPosWorldCenter = new System.Windows.Forms.RadioButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxForVolume = new System.Windows.Forms.GroupBox();
            this.radioButtonVolumeFixed = new System.Windows.Forms.RadioButton();
            this.radioButtonVolumeRandom = new System.Windows.Forms.RadioButton();
            this.numericUpDownInitialVolumeManual = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBoxDescription = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInitialCellNumber)).BeginInit();
            this.groupBoxCellPos.SuspendLayout();
            this.panelManualPos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownManualZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownManualY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownManualX)).BeginInit();
            this.contextMenuStripForManual.SuspendLayout();
            this.groupBoxForVolume.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInitialVolumeManual)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(315, 358);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelForCellNumber
            // 
            this.labelForCellNumber.AutoSize = true;
            this.labelForCellNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelForCellNumber.Location = new System.Drawing.Point(197, 16);
            this.labelForCellNumber.Name = "labelForCellNumber";
            this.labelForCellNumber.Size = new System.Drawing.Size(64, 13);
            this.labelForCellNumber.TabIndex = 11;
            this.labelForCellNumber.Text = "Cell Number";
            this.toolTip.SetToolTip(this.labelForCellNumber, "Click to tansform this constant into a variable");
            this.labelForCellNumber.Click += new System.EventHandler(this.labelForCellNumber_Click);
            // 
            // numericUpDownInitialCellNumber
            // 
            this.numericUpDownInitialCellNumber.Location = new System.Drawing.Point(284, 13);
            this.numericUpDownInitialCellNumber.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDownInitialCellNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownInitialCellNumber.Name = "numericUpDownInitialCellNumber";
            this.numericUpDownInitialCellNumber.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownInitialCellNumber.TabIndex = 10;
            this.numericUpDownInitialCellNumber.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(197, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Cell Type";
            // 
            // comboBoxCellType
            // 
            this.comboBoxCellType.FormattingEnabled = true;
            this.comboBoxCellType.Items.AddRange(new object[] {
            "Regular",
            "Cancer",
            "Scenecent",
            "Apoptotic",
            "Necrotic"});
            this.comboBoxCellType.Location = new System.Drawing.Point(284, 43);
            this.comboBoxCellType.Name = "comboBoxCellType";
            this.comboBoxCellType.Size = new System.Drawing.Size(100, 21);
            this.comboBoxCellType.TabIndex = 8;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(55, 14);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(116, 20);
            this.textBoxName.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Name";
            // 
            // groupBoxCellPos
            // 
            this.groupBoxCellPos.Controls.Add(this.panelManualPos);
            this.groupBoxCellPos.Controls.Add(this.radioButtonPosRandom);
            this.groupBoxCellPos.Controls.Add(this.radioButtonPosManual);
            this.groupBoxCellPos.Controls.Add(this.radioButtonPosWorldCenter);
            this.groupBoxCellPos.Location = new System.Drawing.Point(188, 74);
            this.groupBoxCellPos.Name = "groupBoxCellPos";
            this.groupBoxCellPos.Size = new System.Drawing.Size(200, 191);
            this.groupBoxCellPos.TabIndex = 13;
            this.groupBoxCellPos.TabStop = false;
            this.groupBoxCellPos.Text = "Position";
            // 
            // panelManualPos
            // 
            this.panelManualPos.Controls.Add(this.labelZ);
            this.panelManualPos.Controls.Add(this.numericUpDownManualZ);
            this.panelManualPos.Controls.Add(this.labelY);
            this.panelManualPos.Controls.Add(this.numericUpDownManualY);
            this.panelManualPos.Controls.Add(this.labelX);
            this.panelManualPos.Controls.Add(this.numericUpDownManualX);
            this.panelManualPos.Enabled = false;
            this.panelManualPos.Location = new System.Drawing.Point(42, 91);
            this.panelManualPos.Name = "panelManualPos";
            this.panelManualPos.Size = new System.Drawing.Size(108, 88);
            this.panelManualPos.TabIndex = 3;
            // 
            // labelZ
            // 
            this.labelZ.AutoSize = true;
            this.labelZ.Location = new System.Drawing.Point(6, 62);
            this.labelZ.Name = "labelZ";
            this.labelZ.Size = new System.Drawing.Size(14, 13);
            this.labelZ.TabIndex = 19;
            this.labelZ.Text = "Z";
            this.toolTip.SetToolTip(this.labelZ, "Click to tansform this constant into a variable");
            this.labelZ.Click += new System.EventHandler(this.labelZ_Click);
            // 
            // numericUpDownManualZ
            // 
            this.numericUpDownManualZ.DecimalPlaces = 1;
            this.numericUpDownManualZ.Location = new System.Drawing.Point(22, 59);
            this.numericUpDownManualZ.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDownManualZ.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.numericUpDownManualZ.Name = "numericUpDownManualZ";
            this.numericUpDownManualZ.Size = new System.Drawing.Size(74, 20);
            this.numericUpDownManualZ.TabIndex = 18;
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(7, 36);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(14, 13);
            this.labelY.TabIndex = 17;
            this.labelY.Text = "Y";
            this.toolTip.SetToolTip(this.labelY, "Click to tansform this constant into a variable");
            this.labelY.Click += new System.EventHandler(this.labelY_Click);
            // 
            // numericUpDownManualY
            // 
            this.numericUpDownManualY.DecimalPlaces = 1;
            this.numericUpDownManualY.Location = new System.Drawing.Point(22, 33);
            this.numericUpDownManualY.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDownManualY.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.numericUpDownManualY.Name = "numericUpDownManualY";
            this.numericUpDownManualY.Size = new System.Drawing.Size(74, 20);
            this.numericUpDownManualY.TabIndex = 16;
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(7, 10);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(14, 13);
            this.labelX.TabIndex = 15;
            this.labelX.Text = "X";
            this.toolTip.SetToolTip(this.labelX, "Click to tansform this constant into a variable");
            this.labelX.Click += new System.EventHandler(this.labelX_Click);
            // 
            // numericUpDownManualX
            // 
            this.numericUpDownManualX.DecimalPlaces = 1;
            this.numericUpDownManualX.Location = new System.Drawing.Point(22, 7);
            this.numericUpDownManualX.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDownManualX.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.numericUpDownManualX.Name = "numericUpDownManualX";
            this.numericUpDownManualX.Size = new System.Drawing.Size(74, 20);
            this.numericUpDownManualX.TabIndex = 14;
            // 
            // radioButtonPosRandom
            // 
            this.radioButtonPosRandom.AutoSize = true;
            this.radioButtonPosRandom.Location = new System.Drawing.Point(12, 44);
            this.radioButtonPosRandom.Name = "radioButtonPosRandom";
            this.radioButtonPosRandom.Size = new System.Drawing.Size(65, 17);
            this.radioButtonPosRandom.TabIndex = 2;
            this.radioButtonPosRandom.Text = "Random";
            this.radioButtonPosRandom.UseVisualStyleBackColor = true;
            this.radioButtonPosRandom.CheckedChanged += new System.EventHandler(this.radioButtonPosRandom_CheckedChanged);
            // 
            // radioButtonPosManual
            // 
            this.radioButtonPosManual.AutoSize = true;
            this.radioButtonPosManual.ContextMenuStrip = this.contextMenuStripForManual;
            this.radioButtonPosManual.Location = new System.Drawing.Point(12, 68);
            this.radioButtonPosManual.Name = "radioButtonPosManual";
            this.radioButtonPosManual.Size = new System.Drawing.Size(88, 17);
            this.radioButtonPosManual.TabIndex = 1;
            this.radioButtonPosManual.Text = "Fixed Manual";
            this.radioButtonPosManual.UseVisualStyleBackColor = true;
            this.radioButtonPosManual.CheckedChanged += new System.EventHandler(this.radioButtonPosManual_CheckedChanged);
            // 
            // contextMenuStripForManual
            // 
            this.contextMenuStripForManual.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.worldCenterToolStripMenuItem,
            this.randomToolStripMenuItem});
            this.contextMenuStripForManual.Name = "contextMenuStripForManual";
            this.contextMenuStripForManual.Size = new System.Drawing.Size(143, 48);
            // 
            // worldCenterToolStripMenuItem
            // 
            this.worldCenterToolStripMenuItem.Name = "worldCenterToolStripMenuItem";
            this.worldCenterToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.worldCenterToolStripMenuItem.Text = "World center";
            this.worldCenterToolStripMenuItem.Click += new System.EventHandler(this.worldCenterToolStripMenuItem_Click);
            // 
            // randomToolStripMenuItem
            // 
            this.randomToolStripMenuItem.Name = "randomToolStripMenuItem";
            this.randomToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.randomToolStripMenuItem.Text = "Random";
            this.randomToolStripMenuItem.Click += new System.EventHandler(this.randomToolStripMenuItem_Click);
            // 
            // radioButtonPosWorldCenter
            // 
            this.radioButtonPosWorldCenter.AutoSize = true;
            this.radioButtonPosWorldCenter.Checked = true;
            this.radioButtonPosWorldCenter.Location = new System.Drawing.Point(12, 21);
            this.radioButtonPosWorldCenter.Name = "radioButtonPosWorldCenter";
            this.radioButtonPosWorldCenter.Size = new System.Drawing.Size(124, 17);
            this.radioButtonPosWorldCenter.TabIndex = 0;
            this.radioButtonPosWorldCenter.TabStop = true;
            this.radioButtonPosWorldCenter.Text = "Current World Center";
            this.radioButtonPosWorldCenter.UseVisualStyleBackColor = true;
            this.radioButtonPosWorldCenter.CheckedChanged += new System.EventHandler(this.radioButtonPosWorldCenter_CheckedChanged);
            // 
            // groupBoxForVolume
            // 
            this.groupBoxForVolume.Controls.Add(this.radioButtonVolumeFixed);
            this.groupBoxForVolume.Controls.Add(this.radioButtonVolumeRandom);
            this.groupBoxForVolume.Controls.Add(this.numericUpDownInitialVolumeManual);
            this.groupBoxForVolume.Location = new System.Drawing.Point(188, 271);
            this.groupBoxForVolume.Name = "groupBoxForVolume";
            this.groupBoxForVolume.Size = new System.Drawing.Size(200, 81);
            this.groupBoxForVolume.TabIndex = 14;
            this.groupBoxForVolume.TabStop = false;
            this.groupBoxForVolume.Text = "Initial Volume";
            // 
            // radioButtonVolumeFixed
            // 
            this.radioButtonVolumeFixed.AutoSize = true;
            this.radioButtonVolumeFixed.Checked = true;
            this.radioButtonVolumeFixed.Location = new System.Drawing.Point(31, 47);
            this.radioButtonVolumeFixed.Name = "radioButtonVolumeFixed";
            this.radioButtonVolumeFixed.Size = new System.Drawing.Size(50, 17);
            this.radioButtonVolumeFixed.TabIndex = 16;
            this.radioButtonVolumeFixed.TabStop = true;
            this.radioButtonVolumeFixed.Text = "Fixed";
            this.radioButtonVolumeFixed.UseVisualStyleBackColor = true;
            this.radioButtonVolumeFixed.CheckedChanged += new System.EventHandler(this.radioButtonVolumeFixed_CheckedChanged);
            this.radioButtonVolumeFixed.MouseDown += new System.Windows.Forms.MouseEventHandler(this.radioButtonVolumeFixed_MouseDown);
            // 
            // radioButtonVolumeRandom
            // 
            this.radioButtonVolumeRandom.AutoSize = true;
            this.radioButtonVolumeRandom.Location = new System.Drawing.Point(31, 22);
            this.radioButtonVolumeRandom.Name = "radioButtonVolumeRandom";
            this.radioButtonVolumeRandom.Size = new System.Drawing.Size(65, 17);
            this.radioButtonVolumeRandom.TabIndex = 15;
            this.radioButtonVolumeRandom.Text = "Random";
            this.radioButtonVolumeRandom.UseVisualStyleBackColor = true;
            this.radioButtonVolumeRandom.CheckedChanged += new System.EventHandler(this.radioButtonVolumeRandom_CheckedChanged);
            this.radioButtonVolumeRandom.MouseClick += new System.Windows.Forms.MouseEventHandler(this.radioButtonVolumeRandom_MouseClick);
            // 
            // numericUpDownInitialVolumeManual
            // 
            this.numericUpDownInitialVolumeManual.DecimalPlaces = 1;
            this.numericUpDownInitialVolumeManual.Location = new System.Drawing.Point(87, 47);
            this.numericUpDownInitialVolumeManual.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDownInitialVolumeManual.Name = "numericUpDownInitialVolumeManual";
            this.numericUpDownInitialVolumeManual.Size = new System.Drawing.Size(84, 20);
            this.numericUpDownInitialVolumeManual.TabIndex = 14;
            this.numericUpDownInitialVolumeManual.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Description";
            // 
            // richTextBoxDescription
            // 
            this.richTextBoxDescription.Location = new System.Drawing.Point(17, 74);
            this.richTextBoxDescription.Name = "richTextBoxDescription";
            this.richTextBoxDescription.Size = new System.Drawing.Size(154, 277);
            this.richTextBoxDescription.TabIndex = 16;
            this.richTextBoxDescription.Text = "";
            // 
            // FormForInfoSingleCellPopInit_Simulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(402, 388);
            this.Controls.Add(this.richTextBoxDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBoxForVolume);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.groupBoxCellPos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelForCellNumber);
            this.Controls.Add(this.numericUpDownInitialCellNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxCellType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormForInfoSingleCellPopInit_Simulator";
            this.Text = "Cell population initialization";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInitialCellNumber)).EndInit();
            this.groupBoxCellPos.ResumeLayout(false);
            this.groupBoxCellPos.PerformLayout();
            this.panelManualPos.ResumeLayout(false);
            this.panelManualPos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownManualZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownManualY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownManualX)).EndInit();
            this.contextMenuStripForManual.ResumeLayout(false);
            this.groupBoxForVolume.ResumeLayout(false);
            this.groupBoxForVolume.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInitialVolumeManual)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelForCellNumber;
        private System.Windows.Forms.NumericUpDown numericUpDownInitialCellNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxCellType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.GroupBox groupBoxCellPos;
        private System.Windows.Forms.Panel panelManualPos;
        private System.Windows.Forms.Label labelZ;
        private System.Windows.Forms.NumericUpDown numericUpDownManualZ;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.NumericUpDown numericUpDownManualY;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.NumericUpDown numericUpDownManualX;
        private System.Windows.Forms.RadioButton radioButtonPosRandom;
        private System.Windows.Forms.RadioButton radioButtonPosManual;
        private System.Windows.Forms.RadioButton radioButtonPosWorldCenter;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForManual;
        private System.Windows.Forms.ToolStripMenuItem worldCenterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox groupBoxForVolume;
        private System.Windows.Forms.NumericUpDown numericUpDownInitialVolumeManual;
        private System.Windows.Forms.RadioButton radioButtonVolumeFixed;
        private System.Windows.Forms.RadioButton radioButtonVolumeRandom;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.RichTextBox richTextBoxDescription;
    }
}