﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using weka.core;
using System.Data;
using weka.clusterers;
using LibPlateAnalysis;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Windows.Forms;
using weka.classifiers;
using HCSAnalyzer.Classes;
using weka.core.neighboursearch;
using HCSAnalyzer.Forms.ClusteringForms;
using HCSAnalyzer.Forms.IO;

namespace HCSAnalyzer
{
    public partial class HCSAnalyzer
    {
        #region User Interface
        //private void comboBoxClusteringMethod_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    richTextBoxInfoClustering.Clear();

        //    switch (comboBoxClusteringMethod.SelectedIndex)
        //    {
        //        case 0:
        //            richTextBoxInfoClustering.AppendText("K-Means.\nFor more information, go to: http://en.wikipedia.org/wiki/K-means_clustering");
        //            checkBoxAutomatedClusterNumber.Checked = false;
        //            checkBoxAutomatedClusterNumber.Enabled = false;
        //            numericUpDownClusterNumber.ReadOnly = false;
        //            break;
        //        case 1:
        //            richTextBoxInfoClustering.AppendText("EM.\nFor more information, go to: http://en.wikipedia.org/wiki/Expectation_maximization");
        //            checkBoxAutomatedClusterNumber.Enabled = true;
        //            break;
        //        case 2:
        //            richTextBoxInfoClustering.AppendText("Hierarchical Clusterer.\nNote: well suited for large signatures, but computationaly heavy regarding the number of experiments.\nFor more information, go to: http://en.wikipedia.org/wiki/Hierarchical_clustering");
        //            checkBoxAutomatedClusterNumber.Checked = false;
        //            checkBoxAutomatedClusterNumber.Enabled = false;
        //            numericUpDownClusterNumber.ReadOnly = false;
        //            break;

        //    }

        //}

        //private void richTextBoxInfoClustering_LinkClicked(object sender, LinkClickedEventArgs e)
        //{
        //    ClickOnLink(e.LinkText);

        //}

        //private void buttonCluster_Click(object sender, EventArgs e)
        //{
        //    if (CompleteScreening == null) return;

        //    if (!CompleteScreening.IsSelectedDescriptors())
        //    {
        //        MessageBox.Show("You have to check at least one descriptor !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    this.Cursor = Cursors.WaitCursor;
        //    // -------------- K - Means ---------------------------
        //    if (comboBoxClusteringMethod.SelectedIndex == 0)
        //    {
        //        if (radioButtonClusterPlateByPlate.Checked)
        //        {
        //            int NumberOfPlates = CompleteScreening.ListPlatesActive.Count;
        //            // loop on all the plate
        //            for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
        //            {
        //                cPlate CurrentPlateToProcess = CompleteScreening.ListPlatesActive.GetPlate(CompleteScreening.ListPlatesActive[PlateIdx].Name);
        //                KMeans((int)numericUpDownClusterNumber.Value, CurrentPlateToProcess);
        //            }
        //            richTextBoxInfoClustering.AppendText("\nPlate by plate clustering done !");
        //        }
        //        else
        //        {
        //            KMeansFullScreen((int)numericUpDownClusterNumber.Value);
        //            richTextBoxInfoClustering.AppendText("\nGlobal clustering done !");
        //        }
        //    }
        //    else if (comboBoxClusteringMethod.SelectedIndex == 1)   // ---------------- EM --------------------------
        //    {
        //        FormForEMInfo WindowEMinfo = new FormForEMInfo();

        //        if (WindowEMinfo.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

