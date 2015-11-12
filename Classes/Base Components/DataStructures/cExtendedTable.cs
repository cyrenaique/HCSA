using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibPlateAnalysis;
using System.Data;
using HCSAnalyzer.Classes.General_Types;
using ImageAnalysis;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using weka.core;
//using RDotNet;

namespace HCSAnalyzer.Classes.Base_Classes.DataStructures
{
    [Serializable]
    public class cExtendedTable : List<cExtendedList>
    {
        public string Name = "New Table";
        public List<string> ListRowNames = new List<string>();
        public List<object> ListTags;
        public object DataSource = null;
        public object Tag = null;

        public double[,] CopyToArray()
        {
            double[,] ToReturn = new double[this[0].Count, this.Count];
            for (int Row = 0; Row < this[0].Count; Row++)
            {
                for (int Col = 0; Col < this.Count; Col++)
                {
                    ToReturn[Row, Col] = this[Col][Row];
                }
            }
            return ToReturn;
        }

        public double[][] CopyToArray2()
        {
            double[][] ToReturn = new double[this.Count][];

            for (int i = 0; i < Count; i++)
            {
                ToReturn[i] = new double[this[i].Count];
            }

            for (int Col = 0; Col < this.Count; Col++)
            {
                for (int Row = 0; Row < this[Col].Count; Row++)
                {
                    ToReturn[Col][Row] = this[Col][Row];
                }
            }
            return ToReturn;
        }

        public weka.core.Matrix CopyToWEKAMatrix()
        {
            weka.core.Matrix MatrixToBeReturned = new weka.core.Matrix(this[0].Count, this.Count);

            for (int Col = 0; Col < this.Count; Col++)
                for (int Row = 0; Row < this[0].Count; Row++)
                    MatrixToBeReturned.addElement(Row, Col, this[Col][Row]);

            return MatrixToBeReturned;
        }
        
        /// <summary>
        ///  Create an instances structure with classes for WEKA supervised methods
        ///  Warning: the last column should contain the class index
        /// </summary>
        /// <returns>WEKA list of instance</returns>
        public Instances CreateWekaInstancesWithClasses()
        {
            weka.core.FastVector atts = new FastVector();

            int columnNo = 0;

            for (int i = 0; i < /*ParentScreening.ListPlateBaseddescriptorNames.Count*/ this.Count-1; i++)
            {
                atts.addElement(new weka.core.Attribute(/*ParentScreening.ListPlateBaseddescriptorNames[i]*/this[i].Name));
                columnNo++;
            }

            weka.core.FastVector attVals = new FastVector();
            
            int NumberOfClass = (int)this[this.Count - 1].Max()+1;
            for (int i = 0; i < NumberOfClass; i++)
                attVals.addElement("Class" + (i).ToString());

            atts.addElement(new weka.core.Attribute("Class", attVals));

            Instances data1 = new Instances("MyRelation", atts, 0);
            int IdxWell = 0;

            for (int IdxRow = 0; IdxRow < this[0].Count; IdxRow++)
            {
                
            //}
            //foreach (cWell CurrentWell in this[0].ListActiveWells)
            //{
                //if (CurrentWell.GetCurrentClassIdx() == -1) continue;
                double[] vals = new double[data1.numAttributes()];
                int IdxCol = 0;
                for (int Col = 0; Col < this.Count/* ParentScreening.ListPlateBaseddescriptorNames.Count*/; Col++)
                {
                    vals[IdxCol++] = this[Col][IdxRow];// CurrentWell.ListPlateBasedDescriptors[Col].GetValue();
                }
                vals[columnNo] = this[this.Count-1][IdxRow];// CurrentWell.GetCurrentClassIdx();
                data1.add(new DenseInstance(1.0, vals));
                IdxWell++;
            }
            data1.setClassIndex((data1.numAttributes() - 1));

            return data1;
        }

