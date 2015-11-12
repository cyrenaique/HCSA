using System;
using System.Windows.Forms;
using ChemSpiderClient.Properties;

namespace ChemSpiderClient
{
    public partial class frmElementsSearch : Form
    {
        public frmElementsSearch()
        {
            InitializeComponent();
            txtIncludeElements.Text = Settings.Default.IncludeElements;
            txtExcludeElements.Text = Settings.Default.ExcludeElements;
            chkIncludeAll.Checked = Settings.Default.IncludeAll;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            Settings.Default.IncludeAll = chkIncludeAll.Checked;
            Settings.Default.IncludeElements = chkIncludeAll.Checked ? string.Empty : txtIncludeElements.Text;
            Settings.Default.ExcludeElements = txtExcludeElements.Text;

            Settings.Default.Save();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void chkIncludeAll_CheckedChanged(object sender, EventArgs e)
        {
            txtIncludeElements.Enabled = !chkIncludeAll.Checked;
        }
    }
}
