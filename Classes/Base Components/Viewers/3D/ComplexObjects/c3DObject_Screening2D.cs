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

namespace HCSAnalyzer.Classes.Base_Classes.Viewers._3D.ComplexObjects
{
    class c3DObject_Screening2D : cComplexObject
    {
        cScreening Input;

        public c3DObject_Screening2D()
        {
            Title = "3D complex objects: Screening 2D";
        }


        public void SetInputData(cScreening Input)
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

            ListObjects = new cListGeometric3DObject("Screening MetaObject");

            int IdxPlate = 0;
            int Pos = 0;
            foreach (cPlate item in this.Input.ListPlatesActive)
            {
                c3DObject_Plate2D P2D = new c3DObject_Plate2D();
                P2D.SetInputData(item);
                P2D.Run(_3DWorld);
                foreach (var itemObj in P2D.GetOutPut())
                {
                    cPoint3D CurrentObjPos = itemObj.GetPosition() + new cPoint3D(Pos, 0, 0);
                    itemObj.SetPosition(CurrentObjPos);    
                }

                
                //P2D.GetOutPut()[0].Tag = item;


                Pos += this.Input.Columns + 10;

                //c3DTexturedPlan PlateTexture = new c3DTexturedPlan(new cPoint3D(0,0,0),this.Input.GetColorImage());
                //PlateTexture.SetName(Input.());
                //PlateTexture.Tag = this.Input;
                //List<byte[][]> TmpLut = new List<byte[][]>();
                //TmpLut.Add(cGlobalInfo.CurrentPlateLUT);
                //PlateTexture.LUT = TmpLut ;
                //PlateTexture.Run();

                foreach (var itemObj in P2D.GetOutPut())
                {
                    ListObjects.AddObject(itemObj);
                }

                IdxPlate++;
            }

            ListObjects.Tag = this.Input;



            return base.FeedBackMessage;
        }

        public cListGeometric3DObject GetOutPut()
        {
            return ListObjects;
        }

    }
}
