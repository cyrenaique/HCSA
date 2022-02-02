using System.Windows.Forms;

namespace HCSAnalyzer
{
    public class PlateTabPage : TabPage
    {
        public PlateTabPage()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.DoubleBuffered = true;
        }
    }
}
