using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using weka.clusterers;
using weka.core;
using HCSAnalyzer.Forms.FormsForOptions;
using HCSAnalyzer.Forms.FormsForOptions.ClassForOptions.Children;
using System.Windows;
using System.Windows.Forms;
using System.Data;
using weka.classifiers;
using HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo;
using weka.classifiers.trees;
using Microsoft.Msagl.GraphViewerGdi;
using HCSAnalyzer.Forms;
using weka.classifiers.functions;
using weka.classifiers.rules;
using weka.classifiers.bayes;
using weka.classifiers.lazy;
using LibPlateAnalysis;
using HCSAnalyzer.Forms.IO;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Cell_by_Cell_and_DB;
using HCSAnalyzer.Classes.Base_Classes.GUI;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;

namespace HCSAnalyzer.Classes.Machine_Learning
{

    public class cClusteringObject
    {
        public Evaluation Evaluation;
        public Classifier Model;
        public int FoldNumber;

        public cClusteringObject(Classifier Model, Evaluation Evaluation, int NumFolds)
        {
            this.FoldNumber = NumFolds;
            this.Model = Model;
            this.Evaluation = Evaluation;
        }
    }

    public class cMachineLearning
    {
        cGlobalInfo GlobalInfo;
        public int NumberOfClusters { get; private set; }
        // public Instances ListInstancesWithoutClasses;
        public FastVector attValsWithoutClasses;
        //  public FastVector attValsWithClasses;
        public Clusterer SelectedClusterer;
        public Classifier CurrentClassifier;
        //DataTable dt;
        cExtendedTable InputTable;
        public cExtendedList Classes = new cExtendedList();
        List<string> ListDescriptors = new List<string>();

        public cMachineLearning(cGlobalInfo GlobalInfo)
        {
            this.GlobalInfo = GlobalInfo;
        }

        //public cMachineLearning(cGlobalInfo GlobalInfo, Instances ListIntancesWithoutClasses)
        //{
        //    this.GlobalInfo = GlobalInfo;
        //    this.ListInstancesWithoutClasses = ListIntancesWithoutClasses;

        //}

        #region Clustering
        public cParamAlgo AskAndGetClusteringAlgo()
        {
            FormForClusteringInfo WindowForClusteringParam = new FormForClusteringInfo(this.ListDescriptors, this.GlobalInfo);
            if (WindowForClusteringParam.ShowDialog() != System.Windows.Forms.DialogResult.OK) return null;

            return WindowForClusteringParam.GetSelectedAlgoAndParameters();
        }


        public void UpDateNumberOfCluster()
        {
            this.NumberOfClusters = (int)this.Classes.Max() + 1;
        }

