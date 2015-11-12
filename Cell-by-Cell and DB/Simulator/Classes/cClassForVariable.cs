using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HCSAnalyzer.Simulator.Classes
{

    [Serializable]
    public class cClassForRandomParam
    {
        public double Min = 0;
        public double Max = 1;

    }

    [Serializable]
    public class cListVariables : List<cClassForVariable>
    {
        public cListVariables(List<cClassForVariable> ListVar)
        {
            foreach (cClassForVariable item in ListVar)
            {
                this.Add(item);
            }
        }

        public cClassForVariable FindVariable(string Name)
        {
            foreach (cClassForVariable item in this)
                if (item.Name == Name) return item;                

            return null;
        }
    }

    [Serializable]
    public class cClassForVariable
    {
        public string Name;
        public bool IsConstant = true;
        public bool IsVariableAlongColumns = false;
        public bool IsVariableAlongRows = false;
        public bool IsVariableRandom = false;
        public double Increment = 1.0f;

        public double Cst_Value;

        public cClassForVariable(string Name, double Value)
        {
            this.Cst_Value = Value;
            this.Name = Name;
        }

        public cClassForRandomParam RandomInfo = new cClassForRandomParam();
    }
}
