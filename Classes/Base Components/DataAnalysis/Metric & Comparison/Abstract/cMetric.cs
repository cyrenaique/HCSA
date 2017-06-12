using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    abstract class cMetric : cComponent
    {
        protected cExtendedList Input1;
        protected cExtendedList Input2;

        protected cExtendedTable Output;

        public cExtendedTable GetOutPut()
        {
            return this.Output;
        }
    }
}
