using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows;
using System.Drawing;
using HCSAnalyzer.Forms.IO;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using HCSAnalyzer.Controls;
using HCSAnalyzer.Classes.Base_Classes.GUI;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;

namespace LibPlateAnalysis
{
    public class cListSignature : List<cSignature>
    {

        public cSignature GetSignature(cDescriptorType DescriptorType)
        {
            foreach (var item in this)
                if (item.GetAssociatedType() == DescriptorType) return item;

            return null;
        }
    
    }

    public class cSignature
    {
        //string Name;
        //public bool IsSingle;

        cDescriptorType Type;

        public cWell AssociatedWell;

        private cScreening CurrentScreening;

        public cDescriptorType GetAssociatedType()
        {
            return this.Type;
        }

        public cHisto Histogram;

        private double AverageValue = 0;

        private double ComputeDistributionDistanceToReference()
        {
            return 0;

        }

        public int HistoBins;

        #region public

        /// <summary>
        /// Return the value associated to a descriptor within a well
        /// </summary>
        /// <returns>if scalar mode: average else distance between histograms</returns>
        public double GetValue()
        {
            if ((cGlobalInfo.CurrentScreening.Reference == null)||( Type.GetBinNumber()==1))
            {
                //if (Type.GetBinNumber() > 1)
                //{
                //   // MessageBox.Show("GetWeightedMean() not implemented", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //    //return HistoValues.GetWeightedMean();
                //    return -1;
                //}
                //else
                //    return Histogram.GetXvalues()[0];
                return this.AverageValue;// this.Histogram.GetAverageValue();
            }
            else
            {
                //System.Windows.Forms.MessageBox.Show("GetValue() not implemented", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                List<cDescriptorType> Listdesc= new List<cDescriptorType>();
                Listdesc.Add(Type);

                cExtendedTable Et = cGlobalInfo.CurrentScreening.Reference.GetSingleObjectDescriptorValues(Listdesc);

                
                cHistogramBuilder HB = new cHistogramBuilder();
                HB.BinNumber = Type.GetBinNumber();
                HB.Min = 0;// Histogram.Min();
                HB.Max = 50;// Histogram.Max();
                HB.SetInputData(Et);
                HB.Run();

                cExtendedTable TableForDistances = new cExtendedTable(HB.GetOutPut()[1]);


                cListWells TmpListWells = new cListWells(AssociatedWell);
                Et = TmpListWells.GetSingleObjectDescriptorValues(Listdesc);
                
                HB = new cHistogramBuilder();
                HB.BinNumber = Type.GetBinNumber();
                HB.Min = 0;// Histogram.Min();
                HB.Max = 50;// Histogram.Max();
                HB.SetInputData(Et);
                HB.Run();

                TableForDistances.Add(HB.GetOutPut()[1]);

                cDistances DistanceToCompute = new cDistances();
                DistanceToCompute.SetInputData(TableForDistances);

                if (cGlobalInfo.OptionsWindow.radioButtonDistributionMetricEuclidean.Checked)
                    DistanceToCompute.DistanceType= eDistances.EUCLIDEAN;
                //    return Histogram.Dist_Euclidean(cGlobalInfo.CurrentScreening.Reference[cGlobalInfo.CurrentScreening.ListDescriptors.IndexOf(Type)]);
                else if (cGlobalInfo.OptionsWindow.radioButtonDistributionMetricManhattan.Checked)
                    DistanceToCompute.DistanceType = eDistances.MANHATTAN;
                //    return HistoValues.Dist_Manhattan(cGlobalInfo.CurrentScreening.Reference[cGlobalInfo.CurrentScreening.ListDescriptors.IndexOf(Type)]);
                else if (cGlobalInfo.OptionsWindow.radioButtonDistributionMetricCosine.Checked)
                //    return HistoValues.Dist_VectorCosine(cGlobalInfo.CurrentScreening.Reference[cGlobalInfo.CurrentScreening.ListDescriptors.IndexOf(Type)]);
                    DistanceToCompute.DistanceType = eDistances.VECTOR_COS;
                else if (cGlobalInfo.OptionsWindow.radioButtonDistributionMetricBhattacharyya.Checked)
                    DistanceToCompute.DistanceType= eDistances.BHATTACHARYYA;
                //    return HistoValues.Dist_BhattacharyyaCoefficient(cGlobalInfo.CurrentScreening.Reference[cGlobalInfo.CurrentScreening.ListDescriptors.IndexOf(Type)]);
                else //if(cGlobalInfo.OptionsWindow.radioButtonDistributionMetricEMD.Checked)
                    DistanceToCompute.DistanceType = eDistances.EMD;
                //    return HistoValues.Dist_EarthMover(cGlobalInfo.CurrentScreening.Reference[cGlobalInfo.CurrentScreening.ListDescriptors.IndexOf(Type)]);
                //else
                //return -1;



                
           //     TableForDistances.Add(
                DistanceToCompute.Run();
                return DistanceToCompute.GetOutPut()[0][1];
                

            }

            return -1;

        }

