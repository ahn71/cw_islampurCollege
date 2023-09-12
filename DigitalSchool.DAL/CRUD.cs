using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DS.SysErrMsgHandler;


namespace DS.DAL
{
    public static class CRUD
    {
        private static SqlConnection conn;
        private static SqlConnection _conn;
        private static SqlCommand cmd;
        private static SqlDataAdapter adp;
        private static DataTable dt;

        //Getting Max ID or simple ID from Any Table
        public static int GetMaxID(string sql)
        {
            int maxValue = 0;
            try
            {
                conn = DbConnection.Connection;
                cmd = new SqlCommand(sql, conn);
                maxValue = Convert.ToInt32(cmd.ExecuteScalar());

                return maxValue;
            }
            catch (Exception ex)
            {


                return maxValue;
            }
            finally
            {
                DbConnection.Connection = conn;
            }
        }

        //checking query returns row or not?
        public static int ReturnRow(string sql)
        {
            try
            {
                conn = DbConnection.Connection;
                cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);            
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                
                return 0;
                
            }
            finally
            {             
                DbConnection.Connection = conn;
            }
        }
        

        //method query execution code
        public static Boolean ExecuteQuery(string sql)
        {
            bool result = true;
            try
            {                
                conn = DbConnection.Connection;
                cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                
                return result;
            }
            catch (Exception ex)
            {
                return result = false;              
            }
            finally
            {              
                DbConnection.Connection = conn;
            }
        }

        public static Boolean ExecuteQueryForAPI(string sql)
        {
            bool result = true;
            try
            {
                _conn = DBConnectionStatic.Connection;
                cmd = new SqlCommand(sql, _conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                return result;
            }
            catch (Exception ex)
            {
                return result = false;
            }
            finally
            {
              DBConnectionStatic.Connection = _conn;
            }
        }

        //Method for Return DataTable 
        public static DataTable ReturnTableNull(string sql)
        {
            try
            {
                conn = DbConnection.Connection;
                cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                adp = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 0;
                dt = new DataTable();
                adp.Fill(dt);              
                
                return dt;
            }
            catch (Exception ex)
            {
               
                return dt = null;               
            }
            finally
            {
                
                DbConnection.Connection = conn;
            }
        }
        public static bool ExecuteNonQuery(string sql) 
        {
            try
            {
                conn = DbConnection.Connection;
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
               
                return true;
            }
            catch { return false; }
        }
        public static bool BulkInsert(DataTable dt, string TableName)
        {
            bool result = true;
            try
            {
                conn = DbConnection.Connection;
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                {
                    bulkCopy.DestinationTableName = TableName;
                    bulkCopy.WriteToServer(dt);
                }
                
            
                return result;
            }
            catch(Exception ex)
            {
                
                return result = false;
             
            }
            finally
            {
                DbConnection.Connection = conn;
            }
        }


        public static bool ExecuteNonQuerys(string sql)
        {
            try
            {
                conn = DbConnection.Connection;
                return true;
            }
            catch { return false; }
        }
    }
}
