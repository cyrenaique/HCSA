using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using HCSAnalyzer;
using HCSAnalyzer.Classes;
using weka.core;
using System.Windows.Threading;
using HCSAnalyzer.Classes._3D;
using System.Data.SQLite;
using System.Data;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using HCSAnalyzer.Controls;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Text;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes.MetaComponents;
using Kitware.VTK;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.GUI;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using ImageAnalysis;
using HCSAnalyzer.Classes.Base_Classes.Viewers._2D;
using ImageAnalysisFiltering;
using HCSAnalyzer.Forms.IO;
using HCSAnalyzer.Classes.Base_Classes.Viewers._3D.ComplexObjects;

namespace LibPlateAnalysis
{
    public class cPlate : cGeneralComponent
    {
        public cPlateLUTManager CurrentLUTManager;
        public bool IsActive { get; private set; }
        // string PlateType;
        cWell[,] ListWell = null;

        string Name;
        public string GetName()
        {
            //TmpList[IdxConc].ListProperties.UpdateValueByName("Concentration", (double)Convert.ToDouble(dataGridViewForConcentration.Rows[IdxConc].Cells[1].Value.ToString()));
            string toReturn = (string)this.ListProperties.FindByName("Name").GetValue();
            return toReturn;
        }
        public string AlternateName = "";

        /// <summary>
        /// Define plate quality (for 0 to 1)
        /// </summary>
        public double Quality = 1;

        public cListPlateProperty ListProperties = null;
        public cScreening ParentScreening;
        List<double[]> ListMinMax = null;

        public cListWells ListActiveWells = new cListWells(null);
        public cListWells ListWells = new cListWells(null);

        public cListDRCRegion ListDRCRegions;
        public cDBConnection DBConnection = null;
        //public void DBConnection_Establish(string FileName)
        //{
        //    DBConnection = new cDBConnection(this, FileName);
        //}

        //public int ReplicateNumber = 0;
        int NumberOfActiveWells = 0;
        int[] ListNumObjectPerClasse;
        cInfoClassif InfoClassif = new cInfoClassif();
        public double[] MinMaxHisto = new double[2];

        //public cReplicateType AssociatedReplicateType { get; private set; }

        //public bool SetNewReplicateType(cReplicateType NewReplicate)
        //{
        //    foreach (var item in ParentScreening.ListReplicate)
        //    {
        //        if (NewReplicate == item)
        //        {
        //            this.AssociatedReplicateType = NewReplicate;
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        public cPlate(string Name, cScreening ParentScreening)
        {
            this.ParentScreening = ParentScreening;
            this.Name = Name;
            //this.PlateType = Type;
            ListWell = new cWell[ParentScreening.Columns, ParentScreening.Rows];
            //this.AssociatedReplicateType = ParentScreening.ListReplicate[0];

            this.ListProperties = new cListPlateProperty(this);
            //this.ListProperties.UpdateValueByName("Quality", 1);
            this.ListProperties.UpdateValueByName("Name", Name);

            this.IsActive = true;
            this.CurrentLUTManager = new cPlateLUTManager(this);
            return;
        }

        #region Weka based clustering and classification

        /// <summary>
        /// Create an instances structure without classes for unsupervised methods
        /// </summary>
        /// <returns>a weka Instances object</returns>
        public Instances CreateInstancesWithoutClass()
        {
            weka.core.FastVector atts = new FastVector();
            int columnNo = 0;

            // Descriptors loop
            for (int i = 0; i < ParentScreening.ListDescriptors.Count; i++)
            {
                if (ParentScreening.ListDescriptors[i].IsActive() == false) continue;
                atts.addElement(new weka.core.Attribute(ParentScreening.ListDescriptors[i].GetName()));
                columnNo++;
            }
            weka.core.FastVector attVals = new FastVector();
            Instances data1 = new Instances("MyRelation", atts, 0);

            foreach (cWell CurrentWell in this.ListActiveWells)
            {
                double[] vals = new double[data1.numAttributes()];

                int IdxRealCol = 0;

                for (int Col = 0; Col < ParentScreening.ListDescriptors.Count; Col++)
                {
                    if (ParentScreening.ListDescriptors[Col].IsActive() == false) continue;
                    vals[IdxRealCol++] = CurrentWell.ListSignatures[Col].GetValue();
                }
                data1.add(new DenseInstance(1.0, vals));
            }

            return data1;
        }


        /// <summary>
        /// Create an instances structure with classes for supervised methods
        /// </summary>
        /// <param name="NumClass"></param>
        /// <returns></returns>
        public Instances CreateInstancesWithClasses(cInfoClass InfoClass, int NeutralClass)
        {
            weka.core.FastVector atts = new FastVector();

            int columnNo = 0;

            for (int i = 0; i < ParentScreening.ListDescriptors.Count; i++)
            {
                if (ParentScreening.ListDescriptors[i].IsActive() == false) continue;
                atts.addElement(new weka.core.Attribute(ParentScreening.ListDescriptors[i].GetName()));
                columnNo++;
            }

            weka.core.FastVector attVals = new FastVector();

            for (int i = 0; i < InfoClass.NumberOfClass; i++)
                attVals.addElement("Class__" + (i).ToString());


            atts.addElement(new weka.core.Attribute("Class__", attVals));

            Instances data1 = new Instances("MyRelation", atts, 0);
            int IdxWell = 0;
            foreach (cWell CurrentWell in this.ListActiveWells)
            {
                if (CurrentWell.GetCurrentClassIdx() == NeutralClass) continue;
                double[] vals = new double[data1.numAttributes()];

                int IdxCol = 0;
                for (int Col = 0; Col < ParentScreening.ListDescriptors.Count; Col++)
                {
                    if (ParentScreening.ListDescriptors[Col].IsActive() == false) continue;
                    vals[IdxCol++] = CurrentWell.ListSignatures[Col].GetValue();
                }
                vals[columnNo] = InfoClass.CorrespondanceTable[CurrentWell.GetCurrentClassIdx()];
                data1.add(new DenseInstance(1.0, vals));
                IdxWell++;
            }
            data1.setClassIndex((data1.numAttributes() - 1));

            return data1;
        }



        /// <summary>
        /// Create an instances structure with classes for supervised methods
        /// </summary>
        /// <param name="NumClass"></param>
        /// <returns></returns>
        public Instances CreateInstancesWithClasses(List<bool> ListClassSelected)
        {
            weka.core.FastVector atts = new FastVector();
            int columnNo = 0;
            for (int i = 0; i < ParentScreening.ListDescriptors.Count; i++)
            {
                if (ParentScreening.ListDescriptors[i].IsActive() == false) continue;
                atts.addElement(new weka.core.Attribute(ParentScreening.ListDescriptors[i].GetName()));
                columnNo++;
            }

            weka.core.FastVector attVals = new FastVector();
            foreach (var item in cGlobalInfo.ListWellClasses)
            {
                attVals.addElement(item.Name);
            }

            atts.addElement(new weka.core.Attribute("ClassAttribute", attVals));

            Instances data1 = new Instances("MyRelation", atts, 0);
            int IdxWell = 0;
            foreach (cWell CurrentWell in this.ListActiveWells)
            {
                if (!ListClassSelected[CurrentWell.GetCurrentClassIdx()]) continue;
                double[] vals = new double[data1.numAttributes()];

                int IdxCol = 0;
                for (int Col = 0; Col < ParentScreening.ListDescriptors.Count; Col++)
                {
                    if (ParentScreening.ListDescriptors[Col].IsActive() == false) continue;
                    vals[IdxCol++] = CurrentWell.ListSignatures[Col].GetValue();
                }
                vals[columnNo] = CurrentWell.GetCurrentClassIdx();
                data1.add(new DenseInstance(1.0, vals));
                IdxWell++;
            }
            data1.setClassIndex((data1.numAttributes() - 1));

            return data1;
        }



        /// <summary>
        /// Create an instances structure with classes for supervised methods
        /// </summary>
        /// <param name="NumClass"></param>
        /// <returns></returns>
        public Instances CreateInstancesWithClassesWithPlateBasedDescriptor(int NumberOfClass)
        {
            weka.core.FastVector atts = new FastVector();

            int columnNo = 0;

            for (int i = 0; i < ParentScreening.ListPlateBaseddescriptorNames.Count; i++)
            {
                atts.addElement(new weka.core.Attribute(ParentScreening.ListPlateBaseddescriptorNames[i]));
                columnNo++;
            }

            weka.core.FastVector attVals = new FastVector();

            for (int i = 0; i < NumberOfClass; i++)
                attVals.addElement("Class" + (i).ToString());

            atts.addElement(new weka.core.Attribute("Class", attVals));

            Instances data1 = new Instances("MyRelation", atts, 0);
            int IdxWell = 0;
            foreach (cWell CurrentWell in this.ListActiveWells)
            {
                if (CurrentWell.GetCurrentClassIdx() == -1) continue;
                double[] vals = new double[data1.numAttributes()];
                int IdxCol = 0;
                for (int Col = 0; Col < ParentScreening.ListPlateBaseddescriptorNames.Count; Col++)
                {
                    vals[IdxCol++] = CurrentWell.ListPlateBasedDescriptors[Col].GetValue();
                }
                vals[columnNo] = CurrentWell.GetCurrentClassIdx();
                data1.add(new DenseInstance(1.0, vals));
                IdxWell++;
            }
            data1.setClassIndex((data1.numAttributes() - 1));

            return data1;
        }


        public cInfoForHierarchical CreateInstancesWithUniqueClasse()
        {
            cInfoForHierarchical InfoForHierarchical = new cInfoForHierarchical();
            weka.core.FastVector atts = new FastVector();

            int columnNo = 0;

            for (int i = 0; i < this.ParentScreening.ListDescriptors.Count; i++)
            {
                if (this.ParentScreening.ListDescriptors[i].IsActive() == false) continue;
                atts.addElement(new weka.core.Attribute(this.ParentScreening.ListDescriptors[i].GetName()));
                columnNo++;
            }

            weka.core.FastVector attVals = new FastVector();
            atts.addElement(new weka.core.Attribute("Class_____", attVals));

            InfoForHierarchical.ListInstances = new Instances("MyRelation", atts, 0);
            int IdxWell = 0;
            foreach (cWell CurrentWell in this.ListActiveWells)
            {
                if (CurrentWell.GetCurrentClassIdx() == -1) continue;
                attVals.addElement("Class_____" + (IdxWell).ToString());

                InfoForHierarchical.ListIndexedWells.Add(CurrentWell);

                double[] vals = new double[InfoForHierarchical.ListInstances.numAttributes()];
                int IdxCol = 0;
                for (int Col = 0; Col < this.ParentScreening.ListDescriptors.Count; Col++)
                {
                    if (this.ParentScreening.ListDescriptors[Col].IsActive() == false) continue;
                    vals[IdxCol++] = CurrentWell.ListSignatures[Col].GetValue();
                }
                vals[columnNo] = IdxWell;
                InfoForHierarchical.ListInstances.add(new DenseInstance(1.0, vals));
                IdxWell++;
            }

            InfoForHierarchical.ListInstances.setClassIndex((InfoForHierarchical.ListInstances.numAttributes() - 1));
            return InfoForHierarchical;
        }

        /// <summary>
        /// Assign a class to each well based on table
        /// </summary>
        /// <param name="ListClasses">Table containing the classes</param>
        public void AssignClass(double[] ListClasses)
        {
            int i = 0;
            foreach (cWell CurrentWell in this.ListActiveWells)
                CurrentWell.SetClass((int)ListClasses[i++]);
        }
        #endregion

        public string GetShortInfo()
        {
            base.ShortInfo = "Plate [" + this.Name + "]";
            return base.ShortInfo;
        }

        public string GetCompleteInfo()
        {
            string ToReturn = "Plate [" + this.Name + "]\n";

            return ToReturn;
        }

