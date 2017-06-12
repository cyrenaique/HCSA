using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Viewers;

namespace HCSAnalyzer.Classes.MetaComponents
{
    class cDisplayExtendedTable : cComponent
    {
        cExtendedTable Input;
        public int DigitNumber = 5;
    
        public cDisplayExtendedTable()
        {
            this.Title = "Display ExtendedDataTable";
            
        }

        public cFeedBackMessage Run()
        {
           
            if ((this.Input == null)||(this.Input.Count==0))
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
            // here is the core of the meta component ...
            // just a list of Component steps

            cViewerTable MyTable = new cViewerTable();
            MyTable.SetInputData(this.Input);
            MyTable.DigitNumber = DigitNumber;
            MyTable.Run();

            cDesignerSinglePanel MyDesigner = new cDesignerSinglePanel();
            MyDesigner.SetInputData(MyTable.GetOutPut());
            MyDesigner.Run();

            cDisplayToWindow MyDisplay = new cDisplayToWindow();
            MyDisplay.SetInputData(MyDesigner.GetOutPut());
            MyDisplay.Title = this.Input.Name;
            MyDisplay.Run();
            MyDisplay.Display();
        }


        public void SetInputData(cExtendedTable Input)
        {
            this.Input = Input;
        }
    }
}

