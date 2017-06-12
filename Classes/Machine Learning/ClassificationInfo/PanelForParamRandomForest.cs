using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
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
