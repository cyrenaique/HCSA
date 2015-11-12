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
    public partial class cImageGeometricBinning : c2DImageFilter
    {


        public cImageGeometricBinning()
        {
            this.Title = "Binning";

            cProperty Prop2 = new cProperty(new cPropertyType("Binning", eDataType.INTEGER),null);
            Prop2.PropertyType.Min = 2;
            Prop2.PropertyType.IntType = eIntegerType.EVEN;
            Prop2.Info = "Define the new image binning.";
            Prop2.SetNewValue((int)2);
            base.ListProperties.Add(Prop2);

        }

        public cFeedBackMessage Run()
        {
            if (base.Start() == false)
            {
                base.FeedBackMessage.IsSucceed = false;
                return base.FeedBackMessage;
            }



            object _firstValue = base.ListProperties.FindByName("Binning");
            int Binning = 2;
            if (_firstValue == null)
            {
                base.GenerateError("-Binning- not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                Binning = (int)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("-Binning- cast didn't work");
                return base.FeedBackMessage;
            }


            {
                int i, j, k, cpt;
                float posX, posY, posZ;
                int minX, minY, minZ;
                float[] inData;
                float[] outData;

                //if(base.Input.Depth == 1)
                //    base.Output = new cImage((int)(base.Input.Width / Binning),
                //                            (int)(base.Input.Height / Binning),
                //                            1, 
                //                            base.Input.GetNumChannels());
                //else
                base.Output = new cImage((int)(base.Input.Width / Binning),
                                        (int)(base.Input.Height / Binning),
                                        (int)(base.Input.Depth / Binning)+1, 
                                        base.Input.GetNumChannels());

                for (int band = 0; band < base.Input.GetNumChannels(); band++)
                {
                    inData = base.Input.SingleChannelImage[band].Data;
                    outData = base.Output.SingleChannelImage[band].Data;
                    cpt = 0;

                    for (k = 0; k < this.Input.Depth; k++)
                        for (j = 0; j < this.Input.Height; j++)
                            for (i = 0; i < this.Input.Width; i++, cpt++)
                            {
                                minX = (int)(i / Binning);
                                minY = (int)(j / Binning);
                                minZ = (int)(k / Binning);

                                outData[minX + minY * base.Output.Width + minZ * base.Output.SliceSize] += inData[i + j * base.Input.Width + k * base.Input.SliceSize];
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

            this.Output.Resolution.X = this.Input.Resolution.X * Binning;
            this.Output.Resolution.Y = this.Input.Resolution.Y * Binning;
            this.Output.Resolution.Z = this.Input.Resolution.Z * Binning;

            base.End();
            return FeedBackMessage;
        }

    }
}
