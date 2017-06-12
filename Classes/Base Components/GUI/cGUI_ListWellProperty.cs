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
    public class cGUI_ListWellProperty : cComponentGUI
    {

        public cGUI_ListWellProperty()
        {
            this.Title = "GUI - Well Properties selection";
        }

        //cListPlates ListSelectedPlates = null;

        public List<cPropertyType> GetOutPut()
        {
            return WellPropertySelection.GetListSelectedProperties();
        }

        public bool IsCheckBoxes = true;
        public bool IsCheckOnlyActive = true;
        PanelForWellPropertySelection WellPropertySelection;

        public cFeedBackMessage Run()
        {
            FormForDisplay WindowToDisplay = new FormForDisplay();
            WindowToDisplay.Text = "Well Properties Selection";
            WindowToDisplay.FormBorderStyle = FormBorderStyle.Sizable;


            WellPropertySelection = new PanelForWellPropertySelection(this.IsCheckBoxes);
            if (this.IsCheckBoxes == false)
            {
                if (IsCheckOnlyActive)
                {

                    // get the active property
                    foreach (var item in cGlobalInfo.WindowHCSAnalyzer.toolStripDropDownButtonDisplayMode.DropDownItems)
                    {
                        if (item.GetType() == typeof(ToolStripMenuItem))
                        {
                            ToolStripMenuItem TmpMenuItem = ((ToolStripMenuItem)item);
                            if (TmpMenuItem.Checked)
                            {
                                int IdxToBeChecked = 0;
                                foreach (var ItemFromList in WellPropertySelection.ListRadioButtons)
                                {
                                    if (ItemFromList.Tag == TmpMenuItem.Tag)
                                    {
                                        WellPropertySelection.Select(IdxToBeChecked);
                                        goto ENDLOOP;
                                    }
                                    IdxToBeChecked++;
                                }
                                break;
                            }
                        }
                    }
                }
                //else
                    WellPropertySelection.Select(0);
            }


        ENDLOOP: ;
            //WellPropertySelection.GetListSelectedPlates();
            WellPropertySelection.Height = WindowToDisplay.Height - 70;
            WellPropertySelection.Width = WindowToDisplay.Width - 30;
           // PlateSelectionPanel.SelectAll();
            //ClassSelectionPanel.Select(0);
            //ClassSelectionPanel.Select(1);
            WellPropertySelection.Location = new System.Drawing.Point(5, 5);
            //PlateSelectionPanel.Width = 300;
            //PlateSelectionPanel.Height = PlateSelectionPanel.ListCheckBoxes.Count * 25;
            WellPropertySelection.BorderStyle = BorderStyle.Fixed3D;
            WellPropertySelection.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                        | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            // MyPanel.Controls.Add(ClassSelectionPanel);

            Button ReturnButton = new Button();
            ReturnButton.Text = "Ok";
            ReturnButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);

            ReturnButton.DialogResult = DialogResult.OK;
            ReturnButton.Location = new System.Drawing.Point(WellPropertySelection.Location.X, WellPropertySelection.Location.Y + WellPropertySelection.Height);
            WindowToDisplay.Controls.Add(ReturnButton);

            WindowToDisplay.Controls.Add(WellPropertySelection);
            WindowToDisplay.Width = WellPropertySelection.Width + 28;
            WindowToDisplay.Height = WellPropertySelection.Height + ReturnButton.Height + 48;


            if (WindowToDisplay.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "Selection aborded !";
                return FeedBackMessage;
            }

            //this.ListSelectedPlates = PlateSelectionPanel.GetListSelectedPlates();

            //if (this.ListSelectedPlates.Count == 0)
            //{
            //    FeedBackMessage.IsSucceed = false;
            //    FeedBackMessage.Message = "No class selected !";
            //    return FeedBackMessage;
            //}
            return FeedBackMessage;
        }
    }
}
