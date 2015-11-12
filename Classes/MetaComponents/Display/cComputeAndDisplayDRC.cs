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

namespace HCSAnalyzer.Classes.MetaComponents
{
    class cComputeAndDisplayDRC : cComponent
    {
        cListExtendedTable Input;


        public cComputeAndDisplayDRC()
        {
            this.Title = "Compute Display and display DRCs";
            
        }

        public cFeedBackMessage Run()
        {
            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }
            Process();
            return FeedBackMessage;
        }

        void Process()
        {
            cDesignerTab DT = new cDesignerTab();
                        
            for (int i = 0; i < this.Input.Count; i++)
            {
                cExtendedTable FinalTable = this.Input[i];

                cCurveForGraph CFG = new cCurveForGraph();
                CFG.SetInputData(FinalTable);
                CFG.Run();

                //cLinearRegression LR = new cLinearRegression();
                //LR.SetInputData(FinalTable);
                //LR.Run();
                
                cSigmoidFitting SF = new cSigmoidFitting();
                SF.SetInputData(FinalTable);
                if (SF.Run().IsSucceed == false) continue;

                // double Ratio = LR.GetOutPut()[0][LR.GetOutPut().Count - 1] / SF.GetOutPut()[0][SF.GetOutPut().Count - 1];


                cExtendedTable Sigmoid = SF.GetFittedRawValues(CFG.GetListXValues());
                FinalTable[0] = Sigmoid[1];

                cDesignerSplitter DS = new cDesignerSplitter();

                //cViewerTableAsRichText VT = new cViewerTableAsRichText();
                cViewerTable VT = new cViewerTable();
                VT.SetInputData(SF.GetOutPut());
                VT.DigitNumber = -1;
                VT.Run();

                cViewerGraph1D VS1 = new cViewerGraph1D();
                //VS1.SetInputData(/*new cExtendedTable(AN.GetOutPut()[1])*/FinalTable);
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
                VS1.Chart.LabelAxisY = "Readout";
                VS1.Chart.XAxisFormatDigitNumber = -1;
                VS1.Chart.IsZoomableX = true;

                Classes.Base_Classes.General.cLineVerticalForGraph VLForEC50 = new Classes.Base_Classes.General.cLineVerticalForGraph(SF.GetOutPut()[0][2]);
                VLForEC50.AddText("EC50: " + SF.GetOutPut()[0][2].ToString("e3")/* + "\nError Ratio:" + Ratio.ToString("N4")*/);
                VS1.Chart.ListVerticalLines.Add(VLForEC50);


                VS1.Chart.ArraySeriesInfo = new cSerieInfoDesign[FinalTable.Count];

                for (int IdxCurve = 0; IdxCurve < FinalTable.Count; IdxCurve++)
                {
                    cSerieInfoDesign TmpSerieInfo = new cSerieInfoDesign();
                   // TmpSerieInfo.color = GlobalInfo.ListCellularPhenotypes[IdxCurve % GlobalInfo.ListCellularPhenotypes.Count].ColourForDisplay;
                    TmpSerieInfo.markerStyle = MarkerStyle.Circle;
                    VS1.Chart.ArraySeriesInfo[IdxCurve] = TmpSerieInfo;
                }

                VS1.Run();

                DS.SetInputData(VS1.GetOutPut());
                DS.SetInputData(VT.GetOutPut());
                DS.Orientation = Orientation.Horizontal;
                DS.Title = "Noise Stdev" + i * 10;
                DS.Run();
                DT.SetInputData(DS.GetOutPut());



            }
            DT.Run();

            cDisplayToWindow MyDisplay = new cDisplayToWindow();
            MyDisplay.SetInputData(DT.GetOutPut());
            MyDisplay.Title = this.Input.Name;
            MyDisplay.Run();
            MyDisplay.Display();
        }


        public void SetInputData(cListExtendedTable Input)
        {
            this.Input = Input;
        }
    }
}

