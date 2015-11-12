using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ChemSpiderClient.Properties;

namespace ChemSpiderClient
{
    public partial class frmSimilaritySearch : Form
    {
        public frmSimilaritySearch()
        {
            InitializeComponent();
            comboBox1.Text = Settings.Default.SimilarityType;
            numericUpDown1.Value = Convert.ToDecimal(Settings.Default.SimilarityThreshold);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Settings.Default.SimilarityType = comboBox1.Text;
            Settings.Default.SimilarityThreshold = Convert.ToSingle(numericUpDown1.Value);
            Settings.Default.Save();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
