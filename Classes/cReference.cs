using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibPlateAnalysis;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.General_Types;

namespace HCSAnalyzer.Classes
{
    public class cReference : cListWells //: List<cExtendedList>
    {

      //  List<cWell> ListWellsForReference;

        //public List<cWell> GetReferenceWells()
        //{
        //    return this.ListWellsForReference;
        //}

        //public cReference(cListWells WellsForReference)
        //{

        //    this.ListWellsForReference = WellsForReference;
        //    //int IdxDesc = 0;
        //    //// main loop over the descriptors
        //    //foreach (cDescriptor Desc in WellsForReference[0].ListDescriptors)
        //    //{
        //    //    cExtendedList NewList = new cExtendedList();
                
        //    //    for (int i = 0; i < Desc.GetAssociatedType().GetBinNumber(); i++)
        //    //    {
        //    //        double CurrentVal = 0;
        //    //        foreach (cWell CurrentWell in WellsForReference)
        //    //        {
        //    //            CurrentVal += CurrentWell.ListDescriptors[IdxDesc].GetHistovalues()[i];// Desc.Getvalue(i);
        //    //        }
        //    //        CurrentVal /= (double)WellsForReference.Count;
        //    //        NewList.Add(CurrentVal);
        //    //    }
        //    //    this.Add(NewList);
        //    //    IdxDesc++;
        //    //}
        //}


        public cExtendedList GetValues(int DescriptorIdx)
        {
            cExtendedList ListValue = new cExtendedList();
            foreach (cWell TmpWell in this)
            {
                ListValue.AddRange(TmpWell.ListSignatures[DescriptorIdx].GetOriginalValues());
            }
            return ListValue;
        }

        public Chart GetChart(int DescriptorIdx)
        {
            //if (ListDescriptors[CurrentDescriptorToDisplay].GetAssociatedType().GetBinNumber() == 1) return null;


            List<double[]> CurrentHisto = GetValues(DescriptorIdx).CreateHistogram(100, false);


            Series CurrentSeries = new Series();
            //CurrentSeries.ShadowOffset = 2;

            for (int IdxValue = 0; IdxValue < CurrentHisto[0].Length; IdxValue++)
                CurrentSeries.Points.AddXY(CurrentHisto[0][IdxValue], CurrentHisto[1][IdxValue]);

            ChartArea CurrentChartArea = new ChartArea("ChartArea" + DescriptorIdx);
            CurrentChartArea.BorderColor = Color.White;

            Chart ChartToReturn = new Chart();
            ChartToReturn.ChartAreas.Add(CurrentChartArea);
            // ChartToReturn.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            CurrentChartArea.BackColor = Color.White;

            CurrentChartArea.Axes[1].LabelStyle.Enabled = false;
            CurrentChartArea.Axes[1].MajorGrid.Enabled = false;
            CurrentChartArea.Axes[0].Enabled = AxisEnabled.False;
            CurrentChartArea.Axes[1].Enabled = AxisEnabled.False;


            CurrentChartArea.Axes[0].MajorGrid.Enabled = false;
            //  CurrentChartArea.Axes[0].Title = ListDescriptors[CurrentDescriptorToDisplay].GetName();
            CurrentSeries.ChartType = SeriesChartType.Line;
            CurrentSeries.Color = Color.Black;
            // CurrentSeries.BorderWidth = 3;
            CurrentSeries.ChartArea = "ChartArea" + DescriptorIdx;

           // CurrentSeries.Name = "Series" + PosX + "x" + PosY;
            ChartToReturn.Series.Add(CurrentSeries);

            Title CurrentTitle = new Title("Reference");
            // ChartToReturn.Titles.Add(CurrentTitle);

           // ChartToReturn.Width = 100;
          //  ChartToReturn.Height = 48;

           // ChartToReturn.Update();
            //  ChartToReturn.Show();

            return ChartToReturn;

        }



    }
}
