using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;

namespace HCSAnalyzer.Cell_by_Cell_and_DB
{
    public partial class FormManualGating : Form
    {
        FormForSingleCellsDisplay Sender;
        public FormManualGating( FormForSingleCellsDisplay Sender)
        {
            InitializeComponent();
            this.Sender = Sender;
        }

        private void numericUpDownDesc1Min_ValueChanged(object sender, EventArgs e)
        {
            Sender.chartForPoints.ChartAreas[0].CursorX.SelectionStart = (double)this.numericUpDownDesc1Min.Value; 
        }

        private void numericUpDownDesc1Max_ValueChanged(object sender, EventArgs e)
        {
            Sender.chartForPoints.ChartAreas[0].CursorX.SelectionEnd = (double)this.numericUpDownDesc1Max.Value; 
        }

        private void numericUpDownDesc2Max_ValueChanged(object sender, EventArgs e)
        {
            Sender.chartForPoints.ChartAreas[0].CursorY.SelectionEnd = (double)this.numericUpDownDesc2Max.Value; 

        }

        private void numericUpDownDesc2Min_ValueChanged(object sender, EventArgs e)
        {
            Sender.chartForPoints.ChartAreas[0].CursorY.SelectionStart = (double)this.numericUpDownDesc2Min.Value; 
        }
    }
}
