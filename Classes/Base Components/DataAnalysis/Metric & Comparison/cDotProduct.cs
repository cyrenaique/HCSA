using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    class cDotProduct : cDataAnalysisComponent
    {
        cExtendedTable Input;
        cExtendedTable OutPut;
        public bool IsNormalized = true;

        public cDotProduct()
        {
            this.Title = "Dot product";
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

                this.OutPut.Name = "dot product(s)";
                for (int IdxCol = 0; IdxCol < this.Input.Count; IdxCol++)
                {
                    this.OutPut.ListRowNames.Add(this.Input[IdxCol].Name);
                    cExtendedList NewResult = new cExtendedList();
                    NewResult.Name = this.Input[IdxCol].Name;
                    for (int IdxColBis = 0; IdxColBis < this.Input.Count; IdxColBis++)
                    {
                        //if (IdxCol != IdxColBis)
                        Value = this.Input[IdxCol].DotProduct(this.Input[IdxColBis], IsNormalized);

                        NewResult.Add(Value);
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
