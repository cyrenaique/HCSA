
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using HCSAnalyzer.Classes;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace HCSAnalyzer.Forms.FormsForGraphsDisplay
{
    class PanelForPhenotypeEditing : System.Windows.Forms.Panel
    {
        public List<System.Windows.Forms.TextBox> ListTextBoxes;
        public List<System.Windows.Forms.Panel> ListPanelColor;
        cGlobalInfo GlobalInfo;

        public delegate void ChangedEventHandler(object sender, EventArgs e);
        public event ChangedEventHandler Changed;

        public PanelForPhenotypeEditing(cGlobalInfo GlobalInfo)
        {
            this.GlobalInfo = GlobalInfo;

            this.Changed += new ChangedEventHandler(PanelForPhenotypeEditing_Changed);

            int NumClass = cGlobalInfo.ListCellularPhenotypes.Count;
            this.Height = cGlobalInfo.OptionsWindow.panelForWellClasses.Height;
            this.AutoScroll = true;
            //this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelForClassSelection_MouseDown);
            ListTextBoxes = new List<System.Windows.Forms.TextBox>();
            ListPanelColor = new List<System.Windows.Forms.Panel>();

            for (int IdxClass = 0; IdxClass < NumClass; IdxClass++)
            {
                System.Windows.Forms.Panel PanelForColor = new System.Windows.Forms.Panel();
                PanelForColor.Width = 13;
                PanelForColor.Height = 13;
                PanelForColor.BackColor = cGlobalInfo.ListCellularPhenotypes[IdxClass].ColourForDisplay;
                PanelForColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                PanelForColor.Location = new System.Drawing.Point(5, PanelForColor.Height * IdxClass-2);
                PanelForColor.Tag = IdxClass;
                PanelForColor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MouseDoubleClick);
                ListPanelColor.Add(PanelForColor);
                 
                System.Windows.Forms.TextBox CurrentTextBox = new System.Windows.Forms.TextBox();
                CurrentTextBox.Text = cGlobalInfo.ListCellularPhenotypes[IdxClass].Name;// "Phenotype " + IdxClass;
                CurrentTextBox.Location = new System.Drawing.Point(PanelForColor.Width+15, (CurrentTextBox.Height+5) * IdxClass);

                ListToolTips.Add(new System.Windows.Forms.ToolTip());
                ListToolTips[IdxClass].SetToolTip(CurrentTextBox, CurrentTextBox.Text);
                
                CurrentTextBox.TextChanged += new EventHandler(CurrentTextBox_TextChanged);
                CurrentTextBox.Tag = IdxClass;
                ListTextBoxes.Add(CurrentTextBox);

                PanelForColor.Location = new System.Drawing.Point(5, CurrentTextBox.Location.Y+5);
            }
            this.Controls.AddRange(ListPanelColor.ToArray());
            this.Controls.AddRange(ListTextBoxes.ToArray());

            this.MouseDown += new MouseEventHandler(PanelForPhenotypeEditing_MouseDown);
        }

        void PanelForPhenotypeEditing_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1) return;

            if (e.Button != MouseButtons.Right) return;

            ContextMenuStrip NewMenu = new ContextMenuStrip();

            ToolStripMenuItem ToolStripMenuItem_CopyToClipboard = new ToolStripMenuItem("Copy To Clipboard");
            ToolStripMenuItem_CopyToClipboard.Click += new System.EventHandler(this.ToolStripMenuItem_CopyToClipboard);
            NewMenu.Items.Add(ToolStripMenuItem_CopyToClipboard);

            NewMenu.DropShadowEnabled = true;
            NewMenu.Show(System.Windows.Forms.Control.MousePosition);
        }

        void ToolStripMenuItem_CopyToClipboard(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);

            //Drawing control to the bitmap
            this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));
            MemoryStream ms = new MemoryStream();
            System.Windows.Forms.Clipboard.SetImage(bmp);            
        }

        void PanelForPhenotypeEditing_Changed(object sender, EventArgs e)
        {
        // //   throw new NotImplementedException();
        }

        List<System.Windows.Forms.ToolTip> ListToolTips = new List<System.Windows.Forms.ToolTip>();

        void CurrentTextBox_TextChanged(object sender, EventArgs e)
        {
            int Idx = (int)(((System.Windows.Forms.TextBox)sender).Tag);
            cGlobalInfo.ListCellularPhenotypes[Idx].Name = ((System.Windows.Forms.TextBox)sender).Text;

            ListToolTips[Idx].RemoveAll();
            ListToolTips[Idx].SetToolTip(((System.Windows.Forms.TextBox)sender), ((System.Windows.Forms.TextBox)sender).Text);

            Changed(this, e);
        }

        void MouseDoubleClick(object sender, EventArgs e)
        {
            int Idx = (int)(((System.Windows.Forms.Panel)sender).Tag);
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() != DialogResult.OK) return;
            Color backgroundColor = colorDialog.Color;
            ListPanelColor[Idx].BackColor = backgroundColor;

            cGlobalInfo.ListCellularPhenotypes[Idx].ColourForDisplay = backgroundColor;

            Changed(this, e);
        }

    }
}
