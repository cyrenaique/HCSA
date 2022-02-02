using System.Windows.Forms;

namespace HCSAnalyzer.Forms.FormsForOptions.ClusteringInfo
{
    public partial class PanelForParamKMeans : UserControl
    {
        public PanelForParamKMeans()
        {
            InitializeComponent();
            this.comboBoxDistance.SelectedText = "Euclidean";

        }
    }
}
