using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    class cCovarianceMatrix : cDataAnalysisComponent
    {
        //eCorrelationType CorrelationType = eCorrelationType.PEARSON;
        cExtendedTable Input; 
        cExtendedTable OutPut;

        public cCovarianceMatrix()
        {
            this.Title = "Covariance";
        }

        public void  SetInputData(cExtendedTable Input)
        {
            this.Input = Input;
        }

        private void Process()
        {
            this.OutPut = new cExtendedTable();
            double Value = double.NaN;
            this.OutPut.ListRowNames = new List<string>();

                this.OutPut.Name = "Covariance Matrix";
                for (int IdxCol = 0; IdxCol < this.Input.Count; IdxCol++)
                {
                    this.OutPut.ListRowNames.Add(this.Input[IdxCol].Name);
                    cExtendedList NewResult = new cExtendedList();
                    NewResult.Name = this.Input[IdxCol].Name;
                    for (int IdxColBis = 0; IdxColBis < this.Input.Count; IdxColBis++)
                    {
                        Value = alglib.cov2(this.Input[IdxCol].ToArray(), this.Input[IdxColBis].ToArray());
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
