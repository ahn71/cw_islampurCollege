using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adviitRuntimeScripting;
using System.Data;
using System.Data.SqlClient;
namespace DS.Admin
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (isLogin() == true) Response.Redirect("/Admin/Default.aspx");
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

                sqlDB.fillDataTable("select UserId,Username from useraccount  where UserName=@Username and UserPassword=@UserPassword", dt);


                if (dt.Rows.Count == 0)
                {
                    lblMessage.InnerText = "warning->Username or Password Invalid";
                    return false;
                }

                if (dt.Rows[0]["Username"].ToString().CompareTo(txtUsername.Text.Trim()) == 0)
                {
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