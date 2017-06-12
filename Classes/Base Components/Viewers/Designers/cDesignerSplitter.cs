using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Windows.Forms;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    class cDesignerSplitter : cDesignerParent
    {
        public Orientation Orientation = Orientation.Horizontal;
        List<cExtendedControl> xListControl = new List<cExtendedControl>();

        public cDesignerSplitter()
        {
            this.Title = "Column Splitter Designer";
        }

        public void SetInputData(cExtendedControl ControlToAdd)
        {
            this.xListControl.Add(ControlToAdd);
        }

        public cFeedBackMessage Run()
        {
           

            if (xListControl.Count == 0)
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message += ": No input defined!";
                return base.FeedBackMessage;
            }
            if (xListControl.Count == 1)
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message += ": At least 2 inputs have to be defined for this control!";
                return base.FeedBackMessage;
            }

            this.OutPut = new cExtendedControl();
            this.OutPut.Name = this.Title;
            this.OutPut.Title = this.Title;


            for (int i = 0; i < xListControl.Count; i++)
            {
                if ((xListControl[i].Controls.Count > 0) && (xListControl[i].Controls[0].GetType().ToString().Contains("DataGridView") == false) &&
                    (xListControl[i].Controls[0].Anchor == (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)))
                {
                    xListControl[i].Controls[0].Height = 0;
                    xListControl[i].Controls[0].Width = 0;
                }
            }

            cExtendedControl Tmp1 = this.CreateSplitter(xListControl[0], xListControl[1]);

            for(int IDx=2;IDx<this.xListControl.Count;IDx++)
                Tmp1 = this.CreateSplitter(Tmp1, xListControl[IDx]);

            this.OutPut.Controls.Add(Tmp1);

            this.OutPut.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                       | System.Windows.Forms.AnchorStyles.Left
                       | System.Windows.Forms.AnchorStyles.Right);

            return base.FeedBackMessage;
        }

        cExtendedControl CreateSplitter(cExtendedControl Ctrl1, cExtendedControl Ctrl2)
        {
            SplitContainer SC = new SplitContainer();

            SC.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                       | System.Windows.Forms.AnchorStyles.Left
                       | System.Windows.Forms.AnchorStyles.Right);
            SC.BorderStyle = BorderStyle.FixedSingle;
            SC.Orientation = this.Orientation;

            //SC.Panel1.Width = 
            Ctrl1.Width = SC.Panel1.Width;
            
            Ctrl1.Height = SC.Panel1.Height;
            SC.Panel1.Controls.Add(Ctrl1);

            Ctrl2.Width = SC.Panel2.Width;
            Ctrl2.Height = SC.Panel2.Height;
            SC.Panel2.Controls.Add(Ctrl2);

            cExtendedControl ToBeReturned = new cExtendedControl();

            ToBeReturned.Width = SC.Width;
            ToBeReturned.Height = SC.Height;
            ToBeReturned.Controls.Add(SC);
            ToBeReturned.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                       | System.Windows.Forms.AnchorStyles.Left
                       | System.Windows.Forms.AnchorStyles.Right);

            //ToBeReturned.Controls.Add(SC);
            return ToBeReturned;
        }
    }
}
