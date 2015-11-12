using System;
using System.Windows.Forms;

namespace ChemSpiderClient
{
    public partial class frmSimpleSearch : Form
    {
        public frmSimpleSearch()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQuery.Text))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
