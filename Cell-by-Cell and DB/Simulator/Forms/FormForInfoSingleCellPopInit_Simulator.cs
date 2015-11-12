using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Simulator.Classes;
using HCSAnalyzer.Classes._3D;

namespace HCSAnalyzer.Simulator.Forms
{
    public partial class FormForInfoSingleCellPopInit_Simulator : Form
    {
        public cListAgents CellPopulation;
        cPoint3D WorldDim;
        //cWorld CurrentWorld;
        Random RND = new Random();


        public cListVariables ListVariables = null;

        public cClassForVariable v_CellNumber = null;
        public cClassForVariable v_InitPosX = null;
        public cClassForVariable v_InitPosY = null;
        public cClassForVariable v_InitPosZ = null;
        public cClassForVariable v_InitPosType = null;
        public cClassForVariable v_InitVolType = null;
        public cClassForVariable v_InitVol = null;

        FormForSimuGenerator Parent;

        public FormForInfoSingleCellPopInit_Simulator(cPoint3D WorldDim, cListVariables ListVariablesInput, FormForSimuGenerator Parent, cListAgents CellPopulation)
        {
            InitializeComponent();

            if (CellPopulation == null)
            {
                this.comboBoxCellType.Text = "Regular";
                this.ListVariables = ListVariablesInput;
                this.textBoxName.Text = "Population_" + Parent.MyPanelForParamCellPopulations.listViewForCellPopulations.Items.Count;
            }
            else
            {
                this.comboBoxCellType.Text = CellPopulation[0].Type.Name;
                this.textBoxName.Text = CellPopulation.Name;
                ListVariables = CellPopulation.AssociatedVariables;
            }

            this.WorldDim = WorldDim;
            this.Parent = Parent;

            this.v_CellNumber = ListVariables.FindVariable("v_CellNumber");
            this.v_InitPosX = ListVariables.FindVariable("v_InitPosX");
            this.v_InitPosY = ListVariables.FindVariable("v_InitPosY");
            this.v_InitPosZ = ListVariables.FindVariable("v_InitPosZ");
            this.v_InitPosType = ListVariables.FindVariable("v_InitPosType");
            this.v_InitVolType = ListVariables.FindVariable("v_InitVolType");
            this.v_InitVol = ListVariables.FindVariable("v_InitVol");

            if (v_InitVolType.Cst_Value == 0)
            {
                this.radioButtonVolumeRandom.Checked = false;
                this.radioButtonVolumeFixed.Checked = true;
            }
            else if (v_InitVolType.Cst_Value == 10)
            {
                this.radioButtonVolumeRandom.Checked = true;
                this.radioButtonVolumeFixed.Checked = false;
            }


            if (v_InitPosType.Cst_Value == 0)
            {
                this.radioButtonPosWorldCenter.Checked = true;
                this.radioButtonPosRandom.Checked = false;
                this.radioButtonPosManual.Checked = false;
            }
            else if (v_InitPosType.Cst_Value == 1)
            {
                this.radioButtonPosWorldCenter.Checked = false;
                this.radioButtonPosRandom.Checked = true;
                this.radioButtonPosManual.Checked = false;
            }
            else
            {
                this.radioButtonPosWorldCenter.Checked = false;
                this.radioButtonPosRandom.Checked = false;
                this.radioButtonPosManual.Checked = true;
            }

            this.numericUpDownInitialCellNumber.Value = (decimal)v_CellNumber.Cst_Value;
            this.numericUpDownManualX.Value = (decimal)v_InitPosX.Cst_Value;
            this.numericUpDownManualY.Value = (decimal)v_InitPosY.Cst_Value;
            this.numericUpDownManualZ.Value = (decimal)v_InitPosZ.Cst_Value;
            this.numericUpDownInitialVolumeManual.Value = (decimal)v_InitVol.Cst_Value;

            if (v_InitVol.IsConstant)
            {
                this.radioButtonVolumeFixed.Checked = true;
                this.radioButtonVolumeRandom.Checked = false;
            }
            else
            {
                this.radioButtonVolumeFixed.Checked = false;
                this.radioButtonVolumeRandom.Checked = true;
            }

            this.comboBoxCellType.Items.Clear();

            foreach (cCellType item in Parent.ListCellTypes)
                this.comboBoxCellType.Items.Add(item.Name);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            CellPopulation = new cListAgents(this.textBoxName.Text);
            string CellType = comboBoxCellType.Text;
            cCellCycle ClassicCellCycle = new cCellCycle(); // default cell cycle
            cPoint3D InitialPos = null;

            if (radioButtonPosWorldCenter.Checked)
                InitialPos = new cPoint3D(this.WorldDim.X / 2, this.WorldDim.Y / 2, this.WorldDim.Z / 2);

            for (int i = 0; i < (int)numericUpDownInitialCellNumber.Value; i++)
            {
                if (radioButtonPosManual.Checked)
                {
                    InitialPos = new cPoint3D((double)numericUpDownManualX.Value,
                        (double)numericUpDownManualY.Value,
                        (double)numericUpDownManualZ.Value);
                }
                else if (radioButtonPosRandom.Checked)
                {
                    InitialPos = new cPoint3D(RND.NextDouble() * this.WorldDim.X,
                        RND.NextDouble() * this.WorldDim.Y,
                        RND.NextDouble() * this.WorldDim.Z);
                }

                cCellType CurrentCellType = Parent.ListCellTypes.FindType(CellType);
                if (CurrentCellType == null) continue;

                cAgent NewCell = new cAgent(Parent.ListCellTypes.FindType(CellType), InitialPos, RND.NextDouble() /** ClassicCellCycle.ListProba.Count*/);

                //new cCell(
                //                            InitialPos,
                //                            2,
                //                            RND.NextDouble() * ClassicCellCycle.ListProba.Count);
                CellPopulation.Add(NewCell);
            }

            List<cClassForVariable> ListToReturn = new List<cClassForVariable>();
            this.v_CellNumber.Cst_Value = (double)numericUpDownInitialCellNumber.Value;
            ListToReturn.Add(this.v_CellNumber);

            this.v_InitPosX.Cst_Value = (double)numericUpDownManualX.Value;
            ListToReturn.Add(this.v_InitPosX);

            this.v_InitPosY.Cst_Value = (double)numericUpDownManualY.Value;
            ListToReturn.Add(this.v_InitPosY);

            this.v_InitPosZ.Cst_Value = (double)numericUpDownManualZ.Value;
            ListToReturn.Add(this.v_InitPosZ);

            if (this.radioButtonVolumeFixed.Checked)
                this.v_InitVol.IsConstant = true;
            else
                this.v_InitVol.IsConstant = false;


            this.v_InitVol.Cst_Value = (double)numericUpDownInitialVolumeManual.Value;
            ListToReturn.Add(this.v_InitVol);

            ListToReturn.Add(this.v_InitPosType);
            ListToReturn.Add(this.v_InitVolType);

            this.ListVariables = new cListVariables(ListToReturn);
        }

