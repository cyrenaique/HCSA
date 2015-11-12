using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibPlateAnalysis;
using System.Windows.Forms;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes.Viewers._1D;
using System.Windows.Forms.DataVisualization.Charting;

namespace HCSAnalyzer.Classes.General_Types
{
    public class cListPlates : List<cPlate>
    {
        //cGlobalInfo GlobalInfo;
        public object Sender = null;
        cListWells ListActiveWells;

        public cListWells GetListActiveWells()
        {
            BuidListWells();
            return this.ListActiveWells;
      
        }



        public cPlate FindPlate(cPlate Plate)
        {
            foreach (var item in this)
                if (item == Plate) return item;

            return null;
        }

        void BuidListWells()
        {
            this.ListActiveWells = new cListWells(null);

            foreach (cPlate item in this)
            {
                this.ListActiveWells.AddRange(item.ListActiveWells);
            }
        }

        public cListPlates(object Sender)
        {
            this.Sender = Sender;
        }

        public cListPlates()
        {
        }

        //void ToolStripMenuItem_GetSingleCellTable(object sender, EventArgs e)
        //{
        //    cGetListPhenotypes GLP = new cGetListPhenotypes();
        //    GLP.SetInputData(this);
        //    GLP.Run();
        //    cExtendedTable CompleteTable = GLP.GetOutPut();

        //    CompleteTable.Name = this.Count + " Wells <=> " + CompleteTable[0].Count + " objects";
        //    cViewer2DScatterPoint VS = new cViewer2DScatterPoint();
        //    VS.SetInputData(CompleteTable);
        //    // VS.Chart.IsSelectable = true;
        //    VS.Run();

        //    cDisplayToWindow DTW = new cDisplayToWindow();
        //    DTW.SetInputData(VS.GetOutPut());
        //    DTW.Title = "2D Scatter Points. " + this.Count + " Wells <=> " + CompleteTable[0].Count + " objects";
        //    DTW.Run();
        //    DTW.Display();

        //    //  this.AssociatedPlate.DBConnection.DisplayTable(this);
        //}

        //void ToolStripMenuItem_GetTable(object sender, EventArgs e)
        //{
        //    cExtendedTable DT = GetDescriptorValuesFull();
        //    cDisplayExtendedTable DET = new cDisplayExtendedTable();
        //    DET.SetInputData(DT);
        //    DET.Run();
        //}

        //public cExtendedTable GetDescriptorValuesFull()
        //{
        //    cExtendedTable ToBeReturned = new cExtendedTable();
        //    ToBeReturned.Name = this.Count + " wells associated data table";
        //    ToBeReturned.ListRowNames = new List<string>();
        //    this.GlobalInfo = this[0].Parent.GlobalInfo;

        //    foreach (var item in this[0].Parent.ListDescriptors)
        //    {
        //        if (item.IsActive())
        //        {
        //            ToBeReturned.Add(new cExtendedList(item.GetName()));
        //            ToBeReturned[ToBeReturned.Count - 1].Tag = item;
        //            ToBeReturned[ToBeReturned.Count - 1].ListTags = new List<object>();
        //        }
        //    }
        //    ToBeReturned.Add(new cExtendedList("Class"));
        //    ToBeReturned[ToBeReturned.Count - 1].ListTags = new List<object>();

        //    foreach (var item in this)
        //    {
        //        cExtendedList CEL = item.GetAverageValuesList(true);
        //        int IdxDesc = 0;
        //        foreach (var Desc in CEL)
        //        {
        //            ToBeReturned[IdxDesc].Add(CEL[IdxDesc]);
        //            ToBeReturned[IdxDesc++].ListTags.Add(item);
        //        }
        //        ToBeReturned[ToBeReturned.Count - 1].Add(item.GetClassIdx());
        //        ToBeReturned[ToBeReturned.Count - 1].ListTags.Add(item.GetClassType());
        //        ToBeReturned.ListRowNames.Add(item.GetShortInfo());
        //    }
        //    return ToBeReturned;
        //}

        //public cExtendedTable GetDescriptorValues(int IDxDesc)
        //{
        //    if (this.Count == 0) return null;
        //    if (IDxDesc >= this[0].ListSignatures.Count) return null;

        //    cExtendedTable ToBeReturned = new cExtendedTable();
        //    this.GlobalInfo = this[0].Parent.GlobalInfo;

        //    foreach (var item incGlobalInfo.ListWellClasses)
        //    {
        //        ToBeReturned.Add(new cExtendedList(item.Name));
        //        ToBeReturned[ToBeReturned.Count - 1].Tag = item;
        //        ToBeReturned[ToBeReturned.Count - 1].ListTags = new List<object>();
        //    }

