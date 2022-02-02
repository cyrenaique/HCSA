﻿using HCSAnalyzer.Classes.ImageAnalysis._3D_Engine.Detection;
using ImageAnalysis;
using Kitware.VTK;
//using IM.Imaging;
using LibPlateAnalysis;
using System.Collections.Generic;
using System.Drawing;
//using IM.Library.Descriptor;

namespace HCSAnalyzer.Classes._3D
{

    //public class cMeshSmoother
    //{
    //    public int Type = 0;
    //    public int NumberOfIterations = 1;

    //    //public vtkSmoothPolyDataFilter smoother = new vtkSmoothPolyDataFilter();
    //    // public vtkWindowedSincPolyDataFilter smoother = new vtkWindowedSincPolyDataFilter();

    //    public cMeshSmoother(int NumIter)
    //    {
    //        this.NumberOfIterations = NumIter;
    //    }
    //}



    public class cContainer
    {
        public cContainer(List<cInteractive3DObject> Containers, int ContainerMode)
        {
            this.Containers = Containers;
            this.ContainerMode = ContainerMode;

        }

        public List<cInteractive3DObject> Containers;
        public int ContainerMode;
    }




    public class cInteractive3DObject : cObject3D
    {
        // protected string ObjectType;
        public string Name;

        vtkActor ArrowActor;
        vtkActor SphereActor;

        public Image ThumbnailnewImage = null;

        protected int ContainerIdx = -1;

        public int GetContainerIdx()
        {
            return this.ContainerIdx;
        }

        public cPoint3D GetCentroid()
        {

            double[] CenterPt = vtk_Actor.GetCenter();
            cPoint3D Center = new cPoint3D((float)CenterPt[0], (float)CenterPt[1], (float)CenterPt[2]);

            return Center;
        }

        public string GetType()
        {
            return this.ObjectType;
        }

        public void SetType(string Type)
        {
            ObjectType = Type;
        }

        public cInteractive3DObject()
        {
            vtk_Actor.SetPickable(1);
            ObjectType = "";
        }

        public cInteractive3DObject(string Type, Image Thumbnail)
        {
            vtk_Actor.SetPickable(1);
            ObjectType = Type;
        }

        public void Hide()
        {
            //this.SetOpacity(0);
            cObject3D Obj3d = (cObject3D)this;
            //Obj3d.TextActor
            Obj3d.GetActor().VisibilityOff();
            // this.IsPickable(false);
            if (ArrowActor != null) ArrowActor.VisibilityOff();
            if (SphereActor != null) SphereActor.VisibilityOff();

            this.HideText();

        }

        public cGeometric3DObject AttachPointingArrow(double Scale, Color Colour)
        {
            c3DArrow AssociatedArrow = new c3DArrow(new cPoint3D(this.GetPosition().X - 6, this.GetPosition().Y - 0, this.GetPosition().Z - 6), Scale, Colour);

            AssociatedArrow.AssociatedObject = this;
            return AssociatedArrow;
        }

        public cGeometric3DObject AttachText(string Text, double Scale, Color Colour)
        {
            c3DText AssociatedText = new c3DText(Text, this.GetPosition(), Colour, Scale);

            AssociatedText.AssociatedObject = this;
            return AssociatedText;
        }



        //public void AddPointingArrow(double scale, Color Colour)
        //{
        //    vtkArrowSource ArrowSource = vtkArrowSource.New();

        //    ArrowActor = vtkActor.New();
        //    vtkPolyDataMapper ArrowMapper = vtkPolyDataMapper.New();
        //    ArrowMapper.SetInputConnection(ArrowSource.GetOutputPort());
        //    ArrowActor.SetMapper(ArrowMapper);

        //    ArrowActor.SetPosition(Position.X - 6, Position.Y - 0, Position.Z - 6);
        //    ArrowActor.RotateY(-45);

        //    ArrowActor.SetScale(scale);
        //    ArrowActor.SetPickable(0);
        //    Color C = Color.MediumPurple;

        //    ArrowActor.GetProperty().SetColor(Colour.R / 255.0, Colour.G / 255.0, Colour.G / 255.0);
        //    // CreateVTK3DObject();

        //  //  CurrentWorld.ren1.AddActor(ArrowActor);
        //}

        //public void AddPointingArrow(double scale)
        //{
        //    AddPointingArrow(scale, Color.White);
        //}

