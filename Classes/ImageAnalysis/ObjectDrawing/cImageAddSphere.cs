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
using HCSAnalyzer.Classes._3D;

namespace ImageAnalysisFiltering
{
    public partial class cImageAddSphere : c2DImageFilter
    {
        public cPoint3D Centroid = new cPoint3D(double.NaN,double.NaN,double.NaN);

        public double Radius = 10;
        public float Value = 255;
        public bool IsInPlace = false;
        
        /// <summary>
        /// Specify if the sphere is solid or if the inside values are based on the distance to the center
        /// </summary>
        public bool IsBinary = true;

        public cImageAddSphere()
        {
            this.Title = "Add Sphere";
        }

        public cFeedBackMessage Run()
        {

            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data table defined.";
                return FeedBackMessage;
            }


            if (IsInPlace == false)
                this.Output = new cImage(this.Input, true);
            else
                this.Output = this.Input;

            if (double.IsNaN(Centroid.X)) Centroid.X = this.Output.Width / 2;
            if (double.IsNaN(Centroid.Y)) Centroid.Y = this.Output.Height / 2;
            if (double.IsNaN(Centroid.Z)) Centroid.Z = this.Output.Depth / 2;

            double DistToCenter = 0;
           // Centroid.Z = 0;
            if (IsBinary)
            {
                for (int z = 0; z < this.Output.Depth; z++)
                    for (int y = 0; y < this.Output.Height; y++)
                        for (int x = 0; x < this.Output.Width; x++)
                        {
                            DistToCenter = Math.Sqrt((z - Centroid.Z) * (z - Centroid.Z) + (y - Centroid.Y) * (y - Centroid.Y) + (x - Centroid.X) * (x - Centroid.X));
                            if (DistToCenter <= Radius)
                            {
                                this.Output.SingleChannelImage[this.outputBand].Data[x + y * this.Output.Width + z * this.Output.SliceSize] = this.Value +
                                    this.Input.SingleChannelImage[this.outputBand].Data[x + y * this.Output.Width + z * this.Output.SliceSize];
                            }
                        }
            }
            else
            {
                for (int z = 0; z < this.Output.Depth; z++)
                    for (int y = 0; y < this.Output.Height; y++)
                        for (int x = 0; x < this.Output.Width; x++)
                        {
                            DistToCenter = Math.Sqrt((z - Centroid.Z) * (z - Centroid.Z) + (y - Centroid.Y) * (y - Centroid.Y) + (x - Centroid.X) * (x - Centroid.X));
                            if (DistToCenter <= Radius) this.Output.SingleChannelImage[this.outputBand].Data[x + y * this.Output.Width + z * this.Output.SliceSize] = 100/((float)DistToCenter+1);
                            

                        }

            }

            return FeedBackMessage;
        }

    }
}
