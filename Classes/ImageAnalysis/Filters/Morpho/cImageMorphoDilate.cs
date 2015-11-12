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
    public partial class cImageMorphoDilate : c2DImageFilter
    {
        public int Iterations = 3;

        public cImageMorphoDilate()
        {
            this.Title = "Dilate";
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

                ProcessedImage = inputImage.Dilate(this.Iterations);
                this.Output.SingleChannelImage[IdxChannel].SetNewDataFromOpenCV(ProcessedImage);
            }

            return;
        }

    }
}
