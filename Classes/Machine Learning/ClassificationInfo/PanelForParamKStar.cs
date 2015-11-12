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
