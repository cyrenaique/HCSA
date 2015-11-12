using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{

    public enum eFTestTails { BOTH, LEFT, RIGHT };


    class cTwoSampleFTest : cDataAnalysisComponent
    {
        cExtendedTable Input;
        cExtendedTable OutPut;
        public eFTestTails FTestTails = eFTestTails.BOTH;

        public cTwoSampleFTest()
        {
            this.Title = "Two-Sample F-Test";
            base.Info = @"************************************************************************
Two-sample F-test

This test checks three hypotheses about dispersions of the given  samples.
The following tests are performed:
    * two-tailed test (null hypothesis - the dispersions are equal)
    * left-tailed test (null hypothesis  -  the  dispersion  of  the first
        sample is greater than or equal to  the  dispersion  of  the  second
        sample).
    * right-tailed test (null hypothesis - the  dispersion  of  the  first
        sample is less than or equal to the dispersion of the second sample)

The test is based on the following assumptions:
    * the given samples have normal distributions
    * the samples are independent.

Input parameters:
    X   -   sample 1. Array whose index goes from 0 to N-1.
    N   -   sample size.
    Y   -   sample 2. Array whose index goes from 0 to M-1.
    M   -   sample size.

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

    -- ALGLIB --
        Copyright 19.09.2006 by Bochkanov Sergey
*************************************************************************";
            
        }

        public void SetInputData(cExtendedTable Input)
        {
            this.Input = Input;
        }

        private void Process()
        {
            this.OutPut = new cExtendedTable();
            this.OutPut.ListRowNames = new List<string>();

            this.OutPut.Name = "Two-Sample F-Test";

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
                    Two-sample F-test

                    This test checks three hypotheses about dispersions of the given  samples.
                    The following tests are performed:
                        * two-tailed test (null hypothesis - the dispersions are equal)
                        * left-tailed test (null hypothesis  -  the  dispersion  of  the first
                          sample is greater than or equal to  the  dispersion  of  the  second
                          sample).
                        * right-tailed test (null hypothesis - the  dispersion  of  the  first
                          sample is less than or equal to the dispersion of the second sample)

                    The test is based on the following assumptions:
                        * the given samples have normal distributions
                        * the samples are independent.

                    Input parameters:
                        X   -   sample 1. Array whose index goes from 0 to N-1.
                        N   -   sample size.
                        Y   -   sample 2. Array whose index goes from 0 to M-1.
                        M   -   sample size.

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

                      -- ALGLIB --
                         Copyright 19.09.2006 by Bochkanov Sergey
                    *************************************************************************/
                    double bothtails;
                    double lefttail;
                    double righttail;

                    alglib.ftest(this.Input[IdxCol].ToArray(), this.Input[IdxCol].Count, this.Input[IdxColBis].ToArray(), this.Input[IdxColBis].Count, out bothtails, out lefttail, out righttail);
                    
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
