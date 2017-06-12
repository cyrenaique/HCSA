using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Data;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
using ImageAnalysis;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    class cAddNoise : cDataAnalysisComponent
    {
        cExtendedTable Input;
        cExtendedTable OutPut;

        public double Min = 0;
        public double Max = 100;
        
        public double Mean = 100;
        public double Stdv = 5;
        public eRandDistributionType DistributionType = eRandDistributionType.GAUSSIAN;


        public cAddNoise()
        {
            this.Title = "Add Noise";
        }

        public void SetInputData(cExtendedTable Input)
        {
            this.Input = Input;
        }


        public cExtendedTable GetOutPut()
        {
            return this.OutPut;
        }

        public cFeedBackMessage Run()
        {
         
            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }

            this.OutPut = new cExtendedTable(this.Input);
            cRandomGenerator RG = new cRandomGenerator(this.Input.Count, this.Input[0].Count);
            RG.Seed = (int)System.DateTime.Now.Ticks;
            RG.Min = this.Min;
            RG.Max = this.Max;
            RG.Mean = this.Mean;
            RG.Stdev = this.Stdv;
            RG.DistributionType = this.DistributionType;

            FeedBackMessage = RG.Run();
            if (FeedBackMessage.IsSucceed == false)
            {
                return FeedBackMessage;
            }

            cArithmetic_DualOperator DO = new cArithmetic_DualOperator();
            DO.SetInputData(this.Input, RG.GetOutPut());
            DO.OperationType = eBinaryOperationType.ADD;
            DO.Run();

            this.OutPut = DO.GetOutPut();
            this.OutPut.Name = "Add Noise(" + this.Input.Name + ")";

            return FeedBackMessage;
        }


    }
}
