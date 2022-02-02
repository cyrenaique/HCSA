using System.Windows.Forms;

namespace HCSAnalyzer.Simulator.Forms.NewCellType
{
    public partial class PanelForParamGeneralForCellType : UserControl
    {
        public PanelForParamGeneralForCellType()
        {
            InitializeComponent();
        }

        private void panelColor_MouseClick(object sender, MouseEventArgs e)
        {
            ColorDialog ColorPicker = new ColorDialog();
            if (ColorPicker.ShowDialog() != DialogResult.OK) return;
            panelColor.BackColor = ColorPicker.Color;
            panelColor.Refresh();

        }
    }
}
