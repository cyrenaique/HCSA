using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes._3D;
using HCSAnalyzer.Classes.Base_Classes.Viewers._3D.ComplexObjects;
using System.Drawing;
using HCSAnalyzer.Classes.General_Types;
using LibPlateAnalysis;

namespace HCSAnalyzer.Classes.MetaComponents
{
    class cDisplayListWell3DScatter : cComponent
    {
        cListWells Input;
       

        public cDisplayListWell3DScatter()
        {
            this.Title = "List Wells 3D Plot";

            cPropertyType PTDesc1 = new cPropertyType("Desc. 1", eDataType.STRING);
            PTDesc1.ListPotentialString = cGlobalInfo.CurrentScreening.ListDescriptors.GetListNames();
            cProperty PropDesc1 = new cProperty(PTDesc1, null);

            cPropertyType PTDesc2 = new cPropertyType("Desc. 2", eDataType.STRING);
            PTDesc2.ListPotentialString = cGlobalInfo.CurrentScreening.ListDescriptors.GetListNames();
            cProperty PropDesc2 = new cProperty(PTDesc2, null);
            
            cPropertyType PTDesc3 = new cPropertyType("Desc. 3", eDataType.STRING);
            PTDesc3.ListPotentialString = cGlobalInfo.CurrentScreening.ListDescriptors.GetListNames();
            cProperty PropDesc3 = new cProperty(PTDesc3, null);

            PropDesc1.Info = "Specify the X axis associated descriptor.";
            base.ListProperties.Add(PropDesc1);
            PropDesc2.Info = "Specify the Y axis associated descriptor.";
            base.ListProperties.Add(PropDesc2);
            PropDesc3.Info = "Specify the Z axis associated descriptor.";
            base.ListProperties.Add(PropDesc3);

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

        public cFeedBackMessage Run()
        {
            if (base.Start() == false)
            {
                base.FeedBackMessage.IsSucceed = false;
                return base.FeedBackMessage;
            }


            if ((this.Input == null)||(this.Input.Count==0))
            {
                
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }
            FeedBackMessage.IsSucceed = true;


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


            _firstValue = base.ListProperties.FindByName("Desc. 1");
            string Desc1;
            if (_firstValue == null)
            {
                base.GenerateError("-Desc. 1- not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                Desc1 = (string)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("-Desc. 1- cast didn't work");
                return base.FeedBackMessage;
            }
            cDescriptorType DescX = cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorByName(Desc1);
            if (DescX == null)
            {
                base.GenerateError(Desc1 + " is not a descriptor !");
                return base.FeedBackMessage;
            }

            _firstValue = base.ListProperties.FindByName("Desc. 2");
            if (_firstValue == null)
            {
                base.GenerateError("-Desc. 2- not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                Desc1 = (string)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("-Desc. 2- cast didn't work");
                return base.FeedBackMessage;
            }
            cDescriptorType DescY = cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorByName(Desc1);
            if (DescY == null)
            {
                base.GenerateError(Desc1 + " is not a descriptor !");
                return base.FeedBackMessage;
            }

            _firstValue = base.ListProperties.FindByName("Desc. 3");
            if (_firstValue == null)
            {
                base.GenerateError("-Desc. 3- not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                Desc1 = (string)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("-Desc. 2- cast didn't work");
                return base.FeedBackMessage;
            }
            cDescriptorType DescZ = cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorByName(Desc1);
            if (DescZ == null)
            {
                base.GenerateError(Desc1 + " is not a descriptor !");
                return base.FeedBackMessage;
            }


            #endregion

            List<cDescriptorType> ListDesc = new List<cDescriptorType>();
            ListDesc.Add(DescX);
            ListDesc.Add(DescY);
            ListDesc.Add(DescZ);

            cListExtendedTable LET = new cListExtendedTable();
            foreach (var item in cGlobalInfo.ListWellClasses)
            {
                List<cWellClassType> TypeForFilter = new List<cWellClassType>();
                TypeForFilter.Add(item);

                cExtendedTable TmpTable = ((cListWells)this.Input.Filter(TypeForFilter)).GetAverageDescriptorValues(ListDesc,false, false);
                if (TmpTable != null)
                    LET.Add(TmpTable);
            }

            cViewer3D V3D = new cViewer3D();
            c3DNewWorld MyWorld = new c3DNewWorld(new cPoint3D(1, 1, 1), new cPoint3D(1, 1, 1));

            c3DObjectScatterPoints _3DScatterPt = new c3DObjectScatterPoints();

            _3DScatterPt.ListProperties.FindByName("Draw Axis ?").SetNewValue(IsdrawAxis);
            _3DScatterPt.ListProperties.FindByName("Normalized ?").SetNewValue(IsNormalized);
            _3DScatterPt.ListProperties.FindByName("Link Points ?").SetNewValue(IsLinked);
            _3DScatterPt.SetInputData(LET);
            if (_3DScatterPt.Run(MyWorld).IsSucceed == false) return base.FeedBackMessage;

            cListGeometric3DObject GlobalList = _3DScatterPt.GetOutPut();

            foreach (var item in GlobalList)
            {
                MyWorld.AddGeometric3DObject(item);
            }

            MyWorld.BackGroundColor = cGlobalInfo.OptionsWindow.FFAllOptions.panelFor3DBackColor.BackColor;

            V3D.SetInputData(MyWorld);
            V3D.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(V3D.GetOutPut());
               DTW.Title = "3D Scatter Plot(List "+this.Input.Count +" Wells)";
            DTW.Run();
         
            DTW.Display();


            return base.FeedBackMessage;

        }


        public void SetInputData(cListWells Input)
        {
            this.Input = Input;
        }
    }
}

