using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes;
using HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic;
using HCSAnalyzer.Classes.Machine_Learning;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.Viewers;

namespace HCSAnalyzer.Cell_by_Cell_and_DB.Simulator.Forms
{
    public partial class FormForModelsHistory : Form
    {
        public FormForModelsHistory()
        {
            InitializeComponent();

            //ToolTip MytoolTip = new ToolTip();

            //// Set up the delays for the ToolTip.
            //MytoolTip.AutoPopDelay = 5000;
            //MytoolTip.InitialDelay = 1000;
            //MytoolTip.ReshowDelay = 500;

            //MytoolTip.ShowAlways = true;

            //// Set up the ToolTip text for the Button and Checkbox.
            //MytoolTip.SetToolTip(this.listViewForClassifHistory, "Test");
            //toolTip1.SetToolTip(this.checkBoxShiftRowEffect, "Step: " + GlobalInfo.OptionsWindow.numericUpDownGenerateScreenRowEffectShift.Value);
            this.listViewForClassifHistory.ShowItemToolTips = true;
            this.Visible = false;

        }

        private void listViewForCellPopulations_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //Point locationOnForm = listViewForCellPopulations.FindForm().PointToClient(Control.MousePosition);
                ListViewItem IdxItem = listViewForClassifHistory.GetItemAt(e.X, e.Y);

                ContextMenuStrip contextMenuStripPicker = new ContextMenuStrip();

                //ToolStripMenuItem AddItem = new ToolStripMenuItem("Add");
                ////   AddItem.Click += new System.EventHandler(this.AddItem);
                //contextMenuStripPicker.Items.Add(AddItem);


                //ToolStripMenuItem LoadItem = new ToolStripMenuItem("Load");
                ////  LoadItem.Click += new System.EventHandler(this.LoadItem);
                //contextMenuStripPicker.Items.Add(LoadItem);

                if (IdxItem != null)
                {
                   // ToolStripMenuItem EditItem = new ToolStripMenuItem(IdxItem.Text+" -> Display Info");
                   // EditItem.Click += new System.EventHandler(this.DisplayInfoItem);
                   // contextMenuStripPicker.Items.Add(EditItem);

                    //ToolStripMenuItem SaveItem = new ToolStripMenuItem("Save " + IdxItem.Text);
                    ////   SaveItem.Click += new System.EventHandler(this.SaveItem);
                    //contextMenuStripPicker.Items.Add(SaveItem);

                    //ToolStripMenuItem DeleteItem = new ToolStripMenuItem("Delete " + IdxItem.Text);
                    ////   DeleteItem.Click += new System.EventHandler(this.DeleteItem);
                    //contextMenuStripPicker.Items.Add(DeleteItem);
                  //  contextMenuStripPicker.Items.Add(new ToolStripSeparator());
                }

                if (listViewForClassifHistory.Items.Count > 0)
                { 
                    ToolStripMenuItem DisplayGraphItem = new ToolStripMenuItem("Display Graph");
                    DisplayGraphItem.Click += new System.EventHandler(this.DisplayGraphItem);
                    contextMenuStripPicker.Items.Add(DisplayGraphItem);

                    if (listViewForClassifHistory.Items.Count > 1)
                    {
                        ToolStripMenuItem IdentifyBestItem = new ToolStripMenuItem("Identify Best");
                        IdentifyBestItem.Click += new System.EventHandler(this.IdentifyBestItem);
                        contextMenuStripPicker.Items.Add(IdentifyBestItem);
                    }
                    contextMenuStripPicker.Items.Add(new ToolStripSeparator());



                    ToolStripMenuItem ClearItem = new ToolStripMenuItem("Clear");
                    ClearItem.Click += new System.EventHandler(this.ClearItems);
                    contextMenuStripPicker.Items.Add(ClearItem);



                }
               

                //ToolStripMenuItem NewTypeItem = new ToolStripMenuItem("New Cell Type");
                //// NewTypeItem.Click += new System.EventHandler(this.NewTypeItem);
                //contextMenuStripPicker.Items.Add(NewTypeItem);

                contextMenuStripPicker.Show(Control.MousePosition);
            }
        }


        void IdentifyBestItem(object sender, EventArgs e)
        {

            double MinError = double.Parse(listViewForClassifHistory.Items[0].SubItems[2].Text);
            int IdxBest = 0;

            for (int IdxItem = 1; IdxItem < listViewForClassifHistory.Items.Count; IdxItem++)
            {
                double TmpError = double.Parse(listViewForClassifHistory.Items[IdxItem].SubItems[2].Text);
                if (TmpError < MinError)
                {
                    MinError = TmpError;
                    IdxBest = IdxItem;
                }
            }

            listViewForClassifHistory.FocusedItem = listViewForClassifHistory.Items[IdxBest];
            listViewForClassifHistory.Items[IdxBest].Selected = true;

            UpdateInfo();
        }


        void DisplayGraphItem(object sender, EventArgs e)
        {
            FormForDisplay TMPWin = new FormForDisplay();
            Panel Pan = new Panel();
            Pan.Show();

            cExtendedList ListValue = new cExtendedList();
            for (int Idx = 0; Idx < this.listViewForClassifHistory.Items.Count; Idx++)
            {
               // this.listViewForCellPopulations.Items[Idx];
                ListValue.Add(double.Parse(this.listViewForClassifHistory.Items[Idx].SubItems[2].Text));
            }

         //   ListValue.Name = this.dt.Columns[e.ColumnIndex].ColumnName;

            cPanelHisto PanelHisto = new cPanelHisto(ListValue, eGraphType.LINE, eOrientation.HORIZONTAL);
            TMPWin.Controls.Add(PanelHisto.WindowForPanelHisto.panelForGraphContainer);
            TMPWin.Show();

        }

        void DisplayInfoItem(object sender, EventArgs e)
        {
           // cListAgents SelectedCellPop = (cListAgents)listViewForCellPopulations.FocusedItem.Tag;

        }

        private void ClearItems(object sender, EventArgs e)
        {
            int NumItems = this.listViewForClassifHistory.Items.Count;
            for (int IdxItem = 0; IdxItem < NumItems; IdxItem++)
            {
                this.listViewForClassifHistory.Items[0].Remove();
            }

            this.richTextBoxCV.Clear();
            this.richTextBoxModel.Clear();
        }


        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void listViewForCellPopulations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewForClassifHistory.FocusedItem == null) return;
            UpdateInfo();
        }

        void UpdateInfo()
        {   
            cClusteringObject SelectedCellModel = (cClusteringObject)listViewForClassifHistory.FocusedItem.Tag;
            richTextBoxModel.Clear();
            richTextBoxModel.AppendText(SelectedCellModel.Model.ToString());

            richTextBoxCV.Clear();
            richTextBoxCV.AppendText(SelectedCellModel.Evaluation.toSummaryString());
            richTextBoxCV.AppendText(SelectedCellModel.Evaluation.toClassDetailsString());

        }
   


    }
}
