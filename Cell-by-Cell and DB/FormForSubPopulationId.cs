using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using ImageAnalysis;
using ImageAnalysisFiltering;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Classes.MetaComponents;

namespace HCSAnalyzer.Cell_by_Cell_and_DB
{
    public partial class FormForSubPopulationId : Form
    {
        FormForSingleCellsDisplay Parent;
        public FormForSubPopulationId(FormForSingleCellsDisplay Parent)
        {
            InitializeComponent();
            this.Parent = Parent;
        }

        int ImageWidth;
        int ImageHeight;
        int ImageDepth;

        cExtendedList ListX = new cExtendedList();
        cExtendedList ListY = new cExtendedList();
        cExtendedList ListZ = new cExtendedList();

        double MaxX;
        double MinX;
        double MaxY;
        double MinY;
        double MaxZ;
        double MinZ;

        cImage ResultingImage;
        static cImage DensityMaps;

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            ImageWidth = (int)numericUpDownMapWidth.Value;
            ImageHeight = (int)numericUpDownMapHeight.Value;

            if (this.radioButton2D.Checked) ImageDepth = 1;
            else
                ImageDepth = (int)numericUpDownMapDepth.Value;

            //GlobalInfo.ListCellularPhenotypes[(int)MachineLearning.Classes[j]]
            cImageDrawKernel GK = new cImageDrawKernel();
            GK.sigma_x = (int)numericUpDownKernelSize.Value;
            GK.sigma_y = (int)numericUpDownKernelSize.Value;
            GK.sigma_z = (int)numericUpDownKernelSize.Value;
            GK.Run();

            cImage K = GK.GetOutPut();

            if (Parent.comboBoxAxeY.SelectedIndex == -1) return;
            if (Parent.comboBoxVolume.SelectedIndex == -1) return;

            cExtendedList ListVolumes = new cExtendedList();

            ListX.Clear();
            ListY.Clear();
            ListZ.Clear();

            cExtendedTable TableForDataToBeAnalyzed = Parent.GetActiveSignatures(false);

            for (int j = 0; j < TableForDataToBeAnalyzed[0].Count; j++)
            {
                ListX.Add(TableForDataToBeAnalyzed[Parent.comboBoxAxeX.SelectedIndex][j]);
                ListY.Add(TableForDataToBeAnalyzed[Parent.comboBoxAxeY.SelectedIndex][j]);
            }

            //MaxX = ListX.Max();
            //MinX = ListX.Min();

            //MaxY = ListY.Max();
            //MinY = ListY.Min();

            MaxX = Parent.chartForPoints.ChartAreas[0].AxisX.Maximum;
            MinX = Parent.chartForPoints.ChartAreas[0].AxisX.Minimum;

            MaxY = Parent.chartForPoints.ChartAreas[0].AxisY.Maximum;
            MinY = Parent.chartForPoints.ChartAreas[0].AxisY.Minimum;


            //MaxY = Parent.MaxY;
            //MinY = Parent.MinY;


            if (this.radioButton3D.Checked)
            {
                for (int j = 0; j < TableForDataToBeAnalyzed[0].Count; j++)
                {
                    ListZ.Add(TableForDataToBeAnalyzed[Parent.comboBoxVolume.SelectedIndex][j]);
                }
                MaxZ = ListZ.Max();
                MinZ = ListZ.Min();
            }


            DensityMaps = new cImage(ImageWidth, ImageHeight, ImageDepth, cGlobalInfo.ListCellularPhenotypes.Count);

            int[] ClassPopulations = new int[cGlobalInfo.ListCellularPhenotypes.Count];

