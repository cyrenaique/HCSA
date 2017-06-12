namespace IM3_Plugin3
{
    partial class FormForVolumeDetection
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.numericUpDownSmoothIterations = new System.Windows.Forms.NumericUpDown();
            this.checkBoxVolumeSmooth = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelForRegionBasedInitialization = new System.Windows.Forms.Panel();
            this.panelRegionDetectionExisting = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.radioButtonVolumeDetectionExisting = new System.Windows.Forms.RadioButton();
            this.panelRegionBasedDetectionNew = new System.Windows.Forms.Panel();
            this.numericUpDownRegionBasedMinArea = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.numericUpDownRegionBasedMaxArea = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.radioButtonVolumeDetectionNew = new System.Windows.Forms.RadioButton();
            this.panelSeedBased = new System.Windows.Forms.Panel();
            this.comboBoxExistingSpotsDetected = new System.Windows.Forms.ComboBox();
            this.radioButtonSeedInitialization = new System.Windows.Forms.RadioButton();
            this.radioButtonRegionsBased = new System.Windows.Forms.RadioButton();
            this.checkBoxIsRegionGrowing = new System.Windows.Forms.CheckBox();
            this.groupBoxRegionGrowing = new System.Windows.Forms.GroupBox();
            this.panelForMerging = new System.Windows.Forms.Panel();
            this.trackBarMergingStrength = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownChannelForRegionGrowing = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownIntensityForRegionGrowing = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxIsConvergence = new System.Windows.Forms.CheckBox();
            this.numericUpDownIterationNumber = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownVolumeMinVol = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.numericUpDownVolumeMaxVol = new System.Windows.Forms.NumericUpDown();
            this.groupBoxPostProcessing = new System.Windows.Forms.GroupBox();
            this.checkBoxIsBorderKill = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownIntensityThreshold = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSmoothIterations)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panelForRegionBasedInitialization.SuspendLayout();
            this.panelRegionDetectionExisting.SuspendLayout();
            this.panelRegionBasedDetectionNew.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionBasedMinArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionBasedMaxArea)).BeginInit();
            this.panelSeedBased.SuspendLayout();
            this.groupBoxRegionGrowing.SuspendLayout();
            this.panelForMerging.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMergingStrength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChannelForRegionGrowing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntensityForRegionGrowing)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIterationNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolumeMinVol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolumeMaxVol)).BeginInit();
            this.groupBoxPostProcessing.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntensityThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(513, 528);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(115, 25);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // numericUpDownSmoothIterations
            // 
            this.numericUpDownSmoothIterations.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownSmoothIterations.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownSmoothIterations.Location = new System.Drawing.Point(501, 31);
            this.numericUpDownSmoothIterations.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownSmoothIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSmoothIterations.Name = "numericUpDownSmoothIterations";
            this.numericUpDownSmoothIterations.Size = new System.Drawing.Size(78, 20);
            this.numericUpDownSmoothIterations.TabIndex = 6;
            this.numericUpDownSmoothIterations.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // checkBoxVolumeSmooth
            // 
            this.checkBoxVolumeSmooth.AutoSize = true;
            this.checkBoxVolumeSmooth.Checked = true;
            this.checkBoxVolumeSmooth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxVolumeSmooth.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxVolumeSmooth.Location = new System.Drawing.Point(289, 33);
            this.checkBoxVolumeSmooth.Name = "checkBoxVolumeSmooth";
            this.checkBoxVolumeSmooth.Size = new System.Drawing.Size(103, 17);
            this.checkBoxVolumeSmooth.TabIndex = 7;
            this.checkBoxVolumeSmooth.Text = "Mesh smoothing";
            this.checkBoxVolumeSmooth.UseVisualStyleBackColor = true;
            this.checkBoxVolumeSmooth.CheckedChanged += new System.EventHandler(this.checkBoxVolumeSmooth_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panelForRegionBasedInitialization);
            this.groupBox2.Controls.Add(this.panelSeedBased);
            this.groupBox2.Controls.Add(this.radioButtonSeedInitialization);
            this.groupBox2.Controls.Add(this.radioButtonRegionsBased);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Location = new System.Drawing.Point(12, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(303, 342);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Initialization";
            // 
            // panelForRegionBasedInitialization
            // 
            this.panelForRegionBasedInitialization.Controls.Add(this.panelRegionDetectionExisting);
            this.panelForRegionBasedInitialization.Controls.Add(this.radioButtonVolumeDetectionExisting);
            this.panelForRegionBasedInitialization.Controls.Add(this.panelRegionBasedDetectionNew);
            this.panelForRegionBasedInitialization.Controls.Add(this.radioButtonVolumeDetectionNew);
            this.panelForRegionBasedInitialization.Enabled = false;
            this.panelForRegionBasedInitialization.Location = new System.Drawing.Point(6, 153);
            this.panelForRegionBasedInitialization.Name = "panelForRegionBasedInitialization";
            this.panelForRegionBasedInitialization.Size = new System.Drawing.Size(286, 175);
            this.panelForRegionBasedInitialization.TabIndex = 47;
            // 
            // panelRegionDetectionExisting
            // 
            this.panelRegionDetectionExisting.Controls.Add(this.comboBox1);
            this.panelRegionDetectionExisting.Enabled = false;
            this.panelRegionDetectionExisting.Location = new System.Drawing.Point(94, 106);
            this.panelRegionDetectionExisting.Name = "panelRegionDetectionExisting";
            this.panelRegionDetectionExisting.Size = new System.Drawing.Size(188, 50);
            this.panelRegionDetectionExisting.TabIndex = 47;
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.comboBox1.Location = new System.Drawing.Point(13, 15);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(159, 20);
            this.comboBox1.TabIndex = 30;
            // 
            // radioButtonVolumeDetectionExisting
            // 
            this.radioButtonVolumeDetectionExisting.AutoSize = true;
            this.radioButtonVolumeDetectionExisting.Location = new System.Drawing.Point(22, 124);
            this.radioButtonVolumeDetectionExisting.Name = "radioButtonVolumeDetectionExisting";
            this.radioButtonVolumeDetectionExisting.Size = new System.Drawing.Size(66, 16);
            this.radioButtonVolumeDetectionExisting.TabIndex = 42;
            this.radioButtonVolumeDetectionExisting.Text = "Existing";
            this.radioButtonVolumeDetectionExisting.UseVisualStyleBackColor = true;
            this.radioButtonVolumeDetectionExisting.CheckedChanged += new System.EventHandler(this.radioButtonVolumeDetectionExisting_CheckedChanged);
            // 
            // panelRegionBasedDetectionNew
            // 
            this.panelRegionBasedDetectionNew.Controls.Add(this.numericUpDownRegionBasedMinArea);
            this.panelRegionBasedDetectionNew.Controls.Add(this.label12);
            this.panelRegionBasedDetectionNew.Controls.Add(this.numericUpDownRegionBasedMaxArea);
            this.panelRegionBasedDetectionNew.Controls.Add(this.label13);
            this.panelRegionBasedDetectionNew.Location = new System.Drawing.Point(94, 14);
            this.panelRegionBasedDetectionNew.Name = "panelRegionBasedDetectionNew";
            this.panelRegionBasedDetectionNew.Size = new System.Drawing.Size(188, 68);
            this.panelRegionBasedDetectionNew.TabIndex = 46;
            // 
            // numericUpDownRegionBasedMinArea
            // 
            this.numericUpDownRegionBasedMinArea.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericUpDownRegionBasedMinArea.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownRegionBasedMinArea.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownRegionBasedMinArea.Location = new System.Drawing.Point(93, 5);
            this.numericUpDownRegionBasedMinArea.Maximum = new decimal(new int[] {
            1661992960,
            1808227885,
            5,
            0});
            this.numericUpDownRegionBasedMinArea.Name = "numericUpDownRegionBasedMinArea";
            this.numericUpDownRegionBasedMinArea.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownRegionBasedMinArea.TabIndex = 41;
            this.numericUpDownRegionBasedMinArea.ThousandsSeparator = true;
            this.numericUpDownRegionBasedMinArea.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.SystemColors.Control;
            this.label12.Location = new System.Drawing.Point(11, 7);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 12);
            this.label12.TabIndex = 40;
            this.label12.Text = "Min. Volume";
            // 
            // numericUpDownRegionBasedMaxArea
            // 
            this.numericUpDownRegionBasedMaxArea.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericUpDownRegionBasedMaxArea.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownRegionBasedMaxArea.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownRegionBasedMaxArea.Location = new System.Drawing.Point(93, 39);
            this.numericUpDownRegionBasedMaxArea.Maximum = new decimal(new int[] {
            1661992960,
            1808227885,
            5,
            0});
            this.numericUpDownRegionBasedMaxArea.Name = "numericUpDownRegionBasedMaxArea";
            this.numericUpDownRegionBasedMaxArea.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownRegionBasedMaxArea.TabIndex = 39;
            this.numericUpDownRegionBasedMaxArea.ThousandsSeparator = true;
            this.numericUpDownRegionBasedMaxArea.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.Control;
            this.label13.Location = new System.Drawing.Point(8, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(75, 12);
            this.label13.TabIndex = 38;
            this.label13.Text = "Max. Volume";
            // 
            // radioButtonVolumeDetectionNew
            // 
            this.radioButtonVolumeDetectionNew.AutoSize = true;
            this.radioButtonVolumeDetectionNew.Checked = true;
            this.radioButtonVolumeDetectionNew.Location = new System.Drawing.Point(22, 38);
            this.radioButtonVolumeDetectionNew.Name = "radioButtonVolumeDetectionNew";
            this.radioButtonVolumeDetectionNew.Size = new System.Drawing.Size(47, 16);
            this.radioButtonVolumeDetectionNew.TabIndex = 41;
            this.radioButtonVolumeDetectionNew.TabStop = true;
            this.radioButtonVolumeDetectionNew.Text = "New";
            this.radioButtonVolumeDetectionNew.UseVisualStyleBackColor = true;
            this.radioButtonVolumeDetectionNew.CheckedChanged += new System.EventHandler(this.radioButtonVolumeDetectionNew_CheckedChanged);
            // 
            // panelSeedBased
            // 
            this.panelSeedBased.Controls.Add(this.comboBoxExistingSpotsDetected);
            this.panelSeedBased.Location = new System.Drawing.Point(6, 41);
            this.panelSeedBased.Name = "panelSeedBased";
            this.panelSeedBased.Size = new System.Drawing.Size(286, 74);
            this.panelSeedBased.TabIndex = 45;
            // 
            // comboBoxExistingSpotsDetected
            // 
            this.comboBoxExistingSpotsDetected.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxExistingSpotsDetected.BackColor = System.Drawing.SystemColors.ControlDark;
            this.comboBoxExistingSpotsDetected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxExistingSpotsDetected.ForeColor = System.Drawing.SystemColors.Control;
            this.comboBoxExistingSpotsDetected.Location = new System.Drawing.Point(61, 26);
            this.comboBoxExistingSpotsDetected.Name = "comboBoxExistingSpotsDetected";
            this.comboBoxExistingSpotsDetected.Size = new System.Drawing.Size(159, 20);
            this.comboBoxExistingSpotsDetected.TabIndex = 30;
            // 
            // radioButtonSeedInitialization
            // 
            this.radioButtonSeedInitialization.AutoSize = true;
            this.radioButtonSeedInitialization.Checked = true;
            this.radioButtonSeedInitialization.ForeColor = System.Drawing.SystemColors.Control;
            this.radioButtonSeedInitialization.Location = new System.Drawing.Point(6, 22);
            this.radioButtonSeedInitialization.Name = "radioButtonSeedInitialization";
            this.radioButtonSeedInitialization.Size = new System.Drawing.Size(87, 17);
            this.radioButtonSeedInitialization.TabIndex = 1;
            this.radioButtonSeedInitialization.TabStop = true;
            this.radioButtonSeedInitialization.Text = "Seeds based";
            this.radioButtonSeedInitialization.UseVisualStyleBackColor = true;
            this.radioButtonSeedInitialization.CheckedChanged += new System.EventHandler(this.radioButtonSeedInitialization_CheckedChanged);
            // 
            // radioButtonRegionsBased
            // 
            this.radioButtonRegionsBased.AutoSize = true;
            this.radioButtonRegionsBased.ForeColor = System.Drawing.SystemColors.Control;
            this.radioButtonRegionsBased.Location = new System.Drawing.Point(6, 131);
            this.radioButtonRegionsBased.Name = "radioButtonRegionsBased";
            this.radioButtonRegionsBased.Size = new System.Drawing.Size(96, 17);
            this.radioButtonRegionsBased.TabIndex = 2;
            this.radioButtonRegionsBased.Text = "Regions based";
            this.radioButtonRegionsBased.UseVisualStyleBackColor = true;
            this.radioButtonRegionsBased.CheckedChanged += new System.EventHandler(this.radioButtonRegionsBased_CheckedChanged);
            // 
            // checkBoxIsRegionGrowing
            // 
            this.checkBoxIsRegionGrowing.AutoSize = true;
            this.checkBoxIsRegionGrowing.Checked = true;
            this.checkBoxIsRegionGrowing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIsRegionGrowing.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxIsRegionGrowing.Location = new System.Drawing.Point(334, 100);
            this.checkBoxIsRegionGrowing.Name = "checkBoxIsRegionGrowing";
            this.checkBoxIsRegionGrowing.Size = new System.Drawing.Size(113, 16);
            this.checkBoxIsRegionGrowing.TabIndex = 11;
            this.checkBoxIsRegionGrowing.Text = "Region Growing";
            this.checkBoxIsRegionGrowing.UseVisualStyleBackColor = true;
            this.checkBoxIsRegionGrowing.CheckedChanged += new System.EventHandler(this.checkBoxIsRegionGrowing_CheckedChanged);
            // 
            // groupBoxRegionGrowing
            // 
            this.groupBoxRegionGrowing.Controls.Add(this.panelForMerging);
            this.groupBoxRegionGrowing.Controls.Add(this.numericUpDownChannelForRegionGrowing);
            this.groupBoxRegionGrowing.Controls.Add(this.numericUpDownIntensityForRegionGrowing);
            this.groupBoxRegionGrowing.Controls.Add(this.label15);
            this.groupBoxRegionGrowing.Controls.Add(this.label16);
            this.groupBoxRegionGrowing.Controls.Add(this.panel1);
            this.groupBoxRegionGrowing.Location = new System.Drawing.Point(321, 100);
            this.groupBoxRegionGrowing.Name = "groupBoxRegionGrowing";
            this.groupBoxRegionGrowing.Size = new System.Drawing.Size(307, 342);
            this.groupBoxRegionGrowing.TabIndex = 10;
            this.groupBoxRegionGrowing.TabStop = false;
            this.groupBoxRegionGrowing.Text = "                               ";
            // 
            // panelForMerging
            // 
            this.panelForMerging.Controls.Add(this.trackBarMergingStrength);
            this.panelForMerging.Controls.Add(this.label11);
            this.panelForMerging.Controls.Add(this.label7);
            this.panelForMerging.Controls.Add(this.label8);
            this.panelForMerging.Location = new System.Drawing.Point(5, 199);
            this.panelForMerging.Name = "panelForMerging";
            this.panelForMerging.Size = new System.Drawing.Size(295, 78);
            this.panelForMerging.TabIndex = 49;
            // 
            // trackBarMergingStrength
            // 
            this.trackBarMergingStrength.Location = new System.Drawing.Point(132, 24);
            this.trackBarMergingStrength.Maximum = 30;
            this.trackBarMergingStrength.Name = "trackBarMergingStrength";
            this.trackBarMergingStrength.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBarMergingStrength.Size = new System.Drawing.Size(139, 45);
            this.trackBarMergingStrength.TabIndex = 30;
            this.trackBarMergingStrength.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarMergingStrength.Value = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(14, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 12);
            this.label11.TabIndex = 27;
            this.label11.Text = "Merging Strength";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(129, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 31;
            this.label7.Text = "Weak";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.Control;
            this.label8.Location = new System.Drawing.Point(235, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 12);
            this.label8.TabIndex = 32;
            this.label8.Text = "Strong";
            // 
            // numericUpDownChannelForRegionGrowing
            // 
            this.numericUpDownChannelForRegionGrowing.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownChannelForRegionGrowing.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownChannelForRegionGrowing.Location = new System.Drawing.Point(147, 41);
            this.numericUpDownChannelForRegionGrowing.Name = "numericUpDownChannelForRegionGrowing";
            this.numericUpDownChannelForRegionGrowing.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownChannelForRegionGrowing.TabIndex = 48;
            // 
            // numericUpDownIntensityForRegionGrowing
            // 
            this.numericUpDownIntensityForRegionGrowing.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownIntensityForRegionGrowing.DecimalPlaces = 1;
            this.numericUpDownIntensityForRegionGrowing.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownIntensityForRegionGrowing.Location = new System.Drawing.Point(147, 73);
            this.numericUpDownIntensityForRegionGrowing.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownIntensityForRegionGrowing.Name = "numericUpDownIntensityForRegionGrowing";
            this.numericUpDownIntensityForRegionGrowing.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownIntensityForRegionGrowing.TabIndex = 46;
            this.numericUpDownIntensityForRegionGrowing.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.SystemColors.Control;
            this.label15.Location = new System.Drawing.Point(35, 75);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(111, 12);
            this.label15.TabIndex = 45;
            this.label15.Text = "Intensity Threshold";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.SystemColors.Control;
            this.label16.Location = new System.Drawing.Point(85, 43);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 12);
            this.label16.TabIndex = 47;
            this.label16.Text = "Channel";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBoxIsConvergence);
            this.panel1.Controls.Add(this.numericUpDownIterationNumber);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(6, 124);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(295, 49);
            this.panel1.TabIndex = 29;
            // 
            // checkBoxIsConvergence
            // 
            this.checkBoxIsConvergence.AutoSize = true;
            this.checkBoxIsConvergence.Checked = true;
            this.checkBoxIsConvergence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIsConvergence.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxIsConvergence.Location = new System.Drawing.Point(195, 16);
            this.checkBoxIsConvergence.Name = "checkBoxIsConvergence";
            this.checkBoxIsConvergence.Size = new System.Drawing.Size(99, 16);
            this.checkBoxIsConvergence.TabIndex = 30;
            this.checkBoxIsConvergence.Text = "Convergence";
            this.checkBoxIsConvergence.UseVisualStyleBackColor = true;
            this.checkBoxIsConvergence.CheckedChanged += new System.EventHandler(this.checkBoxIsConvergence_CheckedChanged);
            // 
            // numericUpDownIterationNumber
            // 
            this.numericUpDownIterationNumber.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownIterationNumber.Enabled = false;
            this.numericUpDownIterationNumber.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownIterationNumber.Location = new System.Drawing.Point(121, 14);
            this.numericUpDownIterationNumber.Maximum = new decimal(new int[] {
            1661992960,
            1808227885,
            5,
            0});
            this.numericUpDownIterationNumber.Name = "numericUpDownIterationNumber";
            this.numericUpDownIterationNumber.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownIterationNumber.TabIndex = 30;
            this.numericUpDownIterationNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(12, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "Iterations Number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(294, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 12);
            this.label6.TabIndex = 24;
            this.label6.Text = "Min. Volume";
            // 
            // numericUpDownVolumeMinVol
            // 
            this.numericUpDownVolumeMinVol.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownVolumeMinVol.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownVolumeMinVol.Location = new System.Drawing.Point(296, 43);
            this.numericUpDownVolumeMinVol.Maximum = new decimal(new int[] {
            1661992960,
            1808227885,
            5,
            0});
            this.numericUpDownVolumeMinVol.Name = "numericUpDownVolumeMinVol";
            this.numericUpDownVolumeMinVol.Size = new System.Drawing.Size(114, 20);
            this.numericUpDownVolumeMinVol.TabIndex = 25;
            this.numericUpDownVolumeMinVol.ThousandsSeparator = true;
            this.numericUpDownVolumeMinVol.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.Control;
            this.label17.Location = new System.Drawing.Point(428, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(75, 12);
            this.label17.TabIndex = 22;
            this.label17.Text = "Max. Volume";
            // 
            // numericUpDownVolumeMaxVol
            // 
            this.numericUpDownVolumeMaxVol.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownVolumeMaxVol.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownVolumeMaxVol.Location = new System.Drawing.Point(430, 43);
            this.numericUpDownVolumeMaxVol.Maximum = new decimal(new int[] {
            1661992960,
            1808227885,
            5,
            0});
            this.numericUpDownVolumeMaxVol.Name = "numericUpDownVolumeMaxVol";
            this.numericUpDownVolumeMaxVol.Size = new System.Drawing.Size(114, 20);
            this.numericUpDownVolumeMaxVol.TabIndex = 23;
            this.numericUpDownVolumeMaxVol.ThousandsSeparator = true;
            this.numericUpDownVolumeMaxVol.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            // 
            // groupBoxPostProcessing
            // 
            this.groupBoxPostProcessing.Controls.Add(this.checkBoxIsBorderKill);
            this.groupBoxPostProcessing.Controls.Add(this.label1);
            this.groupBoxPostProcessing.Controls.Add(this.numericUpDownSmoothIterations);
            this.groupBoxPostProcessing.Controls.Add(this.checkBoxVolumeSmooth);
            this.groupBoxPostProcessing.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBoxPostProcessing.Location = new System.Drawing.Point(12, 448);
            this.groupBoxPostProcessing.Name = "groupBoxPostProcessing";
            this.groupBoxPostProcessing.Size = new System.Drawing.Size(616, 74);
            this.groupBoxPostProcessing.TabIndex = 12;
            this.groupBoxPostProcessing.TabStop = false;
            this.groupBoxPostProcessing.Text = "Post Processings";
            // 
            // checkBoxIsBorderKill
            // 
            this.checkBoxIsBorderKill.AutoSize = true;
            this.checkBoxIsBorderKill.Checked = true;
            this.checkBoxIsBorderKill.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIsBorderKill.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxIsBorderKill.Location = new System.Drawing.Point(67, 32);
            this.checkBoxIsBorderKill.Name = "checkBoxIsBorderKill";
            this.checkBoxIsBorderKill.Size = new System.Drawing.Size(73, 17);
            this.checkBoxIsBorderKill.TabIndex = 51;
            this.checkBoxIsBorderKill.Text = "Border Kill";
            this.checkBoxIsBorderKill.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(432, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 12);
            this.label1.TabIndex = 50;
            this.label1.Text = "Iterations";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownIntensityThreshold);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDownVolumeMinVol);
            this.groupBox1.Controls.Add(this.numericUpDownVolumeMaxVol);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(616, 82);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // numericUpDownIntensityThreshold
            // 
            this.numericUpDownIntensityThreshold.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownIntensityThreshold.ForeColor = System.Drawing.SystemColors.Control;
            this.numericUpDownIntensityThreshold.Location = new System.Drawing.Point(87, 43);
            this.numericUpDownIntensityThreshold.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownIntensityThreshold.Name = "numericUpDownIntensityThreshold";
            this.numericUpDownIntensityThreshold.Size = new System.Drawing.Size(114, 20);
            this.numericUpDownIntensityThreshold.TabIndex = 27;
            this.numericUpDownIntensityThreshold.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(85, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "Intensity Threshold";
            // 
            // FormForVolumeDetection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(640, 563);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxPostProcessing);
            this.Controls.Add(this.checkBoxIsRegionGrowing);
            this.Controls.Add(this.groupBoxRegionGrowing);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonOk);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FormForVolumeDetection";
            this.Text = "Volume Detection";
            this.Shown += new System.EventHandler(this.FormForVolumeDetection_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSmoothIterations)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelForRegionBasedInitialization.ResumeLayout(false);
            this.panelForRegionBasedInitialization.PerformLayout();
            this.panelRegionDetectionExisting.ResumeLayout(false);
            this.panelRegionBasedDetectionNew.ResumeLayout(false);
            this.panelRegionBasedDetectionNew.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionBasedMinArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionBasedMaxArea)).EndInit();
            this.panelSeedBased.ResumeLayout(false);
            this.groupBoxRegionGrowing.ResumeLayout(false);
            this.groupBoxRegionGrowing.PerformLayout();
            this.panelForMerging.ResumeLayout(false);
            this.panelForMerging.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMergingStrength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChannelForRegionGrowing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntensityForRegionGrowing)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIterationNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolumeMinVol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolumeMaxVol)).EndInit();
            this.groupBoxPostProcessing.ResumeLayout(false);
            this.groupBoxPostProcessing.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntensityThreshold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.NumericUpDown numericUpDownSmoothIterations;
        public System.Windows.Forms.CheckBox checkBoxVolumeSmooth;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBoxRegionGrowing;
        private System.Windows.Forms.Panel panelForMerging;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.RadioButton radioButtonSeedInitialization;
        public System.Windows.Forms.RadioButton radioButtonRegionsBased;
        public System.Windows.Forms.CheckBox checkBoxIsRegionGrowing;
        public System.Windows.Forms.TrackBar trackBarMergingStrength;
        public System.Windows.Forms.NumericUpDown numericUpDownChannelForRegionGrowing;
        public System.Windows.Forms.NumericUpDown numericUpDownIntensityForRegionGrowing;
        public System.Windows.Forms.CheckBox checkBoxIsConvergence;
        public System.Windows.Forms.NumericUpDown numericUpDownIterationNumber;
        public System.Windows.Forms.NumericUpDown numericUpDownVolumeMinVol;
        public System.Windows.Forms.NumericUpDown numericUpDownVolumeMaxVol;
        private System.Windows.Forms.Panel panelSeedBased;
        public System.Windows.Forms.ComboBox comboBoxExistingSpotsDetected;
        private System.Windows.Forms.GroupBox groupBoxPostProcessing;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox checkBoxIsBorderKill;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.NumericUpDown numericUpDownIntensityThreshold;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelForRegionBasedInitialization;
        public System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panelRegionDetectionExisting;
        public System.Windows.Forms.RadioButton radioButtonVolumeDetectionExisting;
        private System.Windows.Forms.Panel panelRegionBasedDetectionNew;
        public System.Windows.Forms.NumericUpDown numericUpDownRegionBasedMinArea;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.NumericUpDown numericUpDownRegionBasedMaxArea;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.RadioButton radioButtonVolumeDetectionNew;
    }
}