        public void DisplayCentroid(c3DWorld CurrentWorld, double scale)
        {
            if (SphereActor == null)
            {
                vtkSphereSource SphereSource = vtkSphereSource.New();
                vtkPolyDataMapper SphereMapper = vtkPolyDataMapper.New();

                SphereActor = vtkActor.New();
                SphereMapper = vtkPolyDataMapper.New();

                SphereMapper.SetInputConnection(SphereSource.GetOutputPort());
                SphereActor.SetMapper(SphereMapper);

                double[] CenterPos = vtk_Actor.GetCenter();

                SphereActor.SetPosition(CenterPos[0], CenterPos[1], CenterPos[2]);
                SphereActor.SetScale(scale);
                SphereActor.SetPickable(0);
                Color C = Color.YellowGreen;
                SphereActor.GetProperty().SetColor(C.R / 255.0, C.G / 255.0, C.B / 255.0);
                CurrentWorld.ren1.AddActor(SphereActor);
            }
            else
            {
                if (SphereActor.GetVisibility() == 0)
                    SphereActor.SetVisibility(1);
                else
                    SphereActor.SetVisibility(0);
            }
        }

        private cMetaBiologicalObject MetaObjectContainer = null;

        internal void DefineMetaContainer(cMetaBiologicalObject cMetaBiologicalObjectContainer)
        {
            this.MetaObjectContainer = cMetaBiologicalObjectContainer;
        }

        public cMetaBiologicalObject GetMetaObjectContainer()
        {
            return this.MetaObjectContainer;
        }

        //public void SetColor(Color Colour)
        //{
        //    this.GetActor().GetProperty().SetColor(Colour.R / 255.0, Colour.G / 255.0, Colour.B / 255.0);
        //}
    }

    //public class CImage3D
    //{
    //    public int Width;
    //    public int Height;
    //    public int Depth;
    //    public double[] Data;

    //    public int ImageSize;

    //}


    /// <summary>
    /// cBiological3DVolume
    /// </summary>
    public class cBiological3DVolume : cInteractive3DObject
    {
        public cMeshSmoother MeshSmoother = null;
        public cContainer Containers = null;

        private vtkPolyData vtk_PolyData;

        public cInformation Information;
        private vtkHull hullFilter;
        vtkActor hullActor;
        public ConnectedVoxels AssociatedConnectedComponent;

        public vtkPolyData GetPolydata()
        {
            return this.vtk_PolyData;
        }

        public cPoint3D GetPosition()
        {
            return new cPoint3D((float)this.GetActor().GetPosition()[0], (float)this.GetActor().GetPosition()[1], (float)this.GetActor().GetPosition()[2]);
        }

        public class cInformation
        {
            vtkAlgorithmOutput ContourObject;
            cBiological3DVolume CurrentBiologicalObject;
            vtkHull hullFilter;

            public cInformation(vtkAlgorithmOutput ContourObject, cBiological3DVolume CurrentBiologicalObject, vtkHull hullFilter)
            {
                this.ContourObject = ContourObject;
                this.CurrentBiologicalObject = CurrentBiologicalObject;
                this.hullFilter = hullFilter;
            }

            /// <summary>
            /// Return a list of 3 values, respectively: Volume, Surface and compactness
            /// </summary>
            /// <returns></returns>
            private double[] ComputeVolSurfComp()
            {
                vtkTriangleFilter TriangleFilter = vtkTriangleFilter.New();
                TriangleFilter.SetInputConnection(ContourObject);
                vtkMassProperties MassProperties = new vtkMassProperties();
                MassProperties.SetInputConnection(TriangleFilter.GetOutputPort());

                double Volume = MassProperties.GetVolume();
                double Surface = MassProperties.GetSurfaceArea();
                double Compactness = MassProperties.GetNormalizedShapeIndex();
                double[] Res = new double[3];
                Res[0] = Volume;
                Res[1] = Surface;
                Res[2] = Compactness;

                return Res;
            }

            private double ComputeHullRatio(double ObjectVolume)
            {
                vtkTriangleFilter TriangleFilter = vtkTriangleFilter.New();
                TriangleFilter.SetInputConnection(hullFilter.GetOutputPort());
                vtkMassProperties MassProperties = new vtkMassProperties();
                MassProperties.SetInputConnection(TriangleFilter.GetOutputPort());
                return MassProperties.GetVolume() / ObjectVolume;
            }

            #region MutualInformation
            /// <summary>
            /// Compute the mutual information between two 3d images 
            /// </summary>
            /// <param name="Image0">first image</param>
            /// <param name="Channel0">first channel</param>
            /// <param name="Image1">second image</param>
            /// <param name="Channel1">second channel</param>
            /// <returns>the mutual information</returns>
            //public float ComputeMutualInformation(float[] Data0, float[] Data1)
            //{
            //    float[,] JointHistogram;
            //    JointHistogram = this.CreateJointHistogram(Data0, Data1);

