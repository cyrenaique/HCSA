using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes;
using weka.core;
using System.Windows.Forms.DataVisualization.Charting;
using HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Forms.FormsForGraphsDisplay
{
    public partial class cDisplayScatter2D : Form
    {
        private DataTable dt;
       // cGlobalInfo GlobalInfo;

        public cDisplayScatter2D(DataTable dt)
        {
            InitializeComponent();

            #region initialize histograms display
            splitContainerHorizontal.Panel1Collapsed = true;
            splitContainerVertical.Panel2Collapsed = true;

            System.Drawing.Image ImageOriginal = (System.Drawing.Image)(Properties.Resources.Arrow);
            if (splitContainerVertical.Panel2Collapsed)
                ImageOriginal.RotateFlip(RotateFlipType.Rotate270FlipNone);
            else
                ImageOriginal.RotateFlip(RotateFlipType.Rotate90FlipNone);

            buttonCollapseVertical.BackgroundImage = ImageOriginal;
            #endregion

            this.chartForPoints.BorderlineColor = Color.Black;
            this.dt = dt;
            this.dataGridViewForTable.DataSource = dt;
            //this.GlobalInfo = GlobalInfo;

  
        }

        public cDisplayScatter2D(List<cExtendedList> MyData)
        {
            InitializeComponent();

            #region initialize histograms display
            splitContainerHorizontal.Panel1Collapsed = true;
            splitContainerVertical.Panel2Collapsed = true;

            System.Drawing.Image ImageOriginal = (System.Drawing.Image)(Properties.Resources.Arrow);
            if (splitContainerVertical.Panel2Collapsed)
                ImageOriginal.RotateFlip(RotateFlipType.Rotate270FlipNone);
            else
                ImageOriginal.RotateFlip(RotateFlipType.Rotate90FlipNone);

            buttonCollapseVertical.BackgroundImage = ImageOriginal;
            #endregion

            this.chartForPoints.BorderlineColor = Color.Black;
           
            //this.GlobalInfo = null;


            this.dt = createDataTable(MyData);
            this.dataGridViewForTable.DataSource = dt;

            foreach (var item in MyData)
            {
                this.comboBoxAxeX.Items.Add(item.Name);
                this.comboBoxAxeY.Items.Add(item.Name);
                this.comboBoxVolume.Items.Add(item.Name);
            }

            this.comboBoxAxeX.SelectedIndex = 0;
            this.comboBoxAxeY.SelectedIndex = 0;
            this.comboBoxVolume.SelectedIndex = 0;

        }

        DataTable createDataTable(List<cExtendedList> MyData)
        {
            DataTable CurrentTable = new DataTable();

            int IdxCol = 0;
            foreach (cExtendedList item in MyData)
            {
                DataColumn NewCol = null;
                if (item.Name != null)
                    NewCol = new DataColumn(MyData[IdxCol++].Name, typeof(double));
                else
                    NewCol = new DataColumn("Column " + IdxCol++, typeof(double));

                CurrentTable.Columns.Add(NewCol);
            }

            for (int Idx = 0; Idx < MyData[0].Count; Idx++)
            {
                CurrentTable.Rows.Add();

                for (int IdxColumn = 0; IdxColumn < MyData.Count; IdxColumn++)
                {
                    CurrentTable.Rows[CurrentTable.Rows.Count - 1][IdxColumn] = MyData[IdxColumn][Idx];
                }
            }

            //DataGridView NewDataGridView = new DataGridView();
            //NewDataGridView.DataSource = CurrentTable;

           // FormToDisplayDataTable FDT = new FormToDisplayDataTable(CurrentTable, null);
           // FDT.Show();

            return CurrentTable;
        }


        private void ReDraw()
        {
            cExtendedList ListX = new cExtendedList();
            cExtendedList ListY = new cExtendedList();

            if (this.comboBoxAxeY.SelectedIndex == -1) return;
            if (this.comboBoxVolume.SelectedIndex == -1) return;

            cExtendedList ListVolumes = new cExtendedList();

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                ListX.Add(double.Parse(dt.Rows[j][this.comboBoxAxeX.SelectedIndex].ToString()));
                ListY.Add(double.Parse(dt.Rows[j][this.comboBoxAxeY.SelectedIndex].ToString()));
                ListVolumes.Add(double.Parse(dt.Rows[j][this.comboBoxVolume.SelectedIndex].ToString()));
            }

            this.chartForPoints.ChartAreas[0].AxisX.Title = this.comboBoxAxeX.SelectedItem.ToString();
            this.chartForPoints.ChartAreas[0].AxisY.Title = this.comboBoxAxeY.SelectedItem.ToString();
            this.chartForPoints.Series[0].Points.DataBindXY(ListX, ListY);

            this.chartForPoints.ChartAreas[0].AxisX.Minimum = ListX.Min();
            this.chartForPoints.ChartAreas[0].AxisX.Maximum = ListX.Max();

            this.chartForPoints.ChartAreas[0].AxisY.Minimum = ListY.Min();
            this.chartForPoints.ChartAreas[0].AxisY.Maximum = ListY.Max();

            if (!checkBoxIsVolumeConstant.Checked)
            {
                double MaxVolume = ListVolumes.Max();
                double MinVolume = ListVolumes.Min();

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    int MarkerArea = (int)((45* (ListVolumes[j] - MinVolume)) / (MaxVolume - MinVolume))+3;
                    this.chartForPoints.Series[0].Points[j].MarkerSize = MarkerArea;
                   Color C = this.chartForPoints.Series[0].Points[j].Color;// = 
                }
            }

            for (int j = 0; j < this.chartForPoints.Series[0].Points.Count; j++)
            {
                //int ConvertedValue = (int)(((Classes[j] - 0) * (LUT[0].Length - 1)) / (eval.getNumClusters() - 0));
                //  this.chartForPoints.Series[0].Points[j].MarkerColor = GlobalInfo.ListCellularPhenotypes[(int)MachineLearning.Classes[j]].ColourForDisplay;
                this.chartForPoints.Series[0].Points[j].MarkerColor = Color.FromArgb(128, Color.OrangeRed );
            }

            if (checkBoxIsVolumeConstant.Checked)
            {
                for (int j = 0; j < this.chartForPoints.Series[0].Points.Count; j++)
                    this.chartForPoints.Series[0].Points[j].MarkerSize = 10;
            }


            for (int j = 0; j < dt.Rows.Count; j++)
                this.chartForPoints.Series[0].Points[j].Tag = dataGridViewForTable.Rows[j];//dt.Rows[j];

            this.chartForPoints.ChartAreas[0].AxisX.LabelStyle.Format = "N2";
            this.chartForPoints.ChartAreas[0].AxisY.LabelStyle.Format = "N2";

        }

        private void comboBoxAxeX_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReDraw();
            if (splitContainerVertical.Panel2Collapsed) return;
            RedrawHistoHorizontal();
        }

        private void comboBoxAxeY_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReDraw();
            RedrawHistoVertical();
        }

        private void RedrawHistoVertical()
        {
            cExtendedList ListValue = new cExtendedList();
            for (int Idx = 0; Idx < this.dt.Rows.Count; Idx++)
                ListValue.Add(double.Parse(this.dt.Rows[Idx][comboBoxAxeY.SelectedIndex].ToString()));

            cPanelHisto PanelHisto = new cPanelHisto(ListValue, eGraphType.HISTOGRAM, eOrientation.VERTICAL);
            PanelHisto.WindowForPanelHisto.panelForGraphContainer.BackColor = Color.White;
            PanelHisto.WindowForPanelHisto.panelForGraphContainer.Size = new System.Drawing.Size(splitContainerHorizontal.Panel1.Width, splitContainerVertical.Panel1.Height);
            PanelHisto.WindowForPanelHisto.panelForGraphContainer.Location = new Point(0, 0);

            PanelHisto.WindowForPanelHisto.panelForGraphContainer.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            PanelHisto.WindowForPanelHisto.panelForGraphContainer.BorderStyle = BorderStyle.None;

            PanelHisto.WindowForPanelHisto.CurrentChartArea.AxisX.Minimum = chartForPoints.ChartAreas[0].AxisY.Minimum;
            PanelHisto.WindowForPanelHisto.CurrentChartArea.AxisX.Maximum = chartForPoints.ChartAreas[0].AxisY.Maximum;

            splitContainerHorizontal.Panel1.Controls.Clear();
            splitContainerHorizontal.Panel1.Controls.Add(PanelHisto.WindowForPanelHisto.panelForGraphContainer);

        }

        private void RedrawHistoHorizontal()
        {
            cExtendedList ListValue = new cExtendedList();
            for (int Idx = 0; Idx < this.dt.Rows.Count; Idx++)
                ListValue.Add(double.Parse(this.dt.Rows[Idx][comboBoxAxeX.SelectedIndex].ToString()));

            cPanelHisto PanelHisto = new cPanelHisto(ListValue, eGraphType.HISTOGRAM, eOrientation.HORIZONTAL);
            PanelHisto.WindowForPanelHisto.panelForGraphContainer.Size = new System.Drawing.Size(splitContainerVertical.Panel2.Width - 23, splitContainerVertical.Panel2.Height);
            PanelHisto.WindowForPanelHisto.panelForGraphContainer.BackColor = Color.White;
            PanelHisto.WindowForPanelHisto.panelForGraphContainer.Location = new Point(23, 0);

            PanelHisto.WindowForPanelHisto.panelForGraphContainer.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            PanelHisto.WindowForPanelHisto.panelForGraphContainer.BorderStyle = BorderStyle.None;

            PanelHisto.WindowForPanelHisto.CurrentChartArea.AxisX.Minimum = chartForPoints.ChartAreas[0].AxisX.Minimum;
            PanelHisto.WindowForPanelHisto.CurrentChartArea.AxisX.Maximum = chartForPoints.ChartAreas[0].AxisX.Maximum;

            splitContainerVertical.Panel2.Controls.Clear();
            splitContainerVertical.Panel2.Controls.Add(PanelHisto.WindowForPanelHisto.panelForGraphContainer);
        }

        private void comboBoxVolume_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReDraw();
        }

        //private void showClassificationTreeToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (GlobalInfo.CurrentScreen.CellBasedClassification.J48Model == null) return;
        //    GlobalInfo.CurrentScreen.CellBasedClassification.DisplayTree(GlobalInfo).Show();
        //}

        //private void applyClassificationModelToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    return;
            
        //    Instances ListInstancesTOClassify = GlobalInfo.CurrentScreen.CellBasedClassification.CreateInstancesWithoutClass(dt);

        //    FastVector attVals = new FastVector();
        //    for (int i = 0; i  <  GlobalInfo.CurrentScreen.CellBasedClassification.NumClasses; i++)
        //        attVals.addElement(i.ToString());

        //    ListInstancesTOClassify.insertAttributeAt(new weka.core.Attribute("Class", attVals), ListInstancesTOClassify.numAttributes());
        //    ListInstancesTOClassify.setClassIndex(ListInstancesTOClassify.numAttributes() - 1);

        //    List<int> ListIdx = new List<int>();
        //    int Max = int.MinValue;
        //    int Min = int.MaxValue;

        //    for (int i = 0; i < ListInstancesTOClassify.numInstances(); i++)
        //    {
        //        Instance InstToProcess = ListInstancesTOClassify.instance(i);
        //       int Value =(int)GlobalInfo.CurrentScreen.CellBasedClassification.J48Model.classifyInstance(InstToProcess);
        //       if (Value > Max) Max = Value;
        //       if (Value < Min) Min = Value;

        //       ListIdx.Add(Value);
        //    }

        //    byte[][] LUT = GlobalInfo.LUT;
   
        //        for (int j = 0; j < this.chartForPoints.Series[0].Points.Count; j++)
        //        {
        //            int ConvertedValue = (int)(((ListIdx[j] - Min) * (LUT[0].Length - 1)) / (Max - Min));
        //            this.chartForPoints.Series[0].Points[j].MarkerColor = Color.FromArgb(LUT[0][ConvertedValue], LUT[1][ConvertedValue], LUT[2][ConvertedValue]);
        //        }
        //}

        private void checkBoxIsVolumeConstant_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxVolume.Enabled = !checkBoxIsVolumeConstant.Checked;
            ReDraw();
        }

        private void chartForPoints_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HitTestResult Res = this.chartForPoints.HitTest(e.X, e.Y, ChartElementType.DataPoint);
            if (Res.Series == null) return;

            DataPoint PtToTransfer = Res.Series.Points[Res.PointIndex];

            DataGridViewRow DR = (DataGridViewRow)Res.Series.Points[Res.PointIndex].Tag;
            if (DR.Index == -1) return;
            dataGridViewForTable.CurrentRow.Selected = false;
           // dataGridViewForTable.SelectedRows.SelectedRows.Clear();
            DR.Selected = true;

            dataGridViewForTable.FirstDisplayedScrollingRowIndex = DR.Index;
            

        //   
            //DR.

            /*cWell WellToTransfer = (cWell)(PtToTransfer.Tag);
            if (WellToTransfer == null) return;

            List<ToolStripItem> ListNewItemForClass = new List<ToolStripItem>();
            for (int i = 0; i < CompleteScreening.GlobalInfo.GetNumberofDefinedClass(); i++)
            {
                ToolStripItem ChangeClassItem = new ToolStripMenuItem("Class " + i);
                if (i == WellToTransfer.GetClass()) ChangeClassItem.ForeColor = Color.Gray;
                ChangeClassItem.Click += new System.EventHandler(this.ChangeClass);
                ListNewItemForClass.Add(ChangeClassItem);
            }
            if (Res.Series.Points[Res.PointIndex].Tag.GetType().Name.ToString() != "cWell") return;

            WellToTransfer.BuildAndisplaySimpleContextMenu(ListNewItemForClass.ToArray());
            */
        }

        private void dataGridViewForTable_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.ColumnIndex == -1) || (e.RowIndex == -1)) return;
        }

        private void buttonCollapseVertical_Click(object sender, EventArgs e)
        {
            splitContainerVertical.Panel2Collapsed = !splitContainerVertical.Panel2Collapsed;

            if ((!splitContainerVertical.Panel2Collapsed) && (splitContainerVertical.Panel2.Controls.Count == 0))
            {
                RedrawHistoHorizontal();
            }

            System.Drawing.Image ImageOriginal = (System.Drawing.Image)(Properties.Resources.Arrow);
            if (splitContainerVertical.Panel2Collapsed)
            {
                ImageOriginal.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            else
            {
                ImageOriginal.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }  
            buttonCollapseVertical.BackgroundImage = ImageOriginal;
        }

        private void buttonCollapseHorizontal_Click(object sender, EventArgs e)
        {
            splitContainerHorizontal.Panel1Collapsed = !splitContainerHorizontal.Panel1Collapsed;
            System.Drawing.Image ImageOriginal = (System.Drawing.Image)(Properties.Resources.Arrow);
            if (splitContainerHorizontal.Panel1Collapsed)
            {
               // ImageOriginal.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            else
            {
                ImageOriginal.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            buttonCollapseHorizontal.BackgroundImage = ImageOriginal;
        }

        private void chartForPoints_Resize(object sender, EventArgs e)
        {
            if (splitContainerHorizontal.Panel1.Controls.Count == 0) return;
            splitContainerHorizontal.Panel1.Controls[0].Size = new System.Drawing.Size(splitContainerHorizontal.Panel1.Width, splitContainerVertical.Panel1.Height);
        }
    }
}
