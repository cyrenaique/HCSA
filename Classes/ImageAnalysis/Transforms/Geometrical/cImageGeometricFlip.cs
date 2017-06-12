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
    public partial class cImageGeometricFlip : c2DImageFilter
    {


        public cImageGeometricFlip()
        {
            this.Title = "Flip ";

            cProperty Prop1 = new cProperty(new cPropertyType("Horizontal", eDataType.BOOL), null);
            Prop1.Info = "Horizontal Flip?";
            Prop1.SetNewValue((bool)true);
            base.ListProperties.Add(Prop1);

        }

        public cFeedBackMessage Run()
        {
            if (base.Start() == false)
            {
                base.FeedBackMessage.IsSucceed = false;
                return base.FeedBackMessage;
            }

            object _firstValue = base.ListProperties.FindByName("Horizontal");
            bool IsHorizontal = true;
            if (_firstValue == null)
            {
                base.GenerateError("-Horizontal- not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                IsHorizontal = (bool)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("-Horizontal- cast didn't work");
                return base.FeedBackMessage;
            }

            int i, j, k, cpt;
            float[] inData;
            float[] outData;

            base.Output = new cImage(base.Input, false);

            for (int band = 0; band < base.Input.GetNumChannels(); band++)
            {
                inData = base.Input.SingleChannelImage[band].Data;
                outData = base.Output.SingleChannelImage[band].Data;
                cpt = 0;

                for (k = 0; k < base.Output.Depth; k++)

                    if (IsHorizontal)
                    {
                        for (j = 0; j < base.Output.Height; j++)
                            for (i = 0; i < base.Output.Width; i++, cpt++)
                            {
                                outData[cpt] = inData[base.Output.Width - i - 1 + j * base.Input.Width + k * base.Input.SliceSize];
                            }
                    }
                    else
                    {
                        for (j = 0; j < base.Output.Height; j++)
                            for (i = 0; i < base.Output.Width; i++, cpt++)
                            {
                                outData[cpt] = inData[i + (base.Input.Height - j - 1) * base.Input.Width + k * base.Input.SliceSize];
                            }

                    }
            }


            base.End();
            return FeedBackMessage;
        }

    }
}
