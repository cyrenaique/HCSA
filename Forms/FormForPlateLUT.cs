using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibPlateAnalysis;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using HCSAnalyzer.Classes.MetaComponents;
using Kitware.VTK;

namespace HCSAnalyzer.Forms
{
    public partial class FormForPlateLUT : Form
    {
       
        double GlobalMax = 1;
        double GlobalMin = 0;
        double LocalMin = 0;
        double LocalMax = 1;

        public FormForPlateLUT()
        {
            InitializeComponent();
        }

        private void numericUpDownGeneralMax_ValueChanged(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            if (numericUpDownGeneralMax.Value < numericUpDownGeneralMin.Value) numericUpDownGeneralMax.Value = numericUpDownGeneralMin.Value;
            this.GlobalMax = (double)numericUpDownGeneralMax.Value;
            if (this.LocalMax > this.GlobalMax)
            {
                //this.LocalMax = this.GlobalMax;
                //this.labelMax.Text = this.LocalMax.ToString("N3");
                this.SetNewLocalMax(this.GlobalMax);
            }
            else
            {
                //this.GlobalMax = this.LocalMax;
            }

            if (GlobalMax == 0)
                this.trackBarForPlateLUTMax.Value = 0;
            else
            {
                int Value = (int)(((this.GlobalMax - this.LocalMax) * 100) / (this.GlobalMax - this.GlobalMin));
                if((Value>0)&&(Value<=100))
                    this.trackBarForPlateLUTMax.Value = Value;
            }
            UpDatePlateColor();
        }

        private void numericUpDownGeneralMin_ValueChanged_1(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            if (numericUpDownGeneralMin.Value > numericUpDownGeneralMax.Value) numericUpDownGeneralMin.Value = numericUpDownGeneralMax.Value;
         
            this.GlobalMin = (double)numericUpDownGeneralMin.Value;
            if (this.LocalMin < this.GlobalMin)
            {
                //this.LocalMin = this.GlobalMin;
                //this.labelMin.Text = this.LocalMin.ToString("N3");
                SetNewLocalMin(this.GlobalMin);
            }
            else
            {
              //  this.GlobalMin = this.LocalMin;
            }

            this.labelMin.Text = this.LocalMin.ToString("N3");

            //double Res= Math.Ceiling(this.GlobalMin);
            if (GlobalMin == 0)
                this.trackBarForPlateLUTMin.Value = 0;
            else
            {
                int Value = (int)(((this.LocalMin - this.GlobalMin) * 100) / (this.GlobalMax - this.GlobalMin));
                if ((Value > 0) && (Value <= 100))
                    this.trackBarForPlateLUTMin.Value = Value;
            }

            UpDatePlateColor();
        }

