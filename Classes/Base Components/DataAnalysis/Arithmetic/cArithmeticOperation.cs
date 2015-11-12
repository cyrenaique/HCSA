using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataProcessing
{

    abstract class cArithmeticOperation : cComponent
    {
        protected cExtendedTable Input1;
        protected cExtendedTable Input2;
        protected cExtendedTable Output;

        public cExtendedTable GetOutPut()
        {
            return this.Output;
        }




        
    }
}
