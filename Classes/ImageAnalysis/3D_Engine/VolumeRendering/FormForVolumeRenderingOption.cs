using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HCSAnalyzer.Classes.ImageAnalysis._3D_Engine.VolumeRendering
{
    public partial class FormForVolumeRenderingOption : Form
    {
        cVolumeRendering3D AssociatedVolume;

        public FormForVolumeRenderingOption(cVolumeRendering3D AssociatedVolume)
        {
            InitializeComponent();
            this.AssociatedVolume = AssociatedVolume;

        }

        void UpdateOpacity()
        {
            AssociatedVolume.opacityTransferFunction.RemoveAllPoints();
            AssociatedVolume.opacityTransferFunction.AddPoint((double)trackBarMinOpacityPos.Value, (double)numericUpDownMinOpacity.Value);
            AssociatedVolume.opacityTransferFunction.AddPoint((double)trackBarMaxOpacityPos.Value, (double)numericUpDownMaxOpacity.Value);
            AssociatedVolume.volumeProperty.SetScalarOpacity(AssociatedVolume.opacityTransferFunction);
            AssociatedVolume.Update();
           
        }

        private void trackBarMinOpacityPos_Scroll(object sender, EventArgs e)
        {
            if (trackBarMinOpacityPos.Value > trackBarMaxOpacityPos.Value)
                trackBarMinOpacityPos.Value = trackBarMaxOpacityPos.Value;

            UpdateOpacity();
        }

        private void trackBarMaxOpacityPos_Scroll(object sender, EventArgs e)
        {
            if (trackBarMinOpacityPos.Value > trackBarMaxOpacityPos.Value)
                trackBarMinOpacityPos.Value = trackBarMaxOpacityPos.Value;

            UpdateOpacity();
        }

        private void numericUpDownMinOpacity_ValueChanged(object sender, EventArgs e)
        {
            UpdateOpacity();
        }

        private void numericUpDownMaxOpacity_ValueChanged(object sender, EventArgs e)
        {
            UpdateOpacity();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void radioButtonInterpolationNN_CheckedChanged(object sender, EventArgs e)
        {
            AssociatedVolume.volumeProperty.SetInterpolationTypeToNearest();
            AssociatedVolume.Update();
        }

        private void radioButtonInterpolationLinear_CheckedChanged(object sender, EventArgs e)
        {
            AssociatedVolume.volumeProperty.SetInterpolationTypeToLinear();
            AssociatedVolume.Update();
        }

    }
}