        public Instances CreateWekaInstances()
        {
            weka.core.FastVector atts = new FastVector();
            int columnNo = 0;

            for (int i = 0; i < this.Count; i++)
            {
                atts.addElement(new weka.core.Attribute(this[i].Name));
                columnNo++;
            }

            weka.core.FastVector attVals = new FastVector();

            //int NumberOfClass = (int)this[this.Count - 1].Max() + 1;
            //for (int i = 0; i < NumberOfClass; i++)
            //    attVals.addElement("Class" + (i).ToString());

            //atts.addElement(new weka.core.Attribute("Class", attVals));

            Instances data1 = new Instances("MyRelation", atts, 0);
            int IdxWell = 0;

            for (int IdxRow = 0; IdxRow < this[0].Count; IdxRow++)
            {

                double[] vals = new double[data1.numAttributes()];
                int IdxCol = 0;
                for (int Col = 0; Col < this.Count; Col++)
                {
                    vals[IdxCol++] = this[Col][IdxRow];
                }
               
                data1.add(new DenseInstance(1.0, vals));
                IdxWell++;
            }
          //  data1.setClassIndex((data1.numAttributes() - 1));

            return data1;
        }

        ///// <summary>
        ///// Create an instances structure with classes for supervised methods
        ///// </summary>
        ///// <param name="NumClass"></param>
        ///// <returns></returns>
        //public Instances CreateInstances(int NeutralClass, int IdxClass)
        //{
        //    weka.core.FastVector atts = new FastVector();
        //    int columnNo = 0;
        //    Instances data1 = null;

        //    if (IdxClass == -1)
        //    {
        //        // Descriptors loop
        //        for (int i = 0; i < this.Count; i++)
        //        {
        //            atts.addElement(new weka.core.Attribute(this[i].Name));
        //            columnNo++;
        //        }
        //        weka.core.FastVector attVals = new FastVector();
        //        data1 = new Instances("MyRelation", atts, 0);

        //        for (int i = 0; i < this[0].Count; i++)
        //        {
        //            double[] vals = new double[data1.numAttributes()];
        //            int IdxRealCol = 0;

        //            for (int Col = 0; Col < this.Count; Col++)
        //                vals[IdxRealCol++] = this[Col][i];

        //            data1.add(new DenseInstance(1.0, vals));
        //        }
        //    }
        //    else
        //    {

        //        for (int i = 0; i < this.Count; i++)
        //        {
        //            if (i == IdxClass) continue;
        //            atts.addElement(new weka.core.Attribute(this[i].Name));
        //            columnNo++;
        //        }

        //        weka.core.FastVector attVals = new FastVector();

        //        int NumberOfClass = (int)this[IdxClass].Max();

        //        for (int i = 0; i < NumberOfClass; i++)
        //            attVals.addElement("Class__" + (i).ToString());

        //        atts.addElement(new weka.core.Attribute("Class__", attVals));

        //        data1 = new Instances("MyRelation", atts, 0);
        //        int IdxWell = 0;
        //        for (int i = 0; i < this[0].Count; i++)
        //        {
        //            if ((int)this[IdxClass][i] == NeutralClass) continue;
        //            double[] vals = new double[data1.numAttributes()];

        //            int IdxCol = 0;
        //            for (int Col = 0; Col < this.Count; Col++)
        //            {
        //                if (Col == IdxClass) continue;
        //                vals[IdxCol++] = this[Col][i];

        //            }
        //            // vals[columnNo] = InfoClass.CorrespondanceTable[CurrentWell.GetCurrentClassIdx()];
        //            data1.add(new DenseInstance(1.0, vals));
        //            IdxWell++;
        //        }
        //        data1.setClassIndex((data1.numAttributes() - 1));
        //    }
        //    return data1;
        //}

        //public NumericMatrix CopyTo_R_StatMatrix()
        //{
        //    NumericMatrix ToReturn = cGlobalInfo.RStat_engine.CreateNumericMatrix(this.CopyToArray());
        //    cGlobalInfo.RStat_engine.SetSymbol(this.Name, ToReturn);

        //    return ToReturn;
        //}

        public cExtendedTable()
        {

        }

