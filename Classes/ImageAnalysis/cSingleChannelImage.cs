using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.MetaComponents;
using ImageAnalysis;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Windows.Forms;
using HCSAnalyzer;
using System.Drawing;
using HCSAnalyzer.Forms.FormsForImages;
using HCSAnalyzer.Classes._3D;
using IM3_Plugin3;
using HCSAnalyzer.Classes.Base_Classes.Viewers._3D.ComplexObjects;
using HCSAnalyzer.Classes;

namespace ImageAnalysis
{
    public partial class cSingleChannelImage : IDisposable
    {
        public float[] Data;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Depth { get; private set; }
        public string Name = "Single Channel Image";

        public float Min { get; private set; }
        public float Max { get; private set; }

        public cPoint3D Resolution;
        cImagePanel CurrentAssociatedImageViewer = null;

        public void Dispose()
        {
            Data = null;
            GC.SuppressFinalize(this);
        }

        public void UpDateMin()
        {
            this.Min = float.MaxValue;
            for (int i = 0; i < Data.Length; i++)
                if (Data[i] < this.Min) this.Min = Data[i];
        }

        public void UpDateMax()
        {
            this.Max = float.MinValue;
            for (int i = 0; i < Data.Length; i++)
                if (Data[i] > this.Max) this.Max = Data[i];
        }

        public double[] ComputeMinMax()
        {
            double[] MinMax = new double[2];

            this.UpDateMax();
            this.UpDateMin();

            MinMax[0] = this.Min;
            MinMax[1] = this.Max;

            return MinMax;

        }



        public cSingleChannelImage(int Width, int Height, int Depth, cPoint3D Resolution)
        {
            this.Resolution = new cPoint3D(Resolution.X, Resolution.Y, Resolution.Z);

            this.Width = Width;
            this.Height = Height;
            this.Depth = Depth;

            Data = new float[Width * Height * Depth];
            //this.Min = this.Data.Min();
            //this.Max = this.Data.Max(); 
        }


        public double GetTotalIntensity(cPoint3D StartingPoint, cPoint3D EndingPoint)
        {
            double ToReturn = 0;

            int RealXStartPt = (int)StartingPoint.X;
            if (RealXStartPt < 0) RealXStartPt = 0;
            else if (RealXStartPt >= this.Width) RealXStartPt = this.Width - 1;

            int RealXEndPt = (int)EndingPoint.X;
            if (RealXEndPt < 0) RealXEndPt = 0;
            else if (RealXEndPt >= this.Width) RealXEndPt = this.Width - 1;

            int RealYStartPt = (int)StartingPoint.Y;
            if (RealYStartPt < 0) RealYStartPt = 0;
            else if (RealYStartPt >= this.Height) RealYStartPt = this.Height - 1;

            int RealYEndPt = (int)EndingPoint.Y;
            if (RealYEndPt < 0) RealYEndPt = 0;
            else if (RealYEndPt >= this.Height) RealYEndPt = this.Height - 1;


            int RealPosZ;
            int RealPosY;
            int RealPosX;

            int SliceSize = this.Width * this.Height;

            for (int z = (int)StartingPoint.Z; z <= (int)EndingPoint.Z; z++)
            {
                RealPosZ = (int)(z - StartingPoint.Z);
                for (int y = (int)RealYStartPt; y <= (int)RealYEndPt; y++)
                {
                    RealPosY = (int)(y - RealYStartPt);
                    for (int x = (int)RealXStartPt; x <= (int)RealXEndPt; x++)
                    {
                        RealPosX = (int)(x - RealXStartPt);
                        ToReturn += this.Data[x + y * this.Width + z * SliceSize];
                    }
                }
            }

            return ToReturn;
        }


        public Image<Gray, float> ConvertToCVImage(int SliceIdx)
        {
            Image<Gray, float> ImageToBeReturned = new Image<Gray, float>(Width, Height);

            for (int j = 0; j < Height; j++)
                for (int i = 0; i < Width; i++)
                    ImageToBeReturned.Data[j, i, 0] = this.Data[i + j * this.Width + SliceIdx * this.Width * this.Height];

            return ImageToBeReturned;
        }