        //        if (checkBoxAutomatedClusterNumber.Checked)
        //        {
        //            string feedback = ClusteringEM(radioButtonClusterFullScreen.Checked, -1, WindowEMinfo);
        //            if (feedback != null)
        //            {
        //                FormForCellByCellClusteringResults WindowFormForCellByCellClusteringResults = new FormForCellByCellClusteringResults();
        //                WindowFormForCellByCellClusteringResults.richTextBoxResults.Clear();
        //                WindowFormForCellByCellClusteringResults.richTextBoxResults.AppendText(feedback);
        //                WindowFormForCellByCellClusteringResults.buttonPerformLearning.Text = "Ok";
        //                WindowFormForCellByCellClusteringResults.ShowDialog();
        //            }
        //        }
        //        else
        //        {
        //            string feedback = ClusteringEM(radioButtonClusterFullScreen.Checked, (int)numericUpDownClusterNumber.Value, WindowEMinfo);
        //            if (feedback != null)
        //            {
        //                FormForCellByCellClusteringResults WindowFormForCellByCellClusteringResults = new FormForCellByCellClusteringResults();
        //                WindowFormForCellByCellClusteringResults.richTextBoxResults.Clear();
        //                WindowFormForCellByCellClusteringResults.richTextBoxResults.AppendText(feedback);
        //                WindowFormForCellByCellClusteringResults.buttonPerformLearning.Text = "Ok";
        //                WindowFormForCellByCellClusteringResults.ShowDialog();
        //            }
        //        }
        //    }
        //    else if (comboBoxClusteringMethod.SelectedIndex == 2)   // ---------------- Hierarchical --------------------------
        //    {
        //        if (checkBoxAutomatedClusterNumber.Checked)
        //            ClusteringHierarchical(radioButtonClusterFullScreen.Checked, -1);
        //        else
        //            ClusteringHierarchical(radioButtonClusterFullScreen.Checked, (int)numericUpDownClusterNumber.Value);

        //    }

        //    //CompleteScreening.GetPlate(CompleteScreening.CurrentDisplayPlate).DisplayClasses(CompleteScreening.PanelForPlate);
        //    //   tabControlMain.SelectedTab = tabPageDistribution;
        //    this.Cursor = Cursors.Default;


        //    CompleteScreening.GetCurrentDisplayPlate().DisplayDistribution(CompleteScreening.ListDescriptors.CurrentSelectedDescriptor, false);
        //}

        //private void radioButtonClusterPlateByPlate_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (radioButtonClusterPlateByPlate.Checked)
        //        richTextBoxInfoClustering.AppendText("\nWarning: in such mode the results can be inconsistent from one plate to another.");
        //}

        //private void checkBoxAutomatedClusterNumber_CheckedChanged(object sender, EventArgs e)
        //{
        //    numericUpDownClusterNumber.ReadOnly = checkBoxAutomatedClusterNumber.Checked;
        //    if (checkBoxAutomatedClusterNumber.Checked)
        //        richTextBoxInfoClustering.AppendText("\nWarning: this task can be time consuming.\nIf the number of class is higher than 10, the clustering will not be performed.");
        //}
        #endregion

        #region EM Clustering
        private string ClusteringEM(bool IsFullScreen, int ClassNumber, FormForEMInfo WindowEMinfo)
        {
            if (IsFullScreen)
            {
                string feedback = ClusteringEMGlobalScreen(ClassNumber, WindowEMinfo);
                richTextBoxInfoClustering.AppendText("\nGlobal EM clustering done !\n");
                return feedback;


            }
            else
            {
                int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;
                for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
                {
                    cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(PlateIdx);
                    if ((ClassNumber == 0) || (ClassNumber == 1)) return null;
                    ClusteringEMSinglePlate(CurrentPlateToProcess, ClassNumber, WindowEMinfo);
                }
                richTextBoxInfoClustering.AppendText("\nPlate by plate EM clustering done !\n");
                return null;
            }
        }

