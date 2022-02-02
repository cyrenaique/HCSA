using System;
using System.Windows.Forms;

namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{
    public partial class PanelForParamKStar : UserControl
    {
        public PanelForParamKStar()
        {
            InitializeComponent();
        }

        private void checkBoxBlendAuto_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownGlobalBlend.Enabled = !checkBoxBlendAuto.Checked;
        }
    }
}
