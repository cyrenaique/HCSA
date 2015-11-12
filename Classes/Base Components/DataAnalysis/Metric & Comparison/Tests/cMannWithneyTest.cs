using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
//using RDotNet;
using Accord.Statistics.Testing;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{

    class cMannWithneyTest : cDataAnalysisComponent
    {
        cExtendedTable Input;
        cExtendedTable OutPut;

        public eFTestTails FTestTails = eFTestTails.BOTH;

        //public eFTestTails FTestTails = eFTestTails.BOTH;

        public cMannWithneyTest()
        {

            this.Title = "Mann-Withney Test";
            base.Info = @"
Mann-Whitney U-test

The Mann-Whitney U-test is a non-parametric method which is used as an alternative to the two-sample Student's t-test. Usually this test is used to compare medians of non-normal distributions X and Y (the t-test is not applicable because X and Y are not normal). The test works correctly under the following conditions:

X and Y are continuous distributions (or discrete distributions well-approximating continuous distributions)
X and Y have the same shape. The only possible difference is their position (i.e. the value of the median)
the number of elements in each sample is not less than 5
the samples are independent
scale of measurement should be ordinal, interval or ratio (i.e. test could not be applied to nominal variables).
The MannWhitneyUTest subroutine returns three p-values:

p-value for two-tailed test (null hypothesis - the medians are equal)
p-value for left-tailed test (null hypothesis - the median of the first sample is greater than or equal to the median of the second sample)
p-value for right-tailed test (null hypothesis - the median of the first sample is less than or equal to the median of the second sample)
";
        }


        public void SetInputData(cExtendedTable Input)
        {
            this.Input = Input;
        }

        public cExtendedTable GetOutPut()
        {
            return this.OutPut;
        }

        public cFeedBackMessage Run()
        {

            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }

            // ------------- now proceed ------------- 
            this.OutPut = new cExtendedTable();
            this.OutPut.ListRowNames = new List<string>();

            this.OutPut.Name = "Mann-Withney Test";

            if (FTestTails == eFTestTails.BOTH)
                this.OutPut.Name += " (both tails)";
            else if (FTestTails == eFTestTails.LEFT)
                this.OutPut.Name += " (left tail)";
            else if (FTestTails == eFTestTails.RIGHT)
                this.OutPut.Name += " (right tail)";


            for (int IdxCol = 0; IdxCol < this.Input.Count; IdxCol++)
            {
                this.OutPut.ListRowNames.Add(this.Input[IdxCol].Name);
                cExtendedList NewResult = new cExtendedList();
                NewResult.Name = this.Input[IdxCol].Name;
                for (int IdxColBis = 0; IdxColBis < this.Input.Count; IdxColBis++)
                {
                    double bothtails;
                    double lefttail;
                    double righttail;

                    alglib.mannwhitneyutest(this.Input[IdxCol].ToArray(), this.Input[IdxCol].Count, this.Input[IdxColBis].ToArray(), this.Input[IdxColBis].Count, out bothtails, out lefttail, out righttail);

                    if (FTestTails == eFTestTails.BOTH)
                        NewResult.Add(bothtails);
                    else if (FTestTails == eFTestTails.LEFT)
                        NewResult.Add(lefttail);
                    else if (FTestTails == eFTestTails.RIGHT)
                        NewResult.Add(righttail);


                }
                this.OutPut.Add(NewResult);
            }

           return FeedBackMessage;
        }


    }
}
