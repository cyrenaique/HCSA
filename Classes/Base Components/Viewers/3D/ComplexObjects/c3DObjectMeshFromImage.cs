using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes._3D;
using LibPlateAnalysis;
using System.Drawing;
using Kitware.VTK;
using HCSAnalyzer.Classes.Base_Components.Viewers._3D.ComplexObjects;
using HCSAnalyzer.Classes.General_Types;
using ImageAnalysis;
using ImageAnalysisFiltering;
using HCSAnalyzer.Classes.ImageAnalysis._3D_Engine.Detection;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers._3D.ComplexObjects
{
    class c3DObjectMeshFromImage : cComplexObject
    {
        
       // public bool IsLinked = false;
       // public bool DrawAxis = true;
        public int ValueToBeDisplayed = -1;
        public int IndexColumnForSphereRadius = -1;
      //  public cGlobalInfo GlobalInfo = null;
      //  public bool IsNormalized = false;
      
        cSingleChannelImage Input;
        vtkExtractVOI VTKInput;
        cPoint3D Pos;

        public c3DObjectMeshFromImage()
        {
            Title = "3D mesh from image";

            cPropertyType PT = new cPropertyType("Thresold", eDataType.DOUBLE);
            cProperty Prop1 = new cProperty(PT, null);
            Prop1.Info = "Threshold value for binarization";
            Prop1.SetNewValue((double)0.5);
            base.ListProperties.Add(Prop1);

            cPropertyType PT2 = new cPropertyType("Split objects ?", eDataType.BOOL);
            cProperty Prop2 = new cProperty(PT2, null);
            Prop2.Info = "Split objects during the segmentation process";
            Prop2.SetNewValue((bool)true);
            base.ListProperties.Add(Prop2);

            //cPropertyType PT3 = new cPropertyType("Link Points ?", eDataType.BOOL);
            //cProperty Prop3 = new cProperty(PT3, null);
            //Prop3.Info = "Draw lines sequentialy between the points";
            //Prop3.SetNewValue((bool)true);
            //base.ListProperties.Add(Prop3);

        }


        public void SetInputData(cSingleChannelImage Input)
        {
            this.Input = Input;
        }

        public void SetInputData(vtkExtractVOI Input, cPoint3D Pos)
        {
            this.VTKInput = Input;
            this.Pos = Pos;
        }

        public cFeedBackMessage Run(c3DNewWorld _3DWorld)
        {

            if (base.Start() == false)
            {
                base.FeedBackMessage.IsSucceed = false;
                return base.FeedBackMessage;
            }


            #region Properties Management
            object _firstValue = base.ListProperties.FindByName("Thresold");
            double Thresold = 0.5;
            if (_firstValue == null)
            {
                base.GenerateError("-Thresold- not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                Thresold = (double)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("-Thresold- cast didn't work");
                return base.FeedBackMessage;
            }

            _firstValue = base.ListProperties.FindByName("Split objects ?");
            bool IsSplit = false;
            if (_firstValue == null)
            {
                base.GenerateError("-Split objects ?- not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                IsSplit = (bool)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("-Split objects ?- cast didn't work");
                return base.FeedBackMessage;
            }
            #endregion

            ListObjects = new cListGeometric3DObject("");

            if (IsSplit == false)
            {
                c3DMeshObject MyMesh = null;

                if (this.VTKInput == null)
                {
                    MyMesh = new c3DMeshObject(this.Input, Thresold);
                }
                else
                {
                    MyMesh = new c3DMeshObject(this.VTKInput, (int)Thresold);

                }
                MyMesh.Create(Color.Aqua, this.Pos );
                ListObjects.Name = MyMesh.GetName() + " metaobject";
                ListObjects.Add(MyMesh);

            }
            else
            {
                // ok that's a little bit more complicated now
                // first we need to binarize the image
                cImageSegmentationThreshold IST = new cImageSegmentationThreshold();
                IST.ListProperties.FindByName("Threshold").SetNewValue(Thresold);
                cImage SourceImage = new cImage(this.Input, false);
                IST.SetInputData(SourceImage);
                IST.Run();

                // now perform image labeling
                cImage BinImage = IST.GetOutPut();
                cImage LabeledImage = new cImage(BinImage, false);
                ConnectedComponentSet CCS = new ConnectedComponentSet(BinImage,LabeledImage,SourceImage,  0, eConnectivity.THREED_6, 0, float.MaxValue);

                Random RD = new Random();

                int IdxObj = 1;
                // loop over each object
                int NumObj = CCS.Count;

                ListObjects.Name = "T_" + Thresold + " [" + SourceImage.Name + " Metaobject";

                for(int i = 0;i<NumObj;i++)
                {
                    ConnectedVoxels item = CCS[i];

                 //   if (item.Volume <= 1) continue;

                    List<cPoint3D> ExtremePts = item.GetExtremaPoints();
                    // crop the labeled image
                    cImage TmpIm = LabeledImage.Crop(ExtremePts[0], ExtremePts[1]);
                    
                    // we have to clean the cropped image to prevent any overlapping object to be segmented
                    for (int Pix = 0; Pix < TmpIm.ImageSize; Pix++)
                    {
                        if ((TmpIm.SingleChannelImage[0].Data[Pix] > 0) && (TmpIm.SingleChannelImage[0].Data[Pix] != IdxObj)) TmpIm.SingleChannelImage[0].Data[Pix] = 0;
                    }

                    // update the position of the object
                    cPoint3D NewPos = ExtremePts[0]*SourceImage.Resolution;


                    c3DMeshObject MyMesh = new c3DMeshObject(TmpIm.SingleChannelImage[0], 0.5);
                    MyMesh.Create(Color.FromArgb(RD.Next(255), RD.Next(255), RD.Next(255)), NewPos);
                    
                    MyMesh.SetName(MyMesh.GetName() + "_" + IdxObj);
                    MyMesh.AssociatedConnectedComponent = item;
                    ListObjects.AddObject(MyMesh);

                    IdxObj++;
                }

            }
            return base.FeedBackMessage;
        }

        public cListGeometric3DObject GetOutPut()
        {
            return ListObjects;
        }

    }
}
