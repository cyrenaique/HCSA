using HCSAnalyzer.Classes;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using System.Windows.Forms;

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
