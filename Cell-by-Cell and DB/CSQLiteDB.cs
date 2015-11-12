using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Data;
using System.IO;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes;

namespace HCSAnalyzer.Classes
{
    public class cWellForDatabase
    {
        public string PlateName { get; private set; }
        public cExtendedTable ListValues = new cExtendedTable();
        public int PosX { get; private set; }
        public int PosY { get; private set; }

        public cWellForDatabase(string PlateName, int PosX, int PosY)
        {
            this.PlateName = PlateName.Replace(' ','_');
            this.PosX = PosX;
            this.PosY = PosY;
        }

        public void AddListSignatures(cExtendedTable ListValues)
        {
            this.ListValues.AddRange(ListValues);
        }

        public void AddSignature(cExtendedList Signature)
        {
            this.ListValues.Add(Signature);
        }

        public void AddToDataTable(DataTable DataTableToAddedTo)
        {
            for (int i = 0; i < this.ListValues.Count; i++)
            {
                DataTableToAddedTo.Rows.Add();

                for (int IdxCol = 0; IdxCol < ListValues[i].Count; IdxCol++)
                    DataTableToAddedTo.Rows[DataTableToAddedTo.Rows.Count - 1][IdxCol] = this.ListValues[i][IdxCol];
            }
        }
    }


    public class cSQLiteDatabase
    {
        public SQLiteConnection _SQLiteConnection;
        private string ScreeningName;
        List<string> NamesDescriptors = new List<string>();

        public cSQLiteDatabase(string PlateName, List<string> ListValueNames, bool EraseExistingFile)
        {

            if ((EraseExistingFile)&&(File.Exists(PlateName + ".db")))
            {
                File.Delete(PlateName + ".db");
            }

            this.ScreeningName = PlateName;
            this.NamesDescriptors = ListValueNames;

            _SQLiteConnection = new SQLiteConnection("Data Source=" + PlateName + ".db");
            _SQLiteConnection.Open();
       }

        public void SaveTableForHCS_Analyzer(DataTable TableToSave, int PosX, int PosY)
        {
            string WellName = /*TableToSave.TableName + "_" +*/ "\"Wellx" + PosX + "x" + PosY + "\"";
            string sql = "CREATE TABLE " + WellName + " ( rowid INTEGER PRIMARY KEY";
            //sql += ", posx  INTEGER";
            //sql += ", posy  INTEGER";

            foreach (string TmpDescValue in NamesDescriptors)
                sql += ", " + TmpDescValue + " REAL";

            sql += ");";
            SQLiteCommand cmdInsert = _SQLiteConnection.CreateCommand();
            cmdInsert.CommandText = sql;
            cmdInsert.ExecuteNonQuery();

            foreach (DataRow Signature in TableToSave.Rows)
            {
                sql = "INSERT INTO " + WellName + " (posx, posy";

                foreach (string TmpDescValue in NamesDescriptors) sql += ", " + TmpDescValue;

                sql += ") VALUES (" + PosX + ", " + PosY + ", ";

                for (int Idx = 0; Idx < NamesDescriptors.Count; Idx++)
                {
                    sql += Signature[Idx] + ", ";
                }

                string FormattedSQLCmd = sql.Remove(sql.Length - 2) + ")";

                cmdInsert.CommandText = FormattedSQLCmd;
                cmdInsert.ExecuteNonQuery();
            }
        }

        public void CloseConnection()
        {
            if (_SQLiteConnection != null)
            {
                _SQLiteConnection.Close();
                _SQLiteConnection.Dispose();
                _SQLiteConnection = null;
            }

        }

        public cFeedBackMessage AddNewWell(cWellForDatabase WellForDatabase)
        {
            cFeedBackMessage FeedBackMessage = new cFeedBackMessage(true,null);
            //string WellName = "\"" + WellForDatabase.PlateName + "_" + 
            string WellName = "\"Wellx" + WellForDatabase.PosX + "x" + WellForDatabase.PosY + "\"";

            FeedBackMessage.Message = WellName;

            string sql = "CREATE TABLE " + WellName + " ( rowid INTEGER PRIMARY KEY";

            foreach (string TmpDescValue in NamesDescriptors)
                sql += ", \"" + TmpDescValue + "\" REAL";

            sql += ");";
            SQLiteCommand cmdInsert = _SQLiteConnection.CreateCommand();
            cmdInsert.CommandText = sql;

            try
            {
                cmdInsert.ExecuteNonQuery();
            }
            catch(SQLiteException excep)
            {
                FeedBackMessage.Message += " : " + excep.Message;
                FeedBackMessage.IsSucceed = false;
                return FeedBackMessage;
            }

            cmdInsert.Dispose();
            cmdInsert = null;

            SQLiteTransaction mytransaction = _SQLiteConnection.BeginTransaction();
            SQLiteCommand mycommand = new SQLiteCommand(_SQLiteConnection);

            sql = "INSERT INTO " + WellName + " (";
            int Idx = 0;
            foreach (string TmpDescValue in NamesDescriptors)
            {
                sql += "\"" + TmpDescValue  + "\", ";
            }
            string TmpSql1 = sql;
            sql = TmpSql1.Remove(TmpSql1.Length - 2);
            sql += ") VALUES ( ";
            string FormattedSQLCmd = sql.Remove(sql.Length - 2) + ")";

            foreach (string TmpDescValue in NamesDescriptors)
                sql += "?, ";

            FormattedSQLCmd = sql.Remove(sql.Length - 2) + " )";
            mycommand.CommandText = FormattedSQLCmd;

            foreach (string TmpDescValue in NamesDescriptors)
            {
                SQLiteParameter Param = new SQLiteParameter(DbType.Double);
                mycommand.Parameters.Add(Param);
            }

            foreach (List<double> Signature in WellForDatabase.ListValues)
            {
                Idx = 0;
                foreach (double Value in Signature)
                {
                    mycommand.Parameters[Idx].Value = Value;
                    Idx++;
                }

                mycommand.ExecuteNonQuery();
            }
            mytransaction.Commit();

            
            mycommand.Dispose();
            mycommand = null;

            return FeedBackMessage;

        }

