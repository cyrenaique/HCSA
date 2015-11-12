using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes;

namespace HCSAnalyzer.Forms
{
    public class cGUIPlateLUT
    {
        public FormForPlateLUT CurrentFormForPlateLUT;
        public Panel AssociatedPanel =null;

        public cGUIPlateLUT()
        {
            CurrentFormForPlateLUT = new FormForPlateLUT();
            this.AssociatedPanel = CurrentFormForPlateLUT.panelForPlateLUT;
        }


    }
}
