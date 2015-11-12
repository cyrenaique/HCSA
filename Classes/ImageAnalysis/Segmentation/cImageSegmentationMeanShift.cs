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
    public partial class cImageSegmentationMeanShift : c2DImageFilter
    {
        public double SpatialRadius = 9;

        public cImageSegmentationMeanShift()
        {
            this.Title = "Mean Shift";
        }

        public void Run()
        {
            if (base.IsFull3DImage)
                base.Output = new cImage(Input.Width, Input.Height, Input.Depth, base.ListChannelsToBeProcessed.Count);
            else
                base.Output = new cImage(Input.Width, Input.Height, 1, base.ListChannelsToBeProcessed.Count);

            for (int IdxChannel = 0; IdxChannel < base.ListChannelsToBeProcessed.Count; IdxChannel++)
            {
                int CurrentChannel = base.ListChannelsToBeProcessed[IdxChannel];

                Image<Emgu.CV.Structure.Rgb, byte> inputImage = new Image<Rgb, byte>(Input.Width, Input.Height);

                if (base.IsFull3DImage)
                {

                }
                else
                {
                    for (int j = 0; j < Input.Height; j++)
                        for (int i = 0; i < Input.Width; i++)
                            inputImage.Data[j, i, 0] = (byte)Input.SingleChannelImage[CurrentChannel].Data[i + j * Input.Width + base.SliceIndex * Input.SliceSize];

                    Image<Emgu.CV.Structure.Rgb, byte> ProcessedImage = new Image<Rgb, byte>(inputImage.Width, inputImage.Height);

                    Emgu.CV.CvInvoke.cvPyrMeanShiftFiltering(inputImage, ProcessedImage, SpatialRadius, 9, 1, new MCvTermCriteria(5, 1));

                    for (int j = 0; j < Input.Height; j++)
                        for (int i = 0; i < Input.Width; i++)
                            this.Output.SingleChannelImage[IdxChannel].Data[i + j * Input.Width] = ProcessedImage.Data[j, i,0];
                }

            }

            return;
        }


        //public void Run()
        //{
        //    Image<Gray, float> inputImage = new Image<Gray, float>(Input.Width, Input.Height);

        //    for (int j = 0; j < Input.Height; j++)
        //        for (int i = 0; i < Input.Width; i++)
        //            inputImage.Data[j, i, 0] = Input.SingleChannelImage[0].Data[i + j * Input.Width];


        //     Image<Gray, float> ProcessedImage = new Image<Gray, float>(inputImage.Width, inputImage.Height);


        //     Emgu.CV.CvInvoke.cvPyrMeanShiftFiltering(inputImage, ProcessedImage, 3, 3, 1, new MCvTermCriteria(5, 1));


        //     this.Output.SingleChannelImage[IdxChannel].SetNewDataFromOpenCV(ProcessedImage);


        //  //  MCvConnectedComp CCFromMeanShift = new MCvConnectedComp();

        //  //  Emgu.CV.CvInvoke.cvMeanShift(inputImage, new Rectangle(0, 0,3, 3), new MCvTermCriteria(100, 0.1), out CCFromMeanShift);

        //   // return;

        //    //Image<Gray, float> smoothedImage = new Image<Gray, float>(inputImage.Width, inputImage.Height);

        //    //  smoothedImage = inputImage.Erode(this.Iterations);

        //    //get picturebox image
        //    // Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img = new Image<Emgu.CV.Structure.Bgr, byte>((Bitmap)pictureBox1.Image).Resize(500, 500, true);
        //    //Emgu.CV.Image<Gray, byte> gray = inputImage.Convert<Gray, byte>();//convert to grayscale
        //    //           Emgu.CV.Image<Gray, byte> binary = gray.ThresholdBinary(new Gray(100), new Gray(255));//perform binarization
        //    //create pointer
        //    //IntPtr dsti = Emgu.CV.CvInvoke.cvCreateImage(Emgu.CV.CvInvoke.cvGetSize(gray), Emgu.CV.CvEnum.IPL_DEPTH.IPL_DEPTH_32F, 1);
        //    //Emgu.CV.CvInvoke.cvDistTransform(gray, smoothedImage, DistanceType, MaskSize, null, IntPtr.Zero);

        //    //   CvInvoke.cvLaplace(smoothedImage, smoothedImage, 7);
        //    //   CvInvoke.cvThreshold(smoothedImage, smoothedImage, 3, 255, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY);

        // //  return;
        //}

    }
}
