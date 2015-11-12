using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Forms.FormsForImages;
using System.Drawing;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
using HCSAnalyzer.Classes.MetaComponents;

namespace ImageAnalysis
{
    public partial class cImageSeq : List<cImage>
    {
        public cListSingleChannelImage SingleChannelImage = new cListSingleChannelImage();
        public string Name = "New Sequence";
        public object Tag;


        #region Constructors
        private cImageSeq()
        {
            UpDateName();
        }

        void UpDateName()
        {
            //this.Name += " - [" + this.Width + "x" + this.Height + "x" + this.Depth + "]" + " - " + this.NumChannels + " channels";
        }


        


        #endregion
    }




}