        //    foreach (cWell TmpWell in this)
        //    {
        //        ToBeReturned[TmpWell.GetClassIdx()].Add(TmpWell.ListSignatures[IDxDesc].GetValue());
        //        ToBeReturned[TmpWell.GetClassIdx()].ListTags.Add(TmpWell);
        //    }
        //    return ToBeReturned;
        //}

        public cExtendedTable GetDescriptorValues(List<cDescriptorType> ListDesc, bool IsConcentration)
        {
            if (this.Count == 0) return null;

            this.BuidListWells();


            cExtendedTable ToBeReturned = new cExtendedTable();
            //this.GlobalInfo = this[0].ParentScreening.GlobalInfo;

            //foreach (var item incGlobalInfo.ListWellClasses)
            //{
            //    ToBeReturned.Add(new cExtendedList(item.Name));
            //    ToBeReturned[ToBeReturned.Count - 1].Tag = item;
            //    ToBeReturned[ToBeReturned.Count - 1].ListTags = new List<object>();
            //}


            if (IsConcentration)
            {
                ToBeReturned.Add(new cExtendedList());
                ToBeReturned[ToBeReturned.Count - 1].Name = "Concentration";
                ToBeReturned[ToBeReturned.Count - 1].ListTags = new List<object>();
            }
            cExtendedList ListIdxDescriptor = new cExtendedList();

            foreach (var item in ListDesc)
            {
                ToBeReturned.Add(new cExtendedList());
                ToBeReturned[ToBeReturned.Count - 1].Name = item.GetName();
                ToBeReturned[ToBeReturned.Count - 1].Tag = item;
                ToBeReturned[ToBeReturned.Count - 1].ListTags = new List<object>();
                ListIdxDescriptor.Add(cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(item));
            }



            ToBeReturned.ListTags = new List<object>();

            int IdxDesc = 0;
            foreach (cWell TmpWell in this.ListActiveWells)
            {
                IdxDesc = 0;
                cExtendedList CL = TmpWell.GetAverageValuesList(ListIdxDescriptor);

                if (IsConcentration)
                {
                    ToBeReturned[IdxDesc].Add((double)TmpWell.ListProperties.FindValueByName("Concentration"));
                    ToBeReturned[IdxDesc++].ListTags.Add(TmpWell);
                }

                foreach (var item in CL)
                {
                    ToBeReturned[IdxDesc].Add(item);
                    ToBeReturned[IdxDesc++].ListTags.Add(TmpWell);
                }


                ToBeReturned.ListTags.Add(TmpWell);


                //ToBeReturned[TmpWell.GetClassIdx()]
                //   ToBeReturned[TmpWell.GetClassIdx()].Add(TmpWell.ListDescriptors[IDxDesc].GetValue());
                //  ToBeReturned[TmpWell.GetClassIdx()].ListTags.Add(TmpWell);
            }
            return ToBeReturned;

        }

        //public cExtendedTable GetListClasses()
        //{
        //    cExtendedTable ToReturn = new cExtendedTable();
        //    ToReturn.Name = "List classes (" + this.Count + " wells)";

        //    ToReturn.ListRowNames = new List<string>();
        //    cExtendedList L = new cExtendedList("Classes");
        //    L.ListTags = new List<object>();

        //    ToReturn.Add(L);

        //    foreach (var item in this)
        //    {
        //        int IdxClass = item.GetClassIdx();
        //        if (IdxClass < 0) continue;
        //        L.ListTags.Add(item);
        //        ToReturn.ListRowNames.Add(item.Name);
        //        L.Add(item.GetClassIdx());
        //    }
        //    return ToReturn;

        //}


        //#region Context Menu
        //public ToolStripMenuItem GetContextMenu()
        //{
        
        //}
        //    if (this.Count == 0) return null;

        //    ToolStripMenuItem SpecificContextMenu = new ToolStripMenuItem("List " + this.Count + " wells");
        //    // ToolStripSeparator Sep = new ToolStripSeparator();
        //    // base.SpecificContextMenu.Items.Add(Sep);


        //    //ToolStripMenuItem ToolStripMenuItem_Info = new ToolStripMenuItem("Test Automated Menu");

        //    //base.SpecificContextMenu.Items.Add(ToolStripMenuItem_Info);

        //    ////   contextMenuStrip.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem_Info, ToolStripMenuItem_Histo, ToolStripSep, ToolStripMenuItem_Kegg, ToolStripSep1, ToolStripMenuItem_Copy });

        //    ////ToolStripSeparator SepratorStrip = new ToolStripSeparator();
        //    //// contextMenuStrip.Show(Control.MousePosition);
        //    //ToolStripMenuItem_Info.Click += new System.EventHandler(this.DisplayInfo);


