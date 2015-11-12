using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Kitware.VTK;
using HCSAnalyzer.Classes._3D;
using ImageAnalysis;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.ImageAnalysis._3D_Engine
{
    public class cVolumeRendering3D
    {
        public cPoint3D Position;
        public vtkVolume vtk_volume;
        double[] range;
        public byte[][] LUT = null;

        public cVolumeRendering3D(cSingleChannelImage SingleChannelImage, cPoint3D Pos, byte[][] NewLUT)
        {

            this.LUT = NewLUT;

            vtk_volume = vtkVolume.New();
            vtk_volume.PickableOn();
            vtkImageData imageData = vtkImageData.New();
            vtkUnsignedShortArray UshortArray = vtkUnsignedShortArray.New();

            vtkExtractVOI voi = vtkExtractVOI.New();
            vtkPiecewiseFunction opacityTransferFunction = vtkPiecewiseFunction.New();
            vtkColorTransferFunction colorTransferFunction = vtkColorTransferFunction.New();
            vtkVolumeProperty volumeProperty = vtkVolumeProperty.New();

            for (int i = 0; i < SingleChannelImage.Width * SingleChannelImage.Height * SingleChannelImage.Depth; i++)
            {
                UshortArray.InsertTuple1(i, (ushort)SingleChannelImage.Data[i]);
            }

            imageData = vtkImageData.New();
            imageData.SetDimensions(SingleChannelImage.Width, SingleChannelImage.Height, SingleChannelImage.Depth);
            imageData.SetOrigin(0, 0, 0);

            if ((SingleChannelImage.Resolution.X == 0) || (SingleChannelImage.Resolution.Y == 0) || (SingleChannelImage.Resolution.Z == 0))
                imageData.SetSpacing(1.0, 1.0, 1.0);
            else
                imageData.SetSpacing(SingleChannelImage.Resolution.X, SingleChannelImage.Resolution.Y, SingleChannelImage.Resolution.Z);

            imageData.GetPointData().SetScalars(UshortArray);

            voi = vtkExtractVOI.New();
            voi.SetInput(imageData);
            voi.SetVOI(0, SingleChannelImage.Width - 1, 0, SingleChannelImage.Height - 1, 0, SingleChannelImage.Depth - 1);
            voi.SetSampleRate(1, 1, 1);

            opacityTransferFunction = vtkPiecewiseFunction.New();

            range = imageData.GetScalarRange();
            opacityTransferFunction.AddPoint(0, 0.0);
            opacityTransferFunction.AddPoint(255, 1);
            //opacityTransferFunction.AddPoint(range[0] + (range[1]-range[0])/2.0, 0.0);
            //opacityTransferFunction.AddPoint(range[1], 0.7);

            if (LUT == null)
            {
                cLUT MyLut = new cLUT();
                LUT = MyLut.LUT_JET;
            }

            double LUTSize = LUT[0].Length;

            for (int i = 0; i < (int)LUTSize - 1; i++)
            {
                colorTransferFunction.AddRGBPoint(i, this.LUT[0][i] / LUTSize, this.LUT[1][i] / LUTSize, this.LUT[2][i] / LUTSize);
            }

            colorTransferFunction.Build();

            volumeProperty = vtkVolumeProperty.New();
            volumeProperty.SetColor(colorTransferFunction);
            volumeProperty.SetScalarOpacity(opacityTransferFunction);
            volumeProperty.SetInterpolationTypeToLinear();
            volumeProperty.ShadeOff();
            volumeProperty.SetAmbient(0.6);
            volumeProperty.SetDiffuse(0.6);
            volumeProperty.SetSpecular(0.1);

            vtkVolumeTextureMapper3D volumeTextureMapper = vtkVolumeTextureMapper3D.New();
            volumeTextureMapper.SetInputConnection(voi.GetOutputPort());

            //vtkVolumeRayCastCompositeFunction compositeFunction = vtkVolumeRayCastCompositeFunction.New();
            //vtkVolumeRayCastMapper volumeMapper = vtkVolumeRayCastMapper.New();
            //volumeMapper.SetVolumeRayCastFunction(compositeFunction);
            //volumeMapper.SetInputConnection(voi.GetOutputPort());

            vtk_volume = vtkVolume.New();
            vtk_volume.SetMapper(volumeTextureMapper);
            vtk_volume.SetProperty(volumeProperty);
            vtk_volume.PickableOn();

            vtk_volume.SetPosition(Pos.X, Pos.Y, Pos.Z);
        }

        public cVolumeRendering3D(vtkImageData imageData, cPoint3D Pos)
        {
            vtk_volume = vtkVolume.New();
            // vtkFloatArray floatArray = vtkFloatArray.New();
            //vtkCharArray charArray = vtkCharArray.New();
          //  vtkUnsignedShortArray UshortArray = vtkUnsignedShortArray.New();

            vtkExtractVOI voi = vtkExtractVOI.New();
            vtkPiecewiseFunction opacityTransferFunction = vtkPiecewiseFunction.New();
            vtkColorTransferFunction colorTransferFunction = vtkColorTransferFunction.New();
            vtkVolumeProperty volumeProperty = vtkVolumeProperty.New();

           // imageData.GetPointData().SetScalars(UshortArray);

            voi = vtkExtractVOI.New();
            voi.SetInput(imageData);


            voi.SetVOI(0, imageData.GetDimensions()[0] - 1, 0, imageData.GetDimensions()[1] - 1, 0, imageData.GetDimensions()[2] - 1);
            voi.SetSampleRate(1, 1, 1);

            opacityTransferFunction = vtkPiecewiseFunction.New();

            range = imageData.GetScalarRange();
            opacityTransferFunction.AddPoint(100, 0.0);
            opacityTransferFunction.AddPoint(1000, 1);
            //opacityTransferFunction.AddPoint(range[0] + (range[1]-range[0])/2.0, 0.0);
            //opacityTransferFunction.AddPoint(range[1], 0.7);

            ////Scale the image between 0 and 1 using a lookup table 
            //vtkLookupTable table = vtkLookupTable.New(); 
            //table.SetValueRange(0,1); 
            //table.SetSaturationRange(0,0); 
            //table.SetRange(range[0], range[1]); //shoul here not be the minimum/maximum possible of "data"? 
            //table.SetRampToLinear(); 
            //table.Build(); 


            //vtkImageMapToColors color = vtkImageMapToColors.New(); 
            //color.SetLookupTable(table); 
            //color.SetInputConnection(imageData.GetProducerPort()); 

            vtkColorTransferFunction ColorTransferFunction = vtkColorTransferFunction.New();
            ColorTransferFunction.AddRGBPoint(20.0, 0.0, 0.0, 1.0);
            ColorTransferFunction.AddRGBPoint(255.0, 1.0, 0.0, 0.0);
            //ColorTransferFunction.AddRGBPoint(1000.0, 0.8, 0.5, 0.0);

            //opacityTransferFunction.ClampingOff();

            volumeProperty = vtkVolumeProperty.New();
            volumeProperty.SetColor(colorTransferFunction);
            volumeProperty.SetScalarOpacity(opacityTransferFunction);
            volumeProperty.SetInterpolationTypeToLinear();
            volumeProperty.ShadeOn();
            volumeProperty.SetAmbient(0.6);
            volumeProperty.SetDiffuse(0.6);
            volumeProperty.SetSpecular(0.1);

            //volumeProperty.SetAmbient(0.1);
            //volumeProperty.SetDiffuse(0.9);
            //volumeProperty.SetSpecular(0.2);
            //volumeProperty.SetSpecularPower(10.0);
            //volumeProperty[band].SetScalarOpacityUnitDistance(0.8919);

            vtkVolumeTextureMapper3D volumeTextureMapper = vtkVolumeTextureMapper3D.New();
            ////volumeTextureMapper.SetInputConnection(color.GetOutputPort());
            volumeTextureMapper.SetInputConnection(voi.GetOutputPort());


            //vtkVolumeRayCastCompositeFunction compositeFunction = vtkVolumeRayCastCompositeFunction.New();
            //vtkVolumeRayCastMapper volumeMapper = vtkVolumeRayCastMapper.New();
            //volumeMapper.SetVolumeRayCastFunction(compositeFunction);
            //volumeMapper.SetInputConnection(voi.GetOutputPort());

            vtk_volume = vtkVolume.New();
            vtk_volume.SetMapper(volumeTextureMapper);
            vtk_volume.SetProperty(volumeProperty);
            vtk_volume.PickableOff();

            vtk_volume.SetPosition(Pos.X, Pos.Y, Pos.Z);

        }

        public void AddMeToTheWorld(vtkRenderer World)
        {
            World.AddVolume(this.vtk_volume);
         //   vtkColorTransferFunction ColorTransferFunction = vtkColorTransferFunction.New();

            //ColorTransferFunction.AddRGBPoint(range[0] + (range[1] - range[0]) / 6.0, 0.0, 0.0, 0.1);
            //ColorTransferFunction.AddRGBPoint(range[1], 0.1, 0.6, 0.0);

          //  ColorTransferFunction.AddRGBPoint(0, FirstColor.R / 255.0, FirstColor.G / 255.0, FirstColor.B / 255.0);
         //   ColorTransferFunction.AddRGBPoint(255, LastColor.R / 255.0, LastColor.G / 255.0, LastColor.B / 255.0);
            //ColorTransferFunction.AddRGBPoint(/*range[1]*/3000, FirstColor.R / 255.0, FirstColor.G / 255.0, FirstColor.B / 255.0);



            //   ColorTransferFunction.AddRGBPoint(/*range[0]*/100, FirstColor.R / 255.0, FirstColor.G / 255.0, FirstColor.B / 255.0);
            //    ColorTransferFunction.AddRGBPoint(/*range[1]*/1000, LastColor.R / 255.0, LastColor.G / 255.0, LastColor.B / 255.0);


            //vtkPiecewiseFunction opacityTransferFunction = vtkPiecewiseFunction.New();
            //opacityTransferFunction.AddPoint(60, 0.0);
            //opacityTransferFunction.AddPoint(600, 0.1);
            //this.vtk_volume.GetProperty().SetScalarOpacity(opacityTransferFunction);
            //ColorTransferFunction.AddRGBPoint(0, 0.0, 0.0, 0);
            //ColorTransferFunction.AddRGBPoint(60, 1, 0.0, 0);
            //ColorTransferFunction.AddRGBPoint(120, 0.8, 0.8, 0);
            //ColorTransferFunction.AddRGBPoint(170, 0, 0.8, 0);
            //ColorTransferFunction.AddRGBPoint(210, 0, 0.8, 0.8);

            //ColorTransferFunction.AddRGBPoint(250, 0.8, 0.1, 0.0);



           // this.vtk_volume.GetProperty().SetColor(ColorTransferFunction);



            //World.Render();
        }

        //public void ChangeColor(Color StartingColor, double PositionStartingColor, Color EndingColor, double PositionEndingColor)
        //{
        //    vtkColorTransferFunction ColorTransferFunction = vtkColorTransferFunction.New();
        //    ColorTransferFunction.AddRGBPoint(PositionStartingColor, StartingColor.R / 255.0, StartingColor.G / 255.0, StartingColor.B / 255.0);
        //    ColorTransferFunction.AddRGBPoint(PositionEndingColor, EndingColor.R / 255.0, EndingColor.G / 255.0, EndingColor.B / 255.0);
        //    //ColorTransferFunction.AddRGBPoint(1000.0, 0.8, 0.5, 0.0);
        //    this.vtk_volume.GetProperty().SetColor(ColorTransferFunction);

        //    //opacityTransferFunction.ClampingOff();

        //    //volumeProperty = vtkVolumeProperty.New();
        //    //volumeProperty.SetColor(colorTransferFunction);
        //}

        public void ChangeOpacity(double StartingPt, double EndingPt, double OpacityMax)
        {
         //   vtkPiecewiseFunction opacityTransferFunction = vtkPiecewiseFunction.New();// this.vtk_volume.GetProperty().GetScalarOpacity();
         //   opacityTransferFunction.RemoveAllPoints();
            //MaxOpacity = Opacity;
            //if (MaxOpacity < 0) MaxOpacity = 0;
            //if (MaxOpacity > 1) MaxOpacity = 1;
       //     opacityTransferFunction.AddPoint(StartingPt, 0.0);
         //   opacityTransferFunction.AddPoint(EndingPt, OpacityMax);
          //  vtk_volume.GetProperty().SetScalarOpacity(opacityTransferFunction);

        //    return;
            cvtkPiecewiseFunctionBuilder vtkPiecewiseFunctionBuilder = new cvtkPiecewiseFunctionBuilder();
            vtkPiecewiseFunctionBuilder.ListProperties.FindByName("Max. Opacity").IsGUIforValue = true;


            cExtendedTable ET = new cExtendedTable(2,2,0);
            ET[0][0] = StartingPt;
            ET[1][0] = 0;
            ET[0][1] = EndingPt;
            ET[1][1] = OpacityMax;

            vtkPiecewiseFunctionBuilder.SetInputData(ET);
            if (vtkPiecewiseFunctionBuilder.Run().IsSucceed == false) return;

            vtk_volume.GetProperty().SetScalarOpacity(vtkPiecewiseFunctionBuilder.GetOutPut());
        }

        #region Text Display associated
        vtkFollower TextActor;
        vtkPolyDataMapper TextMapper;
        vtkVectorText TextVTK;

        public void AddText(String Text, vtkRenderer ren1, double scale)
        {
            TextActor = vtkFollower.New();
            TextMapper = vtkPolyDataMapper.New();
            TextVTK = vtkVectorText.New();

            TextVTK.SetText(Text);
            TextMapper.SetInputConnection(TextVTK.GetOutputPort());
            TextActor.SetMapper(TextMapper);
            TextActor.SetPosition(Position.X - 1, Position.Y - 1, Position.Z - 1);
            TextActor.SetScale(scale);
            TextActor.SetPickable(0);
            ren1.AddActor(TextActor);
            TextActor.SetCamera(ren1.GetActiveCamera());
        }
        #endregion
    }

}