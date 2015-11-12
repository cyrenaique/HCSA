

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
    class PanelForClassEditing : System.Windows.Forms.Panel
    {
        public List<System.Windows.Forms.TextBox> ListTextBoxes;
        public List<System.Windows.Forms.Panel> ListPanelColor;
        cGlobalInfo GlobalInfo;
        List<System.Windows.Forms.ToolTip> ListToolTips = new List<System.Windows.Forms.ToolTip>();

        public delegate void ChangedEventHandler(object sender, EventArgs e);
        public event ChangedEventHandler Changed;

        public PanelForClassEditing(cGlobalInfo GlobalInfo)
        {
            this.GlobalInfo = GlobalInfo;

            this.Changed += new ChangedEventHandler(PanelForClassEditing_Changed);

            int NumClass = cGlobalInfo.ListWellClasses.Count;
            this.Height = cGlobalInfo.OptionsWindow.panelForWellClasses.Height;
            this.AutoScroll = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelForClassSelection_MouseDown);
            ListTextBoxes = new List<System.Windows.Forms.TextBox>();
            ListPanelColor = new List<System.Windows.Forms.Panel>();

            for (int IdxClass = 0; IdxClass < NumClass; IdxClass++)
            {
                System.Windows.Forms.Panel PanelForColor = new System.Windows.Forms.Panel();
                PanelForColor.Width = 13;
                PanelForColor.Height = 13;
                PanelForColor.BackColor = cGlobalInfo.ListWellClasses[IdxClass].ColourForDisplay;
                PanelForColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                PanelForColor.Location = new System.Drawing.Point(5, PanelForColor.Height * IdxClass-2);
                PanelForColor.Tag = IdxClass;
                PanelForColor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MouseDoubleClick);
                ListPanelColor.Add(PanelForColor);
                 
                System.Windows.Forms.TextBox CurrentTextBox = new System.Windows.Forms.TextBox();
                CurrentTextBox.Text = cGlobalInfo.ListWellClasses[IdxClass].Name;// "Class " + IdxClass;

                ListToolTips.Add(new System.Windows.Forms.ToolTip());
                
                ListToolTips[IdxClass].SetToolTip(CurrentTextBox, CurrentTextBox.Text);
                //System.Windows.Forms.ToolTip TT = new System.Windows.Forms.ToolTip();
                //TT.SetToolTip(CurrentTextBox, CurrentTextBox.Text);

                CurrentTextBox.Location = new System.Drawing.Point(PanelForColor.Width+15, (CurrentTextBox.Height+5) * IdxClass);
                CurrentTextBox.TextChanged += new EventHandler(CurrentTextBox_TextChanged);
                CurrentTextBox.Tag = IdxClass;
                ListTextBoxes.Add(CurrentTextBox);

                PanelForColor.Location = new System.Drawing.Point(5, CurrentTextBox.Location.Y+5);
               
            }
            this.Controls.AddRange(ListPanelColor.ToArray());
            this.Controls.AddRange(ListTextBoxes.ToArray());
        }


        void PanelForClassSelection_MouseDown(object sender, MouseEventArgs e)
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


        void PanelForClassEditing_Changed(object sender, EventArgs e)
        {
            // //   throw new NotImplementedException();
        }

        void CurrentTextBox_TextChanged(object sender, EventArgs e)
        {
            int Idx = (int)(((System.Windows.Forms.TextBox)sender).Tag);
            cGlobalInfo.ListWellClasses[Idx].Name = ((System.Windows.Forms.TextBox)sender).Text;


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

            cGlobalInfo.ListWellClasses[Idx].ColourForDisplay = backgroundColor;
        }


    }
}
