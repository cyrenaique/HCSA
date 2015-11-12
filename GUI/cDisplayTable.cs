using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using HCSAnalyzer.Classes;


namespace HCSAnalyzer.Forms.FormsForGraphsDisplay
{
    class cDisplayTable : FormToDisplaySimpleTable
    {

        DataTable TableValues = null;

        public cDisplayTable(string Title, string[] ColumnNames, List<string[]> Values, cGlobalInfo GlobalInfo, bool IsMINE)
        {
            this.Text = Title;

            this.TableValues = new DataTable();
            this.dataGridViewForTable.Columns.Clear();

            foreach (string Name in ColumnNames)
            {
                this.TableValues.Columns.Add(new DataColumn(Name, typeof(string)));
            }
//this.TableValues.Columns.Add(new DataColumn("", typeof(string)));
            //double[] Mins = new double[IsDisplayColorMap.Count];
            //double[] Maxs = new double[IsDisplayColorMap.Count];

            //for (int idx = 0; idx < Mins.Length; idx++)
            //{
            //    Mins[idx] = double.MaxValue;
            //    Maxs[idx] = double.MinValue;
            //}


            
            for (int Row = 0; Row < Values.Count; Row++)
            {
                this.TableValues.Rows.Add();

                for (int idxString = 0; idxString < Values[Row].Length; idxString++)
                {
                    string CurrentString = Values[Row][idxString];
                    this.TableValues.Rows[Row][idxString] = CurrentString;
                }
            }

            this.dataGridViewForTable.DataSource = this.TableValues;
            this.Show();

            if (IsMINE)
            {
                cLUT LUT = new cLUT();
                for (int Row = 0; Row < Values.Count; Row++)
                {
                    for (int idxString = 0; idxString < Values[Row].Length; idxString++)
                    {
                        string CurrentString = Values[Row][idxString];

                        if ((idxString == 2) || (idxString == 7))
                        {
                            int ConvertedValue = (int)((LUT.LUT_GREEN_TO_RED[0].Length - 1) * Math.Abs(double.Parse(CurrentString)));

                            Color Coul = Color.FromArgb(LUT.LUT_GREEN_TO_RED[0][ConvertedValue], LUT.LUT_GREEN_TO_RED[1][ConvertedValue], LUT.LUT_GREEN_TO_RED[2][ConvertedValue]);

                            this.dataGridViewForTable.Rows[Row].Cells[idxString].Style.BackColor = Coul;
                            this.dataGridViewForTable.Rows[Row].Cells[idxString].Style.ForeColor = Color.White;
                        }
                    }
                }
            }
            
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // cDisplayTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(940, 796);
            this.Name = "cDisplayTable";
            this.ResumeLayout(false);

        }

     
    }
}
