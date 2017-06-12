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
