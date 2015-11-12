using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes
{
    class cDataAnalysis : cComponent
    {
        protected cExtendedTable Input;
        protected cExtendedTable Output;
        public bool IsColumnByColum = true;
            
        public void SetInputData(cExtendedTable MyData)
        {
            this.Input = MyData;
        }

        public cExtendedTable GetOutPut()
        {
            return this.Output;
        }


        protected cFeedBackMessage Run()
        {
            if ((this.Input == null) || (this.Input.Count == 0))
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No Input defined.";
                return FeedBackMessage;
            }

            return FeedBackMessage;
        
        }



    }
}
