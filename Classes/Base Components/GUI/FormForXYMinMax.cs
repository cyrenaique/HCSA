using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HCSAnalyzer.Classes.Base_Classes.GUI
{
    public partial class FormForXYMinMax : Form
    {
        public FormForXYMinMax()
        {
            InitializeComponent();
        }

        private void numericUpDownXMin_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownXMax.Value < numericUpDownXMin.Value) numericUpDownXMax.Value = numericUpDownXMin.Value;
        }

        private void numericUpDownXMax_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownXMax.Value < numericUpDownXMin.Value) numericUpDownXMax.Value = numericUpDownXMin.Value;
        }

        private void numericUpDownYMin_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownYMax.Value < numericUpDownYMin.Value) numericUpDownYMax.Value = numericUpDownYMin.Value;
        }

        private void numericUpDownYMax_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownYMax.Value < numericUpDownYMin.Value) numericUpDownYMax.Value = numericUpDownYMin.Value;
        }

        private void checkBoxXAutomated_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownXMax.Enabled = numericUpDownXMin.Enabled = !checkBoxXAutomated.Checked;
        }

        private void checkBoxYAutomated_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownYMax.Enabled = numericUpDownYMin.Enabled = !checkBoxYAutomated.Checked;
        }

        private void checkBoxXintervalAutomated_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownXInterval.Enabled = !checkBoxXintervalAutomated.Checked;
        }

        private void checkBoxYintervalAutomated_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownYInterval.Enabled = !checkBoxYintervalAutomated.Checked;
        }

    }
}
