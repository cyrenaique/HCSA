using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Simulator.Classes;

namespace HCSAnalyzer.Simulator.Forms.NewCellType
{
    public partial class PanelForTransitionMatrix : UserControl
    {
        FormForSimuGenerator Parent;

        public PanelForTransitionMatrix(FormForSimuGenerator Parent, FormForNewCellType DirectParent)
        {
            InitializeComponent();
            this.Parent = Parent;
            DirectParent.TransitionMatrixGUI = this;
            foreach (var item in Parent.ListCellTypes)
            {
                this.dataGridViewForProbaTransition.Rows.Add();
                this.dataGridViewForProbaTransition.Rows[this.dataGridViewForProbaTransition.Rows.Count - 1].HeaderCell.Value = item.Name;
                this.dataGridViewForProbaTransition[0, this.dataGridViewForProbaTransition.Rows.Count - 1].Value = 0;
            }

            this.dataGridViewForProbaTransition.Rows.Add();
            this.dataGridViewForProbaTransition.Rows[this.dataGridViewForProbaTransition.Rows.Count - 1].HeaderCell.Value = "New Type";
            this.dataGridViewForProbaTransition[0, this.dataGridViewForProbaTransition.Rows.Count - 1].Value = 1;

            //dataGridViewForProbaTransition.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
         //   dataGridViewForProbaTransition.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridViewForProbaTransition.AutoResizeRowHeadersWidth( DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);//[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

        }



        //public PanelForTransitionMatrix(FormForSimuGenerator Parent, FormForNewCellType DirectParent, cCellType CellType )
        //{
        //    InitializeComponent();
        //    this.Parent = Parent;
        //    DirectParent.TransitionMatrixGUI = this;
        //    foreach (var item in CellType.ListInitialTransitions)
        //    {
        //        this.dataGridView.Rows.Add();
        //        this.dataGridView.Rows[this.dataGridView.Rows.Count - 1].HeaderCell.Value = item.DestType.Name;
        //        this.dataGridView[0, this.dataGridView.Rows.Count - 1].Value = item.Value;
        //    }
        //}

        private void resetToNoTransitionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
}