            if (this.radioButton3D.Checked)
            {
                int Idx = 0;
                for (int j = 0; j < Parent.InputTable[0].Count; j++)
                {
                    if (Parent.PanelPhenotypeSelection.GetListSelectedClass()[(int)Parent.MachineLearning.Classes[j]])
                    {
                        double TmpValueX = (ImageWidth * (ListX[Idx] - MinX)) / (MaxX - MinX) - K.Width / 2;
                        double TmpValueY = ImageHeight - (ImageHeight * (ListY[Idx] - MinY)) / (MaxY - MinY) - K.Height / 2;
                        double TmpValueZ = ImageDepth - (ImageDepth * (ListZ[Idx] - MinZ)) / (MaxZ - MinZ) - K.Depth / 2;

                        DensityMaps.AddInto(K, (int)TmpValueX, (int)TmpValueY, (int)TmpValueZ, (int)Parent.MachineLearning.Classes[j]);
                        ClassPopulations[(int)Parent.MachineLearning.Classes[j]]++;
                        Idx++;
                    }
                }
            }
            else
            {
                int Idx = 0;
                for (int j = 0; j < Parent.InputTable[0].Count; j++)
                {
                    if (Parent.PanelPhenotypeSelection.GetListSelectedClass()[(int)Parent.MachineLearning.Classes[j]])
                    {
                        double TmpValueX = (ImageWidth * (ListX[Idx] - MinX)) / (MaxX - MinX) - K.Width / 2;
                        double TmpValueY = ImageHeight - (ImageHeight * (ListY[Idx] - MinY)) / (MaxY - MinY) - K.Height / 2;

                        DensityMaps.AddInto(K, (int)TmpValueX, (int)TmpValueY, 0, (int)Parent.MachineLearning.Classes[j]);
                        ClassPopulations[(int)Parent.MachineLearning.Classes[j]]++;
                        Idx++;
                    }
                }
            }
            cListExtendedTable TablesForDensity = new cListExtendedTable(DensityMaps);

            for (int idxChannel = 0; idxChannel < DensityMaps.GetNumChannels(); idxChannel++)
            {
                DensityMaps.SingleChannelImage[idxChannel].Name = cGlobalInfo.ListCellularPhenotypes[idxChannel].Name;
                float Sum = 1;
                if (checkBoxNormalized.Checked)
                {
                    Sum = (float)TablesForDensity[idxChannel].Sum();
                    if (Sum <= 0.0) continue;
                    for (int i = 0; i < DensityMaps.SingleChannelImage[idxChannel].Data.Length; i++)
                    {
                        //if (ClassPopulations[idxChannel] > 0)
                        DensityMaps.SingleChannelImage[idxChannel].Data[i] = (100 * DensityMaps.SingleChannelImage[idxChannel].Data[i]) / Sum;
                    }
                }
                else
                {
                    for (int i = 0; i < DensityMaps.SingleChannelImage[idxChannel].Data.Length; i++)
                    {
                        // if (ClassPopulations[idxChannel] > 0)
                        DensityMaps.SingleChannelImage[idxChannel].Data[i] = DensityMaps.SingleChannelImage[idxChannel].Data[i];
                    }
                }
            }

            cDisplaySingleImage IV = new cDisplaySingleImage();
            DensityMaps.Name = "Density Maps. K["+ this.numericUpDownKernelSize.Value +"]";

            if (this.checkBoxModifyColors.Checked)
                IV.ListLinearMaxColor = new List<Color>();

            int RealIdxChannel = 0;
            for (int idxChannel = 0; idxChannel < ClassPopulations.Length; idxChannel++)
            {
                if (ClassPopulations[idxChannel] == 0)
                    DensityMaps.SingleChannelImage.RemoveAt(RealIdxChannel--);
                else
                {
                    if (this.checkBoxModifyColors.Checked)
                    IV.ListLinearMaxColor.Add(cGlobalInfo.ListCellularPhenotypes[idxChannel].ColourForDisplay);
                }
                RealIdxChannel++;
            }
            
  
            IV.SetInputData(DensityMaps);
            IV.Run();

