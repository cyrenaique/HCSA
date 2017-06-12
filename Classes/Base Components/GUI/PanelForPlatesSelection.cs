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
    class PanelForPlatesSelection : System.Windows.Forms.Panel
    {
        public List<System.Windows.Forms.CheckBox> ListCheckBoxes;
        public List<System.Windows.Forms.RadioButton> ListRadioButtons;
        

        public PanelForPlatesSelection(bool IsCheckBoxes, cListPlates InitialList, bool SelectOnlyActive)
        {
            
            if (InitialList == null)
                InitialList = cGlobalInfo.CurrentScreening.ListPlatesAvailable;

            int NumPlates = InitialList.Count;
            this.AutoScroll = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelForClassSelection_MouseDown);

            if (IsCheckBoxes)
                ListCheckBoxes = new List<System.Windows.Forms.CheckBox>();
            else
                ListRadioButtons = new List<System.Windows.Forms.RadioButton>();

            for (int IdxPlate = 0; IdxPlate < NumPlates; IdxPlate++)
            {
                if (IsCheckBoxes)
                {
                    System.Windows.Forms.CheckBox CurrentCheckBox = new System.Windows.Forms.CheckBox();
                    CurrentCheckBox.Text = InitialList[IdxPlate].GetName();
                    CurrentCheckBox.Width = 300;
                    CurrentCheckBox.Tag = InitialList[IdxPlate];
                    CurrentCheckBox.Location = new System.Drawing.Point(5, CurrentCheckBox.Height * IdxPlate);

                    if (SelectOnlyActive)
                    {
                        cPlate TmpPlate = cGlobalInfo.CurrentScreening.ListPlatesActive.FindPlate(InitialList[IdxPlate]);
                        if(TmpPlate==null)
                            CurrentCheckBox.Checked = false;
                        else
                            CurrentCheckBox.Checked = true;
                    }
                    else
                        CurrentCheckBox.Checked = true;

                    System.Windows.Forms.ToolTip ToolTipForPlate = new System.Windows.Forms.ToolTip();
                    ToolTipForPlate.SetToolTip(CurrentCheckBox, InitialList[IdxPlate].GetShortInfo());
                    CurrentCheckBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelForClassSelection_MouseDown);
                    ListCheckBoxes.Add(CurrentCheckBox);
                }
                else
                {
                    System.Windows.Forms.RadioButton CurrentRadioButton = new System.Windows.Forms.RadioButton();
                    CurrentRadioButton.Text = InitialList[IdxPlate].GetName();
                    CurrentRadioButton.Location = new System.Drawing.Point(5, CurrentRadioButton.Height * IdxPlate);
                    CurrentRadioButton.Checked = false;
                    System.Windows.Forms.ToolTip ToolTipForPlate = new System.Windows.Forms.ToolTip();
                    ToolTipForPlate.SetToolTip(CurrentRadioButton, InitialList[IdxPlate].GetShortInfo());
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



            if (sender.GetType() == typeof(System.Windows.Forms.CheckBox))
            {   
                contextMenuStripPicker.Items.Add(new ToolStripSeparator());
                System.Windows.Forms.CheckBox Box = (System.Windows.Forms.CheckBox)sender;
                cPlate CurrentPlate = (cPlate)Box.Tag;
                contextMenuStripPicker.Items.Add(CurrentPlate.GetExtendedContextMenu());
            }


            contextMenuStripPicker.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem CopyListToClipboard = new ToolStripMenuItem("Copy List to Clipboard");
            CopyListToClipboard.Click += new System.EventHandler(this.CopyListPlatesToClipBoard);
            contextMenuStripPicker.Items.Add(CopyListToClipboard);


            contextMenuStripPicker.Show(System.Windows.Forms.Control.MousePosition);
            //ToolStripMenuItem SelectAllItem = new ToolStripMenuItem("Select all");
            //SelectAllItem.Click += new System.EventHandler(this.SelectAllItem);
            //contextMenuStripActorPicker.Items.Add(SelectAllItem);
        }

        public cListPlates GetListSelectedPlates()
        {
            cListPlates LP = new cListPlates();

            foreach (var item in this.ListCheckBoxes)
            {
                if (item.Checked)
                    LP.Add((cPlate)item.Tag);
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

        void CopyListPlatesToClipBoard(object sender, EventArgs e)
        {
            string ForClipBoard = "";

            foreach (var item in this.ListCheckBoxes)
            {
                ForClipBoard += item.Text + "\t";

                if (item.Checked)
                    ForClipBoard += "active\n";
                else
                    ForClipBoard += "inactive\n";
            }

            Clipboard.SetText(ForClipBoard);

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
