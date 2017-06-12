using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibPlateAnalysis;

namespace HCSAnalyzer.Classes.General_Types
{

    public enum eComparison { HT, LT, HET, LET, E, NE, I, NI };

    public class cListProperty : List<cProperty>
    {
        public object FindValueByName(string PropertyName)
        {
            foreach (var item in this)
            {
                if (item.PropertyType.Name == PropertyName)
                {
                    return item.GetValue();
                }
            }
            return null;
        }

        public cProperty FindPropertyByName(string PropertyName)
        {
            foreach (var item in this)
            {
                if (item.PropertyType.Name == PropertyName)
                {
                    return item;
                }
            }
            return null;
        }

        public static bool Compare(cProperty Prop1, cProperty Prop2, eComparison ComparisonType)
        {
            bool ToReturn = false;

            if (Prop1.PropertyType.Type != Prop2.PropertyType.Type)
                return false;

            eDataType PropTypeData = Prop1.PropertyType.Type;

            if (PropTypeData == eDataType.BOOL)
            {
                if ((ComparisonType != eComparison.E) && (ComparisonType != eComparison.NE)) return false;
            }
            else if (PropTypeData == eDataType.STRING)
            {
                if ((ComparisonType != eComparison.E) && (ComparisonType != eComparison.NE) &&
                    (ComparisonType != eComparison.I) && (ComparisonType != eComparison.NI) ) return false;

                string Value1 = (string)Prop1.GetValue();
                string Value2 = (string)Prop2.GetValue();

                switch (ComparisonType)
                {
                    case eComparison.I:
                        return (Value2.Contains(Value1));
                    case eComparison.NI:
                        return (!Value2.Contains(Value1));
                    case eComparison.E:
                        return (Value1 == Value2);
                    case eComparison.NE:
                        return (Value1 != Value2);

                    default:
                        return false;
                }


            }
            else if (PropTypeData == eDataType.DOUBLE)
            {
                if ((ComparisonType != eComparison.E) && (ComparisonType != eComparison.NE) &&
                    (ComparisonType != eComparison.LET) && (ComparisonType != eComparison.HET) &&
                    (ComparisonType != eComparison.LT) && (ComparisonType != eComparison.HT)) return false;

                double Value1 = (double)Prop1.GetValue();
                double Value2 = (double)Prop2.GetValue();

                switch (ComparisonType)
                {
                    case eComparison.LT:
                        return (Value1 < Value2);
                    case eComparison.HT:
                        return (Value1 > Value2);
                    case eComparison.HET:
                        return (Value1 >= Value2);
                    case eComparison.LET:
                        return (Value1 <= Value2);
                    case eComparison.E:
                        return (Value1 == Value2);
                    case eComparison.NE:
                        return (Value1 != Value2);

                    default:
                        return false;
                }

            }
            else if (PropTypeData == eDataType.INTEGER)
            {
                if ((ComparisonType != eComparison.E) && (ComparisonType != eComparison.NE) &&
                    (ComparisonType != eComparison.LET) && (ComparisonType != eComparison.HET) &&
                    (ComparisonType != eComparison.LT) && (ComparisonType != eComparison.HT)) return false;

                int Value1 = (int)Prop1.GetValue();
                int Value2 = (int)Prop2.GetValue();

                switch (ComparisonType)
                {
                    case eComparison.LT:
                        return (Value1 < Value2);
                    case eComparison.HT:
                        return (Value1 > Value2);
                    case eComparison.HET:
                        return (Value1 >= Value2);
                    case eComparison.LET:
                        return (Value1 <= Value2);
                    case eComparison.E:
                        return (Value1 == Value2);
                    case eComparison.NE:
                        return (Value1 != Value2);

                    default:
                        return false;
                }

            }





            return ToReturn;
        }


        public bool UpdateValueByName(string PropertyName, object NewValue)
        {
            bool toReturn = false;
            foreach (var item in this)
            {
                if (item.PropertyType.Name == PropertyName)
                {
                    item.SetNewValue(NewValue);
                    return true;
                }
            }
            return toReturn;
        }

        public cProperty FindByName(string Name)
        {
            cProperty ToReturn = null;

            foreach (var item in this)
            {
                if (item.PropertyType.Name == Name)
                    return item;
            }

            return ToReturn;
        }
    }

    public class cListPlateProperty : cListProperty//List<cProperty>
    {
        public cPlate AssociatedPlate = null;

        public cListPlateProperty(cPlate AssociatedPlate)
        {
            this.AssociatedPlate = AssociatedPlate;
            foreach (var item in AssociatedPlate.ParentScreening.ListPlatePropertyTypes)
            {
                this.Add(new cProperty(item, null));
            }
        }

    }

    public class cListWellProperty : cListProperty//List<cProperty>
    {
        public cWell AssociatedWell = null;

        public cListWellProperty(cWell AssociatedWell)
        {
            this.AssociatedWell = AssociatedWell;
            if (cGlobalInfo.CurrentScreening.ListWellPropertyTypes==null) return;
            foreach (var item in cGlobalInfo.CurrentScreening.ListWellPropertyTypes)
            {
                this.Add(new cProperty(item, null));
            }
        }

    }
}
