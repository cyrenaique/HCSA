using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibPlateAnalysis;
using HCSAnalyzer.Classes;

namespace HCSAnalyzer
{
    public partial class FormForImportExcel : Form
    {
        private bool FirstTime = true;
        public bool IsImportCSV =  false;
        public bool IsAppend;
        public int ModeWell;
        public char Separator = ',';

        public cScreening CurrentScreen = null;
        public HCSAnalyzer thisHCSAnalyzer = null;

        public FormForImportExcel()
        {
            InitializeComponent();


            ToolTip MytoolTip = new ToolTip();

            //// Set up the delays for the ToolTip.
            //MytoolTip.AutoPopDelay = 5000;
            //MytoolTip.InitialDelay = 1000;
            //MytoolTip.ReshowDelay = 500;

            //MytoolTip.ShowAlways = true;

            //// Set up the ToolTip text for the Button and Checkbox.
            MytoolTip.SetToolTip(this.checkBoxConvertNaNTo0, "If a non decimal value is detected, it will be automatically converted into a null value.");
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridViewForImport.Rows.Count; i++)
                this.dataGridViewForImport.Rows[i].Cells[1].Value = true;
        }

        private void unselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridViewForImport.Rows.Count; i++)
                this.dataGridViewForImport.Rows[i].Cells[1].Value = false;
        }

        private void toolStripMenuItem96_Click(object sender, EventArgs e)
        {
            this.numericUpDownColumns.Value = 12;
            this.numericUpDownRows.Value = 8;
        }

        private void toolStripMenuItem384_Click(object sender, EventArgs e)
        {
            this.numericUpDownColumns.Value = 24;
            this.numericUpDownRows.Value = 16;
        }

        private void toolStripMenuItem1536_Click(object sender, EventArgs e)
        {
            this.numericUpDownColumns.Value = 48;
            this.numericUpDownRows.Value = 32;
        }


        public int HeaderSize = 0;

        public bool NoName = false;
        public bool IsParentFolder;
    }
}
