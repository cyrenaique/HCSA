using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    class cZFactor : cDataAnalysisComponent
    {
        cExtendedTable Input;
        cExtendedTable OutPut;
        public bool IsRobust = false;

        public cZFactor()
        {
            this.Title = "Z-Factor";
            FeedBackMessage.Message = "\n" + this.Title + ": "; 
        }

        public void SetInputData(cExtendedTable Input)
        {
            this.Input = Input;
        }

        private void Process()
        {
            this.OutPut = new cExtendedTable();
            double Value = double.NaN;
            this.OutPut.ListRowNames = new List<string>();

            this.OutPut.Name = "Z-Factor(s)";

           

            for (int IdxCol = 0; IdxCol < this.Input.Count; IdxCol++)
            {
                this.OutPut.ListRowNames.Add(this.Input[IdxCol].Name);
                cExtendedList NewResult = new cExtendedList();
                NewResult.Name = this.Input[IdxCol].Name;
                for (int IdxColBis = 0; IdxColBis < this.Input.Count; IdxColBis++)
                {
                    if (IdxCol != IdxColBis)
                        Value = this.Input[IdxCol].ZFactor(this.Input[IdxColBis], this.IsRobust);
                    else
                        Value = double.NaN;

                    //if (double.IsInfinity(Value)) Value = 1;

                    NewResult.Add(Value);
                }
                this.OutPut.Add(NewResult);
            }

            base.Info = "Z-Factor (";
            if (IsRobust) base.Info +=  "Robust) - ";
            else
                base.Info += "Regular) - ";

            base.Info += this.Input[0].Count + " vs. " + this.Input[1].Count + " values.";
        }

        public cExtendedTable GetOutPut()
        {
            return this.OutPut;
        }

        public cFeedBackMessage Run()
        {


            if ((this.Input == null)||(this.Input.Count==0))
            {
                FeedBackMessage.IsSucceed = false;

                FeedBackMessage.Message += "No input data defined.";
                return FeedBackMessage;
            }
            // ------------- now proceed ------------- 

            Process();

            FeedBackMessage.IsSucceed = true;
            FeedBackMessage.Message += "Succeed"; 
            return FeedBackMessage;
        }


    }
}
