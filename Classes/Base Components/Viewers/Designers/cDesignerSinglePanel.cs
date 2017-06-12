using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Windows.Forms;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    class cDesignerSinglePanel : cDesignerParent
    {
        public cDesignerSinglePanel()
        {
            this.Title = "Simple Display Designer";
        }

        public void SetInputData(cExtendedControl Input)
        {
            base.OutPut = Input;


            base.OutPut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        }

        public cFeedBackMessage Run()
        {
            if (base.OutPut == null)
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message += ": No input defined!";
                return base.FeedBackMessage;
            }
            this.Title = base.OutPut.Title;


         
            return base.FeedBackMessage;
        }

    }
}
