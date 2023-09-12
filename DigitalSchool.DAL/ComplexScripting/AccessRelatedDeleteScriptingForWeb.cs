using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DS.DAL.ComplexScripting
{
    public class AccessRelatedDeleteScriptingForWeb
    {
        public static void DeleteSpecificRows(string getStatement, OleDbConnection getConnection)
        {
            try
            {
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
                oleDbDataAdapter.DeleteCommand = new OleDbCommand(getStatement, getConnection);
                getConnection.Open();
                oleDbDataAdapter.DeleteCommand.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            {
                if (getConnection.State == ConnectionState.Open)
                    getConnection.Close();
            }
        }

        public static void DeleteAllRows(string getTableName, OleDbConnection getConnection)
        {
            try
            {
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
                oleDbDataAdapter.DeleteCommand = new OleDbCommand("Delete from " + getTableName + " ", getConnection);
                getConnection.Open();
                oleDbDataAdapter.DeleteCommand.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            {
                if (getConnection.State == ConnectionState.Open)
                    getConnection.Close();
            }
        }
    }
}
