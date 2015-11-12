using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows;
using System.Drawing;
using HCSAnalyzer.Forms.IO;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using HCSAnalyzer.Controls;
using HCSAnalyzer.Classes.Base_Classes.GUI;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.General_Types;

namespace LibPlateAnalysis
{
    //public class cListReplicate : List<cReplicateType>
    //{
    //    public string Name;



    //}



    //public class cReplicateType : cGeneralComponent
    //{
    //    public string Name{ get; private set; }

    //    public cReplicateType(string Name)
    //    {
    //        this.Name = Name;
    //    }

    //    public cListWells ListAssociatedWells(cScreening AssociatedScreening, bool IsOnlyActive)
    //    {
    //        cListWells ListWells = new cListWells(null);

    //        //foreach (cPlate TmpPlate in AssociatedScreening.ListPlatesActive)
    //        //{
    //        //    if (TmpPlate.AssociatedReplicateType == this)
    //        //    {
    //        //        foreach (cWell TmpWell in TmpPlate.ListActiveWells)
    //        //        {
    //        //            if (TmpWell == null) continue;
    //        //            ListWells.Add(TmpWell);
    //        //        }
    //        //    }
    //        //}

    //        return ListWells;
    //    }

    //    public string GetShortInfo()
    //    {
    //        base.ShortInfo = "Replicate Type: " + this.Name + "\n";
    //        return base.GetShortInfo();
    //    }

    //    public bool ChangeName(string NewName, cScreening CurrentScreening)
    //    {
    //        this.Name = NewName;
    //        return true;
    //    }

    //    public List<ToolStripMenuItem> GetExtendedContextMenu()
    //    {
    //        List<ToolStripMenuItem> ListToReturn = new List<ToolStripMenuItem>();

    //        base.SpecificContextMenu = new ToolStripMenuItem(this.Name);

    //        // info
    //        ToolStripMenuItem InfoDescItem = new ToolStripMenuItem("Info");
    //        // InfoDescItem.Click += new System.EventHandler(this.InfoDescItem);
    //        base.SpecificContextMenu.DropDownItems.Add(InfoDescItem);


    //        base.SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

    //        ListToReturn.Add(base.SpecificContextMenu);
    //        return ListToReturn;
    //    }

    //}


}
