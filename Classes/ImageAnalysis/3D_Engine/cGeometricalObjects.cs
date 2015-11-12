using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Kitware.VTK;
using LibPlateAnalysis;
using HCSAnalyzer.Classes.Base_Classes.GUI;
using HCSAnalyzer.Classes.General_Types;
using ImageAnalysis;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.ImageAnalysis._3D_Engine;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.ImageAnalysis.FormsForImages;
using HCSAnalyzer.Classes.Base_Components.Viewers._3D;
using HCSAnalyzer.Forms.FormsForImages;
using HCSAnalyzer.Classes.ImageAnalysis._3D_Engine.Detection;

namespace HCSAnalyzer.Classes._3D
{
    public class cListGeometric3DObject : List<cGeometric3DObject>
    {
        public object Tag;
        public string Name = "cListGeometric3DObject";
        c3DNewWorld Associated3DWorld = null;

        public cListGeometric3DObject(string Name)
        {
            this.Name = Name;
        }

        public void AddObject(cGeometric3DObject ObjectToBeAdded)
        {
            ObjectToBeAdded.ParentTag = this;
            this.Add(ObjectToBeAdded);
        }

        public ToolStripMenuItem GetContextMenu(c3DNewWorld _3DWorld)
        {

            if (this.Count == 0) return null;

            this.Associated3DWorld = _3DWorld;

            ToolStripMenuItem SpecificContextMenu = new ToolStripMenuItem("List " + this.Count + " 3D_Objects");

            ToolStripMenuItem ToolStripMenuItem_Opacity = new ToolStripMenuItem("Opacity");
            ToolStripMenuItem_Opacity.Click += new System.EventHandler(this.ToolStripMenuItem_Opacity);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Opacity);


            ToolStripMenuItem ToolStripMenuItem_Color = new ToolStripMenuItem("Color");
            ToolStripMenuItem_Color.Click += new System.EventHandler(this.ToolStripMenuItem_Color);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Color);

