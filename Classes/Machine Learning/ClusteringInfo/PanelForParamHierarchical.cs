using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HCSAnalyzer.Forms.FormsForOptions.ClusteringInfo
{
    public partial class PanelForParamHierarchical : UserControl
    {
        public PanelForParamHierarchical()
        {
            InitializeComponent();
            this.comboBoxDistance.SelectedText = "Euclidean";
            this.comboBoxLinkType.SelectedText = "COMPLETE";
        }
    }
}
