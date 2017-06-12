using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes.Machine_Learning;
using LibPlateAnalysis;
using HCSAnalyzer.Forms.FormsForOptions.ClassForOptions.Children;
using weka.clusterers;

namespace HCSAnalyzer.Classes.Base_Classes.DataProcessing
{
    class cClustering : cDataAnalysis
    {

        public cClustering()
        {
            base.Title = "Clustering";
            base.Info = base.Info = @"Multivariate Clustering";

            cPropertyType PT1 = new cPropertyType("Number of Clusters", eDataType.INTEGER);
            PT1.Min = -1;
            cProperty Prop1 = new cProperty(PT1, null);

            Prop1.Info = "Define the request number of clusters (-1 <=> automated)";
            Prop1.SetNewValue((int)3);
            base.ListProperties.Add(Prop1);
        }

        public cParamAlgo ParamAlgoForClustering = null;


        public cFeedBackMessage Run()
        {
            base.Start();
            if (!FeedBackMessage.IsSucceed) return FeedBackMessage;
            if (this.Input.Count > 1)
            {
                this.FeedBackMessage = new cFeedBackMessage(false, this);
                return this.FeedBackMessage;
            }


            #region parameters initialization
            //object _firstValue = base.ListProperties.FindByName("Number of Clusters");
            //int NumOfClusters = 0;
            //if (_firstValue == null)
            //{
            //    base.GenerateError("-Number of Clusters- not found !");
            //    return base.FeedBackMessage;
            //}
            //try
            //{
            //    cProperty TmpProp = (cProperty)_firstValue;
            //    NumOfClusters = (int)TmpProp.GetValue();
            //}
            //catch (Exception)
            //{
            //    base.GenerateError("-Number of Clusters- cast didn't work");
            //    return base.FeedBackMessage;
            //}
            #endregion

            cMachineLearning MachineLearning = new cMachineLearning(/*cWell.GlobalInfo*/null);
            
            if (ParamAlgoForClustering == null)
                ParamAlgoForClustering = MachineLearning.AskAndGetClusteringAlgo();
            
            if (ParamAlgoForClustering == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "Invalid Parameters";
                return FeedBackMessage;
            }

            MachineLearning.SelectedClusterer = MachineLearning.BuildClusterer(ParamAlgoForClustering, this.Input);

            if (MachineLearning.SelectedClusterer != null)
            {
                cLinearize L = new cLinearize();
                L.SetInputData(this.Input);
                L.Run();
                cExtendedTable Test = L.GetOutPut();


                ClusterEvaluation CE = MachineLearning.EvaluteAndDisplayClusterer(/*richTextBoxInfoClustering*/null,
                                                         null,
                                                         MachineLearning.CreateInstancesWithoutClass(this.Input));
                
                double[] Assign = CE.getClusterAssignments();

                base.Output = new cExtendedTable(this.Input);
                cExtendedList CL = new cExtendedList("Class ID");
                CL.SetInfo(CE.clusterResultsToString());
                base.Output.Add(CL);

                for (int i = 0; i < Assign.Length; i++)
                    base.Output[base.Output.Count-1].Add(Assign[i]);
            }


            base.End();
            return FeedBackMessage;
        }
    }
}