        public void DisplayHistogram(int DescIdx)
        {
            cExtendedList Pos = new cExtendedList();
            //cWell TempWell;

            foreach (cWell item in this.ListActiveWells)
            {
                Pos.Add(item.ListSignatures[DescIdx].GetValue());

            }

            if (Pos.Count == 0)
            {
                MessageBox.Show("Not enough active well selected !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //List<double[]> HistoPos = cGlobalInfo.WindowHCSAnalyzer.CreateHistogram(Pos.ToArray(), (int)cGlobalInfo.OptionsWindow.numericUpDownHistoBin.Value);
            cWindowToDisplayHisto NewWindow = new cWindowToDisplayHisto(ParentScreening, Pos);

            Series SeriesPos = new Series();
            SeriesPos.ShadowOffset = 1;

            /*      if (HistoPos.Count == 0) return;

                  for (int IdxValue = 0; IdxValue < HistoPos[0].Length; IdxValue++)
                  {
                      SeriesPos.Points.AddXY(HistoPos[0][IdxValue], HistoPos[1][IdxValue]);
                      SeriesPos.Points[IdxValue].ToolTip = HistoPos[1][IdxValue].ToString();
                      SeriesPos.Points[IdxValue].Color = Color.DarkBlue;

                  }

                  ChartArea CurrentChartArea = new ChartArea();
                  CurrentChartArea.BorderColor = Color.Black;

                  NewWindow.chartForSimpleForm.ChartAreas.Add(CurrentChartArea);
                  CurrentChartArea.Axes[0].MajorGrid.Enabled = false;
                  CurrentChartArea.Axes[0].Title = ParentScreening.ListDescriptors[DescIdx].GetName();
                  CurrentChartArea.Axes[1].Title = "Sum";
                  CurrentChartArea.AxisX.LabelStyle.Format = "N2";

                  NewWindow.chartForSimpleForm.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
                  CurrentChartArea.BackGradientStyle = GradientStyle.TopBottom;
                  CurrentChartArea.BackColor = cGlobalInfo.OptionsWindow.panel1.BackColor;
                  CurrentChartArea.BackSecondaryColor = Color.White;

                  SeriesPos.ChartType = SeriesChartType.Column;
                  SeriesPos.Color = cGlobalInfo.GetColor(1);
                  NewWindow.chartForSimpleForm.Series.Add(SeriesPos);

                  NewWindow.chartForSimpleForm.ChartAreas[0].CursorX.IsUserEnabled = true;
                  NewWindow.chartForSimpleForm.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                  NewWindow.chartForSimpleForm.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
                  NewWindow.chartForSimpleForm.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

                  if (cGlobalInfo.OptionsWindow.checkBoxDisplayHistoStats.Checked)
                  {
                      StripLine AverageLine = new StripLine();
                      AverageLine.BackColor = Color.Black;
                      AverageLine.IntervalOffset = Pos.Mean();
                      AverageLine.StripWidth = double.Epsilon;
                      CurrentChartArea.AxisX.StripLines.Add(AverageLine);
                      AverageLine.Text = String.Format("{0:0.###}", AverageLine.IntervalOffset);

                      StripLine StdLine = new StripLine();
                      StdLine.BackColor = Color.FromArgb(64, Color.Black);
                      double Std = Pos.Std();
                      StdLine.IntervalOffset = AverageLine.IntervalOffset - 0.5 * Std;
                      StdLine.StripWidth = Std;
                      CurrentChartArea.AxisX.StripLines.Add(StdLine);
                      AverageLine.StripWidth = 0.0001;
                  }
                  */
            Title CurrentTitle = null;

            CurrentTitle = new Title(ParentScreening.GetCurrentDisplayPlate().Name + " - " + ParentScreening.ListDescriptors[DescIdx].GetName() + " histogram.");

            CurrentTitle.Font = new System.Drawing.Font("Arial", 11, FontStyle.Bold);
            NewWindow.chartForSimpleForm.Titles.Add(CurrentTitle);
            NewWindow.Text = CurrentTitle.Text;
            NewWindow.Show();
            NewWindow.chartForSimpleForm.Update();
            NewWindow.chartForSimpleForm.Show();
            NewWindow.Controls.AddRange(new System.Windows.Forms.Control[] { NewWindow.chartForSimpleForm });
            return;
        }

        public void DisplayDescriptorsWindow()
        {
            List<cPanelForDisplayArray> ListPlates = new List<cPanelForDisplayArray>();
            for (int DescIdx = 0; DescIdx < this.ParentScreening.ListDescriptors.Count; DescIdx++)
            {
                if (this.ParentScreening.ListDescriptors[DescIdx].IsActive())
                    ListPlates.Add(new FormToDisplayDescriptorPlate(this, this.ParentScreening, DescIdx));
            }

            cWindowToDisplayEntireDescriptors WindowToDisplayArray = new cWindowToDisplayEntireDescriptors(ListPlates, this.ParentScreening.GetCurrentDisplayPlate().Name, 6);
            WindowToDisplayArray.Show();
        }




        public int[] UpdateNumberOfClass()
        {
            ListNumObjectPerClasse = new int[cGlobalInfo.ListWellClasses.Count + 1];

            for (int i = 0; i < this.ListWells.Count; i++)
            {
                cWell TempWell = this.ListWells[i];
                ListNumObjectPerClasse[TempWell.GetCurrentClassIdx() + 1]++;
            }

            if (ParentScreening.GetSelectionType() >= -1)
            {
                System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
                double Value = 0;
                if ((cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListWells.Count > 0) && (ListNumObjectPerClasse[ParentScreening.GetSelectionType() + 1] > 0))
                    Value = ((100.0 * ListNumObjectPerClasse[ParentScreening.GetSelectionType() + 1]) / cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListWells.Count);

                cGlobalInfo.LabelForClass.Text = ListNumObjectPerClasse[ParentScreening.GetSelectionType() + 1].ToString() + " <=> " + Value.ToString("N2") + " %";
                cGlobalInfo.LabelForClass.Update();

                cGlobalInfo.WindowHCSAnalyzer.UpdateQCDisplay();
        
            }

            

            return ListNumObjectPerClasse;
        }

        public int GetNumberOfClasses()
        {
            int NumberOfClasses = 0;
            int[] ListClasses = UpdateNumberOfClass();

            for (int i = 0; i < ListClasses.Length; i++)
            {
                if (ListClasses[i] > 0) NumberOfClasses++;
            }

            return NumberOfClasses;
        }

        public int GetNumberOfWellOfClass(int Class)
        {
            int[] ListClasses = UpdateNumberOfClass();
            return ListClasses[Class + 1];
        }

        public cInfoClassif GetInfoClassif()
        {
            return this.InfoClassif;
        }

        public cInfoClass GetNumberOfClassesBut(int NeutralClass)
        {

            NeutralClass++;
            int[] ListClasses = UpdateNumberOfClass();

            cInfoClass InfoClass = new cInfoClass();
            InfoClass.CorrespondanceTable = new int[cGlobalInfo.ListWellClasses.Count];

            for (int i = 1; i < ListClasses.Length; i++)
            {
                if ((ListClasses[i] > 0) && (i != NeutralClass))
                {
                    InfoClass.CorrespondanceTable[i - 1] = InfoClass.NumberOfClass++;
                    InfoClass.ListBackAssociation.Add(i - 1);
                }
                else
                    InfoClass.CorrespondanceTable[i - 1] = -1;
            }

            return InfoClass;
        }

        public int GetNumberOfActiveWellsButClass(int NeutralClass)
        {
            int NumberOfActive = 0;

            for (int row = 0; row < ParentScreening.Rows; row++)
                for (int col = 0; col < ParentScreening.Columns; col++)
                    if ((GetWell(col, row, true) != null) && (GetWell(col, row, true).GetCurrentClassIdx() != NeutralClass)) NumberOfActive++;
            return NumberOfActive;
        }

        public void ComputePlateBasedDescriptors()
        {

            cDescriptorType TypeRow = new cDescriptorType("Row_Pos", true, 1);
            cDescriptorType TypeCol = new cDescriptorType("Col_Pos", true, 1);
            cDescriptorType TypeDistBorder = new cDescriptorType("Dist_To_Border", true, 1);
            cDescriptorType TypeDistCenter = new cDescriptorType("Dist_To_Center", true, 1);


            for (int j = 0; j < ParentScreening.Rows; j++)
                for (int i = 0; i < ParentScreening.Columns; i++)
                {
                    cWell TempWell = GetWell(i, j, false);
                    if (TempWell == null) continue;

                    TempWell.ListPlateBasedDescriptors = new cListSignature();

                    cSignature Descriptor0 = new cSignature(j, TypeRow, this.ParentScreening);
                    TempWell.ListPlateBasedDescriptors.Add(Descriptor0);

                    cSignature Descriptor1 = new cSignature(i, TypeCol, this.ParentScreening);
                    TempWell.ListPlateBasedDescriptors.Add(Descriptor1);

                    double MinDist = i + 1;
                    double DistToRight = ParentScreening.Columns - i;
                    if (DistToRight < MinDist) MinDist = DistToRight;
                    double DistToTop = j + 1;
                    if (DistToTop < MinDist) MinDist = DistToTop;
                    double DistToBottom = ParentScreening.Rows - j;
                    if (DistToBottom < MinDist) MinDist = DistToBottom;

                    cSignature Descriptor2 = new cSignature(MinDist, TypeDistBorder, this.ParentScreening);
                    TempWell.ListPlateBasedDescriptors.Add(Descriptor2);


                    double X_Center = ParentScreening.Columns / 2;
                    double Y_Center = ParentScreening.Rows / 2;

                    double DistToCenter = Math.Sqrt((i - X_Center) * (i - X_Center) + (j - Y_Center) * (j - Y_Center));

                    cSignature Descriptor3 = new cSignature(DistToCenter, TypeDistCenter, this.ParentScreening);
                    TempWell.ListPlateBasedDescriptors.Add(Descriptor3);

                }

            return;
        }

        public bool AddWell(cWell NewWell)
        {
            int PosX = NewWell.GetPosX() - 1;
            int PosY = NewWell.GetPosY() - 1;

            try
            {
                ListWell[PosX, PosY] = NewWell;
                ListActiveWells.Add(NewWell);
                ListWells.Add(NewWell);

            }
            catch (Exception)
            {
                return false;                
            }

            return true;
        }

        public void UpDateWellsSelection()
        {
            int SelectionType = ParentScreening.GetSelectionType();
            if (SelectionType == -2) return;

            int PosMouseXMax = ParentScreening.ClientPosLast.X;
            int PosMouseXMin = ParentScreening.ClientPosFirst.X;

            if (ParentScreening.ClientPosFirst.X > PosMouseXMax)
            {
                PosMouseXMax = ParentScreening.ClientPosFirst.X;
                PosMouseXMin = ParentScreening.ClientPosLast.X;
            }

            int PosMouseYMax = ParentScreening.ClientPosLast.Y;
            int PosMouseYMin = ParentScreening.ClientPosFirst.Y;
            if (ParentScreening.ClientPosFirst.Y > PosMouseYMax)
            {
                PosMouseYMax = ParentScreening.ClientPosFirst.Y;
                PosMouseYMin = ParentScreening.ClientPosLast.Y;
            }

            int GutterSize = (int)cGlobalInfo.OptionsWindow.FFAllOptions.numericUpDownGutter.Value;
            int ScrollShiftX = cGlobalInfo.WindowHCSAnalyzer.panelForPlate.HorizontalScroll.Value;
            int ScrollShiftY = cGlobalInfo.WindowHCSAnalyzer.panelForPlate.VerticalScroll.Value;

            cListPlates ListPlateToProcess = new cListPlates();

            if (ParentScreening.IsSelectionApplyToAllPlates == true)
            {
                ListPlateToProcess = ParentScreening.ListPlatesActive;
            }
            else
            {
                ListPlateToProcess.Add(ParentScreening.GetCurrentDisplayPlate());
            }
            int NumberOfPlates = ParentScreening.ListPlatesActive.Count;

            //Point ResMin = cGlobalInfo.WindowHCSAnalyzer.panelForPlate.GetChildAtPoint(new Point(PosMouseXMin, PosMouseYMin));

            int PosWellMinX = (int)((PosMouseXMin - ScrollShiftX) / (cGlobalInfo.SizeHistoWidth + GutterSize));
            int PosWellMinY = (int)((PosMouseYMin - ScrollShiftY) / (cGlobalInfo.SizeHistoHeight + GutterSize));

            int PosWellMaxX = (int)((PosMouseXMax - ScrollShiftX) / (cGlobalInfo.SizeHistoWidth + GutterSize));
            int PosWellMaxY = (int)((PosMouseYMax - ScrollShiftY) / (cGlobalInfo.SizeHistoHeight + GutterSize));


            if (PosWellMaxX > ParentScreening.Columns) PosWellMaxX = ParentScreening.Columns;
            if (PosWellMaxY > ParentScreening.Rows) PosWellMaxY = ParentScreening.Rows;
            if (PosWellMinX < 0) PosWellMinX = 0;
            if (PosWellMinY < 0) PosWellMinY = 0;


            foreach (cPlate CurrentPlate in ListPlateToProcess)
            {

                for (int j = PosWellMinY; j < PosWellMaxY; j++)
                    for (int i = PosWellMinX; i < PosWellMaxX; i++)
                    {
                        cWell TempWell = CurrentPlate.GetWell(i, j, false);

                        if (TempWell == null) continue;
                        if (SelectionType == -1)
                            TempWell.SetAsNoneSelected();
                        else
                            TempWell.SetClass(SelectionType);
                    }
            }
            ParentScreening.GetCurrentDisplayPlate().UpdateNumberOfClass();
            ParentScreening.UpdateListActiveWell();

        }

        public void Display3Dplate(int IdxDescriptor, cPoint3D MinimumPosition)
        {
            ParentScreening._3DWorldForPlateDisplay.ListMetaObjectList = new List<cMetaBiologicalObjectList>();
            cMetaBiologicalObjectList ListMetacells = new cMetaBiologicalObjectList("List Meta Objects");
            ParentScreening._3DWorldForPlateDisplay.ListMetaObjectList.Add(ListMetacells);

            c3DWell NewPlate3D = new c3DWell(new cPoint3D(0, 0, 0), new cPoint3D(ParentScreening.Columns, ParentScreening.Rows, 1), Color.Black, null);
            NewPlate3D.SetOpacity(0.0);

            cMetaBiologicalObject Plate3D = new cMetaBiologicalObject(this.Name, ListMetacells, NewPlate3D);



            #region  display the well list

            foreach (cWell TmpWell in this.ListWell)
            {
                if ((TmpWell == null) || (TmpWell.GetCurrentClassIdx() == -1)) continue;

                double PosZ = 8 - ((TmpWell.ListSignatures[IdxDescriptor].GetValue() - this.ListMinMax[IdxDescriptor][0]) * 8) / (this.ListMinMax[IdxDescriptor][1] - this.ListMinMax[IdxDescriptor][0]);


                double WellSize = (double)cGlobalInfo.OptionsWindow.numericUpDownWellSize.Value;
                double WellBorder = (1 - WellSize) / 2.0;

                cPoint3D CurrentPos = new cPoint3D(TmpWell.GetPosX() - WellBorder + MinimumPosition.X, TmpWell.GetPosY() + WellBorder + MinimumPosition.Y - WellSize / 2, PosZ + MinimumPosition.Z - WellBorder);
                Color WellColor = Color.Black;

                if (cGlobalInfo.IsDisplayClassOnly)
                    WellColor = TmpWell.GetClassColor();
                else
                {
                    int ConvertedValue;
                    byte[][] LUT = cGlobalInfo.CurrentPlateLUT;

                    if (this.ListMinMax[IdxDescriptor][0] == this.ListMinMax[IdxDescriptor][1])
                        ConvertedValue = 0;
                    else
                        ConvertedValue = (int)(((TmpWell.ListSignatures[IdxDescriptor].GetValue() - this.ListMinMax[IdxDescriptor][0]) * (LUT[0].Length - 1)) / (this.ListMinMax[IdxDescriptor][1] - this.ListMinMax[IdxDescriptor][0]));
                    if ((ConvertedValue >= 0) && (ConvertedValue < LUT[0].Length))
                        WellColor = Color.FromArgb(LUT[0][ConvertedValue], LUT[1][ConvertedValue], LUT[2][ConvertedValue]);
                }

                c3DWell New3DWell = new c3DWell(CurrentPos, new cPoint3D(CurrentPos.X + WellSize, CurrentPos.Y + WellSize, CurrentPos.Z + WellSize / 2.0), WellColor, TmpWell);
                New3DWell.SetType("[" + TmpWell.GetPosX() + " x " + TmpWell.GetPosY() + "]");

                cPropertyType WellPropertyType = null;
                foreach (object item in cGlobalInfo.WindowHCSAnalyzer.toolStripDropDownButtonDisplayMode.DropDownItems)
                {

                    if (item.GetType() == typeof(ToolStripMenuItem))
                    {
                        ToolStripMenuItem TmpMenuItem = ((ToolStripMenuItem)item);

                        if (TmpMenuItem.Checked && (TmpMenuItem.Tag != null) && (TmpMenuItem.Tag.GetType() == typeof(cPropertyType)))
                        {
                            WellPropertyType = ((cPropertyType)(TmpMenuItem).Tag);
                            break;
                        }
                    }
                }

                if ((WellPropertyType != null) && (cGlobalInfo.ViewMode != eViewMode.PIE))
                {
                    Title MainLegend = new Title();

                    string ToDisp = "";

                    if (WellPropertyType != null)
                    {

                        object CurrentValue = TmpWell.ListProperties.FindValueByName(WellPropertyType.Name);
                        if (CurrentValue != null) ToDisp = CurrentValue.ToString();//((double)CurrentValue).ToString("e4");
                        else
                            ToDisp = "n.a.";

                    }

                    New3DWell.AddText(ToDisp, ParentScreening._3DWorldForPlateDisplay, 0.1);
                }

                New3DWell.SetOpacity((double)cGlobalInfo.OptionsWindow.numericUpDownWellOpacity.Value);
                ParentScreening._3DWorldForPlateDisplay.AddBiological3DObject(New3DWell);
                Plate3D.AddObject(New3DWell);
            }

            #endregion

            #region Well numbering
            if (cGlobalInfo.OptionsWindow.checkBox3DPlateInformation.Checked)
            {
                for (int i = 1; i <= ParentScreening.Columns; i++)
                {
                    c3DText CurrentText = new c3DText(ParentScreening._3DWorldForPlateDisplay, i.ToString(), new cPoint3D(i - 1 + MinimumPosition.X, -0.5 + MinimumPosition.Y, 0 + MinimumPosition.Z), Color.White, 0.35);
                    ParentScreening._3DWorldForPlateDisplay.AddGeometric3DObject(CurrentText);
                }
                for (int j = 1; j <= ParentScreening.Rows; j++)
                {
                    c3DText CurrentText1;

                    if (cGlobalInfo.OptionsWindow.radioButtonWellPosModeDouble.Checked)
                        CurrentText1 = new c3DText(ParentScreening._3DWorldForPlateDisplay, j.ToString(), new cPoint3D(-1 + MinimumPosition.X, j - 0.5 + MinimumPosition.Y, 0 + MinimumPosition.Z), Color.White, 0.35);
                    else
                        CurrentText1 = new c3DText(ParentScreening._3DWorldForPlateDisplay, cGlobalInfo.ConvertIntPosToStringPos(j), new cPoint3D(-1 + MinimumPosition.X, j - 0.5 + MinimumPosition.Y, 0 + MinimumPosition.Z), Color.White, 0.35);

                    ParentScreening._3DWorldForPlateDisplay.AddGeometric3DObject(CurrentText1);
                }
            }
            #endregion

            if (this.ListDRCRegions != null)
            {
                foreach (cDRC_Region Region in this.ListDRCRegions)
                {

                    if (cGlobalInfo.OptionsWindow.checkBox1.Checked)
                    {

                        cDRC CurrentDRC = Region.GetDRC(this.ParentScreening.ListDescriptors[IdxDescriptor]);
                        if ((CurrentDRC == null) || (CurrentDRC.ResultFit == null) || (CurrentDRC.ResultFit.Y_Estimated.Count <= 1)) continue;


                        c3DDRC New3DDRC = new c3DDRC(CurrentDRC, Region, Color.White, this.ListMinMax[IdxDescriptor][0], this.ListMinMax[IdxDescriptor][1]);
                        New3DDRC.SetOpacity((double)cGlobalInfo.OptionsWindow.numericUpDownDRCOpacity.Value);
                        //New3DDRC.SetType("[" + TmpWell.GetPosX() + " x " + TmpWell.GetPosY() + "]");
                        ParentScreening._3DWorldForPlateDisplay.AddGeometric3DObject(New3DDRC);
                        //Plate3D.AddObject(New3DDRC);
                    }

                    if (cGlobalInfo.OptionsWindow.checkBox3DComputeThinPlate.Checked)
                    {
                        c3DThinPlate NewThinPlate = new c3DThinPlate(Region, (double)cGlobalInfo.OptionsWindow.numericUpDown3DThinPlateRegularization.Value);

                        if (cGlobalInfo.OptionsWindow.checkBox3DDisplayThinPlate.Checked)
                        {
                            NewThinPlate.SetOpacity(0.5);
                            NewThinPlate.SetToWireFrame();
                            ParentScreening._3DWorldForPlateDisplay.AddGeometric3DObject(NewThinPlate);
                        }
                        if (cGlobalInfo.OptionsWindow.checkBox3DDisplayIsoboles.Checked)
                        {
                            for (double PosZContour = 0; PosZContour <= 10.0; PosZContour += 1.5)
                            {
                                c3DIsoContours NewContour = new c3DIsoContours(NewThinPlate, Region, Color.Red, PosZContour, true);
                                ParentScreening._3DWorldForPlateDisplay.AddBiological3DObject(NewContour);
                            }
                        }
                        if (cGlobalInfo.OptionsWindow.checkBox3DDisplayIsoRatioCurves.Checked)
                        {
                            //   for (double PosZContour = 0; PosZContour <= 15.0; PosZContour += 3)
                            {
                                c3DIsoContours NewContour = new c3DIsoContours(NewThinPlate, Region, Color.Blue, 0/*PosZContour*/, false);
                                ParentScreening._3DWorldForPlateDisplay.AddBiological3DObject(NewContour);
                            }
                        }
                    }
                }
            }
        }

        public void Refresh3D(int IdxDescriptor)
        {
            if (cGlobalInfo.Is3DVisu())
            {
                if (ParentScreening._3DWorldForPlateDisplay == null)
                {
                    ParentScreening._3DWorldForPlateDisplay = new c3DWorld(new cPoint3D(ParentScreening.Columns, ParentScreening.Rows, 1), new cPoint3D(1, 1, 1), cGlobalInfo.renderWindowControlForVTK, cGlobalInfo.WinSize, this.ParentScreening);

                    Display3Dplate(IdxDescriptor, new cPoint3D(0, 0, 0));

                    ParentScreening._3DWorldForPlateDisplay.DisplayBottom(Color.FromArgb(255, 255, 255));
                    ParentScreening._3DWorldForPlateDisplay.SetBackgroundColor(Color.Black);
                    ParentScreening._3DWorldForPlateDisplay.ren1.GetActiveCamera().Zoom(1.8);
                    ParentScreening._3DWorldForPlateDisplay.Render();

                    double[] p = ParentScreening._3DWorldForPlateDisplay.ren1.GetActiveCamera().GetPosition();
                    ParentScreening._3DWorldForPlateDisplay.ren1.GetActiveCamera().SetPosition(p[0], p[1], p[2] - 4);
                }
                else
                {
                    double[] View = ParentScreening._3DWorldForPlateDisplay.ren1.GetViewPoint();

                    //this.ren1.ResetCamera();
                    double[] fp = ParentScreening._3DWorldForPlateDisplay.ren1.GetActiveCamera().GetFocalPoint();
                    double[] p = ParentScreening._3DWorldForPlateDisplay.ren1.GetActiveCamera().GetPosition();
                    double[] ViewUp = ParentScreening._3DWorldForPlateDisplay.ren1.GetActiveCamera().GetViewUp();



                    double dist = Math.Sqrt((p[0] - fp[0]) * (p[0] - fp[0]) + (p[1] - fp[1]) * (p[1] - fp[1]) + (p[2] - fp[2]) * (p[2] - fp[2]));
                    //  int[] WinPos = new int[2];

                    if (ParentScreening._3DWorldForPlateDisplay == null)
                    {
                        cGlobalInfo.WinSize[0] = 750;
                        cGlobalInfo.WinSize[1] = 400;
                    }
                    else
                    {
                        cGlobalInfo.WinSize[0] = ParentScreening._3DWorldForPlateDisplay.renWin.GetSize()[0];
                        cGlobalInfo.WinSize[1] = ParentScreening._3DWorldForPlateDisplay.renWin.GetSize()[1];

                    }


                    //WinPos[0] = ParentScreening._3DWorldForPlateDisplay.renWin.GetSize()[0];
                    //WinPos[1] = ParentScreening._3DWorldForPlateDisplay.renWin.GetSize()[1];


                    //  ParentScreening._3DWorldForPlateDisplay.Terminate();
                    //  ParentScreening._3DWorldForPlateDisplay = null;
                    //  ParentScreening._3DWorldForPlateDisplay = new c3DWorld(new cPoint3D(ParentScreening.Columns, ParentScreening.Rows, 1), new cPoint3D(1, 1, 1), cGlobalInfo.renderWindowControlForVTK, cGlobalInfo.WinSize);

                    //ParentScreening._3DWorldForPlateDisplay.ren1.GetActiveCamera().Roll(180);
                    //ParentScreening._3DWorldForPlateDisplay.ren1.GetActiveCamera().Azimuth(180);

                    ParentScreening._3DWorldForPlateDisplay.ren1.RemoveAllViewProps();

                    ParentScreening._3DWorldForPlateDisplay.ren1.GetActiveCamera().SetFocalPoint(fp[0], fp[1], fp[2]);
                    ParentScreening._3DWorldForPlateDisplay.ren1.GetActiveCamera().SetPosition(p[0], p[1], p[2]);
                    ParentScreening._3DWorldForPlateDisplay.ren1.GetActiveCamera().SetViewUp(ViewUp[0], ViewUp[1], ViewUp[2]);

                    //    ParentScreening._3DWorldForPlateDisplay.ren1.GetActiveCamera().Zoom(1.8);

                }

                Display3Dplate(IdxDescriptor, new cPoint3D(0, 0, 0));

                if (cGlobalInfo.OptionsWindow.checkBox3DPlateInformation.Checked) ParentScreening._3DWorldForPlateDisplay.DisplayBottom(Color.FromArgb(255, 255, 255));
                ParentScreening._3DWorldForPlateDisplay.SetBackgroundColor(Color.Black);
                ParentScreening._3DWorldForPlateDisplay.SimpleRender();
            }

            #region Close the 3D view
            else
            {
                if (ParentScreening._3DWorldForPlateDisplay != null)
                {
                    ParentScreening._3DWorldForPlateDisplay.Terminate();
                    ParentScreening._3DWorldForPlateDisplay = null;
                }
            }
            #endregion
        }

        public void Display3DDistributionOnly(int IdxDescriptor)
        {
            if (IdxDescriptor >= ListMinMax.Count) return;
            Refresh3D(IdxDescriptor);
            return;
        }

        public void DisplayDistribution(cDescriptorType Descriptor, bool IsFirstTime, object Sender = null)
        {
            this.DisplayDistribution(ParentScreening.ListDescriptors.GetDescriptorIndex(Descriptor), IsFirstTime, Sender);
        }

        void DisplayDistribution(int IdxDescriptor, bool IsFirstTime, object Sender)
        {
            if (ListMinMax == null) this.UpDataMinMax();

            if (IdxDescriptor >= ListMinMax.Count) return;
            double[] MinMax = this.ListMinMax[IdxDescriptor];

            Refresh3D(IdxDescriptor);

            #region 2D display
            //   if (!this.cGlobalInfo.IsDisplayClassOnly)
            {
                cGlobalInfo.SizeHistoWidth = (cGlobalInfo.OriginalHistoWidth * cGlobalInfo.WindowHCSAnalyzer.XTrackBarForZoom.trackBar.Value) / 100.0f;
                cGlobalInfo.SizeHistoHeight = (cGlobalInfo.OriginalHistoHeight * cGlobalInfo.WindowHCSAnalyzer.XTrackBarForZoom.trackBar.Value) / 100.0f;

                //  ParentScreening.PanelForPlate.Controls.Clear();

                //double[] MinMax = ListMinMax[IdxDescriptor];
                // List<PlateChart> LChart = new List<PlateChart>();
                //int PosScrollX = cGlobalInfo.panelForPlate.HorizontalScroll.Value;
                //int PosScrollY = cGlobalInfo.panelForPlate.VerticalScroll.Value;

                cGlobalInfo.panelForPlate.Controls.Clear();
                List<PlateChart> LChart = new List<PlateChart>();

                if (cGlobalInfo.IsDisplayClassOnly)
                {

                    for (int j = 0; j < ParentScreening.Rows; j++)
                        for (int i = 0; i < ParentScreening.Columns; i++)
                        {
                            cWell TempWell = GetWell(i, j, false);
                            if (TempWell == null) continue;

                            // Add chart control to the form
                            LChart.Add(TempWell.BuildChartForClass());
                        }

                    // return;
                }
                else
                {
                    if (cGlobalInfo.ViewMode == eViewMode.DISTRIBUTION)
                    {
                        UpdateMinMaxHisto(IdxDescriptor);
                    }
                    if (cGlobalInfo.ViewMode == eViewMode.IMAGE)
                    {
                        for (int j = 0; j < ParentScreening.Rows; j++)
                            for (int i = 0; i < ParentScreening.Columns; i++)
                            {
                                cWell TempWell = GetWell(i, j, false);
                                if (TempWell == null) continue;


                                //  Panel TmpPanel = TempWell.BuildChartForImage();

                                cGlobalInfo.panelForPlate.Controls.Add(TempWell.BuildChartForImage());
                            }
                    }
                    else
                    {

                        for (int j = 0; j < ParentScreening.Rows; j++)
                            for (int i = 0; i < ParentScreening.Columns; i++)
                            {
                                cWell TempWell = GetWell(i, j, false);
                                if (TempWell == null) continue;
                                LChart.Add(TempWell.BuildChart(IdxDescriptor, MinMax));
                            }
                    }
                }
                //System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()    {  ParentScreening.PanelForPlate.BeginInvoke(new Action(delegate() {            })); }));
                //thread.Start();
                if (cGlobalInfo.ViewMode != eViewMode.IMAGE)
                    cGlobalInfo.panelForPlate.Controls.AddRange(LChart.ToArray());

                //cGlobalInfo.panelForPlate.HorizontalScroll.Value = PosScrollX;
                //cGlobalInfo.panelForPlate.VerticalScroll.Value = PosScrollY;
                if (MinMax[0] != MinMax[1]) DisplayLUT(IdxDescriptor);
            }

            #endregion

            cGlobalInfo.WindowHCSAnalyzer.RefreshViews(Sender);

            if (!cGlobalInfo.IsDisplayClassOnly) cGlobalInfo.GUIPlateLUT.CurrentFormForPlateLUT.RefreshMinMax();

            //         this.UpDateWellsSelection();
            return;
        }

        public void Refresh1DScatter()
        {
            List<cWellClassType> ListWellClassesSelected = cGlobalInfo.ListWellClasses;

            cViewerGraph1D V1D = new cViewerGraph1D();
            V1D.Chart.IsSelectable = true;
            V1D.Chart.LabelAxisX = "Well Index";
            V1D.Chart.LabelAxisY = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
            V1D.Chart.IsDetachable = false;
            V1D.Chart.IsXGrid = true;


            cExtendedList EtF = new cExtendedList();
            for (int i = 0; i < cGlobalInfo.ListWellClasses.Count; i++)
            {
                EtF.Add(1);
            }

            cExtendedTable DataFromPlate = new cExtendedTable(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListActiveWells,
                                            cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx, EtF);

            DataFromPlate.Name = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetName();

            V1D.Chart.IsShadow = true;
            V1D.Chart.IsBorder = true;
            //V1D.Chart.IsSelectable = true;
            V1D.Chart.CurrentTitle.Tag = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate();
            V1D.SetInputData(DataFromPlate);
            V1D.Run();

            cDesignerSplitter DS = new cDesignerSplitter();
            DS.Orientation = System.Windows.Forms.Orientation.Horizontal;
            DS.SetInputData(V1D.GetOutPut());

            cExtendedTable NewTable = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().ListActiveWells.Filter(ListWellClassesSelected).GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
            NewTable.Name = "Histogram";

            cViewerStackedHistogram CV1 = new cViewerStackedHistogram();
            CV1.SetInputData(NewTable);
            CV1.Chart.LabelAxisX = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
            CV1.Run();
            CV1.Chart.IsDetachable = false;

            DS.SetInputData(CV1.GetOutPut());
            DS.Run();

            cGlobalInfo.WindowHCSAnalyzer.tabPage1DView.Controls.Clear();
            DS.GetOutPut().Width = cGlobalInfo.WindowHCSAnalyzer.tabPage1DView.Width;
            DS.GetOutPut().Height = cGlobalInfo.WindowHCSAnalyzer.tabPage1DView.Height;

            DS.GetOutPut().Controls[0].Width = cGlobalInfo.WindowHCSAnalyzer.tabPage1DView.Width;
            DS.GetOutPut().Controls[0].Height = cGlobalInfo.WindowHCSAnalyzer.tabPage1DView.Height;

            cGlobalInfo.WindowHCSAnalyzer.tabPage1DView.Controls.Add(DS.GetOutPut());

        }

        public void Refresh3DScreenView()
        {
            cViewer3D V3D = new cViewer3D();
            c3DNewWorld MyWorld = new c3DNewWorld(new cPoint3D(1, 1, 1), new cPoint3D(1, 1, 1));
            V3D.SetInputData(MyWorld);

            c3DObject_Screening2D Screening2D = new c3DObject_Screening2D();
            Screening2D.SetInputData(cGlobalInfo.CurrentScreening);
            Screening2D.Run(MyWorld);
            MyWorld.AddGeometric3DObjects(Screening2D.GetOutPut());

           // c3DObject_Plate2D Plate2D = new c3DObject_Plate2D();
           // Plate2D.SetInputData(this);
           // Plate2D.Run(MyWorld);
           // MyWorld.AddGeometric3DObjects(Plate2D.GetOutPut());

           // MyWorld.BackGroundColor = Color.White;


            V3D.Run();

            cExtendedControl EXT = V3D.GetOutPut();
            cGlobalInfo.WindowHCSAnalyzer.tabPage3DPlatesView.Controls.Clear();

            EXT.Width = cGlobalInfo.WindowHCSAnalyzer.tabPage3DPlatesView.Width;
            EXT.Height = cGlobalInfo.WindowHCSAnalyzer.tabPage3DPlatesView.Height;

            // EXT.Controls[0].Width = cGlobalInfo.WindowHCSAnalyzer.tabPage3DPlatesView.Width;
            // EXT.Controls[0].Height = cGlobalInfo.WindowHCSAnalyzer.tabPage3DPlatesView.Height;

            cGlobalInfo.WindowHCSAnalyzer.tabPage3DPlatesView.Controls.Add(EXT);

            //cDesignerSinglePanel CD = new cDesignerSinglePanel();
            //CD.SetInputData(V3D.GetOutPut());
            //if (CD.Run().IsSucceed == false) return;

            //cDisplayToWindow CDW = new cDisplayToWindow();
            //CDW.SetInputData(CD.GetOutPut());
            //CDW.Title = "3D world";
            //if (CDW.Run().IsSucceed == false) return;
            //CDW.Display();

        }



        public cImage GetColorImage()
        {
            int IdxDesc = cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor());
            bool MissingWells;
            cImage ToReturn = new cImage(new cExtendedTable(this.GetAverageValueDescTable(IdxDesc, out MissingWells)));


            return ToReturn;
        }

        public cImage GetClassColorImage()
        {
            cImage ToReturn = new cImage(cGlobalInfo.CurrentScreening.Columns, cGlobalInfo.CurrentScreening.Rows, 1, 3);

            for (int j = 0; j < ToReturn.Height; j++)
                for (int i = 0; i < ToReturn.Width; i++)
                {
                    cWell TmpWell = this.GetWell(i , j , true);
                    if(TmpWell==null) continue;

                    Color C = TmpWell.GetClassColor();

                    ToReturn.SingleChannelImage[0].Data[i + j * ToReturn.Width] = C.R;
                    ToReturn.SingleChannelImage[1].Data[i + j * ToReturn.Width] = C.G;
                    ToReturn.SingleChannelImage[2].Data[i + j * ToReturn.Width] = C.B;
                }


            return ToReturn;
        }

        public void Refresh2DScatter()
        {
            cExtendedTable EL = this.ListActiveWells.GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.GetActiveDescriptors(), false, false);
            EL.Name = this.Name;

            cViewer2DScatterPoint V2D = new cViewer2DScatterPoint();
            V2D.SetInputData(EL);
            V2D.Chart.IsZoomableX = true;
            V2D.Chart.IsZoomableY = true;
            V2D.Chart.IsDetachable = false;
            V2D.Run();

            cExtendedControl EXT = V2D.GetOutPut();
            cGlobalInfo.WindowHCSAnalyzer.tabPage2DView.Controls.Clear();

            EXT.Width = cGlobalInfo.WindowHCSAnalyzer.tabPage2DView.Width;
            EXT.Height = cGlobalInfo.WindowHCSAnalyzer.tabPage2DView.Height;

            if (EXT.Controls.Count > 0)
            {
                EXT.Controls[0].Width = cGlobalInfo.WindowHCSAnalyzer.tabPage2DView.Width;
                EXT.Controls[0].Height = cGlobalInfo.WindowHCSAnalyzer.tabPage2DView.Height;
            }
            cGlobalInfo.WindowHCSAnalyzer.tabPage2DView.Controls.Add(EXT);

        }

