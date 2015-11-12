using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{
    public partial class PanelForParamKNN : UserControl
    {
        public PanelForParamKNN()
        {
            InitializeComponent();
            this.comboBoxDistanceWeight.SelectedText = "No Weighting";
            this.comboBoxDistance.SelectedText = "Euclidean";
        }
    }
}
