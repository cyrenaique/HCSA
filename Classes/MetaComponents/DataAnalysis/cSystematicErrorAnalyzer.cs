using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.General_Types;
using LibPlateAnalysis;
using HCSAnalyzer.Forms.FormsForOptions.ClassForOptions.Children;
using HCSAnalyzer.Classes.Machine_Learning;
using HCSAnalyzer.Forms;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;

namespace HCSAnalyzer.Classes.MetaComponents
{
    class cSystematicErrorAnalyzer : cComponent
    {
        string AssociatedError = "";
        cListExtendedTable Output = new cListExtendedTable();

        cExtendedTable ClusteredTable = null;
        public cListPlates PlatesToProcess;
        public List<cDescriptorType> DescriptorsToProcess;

        int MinObjectsNumber = -1;
        cParamAlgo ParamAlgoForClustering = null;


        public cSystematicErrorAnalyzer()
        {
            this.Title = "Systematic Errors Analyzer";
        }

        //public  Run()
        //{

        //    if (this.Input == null)
        //    {
        //        FeedBackMessage.IsSucceed = false;
        //        FeedBackMessage.Message = "No input data defined.";
        //        return FeedBackMessage;
        //    }
        //    Process();
        //    return FeedBackMessage;
        //}

        public cListExtendedTable GetOuput()
        {
            return this.Output;
        }

        public cFeedBackMessage Run(bool IsDisplayResults)
        {
            cMachineLearning MachineLearning = new cMachineLearning(/*cWell.GlobalInfo*/null);
            ParamAlgoForClustering = MachineLearning.AskAndGetClusteringAlgo();

            if (MinObjectsNumber == -1)
                MinObjectsNumber = (cGlobalInfo.CurrentScreening.Columns * cGlobalInfo.CurrentScreening.Rows * (int)cGlobalInfo.OptionsWindow.numericUpDownSystemMinWellRatio.Value) / 100;

            foreach (cPlate CurrentPlate in PlatesToProcess)
            {
                cExtendedTable CurrentPlateResult = null;

                foreach (var item in this.DescriptorsToProcess)
                {
                    cExtendedTable ET = CurrentPlate.GetAverageValueList(item, false);
                    ET.Name = item.GetName();

                    cExtendedTable ResultTable = this.GenerateArtifactMessage(ET, CurrentPlate);
                    ResultTable.ListRowNames = new List<string>();
                    ResultTable.ListRowNames.Add(item.GetName());

                    if (CurrentPlateResult == null) CurrentPlateResult = new cExtendedTable(ResultTable);
                    else
                    {
                        cMerge M = new cMerge();
                        M.IsHorizontal = false;
                        M.SetInputData(CurrentPlateResult, ResultTable);
                        M.Run();
                        CurrentPlateResult = M.GetOutPut();
                    }

                }
                CurrentPlateResult.Name = CurrentPlate.GetName();
                CurrentPlateResult.Tag = CurrentPlate;

                this.Output.Add(CurrentPlateResult);
            }

            this.Output.Name = "Systematic Errors (" + PlatesToProcess.Count + " plate(s))";


            if (IsDisplayResults)
            {
                cDisplayToWindow DTW = new cDisplayToWindow();

                cDesignerMultiChoices DMC = new cDesignerMultiChoices();

                foreach (var item in this.Output)
                {

                    cDesignerSplitter DS = new cDesignerSplitter();
                    DS.Orientation = System.Windows.Forms.Orientation.Horizontal;
                    DS.Title = item.Name;

                    cViewerTable VT = new cViewerTable();
                    VT.SetInputData(item);
                    VT.DigitNumber = 0;
                    VT.Run();
                    DS.SetInputData(VT.GetOutPut());

                    cViewerHeatMap VHM = new cViewerHeatMap();
                    VHM.SetInputData(item);
                    VHM.IsAutomatedMinMax = false;
                    VHM.Min = 0;
                    VHM.Max = 1;
                    VHM.Run();
                    DS.SetInputData(VHM.GetOutPut());

                    DS.Run();
                    cExtendedControl TmpXCtrl = DS.GetOutPut();
                    TmpXCtrl.Tag = item.Tag;
                    DMC.SetInputData(TmpXCtrl);

                }

                DMC.Run();
                DTW.SetInputData(DMC.GetOutPut());
                DTW.Title = "Systematic Errors [" + PlatesToProcess.Count + " plate(s)]";
                
                DTW.Run();
                DTW.Display();


                //cDisplayListExtendedTable DLET = new cDisplayListExtendedTable();
                //DLET.SetInputData(this.Output);
                //return DLET.Run();
            }


            return FeedBackMessage;
        }

