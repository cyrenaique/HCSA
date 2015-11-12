using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IM3_Plugin3
{
    public partial class FormForInfoBeforeStarting : Form
    {
        public FormForInfoBeforeStarting()
        {
            InitializeComponent();
        }

        private void buttonResetResolutions_Click(object sender, EventArgs e)
        {
            this.numericUpDownResolutionX.Value = (decimal)1.0;
            this.numericUpDownResolutionY.Value = (decimal)1.0;
            this.numericUpDownResolutionZ.Value = (decimal)1.0;

        }

    }
}
