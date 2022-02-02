using System;
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
