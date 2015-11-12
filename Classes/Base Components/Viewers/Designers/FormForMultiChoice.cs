using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.General_Types;
using LibPlateAnalysis;

namespace HCSAnalyzer.Classes.Base_Components.Viewers.Designers
{
    public partial class FormForMultiChoice : Form
    {
        List<cExtendedControl> xListControl = new List<cExtendedControl>();

        public FormForMultiChoice(List<cExtendedControl> xListControl)
        {
            this.xListControl = xListControl;

            InitializeComponent();

            this.listBoxTitle.DisplayMember = "UserName";
            this.listBoxTitle.ValueMember = "IncludedObject";

            foreach (var item in this.xListControl)
            {
                cItemForListBox TmpItem = new cItemForListBox(item);
                TmpItem.UserName = item.Title;
                this.listBoxTitle.Items.Add(TmpItem);
            }

            xListControl[0].Width = this.splitContainer.Panel1.Width;
            xListControl[0].Height = this.splitContainer.Panel1.Height;
            xListControl[0].Controls[0].Width = xListControl[0].Width;
            xListControl[0].Controls[0].Height = xListControl[0].Height;

            xListControl[0].Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                                                      | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.splitContainer.Panel1.Controls.Add(xListControl[0]);
        }

        private void listBoxTitle_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.splitContainer.Panel1.Controls.Clear();
            for (int i = 0; i < this.xListControl.Count; i++)
            {
                if (((ListBox)sender).SelectedItem == null) return;

                cItemForListBox IFLB = (cItemForListBox)((ListBox)sender).SelectedItem;

                if (xListControl[i].Title == IFLB.UserName)
                {
                    xListControl[i].Visible = false;
                    xListControl[i].Width = this.splitContainer.Panel1.Width;
                    xListControl[i].Height = this.splitContainer.Panel1.Height;
                    xListControl[i].Controls[0].Width = xListControl[i].Width;
                    xListControl[i].Controls[0].Height = xListControl[i].Height;

                    xListControl[i].Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                            | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);

                    this.splitContainer.Panel1.Controls.Add(xListControl[i]);
                    xListControl[i].Visible = true;
                    return;

                }

            }




        }

        private void listBoxTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == System.Windows.Forms.MouseButtons.Right) && (sender != null))
            {
                //Point locationOnForm = listViewForCellPopulations.FindForm().PointToClient(Control.MousePosition);
                //ListViewItem IdxItem = listBoxTitle.GetItemAt(e.X, e.Y);
                ContextMenuStrip contextMenuStripPicker = new ContextMenuStrip();


                try
                {
                    cItemForListBox a = (cItemForListBox)((ListBox)(sender)).SelectedItem;
                    cExtendedControl EC = ((cExtendedControl)a.IncludedObject);

                    if (EC.Tag == null) return;


                    if (EC.Tag.GetType() == typeof(cPlate))
                    {
                        contextMenuStripPicker.Items.Add(((cPlate)EC.Tag).GetExtendedContextMenu());
                    }
                    else if (EC.Tag.GetType() == typeof(cWell))
                    {
                        foreach (var item in ((cWell)EC.Tag).GetExtendedContextMenu())
                            contextMenuStripPicker.Items.Add(item);
                    }
                    else
                        return;

                }
                catch (Exception)
                {
                    return;
                }

                //if (listViewForClassifHistory.Items.Count > 0)
                //{
                //    ToolStripMenuItem DisplayGraphItem = new ToolStripMenuItem("Display Graph");
                //    DisplayGraphItem.Click += new System.EventHandler(this.DisplayGraphItem);
                //    contextMenuStripPicker.Items.Add(DisplayGraphItem);

                //    if (listViewForClassifHistory.Items.Count > 1)
                //    {
                //        ToolStripMenuItem IdentifyBestItem = new ToolStripMenuItem("Identify Best");
                //        IdentifyBestItem.Click += new System.EventHandler(this.IdentifyBestItem);
                //        contextMenuStripPicker.Items.Add(IdentifyBestItem);
                //    }
                //    contextMenuStripPicker.Items.Add(new ToolStripSeparator());


                //}

                contextMenuStripPicker.Show(Control.MousePosition);
            }


        }
    }
}
