using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
//using Emgu.CV;
//using Emgu.CV.Structure;
//using Emgu.CV.CvEnum;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.CvEnum;
using System.Drawing;

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