        public void RefreshTableView()
        {
            cExtendedTable EL = this.ListActiveWells.GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.GetActiveDescriptors(), false, false);
            EL.Name = this.Name;

            cViewerTable VT = new cViewerTable();
            VT.SetInputData(EL);

            foreach (var item in cGlobalInfo.CurrentScreening.ListWellPropertyTypes)
            {
                if (!item.IsTobeDisplayed) continue;

                EL.Add(new cExtendedList(item.Name));
                int IdxCol = EL.Count - 1;

                if ((item.Type == eDataType.BOOL) || (item.Type == eDataType.DOUBLE) || (item.Type == eDataType.INTEGER))
                {

                    for (int i = 0; i < EL.ListTags.Count; i++)
                    {
                        cWell TmpWell = ((cWell)EL.ListTags[i]);
                        cProperty P = TmpWell.ListProperties.FindPropertyByName(item.Name);
                        if (P == null) continue;

                        if (P.GetValue() == null)
                            EL[IdxCol].Add(double.NaN);
                        else
                        {
                            object o = P.GetValue();
                            if (item.Type == eDataType.INTEGER)
                                EL[IdxCol].Add((int)(o));
                            else if (item.Type == eDataType.DOUBLE)
                                EL[IdxCol].Add((double)(o));
                            else
                            {
                                bool Res = (bool)o;
                                if (Res) EL[IdxCol].Add(1);
                                else EL[IdxCol].Add(0);
                            }
                        }
                    }

                }
            }


