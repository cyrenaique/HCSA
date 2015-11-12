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
    public partial class cImageDrawKernel : c2DImageFilter
    {
        public double sigma_x = 3;
        public double sigma_y = 3;
        public double sigma_z = 3;
       // public double Teta = 0;

        public cImageDrawKernel()
        {
            this.Title = "Kernel";
        }

        public void Run()
        {
          //  int k = (int)Math.Max(Math.Ceiling(sigma_x * 3.0f), Math.Ceiling(sigma_y * 3.0f));
            int KernelX = (int)Math.Ceiling(sigma_x * 3.0f);
            int KernelY = (int)Math.Ceiling(sigma_y * 3.0f);
            int KernelZ = (int)Math.Ceiling(sigma_z * 3.0f);

            //double i, j;

            this.Output = new cImage(2 * KernelX + 1, 2 * KernelX + 1, 2 * KernelX + 1, 1);

            for (int i0 = -KernelX; i0 <= KernelX; i0++)
                for (int j0 = -KernelY; j0 <= KernelY; j0++)
                    for (int k0 = -KernelZ; k0 <= KernelZ; k0++)
                {
                    this.Output.SingleChannelImage[0].Data[(i0 + KernelX) + (j0 + KernelY) * (this.Output.Width) + (k0 + KernelZ) * (this.Output.SliceSize)] =
                        (float)(Math.Exp(-0.5 * ((i0 / sigma_x) * (i0 / sigma_x) + (j0 / sigma_y) * (j0 / sigma_y) + (k0 / sigma_z) * (k0 / sigma_z))));
                }



            //for (int i0 = -k; i0 <= k; i0++)
            //    for (int j0 = -k; j0 <= k; j0++)
            //    {
            //        // rotation of the filter
            //        //i = i0 * Math.Cos((double)Teta) + j0 * Math.Sin((double)Teta);
            //        //j = j0 * Math.Cos((double)Teta) - i0 * Math.Sin((double)Teta);

            //        //this.Output.SingleChannelImage[0].Data[(i0 + k) + (j0 + k) * (2 * k + 1)] =
            //        //    (float)(Math.Exp(-0.5 * ((i / sigma_x) * (i / sigma_x) + (j / sigma_y) * (j / sigma_y))));

            //        this.Output.SingleChannelImage[0].Data[(i0 + k) + (j0 + k) * (2 * k + 1)] =
            //            (float)(Math.Exp(-0.5 * ((i0 / sigma_x) * (i0 / sigma_x) + (j0 / sigma_y) * (j0 / sigma_y))));
            //    }
            return;
        }

    }
}
