using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Forms.FormsForOptions.ClassForOptions.Children;
using weka.classifiers.functions.supportVector;

namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{

    public partial class FormForClassificationInfo : Form
    {
        cGlobalInfo GlobalInfo;
        cListClassificationAlgo ListClassificationAlgo = null;
        public Kernel GeneratedKernel;

        public FormForClassificationInfo(cGlobalInfo GlobalInfo)
        {
            InitializeComponent();
            this.GlobalInfo = GlobalInfo;
            this.GeneratedKernel = new RBFKernel();
            ((RBFKernel)this.GeneratedKernel).setGamma(1.0);
            ListClassificationAlgo = new cListClassificationAlgo(this);

            this.treeViewForOptions.ExpandAll();
            this.treeViewForOptions.SelectedNode = this.treeViewForOptions.Nodes[0].Nodes[0];
        
            this.treeViewForOptions.SelectedNode.EnsureVisible();  //scroll if necessary
            this.treeViewForOptions.SelectedNode.Checked = true;
          //  this.treeViewForOptions.Focus();
        }

        private void treeViewForOptions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.panelForDisplay.Controls.Clear();
            string TagName = (string)e.Node.Tag;
            if (TagName == null) return;
            Panel PanelToDisp = ListClassificationAlgo.GetPanel(TagName);
            if (PanelToDisp == null) return;
            this.panelForDisplay.Controls.Add(PanelToDisp);

            if (TagName == "J48")
            {
                richTextBoxForInfo.Clear();
                richTextBoxForInfo.AppendText("J48 (C4.5)\n------------------------------------------------------\nFor more information, go to: http://en.wikipedia.org/wiki/C4.5_algorithm");
            }
            else if (TagName == "RandomForest")
            {
                richTextBoxForInfo.Clear();
                richTextBoxForInfo.AppendText("Random Forest\n------------------------------------------------------\nFor more information, go to: http://en.wikipedia.org/wiki/Random_forest");
            }
            else if (TagName == "RandomTree")
            {
                richTextBoxForInfo.Clear();
                richTextBoxForInfo.AppendText("Random Tree\n------------------------------------------------------\nFor more information, go to: http://en.wikipedia.org/wiki/Random_forest");
            }
            else if (TagName == "KStar")
            {
                richTextBoxForInfo.Clear();
                richTextBoxForInfo.AppendText("K*\n------------------------------------------------------\nFor more information, go to: http://wiki.pentaho.com/display/DATAMINING/KStar");
            }
            else if (TagName == "SVM")
            {
                richTextBoxForInfo.Clear();
                richTextBoxForInfo.AppendText("SVM (Support Vector Machine)\n------------------------------------------------------\nFor more information, go to: http://en.wikipedia.org/wiki/Support_vector_machine");
            }
            else if (TagName == "KNN")
            {
                richTextBoxForInfo.Clear();
                richTextBoxForInfo.AppendText("KNN\n------------------------------------------------------\nK-nearest neighbours classifier. Can select appropriate value of K based on cross-validation. Can also do distance weighting.\nFor more information, go to: http://en.wikipedia.org/wiki/K-nearest_neighbor_algorithm");
            }  
            else if (TagName == "Perceptron")
            {
                richTextBoxForInfo.Clear();
                richTextBoxForInfo.AppendText("Multilayer Perceptron\n------------------------------------------------------\nFor more information, go to: http://en.wikipedia.org/wiki/Multilayer_perceptron");
            }
            else if (TagName == "ZeroR")
            {
                richTextBoxForInfo.Clear();
                richTextBoxForInfo.AppendText("Zero R(ule) classifier\n------------------------------------------------------\nFor more information, go to: http://weka.wikispaces.com/ZeroR");
            }  
            else if (TagName == "OneR")
            {
                richTextBoxForInfo.Clear();
                richTextBoxForInfo.AppendText("One R(ule) classifier\n------------------------------------------------------\nFor more information, go to: http://en.wikipedia.org/wiki/Decision_stump");
            }
            else if (TagName == "NaiveBayes")
            {
                richTextBoxForInfo.Clear();
                richTextBoxForInfo.AppendText("Naive Bayes classifier\n------------------------------------------------------\nFor more information, go to: http://en.wikipedia.org/wiki/Naive_Bayes_classifier");
            }
            else if (TagName == "Logistic")
            {
                richTextBoxForInfo.Clear();
                richTextBoxForInfo.AppendText("Logistic classifier\n------------------------------------------------------\nFor more information, go to: http://weka.sourceforge.net/doc.dev/weka/classifiers/functions/Logistic");
            }
        }

        public cParamAlgo GetSelectedAlgoAndParameters()
        {
            cParamAlgo ToReturn = ListClassificationAlgo.GetListParams((string)treeViewForOptions.SelectedNode.Tag);
            return ToReturn;
        }

        private void richTextBoxForInfo_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            cGlobalInfo.WindowHCSAnalyzer.ClickOnLink(e.LinkText);
        }

        private void checkBoxCrossValidation_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownFoldNumber.Enabled = checkBoxCrossValidation.Checked;
        }

    }
}
