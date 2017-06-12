using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows;
using System.Drawing;
using HCSAnalyzer.Forms.IO;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using HCSAnalyzer.Controls;
using HCSAnalyzer.Classes.Base_Classes.GUI;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.General_Types;

namespace LibPlateAnalysis
{

    public enum eDistances { EUCLIDEAN, MANHATTAN, VECTOR_COS, BHATTACHARYYA, EMD };

    public class cDescriptorsLinearCombination : List<cDescriptorType>
    {
        cExtendedList ListWeights;// = new cExtendedList();
     //   cGlobalInfo GlobalInfo;
        cViewerTable Sender = null;
        string Name;

        public cDescriptorsLinearCombination(cExtendedList ListWeights)
        {
            this.ListWeights = new cExtendedList();
            this.ListWeights.Name = ListWeights.Name;
            this.ListWeights = ListWeights;
            //this.GlobalInfo = GlobalInfo;
            this.Name = ListWeights.Name;
        }

        public cDescriptorsLinearCombination(cViewerTable Sender)
        {
            this.ListWeights = new cExtendedList();
            //this.ListWeights.Name = ListWeights.Name;
            //this.ListWeights = ListWeights;
           // this.GlobalInfo = GlobalInfo;
            this.Name = ListWeights.Name;
            this.Sender = Sender;
        }

        public List<ToolStripMenuItem> GetContextMenu()
        {
            List<ToolStripMenuItem> ListToReturn = new List<ToolStripMenuItem>();

            // perform projection
            ToolStripMenuItem PerformProjection = new ToolStripMenuItem("Perform Projection");
            PerformProjection.Click += new System.EventHandler(this.PerformProjection);
            ListToReturn.Add(PerformProjection);
            return ListToReturn;
        }

