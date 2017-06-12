using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using ImageAnalysis;

namespace HCSAnalyzer.Classes.Base_Classes.DataManip
{
    class cConvertToImage : cComponent
    {

        cListExtendedTable Input;
        cImage OutPut;

        public cConvertToImage()
        {
            this.Title = "Convert To Image";
        }

        public void SetInputData(cListExtendedTable Input)
        {
            this.Input = Input;
        }

        public void SetInputData(cExtendedTable Input)
        {
            this.Input = new cListExtendedTable(Input);
           
        }

        public cImage GetOutPut()
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

            this.OutPut = new cImage(this.Input);
            this.OutPut.Name = "Image(" + this.Input.Name + ")";
            return FeedBackMessage;
        }
    }
}
