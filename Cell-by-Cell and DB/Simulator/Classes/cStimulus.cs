using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HCSAnalyzer.Cell_by_Cell_and_DB.Simulator.Classes
{
    public abstract class cStimulus
    {
        string Name;
        object Tag;

        public string GetName()
        {
            return this.Name;
        }

        public cStimulus(string Name)
        {
        
        }
    }

    public class cStimuli : List<cStimulus>
    {
    
        //public c

    }

    public class cStimulus_Physical : cStimulus
    {
        public cStimulus_Physical(string Name) : base(Name)
        {
        }
    }

    public class cStimulus_Chemical : cStimulus
    {
        public cStimulus_Chemical(string Name)
            : base(Name)
        {
        }
    }

    public class cStimulus_Biological : cStimulus
    {
        public cStimulus_Biological(string Name)
            : base(Name)
        {
        }
    }


}
