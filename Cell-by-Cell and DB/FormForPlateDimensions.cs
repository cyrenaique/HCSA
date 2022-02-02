using System;
using System.Windows.Forms;

namespace HCSAnalyzer.Forms
{
    public partial class FormForPlateDimensions : Form
    {
        public FormForPlateDimensions()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem96_Click(object sender, EventArgs e)
        {
            numericUpDownColumns.Value = 12;
            numericUpDownRows.Value = 8;
        }

        private void toolStripMenuItem384_Click(object sender, EventArgs e)
        {
            numericUpDownColumns.Value = 24;
            numericUpDownRows.Value = 16;
        }

        private void toolStripMenuItem1536_Click(object sender, EventArgs e)
        {
            numericUpDownColumns.Value = 48;
            numericUpDownRows.Value = 32;

        }
    }
}
