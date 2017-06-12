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
using Kitware.VTK;

namespace HCSAnalyzer.Classes.General_Types
{
    public class cListWells : List<cWell>
    {
        //static cGlobalInfo GlobalInfo;
        int NewClass;
        public object Tag;
        public object Sender;
        public string Name = "List Wells";
        public string Info;

        public cListWells()
        {
            this.Sender = null;
        }

        public cListWells(cWell Source)
        {
            if (Source == null)
                this.Sender = null;
            else
                this.Add(Source);

        }

        public cListWells(object Sender)
        {
            this.Sender = Sender;
        }
        //public cWell GetWell(int PosX, int PosY)
        //{
        //    return null;
        //}


        public cWell GetWell(int Idx)
        {
            if (Idx < 0) return null;
            if (this.Count == 0) return null;
            // if (Idx > this.Count) return null;
            return this[Idx];
        }

        public cWell GetFirstWell(int PosX, int PosY)
        {
            foreach (var item in this)
            {
                if ((item.GetPosX() == PosX) && (item.GetPosY() == PosY))
                    return item;
            }

            return null;
        }

        public cExtendedTable GenerateAverageWell()
        {
            //.cExtendedcWell WellToReturn = new cWell();
            cExtendedTable ToReturn = new cExtendedTable();

            cExtendedTable TmpValues = this.GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors(), false, false);

            cStatistics S = new cStatistics();
            S.UnselectAll();
            S.IsMean = true;
            S.SetInputData(TmpValues);
            S.Run();

            ToReturn = S.GetOutPut();
            ToReturn.ListTags = new List<object>();
            ToReturn.ListTags.Add((cListWells)this);
            return ToReturn;

        }

        public void ResetProperty(string PropertyName)
        {
            foreach (var item in this)
                item.ListProperties.FindByName(PropertyName).SetNewValue(null);
        }

        public cListWells Filter(List<cWellClassType> ListClasses)
        {
            cListWells ToBeReturned = new cListWells();

            foreach (cWell TmpWell in this)
            {
                foreach (var item in ListClasses)
                {
                    if (TmpWell.GetClassType() == item)
                    {
                        ToBeReturned.Add(TmpWell);
                        break;
                    }
                }
            }

            return ToBeReturned;
        }

        public cListWells FilterByGroup(List<int> ListGroupID)
        {
            cListWells ToBeReturned = new cListWells();

            foreach (cWell TmpWell in this)
            {
                foreach (var item in ListGroupID)
                {
                    object Val = TmpWell.ListProperties.FindByName("Group").GetValue();

                    if ((Val != null) && ((int)(Val) == item))
                    {
                        ToBeReturned.Add(TmpWell);
                        break;
                    }
                }
            }

            return ToBeReturned;
        }

        public void SetNewClass(int IdxClass)
        {
            foreach (var item in this)
                item.SetClass(IdxClass);
        }

        public cExtendedTable GetPositionRelatedSignatures()
        {
            cExtendedTable ToReturn = new cExtendedTable();

            cExtendedList XPos = new cExtendedList("Col_Pos");
            cExtendedList YPos = new cExtendedList("Row_Pos");
            cExtendedList DistToBorder = new cExtendedList("Dist_To_Border");
            cExtendedList DistToCenter = new cExtendedList("Dist_To_Center");
            ToReturn.ListTags = new List<object>();

            foreach (var item in this)
            {
                XPos.Add(item.GetPosX());
                YPos.Add(item.GetPosY());

                DistToBorder.Add(item.DistToBorder());
                DistToCenter.Add(item.DistToCenter());

                ToReturn.ListTags.Add(item);
            }

            ToReturn.Add(XPos);
            ToReturn.Add(YPos);
            ToReturn.Add(DistToBorder);
            ToReturn.Add(DistToCenter);

            return ToReturn;

        }

        void ToolStripMenuItem_GetSingleCell3DView(object sender, EventArgs e)
        {
            BuildAndDisplaySingleCell3DScatterCloud(null);
        }

        public void BuildAndDisplaySingleCell3DScatterCloud(cViewer3D AssociatedViewer)
        {
            cGetListPhenotypes GLP = new cGetListPhenotypes();
            GLP.SetInputData(this);

            GLP.Run();
            cExtendedTable CompleteTable = GLP.GetOutPut();

            bool ToBeCreated = false;

            c3DNewWorld MyWorld;

            if (AssociatedViewer == null)
            {
                AssociatedViewer = new cViewer3D();

                MyWorld = cGlobalInfo.GetActive3DWorld();

                if (MyWorld == null)
                {
                    MyWorld = new c3DNewWorld(new cPoint3D(1, 1, 1), new cPoint3D(1, 1, 1));
                    ToBeCreated = true;
                }
            }
            else
                MyWorld = AssociatedViewer.GetAssociated3DWorld();



            cListGeometric3DObject GlobalList = new cListGeometric3DObject("Single Cell MetaObject");

            cNormalize N = new cNormalize();
            N.SetInputData(CompleteTable);
            N.NormalizationType = eNormalizationType.MIN_MAX;
            N.Run();
            //  cExtendedTable NormTable = N.GetOutPut();

            FormForOptionsSingleCellAnalysis OptionsWindow = new FormForOptionsSingleCellAnalysis();
            OptionsWindow.radioButtonInitPhenoClassDesc.Enabled = false;
            if (OptionsWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;


            int ColorMode = 0;
            if (OptionsWindow.radioButtonInitPhenoClassDB.Checked)
                ColorMode = 0;
            else if (OptionsWindow.radioButtonInitPhenoClassWell.Checked)
                ColorMode = 1;
            else if (OptionsWindow.radioButtonInitPhenoClassDesc.Checked)
                ColorMode = 2;


            c3DPointCloud _3DPtCloud = new c3DPointCloud(CompleteTable);
            _3DPtCloud.AutomatedPtColorMode = ColorMode;
            _3DPtCloud.PtSize = 3;
            _3DPtCloud.Create(new cPoint3D(0, 0, 0));

            _3DPtCloud.SetName("_3DPtCloud " + CompleteTable.Name);
            GlobalList.Add(_3DPtCloud);

            if (ToBeCreated)
            {
                c3DObject_Axis Axis = new c3DObject_Axis();
                cExtendedTable T = new cExtendedTable();
                T.Add(new cExtendedList(CompleteTable[0].Name));

                T[0].Tag = CompleteTable[0].Tag;
                T[0].Add(0);
                T[0].Add(1);
                T.Add(new cExtendedList(CompleteTable[1].Name));
                T[1].Tag = CompleteTable[1].Tag;
                T[1].Add(0);
                T[1].Add(1);

                if (CompleteTable.Count > 2)
                {
                    T.Add(new cExtendedList(CompleteTable[2].Name));
                    T[2].Tag = CompleteTable[2].Tag;
                    T[2].Add(0);
                    T[2].Add(1);
                }

                Axis.SetInputData(T);
                Axis.Run(MyWorld);

                GlobalList.AddRange(Axis.GetOutPut());
                MyWorld.BackGroundColor = cGlobalInfo.OptionsWindow.FFAllOptions.panelFor3DBackColor.BackColor;
            }

            foreach (var item in GlobalList)
            {
                MyWorld.AddGeometric3DObject(item);
            }



            if (ToBeCreated)
            {
                AssociatedViewer.SetInputData(MyWorld);
                AssociatedViewer.Run();

                cDisplayToWindow DTW = new cDisplayToWindow();
                DTW.SetInputData(AssociatedViewer.GetOutPut());
                DTW.Title = "3D Cloud Point - " + CompleteTable[0].Count + " points";
                DTW.Run();

                DTW.Display();
            }
            else
            {
                MyWorld.Redraw();
            }

        }

        void ToolStripMenuItem_GetSingleCellTable(object sender, EventArgs e)
        {
            cGetListPhenotypes GLP = new cGetListPhenotypes();
            GLP.SetInputData(this);
            GLP.Run();
            cExtendedTable CompleteTable = GLP.GetOutPut();

            CompleteTable.Name = this.Count + " Wells <=> " + CompleteTable[0].Count + " objects";
            cViewer2DScatterPoint VS = new cViewer2DScatterPoint();
            VS.SetInputData(CompleteTable);
            VS.Chart.IsZoomableX = true;
            VS.Chart.IsZoomableY = true;
            // VS.Chart.IsSelectable = true;
            VS.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(VS.GetOutPut());
            DTW.Title = "2D Scatter Points. " + this.Count + " Wells <=> " + CompleteTable[0].Count + " objects";
            DTW.Run();
            DTW.Display();

            //  this.AssociatedPlate.DBConnection.DisplayTable(this);
        }

        void ToolStripMenuItem_SingleCellStackedHisto(object sender, EventArgs e)
        {

            cGUI_ListClasses GUIClasses = new cGUI_ListClasses();
            GUIClasses.IsCheckBoxes = true;
            GUIClasses.IsSelectAll = true;
            GUIClasses.ClassType = eClassType.PHENOTYPE;
            if (!GUIClasses.Run().IsSucceed) return;

            cViewerStackedHistogram EC = GetSingleCellStackedHisto(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), GUIClasses);
            if (EC == null) return;

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(EC.GetOutPut());
            DTW.Title = "Stacked Histogram: " + this.Count + " wells";
            DTW.Run();
            DTW.Display();
        }

