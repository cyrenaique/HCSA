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
using HCSAnalyzer.Classes.DRC_Analysis.FormsForDRCAnalysis;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Globalization;
using HCSAnalyzer.Classes.Base_Classes.Data;

namespace HCSAnalyzer.Forms
{
    public partial class FormForDRCDesign : Form
    {
        public FormForDRCDesign()
        {
            InitializeComponent();
            // UpDateDisplay();
        }

        public List<cWell> ListWells = null;

        private cDRC_Region TemplateRegion = null;

        int SizeWell = 8;
        SolidBrush CurrBrush;
        Graphics g;

        int PosXMin;
        int PosYMin;
        int PosXMax;
        int PosYMax;

        private void buttonApply_Click(object sender, EventArgs e)
        {
            FormForDRCDesignValidation FFDRCVal = new FormForDRCDesignValidation();

            PanelForPlatesSelection PanelForPlates = new PanelForPlatesSelection(true, null, false);
            PanelForPlates.Width = FFDRCVal.panelForPlatesName.Width;
            PanelForPlates.Height = FFDRCVal.panelForPlatesName.Height;
            FFDRCVal.panelForPlatesName.Controls.Add(PanelForPlates);

            if (!this.radioButtonConcentrationsManual.Checked) FFDRCVal.label.Text = "";

            if (FFDRCVal.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            this.Visible = false;

            //cScreening CurrentScreen = cGlobalInfo.CurrentScreening;
            int IdxGroup = 0;

            foreach (cPlate TmpPlate in PanelForPlates.GetListSelectedPlates())
            {
                TmpPlate.ListDRCRegions = new cListDRCRegion();

                int SizeX, SizeY;
                if (TemplateRegion.IsConcentrationHorizontal)
                {
                    SizeX = TemplateRegion.NumConcentrations;
                    SizeY = TemplateRegion.NumReplicate;
                }
                else
                {
                    SizeX = TemplateRegion.NumReplicate;
                    SizeY = TemplateRegion.NumConcentrations;
                }

                int NumRepeatX = (AssociatedPlate.ParentScreening.Columns - (TemplateRegion.PosXMin - 1)) / SizeX;
                int NumRepeatY = (AssociatedPlate.ParentScreening.Rows - (TemplateRegion.PosYMin - 1)) / SizeY;



                if (!this.radioButtonOrientationColumn.Checked)
                {
                    for (int j = 0; j < NumRepeatY; j++)
                        for (int i = 0; i < NumRepeatX; i++)
                        {
                            cDRC_Region TempRegion = new cDRC_Region(TmpPlate, (int)this.numericUpDownConcentrationNumber.Value, (int)this.numericUpDownReplication.Value, i * SizeX + (TemplateRegion.PosXMin - 1), j * SizeY + (TemplateRegion.PosYMin - 1), true);
                            TmpPlate.ListDRCRegions.AddNewRegion(TempRegion);

                            // Update the concentration from the manual entry

                            for (int Replicate = 0; Replicate < TempRegion.NumReplicate; Replicate++)
                            {
                                cWell[] TmpList = TempRegion.GetlistReplicate(Replicate);
                                for (int IdxConc = 0; IdxConc < TmpList.Length; IdxConc++)
                                {

                                    if ((this.radioButtonConcentrationsManual.Checked) && (TmpList[IdxConc] != null))
                                        TmpList[IdxConc].ListProperties.UpdateValueByName("Concentration", (double)Convert.ToDouble(dataGridViewForConcentration.Rows[IdxConc].Cells[1].Value.ToString(), CultureInfo.CreateSpecificCulture("en-US")));


                                    if ((FFDRCVal.checkBoxUpdateGroupID.Checked) && (TmpList[IdxConc] != null))
                                        TmpList[IdxConc].ListProperties.UpdateValueByName("Group", (int)IdxGroup);

                                }
                            }

                            IdxGroup++;

                        }
                }
                else
                {
                    for (int j = 0; j < NumRepeatY; j++)
                        for (int i = 0; i < NumRepeatX; i++)
                        {
                            cDRC_Region TempRegion = new cDRC_Region(TmpPlate, (int)this.numericUpDownConcentrationNumber.Value, (int)this.numericUpDownReplication.Value, i * SizeX + (TemplateRegion.PosXMin - 1), j * SizeY + (TemplateRegion.PosYMin - 1), false);
                            TmpPlate.ListDRCRegions.AddNewRegion(TempRegion);

                            // Update the concentration from the manual entry
                            //   if (this.radioButtonConcentrationsManual.Checked)
                            {
                                for (int Replicate = 0; Replicate < TempRegion.NumReplicate; Replicate++)
                                {
                                    cWell[] TmpList = TempRegion.GetlistReplicate(Replicate);
                                    for (int IdxConc = 0; IdxConc < TmpList.Length; IdxConc++)
                                    {
                                        if ((this.radioButtonConcentrationsManual.Checked) && (TmpList[IdxConc] != null))
                                            TmpList[IdxConc].ListProperties.UpdateValueByName("Concentration", Convert.ToDouble(dataGridViewForConcentration.Rows[IdxConc].Cells[1].Value.ToString()));

                                        if ((FFDRCVal.checkBoxUpdateGroupID.Checked) && (TmpList[IdxConc] != null))
                                            TmpList[IdxConc].ListProperties.UpdateValueByName("Group", (int)IdxGroup);
                                    }
                                }
                            }
                            IdxGroup++;
                        }
                }
            }


            //cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().Refresh3D(cGlobalInfo.CurrentScreening.ListDescriptors.CurrentSelectedDescriptorIdx);
            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
        }

        private void UpDateDisplayConcentration()
        {
            if (TemplateRegion == null) return;

            if (radioButtonConcentrationFromThePlate.Checked) dataGridViewForConcentration.Enabled = false;
            else dataGridViewForConcentration.Enabled = true;


            dataGridViewForConcentration.Columns.Clear();

            dataGridViewForConcentration.Columns.Add("Idx", "Idx");
            dataGridViewForConcentration.Columns.Add("Concentration", "Concentration");
            dataGridViewForConcentration.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            for (int i = 1; i <= (int)numericUpDownConcentrationNumber.Value; i++)
            {
                int IdxPosCol = 0;
                dataGridViewForConcentration.Rows.Add();
                dataGridViewForConcentration.Rows[i - 1].Cells[IdxPosCol++].Value = i;


                if (radioButtonConcentrationFromThePlate.Checked)
                {
                    object ConcentrationVal = null;
                    if (ListWells[i - 1] != null)
                        ListWells[i - 1].ListProperties.FindValueByName("Concentration");

                    if (ConcentrationVal != null)
                        dataGridViewForConcentration.Rows[i - 1].Cells[IdxPosCol++].Value = (double)ConcentrationVal;
                    else
                        dataGridViewForConcentration.Rows[i - 1].Cells[IdxPosCol++].Value = 0;
                }
                else
                {
                    if (i == 1)
                        dataGridViewForConcentration.Rows[i - 1].Cells[IdxPosCol++].Value = (int)this.numericUpDownConcentrationStartingValue.Value;
                    else
                        dataGridViewForConcentration.Rows[i - 1].Cells[IdxPosCol].Value = Convert.ToDouble(dataGridViewForConcentration.Rows[i - 2].Cells[IdxPosCol++].Value.ToString()) / (double)this.numericUpDownDilutionFactor.Value;

                }
            }

            dataGridViewForConcentration.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewForConcentration.Update();
        }

        private void DrawSingleRegion(int Idx, int StartX, int StartY)
        {
            int ColorStep = 256 / (int)this.numericUpDownConcentrationNumber.Value;
            int Max = this.panelForDesignDisplay.Width / AssociatedPlate.ParentScreening.Columns;

            if (this.radioButtonOrientationLine.Checked)
            {
                for (int i = PosXMin; i <= PosXMax; i++)
                {
                    Rectangle CurrentRect = new Rectangle((i + StartX) * SizeWell, (PosYMin + StartY) * SizeWell, SizeWell, (int)this.numericUpDownReplication.Value * SizeWell);
                    CurrBrush.Color = Color.FromArgb((i - PosXMin) * ColorStep, 0, Idx);
                    g.FillRectangle(CurrBrush, CurrentRect);
                }
            }
            else
            {
                for (int i = PosYMin; i <= PosYMax; i++)
                {
                    Rectangle CurrentRect = new Rectangle((StartX + PosXMin) * SizeWell, (i + StartY) * SizeWell, (int)this.numericUpDownReplication.Value * SizeWell, SizeWell);
                    CurrBrush.Color = Color.FromArgb((i - PosYMin) * ColorStep, 0, Idx);
                    g.FillRectangle(CurrBrush, CurrentRect);
                }
            }
        }

        cPlate AssociatedPlate = null;
        public void DrawSignature()
        {
            if (ListWells == null) return;

            g = this.panelForDesignDisplay.CreateGraphics();

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.White);

            int SizeWell = 8;
            CurrBrush = new SolidBrush(Color.Fuchsia);

            PosXMin = int.MaxValue;
            PosYMin = int.MaxValue;
            PosXMax = int.MinValue;
            PosYMax = int.MinValue;


            foreach (cWell TmpWell in ListWells)
            {
                if (TmpWell == null) continue;

                AssociatedPlate = TmpWell.AssociatedPlate;
                if (TmpWell.GetPosX() > PosXMax) PosXMax = TmpWell.GetPosX();
                if (TmpWell.GetPosY() > PosYMax) PosYMax = TmpWell.GetPosY();

                if (TmpWell.GetPosX() < PosXMin) PosXMin = TmpWell.GetPosX();
                if (TmpWell.GetPosY() < PosYMin) PosYMin = TmpWell.GetPosY();
            }

            if (AssociatedPlate == null) return;
            int SizeX = PosXMax - PosXMin + 1;
            int SizeY = PosYMax - PosYMin + 1;

            if (this.radioButtonOrientationLine.Checked)
            {
                this.numericUpDownConcentrationNumber.Value = (decimal)SizeX;
                this.numericUpDownReplication.Value = (decimal)SizeY;
                if (PosXMin > PosXMax) return;

                TemplateRegion = new cDRC_Region(AssociatedPlate, (int)this.numericUpDownConcentrationNumber.Value, (int)this.numericUpDownReplication.Value, PosXMin, PosYMin, true);
            }
            else
            {
                this.numericUpDownConcentrationNumber.Value = (decimal)SizeY;
                this.numericUpDownReplication.Value = (decimal)SizeX;
                if (PosXMin > PosXMax) return;
                TemplateRegion = new cDRC_Region(AssociatedPlate, (int)this.numericUpDownConcentrationNumber.Value, (int)this.numericUpDownReplication.Value, PosXMin, PosYMin, false);
            }
            DrawSingleRegion(0, 0, 0);

            for (int j = 1; j <= AssociatedPlate.ParentScreening.Columns + 1; j++)
                g.DrawLine(new Pen(Color.Black), new Point(j * SizeWell, SizeWell), new Point(j * SizeWell, (AssociatedPlate.ParentScreening.Rows + 1) * SizeWell));
            for (int i = 1; i <= AssociatedPlate.ParentScreening.Rows + 1; i++)
                g.DrawLine(new Pen(Color.Black), new Point(SizeWell, i * SizeWell), new Point((AssociatedPlate.ParentScreening.Columns + 1) * SizeWell, i * SizeWell));
        }

