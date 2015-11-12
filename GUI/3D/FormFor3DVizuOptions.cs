using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HCSAnalyzer.Forms._3D
{
    public partial class FormFor3DVizuOptions : Form
    {

   //     public FormFor3DDataDisplay Parent = null;
        FormFor3DDataDisplay Parent;

        public FormFor3DVizuOptions(FormFor3DDataDisplay Parent)
        {
            InitializeComponent();
            this.Parent = Parent;
        }

        private void radioButtonLightAutomated_CheckedChanged(object sender, EventArgs e)
        {
            panelManualLight.Enabled = radioButtonLightManual.Checked;
        }

        private void radioButtonLightManual_CheckedChanged(object sender, EventArgs e)
        {
            panelManualLight.Enabled = radioButtonLightManual.Checked;
        }

        private void numericUpDownLightIntensity_ValueChanged(object sender, EventArgs e)
        {
            Parent.LightIntensity = (double)this.numericUpDownLightIntensity.Value;
            Parent.light.SetIntensity(Parent.LightIntensity);
            Parent.renderWindowControl1.Invalidate();
        }

        private void numericUpDownLightAmbient_ValueChanged(object sender, EventArgs e)
        {
            Parent.LightAmbient = (double)this.numericUpDownLightAmbient.Value;
            Parent.actorSpheres.GetProperty().SetAmbient(Parent.LightAmbient);
            Parent.renderWindowControl1.Invalidate();
        }

        private void numericUpDownLightDiffuse_ValueChanged(object sender, EventArgs e)
        {
            Parent.LightDiffuse = (double)this.numericUpDownLightDiffuse.Value;
            Parent.actorSpheres.GetProperty().SetDiffuse(Parent.LightDiffuse);
            Parent.renderWindowControl1.Invalidate();
        }

        private void numericUpDownLightSpecular_ValueChanged(object sender, EventArgs e)
        {
            Parent.LightSpecular = (double)this.numericUpDownLightSpecular.Value;
            Parent.actorSpheres.GetProperty().SetSpecular(Parent.LightSpecular);
            Parent.renderWindowControl1.Invalidate();
        }

        private void numericUpDownRadiusSphere_ValueChanged(object sender, EventArgs e)
        {
            Parent.RadiusSphere =   (double)this.numericUpDownRadiusSphere.Value / 100.0;
            Parent.SphereSource.SetRadius(Parent.RadiusSphere);
            Parent.renderWindowControl1.Invalidate();
        }

        private void numericUpDownSphereOpacity_ValueChanged(object sender, EventArgs e)
        {
            Parent.SphereOpacity = (double)this.numericUpDownSphereOpacity.Value;
            Parent.actorSpheres.GetProperty().SetOpacity(Parent.SphereOpacity);
            Parent.renderWindowControl1.Invalidate();
        }

    }
}
