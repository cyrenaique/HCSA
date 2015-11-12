using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{

    public class cBaseChart : Chart
    {
        public cExtendedTable Input;

        List<double[]> Histogram;


        public void Run()
        {
            ChartArea NewArea = new ChartArea();
          //  this.Series = new SeriesCollection();

            this.Histogram = Input[0].CreateHistogram(1, false);

            RedrawPie();
            
            
            base.ChartAreas.Add(NewArea);
        }

        void RedrawPie()
        {
            this.Series.Clear();

            Series NewSerie = new Series(Input.Name);
            NewSerie.ChartType = SeriesChartType.Pie;

            cHistogramBuilder HB = new cHistogramBuilder();
            HB.SetInputData(this.Input);
            HB.BinNumber = 1;
            HB.Run();

            cExtendedTable CurrentHistogram = HB.GetOutPut();

            for (int j = 0; j < CurrentHistogram[0].Count; j++)
            {
                //this.chartForPoints.Series[0].Points[j].MarkerColor = Color.FromArgb(128, GlobalInfo.ListCellularPhenotypes[(int)MachineLearning.Classes[j]].ColourForDisplay);
                //double[] Value = new double[1];
                //Value[0] = CurrentHistogram[1][j];
                // if (Value[0] == 0) continue;
                NewSerie.Points.Add(CurrentHistogram[1][j]);

                DataPoint DP = new DataPoint();

                // SeriesPos[i].Points.AddXY(MinValue + ((MaxValue - MinValue) * IdxValue) / Max, HistoPos[i][1][IdxValue]);

                //DP.SetValueXY(Step * j + GlobalMinX, Value[0]);

                //double[] Value = new double[1];
                //Value[0] = CurrentHistogram[1][j];
                // DP.YValues = Value;
                // if (Value[0] == 0) continue;
                // DP.XValue = Step * j + GlobalMinX;
                //DP.Tag = InputSimpleData[IdxSerie].Tag;
            }
            //cExtendedTable CurrentHistogram = HB.GetOutPut();



            this.Series.Add(NewSerie);

        }
            

    
    }



    class cViewerPie : cComponent
    {
        public cViewerPie()
        {
            this.Title = "Pie Viewer";
        }

        cBaseChart MyChart = new cBaseChart();

        public void SetInputData(cExtendedTable input)
        {
            MyChart.Input = input;

        }

        cExtendedControl CurrentPanel = new cExtendedControl();

        public cFeedBackMessage Run()
        {
           
            if (MyChart.Input == null)
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message = "No input data identified.";
                return base.FeedBackMessage;
            }


            this.CurrentPanel = new cExtendedControl();
            this.CurrentPanel.Title = this.Title;
            this.CurrentPanel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                        | System.Windows.Forms.AnchorStyles.Left
                                        | System.Windows.Forms.AnchorStyles.Right);


            MyChart.Run();

            MyChart.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                      | System.Windows.Forms.AnchorStyles.Left
                                      | System.Windows.Forms.AnchorStyles.Right);


            CurrentPanel.Controls.Add(MyChart);
            return base.FeedBackMessage;
        }

        public cExtendedControl GetOutPut()
        {
            return this.CurrentPanel;
        }




    }
}
