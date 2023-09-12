using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DS.DAL.ComplexScripting
{
    public class MSAOperation
    {
        public static OleDbDataAdapter da;
        public static OleDbDataReader dr;
        public static OleDbCommand cmd;

        public static bool forSaveValue(string tableName, string[] setColumns, string[] setValues, OleDbConnection setConnection)
        {
            byte num = (byte)0;
            string str1 = "";
            string str2 = "";
            for (; (int)num < setColumns.Length; ++num)
            {
                if ((int)num == setColumns.Length - 1)
                {
                    str2 = str2 + "'" + setValues[(int)num] + "'";
                    str1 = str1 + setColumns[(int)num];
                }
                else
                {
                    str2 = str2 + "'" + setValues[(int)num] + "',";
                    str1 = str1 + setColumns[(int)num] + ",";
                }
            }
            MSAOperation.cmd = new OleDbCommand("insert into " + tableName + " (" + str1 + ") values (" + str2 + ")", setConnection);
            MSAOperation.cmd.ExecuteNonQuery();
            return true;
        }

        public static bool forUpdateValueByTrustColumns(string tableName, string[] setColumns, string[] setValues, string[] setSeparator, string[] setSeperatorValue, OleDbConnection setConnection)
        {
            byte num = (byte)0;
            string str = "";
            for (; (int)num < setColumns.Length; ++num)
            {
                if ((int)num == setColumns.Length - 1)
                    str = str + setColumns[(int)num] + "='" + setValues[(int)num] + "'";
                else
                    str = str + setColumns[(int)num] + "='" + setValues[(int)num] + "',";
            }
            MSAOperation.cmd = new OleDbCommand("Update " + tableName + " set " + str + " where " + setSeparator[0] + "=" + setSeperatorValue[0], setConnection);
            MSAOperation.cmd.ExecuteNonQuery();
            return true;
        }

        public static bool forUpdateValue(string tableName, string[] setColumns, string[] setValues, string setSeparator, string setSeperatorValue, OleDbConnection setConnection)
        {
            byte num = (byte)0;
            string str = "";
            for (; (int)num < setColumns.Length; ++num)
            {
                if ((int)num == setColumns.Length - 1)
                    str = str + setColumns[(int)num] + "='" + setValues[(int)num] + "'";
                else
                    str = str + setColumns[(int)num] + "='" + setValues[(int)num] + "',";
            }
            MSAOperation.cmd = new OleDbCommand("Update " + tableName + " set " + str + " where " + setSeparator + "=" + setSeperatorValue, setConnection);
            MSAOperation.cmd.ExecuteNonQuery();
            return true;
        }

        public static bool forDelete(string tableName, OleDbConnection setConnection)
        {
            MSAOperation.cmd = new OleDbCommand("delete from " + tableName, setConnection);
            MSAOperation.cmd.ExecuteNonQuery();
            return true;
        }

        public static bool forDeleteRecordByIdentifier(string tableName, string trustColumn, string trustColumnValue, OleDbConnection setConnection)
        {
            MSAOperation.cmd = new OleDbCommand("delete from " + tableName + " where " + trustColumn + " =" + trustColumnValue, setConnection);
            MSAOperation.cmd.ExecuteNonQuery();
            return true;
        }

        public static DataTable seletctAll(string tableName, OleDbConnection setConnection)
        {
            DataTable dataTable = new DataTable();
            MSAOperation.da = new OleDbDataAdapter("select * from " + tableName, setConnection);
            ((DbDataAdapter)MSAOperation.da).Fill(dataTable);
            return dataTable;
        }

        public static DataTable forSeletctByIdentifier(string tableName, string[] setColumns, string identifierName, string identifierValue, OleDbConnection setConnection)
        {
            DataTable dataTable = new DataTable();
            byte num = (byte)0;
            string str = "";
            for (; (int)num < setColumns.Length; ++num)
                str = (int)num != setColumns.Length - 1 ? str + setColumns[(int)num] + "," : str + setColumns[(int)num];
            MSAOperation.da = new OleDbDataAdapter("select " + str + " from " + tableName + " where  " + identifierName + " =" + identifierValue, setConnection);
            ((DbDataAdapter)MSAOperation.da).Fill(dataTable);
            return dataTable;
        }
    }
}
