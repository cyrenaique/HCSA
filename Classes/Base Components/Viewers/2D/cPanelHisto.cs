﻿using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Collections.Generic;

namespace HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic
{
    public enum eOrientation { HORIZONTAL, VERTICAL };
    public enum eGraphType { HISTOGRAM, LINE };

    class cPanelHisto
    {
        public FormPanelForHisto WindowForPanelHisto;// = new FormPanelForHisto(List<cExtendedList> ListValues);

        public cPanelHisto(List<cExtendedList> ListValues, bool IsStacked, eGraphType GraphType, eOrientation HistoOrientation)
        {
            WindowForPanelHisto = new FormPanelForHisto(ListValues, IsStacked, GraphType, HistoOrientation);
        }

        public cPanelHisto(cExtendedList ListValues, eGraphType GraphType, eOrientation HistoOrientation)
        {
            WindowForPanelHisto = new FormPanelForHisto(new List<cExtendedList>() { ListValues }, false, GraphType, HistoOrientation);
        }

        //public System.Windows.Forms.Control GetPanel()
        //{
        //    return this.WindowForPanelHisto.panel.Controls["panel"];
        //}
    }
}
