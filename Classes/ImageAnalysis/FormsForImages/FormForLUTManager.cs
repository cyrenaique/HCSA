using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Classes.ImageAnalysis.FormsForImages;
using HCSAnalyzer.Forms.FormsForImages;

namespace HCSAnalyzer.Classes.ImageAnalysis.FormsForImages
{
    public partial class FormForLUTManager : Form
    {
        public cImagePanel CurrentFormForImageDisplay = null;

        public FormForLUTManager(cImagePanel CurrentFormForImageDisplay)
        {
            this.CurrentFormForImageDisplay = CurrentFormForImageDisplay;
            InitializeComponent();
        }

        public List<byte[][]> GetLUTS()
        {
            List<byte[][]> ToBeReturned = new List<byte[][]>();

            for (int IdxChannel = 0; IdxChannel < this.panelForLUTS.Controls.Count; IdxChannel++)
            {
                UserControlSingleLUT SingleLUT = (UserControlSingleLUT)this.panelForLUTS.Controls[IdxChannel];
                ToBeReturned.Add(SingleLUT.SelectedLUT);
            }

            return ToBeReturned;
        }

        public cImageDisplayProperties GetImageDisplayProperties()
        {
            cImageDisplayProperties ToReturn = new cImageDisplayProperties();

            ToReturn.ListMin = new List<double>();
            ToReturn.ListMax = new List<double>();
            ToReturn.ListActive = new List<bool>();
            ToReturn.ListLUTNames = new List<string>();
            ToReturn.ListGamma = new List<double>();
            ToReturn.ListOpacity = new List<double>();

            for (int IdxChannel = 0; IdxChannel < this.panelForLUTS.Controls.Count; IdxChannel++)
            {
                UserControlSingleLUT SingleLUT = (UserControlSingleLUT)this.panelForLUTS.Controls[IdxChannel];
                ToReturn.ListMin.Add((double)SingleLUT.numericUpDownMinValue.Value);
                ToReturn.ListMax.Add((double)SingleLUT.numericUpDownMaxValue.Value);

                ToReturn.ListActive.Add(SingleLUT.checkBoxIsActive.Checked);

                ToReturn.ListLUTNames.Add(SingleLUT.comboBoxForLUT.Text);

                ToReturn.ListGamma.Add(SingleLUT.trackBarGamma.Value);
                ToReturn.ListOpacity.Add(SingleLUT.trackBarOpacity.Value);
            }

            return ToReturn;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        public void CopyToClipBoard()
        {
            if (cGlobalInfo.TmpImageDisplayProperties == null) cGlobalInfo.TmpImageDisplayProperties = new cImageDisplayProperties();
            cGlobalInfo.TmpImageDisplayProperties.UpdateFromLUTManager(this);

            cGlobalInfo.OptionsWindow.panelForCurrentLUTList.Controls.Clear();

            int IDxBand=  0;
            foreach (var item in this.panelForLUTS.Controls)
            {
                UserControlSingleLUT SingleLUT = new UserControlSingleLUT();
                SingleLUT.Location = new Point(5, SingleLUT.Height * IDxBand + 5);

                SingleLUT.checkBoxIsActive.Checked = ((UserControlSingleLUT)item).checkBoxIsActive.Checked;
                SingleLUT.checkBoxIsActive.Enabled = false;

                SingleLUT.textBoxForName.Text = ((UserControlSingleLUT)item).textBoxForName.Text;
                SingleLUT.textBoxForName.Enabled = false;

                SingleLUT.trackBarGamma.Value = ((UserControlSingleLUT)item).trackBarGamma.Value;
                SingleLUT.trackBarGamma.Enabled = false;

                SingleLUT.trackBarOpacity.Value = ((UserControlSingleLUT)item).trackBarOpacity.Value;
                SingleLUT.trackBarOpacity.Enabled = false;

                SingleLUT.numericUpDownMaxValue.Value = ((UserControlSingleLUT)item).numericUpDownMaxValue.Value;
                SingleLUT.numericUpDownMaxValue.Enabled = false;

                SingleLUT.numericUpDownMinValue.Value = ((UserControlSingleLUT)item).numericUpDownMinValue.Value;
                SingleLUT.numericUpDownMinValue.Enabled = false;

                SingleLUT.comboBoxForLUT.Text = ((UserControlSingleLUT)item).comboBoxForLUT.Text;
                SingleLUT.comboBoxForLUT.Enabled = false;

                SingleLUT.pictureBoxForColorSample.Image = ((UserControlSingleLUT)item).pictureBoxForColorSample.Image;

                cGlobalInfo.OptionsWindow.panelForCurrentLUTList.Controls.Add(SingleLUT);
                IDxBand++;
            }
        }

        public bool PastFromClipBoard()
        {
            return UpdateFromDisplayProperties(cGlobalInfo.TmpImageDisplayProperties);
        }

        public bool UpdateFromDisplayProperties(cImageDisplayProperties ImageDisplayProperties)
        {
            if (this.panelForLUTS.Controls.Count != ImageDisplayProperties.ListMin.Count)
            {
                return false;
            }

             for (int IdxChannel = 0; IdxChannel < this.panelForLUTS.Controls.Count; IdxChannel++)
            {
                UserControlSingleLUT SingleLUT = (UserControlSingleLUT)this.panelForLUTS.Controls[IdxChannel];

                SingleLUT.UpdateFromDisplayProperties(ImageDisplayProperties, IdxChannel);
                //SingleLUT.numericUpDownMinValue.Value = (decimal)ImageDisplayProperties.ListMin[IdxChannel];
                //SingleLUT.numericUpDownMaxValue.Value = (decimal)ImageDisplayProperties.ListMax[IdxChannel];
                //SingleLUT.checkBoxIsActive.Checked = ImageDisplayProperties.ListActive[IdxChannel];
                //SingleLUT.comboBoxForLUT.Text = ImageDisplayProperties.ListLUT[IdxChannel];
            }

             return true;
        }

        public void UnselectAll()
        {
            for (int i = 0; i < panelForLUTS.Controls.Count - 1; i++)
            {
                UserControlSingleLUT CurrentLUTCtrl = (UserControlSingleLUT)panelForLUTS.Controls[i];
                CurrentLUTCtrl.checkBoxIsActive.CheckedChanged -= new EventHandler(CurrentLUTCtrl.checkBoxIsActive_CheckedChanged);
                CurrentLUTCtrl.checkBoxIsActive.Checked = false;
                CurrentLUTCtrl.checkBoxIsActive.CheckedChanged += new EventHandler(CurrentLUTCtrl.checkBoxIsActive_CheckedChanged);
            }

            UserControlSingleLUT CurrentLUTCtrlFinal = (UserControlSingleLUT)panelForLUTS.Controls[panelForLUTS.Controls.Count - 1];
            CurrentLUTCtrlFinal.checkBoxIsActive.Checked = false;
        }

        public void SelectAll()
        {
            for (int i = 0; i < panelForLUTS.Controls.Count - 1; i++)
            {
                UserControlSingleLUT CurrentLUTCtrl = (UserControlSingleLUT)panelForLUTS.Controls[i];
                CurrentLUTCtrl.checkBoxIsActive.CheckedChanged -= new EventHandler(CurrentLUTCtrl.checkBoxIsActive_CheckedChanged);
                CurrentLUTCtrl.checkBoxIsActive.Checked = true;
                CurrentLUTCtrl.checkBoxIsActive.CheckedChanged += new EventHandler(CurrentLUTCtrl.checkBoxIsActive_CheckedChanged);
            }

            UserControlSingleLUT CurrentLUTCtrlFinal = (UserControlSingleLUT)panelForLUTS.Controls[panelForLUTS.Controls.Count - 1];
            CurrentLUTCtrlFinal.checkBoxIsActive.Checked = true;
        }

        public ToolStripMenuItem GetExtendContextMenu()
        {
            ToolStripMenuItem ToBeReturned = new ToolStripMenuItem("LUT Manager");

            ToolStripMenuItem ToolStripMenuItem_InactiveAll = new ToolStripMenuItem("Inactivate All");
            ToolStripMenuItem_InactiveAll.Click += new System.EventHandler(this.ToolStripMenuItem_InactiveAll);
            ToBeReturned.DropDownItems.Add(ToolStripMenuItem_InactiveAll);

            ToolStripMenuItem ToolStripMenuItem_ActiveAll = new ToolStripMenuItem("Activate All");
            ToolStripMenuItem_ActiveAll.Click += new System.EventHandler(this.ToolStripMenuItem_ActiveAll);
            ToBeReturned.DropDownItems.Add(ToolStripMenuItem_ActiveAll);

            return ToBeReturned;
        
        }


        private void ToolStripMenuItem_InactiveAll(object sender, EventArgs e)
        {
            this.UnselectAll();
        }

        private void ToolStripMenuItem_ActiveAll(object sender, EventArgs e)
        {
            this.SelectAll();
        }
        
    }
}
