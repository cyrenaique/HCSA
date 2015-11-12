using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes._3D;
using HCSAnalyzer.Classes.ImageAnalysis._3D_Engine;

namespace HCSAnalyzer.Classes.Base_Components.Viewers._3D
{
    public partial class Form3DTreeView : Form
    {
        c3DNewWorld AssociatedWorld;

        public Form3DTreeView(c3DNewWorld AssociatedWorld)
        {
            InitializeComponent();
            this.AssociatedWorld = AssociatedWorld;
            this.treeViewFor3DObjects.NodeMouseClick += new TreeNodeMouseClickEventHandler(treeViewFor3DObjects_NodeMouseClick);
            this.treeViewFor3DObjects.MouseDown += new MouseEventHandler(treeViewFor3DObjects_MouseDown);
            RefreshTree();
        }

        void treeViewFor3DObjects_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1) return;

            if (e.Button != MouseButtons.Right) return;

            ContextMenuStrip NewMenu = new ContextMenuStrip();

            foreach (var item in GetBasicMenu())
            {
                NewMenu.Items.Add(item);
            }

            NewMenu.DropShadowEnabled = true;
            NewMenu.Show(Control.MousePosition);
        }

        public void RefreshTree()
        {
            this.treeViewFor3DObjects.Nodes.Clear();

            TreeNode TNObject = new TreeNode("3D Objects");

            foreach (var item in AssociatedWorld.ListObject)
            {
                TreeNode TmpNode = new TreeNode(item.GetName());

                cGeometric3DObject Obj = (cGeometric3DObject)item;

                if ((Obj.ParentTag != null) && (Obj.ParentTag.GetType() == typeof(cListGeometric3DObject)))
                {
                    cListGeometric3DObject ParentObjects = (cListGeometric3DObject)(Obj.ParentTag);
                }


                TmpNode.Tag = item;
                TNObject.Nodes.Add(TmpNode);
            }
            this.treeViewFor3DObjects.Nodes.Add(TNObject);


            TreeNode TN3DVolume = new TreeNode("3D Volumes");

            foreach (var item in AssociatedWorld.ListVolume)
            {
                TreeNode TmpNode = new TreeNode(/*item.GetName()*/"Test");
                TmpNode.Tag = item;
                TN3DVolume.Nodes.Add(TmpNode);
            }
            this.treeViewFor3DObjects.Nodes.Add(TN3DVolume);


            this.treeViewFor3DObjects.ExpandAll();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshTree();
        }

        private void treeViewFor3DObjects_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Clicks != 1) return;
            this.richTextBoxFor3DTreeViewInfo.Clear();

            if (e.Node.Tag != null)
            {
                try
                {
                    if (e.Node.Tag.GetType() == typeof(cVolumeRendering3D))
                    {
                        //foreach (var item in ((cVolumeRendering3D)e.Node.Tag).GetExtendedContextMenu())
                        //    NewMenu.Items.Add(item);
                    }
                    else
                    {
                        this.richTextBoxFor3DTreeViewInfo.AppendText(((cObject3D)e.Node.Tag).GetBasicInfo());
                    }
                }
                catch
                {
                
                }
            }



            if (e.Button != MouseButtons.Right) return;

            ContextMenuStrip NewMenu = new ContextMenuStrip();

            foreach (var item in GetBasicMenu())
            {
                NewMenu.Items.Add(item);
            }

            NewMenu.Items.Add(new ToolStripSeparator());
            if (e.Node.Tag == null) return;



            try
            {
                if (e.Node.Tag.GetType() == typeof(cVolumeRendering3D))
                {
                    foreach (var item in ((cVolumeRendering3D)e.Node.Tag).GetExtendedContextMenu())
                        NewMenu.Items.Add(item);
                }
                else
                {
                    foreach (var item in ((cObject3D)e.Node.Tag).GetExtendedContextMenu())
                        NewMenu.Items.Add(item);
                }

            }
            catch (Exception)
            {
                NewMenu.Show(Control.MousePosition);

            }


            NewMenu.Show(Control.MousePosition);
        }

        public List<ToolStripMenuItem> GetBasicMenu()
        {
            List<ToolStripMenuItem> NewMenu = new List<ToolStripMenuItem>();

            ToolStripMenuItem ToolStripMenuItem_CollapseAll = new ToolStripMenuItem("Collapse All");
            ToolStripMenuItem_CollapseAll.Click += new EventHandler(ToolStripMenuItem_CollapseAll_Click);
            NewMenu.Add(ToolStripMenuItem_CollapseAll);

            ToolStripMenuItem ToolStripMenuItem_ExpandAll = new ToolStripMenuItem("Expand All");
            ToolStripMenuItem_ExpandAll.Click += new EventHandler(ToolStripMenuItem_ExpandAll_Click);
            NewMenu.Add(ToolStripMenuItem_ExpandAll);

            ToolStripMenuItem ToolStripMenuItem_Refresh = new ToolStripMenuItem("Refresh");
            ToolStripMenuItem_Refresh.Click += new EventHandler(refreshToolStripMenuItem_Click);
            NewMenu.Add(ToolStripMenuItem_Refresh);

            return NewMenu;
        }

        void ToolStripMenuItem_ExpandAll_Click(object sender, EventArgs e)
        {
            this.treeViewFor3DObjects.ExpandAll();
        }

        void ToolStripMenuItem_CollapseAll_Click(object sender, EventArgs e)
        {
            this.treeViewFor3DObjects.CollapseAll();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }



    }
}
