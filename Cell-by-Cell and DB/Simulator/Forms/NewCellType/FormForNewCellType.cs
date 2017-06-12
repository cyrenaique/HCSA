using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Forms.FormsForOptions.ClassForOptions.Children;
using HCSAnalyzer.Simulator.Classes;

namespace HCSAnalyzer.Simulator.Forms.NewCellType
{
    public partial class FormForNewCellType : Form
    {

        #region List Parameters

        cListParams ListParams;
        public PanelForTransitionMatrix TransitionMatrixGUI;

        public class cListParams : List<cParamAlgo>
        {
            public cListParams(FormForSimuGenerator Parent, FormForNewCellType DirectParent)
            {
                this.Add(new cParamGeneral("General"));
                this.Add(new cParamTransitionMatrix("TransitionMatrix", Parent, DirectParent));
                this.Add(new cParamVelocity("Velocity"));
                this.Add(new cParamCellCycle("CellCycle"));
            }

            public Panel GetPanel(string Name)
            {
                if (Name == null) return null;

                foreach (var item in this)
                    if (item.Name == Name) return item.GetPanel();

                return null;
            }

            public cParamAlgo GetListParams(string CategoryName)
            {
                foreach (cParamAlgo item in this)
                {
                    if (item.Name == CategoryName) return item;
                }
                return null;

            }
        }

        [Serializable]
        public class cParamCellCycle : cParamAlgo
        {
            public cParamCellCycle(string Name)
                : base(Name)
            {
                PanelForParamCellCycle PanelForOption = new PanelForParamCellCycle();
                this.PanelToDisplay = PanelForOption.panel;
            }
        }

        [Serializable]
        public class cParamVelocity : cParamAlgo
        {
            public cParamVelocity(string Name)
                : base(Name)
            {
                PanelForParamVelocity PanelForOption = new PanelForParamVelocity();
                this.PanelToDisplay = PanelForOption.panel;
            }
        }

        [Serializable]
        public class cParamGeneral : cParamAlgo
        {
            public cParamGeneral(string Name)
                : base(Name)
            {
                PanelForParamGeneralForCellType PanelForOption = new PanelForParamGeneralForCellType();
                this.PanelToDisplay = PanelForOption.panel;
            }
        }



        [Serializable]
        public class cParamTransitionMatrix : cParamAlgo
        {
            public cParamTransitionMatrix(string Name, FormForSimuGenerator Parent, FormForNewCellType DirectParent)
                : base(Name)
            {
                PanelForTransitionMatrix PanelForOption = new PanelForTransitionMatrix(Parent, DirectParent);
                this.PanelToDisplay = PanelForOption.panel;
            }
        }

        #endregion
        FormForSimuGenerator Parent;


        public FormForNewCellType(FormForSimuGenerator Parent)
        {
            InitializeComponent();

            this.treeViewForOptions.ExpandAll();
            this.treeViewForOptions.SelectedNode = this.treeViewForOptions.Nodes[0];
            this.ListParams = new cListParams(Parent, this);
            this.Parent = Parent;

        }
        public FormForNewCellType(FormForSimuGenerator Parent, cCellType CurrentType)
        {
            InitializeComponent();

            this.treeViewForOptions.ExpandAll();
            this.treeViewForOptions.SelectedNode = this.treeViewForOptions.Nodes[0];
            this.ListParams = new cListParams(Parent, this);
            this.Parent = Parent;
            // ListParams.GetListParams("Velocity")
            // ListParams.GetListParams("Velocity").GetListValuesParam().ListDoubleValues.Get("numericUpDownFront").Value = 100;

            UpdateNumUpDownValue("Velocity", "numericUpDownFront", CurrentType.Velocity.Weight_Front);
            UpdateNumUpDownValue("Velocity", "numericUpDownBack", CurrentType.Velocity.Weight_Back);
            UpdateNumUpDownValue("Velocity", "numericUpDownTop", CurrentType.Velocity.Weight_Top);
            UpdateNumUpDownValue("Velocity", "numericUpDownBottom", CurrentType.Velocity.Weight_Bottom);
            UpdateNumUpDownValue("Velocity", "numericUpDownLeft", CurrentType.Velocity.Weight_Left);
            UpdateNumUpDownValue("Velocity", "numericUpDownRight", CurrentType.Velocity.Weight_Right);

            UpdatePanelCtrlColor("General", "panelColor", CurrentType.TypeColor);
            UpdateCtrlTextValue("General", "textBoxName", CurrentType.Name);
            UpDateTransitionMatrix("TransitionMatrix", "dataGridViewForProbaTransition", CurrentType.ListInitialTransitions);
        }


