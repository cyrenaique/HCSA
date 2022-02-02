using System;
using System.Windows.Forms;

namespace HCSAnalyzer.Forms.FormsForOptions.ClassificationInfo
{
    public partial class PanelForParamSVM : UserControl
    {
        FormForClassificationInfo ClassifInfoParent;

        public PanelForParamSVM(FormForClassificationInfo ClassifInfoParent)
        {
            InitializeComponent();
            this.ClassifInfoParent = ClassifInfoParent;
            textBoxForKernelType.Text = ClassifInfoParent.GeneratedKernel.toString();
        }

        private void buttonEditKernel_Click(object sender, EventArgs e)
        {
            FormForKernelEditor WindowForKernelEditor = new FormForKernelEditor();
            if (WindowForKernelEditor.ShowDialog() != DialogResult.OK) return;
            this.ClassifInfoParent.GeneratedKernel = WindowForKernelEditor.GeneratedKernel;
            textBoxForKernelType.Text = WindowForKernelEditor.GeneratedKernel.toString();



        }

    }
}
