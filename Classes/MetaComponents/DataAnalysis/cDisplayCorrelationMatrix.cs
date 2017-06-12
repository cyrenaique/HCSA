using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes.Viewers;

namespace HCSAnalyzer.Classes.MetaComponents
{
    class cDisplayCorrelationMatrix : cComponent
    {
        cExtendedTable Input;
        eCorrelationType CorrelationType = eCorrelationType.PEARSON;

        public cDisplayCorrelationMatrix()
        {
            this.Title = "Display Correlation Matrix";
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

        void Process()
        {
            // here is the core of the meta component ...
            // just a list of Component steps

            cCorrelationMatrix MyCorrelation = new cCorrelationMatrix();
            MyCorrelation.SetCorrelationType(this.CorrelationType);
            MyCorrelation.SetInputData(Input);
            MyCorrelation.Run();

            cViewerTable MyTable = new cViewerTable();
            MyTable.SetInputData(MyCorrelation.GetOutPut());
            MyTable.Run();

            cDesignerSinglePanel MyDesigner = new cDesignerSinglePanel();
            MyDesigner.SetInputData(MyTable.GetOutPut());
            MyDesigner.Run();

            cDisplayToWindow MyDisplay = new cDisplayToWindow();
            MyDisplay.SetInputData(MyDesigner.GetOutPut());
            MyDisplay.Title = this.Title;      
            MyDisplay.Run();
            MyDisplay.Display();
        }

        public void SetCorrelationType(eCorrelationType Type)
        {
            this.CorrelationType = Type;
        }

        public void SetInputData(cExtendedTable Input)
        {
            this.Input = Input;
        }
    }
}
