using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Drawing;

namespace HCSAnalyzer.Classes.Base_Classes.Data
{
    public enum eRandDistributionType { UNIFORM, GAUSSIAN };


    class cRandomGenerator : cDataGenerator
    {
        #region Parameters
        int Widht = 100;
        int Height = 100;
        public double Min = 0;
        public double Max = 100;
        public eRandDistributionType DistributionType = eRandDistributionType.GAUSSIAN;
        public double Mean = 100;
        public double Stdev = 5;
        #endregion

        public int Seed;
        Random RND;
        cExtendedTable ToReturn = null;

        public cRandomGenerator(int Width, int Height)
        {
            this.Widht = Width;
            this.Height = Height;
            
        }

        public cExtendedTable GetOutPut()
        {
            return this.ToReturn;
        }

        public cFeedBackMessage Run()
        {

            ToReturn = new cExtendedTable(this.Widht,this.Height,0);

            RND = new Random(this.Seed);

            if (this.DistributionType == eRandDistributionType.UNIFORM)
            {

                foreach (cExtendedList item in ToReturn)
                    for (int i = 0; i < item.Count; i++)
                        item[i] = RND.NextDouble() * (Max - Min) + Min;
            }
            else if (this.DistributionType == eRandDistributionType.GAUSSIAN)
            {
                foreach (cExtendedList item in ToReturn)
                    for (int i = 0; i < item.Count; i++)
                    {
                        double u1 = RND.NextDouble();
                        double u2 = RND.NextDouble();
                        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
                        item[i] = this.Mean + this.Stdev * randStdNormal;
                    }
            }
            else
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "Distribution Type not implemented";
            }

            return FeedBackMessage;

        }


    }
}
