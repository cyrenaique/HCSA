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
    class c3DObject_Axis : cComplexObject
    {
        //public double Radius = 3;
        //public bool IsLinked = false;
        //public bool DrawAxis = true;
        //public int ValueToBeDisplayed = -1;
        //public int ValueForSphereRadius = -1;
        //public cGlobalInfo GlobalInfo = null;


        cExtendedTable Input;


        public c3DObject_Axis()
        {
            Title = "3D complex objects: Axis";
        }


        public void SetInputData(cExtendedTable Input)
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

            ListObjects = new cListGeometric3DObject("Axis MetaObject");
            cExtendedList ListLenghts = new cExtendedList();
            double MinLenght;
            cPoint3D Pt1 = null;
            cPoint3D Pt2 = null;
            cPoint3D Pt3 = null;

            if (this.Input.Count == 1)
            {
                cPoint3D Pt0 = new cPoint3D(this.Input[0][0], 0, 0);
                Pt1 = new cPoint3D(this.Input[0][1], 0, 0);
                ListLenghts.Add(Pt0.DistTo(Pt1));


                MinLenght = ListLenghts.Min();

                if (MinLenght == 0) MinLenght = 1;

                c3DLine XAxis = new c3DLine(Pt0, Pt1);
                XAxis.Tag = this.Input[0].Tag;
                XAxis.SetName("Axis - " + this.Input[0].Name);
                ListObjects.AddObject(XAxis);


            }
            else if (this.Input.Count == 2)
            {
                cPoint3D Pt0 = new cPoint3D(this.Input[0][0], this.Input[1][0], 0);
                Pt1 = new cPoint3D(this.Input[0][1], this.Input[1][0], 0);
                ListLenghts.Add(Pt0.DistTo(Pt1));
                Pt2 = new cPoint3D(this.Input[0][0], this.Input[1][1], 0);
                ListLenghts.Add(Pt0.DistTo(Pt2));


                MinLenght = ListLenghts.Min();

                if (MinLenght == 0) MinLenght = 1;


                c3DLine XAxis = new c3DLine(Pt0, Pt1);
                XAxis.Tag = this.Input[0].Tag;
                XAxis.SetName("Axis - " + this.Input[0].Name);
                ListObjects.AddObject(XAxis);

                c3DLine YAxis = new c3DLine(Pt0, Pt2);
                YAxis.Tag = this.Input[1].Tag;
                YAxis.SetName("Axis - " + this.Input[1].Name);
                ListObjects.AddObject(YAxis);


            }
            else
            {
                cPoint3D Pt0 = new cPoint3D(this.Input[0][0], this.Input[1][0], this.Input[2][0]);
                Pt1 = new cPoint3D(this.Input[0][1], this.Input[1][0], this.Input[2][0]);
                ListLenghts.Add(Pt0.DistTo(Pt1));

                Pt2 = new cPoint3D(this.Input[0][0], this.Input[1][1], this.Input[2][0]);
                ListLenghts.Add(Pt0.DistTo(Pt2));

                Pt3 = new cPoint3D(this.Input[0][0], this.Input[1][0], this.Input[2][1]);
                ListLenghts.Add(Pt0.DistTo(Pt3));


                MinLenght = ListLenghts.Min();

                if (MinLenght == 0) MinLenght = 1;


                c3DLine XAxis = new c3DLine(Pt0, Pt1,Color.Black);
                XAxis.Tag = this.Input[0].Tag;
                XAxis.SetName("Axis - " + this.Input[0].Name);

                ListObjects.AddObject(XAxis);

                c3DLine YAxis = new c3DLine(Pt0, Pt2,Color.Black);
                YAxis.Tag = this.Input[1].Tag;
                YAxis.SetName("Axis - " + this.Input[1].Name);
                ListObjects.AddObject(YAxis);

                c3DLine ZAxis = new c3DLine(Pt0, Pt3,Color.Black);
                ZAxis.Tag = this.Input[2].Tag;
                ZAxis.SetName("Axis - " + this.Input[2].Name);
                ListObjects.AddObject(ZAxis);

            }

            #region draw text
            double TextScale = MinLenght;

            if (MinLenght == 1) TextScale = 1;

            cPoint3D PosText = Pt1;

            // display the axis name
            c3DText TmpText = new c3DText(_3DWorld, this.Input[0].Name, PosText, Color.Black, 0.02 * TextScale);
            TmpText.Tag = this.Input[0].Tag;
            ListObjects.AddObject(TmpText);



            // MinX
            PosText = new cPoint3D(0, 0, 0);

            PosText.X = this.Input[0][0];
            if (this.Input.Count > 1) PosText.Y = this.Input[1][0];
            if (this.Input.Count > 2) PosText.Z = this.Input[2][0] - TextScale * 0.03;

            ListObjects.AddObject(new c3DText(_3DWorld, this.Input[0][0].ToString("N3"), PosText, Color.DimGray, 0.01 * TextScale));

            // MaxX
            PosText.X = this.Input[0][1];
            if (this.Input.Count > 1) PosText.Y = this.Input[1][0];
            if (this.Input.Count > 2) PosText.Z = this.Input[2][0] - TextScale * 0.03;

            if (this.Input.Count > 1) ListObjects.AddObject(new c3DText(_3DWorld, this.Input[0][1].ToString("N3"), PosText, Color.DimGray, 0.01 * TextScale));


            if (Pt2 != null)
            {
                PosText = Pt2;
                TmpText = new c3DText(_3DWorld, this.Input[1].Name, PosText, Color.Black, 0.02 * TextScale);
                TmpText.Tag = this.Input[1].Tag;
                ListObjects.AddObject(TmpText);

                // MinY
                PosText.X = this.Input[0][0] - TextScale * 0.03;// + TextScale*0.02;
                if (this.Input.Count > 1) PosText.Y = this.Input[1][0];// -TextScale * 0.03;
                if (this.Input.Count > 2) PosText.Z = this.Input[2][0] - TextScale * 0.03;
                ListObjects.AddObject(new c3DText(_3DWorld, this.Input[1][0].ToString("N3"), PosText, Color.DimGray, 0.01 * TextScale));

                PosText.X = this.Input[0][0] - TextScale * 0.03;// + TextScale*0.02;
                if (this.Input.Count > 1) PosText.Y = this.Input[1][1];// -TextScale * 0.03;
                if (this.Input.Count > 2) PosText.Z = this.Input[2][0] - TextScale * 0.03;
                ListObjects.AddObject(new c3DText(_3DWorld, this.Input[1][1].ToString("N3"), PosText, Color.DimGray, 0.01 * TextScale));
            }

            if (Pt3 != null)
            {

                PosText = Pt3;
                TmpText = new c3DText(_3DWorld, this.Input[2].Name, /*new cPoint3D(-0.04, -0.04, 0.45)*/ PosText, Color.Black, 0.02 * TextScale);
                TmpText.Tag = this.Input[2].Tag;
                ListObjects.AddObject(TmpText);

                PosText.X = this.Input[0][0] - TextScale * 0.03;// + TextScale*0.02;
                PosText.Y = this.Input[1][0];// -TextScale * 0.03;
                PosText.Z = this.Input[2][0];
                ListObjects.AddObject(new c3DText(_3DWorld, this.Input[2][0].ToString("N3"), PosText, Color.DimGray, 0.01 * TextScale));

                PosText.X = this.Input[0][0] - TextScale * 0.03;// + TextScale*0.02;
                PosText.Y = this.Input[1][0];// -TextScale * 0.03;
                PosText.Z = this.Input[2][1];

                ListObjects.AddObject(new c3DText(_3DWorld, this.Input[2][1].ToString("N3"), PosText, Color.DimGray, 0.01 * TextScale));
            }
            #endregion

            return base.FeedBackMessage;
        }

        public cListGeometric3DObject GetOutPut()
        {
            return ListObjects;
        }

    }
}