            ToolStripMenuItem ToolStripMenuItem_Scale = new ToolStripMenuItem("Scale");
            ToolStripMenuItem_Scale.Click += new System.EventHandler(this.ToolStripMenuItem_Scale);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Scale);

            //SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Scale);


            SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem _3DObjectProperties_Solid = new ToolStripMenuItem("Surface");
            SpecificContextMenu.DropDownItems.Add(_3DObjectProperties_Solid);
            _3DObjectProperties_Solid.Click += new System.EventHandler(this._3DObjectProperties_Solid);

            ToolStripMenuItem _3DObjectProperties_WireFrame = new ToolStripMenuItem("WireFrame");
            SpecificContextMenu.DropDownItems.Add(_3DObjectProperties_WireFrame);
            _3DObjectProperties_WireFrame.Click += new System.EventHandler(this._3DObjectProperties_WireFrame);

            ToolStripMenuItem _3DObjectProperties_Points = new ToolStripMenuItem("Point");
            SpecificContextMenu.DropDownItems.Add(_3DObjectProperties_Points);
            _3DObjectProperties_Points.Click += new System.EventHandler(this._3DObjectProperties_Points);

            SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_BuildDensityMap = new ToolStripMenuItem("Build Density Map");
            ToolStripMenuItem_BuildDensityMap.Click += new System.EventHandler(this.ToolStripMenuItem_BuildDensityMap);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_BuildDensityMap);

            ToolStripMenuItem ToolStripMenuItem_FitEllipse = new ToolStripMenuItem("Fit Ellipse");
            ToolStripMenuItem_FitEllipse.Enabled = false;
            ToolStripMenuItem_FitEllipse.Click += new System.EventHandler(this.ToolStripMenuItem_FitEllipse);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_FitEllipse);

            ToolStripMenuItem ToolStripMenuItem_DisplayMenu = new ToolStripMenuItem("Display");

            ToolStripMenuItem ToolStripMenuItem_DispCentroid = new ToolStripMenuItem("Centroid");
            ToolStripMenuItem_DispCentroid.Click += new System.EventHandler(this.ToolStripMenuItem_DispCentroid);
            ToolStripMenuItem_DisplayMenu.DropDownItems.Add(ToolStripMenuItem_DispCentroid);

            ToolStripMenuItem ToolStripMenuItem_DispBoundingBox = new ToolStripMenuItem("Bounding Box");
            ToolStripMenuItem_DispBoundingBox.Click += new System.EventHandler(this.ToolStripMenuItem_DispBoundingBox);
            ToolStripMenuItem_DisplayMenu.DropDownItems.Add(ToolStripMenuItem_DispBoundingBox);

            ToolStripMenuItem ToolStripMenuItem_DispDelaunay = new ToolStripMenuItem("Delaunay Triangulation");
            ToolStripMenuItem_DispDelaunay.Click += new System.EventHandler(this.ToolStripMenuItem_DispDelaunay);
            ToolStripMenuItem_DisplayMenu.DropDownItems.Add(ToolStripMenuItem_DispDelaunay);


            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayMenu);



            ToolStripMenuItem ToolStripMenuItem_GetAssociatedDataTable = new ToolStripMenuItem("Get Associated Data Table");
            ToolStripMenuItem_GetAssociatedDataTable.Click += new System.EventHandler(this.ToolStripMenuItem_GetAssociatedDataTable);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_GetAssociatedDataTable);

            if (this.Tag != null)
            {
                if (this.Tag.GetType() == typeof(cListWells))
                {
                    SpecificContextMenu.DropDownItems.Add(((cListWells)this.Tag).GetContextMenu());
                }
                else if (this.Tag.GetType() == typeof(cPlate))
                {
                    SpecificContextMenu.DropDownItems.Add(((cPlate)this.Tag).GetContextMenu());
                }

            }


            return SpecificContextMenu;

        }

        private void ToolStripMenuItem_FitEllipse(object sender, EventArgs e)
        {
            cTableFrom3DObjects MyTable = new cTableFrom3DObjects();
            MyTable.SetInputData(this);
            MyTable.Run();


            cProjectorPCA PCA = new cProjectorPCA();
            PCA.SetInputData(MyTable.GetOutPut());
            cFeedBackMessage FM = PCA.Run();
            if (!FM.IsSucceed)
            {
                MessageBox.Show(FM.Message, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cDisplayExtendedTable DET = new cDisplayExtendedTable();
            DET.SetInputData(PCA.GetOutPut());
            DET.Run();

            c3DSphere NewSphere = new c3DSphere(GetCentroid(), 0.1);

            this.Associated3DWorld.AddGeometric3DObject(NewSphere);

            //if (vtk_Actor != null)
            // c
        }

        public cListPoints3D GetListCentroid()
        {
            cListPoints3D ToReturn = new cListPoints3D();

            foreach (var item in this)
            {
                ToReturn.Add(item.GetPosition());
            }


            return ToReturn;
        }

        public cPoint3D GetMinPoint()
        {
            cPoint3D ToReturn = new cPoint3D(double.MaxValue, double.MaxValue, double.MaxValue);

            foreach (var item in this)
            {
                cPoint3D Centroid = item.GetPosition();
                if (Centroid.X < ToReturn.X) ToReturn.X = Centroid.X;
                if (Centroid.Y < ToReturn.Y) ToReturn.Y = Centroid.Y;
                if (Centroid.Z < ToReturn.Z) ToReturn.Z = Centroid.Z;
            }

            return ToReturn;
        }

        public cPoint3D GetMaxPoint()
        {
            cPoint3D ToReturn = new cPoint3D(double.MinValue, double.MinValue, double.MinValue);

            foreach (var item in this)
            {
                cPoint3D Centroid = item.GetPosition();
                if (Centroid.X > ToReturn.X) ToReturn.X = Centroid.X;
                if (Centroid.Y > ToReturn.Y) ToReturn.Y = Centroid.Y;
                if (Centroid.Z > ToReturn.Z) ToReturn.Z = Centroid.Z;
            }

            return ToReturn;
        }

        public cPoint3D GetCentroid()
        {
            cPoint3D ToReturn = new cPoint3D(0, 0, 0);

            foreach (var item in this)
            {
                ToReturn += item.GetPosition();
            }

            ToReturn /= (double)this.Count;
            return ToReturn;
        }

        private void ToolStripMenuItem_DispCentroid(object sender, EventArgs e)
        {
            c3DSphere NewSphere = new c3DSphere(GetCentroid(), 0.1);
            NewSphere.SetName("Centroid [" + this.Name + "]");

            this.Associated3DWorld.AddGeometric3DObject(NewSphere);
        }

        private void ToolStripMenuItem_DispBoundingBox(object sender, EventArgs e)
        {
            c3DCube NewCube = new c3DCube();
            NewCube.Create(this.GetMinPoint(), this.GetMaxPoint(), Color.Red);
            NewCube.SetName("Bounding Box [" + this.Name + "]");

            this.Associated3DWorld.AddGeometric3DObject(NewCube);
        }

        private void ToolStripMenuItem_DispDelaunay(object sender, EventArgs e)
        {
            c2DDelaunay NewDelaunay = new c2DDelaunay(this.GetListCentroid(), true);
            NewDelaunay.SetName("Delaunay triangulation [" + this.Name + "]");
            this.Associated3DWorld.AddGeometric3DObject(NewDelaunay);
        }

        private void ToolStripMenuItem_BuildDensityMap(object sender, EventArgs e)
        {
            cTableFrom3DObjects MyTable = new cTableFrom3DObjects();
            MyTable.SetInputData(this);
            MyTable.Run();

            //cDisplayExtendedTable DET = new cDisplayExtendedTable();
            //DET.SetInputData(MyTable.GetOutPut());
            //DET.Run();

            cBuildDensityMap BDM = new cBuildDensityMap();
            BDM.SetInputData(new cListExtendedTable(MyTable.GetOutPut()));
            BDM.ListProperties.FindByName("Kernel size").SetNewValue((int)10);
            BDM.ListProperties.FindByName("Kernel size").IsGUIforValue = true;

            BDM.ListProperties.FindByName("Image width").SetNewValue((int)256);
            BDM.ListProperties.FindByName("Image width").IsGUIforValue = true;
            BDM.ListProperties.FindByName("Image height").SetNewValue((int)256);
            BDM.ListProperties.FindByName("Image height").IsGUIforValue = true;
            BDM.ListProperties.FindByName("Image depth").SetNewValue((int)256);
            BDM.ListProperties.FindByName("Image depth").IsGUIforValue = true;

            if (BDM.Run().IsSucceed == false) return;

            cVolumeRendering3D Vol = new cVolumeRendering3D(BDM.GetOutPut().SingleChannelImage[0],
                                    new cPoint3D(MyTable.GetOutPut()[0].Min(), MyTable.GetOutPut()[1].Min(), MyTable.GetOutPut()[2].Min()),
                                    null, Associated3DWorld);


            BDM.GetOutPut().Dispose();
            this.Associated3DWorld.AddVolume3D(Vol);

            //  cImageViewer IV = new cImageViewer();
            ////   IV.AssociatedImage
            //   IV.SetImage(BDM.GetOutPut());
            // IV.Display();
        }

        private void ToolStripMenuItem_GetAssociatedDataTable(object sender, EventArgs e)
        {
            cTableFrom3DObjects MyTable = new cTableFrom3DObjects();
            MyTable.SetInputData(this);
            MyTable.Run();

            cDisplayExtendedTable DET = new cDisplayExtendedTable();
            DET.SetInputData(MyTable.GetOutPut());
            DET.Run();

            // cBuildDensityMap BDM = new cBuildDensityMap();
            // BDM.SetInputData(new cListExtendedTable(MyTable.GetOutPut()));

            //// cVolumeRendering3D Vol = new cVolumeRendering3D(BDM.GetOutPut().SingleChannelImage[0],new cPoint3D(0,0,0), null);


            // cImageViewer IV = new cImageViewer();
            // //  IV.AssociatedImage
            // IV.SetImage(BDM.GetOutPut());
            // IV.Display();
        }

        private void ToolStripMenuItem_Color(object sender, EventArgs e)
        {
            ColorDialog ColorPicker = new ColorDialog();
            if (ColorPicker.ShowDialog() != DialogResult.OK) return;
            SetColor(ColorPicker.Color);

            this.Associated3DWorld.AssociatedrenderWindow.RenderWindow.Render();
        }

        private void ToolStripMenuItem_Opacity(object sender, EventArgs e)
        {
            FormForSingleSlider SliderForOpacity = new FormForSingleSlider("Object Opacity");
            SliderForOpacity.numericUpDown.Value = (decimal)(100.0);

            if (SliderForOpacity.ShowDialog() != DialogResult.OK) return;


            //Obj.SetOpacity((double)SliderForOpacity.numericUpDown.Value / 100.0);
            SetOpacity((double)SliderForOpacity.numericUpDown.Value / 100.0);
        }

        private void _3DObjectProperties_Points(object sender, EventArgs e)
        {
            SetToPoints();
        }

        private void _3DObjectProperties_WireFrame(object sender, EventArgs e)
        {
            SetToWireFrame();
        }

        private void _3DObjectProperties_Solid(object sender, EventArgs e)
        {
            SetToSurface();
        }

        private void ToolStripMenuItem_Scale(object sender, EventArgs e)
        {
            FormForSingleSlider SliderForScale = new FormForSingleSlider("Object Scale");
            SliderForScale.numericUpDown.Maximum = 1000;
            SliderForScale.numericUpDown.Value = (decimal)(1000);

            if (SliderForScale.ShowDialog() != DialogResult.OK) return;


            //Obj.SetOpacity((double)SliderForOpacity.numericUpDown.Value / 100.0);
            SetScale((double)SliderForScale.numericUpDown.Value / 100.0);
        }

        public void SetScale(double Scale)
        {
            foreach (var item in this)
            {

                item.SetScale(Scale);
            }
        }

        public void SetOpacity(double Opacity)
        {
            foreach (var item in this)
                item.SetOpacity(Opacity);
        }

        public void SetToPoints()
        {
            foreach (var item in this)
                item.SetToPoints();
        }

        public void SetToWireFrame()
        {
            foreach (var item in this)
                item.SetToWireFrame();
        }

        public void SetToSurface()
        {
            foreach (var item in this)
                item.SetToSurface();
        }

        public void SetColor(Color Colour)
        {
            foreach (var item in this)
                item.SetColor(Colour);
        }
    }

    public class cGeometric3DObject : cObject3D
    {
        protected vtkPolyData SourcePolyData = null;
        protected vtkFollower TextActorFollower = null;
        public cInteractive3DObject AssociatedObject = null;
        public vtkAlgorithmOutput AlgoOutPut = null;
        public bool IsStayInFrontOfCamera = false;
        public object ParentTag = null;

        public cGeometric3DObject()
        {
            vtk_Actor.SetPickable(1);
            ObjectType = "Geometrical";
        }

        public int GetHashCode()
        {
            return -1;
        }

        public cGeometric3DObject(vtkActor CurrentActor)
        {
            vtk_Actor = CurrentActor;
        }

        public cGeometric3DObject(vtkActor CurrentActor, Color NewColor)
        {
            CurrentActor.GetProperty().SetColor(NewColor.R / 255.0, NewColor.G / 255.0, NewColor.B / 255.0);
            vtk_Actor = CurrentActor;
            vtk_Actor.SetPickable(1);
            ObjectType = "Geometrical";
        }

        public cListPoints3D GetListVertex()
        {
            if (SourcePolyData == null) return null;

            cListPoints3D ToReturn = new cListPoints3D();

            int NumPts = (int)SourcePolyData.GetPointData().GetNumberOfTuples();

            for (int i = 0; i < NumPts; i++)
            {
                double[] Pt = SourcePolyData.GetPoint(i);
                ToReturn.Add(new cPoint3D(Pt[0], Pt[1], Pt[2]));
            }

            return ToReturn;
        }

        public cExtendedTable GetInfo()
        {
            if (SourcePolyData == null) return null;

            cExtendedTable ToReturn = new cExtendedTable();
            ToReturn.Name = GetName();
            ToReturn.Add(new cExtendedList("Properties"));
            ToReturn.ListRowNames = new List<string>();

            ToReturn.ListRowNames.Add("Vertices");
            ToReturn[0].Add(SourcePolyData.GetPointData().GetNumberOfTuples());

            //   SourcePolyData.Update();

            double[] Centroid = SourcePolyData.GetCenter();
            ToReturn.ListRowNames.Add("Centroid X");
            ToReturn[0].Add(Centroid[0]);
            ToReturn.ListRowNames.Add("Centroid Y");
            ToReturn[0].Add(Centroid[1]);
            ToReturn.ListRowNames.Add("Centroid Z");
            ToReturn[0].Add(Centroid[2]);

            ToReturn.ListRowNames.Add("Cells");
            ToReturn[0].Add(SourcePolyData.GetPolys().GetNumberOfCells());



            return ToReturn;
        }
    }

    public class c3DMeshObject : cGeometric3DObject
    {
        // int AutomatedPtColorMode = 0;

        public cMeshSmoother MeshSmoother = null;
        public cContainer Containers = null;

        private vtkPolyData vtk_PolyData;

        //  public HCSAnalyzer.Classes._3D.cMetaBiologicalObject.cInformation Information;
        private vtkHull hullFilter;
        vtkActor hullActor;
        public ConnectedVoxels AssociatedConnectedComponent;
        cSingleChannelImage GreyLevelImage = null;
        vtkExtractVOI voi = null;
        double Threshold;

        public bool Detected = true;

        public c3DMeshObject(vtkExtractVOI VTK_VOI, int Threshold)
        {
            this.voi = VTK_VOI;
            this.Threshold = Threshold;
        }

        public c3DMeshObject(cSingleChannelImage GreyLevelImage, double Threshold)
        {
            //  this.AutomatedPtColorMode = AutomatedPtColorMode;
            //Generate(GreyLevelImage, Threshold, Colour, Pos);
            this.GreyLevelImage = GreyLevelImage;
            this.Threshold = Threshold;
        }

        public void Create(Color Colour, cPoint3D Pos)
        {
            // default name
            

            if (voi == null)
            {
                  this.SetName("T_" + Threshold + " [" + GreyLevelImage.Name + "]");
                vtkImageData ImageData1 = new vtkImageData();

                ImageData1.SetDimensions(GreyLevelImage.Width, GreyLevelImage.Height, GreyLevelImage.Depth);
                ImageData1.SetNumberOfScalarComponents(1);
                ImageData1.SetSpacing(GreyLevelImage.Resolution.X, GreyLevelImage.Resolution.Y, GreyLevelImage.Resolution.Z);
                ImageData1.SetScalarTypeToFloat();

                vtkFloatArray array1 = new vtkFloatArray();
                for (int i = 0; i < GreyLevelImage.Width * GreyLevelImage.Height * GreyLevelImage.Depth; i++)
                    array1.InsertTuple1(i, GreyLevelImage.Data[i]);
                ImageData1.GetPointData().SetScalars(array1);

                this.voi = new vtkExtractVOI();
                voi.SetInput(ImageData1);
                voi.SetSampleRate(1, 1, 1);
                voi.SetVOI(0, GreyLevelImage.Width - 1, 0, GreyLevelImage.Height - 1, 0, GreyLevelImage.Depth - 1);
            }
            else
            { 
                this.SetName("T_" + Threshold + " [ VTK VOI ]");
             
            }

            vtkMarchingCubes ContourObject = vtkMarchingCubes.New();
            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            //ContourActor = new vtkActor();

            ContourObject.SetInput(voi.GetOutput());
            ContourObject.SetValue(0, Threshold);

            vtkAlgorithmOutput AlgoOutPut = null;

            if (MeshSmoother != null)
            {
                //vtkSmoothPolyDataFilter smoother = new vtkSmoothPolyDataFilter();
                vtkWindowedSincPolyDataFilter smoother = new vtkWindowedSincPolyDataFilter();
                smoother.SetInputConnection(ContourObject.GetOutputPort());// [deci GetOutputPort]
                smoother.SetNumberOfIterations(MeshSmoother.NumberOfIterations);
                vtk_PolyData = smoother.GetOutput();
                //smoother.GetOutputPort();
                AlgoOutPut = smoother.GetOutputPort();
            }
            else
            {
                vtk_PolyData = ContourObject.GetOutput();
                AlgoOutPut = ContourObject.GetOutputPort();
            }


            vtk_PolyDataMapper.SetInput(vtk_PolyData);
            vtk_PolyDataMapper.ScalarVisibilityOn();
            vtk_PolyDataMapper.SetScalarModeToUseFieldData();


            vtkActor TmpActor = vtkActor.New();
            TmpActor.SetMapper(vtk_PolyDataMapper);
            TmpActor.SetPosition(Pos.X, Pos.Y, Pos.Z);

            //Console.WriteLine("PosX"+Pos.X+" PosY"+Pos.Y+" PosZ"+Pos.Z);



            #region deal with the containers

            if (this.Containers != null)
            {
                if (this.Containers.ContainerMode == 0)
                {
                    cPoint3D Centroid = new cPoint3D((float)TmpActor.GetCenter()[0], (float)TmpActor.GetCenter()[1], (float)TmpActor.GetCenter()[2]);
                    bool IsInside = false;

                    for (int Idx = 0; Idx < Containers.Containers.Count; Idx++)
                    {
                        cBiological3DVolume CurrentContainer = (cBiological3DVolume)(Containers.Containers[Idx]);

                        if (CurrentContainer.IsPointInside(Centroid))
                        {
                            IsInside = true;
                            // ContainerIdx = Idx;
                            break;
                        }
                    }
                    if (IsInside)
                    {
                        this.SetPosition(new cPoint3D(Pos.X, Pos.Y, Pos.Z));
                        this.Colour = Colour;
                        CreateVTK3DObject(1);
                        //   vtk_PolyData = ContourObject.GetOutput();

                        // compute convex hull                
                        hullFilter = vtkHull.New();
                        hullFilter.SetInputConnection(AlgoOutPut);
                        hullFilter.AddRecursiveSpherePlanes(0);
                        hullFilter.Update();

                        //   Information = new HCSAnalyzer.Classes._3D.cMetaBiologicalObject.cInformation(AlgoOutPut, this, hullFilter);
                        this.Detected = true;
                    }
                    else
                    {
                        this.Detected = false;
                    }
                }
                else if (Containers.ContainerMode == 1)
                {
                    this.Detected = true;
                    //bool IsInside = false;
                    for (int Idx = 0; Idx < Containers.Containers.Count; Idx++)
                    {
                        cBiological3DVolume CurrentContainer = (cBiological3DVolume)(Containers.Containers[Idx]);

                        if (CurrentContainer.IsPointInside(Pos))
                        {
                            //IsInside = false;
                            this.Detected = false;
                            return;
                        }
                    }
                    this.SetPosition(new cPoint3D(Pos.X, Pos.Y, Pos.Z));
                    this.Colour = Colour;

                    CreateVTK3DObject(1);
                    // vtk_PolyData = ContourObject.GetOutput();

                    // compute convex hull                
                    //hullFilter = vtkHull.New();
                    //hullFilter.SetInputConnection(AlgoOutPut);
                    //hullFilter.AddRecursiveSpherePlanes(0);
                    //hullFilter.Update();

                    //Information = new HCSAnalyzer.Classes._3D.cMetaBiologicalObject.cInformation(AlgoOutPut, this, hullFilter);
                    this.Detected = true;
                }
            }
            else
            {
                this.SetPosition(new cPoint3D(Pos.X, Pos.Y, Pos.Z));
                this.Colour = Colour;

                CreateVTK3DObject(1);

                //  vtk_PolyData = ContourObject.GetOutput();


                // compute convex hull                
                //hullFilter = vtkHull.New();
                //hullFilter.SetInputConnection(AlgoOutPut);
                //hullFilter.AddRecursiveSpherePlanes(1);
                //hullFilter.Update();

                //  this.BackfaceCulling(false);
                //Information = new cInformation(AlgoOutPut, this, hullFilter);




            }

            #endregion



        }



    }

    /// <summary>
    /// 3D point cloud
    /// </summary>
    public class c3DDRC_Curve : cGeometric3DObject
    {
        int AutomatedPtColorMode = 0;
        public List<double> ListMin = null;
        public List<double> ListMax = null;
        public List<cPoint3D> ListAveragePts = null;
        cListExtendedTable ET = null;

        /// <summary>
        /// Define the B-Spline interpolation strenght: 1 <=> no interpolation
        /// </summary>
        public int SplineFactor = 100;


        public c3DDRC_Curve(cListExtendedTable ET)
        {
            this.ET = ET;
        }

        public c3DDRC_Curve(cListExtendedTable ET, List<double> ListMin, List<double> ListMax)
        {
            this.ET = ET;

            this.ListMin = new List<double>();
            this.ListMin.AddRange(ListMin);

            this.ListMax = new List<double>();
            this.ListMax.AddRange(ListMax);
        }

        public void Create(cPoint3D Center, Color Color)
        {
            Run(Center, Color);
        }

        void Run(cPoint3D Center, Color Color)
        {
            if (ET.Count == 0) return;


            this.SetColor(Color);
            this.Tag = ET.Tag;
            // this.SetName("DRC 3D");
            this.SetPosition(new cPoint3D(Center.X, Center.Y, Center.Z));

            double vX, vY, vZ;
            int NumberOfPt = ET.Count;
            int nTv = 8; // No. of surface elements for each tube vertex

            int i;

            // Create points and cells for the spiral
            vtkPoints points = vtkPoints.New();

            double[] ListRadii = new double[NumberOfPt];

            vtkPoints PtsForRadii = vtkPoints.New();
            // vtkPoints PtsForAlpha = vtkPoints.New();


            ListAveragePts = new List<cPoint3D>();


            for (i = 0; i < NumberOfPt; i++)
            {
                // double CurrentRadius = 0;

                // this is where I'll extract the coordinates of the DRC pts

                // first compute the center pt
                vX = ET[i][0].Mean();
                vY = ET[i][1].Mean();
                vZ = ET[i][2].Mean();


                cPoint3D CenterPt = new cPoint3D(vX, vY, vZ);
                CenterPt.Tag = ET[i];
                cExtendedList DistToCenter = new cExtendedList();
                cExtendedList NumCells = new cExtendedList();

                cListWells TmpWellList = (cListWells)ET[i].Tag;
                int NumObj = 0;
                foreach (cWell item in TmpWellList)
                {
                    NumObj = item.GetNumBiologicalObjects();
                }

                //double Alpha = 1;
                //if (NumObj > 0)
                //{
                //    Alpha = (NumObj - 100)/100.0;
                //    if (Alpha > 1) Alpha = 1;
                //    else if (Alpha < 0) Alpha = 0;
                //}
                //PtsForAlpha.InsertPoint(i, Alpha, 0, 0);

                
                if ((ListMin != null) && (ListMax != null))
                {
                    CenterPt.X = (CenterPt.X - ListMin[0]) / (ListMax[0] - ListMin[0]);
                    CenterPt.Y = (CenterPt.Y - ListMin[1]) / (ListMax[1] - ListMin[1]);
                    CenterPt.Z = (CenterPt.Z - ListMin[2]) / (ListMax[2] - ListMin[2]);



                    for (int iDxPt = 1; iDxPt < ET[i][0].Count; iDxPt++)
                    {

                        DistToCenter.Add(CenterPt.DistTo(new cPoint3D((ET[i][0][iDxPt] - ListMin[0]) / (ListMax[0] - ListMin[0]),
                            (ET[i][1][iDxPt] - ListMin[1]) / (ListMax[1] - ListMin[1]),
                            (ET[i][2][iDxPt]  - ListMin[2]) / (ListMax[2] - ListMin[2]) )));
                    }



                }
                else
                {

                    for (int iDxPt = 1; iDxPt < ET[i][0].Count; iDxPt++)
                    {
                        DistToCenter.Add(CenterPt.DistTo(new cPoint3D(ET[i][0][iDxPt], ET[i][1][iDxPt], ET[i][2][iDxPt])));
                    }
                }

                ListRadii[i] = DistToCenter.Mean()/20;
                //ListRadii[i] = 2;

                // normalize the data if required
                if ((ListMin != null) && (ListMax != null))
                {
                    vX = (vX - ListMin[0]) / (ListMax[0] - ListMin[0]);
                    vY = (vY - ListMin[1]) / (ListMax[1] - ListMin[1]);
                    vZ = (vZ - ListMin[2]) / (ListMax[2] - ListMin[2]);
                }
                points.InsertPoint(i, vX, vY, vZ);

                this.ListAveragePts.Add(new cPoint3D(vX, vY, vZ));
                PtsForRadii.InsertPoint(i, ListRadii[i], 0, 0);

            }



            vtkParametricSpline splineForRadii = vtkParametricSpline.New();
            splineForRadii.SetPoints(PtsForRadii);
            vtkParametricFunctionSource functionSourceForRadii = vtkParametricFunctionSource.New();
            functionSourceForRadii.SetParametricFunction(splineForRadii);
            functionSourceForRadii.SetUResolution((int)SplineFactor * (int)PtsForRadii.GetNumberOfPoints());
            functionSourceForRadii.Update();

            //vtkParametricSpline splineForAlpha = vtkParametricSpline.New();
            //splineForAlpha.SetPoints(PtsForAlpha);
            //vtkParametricFunctionSource functionSourceForAlpha = vtkParametricFunctionSource.New();
            //functionSourceForAlpha.SetParametricFunction(splineForAlpha);
            //functionSourceForAlpha.SetUResolution(SplineFactor * PtsForAlpha.GetNumberOfPoints());
            //functionSourceForAlpha.Update();
            //vtkPoints ListInterpolatedAlphaPts = functionSourceForAlpha.GetOutput().GetPoints();


            vtkParametricSpline spline = vtkParametricSpline.New();
            spline.SetPoints(points);
            vtkParametricFunctionSource functionSource = vtkParametricFunctionSource.New();
            functionSource.SetParametricFunction(spline);
            functionSource.SetUResolution((int)SplineFactor * (int)points.GetNumberOfPoints());
            functionSource.Update();

            int n = (int) functionSource.GetOutput().GetNumberOfPoints();

            NumberOfPt = n;

            vtkCellArray lines = vtkCellArray.New();
            lines.InsertNextCell(NumberOfPt);
            for (i = 0; i < NumberOfPt; i++)
            {
                lines.InsertCellPoint(i);
            }

            //base.SourcePolyData = vtkPolyData.New();
            //base.SourcePolyData.SetPoints(points);
            //base.SourcePolyData.SetLines(lines);


            base.SourcePolyData = functionSource.GetOutput();

            // Varying tube radius using sine-function
            vtkDoubleArray tubeRadius = vtkDoubleArray.New();
            tubeRadius.SetName("TubeRadius");
            tubeRadius.SetNumberOfTuples(NumberOfPt);

            vtkPoints ListInterpolatedRdiiPts = functionSourceForRadii.GetOutput().GetPoints();
            for (i = 0; i < NumberOfPt; i++)
            {
                double TmpRadius = ListInterpolatedRdiiPts.GetPoint(i)[0];
                tubeRadius.SetTuple1(i, TmpRadius);
            }
            base.SourcePolyData.GetPointData().AddArray(tubeRadius);
            base.SourcePolyData.GetPointData().SetActiveScalars("TubeRadius");

            //RBG array (could add Alpha channel too I guess...)
            vtkUnsignedCharArray colors = vtkUnsignedCharArray.New();
            colors.SetName("Colors");
            colors.SetNumberOfComponents(3);
            colors.SetNumberOfTuples(NumberOfPt);
            for (i = 0; i < NumberOfPt; i++)
            {
                //colors.InsertTuple4(i,Color.R,Color.R,Color.B,i);
                colors.InsertTuple3(i, Color.R, Color.R, Color.B);
            }
            base.SourcePolyData.GetPointData().AddArray(colors);

            vtkTubeFilter tube = vtkTubeFilter.New();

            tube.SetInput(base.SourcePolyData);
            tube.SetNumberOfSides(nTv);
            tube.SetVaryRadiusToVaryRadiusByAbsoluteScalar();


            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            vtk_PolyDataMapper.SetInputConnection(tube.GetOutputPort());
            vtk_PolyDataMapper.ScalarVisibilityOn();
            vtk_PolyDataMapper.SetScalarModeToUsePointFieldData();
            // vtk_PolyDataMapper.SelectColorArray("Colors");

            this.vtk_Actor = vtkActor.New();
            this.vtk_Actor.SetMapper(vtk_PolyDataMapper);


            return;
        }
    }

    /// <summary>
    /// 3D point cloud
    /// </summary>
    public class c3DPointCloud : cGeometric3DObject
    {
        public int AutomatedPtColorMode = 0;
        public float PtSize = 10;

        cExtendedTable ET;

        public c3DPointCloud(cExtendedTable ET)
        {
            this.ET = ET;

        }

        public void Create(cPoint3D Center)
        {
            if (ET.Count == 0) return;

            this.Tag = ET;
            this.SetName("Point Cloud [" + ET.Name + "]");
            this.SetPosition(new cPoint3D(Center.X, Center.Y, Center.Z));

            vtkPoints points = vtkPoints.New();

            if (ET.Count == 1)
                for (int i = 0; i < ET[0].Count; i++) points.InsertNextPoint(ET[0][i], 0, 0);
            else if (ET.Count == 2)
                for (int i = 0; i < ET[0].Count; i++) points.InsertNextPoint(ET[0][i], ET[1][i], 0);
            else
                for (int i = 0; i < ET[0].Count; i++) points.InsertNextPoint(ET[0][i], ET[1][i], ET[2][i]);


            base.SourcePolyData = vtkPolyData.New();
            base.SourcePolyData.SetPoints(points);

            vtkVertexGlyphFilter vertexFilter = vtkVertexGlyphFilter.New();

            vertexFilter.SetInput(base.SourcePolyData);

            vertexFilter.Update();

            vtkPolyData polydata = vtkPolyData.New();
            polydata.ShallowCopy(vertexFilter.GetOutput());

            vtkUnsignedCharArray colors = vtkUnsignedCharArray.New();
            colors.SetNumberOfComponents(3);
            colors.SetName("Colors");

            Color TmpColor;

            if ((ET.ListTags != null) && (ET.ListTags.Count == ET[0].Count))
            {
                for (int i = 0; i < ET[0].Count; i++)
                {
                    if (ET.ListTags[i].GetType() == typeof(cWell))
                    {
                        TmpColor = ((cWell)ET.ListTags[i]).GetClassColor();
                        colors.InsertNextTuple3(TmpColor.R, TmpColor.G, TmpColor.B);
                    }
                    else if (ET.ListTags[i].GetType() == typeof(cSingleBiologicalObject))
                    {
                        if (this.AutomatedPtColorMode == 0)
                        {
                            TmpColor = ((cSingleBiologicalObject)ET.ListTags[i]).GetColor();
                        }
                        else if (this.AutomatedPtColorMode == 1)
                        {
                            TmpColor = ((cSingleBiologicalObject)ET.ListTags[i]).AssociatedWell.GetClassColor();
                        }
                        else
                        {
                            TmpColor = Color.Gray;
                        }

                        colors.InsertNextTuple3(TmpColor.R, TmpColor.G, TmpColor.B);
                    }
                }
            }
            else
            {
                for (int i = 0; i < ET[0].Count; i++)
                {
                    colors.InsertNextTuple3(255, 255, 255);
                }
            }
            polydata.GetPointData().SetScalars(colors);

            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            vtk_PolyDataMapper.SetInput(polydata);

            CreateVTK3DObject(0);
            base.vtk_Actor.GetProperty().SetPointSize(PtSize);
            base.vtk_Actor.GetProperty().SetOpacity(0.99); ;
        }
    }

    /// <summary>
    /// 3D point cloud
    /// </summary>
    public class c3DElevationMap : cGeometric3DObject
    {
        //public byte[][] LUT = null;
        public vtkPoints points;

        public c3DElevationMap(cPoint3D Center, cImage InputImage, int Channel, int Zpos)
        {
            BuildElevationMapObject(Center, new cExtendedTable(InputImage, Channel, Zpos), InputImage.AssociatedImagePanel.LUTManager.GetLUTS()[Channel]);
        }

        void BuildElevationMapObject(cPoint3D Center, cExtendedTable Input, byte[][] LUT)
        {
            this.Tag = Input;
            this.SetPosition(new cPoint3D(Center.X, Center.Y, Center.Z));

            points = vtkPoints.New();

            double MaxZ = Input.Max();
            double MinZ = Input.Min();


            for (int IdxX = 0; IdxX < Input.Count; IdxX++)
                for (int IdxY = 0; IdxY < Input[IdxX].Count; IdxY++)
                {
                    double Value = ((Input[IdxX][Input[IdxX].Count - IdxY - 1] - MinZ) / (MaxZ - MinZ) * Input.Count) / 5;
                    points.InsertNextPoint(IdxX, IdxY, Value);
                }

            double[] bounds = points.GetBounds();

            // Add the grid points to a polydata object
            vtkPolyData inputPolyData = vtkPolyData.New();
            inputPolyData.SetPoints(points);

            // Triangulate the grid points
            vtkDelaunay2D delaunay = vtkDelaunay2D.New();
            delaunay.SetInput(inputPolyData);
            delaunay.Update();

            vtkElevationFilter elevationFilter = vtkElevationFilter.New();
            elevationFilter.SetInputConnection(delaunay.GetOutputPort());
            elevationFilter.SetLowPoint(0.0, 0.0, bounds[5]);
            elevationFilter.SetHighPoint(0.0, 0.0, bounds[4]);
            elevationFilter.Update();

            base.SourcePolyData = vtkPolyData.New();
            base.SourcePolyData.ShallowCopy(vtkPolyData.SafeDownCast(elevationFilter.GetOutput()));

            // Generate the colors for each point based on the color map
            vtkUnsignedCharArray colors = vtkUnsignedCharArray.New();
            colors.SetNumberOfComponents(3);
            colors.SetName("Colors");
            base.SourcePolyData.GetPointData().AddArray(colors);

            // Visualize
            //vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            base.vtk_PolyDataMapper = vtkPolyDataMapper.New();
            base.vtk_PolyDataMapper.SetInput(base.SourcePolyData);


            // Create the color map
            vtkLookupTable colorLookupTable = vtkLookupTable.New();
         //   colorLookupTable.Build();

            // no LUT then build one
            if (LUT == null)
            {
                cLUT MyLut = new cLUT();
                LUT = MyLut.LUT_JET;
            }

            double LUTSize = LUT[0].Length;
            colorLookupTable.SetNumberOfTableValues((int)LUTSize - 1);

            for (int i = 0; i < (int)LUTSize - 1; i++)
                colorLookupTable.SetTableValue((int)LUTSize - 1 - i - 1, LUT[0][i] / LUTSize, LUT[1][i] / LUTSize, LUT[2][i] / LUTSize, 1);

            colorLookupTable.Build();

            base.vtk_PolyDataMapper.SetLookupTable(colorLookupTable);

            CreateVTK3DObject(1);
        }

        public c3DElevationMap(cPoint3D Center, cExtendedTable Input, byte[][] LUT)
        {
            BuildElevationMapObject(Center, Input, LUT);
        }
    }

    /// <summary>
    /// Textured Plan
    /// </summary>
    public class c3DTexturedPlan : cGeometric3DObject
    {
        public  List<byte[][]> LUT = null;
        // public vtkPoints points;

        vtkPlaneSource plane;
        vtkTextureMapToPlane texturePlane;
        vtkTexture texture;
        cImage AssociatedImage = null;
        cPoint3D Center = new cPoint3D(0,0,0);
        vtkImageData imageData;
        vtkLookupTable colorLookupTable = null;


        public void ChangeLUT(byte[][] NewLUT)
        {
            this.LUT = new List<byte[][]>();
            this.LUT.Add(NewLUT);

            double LUTSize =this.LUT[0][0].Length;

            colorLookupTable = vtkLookupTable.New();
            //colorLookupTable.SetNumberOfTableValues((int)LUTSize - 1);
            colorLookupTable.SetNumberOfTableValues(256);

            cExtendedTable ET = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetMinMax(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor());



            for (int i = 0; i < (int)LUTSize - 1; i++)
            {
                int Idx = (int)((i * 255) / LUTSize);
                colorLookupTable.SetTableValue(Idx, this.LUT[0][0][i] / LUTSize, LUT[0][1][i] / LUTSize, LUT[0][2][i] / LUTSize, 1);
                //colorLookupTable.SetTableValue((int)LUTSize - 1 - i - 1, this.LUT[0][0][i] / LUTSize, LUT[0][1][i] / LUTSize, LUT[0][2][i] / LUTSize, 1);
            }
            colorLookupTable.Build();

            texture.SetLookupTable(colorLookupTable);
            
            cImageDisplayProperties IDP = new cImageDisplayProperties();
            IDP.ListMin = new List<double>();
            IDP.ListMin.Add(ET[0][0]);
            IDP.ListMax = new List<double>();
            IDP.ListMax.Add(ET[0][1]);



            imageData = vtkImageData.FromImage(AssociatedImage.GetBitmap(1, IDP, this.LUT));
        }     

        public c3DTexturedPlan(cPoint3D Center, cImage AssociatedImage)
        {

            this.Tag = AssociatedImage;
            this.SetPosition(new cPoint3D(Center.X, Center.Y, Center.Z));
            this.SetName("3D Texture Plan [" + AssociatedImage.Name + "]");
            this.AssociatedImage = AssociatedImage;
            this.Center = Center;


        

            return;



        }



        public void Run()
        {
            this.Tag = AssociatedImage;
            this.SetPosition(new cPoint3D(Center.X, Center.Y, Center.Z));
            this.SetName("3D Texture Plan [" + AssociatedImage.Name + "]");

            // Read the image which will be the texture
            //vtkJPEGReader jPEGReader = vtkJPEGReader.New();
            //jPEGReader.SetFileName("E:\\Capture.JPG");

            
            // Create a plane
            plane = vtkPlaneSource.New();
            plane.SetOrigin(0.0, 0.0, 0.0);
            plane.SetPoint1(AssociatedImage.Width, 0.0, 0.0);
            plane.SetPoint2(0.0, AssociatedImage.Height, 0.0);
            plane.SetCenter(Center.X, Center.Y, Center.Z);
            plane.SetNormal(0.0, 0.0, 1.0);

            vtkImageData imageData = vtkImageData.New();

            vtkUnsignedShortArray UshortArray = vtkUnsignedShortArray.New();

            //if (AssociatedImage.AssociatedImagePanel != null)
            //{
            //    cImageDisplayProperties IDP = AssociatedImage.AssociatedImagePanel.LUTManager.GetImageDisplayProperties();

            //    UserControlSingleLUT SingleLUT = (UserControlSingleLUT)AssociatedImage.AssociatedImagePanel.LUTManager.panelForLUTS.Controls[0];
            //    imageData = vtkImageData.FromImage(AssociatedImage.GetBitmap(1, IDP, AssociatedImage.AssociatedImagePanel.LUTManager.GetLUTS()));
            //}
            // else
            {
                cImageDisplayProperties IDP = new cImageDisplayProperties();
                IDP.ListMax = new List<double>();
                IDP.ListMax.Add(255);
                IDP.ListMax.Add(255);
                IDP.ListMax.Add(255);

                IDP.ListMin = new List<double>();
                IDP.ListMin.Add(0);
                IDP.ListMin.Add(0);
                IDP.ListMin.Add(0);
                
                Bitmap BMp = AssociatedImage.GetBitmap(1, IDP, this.LUT);
                imageData = vtkImageData.FromImage(BMp);
            }

            vtkExtractVOI voi = vtkExtractVOI.New();
            voi.SetInput(imageData);
            voi.SetVOI(0, AssociatedImage.Width - 1, 0, AssociatedImage.Height - 1, 0, 0);


            texture = vtkTexture.New();
            texture.SetInputConnection(voi.GetOutputPort());

              //   texture.MapColorScalarsThroughLookupTableOn();



            //   //   colorLookupTable.Build();

            //      //// no LUT then build one
            //      //if (LUT == null)
            //      //{
            //      //    cLUT MyLut = new cLUT();
            //      //    LUT = MyLut.LUT_JET;
            //      //}

         //   if(this.LUT!=null)
         //   ChangeLUT(this.LUT[0]);



            texturePlane = vtkTextureMapToPlane.New();
            texturePlane.SetInputConnection(plane.GetOutputPort());

            base.vtk_PolyDataMapper = vtkPolyDataMapper.New();
            base.vtk_PolyDataMapper.SetInputConnection(texturePlane.GetOutputPort());

            base.vtk_Actor = vtkActor.New();
            base.vtk_Actor.SetMapper(base.vtk_PolyDataMapper);
            base.vtk_Actor.SetTexture(texture);


            base.SourcePolyData = plane.GetOutput();
            this.Colour = Color.FromArgb(255, 255, 255);

            CreateVTK3DObject(0);
            base.vtk_Actor.GetProperty().LightingOff();

            return;
      //      // Create a plane
      //      plane = vtkPlaneSource.New();

      //  //    plane.SetOrigin(0.0, 0.0, 0.0);
      //  //    plane.SetPoint1(AssociatedImage.Width, 0.0, 0.0);
      //  //    plane.SetPoint2(0.0, AssociatedImage.Height, 0.0);
      //     // plane.SetPoint1(AssociatedImage.Width , 0, 0);
      //     // plane.SetPoint2(0, /*AssociatedImage.Height*/24, 0);

      //      plane.SetCenter(Center.X, Center.Y, Center.Z);
      //      plane.SetNormal(0.0, 0.0, 1.0);

      //      imageData = vtkImageData.New();

      //      vtkUnsignedShortArray UshortArray = vtkUnsignedShortArray.New();

      //      if (AssociatedImage.AssociatedImagePanel != null)
      //      {
      //          cImageDisplayProperties IDP = AssociatedImage.AssociatedImagePanel.LUTManager.GetImageDisplayProperties();

      //          UserControlSingleLUT SingleLUT = (UserControlSingleLUT)AssociatedImage.AssociatedImagePanel.LUTManager.panelForLUTS.Controls[0];
      //          this.LUT = AssociatedImage.AssociatedImagePanel.LUTManager.GetLUTS();
      //          imageData = vtkImageData.FromImage(AssociatedImage.GetBitmap(1, IDP, this.LUT ));
      //      }
      //      else
      //      {
      //          Bitmap BMp = AssociatedImage.GetBitmap(1, null, this.LUT);
      //          imageData = vtkImageData.FromImage(BMp);
      //      }

      //      vtkExtractVOI voi = vtkExtractVOI.New();
      //      voi.SetInput(imageData);
      //      voi.SetVOI(0, AssociatedImage.Width, 0, AssociatedImage.Height, 0, 0);

      //      texture = vtkTexture.New();
      //      texture.SetInputConnection(voi.GetOutputPort());

      // //     texture.MapColorScalarsThroughLookupTableOn();


            
      //   //   colorLookupTable.Build();

      //      //// no LUT then build one
      //      //if (LUT == null)
      //      //{
      //      //    cLUT MyLut = new cLUT();
      //      //    LUT = MyLut.LUT_JET;
      //      //}
      //   //   ChangeLUT(this.LUT[0]);


      //      //double LUTSize = this.LUT[0][0].Length;

      //      //colorLookupTable = vtkLookupTable.New();
      //      //colorLookupTable.SetNumberOfTableValues((int)LUTSize - 1);

      //      //for (int i = 0; i < (int)LUTSize - 1; i++)
      //      //    colorLookupTable.SetTableValue((int)LUTSize - 1 - i - 1, this.LUT[0][0][i] / LUTSize, LUT[0][1][i] / LUTSize, LUT[0][2][i] / LUTSize, 1);

      //      //colorLookupTable.Build();

      //    //  texture.SetLookupTable(colorLookupTable);
      //    //  imageData = vtkImageData.FromImage(AssociatedImage.GetBitmap(1, null, this.LUT));








      //      texturePlane = vtkTextureMapToPlane.New();
      //      texturePlane.SetInputConnection(plane.GetOutputPort());


      //      base.vtk_PolyDataMapper = vtkPolyDataMapper.New();
      //      base.vtk_PolyDataMapper.SetInputConnection(texturePlane.GetOutputPort());

      //      base.vtk_Actor = vtkActor.New();
      //      base.vtk_Actor.SetMapper(base.vtk_PolyDataMapper);
      //      base.vtk_Actor.SetTexture(texture);

      //      base.SourcePolyData = plane.GetOutput();
      //      //this.Colour = Color.FromArgb(255, 255, 255);

      //      //base.vtk_Actor.GetProperty().BackfaceCullingOff();
      //  //    base.vtk_Actor.GetProperty().LightingOff();
      //   //   CreateVTK3DObject(0);

      ////base.vtk_Actor.GetProperty().BackfaceCullingOff();
      // //    base.vtk_Actor.GetProperty().LightingOff();
      //      return;


         

      //      texture.InterpolateOn();

      //   //   vtkTextureMapToPlane texturePlane = vtkTextureMapToPlane.New();
      //      texturePlane.SetInputConnection(plane.GetOutputPort());

      //      SourcePolyData = vtkPolyData.New();
      //      SourcePolyData = plane.GetOutput();

      //      vtk_PolyDataMapper = vtkPolyDataMapper.New();
      //      vtk_PolyDataMapper.SetInputConnection(texturePlane.GetOutputPort());

      //    //  CreateVTK3DObject(0);

      //      base.vtk_PolyDataMapper.SetInputConnection(texturePlane.GetOutputPort());

      //      base.vtk_Actor.SetMapper(base.vtk_PolyDataMapper);
      //      base.vtk_Actor.SetTexture(texture);
      //      //base.vtk_Actor.SetScale(AssociatedImage.Width, AssociatedImage.Height, 0);
      //      //base.vtk_Actor.GetProperty().SetOpacity(0.9);

      //      //base.vtk_Actor.GetProperty().SetAmbientColor(255, 255, 255);
      //      //base.vtk_Actor.GetProperty().SetDiffuse(1);
      //      //base.vtk_Actor.GetProperty().SetAmbient(1);
      //      //base.vtk_Actor.GetProperty().SetSpecular(1);
      //      //base.vtk_Actor.GetProperty().SetSpecularColor(255, 255, 255);
      //      //base.vtk_Actor.GetProperty().SetLighting(true);

      //    //  base.vtk_Actor.GetProperty().SetLighting(true);
        }
    }

    /// <summary>
    /// 3D sphere Object
    /// </summary>
    public class c3DSphere : cGeometric3DObject
    {
        public double Radius;
        public vtkSphereSource sphere;

        private void CreateSphere(cPoint3D Center, double Radius, Color Colour, int Precision)
        {
            this.SetPosition(Center);

            this.Radius = Radius;
            this.Colour = Colour;
            this.SetOpacity(Colour.A / 255.0);

            sphere = vtkSphereSource.New();
            sphere.SetThetaResolution(Precision);
            sphere.SetPhiResolution(Precision);
            sphere.SetRadius(Radius);
            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            vtk_PolyDataMapper.SetInputConnection(sphere.GetOutputPort());

            base.SourcePolyData = sphere.GetOutput();

            CreateVTK3DObject(2);

            base.SetName("Sphere");
        }

        public c3DSphere(cPoint3D Center, double Radius, Color Colour)
        {
            CreateSphere(Center, Radius, Colour, 16);
        }

        public c3DSphere(cPoint3D Center, double Radius, Color Colour, int Precision)
        {
            CreateSphere(Center, Radius, Colour, Precision);
        }

        public c3DSphere(cPoint3D Center, double Radius)
        {
            CreateSphere(Center, Radius, Color.Red, 16);
        }
    }

    public class c3DArrow : cGeometric3DObject
    {
        public double Scale;
        public vtkArrowSource Arrow;

        private void CreateArrow(cPoint3D Position, double Scale, Color Colour)
        {
            base.SetPosition(new cPoint3D(Position.X, Position.Y, Position.Z));

            this.Scale = Scale;
            base.Colour = Colour;

            Arrow = vtkArrowSource.New();


            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            vtk_PolyDataMapper.SetInputConnection(Arrow.GetOutputPort());


            // ArrowActor = vtkActor.New();
            // vtkPolyDataMapper ArrowMapper = vtkPolyDataMapper.New();
            // ArrowMapper.SetInputConnection(ArrowSource.GetOutputPort());
            //  ArrowActor.SetMapper(ArrowMapper);

            // ArrowActor.SetPosition(Position.X - 6, Position.Y - 0, Position.Z - 6);
            // ArrowActor.RotateY(-45);

            // ArrowActor.SetScale(scale);
            //   ArrowActor.SetPickable(0);
            //    Color C = Color.MediumPurple;

            //    ArrowActor.GetProperty().SetColor(Colour.R / 255.0, Colour.G / 255.0, Colour.G / 255.0);

            //sphere.SetThetaResolution(Precision);
            //sphere.SetPhiResolution(Precision);
            //sphere.SetRadius(Radius);
            //vtk_PolyDataMapper = vtkPolyDataMapper.New();
            //vtk_PolyDataMapper.SetInputConnection(sphere.GetOutputPort());

            CreateVTK3DObject(1);

            base.vtk_Actor.RotateY(-45);
            base.vtk_Actor.SetScale(Scale);
            //  base.vtk_Actor.SetPosition(Position.X, Position.Y, Position.Z);
            //   base.vtk_Actor.GetProperty().SetColor(Colour.R / 255.0, Colour.G / 255.0, Colour.G / 255.0);
        }

        public c3DArrow(cPoint3D Position, double Scale, Color Colour)
        {
            CreateArrow(Position, Scale, Colour);
        }

        public c3DArrow(cPoint3D Position, double Scale)
        {
            CreateArrow(Position, Scale, Color.White);
        }

        public c3DArrow(cPoint3D Start, cPoint3D End)
        {
            BuildArrow(Start, End, Color.White, 1);
        }

        public c3DArrow(cPoint3D Start, cPoint3D End, double Scale)
        {
            BuildArrow(Start, End, Color.White, Scale);
        }

        public c3DArrow(cPoint3D Start, cPoint3D End, Color Colour)
        {
            BuildArrow(Start, End, Colour, 1);
        }

        void BuildArrow(cPoint3D Start, cPoint3D End, Color Colour, double TmpScale)
        {

            base.SetPosition(new cPoint3D(Start.X, Start.Y, Start.Z));

            cPoint3D normalizedX = new cPoint3D(End.X - Start.X, End.Y - Start.Y, End.Z - Start.Z);
            double Norm = Math.Sqrt(normalizedX.X * normalizedX.X + normalizedX.Y * normalizedX.Y + normalizedX.Z * normalizedX.Z);

            normalizedX.X /= Norm;
            normalizedX.Y /= Norm;
            normalizedX.Z /= Norm;

            double[] arbitrary = new double[3];
            arbitrary[0] = -0.6;
            arbitrary[1] = 0.8;
            arbitrary[2] = 0.9;
            double NormZ = Math.Sqrt(arbitrary[0] * arbitrary[0] + arbitrary[1] * arbitrary[1] + arbitrary[2] * arbitrary[2]);

            arbitrary[0] /= NormZ;
            arbitrary[1] /= NormZ;
            arbitrary[2] /= NormZ;

            cPoint3D normalizedZ = new cPoint3D(normalizedX.Y * arbitrary[2] - normalizedX.Z * arbitrary[1],
                                                normalizedX.Z * arbitrary[0] - normalizedX.X * arbitrary[2],
                                                normalizedX.X * arbitrary[1] - normalizedX.Y * arbitrary[0]);

            cPoint3D normalizedY = new cPoint3D(normalizedZ.Y * normalizedX.Z - normalizedZ.Z * normalizedX.Y,
                                                normalizedZ.Z * normalizedX.X - normalizedZ.X * normalizedX.Z,
                                                normalizedZ.X * normalizedX.Y - normalizedZ.Y * normalizedX.X);

            vtkMatrix4x4 matrix = vtkMatrix4x4.New();
            matrix.Identity();
            matrix.SetElement(0, 0, normalizedX.X);
            matrix.SetElement(0, 1, normalizedY.X);
            matrix.SetElement(0, 2, normalizedZ.X);

            matrix.SetElement(1, 0, normalizedX.Y);
            matrix.SetElement(1, 1, normalizedY.Y);
            matrix.SetElement(1, 2, normalizedZ.Y);

            matrix.SetElement(2, 0, normalizedX.Z);
            matrix.SetElement(2, 1, normalizedY.Z);
            matrix.SetElement(2, 2, normalizedZ.Z);


            // Apply the transforms
            vtkTransform transform = vtkTransform.New();
            // transform.Translate(0,0,0);//-Start.X, -Start.Y, -Start.Z);
            transform.Concatenate(matrix);
            transform.Scale(TmpScale, TmpScale, TmpScale);
            //  transform.Scale(1, 1, TmpScale);


            // Transform the polydata
            vtkTransformPolyDataFilter transformPD = vtkTransformPolyDataFilter.New();
            transformPD.SetTransform(transform);

            Arrow = vtkArrowSource.New();

            transformPD.SetInputConnection(Arrow.GetOutputPort());

            //this.Scale = Scale;
            base.Colour = Colour;
            Scale = Norm;
            double SRad = 0.005;

            // Shaft Parameter
            //  double SRad = Arrow.GetShaftRadius();
            if (TmpScale == 1)
            {
                Arrow.SetShaftRadius(0.005);
            }
            else
            {
                double Tmp = 1 + Math.Log10(TmpScale);
                SRad = 0.002 * Math.Sqrt(Tmp);
                Arrow.SetShaftRadius(SRad);

            }
            Arrow.SetShaftResolution(20);


            // Tip parameter
            // double Tip = Arrow.GetTipLength();
            Arrow.SetTipLength(0.05);
            // double Rad = Arrow.GetTipRadius();
            Arrow.SetTipRadius(3 * SRad/*0.02 / Norm*/);
            // double Res = Arrow.GetTipResolution();
            Arrow.SetTipResolution(20);

            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            vtk_PolyDataMapper.SetInputConnection(transformPD.GetOutputPort());

            CreateVTK3DObject(1);


            base.SourcePolyData = this.Arrow.GetOutput();
            //  base.vtk_Actor.SetScale(TmpScale);
        }

    }

    public class c3DDRC : cGeometric3DObject
    {
        public vtkParametricSpline Spline;

        private void Create3DDRC(cDRC DRCToDraw, cDRC_Region AssociatedRegion, Color Color, double Min, double Max)
        {
            if (DRCToDraw.ResultFit == null) return;

            this.SetPosition(new cPoint3D(AssociatedRegion.PosXMin + 0.5, AssociatedRegion.PosYMin + 0.2, 0));
            this.Colour = Color;
            vtkPoints points = vtkPoints.New();

            vtkUnsignedCharArray colors = vtkUnsignedCharArray.New();
            colors.SetName("Colors");
            colors.SetNumberOfComponents(3);
            colors.SetNumberOfTuples(AssociatedRegion.NumConcentrations);

            for (int i = 0; i < AssociatedRegion.NumConcentrations; i++)
            {
                if (i >= DRCToDraw.ResultFit.Y_Estimated.Count) continue;
                double PosZ = 8 - ((DRCToDraw.ResultFit.GetNormalizedY_Estimated()[i]) * 8);

                points.InsertPoint(i, i, 0, PosZ);
                colors.InsertTuple3(i / AssociatedRegion.NumConcentrations, i / AssociatedRegion.NumConcentrations, 255, i / AssociatedRegion.NumConcentrations);
            }

            Spline = vtkParametricSpline.New();
            Spline.SetPoints(points);
            Spline.ClosedOff();

            vtkParametricFunctionSource SplineSource = vtkParametricFunctionSource.New();
            SplineSource.SetParametricFunction(Spline);

            //     SplineSource.GetPolyDataInput(0).GetPointData().AddArray(colors);

            vtkLinearExtrusionFilter extrude = vtkLinearExtrusionFilter.New();
            extrude.SetInputConnection(SplineSource.GetOutputPort());

            //extrude.GetPolyDataInput(0).GetPointData().AddArray(colors);
            extrude.SetScaleFactor(AssociatedRegion.NumReplicate - 0.2);
            //extrude.SetExtrusionTypeToNormalExtrusion();

            extrude.SetExtrusionTypeToVectorExtrusion();
            extrude.SetVector(0, 1, 0);

            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            vtk_PolyDataMapper.SetInputConnection(extrude.GetOutputPort()/*SplineSource.GetOutputPort()*/);
            vtk_PolyDataMapper.GetInput().GetPointData().AddArray(colors);
            vtk_PolyDataMapper.ScalarVisibilityOn();
            vtk_PolyDataMapper.SetScalarModeToUsePointFieldData();
            vtk_PolyDataMapper.SelectColorArray("Colors");

            CreateVTK3DObject(3);
        }

        public c3DDRC(cDRC DRCToDraw, cDRC_Region AssociatedRegion, Color Color, double Min, double Max)
        {
            Create3DDRC(DRCToDraw, AssociatedRegion, Color, Min, Max);
        }

        public c3DDRC(cDRC DRCToDraw, cDRC_Region AssociatedRegion, double Min, double Max)
        {
            Create3DDRC(DRCToDraw, AssociatedRegion, Color.White, Min, Max);
        }
    }

    public class c3DThinPlate : cGeometric3DObject
    {
        // double regularization = 0.0;
        double bending_energy = 0.0;

        //List<Vector3> control_points = new List<Vector3>();

        /// <summary> Thin-Plate-Spline base function
        /// </summary>
        /// <param name="r">the function parameter</param>
        /// <returns></returns>
        public double tps_base_func(double r)
        {
            if (r == 0.0)
                return 0.0;
            else
                return r * r * Math.Log(r);
        }

        /// <summary>
        /// Compute the Thin Plate Spline of the image, return a 2D tab
        /// </summary>
        /// <param name="control_points">Control points  </param>    
        /// <param name="input">Input image to get the dim xy</param>
        public double[,] calc_tps(cListPoints3D control_points, cDRC_Region AssociatedRegion, double Regularization)
        {

            int p = control_points.Count;
            if (p < 3) return null;
            double[,] grid = new double[AssociatedRegion.SizeX, AssociatedRegion.SizeY];
            Matrix mtx_l = new Matrix(p + 3, p + 3);
            Matrix mtx_v = new Matrix(p + 3, 1);
            Matrix mtx_orig_k = new Matrix(p, p);
            double a = 0.0;
            for (int i = 0; i < p; ++i)
            {
                for (int j = i + 1; j < p; ++j)
                {
                    cPoint3D pt_i = new cPoint3D(control_points[i].X, control_points[i].Y, control_points[i].Z);
                    cPoint3D pt_j = new cPoint3D(control_points[j].X, control_points[j].Y, control_points[j].Z);

                    pt_i.Y = pt_j.Y = 0;

                    //double elen = Math.Sqrt((pt_i.X - pt_j.X) * (pt_i.X - pt_j.X) + (pt_i.Z - pt_j.Z) * (pt_i.Z - pt_j.Z));
                    double elen = pt_i.DistTo(pt_j);
                    mtx_l[i, j] = mtx_l[j, i] = mtx_orig_k[i, j] = mtx_orig_k[j, i] = tps_base_func(elen);
                    a += elen * 2; // same for upper & lower tri
                }
            }
            a /= (double)(p * p);
            //regularization = 0.3f;
            //Fill the rest of L
            for (int i = 0; i < p; ++i)
            {
                //diagonal: reqularization parameters (lambda * a^2)

                mtx_l[i, i] = mtx_orig_k[i, i] = Regularization * (a * a);



                // P (p x 3, upper right)
                mtx_l[i, p + 0] = 1.0;
                mtx_l[i, p + 1] = control_points[i].X;
                mtx_l[i, p + 2] = control_points[i].Z;

                // P transposed (3 x p, bottom left)
                mtx_l[p + 0, i] = 1.0;
                mtx_l[p + 1, i] = control_points[i].X;
                mtx_l[p + 2, i] = control_points[i].Z;
            }
            // O (3 x 3, lower right)
            for (int i = p; i < p + 3; ++i)
                for (int j = p; j < p + 3; ++j)
                    mtx_l[i, j] = 0.0;


            // Fill the right hand vector V
            for (int i = 0; i < p; ++i)
                mtx_v[i, 0] = control_points[i].Y;

            mtx_v[p + 0, 0] = mtx_v[p + 1, 0] = mtx_v[p + 2, 0] = 0.0;
            // Solve the linear system "inplace" 
            Matrix mtx_v_res = new Matrix(p + 3, 1);

            LuDecomposition ty = new LuDecomposition(mtx_l);



            mtx_v_res = ty.Solve(mtx_v);
            if (mtx_v_res == null)
            {
                return null;
            }


            // Interpolate grid heights
            for (int x = 0; x < AssociatedRegion.SizeX; ++x)
            {
                for (int z = 0; z < AssociatedRegion.SizeY; ++z)
                {

                    //float x = 0f; float z = 0.5f;
                    double h = mtx_v_res[p + 0, 0] + mtx_v_res[p + 1, 0] * (float)x / (float)AssociatedRegion.SizeX + mtx_v_res[p + 2, 0] * (float)z / (float)AssociatedRegion.SizeY;
                    //double h = mtx_v[p + 0, 0] + mtx_v[p + 1, 0] * (float)x + mtx_v[p + 2, 0] * (float)z ;
                    cPoint3D pt_ia;
                    cPoint3D pt_cur = new cPoint3D((float)x / (float)AssociatedRegion.SizeX, 0, (float)z / (float)AssociatedRegion.SizeY);
                    //Vector3 pt_cur = new Vector3((float)x , 0, (float)z);
                    for (int i = 0; i < p; ++i)
                    {
                        pt_ia = control_points[i];
                        pt_ia.Y = 0;
                        h += mtx_v_res[i, 0] * tps_base_func(pt_ia.DistTo(pt_cur));
                    }

                    grid[x, z] = h;
                }
            }
            // Calc bending energy
            Matrix w = new Matrix(p, 1);
            for (int i = 0; i < p; ++i)
                w[i, 0] = mtx_v_res[i, 0];

            Matrix be;

            be = Matrix.Multiply(Matrix.Multiply(w.Transpose(), mtx_orig_k), w);
            bending_energy = be[0, 0];

            Console.WriteLine("be= " + be[0, 0]);
            return grid;


        }

        private void Create(cDRC_Region AssociatedRegion, Color Color, double Regularization)
        {
            this.SetPosition(new cPoint3D(AssociatedRegion.PosXMin + 1, AssociatedRegion.PosYMin + 0.7, 0));

            cListPoints3D ListPtSigma = new cListPoints3D();

            double GlobalMin = double.MaxValue;
            double GlobalMax = double.MinValue;
            for (int j = 0; j < AssociatedRegion.SizeY; j++)
                for (int i = 0; i < AssociatedRegion.SizeX; i++)
                {
                    cWell TmpWell = AssociatedRegion.GetListWells()[j][i];
                    if (TmpWell == null) continue;
                    double PosZ = TmpWell.ListSignatures[TmpWell.AssociatedPlate.ParentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetValue();
                    if (PosZ >= GlobalMax) GlobalMax = PosZ;
                    if (PosZ <= GlobalMin) GlobalMin = PosZ;

                    ListPtSigma.Add(new cPoint3D(i / (double)AssociatedRegion.SizeX, PosZ, j / (double)AssociatedRegion.SizeY));
                }

            if (GlobalMax == GlobalMin) return;



            double[,] ResultThinPlate = calc_tps(ListPtSigma, AssociatedRegion, Regularization);

            if (ResultThinPlate == null)
            {
                // MessageBox.Show("Error in computing the associated thinplate !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //  Position = new cPoint3D(0, 0, 0);
            this.Colour = Color;
            vtkPoints points = vtkPoints.New();
            // vtkPoints points0 = vtkPoints.New();

            //vtkUnsignedCharArray colors = vtkUnsignedCharArray.New();
            //colors.SetName("Colors");
            //colors.SetNumberOfComponents(3);
            //colors.SetNumberOfTuples(AssociatedRegion.SizeX * AssociatedRegion.SizeY);




            int Idx = 0;
            for (int j = 0; j < AssociatedRegion.SizeY; j++)
                for (int i = 0; i < AssociatedRegion.SizeX; i++)
                {
                    cWell TmpWell = AssociatedRegion.GetListWells()[j][i];

                    double PosZ = 8 - ((ResultThinPlate[i, j] - GlobalMin) / (GlobalMax - GlobalMin)) * 8;


                    //points.InsertPoint(Idx,TmpWell.GetPosX(), TmpWell.GetPosY(), PosZ);

                    //  points.InsertPoint(Idx, TmpWell.GetPosX(), TmpWell.GetPosY(), 0);
                    points.InsertPoint(Idx++, i, j, PosZ);
                    //  colors.InsertTuple3(Idx++, 1, 1, 1);

                }

            vtkPolyData profile = vtkPolyData.New();
            profile.SetPoints(points);

            vtkDelaunay2D del = vtkDelaunay2D.New();
            del.SetInput(profile);
            del.SetTolerance(0.001);



            vtkButterflySubdivisionFilter subdivisionFilter = vtkButterflySubdivisionFilter.New();
            subdivisionFilter.SetInput(del.GetOutput());
            subdivisionFilter.SetNumberOfSubdivisions(2);
            subdivisionFilter.Update();






            vtk_PolyDataMapper = vtkPolyDataMapper.New();

            AlgoOutPut = subdivisionFilter.GetOutputPort();

            vtk_PolyDataMapper.SetInputConnection(AlgoOutPut);
            // vtk_PolyDataMapper.GetInput().GetPointData().AddArray(colors);
            // vtk_PolyDataMapper.ScalarVisibilityOn();
            // vtk_PolyDataMapper.SetScalarModeToUsePointFieldData();
            // vtk_PolyDataMapper.SelectColorArray("Colors");





            //         vtkLinearExtrusionFilter  extrude = vtkLinearExtrusionFilter.New();
            //         extrude.SetInput(cutter.GetOutput());
            //extrude.SetScaleFactor(1);
            //extrude.SetExtrusionTypeToNormalExtrusion();
            //extrude.SetVector(1, 1, 1);

            //vtkRotationalExtrusionFilter extrude = vtkRotationalExtrusionFilter.New();
            //extrude.SetInput(cutter.GetOutput());
            //extrude.SetResolution(60);
            //extrude.Update();



            //vtk_PolyDataMapper.SetInputConnection(tubeFilter.GetOutputPort());




            CreateVTK3DObject(3);

            SetColor(Color);
            // SetOpacity(1);
            SetToSurface();
        }

        public c3DThinPlate(cDRC_Region AssociatedRegion, Color Color, double Regularization)
        {
            Create(AssociatedRegion, Color, Regularization);
        }

        public c3DThinPlate(cDRC_Region AssociatedRegion, double Regularization)
        {
            Create(AssociatedRegion, Color.White, Regularization);
        }
    }

    /// <summary>
    /// 2D Delaunay mesh
    /// </summary>
    public class c2DDelaunay : cGeometric3DObject
    {
        private void CreateDelaunay(cListPoints3D ListPts, Color Colour, bool IsWire)
        {
            this.SetPosition(new cPoint3D(0, 0, 0));
            this.Colour = Colour;
            vtkPoints ListCentroid = vtkPoints.New();

            for (int i = 0; i < ListPts.Count; i++)
                ListCentroid.InsertPoint(i, ListPts[i].X, ListPts[i].Y, ListPts[i].Z);

            vtkPolyData profile = vtkPolyData.New();
            profile.SetPoints(ListCentroid);

            vtkDelaunay2D del = vtkDelaunay2D.New();
            del.SetInput(profile);
            del.SetTolerance(0.001);

            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            vtk_PolyDataMapper.SetInputConnection(del.GetOutputPort());

            CreateVTK3DObject(0);
            if (IsWire) SetToWireFrame();
            else SetToSurface();
        }

        public c2DDelaunay(cListPoints3D ListPts, Color Colour, bool IsWire)
        {
            CreateDelaunay(ListPts, Colour, IsWire);
        }

        public c2DDelaunay(cListPoints3D ListPts, bool IsWire)
        {
            CreateDelaunay(ListPts, Color.PapayaWhip, IsWire);
        }
    }

    /// <summary>
    /// 3D Line Object
    /// </summary>
    public class c3DLine : cGeometric3DObject
    {
        public vtkLineSource Line;
        private cPoint3D Point1;
        private cPoint3D Point2;

        private void CreateLine(cPoint3D Point1, cPoint3D Point2, Color Colour)
        {
            // Position = new cPoint3D(Point1.X, Point1.Y, Point1.Z);

            this.SetPosition(new cPoint3D(0, 0, 0));

            this.Colour = Colour;
            Line = vtkLineSource.New();

            this.Point1 = new cPoint3D(Point1.X, Point1.Y, Point1.Z);
            this.Point2 = new cPoint3D(Point2.X, Point2.Y, Point2.Z);

            Line.SetPoint1(Point1.X, Point1.Y, Point1.Z);
            Line.SetPoint2(Point2.X, Point2.Y, Point2.Z);


            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            vtk_PolyDataMapper.SetInputConnection(Line.GetOutputPort());


            CreateVTK3DObject(0);

            base.SourcePolyData = Line.GetOutput();

        }

        public c3DLine(cPoint3D Point1, cPoint3D Point2, Color Colour)
        {
            CreateLine(Point1, Point2, Colour);
        }

        public c3DLine(cPoint3D Point1, cPoint3D Point2)
        {
            CreateLine(Point1, Point2, Color.White);
        }

        public void DisplayLenght(c3DWorld CurrentWorld, double scale)
        {
            vtkFollower TextActor = vtkFollower.New();
            vtkPolyDataMapper TextMapper = vtkPolyDataMapper.New();
            vtkVectorText TextVTK = vtkVectorText.New();


            double Dist = Point1.DistTo(Point2);

            TextVTK.SetText(Dist.ToString("N2"));
            TextMapper.SetInputConnection(TextVTK.GetOutputPort());
            TextActor.SetMapper(TextMapper);
            TextActor.SetPosition(this.GetActor().GetCenter()[0], this.GetActor().GetCenter()[1], this.GetActor().GetCenter()[2]);
            TextActor.SetPickable(0);
            TextActor.SetScale(scale);

            CurrentWorld.ren1.AddActor(TextActor);
            TextActor.SetCamera(CurrentWorld.ren1.GetActiveCamera());

        }
    }

    /// <summary>
    /// 3D Line Object
    /// </summary>
    public class c3DPoint : cGeometric3DObject
    {
        // public vtkpoint Line;
        // private cPoint3D Point1;


        private void CreatePoint(cPoint3D Point, Color Colour)
        {
            base.SetPosition(Point);
            vtkPoints points = vtkPoints.New();
            points.InsertNextPoint(0, 0, 0);//Point.X, Point.Y, Point.Z);

            base.SourcePolyData = vtkPolyData.New();

            base.SourcePolyData.SetPoints(points);

            vtkVertexGlyphFilter vertexFilter = vtkVertexGlyphFilter.New();

            vertexFilter.SetInput(base.SourcePolyData);

            vertexFilter.Update();

            vtkPolyData polydata = vtkPolyData.New();
            polydata.ShallowCopy(vertexFilter.GetOutput());

            // Setup colors


            vtkUnsignedCharArray colors =
              vtkUnsignedCharArray.New();
            colors.SetNumberOfComponents(3);
            colors.SetName("Colors");
            //colors.InsertNextTuple1(Colour.R/255.0);
            colors.InsertNextTuple3(Colour.R / 255.0, Colour.G / 255.0, Colour.B / 255.0);
            //  colors->InsertNextTupleValue(blue);
            base.SourcePolyData.GetPointData().SetScalars(colors);



            CreateVTK3DObject(0);
            //base.SourcePolyData = point;

        }

        public c3DPoint(cPoint3D Point, Color Colour)
        {
            CreatePoint(Point, Colour);
        }

        public c3DPoint(cPoint3D Point)
        {
            CreatePoint(Point, Color.White);
        }


    }

    /// <summary>
    /// 3D Line Object
    /// </summary>
    public class c3DText : cGeometric3DObject
    {
        // public vtkLineSource Line;
        private cPoint3D Point1;
        // private cPoint3D Point2;

        //private void CreateLine(cPoint3D Point1, cPoint3D Point2, Color Colour)
        //{
        //    Position = new cPoint3D(Point1.X, Point1.Y, Point1.Z);

        //    Position = new cPoint3D(0, 0, 0);

        //    this.Colour = Colour;
        //    Line = vtkLineSource.New();

        //    this.Point1 = new cPoint3D(Point1.X, Point1.Y, Point1.Z);
        //    this.Point2 = new cPoint3D(Point2.X, Point2.Y, Point2.Z);

        //    Line.SetPoint1(Point1.X, Point1.Y, Point1.Z);
        //    Line.SetPoint2(Point2.X, Point2.Y, Point2.Z);


        //    vtk_PolyDataMapper = vtkPolyDataMapper.New();
        //    vtk_PolyDataMapper.SetInputConnection(Line.GetOutputPort());


        //    CreateVTK3DObject(0);
        //}
        public vtkVectorText TextVTK;

        public c3DText(string TextToDisplay, cPoint3D Position, Color Colour, double Scale)
        {
            TextActorFollower = vtkFollower.New();
            //vtkPolyDataMapper 


            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            TextVTK = vtkVectorText.New();

            TextVTK.SetText(TextToDisplay);
            vtk_PolyDataMapper.SetInputConnection(TextVTK.GetOutputPort());
            TextActorFollower.SetMapper(vtk_PolyDataMapper);
            TextActorFollower.SetPosition(Position.X, Position.Y, Position.Z);
            TextActorFollower.SetPickable(1);
            TextActorFollower.SetScale(Scale);
            TextActorFollower.GetProperty().SetColor(Colour.R / 255.0, Colour.G / 255.0, Colour.B / 255.0);
            TextActorFollower.GetProperty().SetAmbient(1);


            base.SetPosition(new cPoint3D(Position.X, Position.Y, Position.Z));
            base.Colour = Colour;

            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            vtk_PolyDataMapper.SetInputConnection(TextVTK.GetOutputPort());

            CreateVTK3DObject(1);


            base.SourcePolyData = TextVTK.GetOutput();
            //  CurrentWorld.ren1.AddActor(TextActor);
            //  TextActorFollower.SetCamera(CurrentWorld.ren1.GetActiveCamera());

        }

        public c3DText(c3DWorld CurrentWorld, string TextToDisplay, cPoint3D Position, Color Colour, double Scale)
        {
            // CreateLine(Point1, Point2, Colour);

            vtkFollower TextActor = vtkFollower.New();
            vtkPolyDataMapper TextMapper = vtkPolyDataMapper.New();
            vtkVectorText TextVTK = vtkVectorText.New();


            //  double Dist = Point1.DistTo(Point2);

            TextVTK.SetText(TextToDisplay);
            TextMapper.SetInputConnection(TextVTK.GetOutputPort());
            TextActor.SetMapper(TextMapper);
            TextActor.SetPosition(Position.X, Position.Y, Position.Z);
            TextActor.SetPickable(1);
            TextActor.SetScale(Scale);
            TextActor.GetProperty().SetColor(Colour.R / 255.0, Colour.G / 255.0, Colour.B / 255.0);
            TextActor.GetProperty().SetAmbient(1);

            CurrentWorld.ren1.AddActor(TextActor);
            TextActor.SetCamera(CurrentWorld.ren1.GetActiveCamera());

        }

        public c3DText(c3DNewWorld CurrentWorld, string TextToDisplay, cPoint3D Position, Color Colour, double Scale)
        {
            vtkFollower TextFollowerActor = vtkFollower.New();
            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            //vtkVectorText 
            TextVTK = vtkVectorText.New();


            //base.vtk_Actor = vtkActor.New();

            TextVTK.SetText(TextToDisplay);
            vtk_PolyDataMapper.SetInputConnection(TextVTK.GetOutputPort());


            TextFollowerActor.SetMapper(vtk_PolyDataMapper);

            TextFollowerActor.SetCamera(CurrentWorld.Vtk_CameraView);
            TextFollowerActor.SetPosition(Position.X, Position.Y, Position.Z);
            TextFollowerActor.SetPickable(1);
            //TextActor.GetProperty().SetAmbient(1);

            this.SetPosition(Position);
            this.Colour = Colour;



            //vtk_PolyDataMapper = vtkPolyDataMapper.New();
            //vtk_PolyDataMapper.SetInputConnection(TextVTK.GetOutputPort());

            //CreateVTK3DObject(1);
            base.vtk_Actor = TextFollowerActor;
            //  base.vtk_PolyDataMapper = TextMapper;

            base.vtk_Actor.SetScale(Scale);
            base.IsPickable(true);
            base.SetName("3D Text");

        }
    }

    /// <summary>
    /// 3D cube object
    /// </summary>
    public class c3DCube : cGeometric3DObject
    {
        public vtkCubeSource Cube;

        public void Create(cPoint3D MinPt, cPoint3D MaxPt, Color Colour)
        {
            this.SetPosition(new cPoint3D(0, 0, 0));

            this.Colour = Colour;
            Cube = vtkCubeSource.New();
            Cube.SetBounds(MinPt.X, MaxPt.X, MinPt.Y, MaxPt.Y, MinPt.Z, MaxPt.Z);

            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            vtk_PolyDataMapper.SetInputConnection(Cube.GetOutputPort());
            base.SetName("Cube");
            CreateVTK3DObject(0);

            base.SourcePolyData = Cube.GetOutput();

        }
    }

    /// <summary>
    /// 3D plane object
    /// </summary>
    public class c3DPlane : cGeometric3DObject
    {
        public vtkPlaneSource Plane;

        private void CreatePlane(cPoint3D Axis1, cPoint3D Axis2, cPoint3D Origin, Color Colour)
        {
            this.SetPosition(Origin);

            this.Colour = Colour;
            Plane = vtkPlaneSource.New();
            Plane.SetPoint1(Axis1.X, Axis1.Y, Axis1.Z);
            Plane.SetPoint2(Axis2.X, Axis2.Y, Axis2.Z);
            Plane.SetOrigin(0, 0, 0);
            Plane.SetXResolution(1);
            Plane.SetYResolution(1);

            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            vtk_PolyDataMapper.SetInputConnection(Plane.GetOutputPort());

            CreateVTK3DObject(0);


            base.SourcePolyData = Plane.GetOutput();
        }

        public c3DPlane(cPoint3D Point1, cPoint3D Point2, cPoint3D Origin, Color Colour)
        {
            CreatePlane(Point1, Point2, Origin, Colour);
        }

        public c3DPlane(cPoint3D Point1, cPoint3D Point2, cPoint3D Origin)
        {
            CreatePlane(Point1, Point2, Origin, Color.White);
        }
    }
}
