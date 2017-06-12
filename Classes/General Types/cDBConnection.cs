using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using LibPlateAnalysis;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;

namespace HCSAnalyzer.Classes.General_Types
{

    public class cSingleCellOperations
    {
        public List<eBinaryOperationType> ListDualOperations = null;

        public List<eBinaryOperationType> PostProcessingOperation = null;
        public double PostProcessingValue = 0;

    }


    public class cDBConnection
    {
        private SQLiteConnection _SQLiteConnection;
        public string SQLFileDBName = "";
        static cPlate AssociatedPlate = null;
        public bool IsSucceed = true;

        public cDBConnection(cPlate Plate, string SQLFileDBName)
        {
            AssociatedPlate = Plate;

            if (Plate.DBConnection == null)
                this.SQLFileDBName = SQLFileDBName;
            else
                this.SQLFileDBName = Plate.DBConnection.SQLFileDBName;

            if (this.OpenConnection() == false) this.IsSucceed = false;

        }

        public bool OpenConnection()
        {
            if (SQLFileDBName == "") return false;

            this._SQLiteConnection = new SQLiteConnection("Data Source=" + SQLFileDBName);

            try
            {
                this._SQLiteConnection.Open();
            }
            catch (Exception)
            {
                return false;

            }



            return true;
        }

        public void CloseConnection()
        {
            if (this._SQLiteConnection == null) return;
            this._SQLiteConnection.Close();
            this._SQLiteConnection.Dispose();
            this._SQLiteConnection = null;
        }

        public List<string> GetListTableNames()
        {

            List<string> Toreturn = new List<string>();
            SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
            mycommand.CommandText = "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name";
            SQLiteDataReader value = mycommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(value);

            for (int i = 0; i < dt.Rows.Count; i++)
                Toreturn.Add(dt.Rows[i][0].ToString());

            mycommand.Dispose();
            mycommand = null;
            value.Close();
            value.Dispose();
            value = null;

            return Toreturn;

        }

        public void DisplayTable(cWell Well)
        {
            SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
            mycommand.CommandText = "SELECT * FROM \"" + Well.SQLTableName + "\"";
            SQLiteDataReader value = mycommand.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(value);
            cDisplayScatter2D WindowForTable = new cDisplayScatter2D(dt);
            WindowForTable.comboBoxAxeX.DataSource = this.GetDescriptorNames(Well);
            WindowForTable.comboBoxAxeY.DataSource = this.GetDescriptorNames(Well);
            WindowForTable.comboBoxVolume.DataSource = this.GetDescriptorNames(Well);
            WindowForTable.chartForPoints.Series[0].MarkerColor = Well.GetClassColor();

            // WindowForTable.ch
            WindowForTable.Text = Well.AssociatedPlate.GetName() + " [" + Well.GetPosX() + "x" + Well.GetPosY() + "]";

            WindowForTable.Show();
        }

        public int AddWellToDataTable(cWell Well, DataTable DataTableToAddedTo, cGlobalInfo GlobalInfo)
        {
            SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
            mycommand.CommandText = "SELECT * FROM \"" + Well.SQLTableName + "\"";
            SQLiteDataReader value = mycommand.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(value);
            List<string> Names = this.GetDescriptorNames(Well);

            int CurrentClass = Well.GetCurrentClassIdx();

            if (DataTableToAddedTo.Columns.Count == 0)
            {
                foreach (string TmpName in Names)
                {
                    int DescIdx = cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(TmpName);
                    if ((DescIdx != -1) && (cGlobalInfo.CurrentScreening.ListDescriptors[DescIdx].IsActive()))
                        DataTableToAddedTo.Columns.Add(new DataColumn(TmpName, typeof(double)));
                }

                //if (IsAddWellClass)
                //    DataTableToAddedTo.Columns.Add(new DataColumn("Well_Class", typeof(int)));
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTableToAddedTo.Rows.Add();
                int RealIdxCol = 0;
                for (int IdxCol = 0; IdxCol < Names.Count; IdxCol++)
                {
                    int DescIdx = cGlobalInfo.CurrentScreening.ListDescriptors.GetDescriptorIndex(Names[IdxCol]);
                    if ((DescIdx != -1) && (cGlobalInfo.CurrentScreening.ListDescriptors[DescIdx].IsActive()))
                    {
                        DataTableToAddedTo.Rows[DataTableToAddedTo.Rows.Count - 1][RealIdxCol++] = dt.Rows[i][IdxCol];
                    }
                }
                //if (IsAddWellClass)
                //    DataTableToAddedTo.Rows[DataTableToAddedTo.Rows.Count - 1][DataTableToAddedTo.Columns.Count - 1] = CurrentClass;
            }

            mycommand.Dispose();
            mycommand = null;
            value.Close();
            value.Dispose();
            value = null;

            return dt.Rows.Count;
        }

