using System.Windows.Forms;

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using LibPlateAnalysis;
using weka.core;
using System.IO;
using HCSAnalyzer.Forms;
using HCSAnalyzer.Controls;
using System.Reflection;
using System.Collections;
using HCSAnalyzer.Classes;
using System.Resources;
using HCSPlugin;
using HCSAnalyzer.Forms.FormsForOptions;
using HCSAnalyzer.Forms.FormsForDRCAnalysis;
using analysis;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using System.Linq;
//using Emgu.CV;
//using Emgu.CV.Structure;
//using Emgu.CV.CvEnum;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Data.SQLite;
using HCSAnalyzer.Forms.ClusteringForms;
using System.Diagnostics;
using System.Threading.Tasks;
using HCSAnalyzer.Forms.IO;
using ImageAnalysis;
using HCSAnalyzer.Forms.FormsForGraphsDisplay.ForClassSelection;
using HCSAnalyzer.Simulator.Classes;
using HCSAnalyzer.Simulator.Forms;
using HCSAnalyzer.Forms.FormsForOptions.ClassForOptions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using HCSAnalyzer.Classes.Machine_Learning;
using HCSAnalyzer.Forms.FormsForOptions.ClassForOptions.Children;
using HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.Base_Classes.Data;
//using RDotNet;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Classes.Base_Classes.GUI;
using HCSAnalyzer.Classes.DataAnalysis;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes.Base_Classes.Viewers._1D;
using System.Net;
using HCSAnalyzer.Cell_by_Cell_and_DB;
using ImageAnalysisFiltering;
using HCSAnalyzer.Classes.ImageAnalysis._3D_Engine;

namespace HCSAnalyzer
{
    public partial class HCSAnalyzer : Form
    {

        public void UpdateQCDisplay()
        {
            // get the first class list of wells
            List<cWellClassType> WT = new List<cWellClassType>();
            WT.Add(cGlobalInfo.CtrlNeg);
            List<cDescriptorType> LDT = new List<cDescriptorType>();
            LDT.Add(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor());
            cListWells TmpList = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListWells.Filter(WT);
            if ((TmpList == null) || (TmpList.Count == 0))
            {
                cGlobalInfo.WindowHCSAnalyzer.labelQC.Text = double.NaN.ToString();
                cGlobalInfo.WindowHCSAnalyzer.labelQC.Update();
                return;
            }


            cExtendedList EL1 = TmpList.GetAverageDescriptorValues(LDT, false, false)[0];

            WT[0] = cGlobalInfo.CtrlPos;
            TmpList = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListWells.Filter(WT);
            if ((TmpList == null) || (TmpList.Count == 0))
            {
                cGlobalInfo.WindowHCSAnalyzer.labelQC.Text = double.NaN.ToString();
                cGlobalInfo.WindowHCSAnalyzer.labelQC.Update();
                return;
            }

            cExtendedList EL2 = TmpList.GetAverageDescriptorValues(LDT, false, false)[0];

            bool IsRobust = false;
            if (comboBoxQC.SelectedItem.ToString() == "RZ'")
                IsRobust = true;

            double ZF = EL1.ZFactor(EL2, IsRobust);

            cGlobalInfo.WindowHCSAnalyzer.labelQC.Text = ZF.ToString("N3");
            cGlobalInfo.WindowHCSAnalyzer.labelQC.Update();

        }


        #region Math Tools
        public double std(double[] input)
        {
            double var = 0f, mean = Mean(input);
            foreach (double f in input) var += (f - mean) * (f - mean);
            return Math.Sqrt(var / (double)(input.Length - 1));
        }

        public double Variance(double[] input)
        {
            double var = 0f, mean = Mean(input);
            foreach (double f in input) var += (f - mean) * (f - mean);
            return var / (double)(input.Length - 1);
        }

        public double Mean(double[] input)
        {
            double mean = 0f;
            foreach (double f in input) mean += f;
            return mean / (double)input.Length;
        }

        private double[] CreateGauss(double p, double p_2, int p_3)
        {
            double[] Gauss = new double[p_3];

            for (int x = 0; x < p_3; x++)
            {
                Gauss[x] = Math.Exp(-((x - p) * (x - p)) / (2 * p_2 * p_2));
            }

            return Gauss;
        }

        public List<double[]> CreateHistogram(double[] data, double Bin)
        {
            List<double[]> ToReturn = new List<double[]>();

            //float max = math.Max(data);
            if (data.Length == 0) return ToReturn;
            double Max = data[0];
            double Min = data[0];

            for (int Idx = 1; Idx < data.Length; Idx++)
            {
                if (data[Idx] > Max) Max = data[Idx];
                if (data[Idx] < Min) Min = data[Idx];
            }

            double step = (Max - Min) / Bin;

            int HistoSize = (int)((Max - Min) / step) + 1;
            if (HistoSize <= 0) HistoSize = 1;
            double[] axeX = new double[HistoSize];
            for (int i = 0; i < HistoSize; i++)
            {
                axeX[i] = Min + i * step;
            }
            ToReturn.Add(axeX);

            double[] histogram = new double[HistoSize];
            //double RealPos = Min;

            int PosHisto;
            foreach (double f in data)
            {
                PosHisto = (int)((f - Min) / step);
                if ((PosHisto >= 0) && (PosHisto < HistoSize))
                    histogram[PosHisto]++;
            }
            ToReturn.Add(histogram);

            return ToReturn;
        }

        public double[] MeanCenteringStdStandarization(double[] input)
        {
            double mean = Mean(input);
            double Stdev = std(input);

            double[] OutPut = new double[input.Length];

            for (int i = 0; i < input.Length; i++)
                OutPut[i] = input[i] - mean;

            for (int i = 0; i < input.Length; i++)
                OutPut[i] = OutPut[i] / Stdev;

            return OutPut;
        }



        #endregion