            //    int JointHisto1Lentgh = JointHistogram.GetLength(1);
            //    int JointHisto0Lentgh = JointHistogram.GetLength(0);

            //    float MI = 0.0f;
            //    float sum_a_b = 0.0f;
            //    float[,] Prob_Conj = new float[JointHisto0Lentgh, JointHisto1Lentgh];

            //    for (int j = 0; j < JointHisto1Lentgh; j++)
            //        for (int i = 0; i < JointHisto0Lentgh; i++)
            //        {
            //            sum_a_b += JointHistogram[i, j];
            //        }

            //    for (int j = 0; j < JointHisto1Lentgh; j++)
            //        for (int i = 0; i < JointHisto0Lentgh; i++)
            //        {
            //            Prob_Conj[i, j] = JointHistogram[i, j] / sum_a_b;
            //        }

            //    ///////Prob A//////
            //    ///////Prob B//////
            //    float[] Prob_A = new float[JointHisto1Lentgh];
            //    float[] Prob_B = new float[JointHisto0Lentgh];
            //    for (int j = 0; j < JointHisto1Lentgh; j++)
            //    {
            //        for (int i = 0; i < JointHisto0Lentgh; i++)
            //        {
            //            Prob_A[j] += Prob_Conj[i, j];
            //            Prob_B[i] += Prob_Conj[i, j];
            //        }
            //    }
            //    ///////Mutal information////////////
            //    for (int j = 0; j < JointHisto1Lentgh; j++)
            //        for (int i = 0; i < JointHistogram.GetLength(0); i++)
            //        {
            //            if (Prob_Conj[i, j] != 0 && Prob_A[j] != 0 && Prob_B[i] != 0)
            //                MI += (float)(Prob_Conj[i, j] * Math.Log(Prob_Conj[i, j] / (Prob_A[j] * Prob_B[i]), 2));
            //        }
            //    return MI;
            //}

            //private float[,] CreateJointHistogram(float[] Data0, float[] Data1)
            //{
            //    float max;
            //    float max2;
            //    max = new IM.Library.Mathematics.MathTools().Max(Data0);
            //    max2 = new IM.Library.Mathematics.MathTools().Max(Data1);

            //    float[,] Result = new float[(int)max + 1, (int)max2 + 1];

            //    for (int i = 0; i < Data0.Length; i++)
            //    {
            //        Result[(int)Data0[i], (int)Data1[i]]++;
            //    }
            //    return Result;
            //}

            #endregion

            public List<string> GetDescriptorNames()
            {
                List<string> DescriptorNames = new List<string>();
                DescriptorNames.Add("Volume");
                DescriptorNames.Add("Surface");
                DescriptorNames.Add("Compactness");
                DescriptorNames.Add("Z_Center");
                DescriptorNames.Add("Hull_Ratio");
                if (CurrentBiologicalObject.AssociatedConnectedComponent.Intensity != null)
                {
                    for (int i = 0; i < CurrentBiologicalObject.AssociatedConnectedComponent.Intensity.Length; i++)
                        DescriptorNames.Add("Intensity_" + i);

                    for (int i = 0; i < CurrentBiologicalObject.AssociatedConnectedComponent.Intensity.Length; i++)
                        DescriptorNames.Add("Average_Intensity_" + i);

                    for (int j = 0; j < CurrentBiologicalObject.AssociatedConnectedComponent.Intensity.Length; j++)
                        for (int i = 0; i < CurrentBiologicalObject.AssociatedConnectedComponent.Intensity.Length; i++)
                        {
                            if (i <= j) continue;
                            DescriptorNames.Add("Correlation " + i + "_" + j);
                        }


                    for (int j = 0; j < CurrentBiologicalObject.AssociatedConnectedComponent.Intensity.Length; j++)
                        for (int i = 0; i < CurrentBiologicalObject.AssociatedConnectedComponent.Intensity.Length; i++)
                        {
                            if (i <= j) continue;
                            DescriptorNames.Add("Mutual Information " + i + "_" + j);
                        }


                }





                return DescriptorNames;
            }

