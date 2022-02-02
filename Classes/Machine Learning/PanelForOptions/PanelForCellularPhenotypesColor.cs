using HCSAnalyzer.Classes;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using System.Windows.Forms;

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