            VT.Run();
            cExtendedControl EXT = VT.GetOutPut();
            cGlobalInfo.WindowHCSAnalyzer.panelForTableView.Controls.Clear();
            EXT.Width = cGlobalInfo.WindowHCSAnalyzer.panelForTableView.Width;
            EXT.Height = cGlobalInfo.WindowHCSAnalyzer.panelForTableView.Height;

            cGlobalInfo.WindowHCSAnalyzer.panelForTableView.Controls.Add(VT.GetOutPut());
        }


        private void UpdateMinMaxHisto(int IdxDescriptor)
        {
            MinMaxHisto[0] = double.MaxValue;
            MinMaxHisto[1] = double.MinValue;

            double CurrentValue;

            foreach (cWell CurrentWell in this.ListWell)
            {
                if (CurrentWell == null) continue;

                CurrentValue = CurrentWell.ListSignatures[IdxDescriptor].Histogram.Min();
                if (CurrentValue < MinMaxHisto[0])
                    MinMaxHisto[0] = CurrentValue;

                CurrentValue = CurrentWell.ListSignatures[IdxDescriptor].Histogram.Max();
                if (CurrentValue > MinMaxHisto[1])
                    MinMaxHisto[1] = CurrentValue;

            }

            cGlobalInfo.OptionsWindow.numericUpDownAutomatedMin.Value = (decimal)MinMaxHisto[0];
            cGlobalInfo.OptionsWindow.numericUpDownAutomatedMax.Value = (decimal)MinMaxHisto[1];

        }

