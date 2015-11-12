namespace HCSAnalyzer.Simulator.Forms.NewCellType
{
    partial class PanelForTransitionMatrix
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewForProbaTransition = new System.Windows.Forms.DataGridView();
            this.ColumnForNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetToNoTransitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewForProbaTransition)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewForProbaTransition
            // 
            this.dataGridViewForProbaTransition.AllowUserToAddRows = false;
            this.dataGridViewForProbaTransition.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewForProbaTransition.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewForProbaTransition.ColumnHeadersHeight = 28;
            this.dataGridViewForProbaTransition.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnForNames});
            this.dataGridViewForProbaTransition.Location = new System.Drawing.Point(3, 0);
            this.dataGridViewForProbaTransition.Name = "dataGridViewForProbaTransition";
            this.dataGridViewForProbaTransition.Size = new System.Drawing.Size(244, 235);
            this.dataGridViewForProbaTransition.TabIndex = 0;
            // 
            // ColumnForNames
            // 
            this.ColumnForNames.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.ColumnForNames.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnForNames.HeaderText = "Probability";
            this.ColumnForNames.Name = "ColumnForNames";
            this.ColumnForNames.Width = 80;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToNoTransitionToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(187, 26);
            // 
            // resetToNoTransitionToolStripMenuItem
            // 
            this.resetToNoTransitionToolStripMenuItem.Name = "resetToNoTransitionToolStripMenuItem";
            this.resetToNoTransitionToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.resetToNoTransitionToolStripMenuItem.Text = "Reset to no transition";
            this.resetToNoTransitionToolStripMenuItem.Click += new System.EventHandler(this.resetToNoTransitionToolStripMenuItem_Click);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.dataGridViewForProbaTransition);
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(250, 238);
            this.panel.TabIndex = 1;
            // 
            // PanelForTransitionMatrix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForTransitionMatrix";
            this.Size = new System.Drawing.Size(256, 244);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewForProbaTransition)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem resetToNoTransitionToolStripMenuItem;
        public System.Windows.Forms.Panel panel;
        public System.Windows.Forms.DataGridView dataGridViewForProbaTransition;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnForNames;
    }
}