        /// <summary>
        /// Perform an EM clustering on each plate independantely
        /// </summary>
        /// <param name="CurrentPlateToProcess">the plate to process</param>
        /// <param name="ClassNumber">Number of class</param>
        private void ClusteringEMSinglePlate(cPlate CurrentPlateToProcess, int ClassNumber, FormForEMInfo WindowEMinfo)
        {
            weka.core.Instances Ninsts = CurrentPlateToProcess.CreateInstancesWithoutClass();// CreateInstanceWithoutClass(CurrentTable);

            weka.clusterers.EM EMCluster = new EM();
            EMCluster.setNumClusters(ClassNumber);

            EMCluster.setMaxIterations((int)WindowEMinfo.numericUpDownMaxIterations.Value);
            EMCluster.setMinStdDev((double)WindowEMinfo.numericUpDownMinStdev.Value);
            EMCluster.setSeed((int)WindowEMinfo.numericUpDownSeedNumber.Value);


            EMCluster.buildClusterer(Ninsts);
            EMCluster.getClusterModelsNumericAtts();
            if (EMCluster.numberOfClusters() > cGlobalInfo.ListWellClasses.Count)
            {
                richTextBoxInfoClustering.AppendText("\n Plate " + CurrentPlateToProcess.GetName() + ", cluster Number: more than " + cGlobalInfo.ListWellClasses.Count + ", clustering not operated.\n");
                return;
            }
            else
                richTextBoxInfoClustering.AppendText("\n" + CurrentPlateToProcess.GetName() + ": " + EMCluster.numberOfClusters() + " cluster(s)");

            ClusterEvaluation eval = new ClusterEvaluation();
            eval.setClusterer(EMCluster);
            eval.evaluateClusterer(Ninsts);

            CurrentPlateToProcess.AssignClass(eval.getClusterAssignments());
        }

        /// <summary>
        /// perform an EM clustering over the entire screening data
        /// </summary>
        /// <param name="ClassNumber"></param>
        private string ClusteringEMGlobalScreen(int ClassNumber, FormForEMInfo WindowEMinfo)
        {
            weka.core.Instances Ninsts = cGlobalInfo.CurrentScreening.CreateInstancesWithoutClass();// CreateInstanceWithoutClass(CurrentTable);

            weka.clusterers.EM EMCluster = new EM();
            EMCluster.setNumClusters(ClassNumber);
            EMCluster.setMaxIterations((int)WindowEMinfo.numericUpDownMaxIterations.Value);
            EMCluster.setMinStdDev((double)WindowEMinfo.numericUpDownMinStdev.Value);
            EMCluster.setSeed((int)WindowEMinfo.numericUpDownSeedNumber.Value);

            EMCluster.buildClusterer(Ninsts);
            EMCluster.getClusterModelsNumericAtts();
            if (EMCluster.numberOfClusters() > cGlobalInfo.ListWellClasses.Count)
            {
                richTextBoxInfoClustering.AppendText("\nCluster Number: more than " + cGlobalInfo.ListWellClasses.Count + ", clustering not operated.\n");
                return null;
            }

            richTextBoxInfoClustering.AppendText("\n" + EMCluster.numberOfClusters() + " cluster(s) identified");

            ClusterEvaluation eval = new ClusterEvaluation();
            eval.setClusterer(EMCluster);
            eval.evaluateClusterer(Ninsts);

            cGlobalInfo.CurrentScreening.AssignClass(eval.getClusterAssignments());

            return eval.clusterResultsToString();
        }

        #endregion

        #region Hierarchical Clustering
        private void ClusteringHierarchical(bool IsFullScreen, int ClassNumber)
        {
            if (IsFullScreen)
            {
                ClusteringHierarchicalGlobalScreen(ClassNumber);
                richTextBoxInfoClustering.AppendText("\nGlobal Hierarchical clustering done !\n");
            }
            else
            {
                int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;
                for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
                {
                    cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(PlateIdx);
                    if ((ClassNumber == 0) || (ClassNumber == 1)) return;
                    ClusteringHierarchicalSinglePlate(CurrentPlateToProcess, ClassNumber);
                }
                richTextBoxInfoClustering.AppendText("\nPlate by plate Hierarchical clustering done !\n");
            }
        }

