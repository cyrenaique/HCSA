using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Drawing;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.Data;

namespace HCSAnalyzer.Classes.Base_Classes.Data
{
    public enum eFunctionsType { CHISQUARE };

    class cFunctions : cDataAnalysis
    {
        #region Parameters
        public double DegreeOfFreedom = 1;
        public bool IsInverse = false;
        #endregion

        public eFunctionsType FunctionType = eFunctionsType.CHISQUARE;

        public cFunctions()
        {
            base.Title = "Functions";
        }

        public cFeedBackMessage Run()
        {
            FeedBackMessage = base.Run();
            if (!FeedBackMessage.IsSucceed) return FeedBackMessage;

            base.Output = new cExtendedTable();
            base.Output.Name = this.FunctionType.ToString();

            base.Output.Add(new cExtendedList(this.Input[0], 1));
            base.Output.Add(new cExtendedList(this.FunctionType.ToString()));

            for (int IdxValue = 0; IdxValue < this.Input[0].Count; IdxValue++)
            {
                if ((this.FunctionType == eFunctionsType.CHISQUARE) && (this.IsInverse))
                    base.Output[1].Add(alglib.invchisquaredistribution(this.DegreeOfFreedom, this.Input[0][IdxValue]));

                else if ((this.FunctionType == eFunctionsType.CHISQUARE) && (!this.IsInverse))
                    base.Output[1].Add(alglib.chisquaredistribution(this.DegreeOfFreedom, this.Input[0][IdxValue]));
            }
            /*else
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "Distribution Type not implemented";
            }
            */

            return FeedBackMessage;

        }


    }
}
