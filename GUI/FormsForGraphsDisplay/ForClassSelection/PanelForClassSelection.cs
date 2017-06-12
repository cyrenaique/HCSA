using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using HCSAnalyzer.Classes;
using System.Windows.Forms;
using HCSAnalyzer.Classes.Base_Classes.GUI;
using HCSAnalyzer.Classes.General_Types;
using System.Drawing;
using System.IO;

namespace HCSAnalyzer.Forms.FormsForGraphsDisplay
{

    public class PanelForListCheckBoxes : System.Windows.Forms.Panel
    {
        public List<System.Windows.Forms.CheckBox> ListCheckBoxes;
        public List<System.Windows.Forms.RadioButton> ListRadioButtons;
    
    }


    public class PanelForClassSelection : PanelForListCheckBoxes
    {
        public PanelForClassSelection(bool IsCheckBoxes, eClassType ClassType)
        {
            int NumClass = cGlobalInfo.ListWellClasses.Count;
            if (ClassType == eClassType.PHENOTYPE)
                NumClass = cGlobalInfo.ListCellularPhenotypes.Count;

            this.AutoScroll = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelForClassSelection_MouseDown);


            if (ClassType == eClassType.PHENOTYPE)
            {
                PanelForPhenotypeEditing PFPE = (PanelForPhenotypeEditing)(cGlobalInfo.OptionsWindow.panelForCellularPhenotypes.Controls[0]);
                PFPE.Changed += new PanelForPhenotypeEditing.ChangedEventHandler(PFPE_Changed);
            }    

             if (IsCheckBoxes)
                    ListCheckBoxes = new List<System.Windows.Forms.CheckBox>();
                else
                    ListRadioButtons = new List<System.Windows.Forms.RadioButton>();

                for (int IdxClass = 0; IdxClass < NumClass; IdxClass++)
                {
                    System.Windows.Forms.Panel PanelForColor = new System.Windows.Forms.Panel();
                    PanelForColor.Width = 13;
                    PanelForColor.Height = 13;
                    if (ClassType == eClassType.WELL)
                        PanelForColor.BackColor = cGlobalInfo.ListWellClasses[IdxClass].ColourForDisplay;
                    else if (ClassType == eClassType.PHENOTYPE)
                        PanelForColor.BackColor = cGlobalInfo.ListCellularPhenotypes[IdxClass].ColourForDisplay;

                    PanelForColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    PanelForColor.Location = new System.Drawing.Point(5, PanelForColor.Height * IdxClass);
                    PanelForColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelForClassSelection_MouseDown);

                    if (IsCheckBoxes)
                    {
                        System.Windows.Forms.CheckBox CurrentCheckBox = new System.Windows.Forms.CheckBox();
                        if (ClassType == eClassType.WELL)
                            CurrentCheckBox.Text = cGlobalInfo.ListWellClasses[IdxClass].Name;
                        else if (ClassType == eClassType.PHENOTYPE)
                            CurrentCheckBox.Text = cGlobalInfo.ListCellularPhenotypes[IdxClass].Name;

                        CurrentCheckBox.Location = new System.Drawing.Point(PanelForColor.Width + 15, CurrentCheckBox.Height * IdxClass);
                        CurrentCheckBox.Checked = true;
                        CurrentCheckBox.CheckedChanged += new EventHandler(CurrentCheckBox_CheckedChanged);
                        CurrentCheckBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelForClassSelection_MouseDown);

                        System.Windows.Forms.ToolTip TT = new System.Windows.Forms.ToolTip();
                        TT.SetToolTip(CurrentCheckBox, CurrentCheckBox.Text);
                        

                        ListCheckBoxes.Add(CurrentCheckBox);
                        PanelForColor.Location = new System.Drawing.Point(5, CurrentCheckBox.Location.Y + 5);
                    }
                    else
                    {
                        System.Windows.Forms.RadioButton CurrentRadioButton = new System.Windows.Forms.RadioButton();
                        if (ClassType == eClassType.WELL)
                              CurrentRadioButton.Text = cGlobalInfo.ListWellClasses[IdxClass].Name;
                        else if (ClassType == eClassType.PHENOTYPE)
                            CurrentRadioButton.Text = cGlobalInfo.ListCellularPhenotypes[IdxClass].Name;
                        CurrentRadioButton.Location = new System.Drawing.Point(PanelForColor.Width + 15, CurrentRadioButton.Height * IdxClass);
                        CurrentRadioButton.Checked = false;
                        CurrentRadioButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelForClassSelection_MouseDown);

                        System.Windows.Forms.ToolTip TT = new System.Windows.Forms.ToolTip();
                        TT.SetToolTip(CurrentRadioButton, CurrentRadioButton.Text);


