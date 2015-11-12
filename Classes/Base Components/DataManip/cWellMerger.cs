using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.General_Types;
using LibPlateAnalysis;
using HCSAnalyzer.Classes.Base_Classes.GUI;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    class cWellMerger : cDataAnalysisComponent
    {
        cListWells Input;
        cListListWells OutPut;
        public bool IsGUI = true;
        public List<cPropertyType> ListSelectedProp = null;


        public cWellMerger()
        {
            this.Title = "Transpose";
        }

        public void SetInputData(cListWells Input)
        {
            this.Input = Input;
        }

        public cListListWells GetOutPut()
        {
            return this.OutPut;
        }

        public cFeedBackMessage Run()
        {
           
            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }
            if (this.IsGUI)
            {
                cGUI_ListWellProperty GUI_ListWellProperty = new cGUI_ListWellProperty();
                if (GUI_ListWellProperty.Run().IsSucceed == false)
                {
                    FeedBackMessage.IsSucceed = false;
                    FeedBackMessage.Message = "No propoerties selected";
                    return FeedBackMessage;
                }
                ListSelectedProp = GUI_ListWellProperty.GetOutPut();
            
            }


            this.OutPut = new cListListWells();

            foreach(cWell TmpWell in this.Input)
            {
                bool IsSimilar = false;
                for (int i = 0; i < this.OutPut.Count; i++)
                {
                    cWell WellToCompareTo = this.OutPut[i][0];
                    bool TmpSimilarity = true;

                    foreach (cPropertyType Prop in ListSelectedProp)
                    {
                        object Val = WellToCompareTo.ListProperties.FindValueByName(Prop.Name);
                        object Val2 = TmpWell.ListProperties.FindValueByName(Prop.Name);

                        if ((Val == null) && (Val2 == null))
                        {
                            TmpSimilarity = false;
                            break;
                        }
                        if ((Val == null) || (Val2 == null))
                        {
                            TmpSimilarity = false;
                            break;
                        }
                        if (!Val.Equals(Val2))
                        {
                            TmpSimilarity = false;
                            break;
                        }
                    }

                    if (TmpSimilarity == true)
                    {
                        this.OutPut[i].Add(TmpWell);
                        IsSimilar = true;
                        break;
                    }
                }
                if (IsSimilar == false)
                {
                    cListWells LW = new cListWells();
                    LW.Name = "";
                    foreach (var item in TmpWell.ListProperties)
                    {
                        foreach (var P in ListSelectedProp)
                        {
                            if (P.Name == item.PropertyType.Name)
                            {
                                if (item.GetValue() != null)
                                {
                                    if (P.Name == "Well Class")
                                    {
                                        int Value = (int)item.GetValue();
                                        LW.Name += "[" + item.PropertyType.Name + "] = " + cGlobalInfo.ListWellClasses[Value].Name + "\n";
                                    }
                                    else
                                    {
                                        LW.Name += "[" + item.PropertyType.Name + "] = " + item.GetValue().ToString() + "\n";
                                    }
                                }
                                else
                                    goto NEXTLOOP;
                            }
                        }
                    }

                    LW.Add(TmpWell);
                    this.OutPut.Add(LW);
                NEXTLOOP: ;
                }
            }






            return FeedBackMessage;
        }


    }
}
