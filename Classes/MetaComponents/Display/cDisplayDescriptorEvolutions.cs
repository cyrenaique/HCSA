using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes.Viewers._1D;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using HCSAnalyzer.Classes.General_Types;
using LibPlateAnalysis;

namespace HCSAnalyzer.Classes.MetaComponents
{
    class cDisplayDescriptorEvolutions : cComponent
    {
        cExtendedTable Input;
        public cListPlates ListPlates = null;

        public cDisplayDescriptorEvolutions()
        {
            this.Title = "Compute and display descriptor evolutions among the current screening";
        }

        public cFeedBackMessage Run(cScreening CompleteScreening)
        {
            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }

            bool IsCurrentDescOnly = false;
            if (this.ListPlates == null)
                ListPlates = CompleteScreening.ListPlatesActive;

            cDisplayToWindow CDW1 = new cDisplayToWindow();

            cDesignerTab DT = new cDesignerTab();

            foreach (cDescriptorType CurrentDesc in CompleteScreening.ListDescriptors.GetActiveDescriptors())
            {
                cListExtendedTable CompleteListOfData = new cListExtendedTable();

                cExtendedTable FullTableAverage = new cExtendedTable();
                FullTableAverage.ListRowNames = new List<string>();

                string TableName = CurrentDesc.GetName() + " evolution\n" + Input[0].Sum() + " classes - ";

                for (int i = 0; i < Input[0].Count; i++)
                {
                    if ((Input[0][i] == 1) && (Input[0].ListTags != null) && (Input[0].ListTags[i].GetType() == typeof(cWellClassType)))
                    {
                        cWellClassType TmpWellClass = (cWellClassType)Input[0].ListTags[i];

                        CompleteListOfData.Add(new cExtendedTable());
                        CompleteListOfData[CompleteListOfData.Count - 1].Name = TmpWellClass.Name;
                        CompleteListOfData[CompleteListOfData.Count - 1].Tag = Input[0].ListTags[i];

                        FullTableAverage.Add(new cExtendedList(TmpWellClass.Name));
                        FullTableAverage[FullTableAverage.Count - 1].ListTags = new List<object>();
                        FullTableAverage[FullTableAverage.Count - 1].Name = TmpWellClass.Name;
                        FullTableAverage[FullTableAverage.Count - 1].Tag = TmpWellClass;

                        int IdxPlate = 0;
                        foreach (cPlate TmpPlate in CompleteScreening.ListPlatesActive)
                        {
                            FullTableAverage[FullTableAverage.Count - 1].Add(0);
                            FullTableAverage[FullTableAverage.Count - 1].ListTags.Add(TmpWellClass);

                            CompleteListOfData[CompleteListOfData.Count - 1].Add(new cExtendedList(TmpPlate.GetName()));
                            CompleteListOfData[CompleteListOfData.Count - 1][CompleteListOfData[CompleteListOfData.Count - 1].Count - 1].Tag = TmpPlate;
                            CompleteListOfData[CompleteListOfData.Count - 1][CompleteListOfData[CompleteListOfData.Count - 1].Count - 1].Add(IdxPlate);

                            IdxPlate++;
                        }
                    }
                }

                TableName += FullTableAverage[0].Count + " plates";
                FullTableAverage.Name = TableName;

                foreach (cPlate TmpPlate in CompleteScreening.ListPlatesActive)
                    FullTableAverage.ListRowNames.Add(TmpPlate.GetName());

              //  cDescriptorsType CurrentDesc = CompleteScreening.ListDescriptors.GetActiveDescriptor();
                int RealIdx = 0;
                for (int i = 0; i < Input[0].Count; i++)
                {
                    if (Input[0][i] == 1)
                    {
                        int IdxPlate = 0;
                        foreach (cPlate TmpPlate in ListPlates/*CompleteScreening.ListPlatesActive*/)
                        {
                            cExtendedList CurrentListValues = new cExtendedList();

                            foreach (cWell item in TmpPlate.ListActiveWells)
                                if ((item.GetCurrentClassIdx() != -1) && (item.GetCurrentClassIdx() == i))
                                {
                                    double Value = item.ListSignatures.GetSignature(CurrentDesc).GetValue();
                                    CurrentListValues.Add(Value);
                                    CompleteListOfData[RealIdx][IdxPlate].Add(Value);
                                }

                            FullTableAverage[RealIdx][IdxPlate] = CurrentListValues.Mean();
                            IdxPlate++;
                        }
                        RealIdx++;
                    }
                }

                cViewerGraph1D VG = new cViewerGraph1D();
                VG.Chart.IsLine = true;
                VG.Chart.IsShadow = true;
                VG.Chart.IsZoomableX = true;
                // VG.Chart.IsYGrid = true;
                //VG.Chart.IsDisplayValues = true;
                VG.Chart.IsLegend = true;
                //cViewerStackedHistogram CV1 = new cViewerStackedHistogram();
                //  CV1.SetInputData(NewTable);

                VG.SetInputData(FullTableAverage);
                VG.SetInputData(CompleteListOfData);


                VG.Chart.LabelAxisX = "Plate";
                VG.Chart.LabelAxisY = CurrentDesc.GetName();
                VG.Run();

                cExtendedControl TmpCtrl = VG.GetOutPut();
                TmpCtrl.Title = CurrentDesc.GetName();

                DT.SetInputData(TmpCtrl);

            }
            DT.Run();

            CDW1.Title = "Descriptor Evolution";
            CDW1.SetInputData(DT.GetOutPut());
            CDW1.Run();
            CDW1.Display();


             
            return FeedBackMessage;
        }


        public void SetInputData(cExtendedTable Input)
        {
            this.Input = Input;
        }
    }
}

