using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes;

namespace HCSAnalyzer.Forms.FormsForOptions
{
    public partial class radioButtonUISelectionModeTotalIntensity : Form
    {
        public radioButtonUISelectionModeTotalIntensity()
        {
            InitializeComponent();
        }

        private void checkBoxDRCDigitNumberAutomated_CheckedChanged(object sender, EventArgs e)
        {
            this.numericUpDownDRCDigitNumber.Enabled = !checkBoxDRCDigitNumberAutomated.Checked;
        }

        public int GetDRCNumberOfDigit()
        {
            if (this.checkBoxDRCDigitNumberAutomated.Checked) return -1;
            else
                return (int)this.numericUpDownDRCDigitNumber.Value;
        }

        private void checkBoxZscoreMinValueAutomated_CheckedChanged(object sender, EventArgs e)
        {
            this.numericUpDownZscoreMinValue.Enabled = !this.checkBoxZscoreMinValueAutomated.Checked;
        }

        private void panelFor3DBackColor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ColorDialog CD = new ColorDialog();
            CD.Color = panelFor3DBackColor.BackColor;
            if (CD.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            panelFor3DBackColor.BackColor = CD.Color;
        }

        private void panelFontColor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ColorDialog CD = new ColorDialog();
            CD.Color = panelFontColor.BackColor;
            if (CD.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            panelFontColor.BackColor = CD.Color;
        }

        private void textBoxScreeningName_TextChanged(object sender, EventArgs e)
        {
            if (cGlobalInfo.CurrentScreening == null) return;
            cGlobalInfo.CurrentScreening.SetName(textBoxScreeningName.Text);
        }
    }
}
