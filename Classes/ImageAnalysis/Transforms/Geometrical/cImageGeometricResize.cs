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
    public partial class cImageGeometricResize : c2DImageFilter
    {
        public Inter InterpolationType = Inter.Nearest;
     //   public double Scale = 0.5;

        public cImageGeometricResize()
        {
            this.Title = "Resize ";

            cProperty Prop1 = new cProperty(new cPropertyType("Scale", eDataType.DOUBLE), null);
            Prop1.Info = "Resizing scale";
            Prop1.SetNewValue((double)0.5);
            base.ListProperties.Add(Prop1);

            cProperty Prop2 = new cProperty(new cPropertyType("Maximum Width", eDataType.INTEGER),null);
            Prop2.PropertyType.Min = 0;
            Prop2.Info = "Define the maximum width of the image. If set to 0, then unlimited";
            Prop2.SetNewValue((int)0);
            base.ListProperties.Add(Prop2);

        }

        public cFeedBackMessage Run()
        {
            if (base.Start() == false)
            {
                base.FeedBackMessage.IsSucceed = false;
                return base.FeedBackMessage;
            }

            object _firstValue = base.ListProperties.FindByName("Scale");
            double Scale = 0;
            if (_firstValue == null)
            {
                base.GenerateError("-Scale- not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                Scale = (double)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("-Scale- cast didn't work");
                return base.FeedBackMessage;
            }

            _firstValue = base.ListProperties.FindByName("Maximum Width");
            int MaxWidth = 0;
            if (_firstValue == null)
            {
                base.GenerateError("-Maximum Width- not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                MaxWidth = (int)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("-Maximum Width- cast didn't work");
                return base.FeedBackMessage;
            }

            if (this.Input.Depth == 1)
            {
                int RequestedWith = (int)(this.Input.Width * Scale);
                
                // compute the new scale
                if ((MaxWidth!=0) && (RequestedWith > MaxWidth))
                {
                    Scale = (double)MaxWidth / (double)this.Input.Width;
                }


                for (int IdxChannel = 0; IdxChannel < base.ListChannelsToBeProcessed.Count; IdxChannel++)
                {
                    int CurrentChannel = base.ListChannelsToBeProcessed[IdxChannel];
                    Image<Gray, float> inputImage = new Image<Gray, float>(Input.Width, Input.Height);

                    for (int j = 0; j < Input.Height; j++)
                        for (int i = 0; i < Input.Width; i++)
                            inputImage.Data[j, i, 0] = Input.SingleChannelImage[CurrentChannel].Data[i + j * Input.Width + base.SliceIndex * Input.SliceSize];

                    Image<Gray, float> ProcessedImage = new Image<Gray, float>(inputImage.Width, inputImage.Height);

                    ProcessedImage = inputImage.Resize(Scale, this.InterpolationType);

                    if (base.Output == null)
                        base.Output = new cImage(ProcessedImage.Width, ProcessedImage.Height, 1, base.ListChannelsToBeProcessed.Count);

                    this.Output.SingleChannelImage[IdxChannel].SetNewDataFromOpenCV(ProcessedImage);
                     
                }
            }
            else
            {
                int i, j, k, cpt;
                float posX, posY, posZ;
                int minX, minY, minZ;
                float[] inData;
                float[] outData;

                base.Output = new cImage((int)(base.Input.Width * Scale),
                                        (int)(base.Input.Height * Scale),
                                        (int)(base.Input.Depth * Scale), base.Input.GetNumChannels());

                for (int band = 0; band < base.Input.GetNumChannels(); band++)
                {
                    inData = base.Input.SingleChannelImage[band].Data;
                    outData = base.Output.SingleChannelImage[band].Data;
                    cpt = 0;

                    for (k = 0; k < base.Output.Depth; k++)
                        for (j = 0; j < base.Output.Height; j++)
                            for (i = 0; i < base.Output.Width; i++, cpt++)
                            {
                                posX = (float)(i / Scale);
                                posY = (float)(j / Scale);
                                posZ = (float)(k / Scale);


                                minX = (int)Math.Floor(posX);
                                minY = (int)Math.Floor(posY);
                                minZ = (int)Math.Floor(posZ);

                                outData[cpt] = inData[minX + minY *base.Input.Width + minZ * base.Input.SliceSize];
                            }
                }
            
            }

            for (int i = 0; i < this.Input.GetNumChannels(); i++)
            {
                this.Output.SingleChannelImage[i].Name = this.Input.SingleChannelImage[i].Name;
                this.Output.SingleChannelImage[i].Resolution = new HCSAnalyzer.Classes._3D.cPoint3D(this.Input.SingleChannelImage[i].Resolution.X,
                    this.Input.SingleChannelImage[i].Resolution.Y,
                    this.Input.SingleChannelImage[i].Resolution.Z);
            }

            this.Output.Resolution.X = this.Input.Resolution.X / Scale;
            this.Output.Resolution.Y = this.Input.Resolution.Y / Scale;
            this.Output.Resolution.Z = this.Input.Resolution.Z / Scale;

            base.End();
            return FeedBackMessage;
        }

    }
}