            public List<double> GetInformation()
            {
                List<double> DescriptorList = new List<double>();
                double[] Res1 = ComputeVolSurfComp();
                DescriptorList.Add(Res1[0]);
                DescriptorList.Add(Res1[1]);
                DescriptorList.Add(Res1[2]);
                DescriptorList.Add((double)(this.CurrentBiologicalObject.GetCentroid().Z));
                DescriptorList.Add((double)(this.ComputeHullRatio(Res1[0])));
                if (CurrentBiologicalObject.AssociatedConnectedComponent.Intensity != null)
                {
                    for (int i = 0; i < CurrentBiologicalObject.AssociatedConnectedComponent.Intensity.Length; i++)
                        DescriptorList.Add((double)CurrentBiologicalObject.AssociatedConnectedComponent.Intensity[i]);

                    for (int i = 0; i < CurrentBiologicalObject.AssociatedConnectedComponent.Intensity.Length; i++)
                        DescriptorList.Add((double)CurrentBiologicalObject.AssociatedConnectedComponent.Intensity[i] / Res1[0]);


                    List<float[]> ListIntensity = new List<float[]>();
                    for (int j = 0; j < CurrentBiologicalObject.AssociatedConnectedComponent.Intensity.Length; j++)
                    {
                        float[] ArrayValues = new float[CurrentBiologicalObject.AssociatedConnectedComponent.Values.Count];

                        for (int k = 0; k < ArrayValues.Length; k++)
                            ArrayValues[k] = CurrentBiologicalObject.AssociatedConnectedComponent.Values[k][j];

                        ListIntensity.Add(ArrayValues);

                    }

                    //for (int j = 0; j < CurrentBiologicalObject.AssociatedConnectedComponent.Intensity.Length; j++)
                    //    for (int i = 0; i < CurrentBiologicalObject.AssociatedConnectedComponent.Intensity.Length; i++)
                    //    {
                    //        if (i <= j) continue;

                    //        DescriptorList.Add(new IM.Library.Mathematics.ArrayDistance().Coeff_Correlation1D(ListIntensity[i], ListIntensity[j]));
                    //    }



                    //for (int j = 0; j < CurrentBiologicalObject.AssociatedConnectedComponent.Intensity.Length; j++)
                    //    for (int i = 0; i < CurrentBiologicalObject.AssociatedConnectedComponent.Intensity.Length; i++)
                    //    {
                    //        if (i <= j) continue;

                    //        DescriptorList.Add(ComputeMutualInformation(ListIntensity[i], ListIntensity[j]));
                    //    }



                }

                return DescriptorList;
            }
        }

        public bool Detected = true;

        public void DisplayHull(vtkRenderer Currentrenderer)
        {
            if (hullActor == null)
            {
                vtkPolyDataMapper mapHull = vtkPolyDataMapper.New();
                mapHull.SetInput(hullFilter.GetOutput());
                hullActor = vtkActor.New();
                hullActor.SetMapper(mapHull);
                hullActor.SetPosition(GetPosition().X, GetPosition().Y, GetPosition().Z);
                // hullActor.GetProperty().SetRepresentationToWireframe();
                hullActor.GetProperty().SetOpacity(0.3);

                Currentrenderer.AddActor(hullActor);
            }
            else
            {
                if (hullActor.GetVisibility() == 0)
                    hullActor.SetVisibility(1);
                else
                    hullActor.SetVisibility(0);
            }
        }

        public cBiological3DVolume()
        {

        }


        //private void CommonInit(Sequence BinarySubImageSeq)
        //{
        //    vtkImageData ImageData1 = new vtkImageData();

        //    ImageData1.SetDimensions(BinarySubImageSeq.Width, BinarySubImageSeq.Height, BinarySubImageSeq.Depth);
        //    ImageData1.SetNumberOfScalarComponents(1);
        //    ImageData1.SetSpacing(BinarySubImageSeq.XResolution, BinarySubImageSeq.YResolution, BinarySubImageSeq.ZResolution);
        //    ImageData1.SetScalarTypeToFloat();

        //    vtkFloatArray array1 = new vtkFloatArray();
        //    for (int i = 0; i < BinarySubImageSeq.ImageSize; i++)
        //        array1.InsertTuple1(i, BinarySubImageSeq[0].Data[0][i]);
        //    ImageData1.GetPointData().SetScalars(array1);

        //    vtkExtractVOI VOI = new vtkExtractVOI();
        //    VOI.SetInput(ImageData1);
        //    VOI.SetSampleRate(1, 1, 1);

        //    vtkMarchingCubes ContourObject = vtkMarchingCubes.New();
        //    vtk_PolyDataMapper = vtkPolyDataMapper.New();
        //    //ContourActor = new vtkActor();

        //    VOI.SetVOI(0, BinarySubImageSeq.Width - 1, 0, BinarySubImageSeq.Height - 1, 0, BinarySubImageSeq.Depth - 1);
        //    ContourObject.SetInput(VOI.GetOutput());
        //    ContourObject.SetValue(0, 0.5);

