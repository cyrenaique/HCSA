using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibPlateAnalysis;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Forms.FormsForOptions;

namespace HCSAnalyzer
{

    public partial class FormForOptionsWindow : Form
    {
        cScreening CurrentScreen = null;
        public radioButtonUISelectionModeTotalIntensity FFAllOptions = new radioButtonUISelectionModeTotalIntensity();

        public void SetCurrentScreening(cScreening CurrentScreen)
        {
            this.CurrentScreen = CurrentScreen;
        }

        public FormForOptionsWindow()
        {
            InitializeComponent();
            buttonOk.Focus();
            buttonOk.Select();
            this.comboBoxHierarchicalDistance.SelectedIndex = 0;
            this.comboBoxHierarchicalLinkType.SelectedIndex = 0;
            this.treeViewOptions.ExpandAll();
            //FFAA.Show();

        }

        public void LoadDefaultParams()
        {
            if (File.Exists("Default.opt"))
            {
                cGlobalInfoToBeExported GloblaInfoToBeLoaded = new cGlobalInfoToBeExported();
                GloblaInfoToBeLoaded.Load("Default.opt");
            }

        }


        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            if ((CurrentScreen != null) && (CurrentScreen.ListPlatesActive != null))
            {
                CurrentScreen.GetCurrentDisplayPlate().DisplayDistribution(CurrentScreen.ListDescriptors.GetActiveDescriptor(), false);
            }
            cGlobalInfo.ImageAccessor.NumberOfChannels = (int)this.numericUpDownImageAccessNumberOfChannels.Value;
            cGlobalInfo.ImageAccessor.InitialPath = this.textBoxImageAccesImagePath.Text;
            if (this.radioButtonImageAccessManual.Checked)
                cGlobalInfo.ImageAccessor.ImagingPlatformType = Classes.General_Types.eImagingPlatformType.MANUAL;
            else if (this.radioButtonImageAccessHarmony.Checked)
                cGlobalInfo.ImageAccessor.ImagingPlatformType = Classes.General_Types.eImagingPlatformType.HARMONY;
            else if (this.radioButtonImageAccessImageXpress.Checked)
                cGlobalInfo.ImageAccessor.ImagingPlatformType = Classes.General_Types.eImagingPlatformType.IMAGEXPRESS;
            else if (this.radioButtonImageAccessHarmony35.Checked)
                cGlobalInfo.ImageAccessor.ImagingPlatformType = Classes.General_Types.eImagingPlatformType.HARMONY35;
            else if (this.radioButtonImageAccessCellomics.Checked)
                cGlobalInfo.ImageAccessor.ImagingPlatformType = Classes.General_Types.eImagingPlatformType.CELLOMICS;
            else if (this.radioButtonImageAccessINCell.Checked)
                cGlobalInfo.ImageAccessor.ImagingPlatformType = Classes.General_Types.eImagingPlatformType.INCELL;  
            else if (this.radioButtonImageAccessCV7000.Checked)
                cGlobalInfo.ImageAccessor.ImagingPlatformType = Classes.General_Types.eImagingPlatformType.CV7000;  
            else if (this.radioButtonImageAccessBuiltIn.Checked)
                cGlobalInfo.ImageAccessor.ImagingPlatformType = Classes.General_Types.eImagingPlatformType.BUILTIN;
        }

        private void buttonDRCPlateDesign_Click(object sender, EventArgs e)
        {
            if (CurrentScreen == null) return;
            cGlobalInfo.WindowForDRCDesign.ShowDialog();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if ((CurrentScreen != null) && (CurrentScreen.ListPlatesActive != null))
                CurrentScreen.GetCurrentDisplayPlate().DisplayDistribution(CurrentScreen.ListDescriptors.GetActiveDescriptor(), false);
        }

