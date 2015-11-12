using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Windows.Forms;
using HCSAnalyzer;
using HCSAnalyzer.Forms;
using HCSAnalyzer.Classes;
using weka.core;
using System.Runtime.InteropServices;
using System.Xml;
using System.Collections;
using System.Data.SqlClient;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.General_Types;
using System.Data;
using System.Net;
using ImageAnalysis;
using Kitware.VTK;
using HCSAnalyzer.Classes.MetaComponents;
using ImageAnalysisFiltering;
using HCSAnalyzer.Classes.Base_Classes.Viewers._2D;

namespace LibPlateAnalysis
{
    public class cWell : cGeneralClassWithContextMenu
    {

        public Image Thumbnail = null;

        public class cxWellClass
        {
            public int ClassIdx;
            public double Confidence;

            public cxWellClass(int Class, double Confidence)
            {
                this.ClassIdx = Class;
                this.Confidence = Confidence;
            }
        }

        public ChartArea CurrentChartArea;

        public void SetNewColor(Color C)
        {
            if(this.CurrentChartArea!=null)
                this.CurrentChartArea.BackColor = C;
        }

        private int NumBiologicalObjects = 0;

        public int GetNumBiologicalObjects()
        {
            return this.NumBiologicalObjects;
        }

        public void UpdateNumberOfBiologicalObjects()
        {
            // this.AssociatedPlate.DBConnection.OpenConnection();
            this.NumBiologicalObjects = this.AssociatedPlate.DBConnection.GetNumberOfRows(this);
            // this.AssociatedPlate.DBConnection.CloseConnection();
        }

        private int PosX = -1;
        private int PosY = -1;
        public cListSignature ListSignatures;
        public cListSignature ListPlateBasedDescriptors;
        private PlateChart AssociatedChart;

        private int CurrentDescriptorToDisplay;

        // that's a list for the history
        List<cxWellClass> ListClass = new List<cxWellClass>();

        public string SQLTableName = "";
       // public string Name = "";
        public string Info = "";

       // public static cScreening Parent;
        public cPlate AssociatedPlate;

        FormForPathway ListP = new FormForPathway();

        // static IList images = null;
        // int InitialClass = 2;

        public cListWellProperty ListProperties = null;

        //int CurrentClass = 2;

        //public double ClassificationConfidence { get; private set; }

        public double DistToBorder()
        {
            double MinDist = this.GetPosX() + 1;
            double DistToRight = cGlobalInfo.CurrentScreening.Columns - this.GetPosX();
            if (DistToRight < MinDist) MinDist = DistToRight;
            double DistToTop = this.GetPosY() + 1;
            if (DistToTop < MinDist) MinDist = DistToTop;
            double DistToBottom = cGlobalInfo.CurrentScreening.Rows - this.GetPosY();
            if (DistToBottom < MinDist) MinDist = DistToBottom;

            return MinDist;
        }

        public double DistToCenter()
        {
            double X_Center = cGlobalInfo.CurrentScreening.Columns / 2;
            double Y_Center = cGlobalInfo.CurrentScreening.Rows / 2;

            double DistToCenter = Math.Sqrt((this.GetPosX() - X_Center) * (this.GetPosX() - X_Center) + (this.GetPosY() - Y_Center) * (this.GetPosY() - Y_Center));

            return DistToCenter;
        }

        public void CleanClassHistory()
        {
            // remove evrything but the first
            //int History = this.ListClass.Count - 1;
            //this.ListClass.RemoveRange(1, History);
            this.ListClass.Clear();
        }

        public void SetClass(int Class)
        {
            SetClass(Class, 1);
        }

        public void SetClass(int Class, double ConfidenceValue)
        {
            //this.CurrentClass = Class;

            this.ListProperties.UpdateValueByName("Well Class", Class);

            //this.ClassificationConfidence = ConfidenceValue;
            this.ListProperties.UpdateValueByName("Classification Confidence", ConfidenceValue);

            if (AssociatedChart == null) return;
            if (Class == -1)
            {
                SetAsNoneSelected();
                return;
            }

            AssociatedChart.BackColor = cGlobalInfo.ListWellClasses[Class].ColourForDisplay;//CurrentColor;
            AssociatedChart.Update();
        }

        public void SaveCurrentClassStatus()
        {
            object O = this.ListProperties.FindValueByName("Well Class");
            if (O == null) return;

            object Oc = this.ListProperties.FindValueByName("Classification Confidence");
            if (Oc == null) Oc = (double)0;

            this.ListClass.Add(new cxWellClass((int)O, (double)Oc));
        }

        public double GetClassificationConfidence()
        {
            //if (cGlobalInfo.CurrentScreening.CurrentHistoryClassStatusIndex >= this.ListClass.Count)
            //    cGlobalInfo.CurrentScreening.CurrentHistoryClassStatusIndex = this.ListClass.Count - 1;
            //return this.ListClass[cGlobalInfo.CurrentScreening.CurrentHistoryClassStatusIndex];
            object O = this.ListProperties.FindValueByName("Classification Confidence");
            if (O == null) return 0;
            return (double)O;

            //return this.CurrentClass;
        }

        public int GetCurrentClassIdx()
        {
            //if (cGlobalInfo.CurrentScreening.CurrentHistoryClassStatusIndex >= this.ListClass.Count)
            //    cGlobalInfo.CurrentScreening.CurrentHistoryClassStatusIndex = this.ListClass.Count - 1;
            //return this.ListClass[cGlobalInfo.CurrentScreening.CurrentHistoryClassStatusIndex];
            object O = this.ListProperties.FindValueByName("Well Class");
            if (O == null) return -1;
            return (int)O;


            //return this.CurrentClass;
        }

        public int GetHistoricalClass(int Idx)
        {
            if (Idx >= this.ListClass.Count) return -2;
            return this.ListClass[Idx].ClassIdx;
        }

        public void SetAsNoneSelected()
        {
            //  SetClass(-1);
            //Class = -1;
            if (cGlobalInfo.CurrentScreening.CurrentHistoryClassStatusIndex < this.ListClass.Count)
                this.ListClass[cGlobalInfo.CurrentScreening.CurrentHistoryClassStatusIndex] = new cxWellClass(-1, 1);
            this.AssociatedPlate.ListActiveWells.Remove(this);

            this.ListProperties.UpdateValueByName("Well Class", null);

            // StateForClassif = "Unselected (-1)";
            //CurrentColor = cGlobalInfo.panelForPlate.BackColor;
            if (AssociatedChart == null) return;
            AssociatedChart.BackColor = cGlobalInfo.panelForPlate.BackColor;//CurrentColor;
            AssociatedChart.Update();
        }

        //private Color CurrentColor;
        // private string CurrentSelectedPathway = "";

        // public double Concentration = 0;
        //  public double LocusID = -1;

        public int GetLocusID()
        {
            object O = this.ListProperties.FindValueByName("Locus ID");
            if (O == null) return -1;
            return (int)O;
        }

        public void SetLocusID(int ID)
        {
            this.ListProperties.UpdateValueByName("Locus ID", ID);
        }

        public int GetGroupIdx()
        {
            object O = this.ListProperties.FindValueByName("Group");
            if (O == null) return -1;
            return (int)O;
        }

        public void SetGroupIdx(int Idx)
        {
            this.ListProperties.UpdateValueByName("Group", Idx);
        }

        #region Constructors

        public cWell(cSignature Desc, int Col, int Row, cScreening screenParent, cPlate CurrentPlate)
        {
            //Parent = screenParent;
            this.AssociatedPlate = CurrentPlate;

            this.ListSignatures = new cListSignature();

            this.ListSignatures.Add(Desc);

            this.PosX = Col;
            this.PosY = Row;

            //   this.CurrentColor = this.cGlobalInfo.ListClasses[ClassForClassif].ColourForDisplay;
            Desc.AssociatedWell = this;


            // this.ListClass.Add(this.InitialClass);
            this.ListProperties = new cListWellProperty(this);
            this.ListProperties.UpdateValueByName("Well Class", 2);
        }

        public cWell(cListSignature ListDesc, int Col, int Row, cScreening screenParent, cPlate CurrentPlate)
        {
           // Parent = screenParent;
            this.AssociatedPlate = CurrentPlate;

            this.ListSignatures = new cListSignature();

            this.ListSignatures = ListDesc;

            this.PosX = Col;
            this.PosY = Row;

            //   this.CurrentColor = this.cGlobalInfo.ListClasses[ClassForClassif].ColourForDisplay;

            foreach (var item in this.ListSignatures)
                item.AssociatedWell = this;

            //this.ListClass.Add(this.InitialClass);

            this.ListProperties = new cListWellProperty(this);
            this.ListProperties.UpdateValueByName("Well Class", 2);
            this.ListProperties.UpdateValueByName("Plate Name", CurrentPlate.GetName());
        }

