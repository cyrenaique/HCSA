using System;
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
