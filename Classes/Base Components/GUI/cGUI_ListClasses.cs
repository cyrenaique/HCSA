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
    public enum eClassType { WELL, PHENOTYPE };


    public class cGUI_ListClasses : cComponentGUI
    {

        public bool IsCheckBoxes = true;
        public bool IsSelectAll = false;
        public eClassType ClassType = eClassType.WELL;
        cExtendedTable ListSelectedClass = null;
        //List<object> ListSelectedClasses = new List<object>();

        //public object GetListClasses()
        //{
            
        
        //}

        public cGUI_ListClasses()
        {
            this.Title = "GUI - Classes selection";
        }

        public cExtendedTable GetOutPut()
        {
            return this.ListSelectedClass;
        }

        public cFeedBackMessage Run()
        {
            FormForDisplay WindowToDisplay = new FormForDisplay();
            WindowToDisplay.Text = "Class Selection";
            WindowToDisplay.FormBorderStyle = FormBorderStyle.FixedSingle;

            PanelForClassSelection ClassSelectionPanel = new PanelForClassSelection( this.IsCheckBoxes, this.ClassType);
            
            //ClassSelectionPanel.Height = WindowToDisplay.Height;
            if (this.IsSelectAll && this.IsCheckBoxes)
            {
                ClassSelectionPanel.SelectAll();
            }
            else
            {
                ClassSelectionPanel.UnSelectAll();
                ClassSelectionPanel.Select(0);
                ClassSelectionPanel.Select(1);
            }
            ClassSelectionPanel.Location = new System.Drawing.Point(10, 10);
            ClassSelectionPanel.Width = 150;

            if(IsCheckBoxes)
                ClassSelectionPanel.Height = ClassSelectionPanel.ListCheckBoxes.Count*25;
            else
                ClassSelectionPanel.Height = ClassSelectionPanel.ListRadioButtons.Count * 25;
            ClassSelectionPanel.BorderStyle = BorderStyle.Fixed3D;
           // MyPanel.Controls.Add(ClassSelectionPanel);



            Button ReturnButton = new Button();
            ReturnButton.Text = "Ok";
            ReturnButton.DialogResult = DialogResult.OK;
            ReturnButton.Location = new System.Drawing.Point(ClassSelectionPanel.Location.X, ClassSelectionPanel.Location.Y + ClassSelectionPanel.Height + 5);
            WindowToDisplay.Controls.Add(ReturnButton);

            WindowToDisplay.Controls.Add(ClassSelectionPanel);
            WindowToDisplay.Width = ClassSelectionPanel.Width + 28;
            WindowToDisplay.Height = ClassSelectionPanel.Height + ReturnButton.Height+ 48;


            if (WindowToDisplay.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "Selection aborded !";
                return FeedBackMessage;
            }

            List<bool> ListBool = ClassSelectionPanel.GetListSelectedClass();

            int NumSelected = 0;

            this.ListSelectedClass = new cExtendedTable();
            this.ListSelectedClass.Name = "List Class Selected";
            this.ListSelectedClass.Add(new cExtendedList());

            this.ListSelectedClass[0].ListTags = new List<object>();

            int Idx = 0;
            foreach (var item in ListBool)
            {
                if (item)
                {
                    this.ListSelectedClass[0].Add(1);
                    if(this.ClassType== eClassType.WELL)
                        this.ListSelectedClass[0].ListTags.Add(cGlobalInfo.ListWellClasses[Idx]);
                    else if(this.ClassType == eClassType.PHENOTYPE)
                        this.ListSelectedClass[0].ListTags.Add(cGlobalInfo.ListCellularPhenotypes[Idx]);

                    NumSelected++;
                }
                else
                {
                    this.ListSelectedClass[0].Add(0);
                    this.ListSelectedClass[0].ListTags.Add(null);

                }
                Idx++;
            }

            if (NumSelected==0)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No class selected !";
                return FeedBackMessage;
            }

            //if (NumSelected < MinNumberOfClass)
            //{
            //    FeedBackMessage.IsSucceed = false;
            //    FeedBackMessage.Message = "At least "+MinNumberOfClass+" class have to be selected !";
            //    return FeedBackMessage;
            //}


            return FeedBackMessage;
            
        }


    }
}