        public cWell(string FileName, cScreening screenParent, cPlate CurrentPlate)
        {
           // Parent = screenParent;
            this.AssociatedPlate = CurrentPlate;

            StreamReader sr = new StreamReader(FileName);
            int Idx;
            string NewLine;
            string TmpLine;
            string line;

            // we have to build the descriptor list
            if (cGlobalInfo.CurrentScreening.ListDescriptors.Count == 0)
            {
                Idx = FileName.LastIndexOf("\\");
                NewLine = FileName.Remove(0, Idx + 1);
                TmpLine = NewLine;

                Idx = TmpLine.IndexOf("x");
                NewLine = TmpLine.Remove(Idx);

                if (!int.TryParse(NewLine, out this.PosX))
                {
                    MessageBox.Show("Error in load the current file.\n", "Loading error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sr.Close();
                    return;
                }

                NewLine = TmpLine.Remove(0, Idx + 1);
                Idx = NewLine.IndexOf(".");
                TmpLine = NewLine.Remove(Idx);

                this.PosY = Convert.ToInt16(TmpLine);

                line = sr.ReadLine();
                while (line != null)
                {
                    if (line != null)
                    {
                        Idx = line.IndexOf("\t");
                        string DescName = line.Remove(Idx);

                        List<double> readData = new List<double>();

                        NewLine = line.Remove(0, Idx + 1);
                        line = NewLine;

                        Idx = line.IndexOf("\t");
                        int NumValue = 0;
                        while (Idx > 0)
                        {
                            string DescValue = line.Remove(Idx);
                            double CurrentValue = Convert.ToDouble(DescValue);

                            readData.Add(CurrentValue);
                            NewLine = line.Remove(0, Idx + 1);
                            line = NewLine;
                            Idx = line.IndexOf("\t");
                            NumValue++;
                        }
                        if (line.Length > 0)
                        {
                            double Value = Convert.ToDouble(line);
                            readData.Add(Value);
                        }
                        // first check if the descriptor exist
                        cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(new cDescriptorType(DescName, true, NumValue));

                    }
                    line = sr.ReadLine();
                }
                sr.Close();
            }

            this.ListSignatures = new cListSignature();
            sr = new StreamReader(FileName);

            Idx = FileName.LastIndexOf("\\");
            NewLine = FileName.Remove(0, Idx + 1);
            TmpLine = NewLine;

            Idx = TmpLine.IndexOf("x");
            NewLine = TmpLine.Remove(Idx);
            this.PosX = Convert.ToInt16(NewLine);

            NewLine = TmpLine.Remove(0, Idx + 1);
            Idx = NewLine.IndexOf(".");
            TmpLine = NewLine.Remove(Idx);

            this.PosY = Convert.ToInt16(TmpLine);

            line = sr.ReadLine();
            int IDxLine = 0;
            while (line != null)
            {
                if (line != null)
                {
                    Idx = line.IndexOf("\t");
                    string DescName = line.Remove(Idx);
                    cExtendedList readData = new cExtendedList();

                    NewLine = line.Remove(0, Idx + 1);
                    line = NewLine;

                    Idx = line.IndexOf("\t");

                    while (Idx > 0)
                    {
                        string DescValue = line.Remove(Idx);
                        double CurrentValue = Convert.ToDouble(DescValue);

                        readData.Add(CurrentValue);
                        NewLine = line.Remove(0, Idx + 1);
                        line = NewLine;
                        Idx = line.IndexOf("\t");
                    }
                    if (line.Length > 0)
                    {
                        double Value = Convert.ToDouble(line);
                        readData.Add(Value);
                    }
                    cSignature CurrentDesc = new cSignature(readData, cGlobalInfo.CurrentScreening.ListDescriptors[IDxLine].GetBinNumber() - 1, cGlobalInfo.CurrentScreening.ListDescriptors[IDxLine], cGlobalInfo.CurrentScreening);
                    this.ListSignatures.Add(CurrentDesc);
                }
                line = sr.ReadLine();
                IDxLine++;
            }
            sr.Close();


            // this.ListClass.Add(this.InitialClass);


            this.ListProperties = new cListWellProperty(this);
            this.ListProperties.UpdateValueByName("Well Class", 2);
            //    this.CurrentColor = this.cGlobalInfo.ListClasses[ClassForClassif].ColourForDisplay;
            return;
        }
        #endregion

        #region inter wells distance
        public double DistanceTo(cWell DestinationWell, int Idxdescriptor, eDistances DistanceType)
        {
            double Distance = 0;
            switch (DistanceType)
            {
                case eDistances.EUCLIDEAN:
                    Distance = this.ListSignatures[Idxdescriptor].GetHistovalues().Dist_Euclidean(DestinationWell.ListSignatures[Idxdescriptor].GetHistovalues());
                    break;
                case eDistances.MANHATTAN:
                    Distance = this.ListSignatures[Idxdescriptor].GetHistovalues().Dist_Manhattan(DestinationWell.ListSignatures[Idxdescriptor].GetHistovalues());
                    break;
                case eDistances.VECTOR_COS:
                    Distance = this.ListSignatures[Idxdescriptor].GetHistovalues().Dist_VectorCosine(DestinationWell.ListSignatures[Idxdescriptor].GetHistovalues());
                    break;
                case eDistances.BHATTACHARYYA:
                    Distance = this.ListSignatures[Idxdescriptor].GetHistovalues().Dist_BhattacharyyaCoefficient(DestinationWell.ListSignatures[Idxdescriptor].GetHistovalues());
                    break;
                case eDistances.EMD:
                    Distance = this.ListSignatures[Idxdescriptor].GetHistovalues().Dist_EarthMover(DestinationWell.ListSignatures[Idxdescriptor].GetHistovalues());
                    break;
                default:
                    break;
            }
            return Distance;
        }

        public double DistanceTo(cWell DestinationWell, eDistances DistanceType)
        {
            double Distance = 0;

            switch (DistanceType)
            {
                case eDistances.EUCLIDEAN:
                    Distance += this.GetAverageValuesList(true)[0].Dist_Euclidean(DestinationWell.GetAverageValuesList(true)[0]);
                    break;
                case eDistances.MANHATTAN:
                    Distance += this.GetAverageValuesList(true)[0].Dist_Manhattan(DestinationWell.GetAverageValuesList(true)[0]);
                    break;
                case eDistances.VECTOR_COS:
                    Distance += this.GetAverageValuesList(true)[0].Dist_VectorCosine(DestinationWell.GetAverageValuesList(true)[0]);
                    break;
                case eDistances.BHATTACHARYYA:
                    Distance += this.GetAverageValuesList(true)[0].Dist_BhattacharyyaCoefficient(DestinationWell.GetAverageValuesList(true)[0]);
                    break;
                case eDistances.EMD:
                    Distance += this.GetAverageValuesList(true)[0].Dist_EarthMover(DestinationWell.GetAverageValuesList(true)[0]);
                    break;
                default:
                    break;
            }

            return Distance;
        }

        public double DistanceTo(cWell DestinationWell, eDistances IntraHistoDistanceType, eDistances InterHistoDistanceType)
        {
            double Distance = 0;

            cExtendedList ListDistSource = new cExtendedList();
            cExtendedList ListDistDest = new cExtendedList();

            for (int Idxdescriptor = 0; Idxdescriptor < cGlobalInfo.CurrentScreening.ListDescriptors.Count; Idxdescriptor++)
            {
                if (cGlobalInfo.CurrentScreening.ListDescriptors[Idxdescriptor].IsActive() == false) continue;


                switch (IntraHistoDistanceType)
                {
                    case eDistances.EUCLIDEAN:
                        ListDistSource.Add(this.ListSignatures[Idxdescriptor].GetHistovalues().Dist_Euclidean(DestinationWell.ListSignatures[Idxdescriptor].GetHistovalues()));
                        ListDistDest.Add(DestinationWell.ListSignatures[Idxdescriptor].GetValue());


                        Distance = ListDistSource.Dist_Euclidean(ListDistDest);
                        break;
                    case eDistances.MANHATTAN:
                        Distance = ListDistSource.Dist_Manhattan(ListDistDest);
                        break;
                    case eDistances.VECTOR_COS:
                        Distance = ListDistSource.Dist_VectorCosine(ListDistDest);
                        break;
                    case eDistances.BHATTACHARYYA:
                        Distance = ListDistSource.Dist_BhattacharyyaCoefficient(ListDistDest);
                        break;
                    case eDistances.EMD:
                        Distance = ListDistSource.Dist_EarthMover(ListDistDest);
                        break;
                    default:
                        break;
                }





            }



            switch (InterHistoDistanceType)
            {
                case eDistances.EUCLIDEAN:
                    Distance = ListDistSource.Dist_Euclidean(ListDistDest);
                    break;
                case eDistances.MANHATTAN:
                    Distance = ListDistSource.Dist_Manhattan(ListDistDest);
                    break;
                case eDistances.VECTOR_COS:
                    Distance = ListDistSource.Dist_VectorCosine(ListDistDest);
                    break;
                case eDistances.BHATTACHARYYA:
                    Distance = ListDistSource.Dist_BhattacharyyaCoefficient(ListDistDest);
                    break;
                case eDistances.EMD:
                    Distance = ListDistSource.Dist_EarthMover(ListDistDest);
                    break;
                default:
                    break;
            }

            return Distance;
        }
        #endregion

        /// <summary>
        /// Get the class index related to the well
        /// </summary>
        /// <returns>the class index</returns>

        /// <summary>
        /// Return the color of the well (related to the class or the selection mode)
        /// </summary>
        /// <returns>The color</returns>
        public Color GetClassColor()
        {
            if ((this.GetCurrentClassIdx() == -1) || (this.GetCurrentClassIdx()>=cGlobalInfo.ListWellClasses.Count))
            {
                return Color.FromArgb(14, 35, 61);
            }

            return cGlobalInfo.ListWellClasses[this.GetCurrentClassIdx()].ColourForDisplay;
        }

        public cWellClassType GetClassType()
        {
            if (this.GetCurrentClassIdx() == -1) return null;
            return cGlobalInfo.ListWellClasses[this.GetCurrentClassIdx()];
        }

        public string GetClassName()
        {
            if (this.GetCurrentClassIdx() == -1) return "Inactive";
            return cGlobalInfo.ListWellClasses[this.GetCurrentClassIdx()].Name;
        }

        public string GetShortInfo()
        {
            base.ShortInfo = this.AssociatedPlate.GetShortInfo();
            base.ShortInfo += " - Well [" + this.GetPos() + "] ";

            foreach (cPropertyType item in cGlobalInfo.CurrentScreening.ListWellPropertyTypes)
            {
                if (!item.IsTobeDisplayed) continue;
                cProperty P = this.ListProperties.FindPropertyByName(item.Name);
                if (P == null) continue;

                string V = "n.a.";
                if (P.GetValue() != null)
                   V = P.GetValue().ToString();

                base.ShortInfo += " - " + item.Name + " [" + V + "]";
            }

            return base.ShortInfo;
        }

        public string GetCpdName()
        {
            object O = this.ListProperties.FindValueByName("Compound Name");
            if (O == null) return "n.a.";
            return (string)O;
        }

        public void SetCpdName(string Name)
        {
            this.ListProperties.UpdateValueByName("Compound Name", Name);
        }

        public cExtendedTable GetAverageValuesList(bool IsOnlySelectedDescriptors)
        {
            cExtendedTable ValuesToReturn = new cExtendedTable();
            ValuesToReturn.Name = this.GetShortInfo();
            ValuesToReturn.ListRowNames = new List<string>();
            ValuesToReturn.Tag = this;
            ValuesToReturn.Add(new cExtendedList(this.GetShortInfo() + " - Average Values"));

            if (IsOnlySelectedDescriptors)
            {
                for (int i = 0; i < ListSignatures.Count; i++)
                {
                    if (cGlobalInfo.CurrentScreening.ListDescriptors[i].IsActive())
                    {
                        ValuesToReturn.ListRowNames.Add(cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName());
                        ValuesToReturn[0].Add(ListSignatures[i].GetValue());
                    }
                }
            }
            else
            {
                for (int i = 0; i < ListSignatures.Count; i++)
                {
                    ValuesToReturn.ListRowNames.Add(cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName());
                    ValuesToReturn[0].Add(ListSignatures[i].GetValue());
                }
            }
            return ValuesToReturn;
        }

        public cExtendedList GetAverageValuesList(cExtendedList ListIdxDescriptors)
        {
            cExtendedList ValuesToReturn = new cExtendedList();

            foreach (var item in ListIdxDescriptors)
            {
                ValuesToReturn.Add(ListSignatures[(int)item].GetValue());
            }
            return ValuesToReturn;
        }

        public double GetAverageValue(cDescriptorType DescriptorType)
        {
            return ListSignatures.GetSignature(DescriptorType).GetValue();
        }

        public cExtendedTable GetValuesList(cDescriptorType DescriptorType)
        {
            this.AssociatedPlate.DBConnection = new cDBConnection(this.AssociatedPlate, this.SQLTableName);

            cListSingleBiologicalObjects ListPhenotypes =
                this.AssociatedPlate.DBConnection.GetBiologicalPhenotypes(this);

            cExtendedTable ET = this.AssociatedPlate.DBConnection.GetWellValues(this, DescriptorType);

            this.AssociatedPlate.DBConnection.CloseConnection();

            return ET;
        }

        public cExtendedTable GetValuesList(List<cDescriptorType> ListDescriptorTypes)
        {
            this.AssociatedPlate.DBConnection = new cDBConnection(this.AssociatedPlate, this.SQLTableName);

            cListSingleBiologicalObjects ListPhenotypes =
                this.AssociatedPlate.DBConnection.GetBiologicalPhenotypes(this);

            cExtendedTable ET = this.AssociatedPlate.DBConnection.GetWellValues(this, ListDescriptorTypes);

            ET.ListTags = new List<object>();
            foreach (var item in ListPhenotypes)
                ET.ListTags.Add(item);

            this.AssociatedPlate.DBConnection.CloseConnection();

            return ET;
        }

        public cExtendedTable GetValuesList(List<cDescriptorType> ListDescriptorTypes, List<cCellularPhenotype> ListPhenotypesSelected, bool IsIncludeClass)
        {
            if (IsIncludeClass)
            {
            this.AssociatedPlate.DBConnection = new cDBConnection(this.AssociatedPlate, this.SQLTableName);

            cExtendedTable ET = this.AssociatedPlate.DBConnection.GetWellValues(this, ListDescriptorTypes, ListPhenotypesSelected);
            cExtendedTable ET2 = this.AssociatedPlate.DBConnection.GetWellPhenotypeId(this,ListPhenotypesSelected);

            ET.Add(new cExtendedList("Phenotype"));

            ET[1].AddRange(ET2[0]);

            this.AssociatedPlate.DBConnection.CloseConnection();

                return ET;

            }
            else
                return GetValuesList(ListDescriptorTypes, ListPhenotypesSelected);
        }


        public cExtendedTable GetValuesList(List<cDescriptorType> ListDescriptorTypes, List<cCellularPhenotype> ListPhenotypesSelected)
        {
            this.AssociatedPlate.DBConnection = new cDBConnection(this.AssociatedPlate, this.SQLTableName);

            //cListSingleBiologicalObjects ListPhenotypes =
            //    this.AssociatedPlate.DBConnection.GetBiologicalPhenotypes(this);

            cExtendedTable ET = this.AssociatedPlate.DBConnection.GetWellValues(this, ListDescriptorTypes, ListPhenotypesSelected);

            //ET.ListTags = new List<object>();
            //foreach (var item in cGlobalInfo.ListCellularPhenotypes)
            //    ET.ListTags.Add(item);

            this.AssociatedPlate.DBConnection.CloseConnection();

            return ET;
        }

        public int GetPosX()
        {
            return this.PosX;
        }

        public int GetPosY()
        {
            return this.PosY;
        }

        public string GetPos()
        {
            string ToReturn = "";

            char PosY = (char)((GetPosY() - 1) + (int)'a');
            ToReturn += PosY.ToString().ToUpper();
            ToReturn += (GetPosX()).ToString("D2");
            return ToReturn;
        }

        public cExtendedControl BuildChartForImage()
        {
           // Panel TmpPanel = new Panel();
            int Field = 0;

            cViewerImage MyImageViewer = new cViewerImage();

            cGetImageFromWells GIFW = new cGetImageFromWells();
            GIFW.SetInputData(new cListWells(this));
            if(!GIFW.Run().IsSucceed) return null;

            MyImageViewer.SetInputData(GIFW.GetOutPut());

            MyImageViewer.Run();

            


            int GutterSize = (int)cGlobalInfo.OptionsWindow.FFAllOptions.numericUpDownGutter.Value;

            MyImageViewer.GetOutPut().Location = new System.Drawing.Point(
                                        (int)((PosX - 1) * (cGlobalInfo.SizeHistoWidth + GutterSize) + cGlobalInfo.SizeHistoWidth),
                                        (int)((PosY - 1) * (cGlobalInfo.SizeHistoHeight + GutterSize) + cGlobalInfo.SizeHistoHeight));
            MyImageViewer.GetOutPut().BackColor = this.GetClassColor();//cGlobalInfo.ListClasses[this.ClassForClassif].ColourForDisplay;// CurrentColor;
            MyImageViewer.GetOutPut().Width = (int)cGlobalInfo.SizeHistoWidth;
            MyImageViewer.GetOutPut().Height = (int)cGlobalInfo.SizeHistoHeight;


            //PictureBox PB = new PictureBox();
            //PB.Width = (int)cGlobalInfo.SizeHistoWidth-4;
            //PB.Height = (int)cGlobalInfo.SizeHistoHeight-4;
            //PB.Location = new Point(2, 2);
            //PB.BackColor = this.GetClassColor();

            //PB.BorderStyle = BorderStyle.None;
            //PB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            
            //PB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AssociatedChart_MouseClick);
            //PB.MouseDown += new MouseEventHandler(AssociatedChart_MouseDown);
            //PB.AllowDrop = true;
            //PB.DragDrop += new DragEventHandler(AssociatedChart_DragDrop);
            //PB.DragEnter += new DragEventHandler(AssociatedChart_DragEnter);

            if (this.Thumbnail == null)
            {
                cGetImageFromWells GIFWT = new cGetImageFromWells();
                GIFWT.SetInputData(new cListWells(this));
                if (!GIFWT.Run().IsSucceed) return null;

                this.Thumbnail = GIFWT.GetOutPut().GetBitmap(1, null, null);
            }
          //  PB.Image = this.Thumbnail;

           // TmpPanel.Controls.Add(PB);


          
            //TmpPanel.
            //.GetToolTipText += new System.EventHandler<ToolTipEventArgs>(this.AssociatedChart_GetToolTipText);



            return MyImageViewer.GetOutPut();
        }

        public PlateChart BuildChartForClass()
        {
            if (AssociatedChart != null) AssociatedChart.Dispose();
            AssociatedChart = new PlateChart();


            Series CurrentSeries = new Series("ChartSeries" + PosX + "x" + PosY);
            ChartArea CurrentChartArea = new ChartArea("ChartArea" + PosX + "x" + PosY);

            CurrentChartArea.Axes[0].MajorGrid.Enabled = false;
            CurrentChartArea.Axes[0].LabelStyle.Enabled = false;

            CurrentChartArea.Axes[1].LabelStyle.Enabled = false;
            CurrentChartArea.Axes[1].MajorGrid.Enabled = false;
            CurrentChartArea.Axes[0].Enabled = AxisEnabled.False;
            CurrentChartArea.Axes[1].Enabled = AxisEnabled.False;

            CurrentChartArea.BackColor = this.GetClassColor();// cGlobalInfo.ListClasses[this.ClassForClassif].ColourForDisplay;// CurrentColor; //Color.FromArgb(LUT[0][ConvertedValue], LUT[1][ConvertedValue], LUT[2][ConvertedValue]);
            AssociatedChart.ChartAreas.Add(CurrentChartArea);
            //AssociatedChart.Location = new System.Drawing.Point((PosX - 1) * (cGlobalInfo.CurrentScreening.SizeHistoWidth + cGlobalInfo.CurrentScreening.GutterSize), (PosY - 1) * (cGlobalInfo.CurrentScreening.SizeHistoHeight + cGlobalInfo.CurrentScreening.GutterSize));
            //AssociatedChart.Series.Add(CurrentSeries);

            int GutterSize = (int)cGlobalInfo.OptionsWindow.FFAllOptions.numericUpDownGutter.Value;

            AssociatedChart.Location = new System.Drawing.Point(
                                        (int)((PosX - 1) * (cGlobalInfo.SizeHistoWidth + GutterSize) + cGlobalInfo.SizeHistoWidth),
                                        (int)((PosY - 1) * (cGlobalInfo.SizeHistoHeight + GutterSize) + cGlobalInfo.SizeHistoHeight));
            AssociatedChart.Series.Add(CurrentSeries);

            AssociatedChart.BackColor = this.GetClassColor();//cGlobalInfo.ListClasses[this.ClassForClassif].ColourForDisplay;// CurrentColor;
            AssociatedChart.Width = (int)cGlobalInfo.SizeHistoWidth;
            AssociatedChart.Height = (int)cGlobalInfo.SizeHistoHeight;

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

                if (WellPropertyType != null)
                {
                    object CurrentValue = this.ListProperties.FindValueByName(WellPropertyType.Name);
                    if (CurrentValue != null)
                    {

                        if (WellPropertyType.Name == "Well Class")
                        {
                            if((int)CurrentValue==-1)
                            MainLegend.Text = "n.a.";
                            else
                            MainLegend.Text = cGlobalInfo.ListWellClasses[(int)CurrentValue].Name;
                        }
                        else
                            MainLegend.Text = CurrentValue.ToString();
                    }
                    else
                        MainLegend.Text = "n.a.";

                }
                MainLegend.Docking = Docking.Bottom;
                MainLegend.Font = new System.Drawing.Font("Arial", cGlobalInfo.SizeHistoWidth / 10 + 1, FontStyle.Regular);
                MainLegend.BackColor = MainLegend.BackImageTransparentColor;
                MainLegend.ForeColor = cGlobalInfo.OptionsWindow.FFAllOptions.panelFontColor.BackColor; 
                AssociatedChart.Titles.Add(MainLegend);
            }

           // AssociatedChart.Update();
           // AssociatedChart.Show();
            AssociatedChart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AssociatedChart_MouseClick);
            AssociatedChart.MouseDown += new MouseEventHandler(AssociatedChart_MouseDown);

