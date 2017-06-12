using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataProcessing
{
    class cStatistics : cArithmeticOperation
    {
        public cStatistics()
        {
            this.Title = "Statistics";
        }


        public void SetInputData(cExtendedTable MyData)
        {
            this.Input1 = MyData;
        }

        public cFeedBackMessage Run()
        {
            //cFeedBackMessage FeedBackMessage = new cFeedBackMessage(true);

            if ((this.Input1 == null) || (this.Input1.Count == 0))
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No Input defined.";
                return FeedBackMessage;
            }
            if (!IsMAD && !IsMax && !IsMean && !IsMedian && !IsMin && !IsStdDev && !IsSum && !IsCV)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "You have to select at least one statistic value to be computed";
                return FeedBackMessage;
            }
            Process();

           // this.Output[0].Name = "Statistics";
            return FeedBackMessage;
        }

        public bool IsSum = true;
        public bool IsMean = true;
        public bool IsCV = true;
        public bool IsMAD = true;
        public bool IsStdDev = true;
        public bool IsMedian = true;
        public bool IsMin = true;
        public bool IsMax = true;
        public bool IsSkewness = true;
        public bool IsKurtosis = true;
        public bool IsNumberOfData = true;


        public void UnselectAll()
        {
            IsSum = false;
            IsMean = false;
            IsCV = false;
            IsMAD = false;
            IsStdDev = false;
            IsMedian = false;
            IsMin = false;
            IsMax = false;
            IsSkewness = false;
            IsKurtosis = false;
            IsNumberOfData = false;
        }

        void Process()
        {
            this.Title = "Statistics";
            this.Output = new cExtendedTable();
            this.Output.Name = "Statistics (" + this.Input1.Name + ")";

            this.Output.ListRowNames = new List<string>();

            if (IsNumberOfData)
                this.Output.ListRowNames.Add("Number of Data");
            if (IsSum)
                this.Output.ListRowNames.Add("Sum");
            if (IsMin)
                this.Output.ListRowNames.Add("Min");
            if (IsMax)
                this.Output.ListRowNames.Add("Max");
            if (IsMean)
                this.Output.ListRowNames.Add("Mean");
            if (IsMedian)
                this.Output.ListRowNames.Add("Median");
            if (IsStdDev)
                this.Output.ListRowNames.Add("StdDev");
            if (IsMAD)
                this.Output.ListRowNames.Add("MAD");
            if (IsCV)
                this.Output.ListRowNames.Add("CV");
            if (IsSkewness)
                this.Output.ListRowNames.Add("Skewness");
            if (IsKurtosis)
                this.Output.ListRowNames.Add("Kurtosis");


            for (int Col = 0; Col < this.Input1.Count; Col++)
            {
                this.Output.Add(new cExtendedList());
                this.Output[Col].Name = this.Input1[Col].Name;
                if (this.Input1[Col].Tag != null)
                {
                    this.Output[Col].Tag = this.Input1[Col].Tag;
                }
                if (IsNumberOfData)
                    this.Output[Col].Add(this.Input1[Col].Count);
                if (IsSum)
                    this.Output[Col].Add(this.Input1[Col].Sum()); 
                if (IsMin)
                    this.Output[Col].Add(this.Input1[Col].Min());
                if (IsMax)
                    this.Output[Col].Add(this.Input1[Col].Max());
                if (IsMean)
                    this.Output[Col].Add(this.Input1[Col].Mean()); 
                if (IsMedian)
                    this.Output[Col].Add(this.Input1[Col].Median());
                if (IsStdDev)
                    this.Output[Col].Add(this.Input1[Col].Std());             
                if (IsMAD)
                    this.Output[Col].Add(this.Input1[Col].MAD(false));
                if (IsCV)
                    this.Output[Col].Add(this.Input1[Col].CV());
                if (IsSkewness)
                    this.Output[Col].Add(this.Input1[Col].Skewness());
                if (IsKurtosis)
                    this.Output[Col].Add(this.Input1[Col].Kurtosis());


            }
        }

    }
}
