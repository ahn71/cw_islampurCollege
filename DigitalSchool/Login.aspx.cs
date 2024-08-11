using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DS.DAL.AdviitDAL;


namespace DS.Forms
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {          
      
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (isLogin() == true) Response.Redirect("~/Default.aspx");
        }

        private Boolean isLogin()
        {
            try
            {
                Session["__username__"] = "";

                DataTable dt = new DataTable();
                SqlParameter[] parms = {
                    new SqlParameter("@Username", txtUsername.Text.Trim()),
                    new SqlParameter("@UserPassword",  adviitSecurity.crypto(txtPassword.Text.Trim(), true)) 
                };               
                sqlDB.fillDataTable("select UserId,UserType,Username from useraccount  where " + 
                                    " UserName=@Username and UserPassword=@UserPassword", parms, dt);               

                if (dt.Rows.Count == 0) {
                    lblMessage.InnerText = "warning->Username or Password Invalid";
                    return false;
                }

                if (dt.Rows[0]["Username"].ToString().CompareTo(txtUsername.Text.Trim()) == 0)
                {
                    Session["__UserId__"] = dt.Rows[0]["UserId"].ToString();
                    Session["__UserType__"] = dt.Rows[0]["UserType"].ToString();
                    Session["__username__"] = dt.Rows[0]["Username"].ToString();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }        
    }
}