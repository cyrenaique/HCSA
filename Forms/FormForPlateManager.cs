using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibPlateAnalysis;

namespace HCSAnalyzer.Forms
{
    public partial class FormForPlateManager : Form
    {
        cScreening CurrentScreening;

        public FormForPlateManager(cScreening CurrentScreen)
        {
            InitializeComponent();
            this.CurrentScreening = CurrentScreen;

            this.dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            

            DataGridViewComboBoxColumn Col = (DataGridViewComboBoxColumn)this.dataGridView.Columns[1];

            List<string> ListReplicateNames = new List<string>();

           // foreach (var item in cGlobalInfo.CurrentScreening.ListReplicate)
            //    ListReplicateNames.Add(item.Name);

            Col.DataSource = ListReplicateNames.ToArray();

            foreach (cPlate TmpPlate in CurrentScreen.ListPlatesAvailable)
            {
                this.dataGridView.Rows.Add();
                this.dataGridView[0, this.dataGridView.Rows.Count - 2].ValueType = typeof(string);
                this.dataGridView[0, this.dataGridView.Rows.Count - 2].Value = TmpPlate.GetName();

               // this.dataGridView[1, this.dataGridView.Rows.Count - 2].Value = TmpPlate.AssociatedReplicateType.Name;

                this.dataGridView[2, this.dataGridView.Rows.Count - 2].Value = TmpPlate.IsActive;

            }

            
            //dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            
            dataGridView.AllowUserToAddRows = false;
            

            ContextMenuStrip ImageContextMenu = new ContextMenuStrip();

            ToolStripMenuItem ToolStripMenuItem_CheckAll = new ToolStripMenuItem("Check all");
            ImageContextMenu.Items.Add(ToolStripMenuItem_CheckAll);
            ToolStripMenuItem_CheckAll.Click += new System.EventHandler(this.ToolStripMenuItem_CheckAll);

            ToolStripMenuItem ToolStripMenuItem_UnCheckAll = new ToolStripMenuItem("Uncheck all");
            ImageContextMenu.Items.Add(ToolStripMenuItem_UnCheckAll);
            ToolStripMenuItem_UnCheckAll.Click += new System.EventHandler(this.ToolStripMenuItem_UnCheckAll);

            ImageContextMenu.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_MoveUp = new ToolStripMenuItem("Move Up");
            ImageContextMenu.Items.Add(ToolStripMenuItem_MoveUp);
            ToolStripMenuItem_MoveUp.Click += new System.EventHandler(this.ToolStripMenuItem_MoveUp);

            ToolStripMenuItem ToolStripMenuItem_MoveDown = new ToolStripMenuItem("Move Down");
            ImageContextMenu.Items.Add(ToolStripMenuItem_MoveDown);
            ToolStripMenuItem_MoveDown.Click += new System.EventHandler(this.ToolStripMenuItem_MoveDown);

            ImageContextMenu.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_CreateNewReplicateType = new ToolStripMenuItem("Create New Replicate Type");
            ImageContextMenu.Items.Add(ToolStripMenuItem_CreateNewReplicateType);
            ToolStripMenuItem_CreateNewReplicateType.Click += new System.EventHandler(this.ToolStripMenuItem_CreateNewReplicateType);

            dataGridView.ContextMenuStrip = ImageContextMenu;
            dataGridView.ContextMenuStrip.BringToFront();
        }

        private void ToolStripMenuItem_CheckAll(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dataGridView.Rows)
                item.Cells[2].Value = true;
        }

        private void ToolStripMenuItem_UnCheckAll(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dataGridView.Rows)
                item.Cells[2].Value = false;
        }

        private void ToolStripMenuItem_MoveUp(object sender, EventArgs e)
        {
            DataGridView grid = this.dataGridView;
            try
            {
                int totalRows = grid.Rows.Count;
                int idx = grid.SelectedCells[0].OwningRow.Index;
                if (idx == 0)
                    return;
                int col = grid.SelectedCells[0].OwningColumn.Index;
                DataGridViewRowCollection rows = grid.Rows;
                DataGridViewRow row = rows[idx];
                rows.Remove(row);
                rows.Insert(idx - 1, row);
                grid.ClearSelection();
                grid.Rows[idx - 1].Cells[col].Selected = true;

            }
            catch { }
        }

        private void ToolStripMenuItem_MoveDown(object sender, EventArgs e)
        {
            DataGridView grid = this.dataGridView;
            try
            {
                int totalRows = grid.Rows.Count;
                int idx = grid.SelectedCells[0].OwningRow.Index;
                if (idx == totalRows - 2)
                    return;
                int col = grid.SelectedCells[0].OwningColumn.Index;
                DataGridViewRowCollection rows = grid.Rows;
                DataGridViewRow row = rows[idx];
                rows.Remove(row);
                rows.Insert(idx + 1, row);
                grid.ClearSelection();
                grid.Rows[idx + 1].Cells[col].Selected = true;
            }
            catch { }
        }


        private void ToolStripMenuItem_CreateNewReplicateType(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow item in dataGridView.Rows)
            //    item.Cells[2].Value = false;
          //  this.CurrentScreening.ListReplicate.Add(new cReplicateType("New Replicate"));
            UpDateDropBox();
        }

        void UpDateDropBox()
        {

            DataGridViewComboBoxColumn Col = (DataGridViewComboBoxColumn)this.dataGridView.Columns[1];

            List<string> ListReplicateNames = new List<string>();

           // foreach (var item in cGlobalInfo.CurrentScreening.ListReplicate)
           //     ListReplicateNames.Add(item.Name);

            Col.DataSource = ListReplicateNames.ToArray();
        }
    }
}
