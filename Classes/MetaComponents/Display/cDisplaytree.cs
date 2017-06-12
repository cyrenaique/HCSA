using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Viewers;

namespace HCSAnalyzer.Classes.MetaComponents
{
    class cDisplayTree : cComponent
    {
        string Input;
        
    
        public cDisplayTree()
        {
            this.Title = "Display Tree";
            
        }

        public cFeedBackMessage Run()
        {
           
            if ((this.Input == null)||(this.Input==""))
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

            cViewerGraphTree MyTree = new cViewerGraphTree();
            MyTree.SetInputData(this.Input);
            MyTree.Run();

            cDesignerSinglePanel MyDesigner = new cDesignerSinglePanel();
            MyDesigner.SetInputData(MyTree.GetOutPut());
            MyDesigner.Run();

            cDisplayToWindow MyDisplay = new cDisplayToWindow();
            MyDisplay.SetInputData(MyDesigner.GetOutPut());
            MyDisplay.Title = "Tree Viewer";
            MyDisplay.Run();
            MyDisplay.Display();
        }


        public void SetInputData(string Input)
        {
            this.Input = Input;
        }
    }
}

