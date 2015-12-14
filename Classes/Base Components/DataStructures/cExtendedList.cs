using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
//using RDotNet;

namespace HCSAnalyzer.Classes.Base_Classes.DataStructures
{

    public enum eNormalizationType { SHIFT_TO_MIN, SHIFT_TO_MEAN, SHIFT_TO_MEDIAN, STANDARDIZE, MIN_MAX };

    [Serializable]
    public class cExtendedList : List<double>
    {
        public string Name = "Extended List";
        public Color Color = Color.DarkBlue;
        public List<object> ListTags = null;
        public object Tag = null;
        string Info = "";

        public string GetInfo()
        {
            string ToBeReturned = this.Info;
            if (this.Info == "")
            {
                // info has been declared, let's try to build it
                // this.Tag

            }
            return Info;
        }

        public void SetInfo(string Info)
        {
            this.Info = Info;
        }

        #region creators
        public cExtendedList(string Name)
        {
            this.Name = Name;
        }

        public cExtendedList(double Data)
        {
            this.Add(Data);
        }

        //public cExtendedList(NumericVector Data)
        //{
        //    foreach (var item in Data)
        //        this.Add(item);
         
        //}

        //public cExtendedList(DynamicVector Data)
        //{
        //    try
        //    {
        //        foreach (double item in Data)
        //            this.Add(item);
        //    }
        //    catch (Exception)
        //    {
        //        return;
                
        //    }
        //}
        public cExtendedList(float[] Data)
        {
            for (int i = 0; i < Data.Length; i++)
                this.Add(Data[i]);
        }
        /// <summary>
        /// create an extended list from an already existing cExtendedList, but keep only a percentage of the orginal data
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Ratio"></param>
        public cExtendedList(cExtendedList Data, double Ratio)
        {
            if (Ratio > 1) return;
            int NewNumberofValues = (int)(Ratio * Data.Count);
            if (NewNumberofValues <= 0) return;

            for (int i = 0; i < NewNumberofValues; i++)
                this.Add(Data[i]);
        }

        public cExtendedList(double[] Data)
        {
            for (int i = 0; i < Data.Length; i++)
                this.Add(Data[i]);
        }

        public cExtendedList(string Name, Color Color)
        {
            this.Name = Name;
            this.Color = Color;
        }

        public cExtendedList()
        {
            this.Name = "";
        }
        #endregion

        #region Operations


        public static cExtendedList operator +(cExtendedList c1, double c2)
        {
            cExtendedList Toreturn = new cExtendedList();
            foreach (var item in c1) Toreturn.Add(item + c2);
            return Toreturn;
        }

        public static cExtendedList operator -(cExtendedList c1, double c2)
        {
            cExtendedList Toreturn = new cExtendedList();
            foreach (var item in c1) Toreturn.Add(item - c2);
            return Toreturn;
        }

        public static cExtendedList operator *(cExtendedList c1, double c2)
        {
            cExtendedList Toreturn = new cExtendedList();
            foreach (var item in c1) Toreturn.Add(item * c2);
            return Toreturn;
        }

        public static cExtendedList operator /(cExtendedList c1, double c2)
        {
            cExtendedList Toreturn = new cExtendedList();
            foreach (var item in c1) Toreturn.Add(item / c2);
            return Toreturn;
        }
        /// <summary>
        /// Perform operation between extended list
        /// Warning: as Nan and Infy are not allowed, division by zero will generate 0
        /// </summary>
        /// <param name="SecondList"></param>
        /// <param name="OperationType"></param>
        /// <returns></returns>
        public cExtendedList Operation(cExtendedList SecondList, eBinaryOperationType OperationType)
        {
            if (this.Count != SecondList.Count) return null;

            cExtendedList ToBeReturned = new cExtendedList(this, 1);
            ToBeReturned.Name = this.Name + OperationType.ToString() + SecondList.Name;

            switch (OperationType)
            {
                case eBinaryOperationType.ADD:
                    for (int i = 0; i < ToBeReturned.Count; i++) ToBeReturned[i] += SecondList[i];
                    break;
                case eBinaryOperationType.SUBSTRACT:
                    for (int i = 0; i < ToBeReturned.Count; i++) ToBeReturned[i] -= SecondList[i];
                    break;
                case eBinaryOperationType.MULTIPLY:
                    for (int i = 0; i < ToBeReturned.Count; i++) ToBeReturned[i] *= SecondList[i];
                    break;
                case eBinaryOperationType.DIVIDE:
                    for (int i = 0; i < ToBeReturned.Count; i++)
                    {
                        if (SecondList[i] == 0)
                            ToBeReturned[i] = 0;
                        else
                            ToBeReturned[i] /= SecondList[i];
                    }
                    break;
                default:
                    break;
            }

            return ToBeReturned;
        }

