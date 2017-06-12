using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Forms.FormsForOptions.PanelForOptions;
using HCSAnalyzer.Forms.FormsForOptions.ClassForOptions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace HCSAnalyzer.Forms.FormsForOptions
{
    public partial class FormForGlobalInfoOptions : Form
    {
        cListOptions ListOptions;

        public FormForGlobalInfoOptions(cListOptions ListOptions)
        {
            InitializeComponent();

           



            this.ListOptions = ListOptions;
        }

        TreeNode ToReturn = null;


        private void LoopRecursive(TreeNode treeNode, string TagName)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                if ((tn.Tag != null) && (tn.Tag == TagName))
                {
                    ToReturn = tn;
                    return;
                }
                LoopRecursive(tn, TagName);
            }
            return;
        }

        public void SelectOption(string TagName)
        {
            foreach (TreeNode item in treeViewForOptions.Nodes)
            {
                LoopRecursive(item, TagName);
            }

            if (ToReturn != null) treeViewForOptions.SelectedNode = ToReturn;
        }

        private void treeViewForOptions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.panelForDisplay.Controls.Clear();
            Panel PanelToDisp = ListOptions.GetPanel((string)e.Node.Tag);
            if (PanelToDisp == null) return;
            this.panelForDisplay.Controls.Add(PanelToDisp);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog CurrSavefileDialog = new SaveFileDialog();
            CurrSavefileDialog.Filter = "opt files (*.opt)|*.opt";
            DialogResult Res = CurrSavefileDialog.ShowDialog();
            if (Res != DialogResult.OK) return;

            if (CurrSavefileDialog.FileName == "") return;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(CurrSavefileDialog.FileName,
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, ListOptions);
            stream.Close();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog CurrOpenFileDialog = new OpenFileDialog();

            CurrOpenFileDialog.Filter = "opt files (*.opt)|*.opt";
            DialogResult Res = CurrOpenFileDialog.ShowDialog();
            if (Res != DialogResult.OK) return;

            if (CurrOpenFileDialog.FileName == "") return;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(CurrOpenFileDialog.FileName,
                                      FileMode.Open,
                                      FileAccess.Read,
                                      FileShare.Read);

           // MyObject obj = (MyObject)formatter.Deserialize(fromStream);
            stream.Close();



        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.treeViewForOptions.CollapseAll();
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.treeViewForOptions.ExpandAll();
        }
    }
}
