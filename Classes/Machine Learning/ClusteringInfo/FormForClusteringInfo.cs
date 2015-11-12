using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Forms.FormsForOptions.ClassForOptions;
using HCSAnalyzer.Forms.FormsForOptions.ClassForOptions.Children;
using HCSAnalyzer.Classes;

namespace HCSAnalyzer.Forms.FormsForOptions
{
    public partial class FormForClusteringInfo : Form
    {
        cGlobalInfo GlobalInfo;
        cListClusteringAlgo ListClusteringAlgo;

        public FormForClusteringInfo(List<string> ListDescriptors, cGlobalInfo GlobalInfo)
        {
            InitializeComponent();
            this.GlobalInfo = GlobalInfo;
            this.treeViewForOptions.SelectedNode = this.treeViewForOptions.Nodes[0];
            ListClusteringAlgo = new cListClusteringAlgo(ListDescriptors);
        }

        private void treeViewForOptions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.panelForDisplay.Controls.Clear();
            string TagName= (string)e.Node.Tag;

            Panel PanelToDisp = ListClusteringAlgo.GetPanel(TagName);
            if (PanelToDisp == null) return;
            this.panelForDisplay.Controls.Add(PanelToDisp);

            if (TagName == "EM")
            {
                richTextBoxForInfo.Clear();
                richTextBoxForInfo.AppendText("Expectation Maximization (EM)\n------------------------------------------------------\nFor more information, go to: http://en.wikipedia.org/wiki/Expectation_maximization");
            }
            else if (TagName == "K-Means")
            {
                richTextBoxForInfo.Clear();
                richTextBoxForInfo.AppendText("K-Means\n------------------------------------------------------\nFor more information, go to: http://en.wikipedia.org/wiki/K-means_clustering");
            }
            else if (TagName == "Hierarchical")
            {
                richTextBoxForInfo.Clear();
                richTextBoxForInfo.AppendText("Hierarchical Clustering\n------------------------------------------------------\nNote: well suited for large signatures, but computationaly heavy regarding the number of experiments.\nFor more information, go to: http://en.wikipedia.org/wiki/Hierarchical_clustering");
            }
            else if (TagName == "FarthestFirst")
            {
                richTextBoxForInfo.Clear();
                richTextBoxForInfo.AppendText("Farthest First Clustering\n------------------------------------------------------\nFor more information:\n Hochbaum, Shmoys (1985).\nA best possible heuristic for the k-center problem.\nMathematics of Operations Research. 10(2):180-184.");
            }
            else if (TagName == "CobWeb")
            {
                richTextBoxForInfo.Clear();
                richTextBoxForInfo.AppendText("CobWeb Clustering\n------------------------------------------------------\nFor more information, go to: http://en.wikipedia.org/wiki/Cobweb_(clustering)");
            }            
            else if (TagName == "Manual")
            {
                richTextBoxForInfo.Clear();
                richTextBoxForInfo.AppendText("Manual Clustering\n------------------------------------------------------\nIn this mode, classes are defined by a descriptor.\nWarning: the descriptor number of values should be lower than the defined number of distinct cellular phenotypes.");
            }
        }

        public cParamAlgo GetSelectedAlgoAndParameters()
        {
            cParamAlgo ToReturn = ListClusteringAlgo.GetListParams((string)treeViewForOptions.SelectedNode.Tag);
            return ToReturn;
        }

        private void richTextBoxForInfo_LinkClicked(object sender, LinkClickedEventArgs e)
        {
          cGlobalInfo.WindowHCSAnalyzer.ClickOnLink(e.LinkText);
        }
    }

  

}
