using System.Windows.Forms;

namespace ChemSpiderClient
{
    public partial class frmPredictedPropertySearch : Form
    {
        public frmPredictedPropertySearch()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
