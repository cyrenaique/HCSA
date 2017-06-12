using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;

namespace HCSAnalyzer.Classes.Base_Classes.DataProcessing
{
    class cNormalityJarqueBera : cDataAnalysis
    {

        public cNormalityJarqueBera()
        {
            base.Title = "Jarque-Bera Test";


            base.Info = base.Info = @"Jarque-Bera test

This test checks hypotheses about the fact that a  given  sample  X  is  a
sample of normal random variable.

Requirements:
    * the number of elements in the sample is not less than 5.

Accuracy of the approximation used (5<=N<=1951):

p-value  	    relative error (5<=N<=1951)
[1, 0.1]            < 1%
[0.1, 0.01]         < 2%
[0.01, 0.001]       < 6%
[0.001, 0]          wasn't measured

For N>1951 accuracy wasn't measured but it shouldn't be sharply  different from table values.";


        }

        public cFeedBackMessage Run()
        {
            FeedBackMessage = base.Run();
            if (!FeedBackMessage.IsSucceed) return FeedBackMessage;

            /*************************************************************************
            Jarque-Bera test

            This test checks hypotheses about the fact that a  given  sample  X  is  a
            sample of normal random variable.

            Requirements:
                * the number of elements in the sample is not less than 5.

            Input parameters:
                X   -   sample. Array whose index goes from 0 to N-1.
                N   -   size of the sample. N>=5

            Output parameters:
                BothTails   -   p-value for two-tailed test.
                                If BothTails is less than the given significance level
                                the null hypothesis is rejected.
                LeftTail    -   p-value for left-tailed test.
                                If LeftTail is less than the given significance level,
                                the null hypothesis is rejected.
                RightTail   -   p-value for right-tailed test.
                                If RightTail is less than the given significance level
                                the null hypothesis is rejected.

            Accuracy of the approximation used (5<=N<=1951):

            p-value  	    relative error (5<=N<=1951)
            [1, 0.1]            < 1%
            [0.1, 0.01]         < 2%
            [0.01, 0.001]       < 6%
            [0.001, 0]          wasn't measured

            For N>1951 accuracy wasn't measured but it shouldn't be sharply  different
            from table values.

              -- ALGLIB --
                 Copyright 09.04.2007 by Bochkanov Sergey
            *************************************************************************/
            if (base.IsColumnByColum)
            {
                if (Input[0].Count < 5)
                {
                    FeedBackMessage.IsSucceed = false;
                    FeedBackMessage.Message = "This process requires at least 5 values";
                    return FeedBackMessage;
                }

                this.Output = new cExtendedTable();

                foreach (var item in this.Input)
                {
                    double p;
                    alglib.jarqueberatest(item.ToArray(), item.Count, out p);

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

                double p;
                alglib.jarqueberatest(L.GetOutPut()[0].ToArray(), L.GetOutPut()[0].Count, out p);

                cExtendedList NewRes = new cExtendedList(base.Input.Name);
                NewRes.Add(p);

                this.Output.Add(NewRes);
            }

            this.Output.ListRowNames = new List<string>();
            this.Output.ListRowNames.Add("p-value");


            this.Output.Name = "Jarque-Bera Test(" + this.Input + ")";
            return FeedBackMessage;
        }
    }
}
