using System;
using System.Windows.Forms;
using ChemSpiderClient.Properties;

namespace ChemSpiderClient
{
    public partial class frmCommonSearchOptions : Form
    {
        public frmCommonSearchOptions()
        {
            InitializeComponent();
            cboComplexity.Text = Settings.Default.Complexity;
            cboIsotopic.Text = Settings.Default.Isotopic;
            if (string.IsNullOrEmpty(cboComplexity.Text))
                cboComplexity.SelectedIndex = 0;
            if (string.IsNullOrEmpty(cboIsotopic.Text))
                cboIsotopic.SelectedIndex = 0;
            cboIsotopic.Text = Settings.Default.Isotopic;
            chkHasPatents.Checked = Settings.Default.HasPatents;
            chkHasSpectra.Checked = Settings.Default.HasSpectra;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Settings.Default.Complexity = cboComplexity.Text;
            Settings.Default.Isotopic = cboIsotopic.Text;
            Settings.Default.HasPatents = chkHasPatents.Checked;
            Settings.Default.HasSpectra = chkHasSpectra.Checked;
            Settings.Default.Save();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
