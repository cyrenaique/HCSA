using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibPlateAnalysis;
using HCSAnalyzer.Classes.General_Types.Screen.Plate.Well.Properties;

namespace HCSAnalyzer.Classes.General_Types
{
    public partial class FormForPropertiesManager : Form
    {
        public FormForPropertiesManager()
        {
            InitializeComponent();

            foreach (var item in cGlobalInfo.CurrentScreening.ListWellPropertyTypes)
                this.dataGridViewProperties.Rows.Add();
           
            int Idx = 0;

            DataGridViewComboBoxColumn DCC = (DataGridViewComboBoxColumn)this.dataGridViewProperties.Columns[1];
            DCC.Items.Add(eDataType.DOUBLE.ToString());
            DCC.Items.Add(eDataType.INTEGER.ToString());
            DCC.Items.Add(eDataType.STRING.ToString());
            DCC.Items.Add(eDataType.BOOL.ToString());
            //PropertyTypeCombo.Items.Add(eDataType.TIME);

            foreach (var item in cGlobalInfo.CurrentScreening.ListWellPropertyTypes)
            {
                this.dataGridViewProperties[0, Idx].Value = item.Name;
                this.dataGridViewProperties[0, Idx].ToolTipText = item.GetInfo();

                this.dataGridViewProperties[1, Idx].Value = item.Type.ToString();
                this.dataGridViewProperties[1, Idx].ToolTipText = item.GetInfo();

                this.dataGridViewProperties.Rows[Idx].Tag = item;

                if (item.IsLocked)
                {
                    this.dataGridViewProperties.Rows[Idx].DefaultCellStyle.ForeColor = Color.OrangeRed;                    
                    this.dataGridViewProperties.Rows[Idx].ReadOnly = true;
                }

                Idx++;
            }

        }


        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewProperties_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewProperties_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenuStrip MaincontextMenu = new ContextMenuStrip();

                ToolStripMenuItem AddItem = new ToolStripMenuItem("Add Property");
                AddItem.Click += new EventHandler(AddItem_Click);
                MaincontextMenu.Items.Add(AddItem);

                DataGridView.HitTestInfo info = dataGridViewProperties.HitTest(e.X, e.Y);


                if ((info.RowIndex!=-1) && (dataGridViewProperties.Rows[info.RowIndex].Tag != null))
                {
                    if (dataGridViewProperties.Rows[info.RowIndex].Tag.GetType() == typeof(cPropertyType))
                    {
                        cPropertyType CP = (cPropertyType)dataGridViewProperties.Rows[info.RowIndex].Tag;
                        if ((CP.Type == eDataType.BOOL) || (CP.Type == eDataType.DOUBLE) || (CP.Type == eDataType.INTEGER))
                        {
                            ToolStripMenuItem ConvertItem = new ToolStripMenuItem("Convert to Descriptor");
                            ConvertItem.Tag = CP;
                            ConvertItem.Click += new EventHandler(ConvertItem_Click);
                            MaincontextMenu.Items.Add(ConvertItem);
                        }
                    }



                    cPropertyType Pt = ((cPropertyType)dataGridViewProperties.Rows[info.RowIndex].Tag);
                    if (!Pt.IsLocked)
                    {
                        ToolStripMenuItem RemoveItem = new ToolStripMenuItem("Remove ["+Pt.Name+"]");
                        RemoveItem.Click += new EventHandler(RemoveItem_Click);
                        MaincontextMenu.Items.Add(RemoveItem);

                    
                    }
                }

                MaincontextMenu.Show(Control.MousePosition);
            }
        }

        void RemoveItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void ConvertItem_Click(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(ToolStripMenuItem)) return;

            ToolStripMenuItem TMI = (ToolStripMenuItem)(sender);

            cPropertyType CP = null;
            if ((TMI.Tag != null) && (TMI.Tag.GetType() == typeof(cPropertyType)))
                CP = (cPropertyType)(TMI.Tag);
            else
                return;

            cDescriptorType ClassType = new cDescriptorType(CP.Name, true, 1);
            cGlobalInfo.CurrentScreening.ListDescriptors.AddNew(ClassType);

            foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesAvailable)
            {
                foreach (cWell Tmpwell in TmpPlate.ListWells)
                {
                    cListSignature LDesc = new cListSignature();

                    double TmpValue = 0;
                    object TmpObj = Tmpwell.ListProperties.FindByName(CP.Name).GetValue();

                    if ((TmpObj==null)||(!double.TryParse(TmpObj.ToString(), out TmpValue)))
                        TmpValue = 0;

                    cSignature NewDesc = new cSignature(TmpValue, ClassType, cGlobalInfo.CurrentScreening);
                    LDesc.Add(NewDesc);

                    Tmpwell.AddSignatures(LDesc);
                }
            }

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();
        }


        void AddItem_Click(object sender, EventArgs e)
        {
            FormForNewProperty WindowNewProp = new FormForNewProperty();
            if (WindowNewProp.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            cPropertyType NewPT = new cPropertyType(WindowNewProp.textBoxName.Text,((eDataType)WindowNewProp.comboBoxType.SelectedItem));
            cGlobalInfo.CurrentScreening.ListWellPropertyTypes.AddNewType(NewPT);

            this.dataGridViewProperties.Rows.Add();

            this.dataGridViewProperties[0, this.dataGridViewProperties.Rows.Count - 1].Value = NewPT.Name;
            this.dataGridViewProperties[0, this.dataGridViewProperties.Rows.Count - 1].ToolTipText = NewPT.GetInfo();

            this.dataGridViewProperties[1, this.dataGridViewProperties.Rows.Count - 1].Value = NewPT.Type.ToString();
            this.dataGridViewProperties[1, this.dataGridViewProperties.Rows.Count - 1].ToolTipText = NewPT.GetInfo();


        }
    }
}
