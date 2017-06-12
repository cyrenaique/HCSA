using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{

    class cSignTest : cDataAnalysisComponent
    {
        cExtendedTable Input;
        cExtendedTable OutPut;

        public eFTestTails FTestTails = eFTestTails.BOTH;

        public cSignTest()
        {
            this.Title = "Sign test";
        }

        public void SetInputData(cExtendedTable Input)
        {
            this.Input = Input;
        }

        private void Process()
        {
            this.OutPut = new cExtendedTable();
            this.OutPut.ListRowNames = new List<string>();

            this.OutPut.Name = "Sign test";

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
                    /*************************************************************************
                     Sign test

                     This test checks three hypotheses about the median of  the  given  sample.
                     The following tests are performed:
                         * two-tailed test (null hypothesis - the median is equal to the  given
                           value)
                         * left-tailed test (null hypothesis - the median is  greater  than  or
                           equal to the given value)
                         * right-tailed test (null hypothesis - the  median  is  less  than  or
                           equal to the given value)

                     Requirements:
                         * the scale of measurement should be ordinal, interval or ratio  (i.e.
                           the test could not be applied to nominal variables).

                     The test is non-parametric and doesn't require distribution X to be normal

                     Input parameters:
                         X       -   sample. Array whose index goes from 0 to N-1.
                         N       -   size of the sample.
                         Median  -   assumed median value.

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

                     While   calculating   p-values   high-precision   binomial    distribution
                     approximation is used, so significance levels have about 15 exact digits.

                       -- ALGLIB --
                          Copyright 08.09.2006 by Bochkanov Sergey
                     *************************************************************************/

                    double bothtails;
                    double lefttail;
                    double righttail;

                    alglib.onesamplesigntest(this.Input[IdxCol].ToArray(), this.Input[IdxCol].Count, this.Input[IdxColBis].Median(), out bothtails, out lefttail, out righttail);
                    
                    if(FTestTails== eFTestTails.BOTH)
                        NewResult.Add(bothtails);
                    else if(FTestTails == eFTestTails.LEFT)
                        NewResult.Add(lefttail);
                    else if (FTestTails == eFTestTails.RIGHT)
                        NewResult.Add(righttail);


                }
                this.OutPut.Add(NewResult);
            }


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
            Process();
            return FeedBackMessage;
        }


    }
}
