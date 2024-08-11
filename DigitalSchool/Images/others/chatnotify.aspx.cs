using adviitRuntimeScripting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Runtime.Remoting.Contexts;
using System.Net.NetworkInformation;
using DS.BLL;

namespace DS
{
    public partial class chatnotify : System.Web.UI.Page
    {

        
        protected void Page_Load(object sender, EventArgs e)
        {
            string getStatus = Request.QueryString["f"];
            if (!string.IsNullOrEmpty(getStatus))
            {
                signout(); // so=singout
                return;
            }
        }

        [WebMethod]
        public static string AttendanceSink(string ReceiverId)
        {
           
            try
            {
                TimeSpan CurrentTime = TimeSpan.Parse(TimeZoneBD.getCurrentTimeBD().Hour + ":" + TimeZoneBD.getCurrentTimeBD().Minute);
               
                TimeSpan LogInTime_1 = TimeSpan.Parse(11 + ":" + 11);
                TimeSpan LogOutTime_1 = TimeSpan.Parse(16 + ":" + 50);

                TimeSpan LogInTime_2 = TimeSpan.Parse(10 + ":" + 55);
                TimeSpan LogOutTime_2 = TimeSpan.Parse(10 + ":" + 57);

                TimeSpan LogInTime_3 = TimeSpan.Parse(11 + ":" + 20);
                TimeSpan LogOutTime_3 = TimeSpan.Parse(11 + ":" + 22);

                TimeSpan LogInTime_4 = TimeSpan.Parse(16 + ":" + 10);
                TimeSpan LogOutTime_4 = TimeSpan.Parse(16 + ":" + 12);

                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();               
                string Machine = nics[0].GetPhysicalAddress().ToString();


                if (CurrentTime >= LogInTime_1 && CurrentTime <= LogOutTime_1)
                {
                    SqlConnection local_con = new SqlConnection();
                    local_con.ConnectionString = WebConfigurationManager.ConnectionStrings["local"].ConnectionString;
                    local_con.Open();
                    //string userType = HttpContext.Current.Session["__GetUType"].ToString();
                    //if (HttpContext.Current.Session["__GetUType"].ToString().Trim().Equals("Master Admin"))
                    //{
                        SqlDataAdapter da = new SqlDataAdapter("select UserId from ShrinkInfo where Format(ShrinkDate,'dd-MM-yyyy')='" + Classes.ServerTimeZone.GetBangladeshNowDate() + "' AND Convert(time,ShrinkDate)>='11:11:00' AND Convert(time,ShrinkDate) <='16:50:59' ", local_con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0) return ""; // any user id exitst means already shrink are completed

                        else return "Start Shrinking";  // If execute this line then shrinking are started
                    //}
                    //else return "";
                }
                else if (CurrentTime >= LogInTime_2 && CurrentTime <= LogOutTime_2)
                {
                    SqlConnection local_con = new SqlConnection();
                    local_con.ConnectionString = WebConfigurationManager.ConnectionStrings["local"].ConnectionString;
                    local_con.Open();
                    //string userType = HttpContext.Current.Session["__GetUType"].ToString();
                    //if (HttpContext.Current.Session["__GetUType"].ToString().Trim().Equals("Master Admin") )
                    //{
                        SqlDataAdapter da = new SqlDataAdapter("select UserId from ShrinkInfo where Format(ShrinkDate,'dd-MM-yyyy')='" + Classes.ServerTimeZone.GetBangladeshNowDate() + "' AND Convert(time,ShrinkDate)>='10:55:00' AND Convert(time,ShrinkDate) <='10:57:59' ", local_con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                  
                        if (dt.Rows.Count > 0) return ""; // any user id exitst means already shrink are completed

                        else return "Start Shrinking";  // If execute this line then shrinking are started
                    //}
                    //else return "";
                }
                else if (CurrentTime >= LogInTime_3 && CurrentTime <= LogOutTime_3)
                {
                    SqlConnection local_con = new SqlConnection();
                    local_con.ConnectionString = WebConfigurationManager.ConnectionStrings["local"].ConnectionString;
                    local_con.Open();
                    //string userType = HttpContext.Current.Session["__GetUType"].ToString();
                    //if (HttpContext.Current.Session["__GetUType"].ToString().Trim().Equals("Master Admin") )
                    //{
                        SqlDataAdapter da = new SqlDataAdapter("select UserId from ShrinkInfo where Format(ShrinkDate,'dd-MM-yyyy')='" + Classes.ServerTimeZone.GetBangladeshNowDate() + "' AND Convert(time,ShrinkDate)>='11:20:00' AND Convert(time,ShrinkDate) <='11:22:59' ", local_con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0) return ""; // any user id exitst means already shrink are completed

                        else return "Start Shrinking";  // If execute this line then shrinking are started
                    //}
                    //else return "";
                }

                else if (CurrentTime >= LogInTime_4 && CurrentTime <= LogOutTime_4)
                {
                    SqlConnection local_con = new SqlConnection();
                    local_con.ConnectionString = WebConfigurationManager.ConnectionStrings["local"].ConnectionString;
                    local_con.Open();
                    //string userType = HttpContext.Current.Session["__GetUType"].ToString();
                    
                    //if (HttpContext.Current.Session["__GetUType"].ToString().Trim().Equals("Master Admin") && Machine.Equals("B083FE92CD17"))
                    //if (HttpContext.Current.Session["__GetUType"].ToString().Trim().Equals("Master Admin"))
                    //{
                        SqlDataAdapter da = new SqlDataAdapter("select UserId from ShrinkInfo where Format(ShrinkDate,'dd-MM-yyyy')='" + Classes.ServerTimeZone.GetBangladeshNowDate() + "' AND Convert(time,ShrinkDate)>='16:10:00' AND Convert(time,ShrinkDate) <='16:12:59' ", local_con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0) return ""; // any user id exitst means already shrink are completed

                        else return "Start Shrinking";  // If execute this line then shrinking are started
                    //}
                    //else return "";
                }

                return "";
               
            }
            catch (Exception ex)
            {
                return "";
            }
           
        }

        [WebMethod]
        public static string updateLoginDateTime(string ReceiverId)
        {
            try
            {
                string getUserId = HttpContext.Current.Session["__GetUID__"].ToString();

                string dateFormat = "dd-MM-yyyy hh:mm:ss";
                string datTime = TimeZoneBD.getCurrentTimeBD().ToString("dd-MM-yyyy hh:mm:ss tt");
                DateTime LoginDateTime = TimeZoneBD.getCurrentTimeBD();
                SqlCommand cmd = new SqlCommand("Update UserAccount Set IsLogin='1',LoginDateTime='" + LoginDateTime + "' where UserId=" + getUserId + "", sqlDB.connection);
                cmd.ExecuteNonQuery();

                return " ";
            }
            catch { return " "; }
        }

        public void signout()
        {
            try
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                FormsAuthentication.SignOut();
                Response.Redirect("~/ControlPanel/Login.aspx",true);

            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }
    }
}