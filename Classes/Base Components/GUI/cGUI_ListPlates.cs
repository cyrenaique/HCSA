using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using System.Windows.Forms;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using HCSAnalyzer.Classes.General_Types;

namespace HCSAnalyzer.Classes.Base_Classes.GUI
{
    public class cGUI_ListPlates : cComponentGUI
    {

        public cGUI_ListPlates()
        {
            this.Title = "GUI - Plates selection";
        }

        cListPlates ListSelectedPlates = null;
        public cListPlates ListInitialPlates = null;

        public cListPlates GetOutPut()
        {
            return this.ListSelectedPlates;
        }

        public bool IsCheckBoxes = true;
        public bool IsCheckOnlyActive = true;

        public cFeedBackMessage Run()
        {
            FormForDisplay WindowToDisplay = new FormForDisplay();
            WindowToDisplay.Text = "Plates Selection";
            WindowToDisplay.FormBorderStyle = FormBorderStyle.Sizable;


            PanelForPlatesSelection PlateSelectionPanel = new PanelForPlatesSelection(this.IsCheckBoxes, ListInitialPlates, IsCheckOnlyActive);
            PlateSelectionPanel.Height = WindowToDisplay.Height - 70;
            PlateSelectionPanel.Width = WindowToDisplay.Width - 30;
           // PlateSelectionPanel.SelectAll();
            //ClassSelectionPanel.Select(0);
            //ClassSelectionPanel.Select(1);
            PlateSelectionPanel.Location = new System.Drawing.Point(5, 5);
            //PlateSelectionPanel.Width = 300;
            //PlateSelectionPanel.Height = PlateSelectionPanel.ListCheckBoxes.Count * 25;
            PlateSelectionPanel.BorderStyle = BorderStyle.Fixed3D;
            PlateSelectionPanel.Anchor =(System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                        | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            // MyPanel.Controls.Add(ClassSelectionPanel);

            Button ReturnButton = new Button();
            ReturnButton.Text = "Ok";
            ReturnButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);

            ReturnButton.DialogResult = DialogResult.OK;
            ReturnButton.Location = new System.Drawing.Point(PlateSelectionPanel.Location.X, PlateSelectionPanel.Location.Y + PlateSelectionPanel.Height );
            WindowToDisplay.Controls.Add(ReturnButton);

            WindowToDisplay.Controls.Add(PlateSelectionPanel);
            WindowToDisplay.Width = PlateSelectionPanel.Width + 28;
            WindowToDisplay.Height = PlateSelectionPanel.Height + ReturnButton.Height + 48;


            if (WindowToDisplay.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "Selection aborded !";
                return FeedBackMessage;
            }

            this.ListSelectedPlates = PlateSelectionPanel.GetListSelectedPlates();

            if (this.ListSelectedPlates.Count == 0)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No class selected !";
                return FeedBackMessage;
            }
            return FeedBackMessage;
        }
    }
}
