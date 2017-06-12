using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using ImageAnalysis;

namespace HCSAnalyzer.Classes.Base_Classes.DataManip
{
    class cConvertImageToDataTable : cComponent
    {

        cImage Input;
        cListExtendedTable OutPut;

        public cConvertImageToDataTable()
        {
            this.Title = "Convert Image To DataTable";
        }

        public void SetInputData(cImage Input)
        {
            this.Input = Input;
        }


        public cListExtendedTable GetOutPut()
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

            this.OutPut = new cListExtendedTable(this.Input);

            return FeedBackMessage;
        }
    }
}