        public void DisplayLUT(int IdxDescriptor)
        {
            if (ParentScreening.LabelForMin == null) return;

            //     ParentScreening.LabelForMin.Text = String.Format("{0:0.######}", ListMinMax[IdxDescriptor][0]);
            //    ParentScreening.LabelForMax.Text = String.Format("{0:0.######}", ListMinMax[IdxDescriptor][1]);
        }

        public cWell GetWell(int Col, int Row, bool OnlyIfSelected)
        {
            if ((Col >= this.ParentScreening.Columns) || (Row >= this.ParentScreening.Rows)) return null;
            if (ListWell[Col, Row] == null) return null;
            if ((OnlyIfSelected) && (ListWell[Col, Row].GetCurrentClassIdx() == -1))
                return null;
            else return ListWell[Col, Row];
        }

        double[] GetMinMax(int IdxDescriptor)
        {
            double[] Boundaries = new double[2];

            double Min = double.MaxValue;
            double Max = double.MinValue;
            double CurrentVal;

            for (int x = 0; x < ParentScreening.Columns; x++)
                for (int y = 0; y < ParentScreening.Rows; y++)
                {
                    if (ListWell[x, y] == null) continue;

                    cWell TWell = GetWell(x, y, false);

                    if (IdxDescriptor >= TWell.ListSignatures.Count) return null;

                    if (TWell == null) continue;
                    CurrentVal = TWell.ListSignatures[IdxDescriptor].GetValue();// ListWell[x, y].ListDescriptors[IdxDescriptor].AverageValue;
                    if (CurrentVal < Min) Min = CurrentVal;
                    if (CurrentVal > Max) Max = CurrentVal;
                }
            Boundaries[0] = Min;
            Boundaries[1] = Max;

            return Boundaries;
        }

        public cExtendedTable GetAverageValueList(cDescriptorType DescriptorType, bool OnlySelectedWells)
        {
            List<cDescriptorType> LDesc = new List<cDescriptorType>();
            LDesc.Add(DescriptorType);

            cExtendedTable DV = this.ListActiveWells.GetAverageDescriptorValues(LDesc, false, false);

            return DV;
        }


        public cExtendedTable GetAverageValueTable(cDescriptorType DescriptorType, bool OnlySelectedWells)
        {
            cExtendedTable DV = new cExtendedTable(ParentScreening.Columns, ParentScreening.Rows, double.NaN);

            DV.ListRowNames = new List<string>();

            for (int i = 0; i < ParentScreening.Columns; i++)
            {
                DV[i].Name = (i + 1).ToString();
                DV[i].ListTags = new List<object>();
                for (int j = 0; j < ParentScreening.Rows; j++)
                {
                    if (i == 0)
                    {
                        byte[] strArray = new byte[1];
                        strArray[0] = (byte)(j + 65);

                        string Chara = Encoding.UTF7.GetString(strArray);
                        DV.ListRowNames.Add(String.Format("{0}\t", Chara));
                    }
                    cWell TmpWell = this.GetWell(i, j, OnlySelectedWells);

                    if (TmpWell != null)
                    {
                        DV[i].ListTags.Add(TmpWell);
                        DV[i][j] = TmpWell.GetAverageValue(DescriptorType);
                    }
                }
            }

            DV.Name = this.GetName() + " - " + DescriptorType.GetName();

            return DV;
        }

        public cExtendedTable GetMinMax(cDescriptorType DescriptorType)
        {
            cExtendedList MinMax = new cExtendedList();
            MinMax.Name = "Min-Max";

            cExtendedTable ListValues = this.GetAverageValueList(DescriptorType, false);
            if (ListValues == null) return null;
            MinMax.Add(ListValues.Min());
            MinMax.Add(ListValues.Max());

            return new cExtendedTable(MinMax);
        }

        public void LoadFromDisk(string Path)
        {
            if (ListWell == null)
            {
                cGlobalInfo.ConsoleWriteLine("ListWell NULL");
                return;
            }
            IEnumerable<string> ListFile = Directory.EnumerateFiles(Path, "*.txt", SearchOption.TopDirectoryOnly);
            int ProcessedWell = 0;
            foreach (string FileName in ListFile)
            {
                cWell NewWell = new cWell(FileName, this.ParentScreening, this);
                if (NewWell.GetPosX() != -1) ProcessedWell++;
                this.AddWell(NewWell);
                //ListWell[NewWell.GetPosX() - 1, NewWell.GetPosY() - 1] = NewWell;
            }

            ListMinMax = new List<double[]>();
            for (int i = 0; i < ParentScreening.ListDescriptors.Count; i++)
            {
                double[] TmpMinMax = GetMinMax(i);
                ListMinMax.Add(TmpMinMax);
            }

            this.NumberOfActiveWells = ProcessedWell;
            cGlobalInfo.ConsoleWriteLine(ProcessedWell + " well(s) succesfully processed");
        }

        public void UpDataMinMax()
        {
            ListMinMax = new List<double[]>();
            for (int i = 0; i < ParentScreening.ListDescriptors.Count; i++)
            {
                double[] TmpMinMax = GetMinMax(i);
                if (ListMinMax != null) ListMinMax.Add(TmpMinMax);
            }
            return;
        }

        public int GetNumberOfActiveWells()
        {
            int NumberOfActive = 0;

            for (int row = 0; row < ParentScreening.Rows; row++)
                for (int col = 0; col < ParentScreening.Columns; col++)
                    if (GetWell(col, row, true) != null) NumberOfActive++;
            return NumberOfActive;
        }

