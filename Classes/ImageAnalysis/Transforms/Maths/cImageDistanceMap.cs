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

namespace ImageAnalysisFiltering
{
    public partial class cImageDistanceMap : c2DImageFilter
    {
        public Emgu.CV.CvEnum.DistType DistanceType = DistType.L2;
        public int MaskSize = 5;

        public cImageDistanceMap()
        {
            this.Title = "Distance Map";
        }

        public void Run()
        {
            base.Output = new cImage(Input.Width, Input.Height, Input.Depth, base.ListChannelsToBeProcessed.Count);
            for (int IdxChannel = 0; IdxChannel < base.ListChannelsToBeProcessed.Count; IdxChannel++)
            {
                int CurrentChannel = base.ListChannelsToBeProcessed[IdxChannel];

                Image<Gray, float> inputImage = new Image<Gray, float>(Input.Width, Input.Height);

                for (int j = 0; j < Input.Height; j++)
                    for (int i = 0; i < Input.Width; i++)
                        inputImage.Data[j, i, 0] = Input.SingleChannelImage[CurrentChannel].Data[i + j * Input.Width];

                Image<Gray, float> ProcessedImage = new Image<Gray, float>(inputImage.Width, inputImage.Height);


                Emgu.CV.Image<Gray, byte> gray = inputImage.Convert<Gray, byte>();//convert to grayscale
                IntPtr dsti = Emgu.CV.CvInvoke.cvCreateImage(Emgu.CV.CvInvoke.cvGetSize(gray), Emgu.CV.CvEnum.IplDepth.IplDepth32F, 1);
                Emgu.CV.CvInvoke.DistanceTransform(gray, ProcessedImage,null, DistanceType, MaskSize);

                this.Output.SingleChannelImage[IdxChannel].SetNewDataFromOpenCV(ProcessedImage);
            }
            return;
        }

    }
}
