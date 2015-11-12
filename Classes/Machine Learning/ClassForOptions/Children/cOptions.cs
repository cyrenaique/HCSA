using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Forms.FormsForOptions.PanelForOptions;
using System.Windows.Forms;
using HCSAnalyzer.Classes;

namespace HCSAnalyzer.Forms.FormsForOptions.ClassForOptions.Children
{
    #region Parent Class
    [Serializable]
    public abstract class cOptionGeneral
    {
        public List<cDoubleValue> ListDoubleValues = new List<cDoubleValue>();
        public List<cCheckValue> ListCheckValues = new List<cCheckValue>();
        public List<cColorValue> ListColorValues = new List<cColorValue>();
        public List<cTextValue> ListTextValues = new List<cTextValue>();

        protected Panel PanelToDisplay;
        public string Name;

        public Panel GetPanel()
        {
            PanelToDisplay.Location = new System.Drawing.Point(0, 0);
            return PanelToDisplay;
        }

        public cOptionGeneral(string Name)
        {
            this.Name = Name;
        }

        protected void InitValues()
        {
            foreach (Control ctl in PanelToDisplay.Controls)
            {
                Type CtlType = ctl.GetType();

                if (CtlType == typeof(CheckBox))
                {
                    cCheckValue NewValue = new cCheckValue(((CheckBox)ctl).Checked, ctl.Name);
                    this.ListCheckValues.Add(NewValue);
                    ctl.Tag = NewValue;
                }
                else if (CtlType == typeof(RadioButton))
                {
                    cCheckValue NewValue = new cCheckValue(((RadioButton)ctl).Checked, ctl.Name);
                    this.ListCheckValues.Add(NewValue);
                    ctl.Tag = NewValue;
                }
                else if (CtlType == typeof(NumericUpDown))
                {
                    cDoubleValue NewValue = new cDoubleValue((double)((NumericUpDown)ctl).Value, ctl.Name);
                    this.ListDoubleValues.Add(NewValue);
                    ctl.Tag = NewValue;
                }
                else if (CtlType == typeof(TextBox))
                {
                    cTextValue NewValue = new cTextValue(((TextBox)ctl).Text, ctl.Name);
                    this.ListTextValues.Add(NewValue);
                    ctl.Tag = NewValue;
                }
                else if (CtlType == typeof(Panel))
                {
                    cColorValue NewValue = new cColorValue(((Panel)ctl).BackColor, ctl.Name);
                    this.ListColorValues.Add(NewValue);
                    ctl.Tag = NewValue;
                }
            }
        }
    }
    #endregion

    #region child classes
    [Serializable]
    public class cOption3D : cOptionGeneral
    {
        public cOption3D(string Name)
            : base(Name)
        {
            PanelForOptions3D PanelForOption = new PanelForOptions3D();
            this.PanelToDisplay = PanelForOption.panel;
        }
    }

    [Serializable]
    public class cOptionDisplayPlatesandWells : cOptionGeneral
    {
        public cOptionDisplayPlatesandWells (string Name)
            : base(Name)
        {
            PanelForPlatesandWells PanelForOption = new PanelForPlatesandWells();
            this.PanelToDisplay = PanelForOption.panel;
        }
    }

    [Serializable]
    public class cOptionWellClassesColor : cOptionGeneral
    {
        public cOptionWellClassesColor(string Name, cGlobalInfo GlobalInfo)
            : base(Name)
        {
            PanelForWellClassesColor PanelForOption = new PanelForWellClassesColor(GlobalInfo);
            this.PanelToDisplay = PanelForOption.panel;
        }
    }

    [Serializable]
    public class cOptionCellularPhenotypesColor : cOptionGeneral
    {
        public cOptionCellularPhenotypesColor(string Name, cGlobalInfo GlobalInfo)
            : base(Name)
        {
            PanelForCellularPhenotypesColor PanelForOption = new PanelForCellularPhenotypesColor(GlobalInfo);
            this.PanelToDisplay = PanelForOption.panel;
        }
    }


    #endregion
}
