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

namespace HCSAnalyzer.Classes.Base_Classes.Viewers._3D.ComplexObjects
{
    class c3DObjectScatterPoints : cComplexObject
    {
        public double Radius = 3;
       // public bool IsLinked = false;
       // public bool DrawAxis = true;
        public int ValueToBeDisplayed = -1;
        public int IndexColumnForSphereRadius = -1;
      //  public cGlobalInfo GlobalInfo = null;
      //  public bool IsNormalized = false;
      
        cListExtendedTable Input;


        public c3DObjectScatterPoints()
        {
            Title = "3D Scatter Points Graph";

            cPropertyType PT = new cPropertyType("Normalized ?", eDataType.BOOL);
            cProperty Prop1 = new cProperty(PT, null);
            Prop1.Info = "Perform Min-Max normalization of the data.";
            Prop1.SetNewValue((bool)false);
            base.ListProperties.Add(Prop1);

            cPropertyType PT2 = new cPropertyType("Draw Axis ?", eDataType.BOOL);
            cProperty Prop2 = new cProperty(PT2, null);
            Prop2.Info = "Draw data associated axis";
            Prop2.SetNewValue((bool)true);
            base.ListProperties.Add(Prop2);

            cPropertyType PT3 = new cPropertyType("Link Points ?", eDataType.BOOL);
            cProperty Prop3 = new cProperty(PT3, null);
            Prop3.Info = "Draw lines sequentialy between the points";
            Prop3.SetNewValue((bool)true);
            base.ListProperties.Add(Prop3);

        }


        public void SetInputData(cListExtendedTable Input)
        {
            this.Input = Input;
        }