        public void ChangePhenotypeClass(cWell Well, cExtendedList ListNewClasses)
        {
            using (SQLiteTransaction transaction = _SQLiteConnection.BeginTransaction())
            using (SQLiteDataAdapter sqliteAdapter = new SQLiteDataAdapter("SELECT * FROM \"" + Well.SQLTableName + "\"", _SQLiteConnection))
            {
                DataSet DS = new DataSet();
                sqliteAdapter.Fill(DS);

                using (sqliteAdapter.InsertCommand = new SQLiteCommandBuilder(sqliteAdapter).GetUpdateCommand())
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        DS.Tables[0].Rows[i]["Phenotype_Class"] = ListNewClasses[i];

                    sqliteAdapter.Update(DS);
                }
                transaction.Commit();
            }

            return;
        }

        public cExtendedTable GetWellValues(cWell TmpWell, cDescriptorType DescType)
        {
            List<cDescriptorType> LCDT = new List<cDescriptorType>();
            LCDT.Add(DescType);

            cExtendedTable ToReturn = new cExtendedTable(this.GetWellValues(TmpWell, LCDT));

            cListSingleBiologicalObjects ListClassesPhenotypes =
                this.GetBiologicalPhenotypes(/*this.GetWellValues(TmpWell.SQLTableName, DescType),*/ TmpWell);

            for (int i = 0; i < ToReturn.Count; i++)
            {
                ToReturn[i].ListTags = new List<object>();

                for (int j = 0; j < ListClassesPhenotypes.Count; j++)
                    ToReturn[i].ListTags.Add(ListClassesPhenotypes[j]);
            }

            return ToReturn;
        }

        public cExtendedTable GetWellValues(cWell TmpWell, List<cDescriptorType> ListDescType)
        {
            cExtendedTable ToReturn = new cExtendedTable();
            ToReturn.ListTags = new List<object>();


            foreach (var item in ListDescType)
            {
                cExtendedList TmpList = new cExtendedList();
                TmpList.Name = item.GetName();
                TmpList.Tag = item;

                TmpList.ListTags = new List<object>();

                SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
                mycommand.CommandText = "SELECT \"" + item.GetName() + "\" FROM \"" + TmpWell.SQLTableName + "\" ";
                //mycommand.CommandText = "SELECT *, FROM \"" + TableName + "\"";
                SQLiteDataReader value = mycommand.ExecuteReader();
                // value.Read();
                int Pos = value.GetOrdinal(item.GetName());

                if (Pos == -1) continue;
                if (value.GetValue(Pos).ToString() == "") continue;
                while (value.Read())
                {
                    TmpList.Add(value.GetFloat(Pos));
                }

                ToReturn.Add(TmpList);

                mycommand.Dispose();
                mycommand = null;
                value.Close();
                value.Dispose();
                value = null;

            }
            return ToReturn;
        }

        public cExtendedTable GetWellValues(cWell TmpWell, List<cDescriptorType> ListDescType, List<cCellularPhenotype> PhenotypesToBeSelected)
        {
            cExtendedTable ToReturn = new cExtendedTable();
            //ToReturn.ListTags = new List<object>();

            
            foreach (var item in ListDescType)
            {
                if (item.IsConnectedToDatabase == false) continue;
                cExtendedList TmpList = new cExtendedList();
                TmpList.Name = item.GetName();
                TmpList.Tag = item;

               // TmpList.ListTags = new List<object>();

                SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
                mycommand.CommandText = "SELECT \"" + item.GetName() + "\" FROM \"" + TmpWell.SQLTableName + "\" WHERE Phenotype_Class IN (";

                foreach (var PhenotypeClass in PhenotypesToBeSelected)
                {
                    mycommand.CommandText += PhenotypeClass.Idx + ",";
                }
                mycommand.CommandText = mycommand.CommandText.Remove(mycommand.CommandText.Length - 1);
                mycommand.CommandText += ")";

                //mycommand.CommandText = "SELECT *, FROM \"" + TableName + "\"";
                SQLiteDataReader value = mycommand.ExecuteReader();
                // value.Read();
                int Pos = value.GetOrdinal(item.GetName());

                if (Pos == -1) continue;
                while (value.Read())
                {
                    TmpList.Add(value.GetFloat(Pos));
                }

                ToReturn.Add(TmpList);

                mycommand.Dispose();
                mycommand = null;
                value.Close();
                value.Dispose();
                value = null;


            }
            return ToReturn;
        }

        public cExtendedTable GetWellValues(cWell TmpWell, List<cDescriptorType> ListDescType, List<cCellularPhenotype> PhenotypesToBeSelected, double PopulationRatio)
        {
            cExtendedTable ToReturn = new cExtendedTable();

            foreach (var item in ListDescType)
            {
                cExtendedList TmpList = new cExtendedList();
                TmpList.Name = item.GetName();
                TmpList.Tag = item;

                TmpList.ListTags = new List<object>();

                SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
                mycommand.CommandText = "SELECT \"" + item.GetName() + "\" FROM \"" + TmpWell.SQLTableName + "\" WHERE Phenotype_Class IN (";

                foreach (var PhenotypeClass in PhenotypesToBeSelected)
                {
                    mycommand.CommandText += PhenotypeClass.Idx + ",";
                }
                mycommand.CommandText = mycommand.CommandText.Remove(mycommand.CommandText.Length - 1);
                mycommand.CommandText += ")";

                //mycommand.CommandText = "SELECT *, FROM \"" + TableName + "\"";
                SQLiteDataReader value = mycommand.ExecuteReader();
                // value.Read();
                int Pos = value.GetOrdinal(item.GetName());

                if (Pos == -1) continue;
                while (value.Read())
                {
                    TmpList.Add(value.GetFloat(Pos));
                }

                cExtendedList SubListToBeTransfered = new cExtendedList(TmpList, PopulationRatio);

                ToReturn.Add(SubListToBeTransfered);

                mycommand.Dispose();
                mycommand = null;
                value.Close();
                value.Dispose();
                value = null;


            }
            return ToReturn;
        }

