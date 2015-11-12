using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChemSpiderClient
{
    public partial class frmProgress : Form
    {
        public frmProgress()
        {
            InitializeComponent();
            Text = Application.ProductName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