        void UpDateColorSampleImage()
        {
            byte[][] LUT = cGlobalInfo.CurrentPlateLUT;

            Bitmap bmp = new Bitmap(pictureBoxForLUT.Width, pictureBoxForLUT.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
           // double ActiveHeight = 
            int StartY = (int)((pictureBoxForLUT.Height*(100 - trackBarForPlateLUTMax.Value)) / 100.0);
            int EndY = (int)((pictureBoxForLUT.Height * (100 - trackBarForPlateLUTMin.Value)) / 100.0);

            int DeltaY = EndY - StartY;

            ColorPalette ncp = bmp.Palette;
            for (int i = 0; i < 256; i++)
            {
                int RealIdx = (int)((i * LUT[0].Length) / 256);
                ncp.Entries[i] = Color.FromArgb(255, LUT[0][RealIdx], LUT[1][RealIdx], LUT[2][RealIdx]);
            }
            bmp.Palette = ncp;

            var BoundsRect = new Rectangle(0, 0, pictureBoxForLUT.Width, pictureBoxForLUT.Height);
            BitmapData bmpData = bmp.LockBits(BoundsRect, ImageLockMode.WriteOnly, bmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = bmpData.Stride * bmp.Height;
            var rgbValues = new byte[bytes];


            // fill in rgbValues, e.g. with a for loop over an input array

            for (int j = EndY; j < pictureBoxForLUT.Height; j++)
                for (int i = 0; i < pictureBoxForLUT.Width; i++)
                {
                    rgbValues[i + (pictureBoxForLUT.Height - j - 1) * bmpData.Stride] = (byte)(0);
                }


            for (int j = pictureBoxForLUT.Height - EndY; j < pictureBoxForLUT.Height - StartY; j++)
                for (int i = 0; i < pictureBoxForLUT.Width; i++)
                {
                    rgbValues[i + (pictureBoxForLUT.Height - j - 1) * bmpData.Stride] = (byte)(((j - (pictureBoxForLUT.Height - StartY)) * 256) / (/*pictureBoxForLUT.Height*/DeltaY));
                }

            for (int j = pictureBoxForLUT.Height - StartY; j < pictureBoxForLUT.Height; j++)
                for (int i = 0; i < pictureBoxForLUT.Width; i++)
                {
                    rgbValues[i + (pictureBoxForLUT.Height - j - 1) * bmpData.Stride] = (byte)(255);
                }

            
            
            Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);

            pictureBoxForLUT.Image = (Image)bmp;
        }

        private void UpDatePlateColor()
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            if (cGlobalInfo.IsDisplayClassOnly) return;

            cPlate CurrentPlate = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate();
            cDescriptorType cDT = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor();

            UpDateColorSampleImage();

            int ConvertedValue = 0;
            Color WellColor = Color.Black;

            if (!this.activeToolStripMenuItem.Checked)
            {
                foreach (cWell CurrentWell in CurrentPlate.ListActiveWells)
                    CurrentWell.SetNewColor(Color.FromArgb(16, 37, 63));
            }
            else
            {
                foreach (cWell CurrentWell in CurrentPlate.ListActiveWells)
                {
                    if (CurrentWell != null)
                    {
                        if (this.LocalMin == this.LocalMax)
                        {
                            WellColor = Color.FromArgb(cGlobalInfo.CurrentPlateLUT[0][0], cGlobalInfo.CurrentPlateLUT[1][0], cGlobalInfo.CurrentPlateLUT[2][0]);
                        }
                        else
                        {
                            double Val = CurrentWell.GetAverageValue(cDT);

                            ConvertedValue = (int)(((Val - this.LocalMin) * (cGlobalInfo.CurrentPlateLUT[0].Length - 1)) / (this.LocalMax - this.LocalMin));
                            if (ConvertedValue >= cGlobalInfo.CurrentPlateLUT[0].Length)
                                WellColor = Color.FromArgb(cGlobalInfo.CurrentPlateLUT[0][cGlobalInfo.CurrentPlateLUT[0].Length - 1], cGlobalInfo.CurrentPlateLUT[1][cGlobalInfo.CurrentPlateLUT[0].Length - 1], cGlobalInfo.CurrentPlateLUT[2][cGlobalInfo.CurrentPlateLUT[0].Length - 1]);
                            else if (ConvertedValue < 0)
                                WellColor = Color.FromArgb(cGlobalInfo.CurrentPlateLUT[0][0], cGlobalInfo.CurrentPlateLUT[1][0], cGlobalInfo.CurrentPlateLUT[2][0]);
                            else
                                WellColor = Color.FromArgb(cGlobalInfo.CurrentPlateLUT[0][ConvertedValue], cGlobalInfo.CurrentPlateLUT[1][ConvertedValue], cGlobalInfo.CurrentPlateLUT[2][ConvertedValue]);
                        }
                        CurrentWell.SetNewColor(WellColor);
                    }
                }
            }
        }

        void SetNewLocalMax(double LocalMax)
        {
            if (GlobalMax < GlobalMin) return;

            this.LocalMax = LocalMax;
            this.labelMax.Text = this.LocalMax.ToString("N3");

            if (this.GlobalMax == 0)
                this.trackBarForPlateLUTMax.Value = 0;
            else
                this.trackBarForPlateLUTMax.Value = (int)((this.LocalMax * 100) / (this.GlobalMax));
        }

        void SetNewLocalMin(double LocalMin)
        {
            if (GlobalMax <= GlobalMin) return;

            this.LocalMin = LocalMin;
            this.labelMin.Text = this.LocalMin.ToString("N3");

            // compute new slider position
            this.trackBarForPlateLUTMin.Value = (int)(((this.LocalMin - this.GlobalMin) * 100) / (this.GlobalMax-this.GlobalMin));
        }

