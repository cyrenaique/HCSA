using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibPlateAnalysis;
using ImageAnalysis;
using System.IO;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.Base_Classes.Data;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Data.OleDb;
using System.Data;
using System.Xml;

namespace HCSAnalyzer.Classes.General_Types
{

    public enum eImagingPlatformType { HARMONY, HARMONY35, IMAGEXPRESS, CELLOMICS, INCELL, CV7000, BUILTIN, MANUAL };

    public class cImageAccessor
    {
        public string InitialPath = "";
        public eImagingPlatformType ImagingPlatformType;
        public int NumberOfChannels = 1;
        public int Field = 1;
        public bool IsThumbnail = false;
        // public List<cImageMetaInfo> ListImageMetaInfo = null;

        public cImageAccessor(eImagingPlatformType ImagingPlatformType)
        {
            //this.InitialPath = InitialPath;
            this.ImagingPlatformType = ImagingPlatformType;
        }

        public void ConnectToAccess()
        {
            System.Data.OleDb.OleDbConnection conn = new
                System.Data.OleDb.OleDbConnection();
            // TODO: Modify the connection string and include any
            // additional required properties for your database.
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                @"Data source= C:\Documents and Settings\username\" +
                @"My Documents\AccessFile.mdb";
            try
            {
                conn.Open();
                // Insert code to process data.
            }
            catch (Exception ex)
            {
                //  MessageBox.Show("Failed to connect to data source");
            }
            finally
            {
                conn.Close();
            }
        }