        public double Mean()
        {
            double Mean = 0;
            for (int i = 0; i < this.Count; i++)
                Mean += this[i];
            return Mean / (double)this.Count;
        }

        public double Sum()
        {
            double Mean = 0;
            for (int i = 0; i < this.Count; i++)
                Mean += this[i];
            return Mean;
        }

        public double GetWeightedMean()
        {
            double ToReturn = 0;
            double Norm = this.Sum();

            for (int Idx = 0; Idx < this.Count; Idx++)
            {
                ToReturn += (Idx * this[Idx]);
            }

            return ToReturn / Norm;

        }

        public double Std()
        {
            double var = 0f, mean = this.Mean();
            foreach (float f in this) var += (f - mean) * (f - mean);
            return Math.Sqrt(var / (float)(this.Count - 1));
        }



        public double MAD(bool IsNormalDistribution)
        {
            double ResMedian = this.Median();

            cExtendedList Tmp = new cExtendedList(this, 1);

            if (IsNormalDistribution)
                for (int i = 0; i < Tmp.Count; i++) Tmp[i] = 1.4826f * (Tmp[i] - ResMedian);
            else
                for (int i = 0; i < Tmp.Count; i++) Tmp[i] -= ResMedian;

            cExtendedList TmpAbs = Tmp.Abs();

            return TmpAbs.Median();
        }

        public cExtendedList Abs()
        {
            cExtendedList TR = new cExtendedList(this, 1);
            TR.Name = "Abs(" + this.Name + ")";

            for (int i = 0; i < this.Count; i++)
                TR[i] = Math.Abs(this[i]);

            return TR;
        }

        /// <summary>
        /// Compute coefficient of variation
        /// </summary>
        /// <returns></returns>
        public double CV()
        {
            double var = 0f, mean = this.Mean();
            foreach (float f in this) var += (f - mean) * (f - mean);
            return Math.Sqrt(var / (float)(this.Count - 1)) / mean;
        }

        public double Skewness()
        {
            double Skew = 0f, mean = this.Mean(), std = this.Std();

            foreach (float f in this) Skew += (f - mean) * (f - mean) * (f - mean);
            return Skew / ((float)(this.Count - 1) * std * std * std);
        }

        public double Kurtosis()
        {
            double Kurt = 0f, mean = this.Mean(), std = this.Std();

            foreach (float f in this) Kurt += (f - mean) * (f - mean) * (f - mean) * (f - mean);
            return (Kurt / ((float)(this.Count - 1) * std * std * std * std) - 3);
        }

        public double NthQuartile(int N)
        {
            double ToBeReturned = 0;

            cExtendedList SortedList = new cExtendedList(this, 1);
            SortedList.Sort();

            int Idx = (int)(SortedList.Count / N);
            ToBeReturned = SortedList[Idx];

            return ToBeReturned;
        }

        public double Median()
        {
            return NthQuartile(2);
        }

        //public cExtendedList Normalize(double Min, double Max)
        //{
        //    cExtendedList ToReturn = new cExtendedList();

        //    if (Min == Max) return this;

        //    double Diff = Max - Min;

        //    foreach (var item in this)
        //        ToReturn.Add((item - Min) / Diff);

        //    return ToReturn;
        //}

