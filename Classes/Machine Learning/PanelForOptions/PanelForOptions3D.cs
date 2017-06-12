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
    public partial class PanelForOptions3D : UserControl
    {
        public PanelForOptions3D()
        {
            InitializeComponent();
        }


        private void checkBox3DComputeThinPlate_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBoxForSurfaceAnalysis.Enabled = this.checkBox3DComputeThinPlate.Checked;
        }

        private void checkBoxDRC_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBoxForDRC.Enabled = this.checkBoxDRC.Checked;
        }
    }
}