            AssociatedChart.DragDrop += new DragEventHandler(AssociatedChart_DragDrop);
            AssociatedChart.DragEnter += new DragEventHandler(AssociatedChart_DragEnter);
            AssociatedChart.AllowDrop = true;

            AssociatedChart.GetToolTipText += new System.EventHandler<ToolTipEventArgs>(this.AssociatedChart_GetToolTipText);

            return AssociatedChart;
        }

        public PlateChart BuildChart(int IdxDescriptor, double[] MinMax)
        {
            if (cGlobalInfo.IsDisplayClassOnly) return BuildChartForClass();

            cPropertyType WellPropertyType = null;

            ToolStripMenuItem CheckedMenuItem = null;

            foreach (object item in cGlobalInfo.WindowHCSAnalyzer.toolStripDropDownButtonDisplayMode.DropDownItems)
            {

                if (item.GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem TmpMenuItem = ((ToolStripMenuItem)item);

                    if (TmpMenuItem.Checked)
                    {
                        CheckedMenuItem = TmpMenuItem;
                        if ((TmpMenuItem.Tag != null) && (TmpMenuItem.Tag.GetType() == typeof(cPropertyType)))
                        {
                            WellPropertyType = ((cPropertyType)(TmpMenuItem).Tag);
                            break;
                        }
                    }
                }
            }

            int borderSize = 8;
            int GutterSize = (int)cGlobalInfo.OptionsWindow.FFAllOptions.numericUpDownGutter.Value;

            if (AssociatedChart != null) AssociatedChart.Dispose();
            if (IdxDescriptor >= ListSignatures.Count) return null;
            CurrentDescriptorToDisplay = IdxDescriptor;
            AssociatedChart = new PlateChart();
            Series CurrentSeries = new Series("ChartSeries" + PosX + "x" + PosY);
            CurrentChartArea = new ChartArea("ChartArea" + PosX + "x" + PosY);

            CurrentChartArea.Axes[0].MajorGrid.Enabled = false;
            CurrentChartArea.Axes[0].LabelStyle.Enabled = false;

            #region Pie
            if (cGlobalInfo.ViewMode == eViewMode.PIE)
            {
                CurrentSeries.ChartType = SeriesChartType.Pie;
                int RealIdx = 0;

                for (int IdxValue = 0; IdxValue < ListSignatures.Count; IdxValue++)
                {
                    if (ListSignatures[IdxValue].GetAssociatedType().IsActive())
                    {
                        CurrentSeries.Points.Add(ListSignatures[IdxValue].GetValue());
                        CurrentSeries.Points[CurrentSeries.Points.Count - 1].Color = cGlobalInfo.ListCellularPhenotypes[RealIdx++ % cGlobalInfo.ListCellularPhenotypes.Count].ColourForDisplay;
                    }
                }
                AssociatedChart.ChartAreas.Add(CurrentChartArea);
            }
            #endregion

            #region Average
            else if ((ListSignatures[IdxDescriptor].GetAssociatedType().GetBinNumber() == 1) || (cGlobalInfo.ViewMode == eViewMode.AVERAGE))
            {
                CurrentChartArea.Axes[1].LabelStyle.Enabled = false;
                CurrentChartArea.Axes[1].MajorGrid.Enabled = false;
                CurrentChartArea.Axes[0].Enabled = AxisEnabled.False;
                CurrentChartArea.Axes[1].Enabled = AxisEnabled.False;

                //int ConvertedValue;

                //byte[][] LUT = cGlobalInfo.LUT;

                //if (MinMax[0] == MinMax[1])
                //    ConvertedValue = 0;
                //else
                //    ConvertedValue = (int)(((ListSignatures[IdxDescriptor].GetValue() - MinMax[0]) * (LUT[0].Length - 1)) / (MinMax[1] - MinMax[0]));

                //if ((ConvertedValue >= 0) && (ConvertedValue < LUT[0].Length))
                //    CurrentChartArea.BackColor = Color.FromArgb(LUT[0][ConvertedValue], LUT[1][ConvertedValue], LUT[2][ConvertedValue]);

                AssociatedChart.ChartAreas.Add(CurrentChartArea);
            }
            #endregion

            #region Histogram
            else
            {
                CurrentSeries.ChartType = SeriesChartType.Column;

                int HistoSize = ListSignatures[IdxDescriptor].GetHistovalues().Count;
                for (int IdxValue = 0; IdxValue < /* ListDescriptors[IdxDescriptor].GetAssociatedType().GetBinNumber()*/ HistoSize; IdxValue++)
                {
                    if ((cGlobalInfo.OptionsWindow.radioButtonHistoDisplayAutomatedMinMax.Checked) || (cGlobalInfo.OptionsWindow.radioButtonHistoDisplayManualMinMax.Checked))
                        CurrentSeries.Points.AddXY(ListSignatures[IdxDescriptor].GetHistoXvalue(IdxValue), ListSignatures[IdxDescriptor].GetHistovalue(IdxValue));
                    else
                        CurrentSeries.Points.Add(ListSignatures[IdxDescriptor].GetHistovalue(IdxValue));
                }

                CurrentChartArea.Axes[1].MajorGrid.Enabled = false;
                CurrentChartArea.Axes[1].MajorGrid.LineColor = Color.FromArgb(127, 127, 127);
                CurrentChartArea.Axes[1].LineColor = Color.FromArgb(127, 127, 127);
                CurrentChartArea.Axes[1].MajorTickMark.LineColor = Color.FromArgb(127, 127, 127);
                CurrentChartArea.Axes[1].LabelStyle.Enabled = false;
                CurrentChartArea.Axes[0].LineColor = Color.FromArgb(127, 127, 127);
                CurrentChartArea.Axes[0].MajorTickMark.LineColor = Color.FromArgb(127, 127, 127);
                if (cGlobalInfo.OptionsWindow.radioButtonHistoDisplayAutomatedMinMax.Checked)
                {
                    CurrentChartArea.Axes[0].Minimum = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().MinMaxHisto[0];
                    CurrentChartArea.Axes[0].Maximum = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().MinMaxHisto[1];
                }
                else if (cGlobalInfo.OptionsWindow.radioButtonHistoDisplayManualMinMax.Checked)
                {
                    CurrentChartArea.Axes[0].Minimum = (double)cGlobalInfo.OptionsWindow.numericUpDownManualMin.Value;
                    CurrentChartArea.Axes[0].Maximum = (double)cGlobalInfo.OptionsWindow.numericUpDownManualMax.Value;
                }
                CurrentSeries.Color = Color.White;
                CurrentSeries.BorderWidth = 1;
                CurrentChartArea.BorderWidth = borderSize;
                AssociatedChart.ChartAreas.Add(CurrentChartArea);
            }
            #endregion

            AssociatedChart.Location = new System.Drawing.Point(
                                        (int)((PosX - 1) * (cGlobalInfo.SizeHistoWidth + GutterSize) + cGlobalInfo.SizeHistoWidth),
                                        (int)((PosY - 1) * (cGlobalInfo.SizeHistoHeight + GutterSize) + cGlobalInfo.SizeHistoHeight));

            AssociatedChart.Series.Add(CurrentSeries);
            AssociatedChart.BackColor = this.GetClassColor();
            AssociatedChart.Width = (int)cGlobalInfo.SizeHistoWidth;
            AssociatedChart.Height = (int)cGlobalInfo.SizeHistoHeight;

            if ((CheckedMenuItem != null) && (cGlobalInfo.ViewMode != eViewMode.PIE))
            {
                Title MainLegend = new Title();

                if (WellPropertyType != null)
                {
                    object CurrentValue = this.ListProperties.FindValueByName(WellPropertyType.Name);

                    if (CurrentValue != null)
                    {


                        if (WellPropertyType.Name == "Well Class")
                        {
                           if (((int)CurrentValue<cGlobalInfo.ListWellClasses.Count) && ((int)CurrentValue >= 0))
                                MainLegend.Text = cGlobalInfo.ListWellClasses[(int)CurrentValue].Name;
                           else
                               MainLegend.Text = "n.a.";

                        }
                        else
                            MainLegend.Text = CurrentValue.ToString();
                    }
                    else
                        MainLegend.Text = "n.a.";

                    MainLegend.Docking = Docking.Bottom;
                    MainLegend.Font = new System.Drawing.Font("Arial", cGlobalInfo.SizeHistoWidth / 10 + 1, FontStyle.Bold);
                    MainLegend.ForeColor = cGlobalInfo.OptionsWindow.FFAllOptions.panelFontColor.BackColor; 
                    AssociatedChart.Titles.Add(MainLegend);

                }
                else if (CheckedMenuItem.Text == "Current Descriptor Value")
                {
                    MainLegend.Text = this.GetAverageValue(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor()).ToString("N3");

                    MainLegend.Docking = Docking.Bottom;
                    MainLegend.Font = new System.Drawing.Font("Arial", cGlobalInfo.SizeHistoWidth / 10 + 1, FontStyle.Regular);
                    AssociatedChart.Titles.Add(MainLegend);
                }
            }

            //AssociatedChart.Update();
            //AssociatedChart.Show();
            AssociatedChart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AssociatedChart_MouseClick);
            AssociatedChart.MouseDown += new MouseEventHandler(AssociatedChart_MouseDown);
            AssociatedChart.AllowDrop = true;
            AssociatedChart.DragDrop += new DragEventHandler(AssociatedChart_DragDrop);
            AssociatedChart.DragEnter += new DragEventHandler(AssociatedChart_DragEnter);