        public cViewerStackedHistogram GetSingleCellStackedHisto(cDescriptorType DT, cGUI_ListClasses GUIClasses)
        {
            List<cDescriptorType> LCDT = new List<cDescriptorType>();

            //if (GlobalInfo == null)
            //{
            //    if (this.Count == 0) return;
            //   // GlobalInfo = cGlobalInfo.CurrentScreening.GlobalInfo;
            //}

            LCDT.Add(DT);

            cExtendedTable FinalTable = new cExtendedTable();
            FinalTable.Name = "Stacked Histogram: " + this.Count + " wells";



            int Idx = 0;
            foreach (var item in cGlobalInfo.ListCellularPhenotypes)
            {
                FinalTable.Add(new cExtendedList());
                FinalTable[Idx].Name = item.Name;
                FinalTable[Idx].Tag = item;
                Idx++;
            }

            foreach (cWell TmpWell in this)
            {
                TmpWell.AssociatedPlate.DBConnection = new cDBConnection(TmpWell.AssociatedPlate, TmpWell.SQLTableName);

                int IDx = 0;
                foreach (var item in cGlobalInfo.ListCellularPhenotypes)
                {
                    List<cCellularPhenotype> ListCellularPhenotypesToBeSelected = new List<cCellularPhenotype>();
                    if (GUIClasses == null)
                    {
                        ListCellularPhenotypesToBeSelected.Add(item);
                    }
                    else if (GUIClasses.GetOutPut()[0][IDx] > 0)
                    {

                        ListCellularPhenotypesToBeSelected = new List<cCellularPhenotype>();
                        ListCellularPhenotypesToBeSelected.Add(item);
                    }
                    else
                    {
                        IDx++; continue;
                    }


                    cExtendedTable TmpET = null;
                    try
                    {
                        TmpET = TmpWell.AssociatedPlate.DBConnection.GetWellValues(TmpWell, LCDT, ListCellularPhenotypesToBeSelected);
                    }
                    catch (Exception Ex)
                    {
                        cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(this.GetType().ToString() + " : " + Ex.Message);
                    }

                    if ((TmpET != null) && (TmpET.Count > 0)) FinalTable[IDx].AddRange(TmpET[0]);
                    IDx++;
                }
                TmpWell.AssociatedPlate.DBConnection.CloseConnection();
            }

            cViewerStackedHistogram VSH = new cViewerStackedHistogram();
            VSH.SetInputData(FinalTable);
            VSH.Chart.BinNumber = -1;
            //VSH.Chart.Is

            //VSH.ListProperties.FindByName("Bin Number").SetNewValue((int)100);
            //VSH.ListProperties.FindByName("Bin Number").IsGUIforValue = true;

            //  VSH.Chart.BinNumber = LCDT[0].GetBinNumber();
            VSH.Chart.IsShadow = false;
            VSH.Chart.IsBorder = false;
            VSH.Chart.IsXGrid = true;
            VSH.Chart.IsYGrid = true;
            VSH.Chart.LabelAxisX = LCDT[0].GetName();

            VSH.Run();
            VSH.Chart.Width = 0;
            VSH.Chart.Height = 0;

            return VSH;

        }

        void ToolStripMenuItem_SingleCellHisto(object sender, EventArgs e)
        {
            List<cDescriptorType> LCDT = new List<cDescriptorType>();
            // return;

            //if (GlobalInfo == null)
            //{
            //    if (this.Count == 0) return;
            //    GlobalInfo = cGlobalInfo.CurrentScreening.GlobalInfo;
            //}

            LCDT.Add(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor());

            cExtendedTable FinalTable = new cExtendedTable();
            FinalTable.Name = "Histogram: " + this.Count + " wells";

            cGUI_ListClasses GUIClasses = new cGUI_ListClasses();
            GUIClasses.IsCheckBoxes = true;
            GUIClasses.ClassType = eClassType.PHENOTYPE;
            if (!GUIClasses.Run().IsSucceed) return;

            for (int i = 0; i < GUIClasses.GetOutPut()[0].Count; i++)
            {
                if (GUIClasses.GetOutPut()[0][i] == 1)
                {
                    FinalTable.Add(new cExtendedList());
                    FinalTable[0].Name = cGlobalInfo.ListCellularPhenotypes[i].Name;
                    FinalTable[0].Tag = cGlobalInfo.ListCellularPhenotypes[i];
                }
            }

            List<cCellularPhenotype> ListCellularPhenotypesToBeSelected = new List<cCellularPhenotype>();
            for (int i = 0; i < GUIClasses.GetOutPut()[0].Count; i++)
            {
                if (GUIClasses.GetOutPut()[0][i] == 1)
                {

                    ListCellularPhenotypesToBeSelected.Add(cGlobalInfo.ListCellularPhenotypes[i]);
                }
            }


            cDesignerTab DT = new cDesignerTab();


            for (int IdxClass = 0; IdxClass < cGlobalInfo.ListWellClasses.Count; IdxClass++)
            {

                cExtendedTable FinalTableForHisto = new cExtendedTable();
                FinalTableForHisto.Add(new cExtendedList());


                cListWells TmpLW = new cListWells();
                foreach (cWell TmpWell in this)
                {
                    if (TmpWell.GetClassType() == cGlobalInfo.ListWellClasses[IdxClass])
                        TmpLW.Add(TmpWell);
                }

                foreach (cWell TmpWell in TmpLW)
                {
                    TmpWell.AssociatedPlate.DBConnection = new cDBConnection(TmpWell.AssociatedPlate, TmpWell.SQLTableName);

                    //int IDx = 0;
                    //  for (int i = 0; i < GUIClasses.GetOutPut()[0].Count; i++)
                    {
                        //   if (GUIClasses.GetOutPut()[0][i] == 1)
                        {
                            //List<cCellularPhenotype> ListCellularPhenotypesToBeSelected = new List<cCellularPhenotype>();
                            //ListCellularPhenotypesToBeSelected.Add(cGlobalInfo.ListCellularPhenotypes[i]);

                            cExtendedTable TmpET = TmpWell.AssociatedPlate.DBConnection.GetWellValues(TmpWell,
                                                     LCDT, ListCellularPhenotypesToBeSelected);



                            if (TmpET.Count > 0) FinalTableForHisto[0].AddRange(TmpET[0]);
                            //     break;
                        }
                    }
                    TmpWell.AssociatedPlate.DBConnection.CloseConnection();
                }

                if (TmpLW.Count > 0)
                {
                    cHistogramBuilder HB = new cHistogramBuilder();
                    FinalTableForHisto[0].Name = LCDT[0].GetName();
                    HB.SetInputData(FinalTableForHisto);
                    HB.IsNormalized = true;
                    HB.BinNumber = -1;
                    HB.Run();

                    cExtendedTable ResHisto = HB.GetOutPut();
                    ResHisto.Name = cGlobalInfo.ListWellClasses[IdxClass].Name + " - " + TmpLW.Count + " Wells - " + FinalTableForHisto[0].Count + " Points";

                    cViewerTable VT = new cViewerTable();
                    VT.SetInputData(ResHisto);
                    VT.Run();

                    DT.SetInputData(VT.GetOutPut());


                    //  cDisplayExtendedTable DETHB = new cDisplayExtendedTable();
                    //  DETHB.SetInputData(ResHisto);
                    //  DETHB.Run();
                }

            }
            DT.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(DT.GetOutPut());
            DTW.Title = "Single Cell Histograms";
            DTW.Run();
            DTW.Display();


            return;

            cViewerHistogram VH = new cViewerHistogram();
            //cViewerStackedHistogram VSH = new cViewerStackedHistogram();
            //  VH.SetInputData(FinalTableForHisto);
            VH.Chart.BinNumber = -1;
            //VSH.Chart.Is

            //VH.ListProperties.FindByName("Bin Number").SetNewValue((int)100);
            //VH.ListProperties.FindByName("Bin Number").IsGUIforValue = true;

            //  VSH.Chart.BinNumber = LCDT[0].GetBinNumber();
            VH.Chart.IsShadow = false;
            VH.Chart.IsBorder = true;
            VH.Chart.IsXGrid = true;
            VH.Chart.IsHistoNormalized = true;
            VH.Chart.IsBar = true;

            VH.Chart.IsYGrid = true;
            VH.Chart.LabelAxisX = LCDT[0].GetName();

            cFeedBackMessage FBM = VH.Run();
            if (FBM.IsSucceed == false)
            {
                cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(FBM.Message);
            }
            VH.Chart.Width = 0;
            VH.Chart.Height = 0;

            cDisplayToWindow CDTW = new cDisplayToWindow();
            CDTW.SetInputData(VH.GetOutPut());
            CDTW.Title = "Histogram: " + this.Count + " wells";
            CDTW.Run();
            CDTW.Display();
        }

        void ToolStripMenuItem_SingleCellAnalysis(object sender, EventArgs e)
        {
            this.SingleCellBasedAnalysis();
        }

        void ToolStripMenuItem_GetSingleCellDataTable(object sender, EventArgs e)
        {
            //  cGlobalInfo = (cGlobalInfo)this.Sender;
            //if (cGlobalInfo == null)
            //    GlobalInfo = cGlobalInfo.CurrentScreening.GlobalInfo;
            // if (GlobalInfo == null) return;

            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            GUI_ListClasses.IsCheckBoxes = true;
            GUI_ListClasses.IsSelectAll = true;
            GUI_ListClasses.ClassType = eClassType.PHENOTYPE;
            if (!GUI_ListClasses.Run().IsSucceed) return;

            List<cCellularPhenotype> ListPheno = new List<cCellularPhenotype>();
            for (int i = 0; i < GUI_ListClasses.GetOutPut()[0].Count; i++)
            {
                if (GUI_ListClasses.GetOutPut()[0][i] == 1)
                    ListPheno.Add(cGlobalInfo.ListCellularPhenotypes[i]);
            }

            List<cDescriptorType> ListSelectedDesc = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors();
            cExtendedTable CompleteTable = this.GetSingleObjectDescriptorValues(ListSelectedDesc, ListPheno, false);

            cExtendedList IdxList = new cExtendedList("Index");

            for (int i = 0; i < CompleteTable[0].Count; i++)
            {
                IdxList.Add(i);
            }

            CompleteTable.Insert(0, IdxList);
            CompleteTable.Name = "Single cell data table (" + this.Count + " wells)";
            cDisplayExtendedTable DET = new cDisplayExtendedTable();
            DET.SetInputData(CompleteTable);
            DET.Run();
        }