        public double[,] GetAverageValueDescTable(int Desc, out bool IsMissingWell)
        {
            IsMissingWell = false;
            double[,] Table = new double[ParentScreening.Columns, ParentScreening.Rows];

            for (int j = 0; j < ParentScreening.Rows; j++)
                for (int i = 0; i < ParentScreening.Columns; i++)
                {
                    cWell currentWell = this.GetWell(i, j, true);
                    if (currentWell == null)
                        IsMissingWell = true;
                    else
                        Table[i, ParentScreening.Rows - j - 1] = currentWell.GetAverageValuesList(false)[0][Desc];
                }

            return Table;
        }

        public double[,] GetWellClassesTable()
        {
            double[,] Table = new double[ParentScreening.Columns, ParentScreening.Rows];

            for (int j = 0; j < ParentScreening.Rows; j++)
                for (int i = 0; i < ParentScreening.Columns; i++)
                {
                    cWell currentWell = this.GetWell(i, j, false);
                    if (currentWell == null) Table[i, ParentScreening.Rows - j - 1] = double.NaN;
                    else
                        Table[i, ParentScreening.Rows - j - 1] = currentWell.GetCurrentClassIdx();
                }

            return Table;
        }

        public double[][] GetAverageValueDescTable1(int Desc, out bool IsMissingWell)
        {
            IsMissingWell = false;
            double[][] Table = new double[ParentScreening.Rows][];
            for (int J = 0; J < Table.Length; J++)
                Table[J] = new double[ParentScreening.Columns];

            for (int j = 0; j < ParentScreening.Rows; j++)
                for (int i = 0; i < ParentScreening.Columns; i++)
                {
                    cWell currentWell = this.GetWell(i, j, true);
                    if (currentWell == null)
                    {
                        IsMissingWell = true;
                        Table[j][i] = double.NaN;
                    }
                    else
                        Table[j][i] = currentWell.GetAverageValuesList(false)[0][Desc];
                }

            return Table;
        }

        public void SetAverageValueDescTable(int Desc, double[,] Table)
        {

            for (int j = 0; j < ParentScreening.Rows; j++)
                for (int i = 0; i < ParentScreening.Columns; i++)
                {
                    cWell currentWell = this.GetWell(i, j, true);
                    if (currentWell == null)
                        continue;

                    currentWell.ListSignatures[Desc].SetHistoValues(Table[i, j]);
                    currentWell.ListSignatures[Desc].UpDateDescriptorStatistics();
                }

            UpDataMinMax();
        }

