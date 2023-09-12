using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using adviitRuntimeScripting;
namespace DS.Admin
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (Session["__username__"] == null) Response.Redirect("/Admin/AdminLogin.aspx"); //Check valid login
                lblUsername.Text = Session["__username__"].ToString();
            }
            catch { }
        }

       

    }
}