        //    vtk_PolyDataMapper.SetInput(ContourObject.GetOutput());
        //    vtk_PolyDataMapper.ScalarVisibilityOn();
        //    vtk_PolyDataMapper.SetScalarModeToUseFieldData();

        //}




        ///// <summary>
        ///// Generate a 3D mesh using marching-cubes algorithm. If voxel value is lower than 1 it is consider as background, else as object
        ///// </summary>
        ///// <param name="BinarySubImageSeq">The binary image</param>
        ///// <param name="Colour">Mesh color</param>
        ///// <param name="Pos">Postion of the object in the world</param>
        //public void Generate(Sequence BinarySubImageSeq, Color Colour, cPoint3D Pos)
        //{
        //    vtkImageData ImageData1 = new vtkImageData();
        //    ImageData1.SetDimensions(BinarySubImageSeq.Width, BinarySubImageSeq.Height, BinarySubImageSeq.Depth);
        //    ImageData1.SetNumberOfScalarComponents(1);
        //    ImageData1.SetSpacing(BinarySubImageSeq.XResolution, BinarySubImageSeq.YResolution, BinarySubImageSeq.ZResolution);
        //    ImageData1.SetScalarTypeToFloat();

        //    vtkFloatArray array1 = new vtkFloatArray();
        //    for (int i = 0; i < BinarySubImageSeq.ImageSize; i++)
        //        array1.InsertTuple1(i, BinarySubImageSeq[0].Data[0][i]);
        //    ImageData1.GetPointData().SetScalars(array1);

        //    vtkExtractVOI VOI = new vtkExtractVOI();
        //    VOI.SetInput(ImageData1);
        //    VOI.SetSampleRate(1, 1, 1);

        //    vtkMarchingCubes ContourObject = vtkMarchingCubes.New();
        //    vtk_PolyDataMapper = vtkPolyDataMapper.New();
        //    //ContourActor = new vtkActor();

        //    VOI.SetVOI(0, BinarySubImageSeq.Width - 1, 0, BinarySubImageSeq.Height - 1, 0, BinarySubImageSeq.Depth - 1);

        //    // perform the amrching cubes
        //    ContourObject.SetInput(VOI.GetOutput());
        //    ContourObject.SetValue(0, 0.5);

        //    //vtkDecimatePro deci
        //    //deci SetInputConnection [fran GetOutputPort]
        //    //deci SetTargetReduction 0.9
        //    //deci PreserveTopologyOn

        //    if (MeshSmoother!=null)
        //    {
        //        vtkSmoothPolyDataFilter smoother = new vtkSmoothPolyDataFilter();
        //        smoother.SetInputConnection(ContourObject.GetOutputPort());// [deci GetOutputPort]
        //        smoother.SetNumberOfIterations(50);
        //        vtk_PolyData = smoother.GetOutput();
        //    }
        //    else
        //    {
        //        vtk_PolyData = ContourObject.GetOutput();
        //    }

        //    vtk_PolyDataMapper.SetInput(vtk_PolyData);
        //    vtk_PolyDataMapper.ScalarVisibilityOn();
        //    vtk_PolyDataMapper.SetScalarModeToUseFieldData();

        //    this.Position = new cPoint3D(Pos.X, Pos.Y, Pos.Z);
        //    this.Colour = Colour;

        //    CreateVTK3DObject(1);

        //    vtk_PolyData = ContourObject.GetOutput();


        //    // compute convex hull                
        //    hullFilter = vtkHull.New();
        //    hullFilter.SetInputConnection(ContourObject.GetOutputPort());
        //    hullFilter.AddRecursiveSpherePlanes(1);
        //    hullFilter.Update();

        //    //  this.BackfaceCulling(false);
        //    Information = new cInformation(ContourObject, this, hullFilter);
        //}


