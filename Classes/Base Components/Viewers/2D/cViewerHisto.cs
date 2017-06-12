using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;


namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    class cViewerHisto : cDataDisplay
    {
        public cViewerHisto()
        {
        
        }

        FormForDisplay TMPWin = new FormForDisplay();

        public void SetInputData(List<cExtendedList> ListValues)
        {
            cPanelHisto PanelHisto = new cPanelHisto(ListValues, true, eGraphType.HISTOGRAM, eOrientation.HORIZONTAL);

            //  cDisplayHisto CpdToDisplayHisto = new cDisplayHisto();
            TMPWin.Controls.Add(PanelHisto.WindowForPanelHisto.panelForGraphContainer);

            //TMPWin.panel.Controls.Add(CpdToDisplayHisto);
            
        
        }

        public void Display()
        {
            TMPWin.Show();
        }

    }
}