        void UpDateTransitionMatrix(string ParamPanelName, string ParamName, cListTransition ListTransitionValues)
        {
            cParamAlgo MyParamAlgo = ListParams.GetListParams(ParamPanelName);
            Control[] ListCtrl = MyParamAlgo.GetPanel().Controls.Find(ParamName, true);

            DataGridView MyGrid = (DataGridView)ListCtrl[0];

            //     DirectParent.TransitionMatrixGUI = this;
            MyGrid.Rows.Clear();

            foreach (var item in ListTransitionValues)
            {
                MyGrid.Rows.Add();
                MyGrid.Rows[MyGrid.Rows.Count - 1].HeaderCell.Value = item.DestType.Name;
                MyGrid[0, MyGrid.Rows.Count - 1].Value = item.Value;
            }
            //   MyGrid.ColumnCount;
            MyGrid.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);
        }

        void UpdateCtrlTextValue(string ParamPanelName, string ParamName, string NewValue)
        {
            cParamAlgo MyParamAlgo = ListParams.GetListParams(ParamPanelName);
            MyParamAlgo.GetListValuesParam().ListTextValues.Get(ParamName).Value = NewValue;

            Control[] ListCtrl = MyParamAlgo.GetPanel().Controls.Find(ParamName, true);
            if ((ListCtrl != null) && (ListCtrl.Length == 1))
                ListCtrl[0].Text = NewValue;
        }

        void UpdateNumUpDownValue(string ParamPanelName, string ParamName, double NewValue)
        {
            cParamAlgo MyParamAlgo = ListParams.GetListParams(ParamPanelName);
            MyParamAlgo.GetListValuesParam().ListDoubleValues.Get(ParamName).Value = NewValue;

            Control[] ListCtrl = MyParamAlgo.GetPanel().Controls.Find(ParamName, true);
            if ((ListCtrl != null) && (ListCtrl.Length == 1))
                ListCtrl[0].Text = NewValue.ToString();
        }

        void UpdatePanelCtrlColor(string ParamPanelName, string ParamName, Color NewValue)
        {
            cParamAlgo MyParamAlgo = ListParams.GetListParams(ParamPanelName);
            MyParamAlgo.GetListValuesParam().ListColorValues.Get(ParamName).Value = NewValue;

            Control[] ListCtrl = MyParamAlgo.GetPanel().Controls.Find(ParamName, true);
            if ((ListCtrl != null) && (ListCtrl.Length == 1))
            {
                ListCtrl[0].BackColor = NewValue;
            }
        }



        private void treeViewForOptions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.panelForDisplay.Controls.Clear();
            string TagName = (string)e.Node.Tag;
            Panel PanelToDisp = ListParams.GetPanel(TagName);
            if (PanelToDisp == null) return;
            this.panelForDisplay.Controls.Add(PanelToDisp);
        }

        public cCellType NewCellType;

        private void buttonOk_Click(object sender, EventArgs e)
        {
            double Front = (double)(ListParams.GetListParams("Velocity").GetListValuesParam().ListDoubleValues.Get("numericUpDownFront").Value);
            double Back = (double)(ListParams.GetListParams("Velocity").GetListValuesParam().ListDoubleValues.Get("numericUpDownBack").Value);
            double Left = (double)(ListParams.GetListParams("Velocity").GetListValuesParam().ListDoubleValues.Get("numericUpDownLeft").Value);
            double Right = (double)(ListParams.GetListParams("Velocity").GetListValuesParam().ListDoubleValues.Get("numericUpDownRight").Value);
            double Top = (double)(ListParams.GetListParams("Velocity").GetListValuesParam().ListDoubleValues.Get("numericUpDownTop").Value);
            double Bottom = (double)(ListParams.GetListParams("Velocity").GetListValuesParam().ListDoubleValues.Get("numericUpDownBottom").Value);

            cVelocity Velocity = new cVelocity(Front, Back, Left, Right, Top, Bottom);
            Velocity.Speed = 1;

            string Name = (string)(ListParams.GetListParams("General").GetListValuesParam().ListTextValues.Get("textBoxName").Value);

            Color PickedColor = (Color)(ListParams.GetListParams("General").GetListValuesParam().ListColorValues.Get("panelColor").Value);

            List<cTransitionValue> NewListTransitions = new List<cTransitionValue>();

            for (int IdxRow = 0; IdxRow < this.TransitionMatrixGUI.dataGridViewForProbaTransition.Rows.Count; IdxRow++)
            {

                // this.TransitionMatrixGUI.dataGridView.Rows[IdxRow];
                NewListTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType((string)this.TransitionMatrixGUI.dataGridViewForProbaTransition.Rows[IdxRow].HeaderCell.Value),
                                                            Convert.ToDouble(this.TransitionMatrixGUI.dataGridViewForProbaTransition.Rows[IdxRow].Cells[0].Value.ToString())));
            }


            NewCellType = new cCellType(Name, Velocity, PickedColor, new cCellCycle(), NewListTransitions);

            NewCellType.ListInitialTransitions[NewCellType.ListInitialTransitions.Count - 1].DestType = NewCellType;
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.treeViewForOptions.CollapseAll();
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.treeViewForOptions.ExpandAll();
        }
    }
}
