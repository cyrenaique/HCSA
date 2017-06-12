using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using LibPlateAnalysis;
using HCSAnalyzer.Classes.General_Types;

namespace HCSAnalyzer.Classes.MetaComponents
{
    class cGetListPhenotypes : cComponent
    {
        cListWells Input;
        cExtendedTable Output;

        public cGetListPhenotypes()
        {
            this.Title = "Get Phenotypes List";
        }

        public cFeedBackMessage Run()
        {
            
            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }
            Process();
            
            return FeedBackMessage;
        }


        public cExtendedTable GetOutPut()
        {
            return this.Output;
        }

        void Process()
        {
            // here is the core of the meta component ...
            // just a list of Component steps
            foreach (cWell item in Input)
            {
                item.AssociatedPlate.DBConnection = new cDBConnection(item.AssociatedPlate, item.SQLTableName);

                cListSingleBiologicalObjects ListPhenotypes = item.AssociatedPlate.DBConnection.GetBiologicalPhenotypes(item);

                cExtendedTable ET = item.AssociatedPlate.DBConnection.GetWellValues(item, cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors());
                item.AssociatedPlate.DBConnection.CloseConnection();

                ET.ListTags = new List<object>();

                for (int i = 0; i < ET.Count; i++)
                {
                    ET[i].ListTags = new List<object>();

                    for (int j = 0; j < ListPhenotypes.Count; j++)
                        ET[i].ListTags.Add(ListPhenotypes[j]);
                }

                for (int j = 0; j < ListPhenotypes.Count; j++)
                    ET.ListTags.Add(ListPhenotypes[j]);


                if (Output == null)
                    Output = new cExtendedTable(ET);
                else
                {
                    cMerge M = new cMerge();
                    M.IsHorizontal = false;
                    M.SetInputData(Output, ET);
                    M.Run();

                    Output = M.GetOutPut();
                }
            }
        }

        public void SetInputData(cListWells Input)
        {
            this.Input = Input;
        }
    }
}
