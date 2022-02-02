using System;
using System.Windows.Forms;

namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{
    public partial class PanelForParamRandomForest : UserControl
    {
        public PanelForParamRandomForest()
        {
            InitializeComponent();
        }

        private void checkBoxMaxDepthUnlimited_CheckedChanged(object sender, EventArgs e)
        {
            this.numericUpDownMaxDepth.Enabled = !checkBoxMaxDepthUnlimited.Checked;
        }
    }
}
