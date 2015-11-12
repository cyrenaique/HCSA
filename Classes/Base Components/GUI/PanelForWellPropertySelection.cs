using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using HCSAnalyzer.Classes;
using System.Windows.Forms;
using HCSAnalyzer.Classes.Base_Classes.GUI;
using HCSAnalyzer.Classes.General_Types;
using LibPlateAnalysis;

namespace HCSAnalyzer.Forms.FormsForGraphsDisplay
{
    class PanelForWellPropertySelection : System.Windows.Forms.Panel
    {
        public List<System.Windows.Forms.CheckBox> ListCheckBoxes;
        public List<System.Windows.Forms.RadioButton> ListRadioButtons;


        public PanelForWellPropertySelection(bool IsCheckBoxes)
        {
            
            this.AutoScroll = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelForClassSelection_MouseDown);

            if (IsCheckBoxes)
                ListCheckBoxes = new List<System.Windows.Forms.CheckBox>();
            else
                ListRadioButtons = new List<System.Windows.Forms.RadioButton>();

            
            for (int Idx = 0; Idx < cGlobalInfo.CurrentScreening.ListWellPropertyTypes.Count; Idx++)
            {
                cPropertyType CurrentType = cGlobalInfo.CurrentScreening.ListWellPropertyTypes[Idx];
                if (IsCheckBoxes)
                {
                    System.Windows.Forms.CheckBox CurrentCheckBox = new System.Windows.Forms.CheckBox();
                    CurrentCheckBox.Text = CurrentType.Name;
                    CurrentCheckBox.Width = 300;
                    CurrentCheckBox.Tag = CurrentType;
                    CurrentCheckBox.Location = new System.Drawing.Point(5, CurrentCheckBox.Height * Idx);

                   CurrentCheckBox.Checked = true;

                    System.Windows.Forms.ToolTip ToolTipForPlate = new System.Windows.Forms.ToolTip();
                    ToolTipForPlate.SetToolTip(CurrentCheckBox, CurrentType.GetInfo());
                    CurrentCheckBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelForClassSelection_MouseDown);
                    ListCheckBoxes.Add(CurrentCheckBox);
                }
                else
                {
                    System.Windows.Forms.RadioButton CurrentRadioButton = new System.Windows.Forms.RadioButton();
                    CurrentRadioButton.Text = CurrentType.Name;
                    CurrentRadioButton.Width = 300;
                    CurrentRadioButton.Tag = CurrentType;
                    CurrentRadioButton.Location = new System.Drawing.Point(5, CurrentRadioButton.Height * Idx);
                    CurrentRadioButton.Checked = false;
                    System.Windows.Forms.ToolTip ToolTipForPlate = new System.Windows.Forms.ToolTip();
                    ToolTipForPlate.SetToolTip(CurrentRadioButton, CurrentType.GetInfo());
                    CurrentRadioButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelForClassSelection_MouseDown);
                    ListRadioButtons.Add(CurrentRadioButton);
                }
            }

            if (IsCheckBoxes)
                this.Controls.AddRange(ListCheckBoxes.ToArray());
            else
                this.Controls.AddRange(ListRadioButtons.ToArray());

        }


        private void PanelForClassSelection_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button != System.Windows.Forms.MouseButtons.Right) || (this.ListRadioButtons != null)) return;

            ContextMenuStrip contextMenuStripPicker = new ContextMenuStrip();

            ToolStripMenuItem SelectItem = new ToolStripMenuItem("Select all");
            SelectItem.Click += new System.EventHandler(this.SelectItem);
            contextMenuStripPicker.Items.Add(SelectItem);

            ToolStripMenuItem UnselectItem = new ToolStripMenuItem("Unselect all");
            UnselectItem.Click += new System.EventHandler(this.UnselectItem);
            contextMenuStripPicker.Items.Add(UnselectItem);

         

            //if (sender.GetType() == typeof(System.Windows.Forms.CheckBox))
            //{   
            //    contextMenuStripPicker.Items.Add(new ToolStripSeparator());
            //    System.Windows.Forms.CheckBox Box = (System.Windows.Forms.CheckBox)sender;
            //    cPlate CurrentPlate = (cPlate)Box.Tag;
            //    contextMenuStripPicker.Items.Add(CurrentPlate.GetExtendedContextMenu());
            //}


            contextMenuStripPicker.Show(System.Windows.Forms.Control.MousePosition);
            //ToolStripMenuItem SelectAllItem = new ToolStripMenuItem("Select all");
            //SelectAllItem.Click += new System.EventHandler(this.SelectAllItem);
            //contextMenuStripActorPicker.Items.Add(SelectAllItem);
        }

        public List<cPropertyType> GetListSelectedProperties()
        {
            List<cPropertyType> LP = new List<cPropertyType>();


            if (this.ListCheckBoxes == null)
            {
                foreach (var item in this.ListRadioButtons)
                {
                    if (item.Checked)
                        LP.Add((cPropertyType)item.Tag);
                }

            }
            else
            {
                foreach (var item in this.ListCheckBoxes)
                {
                    if (item.Checked)
                        LP.Add((cPropertyType)item.Tag);
                }
            }
            return LP;
        }

        public void UnSelectAll()
        {
            if (this.ListCheckBoxes != null)
            {

                foreach (var item in this.ListCheckBoxes)
                    item.Checked = false;
            }
        }

        public void SelectAll()
        {
            foreach (var item in this.ListCheckBoxes)
                item.Checked = true;
        }

        public void Select(int SelectedIdx)
        {
            int Idx = 0;

            if (this.ListCheckBoxes != null)
            {
                foreach (var item in this.ListCheckBoxes)
                {
                    if (Idx == SelectedIdx)
                    {
                        item.Checked = true;
                        return;
                    }
                    Idx++;
                }
            }
            else
            {
                foreach (var item in this.ListRadioButtons)
                {
                    if (Idx == SelectedIdx)
                    {
                        item.Checked = true;
                        return;
                    }
                    Idx++;
                }

            }
        }

        void UnselectItem(object sender, EventArgs e)
        {
            UnSelectAll();
        }

        void SelectItem(object sender, EventArgs e)
        {
            foreach (var item in this.ListCheckBoxes)
                item.Checked = true;
        }

    }
}
