using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HCSAnalyzer.Forms.IO
{
    public partial class FormInfoForFileImporter : Form
    {
        string FileName;

        public FormInfoForFileImporter(string FileName)
        {
            InitializeComponent();
            this.FileName = FileName;
            this.Text += " - " + FileName;
        }

        public string GetDelimiter()
        {
            if (this.radioButtonComma.Checked)
                return ",";
            else if (this.radioButtonSemiColon.Checked)
                return ";";
            else if (this.radioButtonSpace.Checked)
                return " ";
            else if (this.radioButtonTab.Checked)
                return "\t";

            return "";
        }


        private void numericUpDownHeaderSize_ValueChanged(object sender, EventArgs e)
        {
            string line;

            System.IO.StreamReader file =  new System.IO.StreamReader(FileName);
            this.richTextBoxTextHeader.Clear();

            for (int i = 0; i < this.numericUpDownHeaderSize.Value; i++)
            {
                line = file.ReadLine();
                if (line == null)
                {
                    this.numericUpDownHeaderSize.Value = (decimal)(i - 1);
                    this.numericUpDownHeaderSize.Maximum = (decimal)(i - 1);
                    break;
                }
                this.richTextBoxTextHeader.AppendText(line+"\n");
            }

            file.Close();

          
        }
    }
}
