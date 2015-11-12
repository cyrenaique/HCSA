using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HCSAnalyzer.Classes.General_Types
{
    public enum eDataType { DOUBLE, BOOL, STRING, INTEGER, TIME }
    public enum eIntegerType { ODD, EVEN, BOTH }

    public class cPropertyType
    {
        public string Name { get; private set; }
        public eDataType Type { get; private set; }

        public static double DefaultMin = -999999999999999999;
        public static double DefaultMax = 999999999999999999;

        public double Min;
        public double Max;
        public bool IsConstant = true;
        public bool IsLocked = false;
        public eIntegerType IntType = eIntegerType.BOTH;
        public List<string> ListPotentialString = new List<string>();

        public bool IsTobeDisplayed = false;

        public cPropertyType(string Name, eDataType PropertyType)
        {
            this.Name = Name;
            this.Type = PropertyType;

            Min = DefaultMin;
            Max = DefaultMax;
        }


        public string GetInfo()
        {
            string ToReturn = "[Property Type] "+ this.Name + " - Type: " + this.Type;
            if ((this.Type == eDataType.INTEGER) || (this.Type == eDataType.DOUBLE))
            {
                ToReturn += ". Min: " + this.Min + " - Max: " + this.Max;
            }
            if (this.IsLocked)
                ToReturn += " [Locked]";
            else
                ToReturn += " [Unlocked]";
            return ToReturn;
        }

    }
}
