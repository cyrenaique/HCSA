using System.Windows.Forms;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    public partial class FormForDisplay : Form
    {
        public FormForDisplay()
        {
            InitializeComponent();

        }

        protected override void Dispose(bool disposing)
        {

            for (int i = 0; i < this.Controls.Count; i++)
            {
                this.Controls[i].Dispose();
            }


            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
