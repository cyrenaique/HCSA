using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Simulator.Classes;

namespace HCSAnalyzer.Simulator.Forms
{

    public partial class FormForVariableDef : Form
    {
        public cClassForVariable thisVariable;

        public FormForVariableDef(cClassForVariable thisVariable)
        {
            InitializeComponent();
            this.thisVariable = thisVariable;
            this.radioButtonConstant.Checked = thisVariable.IsConstant;
            this.radioButtonVariable.Checked = !thisVariable.IsConstant;
            this.checkBoxRandom.Checked = thisVariable.IsVariableRandom;
            this.checkBoxProportionalToCol.Checked = thisVariable.IsVariableAlongColumns;
            this.checkBoxProportionalToRow.Checked = thisVariable.IsVariableAlongRows;
            this.numericUpDownIncrement.Value = (decimal)thisVariable.Increment;
        }

        private void radioButtonVariable_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGUI();
        }

        private void radioButtonConstant_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGUI();
        }

        void RefreshGUI()
        {
            groupBoxVariableSpec.Enabled = radioButtonVariable.Checked;
        }

        private void checkBoxRandom_CheckedChanged(object sender, EventArgs e)
        {
            FormForRandSpecOfVar WindowForRandSpec = new FormForRandSpecOfVar(this.thisVariable.RandomInfo);
            if (WindowForRandSpec.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                thisVariable.RandomInfo = WindowForRandSpec.RandomParam;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.thisVariable.IsConstant = this.radioButtonConstant.Checked;
            this.thisVariable.IsVariableAlongColumns = this.checkBoxProportionalToCol.Checked;
            this.thisVariable.IsVariableAlongRows = this.checkBoxProportionalToRow.Checked;
            this.thisVariable.IsVariableRandom = this.checkBoxRandom.Checked;
            this.thisVariable.Increment = (double)this.numericUpDownIncrement.Value;
        }

    }
}
