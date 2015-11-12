using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Forms.FormsForImages;
using System.Drawing;
using System.Windows.Forms;
using HCSAnalyzer.ObjectForNotations;
using ImageAnalysis;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Classes._3D;

namespace HCSAnalyzer
{
    //public class cImageViewer : FormForImageDisplay
    //{
    //   // public Timer timerForDisplay;
    //  //  private System.ComponentModel.IContainer components;

    //    //cLUT ListLUT = new cLUT();

    //    public Point ConstraintImageSize = new Point(-1,-1);

    //    public void Display(cGlobalInfo GlobalInfo)
    //    {
    //    //    this.Paint += new System.Windows.Forms.PaintEventHandler(this.panelForImage_Paint);
    //      //  this.Scroll += new ScrollEventHandler(cImageViewer_Scroll);
    //      //  this.MouseMove += new MouseEventHandler(panelForImage_MouseMove);
    //        //this.timerForDisplay = new Timer();
    //        //this.timerForDisplay.Tick += new EventHandler(timerForDisplay_Tick);
    //        //this.timerForDisplay.Interval = (1000) * (1);              // Timer will tick evert second
    //        //this.timerForDisplay.Enabled = true;                       // Enable the timer
    //        //this.timerForDisplay.Start();                              // Start the timer
    //       // base.panelForImage.Width = AssociatedImage.Width;
    //       // base.panelForImage.Height = AssociatedImage.Height;
    //        this.GlobalInfo = GlobalInfo;

    //        if ((ConstraintImageSize.X < 0) || (ConstraintImageSize.Y < 0))
    //        {
    //            this.Width = AssociatedImage.Width + 40;
    //            this.Height = AssociatedImage.Height + 60;
    //            //this.panelForImage.Width = AssociatedImage.Width;
    //            //this.panelForImage.Height = AssociatedImage.Height;
    //        }
    //        else
    //        {
    //            this.Width = this.ConstraintImageSize.X + 40;
    //            this.Height = this.ConstraintImageSize.Y + 60;
    //            //this.panelForImage.Width = AssociatedImage.Width;
    //            //this.panelForImage.Height = AssociatedImage.Height;
    //        }
            
            
    //        this.ViewDimX = AssociatedImage.Width;
    //        this.ViewDimY = AssociatedImage.Height;
           
    //        this.Show();   
            

    //    }
            
    //    public void Display()
    //    {
    //    //    this.Paint += new System.Windows.Forms.PaintEventHandler(this.panelForImage_Paint);
    //      //  this.Scroll += new ScrollEventHandler(cImageViewer_Scroll);
    //      //  this.MouseMove += new MouseEventHandler(panelForImage_MouseMove);
    //        //this.timerForDisplay = new Timer();
    //        //this.timerForDisplay.Tick += new EventHandler(timerForDisplay_Tick);
    //        //this.timerForDisplay.Interval = (1000) * (1);              // Timer will tick evert second
    //        //this.timerForDisplay.Enabled = true;                       // Enable the timer
    //        //this.timerForDisplay.Start();                              // Start the timer
    //       // base.panelForImage.Width = AssociatedImage.Width;
    //       // base.panelForImage.Height = AssociatedImage.Height;
           
    //        //this.Width = AssociatedImage.Width + 40;
    //        //this.Height = AssociatedImage.Height + statusStripForImageViewer.Height + 60;
    //        //this.panelForImage.Width = AssociatedImage.Width;
    //        //this.panelForImage.Height = AssociatedImage.Height;
    //        //this.ViewDimX = AssociatedImage.Width;
    //        //this.ViewDimY = AssociatedImage.Height;


