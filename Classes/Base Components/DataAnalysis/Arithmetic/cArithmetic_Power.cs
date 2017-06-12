using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataProcessing
{
    class cArithmetic_Power : cArithmeticOperation
    {

        public double Power = 2;

        public cArithmetic_Power()
        {
            this.Title = base.Title + ":Power()";
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
            this.Title = "Power("+ this.Power +")";
            this.Output = new cExtendedTable(this.Input1);
            this.Output.Name = "(" + this.Output.Name + ")^" + this.Power;

            for (int Col = 0; Col < this.Input1.Count; Col++)
                for (int Row = 0; Row < this.Input1[0].Count; Row++)
                    this.Output[Col][Row] = Math.Pow(this.Input1[Col][Row], this.Power);
        }

    }
}