        private void numericUpDownManualMin_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownManualMin.Value >= numericUpDownManualMax.Value) numericUpDownManualMin.Value = numericUpDownManualMax.Value;
        }

        private void numericUpDownManualMax_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownManualMax.Value <= numericUpDownManualMin.Value) numericUpDownManualMax.Value = numericUpDownManualMin.Value;
        }

        private void radioButtonImageAccessManual_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxDefined.Enabled = false;
            groupBoxManual.Enabled = true;
        }

        private void buttonUpdateImagePath_Click(object sender, EventArgs e)
        {
            var dlg1 = new Ionic.Utils.FolderBrowserDialogEx();
            dlg1.Description = "Select the folder containing your databases.";
            dlg1.ShowNewFolderButton = true;
            dlg1.ShowEditBox = true;
            //dlg1.NewStyle = false;
            //  dlg1.SelectedPath = txtExtractDirectory.Text;
            dlg1.ShowFullPathInEditBox = true;
            dlg1.RootFolder = System.Environment.SpecialFolder.Desktop;

            dlg1.SelectedPath = this.textBoxImageAccesImagePath.Text;

            // Show the FolderBrowserDialog.
            DialogResult result = dlg1.ShowDialog();

            if (result != DialogResult.OK) return;

            //FolderBrowserDialog OpenFolderDialog = new FolderBrowserDialog();

            //if (OpenFolderDialog.ShowDialog() != DialogResult.OK) return;
            this.textBoxImageAccesImagePath.Text = dlg1.SelectedPath;
        }



        private void radioButtonImageAccessDefined_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxDefined.Enabled = true;
            groupBoxManual.Enabled = false;
        }

        private void radioButtonObjPosBoundingBox_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonObjPosCenter.Checked)
            {
                panelCenter.Enabled = true;
                panelBoundingBox.Enabled = false;
            }
            else
            {
                panelCenter.Enabled = false;
                panelBoundingBox.Enabled = true;
            }
        }

        private void radioButtonObjPosCenter_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonObjPosCenter.Checked)
            {
                panelCenter.Enabled = true;
                panelBoundingBox.Enabled = false;
            }
            else
            {
                panelCenter.Enabled = false;
                panelBoundingBox.Enabled = true;
            }
        }

        void UpdateLabelPath()
        {
            if (radioButtonImageAccessHarmony35.Checked) labelPath.Text = "Data Path";
            else labelPath.Text = "Images Path";
        }

        void UpdateNumChanneslGUI()
        {
            if (radioButtonImageAccessHarmony35.Checked || radioButtonImageAccessINCell.Checked || radioButtonImageAccessCV7000.Checked || radioButtonImageAccessBuiltIn.Checked) numericUpDownImageAccessNumberOfChannels.Enabled = false;
            else numericUpDownImageAccessNumberOfChannels.Enabled = true;
        }

        private void radioButtonImageAccessOperetta35_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLabelPath();
            UpdateNumChanneslGUI();
        }

        private void radioButtonImageAccessImageXpress_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLabelPath();
            UpdateNumChanneslGUI();
        }


        private void radioButtonImageAccessCellomics_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLabelPath();
            UpdateNumChanneslGUI();
        }

        private void radioButtonImageAccessOperetta_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLabelPath();
            UpdateNumChanneslGUI();
        }

        private void saveDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cGlobalInfoToBeExported GlobalInfoToBeExported = new cGlobalInfoToBeExported();
            GlobalInfoToBeExported.Save("default.opt");
        }

        private void numericUpDownImageAccessNumberOfFields_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownDefaultField.Value >= numericUpDownImageAccessNumberOfFields.Value) numericUpDownDefaultField.Value = numericUpDownImageAccessNumberOfFields.Value - 1;
        }

        private void numericUpDownDefaultField_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownDefaultField.Value >= numericUpDownImageAccessNumberOfFields.Value) numericUpDownDefaultField.Value = numericUpDownImageAccessNumberOfFields.Value - 1;
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panelForCurrentLUTList.Controls.Clear();
            cGlobalInfo.TmpImageDisplayProperties = null;
        }

        #region IO
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentScreen == null) return;
            cGlobalInfoToBeExported GlobalInfoToBeExported = new cGlobalInfoToBeExported();
            GlobalInfoToBeExported.Load("");
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cGlobalInfoToBeExported GlobalInfoToBeExported = new cGlobalInfoToBeExported();
            TmpOptionPath = GlobalInfoToBeExported.Save("");
        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            cGlobalInfoToBeExported GlobalInfoToBeExported = new cGlobalInfoToBeExported();
            if (TmpOptionPath == "")
            {
                TmpOptionPath = GlobalInfoToBeExported.Save("");
            }
            else
            {
                GlobalInfoToBeExported.Save(TmpOptionPath);
            }
        }

        public string TmpOptionPath = "";

        #endregion

        private void tabControlWindowOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlWindowOption.SelectedTab == tabPageMiscInfo)
            {
                richTextBoxSystemInfo.Clear();
                richTextBoxSystemInfo.AppendText(cGlobalInfo.SystemInfo.GetInfo());
            }
        }

        private void treeViewOptions_AfterSelect(object sender, TreeViewEventArgs e)
        {
           Panel CurrentPanel = null;
            panelForSpecificOption.Controls.Clear();
           
            switch (e.Node.Text)
            {
                case "Graphs":
                    CurrentPanel = this.FFAllOptions.panelGraphs;
                    break;
                case "Scatter Plots":
                    CurrentPanel = this.FFAllOptions.panelScatterPlot;
                    break;
                case "1D":
                    CurrentPanel = this.FFAllOptions.panelScatter1D;
                    break;
                case "2D":
                    CurrentPanel = this.FFAllOptions.panelScatter2D;
                    break;
                case "3D":
                    CurrentPanel = this.FFAllOptions.panelScatter3D;
                    break;
                case "Plate Design":
                    CurrentPanel = this.FFAllOptions.panelPlateDesign;
                    break;
                case "DRC":
                    CurrentPanel = this.FFAllOptions.panelDRC;
                    break;
                case "Histograms":
                    CurrentPanel = this.FFAllOptions.panelHisto;
                    break;
                case "Z' graphs":
                    CurrentPanel = this.FFAllOptions.panelZScoreGraph;
                    break;    
                case "World Defaults":
                    CurrentPanel = this.FFAllOptions.panel3DWorldDefaults;
                    break;
                case "Image Viewer":
                    CurrentPanel = this.FFAllOptions.panelImageViewer;
                    break;    
                case "HTML export":
                    CurrentPanel = this.FFAllOptions.panelHTMLExport;
                    break;     
                case "Well":
                    CurrentPanel = CreatePanelForWellProperties();
                    break;  
                case "Screening":
                    if (cGlobalInfo.CurrentScreening != null)
                    {
                        this.FFAllOptions.textBoxScreeningName.Enabled = true;
                        this.FFAllOptions.textBoxScreeningName.Text = cGlobalInfo.CurrentScreening.GetName();
                    }
                    CurrentPanel = this.FFAllOptions.panelScreening;

                    break;     
                default:
                   // panelForSpecificOption = null;
                   
                    break;

            }

            if(CurrentPanel!=null)
            {
                CurrentPanel.Location = new Point(0, 0);
                panelForSpecificOption.Controls.Add(CurrentPanel);
            }
            panelForSpecificOption.Update();
            panelForSpecificOption.Show();



        }

        public Panel CreatePanelForWellProperties()
        {
            if (cGlobalInfo.CurrentScreening.ListWellPropertyTypes == null) return this.FFAllOptions.panelWellProperties;

            this.FFAllOptions.panelForWellPropertiesToBeDisplayed.Controls.Clear();
            int PosY = 0;
            foreach (var item in cGlobalInfo.CurrentScreening.ListWellPropertyTypes)
            {
                CheckBox CB = new CheckBox();
                CB.Text = item.Name;
                CB.Location = new Point(5, PosY);
                PosY += 20;
                CB.Width = this.FFAllOptions.panelForWellPropertiesToBeDisplayed.Width;
                CB.Tag = item;
                CB.Checked = item.IsTobeDisplayed;
                CB.CheckedChanged += new EventHandler(CB_CheckedChanged);
                this.FFAllOptions.panelForWellPropertiesToBeDisplayed.Controls.Add(CB);
            }

            return this.FFAllOptions.panelWellProperties;

        }

        void CB_CheckedChanged(object sender, EventArgs e)
        {
            ((cPropertyType)(((CheckBox)(sender)).Tag)).IsTobeDisplayed = ((CheckBox)(sender)).Checked;

        }


        private void radioButtonImageAccessINCell_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLabelPath();
            UpdateNumChanneslGUI();
        }

        private void radioButtonImageAccessCV7000_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLabelPath();
            UpdateNumChanneslGUI();
        }

        private void radioButtonImageAccessBuiltIn_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLabelPath();
            UpdateNumChanneslGUI();
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(panelForWellClasses.Width, panelForWellClasses.Height);

            //Drawing control to the bitmap
            panelForWellClasses.DrawToBitmap(bmp, new Rectangle(0, 0, panelForWellClasses.Width, panelForWellClasses.Height));
            MemoryStream ms = new MemoryStream();
            System.Windows.Forms.Clipboard.SetImage(bmp);
        }

        private void toolStripMenuItemPhenoToClip_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(panelForCellularPhenotypes.Width, panelForCellularPhenotypes.Height);

            //Drawing control to the bitmap
            panelForCellularPhenotypes.DrawToBitmap(bmp, new Rectangle(0, 0, panelForCellularPhenotypes.Width, panelForCellularPhenotypes.Height));
            MemoryStream ms = new MemoryStream();
            System.Windows.Forms.Clipboard.SetImage(bmp);
        }






    }
}
