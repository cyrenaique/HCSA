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
    public partial class frmSubstructureSearch : Form
    {
        public frmSubstructureSearch()
        {
            InitializeComponent();
            checkBox1.Checked = Settings.Default.MatchTautomers;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Settings.Default.MatchTautomers = checkBox1.Checked;
            Settings.Default.Save();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
