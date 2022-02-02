using HCSAnalyzer.Classes;
using System;
using System.Windows.Forms;

namespace HCSAnalyzer.Forms.ClusteringForms
{
    public partial class FormForHierarchical : Form
    {
        cGlobalInfo GlobalInfo;

        public FormForHierarchical(cGlobalInfo GlobalInfo)
        {
            InitializeComponent();
            this.GlobalInfo = GlobalInfo;
        }

        private void buttonOptions_Click(object sender, EventArgs e)
        {
            cGlobalInfo.OptionsWindow.tabControlWindowOption.SelectedTab = cGlobalInfo.OptionsWindow.tabPageClustering;

            // this.GlobalInfo.OptionsWindow.CurrentScreen = this.CurrentScreen;
            cGlobalInfo.OptionsWindow.Visible = true;
            cGlobalInfo.OptionsWindow.Update();
        }
    }
}
