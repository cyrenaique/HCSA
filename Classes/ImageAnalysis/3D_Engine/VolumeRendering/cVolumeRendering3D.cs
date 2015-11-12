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
using HCSAnalyzer.Classes.ImageAnalysis._3D_Engine.VolumeRendering;
using HCSAnalyzer.Classes.Base_Classes.Viewers._3D.ComplexObjects;
using HCSAnalyzer.Classes.Base_Components.Viewers._3D;

namespace HCSAnalyzer.Classes.ImageAnalysis._3D_Engine
{
    public class cVolumeRendering3D
    {
        cPoint3D Position;
        public vtkVolume vtk_volume;
        public double[] range;
        public byte[][] LUT = null;
        public vtkPiecewiseFunction opacityTransferFunction = vtkPiecewiseFunction.New();
        FormForVolumeRenderingOption WindowVolumeRenderingOption;
        public vtkVolumeProperty volumeProperty = vtkVolumeProperty.New();
        public c3DNewWorld AssociatedWorld = null;
        public cSingleChannelImage AssociatedImage = null;
        string Name;
        public vtkColorTransferFunction ColorTransferFunction;
        vtkExtractVOI voi;

        public cVolumeRendering3D(cSingleChannelImage SingleChannelImage, cPoint3D Pos, byte[][] NewLUT, c3DNewWorld AssociatedWorld)
        {

            if (SingleChannelImage.Data == null) return;

            baseInit(AssociatedWorld);

            this.SetName("Volume 3D [" + SingleChannelImage.Name + "]");

            this.LUT = NewLUT;

            vtk_volume = vtkVolume.New();
            vtkImageData imageData = vtkImageData.New();
            vtkUnsignedShortArray UshortArray = vtkUnsignedShortArray.New();

            voi = vtkExtractVOI.New();
            ColorTransferFunction = vtkColorTransferFunction.New();

            for (int i = 0; i < SingleChannelImage.Width * SingleChannelImage.Height * SingleChannelImage.Depth; i++)
            {
                UshortArray.InsertTuple1(i, (ushort)SingleChannelImage.Data[i]);    // data are converted to UShort
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



            opacityTransferFunction.AddPoint(range[0], 0.0);
            opacityTransferFunction.AddPoint(range[1], 0.3);
            //opacityTransferFunction.AddPoint(2000, 0.1);
            //opacityTransferFunction.AddPoint(range[0] + (range[1]-range[0])/2.0, 0.0);
            //opacityTransferFunction.AddPoint(range[1], 0.7);

            if (LUT == null)
            {
                //cLUT MyLut = new cLUT();
                //LUT = MyLut.LUT_JET;
                ColorTransferFunction.AddRGBPoint(0, 0 , 0, 0);
                ColorTransferFunction.AddRGBPoint(1, 1, 1, 1);
            }

            //double LUTSize = LUT[0].Length;

            //for (int i = 0; i < (int)LUTSize; i++)
            //{
            //    colorTransferFunction.AddRGBPoint(i, LUT[0][i] / LUTSize, LUT[1][i] / LUTSize, LUT[2][i] / LUTSize);
                
            //}

            ColorTransferFunction.Build();

            volumeProperty = vtkVolumeProperty.New();
            volumeProperty.SetColor(ColorTransferFunction);
        
            volumeProperty.SetScalarOpacity(opacityTransferFunction);
            volumeProperty.SetInterpolationTypeToNearest();
            volumeProperty.ShadeOff();

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
            baseInit(null);

            vtk_volume = vtkVolume.New();
            // vtkFloatArray floatArray = vtkFloatArray.New();
            //vtkCharArray charArray = vtkCharArray.New();
          //  vtkUnsignedShortArray UshortArray = vtkUnsignedShortArray.New();

            vtkExtractVOI voi = vtkExtractVOI.New();
          //  vtkPiecewiseFunction opacityTransferFunction = vtkPiecewiseFunction.New();
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
      
        public void SetPosition(cPoint3D Pos)
        {
            this.vtk_volume.SetPosition(Pos.X, Pos.Y, Pos.Z);
        }

        public cPoint3D GetPosition()
        {
            double[] P=  vtk_volume.GetPosition();
            return new cPoint3D(P[0], P[1], P[2]);
        }

        public string GetName()
        {
            return this.Name;
        }

        public void SetName(string Name)
        {
            this.Name = Name;
        }

        public void Update()
        {
            if (this.AssociatedWorld == null) return;
          //  this.AssociatedWorld.AssociatedVTKRenderer.Render();
            this.AssociatedWorld.AssociatedrenderWindow.RenderWindow.Render();
        }

        public void ShowWindowVolumeRenderingOption()
        {
            this.WindowVolumeRenderingOption.Visible = true;
          
        }

        public List<ToolStripMenuItem> GetExtendedContextMenu()
        {
            List<ToolStripMenuItem> ListToReturn = new List<ToolStripMenuItem>();

            ToolStripMenuItem MainMenu = new ToolStripMenuItem("[" + this.Name + "]");

            ToolStripMenuItem _3DObjectProperties_Opacity = new ToolStripMenuItem("Display Options");
            MainMenu.DropDownItems.Add(_3DObjectProperties_Opacity);
            _3DObjectProperties_Opacity.Click += new System.EventHandler(this._3DObjectProperties_Opacity);

            ToolStripMenuItem _3DObjectProperties_Segment = new ToolStripMenuItem("Segmentation");
            MainMenu.DropDownItems.Add(_3DObjectProperties_Segment);
            _3DObjectProperties_Segment.Click += new System.EventHandler(this._3DObjectProperties_Segment);





            //ToolStripMenuItem _3DObjectProperties_Color = new ToolStripMenuItem("Color");
            //MainMenu.DropDownItems.Add(_3DObjectProperties_Color);
            //_3DObjectProperties_Color.Click += new System.EventHandler(this._3DObjectProperties_Color);


            //ToolStripMenuItem ToolStripMenuItem_Position = new ToolStripMenuItem("Position");
            //ToolStripMenuItem_Position.Click += new System.EventHandler(this.ToolStripMenuItem_Position);
            //MainMenu.DropDownItems.Add(ToolStripMenuItem_Position);


            //MainMenu.DropDownItems.Add(new ToolStripSeparator());

            //ToolStripMenuItem _3DObjectProperties_Solid = new ToolStripMenuItem("Surface");
            //MainMenu.DropDownItems.Add(_3DObjectProperties_Solid);
            //_3DObjectProperties_Solid.Click += new System.EventHandler(this._3DObjectProperties_Solid);

            //ToolStripMenuItem _3DObjectProperties_WireFrame = new ToolStripMenuItem("WireFrame");
            //MainMenu.DropDownItems.Add(_3DObjectProperties_WireFrame);
            //_3DObjectProperties_WireFrame.Click += new System.EventHandler(this._3DObjectProperties_WireFrame);

            //ToolStripMenuItem _3DObjectProperties_Point = new ToolStripMenuItem("Point");
            //MainMenu.DropDownItems.Add(_3DObjectProperties_Point);
            //_3DObjectProperties_Point.Click += new System.EventHandler(this._3DObjectProperties_Point);

            //MainMenu.DropDownItems.Add(new ToolStripSeparator());

            //ToolStripMenuItem _3DObjectProperties_AddNotation = new ToolStripMenuItem("Add Notation");
            //MainMenu.DropDownItems.Add(_3DObjectProperties_AddNotation);
            //_3DObjectProperties_AddNotation.Click += new System.EventHandler(this._3DObjectProperties_AddNotation);

            //MainMenu.DropDownItems.Add(new ToolStripSeparator());

            //#region object info
            //ToolStripMenuItem _3DObjectProperties_INFO = new ToolStripMenuItem("Info");

            //ToolStripMenuItem _3DObjectProperties_GetVerticesList = new ToolStripMenuItem("Vertices List");

            //_3DObjectProperties_INFO.DropDownItems.Add(_3DObjectProperties_GetVerticesList);
            //_3DObjectProperties_GetVerticesList.Click += new System.EventHandler(this._3DObjectProperties_GetVerticesList);

            //_3DObjectProperties_INFO.DropDownItems.Add(new ToolStripSeparator());

            //ToolStripMenuItem _3DObjectProperties_GetInfo = new ToolStripMenuItem("Info");

            //_3DObjectProperties_INFO.DropDownItems.Add(_3DObjectProperties_GetInfo);
            //_3DObjectProperties_GetInfo.Click += new System.EventHandler(this._3DObjectProperties_GetInfo);

            //MainMenu.DropDownItems.Add(_3DObjectProperties_INFO);
          //  #endregion

            ListToReturn.Add(MainMenu);

            return ListToReturn;
        }

        void baseInit(c3DNewWorld AssociatedWorld)
        {
            this.WindowVolumeRenderingOption = new FormForVolumeRenderingOption(this);
            //this.WindowVolumeRenderingOption.Visible = false;
            this.WindowVolumeRenderingOption.Show();
            this.WindowVolumeRenderingOption.Visible = false;

            this.AssociatedWorld = AssociatedWorld;

            
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

        private void _3DObjectProperties_Opacity(object sender, EventArgs e)
        {
           // ChangeOpacity(100, 500, 0.1);

            FormFor3DVolumeDisplayParam _3DVolumeParam = new FormFor3DVolumeDisplayParam(this);
            _3DVolumeParam.Show();
        }

        private void _3DObjectProperties_Segment(object sender, EventArgs e)
        {  
            
            c3DObjectMeshFromImage MyMesh = new c3DObjectMeshFromImage();

            if (this.AssociatedImage == null)
            {
                MyMesh.SetInputData(this.voi, this.GetPosition());

                MyMesh.ListProperties.FindByName("Thresold").SetNewValue((double)150.0);
                MyMesh.ListProperties.FindByName("Thresold").PropertyType.Max = 65535;
                MyMesh.ListProperties.FindByName("Thresold").PropertyType.Min = 0;
                MyMesh.ListProperties.FindByName("Thresold").IsGUIforValue = true;

                MyMesh.ListProperties.FindByName("Split objects ?").SetNewValue((bool)false);

            }
            else
            {
                MyMesh.SetInputData(this.AssociatedImage);

                MyMesh.ListProperties.FindByName("Thresold").SetNewValue((double)150.0);
                MyMesh.ListProperties.FindByName("Thresold").PropertyType.Max = (double)this.AssociatedImage.Max;
                MyMesh.ListProperties.FindByName("Thresold").IsGUIforValue = true;

                MyMesh.ListProperties.FindByName("Split objects ?").SetNewValue((bool)false);
                MyMesh.ListProperties.FindByName("Split objects ?").IsGUIforValue = true;
            }

            MyMesh.Run(this.AssociatedWorld);
            this.AssociatedWorld.AddGeometric3DObjects(MyMesh.GetOutPut());
            this.AssociatedWorld.Redraw();

        
        }

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
            
            
            cExtendedTable ET = new cExtendedTable(2,2,0);
            ET[0][0] = StartingPt;
            //ET[1][0] = 0;
            ET[0][1] = EndingPt;
            //ET[1][1] = OpacityMax;
            
            cvtkPiecewiseFunctionBuilder vtkPiecewiseFunctionBuilder = new cvtkPiecewiseFunctionBuilder();
            vtkPiecewiseFunctionBuilder.ListProperties.FindByName("Min. Opacity").IsGUIforValue = true;
            vtkPiecewiseFunctionBuilder.ListProperties.FindByName("Min. Opacity").SetNewValue(0.0);

            vtkPiecewiseFunctionBuilder.ListProperties.FindByName("Max. Opacity").IsGUIforValue = true;
            vtkPiecewiseFunctionBuilder.ListProperties.FindByName("Max. Opacity").SetNewValue(0.5);




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