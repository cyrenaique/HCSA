using HCSAnalyzer.Classes.Base_Classes;
using ImageAnalysis;

namespace ImageAnalysisFiltering
{
    public partial class cImageToVTK : c2DImageFilter
    {

        public cImageToVTK()
        {
            this.Title = "Image To VTK image";
        }

        public cFeedBackMessage Run()
        {
            if (base.Start() == false)
            {
                base.FeedBackMessage.IsSucceed = false;
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

                    for (j = 0; j < base.Output.Height; j++)
                        for (i = 0; i < base.Output.Width; i++, cpt++)
                        {
                            outData[cpt] = inData[i + (base.Input.Height - j - 1) * base.Input.Width + k * base.Input.SliceSize];
                        }
            }


            base.End();
            return FeedBackMessage;
        }

    }
}
