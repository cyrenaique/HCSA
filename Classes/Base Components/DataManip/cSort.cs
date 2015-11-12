using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataProcessing
{
    class cSort : cComponent
    {

        class cTmp
        {
            public double Value;
            public int Index;

            public cTmp(double Value, int Idx)
            {
                this.Value = Value;
                this.Index = Idx;
            }
        }

        public cSort()
        {
            this.Title = "Sort";
        }

        cExtendedTable Input;
        cExtendedTable Output;

        public bool IsAscending = true;

        List<cTmp> RefList = new List<cTmp>();

        public int ColumnIndexForSorting = 0;

        public cExtendedTable GetOutPut()
        {
            return this.Output;
        }

        public void SetInputData(cExtendedTable Input)
        {
            this.Input = Input;
        }

        public cFeedBackMessage Run()
        {
            
            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input defined.";
                return FeedBackMessage;
            }

            if (this.ColumnIndexForSorting >= this.Input.Count)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "Sorting column index exceed table size.";
                return FeedBackMessage;
            }
            Process();
            
            this.Output = new cExtendedTable(this.Input);
            this.Output.Name = "Sorted "+this.Input.Name;

            int IdxCol = 0;
            foreach (var item in this.Input)
            {
                this.Output[IdxCol].Tag = item.Tag;
                IdxCol++;
            }

            for (IdxCol = 0; IdxCol < this.Input.Count; IdxCol++)
            {
                for (int IdxRow = 0; IdxRow < RefList.Count; IdxRow++)
                    this.Output[IdxCol][IdxRow] = this.Input[IdxCol][RefList[IdxRow].Index];

                if ((this.Input[IdxCol].ListTags != null) && (this.Input[IdxCol].ListTags.Count>0))
                {
                    for (int IdxRow = 0; IdxRow < RefList.Count; IdxRow++)
                        this.Output[IdxCol].ListTags[IdxRow] = this.Input[IdxCol].ListTags[RefList[IdxRow].Index];
                }
            }

            for (int IdxRow = 0; IdxRow < this.Input.ListRowNames.Count; IdxRow++)
                this.Output.ListRowNames[IdxRow] = this.Input.ListRowNames[RefList[IdxRow].Index];
            
            if (this.Input.ListTags != null)
            {
                for (int IdxRow = 0; IdxRow < this.Input.ListTags.Count; IdxRow++)
                    this.Output.ListTags[IdxRow] = this.Input.ListTags[RefList[IdxRow].Index];
            }

            return FeedBackMessage;
        }

        void Process()
        {
            this.Output = new cExtendedTable();
            this.Output.Name = "Sort(" + this.Input.Name + ")";

        
            for (int IDx = 0; IDx < this.Input[ColumnIndexForSorting].Count; IDx++)
            {
                RefList.Add(new cTmp(this.Input[ColumnIndexForSorting][IDx], IDx));
            }

            if(IsAscending)
                RefList.Sort(delegate(cTmp p1, cTmp p2) { return p1.Value.CompareTo(p2.Value); });
            else
                RefList.Sort(delegate(cTmp p1, cTmp p2) { return -p1.Value.CompareTo(p2.Value); });
  

          

            //ZFactorList.Sort(delegate(cSimpleSignature p1, cSimpleSignature p2) { return p1.AverageValue.CompareTo(p2.AverageValue); });
        
            
            //    this.Output.ListRowNames = new List<string>();

        //    if (IsSum)
        //        this.Output.ListRowNames.Add("Sum");
        //    if (IsMean)
        //        this.Output.ListRowNames.Add("Mean");
        //    if (IsStdDev)
        //        this.Output.ListRowNames.Add("StdDev");
        //    //if (IsMedian)
        //    //    this.Output.ListRowNames.Add("Median");
        //    //if (IsMAD)
        //    //   this.Output.ListRowNames.Add("MAD");
        //    if (IsMin)
        //        this.Output.ListRowNames.Add("Min");
        //    if (IsMax)
        //        this.Output.ListRowNames.Add("Max");



        //    for (int Col = 0; Col < this.Input1.Count; Col++)
        //    {
        //        this.Output.Add(new cExtendedList());
        //        this.Output[Col].Name = this.Input1[Col].Name;
        //        if (this.Input1[Col].Tag != null)
        //        {
        //            this.Output[Col].Tag = this.Input1[Col].Tag;
        //        }

        //        if (IsSum)
        //            this.Output[Col].Add(this.Input1[Col].Sum());
        //        if (IsMean)
        //            this.Output[Col].Add(this.Input1[Col].Mean());
        //        if (IsStdDev)
        //            this.Output[Col].Add(this.Input1[Col].Std());
        //        // if (IsMedian)
        //        //this.Output[Col].Add(this.Input1[Col].Med);
        //        //if (IsMAD)
        //        //this.Output[Col].Add(this.Input1[Col].Ma);
        //        if (IsMin)
        //            this.Output[Col].Add(this.Input1[Col].Min());
        //        if (IsMax)
        //            this.Output[Col].Add(this.Input1[Col].Max());


        //    }
        }

    }
}
