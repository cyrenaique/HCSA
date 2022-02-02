using System;
using System.Windows.Forms;

namespace IM3_Plugin3
{
    public partial class FormForResolution : Form
    {
        public FormForResolution()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
