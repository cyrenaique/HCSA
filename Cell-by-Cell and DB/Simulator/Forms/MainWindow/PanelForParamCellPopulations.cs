using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Simulator.Classes;
using HCSAnalyzer.Classes._3D;
using HCSAnalyzer.Simulator.Forms.NewCellType;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HCSAnalyzer.Simulator.Forms.Panels
{
    public partial class PanelForParamCellPopulations : UserControl
    {
       // cWorld NewWorld;
       public  cPoint3D WorldDims;
       FormForSimuGenerator Parent;

        public PanelForParamCellPopulations(cPoint3D WorldDims, FormForSimuGenerator Parent)
        {
            InitializeComponent();
            this.WorldDims = WorldDims;
            this.Parent = Parent;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point locationOnForm = listViewForCellPopulations.FindForm().PointToClient(Control.MousePosition);
            ListViewItem IdxItem = listViewForCellPopulations.GetItemAt(Control.MousePosition.X, Control.MousePosition.Y);// locationOnForm.Y - 163;
        }

        void EditItem(object sender, EventArgs e)
        {
            cListAgents SelectedCellPop = (cListAgents)listViewForCellPopulations.FocusedItem.Tag;

            FormForInfoSingleCellPopInit_Simulator WindowForSingleCellPop =
            new FormForInfoSingleCellPopInit_Simulator( this.WorldDims,SelectedCellPop.AssociatedVariables, Parent, SelectedCellPop);

            WindowForSingleCellPop.richTextBoxDescription.Text = SelectedCellPop.Information;

            if (WindowForSingleCellPop.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<string> names = new List<string>();

                cListAgents returnPop = WindowForSingleCellPop.CellPopulation;

                listViewForCellPopulations.FocusedItem.SubItems[0].Text = returnPop.Name;
                listViewForCellPopulations.FocusedItem.SubItems[1].Text = returnPop.Count.ToString();
                listViewForCellPopulations.FocusedItem.SubItems[2].Text = returnPop[0].Type.Name;

                //names.Add(SelectedCellPop.Count.ToString());
                // names.Add(WindowForSingleCellPop.CellPopulation[0].Type.Name);
                returnPop.AssociatedVariables = WindowForSingleCellPop.ListVariables;


                //  ListViewItem NewItem = new ListViewItem(names.ToArray());

                listViewForCellPopulations.FocusedItem.Tag = returnPop;
                // NewItem.Checked = true;
                //this.listViewForCellPopulations.Items.Add(NewItem);
            }
        }

        void DeleteItem(object sender, EventArgs e)
        {
            listViewForCellPopulations.Items.Remove(listViewForCellPopulations.FocusedItem);
        }

        void NewTypeItem(object sender, EventArgs e)
        {
            Parent.CreateNewCellType();
        }

        private void LoadItem(object sender, EventArgs e)
        {
            OpenFileDialog CurrOpenFileDialog = new OpenFileDialog();

            CurrOpenFileDialog.Filter = "opt files (*.CP)|*.CP";
            CurrOpenFileDialog.Multiselect = true;
            DialogResult Res = CurrOpenFileDialog.ShowDialog();
            if (Res != DialogResult.OK) return;

            if (CurrOpenFileDialog.FileNames[0] == "") return;

            foreach (var FileName in CurrOpenFileDialog.FileNames)
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(FileName,
                                          FileMode.Open,
                                          FileAccess.Read,
                                          FileShare.Read);
                cListAgents obj = (cListAgents)formatter.Deserialize(stream);
                stream.Close();

                List<string> names = new List<string>();
                names.Add(obj.Name);
                names.Add(obj.Count.ToString());
                names.Add(obj[0].Type.Name);

                // WindowForSingleCellPop.CellPopulation.AssociatedVariables = WindowForSingleCellPop.ListVariables;
                ListViewItem NewItem = new ListViewItem(names.ToArray());

                NewItem.Tag = obj;

                NewItem.Checked = true;
                this.listViewForCellPopulations.Items.Add(NewItem);
            }
            //Parent.ListCellTypes.Add(obj);
            //MyPanelForParamCellTypes.RefreshDisplay(this);
            //this.RefreshDisplay(Parent);
        }

        private void SaveItem(object sender, EventArgs e)
        {
            SaveFileDialog CurrSavefileDialog = new SaveFileDialog();
            CurrSavefileDialog.Filter = "opt files (*.CP)|*.CP";
            CurrSavefileDialog.FileName = ((cListAgents)listViewForCellPopulations.FocusedItem.Tag).Name + ".CP";
            DialogResult Res = CurrSavefileDialog.ShowDialog();
            if (Res != DialogResult.OK) return;

            if (CurrSavefileDialog.FileName == "") return;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(CurrSavefileDialog.FileName,
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            formatter.Serialize(stream,  (cListAgents)listViewForCellPopulations.FocusedItem.Tag);
            stream.Close();
        }

        private void AddItem(object sender, EventArgs e)
        {
            List<cClassForVariable> ListVar = new List<cClassForVariable>();
            ListVar.Add(new cClassForVariable("v_CellNumber", 100));
            ListVar.Add(new cClassForVariable("v_InitPosX", 0));
            ListVar.Add(new cClassForVariable("v_InitPosY", 0));
            ListVar.Add(new cClassForVariable("v_InitPosZ", 0));
            ListVar.Add(new cClassForVariable("v_InitPosType", 0));
            ListVar.Add(new cClassForVariable("v_InitVolType", 0));
            ListVar.Add(new cClassForVariable("v_InitVol", 10));

            cListVariables AssociatedListVar = new cListVariables(ListVar);

            FormForInfoSingleCellPopInit_Simulator WindowForSingleCellPop =
                new FormForInfoSingleCellPopInit_Simulator(WorldDims, AssociatedListVar, Parent,null);

            if (WindowForSingleCellPop.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<string> names = new List<string>();
                names.Add(WindowForSingleCellPop.CellPopulation.Name);
                names.Add(WindowForSingleCellPop.CellPopulation.Count.ToString());
                names.Add(WindowForSingleCellPop.CellPopulation[0].Type.Name);
                WindowForSingleCellPop.CellPopulation.AssociatedVariables = WindowForSingleCellPop.ListVariables;
                ListViewItem NewItem = new ListViewItem(names.ToArray());
                WindowForSingleCellPop.CellPopulation.Information = WindowForSingleCellPop.richTextBoxDescription.Text;

                NewItem.Tag = WindowForSingleCellPop.CellPopulation;

                NewItem.Checked = true;
                this.listViewForCellPopulations.Items.Add(NewItem);

            }
        }

        private void ClearItem(object sender, EventArgs e)
        {
            int NumItems = this.listViewForCellPopulations.Items.Count;
            for (int IdxItem = 0; IdxItem < NumItems; IdxItem++)
            {
                this.listViewForCellPopulations.Items[0].Remove();
            }
        }

        private void listViewForCellPopulations_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //Point locationOnForm = listViewForCellPopulations.FindForm().PointToClient(Control.MousePosition);
                ListViewItem IdxItem = listViewForCellPopulations.GetItemAt(e.X, e.Y);

                ContextMenuStrip contextMenuStripPicker = new ContextMenuStrip();

                ToolStripMenuItem AddItem = new ToolStripMenuItem("Add");
                AddItem.Click += new System.EventHandler(this.AddItem);
                contextMenuStripPicker.Items.Add(AddItem);


                ToolStripMenuItem LoadItem = new ToolStripMenuItem("Load");
                LoadItem.Click += new System.EventHandler(this.LoadItem);
                contextMenuStripPicker.Items.Add(LoadItem);

                if (IdxItem != null)
                {
                    ToolStripMenuItem EditItem = new ToolStripMenuItem("Edit " + IdxItem.Text);
                    EditItem.Click += new System.EventHandler(this.EditItem);
                    contextMenuStripPicker.Items.Add(EditItem);

                    ToolStripMenuItem SaveItem = new ToolStripMenuItem("Save " + IdxItem.Text);
                    SaveItem.Click += new System.EventHandler(this.SaveItem);
                    contextMenuStripPicker.Items.Add(SaveItem);

                    ToolStripMenuItem DeleteItem = new ToolStripMenuItem("Delete " + IdxItem.Text);
                    DeleteItem.Click += new System.EventHandler(this.DeleteItem);
                    contextMenuStripPicker.Items.Add(DeleteItem);

                }

                if (listViewForCellPopulations.Items.Count > 0)
                {
                    ToolStripMenuItem ClearItem = new ToolStripMenuItem("Clear");
                    ClearItem.Click += new System.EventHandler(this.ClearItem);
                    contextMenuStripPicker.Items.Add(ClearItem);
                }
                contextMenuStripPicker.Items.Add(new ToolStripSeparator());

                ToolStripMenuItem NewTypeItem = new ToolStripMenuItem("New Cell Type");
                NewTypeItem.Click += new System.EventHandler(this.NewTypeItem);
                contextMenuStripPicker.Items.Add(NewTypeItem);

                contextMenuStripPicker.Show(Control.MousePosition);
            }
        }
    }
}
