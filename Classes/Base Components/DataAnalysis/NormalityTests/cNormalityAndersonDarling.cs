using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;

namespace HCSAnalyzer.Classes.Base_Classes.DataProcessing
{
    class cNormalityAndersonDarling : cDataAnalysis
    {

        public cNormalityAndersonDarling()
        {
            base.Title = "Anderson-Darling Test";
            base.Info = base.Info = @"Anderson-Darling Test (Source: Wikipedia)

The Anderson–Darling test is a statistical test of whether a given sample of data is drawn from a given probability distribution. 
In its basic form, the test assumes that there are no parameters to be estimated in the distribution being tested, in which case the test and its set of critical values is distribution-free. 
However, the test is most often used in contexts where a family of distributions is being tested, in which case the parameters of that family need to be estimated and account must be taken of this in adjusting either the test-statistic or its critical values.
When applied to testing if a normal distribution adequately describes a set of data, it is one of the most powerful statistical tools for detecting most departures from normality.
K-sample Anderson–Darling tests are available for testing whether several collections of observations can be modelled as coming from a single population, where the distribution function does not have to be specified.";

        }


        public double Anderson_Darling(cExtendedList tab)
        {
            //double A = 0;
        //    double Mean1 = Mean(tab);
        //    double STD = std(tab);
        //    double[] norm = new double[tab.Length];

        //    for (int i = 0; i < tab.Length; i++)
        //        norm[i] = (tab[i] - Mean1) / STD;

            //tab.Normalize(eNormalizationType.STANDARDIZE);

            return Asquare(tab.Normalize(eNormalizationType.STANDARDIZE));
        }

        public double Asquare(cExtendedList data)
        {
            double A = 0;
            double Mean1 = data.Mean();
            double STD = data.Std();
            double varianceb = Math.Sqrt(2 * STD * STD);
            double err = 0;
            int cpt = 0;
            for (int i = 0; i < data.Count; i++)
            {
                cpt++;
                err += ((2 * cpt - 1) * (Math.Log(CDF(data[i], Mean1, varianceb)) + Math.Log(1 - CDF(data[data.Count - 1 - i], Mean1, varianceb))));
            }
            A = -data.Count - err / data.Count;

            return A;
        }

        public double CDF(double Y, double mu, double varb)
        {
            double Res = 0;
            Res = 0.5 * (1 + alglib.errorfunction((Y - mu) / (varb)));
            return Res;
        }



        public cFeedBackMessage Run()
        {
            FeedBackMessage = base.Run();
            if (!FeedBackMessage.IsSucceed) return FeedBackMessage;

            if (base.IsColumnByColum)
            {
                if (Input[0].Count < 3)
                {
                    FeedBackMessage.IsSucceed = false;
                    FeedBackMessage.Message = "This process requires at least 3 values";
                    return FeedBackMessage;
                }

                this.Output = new cExtendedTable();

                foreach (var item in this.Input)
                {
                    double p = Anderson_Darling(item);

                    cExtendedList NewRes = new cExtendedList(item.Name);
                    NewRes.Add(p);

                    this.Output.Add(NewRes);
                }
            }
            else
            {
                cLinearize L = new cLinearize();
                L.SetInputData(this.Input);
                FeedBackMessage = L.Run();
                if (!FeedBackMessage.IsSucceed) return FeedBackMessage;

                if (L.GetOutPut()[0].Count < 5)
                {
                    FeedBackMessage.IsSucceed = false;
                    FeedBackMessage.Message = "This process requires at least 5 values";
                    return FeedBackMessage;
                }

                this.Output = new cExtendedTable();

                double p = Anderson_Darling(L.GetOutPut()[0]);
                //alglib.jarqueberatest(L.GetOutPut()[0].ToArray(), L.GetOutPut()[0].Count, out p);

                cExtendedList NewRes = new cExtendedList(base.Input.Name);
                NewRes.Add(p);

                this.Output.Add(NewRes);
            }

            this.Output.ListRowNames = new List<string>();
            this.Output.ListRowNames.Add("Value");


            this.Output.Name = "Anderson-Darling Test(" + this.Input + ")";
            return FeedBackMessage;
        }
    }
}
