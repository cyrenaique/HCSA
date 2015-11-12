using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes;

namespace HCSAnalyzer.Forms.IO
{
    public partial class FormForDescFiltering : Form
    {
        cGlobalInfo GlobalInfo;


        public FormForDescFiltering(cGlobalInfo GlobalInfo)
        {
            InitializeComponent();
            this.GlobalInfo = GlobalInfo;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            string FilterName = this.textBoxTextFilter.Text;
            bool ActionState1 = this.radioButtonIsSelect.Checked;


            if (this.checkBoxCaseSensitive.Checked == false)
            {
                FilterName = FilterName.ToLower();

                for (int i = 0; i < cGlobalInfo.WindowHCSAnalyzer.checkedListBoxActiveDescriptors.Items.Count; i++)
                {
                    if (cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName().ToLower().Contains(FilterName))
                    {
                        if((cGlobalInfo.CurrentScreening.ListDescriptors[i].IsConnectedToDatabase && (this.checkBoxSingle.Checked))||
                           (!cGlobalInfo.CurrentScreening.ListDescriptors[i].IsConnectedToDatabase && (this.checkBoxAverage.Checked)))
                        cGlobalInfo.CurrentScreening.ListDescriptors.SetItemState(i, ActionState1);
                    }
                }
            }
            else
            {
                for (int i = 0; i < cGlobalInfo.WindowHCSAnalyzer.checkedListBoxActiveDescriptors.Items.Count; i++)
                {
                    if (cGlobalInfo.CurrentScreening.ListDescriptors[i].GetName().Contains(FilterName))
                    {
                        if ((cGlobalInfo.CurrentScreening.ListDescriptors[i].IsConnectedToDatabase && (this.checkBoxSingle.Checked)) ||
                           (!cGlobalInfo.CurrentScreening.ListDescriptors[i].IsConnectedToDatabase && (this.checkBoxAverage.Checked)))
                        cGlobalInfo.CurrentScreening.ListDescriptors.SetItemState(i, ActionState1);
                    }
                }

            }
            cGlobalInfo.WindowHCSAnalyzer.RefreshInfoScreeningRichBox();

            if (cGlobalInfo.ViewMode == eViewMode.PIE)
                cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors[0], false);
        }
    }
}
