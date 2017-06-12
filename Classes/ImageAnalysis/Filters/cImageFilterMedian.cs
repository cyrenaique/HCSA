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
    public partial class cImageFilterMedian : c2DImageFilter
    {


        public cImageFilterMedian()
        {
            this.Title = "Median Filtering";

            cProperty Prop1 = new cProperty(new cPropertyType("Kernel Size", eDataType.INTEGER), null);
            Prop1.Info = "Median Kernel size";
            Prop1.SetNewValue((int)3);
            base.ListProperties.Add(Prop1);

        }

        public cFeedBackMessage Run()
        {
            base.Start();

            if(base.IsFull3DImage)
                base.Output = new cImage(base.Input, false);
            else
                base.Output = new cImage(Input.Width, Input.Height, 1, base.ListChannelsToBeProcessed.Count);


            object _firstValue = base.ListProperties.FindByName("Kernel Size");
            int KernelSize = 0;
            if (_firstValue == null)
            {
                base.GenerateError("Kernel Size not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                KernelSize = (int)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("Kernel Size cast didn't work");
                return base.FeedBackMessage;
            }

            for (int IdxChannel = 0; IdxChannel < base.ListChannelsToBeProcessed.Count; IdxChannel++)
            {
                int CurrentChannel = base.ListChannelsToBeProcessed[IdxChannel];

                Image<Gray, float> inputImage = new Image<Gray, float>(Input.Width, Input.Height);

                if (base.IsFull3DImage)
                {
                   
                }
                else
                {
                    for (int j = 0; j < Input.Height; j++)
                        for (int i = 0; i < Input.Width; i++)
                            inputImage.Data[j, i, 0] = Input.SingleChannelImage[CurrentChannel].Data[i + j * Input.Width + base.SliceIndex* Input.SliceSize]; 
                    
                    Image<Gray, float> ProcessedImage = new Image<Gray, float>(inputImage.Width, inputImage.Height);
                    ProcessedImage = inputImage.SmoothMedian(KernelSize);
                    this.Output.SingleChannelImage[IdxChannel].SetNewDataFromOpenCV(ProcessedImage);
                }

            }


            base.End();
            return FeedBackMessage;
        }

    }
}
