using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using analysis;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using LibPlateAnalysis;

namespace HCSAnalyzer.Classes.DataAnalysis
{
    public class cMineAnalysis : cDataAnalysisComponent
    {
        #region Private
        cExtendedTable Input;
        cListExtendedTable Output;
        #endregion

        #region public parameters
        public bool Is_BriefReport = true;
        public cScreening CurrentScreening = null;
        #endregion

        


        public cMineAnalysis()
        {
            this.Title = "MINE Analysis";
        }

        public void SetInputData(cExtendedTable Input)
        {
            this.Input = Input;
        }

        public cListExtendedTable GetOutPut()
        {
            return this.Output;
        }

        public void Run()
        {
            // define names
            string[] VarNames = new string[Input.Count];
            double[][] dataset = new double[Input.Count][];
            
            int Idx = 0;
            foreach (var item in Input)
            {
                dataset[Idx] = new double[item.Count];
                Array.Copy(item.ToArray(), dataset[Idx], item.Count);
                VarNames[Idx++] = item.Name;
            }

            //// define data
            data.Dataset data_Set = new data.Dataset(dataset, VarNames, 0);
            analysis.VarPairQueue Qu = new analysis.VarPairQueue(data_Set);

            for(int Idx_0=0;Idx_0<this.Input.Count;Idx_0++)
                for (int Idx_1 = 0; Idx_1 <= Idx_0; Idx_1++)
                    Qu.addPair(Idx_1,Idx_0);

            Analysis ana = new Analysis(data_Set, Qu);
            AnalysisParameters param = new AnalysisParameters();
            double resparam = param.commonValsThreshold;

            java.lang.Class t = java.lang.Class.forName("analysis.results.FullResult");

            if(this.Is_BriefReport)
                t = java.lang.Class.forName("analysis.results.BriefResult");

            ana.analyzePairs(t, param);
            analysis.results.Result[] res = ana.getSortedResults();

            List<string[]> ListValues = new List<string[]>();
            List<bool> ListIscolor = new List<bool>();

            for (Idx = 0; Idx < res.Length; Idx++)
            {
                ListValues.Add(res[Idx].toString().Split(','));
            }
            string[] ListNames = res[0].getHeader().Split(',');

            this.Output = new cListExtendedTable();

            for (int IdxTest = 2; IdxTest < ListNames.Length; IdxTest++)    // loop over all the different type of results
            {
                // remove useless informations
                if (ListNames[IdxTest] == "MI via KDE") continue;
                if (ListNames[IdxTest] == "Fisher") continue;
                if (ListNames[IdxTest] == "last value") continue;
                if (ListNames[IdxTest] == "MAS found at (X)") continue;
                if (ListNames[IdxTest] == "MAS found at (Y)") continue;
                if (ListNames[IdxTest] == "MICfound at (Y)") continue;

                double[,] TmpTable = new double[VarNames.Length, VarNames.Length];

                for (int i = 0; i < res.Length; i++)    // loop over the different pairs
                {
                    string TmpName0 = res[i].getXVar();
                    string TmpName1 = res[i].getYVar();
                    int Idx_var0 = 0;
                    int Idx_var1 = 0;

                    for (Idx_var0 = 0; Idx_var0 < VarNames.Length; Idx_var0++)
                        if(VarNames[Idx_var0]==TmpName0)
                            break;

                    for (Idx_var1 = 0; Idx_var1 < VarNames.Length; Idx_var1++)
                        if (VarNames[Idx_var1] == TmpName1)
                            break;

                    double Value=0;
                    double.TryParse(res[i].toString().Split(',')[IdxTest], out Value);
                    TmpTable[Idx_var0, Idx_var1] = TmpTable[Idx_var1, Idx_var0] = Value;
                }

                cExtendedTable NewTable = new cExtendedTable(TmpTable);
                NewTable.ListRowNames = new List<string>();
                for (int i = 0; i < VarNames.Length; i++)
                {
                    NewTable.ListRowNames.Add(VarNames[i]);
                    NewTable[i].Name = VarNames[i];


                    if (CurrentScreening != null)
                    {
                        int IdxDesc = cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(NewTable[i].Name);
                        if (IdxDesc > -1)
                        {
                            NewTable[i].Tag = cGlobalInfo.CurrentScreening.ListDescriptors[IdxDesc];
                        }
                    }

                }

                NewTable.Name = ListNames[IdxTest];
                this.Output.Add(NewTable);
            }



        }

    }
}
