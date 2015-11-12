using System.Windows.Forms;

namespace ChemSpiderClient
{
    public partial class frmIntrinsicPropertySearch : Form
    {
        public frmIntrinsicPropertySearch()
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
