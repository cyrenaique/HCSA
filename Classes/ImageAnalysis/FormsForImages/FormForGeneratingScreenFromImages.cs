using Emgu.CV;
using Emgu.CV.Structure;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Forms.IO;
using ImageAnalysis;
using LibPlateAnalysis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HCSAnalyzer.Classes.ImageAnalysis.FormsForImages
{
    public partial class FormForGeneratingScreenFromImages : Form
    {
        // cScreening CurrentScreening;

        public FormForGeneratingScreenFromImages()
        {
            //this.CurrentScreening = CurrentScreen;
            InitializeComponent();

            this.textBoxImageRoot.Text = cGlobalInfo.OptionsWindow.textBoxImageAccesImagePath.Text;

            this.textBoxPlateForm.Text = cGlobalInfo.ImageAccessor.ImagingPlatformType.ToString();
            this.numericUpDownChannelNumber.Value = cGlobalInfo.ImageAccessor.NumberOfChannels;
            this.numericUpDownFieldNumber.Value = cGlobalInfo.OptionsWindow.numericUpDownImageAccessNumberOfFields.Value;
        }

        Dictionary<string, Dictionary<string, int>> MainScreenDico = new Dictionary<string, Dictionary<string, int>>();

        private void buttonInspect_Click(object sender, EventArgs e)
        {
            this.treeViewForScreenInspection.Nodes.Clear();
            string[] PlateDirectories = Directory.GetDirectories(this.textBoxImageRoot.Text);

            #region Cellomics
            if (cGlobalInfo.ImageAccessor.ImagingPlatformType == eImagingPlatformType.CELLOMICS)
            {

                for (int i = 0; i < PlateDirectories.Length; i++)
                {
                    string TmpPlateName = PlateDirectories[i].Remove(0, this.textBoxImageRoot.Text.Length + 1);

                    OleDbConnection MyConnection = new OleDbConnection();
                    try
                    {
                        String connectionString = @"Provider=Microsoft.JET.OlEDB.4.0;"
                       + "Data Source=" + PlateDirectories[i] + "\\" + TmpPlateName + ".MDB";
                        MyConnection = new OleDbConnection(connectionString);
                        MyConnection.Open();


                        // get the real plate name
                        OleDbDataAdapter da = new OleDbDataAdapter("Select * FROM asnPlate", MyConnection);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        DataTable dt = ds.Tables[0];
                        //   string PlateName = dt.Rows[0]["Name"].ToString();

                        string PlateName = TmpPlateName;

                        TreeNode TmpNode = new TreeNode("[Plate " + i.ToString() + "] - " + PlateName);
                        TmpNode.Checked = true;
                        TmpNode.Tag = PlateDirectories[i];

                        // loop over the plates
                        Dictionary<string, int> CurrentPlateDico = new Dictionary<string, int>();
                        string[] FirstListImages = Directory.GetFiles(PlateDirectories[i], TmpPlateName + "_*.C01", SearchOption.AllDirectories);
                        //string[] FirstListImages = Directory.GetFiles(PlateDirectories[i], TmpPlateName + "_*.TIF", SearchOption.AllDirectories);

                        List<string> ListTrueFiles = new List<string>();

                        for (int j = 0; j < FirstListImages.Length; j++)
                        {
                            string TmpImName = FirstListImages[j];

                            string[] ForSplit = TmpImName.Split('\\');
                            string ImageName = ForSplit[ForSplit.Length - 1];

                            ListTrueFiles.Add(ImageName);
                        }

                        // loop ovet the wells
                        for (int j = 0; j < ListTrueFiles.Count; j++)
                        {
                            string TmpName = ListTrueFiles[j];
                            string[] ForSplit = TmpName.Split('_');
                            string WellPos = ForSplit[ForSplit.Length - 1].Remove(ForSplit[ForSplit.Length - 1].Length - 9);
                            int NumAssociatedImages = 1;
                            ListTrueFiles.RemoveAt(j--);

                            // now parse the rest of the files to merge the channels
                            for (int k = j + 1; k < ListTrueFiles.Count; k++)
                            {
                                if (ListTrueFiles[k].Contains("_" + WellPos))
                                {

                                    NumAssociatedImages++;
                                    ListTrueFiles.RemoveAt(k);
                                    k--;
                                }
                            }

                            TreeNode WellNode = new TreeNode(WellPos + " : " + NumAssociatedImages + " images");
                            WellNode.Checked = true;
                            WellNode.Tag = null;
                            TmpNode.Nodes.Add(WellNode);

                            CurrentPlateDico.Add(WellPos, NumAssociatedImages);
                        }

                        this.treeViewForScreenInspection.Nodes.Add(TmpNode);
                        MainScreenDico.Add(PlateName, CurrentPlateDico);

                        MyConnection.Close();
                    }
                    catch (Exception ex)
                    {
                        cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(ex.Message + "\n");
                        continue;
                    }




                }

            }
            #endregion

            #region ImageXPress
            else if (cGlobalInfo.ImageAccessor.ImagingPlatformType == eImagingPlatformType.IMAGEXPRESS)
            {
                for (int i = 0; i < PlateDirectories.Length; i++)
                {
                    string PlateName = PlateDirectories[i].Remove(0, this.textBoxImageRoot.Text.Length + 1);
                    TreeNode TmpNode = new TreeNode("[Plate " + i.ToString() + "] - " + PlateName);
                    TmpNode.Checked = true;
                    TmpNode.Tag = PlateDirectories[i];
                    // now parse the images...
                    string[] FirstListImages = Directory.GetFiles(PlateDirectories[i], "*.tif", SearchOption.AllDirectories);

                    Dictionary<string, int> CurrentPlateDico = new Dictionary<string, int>();

                    List<string> ListTrueFiles = new List<string>();

                    for (int j = 0; j < FirstListImages.Length; j++)
                    {
                        string TmpImName = FirstListImages[j];
                        if (TmpImName.Contains("_thumb")) continue;


                        string[] ForSplit = TmpImName.Split('\\');
                        string ImageName = ForSplit[ForSplit.Length - 1];

                        ListTrueFiles.Add(ImageName);
                    }

                    for (int j = 0; j < ListTrueFiles.Count; j++)
                    {
                        string TmpName = ListTrueFiles[j];
                        string[] ForSplit = TmpName.Split('_');
                        string WellPos = ForSplit[1];
                        int NumAssociatedImages = 1;
                        ListTrueFiles.RemoveAt(j--);

                        // now parse the rest of the files to merge the channels
                        for (int k = j + 1; k < ListTrueFiles.Count; k++)
                        {
                            if (ListTrueFiles[k].Contains("_" + WellPos))
                            {

                                NumAssociatedImages++;
                                ListTrueFiles.RemoveAt(k);
                                k--;
                            }
                        }

                        TreeNode WellNode = new TreeNode(WellPos + " : " + NumAssociatedImages + " images");
                        WellNode.Checked = true;
                        WellNode.Tag = null;
                        TmpNode.Nodes.Add(WellNode);

                        CurrentPlateDico.Add(WellPos, NumAssociatedImages);

                    }

                    this.treeViewForScreenInspection.Nodes.Add(TmpNode);
                    MainScreenDico.Add(PlateName, CurrentPlateDico);
                }
            }
            #endregion

            #region CV7000
            else if (cGlobalInfo.ImageAccessor.ImagingPlatformType == eImagingPlatformType.CV7000)
            {
                for (int i = 0; i < PlateDirectories.Length; i++)
                {
                    string PlateName = PlateDirectories[i].Remove(0, cGlobalInfo.OptionsWindow.textBoxImageAccesImagePath.Text.Length + 1);
                    TreeNode TmpNode = new TreeNode("[Plate " + i.ToString() + "] - " + PlateName);
                    TmpNode.Checked = true;
                    TmpNode.Tag = PlateDirectories[i];
                    // now parse the images...
                    List<string> FirstListImages = Directory.GetFiles(PlateDirectories[i], "*.tif", SearchOption.AllDirectories).ToList();
                    List<string> Tmpname = Directory.GetFiles(PlateDirectories[i], "*.wpi", SearchOption.AllDirectories).ToList();
                    string Tmpname2 = Tmpname[0].Remove(Tmpname[0].Count() - 4, 4);
                    List<string> ListTrueFiles = FirstListImages.FindAll(ele => ele.Contains(Tmpname2));

                    List<string> List_Well = new List<string>();

                    for (int ix = 0; ix < ListTrueFiles.Count; ix++)
                    {
                        string WellPos = ListTrueFiles[ix].Substring(Tmpname2.Count() + 1);
                        WellPos = WellPos.Substring(0, 3);
                        List_Well.Add(WellPos);
                    }
                    List<string> Uniq_Well = List_Well.Distinct().ToList();

                    Dictionary<string, int> CurrentPlateDico = new Dictionary<string, int>();
                    int nbr_img = ListTrueFiles.Count / Uniq_Well.Count();
                    for (int ix = 0; ix < Uniq_Well.Count; ix++)
                    {
                        CurrentPlateDico.Add(Uniq_Well[ix], nbr_img);
                    }

                    MainScreenDico.Add(PlateName, CurrentPlateDico);
                }
                //    for (int i = 0; i < PlateDirectories.Length; i++)
                //    {
                //        string PlateName = PlateDirectories[i].Remove(0, this.textBoxImageRoot.Text.Length + 1);
                //        TreeNode TmpNode = new TreeNode("[Plate " + i.ToString() + "] - " + PlateName);
                //        TmpNode.Checked = true;
                //        TmpNode.Tag = PlateDirectories[i];
                //        // now parse the images...
                //        string[] FirstListImages = Directory.GetFiles(PlateDirectories[i], "*.tif", SearchOption.AllDirectories);

                //        Dictionary<string, int> CurrentPlateDico = new Dictionary<string, int>();

                //        List<string> ListTrueFiles = new List<string>();

                //        for (int j = 0; j < FirstListImages.Length; j++)
                //        {
                //            string TmpImName = FirstListImages[j];
                //            if (TmpImName.Contains("_thumb")) continue;
                //            if (TmpImName.Contains("BP")) continue;
                //            if (TmpImName.Contains("CMOS")) continue;

                //            string[] ForSplit = TmpImName.Split('\\');
                //            string ImageName = ForSplit[ForSplit.Length - 1];

                //            ListTrueFiles.Add(ImageName);
                //        }

                //        for (int j = 0; j < ListTrueFiles.Count; j++)
                //        {
                //            string TmpName = ListTrueFiles[j];
                //            string[] ForSplit = TmpName.Split('_');
                //            string WellPos = ForSplit[1];
                //            int NumAssociatedImages = 1;
                //            ListTrueFiles.RemoveAt(j--);

                //            // now parse the rest of the files to merge the channels
                //            for (int k = j + 1; k < ListTrueFiles.Count; k++)
                //            {
                //                if (ListTrueFiles[k].Contains("_" + WellPos))
                //                {

                //                    NumAssociatedImages++;
                //                    ListTrueFiles.RemoveAt(k);
                //                    k--;
                //                }
                //            }

                //            TreeNode WellNode = new TreeNode(WellPos + " : " + NumAssociatedImages + " images");
                //            WellNode.Checked = true;
                //            WellNode.Tag = null;
                //            TmpNode.Nodes.Add(WellNode);

                //            CurrentPlateDico.Add(WellPos, NumAssociatedImages);

                //        }

                //        this.treeViewForScreenInspection.Nodes.Add(TmpNode);
                //        MainScreenDico.Add(PlateName, CurrentPlateDico);
                //    }
                //}
                #endregion

                this.richTextBoxReport.Clear();
                this.richTextBoxReport.AppendText(this.treeViewForScreenInspection.Nodes.Count + " plates.\n");
                foreach (var item in this.treeViewForScreenInspection.Nodes)
                {
                    this.richTextBoxReport.AppendText("\n" + item.ToString() + "\n");
                    int IdxWell = 0;
                    foreach (var Subitem in ((TreeNode)item).Nodes)
                    {
                        this.richTextBoxReport.AppendText("\t[Well] " + IdxWell++ + " : " + ((TreeNode)Subitem).Text + "\n");

                    }
                }

            }
        }
        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This process will generate a new screening and delete the current one. Are you sure?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.OK)
                return;

            // Now Let's write the CSV file
            //string FilePath = "c:\\test.csv";
            //System.IO.StreamWriter file = new System.IO.StreamWriter(FilePath);

            //file.WriteLine("PlateName,WellPosition,ImageNumbers");

            //foreach (var itemPlate in MainScreenDico)
            //{


            //    foreach (var itemWell in itemPlate.Value)
            //    {
            //        string Line = itemPlate.Key.ToString()+","+itemWell.Key.ToString()+","+itemWell.Value;
            //        file.WriteLine(Line);
            //    }

            //}
            //file.Close();

            //  return;

            //  if (this.CurrentScreening == null)

            string[] ForNames = this.textBoxImageRoot.Text.Split('\\');
            cGlobalInfo.CurrentScreening = new cScreening(ForNames[ForNames.Length - 1]);
            cGlobalInfo.CurrentScreening.Columns = 24;
            cGlobalInfo.CurrentScreening.Rows = 16;
            cGlobalInfo.CurrentScreening.ListDescriptors.Clean();
            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(new cDescriptorType("ImageNumbers", true, 1));

            foreach (var itemPlate in MainScreenDico)
            {
                cPlate CurrentPlate = cGlobalInfo.CurrentScreening.GetPlateIfNameIsContainIn(itemPlate.Key);
                if (CurrentPlate == null)
                {
                    CurrentPlate = new cPlate(itemPlate.Key, cGlobalInfo.CurrentScreening);
                    cGlobalInfo.CurrentScreening.AddPlate(CurrentPlate);
                }
                else
                {
                    this.richTextBoxReport.AppendText("----------- Error ----------\nPlate " + itemPlate.Key + " has already been processed. Plate skipped !\n");
                    continue;

                }


                foreach (var itemWell in itemPlate.Value)
                {
                    cListSignature LDesc = new cListSignature();
                    cSignature CurrentDescriptor = new cSignature((double)(int)itemWell.Value, cGlobalInfo.CurrentScreening.ListDescriptors[0/* + ShiftIdx*/], cGlobalInfo.CurrentScreening);
                    LDesc.Add(CurrentDescriptor);

                    int[] Pos = new int[2];
                    Pos = cGlobalInfo.WindowHCSAnalyzer.ConvertPosition(itemWell.Key.ToString());

                    cWell CurrentWell = new cWell(LDesc, Pos[0], Pos[1], cGlobalInfo.CurrentScreening, CurrentPlate);
                    CurrentPlate.AddWell(CurrentWell);
                }
            }

            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            foreach (var item in cGlobalInfo.CurrentScreening.ListPlatesActive)
                item.UpDataMinMax();

            cGlobalInfo.WindowHCSAnalyzer.StartingUpDateUI();

            cGlobalInfo.WindowHCSAnalyzer.toolStripcomboBoxPlateList.Items.Clear();
            for (int IdxPlate = 0; IdxPlate < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; IdxPlate++)
            {
                string Name = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(IdxPlate).GetName();
                cGlobalInfo.WindowHCSAnalyzer.toolStripcomboBoxPlateList.Items.Add(Name);
                cGlobalInfo.WindowHCSAnalyzer.PlateListWindow.listBoxPlateNameToProcess.Items.Add(Name);
                cGlobalInfo.WindowHCSAnalyzer.PlateListWindow.listBoxAvaliableListPlates.Items.Add(Name);
                cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(IdxPlate).UpDataMinMax();
            }
            cGlobalInfo.CurrentScreening.CurrentDisplayPlateIdx = 0;
            cGlobalInfo.CurrentScreening.SetSelectionType(cGlobalInfo.WindowHCSAnalyzer.comboBoxClass.SelectedIndex - 1);

            cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx = 0;
            cGlobalInfo.WindowHCSAnalyzer.UpdateUIAfterLoading();


            // now Image Analysis   

            // first create a descriptor for each readout
            cDescriptorType TmpDescType = new cDescriptorType("Total_Intensity_0", true, 1);
            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(TmpDescType);


            //FormForPlateDimensions PlateDim = new FormForPlateDimensions();
            //PlateDim.checkBoxAddCellNumber.Visible = true;
            //PlateDim.checkBoxIsOmitFirstColumn.Visible = true;
            //PlateDim.labelHisto.Visible = true;
            //PlateDim.numericUpDownHistoSize.Visible = true;

            FormForDoubleProgress WindowProgress = new FormForDoubleProgress();
            WindowProgress.Show();
            WindowProgress.progressBarPlate.Maximum = cGlobalInfo.CurrentScreening.ListPlatesAvailable.Count;
            WindowProgress.progressBarPlate.Value = 0;
            WindowProgress.progressBarPlate.Refresh();

            int IdxPlateProg = 0;
            Parallel.ForEach(cGlobalInfo.CurrentScreening.ListPlatesAvailable, (TmpPlate) =>
            {
                //WindowProgress.progressBarPlate.Value++;
                //WindowProgress.progressBarPlate.Refresh();
                //WindowProgress.labelPlateIdx.Text = (IdxPlateProg + 1) + " / " + cGlobalInfo.CurrentScreening.ListPlatesAvailable.Count;

                //WindowProgress.progressBarWell.Value = 0;
                //WindowProgress.progressBarWell.Maximum = TmpPlate.ListWells.Count;

                #region modif

                //List<cImageMetaInfo> ListImageMetaInfo = new List<cImageMetaInfo>();
                //string Pathds = this.textBoxImageRoot.Text;

                //string XMLFile = Pathds+"\\"+ TmpPlate.GetName()+"\\MeasurementDetail.mrf";
                //int NumberOfChannels = (int)this.numericUpDownChannelNumber.Value;
                //List<string> ListFiles = Directory.GetFiles(Pathds + "\\" + TmpPlate.GetName(), "*C0*.tif", SearchOption.AllDirectories).ToList();
                //ListFiles.Sort(); //OrderBy(q=>q).ToList();
                //if (File.Exists(XMLFile))
                //{
                //    XmlDocument xmlDoc = new XmlDocument();
                //    xmlDoc.Load(XMLFile);
                //    for (int Channel = 0; Channel < NumberOfChannels; Channel++)
                //    {
                //        //string FinalName = ListChannels[Channel];

                //        cImageMetaInfo TmpMetaInfo = new cImageMetaInfo();
                //        //TmpMetaInfo.FileName = FinalName;
                //        TmpMetaInfo.Name = xmlDoc.ChildNodes[1].ChildNodes[1].Attributes["bts:Ch"].Value;//ListChannelNames[Channel];
                //        TmpMetaInfo.ResolutionX = double.Parse(xmlDoc.ChildNodes[1].ChildNodes[1].Attributes["bts:HorizontalPixelDimension"].Value);//1;
                //        TmpMetaInfo.ResolutionY = double.Parse(xmlDoc.ChildNodes[1].ChildNodes[1].Attributes["bts:VerticalPixelDimension"].Value);//1;
                //        TmpMetaInfo.ResolutionZ = 1;
                //        ListImageMetaInfo.Add(TmpMetaInfo);
                //    }
                //}
                //else
                //{
                //    for (int Channel = 0; Channel < NumberOfChannels; Channel++)
                //    {
                //        //string FinalName = ListChannels[Channel];

                //        cImageMetaInfo TmpMetaInfo = new cImageMetaInfo();
                //        //TmpMetaInfo.FileName = FinalName;
                //       // TmpMetaInfo.Name = ListChannelNames[Channel];
                //        TmpMetaInfo.ResolutionX = 1;
                //        TmpMetaInfo.ResolutionY = 1;
                //        TmpMetaInfo.ResolutionZ = 1;
                //        ListImageMetaInfo.Add(TmpMetaInfo);
                //    }


                //}

                #endregion



                int IdxWell = 0;
                Parallel.ForEach(TmpPlate.ListWells, (TmpWell) =>
                {
                    //WindowProgress.labelWellIdx.Text = (IdxWell + 1) + " / " + TmpPlate.ListWells.Count;
                    //WindowProgress.progressBarWell.Value = IdxWell + 1;
                    //WindowProgress.progressBarWell.Refresh();
                    //WindowProgress.Refresh();


                    int NumberOfFieldProcessed = 0;
                    double AverageValue = 0;

                    //Parallel.For(0, (int)this.numericUpDownFieldNumber.Value, IdxField =>
                    for (int IdxField = 0; IdxField < this.numericUpDownFieldNumber.Value; IdxField++)
                    {
                        //var watch = Stopwatch.StartNew();
                        cGetImageFromWells IFW = new cGetImageFromWells();
                        IFW.SetInputData(new cListWells(TmpWell));
                        IFW.ListProperties.FindByName("Field").SetNewValue(IdxField);
                        IFW.Run();

                        cImage TmpImage = IFW.GetOutPut();
                        //watch.Stop();
                        //cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText("IFW = "+watch.ElapsedMilliseconds + "\n");
                        if ((TmpImage == null) || (TmpImage.GetNumChannels() == 0))
                        {
                            //cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText("Error while loading [Plate] " + TmpPlate.GetName() + " [Well] " + TmpWell.GetPos() + " [Field] " + IdxField + "\n");
                            //continue;
                        }
                        else
                        {
                            AverageValue = TmpImage.SingleChannelImage[0].Data.Max() * TmpImage.SingleChannelImage[0].Data.Min();
                            Image<Gray, float> ImageToBeReturned = TmpImage.SingleChannelImage[0].ConvertToCVImage(0);
                            //Image<Gray, float> Results = ImageToBeReturned.SmoothGaussian(4);
                        }

                        IFW.GetOutPut().Dispose();
                        //GC.Collect();
                        NumberOfFieldProcessed++;

                    }

                    if (NumberOfFieldProcessed != 0)
                        AverageValue /= (double)NumberOfFieldProcessed;
                    else
                        AverageValue = 0;


                    cListSignature LDesc = new cListSignature();
                    cSignature NewDesc = new cSignature(AverageValue, TmpDescType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);
                    TmpWell.AddSignatures(LDesc);


                    IdxWell++;
                });

                IdxPlateProg++;

            });

            WindowProgress.Dispose();

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();


        }

        private void treeViewForScreenInspection_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenuStrip contextMenuStripPicker = new ContextMenuStrip();

                ToolStripMenuItem MenuItemExpandAll = new ToolStripMenuItem("Expand All");
                MenuItemExpandAll.Click += new System.EventHandler(this.MenuItemExpandAll);
                contextMenuStripPicker.Items.Add(MenuItemExpandAll);


                ToolStripMenuItem MenuItemCollapseAll = new ToolStripMenuItem("Collapse All");
                MenuItemCollapseAll.Click += new System.EventHandler(this.MenuItemCollapseAll);
                contextMenuStripPicker.Items.Add(MenuItemCollapseAll);


                if (e.Node.Tag != null)
                {
                    ToolStripMenuItem MenuItemOpenLocation = new ToolStripMenuItem("Open Location");
                    MenuItemOpenLocation.Click += new System.EventHandler(this.MenuItemOpenLocation);
                    MenuItemOpenLocation.Tag = e.Node;
                    contextMenuStripPicker.Items.Add(MenuItemOpenLocation);
                }


                contextMenuStripPicker.Show(Control.MousePosition);
            }
        }

        void MenuItemOpenLocation(object sender, EventArgs e)
        {
            string NodePath = ((string)((TreeNode)((ToolStripMenuItem)sender).Tag).Tag);
            if (NodePath == null) return;
            Process.Start(NodePath);
        }

        void MenuItemExpandAll(object sender, EventArgs e)
        {
            this.treeViewForScreenInspection.ExpandAll();
        }

        void MenuItemCollapseAll(object sender, EventArgs e)
        {
            this.treeViewForScreenInspection.CollapseAll();
        }

        private void treeViewForScreenInspection_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                Process.Start((string)e.Node.Tag);
            }
        }

        private void buttonParse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog OpenFolderDialog = new FolderBrowserDialog();
            OpenFolderDialog.SelectedPath = this.textBoxImageRoot.Text;

            if (OpenFolderDialog.ShowDialog() != DialogResult.OK) return;
            this.textBoxImageRoot.Text = OpenFolderDialog.SelectedPath;
        }

        private void treeViewForScreenInspection_AfterCheck(object sender, TreeViewEventArgs e)
        {
            this.treeViewForScreenInspection.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.treeViewForScreenInspection_AfterCheck);
            foreach (TreeNode item in e.Node.Nodes)
            {

                item.Checked = e.Node.Checked;
            }
            this.treeViewForScreenInspection.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewForScreenInspection_AfterCheck);

        }

        private void textBoxImageRoot_TextChanged(object sender, EventArgs e)
        {
            cGlobalInfo.OptionsWindow.textBoxImageAccesImagePath.Text = this.textBoxImageRoot.Text;
        }




    }
}