        /// <summary>
        /// Perform an Hierarchical clustering on each plate independantely
        /// </summary>
        /// <param name="CurrentPlateToProcess">the plate to process</param>
        /// <param name="ClassNumber">Number of class</param>
        private void ClusteringHierarchicalSinglePlate(cPlate CurrentPlateToProcess, int ClassNumber)
        {
            weka.core.Instances Ninsts = CurrentPlateToProcess.CreateInstancesWithoutClass();

            weka.clusterers.HierarchicalClusterer HClusterer = new HierarchicalClusterer();


            //string OptionDistance = " -A \"weka.core.";
            string OptionDistance = " -A \"";

            switch (cGlobalInfo.OptionsWindow.comboBoxHierarchicalDistance.SelectedIndex)
            {
                case 0:
                    OptionDistance += "EuclideanDistance";
                    break;
                case 1:
                    OptionDistance += "ManhattanDistance";
                    break;
                case 2:
                    OptionDistance += "ChebyshevDistance";
                    break;
                default:
                    break;
            }

            OptionDistance += " -R first-last\"";

            string[] TAGS_LINK_TYPE = { "SINGLE", "COMPLETE", "AVERAGE", "MEAN", "CENTROID", "WARD", "ADJCOMPLETE" };

            string WekaOption = "-L " + TAGS_LINK_TYPE[cGlobalInfo.OptionsWindow.comboBoxHierarchicalLinkType.SelectedIndex];// + OptionDistance;

            HClusterer.setOptions(weka.core.Utils.splitOptions(WekaOption));
            //EuclideanDistance2 Dist2 = new EuclideanDistance2();

            //HClusterer.setDistanceFunction(Dist2);
            HClusterer.setNumClusters(ClassNumber);
            HClusterer.buildClusterer(Ninsts);

            richTextBoxInfoClustering.AppendText("\n" + CurrentPlateToProcess.GetName() + ": " + HClusterer.numberOfClusters() + " cluster(s)");

            ClusterEvaluation eval = new ClusterEvaluation();
            eval.setClusterer(HClusterer);
            eval.evaluateClusterer(Ninsts);

            CurrentPlateToProcess.AssignClass(eval.getClusterAssignments());
        }

        /// <summary>
        /// perform an Hierarchical clustering over the entire screening data
        /// </summary>
        /// <param name="ClassNumber"></param>
        private void ClusteringHierarchicalGlobalScreen(int ClassNumber)
        {
            weka.core.Instances Ninsts = cGlobalInfo.CurrentScreening.CreateInstancesWithoutClass();
            weka.clusterers.HierarchicalClusterer HClusterer = new HierarchicalClusterer();

            string OptionDistance = " -A \"weka.core.";

            switch (cGlobalInfo.OptionsWindow.comboBoxHierarchicalDistance.SelectedIndex)
            {
                case 0:
                    OptionDistance += "EuclideanDistance";
                    break;
                case 1:
                    OptionDistance += "ManhattanDistance";
                    break;
                case 2:
                    OptionDistance += "ChebyshevDistance";
                    break;
                default:
                    break;
            }

            OptionDistance += " -R first-last\"";

            string[] TAGS_LINK_TYPE = { "SINGLE", "COMPLETE", "AVERAGE", "MEAN", "CENTROID", "WARD", "ADJCOMPLETE" };

            string WekaOption = "-L " + TAGS_LINK_TYPE[cGlobalInfo.OptionsWindow.comboBoxHierarchicalLinkType.SelectedIndex] + OptionDistance;

            HClusterer.setOptions(weka.core.Utils.splitOptions(WekaOption));
            HClusterer.setNumClusters(ClassNumber);
            HClusterer.buildClusterer(Ninsts);

            richTextBoxInfoClustering.AppendText("\n" + HClusterer.numberOfClusters() + " cluster(s) identified");

            ClusterEvaluation eval = new ClusterEvaluation();
            eval.setClusterer(HClusterer);
            eval.evaluateClusterer(Ninsts);


            cGlobalInfo.CurrentScreening.AssignClass(eval.getClusterAssignments());
        }

        #endregion

