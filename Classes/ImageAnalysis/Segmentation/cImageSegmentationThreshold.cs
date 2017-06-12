using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Forms.FormsForImages;
using System.Drawing;
using ImageAnalysis;
using Emgu.CV;
using Emgu.CV.CvEnum;
using System.Runtime.InteropServices;
using Emgu.CV.Structure;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes.Base_Classes;

namespace ImageAnalysisFiltering
{
    public partial class cImageSegmentationThreshold : c2DImageFilter
    {
        
        public float MaxValue = 255;
        public bool IsInvert = false;

        public cImageSegmentationThreshold()
        {
            this.Title = "Threshold";

            cProperty Prop1 = new cProperty(new cPropertyType("Threshold", eDataType.DOUBLE), null);
            Prop1.Info = "Intensity threshold";
            Prop1.SetNewValue((double)100);
            base.ListProperties.Add(Prop1);

        }

        public cFeedBackMessage Run()
        {
            // Output = new cImage(Input);
            if (base.Start() == false)
            {
                base.FeedBackMessage.IsSucceed = false;
                return base.FeedBackMessage;
            }


            object _firstValue = base.ListProperties.FindByName("Threshold");
            double Threshold = 0;
            if (_firstValue == null)
            {
                base.GenerateError("Threshold not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                Threshold = (double)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("Threshold cast didn't work");
                return base.FeedBackMessage;
            }

          

            base.Output = new cImage(Input, false);
            //base.Output = new cImage(Input.Width, Input.Height, Input.Depth, base.ListChannelsToBeProcessed.Count);
            for (int IdxChannel = 0; IdxChannel < base.ListChannelsToBeProcessed.Count; IdxChannel++)
            {
                int CurrentChannel = base.ListChannelsToBeProcessed[IdxChannel];

                if (IsInvert)
                {
                    for (int k = 0; k < Input.Depth; k++)
                    for (int j = 0; j < Input.Height; j++)
                        for (int i = 0; i < Input.Width; i++)
                        {
                            if (Input.SingleChannelImage[CurrentChannel].Data[i + j * Input.Width + k*Input.SliceSize] > Threshold)
                                this.Output.SingleChannelImage[IdxChannel].Data[i + j * Input.Width + k * Input.SliceSize] = this.MaxValue;
                            else
                                this.Output.SingleChannelImage[IdxChannel].Data[i + j * Input.Width + k * Input.SliceSize] = 0;
                        }
                }
                else
                {
                    for(int k =0; k<Input.Depth;k++)
                    for (int j = 0; j < Input.Height; j++)
                        for (int i = 0; i < Input.Width; i++)
                        {
                            if (Input.SingleChannelImage[CurrentChannel].Data[i + j * Input.Width + k*Input.SliceSize] < Threshold)
                                this.Output.SingleChannelImage[IdxChannel].Data[i + j * Input.Width + k*Input.SliceSize] = 0;
                            else
                                this.Output.SingleChannelImage[IdxChannel].Data[i + j * Input.Width + k * Input.SliceSize] = this.MaxValue;
                        }
                }
            }
            base.End();
            return FeedBackMessage;
        }
    }
}
