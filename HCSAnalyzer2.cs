using System.Windows.Forms;
//using Microsoft.Office.Interop.PowerPoint;

using System;
using System.Drawing;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Forms.FormsForOptions;
using HCSAnalyzer.Forms.FormsForDRCAnalysis;
using System.Collections.Generic;
using HCSAnalyzer.Forms;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using LibPlateAnalysis;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.GUI;
using HCSPlugin;
using HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic;
using HCSAnalyzer.Classes.Base_Classes;
using System.IO;
using HCSAnalyzer.Classes._3D;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes.Viewers._3D.ComplexObjects;
//using Microsoft.Office.Core;
namespace HCSAnalyzer
{
    public partial class HCSAnalyzer : Form
    {

        private void testPPTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Microsoft.Office.Interop.PowerPoint.Application pptApplication = new Microsoft.Office.Interop.PowerPoint.Application();

            //Microsoft.Office.Interop.PowerPoint.Slides slides;
            //Microsoft.Office.Interop.PowerPoint._Slide slide;
            //Microsoft.Office.Interop.PowerPoint.TextRange objText;

            //// Create the Presentation File
            //Presentation pptPresentation = pptApplication.Presentations.Add(MsoTriState.msoTrue);

            //Microsoft.Office.Interop.PowerPoint.CustomLayout customLayout = pptPresentation.SlideMaster.CustomLayouts[Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutText];

            //// Create new Slide
            //slides = pptPresentation.Slides;
            //slide = slides.AddSlide(1, customLayout);

            //// Add title
            //objText = slide.Shapes[1].TextFrame.TextRange;
            //objText.Text = "FPPT.com";
            //objText.Font.Name = "Arial";
            //objText.Font.Size = 32;

            //objText = slide.Shapes[2].TextFrame.TextRange;
            //objText.Text = "Content goes here\nYou can add text\nItem 3";

            //slide.NotesPage.Shapes[2].TextFrame.TextRange.Text = "This demo is created by FPPT using C# - Download free templates from http://FPPT.com";

            //pptPresentation.SaveAs(@"c:\temp\fppt.pptx", Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsDefault, MsoTriState.msoTrue);
            ////pptPresentation.Close();
            ////pptApplication.Quit();


        }

        #region Plugins Management
        private void HCSAnalyzer_Shown(object sender, EventArgs e)
        {
            BuildPluginMenu();
        }

