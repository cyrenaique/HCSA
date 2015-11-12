using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes._3D;
using HCSAnalyzer.Classes.ImageAnalysis._3D_Engine;
using Kitware.VTK;

namespace HCSAnalyzer.Classes.Base_Components.Viewers._3D
{
    public partial class FormFor3DVolumeDisplayParam : Form
    {

        cVolumeRendering3D Object3D;

        public FormFor3DVolumeDisplayParam(cVolumeRendering3D Object3D)
        {
            this.Object3D = Object3D;
            InitializeComponent();
            this.Text += " - " + Object3D.GetName();

            this.numericUpDownPosX.ValueChanged -= new EventHandler(numericUpDownPosX_ValueChanged);
            this.numericUpDownPosY.ValueChanged -= new EventHandler(numericUpDownPosY_ValueChanged);
            this.numericUpDownPosZ.ValueChanged -= new EventHandler(numericUpDownPosZ_ValueChanged);
            this.numericUpDownOpacityMinPos.ValueChanged -= new EventHandler(numericUpDownOpacityMinPos_ValueChanged);
            this.numericUpDownOpacityMaxPos.ValueChanged -= new EventHandler(numericUpDownOpacityMaxPos_ValueChanged);
            this.numericUpDownOpacityMaxValue.ValueChanged -= new EventHandler(numericUpDownOpacityMaxValue_ValueChanged);
            this.numericUpDownOpacityMinValue.ValueChanged -= new EventHandler(numericUpDownOpacityMinValue_ValueChanged);
            
            this.numericUpDownColorMinPos.ValueChanged -= new EventHandler(numericUpDownColorMinPos_ValueChanged);
            this.numericUpDownColorMaxPos.ValueChanged -= new EventHandler(numericUpDownColorMaxPos_ValueChanged);
            

            //Object3D.opacityTransferFunction.GetValue();

            this.numericUpDownColorMinPos.Value = this.numericUpDownOpacityMinPos.Value = (decimal)Object3D.range[0];
            this.numericUpDownColorMaxPos.Value = this.numericUpDownOpacityMaxPos.Value = (decimal)Object3D.range[1];
            this.numericUpDownOpacityMaxValue.Value = (decimal)Object3D.opacityTransferFunction.GetValue(Object3D.range[1]);
            this.numericUpDownOpacityMinValue.Value = (decimal)Object3D.opacityTransferFunction.GetValue(Object3D.range[0]);


            this.panelColor.BackColor = Color.FromArgb((int)(Object3D.ColorTransferFunction.GetRedValue(Object3D.range[0]) * 255),
                                                  (int)(Object3D.ColorTransferFunction.GetGreenValue(Object3D.range[0]) * 255),
                                                  (int)(Object3D.ColorTransferFunction.GetBlueValue(Object3D.range[0]) * 255));

            this.panelColorLast.BackColor = Color.FromArgb((int)(Object3D.ColorTransferFunction.GetRedValue(Object3D.range[1]) * 255),
                                                  (int)(Object3D.ColorTransferFunction.GetGreenValue(Object3D.range[1]) * 255),
                                                  (int)(Object3D.ColorTransferFunction.GetBlueValue(Object3D.range[1]) * 255));

            //Object3D.C


            if (this.Object3D.volumeProperty.GetInterpolationTypeAsString() == "Nearest Neighbor")
            {
                this.radioButtonInterpolationNN.Checked = true;
                this.radioButtonInterpolationLinear.Checked = false;
            }
            else
            {
                this.radioButtonInterpolationNN.Checked = false;
                this.radioButtonInterpolationLinear.Checked = true;
            }
            //else if (this.Object3D.GetMode() == eMesh3DMode.WIREFRAME)
            //{
            //    this.radioButtonSolid.Checked = false;
            //    this.radioButtonWireFrame.Checked = true;
            //    this.radioButtonPoint.Checked = false;
            //}
            //else
            //{
            //    this.radioButtonSolid.Checked = false;
            //    this.radioButtonWireFrame.Checked = false;
            //    this.radioButtonPoint.Checked = true;
            //}

            //this.radioButtonSolid.CheckedChanged += new EventHandler(radioButtonSolid_CheckedChanged);
            //this.radioButtonWireFrame.CheckedChanged += new EventHandler(radioButtonWireFrame_CheckedChanged);
            //this.radioButtonPoint.CheckedChanged += new EventHandler(radioButtonPoint_CheckedChanged);

            this.numericUpDownPosX.Value = (decimal)Object3D.GetPosition().X;
            this.numericUpDownPosY.Value = (decimal)Object3D.GetPosition().Y;
            this.numericUpDownPosZ.Value = (decimal)Object3D.GetPosition().Z;
            //this.numericUpDownOpacity.Value = (decimal)Object3D.GetOpacity();

            this.numericUpDownPosX.ValueChanged += new EventHandler(numericUpDownPosX_ValueChanged);
            this.numericUpDownPosY.ValueChanged += new EventHandler(numericUpDownPosY_ValueChanged);
            this.numericUpDownPosZ.ValueChanged += new EventHandler(numericUpDownPosZ_ValueChanged);

            this.numericUpDownColorMinPos.ValueChanged -= new EventHandler(numericUpDownColorMinPos_ValueChanged);
            this.numericUpDownColorMaxPos.ValueChanged -= new EventHandler(numericUpDownColorMaxPos_ValueChanged);


            this.numericUpDownOpacityMinPos.ValueChanged += new EventHandler(numericUpDownOpacityMinPos_ValueChanged);
            this.numericUpDownOpacityMaxPos.ValueChanged += new EventHandler(numericUpDownOpacityMaxPos_ValueChanged);
            this.numericUpDownOpacityMaxValue.ValueChanged += new EventHandler(numericUpDownOpacityMaxValue_ValueChanged);
            this.numericUpDownOpacityMinValue.ValueChanged += new EventHandler(numericUpDownOpacityMinValue_ValueChanged);
            
            //this.panelColor.BackColor = Object3D.Colour;


        }