        public void RefreshMinMax()
        {
            if (cGlobalInfo.CurrentScreening == null) return;

            cExtendedTable MinMaxTable = null;

            if (currentPlateOnlyToolStripMenuItem.Checked)
            {
                cPlate CurrentPlate = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate();
                cDescriptorType cDT = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor();
                MinMaxTable = CurrentPlate.GetMinMax(cDT);
            }
            else
            {
                cDescriptorType cDT = cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor();
                MinMaxTable = cGlobalInfo.CurrentScreening.ListPlatesActive.GetMinMax(cDT);

                //UpDatePlateColor();
            }

            //this.numericUpDownGeneralMin.ValueChanged -= new EventHandler(numericUpDownGeneralMin_ValueChanged_1);
            if (MinMaxTable == null)
            {
                numericUpDownGeneralMax.Value = numericUpDownGeneralMin.Value = 0;
                this.GlobalMin = this.GlobalMax = 0;

                SetNewLocalMin(this.GlobalMin);
                SetNewLocalMax(this.GlobalMax);
            }
            else
            {
                numericUpDownGeneralMin.Value = (decimal)MinMaxTable[0][0];
                this.GlobalMin = MinMaxTable[0][0];

                /*if ((this.GlobalMin > this.LocalMin)||(this.labelMin.Text=="###"))*/
                SetNewLocalMin(this.GlobalMin);
                //this.numericUpDownGeneralMin.ValueChanged += new EventHandler(numericUpDownGeneralMin_ValueChanged_1);

                //this.numericUpDownGeneralMax.ValueChanged -= new EventHandler(numericUpDownGeneralMax_ValueChanged);
                numericUpDownGeneralMax.Value = (decimal)MinMaxTable[0][1];
                this.GlobalMax = MinMaxTable[0][1];
                SetNewLocalMax(this.GlobalMax);
            }
            /*if ((this.GlobalMax < this.LocalMax)||(this.labelMax.Text=="###"))*/ 
            //this.numericUpDownGeneralMax.ValueChanged += new EventHandler(numericUpDownGeneralMax_ValueChanged);

            UpDatePlateColor();


        }

        private void currentPlateOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshMinMax(); 
           // SetNewLocalMax(this.GlobalMax);
          //  SetNewLocalMin(this.GlobalMin);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshMinMax();
           // SetNewLocalMax(this.GlobalMax);
           // SetNewLocalMin(this.GlobalMin);

        }

        private void trackBarForPlateLUTMax_ValueChanged(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            double CurrentValue = (double)numericUpDownGeneralMin.Value + (trackBarForPlateLUTMax.Value / 100.0) * ((double)numericUpDownGeneralMax.Value - (double)numericUpDownGeneralMin.Value);

            labelMax.Text = CurrentValue.ToString("N3");
            this.LocalMax = CurrentValue;

            if (trackBarForPlateLUTMax.Value < trackBarForPlateLUTMin.Value) trackBarForPlateLUTMax.Value = trackBarForPlateLUTMin.Value;
            //   if (trackBarForPlateLUTMin.Value < numericUpDownGeneralMin.Value) trackBarForPlateLUTMin.Value = (int)numericUpDownGeneralMin.Value;
            trackBarForPlateLUTMax.Update();

            UpDatePlateColor();
        }

        private void trackBarForPlateLUTMin_ValueChanged(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            double CurrentValue = (double)numericUpDownGeneralMin.Value + (trackBarForPlateLUTMin.Value / 100.0) * ((double)numericUpDownGeneralMax.Value - (double)numericUpDownGeneralMin.Value);

            labelMin.Text = CurrentValue.ToString("N3");
            this.LocalMin = CurrentValue;
            if (trackBarForPlateLUTMin.Value > trackBarForPlateLUTMax.Value) trackBarForPlateLUTMin.Value = trackBarForPlateLUTMax.Value;
            //   if (trackBarForPlateLUTMin.Value < numericUpDownGeneralMin.Value) trackBarForPlateLUTMin.Value = (int)numericUpDownGeneralMin.Value;
            trackBarForPlateLUTMin.Update();
            UpDatePlateColor();
        }

