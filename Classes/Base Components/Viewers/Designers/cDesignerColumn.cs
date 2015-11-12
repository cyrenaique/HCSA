using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Windows.Forms;
using HCSAnalyzer.Classes.Base_Classes.Viewers;

namespace HCSAnalyzer.Classes.Base_Classes
    {
    class cDesignerColumn : cDesignerParent
    {

        public Orientation Orientation = Orientation.Horizontal;
        List<cExtendedControl> xListControl = new List<cExtendedControl>();

        public cDesignerColumn()
        {
            this.Title = "Column Designer";
        }

        public void SetInputData(cExtendedControl ControlToAdd)
        {
            this.xListControl.Add(ControlToAdd);
        }

        public cFeedBackMessage Run()
        {
            

            if (xListControl.Count == 0)
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message += ": No input defined!";
                return base.FeedBackMessage;
            }

            this.OutPut = new cExtendedControl();

            TableLayoutPanel NewPanel = new TableLayoutPanel();

            //NewPanel.columns
            //NewPanel.ColumnStyles = new TableLayoutColumnStyleCollection(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F);
            
            NewPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            NewPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                 NewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                            | System.Windows.Forms.AnchorStyles.Left
                            | System.Windows.Forms.AnchorStyles.Right);


                 for (int IDx = 0; IDx < this.xListControl.Count; IDx++)
                 {
                     NewPanel.Controls.Add(this.xListControl[IDx], IDx, 0);
                 }




            //     Panel NewPanel = new Panel();
            //     NewPanel.Location = new System.Drawing.Point(0, 0);
            //     NewPanel.Width = 100;
            //     NewPanel.Height = 100 * this.xListControl.Count;
            //     NewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            //                | System.Windows.Forms.AnchorStyles.Left
            //                | System.Windows.Forms.AnchorStyles.Right);

            //     //cExtendedControl Tmp1 = this.CreatePanel(xListControl[0], xListControl[1]);

            //     for (int IDx = 0; IDx < this.xListControl.Count; IDx++)
            //     {
            //         this.xListControl[IDx].Anchor = AnchorStyles.None;
            //         Panel TmpPanel = new Panel();
            //         TmpPanel.Location = new System.Drawing.Point(0, IDx * 100);
            //         TmpPanel.Width = 100;
            //         TmpPanel.Height = 100;
            //         TmpPanel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
            //| System.Windows.Forms.AnchorStyles.Left
            //| System.Windows.Forms.AnchorStyles.Right);

            //         TmpPanel.BorderStyle = BorderStyle.FixedSingle;
            //         TmpPanel.Controls.Add(this.xListControl[IDx]);
            //         TmpPanel.Dock = (DockStyle)( DockStyle.Top | DockStyle.Bottom);

            //         //this.xListControl[IDx].Width = 100;
            //         //this.xListControl[IDx].Height = 100;
            //         //this.xListControl[IDx].Location = new System.Drawing.Point(0, 0);
            //         this.xListControl[IDx].Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            //                | System.Windows.Forms.AnchorStyles.Left
            //                | System.Windows.Forms.AnchorStyles.Right);
            //         NewPanel.Controls.Add(TmpPanel);

            //         //Tmp1 = this.CreatePanel(Tmp1, xListControl[IDx]);
            //     }
            //     NewPanel.AutoScroll = true;
            this.OutPut.Controls.Add(NewPanel);
            return base.FeedBackMessage;
        }

    }

}
