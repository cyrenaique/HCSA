using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HCSAnalyzer.Classes.Base_Components.GUI
{
    public partial class FormForHistoBinOptions : Form
    {
        public FormForHistoBinOptions(int InitialBinNumber)
        {
            InitializeComponent();
            this.numericUpDown.Value = InitialBinNumber;
            trackBar.Value = (int)this.numericUpDown.Value;
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.numericUpDown.Value > this.trackBar.Maximum)
                this.trackBar.Maximum = (int)this.numericUpDown.Value;

            if (this.numericUpDown.Value == -1)
                this.trackBar.Value = 0;
            else
                this.trackBar.Value = (int)this.numericUpDown.Value;
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            this.numericUpDown.Value = this.trackBar.Value;
        }

        private void radioButtonBinSize_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBoxBinNumber.Enabled = false;
            this.groupBoxBinSize.Enabled = true;
        }

        private void radioButtonBinNumber_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBoxBinNumber.Enabled = true;
            this.groupBoxBinSize.Enabled = false;
        }

        private void radioButtonMinMaxAutomated_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = radioButtonMinMaxManual.Checked;
        }

        private void radioButtonMinMaxManual_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = radioButtonMinMaxManual.Checked;
        }

        private void numericUpDownMin_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownMin.Value > numericUpDownMax.Value) numericUpDownMin.Value = numericUpDownMax.Value;
        }

        private void numericUpDownMax_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownMin.Value > numericUpDownMax.Value) numericUpDownMin.Value = numericUpDownMax.Value;
        }


    }
}