        public cFeedBackMessage Run(c3DNewWorld _3DWorld)
        {

            if (base.Start() == false)
            {
                base.FeedBackMessage.IsSucceed = false;
                return base.FeedBackMessage;
            }

            //if (this.Input == null)
            //{
            //    FeedBackMessage.IsSucceed = false;
            //    FeedBackMessage.Message = "No input data table defined.";
            //    return FeedBackMessage;
            //}

            #region Properties Management
            object _firstValue = base.ListProperties.FindByName("Normalized ?");
            bool IsNormalized = false;
            if (_firstValue == null)
            {
                base.GenerateError("-Normalized ?- not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                IsNormalized = (bool)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("-Normalized ?- cast didn't work");
                return base.FeedBackMessage;
            }

            _firstValue = base.ListProperties.FindByName("Draw Axis ?");
            bool IsdrawAxis = true;
            if (_firstValue == null)
            {
                base.GenerateError("-Draw Axis ?- not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                IsdrawAxis = (bool)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("-Draw Axis ?- cast didn't work");
                return base.FeedBackMessage;
            }

            _firstValue = base.ListProperties.FindByName("Link Points ?");
            bool IsLinked = false;
            if (_firstValue == null)
            {
                base.GenerateError("-Link Points ?- not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                IsLinked = (bool)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("-Link Points ?- cast didn't work");
                return base.FeedBackMessage;
           }
            #endregion


            cExtendedList ListLenghtsMax = new cExtendedList();

            ListObjects = new cListGeometric3DObject("Scatter Plot MetaObject");

            double MinX = this.Input.Min(0);
            double MaxX = this.Input.Max(0);
            double DeltaX = MaxX - MinX;
            ListLenghtsMax.Add(DeltaX);
            if (DeltaX == 0) DeltaX = 1;

            double MinY = this.Input.Min(1);
            double MaxY = this.Input.Max(1);
            double DeltaY = MaxY - MinY;
            ListLenghtsMax.Add(DeltaY);
            if (DeltaY == 0) DeltaY = 1;

            double MinZ = this.Input.Min(2);
            double MaxZ = this.Input.Max(2);
            double DeltaZ = MaxZ - MinZ;
            ListLenghtsMax.Add(DeltaZ);
            if (DeltaZ == 0) DeltaZ = 1;

            double MinRad = 0;
            double MaxRad = 0;
            double DeltaRad = 1;


            double MaxLenght = ListLenghtsMax.Min();
            if (MaxLenght <= 0) MaxLenght = 1;

            for (int CurrentSerie = 0; CurrentSerie < this.Input.Count; CurrentSerie++)
            {   
                cExtendedTable CurrentTable = this.Input[CurrentSerie];
                if ((IndexColumnForSphereRadius > 0) && (IndexColumnForSphereRadius < CurrentTable.Count))
                {
                    MinRad = CurrentTable[IndexColumnForSphereRadius].Min();
                    MaxRad = CurrentTable[IndexColumnForSphereRadius].Max();

                    if (MaxRad != MinRad)
                        DeltaRad = MaxRad - MinRad;
                }

                if (CurrentTable.Count == 0) continue;

                cListGeometric3DObject TmpListObjects = new cListGeometric3DObject("Scatter MetaObject");
                TmpListObjects.Tag = CurrentTable.Tag;
                cPoint3D Pt = null;
                for (int IdxPt = 0; IdxPt < CurrentTable[0].Count; IdxPt++)
                {
                    if(IsNormalized)
                    Pt = new cPoint3D((CurrentTable[0][IdxPt] - MinX) / DeltaX, (CurrentTable[1][IdxPt] - MinY) / DeltaY, (CurrentTable[2][IdxPt] - MinZ) / DeltaZ);
                    else
                    Pt = new cPoint3D(CurrentTable[0][IdxPt], CurrentTable[1][IdxPt], CurrentTable[2][IdxPt]);

                    double Rad = this.Radius;
                    if ((IndexColumnForSphereRadius > 0) && (IndexColumnForSphereRadius < CurrentTable.Count))
                    {
                        Rad = this.Radius * ((CurrentTable[IndexColumnForSphereRadius][IdxPt] - MinRad)) / DeltaRad;
                        Rad /= 200;
                    }
                 
                    if(IndexColumnForSphereRadius>-1)
                    Rad = CurrentTable[IndexColumnForSphereRadius][IdxPt];

                    if (IsNormalized) Rad /= 300.0;
                    else
                    {
                        Rad = MaxLenght / 100.0;
                    }
                    
                    c3DSphere Sphere = new c3DSphere(Pt, Rad);
                    //c3DPoint Sphere = new c3DPoint(Pt);
                    if ((CurrentTable.ListRowNames != null) && (CurrentTable.ListRowNames.Count > IdxPt))
                    {
                        if (CurrentTable.ListRowNames[IdxPt] != null) Sphere.SetName(CurrentTable.ListRowNames[IdxPt]);
                        else
                            Sphere.SetName("Sphere " + IdxPt);
                    }
                    else
                    {
                        Sphere.SetName("Sphere " + IdxPt);
                    }

                    if ((CurrentTable.ListTags != null) && (CurrentTable.ListTags[IdxPt] != null) && (CurrentTable.ListTags[IdxPt].GetType() == typeof(cWell)))
                    {
                        if (CurrentTable.ListTags[IdxPt].GetType() == typeof(cWell))
                        {
                            Sphere.Colour = ((cWell)(CurrentTable.ListTags[IdxPt])).GetClassType().ColourForDisplay;
                            Sphere.Tag = CurrentTable.ListTags[IdxPt];
                        }
                    }
                    else //if (this.GlobalInfo != null)
                    {
                        Sphere.Colour = cGlobalInfo.ListCellularPhenotypes[CurrentSerie % cGlobalInfo.ListCellularPhenotypes.Count].ColourForDisplay;
                    }


                    TmpListObjects.AddObject(Sphere);

                    if ((ValueToBeDisplayed > 0) && (ValueToBeDisplayed < CurrentTable.Count))
                    {
                        if (_3DWorld != null)
                            TmpListObjects.Add(new c3DText(_3DWorld, CurrentTable[ValueToBeDisplayed][IdxPt].ToString("N3"), new cPoint3D(Pt.X + 0.02, Pt.Y + 0.02, Pt.Z + 0.02), Color.White, 0.01));
                    }
                }

                ListObjects.AddRange(TmpListObjects);
              
                #region Draw Links
                if (IsLinked)
                {
                    cListGeometric3DObject ListLinks = new cListGeometric3DObject( "Links MetaObject");

                    for (int IdxPt = 1; IdxPt < CurrentTable[0].Count; IdxPt++)
                    {
                        cPoint3D Pt1 = null;
                        cPoint3D Pt0 = null;

                        if (IsNormalized)
                        {
                            Pt1 = new cPoint3D((CurrentTable[0][IdxPt] - MinX) / DeltaX, (CurrentTable[1][IdxPt] - MinY) / DeltaY, (CurrentTable[2][IdxPt] - MinZ) / DeltaZ);
                            Pt0 = new cPoint3D((CurrentTable[0][IdxPt - 1] - MinX) / DeltaX, (CurrentTable[1][IdxPt - 1] - MinY) / DeltaY, (CurrentTable[2][IdxPt - 1] - MinZ) / DeltaZ);
                        }
                        else
                        {
                            Pt1 = new cPoint3D(CurrentTable[0][IdxPt], CurrentTable[1][IdxPt] , CurrentTable[2][IdxPt] );
                            Pt0 = new cPoint3D(CurrentTable[0][IdxPt - 1] , CurrentTable[1][IdxPt - 1] , CurrentTable[2][IdxPt - 1] );
                        }
                       // new cPoint3D((CurrentTable[0][IdxPt] - MinX) / DeltaX, (CurrentTable[1][IdxPt] - MinY) / DeltaY, (CurrentTable[2][IdxPt] - MinZ) / DeltaZ);
                        //cPoint3D Pt0 = new cPoint3D((CurrentTable[0][IdxPt - 1] - MinX) / DeltaX, (CurrentTable[1][IdxPt - 1] - MinY) / DeltaY, (CurrentTable[2][IdxPt - 1] - MinZ) / DeltaZ);

                        c3DLine Line = new c3DLine(Pt0, Pt1);
                        Line.SetName("Link ["+(IdxPt-1)+";"+IdxPt+"]"); 
                        ListLinks.AddObject(Line);
                    }

                    ListObjects.AddRange(ListLinks);
                }
                #endregion

            }

            #region Draw Axis
            if (IsdrawAxis && (this.Input[0].Count > 2))
            {
                c3DObject_Axis Axis = new c3DObject_Axis();
                cExtendedTable T = new cExtendedTable();
                T.Add(new cExtendedList(Input[0][0].Name));
                if (IsNormalized)
                {
                    T[0].Tag = Input[0][0].Tag;
                    T[0].Add(0);
                    T[0].Add(1);
                    T.Add(new cExtendedList(Input[0][1].Name));
                    T[1].Tag = Input[0][1].Tag;
                    T[1].Add(0);
                    T[1].Add(1);
                    T.Add(new cExtendedList(Input[0][2].Name));
                    T[2].Tag = Input[0][2].Tag;
                    T[2].Add(0);
                    T[2].Add(1);
                }
                else
                {
                    T[0].Tag = Input[0][0].Tag;
                    T[0].Add(MinX);
                    T[0].Add(MaxX);
                    T.Add(new cExtendedList(Input[0][1].Name));
                    T[1].Tag = Input[0][1].Tag;
                    T[1].Add(MinY);
                    T[1].Add(MaxY);
                    T.Add(new cExtendedList(Input[0][2].Name));
                    T[2].Tag = Input[0][2].Tag;
                    T[2].Add(MinZ);
                    T[2].Add(MaxZ);
                }
              
                Axis.SetInputData(T);
                Axis.Run(_3DWorld);

                ListObjects.AddRange(Axis.GetOutPut());
            }
            #endregion

            // _3DWorld.AddGeometric3DObjects(this.ListObjects);

            return base.FeedBackMessage;
        }

        public cListGeometric3DObject GetOutPut()
        {
            return ListObjects;
        }

    }
}
