﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace IM3_Plugin3
{
    public partial class FormForArrow : Form
    {
        public FormForArrow()
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
