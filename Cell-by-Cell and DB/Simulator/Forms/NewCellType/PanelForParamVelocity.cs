using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HCSAnalyzer.Simulator.Forms.NewCellType
{
    public partial class PanelForParamVelocity : UserControl
    {
        public PanelForParamVelocity()
        {
            InitializeComponent();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.numericUpDownBack.Value = 1;
            this.numericUpDownFront.Value = 1;
            this.numericUpDownLeft.Value = 1;
            this.numericUpDownRight.Value = 1;
            this.numericUpDownTop.Value = 0;
            this.numericUpDownBottom.Value = 0;
        }

    
    }
}