        public cExtendedTable ConvertToTable(int SliceIndex)
        {
            cExtendedTable ToBeReturned = new cExtendedTable(this.Width, this.Height, 0);

            for (int j = 0; j < this.Height; j++)
                for (int i = 0; i < this.Width; i++)
                    ToBeReturned[i][j] = this.Data[i + j * this.Width + SliceIndex * this.Width * this.Height];

            return ToBeReturned;
        }

        public void DrawRectange(Rectangle Rec, float Intensity)
        {
            for (int X = Rec.X; X <= Rec.X + Rec.Width; X++)
            {
                this.Data[X + Rec.Y * this.Width] = Intensity;
                this.Data[X + (Rec.Y + Rec.Height) * this.Width] = Intensity;
            }

            for (int Y = Rec.Y; Y <= Rec.Y + Rec.Height; Y++)
            {
                this.Data[Rec.X + Y * this.Width] = Intensity;
                this.Data[Rec.X + Rec.Width + Y * this.Width] = Intensity;
            }
        }

        public bool SetNewDataFromOpenCV(Image<Gray, float> CVImage)
        {
            if (CVImage.Width * CVImage.Height != this.Data.Length) return false;

            for (int j = 0; j < CVImage.Height; j++)
                for (int i = 0; i < CVImage.Width; i++)
                    this.Data[i + j * this.Width] = CVImage.Data[j, i, 0];
            return true;
        }

        public bool SetNewDataFromOpenCV(Image<Gray, byte> CVImage)
        {
            if (CVImage.Width * CVImage.Height != this.Data.Length) return false;

            for (int j = 0; j < CVImage.Height; j++)
                for (int i = 0; i < CVImage.Width; i++)
                    this.Data[i + j * this.Width] = CVImage.Data[j, i, 0];
            return true;
        }
        public bool SetNewDataFromOpenCV(Image<Gray, Int16> CVImage)
        {
            if (CVImage.Width * CVImage.Height != this.Data.Length) return false;

            for (int j = 0; j < CVImage.Height; j++)
                for (int i = 0; i < CVImage.Width; i++)
                    this.Data[i + j * this.Width] = CVImage.Data[j, i, 0];
            return true;
        }

        public ToolStripMenuItem GetContextMenu(cImagePanel CurrentImageViewer)
        {
            CurrentAssociatedImageViewer = CurrentImageViewer;

            ToolStripMenuItem SpecificContextMenu = new ToolStripMenuItem(this.Name);

            ToolStripMenuItem ToolStripMenuItem_DisplayHistogram = new ToolStripMenuItem("Histogram");
            ToolStripMenuItem_DisplayHistogram.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayHistogram);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayHistogram);

