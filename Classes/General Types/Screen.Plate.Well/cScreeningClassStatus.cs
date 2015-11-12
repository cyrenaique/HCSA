using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HCSAnalyzer.Classes.General_Types
{
    public class cScreeningClassStatus
    {
        public string Name;
        public DateTime Time { get; private set; }

        public cScreeningClassStatus(string Name)
        {
            this.Name = Name;
            this.Time = DateTime.Now;
            
        }

    }
}