        void ToolStripMenuItem_ComputeDisplaySingleCellDRC(object sender, EventArgs e)
        {
            //GlobalInfo = (cGlobalInfo)this.Sender;
            //if (GlobalInfo == null) GlobalInfo = cGlobalInfo.CurrentScreening.GlobalInfo;
            //if (GlobalInfo == null) return;

            //int DescriptorIdx = GlobalInfo.CurrentScreen.ListDescriptors.GetDescriptorIndex(GlobalInfo.CurrentScreen.ListDescriptors.GetActiveDescriptor());

            cDesignerTab DT = new cDesignerTab();
            DT.IsMultiline = true;

            cExtendedTable TableForGeneralResuts = new cExtendedTable();
            TableForGeneralResuts.Add(new cExtendedList("EC50"));
            TableForGeneralResuts.ListRowNames = new List<string>();
            TableForGeneralResuts.ListTags = new List<object>();

            cDesignerSplitter MainSplitter = new cDesignerSplitter();

            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            GUI_ListClasses.IsCheckBoxes = true;
            GUI_ListClasses.IsSelectAll = true;
            GUI_ListClasses.ClassType = eClassType.PHENOTYPE;
            if (!GUI_ListClasses.Run().IsSucceed) return;

            List<cCellularPhenotype> ListPheno = new List<cCellularPhenotype>();
            for (int i = 0; i < GUI_ListClasses.GetOutPut()[0].Count; i++)
            {
                if (GUI_ListClasses.GetOutPut()[0][i] == 1)
                    ListPheno.Add(cGlobalInfo.ListCellularPhenotypes[i]);
            }

            List<cDescriptorType> ListSelectedDesc = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors();


            for (int i = 0; i < ListSelectedDesc.Count; i++)
            {
                if (ListSelectedDesc[i].IsConnectedToDatabase == false) continue;

                List<cDescriptorType> LType = new List<cDescriptorType>();
                LType.Add(ListSelectedDesc[i]);

                cExtendedTable CompleteTable = this.GetSingleObjectDescriptorValues(LType, ListPheno, true);

                //cExtendedTable CompleteTable = GLP.GetOutPut();

                cCurveForGraph CFG = new cCurveForGraph();
                CFG.SetInputData(CompleteTable);
                CFG.Run();


                cSigmoidFitting SF = new cSigmoidFitting();
                SF.SetInputData(CompleteTable);
                if (SF.Run().IsSucceed == false) continue;

                // double Ratio = LR.GetOutPut()[0][LR.GetOutPut().Count - 1] / SF.GetOutPut()[0][SF.GetOutPut().Count - 1];


                cExtendedTable Sigmoid = SF.GetFittedRawValues(CFG.GetListXValues());
                CompleteTable[0] = Sigmoid[1];
                CompleteTable[0].Name = LType[0].GetName() + "\n" + Sigmoid[1].Name;
                cDesignerSplitter DS = new cDesignerSplitter();

                //cViewerTableAsRichText VT = new cViewerTableAsRichText();
                cViewerTable VT = new cViewerTable();
                cExtendedTable TableResults = SF.GetOutPut();
                //TableResults[0].Add(S.GetOutPut()[0][0]);
                //TableResults[0].Add(S.GetOutPut()[0][1]);
                //TableResults.ListRowNames.Add("p-Value");
                //TableResults.ListRowNames.Add("Rejected?");

                TableResults[0].Name = "Fitting Parameters";
                VT.SetInputData(TableResults);
                VT.DigitNumber = -1;
                VT.Run();

                cViewerGraph1D VS1 = new cViewerGraph1D();
                //VS1.SetInputData(/*new cExtendedTable(AN.GetOutPut()[1])*/CompleteTable);
                VS1.SetInputData(new cExtendedTable(Sigmoid[1]));

                VS1.AddCurve(CFG);

                VS1.Chart.X_AxisValues = Sigmoid[0];//DGS.GetOutPut()[0];
                VS1.Chart.IsLogAxis = true;
                VS1.Chart.IsLine = true;
                VS1.Chart.IsShadow = true;
                VS1.Chart.Opacity = 210;
                VS1.Chart.LineWidth = 3;
                //VS1.Chart.IsDisplayValues = true;
                VS1.Chart.LabelAxisX = "Concentration";
                VS1.Chart.LabelAxisY = CompleteTable[1].Name;
                VS1.Chart.XAxisFormatDigitNumber = -1;
                VS1.Chart.IsZoomableX = true;

                Classes.Base_Classes.General.cLineVerticalForGraph VLForEC50 = new Classes.Base_Classes.General.cLineVerticalForGraph(SF.GetOutPut()[0][2]);
                VLForEC50.AddText("EC50: " + SF.GetOutPut()[0][2].ToString("e3")/* + "\nError Ratio:" + Ratio.ToString("N4")*/);
                VS1.Chart.ListVerticalLines.Add(VLForEC50);

                TableForGeneralResuts.ListRowNames.Add(ListSelectedDesc[i].GetName());
                TableForGeneralResuts.ListTags.Add(ListSelectedDesc[i]);
                TableForGeneralResuts[0].Add(SF.GetOutPut()[0][2]);

                VS1.Chart.ArraySeriesInfo = new cSerieInfoDesign[CompleteTable.Count];

                for (int IdxCurve = 0; IdxCurve < CompleteTable.Count; IdxCurve++)
                {
                    cSerieInfoDesign TmpSerieInfo = new cSerieInfoDesign();
                    TmpSerieInfo.color = cGlobalInfo.ListCellularPhenotypes[IdxCurve % cGlobalInfo.ListCellularPhenotypes.Count].ColourForDisplay;

                    TmpSerieInfo.markerStyle = MarkerStyle.Circle;
                    VS1.Chart.ArraySeriesInfo[IdxCurve] = TmpSerieInfo;
                }

                VS1.Run();

                DS.SetInputData(VS1.GetOutPut());
                DS.SetInputData(VT.GetOutPut());
                DS.Orientation = Orientation.Horizontal;
                DS.Title = ListSelectedDesc[i].GetName();
                DS.Run();
                DT.SetInputData(DS.GetOutPut());
            }

            DT.Run();
            cExtendedControl TextEC = DT.GetOutPut();
            if (TextEC == null) return;

            TextEC.Width = 0;
            TextEC.Height = 0;

            TextEC.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                    | System.Windows.Forms.AnchorStyles.Left
                    | System.Windows.Forms.AnchorStyles.Right);

            MainSplitter.SetInputData(TextEC);


            cSort S = new cSort();
            S.SetInputData(TableForGeneralResuts);
            S.IsAscending = true;
            S.Run();


            cViewerTable VTForEC50 = new cViewerTable();
            VTForEC50.SetInputData(S.GetOutPut());
            VTForEC50.DigitNumber = -1;
            VTForEC50.Run();

            MainSplitter.SetInputData(VTForEC50.GetOutPut());

            MainSplitter.Orientation = Orientation.Vertical;
            MainSplitter.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(MainSplitter.GetOutPut());
            DTW.Title = "DRC analysis - " + this.Count + " Wells";