        public void RefreshHisto(int NewNumBins)
        {
            this.HistoBins = NewNumBins;
            this.Histogram = new cHisto(this.GetOriginalValues(), HistoBins);


        }



        public void SetHistoValues(List<double> ListXValues, List<double> ListYValues)
        {
            this.Histogram = new cHisto(ListXValues, ListYValues);
            UpDateDescriptorStatistics();

        }

        public void SetHistoValues(double Value)
        {
            this.Histogram = new cHisto(Value);
            // HistoValues[0] = Value;
            UpDateDescriptorStatistics();

        }

        public void SetHistoValues(int Idx, double Value)
        {
            this.Histogram.SetYvalues(Value, Idx);
            // HistoValues[Idx] = Value;
            UpDateDescriptorStatistics();

        }

        public cExtendedList GetHistovalues()
        {
            return this.Histogram.GetYvalues();
        }

        public double GetHistovalue(int Idx)
        {
            return this.Histogram.GetYvalues()[Idx];
        }

        public double GetHistoXvalue(int Idx)
        {
            return this.Histogram.GetXvalues()[Idx];
        }

        /// <summary>
        /// return the descriptor name
        /// </summary>
        /// <returns>the Descriptor name</returns>
        public string GetName()
        {
            return this.Type.GetName();
        }


        /// <summary>
        /// Update the descritpor statistic (Average, first and last value)
        /// </summary>
        public void UpDateDescriptorStatistics()
        {
            this.AverageValue = Histogram.GetAverageValue();
            //FirstValue = HistoValues[0];
            //  LastValue = HistoValues[HistoValues.Count - 1];
        }


        #endregion

        // private double AverageValue;

        //  private double FirstValue = -1;
        // private double LastValue = -1;

        private double[] OriginalValues = null;

        public double[] GetOriginalValues()
        {
            if ((cGlobalInfo.CellByCellDataAccess == eCellByCellDataAccess.MEMORY) && (this.OriginalValues != null))
            {
                return this.OriginalValues;
            }
            else if (cGlobalInfo.CellByCellDataAccess == eCellByCellDataAccess.HD)
            {
                AssociatedWell.AssociatedPlate.DBConnection = new cDBConnection(AssociatedWell.AssociatedPlate, AssociatedWell.SQLTableName);
                List<cDescriptorType> LCDT = new List<cDescriptorType>();
                LCDT.Add(this.GetAssociatedType());
                cExtendedTable ToReturn = AssociatedWell.AssociatedPlate.DBConnection.GetWellValues(AssociatedWell, LCDT );
                AssociatedWell.AssociatedPlate.DBConnection.CloseConnection();
                return ToReturn[0].ToArray();
            }
            return null;
        }

        //private double getAverageValue(float[] Data)
        //{
        //    double Res = 0;
        //    for (int i = 0; i < Data.Length; i++)
        //        Res += Data[i];

        //    return Res / (double)(Data.Length);
        //}

        //private cExtendedList CreateHistogram(double[] data, double start, double end, double step)
        //{
        //    int HistoSize = (int)((end - start) / step) + 1;

        //    double[] histogram = new double[HistoSize];
        //    double RealPos = start;

        //    int PosHisto;
        //    foreach (double f in data)
        //    {
        //        PosHisto = (int)((f - start) / step);
        //        if ((PosHisto >= 0) && (PosHisto < HistoSize))
        //            histogram[PosHisto]++;
        //    }

