using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageAnalysis;
using HCSAnalyzer.Classes._3D;

namespace HCSAnalyzer.Classes.ImageAnalysis
{
    public class cImageMontage 
    {
        cPoint3D MontageDimensions = new cPoint3D(0,0,0);
        List<cImage> ListImage = null;
        int NumberOfChannels;
        cPoint3D Resolution = new cPoint3D(1, 1, 1);

        public cPoint3D ComputeDimensions()
        {
            foreach (var item in ListImage)
            {
                
            }

            return MontageDimensions;
        }

        /// <summary>
        /// Add a new image to the montage
        /// </summary>
        /// <param name="NewIm">New image to be integrated</param>
        /// <param name="IsForceInconsistencies">if set to TRUE then we'll try to force the image integration within the montage even if there are some inconsistencies with the last image (resolution, number of channels)</param>
        public bool Add(cImage NewIm, bool IsForceInconsistencies)
        {
            if (ListImage == null)
            {
                ListImage = new List<cImage>();
                ListImage.Add(NewIm);
            }

            if ((ListImage[ListImage.Count - 1].GetNumChannels() != NewIm.GetNumChannels()) && (!IsForceInconsistencies))
            {
                return false;
            }
            else
            {
                if (this.NumberOfChannels < NewIm.GetNumChannels())
                    this.NumberOfChannels = NewIm.GetNumChannels();

                this.ListImage.Add(NewIm);
            }

            if (((ListImage[ListImage.Count - 1].Resolution.X != NewIm.Resolution.X) && (!IsForceInconsistencies)) ||
                ((ListImage[ListImage.Count - 1].Resolution.Y != NewIm.Resolution.Y) && (!IsForceInconsistencies)) ||
                ((ListImage[ListImage.Count - 1].Resolution.Z != NewIm.Resolution.Z) && (!IsForceInconsistencies)))
            {
                return false;
            }
            else
            {
                this.Resolution.X = NewIm.Resolution.X;
                this.Resolution.Y = NewIm.Resolution.Y;
                this.Resolution.Z = NewIm.Resolution.Z;

                this.ListImage.Add(NewIm);
            }


            return true;
        }

        public cImage GetMontage()
        {
            cImage MyIm = new cImage((int)this.MontageDimensions.X, (int)this.MontageDimensions.Y, (int)this.MontageDimensions.Z,this.NumberOfChannels);
            MyIm.Resolution = new cPoint3D(this.Resolution);

            return MyIm;
        
        }

    }
}
