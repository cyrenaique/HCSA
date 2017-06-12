using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.General_Types;
using System.IO;
using LibPlateAnalysis;
using System.Windows.Forms;
using HCSAnalyzer.Forms.IO;

namespace HCSAnalyzer.Classes.Base_Components.DataManip
{

    public class cDBToCSV : cComponent
    {
        cListWells Input = null;

        //public int DigitNumber = 2;
        //public string Separator = ",";
        //public eFileType FileType = eFileType.CSV;
        //string FileExtension = ".csv";
        public string FilePath = "";
        //public bool IsDisplayUIForFilePath = false;
        //public bool IsRunEXCEL = false;
        //public bool IsAppend = false;

        public cDBToCSV()
        {
            Title = "DB to CSV";
        }

        public void SetInputData(cListWells MyData)
        {
            this.Input = MyData;
        }

        public cFeedBackMessage Run()
        {
            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }

            if (FilePath == "")
            {
                // user interface for file selection
                SaveFileDialog CurrSaveFileDialog = new SaveFileDialog();
                CurrSaveFileDialog.Filter = "CSV file (*.csv)|*.csv";
                System.Windows.Forms.DialogResult Res = CurrSaveFileDialog.ShowDialog();
                if (Res != System.Windows.Forms.DialogResult.OK)
                    return new cFeedBackMessage(false,this);
                FilePath = CurrSaveFileDialog.FileName;
            }

            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate))
                {
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("File currently used by another application.\n", "Saving error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new cFeedBackMessage(false,this);
            }

            // create the file
            string separator = ",";
            List<cDescriptorType> ListSelectedDesc = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveSingleCellDescriptors();

            if (ListSelectedDesc.Count == 0)
            {
                MessageBox.Show("No single cell based descriptor selected.\n", "Saving error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new cFeedBackMessage(false, this);
            }

            FormForProgress MyProgressBar = new FormForProgress();
            MyProgressBar.progressBar.Maximum = this.Input.Count;

            MyProgressBar.Show();

            using (StreamWriter myFile = new StreamWriter(FilePath, false, Encoding.Default))
            {
                string HeaderToBeWritten = "Plate,Well,";
                for (int i = 0; i < ListSelectedDesc.Count; i++)
                {
                    HeaderToBeWritten += ListSelectedDesc[i].GetName() + separator;
                }
                HeaderToBeWritten += "Phenotype_Class";
                myFile.WriteLine(HeaderToBeWritten);


                int IdxWell = 0;
                foreach(cWell TmpWell in this.Input)
                {
                    cListWells TmpList = new cListWells(TmpWell);

                    // this table should contain all the selected descriptor values and the phenotypic classes
                    cExtendedTable CompleteTable = TmpList.GetSingleObjectDescriptorValuesFull(ListSelectedDesc);

                    HeaderToBeWritten = TmpWell.AssociatedPlate.GetName() + separator;
                    HeaderToBeWritten += TmpWell.GetPos() + separator;

                    for (int Row = 0; Row < CompleteTable[0].Count; Row++)
                    {
                        string ToBeWritten = HeaderToBeWritten;

                        for (int i = 0; i < CompleteTable.Count - 1; i++)
                            ToBeWritten += CompleteTable[i][Row] + separator;

                        ToBeWritten += CompleteTable[CompleteTable.Count - 1][Row];
                        myFile.WriteLine(ToBeWritten);
                    }

                    MyProgressBar.progressBar.Value = IdxWell++;
                    MyProgressBar.richTextBoxForComment.Text = TmpWell.GetShortInfo();
                    MyProgressBar.Update();
                }
                myFile.Close();
            }

            MyProgressBar.Close();

























            return FeedBackMessage;
        }
    }
}
