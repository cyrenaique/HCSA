using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    class cMerge : cDataAnalysisComponent
    {
        cExtendedTable Input1;
        cExtendedTable Input2;

        public bool IsHorizontal = true;

        cExtendedTable OutPut;

        public cMerge()
        {
            this.Title = "Merge";
        }

        public void SetInputData(cExtendedTable Input1, cExtendedTable Input2)
        {
            this.Input1 = Input1;
            this.Input2 = Input2;
        }

        public cExtendedTable GetOutPut()
        {
            return this.OutPut;
        }

        public cFeedBackMessage Run()
        {

            if ((this.Input1 == null) || (this.Input2 == null) || (this.Input1.Count == 0) || (this.Input2.Count == 0))
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }

            if (IsHorizontal)
            {
                //if (this.Input1[0].Count != this.Input2[0].Count)
                //{
                //    FeedBackMessage.IsSucceed = false;
                //    FeedBackMessage.Message = "Table dimensions are inconsistent";
                //    return FeedBackMessage;
                //}

                OutPut = new cExtendedTable(this.Input1);
                this.OutPut.ListRowNames = null;

                for (int i = 0; i < this.Input2.Count; i++)
                {
                    this.OutPut.Add(this.Input2[i]);
                    this.OutPut[this.OutPut.Count - 1].ListTags = this.Input2[i].ListTags;
                }

                this.OutPut.Name = "Hori. Merge(" + this.Input1.Name + "," + this.Input2.Name + ")";

              
                return FeedBackMessage;
            }
            else
            {
                if (this.Input1.Count != this.Input2.Count)
                {
                    FeedBackMessage.IsSucceed = false;
                    FeedBackMessage.Message = "Table dimensions are inconsistent";
                    return FeedBackMessage;
                }

                OutPut = new cExtendedTable(this.Input1);
                //this.OutPut.ListRowNames = null;

                for (int i = 0; i < this.Input2[0].Count; i++)
                {
                    for (int j = 0; j < this.Input2.Count; j++)
                    {
                        this.OutPut[j].Add(this.Input2[j][i]);

                        if ((this.OutPut[j].ListTags != null) && (this.Input2[j].ListTags.Count > i))
                            this.OutPut[j].ListTags.Add(this.Input2[j].ListTags[i]);
                    }
                }

                if ((this.OutPut.ListRowNames != null) && (this.Input2.ListRowNames != null))
                    foreach (var item in this.Input2.ListRowNames)
                        this.OutPut.ListRowNames.Add(item);

                if ((this.OutPut.ListTags != null) && (this.Input2.ListTags != null))
                    foreach (var item in this.Input2.ListTags)
                        this.OutPut.ListTags.Add(item);
                
                this.OutPut.Name = "Vert. Merge(" + this.Input1.Name + "," + this.Input2.Name + ")";

                return FeedBackMessage;
            }
        }


    }
}
