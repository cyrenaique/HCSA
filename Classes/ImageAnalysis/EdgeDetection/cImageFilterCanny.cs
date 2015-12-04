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
    public partial class cImageFilterCanny : c2DImageFilter
    {
        public double GreyThreshold = 10;
        public double GreyThresholdLinkin = 60;
        //public int Aperture = 3;

        public cImageFilterCanny()
        {
            this.Title = "Canny Filtering";
        }

        public void Run()
        {
            base.Start();

            base.Output = new cImage(base.Input, false);

            for (int IdxChannel = 0; IdxChannel < base.ListChannelsToBeProcessed.Count; IdxChannel++)
            {
                int CurrentChannel = base.ListChannelsToBeProcessed[IdxChannel];

                Image<Gray, float> inputImage = new Image<Gray, float>(Input.Width, Input.Height);

                for (int j = 0; j < Input.Height; j++)
                    for (int i = 0; i < Input.Width; i++)
                        inputImage.Data[j, i, 0] = Input.SingleChannelImage[CurrentChannel].Data[i + j * Input.Width];

                Image<Gray, byte> ProcessedImage = new Image<Gray, byte>(inputImage.Width, inputImage.Height);
                ProcessedImage = inputImage.Canny(GreyThreshold, GreyThresholdLinkin);

                this.Output.SingleChannelImage[IdxChannel].SetNewDataFromOpenCV(ProcessedImage);
            }

            base.End();
            
            return;
        }

    }
}
