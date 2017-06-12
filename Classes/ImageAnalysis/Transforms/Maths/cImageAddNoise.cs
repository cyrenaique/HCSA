using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Data;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
using ImageAnalysisFiltering;
using ImageAnalysis;

namespace HCSAnalyzer.Classes.Base_Classes
{
    class cImageAddNoise : c2DImageFilter
    {
        public double Min = 0;
        public double Max = 100;

        public double Mean = 0;
        public double Stdev = 10;
        public eRandDistributionType DistributionType = eRandDistributionType.GAUSSIAN;
        public bool IsInPlace = false;

        public cImageAddNoise()
        {
            this.Title = "Add Noise";
        }

        public cFeedBackMessage Run()
        {
            

            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }

            Random RND = new Random();

            if (IsInPlace == false)
                base.Output = new cImage(Input.Width, Input.Height, Input.Depth, base.ListChannelsToBeProcessed.Count);
            else
                base.Output = Input;
            
            
            for (int IdxChannel = 0; IdxChannel < base.ListChannelsToBeProcessed.Count; IdxChannel++)
            {
                int CurrentChannel = base.ListChannelsToBeProcessed[IdxChannel];

                if (this.DistributionType == eRandDistributionType.UNIFORM)
                {
                    for (int i = 0; i < this.Output.ImageSize; i++)
                        this.Output.SingleChannelImage[CurrentChannel].Data[i] = this.Input.SingleChannelImage[CurrentChannel].Data[i] +
                                                                    (float)(RND.NextDouble() * (Max - Min) + Min);
                }
                else if (this.DistributionType == eRandDistributionType.GAUSSIAN)
                {
                    for (int i = 0; i < this.Output.ImageSize; i++)
                    {
                        double u1 = RND.NextDouble();
                        double u2 = RND.NextDouble();
                        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
                        this.Output.SingleChannelImage[CurrentChannel].Data[i] = this.Input.SingleChannelImage[CurrentChannel].Data[i] +
                                                                    (float)(this.Mean + this.Stdev * randStdNormal);
                    }
                }
                else
                {
                    FeedBackMessage.IsSucceed = false;
                    FeedBackMessage.Message = "Distribution Type not implemented";
                }

            }

            return FeedBackMessage;
        }


    }
}
