using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;

namespace HCSAnalyzer.Classes.General_Types
{
    [Serializable]
    public class cDataForcGlobalInfoToBeExported
    {
        public List<string> ListPhenotypeNames = new List<string>();
        public List<Color> ListPhenotypeColors = new List<Color>();
        public List<string> ListClassNames = new List<string>();
        public List<Color> ListClassColors = new List<Color>();
        public string textBoxImageAccesImagePath;
        public bool radioButtonImageAccessDefinedChecked;
        public bool radioButtonImageAccessOperettaChecked;
        public bool radioButtonImageAccessOperetta35Checked;
        public bool radioButtonImageAccessImageXpressChecked;
        public bool radioButtonImageAccessCellomicsChecked;
        public bool radioButtonImageAccessINCellChecked;  
        public bool radioButtonImageAccessCV7000Checked;  
        public bool radioButtonImageAccessBuiltInChecked;
        public string CurrentFormForPlateLUTtoolStripComboBoxLUTText;
        public int numericUpDownImageAccessNumberOfChannels;
        public int numericUpDownImageAccessNumberOfFields;
        public int numericUpDownDefaultField;
    }



    public class cGlobalInfoToBeExported
    {
        public cDataForcGlobalInfoToBeExported DataForcGlobalInfoToBeExported;

        public string Save(string Path)
        {
            DataForcGlobalInfoToBeExported = new cDataForcGlobalInfoToBeExported();
            DataForcGlobalInfoToBeExported.ListPhenotypeNames = new List<string>();

            foreach (var item in cGlobalInfo.ListCellularPhenotypes)
            {
                DataForcGlobalInfoToBeExported.ListPhenotypeNames.Add(item.Name);
                DataForcGlobalInfoToBeExported.ListPhenotypeColors.Add(item.ColourForDisplay);
            }

            foreach (var item in cGlobalInfo.ListWellClasses)
            {
                DataForcGlobalInfoToBeExported.ListClassNames.Add(item.Name);
                DataForcGlobalInfoToBeExported.ListClassColors.Add(item.ColourForDisplay);
            }

            DataForcGlobalInfoToBeExported.textBoxImageAccesImagePath = cGlobalInfo.OptionsWindow.textBoxImageAccesImagePath.Text;
            DataForcGlobalInfoToBeExported.radioButtonImageAccessDefinedChecked = cGlobalInfo.OptionsWindow.radioButtonImageAccessDefined.Checked;
            DataForcGlobalInfoToBeExported.radioButtonImageAccessOperettaChecked = cGlobalInfo.OptionsWindow.radioButtonImageAccessHarmony.Checked;
            DataForcGlobalInfoToBeExported.radioButtonImageAccessOperetta35Checked = cGlobalInfo.OptionsWindow.radioButtonImageAccessHarmony35.Checked;
            DataForcGlobalInfoToBeExported.radioButtonImageAccessImageXpressChecked = cGlobalInfo.OptionsWindow.radioButtonImageAccessImageXpress.Checked;
            DataForcGlobalInfoToBeExported.radioButtonImageAccessCellomicsChecked = cGlobalInfo.OptionsWindow.radioButtonImageAccessCellomics.Checked;
            DataForcGlobalInfoToBeExported.radioButtonImageAccessINCellChecked = cGlobalInfo.OptionsWindow.radioButtonImageAccessINCell.Checked;
            DataForcGlobalInfoToBeExported.radioButtonImageAccessCV7000Checked = cGlobalInfo.OptionsWindow.radioButtonImageAccessCV7000.Checked;       
            DataForcGlobalInfoToBeExported.radioButtonImageAccessBuiltInChecked = cGlobalInfo.OptionsWindow.radioButtonImageAccessBuiltIn.Checked;

            DataForcGlobalInfoToBeExported.CurrentFormForPlateLUTtoolStripComboBoxLUTText = cGlobalInfo.GUIPlateLUT.CurrentFormForPlateLUT.toolStripComboBoxLUT.Text;

            DataForcGlobalInfoToBeExported.numericUpDownImageAccessNumberOfChannels = (int)cGlobalInfo.OptionsWindow.numericUpDownImageAccessNumberOfChannels.Value;
            DataForcGlobalInfoToBeExported.numericUpDownImageAccessNumberOfFields = (int)cGlobalInfo.OptionsWindow.numericUpDownImageAccessNumberOfFields.Value;
            DataForcGlobalInfoToBeExported.numericUpDownDefaultField = (int)cGlobalInfo.OptionsWindow.numericUpDownDefaultField.Value;

            if (Path == "")
            {
                SaveFileDialog CurrSavefileDialog = new SaveFileDialog();
                CurrSavefileDialog.Filter = "options file (*.opt)|*.opt";
                CurrSavefileDialog.FileName = "Options.opt";
                DialogResult Res = CurrSavefileDialog.ShowDialog();
                if (Res != DialogResult.OK) return "";

                if (CurrSavefileDialog.FileName == "") return "";

                Path = CurrSavefileDialog.FileName;
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path,
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, (cDataForcGlobalInfoToBeExported)DataForcGlobalInfoToBeExported);
            stream.Close();

            return Path;

        }

