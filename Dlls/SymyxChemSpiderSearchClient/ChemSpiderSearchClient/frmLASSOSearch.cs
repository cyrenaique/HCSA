using System;
using System.Windows.Forms;
using ChemSpiderClient.Properties;

namespace ChemSpiderClient
{
    public partial class frmLASSOSearch : Form
    {
        public frmLASSOSearch()
        {
            InitializeComponent();
            cboFamilyMax.Text = Settings.Default.FamilyMax;
            cboFamilyMin.Text = Settings.Default.FamilyMin;
            nudThresholdMax.Value = Convert.ToDecimal(Settings.Default.ThresholdMax);
            nudThresholdMin.Value = Convert.ToDecimal(Settings.Default.ThresholdMin);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Settings.Default.FamilyMin = cboFamilyMin.Text;
            Settings.Default.FamilyMax = cboFamilyMax.Value;
            Settings.Default.ThresholdMax = Convert.ToDouble(nudThresholdMax.Value);
            Settings.Default.ThresholdMin = Convert.ToDouble(nudThresholdMin.Value);

            Settings.Default.Save();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
