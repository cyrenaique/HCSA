using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    class cViewerGraph1D : cDataDisplay
    {     
        //cPanelHisto CurrentPanelHisto;
        //public eOrientation Orientation = eOrientation.HORIZONTAL;

        public cChart1DGraph Chart = new cChart1DGraph();

        public cViewerGraph1D()
        {
            this.Title = "Graph Viewer";
        }

        public void SetInputData(cExtendedTable input)
        {
            //CurrentPanelHisto = new cPanelHisto(ListValues, true, eGraphType.LINE, this.Orientation);
          // Chart = new cChart1DGraph();  
            Chart.InputSimpleData = input;
        }

        public void SetInputData(cListExtendedTable ListInput)
        {
            //CurrentPanelHisto = new cPanelHisto(ListValues, true, eGraphType.LINE, this.Orientation);
          // Chart = new cChart1DGraph();  
            Chart.ListInput = ListInput;
        }

        public void AddCurve(cCurveForGraph NewCurve)
        {
            Chart.ListCurves.Add(NewCurve);
        }

        public cFeedBackMessage Run()
        {

            if ((Chart.InputSimpleData!=null) && (this.Chart.X_AxisValues == null) && (this.Chart.IsLogAxis))
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message = "non negative X-axis values have to be specificied for logarithmic charts";
                return base.FeedBackMessage;
            }
            if ((Chart.InputSimpleData == null) && (Chart.ListCurves.Count==0) &&(Chart.ListInput == null))
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message = "No input data defined.";
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
