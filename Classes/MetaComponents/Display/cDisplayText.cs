using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Viewers;

namespace HCSAnalyzer.Classes.MetaComponents
{
    class cDisplayText : cComponent
    {
        string Input;

        public cDisplayText()
        {
            this.Title = "Text Viewer";
        }

        public cFeedBackMessage Run()
        {
            Process();
            FeedBackMessage.IsSucceed = true;
            return FeedBackMessage;
        }

        void Process()
        {
            // here is the core of the meta component ...
            // just a list of Component steps

            cViewertext VT = new cViewertext();
            VT.SetInputData(this.Input);
            VT.Run();

            cDesignerSinglePanel MyDesigner = new cDesignerSinglePanel();
            MyDesigner.SetInputData(VT.GetOutPut());
            MyDesigner.Run();

            cDisplayToWindow MyDisplay = new cDisplayToWindow();
            MyDisplay.SetInputData(MyDesigner.GetOutPut());
            MyDisplay.Title = this.Title;
            MyDisplay.Run();
            MyDisplay.Display();
        }


        public void SetInputData(string Input)
        {
            this.Input = Input;
        }
    }
}

