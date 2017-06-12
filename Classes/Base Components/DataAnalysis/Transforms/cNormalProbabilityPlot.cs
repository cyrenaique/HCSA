using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    class cNormalProbabilityPlot : cComponent
    {
        cExtendedTable Input = null;
        cExtendedTable Output;
        public int IdxColumnToProcess = 0;


        public cNormalProbabilityPlot()
        {
            this.Title = "Normal Probability Plot";
        }

        public void SetInputData(cExtendedTable Input)
        {
            this.Input = Input;
        }

        public cFeedBackMessage Run()
        {
            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data table defined.";
                return FeedBackMessage;
            }

            // ------------- now proceed ------------- 
            return Process();


            //FeedBackMessage = new cFeedBackMessage(true);
            //return FeedBackMessage;
        }

        public cExtendedTable GetOutPut()
        {
            return this.Output;
        }

     
        private cFeedBackMessage Process()
        {
         
            cExtendedTable TmpTable = new cExtendedTable(this.Input[this.IdxColumnToProcess]);

            cSort S = new cSort();
            S.SetInputData(TmpTable);
            base.FeedBackMessage = S.Run();
            if (base.FeedBackMessage.IsSucceed == false) return base.FeedBackMessage;

            cNormalize N = new cNormalize();
            N.NormalizationType = eNormalizationType.STANDARDIZE;
            N.SetInputData(S.GetOutPut());
            base.FeedBackMessage = N.Run();
            if (base.FeedBackMessage.IsSucceed == false) return base.FeedBackMessage;

            int Num = N.GetOutPut()[0].Count;
            double[] CumulativeProba = new double[Num];
            for (int i = 1; i < Num - 1; i++)
                CumulativeProba[i] = (i - 0.3175) / (Num + 0.365);

            CumulativeProba[Num - 1] = Math.Pow(0.5, 1.0 / Num);
            CumulativeProba[0] = 1 - CumulativeProba[Num - 1];

            double[] PercentPointFunction = new double[Num];

            for (int i = 0; i < Num; i++)
                PercentPointFunction[i] = alglib.normaldistr.invnormaldistribution(CumulativeProba[i]);

            this.Output = new cExtendedTable(new cExtendedList(PercentPointFunction));

            this.Output.Add(new cExtendedList());

            this.Output.ListTags = new List<object>();
            
            for (int i = 0; i < N.GetOutPut()[0].Count; i++)
            {
                this.Output[1].Add(N.GetOutPut()[0][i]);
                if(S.GetOutPut()[0].ListTags!=null)
                this.Output.ListTags.Add(S.GetOutPut()[0].ListTags[i]);
            }

            this.Output[0].Name = "";
            this.Output[1].Name = "Normalized Data";

            return base.FeedBackMessage;
        }

    }
}
