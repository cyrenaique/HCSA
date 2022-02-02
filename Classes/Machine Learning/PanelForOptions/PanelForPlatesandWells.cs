using System;
using System.Windows.Forms;

namespace HCSAnalyzer.Forms.FormsForOptions.PanelForOptions
{
    public partial class PanelForPlatesandWells : UserControl
    {
        public PanelForPlatesandWells()
        {
            InitializeComponent();
        }

        private void checkBoxDisplayWellInformation_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxForWellInfo.Enabled = checkBoxDisplayWellInformation.Checked;
        }
    }
}
