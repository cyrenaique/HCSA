using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using LibPlateAnalysis;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using System.Windows.Forms.DataVisualization.Charting;
using HCSAnalyzer.Classes.Base_Classes.GUI;

namespace HCSAnalyzer.Classes.General_Types
{
    public class cWellClassType : cObjectWithClass
    {
        cGlobalInfo GlobalInfo;

        public cWellClassType(Color Colour, string Name, cGlobalInfo GlobalInfo)
        {
            base.ColourForDisplay = Colour;
            base.Name = Name;
            this.GlobalInfo = GlobalInfo;
        }

        public ToolStripMenuItem GetExtendedContextMenu()
        {
            #region Context Menu
            base.SpecificContextMenu = new ToolStripMenuItem("Class [" + this.Name + "]");

            ToolStripMenuItem ToolStripMenuItem_DisplayDataTable = new ToolStripMenuItem("Display Data Table");
            ToolStripMenuItem_DisplayDataTable.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayDataTable);
            base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayDataTable);

            ToolStripMenuItem ToolStripMenuItem_DisplayHistograms = new ToolStripMenuItem("Display Histograms");
            ToolStripMenuItem_DisplayHistograms.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayHistograms);
            base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayHistograms);

            if (cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor().IsConnectedToDatabase)
            {
                ToolStripMenuItem ToolStripMenuItem_DisplaySingleCellHistogram = new ToolStripMenuItem("Single Cell Histogram");
                ToolStripMenuItem_DisplaySingleCellHistogram.Click += new System.EventHandler(this.ToolStripMenuItem_DisplaySingleCellHistogram);
                base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplaySingleCellHistogram);
            }


            ToolStripSeparator ToolStripSep = new ToolStripSeparator();
            base.SpecificContextMenu.DropDownItems.Add(ToolStripSep);

            ToolStripMenuItem ToolStripMenuItem_SetAsActivePlate = new ToolStripMenuItem("Set as Active");
            ToolStripMenuItem_SetAsActivePlate.Click += new System.EventHandler(this.ToolStripMenuItem_SetAsActivePlate);
            base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_SetAsActivePlate);
            #endregion
            return base.SpecificContextMenu;
        }

        private void ToolStripMenuItem_DisplaySingleCellHistogram(object sender, EventArgs e)
        {
            cListWells ListWells = new cListWells();
            List<cDescriptorType> LCDT = new List<cDescriptorType>();
            LCDT.Add(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor());

            List<cWellClassType> ListForCurrentClass = new List<cWellClassType>();
            ListForCurrentClass.Add(this);

            cGUI_ListClasses GUIClasses = new cGUI_ListClasses();
            GUIClasses.IsCheckBoxes = true;
            GUIClasses.IsSelectAll = true;
            GUIClasses.ClassType = eClassType.PHENOTYPE;
            if (!GUIClasses.Run().IsSucceed) return;

            cDesignerTab DT = new cDesignerTab();

            if (cGlobalInfo.WindowHCSAnalyzer.ProcessModeEntireScreeningToolStripMenuItem.Checked)
            {
                foreach (cPlate CurrentPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                {
                    ListWells.AddRange(CurrentPlate.ListWells.Filter(ListForCurrentClass));
                }
                    cExtendedTable FinalTable = new cExtendedTable();
                    FinalTable.Name = "Stacked Histogram: " + ListWells.Count + " wells";

                    int Idx = 0;
                    foreach (var item in cGlobalInfo.ListCellularPhenotypes)
                    {
                        FinalTable.Add(new cExtendedList());
                        FinalTable[Idx].Name = item.Name;
                        FinalTable[Idx].Tag = item;
                       
                        Idx++;
                    }

                    foreach (cWell TmpWell in ListWells)
                    {
                        TmpWell.AssociatedPlate.DBConnection = new cDBConnection(TmpWell.AssociatedPlate, TmpWell.SQLTableName);

                        int IDx = 0;
                        foreach (var item in cGlobalInfo.ListCellularPhenotypes)
                        {
                            if (GUIClasses.GetOutPut()[0][IDx] == 0) { IDx++; continue; }
                            List<cCellularPhenotype> ListCellularPhenotypesToBeSelected = new List<cCellularPhenotype>();
                            ListCellularPhenotypesToBeSelected.Add(item);

                            cExtendedTable TmpET = TmpWell.AssociatedPlate.DBConnection.GetWellValues(TmpWell,
                                                     LCDT, ListCellularPhenotypesToBeSelected);

                            if (TmpET.Count > 0) FinalTable[IDx].AddRange(TmpET[0]);
                            IDx++;
                        }
                        TmpWell.AssociatedPlate.DBConnection.CloseConnection();
                    }

                    cViewerStackedHistogram VSH = new cViewerStackedHistogram();
                    VSH.SetInputData(FinalTable);
                    VSH.Chart.BinNumber = LCDT[0].GetBinNumber();
                    VSH.Chart.IsShadow = false;
                    VSH.Chart.IsBorder = false;
                    VSH.Chart.IsXGrid = true;
                    VSH.Chart.IsYGrid = true;
                    VSH.Chart.LabelAxisX = LCDT[0].GetName();

                    VSH.Run();
                    VSH.Chart.Width = 0;
                    VSH.Chart.Height = 0;
                    VSH.GetOutPut().Title = cGlobalInfo.CurrentScreening.GetName();
                    DT.SetInputData(VSH.GetOutPut());
            }
            else
            {
                cListPlates ListPlates = new cListPlates();
                if (cGlobalInfo.WindowHCSAnalyzer.ProcessModeplateByPlateToolStripMenuItem.Checked)
                    ListPlates = cGlobalInfo.CurrentScreening.ListPlatesActive;
                else
                    ListPlates.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());

                foreach (cPlate CurrentPlate in ListPlates)
                {
                    ListWells = CurrentPlate.ListWells.Filter(ListForCurrentClass);
                        
                    cExtendedTable FinalTable = new cExtendedTable();
                    FinalTable.Name = "Stacked Histogram: " + ListWells.Count + " wells - " + CurrentPlate.GetName();

                    int Idx = 0;
                    foreach (var item in cGlobalInfo.ListCellularPhenotypes)
                    {
                        FinalTable.Add(new cExtendedList());
                        FinalTable[Idx].Name = item.Name;
                        FinalTable[Idx].Tag = item;
                        
                        if (GUIClasses.GetOutPut()[0][Idx] == 0)
                        {
                            Idx++;
                            continue;
                        }
                        
                        Idx++;
                    }

                    foreach (cWell TmpWell in ListWells)
                    {
                        TmpWell.AssociatedPlate.DBConnection = new cDBConnection(TmpWell.AssociatedPlate, TmpWell.SQLTableName);

                        int IDx = 0;
                        foreach (var item in cGlobalInfo.ListCellularPhenotypes)
                        {
                            if (GUIClasses.GetOutPut()[0][IDx] == 0) { IDx++; continue; }
                            List<cCellularPhenotype> ListCellularPhenotypesToBeSelected = new List<cCellularPhenotype>();
                            ListCellularPhenotypesToBeSelected.Add(item);

                            cExtendedTable TmpET = TmpWell.AssociatedPlate.DBConnection.GetWellValues(TmpWell,
                                                     LCDT, ListCellularPhenotypesToBeSelected);

                            if (TmpET.Count > 0) FinalTable[IDx].AddRange(TmpET[0]);
                            IDx++;
                        }
                        TmpWell.AssociatedPlate.DBConnection.CloseConnection();
                    }

                    cViewerStackedHistogram VSH = new cViewerStackedHistogram();
                    VSH.SetInputData(FinalTable);
                    VSH.Chart.BinNumber = LCDT[0].GetBinNumber();
                    VSH.Chart.IsShadow = false;
                    VSH.Chart.IsBorder = false;
                    VSH.Chart.IsXGrid = true;
                    VSH.Chart.IsYGrid = true;
                    VSH.Chart.LabelAxisX = LCDT[0].GetName();
                   

                    VSH.Run();
                    VSH.Chart.Width = 0;
                    VSH.Chart.Height = 0;
                    VSH.GetOutPut().Title = CurrentPlate.GetName();
                    DT.SetInputData(VSH.GetOutPut());
                }
            }
            DT.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(DT.GetOutPut());
            DTW.Title = "Single Cell Histograms";
            DTW.Run();
            DTW.Display();
        }


        private void ToolStripMenuItem_SetAsActivePlate(object sender, EventArgs e)
        {
            int PosPlate = cGlobalInfo.WindowHCSAnalyzer.toolStripcomboBoxPlateList.FindStringExact(this.Name);
            cGlobalInfo.WindowHCSAnalyzer.comboBoxClass.Text = this.Name;
        }


        private void ToolStripMenuItem_DisplayHistograms(object sender, EventArgs e)
        {
            if (GlobalInfo == null) return;
            for (int i = 0; i < cGlobalInfo.ListWellClasses.Count; i++)
            {
                if (cGlobalInfo.ListWellClasses[i].Name == this.Name)
                {
                    Idx = i;
                    break;
                }
            }

            if (Idx == -1) return;

            if ((cGlobalInfo.CurrentScreening.ListDescriptors == null) || (cGlobalInfo.CurrentScreening.ListDescriptors.Count == 0)) return;

            cDisplayToWindow CDW1 = new cDisplayToWindow();

            cListWells ListWellsToProcess = new cListWells(null);
            List<cPlate> PlateList = new List<cPlate>();
            cDesignerSplitter DS = new cDesignerSplitter();

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive) PlateList.Add(TmpPlate);

            foreach (cPlate TmpPlate in PlateList)
                foreach (cWell item in TmpPlate.ListActiveWells)
                    if (item.GetCurrentClassIdx() == base.Idx) ListWellsToProcess.Add(item);

            cExtendedTable NewTable2 = ListWellsToProcess.GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
            NewTable2.Name = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName() + " - Histogram - " + PlateList.Count + " plates";

            cViewerStackedHistogram CV2 = new cViewerStackedHistogram();
            CV2.SetInputData(NewTable2);
            CV2.Chart.LabelAxisX = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
            CV2.Chart.IsBorder = false;
            CV2.Chart.Width = 0;
            CV2.Chart.Height = 0;

            //StripLine AverageLine = new StripLine();
            //AverageLine.BackColor = Color.Red;
            //AverageLine.IntervalOffset = GlobalInfo.CurrentScreen.ListDescriptors[Parent.ListDescriptors.CurrentSelectedDescriptor].GetValue();
            //AverageLine.StripWidth = 0.0001;
            //AverageLine.Text = this.ListDescriptors[Parent.ListDescriptors.CurrentSelectedDescriptor].GetValue().ToString("N2");

            CV2.Run();

            //CV2.Chart.ChartAreas[0].AxisX.StripLines.Add(AverageLine);

            DS.SetInputData(CV2.GetOutPut());


            PlateList.Clear();
            PlateList.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());
            ListWellsToProcess.Clear();
            foreach (cPlate TmpPlate in PlateList)
                foreach (cWell item in TmpPlate.ListActiveWells)
                    if (item.GetCurrentClassIdx() == Idx) ListWellsToProcess.Add(item);

            CDW1.Title = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName() + " - Histogram (" + PlateList[0].GetName() + ")";

            cExtendedTable NewTable = ListWellsToProcess.GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
            NewTable.Name = CDW1.Title;

            cViewerStackedHistogram CV1 = new cViewerStackedHistogram();
            CV1.SetInputData(NewTable);
            CV1.Chart.LabelAxisX = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
            CV1.Chart.Width = 0;
            CV1.Chart.Height = 0;

            //  CV1.Chart.ChartAreas[0].AxisX.Minimum = CV2.Chart.ChartAreas[0].AxisX.Minimum;
            //  CV1.Chart.ChartAreas[0].AxisX.Maximum = CV2.Chart.ChartAreas[0].AxisX.Maximum;
            CV1.Run();

            // CV1.Chart.ChartAreas[0].AxisX.StripLines.Add(AverageLine);
            DS.SetInputData(CV1.GetOutPut());
            DS.Run();

            CDW1.SetInputData(DS.GetOutPut());
            CDW1.Run();
            CDW1.Display();

            return;
        }


        private void ToolStripMenuItem_DisplayDataTable(object sender, EventArgs e)
        {
            if (GlobalInfo == null) return;
            cListWells ListWellsToProcess = new cListWells(null);

            for (int i = 0; i < cGlobalInfo.ListWellClasses.Count; i++)
            {
                if (cGlobalInfo.ListWellClasses[i].Name == this.Name)
                {
                    Idx = i;
                    break;
                }
            }

            if (Idx == -1) return;

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                foreach (cWell item in TmpPlate.ListActiveWells)
                    if (item.GetCurrentClassIdx() == Idx) ListWellsToProcess.Add(item);

            cExtendedTable DataFromPlate = new cExtendedTable(ListWellsToProcess, true);
            DataFromPlate.Name = this.Name + " : " + ListWellsToProcess.Count + " wells";
            DataFromPlate.ListRowNames.Clear();

            foreach (var item in ListWellsToProcess)
            {
                DataFromPlate.ListRowNames.Add(item.GetShortInfo());
            }

            cDisplayExtendedTable DEXT = new cDisplayExtendedTable();
            DEXT.SetInputData(DataFromPlate);
            DEXT.Run();


        }

    }

}