        public cExtendedTable(cExtendedTable Source)
        {
            this.Name = Source.Name + "_Copy";
            int IdxCol = 0;
            foreach (var item in Source)
            {

                cExtendedList NewCol = new cExtendedList();

                NewCol.Name = item.Name;
                NewCol.Tag = item.Tag;
                NewCol.AddRange(item);
                this.Add(NewCol);


                //if (item.Tag != null)
                //{
                //    this[IdxCol].Tag = new object();
                //    this[IdxCol].Tag = item.Tag;
                //}

                if (item.ListTags != null)
                {
                    this[IdxCol].ListTags = new List<object>();
                    this[IdxCol].ListTags.AddRange(item.ListTags);
                }

                IdxCol++;

            }
            if (Source.ListRowNames != null)
                this.ListRowNames.AddRange(Source.ListRowNames);

            if (Source.ListTags != null)
            {
                this.ListTags = new List<object>();
                this.ListTags.AddRange(Source.ListTags);
            }

        }

        public cExtendedTable(cExtendedList Source)
        {

            this.Name = Source.Name;
            cExtendedList NewCol = new cExtendedList();
            NewCol.Name = Source.Name;
            NewCol.AddRange(Source);
            this.Add(NewCol);

            if (Source.ListTags != null)
            {
                this[0].ListTags = new List<object>();
                this[0].ListTags.AddRange(Source.ListTags);

                this.ListTags = new List<object>();
                this.ListTags.AddRange(Source.ListTags);

            }

            //this.ListRowNames.AddRange(Source.ListRowNames);
        }

        public cExtendedTable(cImage InputImage, int Channel, int Slice)
        {
            for (int Col = 0; Col < InputImage.Width; Col++)
            {
                cExtendedList EL = new cExtendedList();

                for (int Row = 0; Row < InputImage.Height; Row++)
                    EL.Add(InputImage.SingleChannelImage[Channel].Data[Col + Row * InputImage.Width + Slice * InputImage.SliceSize]);

                this.Add(EL);
                this[Col].Name = "Col. " + Col;
            }

            this.ListRowNames = new List<string>();
            for (int Row = 0; Row < InputImage.Height; Row++)
                this.ListRowNames.Add("Row " + Row);

            this.DataSource = InputImage.SingleChannelImage[Channel];

            this.Name = "Table (" + InputImage.Name + ") Channel_" + Channel; ;
        }

        public cExtendedTable(cSingleChannelImage InputImage, int Slice)
        {
            for (int Col = 0; Col < InputImage.Width; Col++)
            {
                cExtendedList EL = new cExtendedList();

                for (int Row = 0; Row < InputImage.Height; Row++)
                    EL.Add(InputImage.Data[Col + Row * InputImage.Width + Slice * InputImage.Width * InputImage.Height]);

                this.Add(EL);
                this[Col].Name = "Col. " + Col;
            }

            this.ListRowNames = new List<string>();
            for (int Row = 0; Row < InputImage.Height; Row++)
                this.ListRowNames.Add("Row " + Row);

            this.DataSource = InputImage;

            this.Name = "Table (" + InputImage.Name + ")";
        }

        public void Add(cExtendedTable TableToBeAdded)
        {
            if ((TableToBeAdded == null) || (TableToBeAdded.Count == 0)) return;
            if ((TableToBeAdded.Count != this.Count) || (TableToBeAdded[0].Count != this[0].Count)) return;

            for (int i = 0; i < this.Count; i++)
            {
                for (int j = 0; j < this[i].Count; j++)
                {
                    this[i][j] += TableToBeAdded[i][j];
                }
            }
        }

        public cExtendedTable(int NumCol, int NumRow, double DefaultValue)
        {
            for (int Col = 0; Col < NumCol; Col++)
            {
                cExtendedList NewCol = new cExtendedList();
                for (int Row = 0; Row < NumRow; Row++)
                {
                    NewCol.Add(DefaultValue);
                }
                this.Add(NewCol);
            }
        }

        public cExtendedTable(double[,] Table)
        {
            int NumCol = Table.GetLength(0);
            int NumRow = Table.GetLength(1);

            for (int Col = 0; Col < NumCol; Col++)
            {
                cExtendedList NewCol = new cExtendedList();
                for (int Row = 0; Row < NumRow; Row++)
                {
                    NewCol.Add(Table[Col, Row]);
                }
                this.Add(NewCol);
            }
        }

