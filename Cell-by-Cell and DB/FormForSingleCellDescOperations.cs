using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HCSAnalyzer.Cell_by_Cell_and_DB
{
    public partial class FormForDescOperations : Form
    {
        public FormForDescOperations(List<string> ListDescNames)
        {
            InitializeComponent();
            foreach (var item in ListDescNames)
            {
                    this.comboBoxDescriptor1.Items.Add(item);
                    this.comboBoxDescriptor2.Items.Add(item);
            }

            //if (this.comboBoxDescriptor1.Items.Count == 0) return;
            this.comboBoxDescriptor1.Text = this.comboBoxDescriptor1.Items[0].ToString();
            this.comboBoxDescriptor2.Text = this.comboBoxDescriptor2.Items[0].ToString();
        }

        private void comboBoxDescriptor1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDescName();
        }

        private void comboBoxDescriptor2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDescName();
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            UpdateDescName();
        }

        void UpdateDescName()
        {
            if (tabControlMain.SelectedTab.Name == "tabPageBinary")
            {
                this.textBoxNewDescName.Text = comboBoxDescriptor1.Text + " "
                                                + domainUpDown1.Text + " "
                                                + comboBoxDescriptor2.Text;
            }
            else
            {
                if (radioButtonSQRT.Checked)
                    this.textBoxNewDescName.Text = "Sqrt("+ comboBoxDescriptor1.Text + ")";
                else if(radioButtonLog.Checked)
                    this.textBoxNewDescName.Text = "Log(" + comboBoxDescriptor1.Text + ")";
                else if (radioButtonABS.Checked)
                    this.textBoxNewDescName.Text = "Abs(" + comboBoxDescriptor1.Text + ")";
                else if (radioButtonEXP.Checked)
                    this.textBoxNewDescName.Text = "Exp(" + comboBoxDescriptor1.Text + ")";

            }
        }

        private void radioButtonLog_CheckedChanged(object sender, EventArgs e)
        {
            richTextBoxInfo.Clear();
            richTextBoxInfo.AppendText("Return the base e logarithm of the values.\n\nAs the software requires double values, if the original data value is lower or equal to 0, the log value will be defined as Double.Epsilon.");
            UpdateDescName();
        }

        private void radioButtonSQRT_CheckedChanged(object sender, EventArgs e)
        {
            richTextBoxInfo.Clear();
            richTextBoxInfo.AppendText("Return the square root of the values.\n\nAs the software requires double values, if the original data value is lower than 0 the value will be defined as 0.");
            UpdateDescName();
        }

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDescName();
        }

        private void radioButtonABS_CheckedChanged(object sender, EventArgs e)
        {
            richTextBoxInfo.Clear();
            richTextBoxInfo.AppendText("Return the absolute value of the data.");
            UpdateDescName();
        }

        private void radioButtonEXP_CheckedChanged(object sender, EventArgs e)
        {
            richTextBoxInfo.Clear();
            richTextBoxInfo.AppendText("Return e raised to the specified power defined by the data value.");
            UpdateDescName();
        }

        private void checkBoxActivePostProcess_CheckedChanged(object sender, EventArgs e)
        {
            this.panelPostProcess.Enabled = this.checkBoxActivePostProcess.Checked;
        }

        private void domainUpDownPostProcessOperator_SelectedItemChanged(object sender, EventArgs e)
        {
            if ((domainUpDownPostProcessOperator.Text == "/") && (this.numericUpDownPostProcessValue.Value == 0))
                this.numericUpDownPostProcessValue.Value = 1;
        }

        private void numericUpDownPostProcessValue_ValueChanged(object sender, EventArgs e)
        {
            if ((domainUpDownPostProcessOperator.Text == "/") && (this.numericUpDownPostProcessValue.Value == 0))
                this.numericUpDownPostProcessValue.Value = 1;

        }



    }
}
