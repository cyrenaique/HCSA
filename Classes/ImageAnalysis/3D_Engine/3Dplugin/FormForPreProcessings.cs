using System;
using System.Windows.Forms;

namespace IM3_Plugin3
{
    public partial class FormForPreProcessings : Form
    {

        FormForControl Parent;

        public FormForPreProcessings(FormForControl Parent)
        {
            InitializeComponent();
            this.Parent = Parent;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void checkBoxBlurBackgroundIsDisable_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownBlurForBackground.Enabled = !checkBoxBlurBackgroundIsDisable.Checked;
        }

        private void checkBoxMedianIsDisabled_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownMedianKernel.Enabled = !checkBoxMedianIsDisabled.Checked;
        }
    }
}
