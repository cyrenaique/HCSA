using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ImageAnalysis;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.ImageAnalysis.FormsForImages;

namespace HCSAnalyzer.Forms.FormsForImages
{
    public partial class UserControlSingleLUT : UserControl
    {
        cImagePanel CurrentFormForImageDisplay = null;
        cSingleChannelImage AssociatedImageBand = null;

        public byte[][] SelectedLUT { get; private set; }

        cGlobalInfo GlobalInfo;
        cLUT ListLUT = new cLUT();

        Color MaxColorForLinear = Color.White;
        //string Name;


        public UserControlSingleLUT()
        {
            InitializeComponent();
        }

        public UserControlSingleLUT(cImagePanel CurrentFormForImageDisplay, cSingleChannelImage AssociatedImageBand, int InitialIdx)
        {
            this.AssociatedImageBand = AssociatedImageBand;
            this.CurrentFormForImageDisplay = CurrentFormForImageDisplay;
            this.GlobalInfo = CurrentFormForImageDisplay.GlobalInfo;

            this.SelectedLUT = ListLUT.LUT_LINEAR;

            InitializeComponent();


            if (InitialIdx == 0)
            {
                this.MaxColorForLinear = Color.FromArgb(255, 255, 0, 0);
            }
            else if (InitialIdx == 1)
            {
                this.MaxColorForLinear = Color.FromArgb(255, 0, 255, 0);
            }
            else if (InitialIdx == 2)
            {
                this.MaxColorForLinear = Color.FromArgb(255, 0, 0, 255);
            }
            else
            {
                this.MaxColorForLinear = Color.FromArgb(255, 255, 0, 0);
            }
            UpDateLinearLUT();
            comboBoxForLUT.Text = "Linear ";

            UpDateColorSampleImage();

            AssociatedImageBand.UpDateMin();
            AssociatedImageBand.UpDateMax();

            //  this.numericUpDownMinValue.ValueChanged -= new EventHandler(numericUpDownMinValue_ValueChanged);
            this.numericUpDownMinValue.Value = (decimal)AssociatedImageBand.Min;
            this.numericUpDownMinValue.ValueChanged += new EventHandler(numericUpDownMinValue_ValueChanged);

            //  this.numericUpDownMaxValue.ValueChanged -= new EventHandler(numericUpDownMaxValue_ValueChanged);
            this.numericUpDownMaxValue.Value = (decimal)AssociatedImageBand.Max;
            this.numericUpDownMaxValue.ValueChanged += new EventHandler(numericUpDownMaxValue_ValueChanged);

            this.trackBarOpacity.ValueChanged += new System.EventHandler(this.trackBarOpacity_ValueChanged);
            this.checkBoxIsActive.CheckedChanged += new System.EventHandler(this.checkBoxIsActive_CheckedChanged);
            this.comboBoxForLUT.SelectedIndexChanged += new System.EventHandler(this.comboBoxForLUT_SelectedIndexChanged);
            this.trackBarGamma.ValueChanged += new System.EventHandler(this.trackBarGamma_ValueChanged);
            this.comboBoxForLUT.SelectedValueChanged += new System.EventHandler(this.comboBoxForLUT_SelectedValueChanged);
            this.pictureBoxForColorSample.DoubleClick += new System.EventHandler(this.pictureBoxForColorSample_DoubleClick);
            
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UserControlSingleLUT_MouseDown);
            this.toolStripMenuItemGammaDefault.Click += new System.EventHandler(this.toolStripMenuItemGammaDefault_Click);
            this.textBoxForName.TextChanged += new System.EventHandler(this.textBoxForName_TextChanged);

            this.BringToFront();
        }

            
        private void UserControlSingleLUT_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            ContextMenuStrip NewMenu = new ContextMenuStrip();

            ToolStripMenuItem MenuItem = AssociatedImageBand.GetContextMenu(CurrentFormForImageDisplay);

            if (MenuItem != null)
                NewMenu.Items.Add(MenuItem);

            ToolStripMenuItem ToolStripMenuItem_UpdateMinMax = new ToolStripMenuItem("Update Min-Max");
            ToolStripMenuItem_UpdateMinMax.Click += new System.EventHandler(this.ToolStripMenuItem_UpdateMinMax);
            NewMenu.Items.Add(ToolStripMenuItem_UpdateMinMax);

            ToolStripMenuItem ToolStripMenuItem_DisplayProperties = new ToolStripMenuItem("Display Properties");