        private cExtendedTable GenerateArtifactMessage(cExtendedTable TmpTable, cPlate PlateToProcess)
        {
            cLinearize Lin = new cLinearize();
            Lin.SetInputData(TmpTable);
            Lin.Run();
            cExtendedTable LINTable = Lin.GetOutPut();

            cClustering Cluster = new cClustering();
            Cluster.SetInputData(LINTable);
            Cluster.ParamAlgoForClustering = this.ParamAlgoForClustering;
            if (Cluster.Run().IsSucceed == false) return null;

            this.ClusteredTable = Cluster.GetOutPut();

            // now clustering
            //if (!KMeans((int)cGlobalInfo.OptionsWindow.numericUpDownSystErrorIdentKMeansClasses.Value, PlateToProcess, CurrentDescSel))
            //{
            //    List<string> ListMessageError = new List<string>();
            //    ListMessageError.Add("K-Means Error");
            //    return ListMessageError;
            //}

            //// and finally classification

            //    return this.ComputePlateBasedClassification(MinObjectsNumber);

            //}

            cExtendedTable ET = PlateToProcess.ListWells.GetPositionRelatedSignatures();

            ET.Add(this.ClusteredTable[this.ClusteredTable.Count - 1]);

            weka.core.Instances insts = ET.CreateWekaInstancesWithClasses(); //CurrentPlateToProcess.CreateInstancesWithClassesWithPlateBasedDescriptor(Classes);
            weka.classifiers.trees.J48 ClassificationModel = new weka.classifiers.trees.J48();
            ClassificationModel.setMinNumObj(MinObjectsNumber);

            weka.core.Instances train = new weka.core.Instances(insts, 0, insts.numInstances());
            ClassificationModel.buildClassifier(train);

            string DotString = ClassificationModel.graph().Remove(0, ClassificationModel.graph().IndexOf("{") + 2);
            int DotLenght = DotString.Length;

            string NewDotString = DotString.Remove(DotLenght - 3, 3);

            // display the tree is requested
            //cDisplayTree DT = new cDisplayTree();
            //DT.SetInputData(NewDotString);
            //DT.Run();

            cExtendedTable ToReturn = new cExtendedTable(4, 1, 0);
            ToReturn.ListRowNames = new List<string>();
            ToReturn.ListTags = new List<object>();

            int CurrentPos = 0;
            int NextReturnPos = CurrentPos;
            List<int> ListNodeId = new List<int>();
            string TmpDotString = NewDotString;

            int TmpClass = 0;
            string ErrorString = "";
            int ErrorMessage = 0;

            
            ToReturn[0].Name = "Edge artifact";   // edge
            ToReturn[1].Name = "Column artifact";   // col
            ToReturn[2].Name = "Row artifact";   // row
            ToReturn[3].Name = "Bowl artifact";   // bowl

            #region build message
            while (NextReturnPos != -1)
            {
                int NextBracket = NewDotString.IndexOf("[");
                string StringToProcess = NewDotString.Remove(NextBracket - 1);
                string StringToProcess1 = StringToProcess.Remove(0, 1);

                if (StringToProcess1.Contains("N") == false)
                {
                    int Id = Convert.ToInt32(StringToProcess1);


                    int LabelPos = NewDotString.IndexOf("label=\"");
                    string LabelString = NewDotString.Remove(0, LabelPos + 7);
                    LabelPos = LabelString.IndexOf("\"");
                    string FinalLabel = LabelString.Remove(LabelPos);

                    // if (TmpClass < Classes)
                    {
                        if ((FinalLabel == "Dist_To_Border") || (FinalLabel == "Col_Pos") || (FinalLabel == "Row_Pos") || (FinalLabel == "Dist_To_Center"))
                        {
                            if ((FinalLabel == "Dist_To_Border") && (!ErrorString.Contains(" an edge effect")) && (!ErrorString.Contains(" a bowl effect")) && (ErrorMessage < 2))
                            {
                                if (TmpClass > 0) ErrorString += " combined with";
                                ErrorString += " an " + cGlobalInfo.ListArtifacts[0];
                                ErrorMessage++;
                                ToReturn[0][0] = 1;
                            }
                            else if ((FinalLabel == "Col_Pos") && (!ErrorString.Contains(" a column artifact")) && (ErrorMessage < 2))
                            {
                                if (TmpClass > 0) ErrorString += " combined with";
                                ErrorString += " a " + cGlobalInfo.ListArtifacts[1];
                                ErrorMessage++;
                                ToReturn[1][0] = 1;

                            }
                            else if ((FinalLabel == "Row_Pos") && (!ErrorString.Contains(" a row artifact")) && (ErrorMessage < 2))
                            {
                                if (TmpClass > 0) ErrorString += " combined with";
                                ErrorString += " a " + cGlobalInfo.ListArtifacts[2];
                                ErrorMessage++;
                                ToReturn[2][0] = 1;

                            }
                            else if ((FinalLabel == "Dist_To_Center") && (!ErrorString.Contains(" a bowl effect")) && (!ErrorString.Contains(" an edge effect")) && (ErrorMessage < 2))
                            {
                                if (TmpClass > 0) ErrorString += " combined with";
                                ErrorString += " a " + cGlobalInfo.ListArtifacts[3];
                                ErrorMessage++;
                                ToReturn[3][0] = 1;

                            }
                            TmpClass++;
                        }
                    }
                }

                NextReturnPos = NewDotString.IndexOf("\n");
                if (NextReturnPos != -1)
                {
                    string TmpString = NewDotString.Remove(0, NextReturnPos + 1);
                    NewDotString = TmpString;
                }
            }

            if (TmpClass == 0)
            {
                string NoError = "No systematic error detected !";
                ToReturn.ListTags.Add(NoError);
                //ToReturn.Add(NoError);
                return ToReturn;
            }

            string FinalString = "You have a systematic error !\nThis is " + ErrorString;

            NewDotString = TmpDotString;
            NextReturnPos = 0;
            while (NextReturnPos != -1)
            {
                int NextBracket = NewDotString.IndexOf("[");
                string StringToProcess = NewDotString.Remove(NextBracket - 1);
                string StringToProcess1 = StringToProcess.Remove(0, 1);

                if (StringToProcess1.Contains("N"))
                {
                    //// this is an edge
                    string stringNodeIdxStart = StringToProcess1.Remove(StringToProcess1.IndexOf("-"));
                    int NodeIdxStart = Convert.ToInt32(stringNodeIdxStart);

                    string stringNodeIdxEnd = StringToProcess1.Remove(0, StringToProcess1.IndexOf("N") + 1);
                    int NodeIdxSEnd = Convert.ToInt32(stringNodeIdxEnd);

                    int LabelPos = NewDotString.IndexOf("label=");
                    LabelPos += 7;

                    string CurrLabelString = NewDotString.Remove(0, LabelPos);
                }
                NextReturnPos = NewDotString.IndexOf("\n");

                if (NextReturnPos != -1)
                {
                    string TmpString = NewDotString.Remove(0, NextReturnPos + 1);
                    NewDotString = TmpString;
                }
            }

            ToReturn.ListTags.Add(FinalString + ".");
            #endregion

            return ToReturn;
        }



        #region Tree graph display functions
        public Microsoft.Msagl.Drawing.Graph ComputeAndDisplayGraph(string DotString, bool IsCellular)
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

        private Microsoft.Msagl.Drawing.Graph DisplayTheGraph(cPlate PlateForTree)
        {
            FormForClassificationTree WindowForTree = new FormForClassificationTree();

            WindowForTree.Text = PlateForTree.GetName();
            string StringForTree = PlateForTree.GetInfoClassif().StringForTree;
            if ((StringForTree == null) || (StringForTree.Length == 0))
                return null;

            WindowForTree.richTextBoxConsoleForClassification.Clear();
            WindowForTree.richTextBoxConsoleForClassification.AppendText(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetInfoClassif().StringForQuality);
            WindowForTree.richTextBoxConsoleForClassification.AppendText(cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().GetInfoClassif().ConfusionMatrix);

            return ComputeAndDisplayGraph(StringForTree.Remove(StringForTree.Length - 3, 3), false);

        }
        #endregion

    }
}