        public cExtendedList Normalize(eNormalizationType NormalizationType)
        {
            cExtendedList ToReturn = new cExtendedList();

            if (NormalizationType == eNormalizationType.MIN_MAX)
            {
                double Min = this.Min();
                double Max = this.Max();

                if (Min == Max) return null;

                double Diff = Max - Min;

                foreach (var item in this)
                    ToReturn.Add((item - Min) / Diff);

                ToReturn.Name = "Min-Max(" + this.Name + ")";

                return ToReturn;
            }
            else if (NormalizationType == eNormalizationType.SHIFT_TO_MIN)
            {
                double Min = this.Min();

                foreach (var item in this)
                    ToReturn.Add(item - Min);

                ToReturn.Name = "ShiftToMin(" + this.Name + ")";

                return ToReturn;
            }
            else if (NormalizationType == eNormalizationType.SHIFT_TO_MEAN)
            {
                double Mean = this.Mean();

                foreach (var item in this)
                    ToReturn.Add(item - Mean);

                ToReturn.Name = "ShiftToMean(" + this.Name + ")";

                return ToReturn;
            }
            else if (NormalizationType == eNormalizationType.SHIFT_TO_MEDIAN)
            {
                //double Min = this.Med

                //foreach (var item in this)
                //    ToReturn.Add(item - Min);

                //ToReturn.Name = "ShiftToMin(" + this.Name + ")";

                return ToReturn;
            }
            else if (NormalizationType == eNormalizationType.STANDARDIZE)
            {
                double Mean = this.Mean();
                double Stdev = this.Std();

                if (Stdev <= 0) return null;

                foreach (var item in this)
                    ToReturn.Add((item - Mean) / Stdev);

                ToReturn.Name = "Standardize(" + this.Name + ")";

                return ToReturn;
            }
            else return null;
        }
        #endregion

        #region Convert
        //public NumericVector CopyTo_R_StatVector()
        //{
        //    NumericVector ToReturn = cGlobalInfo.RStat_engine.CreateNumericVector(this.ToArray());
        //    cGlobalInfo.RStat_engine.SetSymbol(this.Name, ToReturn);

        //    return ToReturn;
        //}
        #endregion 

        public List<double[]> CreateHistogram(double Min, double Max, double BinSize)
        {
            List<double[]> ToReturn = new List<double[]>();


            if (Min == Max)
            {
                ToReturn.Add(new double[1]);
                ToReturn.Add(new double[1]);
                return ToReturn;
            }

            //float max = math.Max(data);
            if (this.Count == 0) return ToReturn;

            double step = BinSize;// (Max - Min) / BinSize;

            int HistoSize = (int)((Max - Min) / step) + 1;

            double[] axeX = new double[HistoSize];
            for (int i = 0; i < HistoSize; i++)
            {
                axeX[i] = Min + i * step;
            }
            ToReturn.Add(axeX);

            double[] histogram = new double[HistoSize];
            //double RealPos = Min;

            int PosHisto;
            foreach (double f in this)
            {
                PosHisto = (int)((f - Min) / step);
                if ((PosHisto >= 0) && (PosHisto < HistoSize))
                    histogram[PosHisto]++;
            }
            ToReturn.Add(histogram);

            return ToReturn;
        }

        public List<double[]> CreateHistogram(double Min, double Max, int NumBin, bool IsNormalized)
        {
            List<double[]> ToReturn = new List<double[]>();

            double BinSize = (Max - Min) / (double)(NumBin - 1);

            //float max = math.Max(data);
            if (this.Count == 0) return ToReturn;

            double step = BinSize;

            int HistoSize = NumBin;

            double[] axeX = new double[HistoSize];
            for (int i = 0; i < HistoSize; i++)
            {
                axeX[i] = Min + i * step;
            }
            ToReturn.Add(axeX);

            double[] histogram = new double[HistoSize];
            //double RealPos = Min;

            int PosHisto;
            double Increment = 1;
            if (IsNormalized) Increment = 100 / (double)(this.Count * step);

            //double Total = Increment * this.Count;
            //  Total = 0;

            foreach (double f in this)
            {
                PosHisto = (int)((f - Min) / step);
                if ((PosHisto >= 0) && (PosHisto < HistoSize))
                histogram[PosHisto] += Increment;
                //else
                //{
                //}

                //Total += Increment;

            }
            ToReturn.Add(histogram);

            return ToReturn;
        }

