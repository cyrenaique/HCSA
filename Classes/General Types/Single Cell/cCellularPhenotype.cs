using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibPlateAnalysis;
using System.Drawing;
using System.Windows.Forms;

namespace HCSAnalyzer.Classes.General_Types
{
    public class cCellularPhenotype : cObjectWithClass
    {
      
        public string Name;
      
        
        public cCellularPhenotype(Color Colour, int Idx)
        {
            this.ColourForDisplay = Colour;
            this.Name = "Phenotype " + Idx;
            base.Idx = Idx;
        }

        public ToolStripMenuItem GetExtendedContextMenu()
        {
            #region Context Menu
            base.SpecificContextMenu = new ToolStripMenuItem(this.Name);

            //ToolStripMenuItem ToolStripMenuItem_DisplayDataTable = new ToolStripMenuItem("Display Data Table");
            //ToolStripMenuItem_DisplayDataTable.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayDataTable);
            //base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayDataTable);

            //ToolStripMenuItem ToolStripMenuItem_DisplayHistograms = new ToolStripMenuItem("Display Histograms");
            //ToolStripMenuItem_DisplayHistograms.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayHistograms);
            //base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayHistograms);

            //ToolStripSeparator ToolStripSep = new ToolStripSeparator();
            //base.SpecificContextMenu.DropDownItems.Add(ToolStripSep);

            //ToolStripMenuItem ToolStripMenuItem_SetAsActivePlate = new ToolStripMenuItem("Set as Active");
            //ToolStripMenuItem_SetAsActivePlate.Click += new System.EventHandler(this.ToolStripMenuItem_SetAsActivePlate);
            //base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_SetAsActivePlate);
            #endregion
            return base.SpecificContextMenu;
        }



    }

}