        void PerformProjection(object sender, EventArgs e)
        {
            string NewName = this.Name;
            string Description = "";

            if (this.Sender != null)
            {
                cExtendedTable CT =  this.Sender.GetLiveListValues();
                if((CT==null)||(CT.Count==0))
                    return;

                this.ListWeights = CT[0];
                    if (this.ListWeights == null) return;

                    this.Name = this.Sender.Title;

            }

            for (int IdxActiveDesc = 0; IdxActiveDesc < this.Count; IdxActiveDesc++)
                Description += this.ListWeights[IdxActiveDesc].ToString("N2") + "\t*\t" + this[IdxActiveDesc].GetName() + "\n";

            cDescriptorType NewType = new cDescriptorType(NewName, true, 1, Description);
            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(NewType);

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListActiveWells)
                {
                    cListSignature LDesc = new cListSignature();
                    double NewValue = 0;

                    for (int IdxActiveDesc = 0; IdxActiveDesc < this.Count; IdxActiveDesc++)
                        NewValue += this.ListWeights[IdxActiveDesc] * Tmpwell.ListSignatures[IdxActiveDesc].GetValue();

                    cSignature NewDesc = new cSignature(NewValue, NewType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);
                    Tmpwell.AddSignatures(LDesc);
                }
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }
    }

    public class cDescriptorType : cGeneralComponent
    {
        public bool IsConnectedToDatabase { get; private set; }
        private string Name;
        public string description;
        private int NumBin;
        //cGlobalInfo GlobalInfo;
        public FormForDescriptorInfo WindowDescriptorInfo;
        public double Weight  { get; private set; }
        private bool ActiveState;

        void CommonInit()
        {
            this.Weight = 1;
        }

        public cDescriptorType(string Name, bool IsActive, int BinNumber, bool IsConnectedToDB)
        {
            //this.AssociatedcListDescriptors = AssociatedcListDescriptors;
            if (cGlobalInfo.CurrentScreening != null)
            {
                int IdxForNewName = -1;
                bool IsAlreadyExisting = false;
                string OriginalName = Name;

                foreach (var item in cGlobalInfo.CurrentScreening.ListDescriptors)
                {
                    if (item.Name == Name)
                    {
                        IsAlreadyExisting = true;
                        break;
                    }
                }

                while (IsAlreadyExisting)
                {
                    IdxForNewName++;
                    Name = OriginalName + IdxForNewName;

                    IsAlreadyExisting = false;
                    foreach (var item in cGlobalInfo.CurrentScreening.ListDescriptors)
                    {
                        if (item.Name == Name)
                        {
                            IsAlreadyExisting = true;
                            break;
                        }
                    }
                }
            }

            this.Name = Name;
            this.ActiveState = IsActive;
            this.NumBin = BinNumber;
            this.IsConnectedToDatabase = IsConnectedToDB;
           // this.GlobalInfo = GlobalInfo;
            CommonInit();

            CreateAssociatedWindow();
        }

        public cDescriptorType(string Name, bool IsActive, int BinNumber)
        {
            //this.AssociatedcListDescriptors = AssociatedcListDescriptors;
            if (cGlobalInfo.CurrentScreening != null)
            {
                int IdxForNewName = -1;
                bool IsAlreadyExisting = false;
                string OriginalName = Name;

                foreach (var item in cGlobalInfo.CurrentScreening.ListDescriptors)
                {
                    if (item.Name == Name)
                    {
                        IsAlreadyExisting = true;
                        break;
                    }
                }

                while (IsAlreadyExisting)
                {
                    IdxForNewName++;
                    Name = OriginalName + IdxForNewName;

                    IsAlreadyExisting = false;
                    foreach (var item in cGlobalInfo.CurrentScreening.ListDescriptors)
                    {
                        if (item.Name == Name)
                        {
                            IsAlreadyExisting = true;
                            break;
                        }
                    }
                }
            }
            this.Name = Name;
            this.ActiveState = IsActive;
            this.NumBin = BinNumber;
            this.IsConnectedToDatabase = false;
           // this.GlobalInfo = GlobalInfo;
            CommonInit();
            CreateAssociatedWindow();
        }

        public cDescriptorType(string Name, bool IsActive, int BinNumber, string Description)
        {
            //this.AssociatedcListDescriptors = AssociatedcListDescriptors;
            if (cGlobalInfo.CurrentScreening != null)
            {
                int IdxForNewName = -1;
                bool IsAlreadyExisting = false;
                string OriginalName = Name;

                foreach (var item in cGlobalInfo.CurrentScreening.ListDescriptors)
                {
                    if (item.Name == Name)
                    {
                        IsAlreadyExisting = true;
                        break;
                    }
                }

                while (IsAlreadyExisting)
                {
                    IdxForNewName++;
                    Name = OriginalName + IdxForNewName;

                    IsAlreadyExisting = false;
                    foreach (var item in cGlobalInfo.CurrentScreening.ListDescriptors)
                    {
                        if (item.Name == Name)
                        {
                            IsAlreadyExisting = true;
                            break;
                        }
                    }
                }

               
            } 
            this.Name = Name;
            this.ActiveState = IsActive;
            this.NumBin = BinNumber;
            this.IsConnectedToDatabase = false;
            //this.GlobalInfo = GlobalInfo;
            this.description = Description;
            CommonInit();
            CreateAssociatedWindow();
        }
  
        public string GetName()
        {
            return Name;
        }

        public string GetShortInfo()
        {
            base.ShortInfo = "Descriptor: " + this.Name + "\n";
            return base.GetShortInfo();
        }

        public int GetBinNumber()
        {
            return NumBin;
        }

        public string GetDataType()
        {
            if (NumBin == 1) return "Single";
            else
                return "Histogram";
        }

        public void SetActiveState(bool IsActive)
        {
            this.ActiveState = IsActive;
        }

        public bool IsActive()
        {
            return this.ActiveState;
        }

        public bool ChangeName(string NewName)
        {
            this.Name = NewName;
            return true;
        }

        public void ChangeBinNumber(int NewBinNumber)
        {
            int IdxDesc = cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(this);

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell TmpWell in TmpPlate.ListWells)
                {
                    TmpWell.ListSignatures[IdxDesc].RefreshHisto(NewBinNumber);
                }
                //for (int Col = 1; Col <= GlobalInfo.CurrentScreening.Columns; Col++)
                //    for (int Row = 1; Row <= GlobalInfo.CurrentScreening.Rows; Row++)
                //    {
                //        cWell TmpWell = TmpPlate.GetWell(Col, Row, false);
                //        if (TmpWell == null) continue;
                //        TmpWell.ListSignatures[IdxDesc].RefreshHisto(NewBinNumber);
                //    }
            }

            this.NumBin = NewBinNumber;

        }

        private void CreateAssociatedWindow()
        {
            WindowDescriptorInfo = new FormForDescriptorInfo(this);
            WindowDescriptorInfo.CurrentDesc = this;
            WindowDescriptorInfo.Text = this.Name;
        }

        public List<ToolStripMenuItem> GetExtendedContextMenu()
        {
            List<ToolStripMenuItem> ListToReturn = new List<ToolStripMenuItem>();

            base.SpecificContextMenu = new ToolStripMenuItem(this.Name);

            // info
            ToolStripMenuItem InfoDescItem = new ToolStripMenuItem("Info");
            InfoDescItem.Click += new System.EventHandler(this.InfoDescItem);
            base.SpecificContextMenu.DropDownItems.Add(InfoDescItem);

            ToolStripMenuItem StackedHistoDescItem = new ToolStripMenuItem("Stacked Histo.");
            StackedHistoDescItem.Click += new System.EventHandler(this.StackedHistoDescItem);
            base.SpecificContextMenu.DropDownItems.Add(StackedHistoDescItem);

            if (cGlobalInfo.CurrentScreening.ListDescriptors.Count >= 2)
            {
                ToolStripMenuItem RemoveDescItem = new ToolStripMenuItem("Remove");
                RemoveDescItem.Click += new System.EventHandler(this.RemoveDescItem);
                base.SpecificContextMenu.DropDownItems.Add(RemoveDescItem);
            }

            base.SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem DescriptorsView = new ToolStripMenuItem("Descriptor view");
            DescriptorsView.Click += new System.EventHandler(this.DescriptorsView);
            base.SpecificContextMenu.DropDownItems.Add(DescriptorsView);

            ToolStripMenuItem DescriptorsSetAsActive = new ToolStripMenuItem("Set as current");
            DescriptorsSetAsActive.Click += new System.EventHandler(this.DescriptorsSetAsActive);
            base.SpecificContextMenu.DropDownItems.Add(DescriptorsSetAsActive);


            string NewState = "";
            if (this.ActiveState)
                NewState = "inactive";
            else
                NewState = "active";

            ToolStripMenuItem DescriptorsSetAsInActive = new ToolStripMenuItem("Set as "+NewState);
            DescriptorsSetAsInActive.Click += new System.EventHandler(this.DescriptorsSetAsInActive);
            base.SpecificContextMenu.DropDownItems.Add(DescriptorsSetAsInActive);

            ListToReturn.Add(base.SpecificContextMenu);
            return ListToReturn;
        }

        void DescriptorsView(object sender, EventArgs e)
        {
          //  GlobalInfo.CurrentScreen.GetCurrentDisplayPlate().DisplayDescriptorsWindow();

            List<cPanelForDisplayArray> ListPlates = new List<cPanelForDisplayArray>();

            foreach (cPlate CurrentPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                ListPlates.Add(new FormToDisplayPlate(CurrentPlate));

            cWindowToDisplayEntireScreening WindowToDisplayArray = new cWindowToDisplayEntireScreening(ListPlates, this.GetName(), 6);
            WindowToDisplayArray.Show();
        }

        void DescriptorsSetAsActive(object sender, EventArgs e)
        {
            cGlobalInfo.WindowHCSAnalyzer.comboBoxDescriptorToDisplay.Text = this.Name;
        }

        void DescriptorsSetAsInActive(object sender, EventArgs e)
        {
            for (int i = 0; i < cGlobalInfo.WindowHCSAnalyzer.checkedListBoxActiveDescriptors.Items.Count; i++)
			{
                if (cGlobalInfo.WindowHCSAnalyzer.checkedListBoxActiveDescriptors.Items[i].ToString() == this.Name)
                {
                    this.ActiveState = !this.ActiveState;
                    cGlobalInfo.WindowHCSAnalyzer.checkedListBoxActiveDescriptors.SetItemChecked(i, this.ActiveState);
                    return;
                }
			}
        }

        void StackedHistoDescItem(object sender, EventArgs e)
        {

            #region Obsolete
            //FormForMultipleClassSelection WindowForClassSelection = new FormForMultipleClassSelection();
            //PanelForClassSelection ClassSelectionPanel = new PanelForClassSelection(GlobalInfo, true);
            //ClassSelectionPanel.Height = WindowForClassSelection.splitContainerForClassSelection.Panel1.Height;
            //WindowForClassSelection.splitContainerForClassSelection.Panel1.Controls.Add(ClassSelectionPanel);

            //if (WindowForClassSelection.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            ////     WindowForClassSelection.panelForClassesSelection = new 

            //cExtendedList[] ListValuesForHisto = new cExtendedList[/*ClassSelectionPanel.GetListIndexSelectedClass().Count*/ cGlobalInfo.ListWellClasses.Count];

            //List<bool> ListSelectedClass = ClassSelectionPanel.GetListSelectedClass();

            //for (int i = 0; i < ListValuesForHisto.Length; i++)
            //    ListValuesForHisto[i] = new cExtendedList();

            //cWell TempWell;

            //int NumberOfPlates = GlobalInfo.CurrentScreen.ListPlatesActive.Count;

            //double MinValue = double.MaxValue;
            //double MaxValue = double.MinValue;
            //double CurrentValue;

            //int IdxDesc = -1;

            //for (int Idx = 0; Idx < GlobalInfo.CurrentScreen.ListDescriptors.Count; Idx++)
            //{
            //    if (GlobalInfo.CurrentScreen.ListDescriptors[Idx] == this)
            //    {
            //        IdxDesc = Idx;
            //        break;
            //    }
            //}
            //if (IdxDesc == -1) return;

            //// loop on all the plate
            //for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
            //{
            //    cPlate CurrentPlateToProcess = GlobalInfo.CurrentScreen.ListPlatesActive.GetPlate(GlobalInfo.CurrentScreen.ListPlatesActive[PlateIdx].Name);

            //    for (int row = 0; row < GlobalInfo.CurrentScreen.Rows; row++)
            //        for (int col = 0; col < GlobalInfo.CurrentScreen.Columns; col++)
            //        {
            //            TempWell = CurrentPlateToProcess.GetWell(col, row, false);
            //            if (TempWell == null) continue;
            //            else
            //            {
            //                if (TempWell.GetClassIdx() >= 0)
            //                {
            //                    CurrentValue = TempWell.ListDescriptors[IdxDesc].GetValue();
            //                    ListValuesForHisto[TempWell.GetClassIdx()].Add(CurrentValue);
            //                    if (CurrentValue < MinValue) MinValue = CurrentValue;
            //                    if (CurrentValue > MaxValue) MaxValue = CurrentValue;
            //                }
            //            }
            //        }
            //}
            //SimpleForm NewWindow = new SimpleForm();
            //List<double[]>[] HistoPos = new List<double[]>[ListValuesForHisto.Length];
            //Series[] SeriesPos = new Series[ListValuesForHisto.Length];


            //for (int i = 0; i < ListValuesForHisto.Length; i++)
            //{
            //    HistoPos[i] = new List<double[]>();
            //    if (ListSelectedClass[i])
            //        HistoPos[i] = ListValuesForHisto[i].CreateHistogram(MinValue, MaxValue, (int)GlobalInfo.OptionsWindow.numericUpDownHistoBin.Value);

            //    SeriesPos[i] = new Series();
            //}

            //for (int i = 0; i < SeriesPos.Length; i++)
            //{
            //    int Max = 0;
            //    if (HistoPos[i].Count > 0)
            //        Max = HistoPos[i][0].Length;

            //    for (int IdxValue = 0; IdxValue < Max; IdxValue++)
            //    {
            //        SeriesPos[i].Points.AddXY(MinValue + ((MaxValue - MinValue) * IdxValue) / Max, HistoPos[i][1][IdxValue]);
            //        SeriesPos[i].Points[IdxValue].ToolTip = HistoPos[i][1][IdxValue].ToString();
            //        if (GlobalInfo.CurrentScreen.SelectedClass == -1)
            //            SeriesPos[i].Points[IdxValue].Color = Color.Black;
            //        else
            //            SeriesPos[i].Points[IdxValue].Color = GlobalInfo.CurrentScreen.GlobalInfo.ListWellClasses[i].ColourForDisplay;
            //    }
            //}
            //ChartArea CurrentChartArea = new ChartArea();
            //CurrentChartArea.BorderColor = Color.Black;

            //NewWindow.chartForSimpleForm.ChartAreas.Add(CurrentChartArea);
            //CurrentChartArea.Axes[0].MajorGrid.Enabled = false;
            //CurrentChartArea.Axes[0].Title = this.Name;
            //CurrentChartArea.Axes[1].Title = "Sum";
            //CurrentChartArea.AxisX.LabelStyle.Format = "N2";

            //NewWindow.chartForSimpleForm.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            //CurrentChartArea.BackGradientStyle = GradientStyle.TopBottom;
            //CurrentChartArea.BackColor = GlobalInfo.CurrentScreen.GlobalInfo.OptionsWindow.panel1.BackColor;
            //CurrentChartArea.BackSecondaryColor = Color.White;


            //for (int i = 0; i < SeriesPos.Length; i++)
            //{
            //    SeriesPos[i].ChartType = SeriesChartType.StackedColumn;
            //    // SeriesPos[i].Color = GlobalInfo.CurrentScreen.GlobalInfo.GetColor(1);
            //    if (ListSelectedClass[i])
            //        NewWindow.chartForSimpleForm.Series.Add(SeriesPos[i]);
            //}
            ////Series SeriesGaussNeg = new Series();
            ////SeriesGaussNeg.ChartType = SeriesChartType.Spline;

            ////Series SeriesGaussPos = new Series();
            ////SeriesGaussPos.ChartType = SeriesChartType.Spline;

            ////if (HistoPos.Count != 0)
            ////{
            ////    double[] HistoGaussPos = CreateGauss(Mean(Pos.ToArray()), std(Pos.ToArray()), HistoPos[0].Length);

            ////    SeriesGaussPos.Color = Color.Black;
            ////    SeriesGaussPos.BorderWidth = 2;
            ////}
            ////SeriesGaussNeg.Color = Color.Black;
            ////SeriesGaussNeg.BorderWidth = 2;

            ////NewWindow.chartForSimpleForm.Series.Add(SeriesGaussNeg);
            ////NewWindow.chartForSimpleForm.Series.Add(SeriesGaussPos);
            //NewWindow.chartForSimpleForm.ChartAreas[0].CursorX.IsUserEnabled = true;
            //NewWindow.chartForSimpleForm.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            //NewWindow.chartForSimpleForm.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            //NewWindow.chartForSimpleForm.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            //Title CurrentTitle = null;

            //CurrentTitle = new Title(this.Name + " Stacked histogram.");

            //CurrentTitle.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            //NewWindow.chartForSimpleForm.Titles.Add(CurrentTitle);
            //NewWindow.Text = CurrentTitle.Text;
            //NewWindow.Show();
            //NewWindow.chartForSimpleForm.Update();
            //NewWindow.chartForSimpleForm.Show();
            //NewWindow.Controls.AddRange(new System.Windows.Forms.Control[] { NewWindow.chartForSimpleForm });
            //return;
            #endregion

            cGUI_ListClasses GUI_ListClasses = new cGUI_ListClasses();
            GUI_ListClasses.IsCheckBoxes = true;
            GUI_ListClasses.IsSelectAll = true;

            if (GUI_ListClasses.Run().IsSucceed == false) return;
            cExtendedList ListClassSelected = GUI_ListClasses.GetOutPut()[0];

            if (ListClassSelected.Sum() < 1)
            {
                System.Windows.Forms.MessageBox.Show("At least one classe has to be selected.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cDisplayToWindow CDW1 = new cDisplayToWindow();

            

           // if ((ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked) || (ProcessModeEntireScreeningToolStripMenuItem.Checked))
            {
                cListWells ListWellsToProcess = new cListWells(null);
                List<cPlate> PlateList = new List<cPlate>();

//                if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
//                    PlateList.Add(GlobalInfo.CurrentScreen.GetCurrentDisplayPlate());
//                else
//                {
                    foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive) PlateList.Add(TmpPlate);
//                }

                foreach (cPlate TmpPlate in PlateList)
                    foreach (cWell item in TmpPlate.ListActiveWells)
                        if ((item.GetCurrentClassIdx() != -1) && (ListClassSelected[item.GetCurrentClassIdx()] == 1)) ListWellsToProcess.Add(item);


               // if (ProcessModeCurrentPlateOnlyToolStripMenuItem.Checked)
               //     CDW1.Title = GlobalInfo.CurrentScreen.ListDescriptors[GlobalInfo.CurrentScreen.ListDescriptors.CurrentSelectedDescriptor].GetName() + " - Stacked Histogram (" + PlateList[0].Name + ")";
               // else


                CDW1.Title = this.GetName() + " - Stacked Histogram - " + ListWellsToProcess.Count + " Wells";
                    //CDW1.Title = GlobalInfo.CurrentScreen.ListDescriptors[GlobalInfo.CurrentScreen.ListDescriptors.CurrentSelectedDescriptorIdx].GetName() + " - Stacked Histogram - " + PlateList.Count + " plates";



                int IdxDesc = cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(this);

               
                cExtendedTable NewTable = ListWellsToProcess.GetAverageDescriptorValues(IdxDesc);
                NewTable.Name = CDW1.Title;

                cViewerStackedHistogram CV1 = new cViewerStackedHistogram();
                CV1.SetInputData(NewTable);
                CV1.Chart.LabelAxisX = this.GetName();
                CV1.Chart.IsZoomableX = true;
                CV1.Run();

                CDW1.SetInputData(CV1.GetOutPut());
            }
            //else if (ProcessModeplateByPlateToolStripMenuItem.Checked)
            //{
            //    cDesignerTab CDT = new cDesignerTab();
            //    foreach (cPlate TmpPlate in GlobalInfo.CurrentScreen.ListPlatesActive)
            //    {
            //        cListWell ListWellsToProcess = new cListWell(null);
            //        foreach (cWell item in TmpPlate.ListActiveWells)
            //            if ((item.GetClassIdx() != -1) && (ListClassSelected[item.GetClassIdx()] == 1)) ListWellsToProcess.Add(item);

            //        cExtendedTable NewTable = ListWellsToProcess.GetDescriptorValues(GlobalInfo.CurrentScreen.ListDescriptors.CurrentSelectedDescriptor, true);
            //        NewTable.Name = GlobalInfo.CurrentScreen.ListDescriptors[GlobalInfo.CurrentScreen.ListDescriptors.CurrentSelectedDescriptor].GetName() + " - " + TmpPlate.Name;


            //        cViewerStackedHistogram CV1 = new cViewerStackedHistogram();
            //        CV1.SetInputData(NewTable);
            //        CV1.Chart.LabelAxisX = GlobalInfo.CurrentScreen.ListDescriptors[GlobalInfo.CurrentScreen.ListDescriptors.CurrentSelectedDescriptor].GetName();
            //        CV1.Title = TmpPlate.Name;
            //        CV1.Run();

            //        CDT.SetInputData(CV1.GetOutPut());
            //    }
            //    CDT.Run();
            //    CDW1.SetInputData(CDT.GetOutPut());
            //    CDW1.Title = "Stacked Histogram - " + GlobalInfo.CurrentScreen.ListPlatesActive.Count + " plates";
            //}

            CDW1.Run();
            CDW1.Display();

        }

        void RemoveDescItem(object sender, EventArgs e)
        {
            if (this.IsConnectedToDatabase)
            {
                System.Windows.Forms.DialogResult ResWin = System.Windows.Forms.MessageBox.Show("Removal of DB connected descritpor is not current implemented.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                return;
                //System.Windows.Forms.DialogResult ResWin = System.Windows.Forms.MessageBox.Show("By applying this process, the selected descriptor will be definitively removed from the Database ! Proceed ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //if (ResWin == System.Windows.Forms.DialogResult.No) return;

                //foreach (cPlate TmpPlate in this.GlobalInfo.CurrentScreening.ListPlatesAvailable)
                //{
                //    TmpPlate.DBConnection.OpenConnection();
                //    TmpPlate.DBConnection.RemoveColumn(this.Name);
                //    TmpPlate.DBConnection.CloseConnection();
                //}

            }
            else
            {
                System.Windows.Forms.DialogResult ResWin = System.Windows.Forms.MessageBox.Show("By applying this process, the selected descriptor will be definitively removed from this analysis ! Proceed ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (ResWin == System.Windows.Forms.DialogResult.No) return;
                cGlobalInfo.CurrentScreening.ListDescriptors.RemoveDesc(this);
            }


            //GlobalInfo.CurrentScreen.UpDatePlateListWithFullAvailablePlate();
            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
        }

        void InfoDescItem(object sender, EventArgs e)
        {
            WindowDescriptorInfo.ShowDialog();
            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
        }

    }

    public class cListDescriptorTypes : List<cDescriptorType>
    {
        CheckedListBox AssociatedListBox;
        ComboBox AssociatedListDescriptorToDisplay;
        public int CurrentSelectedDescriptorIdx = -1;

        public int GetDescriptorIndex(cDescriptorType DescriptorType)
        {
            int DescIndex = -1;
            foreach (cDescriptorType TmpDescType in this)
            {
                DescIndex++;
                if (TmpDescType.GetName() == DescriptorType.GetName()) return DescIndex;
            }

            return -1;
        }

        public List<cDescriptorType> GetActiveDescriptors()
        {
            List<cDescriptorType> ToReturn = new List<cDescriptorType>();
            
            foreach (cDescriptorType TmpDesc in this)
                if (TmpDesc.IsActive()) ToReturn.Add(TmpDesc);

            return ToReturn;            
        }

        public List<cDescriptorType> GetActiveSingleCellDescriptors()
        {
            List<cDescriptorType> ToReturn = new List<cDescriptorType>();

            foreach (cDescriptorType TmpDesc in this)
                if (TmpDesc.IsActive()&&(TmpDesc.IsConnectedToDatabase)) ToReturn.Add(TmpDesc);

            return ToReturn;
        }


        public cDescriptorType GetActiveDescriptor()
        {
            if (CurrentSelectedDescriptorIdx == -1) return null;
            return this[CurrentSelectedDescriptorIdx];
        }

        public int GetDescriptorIndex(string DescriptorName)
        {
            int DescIndex = -1;
            foreach (cDescriptorType TmpDescType in this)
            {
                DescIndex++;
                if (TmpDescType.GetName() == DescriptorName) return DescIndex;
            }

            return -1;
        }

        public cDescriptorType GetDescriptorByName(string DescriptorName)
        {
            int DescIndex = -1;
            foreach (cDescriptorType TmpDescType in this)
            {
                DescIndex++;
                if (TmpDescType.GetName() == DescriptorName) return TmpDescType;
            }

            return null;
        }

        public void SetCurrentSelectedDescriptor(int Desc)
        {
            this.CurrentSelectedDescriptorIdx = Desc;
            this.AssociatedListDescriptorToDisplay.SelectedIndex = Desc;
        }

        public cListDescriptorTypes(CheckedListBox AssociatedListBox, ComboBox AssociatedComboBox)
        {
            this.AssociatedListBox = AssociatedListBox;
            this.AssociatedListDescriptorToDisplay = AssociatedComboBox;

        }

        /// <summary>
        /// Clear the object as well as the associated control
        /// </summary>
        public void Clean()
        {
            this.Clear();
            AssociatedListBox.Items.Clear();
            AssociatedListDescriptorToDisplay.Items.Clear();
        }

        /// <summary>
        /// Add a descritpor to the global descriptor list
        /// </summary>
        /// <param name="DescriptorsType"></param>
        /// <returns>return false if the descriptor type already exist</returns>
        public bool AddNew(cDescriptorType DescriptorsType)
        {
            foreach (cDescriptorType temp in this)
            {
                if (temp.GetName() == DescriptorsType.GetName())
                    return false;
            }


            this.Add(DescriptorsType);
            this.AssociatedListBox.Items.Add(DescriptorsType.GetName(), true);
            this.AssociatedListDescriptorToDisplay.Items.Add(DescriptorsType.GetName());
            return true;
        }

        public void RemoveDesc(cDescriptorType DescriptorTypeToBeRemoved)
        {
            for (int i = 0; i < this.Count; i++)
            {
                cDescriptorType TmpType = this[i];

                if (DescriptorTypeToBeRemoved == TmpType)
                {
                    foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
                    {
                        foreach (cWell Tmpwell in TmpPlate.ListActiveWells) Tmpwell.ListSignatures.RemoveAt(i);
                    }

                    this.RemoveAt(i);
                    AssociatedListBox.Items.RemoveAt(i);
                    AssociatedListDescriptorToDisplay.Items.RemoveAt(i);
                    AssociatedListDescriptorToDisplay.SelectedIndex = 0;
                    return;

                }
            }
        }

        public void RemoveDescUnSafe(cDescriptorType DescriptorTypeToBeRemoved, cScreening CurrentScreen)
        {
            for (int i = 0; i < this.Count; i++)
            {
                cDescriptorType TmpType = this[i];

                if (DescriptorTypeToBeRemoved == TmpType)
                {
                    foreach (cPlate TmpPlate in CurrentScreen.ListPlatesAvailable)
                    {
                        foreach (cWell Tmpwell in TmpPlate.ListActiveWells) Tmpwell.ListSignatures.RemoveAt(i);
                    }

                    this.RemoveAt(i);
                    AssociatedListBox.Items.RemoveAt(i);
                    AssociatedListDescriptorToDisplay.Items.RemoveAt(i);

                    return;

                }
            }
        }

        public List<string> GetListNameActives()
        {
            List<string> NameActiveDesc = new List<string>();

            foreach (cDescriptorType TmpDesc in this)
            {
                if (TmpDesc.IsActive()) NameActiveDesc.Add(TmpDesc.GetName());
            }
            return NameActiveDesc;
        }

        public List<string> GetListNames()
        {
            List<string> NameDesc = new List<string>();

            foreach (cDescriptorType TmpDesc in this)
            {
                NameDesc.Add(TmpDesc.GetName());
            }
            return NameDesc;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsActive"></param>
        public void SetItemState(int IdxDesc, bool IsActive)
        {
            if (IsActive)
            {
                AssociatedListBox.SetItemCheckState(IdxDesc, CheckState.Checked);
                this[IdxDesc].SetActiveState(true);
            }
            else
            {
                AssociatedListBox.SetItemCheckState(IdxDesc, CheckState.Unchecked);
                this[IdxDesc].SetActiveState(false);
            }

        }

        public void UpDateDisplay()
        {
            int Idx = 0;
            foreach (cDescriptorType TmpType in this)
            {
                AssociatedListBox.Items[Idx] = TmpType.GetName();
                AssociatedListDescriptorToDisplay.Items[Idx] = TmpType.GetName();
                Idx++;
            }
        }

        public cExtendedList GetValue(List<cPlate> ListPlate, cDescriptorType Desc)
        {
            cExtendedList ToReturn = new cExtendedList();

            int Idx = this.GetDescriptorIndex(Desc);

            foreach (cPlate CurrentPlate in ListPlate)
                foreach (cWell TmpWell in CurrentPlate.ListActiveWells)
                    ToReturn.Add(TmpWell.ListSignatures[Idx].GetValue());
            return ToReturn;
        }

        public cExtendedTable GetListWeights()
        {
            cExtendedTable ListWeight = new cExtendedTable();
            ListWeight.Name = "Manual Linear Projection";

            ListWeight.ListRowNames = new List<string>();

            ListWeight.Add(new cExtendedList());
            ListWeight[0].ListTags = new List<object>();
            ListWeight[0].Name = "Weights";
            foreach (var item in this)
            {
                ListWeight[0].Add(item.Weight);
                ListWeight.ListRowNames.Add(item.GetName());
                ListWeight[0].ListTags.Add(item);
            }

            return ListWeight;
        }


        public string GetInfo()
        {
            string ToBeReturned = this.Count + "  descriptors:\n\n";

            int Idx = 0;
            foreach (var item in this)
            {
                ToBeReturned += "[" + Idx + "] - " + item.GetName()+"\n";
                if (item.IsActive())
                    ToBeReturned += "\tActive\n";
                else
                    ToBeReturned += "\tInactive\n";

                if (item.IsConnectedToDatabase)
                    ToBeReturned += "\tSingle object: " + item.GetBinNumber() + " bins.\n";
                else
                    ToBeReturned += "\tWell Averaged.\n";

                ToBeReturned += "\tWeight: " + item.Weight + "\n";
                ToBeReturned += "\tInfo:\n\t" + item.description+"\n\n";

                Idx++;
            }


            return ToBeReturned;

        }

        public cExtendedTable GetDataTable()
        {
            cExtendedTable ToBeReturned = new cExtendedTable(3,this.Count,0);

            ToBeReturned.Name = "List Descriptors";

            ToBeReturned.ListRowNames = new List<string>();
            ToBeReturned.ListTags = new List<object>();

            ToBeReturned[0].Name = "Active State";
            ToBeReturned[1].Name = "Connected to DB";
            ToBeReturned[2].Name = "Weight";

            int Idx = 0;
            foreach (var item in this)
            {
                ToBeReturned.ListRowNames.Add(item.GetName());
                ToBeReturned.ListTags.Add(item);
                if(item.IsActive())
                    ToBeReturned[0][Idx] = 1;
                if (item.IsConnectedToDatabase)
                    ToBeReturned[1][Idx] = 1;

                ToBeReturned[2][Idx] = item.Weight;
                Idx++;
            }


            return ToBeReturned;
        
        }



    }

}
