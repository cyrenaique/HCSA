using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataProcessing
{
    class cArithmetic_Log : cArithmeticOperation
    {

        public double Base = 10;

        public cArithmetic_Log()
        {
            this.Title = base.Title + ":Log()";
        }

        public void SetInputData(cExtendedTable MyData)
        {
            this.Input1 = MyData;
        }

        public cFeedBackMessage Run()
        {
          
            if (this.Input1 == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No Basis defined.";
                return FeedBackMessage;
            }

            Process();

            return FeedBackMessage;
        }

        void Process()
        {
            this.Title = "Log_"+ this.Base +"";
            this.Output = new cExtendedTable(this.Input1);
            this.Output.Name = "Log_"+this.Base+"(" + this.Output.Name + ")";

            for (int Col = 0; Col < this.Input1.Count; Col++)
                for (int Row = 0; Row < this.Input1[0].Count; Row++)
                    this.Output[Col][Row] = Math.Log(this.Input1[Col][Row], this.Base);
        }

    }
}
