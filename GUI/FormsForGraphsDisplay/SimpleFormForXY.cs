using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LibPlateAnalysis
{
    public partial class SimpleFormForXY : Form
    {
        public cScreening CompleteScreening = null;
        FormForMaxMinRequest RequestWindow = new FormForMaxMinRequest();
        private bool IsFullScreen;

        public SimpleFormForXY(bool IsFullScreen)
        {
            InitializeComponent();
            this.IsFullScreen = IsFullScreen;
        }

        private void saveGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveFileDialog CurrSaveFileDialog = new SaveFileDialog();
            CurrSaveFileDialog.Filter = "PNG files (*.png)|*.png";
            System.Windows.Forms.DialogResult Res = CurrSaveFileDialog.ShowDialog();
            if (Res != System.Windows.Forms.DialogResult.OK) return;

            string CurrentPath = CurrSaveFileDialog.FileName;
            this.chartForSimpleFormXY.SaveImage(CurrentPath, ChartImageFormat.Png);

            MessageBox.Show("File saved !");
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            this.chartForSimpleFormXY.SaveImage(ms, ChartImageFormat.Bmp);
            Bitmap bm = new Bitmap(ms);
            Clipboard.SetImage(bm);
        }

        public string GetValues()
        {

            StringBuilder sb = new StringBuilder();

            if (this.chartForSimpleFormXY.Titles.Count != 0)
                sb.Append(this.chartForSimpleFormXY.Titles[0].Text + "\n");
            sb.Append("Axis X: " + this.chartForSimpleFormXY.ChartAreas[0].AxisX.Title + "\n");
            sb.Append("Axis Y: " + this.chartForSimpleFormXY.ChartAreas[0].AxisY.Title + "\n");


            if (this.chartForSimpleFormXY.Series[0].Name.ToString() == "Matrix")
            {
                sb.Append("\t");

                for (int X = 0; X < this.chartForSimpleFormXY.ChartAreas[0].AxisX.CustomLabels.Count; X++)
                    sb.Append(this.chartForSimpleFormXY.ChartAreas[0].AxisX.CustomLabels[X].Text + "\t");

                sb.Append("\n");
                for (int Y = 0; Y < this.chartForSimpleFormXY.ChartAreas[0].AxisY.CustomLabels.Count; Y++)
                {
                    sb.Append(this.chartForSimpleFormXY.ChartAreas[0].AxisY.CustomLabels[Y].Text + "\t");

                    for (int X = 0; X < this.chartForSimpleFormXY.ChartAreas[0].AxisX.CustomLabels.Count; X++)
                        sb.Append(this.chartForSimpleFormXY.Series[0].Points[X + Y * this.chartForSimpleFormXY.ChartAreas[0].AxisX.CustomLabels.Count].ToolTip + "\t");

                    sb.Append("\n");
                }
            }
            else
            {
                //sb.Append("\t");
                for (int Serie = 0; Serie < this.chartForSimpleFormXY.Series.Count; Serie++)
                {
                    sb.Append(this.chartForSimpleFormXY.Series[Serie].Label + " X values\t");
                    if (this.chartForSimpleFormXY.ChartAreas[0].AxisX.CustomLabels.Count > 0)
                    {
                        for (int X = 0; X < this.chartForSimpleFormXY.ChartAreas[0].AxisX.CustomLabels.Count; X++)
                            sb.Append(this.chartForSimpleFormXY.ChartAreas[0].AxisX.CustomLabels[X].Text.Replace("\n", " ") + "\t");

                    }
                    else
                    {

                        for (int i = 0; i < this.chartForSimpleFormXY.Series[Serie].Points.Count; i++)
                            sb.Append(String.Format("{0}\t", this.chartForSimpleFormXY.Series[Serie].Points[i].XValue));
                    }
                    sb.Append("\n");

                    sb.Append(this.chartForSimpleFormXY.Series[Serie].Label + " Y Values\t");
                    for (int i = 0; i < this.chartForSimpleFormXY.Series[Serie].Points.Count; i++)
                        sb.Append(String.Format("{0}\t", this.chartForSimpleFormXY.Series[Serie].Points[i].YValues[0]));
                }
            }
            return sb.ToString();
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GetValues());
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chartForSimpleFormXY.Printing.PageSetup();
            this.chartForSimpleFormXY.Printing.PrintPreview();
            this.chartForSimpleFormXY.Printing.Print(false);
        }

        private void comboBoxDescriptorX_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayXY();
        }

        private void comboBoxDescriptorY_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayXY();
        }

        Series CurrentSeries;
        ChartArea CurrentChartArea = new ChartArea();

        public bool DisplayXY()
        {
            if (CompleteScreening == null) return false;

            int DescX = this.comboBoxDescriptorX.SelectedIndex;
            int DescY = this.comboBoxDescriptorY.SelectedIndex;

            if (DescX < 0) DescX = 0;
            if (DescY < 0) DescY = 0;

            CurrentSeries = new Series("ScatterPoints");
            CurrentSeries.ShadowOffset = 1;

            double MinX = double.MaxValue;
            double MinY = double.MaxValue;
            double MaxX = double.MinValue;
            double MaxY = double.MinValue;

            double TempX, TempY;
            int Idx = 0;

            cListPlates ListPlate = new cListPlates();
            if (!IsFullScreen)
                ListPlate.Add(CompleteScreening.GetCurrentDisplayPlate());
            else
                ListPlate = CompleteScreening.ListPlatesActive;

            for (int i = 0; i < ListPlate.Count; i++)
            {            
                cPlate CurrentPlate = ListPlate[i];
                for (int IdxValue = 0; IdxValue < CompleteScreening.Columns; IdxValue++)
                    for (int IdxValue0 = 0; IdxValue0 < CompleteScreening.Rows; IdxValue0++)
                    {
                        cWell TmpWell = CurrentPlate.GetWell(IdxValue, IdxValue0, true);
                        if (TmpWell != null)
                        {

                            TempX = TmpWell.ListSignatures[DescX].GetValue();
                            if (TempX < MinX) MinX = TempX;
                            if (TempX > MaxX) MaxX = TempX;


                            TempY = TmpWell.ListSignatures[DescY].GetValue();
                            if (TempY < MinY) MinY = TempY;
                            if (TempY > MaxY) MaxY = TempY;


                            CurrentSeries.Points.AddXY(TempX, TempY);
                            CurrentSeries.Points[Idx].Color = TmpWell.GetClassColor();

                            if (IsFullScreen)
                                CurrentSeries.Points[Idx].ToolTip = TmpWell.AssociatedPlate.GetName() + "\n" + TmpWell.GetPosX() + "x" + TmpWell.GetPosY();
                            else
                                CurrentSeries.Points[Idx].ToolTip = TmpWell.GetPosX() + "x" + TmpWell.GetPosY();
                            CurrentSeries.Points[Idx].Tag = TmpWell;
                            CurrentSeries.Points[Idx].MarkerStyle = MarkerStyle.Circle;
                            CurrentSeries.Points[Idx].MarkerSize = 8;
                            Idx++;
                        }
                    }
            }

            if (CurrentSeries.Points.Count < 2)
            {
                MessageBox.Show("Statistical Analyses - More than one data point needed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

           
            
            CurrentChartArea.CursorX.IsUserSelectionEnabled = true;
            CurrentChartArea.CursorY.IsUserSelectionEnabled = true;
            CurrentChartArea.BorderColor = Color.Black;
            this.chartForSimpleFormXY.ChartAreas.Clear();
            this.chartForSimpleFormXY.ChartAreas.Add(CurrentChartArea);

            this.chartForSimpleFormXY.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            CurrentChartArea.BackColor = Color.FromArgb(164, 164, 164);


            CurrentChartArea.Axes[0].Title = CompleteScreening.ListDescriptors[DescX].GetName();
            CurrentChartArea.Axes[0].Minimum = MinX;
            CurrentChartArea.Axes[0].Maximum = MaxX;

            CurrentChartArea.Axes[1].Title = CompleteScreening.ListDescriptors[DescY].GetName();
            CurrentChartArea.Axes[1].Minimum = MinY;
            CurrentChartArea.Axes[1].Maximum = MaxY;

            CurrentChartArea.AxisX.LabelStyle.Format = "N2";
            CurrentChartArea.AxisY.LabelStyle.Format = "N2";
            CurrentSeries.ChartType = SeriesChartType.FastPoint;

            this.chartForSimpleFormXY.Series.Clear();
            this.chartForSimpleFormXY.Series.Add(CurrentSeries);

            this.Text = "Scatter Point / " + Idx + " points";

            //  this.chartForSimpleFormXY.GetToolTipText += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs>(this.chartForSimpleFormXY_GetToolTipText);


            this.chartForSimpleFormXY.Update();
            return true;
        }







        private void chartForSimpleFormXY_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HitTestResult Res = this.chartForSimpleFormXY.HitTest(e.X, e.Y, ChartElementType.DataPoint);

            if ((Res.Series == null) || (Res.Series.Points[Res.PointIndex].Tag.GetType().Name.ToString() != "cWell")) return;

            cWell TmpWell = (cWell)(Res.Series.Points[Res.PointIndex].Tag);
            if (TmpWell == null) return;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                //CompleteScreening.GlobalInfo.WindowHCSAnalyzer.tabControlMain.SelectedTab = CompleteScreening.GlobalInfo.WindowHCSAnalyzer.tabPageDistribution;
                int PosPlate = cGlobalInfo.WindowHCSAnalyzer.toolStripcomboBoxPlateList.FindStringExact(TmpWell.AssociatedPlate.GetName());
                cGlobalInfo.WindowHCSAnalyzer.toolStripcomboBoxPlateList.SelectedIndex = PosPlate;
                CompleteScreening.CurrentDisplayPlateIdx = PosPlate;
                CompleteScreening.GetCurrentDisplayPlate().DisplayDistribution(CompleteScreening.ListDescriptors.GetActiveDescriptor(), false);
                TmpWell.DisplayInfoWindow(CompleteScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
            }
        }

        static DataPoint PtToTransfer;

        void ChangeClass(object sender, EventArgs e)
        {
            cWell WellToTransfer = (cWell)(PtToTransfer.Tag);
            if (WellToTransfer == null) return;
            WellToTransfer.SetClass(int.Parse(sender.ToString().Remove(0, 6)));
            WellToTransfer.AssociatedPlate.UpdateNumberOfClass();
            PtToTransfer.Color = WellToTransfer.GetClassColor();
        }

        private void parametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.chartForSimpleFormXY.Series[0].Points.Count >= 1)
                RequestWindow.numericUpDownMarkerSize.Value = (decimal)this.chartForSimpleFormXY.Series[0].Points[0].MarkerSize;


            RequestWindow.numericUpDownMax.Value = (decimal)this.chartForSimpleFormXY.ChartAreas[0].AxisY.Maximum;
            RequestWindow.numericUpDownMin.Value = (decimal)this.chartForSimpleFormXY.ChartAreas[0].AxisY.Minimum;

            if (RequestWindow.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            if (RequestWindow.numericUpDownMax.Value <= RequestWindow.numericUpDownMin.Value) return;

            this.chartForSimpleFormXY.ChartAreas[0].AxisY.Maximum = (double)RequestWindow.numericUpDownMax.Value;
            this.chartForSimpleFormXY.ChartAreas[0].AxisY.Minimum = (double)RequestWindow.numericUpDownMin.Value;
            foreach (DataPoint Pt in this.chartForSimpleFormXY.Series[0].Points)
                Pt.MarkerSize = (int)RequestWindow.numericUpDownMarkerSize.Value;
        }

        private void chartForSimpleFormXY_MouseClick_1(object sender, MouseEventArgs e)
        {
            if ((e.Button != System.Windows.Forms.MouseButtons.Right) || (CompleteScreening == null)) return;

            HitTestResult Res = this.chartForSimpleFormXY.HitTest(e.X, e.Y, ChartElementType.DataPoint);
            if (Res.Series == null) return;

            PtToTransfer = Res.Series.Points[Res.PointIndex];
            cWell WellToTransfer = (cWell)(PtToTransfer.Tag);
            if (WellToTransfer == null) return;

            List<ToolStripItem> ListNewItemForClass = new List<ToolStripItem>();
            for (int i = 0; i < cGlobalInfo.ListWellClasses.Count; i++)
            {
                ToolStripItem ChangeClassItem = new ToolStripMenuItem("Class " + i);
                if (i == WellToTransfer.GetCurrentClassIdx()) ChangeClassItem.ForeColor = Color.Gray;
                ChangeClassItem.Click += new System.EventHandler(this.ChangeClass);
                ListNewItemForClass.Add(ChangeClassItem);
            }
            if (Res.Series.Points[Res.PointIndex].Tag.GetType().Name.ToString() != "cWell") return;

            WellToTransfer.BuildAndisplaySimpleContextMenu(ListNewItemForClass.ToArray());
        }

        void DisplayValuesChart(object sender, EventArgs e)
        {
            cWell WellToTransfer = (cWell)(PtToTransfer.Tag);
            if (WellToTransfer == null) return;
            WellToTransfer.DisplayInfoWindow(CompleteScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
        }

        
    }
}