        #region User Interface Functions
        private void tabControlMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void tabControlMain_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files[0].Remove(0, files[0].Length - 4) == ".csv")
                {
                    FormForImportExcel CSVFeedBackWindow = LoadCSVAssay(files, false);
                    if (CSVFeedBackWindow == null) return;
                    if (CSVFeedBackWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
                    ProcessOK(CSVFeedBackWindow);

                    UpdateUIAfterLoading();
                }
            }
            return;
        }

        #region Descriptor List UI management

        public List<ToolStripMenuItem> GetGeneralListDescMenu()
        {

            List<ToolStripMenuItem> ListItemToReturn = new List<System.Windows.Forms.ToolStripMenuItem>();


            ToolStripMenuItem MenuSelection = new System.Windows.Forms.ToolStripMenuItem("Selection");

            ToolStripMenuItem UnselectItem = new ToolStripMenuItem("Unselect all");
            UnselectItem.Click += new System.EventHandler(this.UnselectItem);
            MenuSelection.DropDownItems.Add(UnselectItem);

            ToolStripMenuItem SelectAllItem = new ToolStripMenuItem("Select all");
            SelectAllItem.Click += new System.EventHandler(this.SelectAllItem);
            MenuSelection.DropDownItems.Add(SelectAllItem);

            ToolStripMenuItem InverseSelection = new ToolStripMenuItem("Inverse Selection");
            InverseSelection.Click += new System.EventHandler(this.InverseSelection);
            MenuSelection.DropDownItems.Add(InverseSelection);


            ToolStripMenuItem SelectionFilter = new ToolStripMenuItem("Selection Filter");
            SelectionFilter.Click += new System.EventHandler(this.SelectionFilter);
            MenuSelection.DropDownItems.Add(SelectionFilter);


            MenuSelection.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem DisplayAsDataTable = new ToolStripMenuItem("Display as Data Table");
            DisplayAsDataTable.Click += new System.EventHandler(this.DisplayAsDataTable);
            MenuSelection.DropDownItems.Add(DisplayAsDataTable);

            MenuSelection.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem CopySelectionItem = new ToolStripMenuItem("Copy Selection");
            CopySelectionItem.Click += new System.EventHandler(this.CopySelectionItem);
            MenuSelection.DropDownItems.Add(CopySelectionItem);

            ToolStripMenuItem PasteSelectionItem = new ToolStripMenuItem("Paste Selection");
            PasteSelectionItem.Click += new System.EventHandler(this.PasteSelectionItem);
            if (cGlobalInfo.CurrentListDescriptorSelected == null)
                PasteSelectionItem.Enabled = false;
            else
                PasteSelectionItem.Enabled = true;
            MenuSelection.DropDownItems.Add(PasteSelectionItem);



            MenuSelection.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem RemoveSelectedItem = new ToolStripMenuItem("Remove Selected Descriptors");
            RemoveSelectedItem.Click += new System.EventHandler(this.RemoveSelectedItem);
            MenuSelection.DropDownItems.Add(RemoveSelectedItem);

            ListItemToReturn.Add(MenuSelection);

            return ListItemToReturn;
        }




        private void checkedListBoxActiveDescriptors_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button != System.Windows.Forms.MouseButtons.Right) || (cGlobalInfo.CurrentScreening == null)) return;

            ContextMenuStrip contextMenuStripActorPicker = new ContextMenuStrip();

            List<ToolStripMenuItem> ListMenus = GetGeneralListDescMenu();
            foreach (var item in ListMenus) contextMenuStripActorPicker.Items.Add(item);

            contextMenuStripActorPicker.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripGenerateMenuItems = new ToolStripMenuItem("Generate Descriptor");

            ToolStripMenuItem ConcentrationToDescriptorItem = new ToolStripMenuItem("Concentration to Descriptor");
            ConcentrationToDescriptorItem.Click += new System.EventHandler(this.ConcentrationToDescriptorItem);
            ToolStripGenerateMenuItems.DropDownItems.Add(ConcentrationToDescriptorItem);

            ToolStripMenuItem ColumnToDescriptorItem = new ToolStripMenuItem("Column to Descriptor");
            ColumnToDescriptorItem.Click += new System.EventHandler(this.ColumnToDescriptorItem);
            ToolStripGenerateMenuItems.DropDownItems.Add(ColumnToDescriptorItem);

            ToolStripMenuItem RowToDescriptorItem = new ToolStripMenuItem("Row to Descriptor");
            RowToDescriptorItem.Click += new System.EventHandler(this.RowToDescriptorItem);
            ToolStripGenerateMenuItems.DropDownItems.Add(RowToDescriptorItem);

            ToolStripMenuItem ClassToDescriptorItem = new ToolStripMenuItem("Class to Descriptor");
            ClassToDescriptorItem.Click += new System.EventHandler(this.ClassToDescriptorItem);
            ToolStripGenerateMenuItems.DropDownItems.Add(ClassToDescriptorItem);

            ToolStripMenuItem ClassificationConfidenceToDescriptorItem = new ToolStripMenuItem("Classification Confidence to Descriptor");
            ClassificationConfidenceToDescriptorItem.Click += new System.EventHandler(this.ClassificationConfidenceToDescriptorItem);
            ToolStripGenerateMenuItems.DropDownItems.Add(ClassificationConfidenceToDescriptorItem);

            ToolStripGenerateMenuItems.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem RatioPopDescItem = new ToolStripMenuItem("Population Ratio");
            RatioPopDescItem.ToolTipText = "Generate a single value descriptor containing the ratio of a specific cell population over other one(s)";
            RatioPopDescItem.Click += new System.EventHandler(this.RatioPopDescItem);
            ToolStripGenerateMenuItems.DropDownItems.Add(RatioPopDescItem);

            ToolStripMenuItem PopValueDescItem = new ToolStripMenuItem("Population Value");
            PopValueDescItem.ToolTipText = "Generate a single value descriptor containing the number of element of a population over other one(s)";
            PopValueDescItem.Click += new System.EventHandler(this.PopValueDescItem);
            ToolStripGenerateMenuItems.DropDownItems.Add(PopValueDescItem);

            ToolStripMenuItem SingleCellOperationPhenotypToDesc = new ToolStripMenuItem("Phenotypic Class to Descriptor");
            SingleCellOperationPhenotypToDesc.ToolTipText = "Save the current phenotypic class to a new single cell feature";
            SingleCellOperationPhenotypToDesc.Click += new System.EventHandler(this.SingleCellOperationPhenotypToDesc);
            ToolStripGenerateMenuItems.DropDownItems.Add(SingleCellOperationPhenotypToDesc);

            ToolStripMenuItem SingleCellOperationPhenotypeConfidenceToDesc = new ToolStripMenuItem("Phenotypic Class Confidence to Descriptor");
            SingleCellOperationPhenotypeConfidenceToDesc.ToolTipText = "Save the current phenotypic classification confidence to a new single cell feature";
            SingleCellOperationPhenotypeConfidenceToDesc.Click += new System.EventHandler(this.SingleCellOperationPhenotypeConfidenceToDesc);
            ToolStripGenerateMenuItems.DropDownItems.Add(SingleCellOperationPhenotypeConfidenceToDesc);

            ToolStripMenuItem ToolStripConvertMenuItems = new ToolStripMenuItem("Operations");

            if (cGlobalInfo.WindowHCSAnalyzer.checkedListBoxActiveDescriptors.CheckedItems.Count >= 2)
            {
                //   ToolStripSeparator ToolStripSep = new ToolStripSeparator();
                //   ToolStripConvertMenuItems.DropDownItems.Add(ToolStripSep);
                ToolStripMenuItem SumCheckedDescToDescriptorItem = new ToolStripMenuItem("Sum Checked Descriptors");
                SumCheckedDescToDescriptorItem.Click += new System.EventHandler(this.SumCheckedDescToDescriptorItem);
                ToolStripConvertMenuItems.DropDownItems.Add(SumCheckedDescToDescriptorItem);

                ToolStripMenuItem MultiplyCheckedDescToDescriptorItem = new ToolStripMenuItem("Multiply Checked Descriptors");
                MultiplyCheckedDescToDescriptorItem.Click += new System.EventHandler(this.MultiplyCheckedDescToDescriptorItem);
                ToolStripConvertMenuItems.DropDownItems.Add(MultiplyCheckedDescToDescriptorItem);
            }

            ToolStripMenuItem NormalizeCheckedDescToDescriptorItem = new ToolStripMenuItem("Normalize Checked Descriptors");
            NormalizeCheckedDescToDescriptorItem.Click += new System.EventHandler(this.NormalizeCheckedDescToDescriptorItem);
            ToolStripConvertMenuItems.DropDownItems.Add(NormalizeCheckedDescToDescriptorItem);

            ToolStripMenuItem GenerateLADDescriptorItem = new ToolStripMenuItem("LDA Optimized Descriptor");
            GenerateLADDescriptorItem.Click += new System.EventHandler(this.GenerateLADDescriptorItem);
            GenerateLADDescriptorItem.Enabled = false;
            ToolStripConvertMenuItems.DropDownItems.Add(GenerateLADDescriptorItem);

            ToolStripMenuItem GeneratePCADescriptorItem = new ToolStripMenuItem("PCA Projection");
            GeneratePCADescriptorItem.Click += new System.EventHandler(this.GeneratePCADescriptorItem);
            GeneratePCADescriptorItem.Enabled = false;
            ToolStripConvertMenuItems.DropDownItems.Add(GeneratePCADescriptorItem);

            ToolStripMenuItem GenerateManualLinearDescriptorItem = new ToolStripMenuItem("Manual Linear Projection");
            GenerateManualLinearDescriptorItem.Click += new System.EventHandler(this.GenerateManualLinearDescriptorItem);
            ToolStripConvertMenuItems.DropDownItems.Add(GenerateManualLinearDescriptorItem);

            ToolStripMenuItem GenerateRandomProjectionDescriptorItem = new ToolStripMenuItem("Random Projection");
            GenerateRandomProjectionDescriptorItem.Click += new System.EventHandler(this.GenerateRandomProjectionDescriptorItem);
            GenerateRandomProjectionDescriptorItem.Enabled = false;
            ToolStripConvertMenuItems.DropDownItems.Add(GenerateRandomProjectionDescriptorItem);

            ToolStripConvertMenuItems.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem AverageOperationsToDescriptorItem = new ToolStripMenuItem("Average Based Operations");
            AverageOperationsToDescriptorItem.Click += new System.EventHandler(this.AverageOperationsToDescriptorItem);
            ToolStripConvertMenuItems.DropDownItems.Add(AverageOperationsToDescriptorItem);

            ToolStripMenuItem SingleCellOperationsToDescriptorItem = new ToolStripMenuItem("Single Cell Based Operations");
            SingleCellOperationsToDescriptorItem.Click += new System.EventHandler(this.SingleCellOperationsToDescriptorItem);

            bool NumSingleCell = false;

            foreach (var item in cGlobalInfo.CurrentScreening.ListDescriptors)
            {
                if (item.IsConnectedToDatabase)
                {
                    NumSingleCell = true;
                }
            }

            SingleCellOperationsToDescriptorItem.Enabled = NumSingleCell;
            ToolStripConvertMenuItems.DropDownItems.Add(SingleCellOperationsToDescriptorItem);

            Point locationOnForm = checkedListBoxActiveDescriptors.FindForm().PointToClient(Control.MousePosition);

            int IdxItem = checkedListBoxActiveDescriptors.IndexFromPoint(e.Location);// locationOnForm.Y - 163;
            //int ItemHeight = checkedListBoxActiveDescriptors.GetItemHeight(0);
            // = VertPos / ItemHeight;
            IntToTransfer = IdxItem;

            if ((IdxItem < cGlobalInfo.CurrentScreening.ListDescriptors.Count) && ((IdxItem >= 0)))
            {
                List<ToolStripMenuItem> ToolStripMenuItems = cGlobalInfo.CurrentScreening.ListDescriptors[IdxItem].GetExtendedContextMenu();

                //new ToolStripMenuItem(CompleteScreening.ListDescriptors[IdxItem].GetName());

                //ToolStripMenuItem InfoDescItem = new ToolStripMenuItem("Info");
                //IntToTransfer = IdxItem;
                //InfoDescItem.Click += new System.EventHandler(this.InfoDescItem);
                //ToolStripMenuItems.DropDownItems.Add(InfoDescItem);

                //ToolStripMenuItem StackedHistoDescItem = new ToolStripMenuItem("Stacked Histo.");
                //StackedHistoDescItem.Click += new System.EventHandler(this.StackedHistoDescItem);
                //ToolStripMenuItems.DropDownItems.Add(StackedHistoDescItem);

                //if (CompleteScreening.ListDescriptors.Count >= 2)
                //{
                //    ToolStripMenuItem RemoveDescItem = new ToolStripMenuItem("Remove");
                //    RemoveDescItem.Click += new System.EventHandler(this.RemoveDescItem);
                //    ToolStripMenuItems.DropDownItems.Add(RemoveDescItem);
                //}

                if (cGlobalInfo.CurrentScreening.ListDescriptors[IdxItem].GetBinNumber() > 1)
                {
                    ToolStripMenuItems[0].DropDownItems.Add(new ToolStripSeparator());

                    ToolStripMenuItem SplitDescItem = new ToolStripMenuItem("Split");
                    SplitDescItem.ToolTipText = "Split the histogram bins in individual descriptor";
                    SplitDescItem.Click += new System.EventHandler(this.SplitDescItem);
                    ToolStripMenuItems[0].DropDownItems.Add(SplitDescItem);

                    ToolStripMenuItem AverageDescItem = new ToolStripMenuItem("Sub-population Statistics");
                    AverageDescItem.ToolTipText = "Generate a single value descriptor, resulting from the single cell statistics values over a defined phenotype";
                    AverageDescItem.Click += new System.EventHandler(this.SubPopulationStatDescItem);
                    ToolStripMenuItems[0].DropDownItems.Add(AverageDescItem);
                }

                if (cGlobalInfo.CurrentScreening.ListDescriptors[IdxItem].GetBinNumber() == 1)
                {
                    ToolStripGenerateMenuItems.DropDownItems.Add(new ToolStripSeparator());

                    ToolStripMenuItem AddCorrelatedDescItem = new ToolStripMenuItem("Generate Square");
                    AddCorrelatedDescItem.Click += new System.EventHandler(this.AddCorrelatedSquareDescItem);
                    ToolStripGenerateMenuItems.DropDownItems.Add(AddCorrelatedDescItem);

                    ToolStripMenuItem AddCorrelatedSineDescItem = new ToolStripMenuItem("Generate Sine");
                    AddCorrelatedSineDescItem.Click += new System.EventHandler(this.AddCorrelatedSineDescItem);
                    ToolStripGenerateMenuItems.DropDownItems.Add(AddCorrelatedSineDescItem);

                    ToolStripMenuItem AddCorrelatedCosineDescItem = new ToolStripMenuItem("Generate Cosine");
                    AddCorrelatedCosineDescItem.Click += new System.EventHandler(this.AddCorrelatedCosineDescItem);
                    ToolStripGenerateMenuItems.DropDownItems.Add(AddCorrelatedCosineDescItem);

                    //ToolStripMenuItem AddCorrelatedExpDescItem = new ToolStripMenuItem("Generate Exp.");
                    //AddCorrelatedExpDescItem.Click += new System.EventHandler(this.AddCorrelatedExpDescItem);
                    //ToolStripGenerateMenuItems.DropDownItems.Add(AddCorrelatedExpDescItem);

                    ToolStripGenerateMenuItems.DropDownItems.Add(new ToolStripSeparator());

                    ToolStripMenuItem DuplicateDescItem = new ToolStripMenuItem("Duplicate");
                    DuplicateDescItem.Click += new System.EventHandler(this.DuplicateDescItem);
                    ToolStripGenerateMenuItems.DropDownItems.Add(DuplicateDescItem);
                }
                //if (GlobalInfo.WindowHCSAnalyzer.checkedListBoxActiveDescriptors.CheckedItems.Count >= 2)
                //{
                //    contextMenuStripActorPicker.Items.Add(ToolStripGenerateMenuItems);

                //    foreach (var item in ToolStripMenuItems)
                //        contextMenuStripActorPicker.Items.Add(item);
                //}
                //else
                {
                    contextMenuStripActorPicker.Items.Add(ToolStripGenerateMenuItems);

                    contextMenuStripActorPicker.Items.Add(ToolStripConvertMenuItems);

                    foreach (var item in ToolStripMenuItems)
                        contextMenuStripActorPicker.Items.Add(item);
                }
            }
            else
            {
                //  if (GlobalInfo.WindowHCSAnalyzer.checkedListBoxActiveDescriptors.CheckedItems.Count >= 2)
                contextMenuStripActorPicker.Items.AddRange(new ToolStripItem[] { ToolStripGenerateMenuItems, ToolStripConvertMenuItems });
                //    else
                //    contextMenuStripActorPicker.Items.AddRange(new ToolStripItem[] { ToolStripGenerateMenuItems });
            }


            contextMenuStripActorPicker.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripInfoDescriptor = new ToolStripMenuItem("Info");

            ToolStripInfoDescriptor.Click += new System.EventHandler(this.ToolStripInfoDescriptor);
            contextMenuStripActorPicker.Items.Add(ToolStripInfoDescriptor);

            contextMenuStripActorPicker.Show(Control.MousePosition);
        }

        static int IntToTransfer;
        //void RemoveDescItem(object sender, EventArgs e)
        //{
        //    System.Windows.Forms.DialogResult ResWin = MessageBox.Show("By applying this process, the selected descriptor will be definitively removed from this analysis ! Proceed ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //    if (ResWin == System.Windows.Forms.DialogResult.No) return;
        //    CompleteScreening.ListDescriptors.RemoveDesc(CompleteScreening.ListDescriptors[IntToTransfer], CompleteScreening);


        //    //CompleteScreening.UpDatePlateListWithFullAvailablePlate();
        //    for (int idxP = 0; idxP < CompleteScreening.ListPlatesActive.Count; idxP++)
        //        CompleteScreening.ListPlatesActive[idxP].UpDataMinMax();
        //    CompleteScreening.GetCurrentDisplayPlate().DisplayDistribution(CompleteScreening.ListDescriptors.CurrentSelectedDescriptor, false);
        //}

        void ToolStripInfoDescriptor(object sender, EventArgs e)
        {
            cDisplayText DT = new cDisplayText();

            DT.SetInputData(cGlobalInfo.CurrentScreening.ListDescriptors.GetInfo());
            DT.Title = "Descriptors info";
            DT.Run();
        }



        void DisplayAsDataTable(object sender, EventArgs e)
        {
            cDisplayExtendedTable DET = new cDisplayExtendedTable();
            DET.SetInputData(cGlobalInfo.CurrentScreening.ListDescriptors.GetDataTable());
            DET.Run();
        }

        void RemoveSelectedItem(object sender, EventArgs e)
        {
            if (checkedListBoxActiveDescriptors.Items.Count == cGlobalInfo.CurrentScreening.ListDescriptors.GetListNameActives().Count)
            {
                MessageBox.Show("You cannot remove all the descriptors !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            System.Windows.Forms.DialogResult ResWin = MessageBox.Show("By applying this process, the selected descriptor will be definitively removed from this analysis ! Proceed ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (ResWin == System.Windows.Forms.DialogResult.No) return;

            int NumDesc = checkedListBoxActiveDescriptors.Items.Count;
            for (int i = 0; i < cGlobalInfo.CurrentScreening.ListDescriptors.Count; i++)
            {
                if ((cGlobalInfo.CurrentScreening.ListDescriptors[i].IsActive()))
                {
                    //CompleteScreening.ListDescriptors[i].;
                    cGlobalInfo.CurrentScreening.ListDescriptors.RemoveDesc(cGlobalInfo.CurrentScreening.ListDescriptors[i]);
                    i--;
                }
            }

            //CompleteScreening.UpDatePlateListWithFullAvailablePlate();
            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
        }



        //void InfoDescItem(object sender, EventArgs e)
        //{
        //    CompleteScreening.ListDescriptors[IntToTransfer].WindowDescriptorInfo.ShowDialog();
        //    CompleteScreening.ListDescriptors.UpDateDisplay();
        //}

        //void StackedHistoDescItem(object sender, EventArgs e)
        //{
        //    DisplayStackedHisto(IntToTransfer);
        //}


        void UnselectItem(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxActiveDescriptors.Items.Count; i++)
                cGlobalInfo.CurrentScreening.ListDescriptors.SetItemState(i, false);

            RefreshInfoScreeningRichBox();

            if (cGlobalInfo.ViewMode == eViewMode.PIE)
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors[0], false);

        }

        void SelectionFilter(object sender, EventArgs e)
        {
            FormForDescFiltering WinForDescFiltering = new FormForDescFiltering(GlobalInfo);
            if (WinForDescFiltering.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            string FilterName = WinForDescFiltering.textBoxTextFilter.Text;
            bool ActionState1 = WinForDescFiltering.radioButtonIsSelect.Checked;


            if (WinForDescFiltering.checkBoxCaseSensitive.Checked == false)
            {
                FilterName = FilterName.ToLower();

                for (int i = 0; i < checkedListBoxActiveDescriptors.Items.Count; i++)
                {
                    if (cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName().ToLower().Contains(FilterName))
                        cGlobalInfo.CurrentScreening.ListDescriptors.SetItemState(i, ActionState1);
                }
            }
            else
            {
                for (int i = 0; i < checkedListBoxActiveDescriptors.Items.Count; i++)
                {
                    if (cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName().Contains(FilterName))
                        cGlobalInfo.CurrentScreening.ListDescriptors.SetItemState(i, ActionState1);
                }

            }
            RefreshInfoScreeningRichBox();

            if (cGlobalInfo.ViewMode == eViewMode.PIE)
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors[0], false);
        }

        void CopySelectionItem(object sender, EventArgs e)
        {
            cGlobalInfo.CurrentListDescriptorSelected = new List<cDescriptorType>();

            for (int i = 0; i < checkedListBoxActiveDescriptors.Items.Count; i++)
            {
                if (cGlobalInfo.CurrentScreening.ListDescriptors[i].IsActive())
                    cGlobalInfo.CurrentListDescriptorSelected.Add(cGlobalInfo.CurrentScreening.ListDescriptors[i]);
            }
        }

        void PasteSelectionItem(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxActiveDescriptors.Items.Count; i++)
            {
                bool toBeChecked = false;

                foreach (var item in cGlobalInfo.CurrentListDescriptorSelected)
                {
                    if (item == cGlobalInfo.CurrentScreening.ListDescriptors[i])
                    {
                        toBeChecked = true;
                        break;
                    }
                }
                cGlobalInfo.CurrentScreening.ListDescriptors.SetItemState(i, toBeChecked);
            }

            RefreshInfoScreeningRichBox();
        }

        void InverseSelection(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxActiveDescriptors.Items.Count; i++)
                cGlobalInfo.CurrentScreening.ListDescriptors.SetItemState(i, !cGlobalInfo.CurrentScreening.ListDescriptors[i].IsActive());

            RefreshInfoScreeningRichBox();

            if (cGlobalInfo.ViewMode == eViewMode.PIE)
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors[0], false);

        }

        void SelectAllItem(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxActiveDescriptors.Items.Count; i++)
                cGlobalInfo.CurrentScreening.ListDescriptors.SetItemState(i, true);

            RefreshInfoScreeningRichBox();

            if (cGlobalInfo.ViewMode == eViewMode.PIE)
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors[0], false);

        }

        private void AverageOperationsToDescriptorItem(object sender, EventArgs e)
        {
            FormForDescOperations MainWindow = new FormForDescOperations(cGlobalInfo.CurrentScreening.ListDescriptors.GetListNames());
            MainWindow.Text = "Average Based Operations";
            if (MainWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            string NewDescName = MainWindow.textBoxNewDescName.Text;
            cDescriptorType DescType1 = cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorByName(MainWindow.comboBoxDescriptor1.Text);
            cDescriptorType DescType2 = cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorByName(MainWindow.comboBoxDescriptor2.Text);
            if ((DescType1 == null) || (DescType2 == null)) return;

            FormForProgress ProgressWindow = new FormForProgress();
            ProgressWindow.Show();
            int IdxProgress = 1;
            int MaxProgress = cGlobalInfo.CurrentScreening.ListPlatesAvailable.Count;
            ProgressWindow.progressBar.Maximum = MaxProgress;

            cDescriptorType NewDescType = new cDescriptorType(NewDescName, true, 1);
            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(NewDescType);

            eBinaryOperationType PostProcessBinaryOp = eBinaryOperationType.UNDEFINED;

            if (MainWindow.checkBoxActivePostProcess.Checked)
            {
                switch (MainWindow.domainUpDownPostProcessOperator.Text)
                {
                    case "*":
                        PostProcessBinaryOp = eBinaryOperationType.MULTIPLY;
                        break;
                    case "+":
                        PostProcessBinaryOp = eBinaryOperationType.ADD;
                        break;
                    case "/":
                        PostProcessBinaryOp = eBinaryOperationType.DIVIDE;
                        break;
                    case "-":
                        PostProcessBinaryOp = eBinaryOperationType.SUBSTRACT;
                        break;
                    default:
                        return;
                }
            }

            if (MainWindow.tabControlMain.SelectedTab.Name == "tabPageBinary")
            {
                // SingleCellOperation.ListDualOperations = new List<eBinaryOperationType>();
                //  eDualOperationType OperationType = eDualOperationType.ADD;
                eBinaryOperationType BinaryOp = eBinaryOperationType.ADD;


                switch (MainWindow.domainUpDown1.Text)
                {
                    case "*":
                        BinaryOp = eBinaryOperationType.MULTIPLY;
                        break;
                    case "+":
                        BinaryOp = eBinaryOperationType.ADD;
                        break;
                    case "/":
                        BinaryOp = eBinaryOperationType.DIVIDE;
                        break;
                    case "-":
                        BinaryOp = eBinaryOperationType.SUBSTRACT;
                        break;
                    default:
                        return;
                }

                foreach (cPlate CurrentPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
                {
                    foreach (cWell Tmpwell in CurrentPlate.ListActiveWells)
                    {
                        cListSignature LDesc = new cListSignature();

                        cSignature NewDesc = null;
                        double Val = 0;

                        switch (BinaryOp)
                        {
                            case eBinaryOperationType.ADD:
                                Val = Tmpwell.GetAverageValue(DescType1) + Tmpwell.GetAverageValue(DescType2);
                                break;
                            case eBinaryOperationType.MULTIPLY:
                                Val = Tmpwell.GetAverageValue(DescType1) * Tmpwell.GetAverageValue(DescType2);
                                break;
                            case eBinaryOperationType.SUBSTRACT:
                                Val = Tmpwell.GetAverageValue(DescType1) - Tmpwell.GetAverageValue(DescType2);
                                break;
                            case eBinaryOperationType.DIVIDE:
                                double denominator = Tmpwell.GetAverageValue(DescType2);
                                if (denominator == 0) Val = 0;
                                else Val = Tmpwell.GetAverageValue(DescType1) / denominator;
                                break;
                            default:
                                break;
                        }


                        if (PostProcessBinaryOp != eBinaryOperationType.UNDEFINED)
                        {
                            double ValuePostProcess = (double)MainWindow.numericUpDownPostProcessValue.Value;

                            switch (PostProcessBinaryOp)
                            {
                                case eBinaryOperationType.ADD:
                                    Val += ValuePostProcess;
                                    break;
                                case eBinaryOperationType.SUBSTRACT:
                                    Val -= ValuePostProcess;
                                    break;
                                case eBinaryOperationType.MULTIPLY:
                                    Val *= ValuePostProcess;
                                    break;
                                case eBinaryOperationType.DIVIDE:
                                    if (ValuePostProcess == 0)
                                        Val = 0;
                                    else
                                        Val /= ValuePostProcess;
                                    break;
                                default:
                                    break;
                            }

                        }


                        NewDesc = new cSignature(Val, NewDescType, cGlobalInfo.CurrentScreening);

                        LDesc.Add(NewDesc);
                        Tmpwell.AddSignatures(LDesc);
                    }



                    ProgressWindow.progressBar.Value = IdxProgress++;
                    ProgressWindow.richTextBoxForComment.AppendText(CurrentPlate.GetShortInfo());
                    ProgressWindow.Refresh();
                }
            }
            #region unary operator
            else
            {
                eUnaryOperationType OperationType = eUnaryOperationType.LOG;
                if (MainWindow.radioButtonSQRT.Checked)
                    OperationType = eUnaryOperationType.SQRT;
                else if (MainWindow.radioButtonLog.Checked)
                    OperationType = eUnaryOperationType.LOG;
                else if (MainWindow.radioButtonABS.Checked)
                    OperationType = eUnaryOperationType.ABS;
                else if (MainWindow.radioButtonEXP.Checked)
                    OperationType = eUnaryOperationType.EXP;

                foreach (cPlate CurrentPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
                {
                    foreach (cWell Tmpwell in CurrentPlate.ListActiveWells)
                    {
                        cListSignature LDesc = new cListSignature();

                        cSignature NewDesc = null;
                        double Val = 0;

                        switch (OperationType)
                        {
                            case eUnaryOperationType.LOG:
                                Val = Math.Log(Tmpwell.GetAverageValue(DescType1));
                                if (double.IsInfinity(Val) || double.IsNaN(Val)) Val = double.Epsilon;
                                break;
                            case eUnaryOperationType.SQRT:
                                Val = Tmpwell.GetAverageValue(DescType1);
                                if (Val < 0) Val = 0;
                                Val = Math.Sqrt(Val);
                                break;
                            case eUnaryOperationType.ABS:
                                Val = Math.Abs(Tmpwell.GetAverageValue(DescType1));
                                break;
                            case eUnaryOperationType.EXP:
                                Val = Math.Exp(Tmpwell.GetAverageValue(DescType1));
                                break;
                            default:
                                break;
                        }


                        if (PostProcessBinaryOp != eBinaryOperationType.UNDEFINED)
                        {
                            double ValuePostProcess = (double)MainWindow.numericUpDownPostProcessValue.Value;

                            switch (PostProcessBinaryOp)
                            {
                                case eBinaryOperationType.ADD:
                                    Val += ValuePostProcess;
                                    break;
                                case eBinaryOperationType.SUBSTRACT:
                                    Val -= ValuePostProcess;
                                    break;
                                case eBinaryOperationType.MULTIPLY:
                                    Val *= ValuePostProcess;
                                    break;
                                case eBinaryOperationType.DIVIDE:
                                    if (ValuePostProcess == 0)
                                        Val = 0;
                                    else
                                        Val /= ValuePostProcess;
                                    break;
                                default:
                                    break;
                            }

                        }


                        NewDesc = new cSignature(Val, NewDescType, cGlobalInfo.CurrentScreening);

                        LDesc.Add(NewDesc);
                        Tmpwell.AddSignatures(LDesc);
                    }

                    ProgressWindow.progressBar.Value = IdxProgress++;
                    ProgressWindow.richTextBoxForComment.AppendText(CurrentPlate.GetShortInfo());
                    ProgressWindow.Refresh();
                }


            }
            #endregion
            ProgressWindow.Close();

            int IdxNull = 0;
            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
            {
                foreach (cWell TmpWell in TmpPlate.ListActiveWells)
                    if (TmpWell.ListSignatures.Count != cGlobalInfo.CurrentScreening.ListDescriptors.Count)
                        IdxNull++;
            }
            if (IdxNull > 0)
                System.Windows.Forms.MessageBox.Show("List signature count is different from list descriptor count", "Critical Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);


            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            //  CompleteScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesAvailable.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();

            StartingUpDateUI();

            this.toolStripcomboBoxPlateList.Items.Clear();

            for (int IdxPlate = 0; IdxPlate < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; IdxPlate++)
            {
                string Name = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(IdxPlate).GetName();
                this.toolStripcomboBoxPlateList.Items.Add(Name);
                PlateListWindow.listBoxPlateNameToProcess.Items.Add(Name);
                PlateListWindow.listBoxAvaliableListPlates.Items.Add(Name);
            }
            cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx = 0;
            cGlobalInfo.CurrentScreening.SetSelectionType(comboBoxClass.SelectedIndex - 1);

            UpdateUIAfterLoading();

            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);

        }

        private void SingleCellOperationsToDescriptorItem(object sender, EventArgs e)
        {

            List<string> ListNamesDesc = new List<string>();
            foreach (var item in cGlobalInfo.CurrentScreening.ListDescriptors)
            {
                if (item.GetDataType() != "Single")
                {
                    ListNamesDesc.Add(item.GetName());
                }
            }

            FormForDescOperations MainWindow = new FormForDescOperations(ListNamesDesc);
            if (MainWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            string NewDescName = MainWindow.textBoxNewDescName.Text;
            cDescriptorType DescType1 = cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorByName(MainWindow.comboBoxDescriptor1.Text);
            cDescriptorType DescType2 = cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorByName(MainWindow.comboBoxDescriptor2.Text);
            if ((DescType1 == null) || (DescType2 == null)) return;

            FormForProgress ProgressWindow = new FormForProgress();
            ProgressWindow.Show();
            int IdxProgress = 1;
            int MaxProgress = cGlobalInfo.CurrentScreening.ListPlatesAvailable.Count;
            ProgressWindow.progressBar.Maximum = MaxProgress;

            cDescriptorType NewDescType = new cDescriptorType(NewDescName, true, 256, true);
            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(NewDescType);


            //HCSAnalyzer.Classes.General_Types.cDBConnection.cSingleCellOperations SCO = new HCSAnalyzer.Classes.General_Types.cDBConnection.cSingleCellOperations()

            cSingleCellOperations SingleCellOperation = new cSingleCellOperations();

            if (MainWindow.tabControlMain.SelectedTab.Name == "tabPageBinary")
            {
                SingleCellOperation.ListDualOperations = new List<eBinaryOperationType>();
                //  eDualOperationType OperationType = eDualOperationType.ADD;
                switch (MainWindow.domainUpDown1.Text)
                {
                    case "*":
                        SingleCellOperation.ListDualOperations.Add(eBinaryOperationType.MULTIPLY);
                        break;
                    case "+":
                        SingleCellOperation.ListDualOperations.Add(eBinaryOperationType.ADD);
                        break;
                    case "/":
                        SingleCellOperation.ListDualOperations.Add(eBinaryOperationType.DIVIDE);
                        break;
                    case "-":
                        SingleCellOperation.ListDualOperations.Add(eBinaryOperationType.SUBSTRACT);
                        break;
                    default:
                        return;
                }


                if (MainWindow.checkBoxActivePostProcess.Checked)
                {
                    SingleCellOperation.PostProcessingOperation = new List<eBinaryOperationType>();

                    switch (MainWindow.domainUpDownPostProcessOperator.Text)
                    {
                        case "*":
                            SingleCellOperation.PostProcessingOperation.Add(eBinaryOperationType.MULTIPLY);
                            break;
                        case "+":
                            SingleCellOperation.PostProcessingOperation.Add(eBinaryOperationType.ADD);
                            break;
                        case "/":
                            SingleCellOperation.PostProcessingOperation.Add(eBinaryOperationType.DIVIDE);
                            break;
                        case "-":
                            SingleCellOperation.PostProcessingOperation.Add(eBinaryOperationType.SUBSTRACT);
                            break;
                        default:
                            return;
                    }

                    SingleCellOperation.PostProcessingValue = (double)MainWindow.numericUpDownPostProcessValue.Value;
                }


                foreach (cPlate CurrentPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
                {
                    CurrentPlate.DBConnection.OpenConnection();
                    CurrentPlate.DBConnection.CreateNewColumn(NewDescType,
                                                                DescType1,
                                                                DescType2,
                                                                SingleCellOperation,
                                                                GlobalInfo, ref CurrentPlate.ListActiveWells);
                    CurrentPlate.DBConnection.CloseConnection();
                    ProgressWindow.progressBar.Value = IdxProgress++;
                    ProgressWindow.richTextBoxForComment.AppendText(CurrentPlate.GetShortInfo());
                    ProgressWindow.Refresh();
                }
            }
            else
            {
                eUnaryOperationType OperationType = eUnaryOperationType.LOG;
                if (MainWindow.radioButtonSQRT.Checked)
                    OperationType = eUnaryOperationType.SQRT;
                else if (MainWindow.radioButtonLog.Checked)
                    OperationType = eUnaryOperationType.LOG;
                else if (MainWindow.radioButtonABS.Checked)
                    OperationType = eUnaryOperationType.ABS;
                else if (MainWindow.radioButtonEXP.Checked)
                    OperationType = eUnaryOperationType.EXP;

                foreach (cPlate CurrentPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
                {
                    CurrentPlate.DBConnection.OpenConnection();
                    CurrentPlate.DBConnection.CreateNewColumn(NewDescType,
                                                                DescType1,
                                                                OperationType,
                                                                GlobalInfo, ref CurrentPlate.ListActiveWells);
                    CurrentPlate.DBConnection.CloseConnection();
                    ProgressWindow.progressBar.Value = IdxProgress++;
                    ProgressWindow.richTextBoxForComment.AppendText(CurrentPlate.GetShortInfo());
                    ProgressWindow.Refresh();
                }

            }

            ProgressWindow.Close();

            int IdxNull = 0;
            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
            {
                foreach (cWell TmpWell in TmpPlate.ListActiveWells)
                    if (TmpWell.ListSignatures.Count != cGlobalInfo.CurrentScreening.ListDescriptors.Count)
                        IdxNull++;
            }
            if (IdxNull > 0)
                System.Windows.Forms.MessageBox.Show("List signature count is different from list descriptor count", "Critical Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);


            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            //  CompleteScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesAvailable.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();

            StartingUpDateUI();

            this.toolStripcomboBoxPlateList.Items.Clear();

            for (int IdxPlate = 0; IdxPlate < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; IdxPlate++)
            {
                string Name = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(IdxPlate).GetName();
                this.toolStripcomboBoxPlateList.Items.Add(Name);
                PlateListWindow.listBoxPlateNameToProcess.Items.Add(Name);
                PlateListWindow.listBoxAvaliableListPlates.Items.Add(Name);
            }
            cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx = 0;
            cGlobalInfo.CurrentScreening.SetSelectionType(comboBoxClass.SelectedIndex - 1);

            UpdateUIAfterLoading();

            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
        }

        #region Single Value Descriptor

        private void ClassToDescriptorItem(object sender, EventArgs e)
        {
            cDescriptorType ClassType = new cDescriptorType("Class", true, 1);
            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(ClassType);

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                {
                    cListSignature LDesc = new cListSignature();

                    cSignature NewDesc = new cSignature(Tmpwell.GetCurrentClassIdx(), ClassType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);

                    Tmpwell.AddSignatures(LDesc);
                }
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }

        private void ClassificationConfidenceToDescriptorItem(object sender, EventArgs e)
        {
            cDescriptorType ClassificationConfidenceType = new cDescriptorType("Classification Confidence", true, 1);
            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(ClassificationConfidenceType);

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                {
                    cListSignature LDesc = new cListSignature();

                    cSignature NewDesc = new cSignature(Tmpwell.GetClassificationConfidence(), ClassificationConfidenceType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);

                    Tmpwell.AddSignatures(LDesc);
                }
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }

        private void ConcentrationToDescriptorItem(object sender, EventArgs e)
        {
            cDescriptorType ConcentrationType = new cDescriptorType("Concentration", true, 1);
            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(ConcentrationType);

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                {
                    cListSignature LDesc = new cListSignature();

                    object ConcentrationValue = Tmpwell.ListProperties.FindValueByName("Concentration");
                    float Value = 0;
                    if (ConcentrationValue == null)
                    {
                        Value = 0;
                        richTextBoxConsole.AppendText(Tmpwell.GetShortInfo() + ": Concentration NULL. Value set to 0\n");
                    }
                    else
                        Value = (float)ConcentrationValue;

                    cSignature NewDesc = new cSignature(Value, ConcentrationType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);

                    Tmpwell.AddSignatures(LDesc);
                }
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }

        private void DuplicateDescItem(object sender, EventArgs e)
        {
            cDescriptorType ColumnType = new cDescriptorType("Duplicate(" + cGlobalInfo.CurrentScreening.ListDescriptors[IntToTransfer].GetName() + ")", true, 1);

            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(ColumnType);

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                {
                    cListSignature LDesc = new cListSignature();

                    cSignature NewDesc = new cSignature(Tmpwell.ListSignatures[IntToTransfer].GetValue(), ColumnType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);

                    Tmpwell.AddSignatures(LDesc);
                }
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }

        private void AddCorrelatedCosineDescItem(object sender, EventArgs e)
        {
            cDescriptorType ColumnType = new cDescriptorType("Cosine(" + cGlobalInfo.CurrentScreening.ListDescriptors[IntToTransfer].GetName() + ")", true, 1);

            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(ColumnType);

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                {
                    cListSignature LDesc = new cListSignature();

                    cSignature NewDesc = new cSignature(Math.Cos(Tmpwell.ListSignatures[IntToTransfer].GetValue()), ColumnType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);

                    Tmpwell.AddSignatures(LDesc);
                }
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }

        private void AddCorrelatedExpDescItem(object sender, EventArgs e)
        {
            cDescriptorType ColumnType = new cDescriptorType("Exp(" + cGlobalInfo.CurrentScreening.ListDescriptors[IntToTransfer].GetName() + ")", true, 1);

            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(ColumnType);

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                {
                    cListSignature LDesc = new cListSignature();

                    cSignature NewDesc = new cSignature(Math.Exp(Tmpwell.ListSignatures[IntToTransfer].GetValue()), ColumnType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);

                    Tmpwell.AddSignatures(LDesc);
                }
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }

        private void AddCorrelatedSineDescItem(object sender, EventArgs e)
        {
            cDescriptorType ColumnType = new cDescriptorType("Sine(" + cGlobalInfo.CurrentScreening.ListDescriptors[IntToTransfer].GetName() + ")", true, 1);

            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(ColumnType);

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                {
                    cListSignature LDesc = new cListSignature();

                    cSignature NewDesc = new cSignature(Math.Sin(Tmpwell.ListSignatures[IntToTransfer].GetValue()), ColumnType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);

                    Tmpwell.AddSignatures(LDesc);
                }
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }

        private void AddCorrelatedSquareDescItem(object sender, EventArgs e)
        {
            cDescriptorType ColumnType = new cDescriptorType("Square(" + cGlobalInfo.CurrentScreening.ListDescriptors[IntToTransfer].GetName() + ")", true, 1);

            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(ColumnType);

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                {
                    cListSignature LDesc = new cListSignature();

                    cSignature NewDesc = new cSignature(Math.Pow(Tmpwell.ListSignatures[IntToTransfer].GetValue(), 2), ColumnType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);

                    Tmpwell.AddSignatures(LDesc);
                }
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }

        private void SumCheckedDescToDescriptorItem(object sender, EventArgs e)
        {
            string NewName = "";
            string Description = "Sum of:\n";
            for (int Idx = 0; Idx < cGlobalInfo.WindowHCSAnalyzer.checkedListBoxActiveDescriptors.Items.Count; Idx++)
            {

                if (cGlobalInfo.CurrentScreening.ListDescriptors[Idx].IsActive())
                    if (cGlobalInfo.CurrentScreening.ListDescriptors[Idx].GetBinNumber() == 1)
                    {
                        NewName += cGlobalInfo.CurrentScreening.ListDescriptors[Idx].GetName() + "+";
                        Description += cGlobalInfo.CurrentScreening.ListDescriptors[Idx].GetName() + "+\n";
                    }
                    else
                    {
                        MessageBox.Show("Descriptor length not consistent (" + cGlobalInfo.CurrentScreening.ListDescriptors[Idx].GetName() + " : " + cGlobalInfo.CurrentScreening.ListDescriptors[Idx].GetBinNumber() + " bins", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
            }
            cDescriptorType ColumnType = new cDescriptorType(NewName.Remove(NewName.Length - 1), true, 1);
            ColumnType.description = Description;

            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(ColumnType);

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                {
                    cListSignature LDesc = new cListSignature();

                    double NewValue = 0;

                    for (int IdxActiveDesc = 0; IdxActiveDesc < cGlobalInfo.CurrentScreening.ListDescriptors.Count - 1; IdxActiveDesc++)
                    {
                        if (cGlobalInfo.CurrentScreening.ListDescriptors[IdxActiveDesc].IsActive())
                            NewValue += Tmpwell.ListSignatures[IdxActiveDesc].GetValue();
                    }
                    cSignature NewDesc = new cSignature(NewValue, ColumnType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);

                    Tmpwell.AddSignatures(LDesc);

                }
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }

        private void GenerateLADDescriptorItem(object sender, EventArgs e)
        {
            FormForProjections WindowClassification = new FormForProjections();
            //WindowClassification.buttonClassification.Text = "Process";
            WindowClassification.label1.Text = "Neutral Class";
            WindowClassification.Text = "LDA";
            WindowClassification.IsPCA = false;
            WindowClassification.numericUpDownNumberOfAxis.Visible = false;
            WindowClassification.labelAxeNumber.Visible = false;

            PanelForClassSelection ClassSelectionPanel = new PanelForClassSelection(true, eClassType.WELL);
            ClassSelectionPanel.Height = WindowClassification.panelForClasses.Height;
            ClassSelectionPanel.UnSelectAll();
            ClassSelectionPanel.Select(0);
            ClassSelectionPanel.Select(1);
            WindowClassification.panelForClasses.Controls.Add(ClassSelectionPanel);

            cListPlates PlatesToProcess = new cListPlates(null);
            if (WindowClassification.radioButtonFromCurrentPlate.Checked)
                PlatesToProcess.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());
            else
                PlatesToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive;
            WindowClassification.PlatesToProcess = PlatesToProcess;

            if (WindowClassification.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

        }

        private void GenerateManualLinearDescriptorItem(object sender, EventArgs e)
        {
            cExtendedTable NewBasis = cGlobalInfo.CurrentScreening.ListDescriptors.GetListWeights();
            cViewerTable VHM = new cViewerTable();

            cDescriptorsLinearCombination DLC = new cDescriptorsLinearCombination(VHM);
            foreach (cDescriptorType Desc in NewBasis[0].ListTags)
                DLC.Add(Desc);
            NewBasis[0].Tag = DLC;

            VHM.SetInputData(NewBasis);
            VHM.Run();

            cDisplayToWindow vD = new cDisplayToWindow();
            vD.SetInputData(VHM.GetOutPut());
            vD.Title = "Manual Linear Projection";
            vD.Run();
            vD.Display();
        }

        private void GeneratePCADescriptorItem(object sender, EventArgs e)
        {
            FormForProjections WindowClassification = new FormForProjections();
            //WindowClassification.buttonClassification.Text = "Process";
            WindowClassification.label1.Text = "Class of Interest";
            WindowClassification.Text = "PCA";
            WindowClassification.IsPCA = true;
            WindowClassification.numericUpDownNumberOfAxis.Maximum = cGlobalInfo.CurrentScreening.GetNumberOfActiveDescriptor();

            PanelForClassSelection ClassSelectionPanel = new PanelForClassSelection(true, eClassType.WELL);
            ClassSelectionPanel.Height = WindowClassification.panelForClasses.Height;
            ClassSelectionPanel.UnSelectAll();
            ClassSelectionPanel.Select(2);
            WindowClassification.panelForClasses.Controls.Add(ClassSelectionPanel);



            cListPlates PlatesToProcess = new cListPlates(null);
            if (WindowClassification.radioButtonFromCurrentPlate.Checked)
                PlatesToProcess.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());
            else
                PlatesToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive;

            WindowClassification.PlatesToProcess = PlatesToProcess;
            if (WindowClassification.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
        }

        private void GenerateRandomProjectionDescriptorItem(object sender, EventArgs e)
        {
            FormForProjections WindowClassification = new FormForProjections();
            //WindowClassification.buttonClassification.Text = "Process";
            WindowClassification.label1.Text = "Class of Interest";
            WindowClassification.Text = "Random Projection";
            WindowClassification.IsPCA = true;
            WindowClassification.numericUpDownNumberOfAxis.Maximum = cGlobalInfo.CurrentScreening.GetNumberOfActiveDescriptor();

            cListPlates PlatesToProcess = new cListPlates();
            if (WindowClassification.radioButtonFromCurrentPlate.Checked)
                PlatesToProcess.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());
            else
                PlatesToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive;

            WindowClassification.PlatesToProcess = PlatesToProcess;
            if (WindowClassification.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
        }

        private void NormalizeCheckedDescToDescriptorItem(object sender, EventArgs e)
        {
            FormForDescriptorNormalization WindowForDescNorm = new FormForDescriptorNormalization();
            WindowForDescNorm.textBoxForDescName.Text = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor().GetName();
            if (WindowForDescNorm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            // first: identify the reference desc.
            cDescriptorType SelectedDesc = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor();

            eBinaryOperationType OT = eBinaryOperationType.DIVIDE;
            string OperationString = " / ";
            if (WindowForDescNorm.radioButtonOperationMultiply.Checked)
            {
                OperationString = " * ";
                OT = eBinaryOperationType.MULTIPLY;
            }
            if (WindowForDescNorm.radioButtonOperationAdd.Checked)
            {
                OperationString = " + ";
                OT = eBinaryOperationType.ADD;
            }
            if (WindowForDescNorm.radioButtonOperationSubstract.Checked)
            {
                OperationString = " - ";
                OT = eBinaryOperationType.SUBSTRACT;
            }

            List<cDescriptorType> ListNewDesc = new List<cDescriptorType>();
            int CurrentNumberOfDesc = cGlobalInfo.WindowHCSAnalyzer.checkedListBoxActiveDescriptors.Items.Count;
            for (int Idx = 0; Idx < CurrentNumberOfDesc; Idx++)
            {

                if (cGlobalInfo.CurrentScreening.ListDescriptors[Idx].IsActive())
                {
                    if (cGlobalInfo.CurrentScreening.ListDescriptors[Idx].GetBinNumber() == 1)
                    {
                        string NewName = cGlobalInfo.CurrentScreening.ListDescriptors[Idx].GetName() + OperationString;
                        if (WindowForDescNorm.radioButtonValueCurrentActiveValue.Checked)
                            NewName += SelectedDesc.GetName();
                        else
                            NewName += WindowForDescNorm.numericUpDownValueConst.Value.ToString();

                        ListNewDesc.Add(new cDescriptorType(NewName, true, 1));
                        cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(ListNewDesc[ListNewDesc.Count - 1]);
                    }
                    else
                    {
                        MessageBox.Show("Descriptor length not consistent (" + cGlobalInfo.CurrentScreening.ListDescriptors[Idx].GetName() + " : " + cGlobalInfo.CurrentScreening.ListDescriptors[Idx].GetBinNumber() + " bins", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            double NewValue = 1;
            double ValueForNormalization;

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                {

                    if (WindowForDescNorm.radioButtonValueConstant.Checked) ValueForNormalization = (double)WindowForDescNorm.numericUpDownValueConst.Value;
                    else
                        ValueForNormalization = Tmpwell.ListSignatures.GetSignature(SelectedDesc).GetValue();

                    int RealIDx = 0;
                    for (int IdxActiveDesc = 0; IdxActiveDesc < CurrentNumberOfDesc; IdxActiveDesc++)
                    {
                        if (cGlobalInfo.CurrentScreening.ListDescriptors[IdxActiveDesc].IsActive())
                        {
                            cListSignature LDesc = new cListSignature();

                            switch (OT)
                            {
                                case eBinaryOperationType.DIVIDE:
                                    NewValue = Tmpwell.ListSignatures[IdxActiveDesc].GetValue() / ValueForNormalization;
                                    break;
                                case eBinaryOperationType.MULTIPLY:
                                    NewValue = Tmpwell.ListSignatures[IdxActiveDesc].GetValue() * ValueForNormalization;
                                    break;
                                case eBinaryOperationType.ADD:
                                    NewValue = Tmpwell.ListSignatures[IdxActiveDesc].GetValue() + ValueForNormalization;
                                    break;
                                case eBinaryOperationType.SUBSTRACT:
                                    NewValue = Tmpwell.ListSignatures[IdxActiveDesc].GetValue() - ValueForNormalization;
                                    break;
                                default:
                                    break;
                            }


                            cSignature NewDesc = new cSignature(NewValue, ListNewDesc[RealIDx++], cGlobalInfo.CurrentScreening);
                            LDesc.Add(NewDesc);
                            Tmpwell.AddSignatures(LDesc);
                        }
                    }
                }
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }

        private void MultiplyCheckedDescToDescriptorItem(object sender, EventArgs e)
        {
            string NewName = "";

            for (int Idx = 0; Idx < cGlobalInfo.WindowHCSAnalyzer.checkedListBoxActiveDescriptors.Items.Count; Idx++)
            {

                if (cGlobalInfo.CurrentScreening.ListDescriptors[Idx].IsActive())
                    if (cGlobalInfo.CurrentScreening.ListDescriptors[Idx].GetBinNumber() == 1)
                    {
                        NewName += cGlobalInfo.CurrentScreening.ListDescriptors[Idx].GetName() + "*";
                    }
                    else
                    {
                        MessageBox.Show("Descriptor length not consistent (" + cGlobalInfo.CurrentScreening.ListDescriptors[Idx].GetName() + " : " + cGlobalInfo.CurrentScreening.ListDescriptors[Idx].GetBinNumber() + " bins", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
            }
            cDescriptorType ColumnType = new cDescriptorType(NewName.Remove(NewName.Length - 1), true, 1);

            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(ColumnType);

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                {
                    cListSignature LDesc = new cListSignature();

                    double NewValue = 1;

                    for (int IdxActiveDesc = 0; IdxActiveDesc < cGlobalInfo.CurrentScreening.ListDescriptors.Count - 1; IdxActiveDesc++)
                    {
                        if (cGlobalInfo.CurrentScreening.ListDescriptors[IdxActiveDesc].IsActive())
                            NewValue *= Tmpwell.ListSignatures[IdxActiveDesc].GetValue();
                    }
                    cSignature NewDesc = new cSignature(NewValue, ColumnType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);

                    Tmpwell.AddSignatures(LDesc);

                }
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }

        private void ColumnToDescriptorItem(object sender, EventArgs e)
        {
            cDescriptorType ColumnType = new cDescriptorType("Column", true, 1);

            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(ColumnType);

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                {
                    cListSignature LDesc = new cListSignature();

                    cSignature NewDesc = new cSignature(Tmpwell.GetPosX(), ColumnType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);

                    Tmpwell.AddSignatures(LDesc);
                }
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }

        private void RowToDescriptorItem(object sender, EventArgs e)
        {
            cDescriptorType RowType = new cDescriptorType("Row", true, 1);

            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(RowType);

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                {
                    cListSignature LDesc = new cListSignature();

                    cSignature NewDesc = new cSignature(Tmpwell.GetPosY(), RowType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);

                    Tmpwell.AddSignatures(LDesc);
                }
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }
        #endregion


        private void SplitDescItem(object sender, EventArgs e)
        {
            int NumBin = cGlobalInfo.CurrentScreening.ListDescriptors[IntToTransfer].GetBinNumber();

            // first we update the descriptor
            for (int i = 0; i < NumBin; i++)
                cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(new cDescriptorType(cGlobalInfo.CurrentScreening.ListDescriptors[IntToTransfer].GetName() + "_" + i, true, 1));

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                {
                    cListSignature LDesc = new cListSignature();
                    for (int i = 0; i < NumBin; i++)
                    {
                        cSignature NewDesc = new cSignature(Tmpwell.ListSignatures[IntToTransfer].GetHistovalue(i), cGlobalInfo.CurrentScreening.ListDescriptors[i + IntToTransfer + 1], cGlobalInfo.CurrentScreening);
                        LDesc.Add(NewDesc);
                    }
                    Tmpwell.AddSignatures(LDesc);
                }
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }

        private void SingleCellOperationPhenotypeConfidenceToDesc(object sender, EventArgs e)
        {
            //string description = "This descriptor has been generated by converting single cell phenotypic class into feature\n";
            FormForNameRequest FFNR = new FormForNameRequest();
            FFNR.Text = "Descriptor Name";
            FFNR.textBoxForName.Text = "Classification Confidence";
            if (FFNR.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            if (FFNR.textBoxForName.Text == "") return;

            cDescriptorType NewDescType = new cDescriptorType(FFNR.textBoxForName.Text, true, cGlobalInfo.ListCellularPhenotypes.Count, true);
            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(NewDescType);

            FormForProgress ProgressWindow = new FormForProgress();
            ProgressWindow.Show();

            int IdxProgress = 0;
            int MaxProgress = 0;

            foreach (cPlate CurrentPlateToProcess in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
                MaxProgress += (int)CurrentPlateToProcess.ListActiveWells.Count;

            ProgressWindow.progressBar.Maximum = MaxProgress;

            foreach (cPlate CurrentPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                CurrentPlate.DBConnection.OpenConnection();
                CurrentPlate.DBConnection.CreateNewColumnFromExisting(NewDescType, "Phenotype_Confidence", GlobalInfo, ref CurrentPlate.ListActiveWells);
                CurrentPlate.DBConnection.CloseConnection();
                ProgressWindow.progressBar.Value = IdxProgress++;
                ProgressWindow.richTextBoxForComment.AppendText(CurrentPlate.GetShortInfo());
                ProgressWindow.Refresh();
            }

            ProgressWindow.Close();
            int IdxNull = 0;
            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
            {
                foreach (cWell TmpWell in TmpPlate.ListActiveWells)
                    if (TmpWell.ListSignatures.Count != cGlobalInfo.CurrentScreening.ListDescriptors.Count)
                        IdxNull++;
            }
            if (IdxNull > 0)
                System.Windows.Forms.MessageBox.Show("List signature count is different from list descriptor count", "Critical Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);


            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            //  CompleteScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesAvailable.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();

            StartingUpDateUI();

            this.toolStripcomboBoxPlateList.Items.Clear();

            for (int IdxPlate = 0; IdxPlate < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; IdxPlate++)
            {
                string Name = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(IdxPlate).GetName();
                this.toolStripcomboBoxPlateList.Items.Add(Name);
                PlateListWindow.listBoxPlateNameToProcess.Items.Add(Name);
                PlateListWindow.listBoxAvaliableListPlates.Items.Add(Name);
            }
            cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx = 0;
            cGlobalInfo.CurrentScreening.SetSelectionType(comboBoxClass.SelectedIndex - 1);

            UpdateUIAfterLoading();

            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);

        }


        private void SingleCellOperationPhenotypToDesc(object sender, EventArgs e)
        {
            string description = "This descriptor has been generated by converting single cell phenotypic class into feature\n";

            FormForNameRequest FFNR = new FormForNameRequest();
            FFNR.Text = "Descriptor Name";
            FFNR.textBoxForName.Text = "Phenotype Class";
            if (FFNR.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            if (FFNR.textBoxForName.Text == "") return;

            cDescriptorType NewDescType = new cDescriptorType(FFNR.textBoxForName.Text, true, cGlobalInfo.ListCellularPhenotypes.Count, true);
            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(NewDescType);

            FormForProgress ProgressWindow = new FormForProgress();
            ProgressWindow.Show();

            int IdxProgress = 0;
            int MaxProgress = 0;

            foreach (cPlate CurrentPlateToProcess in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
                MaxProgress += (int)CurrentPlateToProcess.ListActiveWells.Count;

            ProgressWindow.progressBar.Maximum = MaxProgress;

            foreach (cPlate CurrentPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                CurrentPlate.DBConnection.OpenConnection();

                CurrentPlate.DBConnection.CreateNewColumnFromExisting(NewDescType, "Phenotype_Class", GlobalInfo, ref CurrentPlate.ListActiveWells);

                CurrentPlate.DBConnection.CloseConnection();
                ProgressWindow.progressBar.Value = IdxProgress++;
                ProgressWindow.progressBar.Refresh();
            }

            ProgressWindow.Close();
            int IdxNull = 0;
            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
            {
                foreach (cWell TmpWell in TmpPlate.ListActiveWells)
                    if (TmpWell.ListSignatures.Count != cGlobalInfo.CurrentScreening.ListDescriptors.Count)
                        IdxNull++;
            }
            if (IdxNull > 0)
                System.Windows.Forms.MessageBox.Show("List signature count is different from list descriptor count", "Critical Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);


            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            //  CompleteScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesAvailable.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();

            StartingUpDateUI();

            this.toolStripcomboBoxPlateList.Items.Clear();

            for (int IdxPlate = 0; IdxPlate < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; IdxPlate++)
            {
                string Name = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(IdxPlate).GetName();
                this.toolStripcomboBoxPlateList.Items.Add(Name);
                PlateListWindow.listBoxPlateNameToProcess.Items.Add(Name);
                PlateListWindow.listBoxAvaliableListPlates.Items.Add(Name);
            }
            cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx = 0;
            cGlobalInfo.CurrentScreening.SetSelectionType(comboBoxClass.SelectedIndex - 1);

            UpdateUIAfterLoading();

            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
        }

        private void SubPopulationStatDescItem(object sender, EventArgs e)
        {
            PanelForClassSelection ClassSelectionPanel = new PanelForClassSelection(true, eClassType.PHENOTYPE);
            //ClassSelectionPanel.Height = WindowToDisplay.Height;
            ClassSelectionPanel.UnSelectAll();
            ClassSelectionPanel.Select(0);
            ClassSelectionPanel.Select(1);
            ClassSelectionPanel.Location = new System.Drawing.Point(10, 10);
            ClassSelectionPanel.Width = 150;
            ClassSelectionPanel.Height = ClassSelectionPanel.ListCheckBoxes.Count * 25;
            ClassSelectionPanel.BorderStyle = BorderStyle.Fixed3D;


            FormForStatDesc NewWindow = new FormForStatDesc(this.GlobalInfo);
            NewWindow.panelForSubPopulation.Controls.Add(ClassSelectionPanel);
            NewWindow.textBoxDescName.Text = cGlobalInfo.CurrentScreening.ListDescriptors[IntToTransfer].GetName() + "_Average";
            if (NewWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            if ((NewWindow.comboBoxStatistics.Text != "Average") && (NewWindow.comboBoxStatistics.Text != "Stdev") &&
                (NewWindow.comboBoxStatistics.Text != "Sum") && (NewWindow.comboBoxStatistics.Text != "Median") && (NewWindow.comboBoxStatistics.Text != "MAD") && (NewWindow.comboBoxStatistics.Text != "CV%"))
                return;


            List<cCellularPhenotype> LCP = new List<cCellularPhenotype>();
            for (int IdxPheno = 0; IdxPheno < ClassSelectionPanel.GetListSelectedClass().Count; IdxPheno++)
            {
                if (ClassSelectionPanel.GetListSelectedClass()[IdxPheno])
                    LCP.Add(cGlobalInfo.ListCellularPhenotypes[IdxPheno]);
            }

            if (LCP.Count == 0) return;

            double RatioPopulation = 1;

            string description = "This descriptor has been generated by computing the " + NewWindow.textBoxDescName.Text + " of " + RatioPopulation * 100 + "% the following phenotypic sub-populations:\n";
            foreach (var item in LCP)
            {
                description += item.Name + "\n";
            }

            cDescriptorType NewAverageType = new cDescriptorType(NewWindow.textBoxDescName.Text, true, 1, description);
            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(NewAverageType);

            FormForProgress ProgressWindow = new FormForProgress();
            ProgressWindow.Show();

            int IdxProgress = 0;
            int MaxProgress = 0;

            foreach (cPlate CurrentPlateToProcess in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
                MaxProgress += (int)CurrentPlateToProcess.ListActiveWells.Count;

            ProgressWindow.progressBar.Maximum = MaxProgress;

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                {
                    TmpPlate.DBConnection = new cDBConnection(TmpPlate, Tmpwell.SQLTableName);
                    List<cDescriptorType> LDT = new List<cDescriptorType>();
                    LDT.Add(cGlobalInfo.CurrentScreening.ListDescriptors[IntToTransfer]);

                    cExtendedTable CT = TmpPlate.DBConnection.GetWellValues(Tmpwell, LDT, LCP, RatioPopulation);
                    cListSignature LDesc = new cListSignature();

                    double Value = 0;
                    if (CT.Count == 1)
                    {

                        if (CT[0].Count == 0)
                        {
                            Value = 0;
                            richTextBoxConsole.AppendText(TmpPlate.GetName() + " : " + Tmpwell.GetShortInfo().Remove(Tmpwell.GetShortInfo().Length - 2) + ": no objects fullfill your request. Statistics set to null.\n");
                        }
                        else
                        {
                            if (NewWindow.comboBoxStatistics.Text == "Average")
                                Value = CT[0].Average();
                            else if (NewWindow.comboBoxStatistics.Text == "Stdev")
                                Value = CT[0].Std();
                            else if (NewWindow.comboBoxStatistics.Text == "Sum")
                                Value = CT[0].Sum();
                            else if (NewWindow.comboBoxStatistics.Text == "Median")
                                Value = CT[0].Median();
                            else if (NewWindow.comboBoxStatistics.Text == "MAD")
                                Value = CT[0].MAD(true);
                            else if (NewWindow.comboBoxStatistics.Text == "CV%")
                                Value = CT[0].CV();
                        }
                    }

                    ProgressWindow.richTextBoxForComment.AppendText(Tmpwell.GetShortInfo() + ": " + CT[0].Count.ToString() + " / " + Tmpwell.GetNumBiologicalObjects() + "\n");

                    cSignature NewDesc = new cSignature(Value, NewAverageType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);
                    Tmpwell.AddSignatures(LDesc);
                    TmpPlate.DBConnection.CloseConnection();

                    ProgressWindow.progressBar.Value = IdxProgress++;

                    ProgressWindow.richTextBoxForComment.AppendText(TmpPlate.GetName() + " : " + Tmpwell.GetShortInfo().Remove(Tmpwell.GetShortInfo().Length - 2) + "\n");
                    ProgressWindow.Refresh();
                }
            }
            ProgressWindow.Close();

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }

        private void PopValueDescItem(object sender, EventArgs e)
        {
            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            GUI_ListClasses.ClassType = eClassType.PHENOTYPE;
            GUI_ListClasses.IsCheckBoxes = true;

            if (GUI_ListClasses.Run().IsSucceed == false) return;


            List<cCellularPhenotype> LCP0 = new List<cCellularPhenotype>();

            for (int IdxPheno = 0; IdxPheno < GUI_ListClasses.GetOutPut()[0].Count; IdxPheno++)
            {
                if (GUI_ListClasses.GetOutPut()[0][IdxPheno] == 1)
                    LCP0.Add(cGlobalInfo.ListCellularPhenotypes[IdxPheno]);
            }

            //if (LCP.Count == 0) return;

            string description = "Number of elements of:\n";

            foreach (var item in LCP0)
                description += item.Name + "\n";

            cDescriptorType NewAverageType = new cDescriptorType("Object_count", true, 1, description);
            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(NewAverageType);

            FormForProgress ProgressWindow = new FormForProgress();
            ProgressWindow.Show();

            int IdxProgress = 0;
            int MaxProgress = 0;

            foreach (cPlate CurrentPlateToProcess in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
                MaxProgress += (int)CurrentPlateToProcess.ListWells.Count;

            ProgressWindow.progressBar.Maximum = MaxProgress;

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListWells)
                {
                    TmpPlate.DBConnection = new cDBConnection(TmpPlate, Tmpwell.SQLTableName);
                    List<cDescriptorType> LDT = new List<cDescriptorType>();
                    LDT.Add(cGlobalInfo.CurrentScreening.ListDescriptors[0]);

                    cExtendedTable CT = TmpPlate.DBConnection.GetWellValues(Tmpwell, LDT, LCP0, 1);
                    double NumObject0 = CT[0].Count;

                    //cExtendedTable CT1 = TmpPlate.DBConnection.GetWellValues(Tmpwell, LDT, LCP1, 1);
                    //double NumObject1 = CT1[0].Count;

                    cListSignature LDesc = new cListSignature();

                    cSignature NewDesc = new cSignature((double)NumObject0, NewAverageType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);
                    Tmpwell.AddSignatures(LDesc);
                    TmpPlate.DBConnection.CloseConnection();

                    ProgressWindow.richTextBoxForComment.AppendText(TmpPlate.GetName() + " : " + Tmpwell.GetShortInfo().Remove(Tmpwell.GetShortInfo().Length - 2) + "\n");
                    ProgressWindow.progressBar.Value = IdxProgress++;
                    ProgressWindow.Refresh();
                }
            }
            ProgressWindow.Close();


            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }

        private void RatioPopDescItem(object sender, EventArgs e)
        {
            cGUI_2ClassesSelection GUI_ListClasses = new cGUI_2ClassesSelection();
            GUI_ListClasses.ClassType = eClassType.PHENOTYPE;
            GUI_ListClasses.PanelRight_IsCheckBoxes = true;
            GUI_ListClasses.PanelLeft_IsCheckBoxes = true;

            if (GUI_ListClasses.Run(this.GlobalInfo).IsSucceed == false) return;


            List<cCellularPhenotype> LCP0 = new List<cCellularPhenotype>();
            List<cCellularPhenotype> LCP1 = new List<cCellularPhenotype>();

            for (int IdxPheno = 0; IdxPheno < GUI_ListClasses.GetOutPut()[0].Count; IdxPheno++)
            {
                if (GUI_ListClasses.GetOutPut()[0][IdxPheno] == 1)
                    LCP0.Add(cGlobalInfo.ListCellularPhenotypes[IdxPheno]);

                if (GUI_ListClasses.GetOutPut()[1][IdxPheno] == 1)
                    LCP1.Add(cGlobalInfo.ListCellularPhenotypes[IdxPheno]);
            }

            //if (LCP.Count == 0) return;

            string description = "Ratio of:\n";

            foreach (var item in LCP0)
                description += item.Name + "\n";

            description += "over\n";

            foreach (var item in LCP1)
                description += item.Name + "\n";


            cDescriptorType NewAverageType = new cDescriptorType("Ratio_Pop", true, 1, description);
            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(NewAverageType);

            FormForProgress ProgressWindow = new FormForProgress();
            ProgressWindow.Show();

            int IdxProgress = 0;
            int MaxProgress = 0;

            foreach (cPlate CurrentPlateToProcess in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
                MaxProgress += (int)CurrentPlateToProcess.ListWells.Count;

            ProgressWindow.progressBar.Maximum = MaxProgress;

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListWells)
                {
                    TmpPlate.DBConnection = new cDBConnection(TmpPlate, Tmpwell.SQLTableName);
                    List<cDescriptorType> LDT = new List<cDescriptorType>();
                    LDT.Add(cGlobalInfo.CurrentScreening.ListDescriptors[0]);

                    cExtendedTable CT = TmpPlate.DBConnection.GetWellValues(Tmpwell, LDT, LCP0, 1);
                    double NumObject0 = CT[0].Count;

                    cExtendedTable CT1 = TmpPlate.DBConnection.GetWellValues(Tmpwell, LDT, LCP1, 1);
                    double NumObject1 = CT1[0].Count;

                    cListSignature LDesc = new cListSignature();

                    double Value = 0;
                    if (NumObject1 == 0)
                        Value = 0;
                    else
                        Value = NumObject0 / NumObject1;

                    cSignature NewDesc = new cSignature(Value, NewAverageType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);
                    Tmpwell.AddSignatures(LDesc);
                    TmpPlate.DBConnection.CloseConnection();

                    ProgressWindow.richTextBoxForComment.AppendText(TmpPlate.GetName() + " : " + Tmpwell.GetShortInfo().Remove(Tmpwell.GetShortInfo().Length - 2) + "\n");
                    ProgressWindow.progressBar.Value = IdxProgress++;
                    ProgressWindow.Refresh();
                }
            }
            ProgressWindow.Close();


            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }

        #endregion

        private void clearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cGlobalInfo.CurrentRichTextBox.Clear();
        }

        private void buttonClearConsole_Click(object sender, EventArgs e)
        {
            cGlobalInfo.CurrentRichTextBox.Clear();
        }


        //private void distributionToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    DisplayHistogram(false);
        //}



        /// <summary>
        /// Change the slection mode (global or local)
        /// </summary>
        /// <param name="OnlyOnSelected">True: single plate selection; False: entiere screen selection</param>
        private void GlobalSelection(bool OnlyOnSelected)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            if (cGlobalInfo.CurrentScreening.GetSelectionType() <= -2) return;

            for (int col = 0; col < cGlobalInfo.CurrentScreening.Columns; col++)
                for (int row = 0; row < cGlobalInfo.CurrentScreening.Rows; row++)
                {

                    if (cGlobalInfo.CurrentScreening.IsSelectionApplyToAllPlates)
                    {
                        int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;

                        for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
                        {
                            cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(PlateIdx);
                            cWell TmpWell = CurrentPlateToProcess.GetWell(col, row, OnlyOnSelected);
                            if (TmpWell == null) continue;

                            if (cGlobalInfo.CurrentScreening.GetSelectionType() == -1)
                                TmpWell.SetAsNoneSelected();
                            else
                                TmpWell.SetClass(cGlobalInfo.CurrentScreening.GetSelectionType());
                        }
                    }
                    else
                    {
                        cWell TmpWell = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetWell(col, row, OnlyOnSelected);
                        if (TmpWell != null)
                        {
                            if (cGlobalInfo.CurrentScreening.GetSelectionType() == -1) TmpWell.SetAsNoneSelected();
                            else
                                TmpWell.SetClass(cGlobalInfo.CurrentScreening.GetSelectionType());


                        }
                    }
                }

            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().UpdateNumberOfClass();
        }


        /// <summary>
        /// Manage the event related to the active plate selection combo list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripcomboBoxPlateList_SelectedIndexChanged(object sender, EventArgs e)
        {
            cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx = this.toolStripcomboBoxPlateList.SelectedIndex;

            if (cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx == -1) return;
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false, toolStripcomboBoxPlateList);

            cGlobalInfo.WindowHCSAnalyzer.UpdateQCDisplay();

        }

        /// <summary>
        /// Manage the event related to Class selection control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            cGlobalInfo.CurrentScreening.SetSelectionType(comboBoxClass.SelectedIndex - 1);

            if (comboBoxClass.SelectedIndex >= 1)
                comboBoxClass.BackColor = cGlobalInfo.ListWellClasses[comboBoxClass.SelectedIndex - 1].ColourForDisplay;
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().UpdateNumberOfClass();
        }

        /// <summary>
        /// set all the well of the current plate to "unselected" mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int col = 0; col < cGlobalInfo.CurrentScreening.Columns; col++)
                for (int row = 0; row < cGlobalInfo.CurrentScreening.Rows; row++)
                {
                    cWell TmpWell = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx).GetWell(col, row, false);
                    if (TmpWell != null) TmpWell.SetAsNoneSelected();
                }
        }

        private void checkedListBoxDescriptorActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx == -1) return;


            for (int idxDesc = 0; idxDesc < cGlobalInfo.CurrentScreening.ListDescriptors.Count; idxDesc++)
                cGlobalInfo.CurrentScreening.ListDescriptors[idxDesc].SetActiveState(false);

            for (int IdxDesc = 0; IdxDesc < checkedListBoxActiveDescriptors.CheckedItems.Count; IdxDesc++)
                cGlobalInfo.CurrentScreening.ListDescriptors[checkedListBoxActiveDescriptors.CheckedIndices[IdxDesc]].SetActiveState(true);

            RefreshInfoScreeningRichBox();


            if (cGlobalInfo.ViewMode == eViewMode.PIE)
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors[0], false);

            RefreshViews(checkedListBoxActiveDescriptors);

            return;
        }

        private void comboBoxDescriptorToDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx = (int)comboBoxDescriptorToDisplay.SelectedIndex;

            if ((!checkBoxDisplayClasses.Checked) && (cGlobalInfo.ViewMode != eViewMode.PIE))
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);


            cGlobalInfo.WindowHCSAnalyzer.RefreshViews(comboBoxDescriptorToDisplay);

            cGlobalInfo.WindowHCSAnalyzer.UpdateQCDisplay();

            //ToolTip ToolTipFor1 = new ToolTip();
            //ToolTipFor1.AutoPopDelay = 5000;
            //ToolTipFor1.InitialDelay = 500;
            //ToolTipFor1.ReshowDelay = 500;
            //ToolTipFor1.ShowAlways = true;
            //ToolTipFor1.SetToolTip(comboBoxDescriptorToDisplay, comboBoxDescriptorToDisplay.Text);

        }

        public void StartingUpDateUI()
        {
            cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx = 0;
            cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx = 0;
            cGlobalInfo.LabelForClass = this.labelNumClasses;
            //CompleteScreening.LabelForMin = this.labelMin;
            // CompleteScreening.LabelForMax = this.labelMax;
            // CompleteScreening.PanelForLUT = this.panelForLUT;
            cGlobalInfo.CurrentScreening.PanelForPlate = this.panelForPlate;

            // CompleteScreening.ListDescriptors = new cListDescriptors(this.checkedListBoxActiveDescriptors, comboBoxDescriptorToDisplay);

            PlateListWindow.listBoxPlateNameToProcess.Items.Clear();
            PlateListWindow.listBoxAvaliableListPlates.Items.Clear();

            // CompleteScreening.ListBoxSelectedPlates = PlateListWindow.listBoxPlateNameToProcess;
            this.toolStripcomboBoxPlateList.Items.Clear();

            cGlobalInfo.CurrentScreening.IsSelectionApplyToAllPlates = checkBoxApplyTo_AllPlates.Checked;
            //cGlobalInfo.CurrentScreening = CurrentScreening;



        }

        private void panelForLUT_Paint(object sender, PaintEventArgs e)
        {
            if ((cGlobalInfo.CurrentScreening == null) || (cGlobalInfo.CurrentScreening.ListPlatesAvailable.Count == 0) || (cGlobalInfo.CurrentScreening.ISLoading))
                return;

            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayLUT(cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
        }

        //private void dataGridViewForQualityControl_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if ((e.ColumnIndex == -1) || (e.RowIndex == -1)) return;
        //    String PlateName = (string)dataGridViewForQualityControl.Rows[e.RowIndex].Cells[0].Value;
        //    String DescName = (string)dataGridViewForQualityControl.Rows[e.RowIndex].Cells[1].Value;
        //    //  tabControlMain.SelectedTab = tabPageDistribution;

        //    int PosPlate = this.toolStripcomboBoxPlateList.FindStringExact(PlateName);
        //    this.toolStripcomboBoxPlateList.SelectedIndex = PosPlate;
        //    cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx = this.toolStripcomboBoxPlateList.SelectedIndex;

        //    int PosDesc = this.comboBoxDescriptorToDisplay.FindStringExact(DescName);
        //    comboBoxDescriptorToDisplay.SelectedIndex = PosDesc;
        //    cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx = (int)comboBoxDescriptorToDisplay.SelectedIndex;

        //    cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
        //}

        private void comboBoxDimReductionNeutralClass_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            SolidBrush BrushForColor = new SolidBrush(cGlobalInfo.ListWellClasses[e.Index].ColourForDisplay);
            e.Graphics.FillRectangle(BrushForColor, e.Bounds.X + 1, e.Bounds.Y + 1, 10, 10);
            if (e.Index == 0)
                e.Graphics.DrawString("Inactive", comboBoxClass.Font,
                                   System.Drawing.Brushes.Black, new RectangleF(e.Bounds.X + 15, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            else
                e.Graphics.DrawString(cGlobalInfo.ListWellClasses[e.Index - 1].Name, comboBoxClass.Font,
                                        System.Drawing.Brushes.Black, new RectangleF(e.Bounds.X + 15, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            e.DrawFocusRectangle();
        }

        private void comboBoxNeutralClassForClassif_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            SolidBrush BrushForColor = new SolidBrush(cGlobalInfo.ListWellClasses[e.Index].ColourForDisplay);
            e.Graphics.FillRectangle(BrushForColor, e.Bounds.X + 1, e.Bounds.Y + 1, 10, 10);
            if (e.Index == 0)
                e.Graphics.DrawString("Inactive", comboBoxClass.Font,
                                   System.Drawing.Brushes.Black, new RectangleF(e.Bounds.X + 15, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            else
                e.Graphics.DrawString(cGlobalInfo.ListWellClasses[e.Index - 1].Name, comboBoxClass.Font,
                                        System.Drawing.Brushes.Black, new RectangleF(e.Bounds.X + 15, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));

            e.DrawFocusRectangle();
        }

        private void panelForPlate_Paint(object sender, PaintEventArgs e)
        {
            if ((cGlobalInfo.CurrentScreening == null) || (cGlobalInfo.CurrentScreening.ListPlatesAvailable.Count == 0) || (cGlobalInfo.CurrentScreening.ISLoading))
                return;

            float SizeFont = cGlobalInfo.SizeHistoHeight / 2;
            int Gutter = (int)cGlobalInfo.OptionsWindow.FFAllOptions.numericUpDownGutter.Value;
            Graphics g = cGlobalInfo.CurrentScreening.PanelForPlate.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(cGlobalInfo.CurrentScreening.PanelForPlate.BackColor);
            int ScrollShiftY = this.panelForPlate.VerticalScroll.Value;
            int ScrollShiftX = this.panelForPlate.HorizontalScroll.Value;

            for (int i = 1; i <= cGlobalInfo.CurrentScreening.Columns; i++)
                g.DrawString(i.ToString(), new Font("Arial", SizeFont), Brushes.White, new PointF((cGlobalInfo.SizeHistoWidth + Gutter) * (i - 1) + (cGlobalInfo.SizeHistoWidth + Gutter) / 4
                    - (i.ToString().Length - 1) * (cGlobalInfo.SizeHistoWidth + Gutter) / 8 + cGlobalInfo.SizeHistoWidth - ScrollShiftX, -ScrollShiftY));

            for (int j = 1; j <= cGlobalInfo.CurrentScreening.Rows; j++)
            {
                if (cGlobalInfo.OptionsWindow.radioButtonWellPosModeDouble.Checked)
                    g.DrawString(j.ToString(), new Font("Arial", SizeFont), Brushes.White,
                        new PointF(-ScrollShiftX, (cGlobalInfo.SizeHistoHeight + Gutter) * (j - 1) + cGlobalInfo.SizeHistoHeight - ScrollShiftY));
                else
                    g.DrawString(cGlobalInfo.ConvertIntPosToStringPos(j), new Font("Arial", SizeFont),
                        Brushes.White, new PointF(-ScrollShiftX, (cGlobalInfo.SizeHistoHeight + Gutter) * (j - 1) + cGlobalInfo.SizeHistoHeight - ScrollShiftY));
            }
        }

        public void UpdateUIAfterLoading()
        {
            if ((comboBoxDescriptorToDisplay.Items.Count >= 1) && (PlateListWindow.listBoxAvaliableListPlates.Items.Count > 0))
            {
                pluginsToolStripMenuItem.Enabled = true;
                exportToolStripMenuItem.Enabled = true;
                exportAsCSVToolStripMenuItem.Enabled = true;
                appendDescriptorsToolStripMenuItem.Enabled = true;
                //linkToolStripMenuItem.Enabled = true;
                toolStripMenuItemLoadProperty.Enabled = true;
                copyAverageValuesToolStripMenuItem1.Enabled = true;
                copyPropertyToolStripMenuItem.Enabled = true;
                swapClassesToolStripMenuItem.Enabled = true;
                applySelectionToScreenToolStripMenuItem.Enabled = true;
                toolStripMenuItemDescManagement.Enabled = true;
                visualizationToolStripMenuItem.Enabled = true;
                StatisticsToolStripMenuItem.Enabled = true;
                // hitIdentificationToolStripMenuItem.Enabled = true;
                buttonReduceDim.Enabled = true;
                // visualizationToolStripMenuItemFullScreen.Enabled = true;
                qualityControlToolStripMenuItem.Enabled = true;
                buttonCorrectionPlateByPlate.Enabled = true;
                buttonRejectPlates.Enabled = true;
                buttonNormalize.Enabled = true;
                ButtonClustering.Enabled = true;
                buttonStartClassification.Enabled = true;
                buttonExport.Enabled = true;
                //buttonDisplayWellsSelectionData.Enabled = true;
                platesManagerToolStripMenuItem.Enabled = true;
                betaToolStripMenuItem.Enabled = true;
                toolStripMenuItemGeneAnalysis.Enabled = true;
                viewToolStripMenuItem.Enabled = true;

                if (cGlobalInfo.CurrentScreening != null)
                    cGlobalInfo.CurrentScreening.ISLoading = false;


                comboBoxDescriptorToDisplay.SelectedIndex = 0;

                string NamePlate = PlateListWindow.listBoxAvaliableListPlates.Items[0].ToString();
                toolStripcomboBoxPlateList.Text = NamePlate + " ";

                if ((cGlobalInfo.CurrentScreening != null) && (checkBoxDisplayClasses.Checked))
                    cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);

                this.listViewForListWell.Items.Clear();
                cGlobalInfo.ListSelectedWell.Clear();



            }
        }

        public void RefreshInfoScreeningRichBox()
        {
            if (tabControlForScreening.SelectedTab.Name != "tabPageScreeningInfo") return;
            richTextBoxForScreeningInformation.Clear();

            if (cGlobalInfo.CurrentScreening == null) return;




            richTextBoxForScreeningInformation.AppendText(cGlobalInfo.CurrentScreening.GetInfo() + "\n");

        }


        //void RefreshClustering()
        //{
        //    if (tabControlMain.SelectedTab.Name == "tabPageClassification")
        //    {
        //        panelForClassSelectionClustering.Controls.Clear();
        //        PanelForClassSelection ClassSelectionPanel = new PanelForClassSelection(GlobalInfo);
        //        ClassSelectionPanel.Height = panelForClassSelectionClustering.Height;
        //        ClassSelectionPanel.UnSelectAll();
        //        panelForClassSelectionClustering.Controls.Add(ClassSelectionPanel);
        //    }

        //}

        // Convert and normalize the points and draw the reversible frame.
        private void MyDrawReversibleRectangle(Point p1, Point p2)
        {
            Rectangle rc = new Rectangle();

            // Convert the points to screen coordinates.
            p1 = PointToScreen(p1);
            //p1.X += tabControlMain.Location.X;
            //p1.Y += tabControlMain.Location.Y;

            p2 = PointToScreen(p2);
            //p2.X += tabControlMain.Location.X;
            //p2.Y += tabControlMain.Location.Y;

            // Normalize the rectangle.
            if (p1.X < p2.X)
            {
                rc.X = p1.X;
                rc.Width = p2.X - p1.X;
            }
            else
            {
                rc.X = p2.X;
                rc.Width = p1.X - p2.X;
            }
            if (p1.Y < p2.Y)
            {
                rc.Y = p1.Y;
                rc.Height = p2.Y - p1.Y;
            }
            else
            {
                rc.Y = p2.Y;
                rc.Height = p1.Y - p2.Y;
            }
            // Draw the reversible frame.

            ControlPaint.DrawReversibleFrame(rc, Color.Red, FrameStyle.Dashed);
        }

        private void panelForPlate_MouseDown(object sender, MouseEventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                cGlobalInfo.CurrentScreening.ClientPosFirst.X = e.X;
                cGlobalInfo.CurrentScreening.ClientPosFirst.Y = e.Y;

                //     if (GlobalInfo.WindowForDRCDesign.Visible) return;
                Point locationOnForm = this.panelForPlate.FindForm().PointToClient(Control.MousePosition);
                // int VertPos = locationOnForm.Y - 163;
                // Make a note that we "have the mouse".
                bHaveMouse = true;

                // Store the "starting point" for this rubber-band rectangle.
                cGlobalInfo.CurrentScreening.ptOriginal.X = locationOnForm.X;// e.X + this.panelForPlate.Location.X/* + 10*/;
                cGlobalInfo.CurrentScreening.ptOriginal.Y = locationOnForm.Y;// e.Y + this.panelForPlate.Location.Y/* + 76*/;
                // Special value lets us know that no previous
                // rectangle needs to be erased.
                cGlobalInfo.CurrentScreening.ptLast.X = -1;
                cGlobalInfo.CurrentScreening.ptLast.Y = -1;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                int ScrollShiftY = this.panelForPlate.VerticalScroll.Value;
                int ScrollShiftX = this.panelForPlate.HorizontalScroll.Value;
                int Gutter = (int)cGlobalInfo.OptionsWindow.FFAllOptions.numericUpDownGutter.Value;

                int PosX = (int)((e.X - ScrollShiftX) / (cGlobalInfo.SizeHistoWidth + Gutter));
                int PosY = (int)((e.Y - ScrollShiftY) / (cGlobalInfo.SizeHistoHeight + Gutter));

                bool OnlyOnSelected = false;

                cExtendedList cExL = new cExtendedList();
                List<string> Names = new List<string>();
                string CurrentName = "";

                #region Display plate heat map
                if ((PosX == 0) && (PosY == 0))
                {
                    cListWells ListWellsToProcess = new cListWells(null);
                    foreach (cWell item in cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListActiveWells)
                        if (item.GetCurrentClassIdx() != -1) ListWellsToProcess.Add(item);

                    cDesignerTab DT = new cDesignerTab();

                    for (int IdxDesc = 0; IdxDesc < cGlobalInfo.CurrentScreening.ListDescriptors.Count; IdxDesc++)
                    {
                        if (!cGlobalInfo.CurrentScreening.ListDescriptors[IdxDesc].IsActive()) continue;

                        bool IsMissing;

                        cExtendedTable NewTable = null;
                        if (!checkBoxDisplayClasses.Checked)
                            NewTable = new cExtendedTable(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetAverageValueDescTable(IdxDesc, out IsMissing));
                        else
                            NewTable = new cExtendedTable(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetWellClassesTable());

                        foreach (var item in NewTable)
                            item.ListTags = new List<object>();

                        for (int i = 0; i < cGlobalInfo.CurrentScreening.Columns; i++)
                            for (int j = 0; j < cGlobalInfo.CurrentScreening.Rows; j++)
                            {
                                cWell currentWell = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetWell(i, cGlobalInfo.CurrentScreening.Rows - j - 1, true);
                                NewTable[i].ListTags.Add(currentWell);
                            }

                        for (int IdxCol = 0; IdxCol < NewTable.Count; IdxCol++)
                            NewTable[IdxCol].Name = "Column " + (IdxCol + 1);

                        List<string> ListRow = new List<string>();
                        for (int IdxRow = 0; IdxRow < NewTable[0].Count; IdxRow++)
                            ListRow.Add("Row " + (NewTable[0].Count - IdxRow));

                        NewTable.ListRowNames = ListRow;
                        if (!checkBoxDisplayClasses.Checked)
                            NewTable.Name = cGlobalInfo.CurrentScreening.ListDescriptors[IdxDesc].GetName() + " (" + ListWellsToProcess.Count + " wells)";
                        else
                            NewTable.Name = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetName() + " - Well Associated Classes (" + ListWellsToProcess.Count + " wells)";

                        cViewerHeatMap VHM = new cViewerHeatMap();
                        VHM.SetInputData(NewTable);
                        VHM.GlobalInfo = GlobalInfo;
                        VHM.IsDisplayValues = false;
                        if (!checkBoxDisplayClasses.Checked)
                            VHM.Title = cGlobalInfo.CurrentScreening.ListDescriptors[IdxDesc].GetName() + " (" + ListWellsToProcess.Count + " wells)";
                        else
                        {
                            //VHM.ChartToBeIncluded
                            VHM.CurrentLUT = cGlobalInfo.ListWellClasses.BuildLUT();
                            VHM.IsAutomatedMinMax = false;
                            VHM.Min = 0;
                            VHM.Max = cGlobalInfo.ListWellClasses.Count - 1;
                            VHM.IsWellClassLegend = true;
                            VHM.Title = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetName() + " - Well Associated Classes (" + ListWellsToProcess.Count + " wells)";
                        }
                        VHM.Run();

                        DT.SetInputData(VHM.GetOutPut());

                        if (checkBoxDisplayClasses.Checked) break;

                    }

                    DT.Run();

                    cDisplayToWindow DWForPlate = new cDisplayToWindow();
                    DWForPlate.SetInputData(DT.GetOutPut());
                    DWForPlate.Run();
                    DWForPlate.Display();
                    return;
                }
                #endregion
                #region Display column or row graphs
                else if ((PosX == 0) && (PosY > 0))
                {
                    cExL.ListTags = new List<object>();
                    for (int col = 0; col < cGlobalInfo.CurrentScreening.Columns; col++)
                    {
                        cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate();
                        cWell TmpWell = CurrentPlateToProcess.GetWell(col, PosY - 1, OnlyOnSelected);
                        if (TmpWell == null) continue;
                        cExL.ListTags.Add(TmpWell);
                        cExL.Add(TmpWell.ListSignatures[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetValue());
                        Names.Add("Column " + col);
                    }
                    CurrentName = "Row " + PosY;
                }
                else if ((PosY == 0) && (PosX > 0))
                {
                    cExL.ListTags = new List<object>();
                    for (int row = 0; row < cGlobalInfo.CurrentScreening.Rows; row++)
                    {
                        cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate();
                        cWell TmpWell = CurrentPlateToProcess.GetWell(PosX - 1, row, OnlyOnSelected);
                        if (TmpWell == null) continue;
                        cExL.ListTags.Add(TmpWell);

                        cExL.Add(TmpWell.ListSignatures[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetValue());
                        Names.Add("Row " + row);
                    }
                    CurrentName = "Column " + PosX;
                }
                #endregion
                else
                {
                    ContextMenuStrip NewMenu = new ContextMenuStrip();
                    NewMenu.Items.Add(cGlobalInfo.CurrentScreening.GetExtendedContextMenu());
                    NewMenu.Items.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetExtendedContextMenu());
                    NewMenu.Show(Control.MousePosition);
                    return;
                }

                cExtendedTable cExT = new cExtendedTable(cExL);
                cExT.ListRowNames = Names;

                cExT.Name = CurrentName;
                cExT[0].Name = CurrentName;


                cViewerGraph1D CV = new cViewerGraph1D();
                CV.Chart.IsSelectable = true;
                CV.Chart.LabelAxisX = "Well Index";
                CV.Chart.LabelAxisY = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
                //V1D.Chart.BackgroundColor = Color.LightYellow;
                CV.Chart.IsXGrid = true;
                CV.Chart.IsLine = true;
                CV.SetInputData(cExT);

                cFeedBackMessage Mess = CV.Run();
                if (!Mess.IsSucceed)
                {
                    MessageBox.Show(Mess.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //cViewerGraph CV = new cViewerGraph();
                //CV.SetInputData(cExT);
                //CV.Run();

                cDesignerSinglePanel CD = new cDesignerSinglePanel();
                CD.SetInputData(CV.GetOutPut());
                CD.Run();

                cDisplayToWindow DW = new cDisplayToWindow();
                DW.SetInputData(CD.GetOutPut());
                DW.Run();
                DW.Display();
                // CV.Display();
            }
        }

        private void panelForPlate_MouseMove(object sender, MouseEventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            //  if (GlobalInfo.WindowForDRCDesign.Visible) return;


            // If we "have the mouse", then we draw our lines.
            if (bHaveMouse)
            {
                Point ptCurrent = this.panelForPlate.FindForm().PointToClient(Control.MousePosition);
                // Point ptCurrent = new Point(e.X + this.panelForPlate.Location.X /*+ 10*/, e.Y + this.panelForPlate.Location.Y /*+ 76*/);
                // If we have drawn previously, draw again in
                // that spot to remove the lines.
                if (cGlobalInfo.CurrentScreening.ptLast.X != -1)
                {
                    MyDrawReversibleRectangle(cGlobalInfo.CurrentScreening.ptOriginal, cGlobalInfo.CurrentScreening.ptLast);
                }
                // Update last point.
                cGlobalInfo.CurrentScreening.ptLast = ptCurrent;
                // Draw new lines.
                MyDrawReversibleRectangle(cGlobalInfo.CurrentScreening.ptOriginal, ptCurrent);
            }
        }

        private void panelForPlate_MouseUp(object sender, MouseEventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            //  if (GlobalInfo.WindowForDRCDesign.Visible) return;

            // Set internal flag to know we no longer "have the mouse".
            bHaveMouse = false;
            // If we have drawn previously, draw again in that spot
            // to remove the lines.
            if (cGlobalInfo.CurrentScreening.ptLast.X != -1)
            {
                Point ptCurrent = this.panelForPlate.FindForm().PointToClient(Control.MousePosition);
                //Point ptCurrent = new Point(e.X + panelForPlate.Location.X /*+ 10*/, e.Y + panelForPlate.Location.Y /*+ 76*/);
                MyDrawReversibleRectangle(cGlobalInfo.CurrentScreening.ptOriginal, cGlobalInfo.CurrentScreening.ptLast);


                cGlobalInfo.CurrentScreening.ClientPosLast.X = e.X;
                cGlobalInfo.CurrentScreening.ClientPosLast.Y = e.Y;

                if (!cGlobalInfo.WindowForDRCDesign.Visible)
                {
                    cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().UpDateWellsSelection();
                    cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false, panelForPlate);
                }
                else
                {
                    int SelectionType = cGlobalInfo.CurrentScreening.GetSelectionType();
                    if (SelectionType == -2) return;

                    int PosMouseXMax = cGlobalInfo.CurrentScreening.ClientPosLast.X;
                    int PosMouseXMin = cGlobalInfo.CurrentScreening.ClientPosFirst.X;

                    if (cGlobalInfo.CurrentScreening.ClientPosFirst.X > PosMouseXMax)
                    {
                        PosMouseXMax = cGlobalInfo.CurrentScreening.ClientPosFirst.X;
                        PosMouseXMin = cGlobalInfo.CurrentScreening.ClientPosLast.X;
                    }

                    int PosMouseYMax = cGlobalInfo.CurrentScreening.ClientPosLast.Y;
                    int PosMouseYMin = cGlobalInfo.CurrentScreening.ClientPosFirst.Y;
                    if (cGlobalInfo.CurrentScreening.ClientPosFirst.Y > PosMouseYMax)
                    {
                        PosMouseYMax = cGlobalInfo.CurrentScreening.ClientPosFirst.Y;
                        PosMouseYMin = cGlobalInfo.CurrentScreening.ClientPosLast.Y;
                    }

                    int GutterSize = (int)cGlobalInfo.OptionsWindow.FFAllOptions.numericUpDownGutter.Value;
                    int ScrollShiftX = cGlobalInfo.WindowHCSAnalyzer.panelForPlate.HorizontalScroll.Value;
                    int ScrollShiftY = cGlobalInfo.WindowHCSAnalyzer.panelForPlate.VerticalScroll.Value;

                    int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;

                    //Point ResMin = cGlobalInfo.WindowHCSAnalyzer.panelForPlate.GetChildAtPoint(new Point(PosMouseXMin, PosMouseYMin));

                    int PosWellMinX = (int)((PosMouseXMin - ScrollShiftX) / (cGlobalInfo.SizeHistoWidth + GutterSize));
                    int PosWellMinY = (int)((PosMouseYMin - ScrollShiftY) / (cGlobalInfo.SizeHistoHeight + GutterSize));

                    int PosWellMaxX = (int)((PosMouseXMax - ScrollShiftX) / (cGlobalInfo.SizeHistoWidth + GutterSize));
                    int PosWellMaxY = (int)((PosMouseYMax - ScrollShiftY) / (cGlobalInfo.SizeHistoHeight + GutterSize));


                    if (PosWellMaxX > cGlobalInfo.CurrentScreening.Columns) PosWellMaxX = cGlobalInfo.CurrentScreening.Columns;
                    if (PosWellMaxY > cGlobalInfo.CurrentScreening.Rows) PosWellMaxY = cGlobalInfo.CurrentScreening.Rows;
                    if (PosWellMinX < 0) PosWellMinX = 0;
                    if (PosWellMinY < 0) PosWellMinY = 0;

                    cGlobalInfo.WindowForDRCDesign.ListWells = new List<cWell>();

                    for (int j = PosWellMinY; j < PosWellMaxY; j++)
                        for (int i = PosWellMinX; i < PosWellMaxX; i++)
                        {

                            cWell TempWell = cGlobalInfo.CurrentScreening.ListPlatesActive[cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx].GetWell(i, j, false);

                            cGlobalInfo.WindowForDRCDesign.ListWells.Add(TempWell);

                            //if (TempWell == null) continue;
                            //   TempWell.SetClass(SelectionType);
                        }


                    //int PosMouseXMax = CompleteScreening.ptLast.X;
                    //int PosMouseXMin = CompleteScreening.ptOriginal.X;
                    //if (CompleteScreening.ptOriginal.X > PosMouseXMax)
                    //{
                    //    PosMouseXMax = CompleteScreening.ptOriginal.X;
                    //    PosMouseXMin = CompleteScreening.ptLast.X;
                    //}

                    //int PosMouseYMax = CompleteScreening.ptLast.Y;
                    //int PosMouseYMin = CompleteScreening.ptOriginal.Y;
                    //if (CompleteScreening.ptOriginal.Y > PosMouseYMax)
                    //{
                    //    PosMouseYMax = CompleteScreening.ptOriginal.Y;
                    //    PosMouseYMin = CompleteScreening.ptLast.Y;
                    //}

                    ////List<cWell> ListWellSelected = new List<cWell>();
                    //GlobalInfo.WindowForDRCDesign.ListWells = new List<cWell>();


                    //for (int j = 0; j < CompleteScreening.Rows; j++)
                    //    for (int i = 0; i < CompleteScreening.Columns; i++)
                    //    {
                    //        cWell TempWell = CompleteScreening.GetCurrentDisplayPlate().GetWell(i, j, false);
                    //        if (TempWell == null) continue;
                    //        //    int PWellX = (int)((TempWell.GetPosX() + 1) * (CompleteScreening.GlobalInfo.SizeHistoWidth + (int)GlobalInfo.OptionsWindow.numericUpDownGutter.Value));// - 2*cGlobalInfo.ShiftX);
                    //        //   int PWellY = (int)((TempWell.GetPosY() + 1) * (CompleteScreening.GlobalInfo.SizeHistoHeight + (int)GlobalInfo.OptionsWindow.numericUpDownGutter.Value) + +(int)((int)GlobalInfo.OptionsWindow.numericUpDownGutter.Value * 2.5) );// (int)cGlobalInfo.OptionsWindow.numericUpDownShiftY.Value);

                    //        //   if ((PWellX > PosMouseXMin) && (PWellX < PosMouseXMax) && (PWellY > PosMouseYMin) && (PWellY < PosMouseYMax))
                    //        {
                    //            GlobalInfo.WindowForDRCDesign.ListWells.Add(TempWell);
                    //        }
                    //    }

                    cGlobalInfo.WindowForDRCDesign.DrawSignature();
                }


                //    if (CompleteScreening.GlobalInfo.IsDisplayClassOnly)

            }
            // Set flags to know that there is no "previous" line to reverse.
            cGlobalInfo.CurrentScreening.ptLast.X = -1;
            cGlobalInfo.CurrentScreening.ptLast.Y = -1;
            cGlobalInfo.CurrentScreening.ptOriginal.X = -1;
            cGlobalInfo.CurrentScreening.ptOriginal.Y = -1;
        }

        private void panelForPlate_MouseWheel(object sender, MouseEventArgs e)
        {
            // Update the drawing based upon the mouse wheel scrolling.
            if (cGlobalInfo.CurrentScreening == null) return;

            int numberOfTextLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / 120;

            if (XTrackBarForZoom.trackBar.Value + numberOfTextLinesToMove > XTrackBarForZoom.trackBar.Maximum)
                XTrackBarForZoom.trackBar.Value = XTrackBarForZoom.trackBar.Maximum;
            else if (XTrackBarForZoom.trackBar.Value + numberOfTextLinesToMove < XTrackBarForZoom.trackBar.Minimum)
                XTrackBarForZoom.trackBar.Value = XTrackBarForZoom.trackBar.Minimum;
            else
                XTrackBarForZoom.trackBar.Value += numberOfTextLinesToMove;


            //int numberOfTextLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            //if (numberOfTextLinesToMove > 0)
            //   cGlobalInfo.CurrentScreening.GlobalInfo.ChangeSize(numberOfTextLinesToMove);
            //else
            //    cGlobalInfo.CurrentScreening.GlobalInfo.ChangeSize(1.0f / (-1 * numberOfTextLinesToMove));


        }
        #endregion

        #region Selection management
        private void applySelectionToScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            cGlobalInfo.CurrentScreening.ApplyCurrentClassesToAllPlates();
        }

        private void swapClassesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            FormForSwapClasses WindowSwapClasses = new FormForSwapClasses(GlobalInfo);

            PanelForClassSelection ClassSelectionPanel = new PanelForClassSelection(true, eClassType.WELL);
            ClassSelectionPanel.Height = WindowSwapClasses.panelToBeSwapped.Height;
            //ClassSelectionPanel.Location.Y = ClassSelectionPanel.Location.Y+ 20;
            ClassSelectionPanel.UnSelectAll();
            WindowSwapClasses.panelToBeSwapped.Controls.Add(ClassSelectionPanel);

            if (WindowSwapClasses.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            int Idx = 0;
            int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;


            // int OriginalIdx = WindowSwapClasses.comboBoxOriginalClass.SelectedIndex - 1;
            int DestinatonIdx = WindowSwapClasses.comboBoxDestinationClass.SelectedIndex - 1;

            // loop on all the plate
            for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
            {
                cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(PlateIdx);

                for (int IdxValue = 0; IdxValue < cGlobalInfo.CurrentScreening.Columns; IdxValue++)
                    for (int IdxValue0 = 0; IdxValue0 < cGlobalInfo.CurrentScreening.Rows; IdxValue0++)
                    {
                        cWell TmpWell = CurrentPlateToProcess.GetWell(IdxValue, IdxValue0, false);
                        if (TmpWell == null) continue;

                        if ((TmpWell.GetCurrentClassIdx() > -1) && (ClassSelectionPanel.GetListSelectedClass()[TmpWell.GetCurrentClassIdx()]))
                        {
                            if (DestinatonIdx == -1)
                                TmpWell.SetAsNoneSelected();
                            else
                                TmpWell.SetClass(DestinatonIdx);

                            Idx++;
                        }

                    }
            }
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
            MessageBox.Show(Idx + " wells have been swapped !", "Process over !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        #endregion

        #region Options window
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (cGlobalInfo.OptionsWindow == null) cGlobalInfo.OptionsWindow = new FormForOptionsWindow();

            cGlobalInfo.OptionsWindow.SetCurrentScreening(cGlobalInfo.CurrentScreening);
            cGlobalInfo.OptionsWindow.Visible = !cGlobalInfo.OptionsWindow.Visible;

            //if (cGlobalInfo.OptionsWindow.Visible)
            //{
            //    cGlobalInfo.OptionsWindow.panelForWellClasses.Controls.Clear();
            //    cGlobalInfo.OptionsWindow.panelForWellClasses.Controls.Add(new PanelForClassEditing(GlobalInfo));

            //    cGlobalInfo.OptionsWindow.panelForCellularPhenotypes.Controls.Clear();
            //    cGlobalInfo.OptionsWindow.panelForCellularPhenotypes.Controls.Add(new PanelForPhenotypeEditing(GlobalInfo));

            //}

            if (cGlobalInfo.CurrentScreening != null)
            {
                string CurrentDescX = cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosX.Text;
                string CurrentDescY = cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosY.Text;

                string CurrentDescBoundingMinX = cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinX.Text;
                string CurrentDescBoundingMinY = cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinY.Text;
                string CurrentDescBoundingMaxX = cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxX.Text;
                string CurrentDescBoundingMaxY = cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxY.Text;


                string CurrentDescField = cGlobalInfo.OptionsWindow.comboBoxDescriptorForField.Text;

                cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinX.Items.Clear();
                cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinY.Items.Clear();
                cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxX.Items.Clear();
                cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxY.Items.Clear();

                cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosX.Items.Clear();
                cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosY.Items.Clear();
                cGlobalInfo.OptionsWindow.comboBoxDescriptorForField.Items.Clear();

                foreach (var item in cGlobalInfo.CurrentScreening.ListDescriptors)
                {
                    if (item.GetDataType() != "Single")
                    {
                        cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinX.Items.Add(item.GetName());
                        cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinY.Items.Add(item.GetName());
                        cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxX.Items.Add(item.GetName());
                        cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxY.Items.Add(item.GetName());


                        cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosX.Items.Add(item.GetName());
                        cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosY.Items.Add(item.GetName());
                        cGlobalInfo.OptionsWindow.comboBoxDescriptorForField.Items.Add(item.GetName());
                        //MainWindow.comboBoxDescriptor2.Items.Add(item.GetName());
                    }
                }

                if (cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosX.Items.Contains(CurrentDescX))
                    cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosX.Text = CurrentDescX;
                else if (cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosX.Items.Contains("X"))
                    cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosX.Text = "X";

                if (cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosY.Items.Contains(CurrentDescY))
                    cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosY.Text = CurrentDescY;
                else if (cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosY.Items.Contains("Y"))
                    cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosY.Text = "Y";

                if (cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinX.Items.Contains(CurrentDescBoundingMinX))
                    cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinX.Text = CurrentDescBoundingMinX;
                else if (cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinX.Items.Contains("ListBoundingBoxCoord_1_"))
                    cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinX.Text = "ListBoundingBoxCoord_1_";
                else if (cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinX.Items.Contains("X_Min"))
                    cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinX.Text = "X_Min";

                if (cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinY.Items.Contains(CurrentDescBoundingMinY))
                    cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinY.Text = CurrentDescBoundingMinY;
                else if (cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinY.Items.Contains("ListBoundingBoxCoord_2_"))
                    cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinY.Text = "ListBoundingBoxCoord_2_";
                else if (cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinY.Items.Contains("Y_Min"))
                    cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinY.Text = "Y_Min";

                if (cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxX.Items.Contains(CurrentDescBoundingMaxX))
                    cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxX.Text = CurrentDescBoundingMaxX;
                else if (cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxX.Items.Contains("ListBoundingBoxCoord_3_"))
                    cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxX.Text = "ListBoundingBoxCoord_3_";
                else if (cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxX.Items.Contains("X_Max"))
                    cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxX.Text = "X_Max";

                if (cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxY.Items.Contains(CurrentDescBoundingMaxY))
                    cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxY.Text = CurrentDescBoundingMaxY;
                else if (cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxY.Items.Contains("ListBoundingBoxCoord_4_"))
                    cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxY.Text = "ListBoundingBoxCoord_4_";
                else if (cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxY.Items.Contains("Y_Max"))
                    cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxY.Text = "Y_Max";


                if (cGlobalInfo.OptionsWindow.comboBoxDescriptorForField.Items.Contains(CurrentDescField))
                    cGlobalInfo.OptionsWindow.comboBoxDescriptorForField.Text = CurrentDescField;
                else if (cGlobalInfo.OptionsWindow.comboBoxDescriptorForField.Items.Contains("Field"))
                    cGlobalInfo.OptionsWindow.comboBoxDescriptorForField.Text = "Field";


                cGlobalInfo.OptionsWindow.FFAllOptions.textBoxScreeningName.Enabled = true;
                cGlobalInfo.OptionsWindow.FFAllOptions.textBoxScreeningName.Text = cGlobalInfo.CurrentScreening.GetName();


            }

            cGlobalInfo.OptionsWindow.Update();
        }

        #endregion

        #region Misc menus (console, plates manager, exit, about)


        private void platesManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            PlateListWindow.listBoxPlateNameToProcess.Items.Clear();
            for (int i = 0; i < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; i++)
                PlateListWindow.listBoxPlateNameToProcess.Items.Add(cGlobalInfo.CurrentScreening.ListPlatesActive[i].GetName());


            PlateListWindow.ShowDialog();// != System.Windows.Forms.DialogResult.OK) return; 

            while (PlateListWindow.listBoxPlateNameToProcess.Items.Count == 0)
            {
                MessageBox.Show("At least one plate has to be selected !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PlateListWindow.ShowDialog();// != System.Windows.Forms.DialogResult.OK) return; 
            }
            toolStripcomboBoxPlateList.Items.Clear();
            cGlobalInfo.CurrentScreening.ListPlatesActive.Clear();
            RefreshInfoScreeningRichBox();

            for (int i = 0; i < PlateListWindow.listBoxPlateNameToProcess.Items.Count; i++)
            {
                cGlobalInfo.CurrentScreening.ListPlatesActive.Add(cGlobalInfo.CurrentScreening.ListPlatesAvailable.GetPlate((string)PlateListWindow.listBoxPlateNameToProcess.Items[i]));
                toolStripcomboBoxPlateList.Items.Add(cGlobalInfo.CurrentScreening.ListPlatesActive[i].GetName());
            }
            cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx = 0;
            toolStripcomboBoxPlateList.SelectedIndex = 0;

            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening != null)
                cGlobalInfo.CurrentScreening.Close3DView();
            this.Dispose();
        }

        private void aboutHCSAnalyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox About = new AboutBox();
            About.Text = "HCS Analyzer";
            //About.Opacity = 0.9;
            About.ShowDialog();
        }
        #endregion

        #region Quality controls - Zfactor, SSMD, Coeff. of variation, descriptor evolution

        /// <summary>
        /// This function displays the evolution of the average value of a certain descriptor through the plates, for a specified class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void descriptorEvolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  if (CompleteScreening == null) return;

            #region extract classes of interest
            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            GUI_ListClasses.IsCheckBoxes = !_DescEvolCellByCellItem.Checked;
            GUI_ListClasses.IsSelectAll = false;

            if (GUI_ListClasses.Run().IsSucceed == false) return;
            cExtendedTable ListClassSelected = GUI_ListClasses.GetOutPut();

            if (ListClassSelected.Sum() < 1)
            {
                MessageBox.Show("At least one classe has to be selected.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            cDescriptorType DT = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor();

            cGUI_ListPlates GUI_ListPlates = new cGUI_ListPlates();
            GUI_ListPlates.IsCheckBoxes = true;
            GUI_ListPlates.ListInitialPlates = cGlobalInfo.CurrentScreening.ListPlatesActive;
            // GUI_ListClasses.ListInitialPlates = null;
            cFeedBackMessage FBM = GUI_ListPlates.Run();
            richTextBoxConsole.AppendText(FBM.Message);
            if (!FBM.IsSucceed) return;

            cListPlates LP = GUI_ListPlates.GetOutPut();


            if (_DescEvolCellByCellItem.Checked)
            {
                cExtendedTable FinalTable = new cExtendedTable();
                foreach (cPlate CurrentPlate in LP)
                {
                    cExtendedTable TmpFinalTable = new cExtendedTable();

                    int NumberOfWells = 0;
                    foreach (cWell item in CurrentPlate.ListActiveWells)
                    {
                        if (item.GetCurrentClassIdx() == -1) continue;
                        if (ListClassSelected[0][item.GetCurrentClassIdx()] == 1)
                        {
                            cExtendedTable TmpET = item.GetValuesList(DT);

                            if (TmpFinalTable.Count == 0)
                            {
                                TmpFinalTable = new cExtendedTable(TmpET);
                            }
                            else
                            {
                                cMerge M = new cMerge();
                                M.IsHorizontal = false;
                                M.SetInputData(TmpFinalTable, TmpET);
                                M.Run();
                                TmpFinalTable = M.GetOutPut();
                            }
                            NumberOfWells++;
                        }
                    }

                    if (NumberOfWells == 0) continue;

                    if (FinalTable.Count == 0)
                    {
                        FinalTable = new cExtendedTable(TmpFinalTable);
                        FinalTable[0].Name = CurrentPlate.GetName() + " (" + NumberOfWells + " wells)";
                    }
                    else
                    {
                        cMerge M = new cMerge();
                        M.IsHorizontal = true;
                        TmpFinalTable[0].Name = CurrentPlate.GetName() + " (" + NumberOfWells + " wells)";
                        M.SetInputData(FinalTable, TmpFinalTable);
                        M.Run();
                        FinalTable = M.GetOutPut();
                    }


                }
                cViewerHistogram VH = new cViewerHistogram();
                FinalTable.Name = DT.GetName();
                VH.Chart.LabelAxisX = DT.GetName();
                VH.Chart.IsArea = true;
                VH.Chart.IsPoint = false;
                //VH.Chart.IsBar = false;
                //VH.Chart.IsShadow = true;
                VH.Chart.IsXGrid = VH.Chart.IsYGrid = false;
                VH.Chart.Opacity = 200;
                VH.Chart.IsLegend = true;
                VH.SetInputData(FinalTable);
                VH.Chart.CurrentTitle.Text = cGlobalInfo.CurrentScreening.ListPlatesActive.Count + " plates";
                VH.Run();

                cDisplayToWindow DTW = new cDisplayToWindow();
                DTW.SetInputData(VH.GetOutPut());
                DTW.Title = "Cell-by-Cell Descriptor Evolution";
                DTW.Run();

                DTW.Display();


                //cExtendedTable ET = CompleteScreening.GetCurrentDisplayPlate().DBConnection.GetWellValues(
                //    this.AssociatedPlate.DBConnection.GetWellValues(this,
                //                         LCDT,
                //                         ListCellularPhenotypesToBeSelected);
            }
            else
            {
                cDisplayDescriptorEvolutions DEV = new cDisplayDescriptorEvolutions();
                DEV.ListPlates = LP;
                DEV.SetInputData(ListClassSelected);
                DEV.Run(cGlobalInfo.CurrentScreening);
            }
        }

        //private void zscoreToolStripMenuItem_Click_1(object sender, EventArgs e)
        //{
        //    if (CompleteScreening == null) return;

        //    BuildZFactor(this.comboBoxDescriptorToDisplay.SelectedIndex).Show();
        //}



        //private SimpleForm BuildZFactor(int Desc)
        //{
        //    List<double> Pos = new List<double>();
        //    List<double> Neg = new List<double>();
        //    List<cSimpleSignature> ZFactorList = new List<cSimpleSignature>();

        //    cWell TempWell;
        //    int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;

        //    // loop on all the plate
        //    for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
        //    {
        //        cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(cGlobalInfo.CurrentScreening.ListPlatesActive[PlateIdx].GetName());

        //        Pos.Clear();
        //        Neg.Clear();


        //        for (int row = 0; row < cGlobalInfo.CurrentScreening.Rows; row++)
        //            for (int col = 0; col < cGlobalInfo.CurrentScreening.Columns; col++)
        //            {
        //                TempWell = CurrentPlateToProcess.GetWell(col, row, true);
        //                if (TempWell == null) continue;
        //                else
        //                {
        //                    if (TempWell.GetCurrentClassIdx() == 0)
        //                        Pos.Add(TempWell.ListSignatures[Desc].GetValue());
        //                    if (TempWell.GetCurrentClassIdx() == 1)
        //                        Neg.Add(TempWell.ListSignatures[Desc].GetValue());
        //                }
        //            }

        //        double ZScore = 1 - 3 * (std(Pos.ToArray()) + std(Neg.ToArray())) / (Math.Abs(Mean(Pos.ToArray()) - Mean(Neg.ToArray())));
        //        GlobalInfo.ConsoleWriteLine(CurrentPlateToProcess.GetName() + ", Z-Score = " + ZScore);
        //        cSimpleSignature TmpDesc = new cSimpleSignature(CurrentPlateToProcess.GetName(), ZScore);
        //        ZFactorList.Add(TmpDesc);
        //    }

        //    Series CurrentSeries = new Series();
        //    CurrentSeries.ChartType = SeriesChartType.Column;
        //    CurrentSeries.ShadowOffset = 1;

        //    Series SeriesLine = new Series();
        //    SeriesLine.Name = "SeriesLine";
        //    SeriesLine.ShadowOffset = 1;
        //    SeriesLine.ChartType = SeriesChartType.Line;

        //    int RealIdx = 0;
        //    for (int IdxValue = 0; IdxValue < ZFactorList.Count; IdxValue++)
        //    {
        //        if (ZFactorList[IdxValue].AverageValue.ToString() == "NaN") continue;

        //        CurrentSeries.Points.Add(ZFactorList[IdxValue].AverageValue);
        //        CurrentSeries.Points[RealIdx].Label = string.Format("{0:0.###}", ZFactorList[IdxValue].AverageValue);
        //        CurrentSeries.Points[RealIdx].Font = new Font("Arial", 10);
        //        CurrentSeries.Points[RealIdx].ToolTip = ZFactorList[IdxValue].Name;
        //        CurrentSeries.Points[RealIdx].AxisLabel = ZFactorList[IdxValue].Name;

        //        SeriesLine.Points.Add(ZFactorList[IdxValue].AverageValue);
        //        SeriesLine.Points[RealIdx].BorderColor = Color.Black;
        //        SeriesLine.Points[RealIdx].MarkerStyle = MarkerStyle.Circle;
        //        SeriesLine.Points[RealIdx].MarkerSize = 4;
        //        RealIdx++;
        //    }

        //    SimpleForm NewWindow = new SimpleForm();
        //    int thisWidth = 200 * RealIdx;
        //    NewWindow.Width = thisWidth;
        //    NewWindow.Height = 400;
        //    NewWindow.Text = "Z-factors";

        //    ChartArea CurrentChartArea = new ChartArea();
        //    CurrentChartArea.BorderColor = Color.Black;
        //    CurrentChartArea.AxisX.Interval = 1;
        //    NewWindow.chartForSimpleForm.Series.Add(CurrentSeries);
        //    NewWindow.chartForSimpleForm.Series.Add(SeriesLine);

        //    CurrentChartArea.AxisX.IsLabelAutoFit = true;
        //    NewWindow.chartForSimpleForm.ChartAreas.Add(CurrentChartArea);

        //    CurrentChartArea.Axes[1].Maximum = 1.1;
        //    CurrentChartArea.Axes[1].IsMarksNextToAxis = true;
        //    CurrentChartArea.Axes[0].MajorGrid.Enabled = false;
        //    CurrentChartArea.Axes[1].MajorGrid.Enabled = false;

        //    NewWindow.chartForSimpleForm.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
        //    CurrentChartArea.BackColor = Color.White;

        //    Title CurrentTitle = new Title(cGlobalInfo.CurrentScreening.ListDescriptors[Desc].GetName() + " Z-factors");
        //    CurrentTitle.Font = new System.Drawing.Font("Arial", 11, FontStyle.Bold);
        //    NewWindow.chartForSimpleForm.Titles.Add(CurrentTitle);

        //    return NewWindow;
        //}

        //private void sSMDToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (cGlobalInfo.CurrentScreening == null) return;
        //    BuildSSMD(this.comboBoxDescriptorToDisplay.SelectedIndex).Show();
        //}


        private class cSimpleSignature
        {
            public cSimpleSignature(string Name, double Value)
            {
                this.Name = Name;
                this.AverageValue = Value;

            }


            public string Name;
            public double AverageValue;

        }

        //private SimpleForm BuildSSMD(int Desc)
        //{
        //    List<double> Pos = new List<double>();
        //    List<double> Neg = new List<double>();
        //    List<cSimpleSignature> ZFactorList = new List<cSimpleSignature>();

        //    cWell TempWell;
        //    int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;

        //    // loop on all the plate
        //    for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
        //    {
        //        cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(cGlobalInfo.CurrentScreening.ListPlatesActive[PlateIdx].GetName());

        //        Pos.Clear();
        //        Neg.Clear();

        //        for (int row = 0; row < cGlobalInfo.CurrentScreening.Rows; row++)
        //            for (int col = 0; col < cGlobalInfo.CurrentScreening.Columns; col++)
        //            {
        //                TempWell = CurrentPlateToProcess.GetWell(col, row, true);
        //                if (TempWell == null) continue;
        //                else
        //                {
        //                    if (TempWell.GetCurrentClassIdx() == 0)
        //                        Pos.Add(TempWell.ListSignatures[Desc].GetValue());
        //                    if (TempWell.GetCurrentClassIdx() == 1)
        //                        Neg.Add(TempWell.ListSignatures[Desc].GetValue());
        //                }
        //            }

        //        double SSMDScore = (Mean(Pos.ToArray()) - Mean(Neg.ToArray())) / Math.Sqrt(std(Pos.ToArray()) * std(Pos.ToArray()) + std(Neg.ToArray()) * std(Neg.ToArray()));
        //        GlobalInfo.ConsoleWriteLine(CurrentPlateToProcess.GetName() + ", SSMD = " + SSMDScore);

        //        //cDescriptor TmpDesc = new cDescriptor(SSMDScore, CurrentPlateToProcess.Name);
        //        cSimpleSignature TmpDesc = new cSimpleSignature(CurrentPlateToProcess.GetName(), SSMDScore);
        //        ZFactorList.Add(TmpDesc);
        //    }

        //    Series CurrentSeries = new Series();
        //    CurrentSeries.ChartType = SeriesChartType.Column;
        //    CurrentSeries.ShadowOffset = 1;

        //    Series SeriesLine = new Series();
        //    SeriesLine.Name = "SeriesLine";
        //    SeriesLine.ShadowOffset = 1;
        //    SeriesLine.ChartType = SeriesChartType.Line;

        //    int RealIdx = 0;
        //    for (int IdxValue = 0; IdxValue < ZFactorList.Count; IdxValue++)
        //    {
        //        if (ZFactorList[IdxValue].AverageValue.ToString() == "NaN") continue;

        //        CurrentSeries.Points.Add(ZFactorList[IdxValue].AverageValue);
        //        CurrentSeries.Points[RealIdx].Label = string.Format("{0:0.###}", ZFactorList[IdxValue].AverageValue);
        //        CurrentSeries.Points[RealIdx].Font = new Font("Arial", 10);
        //        CurrentSeries.Points[RealIdx].ToolTip = ZFactorList[IdxValue].Name;
        //        CurrentSeries.Points[RealIdx].AxisLabel = ZFactorList[IdxValue].Name;

        //        SeriesLine.Points.Add(ZFactorList[IdxValue].AverageValue);
        //        SeriesLine.Points[RealIdx].BorderColor = Color.Black;
        //        SeriesLine.Points[RealIdx].MarkerStyle = MarkerStyle.Circle;
        //        SeriesLine.Points[RealIdx].MarkerSize = 4;
        //        RealIdx++;
        //    }

        //    SimpleForm NewWindow = new SimpleForm();
        //    int thisWidth = 200 * RealIdx;
        //    if (thisWidth > (int)cGlobalInfo.OptionsWindow.numericUpDownMaximumWidth.Value)
        //        thisWidth = (int)cGlobalInfo.OptionsWindow.numericUpDownMaximumWidth.Value;
        //    NewWindow.Width = thisWidth;
        //    NewWindow.Height = 400;
        //    NewWindow.Text = "SSMD";

        //    ChartArea CurrentChartArea = new ChartArea();
        //    CurrentChartArea.BorderColor = Color.Black;
        //    CurrentChartArea.AxisX.Interval = 1;
        //    NewWindow.chartForSimpleForm.Series.Add(CurrentSeries);
        //    NewWindow.chartForSimpleForm.Series.Add(SeriesLine);

        //    CurrentChartArea.AxisX.IsLabelAutoFit = true;
        //    NewWindow.chartForSimpleForm.ChartAreas.Add(CurrentChartArea);

        //    // CurrentChartArea.Axes[1].Maximum = 2;
        //    CurrentChartArea.Axes[1].IsMarksNextToAxis = true;
        //    CurrentChartArea.Axes[0].MajorGrid.Enabled = false;
        //    CurrentChartArea.Axes[1].MajorGrid.Enabled = false;

        //    NewWindow.chartForSimpleForm.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
        //    CurrentChartArea.BackColor = Color.White;

        //    Title CurrentTitle = new Title(cGlobalInfo.CurrentScreening.ListDescriptors[Desc].GetName() + " SSMD");
        //    CurrentTitle.Font = new System.Drawing.Font("Arial", 11, FontStyle.Bold);
        //    NewWindow.chartForSimpleForm.Titles.Add(CurrentTitle);

        //    return NewWindow;
        //}


        #endregion

        #region Histograms section

        //private void stackedHistogramToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (CompleteScreening == null) return;
        //    if ((CompleteScreening.ListDescriptors == null) || (CompleteScreening.ListDescriptors.Count == 0)) return;
        //    DisplayStackedHisto(CompleteScreening.ListDescriptors.CurrentSelectedDescriptor);
        //}

        //public void DisplayStackedHisto(int IdxDesc)
        //{
        //    FormForMultipleClassSelection WindowForClassSelection = new FormForMultipleClassSelection();
        //    PanelForClassSelection ClassSelectionPanel = new PanelForClassSelection(GlobalInfo, true);
        //    ClassSelectionPanel.Height = WindowForClassSelection.splitContainerForClassSelection.Panel1.Height;
        //    WindowForClassSelection.splitContainerForClassSelection.Panel1.Controls.Add(ClassSelectionPanel);

        //    if (WindowForClassSelection.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
        //    //     WindowForClassSelection.panelForClassesSelection = new 

        //    cExtendedList[] ListValuesForHisto = new cExtendedList[/*ClassSelectionPanel.GetListIndexSelectedClass().Count*/ cGlobalInfo.ListWellClasses.Count];

        //    List<bool> ListSelectedClass = ClassSelectionPanel.GetListSelectedClass();

        //    for (int i = 0; i < ListValuesForHisto.Length; i++)
        //        ListValuesForHisto[i] = new cExtendedList();

        //    cWell TempWell;

        //    int NumberOfPlates = CompleteScreening.ListPlatesActive.Count;

        //    double MinValue = double.MaxValue;
        //    double MaxValue = double.MinValue;
        //    double CurrentValue;

        //    // loop on all the plate
        //    for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
        //    {
        //        cPlate CurrentPlateToProcess = CompleteScreening.ListPlatesActive.GetPlate(CompleteScreening.ListPlatesActive[PlateIdx].Name);

        //        for (int row = 0; row < CompleteScreening.Rows; row++)
        //            for (int col = 0; col < CompleteScreening.Columns; col++)
        //            {
        //                TempWell = CurrentPlateToProcess.GetWell(col, row, false);
        //                if (TempWell == null) continue;
        //                else
        //                {
        //                    if (TempWell.GetClassIdx() >= 0)
        //                    {
        //                        CurrentValue = TempWell.ListDescriptors[IdxDesc].GetValue();
        //                        ListValuesForHisto[TempWell.GetClassIdx()].Add(CurrentValue);
        //                        if (CurrentValue < MinValue) MinValue = CurrentValue;
        //                        if (CurrentValue > MaxValue) MaxValue = CurrentValue;
        //                    }
        //                }
        //            }
        //    }
        //    SimpleForm NewWindow = new SimpleForm();
        //    List<double[]>[] HistoPos = new List<double[]>[ListValuesForHisto.Length];
        //    Series[] SeriesPos = new Series[ListValuesForHisto.Length];


        //    for (int i = 0; i < ListValuesForHisto.Length; i++)
        //    {
        //        HistoPos[i] = new List<double[]>();
        //        if (ListSelectedClass[i])
        //            HistoPos[i] = ListValuesForHisto[i].CreateHistogram(MinValue, MaxValue, (int)GlobalInfo.OptionsWindow.numericUpDownHistoBin.Value);

        //        SeriesPos[i] = new Series();
        //    }

        //    for (int i = 0; i < SeriesPos.Length; i++)
        //    {
        //        int Max = 0;
        //        if (HistoPos[i].Count > 0)
        //            Max = HistoPos[i][0].Length;

        //        for (int IdxValue = 0; IdxValue < Max; IdxValue++)
        //        {
        //            SeriesPos[i].Points.AddXY(MinValue + ((MaxValue - MinValue) * IdxValue) / Max, HistoPos[i][1][IdxValue]);
        //            SeriesPos[i].Points[IdxValue].ToolTip = HistoPos[i][1][IdxValue].ToString();
        //            if (CompleteScreening.SelectedClass == -1)
        //                SeriesPos[i].Points[IdxValue].Color = Color.Black;
        //            else
        //                SeriesPos[i].Points[IdxValue].Color = CompleteScreening.GlobalInfo.ListWellClasses[i].ColourForDisplay;
        //        }
        //    }
        //    ChartArea CurrentChartArea = new ChartArea();
        //    CurrentChartArea.BorderColor = Color.Black;

        //    NewWindow.chartForSimpleForm.ChartAreas.Add(CurrentChartArea);
        //    CurrentChartArea.Axes[0].MajorGrid.Enabled = false;
        //    CurrentChartArea.Axes[0].Title = CompleteScreening.ListDescriptors[IdxDesc].GetName();
        //    CurrentChartArea.Axes[1].Title = "Sum";
        //    CurrentChartArea.AxisX.LabelStyle.Format = "N2";

        //    NewWindow.chartForSimpleForm.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
        //    CurrentChartArea.BackGradientStyle = GradientStyle.TopBottom;
        //    CurrentChartArea.BackColor = CompleteScreening.GlobalInfo.OptionsWindow.panel1.BackColor;
        //    CurrentChartArea.BackSecondaryColor = Color.White;


        //    for (int i = 0; i < SeriesPos.Length; i++)
        //    {
        //        SeriesPos[i].ChartType = SeriesChartType.StackedColumn;
        //        // SeriesPos[i].Color = CompleteScreening.GlobalInfo.GetColor(1);
        //        if (ListSelectedClass[i])
        //            NewWindow.chartForSimpleForm.Series.Add(SeriesPos[i]);
        //    }
        //    //Series SeriesGaussNeg = new Series();
        //    //SeriesGaussNeg.ChartType = SeriesChartType.Spline;

        //    //Series SeriesGaussPos = new Series();
        //    //SeriesGaussPos.ChartType = SeriesChartType.Spline;

        //    //if (HistoPos.Count != 0)
        //    //{
        //    //    double[] HistoGaussPos = CreateGauss(Mean(Pos.ToArray()), std(Pos.ToArray()), HistoPos[0].Length);

        //    //    SeriesGaussPos.Color = Color.Black;
        //    //    SeriesGaussPos.BorderWidth = 2;
        //    //}
        //    //SeriesGaussNeg.Color = Color.Black;
        //    //SeriesGaussNeg.BorderWidth = 2;

        //    //NewWindow.chartForSimpleForm.Series.Add(SeriesGaussNeg);
        //    //NewWindow.chartForSimpleForm.Series.Add(SeriesGaussPos);
        //    NewWindow.chartForSimpleForm.ChartAreas[0].CursorX.IsUserEnabled = true;
        //    NewWindow.chartForSimpleForm.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
        //    NewWindow.chartForSimpleForm.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
        //    NewWindow.chartForSimpleForm.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

        //    Title CurrentTitle = null;

        //    CurrentTitle = new Title(CompleteScreening.ListDescriptors[IdxDesc].GetName() + " Stacked histogram.");

        //    CurrentTitle.Font = new System.Drawing.Font("Arial", 11, FontStyle.Bold);
        //    NewWindow.chartForSimpleForm.Titles.Add(CurrentTitle);
        //    NewWindow.Text = CurrentTitle.Text;
        //    NewWindow.Show();
        //    NewWindow.chartForSimpleForm.Update();
        //    NewWindow.chartForSimpleForm.Show();
        //    NewWindow.Controls.AddRange(new System.Windows.Forms.Control[] { NewWindow.chartForSimpleForm });
        //    return;
        //}
        #endregion

        private void buttonNewClassificationProcess_Click(object sender, EventArgs e)
        {
            cMachineLearning MachineLearningForClassif = new cMachineLearning(this.GlobalInfo);
            //cParamAlgo AlgoAndParameters = MachineLearningForClassif.AskAndGetClassifAlgo();


            // List<bool> ListClassSelected = ((PanelForClassSelection)panelForClassSelectionClustering.Controls[0]).GetListSelectedClass();

            //    cInfoClass InfoClass = new cInfoClass();
            //Instances ListInstances =  CompleteScreening.GetCurrentDisplayPlate().CreateInstancesWithClasses(ListClassSelected);
            cInfoClass InfoClass = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetNumberOfClassesBut(comboBoxNeutralClassForClassif.SelectedIndex);


            Instances ListInstances = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().CreateInstancesWithClasses(InfoClass, comboBoxNeutralClassForClassif.SelectedIndex);

            //panelForClassSelectionClustering

            weka.classifiers.Evaluation EvalClassif = new weka.classifiers.Evaluation(ListInstances);

            MachineLearningForClassif.PerformTraining(MachineLearningForClassif.AskAndGetClassifAlgo(),
                                                        ListInstances,
                //InfoClass.NumberOfClass,
                                                        richTextBoxInfoClustering,
                                                        panelTMPForFeedBack,
                                                        out EvalClassif,
                                                        false);
        }

        private void heatMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            List<cPanelForDisplayArray> ListPlates = new List<cPanelForDisplayArray>();

            ListPlates.Add(new FormToDisplayPlate(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate()));

            cWindowToDisplayEntireScreening WindowToDisplayArray = new cWindowToDisplayEntireScreening(ListPlates, cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName(), 6);

            WindowToDisplayArray.Show();
        }


        #region PCA


        //private void pCAToolStripMenuItem2_Click(object sender, EventArgs e)
        //{
        //    ComputeAndDisplayPCA(CompleteScreening.ListPlatesActive);
        //}

        //private void ComputeAndDisplayPCA(cListPlates PlatesToProcess)
        //{
        //    if (CompleteScreening == null) return;
        //    FormClassification WindowClassification = new FormClassification(CompleteScreening);
        //    WindowClassification.label1.Text = "Class of interest";
        //    WindowClassification.Text = "PCA";
        //    WindowClassification.buttonClassification.Text = "Process";

        //    if (WindowClassification.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

        //    int NeutralClass = WindowClassification.comboBoxForNeutralClass.SelectedIndex;

        //    int NumWell = 0;
        //    int NumWellForLearning = 0;
        //    foreach (cPlate CurrentPlate in PlatesToProcess)
        //    {
        //        foreach (cWell CurrentWell in CurrentPlate.ListActiveWells)
        //        {
        //            if (CurrentWell.GetClassIdx() == NeutralClass)
        //                NumWellForLearning++;
        //        }
        //        NumWell += CurrentPlate.GetNumberOfActiveWells();
        //    }

        //    if (NumWellForLearning == 0)
        //    {
        //        MessageBox.Show("No well of the selected class identified", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }


        //    int NumDesc = CompleteScreening.GetNumberOfActiveDescriptor();

        //    if (NumDesc <= 1)
        //    {
        //        MessageBox.Show("More than one descriptor are required for this operation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    double[,] DataForPCA = new double[NumWellForLearning, CompleteScreening.GetNumberOfActiveDescriptor() + 1];

        //    //   return;
        //    Matrix EigenVectors = PCAComputation(DataForPCA, NumWellForLearning, NumDesc, NeutralClass, PlatesToProcess);
        //    if (EigenVectors == null) return;

        //    SimpleForm NewWindow = new SimpleForm(CompleteScreening);
        //    Series CurrentSeries = new Series();
        //    CurrentSeries.ShadowOffset = 1;

        //    Matrix CurrentPt = new Matrix(NumWell, NumDesc);
        //    DataForPCA = new double[NumWell, NumDesc + 1];

        //    for (int desc = 0; desc < NumDesc; desc++)
        //    {
        //        if (CompleteScreening.ListDescriptors[desc].IsActive() == false) continue;
        //        List<double> CurrentDesc = new List<double>();
        //        foreach (cPlate CurrentPlate in PlatesToProcess)
        //        {
        //            for (int IdxValue = 0; IdxValue < CompleteScreening.Columns; IdxValue++)
        //                for (int IdxValue0 = 0; IdxValue0 < CompleteScreening.Rows; IdxValue0++)
        //                {
        //                    cWell TmpWell = CurrentPlate.GetWell(IdxValue, IdxValue0, true);
        //                    if (TmpWell == null) continue;
        //                    CurrentDesc.Add(TmpWell.ListDescriptors[desc].GetValue());
        //                }
        //        }
        //        for (int i = 0; i < NumWell; i++)
        //            DataForPCA[i, desc] = CurrentDesc[i];
        //    }

        //    int IDx = 0;
        //    foreach (cPlate CurrentPlate in PlatesToProcess)
        //    {
        //        for (int IdxValue = 0; IdxValue < CompleteScreening.Columns; IdxValue++)
        //            for (int IdxValue0 = 0; IdxValue0 < CompleteScreening.Rows; IdxValue0++)
        //            {
        //                cWell TmpWell = CurrentPlate.GetWell(IdxValue, IdxValue0, true);
        //                if (TmpWell == null) continue;
        //                DataForPCA[IDx++, NumDesc] = TmpWell.GetClassIdx();
        //            }
        //    }

        //    for (int i = 0; i < NumWell; i++)
        //        for (int j = 0; j < NumDesc; j++) CurrentPt.addElement(i, j, DataForPCA[i, j]);

        //    Matrix NewPt = new Matrix(NumWell, NumDesc);

        //    NewPt = CurrentPt.multiply(EigenVectors);

        //    double MinY = double.MaxValue, MaxY = double.MinValue;

        //    for (int IdxValue0 = 0; IdxValue0 < NumWell; IdxValue0++)
        //    {
        //        double CurrentY = NewPt.getElement(IdxValue0, 1);

        //        if (CurrentY < MinY) MinY = CurrentY;
        //        if (CurrentY > MaxY) MaxY = CurrentY;

        //        CurrentSeries.Points.AddXY(NewPt.getElement(IdxValue0, 0), CurrentY);

        //        CurrentSeries.Points[IdxValue0].Color = CompleteScreening.GlobalInfo.ListWellClasses[(int)DataForPCA[IdxValue0, NumDesc]].ColourForDisplay;
        //        CurrentSeries.Points[IdxValue0].MarkerStyle = MarkerStyle.Circle;
        //        CurrentSeries.Points[IdxValue0].MarkerSize = 8;
        //    }

        //    ChartArea CurrentChartArea = new ChartArea();
        //    CurrentChartArea.BorderColor = Color.Black;

        //    NewWindow.chartForSimpleForm.ChartAreas.Add(CurrentChartArea);
        //    NewWindow.chartForSimpleForm.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
        //    CurrentChartArea.BackColor = Color.FromArgb(164, 164, 164);

        //    string AxeName = "";
        //    int IDxDesc = 0;
        //    for (int Desc = 0; Desc < CompleteScreening.ListDescriptors.Count; Desc++)
        //    {
        //        if (CompleteScreening.ListDescriptors[Desc].IsActive() == false) continue;
        //        AxeName += String.Format("{0:0.###}", EigenVectors.getElement(IDxDesc++, 0)) + "x" + CompleteScreening.ListDescriptors[Desc].GetName() + " + ";
        //        //   AxeName += String.Format("{0:0.##}", EigenVectors.getElement(CompleteScreening.ListDescriptors.Count - 1, 0)) + "x" + CompleteScreening.ListDescriptorName[CompleteScreening.ListDescriptors.Count - 1];
        //    }
        //    CurrentChartArea.Axes[0].Title = AxeName.Remove(AxeName.Length - 3);
        //    CurrentChartArea.Axes[0].MajorGrid.Enabled = true;

        //    AxeName = "";
        //    IDxDesc = 0;
        //    for (int Desc = 0; Desc < CompleteScreening.ListDescriptors.Count; Desc++)
        //    {
        //        if (CompleteScreening.ListDescriptors[Desc].IsActive() == false) continue;
        //        AxeName += String.Format("{0:0.###}", EigenVectors.getElement(IDxDesc++, 1)) + "x" + CompleteScreening.ListDescriptors[Desc].GetName() + " + ";
        //    }
        //    //AxeName += String.Format("{0:0.##}", EigenVectors.getElement(CompleteScreening.ListDescriptors.Count - 1, 0)) + "x" + CompleteScreening.ListDescriptorName[CompleteScreening.ListDescriptors.Count - 1];

        //    CurrentChartArea.Axes[1].Title = AxeName.Remove(AxeName.Length - 3);
        //    CurrentChartArea.Axes[1].MajorGrid.Enabled = true;
        //    CurrentChartArea.Axes[1].Minimum = MinY;
        //    CurrentChartArea.Axes[1].Maximum = MaxY;
        //    CurrentChartArea.AxisX.LabelStyle.Format = "N2";
        //    CurrentChartArea.AxisY.LabelStyle.Format = "N2";


        //    CurrentSeries.ChartType = SeriesChartType.Point;
        //    if (GlobalInfo.OptionsWindow.checkBoxDisplayFastPerformance.Checked) CurrentSeries.ChartType = SeriesChartType.FastPoint;
        //    NewWindow.chartForSimpleForm.Series.Add(CurrentSeries);


        //    NewWindow.Text = "PCA";
        //    NewWindow.Show();
        //    NewWindow.chartForSimpleForm.Update();
        //    NewWindow.chartForSimpleForm.Show();
        //    NewWindow.Controls.AddRange(new System.Windows.Forms.Control[] { NewWindow.chartForSimpleForm });


        //}

        private Matrix PCAComputation(double[,] DataForPCA, int NumWellForLearning, int NumDesc, int NeutralClass, cListPlates PlatesToProcess)
        {

            for (int desc = 0; desc < NumDesc; desc++)
            {
                if (cGlobalInfo.CurrentScreening.ListDescriptors[desc].IsActive() == false) continue;
                List<double> CurrentDesc = new List<double>();

                foreach (cPlate CurrentPlate in PlatesToProcess)
                {
                    for (int IdxValue = 0; IdxValue < cGlobalInfo.CurrentScreening.Columns; IdxValue++)
                        for (int IdxValue0 = 0; IdxValue0 < cGlobalInfo.CurrentScreening.Rows; IdxValue0++)
                        {
                            cWell TmpWell = CurrentPlate.GetWell(IdxValue, IdxValue0, true);
                            if ((TmpWell == null) || (TmpWell.GetCurrentClassIdx() != NeutralClass)) continue;
                            CurrentDesc.Add(TmpWell.ListSignatures[desc].GetValue());
                        }
                }
                for (int i = 0; i < NumWellForLearning; i++)
                {
                    DataForPCA[i, desc] = CurrentDesc[i];
                }
            }
            int IDx = 0;

            foreach (cPlate CurrentPlate in PlatesToProcess)
            {
                foreach (cWell CurrentWell in CurrentPlate.ListActiveWells)
                {
                    if (CurrentWell.GetCurrentClassIdx() == NeutralClass)
                        DataForPCA[IDx++, NumDesc] = NeutralClass;// CurrentWell.GetClassIdx();
                }
                // NumWell += CompleteScreening.GetCurrentDisplayPlate().GetNumberOfActiveWells();
            }

            double[,] Basis;
            double[] s2;
            int Info;

            alglib.pcabuildbasis(DataForPCA, NumWellForLearning, NumDesc, out Info, out s2, out Basis);

            Matrix EigenVectors = null;
            if (Info > 0)
            {
                EigenVectors = new Matrix(NumDesc, NumDesc);
                for (int row = 0; row < NumDesc; row++)
                    for (int col = 0; col < NumDesc; col++)
                        EigenVectors.addElement(row, col, Basis[row, col]);
            }
            return EigenVectors;
        }
        #endregion

        #region LDA


        private Matrix LDAComputation(double[,] DataForLDA, int NumWellForLearning, int NumWell, int NumDesc, int NeutralClass, cListPlates PlatesToProcess)
        {
            int Info;
            for (int desc = 0; desc < NumDesc; desc++)
            {
                if (cGlobalInfo.CurrentScreening.ListDescriptors[desc].IsActive() == false) continue;
                List<double> CurrentDesc = new List<double>();

                foreach (cPlate CurrentPlate in PlatesToProcess)
                {
                    for (int IdxValue = 0; IdxValue < cGlobalInfo.CurrentScreening.Columns; IdxValue++)
                        for (int IdxValue0 = 0; IdxValue0 < cGlobalInfo.CurrentScreening.Rows; IdxValue0++)
                        {
                            cWell TmpWell = CurrentPlate.GetWell(IdxValue, IdxValue0, true);
                            if ((TmpWell == null) || (TmpWell.GetCurrentClassIdx() == NeutralClass)) continue;
                            CurrentDesc.Add(TmpWell.ListSignatures[desc].GetValue());
                        }
                }
                for (int i = 0; i < NumWellForLearning; i++)
                {
                    DataForLDA[i, desc] = CurrentDesc[i];
                }
            }
            int IDx = 0;
            foreach (cPlate CurrentPlate in PlatesToProcess)
            {
                for (int IdxValue = 0; IdxValue < cGlobalInfo.CurrentScreening.Columns; IdxValue++)
                    for (int IdxValue0 = 0; IdxValue0 < cGlobalInfo.CurrentScreening.Rows; IdxValue0++)
                    {
                        cWell TmpWell = CurrentPlate.GetWell(IdxValue, IdxValue0, true);
                        if ((TmpWell == null) || (TmpWell.GetCurrentClassIdx() == NeutralClass)) continue;
                        DataForLDA[IDx++, NumDesc] = TmpWell.GetCurrentClassIdx();
                    }
            }
            double[,] Basis;

            //alglib.pcabuildbasis(DataForLDA, NumWellForLearning, NumWellForLearning, out Info, out Basis);
            alglib.fisherldan(DataForLDA, NumWellForLearning, NumDesc, NumWellForLearning, out Info, out Basis);
            Matrix EigenVectors = null;
            if (Info > 0)
            {
                EigenVectors = new Matrix(NumDesc, NumDesc);
                for (int row = 0; row < NumDesc; row++)
                    for (int col = 0; col < NumDesc; col++)
                        EigenVectors.addElement(row, col, Basis[row, col]);
            }
            return EigenVectors;
        }

        private void ComputeAndDisplayLDA(cListPlates PlatesToProcess)
        {

            FormClassification WindowClassification = new FormClassification(cGlobalInfo.CurrentScreening);
            WindowClassification.buttonClassification.Text = "Process";
            WindowClassification.Text = "LDA";
            if (WindowClassification.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            int NeutralClass = WindowClassification.comboBoxForNeutralClass.SelectedIndex;

            int NumWell = 0;
            int NumWellForLearning = 0;
            foreach (cPlate CurrentPlate in PlatesToProcess)
            {
                NumWellForLearning += CurrentPlate.GetNumberOfActiveWellsButClass(NeutralClass);
                NumWell += CurrentPlate.GetNumberOfActiveWells();
            }
            // return;
            if (NumWellForLearning == 0)
            {
                MessageBox.Show("No well identified !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int NumDesc = cGlobalInfo.CurrentScreening.GetNumberOfActiveDescriptor();

            if (NumDesc <= 1)
            {
                MessageBox.Show("More than one descriptor are required for this operation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double[,] DataForLDA = new double[NumWellForLearning, cGlobalInfo.CurrentScreening.GetNumberOfActiveDescriptor() + 1];

            //   return;
            Matrix EigenVectors = LDAComputation(DataForLDA, NumWellForLearning, NumWell, NumDesc, NeutralClass, PlatesToProcess);
            if (EigenVectors == null) return;

            SimpleForm NewWindow = new SimpleForm();
            Series CurrentSeries = new Series();
            CurrentSeries.ShadowOffset = 1;

            Matrix CurrentPt = new Matrix(NumWell, NumDesc);
            DataForLDA = new double[NumWell, NumDesc + 1];

            for (int desc = 0; desc < NumDesc; desc++)
            {
                if (cGlobalInfo.CurrentScreening.ListDescriptors[desc].IsActive() == false) continue;
                List<double> CurrentDesc = new List<double>();
                foreach (cPlate CurrentPlate in PlatesToProcess)
                {
                    for (int IdxValue = 0; IdxValue < cGlobalInfo.CurrentScreening.Columns; IdxValue++)
                        for (int IdxValue0 = 0; IdxValue0 < cGlobalInfo.CurrentScreening.Rows; IdxValue0++)
                        {
                            cWell TmpWell = CurrentPlate.GetWell(IdxValue, IdxValue0, true);
                            if (TmpWell == null) continue;
                            CurrentDesc.Add(TmpWell.ListSignatures[desc].GetValue());
                        }
                }
                for (int i = 0; i < NumWell; i++)
                    DataForLDA[i, desc] = CurrentDesc[i];
            }

            int IDx = 0;
            foreach (cPlate CurrentPlate in PlatesToProcess)
            {
                for (int IdxValue = 0; IdxValue < cGlobalInfo.CurrentScreening.Columns; IdxValue++)
                    for (int IdxValue0 = 0; IdxValue0 < cGlobalInfo.CurrentScreening.Rows; IdxValue0++)
                    {
                        cWell TmpWell = CurrentPlate.GetWell(IdxValue, IdxValue0, true);
                        if (TmpWell == null) continue;
                        DataForLDA[IDx++, NumDesc] = TmpWell.GetCurrentClassIdx();
                    }
            }

            for (int i = 0; i < NumWell; i++)
                for (int j = 0; j < NumDesc; j++) CurrentPt.addElement(i, j, DataForLDA[i, j]);

            Matrix NewPt = new Matrix(NumWell, NumDesc);

            NewPt = CurrentPt.multiply(EigenVectors);

            double MinY = double.MaxValue, MaxY = double.MinValue;

            for (int IdxValue0 = 0; IdxValue0 < NumWell; IdxValue0++)
            {
                double CurrentY = NewPt.getElement(IdxValue0, 1);

                if (CurrentY < MinY) MinY = CurrentY;
                if (CurrentY > MaxY) MaxY = CurrentY;

                CurrentSeries.Points.AddXY(NewPt.getElement(IdxValue0, 0), CurrentY);

                CurrentSeries.Points[IdxValue0].Color = cGlobalInfo.ListWellClasses[(int)DataForLDA[IdxValue0, NumDesc]].ColourForDisplay;
                CurrentSeries.Points[IdxValue0].MarkerStyle = MarkerStyle.Circle;
                CurrentSeries.Points[IdxValue0].MarkerSize = 8;
            }

            ChartArea CurrentChartArea = new ChartArea();
            CurrentChartArea.BorderColor = Color.Black;

            NewWindow.chartForSimpleForm.ChartAreas.Add(CurrentChartArea);
            NewWindow.chartForSimpleForm.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            CurrentChartArea.BackColor = Color.FromArgb(164, 164, 164);

            string AxeName = "";
            int IDxDesc = 0;
            for (int Desc = 0; Desc < cGlobalInfo.CurrentScreening.ListDescriptors.Count; Desc++)
            {
                if (cGlobalInfo.CurrentScreening.ListDescriptors[Desc].IsActive() == false) continue;
                AxeName += String.Format("{0:0.###}", EigenVectors.getElement(IDxDesc++, 0)) + "x" + cGlobalInfo.CurrentScreening.ListDescriptors[Desc].GetName() + " + ";
                //   AxeName += String.Format("{0:0.##}", EigenVectors.getElement(CompleteScreening.ListDescriptors.Count - 1, 0)) + "x" + CompleteScreening.ListDescriptorName[CompleteScreening.ListDescriptors.Count - 1];
            }
            CurrentChartArea.Axes[0].Title = AxeName.Remove(AxeName.Length - 3);
            CurrentChartArea.Axes[0].MajorGrid.Enabled = true;

            AxeName = "";
            IDxDesc = 0;
            for (int Desc = 0; Desc < cGlobalInfo.CurrentScreening.ListDescriptors.Count; Desc++)
            {
                if (cGlobalInfo.CurrentScreening.ListDescriptors[Desc].IsActive() == false) continue;
                AxeName += String.Format("{0:0.###}", EigenVectors.getElement(IDxDesc++, 1)) + "x" + cGlobalInfo.CurrentScreening.ListDescriptors[Desc].GetName() + " + ";
            }
            //AxeName += String.Format("{0:0.##}", EigenVectors.getElement(CompleteScreening.ListDescriptors.Count - 1, 0)) + "x" + CompleteScreening.ListDescriptorName[CompleteScreening.ListDescriptors.Count - 1];

            CurrentChartArea.Axes[1].Title = AxeName.Remove(AxeName.Length - 3);
            CurrentChartArea.Axes[1].MajorGrid.Enabled = true;
            CurrentChartArea.Axes[1].Minimum = MinY;
            CurrentChartArea.Axes[1].Maximum = MaxY;
            CurrentChartArea.AxisX.LabelStyle.Format = "N2";
            CurrentChartArea.AxisY.LabelStyle.Format = "N2";


            CurrentSeries.ChartType = SeriesChartType.Point;

            NewWindow.chartForSimpleForm.Series.Add(CurrentSeries);

            NewWindow.Text = "LDA";
            NewWindow.Show();
            NewWindow.chartForSimpleForm.Update();
            NewWindow.chartForSimpleForm.Show();
            NewWindow.Controls.AddRange(new System.Windows.Forms.Control[] { NewWindow.chartForSimpleForm });


        }
        #endregion

        #region Genes Analysis
        public class cPathWay
        {
            public string Name;
            public int Occurence = 0;

        }

        private void findGeneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            FormForNameRequest FormForRequest = new FormForNameRequest();
            if (FormForRequest.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;

            // loop on all the plate
            for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
            {
                cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(cGlobalInfo.CurrentScreening.ListPlatesActive[PlateIdx].GetName());

                for (int IdxValue = 0; IdxValue < cGlobalInfo.CurrentScreening.Columns; IdxValue++)
                    for (int IdxValue0 = 0; IdxValue0 < cGlobalInfo.CurrentScreening.Rows; IdxValue0++)
                    {
                        cWell TmpWell = CurrentPlateToProcess.GetWell(IdxValue, IdxValue0, true);
                        if (TmpWell == null) continue;

                        if (TmpWell.GetCpdName() == FormForRequest.textBoxForName.Text)
                        {

                            CurrentPlateToProcess.DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
                            int Col = IdxValue + 1;
                            int row = IdxValue0 + 1;
                            MessageBox.Show("Column " + Col + " x Row " + row, TmpWell.GetCpdName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
            }

            MessageBox.Show("Gene not found !", FormForRequest.textBoxForName.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        #region KEGG related
        static public List<string> Find_Pathways(double LocusID)
        {
            string getvars = "/link/pathway/hsa:" + LocusID;
            HttpWebRequest req = WebRequest.Create(string.Format("http://rest.kegg.jp" + getvars)) as HttpWebRequest;
            req.Method = "GET";


            HttpWebResponse response2 = req.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response2.GetResponseStream());



            List<string> Pathways2 = new List<string>();

            // Console application output  
            while (reader.Peek() >= 0)
            {

                string[] resukt = reader.ReadLine().Split('\t');
                if (resukt.Length > 1)
                {
                    Pathways2.Add(resukt[1]);
                }

            }

            reader.Close();
            response2.Close();

            List<string> ListPathway = new List<string>();
            foreach (string item in Pathways2)
                ListPathway.Add(item.Remove(0, 5));

            return ListPathway;
        }

        public string Find_Info(string Path)
        {
            HttpWebRequest req2 = WebRequest.Create(string.Format("http://rest.kegg.jp/get/" + Path)) as HttpWebRequest;
            req2.Method = "GET";

            HttpWebResponse response = req2.GetResponse() as HttpWebResponse;
            StreamReader reader2 = new StreamReader(response.GetResponseStream());

            string GenInfo = reader2.ReadToEnd();
            reader2.Close();

            return GenInfo;
        }

        private FormForPie PathWayAnalysis(int Class)
        {
            //int Idx = 0;
            int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;

            List<cPathWay> ListPathway = new List<cPathWay>();

            // loop on all the plate
            for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
            {
                cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(cGlobalInfo.CurrentScreening.ListPlatesActive[PlateIdx].GetName());

                for (int IdxValue = 0; IdxValue < cGlobalInfo.CurrentScreening.Columns; IdxValue++)
                    for (int IdxValue0 = 0; IdxValue0 < cGlobalInfo.CurrentScreening.Rows; IdxValue0++)
                    {
                        cWell TmpWell = CurrentPlateToProcess.GetWell(IdxValue, IdxValue0, true);


                        object TmpID = TmpWell.ListProperties.FindValueByName("Locus ID");
                        if ((TmpWell == null) || (TmpWell.GetCurrentClassIdx() != Class) || (TmpID == null)) continue;


                        //string[] intersection_gene_pathways = new string[1];
                        //intersection_gene_pathways[0] = "hsa:" + TmpWell.LocusID;
                        //string[] Pathways = ServKegg.get_pathways_by_genes(intersection_gene_pathways);
                        string[] Pathways = Find_Pathways((double)((int)TmpID)).ToArray();

                        if ((Pathways == null) || (Pathways.Length == 0)) continue;

                        for (int Idx = 0; Idx < Pathways.Length; Idx++)
                        {
                            //  string PathName = Pathways[Idx].Remove(0, 8);
                            //string GenInfo = ServKegg.bget(Pathways[Idx]);

                            string GenInfo = Find_Info(Pathways[Idx]);

                            string[] Genes = GenInfo.Split(new char[] { '\n' });
                            string PathName = "";
                            foreach (string item in Genes)
                            {
                                string[] fre = item.Split(' ');
                                string[] STRsection = fre[0].Split('_');

                                if (STRsection[0] == "NAME")
                                {
                                    for (int i = 1; i < fre.Length; i++)
                                    {
                                        if (fre[i] == "") continue;
                                        PathName += fre[i] + " ";
                                    }
                                    break;
                                }
                            }

                            if (ListPathway.Count == 0)
                            {
                                cPathWay CurrPath = new cPathWay();
                                CurrPath.Name = PathName;
                                CurrPath.Occurence = 1;
                                ListPathway.Add(CurrPath);
                                continue;
                            }

                            bool DidIt = false;
                            for (int i = 0; i < ListPathway.Count; i++)
                            {
                                if (PathName == ListPathway[i].Name)
                                {
                                    ListPathway[i].Occurence++;
                                    DidIt = true;
                                    break;
                                }
                            }

                            if (DidIt == false)
                            {
                                cPathWay CurrPath1 = new cPathWay();
                                CurrPath1.Name = PathName;
                                CurrPath1.Occurence = 1;
                                ListPathway.Add(CurrPath1);
                            }
                        }
                    }
            }

            // now draw the pie
            if (ListPathway.Count == 0)
            {
                MessageBox.Show("No pathway identified !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;

            }
            FormForPie Pie = new FormForPie();

            Series CurrentSeries = Pie.chartForPie.Series[0];

            // loop on all the plate
            int MaxOccurence = int.MinValue;
            int MaxIdx = 0;
            int TotalOcurrence = 0;
            for (int Idx = 0; Idx < ListPathway.Count; Idx++)
            {
                if (ListPathway[Idx].Occurence > MaxOccurence)
                {
                    MaxOccurence = ListPathway[Idx].Occurence;
                    MaxIdx = Idx;
                }
                TotalOcurrence += ListPathway[Idx].Occurence;
            }



            //CurrentSeries.CustomProperties = "PieLabelStyle=Outside";
            for (int Idx = 0; Idx < ListPathway.Count; Idx++)
            {
                CurrentSeries.Points.Add(ListPathway[Idx].Occurence);
                CurrentSeries.Points[Idx].Label = String.Format("{0:0.###}", ((100.0 * ListPathway[Idx].Occurence) / TotalOcurrence)) + " %";

                CurrentSeries.Points[Idx].LegendText = ListPathway[Idx].Name;
                CurrentSeries.Points[Idx].ToolTip = ListPathway[Idx].Name;
                if (Idx == MaxIdx)
                    CurrentSeries.Points[Idx].SetCustomProperty("Exploded", "True");
            }

            return Pie;
        }
        #endregion

        //private FormForPie PathWayAnalysis(int Class)
        //{
        //    //int Idx = 0;
        //    int NumberOfPlates = CompleteScreening.ListPlatesActive.Count;

        //    KEGG ServKegg = new KEGG();

        //    List<cPathWay> ListPathway = new List<cPathWay>();

        //    // loop on all the plate
        //    for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
        //    {
        //        cPlate CurrentPlateToProcess = CompleteScreening.ListPlatesActive.GetPlate(CompleteScreening.ListPlatesActive[PlateIdx].Name);

        //        for (int IdxValue = 0; IdxValue < CompleteScreening.Columns; IdxValue++)
        //            for (int IdxValue0 = 0; IdxValue0 < CompleteScreening.Rows; IdxValue0++)
        //            {
        //                cWell TmpWell = CurrentPlateToProcess.GetWell(IdxValue, IdxValue0, true);
        //                if ((TmpWell == null) || (TmpWell.GetClassIdx() != Class) || (TmpWell.LocusID == -1)) continue;


        //                string[] intersection_gene_pathways = new string[1];
        //                intersection_gene_pathways[0] = "hsa:" + TmpWell.LocusID;
        //                string[] Pathways = ServKegg.get_pathways_by_genes(intersection_gene_pathways);
        //                if ((Pathways == null) || (Pathways.Length == 0)) continue;

        //                for (int Idx = 0; Idx < Pathways.Length; Idx++)
        //                {
        //                    //  string PathName = Pathways[Idx].Remove(0, 8);
        //                    string GenInfo = ServKegg.bget(Pathways[Idx]);
        //                    string[] Genes = GenInfo.Split(new char[] { '\n' });
        //                    string PathName = "";
        //                    foreach (string item in Genes)
        //                    {
        //                        string[] fre = item.Split(' ');
        //                        string[] STRsection = fre[0].Split('_');

        //                        if (STRsection[0] == "NAME")
        //                        {
        //                            for (int i = 1; i < fre.Length; i++)
        //                            {
        //                                if (fre[i] == "") continue;
        //                                PathName += fre[i] + " ";
        //                            }
        //                            break;
        //                        }
        //                    }

        //                    if (ListPathway.Count == 0)
        //                    {
        //                        cPathWay CurrPath = new cPathWay();
        //                        CurrPath.Name = PathName;
        //                        CurrPath.Occurence = 1;
        //                        ListPathway.Add(CurrPath);
        //                        continue;
        //                    }

        //                    bool DidIt = false;
        //                    for (int i = 0; i < ListPathway.Count; i++)
        //                    {
        //                        if (PathName == ListPathway[i].Name)
        //                        {
        //                            ListPathway[i].Occurence++;
        //                            DidIt = true;
        //                            break;
        //                        }
        //                    }

        //                    if (DidIt == false)
        //                    {
        //                        cPathWay CurrPath1 = new cPathWay();
        //                        CurrPath1.Name = PathName;
        //                        CurrPath1.Occurence = 1;
        //                        ListPathway.Add(CurrPath1);
        //                    }
        //                }
        //            }
        //    }

        //    // now draw the pie
        //    if (ListPathway.Count == 0)
        //    {
        //        MessageBox.Show("No pathway identified !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return null;

        //    }
        //    FormForPie Pie = new FormForPie();

        //    Series CurrentSeries = Pie.chartForPie.Series[0];

        //    // loop on all the plate
        //    int MaxOccurence = int.MinValue;
        //    int MaxIdx = 0;
        //    int TotalOcurrence = 0;
        //    for (int Idx = 0; Idx < ListPathway.Count; Idx++)
        //    {
        //        if (ListPathway[Idx].Occurence > MaxOccurence)
        //        {
        //            MaxOccurence = ListPathway[Idx].Occurence;
        //            MaxIdx = Idx;
        //        }
        //        TotalOcurrence += ListPathway[Idx].Occurence;
        //    }



        //    //CurrentSeries.CustomProperties = "PieLabelStyle=Outside";
        //    for (int Idx = 0; Idx < ListPathway.Count; Idx++)
        //    {
        //        CurrentSeries.Points.Add(ListPathway[Idx].Occurence);
        //        CurrentSeries.Points[Idx].Label = String.Format("{0:0.###}", ((100.0 * ListPathway[Idx].Occurence) / TotalOcurrence)) + " %";

        //        CurrentSeries.Points[Idx].LegendText = ListPathway[Idx].Name;
        //        CurrentSeries.Points[Idx].ToolTip = ListPathway[Idx].Name;
        //        if (Idx == MaxIdx)
        //            CurrentSeries.Points[Idx].SetCustomProperty("Exploded", "True");
        //    }

        //    return Pie;
        //}

        private void pahtwaysAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            this.Cursor = Cursors.WaitCursor;
            FormForPie CurrentFormForPie = PathWayAnalysis(cGlobalInfo.CurrentScreening.SelectedClass);
            this.Cursor = Cursors.Default;
            if (CurrentFormForPie != null) CurrentFormForPie.Show();
        }
        #endregion

        #region Systematic Error Identification

        private void ComputeSystematicErrorsTable()
        {
            cListPlates LP = new cListPlates();

            if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
                LP.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());
            else
                LP = cGlobalInfo.CurrentScreening.ListPlatesActive;

            cSystematicErrorAnalyzer SEA = new cSystematicErrorAnalyzer();
            SEA.PlatesToProcess = LP;
            SEA.DescriptorsToProcess = cGlobalInfo.CurrentScreening.GetActiveDescriptors();
            SEA.Run(true);
        }

        #endregion

        #region Compute and display Correlation matrix
        private void correlationMatrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComputeAndDisplayCorrelationMatrix(true, true, null);
        }


        private void ComputeAndDisplayCorrelationMatrix(bool IsFullScreen, bool IsToBeDisplayed, string PathForImage)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            if (checkedListBoxActiveDescriptors.CheckedItems.Count <= 1)
            {
                MessageBox.Show("At least two descriptors have to be selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool IsDisplayRanking = cGlobalInfo.OptionsWindow.checkBoxCorrelationMatrixDisplayRanking.Checked;
            //bool IsPearson = CompleteScreening.GlobalInfo.OptionsWindow.radioButtonPearson.Checked;
            Boolean IsDisplayValues = false;


            List<double>[] ListValueDesc = ExtractDesciptorAverageValuesList(IsFullScreen);
            double[,] CorrelationMatrix = ComputeCorrelationMatrix(ListValueDesc);

            if (CorrelationMatrix == null)
            {
                MessageBox.Show("Data error, correlation computation impossible !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //   return;
            List<string> NameX = cGlobalInfo.CurrentScreening.ListDescriptors.GetListNameActives();
            List<string> NameY = cGlobalInfo.CurrentScreening.ListDescriptors.GetListNameActives();

            string TitleForGraph = "";
            if (cGlobalInfo.OptionsWindow.radioButtonPearson.Checked) TitleForGraph = "Pearson ";
            else if
                (cGlobalInfo.OptionsWindow.radioButtonSpearman.Checked) TitleForGraph = "Spearman's ";
            else if
                (cGlobalInfo.OptionsWindow.radioButtonMIC.Checked) TitleForGraph = "MIC's ";
            TitleForGraph += " correlation matrix.";

            int SquareSize;

            if (NameX.Count > 20)
                SquareSize = 5;
            else
                SquareSize = 100 - ((10 * NameX.Count) / 3);
            DisplayMatrix(CorrelationMatrix, NameX, NameY, IsDisplayValues, TitleForGraph, SquareSize, IsToBeDisplayed, PathForImage);
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (IsDisplayRanking == false) return;
            else
                DisplayCorrelationRanking(ListValueDesc, CorrelationMatrix);
        }


        public void DisplayCorrelationRanking(List<double>[] ListValueDesc, double[,] CorrelationMatrix)
        {

            string TitleForGraph;
            Series CurrentSeries1 = new Series("Data1");
            CurrentSeries1.ShadowOffset = 1;
            CurrentSeries1.ChartType = SeriesChartType.Column;


            int RealPosSelectedDesc = -1;

            int realPos = 0;
            for (int i = 0; i < cGlobalInfo.CurrentScreening.ListDescriptors.Count; i++)
            {
                if (cGlobalInfo.CurrentScreening.ListDescriptors[i].IsActive()) realPos++;
                if (i == cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx)
                {
                    RealPosSelectedDesc = i - 2;
                    break;
                }
            }

            // loop on all the desciptors
            int IdxValue = 0;
            for (int iDesc = 0; iDesc < ListValueDesc.Length; iDesc++)
                for (int jDesc = 0; jDesc < ListValueDesc.Length; jDesc++)
                {
                    if (iDesc <= jDesc) continue;
                    CurrentSeries1.Points.Add(Math.Abs(CorrelationMatrix[iDesc, jDesc]));

                    if (cGlobalInfo.OptionsWindow.checkBoxCorrelationRankChangeColorForActiveDesc.Checked)
                    {
                        if (cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx < cGlobalInfo.CurrentScreening.ListDescriptors.GetListNameActives().Count)
                        {
                            if ((cGlobalInfo.CurrentScreening.ListDescriptors.GetListNameActives()[iDesc] == cGlobalInfo.CurrentScreening.ListDescriptors.GetListNameActives()[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx]) ||
                                (cGlobalInfo.CurrentScreening.ListDescriptors.GetListNameActives()[jDesc] == cGlobalInfo.CurrentScreening.ListDescriptors.GetListNameActives()[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx]))
                                CurrentSeries1.Points[IdxValue].Color = Color.LightGreen;
                        }
                    }
                    CurrentSeries1.Points[IdxValue].Label = string.Format("{0:0.###}", CorrelationMatrix[iDesc, jDesc]);
                    CurrentSeries1.Points[IdxValue].ToolTip = cGlobalInfo.CurrentScreening.ListDescriptors.GetListNameActives()[iDesc] + "\n vs. \n" + cGlobalInfo.CurrentScreening.ListDescriptors.GetListNameActives()[jDesc];
                    CurrentSeries1.Points[IdxValue].AxisLabel = CurrentSeries1.Points[IdxValue++].ToolTip;
                }

            SimpleForm NewWindow1 = new SimpleForm();
            int thisWidth = CurrentSeries1.Points.Count * 100 + 200;
            NewWindow1.Width = thisWidth;
            //NewWindow1.Width = CurrentSeries1.Points.Count * 100 + 200;

            ChartArea CurrentChartArea1 = new ChartArea("Default1");
            CurrentChartArea1.BackColor = Color.White;
            CurrentChartArea1.BorderColor = Color.Black;

            NewWindow1.chartForSimpleForm.ChartAreas.Add(CurrentChartArea1);
            CurrentSeries1.SmartLabelStyle.Enabled = true;
            CurrentChartArea1.AxisY.Title = "Absolute Correlation Coeff.";

            NewWindow1.chartForSimpleForm.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            NewWindow1.chartForSimpleForm.Series.Add(CurrentSeries1);

            CurrentChartArea1.Axes[0].MajorGrid.Enabled = false;
            CurrentChartArea1.Axes[1].MajorGrid.Enabled = false;
            CurrentChartArea1.Axes[1].Minimum = 0;
            CurrentChartArea1.Axes[1].Maximum = 1.2;

            CurrentChartArea1.AxisX.Interval = 1;
            NewWindow1.chartForSimpleForm.Series[0].Sort(PointSortOrder.Ascending, "Y");

            if (cGlobalInfo.OptionsWindow.radioButtonPearson.Checked) TitleForGraph = "Pearson's ";
            else if (cGlobalInfo.OptionsWindow.radioButtonPearson.Checked) TitleForGraph = "Spearman's ";
            else TitleForGraph = "MIC's ";

            TitleForGraph += "correlation ranking";

            Title CurrentTitle1 = new Title(TitleForGraph);

            /* if (IsToBeDisplayed) */
            NewWindow1.Show();
            //else
            //    NewWindow1.chartForSimpleForm.SaveImage(PathForImage + "_Ranking.png", ChartImageFormat.Png);

            NewWindow1.chartForSimpleForm.Titles.Add(CurrentTitle1);
            NewWindow1.Text = "Quality Control: Corr. ranking";
            NewWindow1.chartForSimpleForm.Update();
            NewWindow1.Controls.AddRange(new System.Windows.Forms.Control[] { NewWindow1.chartForSimpleForm });

            return;
        }



        private double[,] ComputeCorrelationMatrix(List<double>[] ListValueDesc)
        {
            int NumDesc = ListValueDesc.Length;
            double[,] CorrelationMatrix = new double[NumDesc, NumDesc];

            if (cGlobalInfo.OptionsWindow.radioButtonMIC.Checked)
            {
                double[][] dataset1 = new double[NumDesc][];
                string[] VarNames = new string[NumDesc];


                for (int iDesc = 0; iDesc < NumDesc; iDesc++)
                {
                    dataset1[iDesc] = new double[ListValueDesc[iDesc].Count];

                    Array.Copy(ListValueDesc[iDesc].ToArray(), dataset1[iDesc], ListValueDesc[iDesc].Count);
                    VarNames[iDesc] = iDesc.ToString();
                }
                data.Dataset data1 = new data.Dataset(dataset1, VarNames, 0);
                VarPairQueue Qu = new VarPairQueue(data1);


                for (int iDesc = 0; iDesc < NumDesc; iDesc++)
                    for (int jDesc = 0; jDesc < NumDesc; jDesc++)
                    {
                        Qu.addPair(iDesc, jDesc);
                    }


                Analysis ana = new Analysis(data1, Qu);
                AnalysisParameters param = new AnalysisParameters();
                double resparam = param.commonValsThreshold;

                analysis.results.FullResult Full = new analysis.results.FullResult();
                //List<analysis.results.BriefResult> Brief = new List<analysis.results.BriefResult>();
                analysis.results.BriefResult Brief = new analysis.results.BriefResult();

                java.lang.Class t = java.lang.Class.forName("analysis.results.BriefResult");

                //java.lang.Class restype = null;
                ana.analyzePairs(t, param);

                //   object o =  (ana.varPairQueue().peek());
                //   ana.getClass();
                //  int resNum = ana.numResults();
                analysis.results.Result[] res = ana.getSortedResults();
                //  double main = res[0].getMainScore();
                for (int iDesc = 0; iDesc < NumDesc; iDesc++)
                    for (int jDesc = 0; jDesc < NumDesc; jDesc++)
                    {
                        int X = int.Parse(res[jDesc + iDesc * NumDesc].getXVar());
                        int Y = int.Parse(res[jDesc + iDesc * NumDesc].getYVar());
                        CorrelationMatrix[X, Y] = res[jDesc + iDesc * NumDesc].getMainScore();

                    }
            }
            else
            {

                //return null;
                for (int iDesc = 0; iDesc < NumDesc; iDesc++)
                    for (int jDesc = 0; jDesc < NumDesc; jDesc++)
                    {
                        try
                        {
                            if (cGlobalInfo.OptionsWindow.radioButtonPearson.Checked)
                                CorrelationMatrix[iDesc, jDesc] = (alglib.pearsoncorr2(ListValueDesc[iDesc].ToArray(), ListValueDesc[jDesc].ToArray()));
                            else if (cGlobalInfo.OptionsWindow.radioButtonSpearman.Checked)
                                CorrelationMatrix[iDesc, jDesc] = (alglib.spearmancorr2(ListValueDesc[iDesc].ToArray(), ListValueDesc[jDesc].ToArray()));

                        }
                        catch
                        {
                            //Console.WriteLine("Input string is not a sequence of digits.");
                            return null;
                        }

                    }
            }
            return CorrelationMatrix;
        }

        private List<double>[] ExtractDesciptorAverageValuesList(bool IsFullScreen)
        {
            int NumDesc = cGlobalInfo.CurrentScreening.ListDescriptors.GetListNameActives().Count;
            int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;
            List<double>[] ListValueDesc = new List<double>[NumDesc];

            for (int i = 0; i < NumDesc; i++) ListValueDesc[i] = new List<double>();

            List<cPlate> PlatesToProcess = new List<cPlate>();
            if (IsFullScreen)
            {
                for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
                {
                    cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(cGlobalInfo.CurrentScreening.ListPlatesActive[PlateIdx].GetName());
                    PlatesToProcess.Add(CurrentPlateToProcess);
                }
            }
            else
            {
                PlatesToProcess.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());
            }

            int ActiveDesc;

            // loop on all the plate
            for (int PlateIdx = 0; PlateIdx < PlatesToProcess.Count; PlateIdx++)
            {
                cPlate CurrentPlateToProcess = PlatesToProcess[PlateIdx];

                int NumActiveWells = CurrentPlateToProcess.GetNumberOfActiveWells();

                for (int Col = 0; Col < cGlobalInfo.CurrentScreening.Columns; Col++)
                    for (int Row = 0; Row < cGlobalInfo.CurrentScreening.Rows; Row++)
                    {
                        cWell TmpWell = CurrentPlateToProcess.GetWell(Col, Row, true);
                        if (TmpWell == null) continue;
                        ActiveDesc = 0;
                        for (int Desc = 0; Desc < cGlobalInfo.CurrentScreening.ListDescriptors.Count; Desc++)
                        {
                            if (cGlobalInfo.CurrentScreening.ListDescriptors[Desc].IsActive() == false) continue;
                            ListValueDesc[ActiveDesc++].Add(TmpWell.ListSignatures[Desc].GetValue());
                        }
                    }
            }
            return ListValueDesc;
        }

        private void DisplayMatrix(double[,] Matrix, List<string> ListLabelX, List<string> ListLabelY, bool IsDisplayValues, string TitleForGraph, int SquareSize, bool IsToBeDisplayed, string PathName)
        {
            int IdxValue = 0;

            Series CurrentSeries = new Series("Matrix");
            CurrentSeries.ChartType = SeriesChartType.Point;
            // loop on all the desciptors
            for (int iDesc = 0; iDesc < ListLabelX.Count; iDesc++)
            {
                for (int jDesc = 0; jDesc < ListLabelY.Count; jDesc++)
                {
                    CurrentSeries.Points.AddXY(iDesc + 1, jDesc + 1);
                    CurrentSeries.Points[IdxValue].MarkerStyle = MarkerStyle.Square;
                    CurrentSeries.Points[IdxValue].MarkerSize = SquareSize;
                    CurrentSeries.Points[IdxValue].BorderColor = Color.Black;
                    CurrentSeries.Points[IdxValue].BorderWidth = 1;
                    double Value = Matrix[iDesc, jDesc];

                    if (IsDisplayValues) CurrentSeries.Points[IdxValue].Label = string.Format("{0:0.###}", Math.Abs(Value));

                    CurrentSeries.Points[IdxValue].ToolTip = Math.Abs(Value) + " <=> | " + Matrix[iDesc, jDesc].ToString() + " |";

                    int ConvertedValue = (int)(Math.Abs(Value) * (cGlobalInfo.CurrentPlateLUT[0].Length - 1));

                    CurrentSeries.Points[IdxValue++].Color = Color.FromArgb(cGlobalInfo.CurrentPlateLUT[0][ConvertedValue], cGlobalInfo.CurrentPlateLUT[1][ConvertedValue], cGlobalInfo.CurrentPlateLUT[2][ConvertedValue]);
                }
            }

            for (int iDesc = 0; iDesc < ListLabelX.Count * ListLabelX.Count; iDesc++)
                CurrentSeries.Points[iDesc].AxisLabel = cGlobalInfo.CurrentScreening.ListDescriptors.GetListNameActives()[iDesc / ListLabelX.Count];

            SmartLabelStyle SStyle = new SmartLabelStyle();

            SimpleForm NewWindow = new SimpleForm();
            NewWindow.Height = SquareSize * ListLabelY.Count + 220;
            NewWindow.Width = SquareSize * ListLabelX.Count + 245;

            ChartArea CurrentChartArea = new ChartArea("Default");
            for (int i = 0; i < cGlobalInfo.CurrentScreening.ListDescriptors.GetListNameActives().Count; i++)
            {
                CustomLabel lblY = new CustomLabel();
                lblY.ToPosition = i * 2 + 2;
                lblY.Text = ListLabelY[i];
                CurrentChartArea.AxisY.CustomLabels.Add(lblY);
            }

            CurrentChartArea.AxisY.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep30;
            CurrentChartArea.BorderColor = Color.Black;
            NewWindow.chartForSimpleForm.ChartAreas.Add(CurrentChartArea);
            CurrentSeries.SmartLabelStyle.Enabled = true;

            NewWindow.chartForSimpleForm.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            NewWindow.chartForSimpleForm.Series.Add(CurrentSeries);

            CurrentChartArea.Axes[0].MajorGrid.Enabled = false;
            CurrentChartArea.Axes[0].Minimum = 0;
            CurrentChartArea.Axes[0].Maximum = ListLabelX.Count + 1;
            CurrentChartArea.Axes[1].MajorGrid.Enabled = false;
            CurrentChartArea.Axes[1].Minimum = 0;
            CurrentChartArea.Axes[1].Maximum = ListLabelY.Count + 1;
            CurrentChartArea.AxisX.Interval = 1;
            CurrentChartArea.AxisY.Interval = 1;

            Title CurrentTitle = new Title(TitleForGraph);
            NewWindow.chartForSimpleForm.Titles.Add(CurrentTitle);
            NewWindow.chartForSimpleForm.Titles[0].Font = new Font("Arial", 9);

            if (IsToBeDisplayed) NewWindow.Show();
            else
                NewWindow.chartForSimpleForm.SaveImage(PathName + "_Matrix.emf", ChartImageFormat.Emf);
            NewWindow.Text = TitleForGraph;
            NewWindow.chartForSimpleForm.Update();
            NewWindow.chartForSimpleForm.Show();
            NewWindow.Controls.AddRange(new System.Windows.Forms.Control[] { NewWindow.chartForSimpleForm });
        }



        #endregion

        #region Edit
        private void copyAverageValuesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().CopyValuestoClipBoard();
        }

        private void copyPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            cGUI_ListWellProperty GUI_ListWellProperty = new cGUI_ListWellProperty();
            GUI_ListWellProperty.IsCheckBoxes = false;
            if (GUI_ListWellProperty.Run().IsSucceed == false) return;
            List<cPropertyType> ListSelectedProp = GUI_ListWellProperty.GetOutPut();



            cListPlates LP = new cListPlates();

            if (ProcessModeplateByPlateToolStripMenuItem.Checked)
            {
                cGUI_ListPlates GUI_ListPlates = new cGUI_ListPlates();
                GUI_ListPlates.IsCheckBoxes = true;
                cFeedBackMessage FBM = GUI_ListPlates.Run();
                cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(FBM.Message);
                if (!FBM.IsSucceed) return;

                LP = GUI_ListPlates.GetOutPut();
            }
            else if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
            {
                LP.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());
            }
            else
            {
                LP = cGlobalInfo.CurrentScreening.ListPlatesActive;
            }

            cGlobalInfo.CurrentScreening.CopyPropertyToClipBoard(ListSelectedProp, LP);

        }
        #endregion

        public string GenerateLDADescriptor(cListPlates PlatesToProcess, int NeutralClass)
        {

            int NumWell = 0;
            int NumWellForLearning = 0;
            foreach (cPlate CurrentPlate in PlatesToProcess)
            {
                NumWellForLearning += CurrentPlate.GetNumberOfActiveWellsButClass(NeutralClass);
                NumWell += cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetNumberOfActiveWells();
            }

            if (NumWellForLearning == 0)
            {
                MessageBox.Show("No well identified !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            int NumDesc = cGlobalInfo.CurrentScreening.GetNumberOfActiveDescriptor();

            if (NumDesc <= 1)
            {
                MessageBox.Show("More than one descriptor are required for this operation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            double[,] DataForLDA = new double[NumWellForLearning, cGlobalInfo.CurrentScreening.GetNumberOfActiveDescriptor() + 1];

            //   return;
            Matrix EigenVectors = LDAComputation(DataForLDA, NumWellForLearning, NumWell, NumDesc, NeutralClass, PlatesToProcess);


            string AxeName = "";
            int IDxDesc = 0;
            //for (int Desc = 0; Desc < CompleteScreening.ListDescriptors.Count; Desc++)
            //{
            //    if (CompleteScreening.ListDescriptors[Desc].IsActive() == false) continue;

            //    //   AxeName += String.Format("{0:0.##}", EigenVectors.getElement(CompleteScreening.ListDescriptors.Count - 1, 0)) + "x" + CompleteScreening.ListDescriptorName[CompleteScreening.ListDescriptors.Count - 1];
            //}

            for (int Idx = 0; Idx < cGlobalInfo.WindowHCSAnalyzer.checkedListBoxActiveDescriptors.Items.Count; Idx++)
            {

                if (cGlobalInfo.CurrentScreening.ListDescriptors[Idx].IsActive())
                    if (cGlobalInfo.CurrentScreening.ListDescriptors[Idx].GetBinNumber() == 1)
                    {
                        AxeName += String.Format("{0:0.###}", EigenVectors.getElement(IDxDesc++, 0)) + "x" + cGlobalInfo.CurrentScreening.ListDescriptors[Idx].GetName() + " + ";
                    }
                    else
                    {
                        MessageBox.Show("Descriptor length not consistent (" + cGlobalInfo.CurrentScreening.ListDescriptors[Idx].GetName() + " : " + cGlobalInfo.CurrentScreening.ListDescriptors[Idx].GetBinNumber() + " bins", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
            }


            cDescriptorType ColumnType = new cDescriptorType(AxeName.Remove(AxeName.Length - 3), true, 1);

            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(ColumnType);



            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                {
                    cListSignature LDesc = new cListSignature();

                    double NewValue = 0;
                    IDxDesc = 0;
                    for (int Idx = 0; Idx < cGlobalInfo.WindowHCSAnalyzer.checkedListBoxActiveDescriptors.Items.Count - 1; Idx++)
                    {
                        if (cGlobalInfo.CurrentScreening.ListDescriptors[Idx].IsActive())
                            // AxeName += String.Format("{0:0.###}", EigenVectors.getElement(IDxDesc++, 0)) + "x" + CompleteScreening.ListDescriptors[Idx].GetName() + " + ";
                            NewValue += EigenVectors.getElement(IDxDesc++, 0) * Tmpwell.ListSignatures[Idx].GetValue();
                    }

                    cSignature NewDesc = new cSignature(NewValue, ColumnType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);
                    Tmpwell.AddSignatures(LDesc);
                }
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();
            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();

            StartingUpDateUI();

            return AxeName;

        }

        public string GeneratePCADescriptor(cListPlates PlatesToProcess, int NumberOfAxis, int NeutralClass)
        {
            int NumWell = 0;
            int NumWellForLearning = 0;
            foreach (cPlate CurrentPlate in PlatesToProcess)
            {
                NumWellForLearning += CurrentPlate.GetNumberOfWellOfClass(NeutralClass);
                NumWell += cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetNumberOfActiveWells();
            }

            if (NumWellForLearning == 0)
            {
                MessageBox.Show("No well identified !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int NumDesc = cGlobalInfo.CurrentScreening.GetNumberOfActiveDescriptor();

            if (NumDesc <= 1)
            {
                MessageBox.Show("More than one descriptor are required for this operation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            double[,] DataForLDA = new double[NumWellForLearning, cGlobalInfo.CurrentScreening.GetNumberOfActiveDescriptor() + 1];

            //   return;
            Matrix EigenVectors = PCAComputation(DataForLDA, NumWellForLearning, NumDesc, NeutralClass, PlatesToProcess);


            string AxeName = "";
            int IDxDesc = 0;
            //for (int Desc = 0; Desc < CompleteScreening.ListDescriptors.Count; Desc++)
            //{
            //    if (CompleteScreening.ListDescriptors[Desc].IsActive() == false) continue;

            //       AxeName += String.Format("{0:0.##}", EigenVectors.getElement(CompleteScreening.ListDescriptors.Count - 1, 0)) + "x" + CompleteScreening.ListDescriptorName[CompleteScreening.ListDescriptors.Count - 1];
            //}

            int OriginalDescNumber = cGlobalInfo.WindowHCSAnalyzer.checkedListBoxActiveDescriptors.Items.Count;


            for (int AxesIdx = 0; AxesIdx < NumberOfAxis; AxesIdx++)
            {

                //for (int Idx = 0; Idx < CompleteScreening.GlobalInfo.WindowHCSAnalyzer.checkedListBoxActiveDescriptors.Items.Count; Idx++)
                //{

                //    if (CompleteScreening.ListDescriptors[Idx].IsActive())
                //        if (CompleteScreening.ListDescriptors[Idx].GetBinNumber() == 1)
                //        {
                //            AxeName += String.Format("{0:0.###}", EigenVectors.getElement(IDxDesc++, AxesIdx)) + "x" + CompleteScreening.ListDescriptors[Idx].GetName() + " + ";
                //        }
                //        else
                //        {
                //            MessageBox.Show("Descriptor length not consistent (" + CompleteScreening.ListDescriptors[Idx].GetName() + " : " + CompleteScreening.ListDescriptors[Idx].GetBinNumber() + " bins", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            return "";
                //        }
                //}

                //cDescriptorsType ColumnType = new cDescriptorsType(AxeName.Remove(AxeName.Length - 3), true, 1);

                cDescriptorType ColumnType = new cDescriptorType("PCA_" + (AxesIdx + 1), true, 1);

                while (cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(ColumnType) == false)
                {
                    FormForNewDescName NewNameWindow = new FormForNewDescName();
                    NewNameWindow.textBoxName.Text = ColumnType.GetName();

                    if (NewNameWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        return ColumnType.GetName();

                    ColumnType.ChangeName(NewNameWindow.textBoxName.Text);
                }
                //CompleteScreening.ListDescriptors.AddNew(ColumnType);

                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
                {
                    foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                    {
                        cListSignature LDesc = new cListSignature();

                        double NewValue = 0;
                        IDxDesc = 0;

                        //    AxeName += "\nPCA_" + (AxesIdx + 1);
                        for (int Idx = 0; Idx < OriginalDescNumber - 1; Idx++)
                        {
                            if (cGlobalInfo.CurrentScreening.ListDescriptors[Idx].IsActive())
                                // AxeName += String.Format("{0:0.###}", EigenVectors.getElement(IDxDesc, AxesIdx)) + "x" + CompleteScreening.ListDescriptors[Idx].GetName() + " + ";
                                NewValue += EigenVectors.getElement(IDxDesc++, AxesIdx) * Tmpwell.ListSignatures[Idx].GetValue();
                        }

                        cSignature NewDesc = new cSignature(NewValue, ColumnType, cGlobalInfo.CurrentScreening);
                        LDesc.Add(NewDesc);
                        Tmpwell.AddSignatures(LDesc);
                    }
                }
            }
            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();
            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();

            StartingUpDateUI();

            return AxeName;
        }

    }
}
