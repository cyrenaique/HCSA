using System;
using System.Drawing;
using System.Windows.Forms;

namespace LibPlateAnalysis
{
    public partial class FormForDescriptorInfo : Form
    {

        public cDescriptorType CurrentDesc = null;

        int OriginalBinNumber;


        public FormForDescriptorInfo(cDescriptorType CurrentDesc)
        {
            this.CurrentDesc = CurrentDesc;
            InitializeComponent();
            this.textBoxNameDescriptor.Text = CurrentDesc.GetName();
            this.richTextBoxDescription.Text = CurrentDesc.description;
            this.labelDataType.Text = CurrentDesc.GetDataType();
            if (CurrentDesc.GetDataType() == "Single")
                this.numericUpDownBinValue.Visible = false;
            this.numericUpDownBinValue.Value = CurrentDesc.GetBinNumber();

            this.OriginalBinNumber = CurrentDesc.GetBinNumber();

            if (CurrentDesc.IsConnectedToDatabase)
            {
                this.labelDataBaseConnection.Text = "DataBase Connection.";
                panelForColor.BackColor = Color.LightGreen;
            }
            else
            {
                this.labelDataBaseConnection.Text = "No DataBase connection.";
                panelForColor.BackColor = Color.Red;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            if (CurrentDesc.GetName() != this.textBoxNameDescriptor.Text)
                this.CurrentDesc.ChangeName(this.textBoxNameDescriptor.Text);

            if (this.numericUpDownBinValue.Value != OriginalBinNumber)
                this.CurrentDesc.ChangeBinNumber((int)this.numericUpDownBinValue.Value);

            this.CurrentDesc.description = this.richTextBoxDescription.Text;
            //this.CurrentDesc.RefreshHisto(this.numericUpDownBinValue.Value);


        }


    }
}