        public cExtendedTable(double[][] Table)
        {
            int NumCol = Table.Length;
            int NumRow = Table[0].Length;

            for (int Col = 0; Col < NumCol; Col++)
            {
                cExtendedList NewCol = new cExtendedList();
                for (int Row = 0; Row < NumRow; Row++)
                {
                    NewCol.Add(Table[Col][Row]);
                }
                this.Add(NewCol);
            }
        }

        public cExtendedTable(weka.core.Matrix Table)
        {
            int NumRow = Table.numRows();
            int NumCol = Table.numColumns();

            for (int Col = 0; Col < NumCol; Col++)
            {
                cExtendedList NewCol = new cExtendedList();
                for (int Row = 0; Row < NumRow; Row++)
                {
                    NewCol.Add(Table.getElement(Row, Col));
                }
                this.Add(NewCol);
            }
        }

        public cExtendedTable(DataTable Table)
        {
            // int NumberOfPlates = CompleteScreening.ListPlatesActive.Count;
            //cExtendedTable ListValueDesc = new cExtendedTable();

            for (int i = 0; i < Table.Columns.Count; i++) this.Add(new cExtendedList());
            for (int i = 0; i < Table.Columns.Count; i++) this[i].Name = Table.Columns[i].ColumnName;

            // loop on all the plate
            for (int RowIdx = 0; RowIdx < Table.Rows.Count; RowIdx++)
            {
                for (int ColIdx = 0; ColIdx < Table.Columns.Count; ColIdx++)
                    this[ColIdx].Add((double)Table.Rows[RowIdx][ColIdx]);
            }
            //return ListValueDesc;
        }

        /// <summary>
        /// Build a table containing the well values
        /// </summary>
        /// <param name="ListWell">List of the wells</param>
        /// <param name="GlobalInfo">Required to take into account the selected descriptors</param>
        public cExtendedTable(cListWells ListWell, bool OnlySelectedDescriptors)
        {
            if (ListWell.Count == 0) return;

            foreach (var Desc in cGlobalInfo.CurrentScreening.ListDescriptors)
            {
                if (Desc.IsActive())
                {
                    cExtendedList NewList = new cExtendedList(Desc.GetName());

                    this.Add(NewList);
                    this[this.Count - 1].Tag = Desc;
                }
            }

            cExtendedList Values;
            int IdxWell = 0;

            this.ListTags = new List<object>();

            foreach (cWell CurrentWell in ListWell)
            {
                Values = CurrentWell.GetAverageValuesList(OnlySelectedDescriptors)[0];
                ListRowNames.Add(CurrentWell.GetShortInfo());

                for (int i = 0; i < Values.Count; i++)
                    this[i].Add(Values[i]);


                this.ListTags.Add(CurrentWell);
                IdxWell++;
            }


            // in this specific case, we can add the tags
            for (int i = 0; i < this.Count; i++)
            {
                this[i].ListTags = new List<object>();

                for (int j = 0; j < ListWell.Count; j++)
                    this[i].ListTags.Add(ListWell[j]);
            }
        }

        public cExtendedList GetRow(int Idx)
        {
            if (Idx >= this[0].Count) return null;

            cExtendedList ToReturn = new cExtendedList();
            for (int i = 0; i < this.Count; i++)
            {
                ToReturn.Add(this[i][Idx]);
            }

            ToReturn.Name = this.Name + "_Row_" + Idx;
            return ToReturn;
        }

