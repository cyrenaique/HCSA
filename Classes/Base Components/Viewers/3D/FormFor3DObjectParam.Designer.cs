namespace HCSAnalyzer.Classes.Base_Components.Viewers._3D
{
    partial class FormFor3DObjectParam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFor3DObjectParam));
            this.panelFor3DOptions = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelColor = new System.Windows.Forms.Panel();
            this.Opacity = new System.Windows.Forms.GroupBox();
            this.numericUpDownOpacity = new System.Windows.Forms.NumericUpDown();
            this.GroupBoxPosition = new System.Windows.Forms.GroupBox();
            this.contextMenuStripPos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.centerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDownPosZ = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownPosY = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownPosX = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonSolid = new System.Windows.Forms.RadioButton();
            this.radioButtonWireFrame = new System.Windows.Forms.RadioButton();
            this.radioButtonPoint = new System.Windows.Forms.RadioButton();
            this.panelFor3DOptions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Opacity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpacity)).BeginInit();
            this.GroupBoxPosition.SuspendLayout();
            this.contextMenuStripPos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosX)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFor3DOptions
            // 
            this.panelFor3DOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFor3DOptions.Controls.Add(this.groupBox2);
            this.panelFor3DOptions.Controls.Add(this.groupBox1);
            this.panelFor3DOptions.Controls.Add(this.Opacity);
            this.panelFor3DOptions.Controls.Add(this.GroupBoxPosition);
            this.panelFor3DOptions.Location = new System.Drawing.Point(2, 3);
            this.panelFor3DOptions.Name = "panelFor3DOptions";
            this.panelFor3DOptions.Size = new System.Drawing.Size(407, 178);
            this.panelFor3DOptions.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelColor);
            this.groupBox1.Location = new System.Drawing.Point(202, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(191, 65);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color";
            // 
            // panelColor
            // 
            this.panelColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColor.Location = new System.Drawing.Point(16, 24);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(24, 23);
            this.panelColor.TabIndex = 0;
            this.panelColor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDoubleClick);
            // 
            // Opacity
            // 
            this.Opacity.Controls.Add(this.numericUpDownOpacity);
            this.Opacity.Location = new System.Drawing.Point(5, 110);
            this.Opacity.Name = "Opacity";
            this.Opacity.Size = new System.Drawing.Size(191, 65);
            this.Opacity.TabIndex = 1;
            this.Opacity.TabStop = false;
            this.Opacity.Text = "Opacity";
            // 
            // numericUpDownOpacity
            // 
            this.numericUpDownOpacity.DecimalPlaces = 4;
            this.numericUpDownOpacity.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownOpacity.Location = new System.Drawing.Point(35, 27);
            this.numericUpDownOpacity.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownOpacity.Name = "numericUpDownOpacity";
            this.numericUpDownOpacity.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownOpacity.TabIndex = 6;
            this.numericUpDownOpacity.ValueChanged += new System.EventHandler(this.numericUpDownOpacity_ValueChanged);
            // 
            // GroupBoxPosition
            // 
            this.GroupBoxPosition.ContextMenuStrip = this.contextMenuStripPos;
            this.GroupBoxPosition.Controls.Add(this.numericUpDownPosZ);
            this.GroupBoxPosition.Controls.Add(this.label3);
            this.GroupBoxPosition.Controls.Add(this.numericUpDownPosY);
            this.GroupBoxPosition.Controls.Add(this.label2);
            this.GroupBoxPosition.Controls.Add(this.numericUpDownPosX);
            this.GroupBoxPosition.Controls.Add(this.label1);
            this.GroupBoxPosition.Location = new System.Drawing.Point(3, 3);
            this.GroupBoxPosition.Name = "GroupBoxPosition";
            this.GroupBoxPosition.Size = new System.Drawing.Size(193, 101);
            this.GroupBoxPosition.TabIndex = 0;
            this.GroupBoxPosition.TabStop = false;
            this.GroupBoxPosition.Text = "Position";
            // 
            // contextMenuStripPos
            // 
            this.contextMenuStripPos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.centerToolStripMenuItem});
            this.contextMenuStripPos.Name = "contextMenuStripPos";
            this.contextMenuStripPos.Size = new System.Drawing.Size(110, 26);
            // 
            // centerToolStripMenuItem
            // 
            this.centerToolStripMenuItem.Name = "centerToolStripMenuItem";
            this.centerToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.centerToolStripMenuItem.Text = "Center";
            this.centerToolStripMenuItem.Click += new System.EventHandler(this.centerToolStripMenuItem_Click);
            // 
            // numericUpDownPosZ
            // 
            this.numericUpDownPosZ.DecimalPlaces = 4;
            this.numericUpDownPosZ.Location = new System.Drawing.Point(51, 69);
            this.numericUpDownPosZ.Maximum = new decimal(new int[] {
            1874919424,
            2328306,
            0,
            0});
            this.numericUpDownPosZ.Minimum = new decimal(new int[] {
            276447232,
            23283,
            0,
            -2147483648});
            this.numericUpDownPosZ.Name = "numericUpDownPosZ";
            this.numericUpDownPosZ.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownPosZ.TabIndex = 5;
            this.numericUpDownPosZ.ValueChanged += new System.EventHandler(this.numericUpDownPosZ_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ContextMenuStrip = this.contextMenuStripPos;
            this.label3.Location = new System.Drawing.Point(22, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Z";
            // 
            // numericUpDownPosY
            // 
            this.numericUpDownPosY.DecimalPlaces = 4;
            this.numericUpDownPosY.Location = new System.Drawing.Point(51, 43);
            this.numericUpDownPosY.Maximum = new decimal(new int[] {
            1874919424,
            2328306,
            0,
            0});
            this.numericUpDownPosY.Minimum = new decimal(new int[] {
            276447232,
            23283,
            0,
            -2147483648});
            this.numericUpDownPosY.Name = "numericUpDownPosY";
            this.numericUpDownPosY.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownPosY.TabIndex = 3;
            this.numericUpDownPosY.ValueChanged += new System.EventHandler(this.numericUpDownPosY_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ContextMenuStrip = this.contextMenuStripPos;
            this.label2.Location = new System.Drawing.Point(22, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y";
            // 
            // numericUpDownPosX
            // 
            this.numericUpDownPosX.DecimalPlaces = 4;
            this.numericUpDownPosX.Location = new System.Drawing.Point(51, 18);
            this.numericUpDownPosX.Maximum = new decimal(new int[] {
            1874919424,
            2328306,
            0,
            0});
            this.numericUpDownPosX.Minimum = new decimal(new int[] {
            276447232,
            23283,
            0,
            -2147483648});
            this.numericUpDownPosX.Name = "numericUpDownPosX";
            this.numericUpDownPosX.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownPosX.TabIndex = 1;
            this.numericUpDownPosX.ValueChanged += new System.EventHandler(this.numericUpDownPosX_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ContextMenuStrip = this.contextMenuStripPos;
            this.label1.Location = new System.Drawing.Point(22, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(334, 187);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonPoint);
            this.groupBox2.Controls.Add(this.radioButtonWireFrame);
            this.groupBox2.Controls.Add(this.radioButtonSolid);
            this.groupBox2.Location = new System.Drawing.Point(202, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(191, 57);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Display Mode";
            // 
            // radioButtonSolid
            // 
            this.radioButtonSolid.AutoSize = true;
            this.radioButtonSolid.Checked = true;
            this.radioButtonSolid.Location = new System.Drawing.Point(11, 23);
            this.radioButtonSolid.Name = "radioButtonSolid";
            this.radioButtonSolid.Size = new System.Drawing.Size(48, 17);
            this.radioButtonSolid.TabIndex = 0;
            this.radioButtonSolid.Text = "Solid";
            this.radioButtonSolid.UseVisualStyleBackColor = true;
            this.radioButtonSolid.CheckedChanged += new System.EventHandler(this.radioButtonSolid_CheckedChanged);
            // 
            // radioButtonWireFrame
            // 
            this.radioButtonWireFrame.AutoSize = true;
            this.radioButtonWireFrame.Location = new System.Drawing.Point(62, 23);
            this.radioButtonWireFrame.Name = "radioButtonWireFrame";
            this.radioButtonWireFrame.Size = new System.Drawing.Size(73, 17);
            this.radioButtonWireFrame.TabIndex = 1;
            this.radioButtonWireFrame.Text = "Wireframe";
            this.radioButtonWireFrame.UseVisualStyleBackColor = true;
            this.radioButtonWireFrame.CheckedChanged += new System.EventHandler(this.radioButtonWireFrame_CheckedChanged);
            // 
            // radioButtonPoint
            // 
            this.radioButtonPoint.AutoSize = true;
            this.radioButtonPoint.Location = new System.Drawing.Point(137, 23);
            this.radioButtonPoint.Name = "radioButtonPoint";
            this.radioButtonPoint.Size = new System.Drawing.Size(49, 17);
            this.radioButtonPoint.TabIndex = 2;
            this.radioButtonPoint.Text = "Point";
            this.radioButtonPoint.UseVisualStyleBackColor = true;
            this.radioButtonPoint.CheckedChanged += new System.EventHandler(this.radioButtonPoint_CheckedChanged);
            // 
            // FormFor3DObjectParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 216);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.panelFor3DOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFor3DObjectParam";
            this.Text = "3D Object Parameter";
            this.panelFor3DOptions.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.Opacity.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpacity)).EndInit();
            this.GroupBoxPosition.ResumeLayout(false);
            this.GroupBoxPosition.PerformLayout();
            this.contextMenuStripPos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosX)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelFor3DOptions;
        private System.Windows.Forms.GroupBox GroupBoxPosition;
        public System.Windows.Forms.NumericUpDown numericUpDownPosZ;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown numericUpDownPosY;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDownPosX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPos;
        private System.Windows.Forms.ToolStripMenuItem centerToolStripMenuItem;
        private System.Windows.Forms.GroupBox Opacity;
        public System.Windows.Forms.NumericUpDown numericUpDownOpacity;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panelColor;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.RadioButton radioButtonPoint;
        public System.Windows.Forms.RadioButton radioButtonWireFrame;
        public System.Windows.Forms.RadioButton radioButtonSolid;
    }
}