using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Forms.FormsForOptions.PanelForOptions;
using System.Windows.Forms;
using HCSAnalyzer.Forms.FormsForOptions.ClusteringInfo;
using HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Classes.Machine_Learning.ClassificationInfo;

namespace HCSAnalyzer.Forms.FormsForOptions.ClassForOptions.Children
{
    public class cListDoubleValues : List<cDoubleValue>
    {
        public cDoubleValue Get(string NameVariable)
        {
            foreach (cDoubleValue item in this)
            {
                if (item.Name == NameVariable) return item;
            }
            return null;
        }

    }

    public class cListCheckValues : List<cCheckValue>
    {
        public cCheckValue Get(string NameVariable)
        {
            foreach (cCheckValue item in this)
            {
                if (item.Name == NameVariable) return item;
            }
            return null;
        }


    }

    public class cListColorValue : List<cColorValue>
    {
        public cColorValue Get(string NameVariable)
        {
            foreach (cColorValue item in this)
            {
                if (item.Name == NameVariable) return item;
            }
            return null;
        }

    }

    public class cListTextValue : List<cTextValue>
    {
        public cTextValue Get(string NameVariable)
        {
            foreach (cTextValue item in this)
            {
                if (item.Name == NameVariable) return item;
            }
            return null;
        }

    }

    
    public class cListValuesParam
    {
        public cListDoubleValues ListDoubleValues = new cListDoubleValues();
        public cListCheckValues ListCheckValues = new cListCheckValues();
        public cListColorValue ListColorValues = new cListColorValue();
        public cListTextValue ListTextValues = new cListTextValue();
        
    }

    #region Parent Class
    [Serializable]
    public abstract class cParamAlgo
    {

        protected Panel PanelToDisplay;
        public string Name;


        public Panel GetPanel()
        {
            PanelToDisplay.Location = new System.Drawing.Point(0, 0);
            return PanelToDisplay;
        }

        public cParamAlgo(string Name)
        {
            this.Name = Name;
        }

        /// <summary>
        /// recursive loop to gather all the controls
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        private IEnumerable<Control> GetAllControls(Control container)
        {
            List<Control> controlList = new List<Control>();
            foreach (Control c in container.Controls)
            {
                controlList.AddRange(GetAllControls(c));
                controlList.Add(c);
            }
            return controlList;
        }

        public cListValuesParam GetListValuesParam()
        {
            cListValuesParam ListValuesParam = new cListValuesParam();
            IEnumerable<Control> ListAllCtrls = GetAllControls(this.PanelToDisplay);
                
            foreach (Control ctl in ListAllCtrls)
            {
                Type CtlType = ctl.GetType();

                if (CtlType == typeof(CheckBox))
                {
                    cCheckValue NewValue = new cCheckValue(((CheckBox)ctl).Checked, ctl.Name);
                    ListValuesParam.ListCheckValues.Add(NewValue);
                    ctl.Tag = NewValue;
                }
                else if (CtlType == typeof(RadioButton))
                {
                    cCheckValue NewValue = new cCheckValue(((RadioButton)ctl).Checked, ctl.Name);
                    ListValuesParam.ListCheckValues.Add(NewValue);
                    ctl.Tag = NewValue;
                }
                else if (CtlType == typeof(NumericUpDown))
                {
                    cDoubleValue NewValue = new cDoubleValue((double)((NumericUpDown)ctl).Value, ctl.Name);
                    ListValuesParam.ListDoubleValues.Add(NewValue);
                    ctl.Tag = NewValue;
                }
                else if (CtlType == typeof(TextBox))
                {
                    cTextValue NewValue = new cTextValue(((TextBox)ctl).Text, ctl.Name);
                    ListValuesParam.ListTextValues.Add(NewValue);
                    ctl.Tag = NewValue;
                }
                else if (CtlType == typeof(Panel))
                {
                    cColorValue NewValue = new cColorValue(((Panel)ctl).BackColor, ctl.Name);
                    ListValuesParam.ListColorValues.Add(NewValue);
                    ctl.Tag = NewValue;
                }
                else if (CtlType == typeof(ComboBox))
                {
                    cTextValue NewValue = new cTextValue(((ComboBox)ctl).Text, ctl.Name);
                    ListValuesParam.ListTextValues.Add(NewValue);
                    ctl.Tag = NewValue;
                }


            }
            return ListValuesParam;
        }
    }
    #endregion

    #region ------------------------------------ Specific classification ------------------------------------

