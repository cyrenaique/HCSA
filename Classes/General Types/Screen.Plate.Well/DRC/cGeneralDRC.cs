using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;

namespace HCSAnalyzer.Classes.General_Types
{

    public class cListWellsForDRC : cListWells
    {
        public double Concentration = -1;
    
    }

    public class cGeneralDRC : List<cListWellsForDRC>
    {

        public cExtendedTable ListAverageValues = new cExtendedTable();

        public cGeneralDRC(cListWells ListWells)
        {
            cExtendedList TmpListConcentrations = new cExtendedList();
            cExtendedTable ListConcentrations = new cExtendedTable(TmpListConcentrations);

            for (int i = 0; i < ListWells.Count; i++)
            {
                object ConcentrationVal = ListWells[i].ListProperties.FindValueByName("Concentration");
                if (ConcentrationVal != null)
                {
                    double CurrentConc = (double)ConcentrationVal;
                    bool IsExist = false;
                    foreach (double item in ListConcentrations[0])
                    {
                        if (item == CurrentConc)
                        {
                            IsExist = true;
                            break;
                        }
                    }
                    if(!IsExist)
                        ListConcentrations[0].Add(CurrentConc);
                }
                else
                    continue;
            }

            if (ListConcentrations[0].Count == 0) return;

            cSort S = new cSort();
            S.SetInputData(ListConcentrations);
            S.Run();
            ListConcentrations = S.GetOutPut();


            foreach (var item in ListConcentrations[0])
            {
                cListWellsForDRC ListWellsForDRC = new cListWellsForDRC();
                ListWellsForDRC.Concentration = item;

                this.Add(ListWellsForDRC);
            }

            for (int i = 0; i < ListWells.Count; i++)
            {
                object ConcentrationVal = ListWells[i].ListProperties.FindValueByName("Concentration");
                if (ConcentrationVal == null) continue;
               
                double CurrentConc = (double)ConcentrationVal;


                foreach (var item in this)
                {
                    if (item.Concentration == CurrentConc)
                    {
                        item.Add(ListWells[i]);
                        break;
                    }
                }

            }

            cExtendedTable TableToBeReturned = new cExtendedTable();

            foreach (var item in this)
            {
                cExtendedTable AverageWell = item.GenerateAverageWell();

                if (TableToBeReturned.Count == 0)
                {
                    TableToBeReturned = new cExtendedTable(AverageWell);
                }
                else
                {
                    cMerge M = new cMerge();
                    M.SetInputData(TableToBeReturned, AverageWell);
                    M.IsHorizontal = false;
                    M.Run();
                    TableToBeReturned = M.GetOutPut();
                }
                
               // this.ListAverageValues.Add(AverageWell);
            }

            this.ListAverageValues = TableToBeReturned;
        }

    }
}