        public cExtendedTable(cListWells ListWell, int IdxDesc)
        {
            if (ListWell.Count == 0) return;
            cExtendedList NewList = new cExtendedList(cGlobalInfo.CurrentScreening.ListDescriptors[IdxDesc].GetName());
            this.Add(NewList);


            this.ListTags = new List<object>();

            foreach (cWell CurrentWell in ListWell)
            {
                double Value = CurrentWell.ListSignatures[IdxDesc].GetValue();
                ListRowNames.Add(CurrentWell.GetShortInfo());
                this.ListTags.Add(CurrentWell);

                this[0].Add(Value);
            }

            // in this specific case, we can add the tags
            for (int i = 0; i < this.Count; i++)
            {
                this[i].ListTags = new List<object>();

                for (int j = 0; j < ListWell.Count; j++)
                    this[i].ListTags.Add(ListWell[j]);
            }
        }

        public cExtendedTable(cListWells ListWell, int IdxDesc, cExtendedList ListActiveClasses)
        {
            cExtendedList NewList = new cExtendedList(cGlobalInfo.CurrentScreening.ListDescriptors[IdxDesc].GetName());
            this.Add(NewList);

            this.ListTags = new List<object>();

            foreach (cWell CurrentWell in ListWell)
            {
                if ((CurrentWell.GetCurrentClassIdx() >= 0) && (ListActiveClasses[CurrentWell.GetCurrentClassIdx()] == 1))
                {
                    double Value = CurrentWell.ListSignatures[IdxDesc].GetValue();
                    ListRowNames.Add(CurrentWell.GetShortInfo());
                    this.ListTags.Add(CurrentWell);
                    this[0].Add(Value);
                }
            }

            // in this specific case, we can add the tags
            for (int i = 0; i < this.Count; i++)
            {
                this[i].ListTags = new List<object>();

                for (int j = 0; j < ListWell.Count; j++)
                {
                    if ((ListWell[j].GetCurrentClassIdx() >= 0) && (ListActiveClasses[ListWell[j].GetCurrentClassIdx()] == 1))
                        this[i].ListTags.Add(ListWell[j]);
                }
            }
        }

        public cExtendedTable Crop(int MinX, int MaxX, int MinY, int MaxY)
        {

            try
            {

                if ((MinX < 0) || (MinY < 0)) return null;
                if ((MaxX >= this.Count) || (MaxY >= this[0].Count)) return null;

                cExtendedTable NT = new cExtendedTable(MaxX - MinX + 1, MaxY - MinY + 1, 0);
                NT.Name = "Crop(" + this.Name + ")";
                if ((this.ListRowNames != null) && (this.ListRowNames.Count > 0))
                {
                    NT.ListRowNames = new List<string>();
                    for (int i = MinY; i <= MaxY; i++)
                    {
                        NT.ListRowNames.Add(this.ListRowNames[i]);
                    }
                }
                if ((this.ListTags != null) && (this.ListTags.Count > 0))
                {
                    NT.ListTags = new List<object>();
                    for (int i = MinY; i <= MaxY; i++)
                    {
                        NT.ListTags.Add(this.ListTags[i]);
                    }
                }

                for (int j = MinX; j <= MaxX; j++)
                {
                    NT[j - MinX].Name = this[j].Name;
                    if (this[j].ListTags != null)
                    {
                        NT[j - MinX].ListTags = new List<object>();
                        for (int i = MinY; i <= MaxY; i++)
                            NT[j - MinX].ListTags.Add(this[j].ListTags[i]);
                    }

                    for (int i = MinY; i <= MaxY; i++)
                    {
                        NT[j - MinX][i - MinY] = this[j][i];
                    }
                }

                return NT;
            }
            catch (Exception)
            {
                return null;
            }



        }

        public double Max()
        {
            cExtendedList ListMaxima = new cExtendedList();

            foreach (var item in this)
                ListMaxima.Add(item.Max());

            return ListMaxima.Max();
        }

        public double Min()
        {
            cExtendedList ListMinima = new cExtendedList();

            foreach (var item in this)
                ListMinima.Add(item.Min());

            return ListMinima.Min();

        }

        public double Mean()
        {
            cLinearize L = new cLinearize();
            L.SetInputData(this);
            L.Run();

            return L.GetOutPutAsExtendedList().Mean();
        }

        public double Sum()
        {
            cLinearize L = new cLinearize();
            L.SetInputData(this);
            L.Run();

            return L.GetOutPutAsExtendedList().Sum();
        }

    }
}