        public List<double[]> CreateHistogram(double NumberOfBin, bool IsNormalized)
        {
            List<double[]> ToReturn = new List<double[]>();

            //float max = math.Max(data);
            if (this.Count == 0) return ToReturn;
            double Max = this[0];
            double Min = this[0];

            for (int Idx = 1; Idx < this.Count; Idx++)
            {
                if (this[Idx] > Max) Max = this[Idx];
                if (this[Idx] < Min) Min = this[Idx];
            }

            double[] axeX;
            double[] histogram;

            if (Max == Min)
            {
                axeX = new double[1];
                axeX[0] = Max;
                ToReturn.Add(axeX);
                histogram = new double[1];
                histogram[0] = /*Max **/ this.Count;
                ToReturn.Add(histogram);
                return ToReturn;

            }

            double step = (Max - Min) / NumberOfBin;

            if (NumberOfBin == -1)
            {
                NumberOfBin = Max - Min + 1;
                step = 1;// (Max - Min + 1) / NumberOfBin;
            }

            //  int HistoSize = (int)((Max - Min) / step)+1;

            axeX = new double[(int)NumberOfBin];

            for (int i = 0; i < (int)NumberOfBin; i++)
            {
                axeX[i] = Min + i * step;
            }
            ToReturn.Add(axeX);

            histogram = new double[(int)NumberOfBin];
            //double RealPos = Min;

            int PosHisto;
            double Increment = 1;
            if (IsNormalized) Increment = 100 / (double)(this.Count * step);
            //    double Total = 0;
            foreach (double f in this)
            {
                PosHisto = (int)(((NumberOfBin - 1) * (f - Min)) / (Max - Min));
                // if ((PosHisto >= 0) && (PosHisto < Bin))
                histogram[PosHisto] += Increment;
                //      Total += Increment;
            }
            ToReturn.Add(histogram);

            return ToReturn;
        }

        //public List<cExtendedList> CreateHistogram(double Bins)
        //{
        //    List<cExtendedList> ToReturn = new List<cExtendedList>();


        //    //float max = math.Max(data);
        //    if (this.Count == 0) return ToReturn;
        //    double Max = this[0];
        //    double Min = this[0];

        //    for (int Idx = 1; Idx < this.Count; Idx++)
        //    {
        //        if (this[Idx] > Max) Max = this[Idx];
        //        if (this[Idx] < Min) Min = this[Idx];
        //    }

        //    cExtendedList axeX = new cExtendedList();
        //    cExtendedList histogram = new cExtendedList();

        //    if (Max == Min)
        //    {

        //        axeX.Add(Max);
        //        ToReturn.Add(axeX);
        //        histogram.Add(Max * this.Count);
        //        //histogram[0] = Max * this.Count;
        //        ToReturn.Add(histogram);
        //        return ToReturn;

        //    }

        //    double step = (Max - Min) / Bin;
        //    //  int HistoSize = (int)((Max - Min) / step)+1;

        //   // axeX = new double[(int)Bin];

        //    for (int i = 0; i < (int)Bin; i++)
        //    {
        //        axeX.Add(Min + i * step);
        //    }
        //    ToReturn.Add(axeX);

        //  //  histogram = new double[(int)Bin];
        //    //double RealPos = Min;

        //    int PosHisto;
        //    foreach (double f in this)
        //    {
        //        PosHisto = (int)(((Bin - 1) * (f - Min)) / (Max - Min));
        //        // if ((PosHisto >= 0) && (PosHisto < Bin))
        //        histogram[PosHisto]++;
        //    }
        //    ToReturn.Add(histogram);

        //    return ToReturn;
        //}

        #region informations
        public double Max()
        {
            double Max = double.MinValue;

            foreach (double val in this)
                if (val >= Max) Max = val;
            return Max;

        }

        public double Min()
        {
            double Min = double.MaxValue;

            foreach (double val in this)
                if (val <= Min) Min = val;
            return Min;

        }

        public bool IsContainNegative()
        {
            foreach (var item in this)
            {
                if (item < 0) return true;
            }
            return false;
        }
        #endregion

        #region Metrics
        public double Dist_Mahalanobis(cExtendedList CompareTo, cExtendedTable TransitionMatrix)
        {
            if (CompareTo.Count != this.Count) return -1;
            if ((CompareTo.Count != TransitionMatrix.Count) || (CompareTo.Count != TransitionMatrix[0].Count)) return -1;

            cExtendedList TmpVector = new cExtendedList();

            for (int i = 0; i < this.Count; i++)
            {
                double TMpValue = 0;
                for (int j = 0; j < this.Count; j++)
                {
                    TMpValue += TransitionMatrix[j][i] * (CompareTo[j] - this[j]);
                }
                TmpVector.Add(TMpValue);
            }

            double ToReturn = 0;
            for (int j = 0; j < TmpVector.Count; j++)
            {
                ToReturn += TmpVector[j] * (CompareTo[j] - this[j]);
            }


            return Math.Sqrt(ToReturn);
        }

