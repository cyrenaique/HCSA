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
using ImageAnalysis;
using HCSAnalyzer.Classes.MetaComponents;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers._3D.ComplexObjects
{
    class c3DObject_Plate2D : cComplexObject
    {
         cPlate Input;
        
        public c3DObject_Plate2D()
        {
            Title = "3D complex objects: Plate 2D";
        }


        public void SetInputData(cPlate Input)
        {
            this.Input = Input;
        }

        public cFeedBackMessage Run(c3DNewWorld _3DWorld)
        {
            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data table defined.";
                return FeedBackMessage;
            }

            ListObjects = new cListGeometric3DObject("Plate MetaObject");


            cImage I = this.Input.GetClassColorImage();
            
          //  cDisplaySingleImage DSI = new cDisplaySingleImage();
          //  DSI.SetInputData(I);
          //  DSI.Run();

            c3DTexturedPlan PlateTexture = new c3DTexturedPlan(new cPoint3D(0, 0, 0), I);

            //c3DTexturedPlan PlateTexture = new c3DTexturedPlan(new cPoint3D(0,0,0),this.Input.GetColorImage());
            PlateTexture.SetName(Input.GetShortInfo());
            PlateTexture.Tag = this.Input;
         //   List<byte[][]> TmpLut = new List<byte[][]>();
          //  TmpLut.Add(cGlobalInfo.CurrentPlateLUT);
          //  PlateTexture.LUT = TmpLut ;
            PlateTexture.Run();


            ListObjects.AddObject(PlateTexture);
            ListObjects[0].Tag = ListObjects.Tag = this.Input;

    
            ListObjects.AddObject(new c3DText(this.Input.GetName(), new cPoint3D(-12-1.2, -8-1.2, 0.2), Color.Black, 70));




            return base.FeedBackMessage;
        }

        public cListGeometric3DObject GetOutPut()
        {
            return ListObjects;
        }

    }
}
