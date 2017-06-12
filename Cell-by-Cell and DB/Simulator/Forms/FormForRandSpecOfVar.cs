using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Simulator.Classes;

namespace HCSAnalyzer.Simulator.Forms
{
    public partial class FormForRandSpecOfVar : Form
    {
        public cClassForRandomParam RandomParam;

        public FormForRandSpecOfVar(cClassForRandomParam RandomParam)
        {
            InitializeComponent();
            this.numericUpDownMax.Value = (decimal)RandomParam.Max;
            this.numericUpDownMin.Value = (decimal)RandomParam.Min;
            this.RandomParam = RandomParam;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.RandomParam.Max = (double)this.numericUpDownMax.Value;
            this.RandomParam.Min = (double)this.numericUpDownMin.Value;

            
        }
    }
}
