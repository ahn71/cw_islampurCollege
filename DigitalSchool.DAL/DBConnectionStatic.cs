using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DS.DAL
{
    class DBConnectionStatic
    {
        //static string _ConnectionString = "Data Source=DESKTOP-KHK1784;Initial Catalog=cw_islampurCollege;Persist Security Info=True;User ID=sa;Password=123; timeout=20";
        static string _ConnectionString = "Data Source=localhost;Initial Catalog=cw_islampurCollege;Persist Security Info=True;User ID=cw_admin;Password=@CW#2018; timeout=20";
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
