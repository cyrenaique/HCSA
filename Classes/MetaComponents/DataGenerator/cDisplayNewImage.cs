using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes.Viewers._1D;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using HCSAnalyzer.Classes.General_Types;
using ImageAnalysis;

namespace HCSAnalyzer.Classes.MetaComponents
{
    class cDisplayNewImage : cComponent
    {

        public cDisplayNewImage()
        {
            this.Title = "Generate Image";

            cProperty PropWidth = new cProperty(new cPropertyType("Width", eDataType.INTEGER), null);
            PropWidth.Info = "Image Width";
            PropWidth.SetNewValue((int)256);
            base.ListProperties.Add(PropWidth);

            cProperty PropHeight = new cProperty(new cPropertyType("Height", eDataType.INTEGER), null);
            PropHeight.Info = "Image Height";
            PropHeight.SetNewValue((int)256);
            base.ListProperties.Add(PropHeight);

            cProperty PropDepth = new cProperty(new cPropertyType("Depth", eDataType.INTEGER), null);
            PropDepth.Info = "Image Depth";
            PropDepth.SetNewValue((int)1);
            base.ListProperties.Add(PropDepth);

        }

        public cFeedBackMessage Run()
        {
            base.Start();

            object _firstValue = base.ListProperties.FindByName("Width");
            int Width = 0;
            if (_firstValue == null)
            {
                base.GenerateError("-Width- not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                Width = (int)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("-Width- Size cast didn't work");
                return base.FeedBackMessage;
            }

            _firstValue = base.ListProperties.FindByName("Height");
            int Height = 0;
            if (_firstValue == null)
            {
                base.GenerateError("-Height- not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                Height = (int)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("-Height- Size cast didn't work");
                return base.FeedBackMessage;
            }

            _firstValue = base.ListProperties.FindByName("Depth");
            int Depth = 0;
            if (_firstValue == null)
            {
                base.GenerateError("-Depth- not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                Depth = (int)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("-Depth- Size cast didn't work");
                return base.FeedBackMessage;
            }

            cImage NewImage = new cImage(Width, Height, Depth, 1);
            NewImage.Name = "Image [" + Width + ", " + Height + ", " + Depth + "]"; 

            cDisplaySingleImage DSI = new cDisplaySingleImage();
            DSI.SetInputData(NewImage);
            DSI.Run();


            base.End();
            return FeedBackMessage;

        }

    }
}

