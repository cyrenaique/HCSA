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

using System.Data.OleDb;
using System.Linq;
using System.Drawing.Imaging;
using HCSAnalyzer.Forms;
using HCSAnalyzer.Classes;
using weka.core.converters;
using System.Diagnostics;
using HCSAnalyzer.Forms.IO;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using System.Data.SQLite;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.General_Types;
using System.Globalization;
using HCSAnalyzer.Forms.FormsForOptions;
using HCSAnalyzer.Classes.Base_Classes.Data;

namespace HCSAnalyzer
{
    public partial class HCSAnalyzer
    {
        #region User Interface

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;
            int NumDesc = cGlobalInfo.CurrentScreening.ListDescriptors.Count;

            FolderBrowserDialog OpenFolderDialog = new FolderBrowserDialog();
            DialogResult result = OpenFolderDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            string PathName = OpenFolderDialog.SelectedPath;

            this.Cursor = Cursors.WaitCursor;
            // save a CSV file containing all the plates and chosen descriptors
            if (checkBoxExportFullScreen.Checked)
            {
                bool res = DisplayDescriptorsToSave(PathName + "\\fullScreen.csv");
                if (res == false) return;
            }
            // save a CSV file containting the plate designs
            ExportToCSV(PathName, true/*checkBoxExportToCSVIncludeDescriptors.Checked*/, checkBoxExportPlateFormat.Checked, false, /*checkBoxExportToCSVIncludeName.Checked*/ true, /*checkBoxExportToCSVIncludeInfo.Checked*/ true);

            // save the report
            //  if (checkBoxExportScreeningInformation.Checked)
            richTextBoxForScreeningInformation.SaveFile(PathName + "\\Information.rtf");


            if (treeViewSelectionForExport.Nodes["NodeClassification"].Nodes["NodeClassifTree"].Checked)
            {
                for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
                {
                    cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(cGlobalInfo.CurrentScreening.ListPlatesActive[PlateIdx].GetName());
                    Microsoft.Msagl.Drawing.Graph CurrentGraph = DisplayTheGraph(CurrentPlateToProcess);
                    if (CurrentGraph == null) continue;

                    Microsoft.Msagl.GraphViewerGdi.GraphRenderer renderer = new Microsoft.Msagl.GraphViewerGdi.GraphRenderer(CurrentGraph);
                    renderer.CalculateLayout();

                    Bitmap bitmap = new Bitmap(800, 800, PixelFormat.Format32bppPArgb);
                    renderer.Render(bitmap);

                    string CorrectedName = CheckAndCorrectFilemName(CurrentPlateToProcess.GetName(), false);
                    bitmap.Save(PathName + "\\" + CorrectedName + ".png", ImageFormat.Png);

                    //MemoryStream ms = new MemoryStream();
                    //this.chartForSimpleForm.SaveImage(ms, ChartImageFormat.Bmp);
                    //Bitmap bm = new Bitmap(ms);
                    //Clipboard.SetImage(bitmap);
                    //CompleteScreening.CurrentRichTextBox.Paste();
                }
            }

            //if (treeViewSelectionForExport.Nodes["NodeQualityControl"].Nodes["NodeSystematicError"].Checked)
            //{
            //    DataTable ResultSystematicError = ComputeSystematicErrorsTable();
            //    dataGridViewForQualityControl.DataSource = ResultSystematicError;
            //    dataGridViewForQualityControl.Update();

            //    DataGridfViewToCsV(dataGridViewForQualityControl, PathName + "\\SystematicErrorReport.csv");
            //}

            if (treeViewSelectionForExport.Nodes["NodeQualityControl"].Nodes["NodeZfactor"].Checked)
            {
                //RichTextBox NewBox = new RichTextBox();
                //int IdxDesc = cGlobalInfo.CurrentScreening.ListDescriptors.Count;
                //int TmpMaxWidth = 700;
                //// loop on all the desciptors
                //for (int Desc = 0; Desc < NumDesc; Desc++)
                //{
                //    if (cGlobalInfo.CurrentScreening.ListDescriptors[Desc].IsActive() == false) continue;
                //    MemoryStream ms = new MemoryStream();
                //    SimpleForm GraphForm = BuildZFactor(Desc);

                //    foreach (DataPoint CurrentPt in GraphForm.chartForSimpleForm.Series[0].Points)
                //        CurrentPt.Label = "";

                //    GraphForm.chartForSimpleForm.SaveImage(ms, ChartImageFormat.Bmp);
                //    Bitmap bm = new Bitmap(ms);
                //    Clipboard.SetImage(bm);
                //    NewBox.Paste();
                //    NewBox.AppendText("\n");

                //    NewBox.AppendText(GraphForm.GetValues());

                //}

                //NewBox.SaveFile(PathName + "\\ZFactors.rtf");
                //cGlobalInfo.OptionsWindow.numericUpDownMaximumWidth.Value = TmpMaxWidth;
            }


            if (treeViewSelectionForExport.Nodes["NodeQualityControl"].Nodes["NodeCorrelationMatRank"].Checked)
                ComputeAndDisplayCorrelationMatrix(true, false, PathName + "\\Correlation");

            if (treeViewSelectionForExport.Nodes["NodesiRNA"].Nodes["NodePathwayAnalysis"].Checked)
            {
                int Class = 1;
                PathWayAnalysis(Class).chartForPie.SaveImage(PathName + "\\PathWayAnalysis.emf", ChartImageFormat.Emf);
            }


            if (treeViewSelectionForExport.Nodes["NodeMisc"].Nodes["NodeWekaArff"].Checked)
            {
                cInfoClass InfoClass = cGlobalInfo.CurrentScreening.GetNumberOfClassesBut(-1);
                Instances insts = cGlobalInfo.CurrentScreening.CreateInstancesWithClasses(InfoClass, -1);
                ArffSaver saver = new ArffSaver();
                CSVSaver savercsv = new CSVSaver();
                saver.setInstances(insts);
                saver.setFile(new java.io.File(PathName + "\\data.arff"));
                saver.writeBatch();
            }
            this.Cursor = Cursors.Default;

            Process.Start(PathName);

            MessageBox.Show("Export done !");
        }

        /// <summary>
        /// Save screen to MTR format
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void currentPlateTomtrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            SaveFileDialog CurrSaveFileDialog = new SaveFileDialog();
            CurrSaveFileDialog.Filter = "mtr files (*.mtr)|*.mtr|text files (*.txt)|*.txt";
            System.Windows.Forms.DialogResult Res = CurrSaveFileDialog.ShowDialog();
            if (Res != System.Windows.Forms.DialogResult.OK) return;
            string PathName = CurrSaveFileDialog.FileName;

            if (PathName == "") return;

            StreamWriter stream = new StreamWriter(PathName, true, System.Text.Encoding.ASCII);
            int iValue = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;
            stream.Write(iValue.ToString() + " ");
            iValue = cGlobalInfo.CurrentScreening.Rows;
            stream.Write(iValue.ToString() + " ");
            iValue = cGlobalInfo.CurrentScreening.Columns;
            stream.Write(iValue.ToString() + "\r\n");

