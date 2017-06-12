using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.GUI.FormsForGraphsDisplay.Generic;
using HCSAnalyzer.Classes.General_Types;
using Microsoft.Msagl.GraphViewerGdi;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    class cViewerGraphTree : cDataDisplay
    {

        string Input;

        public cViewerGraphTree()
        {
            this.Title = "Graph Tree Viewer";
        }

        public void SetInputData(string Input)
        {
            this.Input = Input;
            //CurrentPanelHisto = new cPanelHisto(ListValues, true, eGraphType.LINE, this.Orientation);
        }

        Microsoft.Msagl.Drawing.Graph ComputeAndDisplayGraph(string DotString, bool IsCellular)
        {
            int CurrentPos = 0;
            int NextReturnPos = CurrentPos;
            List<int> ListNodeId = new List<int>();
            string TmpDotString = DotString;

            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

            while (NextReturnPos != -1)
            {
                int NextBracket = DotString.IndexOf("[");
                string StringToProcess = DotString.Remove(NextBracket - 1);
                string StringToProcess1 = StringToProcess.Remove(0, 1);

                if (StringToProcess1.Contains("N") == false)
                {
                    int Id = Convert.ToInt32(StringToProcess1);
                    Microsoft.Msagl.Drawing.Node Currentnode = new Microsoft.Msagl.Drawing.Node(Id.ToString());
                    Currentnode.Label.FontColor = Microsoft.Msagl.Drawing.Color.Red;
                    int LabelPos = DotString.IndexOf("label=\"");
                    string LabelString = DotString.Remove(0, LabelPos + 7);
                    LabelPos = LabelString.IndexOf("\"");
                    string FinalLabel = LabelString.Remove(LabelPos);
                    Currentnode.LabelText = FinalLabel;

                    if (IsCellular && (FinalLabel[FinalLabel.Length - 1] == ')') && (FinalLabel.Contains('(')))   // that's a leaf
                    {
                        int LastBracket = FinalLabel.LastIndexOf('(');
                        string LeafName = FinalLabel.Remove(LastBracket - 1);

                        cCellularPhenotype CP = cGlobalInfo.ListCellularPhenotypes.FindFromName(LeafName);
                        if (CP != null)
                        {
                            Currentnode.Attr.FillColor = new Microsoft.Msagl.Drawing.Color(CP.ColourForDisplay.R, CP.ColourForDisplay.G, CP.ColourForDisplay.B);
                        }
                    }
                    graph.AddNode(Currentnode);
                }

                NextReturnPos = DotString.IndexOf("\n");
                if (NextReturnPos != -1)
                {
                    string TmpString = DotString.Remove(0, NextReturnPos + 1);
                    DotString = TmpString;
                }
            }

            DotString = TmpDotString;
            NextReturnPos = 0;
            while (NextReturnPos != -1)
            {
                int NextBracket = DotString.IndexOf("[");
                string StringToProcess = DotString.Remove(NextBracket - 1);
                string StringToProcess1 = StringToProcess.Remove(0, 1);

                if (StringToProcess1.Contains("N"))
                {
                    string stringNodeIdxStart = StringToProcess1.Remove(StringToProcess1.IndexOf("-"));
                    int NodeIdxStart = Convert.ToInt32(stringNodeIdxStart);

                    string stringNodeIdxEnd = StringToProcess1.Remove(0, StringToProcess1.IndexOf("N") + 1);
                    int NodeIdxSEnd = Convert.ToInt32(stringNodeIdxEnd);

                    int LabelPos = DotString.IndexOf("label=");
                    LabelPos += 7;

                    string CurrLabelString = DotString.Remove(0, LabelPos);

                    Microsoft.Msagl.Drawing.Edge Currentedge = new Microsoft.Msagl.Drawing.Edge(stringNodeIdxStart, ""/*NodeIdx.ToString()*/, stringNodeIdxEnd);
                    Currentedge.LabelText = CurrLabelString.Remove(CurrLabelString.IndexOf("]") - 1);
                    graph.Edges.Add(Currentedge);
                }

                NextReturnPos = DotString.IndexOf("\n");

                if (NextReturnPos != -1)
                {
                    string TmpString = DotString.Remove(0, NextReturnPos + 1);
                    DotString = TmpString;
                }
            }
            return graph;
        }



        public cFeedBackMessage Run()
        {
            this.CurrentPanel = new cExtendedControl();
            this.CurrentPanel.Title = this.Title;

            this.CurrentPanel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                        | System.Windows.Forms.AnchorStyles.Left
                                        | System.Windows.Forms.AnchorStyles.Right);

            GViewer GraphView = new GViewer();

            GraphView.Graph = ComputeAndDisplayGraph(this.Input,false);
            GraphView.Dock = System.Windows.Forms.DockStyle.Fill;

           // this.panel.Controls.Add(GraphView);



            CurrentPanel.Controls.Add(GraphView);
            return base.FeedBackMessage;
        }

    }
}
