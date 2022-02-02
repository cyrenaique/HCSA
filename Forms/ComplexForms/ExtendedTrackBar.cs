using HCSAnalyzer.Classes;
using System;
using System.Windows.Forms;

namespace HCSAnalyzer.Forms.ComplexForms
{
    public partial class ExtendedTrackBar : Form
    {
        public ExtendedTrackBar()
        {
            InitializeComponent();
        }



        private void buttonPlus_Click(object sender, EventArgs e)
        {
            if (trackBar.Value >= trackBar.Maximum)
            {
                trackBar.Value = trackBar.Maximum;
                return;
            }
            trackBar.Value += 10;
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            if (trackBar.Value <= trackBar.Minimum)
            {
                trackBar.Value = trackBar.Minimum;
                return;
            }
            trackBar.Value -= 10;
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownZoom.Value = trackBar.Value;

            if (cGlobalInfo.CurrentScreening == null) return;

            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
        }

        private void toolStripMenuItem100_Click(object sender, EventArgs e)
        {
            trackBar.Value = 100;
        }

        private void numericUpDownZoom_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownZoom.Value > trackBar.Maximum) numericUpDownZoom.Value = trackBar.Maximum;
            trackBar.Value = (int)numericUpDownZoom.Value;
        }


    }
}
