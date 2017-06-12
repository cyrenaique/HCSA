using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HCSAnalyzer.Simulator.Forms.Panels
{
    public partial class PanelForParamPlateDesign : UserControl
    {
        public PanelForParamPlateDesign()
        {
            InitializeComponent();
        }

        private void checkBoxExportToDB_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxPlateDesign.Enabled = checkBoxExportToDB.Checked;
        }
    }
}
