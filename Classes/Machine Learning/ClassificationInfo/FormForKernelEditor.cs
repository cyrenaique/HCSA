using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weka.classifiers.functions.supportVector;

namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{
    public partial class FormForKernelEditor : Form
    {
        public FormForKernelEditor()
        {
            InitializeComponent();
            GeneratedKernel = new PolyKernel();
            ((PolyKernel)GeneratedKernel).setExponent((double)this.numericUpDownPolyExponent.Value);
        }

        void updateUI()
        {
            groupBoxPolyKernel.Enabled = radioButtonPolyKernel.Checked;
            groupBoxPearson.Enabled = radioButtonPearson.Checked;
            groupBoxRBF.Enabled = radioButtonRBF.Checked;
        }

        private void radioButtonPolyKernel_CheckedChanged(object sender, EventArgs e)
        {
            updateUI();
        }

        private void radioButtonRBF_CheckedChanged(object sender, EventArgs e)
        {
            updateUI();
        }

        private void radioButtonPearson_CheckedChanged(object sender, EventArgs e)
        {
            updateUI();
        }

        public Kernel GeneratedKernel = null;

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (this.radioButtonRBF.Checked)
            {
                RBFKernel MyGeneratedKernel = new RBFKernel();
                ((RBFKernel)MyGeneratedKernel).setGamma((double)this.numericUpDownRBFGamma.Value);
                GeneratedKernel = (Kernel)(MyGeneratedKernel);
            }
            if (this.radioButtonPearson.Checked)
            {
                Puk MyGeneratedKernel = new Puk();
                ((Puk)MyGeneratedKernel).setSigma((double)this.numericUpDownPearsonSigma.Value);
                ((Puk)MyGeneratedKernel).setOmega((double)this.numericUpDownPearsonOmega.Value);
                GeneratedKernel = (Kernel)(MyGeneratedKernel);
            }
            if (this.radioButtonPolyKernel.Checked)
            {
                PolyKernel MyGeneratedKernel = new PolyKernel();
                MyGeneratedKernel.setExponent((double)this.numericUpDownPolyExponent.Value);
                GeneratedKernel = (Kernel)(MyGeneratedKernel);
            }

        }

    }
}
