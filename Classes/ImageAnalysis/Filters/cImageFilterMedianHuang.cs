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
    public partial class ImageFilterMedianHuang : c2DImageFilter
    {
        float Min(float[] input)
        {
            float min = float.MaxValue;
            foreach (float f in input) if (f < min) min = f;
            return min;
        }

        float Max(float[] input)
        {
            float max = float.MinValue;
            foreach (float f in input) if (f > max) max = f;
            return max;
        }

        float[] Rescale(float[] input, float[] output, float minBound, float maxBound)
        {
            float min = Min(input), max = Max(input);
            if (min == max)
            {
                for (int i = 0; i < input.Length; i++) output[i] = minBound;

                //Console.WriteLine("Rescale: input is constant and has been set to " + minBound);
            }
            else
                for (int i = 0; i < input.Length; i++)
                    output[i] = ((maxBound - minBound) * (input[i] - min) / (max - min)) + minBound;

            return output;
        }

        cImage Rescale(cImage input, int inputChannel, cImage output, int outputChannel, float minBound, float maxBound)
        {
            float min = input.SingleChannelImage[inputChannel].Data.Min();
            float max = input.SingleChannelImage[inputChannel].Data.Max();
            if (min == max)
            {
                for (int i = 0; i < input.Height*input.Width; i++) output.SingleChannelImage[outputChannel].Data[i] = minBound;
            }
            else
                for (int i = 0; i < input.Width*input.Height; i++)
                    output.SingleChannelImage[outputChannel].Data[i] = ((maxBound - minBound) * (input.SingleChannelImage[inputChannel].Data[i] - min) / (max - min)) + minBound;


            return output;
        }

    /// <summary> Rescales each band of the given image to [minBound,maxBound] 
    /// </summary>
    /// <param name="input">the input image</param>
    /// <param name="output">the output image</param>
    /// <param name="minBound">the final minimum value</param>
    /// <param name="maxBound">the final maximum value</param>
          //private cImage Rescale(cImage input, cImage output, float minBound, float maxBound)
          //{
          //    for (int band = 0; band < input.NumChannels; band++) Rescale(input.Data[band], output.Data[band], minBound, maxBound);

          //}

 
        public int Radius = 3;
        public int HistoSize = 256;
        public bool IsSliceBySliceRescaling = false;
      



        ///<summary>
        ///Median filter by Huang's method (C=O(r))
        ///!!! the image will be discretized according to HistoSize !!!
        ///</summary>
        ///<param name="input">input image</param>
        ///<param name="inputBand">input channel</param>
        ///<param name="output">output image</param>
        ///<param name="outputBand">output channel</param>
        ///<param name="radius">filter radius</param>
        ///<param name="HistoSize">Histogram number of bins</param>
        ///<param name="IsSliceBySliceRescaling">false : 3D rescaling; true : slice by slice rescaling</param>
        public ImageFilterMedianHuang()
        {
            this.Title = "Huang Median Filtering";
        }





        public void Run()
        {
            // first we have to convert the image with the right number of color
            cImage RescaledInput = new cImage(Input.Width, Input.Height, Input.Depth, 1);
            int[] Histo = new int[HistoSize];

        //    Rescale(Input, inputBand, RescaledInput, 0, 0, HistoSize - 1);

            float[] inp = RescaledInput.SingleChannelImage[0].Data;
            float[] outp = Output.SingleChannelImage[outputBand].Data;
            int PosX, PosY;

            for (int depth = 0; depth < Input.Depth; depth++)
            {
                int i, j, ki, kj;

                int Value = 0;
                int IdxFinal = 0;
                int Threshold = ((2 * Radius + 1) * (2 * Radius + 1)) / 2;

                #region main loop
                for (j = 0; j < Input.Height; j++)
                {
                    for (int Idx = 0; Idx < HistoSize; Idx++)
                        Histo[Idx] = 0;

                    for (kj = -Radius; kj <= Radius; kj++)
                        for (ki = -Radius; ki <= Radius; ki++)
                        {
                            PosX = ki;
                            if (PosX < 0) PosX = -PosX - 1;
                            else if (PosX >= Input.Width) PosX = 2 * Input.Width - PosX - 1;
                            PosY = kj + j;
                            if (PosY < 0) PosY = -PosY - 1;
                            else if (PosY >= Input.Height) PosY = 2 * Input.Height - PosY - 1;

                            Histo[(int)inp[PosX + PosY * Input.Width + depth * Input.SliceSize]]++;
                        }

                    Value = 0;
                    IdxFinal = 0;
                    while (Value < Threshold)
                    {
                        Value += Histo[IdxFinal];
                        IdxFinal++;
                    }
                    outp[Radius + 1 + j * Input.Width + depth * Input.SliceSize] = IdxFinal;


                    //for (i = 0; i < input.Width; i++)
                    for (i = 0; i < Input.Width/* - (radius + 1)*/; i++)
                    {
                        // remove the first column
                        for (int Idx = -Radius; Idx <= Radius; Idx++)
                        {
                            PosX = i - Radius - 1;
                            if (PosX < 0) PosX = -PosX - 1;
                            else if (PosX >= Input.Width) PosX = 2 * Input.Width - PosX - 1;

                            PosY = j + Idx;
                            if (PosY < 0) PosY = -PosY - 1;
                            else if (PosY >= Input.Height) PosY = 2 * Input.Height - PosY - 1;

                            Histo[(int)inp[PosX + PosY * Input.Width + depth * Input.SliceSize]]--;
                        }
                        // add the new column
                        for (int Idx = -Radius; Idx <= Radius; Idx++)
                        {
                            PosX = i + Radius;
                            if (PosX < 0) PosX = -PosX - 1;
                            if (PosX >= Input.Width) PosX = Input.Width - (PosX - Input.Width + 1);

                            PosY = j + Idx;
                            if (PosY < 0) PosY = -PosY - 1;
                            if (PosY >= Input.Height) PosY = 2 * Input.Height - PosY - 1;

                            Histo[(int)inp[PosX + PosY * Input.Width + depth * Input.SliceSize]]++;
                        }
                        Value = 0;
                        IdxFinal = 0;
                        while (Value < Threshold)
                        {
                            Value += Histo[IdxFinal];
                            IdxFinal++;
                        }
                        //return;
                        outp[i + j * Input.Width + depth * Input.SliceSize] = IdxFinal;

                    }
                }
                #endregion
            }

            return;
        }

    }
}
