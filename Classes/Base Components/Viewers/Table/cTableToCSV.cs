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
//using Microsoft.Office.Interop.Excel;
using ImageAnalysis;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{

    public enum eFileType { CSV, XLS };

    public class cTableToFile : cComponent
    {
        cExtendedTable Input = null;

        public int DigitNumber = 2;
        public string Separator = ",";
        public eFileType FileType = eFileType.CSV;
        string FileExtension = ".csv";
        public string FilePath = "";
        public bool IsDisplayUIForFilePath = false;
        public bool IsRunEXCEL = false;


        public cTableToFile()
        {
            Title = "Table to File";
        }

        public void SetInputData(cExtendedTable MyData)
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

            if (IsDisplayUIForFilePath)
            {
                SaveFileDialog CurrSaveFileDialog = new SaveFileDialog();
                CurrSaveFileDialog.Filter = "CSV file (*.csv)|*.csv| XLS file (*.xls)|*.xls";
                System.Windows.Forms.DialogResult Res = CurrSaveFileDialog.ShowDialog();
                if (Res != System.Windows.Forms.DialogResult.OK)
                {
                    FeedBackMessage.IsSucceed = false;
                    FeedBackMessage.Message = "Incorrect File Name.";
                    return FeedBackMessage;
                }
                FilePath = CurrSaveFileDialog.FileName;
                if (CurrSaveFileDialog.FilterIndex == 0)
                    this.FileType = eFileType.CSV;
                else
                    this.FileType = eFileType.XLS;
            }


            if (this.FileType == eFileType.CSV)
            {
                using (StreamWriter myFile = new StreamWriter(FilePath, false, Encoding.Default))
                {
                    // Export titles:  
                    string sHeaders = "";

                    if (this.Input.ListRowNames != null)
                        sHeaders += this.Separator;

                    for (int j = 0; j < this.Input.Count; j++) { sHeaders = sHeaders.ToString() + this.Input[j].Name + Separator; }
                    sHeaders = sHeaders.Remove(sHeaders.Length - 1);

                    myFile.WriteLine(sHeaders);

                    // Export data.  
                    for (int i = 0; i < this.Input[0].Count; i++)
                    {
                        string stLine = "";

                        if (this.Input.ListRowNames != null)
                            stLine += this.Input.ListRowNames[i] + this.Separator;

                        for (int j = 0; j < this.Input.Count; j++) { stLine = stLine.ToString() + this.Input[j][i] + Separator; }

                        stLine = stLine.Remove(stLine.Length - 1);
                        myFile.WriteLine(stLine);
                    }
                    myFile.Close();
                }
            }
            else if (this.FileType == eFileType.XLS)
            {
                ExportToExcel(this.Input, FilePath);


                //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                //Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                //Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                //object misValue = System.Reflection.Missing.Value;

                //// xlApp = new Excel.ApplicationClass();
                //xlWorkBook = xlApp.Workbooks.Add(misValue);
                //xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                //// Microsoft.Office.Interop.Excel.Range cell = GetMyPictureCELL(taperSheet);

                ////xlWorkSheet.Cells[j + 2, 1].AddComment(" ");
                ////xlWorkSheet.Cells[j + 2, 1].Comment.Shape.Fill.UserPicture(imagenames[j]);

                //xlWorkBook.SaveAs(FilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                //               Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                //xlWorkBook.Close(true, misValue, misValue);
                //xlApp.Quit();
                //releaseObject(xlWorkSheet);
                //releaseObject(xlWorkBook);
                //releaseObject(xlApp);


            }


            if (this.IsRunEXCEL)
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = "excel";
                proc.StartInfo.Arguments = FilePath;
                proc.Start();

            }

            return FeedBackMessage;
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }


        public static void ExportToExcel(cExtendedTable Tbl, string ExcelFilePath)
        {
            //try
            //{
            //    //if (Tbl == null || Tbl.Columns.Count == 0)
            //    //    throw new Exception("ExportToExcel: Null or empty input table!\n");

            //    // load excel, and create a new workbook
            //    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            //    excelApp.Workbooks.Add();

            //    // single worksheet
            //    Microsoft.Office.Interop.Excel._Worksheet workSheet = excelApp.ActiveSheet;

            //    // column headings
            //    for (int i = 0; i < Tbl.Count; i++)
            //    {
            //        workSheet.Cells[1, (i+2)] = Tbl[i].Name;
            //    }

            //    if (Tbl.ListRowNames != null)
            //    {
            //        for (int i = 0; i < Tbl.ListRowNames.Count; i++)
            //        {
            //            if(Tbl.ListRowNames[i]!=null)
            //                workSheet.Cells[(i + 2), 1] = Tbl.ListRowNames[i];
            //        }
            //    }

            //    for (int j = 0; j < Tbl.Count; j++)
            //    for (int i = 0; i < Tbl[j].Count; i++)
            //    {
            //       workSheet.Cells[(i + 2), (j + 2)] = Tbl[j][i];
            //       if (Tbl[j].ListTags[i] != null)
            //       {
            //           if (Tbl[j].ListTags[i].GetType() == typeof(cImage))
            //           {
            //               //workSheet.Cells[(i + 2), (j + 2)].AddComment(" ");
            //               cImage TmpImage = (cImage)Tbl[j].ListTags[i];
            //               workSheet.Cells[(i + 2), (j + 2)].Comment.Shape.Fill.UserPicture(TmpImage.GetBitmap(1,null,null,null));
            //           }
            //           if (Tbl[j].ListTags[i].GetType() == typeof(cWell))
            //           {
            //               cWell TmpWell = (cWell)Tbl[j].ListTags[i];
            //               workSheet.Cells[(i + 2), (j + 2)].AddComment(TmpWell.GetShortInfo());
            //               //workSheet.Cells[(i + 2), (j + 2)].Comment.Shape.Fill.UserPicture(imagenames[j]);
            //           }
                   
            //       }


            //    }

            //    // check fielpath
            //    if (ExcelFilePath != null && ExcelFilePath != "")
            //    {
            //        try
            //        {
            //            workSheet.SaveAs(ExcelFilePath);
            //            excelApp.Quit();
            //            MessageBox.Show("Excel file saved!");
            //        }
            //        catch (Exception ex)
            //        {
            //            throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
            //                + ex.Message);
            //        }
            //    }
            //    else    // no filepath is given
            //    {
            //        excelApp.Visible = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("ExportToExcel: \n" + ex.Message);
            //}
        }




    }
}