        private void numericUpDownPosX_ValueChanged(object sender, EventArgs e)
        {
            cPoint3D CurrentPos = Object3D.GetPosition();
            CurrentPos.X = (double)this.numericUpDownPosX.Value;
            Object3D.SetPosition(CurrentPos);

            Redraw();
        }

        private void numericUpDownPosY_ValueChanged(object sender, EventArgs e)
        {
            cPoint3D CurrentPos = Object3D.GetPosition();
            CurrentPos.Y = (double)this.numericUpDownPosY.Value;
            Object3D.SetPosition(CurrentPos);

            Redraw();
        }

        private void numericUpDownPosZ_ValueChanged(object sender, EventArgs e)
        {
            cPoint3D CurrentPos = Object3D.GetPosition();
            CurrentPos.Z = (double)this.numericUpDownPosZ.Value;
            Object3D.SetPosition(CurrentPos);

            Redraw();
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
            RefreshColor();
        }


        void RefreshColor()
        {
            Object3D.ColorTransferFunction.RemoveAllPoints();
//            Object3D.ColorTransferFunction = vtkColorTransferFunction.New();

            Object3D.ColorTransferFunction.AddRGBPoint((double)this.numericUpDownColorMinPos.Value, panelColor.BackColor.R / 255.0, panelColor.BackColor.G / 255.0, panelColor.BackColor.B / 255.0);
            Object3D.ColorTransferFunction.AddRGBPoint((double)this.numericUpDownColorMaxPos.Value, panelColorLast.BackColor.R / 255.0, panelColorLast.BackColor.G / 255.0, panelColorLast.BackColor.B / 255.0);

            Object3D.ColorTransferFunction.Build();

            //Object3D.vtk_volume.GetProperty().SetColor(colorTransferFunction);
            Object3D.Update();
            Redraw();
        }


        public void Redraw()
        {
            if (Object3D.AssociatedWorld != null) Object3D.AssociatedWorld.Redraw();
        }

        private void numericUpDownColorMinPos_ValueChanged(object sender, EventArgs e)
        {
            RefreshColor();
        }

        private void panelColorLast_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ColorDialog CD = new ColorDialog();
            if (CD.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            panelColorLast.BackColor = CD.Color;
            RefreshColor();
        }

        private void numericUpDownColorMaxPos_ValueChanged(object sender, EventArgs e)
        {
            RefreshColor();
        }

        private void radioButtonInterpolationNN_CheckedChanged(object sender, EventArgs e)
        {
            Object3D.volumeProperty.SetInterpolationTypeToNearest();
            Object3D.Update();
        }

        private void radioButtonInterpolationLinear_CheckedChanged(object sender, EventArgs e)
        {
            Object3D.volumeProperty.SetInterpolationTypeToLinear();
            Object3D.Update();
        }

        void RefreshOpacity()
        {

            Object3D.opacityTransferFunction = vtkPiecewiseFunction.New();

            Object3D.opacityTransferFunction.AddPoint((double)numericUpDownOpacityMinPos.Value, (double)numericUpDownOpacityMinValue.Value);
            Object3D.opacityTransferFunction.AddPoint((double)numericUpDownOpacityMaxPos.Value, (double)numericUpDownOpacityMaxValue.Value);
            Object3D.volumeProperty.SetScalarOpacity(Object3D.opacityTransferFunction);
            Object3D.Update();
            Redraw();
        }

        private void numericUpDownOpacityMinValue_ValueChanged(object sender, EventArgs e)
        {
            RefreshOpacity();
        }

        private void numericUpDownOpacityMaxValue_ValueChanged(object sender, EventArgs e)
        {
            RefreshOpacity();
        }

        private void numericUpDownOpacityMinPos_ValueChanged(object sender, EventArgs e)
        {
            RefreshOpacity();
        }

        private void numericUpDownOpacityMaxPos_ValueChanged(object sender, EventArgs e)
        {
            RefreshOpacity();
        }



    }
}
