using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.IO;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    public class cDisplayToClipBoard : cComponent
    {
        //FormForDisplay WindowToDisplay;

        public bool IsModal = false;
        cExtendedControl ControlToDisplay = null;

        public cDisplayToClipBoard()
        {
           // ControlsToDisplay = new List<cExtendedControl>();
        }

        public void SetInputData(cExtendedControl Input)
        {
          //  foreach (var item in Input)
            this.ControlToDisplay = Input;
            //{
            //    ControlsToDisplay.Add(item);
           // }

        }


        //public void SetInputData(cExtendedControl Input)
        //{
        //    ControlsToDisplay.Add(Input);

        //}

        public cFeedBackMessage Run()
        {
            //WindowToDisplay = new FormForDisplay();

            if (ControlToDisplay == null)
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message += ": No input defined!";
                return base.FeedBackMessage;
            }
            return base.FeedBackMessage;
        }

        public void Display()
        {
            ControlToDisplay.CreateGraphics();

         //   ControlToDisplay.Show();

            ControlToDisplay.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
            | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);

            ControlToDisplay.Width = 300;
            ControlToDisplay.Height = 200;

            ControlToDisplay.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
            | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);


            Bitmap bmp = new Bitmap(ControlToDisplay.Width, ControlToDisplay.Height);

            //Drawing control to the bitmap
            ControlToDisplay.DrawToBitmap(bmp, new Rectangle(0, 0, ControlToDisplay.Width, ControlToDisplay.Height));


            MemoryStream ms = new MemoryStream();
           // this.chart.SaveImage(ms, ChartImageFormat.Bmp);
           // Bitmap bm = new Bitmap(ms);
            System.Windows.Forms.Clipboard.SetImage(bmp);

          //  bmp.Save(fileName);
           // bmp.Dispose();


        }
    }
}

