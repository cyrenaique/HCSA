using System;
using System.Windows.Forms;

namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{
    public partial class PanelForParamRandomTree : UserControl
    {
        public PanelForParamRandomTree()
        {
            InitializeComponent();
        }

        private void checkBoxIsBackfitting_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownBackFittingFolds.Enabled = checkBoxIsBackfitting.Checked;
        }
    }
}
