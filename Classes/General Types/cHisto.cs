using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes
{
    public class cHisto
    {


        public double Min()
        {
            return this.ListValuesX[0];
        }

        public double Max()
        {
            return this.ListValuesX[this.ListValuesX.Length - 1];
        }

        double[] ListValuesY;
        double[] ListValuesX;

        public cExtendedList GetXvalues()
        {
            cExtendedList List = new cExtendedList();
            List.AddRange(ListValuesX);
            return List;
        }

        public cExtendedList GetYvalues()
        {
            cExtendedList List = new cExtendedList();
            List.AddRange(ListValuesY);
            return List;
        }

        public void SetYvalues(double Value, int Idx)
        {
            this.ListValuesY[Idx] = Value;
        }

        public double GetXValue(int Idx)
        {
            return ListValuesX[Idx];
        }

        public double GetYValue(int Idx)
        {
            return ListValuesY[Idx];
        }

        public cHisto(cExtendedList OriginalValues, int NumBin)
        {
            List<double[]> Res= OriginalValues.CreateHistogram(NumBin, false);
            ListValuesX = Res[0];
            ListValuesY = Res[1];
        }

        public List<double[]> CreateHistogram(double[] ListValues, double Bin)
        {
            List<double[]> ToReturn = new List<double[]>();


            //float max = math.Max(data);

            double Max = ListValues[0];
            double Min = ListValues[0];

            for (int Idx = 1; Idx < ListValues.Length; Idx++)
            {
                if (ListValues[Idx] > Max) Max = ListValues[Idx];
                if (ListValues[Idx] < Min) Min = ListValues[Idx];
            }

            double[] axeX;
            double[] histogram;

            if (Max == Min)
            {
                axeX = new double[1];
                axeX[0] = Max;
                ToReturn.Add(axeX);
                histogram = new double[1];
                histogram[0] = /*Max **/ ListValues.Length;
                ToReturn.Add(histogram);
                return ToReturn;

            }

            double step = (Max - Min) / Bin;
            //  int HistoSize = (int)((Max - Min) / step)+1;

            axeX = new double[(int)Bin];

            for (int i = 0; i < (int)Bin; i++)
            {
                axeX[i] = Min + i * step;
            }
            ToReturn.Add(axeX);

            histogram = new double[(int)Bin];
            //double RealPos = Min;

            int PosHisto;
            for(int Idx=0;Idx<ListValues.Length;Idx++)
            {
                double f=ListValues[Idx];
                PosHisto = (int)(((Bin - 1) * (f - Min)) / (Max - Min));
                // if ((PosHisto >= 0) && (PosHisto < Bin))
                histogram[PosHisto]++;
            }
            ToReturn.Add(histogram);

            return ToReturn;
        }

        public cHisto(double[] OriginalValues, int NumBin)
        {
            List<double[]> Res= this.CreateHistogram(OriginalValues, NumBin);
            ListValuesX = Res[0];
            ListValuesY = Res[1];
        }

        public cHisto(List<double> ListXValues, List<double> ListYValues)
        {
            this.ListValuesX = new double[ListXValues.Count];
            this.ListValuesY = new double[ListYValues.Count];

            if (ListXValues == null)
            {
                for (int i = 0; i < this.ListValuesX.Length; i++)
                    this.ListValuesX[i] = i;
            }
            else
                Array.Copy(ListXValues.ToArray(), this.ListValuesX, this.ListValuesX.Length);
            
            
            Array.Copy(ListYValues.ToArray(), this.ListValuesY, this.ListValuesY.Length);
           
            
            

        }

        public double GetAverageValue()
        {
            double Value = 0;
            double Sum = 0;
            for (int i = 0; i < this.ListValuesX.Length; i++)
            {
                Value += this.ListValuesX[i] * this.ListValuesY[i];
                Sum += this.ListValuesY[i];
            }

            return Value/Sum;
        
        }

        public cHisto(double OriginalValue)
        {
            ListValuesX = new double[1];
            ListValuesY = new double[1];

            ListValuesX[0] = OriginalValue;
            ListValuesY[0] = 1;

        }

    }
}
