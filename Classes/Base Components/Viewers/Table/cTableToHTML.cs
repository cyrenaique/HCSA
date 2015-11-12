using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.IO;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using HCSAnalyzer.Classes.General_Types;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    class cTableToHTML : cComponent
    {
        public string FolderName = "";
        public bool IsDisplayUIForFilePath = false;
        //  public bool IsDisplayResult = false;

        cExtendedTable Input = null;

        public cTableToHTML()
        {
            base.Title = "Table to HTML";
            base.Info = "Write a table in a HTML file (if Tags are images, they are saved as well).";

            cPropertyType PT = new cPropertyType("Open HTML File ?", eDataType.BOOL);
            cProperty Prop1 = new cProperty(PT, null);
            Prop1.Info = "Open Web browser to display the generated HTML file";
            Prop1.SetNewValue((bool)true);
            base.ListProperties.Add(Prop1);


        }

        public void SetInputData(cExtendedTable InputTable)
        {
            this.Input = InputTable;
        }

        public cFeedBackMessage Run()
        {


            if (this.Input == null)
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message += ": No input defined!";
                return base.FeedBackMessage;
            }
            else
            {
                if (base.Start() == false)
                {
                    base.FeedBackMessage.IsSucceed = false;
                    return base.FeedBackMessage;
                }


                #region Properties Management
                object _firstValue = base.ListProperties.FindByName("Open HTML File ?");
                bool IsDisplayResult = true;
                if (_firstValue == null)
                {
                    base.GenerateError("-Open HTML File ?- not found !");
                    return base.FeedBackMessage;
                }
                try
                {
                    cProperty TmpProp = (cProperty)_firstValue;
                    IsDisplayResult = (bool)TmpProp.GetValue();
                }
                catch (Exception)
                {
                    base.GenerateError("-Open HTML File ?- cast didn't work");
                    return base.FeedBackMessage;
                }
                #endregion


                if (IsDisplayUIForFilePath)
                {
                    var dlg1 = new Ionic.Utils.FolderBrowserDialogEx();
                    dlg1.Description = "Select the folder containing your databases.";
                    dlg1.ShowNewFolderButton = true;
                    dlg1.ShowEditBox = true;
                    dlg1.ShowFullPathInEditBox = true;

                    DialogResult result = dlg1.ShowDialog();
                    if (result != DialogResult.OK)
                    {
                        FeedBackMessage.IsSucceed = false;
                        FeedBackMessage.Message = "Incorrect Folder Name.";
                        return FeedBackMessage;
                    }

                    string Path = dlg1.SelectedPath;
                    if (Directory.Exists(Path) == false)
                    {
                        FeedBackMessage.IsSucceed = false;
                        FeedBackMessage.Message = "Incorrect Folder Name.";
                        return FeedBackMessage;
                    }

                    //FolderBrowserDialog WorkingFolderDialog = new FolderBrowserDialog();
                    //WorkingFolderDialog.ShowNewFolderButton = true;
                    //WorkingFolderDialog.Description = "Select destination folder";
                    //if (WorkingFolderDialog.ShowDialog() != DialogResult.OK)
                    //{
                    //    FeedBackMessage.IsSucceed = false;
                    //    FeedBackMessage.Message = "Incorrect Folder Name.";
                    //    return FeedBackMessage;
                    //}
                    //FolderName = WorkingFolderDialog.SelectedPath;

                    FolderName = dlg1.SelectedPath;

                }

                string HTMLFileName = FolderName + "\\Index.html";
                Directory.CreateDirectory(FolderName + @"\\Images\\");

                int IdxImage = 0;

                #region Create HTML document
                using (StreamWriter myFile = new StreamWriter(HTMLFileName, false, Encoding.Default))
                {
                    // Export titles:  
                    string ToBeWritten = "<!DOCTYPE html><html><body><h1>Report : " + this.Input.Name + @"</h1><table border=1>";

                    string RowString = "<tr>";

                    // if ListTags associated to the table, then create a new column
                    if (this.Input.ListTags != null)
                    {
                        RowString += "<td>";
                        RowString += "Associated Tag";
                        RowString += "</td>";
                    }

                    // if ListRowNames then create another column
                    if (this.Input.ListRowNames != null)
                    {
                        RowString += "<td>";
                        RowString += this.Input.Name;
                        RowString += "</td>";
                    }

                    // ??? probleme column/row ???
                    for (int j = 0; j < this.Input.Count; j++)
                    {
                        RowString += "<td>";
                        RowString += this.Input[j].Name;
                        RowString += "</td>";
                    }

                    RowString += "</tr>";
                    ToBeWritten += RowString;

                    // Now let's process the table by itself
                    for (int i = 0; i < this.Input[0].Count; i++)
                    {
                        RowString = "<tr>";

                        // if List Tags, then include the associated object if it is an image
                        #region Include Tags
                        if (this.Input.ListTags != null)
                        {
                            RowString += "<td align=\"center\">";

                            if ((this.Input.ListTags[i] != null) && (this.Input.ListTags[i].GetType() == typeof(Bitmap)))
                            {
                                //  RowString += "Associated Tag"; 

                                string ImageName = FolderName + @"\Images\Image" + IdxImage.ToString() + ".jpg";
                                ((Bitmap)(this.Input.ListTags[i])).Save(ImageName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                RowString += "<img src=\"Images\\Image" + IdxImage.ToString() + ".jpg\">";
                                RowString += "</td>";

                                IdxImage++;

                                if (this.Input.ListRowNames != null)
                                {
                                    RowString += "<td>";
                                    RowString += this.Input.ListRowNames[i];
                                    RowString += "</td>";
                                }


                                for (int j = 0; j < this.Input.Count; j++)
                                {
                                    RowString += "<td>";
                                    RowString += this.Input[j][i].ToString();
                                    RowString += "</td>";
                                }

                                RowString += "</tr>";
                                ToBeWritten += RowString;


                                continue;
                            }

                            Chart A = null;
                            try
                            {
                                A = (Chart)this.Input.ListTags[i];
                            }
                            catch (Exception)
                            {
                                //throw;
                            }

                            if (A != null)
                            {

                                try
                                {
                                    int TmpWidth = A.Width;
                                    int TmpHeight = A.Height;

                                    A.Width = 500;
                                    A.Height = 300;

                                    string ImageName = FolderName + @"\Images\Image" + IdxImage.ToString() + ".jpg";
                                    ((Chart)this.Input.ListTags[i]).SaveImage(ImageName, ChartImageFormat.Jpeg);

                                    A.Width = TmpWidth;
                                    A.Height = TmpHeight;

                                    RowString += "<img src=\"Images\\Image" + IdxImage.ToString() + ".jpg\">";
                                    IdxImage++;
                                }
                                catch (Exception)
                                {
                                    RowString += "n.a.";
                                }


                            }
                            else
                            {
                                RowString += "n.a.";
                            }

                            RowString += "</td>";
                        }
                        #endregion

                        #region Include Names
                        if (this.Input.ListRowNames != null)
                        {
                            RowString += "<td>";
                            if (i >= this.Input.ListRowNames.Count)
                                RowString += "";
                            else
                                RowString += this.Input.ListRowNames[i];
                            RowString += "</td>";
                        }
                        #endregion


                        #region Include the values

                        for (int j = 0; j < this.Input.Count; j++)
                        {
                            RowString += "<td>";
                            if ((this.Input[j].ListTags != null) && (i < this.Input[j].Count))
                            {
                                Chart A = null;
                                try
                                {
                                    A = (Chart)this.Input[j].ListTags[i];
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }

                                if (A != null)
                                {

                                    try
                                    {
                                        int TmpWidth = A.Width;
                                        int TmpHeight = A.Height;

                                        A.Width = 500;
                                        A.Height = 300;

                                        string ImageName = FolderName + @"\Images\Image" + IdxImage.ToString() + ".jpg";
                                        A.SaveImage(ImageName, ChartImageFormat.Jpeg);

                                        A.Width = TmpWidth;
                                        A.Height = TmpHeight;

                                        RowString += "<img src=\"Images\\Image" + IdxImage.ToString() + ".jpg\">";
                                        IdxImage++;
                                    }
                                    catch (Exception)
                                    {
                                        RowString += "n.a.";
                                    }


                                }


                            }
                            else
                            {
                                RowString += this.Input[j][i].ToString();
                            }
                            RowString += "</td>";

                        }
                        #endregion

                        RowString += "</tr>";
                        ToBeWritten += RowString;
                    }

                    ToBeWritten += "</table>";

                    ToBeWritten += "</body></html>";
                    myFile.Write(ToBeWritten);
                    myFile.Close();
                }
                #endregion

                if (IsDisplayResult)
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    if (cGlobalInfo.OptionsWindow.radioButtonIE.Checked)
                        proc.StartInfo.FileName = "iexplore";
                    else
                        proc.StartInfo.FileName = "chrome";
                    proc.StartInfo.Arguments = HTMLFileName;
                    proc.Start();
                }

            }
            return base.FeedBackMessage;
        }

    }
}
