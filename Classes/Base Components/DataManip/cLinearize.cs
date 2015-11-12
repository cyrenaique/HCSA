using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    class cLinearize : cDataAnalysisComponent
    {
        cExtendedTable Input;
        cExtendedTable OutPut;
     

        public cLinearize()
        {
            this.Title = "Linearize";
        }

        public void SetInputData(cExtendedTable Input)
        {
            this.Input = Input;
        }

        public cExtendedList GetOutPutAsExtendedList()
        {
            return this.OutPutList;
        }

        public cExtendedTable GetOutPut()
        {
            return this.OutPut;
        }
        
        cExtendedList OutPutList = null;
        
        public cFeedBackMessage Run()
        {
            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }


            OutPutList = new cExtendedList();
            OutPutList.ListTags = new List<object>(); 
            for (int i = 0; i < this.Input.Count; i++)
            {
                for (int j = 0; j < this.Input[0].Count; j++)
                {
                    OutPutList.Add(this.Input[i][j]);
                }
                if (this.Input[i].ListTags != null)
                {
                 
                    for (int j = 0; j < this.Input[i].Count; j++)
                        OutPutList.ListTags.Add(this.Input[i].ListTags[j]);
                }
            }

            this.OutPut = new cExtendedTable(OutPutList);
            this.OutPut.Name = "Linearize(" + this.Input.Name + ")";

            return FeedBackMessage;
        }


    }
}
