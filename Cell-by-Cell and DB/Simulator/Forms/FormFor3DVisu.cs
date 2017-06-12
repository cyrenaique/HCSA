using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes._3D;
using LibPlateAnalysis;
using HCSAnalyzer.Simulator.Classes;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;

namespace HCSAnalyzer.Simulator.Forms
{
    public partial class FormFor3DVisu : Form
    {
        c3DWorld CurrentWorld;
        cScreening CompleteScreening;
        //cWorld NewWorld;
        FormForSimuGenerator Parent;

        public FormFor3DVisu(cScreening CompleteScreening, FormForSimuGenerator Parent)
        {
            InitializeComponent();
            this.CompleteScreening = CompleteScreening;
            //this.NewWorld = NewWorld;
            this.Parent = Parent;
        }

        private void renderWindowControlFor3D_Load(object sender, EventArgs e)
        {
            Display3D();
        }

        private void Display3D()
        {

        
            int[] ListPos =  new int[3];
            ListPos[0] = 0;
            ListPos[1] = 0;
            ListPos[2] = 0;

            if (CurrentWorld == null)
            {
                CurrentWorld = new c3DWorld(Parent.NewWorld.Dimensions, new cPoint3D(1,1,1), this.renderWindowControlFor3D, ListPos, CompleteScreening);
            }
            CurrentWorld.ren1.RemoveAllViewProps(); 
            foreach (var CurrentCell in Parent.NewWorld.ListCells)
            {
                c3DSphere CurrentSphere = CurrentCell.Get3DObj();
                
                if (Parent.ListClusteringAlgo.GetListParams("3D").GetListValuesParam().ListCheckValues.Get("checkBoxDisplayText").Value)
                    CurrentSphere.AddText(CurrentCell.Type.Name,CurrentWorld,2,CurrentCell.Type.TypeColor);

                if (Parent.ListClusteringAlgo.GetListParams("3D").GetListValuesParam().ListCheckValues.Get("checkBoxDisplayCellPath").Value)
                {
                    for (int Memory = 0; Memory < CurrentCell.PreviousStates.Count-1; Memory++)
                    {
                        c3DLine NewLineForMemory = new c3DLine(CurrentCell.PreviousStates[Memory].CentroidPosition,CurrentCell.PreviousStates[Memory + 1].CentroidPosition);
                        NewLineForMemory.Colour = CurrentCell.Type.TypeColor;
                        CurrentWorld.AddGeometric3DObject(NewLineForMemory);
                    }

                    c3DLine NewLineForMemoryFinal = new c3DLine(CurrentCell.PreviousStates[CurrentCell.PreviousStates.Count - 1].CentroidPosition,
                                                                CurrentCell.CentroidPosition);
                    NewLineForMemoryFinal.Colour = CurrentCell.Type.TypeColor;
                    CurrentWorld.AddGeometric3DObject(NewLineForMemoryFinal);

                }


                CurrentWorld.AddGeometric3DObject(CurrentSphere);
               // CurrentSphere.Colour = CurrentCell.Type.TypeColor;
                
            }


            CurrentWorld.DisplayBottom(Color.White);

            CurrentWorld.Render();
        
        
        }

        private void buttonDisplayVolumes_Click(object sender, EventArgs e)
        {
            cWindowToDisplayHisto WindowToDisplayHisto = new cWindowToDisplayHisto(CompleteScreening, Parent.NewWorld.ListCells.GetVolumes());
            WindowToDisplayHisto.Show();
        }

    }
}
