using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;

namespace HCSAnalyzer.Classes.Base_Classes.GUI
{
    public partial class FormForStatDesc : Form
    {
        cGlobalInfo GlobalInfo;

        public FormForStatDesc(cGlobalInfo GlobalInfo)
        {
            this.GlobalInfo = GlobalInfo;
            InitializeComponent();
        }

        private void comboBoxStatistics_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FinalName = comboBoxStatistics.Text + "(" ;

            PanelForClassSelection CurrentPanel = (PanelForClassSelection)this.panelForSubPopulation.Controls[0];
            List<bool> ListRes = CurrentPanel.GetListSelectedClass();

            int Idx = 0;
            int IdxName =0;
            foreach (var item in CurrentPanel.GetListSelectedClass())
            {
                if(item)
                {
                    FinalName += cGlobalInfo.ListCellularPhenotypes[Idx].Name +",";
                    IdxName++;
                }
                Idx++;
            }

            if (IdxName > 0)
                FinalName = FinalName.Remove(FinalName.Length - 1);

            this.textBoxDescName.Text = FinalName + ")";
            
        }
    }
}
