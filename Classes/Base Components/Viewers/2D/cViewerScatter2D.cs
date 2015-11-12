using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    public class cViewerScatter2D : cDataDisplay
    {
        public cViewerScatter2D()
        {
            Title = "New Viewer Scatter";   
        }

        cExtendedTable InputData = null;

        public void SetInputData(cExtendedTable Input)
        {
            this.InputData = Input;
        }

        public cFeedBackMessage Run()
        {
            return base.FeedBackMessage;
        }
    }
}
