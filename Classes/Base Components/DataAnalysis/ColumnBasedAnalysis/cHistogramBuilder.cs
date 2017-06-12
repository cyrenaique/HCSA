using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataProcessing
{
    class cHistogramBuilder : cComponent
    {
        public cHistogramBuilder()
        {
            this.Title = "Histogram builder";
        }

        cExtendedTable Input;
        cExtendedTable Output;

        public double BinNumber = 100;
        public double BinSize = 1;
        public double Min = double.NaN;
        public double Max = double.NaN;
        public bool IsNormalized = false;
        public bool IsBinNumberMode = true;


        public void SetInputData(cExtendedTable InputTable)
        {
            this.Input = InputTable;
        }

        public cExtendedTable GetOutPut()
        {
            return this.Output;
        }


        public cFeedBackMessage Run()
        {
            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data.";
                return FeedBackMessage;
            }
            return Process();
        }

        cFeedBackMessage Process()
        {
            this.Output = new cExtendedTable();
            this.Output.Name = "Histogram (" + this.Input.Name + ")";

            List<double[]> Res = null;

            if (IsBinNumberMode)
            {
                if (double.IsNaN(this.Min) || double.IsNaN(this.Max))
                    Res = Input[0].CreateHistogram(this.BinNumber, this.IsNormalized);
                else
                    Res = Input[0].CreateHistogram(this.Min, this.Max, (int)this.BinNumber, this.IsNormalized);
            }
            else
            {
                //if (double.IsNaN(this.Min) || double.IsNaN(this.Max))
                //    Res = Input[0].CreateHistogram(this.BinNumber, this.IsNormalized);
                //else
                    Res = Input[0].CreateHistogram(this.Min, this.Max, this.BinSize);
            }

            if (Res.Count == 0)
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message = "Histogram failure";
                return base.FeedBackMessage;
            }



            string ColName = "Frequency";
            if (IsNormalized) ColName = "Normalized " + ColName;

            cExtendedList ListY = new cExtendedList(ColName);
            if (this.Input[0].Name == "") ColName = "Value";
            else
                ColName = this.Input[0].Name;
            cExtendedList ListX = new cExtendedList(ColName);

            ListX.AddRange(Res[0]);
            ListY.AddRange(Res[1]);

            this.Output.Add(ListX);
            this.Output.Add(ListY);

            return base.FeedBackMessage;
        }

    }
}