            ToolStripMenuItem ToolStripMenuItem_DisplayPropertiesCopy = new ToolStripMenuItem("Copy To Clipboard");
            ToolStripMenuItem_DisplayPropertiesCopy.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayPropertiesCopy);
            ToolStripMenuItem_DisplayProperties.DropDownItems.Add(ToolStripMenuItem_DisplayPropertiesCopy);

            ToolStripMenuItem ToolStripMenuItem_DisplayPropertiesPaste = new ToolStripMenuItem("Past From Clipboard");
            ToolStripMenuItem_DisplayPropertiesPaste.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayPropertiesPaste);
            ToolStripMenuItem_DisplayProperties.DropDownItems.Add(ToolStripMenuItem_DisplayPropertiesPaste);

            NewMenu.Items.Add(ToolStripMenuItem_DisplayProperties);

            NewMenu.Items.Add(CurrentFormForImageDisplay.LUTManager.GetExtendContextMenu());

            NewMenu.Show(Control.MousePosition);
        }


        private void ToolStripMenuItem_DisplayPropertiesCopy(object sender, EventArgs e)
        {
            CurrentFormForImageDisplay.LUTManager.CopyToClipBoard();
        }

        private void ToolStripMenuItem_DisplayPropertiesPaste(object sender, EventArgs e)
        {
            if (CurrentFormForImageDisplay.LUTManager.PastFromClipBoard() == false)
            {
                MessageBox.Show("Inconsistent number of channels!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ToolStripMenuItem_UpdateMinMax(object sender, EventArgs e)
        {
            AssociatedImageBand.UpDateMin();
            AssociatedImageBand.UpDateMax();

            this.numericUpDownMinValue.ValueChanged -= new EventHandler(numericUpDownMinValue_ValueChanged);
            this.numericUpDownMinValue.Value = (decimal)AssociatedImageBand.Min;
            this.numericUpDownMinValue.ValueChanged += new EventHandler(numericUpDownMinValue_ValueChanged);
            this.numericUpDownMaxValue.Value = (decimal)AssociatedImageBand.Max;
        }

        private void textBoxForName_TextChanged(object sender, EventArgs e)
        {
            //if(AssociatedImageBand!=null)
            AssociatedImageBand.Name = textBoxForName.Text;
        }

        private void toolStripMenuItemGammaDefault_Click(object sender, EventArgs e)
        {
            this.trackBarGamma.Value = this.trackBarGamma.Maximum / 2;
        }

        private void pictureBoxForColorSample_DoubleClick(object sender, EventArgs e)
        {
            if (this.SelectedLUT != ListLUT.LUT_LINEAR) return;

            ColorDialog ColorDia = new ColorDialog();
            ColorDia.ShowDialog();

            this.MaxColorForLinear = ColorDia.Color;
            UpDateLinearLUT();

            UpDateColorSampleImage();
            CurrentFormForImageDisplay.ReDrawPic();
        }

        private void trackBarGamma_ValueChanged(object sender, EventArgs e)
        {
            CurrentFormForImageDisplay.ReDrawPic();
        }

        private void comboBoxForLUT_SelectedValueChanged(object sender, EventArgs e)
        {
            this.UpdateLUT();
        }

        private void comboBoxForLUT_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLUT();
        }

        private void numericUpDownMaxValue_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownMaxValue.Value < numericUpDownMinValue.Value) numericUpDownMaxValue.Value = numericUpDownMinValue.Value;
            CurrentFormForImageDisplay.ReDrawPic();
        }

        private void numericUpDownMinValue_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownMinValue.Value > numericUpDownMaxValue.Value) numericUpDownMinValue.Value = numericUpDownMaxValue.Value;
            CurrentFormForImageDisplay.ReDrawPic();
        }

        public void checkBoxIsActive_CheckedChanged(object sender, EventArgs e)
        {
            CurrentFormForImageDisplay.ReDrawPic();
        }

        private void trackBarOpacity_ValueChanged(object sender, EventArgs e)
        {
            CurrentFormForImageDisplay.ReDrawPic();
        }

        public double ComputeMin()
        {
            return new cExtendedList(this.AssociatedImageBand.Data).Min();
        }

        public double ComputeMax()
        {
            return new cExtendedList(this.AssociatedImageBand.Data).Max();
        }

        public string GetName()
        {
            return textBoxForName.Text;
        }

        void UpdateLUT()
        {
            switch (comboBoxForLUT.SelectedIndex)
            {
                case 0:
                    this.SelectedLUT = ListLUT.LUT_LINEAR;
                    break;
                case 1:
                    this.SelectedLUT = ListLUT.LUT_HSV;
                    break;
                case 2:
                    this.SelectedLUT = ListLUT.LUT_FIRE;
                    break;
                case 3:
                    this.SelectedLUT = ListLUT.LUT_GREEN_TO_RED;
                    break;
                case 4:
                    this.SelectedLUT = ListLUT.LUT_JET;
                    break;
                case 5:
                    this.SelectedLUT = ListLUT.LUT_HOT;
                    break;
                case 6:
                    this.SelectedLUT = ListLUT.LUT_COOL;
                    break;
                case 7:
                    this.SelectedLUT = ListLUT.LUT_SPRING;
                    break;
                case 8:
                    this.SelectedLUT = ListLUT.LUT_SUMMER;
                    break;
                case 9:
                    this.SelectedLUT = ListLUT.LUT_AUTOMN;
                    break;
                case 10:
                    this.SelectedLUT = ListLUT.LUT_WINTER;
                    break;
                case 11:
                    this.SelectedLUT = ListLUT.LUT_BONE;
                    break;
                case 12:
                    this.SelectedLUT = ListLUT.LUT_COPPER;
                    break;
                case 13:
                    this.SelectedLUT = ListLUT.LUT_GD;
                    break;
                default: return;

            }

            UpDateColorSampleImage();
            CurrentFormForImageDisplay.ReDrawPic();

        }

        void UpDateColorSampleImage()
        {
            Bitmap bmp = new Bitmap(pictureBoxForColorSample.Width, pictureBoxForColorSample.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            ColorPalette ncp = bmp.Palette;
            for (int i = 0; i < 256; i++)
            {
                int RealIdx = (int)((i * this.SelectedLUT[0].Length) / 256);
                ncp.Entries[i] = Color.FromArgb(255, this.SelectedLUT[0][RealIdx], this.SelectedLUT[1][RealIdx], this.SelectedLUT[2][RealIdx]);
            }
            bmp.Palette = ncp;

            var BoundsRect = new Rectangle(0, 0, pictureBoxForColorSample.Width, pictureBoxForColorSample.Height);
            BitmapData bmpData = bmp.LockBits(BoundsRect, ImageLockMode.WriteOnly, bmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = bmpData.Stride * bmp.Height;
            var rgbValues = new byte[bytes];

            // fill in rgbValues, e.g. with a for loop over an input array
            for (int j = 0; j < pictureBoxForColorSample.Height; j++)
                for (int i = 0; i < pictureBoxForColorSample.Width; i++)
                {
                    rgbValues[i + j * bmpData.Stride] = (byte)((i * 256) / (pictureBoxForColorSample.Width));
                }

            Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);

            pictureBoxForColorSample.Image = (Image)bmp;
        }

        void UpDateLinearLUT()
        {
            for (int Idx = 0; Idx < ListLUT.LUT_LINEAR[0].Length; Idx++)
            {
                ListLUT.LUT_LINEAR[0][Idx] = (byte)((MaxColorForLinear.R * Idx) / ListLUT.LUT_LINEAR[0].Length);
                ListLUT.LUT_LINEAR[1][Idx] = (byte)((MaxColorForLinear.G * Idx) / ListLUT.LUT_LINEAR[0].Length);
                ListLUT.LUT_LINEAR[2][Idx] = (byte)((MaxColorForLinear.B * Idx) / ListLUT.LUT_LINEAR[0].Length);
            }

        }

        public void CreateAndUpDateLinearLUT(Color MaxColor)
        {
            this.MaxColorForLinear = MaxColor; 
            this.SelectedLUT = ListLUT.LUT_LINEAR;
            UpDateLinearLUT();
            UpDateColorSampleImage();
        } 

        
        public void UpdateFromDisplayProperties(cImageDisplayProperties ImageDisplayProperties, int IdxChannel)
        {
            // this.numericUpDownMaxValue.ValueChanged -=  new EventHandler(numericUpDownMaxValue_ValueChanged);
            this.numericUpDownMaxValue.Value = (decimal)ImageDisplayProperties.ListMax[IdxChannel];
            //   this.numericUpDownMaxValue.ValueChanged +=  new EventHandler(numericUpDownMaxValue_ValueChanged);
            //   this.comboBoxForLUT.SelectedIndexChanged -= new EventHandler(comboBoxForLUT_SelectedIndexChanged);

            //   this.comboBoxForLUT.SelectedIndexChanged += new EventHandler(comboBoxForLUT_SelectedIndexChanged); 

            //   this.numericUpDownMinValue.ValueChanged -= new EventHandler(numericUpDownMinValue_ValueChanged);
            this.numericUpDownMinValue.Value = (decimal)ImageDisplayProperties.ListMin[IdxChannel];
            //   this.numericUpDownMinValue.ValueChanged += new EventHandler(numericUpDownMinValue_ValueChanged); 


            //   this.checkBoxIsActive.CheckedChanged -= new EventHandler(checkBoxIsActive_CheckedChanged);
            this.checkBoxIsActive.Checked = ImageDisplayProperties.ListActive[IdxChannel];
            //  this.checkBoxIsActive.CheckedChanged += new EventHandler(checkBoxIsActive_CheckedChanged);

            this.comboBoxForLUT.Text = ImageDisplayProperties.ListLUTNames[IdxChannel];

            this.trackBarOpacity.Value = (int)ImageDisplayProperties.ListOpacity[IdxChannel];
            this.trackBarGamma.Value = (int)ImageDisplayProperties.ListGamma[IdxChannel];

        }






    }
}
