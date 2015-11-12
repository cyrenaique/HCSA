using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Forms.FormsForImages;
using ImageAnalysis;
using HCSAnalyzer.Classes.MetaComponents;

namespace HCSAnalyzer.Classes.ImageAnalysis.FormsForImages
{
    public partial class FormForNavigator : Form
    {
        cImagePanel Sender;

        public FormForNavigator(cImagePanel Sender)
        {
            InitializeComponent();
            this.Sender = Sender;
            if (Sender.AssociatedImage.Depth == 0)
                this.numericUpDownZPos.Maximum = 0;
            else
                this.numericUpDownZPos.Maximum = Sender.AssociatedImage.Depth - 1;

            this.trackBarForZPos.Maximum = (int)this.numericUpDownZPos.Maximum;
        }

        private void trackBarForZPos_Scroll(object sender, EventArgs e)
        {
            numericUpDownZPos.Value = trackBarForZPos.Value;
        }

        private void numericUpDownZPos_ValueChanged(object sender, EventArgs e)
        {
            // if (numericUpDownZPos.Value < 0) numericUpDownZPos.Value = (decimal)0;
            trackBarForZPos.Value = (int)numericUpDownZPos.Value;
            Sender.ReDrawPic();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void exctractSliceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int CurrentZ = (int)this.numericUpDownZPos.Value;

            cDisplaySingleImage NewView = new cDisplaySingleImage();

            cImage NewImage = new cImage(this.Sender.AssociatedImage.Width, this.Sender.AssociatedImage.Height, 1, this.Sender.AssociatedImage.GetNumChannels());
            NewImage.Name = this.Sender.AssociatedImage.Name + " [Z=" + CurrentZ + "]";
            NewImage.Resolution = new _3D.cPoint3D(this.Sender.AssociatedImage.Resolution);

            for (int i = 0; i < this.Sender.AssociatedImage.GetNumChannels(); i++)
            {

                Array.Copy(this.Sender.AssociatedImage.SingleChannelImage[i].Data, CurrentZ * this.Sender.AssociatedImage.SliceSize,
                            NewImage.SingleChannelImage[i].Data, 0,
                            NewImage.ImageSize);
            }
            NewView.SetInputData(NewImage);
            NewView.Run();



        }


    }
}