        /// <summary>
        /// display a GUI and generate the WEKA based clusterer
        /// </summary>
        /// <param name="InstancesList">list of the weka instance</param>
        /// <returns>weka clusterer</returns>
        public Clusterer BuildClusterer(cParamAlgo ClusteringAlgo, cExtendedTable Input)
        {
            this.InputTable = Input;

            foreach (var item in Input)
            {
                this.ListDescriptors.Add(item.Name);
            }

            cListValuesParam Parameters = ClusteringAlgo.GetListValuesParam();

            Clusterer ClustererToReturn = null;
            Instances ListInstancesWithoutClasses = CreateInstancesWithoutClass(Input);
            #region EM
            if (ClusteringAlgo.Name == "EM")
            {
                ClustererToReturn = new EM();

                if (Parameters.ListCheckValues.Get("checkBoxAutomatedClassNum").Value)
                    ((EM)ClustererToReturn).setNumClusters(-1);
                else
                    ((EM)ClustererToReturn).setNumClusters((int)Parameters.ListDoubleValues.Get("numericUpDownNumClasses").Value);

                ((EM)ClustererToReturn).setMaxIterations((int)Parameters.ListDoubleValues.Get("numericUpDownMaxIterations").Value);
                ((EM)ClustererToReturn).setMinStdDev((double)Parameters.ListDoubleValues.Get("numericUpDownMinStdev").Value);
                ((EM)ClustererToReturn).setSeed((int)Parameters.ListDoubleValues.Get("numericUpDownSeedNumber").Value);
                ClustererToReturn.buildClusterer(ListInstancesWithoutClasses);
                this.NumberOfClusters = ClustererToReturn.numberOfClusters();
            }
            #endregion
            #region K Means
            else if (ClusteringAlgo.Name == "K-Means")
            {
                ClustererToReturn = new SimpleKMeans();
                ((SimpleKMeans)ClustererToReturn).setNumClusters((int)Parameters.ListDoubleValues.Get("numericUpDownNumClasses").Value);
                ((SimpleKMeans)ClustererToReturn).setSeed((int)Parameters.ListDoubleValues.Get("numericUpDownSeedNumber").Value);

                string DistanceType = (string)Parameters.ListTextValues.Get("comboBoxDistance").Value;

                if (DistanceType == "Euclidean")
                {
                    EuclideanDistance ED = new EuclideanDistance();
                    ED.setDontNormalize(!(bool)Parameters.ListCheckValues.Get("checkBoxNormalize").Value);
                    ((SimpleKMeans)ClustererToReturn).setDistanceFunction(ED);
                }
                else if (DistanceType == "Manhattan")
                {
                    ManhattanDistance MD = new ManhattanDistance();
                    MD.setDontNormalize(!(bool)Parameters.ListCheckValues.Get("checkBoxNormalize").Value);
                    ((SimpleKMeans)ClustererToReturn).setDistanceFunction(MD);
                }
                else return null;
                ClustererToReturn.buildClusterer(ListInstancesWithoutClasses);
                this.NumberOfClusters = ClustererToReturn.numberOfClusters();
            }
            #endregion
            //#region K Means++
            //else if (ClusteringAlgo.Name == "K-Means++")
            //{
            //    ClustererToReturn = new SimpleKMeans();
            //    ((SimpleKMeans)ClustererToReturn).setNumClusters((int)Parameters.ListDoubleValues.Get("numericUpDownNumClasses").Value);
            //    ((SimpleKMeans)ClustererToReturn).setSeed((int)Parameters.ListDoubleValues.Get("numericUpDownSeedNumber").Value);

            //    string DistanceType = (string)Parameters.ListTextValues.Get("comboBoxDistance").Value;

            //    if (DistanceType == "Euclidean")
            //    {
            //        EuclideanDistance ED = new EuclideanDistance();
            //        ED.setDontNormalize(!(bool)Parameters.ListCheckValues.Get("checkBoxNormalize").Value);
            //        ((SimpleKMeans)ClustererToReturn).setDistanceFunction(ED);
            //    }
            //    else if (DistanceType == "Manhattan")
            //    {
            //        ManhattanDistance MD = new ManhattanDistance();
            //        MD.setDontNormalize(!(bool)Parameters.ListCheckValues.Get("checkBoxNormalize").Value);
            //        ((SimpleKMeans)ClustererToReturn).setDistanceFunction(MD);
            //    }
            //    else return null;
            //    ClustererToReturn.buildClusterer(ListInstancesWithoutClasses);
            //    this.NumberOfClusters = ClustererToReturn.numberOfClusters();
            //}
            //#endregion

            #region hierarchical
            else if (ClusteringAlgo.Name == "Hierarchical")
            {
                ClustererToReturn = new weka.clusterers.HierarchicalClusterer();
                string OptionDistance = " -N " + (int)Parameters.ListDoubleValues.Get("numericUpDownNumClasses").Value;

                string DistanceType = (string)Parameters.ListTextValues.Get("comboBoxDistance").Value;
                OptionDistance += " -A \"weka.core.";
                switch (DistanceType)
                {
                    case "Euclidean":
                        OptionDistance += "EuclideanDistance";
                        break;
                    case "Manhattan":
                        OptionDistance += "ManhattanDistance";
                        break;
                    case "Chebyshev":
                        OptionDistance += "ChebyshevDistance";
                        break;
                    default:
                        break;
                }

                if (!(bool)Parameters.ListCheckValues.Get("checkBoxNormalize").Value)
                    OptionDistance += " -D";
                OptionDistance += " -R ";


                OptionDistance += "first-last\"";
                string WekaOption = "-L " + (string)Parameters.ListTextValues.Get("comboBoxLinkType").Value + OptionDistance;
                ((HierarchicalClusterer)ClustererToReturn).setOptions(weka.core.Utils.splitOptions(WekaOption));

                ClustererToReturn.buildClusterer(ListInstancesWithoutClasses);
                this.NumberOfClusters = ClustererToReturn.numberOfClusters();
            }
            #endregion
            #region Farthest First
            else if (ClusteringAlgo.Name == "FarthestFirst")
            {
                ClustererToReturn = new weka.clusterers.FarthestFirst();

                ((FarthestFirst)ClustererToReturn).setNumClusters((int)Parameters.ListDoubleValues.Get("numericUpDownNumClasses").Value);
                ((FarthestFirst)ClustererToReturn).setSeed((int)Parameters.ListDoubleValues.Get("numericUpDownSeedNumber").Value);
                ClustererToReturn.buildClusterer(ListInstancesWithoutClasses);
                this.NumberOfClusters = ClustererToReturn.numberOfClusters();
            }
            #endregion
            #region CobWeb
            else if (ClusteringAlgo.Name == "CobWeb")
            {
                ClustererToReturn = new weka.clusterers.Cobweb();

                ((Cobweb)ClustererToReturn).setSeed((int)Parameters.ListDoubleValues.Get("numericUpDownSeedNumber").Value);
                ((Cobweb)ClustererToReturn).setAcuity((double)Parameters.ListDoubleValues.Get("numericUpDownAcuity").Value);
                ((Cobweb)ClustererToReturn).setCutoff((double)Parameters.ListDoubleValues.Get("numericUpDownCutOff").Value);
                ClustererToReturn.buildClusterer(ListInstancesWithoutClasses);

                this.NumberOfClusters = ClustererToReturn.numberOfClusters();
            }
            #endregion
            #region Manual
            else if (ClusteringAlgo.Name == "Manual")
            {
                string DescriptorName = (string)Parameters.ListTextValues.Get("comboBoxForDescriptorManualClustering").Value;

                //  this.Classes = new double[ListInstancesWithoutClasses.numInstances()];

                for (int IdxPt = 0; IdxPt < this.Classes.Count / 2; IdxPt++)
                {
                    this.Classes[IdxPt] = 2;
                }
                this.NumberOfClusters = 2;
                //    break;

                //int IdxDesc = -1;
                //foreach (string item in this.ListDescriptors)
                //{
                //    IdxDesc++;
                //    if (item == DescriptorName) break;
                //}

                //int Idx=0;


                //foreach (Instance item in ListInstancesWithoutClasses)
                //{
                //    this.Classes.Add(((int)item.value(IdxDesc)) % cGlobalInfo.ListCellularPhenotypes.Count);
                //}

                //// re - ordonner les valeurs du discripteur afin que les classes se suivent sans laisser de classe vide !!
                //this.NumberOfClusters =  cGlobalInfo.ListCellularPhenotypes.Count;
            }
            #endregion

            else
            {
                System.Windows.Forms.MessageBox.Show("Clustering method not implemented !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return ClustererToReturn;
        }

        /// <summary>
        /// Evalute and display a WEKA clusterer
        /// </summary>
        /// <param name="SelectedClusterer">weka clusterer</param>
        /// <param name="InstancesList">list of instances for the validation</param>
        /// <param name="RichTextBoxToDisplayResults">Text box for the results (can be NULL)</param>
        /// <param name="PanelTodisplayGraphicalResults">Panel to display visual results if avalaible (can be NULL)</param>
        /// <returns></returns>
        public ClusterEvaluation EvaluteAndDisplayClusterer(RichTextBox RichTextBoxToDisplayResults,
                                                            Panel PanelTodisplayGraphicalResults, Instances ListInstanceForValid)
        {
            ClusterEvaluation eval = new ClusterEvaluation();
            eval.setClusterer(SelectedClusterer);
            eval.evaluateClusterer(ListInstanceForValid);


            if (RichTextBoxToDisplayResults != null)
            {

                if ((RichTextBoxToDisplayResults != null) && (eval.getNumClusters() > cGlobalInfo.ListCellularPhenotypes.Count))
                {
                    RichTextBoxToDisplayResults.Clear();
                    RichTextBoxToDisplayResults.AppendText("Error: " + eval.getNumClusters() + " clusters identifed.");
                    RichTextBoxToDisplayResults.AppendText("The maximum number of cluster is " + cGlobalInfo.ListCellularPhenotypes.Count + ".");
                    return null;

                }
                if (RichTextBoxToDisplayResults != null)
                {
                    RichTextBoxToDisplayResults.Clear();
                    RichTextBoxToDisplayResults.AppendText(eval.clusterResultsToString());
                }


                RichTextBoxToDisplayResults.AppendText("\n" + ListInstanceForValid.numAttributes() + " attributes:\n\n");
                for (int IdxAttributes = 0; IdxAttributes < ListInstanceForValid.numAttributes(); IdxAttributes++)
                {
                    RichTextBoxToDisplayResults.AppendText(IdxAttributes + "\t: " + ListInstanceForValid.attribute(IdxAttributes).name() + "\n");
                }
            }


            if (PanelTodisplayGraphicalResults != null) PanelTodisplayGraphicalResults.Controls.Clear();

            if ((PanelTodisplayGraphicalResults != null) && (SelectedClusterer.GetType().Name == "HierarchicalClusterer"))
            {
                Button ButtonToDisplayHierarchicalClustering = new Button();
                ButtonToDisplayHierarchicalClustering.Text = "Display Hierarchical Tree";
                ButtonToDisplayHierarchicalClustering.Width *= 2;
                ButtonToDisplayHierarchicalClustering.Location = new System.Drawing.Point((PanelTodisplayGraphicalResults.Width - ButtonToDisplayHierarchicalClustering.Width) / 2,
                    (PanelTodisplayGraphicalResults.Height - ButtonToDisplayHierarchicalClustering.Height) / 2);

                ButtonToDisplayHierarchicalClustering.Anchor = AnchorStyles.None;
                ButtonToDisplayHierarchicalClustering.Click += new EventHandler(ClickToDisplayHierarchicalTree);
                PanelTodisplayGraphicalResults.Controls.Add(ButtonToDisplayHierarchicalClustering);
            }

            return eval;

        }
        #endregion

        public FormForClassificationInfo AskAndGetClassifAlgo()
        {
            FormForClassificationInfo WindowForClassifParam = new FormForClassificationInfo(this.GlobalInfo);
            if (WindowForClassifParam.ShowDialog() != System.Windows.Forms.DialogResult.OK) return null;

            return WindowForClassifParam;//.GetSelectedAlgoAndParameters();
        }

        /// <summary>
        /// Build the learning model for classification
        /// </summary>
        /// <param name="InstancesList">list of instances </param>
        /// <param name="NumberofClusters">Number of Clusters</param>
        /// <param name="TextBoxForFeedback">Text box for the results (can be NULL)</param>
        /// <param name="PanelForVisualFeedback">Panel to display visual results if avalaible (can be NULL)</param>
        public Classifier PerformTraining(FormForClassificationInfo WindowForClassificationParam, Instances InstancesList, /*int NumberofClusters,*/ RichTextBox TextBoxForFeedback,
                                            Panel PanelForVisualFeedback, out weka.classifiers.Evaluation ModelEvaluation, bool IsCellular)
        {
            //   weka.classifiers.Evaluation ModelEvaluation = null;
            // FormForClassificationInfo WindowForClassificationParam = new FormForClassificationInfo(GlobalInfo);
            ModelEvaluation = null;
            //  if (WindowForClassificationParam.ShowDialog() != System.Windows.Forms.DialogResult.OK) return null;
            //   weka.classifiers.Evaluation ModelEvaluation = new Evaluation(


            cParamAlgo ClassifAlgoParams = WindowForClassificationParam.GetSelectedAlgoAndParameters();
            if (ClassifAlgoParams == null) return null;

            //this.Cursor = Cursors.WaitCursor;

            //  cParamAlgo ClassificationAlgo = WindowForClassificationParam.GetSelectedAlgoAndParameters();
            cListValuesParam Parameters = ClassifAlgoParams.GetListValuesParam();

            //Classifier this.CurrentClassifier = null;

            // -------------------------- Classification -------------------------------
            // create the instances
            // InstancesList = this.ListInstances;
            this.attValsWithoutClasses = new FastVector();

            if (IsCellular)
                for (int i = 0; i < cGlobalInfo.ListCellularPhenotypes.Count; i++)
                    this.attValsWithoutClasses.addElement(cGlobalInfo.ListCellularPhenotypes[i].Name);
            else
                for (int i = 0; i < cGlobalInfo.ListWellClasses.Count; i++)
                    this.attValsWithoutClasses.addElement(cGlobalInfo.ListWellClasses[i].Name);


            InstancesList.insertAttributeAt(new weka.core.Attribute("Class", this.attValsWithoutClasses), InstancesList.numAttributes());
            //int A = Classes.Count;
            for (int i = 0; i < Classes.Count; i++)
                InstancesList.get(i).setValue(InstancesList.numAttributes() - 1, Classes[i]);

            InstancesList.setClassIndex(InstancesList.numAttributes() - 1);

            weka.core.Instances train = new weka.core.Instances(InstancesList, 0, InstancesList.numInstances());

            if (PanelForVisualFeedback != null)
                PanelForVisualFeedback.Controls.Clear();

            #region List classifiers

            #region J48
            if (ClassifAlgoParams.Name == "J48")
            {
                this.CurrentClassifier = new weka.classifiers.trees.J48();
                ((J48)this.CurrentClassifier).setMinNumObj((int)Parameters.ListDoubleValues.Get("numericUpDownMinInstLeaf").Value);
                ((J48)this.CurrentClassifier).setConfidenceFactor((float)Parameters.ListDoubleValues.Get("numericUpDownConfFactor").Value);
                ((J48)this.CurrentClassifier).setNumFolds((int)Parameters.ListDoubleValues.Get("numericUpDownNumFolds").Value);
                ((J48)this.CurrentClassifier).setUnpruned((bool)Parameters.ListCheckValues.Get("checkBoxUnPruned").Value);
                ((J48)this.CurrentClassifier).setUseLaplace((bool)Parameters.ListCheckValues.Get("checkBoxLaplacianSmoothing").Value);
                ((J48)this.CurrentClassifier).setSeed((int)Parameters.ListDoubleValues.Get("numericUpDownSeedNumber").Value);
                ((J48)this.CurrentClassifier).setSubtreeRaising((bool)Parameters.ListCheckValues.Get("checkBoxSubTreeRaising").Value);

                //   CurrentClassif.SetJ48Tree((J48)this.CurrentClassifier, Classes.Length);
                this.CurrentClassifier.buildClassifier(train);
                // display results training
                // display tree
                if (PanelForVisualFeedback != null)
                {
                    GViewer GraphView = DisplayTree(GlobalInfo, ((J48)this.CurrentClassifier), IsCellular).gViewerForTreeClassif;
                    GraphView.Size = new System.Drawing.Size(PanelForVisualFeedback.Width, PanelForVisualFeedback.Height);
                    GraphView.Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                    PanelForVisualFeedback.Controls.Clear();
                    PanelForVisualFeedback.Controls.Add(GraphView);
                }
            }
            #endregion
            #region Random Tree
            else if (ClassifAlgoParams.Name == "RandomTree")
            {
                this.CurrentClassifier = new weka.classifiers.trees.RandomTree();

                if ((bool)Parameters.ListCheckValues.Get("checkBoxMaxDepthUnlimited").Value)
                    ((RandomTree)this.CurrentClassifier).setMaxDepth(0);
                else
                    ((RandomTree)this.CurrentClassifier).setMaxDepth((int)Parameters.ListDoubleValues.Get("numericUpDownMaxDepth").Value);
                ((RandomTree)this.CurrentClassifier).setSeed((int)Parameters.ListDoubleValues.Get("numericUpDownSeed").Value);
                ((RandomTree)this.CurrentClassifier).setMinNum((double)Parameters.ListDoubleValues.Get("numericUpDownMinWeight").Value);

                if ((bool)Parameters.ListCheckValues.Get("checkBoxIsBackfitting").Value)
                {
                    ((RandomTree)this.CurrentClassifier).setNumFolds((int)Parameters.ListDoubleValues.Get("numericUpDownBackFittingFolds").Value);
                }
                else
                {
                    ((RandomTree)this.CurrentClassifier).setNumFolds(0);
                }
                this.CurrentClassifier.buildClassifier(train);
                //string StringForTree = ((RandomTree)this.CurrentClassifier).graph().Remove(0, ((RandomTree)this.CurrentClassifier).graph().IndexOf("{") + 2);

                //Microsoft.Msagl.GraphViewerGdi.GViewer GraphView = new GViewer();
                //GraphView.Graph = GlobalInfo.WindowHCSAnalyzer.ComputeAndDisplayGraph(StringForTree);//.Remove(StringForTree.Length - 3, 3));

                //GraphView.Size = new System.Drawing.Size(panelForGraphicalResults.Width, panelForGraphicalResults.Height);
                //GraphView.Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                //this.panelForGraphicalResults.Controls.Clear();
                //this.panelForGraphicalResults.Controls.Add(GraphView);


            }
            #endregion
            #region Random Forest
            else if (ClassifAlgoParams.Name == "RandomForest")
            {
                this.CurrentClassifier = new weka.classifiers.trees.RandomForest();

                if ((bool)Parameters.ListCheckValues.Get("checkBoxMaxDepthUnlimited").Value)
                    ((RandomForest)this.CurrentClassifier).setMaxDepth(0);
                else
                    ((RandomForest)this.CurrentClassifier).setMaxDepth((int)Parameters.ListDoubleValues.Get("numericUpDownMaxDepth").Value);

                ((RandomForest)this.CurrentClassifier).setNumTrees((int)Parameters.ListDoubleValues.Get("numericUpDownNumTrees").Value);
                ((RandomForest)this.CurrentClassifier).setSeed((int)Parameters.ListDoubleValues.Get("numericUpDownSeed").Value);

                this.CurrentClassifier.buildClassifier(train);
            }
            #endregion
            #region KStar
            else if (ClassifAlgoParams.Name == "KStar")
            {
                this.CurrentClassifier = new weka.classifiers.lazy.KStar();
                ((KStar)this.CurrentClassifier).setGlobalBlend((int)Parameters.ListDoubleValues.Get("numericUpDownGlobalBlend").Value);
                ((KStar)this.CurrentClassifier).setEntropicAutoBlend((bool)Parameters.ListCheckValues.Get("checkBoxBlendAuto").Value);
                this.CurrentClassifier.buildClassifier(train);
            }
            #endregion
            #region SVM
            else if (ClassifAlgoParams.Name == "SVM")
            {
                this.CurrentClassifier = new weka.classifiers.functions.SMO();
                ((SMO)this.CurrentClassifier).setC((double)Parameters.ListDoubleValues.Get("numericUpDownC").Value);
                ((SMO)this.CurrentClassifier).setKernel(WindowForClassificationParam.GeneratedKernel);
                ((SMO)this.CurrentClassifier).setRandomSeed((int)Parameters.ListDoubleValues.Get("numericUpDownSeed").Value);
                this.CurrentClassifier.buildClassifier(train);
            }
            #endregion
            #region KNN
            else if (ClassifAlgoParams.Name == "KNN")
            {
                this.CurrentClassifier = new weka.classifiers.lazy.IBk();

                string OptionDistance = " -K " + (int)Parameters.ListDoubleValues.Get("numericUpDownKNN").Value + " -W 0 ";

                string WeightType = (string)Parameters.ListTextValues.Get("comboBoxDistanceWeight").Value;
                switch (WeightType)
                {
                    case "No Weighting":
                        OptionDistance += "";
                        break;
                    case "1/Distance":
                        OptionDistance += "-I";
                        break;
                    case "1-Distance":
                        OptionDistance += "-F";
                        break;
                    default:
                        break;
                }
                OptionDistance += " -A \"weka.core.neighboursearch.LinearNNSearch -A \\\"weka.core.";

                string DistanceType = (string)Parameters.ListTextValues.Get("comboBoxDistance").Value;
                // OptionDistance += " -A \"weka.core.";
                switch (DistanceType)
                {
                    case "Euclidean":
                        OptionDistance += "EuclideanDistance";
                        break;
                    case "Manhattan":
                        OptionDistance += "ManhattanDistance";
                        break;
                    case "Chebyshev":
                        OptionDistance += "ChebyshevDistance";
                        break;
                    default:
                        break;
                }

                if (!(bool)Parameters.ListCheckValues.Get("checkBoxNormalize").Value)
                    OptionDistance += " -D";
                OptionDistance += " -R ";

                OptionDistance += "first-last\\\"\"";
                ((IBk)this.CurrentClassifier).setOptions(weka.core.Utils.splitOptions(OptionDistance));

                //((IBk)this.CurrentClassifier).setKNN((int)Parameters.ListDoubleValues.Get("numericUpDownKNN").Value);
                this.CurrentClassifier.buildClassifier(train);
            }
            #endregion
            #region Multilayer Perceptron
            else if (ClassifAlgoParams.Name == "Perceptron")
            {
                this.CurrentClassifier = new weka.classifiers.functions.MultilayerPerceptron();
                ((MultilayerPerceptron)this.CurrentClassifier).setMomentum((double)Parameters.ListDoubleValues.Get("numericUpDownMomentum").Value);
                ((MultilayerPerceptron)this.CurrentClassifier).setLearningRate((double)Parameters.ListDoubleValues.Get("numericUpDownLearningRate").Value);
                ((MultilayerPerceptron)this.CurrentClassifier).setSeed((int)Parameters.ListDoubleValues.Get("numericUpDownSeed").Value);
                ((MultilayerPerceptron)this.CurrentClassifier).setTrainingTime((int)Parameters.ListDoubleValues.Get("numericUpDownTrainingTime").Value);
                ((MultilayerPerceptron)this.CurrentClassifier).setNormalizeAttributes((bool)Parameters.ListCheckValues.Get("checkBoxNormAttribute").Value);
                ((MultilayerPerceptron)this.CurrentClassifier).setNormalizeNumericClass((bool)Parameters.ListCheckValues.Get("checkBoxNormNumericClasses").Value);
                this.CurrentClassifier.buildClassifier(train);
            }
            #endregion
            #region ZeroR
            else if (ClassifAlgoParams.Name == "ZeroR")
            {
                this.CurrentClassifier = new weka.classifiers.rules.OneR();
                this.CurrentClassifier.buildClassifier(train);
            }
            #endregion
            #region OneR
            else if (ClassifAlgoParams.Name == "OneR")
            {
                this.CurrentClassifier = new weka.classifiers.rules.OneR();
                ((OneR)this.CurrentClassifier).setMinBucketSize((int)Parameters.ListDoubleValues.Get("numericUpDownMinBucketSize").Value);
                this.CurrentClassifier.buildClassifier(train);
            }
            #endregion
            #region Naive Bayes
            else if (ClassifAlgoParams.Name == "NaiveBayes")
            {
                this.CurrentClassifier = new weka.classifiers.bayes.NaiveBayes();
                ((NaiveBayes)this.CurrentClassifier).setUseKernelEstimator((bool)Parameters.ListCheckValues.Get("checkBoxKernelEstimator").Value);
                this.CurrentClassifier.buildClassifier(train);
            }
            #endregion
            #region Logistic
            else if (ClassifAlgoParams.Name == "Logistic")
            {
                this.CurrentClassifier = new weka.classifiers.functions.Logistic();
                ((Logistic)this.CurrentClassifier).setUseConjugateGradientDescent((bool)Parameters.ListCheckValues.Get("checkBoxUseConjugateGradientDescent").Value);
                ((Logistic)this.CurrentClassifier).setRidge((double)Parameters.ListDoubleValues.Get("numericUpDownRidge").Value);
                this.CurrentClassifier.buildClassifier(train);
            }
            #endregion
            //weka.classifiers.functions.SMO
            //BayesNet


            #endregion

            if (TextBoxForFeedback != null)
            {
                TextBoxForFeedback.Clear();
                TextBoxForFeedback.AppendText(this.CurrentClassifier.ToString());
            }

            TextBoxForFeedback.AppendText("\n" + (InstancesList.numAttributes() - 1) + " attributes:\n\n");
            for (int IdxAttributes = 0; IdxAttributes < InstancesList.numAttributes() - 1; IdxAttributes++)
            {
                TextBoxForFeedback.AppendText(IdxAttributes + "\t: " + InstancesList.attribute(IdxAttributes).name() + "\n");
            }

            #region evaluation of the model and results display

            if ((WindowForClassificationParam.numericUpDownFoldNumber.Enabled) && (TextBoxForFeedback != null))
            {

                TextBoxForFeedback.AppendText("\n-----------------------------\nModel validation\n-----------------------------\n");
                ModelEvaluation = new weka.classifiers.Evaluation(InstancesList);
                ModelEvaluation.crossValidateModel(this.CurrentClassifier, InstancesList, (int)WindowForClassificationParam.numericUpDownFoldNumber.Value, new java.util.Random(1));
                TextBoxForFeedback.AppendText(ModelEvaluation.toSummaryString());
                TextBoxForFeedback.AppendText("\n-----------------------------\nConfusion Matrix:\n-----------------------------\n");
                double[][] ConfusionMatrix = ModelEvaluation.confusionMatrix();

                string NewLine = "";
                for (int i = 0; i < ConfusionMatrix[0].Length; i++)
                {
                    NewLine += "c" + i + "\t";
                }
                TextBoxForFeedback.AppendText(NewLine + "\n\n");

                for (int j = 0; j < ConfusionMatrix.Length; j++)
                {
                    NewLine = "";
                    for (int i = 0; i < ConfusionMatrix[0].Length; i++)
                    {
                        NewLine += ConfusionMatrix[j][i] + "\t";
                    }
                    // if
                    TextBoxForFeedback.AppendText(NewLine + "| c" + j + " <=> " + cGlobalInfo.ListCellularPhenotypes[j].Name + "\n");
                }
            }
            #endregion

            return this.CurrentClassifier;
        }

        public void PerformClassification()
        {
            FormForSingleCellClassifOptions FFSC = new FormForSingleCellClassifOptions();
            //cGUI_ListClasses GLC = new cGUI_ListClasses();
            //GLC.ClassType = eClassType.PHENOTYPE;
            //GLC.IsCheckBoxes = true;
            //GLC.IsSelectAll = true;
            //GLC.Run(GlobalInfo);

            PanelForClassSelection PhenotypeSelectionPanel = new PanelForClassSelection( true, eClassType.PHENOTYPE);
            PhenotypeSelectionPanel.Height = FFSC.panelPhenoToBeClassified.Height;
            FFSC.panelPhenoToBeClassified.Controls.Add(PhenotypeSelectionPanel);

            PanelForClassSelection WellClassSelectionPanel = new PanelForClassSelection( true, eClassType.WELL);
            WellClassSelectionPanel.Height = FFSC.panelWellToBeClassified.Height;
            FFSC.panelWellToBeClassified.Controls.Add(WellClassSelectionPanel);


            PanelForPlatesSelection PlatesSelectionPanel = new PanelForPlatesSelection( true, null, true);
            PlatesSelectionPanel.Height = FFSC.panelWellToBeClassified.Height;
            FFSC.tabPagePlates.Controls.Add(PlatesSelectionPanel);

            if (FFSC.ShowDialog() != DialogResult.OK) return;

            // ----------------------- Classification ------------------------------
            int DescrCount = cGlobalInfo.CurrentScreening.ListDescriptors.Count;

            this.UpDateNumberOfCluster();
            if (NumberOfClusters == 0)
            {
                System.Windows.Forms.MessageBox.Show("Number of cluster is null", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            //if (FFSC.checkBoxGenerationRatio.Checked)
            //{
            //    // first we update the descriptor
            //    for (int i = 0; i < this.NumberOfClusters; i++)
            //        GlobalInfo.CurrentScreening.ListDescriptors.AddNew(new cDescriptorType("Ratio_" + GlobalInfo.ListCellularPhenotypes[i].Name, true, 1, GlobalInfo));
            //}

            FormForProgress ProgressWindow = new FormForProgress();
            ProgressWindow.Show();

            int IdxProgress = 0;
            int MaxProgress = 0;

            #region Confusion Matrix init
            cListExtendedTable LT = new cListExtendedTable();
            cExtendedTable ConfusionMatrix = new cExtendedTable(cGlobalInfo.ListCellularPhenotypes.Count, cGlobalInfo.ListCellularPhenotypes.Count, 0);
            ConfusionMatrix.Name = "Confusion Matrix (global)";
            ConfusionMatrix.ListRowNames = new List<string>();
            for (int i = 0; i < cGlobalInfo.ListCellularPhenotypes.Count; i++)
            {
                ConfusionMatrix.ListRowNames.Add(cGlobalInfo.ListCellularPhenotypes[i].Name + "*");
                ConfusionMatrix[i].Name = cGlobalInfo.ListCellularPhenotypes[i].Name;
            }
            LT.Add(ConfusionMatrix);
            for (int i = 0; i < cGlobalInfo.ListWellClasses.Count; i++)
            {
                cExtendedTable ConfusionMatrixTmp = new cExtendedTable(cGlobalInfo.ListCellularPhenotypes.Count, cGlobalInfo.ListCellularPhenotypes.Count, 0);
                ConfusionMatrixTmp.Name = "Confusion Matrix - " + cGlobalInfo.ListWellClasses[i].Name;
                ConfusionMatrixTmp.ListRowNames = new List<string>();
                for (int j = 0; j < cGlobalInfo.ListCellularPhenotypes.Count; j++)
                {
                    ConfusionMatrixTmp.ListRowNames.Add(cGlobalInfo.ListCellularPhenotypes[j].Name + "*");
                    ConfusionMatrixTmp[j].Name =cGlobalInfo.ListCellularPhenotypes[j].Name;
                }
                LT.Add(ConfusionMatrixTmp);
            }
            #endregion

            cListPlates LP = PlatesSelectionPanel.GetListSelectedPlates();

            foreach (cPlate CurrentPlateToProcess in LP /*GlobalInfo.CurrentScreening.ListPlatesAvailable*/)
                MaxProgress += (int)CurrentPlateToProcess.ListActiveWells.Count;
            ProgressWindow.progressBar.Maximum = MaxProgress;

            FastVector attVals = new FastVector();
            for (int i = 0; i < this.NumberOfClusters; i++)
                attVals.addElement(i.ToString());

            cPlate CurrentDispPlate = cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate();

            foreach (cPlate CurrentPlateToProcess in LP/* GlobalInfo.CurrentScreening.ListPlatesAvailable*/)
            {
                foreach (cWell TmpWell in CurrentPlateToProcess.ListActiveWells)
                {
                    ProgressWindow.progressBar.Value = IdxProgress++;

                    if (TmpWell.GetCurrentClassIdx() == -1) continue;
                    if (WellClassSelectionPanel.ListCheckBoxes[TmpWell.GetCurrentClassIdx()].Checked == false) continue;

                    DataTable FinalDataTable = new DataTable();
                    TmpWell.AssociatedPlate.DBConnection = new cDBConnection(TmpWell.AssociatedPlate, TmpWell.SQLTableName);
                    TmpWell.AssociatedPlate.DBConnection.AddWellToDataTable(TmpWell, FinalDataTable, this.GlobalInfo);
                    cListSingleBiologicalObjects LSBO = TmpWell.AssociatedPlate.DBConnection.GetBiologicalPhenotypes(TmpWell);
                    //TmpWell.AssociatedPlate.DBConnection.AddWellToDataTable(TmpWell, FinalDataTable, checkBoxIncludeWellClassAsDesc.Checked, GlobalInfo);
                    Instances ListInstancesTOClassify = this.CreateInstancesWithoutClass(FinalDataTable);

                    ListInstancesTOClassify.insertAttributeAt(new weka.core.Attribute("Class", attVals), ListInstancesTOClassify.numAttributes());
                    ListInstancesTOClassify.setClassIndex(ListInstancesTOClassify.numAttributes() - 1);

                    cExtendedList ListNewClasses = new cExtendedList();

                    int NumInstances = ListInstancesTOClassify.numInstances();
                    for (int i = 0; i < NumInstances; i++)
                    {
                        // ClassId contains the new class
                        Instance CurrentInst = ListInstancesTOClassify.instance(i);

                        double classId = this.CurrentClassifier.classifyInstance(CurrentInst);
                        double[] ClassConfidence = this.CurrentClassifier.distributionForInstance(CurrentInst);
                        LSBO[i].ClassificationConfidence = ClassConfidence[(int)classId];
                        ListNewClasses.Add(classId);

                        if (CurrentPlateToProcess == CurrentDispPlate)
                        {
                            LT[0][LSBO[i].GetAssociatedPhenotype().Idx][(int)classId]++;

                            if (TmpWell.GetCurrentClassIdx() >= 0)
                                LT[TmpWell.GetCurrentClassIdx() + 1][LSBO[i].GetAssociatedPhenotype().Idx][(int)classId]++;
                        }
                    }

                    ProgressWindow.richTextBoxForComment.AppendText(TmpWell.GetShortInfo().Remove(TmpWell.GetShortInfo().Length - 2) + " : " + NumInstances + " objects\n");
                    ProgressWindow.Refresh();
                    // ------------- update class within the database -----------------------------
                    TmpWell.AssociatedPlate.DBConnection.ChangePhenotypeClass(TmpWell, ListNewClasses);

                    //if (FFSC.checkBoxGenerationRatio.Checked)
                    //{
                    //    List<double[]> Histo = ListNewClasses.CreateHistogram(0, ListInstancesTOClassify.numClasses(), ListInstancesTOClassify.numClasses());
                    //    cListSignature LDesc = new cListSignature();

                    //    for (int IdxHisto = 0; IdxHisto < Histo[1].Length - 1; IdxHisto++)
                    //    {
                    //        Histo[1][IdxHisto] = (100.0 * Histo[1][IdxHisto]) / (double)ListInstancesTOClassify.numInstances();

                    //        cSignature NewDesc = new cSignature(Histo[1][IdxHisto], GlobalInfo.CurrentScreening.ListDescriptors[IdxHisto + DescrCount], GlobalInfo.CurrentScreening);
                    //        LDesc.Add(NewDesc);
                    //    }
                    //    TmpWell.AddSignatures(LDesc);
                    //}

                    TmpWell.AssociatedPlate.DBConnection.CloseConnection();
                }
            }

            #region Display Report
            cDesignerSplitter DS = new cDesignerSplitter();
            DS.Orientation = Orientation.Vertical;

            cViewertext VTEXT = new cViewertext();
            VTEXT.SetInputData(ProgressWindow.richTextBoxForComment.Text);
            VTEXT.Run();

            cDesignerTab DT = new cDesignerTab();
            DT.IsMultiline = false;

            foreach (var item in LT)
            {
                cViewerTable VT = new cViewerTable();
                VT.SetInputData(item);
                VT.DigitNumber = 0;
                VT.Run();
                DT.SetInputData(VT.GetOutPut());
            }

            DT.Run();

            cExtendedControl TextEC = DT.GetOutPut();
            TextEC.Width = 0;
            TextEC.Height = 0;
            TextEC.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                                                | System.Windows.Forms.AnchorStyles.Left
                                                                | System.Windows.Forms.AnchorStyles.Right);

            DS.SetInputData(VTEXT.GetOutPut());
            DS.SetInputData(TextEC);

            DS.Run();

            ProgressWindow.Close();

            cDisplayToWindow CDT = new cDisplayToWindow();
            CDT.SetInputData(DS.GetOutPut());
            CDT.IsModal = true;
            CDT.Title = "Phenotypic Classificaton Report";
            CDT.Run();
            CDT.Display();

            #endregion

            //if (IsKeepOriginalDesc == System.Windows.Forms.DialogResult.No)
            //{
            //    // int DescNumToRemove = GlobalInfo.CurrentScreen.ListDescriptors.Count - 
            //    for (int IdxDesc = 0; IdxDesc < DescrCount; IdxDesc++)
            //        GlobalInfo.CurrentScreening.ListDescriptors.RemoveDesc(GlobalInfo.CurrentScreening.ListDescriptors[0], GlobalInfo.CurrentScreening);
            //}

            cGlobalInfo.CurrentScreening.ListDescriptors.UpDateDisplay();
            cGlobalInfo.CurrentScreening.UpDatePlateListWithFullAvailablePlate();

            for (int idxP = 0; idxP < cGlobalInfo.CurrentScreening.ListPlatesActive.Count; idxP++)
                cGlobalInfo.CurrentScreening.ListPlatesActive[idxP].UpDataMinMax();

            //WindowFormForCellbyCellClassif.Close();
            //WindowClusteringInfo.Close();

        }

        private void ClickToDisplayHierarchicalTree(object sender, EventArgs e)
        {
            cDendoGram Dendogram = new cDendoGram(((HierarchicalClusterer)SelectedClusterer),null, null);

            FormDendogram WindowForDendoGram = new FormDendogram(GlobalInfo);
            WindowForDendoGram.CurrentDendo = Dendogram;
            WindowForDendoGram.Show();
        }

        public FormForClassificationTree DisplayTree(cGlobalInfo GlobalInfo, J48 J48Model, bool IsCellular)
        {
            FormForClassificationTree WindowForTree = new FormForClassificationTree();
            string StringForTree = J48Model.graph().Remove(0, J48Model.graph().IndexOf("{") + 2);
            WindowForTree.gViewerForTreeClassif.Graph = cGlobalInfo.WindowHCSAnalyzer.ComputeAndDisplayGraph(StringForTree.Remove(StringForTree.Length - 3, 3), IsCellular);
            return WindowForTree;
        }

        public Instances CreateInstancesWithoutClass(DataTable dt)
        {
            weka.core.FastVector atts = new FastVector();
            int columnNo = 0;

            // Descriptors loop
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                //if (ParentScreening.ListDescriptors[i].IsActive() == false) continue;
                atts.addElement(new weka.core.Attribute(dt.Columns[i].ColumnName));
                columnNo++;
            }
            // weka.core.FastVector attVals = new FastVector();
            Instances data1 = new Instances("MyRelation", atts, 0);

            for (int IdxRow = 0; IdxRow < dt.Rows.Count; IdxRow++)
            {
                double[] vals = new double[data1.numAttributes()];
                for (int Col = 0; Col < columnNo; Col++)
                {
                    // if (Glo .ListDescriptors[Col].IsActive() == false) continue;
                    vals[Col] = double.Parse(dt.Rows[IdxRow][Col].ToString());
                }
                data1.add(new DenseInstance(1.0, vals));
            }

            return data1;
        }



        public Instances CreateInstancesWithoutClass(cExtendedTable Input)
        {
            weka.core.FastVector atts = new FastVector();
            int columnNo = 0;

            // Descriptors loop
            for (int i = 0; i < Input.Count; i++)
            {
                //if (ParentScreening.ListDescriptors[i].IsActive() == false) continue;
                atts.addElement(new weka.core.Attribute(Input[i].Name));
                columnNo++;
            }
            // weka.core.FastVector attVals = new FastVector();
            Instances data1 = new Instances("MyRelation", atts, 0);

            for (int IdxRow = 0; IdxRow < Input[0].Count; IdxRow++)
            {
                double[] vals = new double[data1.numAttributes()];
                for (int Col = 0; Col < columnNo; Col++)
                {
                    // if (Glo .ListDescriptors[Col].IsActive() == false) continue;
                    vals[Col] = Input[Col][IdxRow];// double.Parse(dt.Rows[IdxRow][Col].ToString());
                }
                data1.add(new DenseInstance(1.0, vals));
            }

            return data1;
        }

    }




}
