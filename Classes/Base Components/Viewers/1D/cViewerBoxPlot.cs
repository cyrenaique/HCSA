using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    class cViewerBoxPlot : cDataDisplay
    {     
        //cPanelHisto CurrentPanelHisto;
        //public eOrientation Orientation = eOrientation.HORIZONTAL;

        public cChart1DBoxPlot Chart = new cChart1DBoxPlot();


        public cViewerBoxPlot()
        {
            this.Title = "Box plot";
        }

        public void SetInputData(cExtendedTable input)
        {
            //CurrentPanelHisto = new cPanelHisto(ListValues, true, eGraphType.LINE, this.Orientation);
          // Chart = new cChart1DGraph();  
            Chart.InputSimpleData = input;
           
           
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
            Chart.Run();

            CurrentPanel.Controls.Add(Chart);
            return base.FeedBackMessage;
        }






    }
}