        private void BuildPluginMenu()
        {
            List<PluginDescriptor> paList = null;
            try
            {
                paList = PluginDescriptor.GetList(Application.StartupPath + @"\Plugins");
            }
            catch (DirectoryNotFoundException e)
            {
                Directory.CreateDirectory("Plugins");
                paList = PluginDescriptor.GetList(Application.StartupPath + @"\Plugins");
                //MessageBox.Show("Error: " + e.Message, "Plugin's directory not Found" + "\n No Plugin will be loaded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            ToolStripMenuItem currentMenu = null;

            foreach (PluginDescriptor pluginDescriptor in paList)
            {
                currentMenu = pluginsToolStripMenuItem;

                string[] subMenu = pluginDescriptor.MenuPath.Split('|');
                if (pluginDescriptor.MenuPath.Length != 0)
                {
                    foreach (string sm in subMenu)
                    {
                        string menuName = sm.Trim();

                        //if submenu exist , get in
                        if (currentMenu.DropDownItems.ContainsKey(menuName))
                        {
                            currentMenu = (ToolStripMenuItem)currentMenu.DropDownItems[menuName];
                        }
                        else//if not, create it first.
                        {
                            ToolStripMenuItem tsmMenu = new ToolStripMenuItem(menuName);
                            currentMenu.DropDownItems.Add(tsmMenu);
                            tsmMenu.Name = menuName;
                            currentMenu = tsmMenu;
                        }
                    }
                }

                ToolStripMenuItem tsmiName =
                    new ToolStripMenuItem(pluginDescriptor.Name + @" - " + pluginDescriptor.Author);
                currentMenu.DropDownItems.Add(tsmiName);
                tsmiName.Tag = pluginDescriptor;
                tsmiName.Name = pluginDescriptor.Name;
                currentMenu = tsmiName;
                currentMenu.Click += new EventHandler(toolMenuItem_Click);
            }
        }

        private void toolMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem s = (ToolStripMenuItem)sender;

                PluginDescriptor p = (PluginDescriptor)s.Tag;
                Plugin.CurrentScreen = cGlobalInfo.CurrentScreening;
                p.Instanciate();
            }
            catch (PluginException ex)
            {
                MessageBox.Show(ex.Message, "Plugin information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        private void SwitchVizuMode(object sender, EventArgs e)
        {
            GlobalInfo.SwitchVisuMode();
        }

        #region DRC management

        private void convertDRCToWellToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            System.Windows.Forms.DialogResult ResWin = MessageBox.Show("By applying this process, the current screening will be entirely updated ! Proceed ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (ResWin == System.Windows.Forms.DialogResult.No) return;
            //foreach (cDescriptorsType DescType in CompleteScreening.ListDescriptors)
            //{
            //    CompleteScreening.ListDescriptors.RemoveDescUnSafe(DescType, CompleteScreening);
            //}


            if (cGlobalInfo.CurrentScreening != null) cGlobalInfo.CurrentScreening.Close3DView();

            //   CompleteScreening.ListDescriptors.RemoveDesc(CompleteScreening.ListDescriptors[IntToTransfer], CompleteScreening);
            cScreening MergedScreening = new cScreening("Merged Screen");
            MergedScreening.PanelForPlate = this.panelForPlate;

            MergedScreening.Rows = cGlobalInfo.CurrentScreening.Rows;
            MergedScreening.Columns = cGlobalInfo.CurrentScreening.Columns;
            MergedScreening.ListPlatesAvailable = new cListPlates(null);

            // create the descriptor
            MergedScreening.ListDescriptors.Clean();

            int Idesc = 0;

            List<cDescriptorType> ListDescType = new List<cDescriptorType>();

            for (int i = 0; i < cGlobalInfo.CurrentScreening.ListDescriptors.Count; i++)
            {
                if (!cGlobalInfo.CurrentScreening.ListDescriptors[i].IsActive()) continue;

                cDescriptorType DescEC50 = new cDescriptorType("EC50_" + cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName(), true, 1);
                ListDescType.Add(DescEC50);
                MergedScreening.ListDescriptors.AddNew(DescEC50);

                cDescriptorType DescTop = new cDescriptorType("Top_" + cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName(), true, 1);
                ListDescType.Add(DescTop);
                MergedScreening.ListDescriptors.AddNew(DescTop);

                cDescriptorType DescBottom = new cDescriptorType("Bottom_" + cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName(), true, 1);
                ListDescType.Add(DescBottom);
                MergedScreening.ListDescriptors.AddNew(DescBottom);

                cDescriptorType DescSlope = new cDescriptorType("Slope_" + cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName(), true, 1);
                ListDescType.Add(DescSlope);
                MergedScreening.ListDescriptors.AddNew(DescSlope);

                Idesc++;
            }

            MergedScreening.ListDescriptors.CurrentSelectedDescriptorIdx = 0;
            foreach (cPlate CurrentPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {

                cPlate NewPlate = new cPlate(CurrentPlate.GetName() + " Merged", MergedScreening);
                // check if the plate exist already
                MergedScreening.AddPlate(NewPlate);

                foreach (cDRC_Region CurrentRegion in CurrentPlate.ListDRCRegions)
                {

                    cListSignature LDesc = new cListSignature();

                    Idesc = 0;
                    int IDESCBase = 0;

                    for (int i = 0; i < cGlobalInfo.CurrentScreening.ListDescriptors.Count; i++)
                    {
                        if (!cGlobalInfo.CurrentScreening.ListDescriptors[i].IsActive()) continue;

                        cDRC CurrentDRC = CurrentRegion.GetDRC(cGlobalInfo.CurrentScreening.ListDescriptors[IDESCBase++]);

                        cSignature Desc_EC50 = new cSignature(CurrentDRC.EC50, ListDescType[Idesc++], cGlobalInfo.CurrentScreening);
                        LDesc.Add(Desc_EC50);

                        cSignature Desc_Top = new cSignature(CurrentDRC.Top, ListDescType[Idesc++], cGlobalInfo.CurrentScreening);
                        LDesc.Add(Desc_Top);

                        cSignature Desc_Bottom = new cSignature(CurrentDRC.Bottom, ListDescType[Idesc++], cGlobalInfo.CurrentScreening);
                        LDesc.Add(Desc_Bottom);

                        cSignature Desc_Slope = new cSignature(CurrentDRC.Slope, ListDescType[Idesc++], cGlobalInfo.CurrentScreening);
                        LDesc.Add(Desc_Slope);
                    }
                    cWell NewWell = new cWell(LDesc, CurrentRegion.PosXMin + 1, CurrentRegion.PosYMin + 1, MergedScreening, NewPlate);
                    NewWell.ListProperties.UpdateValueByName("Compound Name","DRC [" + CurrentRegion.PosXMin + ":" + CurrentRegion.PosYMin + "]");
                 
                    NewPlate.AddWell(NewWell);
                }
            }

            // PanelList[0].CurrentScreening.ListPlatesActive.Clear();
            // PanelList[0].CurrentScreening.GlobalInfo.WindowHCSAnalyzer.RefreshInfoScreeningRichBox();
            MergedScreening.ListPlatesActive = new cListPlates();

            for (int i = 0; i < MergedScreening.ListPlatesAvailable.Count; i++)
            {
                MergedScreening.ListPlatesActive.Add(MergedScreening.ListPlatesAvailable[i]);
                // MergedScreening.GlobalInfo.WindowHCSAnalyzer.toolStripcomboBoxPlateList.Items.Add(PanelList[0].CurrentScreening.ListPlatesActive[i].Name);
            }
            //PanelList[0].CurrentScreening.CurrentDisplayPlateIdx = 0;
            //PanelList[0].CurrentScreening.GlobalInfo.WindowHCSAnalyzer.toolStripcomboBoxPlateList.SelectedIndex = 0;

            //PanelList[0].CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(PanelList[0].CurrentScreening.ListDescriptors.CurrentSelectedDescriptor, false);




            cGlobalInfo.CurrentScreening.ListDescriptors = MergedScreening.ListDescriptors;
            cGlobalInfo.CurrentScreening.ListPlatesAvailable = MergedScreening.ListPlatesAvailable;
            cGlobalInfo.CurrentScreening.ListPlatesActive = MergedScreening.ListPlatesActive;

            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();
            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), true);
        }

        //private void displayDRCToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    if (cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListDRCRegions == null) return;

        //    int h = 0;
        //    FormToDisplayDRC WindowforDRCsDisplay = new FormToDisplayDRC();

        //    foreach (cDRC_Region TmpRegion in cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListDRCRegions)
        //    {
        //        int cpt = 0;
        //        List<cDRC> ListDRC = new List<cDRC>();
        //        for (int i = 0; i < cGlobalInfo.CurrentScreening.ListDescriptors.Count; i++)
        //        {
        //            if (cGlobalInfo.CurrentScreening.ListDescriptors[i].IsActive())
        //            {
        //                cDRC CurrentDRC = new cDRC(TmpRegion, cGlobalInfo.CurrentScreening.ListDescriptors[i]);

        //                ListDRC.Add(CurrentDRC);
        //                cpt++;
        //            }

        //        }

        //        cDRCDisplay DRCDisplay = new cDRCDisplay(ListDRC, GlobalInfo);

        //        if (DRCDisplay.CurrentChart.Series.Count == 0) continue;

        //        DRCDisplay.CurrentChart.Location = new Point((DRCDisplay.CurrentChart.Width + 50) * 0, (DRCDisplay.CurrentChart.Height + 10 + DRCDisplay.CurrentRichTextBox.Height) * h++);
        //        DRCDisplay.CurrentRichTextBox.Location = new Point(DRCDisplay.CurrentChart.Location.X, DRCDisplay.CurrentChart.Location.Y + DRCDisplay.CurrentChart.Height + 5);

        //        WindowforDRCsDisplay.LChart.Add(DRCDisplay.CurrentChart);
        //        WindowforDRCsDisplay.LRichTextBox.Add(DRCDisplay.CurrentRichTextBox);
        //    }

        //    WindowforDRCsDisplay.panelForDRC.Controls.AddRange(WindowforDRCsDisplay.LChart.ToArray());
        //    WindowforDRCsDisplay.panelForDRC.Controls.AddRange(WindowforDRCsDisplay.LRichTextBox.ToArray());
        //    WindowforDRCsDisplay.Show();
        //}


        //private void displayRespondingDRCToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    if (cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListDRCRegions == null) return;

        //    FormForDRCSelection WindowSelectionDRC = new FormForDRCSelection();
        //    if (WindowSelectionDRC.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

        //    if (WindowSelectionDRC.checkBoxMOAClassification.Checked == false)
        //    {
        //        int h = 0;
        //        FormToDisplayDRC WindowforDRCsDisplay = new FormToDisplayDRC();

        //        foreach (cDRC_Region TmpRegion in cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListDRCRegions)
        //        {
        //            int cpt = 0;
        //            List<cDRC> ListDRC = new List<cDRC>();
        //            for (int i = 0; i < cGlobalInfo.CurrentScreening.ListDescriptors.Count; i++)
        //            {
        //                if (cGlobalInfo.CurrentScreening.ListDescriptors[i].IsActive())
        //                {
        //                    cDRC CurrentDRC = new cDRC(TmpRegion, cGlobalInfo.CurrentScreening.ListDescriptors[i]);
        //                    if (CurrentDRC.IsResponding(WindowSelectionDRC) == 1)
        //                    {
        //                        ListDRC.Add(CurrentDRC);
        //                        cpt++;
        //                    }
        //                }
        //            }

        //            cDRCDisplay DRCDisplay = new cDRCDisplay(ListDRC, GlobalInfo);
        //            if (DRCDisplay.CurrentChart.Series.Count == 0) continue;

        //            DRCDisplay.CurrentChart.Location = new Point((DRCDisplay.CurrentChart.Width + 50) * 0, (DRCDisplay.CurrentChart.Height + 10 + DRCDisplay.CurrentRichTextBox.Height) * h++);
        //            DRCDisplay.CurrentRichTextBox.Location = new Point(DRCDisplay.CurrentChart.Location.X, DRCDisplay.CurrentChart.Location.Y + DRCDisplay.CurrentChart.Height + 5);

        //            WindowforDRCsDisplay.LChart.Add(DRCDisplay.CurrentChart);
        //            WindowforDRCsDisplay.LRichTextBox.Add(DRCDisplay.CurrentRichTextBox);
        //        }

        //        WindowforDRCsDisplay.panelForDRC.Controls.AddRange(WindowforDRCsDisplay.LChart.ToArray());
        //        WindowforDRCsDisplay.panelForDRC.Controls.AddRange(WindowforDRCsDisplay.LRichTextBox.ToArray());
        //        WindowforDRCsDisplay.Show();
        //        return;
        //    }


        //    System.Windows.Forms.DialogResult ResWin = MessageBox.Show("By applying this process, the current screening will be entirely updated ! Proceed ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //    if (ResWin == System.Windows.Forms.DialogResult.No) return;



        //    foreach (cPlate CurrentPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
        //    {
        //        foreach (cDRC_Region TmpRegion in CurrentPlate.ListDRCRegions)
        //        {
        //            //int cpt = 0;
        //            //List<cDRC> ListDRC = new List<cDRC>();
        //            //for (int i = 0; i < CompleteScreening.ListDescriptors.Count; i++)
        //            //{
        //            //  if (CompleteScreening.ListDescriptors[i].IsActive())
        //            //    {
        //            //        cDRC CurrentDRC = new cDRC(TmpRegion, CompleteScreening.ListDescriptors[i]);
        //            //        if (CurrentDRC.IsResponding(WindowSelectionDRC))
        //            //        {
        //            //            ListDRC.Add(CurrentDRC);
        //            //            cpt++;
        //            //        }
        //            //    }


        //            //}
        //            List<int> ResDescActive = TmpRegion.GetListRespondingDescritpors(CurrentScreening, WindowSelectionDRC);

        //            for (int j = 0; j < TmpRegion.NumReplicate; j++)
        //                for (int i = 0; i < TmpRegion.NumConcentrations; i++)
        //                {

        //                    cWell CurrentWell = TmpRegion.GetListWells()[j][i];
        //                    if (CurrentWell == null) continue;

        //                    for (int IdxDesc = 0; IdxDesc < ResDescActive.Count; IdxDesc++)
        //                    {
        //                        if (ResDescActive[IdxDesc] == -1) continue;

        //                        //CurrentWell.ListDescriptors[IdxDesc].HistoValues = new double[1];
        //                        CurrentWell.ListSignatures[IdxDesc].SetHistoValues((double)ResDescActive[IdxDesc]);
        //                        if ((i == 0) && (j == 0))
        //                            CurrentWell.SetClass(0);
        //                        else
        //                            CurrentWell.SetAsNoneSelected();
        //                        //[0] = ResDescActive[IdxDesc];   
        //                        CurrentWell.ListSignatures[IdxDesc].UpDateDescriptorStatistics();

        //                    }

        //                }
        //        }
        //        CurrentPlate.UpDataMinMax();
        //    }
        //}

        #endregion

        #region Distributions
        private void distributionsModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalInfo.SwitchDistributionMode();
        }

