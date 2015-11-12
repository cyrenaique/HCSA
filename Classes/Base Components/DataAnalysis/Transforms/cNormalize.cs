using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    class cNormalize : cComponent
    {
        cExtendedTable Input = null;
        cExtendedTable Output;

        //public bool IsColumnByColumn = true;
        public eNormalizationType NormalizationType = eNormalizationType.STANDARDIZE;

        public cNormalize()
        {
            this.Title = "Normalize";
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
                FeedBackMessage.Message = "No input data table defined.";
                return FeedBackMessage;
            }

            // ------------- now proceed ------------- 
            return Process();


            //FeedBackMessage = new cFeedBackMessage(true);
            //return FeedBackMessage;
        }

        public cExtendedTable GetOutPut()
        {
            return this.Output;
        }

     
        private cFeedBackMessage Process()
        {
            this.Output = new cExtendedTable(this.Input);

            //int IDxCOl = 0;
           // foreach (cExtendedList item in this.Output)
            for (int IDxCOl = 0; IDxCOl < this.Output.Count; IDxCOl++)
            {
                this.Output[IDxCOl] = Input[IDxCOl].Normalize(this.NormalizationType);
            }

            switch (this.NormalizationType)
            {
                case eNormalizationType.STANDARDIZE :
                    this.Output.Name = "Standardize(" + this.Input.Name + ")";        
                    break;
                case eNormalizationType.MIN_MAX:
                    this.Output.Name = "MinMax(" + this.Input.Name + ")";
                    break;
                case eNormalizationType.SHIFT_TO_MEAN:
                    this.Output.Name = "Shift To Mean(" + this.Input.Name + ")";
                    break;
                case eNormalizationType.SHIFT_TO_MEDIAN:
                    this.Output.Name = "Shift To Median(" + this.Input.Name + ")";
                    break;
                case eNormalizationType.SHIFT_TO_MIN:
                    this.Output.Name = "Shift To Min(" + this.Input.Name + ")";
                    break;
                default:
                    break;
            }

            return base.FeedBackMessage;
        }

    }
}