        //public cExtendedTable GetWellValues(string TableName, List<cDescriptorsType> ListDescType)
        //{
        //    cExtendedTable ToReturn = new cExtendedTable();

        //    foreach (var item in ListDescType)
        //    {
        //        cExtendedList TmpList = new cExtendedList();

        //        SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
        //        mycommand.CommandText = "SELECT *, \"" + item.GetName() + "\" FROM \"" + TableName + "\"";
        //        //mycommand.CommandText = "SELECT *, FROM \"" + TableName + "\"";
        //        SQLiteDataReader value = mycommand.ExecuteReader();
        //        // value.Read();
        //        int Pos = value.GetOrdinal(item.GetName());

        //        //if (Pos == -1) continue;

        //        while (value.Read()) TmpList.Add(value.GetFloat(Pos));

        //        ToReturn.Add(TmpList);
        //    }
        //    return ToReturn;
        //}

        public cExtendedTable GetWellValues(string TableName, List<cDescriptorType> ListDescType)
        {
            cExtendedTable ToReturn = new cExtendedTable();

            foreach (var item in ListDescType)
            {
                cExtendedList TmpList = new cExtendedList();

                SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
                mycommand.CommandText = "SELECT *, \"" + item.GetName() + "\" FROM \"" + TableName + "\"";
                //mycommand.CommandText = "SELECT *, FROM \"" + TableName + "\"";
                SQLiteDataReader value = mycommand.ExecuteReader();
                // value.Read();
                int Pos = value.GetOrdinal(item.GetName());

                cExtendedList ListErrorsIdx = new cExtendedList("List Error Indexes - " + TableName);
                int IdxRow = 0;
                //if (Pos == -1) continue;
                while (value.Read())
                {
                    if (value.GetValue(Pos).ToString() != "")
                    {
                        TmpList.Add(value.GetDouble(Pos));
                    }
                    else
                    {
                        TmpList.Add(0);
                        ListErrorsIdx.Add(IdxRow);
                    }
                    IdxRow++;
                }
                ToReturn.Add(TmpList);

                mycommand.Dispose();
                mycommand = null;
                value.Close();
                value.Dispose();
                value = null;


            }
            return ToReturn;
        }

        public cExtendedTable GetWellPhenotypeId(cWell TmpWell, List<cCellularPhenotype> PhenotypesToBeSelected)
        {
            cExtendedTable ToReturn = new cExtendedTable();

          //  foreach (var item in ListDescType)
           // {
                cExtendedList TmpList = new cExtendedList();
                TmpList.Name = "Phenotype_Class";
                //TmpList.Tag = item;

                TmpList.ListTags = new List<object>();

                SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);

                if (PhenotypesToBeSelected != null)
                {
                    mycommand.CommandText = "SELECT \"" + "Phenotype_Class" + "\" FROM \"" + TmpWell.SQLTableName + "\" WHERE Phenotype_Class IN (";

                    foreach (var PhenotypeClass in PhenotypesToBeSelected)
                    {
                        mycommand.CommandText += PhenotypeClass.Idx + ",";
                    }
                    mycommand.CommandText = mycommand.CommandText.Remove(mycommand.CommandText.Length - 1);
                    mycommand.CommandText += ")";
                }
                else
                {
                    mycommand.CommandText = "SELECT \"" + "Phenotype_Class" + "\" FROM \"" + TmpWell.SQLTableName + "\"";
                }

                //mycommand.CommandText = "SELECT *, FROM \"" + TableName + "\"";
                SQLiteDataReader value = mycommand.ExecuteReader();
                // value.Read();
                int Pos = value.GetOrdinal("Phenotype_Class");

                if (Pos != -1)
                {
                    while (value.Read())    TmpList.Add(value.GetFloat(Pos));
                }

                ToReturn.Add(TmpList);

                mycommand.Dispose();
                mycommand = null;
                value.Close();
                value.Dispose();
                value = null;


           // }
            return ToReturn;


            //cExtendedTable ToReturn = new cExtendedTable();

            //cExtendedList TmpList = new cExtendedList();

            //SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
            //mycommand.CommandText = "SELECT *, \"" + "Phenotype_Class" + "\" FROM \"" + TmpWell.SQLTableName + "\"";
            //SQLiteDataReader value = mycommand.ExecuteReader();
            //int PosPhenotypeID = value.GetOrdinal("Phenotype_Class");


