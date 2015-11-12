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

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    public class cTableToClipBoard : cComponent
    {
        cExtendedTable Input = null;

        public string Separator = "\t";
       // public bool IsRTFFormat = false;

        public cTableToClipBoard()
        {
            Title = "Table to Clipboard";
        }

        public void SetInputData(cExtendedTable MyData)
        {
            this.Input = MyData;
        }

        public cFeedBackMessage Run()
        {
            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }


            //if (IsRTFFormat)
            //{
            //    //Since too much string appending go for string builder
            //    StringBuilder tableRtf = new StringBuilder();

            //    //beginning of rich text format,dont customize this begining line              
            //    tableRtf.Append(@"{\rtf1 ");

            //    //create 5 rows with 3 cells each

            //    for (int i = 0; i < this.Input.Count; i++)
            //    {

            //        tableRtf.Append(@"\trowd");

            //        for (int j = 0; j < this.Input[i].Count; j++)
            //        {


            //            //A cell with width 1000.
            //            tableRtf.Append(@"\cellx1000");

            //            //Another cell with width 2000.end point is 3000 (which is 1000+2000).
            //            //tableRtf.Append(@"\cellx3000");

            //            //Another cell with width 1000.end point is 4000 (which is 3000+1000)
            //            //tableRtf.Append(@"\cellx4000");
            //        }
            //        tableRtf.Append(@"\intbl \cell \row"); //create row

            //    }

            //    tableRtf.Append(@"\pard");

            //    tableRtf.Append(@"}");

            //    Clipboard.SetText(tableRtf.ToString());


            //}
            //else
            {

                // Export titles:  
                string TextToClip = "";

                if (this.Input.ListRowNames != null)
                    TextToClip += this.Separator;

                for (int j = 0; j < this.Input.Count; j++) { TextToClip = TextToClip.ToString() + this.Input[j].Name + Separator; }
                TextToClip = TextToClip.Remove(TextToClip.Length - 1);
                TextToClip += "\n";

                // Export data.  
                for (int i = 0; i < this.Input[0].Count; i++)
                {
                    if ((this.Input.ListRowNames != null)&&(this.Input.ListRowNames.Count>i))
                        TextToClip += this.Input.ListRowNames[i] + this.Separator;

                    for (int j = 0; j < this.Input.Count; j++) { TextToClip = TextToClip + this.Input[j][i] + Separator; }

                    TextToClip = TextToClip.Remove(TextToClip.Length - 1);
                    TextToClip += "\n";
                }
                TextToClip = TextToClip.Remove(TextToClip.Length - 1);
                Clipboard.SetText(TextToClip);
            }
            return FeedBackMessage;
        }
    }
}
