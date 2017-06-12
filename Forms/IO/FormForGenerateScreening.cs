using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes;
using LibPlateAnalysis;

namespace HCSAnalyzer.Forms
{
    public partial class FormForGenerateScreening : Form
    {

        cGlobalInfo GlobalInfo;

        public FormForGenerateScreening(cGlobalInfo GlobalInfo)
        {
            InitializeComponent();

            this.GlobalInfo = GlobalInfo;

            if (cGlobalInfo.CurrentScreening == null) this.checkBoxAddAsDescriptor.Enabled = false;

            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;

            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.checkBoxStandardDeviation, "Step: " + cGlobalInfo.OptionsWindow.numericUpDownGenerateScreenNoiseStdDev.Value);
            toolTip1.SetToolTip(this.checkBoxShiftRowEffect, "Step: " + cGlobalInfo.OptionsWindow.numericUpDownGenerateScreenRowEffectShift.Value);
            toolTip1.SetToolTip(this.checkBoxRatioXY, "Step: " + cGlobalInfo.OptionsWindow.numericUpDownGenerateScreenRatioXY.Value);
            toolTip1.SetToolTip(this.checkBoxEdgeEffectIteration, "Step: " + cGlobalInfo.OptionsWindow.numericUpDownGenerateScreenDiffusion.Value);
        }

        private void numericUpDownColPosCtrl_ValueChanged(object sender, EventArgs e)
        {
            if ((int)numericUpDownColPosCtrl.Value > (int)numericUpDownColumns.Value-1)
                numericUpDownColPosCtrl.Value = numericUpDownColumns.Value-1;
        }

        private void numericUpDownColNegCtrl_ValueChanged(object sender, EventArgs e)
        {
            if ((int)numericUpDownColNegCtrl.Value > (int)numericUpDownColumns.Value - 1)
                numericUpDownColNegCtrl.Value = numericUpDownColumns.Value - 1;

        }

        private void checkBoxPositiveCtrl_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownColPosCtrl.Enabled = checkBoxPositiveCtrl.Checked;
            numericUpDownPosCtrlMean.Enabled = checkBoxPositiveCtrl.Checked;
            numericUpDownPosCtrlStdv.Enabled = checkBoxPositiveCtrl.Checked;
        }

        private void checkBoxNegativeCtrl_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownColNegCtrl.Enabled = checkBoxNegativeCtrl.Checked;
            numericUpDownNegCtrlMean.Enabled = checkBoxNegativeCtrl.Checked;
            numericUpDownNegCtrlStdv.Enabled = checkBoxNegativeCtrl.Checked;
        }

        private void numericUpDownColumns_ValueChanged(object sender, EventArgs e)
        {
            if ((int)numericUpDownColPosCtrl.Value > (int)numericUpDownColumns.Value - 1)
                numericUpDownColPosCtrl.Value = numericUpDownColumns.Value - 1;
            if ((int)numericUpDownColNegCtrl.Value > (int)numericUpDownColumns.Value - 1)
                numericUpDownColNegCtrl.Value = numericUpDownColumns.Value - 1;

            numericUpDownBowlEffectRatioXY.Value = numericUpDownColumns.Value / numericUpDownRows.Value;
        }


        private void numericUpDownRowEffectIntensity_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownRowEffectIntensity.Enabled = checkBoxRowEffect.Checked;
        }

        private void numericUpDownRows_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownBowlEffectRatioXY.Value = numericUpDownColumns.Value / numericUpDownRows.Value;
        }

        private void checkBoxRowEffect_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownRowEffectIntensity.Enabled = checkBoxRowEffect.Checked;
        }

        private void checkBoxColumnEffect_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownColEffectIntensity.Enabled = checkBoxColumnEffect.Checked;
        }

        private void checkBoxEdgeEffect_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownEdgeEffectIntensity.Enabled = numericUpDownEdgeEffectIteration.Enabled = checkBoxEdgeEffect.Checked;
        }

        private void checkBoxBowlEffect_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownBowlEffectIntensity.Enabled = numericUpDownBowlEffectRatioXY.Enabled = checkBoxBowlEffect.Checked;
        }

        private void checkBoxAddAsDescriptor_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxGeneralInfo.Enabled = !checkBoxAddAsDescriptor.Checked;

            if (checkBoxAddAsDescriptor.Checked)
            {
                numericUpDownPlateNumber.Value = cGlobalInfo.CurrentScreening.GetNumberOfOriginalPlates();
                numericUpDownColumns.Value = cGlobalInfo.CurrentScreening.Columns;
                numericUpDownRows.Value = cGlobalInfo.CurrentScreening.Rows;
            }
        }

        private void toolStripMenuItem96_Click(object sender, EventArgs e)
        {
            this.numericUpDownColumns.Value = 12;
            this.numericUpDownRows.Value = 8;
        }

        private void toolStripMenuItem384_Click(object sender, EventArgs e)
        {
            this.numericUpDownColumns.Value = 24;
            this.numericUpDownRows.Value = 16;
        }

        private void toolStripMenuItem1536_Click(object sender, EventArgs e)
        {
            this.numericUpDownColumns.Value = 48;
            this.numericUpDownRows.Value = 32;
        }



    }
}