    public class cListClassificationAlgo : List<cParamAlgo>
    {
        public cListClassificationAlgo(FormForClassificationInfo ForClassificationInfo)
        {
            this.Add(new cParamJ48("J48"));
            this.Add(new cParamRandomForest("RandomForest")); 
            this.Add(new cParamRandomTree("RandomTree"));
            this.Add(new cParamKStar("KStar"));
            this.Add(new cParamSVM("SVM", ForClassificationInfo));
            this.Add(new cParamKNN("KNN"));
            this.Add(new cParamPerceptron("Perceptron")); 
            this.Add(new cParamZeroR("ZeroR"));
            this.Add(new cParamOneR("OneR"));
            this.Add(new cParamNaiveBayes("NaiveBayes"));
            this.Add(new cParamLogistic("Logistic"));
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
    public class cParamJ48 : cParamAlgo
    {
        public cParamJ48(string Name)
            : base(Name)
        {
            PanelForParamJ48 PanelForOption = new PanelForParamJ48();
            this.PanelToDisplay = PanelForOption.panel;
        }
    }

    [Serializable]
    public class cParamRandomForest : cParamAlgo
    {
        public cParamRandomForest(string Name)
            : base(Name)
        {
            PanelForParamRandomForest PanelForOption = new PanelForParamRandomForest();
            this.PanelToDisplay = PanelForOption.panel;
        }
    }

    [Serializable]
    public class cParamRandomTree : cParamAlgo
    {
        public cParamRandomTree(string Name)
            : base(Name)
        {
            PanelForParamRandomTree PanelForOption = new PanelForParamRandomTree();
            this.PanelToDisplay = PanelForOption.panel;
        }
    }
    [Serializable]
    public class cParamKStar : cParamAlgo
    {
        public cParamKStar(string Name)
            : base(Name)
        {
            PanelForParamKStar PanelForOption = new PanelForParamKStar();
            this.PanelToDisplay = PanelForOption.panel;
        }
    }
    [Serializable]
    public class cParamKNN : cParamAlgo
    {
        public cParamKNN(string Name)
            : base(Name)
        {
            PanelForParamKNN PanelForOption = new PanelForParamKNN();
            this.PanelToDisplay = PanelForOption.panel;
        }
    }
    [Serializable]
    public class cParamSVM : cParamAlgo
    {
        public cParamSVM(string Name, FormForClassificationInfo ForClassificationInfo)
            : base(Name)
        {
            PanelForParamSVM PanelForOption = new PanelForParamSVM(ForClassificationInfo);
            this.PanelToDisplay = PanelForOption.panel;
        }
    }
    [Serializable]
    public class cParamPerceptron : cParamAlgo
    {
        public cParamPerceptron(string Name)
            : base(Name)
        {
            PanelForParamPerceptron PanelForOption = new PanelForParamPerceptron();
            this.PanelToDisplay = PanelForOption.panel;
        }
    }   
    [Serializable]
    public class cParamZeroR : cParamAlgo
    {
        public cParamZeroR(string Name)
            : base(Name)
        {
            PanelForParamZeroR PanelForOption = new PanelForParamZeroR();
            this.PanelToDisplay = PanelForOption.panel;
        }
    } 
    [Serializable]
    public class cParamOneR : cParamAlgo
    {
        public cParamOneR(string Name)
            : base(Name)
        {
            PanelForParamOneR PanelForOption = new PanelForParamOneR();
            this.PanelToDisplay = PanelForOption.panel;
        }
    }
    [Serializable]
    public class cParamNaiveBayes : cParamAlgo
    {
        public cParamNaiveBayes(string Name)
            : base(Name)
        {
            PanelForParamNaiveBayes PanelForOption = new PanelForParamNaiveBayes();
            this.PanelToDisplay = PanelForOption.panel;
        }
    }
    [Serializable]
    public class cParamLogistic: cParamAlgo
    {
        public cParamLogistic(string Name)
            : base(Name)
        {
            PanelForParamLogistic PanelForOption = new PanelForParamLogistic();
            this.PanelToDisplay = PanelForOption.panel;
        }
    }
    #endregion

    #region ------------------------------------ Specific Clustering ------------------------------------
    public class cListClusteringAlgo : List<cParamAlgo>
    {
        public cListClusteringAlgo(List<string> ListDescritpors)
        {
            this.Add(new cParamEM("EM"));
            this.Add(new cParamKMeans("K-Means"));
            this.Add(new cParamHierarchical("Hierarchical"));
            this.Add(new cParamFarthestFirst("FarthestFirst"));
            this.Add(new cParamCobWeb("CobWeb"));
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
    public class cParamEM : cParamAlgo
    {
        public cParamEM(string Name)
            : base(Name)
        {
            PanelForParamEM PanelForOption = new PanelForParamEM();
            this.PanelToDisplay = PanelForOption.panel;
        }
    }

    [Serializable]
    public class cParamKMeans : cParamAlgo
    {
        public cParamKMeans(string Name)
            : base(Name)
        {
            PanelForParamKMeans PanelForOption = new PanelForParamKMeans();
            this.PanelToDisplay = PanelForOption.panel;
        }
    }

    [Serializable]
    public class cParamHierarchical : cParamAlgo
    {
        public cParamHierarchical(string Name)
            : base(Name)
        {
            PanelForParamHierarchical PanelForOption = new PanelForParamHierarchical();
            this.PanelToDisplay = PanelForOption.panel;
        }
    }

    [Serializable]
    public class cParamFarthestFirst : cParamAlgo
    {
        public cParamFarthestFirst(string Name)
            : base(Name)
        {
            PanelForParamFarthestFirst PanelForOption = new PanelForParamFarthestFirst();
            this.PanelToDisplay = PanelForOption.panel;
        }
    }   
    
    [Serializable]
    public class cParamCobWeb : cParamAlgo
    {
        public cParamCobWeb(string Name)
            : base(Name)
        {
            PanelForParamCobWeb PanelForOption = new PanelForParamCobWeb();
            this.PanelToDisplay = PanelForOption.panel;
        }
    }  
    

#endregion

}