        public void Load(string Path)
        {
            if (Path == "")
            {
                OpenFileDialog CurrOpenFileDialog = new OpenFileDialog();

                CurrOpenFileDialog.Filter = "options file (*.OPT)|*.OPT";
                CurrOpenFileDialog.Multiselect = false;
                DialogResult Res = CurrOpenFileDialog.ShowDialog();
                if (Res != DialogResult.OK) return;

                if (CurrOpenFileDialog.FileName == "") return;
                Path = CurrOpenFileDialog.FileName;
            }

            // cDataForcGlobalInfoToBeExported MyObj = null;
            //using (var stream = System.IO.File.OpenRead(Path))
            //{
            //    var serializer = new XmlSerializer(typeof(cDataForcGlobalInfoToBeExported));
            //    MyObj = (cDataForcGlobalInfoToBeExported)serializer.Deserialize(stream);
            //}

            cDataForcGlobalInfoToBeExported MyObj = null;
            try
            {

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(Path,
                                          FileMode.Open,
                                          FileAccess.Read,
                                          FileShare.Read);
                MyObj = (cDataForcGlobalInfoToBeExported)formatter.Deserialize(stream);
                stream.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error while loading:\n" + Path, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (MyObj == null) return;

            int Idx = 0;
            foreach (var item in MyObj.ListClassNames)
            {
                cGlobalInfo.ListWellClasses[Idx].Name = item;
                PanelForClassEditing PFCE = (PanelForClassEditing)cGlobalInfo.OptionsWindow.panelForWellClasses.Controls[0];
                PFCE.ListTextBoxes[Idx].Text = item;
                Idx++;
            }

            Idx = 0;
            foreach (var item in MyObj.ListClassColors)
            {
                cGlobalInfo.ListWellClasses[Idx].ColourForDisplay = item;
                Idx++;
            }

            Idx = 0;
            foreach (var item in MyObj.ListPhenotypeColors)
            {
                cGlobalInfo.ListCellularPhenotypes[Idx++].ColourForDisplay = item;

            }

            Idx = 0;
            foreach (var item in MyObj.ListPhenotypeNames)
            {
                cGlobalInfo.ListCellularPhenotypes[Idx].Name = item;
                
                PanelForPhenotypeEditing PFCE = (PanelForPhenotypeEditing)cGlobalInfo.OptionsWindow.panelForCellularPhenotypes.Controls[0];
                PFCE.ListTextBoxes[Idx].Text = item;
                Idx++;

            }

            if (MyObj.textBoxImageAccesImagePath != null)
                cGlobalInfo.OptionsWindow.textBoxImageAccesImagePath.Text = MyObj.textBoxImageAccesImagePath;

            if (MyObj.radioButtonImageAccessDefinedChecked != null)
                cGlobalInfo.OptionsWindow.radioButtonImageAccessDefined.Checked = MyObj.radioButtonImageAccessDefinedChecked;

            if (MyObj.radioButtonImageAccessOperettaChecked != null)
                cGlobalInfo.OptionsWindow.radioButtonImageAccessHarmony.Checked = MyObj.radioButtonImageAccessOperettaChecked;

            if (MyObj.radioButtonImageAccessOperetta35Checked != null)
                cGlobalInfo.OptionsWindow.radioButtonImageAccessHarmony35.Checked = MyObj.radioButtonImageAccessOperetta35Checked;

            if (MyObj.radioButtonImageAccessImageXpressChecked != null)
                cGlobalInfo.OptionsWindow.radioButtonImageAccessImageXpress.Checked = MyObj.radioButtonImageAccessImageXpressChecked;

            if (MyObj.radioButtonImageAccessCellomicsChecked != null)
                cGlobalInfo.OptionsWindow.radioButtonImageAccessCellomics.Checked = MyObj.radioButtonImageAccessCellomicsChecked;

            if (MyObj.radioButtonImageAccessINCellChecked != null)
                cGlobalInfo.OptionsWindow.radioButtonImageAccessINCell.Checked = MyObj.radioButtonImageAccessINCellChecked;

            if (MyObj.radioButtonImageAccessCV7000Checked != null)
                cGlobalInfo.OptionsWindow.radioButtonImageAccessCV7000.Checked = MyObj.radioButtonImageAccessCV7000Checked;  
            
            if (MyObj.radioButtonImageAccessBuiltInChecked != null)
                cGlobalInfo.OptionsWindow.radioButtonImageAccessBuiltIn.Checked = MyObj.radioButtonImageAccessBuiltInChecked;

            if (MyObj.CurrentFormForPlateLUTtoolStripComboBoxLUTText != null)
            {
                if (cGlobalInfo.GUIPlateLUT == null)
                    cGlobalInfo.GUIPlateLUT = new Forms.cGUIPlateLUT();
                cGlobalInfo.GUIPlateLUT.CurrentFormForPlateLUT.toolStripComboBoxLUT.Text = MyObj.CurrentFormForPlateLUTtoolStripComboBoxLUTText;
            }

            if (MyObj.numericUpDownImageAccessNumberOfChannels != 0)
                cGlobalInfo.OptionsWindow.numericUpDownImageAccessNumberOfChannels.Value = (decimal)MyObj.numericUpDownImageAccessNumberOfChannels;

            if (MyObj.numericUpDownImageAccessNumberOfFields != 0)
                cGlobalInfo.OptionsWindow.numericUpDownImageAccessNumberOfFields.Value = (decimal)MyObj.numericUpDownImageAccessNumberOfFields;

            cGlobalInfo.OptionsWindow.numericUpDownDefaultField.Value = (decimal)MyObj.numericUpDownDefaultField;

        }



    }
}
