using System;
using System.Windows.Forms;

namespace IM3_Plugin3
{
    public partial class FormForLinkToMasterInfo : Form
    {
        public FormForLinkToMasterInfo()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void checkBoxDrawLinkToMasterCenter_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxDisplayBranchToCenterDistance.Enabled = checkBoxDrawLinkToMasterCenter.Checked;
        }

    }
}
