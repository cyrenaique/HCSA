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
using HCSAnalyzer.Classes;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes.Base_Classes;

namespace ImageAnalysisFiltering
{

    public abstract partial class cImageFilterConvolution : c2DImageFilter
    {

    }

    public partial class cImageFilterGaussianConvolution : cImageFilterConvolution
    {

      //  public double StdDev = 3.0;

        public cImageFilterGaussianConvolution()
        {
            this.Title = "Gaussian Convolution";

            cPropertyType PT = new cPropertyType("Kernel Size", eDataType.INTEGER);
            PT.IntType = eIntegerType.ODD;
            cProperty Prop1 = new cProperty(PT, null);
            Prop1.Info = "Gaussian kernel size";
            Prop1.SetNewValue((int)3);
            base.ListProperties.Add(Prop1);
        }

        public cFeedBackMessage Run()
        {
            if (base.Start() == false)
            {
                base.FeedBackMessage.IsSucceed = false;
                return base.FeedBackMessage;
            }

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

            //Matrix<float> Signature1 = new Matrix<float>(this.Count, 2);
            //Matrix<float> Signature2 = new Matrix<float>(CompareTo.Count, 2);

            //for (int Idx = 0; Idx < this.Count; Idx++)
            //{
            //    Signature1[Idx, 0] = (float)this[Idx];
            //    Signature1[Idx, 1] = Idx;

            //    Signature2[Idx, 0] = (float)CompareTo[Idx];
            //    Signature2[Idx, 1] = Idx;
            //}

            //double ResutatEMD;
            //ResutatEMD = CvInvoke.cvCalcEMD2(Signature1.Ptr, Signature2.Ptr, DIST_TYPE.CV_DIST_L1, null, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

            //Emgu.CV.Structure.MCvPoint2D64f

            // IntPtr SrcImage = CvInvoke.cvCreateImage(


            // Matrix<float> Src = new Matrix<float>(input.Data[inputBand].Data.ToArray());

            // Matrix<float> Dst = new Matrix<float>(output.Data[inputBand].Data.ToArray());
            //  CvArray<IPL_DEPTH.IPL_DEPTH_32F> SRC = new 

            //IntPtr Src = CvInvoke.cvCreateImageHeader(new Size(input.Width, input.Height), IPL_DEPTH.IPL_DEPTH_32F, 1);
            //Src =  Marshal.UnsafeAddrOfPinnedArrayElement(input.Data[inputBand].Data, 0);
            //CvInvoke.image

            //ipl_image_p->imageData = my_float_image_data;
            // ipl_image_p->imageDataOrigin = ipl_image_p->imageData;


            base.Output = new cImage(base.Input, false);


            for (int IdxChannel = 0; IdxChannel < base.ListChannelsToBeProcessed.Count; IdxChannel++)
            {
                int CurrentChannel = base.ListChannelsToBeProcessed[IdxChannel];

                Image<Gray, float> inputImage = new Image<Gray, float>(Input.Width, Input.Height);

                //float[,] SrcArray = new float[input.Width, input.Height];
                for (int j = 0; j < Input.Height; j++)
                    for (int i = 0; i < Input.Width; i++)
                    {
                        inputImage.Data[j, i, 0] = Input.SingleChannelImage[CurrentChannel].Data[i + j * Input.Width];
                    }

                Image<Gray, float> ProcessedImage = new Image<Gray, float>(inputImage.Width, inputImage.Height);
                ProcessedImage = inputImage.SmoothGaussian(KernelSize);

                
                
               

                this.Output.SingleChannelImage[IdxChannel].SetNewDataFromOpenCV(ProcessedImage);

                //            CvInvoke.cvSmooth(inputImage.Ptr, smoothedImage.Ptr, SMOOTH_TYPE.CV_MEDIAN, 5, 0, 0, 0);
                //CvInvoke.cvSobel(inputImage.Ptr, smoothedImage.Ptr, 2, 2, 2);

                //CvInvoke.cvSmooth(inputImage.Ptr, smoothedImage.Ptr, SMOOTH_TYPE.CV_GAUSSIAN,13, 13, 1.5, 1);

                //smoothedImage =  inputImage.Sobel(1, 0, 3);
            }
            // float[,] DestArray = new float[output.Width,output.Height];
            //    IntPtr MyintPtrDst = Marshal.UnsafeAddrOfPinnedArrayElement(DestArray, 0);


            //    CvInvoke.cvShowImage("Test", MyintPtr);


            //CvInvoke.cvSmooth(MyintPtrSrc, MyintPtrDst, SMOOTH_TYPE.CV_GAUSSIAN, 0, 0, 3, 0);


            //for (int j = 0; j < input.Height; j++)
            //    for (int i = 0; i < input.Width; i++)
            //    {
            //        output.Data[outputBand].Data[i + j * output.Width] = ;
            //    }



            //  IntPtr Dest = CvInvoke.cvCreateMat(output.Width, output.Height, MAT_DEPTH.CV_32F);

            //Src.Width = input.Width;

            //IntPtr ResImage = CvInvoke.cvCreateImage(new Size(output.Width, output.Height), IPL_DEPTH.IPL_DEPTH_32F, 1);

            //SrcImage =  Marshal.UnsafeAddrOfPinnedArrayElement(input.Data[inputBand].Data, 0);
            //ResImage = Marshal.UnsafeAddrOfPinnedArrayElement(output.Data[outputBand].Data, 0);


            // Perform a Gaussian blur
            //IntPtr widthPtr = new IntPtr();

            // IntPtr inputPtr = Marshal.UnsafeAddrOfPinnedArrayElement(input.Data[inputBand].Data, 0);
            //  IntPtr outputPtr = Marshal.UnsafeAddrOfPinnedArrayElement(output.Data[outputBand].Data, 0);


            // CvInvoke.cvSmooth(inputPtr ,outputPtr, SMOOTH_TYPE.CV_GAUSSIAN,0,0,3,3);
            //cvSmooth( img, out, CV_GAUSSIAN, 11, 11 );

            // Show the processed image
            //CvInvoke.cvShowImage("Example3-out", out);



            //   return ResutatEMD;

            base.End();

            return FeedBackMessage;
        }

    }
}
