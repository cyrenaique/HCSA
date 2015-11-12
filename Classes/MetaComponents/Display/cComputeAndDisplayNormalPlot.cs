using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes.Viewers._1D;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
using LibPlateAnalysis;

namespace HCSAnalyzer.Classes.MetaComponents
{
    class cComputeAndDisplayNormalPlot : cComponent
    {
        cListWells InputWells = null;
        cExtendedTable InputTable = null;
        cExtendedControl Output;


        public cComputeAndDisplayNormalPlot()
        {
            this.Title = "Compute Display and display Normality Plots";
        }

        public void SetInputData(cListWells Input)
        {
            this.InputWells = Input;
        }

        public void SetInputData(cExtendedTable Input)
        {
            this.InputTable = Input;
        }

        public cFeedBackMessage Run()
        {
            if ((this.InputWells == null)&&(this.InputTable==null))
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }
            Process();
            return FeedBackMessage;
        }

        public cExtendedControl GetOutPut()
        {
            return this.Output;
        }

        void Process()
        {

            cExtendedTable NewTable1 = null;
            string DescName = "";

            if (this.InputWells != null)
            {
                NewTable1 = new cExtendedTable(this.InputWells, cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
                DescName = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
            }
            else if (this.InputTable != null)
            {
                NewTable1 = this.InputTable;
            }

            cDesignerTab NormalityTestTabs = new cDesignerTab();

            cNormalityAndersonDarling NAD = new cNormalityAndersonDarling();
            NAD.SetInputData(NewTable1);
            NAD.Run();

            cExtendedTable TNAD = NAD.GetOutPut();

            TNAD[0].Name = "Anderson-Darling Test";
            cViewerTable VTNAD = new cViewerTable();
            VTNAD.SetInputData(TNAD);
            VTNAD.Sender = NAD;
            VTNAD.IsDisplayInfo = true;
            VTNAD.DigitNumber = -1;
            VTNAD.Run();

            cExtendedControl AndersonCtrl = VTNAD.GetOutPut();
            AndersonCtrl.Title = TNAD[0].Name;

            cNormalityJarqueBera NJB = new cNormalityJarqueBera();
            NJB.SetInputData(NewTable1);
            NJB.Run();

            cExtendedTable TmpTest = NJB.GetOutPut();
            TmpTest[0].Name = "Jarque-Bera Test";
            cViewerTable VT = new cViewerTable();
            VT.SetInputData(TmpTest);
            VT.Sender = NJB;
            VT.IsDisplayInfo = true;
            VT.DigitNumber = -1;
            VT.Run();

            cExtendedControl JarqueCtrl = VT.GetOutPut();
            JarqueCtrl.Title = TmpTest[0].Name;

            NormalityTestTabs.SetInputData(JarqueCtrl);  
            NormalityTestTabs.SetInputData(AndersonCtrl);
            
            NormalityTestTabs.Run();


            //cExtendedControl ControlForTab = CADP.GetOutPut();
            //NormalityTestTabs.Title = TmpPlate.Name;


            cDesignerSplitter MainDS = new cDesignerSplitter();
            MainDS.Orientation = Orientation.Vertical;

            cDesignerSplitter DS = new cDesignerSplitter();
            DS.Orientation = Orientation.Horizontal;

            cNormalProbabilityPlot NPP = new cNormalProbabilityPlot();
            NPP.SetInputData(NewTable1);
            NPP.IdxColumnToProcess = 0;
            NPP.Run();

            cViewer2DScatterPoint V2DS = new cViewer2DScatterPoint();
            cExtendedTable TableToDisp = NPP.GetOutPut();
            TableToDisp.Name = "";
            V2DS.Chart.IsSelectable = true;
            V2DS.Chart.IsBorder = false;
            V2DS.Chart.IsShadow = false;
            V2DS.Chart.IsXGrid = true;
            V2DS.Chart.IsYGrid = true;
            V2DS.Chart.IsDisplayTrendLine = true;
            V2DS.SetInputData(TableToDisp);
            V2DS.Run();
            V2DS.Chart.CurrentTitle.Text = "Normal Probability Plot\n" + DescName + " - " + NewTable1[0].Count + " points";

            DS.SetInputData(V2DS.GetOutPut());

            cViewerStackedHistogram VSH = new cViewerStackedHistogram();

            cExtendedTable NewTable = null;

            if (this.InputWells != null)
            {
                NewTable = this.InputWells.GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
            }
            else
            {
                NewTable = this.InputTable;
            }
            NewTable.Name = DescName + " - Stacked Histogram - " + NewTable1[0].Count + " values";

            VSH.SetInputData(NewTable);
            VSH.Chart.LabelAxisX = DescName;
            VSH.Run();

            DS.SetInputData(VSH.GetOutPut());
            DS.Run();


            cExtendedControl TextEC = NormalityTestTabs.GetOutPut();
            TextEC.Width = 0;
            TextEC.Height = 0;

            TextEC.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                    | System.Windows.Forms.AnchorStyles.Left
                    | System.Windows.Forms.AnchorStyles.Right);


            MainDS.SetInputData(TextEC);
            MainDS.SetInputData(DS.GetOutPut());

            MainDS.Run();
            this.Output = MainDS.GetOutPut();

        }



    }
}

