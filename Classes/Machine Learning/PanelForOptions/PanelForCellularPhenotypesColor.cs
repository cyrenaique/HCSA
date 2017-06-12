using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using HCSAnalyzer.Classes;

namespace HCSAnalyzer.Forms.FormsForOptions.PanelForOptions
{
    public partial class PanelForCellularPhenotypesColor : UserControl
    {
        public PanelForCellularPhenotypesColor(cGlobalInfo GlobalInfo)
        {
            InitializeComponent();

            this.panelForCellularPhenotypes.Controls.Add(new PanelForPhenotypeEditing(GlobalInfo));

        }
    }
}