        private void buttonFill_Click(object sender, EventArgs e)
        {
            if (TemplateRegion == null) return;

            int SizeX, SizeY;
            if (TemplateRegion.IsConcentrationHorizontal)
            {
                SizeX = TemplateRegion.NumConcentrations;
                SizeY = TemplateRegion.NumReplicate;
            }
            else
            {
                SizeX = TemplateRegion.NumReplicate;
                SizeY = TemplateRegion.NumConcentrations;
            }

            int NumRepeatX = (AssociatedPlate.ParentScreening.Columns - (TemplateRegion.PosXMin - 1)) / SizeX;
            int NumRepeatY = (AssociatedPlate.ParentScreening.Rows - (TemplateRegion.PosYMin - 1)) / SizeY;

            int SecondColorStep = 256 / (NumRepeatX * NumRepeatY);

            if (!this.radioButtonOrientationColumn.Checked)
            {
                for (int j = 0; j < NumRepeatY; j++)
                    for (int i = 0; i < NumRepeatX; i++)
                        DrawSingleRegion((i + j * NumRepeatX) * SecondColorStep, i * SizeX, j * SizeY);
            }
            else
            {
                for (int j = 0; j < NumRepeatY; j++)
                    for (int i = 0; i < NumRepeatX; i++)
                        DrawSingleRegion((j + i * NumRepeatY) * SecondColorStep, i * SizeX, j * SizeY);
            }

            for (int j = 1; j <= AssociatedPlate.ParentScreening.Columns + 1; j++)
                g.DrawLine(new Pen(Color.Black), new Point(j * SizeWell, SizeWell), new Point(j * SizeWell, (AssociatedPlate.ParentScreening.Rows + 1) * SizeWell));
            for (int i = 1; i <= AssociatedPlate.ParentScreening.Rows + 1; i++)
                g.DrawLine(new Pen(Color.Black), new Point(SizeWell, i * SizeWell), new Point((AssociatedPlate.ParentScreening.Columns + 1) * SizeWell, i * SizeWell));
        }

