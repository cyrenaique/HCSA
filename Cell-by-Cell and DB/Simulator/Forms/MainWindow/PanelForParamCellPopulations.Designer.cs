namespace HCSAnalyzer.Simulator.Forms.Panels
{
    partial class PanelForParamCellPopulations
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
            this.panel = new System.Windows.Forms.Panel();
            this.listViewForCellPopulations = new System.Windows.Forms.ListView();
            this.columnHeaderPopName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCellNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCellType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.listViewForCellPopulations);
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(233, 251);
            this.panel.TabIndex = 0;
            // 
            // listViewForCellPopulations
            // 
            this.listViewForCellPopulations.CheckBoxes = true;
            this.listViewForCellPopulations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderPopName,
            this.columnHeaderCellNum,
            this.columnHeaderCellType});
            this.listViewForCellPopulations.GridLines = true;
            this.listViewForCellPopulations.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewForCellPopulations.Location = new System.Drawing.Point(1, 2);
            this.listViewForCellPopulations.MultiSelect = false;
            this.listViewForCellPopulations.Name = "listViewForCellPopulations";
            this.listViewForCellPopulations.Size = new System.Drawing.Size(230, 246);
            this.listViewForCellPopulations.TabIndex = 9;
            this.listViewForCellPopulations.UseCompatibleStateImageBehavior = false;
            this.listViewForCellPopulations.View = System.Windows.Forms.View.Details;
            this.listViewForCellPopulations.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewForCellPopulations_MouseDown);
            // 
            // columnHeaderPopName
            // 
            this.columnHeaderPopName.Text = "Name";
            this.columnHeaderPopName.Width = 78;
            // 
            // columnHeaderCellNum
            // 
            this.columnHeaderCellNum.Text = "Cell Number";
            this.columnHeaderCellNum.Width = 77;
            // 
            // columnHeaderCellType
            // 
            this.columnHeaderCellType.Text = "Type";
            this.columnHeaderCellType.Width = 90;
            // 
            // PanelForParamCellPopulations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "PanelForParamCellPopulations";
            this.Size = new System.Drawing.Size(239, 258);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnHeaderPopName;
        private System.Windows.Forms.ColumnHeader columnHeaderCellNum;
        private System.Windows.Forms.ColumnHeader columnHeaderCellType;
        public System.Windows.Forms.Panel panel;
        public System.Windows.Forms.ListView listViewForCellPopulations;
    }
}
