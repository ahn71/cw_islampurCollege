using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace DS.DAL.AdviitDAL
{
    public static class sqlDB
    {
        public static SqlConnection connection; 
        public static int displayGridItems = 15;
        public static string dbError = "";
        public static string projectName = "";
        public static int recordsFound = 0;
        public static string lastQuery = "";

        public static bool connectDB()
        {
            try
            {
                sqlDB.connection = DbConnection.Connection;
                return true;
            }
            catch (Exception ex)
            {
                sqlDB.dbError = DateTime.Now.ToString() + " : " + ex.Message;
                adviitScripting.writeFile(ref sqlDB.dbError, "c:\\logs.txt");
                return false;
            }
        }

        public static bool closeDB()
        {
            try
            {
                DbConnection.Connection = connection;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool executeProcedure(string ProcedureName)
        {
            try
            {
                sqlDB.connectDB();
                SqlCommand sqlCommand = new SqlCommand(ProcedureName, sqlDB.connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                sqlDB.dbError = ex.Message;
                return false;
            }
        }

        public static bool executeProcedureWithParameter(string ProcedureName, SqlParameter[] parms)
        {
            try
            {
                sqlDB.connectDB();
                SqlCommand sqlCommand = new SqlCommand(ProcedureName, sqlDB.connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddRange(parms);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                sqlDB.dbError = ex.Message;
                return false;
            }
        }

        public static bool ExecuteNonQuery(string sqlCmd, CommandType cmdType, SqlParameter[] parameters)
        {
            int num = 0;
            using (SqlCommand command = sqlDB.connection.CreateCommand())
            {
                command.CommandType = cmdType;
                command.CommandText = sqlCmd;
                command.Parameters.AddRange(parameters);
                try
                {
                    sqlDB.connectDB();
                    num = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    sqlDB.dbError = ex.Message;
                }
            }
            return num > 0;
        }

        public static bool executeCommand(string sqlCmd)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                sqlDB.lastQuery = sqlCmd;
                using (SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection))
                    sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                sqlDB.dbError = ex.Message;
                return false;
            }
        }

        public static DataSet fillDataSet(string sqlCmd)
        {
            DataSet dataSet = new DataSet();
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                sqlDB.lastQuery = sqlCmd;
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd, sqlDB.connection))
                    ((DataAdapter)sqlDataAdapter).Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return dataSet;
            }
        }

        public static DataSet fillDataSetUsingSP(string ProcedureName)
        {
            DataSet dataSet = new DataSet();
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (SqlCommand sqlCommand = new SqlCommand(ProcedureName, sqlDB.connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                    {
                        sqlDataAdapter.SelectCommand = sqlCommand;
                        ((DataAdapter)sqlDataAdapter).Fill(dataSet);
                    }
                }
                return dataSet;
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(ProcedureName, ex.Message, "");
                return dataSet;
            }
        }

        public static DataSet fillDataSetUsingSP(string ProcedureName, SqlParameter[] parameters)
        {
            DataSet dataSet = new DataSet();
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (SqlCommand sqlCommand = new SqlCommand(ProcedureName, sqlDB.connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddRange(parameters);
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                    {
                        sqlDataAdapter.SelectCommand = sqlCommand;
                        ((DataAdapter)sqlDataAdapter).Fill(dataSet);
                    }
                }
                return dataSet;
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(ProcedureName, ex.Message, "");
                return dataSet;
            }
        }

        public static string fillDataGrid(string sqlCmd, GridView grd)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                sqlDB.lastQuery = sqlCmd;
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    grd.DataSource = (object)dataTable;
                    grd.PageSize = sqlDB.displayGridItems;
                    grd.DataBind();
                    grd.UseAccessibleHeader = true;
                    grd.HeaderRow.TableSection = TableRowSection.TableHeader;
                    sqlDB.recordsFound = dataTable.Rows.Count;
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string fillGridUsingSP(string StoreProcedureName, GridView grd)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(StoreProcedureName, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    grd.DataSource = (object)dataTable;
                    grd.PageSize = sqlDB.displayGridItems;
                    grd.DataBind();
                    grd.UseAccessibleHeader = true;
                    grd.HeaderRow.TableSection = TableRowSection.TableHeader;
                    sqlDB.recordsFound = dataTable.Rows.Count;
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure("Store procedure : " + StoreProcedureName, ex.Message, "");
                return ex.Message;
            }
        }

        public static string fillGridUsingSP(string StoreProcedureName, SqlParameter[] parameters, GridView grd)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(StoreProcedureName, sqlDB.connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddRange(parameters);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    grd.DataSource = (object)dataTable;
                    grd.PageSize = sqlDB.displayGridItems;
                    grd.DataBind();
                    grd.UseAccessibleHeader = true;
                    grd.HeaderRow.TableSection = TableRowSection.TableHeader;
                    sqlDB.recordsFound = dataTable.Rows.Count;
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(StoreProcedureName, ex.Message, "");
                return ex.Message;
            }
        }

        public static string fillDataTable(string sqlCmd, DataTable dt)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                sqlDB.lastQuery = sqlCmd;
                SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                sqlCommand.CommandTimeout = 300;
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    dt.Load((IDataReader)sqlDataReader);
                sqlDB.recordsFound = dt.Rows.Count;
                sqlCommand.Dispose();
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string fillDataTable(string sqlCmd, SqlParameter[] parameters, DataTable dt)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                sqlDB.lastQuery = sqlCmd;
                SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                sqlCommand.Parameters.AddRange(parameters);
                sqlCommand.CommandTimeout = 300;
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    dt.Load((IDataReader)sqlDataReader);
                sqlDB.recordsFound = dt.Rows.Count;
                sqlCommand.Dispose();
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string fillDataTableUsingSP(string StoreProcedureName, SqlParameter[] parameters, DataTable dt)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (SqlCommand sqlCommand = new SqlCommand(StoreProcedureName, sqlDB.connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddRange(parameters);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dt.Load((IDataReader)sqlDataReader);
                    sqlDB.recordsFound = dt.Rows.Count;
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(StoreProcedureName, ex.Message, "");
                return ex.Message;
            }
        }

        public static string fillDataTableUsingSP(string StoreProcedureName, DataTable dt)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (SqlCommand sqlCommand = new SqlCommand(StoreProcedureName, sqlDB.connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dt.Load((IDataReader)sqlDataReader);
                    sqlDB.recordsFound = dt.Rows.Count;
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(StoreProcedureName, ex.Message, "");
                return ex.Message;
            }
        }

        public static string bindDropDownList(string sqlCmd, string DataMember, DropDownList dl)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                sqlDB.lastQuery = sqlCmd;
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    dl.DataSource = (object)dataTable;
                    dl.DataValueField = DataMember;
                    dl.DataBind();
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string bindDropDownList(string sqlCmd, string ValueField, string TextField, DropDownList dl)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                sqlDB.lastQuery = sqlCmd;
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    dl.DataSource = (object)dataTable;
                    dl.DataValueField = ValueField;
                    dl.DataTextField = TextField;
                    dl.DataBind();
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string loadDropDownList(string sqlCmd, DropDownList dl)
        {
            try
            {
                sqlDB.lastQuery = sqlCmd;
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    if (dataTable.Columns.Count > 1)
                    {
                        for (int index = 0; index < dataTable.Rows.Count; ++index)
                            dl.Items.Add(new ListItem(dataTable.Rows[index].ItemArray[1].ToString(), dataTable.Rows[index].ItemArray[0].ToString().ToUpper()));
                    }
                    else
                    {
                        for (int index = 0; index < dataTable.Rows.Count; ++index)
                            dl.Items.Add(dataTable.Rows[index].ItemArray[0].ToString());
                    }
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }
        public static string loadDropDownList(string sqlCmd, DropDownList dl, DropDownList dl2)
        {
            try
            {
                sqlDB.lastQuery = sqlCmd;
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    if (dataTable.Columns.Count > 1)
                    {
                        for (int index = 0; index < dataTable.Rows.Count; ++index)
                        {
                            dl.Items.Add(new ListItem(dataTable.Rows[index].ItemArray[1].ToString(), dataTable.Rows[index].ItemArray[0].ToString()));
                            dl2.Items.Add(new ListItem(dataTable.Rows[index].ItemArray[1].ToString(), dataTable.Rows[index].ItemArray[0].ToString()));
                        }
                    }
                    else
                    {
                        for (int index = 0; index < dataTable.Rows.Count; ++index)
                        {
                            dl.Items.Add(dataTable.Rows[index].ItemArray[0].ToString());
                            dl2.Items.Add(dataTable.Rows[index].ItemArray[0].ToString());
                        }
                    }
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string loadDropDownList(string sqlCmd, DropDownList dl, DropDownList dl2, DropDownList dl3)
        {
            try
            {
                sqlDB.lastQuery = sqlCmd;
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    if (dataTable.Columns.Count > 1)
                    {
                        for (int index = 0; index < dataTable.Rows.Count; ++index)
                        {
                            dl.Items.Add(new ListItem(dataTable.Rows[index].ItemArray[1].ToString(), dataTable.Rows[index].ItemArray[0].ToString()));
                            dl2.Items.Add(new ListItem(dataTable.Rows[index].ItemArray[1].ToString(), dataTable.Rows[index].ItemArray[0].ToString()));
                            dl3.Items.Add(new ListItem(dataTable.Rows[index].ItemArray[1].ToString(), dataTable.Rows[index].ItemArray[0].ToString()));
                        }
                    }
                    else
                    {
                        for (int index = 0; index < dataTable.Rows.Count; ++index)
                        {
                            dl.Items.Add(dataTable.Rows[index].ItemArray[0].ToString());
                            dl2.Items.Add(dataTable.Rows[index].ItemArray[0].ToString());
                            dl3.Items.Add(dataTable.Rows[index].ItemArray[0].ToString());
                        }
                    }
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string loadDropDownList(string sqlCmd, string ValueFieldName, string TextFieldName, DropDownList dl)
        {
            try
            {
                sqlDB.lastQuery = sqlCmd;
                int index1 = 0;
                int index2 = 0;
                ValueFieldName = ValueFieldName.Trim().ToLower();
                TextFieldName = TextFieldName.Trim().ToLower();
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    for (int index3 = 0; index3 < dataTable.Columns.Count; ++index3)
                    {
                        if (dataTable.Columns[index3].ColumnName.ToLower().CompareTo(ValueFieldName) == 0)
                            index1 = index3;
                        else if (dataTable.Columns[index3].ColumnName.ToLower().CompareTo(TextFieldName) == 0)
                            index2 = index3;
                    }
                    for (int index3 = 0; index3 < dataTable.Rows.Count; ++index3)
                        dl.Items.Add(new ListItem(dataTable.Rows[index3].ItemArray[index2].ToString(), dataTable.Rows[index3].ItemArray[index1].ToString()));
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string loadDropDownList(string sqlCmd, string ValueFieldName, string TextFieldName, DropDownList dl, DropDownList dl2)
        {
            try
            {
                sqlDB.lastQuery = sqlCmd;
                int index1 = 0;
                int index2 = 0;
                ValueFieldName = ValueFieldName.Trim().ToLower();
                TextFieldName = TextFieldName.Trim().ToLower();
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    for (int index3 = 0; index3 < dataTable.Columns.Count; ++index3)
                    {
                        if (dataTable.Columns[index3].ColumnName.ToLower().CompareTo(ValueFieldName) == 0)
                            index1 = index3;
                        else if (dataTable.Columns[index3].ColumnName.ToLower().CompareTo(TextFieldName) == 0)
                            index2 = index3;
                    }
                    for (int index3 = 0; index3 < dataTable.Rows.Count; ++index3)
                    {
                        dl.Items.Add(new ListItem(dataTable.Rows[index3].ItemArray[index2].ToString(), dataTable.Rows[index3].ItemArray[index1].ToString()));
                        dl2.Items.Add(new ListItem(dataTable.Rows[index3].ItemArray[index2].ToString(), dataTable.Rows[index3].ItemArray[index1].ToString()));
                    }
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string loadDropDownList(string sqlCmd, string ValueFieldName, string TextFieldName, DropDownList dl, DropDownList dl2, DropDownList dl3)
        {
            try
            {
                sqlDB.lastQuery = sqlCmd;
                int index1 = 0;
                int index2 = 0;
                ValueFieldName = ValueFieldName.Trim().ToLower();
                TextFieldName = TextFieldName.Trim().ToLower();
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    for (int index3 = 0; index3 < dataTable.Columns.Count; ++index3)
                    {
                        if (dataTable.Columns[index3].ColumnName.ToLower().CompareTo(ValueFieldName) == 0)
                            index1 = index3;
                        else if (dataTable.Columns[index3].ColumnName.ToLower().CompareTo(TextFieldName) == 0)
                            index2 = index3;
                    }
                    for (int index3 = 0; index3 < dataTable.Rows.Count; ++index3)
                    {
                        dl.Items.Add(new ListItem(dataTable.Rows[index3].ItemArray[index2].ToString(), dataTable.Rows[index3].ItemArray[index1].ToString()));
                        dl2.Items.Add(new ListItem(dataTable.Rows[index3].ItemArray[index2].ToString(), dataTable.Rows[index3].ItemArray[index1].ToString()));
                        dl3.Items.Add(new ListItem(dataTable.Rows[index3].ItemArray[index2].ToString(), dataTable.Rows[index3].ItemArray[index1].ToString()));
                    }
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string loadDropDownFromDT(DataTable dt, string ValueFieldName, string TextFieldName, DropDownList dl)
        {
            try
            {
                int index1 = 0;
                int index2 = 0;
                ValueFieldName = ValueFieldName.Trim().ToLower();
                TextFieldName = TextFieldName.Trim().ToLower();
                for (int index3 = 0; index3 < dt.Columns.Count; ++index3)
                {
                    if (dt.Columns[index3].ColumnName.ToLower().CompareTo(ValueFieldName) == 0)
                        index1 = index3;
                    else if (dt.Columns[index3].ColumnName.ToLower().CompareTo(TextFieldName) == 0)
                        index2 = index3;
                }
                for (int index3 = 0; index3 < dt.Rows.Count; ++index3)
                    dl.Items.Add(new ListItem(dt.Rows[index3].ItemArray[index2].ToString(), dt.Rows[index3].ItemArray[index1].ToString()));
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string loadDropDownFromDT(DataTable dt, string ValueFieldName, string TextFieldName, DropDownList dl1, DropDownList dl2)
        {
            try
            {
                int index1 = 0;
                int index2 = 0;
                ValueFieldName = ValueFieldName.Trim().ToLower();
                TextFieldName = TextFieldName.Trim().ToLower();
                for (int index3 = 0; index3 < dt.Columns.Count; ++index3)
                {
                    if (dt.Columns[index3].ColumnName.ToLower().CompareTo(ValueFieldName) == 0)
                        index1 = index3;
                    else if (dt.Columns[index3].ColumnName.ToLower().CompareTo(TextFieldName) == 0)
                        index2 = index3;
                }
                for (int index3 = 0; index3 < dt.Rows.Count; ++index3)
                {
                    dl1.Items.Add(new ListItem(dt.Rows[index3].ItemArray[index2].ToString(), dt.Rows[index3].ItemArray[index1].ToString()));
                    dl2.Items.Add(new ListItem(dt.Rows[index3].ItemArray[index2].ToString(), dt.Rows[index3].ItemArray[index1].ToString()));
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string loadCheckListFromDT(DataTable dt, string ValueFieldName, string TextFieldName, CheckBoxList lst)
        {
            try
            {
                int index1 = 0;
                int index2 = 0;
                ValueFieldName = ValueFieldName.Trim().ToLower();
                TextFieldName = TextFieldName.Trim().ToLower();
                for (int index3 = 0; index3 < dt.Columns.Count; ++index3)
                {
                    if (dt.Columns[index3].ColumnName.ToLower().CompareTo(ValueFieldName) == 0)
                        index1 = index3;
                    else if (dt.Columns[index3].ColumnName.ToLower().CompareTo(TextFieldName) == 0)
                        index2 = index3;
                }
                for (int index3 = 0; index3 < dt.Rows.Count; ++index3)
                    lst.Items.Add(new ListItem(dt.Rows[index3].ItemArray[index2].ToString(), dt.Rows[index3].ItemArray[index1].ToString()));
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string loadDropDownListWithParameterSP(string sqlCmd, string ParameterName, string ParameterValue, DropDownList dl)
        {
            try
            {
                sqlDB.lastQuery = sqlCmd;
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    int index1 = 0;
                    using (SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = 300;
                        sqlCommand.Parameters.AddWithValue(ParameterName, (object)ParameterValue);
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                            dataTable.Load((IDataReader)sqlDataReader);
                    }
                    if (dataTable.Columns.Count > 1)
                        index1 = 1;
                    for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
                        dl.Items.Add(new ListItem(dataTable.Rows[index2].ItemArray[index1].ToString(), dataTable.Rows[index2].ItemArray[0].ToString()));
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string loadDropDownListFromArray(string[] values, DropDownList dl)
        {
            try
            {
                int num = 0;
                for (int index = 0; index < values.Length; ++index)
                {
                    dl.Items.Add(values[index]);
                    ++num;
                    if (num > 1000)
                        return "";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string loadDropDownListFromArray(string[] values, CheckBoxList lst)
        {
            try
            {
                int num = 0;
                for (int index = 0; index < values.Length; ++index)
                {
                    lst.Items.Add(values[index]);
                    ++num;
                    if (num > 1000)
                        return "";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string bindDropDown(string sqlCmd, string ValueFieldName, string TextFieldName, DropDownList dl)
        {
            try
            {
                sqlDB.lastQuery = sqlCmd;
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    dl.DataSource = (object)dataTable;
                    dl.DataValueField = ValueFieldName;
                    dl.DataTextField = TextFieldName;
                    dl.DataBind();
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string bindDropDownUsingSP(string storeProcedureName, string ValueFieldName, string TextFieldName, DropDownList dl)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(storeProcedureName, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    dl.DataSource = (object)dataTable;
                    dl.DataValueField = ValueFieldName;
                    dl.DataTextField = TextFieldName;
                    dl.DataBind();
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(storeProcedureName, ex.Message, "");
                return ex.Message;
            }
        }

        public static string loadDropDownUsingSP(string storeProcedureName, string ValueFieldName, string TextFieldName, DropDownList dl)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    int index1 = 0;
                    int index2 = 0;
                    SqlCommand sqlCommand = new SqlCommand(storeProcedureName, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        dataTable.Load((IDataReader)sqlDataReader);
                        ValueFieldName = ValueFieldName.ToLower();
                        TextFieldName = TextFieldName.ToLower();
                        for (int index3 = 0; index3 < dataTable.Columns.Count; ++index3)
                        {
                            if (dataTable.Columns[index3].ColumnName.ToLower().CompareTo(ValueFieldName) == 0)
                                index2 = index3;
                            else if (dataTable.Columns[index3].ColumnName.ToLower().CompareTo(TextFieldName) == 0)
                                index1 = index3;
                        }
                        for (int index3 = 0; index3 < dataTable.Rows.Count; ++index3)
                            dl.Items.Add(new ListItem(dataTable.Rows[index3].ItemArray[index1].ToString(), dataTable.Rows[index3].ItemArray[index2].ToString()));
                    }
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(storeProcedureName, ex.Message, "");
                return ex.Message;
            }
        }

        public static string bindDropDown(DataTable SourceDataTable, string ValueFieldName, string TextFieldName, DropDownList dl)
        {
            try
            {
                dl.DataSource = (object)SourceDataTable;
                dl.DataValueField = ValueFieldName;
                dl.DataTextField = TextFieldName;
                dl.DataBind();
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string loadList(string sqlCmd, ListBox li)
        {
            try
            {
                sqlDB.lastQuery = sqlCmd;
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    if (dataTable.Columns.Count > 1)
                    {
                        for (int index = 0; index < dataTable.Rows.Count; ++index)
                            li.Items.Add(new ListItem(dataTable.Rows[index].ItemArray[1].ToString(), dataTable.Rows[index].ItemArray[0].ToString()));
                    }
                    else
                    {
                        for (int index = 0; index < dataTable.Rows.Count; ++index)
                            li.Items.Add(dataTable.Rows[index].ItemArray[0].ToString());
                    }
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string bindListBox(string sqlCmd, string DataMember, ListBox li)
        {
            try
            {
                sqlDB.lastQuery = sqlCmd;
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    li.DataSource = (object)dataTable;
                    li.DataValueField = DataMember;
                    li.DataBind();
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string bindListBox(string sqlCmd, string ValueField, string TextField, ListBox li)
        {
            try
            {
                sqlDB.lastQuery = sqlCmd;
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    li.DataSource = (object)dataTable;
                    li.DataValueField = ValueField;
                    li.DataTextField = TextField;
                    li.DataBind();
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string bindList(string sqlCmd, string ValueFieldName, string TextFieldName, ListBox li)
        {
            try
            {
                sqlDB.lastQuery = sqlCmd;
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    li.DataSource = (object)dataTable;
                    li.DataValueField = ValueFieldName;
                    li.DataTextField = TextFieldName;
                    li.DataBind();
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string bindCheckList(string sqlCmd, CheckBoxList li)
        {
            try
            {
                sqlDB.lastQuery = sqlCmd;
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    li.DataSource = (object)dataTable;
                    li.DataValueField = ((object)dataTable.Columns[0].ColumnName).ToString();
                    li.DataTextField = ((object)dataTable.Columns[1].ColumnName).ToString();
                    li.DataBind();
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string bindCheckList(string sqlCmd, string ValueFieldName, string TextFieldName, CheckBoxList li)
        {
            try
            {
                sqlDB.lastQuery = sqlCmd;
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    li.DataSource = (object)dataTable;
                    li.DataValueField = ValueFieldName;
                    li.DataTextField = TextFieldName;
                    li.DataBind();
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string bindCheckListBox(string sqlCmd, string DataMember, CheckBoxList li)
        {
            try
            {
                sqlDB.lastQuery = sqlCmd;
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection);
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    li.DataSource = (object)dataTable;
                    li.DataValueField = DataMember;
                    li.DataBind();
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string bindCheckListBoxUsingSP(string ProcedureName, CheckBoxList li)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(ProcedureName, sqlDB.connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    li.DataSource = (object)dataTable;
                    li.DataValueField = ((object)dataTable.Columns[0].ColumnName).ToString();
                    li.DataTextField = ((object)dataTable.Columns[1].ColumnName).ToString();
                    li.DataBind();
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(ProcedureName, ex.Message, "");
                return ex.Message;
            }
        }

        public static string bindCheckListBoxUsingSP(string ProcedureName, string ValueFieldName, string TextFieldName, CheckBoxList li)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                using (DataTable dataTable = new DataTable())
                {
                    SqlCommand sqlCommand = new SqlCommand(ProcedureName, sqlDB.connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 300;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        dataTable.Load((IDataReader)sqlDataReader);
                    li.DataSource = (object)dataTable;
                    li.DataValueField = ValueFieldName;
                    li.DataTextField = TextFieldName;
                    li.DataBind();
                    sqlCommand.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(ProcedureName, ex.Message, "");
                return ex.Message;
            }
        }

        public static string bindCheckListBox(DataTable SourceDataTable, string ValueField, string TextField, CheckBoxList li)
        {
            try
            {
                li.DataSource = (object)SourceDataTable;
                li.DataValueField = ValueField;
                li.DataTextField = TextField;
                li.DataBind();
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string getScalarValue(string sqlCmd, ref string getValue)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                sqlDB.lastQuery = sqlCmd;
                using (SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection))
                    getValue = sqlCommand.ExecuteScalar().ToString();
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return ex.Message;
            }
        }

        public static string getScalarValue(string sqlCmd)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                sqlDB.lastQuery = sqlCmd;
                using (SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlDB.connection))
                    return sqlCommand.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                sqlDB.saveSqlFaliure(sqlCmd, ex.Message, "");
                return "";
            }
        }

        public static void saveSqlFaliure(string SqlCmd, string ExceptionMessage, string Username)
        {
            try
            {
                sqlDB.executeProcedureWithParameter("saveSqlFailure", new SqlParameter[3]
                {
                  new SqlParameter("@SqlCmd", (object) SqlCmd),
                  new SqlParameter("@ExecptionMessage", (object) ExceptionMessage),
                  new SqlParameter("@Username", (object) Username)
                });
            }
            catch (Exception ex)
            {
            }
        }

        public static void getRowFromDatatable(ref DataTable dt, ref DataRow dr, string ColumnName, string Value)
        {
            try
            {
                int index1 = -1;
                ColumnName = ColumnName.ToLower();
                for (int index2 = 0; index2 < dt.Rows.Count; ++index2)
                {
                    if (((object)dt.Columns[index2].ColumnName).ToString().ToLower().CompareTo(ColumnName) == 0)
                    {
                        index1 = index2;
                        break;
                    }
                }
                if (index1 == -1)
                    return;
                for (int index2 = 0; index2 < dt.Rows.Count; ++index2)
                {
                    if (dt.Rows[index2].ItemArray[index1].ToString().CompareTo(Value) == 0)
                    {
                        dr = dt.Rows[index2];
                        break;
                    }
                }
            }
            catch
            {
            }
        }

        public static string deleteFromGrid(string sqlCmd, int deleteItemIndex, bool isFieldTypeString, GridView grv)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                CheckBox checkBox = new CheckBox();
                bool flag = false;
                if (((CheckBox)grv.HeaderRow.FindControl("chkHeader")).Checked)
                    flag = true;
                for (int index = 0; index < grv.Rows.Count; ++index)
                {
                    if (((CheckBox)grv.Rows[index].Cells[0].FindControl("chkRow")).Checked || flag)
                        (!isFieldTypeString ? (DbCommand)new SqlCommand(sqlCmd + grv.Rows[index].Cells[deleteItemIndex].Text, sqlDB.connection) : (DbCommand)new SqlCommand(sqlCmd + "'" + grv.Rows[index].Cells[deleteItemIndex].Text + "'", sqlDB.connection)).ExecuteNonQuery();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string deleteFromGrid(string sqlCmd, string firstFieldName, string secondFieldName, int deleteItemIndex1, int deleteItemIndex2, int queryMode, GridView grv)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                CheckBox checkBox = new CheckBox();
                bool flag = false;
                if (((CheckBox)grv.HeaderRow.FindControl("chkHeader")).Checked)
                    flag = true;
                for (int index = 0; index < grv.Rows.Count; ++index)
                {
                    if (((CheckBox)grv.Rows[index].Cells[0].FindControl("chkRow")).Checked || flag)
                    {
                        SqlCommand sqlCommand;
                        if (queryMode == 0)
                            sqlCommand = new SqlCommand(sqlCmd + " " + firstFieldName + "='" + grv.Rows[index].Cells[deleteItemIndex1].Text + "' and " + secondFieldName + "='" + grv.Rows[index].Cells[deleteItemIndex2].Text + "'", sqlDB.connection);
                        else if (queryMode == 1)
                            sqlCommand = new SqlCommand(sqlCmd + " " + firstFieldName + "=" + grv.Rows[index].Cells[deleteItemIndex1].Text + " and " + secondFieldName + "='" + grv.Rows[index].Cells[deleteItemIndex2].Text + "'", sqlDB.connection);
                        else
                            sqlCommand = new SqlCommand(sqlCmd + " " + firstFieldName + "=" + grv.Rows[index].Cells[deleteItemIndex1].Text + " and " + secondFieldName + "=" + grv.Rows[index].Cells[deleteItemIndex2].Text, sqlDB.connection);
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static bool isValidInputs(SqlCommand cmd)
        {
            try
            {
                for (int index = 0; index < cmd.Parameters.Count; ++index)
                {
                    if (!sqlDB.checkInput(cmd.Parameters[index].Value.ToString()))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static bool checkInput(string inputValue)
        {
            try
            {
                inputValue = inputValue.ToLower();
                return inputValue.IndexOf("<script>") <= 0 && Regex.Matches(inputValue, "<").Count <= 2;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string getCSVString(DataTable dt, bool RemoveComma)
        {
            try
            {
                string str1 = "";
                int count1 = dt.Columns.Count;
                for (int index = 0; index < count1; ++index)
                {
                    str1 = str1 + (object)dt.Columns[index];
                    if (index < count1 - 1)
                        str1 = str1 + ",";
                }
                string str2 = str1 + "\r\n";
                int count2 = dt.Rows.Count;
                if (RemoveComma)
                {
                    for (int index1 = 0; index1 < count2; ++index1)
                    {
                        for (int index2 = 0; index2 < count1; ++index2)
                        {
                            str2 = str2 + dt.Rows[index1].ItemArray[index2].ToString().Replace(',', ' ');
                            if (index2 < count1 - 1)
                                str2 = str2 + ",";
                        }
                        str2 = str2 + "\n\r";
                    }
                }
                else
                {
                    for (int index1 = 0; index1 < count2; ++index1)
                    {
                        for (int index2 = 0; index2 < count1; ++index2)
                        {
                            str2 = str2 + dt.Rows[index1].ItemArray[index2].ToString();
                            if (index2 < count1 - 1)
                                str2 = str2 + ",";
                        }
                        str2 = str2 + "\n\r";
                    }
                }
                return str2;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static void displayRecordFound(HtmlGenericControl span)
        {
            try
            {
                if (sqlDB.recordsFound == 200)
                    span.InnerText = "Found: " + (object)sqlDB.recordsFound + "+ records";
                else
                    span.InnerText = "Found: " + (object)sqlDB.recordsFound + " records";
            }
            catch
            {
            }
        }

        public static bool hasTableRows(string sqlCmd)
        {
            try
            {
                if (sqlDB.connection.State != ConnectionState.Open)
                    sqlDB.connectDB();
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);
                return dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static int getPrivilegeId(string[,] privlegeData, string currentPage)
        {
            try
            {
                currentPage = currentPage.ToLower();
                for (int index = 0; index < privlegeData.GetLength(0); ++index)
                {
                    string str = privlegeData[index, 0].ToLower();
                    if (str.CompareTo(currentPage) == 0 || str.IndexOf(currentPage) > -1)
                        return adviitScripting.valueInt(((object)privlegeData[index, 1]).ToString());
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static int getPrivilegeId(DataTable privlegeData, string currentPage)
        {
            try
            {
                currentPage = currentPage.ToLower();
                for (int index = 0; index < privlegeData.Rows.Count; ++index)
                {
                    string str = privlegeData.Rows[index].ItemArray[0].ToString().ToLower();
                    if (str.CompareTo(currentPage) == 0 || str.IndexOf(currentPage) > -1)
                        return adviitScripting.valueInt(privlegeData.Rows[index].ItemArray[1].ToString());
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static int getPrivilegeId(string privilegeData, string currentPage)
        {
            try
            {
                currentPage = currentPage.ToLower();
                int startIndex = privilegeData.IndexOf(currentPage);
                if (startIndex < 0)
                    return 0;
                string str = privilegeData.Substring(startIndex, privilegeData.IndexOf("!", startIndex) - startIndex);
                return int.Parse(str.Substring(str.LastIndexOf("|") + 1));
            }
            catch
            {
                return 0;
            }
        }

        public static string setErrorMessage(string OriginalErrorMessage)
        {
            try
            {
                OriginalErrorMessage = OriginalErrorMessage.ToLower();
                if (OriginalErrorMessage.IndexOf("Violation of PRIMARY KEY".ToLower()) > -1)
                    return "Duplicate data is not allowed.";
                else
                    return OriginalErrorMessage;
            }
            catch (Exception ex)
            {
                return OriginalErrorMessage;
            }
        }

        public static void fillNumberOrder(int UpperLevel, DropDownList dl)
        {
            try
            {
                string text = "";
                for (int index = 1; index <= UpperLevel; ++index)
                {
                    if (index == 1)
                        text = (string)(object)index + (object)"st";
                    else if (index == 2)
                        text = (string)(object)index + (object)"nd";
                    else if (index == 3)
                        text = (string)(object)index + (object)"rd";
                    else if (index >= 4 && index < 21)
                        text = (string)(object)index + (object)"th";
                    else if (index >= 21 && index % 10 == 1)
                        text = (string)(object)index + (object)"st";
                    else if (index >= 21 && index % 10 == 2)
                        text = (string)(object)index + (object)"nd";
                    else if (index >= 21 && index % 10 == 3)
                        text = (string)(object)index + (object)"rd";
                    else if (index >= 21 && index % 10 >= 4 || index % 10 == 0)
                        text = (string)(object)index + (object)"th";
                    dl.Items.Add(new ListItem(text, index.ToString()));
                }
            }
            catch
            {
            }
        }

        public static void fillNumberOrder(int UpperLevel, DropDownList dl1, DropDownList dl2)
        {
            try
            {
                string text = "";
                for (int index = 1; index <= UpperLevel; ++index)
                {
                    if (index == 1)
                        text = (string)(object)index + (object)"st";
                    else if (index == 2)
                        text = (string)(object)index + (object)"nd";
                    else if (index == 3)
                        text = (string)(object)index + (object)"rd";
                    else if (index >= 4 && index < 21)
                        text = (string)(object)index + (object)"th";
                    else if (index >= 21 && index % 10 == 1)
                        text = (string)(object)index + (object)"st";
                    else if (index >= 21 && index % 10 == 2)
                        text = (string)(object)index + (object)"nd";
                    else if (index >= 21 && index % 10 == 3)
                        text = (string)(object)index + (object)"rd";
                    else if (index >= 21 && index % 10 >= 4 || index % 10 == 0)
                        text = (string)(object)index + (object)"th";
                    dl1.Items.Add(new ListItem(text, index.ToString()));
                    dl2.Items.Add(new ListItem(text, index.ToString()));
                }
            }
            catch
            {
            }
        }

        public static bool changeNumberOrder(ref GridView dg, int columnIndex)
        {
            try
            {
                string str = "";
                int count = dg.Rows.Count;
                for (int index = 0; index < count; ++index)
                {
                    int num = adviitScripting.valueInt(dg.Rows[index].Cells[columnIndex].Text);
                    switch (num)
                    {
                        case 1:
                            str = (string)(object)num + (object)"st";
                            break;
                        case 2:
                            str = (string)(object)num + (object)"nd";
                            break;
                        case 3:
                            str = (string)(object)num + (object)"rd";
                            break;
                        default:
                            if (num >= 4 && num < 21)
                            {
                                str = (string)(object)num + (object)"th";
                                break;
                            }
                            else if (num >= 21 && num % 10 == 1)
                            {
                                str = (string)(object)num + (object)"st";
                                break;
                            }
                            else if (num >= 21 && num % 10 == 2)
                            {
                                str = (string)(object)num + (object)"nd";
                                break;
                            }
                            else if (num >= 21 && num % 10 == 3)
                            {
                                str = (string)(object)num + (object)"rd";
                                break;
                            }
                            else if (num >= 21 && num % 10 >= 4)
                            {
                                str = (string)(object)num + (object)"th";
                                break;
                            }
                            else
                                break;
                    }
                    dg.Rows[index].Cells[columnIndex].Text = str;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool changeNumberOrder(ref GridView dg, int columnIndex, int DefaultNumber, string DefaultValue)
        {
            try
            {
                string str = "";
                int count = dg.Rows.Count;
                for (int index = 0; index < count; ++index)
                {
                    int num = adviitScripting.valueInt(dg.Rows[index].Cells[columnIndex].Text);
                    if (num == 1)
                        str = (string)(object)num + (object)"st";
                    else if (num == DefaultNumber)
                        str = DefaultValue;
                    else if (num == 2)
                        str = (string)(object)num + (object)"nd";
                    else if (num == 3)
                        str = (string)(object)num + (object)"rd";
                    else if (num >= 4 && num < 21)
                        str = (string)(object)num + (object)"th";
                    else if (num >= 21 && num % 10 == 1)
                        str = (string)(object)num + (object)"st";
                    else if (num >= 21 && num % 10 == 2)
                        str = (string)(object)num + (object)"nd";
                    else if (num >= 21 && num % 10 == 3)
                        str = (string)(object)num + (object)"rd";
                    else if (num >= 21 && num % 10 >= 4)
                        str = (string)(object)num + (object)"th";
                    dg.Rows[index].Cells[columnIndex].Text = str;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string getNumberOrder(int Number)
        {
            try
            {
                if (Number == 1)
                    return (string)(object)Number + (object)"st";
                if (Number == 2)
                    return (string)(object)Number + (object)"nd";
                if (Number == 3)
                    return (string)(object)Number + (object)"rd";
                if (Number >= 4 && Number < 21)
                    return (string)(object)Number + (object)"th";
                if (Number >= 21 && Number % 10 == 1)
                    return (string)(object)Number + (object)"st";
                if (Number >= 21 && Number % 10 == 2)
                    return (string)(object)Number + (object)"nd";
                if (Number >= 21 && Number % 10 == 3)
                    return (string)(object)Number + (object)"rd";
                if (Number >= 21 && Number % 10 >= 4)
                    return (string)(object)Number + (object)"th";
                else
                    return Number.ToString();
            }
            catch
            {
                return Number.ToString();
            }
        }

        public static void selectItem(ref DropDownList dl, string value, bool compareValue)
        {
            try
            {
                if (value.Length == 0)
                    return;
                int count = dl.Items.Count;
                if (compareValue)
                {
                    for (int index = 0; index < count; ++index)
                    {
                        if (dl.Items[index].Value.CompareTo(value) == 0)
                        {
                            dl.Items[index].Selected = true;
                            break;
                        }
                    }
                }
                else
                {
                    if (compareValue)
                        return;
                    for (int index = 0; index < count; ++index)
                    {
                        if (dl.Items[index].Text.CompareTo(value) == 0)
                        {
                            dl.Items[index].Selected = true;
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}