        public void Generate(cImage BinarySubImageSeq, Color Colour, cPoint3D Pos/*, List<cBiological3DObject> Containers, int ContainerMode*/)
        {
            vtkImageData ImageData1 = new vtkImageData();

            ImageData1.SetDimensions(BinarySubImageSeq.Width, BinarySubImageSeq.Height, BinarySubImageSeq.Depth);
            ImageData1.SetNumberOfScalarComponents(1);
            ImageData1.SetSpacing(BinarySubImageSeq.Resolution.X, BinarySubImageSeq.Resolution.Y, BinarySubImageSeq.Resolution.Z);
            ImageData1.SetScalarTypeToFloat();

            vtkFloatArray array1 = new vtkFloatArray();
            for (int i = 0; i < BinarySubImageSeq.ImageSize; i++)
                array1.InsertTuple1(i, BinarySubImageSeq.SingleChannelImage[0].Data[i]);
            ImageData1.GetPointData().SetScalars(array1);

            vtkExtractVOI VOI = new vtkExtractVOI();
            VOI.SetInput(ImageData1);
            VOI.SetSampleRate(1, 1, 1);

            vtkMarchingCubes ContourObject = vtkMarchingCubes.New();
            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            //ContourActor = new vtkActor();

            VOI.SetVOI(0, BinarySubImageSeq.Width - 1, 0, BinarySubImageSeq.Height - 1, 0, BinarySubImageSeq.Depth - 1);
            ContourObject.SetInput(VOI.GetOutput());
            ContourObject.SetValue(0, 0.5);


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
                            ContainerIdx = Idx;
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

                        Information = new cInformation(AlgoOutPut, this, hullFilter);
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
                    hullFilter = vtkHull.New();
                    hullFilter.SetInputConnection(AlgoOutPut);
                    hullFilter.AddRecursiveSpherePlanes(0);
                    hullFilter.Update();

                    Information = new cInformation(AlgoOutPut, this, hullFilter);
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
                hullFilter = vtkHull.New();
                hullFilter.SetInputConnection(AlgoOutPut);
                hullFilter.AddRecursiveSpherePlanes(1);
                hullFilter.Update();

                //  this.BackfaceCulling(false);
                Information = new cInformation(AlgoOutPut, this, hullFilter);




            }

            #endregion



        }

