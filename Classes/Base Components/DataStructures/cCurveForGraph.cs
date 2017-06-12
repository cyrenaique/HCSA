using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;

namespace HCSAnalyzer.Classes.Base_Classes.DataStructures
{
    public class cCurveForGraph : cComponent
    {
        public cExtendedTable ListPtValues;
       // public cExtendedList ListXValues;

        cExtendedTable InputTable;

        public cCurveForGraph()
        {
            this.Title = "Curve for Graph";
        }

        public void SetInputPoints(/*cExtendedList ListXValues, */cExtendedTable ListPtValues)
        {
            this.ListPtValues = ListPtValues;
           // this.ListXValues = ListXValues;
        }


        public void SetInputData(cExtendedTable InputTable)
        {
            this.InputTable = InputTable;

            //this.ListXValues = ListXValues;
        }


        public cExtendedList GetListXValues()
        {
            if ((this.ListPtValues == null) || (this.ListPtValues.Count == 0)) return null;

            cExtendedList ET = new cExtendedList();
            
            ET.Name = "List X-Values";
            for (int i = 0; i < this.ListPtValues.Count; i++)
            {
                ET.Add(this.ListPtValues[i][0]);
            }
            return ET;
        }


        public cFeedBackMessage Run()
        {

            if (this.InputTable == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }
            if (this.InputTable.Count != 2)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "This process requires 2 columns";
                return FeedBackMessage;
            }

            cSort S = new cSort();
            S.SetInputData(this.InputTable);
            S.Run();

            cExtendedTable TableToProcess = S.GetOutPut();

            this.ListPtValues = new cExtendedTable();
            int CurrentTablePos = 0;

            //while ((CurrentTablePos <= TableToProcess[0].Count))
            for (;;)
            {

                if (CurrentTablePos > TableToProcess[0].Count - 1) break;
                double OriginalXValue = TableToProcess[0][CurrentTablePos];
                
                cExtendedList NewPtList = new cExtendedList();
                
                // first add the concentration
                NewPtList.Add(OriginalXValue);

                // then the first associated value
                NewPtList.Add(TableToProcess[1][CurrentTablePos]);

                //double TmpValue = OriginalXValue;
                //CurrentTablePos++;
                
                //while (TableToProcess[0][CurrentTablePos] == OriginalXValue)
                for (; ; )
                {
                    CurrentTablePos++;
                    if (CurrentTablePos >= TableToProcess[1].Count) break;
                    if(TableToProcess[0][CurrentTablePos]!=OriginalXValue) break;
                    
                    NewPtList.Add(TableToProcess[1][CurrentTablePos]);
                    //if (CurrentTablePos >= TableToProcess[0].Count - 1)
                    //{

                       // break;
                   // }

                    
                    if(TableToProcess[0][CurrentTablePos]!=OriginalXValue) break;

                    //if(CurrentTablePos
                        
                        
                    //if (TmpValue == OriginalXValue) break;
                    //  TmpValue = TableToProcess[0][CurrentTablePos];
                }
                
                this.ListPtValues.Add(NewPtList);
                //CurrentTablePos++;

            }

            //double OriginalXValue1 = TableToProcess[0][CurrentTablePos];
            //cExtendedList NewPtList1 = new cExtendedList();
            //NewPtList1.Add(OriginalXValue1);
            //NewPtList1.Add(TableToProcess[1][CurrentTablePos++]);
            //this.ListPtValues.Add(NewPtList1);
            //double TmpValue1 = OriginalXValue1;
            //while (TableToProcess[0][CurrentTablePos] == OriginalXValue1)
            //{
            //    NewPtList1.Add(TableToProcess[1][CurrentTablePos++]);
            //    if (CurrentTablePos >= TableToProcess[0].Count - 1)
            //    {

            //        break;
            //    }
            //    //if (TmpValue == OriginalXValue) break;
            //    //  TmpValue = TableToProcess[0][CurrentTablePos];
            //}
            //this.ListPtValues[this.ListPtValues.Count-1].Add(TableToProcess[1][CurrentTablePos]);
           this.ListPtValues.Name = this.InputTable.Name;

            return FeedBackMessage;

        }

    }
}
