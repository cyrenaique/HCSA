using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    public enum eCorrelationType { PEARSON, SPEARMAN };

    class cCorrelation : cMetric
    {
        eCorrelationType CorrelationType = eCorrelationType.PEARSON;

        public cCorrelation()
        {
            this.Title = "Correlation";
        }

        public void SetCorrelationType(eCorrelationType Type)
        {
            this.CorrelationType = Type;
        }

        public void Set_Data(cExtendedList Input1, cExtendedList Input2)
        {
            base.Input1 = Input1;
            base.Input2 = Input2;
        }

        private void Process()
        {
            base.Output = new cExtendedTable();
            double Value = double.NaN;
            base.Output.ListRowNames = new List<string>();
            base.Output.ListRowNames.Add(base.Input1.Name);

            if (this.CorrelationType == eCorrelationType.PEARSON)
            {
                base.Output.Name = "Pearson Correlation Coefficient";
                Value = alglib.pearsoncorr2(base.Input1.ToArray(), base.Input2.ToArray());
            }
            else if (this.CorrelationType == eCorrelationType.SPEARMAN)
            {
                base.Output.Name = "Spearman Correlation Coefficient";
                Value = alglib.spearmancorr2(base.Input1.ToArray(), base.Input2.ToArray());
            }

            cExtendedList Result = new cExtendedList();
            Result.Add(Value);
            base.Output.Add(Result);
            base.Output[0].Name = base.Input2.Name;

        }

        public cFeedBackMessage Run()
        {

            if ((base.Input1 == null) || (base.Input2 == null))
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