    //        if ((ConstraintImageSize.X < 0) || (ConstraintImageSize.Y < 0))
    //        {
    //            this.Width = AssociatedImage.Width + 40;
    //            this.Height = AssociatedImage.Height +  60;
    //            //this.panelForImage.Width = AssociatedImage.Width;
    //            //this.panelForImage.Height = AssociatedImage.Height;
    //        }
    //        else
    //        {
    //            this.Width = this.ConstraintImageSize.X + 40;
    //            this.Height = this.ConstraintImageSize.Y + 60;
    //            //this.panelForImage.Width = AssociatedImage.Width;
    //            //this.panelForImage.Height = AssociatedImage.Height;
    //        }

    //        this.ViewDimX = AssociatedImage.Width;
    //        this.ViewDimY = AssociatedImage.Height;
           
            

            
    //        //base.numericUpDownStripItem1.Text = "100";
    //        this.Show();   
            

    //    }
       
    //    public void SetImage(cImage Image)
    //    {
    //        Image.AssociatedImageViewer = this;

    //        this.Unsafe_SetAssociatedImage(Image);
    //        this.Text = Image.Name + " - [" + Image.Width + "x" + Image.Height + "x" + Image.Depth + "]" + " - " + Image.GetNumChannels() + " channels"; ;

    //        this.LUTManager = new FormForLUTManager(this);
    //        this.ZNavigator = new Classes.ImageAnalysis.FormsForImages.FormForNavigator(this);


    //         //List<byte[][]> ListLUTs = new List<byte[][]>();
    //         //   cLUT LUT = new cLUT();

    //         //   if (Image.GetNumChannels() == 1)
    //         //   {
    //         //       ListLUTs.Add(LUT.LUT_LINEAR);
    //         //   }
    //         //   else if (Image.GetNumChannels() == 2)
    //         //   {
    //         //       ListLUTs.Add(LUT.LUT_LINEAR_RED);
    //         //       ListLUTs.Add(LUT.LUT_LINEAR_GREEN);
    //         //   }
    //         //   else if (Image.GetNumChannels() == 3)
    //         //   {
    //         //       ListLUTs.Add(LUT.LUT_LINEAR_RED);
    //         //       ListLUTs.Add(LUT.LUT_LINEAR_GREEN);
    //         //       ListLUTs.Add(LUT.LUT_LINEAR_BLUE);
    //         //   }
    //         //   else
    //         //   {
    //         //       ListLUTs.Add(LUT.LUT_LINEAR_RED);
    //         //       ListLUTs.Add(LUT.LUT_LINEAR_GREEN);
    //         //       ListLUTs.Add(LUT.LUT_LINEAR_BLUE);
    //         //       for (int i = 0; i < Image.GetNumChannels() - 3; i++)
    //         //       {
    //         //           ListLUTs.Add(LUT.LUT_LINEAR);
    //         //       }
    //         //   }
           






    //        for (int IdxLUT = 0; IdxLUT < this.AssociatedImage.GetNumChannels(); IdxLUT++)
    //        {
    //            if (Image.SingleChannelImage[IdxLUT].Name == "Single Channel Image")
    //                Image.SingleChannelImage[IdxLUT].Name = "Channel " + IdxLUT;
    //            UserControlSingleLUT SingleLUT = new UserControlSingleLUT(this, Image.SingleChannelImage[IdxLUT], IdxLUT);
    //            SingleLUT.textBoxForName.Text = Image.SingleChannelImage[IdxLUT].Name;
    //            SingleLUT.Location = new Point(0, IdxLUT * SingleLUT.Height);
    //           // SingleLUT.SelectedLUT = ListLUTs[IdxLUT];
    //            this.LUTManager.panelForLUTS.Controls.Add(SingleLUT);
    //        }

    //    }

    //    public void AddNotation(cObjectForAnnotation ObjectForNotation, string Name)
    //    {
    //        //this.ListObjectForNotations.Add(ObjectForNotation);
            
    //      //  this.DictionaryForNotations.
    //        this.DictionaryForNotations.Add(Name,ObjectForNotation);

    //    }

    //    public void RemoveNotation(string Name)
    //    {
    //        //this.ListObjectForNotations.Add(ObjectForNotation);
    //        this.DictionaryForNotations.Remove(Name);
    //    }

