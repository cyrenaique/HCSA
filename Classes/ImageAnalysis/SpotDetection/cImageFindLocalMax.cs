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
using Emgu.CV.Util;

namespace ImageAnalysisFiltering
{
    public partial class cImageFindLocalMax : c2DImageFilter
    {
        //public double Threshold = 100;
        //public float MaxValue = 255;
        //public bool IsInvert = false;

        double MinVal = 0;
        double MaxVal = 100;

        public cImageFindLocalMax()
        {
            this.Title = "Find Local Maxima";
        }

        public void Run()
        {

                        base.Output = new cImage(Input.Width, Input.Height, Input.Depth, base.ListChannelsToBeProcessed.Count);
                        for (int IdxChannel = 0; IdxChannel < base.ListChannelsToBeProcessed.Count; IdxChannel++)
                        {
                            int CurrentChannel = base.ListChannelsToBeProcessed[IdxChannel];


                            Image<Gray, byte> inputImage = new Image<Gray, byte>(Input.Width, Input.Height);

                            for (int j = 0; j < Input.Height; j++)
                                for (int i = 0; i < Input.Width; i++)
                                    inputImage.Data[j, i, 0] = (byte)Input.SingleChannelImage[CurrentChannel].Data[i + j * Input.Width];

                            //MCvConnectedComp CCFromMeanShift = new MCvConnectedComp();

                            Image<Gray, float> ProcessedImage = new Image<Gray, float>(inputImage.Width, inputImage.Height);

                            Point MinPtLoc = new Point();
                            Point MaxPtLoc = new Point();

                            // IntPtr contour1 = new IntPtr();
                            //  IntPtr storage = CvInvoke.cvCreateMemStorage(0);

                            Emgu.CV.CvInvoke.cvMinMaxLoc(inputImage.Ptr, ref MinVal, ref MaxVal, ref MinPtLoc, ref MaxPtLoc, IntPtr.Zero);

                            //    Emgu.CV.CvInvoke.cvGetSubRect(
                            Emgu.CV.CvInvoke.cvDFT(inputImage.Ptr, ProcessedImage.Ptr, CV_DXT.CV_DXT_FORWARD, 0);
                        }
            //IntPtr contour1 = new IntPtr();
            //IntPtr storage = CvInvoke.cvCreateMemStorage(0);
            
            //Emgu.CV.CvInvoke.cvFindContours(inputImage, storage, ref contour1, StructSize.MCvContour, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_CCOMP, Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, new Point(0, 0));

            //Seq<Point> contour = new Seq<Point>(contour1, null);


            //this.output = new cImage(this.input);

            //int IdxContour = 0;
            //for (; contour != null && contour.Ptr.ToInt32() != 0; contour = contour.HNext)
            //{
            //    Rectangle TmpBB = contour.BoundingRectangle;
            //    this.output.Data[0].DrawRectange(TmpBB, IdxContour++);
            //}


            //this.output = new cImage(smoothedImage);
           

            //Image<Gray, float> smoothedImage = new Image<Gray, float>(inputImage.Width, inputImage.Height);

            //  smoothedImage = inputImage.Erode(this.Iterations);

            //get picturebox image
            // Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img = new Image<Emgu.CV.Structure.Bgr, byte>((Bitmap)pictureBox1.Image).Resize(500, 500, true);
            //Emgu.CV.Image<Gray, byte> gray = inputImage.Convert<Gray, byte>();//convert to grayscale
            //           Emgu.CV.Image<Gray, byte> binary = gray.ThresholdBinary(new Gray(100), new Gray(255));//perform binarization
            //create pointer
            //IntPtr dsti = Emgu.CV.CvInvoke.cvCreateImage(Emgu.CV.CvInvoke.cvGetSize(gray), Emgu.CV.CvEnum.IPL_DEPTH.IPL_DEPTH_32F, 1);
            //Emgu.CV.CvInvoke.cvDistTransform(gray, smoothedImage, DistanceType, MaskSize, null, IntPtr.Zero);

            //   CvInvoke.cvLaplace(smoothedImage, smoothedImage, 7);
            //   CvInvoke.cvThreshold(smoothedImage, smoothedImage, 3, 255, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY);

           return;
        }

    }
}
