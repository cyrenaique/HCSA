using System.Windows.Forms;

namespace HCSAnalyzer.Forms.FormsForOptions.ClusteringInfo
{
    public partial class PanelForParamHierarchical : UserControl
    {
        public PanelForParamHierarchical()
        {
            InitializeComponent();
            this.comboBoxDistance.SelectedText = "Euclidean";
            this.comboBoxLinkType.SelectedText = "COMPLETE";
        }
    }
}
