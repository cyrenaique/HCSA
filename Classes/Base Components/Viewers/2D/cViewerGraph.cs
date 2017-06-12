using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    class cViewerGraph : cDataDisplay
    {     
        cPanelHisto CurrentPanelHisto;
        public eOrientation Orientation = eOrientation.HORIZONTAL;
   
        public cViewerGraph()
        {
            this.Title = "Graph Viewer";
        }

        public void SetInputData(cExtendedTable ListValues)
        {
            CurrentPanelHisto = new cPanelHisto(ListValues, true, eGraphType.LINE, this.Orientation);
        }

        public cFeedBackMessage Run()
        {
           

            this.CurrentPanel = new cExtendedControl();
            this.CurrentPanel.Title = this.Title;

            this.CurrentPanel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                        | System.Windows.Forms.AnchorStyles.Left
                                        | System.Windows.Forms.AnchorStyles.Right);

            //CurrentPanelHisto.WindowForPanelHisto.panelForGraphContainer.Width = CurrentPanel.Width-50;
            //CurrentPanelHisto.WindowForPanelHisto.panelForGraphContainer.Height = CurrentPanel.Height-5;

            CurrentPanel.Controls.Add(CurrentPanelHisto.WindowForPanelHisto.panelForGraphContainer);
            return base.FeedBackMessage;
        }

    }
}
