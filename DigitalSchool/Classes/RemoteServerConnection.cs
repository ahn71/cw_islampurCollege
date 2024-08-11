using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DS.Classes
{
    public class RemoteServerConnection
    {
       public static SqlConnection remote_con = new SqlConnection();

        public static void GetRemoteServerConnection()
        {
            try
            {
                HttpCookie getCookies = HttpContext.Current.Request.Cookies["userInfo"];
                string CSN = getCookies["__CompanyShortName__"].ToString();

                if (!remote_con.State.ToString().Trim().Equals("Closed")) remote_con.Close();            
                switch (CSN)
                {
                    case "DSCC":
                        remote_con.ConnectionString = WebConfigurationManager.ConnectionStrings["remote_DSCC"].ConnectionString;
                        remote_con.Open();
                        break;
                    case "DMGH":
                        remote_con.ConnectionString = WebConfigurationManager.ConnectionStrings["remote_DMGH"].ConnectionString;
                        remote_con.Open();
                        break;
                    case "DMSH":
                        remote_con.ConnectionString = WebConfigurationManager.ConnectionStrings["remote_DMSH"].ConnectionString;
                        remote_con.Open();
                        break;
                    case "NMSH":
                        remote_con.ConnectionString = WebConfigurationManager.ConnectionStrings["remote_NMSH"].ConnectionString;
                        remote_con.Open();
                        break;
                }                                  
            }
            catch
            {
                remote_con.Close();
            }
        }
    }
}