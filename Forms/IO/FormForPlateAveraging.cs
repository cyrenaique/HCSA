using System;
using System.Windows.Forms;

namespace HCSAnalyzer.Forms.IO
{
    public partial class FormForPlateAveraging : Form
    {
        public FormForPlateAveraging()
        {
            InitializeComponent();
        }

        private void FormForPlateAveraging_Load(object sender, EventArgs e)
        {
            ToolTip NewTip = new ToolTip();
            NewTip.SetToolTip(this.checkBoxWeightedSum, "If selected, each value will be weighted by the associated plate quality value.\nIf this value is not defined, it will be automatically set to unit.");



        }
    }
}