        //private void UpDateDbFromDataTable()
        //{

        //    //    OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + CurrentBrainDatabase.DataBasePath + "\\brain1.accdb");

        //    DataTable TableToSave = new DataTable();//CurrentBrainDatabase.BiologicalObjects.GetDataTableFromList();

        //    // con.Open();
        //    string values;
        //    string tableName = "cells3";
        //    SQLiteCommand cmdInsert = _SQLiteConnection.CreateCommand();

        //    // il faut creer cells1
        //    string sql = "CREATE TABLE [" + tableName + "] (";
        //    for (int i = 0; i < TableToSave.Columns.Count; i++)
        //    {
        //        sql += TableToSave.Columns[i].ColumnName;
        //        if (i + 1 == TableToSave.Columns.Count)
        //        {
        //            //Here we decide should we close insert command or appebd another create column command
        //            //  sql += " " + GetColumnType(TableToSave.Columns[i]) + ")"; //Close insert
        //        }
        //        else
        //        {
        //            if (TableToSave.Columns[i].ColumnName.Equals("Id"))
        //            {
        //                sql += " decimal IDENTITY(1,1) PRIMARY KEY,"; //there is more columns to add
        //            }
        //            else
        //            {
        //                //     sql += " " + GetColumnType(TableToSave.Columns[i]) + ","; //there is more columns to add
        //            }
        //        }
        //    }

        //    if (!String.IsNullOrEmpty(sql))
        //    {
        //        cmdInsert.CommandText = sql;
        //        cmdInsert.ExecuteNonQuery();
        //    }


        //    foreach (DataRow row in TableToSave.Rows)
        //    {
        //        values = "(";
        //        for (int i = 0; i < TableToSave.Columns.Count; i++)
        //        {
        //            if (i + 1 == TableToSave.Columns.Count)
        //            {
        //                if (TableToSave.Columns[i].DataType == System.Type.GetType("System.Decimal") ||
        //                        TableToSave.Columns[i].DataType == System.Type.GetType("System.Int64") ||
        //                        TableToSave.Columns[i].DataType == System.Type.GetType("System.Int32") ||
        //                        TableToSave.Columns[i].DataType == System.Type.GetType("System.Double") ||
        //                        TableToSave.Columns[i].DataType == System.Type.GetType("System.Single"))
        //                    values += (String.IsNullOrEmpty(row[i].ToString()) || Double.IsNaN(Convert.ToDouble(row[i]))) ? "0)" : Convert.ToDouble(row[i]).ToString() + ")";
        //                else if (TableToSave.Columns[i].DataType == System.Type.GetType("System.String"))
        //                    values += "'" + row[i] + "'";
        //                else
        //                    values += "'" + System.Security.SecurityElement.Escape(row[i].ToString()) + "')";
        //            }
        //            else
        //            {
        //                if (TableToSave.Columns[i].DataType == System.Type.GetType("System.Decimal") ||
        //                        TableToSave.Columns[i].DataType == System.Type.GetType("System.Int64") ||
        //                        TableToSave.Columns[i].DataType == System.Type.GetType("System.Int32") ||
        //                        TableToSave.Columns[i].DataType == System.Type.GetType("System.Double") ||
        //                        TableToSave.Columns[i].DataType == System.Type.GetType("System.String") ||
        //                        TableToSave.Columns[i].DataType == System.Type.GetType("System.Single"))
        //                    values += (String.IsNullOrEmpty(row[i].ToString()) || Double.IsNaN(Convert.ToDouble(row[i]))) ? "0," : Convert.ToDouble(row[i]).ToString() + ",";
        //                else
        //                    values += "'" + System.Security.SecurityElement.Escape(row[i].ToString()) + "',";
        //            }
        //        }
        //        values += ")";
        //        cmdInsert.CommandText = String.Format("Insert into [{0}] VALUES {1}", tableName, values);
        //        cmdInsert.ExecuteNonQuery();
        //    }


        //    cmdInsert.CommandText = "SELECT * FROM cells1";

        //    SQLiteDataReader value = cmdInsert.ExecuteReader();
        //    // int toto = mycommand.ExecuteNonQuery();
        //    //TableToSave.Load(
        //    //            adapter.Lo();

        //    //  adapter.Update(TableToSave);
        //    _SQLiteConnection.Close();

        //}

    }
}
