using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Drawing;

namespace HCSAnalyzer.Classes.Base_Classes.Data
{

    class cDataGeneratorTitration : cDataGenerator
    {
        #region Parameters
        public double Start = 10;
        public double DilutionFactor = 2;
        public int NumberOfPoint = 12;
        #endregion

        cExtendedTable OutPut = null;
        cExtendedList Input = null;


        public cDataGeneratorTitration()
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


            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data";
            }

            this.OutPut = new cExtendedTable();
            this.OutPut.Add(new cExtendedList());

            for (int i = 0; i < NumberOfPoint; i++)
            {
                if (i == 0)
                    this.OutPut[0].Add(Start);
                else
                    this.OutPut[0].Add(this.OutPut[0][this.OutPut[0].Count - 1] / this.DilutionFactor);
            }

            double End = this.OutPut[0][this.OutPut[0].Count - 1];
            this.OutPut[0].Name = "Titration[" + Start + "..." + End + ":" + DilutionFactor + "]";
            return FeedBackMessage;

        }


    }
}
