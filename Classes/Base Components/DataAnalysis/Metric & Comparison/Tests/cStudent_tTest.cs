using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
//using RDotNet;
using Accord.Statistics.Testing;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{

    class cStudent_tTest : cDataAnalysisComponent
    {
        cExtendedTable Input;
        cExtendedTable OutPut;

        public bool IsUnequalVariance = true;
        public eFTestTails FTestTails = eFTestTails.BOTH;

        //public eFTestTails FTestTails = eFTestTails.BOTH;

        public cStudent_tTest()
        {
           
            this.Title = "Student's t-Test";
            base.Info = @"
Student's t-tests

One of the most frequent statistical problems is testing hypotheses about the mean of the samples considered.

Subroutine StudentTTest2 returns three p-values:

p-value for two-tailed test (null hypothesis - means are equal)
p-value for left-tailed test (null hypothesis - mean of the first sample is greater than or equal to the mean of the second sample)
p-value for right-tailed test (null hypothesis - mean of the first sample is less than or equal to the mean of the second sample)
Two-sample unpooled test

This test checks hypotheses about the fact that the means of two random variables X and Y which are represented by samples xS  and yS  are equal. The test works correctly under the following conditions:

both random variables have a normal distribution
samples are independent.
Dispersion equality is not required.

During its work, the test calculates the t-statistic:

If X and Y have a normal distribution, the t-statistic will have Student's distribution with DF degrees of freedom:

This allows the use of the Student's distribution to define the significance level which corresponds to the value of the t-statistic.

Note #3
If X or Y is not normal, t will have an unknown distribution and, strictly speaking, the t-test is inapplicable. However, according to the central limit theorem, as the sample sizes increase, the distribution of t tends to be normal. Therefore, if sample sizes are big enough, we can use the t-test even if X or Y is not normal. But there is no way to find what values for NX  and NY  are big enough. These values depend on how X and Y deviate from the normal distribution. Some sources claim that NX  +NY  should be greater than 40, but sometimes even these sizes are not enough. If you are not confident that the distributions are normal, it's better to use non-parametric test: Mann-Whitney U-test.

Subroutine UnequalVarianceTTest returns three p-values:

p-value for two-tailed test (null hypothesis - means are equal)
p-value for left-tailed test (null hypothesis - mean of the first sample is greater than or equal to the mean of the second sample)
p-value for right-tailed test (null hypothesis - mean of the first sample is less than or equal to the mean of the second sample).";
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

            this.OutPut.Name = "Student's t-Test";

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

                    if(this.IsUnequalVariance)
                        alglib.unequalvariancettest(this.Input[IdxCol].ToArray(), this.Input[IdxCol].Count, this.Input[IdxColBis].ToArray(), this.Input[IdxColBis].Count, out bothtails, out lefttail, out righttail);
                    else
                        alglib.studentttest2(this.Input[IdxCol].ToArray(), this.Input[IdxCol].Count, this.Input[IdxColBis].ToArray(), this.Input[IdxColBis].Count, out bothtails, out lefttail, out righttail);
                   
                    //alglib.mannwhitneyutest(

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
