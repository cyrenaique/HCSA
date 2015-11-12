using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataProcessing
{
    class cArithmetic_Abs : cArithmeticOperation
    {
        public cArithmetic_Abs()
        {

        }


        public void SetInputData(cExtendedTable MyData)
        {
            this.Input1 = MyData;
        }

        public cFeedBackMessage Run()
        {
           // cFeedBackMessage FeedBackMessage = new cFeedBackMessage(true);

            if (this.Input1 == null)
            {
                base.FeedBackMessage.IsSucceed= false;
                base.FeedBackMessage.Message = "No Basis defined.";
                return base.FeedBackMessage;
            }

            Process();

            return base.FeedBackMessage;
        }

        void Process()
        {
            this.Title = "Abs.";
            this.Output = new cExtendedTable(this.Input1);
            this.Output.Name = "abs(" + this.Input1.Name + ")";
            
            for (int Col = 0; Col < this.Input1.Count; Col++)
            {


                for (int Row = 0; Row < this.Input1[0].Count; Row++)
                    this.Output[Col][Row] = Math.Abs(this.Input1[Col][Row]);
            }
        }

    }
}
