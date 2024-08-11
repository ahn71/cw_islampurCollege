using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DS.DAL
{    
    public static class DbConnection
    {
        static string _ConnectionString = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
        static SqlConnection _Connection = null;
        

        public static SqlConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    _Connection = new SqlConnection(_ConnectionString);
                    _Connection.Open();

                    return _Connection;
                }
                else if (_Connection.State != ConnectionState.Open)
                {
                    _Connection = new SqlConnection(_ConnectionString);
                    _Connection.Open();
                    return _Connection;
                   
                }
                else
                {
                    _Connection.Close();
                    _Connection.Dispose();
                    _Connection = new SqlConnection(_ConnectionString);
                    _Connection.Open();
                    return _Connection;
                }
            }
            set 
            {
                if (_Connection.State == ConnectionState.Open)
                {
                    _Connection.Close();
                    _Connection.Dispose();
                }
            }
        }
    }
}
