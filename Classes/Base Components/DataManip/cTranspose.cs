using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    class cTranspose : cDataAnalysisComponent
    {
        cExtendedTable Input;
        cExtendedTable OutPut;
        public alglib.matinvreport Report;

        public cTranspose()
        {
            this.Title = "Transpose";
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

            //if (Info != 1)
            //{
            //    FeedBackMessage.IsSucceed = false;
            //    FeedBackMessage.Message = "Error while inversing the matrix";
            //    return FeedBackMessage;
            //}

            this.OutPut = new cExtendedTable(this.Input[0].Count, this.Input.Count, 0);

            if (Input[0].ListTags != null)
            {
                foreach (var item in this.OutPut)
                {
                    item.ListTags = new List<object>();
                }
            }


            this.OutPut.ListRowNames = new List<string>();

            for (int i = 0; i < this.Input.Count; i++)
            {
                this.OutPut.ListRowNames.Add(this.Input[i].Name);
                for (int j = 0; j < this.Input[i].Count; j++)
                {
                    this.OutPut[j][i] = this.Input[i][j];
                    if (this.Input[i].ListTags != null) this.OutPut[j].ListTags.Add(this.Input[i].ListTags[j]);
                }
            }

            if (this.Input.ListRowNames != null)
            {
                for (int i = 0; i < this.Input.ListRowNames.Count; i++)
                    this.OutPut[i].Name = this.Input.ListRowNames[i];
            }

            this.OutPut.Name = "Transpose(" + this.Input.Name + ")";

            return FeedBackMessage;
        }


    }
}
