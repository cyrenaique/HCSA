namespace HCSAnalyzer.Forms.FormsForGraphsDisplay
{
    partial class FormToDisplaySimpleTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormToDisplaySimpleTable));
            this.dataGridViewForTable = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewForTable)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewForTable
            // 
            this.dataGridViewForTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewForTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewForTable.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewForTable.Name = "dataGridViewForTable";
            this.dataGridViewForTable.Size = new System.Drawing.Size(916, 772);
            this.dataGridViewForTable.TabIndex = 1;
            // 
            // FormToDisplaySimpleTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 796);
            this.Controls.Add(this.dataGridViewForTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormToDisplaySimpleTable";
            this.Text = "FormToDisplaySimpleTable";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewForTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridViewForTable;
    }
}