using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Simulator.Classes;
using HCSAnalyzer.Simulator.Forms.NewCellType;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace HCSAnalyzer.Simulator.Forms.Panels
{
    public partial class PanelForParamCellTypes : UserControl
    {

        public PanelForParamCellTypes()
        {
            InitializeComponent();
        }

        FormForSimuGenerator Parent;

        public void RefreshDisplay(FormForSimuGenerator Parent)
        {
            this.Parent = Parent;
            comboBoxCellTypes.Items.Clear();
            foreach (cCellType item in Parent.ListCellTypes)
            {
                comboBoxCellTypes.Items.Add(item.Name);
            }

            this.comboBoxCellTypes.SelectedItem = this.comboBoxCellTypes.Items[0];
        }

        private void comboBoxCellTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cCellType CurrentType = Parent.ListCellTypes.FindType(comboBoxCellTypes.Text);
            if (CurrentType == null) return;
            this.panelForColor.BackColor = CurrentType.TypeColor;
            this.richTextBox.Clear();
          //  this.richTextBox.AppendText(CurrentType.Cycle.ListProba.ToString());
        }

        private void panelForColor_MouseClick(object sender, MouseEventArgs e)
        {
            ColorDialog ColorPicker = new ColorDialog();
            if (ColorPicker.ShowDialog() != DialogResult.OK) return;
            panelForColor.BackColor = ColorPicker.Color;
            panelForColor.Refresh();
            cCellType CurrentType = Parent.ListCellTypes.FindType(comboBoxCellTypes.Text);
            CurrentType.TypeColor = panelForColor.BackColor;
        }

        private void addNewCellTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
             Parent.CreateNewCellType();
        }

        private void editCurrentTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Parent.EditCellType(Parent.ListCellTypes.FindType(comboBoxCellTypes.Text));
        }

        private void saveCellTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
//            Parent.ListCellTypes.FindType(comboBoxCellTypes.Text);

            SaveFileDialog CurrSavefileDialog = new SaveFileDialog();
            CurrSavefileDialog.Filter = "opt files (*.CT)|*.CT";
            DialogResult Res = CurrSavefileDialog.ShowDialog();
            if (Res != DialogResult.OK) return;

            if (CurrSavefileDialog.FileName == "") return;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(CurrSavefileDialog.FileName,
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, Parent.ListCellTypes.FindType(comboBoxCellTypes.Text));
            stream.Close();

          


        }

        private void loadCellTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog CurrOpenFileDialog = new OpenFileDialog();

            CurrOpenFileDialog.Filter = "opt files (*.CT)|*.CT";
            DialogResult Res = CurrOpenFileDialog.ShowDialog();
            if (Res != DialogResult.OK) return;

            if (CurrOpenFileDialog.FileName == "") return;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(CurrOpenFileDialog.FileName,
                                      FileMode.Open,
                                      FileAccess.Read,
                                      FileShare.Read);
            cCellType obj = (cCellType)formatter.Deserialize(stream);
            stream.Close();

            Parent.ListCellTypes.Add(obj);
            //MyPanelForParamCellTypes.RefreshDisplay(this);
            this.RefreshDisplay(Parent);

        }

    }
}
