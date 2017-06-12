using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibPlateAnalysis;
using HCSAnalyzer.Classes;
using System.IO;
using HCSAnalyzer.Forms.FormsForOptions;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Controls
{
    public abstract partial class cWindowToDisplayGeneralArray : Form
    {
        protected List<cPanelForDisplayArray> PanelList;
        public int CellSize = 5;
        private int Gutter = 25;
        protected int XNumber;

        public cWindowToDisplayGeneralArray(cPanelForDisplayArray[][] ListPanel)
        {
            InitializeComponent();
        }

        public cWindowToDisplayGeneralArray(cPanelForDisplayArray SimplePanel)
        {
            InitializeComponent();
            SimplePanel.Location = new Point(10, 10);
            this.Controls.Add(SimplePanel);
            this.Show();
        }

        public void RefreshDisplay(bool IsOnlyColors)
        {
            // System.Drawing.Graphics formGraphics = this.panelForPlates.CreateGraphics();

            if (!IsOnlyColors) this.panelForPlates.Controls.Clear();
            //int XNumber = this.panelForPlates.Width / (PanelList[0].Width + Gutter);
            for (int i = 0; i < PanelList.Count; i++)
            {
                PanelList[i].IsGlobalNormalization = checkBoxGlobalNormalization.Checked;
                PanelList[i].IsDisplayClasses = checkBoxDisplayClasses.Checked;

                PanelList[i].SetNewCellSize(this.CellSize);
                PanelList[i].Location = new Point(i % XNumber * (PanelList[i].Width + Gutter) + Gutter / 2, (i / XNumber) * (PanelList[i].Height + Gutter) + Gutter / 2);

                if (!IsOnlyColors) this.panelForPlates.Controls.Add(PanelList[i]);

                if (PanelList[i].Tag != null)
                 {
                     cPlate CurrentPlate = (cPlate)PanelList[i].Tag;
                     Label TextToAdd = new Label();
                     TextBox TB = new TextBox();
                     TB.Text = CurrentPlate.GetName();
                     TB.ReadOnly = true;
                     TB.Location = new Point(PanelList[i].Location.X, PanelList[i].Location.Y + PanelList[i].Height);
                     TB.Width = PanelList[i].Width;
                     this.panelForPlates.Controls.Add(TB);
                 }

            }

        }

        public cWindowToDisplayGeneralArray()
        {
            InitializeComponent();
        }

        private void buttonIncrease_Click(object sender, EventArgs e)
        {
            this.CellSize++;
            this.panelForPlates.Controls.Clear();
            RefreshDisplay(false);
        }

        private void buttonReduce_Click(object sender, EventArgs e)
        {
            this.CellSize--;
            this.panelForPlates.Controls.Clear();
            if (this.CellSize < 1) this.CellSize = 1;
            RefreshDisplay(false);
        }

        private void checkBoxGlobalNormalization_CheckedChanged(object sender, EventArgs e)
        {
            this.RefreshDisplay(true);
        }

        private void checkBoxDisplayClasses_CheckedChanged(object sender, EventArgs e)
        {
            this.RefreshDisplay(true);
        }

        private void comboBoxDistances_SelectedIndexChanged(object sender, EventArgs e)
        {
            eDistances SelectedDist = eDistances.EUCLIDEAN;

            switch (comboBoxDistances.SelectedIndex)
            {
                case 0:
                    SelectedDist = eDistances.EUCLIDEAN;
                    break;
                case 1:
                    SelectedDist = eDistances.MANHATTAN;
                    break;
                case 2:
                    SelectedDist = eDistances.VECTOR_COS;
                    break;
                case 3:
                    SelectedDist = eDistances.BHATTACHARYYA;
                    break;
                case 4:
                    SelectedDist = eDistances.EMD;
                    break;
                default:
                    break;
            }

            ((FormToDisplayDistanceMap)(PanelList[0])).RecomputeDistances(SelectedDist);

            this.RefreshDisplay(true);
            this.Text = "Distance Matrix ( " + SelectedDist.ToString() + ")";
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (PanelList.Count == 0)
            {
                MessageBox.Show("At least one plate has to bo selected !", "Error - Operation Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                //PlateListWindow.ShowDialog();// != System.Windows.Forms.DialogResult.OK) return; 
            }

            cGlobalInfo.WindowHCSAnalyzer.toolStripcomboBoxPlateList.Items.Clear();
            PanelList[0].CurrentScreening.ListPlatesActive.Clear();
            cGlobalInfo.WindowHCSAnalyzer.RefreshInfoScreeningRichBox();

            for (int i = 0; i < PanelList.Count; i++)
            {
                PanelList[0].CurrentScreening.ListPlatesActive.Add(PanelList[0].CurrentScreening.ListPlatesAvailable.GetPlate(PanelList[i].AssociatedPlate.GetName()));
                cGlobalInfo.WindowHCSAnalyzer.toolStripcomboBoxPlateList.Items.Add(PanelList[0].CurrentScreening.ListPlatesActive[i].GetName());
            }
            PanelList[0].CurrentScreening.CurrentDisplayPlateIdx = 0;
            cGlobalInfo.WindowHCSAnalyzer.toolStripcomboBoxPlateList.SelectedIndex = 0;

            PanelList[0].CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(PanelList[0].CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);

            this.Dispose();
        }

        private void displayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormForFullScreeningViewOptions WindowForVizuOptions = new FormForFullScreeningViewOptions();

            if (PanelList.Count > this.XNumber)
                WindowForVizuOptions.numericUpDownHorizontalPlateNumbers.Maximum = PanelList.Count;
            else
                WindowForVizuOptions.numericUpDownHorizontalPlateNumbers.Maximum = this.XNumber;
            //  if(this.XNumber>=Win
            WindowForVizuOptions.numericUpDownHorizontalPlateNumbers.Value = (decimal)this.XNumber;

            if (WindowForVizuOptions.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            this.XNumber = (int)WindowForVizuOptions.numericUpDownHorizontalPlateNumbers.Value;
            RefreshDisplay(false);
        }
    }

    public class cWindowToDisplayEntireScreening : cWindowToDisplayGeneralArray
    {
       // cGlobalInfo GlobalInfo;

        public cWindowToDisplayEntireScreening(List<cPanelForDisplayArray> PanelList, string Text, int XNumber)
        {
          //  this.GlobalInfo = GlobalInfo;
            this.XNumber = XNumber;
            this.Text += " : " + Text;
            cExtendedList GlobalValues = new cExtendedList();

            for (int i = 0; i < PanelList.Count; i++)
            {
                cExtendedList TmpList = PanelList[i].GetListValues();
                if (TmpList == null) continue;
                GlobalValues.AddRange(TmpList);
            }

            double Max_ = GlobalValues.Max();
            double Min_ = GlobalValues.Min();

            for (int i = 0; i < PanelList.Count; i++)
            {
                PanelList[i].GlobalMin = Min_;
                PanelList[i].GlobalMax = Max_;
                PanelList[i].ParentWindow = this;
            }

            this.PanelList = PanelList;
            this.comboBoxDistances.Visible = false;

            RefreshDisplay(false);
            this.Show();
        }

        public void RemovePlate(cPlate PlateToRemove)
        {
            for (int i = 0; i < PanelList.Count; i++)
            {
                if (PanelList[i].AssociatedPlate == PlateToRemove)
                {
                    PanelList.RemoveAt(i);
                    RefreshDisplay(false);
                    break;
                }
            }
        }

        public void PerformScreeningClustering(List<cPlate> PlatesToCluster)
        {
            cGlobalInfo.WindowHCSAnalyzer.PerformScreeningClustering(PlatesToCluster, false);

            RefreshDisplay(false);
        }
    }

    public class cWindowToDisplayEntireDescriptors : cWindowToDisplayGeneralArray
    {
        public cWindowToDisplayEntireDescriptors(List<cPanelForDisplayArray> PanelList, string Text, int XNumber)
        {
            this.XNumber = XNumber;
            this.Text = "Descriptors View : " + Text;
            cExtendedList GlobalValues = new cExtendedList();

            for (int i = 0; i < PanelList.Count; i++)
            {
                cExtendedList TmpList = PanelList[i].GetListValues();
                if (TmpList == null) continue;
                GlobalValues.AddRange(TmpList);
            }

            double Max_ = GlobalValues.Max();
            double Min_ = GlobalValues.Min();

            for (int i = 0; i < PanelList.Count; i++)
            {
                PanelList[i].GlobalMin = Min_;
                PanelList[i].GlobalMax = Max_;
                PanelList[i].ParentWindow = this;
            }

            this.PanelList = PanelList;
            this.comboBoxDistances.Visible = false;
            RefreshDisplay(false);
            this.Show();
        }
    }

    public class cWindowToDisplaySingleMatrix : cWindowToDisplayGeneralArray
    {
        public cWindowToDisplaySingleMatrix(cPanelForDisplayArray PanelToDisplay, eDistances Dist)
        {
            //this.CellSize = SizeSquare;
            this.Text = "Distance Matrix ( " + Dist.ToString() + ")";

            this.XNumber = 1;

            this.checkBoxDisplayClasses.Visible = false;
            this.checkBoxGlobalNormalization.Visible = false;

            cExtendedList GlobalValues = new cExtendedList();


            GlobalValues.AddRange(PanelToDisplay.GetListValues());

            double Max_ = GlobalValues.Max();
            double Min_ = GlobalValues.Min();

            PanelToDisplay.GlobalMin = Min_;
            PanelToDisplay.GlobalMax = Max_;

            this.PanelList = new List<cPanelForDisplayArray>();
            this.PanelList.Add(PanelToDisplay);


            this.Width = PanelToDisplay.Width + 500;
            this.Height = PanelToDisplay.Height + 500;
            RefreshDisplay(false);

            this.Show();
        }
    }

    public abstract partial class cPanelForDisplayArray : Panel
    {
        public cScreening CurrentScreening;
        protected double[][] ValuesMatrix = null;
        public cPlate AssociatedPlate = null;
        protected byte[][] LUT;
        public cWindowToDisplayGeneralArray ParentWindow;
        
        
        public cExtendedList GetListValues()
        {
            if (ValuesMatrix == null) return null;
            cExtendedList ToReturn = new cExtendedList();

            for (int j = 0; j < ValuesMatrix.Length; j++)
                for (int i = 0; i < ValuesMatrix[0].Length; i++)
                {
                    if (ValuesMatrix[j][i] != null) ToReturn.Add(ValuesMatrix[j][i]);

                }
            return ToReturn;

        }

        protected double Min = double.MaxValue;
        protected double Max = double.MinValue;

        public double GlobalMin = double.MaxValue;
        public double GlobalMax = double.MinValue;

        int CellSize;
        public bool IsGlobalNormalization = false;
        public bool IsDisplayClasses = false;


        public void SetNewCellSize(int NewSize)
        {
            this.CellSize = NewSize;
            this.Height = ValuesMatrix.Length * this.CellSize;
            this.Width = ValuesMatrix[0].Length * this.CellSize;
            this.DisplayMatrix();
        }

        public cPanelForDisplayArray()
        {
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.cPanelForDisplayArray_Paint);
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.Tag = 
            this.DoubleBuffered = false;
        }

        public cPanelForDisplayArray(double[][] Values, cScreening CurrentScreening)
        {
            this.CellSize = 2;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.cPanelForDisplayArray_Paint);
            this.DoubleBuffered = false;

            this.CurrentScreening = CurrentScreening;
            this.ValuesMatrix = Values;


            for (int j = 0; j < ValuesMatrix.Length; j++)
                for (int i = 0; i < ValuesMatrix[0].Length; i++)
                {
                    if (ValuesMatrix[j][i] > this.Max)
                        this.Max = ValuesMatrix[j][i];
                    if (ValuesMatrix[j][i] < this.Min)
                        this.Min = ValuesMatrix[j][i];

                }

            DisplayMatrix();






        }

        private void cPanelForDisplayArray_Paint(object sender, PaintEventArgs e)
        {
            DisplayMatrix();
        }

        private void cPanelForDisplayArray_ClientSizeChanged(object sender, EventArgs e)
        {
            DisplayMatrix();
        }

        protected void DisplayMatrix()
        {
            int PosXMatrix = 0;
            int PosYMatrix = 0;
            // Color BorderColor = Color.BlueViolet;
            Color CenterColor = Color.Transparent;

            int NumCol = ValuesMatrix.Length;
            int NumRow = ValuesMatrix[0].Length;

            double[,] MatrixToDisplay = new double[NumCol, NumRow];
            System.Drawing.Graphics formGraphics = this.CreateGraphics();
            // this.BackColor = Color.Tomato;
            if ((this.IsDisplayClasses == false))
            {
                double LocalMin = Min;
                double LocalMax = Max;

                if (IsGlobalNormalization)
                {
                    LocalMin = GlobalMin;
                    LocalMax = GlobalMax;
                }




                //  MemoryStream ms = new MemoryStream(BitmapData);

                Bitmap BMP = new Bitmap(ValuesMatrix[0].Length * CellSize + PosXMatrix, ValuesMatrix.Length * CellSize + PosYMatrix);

                for (int j = 0; j < ValuesMatrix.Length; j++)
                    for (int i = 0; i < ValuesMatrix[0].Length; i++)
                    {
                        CenterColor = Color.Transparent;

                        if (double.IsNaN(ValuesMatrix[j][i]))
                        {
                            CenterColor = this.BackColor;

                            for (int k = 0; k < CellSize; k++)
                                for (int l = 0; l < CellSize; l++)
                                {
                                    BMP.SetPixel(i * CellSize + PosXMatrix + k, j * CellSize + PosYMatrix + l, CenterColor);
                                }
                            continue;
                        }
                        //  continue;
                        int ConvertedValue1 = 0;
                        if (LocalMax != LocalMin)
                            ConvertedValue1 = (int)(((ValuesMatrix[j][i] - LocalMin) * (LUT[0].Length - 1)) / (LocalMax - LocalMin));
                        CenterColor = Color.FromArgb(LUT[0][ConvertedValue1], LUT[1][ConvertedValue1], LUT[2][ConvertedValue1]);

                        for (int k = 0; k < CellSize; k++)
                            for (int l = 0; l < CellSize; l++)
                            {
                                BMP.SetPixel(i * CellSize + PosXMatrix + k, j * CellSize + PosYMatrix + l, CenterColor);
                            }
                    }
                //Image.FromHbitmap( ImToDisplay = new 


                formGraphics.DrawImage((Image)BMP, PosXMatrix, PosYMatrix, ValuesMatrix[0].Length * CellSize, ValuesMatrix.Length * CellSize);


                
                return;
                // if (this.AssociatedPlate == null)
                //{

                //    for (int j = 0; j < ValuesMatrix.Length; j++)
                //        for (int i = 0; i < ValuesMatrix[0].Length; i++)
                //        {

                //            int PosX = PosXMatrix + i * CellSize;
                //            int PosY = PosYMatrix + j * CellSize;
                //            if (!double.IsNaN(ValuesMatrix[j][i]))
                //            {
                //                int ConvertedValue = (int)(((ValuesMatrix[j][i] - LocalMin) * (LUT[0].Length - 1)) / (LocalMax - LocalMin));
                //                CenterColor = Color.FromArgb(LUT[0][ConvertedValue], LUT[1][ConvertedValue], LUT[2][ConvertedValue]);
                //            }
                //            else CenterColor = Color.Transparent;
                //            SolidBrush Brush = new SolidBrush(CenterColor);

                //            Rectangle Rect = new Rectangle(PosX, PosY, CellSize, CellSize);
                //            formGraphics.FillRectangle(Brush, Rect);




                //            //System.Drawing.Pen myPen = new System.Drawing.Pen(BorderColor, WidthBorder);
                //            //formGraphics.DrawRectangle(myPen, Rect);
                //        }
                //}
                //else
                //{
                //    for (int j = 0; j < ValuesMatrix.Length; j++)
                //        for (int i = 0; i < ValuesMatrix[0].Length; i++)
                //        {
                //            int PosX = PosXMatrix + i * CellSize;
                //            int PosY = PosYMatrix + j * CellSize;
                //            cWell TmpWell = this.AssociatedPlate.GetWell(i, j, false);
                //            if (TmpWell == null) continue;

                //            double CurrentVal = TmpWell.ListDescriptors[this.CurrentScreening.ListDescriptors.CurrentSelectedDescriptor].GetValue();
                //            int ConvertedValue = (int)(((CurrentVal - LocalMin) * (LUT[0].Length - 1)) / (LocalMax - LocalMin));
                //            CenterColor = Color.FromArgb(LUT[0][ConvertedValue], LUT[1][ConvertedValue], LUT[2][ConvertedValue]);


                //            SolidBrush Brush = new SolidBrush(CenterColor);

                //            Rectangle Rect = new Rectangle(PosX, PosY, CellSize, CellSize);
                //            formGraphics.FillRectangle(Brush, Rect);

                //            //System.Drawing.Pen myPen = new System.Drawing.Pen(BorderColor, WidthBorder);
                //            //formGraphics.DrawRectangle(myPen, Rect);
                //        }



                //}

            }
            else
            {
                for (int j = 0; j < ValuesMatrix.Length; j++)
                    for (int i = 0; i < ValuesMatrix[0].Length; i++)
                    {
                        int PosX = PosXMatrix + i * CellSize;
                        int PosY = PosYMatrix + j * CellSize;
                        cWell TmpWell = this.AssociatedPlate.GetWell(i, j, false);
                        if (TmpWell == null) continue;

                        SolidBrush Brush = new SolidBrush(TmpWell.GetClassColor());

                        Rectangle Rect = new Rectangle(PosX, PosY, CellSize, CellSize);
                        formGraphics.FillRectangle(Brush, Rect);
                    }

            }

        



        }
    }

    public partial class FormToDisplayPlate : cPanelForDisplayArray
    {
     //   cScreening CompleteScreening;
        public cWindowToDisplayEntireScreening WindowToDisplayEntireScreening;
        cPlate PlateToDisplay;

        public FormToDisplayPlate(cPlate PlateToDisplay)
        {
            //this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            // this.WindowToDisplayEntireScreening = WindowToDisplayEntireScreening;
            this.PlateToDisplay = PlateToDisplay;

          //  this.CompleteScreening = CompleteScreening;
          //  this.CurrentScreening = CompleteScreening;
            this.LUT = cGlobalInfo.CurrentPlateLUT;
            this.AssociatedPlate = PlateToDisplay;
            base.Tag = PlateToDisplay;
            bool ResMiss = false;
            this.MouseDoubleClick += new MouseEventHandler(this.FormToDisplayPlate_MouseDoubleClick);
            this.MouseClick += new MouseEventHandler(this.FormToDisplayPlate_MouseClick);

            ValuesMatrix = PlateToDisplay.GetAverageValueDescTable1(cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx, out ResMiss);
            System.Windows.Forms.ToolTip ToolTip = new System.Windows.Forms.ToolTip();
            ToolTip.SetToolTip(this, PlateToDisplay.GetName() /*+ ". Replicate " + PlateToDisplay.AssociatedReplicateType.Name*/);
            ToolTip.Tag = PlateToDisplay;





            for (int j = 0; j < ValuesMatrix.Length; j++)
                for (int i = 0; i < ValuesMatrix[0].Length; i++)
                {
                    if (double.IsNaN(ValuesMatrix[j][i])) continue;

                    if (ValuesMatrix[j][i] > Max)
                        Max = ValuesMatrix[j][i];
                    if (ValuesMatrix[j][i] < Min)
                        Min = ValuesMatrix[j][i];
                }

            this.SetNewCellSize(5);
            //DisplayMatrix();

            // this.Update();
        }


        private void FormToDisplayPlate_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

                ToolStripMenuItem ToolStripMenuItem_RemovePlate = new ToolStripMenuItem("Remove Plate");
                ToolStripMenuItem_RemovePlate.Click += new System.EventHandler(this.RemovePlate);
                contextMenuStrip.Items.Add(ToolStripMenuItem_RemovePlate);

                ToolStripSeparator ToolStripSep = new ToolStripSeparator();
                contextMenuStrip.Items.Add(ToolStripSep);

                ToolStripMenuItem ToolStripMenuItem_ClusterPlate = new ToolStripMenuItem("Clustering");
                ToolStripMenuItem_ClusterPlate.Click += new System.EventHandler(this.ClusterPlate);
                contextMenuStrip.Items.Add(ToolStripMenuItem_ClusterPlate);

                contextMenuStrip.Show(Control.MousePosition);

            }
        }

        private void ClusterPlate(object sender, EventArgs e)
        {
            List<cPlate> ListPlate = new List<cPlate>();
            ListPlate.Add(this.PlateToDisplay);

            ((cWindowToDisplayEntireScreening)ParentWindow).PerformScreeningClustering(ListPlate);
        }

        private void FormToDisplayPlate_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int Idx = 0; Idx < cGlobalInfo.WindowHCSAnalyzer.toolStripcomboBoxPlateList.Items.Count; Idx++)
            {
                if (cGlobalInfo.WindowHCSAnalyzer.toolStripcomboBoxPlateList.Items[Idx].ToString() == this.AssociatedPlate.GetName())
                {
                    cGlobalInfo.WindowHCSAnalyzer.toolStripcomboBoxPlateList.SelectedIndex = Idx;
                    return;
                }
            }

        }

        private void RemovePlate(object sender, EventArgs e)
        {
            ((cWindowToDisplayEntireScreening)ParentWindow).RemovePlate(this.PlateToDisplay);
            //WindowToDisplayEntireScreening.RemovePlate(this.PlateToDisplay);
        }



    }

    public partial class FormToDisplayDescriptorPlate : cPanelForDisplayArray
    {
        cScreening CompleteScreening;
        // public cWindowToDisplayEntireScreening WindowToDisplayEntireScreening;
        cPlate PlateToDisplay;
        int IdxDesc;

        public FormToDisplayDescriptorPlate(cPlate PlateToDisplay, cScreening CompleteScreening, int DescIdx)
        {
            //this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            // this.WindowToDisplayEntireScreening = WindowToDisplayEntireScreening;
            this.PlateToDisplay = PlateToDisplay;

            this.IdxDesc = DescIdx;
            this.CompleteScreening = CompleteScreening;
            this.CurrentScreening = CompleteScreening;
            this.LUT = cGlobalInfo.CurrentPlateLUT;
            this.AssociatedPlate = PlateToDisplay;
            bool ResMiss = false;


            ValuesMatrix = PlateToDisplay.GetAverageValueDescTable1(DescIdx, out ResMiss);
            System.Windows.Forms.ToolTip ToolTip = new System.Windows.Forms.ToolTip();
            ToolTip.SetToolTip(this, CompleteScreening.ListDescriptors[DescIdx].GetName());
            this.MouseClick += new MouseEventHandler(this.FormToDisplayDescriptorPlate_MouseClick);

            for (int j = 0; j < ValuesMatrix.Length; j++)
                for (int i = 0; i < ValuesMatrix[0].Length; i++)
                {
                    if (double.IsNaN(ValuesMatrix[j][i])) continue;

                    if (ValuesMatrix[j][i] > Max)
                        Max = ValuesMatrix[j][i];
                    if (ValuesMatrix[j][i] < Min)
                        Min = ValuesMatrix[j][i];
                }

            this.SetNewCellSize(5);
            //DisplayMatrix();

            // this.Update();
        }


        private void FormToDisplayDescriptorPlate_MouseClick(object sender, EventArgs e)
        {
            PlateToDisplay.DisplayHistogram(IdxDesc);

        }

    }

    public partial class FormToDisplayDistanceMap : cPanelForDisplayArray
    {
        public void RecomputeDistances(eDistances Dist)
        {
            for (int j = 0; j < ValuesMatrix.Length; j++)
            {
                cWell SourceWell = this.AssociatedPlate.ListActiveWells[j];
                for (int i = j; i < ValuesMatrix[0].Length; i++)
                {
                    cWell DestinationWell = this.AssociatedPlate.ListActiveWells[i];
                    ValuesMatrix[i][j] = ValuesMatrix[j][i] = SourceWell.DistanceTo(DestinationWell, Dist);

                    //    cGlobalInfo.CurrentScreening.ListDescriptors[0].IsActive
                }
            }


            Max = double.MinValue;
            Min = double.MaxValue;

            for (int j = 0; j < ValuesMatrix.Length; j++)
                for (int i = 0; i < ValuesMatrix[0].Length; i++)
                {
                    if (ValuesMatrix[j][i] > Max)
                        Max = ValuesMatrix[j][i];
                    if (ValuesMatrix[j][i] < Min)
                        Min = ValuesMatrix[j][i];
                }
        }


        private void FormToDisplayDistanceMap_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                ToolStripMenuItem ToolStripMenuItem_CopyToClipboard = new ToolStripMenuItem("Copy to ClipBoard");
                //ToolStripSeparator ToolStripSep = new ToolStripSeparator();
                contextMenuStrip.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem_CopyToClipboard });
                contextMenuStrip.Show(Control.MousePosition);
                ToolStripMenuItem_CopyToClipboard.Click += new System.EventHandler(this.CopyToClipBoard);
            }
        }


        private void CopyToClipBoard(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            for (int j = 0; j < ValuesMatrix.Length; j++)
            {
                for (int i = 0; i < ValuesMatrix[0].Length; i++)
                    sb.Append(ValuesMatrix[j][i].ToString() + "\t");

                sb.AppendLine();
            }

            Clipboard.SetText(sb.ToString());
        }



        public FormToDisplayDistanceMap(cPlate CurrentPlate)
        {
         //   this.CurrentScreening = CompleteScreening;
            cLUT TmpLUT = new cLUT();
            this.LUT = TmpLUT.LUT_FIRE;
            this.AssociatedPlate = CurrentPlate;
            bool ResMiss = false;
            //this.MouseDoubleClick += new MouseEventHandler(this.FormToDisplayPlate_MouseDoubleClick);
            this.MouseClick += new MouseEventHandler(this.FormToDisplayDistanceMap_MouseClick);

            ValuesMatrix = new double[CurrentPlate.ListActiveWells.Count][];

            for (int i = 0; i < ValuesMatrix.Length; i++)
                ValuesMatrix[i] = new double[CurrentPlate.ListActiveWells.Count];

            RecomputeDistances(eDistances.EUCLIDEAN);

            this.SetNewCellSize(2);
        }
    }

}