        #region User Interface
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name.ToString() == "tabPageConcentrations")
                UpDateDisplayConcentration();
        }

        private void numericUpDownConcentrationNumber_ValueChanged(object sender, EventArgs e)
        {
            UpDateDisplayConcentration();
        }

        private void panelForDesignDisplay_Paint(object sender, PaintEventArgs e)
        {
            DrawSignature();
        }

        private void radioButtonOrientationLine_CheckedChanged(object sender, EventArgs e)
        {
            DrawSignature();
        }

        private void radioButtonOrientationColumn_CheckedChanged(object sender, EventArgs e)
        {
            DrawSignature();
        }

        private void radioButtonConcentrationsManual_CheckedChanged(object sender, EventArgs e)
        {
            UpDateDisplayConcentration();
        }

        private void radioButtonConcentrationFromThePlate_CheckedChanged(object sender, EventArgs e)
        {
            UpDateDisplayConcentration();
        }

        private void numericUpDownConcentrationStartingValue_ValueChanged(object sender, EventArgs e)
        {
            UpDateDisplayConcentration();
        }

        private void numericUpDownDilutionFactor_ValueChanged(object sender, EventArgs e)
        {
            UpDateDisplayConcentration();
        }
        #endregion

        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cCSVToTable CSVT = new cCSVToTable();
            CSVT.IsDisplayUIForFilePath = true;
            CSVT.IsContainRowNames = true;
            CSVT.IsContainColumnHeaders = true;
            CSVT.Run();

            cExtendedTable PlateDesign = CSVT.GetOutPut();
            if (PlateDesign == null) return;

            for (int Col = 0; Col < cGlobalInfo.CurrentScreening.Columns; Col++)
                for (int Row = 0; Row < cGlobalInfo.CurrentScreening.Rows; Row++)
                {
                    if ((Col < PlateDesign.Count) && (Row < PlateDesign[Col].Count))
                    {
                        double NewConcentration = (double)PlateDesign[Col][Row];

                        //if (cWell.cGlobalInfo.WindowHCSAnalyzer.checkBoxApplyTo_AllPlates.Checked)
                        {
                            foreach (cPlate CurrentPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
                            {
                                cWell TmpWell = CurrentPlate.GetWell(Col, Row, false);
                                if (TmpWell != null)
                                    TmpWell.ListProperties.UpdateValueByName("Concentration", NewConcentration);
                            }
                        }
                        //else
                        // {
                        //     cPlate CurrentPlate = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate();
                        //     CurrentPlate.GetWell(Col, Row, false).ListProperties.UpdateValueByName("Concentration", NewConcentration);
                        // }
                    }
                }
        }


    }
}
