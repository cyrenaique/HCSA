using System.Windows.Forms;

namespace HCSAnalyzer.Forms
{
    public class cGUIPlateLUT
    {
        public FormForPlateLUT CurrentFormForPlateLUT;
        public Panel AssociatedPanel = null;

        public cGUIPlateLUT()
        {
            CurrentFormForPlateLUT = new FormForPlateLUT();
            this.AssociatedPanel = CurrentFormForPlateLUT.panelForPlateLUT;
        }


    }
}
