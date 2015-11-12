using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Msagl.GraphViewerGdi;
using HCSAnalyzer.Simulator.Classes;

namespace HCSAnalyzer.Simulator.Forms
{
    public partial class FormForCellTypeRelationships : Form
    {
        FormForSimuGenerator Parent;
        GViewer GraphView;
        Microsoft.Msagl.Drawing.Graph graph;

        public FormForCellTypeRelationships(FormForSimuGenerator Parent)
        {
            InitializeComponent();
            this.Parent = Parent;

            GraphView = new GViewer();
            graph = new Microsoft.Msagl.Drawing.Graph("graph");
            GraphView.Size = new System.Drawing.Size(panel.Width, panel.Height);
            GraphView.Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);

            RefreshDisplay();
            
        }

    

        void RefreshDisplay()
        {
         

            foreach (cCellType item in Parent.ListCellTypes)
            {
                Microsoft.Msagl.Drawing.Node NewNode = new Microsoft.Msagl.Drawing.Node(item.Name);
                NewNode.Label.FontColor = new Microsoft.Msagl.Drawing.Color(item.TypeColor.R, item.TypeColor.G, item.TypeColor.B);

                NewNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.DimGray;
                NewNode.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Ellipse;
                graph.AddNode(NewNode);
            }



            foreach (cCellType item in Parent.ListCellTypes)
            {
                foreach (cTransitionValue Transitions in item.ListInitialTransitions)
                {
                    if (Transitions.Value == 0) continue;
                    Microsoft.Msagl.Drawing.Edge Currentedge = new Microsoft.Msagl.Drawing.Edge(item.Name, Transitions.Value.ToString(), Transitions.DestType.Name);
                    //Currentedge.Label.FontSize = Transitions.Value;
                    //Currentedge.LabelText = ;
                    graph.Edges.Add(Currentedge);
                }


            }

            GraphView.Graph =  graph;
            
            GraphView.Dock = System.Windows.Forms.DockStyle.Fill; 

            this.panel.Controls.Add(GraphView);

        
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
