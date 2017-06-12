using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    public abstract class cDesignerParent : cComponent
    {

        protected cExtendedControl OutPut;

        public cExtendedControl GetOutPut()
        {
            return this.OutPut;
        }

    }
}