            for (int Col = 0; Col < cGlobalInfo.CurrentScreening.Columns; Col++)
            {
                for (int Row = 0; Row < cGlobalInfo.CurrentScreening.Rows; Row++)
                {
                    for (int SeqPos = 0; SeqPos < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; SeqPos++)
                    {
                        cPlate CurrentPlate = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(SeqPos);
                        cWell TmpWell = CurrentPlate.GetWell(Col, Row, false);
                        double Value = -1;
                        if (TmpWell != null)
                            Value = TmpWell.GetAverageValuesList(false)[0][comboBoxDescriptorToDisplay.SelectedIndex];
                        stream.Write(Value.ToString() + "\t");

                    }
                    stream.Write("\r\n");
                }
            }
            stream.Dispose();
        }

        private void appendAssayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            OpenFileDialog CurrOpenFileDialog = new OpenFileDialog();

            CurrOpenFileDialog.Filter = "csv files (*.csv)|*.csv";
            CurrOpenFileDialog.Multiselect = true;
            DialogResult Res = CurrOpenFileDialog.ShowDialog();
            if (Res != DialogResult.OK) return;

            if (CurrOpenFileDialog.FileNames == null) return;

            //if (CurrOpenFileDialog.FileNames[0].Remove(0, CurrOpenFileDialog.FileNames[0].Length - 4) == ".mtr") LoadMTRAssay(CurrOpenFileDialog);
            //if (CurrOpenFileDialog.FileNames[0].Remove(0, CurrOpenFileDialog.FileNames[0].Length - 4) == ".txt") LoadTXTAssay(CurrOpenFileDialog);
            if (CurrOpenFileDialog.FileNames[0].Remove(0, CurrOpenFileDialog.FileNames[0].Length - 4) == ".csv") LoadCSVAssay(CurrOpenFileDialog.FileNames, true);
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog CurrOpenFileDialog = new OpenFileDialog();
            CurrOpenFileDialog.Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*.txt| All files (*.*)|*.*"; //|mtr files (*.mtr)|*.mtr
            CurrOpenFileDialog.Multiselect = true;

            DialogResult Res = CurrOpenFileDialog.ShowDialog();
            if (Res != DialogResult.OK) return;

            if (CurrOpenFileDialog.FileNames == null) return;

          //  if (CurrOpenFileDialog.FileNames[0].Remove(0, CurrOpenFileDialog.FileNames[0].Length - 4) == ".mtr") LoadMTRAssay(CurrOpenFileDialog);
          //  if (CurrOpenFileDialog.FileNames[0].Remove(0, CurrOpenFileDialog.FileNames[0].Length - 4) == ".txt") LoadTXTAssay(CurrOpenFileDialog);
          //  if (CurrOpenFileDialog.FileNames[0].Remove(0, CurrOpenFileDialog.FileNames[0].Length - 4) == ".csv")
            {
                FormForImportExcel CSVFeedBackWindow = LoadCSVAssay(CurrOpenFileDialog.FileNames, false);
                if (CSVFeedBackWindow == null) return;
                if (CSVFeedBackWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
                ProcessOK(CSVFeedBackWindow);

                UpdateUIAfterLoading();// LoadCSVAssay(CurrOpenFileDialog.FileNames, false);
            }


        }

        private void ImportFiles(string[] FileNames)
        {
            if (IsFileUsed(FileNames[0]))
            {
                MessageBox.Show("File currently used by another application.\n", "Loading error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool ResultLoading = false;

            if (FileNames[0].Remove(0, FileNames[0].Length - 4) == ".mtr")
            {
                LoadMTRAssay(FileNames);
                ResultLoading = true;
            }
            if (FileNames[0].Remove(0, FileNames[0].Length - 4) == ".txt")
            {
                LoadTXTAssay(FileNames);
                ResultLoading = true;
            }
            //if ((CurrOpenFileDialog.FileNames[0].Remove(0, CurrOpenFileDialog.FileNames[0].Length - 4) == ".csv") || (CurrOpenFileDialog.FileNames[0].Remove(0, CurrOpenFileDialog.FileNames[0].Length - 4) == ".CSV"))
            else
            {
                FormForImportExcel CSVFeedBackWindow = LoadCSVAssay(FileNames, false);
                if (CSVFeedBackWindow == null) return;
                if (CSVFeedBackWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
                ProcessOK(CSVFeedBackWindow);
            }
            
            UpdateUIAfterLoading();
        }





        private void ProcessOK(FormForImportExcel CSVFeedBackWindow)
        {
            int NumPlateName = 0;
            int NumRow = 0;
            int NumCol = 0;
            int NumWellPos = 0;
            int NumLocusID = 0;
            int NumConcentration = 0;
            int NumName = 0;
            int NumInfo = 0;
            int NumClass = 0;

            int numDescritpor = 0;

            for (int i = 0; i < CSVFeedBackWindow.dataGridViewForImport.Rows.Count; i++)
            {
                string CurrentVal = CSVFeedBackWindow.dataGridViewForImport.Rows[i].Cells[2].Value.ToString();
                if ((CurrentVal == "Plate name") && ((bool)CSVFeedBackWindow.dataGridViewForImport.Rows[i].Cells[1].Value))
                    NumPlateName++;
                if ((CurrentVal == "Row") && ((bool)CSVFeedBackWindow.dataGridViewForImport.Rows[i].Cells[1].Value))
                    NumRow++;
                if ((CurrentVal == "Column") && ((bool)CSVFeedBackWindow.dataGridViewForImport.Rows[i].Cells[1].Value))
                    NumCol++;
                if ((CurrentVal == "Well position") && ((bool)CSVFeedBackWindow.dataGridViewForImport.Rows[i].Cells[1].Value))
                    NumWellPos++;
                if ((CurrentVal == "Locus ID") && ((bool)CSVFeedBackWindow.dataGridViewForImport.Rows[i].Cells[1].Value))
                    NumLocusID++;
                if ((CurrentVal == "Concentration") && ((bool)CSVFeedBackWindow.dataGridViewForImport.Rows[i].Cells[1].Value))
                    NumConcentration++;
                if ((CurrentVal == "Name") && ((bool)CSVFeedBackWindow.dataGridViewForImport.Rows[i].Cells[1].Value))
                    NumName++;
                if ((CurrentVal == "Info") && ((bool)CSVFeedBackWindow.dataGridViewForImport.Rows[i].Cells[1].Value))
                    NumInfo++;
                if ((CurrentVal == "Class") && ((bool)CSVFeedBackWindow.dataGridViewForImport.Rows[i].Cells[1].Value))
                    NumClass++;
                if ((CurrentVal == "Descriptor") && ((bool)CSVFeedBackWindow.dataGridViewForImport.Rows[i].Cells[1].Value))
                    numDescritpor++;
            }

            if (NumPlateName != 1)
            {
                FormOptionsForPlateName FormForPlateName = new FormOptionsForPlateName();
                if (FormForPlateName.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

                CSVFeedBackWindow.NoName = true;
                CSVFeedBackWindow.IsParentFolder = FormForPlateName.radioButtonNameFromFolder.Checked;

            }
            if ((NumRow != 1) && ((CSVFeedBackWindow.ModeWell == 1)||(CSVFeedBackWindow.ModeWell == 3) ))
            {
                MessageBox.Show("One and only one \"Row\" has to be selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((NumCol != 1) && ((CSVFeedBackWindow.ModeWell == 1) || (CSVFeedBackWindow.ModeWell == 3)))
            {
                MessageBox.Show("One and only one \"Column\" has to be selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((NumWellPos != 1) && (CSVFeedBackWindow.ModeWell == 2))
            {
                MessageBox.Show("One and only one \"Well position\" has to be selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (NumName > 1)
            {
                MessageBox.Show("You cannot select more than one \"Name\" !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (NumClass > 1)
            {
                MessageBox.Show("You cannot select more than one \"Class\" !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (NumInfo > 1)
            {
                MessageBox.Show("You cannot select more than one \"Info\" !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (NumLocusID > 1)
            {
                MessageBox.Show("You cannot select more than one \"Locus ID\" !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (NumConcentration > 1)
            {
                MessageBox.Show("You cannot select more than one \"Concentration\" !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((numDescritpor < 1) && (CSVFeedBackWindow.IsImportCSV))
            {
                MessageBox.Show("You need to select at least one \"Descriptor\" !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CSVFeedBackWindow.IsImportCSV)
                LoadingProcedureForCSVImport(CSVFeedBackWindow);
            else
                LoadingProcedure(CSVFeedBackWindow);

            //this.Dispose();       



        }
        #endregion

        #region CSV functions
        private class CsvRow : List<string>
        {
            public string LineText { get; set; }
        }

        private class CsvFileReader : StreamReader
        {

            public char Separator = ',';

            public CsvFileReader(Stream stream)
                : base(stream)
            {
            }

            public CsvFileReader(string filename)
                : base(filename)
            {
            }

            /// <summary>
            /// Reads a row of data from a CSV file
            /// </summary>
            /// <param name="row"></param>
            /// <returns></returns>
            public bool ReadRow(CsvRow row)
            {

                row.LineText = ReadLine();
                if (String.IsNullOrEmpty(row.LineText))
                    return false;

                int pos = 0;
                int rows = 0;

                while (pos < row.LineText.Length)
                {
                    string value;

                    // Special handling for quoted field
                    if (row.LineText[pos] == '"')
                    {
                        // Skip initial quote
                        pos++;

                        // Parse quoted value
                        int start = pos;
                        while (pos < row.LineText.Length)
                        {
                            // Test for quote character
                            if (row.LineText[pos] == '"')
                            {
                                // Found one
                                pos++;

                                // If two quotes together, keep one
                                // Otherwise, indicates end of value
                                if (pos >= row.LineText.Length || row.LineText[pos] != '"')
                                {
                                    pos--;
                                    break;
                                }
                            }
                            pos++;
                        }
                        value = row.LineText.Substring(start, pos - start);
                        value = value.Replace("\"\"", "\"");
                    }
                    else
                    {
                        // Parse unquoted value
                        int start = pos;
                        while (pos < row.LineText.Length && row.LineText[pos] != Separator)
                            pos++;
                        value = row.LineText.Substring(start, pos - start);
                    }

                    // Add field to list
                    if (rows < row.Count)
                        row[rows] = value;
                    else
                        row.Add(value);
                    rows++;

                    // Eat up to and including next comma
                    while (pos < row.LineText.Length && row.LineText[pos] != Separator)
                        pos++;
                    if (pos < row.LineText.Length)
                        pos++;
                }
                // Delete any unused items
                while (row.Count > rows)
                    row.RemoveAt(rows);

                // Return true if any columns read
                //return (row.Count > 0);
                return true;
            }
        }

        public int[] ConvertPosition(string PosString)
        {
            int[] Pos = new int[2];

            Pos[1] = Convert.ToInt16(PosString.ToUpper()[0]) - 64;
            string PosY = PosString.Remove(0, 1);
            bool IsTrue = int.TryParse(PosY, out Pos[0]);

            if (!IsTrue) return null;

            return Pos;
        }

        public int[] ConvertPositionFromGE(string PosString)
        {
            string[] Sep = new string[1];
            Sep[0] = " - ";
            string[] ResList = PosString.Split(Sep, StringSplitOptions.None);

            if (ResList.Length != 2) return null;

            int[] Pos = new int[2];

            Pos[1] = Convert.ToInt16(ResList[0].ToUpper()[0]) - 64;
           // string PosY = PosString.Remove(0, 1);
            bool IsTrue = int.TryParse(ResList[1], out Pos[0]);

            if (!IsTrue) return null;

            return Pos;
        }

        public string ConvertPosition(int PosX, int PosY)
        {
            char character = (char)(PosY + 64);
            string ToReturn = character.ToString() + PosX.ToString("00");
            return ToReturn;
        }

        private FormForImportExcel LoadCSVAssay(string[] FileNames, bool IsAppend)
        {
           
            if (cGlobalInfo.CurrentScreening == null) IsAppend = false;

            FormInfoForFileImporter InfoForFileImporter = new FormInfoForFileImporter(FileNames[0]);
            if (InfoForFileImporter.ShowDialog() != System.Windows.Forms.DialogResult.OK) return null;

            FormForImportExcel FromExcel = new FormForImportExcel();
           

        //    char Separator = ',';
            if (InfoForFileImporter.radioButtonSemiColon.Checked) FromExcel.Separator = ';';
            else if (InfoForFileImporter.radioButtonSpace.Checked) FromExcel.Separator = ' ';
            else if (InfoForFileImporter.radioButtonTab.Checked) FromExcel.Separator = '\t';

            PathNames = FileNames;
            if (IsAppend == false)
            {
                if (cGlobalInfo.CurrentScreening != null) cGlobalInfo.CurrentScreening.Close3DView();
                cGlobalInfo.CurrentScreening = new cScreening("CSV imported Screening");

            }
            //CompleteScreening = new cScreening("MTR imported Screening", GlobalInfo);
            StartingUpDateUI();
            //PathNames = CurrOpenFileDialog.FileNames;

            if (PathNames == null) return null;
            // Window form creation
            
            FromExcel.Text += " - " + PathNames[0];
            if (IsAppend)
            {
                FromExcel.numericUpDownColumns.Value = (decimal)cGlobalInfo.CurrentScreening.Columns;
                FromExcel.numericUpDownColumns.ReadOnly = true;
                FromExcel.numericUpDownRows.Value = (decimal)cGlobalInfo.CurrentScreening.Rows;
                FromExcel.numericUpDownRows.ReadOnly = true;
            }

            FromExcel.IsImportCSV = true;

           
            //int Mode = 2;
            FromExcel.ModeWell = 2;


            if (InfoForFileImporter.radioButtonWellPosModeA02.Checked)
                FromExcel.ModeWell = 2;
            else if (InfoForFileImporter.radioButtonWellPosModeA_02.Checked)
                FromExcel.ModeWell = 1;      
            else if (InfoForFileImporter.radioButtonWellPosModeA_2.Checked)
                FromExcel.ModeWell = 4; // GE
            else FromExcel.ModeWell = 3; //(InfoForFileImporter.radioButtonWellPosMode1_2.Checked)

            //if (cGlobalInfo.OptionsWindow.radioButtonWellPosModeSingle.Checked) Mode = 1;
            CsvFileReader CSVsr = new CsvFileReader(PathNames[0]);
            CSVsr.Separator = FromExcel.Separator;

            for (int i = 0; i < InfoForFileImporter.numericUpDownHeaderSize.Value; i++)
            {
                CsvRow TNames = new CsvRow();
                CSVsr.ReadRow(TNames);
            }

            CsvRow Names = new CsvRow();
            if (!CSVsr.ReadRow(Names))
            {
                CSVsr.Close();
                return null;
            }

            int NumPreview = (int)InfoForFileImporter.numericUpDownPreviewSize.Value;
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
            if ((FromExcel.ModeWell == 1) || (FromExcel.ModeWell == 3))
                columnType.DataSource = new string[] { "Plate name", "Column", "Row", "Class", "Name", "Locus ID", "Concentration", "Info", "Descriptor" };
            else
                columnType.DataSource = new string[] { "Plate name", "Well position", "Class", "Name", "Locus ID", "Concentration", "Info", "Descriptor" };
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
                else if ((i == 2) && ((FromExcel.ModeWell == 1) || (FromExcel.ModeWell == 3))) FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = true;
                else FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = false;

                if (i == 0)
                {
                    FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = "Plate name";
                }
                else if (i == 1)
                {
                    if ((FromExcel.ModeWell == 1) || (FromExcel.ModeWell == 3))
                        FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = "Column";
                    else
                        FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = "Well position";
                }
                else if (i == 2)
                {
                    if ((FromExcel.ModeWell == 1) || (FromExcel.ModeWell == 3))
                        FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = "Row";
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
            //   FromExcel.dataGridViewForImport.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewForImport_MouseClick);

            FromExcel.CurrentScreen = cGlobalInfo.CurrentScreening;
            FromExcel.thisHCSAnalyzer = this;
            FromExcel.IsAppend = IsAppend;
            FromExcel.HeaderSize = (int)InfoForFileImporter.numericUpDownHeaderSize.Value;


            return FromExcel;

        }

        public void LoadingProcedureForCSVImport(FormForImportExcel FromExcel)
        {
            CsvFileReader CSVsr = new CsvFileReader(PathNames[0]);
            CSVsr.Separator = FromExcel.Separator;

            for (int i = 0; i < FromExcel.HeaderSize; i++)
            {
                CsvRow TNames = new CsvRow();
                CSVsr.ReadRow(TNames);
            }
            CsvRow OriginalNames = new CsvRow();

            if (!CSVsr.ReadRow(OriginalNames))
            {
                CSVsr.Close();
                return;
            }

            int ColSelectedForName = GetColIdxFor("name", FromExcel);
            int ColLocusID = GetColIdxFor("Locus ID", FromExcel);
            int ColConcentration = GetColIdxFor("Concentration", FromExcel);
            int ColInfo = GetColIdxFor("Info", FromExcel);
            int ColClass = GetColIdxFor("Class", FromExcel);
            int ColPlateName = GetColIdxFor("Plate name", FromExcel);
            int ColCol = GetColIdxFor("Column", FromExcel);
            int ColRow = GetColIdxFor("Row", FromExcel);
            int ColWellPos = GetColIdxFor("Well position", FromExcel);
            int[] ColsForDescriptors = GetColsIdxFor("Descriptor", FromExcel);

            int WellLoaded = 0;
            int FailToLoad = 0;

            if (!FromExcel.IsAppend)
            {
                cGlobalInfo.CurrentScreening.Columns = (int)FromExcel.numericUpDownColumns.Value;
                cGlobalInfo.CurrentScreening.Rows = (int)FromExcel.numericUpDownRows.Value;
                cGlobalInfo.CurrentScreening.ListDescriptors.Clean();
            }
            int ShiftIdx = cGlobalInfo.CurrentScreening.ListDescriptors.Count;


            for (int IdxFile = 0; IdxFile < PathNames.Length; IdxFile++)
            {
                string CurrentFileName = PathNames[IdxFile];

                CSVsr = new CsvFileReader(CurrentFileName);
                CSVsr.Separator = FromExcel.Separator;
                CsvRow Names = new CsvRow();

                for (int i = 0; i < FromExcel.HeaderSize; i++)
                {
                    CsvRow TNames = new CsvRow();
                    CSVsr.ReadRow(TNames);
                }

                #region Check the validity of the header
                if (!CSVsr.ReadRow(Names))
                {
                    CSVsr.Close();
                    cGlobalInfo.ConsoleWriteLine(CurrentFileName + ": file opening failed.");
                    goto NEXTFILE;
                }

                if (Names.Count != OriginalNames.Count)
                {
                    CSVsr.Close();
                    cGlobalInfo.ConsoleWriteLine(CurrentFileName + ": Header inconsistent.");
                    goto NEXTFILE;
                }
                for (int IdxName = 0; IdxName < Names.Count; IdxName++)
                {

                    if (Names[IdxName] != OriginalNames[IdxName])
                    {
                        CSVsr.Close();
                        cGlobalInfo.ConsoleWriteLine(CurrentFileName + ": Header inconsistent.");
                        goto NEXTFILE;
                    }
                }
                #endregion

                if (!FromExcel.IsAppend)
                {
                    for (int idxDesc = 0; idxDesc < ColsForDescriptors.Length; idxDesc++)
                        cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(new cDescriptorType(Names[ColsForDescriptors[idxDesc]], true, 1));

                    FromExcel.IsAppend = true;

                }

                int ConvertedNaNValue = 0;
                while (CSVsr.EndOfStream != true)
                {
                NEXT: ;
                    CsvRow CurrentDesc = new CsvRow();
                    if (CSVsr.ReadRow(CurrentDesc) == false) break;

                    string PlateName = "";
                    if (FromExcel.NoName)    // no name defined, let's use the file name instead.
                    {
                        if (FromExcel.IsParentFolder)
                        {
                            string[] Sep = new string[1];
                            Sep[0] = "\\";
                            string[] SplittedStrings = CurrentFileName.Split(Sep, StringSplitOptions.None);

                            PlateName = SplittedStrings[SplittedStrings.Length-2];
                        }
                        else
                        {
                            PlateName = CurrentFileName;
                        }

                    }
                    else
                        PlateName = CurrentDesc[ColPlateName];

                    //return;
                    // check if the plate exist already
                    cPlate CurrentPlate = cGlobalInfo.CurrentScreening.GetPlateIfNameIsContainIn(PlateName);
                    if (CurrentPlate == null)
                    {
                        CurrentPlate = new cPlate( PlateName, cGlobalInfo.CurrentScreening);
                        cGlobalInfo.CurrentScreening.AddPlate(CurrentPlate);
                    }

                    int[] Pos = new int[2];
                    if (FromExcel.ModeWell == 2)
                    {
                        if (CurrentDesc[ColWellPos] == "") goto NEXTLOOP;
                        Pos = ConvertPosition(CurrentDesc[ColWellPos]);
                        if (Pos == null)
                        {
                            if (MessageBox.Show("Error in converting the current well position.\nGo to Edit->Options->Import-Export->Well Position Mode to fix this.\nDo you want continue ?", "Loading error !", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.No)
                            {
                                CSVsr.Close();
                                return;
                            }
                            else
                                goto NEXTLOOP;
                        }
                    }
                    else if (FromExcel.ModeWell == 4)
                    {
                        if (CurrentDesc[ColWellPos] == "") goto NEXTLOOP;
                        Pos = ConvertPositionFromGE(CurrentDesc[ColWellPos]);
                        if (Pos == null)
                        {
                            if (MessageBox.Show("Error in converting the current well position.\nGo to Edit->Options->Import-Export->Well Position Mode to fix this.\nDo you want continue ?", "Loading error !", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.No)
                            {
                                CSVsr.Close();
                                return;
                            }
                            else
                                goto NEXTLOOP;
                        }
                    }
                    else if (FromExcel.ModeWell == 1)
                    {
                        if (int.TryParse(CurrentDesc[ColCol], out Pos[0]) == false)
                            goto NEXTLOOP;

                        Pos[1]= Convert.ToInt32(CurrentDesc[ColRow].ToUpper()[0])-64;
                    }
                    else
                    {
                        if (int.TryParse(CurrentDesc[ColCol], out Pos[0]) == false)
                            goto NEXTLOOP;
                        if (int.TryParse(CurrentDesc[ColRow], out Pos[1]) == false)
                            goto NEXTLOOP;
                    }


                    cListSignature LDesc = new cListSignature();
                    for (int idxDesc = 0; idxDesc < ColsForDescriptors.Length; idxDesc++)
                    {
                        double Value;
                        if (ColsForDescriptors[idxDesc] >= CurrentDesc.Count)
                        {
                            FailToLoad++;
                            goto NEXTLOOP;
                        }

                        cSignature CurrentDescriptor;

                        if (double.TryParse(CurrentDesc[ColsForDescriptors[idxDesc]], NumberStyles.Any, CultureInfo.InvariantCulture/*.CreateSpecificCulture("en-US")*/, out Value))
                        {
                            //if(double.IsNaN(Value))
                           // {
                           //     else
                                //{
                                //    FailToLoad++;
                                //    goto NEXTLOOP;   
                                //}
                           // }
                            CurrentDescriptor = new cSignature(Value, cGlobalInfo.CurrentScreening.ListDescriptors[idxDesc/* + ShiftIdx*/], cGlobalInfo.CurrentScreening);
                            LDesc.Add(CurrentDescriptor);
                        }
                        else
                        {
                            if (FromExcel.checkBoxConvertNaNTo0.Checked)
                            {
                                Value = 0;
                                CurrentDescriptor = new cSignature(Value, cGlobalInfo.CurrentScreening.ListDescriptors[idxDesc/* + ShiftIdx*/], cGlobalInfo.CurrentScreening);
                                LDesc.Add(CurrentDescriptor);

                                ConvertedNaNValue++;

                            }
                            else
                            {
                                FailToLoad++;
                                goto NEXTLOOP;
                            }
                        }
                    }
                    if ((Pos[0] > cGlobalInfo.CurrentScreening.Columns) || (Pos[1] > cGlobalInfo.CurrentScreening.Rows))
                    {
                        if (MessageBox.Show("Well position exceeds plate dimensions.\nDo you want continue ?", "Loading error !", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.No)
                        {
                            CSVsr.Close();
                            return;
                        }
                        else
                            goto NEXTLOOP;
                    }


                    cWell CurrentWell = new cWell(LDesc, Pos[0], Pos[1], cGlobalInfo.CurrentScreening, CurrentPlate);


                    CurrentPlate.AddWell(CurrentWell);
                    WellLoaded++;

                    if ((ColSelectedForName != -1) && (ColSelectedForName < CurrentDesc.Count))
                    {
                        CurrentWell.ListProperties.UpdateValueByName("Compound Name", CurrentDesc[ColSelectedForName]);
                    }

                    if (ColLocusID != -1)
                    {
                        double CurrentValue;

                        if (!double.TryParse(CurrentDesc[ColLocusID], out CurrentValue))
                            goto NEXTSTEP;

                        CurrentWell.ListProperties.UpdateValueByName("Locus ID",(int)CurrentValue);

                    }
                    if (ColConcentration != -1)
                    {
                        double CurrentValue;

                        if (!double.TryParse(CurrentDesc[ColConcentration], out CurrentValue))
                            goto NEXTSTEP;
                        CurrentWell.ListProperties.UpdateValueByName("Concentration", CurrentValue);
                        //CurrentWell.Concentration = CurrentValue;
                    }
                NEXTSTEP: ;

                    if ((ColInfo != -1) && (ColInfo < CurrentDesc.Count))
                        CurrentWell.Info = CurrentDesc[ColInfo];

                    if (ColClass != -1)
                    {
                        int CurrentValue;
                        if (!int.TryParse(CurrentDesc[ColClass], out CurrentValue))
                            goto NEXTLOOP;
                        CurrentWell.SetClass(CurrentValue);
                    }

                NEXTLOOP: ;

                }
            NEXTFILE: ;
                CSVsr.Close();
            }
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            FromExcel.Dispose();

            MessageBox.Show("CSV file loaded:\n" + WellLoaded + " well(s) loaded\n" + FailToLoad + " well(s) rejected.", "Process finished !", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.toolStripcomboBoxPlateList.Items.Clear();
            for (int IdxPlate = 0; IdxPlate < cGlobalInfo.CurrentScreening.GetNumberOfOriginalPlates(); IdxPlate++)
            {
                string Name = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(IdxPlate).GetName();
                this.toolStripcomboBoxPlateList.Items.Add(Name);
                PlateListWindow.listBoxPlateNameToProcess.Items.Add(Name);
                PlateListWindow.listBoxAvaliableListPlates.Items.Add(Name);
            }


            UpdateUIAfterLoading();
            //    CompleteScreening.CurrentDisplayPlateIdx = 0;
            cGlobalInfo.CurrentScreening.SetSelectionType(comboBoxClass.SelectedIndex - 1);

            // toolStripcomboBoxPlateList.SelectedIndex = 0;

            cGlobalInfo.CurrentScreening.ListDescriptors.SetCurrentSelectedDescriptor(0);
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors[0], true);
            return;
        }

        #endregion

        #region Link To Data

        //   FormForImportExcel FromExcel;
        string[] PathNames;

        //private void dataGridViewForImport_MouseClick(object sender, MouseEventArgs e)
        //{
        //    if (e.Clicks != 1) return;
        //    if (e.Button == MouseButtons.Right)
        //    {
        //        ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
        //        ToolStripMenuItem ToolStripMenuItem_Info = new ToolStripMenuItem("Select All");

        //        ToolStripMenuItem ToolStripMenuItem_Kegg = new ToolStripMenuItem("Unselect All");
        //        contextMenuStrip.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem_Info, ToolStripMenuItem_Kegg });

        //        contextMenuStrip.Show(Control.MousePosition);

        //        ToolStripMenuItem_Info.Click += new System.EventHandler(this.SelectAll);
        //        ToolStripMenuItem_Kegg.Click += new System.EventHandler(this.UnselectAll);

        //    }

        //}

        //private void SelectAll(object sender, EventArgs e)
        //{
        //    //for (int i = 0; i < FromExcel.dataGridViewForImport.Rows.Count; i++)
        //    //    FromExcel.dataGridViewForImport.Rows[i].Cells[1].Value = true;
        //}

        //private void UnselectAll(object sender, EventArgs e)
        //{
        //    //for (int i = 0; i < FromExcel.dataGridViewForImport.Rows.Count; i++)
        //    //    FromExcel.dataGridViewForImport.Rows[i].Cells[1].Value = false;
        //}

        private void linkToolStripMenuItem_Click(object sender, EventArgs e)
        {      
            if (PathNames == null) return;

            cCSVToTable CSVT = new cCSVToTable();
            CSVT.AddAsObject = true;
            CSVT.IsDisplayUIForFilePath = true;
            CSVT.IsContainRowNames = true;
            CSVT.IsContainColumnHeaders = true;
            CSVT.Run();

            cExtendedTable ET = CSVT.GetOutPut();

            int NumWellUpdated = 0;

            for (int i = 0; i < ET.ListRowNames.Count; i++)
            {
                string PlateName = ET.ListRowNames[i];

                string WellPos = (string)(ET[0].ListTags[i]);
                string CpdName = (string)(ET[1].ListTags[i]);

                // first retrieve the plate
                foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                {
                    if (TmpPlate.GetName() != PlateName) continue;

                    foreach (cWell TmpWell in TmpPlate.ListActiveWells)
                    {
                        if (TmpWell.GetPos() != WellPos) continue;

                        TmpWell.ListProperties.UpdateValueByName("Compound Name", CpdName);
                        NumWellUpdated++;
                    }
                }
            }

            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
            MessageBox.Show(NumWellUpdated + " over "+ cGlobalInfo.CurrentScreening.ListPlatesActive.GetListActiveWells().Count +" wells have been updated !", "Process over !", MessageBoxButtons.OK, MessageBoxIcon.Information);
  
            return;

            //if (CompleteScreening == null) return;

            //OpenFileDialog CurrOpenFileDialog = new OpenFileDialog();
            //CurrOpenFileDialog.Filter = "csv files (*.csv)|*.csv";
            //System.Windows.Forms.DialogResult Res = CurrOpenFileDialog.ShowDialog();
            //if (Res != System.Windows.Forms.DialogResult.OK) return;
            //PathNames = CurrOpenFileDialog.FileNames;

            //if (PathNames == null) return;

            //// Window form creation
            //FromExcel = new FormForImportExcel();
            //FromExcel.numericUpDownColumns.Value = (decimal)CompleteScreening.Columns;
            //FromExcel.numericUpDownColumns.ReadOnly = true;
            //FromExcel.numericUpDownRows.Value = (decimal)CompleteScreening.Rows;
            //FromExcel.numericUpDownRows.ReadOnly = true;

            //int Mode = 2;
            //if (GlobalInfo.OptionsWindow.radioButtonWellPosModeSingle.Checked) Mode = 1;



            //CsvFileReader CSVsr = new CsvFileReader(PathNames[0]);

            //CsvRow Names = new CsvRow();
            //if (!CSVsr.ReadRow(Names))
            //{
            //    CSVsr.Close();
            //    return;
            //}

            //int NumPreview = 4;
            //List<CsvRow> LCSVRow = new List<CsvRow>();
            //for (int Idx = 0; Idx < NumPreview; Idx++)
            //{
            //    CsvRow TNames = new CsvRow();
            //    if (!CSVsr.ReadRow(TNames))
            //    {
            //        CSVsr.Close();
            //        return;
            //    }
            //    LCSVRow.Add(TNames);
            //}

            ////for (int i = 0; i < Names.Count; i++)
            ////{
            ////    FromExcel.comboBoxClass.Items.Add(Names[i]);
            ////    FromExcel.comboBoxLocusID.Items.Add(Names[i]);
            ////    FromExcel.comboBoxInfo.Items.Add(Names[i]);
            ////}

            //DataGridViewColumn ColName = new DataGridViewColumn();
            //FromExcel.dataGridViewForImport.Columns.Add("Data Name", "Data Name");

            //DataGridViewCheckBoxColumn columnSelection = new DataGridViewCheckBoxColumn();
            //columnSelection.Name = "Selection";
            //FromExcel.dataGridViewForImport.Columns.Add(columnSelection);

            //DataGridViewComboBoxColumn columnType = new DataGridViewComboBoxColumn();
            //if (CompleteScreening.GlobalInfo.OptionsWindow.radioButtonWellPosModeDouble.Checked)
            //    columnType.DataSource = new string[] { "Plate name", "Column", "Row", "Class", "Name", "Locus ID", "Concentration", "Info" };
            //else
            //    columnType.DataSource = new string[] { "Plate name", "Well position", "Class", "Name", "Locus ID", "Concentration", "Info" };
            //columnType.Name = "Type";
            //FromExcel.dataGridViewForImport.Columns.Add(columnType);

            //for (int i = 0; i < LCSVRow.Count; i++)
            //{
            //    DataGridViewColumn NewCol = new DataGridViewColumn();
            //    NewCol.ReadOnly = true;
            //    FromExcel.dataGridViewForImport.Columns.Add("Readout " + i, "Readout " + i);
            //}


            //for (int i = 0; i < Names.Count; i++)
            //{
            //    int IdxRow = 0;
            //    FromExcel.dataGridViewForImport.Rows.Add();
            //    FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = Names[i];

            //    if (i == 0) FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = true;
            //    else if (i == 1) FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = true;
            //    else if ((i == 2) && (CompleteScreening.GlobalInfo.OptionsWindow.radioButtonWellPosModeDouble.Checked)) FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = true;
            //    else FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = false;

            //    if (i == 0)
            //    {
            //        FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = "Plate name";
            //    }
            //    else if (i == 1)
            //    {
            //        if (CompleteScreening.GlobalInfo.OptionsWindow.radioButtonWellPosModeDouble.Checked)
            //        {
            //            FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = "Column";
            //        }
            //        else
            //        {
            //            FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = "Well position";
            //        }
            //    }
            //    else if (i == 2)
            //    {
            //        if (CompleteScreening.GlobalInfo.OptionsWindow.radioButtonWellPosModeDouble.Checked)
            //        {
            //            FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = "Row";
            //        }
            //        else
            //            FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = "Name";
            //    }
            //    else
            //    {
            //        FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow++].Value = "Name";
            //    }

            //    for (int j = 0; j < LCSVRow.Count; j++)
            //        FromExcel.dataGridViewForImport.Rows[i].Cells[IdxRow + j].Value = LCSVRow[j][i].ToString();
            //}

            //FromExcel.dataGridViewForImport.Update();
            //FromExcel.dataGridViewForImport.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewForImport_MouseClick);
            //FromExcel.CurrentScreen = CompleteScreening;
            //FromExcel.thisHCSAnalyzer = this;

            //FromExcel.ShowDialog();
        }

        private int GetColIdxFor(string StringToBeDetected, FormForImportExcel FromExcel)
        {
            int Pos = -1;

            for (int i = 0; i < FromExcel.dataGridViewForImport.Rows.Count; i++)
            {
                string CurrentVal = FromExcel.dataGridViewForImport.Rows[i].Cells[2].Value.ToString();
                bool IsSelected = (bool)FromExcel.dataGridViewForImport.Rows[i].Cells[1].Value;

                if ((CurrentVal == StringToBeDetected) && (IsSelected == true)) Pos = i;
            }

            return Pos;
        }

        private int[] GetColsIdxFor(string StringToBeDetected, FormForImportExcel FromExcel)
        {
            List<int> Pos = new List<int>();
            for (int i = 0; i < FromExcel.dataGridViewForImport.Rows.Count; i++)
            {
                string CurrentVal = FromExcel.dataGridViewForImport.Rows[i].Cells[2].Value.ToString();
                bool IsSelected = (bool)FromExcel.dataGridViewForImport.Rows[i].Cells[1].Value;

                if ((CurrentVal == StringToBeDetected) && (IsSelected == true))
                    Pos.Add(i);
            }

            return Pos.ToArray();
        }

        public void LoadingProcedure(FormForImportExcel FromExcel)
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
            int ColLocusID = GetColIdxFor("Locus ID", FromExcel);
            int ColConcentration = GetColIdxFor("Concentration", FromExcel);
            int ColInfo = GetColIdxFor("Info", FromExcel);
            int ColClass = GetColIdxFor("Class", FromExcel);
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

                if (ColLocusID != -1)
                {
                    double CurrentValue;

                    if (!double.TryParse(CurrentDesc[ColLocusID], out CurrentValue))
                        goto NEXTSTEP;

                    CurrentWell.ListProperties.UpdateValueByName("Locus ID",(int)CurrentValue);

                }
                if (ColConcentration != -1)
                {
                    double CurrentValue;

                    if (!double.TryParse(CurrentDesc[ColConcentration], out CurrentValue))
                        goto NEXTSTEP;


                    CurrentWell.ListProperties.UpdateValueByName("Concentration",CurrentValue);

                }
            NEXTSTEP: ;

                if (ColInfo != -1)
                    CurrentWell.Info = CurrentDesc[ColInfo];

                if (ColClass != -1)
                {
                    int CurrentValue;
                    if (!int.TryParse(CurrentDesc[ColClass], out CurrentValue))
                        goto NEXTLOOP;
                    CurrentWell.SetClass(CurrentValue);
                }

            NEXTLOOP: ;

            }

            CSVsr.Close();

            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);

            MessageBox.Show("File loaded", "Process finished !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        #endregion

        #region export to CSV
        private void toARFFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            SaveFileDialog CurrSaveFileDialog = new SaveFileDialog();
            CurrSaveFileDialog.Filter = "arff files (*.arff)|*.arff";
            System.Windows.Forms.DialogResult Res = CurrSaveFileDialog.ShowDialog();
            if (Res != System.Windows.Forms.DialogResult.OK) return;
            string PathName = CurrSaveFileDialog.FileName;

            if (PathName == "") return;

            cInfoClass InfoClass = cGlobalInfo.CurrentScreening.GetNumberOfClassesBut(-1);
            Instances insts = cGlobalInfo.CurrentScreening.CreateInstancesWithClasses(InfoClass, -1);
            ArffSaver saver = new ArffSaver();
            CSVSaver savercsv = new CSVSaver();
            saver.setInstances(insts);
            saver.setFile(new java.io.File(PathName));
            saver.writeBatch();

        }

        public string CheckAndCorrectFilemName(string FileName, bool IsWarn)
        {
            string[] ListChr = new string[] { "?", ">", "<", "*", ":", "|", "/", "\\" };

            bool Error = false;
            for (int i = 0; i < ListChr.Length; i++)
            {
                if (FileName.Contains(ListChr[i]))
                {
                    Error = true;
                    break;
                }
            }

            if (Error)
            {
                if (IsWarn) MessageBox.Show("The plate name contains character(s) that cannot be used !\nThey will be replaced by: _", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                for (int i = 0; i < ListChr.Length; i++)
                {
                    string TmpName = FileName.Replace(ListChr[i], "_");
                    FileName = TmpName;
                }
            }

            return FileName;
        }

        private void DataGridfViewToCsV(DataGridView dGV, string filename)
        {
            using (StreamWriter myFile = new StreamWriter(filename, false, Encoding.Default))
            {
                // Export titles:  
                string sHeaders = "";
                for (int j = 0; j < dGV.Columns.Count; j++) { sHeaders = sHeaders.ToString() + dGV.Columns[j].HeaderText + ","; }
                myFile.WriteLine(sHeaders);

                // Export data.  
                for (int i = 0; i < dGV.RowCount - 1; i++)
                {
                    string stLine = "";
                    for (int j = 0; j < dGV.Rows[i].Cells.Count; j++) { stLine = stLine.ToString() + dGV.Rows[i].Cells[j].Value + ","; }
                    myFile.WriteLine(stLine);
                }
                myFile.Close();
            }

        }

        /// <summary>
        /// Save screen to CSV file format
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            //int NumberOfPlates = CompleteScreening.ListPlatesActive.Count;
            //int NumDesc = CompleteScreening.ListDescriptors.Count;

            SaveFileDialog CurrSaveFileDialog = new SaveFileDialog();
            CurrSaveFileDialog.Filter = "csv files (*.csv)|*.csv";
            System.Windows.Forms.DialogResult Res = CurrSaveFileDialog.ShowDialog();
            if (Res != System.Windows.Forms.DialogResult.OK) return;
            string CurrentPathforCSV = CurrSaveFileDialog.FileName;


            DisplayDescriptorsToSave(CurrentPathforCSV);

            MessageBox.Show("CSV file saved !");


        }

        private bool DisplayDescriptorsToSave(string CurrentPathforCSV)
        {
            // Window form creation
            FormSaveScreening FormToSave = new FormSaveScreening();
            FormToSave.Name = "Save to CSV";

            int Mode = 2;
            if (cGlobalInfo.OptionsWindow.radioButtonWellPosModeSingle.Checked) Mode = 1;

            DataGridViewColumn ColName = new DataGridViewColumn();
            FormToSave.dataGridView.Columns.Add("Data Name", "Data Name");

            DataGridViewCheckBoxColumn columnSelection = new DataGridViewCheckBoxColumn();
            columnSelection.Name = "Selection";
            FormToSave.dataGridView.Columns.Add(columnSelection);

            DataGridViewColumn ColExample = new DataGridViewColumn();
            FormToSave.dataGridView.Columns.Add("Well 0", "Well 0");

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Font = new Font(FormToSave.dataGridView.Font, FontStyle.Bold);

            FormToSave.dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;



            int RowIdx = 0;
            FormToSave.dataGridView.Rows.Add();
            FormToSave.dataGridView.Rows[RowIdx].Cells[0].Value = "Plate Name";
            FormToSave.dataGridView.Rows[RowIdx].Cells[1].Value = true;
            FormToSave.dataGridView.Rows[RowIdx++].DefaultCellStyle = style;

            if (Mode == 1)
            {
                FormToSave.dataGridView.Rows.Add();
                FormToSave.dataGridView.Rows[RowIdx].Cells[0].Value = "Well Position";
                FormToSave.dataGridView.Rows[RowIdx].Cells[1].Value = true;
                FormToSave.dataGridView.Rows[RowIdx++].DefaultCellStyle = style;
            }
            else
            {
                FormToSave.dataGridView.Rows.Add();
                FormToSave.dataGridView.Rows[RowIdx].Cells[0].Value = "Column";
                FormToSave.dataGridView.Rows[RowIdx].Cells[1].Value = true;
                FormToSave.dataGridView.Rows[RowIdx++].DefaultCellStyle = style;

                FormToSave.dataGridView.Rows.Add();
                FormToSave.dataGridView.Rows[RowIdx].Cells[0].Value = "Row";
                FormToSave.dataGridView.Rows[RowIdx].Cells[1].Value = true;
                FormToSave.dataGridView.Rows[RowIdx++].DefaultCellStyle = style;
            }


            foreach (var item in cGlobalInfo.CurrentScreening.ListWellPropertyTypes)
            {
                FormToSave.dataGridView.Rows.Add();
                FormToSave.dataGridView.Rows[RowIdx].Cells[0].Value = item.Name;
                FormToSave.dataGridView.Rows[RowIdx].Cells[1].Value = true;
                FormToSave.dataGridView.Rows[RowIdx++].DefaultCellStyle = style;
            }


            int NumDescriptor = cGlobalInfo.CurrentScreening.ListDescriptors.Count;

            for (int PlateIdx = 0; PlateIdx < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; PlateIdx++)
            {
                cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(PlateIdx);

                for (int IdxValue = 0; IdxValue < cGlobalInfo.CurrentScreening.Columns; IdxValue++)
                    for (int IdxValue0 = 0; IdxValue0 < cGlobalInfo.CurrentScreening.Rows; IdxValue0++)
                    {
                        cWell TmpWell = CurrentPlateToProcess.GetWell(IdxValue, IdxValue0, true);
                        if (TmpWell == null) continue;

                        RowIdx = 0;

                        FormToSave.dataGridView.Rows[RowIdx++].Cells[2].Value = CurrentPlateToProcess.GetName();
                        if (Mode == 1)
                        {
                            string PosString = ConvertPosition(TmpWell.GetPosX(), TmpWell.GetPosY());
                            FormToSave.dataGridView.Rows[RowIdx++].Cells[2].Value = PosString;
                        }
                        else
                        {
                            FormToSave.dataGridView.Rows[RowIdx++].Cells[2].Value = TmpWell.GetPosX();
                            FormToSave.dataGridView.Rows[RowIdx++].Cells[2].Value = TmpWell.GetPosY();
                        }

                        foreach (var item in cGlobalInfo.CurrentScreening.ListWellPropertyTypes)
                        {
                            object TmpID = TmpWell.ListProperties.FindValueByName(item.Name);
                            if (TmpID == null)
                                FormToSave.dataGridView.Rows[RowIdx++].Cells[2].Value = "n.a.";
                            else
                                FormToSave.dataGridView.Rows[RowIdx++].Cells[2].Value = TmpID.ToString();
                        }
                        //FormToSave.dataGridView.Rows[RowIdx++].Cells[2].Value = TmpWell.Name;

                        //FormToSave.dataGridView.Rows[RowIdx++].Cells[2].Value = TmpWell.GetCurrentClassIdx();

                        //object TmpID = TmpWell.ListProperties.FindValueByName("Locus ID");
                        //if (TmpID==null)
                        //    FormToSave.dataGridView.Rows[RowIdx++].Cells[2].Value = "n.a.";
                        //else
                        //    FormToSave.dataGridView.Rows[RowIdx++].Cells[2].Value = TmpID.ToString();

                        /*object ConcentrationValue = TmpWell.ListProperties.FindValueByName("Concentration");
                        if (ConcentrationValue == null)
                            FormToSave.dataGridView.Rows[RowIdx++].Cells[2].Value = "";
                        else
                            FormToSave.dataGridView.Rows[RowIdx++].Cells[2].Value = (double)ConcentrationValue;
                        */
                        //FormToSave.dataGridView.Rows[RowIdx++].Cells[2].Value = TmpWell.Info;

                        for (int N = 0; N < NumDescriptor; N++)
                        {
                            FormToSave.dataGridView.Rows.Add();
                            FormToSave.dataGridView.Rows[N + RowIdx].Cells[0].Value = cGlobalInfo.CurrentScreening.ListDescriptors[N].GetName();
                            FormToSave.dataGridView.Rows[N + RowIdx].Cells[2].Value = TmpWell.ListSignatures[N].GetValue();

                            if (cGlobalInfo.CurrentScreening.ListDescriptors[N].IsActive())
                                FormToSave.dataGridView.Rows[N + RowIdx].Cells[1].Value = true;
                            else
                                FormToSave.dataGridView.Rows[N + RowIdx].Cells[1].Value = false;
                        }

                        goto NEXTSTEP;
                    }
            }

        NEXTSTEP: ;
            FormToSave.dataGridView.Update();

            if (FormToSave.ShowDialog() != System.Windows.Forms.DialogResult.OK) return false;
            ExportToCSV(CurrentPathforCSV, FormToSave.dataGridView);

            return true;

        }

        private void ExportToCSV(string PathName, DataGridView GridView)
        {
            DataGridView GridToSave = new DataGridView();

            for (int Row = 0; Row < GridView.RowCount; Row++)
            {
                if ((bool)GridView.Rows[Row].Cells[1].Value == true)
                {
                    string NameCol = (string)GridView.Rows[Row].Cells[0].Value;
                    GridToSave.Columns.Add(NameCol, NameCol);
                }
            }

            int NumDescriptor = cGlobalInfo.CurrentScreening.ListDescriptors.Count;
            int RowPos = 0;

            for (int PlateIdx = 0; PlateIdx < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; PlateIdx++)
            {
                cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(PlateIdx);

                for (int IdxValue = 0; IdxValue < cGlobalInfo.CurrentScreening.Columns; IdxValue++)
                    for (int IdxValue0 = 0; IdxValue0 < cGlobalInfo.CurrentScreening.Rows; IdxValue0++)
                    {
                        cWell TmpWell = CurrentPlateToProcess.GetWell(IdxValue, IdxValue0, true);
                        if (TmpWell == null) continue;

                        GridToSave.Rows.Add();

                        int ColPos = 0;
                        int RealPos = 0;

                        if ((bool)GridView.Rows[RealPos++].Cells[1].Value)
                            GridToSave.Rows[RowPos].Cells[ColPos++].Value = CurrentPlateToProcess.GetName();

                        if (cGlobalInfo.OptionsWindow.radioButtonWellPosModeSingle.Checked)
                        {
                            if ((bool)GridView.Rows[RealPos++].Cells[1].Value)
                                GridToSave.Rows[RowPos].Cells[ColPos++].Value = ConvertPosition(TmpWell.GetPosX(), TmpWell.GetPosY());
                        }
                        else
                        {
                            if ((bool)GridView.Rows[RealPos++].Cells[1].Value)
                            {
                                GridToSave.Rows[RowPos].Cells[ColPos++].Value = TmpWell.GetPosX();
                            }
                            if ((bool)GridView.Rows[RealPos++].Cells[1].Value)
                            {
                                GridToSave.Rows[RowPos].Cells[ColPos++].Value = TmpWell.GetPosY();
                            }
                        }

                        //if ((bool)GridView.Rows[RealPos++].Cells[1].Value)
                        //    GridToSave.Rows[RowPos].Cells[ColPos++].Value = TmpWell.Name;

                        //if ((bool)GridView.Rows[RealPos++].Cells[1].Value)
                        //    GridToSave.Rows[RowPos].Cells[ColPos++].Value = TmpWell.GetCurrentClassIdx();

                        //if ((bool)GridView.Rows[RealPos++].Cells[1].Value)
                        //{
                        //    object TmpID = TmpWell.ListProperties.FindValueByName("Locus ID");

                        //    if (TmpID == null)
                        //        GridToSave.Rows[RowPos].Cells[ColPos++].Value = "";
                        //    else
                        //    {
                        //        int LID = (int)TmpID;
                        //        GridToSave.Rows[RowPos].Cells[ColPos++].Value = LID;
                        //    }
                        //}

                        //if ((bool)GridView.Rows[RealPos++].Cells[1].Value)
                        //    GridToSave.Rows[RowPos].Cells[ColPos++].Value = (string)TmpWell.Info;


                        //if ((bool)GridView.Rows[RealPos++].Cells[1].Value)
                        //    GridToSave.Rows[RowPos].Cells[ColPos++].Value = (double)TmpWell.ListProperties.FindValueByName("Concentration");


                       // int IdxProperty = 0;
                        foreach (var item in cGlobalInfo.CurrentScreening.ListWellPropertyTypes)
                        {
                            if ((bool)GridView.Rows[RealPos++].Cells[1].Value)
                            {
                                object TmpObj = TmpWell.ListProperties.FindValueByName(item.Name);
                                if (TmpObj != null)
                                {
                                    GridToSave.Rows[RowPos].Cells[ColPos++].Value = TmpObj.ToString();
                                }
                                else
                                {
                                    GridToSave.Rows[RowPos].Cells[ColPos++].Value = "n.a.";
                                }
                            }
                        }



                        for (int N = 0; N < NumDescriptor; N++)
                        {
                            if ((bool)GridView.Rows[RealPos + N].Cells[1].Value)
                                GridToSave.Rows[RowPos].Cells[ColPos++].Value = TmpWell.ListSignatures[N].GetValue();
                        }
                        RowPos++;
                    }
            }
            DataGridfViewToCsV(GridToSave, PathName);
        }

        private void ExportToCSV(string PathName, bool IsAddDescriptors, bool IsPlateMode, bool IsFullScreen, bool IsIncludeNames, bool IsIncludeInfo)
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;
            int NumDesc = cGlobalInfo.CurrentScreening.ListDescriptors.Count;
            bool FirstWarning = true;


            if (IsPlateMode)
            {
                // loop on all the plate
                for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
                {
                    cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(cGlobalInfo.CurrentScreening.ListPlatesActive[PlateIdx].GetName());

                    // string CorrectedName = CurrentPlateToProcess.Name.Replace("/", "_");
                    //CorrectedName = CurrentPlateToProcess.Name.Replace("/", "_");

                    string CorrectedName = CheckAndCorrectFilemName(CurrentPlateToProcess.GetName(), FirstWarning);
                    if (CorrectedName != CurrentPlateToProcess.GetName())
                    {
                        FirstWarning = false;
                    }
                    StreamWriter stream = new StreamWriter(PathName + "\\" + CorrectedName + ".csv", false, System.Text.Encoding.ASCII);

                    string FirstLine = "Class";
                    for (int Col = 1; Col <= cGlobalInfo.CurrentScreening.Columns; Col++)
                        FirstLine += "," + Col;
                    stream.WriteLine(FirstLine);

                    for (int Row = 0; Row < cGlobalInfo.CurrentScreening.Rows; Row++)
                    {
                        byte[] strArray = new byte[1];
                        strArray[0] = (byte)(Row + 65);
                        string Chara = Encoding.UTF7.GetString(strArray);
                        string CurrentLine = Chara;

                        for (int Col = 0; Col < cGlobalInfo.CurrentScreening.Columns; Col++)
                        {
                            cWell TmpWell = CurrentPlateToProcess.GetWell(Col, Row, true);

                            if (TmpWell == null)
                                CurrentLine += ",";
                            else
                                CurrentLine += "," + TmpWell.GetCurrentClassIdx();
                        }
                        stream.WriteLine(CurrentLine);
                    }
                    stream.WriteLine("");


                    if (IsIncludeNames)
                    {
                        FirstLine = "Names";
                        for (int Col = 1; Col <= cGlobalInfo.CurrentScreening.Columns; Col++)
                            FirstLine += "," + Col;
                        stream.WriteLine(FirstLine);

                        for (int Row = 0; Row < cGlobalInfo.CurrentScreening.Rows; Row++)
                        {
                            byte[] strArray = new byte[1];
                            strArray[0] = (byte)(Row + 65);
                            string Chara = Encoding.UTF7.GetString(strArray);
                            string CurrentLine = Chara;

                            for (int Col = 0; Col < cGlobalInfo.CurrentScreening.Columns; Col++)
                            {

                                cWell TmpWell = CurrentPlateToProcess.GetWell(Col, Row, true);

                                if (TmpWell == null)
                                    CurrentLine += ",";
                                else
                                    CurrentLine += "," + TmpWell.GetCpdName();
                            }
                            stream.WriteLine(CurrentLine);
                        }
                        stream.WriteLine("");
                    }

                    if (IsIncludeInfo)
                    {
                        FirstLine = "Info";
                        for (int Col = 1; Col <= cGlobalInfo.CurrentScreening.Columns; Col++)
                            FirstLine += "," + Col;
                        stream.WriteLine(FirstLine);

                        for (int Row = 0; Row < cGlobalInfo.CurrentScreening.Rows; Row++)
                        {
                            byte[] strArray = new byte[1];
                            strArray[0] = (byte)(Row + 65);
                            string Chara = Encoding.UTF7.GetString(strArray);
                            string CurrentLine = Chara;

                            for (int Col = 0; Col < cGlobalInfo.CurrentScreening.Columns; Col++)
                            {

                                cWell TmpWell = CurrentPlateToProcess.GetWell(Col, Row, true);

                                if (TmpWell == null)
                                    CurrentLine += ",";
                                else
                                    CurrentLine += "," + TmpWell.Info;
                            }
                            stream.WriteLine(CurrentLine);
                        }
                        stream.WriteLine("");
                    }


                    for (int Desc = 0; Desc < NumDesc; Desc++)
                    {
                        if (cGlobalInfo.CurrentScreening.ListDescriptors[Desc].IsActive() == false) continue;

                        FirstLine = cGlobalInfo.CurrentScreening.ListDescriptors[Desc].GetName();
                        for (int Col = 1; Col <= cGlobalInfo.CurrentScreening.Columns; Col++)
                            FirstLine += "," + Col;
                        stream.WriteLine(FirstLine);

                        for (int Row = 0; Row < cGlobalInfo.CurrentScreening.Rows; Row++)
                        {
                            byte[] strArray = new byte[1];
                            strArray[0] = (byte)(Row + 65);
                            string Chara = Encoding.UTF7.GetString(strArray);
                            string CurrentLine = Chara;

                            for (int Col = 0; Col < cGlobalInfo.CurrentScreening.Columns; Col++)
                            {
                                cWell TmpWell = CurrentPlateToProcess.GetWell(Col, Row, true);

                                if (TmpWell == null)
                                    CurrentLine += ",";
                                else
                                    CurrentLine += "," + TmpWell.ListSignatures[Desc].GetValue();
                            }
                            stream.WriteLine(CurrentLine);
                        }
                        stream.WriteLine("");
                    }
                    stream.Dispose();
                }
            }

            if (IsFullScreen)
            {
                StreamWriter stream = new StreamWriter(PathName + "\\FullScreen.csv", false, System.Text.Encoding.ASCII);
                string TitleLine = "Plate Name,Column,Row,Class";
                if (IsIncludeNames) TitleLine += ",Name";
                if (IsIncludeInfo) TitleLine += ",Info";
                if (IsAddDescriptors)
                {
                    for (int Desc = 0; Desc < NumDesc; Desc++)
                    {
                        if (cGlobalInfo.CurrentScreening.ListDescriptors[Desc].IsActive() == false) continue;
                        else
                            TitleLine += "," + cGlobalInfo.CurrentScreening.ListDescriptors[Desc].GetName();
                    }
                }
                stream.WriteLine(TitleLine);

                // loop on all the plate
                for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
                {
                    cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(cGlobalInfo.CurrentScreening.ListPlatesActive[PlateIdx].GetName());

                    for (int Col = 0; Col < cGlobalInfo.CurrentScreening.Columns; Col++)
                        for (int Row = 0; Row < cGlobalInfo.CurrentScreening.Rows; Row++)
                        {
                            cWell TmpWell = CurrentPlateToProcess.GetWell(Col, Row, true);

                            if (TmpWell == null) continue;

                            string CurrentLine = CurrentPlateToProcess.GetName() + ",";
                            int _Class = TmpWell.GetCurrentClassIdx();

                            int _Column = Col + 1;
                            int _Row = Row + 1;

                            CurrentLine += _Column + "," + _Row + "," + _Class;

                            if (IsIncludeNames)
                                CurrentLine += "," + TmpWell.GetCpdName();

                            if (IsIncludeInfo)
                                CurrentLine += "," + TmpWell.Info;

                            if (IsAddDescriptors)
                            {
                                for (int Desc = 0; Desc < NumDesc; Desc++)
                                {
                                    if (cGlobalInfo.CurrentScreening.ListDescriptors[Desc].IsActive() == false) continue;
                                    else
                                        CurrentLine += "," + TmpWell.ListSignatures[Desc].GetValue();
                                }
                            }
                            stream.WriteLine(CurrentLine);
                        }
                }
                stream.Dispose();
            }


        }
        #endregion

        #region import TXT
        private void LoadTXTAssay(string[] FileNames)
        {
            FormForPlateDimensions PlateDim = new FormForPlateDimensions();
            if (PlateDim.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            if (cGlobalInfo.CurrentScreening != null) cGlobalInfo.CurrentScreening.Close3DView();
            cGlobalInfo.CurrentScreening = new cScreening("TXT imported Screening");
            cGlobalInfo.CurrentScreening.ImportFromTXT(FileNames, (int)PlateDim.numericUpDownColumns.Value, (int)PlateDim.numericUpDownRows.Value);
            StartingUpDateUI();

            if (cGlobalInfo.CurrentScreening.GetNumberOfOriginalPlates() == 0)
            {
                cGlobalInfo.ConsoleWriteLine("No plate loaded !");
                return;
            }

            this.toolStripcomboBoxPlateList.Items.Clear();
            for (int IdxPlate = 0; IdxPlate < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; IdxPlate++)
            {
                string Name = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(IdxPlate).GetName();
                this.toolStripcomboBoxPlateList.Items.Add(Name);
                PlateListWindow.listBoxPlateNameToProcess.Items.Add(Name);
                PlateListWindow.listBoxAvaliableListPlates.Items.Add(Name);
            }

            //  this.toolStripcomboBoxPlateList.SelectedIndex = CompleteScreening.CurrentDisplayPlateIdx = 0;
            cGlobalInfo.CurrentScreening.SetSelectionType(comboBoxClass.SelectedIndex - 1);

            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();
            UpdateUIAfterLoading();

            // comboBoxDescriptorToDisplay.SelectedIndex = 0;
            //  CompleteScreening.GetCurrentDisplayPlate().DisplayDistribution(CompleteScreening.ListDescriptors.CurrentSelectedDescriptor, true);
        }
        #endregion

        #region import MTR
        private void LoadMTRAssay(string[] FileNames)
        {
            if (cGlobalInfo.CurrentScreening != null) cGlobalInfo.CurrentScreening.Close3DView();

            cGlobalInfo.CurrentScreening = new cScreening("MTR imported Screening");
            StartingUpDateUI();

            cGlobalInfo.CurrentScreening.ImportFromMTR(FileNames);

            if (cGlobalInfo.CurrentScreening.ListPlatesActive.Count == 0)
            {
                cGlobalInfo.ConsoleWriteLine("No plate loaded !");
                return;
            }

            this.toolStripcomboBoxPlateList.Items.Clear();
            for (int IdxPlate = 0; IdxPlate < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; IdxPlate++)
            {
                string Name = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(IdxPlate).GetName();
                this.toolStripcomboBoxPlateList.Items.Add(Name);
                PlateListWindow.listBoxPlateNameToProcess.Items.Add(Name);
                PlateListWindow.listBoxAvaliableListPlates.Items.Add(Name);
                cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(IdxPlate).UpDataMinMax();
            }

            cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx = 0;
            cGlobalInfo.CurrentScreening.SetSelectionType(comboBoxClass.SelectedIndex - 1);
            comboBoxDescriptorToDisplay.SelectedIndex = 0;
            toolStripcomboBoxPlateList.SelectedIndex = 0;

            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), true);
        }
        #endregion

        #region Generate artificial screening data
        private void univariateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening != null) cGlobalInfo.CurrentScreening.Close3DView();

            FormForGenerateScreening WindowGenerateScreening = new FormForGenerateScreening(GlobalInfo);
            if (WindowGenerateScreening.ShowDialog() != DialogResult.OK) return;

            int NumRow = (int)WindowGenerateScreening.numericUpDownRows.Value;
            int NumCol = (int)WindowGenerateScreening.numericUpDownColumns.Value;
            if (!WindowGenerateScreening.checkBoxAddAsDescriptor.Checked)
            {
                cGlobalInfo.CurrentScreening = new cScreening("Generated Screen");
                cGlobalInfo.CurrentScreening.Rows = NumRow;
                cGlobalInfo.CurrentScreening.Columns = NumCol;
                cGlobalInfo.CurrentScreening.ListPlatesAvailable = new cListPlates();
                //cGlobalInfo.CurrentScreening = CurrentScreening;
            }

            int NumPlate = (int)WindowGenerateScreening.numericUpDownPlateNumber.Value;

            double MeanCpds = (double)WindowGenerateScreening.numericUpDownCpdsMean.Value;
            double StdevCpds = (double)WindowGenerateScreening.numericUpDownCpdsStdev.Value;

            double MeanPos = (double)WindowGenerateScreening.numericUpDownPosCtrlMean.Value;
            double StdevPos = (double)WindowGenerateScreening.numericUpDownPosCtrlStdv.Value;

            double MeanNeg = (double)WindowGenerateScreening.numericUpDownNegCtrlMean.Value;
            double StdevNeg = (double)WindowGenerateScreening.numericUpDownNegCtrlStdv.Value;

            cDescriptorType NewDescType = null;

            // create the descriptor
            if (!WindowGenerateScreening.checkBoxAddAsDescriptor.Checked)
            {
                cGlobalInfo.CurrentScreening.ListDescriptors.Clean();
                NewDescType = new cDescriptorType("Descriptor", true, 1);
                cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(NewDescType);
            }
            else
            {

                NewDescType = new cDescriptorType("New_Descriptor", true, 1);

                int NIdxDesc = 0;
                while (cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(NewDescType) != -1)
                {
                    NewDescType = new cDescriptorType("New_Descriptor" + NIdxDesc++, true, 1);

                }

                cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(NewDescType);
            }

            Random rand = new Random();

            int StepForDiffusion = (int)cGlobalInfo.OptionsWindow.numericUpDownGenerateScreenDiffusion.Value;
            double StepForRatioXY = (double)cGlobalInfo.OptionsWindow.numericUpDownGenerateScreenRatioXY.Value;
            double StepForNoiseStandardDeviation = (double)cGlobalInfo.OptionsWindow.numericUpDownGenerateScreenNoiseStdDev.Value;
            double StepForShiftRow = (double)cGlobalInfo.OptionsWindow.numericUpDownGenerateScreenRowEffectShift.Value;

            cEdgeEffect EdgeEffect = null;
            if (WindowGenerateScreening.checkBoxEdgeEffect.Checked)
            {
                if (WindowGenerateScreening.checkBoxEdgeEffectIteration.Checked)
                    EdgeEffect = new cEdgeEffect((int)WindowGenerateScreening.numericUpDownEdgeEffectIteration.Value + 1 + (int)WindowGenerateScreening.numericUpDownPlateNumber.Value * StepForDiffusion);
                else
                    EdgeEffect = new cEdgeEffect((int)WindowGenerateScreening.numericUpDownEdgeEffectIteration.Value + 1);
            }

            for (int IdxPlate = 0; IdxPlate < NumPlate; IdxPlate++)
            {
                cPlate CurrentPlate = null;
                if (!WindowGenerateScreening.checkBoxAddAsDescriptor.Checked)
                {
                    string PlateName = "Plate_" + IdxPlate;
                    CurrentPlate = new cPlate(PlateName, cGlobalInfo.CurrentScreening);
                    cGlobalInfo.CurrentScreening.AddPlate(CurrentPlate);
                }
                else
                {
                    CurrentPlate = cGlobalInfo.CurrentScreening.ListPlatesAvailable[IdxPlate];
                }

                if (WindowGenerateScreening.checkBoxStandardDeviation.Checked)
                    StdevCpds = (double)WindowGenerateScreening.numericUpDownCpdsStdev.Value + StepForNoiseStandardDeviation * IdxPlate;

                int IdxDesc = cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(NewDescType);

                for (int X = 1; X <= NumCol; X++)
                    for (int Y = 1; Y <= NumRow; Y++)
                    {
                        cListSignature LDesc = new cListSignature();

                        double u1 = rand.NextDouble();
                        double u2 = rand.NextDouble();
                        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
                        double randNormal = MeanCpds + StdevCpds * randStdNormal;

                        cSignature Desc = new cSignature(randNormal, NewDescType, cGlobalInfo.CurrentScreening);
                        cWell CurrentWell = null;

                        if (!WindowGenerateScreening.checkBoxAddAsDescriptor.Checked)
                        {
                            CurrentWell = new cWell(LDesc, X, Y, cGlobalInfo.CurrentScreening, CurrentPlate);
                            //CurrentWell.SetCpdName("Cpd X";
                          //  CurrentWell.SetClass(2);
                            CurrentPlate.AddWell(CurrentWell);
                        }
                        else
                            CurrentWell = CurrentPlate.GetWell(X - 1, Y - 1, false);

                        if (!WindowGenerateScreening.checkBoxAddAsDescriptor.Checked)
                            LDesc.Add(Desc);
                        else
                        {
                            if (CurrentWell == null) continue;
                            LDesc.Add(Desc);
                            CurrentWell.AddSignatures(LDesc);
                        }
                    }


                if (WindowGenerateScreening.checkBoxPositiveCtrl.Checked)
                {
                    for (int Y = 0; Y < NumRow; Y++)
                    {
                        cWell CurrentWell = CurrentPlate.GetWell((int)WindowGenerateScreening.numericUpDownColPosCtrl.Value, Y, false);
                        if (CurrentWell == null) continue;
                        CurrentWell.SetCpdName("+ve");
                        CurrentWell.SetClass(0);

                        double u1 = rand.NextDouble();
                        double u2 = rand.NextDouble();
                        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
                        double randNormal = MeanPos + StdevPos * randStdNormal;

                        CurrentWell.ListSignatures[IdxDesc].SetHistoValues(randNormal);
                    }
                }


                if (WindowGenerateScreening.checkBoxNegativeCtrl.Checked)
                {
                    for (int Y = 0; Y < NumRow; Y++)
                    {
                        cWell CurrentWell = CurrentPlate.GetWell((int)WindowGenerateScreening.numericUpDownColNegCtrl.Value, Y, false);
                        if (CurrentWell == null) continue;
                        CurrentWell.SetCpdName("-ve");
                        CurrentWell.SetClass(1);

                        double u1 = rand.NextDouble();
                        double u2 = rand.NextDouble();
                        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
                        double randNormal = MeanNeg + StdevNeg * randStdNormal;

                        CurrentWell.ListSignatures[IdxDesc].SetHistoValues(randNormal);
                    }
                }



                if (WindowGenerateScreening.checkBoxRowEffect.Checked)
                {
                    double ShiftForRow = (double)WindowGenerateScreening.numericUpDownRowEffectIntensity.Value;
                    if (WindowGenerateScreening.checkBoxShiftRowEffect.Checked)
                        ShiftForRow += StepForShiftRow * IdxPlate;

                    for (int X = 0; X < NumCol; X++)
                        for (int Y = 0; Y < NumRow; Y++)
                        {
                            cWell CurrentWell = CurrentPlate.GetWell(X, Y, false);
                            if (CurrentWell == null) continue;
                            double NewVal = CurrentWell.ListSignatures[CurrentWell.ListSignatures.Count - 1].GetValue() * ((Y + 1) + ShiftForRow);

                            CurrentWell.ListSignatures[IdxDesc].SetHistoValues(NewVal);
                        }
                }

                if (WindowGenerateScreening.checkBoxColumnEffect.Checked)
                {
                    for (int X = 0; X < NumCol; X++)
                        for (int Y = 0; Y < NumRow; Y++)
                        {
                            cWell CurrentWell = CurrentPlate.GetWell(X, Y, false);
                            if (CurrentWell == null) continue;
                            double CurrentValue = (CurrentWell.ListSignatures[CurrentWell.ListSignatures.Count - 1].GetValue() * ((X + 1) + (double)WindowGenerateScreening.numericUpDownColEffectIntensity.Value));

                            CurrentWell.ListSignatures[IdxDesc].SetHistoValues(CurrentValue);
                        }
                }

                if (WindowGenerateScreening.checkBoxBowlEffect.Checked)
                {
                    double CenterX = (double)NumCol / 2.0 - 0.5;
                    double CenterY = (double)NumRow / 2.0 - 0.5;

                    double CurrentRatioXY = (double)WindowGenerateScreening.numericUpDownBowlEffectRatioXY.Value;

                    if (WindowGenerateScreening.checkBoxRatioXY.Checked)
                        CurrentRatioXY = (double)WindowGenerateScreening.numericUpDownBowlEffectRatioXY.Value + StepForRatioXY * IdxPlate;

                    for (int X = 0; X < NumCol; X++)
                        for (int Y = 0; Y < NumRow; Y++)
                        {
                            cWell CurrentWell = CurrentPlate.GetWell(X, Y, false);
                            if (CurrentWell == null) continue;
                            double CurrentValue = (CurrentWell.ListSignatures[CurrentWell.ListSignatures.Count - 1].GetValue() * ((X + 1) + (double)WindowGenerateScreening.numericUpDownColEffectIntensity.Value));
                            CurrentValue *= (float)Math.Sqrt((X - CenterX) * (X - CenterX) / CurrentRatioXY + (Y - CenterY) * (Y - CenterY));

                            CurrentWell.ListSignatures[IdxDesc].SetHistoValues(CurrentValue);
                        }
                }

                if (WindowGenerateScreening.checkBoxEdgeEffect.Checked)
                {
                    int IdxDiff;

                    if (WindowGenerateScreening.checkBoxEdgeEffectIteration.Checked)
                        IdxDiff = (int)WindowGenerateScreening.numericUpDownEdgeEffectIteration.Value + IdxPlate * StepForDiffusion;
                    else
                        IdxDiff = (int)WindowGenerateScreening.numericUpDownEdgeEffectIteration.Value;

                    double[,] DiffusionMap = EdgeEffect.GetDiffusion(IdxDiff);

                    for (int X = 0; X < NumCol; X++)
                        for (int Y = 0; Y < NumRow; Y++)
                        {
                            cWell CurrentWell = CurrentPlate.GetWell(X, Y, false);
                            if (CurrentWell == null) continue;
                            double CurrentValue = (CurrentWell.ListSignatures[CurrentWell.ListSignatures.Count - 1].GetValue()) * (DiffusionMap[X, Y] * (double)WindowGenerateScreening.numericUpDownEdgeEffectIntensity.Value);
                            CurrentWell.ListSignatures[IdxDesc].SetHistoValues(CurrentValue);
                        }
                }
            }


            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();

            StartingUpDateUI();

            if (!WindowGenerateScreening.checkBoxAddAsDescriptor.Checked)
            {
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
            }

            //  FormForPlateManager WindowForPlateManager = new FormForPlateManager(CompleteScreening);
            //   WindowForPlateManager.ShowDialog();


        }

        public class cRandomDistribution
        {
            Accord.Statistics.Distributions.Multivariate.MultivariateNormalDistribution rand;
            public int IdxClass;
            public int ColPosi;
            public double[][] MultivariateDistrib;


            public cRandomDistribution(double[] means, double[,] covariances, int IdxClass, int ColumnPosition, int Dim, int NumPt)
            {
                this.IdxClass = IdxClass;
                this.ColPosi = ColumnPosition;
                this.rand = new Accord.Statistics.Distributions.Multivariate.MultivariateNormalDistribution(means, covariances);
                MultivariateDistrib = rand.Generate(NumPt);
            }
        }

        private void multivariateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormForMultivariateScreen WindowMultivariateScreen = new FormForMultivariateScreen(GlobalInfo);

            if (WindowMultivariateScreen.ShowDialog() != DialogResult.OK) return;

            int NumPlate = (int)WindowMultivariateScreen.numericUpDownPlateNumber.Value;
            int NumRow = (int)WindowMultivariateScreen.numericUpDownRows.Value;
            int NumCol = (int)WindowMultivariateScreen.numericUpDownColumns.Value;
            int NumDesc = (int)WindowMultivariateScreen.numericUpDownDimensionNumber.Value;

            if (cGlobalInfo.CurrentScreening != null) cGlobalInfo.CurrentScreening.Close3DView();

            cGlobalInfo.CurrentScreening = new cScreening("Generated Screen");

            cGlobalInfo.CurrentScreening.ListDescriptors.Clean();
            cGlobalInfo.CurrentScreening.Rows = NumRow;
            cGlobalInfo.CurrentScreening.Columns = NumCol;
            cGlobalInfo.CurrentScreening.ListPlatesAvailable = new cListPlates();

            for (int i = 0; i < NumDesc; i++)
                cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(new cDescriptorType("Descriptor_" + i, true, 1));

            // let's generate all the distributions
            List<cRandomDistribution> ListDistributions = new List<cRandomDistribution>();
            for (int i = 0; i < cGlobalInfo.ListWellClasses.Count; i++)
            {
                if ((bool)WindowMultivariateScreen.dataGridViewForCompounds.Rows[i].Cells[2].Value)
                {
                    double[] means = new double[NumDesc];
                    double[,] covariances = new double[NumDesc, NumDesc];
                    for (int Dim = 0; Dim < NumDesc; Dim++)
                    {
                        means[Dim] = Convert.ToDouble(WindowMultivariateScreen.dataGridViewForCompounds.Rows[i].Cells[2 * Dim + 3].Value.ToString());
                        covariances[Dim, Dim] = Math.Pow(Convert.ToDouble(WindowMultivariateScreen.dataGridViewForCompounds.Rows[i].Cells[2 * Dim + 4].Value.ToString()), 2);
                    }

                    cRandomDistribution CurrentDistrib;
                    if ((string)WindowMultivariateScreen.dataGridViewForCompounds.Rows[i].Cells[1].Value == "Entire plate")

                        CurrentDistrib = new cRandomDistribution(means, covariances, i, -1, NumDesc, NumRow * NumCol * NumPlate);
                    else
                        CurrentDistrib = new cRandomDistribution(means, covariances, i, Convert.ToInt16((string)WindowMultivariateScreen.dataGridViewForCompounds.Rows[i].Cells[1].Value), NumDesc, NumRow * NumPlate);

                    ListDistributions.Add(CurrentDistrib);
                }
            }

            // generate the plates
            for (int IdxPlate = 0; IdxPlate < NumPlate; IdxPlate++)
            {
                string PlateName = "Plate_" + IdxPlate;
                cPlate CurrentPlate = new cPlate( PlateName, cGlobalInfo.CurrentScreening);
                cGlobalInfo.CurrentScreening.AddPlate(CurrentPlate);
            }

            FormForProgress FFP = new FormForProgress();
            FFP.progressBar.Maximum = NumPlate * (ListDistributions.Count+1);
            FFP.progressBar.Value = 0;
            FFP.Show();

            //toolStripProgressBar.Maximum


            // Place the values
            for (int IdxDistri = 0; IdxDistri < ListDistributions.Count; IdxDistri++)
            {
                if (ListDistributions[IdxDistri].ColPosi != -1) continue;

                for (int IdxPlate = 0; IdxPlate < NumPlate; IdxPlate++)
                {
                    cPlate CurrentPlate = cGlobalInfo.CurrentScreening.ListPlatesAvailable[IdxPlate];
                    for (int X = 1; X <= NumCol; X++)
                        for (int Y = 1; Y <= NumRow; Y++)
                        {
                            // Create the descriptor list and add it to the well, then add the well
                            cListSignature LDesc = new cListSignature();
                            for (int i = 0; i < NumDesc; i++)
                            {
                                cSignature Desc = null;
                                double randNormal = ListDistributions[IdxDistri].MultivariateDistrib[(X - 1) + (Y - 1) * NumCol + IdxPlate * NumRow * NumCol][i];

                                Desc = new cSignature(randNormal, cGlobalInfo.CurrentScreening.ListDescriptors[i], cGlobalInfo.CurrentScreening);
                                LDesc.Add(Desc);
                            }

                            cWell CurrentWell = CurrentPlate.GetWell(X - 1, Y - 1, false);
                            if (CurrentWell == null)
                            {
                                CurrentWell = new cWell(LDesc, X, Y, cGlobalInfo.CurrentScreening, CurrentPlate);
                                CurrentPlate.AddWell(CurrentWell);
                            }
                           // CurrentWell.Name = "Cpds";
                            CurrentWell.SetClass(ListDistributions[IdxDistri].IdxClass);
                        }
                    
                    FFP.progressBar.Value = IdxPlate*ListDistributions.Count + IdxPlate;
                    FFP.Refresh();
                }

                

            }


            FFP.Close();

            for (int IdxPlate = 0; IdxPlate < NumPlate; IdxPlate++)
            {
                cPlate CurrentPlate = cGlobalInfo.CurrentScreening.ListPlatesAvailable[IdxPlate];
                for (int IdxDistri = 0; IdxDistri < ListDistributions.Count; IdxDistri++)
                {
                    if (ListDistributions[IdxDistri].ColPosi != -1)
                    {
                        for (int Y = 1; Y <= NumRow; Y++)
                        {
                            // Create the descriptor list and add it to the well, then add the well
                            cListSignature LDesc = new cListSignature();
                            for (int i = 0; i < NumDesc; i++)
                            {
                                cSignature Desc = null;
                                double randNormal = ListDistributions[IdxDistri].MultivariateDistrib[(Y - 1) + IdxPlate * NumRow][i];

                                Desc = new cSignature(randNormal, cGlobalInfo.CurrentScreening.ListDescriptors[i], cGlobalInfo.CurrentScreening);
                                LDesc.Add(Desc);
                            }

                            cWell CurrentWell = CurrentPlate.GetWell(ListDistributions[IdxDistri].ColPosi, Y - 1, false);
                            if (CurrentWell == null)
                            {
                                CurrentWell = new cWell(LDesc, ListDistributions[IdxDistri].ColPosi + 1, Y, cGlobalInfo.CurrentScreening, CurrentPlate);
                                CurrentPlate.AddWell(CurrentWell);
                            }
                            else
                            {
                                for (int i = 0; i < NumDesc; i++)
                                {
                                    CurrentWell.ListSignatures[i].SetHistoValues(ListDistributions[IdxDistri].MultivariateDistrib[(Y - 1) + IdxPlate * NumRow][i]);
                                    //CurrentWell.ListDescriptors[i].Getvalue() = ListDistributions[IdxDistri].MultivariateDistrib[(Y - 1) + IdxPlate * NumRow][i];
                                }

                            }
                           // CurrentWell.Name = "Cpds";
                            CurrentWell.SetClass(ListDistributions[IdxDistri].IdxClass);
                        }
                    }
                }
            }


            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();

            StartingUpDateUI();

            this.toolStripcomboBoxPlateList.Items.Clear();
            for (int IdxPlate = 0; IdxPlate < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; IdxPlate++)
            {
                string Name = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(IdxPlate).GetName();
                this.toolStripcomboBoxPlateList.Items.Add(Name);
                PlateListWindow.listBoxPlateNameToProcess.Items.Add(Name);
                PlateListWindow.listBoxAvaliableListPlates.Items.Add(Name);
                cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(IdxPlate).UpDataMinMax();
            }
            cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx = 0;
            cGlobalInfo.CurrentScreening.SetSelectionType(comboBoxClass.SelectedIndex - 1);

            cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx = 0;
            //   comboBoxDescriptorToDisplay.SelectedIndex = 0;
            // toolStripcomboBoxPlateList.SelectedIndex = 0;
            // CompleteScreening.GetCurrentDisplayPlate().DisplayDistribution(CompleteScreening.ListDescriptors.CurrentSelectedDescriptor, true);

            UpdateUIAfterLoading();
        }
        #endregion

        private void LoadCellByCellDB(FormForPlateDimensions PlateDim, string Path, bool IsDisplayPlateSelection)
        {
            string[] ListFilesForPlates = null;
            try
            {
                //ListFilesForPlates
                //Directory.GetFiles(Path, "*.dll").Select(fn => new FileInfo(fn)).OrderBy(f => f.Length);

                ListFilesForPlates = Directory.GetFiles(Path, "*.db", SearchOption.TopDirectoryOnly).OrderBy(f => f).ToArray();


            }
            catch (System.Exception excep)
            {
                MessageBox.Show(excep.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

          
            if (ListFilesForPlates.Length == 0)
            {
                MessageBox.Show("The selected directory do not contain any .db file !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsDisplayPlateSelection)
            {
                // plates selection GUI
                FormForPlateSelection FFP = new FormForPlateSelection(ListFilesForPlates, true);
                if (FFP.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
                ListFilesForPlates = FFP.GetListPlatesSelected();
            }

            //   CompleteScreening.LoadData(Path, (int)PlateDim.numericUpDownColumns.Value, (int)PlateDim.numericUpDownRows.Value);
            int StartColumn = 0;
            if (PlateDim.checkBoxIsOmitFirstColumn.Checked) StartColumn = 1;

            int HistoSize = (int)PlateDim.numericUpDownHistoSize.Value;
            int NumRow = (int)PlateDim.numericUpDownRows.Value;
            int NumCol = (int)PlateDim.numericUpDownColumns.Value;

            string[] ScreeningName = Path.Split('\\');

            int lastIdx = Path.LastIndexOf('\\');

            cGlobalInfo.CurrentScreening = new cScreening(ScreeningName[ScreeningName.Length - 1]);
            cGlobalInfo.CurrentScreening.Rows = NumRow;
            cGlobalInfo.CurrentScreening.Columns = NumCol;
            cGlobalInfo.CurrentScreening.ListPlatesAvailable = new cListPlates();
            if (PlateDim.radioButtonDataHDDB.Checked)
                cGlobalInfo.CellByCellDataAccess = eCellByCellDataAccess.HD;
            else
                cGlobalInfo.CellByCellDataAccess = eCellByCellDataAccess.MEMORY;

            cDescriptorType NewDescType = null;

            // create the descriptor
            if (cGlobalInfo.CurrentScreening != null)
                cGlobalInfo.CurrentScreening.ListDescriptors.Clean();
            cGlobalInfo.CurrentScreening.ListDescriptors.Clean();

            // open the first database to build the descriptor list
            cPlate CurrentPlate = null;

            string[] TmpNames = ListFilesForPlates[0].Split('\\');
            string SafePlateName = TmpNames[TmpNames.Length - 1];
            SafePlateName = SafePlateName.Remove(SafePlateName.LastIndexOf(".db"));

            string PlateName = ListFilesForPlates[0];

            CurrentPlate = new cPlate( SafePlateName, cGlobalInfo.CurrentScreening);

            CurrentPlate.DBConnection = new cDBConnection(CurrentPlate, PlateName);
            List<string> ListWells = CurrentPlate.DBConnection.GetListTableNames();
            List<string> ListDescNames = CurrentPlate.DBConnection.GetDescriptorNames(0);

            bool IsPhenotypeClassExist = CurrentPlate.DBConnection.CheckIfColumnExist(0, "Phenotype_Class");
            if (!IsPhenotypeClassExist)
            {
                if (MessageBox.Show("Create " + "Phenotype_Class" + " column in the current database ?", "Error DB: " + "Phenotype_Class" + " does not exist", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Yes)
                {
                    CurrentPlate.DBConnection.CreateNewColumn("Phenotype_Class", 0);
                }
                else
                    return;
            }

            bool IsPhenotypeConfidenceExist = CurrentPlate.DBConnection.CheckIfColumnExist(0, "Phenotype_Confidence");
            if (!IsPhenotypeConfidenceExist)
            {
                if (MessageBox.Show("Create " + "Phenotype_Confidence" + " column in the current database ?", "Error DB: " + "Phenotype_Confidence" + " does not exist", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Yes)
                {
                    CurrentPlate.DBConnection.CreateNewColumn("Phenotype_Confidence", 1);
                }
                else
                    return;
            }

            int TmpNumDesc = 0;
            for (int IdxDesc = StartColumn; IdxDesc < ListDescNames.Count; IdxDesc++)
            {
                if ((ListDescNames[IdxDesc] == "Phenotype_Class" || (ListDescNames[IdxDesc] == "Phenotype_Confidence"))) continue;
              //  if (PlateDim.checkBoxIsOmitFirstColumn.Checked)&&(ListDescNames[IdxDesc
                NewDescType = new cDescriptorType(ListDescNames[IdxDesc], true, HistoSize, true);
                cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(NewDescType);
                TmpNumDesc++;
            }
        
            int NumDesc = cGlobalInfo.CurrentScreening.ListDescriptors.Count;

            cDescriptorType DescTypeCellCount = null;
            if (PlateDim.checkBoxAddCellNumber.Checked)
            {
                DescTypeCellCount = new cDescriptorType("Cell Count", true, 1, false);
                cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(DescTypeCellCount);
            
            }
            
        
            CurrentPlate.DBConnection.CloseConnection();

            FormForDoubleProgress WindowProgress = new FormForDoubleProgress();
            WindowProgress.Show();
            WindowProgress.progressBarPlate.Maximum = (int)ListFilesForPlates.Length;
            WindowProgress.progressBarPlate.Value = 0;
            WindowProgress.progressBarPlate.Refresh();

            for (int IdxPlate = 0; IdxPlate < (int)ListFilesForPlates.Length; IdxPlate++)
            {
                WindowProgress.progressBarPlate.Value++;
                WindowProgress.progressBarPlate.Refresh();
                WindowProgress.labelPlateIdx.Text = (IdxPlate + 1) + " / " + (int)ListFilesForPlates.Length;

                //UpdateProgress(WindowProgress.progressBarPlate, IDx++);

                TmpNames = ListFilesForPlates[IdxPlate].Split('\\');
                SafePlateName = TmpNames[TmpNames.Length - 1];
                SafePlateName = SafePlateName.Remove(SafePlateName.LastIndexOf(".db"));

                PlateName = ListFilesForPlates[IdxPlate];
                CurrentPlate = new cPlate( SafePlateName, cGlobalInfo.CurrentScreening);

                CurrentPlate.DBConnection = new cDBConnection(CurrentPlate, PlateName);

                if (CurrentPlate.DBConnection.IsSucceed == false) continue;

                // check the consistency of 
                List<string> ListDescNamesTmp = CurrentPlate.DBConnection.GetDescriptorNames(0);

                if (ListDescNamesTmp.Count != ListDescNames.Count)
                {
                    richTextBoxConsole.AppendText(CurrentPlate.GetName() + ": number of descriptors is inconstistent ! plate skipped.\n");
                    cGlobalInfo.CurrentScreening.ListPlatesAvailable.Remove(CurrentPlate);
                    continue;
                }


                IsPhenotypeConfidenceExist = CurrentPlate.DBConnection.CheckIfColumnExist(0, "Phenotype_Confidence");
                if (!IsPhenotypeConfidenceExist)
                {
                    richTextBoxConsole.AppendText(CurrentPlate.GetName() + ": Create Phenotype_Confidence column in the current database\n");
                    CurrentPlate.DBConnection.CreateNewColumn("Phenotype_Confidence", 1);
                }


                cGlobalInfo.CurrentScreening.AddPlate(CurrentPlate);

                int IdxDesc = cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(NewDescType);
                ListWells = CurrentPlate.DBConnection.GetListTableNames();

                WindowProgress.progressBarWell.Value = 0;
                WindowProgress.progressBarWell.Maximum = ListWells.Count;

                for (int IdxWell = 0; IdxWell < ListWells.Count; IdxWell++)
                {
                    WindowProgress.labelWellIdx.Text = IdxWell + " / " + ListWells.Count;
                    WindowProgress.progressBarWell.Value = IdxWell;
                    WindowProgress.Refresh();
                    // first rebuild the position with the name
                    string[] ListS = ListWells[IdxWell].Split('_');
                    string[] Positions = ListS[ListS.Length - 1].Split('x');

                    // obsolete: can be optimized
                    List<cDescriptorType> LCDT = new List<cDescriptorType>();
                    LCDT.Add(cGlobalInfo.CurrentScreening.ListDescriptors[0]);

                    int Numcells = CurrentPlate.DBConnection.GetWellValues(ListWells[IdxWell], LCDT)[0].Count;
                    if (Numcells == 0) continue;

                    cListSignature LDesc = new cListSignature();

                    for (IdxDesc = 0/* StartColumn*/; IdxDesc < NumDesc; IdxDesc++)
                    {
                        LCDT = new List<cDescriptorType>();
                        LCDT.Add(cGlobalInfo.CurrentScreening.ListDescriptors[IdxDesc /*- StartColumn*/]);

                        cExtendedTable ListForPts = CurrentPlate.DBConnection.GetWellValues(ListWells[IdxWell], LCDT);

                        cSignature Desc = new cSignature(ListForPts[0], HistoSize, cGlobalInfo.CurrentScreening.ListDescriptors[IdxDesc /*- StartColumn*/], cGlobalInfo.CurrentScreening);
                        LDesc.Add(Desc);
                    }

                    if (DescTypeCellCount != null)
                    {
                        cSignature Desc = new cSignature(Numcells, DescTypeCellCount, cGlobalInfo.CurrentScreening);
                        LDesc.Add(Desc);
                    }
                    if (Positions.Length <= 2) continue;
                    cWell CurrentWell = new cWell(LDesc, int.Parse(Positions[1]), int.Parse(Positions[2]), cGlobalInfo.CurrentScreening, CurrentPlate);

                    int a = cGlobalInfo.CurrentScreening.ListDescriptors.Count;

                    //CurrentWell.Name = "Cpds";
                    CurrentWell.SQLTableName = ListWells[IdxWell];
                    if (CurrentPlate.AddWell(CurrentWell) == false)
                    {
                        // the well has not been added (probably out of the plate dimension)
                    }
                    else
                    {
                        CurrentWell.UpdateNumberOfBiologicalObjects();
                    }
                    //this.Invoke(delegate(){ WindowProgress.progressBarWell.Value++;});
                    //this.Invoke(Delegate() { });


                    //WindowProgress.Invoke(delegate(){});

                }
                CurrentPlate.DBConnection.CloseConnection();
                //  WindowProgress.progressBarPlate.Value++;

            }

            WindowProgress.Dispose();

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
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

        public void UpdateProgress(ProgressBar PG, int progress)
        {
            PG.BeginInvoke(
                new Action(() =>
                {
                    PG.Value = progress;
                }
            ));
        }


        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileDialogCurrPict = new OpenFileDialog();
            FileDialogCurrPict.DefaultExt = "fcs";
            FileDialogCurrPict.Filter = "FCS files (*.fcs)|*.fcs";
            DialogResult result = FileDialogCurrPict.ShowDialog();


            if (FileDialogCurrPict.FileName == "") return;

            FormForPlateDimensions PlateDim = new FormForPlateDimensions();
            PlateDim.checkBoxAddCellNumber.Visible = true;
            PlateDim.labelHisto.Visible = true;
            PlateDim.numericUpDownHistoSize.Visible = true;
            PlateDim.numericUpDownColumns.Value = 1;
            PlateDim.numericUpDownColumns.ReadOnly = true;
            PlateDim.numericUpDownRows.Value = 1;
            PlateDim.numericUpDownRows.ReadOnly = true;


            if (PlateDim.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            using (BinaryReader b = new BinaryReader(File.OpenRead(FileDialogCurrPict.FileName)))
            {
                int pos = 0;

                int length = (int)b.BaseStream.Length;
                {
                    char[] c = b.ReadChars(10);
                    Console.WriteLine(c);
                    pos += 10;

                    int ip1 = System.Convert.ToInt32(new string(b.ReadChars(8)));
                    int ip2 = System.Convert.ToInt32(new string(b.ReadChars(8)));
                    int ip3 = System.Convert.ToInt32(new string(b.ReadChars(8)));
                    int ip4 = System.Convert.ToInt32(new string(b.ReadChars(8)));

                    Console.WriteLine(ip1 + ":" + ip2 + ":" + ip3 + ":" + ip4);

                    b.BaseStream.Position = ip1;
                    int sizeText = ip2 - ip1;
                    char[] Txt = b.ReadChars(sizeText);
                    string txtString = new string(Txt);
                    int IdxPAR = txtString.IndexOf("PAR");
                    b.BaseStream.Position = ip1 + IdxPAR + 4;
                    char[] NumPar = b.ReadChars(1);
                    string stringNum = new string(NumPar);
                    int NumParameters = System.Convert.ToInt32(stringNum);
                    Console.WriteLine("Num. Param. :" + NumParameters);

                    int IdxMode = txtString.IndexOf("MODE");
                    b.BaseStream.Position = ip1 + IdxMode + 5;
                    char NumMode = b.ReadChar();
                    Console.WriteLine("Mode :" + NumMode + " (L <=> List)");

                    int IdxDataType = txtString.IndexOf("DATATYPE");
                    b.BaseStream.Position = ip1 + IdxDataType + 9;
                    char NumDataType = b.ReadChar();
                    Console.WriteLine("Datatype :" + NumDataType + " (I <=> integer)");
                    if (NumDataType != 73) return;


                    List<string> ListDescNames = new List<string>();
                    string stoFind;
                    for (int i = 1; i <= NumParameters; i++)
                    {
                        string TmpName = "";
                        stoFind = "P" + i + "N";
                        int IdxPos = txtString.IndexOf(stoFind);
                        b.BaseStream.Position = ip1 + IdxPos + 4;

                        char TChar = new char();
                        while (!char.IsWhiteSpace(TChar))
                        {
                            TChar = b.ReadChar();
                            TmpName += TChar;
                        }
                        stoFind = TmpName.Remove(TmpName.Length - 1);
                        ListDescNames.Add(stoFind);
                    }

                    int HistoSize = (int)PlateDim.numericUpDownHistoSize.Value;
                    int NumRow = (int)PlateDim.numericUpDownRows.Value;
                    int NumCol = (int)PlateDim.numericUpDownColumns.Value;

                    cGlobalInfo.CurrentScreening = new cScreening("FACS");
                    cGlobalInfo.CurrentScreening.Rows = NumRow;
                    cGlobalInfo.CurrentScreening.Columns = NumCol;
                    cGlobalInfo.CurrentScreening.ListPlatesAvailable = new cListPlates();

                    cDescriptorType NewDescType = null;

                    // create the descriptor
                    cGlobalInfo.CurrentScreening.ListDescriptors.Clean();

                    // open the first database to build the descriptor list
                    cPlate CurrentPlate = null;
                    string PlateName = "Plate_1";
                    CurrentPlate = new cPlate(PlateName, cGlobalInfo.CurrentScreening);
                    cGlobalInfo.CurrentScreening.AddPlate(CurrentPlate);
                    int NumDesc = ListDescNames.Count;
                    for (int IdxDesc = 0; IdxDesc < NumDesc; IdxDesc++)
                    {
                        NewDescType = new cDescriptorType(ListDescNames[IdxDesc], true, HistoSize, true);
                        cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(NewDescType);
                    }

                    cDescriptorType DescTypeCellCount = null;
                    if (PlateDim.checkBoxAddCellNumber.Checked)
                    {
                        DescTypeCellCount = new cDescriptorType("Cell Count", true, 1, false);
                        cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(DescTypeCellCount);
                    }

                    List<string> ParamLogZero = new List<string>();
                    for (int i = 1; i <= NumParameters; i++)
                    {
                        string TmpName = "";
                        stoFind = "P" + i + "E";
                        int IdxPos = txtString.IndexOf(stoFind);
                        b.BaseStream.Position = ip1 + IdxPos + 4;

                        char TChar = new char();
                        TChar = b.ReadChar();
                        TmpName += TChar;
                        ParamLogZero.Add(TmpName);
                    }

                    b.BaseStream.Position = ip3;

                    int NumCells = (ip4 - ip3) / (sizeof(Int16) * NumParameters) + 1;

                    cExtendedList[] TableValues = new cExtendedList[NumDesc];
                    for (int j = 0; j < NumDesc; j++) TableValues[j] = new cExtendedList();

                    int TmpValue;
                    byte b1, b2;

                    for (int i = 0; i < NumCells; i++)
                    {
                        for (int j = 0; j < NumParameters; j++)
                        {
                            b1 = b.ReadByte();
                            b2 = b.ReadByte();

                            // if (j == DataIdx)
                            // {
                            TmpValue = b2 + b1 * 256;

                            TableValues[j].Add(TmpValue);
                            //if (TmpValue == 1023) TmpValue = 0;

                            //    ListDataForHisto[IdxCol].Add(TmpValue);
                            // }
                            //TmpListData[i] = TmpValue;// 100.0f * (float)(Math.Log10(TmpValue + 4));
                        }
                    }




                    cListSignature LDesc = new cListSignature();
                    for (int IdxDesc = 0; IdxDesc < NumDesc; IdxDesc++)
                    {
                        cExtendedList ListForPts = TableValues[IdxDesc];
                        //   cDescriptor Desc = new cDescriptor(ListForPts.CreateHistogram(0, HistoSize - 1, HistoSize - 1)[1], 0, HistoSize - 1, CompleteScreening.ListDescriptors[IdxDesc], CompleteScreening);


                        cSignature Desc = new cSignature(ListForPts, HistoSize, cGlobalInfo.CurrentScreening.ListDescriptors[IdxDesc], cGlobalInfo.CurrentScreening);

                        // ListForPts.CreateHistogram(HistoSize)[1], ListForPts.Min(), ListForPts.Max(), CompleteScreening.ListDescriptors[IdxDesc], CompleteScreening);

                        LDesc.Add(Desc);
                    }

                    if (DescTypeCellCount != null)
                    {
                        cSignature Desc = new cSignature(NumCells, DescTypeCellCount, cGlobalInfo.CurrentScreening);

                        LDesc.Add(Desc);

                    }

                    cWell CurrentWell = new cWell(LDesc, 1, 1, cGlobalInfo.CurrentScreening, CurrentPlate);
                  //  CurrentWell.Name = "Cpds";
                    //CurrentWell.SQLTableName = ListWells[IdxWell];
                    CurrentPlate.AddWell(CurrentWell);



                    b.Close();


                    cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
                    cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

                    for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
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


            }

            return;
        }



    }
}