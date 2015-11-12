using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic;
using HCSAnalyzer.Classes.General_Types;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    public class cViewerStackedHistogram : cDataDisplay
    {     
        //cPanelHisto CurrentPanelHisto;
        //public eOrientation Orientation = eOrientation.HORIZONTAL;

        public cChart1DStackedHistogram Chart = new cChart1DStackedHistogram();


        public cViewerStackedHistogram()
        {
            this.Title = "Stacked Histogram Viewer";

            cPropertyType PT = new cPropertyType("Bin Number", eDataType.INTEGER);
            PT.Min = -1;
            cProperty Prop1 = new cProperty(PT, null);
            Prop1.Info = "Number of bins (-1 <=> automated)";
            Prop1.SetNewValue((int)100);
            base.ListProperties.Add(Prop1);


        }

        public void SetInputData(cExtendedTable input)
        {
            //CurrentPanelHisto = new cPanelHisto(ListValues, true, eGraphType.LINE, this.Orientation);
          // Chart = new cChart1DGraph();  
            Chart.InputSimpleData = input;
            Chart.LabelAxisY = "Frequency";
            base.InputData = input;
        }

        public cFeedBackMessage Run()
        {
            if (base.Start() == false)
            {
                base.FeedBackMessage.IsSucceed = false;
                return base.FeedBackMessage;
            }


            if (Chart.InputSimpleData == null)
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message = "No input data identified.";
                return base.FeedBackMessage;
            }


            object _firstValue = base.ListProperties.FindByName("Bin Number");
            int BinNumber = 100;
            if (_firstValue == null)
            {
                base.GenerateError("Bin Number not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                BinNumber = (int)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("Bin Number cast didn't work");
                return base.FeedBackMessage;
            }


            Chart.BinNumber = BinNumber;


            this.CurrentPanel = new cExtendedControl();
            this.CurrentPanel.Title = this.Title;
            this.CurrentPanel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                        | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);

            //CurrentPanelHisto.WindowForPanelHisto.panelForGraphContainer.Width = CurrentPanel.Width-50;
            //CurrentPanelHisto.WindowForPanelHisto.panelForGraphContainer.Height = CurrentPanel.Height-5;
            Chart.Run();

            CurrentPanel.Controls.Add(Chart);

            base.End();
            return base.FeedBackMessage;
        }






    }
}
