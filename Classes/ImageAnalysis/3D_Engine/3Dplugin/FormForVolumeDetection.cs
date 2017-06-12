using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IM3_Plugin3
{

    public partial class FormForVolumeDetection : Form
    {

        FormForControl Parent;


        public FormForVolumeDetection(FormForControl Parent)
        {
            InitializeComponent();

            this.Parent = Parent;

            //comboBoxExistingSpotsDetected.DataSource = this.Parent.ListDetectedSpots;
            //if (this.Parent.ListDetectedSpots.Count > 0)
            //    comboBoxExistingSpotsDetected.DisplayMember = this.Parent.ListDetectedSpots[0];

            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            //comboBox1.DisplayMember = "ProductName";

            //cListObject LObj = new cListObject();
            //LObj.Add("Test1");
            //LObj.Add("Test2");

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void checkBoxVolumeSmooth_CheckedChanged(object sender, EventArgs e)
        {
            this.numericUpDownSmoothIterations.Enabled = this.checkBoxVolumeSmooth.Checked;
        }

        private void checkBoxIsRegionGrowing_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxRegionGrowing.Enabled = checkBoxIsRegionGrowing.Checked;
        }

        private void radioButtonSeedInitialization_CheckedChanged(object sender, EventArgs e)
        {
            panelSeedBased.Enabled = radioButtonSeedInitialization.Checked;
            panelForRegionBasedInitialization.Enabled = !radioButtonSeedInitialization.Checked;
        }

        private void radioButtonRegionsBased_CheckedChanged(object sender, EventArgs e)
        {
            panelSeedBased.Enabled = !radioButtonRegionsBased.Checked;
            panelForRegionBasedInitialization.Enabled = radioButtonRegionsBased.Checked;
        }

        private void checkBoxIsConvergence_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownIterationNumber.Enabled = !checkBoxIsConvergence.Checked;
        }

        private void FormForVolumeDetection_Shown(object sender, EventArgs e)
        {
            comboBoxExistingSpotsDetected.Items.Clear();

            foreach (string Spot in this.Parent.ListDetectedSpots)
            {
                comboBoxExistingSpotsDetected.Items.Add(Spot);
            }

            if (this.Parent.ListDetectedSpots.Count > 0)
                comboBoxExistingSpotsDetected.SelectedIndex = 0;
            comboBoxExistingSpotsDetected.Update();
        }

        private void radioButtonVolumeDetectionNew_CheckedChanged(object sender, EventArgs e)
        {
            panelRegionBasedDetectionNew.Enabled = radioButtonVolumeDetectionNew.Checked;
            panelRegionDetectionExisting.Enabled = !radioButtonVolumeDetectionNew.Checked;
        }

        private void radioButtonVolumeDetectionExisting_CheckedChanged(object sender, EventArgs e)
        {
            panelRegionBasedDetectionNew.Enabled = !radioButtonVolumeDetectionExisting.Checked;
            panelRegionDetectionExisting.Enabled = radioButtonVolumeDetectionExisting.Checked;
        }
    }



}