        public double Dist_Euclidean(cExtendedList CompareTo)
        {
            double Res = 0;
            if (CompareTo.Count != this.Count) return -1;


            for (int i = 0; i < this.Count; i++)
            {
                Res += ((this[i] - CompareTo[i]) * (this[i] - CompareTo[i]));
            }


            return Math.Sqrt(Res);
        }

        public double Dist_Manhattan(cExtendedList CompareTo)
        {
            double Res = 0;
            if (CompareTo.Count != this.Count) return -1;

            for (int i = 0; i < this.Count; i++)
            {
                Res += Math.Abs(this[i] - CompareTo[i]);
            }


            return Res;
        }

        public double Dist_VectorCosine(cExtendedList CompareTo)
        {

            if (CompareTo.Count != this.Count) return -1;

            double Top = 0;
            double Bottom1 = 0;
            double Bottom2 = 0;

            for (int i = 0; i < this.Count; i++)
            {
                Top += this[i] * CompareTo[i];

                Bottom1 += this[i] * this[i];
                Bottom2 += CompareTo[i] * CompareTo[i];

            }

            double Bottom = Math.Sqrt(Bottom1) * Math.Sqrt(Bottom2);

            if (Bottom <= 0) return -1;

            double ToReturn = 1 - (Top / Bottom);

            return ToReturn;
        }

        public double ZFactor(cExtendedList CompareTo, bool IsRobust)
        {
            double Mean1, Mean2;
            double ZScore;
            double Std1, Std2;

            if (IsRobust)
            {
                Mean2 = CompareTo.Median();
                Mean1 = this.Median();

                Std1 = this.MAD(true);
                Std2 = CompareTo.MAD(true);
            }
            else
            {
                Mean2 = CompareTo.Mean();
                Mean1 = this.Mean();

                Std1 = this.Std();
                Std2 = CompareTo.Std();
            }

            if (Mean2 != Mean1)
                ZScore = 1 - 3 * (Std1 + Std2) / (Math.Abs(Mean1 - Mean2));
            else
                ZScore = double.NaN;

            return ZScore;
        }

        public double GetVectNorm()
        {
            double NormToReturn = 0;

            for (int i = 0; i < this.Count; i++)
                NormToReturn += this[i] * this[i];

            return Math.Sqrt(NormToReturn);
        }



        public double DotProduct(cExtendedList CompareTo, bool IsNormalized)
        {
            if (this.Count != CompareTo.Count) return double.NaN;

            double DotProduct = 0;

            for (int i = 0; i < this.Count; i++)
            {
                DotProduct += this[i] * CompareTo[i];
            }

            if (IsNormalized)
            {
                DotProduct /= (this.GetVectNorm() * CompareTo.GetVectNorm());
            }

            return DotProduct;
        }

        public double Dist_BhattacharyyaCoefficient(cExtendedList CompareTo)
        {
            double Res = 0;
            if (CompareTo.Count != this.Count) return -1;


            for (int i = 0; i < this.Count; i++)
            {
                Res += Math.Sqrt(this[i] * CompareTo[i]);
            }

            return Res;
        }

        public double Dist_EarthMover(cExtendedList CompareTo)
        {
            Emgu.CV.Matrix<float> Signature1 = new Emgu.CV.Matrix<float>(this.Count, 2);
            Emgu.CV.Matrix<float> Signature2 = new Emgu.CV.Matrix<float>(CompareTo.Count, 2);


           // CvInvoke.cvCalcOpticalFlowBM(

            for (int Idx = 0; Idx < this.Count; Idx++)
            {
                Signature1[Idx, 0] = (float)this[Idx];
                Signature1[Idx, 1] = Idx;

                Signature2[Idx, 0] = (float)CompareTo[Idx];
                Signature2[Idx, 1] = Idx;
            }

            double ResutatEMD=0.0;
            

            //ResutatEMD = CvInvoke.EMD(Signature1, Signature2, DistType.L1, null, Ptr.Zero, IntPtr.Zero);

            //Emgu.CV.Structure.MCvPoint2D64f



            return ResutatEMD;

        }
        #endregion
    }

}
