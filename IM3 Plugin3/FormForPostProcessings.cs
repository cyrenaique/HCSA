using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IM3_Plugin3
{
    public partial class FormForPostProcessings : Form
    {
        public FormForPostProcessings()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void buttonChangeColorPositive_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() != DialogResult.OK) return;
            Color backgroundColor = colorDialog.Color;

            this.buttonChangeColorPositive.BackColor = backgroundColor;
            this.buttonChangeColorPositive.Refresh();
        }
    }
}
