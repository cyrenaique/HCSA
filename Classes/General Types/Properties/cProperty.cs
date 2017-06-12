using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HCSAnalyzer.Classes.General_Types
{
    public class cProperty
    {
        public cPropertyType PropertyType { get; private set; }
        object Value;

        public string Info = "";
        public bool IsGUIforValue = false;

        NumericUpDown CtrlForInt;

        public object GetValue()
        {   
            if (this.Value == null) return null;
            
            if (PropertyType.IsConstant)
            {
                if (this.PropertyType.Type == eDataType.DOUBLE)
                {
                    double CurrentValue = (double)this.Value;
                    if (CurrentValue < PropertyType.Min) CurrentValue = PropertyType.Min;
                    if (CurrentValue > PropertyType.Max) CurrentValue = PropertyType.Max;

                    return CurrentValue;
                }
                else if (this.PropertyType.Type == eDataType.INTEGER)
                {
                    int CurrentValue = (int)this.Value;
                    if (CurrentValue < PropertyType.Min) CurrentValue = (int)PropertyType.Min;
                    if (CurrentValue > PropertyType.Max) CurrentValue = (int)PropertyType.Max;

                    return CurrentValue;
                }
                else if (this.PropertyType.Type == eDataType.BOOL)
                {
                    bool CurrentValue = (bool)this.Value;
                    return CurrentValue;
                }
                else
                {

                    return Value;
                }
            }
            else
            {
                // not yet implemented
                return null;
            }
        }

        public cProperty(cPropertyType PropertyType, object Value)
        {
            this.PropertyType = PropertyType;
            this.Value = Value;
        }

        public void SetNewValue(object NewValue)
        {
            if (NewValue == null)
            {
                this.Value = null;
                return;
            }

            if (this.PropertyType.Type == eDataType.INTEGER)
            {
                int MyNewValue = (int)NewValue;
                if ((MyNewValue > this.PropertyType.Max) || (MyNewValue < this.PropertyType.Min))
                    this.Value = null;
                else
                    this.Value = MyNewValue;

            }
            else if (this.PropertyType.Type == eDataType.DOUBLE)
            {
                double MyNewValue = (double)NewValue;
                if ((MyNewValue > this.PropertyType.Max) || (MyNewValue < this.PropertyType.Min))
                    this.Value = null;
                else
                    this.Value = MyNewValue;

            }
            else
                this.Value = NewValue;
        }

        public Control GetAssociatedGUI()
        {
            Panel ToBeReturned = new Panel();

            ToBeReturned.Tag = this;
            Label TextName = new Label();
            TextName.Text = this.PropertyType.Name;

            ToolTip ToolTipForInfo = new ToolTip();
            ToolTipForInfo.SetToolTip(TextName, PropertyType.GetInfo());


            ToBeReturned.Controls.Add(TextName);

            object TmpObj = this.GetValue();

            if (this.PropertyType.Type == eDataType.DOUBLE)
            {
                NumericUpDown CtrlForDouble = new NumericUpDown();

                double DefaultValue = 0;
                if ((TmpObj != null) && (TmpObj.GetType() == typeof(double)))
                    DefaultValue = (double)TmpObj;
             

                CtrlForDouble.DecimalPlaces = 4;
                CtrlForDouble.Minimum = (decimal)this.PropertyType.Min;
                CtrlForDouble.Maximum = (decimal)this.PropertyType.Max;
                
                CtrlForDouble.Value = (decimal)DefaultValue;

                CtrlForDouble.Location = new System.Drawing.Point(TextName.Width + 10, TextName.Location.Y);
                ToBeReturned.Width = CtrlForDouble.Location.X + CtrlForDouble.Width + 40;
                ToBeReturned.Height = CtrlForDouble.Height;
                ToBeReturned.Controls.Add(CtrlForDouble);
            }
            else if (this.PropertyType.Type == eDataType.INTEGER)
            {
                CtrlForInt = new NumericUpDown();

                CtrlForInt.ValueChanged += new EventHandler(CtrlForInt_ValueChanged);

                int DefaultValue = 0;
                if ((TmpObj != null) && (TmpObj.GetType() == typeof(int)))
                    DefaultValue = (int)TmpObj;

                CtrlForInt.DecimalPlaces = 0;
                CtrlForInt.Minimum = (decimal)this.PropertyType.Min;
                CtrlForInt.Maximum = (decimal)this.PropertyType.Max;
                CtrlForInt.Value = (decimal)DefaultValue;
                
                if((PropertyType.IntType == eIntegerType.EVEN)||(PropertyType.IntType == eIntegerType.ODD))
                {
                    CtrlForInt.Increment=2;
                }

                CtrlForInt.Location = new System.Drawing.Point(TextName.Width + 10, TextName.Location.Y);
                ToBeReturned.Width = CtrlForInt.Location.X + CtrlForInt.Width + 40;
                ToBeReturned.Height = CtrlForInt.Height;
                ToBeReturned.Controls.Add(CtrlForInt);
            }
            else if (this.PropertyType.Type == eDataType.BOOL)
            {
                CheckBox CtrlForBool = new CheckBox();

                bool DefaultValue = true;
                if ((TmpObj != null) && (TmpObj.GetType() == typeof(bool)))
                    DefaultValue = (bool)TmpObj;

                CtrlForBool.Checked = DefaultValue;

                CtrlForBool.Location = new System.Drawing.Point(TextName.Width + 2, TextName.Location.Y-4);
                ToBeReturned.Width = CtrlForBool.Location.X + CtrlForBool.Width + 40;
                ToBeReturned.Height = CtrlForBool.Height;
                ToBeReturned.Controls.Add(CtrlForBool);
            }
            else if (this.PropertyType.Type == eDataType.STRING)
            {
                if (this.PropertyType.ListPotentialString.Count > 1)
                {
                    ComboBox ComboForString = new ComboBox();

                    foreach (var item in this.PropertyType.ListPotentialString)
                    {
                       ComboForString.Items.Add(item); 
                    }

                    string DefaultValue = "";
                    if ((TmpObj != null) && (TmpObj.GetType() == typeof(string)))
                        DefaultValue = (string)TmpObj;

                    ComboForString.Text = DefaultValue;

                    ComboForString.Location = new System.Drawing.Point(TextName.Width + 2, TextName.Location.Y - 4);
                    ToBeReturned.Width = ComboForString.Location.X + ComboForString.Width + 40;
                    ToBeReturned.Height = ComboForString.Height;
                    ToBeReturned.Controls.Add(ComboForString);

                }
                else
                {
                    TextBox CtrlForString = new TextBox();

                    string DefaultValue = "";
                    if ((TmpObj != null) && (TmpObj.GetType() == typeof(string)))
                        DefaultValue = (string)TmpObj;

                    CtrlForString.Text = DefaultValue;

                    CtrlForString.Location = new System.Drawing.Point(TextName.Width + 2, TextName.Location.Y - 4);
                    ToBeReturned.Width = CtrlForString.Location.X + CtrlForString.Width + 40;
                    ToBeReturned.Height = CtrlForString.Height;
                    ToBeReturned.Controls.Add(CtrlForString);
                }
            }

            return ToBeReturned;
        }

        void CtrlForInt_ValueChanged(object sender, EventArgs e)
        {
            if (PropertyType.IntType == eIntegerType.BOTH) return;

            else if ((PropertyType.IntType == eIntegerType.EVEN)&&((((int)CtrlForInt.Value)%2)==1))
                CtrlForInt.Value = (int)CtrlForInt.Value + 1;
            else if ((PropertyType.IntType == eIntegerType.ODD) && ((((int)CtrlForInt.Value) % 2) == 0))
                CtrlForInt.Value = (int)CtrlForInt.Value + 1;
        }

    }
}
