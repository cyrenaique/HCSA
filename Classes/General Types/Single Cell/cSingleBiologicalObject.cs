using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibPlateAnalysis;
using System.Drawing;
using System.Windows.Forms;
using HCSAnalyzer.Classes._3D;
using ImageAnalysis;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.MetaComponents;

namespace HCSAnalyzer.Classes.General_Types
{
    public class cSingleBiologicalObject : cGeneralComponent
    {
        //public int Class = 0;
       // cGlobalInfo GlobalInfo;
        cCellularPhenotype CellularPhenotypeType;
        public cWell AssociatedWell;
        public cPoint3D Position = new cPoint3D(-1, -1, -1);
        public cPoint3D BD_BoxMin = new cPoint3D(-1, -1, 0);
        public cPoint3D BD_BoxMax = new cPoint3D(-1, -1, 0);

        public int ImageField =0;
        public double ClassificationConfidence = 1;
      //  static cImageAccessor ImageAccessor;

        public cSingleBiologicalObject(cCellularPhenotype CellularPhenotypeType, cWell AssociatedWell, int Idx)
        {
         //   this.GlobalInfo = GlobalInfo;
         //   this.Class = Class;
            this.CellularPhenotypeType = CellularPhenotypeType;
            this.AssociatedWell = AssociatedWell;

        }

        public Color GetColor()
        {
            return this.CellularPhenotypeType.ColourForDisplay;

        }

        public cCellularPhenotype GetAssociatedPhenotype()
        {
            return this.CellularPhenotypeType;
        }

        public List<ToolStripMenuItem> GetExtendedContextMenu()
        {
            List<ToolStripMenuItem> ListToReturn = new List<ToolStripMenuItem>();

            List<ToolStripMenuItem> WellMenu  = this.AssociatedWell.GetExtendedContextMenu();

            for (int IdxMenu = 0; IdxMenu < WellMenu.Count; IdxMenu++)
            {
                ListToReturn.Add(WellMenu[IdxMenu]);
            }


            #region Context Menu
            base.SpecificContextMenu = new ToolStripMenuItem("Object ["+this.CellularPhenotypeType.Name+"]");
            // ToolStripSeparator Sep = new ToolStripSeparator();
            // base.SpecificContextMenu.Items.Add(Sep);


            //ToolStripMenuItem ToolStripMenuItem_Info = new ToolStripMenuItem("Test Automated Menu");

            //base.SpecificContextMenu.Items.Add(ToolStripMenuItem_Info);

            ////   contextMenuStrip.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem_Info, ToolStripMenuItem_Histo, ToolStripSep, ToolStripMenuItem_Kegg, ToolStripSep1, ToolStripMenuItem_Copy });

            ////ToolStripSeparator SepratorStrip = new ToolStripSeparator();
            //// contextMenuStrip.Show(Control.MousePosition);
            //ToolStripMenuItem_Info.Click += new System.EventHandler(this.DisplayInfo);

            ToolStripMenuItem ToolStripMenuItem_DisplayImage = new ToolStripMenuItem("Display Image");
            ToolStripMenuItem_DisplayImage.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayImage);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayImage);

            SpecificContextMenu.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_Info = new ToolStripMenuItem("Info");
            ToolStripMenuItem_Info.Click += new System.EventHandler(this.DisplayInfo);
            SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Info);

            //ToolStripMenuItem ToolStripMenuItem_Histo = new ToolStripMenuItem("Histograms");
            //ToolStripMenuItem_Histo.Click += new System.EventHandler(this.DisplayHisto);
            //SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Histo);

            //if (this.LocusID != -1.0)
            //{
            //    ToolStripMenuItem ToolStripMenuItem_Kegg = new ToolStripMenuItem("Kegg");
            //    ToolStripMenuItem_Kegg.Click += new System.EventHandler(this.DisplayPathways);
            //    SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_Kegg);
            //}

            //if (this.SQLTableName != "")
            //{
            //    ToolStripSeparator ToolStripSep = new ToolStripSeparator();
            //    SpecificContextMenu.DropDownItems.Add(ToolStripSep);

            //    ToolStripMenuItem ToolStripMenuItem_DisplayData = new ToolStripMenuItem("Display Single Object Data");
            //    ToolStripMenuItem_DisplayData.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayData);
            //    SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_DisplayData);

            //    ToolStripMenuItem ToolStripMenuItem_AddToSingleCellAnalysis = new ToolStripMenuItem("Add to Single Object Analysis");
            //    ToolStripMenuItem_AddToSingleCellAnalysis.Click += new System.EventHandler(this.ToolStripMenuItem_AddToSingleCellAnalysis);
            //    SpecificContextMenu.DropDownItems.Add(ToolStripMenuItem_AddToSingleCellAnalysis);
            //}

            //if (this.GetClassIdx() >= 0)
            //    base.SpecificContextMenu.DropDownItems.Add(cGlobalInfo.ListWellClasses[this.GetClassIdx()].GetExtendedContextMenu());

            //ToolStripSeparator ToolStripSep1 = new ToolStripSeparator();
            //ToolStripMenuItem ToolStripMenuItem_Copy = new ToolStripMenuItem("Copy Visu.");

            //ToolStripSeparator SepratorStrip = new ToolStripSeparator();
            //base.SpecificContextMenu.Show(Control.MousePosition);
            //ToolStripMenuItem_Copy.Click += new System.EventHandler(this.CopyVisu);
            #endregion

            ListToReturn.Add(base.SpecificContextMenu);

            return ListToReturn;
        }
        //ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

        private void DisplayInfo(object sender, EventArgs e)
        {
            //DisplayInfoWindow(CurrentDescriptorToDisplay);

            cExtendedTable InfoTable = new cExtendedTable();
            //this.DisplayInfo(


        }

        private void ToolStripMenuItem_DisplayImage(object sender, EventArgs e)
        {
            List<cImageMetaInfo> ListInfo = cGlobalInfo.ImageAccessor.GetImageInfo(this);
            cImage Image = new cImage(ListInfo);
            this.BD_BoxMax.Z = this.BD_BoxMin.Z = 0;

           // this.BD_BoxMin.Y = Image.Height - this.BD_BoxMin.Y;
           // this.BD_BoxMax.Y = Image.Height - this.BD_BoxMax.Y;

            if (this.BD_BoxMin.Y > this.BD_BoxMax.Y)
            {
                double Tmp = this.BD_BoxMin.Y;
                this.BD_BoxMin.Y = this.BD_BoxMax.Y;
                this.BD_BoxMax.Y = Tmp;
            }

            cImage CroppedImaged = Image.Crop(this.BD_BoxMin, this.BD_BoxMax);
            cDisplaySingleImage IV = new cDisplaySingleImage();
            IV.IsUseSavedDefaultDisplayProperties = true;
            IV.DefaultZoom = 100;
            IV.Title = "[Plate] " + this.AssociatedWell.AssociatedPlate.GetName() + " - [Well] " + this.AssociatedWell.GetPos() + " - [Field] " + this.ImageField + " - " + this.CellularPhenotypeType.Name;
            IV.SetInputData(CroppedImaged);
            IV.Run();
        }

        public cPoint3D GetPosition()
        {
           cDescriptorType DescTypeForPosX = cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorByName(cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosX.Text);
           cDescriptorType DescTypeForPosY = cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorByName(cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosY.Text);

           if ((DescTypeForPosX == null) || (DescTypeForPosY == null))
           {
               this.Position = new cPoint3D(-1, -1, -1);
               return this.Position;
           }
           return this.Position;
        }
   
    }
}
