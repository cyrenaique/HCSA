using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.IO;
using System.Drawing;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    class cDisplayToWindow : cComponent
    {
        FormForDisplay WindowToDisplay;
        public bool IsModal = false;
        cExtendedControl ControlToDisplay = null;

        public cDisplayToWindow()
        {
            ControlToDisplay = new cExtendedControl();
        }

        public void SetInputData(cExtendedControl Input)
        {
            this.ControlToDisplay = Input;
        }

        public cFeedBackMessage Run()
        {
            
            WindowToDisplay = new FormForDisplay();
            

            if ((ControlToDisplay == null)||(ControlToDisplay.Controls.Count==0))
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message += ": No input defined!";
                return base.FeedBackMessage;
            }
            else
            {
                //this.Title = ControlToDisplay.Title;

                ControlToDisplay.Width = WindowToDisplay.Width - 34;
                ControlToDisplay.Height = WindowToDisplay.Height - 50;

                ControlToDisplay.Controls[0].Width = WindowToDisplay.Width - 34;
                ControlToDisplay.Controls[0].Height = WindowToDisplay.Height - 50;

                ControlToDisplay.Location = new System.Drawing.Point(5, 5);

                ControlToDisplay.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                 | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            }
            WindowToDisplay.Controls.Add(ControlToDisplay);


         
            WindowToDisplay.Text = base.Title;
            return base.FeedBackMessage;
        }

        public bool Display()
        {
            if (IsModal)
            {
                if (WindowToDisplay.ShowDialog() == DialogResult.OK) return true;
                else return false;
            }
            else
            {


           //     WindowToDisplay.Visible = false;
                WindowToDisplay.Show();     
                
                
              //  System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(WindowToDisplay.panelForControls.Width, WindowToDisplay.panelForControls.Height);
              //  WindowToDisplay.panelForControls.DrawToBitmap(bmp, WindowToDisplay.panelForControls.ClientRectangle);


                //System.Drawing.Graphics G =  WindowToDisplay.panelForControls.CreateGraphics();
                //System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(WindowToDisplay.panelForControls.Width, WindowToDisplay.panelForControls.Height);

                //WindowToDisplay.panelForControls.DrawToBitmap(bmp,new Rectangle(0,0,WindowToDisplay.panelForControls.Width, WindowToDisplay.panelForControls.Height));

                //Clipboard.SetImage();


                //MemoryStream ms = new MemoryStream();
                //this.SaveImage(ms, ChartImageFormat.Bmp);
                //Bitmap bm = new Bitmap(ms);
               // Clipboard.SetImage(bmp);

            }




            return true;
        }
    }
}