                        ListRadioButtons.Add(CurrentRadioButton);
                        PanelForColor.Location = new System.Drawing.Point(5, CurrentRadioButton.Location.Y + 5);
                    }
                    this.Controls.Add(PanelForColor);
                }

                if (IsCheckBoxes)
                    this.Controls.AddRange(ListCheckBoxes.ToArray());
                else
                    this.Controls.AddRange(ListRadioButtons.ToArray());

             // (PanelForPhenotypeEditing)cGlobalInfo.OptionsWindow.panelForCellularPhenotypes.Controls[0].Pane


        }

        void PFPE_Changed(object sender, EventArgs e)
        {
            int IdxColorChanged = 0;
            if (ListCheckBoxes == null) return;
            if (this.Controls.Count == 0) return;

            for (int IdxClass = 0; IdxClass < cGlobalInfo.ListCellularPhenotypes.Count; IdxClass++)
            {
                ListCheckBoxes[IdxClass].Text = cGlobalInfo.ListCellularPhenotypes[IdxClass].Name;
                System.Windows.Forms.Panel PanelForColor = (System.Windows.Forms.Panel)this.Controls[IdxClass];
                if (PanelForColor.BackColor != cGlobalInfo.ListCellularPhenotypes[IdxClass].ColourForDisplay)
                {
                    PanelForColor.BackColor = cGlobalInfo.ListCellularPhenotypes[IdxClass].ColourForDisplay;
                    IdxColorChanged++;
                }
            }

            if (IdxColorChanged > 0) SelectionChanged(this, e);
        }

        //public delegate void SelectionChangedEventHandler(object sender, EventArgs e);
        public delegate void ChangedEventHandler(object sender, EventArgs e);
        public event ChangedEventHandler SelectionChanged;

        public void CurrentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectionChanged == null) return;
            SelectionChanged(this, e);
        }

        private void PanelForClassSelection_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.ListCheckBoxes==null) return;

            // right button clicked... context menu
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenuStrip contextMenuStripPicker = new ContextMenuStrip();

                ToolStripMenuItem SelectItem = new ToolStripMenuItem("Select all");
                SelectItem.Click += new System.EventHandler(this.SelectItem);
                contextMenuStripPicker.Items.Add(SelectItem);

                ToolStripMenuItem UnselectItem = new ToolStripMenuItem("Unselect all");
                UnselectItem.Click += new System.EventHandler(this.UnselectItem);
                contextMenuStripPicker.Items.Add(UnselectItem);

                contextMenuStripPicker.Items.Add(new ToolStripSeparator());

                ToolStripMenuItem CopyToClipBoard = new ToolStripMenuItem("Copy To Clipboard");
                CopyToClipBoard.Click += new System.EventHandler(this.CopyToClipBoard);
                contextMenuStripPicker.Items.Add(CopyToClipBoard);


                contextMenuStripPicker.Show(System.Windows.Forms.Control.MousePosition);
            }
        }

        void CopyToClipBoard(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);

            //Drawing control to the bitmap
            this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));
            MemoryStream ms = new MemoryStream();
            System.Windows.Forms.Clipboard.SetImage(bmp);
        }

        public List<int> GetListIndexSelectedClass()
        {
            List<int> SelectedClass = new List<int>();
            int Idx = 0;

            if (this.ListCheckBoxes != null)
            {
                foreach (var item in this.ListCheckBoxes)
                {
                    if (item.Checked)
                        SelectedClass.Add(Idx);
                    Idx++;
                }
            }
            else
            {
                foreach (var item in this.ListRadioButtons)
                {
                    if (item.Checked)
                        SelectedClass.Add(Idx);
                    Idx++;
                }
            }

                return SelectedClass;
        }
  
        public List<cWellClassType> GetListSelectedClassTypes()
        {
            List<cWellClassType> SelectedClass = new List<cWellClassType>();

            int IDx =0;

            if (this.ListCheckBoxes != null)
            {
                
                foreach (var item in this.ListCheckBoxes)
                {
                    if (item.Checked)
                        SelectedClass.Add(cGlobalInfo.ListWellClasses[IDx]);

                    IDx++;
                }
            }
            else
            {
           foreach (var item in this.ListRadioButtons)
                {
                    if (item.Checked)
                        SelectedClass.Add(cGlobalInfo.ListWellClasses[IDx]);

                    IDx++;
                }  
            
            }
            return SelectedClass;
        }
        
        public List<bool> GetListSelectedClass()
        {
            List<bool> SelectedClass = new List<bool>();

            if (this.ListCheckBoxes != null)
            {
                foreach (var item in this.ListCheckBoxes)
                {
                    if (item.Checked)
                        SelectedClass.Add(true);
                    else
                        SelectedClass.Add(false);
                }
            }
            else
            {
           foreach (var item in this.ListRadioButtons)
                {
                    if (item.Checked)
                        SelectedClass.Add(true);
                    else
                        SelectedClass.Add(false);
                }  
            
            }
            return SelectedClass;
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
