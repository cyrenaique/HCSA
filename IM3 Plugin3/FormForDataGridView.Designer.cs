namespace IM3_Plugin3
{
    partial class FormForDataGridView
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
            this.dataGridViewForResults = new System.Windows.Forms.DataGridView();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonSaveToDatabase = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewForResults)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewForResults
            // 
            this.dataGridViewForResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewForResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewForResults.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewForResults.Name = "dataGridViewForResults";
            this.dataGridViewForResults.Size = new System.Drawing.Size(708, 753);
            this.dataGridViewForResults.TabIndex = 0;
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Location = new System.Drawing.Point(726, 12);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(130, 35);
            this.buttonClear.TabIndex = 1;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonSaveToDatabase
            // 
            this.buttonSaveToDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveToDatabase.Location = new System.Drawing.Point(726, 53);
            this.buttonSaveToDatabase.Name = "buttonSaveToDatabase";
            this.buttonSaveToDatabase.Size = new System.Drawing.Size(130, 35);
            this.buttonSaveToDatabase.TabIndex = 2;
            this.buttonSaveToDatabase.Text = "Save To DataBase";
            this.buttonSaveToDatabase.UseVisualStyleBackColor = true;
            this.buttonSaveToDatabase.Click += new System.EventHandler(this.buttonSaveToDatabase_Click);
            // 
            // FormForDataGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 777);
            this.Controls.Add(this.buttonSaveToDatabase);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.dataGridViewForResults);
            this.Name = "FormForDataGridView";
            this.Text = "Data Export";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewForResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClear;
        public System.Windows.Forms.DataGridView dataGridViewForResults;
        private System.Windows.Forms.Button buttonSaveToDatabase;
    }
}