        private void displayReferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (cGlobalInfo.CurrentScreening.Reference == null)
            {
                MessageBox.Show("No reference curve generated. Switch to Distribution mode.\n", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FormForDisplay TMPWin = new FormForDisplay();
            cExtendedList ListValues = cGlobalInfo.CurrentScreening.Reference.GetValues(cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
            ListValues.Name = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();

            cPanelHisto PanelHisto = new cPanelHisto(ListValues, eGraphType.HISTOGRAM, eOrientation.HORIZONTAL);

            //  cDisplayHisto CpdToDisplayHisto = new cDisplayHisto();
            TMPWin.Controls.Add(PanelHisto.WindowForPanelHisto.panelForGraphContainer);

            //TMPWin.panel.Controls.Add(CpdToDisplayHisto);
            TMPWin.ShowDialog();


            //  cWindowToDisplayHisto NewWindow = new cWindowToDisplayHisto(CompleteScreening,CompleteScreening.Reference.GetValues(CompleteScreening.ListDescriptors.CurrentSelectedDescriptor));
            //   NewWindow.Show();
            //    NewWindow.chartForSimpleForm.ChartAreas.Add(CurrentChartArea);

            // cWindowToDisplayScatter NewWindow = new cWindowToDisplayScatter();
            //   NewWindow.chartForSimpleForm.Controls.Add(CompleteScreening.Reference.GetChart(CompleteScreening.ListDescriptors.CurrentSelectedDescriptor));
            //  NewWindow.Show();
            //cDisplayGraph DispGraph = new cDisplayGraph(CompleteScreening.Reference[CompleteScreening.ListDescriptors.CurrentSelectedDescriptor].ToArray(),
            //    CompleteScreening.ListDescriptors[CompleteScreening.ListDescriptors.CurrentSelectedDescriptor].GetName() + " - Reference distribution.");
        }
        #endregion
    }
}