        public bool IsPointInside(cPoint3D Pt)
        {
            vtkSelectEnclosedPoints vtk_SelectEnclosedPoints = vtkSelectEnclosedPoints.New();
            vtk_SelectEnclosedPoints.SetSurface(vtk_PolyData);

            double[] testInside = new double[3];
            vtkPoints points = vtkPoints.New();

            //points.InsertNextPoint(vtk_PolyData.GetCenter()[0], vtk_PolyData.GetCenter()[1], vtk_PolyData.GetCenter()[2]);

            // Console.WriteLine("PolyDataCenter: X " + vtk_PolyData.GetCenter()[0] + " Y " + vtk_PolyData.GetCenter()[1] + " Z "+vtk_PolyData.GetCenter()[2]);

            points.InsertNextPoint(Pt.X - vtk_Actor.GetPosition()[0], Pt.Y - vtk_Actor.GetPosition()[1], Pt.Z - vtk_Actor.GetPosition()[2]);

            vtkPolyData pointsPolydata = vtkPolyData.New();
            pointsPolydata.SetPoints(points);

            vtk_SelectEnclosedPoints.SetInput(pointsPolydata);
            vtk_SelectEnclosedPoints.Update();

            int Res = vtk_SelectEnclosedPoints.IsInside(0);

            //Console.WriteLine("" + vtk_SelectEnclosedPoints.IsInside(0) + ";" + vtk_SelectEnclosedPoints.IsInside(1) + ";" + vtk_SelectEnclosedPoints.IsInside(2));

            if (Res == 1)
                return true;
            else
                return false;
        }
    }

    /// <summary>
    /// cBiologicalSpot
    /// </summary>
    public class cBiologicalSpot : cInteractive3DObject
    {
        // vtkPolyData vtk_PolyData;
        public cInformation Information;
        public int Radius = 4;
        vtkSphereSource VTK_Sphere;
        double Intensity = -1;

        public class cInformation
        {
            cBiologicalSpot CurrentBiologicalObject;

            public cInformation(cBiologicalSpot CurrentBiologicalObject)
            {
                this.CurrentBiologicalObject = CurrentBiologicalObject;

            }

            public List<string> GetDescriptorNames()
            {
                List<string> DescriptorNames = new List<string>();
                DescriptorNames.Add("Intensity");
                DescriptorNames.Add("Z_Center");
                return DescriptorNames;
            }

            public List<double> GetInformation()
            {
                List<double> DescriptorList = new List<double>();
                DescriptorList.Add((double)(this.CurrentBiologicalObject.Intensity));
                DescriptorList.Add((double)(this.CurrentBiologicalObject.GetCentroid().Z));
                return DescriptorList;
            }
        }

        public bool Detected = true;

        /// <summary>
        /// Generate a 3D mesh using marching-cubes algorithm. If voxel value is lower than 1 it is consider as background, else as object
        /// </summary>
        /// <param name="BinarySubImageSeq">The binary image</param>
        /// <param name="Colour">Mesh color</param>
        /// <param name="Pos">Postion of the object in the world</param>
        public cBiologicalSpot(Color Colour, cPoint3D Pos, double Intensity, double Radius)
        {
            this.Intensity = Intensity;
            VTK_Sphere = vtkSphereSource.New();
            VTK_Sphere.SetThetaResolution(6);
            VTK_Sphere.SetPhiResolution(6);
            VTK_Sphere.SetRadius(Radius);
            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            vtk_PolyDataMapper.SetInputConnection(VTK_Sphere.GetOutputPort());

            this.SetPosition(new cPoint3D(Pos.X, Pos.Y, Pos.Z));
            this.Colour = Colour;

            CreateVTK3DObject(1);
            Information = new cInformation(this);
        }

        public cBiologicalSpot(Color Colour, cPoint3D Pos, double Intensity, double Radius, List<cInteractive3DObject> Containers, int ContainerMode)
        {
            this.Intensity = Intensity;

            this.Detected = true;

            this.Intensity = Intensity;
            VTK_Sphere = vtkSphereSource.New();
            VTK_Sphere.SetThetaResolution(10);
            VTK_Sphere.SetPhiResolution(10);
            VTK_Sphere.SetRadius(Radius);
            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            vtk_PolyDataMapper.SetInputConnection(VTK_Sphere.GetOutputPort());

            this.SetPosition(new cPoint3D(Pos.X, Pos.Y, Pos.Z));
            this.Colour = Colour;

            CreateVTK3DObject(1);
            Information = new cInformation(this);

            this.Detected = true;

            /*
            vtkActor TmpActor = vtkActor.New();
            TmpActor.SetMapper(vtk_PolyDataMapper);
            TmpActor.SetPosition(Pos.X, Pos.Y, Pos.Z);

            //Console.WriteLine("PosX"+Pos.X+" PosY"+Pos.Y+" PosZ"+Pos.Z);
            cPoint3D Centroid = new cPoint3D((float)TmpActor.GetCenter()[0], (float)TmpActor.GetCenter()[1], (float)TmpActor.GetCenter()[2]);


            bool IsInside = false;
            for (int Idx = 0; Idx < Containers.Count; Idx++)
            {
                cBiological3DVolume CurrentContainer = (cBiological3DVolume)(Containers[Idx]);

                if (CurrentContainer.IsPointInside(Centroid))
                {
                    IsInside = true;
                    ContainerIdx = Idx;
                    break;
                }
            }
            if (IsInside)
            {
                this.Position = new cPoint3D(Pos.X, Pos.Y, Pos.Z);
                this.Colour = Colour;

                CreateVTK3DObject(1);

                vtk_PolyData = ContourObject.GetOutput();
                //  this.BackfaceCulling(false);

                Information = new cInformation(ContourObject, this);
                this.Detected = true;
            }
            else
            {
                this.Detected = false;
            }*/

        }

    }

    public class c3DWell : cInteractive3DObject
    {
        // vtkPolyData vtk_PolyData;
        public cInformation Information;

        //   vtkSphereSource VTK_Sphere;
        double Intensity = -1;
        public cWell AssociatedWell = null;

        public class cInformation
        {
            c3DWell CurrentBiologicalObject;


            public cInformation(c3DWell CurrentBiologicalObject)
            {
                this.CurrentBiologicalObject = CurrentBiologicalObject;

            }

            public List<string> GetDescriptorNames()
            {
                List<string> DescriptorNames = new List<string>();

                foreach (cDescriptorType Desc in CurrentBiologicalObject.AssociatedWell.AssociatedPlate.ParentScreening.ListDescriptors)
                {
                    if (Desc.IsActive() == false) continue;
                    DescriptorNames.Add(Desc.GetName());

                }
                //   CurrentBiologicalObject.AssociatedWell.AssociatedPlate.ParentScreening.ListDescriptors[


                return DescriptorNames;
            }

            public List<double> GetInformation()
            {
                List<double> DescriptorList = new List<double>();

                int IdxDesc = -1;
                foreach (cDescriptorType Desc in CurrentBiologicalObject.AssociatedWell.AssociatedPlate.ParentScreening.ListDescriptors)
                {
                    IdxDesc++;
                    if (Desc.IsActive() == false) continue;
                    DescriptorList.Add(CurrentBiologicalObject.AssociatedWell.ListSignatures[IdxDesc].GetValue());
                }

                return DescriptorList;
            }
        }

        public bool Detected = true;



        public vtkCubeSource VTK_Cube;

        public c3DWell(cPoint3D MinPt, cPoint3D MaxPt, Color Colour, cWell CurrentWell)
        {
            this.SetPosition(new cPoint3D(0, 0, 0));

            this.Colour = Colour;
            VTK_Cube = vtkCubeSource.New();
            //VTK_Cube.SetBounds(MinPt.X, MaxPt.X, MinPt.Y, MaxPt.Y, MinPt.Z, MaxPt.Z);
            VTK_Cube.SetXLength(MaxPt.X - MinPt.X);
            VTK_Cube.SetYLength(MaxPt.Y - MinPt.Y);
            VTK_Cube.SetZLength(MaxPt.Z - MinPt.Z);


            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            vtk_PolyDataMapper.SetInputConnection(VTK_Cube.GetOutputPort());

            this.SetPosition(MinPt);
            this.Colour = Colour;
            this.AssociatedWell = CurrentWell;

            CreateVTK3DObject(1);
            Information = new cInformation(this);
        }


    }

    public class c3DIsoContours : cInteractive3DObject //cGeometric3DObject
    {
        public cListPoints3D ListPtContour = new cListPoints3D();

        private void Create(cGeometric3DObject AssociatedObject, cDRC_Region AssociatedRegion, Color Color, double ZPosition, bool IsIsoboles)
        {
            if (AssociatedObject.AlgoOutPut == null) return;

            SetPosition(new cPoint3D(AssociatedObject.GetActor().GetPosition()[0], AssociatedObject.GetActor().GetPosition()[1], 0));// AssociatedObject.GetActor().GetPosition()[2]);
            //Position = new cPoint3D(0, 0, 0);// AssociatedObject.GetActor().GetPosition()[2]);

            vtkPlane plane = vtkPlane.New();

            // isoboles
            if (IsIsoboles)
            {
                plane.SetOrigin(AssociatedObject.GetActor().GetPosition()[0], AssociatedObject.GetActor().GetPosition()[1], ZPosition);
                plane.SetNormal(0, 0, 1);
            }
            else
            {
                plane.SetOrigin(AssociatedObject.GetPosition().X - AssociatedObject.GetActor().GetPosition()[0], AssociatedObject.GetPosition().Y - AssociatedObject.GetActor().GetPosition()[1], 0);
                plane.SetNormal(AssociatedRegion.SizeY - 1 + 2 * ZPosition, -AssociatedRegion.SizeX, 0);

                // vtkPlaneSource PlaneSource = vtkPlaneSource.New();
                //PlaneSource.SetOrigin(AssociatedObject.Position.X, AssociatedObject.Position.Y, 0);
                //PlaneSource.SetNormal(5, -10, 0);
            }

            // Create cutter
            vtkCutter cutter = vtkCutter.New();

            cutter.SetCutFunction(plane);
            cutter.SetInputConnection(AssociatedObject.AlgoOutPut);
            cutter.Update();


            vtkPoints Pts = cutter.GetOutput().GetPoints();
            int NumPts = (int)Pts.GetNumberOfPoints();
            for (int IdxPt = 0; IdxPt < NumPts; IdxPt++)
            {

                double[] Pt = Pts.GetPoint(IdxPt);
                cPoint3D NewPt = new cPoint3D(Pt[0], Pt[1], Pt[2]);
                ListPtContour.Add(NewPt);
            }



            vtkTubeFilter tubeFilter = vtkTubeFilter.New();

            tubeFilter.SetInputConnection(cutter.GetOutputPort());
            tubeFilter.SetRadius(.035); //default is .5
            tubeFilter.SetNumberOfSides(50);
            tubeFilter.Update();


            //     vtkPolyDataMapper cutterMapper = vtkPolyDataMapper.New();

            vtk_PolyDataMapper = vtkPolyDataMapper.New();
            vtk_PolyDataMapper.SetInputConnection(/*PlaneSource.GetOutputPort());*/ tubeFilter.GetOutputPort());

            // Create plane actor
            //vtkActor planeActor = vtkActor.New();

            //planeActor.SetMapper(vtk_PolyDataMapper);

            CreateVTK3DObject(0);

            SetColor(Color);
            SetOpacity(1);
            SetToSurface();
        }

        public c3DIsoContours(cGeometric3DObject AssociatedObject, cDRC_Region AssociatedRegion, Color Color, double Zposition, bool IsIsoboles)
        {
            Create(AssociatedObject, AssociatedRegion, Color, Zposition, IsIsoboles);
        }

        public c3DIsoContours(cGeometric3DObject AssociatedObject, cDRC_Region AssociatedRegion, double Zposition, bool IsIsoboles)
        {
            Create(AssociatedObject, AssociatedRegion, Color.White, Zposition, IsIsoboles);
        }
    }
}
