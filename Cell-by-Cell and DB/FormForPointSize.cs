using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HCSAnalyzer.Forms.FormsForGraphsDisplay
{
    public partial class FormForPointSize : Form
    {
        public FormForPointSize()
        {
            InitializeComponent();
        }

        private void trackBarPointSize_ValueChanged(object sender, EventArgs e)
        {
            this.labelForPointSize.Text = trackBarPointSize.Value.ToString();
        }
    }
}
