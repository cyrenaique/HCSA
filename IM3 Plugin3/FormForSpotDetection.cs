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
    public partial class FormForSpotDetection : Form
    {

        FormForControl Parent;

        public FormForSpotDetection(FormForControl Parent)
        {
            InitializeComponent();

            this.Parent = Parent;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
