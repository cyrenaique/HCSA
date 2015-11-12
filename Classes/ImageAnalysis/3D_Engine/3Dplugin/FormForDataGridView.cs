using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace IM3_Plugin3
{
    public partial class FormForDataGridView : Form
    {
        public FormForDataGridView()
        {
            InitializeComponent();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.dataGridViewForResults.Columns.Clear();
        }

        int Idx = 0;

    //    private void buttonSaveToDatabase_Click(object sender, EventArgs e)
    //    {
    //        List<string> ListNames = new List<string>();


    //        for (int IdxDesc = 0; IdxDesc < this.dataGridViewForResults.Columns.Count; IdxDesc++)
    //            ListNames.Add(this.dataGridViewForResults.Columns[IdxDesc].Name);


    //        cSQLiteDatabase NewDB = new cSQLiteDatabase("Plate" + Idx, ListNames);

    //        NewDB.SaveTableForHCS_Analyzer((DataTable)this.dataGridViewForResults.DataSource, Idx, 1);

    //        NewDB.CloseConnection();
                
    //    }
    //
    
    }
}
