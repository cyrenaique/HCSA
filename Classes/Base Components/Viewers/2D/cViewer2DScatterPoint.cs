using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic;


namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    class cViewer2DScatterPoint : cDataDisplay
    {     
        public cChart2DScatterPoint Chart = new cChart2DScatterPoint();

        public cViewer2DScatterPoint()
        {
            this.Title = "2D scatter point viewer";
        }

        public void SetInputData(cExtendedTable input)
        {
            Chart.InputSimpleData = input;
        }

        public cFeedBackMessage Run()
        {
            
            if (Chart.InputSimpleData.Count <= 1)
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message = "At least two dimensions are required for this display.";
                return base.FeedBackMessage;
            }

            this.CurrentPanel = new cExtendedControl();
            this.CurrentPanel.Title = this.Title;
            this.CurrentPanel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                        | System.Windows.Forms.AnchorStyles.Left
                                        | System.Windows.Forms.AnchorStyles.Right);

            Chart.Run();

            CurrentPanel.Controls.Add(Chart);
            return base.FeedBackMessage;
        }
    }
}
