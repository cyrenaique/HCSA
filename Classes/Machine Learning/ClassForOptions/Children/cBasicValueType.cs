using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Controls;

namespace HCSAnalyzer.Forms.FormsForOptions.ClassForOptions.Children
{
    public abstract class cGeneralValue
    {
        public string Name;

        public cGeneralValue(string Name)
        {
            this.Name = Name;
        }
    }



    [Serializable]
    public class cCheckValue : cGeneralValue
    {
        public bool Value;

        public cCheckValue(bool Value, string Name) : base(Name)
        {
            this.Value = Value;
        }
    }

    [Serializable]
    public class cColorValue : cGeneralValue
    {
        public Color Value;

        public cColorValue(Color Value, string Name)
            : base(Name)
        {
            this.Value = Value;
        }
    }

    [Serializable]
    public class cDoubleValue : cGeneralValue
    {
        public double Value;

        public cDoubleValue(double Value, string Name)
            : base(Name)
        {
            this.Value = Value;
        }
    }

    [Serializable]
    public class cTextValue : cGeneralValue
    {
        public string Value;

        public cTextValue(string Value, string Name)
            : base(Name)
        {
            this.Value = Value;
        }
    }  
    
    public class cListViewValue : cGeneralValue
    {
        public System.Windows.Controls.ListView Value;

        public cListViewValue(System.Windows.Controls.ListView Value, string Name)
            : base(Name)
        {
            this.Value = Value;
        }
    }
}
