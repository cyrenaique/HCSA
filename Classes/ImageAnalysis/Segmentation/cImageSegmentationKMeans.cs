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
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.General_Types;

namespace ImageAnalysisFiltering
{
    public partial class cImageSegmentationKMeans : c2DImageFilter
    {
        //public int ClusterCount = 2;
        public int Attempts = 2;
        public int MaxIterations = 100;
        public double Eps = 0.5;

        public cImageSegmentationKMeans()
        {
            this.Title = "K-Means";


            cProperty Prop1 = new cProperty(new cPropertyType("Number of clusters", eDataType.INTEGER), null);
            Prop1.Info = "Number of cluster(s)";
            Prop1.SetNewValue((int)2);
            base.ListProperties.Add(Prop1);


        }

        public cFeedBackMessage Run()
        {
            if (base.Start() == false)
            {
                base.FeedBackMessage.IsSucceed = false;
                return base.FeedBackMessage;
            }


            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data image defined.";
                return FeedBackMessage;
            }

            object _firstValue = base.ListProperties.FindByName("Number of clusters");
            int ClusterCount = 0;
            if (_firstValue == null)
            {
                base.GenerateError("Number of clusters not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                ClusterCount = (int)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("Number of clusters cast didn't work");
                return base.FeedBackMessage;
            }


            Output = new cImage(Input.Width,Input.Height,Input.Depth, base.ListChannelsToBeProcessed.Count);

            for (int IdxChannel = 0; IdxChannel < base.ListChannelsToBeProcessed.Count; IdxChannel++)
            {
                int CurrentChannel = base.ListChannelsToBeProcessed[IdxChannel];

                Matrix<float> samples = new Matrix<float>(Input.Width * Input.Height, 1, 1);
                Matrix<int> finalClusters = new Matrix<int>(Input.Width * Input.Height, 1);

                for (int y = 0; y < Input.Height; y++)
                {
                    for (int x = 0; x < Input.Width; x++)
                    {
                        samples.Data[y + x * Input.Height, 0] = (float)Input.SingleChannelImage[CurrentChannel].Data[x + y * Input.Width];
                    }
                }

                MCvTermCriteria term = new MCvTermCriteria(MaxIterations, Eps);
                term.type = TERMCRIT.CV_TERMCRIT_ITER | TERMCRIT.CV_TERMCRIT_EPS;

                Matrix<Single> centers = new Matrix<Single>(ClusterCount, Input.Width * Input.Height);
                CvInvoke.cvKMeans2(samples, ClusterCount, finalClusters, term, Attempts, IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero);

                for (int j = 0; j < Input.Height; j++)
                    for (int i = 0; i < Input.Width; i++)
                        this.Output.SingleChannelImage[IdxChannel].Data[i + j * Input.Width] = finalClusters[i * this.Input.Height + j, 0];
            }
            base.End();
            
            return FeedBackMessage;
        }
    }
}
