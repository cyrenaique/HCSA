using System.Windows.Forms;

namespace IM3_Plugin3
{
    public partial class FormFor3DVolumeRendering : Form
    {

        FormForControl Parent;


        public FormFor3DVolumeRendering(FormForControl Parent)
        {
            InitializeComponent();

            this.Parent = Parent;
        }
    }
}
