using System.Windows.Forms;

namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{
    public partial class PanelForParamKNN : UserControl
    {
        public PanelForParamKNN()
        {
            InitializeComponent();
            this.comboBoxDistanceWeight.SelectedText = "No Weighting";
            this.comboBoxDistance.SelectedText = "Euclidean";
        }
    }
}
