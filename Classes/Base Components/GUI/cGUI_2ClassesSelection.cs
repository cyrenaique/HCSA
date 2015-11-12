using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Forms;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using System.Windows.Forms;

namespace HCSAnalyzer.Classes.Base_Classes.GUI
{
    class cGUI_2ClassesSelection : cComponentGUI
    {

        public cGUI_2ClassesSelection()
        {
            this.Title = "GUI - 2 Classes selection";
        }

        public eClassType ClassType = eClassType.WELL;
        cExtendedTable ListSelectedClass = null;
        public bool PanelLeft_IsCheckBoxes = false;
        public bool PanelRight_IsCheckBoxes = false;


        public cExtendedTable GetOutPut()
        {
            return this.ListSelectedClass;
        }

        public cFeedBackMessage Run(cGlobalInfo GlobalInfo)
        {
            FormForDisplay WindowToDisplay = new FormForDisplay();
            WindowToDisplay.Text = "2 Classes Selection";
            WindowToDisplay.FormBorderStyle = FormBorderStyle.FixedSingle;

            PanelForClassSelection ClassSelectionPanel1 = new PanelForClassSelection(PanelLeft_IsCheckBoxes, this.ClassType);
            ClassSelectionPanel1.UnSelectAll();
            ClassSelectionPanel1.Select(0);
            ClassSelectionPanel1.Location = new System.Drawing.Point(10, 10);
            ClassSelectionPanel1.Width = 140;
            if(ClassSelectionPanel1.ListCheckBoxes!=null)
                ClassSelectionPanel1.Height = ClassSelectionPanel1.ListCheckBoxes.Count*25;
            else
                ClassSelectionPanel1.Height = ClassSelectionPanel1.ListRadioButtons.Count * 25;
            ClassSelectionPanel1.BorderStyle = BorderStyle.Fixed3D;


            PanelForClassSelection ClassSelectionPanel2 = new PanelForClassSelection( PanelRight_IsCheckBoxes, this.ClassType);
            ClassSelectionPanel2.UnSelectAll();
            ClassSelectionPanel2.Select(1);
            ClassSelectionPanel2.Location = new System.Drawing.Point(10 + ClassSelectionPanel1.Width, 10);
            ClassSelectionPanel2.Width = 140;
            if (ClassSelectionPanel2.ListCheckBoxes != null)
                ClassSelectionPanel2.Height = ClassSelectionPanel2.ListCheckBoxes.Count * 25;
            else
                ClassSelectionPanel2.Height = ClassSelectionPanel2.ListRadioButtons.Count * 25;
            ClassSelectionPanel2.BorderStyle = BorderStyle.Fixed3D;
           



            Button ReturnButton = new Button();
            ReturnButton.Text = "Ok";
            ReturnButton.DialogResult = DialogResult.OK;
            ReturnButton.Location = new System.Drawing.Point(ClassSelectionPanel1.Location.X, ClassSelectionPanel1.Location.Y + ClassSelectionPanel1.Height + 5);
            WindowToDisplay.Controls.Add(ReturnButton);

            WindowToDisplay.Controls.Add(ClassSelectionPanel1);
            WindowToDisplay.Controls.Add(ClassSelectionPanel2);
            WindowToDisplay.Width = ClassSelectionPanel1.Width + ClassSelectionPanel2.Width + 28;
            WindowToDisplay.Height = ClassSelectionPanel1.Height + ReturnButton.Height+ 48;


            if (WindowToDisplay.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "Selection aborded !";
                return FeedBackMessage;
            }

            List<bool> ListBool1 = ClassSelectionPanel1.GetListSelectedClass();
            List<bool> ListBool2 = ClassSelectionPanel2.GetListSelectedClass();

            int NumSelected = 0;
            this.ListSelectedClass = new cExtendedTable();

            foreach (var item in ListBool1)
            {
                this.ListSelectedClass.Add(new cExtendedList());
                if (item)
                {
                    this.ListSelectedClass[0].Add(1);
                    NumSelected++;
                }
                else
                    this.ListSelectedClass[0].Add(0);
            }
            foreach (var item in ListBool2)
            {
                this.ListSelectedClass.Add(new cExtendedList());
                if (item)
                {
                    this.ListSelectedClass[1].Add(1);
                    NumSelected++;
                }
                else
                    this.ListSelectedClass[1].Add(0);
            }


            return FeedBackMessage;
            
        }


    }
}
