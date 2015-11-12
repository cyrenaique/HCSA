using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using weka.core;
//using System.Data;
using weka.clusterers;
using LibPlateAnalysis;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Msagl.GraphViewerGdi;
using HCSAnalyzer.Forms;
using weka.attributeSelection;
using weka.filters;
using weka.classifiers;
using HCSAnalyzer.Classes;
using Microsoft.Research.Science.Data;
using Microsoft.Research.Science.Data.Imperative;
using System.IO;
using HCSAnalyzer.Forms.IO;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer
{
    public partial class HCSAnalyzer
    {
        public virtual bool IsFileUsed(string file)
        {
            try
            {
                using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
                {
                    //fs.CanWrite;
                }
                return false;
            }
            catch (IOException ex)
            {
                return true;
            }
        }

        private FormForImportExcel CellByCellFromCSV(string FileName, char Delimiter)
        {
            FormForImportExcel FromExcel = new FormForImportExcel();
            FromExcel.IsImportCSV = true;

            int Mode = 2;
            if (cGlobalInfo.OptionsWindow.radioButtonWellPosModeSingle.Checked) Mode = 1;
            CsvFileReader CSVsr = new CsvFileReader(FileName);
            CSVsr.Separator = Delimiter;

            CsvRow Names = new CsvRow();
            if (!CSVsr.ReadRow(Names))
            {
                CSVsr.Close();
                return null;
            }

            int NumPreview = 4;
            List<CsvRow> LCSVRow = new List<CsvRow>();
            for (int Idx = 0; Idx < NumPreview; Idx++)
            {
                CsvRow TNames = new CsvRow();
                // if (TNames.Count == 0) break;
                if (!CSVsr.ReadRow(TNames))
                {
                    CSVsr.Close();
                    return null;
                }
                LCSVRow.Add(TNames);
            }

            // FromExcel.dataGridViewForImport.RowsDefaultCellStyle.BackColor = Color.Bisque;
            FromExcel.dataGridViewForImport.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;


            DataGridViewColumn ColName = new DataGridViewColumn();
            FromExcel.dataGridViewForImport.Columns.Add("Data Name", "Data Name");

            DataGridViewCheckBoxColumn columnSelection = new DataGridViewCheckBoxColumn();
            columnSelection.Name = "Selection";
            FromExcel.dataGridViewForImport.Columns.Add(columnSelection);

            DataGridViewComboBoxColumn columnType = new DataGridViewComboBoxColumn();
            if (cGlobalInfo.OptionsWindow.radioButtonWellPosModeDouble.Checked)
                columnType.DataSource = new string[] { "Plate name", "Column", "Row", "Descriptor", "Phenotype Class" };
            else
                columnType.DataSource = new string[] { "Plate name", "Well position", "Descriptor", "Phenotype Class" };
            columnType.Name = "Type";
            FromExcel.dataGridViewForImport.Columns.Add(columnType);

            for (int i = 0; i < LCSVRow.Count; i++)
            {
                DataGridViewColumn NewCol = new DataGridViewColumn();
                NewCol.ReadOnly = true;
                FromExcel.dataGridViewForImport.Columns.Add("Readout " + i, "Readout " + i);
            }

            if (LCSVRow[0].Count > Names.Count)
            {
                CSVsr.Close();
                MessageBox.Show("Inconsistent column number. Check your CSV file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }

            for (int i = 0; i < Names.Count; i++)
            {
                int IdxRow = 0;
                FromExcel.dataGridViewForImport.Rows.Add();
                FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = Names[i];

                if (i == 0) FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = true;
                else if (i == 1) FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = true;
                else if ((i == 2) && (cGlobalInfo.OptionsWindow.radioButtonWellPosModeDouble.Checked)) FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = true;
                else FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = false;

                if (i == 0)
                {
                    FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = "Plate name";
                }
                else if (i == 1)
                {
                    if (cGlobalInfo.OptionsWindow.radioButtonWellPosModeDouble.Checked)
                    {
                        FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = "Column";
                    }
                    else
                    {
                        FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = "Well position";
                    }
                }
                else if (i == 2)
                {
                    if (cGlobalInfo.OptionsWindow.radioButtonWellPosModeDouble.Checked)
                    {
                        FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = "Row";
                    }
                    else
                        FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = "Descriptor";
                }
                else
                {
                    FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = "Descriptor";
                }

                for (int j = 0; j < LCSVRow.Count; j++)
                {
                    if (i < LCSVRow[j].Count)
                        FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow + j].Value = LCSVRow[j][i].ToString();
                    else
                        FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow + j].Value = "";
                }
            }

            FromExcel.dataGridViewForImport.Update();
            // FromExcel.dataGridViewForImport.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewForImport_MouseClick);

            FromExcel.CurrentScreen = cGlobalInfo.CurrentScreening;
            FromExcel.thisHCSAnalyzer = this;

            return FromExcel;
        }

        private void LoadingCSVProcedure(FormForImportExcel FromExcel)
        {
            int Mode = 2;
            if (cGlobalInfo.OptionsWindow.radioButtonWellPosModeSingle.Checked) Mode = 1;
            CsvFileReader CSVsr = new CsvFileReader(PathNames[0]);
            if (!CSVsr.BaseStream.CanRead)
            {
                MessageBox.Show("Cannot read the file !", "Process finished !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CSVsr.Close();
                return;
            }
            CsvRow Names = new CsvRow();
            if (!CSVsr.ReadRow(Names))
            {
                CSVsr.Close();
                return;
            }
            int ColSelectedForName = GetColIdxFor("Name", FromExcel);
            //  int ColLocusID = GetColIdxFor("Locus ID", FromExcel);
            // int ColConcentration = GetColIdxFor("Concentration", FromExcel);
            //  int ColInfo = GetColIdxFor("Info", FromExcel);
            //  int ColClass = GetColIdxFor("Class", FromExcel);
            int ColPlateName = GetColIdxFor("Plate name", FromExcel);
            int ColCol = GetColIdxFor("Column", FromExcel);
            int ColRow = GetColIdxFor("Row", FromExcel);
            int ColWellPos = GetColIdxFor("Well position", FromExcel);
            int[] ColsForDescriptors = GetColsIdxFor("Descriptor", FromExcel);


            while (CSVsr.EndOfStream != true)
            {
            NEXT: ;
                CsvRow CurrentDesc = new CsvRow();
                if (CSVsr.ReadRow(CurrentDesc) == false) break;

                string PlateName = CurrentDesc[ColPlateName];
                //return;
                // check if the plate exist already
                cPlate CurrentPlate = cGlobalInfo.CurrentScreening.GetPlateIfNameIsContainIn(PlateName);
                if (CurrentPlate == null) goto NEXT;

                int[] Pos = new int[2];
                if (Mode == 1)
                {
                    Pos = ConvertPosition(CurrentDesc[ColWellPos]);
                }
                else
                {
                    if (!int.TryParse(CurrentDesc[ColCol], out Pos[0]))
                    {
                        if (MessageBox.Show("Error in converting the current well position.\nGo to Edit->Options->Import-Export->Well Position Mode to fix this.\nDo you want continue ?", "Loading error !", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.No)
                        {
                            CSVsr.Close();
                            return;
                        }
                        else
                            goto NEXTLOOP;
                    }

                    Pos[1] = Convert.ToInt16(CurrentDesc[ColRow]);
                }

                cWell CurrentWell = CurrentPlate.GetWell(Pos[0] - 1, Pos[1] - 1, false);
                if (CurrentWell == null) goto NEXT;

                if (ColSelectedForName != -1)
                {
                    
                    CurrentWell.SetCpdName(CurrentDesc[ColSelectedForName]);
                }
                else
                {
                    CurrentWell.SetAsNoneSelected();
                }

                //if (ColLocusID != -1)
            //{
            //    double CurrentValue;

                //    if (!double.TryParse(CurrentDesc[ColLocusID], out CurrentValue))
            //        goto NEXTSTEP;

                //    CurrentWell.LocusID = CurrentValue;

                //}
            //if (ColConcentration != -1)
            //{
            //    double CurrentValue;

                //    if (!double.TryParse(CurrentDesc[ColConcentration], out CurrentValue))
            //        goto NEXTSTEP;


                //    CurrentWell.Concentration = CurrentValue;

                //}
            NEXTSTEP: ;

                //if (ColInfo != -1)
            //    CurrentWell.Info = CurrentDesc[ColInfo];

                //if (ColClass != -1)
            //{
            //    int CurrentValue;
            //    if (!int.TryParse(CurrentDesc[ColClass], out CurrentValue))
            //        goto NEXTLOOP;
            //    CurrentWell.SetClass(CurrentValue);
            //}

            NEXTLOOP: ;

            }

            CSVsr.Close();

            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);

            MessageBox.Show("File loaded", "Process finished !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }


        private static bool IsRightName(string Name, string NameToCompare)
        {
            if (Name == NameToCompare)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region User interface
        private void comboBoxCLassificationMethod_SelectedIndexChanged(object sender, EventArgs e)
        {

            richTextBoxInfoClassif.Clear();

            switch (comboBoxCLassificationMethod.SelectedIndex)
            {
                case 0:
                    richTextBoxInfoClassif.AppendText("C4.5.\nFor more information, go to: http://en.wikipedia.org/wiki/C4.5_algorithm \n");
                    break;
                case 1:
                    richTextBoxInfoClassif.AppendText("Support Vector Machine.\nFor more information, go to: http://en.wikipedia.org/wiki/Support_vector_machine \n");
                    break;
                case 2:
                    richTextBoxInfoClassif.AppendText("Neural Network.\nFor more information, go to: http://en.wikipedia.org/wiki/Artificial_neural_network \n");
                    break;
                case 3:
                    richTextBoxInfoClassif.AppendText("K-Nearest Neighbor(s).\nFor more information, go to: http://en.wikipedia.org/wiki/K-nearest_neighbor_algorithm \n");
                    break;
                case 4:
                    richTextBoxInfoClassif.AppendText("Random Forest.\nFor more information, go to: http://en.wikipedia.org/wiki/Random_forest \n");
                    break;

            }

        }

        private void buttonStartClassification_Click_1(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            if (!cGlobalInfo.CurrentScreening.IsSelectedDescriptors())
            {
                MessageBox.Show("You have to check at least one descriptor !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            this.Cursor = Cursors.WaitCursor;
            Classification(comboBoxNeutralClassForClassif.SelectedIndex, ProcessModeplateByPlateToolStripMenuItem.Checked, comboBoxCLassificationMethod.SelectedIndex);
            this.Cursor = Cursors.Default;

            switch (comboBoxCLassificationMethod.SelectedIndex)
            {
                case 0:
                    if (ProcessModeplateByPlateToolStripMenuItem.Checked)
                        MessageBox.Show("C4.5 classification process finished ! \n Press (Ctrl+T) for current plate tree.", "Process over !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                        MessageBox.Show("C4.5 classification process finished ! \n", "Process over !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 1:
                    MessageBox.Show("Support Vector Machine classification process finished !", "Process over !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 2:
                    MessageBox.Show("Neural Network classification process finished !", "Process over !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 3:
                    MessageBox.Show("K-Nearest Neighbor(s) classification process finished !", "Process over !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 4:
                    MessageBox.Show("Random Forest classification process finished !", "Process over !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                default:
                    break;
            }

            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);


        }

        private void richTextBoxInfoClassif_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            ClickOnLink(e.LinkText);
        }

        private void classificationTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            FormForClassificationTree WindowForTree = new FormForClassificationTree();

            WindowForTree.Text = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetName();
            string StringForTree = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetInfoClassif().StringForTree;
            if ((StringForTree == null) || (StringForTree.Length == 0))
            {
                MessageBox.Show("No tree avaliable for the selected plate !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            WindowForTree.gViewerForTreeClassif.Graph = ComputeAndDisplayGraph(StringForTree.Remove(StringForTree.Length - 3, 3), false);

            WindowForTree.richTextBoxConsoleForClassification.Clear();
            WindowForTree.richTextBoxConsoleForClassification.AppendText(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetInfoClassif().StringForQuality);
            WindowForTree.richTextBoxConsoleForClassification.AppendText(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetInfoClassif().ConfusionMatrix);

            WindowForTree.Show();
        }
        #endregion

        #region Tree graph display functions
        public Microsoft.Msagl.Drawing.Graph ComputeAndDisplayGraph(string DotString, bool IsCellular)
        {
            int CurrentPos = 0;
            int NextReturnPos = CurrentPos;
            List<int> ListNodeId = new List<int>();
            string TmpDotString = DotString;

            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

            while (NextReturnPos != -1)
            {
                int NextBracket = DotString.IndexOf("[");
                string StringToProcess = DotString.Remove(NextBracket - 1);
                string StringToProcess1 = StringToProcess.Remove(0, 1);

                if (StringToProcess1.Contains("N") == false)
                {
                    int Id = Convert.ToInt32(StringToProcess1);
                    Microsoft.Msagl.Drawing.Node Currentnode = new Microsoft.Msagl.Drawing.Node(Id.ToString());
                    Currentnode.Label.FontColor = Microsoft.Msagl.Drawing.Color.Red;
                    int LabelPos = DotString.IndexOf("label=\"");
                    string LabelString = DotString.Remove(0, LabelPos + 7);
                    LabelPos = LabelString.IndexOf("\"");
                    string FinalLabel = LabelString.Remove(LabelPos);
                    Currentnode.LabelText = FinalLabel;

                    if (IsCellular && (FinalLabel[FinalLabel.Length - 1] == ')') && (FinalLabel.Contains('(')))   // that's a leaf
                    {
                        int LastBracket = FinalLabel.LastIndexOf('(');
                        string LeafName = FinalLabel.Remove(LastBracket - 1);

                        cCellularPhenotype CP = cGlobalInfo.ListCellularPhenotypes.FindFromName(LeafName);
                        if (CP != null)
                        {
                            Currentnode.Attr.FillColor = new Microsoft.Msagl.Drawing.Color(CP.ColourForDisplay.R, CP.ColourForDisplay.G, CP.ColourForDisplay.B);
                        }
                    }
                    graph.AddNode(Currentnode);
                }

                NextReturnPos = DotString.IndexOf("\n");
                if (NextReturnPos != -1)
                {
                    string TmpString = DotString.Remove(0, NextReturnPos + 1);
                    DotString = TmpString;
                }
            }

            DotString = TmpDotString;
            NextReturnPos = 0;
            while (NextReturnPos != -1)
            {
                int NextBracket = DotString.IndexOf("[");
                string StringToProcess = DotString.Remove(NextBracket - 1);
                string StringToProcess1 = StringToProcess.Remove(0, 1);

                if (StringToProcess1.Contains("N"))
                {
                    string stringNodeIdxStart = StringToProcess1.Remove(StringToProcess1.IndexOf("-"));
                    int NodeIdxStart = Convert.ToInt32(stringNodeIdxStart);

                    string stringNodeIdxEnd = StringToProcess1.Remove(0, StringToProcess1.IndexOf("N") + 1);
                    int NodeIdxSEnd = Convert.ToInt32(stringNodeIdxEnd);

                    int LabelPos = DotString.IndexOf("label=");
                    LabelPos += 7;

                    string CurrLabelString = DotString.Remove(0, LabelPos);

                    Microsoft.Msagl.Drawing.Edge Currentedge = new Microsoft.Msagl.Drawing.Edge(stringNodeIdxStart, ""/*NodeIdx.ToString()*/, stringNodeIdxEnd);
                    Currentedge.LabelText = CurrLabelString.Remove(CurrLabelString.IndexOf("]") - 1);
                    graph.Edges.Add(Currentedge);
                }

                NextReturnPos = DotString.IndexOf("\n");

                if (NextReturnPos != -1)
                {
                    string TmpString = DotString.Remove(0, NextReturnPos + 1);
                    DotString = TmpString;
                }
            }
            return graph;
        }

        private Microsoft.Msagl.Drawing.Graph DisplayTheGraph(cPlate PlateForTree)
        {
            FormForClassificationTree WindowForTree = new FormForClassificationTree();

            WindowForTree.Text = PlateForTree.GetName();
            string StringForTree = PlateForTree.GetInfoClassif().StringForTree;
            if ((StringForTree == null) || (StringForTree.Length == 0))
                return null;

            WindowForTree.richTextBoxConsoleForClassification.Clear();
            WindowForTree.richTextBoxConsoleForClassification.AppendText(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetInfoClassif().StringForQuality);
            WindowForTree.richTextBoxConsoleForClassification.AppendText(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetInfoClassif().ConfusionMatrix);

            return ComputeAndDisplayGraph(StringForTree.Remove(StringForTree.Length - 3, 3), false);

        }
        #endregion

        #region Classification Main functions
        private void Classification(int NeutralClass, bool IsPlateByPlate, int IdxClassifier)
        {
            if (IsPlateByPlate)
                ClassificationPlateByPlate(NeutralClass, IdxClassifier);
            else
                ClassificationGlobal(NeutralClass, IdxClassifier);
        }

        /// <summary>
        /// Global classification
        /// </summary>
        /// <param name="NeutralClass">Neutral class</param>
        /// <param name="IdxClassifier">Classifier Index (0:J48), (1:SVM), (2:NN), (3:KNN)</param>
        private void ClassificationGlobal(int NeutralClass, int IdxClassifier)
        {
            cInfoClass InfoClass = cGlobalInfo.CurrentScreening.GetNumberOfClassesBut(NeutralClass);

            if (InfoClass.NumberOfClass <= 1)
            {
                richTextBoxInfoClassif.AppendText("Screening not processed.\n");
                return;
            }

            cExtendedTable TrainingTable = cGlobalInfo.CurrentScreening.ListPlatesActive.GetListActiveWells().GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors(), false, true);

            weka.core.Instances insts = cGlobalInfo.CurrentScreening.CreateInstancesWithClasses(InfoClass, NeutralClass);
            Classifier ClassificationModel = null;

            switch (IdxClassifier)
            {
                case 0: // J48
                    ClassificationModel = new weka.classifiers.trees.J48();
                    weka.classifiers.trees.J48 J48Model = (weka.classifiers.trees.J48)ClassificationModel;
                    J48Model.setMinNumObj((int)cGlobalInfo.OptionsWindow.numericUpDownJ48MinNumObjects.Value);
                    richTextBoxInfoClassif.AppendText("\nC4.5 : " + InfoClass.NumberOfClass + " classes");
                    break;
                case 1: // SVM
                    ClassificationModel = new weka.classifiers.functions.SMO();
                    break;
                case 2: // NN
                    ClassificationModel = new weka.classifiers.functions.MultilayerPerceptron();
                    break;
                case 3: // KNN
                    ClassificationModel = new weka.classifiers.lazy.IBk((int)cGlobalInfo.OptionsWindow.numericUpDownKofKNN.Value);
                    break;
                case 4: // Random Forest
                    ClassificationModel = new weka.classifiers.trees.RandomForest();
                    break;
                default:
                    break;
            }


            weka.core.Instances train = new weka.core.Instances(insts, 0, insts.numInstances());

            ClassificationModel.buildClassifier(train);
            cGlobalInfo.ConsoleWriteLine(ClassificationModel.ToString());

            weka.classifiers.Evaluation evaluation = new weka.classifiers.Evaluation(insts);
            evaluation.crossValidateModel(ClassificationModel, insts, 2, new java.util.Random(1));


            cGlobalInfo.ConsoleWriteLine(evaluation.toSummaryString());
            cGlobalInfo.ConsoleWriteLine(evaluation.toMatrixString());

            // update classification information of the current plate
            string Text = "";
            switch (IdxClassifier)
            {
                case 0: // J48
                    Text = "J48 - ";
                    break;
                case 1: // SVM
                    //  ClassificationModel = new weka.classifiers.functions.SMO();
                    Text = "SVM - ";
                    break;
                case 2: // NN
                    // ClassificationModel = new weka.classifiers.functions.MultilayerPerceptron();
                    Text = "Neural Network - ";
                    break;
                case 3: // KNN
                    // ClassificationModel = new weka.classifiers.lazy.IBk((int)CompleteScreening.GlobalInfo.OptionsWindow.numericUpDownKofKNN.Value);
                    Text = "K-Nearest Neighbor(s) - ";
                    break;
                default:
                    break;
            }
            richTextBoxInfoClassif.AppendText(Text + InfoClass.NumberOfClass + " classes.");

            // CurrentPlateToProcess.GetInfoClassif().StringForQuality = evaluation.toSummaryString();
            //  CurrentPlateToProcess.GetInfoClassif().ConfusionMatrix = evaluation.toMatrixString();
            foreach (cPlate CurrentPlateToProcess in cGlobalInfo.CurrentScreening.ListPlatesActive)
            {
                foreach (cWell TmpWell in CurrentPlateToProcess.ListActiveWells)
                {
                    // return;
                    weka.core.Instance currentInst = TmpWell.CreateInstanceForNClasses(InfoClass).instance(0);
                    double predictedClass = ClassificationModel.classifyInstance(currentInst);
                    double[] ClassConfidence = ClassificationModel.distributionForInstance(currentInst);
                    double ConfidenceValue = ClassConfidence[(int)predictedClass];
                    TmpWell.SetClass(InfoClass.ListBackAssociation[(int)predictedClass], ConfidenceValue);
                }
            }
            return;
        }

        /// <summary>
        /// Plate by plate classification
        /// </summary>
        /// <param name="NeutralClass">Neutral class</param>
        /// <param name="IdxClassifier">Classifier Index (0:J48), (1:SVM), (2:NN), (3:KNN)</param>
        private void ClassificationPlateByPlate(int NeutralClass, int IdxClassifier)
        {

            int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;

            for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
            {
                cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(cGlobalInfo.CurrentScreening.ListPlatesActive[PlateIdx].GetName());
                cInfoClass InfoClass = CurrentPlateToProcess.GetNumberOfClassesBut(NeutralClass);
                // return;
                if (InfoClass.NumberOfClass <= 1)
                {
                    richTextBoxInfoClassif.AppendText(CurrentPlateToProcess.GetName() + " not processed.\n");
                    continue;
                }

                weka.core.Instances insts = CurrentPlateToProcess.CreateInstancesWithClasses(InfoClass, NeutralClass);
                Classifier ClassificationModel = null;
                string Text = "";
                switch (IdxClassifier)
                {
                    case 0: // J48
                        ClassificationModel = new weka.classifiers.trees.J48();
                        weka.classifiers.trees.J48 J48Model = (weka.classifiers.trees.J48)ClassificationModel;
                        J48Model.setMinNumObj((int)cGlobalInfo.OptionsWindow.numericUpDownJ48MinNumObjects.Value);
                        Text = "J48 - ";
                        break;
                    case 1: // SVM
                        ClassificationModel = new weka.classifiers.functions.SMO();
                        Text = "SVM - ";
                        break;
                    case 2: // NN
                        ClassificationModel = new weka.classifiers.functions.MultilayerPerceptron();
                        Text = "Neural Network - ";
                        break;
                    case 3: // KNN
                        ClassificationModel = new weka.classifiers.lazy.IBk((int)cGlobalInfo.OptionsWindow.numericUpDownKofKNN.Value);
                        Text = "K-Nearest Neighbor(s) - ";
                        break;
                    case 4: // Random Forest
                        ClassificationModel = new weka.classifiers.trees.RandomForest();
                        Text = "Random Forest - ";

                        break;
                    default:
                        break;
                }
                richTextBoxInfoClassif.AppendText(Text + InfoClass.NumberOfClass + " classes - Plate: ");

                richTextBoxInfoClassif.AppendText(CurrentPlateToProcess.GetName() + " OK \n");
                weka.core.Instances train = new weka.core.Instances(insts, 0, insts.numInstances());

                ClassificationModel.buildClassifier(train);
                cGlobalInfo.ConsoleWriteLine(ClassificationModel.ToString());

                weka.classifiers.Evaluation evaluation = new weka.classifiers.Evaluation(insts);
                evaluation.crossValidateModel(ClassificationModel, insts, 2, new java.util.Random(1));


                cGlobalInfo.ConsoleWriteLine(evaluation.toSummaryString());
                cGlobalInfo.ConsoleWriteLine(evaluation.toMatrixString());

                // update classification information of the current plate
                switch (IdxClassifier)
                {
                    case 0: // J48
                        weka.classifiers.trees.J48 CurrentClassifier = (weka.classifiers.trees.J48)(ClassificationModel);
                        CurrentPlateToProcess.GetInfoClassif().StringForTree = CurrentClassifier.graph().Remove(0, CurrentClassifier.graph().IndexOf("{") + 2);
                        break;
                    /*case 1: // SVM

                        break;
                    case 2: // NN

                        break;
                    case 3: // KNN

                        break;*/
                    default:
                        break;
                }

                CurrentPlateToProcess.GetInfoClassif().StringForQuality = evaluation.toSummaryString();
                CurrentPlateToProcess.GetInfoClassif().ConfusionMatrix = evaluation.toMatrixString();

                foreach (cWell TmpWell in CurrentPlateToProcess.ListActiveWells)
                {
                    weka.core.Instance currentInst = TmpWell.CreateInstanceForNClasses(InfoClass).instance(0);
                    double predictedClass = ClassificationModel.classifyInstance(currentInst);

                    TmpWell.SetClass(InfoClass.ListBackAssociation[(int)predictedClass]);
                }
            }
            return;
        }
        #endregion

    }
}

