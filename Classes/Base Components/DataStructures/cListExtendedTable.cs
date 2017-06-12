using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageAnalysis;

namespace HCSAnalyzer.Classes.Base_Classes.DataStructures
{
    public class cListExtendedTable : List<cExtendedTable>
    {
        public string Name = "New Table List";
        public object Tag = null;

        public cListExtendedTable()
        {
        
        }


        public double Min()
        {
            cExtendedList ListMins = new cExtendedList();
            foreach (var item in this)
            {
                ListMins.Add(item.Min());
            }
            return ListMins.Min();
        }

        public double Max()
        {
            cExtendedList ListMaxs = new cExtendedList();
            foreach (var item in this)
            {
                ListMaxs.Add(item.Max());
            }
            return ListMaxs.Min();
        }


        public double Min(int Col)
        {
            double Min = double.MaxValue;
            
            foreach (var item in this)
            {
                if (Col >= item.Count) continue;
                double TmpMin = item[Col].Min();
                if (TmpMin < Min) Min = TmpMin;
            }
            return Min;
        }

        public double Max(int Col)
        {
            double Max = double.MinValue;

            foreach (var item in this)
            {
                if (Col >= item.Count) continue;
                double TmpMax = item[Col].Max();
                if (TmpMax > Max) Max = TmpMax;
            }
            return Max;
        }



        public cListExtendedTable(cExtendedTable InputTable)
        {
            this.Add(InputTable);
        }

        public cListExtendedTable(cImage InputImage)
        {
          //  this.Clear();
            for (int i = 0; i < InputImage.GetNumChannels(); i++)
            {
                cExtendedTable ET = new cExtendedTable();
              
                for (int Col = 0; Col < InputImage.Width; Col++)
                {
                    //ET.Name = "Col. " + Col;            // update Column names
                    cExtendedList EL = new cExtendedList();

                    for (int Row = 0; Row < InputImage.Height; Row++)
                        EL.Add(InputImage.SingleChannelImage[i].Data[Col + Row * InputImage.Width]);

                    ET.Add(EL);
                    ET[Col].Name = "Col. " + Col;
                }
                ET.Name = InputImage.Name + " - Channel_" + i;
                // update row names
                ET.ListRowNames = new List<string>();
                for (int Row = 0; Row < InputImage.Height; Row++)
                    ET.ListRowNames.Add( "Row " + Row);

                ET.DataSource = InputImage;
                this.Add(ET);
            }

            this.Name = "Table ("+InputImage.Name+")";
        }

    }
}
