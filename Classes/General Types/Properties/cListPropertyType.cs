using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibPlateAnalysis;
using System.Windows.Forms;

namespace HCSAnalyzer.Classes.General_Types.Screen
{
    public class cListPropertyType : List<cPropertyType>
    {
        protected cScreening AssociatedScreening;


        public cPropertyType FindByName(string Name)
        {
            foreach (var item in this)
                if (item.Name == Name) return item;

            return null;
        }


    }

    public class cListWellPropertyType : cListPropertyType
    {
        public cListWellPropertyType(cScreening AssociatedScreening)
        {
            base.AssociatedScreening = AssociatedScreening;
            
            cGlobalInfo.WindowHCSAnalyzer.toolStripDropDownButtonDisplayMode.DropDownItems.Clear();
        }

        public void AddNewType(cPropertyType WellPropertyType)
        {
            this.Add(WellPropertyType);
            foreach (cPlate TmpPlate in AssociatedScreening.ListPlatesAvailable)
                foreach (cWell TmpWell in TmpPlate.ListWells)
                {
                    TmpWell.ListProperties.Add(new cProperty(WellPropertyType, null));
                }

            ToolStripMenuItem NewItem = new ToolStripMenuItem(WellPropertyType.Name);
            NewItem.Tag = WellPropertyType;
            NewItem.CheckOnClick = true;
            NewItem.Checked = false;
            //NewItem.DropDownItemClicked += new ToolStripItemClickedEventHandler(NewItem_DropDownItemClicked);
            NewItem.CheckedChanged += new EventHandler(NewItem_CheckedChanged);

            cGlobalInfo.WindowHCSAnalyzer.toolStripDropDownButtonDisplayMode.DropDownItems.Insert(0,NewItem);
        }

        public void NewItem_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var item in cGlobalInfo.WindowHCSAnalyzer.toolStripDropDownButtonDisplayMode.DropDownItems)
            {
                if (item.GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem TmpMenuItem = ((ToolStripMenuItem)item);
                    TmpMenuItem.CheckedChanged -= new EventHandler(NewItem_CheckedChanged);
                    TmpMenuItem.Checked = false;
                    TmpMenuItem.CheckedChanged += new EventHandler(NewItem_CheckedChanged);
                }
            }
            ((ToolStripMenuItem)(sender)).CheckedChanged -= new EventHandler(NewItem_CheckedChanged);
            ((ToolStripMenuItem)(sender)).Checked = true;
            ((ToolStripMenuItem)(sender)).CheckedChanged += new EventHandler(NewItem_CheckedChanged);

            cGlobalInfo.CurrentScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptor(), false);
        }
    }

    public class cListPlatePropertyType : cListPropertyType
    {
        public cListPlatePropertyType(cScreening AssociatedScreening)
        {
            base.AssociatedScreening = AssociatedScreening;
        }

        public void AddNewType(cPropertyType PlatePropertyType)
        {
            this.Add(PlatePropertyType);
            foreach (cPlate TmpPlate in AssociatedScreening.ListPlatesAvailable)
            {
                TmpPlate.ListProperties.Add(new cProperty(PlatePropertyType, null));
            }
        }
    }
}
