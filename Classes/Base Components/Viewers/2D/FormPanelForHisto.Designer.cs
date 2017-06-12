namespace HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic
{
    partial class FormPanelForHisto
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
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.panelForGraphContainer = new System.Windows.Forms.Panel();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.majorXAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.majorYAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backColorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.shadowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markerBorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelForGraphContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelForGraphContainer
            // 
            this.panelForGraphContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForGraphContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelForGraphContainer.Controls.Add(this.chart);
            this.panelForGraphContainer.Location = new System.Drawing.Point(3, 4);
            this.panelForGraphContainer.Name = "panelForGraphContainer";
            this.panelForGraphContainer.Size = new System.Drawing.Size(471, 227);
            this.panelForGraphContainer.TabIndex = 0;
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(-1, -1);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(471, 227);
            this.chart.TabIndex = 2;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayToolStripMenuItem,
            this.axisToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyToClipboardToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(170, 98);
            // 
            // displayToolStripMenuItem
            // 
            this.displayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gridToolStripMenuItem,
            this.backColorToolStripMenuItem1,
            this.shadowToolStripMenuItem,
            this.markerBorderToolStripMenuItem});
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            this.displayToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.displayToolStripMenuItem.Text = "Display";
            // 
            // gridToolStripMenuItem
            // 
            this.gridToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.majorXAxisToolStripMenuItem,
            this.majorYAxisToolStripMenuItem});
            this.gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            this.gridToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.gridToolStripMenuItem.Text = "Grid";
            // 
            // majorXAxisToolStripMenuItem
            // 
            this.majorXAxisToolStripMenuItem.Checked = true;
            this.majorXAxisToolStripMenuItem.CheckOnClick = true;
            this.majorXAxisToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.majorXAxisToolStripMenuItem.Name = "majorXAxisToolStripMenuItem";
            this.majorXAxisToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.majorXAxisToolStripMenuItem.Text = "Major X Axis";
            this.majorXAxisToolStripMenuItem.Click += new System.EventHandler(this.majorXAxisToolStripMenuItem_Click);
            // 
            // majorYAxisToolStripMenuItem
            // 
            this.majorYAxisToolStripMenuItem.Checked = true;
            this.majorYAxisToolStripMenuItem.CheckOnClick = true;
            this.majorYAxisToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.majorYAxisToolStripMenuItem.Name = "majorYAxisToolStripMenuItem";
            this.majorYAxisToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.majorYAxisToolStripMenuItem.Text = "Major Y Axis";
            this.majorYAxisToolStripMenuItem.Click += new System.EventHandler(this.majorYAxisToolStripMenuItem_Click);
            // 
            // backColorToolStripMenuItem1
            // 
            this.backColorToolStripMenuItem1.Name = "backColorToolStripMenuItem1";
            this.backColorToolStripMenuItem1.Size = new System.Drawing.Size(149, 22);
            this.backColorToolStripMenuItem1.Text = "Back Color";
            this.backColorToolStripMenuItem1.Click += new System.EventHandler(this.backColorToolStripMenuItem1_Click);
            // 
            // shadowToolStripMenuItem
            // 
            this.shadowToolStripMenuItem.Checked = true;
            this.shadowToolStripMenuItem.CheckOnClick = true;
            this.shadowToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.shadowToolStripMenuItem.Name = "shadowToolStripMenuItem";
            this.shadowToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.shadowToolStripMenuItem.Text = "Shadow";
            this.shadowToolStripMenuItem.Click += new System.EventHandler(this.shadowToolStripMenuItem_Click);
            // 
            // markerBorderToolStripMenuItem
            // 
            this.markerBorderToolStripMenuItem.Checked = true;
            this.markerBorderToolStripMenuItem.CheckOnClick = true;
            this.markerBorderToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.markerBorderToolStripMenuItem.Name = "markerBorderToolStripMenuItem";
            this.markerBorderToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.markerBorderToolStripMenuItem.Text = "Marker Border";
            this.markerBorderToolStripMenuItem.Click += new System.EventHandler(this.markerBorderToolStripMenuItem_Click);
            // 
            // axisToolStripMenuItem
            // 
            this.axisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yAxisToolStripMenuItem,
            this.xAxisToolStripMenuItem});
            this.axisToolStripMenuItem.Name = "axisToolStripMenuItem";
            this.axisToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.axisToolStripMenuItem.Text = "Axis";
            // 
            // yAxisToolStripMenuItem
            // 
            this.yAxisToolStripMenuItem.Name = "yAxisToolStripMenuItem";
            this.yAxisToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.yAxisToolStripMenuItem.Text = "Y Axis";
            this.yAxisToolStripMenuItem.Click += new System.EventHandler(this.yAxisToolStripMenuItem_Click);
            // 
            // xAxisToolStripMenuItem
            // 
            this.xAxisToolStripMenuItem.Enabled = false;
            this.xAxisToolStripMenuItem.Name = "xAxisToolStripMenuItem";
            this.xAxisToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.xAxisToolStripMenuItem.Text = "X Axis";
            this.xAxisToolStripMenuItem.Click += new System.EventHandler(this.xAxisToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayStatisticsToolStripMenuItem});
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.infoToolStripMenuItem.Text = "Info";
            // 
            // displayStatisticsToolStripMenuItem
            // 
            this.displayStatisticsToolStripMenuItem.CheckOnClick = true;
            this.displayStatisticsToolStripMenuItem.Name = "displayStatisticsToolStripMenuItem";
            this.displayStatisticsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.displayStatisticsToolStripMenuItem.Text = "Display Statistics";
            this.displayStatisticsToolStripMenuItem.Click += new System.EventHandler(this.displayStatisticsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Silver;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(497, 70);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // FormPanelForHisto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 337);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelForGraphContainer);
            this.Name = "FormPanelForHisto";
            this.Text = "FormPanelForHisto";
            this.panelForGraphContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelForGraphContainer;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backColorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem gridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem majorXAxisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem majorYAxisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem axisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yAxisToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.ToolStripMenuItem xAxisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shadowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markerBorderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayStatisticsToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}