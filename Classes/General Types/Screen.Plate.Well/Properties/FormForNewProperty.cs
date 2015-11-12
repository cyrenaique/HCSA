using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibPlateAnalysis;

namespace HCSAnalyzer.Classes.General_Types.Screen.Plate.Well.Properties
{
    public partial class FormForNewProperty : Form
    {
        public FormForNewProperty()
        {
            InitializeComponent();

            comboBoxType.Items.Add(eDataType.DOUBLE);
            comboBoxType.Items.Add(eDataType.INTEGER);
            comboBoxType.Items.Add(eDataType.STRING);
            comboBoxType.Items.Add(eDataType.BOOL);

            comboBoxType.Text = eDataType.DOUBLE.ToString();

            this.numericUpDownNumberMin.Value = this.numericUpDownNumberMax.Minimum = this.numericUpDownNumberMin.Minimum = (decimal)cPropertyType.DefaultMin;
            this.numericUpDownNumberMax.Value = this.numericUpDownNumberMin.Maximum = this.numericUpDownNumberMax.Maximum = (decimal)cPropertyType.DefaultMax; 

        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            foreach (var item in cGlobalInfo.CurrentScreening.ListWellPropertyTypes)
            {
                if (item.Name == this.textBoxName.Text)
                {
                    MessageBox.Show("This property name already exists. Please choose a new a name !");
                    return;
                }

            }

            MessageBox.Show("Name is Ok !");

        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxType.Text==eDataType.DOUBLE.ToString()||comboBoxType.Text==eDataType.INTEGER.ToString())
            {
                this.panelForOptionsNumber.Visible = true;
                if(comboBoxType.Text==eDataType.DOUBLE.ToString())
                {
                    this.numericUpDownNumberMin.DecimalPlaces = 4;
                    this.numericUpDownNumberMax.DecimalPlaces = 4;
                }
                else
                {
                    this.numericUpDownNumberMin.DecimalPlaces = 0;
                    this.numericUpDownNumberMax.DecimalPlaces = 0;
                }


            }
            else
            {
                this.panelForOptionsNumber.Visible = false;
            }
               
        }

    }
}
