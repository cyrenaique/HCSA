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

namespace ImageAnalysisFiltering
{
    public abstract class c2DImageFilter : cComponent
    {
        protected cImage Input;
        protected cImage Output;
        //protected int inputBand;
        protected int outputBand;

        public bool IsFull3DImage = false;
        public int SliceIndex = 0;
        public bool IsInPlace = false;

        public List<int> ListChannelsToBeProcessed = new List<int>();

        public c2DImageFilter()
        {
            ListChannelsToBeProcessed = new List<int>();
            
            // by default only one channel has to be processed: the first one
           // ListChannelsToBeProcessed.Add(0);
        }

        public cImage GetOutPut()
        {
            if (this.Output == null) return null;

            if (this.Input != null)
                this.Output.Name = this.Title + "(" + this.Input.Name + ")";
            else
                this.Output.Name = this.Title;

            return this.Output;
        }

        public void SetInputData(cImage input, int inputBand)
        {
            this.Input = input;
            this.ListChannelsToBeProcessed.Add(inputBand);
        }

        public void SetInputData(cImage input)
        {
            this.Input = input;
            this.ListChannelsToBeProcessed.AddRange(input.GetListChannels());
        }

        public void SetInputData(cImage input, List<int> ListChannelsToBeProcessed)
        {
            this.Input = input;
            this.ListChannelsToBeProcessed.AddRange(ListChannelsToBeProcessed);
        }


    }


}
