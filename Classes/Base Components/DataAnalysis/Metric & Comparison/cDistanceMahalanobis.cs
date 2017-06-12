using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using LibPlateAnalysis;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    

    class cDistanceMahalanobis : cDataAnalysisComponent
    {
     //   public eDistances DistanceType = eDistances.EUCLIDEAN;

        cExtendedTable Input;
        cExtendedTable OutPut;
        cExtendedTable CovarianceMatrix;


        public cDistanceMahalanobis(cExtendedTable CovarianceMatrix)
        {
            this.Title = "Mahalanobis Distance";
            this.CovarianceMatrix = CovarianceMatrix;
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

            this.OutPut.Name = "Mahalanobis";
            for (int IdxCol = 0; IdxCol < this.Input.Count; IdxCol++)
            {
                this.OutPut.ListRowNames.Add(this.Input[IdxCol].Name);
                cExtendedList NewResult = new cExtendedList();
                NewResult.Name = this.Input[IdxCol].Name;
                for (int IdxColBis = 0; IdxColBis < this.Input.Count; IdxColBis++)
                {
                    Value = this.Input[IdxCol].Dist_Euclidean(this.Input[IdxColBis]);
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
