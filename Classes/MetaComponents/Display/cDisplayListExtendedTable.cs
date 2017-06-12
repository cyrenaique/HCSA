using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Viewers;

namespace HCSAnalyzer.Classes.MetaComponents
{
    class cDisplayListExtendedTable : cComponent
    {
        cListExtendedTable Input;


        public cDisplayListExtendedTable()
        {
            this.Title = "Display ListExtendedTable";
            
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
            FeedBackMessage.IsSucceed = true;
            return FeedBackMessage;
        }

        void Process()
        {
            cDesignerTab DT = new cDesignerTab();

            for (int i = 0; i < this.Input.Count; i++)
            {
                cViewerTable MyTable = new cViewerTable();
                MyTable.SetInputData(this.Input[i]);
                MyTable.Run();

                DT.SetInputData(MyTable.GetOutPut());
            }
            DT.Run();

            cDisplayToWindow MyDisplay = new cDisplayToWindow();
            MyDisplay.SetInputData(DT.GetOutPut());
            MyDisplay.Title = this.Input.Name;
            MyDisplay.Run();
            MyDisplay.Display();
        }


        public void SetInputData(cListExtendedTable Input)
        {
            this.Input = Input;
        }
    }
}

