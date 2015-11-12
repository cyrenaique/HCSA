using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HCSAnalyzer.Classes.General_Types
{
    public class cListSingleBiologicalObjects : List<cSingleBiologicalObject>
    {
        public cGlobalInfo GlobalInfo;
        int NewClass;
       // public object Sender;

        public cListSingleBiologicalObjects()
        {
            //this.Sender = Sender;
        }

        #region Context Menu
        public ToolStripMenuItem GetContextMenu()
        {

            if (this.Count == 0) return null;

            ToolStripMenuItem SpecificContextMenu = new ToolStripMenuItem("List " + this.Count + " biological objects");
            // ToolStripSeparator Sep = new ToolStripSeparator();
            // base.SpecificContextMenu.Items.Add(Sep);


            //ToolStripMenuItem ToolStripMenuItem_Info = new ToolStripMenuItem("Test Automated Menu");

            //base.SpecificContextMenu.Items.Add(ToolStripMenuItem_Info);

            ////   contextMenuStrip.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem_Info, ToolStripMenuItem_Histo, ToolStripSep, ToolStripMenuItem_Kegg, ToolStripSep1, ToolStripMenuItem_Copy });

            ////ToolStripSeparator SepratorStrip = new ToolStripSeparator();
            //// contextMenuStrip.Show(Control.MousePosition);
            //ToolStripMenuItem_Info.Click += new System.EventHandler(this.DisplayInfo);


            ToolStripMenuItem ToolStripMenuItem_ChangeClass = new ToolStripMenuItem("Change Phenotype");
            //ToolStripMenuItem_CopyClassToClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_CopyClassToClipBoard);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChangeClass);


            for (int i = 0; i < cGlobalInfo.ListWellClasses.Count; i++)
            {
                ToolStripMenuItem ToolStripMenuItem_NewClass = new ToolStripMenuItem(cGlobalInfo.ListCellularPhenotypes[i].Name);
                ToolStripMenuItem_NewClass.Click += new System.EventHandler(this.ToolStripMenuItem_NewClass);
                ToolStripMenuItem_NewClass.Tag = i;// this[0].cGlobalInfo.ListWellClasses[i];
                ToolStripMenuItem_ChangeClass.DropDownItems.Add(ToolStripMenuItem_NewClass);
            }

            //ToolStripMenuItem ToolStripMenuItem_GetTable = new ToolStripMenuItem("Get Associated Data Table");
            //ToolStripMenuItem_GetTable.Click += new System.EventHandler(this.ToolStripMenuItem_GetTable);
            //// ToolStripMenuItem_NewClass.Tag = i;// this[0].cGlobalInfo.ListWellClasses[i];
            //SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_GetTable);

            //ToolStripMenuItem ToolStripMenuItem_GetSingleCellTable = new ToolStripMenuItem("Display Single Cell Scatter Points");
            //ToolStripMenuItem_GetSingleCellTable.Click += new System.EventHandler(this.ToolStripMenuItem_GetSingleCellTable);
            ////// ToolStripMenuItem_NewClass.Tag = i;// this[0].cGlobalInfo.ListWellClasses[i];
            //SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_GetSingleCellTable);

            //ToolStripMenuItem ToolStripMenuItem_CopyValuestoClipBoard = new ToolStripMenuItem("Copy values to clipboard");
            //ToolStripMenuItem_CopyValuestoClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_CopyValuestoClipBoard);
            //base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_CopyValuestoClipBoard);

            return SpecificContextMenu;

        }

        private void ToolStripMenuItem_NewClass(object sender, EventArgs e)
        {
            ////CopyValuestoClipBoard();
            //foreach (var item in this)
            //{
            //    int Classe = 0;
            //    int ResultClasse = -1;
            //    foreach (var Class in item.cGlobalInfo.ListWellClasses)
            //    {
            //        if (Class.Name == sender.ToString())
            //        {
            //            ResultClasse = Classe;
            //            break;
            //        }

            //        Classe++;
            //    }

            //    item.SetClass(ResultClasse);
            //}


            //if ((this.Sender != null) && (this.Sender.GetType() == typeof(cChart2DScatterPoint)))
            //{
            //    ((cChart2DScatterPoint)(this.Sender)).RefreshDisplay();
            //}

        }
        #endregion


    }
}
