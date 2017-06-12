using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageAnalysis;
using System.Windows.Forms;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    class cViewertext : cDataDisplay
    {
        public cViewertext()
        {
            Title = "Rich Text Viewer";   
        }

        List<string> InputData = new List<string>();
        RichTextBox TextPanel = new RichTextBox();

        public void SetInputData(string Input)
        {
            this.InputData.Add(Input);
        }

        public cFeedBackMessage Run()
        {
            

            foreach (var item in this.InputData)
            {
                this.TextPanel.AppendText(item + "\n");
            }

            TextPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart_MouseDown);
  
            TextPanel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                        | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            TextPanel.Width = 0;// base.CurrentPanel.Width;
            TextPanel.Height = 0;// base.CurrentPanel.Height;

            base.CurrentPanel.Width = 0;
            base.CurrentPanel.Height = 0;

            base.CurrentPanel.Title = this.Title;
            base.CurrentPanel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                        | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);

            CurrentPanel.Controls.Add(this.TextPanel);

            return base.FeedBackMessage;
        }


        private void chart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                CompleteMenu = new ContextMenuStrip();

                ToolStripMenuItem ToolStripMenuItem_DisplayTable = new ToolStripMenuItem("Clear");
                CompleteMenu.Items.Add(ToolStripMenuItem_DisplayTable);
                ToolStripMenuItem_DisplayTable.Click += new System.EventHandler(this.ClearPanel);


                CompleteMenu.Show(Control.MousePosition);
            }
        }


        private void ClearPanel(object sender, EventArgs e)
        {
            this.TextPanel.Clear();
        }


    }
}