            AssociatedChart.GetToolTipText += new System.EventHandler<ToolTipEventArgs>(this.AssociatedChart_GetToolTipText);

            return this.AssociatedChart;
        }

        void AssociatedChart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1) return;

            if (e.Button == MouseButtons.Left)
            {
                cListWells ListWells = new cListWells(null);

                if (Control.ModifierKeys == (Keys.Control | Keys.Shift))
                {
                    foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                    {
                        foreach (var item in TmpPlate.ListActiveWells)
                        {
                            if (item.GetCurrentClassIdx() == this.GetCurrentClassIdx())
                                ListWells.Add(item);
                        }
                    }
                }
                else if (Control.ModifierKeys == Keys.Shift)
                {
                    foreach (var item in AssociatedPlate.ListActiveWells)
                    {
                        if (item.GetCurrentClassIdx() == this.GetCurrentClassIdx())
                            ListWells.Add(item);
                    }
                }
                else
                    ListWells.Add(this);

                this.AssociatedChart.DoDragDrop(ListWells, DragDropEffects.Copy);
                return;
            }
        }

        private void AssociatedChart_DragDrop(object sender, DragEventArgs e)
        {
            cListWells List_Wells = (cListWells)e.Data.GetData(typeof(cListWells));


            cExtendedTable ET = new cExtendedTable();
            ET.Name = "Distance to " + this.GetShortInfo().Remove(this.GetShortInfo().Length - 1);
            ET.ListRowNames = new List<string>();
            ET.ListTags = new List<object>();
            ET.Add(new cExtendedList("Euclidean"));
            ET.Add(new cExtendedList("Manhattan"));
            ET.Add(new cExtendedList("Vector Cosine"));

            ET[0].ListTags = new List<object>();
            ET[1].ListTags = new List<object>();
            ET[2].ListTags = new List<object>();

            string Info = "Distance from:\n" + this.GetShortInfo() + "\nto:\n";

            foreach (cWell item in List_Wells)
            {
                ET.ListRowNames.Add(item.GetShortInfo().Remove(item.GetShortInfo().Length - 1));
                ET.ListTags.Add(item);
                ET[0].Add(item.DistanceTo(this, eDistances.EUCLIDEAN));
                ET[0].ListTags.Add(item);
                ET[1].Add(item.DistanceTo(this, eDistances.MANHATTAN));
                ET[1].ListTags.Add(item);
                ET[2].Add(item.DistanceTo(this, eDistances.VECTOR_COS));
                ET[2].ListTags.Add(item);
                Info += item.GetShortInfo();
            }

            cViewerTable VT = new cViewerTable();
            VT.SetInputData(ET);
            VT.Run();

            cDesignerSplitter DS = new cDesignerSplitter();
            DS.Orientation = Orientation.Vertical;

            cViewertext VText = new cViewertext();

            Info += "\nDescriptors:\n\n";
            int Idx = 0;
            for (int i = 0; i < cGlobalInfo.CurrentScreening.ListDescriptors.Count; i++)
            {
                if (cGlobalInfo.CurrentScreening.ListDescriptors[i].IsActive())
                {
                    Info += (Idx++) + " - " + cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName() + "\n";
                }
            }

            VText.SetInputData(Info);
            VText.Run();

            DS.SetInputData(VText.GetOutPut());
            DS.SetInputData(VT.GetOutPut());

            DS.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(DS.GetOutPut());
            DTW.Title = ET.Name;
            DTW.Run();

            DTW.Display();

        }

        public TreeNode GetAssociatedTreeNode()
        {
            //  if (this.ShortInfo == null) return null;

            this.ShortInfo = GetShortInfo();

            TreeNode TN = new TreeNode(this.ShortInfo.Remove(this.ShortInfo.Length - 1));
            TN.Checked = true;
            TN.Tag = this;
            TN.ToolTipText = this.GetShortInfo();
            return TN;
        }

        private void AssociatedChart_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(cListWells)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        void AssociatedChart_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int ClassSelected = cGlobalInfo.CurrentScreening.GetSelectionType();
                if (ClassSelected == -2) return;

                if (!cGlobalInfo.CurrentScreening.IsSelectionApplyToAllPlates)
                {
                    if (ClassSelected == -1)
                    {
                        SetAsNoneSelected();
                        return;
                    }
                    else
                        SetClass(ClassSelected);

                    int[] a = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().UpdateNumberOfClass();
                }
                else
                {
                    cWell TempWell;
                    int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;

                    for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
                    {
                        cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(PlateIdx);
                        TempWell = CurrentPlateToProcess.GetWell(this.PosX - 1, this.PosY - 1, false);
                        if (TempWell == null) continue;
                        if (ClassSelected == -1)
                            TempWell.SetAsNoneSelected();
                        else
                            TempWell.SetClass(ClassSelected);

                        CurrentPlateToProcess.UpdateNumberOfClass();
                    }
                }

                if (ClassSelected == -1)
                {
                    cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().UpDataMinMax();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip NewMenu = new ContextMenuStrip();

                foreach (var item in this.GetExtendedContextMenu())
                    NewMenu.Items.Add(item);

                NewMenu.Show(Control.MousePosition);
            }          // Chart source = (Chart)sender;

        }

        private void AssociatedChart_GetToolTipText(object sender, System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs e)
        {
            byte[] strArray = new byte[1];
            strArray[0] = (byte)(this.PosY + 64);

            string Chara = Encoding.UTF7.GetString(strArray);
            Chara += this.PosX + " [" + this.PosX + "x" + this.PosY + "]" + ": " + this.GetCpdName() + " - " + this.GetClassName() + "\n";
            for (int i = 0; i < cGlobalInfo.CurrentScreening.ListDescriptors.Count; i++)  // to be checked
            {
                if (cGlobalInfo.CurrentScreening.ListDescriptors[i].IsActive() == false) continue;
                if (i == cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx)
                    Chara += "\t-> " + cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName() + ": " + string.Format("{0:0.######}", ListSignatures[i].GetValue()) + "\n";
                else
                    Chara += cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName() + ": " + string.Format("{0:0.######}", ListSignatures[i].GetValue()) + "\n";
            }
            e.Text = Chara;
        }

        public void BuildAndisplaySimpleContextMenu(ToolStripItem[] ToBeAdded)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            ToolStripMenuItem ToolStripMenuItem_Info = new ToolStripMenuItem("Info");
            ToolStripMenuItem ToolStripMenuItem_Histo = new ToolStripMenuItem("Histogram");
            ToolStripMenuItem ToolStripMenuItem_DisplayData = new ToolStripMenuItem("Display Data");
            ToolStripSeparator ToolStripSep = new ToolStripSeparator();


            if (this.SQLTableName != "")
                contextMenuStrip.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem_Info, ToolStripMenuItem_DisplayData, ToolStripMenuItem_Histo, ToolStripSep });
            else
                contextMenuStrip.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem_Info, ToolStripMenuItem_Histo, ToolStripSep });

            if (ToBeAdded != null)
                contextMenuStrip.Items.AddRange(ToBeAdded);
            //ToolStripSeparator SepratorStrip = new ToolStripSeparator();
            contextMenuStrip.Show(Control.MousePosition);

            ToolStripMenuItem_Info.Click += new System.EventHandler(this.DisplayInfo);
            ToolStripMenuItem_Histo.Click += new System.EventHandler(this.DisplayHisto);
            ToolStripMenuItem_DisplayData.Click += new System.EventHandler(this.ToolStripMenuItem_DisplaySingleCellData);
        }

        public List<ToolStripMenuItem> GetExtendedContextMenu()
        {
            List<ToolStripMenuItem> ListToReturn = new List<ToolStripMenuItem>();

            ListToReturn.Add(AssociatedPlate.ParentScreening.GetExtendedContextMenu());
            ListToReturn.Add(AssociatedPlate.GetExtendedContextMenu());

            #region Context Menu
            base.SpecificContextMenu = new ToolStripMenuItem("Well [" + this.PosX + "x" + this.PosY + "]");
            // ToolStripSeparator Sep = new ToolStripSeparator();
            // base.SpecificContextMenu.Items.Add(Sep);


            //ToolStripMenuItem ToolStripMenuItem_Info = new ToolStripMenuItem("Test Automated Menu");

            //base.SpecificContextMenu.Items.Add(ToolStripMenuItem_Info);

            ////   contextMenuStrip.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem_Info, ToolStripMenuItem_Histo, ToolStripSep, ToolStripMenuItem_Kegg, ToolStripSep1, ToolStripMenuItem_Copy });

            ////ToolStripSeparator SepratorStrip = new ToolStripSeparator();
            //// contextMenuStrip.Show(Control.MousePosition);
            //ToolStripMenuItem_Info.Click += new System.EventHandler(this.DisplayInfo);

            if (cGlobalInfo.ImageAccessor != null)
            {
                ToolStripMenuItem ToolStripMenuItem_Image = new ToolStripMenuItem("Image");
                ToolStripMenuItem_Image.Click += new System.EventHandler(this.ToolStripMenuItem_Image);
                SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Image);
            }

            ToolStripMenuItem ToolStripMenuItem_Histo = new ToolStripMenuItem("Histograms");
            ToolStripMenuItem_Histo.Click += new System.EventHandler(this.DisplayHisto);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Histo);

            if (this.ListProperties.FindValueByName("Locus ID") != null)
            {
                ToolStripMenuItem ToolStripMenuItem_Kegg = new ToolStripMenuItem("Kegg");
                ToolStripMenuItem_Kegg.Click += new System.EventHandler(this.DisplayPathways);
                SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Kegg);
            }

            if (this.SQLTableName != "")
            {
                ToolStripSeparator ToolStripSep = new ToolStripSeparator();
                SpecificContextMenu.DropDownItems.Add(ToolStripSep);

                ToolStripMenuItem ToolStripMenuItem_DisplaySingleCellData = new ToolStripMenuItem("Display Single Object Scatter Graph");
                ToolStripMenuItem_DisplaySingleCellData.Click += new System.EventHandler(this.ToolStripMenuItem_DisplaySingleCellData);
                SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplaySingleCellData);

                ToolStripMenuItem ToolStripMenuItem_AddToSingleCellAnalysis = new ToolStripMenuItem("Copy to List Wells");
                ToolStripMenuItem_AddToSingleCellAnalysis.Click += new System.EventHandler(this.ToolStripMenuItem_AddToSingleCellAnalysis);
                SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_AddToSingleCellAnalysis);
            }
            cListWells TmpListWells = new cListWells();
            TmpListWells.Add(this);
            SpecificContextMenu.DropDownItems.Add(TmpListWells.GetExtendedContextMenu());

            //if (this.Info != "")
            //{
            //    ToolStripSeparator ToolStripSep = new ToolStripSeparator();
            //    SpecificContextMenu.DropDownItems.Add(ToolStripSep);

            //    ToolStripMenuItem ToolStripMenuItem_DisplayImage = new ToolStripMenuItem("Display Image");
            //    ToolStripMenuItem_DisplayImage.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayImage);
            //    SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayImage);
            //}


            if (this.GetCurrentClassIdx() >= 0)
                base.SpecificContextMenu.DropDownItems.Add(cGlobalInfo.ListWellClasses[this.GetCurrentClassIdx()].GetExtendedContextMenu());

            SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_Info = new ToolStripMenuItem("Info");
            ToolStripMenuItem_Info.Click += new System.EventHandler(this.DisplayInfo);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Info);

            //ToolStripSeparator ToolStripSep1 = new ToolStripSeparator();
            //ToolStripMenuItem ToolStripMenuItem_Copy = new ToolStripMenuItem("Copy Visu.");

            //ToolStripSeparator SepratorStrip = new ToolStripSeparator();
            //base.SpecificContextMenu.Show(Control.MousePosition);
            //ToolStripMenuItem_Copy.Click += new System.EventHandler(this.CopyVisu);
            #endregion

            ListToReturn.Add(base.SpecificContextMenu);

            return ListToReturn;
        }
        //ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

        private void ToolStripMenuItem_AddToSingleCellAnalysis(object sender, EventArgs e)
        {
            AddToListWellsGUI();
        }

        public void AddToListWellsGUI()
        {
            //cGlobalInfo.WindowHCSAnalyzer.listBoxSelectedWells.Items.Add(this.AssociatedPlate.Name + " : " + this.GetPosX() + "x" + this.GetPosY());

            List<string> names = new List<string>();
            names.Add(this.AssociatedPlate.GetName() + " : " + this.GetPosX() + "x" + this.GetPosY());

            object ObjConcentration = this.ListProperties.FindValueByName("Concentration");
            if (ObjConcentration == null)
                names.Add("n.a.");
            else
                names.Add(((double)ObjConcentration).ToString());
            names.Add(this.GetCurrentClassIdx().ToString());

            names.Add(this.NumBiologicalObjects.ToString());

            // WindowForSingleCellPop.CellPopulation.AssociatedVariables = WindowForSingleCellPop.ListVariables;
            ListViewItem NewItem = new ListViewItem(names.ToArray());

            NewItem.Tag = this;

            NewItem.ToolTipText = this.Info;
            NewItem.BackColor = this.GetClassColor();
            cGlobalInfo.WindowHCSAnalyzer.listViewForListWell.Items.Add(NewItem);

            //cGlobalInfo.WindowHCSAnalyzer.listViewForListWell.Items.Add(this.AssociatedPlate.Name + " : " + this.GetPosX() + "x" + this.GetPosY());
            cGlobalInfo.ListSelectedWell.Add(this);
        }

        private void ToolStripMenuItem_DisplaySingleCellData(object sender, EventArgs e)
        {
            this.AssociatedPlate.DBConnection = new cDBConnection(this.AssociatedPlate, this.SQLTableName);

            cListSingleBiologicalObjects ListPhenotypes =
                this.AssociatedPlate.DBConnection.GetBiologicalPhenotypes(this);

            cExtendedTable ET = this.AssociatedPlate.DBConnection.GetWellValues(this, cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors());

            for (int i = 0; i < ET.Count; i++)
            {
                ET[i].ListTags = new List<object>();

                for (int j = 0; j < ListPhenotypes.Count; j++)
                    ET[i].ListTags.Add(ListPhenotypes[j]);
            }

            for (int j = 0; j < ListPhenotypes.Count; j++)
                ET.ListTags.Add(ListPhenotypes[j]);

            ET.Name = this.GetShortInfo();
            cViewer2DScatterPoint VS = new cViewer2DScatterPoint();
            VS.SetInputData(ET);
            VS.Chart.IsSelectable = true;
            if (VS.Run().IsSucceed == false) return;

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(VS.GetOutPut());
            DTW.Title = "Well [" + this.PosX + "x" + this.PosY + "] - " + this.GetShortInfo();
            DTW.Run();
            DTW.Display();

            this.AssociatedPlate.DBConnection.CloseConnection();
        }

        //private void ToolStripMenuItem_DisplayImage(object sender, EventArgs e)
        //{
        //    vtkImageData ID0 = null;

        //    string[] ListChar = this.Info.Split('.');

        //    if (ListChar[ListChar.Length - 1].ToLower() == "jpg")
        //    {
        //        vtkJPEGReader JPEGReader = vtkJPEGReader.New();
        //        JPEGReader.SetFileName(this.Info);
        //        JPEGReader.Update();
        //        ID0 = JPEGReader.GetOutput();
        //    }
        //    if ((ListChar[ListChar.Length - 1].ToLower() == "tif") || (ListChar[ListChar.Length - 1].ToLower() == "tiff"))
        //    {
        //        vtkTIFFReader TIFFReader = vtkTIFFReader.New();
        //        TIFFReader.SetFileName(this.Info);
        //        TIFFReader.Update();
        //        ID0 = TIFFReader.GetOutput();
        //    }

        //    vtkImageReader Reader = vtkImageReader.New();
        //    Reader.SetFileName(this.Info);
        //    Reader.Update();
        //    if (Reader == null) return;

        //    //vtkJPEGReader TIFFReader = vtkJPEGReader.New();
        //    //TIFFReader.SetFileName(this.Info);
        //    //TIFFReader.Update();


        //    //    cVolume3D Volume3D0 = new cVolume3D(ID0, new HCSAnalyzer.Classes._3D.cPoint3D(0, 0, 0));

        //    cViewerImage3D VI3D = new cViewerImage3D();
        //    VI3D.SetInputData(ID0);
        //    VI3D.Run();

        //    cDisplayToWindow DTW = new cDisplayToWindow();
        //    DTW.SetInputData(VI3D.GetOutPut());
        //    DTW.Title = this.Info;
        //    DTW.Run();
        //    DTW.Title = this.Info;
        //    DTW.Display();

        //}

        private void DisplayHisto(object sender, EventArgs e)
        {
            if ((cGlobalInfo.CurrentScreening.ListDescriptors == null) || (cGlobalInfo.CurrentScreening.ListDescriptors.Count == 0)) return;

            cDisplayToWindow CDW1 = new cDisplayToWindow();

            cListWells ListWellsToProcess = new cListWells(null);
            List<cPlate> PlateList = new List<cPlate>();
            cDesignerSplitter DS = new cDesignerSplitter();

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive) PlateList.Add(TmpPlate);

            foreach (cPlate TmpPlate in PlateList)
                foreach (cWell item in TmpPlate.ListActiveWells)
                    if (item.GetCurrentClassIdx() != -1) ListWellsToProcess.Add(item);

            cExtendedTable NewTable2 = ListWellsToProcess.GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
            NewTable2.Name = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName() + " - Stacked Histogram - " + PlateList.Count + " plates";

            cViewerStackedHistogram CV2 = new cViewerStackedHistogram();
            CV2.SetInputData(NewTable2);
            CV2.Chart.LabelAxisX = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
            CV2.Chart.IsBorder = false;
            CV2.Chart.Width = 0;
            CV2.Chart.Height = 0;

            StripLine AverageLine = new StripLine();
            AverageLine.BackColor = Color.Red;
            AverageLine.IntervalOffset = this.ListSignatures[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetValue();
            AverageLine.StripWidth = 0.0001;
            AverageLine.Text = this.ListSignatures[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetValue().ToString("N2");

            CV2.Run();

            CV2.Chart.ChartAreas[0].AxisX.StripLines.Add(AverageLine);

            DS.SetInputData(CV2.GetOutPut());


            PlateList.Clear();
            PlateList.Add(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate());
            ListWellsToProcess.Clear();
            foreach (cPlate TmpPlate in PlateList)
                foreach (cWell item in TmpPlate.ListActiveWells)
                    if (item.GetCurrentClassIdx() != -1) ListWellsToProcess.Add(item);

            CDW1.Title = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName() + " - Stacked Histogram (" + PlateList[0].GetName() + ")";

            cExtendedTable NewTable = ListWellsToProcess.GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
            NewTable.Name = CDW1.Title;

            cViewerStackedHistogram CV1 = new cViewerStackedHistogram();
            CV1.SetInputData(NewTable);
            CV1.Chart.LabelAxisX = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();
            CV1.Chart.Width = 0;
            CV1.Chart.Height = 0;



            //  CV1.Chart.ChartAreas[0].AxisX.Minimum = CV2.Chart.ChartAreas[0].AxisX.Minimum;
            //  CV1.Chart.ChartAreas[0].AxisX.Maximum = CV2.Chart.ChartAreas[0].AxisX.Maximum;
            CV1.Run();

            CV1.Chart.ChartAreas[0].AxisX.StripLines.Add(AverageLine);

            DS.SetInputData(CV1.GetOutPut());

            DS.Run();

            CDW1.SetInputData(DS.GetOutPut());

            CDW1.Run();
            CDW1.Display();

            return;



            //cExtendedList Pos = new cExtendedList();
            //cWell TempWell;

            //int NumberOfPlates = cGlobalInfo.PlateListWindow.listBoxPlateNameToProcess.Items.Count;

            //// loop on all the plate
            //for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
            //{
            //    cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate((string)cGlobalInfo.PlateListWindow.listBoxPlateNameToProcess.Items[PlateIdx]);

            //    for (int row = 0; row < cGlobalInfo.CurrentScreening.Rows; row++)
            //        for (int col = 0; col < cGlobalInfo.CurrentScreening.Columns; col++)
            //        {
            //            TempWell = CurrentPlateToProcess.GetWell(col, row, false);
            //            if (TempWell == null) continue;
            //            else
            //            {
            //                if (TempWell.GetClassIdx() == this.ClassForClassif)
            //                    Pos.Add(TempWell.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptor].GetValue());
            //            }
            //        }
            //}

            //if (Pos.Count == 0)
            //{
            //    MessageBox.Show("No well of class " + cGlobalInfo.CurrentScreening.SelectedClass + " selected !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //List<double[]> HistoPos = Pos.CreateHistogram((int)cGlobalInfo.OptionsWindow.numericUpDownHistoBin.Value);
            //if (HistoPos == null) return;
            //cWindowToDisplayHisto NewWindow = new cWindowToDisplayHisto(this.Parent, Pos);

            //Series SeriesPos = new Series();
            //SeriesPos.ShadowOffset = 1;

            //if (HistoPos.Count == 0) return;

            //for (int IdxValue = 0; IdxValue < HistoPos[0].Length; IdxValue++)
            //{
            //    SeriesPos.Points.AddXY(HistoPos[0][IdxValue], HistoPos[1][IdxValue]);
            //    SeriesPos.Points[IdxValue].ToolTip = HistoPos[1][IdxValue].ToString();

            //    if (this.ClassForClassif == -1)
            //        SeriesPos.Points[IdxValue].Color = Color.Black;
            //    else
            //        SeriesPos.Points[IdxValue].Color = cGlobalInfo.ListWellClasses[this.ClassForClassif].ColourForDisplay;
            //}

            //ChartArea CurrentChartArea = new ChartArea();
            //CurrentChartArea.BorderColor = Color.Black;

            //NewWindow.chartForSimpleForm.ChartAreas.Add(CurrentChartArea);
            //CurrentChartArea.Axes[0].MajorGrid.Enabled = false;
            //CurrentChartArea.Axes[0].Title = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptor].GetName();
            //CurrentChartArea.Axes[1].Title = "Sum";
            //CurrentChartArea.AxisX.LabelStyle.Format = "N2";

            //NewWindow.chartForSimpleForm.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            //CurrentChartArea.BackGradientStyle = GradientStyle.TopBottom;
            //CurrentChartArea.BackColor = cGlobalInfo.OptionsWindow.panel1.BackColor;
            //CurrentChartArea.BackSecondaryColor = Color.White;

            //SeriesPos.ChartType = SeriesChartType.Column;
            //// SeriesPos.Color = cGlobalInfo.CurrentScreening.GetColor(1);
            //NewWindow.chartForSimpleForm.Series.Add(SeriesPos);

            //NewWindow.chartForSimpleForm.ChartAreas[0].CursorX.IsUserEnabled = true;
            //NewWindow.chartForSimpleForm.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            //NewWindow.chartForSimpleForm.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            //NewWindow.chartForSimpleForm.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            //StripLine AverageLine = new StripLine();
            //AverageLine.BackColor = Color.Red;
            //AverageLine.IntervalOffset = this.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptor].GetValue();
            //AverageLine.StripWidth = 0.0001;
            //AverageLine.Text = String.Format("{0:0.###}", this.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptor].GetValue());
            //CurrentChartArea.AxisX.StripLines.Add(AverageLine);

            //if (cGlobalInfo.OptionsWindow.checkBoxDisplayHistoStats.Checked)
            //{
            //    StripLine NAverageLine = new StripLine();
            //    NAverageLine.BackColor = Color.Black;
            //    NAverageLine.IntervalOffset = Pos.Mean();
            //    NAverageLine.StripWidth = 0.0001;// double.Epsilon;
            //    CurrentChartArea.AxisX.StripLines.Add(NAverageLine);
            //    NAverageLine.Text = String.Format("{0:0.###}", NAverageLine.IntervalOffset);

            //    StripLine StdLine = new StripLine();
            //    StdLine.BackColor = Color.FromArgb(64, Color.Black);
            //    double Std = Pos.Std();
            //    StdLine.IntervalOffset = NAverageLine.IntervalOffset - 0.5 * Std;
            //    StdLine.StripWidth = Std;
            //    CurrentChartArea.AxisX.StripLines.Add(StdLine);
            //    //NAverageLine.StripWidth = 0.01;
            //}

            //Title CurrentTitle = new Title(this.StateForClassif + " - " + cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptor].GetName() + " histogram.");
            //CurrentTitle.Font = new System.Drawing.Font("Arial", 11, FontStyle.Bold);
            //NewWindow.chartForSimpleForm.Titles.Add(CurrentTitle);

            //NewWindow.Text = CurrentTitle.Text;
            //NewWindow.Show();
            //NewWindow.chartForSimpleForm.Update();
            //NewWindow.chartForSimpleForm.Show();
            //NewWindow.Controls.AddRange(new System.Windows.Forms.Control[] { NewWindow.chartForSimpleForm });

            //return;
        }

        //void DisplayPathways(object sender, EventArgs e)
        //{
        //    if (LocusID == -1) return;
        //    FormForKeggGene KeggWin = new FormForKeggGene();
        //    KEGG ServKegg = new KEGG();
        //    string[] intersection_gene_pathways = new string[1];

        //    intersection_gene_pathways[0] = "hsa:" + LocusID;
        //    string[] Pathways = ServKegg.get_pathways_by_genes(intersection_gene_pathways);
        //    if ((Pathways == null) || (Pathways.Length == 0))
        //    {
        //        MessageBox.Show("No pathway founded !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        return;
        //    }

        //    string GenInfo = ServKegg.bget(intersection_gene_pathways[0]);



        //    // FormForPathway PathwaysGenes = new FormForPathway();

        //    KeggWin.richTextBox.Text = GenInfo;
        //    KeggWin.Text = "Gene Infos";
        //    //PathwaysGenes.Show();
        //    ListP = new FormForPathway();

        //    ListP.listBoxPathways.DataSource = Pathways;

        //    ListP.Text = this.Name;
        //    ListP.Show();
        //    //foreach (string item in Pathways)
        //    //{
        //    //     string PathwayInfo = ServKegg.bget(item);
        //    //     FormPathwaysGenes PathwaysGenes = new FormPathwaysGenes();

        //    //     PathwaysGenes.richTextBox1.Text = PathwayInfo;
        //    //     PathwaysGenes.Text = "Pathways Infos";
        //    //     PathwaysGenes.Show();

        //    //}



        //    string[] fg_list = { "black" };
        //    string[] bg_list = { "orange" };
        //    // string[] intersection_gene_pathways = new string[1];

        //    // intersection_gene_pathways[0] = "hsa:" + LocusID;


        //    string pathway_map_html = "";
        //    //  KEGG ServKegg = new KEGG();

        //    pathway_map_html = ServKegg.get_html_of_colored_pathway_by_objects((string)(ListP.listBoxPathways.SelectedItem), intersection_gene_pathways, fg_list, bg_list);

        //    // FormForKegg KeggWin = new FormForKegg();
        //    if (pathway_map_html.Length == 0) return;

        //    //
        //    //KeggWin.Show();
        //    ListP.listBoxPathways.MouseDoubleClick += new MouseEventHandler(listBox1_MouseDoubleClick);
        //    KeggWin.webBrowser.Navigate(pathway_map_html);

        //    KeggWin.Show();

        //}

        void DisplayPathways(object sender, EventArgs e)
        {
            object ValObj = this.ListProperties.FindValueByName("Locus ID");
            if (ValObj == null) return;

            int LocusID = (int)ValObj;

            List<string> Pathways2 = null;
            try
            {
                Pathways2 = HCSAnalyzer.HCSAnalyzer.Find_Pathways(LocusID); //appel de la function static je ne sais pas pourquoi elle doit etre static…
            }
            catch (Exception)
            {
                MessageBox.Show("No pathway founded !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (Pathways2.Count > 0)
            {
                string getvars2 = "/get/hsa:" + LocusID;
                HttpWebRequest req2 = WebRequest.Create(string.Format("http://rest.kegg.jp" + getvars2)) as HttpWebRequest;
                req2.Method = "GET";

                HttpWebResponse response = req2.GetResponse() as HttpWebResponse;
                StreamReader reader2 = new StreamReader(response.GetResponseStream());


                string GenInfo = reader2.ReadToEnd();

                reader2.Close();

                foreach (string item in Pathways2)
                {
                    FormForKeggGene KeggWin = new FormForKeggGene();

                    KeggWin.webBrowser.Navigate("http://www.kegg.jp/kegg-bin/show_pathway?" + item + "/default%3dpink/" + "hsa:" + LocusID + "%09,blue");
                    KeggWin.richTextBox.Text = GenInfo;
                    KeggWin.Text = "Gene Infos";
                    KeggWin.Show();
                }
                response.Close();
            }
            else
            {
                MessageBox.Show("No pathway founded !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    string[] fg_list = { "black" };
        //    string[] bg_list = { "orange" };
        //    string[] intersection_gene_pathways = new string[1];

        //    intersection_gene_pathways[0] = "hsa:" + LocusID;

        //    string pathway_map_html = "";
        //    KEGG ServKegg = new KEGG();

        //    string[] ListGenesinPathway = ServKegg.get_genes_by_pathway((string)ListP.listBoxPathways.SelectedItem);
        //    double[] ListValues = new double[ListGenesinPathway.Length];
        //    int IDxGeneOfInterest = 0;
        //    foreach (cPlate CurrentPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
        //    {
        //        foreach (cWell CurrentWell in CurrentPlate.ListActiveWells)
        //        {
        //            string CurrentLID = "hsa:" + (int)CurrentWell.LocusID;

        //            for (int IdxGene = 0; IdxGene < ListGenesinPathway.Length; IdxGene++)
        //            {

        //                if (CurrentLID == intersection_gene_pathways[0])
        //                    IDxGeneOfInterest = IdxGene;

        //                if (CurrentLID == ListGenesinPathway[IdxGene])
        //                {
        //                    ListValues[IdxGene] = CurrentWell.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetValue();
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    bg_list = new string[ListGenesinPathway.Length];
        //    fg_list = new string[ListGenesinPathway.Length];

        //    double MinValue = ListValues.Min();
        //    double MaxValue = ListValues.Max();

        //    for (int IdxCol = 0; IdxCol < bg_list.Length; IdxCol++)
        //    {

        //        int ConvertedValue = (int)((((cGlobalInfo.LUTs.LUT_JET[0].Length - 1) * (ListValues[IdxCol] - MinValue)) / (MaxValue - MinValue)));

        //        Color Coul = Color.FromArgb(cGlobalInfo.LUTs.LUT_JET[0][ConvertedValue], cGlobalInfo.LUTs.LUT_JET[1][ConvertedValue], cGlobalInfo.LUTs.LUT_JET[2][ConvertedValue]);

        //        if (IdxCol == IDxGeneOfInterest)
        //            fg_list[IdxCol] = "white";
        //        else
        //            fg_list[IdxCol] = "#000000";
        //        bg_list[IdxCol] = "#" + Coul.Name.Remove(0, 2);

        //    }

        //    pathway_map_html = ServKegg.get_html_of_colored_pathway_by_objects((string)ListP.listBoxPathways.SelectedItem, ListGenesinPathway, fg_list, bg_list);

        //    string GenInfo = ServKegg.bget((string)ListP.listBoxPathways.SelectedItem);
        //    string[] Genes = GenInfo.Split(new char[] { '\n' });
        //    string Res = "";
        //    foreach (string item in Genes)
        //    {
        //        string[] fre = item.Split(' ');
        //        string[] STRsection = fre[0].Split('_');

        //        if (STRsection[0] != "NAME") continue;

        //        for (int i = 1; i < fre.Length; i++)
        //        {
        //            if (fre[i] == "") continue;
        //            Res += fre[i] + " ";
        //        }
        //    }

        //    FormForKegg KeggWin = new FormForKegg();

        //    if (pathway_map_html.Length == 0) return;

        //    KeggWin.Text = Res;
        //    KeggWin.Show();

        //    KeggWin.webBrowser.Navigate(pathway_map_html);
        //}

        public Chart GetChart()
        {
            if (ListSignatures[CurrentDescriptorToDisplay].GetAssociatedType().GetBinNumber() == 1) return null;

            Series CurrentSeries = new Series("ChartSeries" + PosX + "x" + PosY);
            //CurrentSeries.ShadowOffset = 2;

            for (int IdxValue = 0; IdxValue < ListSignatures[CurrentDescriptorToDisplay].GetAssociatedType().GetBinNumber(); IdxValue++)
                CurrentSeries.Points.Add(ListSignatures[CurrentDescriptorToDisplay].GetHistovalue(IdxValue));

            ChartArea CurrentChartArea = new ChartArea("ChartArea" + PosX + "x" + PosY);
            CurrentChartArea.BorderColor = Color.White;

            Chart ChartToReturn = new Chart();
            ChartToReturn.ChartAreas.Add(CurrentChartArea);
            // ChartToReturn.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            CurrentChartArea.BackColor = Color.White;

            CurrentChartArea.Axes[1].LabelStyle.Enabled = false;
            CurrentChartArea.Axes[1].MajorGrid.Enabled = false;
            CurrentChartArea.Axes[0].Enabled = AxisEnabled.False;
            CurrentChartArea.Axes[1].Enabled = AxisEnabled.False;


            CurrentChartArea.Axes[0].MajorGrid.Enabled = false;
            //  CurrentChartArea.Axes[0].Title = ListDescriptors[CurrentDescriptorToDisplay].GetName();
            CurrentSeries.ChartType = SeriesChartType.Line;
            CurrentSeries.Color = Color.Black;
            // CurrentSeries.BorderWidth = 3;
            CurrentSeries.ChartArea = "ChartArea" + PosX + "x" + PosY;

            CurrentSeries.Name = "Series" + PosX + "x" + PosY;
            ChartToReturn.Series.Add(CurrentSeries);

            Title CurrentTitle = new Title(PosX + "x" + PosY);
            // ChartToReturn.Titles.Add(CurrentTitle);

            ChartToReturn.Width = 100;
            ChartToReturn.Height = 48;

            ChartToReturn.Update();
            //  ChartToReturn.Show();

            return ChartToReturn;

        }

        /// <summary>
        /// Display the information window related to the selected well
        /// </summary>

        private void ToolStripMenuItem_Image(object sender, EventArgs e)
        {
            int Field = (int)cGlobalInfo.OptionsWindow.numericUpDownDefaultField.Value;

            cDisplaySingleImage IV = new cDisplaySingleImage();

            cGetImageFromWells IFW = new cGetImageFromWells();
            IFW.SetInputData(new cListWells(this));
            IFW.ListProperties.FindByName("Field").SetNewValue((int)0);

            if(cGlobalInfo.OptionsWindow.numericUpDownImageAccessNumberOfFields.Value>1)
                IFW.ListProperties.FindByName("Field").IsGUIforValue = true;
            if (IFW.Run().IsSucceed == false)
            {
                return;
            }
             
            IV.SetInputData(IFW.GetOutPut());
            IV.Title = this.GetShortInfo() + " - Field: " + ((int)(IFW.ListProperties.FindByName("Field").GetValue()));
            IV.IsDisplayScale = true;
            IV.IsUseSavedDefaultDisplayProperties = true;
            IV.Run();
        }

        public void DisplayInfoWindow(int IdxDescriptor)
        {
            FormForWellInformation NewWindow = new FormForWellInformation(this);
            NewWindow.Text = this.GetShortInfo();

            cExtendedTable EL = this.GetAverageValuesList(true);
            
            cViewerTable VT = new cViewerTable();
            VT.SetInputData(EL);
            VT.Run();
            cExtendedControl EXT = VT.GetOutPut();
            EXT.Width = NewWindow.panelForDescValues.Width;
            EXT.Height = NewWindow.panelForDescValues.Height;
            
            NewWindow.panelForDescValues.Controls.Add(VT.GetOutPut());

            NewWindow.textBoxName.Text = GetCpdName();
            NewWindow.textBoxInfo.Text = Info;

            object ConcentrationValue = this.ListProperties.FindValueByName("Concentration");
            if (ConcentrationValue != null) NewWindow.textBoxConcentration.Text = ((double)ConcentrationValue).ToString("e4");
            else
                NewWindow.textBoxConcentration.Text = "n.a.";

            object ValObj = this.ListProperties.FindValueByName("Locus ID");
            if (ValObj != null)
            {
                int LocusID = (int)ValObj;
                NewWindow.textBoxLocusID.Text = ((int)(LocusID)).ToString();
            }
            else
                NewWindow.textBoxLocusID.Text = "n.a.";

            if (cGlobalInfo.ViewMode != eViewMode.PIE)
            {
                if (/*(cGlobalInfo.ViewMode == eViewMode.DISTRIBUTION) &&*/ (cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor().IsConnectedToDatabase))
                {
                    this.AssociatedPlate.DBConnection = new cDBConnection(this.AssociatedPlate, this.SQLTableName);

                    List<cDescriptorType> LCDT = new List<cDescriptorType>();
                    LCDT.Add(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor());
                    cExtendedTable ET = new cExtendedTable();

                    foreach (var item in cGlobalInfo.ListCellularPhenotypes)
                    {
                        List<cCellularPhenotype> ListCellularPhenotypesToBeSelected = new List<cCellularPhenotype>();
                        ListCellularPhenotypesToBeSelected.Add(item);

                        cExtendedTable TmpET = this.AssociatedPlate.DBConnection.GetWellValues(this,
                                                 LCDT,
                                                 ListCellularPhenotypesToBeSelected);
                        if (TmpET.Count == 0) continue;
                        TmpET[0].Name = item.Name;
                        TmpET[0].Tag = item;

                        ET.Add(TmpET[0]);
                    }

                    ET.Name = this.GetShortInfo();
                    cViewerStackedHistogram VSH = new cViewerStackedHistogram();
                    VSH.SetInputData(ET);
                    // VSH.Chart.BinNumber = LCDT[0].GetBinNumber();
                    VSH.ListProperties.FindByName("Bin Number").SetNewValue((int)LCDT[0].GetBinNumber());

                    //VS.Chart.IsSelectable = true;
                    //VSH.Chart.BackgroundColor = Color.LightGray;
                    VSH.Chart.IsShadow = true;
                    VSH.Chart.IsXGrid = true;
                    VSH.Chart.IsYGrid = true;
                    VSH.Chart.LabelAxisX = cGlobalInfo.CurrentScreening.ListDescriptors[cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx].GetName();

                    VSH.Run();
                    VSH.Chart.Width = 0;
                    VSH.Chart.Height = 0;

                    cExtendedControl EC = VSH.GetOutPut();
                    EC.Width = NewWindow.chartForFormWell.Width;
                    EC.Height = NewWindow.chartForFormWell.Height;

                    NewWindow.chartForFormWell.Controls.Add(EC);

                    this.AssociatedPlate.DBConnection.CloseConnection();
                }
                else
                {
                    NewWindow.tabControlMain.TabPages.RemoveByKey("tabPageHisto");

                }

            }
            else
            {
                NewWindow.tabControlMain.TabPages["tabPageHisto"].Text = "Pie";
                Series CurrentSeries = new Series("ChartSeries" + PosX + "x" + PosY);
                ChartArea CurrentChartArea = new ChartArea("ChartArea" + PosX + "x" + PosY);
                CurrentSeries.ChartType = SeriesChartType.Pie;
                for (int IdxValue = 0; IdxValue < ListSignatures.Count; IdxValue++)
                {
                    if (ListSignatures[IdxValue].GetAssociatedType().IsActive())
                    {
                        double Value = ListSignatures[IdxValue].GetValue();
                        CurrentSeries.Points.Add(Value);
                        if (Value > 0)
                        {
                            CurrentSeries.Points[CurrentSeries.Points.Count - 1].Label = ListSignatures[IdxValue].GetAssociatedType().GetName();
                            CurrentSeries.Points[CurrentSeries.Points.Count - 1].ToolTip = Value.ToString();
                        }
                    }
                }
                CurrentChartArea.BorderColor = Color.Black;

                NewWindow.chartForFormWell.ChartAreas.Add(CurrentChartArea);
                NewWindow.chartForFormWell.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
                CurrentChartArea.BackColor = Color.FromArgb(64, 64, 64);
                CurrentSeries.ChartArea = "ChartArea" + PosX + "x" + PosY;

                CurrentSeries.Name = "Series" + PosX + "x" + PosY;
                NewWindow.chartForFormWell.Series.Add(CurrentSeries);
            }

            #region Text Info
            NewWindow.richTextBoxDescription.AcceptsTab = true;
            NewWindow.richTextBoxDescription.AppendText("Plate: " + this.AssociatedPlate.GetName() + "\nWell: [" + this.GetPosX() + "x" + this.GetPosY() + "] - " + this.GetNumBiologicalObjects() + " Object(s).");

            NewWindow.richTextBoxDescription.AppendText("\n\nProperty List:\n");
            foreach (var item in this.ListProperties)
            {
                if (item.GetValue() != null)
                {
                    NewWindow.richTextBoxDescription.AppendText("\nName:\t" + item.PropertyType.Name + "\n\tType: " + item.PropertyType.Type.ToString() + "\n\tValue: " + item.GetValue().ToString() + "\n\tLocked: " + item.PropertyType.IsLocked.ToString() + "\n");
                    if ((item.PropertyType.Type == eDataType.DOUBLE) || (item.PropertyType.Type == eDataType.INTEGER))
                    {
                        NewWindow.richTextBoxDescription.AppendText("\tMin:\t" + item.PropertyType.Min + "\n\tMax: " + item.PropertyType.Max + "\n");
                    }

                }
                else
                    NewWindow.richTextBoxDescription.AppendText("\nName:\t" + item.PropertyType.Name + "\n\tType: " + item.PropertyType.Type.ToString() + "\n\tValue: NULL\n\tLocked: " + item.PropertyType.IsLocked.ToString() + "\n");
            }
            #endregion

            NewWindow.chartForFormWell.Update();
            NewWindow.Show();
            return;
        }


        public cViewerStackedHistogram GetViewerStackedHistogram(cDescriptorType DT)
        {
            cListWells LW = new cListWells(this);
            return LW.GetSingleCellStackedHisto(DT,null);
        }

        private void DisplayInfo(object sender, EventArgs e)
        {
            DisplayInfoWindow(CurrentDescriptorToDisplay);
        }

        public void AddSignatures(cListSignature LDesc)
        {
            for (int i = 0; i < LDesc.Count; i++)
            {
                LDesc[i].AssociatedWell = this;
                LDesc[i].UpDateDescriptorStatistics();
                this.ListSignatures.Add(LDesc[i]);
            }
        }

        /// <summary>
        /// copy the plate visualization to the clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyVisu(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();

            Graphics g = cGlobalInfo.panelForPlate.CreateGraphics();
            Bitmap bmp = new Bitmap(cGlobalInfo.panelForPlate.Width, cGlobalInfo.panelForPlate.Height);

            cGlobalInfo.panelForPlate.DrawToBitmap(bmp, new Rectangle(0, 0, cGlobalInfo.panelForPlate.Width, cGlobalInfo.panelForPlate.Height));
            Clipboard.SetImage(bmp);

            return;
        }

        /// <summary>
        /// Create a single instance for WEKA
        /// </summary>
        /// <param name="NClasses">Number of classes</param>
        /// <returns>the weka instances</returns>
        public Instances CreateInstanceForNClasses(cInfoClass InfoClass)
        {
            List<double> AverageList = new List<double>();

            for (int i = 0; i < cGlobalInfo.CurrentScreening.ListDescriptors.Count; i++)
                if (cGlobalInfo.CurrentScreening.ListDescriptors[i].IsActive()) AverageList.Add(GetAverageValuesList(false)[0][i]);

            weka.core.FastVector atts = new FastVector();

            List<string> NameList = cGlobalInfo.CurrentScreening.ListDescriptors.GetListNameActives();

            for (int i = 0; i < NameList.Count; i++)
                atts.addElement(new weka.core.Attribute(NameList[i]));

            weka.core.FastVector attVals = new FastVector();
            for (int i = 0; i < InfoClass.NumberOfClass; i++)
                attVals.addElement("Class" + i);

            atts.addElement(new weka.core.Attribute("Class__", attVals));

            Instances data1 = new Instances("SingleInstance", atts, 0);

            double[] newTable = new double[AverageList.Count + 1];
            Array.Copy(AverageList.ToArray(), 0, newTable, 0, AverageList.Count);
            //newTable[AverageList.Count] = 1;

            data1.add(new DenseInstance(1.0, newTable));
            data1.setClassIndex((data1.numAttributes() - 1));
            return data1;
        }

        //public Instances CreateInstanceWithClass(List<bool> ListClassSelected)
        //{
        //    List<double> AverageList = new List<double>();

        //    for (int i = 0; i < cGlobalInfo.CurrentScreening.ListDescriptors.Count; i++)
        //        if (cGlobalInfo.CurrentScreening.ListDescriptors[i].IsActive()) AverageList.Add(GetAverageValuesList(false)[i]);

        //    weka.core.FastVector atts = new FastVector();

        //    List<string> NameList = cGlobalInfo.CurrentScreening.ListDescriptors.GetListNameActives();

        //    for (int i = 0; i < NameList.Count; i++)
        //        atts.addElement(new weka.core.Attribute(NameList[i]));

        //    weka.core.FastVector attVals = new FastVector();

        //    for (int i = 0; i < InfoClass.NumberOfClass; i++)
        //        attVals.addElement("Class" + i);

        //    atts.addElement(new weka.core.Attribute("Class", attVals));

        //    Instances InstancesToReturn = new Instances("SingleInstance", atts, 0);

        //    double[] newTable = new double[AverageList.Count + 1];
        //    Array.Copy(AverageList.ToArray(), 0, newTable, 0, AverageList.Count);
        //    //newTable[AverageList.Count] = 1;

        //    InstancesToReturn.add(new DenseInstance(1.0, newTable));
        //    InstancesToReturn.setClassIndex((InstancesToReturn.numAttributes() - 1));
        //    return InstancesToReturn;


        //}


    }
}
