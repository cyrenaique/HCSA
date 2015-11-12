using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    class cInverse : cDataAnalysisComponent
    {
        cExtendedTable Input;
        cExtendedTable OutPut;
        public alglib.matinvreport Report;

        public cInverse()
        {
            this.Title = "Inverse";
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
            if (this.Input.Count != this.Input[0].Count)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "A square matrix is required for this operation";
                return FeedBackMessage;
            }


            // ------------- now proceed ------------- 
            int Info;

            double[,] Mat = this.Input.CopyToArray();
            alglib.rmatrixinverse(ref Mat, out Info, out Report);

            if (Info != 1)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "Error while inversing the matrix";
                return FeedBackMessage;
            }

            this.OutPut = new cExtendedTable(Mat);
            

            if (this.Input.ListRowNames != null)
            {
                this.OutPut.ListRowNames = new List<string>();
                this.OutPut.ListRowNames.AddRange(this.Input.ListRowNames.ToArray());
            }

            int IdxCOl=0;
            foreach (var item in this.Input)
            {
                this.OutPut[IdxCOl++].Name = item.Name;
            }


            this.OutPut.Name = "Inverse(" + this.Input.Name + ")";

            return FeedBackMessage;
        }


    }
}
