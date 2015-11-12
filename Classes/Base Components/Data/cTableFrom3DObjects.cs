using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using System.Windows.Forms;
using System.Data;
using LibPlateAnalysis;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes.DataManip;
using HCSAnalyzer.Classes.Base_Classes.Viewers._2D;
using System.IO;
using HCSAnalyzer.Classes._3D;

namespace HCSAnalyzer.Classes.Base_Classes
{
    public class cTableFrom3DObjects : cComponent
    {
        cExtendedTable Output = null;


        cListGeometric3DObject ListObjects;


        public cTableFrom3DObjects()
        {
            Title = "3D Objects to DataTable";
        }

        public void SetInputData(cListGeometric3DObject ListObjects)
        {
            this.ListObjects = ListObjects;
        }

        public cExtendedTable GetOutPut()
        {
            return this.Output;
        }

        public cFeedBackMessage Run()
        {
            this.Output = new cExtendedTable();
            this.Output.Tag = this.ListObjects.Tag;
            this.Output.Name = "3D View Associated Data Table";
                if(this.ListObjects.Name!=null)
                    this.Output.Name += this.ListObjects.Name;
            this.Output.ListTags = new List<object>();
            this.Output.ListRowNames = new List<string>();

            this.Output.Add(new cExtendedList("X"));
            this.Output.Add(new cExtendedList("Y"));
            this.Output.Add(new cExtendedList("Z"));


            foreach (var item in this.ListObjects)
            {
                this.Output[0].Add(item.GetPosition().X);
                this.Output[1].Add(item.GetPosition().Y);
                this.Output[2].Add(item.GetPosition().Z);
                this.Output.ListTags.Add(item.Tag);
                this.Output.ListRowNames.Add(item.GetName());
            }

            return FeedBackMessage;
        }
    }
}
