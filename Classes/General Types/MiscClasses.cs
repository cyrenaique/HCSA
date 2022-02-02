using System;
using System.Collections.Generic;
//using Emgu.CV;
//using Emgu.CV.Structure;
//using Emgu.CV.CvEnum;

namespace HCSAnalyzer.Classes
{




    public class cInfoClassif
    {
        public String StringForTree;
        public String StringForQuality;
        public String ConfusionMatrix;

    }


    public class cInfoClass
    {
        public int[] CorrespondanceTable;
        public List<int> ListBackAssociation = new List<int>();
        public int NumberOfClass = 0;

    }


    public class cScoreAndClass
    {
        public double Score;
        public int Class;

        public cScoreAndClass(int Class, double Score)
        {
            this.Class = Class;
            this.Score = Score;
        }
    }


    public class cInfoDescriptors
    {
        public int[] CorrespondanceTable;
        public List<int> ListBackAssociation = new List<int>();
    }



}
