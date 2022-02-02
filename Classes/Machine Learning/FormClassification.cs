using HCSAnalyzer.Classes;
using LibPlateAnalysis;
using System.Drawing;
using System.Windows.Forms;

namespace HCSAnalyzer
{

    public partial class FormClassification : Form
    {
        cScreening CurrentScreening;

        public FormClassification(cScreening CurrentScreening)
        {
            InitializeComponent();
            this.CurrentScreening = CurrentScreening;
        }

        private void comboBoxForNeutralClass_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            SolidBrush BrushForColor = new SolidBrush(cGlobalInfo.ListWellClasses[e.Index].ColourForDisplay);
            e.Graphics.FillRectangle(BrushForColor, e.Bounds.X + 1, e.Bounds.Y + 1, 10, 10);
            e.Graphics.DrawString(comboBoxForNeutralClass.Items[e.Index].ToString(), comboBoxForNeutralClass.Font,
                System.Drawing.Brushes.Black, new RectangleF(e.Bounds.X + 15, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            e.DrawFocusRectangle();
        }



    }
}
