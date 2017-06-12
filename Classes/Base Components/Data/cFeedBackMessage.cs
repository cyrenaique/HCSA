using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HCSAnalyzer.Classes.Base_Classes
{
    public class cFeedBackMessage
    {
        public bool IsSucceed;
        public string Message;
        public double ExecutionTime = 0;
        cComponent Source;

        public string GetFullFeedBack()
        {
            string toBeReturned = "Action";

            if (Source != null)
            {
                toBeReturned = Source.Title;
            }

            if (this.IsSucceed)
            {
                toBeReturned += " succeeded.\n";
                toBeReturned += "Exc. time: " + ExecutionTime + " ms.\n";
            }
            else
            {
                toBeReturned += " failed.\n";
            }
            return toBeReturned;

        }

        public cFeedBackMessage(bool IsSucceed, cComponent Source)
        {
            this.Source = Source;

            if (!IsSucceed)
            {
                this.IsSucceed = false;
                this.Message = "Failure";
            }
            else
            {
                this.IsSucceed = true;
                this.Message = "Success";
            }
        }

    }
}
