using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibPlateAnalysis
{
    public class cGeneralComponent
    {
        protected ToolStripMenuItem SpecificContextMenu = null;
        protected string ShortInfo;

        public string GetShortInfo()
        {

            return this.ShortInfo;
        }

        public ToolStripMenuItem GetContextMenu()
        {
            return this.SpecificContextMenu;
        }
    }



    public class cGeneralClassWithContextMenu : cGeneralComponent
    {
    
    
    }

}
