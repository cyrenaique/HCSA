using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using Accord.Statistics.Testing;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{


    class cANOVA : cDataAnalysisComponent
    {
        cExtendedTable Input;
        cExtendedTable OutPut;
        
        /// <summary>
        /// Eta^2 (Sum_Of_Squares Between groups / Sum_Of_Squares Total)
        /// measure of the strength of a curvilinear relationship between the independent and dependent variable is given by a quantity known as as eta-square
        /// </summary>
        public double Eta_2 = -1;
        public double SignificanceThreshold = 0.05;

        public cANOVA()
        {
            this.Title = "ANOVA";
            base.Info = @"One-way analysis of variance 
From Wikipedia, the free encyclopedia
  (Redirected from One-way ANOVA)

In statistics, one-way analysis of variance (abbreviated one-way ANOVA) is a technique used to compare means of two or more samples (using the F distribution). This technique can be used only for numerical data.
The ANOVA tests the null hypothesis that samples in two or more groups are drawn from populations with the same mean values. 
To do this, two estimates are made of the population variance. These estimates rely on various assumptions. The ANOVA produces an F-statistic, the ratio of the variance calculated among the means to the variance within the samples. If the group means are drawn from populations with the same mean values, the variance between the group means should be lower than the variance of the samples, following the central limit theorem. A higher ratio therefore implies that the samples were drawn from populations with different mean values.
Typically, however, the one-way ANOVA is used to test for differences among at least three groups, since the two-group case can be covered by a t-test (Gosset, 1908). When there are only two means to compare, the t-test and the F-test are equivalent.
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
            if (this.Input.Count < 2)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "Wrong data format";
                return FeedBackMessage;
            }

            foreach (var item in this.Input)
            {
                if (item.Count <= 1)
                {
                    FeedBackMessage.IsSucceed = false;
                    FeedBackMessage.Message = "Wrong data format";
                    return FeedBackMessage;
                }
            }


            int SampleSize = this.Input[0].Count;
            foreach (var item in this.Input)
            {
                if (item.Count != SampleSize)
                {
                    FeedBackMessage.IsSucceed = false;
                    FeedBackMessage.Message = "Unequal sample size";
                    return FeedBackMessage;
                }
            }


            this.OutPut = new cExtendedTable();
            this.OutPut.Name = "ANOVA";
            this.OutPut.ListRowNames = new List<string>();

            double[][] Table = this.Input.CopyToArray2();
            OneWayAnova MyOneWayAnova = new OneWayAnova(Table);
            


            cExtendedList NewList = new cExtendedList(MyOneWayAnova.Table[0].Source); // between-groups

            if (MyOneWayAnova.Table[0].Significance == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = MyOneWayAnova.Table[0].Source + " is null !";
                return FeedBackMessage;
            }

            this.OutPut.ListRowNames.Add("P-Value");
            this.OutPut.ListRowNames.Add("Significant?");
           // this.OutPut.ListRowNames.Add("F-Statistic");

            this.OutPut.Add(NewList);
            this.OutPut[this.OutPut.Count - 1].Add(MyOneWayAnova.Table[0].Significance.PValue);

            if (/*MyOneWayAnova.Table[0].Significance.Significant*/ MyOneWayAnova.Table[0].Significance.PValue < this.SignificanceThreshold)
                this.OutPut[this.OutPut.Count - 1].Add(1);
            else
                this.OutPut[this.OutPut.Count - 1].Add(0);


            
            this.Eta_2 = MyOneWayAnova.Table[0].SumOfSquares / MyOneWayAnova.Table[2].SumOfSquares;


            return FeedBackMessage;
        }


    }
}
