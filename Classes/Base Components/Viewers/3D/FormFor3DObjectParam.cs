using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes._3D;

namespace HCSAnalyzer.Classes.Base_Components.Viewers._3D
{
    public partial class FormFor3DObjectParam : Form
    {

        cObject3D Object3D;

        public FormFor3DObjectParam(cObject3D Object3D)
        {
            this.Object3D = Object3D;
            InitializeComponent();
            this.Text += " - " + Object3D.GetName();

            this.numericUpDownPosX.ValueChanged -= new EventHandler(numericUpDownPosX_ValueChanged);
            this.numericUpDownPosY.ValueChanged -= new EventHandler(numericUpDownPosY_ValueChanged);
            this.numericUpDownPosZ.ValueChanged -= new EventHandler(numericUpDownPosZ_ValueChanged);
            this.numericUpDownOpacity.ValueChanged -= new EventHandler(numericUpDownOpacity_ValueChanged);
            
            this.radioButtonSolid.CheckedChanged -= new EventHandler(radioButtonSolid_CheckedChanged);
            this.radioButtonWireFrame.CheckedChanged -= new EventHandler(radioButtonWireFrame_CheckedChanged);
            this.radioButtonPoint.CheckedChanged -= new EventHandler(radioButtonPoint_CheckedChanged);

            if (this.Object3D.GetMode() == eMesh3DMode.SOLID)
            {
                this.radioButtonSolid.Checked = true;
                this.radioButtonWireFrame.Checked = false;
                this.radioButtonPoint.Checked = false;
            }
            else if (this.Object3D.GetMode() == eMesh3DMode.WIREFRAME)
            {
                this.radioButtonSolid.Checked = false;
                this.radioButtonWireFrame.Checked = true;
                this.radioButtonPoint.Checked = false;
            }
            else
            {
                this.radioButtonSolid.Checked = false;
                this.radioButtonWireFrame.Checked = false;
                this.radioButtonPoint.Checked = true;
            }

            this.radioButtonSolid.CheckedChanged += new EventHandler(radioButtonSolid_CheckedChanged);
            this.radioButtonWireFrame.CheckedChanged += new EventHandler(radioButtonWireFrame_CheckedChanged);
            this.radioButtonPoint.CheckedChanged += new EventHandler(radioButtonPoint_CheckedChanged);
            
            this.numericUpDownPosX.Value = (decimal)Object3D.GetPosition().X;
            this.numericUpDownPosY.Value = (decimal)Object3D.GetPosition().Y;
            this.numericUpDownPosZ.Value = (decimal)Object3D.GetPosition().Z;
            this.numericUpDownOpacity.Value = (decimal)Object3D.GetOpacity();

            this.numericUpDownPosX.ValueChanged += new EventHandler(numericUpDownPosX_ValueChanged);
            this.numericUpDownPosY.ValueChanged += new EventHandler(numericUpDownPosY_ValueChanged);
            this.numericUpDownPosZ.ValueChanged += new EventHandler(numericUpDownPosZ_ValueChanged);
            this.numericUpDownOpacity.ValueChanged += new EventHandler(numericUpDownOpacity_ValueChanged);

            this.panelColor.BackColor = Object3D.Colour;


        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.numericUpDownPosX.Value = 0;
            this.numericUpDownPosY.Value = 0;
            this.numericUpDownPosZ.Value = 0;
        }

        private void numericUpDownPosX_ValueChanged(object sender, EventArgs e)
        {
            cPoint3D CurrentPos = Object3D.GetPosition();
            CurrentPos.X = (double)this.numericUpDownPosX.Value;
            Object3D.SetPosition(CurrentPos);
          //  Object3D.AssociatedWorld.Redraw();
        }

        private void numericUpDownPosY_ValueChanged(object sender, EventArgs e)
        {
            cPoint3D CurrentPos = Object3D.GetPosition();
            CurrentPos.Y = (double)this.numericUpDownPosY.Value;
            Object3D.SetPosition(CurrentPos);
        }

        private void numericUpDownPosZ_ValueChanged(object sender, EventArgs e)
        {
            cPoint3D CurrentPos = Object3D.GetPosition();
            CurrentPos.Z = (double)this.numericUpDownPosZ.Value;
            Object3D.SetPosition(CurrentPos);
        }

        private void numericUpDownOpacity_ValueChanged(object sender, EventArgs e)
        {
            Object3D.SetOpacity((double)this.numericUpDownOpacity.Value);
            Object3D.Redraw();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void panelColor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ColorDialog CD = new ColorDialog();
            if (CD.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            panelColor.BackColor = CD.Color;

            Object3D.SetColor(CD.Color);
            Object3D.Redraw();
        }

        private void radioButtonSolid_CheckedChanged(object sender, EventArgs e)
        {
            ChangeDisplayMode();
        }

        private void radioButtonWireFrame_CheckedChanged(object sender, EventArgs e)
        {
            ChangeDisplayMode();
        }

        private void radioButtonPoint_CheckedChanged(object sender, EventArgs e)
        {
            ChangeDisplayMode();
        }

        void ChangeDisplayMode()
        {
            if (radioButtonSolid.Checked)
                Object3D.SetToSurface();
            else if (radioButtonWireFrame.Checked)
                Object3D.SetToWireFrame();
            else
                Object3D.SetToPoints();

            Object3D.AssociatedWorld.AssociatedVTKRenderer.GetRenderWindow().GetInteractor().Render();
        }

    }
}