    //    private void InitializeComponent()
    //    {
    //        this.SuspendLayout();
    //        // 
    //        // panelForImage
    //        // 
    //        //this.panelForImage.Size = new System.Drawing.Size(657, 393);
    //        // 
    //        // panelForInfo
    //        // 
    //        //this.panelForInfo.Size = new System.Drawing.Size(657, 96);
    //        // 
    //        // cImageViewer
    //        // 
    //        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    //        this.ClientSize = new System.Drawing.Size(681, 519);
    //        this.Name = "cImageViewer";
    //        this.ResumeLayout(false);

    //    }

    //    //void timerForDisplay_Tick(object sender, EventArgs e)
    //    //{
    //    //    this.Text = DateTime.Now.ToString();

    //    //    //foreach (cObjectForAnnotation TmpObj in ListObjectForNotations)
    //    //    //{

    //    //    //    if (TmpObj.GetType() == typeof(cDisk))
    //    //    //    {
    //    //    //        //TmpObj.PosX++;
    //    //    //        //ThisGraph.FillEllipse(new SolidBrush(((cDisk)TmpObj).ObjectColor), ((cDisk)TmpObj).PosX, ((cDisk)TmpObj).PosY, ((cDisk)TmpObj).Size / 2, ((cDisk)TmpObj).Size / 2);
    //    //    //    }
    //    //    //}
        
    //    //    DrawLayers();


    //    //}

    //    //private void InitializeComponent()
    //    //{
    //    //    this.components = new System.ComponentModel.Container();
    //    //    this.timerForDisplay = new System.Windows.Forms.Timer(this.components);
    //    //    this.SuspendLayout();
    //    //    // 
    //    //    // timerForDisplay
    //    //    // 
    //    //    this.timerForDisplay.Enabled = true;
    //    //    this.timerForDisplay.Tick += new System.EventHandler(this.timerForDisplay_Tick);
    //    //    // 
    //    //    // cImageViewer
    //    //    // 
    //    //    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    //    //    this.ClientSize = new System.Drawing.Size(604, 366);
    //    //    this.Name = "cImageViewer";
    //    //    this.ResumeLayout(false);

    //    //}
    //}

}

namespace HCSAnalyzer.ObjectForNotations
{
    public abstract class cObjectForAnnotation
    {
        public cPoint3D Pos;
        public bool OnlyOnImage = true;
        public cPoint3D Size;
        public Color ObjectColor;
        
    }

    public class cString : cObjectForAnnotation
    {
        public string Text;

        public cString(string Text, Point Position, Color Color, float Size)
        {
            this.ObjectColor = Color;
            this.Text = Text;
            this.Pos = new cPoint3D(Position.X, Position.Y, 0);
            this.Size = new cPoint3D(Size, Size, 0);
        }
    }

    public class cDisk : cObjectForAnnotation
    {
        public cDisk(Point Position, Color Color, float Radius)
        {
            this.ObjectColor = Color;
            this.Pos = new cPoint3D(Position.X,Position.Y,0);
            this.Size = new cPoint3D(Radius, Radius, 0);
            
        }
    }


    public class cSquare : cObjectForAnnotation
    {
        public cSquare(Point PositionMin, Point PositionMax, Color Color)
        {
            this.ObjectColor = Color;
            this.Pos = new cPoint3D(PositionMin.X, PositionMin.Y, 0);
            this.Size = new cPoint3D(PositionMax.X - PositionMin.X, PositionMax.Y - PositionMin.Y,0);
        }
    }

    public class cLine : cObjectForAnnotation
    {
        public cLine(Point Start, Point End, Color Color)
        {
            this.ObjectColor = Color;
            this.Pos = new cPoint3D(Start.X, Start.Y, 0);
            this.Size = new cPoint3D(End.X, End.Y, 0);
        }
    }

}
