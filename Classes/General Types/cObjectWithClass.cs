using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibPlateAnalysis;
using System.Drawing;

namespace HCSAnalyzer.Classes.General_Types
{
    public class cObjectWithClass : cGeneralComponent
    {
        public int Idx { get; protected set; }
        public Color ColourForDisplay;// {get; protected set; }
        public string Name;

    }
}