        public ToolStripMenuItem GetExtendedContextMenu()
        {
            #region Context Menu
            base.SpecificContextMenu = new ToolStripMenuItem("Plate [" + this.Name + "]");
            // ToolStripSeparator Sep = new ToolStripSeparator();
            // base.SpecificContextMenu.Items.Add(Sep);


            //ToolStripMenuItem ToolStripMenuItem_Info = new ToolStripMenuItem("Test Automated Menu");

            //base.SpecificContextMenu.Items.Add(ToolStripMenuItem_Info);

            ////   contextMenuStrip.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem_Info, ToolStripMenuItem_Histo, ToolStripSep, ToolStripMenuItem_Kegg, ToolStripSep1, ToolStripMenuItem_Copy });

            ////ToolStripSeparator SepratorStrip = new ToolStripSeparator();
            //// contextMenuStrip.Show(Control.MousePosition);
            //ToolStripMenuItem_Info.Click += new System.EventHandler(this.DisplayInfo);




            ToolStripMenuItem ToolStripMenuItem_CopyValuestoClipBoard = new ToolStripMenuItem("Copy Values to Clipboard");
            ToolStripMenuItem_CopyValuestoClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_CopyValuestoClipBoard);
            base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_CopyValuestoClipBoard);

            //ToolStripMenuItem ToolStripMenuItem_CopyClassToClipBoard = new ToolStripMenuItem("Copy Classes to Clipboard");
            //ToolStripMenuItem_CopyClassToClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_CopyClassToClipBoard);
            //base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_CopyClassToClipBoard);

            ToolStripMenuItem ToolStripMenuItem_CopyPropertyToClipBoard = new ToolStripMenuItem("Copy Property to Clipboard");
            ToolStripMenuItem_CopyPropertyToClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_CopyPropertyToClipBoard);
            base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_CopyPropertyToClipBoard);

            //  if (cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor().IsConnectedToDatabase)
            {
                ToolStripMenuItem ToolStripMenuItem_ExportToHTML = new ToolStripMenuItem("Export to HTML");
                ToolStripMenuItem_ExportToHTML.Click += new System.EventHandler(this.ToolStripMenuItem_ExportToHTML);
                base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ExportToHTML);
            }

            if (this.ListActiveWells.Count > 0)
                base.SpecificContextMenu.DropDownItems.Add(this.ListActiveWells.GetContextMenu());


            ToolStripMenuItem ToolStripMenuItem_CopyWellsToList = new ToolStripMenuItem("Copy Well(s) to List Wells");
            ToolStripMenuItem_CopyWellsToList.Click += new System.EventHandler(this.ToolStripMenuItem_CopyWellsToList);
            base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_CopyWellsToList);


            ToolStripSeparator ToolStripSep = new ToolStripSeparator();
            base.SpecificContextMenu.DropDownItems.Add(ToolStripSep);


            ToolStripMenuItem ToolStripMenuItem_SetAsActivePlate = new ToolStripMenuItem("Set as Active");
            ToolStripMenuItem_SetAsActivePlate.Click += new System.EventHandler(this.ToolStripMenuItem_SetAsActivePlate);
            base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_SetAsActivePlate);

            ToolStripMenuItem ToolStripMenuItem_Clustering = new ToolStripMenuItem("Cluster");
            ToolStripMenuItem_Clustering.Click += new System.EventHandler(this.ToolStripMenuItem_Clustering);
            base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Clustering);


            //base.SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            //ToolStripMenuItem ToolStripMenuItem_DisplayImages = new ToolStripMenuItem("Display Images");
            //ToolStripMenuItem_DisplayImages.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayImages);
            //ToolStripMenuItem_DisplayImages.Enabled = false;
            //base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayImages);

            ToolStripMenuItem DisplayAsContextMenu = new ToolStripMenuItem("Display as...");

            ToolStripMenuItem ToolStripMenuItem_DisplayAs2DScatterPoints = new ToolStripMenuItem("2D Scatter Points");
            ToolStripMenuItem_DisplayAs2DScatterPoints.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayAs2DScatterPoints);
            DisplayAsContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayAs2DScatterPoints);

            ToolStripMenuItem ToolStripMenuItem_DisplayAsStackedHistograms = new ToolStripMenuItem("Stacked Histograms");
            ToolStripMenuItem_DisplayAsStackedHistograms.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayAsStackedHistograms);
            DisplayAsContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayAsStackedHistograms);

            ToolStripMenuItem ToolStripMenuItem_DisplayDataTable = new ToolStripMenuItem("Data Table");
            ToolStripMenuItem_DisplayDataTable.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayDataTable);
            DisplayAsContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayDataTable);

            ToolStripMenuItem ToolStripMenuItem_DisplayImages = new ToolStripMenuItem("Image Montage");
            ToolStripMenuItem_DisplayImages.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayImages);
            // ToolStripMenuItem_DisplayImages.Enabled = false;
            DisplayAsContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayImages);

            DisplayAsContextMenu.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_DisplayDataTableDistance = new ToolStripMenuItem("Distance Matrix (Euclidean)");
            ToolStripMenuItem_DisplayDataTableDistance.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayDataTableDistance);
            DisplayAsContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayDataTableDistance);

            base.SpecificContextMenu.DropDownItems.Add(DisplayAsContextMenu);


            base.SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_PlateInfo = new ToolStripMenuItem("Info");
            ToolStripMenuItem_PlateInfo.Click += new System.EventHandler(this.ToolStripMenuItem_PlateInfo);
            base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_PlateInfo);


            //ToolStripMenuItem ToolStripMenuItem_RemovePlate = new ToolStripMenuItem("Remove");
            //ToolStripMenuItem_RemovePlate.Click += new System.EventHandler(this.ToolStripMenuItem_RemovePlate);
            //base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_RemovePlate);




            //ToolStripMenuItem ToolStripMenuItem_Histo = new ToolStripMenuItem("Histogram");
            //ToolStripMenuItem_Histo.Click += new System.EventHandler(this.DisplayHisto);
            //SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Histo);

            //if (this.LocusID != -1.0)
            //{
            //    ToolStripMenuItem ToolStripMenuItem_Kegg = new ToolStripMenuItem("Kegg");
            //    ToolStripMenuItem_Kegg.Click += new System.EventHandler(this.DisplayPathways);
            //    SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Kegg);
            //}

            //if (this.SQLTableName != "")
            //{
            //    ToolStripSeparator ToolStripSep = new ToolStripSeparator();
            //    SpecificContextMenu.DropDownItems.Add(ToolStripSep);

            //    ToolStripMenuItem ToolStripMenuItem_DisplayData = new ToolStripMenuItem("Display Single Object Data");
            //    ToolStripMenuItem_DisplayData.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayData);
            //    SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayData);

            //    ToolStripMenuItem ToolStripMenuItem_AddToSingleCellAnalysis = new ToolStripMenuItem("Add to Single Object Analysis");
            //    ToolStripMenuItem_AddToSingleCellAnalysis.Click += new System.EventHandler(this.ToolStripMenuItem_AddToSingleCellAnalysis);
            //    SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_AddToSingleCellAnalysis);
            //}

            //ToolStripSeparator ToolStripSep1 = new ToolStripSeparator();
            //ToolStripMenuItem ToolStripMenuItem_Copy = new ToolStripMenuItem("Copy Visu.");

            //ToolStripSeparator SepratorStrip = new ToolStripSeparator();
            //base.SpecificContextMenu.Show(Control.MousePosition);
            //ToolStripMenuItem_Copy.Click += new System.EventHandler(this.CopyVisu);
            #endregion

            return base.SpecificContextMenu;
        }

        private void ToolStripMenuItem_CopyValuestoClipBoard(object sender, EventArgs e)
        {
            CopyValuestoClipBoard();
        }

        private void ToolStripMenuItem_ExportToHTML(object sender, EventArgs e)
        {
            ExportToHTML(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor());
        }

        public void ExportToHTML(cDescriptorType Desc)
        {
            if (Desc.IsConnectedToDatabase == false) return;
            List<cDescriptorType> LCDT = new List<cDescriptorType>();
            LCDT.Add(Desc);

            List<cCellularPhenotype> ListCellularPhenotypesToBeSelected = new List<cCellularPhenotype>();

            cGUI_ListClasses GUIClasses = new cGUI_ListClasses();
            GUIClasses.IsCheckBoxes = true;
            GUIClasses.IsSelectAll = true;
            GUIClasses.ClassType = eClassType.PHENOTYPE;
            if (!GUIClasses.Run().IsSucceed) return;

            int IDx = 0;
            foreach (var item in cGlobalInfo.ListCellularPhenotypes)
            {
                if ((GUIClasses.GetOutPut()[0][IDx++] != 0))
                    ListCellularPhenotypesToBeSelected.Add(item);
            }

            cExtendedTable ET = new cExtendedTable(cGlobalInfo.CurrentScreening.Columns, cGlobalInfo.CurrentScreening.Rows, 0);
            ET.Name = this.Name + " [" + Desc.GetName() + "]";
            ET.ListRowNames = new List<string>();

            for (int i = 0; i < ET[0].Count; i++)
            {
                ET.ListRowNames.Add(((char)(i + 65)).ToString());
            }

            FormForProgress MyProgressBar = new FormForProgress();

            MyProgressBar.progressBar.Maximum = this.ListActiveWells.Count;
            MyProgressBar.Show();

            //ET.ListTags = new List<object>();
            for (int i = 0; i < cGlobalInfo.CurrentScreening.Columns; i++)
            {
                ET[i].ListTags = new List<object>();
                ET[i].Name = (i + 1).ToString();

                for (int j = 0; j < cGlobalInfo.CurrentScreening.Rows; j++)
                {

                    MyProgressBar.progressBar.Update();

                    cWell TmpWell = this.GetWell(i, j, true);
                    if (TmpWell == null)
                    {
                        ET[i].ListTags.Add("n.a.");
                        continue;
                    }
                    MyProgressBar.progressBar.Value++;
                    cExtendedTable TmpET = null;
                    if (cGlobalInfo.OptionsWindow.FFAllOptions.checkBoxHTMLExportStackedHisto.Checked)
                        TmpET = TmpWell.GetValuesList(LCDT, ListCellularPhenotypesToBeSelected, true);
                    else
                        TmpET = TmpWell.GetValuesList(LCDT, ListCellularPhenotypesToBeSelected, false);
                    TmpET.Name = "[" + TmpWell.GetPos() + "] - " + TmpET[0].Count + " Objects";
                    // get the values associated to this well and descriptor

                    if (cGlobalInfo.OptionsWindow.FFAllOptions.checkBoxHTMLExportStackedHisto.Checked)
                    {
                        cExtendedTable FinalTable = new cExtendedTable();
                        FinalTable.Name = "Stacked Histogram - " + this.GetName();

                        int Idx = 0;
                        foreach (var item in cGlobalInfo.ListCellularPhenotypes)
                        {
                            FinalTable.Add(new cExtendedList());
                            FinalTable[Idx].Name = item.Name;
                            FinalTable[Idx].Tag = item;


                            for (int k = 0; k < TmpET[1].Count; k++)
                            {
                                if (TmpET[1][k] == Idx)
                                    FinalTable[Idx].Add(TmpET[0][k]);
                            }
                            Idx++;
                        }


                        cViewerStackedHistogram VSH = new cViewerStackedHistogram();
                        VSH.SetInputData(FinalTable);//TmpET);
                        VSH.Chart.LabelAxisX = Desc.GetName();
                        VSH.Chart.IsLine = false;
                        VSH.Chart.CurrentTitle.Text = TmpET.Name;
                        if ((cGlobalInfo.OptionsWindow.radioButtonHistoDisplayManualMinMax.Checked) || (cGlobalInfo.OptionsWindow.radioButtonHistoDisplayAutomatedMinMax.Checked))
                        {
                            VSH.Chart.DefaultAxisXMin = new cExtendedList();
                            VSH.Chart.DefaultAxisXMin.Add((double)cGlobalInfo.OptionsWindow.numericUpDownManualMin.Value);

                            VSH.Chart.DefaultAxisXMax = new cExtendedList();
                            VSH.Chart.DefaultAxisXMax.Add((double)cGlobalInfo.OptionsWindow.numericUpDownManualMax.Value);

                            if (cGlobalInfo.OptionsWindow.FFAllOptions.checkBoxHTMLExportBackColorClass.Checked)
                                VSH.Chart.BackgroundColor = TmpWell.GetClassColor();
                        }

                        VSH.Chart.BinNumber = Desc.GetBinNumber();
                        VSH.Chart.IsShadow = false;
                        VSH.Chart.IsBorder = false;
                        VSH.Run();
                        ET[i].ListTags.Add((Chart)VSH.Chart);
                    }
                    else
                    {
                        cViewerHistogram VH = new cViewerHistogram();
                        VH.SetInputData(TmpET);
                        VH.Chart.LabelAxisX = Desc.GetName();
                        VH.Chart.CurrentTitle.Text = TmpET.Name;
                        if ((cGlobalInfo.OptionsWindow.radioButtonHistoDisplayManualMinMax.Checked) || (cGlobalInfo.OptionsWindow.radioButtonHistoDisplayAutomatedMinMax.Checked))
                        {
                            VH.Chart.DefaultAxisXMin = new cExtendedList();
                            VH.Chart.DefaultAxisXMin.Add((double)cGlobalInfo.OptionsWindow.numericUpDownManualMin.Value);

                            VH.Chart.DefaultAxisXMax = new cExtendedList();
                            VH.Chart.DefaultAxisXMax.Add((double)cGlobalInfo.OptionsWindow.numericUpDownManualMax.Value);

                            if (cGlobalInfo.OptionsWindow.FFAllOptions.checkBoxHTMLExportBackColorClass.Checked)
                                VH.Chart.BackgroundColor = TmpWell.GetClassColor();
                        }

                        VH.Run();
                        ET[i].ListTags.Add((Chart)VH.Chart);
                    }
                }
            }

            MyProgressBar.Close();

            cTableToHTML TToHTML = new cTableToHTML();
            TToHTML.IsDisplayUIForFilePath = true;
            TToHTML.SetInputData(ET);

            TToHTML.Run();
        }




        private void ToolStripMenuItem_CopyWellsToList(object sender, EventArgs e)
        {
            cGUI_ListClasses GUIListClasses = new cGUI_ListClasses();
            GUIListClasses.IsCheckBoxes = true;
            if (GUIListClasses.Run().IsSucceed == false) return;
            cExtendedTable ListClassSelected = GUIListClasses.GetOutPut();

            foreach (cWell TmpWell in this.ListActiveWells)
            {
                if ((TmpWell.GetCurrentClassIdx() < 0) || (TmpWell.GetCurrentClassIdx() >= cGlobalInfo.ListWellClasses.Count)) continue;
                if ((int)ListClassSelected[0][TmpWell.GetCurrentClassIdx()] == 1)
                {
                    TmpWell.AddToListWellsGUI();
                }
            }
        }

        public cExtendedTable GetListValues(bool IsOnlySelectedDesc)
        {
            cExtendedTable ToReturn = new cExtendedTable();
            cExtendedList ListCol = new cExtendedList("Column");
            cExtendedList ListRow = new cExtendedList("Row");
            ToReturn.Add(ListCol);
            ToReturn.Add(ListRow);
            ToReturn[0].ListTags = new List<object>();
            ToReturn[1].ListTags = new List<object>();

            foreach (cDescriptorType item in ParentScreening.ListDescriptors)
            {
                if (item.IsActive())
                {
                    cExtendedList NewList = new cExtendedList(item.GetName());
                    NewList.ListTags = new List<object>();
                    NewList.Tag = item;
                    ToReturn.Add(NewList);
                }
            }

            cExtendedList ListClasses = new cExtendedList("Class");
            ToReturn.Add(ListClasses);
            ToReturn[ToReturn.Count - 1].ListTags = new List<object>();

            ToReturn.ListRowNames = new List<string>();
            ToReturn.ListTags = new List<object>();
            foreach (cWell item in this.ListActiveWells)
            {
                if (item == null) continue;

                ToReturn.ListRowNames.Add(item.GetShortInfo());
                ToReturn.ListTags.Add(item);

                ToReturn[0].Add(item.GetPosX());
                ToReturn[1].Add(item.GetPosY());
                ToReturn[0].ListTags.Add(item);
                ToReturn[1].ListTags.Add(item);

                for (int i = 2; i < ToReturn.Count - 1; i++)
                {
                    cDescriptorType DT = (cDescriptorType)ToReturn[i].Tag;
                    ToReturn[i].Add(item.ListSignatures.GetSignature(DT).GetValue());
                    ToReturn[i].ListTags.Add(item);
                }

                ToReturn[ToReturn.Count - 1].Add(item.GetCurrentClassIdx());
                ToReturn[ToReturn.Count - 1].ListTags.Add(item.GetClassType());
            }
            return ToReturn;
        }

        private void ToolStripMenuItem_DisplayDataTable(object sender, EventArgs e)
        {

            cExtendedTable ET = this.GetAverageValueTable(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), true);
            //ET = GetListValues(true);



            cDisplayExtendedTable DET = new cDisplayExtendedTable();



            DET.SetInputData(ET);
            DET.Run();
        }

        private void ToolStripMenuItem_PlateInfo(object sender, EventArgs e)
        {
            cDesignerSplitter DSV = new cDesignerSplitter();
            DSV.Orientation = Orientation.Vertical;

            cViewertext VT = new cViewertext();
            string InfoPlate = "NAME: " + this.Name + "\n\n";
            InfoPlate += "DIMENSIONS: " + ParentScreening.Columns + " x " + ParentScreening.Rows + " (" + ParentScreening.Rows * ParentScreening.Columns + ")\n\n";
            InfoPlate += "WELLS: " + this.ListWells.Count + " (" + this.ListActiveWells.Count + " actives)\n\n";

            InfoPlate += "PROPERTIES:\n\n";

            foreach (var item in this.ListProperties)
            {
                InfoPlate += " NAME: " + item.PropertyType.Name + "\n TYPE: " + item.PropertyType.Type + "\n VALUE: ";
                object ValueProp = item.GetValue();
                if (ValueProp == null)
                    InfoPlate += "NULL\n";
                else
                {
                    //if(ValueProp.GetType()==typeof(double))
                    InfoPlate += ValueProp.ToString() + "\n";
                    //else if(ValueProp.GetType()==typeof(string))

                }
                InfoPlate += " INFO: " + item.Info + "\n\n";
            }

            VT.SetInputData(InfoPlate);
            VT.Run();


            DSV.SetInputData(VT.GetOutPut());
            DSV.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(VT.GetOutPut());
            DTW.Title = this.Name + " (Info)";

            DTW.Run();
            DTW.Display();
        }

        private void ToolStripMenuItem_DisplayDataTableDistance(object sender, EventArgs e)
        {
            cExtendedTable ET = this.ListActiveWells.GetDistanceMatrix(eDistances.EUCLIDEAN);

            cDisplayExtendedTable DET = new cDisplayExtendedTable();
            DET.SetInputData(ET);
            DET.Run();
        }

        private void ToolStripMenuItem_SetAsActivePlate(object sender, EventArgs e)
        {
            int PosPlate = cGlobalInfo.WindowHCSAnalyzer.toolStripcomboBoxPlateList.FindStringExact(this.Name);
            cGlobalInfo.WindowHCSAnalyzer.toolStripcomboBoxPlateList.SelectedIndex = PosPlate;
            this.ParentScreening.CurrentDisplayPlateIdx = PosPlate;
            this.ParentScreening.GetCurrentDisplayPlate().DisplayDistribution(this.ParentScreening.ListDescriptors.GetActiveDescriptor(), false);
        }

        //private void ToolStripMenuItem_CopyClassToClipBoard(object sender, EventArgs e)
        //{
        //    CopyClassToClipBoard();
        //}

        private void ToolStripMenuItem_CopyPropertyToClipBoard(object sender, EventArgs e)
        {
            CopyPropertyToClipboard();

        }

        public cExtendedTable GetPropertyAsTable(cPropertyType PT)
        {
            cExtendedTable ToBeReturned = new cExtendedTable(ParentScreening.Columns, ParentScreening.Rows, 0);

            ToBeReturned.Name = PT.Name + " - " + this.GetName();
            ToBeReturned.ListRowNames = new List<string>();
            for (int j = 0; j < ParentScreening.Rows; j++)
            {
                byte[] strArray = new byte[1];
                strArray[0] = (byte)(j + 65);

                string Chara = Encoding.UTF7.GetString(strArray);
                ToBeReturned.ListRowNames.Add(Chara);
            }

            for (int i = 0; i < ParentScreening.Columns; i++)
            {
                ToBeReturned[i].ListTags = new List<object>();
                ToBeReturned[i].Name = (i + 1).ToString();

                for (int j = 0; j < ParentScreening.Rows; j++)
                {
                    cWell CurrentWell = ParentScreening.GetCurrentDisplayPlate().GetWell(i, j, false);
                    if (CurrentWell == null)
                        ToBeReturned[i].ListTags.Add(null);
                    else
                    {
                        object TmpOBj = CurrentWell.ListProperties.FindValueByName(PT.Name);
                        ToBeReturned[i].ListTags.Add(TmpOBj);
                    }
                }
            }


            return ToBeReturned;
        }

        public void CopyPropertyToClipboard()
        {
            cGUI_ListWellProperty GUI_ListWellProperty = new cGUI_ListWellProperty();
            GUI_ListWellProperty.IsCheckBoxes = false;
            if (GUI_ListWellProperty.Run().IsSucceed == false) return;
            List<cPropertyType> ListSelectedProp = GUI_ListWellProperty.GetOutPut();

            StringBuilder sb = new StringBuilder();

            sb.Append(String.Format(ListSelectedProp[0].Name + "\n"));
            sb.Append(String.Format(this.Name + "\t"));
            for (int i = 0; i < ParentScreening.Columns - 1; i++)
            {
                int IdxCol = i + 1;
                sb.Append(String.Format("{0}\t", IdxCol));
            }
            sb.Append(String.Format("{0}", ParentScreening.Columns));
            sb.AppendLine();

            for (int j = 0; j < ParentScreening.Rows; j++)
            {
                byte[] strArray = new byte[1];
                strArray[0] = (byte)(j + 65);

                string Chara = Encoding.UTF7.GetString(strArray);
                sb.Append(String.Format("{0}\t", Chara));

                for (int i = 0; i < ParentScreening.Columns - 1; i++)
                {
                    cWell CurrentWell = ParentScreening.GetCurrentDisplayPlate().GetWell(i, j, false);
                    if (CurrentWell == null)
                        sb.Append("\t");
                    else
                    {
                        object TmpOBj = CurrentWell.ListProperties.FindValueByName(ListSelectedProp[0].Name);
                        if (TmpOBj == null)
                            sb.Append("n.a.\t");
                        else
                            sb.Append(TmpOBj.ToString() + "\t");
                    }
                }

                cWell CurrentWellFinal = ParentScreening.GetCurrentDisplayPlate().GetWell(ParentScreening.Columns - 1, j, false);
                if (CurrentWellFinal == null)
                    sb.Append("\t");
                else
                {
                    object TmpOBj = CurrentWellFinal.ListProperties.FindValueByName(ListSelectedProp[0].Name);
                    if (TmpOBj == null)
                        sb.Append("n.a.\t");
                    else
                        sb.Append(TmpOBj.ToString() + "\t");
                }

                sb.AppendLine();
            }
            Clipboard.SetText(sb.ToString());

        }

        //public void CopyClassToClipBoard()
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append(String.Format(this.Name + "\t"));
        //    //sb.Append("\t");
        //    for (int i = 0; i < ParentScreening.Columns - 1; i++)
        //    {
        //        int IdxCol = i + 1;
        //        sb.Append(String.Format("{0}\t", IdxCol));
        //    }
        //    sb.Append(String.Format("{0}", ParentScreening.Columns));
        //    sb.AppendLine();

        //    for (int j = 0; j < ParentScreening.Rows; j++)
        //    {
        //        byte[] strArray = new byte[1];
        //        strArray[0] = (byte)(j + 65);

        //        string Chara = Encoding.UTF7.GetString(strArray);
        //        sb.Append(String.Format("{0}\t", Chara));

        //        for (int i = 0; i < ParentScreening.Columns - 1; i++)
        //        {
        //            cWell CurrentWell = ParentScreening.GetCurrentDisplayPlate().GetWell(i, j, false);
        //            if (CurrentWell == null)
        //                sb.Append("\t");
        //            else
        //                sb.Append(String.Format("{0}\t", CurrentWell.GetCurrentClassIdx()));
        //        }


        //        cWell CurrentWellFinal = ParentScreening.GetCurrentDisplayPlate().GetWell(ParentScreening.Columns - 1, j, false);
        //        if (CurrentWellFinal == null)
        //            sb.Append("\t");
        //        else
        //            sb.Append(String.Format("{0}\t", CurrentWellFinal.GetCurrentClassIdx()));


        //        //  sb.Append(String.Format("{0}", CompleteScreening.GetPlate(0).GetWell(CompleteScreening.Columns-1, j).ListDescriptors[(int)numericUpDownDescriptorIndex.Value].AverageValue));
        //        sb.AppendLine();
        //    }
        //    Clipboard.SetText(sb.ToString());

        //}

        public void CopyValuestoClipBoard()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(String.Format(ParentScreening.ListDescriptors[ParentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName() + "\t"));
            //sb.Append("\t");
            for (int i = 0; i < ParentScreening.Columns - 1; i++)
            {
                int IdxCol = i + 1;
                sb.Append(String.Format("{0}\t", IdxCol));
            }
            sb.Append(String.Format("{0}", ParentScreening.Columns));
            sb.AppendLine();

            for (int j = 0; j < ParentScreening.Rows; j++)
            {
                byte[] strArray = new byte[1];
                strArray[0] = (byte)(j + 65);

                string Chara = Encoding.UTF7.GetString(strArray);
                sb.Append(String.Format("{0}\t", Chara));

                for (int i = 0; i < ParentScreening.Columns - 1; i++)
                {
                    cWell CurrentWell = ParentScreening.GetCurrentDisplayPlate().GetWell(i, j, false);
                    if (CurrentWell == null)
                        sb.Append("\t");
                    else
                        sb.Append(String.Format("{0}\t", CurrentWell.ListSignatures[ParentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetValue()));
                }


                cWell CurrentWellFinal = ParentScreening.GetCurrentDisplayPlate().GetWell(ParentScreening.Columns - 1, j, false);
                if (CurrentWellFinal == null)
                    sb.Append("\t");
                else
                    sb.Append(String.Format("{0}\t", CurrentWellFinal.ListSignatures[ParentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetValue()));


                //  sb.Append(String.Format("{0}", CompleteScreening.GetPlate(0).GetWell(CompleteScreening.Columns-1, j).ListDescriptors[(int)numericUpDownDescriptorIndex.Value].AverageValue));
                sb.AppendLine();
            }



            Clipboard.SetText(sb.ToString());

        }

        private void ToolStripMenuItem_Clustering(object sender, EventArgs e)
        {
            List<cPlate> ListPlate = new List<cPlate>();
            ListPlate.Add(this);

            cGlobalInfo.WindowHCSAnalyzer.PerformScreeningClustering(ListPlate, true);
        }

        private void ToolStripMenuItem_DisplayAs2DScatterPoints(object sender, EventArgs e)
        {
            cListWells ListWellsToProcess = new cListWells(null);

            foreach (cWell item in this.ListActiveWells)
                if (item.GetCurrentClassIdx() != -1)
                    ListWellsToProcess.Add(item);

            cExtendedTable DataFromPlate = new cExtendedTable(ListWellsToProcess, true);
            DataFromPlate.Name = this.Name;

            cViewer2DScatterPoint V1D = new cViewer2DScatterPoint();
            V1D.Chart.IsSelectable = true;
            V1D.Chart.IsShadow = true;
            V1D.Chart.IsBorder = true;
            V1D.Chart.IsSelectable = true;
            V1D.Chart.CurrentTitle.Tag = this;
            V1D.SetInputData(DataFromPlate);
            cFeedBackMessage MessageReturned = V1D.Run();
            if (MessageReturned.IsSucceed == false)
            {
                MessageBox.Show(MessageReturned.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cDesignerSinglePanel Designer0 = new cDesignerSinglePanel();
            Designer0.SetInputData(V1D.GetOutPut());
            Designer0.Run();

            cDisplayToWindow Disp0 = new cDisplayToWindow();
            Disp0.SetInputData(Designer0.GetOutPut());
            Disp0.Title = "2D Scatter points graph - " + DataFromPlate[0].Count + " wells.";
            if (!Disp0.Run().IsSucceed) return;
            Disp0.Display();

        }

        private void ToolStripMenuItem_DisplayAsStackedHistograms(object sender, EventArgs e)
        {
            cDisplayToWindow CDW1 = new cDisplayToWindow();

            cListWells ListWellsToProcess = new cListWells(null);
            List<cPlate> PlateList = new List<cPlate>();

            PlateList.Add(this);

            foreach (cPlate TmpPlate in PlateList)
                foreach (cWell item in TmpPlate.ListActiveWells)
                    if (item.GetCurrentClassIdx() != -1) ListWellsToProcess.Add(item);


            CDW1.Title = this.ParentScreening.ListDescriptors[this.ParentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName() + " - Stacked Histogram (" + PlateList[0].Name + ")";

            cExtendedTable NewTable = ListWellsToProcess.GetAverageDescriptorValues(this.ParentScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
            NewTable.Name = CDW1.Title;

            cViewerStackedHistogram CV1 = new cViewerStackedHistogram();
            CV1.SetInputData(NewTable);
            CV1.Chart.LabelAxisX = this.ParentScreening.ListDescriptors[this.ParentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
            CV1.Run();

            CDW1.SetInputData(CV1.GetOutPut());

            CDW1.Run();
            CDW1.Display();
        }

        private void ToolStripMenuItem_DisplayImages(object sender, EventArgs e)
        {

            //if (cWell.cGlobalInfo.ImageAccessor.ImagingPlatformType == HCSAnalyzer.Classes.General_Types.eImagingPlatformType.OPERETTA) return;

            cGlobalInfo.ImageAccessor.Field = 1;// (int)this.numericUpDownField.Value;
            //string FileName = cWell.cGlobalInfo.ImageAccessor.GetImageFileName(this);


            foreach (cWell item in this.ListActiveWells)
            {

                List<cImageMetaInfo> ListMetaInfo = cGlobalInfo.ImageAccessor.GetImageInfo(item);

                cImage Image = new cImage(ListMetaInfo);
                Image.Name = "Image [" + item.GetShortInfo() + "] - Field [" + cGlobalInfo.ImageAccessor.Field + "]";


                cImage FinaleImage = Image.Crop(new cPoint3D(400, 400, 0), new cPoint3D(800 + 400, 800 + 400, 0));

                //cImageGeometricResize IR = new cImageGeometricResize();
                //IR.SetInputData(Image);
                //IR.InterpolationType = Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR;
                //IR.ListProperties.UpdateValueByName("Scale", (double)0.25);
                //IR.Run();

                //cImage FinaleImage = IR.GetOutPut();
                //FinaleImage.Name = "Image [" + item.GetShortInfo()+"] - Field ["+ cWell.cGlobalInfo.ImageAccessor.Field + "]";

                cDisplaySingleImage IV = new cDisplaySingleImage();
                IV.SetInputData(FinaleImage);
                IV.Run();

            }



            //cImage IM = new cImage(this.Info);
            //if (IM == null) return;

            //cImageViewer IV = new cImageViewer();
            //IV.SetImage(IM);
            //IV.Show();

            //cViewerImage3D VI3D = new cViewerImage3D();

            //foreach (cWell item in this.ListActiveWells)
            //{
            //    vtkJPEGReader JPEGReader = vtkJPEGReader.New();
            //    JPEGReader.SetFileName(item.Info);
            //    JPEGReader.Update();
            //    vtkImageData ID0 = JPEGReader.GetOutput();
            //    VI3D.SetInputData(ID0);
            //}



            ////    cVolume3D Volume3D0 = new cVolume3D(ID0, new HCSAnalyzer.Classes._3D.cPoint3D(0, 0, 0));



            //VI3D.Run();

            //cDisplayToWindow DTW = new cDisplayToWindow();
            //DTW.SetInputData(VI3D.GetOutPut());
            //DTW.Run();
            //DTW.Display();

        }
    }
}