            DTW.Run();
            DTW.Display();
        }

        void ToolStripMenuItem_ComputeDisplayDRC(object sender, EventArgs e)
        {
            //GlobalInfo = (cGlobalInfo)this.Sender;
            //if (GlobalInfo == null)
            //    GlobalInfo = cGlobalInfo.CurrentScreening.GlobalInfo;
            //if (GlobalInfo == null) return;
            //int DescriptorIdx = GlobalInfo.CurrentScreen.ListDescriptors.GetDescriptorIndex(GlobalInfo.CurrentScreen.ListDescriptors.GetActiveDescriptor());

            //cDesignerTab DT = new cDesignerTab();  
            cDesignerMultiChoices DT = new cDesignerMultiChoices();
            // DT.IsMultiline = true;

            cExtendedTable TableForGeneralResuts = new cExtendedTable();
            TableForGeneralResuts.Add(new cExtendedList("p-Value"));
            TableForGeneralResuts.Add(new cExtendedList("EC50"));
            TableForGeneralResuts.Add(new cExtendedList("Slope"));
            TableForGeneralResuts.Add(new cExtendedList("Bottom"));
            TableForGeneralResuts.Add(new cExtendedList("Top"));
            TableForGeneralResuts.Add(new cExtendedList("Window"));


            TableForGeneralResuts.ListRowNames = new List<string>();
            TableForGeneralResuts.ListTags = new List<object>();

            cDesignerSplitter MainSplitter = new cDesignerSplitter();

            List<cDescriptorType> ListSelectedDesc = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors();
            for (int i = 0; i < ListSelectedDesc.Count; i++)
            {
                List<cDescriptorType> LType = new List<cDescriptorType>();
                LType.Add(ListSelectedDesc[i]);
                cExtendedTable CompleteTable = this.GetAverageDescriptorValues(LType, true, false);

                //cExtendedTable CompleteTable = GLP.GetOutPut();

                cCurveForGraph CFG = new cCurveForGraph();
                CFG.SetInputData(CompleteTable);
                CFG.Run();

                cSigmoidFitting SF = new cSigmoidFitting();
                SF.SetInputData(CompleteTable);
                if (SF.Run().IsSucceed == false) continue;

                // double Ratio = LR.GetOutPut()[0][LR.GetOutPut().Count - 1] / SF.GetOutPut()[0][SF.GetOutPut().Count - 1];

                cANOVA A = new cANOVA();
                //cExtendedTable NewTable = CFG.ListPtValues.Crop(0, CFG.ListPtValues.Count - 1, 1, CFG.ListPtValues[0].Count - 1);
                A.SignificanceThreshold = 1E-11;
                A.SetInputData(CFG.ListPtValues/*NewTable*/);
                A.Run();

                cExtendedTable Sigmoid = SF.GetFittedRawValues(CFG.GetListXValues());
                CompleteTable[0] = Sigmoid[1];
                CompleteTable[0].Name = LType[0].GetName() + "\n" + Sigmoid[1].Name;
                cDesignerSplitter DS = new cDesignerSplitter();

                //cViewerTableAsRichText VT = new cViewerTableAsRichText();
                cViewerTable VT = new cViewerTable();
                cExtendedTable TableResults = SF.GetOutPut();

                if ((A.GetOutPut() != null) && (A.GetOutPut().Count > 0))
                {
                    TableResults[0].Add(A.GetOutPut()[0][0]);
                    TableResults[0].Add(A.GetOutPut()[0][1]);
                    TableResults.ListRowNames.Add("p-Value");
                    TableResults.ListRowNames.Add("Rejected?");

                    TableForGeneralResuts[0].Add(A.GetOutPut()[0][0]);
                }
                else
                {
                    TableForGeneralResuts[0].Add(1);
                }

                TableResults[0].Name = "Fitting Parameters";
                VT.SetInputData(TableResults);
                VT.DigitNumber = -1;
                VT.Run();

                cViewerGraph1D VS1 = new cViewerGraph1D();
                //VS1.SetInputData(/*new cExtendedTable(AN.GetOutPut()[1])*/CompleteTable);

                cExtendedTable MyTable = new cExtendedTable(Sigmoid[1]);

                VS1.SetInputData(MyTable);

                VS1.AddCurve(CFG);

                VS1.Chart.X_AxisValues = Sigmoid[0];//DGS.GetOutPut()[0];
                VS1.Chart.IsLogAxis = true;
                VS1.Chart.IsLine = true;
                VS1.Chart.IsShadow = false;
                VS1.Chart.Opacity = 210;
                VS1.Chart.LineWidth = 3;
                VS1.Chart.MarkerSize = 8;
                //VS1.Chart.IsDisplayValues = true;
                VS1.Chart.LabelAxisX = "Concentration";
                VS1.Chart.LabelAxisY = CompleteTable[1].Name;
                VS1.Chart.XAxisFormatDigitNumber = -1;
                VS1.Chart.IsZoomableX = true;

                Classes.Base_Classes.General.cLineVerticalForGraph VLForEC50 = new Classes.Base_Classes.General.cLineVerticalForGraph(SF.GetOutPut()[0][2]);
                //VLForEC50.AddText("EC50: " + SF.GetOutPut()[0][2].ToString("e3")/* + "\nError Ratio:" + Ratio.ToString("N4")*/);
                VS1.Chart.ListVerticalLines.Add(VLForEC50);


                TableForGeneralResuts.ListRowNames.Add(ListSelectedDesc[i].GetName());
                TableForGeneralResuts.ListTags.Add((Chart)VS1.Chart/*ListSelectedDesc[i]*/);

                //EC50
                TableForGeneralResuts[1].Add(SF.GetOutPut()[0][2]);

                // Slope
                TableForGeneralResuts[2].Add(SF.GetOutPut()[0][3]);

                // Bottom
                TableForGeneralResuts[3].Add(SF.GetOutPut()[0][0]);

                // Top
                TableForGeneralResuts[4].Add(SF.GetOutPut()[0][1]);

                // Window
                double Window = SF.GetOutPut()[0][1] / SF.GetOutPut()[0][0];
                TableForGeneralResuts[5].Add(Window);

                VS1.Chart.ArraySeriesInfo = new cSerieInfoDesign[CompleteTable.Count];

                for (int IdxCurve = 0; IdxCurve < CompleteTable.Count; IdxCurve++)
                {
                    cSerieInfoDesign TmpSerieInfo = new cSerieInfoDesign();
                    TmpSerieInfo.color = cGlobalInfo.ListCellularPhenotypes[IdxCurve % cGlobalInfo.ListCellularPhenotypes.Count].ColourForDisplay;
                    TmpSerieInfo.markerStyle = MarkerStyle.None;
                    VS1.Chart.ArraySeriesInfo[IdxCurve] = TmpSerieInfo;
                }

                VS1.Run();

                DS.SetInputData(VS1.GetOutPut());
                DS.SetInputData(VT.GetOutPut());
                DS.Orientation = Orientation.Horizontal;
                DS.Title = ListSelectedDesc[i].GetName();
                DS.Run();
                DT.SetInputData(DS.GetOutPut());
            }

            DT.Run();
            cExtendedControl TextEC = DT.GetOutPut();
            if (TextEC == null) return;

            TextEC.Width = 0;
            TextEC.Height = 0;

            TextEC.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                    | System.Windows.Forms.AnchorStyles.Left
                    | System.Windows.Forms.AnchorStyles.Right);

            MainSplitter.SetInputData(TextEC);


            cSort S = new cSort();
            S.SetInputData(TableForGeneralResuts);
            S.IsAscending = true;
            S.Run();


            cViewerTable VTForEC50 = new cViewerTable();
            VTForEC50.SetInputData(S.GetOutPut());
            VTForEC50.DigitNumber = -1;
            VTForEC50.Run();

            MainSplitter.SetInputData(VTForEC50.GetOutPut());

            MainSplitter.Orientation = Orientation.Vertical;
            MainSplitter.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(MainSplitter.GetOutPut());
            DTW.Title = "DRC analysis - " + this.Count + " Wells - " + this.Name;
            DTW.Run();
            DTW.Display();
        }

        void ToolStripMenuItem_GetDistanceMatrix(object sender, EventArgs e)
        {
            cExtendedTable ET = this.GetDistanceMatrix(eDistances.EUCLIDEAN);

            cDisplayExtendedTable DET = new cDisplayExtendedTable();
            DET.SetInputData(ET);
            DET.Run();
        }

        void ToolStripMenuItem_GetTable(object sender, EventArgs e)
        {
            cGetListWellValues GLWV = new cGetListWellValues();
            GLWV.SetInputData(this);
            GLWV.ListProperties.FindByName("Include Images?").SetNewValue((bool)false);
            GLWV.ListProperties.FindByName("Include Images?").IsGUIforValue = true;
            GLWV.Run();


            cExtendedTable DT = GLWV.GetOutPut();  // GetAverageDescriptorValuesFull();
            cDisplayExtendedTable DET = new cDisplayExtendedTable();
            DET.SetInputData(DT);
            DET.Run();
        }

        void ToolStripMenuItem_Display3DDRC(object sender, EventArgs e)
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
            cWellMerger WM = new cWellMerger();
            WM.IsGUI = false;
            WM.ListSelectedProp = new List<cPropertyType>();
            WM.ListSelectedProp.Add(ConcProperty);
            WM.SetInputData(this);
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

            bool ToBeCreated = false;
            cViewer3D V3D = new cViewer3D();

            c3DNewWorld MyWorld = cGlobalInfo.GetActive3DWorld();

            if (MyWorld == null)
            {
                MyWorld = new c3DNewWorld(new cPoint3D(1, 1, 1), new cPoint3D(1, 1, 1));
                ToBeCreated = true;
            }

            cListGeometric3DObject GlobalList = new cListGeometric3DObject("3D DRC object");


            List<cDescriptorType> ListActiveDesc = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors();

            List<cDescriptorType> TmpListDesc = new List<cDescriptorType>();
            List<double> ListMin = new List<double>();
            List<double> ListMax = new List<double>();

            TmpListDesc.Add(ListActiveDesc[0]);
            cExtendedTable TmpTable = cGlobalInfo.CurrentScreening.GetListWells().GetAverageDescriptorValues(TmpListDesc, false, false);
            ListMin.Add(TmpTable[0].Min());
            ListMax.Add(TmpTable[0].Max());

            TmpListDesc[0] = ListActiveDesc[1];
            TmpTable = cGlobalInfo.CurrentScreening.GetListWells().GetAverageDescriptorValues(TmpListDesc, false, false);
            ListMin.Add(TmpTable[0].Min());
            ListMax.Add(TmpTable[0].Max());

            TmpListDesc[0] = ListActiveDesc[2];
            TmpTable = cGlobalInfo.CurrentScreening.GetListWells().GetAverageDescriptorValues(TmpListDesc, false, false);
            ListMin.Add(TmpTable[0].Min());
            ListMax.Add(TmpTable[0].Max());


            cListExtendedTable ET = SortedList.GetAverageDescriptorValues(ListActiveDesc);

            //ET[0]
            c3DDRC_Curve _3dDRC = new c3DDRC_Curve(ET, ListMin, ListMax);
            _3dDRC.Create(new cPoint3D(0, 0, 0), Color.Red);
            //  _3dDRC.SetName("_3D DRC Curve");
            GlobalList.Add(_3dDRC);


            //   c3DSphere MySphere = new c3DSphere(new cPoint3D(0, 0, 0), 20,Color.Red);
            // GlobalList.Add(MySphere);

            //cNormalize N = new cNormalize();
            //N.SetInputData(CompleteTable);
            //N.NormalizationType = eNormalizationType.MIN_MAX;
            //N.Run();
            //cExtendedTable NormTable = N.GetOutPut();

            //FormForOptionsSingleCellAnalysis OptionsWindow = new FormForOptionsSingleCellAnalysis();
            //OptionsWindow.radioButtonInitPhenoClassDesc.Enabled = false;
            //if (OptionsWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;


            //int ColorMode = 0;
            //if (OptionsWindow.radioButtonInitPhenoClassDB.Checked)
            //    ColorMode = 0;
            //else if (OptionsWindow.radioButtonInitPhenoClassWell.Checked)
            //    ColorMode = 1;
            //else if (OptionsWindow.radioButtonInitPhenoClassDesc.Checked)
            //    ColorMode = 2;




            //c3DObject_Axis Axis = new c3DObject_Axis();
            //cExtendedTable T = new cExtendedTable();
            //T.Add(new cExtendedList(CompleteTable[0].Name));

            //T[0].Tag = CompleteTable[0].Tag;
            //T[0].Add(0);
            //T[0].Add(1);
            //T.Add(new cExtendedList(CompleteTable[1].Name));
            //T[1].Tag = CompleteTable[1].Tag;
            //T[1].Add(0);
            //T[1].Add(1);

            //if (CompleteTable.Count > 2)
            //{
            //    T.Add(new cExtendedList(CompleteTable[2].Name));
            //    T[2].Tag = CompleteTable[2].Tag;
            //    T[2].Add(0);
            //    T[2].Add(1);
            //}

            //Axis.SetInputData(T);
            //Axis.Run(MyWorld);

            //GlobalList.AddRange(Axis.GetOutPut());

            // GlobalList.Add(Axis);




            foreach (var item in GlobalList)
            {
                MyWorld.AddGeometric3DObject(item);
            }

            // MyWorld.BackGroundColor = cGlobalInfo.OptionsWindow.FFAllOptions.panelFor3DBackColor.BackColor;


            if (ToBeCreated)
            {
                V3D.SetInputData(MyWorld);
                V3D.Run();

                cDisplayToWindow DTW = new cDisplayToWindow();
                DTW.SetInputData(V3D.GetOutPut());
                DTW.Title = "3D DRC- " + SortedList.Count + " points";
                DTW.Run();

                DTW.Display();
            }
            else
            {
                MyWorld.Redraw();
            }




            #endregion

            /*
            cDisplayListWell3DScatter _3DScatterPt = new cDisplayListWell3DScatter();
            _3DScatterPt.SetInputData(this);

            _3DScatterPt.ListProperties.FindByName("Desc. 1").SetNewValue((string)cGlobalInfo.CurrentScreening.GetActiveDescriptors()[0].GetName());
            _3DScatterPt.ListProperties.FindByName("Desc. 1").IsGUIforValue = true;

            int NextDesc = 0;
            if (cGlobalInfo.CurrentScreening.ListDescriptors.Count > 1)
                NextDesc = 1;
            _3DScatterPt.ListProperties.FindByName("Desc. 2").SetNewValue((string)cGlobalInfo.CurrentScreening.GetActiveDescriptors()[NextDesc].GetName());
            _3DScatterPt.ListProperties.FindByName("Desc. 2").IsGUIforValue = true;

            if (cGlobalInfo.CurrentScreening.ListDescriptors.Count == 1)
                NextDesc = 0;
            else if (cGlobalInfo.CurrentScreening.ListDescriptors.Count == 2)
                NextDesc = 1;
            else
                NextDesc = 2;
            _3DScatterPt.ListProperties.FindByName("Desc. 3").SetNewValue((string)cGlobalInfo.CurrentScreening.GetActiveDescriptors()[NextDesc].GetName());
            _3DScatterPt.ListProperties.FindByName("Desc. 3").IsGUIforValue = true;

            _3DScatterPt.ListProperties.FindByName("Draw Axis ?").SetNewValue((bool)true);
            _3DScatterPt.ListProperties.FindByName("Draw Axis ?").IsGUIforValue = true;
            _3DScatterPt.ListProperties.FindByName("Normalized ?").SetNewValue((bool)true);
            _3DScatterPt.ListProperties.FindByName("Normalized ?").IsGUIforValue = true;
            _3DScatterPt.ListProperties.FindByName("Link Points ?").SetNewValue((bool)false);
            _3DScatterPt.ListProperties.FindByName("Link Points ?").IsGUIforValue = true;

            _3DScatterPt.Run();*/
        }

        void ToolStripMenuItem_Display3DScatter(object sender, EventArgs e)
        {
            cDisplayListWell3DScatter _3DScatterPt = new cDisplayListWell3DScatter();
            _3DScatterPt.SetInputData(this);

            _3DScatterPt.ListProperties.FindByName("Desc. 1").SetNewValue((string)cGlobalInfo.CurrentScreening.GetActiveDescriptors()[0].GetName());
            _3DScatterPt.ListProperties.FindByName("Desc. 1").IsGUIforValue = true;

            int NextDesc = 0;
            if (cGlobalInfo.CurrentScreening.ListDescriptors.Count > 1)
                NextDesc = 1;
            _3DScatterPt.ListProperties.FindByName("Desc. 2").SetNewValue((string)cGlobalInfo.CurrentScreening.GetActiveDescriptors()[NextDesc].GetName());
            _3DScatterPt.ListProperties.FindByName("Desc. 2").IsGUIforValue = true;

            if (cGlobalInfo.CurrentScreening.ListDescriptors.Count == 1)
                NextDesc = 0;
            else if (cGlobalInfo.CurrentScreening.ListDescriptors.Count == 2)
                NextDesc = 1;
            else
                NextDesc = 2;
            _3DScatterPt.ListProperties.FindByName("Desc. 3").SetNewValue((string)cGlobalInfo.CurrentScreening.GetActiveDescriptors()[NextDesc].GetName());
            _3DScatterPt.ListProperties.FindByName("Desc. 3").IsGUIforValue = true;

            _3DScatterPt.ListProperties.FindByName("Draw Axis ?").SetNewValue((bool)true);
            _3DScatterPt.ListProperties.FindByName("Draw Axis ?").IsGUIforValue = true;
            _3DScatterPt.ListProperties.FindByName("Normalized ?").SetNewValue((bool)true);
            _3DScatterPt.ListProperties.FindByName("Normalized ?").IsGUIforValue = true;
            _3DScatterPt.ListProperties.FindByName("Link Points ?").SetNewValue((bool)false);
            _3DScatterPt.ListProperties.FindByName("Link Points ?").IsGUIforValue = true;

            _3DScatterPt.Run();


        }

        public cExtendedTable GetAverageDescriptorValuesFull()
        {
            if (this.Count == 0) return null;

            cExtendedTable ToBeReturned = new cExtendedTable();
            ToBeReturned.Tag = this;
            ToBeReturned.ListTags = new List<object>();
            ToBeReturned.Name = this.Count + " wells associated data table";
            ToBeReturned.ListRowNames = new List<string>();
            //GlobalInfo = cGlobalInfo.CurrentScreening.GlobalInfo;

            foreach (var item in cGlobalInfo.CurrentScreening.ListDescriptors)
            {
                if (item.IsActive())
                {
                    ToBeReturned.Add(new cExtendedList(item.GetName()));
                    ToBeReturned[ToBeReturned.Count - 1].Tag = item;
                    ToBeReturned[ToBeReturned.Count - 1].ListTags = new List<object>();
                }
            }
            ToBeReturned.Add(new cExtendedList("Class"));
            ToBeReturned[ToBeReturned.Count - 1].ListTags = new List<object>();

            foreach (var item in this)
            {
                cExtendedList CEL = item.GetAverageValuesList(true)[0];
                int IdxDesc = 0;
                foreach (var Desc in CEL)
                {
                    ToBeReturned[IdxDesc].Add(CEL[IdxDesc]);
                    ToBeReturned[IdxDesc++].ListTags.Add(item);
                }
                ToBeReturned[ToBeReturned.Count - 1].Add(item.GetCurrentClassIdx());
                ToBeReturned[ToBeReturned.Count - 1].ListTags.Add(item.GetClassType());
                ToBeReturned.ListRowNames.Add(item.GetShortInfo());

                ToBeReturned.ListTags.Add(item);

            }
            return ToBeReturned;
        }

        public cExtendedTable GetAverageDescriptorValues(List<cDescriptorType> ListDesc, bool IsConcentration, bool IsClass)
        {
            if ((ListDesc == null) || (this.Count == 0)) return null;

            cExtendedTable ToBeReturned = new cExtendedTable();
            //  GlobalInfo = cGlobalInfo.CurrentScreening.GlobalInfo;
            ToBeReturned.Tag = this;

            //foreach (var item incGlobalInfo.ListWellClasses)
            //{
            //    ToBeReturned.Add(new cExtendedList(item.Name));
            //    ToBeReturned[ToBeReturned.Count - 1].Tag = item;
            //    ToBeReturned[ToBeReturned.Count - 1].ListTags = new List<object>();
            //}


            if (IsConcentration)
            {
                ToBeReturned.Add(new cExtendedList());
                ToBeReturned[ToBeReturned.Count - 1].Name = "Concentration";
                ToBeReturned[ToBeReturned.Count - 1].ListTags = new List<object>();
            }
            if (IsClass)
            {
                ToBeReturned.Add(new cExtendedList());
                ToBeReturned[ToBeReturned.Count - 1].Name = "Class";
                ToBeReturned[ToBeReturned.Count - 1].ListTags = new List<object>();
            }


            cExtendedList ListIdxDescriptor = new cExtendedList();

            foreach (var item in ListDesc)
            {
                ToBeReturned.Add(new cExtendedList());
                ToBeReturned[ToBeReturned.Count - 1].Name = item.GetName();
                ToBeReturned[ToBeReturned.Count - 1].Tag = item;
                ToBeReturned[ToBeReturned.Count - 1].ListTags = new List<object>();
                ListIdxDescriptor.Add(cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(item));
            }



            ToBeReturned.ListTags = new List<object>();
            ToBeReturned.ListRowNames = new List<string>();

            int IdxDesc = 0;
            foreach (cWell TmpWell in this)
            {
                IdxDesc = 0;
                cExtendedList CL = TmpWell.GetAverageValuesList(ListIdxDescriptor);

                if (IsConcentration)
                {
                    object ConcentrationValue = TmpWell.ListProperties.FindValueByName("Concentration");
                    if (ConcentrationValue == null) continue;
                    ToBeReturned[IdxDesc].Add((double)ConcentrationValue);
                    ToBeReturned[IdxDesc++].ListTags.Add(TmpWell);
                }
                if (IsClass)
                {
                    //object ClassValue = TmpWell.GetCurrentClassIdx();
                    // if (ConcentrationValue == null) continue;
                    ToBeReturned[IdxDesc].Add((double)TmpWell.GetCurrentClassIdx());
                    ToBeReturned[IdxDesc++].ListTags.Add(TmpWell);
                }


                foreach (var item in CL)
                {
                    ToBeReturned[IdxDesc].Add(item);
                    ToBeReturned[IdxDesc++].ListTags.Add(TmpWell);
                }


                ToBeReturned.ListTags.Add(TmpWell);
                ToBeReturned.ListRowNames.Add(TmpWell.GetShortInfo());

                //ToBeReturned[TmpWell.GetClassIdx()]
                //   ToBeReturned[TmpWell.GetClassIdx()].Add(TmpWell.ListDescriptors[IDxDesc].GetValue());
                //  ToBeReturned[TmpWell.GetClassIdx()].ListTags.Add(TmpWell);
            }
            return ToBeReturned;

        }

        public cExtendedTable GetAverageDescriptorValues(int IDxDesc)
        {
            if (this.Count == 0) return null;
            if (IDxDesc >= this[0].ListSignatures.Count) return null;

            cExtendedTable ToBeReturned = new cExtendedTable();
            //GlobalInfo = cGlobalInfo.CurrentScreening.GlobalInfo;

            foreach (var item in cGlobalInfo.ListWellClasses)
            {
                ToBeReturned.Add(new cExtendedList(item.Name));
                ToBeReturned[ToBeReturned.Count - 1].Tag = item;
                ToBeReturned[ToBeReturned.Count - 1].ListTags = new List<object>();
            }

            foreach (cWell TmpWell in this)
            {
                ToBeReturned[TmpWell.GetCurrentClassIdx()].Add(TmpWell.ListSignatures[IDxDesc].GetValue());
                ToBeReturned[TmpWell.GetCurrentClassIdx()].ListTags.Add(TmpWell);
            }
            return ToBeReturned;
        }

        public cExtendedTable GetListWellClasses()
        {
            cExtendedTable ToReturn = new cExtendedTable();
            ToReturn.Name = "List classes (" + this.Count + " wells)";

            ToReturn.ListRowNames = new List<string>();
            cExtendedList L = new cExtendedList("Classes");
            L.ListTags = new List<object>();

            ToReturn.Add(L);

            foreach (var item in this)
            {
                int IdxClass = item.GetCurrentClassIdx();
                if (IdxClass < 0) continue;
                L.ListTags.Add(item);
                ToReturn.ListRowNames.Add(item.GetShortInfo());
                L.Add(item.GetCurrentClassIdx());
            }
            return ToReturn;

        }

        public cExtendedTable GetSingleObjectPhenotypeID(List<cCellularPhenotype> ListPhenotypes)
        {
            if (this.Count == 0) return null;

            //GlobalInfo = cGlobalInfo.CurrentScreening.GlobalInfo;

            cExtendedTable ToBeReturned = new cExtendedTable();


            foreach (cWell TmpWell in this)
            {

                TmpWell.AssociatedPlate.DBConnection = new cDBConnection(TmpWell.AssociatedPlate, TmpWell.SQLTableName);
                cExtendedTable DescValues = null;

                DescValues = TmpWell.AssociatedPlate.DBConnection.GetWellPhenotypeId(TmpWell, ListPhenotypes);

                TmpWell.AssociatedPlate.DBConnection.CloseConnection();

                if (DescValues.Count == 0) continue;

                cExtendedTable TmpTable = new cExtendedTable();

                for (int i = 0; i < DescValues.Count; i++)
                    TmpTable.Add(DescValues[i]);


                if ((ToBeReturned == null) || (ToBeReturned.Count < 1))
                {
                    ToBeReturned = new cExtendedTable(TmpTable);
                }
                else
                {
                    cMerge M = new cMerge();
                    M.SetInputData(ToBeReturned, TmpTable);
                    M.IsHorizontal = false;
                    M.Run();
                    ToBeReturned = M.GetOutPut();
                }

            }
            return ToBeReturned;
        }

        public cExtendedTable GetSingleObjectDescriptorValues(List<cDescriptorType> ListDesc, List<cCellularPhenotype> ListPhenotypes, bool IsConcentration)
        {
            if (this.Count == 0) return null;

            //GlobalInfo = cGlobalInfo.CurrentScreening.GlobalInfo;

            cExtendedTable ToBeReturned = new cExtendedTable();
            if (IsConcentration)
            {
                ToBeReturned.Add(new cExtendedList());
                ToBeReturned[ToBeReturned.Count - 1].Name = "Concentration";
                //   ToBeReturned[ToBeReturned.Count - 1].ListTags = new List<object>();
            }

            //   cExtendedList ListIdxDescriptor = new cExtendedList();

            //foreach (var item in ListDesc)
            //{
            //    if (item.IsConnectedToDatabase == false) continue;
            //    ToBeReturned.Add(new cExtendedList());
            //    ToBeReturned[ToBeReturned.Count - 1].Name = item.GetName();
            //    ToBeReturned[ToBeReturned.Count - 1].Tag = item;
            //    //   ToBeReturned[ToBeReturned.Count - 1].ListTags = new List<object>();
            //    ListIdxDescriptor.Add(GlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(item));
            //}

            //  ToBeReturned.ListTags = new List<object>();
            //  ToBeReturned.ListRowNames = new List<string>();

            //   int IdxDesc = 0;


            foreach (cWell TmpWell in this)
            {
                //int IdxDesc = 0;
                //  cExtendedList CL = TmpWell.Get.GetAverageValuesList(ListIdxDescriptor);

                TmpWell.AssociatedPlate.DBConnection = new cDBConnection(TmpWell.AssociatedPlate, TmpWell.SQLTableName);
                cExtendedTable DescValues = null;
                if (ListPhenotypes == null)
                    DescValues = TmpWell.AssociatedPlate.DBConnection.GetWellValues(TmpWell, ListDesc);
                else
                    DescValues = TmpWell.AssociatedPlate.DBConnection.GetWellValues(TmpWell, ListDesc, ListPhenotypes);
                TmpWell.AssociatedPlate.DBConnection.CloseConnection();

                if (DescValues.Count == 0) continue;

                cExtendedTable TmpTable = new cExtendedTable();

                if (IsConcentration)
                {
                    object ConcentrationValue = TmpWell.ListProperties.FindValueByName("Concentration");
                    if (ConcentrationValue != null)
                    {
                        cExtendedList ConcentrationList = new cExtendedList("Concentration");

                        for (int i = 0; i < DescValues[0].Count; i++)
                        {
                            ConcentrationList.Add((double)ConcentrationValue);
                        }

                        TmpTable.Add(ConcentrationList);
                        //ToBeReturned[IdxDesc].Add((double)ConcentrationValue);
                        //ToBeReturned[IdxDesc++].ListTags.Add(TmpWell);
                    }
                }


                for (int i = 0; i < DescValues.Count; i++)
                {
                    TmpTable.Add(DescValues[i]);
                }


                if ((ToBeReturned == null) || (ToBeReturned.Count < 1))
                {
                    ToBeReturned = new cExtendedTable(TmpTable);
                }
                else
                {
                    cMerge M = new cMerge();
                    M.SetInputData(ToBeReturned, TmpTable);
                    M.IsHorizontal = false;
                    M.Run();
                    ToBeReturned = M.GetOutPut();
                }


                //foreach (var item in CL)
                //{
                //    ToBeReturned[IdxDesc].Add(item);
                //    ToBeReturned[IdxDesc++].ListTags.Add(TmpWell);
                //}

                //    ToBeReturned.ListTags.Add(TmpWell);
                //   ToBeReturned.ListRowNames.Add(TmpWell.GetShortInfo());

            }
            return ToBeReturned;

        }

        public cExtendedTable GetSingleObjectDescriptorValues(List<cDescriptorType> ListDesc)
        {
            if (this.Count == 0) return null;

            cExtendedTable ToBeReturned = new cExtendedTable();

            foreach (cWell TmpWell in this)
            {
                TmpWell.AssociatedPlate.DBConnection = new cDBConnection(TmpWell.AssociatedPlate, TmpWell.SQLTableName);
                cExtendedTable DescValues = null;

                DescValues = TmpWell.AssociatedPlate.DBConnection.GetWellValues(TmpWell, ListDesc);
                TmpWell.AssociatedPlate.DBConnection.CloseConnection();

                if (DescValues.Count == 0) continue;

                cExtendedTable TmpTable = new cExtendedTable();

                for (int i = 0; i < DescValues.Count; i++)
                {
                    TmpTable.Add(DescValues[i]);
                }

                if ((ToBeReturned == null) || (ToBeReturned.Count < 1))
                {
                    ToBeReturned = new cExtendedTable(TmpTable);
                }
                else
                {
                    cMerge M = new cMerge();
                    M.SetInputData(ToBeReturned, TmpTable);
                    M.IsHorizontal = false;
                    M.Run();
                    ToBeReturned = M.GetOutPut();
                }
            }
            return ToBeReturned;
        }

        public cExtendedTable GetSingleObjectDescriptorValuesFull(List<cDescriptorType> ListDesc)
        {
            if (this.Count == 0) return null;

            cExtendedTable ToBeReturned = new cExtendedTable();

            foreach (cWell TmpWell in this)
            {
                TmpWell.AssociatedPlate.DBConnection = new cDBConnection(TmpWell.AssociatedPlate, TmpWell.SQLTableName);
                cExtendedTable DescValues = null;

                DescValues = TmpWell.AssociatedPlate.DBConnection.GetWellValues(TmpWell, ListDesc);


                if (DescValues.Count == 0) continue;

                cExtendedTable TmpTable = new cExtendedTable();

                for (int i = 0; i < DescValues.Count; i++)
                {
                    TmpTable.Add(DescValues[i]);
                }

                // now time to get the phenotypic classes
                cExtendedTable IDTable = TmpWell.AssociatedPlate.DBConnection.GetWellPhenotypeId(TmpWell, null);

                TmpWell.AssociatedPlate.DBConnection.CloseConnection();

                TmpTable.Add(IDTable[0]);


                if ((ToBeReturned == null) || (ToBeReturned.Count < 1))
                {
                    ToBeReturned = new cExtendedTable(TmpTable);
                }
                else
                {

                    cMerge M = new cMerge();
                    M.SetInputData(ToBeReturned, TmpTable);
                    M.IsHorizontal = false;
                    M.Run();
                    ToBeReturned = M.GetOutPut();
                }



            }
            return ToBeReturned;
        }

        public void SingleCellBasedAnalysis()
        {
            FormForOptionsSingleCellAnalysis OptionsWindow = new FormForOptionsSingleCellAnalysis();
            if (OptionsWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;


            if (OptionsWindow.radioButtonInitPhenoClassWell.Checked)
            {
                DialogResult DR = MessageBox.Show("Update Phenotypes with Well Class colors/names ?", "Single Cell Analysis", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if ((DR != DialogResult.Yes) && (DR != DialogResult.No)) return;
                if (DR == DialogResult.Yes)
                {
                    int MinClass = cGlobalInfo.ListCellularPhenotypes.Count;
                    if (cGlobalInfo.ListWellClasses.Count < MinClass) MinClass = cGlobalInfo.ListWellClasses.Count;

                    for (int i = 0; i < MinClass; i++)
                    {
                        cGlobalInfo.ListCellularPhenotypes[i].ColourForDisplay = cGlobalInfo.ListWellClasses[i].ColourForDisplay;
                        cGlobalInfo.ListCellularPhenotypes[i].Name = cGlobalInfo.ListWellClasses[i].Name;
                    }
                }
            }


            //if (GlobalInfo == null)
            //{
            //    if (this.Count == 0) return;
            //    GlobalInfo = cGlobalInfo.CurrentScreening.GlobalInfo;
            //}

            //cGUI_ListClasses GUIClasses = new cGUI_ListClasses();
            //GUIClasses.IsCheckBoxes = true;
            //GUIClasses.ClassType = eClassType.PHENOTYPE;
            //GUIClasses.IsSelectAll = true;
            //if (!GUIClasses.Run().IsSucceed) return;


            //List<cCellularPhenotype> LPheno = new List<cCellularPhenotype>();

            //for (int i = 0; i < GUIClasses.GetOutPut()[0].Count; i++)
            //{
            // /  if (GUIClasses.GetOutPut()[0][i] == 1)
            //    {
            //        LPheno.Add(cGlobalInfo.ListCellularPhenotypes[i]);
            //    }
            //}

            cExtendedTable FinalTable = null;
            cExtendedList ListClasses = new cExtendedList();

            //cGetListPhenotypes GLP = new cGetListPhenotypes();
            //GLP.SetInputData(this);
            //GLP.Run();
            //cExtendedTable ETLP = GLP.GetOutPut();


            foreach (cWell TmpWell in this)
            {
                if (TmpWell.AssociatedPlate.DBConnection == null)
                {
                    MessageBox.Show("No Database connection.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if ((OptionsWindow.radioButtonInitPhenoClassWell.Checked) && (TmpWell.GetCurrentClassIdx() == -1))
                {
                    cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(TmpWell.GetShortInfo() + " is not active and thus cannot be included in the single analysis process.\n");
                    continue;
                }

                cExtendedTable TmpTable = TmpWell.GetValuesList(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors(), cGlobalInfo.ListCellularPhenotypes);//LPheno);

                if (TmpTable.Count == 0) return;

                int NumCells = TmpTable[0].Count;

                int CurrentWellClass = TmpWell.GetCurrentClassIdx();

                if (OptionsWindow.radioButtonInitPhenoClassWell.Checked)
                {
                    for (int IdxCell = 0; IdxCell < NumCells; IdxCell++)
                        ListClasses.Add(CurrentWellClass);
                }
                else if (OptionsWindow.radioButtonInitPhenoClassDB.Checked)
                {

                    int IdxClassPheno = -1;
                    // First : identify the column index
                    for (int i = 0; i < TmpTable.Count; i++)
                    {
                        if (TmpTable[i].Name == "")
                        {
                            IdxClassPheno = i;
                            break;
                        }
                    }

                    TmpWell.AssociatedPlate.DBConnection = new cDBConnection(TmpWell.AssociatedPlate, TmpWell.SQLTableName);
                    cExtendedTable LSO = TmpWell.AssociatedPlate.DBConnection.GetWellPhenotypeId(TmpWell, cGlobalInfo.ListCellularPhenotypes);// LPheno);
                    TmpWell.AssociatedPlate.DBConnection.CloseConnection();

                    for (int IdxCell = 0; IdxCell < NumCells; IdxCell++)
                        ListClasses.Add(LSO[0][IdxCell]);//LSO[IdxCell].GetAssociatedPhenotype().Idx);
                }
                else
                {
                    //cListSingleBiologicalObjects LSO = TmpWell.AssociatedPlate.DBConnection.GetWellBiologicalPhenotypes(TmpWell);
                    if (cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor().IsConnectedToDatabase)
                    {
                        TmpWell.AssociatedPlate.DBConnection = new cDBConnection(TmpWell.AssociatedPlate, TmpWell.SQLTableName);
                        cExtendedTable DescValues = TmpWell.AssociatedPlate.DBConnection.GetWellValues(TmpWell, cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor());
                        TmpWell.AssociatedPlate.DBConnection.CloseConnection();

                        for (int IdxCell = 0; IdxCell < NumCells; IdxCell++)
                        {
                            int Class = (int)DescValues[0][IdxCell] % cGlobalInfo.ListCellularPhenotypes.Count;
                            ListClasses.Add(Class);
                        }
                    }
                    else
                    {
                        int Class = (int)(TmpWell.ListSignatures.GetSignature(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor()).GetValue()) % cGlobalInfo.ListCellularPhenotypes.Count;
                        for (int IdxCell = 0; IdxCell < NumCells; IdxCell++)
                            ListClasses.Add(Class);
                    }
                }

                if (FinalTable == null)
                {
                    FinalTable = new cExtendedTable(TmpTable);
                }
                else
                {
                    cMerge M = new cMerge();
                    M.SetInputData(FinalTable, TmpTable);
                    M.IsHorizontal = false;
                    M.Run();
                    FinalTable = M.GetOutPut();
                }


            }

            FormForSingleCellsDisplay WindowForTable = new FormForSingleCellsDisplay(FinalTable, ListClasses);
            WindowForTable.AssociatedListWells = this;


            if ((FinalTable == null) || (FinalTable.Count == 0))
            {
                MessageBox.Show("Data corrupted !", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int IdxCol = 0; IdxCol < FinalTable.Count; IdxCol++)
            {
                WindowForTable.comboBoxAxeX.Items.Add(FinalTable[IdxCol].Name);
                WindowForTable.comboBoxAxeY.Items.Add(FinalTable[IdxCol].Name);
                WindowForTable.comboBoxVolume.Items.Add(FinalTable[IdxCol].Name);
            }

            WindowForTable.comboBoxAxeX.Text = WindowForTable.comboBoxAxeX.GetItemText(WindowForTable.comboBoxAxeX.Items[0]);
            WindowForTable.comboBoxAxeY.Text = WindowForTable.comboBoxAxeX.GetItemText(WindowForTable.comboBoxAxeX.Items[0]);
            WindowForTable.comboBoxVolume.Text = WindowForTable.comboBoxAxeX.GetItemText(WindowForTable.comboBoxAxeX.Items[0]);

            WindowForTable.UpdateText();
            WindowForTable.Show();

        }

        public cExtendedTable GetDistanceMatrix(eDistances DistanceType)
        {
            cExtendedTable TableToBeReturned = new cExtendedTable();
            TableToBeReturned.Name = "Distance Matrix -" + this.Count + " wells";

            cExtendedTable ET = this.GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors(), false, false);

            cTranspose TM = new cTranspose();
            TM.SetInputData(ET);
            TM.Run();

            cDistances D = new cDistances();
            D.SetInputData(TM.GetOutPut());
            D.DistanceType = DistanceType;
            D.Run();

            TableToBeReturned = D.GetOutPut();

            return TableToBeReturned;
        }

        #region Context Menu
        public ToolStripMenuItem GetExtendedContextMenu()
        {
            if (this.Count == 0) return null;

            ToolStripMenuItem SpecificContextMenu = GetContextMenu();

            SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_CopyWellsToList = new ToolStripMenuItem("Copy Well(s) to List Wells");
            ToolStripMenuItem_CopyWellsToList.Click += new System.EventHandler(this.ToolStripMenuItem_CopyWellsToList);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_CopyWellsToList);

            return SpecificContextMenu;
        }

        private void ToolStripMenuItem_CopyWellsToList(object sender, EventArgs e)
        {
            foreach (cWell TmpWell in this)
            {
                if (TmpWell.GetCurrentClassIdx() < 0) continue;
                TmpWell.AddToListWellsGUI();
            }
        }

        public TreeNode GetAssociatedTreeNode()
        {
            TreeNode TN = new TreeNode(this.Name.Replace("\n", " ") + " (" + this.Count + " wells)");
            TN.ToolTipText = this.Info;
            TN.Checked = true;
            TN.Tag = this;
            foreach (var item in this)
            {
                if (item.GetCurrentClassIdx() == -1) continue;
                TreeNode TmpNode = item.GetAssociatedTreeNode();

                if (TmpNode != null)
                    TN.Nodes.Add(TmpNode);
            }

            return TN;
        }

        public ToolStripMenuItem GetContextMenu()
        {
            if (this.Count == 0) return null;

            ToolStripMenuItem SpecificContextMenu = new ToolStripMenuItem("List " + this.Count + " wells");

            ToolStripMenuItem ToolStripMenuItem_ChangeClass = new ToolStripMenuItem("Swap Classes");
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChangeClass);

            for (int i = 0; i < cGlobalInfo.ListWellClasses.Count; i++)
            {
                ToolStripMenuItem ToolStripMenuItem_NewClass = new ToolStripMenuItem(cGlobalInfo.ListWellClasses[i].Name);
                ToolStripMenuItem_NewClass.Click += new System.EventHandler(this.ToolStripMenuItem_NewClass);
                ToolStripMenuItem_NewClass.Tag = i;// this[0].cGlobalInfo.ListWellClasses[i];
                ToolStripMenuItem_ChangeClass.DropDownItems.Add(ToolStripMenuItem_NewClass);
            }

            #region Display As...
            ToolStripMenuItem ToolStripMenuItem_DisplayAS = new ToolStripMenuItem("Display As...");

            ToolStripMenuItem ToolStripMenuItem_Display3DScatter = new ToolStripMenuItem("3D Scatter Plot");
            ToolStripMenuItem_Display3DScatter.Click += new System.EventHandler(this.ToolStripMenuItem_Display3DScatter);
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_Display3DScatter);

            ToolStripMenuItem ToolStripMenuItem_Display3DDRC = new ToolStripMenuItem("3D DRC");
            ToolStripMenuItem_Display3DDRC.Click += new System.EventHandler(this.ToolStripMenuItem_Display3DDRC);
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_Display3DDRC);


            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayAS);
            #endregion

            #region Process
            ToolStripMenuItem ToolStripMenuItem_Process = new ToolStripMenuItem("Process");
            ToolStripMenuItem ToolStripMenuItem_GetDistanceMatrix = new ToolStripMenuItem("Get Distance Matrix (Euclidean)");
            ToolStripMenuItem_GetDistanceMatrix.Click += new System.EventHandler(this.ToolStripMenuItem_GetDistanceMatrix);
            ToolStripMenuItem_Process.DropDownItems.Add(ToolStripMenuItem_GetDistanceMatrix);

            ToolStripMenuItem_Process.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_ComputeDisplayDRC = new ToolStripMenuItem("DRC Analysis");
            ToolStripMenuItem_ComputeDisplayDRC.Click += new System.EventHandler(this.ToolStripMenuItem_ComputeDisplayDRC);
            ToolStripMenuItem_Process.DropDownItems.Add(ToolStripMenuItem_ComputeDisplayDRC);


            ToolStripMenuItem ToolStripMenuItem_MergeWellsTool = new ToolStripMenuItem("Merge Wells Tool");
            ToolStripMenuItem_MergeWellsTool.Click += new System.EventHandler(this.ToolStripMenuItem_MergeWellsTool);
            ToolStripMenuItem_Process.DropDownItems.Add(ToolStripMenuItem_MergeWellsTool);

            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Process);
            #endregion

            #region Single Cell
            ToolStripMenuItem ToolStripMenuItem_SingleCell = new ToolStripMenuItem("Single Cell");

            int NumSingleCellDesc = 0;
            List<cDescriptorType> ListSelectedDesc = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors();
            foreach (var item in ListSelectedDesc)
            {
                if (item.IsConnectedToDatabase) NumSingleCellDesc++;
            }


            if (NumSingleCellDesc > 1)
            {
                ToolStripMenuItem ToolStripMenuItem_GetSingleCellDataTable = new ToolStripMenuItem("Get Single Cell Data Table");
                ToolStripMenuItem_GetSingleCellDataTable.Click += new System.EventHandler(this.ToolStripMenuItem_GetSingleCellDataTable);
                ToolStripMenuItem_SingleCell.DropDownItems.Add(ToolStripMenuItem_GetSingleCellDataTable);

                ToolStripMenuItem ToolStripMenuItem_GetSingleCellTable = new ToolStripMenuItem("Display Single Cell 2D Scatter Plot");
                ToolStripMenuItem_GetSingleCellTable.Click += new System.EventHandler(this.ToolStripMenuItem_GetSingleCellTable);
                ToolStripMenuItem_SingleCell.DropDownItems.Add(ToolStripMenuItem_GetSingleCellTable);

                ToolStripMenuItem ToolStripMenuItem_GetSingleCell3DView = new ToolStripMenuItem("Display Single Cell 3D Scatter Plot");
                ToolStripMenuItem_GetSingleCell3DView.Click += new System.EventHandler(this.ToolStripMenuItem_GetSingleCell3DView);
                ToolStripMenuItem_SingleCell.DropDownItems.Add(ToolStripMenuItem_GetSingleCell3DView);
            }

            if (cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor().IsConnectedToDatabase)
            {
                ToolStripMenuItem ToolStripMenuItem_SingleCellHisto = new ToolStripMenuItem("Single Cell Histogram");
                ToolStripMenuItem_SingleCellHisto.Click += new System.EventHandler(this.ToolStripMenuItem_SingleCellHisto);
                ToolStripMenuItem_SingleCell.DropDownItems.Add(ToolStripMenuItem_SingleCellHisto);

                ToolStripMenuItem ToolStripMenuItem_SingleCellStackedHisto = new ToolStripMenuItem("Single Cell Stacked Histogram");
                ToolStripMenuItem_SingleCellStackedHisto.Click += new System.EventHandler(this.ToolStripMenuItem_SingleCellStackedHisto);
                ToolStripMenuItem_SingleCell.DropDownItems.Add(ToolStripMenuItem_SingleCellStackedHisto);
            }

            if (NumSingleCellDesc > 1)
            {
                ToolStripMenuItem_SingleCell.DropDownItems.Add(new ToolStripSeparator());

                ToolStripMenuItem ToolStripMenuItem_SingleCellAnalysis = new ToolStripMenuItem("Single Cell Analysis");
                ToolStripMenuItem_SingleCellAnalysis.Click += new System.EventHandler(this.ToolStripMenuItem_SingleCellAnalysis);
                ToolStripMenuItem_SingleCell.DropDownItems.Add(ToolStripMenuItem_SingleCellAnalysis);

                ToolStripMenuItem ToolStripMenuItem_ComputeDisplaySingleCellDRC = new ToolStripMenuItem("Single Cell DRC Analysis");
                ToolStripMenuItem_ComputeDisplaySingleCellDRC.Click += new System.EventHandler(this.ToolStripMenuItem_ComputeDisplaySingleCellDRC);
                ToolStripMenuItem_SingleCell.DropDownItems.Add(ToolStripMenuItem_ComputeDisplaySingleCellDRC);

                ToolStripMenuItem_SingleCell.DropDownItems.Add(new ToolStripSeparator());


                ToolStripMenuItem ToolStripMenuItem_ExportDBAsCSV = new ToolStripMenuItem("Export DB as CSV");
                ToolStripMenuItem_ExportDBAsCSV.Click += new System.EventHandler(this.ToolStripMenuItem_ExportDBAsCSV);
                ToolStripMenuItem_SingleCell.DropDownItems.Add(ToolStripMenuItem_ExportDBAsCSV);
            }

            if ((NumSingleCellDesc > 1) || ((cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor().IsConnectedToDatabase)))
            {
                SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_SingleCell);
            }

            #endregion

            ToolStripMenuItem ToolStripMenuItem_GetTable = new ToolStripMenuItem("Get Data Table (Average)");
            ToolStripMenuItem_GetTable.Click += new System.EventHandler(this.ToolStripMenuItem_GetTable);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_GetTable);

            return SpecificContextMenu;

        }

        private void ToolStripMenuItem_MergeWellsTool(object sender, EventArgs e)
        {
            this.MergeWells();
        }

        private void ToolStripMenuItem_NewClass(object sender, EventArgs e)
        {

            cGUI_ListSingleClasse ListClass = new cGUI_ListSingleClasse();
            ListClass.IsCheckBoxes = true;
            ListClass.IsSelectAll = true;
            ListClass.ClassType = eClassType.WELL;
            if (!ListClass.Run().IsSucceed) return;

            //CopyValuestoClipBoard();
            foreach (var item in this)
            {
                int Classe = 0;

                int ClassIdx = item.GetCurrentClassIdx();
                int ResultClasse = ClassIdx;
                //bool IsCurrentClassSelected;
                //if(ListClass.GetOutPut()[ClassIdx]==1);

                foreach (var Class in cGlobalInfo.ListWellClasses)
                {

                    //if (ClassIdx == -1)
                    //{
                    //    ResultClasse = Classe;
                    //    break;
                    //}
                    if ((Class.Name == sender.ToString()) && (ListClass.GetOutPut()[ClassIdx] == 1))
                    {
                        ResultClasse = Classe;
                        break;
                    }

                    Classe++;
                }

                item.SetClass(ResultClasse);
            }


            if ((this.Sender != null) && (this.Sender.GetType() == typeof(cChart2DScatterPoint)))
            {
                ((cChart2DScatterPoint)(this.Sender)).RefreshDisplay();
            }

        }
        #endregion

        public void MergeWells()
        {

            cWellMerger WM = new cWellMerger();
            WM.IsGUI = true;
            WM.SetInputData(this);
            WM.Run();
            List<cListWells> ListMergedWells = WM.GetOutPut();


            cExtendedTable ET = new cExtendedTable();
            ET.Name = "Wells Merging";
            ET.Add(new cExtendedList("Well Count"));
            ET.ListRowNames = new List<string>();
            ET.ListTags = new List<object>();

            // resest groups list
            //cGlobalInfo.CurrentScreening.ListGroups.Clear();
            //int IdxGroup = 0;

            //foreach (var item in ListMergedWells)
            //{
            //    foreach (cWell TmpWell in item)
            //        TmpWell.SetGroupIdx(IdxGroup);

            //    cGroup TmpGroup = new cGroup();
            //    // TmpGroup = (cListWells)item;
            //    //TmpGroup.Add(item);
            //    TmpGroup.ID = IdxGroup;
            //    cGlobalInfo.CurrentScreening.ListGroups.Add(TmpGroup);
            //    IdxGroup++;

            //    ET.ListRowNames.Add(item.Name);
            //    ET.ListTags.Add(item);
            //    ET[0].Add(item.Count);
            //}

            cViewerListWellsTreeView VLWTV = new cViewerListWellsTreeView();
            VLWTV.SetInputData(ListMergedWells);
            VLWTV.Run();

            cDisplayToWindow CDTW = new cDisplayToWindow();

            CDTW.SetInputData(VLWTV.GetOutPut());
            CDTW.Title = "Group Tree View (" + ListMergedWells.Count + " groups)";
            CDTW.Run();
            if (CDTW.Display() == false) return;
        }

        private void ToolStripMenuItem_ExportDBAsCSV(object sender, EventArgs e)
        {
            ExportDBAsCSV("");
        }

        /// <summary>
        /// export database as a CSV file
        /// </summary>
        /// <param name="FilePath">CSV file name. If "" then UI will be displayed</param>
        public void ExportDBAsCSV(string FilePath)
        {
            cDBToCSV DBToCSV = new cDBToCSV();
            DBToCSV.SetInputData(this);
            DBToCSV.FilePath = "";
            DBToCSV.Run();
        }

    }
}
