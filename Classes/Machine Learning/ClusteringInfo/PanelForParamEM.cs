using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HCSAnalyzer.Forms.FormsForOptions.ClusteringInfo
{
    public partial class PanelForParamEM : UserControl
    {
        public PanelForParamEM()
        {
            InitializeComponent();
            
        }

        public object GetPanel()
        {
            return this;

        }

        private void checkBoxAutomatedClassNum_CheckedChanged(object sender, EventArgs e)
        {
            this.numericUpDownNumClasses.Enabled = !this.checkBoxAutomatedClassNum.Checked;
        }
    }
}