        //    ToolStripMenuItem ToolStripMenuItem_ChangeClass = new ToolStripMenuItem("Classes");
        //    //ToolStripMenuItem_CopyClassToClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_CopyClassToClipBoard);
        //    SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ChangeClass);


        //    for (int i = 0; i < this[0].cGlobalInfo.ListWellClasses.Count; i++)
        //    {
        //        ToolStripMenuItem ToolStripMenuItem_NewClass = new ToolStripMenuItem(this[0].cGlobalInfo.ListWellClasses[i].Name);
        //        ToolStripMenuItem_NewClass.Click += new System.EventHandler(this.ToolStripMenuItem_NewClass);
        //        ToolStripMenuItem_NewClass.Tag = i;// this[0].cGlobalInfo.ListWellClasses[i];
        //        ToolStripMenuItem_ChangeClass.DropDownItems.Add(ToolStripMenuItem_NewClass);
        //    }

        //    ToolStripMenuItem ToolStripMenuItem_GetTable = new ToolStripMenuItem("Get Associated Data Table");
        //    ToolStripMenuItem_GetTable.Click += new System.EventHandler(this.ToolStripMenuItem_GetTable);
        //    // ToolStripMenuItem_NewClass.Tag = i;// this[0].cGlobalInfo.ListWellClasses[i];
        //    SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_GetTable);

        //    ToolStripMenuItem ToolStripMenuItem_GetSingleCellTable = new ToolStripMenuItem("Display Single Cell Scatter Points");
        //    ToolStripMenuItem_GetSingleCellTable.Click += new System.EventHandler(this.ToolStripMenuItem_GetSingleCellTable);
        //    //// ToolStripMenuItem_NewClass.Tag = i;// this[0].cGlobalInfo.ListWellClasses[i];
        //    SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_GetSingleCellTable);


        //    SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

        //    ToolStripMenuItem ToolStripMenuItem_ComputeDisplayDRC = new ToolStripMenuItem("DRC Analysis");
        //    ToolStripMenuItem_ComputeDisplayDRC.Click += new System.EventHandler(this.ToolStripMenuItem_ComputeDisplayDRC);
        //    //// ToolStripMenuItem_NewClass.Tag = i;// this[0].cGlobalInfo.ListWellClasses[i];
        //    SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_ComputeDisplayDRC);



        //    //ToolStripMenuItem ToolStripMenuItem_CopyValuestoClipBoard = new ToolStripMenuItem("Copy values to clipboard");
        //    //ToolStripMenuItem_CopyValuestoClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_CopyValuestoClipBoard);
        //    //base.SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_CopyValuestoClipBoard);

        //    return SpecificContextMenu;

        //}

        //private void ToolStripMenuItem_NewClass(object sender, EventArgs e)
        //{
        //    //CopyValuestoClipBoard();
        //    foreach (var item in this)
        //    {
        //        int Classe = 0;
        //        int ResultClasse = -1;
        //        foreach (var Class in item.cGlobalInfo.ListWellClasses)
        //        {
        //            if (Class.Name == sender.ToString())
        //            {
        //                ResultClasse = Classe;
        //                break;
        //            }

        //            Classe++;
        //        }

        //        item.SetClass(ResultClasse);
        //    }


        //    if ((this.Sender != null) && (this.Sender.GetType() == typeof(cChart2DScatterPoint)))
        //    {
        //        ((cChart2DScatterPoint)(this.Sender)).RefreshDisplay();
        //    }

        //}
        //#endregion

        public cPlate GetPlate(string PlateName)
        {
            for (int Idx = 0; Idx < this.Count; Idx++)
            {
                if (PlateName == this[Idx].GetName())
                    return this[Idx];
            }
            return null;
        }

        public cPlate GetPlate(int Idx)
        {
            if (Idx < 0) return null;
            if (this.Count == 0) return null;
            if (Idx >= this.Count) return null;
            return this[Idx];
        }


        public cExtendedTable GetMinMax(cDescriptorType DescriptorType)
        {
            cExtendedList MinMax = new cExtendedList();
            MinMax.Name = "Min-Max";


            List<cDescriptorType> LDescType = new List<cDescriptorType>();
            LDescType.Add(DescriptorType);


            cExtendedTable ListValues = this.GetDescriptorValues(LDescType, false);

            //cExtendedTable ListValues = this.GetAverageValueList(DescriptorType, false);

            MinMax.Add(ListValues.Min());
            MinMax.Add(ListValues.Max());

            return new cExtendedTable(MinMax);
        }

    }
}