        public List<cImageMetaInfo> GetImageInfo(object Sender)
        {
            //   string FileName = "";
            #region Operetta
            // The image file name is composed as follows:
            // r02c07f01p01rc1-ch1sk1fk1fl1.tiff
            // r=row, c=coloumn, f=field, p=plane, rc= record (channel)
            // (second part of the file name currently not in use)
            if (this.ImagingPlatformType == eImagingPlatformType.HARMONY)
            {
                if (InitialPath == "") return null;
                if (Sender.GetType() == typeof(cSingleBiologicalObject))
                {
                    cSingleBiologicalObject SingleBiologicalObject = (cSingleBiologicalObject)Sender;

                    IEnumerable<string> ListDir = Directory.EnumerateDirectories(InitialPath, "*" + SingleBiologicalObject.AssociatedWell.AssociatedPlate.GetName() + "__*");
                    if (ListDir.Count() != 1) return null;

                    List<cImageMetaInfo> ListImageMetaInfo = new List<cImageMetaInfo>();

                    string NewDir = ListDir.ElementAt(0);

                    for (int Channel = 1; Channel <= NumberOfChannels; Channel++)
                    {
                        // get the well information
                        int c = SingleBiologicalObject.AssociatedWell.GetPosX();
                        int r = SingleBiologicalObject.AssociatedWell.GetPosY();
                        int f = SingleBiologicalObject.ImageField; // field
                        int p = 1; // plane

                        cImageMetaInfo TmpInfo = new cImageMetaInfo();


                        string Name = "r" + r.ToString("D2") +
                                      "c" + c.ToString("D2") +
                                      "f" + f.ToString("D2") +
                                      "p" + p.ToString("D2") +
                                      "rc" + Channel.ToString() +
                                      "-ch1sk1fk1fl1.tiff";

                        TmpInfo.FileName = NewDir + "\\Images\\" + Name;// + "|";
                        ListImageMetaInfo.Add(TmpInfo);
                    }


                    return ListImageMetaInfo;


                }
                else if (Sender.GetType() == typeof(cWell))
                {
                    cWell WellObject = (cWell)Sender;
                    List<cImageMetaInfo> ListImageMetaInfo = new List<cImageMetaInfo>();

                    IEnumerable<string> ListDir = Directory.EnumerateDirectories(InitialPath, "*Plate4__*");
                    if (ListDir.Count() != 1) return null;

                    string NewDir = ListDir.ElementAt(0);

                    for (int Channel = 1; Channel <= NumberOfChannels; Channel++)
                    {
                        cImageMetaInfo TmpInfo = new cImageMetaInfo();
                        // get the well information
                        int c = WellObject.GetPosX();
                        int r = WellObject.GetPosY();
                        int f = this.Field; // field
                        int p = 1; // plane

                        string Name = "r" + r.ToString("D2") +
                                      "c" + c.ToString("D2") +
                                      "f" + f.ToString("D2") +
                                      "p" + p.ToString("D2") +
                                      "rc" + Channel.ToString() +
                                      "-ch1sk1fk1fl1.tiff";
                        TmpInfo.FileName = NewDir + "\\Images\\" + Name;
                        //FileName += NewDir + "\\Images\\" + Name + "|";

                        ListImageMetaInfo.Add(TmpInfo);
                    }

                    return ListImageMetaInfo;
                }
            }
            #endregion
            #region Harmony3.5
            if (this.ImagingPlatformType == eImagingPlatformType.HARMONY35)
            {
                string PlateName;
                cWell WellObject;
                if (Sender.GetType() == typeof(cSingleBiologicalObject))
                {
                    cSingleBiologicalObject SingleBiologicalObject = (cSingleBiologicalObject)Sender;
                    WellObject = SingleBiologicalObject.AssociatedWell;
                    PlateName = WellObject.AssociatedPlate.GetName();
                    this.Field = SingleBiologicalObject.ImageField - 1;
                }
                else if (Sender.GetType() == typeof(cWell))
                {
                    WellObject = (cWell)Sender;
                    PlateName = WellObject.AssociatedPlate.GetName();
                }
                else
                    return null;

                List<cImageMetaInfo> ListImageMetaInfo = new List<cImageMetaInfo>();

                // Identify the directory of the associated plate
                IEnumerable<string> ListDir = Directory.EnumerateDirectories(InitialPath, PlateName + "*", SearchOption.AllDirectories);
                if (ListDir.Count() != 1) return null;

                string CurrentPathForFile = ListDir.ElementAt(0) + "\\indexfile.txt";
                if (File.Exists(CurrentPathForFile) == false) return null;

                cCSVToTable CSVTable = new cCSVToTable();
                CSVTable.SetInputData(CurrentPathForFile);
                CSVTable.IsContainColumnHeaders = true;
                CSVTable.AddAsObject = true;
                CSVTable.Separator = "\t";
                CSVTable.Run();

                cExtendedTable ET = CSVTable.GetOutPut();

                int c = WellObject.GetPosX();
                int r = WellObject.GetPosY();
                int f = this.Field + 1;

                for (int IdxRow = 0; IdxRow < ET[0].ListTags.Count; IdxRow++)
                {
                    int currentRow = int.Parse(ET[0].ListTags[IdxRow].ToString());

                    if (currentRow == r)
                    {
                        int currentCol = int.Parse(ET[1].ListTags[IdxRow].ToString());
                        if (currentCol == c)
                        {
                            int currentField = int.Parse(ET[4].ListTags[IdxRow].ToString());
                            if (currentField == f)
                            {
                                while (int.Parse(ET[4].ListTags[IdxRow].ToString()) == f)
                                {
                                    cImageMetaInfo TmpInfo = new cImageMetaInfo();
                                    //FileName += ET[8].ListTags[IdxRow].ToString() + "|";

                                    for (int i = 0; i < ET.Count; i++)
                                    {
                                        if (ET[i].Name == "URL")
                                            TmpInfo.FileName = ET[i].ListTags[IdxRow].ToString();

                                        if (ET[i].Name == "Channel Name")
                                            TmpInfo.Name = ET[i].ListTags[IdxRow].ToString();

                                        if (ET[i].Name == "ImageResolutionX [m]")
                                            TmpInfo.ResolutionX = double.Parse(ET[i].ListTags[IdxRow].ToString()) * 10E6;

                                        if (ET[i].Name == "ImageResolutionY [m]")
                                            TmpInfo.ResolutionY = double.Parse(ET[i].ListTags[IdxRow].ToString()) * 10E6;

                                        if (ET[i].Name == "PositionX [m]")
                                            TmpInfo.PositionX = double.Parse(ET[i].ListTags[IdxRow].ToString()) * 10E6;

                                        if (ET[i].Name == "PositionY [m]")
                                            TmpInfo.PositionY = double.Parse(ET[i].ListTags[IdxRow].ToString()) * 10E6;


                                    }

                                    ListImageMetaInfo.Add(TmpInfo);


                                    IdxRow++;
                                }
                                goto _END;
                            }
                        }
                    }
                }
            _END: ;
                return ListImageMetaInfo;
            }
            #endregion
            #region ImageXPress
            // The image file name is composed as follows:
            // 140128_F21_s1_w174B98CD5-110B-4D9E-AF8F-326343D3C28D.tif
            // <=> DATE_WELL_site_wavelenght_CheckSum
            if (this.ImagingPlatformType == eImagingPlatformType.IMAGEXPRESS)
            {
                if (Sender.GetType() == typeof(cSingleBiologicalObject))
                {
                    cSingleBiologicalObject SingleBiologicalObject = (cSingleBiologicalObject)Sender;

                    IEnumerable<string> ListDir = null;
                    try
                    {
                        ListDir = Directory.EnumerateDirectories(InitialPath, SingleBiologicalObject.AssociatedWell.AssociatedPlate.GetName(), SearchOption.AllDirectories);
                    }
                    catch (Exception e)
                    {
                        cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(e.Message + "\n");
                        return null;
                    }

                    string NewDir;
                    try
                    {
                        NewDir = ListDir.ElementAt(0);
                    }
                    catch (Exception)
                    {
                        return null;
                        throw;
                    }
                    // get the well information
                    string Pos = SingleBiologicalObject.AssociatedWell.GetPos();

                    // IsThumb = "";
                    //140128_F21_s1_w1_thumbC02DD956-A29B-4B59-B231-A8DD0AAE37D1.tif

                    List<cImageMetaInfo> ListImageMetaInfo = new List<cImageMetaInfo>();

                    for (int Channel = 1; Channel <= NumberOfChannels; Channel++)
                    {
                        string Name = "*_" + Pos + "_s" + (this.Field + 1) + "_w" + Channel + "*.tif";
                        if (cGlobalInfo.OptionsWindow.numericUpDownImageAccessNumberOfFields.Value == 1)
                            Name = "*_" + Pos + "_w" + Channel + "*.tif";

                        ListDir = Directory.EnumerateFiles(NewDir, Name, SearchOption.AllDirectories);

                        int FileCount = ListDir.Count();
                        if (FileCount == 0) return null;

                        string FinalName = ListDir.ElementAt(0);

                        cImageMetaInfo TmpMetaInfo = new cImageMetaInfo();

                        TmpMetaInfo.FileName = FinalName;

                        ListImageMetaInfo.Add(TmpMetaInfo);
                    }

                    return ListImageMetaInfo;
                }
                else if (Sender.GetType() == typeof(cWell))
                {
                    cWell WellObject = (cWell)Sender;
                    IEnumerable<string> ListDir = null;
                    try
                    {
                        ListDir = Directory.EnumerateDirectories(InitialPath, WellObject.AssociatedPlate.GetName(), SearchOption.AllDirectories);
                    }
                    catch (Exception e)
                    {
                        cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(e.Message + "\n");
                        return null;
                    }

                    //if (ListDir.Count() != 1) return "Plate Not Found";

                    //string[] ListDIr = Directory.GetDirectories(InitialPath, WellObject.AssociatedPlate.GetName(), SearchOption.AllDirectories);


                    //if (ListDir.Count() > 1) return "Multiple Occurences Found";

                    string NewDir;
                    try
                    {
                        NewDir = ListDir.ElementAt(0);
                    }
                    catch (Exception)
                    {
                        return null;
                        throw;
                    }
                    // get the well information
                    string Pos = WellObject.GetPos();

                    // IsThumb = "";
                    //140128_F21_s1_w1_thumbC02DD956-A29B-4B59-B231-A8DD0AAE37D1.tif

                    List<cImageMetaInfo> ListImageMetaInfo = new List<cImageMetaInfo>();

                    for (int Channel = 1; Channel <= NumberOfChannels; Channel++)
                    {
                        string Name = "*_" + Pos + "_s" + (this.Field + 1) + "_w" + Channel + "*.tif";
                        if(cGlobalInfo.OptionsWindow.numericUpDownImageAccessNumberOfFields.Value==1)
                            Name = "*_" + Pos + "_w" + Channel + "*.tif";

                        ListDir = Directory.EnumerateFiles(NewDir, Name, SearchOption.AllDirectories);

                        int FileCount = ListDir.Count();
                        if (FileCount == 0) return null;

                        string FinalName = ListDir.ElementAt(0);

                        cImageMetaInfo TmpMetaInfo = new cImageMetaInfo();


                        TmpMetaInfo.FileName = FinalName;


                        ListImageMetaInfo.Add(TmpMetaInfo);
                    }


                    return ListImageMetaInfo;

                }
            }
            #endregion
            #region Cellomics
            // The image file name is composed as follows:
            // 
            // <=> DATE_WELL_site_wavelenght_CheckSum
            if (this.ImagingPlatformType == eImagingPlatformType.CELLOMICS)
            {
                if (Sender.GetType() == typeof(cSingleBiologicalObject))
                {
                    cSingleBiologicalObject SingleBiologicalObject = (cSingleBiologicalObject)Sender;

                    IEnumerable<string> ListDir = Directory.EnumerateDirectories(InitialPath, "*" + SingleBiologicalObject.AssociatedWell.AssociatedPlate.GetName() + "_*");

                    if (ListDir.Count() != 1) return null;

                    string NewDir = ListDir.ElementAt(0);

                    List<cImageMetaInfo> ListImageMetaInfo = new List<cImageMetaInfo>();


                    for (int Channel = 1; Channel <= NumberOfChannels; Channel++)
                    {
                        // get the well information
                        int c = SingleBiologicalObject.AssociatedWell.GetPosX();
                        int r = SingleBiologicalObject.AssociatedWell.GetPosY();
                        int f = SingleBiologicalObject.ImageField; // field
                        int p = 1; // plane

                        string Name = "r" + r.ToString("D2") +
                                      "c" + c.ToString("D2") +
                                      "f" + f.ToString("D2") +
                                      "p" + p.ToString("D2") +
                                      "rc" + Channel.ToString() +
                                      "-ch1sk1fk1fl1.tif";

                        cImageMetaInfo TmpInfo = new cImageMetaInfo();

                        TmpInfo.FileName = NewDir + "\\Images\\" + Name;


                        ListImageMetaInfo.Add(TmpInfo);
                        //FileName += NewDir + "\\Images\\" + Name + "|";
                    }

                    return ListImageMetaInfo;
                }
                else if (Sender.GetType() == typeof(cWell))
                {
                    cWell WellObject = (cWell)Sender;
                    IEnumerable<string> ListDir = null;
                    try
                    {
                        ListDir = Directory.EnumerateDirectories(InitialPath, WellObject.AssociatedPlate.GetName(), SearchOption.AllDirectories);
                    }
                    catch (Exception e)
                    {
                        cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(e.Message + "\n");
                        return null;
                    }

                    //if (ListDir.Count() != 1) return "Plate Not Found";

                    //string[] ListDIr = Directory.GetDirectories(InitialPath, WellObject.AssociatedPlate.GetName(), SearchOption.AllDirectories);


                    //if (ListDir.Count() > 1) return "Multiple Occurences Found";

                    string NewDir;
                    try
                    {
                        NewDir = ListDir.ElementAt(0);
                    }
                    catch (Exception)
                    {
                        return null;
                        throw;
                    }


                    // get the well information
                    string Pos = WellObject.GetPos();

                    // MFGTMP-PC140320150001_A01f00d0.C01
                    List<cImageMetaInfo> ListImageMetaInfo = new List<cImageMetaInfo>();
                    OleDbConnection MyConnection = new OleDbConnection();
                    try
                    {
                        String connectionString = @"Provider=Microsoft.JET.OlEDB.4.0;"
                       + "Data Source=" + NewDir + "\\" + WellObject.AssociatedPlate.GetName() + ".MDB";
                        MyConnection = new OleDbConnection(connectionString);
                        MyConnection.Open();


                        // get the real plate name
                        //OleDbDataAdapter da = new OleDbDataAdapter("Select * FROM asnPlate", MyConnection);
                        //DataSet ds = new DataSet();
                        //da.Fill(ds);

                        //DataTable dt = ds.Tables[0];
                        //   string PlateName = dt.Rows[0]["Name"].ToString();
                        OleDbDataAdapter da = new OleDbDataAdapter("Select * FROM asnProtocolChannel", MyConnection);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        DataTable dt = ds.Tables[0];
                        this.NumberOfChannels = dt.Rows.Count;

                        da = new OleDbDataAdapter("Select * FROM asnProtocolChannel", MyConnection);
                        ds = new DataSet();
                        da.Fill(ds);

                        dt = ds.Tables[0];

                        //for (int i = 1; i <= this.NumberOfChannels; i++)
                        //{
                        //    
                        //}
                        for (int Channel = 0; Channel < NumberOfChannels; Channel++)
                        {
                            string ChannelName = dt.Rows[Channel]["Dye"].ToString();
                            string Name = WellObject.AssociatedPlate.GetName() + "_" + Pos + "f" + (this.Field + 1).ToString("00") + "d" + Channel + ".C01";
                            cImageMetaInfo TmpMetaInfo = new cImageMetaInfo();

                            TmpMetaInfo.Name = ChannelName;
                            TmpMetaInfo.FileName = NewDir + "\\" + Name;
                            ListImageMetaInfo.Add(TmpMetaInfo);
                        }

                        MyConnection.Close();
                    }
                    catch
                    {
                        return null;
                        throw;
                    }








                    return ListImageMetaInfo;

                }
            }
            #endregion
            #region INCell
            // The image file name is composed as follows:
            // 140128_F21_s1_w174B98CD5-110B-4D9E-AF8F-326343D3C28D.tif
            // <=> DATE_WELL_site_wavelenght_CheckSum
            if (this.ImagingPlatformType == eImagingPlatformType.INCELL)
            {
                cWell WellObject = null;

                if (Sender.GetType() == typeof(cSingleBiologicalObject))
                {
                    cSingleBiologicalObject SingleBiologicalObject = (cSingleBiologicalObject)Sender;
                    WellObject = SingleBiologicalObject.AssociatedWell;
                    this.IsThumbnail = false;   // cannot have thumbnail of a biological object
                }
                else if (Sender.GetType() == typeof(cWell))
                {
                    WellObject = (cWell)Sender;
                }

                IEnumerable<string> ListDir = null;
                try
                {
                    ListDir = Directory.EnumerateDirectories(InitialPath, WellObject.AssociatedPlate.GetName(), SearchOption.AllDirectories);
                }
                catch (Exception e)
                {
                    cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(e.Message + "\n");
                    return null;
                }

                string NewDir;
                try
                {
                    NewDir = ListDir.ElementAt(0);
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }

                // get the well information
                string Pos = WellObject.GetPos();

                List<cImageMetaInfo> ListImageMetaInfo = new List<cImageMetaInfo>();
                string FinalPos = Pos[0] + " - " + WellObject.GetPosX().ToString("D2");
                
                // find the channels Names
                string Prefix = FinalPos + "(fld " + (this.Field + 1).ToString("D2") + " wv ";
                string Name = Prefix;

                if (IsThumbnail)
                {
                    Name += "*.jpg";
                    NewDir += "\\thumbs";
                }
                else
                    Name += "*.tif";

                string[] ListChannels = Directory.GetFiles(NewDir, Name, SearchOption.TopDirectoryOnly);
                //  if(NumberOfChannels>ListChannels.Count()) NumberOfChannels = ListChannels.Count();
                NumberOfChannels = ListChannels.Count();

                List<string> ListChannelNames = new List<string>();
                string[] Sep = new string[1];
                Sep[0] = "\\";

                for (int Channel = 0; Channel < NumberOfChannels; Channel++)
                {
                    Sep[0] = "\\";
                    string[] SplittedNames = ListChannels[Channel].Split(Sep, StringSplitOptions.None);
                    string TmpString = SplittedNames[SplittedNames.Length - 1];
                    TmpString = TmpString.Remove(0, Prefix.Length);
                    Sep[0] = " - ";
                    SplittedNames = TmpString.Split(Sep, StringSplitOptions.None);
                    ListChannelNames.Add(SplittedNames[0]);
                }

                for (int Channel = 0; Channel < NumberOfChannels; Channel++)
                {
                    string FinalName = ListChannels[Channel];

                    cImageMetaInfo TmpMetaInfo = new cImageMetaInfo();

                    TmpMetaInfo.FileName = FinalName;
                    TmpMetaInfo.Name = ListChannelNames[Channel];
                    TmpMetaInfo.ResolutionX = 1;
                    TmpMetaInfo.ResolutionY = 1;
                    TmpMetaInfo.ResolutionZ = 1;

                    ListImageMetaInfo.Add(TmpMetaInfo);
                }
                return ListImageMetaInfo;

            }
            #endregion
            #region CV7000
            // The image file name is composed as follows:
            // 140128_F21_s1_w174B98CD5-110B-4D9E-AF8F-326343D3C28D.tif
            // <=> DATE_WELL_site_wavelenght_CheckSum
            if (this.ImagingPlatformType == eImagingPlatformType.CV7000)
            {
                cWell WellObject = null;

                if (Sender.GetType() == typeof(cSingleBiologicalObject))
                {
                    cSingleBiologicalObject SingleBiologicalObject = (cSingleBiologicalObject)Sender;
                    WellObject = SingleBiologicalObject.AssociatedWell;
                    this.IsThumbnail = false;   // cannot have thumbnail of a biological object
                }
                else if (Sender.GetType() == typeof(cWell))
                {
                    WellObject = (cWell)Sender;
                }
                
                List<string> ListDir = new List<string>();
                string ListDir2 = "";
                try
                {
                    //ListDir.Add(Directory.GetDirectories(InitialPath+"\\"+WellObject.GetShortInfo().Split('[')[1].Split(']')[0])[0]);
                    //for (int item=0; item<ListDir.Count;item++)
                    //{

                    //    ListDir2.Add(Directory.GetDirectories(ListDir[item]).ToList());
                    //}
                    ListDir2 = InitialPath + "\\" + WellObject.GetShortInfo().Split('[')[1].Split(']')[0];

                }
                catch (Exception e)
                {
                    cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(e.Message + "\n");
                    return null;
                }

                string NewDir2;
                try
                {
                    //NewDir = ListDir.ElementAt(0);
                    NewDir2 = ListDir2;
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }

                // get the well information
                string Pos = WellObject.GetPos();

                List<cImageMetaInfo> ListImageMetaInfo = new List<cImageMetaInfo>();
                
                
                string FinalPos = "*_" +Pos + "_T0001F" +  (this.Field + 1).ToString("D3")+ "L01A";

                // find the channels Names
                string Prefix = FinalPos;
                string Name = Prefix;

                Name += "*Z01C*.tif";
                //string NewDir2 = NewDir + "\\DMD plate 5\\";
                string[] ListChannels = Directory.GetFiles(NewDir2, Name, SearchOption.AllDirectories);
                //  if(NumberOfChannels>ListChannels.Count()) NumberOfChannels = ListChannels.Count();
                NumberOfChannels = ListChannels.Count();

                List<string> ListChannelNames = new List<string>();
                string[] Sep = new string[1];
                Sep[0] = "\\";

                for (int Channel = 0; Channel < NumberOfChannels; Channel++)
                {
                    Sep[0] = "\\";
                    string[] SplittedNames = ListChannels[Channel].Split(Sep, StringSplitOptions.None);
                    string TmpString = SplittedNames[SplittedNames.Length - 1];
                    TmpString = TmpString.Remove(0, Prefix.Length);
                    Sep[0] = " - ";
                    SplittedNames = TmpString.Split(Sep, StringSplitOptions.None);
                    ListChannelNames.Add(SplittedNames[0]);
                }

               
                string XMLFile = NewDir2 + "\\MeasurementDetail.mrf";

                if (File.Exists(XMLFile))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(XMLFile);
                    for (int Channel = 0; Channel < NumberOfChannels; Channel++)
                    {
                        string FinalName = ListChannels[Channel];

                        cImageMetaInfo TmpMetaInfo = new cImageMetaInfo();
                        TmpMetaInfo.FileName = FinalName;
                        TmpMetaInfo.Name = xmlDoc.ChildNodes[1].ChildNodes[1].Attributes["bts:Ch"].Value;//ListChannelNames[Channel];
                        TmpMetaInfo.ResolutionX = double.Parse(xmlDoc.ChildNodes[1].ChildNodes[1].Attributes["bts:HorizontalPixelDimension"].Value);//1;
                        TmpMetaInfo.ResolutionY = double.Parse(xmlDoc.ChildNodes[1].ChildNodes[1].Attributes["bts:VerticalPixelDimension"].Value);//1;
                        TmpMetaInfo.ResolutionZ = 1;
                        ListImageMetaInfo.Add(TmpMetaInfo);
                    }
                }
                else
                {
                    for (int Channel = 0; Channel < NumberOfChannels; Channel++)
                    {
                        string FinalName = ListChannels[Channel];

                        cImageMetaInfo TmpMetaInfo = new cImageMetaInfo();
                        TmpMetaInfo.FileName = FinalName;
                        TmpMetaInfo.Name = ListChannelNames[Channel];
                        TmpMetaInfo.ResolutionX = 1;
                        TmpMetaInfo.ResolutionY = 1;
                        TmpMetaInfo.ResolutionZ = 1;
                        ListImageMetaInfo.Add(TmpMetaInfo);
                    }


                }
                return ListImageMetaInfo;

            }
            #endregion
            #region Built In
            // The image file name is composed as follows:
            // r[run].p[plate].w[well].s[site]_channel
            if (this.ImagingPlatformType == eImagingPlatformType.BUILTIN)
            {
                cWell WellObject = null;

                if (Sender.GetType() == typeof(cSingleBiologicalObject))
                {
                    cSingleBiologicalObject SingleBiologicalObject = (cSingleBiologicalObject)Sender;
                    WellObject = SingleBiologicalObject.AssociatedWell;
                    this.IsThumbnail = false;   // cannot have thumbnail of a biological object
                }
                else if (Sender.GetType() == typeof(cWell))
                {
                    WellObject = (cWell)Sender;
                }

                //IEnumerable<string> ListDir = null;
                //try
                //{
                //    ListDir = Directory.EnumerateDirectories(InitialPath, WellObject.AssociatedPlate.GetName(), SearchOption.AllDirectories);
                //}
                //catch (Exception e)
                //{
                //    cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(e.Message + "\n");
                //    return null;
                //}

                //string NewDir;
                //try
                //{
                //    NewDir = ListDir.ElementAt(0);
                //}
                //catch (Exception)
                //{
                //    return null;
                //    throw;
                //}

                // get the well information
                string Pos = WellObject.GetPos();
                
                List<cImageMetaInfo> ListImageMetaInfo = new List<cImageMetaInfo>();


                string FinalPos = "*.p"+ WellObject.AssociatedPlate.GetName()+ ".w" + Pos + ".s" +(this.Field + 1).ToString()+"_*.*";


                string[] ListChannels = Directory.GetFiles(InitialPath, FinalPos, SearchOption.TopDirectoryOnly);
                //  if(NumberOfChannels>ListChannels.Count()) NumberOfChannels = ListChannels.Count();
                NumberOfChannels = ListChannels.Count();

                List<string> ListChannelNames = new List<string>();
                string[] Sep = new string[1];
                Sep[0] = "\\";

                for (int Channel = 0; Channel < NumberOfChannels; Channel++)
                {
                    Sep[0] = "\\";
                    string[] SplittedNames = ListChannels[Channel].Split(Sep, StringSplitOptions.None);
                    string TmpString = SplittedNames[SplittedNames.Length - 1];
                     Sep[0] = "_";
                    SplittedNames = TmpString.Split(Sep, StringSplitOptions.None);
                    Sep[0] = ".";
                    SplittedNames = SplittedNames[1].Split(Sep, StringSplitOptions.None);
                    ListChannelNames.Add(SplittedNames[0]);
                }


                
                    for (int Channel = 0; Channel < NumberOfChannels; Channel++)
                    {
                        string FinalName = ListChannels[Channel];

                        cImageMetaInfo TmpMetaInfo = new cImageMetaInfo();
                        TmpMetaInfo.FileName = FinalName;
                        TmpMetaInfo.Name = ListChannelNames[Channel];
                        TmpMetaInfo.ResolutionX = 1;
                        TmpMetaInfo.ResolutionY = 1;
                        TmpMetaInfo.ResolutionZ = 1;
                        ListImageMetaInfo.Add(TmpMetaInfo);
                    }


                
                return ListImageMetaInfo;

            }
            #endregion
            #region Manual
            else if (this.ImagingPlatformType == eImagingPlatformType.MANUAL)
            {
                List<cImageMetaInfo> ListImageMetaInfo = new List<cImageMetaInfo>();

                cImageMetaInfo TmpMetaInfo = new cImageMetaInfo();

                if (Sender.GetType() == typeof(cSingleBiologicalObject))
                {
                    cSingleBiologicalObject SingleBiologicalObject = (cSingleBiologicalObject)Sender;
                    TmpMetaInfo.FileName = SingleBiologicalObject.AssociatedWell.Info;
                    //FileName += SingleBiologicalObject.AssociatedWell.Info;
                }
                if (Sender.GetType() == typeof(cWell))
                {
                    cWell Well = (cWell)Sender;
                    TmpMetaInfo.FileName = Well.Info;
                    //FileName += Well.Info;
                }

                ListImageMetaInfo.Add(TmpMetaInfo);
                return ListImageMetaInfo;

            }
            #endregion
            return null;
        }

    }
}
