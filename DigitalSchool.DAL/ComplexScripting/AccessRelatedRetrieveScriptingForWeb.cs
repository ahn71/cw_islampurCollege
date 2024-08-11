using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DS.DAL.ComplexScripting
{
    public class AccessRelatedRetrieveScriptingForWeb
    {
        public static void RetrieveALLFilds(string getTableName, OleDbConnection getConnection, GridView getGridView)
        {
            try
            {
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("select * from " + getTableName, getConnection);
                DataTable dataTable = new DataTable();
                ((DbDataAdapter)oleDbDataAdapter).Fill(dataTable);
                getConnection.Open();
                getGridView.DataSource = (object)dataTable;
                getGridView.DataBind();
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

        public static void RetrieveSpecificFilds(string getStatement, string getTableName, OleDbConnection getConnection, GridView getGridView)
        {
            try
            {
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("select " + getStatement + " from " + getTableName, getConnection);
                DataTable dataTable = new DataTable();
                ((DbDataAdapter)oleDbDataAdapter).Fill(dataTable);
                getConnection.Open();
                getGridView.DataSource = (object)dataTable;
                getGridView.DataBind();
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

        public static void RetrieveSearchingFilds(string getStatement, OleDbConnection getConnection, GridView getGridView)
        {
            try
            {
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(getStatement, getConnection);
                DataTable dataTable = new DataTable();
                ((DbDataAdapter)oleDbDataAdapter).Fill(dataTable);
                getConnection.Open();
                getGridView.DataSource = (object)dataTable;
                getGridView.DataBind();
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

        public static void RetrieveSearchingFilds(string getStatement, OleDbConnection getConnection, DetailsView getDetailsView)
        {
            try
            {
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(getStatement, getConnection);
                DataTable dataTable = new DataTable();
                ((DbDataAdapter)oleDbDataAdapter).Fill(dataTable);
                getConnection.Open();
                getDetailsView.DataSource = (object)dataTable;
                getDetailsView.DataBind();
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

        public static DataTable RetrieveReturnALLFilds(string getTableName, OleDbConnection getConnection)
        {
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("select * from " + getTableName, getConnection);
            DataTable dataTable = new DataTable();
            ((DbDataAdapter)oleDbDataAdapter).Fill(dataTable);
            return dataTable;
        }

        public static DataTable RetrieveReturnSpecificFilds(string getStatement, string getTableName, OleDbConnection getConnection)
        {
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("select " + getStatement + " from " + getTableName, getConnection);
            DataTable dataTable = new DataTable();
            ((DbDataAdapter)oleDbDataAdapter).Fill(dataTable);
            return dataTable;
        }

        public static DataTable RetrieveReturnSearchingFilds(string getStatement, OleDbConnection getConnection)
        {
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(getStatement, getConnection);
            DataTable dataTable = new DataTable();
            ((DbDataAdapter)oleDbDataAdapter).Fill(dataTable);
            return dataTable;
        }
    }
}