            this.buttonApply.Enabled = true;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {

            if (DensityMaps.GetNumChannels() > cGlobalInfo.ListCellularPhenotypes.Count - 2)
            {
                MessageBox.Show("This process cannot manage more than " + (cGlobalInfo.ListCellularPhenotypes.Count - 2) + " different classes !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cImageSegmentationThreshold ST = new cImageSegmentationThreshold();
            ST.SetInputData(DensityMaps);
            ST.ListProperties.UpdateValueByName("Threshold", (double)this.numericUpDown1.Value);
            ST.MaxValue = 1;
            ST.Run();

            cImage SegmentedImages = ST.GetOutPut();

            // let's create an image containing the different non overlapping populations

            // Values:
            // 0 : outliers
            // 1 : Overlapping population
            // then population specific areas
            ResultingImage = new cImage(SegmentedImages.Width, SegmentedImages.Height, SegmentedImages.Depth, 1);
            ResultingImage.Name = "Overlapping Populations Analysis";

            for (int i = 0; i < ResultingImage.Width * ResultingImage.Height * ResultingImage.Depth; i++)
            {
                int IdxPop = 0;
                float PixTotal = 0;
                float CurrentValue = 0;
                for (int IdxChannel = 0; IdxChannel < SegmentedImages.GetNumChannels(); IdxChannel++)
                {
                    CurrentValue = SegmentedImages.SingleChannelImage[IdxChannel].Data[i];
                    PixTotal += CurrentValue;
                    if (CurrentValue == 1)
                        IdxPop = IdxChannel;

                }


                if (PixTotal > 1)
                    ResultingImage.SingleChannelImage[0].Data[i] = 1;
                else if (PixTotal == 1)
                {
                    ResultingImage.SingleChannelImage[0].Data[i] = IdxPop + 2;
                }

                //float Value0 = SegmentedImages.SingleChannelImage[PositiveBand].Data[i];
                //float Value1 = SegmentedImages.SingleChannelImage[NegativeBand].Data[i];

                //if ((Value0 > 0) && (Value1 > 0))  // overlapping
                //    ResultingImage.SingleChannelImage[0].Data[i] = 1;
                //else if ((Value0 > 0) && (Value1 <= 0))
                //    ResultingImage.SingleChannelImage[0].Data[i] = 2;// DensityMaps.SingleChannelImage[PositiveBand].Data[i];
                //else if ((Value0 <= 0) && (Value1 > 0))
                //    ResultingImage.SingleChannelImage[0].Data[i] = 3;// -DensityMaps.SingleChannelImage[NegativeBand].Data[i];

            }


            if (checkBoxUpdatePhenotypeName.Checked)
            {
                cGlobalInfo.ListCellularPhenotypes[0].Name = "Outliers";
                cGlobalInfo.ListCellularPhenotypes[1].Name = "Overlapping Populations";

                for (int IdxChannel = 0; IdxChannel < DensityMaps.GetNumChannels(); IdxChannel++)
                {
                    cGlobalInfo.ListCellularPhenotypes[IdxChannel + 2].Name = DensityMaps.SingleChannelImage[IdxChannel].Name + " Specific";
                }
            }

            if (checkBoxDisplayResultingImage.Checked)
            {
                //  cImageViewer IV2 = new cImageViewer();
                //  IV2.SetImage(ResultingImage/*SegmentedImages*/);
                //  IV2.Show();
                List<byte[][]> ListLUTs = new List<byte[][]>();
                cLUT LUT = new cLUT();
                ListLUTs.Add(LUT.LUT_FIRE);

                pictureBoxForImage.Image = ResultingImage.GetBitmap(1, null, ListLUTs);
            }
            //   return;


            // now apply the result to the points
            for (int j = 0; j < Parent.InputTable[0].Count/* dt.Rows.Count*/; j++)
            {

                double TmpValueX = (ImageWidth * (ListX[j] - MinX)) / (MaxX - MinX);
                double TmpValueY = ImageHeight - (ImageHeight * (ListY[j] - MinY)) / (MaxY - MinY);

                int PosX = (int)TmpValueX;
                int PosY = (int)TmpValueY;

                if (PosX >= ResultingImage.Width) PosX = ResultingImage.Width - 1;
                if (PosY >= ResultingImage.Height) PosY = ResultingImage.Height - 1;
                if (PosX < 0) PosX = 0;
                if (PosY < 0) PosY = 0;

                double Value = ResultingImage.SingleChannelImage[0].Data[PosX + PosY * ResultingImage.Width];
                if (Value > 0)
                {
                    Parent.MachineLearning.Classes[j] = Value;
                }
                else
                    Parent.MachineLearning.Classes[j] = 0;

                //double currentClass = MachineLearning.Classes[j];

                //ListX.Add(double.Parse(dt.Rows[j][this.comboBoxAxeX.SelectedIndex].ToString()));
                //ListY.Add(double.Parse(dt.Rows[j][this.comboBoxAxeY.SelectedIndex].ToString()));
            }
            Parent.MachineLearning.UpDateNumberOfCluster();
            Parent.ReDraw();
            //(int)MachineLearning.Classes[j]
        }

        private void radioButton3D_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2D.Checked) numericUpDownMapDepth.Enabled = false;
            else
                numericUpDownMapDepth.Enabled = true;
        }

        private void radioButton2D_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2D.Checked) numericUpDownMapDepth.Enabled = false;
            else
                numericUpDownMapDepth.Enabled = true;

        }
    }
}