        private void radioButtonPosWorldCenter_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGUIPosition();
        }

        private void radioButtonPosRandom_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGUIPosition();
        }

        private void radioButtonPosManual_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGUIPosition();
        }

        void RefreshGUIPosition()
        {
            panelManualPos.Enabled = radioButtonPosManual.Checked;

            if (radioButtonPosWorldCenter.Checked)
                this.v_InitPosType.Cst_Value = 0;
            else if (radioButtonPosRandom.Checked)
                this.v_InitPosType.Cst_Value = 1;
            else
                this.v_InitPosType.Cst_Value = 2;
        }


        void RefreshGUIVolume()
        {
            this.numericUpDownInitialVolumeManual.Enabled = this.radioButtonVolumeFixed.Checked;

            if (this.radioButtonVolumeFixed.Checked)
                this.v_InitVolType.Cst_Value = 10;
            else
                this.v_InitVolType.Cst_Value = 0;
        }


        private void worldCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            numericUpDownManualX.Value = (decimal)(this.WorldDim.X / 2);
            numericUpDownManualY.Value = (decimal)(this.WorldDim.Y / 2);
            numericUpDownManualZ.Value = (decimal)(this.WorldDim.Z / 2);
        }

        private void randomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            numericUpDownManualX.Value = (decimal)(this.RND.NextDouble() * this.WorldDim.X / 2);
            numericUpDownManualY.Value = (decimal)(this.RND.NextDouble() * this.WorldDim.Y / 2);
            numericUpDownManualZ.Value = (decimal)(this.RND.NextDouble() * this.WorldDim.Z / 2);
        }

        #region UpDate Variables and associated display
        private void UpdateVar(cClassForVariable Var, Label CurrentLabel)
        {
            FormForVariableDef WindowForVarDef = new FormForVariableDef(Var);
            if (WindowForVarDef.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            Var = WindowForVarDef.thisVariable;

            if (CurrentLabel != null)
            {
                if (Var.IsConstant == false) CurrentLabel.ForeColor = Color.Firebrick;
                else CurrentLabel.ForeColor = Color.Black;
            }
        }

        private void labelForCellNumber_Click(object sender, EventArgs e)
        {
            UpdateVar(this.v_CellNumber, labelForCellNumber);
        }

        private void labelX_Click(object sender, EventArgs e)
        {
            UpdateVar(this.v_InitPosX, labelX);
        }

        private void labelY_Click(object sender, EventArgs e)
        {
            UpdateVar(this.v_InitPosY, labelY);
        }

        private void labelZ_Click(object sender, EventArgs e)
        {
            UpdateVar(this.v_InitPosZ, labelZ);
        }
        #endregion


        private void labelFixedManual_MouseDown(object sender, MouseEventArgs e)
        {
            UpdateVar(this.v_InitVol, null);
        }

        private void radioButtonVolumeRandom_CheckedChanged(object sender, EventArgs e)
        {
            this.RefreshGUIVolume();
        }

        private void radioButtonVolumeFixed_CheckedChanged(object sender, EventArgs e)
        {
            this.RefreshGUIVolume();
        }

        private void radioButtonVolumeFixed_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                UpdateVar(this.v_InitVol, null);
            }

        }

        private void radioButtonVolumeRandom_MouseClick(object sender, MouseEventArgs e)
        {
        //    if (e.Button == System.Windows.Forms.MouseButtons.Right)
        //    {
                FormForRandSpecOfVar WindowForRandSpec = new FormForRandSpecOfVar(this.v_InitVol.RandomInfo);
                if (WindowForRandSpec.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.v_InitVol.RandomInfo = WindowForRandSpec.RandomParam;
            
          //  }
        }



    }
}
