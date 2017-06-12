using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using ImageAnalysis;
using HCSAnalyzer.Classes.Base_Classes.Viewers._2D;
using System.Windows.Forms;
using System.Drawing;

namespace HCSAnalyzer.Classes.MetaComponents
{
    public class cDisplaySingleImage : cComponent
    {
        cImage Input;
        public bool IsDisplayScale = false;
        public bool IsUseSavedDefaultDisplayProperties = false;
        public int DefaultZoom = 0; // 0 <=> automated
        public cViewerImage MyImageViewer;
        public List<Color> ListLinearMaxColor = null; // if this list not null, the LUT will based on these colors as maximum values

        public cDisplaySingleImage()
        {
            this.Title = "Display Image";
        }

        public cFeedBackMessage Run()
        {
            base.Start();

            if (this.Input == null)
            {
                
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }
            Process();
            FeedBackMessage.IsSucceed = true;

            base.End();
            return FeedBackMessage;
        }
        
        
        void Process()
        {
            // here is the core of the meta component ...
            // just a list of Component steps

            MyImageViewer = new cViewerImage();
            MyImageViewer.SetInputData(this.Input);
            MyImageViewer.ListLinearMaxColor = this.ListLinearMaxColor;

            MyImageViewer.IsDisplayScale = this.IsDisplayScale;
            MyImageViewer.IsUseSavedDefaultDisplayProperties = this.IsUseSavedDefaultDisplayProperties;
            MyImageViewer.DefaultZoom = this.DefaultZoom;
            this.Title = this.Input.Name;

            MyImageViewer.Run();

            cDesignerSinglePanel MyDesigner = new cDesignerSinglePanel();
            MyDesigner.SetInputData(MyImageViewer.GetOutPut());
            MyDesigner.Run();

            cDisplayToWindow MyDisplay = new cDisplayToWindow();
            MyDisplay.SetInputData(MyImageViewer.GetOutPut());
            MyDisplay.Title = this.Title;


            MyImageViewer.IP.Resize-= new EventHandler(MyImageViewer.IP.panelForImage_Resize);
            MyDisplay.Run();
            MyImageViewer.IP.Resize += new EventHandler(MyImageViewer.IP.panelForImage_Resize);
            MyDisplay.Display();

        }

        public void SetInputData(cImage Input)
        {
            this.Input = Input;
        }
    }
}

