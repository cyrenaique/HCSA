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
    public partial class PanelForWellClassesColor : UserControl
    {
        public PanelForWellClassesColor(cGlobalInfo GlobalInfo)
        {
            InitializeComponent();

            this.panelForWellClasses.Controls.Add(new PanelForClassEditing(GlobalInfo));

            //Controls.Add(new PanelForClassEditing(this));
            //panelForCellularPhenotypes.Controls.Add(new PanelForPhenotypeEditing(this));


        }
    }
}
