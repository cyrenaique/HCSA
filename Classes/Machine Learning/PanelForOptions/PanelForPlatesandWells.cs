using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
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
