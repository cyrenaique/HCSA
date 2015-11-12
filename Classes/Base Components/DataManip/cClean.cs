using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    class cClean : cDataAnalysisComponent
    {
        cExtendedTable Input;
        cExtendedTable OutPut;
        public int MinNumberOfData = 0;
        public int MaxNumberOfData = int.MaxValue;
        
        public cClean()
        {
            this.Title = "Clean";
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
            if(this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }

            OutPut = new cExtendedTable(this.Input);
            this.OutPut.ListRowNames = null;

            int NumberOfColumnsRemoved = 0;
            int RealIdx = 0;
            for (int i = 0; i < this.Input.Count; i++)
            {
                if ((this.Input[i].Count <= this.MinNumberOfData) || (this.Input[i].Count >= this.MaxNumberOfData))
                {
                    this.OutPut.RemoveAt(RealIdx);
                    NumberOfColumnsRemoved++;
                    RealIdx--;
                }
                RealIdx++;
            }

            this.OutPut.Name = "Clean(" + this.Input.Name + ")";

            FeedBackMessage.Message = NumberOfColumnsRemoved + " columns removed";
            return FeedBackMessage;
        }


    }
}