        private void activeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.UpDatePlateColor();
        }

        private void toolStripComboBoxLUT_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBoxLUT.Text)
            {
                case "HSV":
                    cGlobalInfo.ChangeLUT(Classes.Base_Classes.General.eDefinedLUTs.HSV);
                    this.UpDatePlateColor();
                break;
                case "FIRE":
                    cGlobalInfo.ChangeLUT(Classes.Base_Classes.General.eDefinedLUTs.FIRE);
                    this.UpDatePlateColor();
                break;
                case "GREEN_TO_RED":
                    cGlobalInfo.ChangeLUT(Classes.Base_Classes.General.eDefinedLUTs.GREEN_TO_RED);
                    this.UpDatePlateColor();
                break;
                case "JET":
                    cGlobalInfo.ChangeLUT(Classes.Base_Classes.General.eDefinedLUTs.JET);
                    this.UpDatePlateColor();
                break;
                case "HOT":
                    cGlobalInfo.ChangeLUT(Classes.Base_Classes.General.eDefinedLUTs.HOT);
                    this.UpDatePlateColor();
                break;
                case "COOL":
                    cGlobalInfo.ChangeLUT(Classes.Base_Classes.General.eDefinedLUTs.COOL);
                    this.UpDatePlateColor();
                break;
                case "SPRING":
                    cGlobalInfo.ChangeLUT(Classes.Base_Classes.General.eDefinedLUTs.SPRING);
                    this.UpDatePlateColor();
                break;
                case "SUMMER":
                    cGlobalInfo.ChangeLUT(Classes.Base_Classes.General.eDefinedLUTs.SUMMER);
                    this.UpDatePlateColor();
                break;
                case "AUTUMN":
                    cGlobalInfo.ChangeLUT(Classes.Base_Classes.General.eDefinedLUTs.AUTUMN);
                    this.UpDatePlateColor();
                break;
                case "WINTER":
                    cGlobalInfo.ChangeLUT(Classes.Base_Classes.General.eDefinedLUTs.WINTER);
                    this.UpDatePlateColor();
                break;
                case "BONE":
                    cGlobalInfo.ChangeLUT(Classes.Base_Classes.General.eDefinedLUTs.BONE);
                    this.UpDatePlateColor();
                break;
                case "COPPER":
                    cGlobalInfo.ChangeLUT(Classes.Base_Classes.General.eDefinedLUTs.COPPER);
                    this.UpDatePlateColor();
                break; 
                case "GD":
                    cGlobalInfo.ChangeLUT(Classes.Base_Classes.General.eDefinedLUTs.GD);
                    this.UpDatePlateColor();
                break;
                case "LINEAR":
                    cGlobalInfo.ChangeLUT(Classes.Base_Classes.General.eDefinedLUTs.LINEAR);
                    this.UpDatePlateColor();
                break;
                default:
                break;
            }
        }

        private void dataTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cDisplayExtendedTable CDT = new cDisplayExtendedTable();
            
            cExtendedTable ET = new cExtendedTable();
            ET.Name = toolStripComboBoxLUT.Text;
            ET.ListRowNames = new List<string>();

            byte[][] Res=  cGlobalInfo.CurrentPlateLUT;


            vtkColorTransferFunction TF = vtkColorTransferFunction.New();

           // double[] ListValues =  {-100.0, -90.0, -80.0, -70.0, -60.000004, -50.0, -40.0, -30.000002, -20.0, 20.0, 30.000002, 40.0, 50.0, 60.000004, 70.0, 80.0, 90.0, 100.0};



            for (int i = 0; i < Res[0].Length; i++)
            {
                TF.AddRGBPoint((double)(255.0*i)/Res[0].Length, Res[0][i] / 255.0, Res[1][i] / 255.0, Res[2][i] / 255.0);
            }
            TF.Build();


            ET.Add(new cExtendedList("Red"));
            ET.Add(new cExtendedList("Green"));
            ET.Add(new cExtendedList("Blue"));

            for (int i = 0; i < 255; i++)
			{
                double[] Test = TF.GetColor(i);
                ET[0].Add((char)(Test[0]*255));
                ET[1].Add((char)(Test[1]*255));
                ET[2].Add((char)(Test[2]*255));
 

			}

            




            //ET.Add(new cExtendedList("Red"));
            //for (int i = 0; i < Res[0].Length; i++)
            //{
            //    ET[0].Add(Res[0][i]);
            //    ET.ListRowNames.Add(i.ToString());
            //}

            //ET.Add(new cExtendedList("Green"));
            //for (int i = 0; i < Res[1].Length; i++)
            //    ET[1].Add(Res[1][i]);
            
            //ET.Add(new cExtendedList("Blue"));
            //for (int i = 0; i < Res[2].Length; i++)
            //    ET[2].Add(Res[2][i]);


            CDT.SetInputData(ET);
            CDT.Run();





        }

    }
}