            //cExtendedList ListErrorsIdx = new cExtendedList("List Error Indexes - " + TmpWell.Name);
            //int IdxRow = 0;
            ////if (Pos == -1) continue;
            //while (value.Read())
            //{
            //    if (value.GetValue(PosPhenotypeID).ToString() != "")
            //    {
            //        TmpList.Add(value.GetDouble(PosPhenotypeID));
            //    }
            //    else
            //    {
            //        TmpList.Add(0);
            //        ListErrorsIdx.Add(IdxRow);
            //    }
            //    IdxRow++;
            //}
            //ToReturn.Add(TmpList);

            //return ToReturn;


        }


        //public cExtendedList GetWellPhenotypeClasses(string TableName)
        //{
        //    cExtendedList ToReturn = new cExtendedList();

        //    SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
        //    mycommand.CommandText = "SELECT *, \"" + "Phenotype_Class" + "\" FROM \"" + TableName + "\"";
        //    SQLiteDataReader value = mycommand.ExecuteReader();
        //    int Pos = value.GetOrdinal("Phenotype_Class");

        //    while (value.Read())
        //        ToReturn.Add(value.GetFloat(Pos));

        //    return ToReturn;
        //}

        public cListSingleBiologicalObjects GetBiologicalPhenotypes(cWell TmpWell)
        {
            cListSingleBiologicalObjects ToReturn = new cListSingleBiologicalObjects();

            SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
            mycommand.CommandText = "SELECT *, \"" + "Phenotype_Class" + "\" FROM \"" + TmpWell.SQLTableName + "\"";
            SQLiteDataReader value = mycommand.ExecuteReader();
            int PosPhenotypeID = value.GetOrdinal("Phenotype_Class");

            int PosPhenotypeConfidence = value.GetOrdinal("Phenotype_Confidence");
            int PosPosX = value.GetOrdinal(cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosX.Text);
            int PosPosY = value.GetOrdinal(cGlobalInfo.OptionsWindow.comboBoxDescriptorForPosY.Text);

            int BB_MinX = value.GetOrdinal(cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinX.Text);
            int BB_MaxX = value.GetOrdinal(cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxX.Text);
            int BB_MinY = value.GetOrdinal(cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMinY.Text);
            int BB_MaxY = value.GetOrdinal(cGlobalInfo.OptionsWindow.comboBoxDescForBoundingMaxY.Text);
            
            int PosField = value.GetOrdinal(cGlobalInfo.OptionsWindow.comboBoxDescriptorForField.Text);
            int PosIdx = value.GetOrdinal("rowid");

            if (PosPhenotypeID != -1)
            {
                while (value.Read())
                {
                    string Res = value.GetValue(PosPhenotypeID).ToString();
                    if (Res == "") continue;
                    cSingleBiologicalObject NewPhenotype = new cSingleBiologicalObject(cGlobalInfo.ListCellularPhenotypes[(int)value.GetFloat(PosPhenotypeID)],
                                    TmpWell, (int)value.GetFloat(PosIdx));
                    ToReturn.Add(NewPhenotype);


                    if (PosPhenotypeConfidence != -1)
                        NewPhenotype.ClassificationConfidence = (double)value.GetFloat(PosPhenotypeConfidence);
                    else
                    {
                        cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(TmpWell.AssociatedPlate.GetName() + " / " + TmpWell.GetPos() + ": Phenotype_Confidence column not found. Value set to null.");
                        NewPhenotype.ClassificationConfidence = 0;
                    }

                    if ((PosPosX != -1) && (PosPosY != -1))
                        ToReturn[ToReturn.Count - 1].Position = new _3D.cPoint3D(value.GetFloat(PosPosX), value.GetFloat(PosPosY), -1);

                    if ((BB_MinX != -1) && (BB_MinY != -1) && (BB_MaxX != -1) && (BB_MaxY != -1))
                    {
                        ToReturn[ToReturn.Count - 1].BD_BoxMin = new _3D.cPoint3D(value.GetFloat(BB_MinX), value.GetFloat(BB_MinY), -1);
                        ToReturn[ToReturn.Count - 1].BD_BoxMax = new _3D.cPoint3D(value.GetFloat(BB_MaxX), value.GetFloat(BB_MaxY), -1);
                    }


                    if (PosField != -1)
                        ToReturn[ToReturn.Count - 1].ImageField = (int)value.GetFloat(PosField);
                }
            }
            else
            {
                while (value.Read())
                {
                    cSingleBiologicalObject NewPhenotype = new cSingleBiologicalObject(cGlobalInfo.ListCellularPhenotypes[0], TmpWell, (int)value.GetFloat(PosIdx));
                    NewPhenotype.ClassificationConfidence = (double)value.GetFloat(PosPhenotypeConfidence);
                    ToReturn.Add(NewPhenotype);

                    if ((PosPosX != -1) && (PosPosY != -1))
                        ToReturn[ToReturn.Count - 1].Position = new _3D.cPoint3D(value.GetFloat(PosPosX), value.GetFloat(PosPosY), -1);

                    if (PosField != -1)
                        ToReturn[ToReturn.Count - 1].ImageField = (int)value.GetFloat(PosField);
                }

            }

            mycommand.Dispose();
            mycommand = null;
            value.Close();
            value.Dispose();
            value = null;


            return ToReturn;
        }

        public DataTable GetWellAllDescriptorValues(string TableName)
        {
            //  cExtendedList ToReturn = new cExtendedList();

            SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
            // mycommand.CommandText = "SELECT *, \"" + DescType.GetName() + "\" FROM \"" + TableName + "\"";
            mycommand.CommandText = "SELECT * FROM \"" + TableName + "\"";
            SQLiteDataReader value = mycommand.ExecuteReader();
            // value.Read();

            //object[] myObjectArray = new object[value.FieldCount];

            //while (value.Read())
            //{
            //    value.GetValues(myObjectArray);
            //    int a = 1;
            //    //ToReturn.Add((double)myObjectArray[0]);
            //}
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(mycommand.CommandText, _SQLiteConnection);

            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);



            //Get the collection of rows from the DataSet
            //  DataRowCollection dataRowCol = ds.Tables[0].Rows;

            DataTable TableToReturn = ds.Tables[0];
            //Add the tables available in the DB to the combo box
            //foreach (DataRow dr in dataRowCol)
            //{
            //    tablecombobox.Items.Add(dr["name"]);
            //}


            /*int Pos = value.GetOrdinal(DescType.GetName());

            while (value.Read())
            {
                ToReturn.Add(value.GetFloat(Pos));
            }
            */
            dataAdapter.Dispose();
            dataAdapter = null;

            mycommand.Dispose();
            mycommand = null;
            value.Close();
            value.Dispose();
            value = null;


            return TableToReturn;
        }

        public List<string> GetDescriptorNames(int IdxWell)
        {
            List<string> ToReturn = new List<string>();
            string NameWell = "\"" + GetListTableNames()[IdxWell] + "\"";

            SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
            mycommand.CommandText = "SELECT * FROM " + NameWell;
            SQLiteDataReader value = mycommand.ExecuteReader();

            for (int i = 0; i < value.FieldCount; i++)
                ToReturn.Add(value.GetName(i));

            mycommand.Dispose();
            mycommand = null;
            value.Close();
            value.Dispose();
            value = null;

            return ToReturn;
        }

        public bool CheckIfColumnExist(int IdxWell, string ColumnName)
        {
            string NameWell = "\"" + GetListTableNames()[IdxWell] + "\"";

            SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
            mycommand.CommandText = "SELECT " + ColumnName + " FROM " + NameWell;
            try
            {
                SQLiteDataReader value = mycommand.ExecuteReader();
                mycommand.Dispose();
                mycommand = null;
                value.Close();
                value.Dispose();
                value = null;


            }
            catch
            {
                mycommand.Dispose();
                mycommand = null;

                return false;
            }
            return true;
        }

        public int GetNumberOfRows(cWell Well)
        {
            SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);

            mycommand.CommandText = "SELECT COUNT(*) FROM " + Well.SQLTableName;
            try
            {
                SQLiteDataReader value = mycommand.ExecuteReader();

                int ToReturn = 0;
                while (value.Read())
                {
                    ToReturn = value.GetInt32(0);
                }

                mycommand.Dispose();
                mycommand = null;
                value.Close();
                value.Dispose();
                value = null;


                return ToReturn;
            }
            catch
            {
                mycommand.Dispose();
                mycommand = null;
                return -1;
            }

        }

        /// <summary>
        /// Create a new column in the database of the current plate
        /// </summary>
        /// <param name="ColumnName">Column name</param>
        /// <param name="DefaultValue">Default value</param>
        /// <returns>Return the number of table (i.e. wells) processed</returns>
        public int CreateNewColumn(string ColumnName, double DefaultValue)
        {
            int NumTableProcessed = 0;
            foreach (var item in GetListTableNames())
            {
                SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
                String SQL = "ALTER TABLE \"" + item + "\" ADD COLUMN \"" + ColumnName + "\" REAL DEFAULT " + DefaultValue;
                using (SQLiteCommand Command = new SQLiteCommand(SQL, _SQLiteConnection))
                {
                    Command.ExecuteNonQuery();
                }
                NumTableProcessed++;


                mycommand.Dispose();
                mycommand = null;
            }
            return NumTableProcessed;

        }

        public void RemoveColumn(string ColumnName)
        {


            // using (SQLiteTransaction transaction = _SQLiteConnection.BeginTransaction())
            //// using (SQLiteDataAdapter sqliteAdapter = new SQLiteDataAdapter("SELECT * FROM \"" + item + "\"", _SQLiteConnection))
            // {
            //     //DataSet DS = new DataSet();
            //     //sqliteAdapter.Fill(DS);

            //     //using (sqliteAdapter.InsertCommand = new SQLiteCommandBuilder(sqliteAdapter).GetUpdateCommand())
            //     //{
            //     //    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            //     //        DS.Tables[0].Rows[i][ColumnName] = ListResults[i];

            //     //    sqliteAdapter.Update(DS);
            //     //}
            //     transaction.Commit();
            // }

            List<string> ListWellNames = GetListTableNames();
            if ((ListWellNames == null) || (ListWellNames.Count == 0)) return;

            List<string> ListColNames = new List<string>();
            string NameWell = "\"" + ListWellNames[0] + "\"";

            SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
            mycommand.CommandText = "SELECT * FROM " + NameWell;
            SQLiteDataReader value = mycommand.ExecuteReader();


            string SQL_BackUpString = "";
            for (int i = 0; i < value.FieldCount; i++)
            {
                string TmpS = value.GetName(i);
                if (TmpS != ColumnName)
                    SQL_BackUpString += TmpS + ",";
                //ListColNames.Add(value.GetName(i));
            }

            SQL_BackUpString = SQL_BackUpString.Remove(SQL_BackUpString.Length - 1);



            //  int NumTableProcessed = 0;
            foreach (var item in ListWellNames)
            {
                mycommand = new SQLiteCommand(_SQLiteConnection);
                string SQLCommand = "CREATE TEMPORARY TABLE t1_backup(" + SQL_BackUpString + ")";

                using (SQLiteCommand Command = new SQLiteCommand(SQLCommand, _SQLiteConnection))
                    Command.ExecuteNonQuery();

                SQLCommand = "INSERT INTO t1_backup SELECT " + SQL_BackUpString + " FROM " + item;
                using (SQLiteCommand Command = new SQLiteCommand(SQLCommand, _SQLiteConnection))
                    Command.ExecuteNonQuery();


                //   _SQLiteConnection.Close();

                SQLCommand = "DROP TABLE " + item;
                using (SQLiteCommand Command = new SQLiteCommand(SQLCommand, _SQLiteConnection))
                    Command.ExecuteNonQuery();

                //    _SQLiteConnection.Open();

                //using (SQLiteTransaction transaction = _SQLiteConnection.BeginTransaction())
                //using (SQLiteDataAdapter sqliteAdapter = new SQLiteDataAdapter(SQLCommand, _SQLiteConnection))
                //{
                //    //DataSet DS = new DataSet();
                //    //sqliteAdapter.Fill(DS);

                //    using (sqliteAdapter.InsertCommand = new SQLiteCommandBuilder(sqliteAdapter).GetUpdateCommand())
                //    {
                //      //  for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                //       //     DS.Tables[0].Rows[i]["Phenotype_Class"] = ListNewClasses[i];

                //       // sqliteAdapter.Update(DS);
                //    }
                //    transaction.Commit();
                //}




                SQLCommand = "CREATE TABLE " + item + "(" + SQL_BackUpString + ")";
                using (SQLiteCommand Command = new SQLiteCommand(SQLCommand, _SQLiteConnection))
                    Command.ExecuteNonQuery();

                SQLCommand = "INSERT INTO " + item + " SELECT " + SQL_BackUpString + " FROM t1_backup";
                using (SQLiteCommand Command = new SQLiteCommand(SQLCommand, _SQLiteConnection))
                    Command.ExecuteNonQuery();

                SQLCommand = "DROP TABLE t1_backup";
                using (SQLiteCommand Command = new SQLiteCommand(SQLCommand, _SQLiteConnection))
                    Command.ExecuteNonQuery();


                //                How do I add or delete columns from an existing table in SQLite.

                //SQLite has limited ALTER TABLE support that you can use to add a column to the end of a table or to change the name of a table. If you want to make more complex changes in the structure of a table, you will have to recreate the table. You can save existing data to a temporary table, drop the old table, create the new table, then copy the data back in from the temporary table.

                //For example, suppose you have a table named "t1" with columns names "a", "b", and "c" and that you want to delete column "c" from this table. The following steps illustrate how this could be done:

                //BEGIN TRANSACTION;
                //CREATE TEMPORARY TABLE t1_backup(a,b);
                //INSERT INTO t1_backup SELECT a,b FROM t1;
                //DROP TABLE t1;
                //CREATE TABLE t1(a,b);
                //INSERT INTO t1 SELECT a,b FROM t1_backup;
                //DROP TABLE t1_backup;
                //COMMIT;


            }
            return;

        }


        /// <summary>
        /// Create a new column in the database of the current plate
        /// </summary>
        /// <param name="ColumnName">Column name</param>
        /// <param name="DefaultValue">Array of values</param>
        /// <returns>Return the number of table (i.e. wells) processed</returns>
        public void CreateNewColumn(cDescriptorType NewDescType, cDescriptorType Desc0, cDescriptorType Desc1,
                                    cSingleCellOperations SingleCellOperations, 
                                    cGlobalInfo GlobalInfo, ref cListWells ListWell)
        {
            string ColumnName = NewDescType.GetName();

            int NumTableProcessed = 0;
            foreach (var item in GetListTableNames())   // loop over the wells
            {
                // compute the new values
                List<cDescriptorType> LDes = new List<cDescriptorType>();
                LDes.Add(Desc0);
                LDes.Add(Desc1);
                cExtendedTable LValues = this.GetWellValues(item, LDes);
                cExtendedList ListResults = LValues[0].Operation(LValues[1], SingleCellOperations.ListDualOperations[0]);

                if ((SingleCellOperations.PostProcessingOperation != null) && (SingleCellOperations.PostProcessingOperation.Count > 0))
                {
                    switch (SingleCellOperations.PostProcessingOperation[0])
                    {
                        case eBinaryOperationType.ADD:
                            ListResults += SingleCellOperations.PostProcessingValue;
                            break;
                        case eBinaryOperationType.SUBSTRACT:
                            ListResults -= SingleCellOperations.PostProcessingValue;
                            break;
                        case eBinaryOperationType.MULTIPLY:
                            ListResults *= SingleCellOperations.PostProcessingValue;
                            break;
                        case eBinaryOperationType.DIVIDE:
                            ListResults /= SingleCellOperations.PostProcessingValue;
                            break;
                        default:
                            break;
                    }
                }

                #region Update the database
                // create new column
                SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
                String SQL = "ALTER TABLE \"" + item + "\" ADD COLUMN \"" + ColumnName + "\" REAL DEFAULT 0";
                using (SQLiteCommand Command = new SQLiteCommand(SQL, _SQLiteConnection))
                {
                    Command.ExecuteNonQuery();
                }

                // write data
                using (SQLiteTransaction transaction = _SQLiteConnection.BeginTransaction())
                using (SQLiteDataAdapter sqliteAdapter = new SQLiteDataAdapter("SELECT * FROM \"" + item + "\"", _SQLiteConnection))
                {
                    DataSet DS = new DataSet();
                    sqliteAdapter.Fill(DS);

                    using (sqliteAdapter.InsertCommand = new SQLiteCommandBuilder(sqliteAdapter).GetUpdateCommand())
                    {
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                            DS.Tables[0].Rows[i][ColumnName] = ListResults[i];

                        sqliteAdapter.Update(DS);
                    }
                    transaction.Commit();
                }
                #endregion

                #region Update the screening data
                cListSignature LDesc = new cListSignature();
                cSignature NewSignature = new cSignature(ListResults, 256, NewDescType, cGlobalInfo.CurrentScreening);
                LDesc.Add(NewSignature);

                string[] Positions = item.Split('x');

                cWell TmpWell = ListWell.GetFirstWell(int.Parse(Positions[1]), int.Parse(Positions[2]));
                if(TmpWell!=null) TmpWell.AddSignatures(LDesc);
                #endregion

                NumTableProcessed++;
            }
        }



        /// <summary>
        /// Create a new column in the database of the current plate
        /// </summary>
        /// <param name="ColumnName">Column name</param>
        /// <param name="DefaultValue">Array of values</param>
        /// <returns>Return the number of table (i.e. wells) processed</returns>
        public void CreateNewColumn(cDescriptorType NewDescType, cDescriptorType Desc0, cDescriptorType Desc1, eBinaryOperationType OperationType, cGlobalInfo GlobalInfo, ref cListWells ListWell)
        {
            string ColumnName = NewDescType.GetName();

            int NumTableProcessed = 0;
            foreach (var item in GetListTableNames())   // loop over the wells
            {
                // compute the new values
                List<cDescriptorType> LDes = new List<cDescriptorType>();
                LDes.Add(Desc0);
                LDes.Add(Desc1);
                cExtendedTable LValues = this.GetWellValues(item, LDes);
                cExtendedList ListResults = LValues[0].Operation(LValues[1], OperationType);

                #region Update the database
                // create new column
                SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
                String SQL = "ALTER TABLE \"" + item + "\" ADD COLUMN \"" + ColumnName + "\" REAL DEFAULT 0";
                using (SQLiteCommand Command = new SQLiteCommand(SQL, _SQLiteConnection))
                {
                    Command.ExecuteNonQuery();
                }

                // write data
                using (SQLiteTransaction transaction = _SQLiteConnection.BeginTransaction())
                using (SQLiteDataAdapter sqliteAdapter = new SQLiteDataAdapter("SELECT * FROM \"" + item + "\"", _SQLiteConnection))
                {
                    DataSet DS = new DataSet();
                    sqliteAdapter.Fill(DS);

                    using (sqliteAdapter.InsertCommand = new SQLiteCommandBuilder(sqliteAdapter).GetUpdateCommand())
                    {
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                            DS.Tables[0].Rows[i][ColumnName] = ListResults[i];

                        sqliteAdapter.Update(DS);
                    }
                    transaction.Commit();
                }
                #endregion

                #region Update the screening data
                cListSignature LDesc = new cListSignature();
                cSignature NewSignature = new cSignature(ListResults, 256, NewDescType, cGlobalInfo.CurrentScreening);
                LDesc.Add(NewSignature);

                string[] Positions = item.Split('x');
                ListWell.GetFirstWell(int.Parse(Positions[1]), int.Parse(Positions[2])).AddSignatures(LDesc);
                #endregion

                NumTableProcessed++;
            }
        }

        public void CreateNewColumn(cDescriptorType NewDescType, cDescriptorType Desc0, eUnaryOperationType OperationType, cGlobalInfo GlobalInfo, ref cListWells ListWell)
        {
            string ColumnName = NewDescType.GetName();

            int NumTableProcessed = 0;
            foreach (var item in GetListTableNames())   // loop over the wells
            {
                // compute the new values
                List<cDescriptorType> LDes = new List<cDescriptorType>();
                LDes.Add(Desc0);
              //  LDes.Add(Desc1);
                cExtendedTable LValues = this.GetWellValues(item, LDes);
                cExtendedList ListResults = new cExtendedList();// LValues[0].Operation(LValues[1], OperationType);

                if (OperationType == eUnaryOperationType.LOG)
                {
                    for (int i = 0; i < LValues[0].Count; i++)
                    {
                        double tmpVal = LValues[0][i];
                        if (tmpVal <= 0) tmpVal = double.Epsilon;
                        ListResults.Add(Math.Log10(tmpVal));
                    }
                }
                else if (OperationType == eUnaryOperationType.SQRT)
                {
                    for (int i = 0; i < LValues[0].Count; i++)
                    {
                        double tmpVal = LValues[0][i];
                        if (tmpVal < 0) tmpVal = 0;
                        ListResults.Add(Math.Sqrt(tmpVal));
                    }
                }
                else if (OperationType == eUnaryOperationType.ABS)
                {
                    for (int i = 0; i < LValues[0].Count; i++)
                    {
                        double tmpVal = LValues[0][i];
                        ListResults.Add(Math.Abs(tmpVal));
                    }
                }
                else if (OperationType == eUnaryOperationType.EXP)
                {
                    for (int i = 0; i < LValues[0].Count; i++)
                    {
                        double tmpVal = LValues[0][i];
                        ListResults.Add(Math.Exp(tmpVal));
                    }
                }

                #region Update the database
                // create new column
                SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
                String SQL = "ALTER TABLE \"" + item + "\" ADD COLUMN \"" + ColumnName + "\" REAL DEFAULT 0";
                using (SQLiteCommand Command = new SQLiteCommand(SQL, _SQLiteConnection))
                {
                    Command.ExecuteNonQuery();
                }

                // write data
                using (SQLiteTransaction transaction = _SQLiteConnection.BeginTransaction())
                using (SQLiteDataAdapter sqliteAdapter = new SQLiteDataAdapter("SELECT * FROM \"" + item + "\"", _SQLiteConnection))
                {
                    DataSet DS = new DataSet();
                    sqliteAdapter.Fill(DS);

                    using (sqliteAdapter.InsertCommand = new SQLiteCommandBuilder(sqliteAdapter).GetUpdateCommand())
                    {
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                            DS.Tables[0].Rows[i][ColumnName] = ListResults[i];

                        sqliteAdapter.Update(DS);
                    }
                    transaction.Commit();
                }
                #endregion

                #region Update the screening data
                cListSignature LDesc = new cListSignature();
                cSignature NewSignature = new cSignature(ListResults, 256, NewDescType, cGlobalInfo.CurrentScreening);
                LDesc.Add(NewSignature);

                string[] Positions = item.Split('x');
                ListWell.GetFirstWell(int.Parse(Positions[1]), int.Parse(Positions[2])).AddSignatures(LDesc);
                #endregion

                NumTableProcessed++;
            }
        }


        public void CreateNewColumnFromExisting(cDescriptorType NewDescType, string DescToBeDuplicated, cGlobalInfo GlobalInfo, ref cListWells ListWell)
        {
            this.CreateNewColumn(NewDescType.GetName(), 0);

            // cExtendedList ListResults = null;


            foreach (var item in GetListTableNames())
            {
                SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
                //update tableName set onefield = secondfield;
                string SQL = "UPDATE \"" + item + "\" SET \"" + NewDescType.GetName() + "\" = \"" + DescToBeDuplicated + "\"";
                using (SQLiteCommand Command = new SQLiteCommand(SQL, _SQLiteConnection))
                {
                    Command.ExecuteNonQuery();
                }

                #region Update the screening data
                List<cDescriptorType> LDes = new List<cDescriptorType>();
                LDes.Add(NewDescType);
                cExtendedTable LValues = this.GetWellValues(item, LDes);



                cListSignature LDesc = new cListSignature();
                cSignature NewSignature = new cSignature(LValues[0], NewDescType.GetBinNumber(), NewDescType, cGlobalInfo.CurrentScreening);
                LDesc.Add(NewSignature);

                string[] Positions = item.Split('x');
                ListWell.GetFirstWell(int.Parse(Positions[1]), int.Parse(Positions[2])).AddSignatures(LDesc);
                #endregion
            }




            return;
        }

        public List<string> GetDescriptorNames(cWell Well)
        {
            List<string> ToReturn = new List<string>();
            //  string NameWell = GetListTableNames()[IdxWell];

            SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);
            mycommand.CommandText = "SELECT * FROM \"" + Well.SQLTableName + "\"";
            SQLiteDataReader value = mycommand.ExecuteReader();

            for (int i = 0; i < value.FieldCount; i++)
                ToReturn.Add(value.GetName(i));

            return ToReturn;
        }


    }

}
