using System.Collections.Generic;

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
