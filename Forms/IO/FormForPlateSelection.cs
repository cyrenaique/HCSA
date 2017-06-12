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
    public partial class FormForPlateSelection : Form
    {
        public FormForPlateSelection(string[] ListNames, bool IsMultipleSelection)
        {
            InitializeComponent();

            MaintreeView.CheckBoxes = IsMultipleSelection;

            for (int i = 0; i < ListNames.Length; i++)
            {
                string CurrentNode = ListNames[i];

                string[] ShortListNames = CurrentNode.Split('\\');
                TreeNode TN = new TreeNode(ShortListNames[ShortListNames.Length-1]);

                TN.Tag = CurrentNode;
                TN.Checked = true;//IsMultipleSelection;
                TN.ToolTipText = CurrentNode;

                this.MaintreeView.Nodes.Add(TN);
            }
            this.MaintreeView.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(MaintreeView_NodeMouseDoubleClick);
        }

        void MaintreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                // Look for a file extension. 
                //if (e.Node.Text.Contains("."))
                string filename = (string)e.Node.Tag;
                //Path.Split('\\');
                //string FinalPath = 

                System.Diagnostics.Process.Start("explorer.exe", "/select," + filename);

                    //System.Diagnostics.Process.Start();
            }
            // If the file is not found, handle the exception and inform the user. 
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("File not found.");
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TreeNode item in this.MaintreeView.Nodes)
                item.Checked = true;
        }

        private void unselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TreeNode item in this.MaintreeView.Nodes)
                item.Checked = false;

        }

        private void inverseSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TreeNode item in this.MaintreeView.Nodes)
                item.Checked = !item.Checked;
        }


        public string[] GetListPlatesSelected()
        {
            List<string> ToReturn = new List<string>();

            if (MaintreeView.CheckBoxes)
            {

                foreach (TreeNode item in this.MaintreeView.Nodes)
                {
                    if (item.Checked)
                        ToReturn.Add((string)(item.Tag));
                }
            }
            else
            {
                foreach (TreeNode item in this.MaintreeView.Nodes)
                {
                    if (item.IsSelected)
                    {
                        ToReturn.Add((string)(item.Tag));
                        return ToReturn.ToArray();
                    }

                }

                ToReturn.Add((string)(this.MaintreeView.Nodes[0].Tag));

            
            }

            return ToReturn.ToArray();

        }

        private void MaintreeView_DoubleClick(object sender, EventArgs e)
        {
            
        }

    }
}
