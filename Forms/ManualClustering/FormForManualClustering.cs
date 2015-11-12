using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using HCSAnalyzer.Classes.Base_Classes.GUI;
using HCSAnalyzer.Classes.General_Types;

namespace HCSAnalyzer.Forms
{

    public partial class FormForManualClustering : Form
    {
        cGlobalInfo GlobalInfo;
        int TotalHeightDesc = 0;
        int TotalHeightProp = 0;
        //int TotalHeight = 0;
        public int PanelHeight = 0;

        PanelForClassSelection WellClassSelectionPanel;
        PanelForPlatesSelection PlatesSelectionPanel;
        PanelForClassSelection HitClassPanel;
        PanelForClassSelection NonHitClassPanel;

        List<FormForPanelFilterHits> ListFormForPanelFilterDescHits = new List<FormForPanelFilterHits>();
        List<FormForPanelFilterHits> ListFormForPanelFilterPropertyHits = new List<FormForPanelFilterHits>();

        public FormForManualClustering(cGlobalInfo GlobalInfo)
        {
            InitializeComponent();

            this.GlobalInfo = GlobalInfo;
            WellClassSelectionPanel = new PanelForClassSelection(true, eClassType.WELL);
            WellClassSelectionPanel.SelectAll();
            WellClassSelectionPanel.Height = tabPageWellClasses.Height;
            tabPageWellClasses.Controls.Add(WellClassSelectionPanel);

            HitClassPanel = new PanelForClassSelection(false, eClassType.WELL);
            HitClassPanel.Height = panelForHitClass.Height;
            HitClassPanel.UnSelectAll();
            HitClassPanel.Select(1);
            panelForHitClass.Controls.Add(HitClassPanel);

            NonHitClassPanel = new PanelForClassSelection( false, eClassType.WELL);
            NonHitClassPanel.Height = panelForNonHitClass.Height;
            NonHitClassPanel.UnSelectAll();
            NonHitClassPanel.Select(0);
            panelForNonHitClass.Controls.Add(NonHitClassPanel);

            PlatesSelectionPanel = new PanelForPlatesSelection(true, null, true);
            PlatesSelectionPanel.Height = tabPageWellClasses.Height;
            PlatesSelectionPanel.Width = tabPageWellClasses.Width;
            TabPagePlates.Controls.Add(PlatesSelectionPanel);
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabPageFilterDesc)
            {
                FormForPanelFilterHits TmpFormForPanel = new FormForPanelFilterHits(this.GlobalInfo);
                TmpFormForPanel.panelFilterDesc.Location = new Point(5, TotalHeightDesc);
                PanelHeight = TmpFormForPanel.panelFilterDesc.Height;
                TotalHeightDesc += TmpFormForPanel.panelFilterDesc.Height + 5;

                TmpFormForPanel.comboBoxForDescName.Text = TmpFormForPanel.comboBoxForDescName.Items[0].ToString();
                ListFormForPanelFilterDescHits.Add(TmpFormForPanel);
                this.panelMainFilterDesc.Controls.Add(TmpFormForPanel.panelFilterDesc);
                buttonRemove.Enabled = true;
            }
            else if (tabControl.SelectedTab == tabPageFitlersProperties)
            {
                FormForPanelFilterHits TmpFormForPanel = new FormForPanelFilterHits(this.GlobalInfo);
                TmpFormForPanel.panelFilterProperties.Location = new Point(5, TotalHeightProp);
                PanelHeight = TmpFormForPanel.panelFilterProperties.Height;
                TotalHeightProp += TmpFormForPanel.panelFilterProperties.Height + 5;

                TmpFormForPanel.comboBoxForPropertyName.Text = TmpFormForPanel.comboBoxForPropertyName.Items[0].ToString();
                ListFormForPanelFilterPropertyHits.Add(TmpFormForPanel);
                this.panelMainFilterProp.Controls.Add(TmpFormForPanel.panelFilterProperties);
                buttonRemove.Enabled = true;
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabPageFilterDesc)
            {

                if (this.panelMainFilterDesc.Controls.Count > 0)
                {
                    this.panelMainFilterDesc.Controls.RemoveAt(this.panelMainFilterDesc.Controls.Count - 1);
                    TotalHeightDesc -= PanelHeight + 5;

                    ListFormForPanelFilterDescHits.RemoveAt(ListFormForPanelFilterDescHits.Count - 1);
                }

                if (this.panelMainFilterDesc.Controls.Count == 0) buttonRemove.Enabled = false;
            }
            else if (tabControl.SelectedTab == tabPageFitlersProperties)
            {
                if (this.panelMainFilterProp.Controls.Count > 0)
                {
                    this.panelMainFilterProp.Controls.RemoveAt(this.panelMainFilterProp.Controls.Count - 1);
                    TotalHeightProp -= PanelHeight + 5;

                    ListFormForPanelFilterPropertyHits.RemoveAt(ListFormForPanelFilterPropertyHits.Count - 1);
                }

                if (this.panelMainFilterProp.Controls.Count == 0) buttonRemove.Enabled = false;

            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if ((this.panelMainFilterDesc.Controls.Count == 0) && (this.panelMainFilterProp.Controls.Count == 0)) return;

            int HitClass = HitClassPanel.GetListIndexSelectedClass()[0];
            int NonHitClass = NonHitClassPanel.GetListIndexSelectedClass()[0];

            // parse the plates
            foreach (var TmpPlate in PlatesSelectionPanel.GetListSelectedPlates())
            {
                // check if the class has to be processed
                cListWells TmpListWell = TmpPlate.ListWells.Filter(WellClassSelectionPanel.GetListSelectedClassTypes());

                foreach (var TmpWell in TmpListWell)
                {
                    bool IsHit = false;

                    #region filter the descriptors
                    // get the value associated to the selected descriptor
                    int IDxPanel = 0;
                    foreach (var TmpFilter in this.panelMainFilterDesc.Controls)
                    {
                        double Value = TmpWell.GetAverageValue(cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorByName(this.ListFormForPanelFilterDescHits[IDxPanel].comboBoxForDescName.Text));
                        double ValueTobeComparedTo = 0;
                        if (this.ListFormForPanelFilterDescHits[IDxPanel].radioButtonManual.Checked)
                            ValueTobeComparedTo = (double)this.ListFormForPanelFilterDescHits[IDxPanel].numericUpDownManualValue.Value;
                        else
                            ValueTobeComparedTo = (double)this.ListFormForPanelFilterDescHits[IDxPanel].numericUpDownZScoreValue.Value;

                        if (this.ListFormForPanelFilterDescHits[IDxPanel].comboBoxComparison.Text == ">")
                        {
                            if (Value > ValueTobeComparedTo) IsHit = true;
                            else
                            {
                                IsHit = false;
                                TmpWell.SetClass(NonHitClass);
                                break;
                            }
                        }
                        else if (this.ListFormForPanelFilterDescHits[IDxPanel].comboBoxComparison.Text == "<")
                        {
                            if (Value > ValueTobeComparedTo) IsHit = true;
                            else
                            {
                                IsHit = false;
                                TmpWell.SetClass(NonHitClass);
                                break;
                            }

                        }
                        else if (this.ListFormForPanelFilterDescHits[IDxPanel].comboBoxComparison.Text == ">=")
                        {
                            if (Value >= ValueTobeComparedTo) IsHit = true;
                            else
                            {
                                IsHit = false;
                                TmpWell.SetClass(NonHitClass);
                                break;
                            }

                        }
                        else if (this.ListFormForPanelFilterDescHits[IDxPanel].comboBoxComparison.Text == "<=")
                        {
                            if (Value <= ValueTobeComparedTo) IsHit = true;
                            else
                            {
                                IsHit = false;
                                TmpWell.SetClass(NonHitClass);
                                break;
                            }

                        }
                        else if (this.ListFormForPanelFilterDescHits[IDxPanel].comboBoxComparison.Text == "=")
                        {
                            if (Value == ValueTobeComparedTo) IsHit = true;
                            else
                            {
                                IsHit = false;
                                TmpWell.SetClass(NonHitClass);
                                break;
                            }


                        }
                        else if (this.ListFormForPanelFilterDescHits[IDxPanel].comboBoxComparison.Text == "!=")
                        {
                            if (Value != ValueTobeComparedTo) IsHit = true;
                            else
                            {
                                IsHit = false;
                                break;
                            }

                        }

                        IDxPanel++;
                    }
                    #endregion

                 //   if (!IsHit) continue;

                    #region Filter the properties
                    // get the value associated to the selected descriptor
                    IDxPanel = 0;// this.ListFormForPanelFilterPropertyHits.Count - 1;

                    foreach (var TmpFilter in this.panelMainFilterProp.Controls)
                    {
                        cProperty ObjValue = TmpWell.ListProperties.FindPropertyByName(this.ListFormForPanelFilterPropertyHits[IDxPanel].comboBoxForPropertyName.Text);

                        if (ObjValue == null) continue;

                        eComparison Comparison = eComparison.E;
                        switch (this.ListFormForPanelFilterPropertyHits[IDxPanel].comboBoxPropertyComparison.Text)
                        {
                            case ">":
                                Comparison = eComparison.HT; break;
                            case "<":
                                Comparison = eComparison.LT; break;
                            case ">=":
                                Comparison = eComparison.HET; break;
                            case "<=":
                                Comparison = eComparison.LET; break;
                            case "=":
                                Comparison = eComparison.E; break;
                            case "!=":
                                Comparison = eComparison.NE; break;
                            case "C":
                                Comparison = eComparison.I; break;
                            case "!C":
                                Comparison = eComparison.NI; break;
                            default:
                                continue;
                                break;
                        }

                        //object 
                        cProperty TmpProp = null;

                        if (ObjValue.PropertyType.Type == eDataType.DOUBLE)
                        {
                            TmpProp = new cProperty(new cPropertyType("", eDataType.DOUBLE), (double)this.ListFormForPanelFilterPropertyHits[IDxPanel].numericUpDownValuePropToBeCompared.Value);
                        }
                        else if (ObjValue.PropertyType.Type == eDataType.INTEGER)
                        {
                            TmpProp = new cProperty(new cPropertyType("", eDataType.INTEGER), (int)this.ListFormForPanelFilterPropertyHits[IDxPanel].numericUpDownValuePropToBeCompared.Value);
                        }
                        else if (ObjValue.PropertyType.Type == eDataType.BOOL)
                        {
                            if ((int)this.ListFormForPanelFilterPropertyHits[IDxPanel].numericUpDownValuePropToBeCompared.Value == 1)
                                TmpProp = new cProperty(new cPropertyType("", eDataType.BOOL), true);
                            else
                                TmpProp = new cProperty(new cPropertyType("", eDataType.BOOL), false);
                        }
                        else if (ObjValue.PropertyType.Type == eDataType.STRING)
                        {
                            TmpProp = new cProperty(new cPropertyType("", eDataType.STRING), (string)this.ListFormForPanelFilterPropertyHits[IDxPanel].TextBoxValuePropToBeCompared.Text);
                        }
                        else
                        {
                            continue;
                        }

                        bool ResComparison = cListProperty.Compare(ObjValue, TmpProp, Comparison);

                        if (ResComparison)
                        {
                            IsHit = true;
                        }
                        else
                        {
                            IsHit = false;
                            TmpWell.SetClass(NonHitClass);
                            break;
                        }

                        IDxPanel++;
                    }

                    if (this.checkBoxTransferToHitList.Checked)
                    {
                        if (IsHit) TmpWell.AddToListWellsGUI();

                    }
                    else
                    {
                        if (IsHit) TmpWell.SetClass(HitClass);
                        else
                        {
                            if(this.checkBoxIsRejectedClass.Checked)
                                TmpWell.SetClass(NonHitClass);
                        }
                    }
                    #endregion
                }
            }




        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabPageFilterDesc)
            {
                buttonAdd.Enabled = true;

                if (ListFormForPanelFilterDescHits.Count > 0)
                    buttonRemove.Enabled = true;
                else
                    buttonRemove.Enabled = false;
            }
            else if (tabControl.SelectedTab == tabPageFitlersProperties)
            {
                buttonAdd.Enabled = true;

                if (ListFormForPanelFilterPropertyHits.Count > 0)
                    buttonRemove.Enabled = true;
                else
                    buttonRemove.Enabled = false;


            }
            else
            {
                buttonRemove.Enabled = false;
                buttonAdd.Enabled = false;
            }
        }

        private void checkBoxIsRejectedClass_CheckedChanged(object sender, EventArgs e)
        {
            panelForNonHitClass.Enabled = checkBoxIsRejectedClass.Checked;
        }
    }
}