        //    return histogram;
        //}

        //private cExtendedList CreateHistogram(float[] data, double start, double end, double step)
        //{
        //    int HistoSize = (int)((end - start) / step) + 1;


        //    double[] histogram = new double[HistoSize];
        //    double RealPos = start;

        //    int PosHisto;
        //    foreach (float f in data)
        //    {
        //        PosHisto = (int)((f - start) / step);
        //        if ((PosHisto >= 0) && (PosHisto < HistoSize))
        //            histogram[PosHisto]++;
        //    }

        //    return histogram;
        //}

        //private double getAverageValue(double[] Data)
        //{
        //    double Res = 0;
        //    for (int i = 0; i < Data.Length; i++)
        //        Res += Data[i];

        //    return Res / (double)(Data.Length);
        //}




        /// <summary>
        /// Create a descriptor based on a list of value (typically an histogram)
        /// </summary>
        /// <param name="ListOriginalValues">Array of values</param>
        /// <param name="Name">Descriptor name</param>
        //public cDescriptor(double[] ListOriginalValues, cDescriptorsType Type)
        //{


        //    this.OriginalValues = new double[ListOriginalValues.Length];
        //    Array.Copy(ListOriginalValues, this.OriginalValues, OriginalValues.Length);

        //    this.Type = Type;

        //    double Max = ListOriginalValues[0];
        //    for (int i = 1; i < ListOriginalValues.Length; i++)
        //    {
        //        if (ListOriginalValues[i] > Max) Max = ListOriginalValues[i];
        //    }

        //    this.FirstValue = 0;
        //    this.LastValue = Max;

        //    HistoValues = this.CreateHistogram(ListOriginalValues, 0, Max, Type.GetBinNumber());

        //    AverageValue = getAverageValue(ListOriginalValues);
        //    //  if (HistoValues.Length == 1) IsSingle = true;
        //    // else IsSingle = false;
        //}

        /// <summary>
        /// Create a descritpor based on a single value
        /// </summary>
        /// <param name="Value">Descritpor value</param>
        /// <param name="Name">Descritpor name</param>
        public cSignature(double Value, cDescriptorType Type, cScreening CurrentScreening)
        {
            this.CurrentScreening = CurrentScreening;
            this.Type = Type;
            this.Histogram = new cHisto(Value);
            this.HistoBins = 1;
            this.AverageValue = Value;
            //this.FirstValue = this.LastValue = this.AverageValue = this.HistoValues[0] = Value;

            if (cGlobalInfo.CellByCellDataAccess == eCellByCellDataAccess.MEMORY)
            {
                this.OriginalValues = new double[1];
                this.OriginalValues[0] = Value;
            }
        }

        public cSignature(cExtendedList Values, int Bin, cDescriptorType Type, cScreening CurrentScreening)
        {
            this.CurrentScreening = CurrentScreening;
            this.Type = Type;
            this.HistoBins = Bin;
            this.Histogram = new cHisto(Values, HistoBins);

            this.HistoBins = this.Histogram.GetXvalues().Count;
            this.AverageValue = Values.Mean();

            if (cGlobalInfo.CellByCellDataAccess == eCellByCellDataAccess.MEMORY)
            {
                this.OriginalValues = new double[Values.Count];
                Array.Copy(Values.ToArray(), this.OriginalValues, this.OriginalValues.Length);
            }

            //this.FirstValue = FirstValue;
            // this.LastValue = LastValue;

            //this.HistoValues = new cExtendedList();

            //this.HistoValues.AddRange(HistoGram);

            //if (HistoGram.Length < Type.GetBinNumber())
            //{
            //    for (int i = 0; i < Type.GetBinNumber() - HistoGram.Length; i++)
            //        this.HistoValues.Add(0);
            //}


            //    new double[HistoGram.Length];
            //Array.Copy(HistoGram, this.HistoValues, HistoGram.Length);
            //this.Name = Name;
            //AverageValue = getAverageValue(HistoGram);
            //  if (HistoGram.Length == 1) IsSingle = true;
            //  else IsSingle = false;
        }

    }
}
