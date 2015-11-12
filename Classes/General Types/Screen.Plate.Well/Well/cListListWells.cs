using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibPlateAnalysis;
using System.Windows.Forms;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes.Viewers._1D;
using System.Windows.Forms.DataVisualization.Charting;
using HCSAnalyzer.Forms.IO;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using HCSAnalyzer.Classes.Base_Classes.GUI;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes._3D;
using System.Drawing;
using HCSAnalyzer.Classes.Base_Classes.Viewers._3D.ComplexObjects;
using System.IO;
using HCSAnalyzer.Classes.Base_Components.DataManip;

namespace HCSAnalyzer.Classes.General_Types
{
    public class cListListWells : List<cListWells>
    {
        //public object Sender;
        public string Name = "List of List Wells";
        public string Info;
        public object Tag;

        public cListListWells()
        {

        }

        public cListListWells(cWell Source)
        {
            this.Add(new cListWells(Source));
        }

        public cListListWells(cListWells Source)
        {
            this.Add(Source);
        }

        public cListExtendedTable GetAverageDescriptorValues(List<cDescriptorType> ListDesc)
        {
            if (this.Count == 0) return null;

            cListExtendedTable ToBeReturned = new cListExtendedTable();
            ToBeReturned.Tag = this;

            foreach (var item in this)
            {
                ToBeReturned.Add(item.GetAverageDescriptorValues(ListDesc, false, false));
            }
            return ToBeReturned;
        }




        public void BuildAssociated3DDRC()
        {
            // first Merge the wells based on their concentration
            cPropertyType ConcProperty = null;

            foreach (var item in cGlobalInfo.ListDefaultPropertyTypes)
            {
                if (item.Name == "Concentration")
                {
                    ConcProperty = item;
                    break;
                }
            }

            if (ConcProperty == null) return;

            #region Build the world
            bool ToBeCreated = false;
            cViewer3D V3D = new cViewer3D();
            c3DNewWorld MyWorld = cGlobalInfo.GetActive3DWorld();

            if (MyWorld == null)
            {
                MyWorld = new c3DNewWorld(new cPoint3D(1, 1, 1), new cPoint3D(1, 1, 1));
                ToBeCreated = true;
            }

            cListGeometric3DObject GlobalList = new cListGeometric3DObject("3D DRC object");
            #endregion

            int IdxDRC = 0;

            List<cDescriptorType> ListActiveDesc = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors();

            #region Get Min Max for normalization
            List<cDescriptorType> TmpListDesc = new List<cDescriptorType>();
            List<double> ListMin = new List<double>();
            List<double> ListMax = new List<double>();

            TmpListDesc.Add(ListActiveDesc[0]);
            cListExtendedTable TmpTable = this.GetAverageDescriptorValues(TmpListDesc);
            ListMin.Add(TmpTable.Min());
            ListMax.Add(TmpTable.Max());

            TmpListDesc[0] = ListActiveDesc[1];
            TmpTable = this.GetAverageDescriptorValues(TmpListDesc);
            ListMin.Add(TmpTable.Min());
            ListMax.Add(TmpTable.Max());

            TmpListDesc[0] = ListActiveDesc[2];
            TmpTable = this.GetAverageDescriptorValues(TmpListDesc);
            ListMin.Add(TmpTable.Min());
            ListMax.Add(TmpTable.Max());
            #endregion

            #region Build Axis
            c3DObject_Axis MyAxis = new c3DObject_Axis();
            cExtendedTable T = new cExtendedTable();
            T.Add(new cExtendedList(ListActiveDesc[0].GetName()));
            T[0].Tag = ListActiveDesc[0];
            T[0].Add(0);
            T[0].Add(1);
            T.Add(new cExtendedList(ListActiveDesc[1].GetName()));
            T[1].Tag = ListActiveDesc[1];
            T[1].Add(0);
            T[1].Add(1);
            T.Add(new cExtendedList(ListActiveDesc[2].GetName()));
            T[2].Tag = ListActiveDesc[2];
            T[2].Add(0);
            T[2].Add(1);


            MyAxis.SetInputData(T);
            MyAxis.Run(MyWorld);

            GlobalList.AddRange(MyAxis.GetOutPut());
            #endregion



           // c3DCube SpaceCube = new c3DCube();
           // SpaceCube.Create(new cPoint3D(ListMin[0], ListMin[1], ListMin[2]), new cPoint3D(ListMax[0], ListMax[1], ListMax[2]), Color.Red);
           // SpaceCube.SetOpacity(0.1);

           // GlobalList.Add(SpaceCube);

            cListGeometric3DObject SpheresList = new cListGeometric3DObject("Starting Concentration Spheres");
            cListGeometric3DObject DRCList = new cListGeometric3DObject("List DRCs");

            foreach (cListWells SingleDRCListWells in this)
            {
                cWellMerger WM = new cWellMerger();
                WM.IsGUI = false;
                WM.ListSelectedProp = new List<cPropertyType>();
                WM.ListSelectedProp.Add(ConcProperty);
                WM.SetInputData(SingleDRCListWells);
                WM.Run();

                cListListWells Result = WM.GetOutPut();

                if (Result.Count < 2) return; // not enough pts for a DRC

                // now let's sort the wells based on the concentration
                #region Sorting process based on the concentration values
                cExtendedList MyList = new cExtendedList();
                MyList.ListTags = new List<object>();

                foreach (var item in Result)
                {
                    object Con = item[0].ListProperties.FindByName("Concentration").GetValue();
                    if (Con == null) continue;
                    double CurrentConcentration = (double)Con;
                    MyList.Add(CurrentConcentration);
                    MyList.ListTags.Add(item);
                }

                cSort S = new cSort();
                S.SetInputData(new cExtendedTable(MyList));
                S.IsAscending = true;
                S.Run();
                cExtendedTable Sorting = S.GetOutPut();

                cListListWells SortedList = new cListListWells();
                foreach (var item in Sorting.ListTags)
                {
                    SortedList.Add((cListWells)item);
                }
                #endregion

                #region 3D object creation

                cListExtendedTable ET = SortedList.GetAverageDescriptorValues(ListActiveDesc);
                c3DDRC_Curve My3dDRC = new c3DDRC_Curve(ET, ListMin, ListMax);
                My3dDRC.SplineFactor = 100;
                My3dDRC.Create(new cPoint3D(0, 0, 0), cGlobalInfo.ListGroupInfo.GetColor(IdxDRC));

                My3dDRC.SetName(SingleDRCListWells.Tag.ToString());
                DRCList.AddObject(My3dDRC);

                c3DSphere TmpSphere = new c3DSphere(My3dDRC.ListAveragePts[0], 0.030 , Color.FromArgb(100, My3dDRC.Colour));
                TmpSphere.SetName(My3dDRC.GetName() + " - Starting Pt");
                SpheresList.AddObject(TmpSphere);

                IdxDRC++;
            }

            GlobalList.AddRange(SpheresList);
            GlobalList.AddRange(DRCList);

            foreach (var item in GlobalList)
                MyWorld.AddGeometric3DObject(item);

            if (ToBeCreated)
            {
                V3D.SetInputData(MyWorld);
                V3D.Run();

                cDisplayToWindow DTW = new cDisplayToWindow();
                DTW.SetInputData(V3D.GetOutPut());
                DTW.Title = "3D DRCs - " + this.Count + " groups";
                DTW.Run();

                DTW.Display();
            }
            else
            {
                MyWorld.Redraw();
            }
                #endregion
        }
    }
}
