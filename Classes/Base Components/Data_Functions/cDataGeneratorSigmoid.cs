using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Drawing;

namespace HCSAnalyzer.Classes.Base_Classes.Data
{
 
    class cDataGeneratorSigmoid : cDataGenerator
    {
        #region Parameters
        public double Top = 10;
        public double Bottom = 0;
        public double EC50 = 5;
        public double Slope = 1;
        #endregion

        cExtendedTable OutPut = null;
        cExtendedList Input = null;


        public cDataGeneratorSigmoid()
        {
            this.Title = "Generated Sigmoid";
        }


        public cExtendedTable GetOutPut()
        {
            return this.OutPut;
        }

        public void SetInputData(cExtendedList Input)
        {
            this.Input = Input;
        }


        public cFeedBackMessage Run()
        {
           
            if(this.Input==null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data";
            }

            this.OutPut = new cExtendedTable(this.Input);
            this.OutPut[0].Name = "X";
            this.OutPut.Add(new cExtendedList());
            this.OutPut[1].Name = "Sigmoid(X)";

           
            foreach (var item in this.Input)
            {
                //double Value = Bottom +
                //    ( (Top - Bottom) / (1 + Math.Pow(10, (Math.Log10(EC50) - item) * Slope) ) );

                double Value = Bottom + (Top - Bottom) / (1 + Math.Pow((EC50 / item),Slope));

                this.OutPut[1].Add(Value);
                        /// (1 + Math.Pow((Math.Pow(10, EC50) / Math.Pow(10, /*ResultFit.GetLogConcentrations()*/item)), Slope)));
            }

            //for (int IdxConc = 0; IdxConc < this.Input[0].Count; IdxConc++)
            // {
            //    Y_Estimated.Add(c[0] + (c[1] - c[0]) / (1 + Math.Pow((Math.Pow(10, c[2]) / Math.Pow(10, /*ResultFit.GetLogConcentrations()*/this.Input[0][IdxConc])), c[3])));
            //    //   ResultFit.Y_Estimated.Add(c[0] + (c[1] - c[0]) / (1 + Math.Pow(((c[2]) /  ResultFit.GetLogConcentrations()[IdxConc]), c[3])));
            // }
           

            return FeedBackMessage;

        }


    }
}
