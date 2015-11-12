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
using HCSAnalyzer.Classes.General_Types;

namespace HCSAnalyzer.Forms
{
    public partial class FormForPanelFilterHits : Form
    {
        public FormForPanelFilterHits(cGlobalInfo GlobalInfo)
        {
            InitializeComponent();

            foreach (var item in cGlobalInfo.CurrentScreening.ListDescriptors)
            {
                if (item.IsActive()) this.comboBoxForDescName.Items.Add(item.GetName());
            }

            foreach (var item in cGlobalInfo.CurrentScreening.ListWellPropertyTypes)
            {
                this.comboBoxForPropertyName.Items.Add(item.Name);
            }


            //this.comboBoxForDescName.SelectedText = this.comboBoxForDescName.Items[0].ToString();

        }

        private void radioButtonZScore_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownManualValue.Enabled = radioButtonManual.Checked;
            numericUpDownZScoreValue.Enabled = radioButtonZScore.Checked;
        }

        private void radioButtonManual_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownManualValue.Enabled = radioButtonManual.Checked;
            numericUpDownZScoreValue.Enabled = radioButtonZScore.Checked;
        }

        private void comboBoxForPropertyName_SelectedIndexChanged(object sender, EventArgs e)
        {

            cPropertyType CPT = cGlobalInfo.CurrentScreening.ListWellPropertyTypes.FindByName(this.comboBoxForPropertyName.Text);
            this.comboBoxPropertyComparison.Items.Clear();
            if (CPT == null) return;


            if (CPT.Type == eDataType.STRING)
            {
                this.comboBoxPropertyComparison.Items.Add("=");
                this.comboBoxPropertyComparison.Items.Add("!=");
                this.comboBoxPropertyComparison.Items.Add("C");
                this.comboBoxPropertyComparison.Items.Add("!C");

                this.TextBoxValuePropToBeCompared.Visible = true;
                this.numericUpDownValuePropToBeCompared.Visible = false;

            }
            else if (CPT.Type == eDataType.BOOL)
            {
                this.comboBoxPropertyComparison.Items.Add("=");
                this.comboBoxPropertyComparison.Items.Add("!=");

                this.TextBoxValuePropToBeCompared.Visible = false;
                this.numericUpDownValuePropToBeCompared.Visible = true;
                this.numericUpDownValuePropToBeCompared.Minimum = 0;
                this.numericUpDownValuePropToBeCompared.Maximum = 1;
                this.numericUpDownValuePropToBeCompared.DecimalPlaces = 0;

            }
            else if (CPT.Type == eDataType.INTEGER)
            {
                this.comboBoxPropertyComparison.Items.Add(">");
                this.comboBoxPropertyComparison.Items.Add("<");
                this.comboBoxPropertyComparison.Items.Add(">=");
                this.comboBoxPropertyComparison.Items.Add("<=");
                this.comboBoxPropertyComparison.Items.Add("=");
                this.comboBoxPropertyComparison.Items.Add("!=");

                this.TextBoxValuePropToBeCompared.Visible = false;
                this.numericUpDownValuePropToBeCompared.Visible = true;

                this.numericUpDownValuePropToBeCompared.Minimum = -9999999999999999;
                this.numericUpDownValuePropToBeCompared.Maximum = +9999999999999999;
                this.numericUpDownValuePropToBeCompared.DecimalPlaces = 0;
            }
            else if (CPT.Type == eDataType.DOUBLE)
            {
                this.comboBoxPropertyComparison.Items.Add(">");
                this.comboBoxPropertyComparison.Items.Add("<");
                this.comboBoxPropertyComparison.Items.Add(">=");
                this.comboBoxPropertyComparison.Items.Add("<=");
                this.comboBoxPropertyComparison.Items.Add("=");
                this.comboBoxPropertyComparison.Items.Add("!=");

                this.TextBoxValuePropToBeCompared.Visible = false;
                this.numericUpDownValuePropToBeCompared.Visible = true;

                this.numericUpDownValuePropToBeCompared.Minimum = -9999999999999999;
                this.numericUpDownValuePropToBeCompared.Maximum = +9999999999999999;
                this.numericUpDownValuePropToBeCompared.DecimalPlaces = 4;


            }
            else if (CPT.Type == eDataType.TIME)
            {
                this.comboBoxPropertyComparison.Items.Add(">");
                this.comboBoxPropertyComparison.Items.Add("<");
                this.comboBoxPropertyComparison.Items.Add(">=");
                this.comboBoxPropertyComparison.Items.Add("<=");
                this.comboBoxPropertyComparison.Items.Add("=");
                this.comboBoxPropertyComparison.Items.Add("!=");

                this.TextBoxValuePropToBeCompared.Visible = false;
                this.numericUpDownValuePropToBeCompared.Visible = false;


            }

            this.comboBoxPropertyComparison.Text = this.comboBoxPropertyComparison.Items[0].ToString();


        }
    }
}
