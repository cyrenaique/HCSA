using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Forms.FormsForOptions.ClassForOptions.Children;
using System.Windows.Forms;
using HCSAnalyzer.Classes;

namespace HCSAnalyzer.Forms.FormsForOptions.ClassForOptions
{
    [Serializable]
    public class cListOptions : List<cOptionGeneral>
    {
        public cListOptions(cGlobalInfo GlobalInfo)
        {
            this.Add(new cOption3D("3D"));
            this.Add(new cOptionDisplayPlatesandWells("Plates and Wells"));
            this.Add(new cOptionWellClassesColor("Well Classes", GlobalInfo));
            this.Add(new cOptionCellularPhenotypesColor("Cellular Phenotypes", GlobalInfo));
        }

        public Panel GetPanel(string Name)
        {
            if (Name == null) return null;

            foreach (var item in this)
                if (item.Name == Name) return item.GetPanel();

            return null;
        }

    }
}