        #region K-Means
        /// <summary>
        /// K - Mean clustering procedure
        /// </summary>
        /// <param name="Classes"></param>
        /// <param name="PlateToProcess"></param>
        /// <returns></returns>
        private bool KMeans(int Classes, cPlate PlateToProcess)
        {
            int NumWell = PlateToProcess.GetNumberOfActiveWells();
            int Numdesc = cGlobalInfo.CurrentScreening.GetNumberOfActiveDescriptor();
            double[,] DataForKMeans = new double[NumWell, Numdesc];

            int IdxDesc = 0;
            for (int desc = 0; desc < cGlobalInfo.CurrentScreening.ListDescriptors.Count; desc++)
            {
                if (cGlobalInfo.CurrentScreening.ListDescriptors[desc].IsActive() == false) continue;
                List<double> CurrentDesc = new List<double>();
                double[] NormDesc = null;
                for (int IdxValue = 0; IdxValue < cGlobalInfo.CurrentScreening.Columns; IdxValue++)
                    for (int IdxValue0 = 0; IdxValue0 < cGlobalInfo.CurrentScreening.Rows; IdxValue0++)
                    {
                        cWell TmpWell = PlateToProcess.GetWell(IdxValue, IdxValue0, true);
                        if (TmpWell != null)
                            CurrentDesc.Add(TmpWell.ListSignatures[desc].GetValue());
                    }

                if (CurrentDesc.Count == 0) continue;
                NormDesc = MeanCenteringStdStandarization(CurrentDesc.ToArray());
                for (int row = 0; row < NumWell; row++)
                    DataForKMeans[row, IdxDesc] = NormDesc[row];


                IdxDesc++;
            }
            int Info;
            double[,] CenterPos;
            int[] ClusterIndx;

            try
            {
                alglib.kmeansgenerate(DataForKMeans, NumWell, Numdesc, Classes, 10, out Info, out CenterPos, out ClusterIndx);
                int Idx = 0;
                for (int IdxValue = 0; IdxValue < cGlobalInfo.CurrentScreening.Columns; IdxValue++)
                    for (int IdxValue0 = 0; IdxValue0 < cGlobalInfo.CurrentScreening.Rows; IdxValue0++)
                    {
                        cWell TmpWell = PlateToProcess.GetWell(IdxValue, IdxValue0, true);
                        if (TmpWell != null)
                        {
                            if (ClusterIndx[Idx] == -1)
                                TmpWell.SetAsNoneSelected();
                            else
                                TmpWell.SetClass(ClusterIndx[Idx]);

                            Idx++;
                        }
                    }
            }
            catch
            {
                richTextBoxInfoClustering.AppendText("\nPlate: " + PlateToProcess.GetName() + " skipped: data corrupted (check your descriptor data validity).");

                //MessageBox.Show("Check the data validity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void KMeansFullScreen(int Classes)
        {
            int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;
            int Numdesc = cGlobalInfo.CurrentScreening.GetNumberOfActiveDescriptor();

            int GlobalNumWell = 0;

            for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
            {
                cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(cGlobalInfo.CurrentScreening.ListPlatesActive[PlateIdx].GetName());
                GlobalNumWell += CurrentPlateToProcess.GetNumberOfActiveWells();
            }

            double[,] DataForKMeans = new double[GlobalNumWell, Numdesc];

            int IdxDesc = 0;
            for (int desc = 0; desc < cGlobalInfo.CurrentScreening.ListDescriptors.Count; desc++)
            {
                if (cGlobalInfo.CurrentScreening.ListDescriptors[desc].IsActive() == false) continue;
                List<double> CurrentDesc = new List<double>();
                double[] NormDesc = null;

                for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
                {
                    cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(cGlobalInfo.CurrentScreening.ListPlatesActive[PlateIdx].GetName());
                    int NumWell = CurrentPlateToProcess.GetNumberOfActiveWells();

                    for (int IdxValue = 0; IdxValue < cGlobalInfo.CurrentScreening.Columns; IdxValue++)
                        for (int IdxValue0 = 0; IdxValue0 < cGlobalInfo.CurrentScreening.Rows; IdxValue0++)
                        {
                            cWell TmpWell = CurrentPlateToProcess.GetWell(IdxValue, IdxValue0, true);
                            if (TmpWell != null)
                                CurrentDesc.Add(TmpWell.ListSignatures[desc].GetValue());
                        }
                }
                if (CurrentDesc.Count == 0) continue;
                NormDesc = MeanCenteringStdStandarization(CurrentDesc.ToArray());
                for (int IDxWell = 0; IDxWell < GlobalNumWell; IDxWell++)
                    DataForKMeans[IDxWell, IdxDesc] = NormDesc[IDxWell];
                IdxDesc++;
            }
            int Info;
            double[,] CenterPos;
            int[] ClusterIndx;

            try
            {
                alglib.kmeansgenerate(DataForKMeans, GlobalNumWell, Numdesc, Classes, 10, out Info, out CenterPos, out ClusterIndx);
                int Idx = 0;

                for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
                {
                    cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(cGlobalInfo.CurrentScreening.ListPlatesActive[PlateIdx].GetName());

                    for (int IdxValue = 0; IdxValue < cGlobalInfo.CurrentScreening.Columns; IdxValue++)
                        for (int IdxValue0 = 0; IdxValue0 < cGlobalInfo.CurrentScreening.Rows; IdxValue0++)
                        {
                            cWell TmpWell = CurrentPlateToProcess.GetWell(IdxValue, IdxValue0, true);
                            if (TmpWell != null)
                            {
                                if (ClusterIndx[Idx] == -1)
                                    TmpWell.SetAsNoneSelected();
                                else
                                    TmpWell.SetClass(ClusterIndx[Idx]);

                                Idx++;
                            }
                        }
                }
            }
            catch
            {
                MessageBox.Show("Check the data validity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// K-Mean for Systematic error Identification
        /// </summary>
        /// <param name="Classes"></param>
        /// <param name="PlateToProcess"></param>
        /// <param name="Desc"></param>
        /// <returns></returns>
        private bool KMeans(int Classes, cPlate PlateToProcess, int Desc)
        {
            int NumWell = PlateToProcess.GetNumberOfActiveWells();
            int Numdesc = cGlobalInfo.CurrentScreening.GetNumberOfActiveDescriptor();
            double[,] DataForKMeans = new double[NumWell, Numdesc];

            List<double> CurrentDesc = new List<double>();
            double[] NormDesc = null;
            for (int IdxValue = 0; IdxValue < cGlobalInfo.CurrentScreening.Columns; IdxValue++)
                for (int IdxValue0 = 0; IdxValue0 < cGlobalInfo.CurrentScreening.Rows; IdxValue0++)
                {
                    cWell TmpWell = PlateToProcess.GetWell(IdxValue, IdxValue0, true);
                    if (TmpWell != null)
                        CurrentDesc.Add(TmpWell.ListSignatures[Desc].GetValue());
                }

            if (CurrentDesc.Count == 0) return false;
            NormDesc = MeanCenteringStdStandarization(CurrentDesc.ToArray());
            for (int row = 0; row < NumWell; row++)
                DataForKMeans[row, 0] = NormDesc[row];

            int Info;
            double[,] CenterPos;
            int[] ClusterIndx;

            try
            {
                alglib.kmeansgenerate(DataForKMeans, NumWell, Numdesc, Classes, 10, out Info, out CenterPos, out ClusterIndx);
                int Idx = 0;
                for (int IdxValue = 0; IdxValue < cGlobalInfo.CurrentScreening.Columns; IdxValue++)
                    for (int IdxValue0 = 0; IdxValue0 < cGlobalInfo.CurrentScreening.Rows; IdxValue0++)
                    {
                        cWell TmpWell = PlateToProcess.GetWell(IdxValue, IdxValue0, true);
                        if (TmpWell != null)
                        {
                            if (ClusterIndx[Idx] == -1)
                                TmpWell.SetAsNoneSelected();
                            else
                                TmpWell.SetClass(ClusterIndx[Idx]);

                            Idx++;
                        }
                    }
            }
            catch
            {
                MessageBox.Show("Check the data validity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

    }
}