            ToolStripMenuItem ToolStripMenuItem_DisplayStats = new ToolStripMenuItem("Statistics");
            ToolStripMenuItem_DisplayStats.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayStats);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayStats);

            ToolStripMenuItem ToolStripMenuItem_DisplayAS = new ToolStripMenuItem("Display as...");

            ToolStripMenuItem ToolStripMenuItem_DisplayDataTable = new ToolStripMenuItem("Data Table");
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_DisplayDataTable);
            ToolStripMenuItem_DisplayDataTable.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayDataTable);

            ToolStripMenuItem ToolStripMenuItem_DisplayElevationMap = new ToolStripMenuItem("Elevation Map (Z=" + (int)this.CurrentAssociatedImageViewer.ZNavigator.numericUpDownZPos.Value + ")");
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_DisplayElevationMap);
            ToolStripMenuItem_DisplayElevationMap.Click += new System.EventHandler(this.DisplayElevationMap);


            if (this.Depth > 1)
            {
                ToolStripMenuItem ToolStripMenuItem_Display3DVolume = new ToolStripMenuItem("3D Volume");
                ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_Display3DVolume);
                ToolStripMenuItem_Display3DVolume.Click += new System.EventHandler(this.ToolStripMenuItem_Display3DVolume);
            }


            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayAS);

            ToolStripMenuItem ToolStripMenuItem_ExtractChannel = new ToolStripMenuItem("Extract Channel as Image");
            ToolStripMenuItem_ExtractChannel.Click += new System.EventHandler(this.ToolStripMenuItem_ExtractChannel);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ExtractChannel);

            return SpecificContextMenu;
        }

        private void ToolStripMenuItem_Display3DVolume(object sender, EventArgs e)
        {
            c3DNewWorld MyWorld = new c3DNewWorld(new cPoint3D(this.Width, this.Height, this.Depth),
                                                    this.Resolution);

            c3DObject_Axis Axis = new c3DObject_Axis();
            cExtendedTable T = new cExtendedTable();
            T.Add(new cExtendedList("X"));
            T[0].Add(0);
            T[0].Add(this.Width * this.Resolution.X);
            T.Add(new cExtendedList("Y"));
            T[1].Add(0);
            T[1].Add(this.Height * this.Resolution.Y);
            T.Add(new cExtendedList("Z"));
            T[2].Add(0);
            T[2].Add(this.Depth * this.Resolution.Z);
            Axis.SetInputData(T);
            Axis.Run(MyWorld);

            List<cListGeometric3DObject> GlobalList = new List<cListGeometric3DObject>();
            GlobalList.Add(Axis.GetOutPut());

            //c3DMeshObject MyMesh = new c3DMeshObject(this, 150.0, Color.Aqua, new cPoint3D(0, 0, 0));
            //cListGeometric3DObject TmpListMesh = new cListGeometric3DObject();
            //c3DObjectMeshFromImage MyMesh = new c3DObjectMeshFromImage();
            //MyMesh.SetInputData(this);
            
            //MyMesh.ListProperties.FindByName("Thresold").SetNewValue((double)150.0);
            //MyMesh.ListProperties.FindByName("Thresold").PropertyType.Max = (double)this.Max;
            //MyMesh.ListProperties.FindByName("Thresold").IsGUIforValue = true;
            //MyMesh.Run(MyWorld);
            

            //GlobalList.Add(MyMesh.GetOutPut());
         



            foreach (var item in GlobalList)
            {
                MyWorld.AddGeometric3DObjects(item);
            }

            MyWorld.BackGroundColor = cGlobalInfo.OptionsWindow.FFAllOptions.panelFor3DBackColor.BackColor;


            // MyWorld.BackGroundColor = Color.LightGray;

            int CurrentIdx = 0;
            for (int i = 0; i < this.CurrentAssociatedImageViewer.AssociatedImage.SingleChannelImage.Count; i++)
            {
                if (this.CurrentAssociatedImageViewer.AssociatedImage.SingleChannelImage[i] == this)
                {
                    CurrentIdx = i;
                    break;
                }
            }
            UserControlSingleLUT SingleLUT = (UserControlSingleLUT)this.CurrentAssociatedImageViewer.LUTManager.panelForLUTS.Controls[CurrentIdx];

            
            HCSAnalyzer.Classes.ImageAnalysis._3D_Engine.cVolumeRendering3D VRD3D = new HCSAnalyzer.Classes.ImageAnalysis._3D_Engine.cVolumeRendering3D(this, new cPoint3D(0, 0, 0), SingleLUT.SelectedLUT, MyWorld);
            VRD3D.AssociatedImage = this;
            MyWorld.AddVolume3D(VRD3D);

            cViewer3D V3D = new cViewer3D();
            V3D.SetInputData(MyWorld);
            V3D.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            V3D.GetOutPut().Name = "3D Volume ["+  this.Name+ "]"; 
            DTW.SetInputData(V3D.GetOutPut()); 
            DTW.Title = "3D Volume [" + this.Name + "]"; 
            DTW.Run();


            


            DTW.Display();
        }

        private void DisplayElevationMap(object sender, EventArgs e)
        {
            cExtendedTable Table = this.ConvertToTable((int)this.CurrentAssociatedImageViewer.ZNavigator.numericUpDownZPos.Value);

            cViewerElevationMap3D VE = new cViewerElevationMap3D();
            VE.SetInputData(new cListExtendedTable(Table));


            int CurrentIdx = 0;
            for (int i = 0; i < this.CurrentAssociatedImageViewer.AssociatedImage.SingleChannelImage.Count; i++)
            {
                if (this.CurrentAssociatedImageViewer.AssociatedImage.SingleChannelImage[i] == this)
                {
                    CurrentIdx = i;
                    break;
                }
            }

            UserControlSingleLUT SingleLUT = (UserControlSingleLUT)this.CurrentAssociatedImageViewer.LUTManager.panelForLUTS.Controls[CurrentIdx];
            VE.LUT = SingleLUT.SelectedLUT;


            if (VE.Run().IsSucceed == false) return;

            cDesignerSinglePanel CD = new cDesignerSinglePanel();
            CD.SetInputData(VE.GetOutPut());
            if (CD.Run().IsSucceed == false) return;

            cDisplayToWindow CDW = new cDisplayToWindow();
            CDW.SetInputData(CD.GetOutPut());
            CDW.Title = "Elevation Map(" + this.Name + " - (Z=" + (int)this.CurrentAssociatedImageViewer.ZNavigator.numericUpDownZPos.Value + ")";
            if (CDW.Run().IsSucceed == false) return;
            CDW.Display();
        }


        private void ToolStripMenuItem_DisplayDataTable(object sender, EventArgs e)
        {
            cExtendedTable Table = this.ConvertToTable(0);

            cDisplayExtendedTable DLET = new cDisplayExtendedTable();
            DLET.SetInputData(Table);
            DLET.Run();
        }

        private void ToolStripMenuItem_ExtractChannel(object sender, EventArgs e)
        {
            cImage ExtractedImage = new cImage(this,true);

            cDisplaySingleImage IV = new cDisplaySingleImage();
            IV.SetInputData(ExtractedImage);
            IV.Run();
        }

        private void ToolStripMenuItem_DisplayHistogram(object sender, EventArgs e)
        {
            cExtendedTable Table = new cExtendedTable(new cExtendedList(this.Data));
            Table.Name = this.Name;

            cViewerStackedHistogram VH = new cViewerStackedHistogram();
            VH.SetInputData(Table);
            VH.Title = Table.Name;
            VH.Run();

            cDisplayToWindow MyDisplay = new cDisplayToWindow();
            MyDisplay.SetInputData(VH.GetOutPut());
            MyDisplay.Title = "Histogram(" + this.Name + ")";
            MyDisplay.Run();
            MyDisplay.Display();

        }

        private void ToolStripMenuItem_DisplayStats(object sender, EventArgs e)
        {
            cExtendedTable Table = new cExtendedTable(new cExtendedList(this.Data));
            Table.Name = this.Name;

            cStatistics S = new cStatistics();
            S.SetInputData(Table);
            S.Run();

            cViewerTable VT = new cViewerTable();
            VT.SetInputData(S.GetOutPut());
            VT.Run();

            cDisplayToWindow MyDisplay = new cDisplayToWindow();
            MyDisplay.SetInputData(VT.GetOutPut());
            MyDisplay.Title = "Statistics(" + this.Name + ")";
            MyDisplay.Run();
            MyDisplay.Display();

        }

    }


    public class cListSingleChannelImage : List<cSingleChannelImage>
    {
        public string Name;

        public void Dispose()
        {
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue 
            // and prevent finalization code for this object
            // from executing a second time.
            foreach (var item in this)
            {
                item.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        //public void Add(Image<Gray, float> CVImage)
        //{

        //}
    }
}
