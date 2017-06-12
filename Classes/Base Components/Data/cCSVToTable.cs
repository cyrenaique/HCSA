using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using System.Windows.Forms;
using System.Data;
using LibPlateAnalysis;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes.DataManip;
using HCSAnalyzer.Classes.Base_Classes.Viewers._2D;
using System.IO;

namespace HCSAnalyzer.Classes.Base_Classes.Data
{
    public class cCSVToTable : cComponent
    {
        cExtendedTable Output = null;

        public bool AddAsObject = false;
        string CSVFileName = "";
        public string Separator = ",";
        public string FileExtension = ".csv";
        public bool IsDisplayUIForFilePath = false;
        public int HeaderSize = 0;
        public bool IsContainColumnHeaders = true;
        public bool IsContainRowNames = false;
        public int RejectedRows { get; private set;}

        public cCSVToTable()
        {
            Title = "CSV to DataTable";
        }

        public void SetInputData(string CSVFileName)
        {
            this.CSVFileName = CSVFileName;
        }

        public cExtendedTable GetOutPut()
        {
            return this.Output;
        }

        public cFeedBackMessage Run()
        {
            string TableName = "";

            if (IsDisplayUIForFilePath)
            {
                OpenFileDialog CurrOpenFileDialog = new OpenFileDialog();
                CurrOpenFileDialog.Filter = "csv files (*" + FileExtension + ")|*" + FileExtension;
                System.Windows.Forms.DialogResult Res = CurrOpenFileDialog.ShowDialog();
                if (Res != System.Windows.Forms.DialogResult.OK)
                {
                    FeedBackMessage.IsSucceed = false;
                    FeedBackMessage.Message = "Incorrect File Name.";
                    return FeedBackMessage;
                }
                this.CSVFileName = CurrOpenFileDialog.FileName;
                TableName = CurrOpenFileDialog.SafeFileName;
            }
            else
            {
                TableName = this.CSVFileName;
            }

            if (this.CSVFileName == "")
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "CSV file name not specified";
                return FeedBackMessage;
            }

            this.Output = new cExtendedTable();
            this.Output.Name = TableName;
            RejectedRows=0;

            //using (FileStream myFile =  new FileStream(this.CSVFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))

            FileStream fs = new FileStream(CSVFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
         //   StreamReader sr = new StreamReader(fs);

            using (StreamReader myFile = new StreamReader(fs))// new StreamReader(File.OpenRead(this.CSVFileName)))//, Encoding.Default, false))
            {
                
                // jump the header
                for (int i = 0; i < HeaderSize; i++)
                    myFile.ReadLine();

                // read the first row to build the table
                string CurrentRow = myFile.ReadLine();
                string[] Values = CurrentRow.Split(this.Separator.ToArray());

                int ColumnNumber = Values.Length;
                if (IsContainRowNames) ColumnNumber--;

                if (ColumnNumber <= 0)
                {
                    FeedBackMessage.IsSucceed = false;
                    FeedBackMessage.Message = "Corrupted data";
                    return FeedBackMessage;
                }

                int RowShift = 0;
                if (IsContainRowNames) RowShift = 1;

                for (int i = 0; i < ColumnNumber; i++)
                {
                    this.Output.Add(new cExtendedList(Values[i + RowShift]));
                    //this.Output[this.Output.Count - 1].Name = ;
                }

                if (IsContainRowNames) this.Output.ListRowNames = new List<string>();

                if (AddAsObject)
                {
                    List<object> LValues = new List<object>();
                    while (myFile.EndOfStream != true)
                    {
                    NEXTLOOP: ;
                        CurrentRow = myFile.ReadLine();

                        if (CurrentRow == null) continue;

                        Values = CurrentRow.Split(this.Separator.ToArray());
                      //  double ResValue;
                        LValues.Clear();
                        for (int i = RowShift; i < Values.Length; i++)
                        {
                            //bool TestValue = double.TryParse(Values[i], out ResValue);
                            //if (TestValue == false)
                            //{
                            //    RejectedRows++;
                            //    goto NEXTLOOP;
                            //}

                            if (Values[i] != "")
                                LValues.Add(Values[i]);
                            else
                                LValues.Add(null);

                        }
                        if (LValues.Count != ColumnNumber)
                        {
                            RejectedRows++;
                            goto NEXTLOOP;
                        }
                        for (int i = 0; i < LValues.Count; i++)
                        {
                            //this.Output[i].Add(LValues[i]);
                            if (this.Output[i].ListTags == null) this.Output[i].ListTags = new List<object>();
                            this.Output[i].ListTags.Add(LValues[i]);
                        }

                        if (IsContainRowNames) this.Output.ListRowNames.Add(Values[0]);
                    }
                }
                else
                {
                    List<double> LValues = new List<double>();
                    while (myFile.EndOfStream != true)
                    {
                    NEXTLOOP: ;
                        CurrentRow = myFile.ReadLine();

                        if (CurrentRow == null) continue;

                        Values = CurrentRow.Split(this.Separator.ToArray());
                        double ResValue;
                        LValues.Clear();
                        for (int i = RowShift; i < Values.Length; i++)
                        {
                            bool TestValue = double.TryParse(Values[i], out ResValue);
                            if (TestValue == false)
                            {
                                RejectedRows++;
                                goto NEXTLOOP;
                            }
                            LValues.Add(ResValue);

                        }
                        if (LValues.Count != ColumnNumber)
                        {
                            RejectedRows++;
                            goto NEXTLOOP;
                        }
                        for (int i = 0; i < LValues.Count; i++) this.Output[i].Add(LValues[i]);

                        if (IsContainRowNames) this.Output.ListRowNames.Add(Values[0]);

                    }
                }

                myFile.Close();
            }

            return FeedBackMessage;
        }
    }
}
