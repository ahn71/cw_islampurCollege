using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DS.DAL.ComplexScripting
{
    public static class sqlRelatedScriptingForWeb
    {
        public static SqlConnection cont;

        public static void ConnectionWithWebServer()
        {
            sqlRelatedScriptingForWeb.cont = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString);
            sqlRelatedScriptingForWeb.cont.Open();
        }
    }
}
