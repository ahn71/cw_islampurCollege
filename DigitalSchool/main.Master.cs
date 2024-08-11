using DS.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS
{
    public partial class main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie getCookies = Request.Cookies["userInfoSchool"];
            if ( getCookies == null || getCookies.Value == "" || Session["__UserTypeId__"]==null)
            {
               Response.Redirect("~/"+Routing.LoginRouteUrl);
              //  Response.Redirect(GetRouteUrl("LoginRoute"));
            }
            //DateTime datetimeopen = Convert.ToDateTime("08/23/2017 11:59:59 PM");
            //DateTime datetimeopen = Convert.ToDateTime("23/08/2017 11:59:59 PM");
            //DateTime datetimeopen = DateTime.Parse("08/30/2018 11:59:59 PM"); // re-new
            // datetimeopen = datetimeopen.AddYears(1);
            // DateTime datetimeexpire = DateTime.Now;
            // if (datetimeopen < datetimeexpire)
            // {
            //     Response.Redirect("~/UserLogin.aspx");
            // }
            try
            {
                //DateTime datetimeopen = Convert.ToDateTime("08/30/2018 11:59:59 PM");
                //datetimeopen = datetimeopen.AddYears(1);
                //DateTime datetimeexpire = DateTime.Now;
                //if (datetimeopen < datetimeexpire)
                //{
                //    Response.Redirect("~/UserLogin.aspx");
                  
                //}
            }
            catch { }
            if (!IsPostBack)
            {
                Session["__UserId__"] = getCookies["__UserId__"].ToString();
                Session["__UserType__"] = getCookies["__UserType__"].ToString();
                Session["__UserTypeId__"] = getCookies["__UserTypeId__"].ToString();
                Session["__StudentId__"] = getCookies["__StudentId__"].ToString();
                Session["__EID__"] = getCookies["__EID__"].ToString();
                Session["__SchoolName__"] = getCookies["__SchoolName__"].ToString();
                Session["__SchoolAddress__"] = getCookies["__SchoolAddress__"].ToString();
                lblUserName.Text = getCookies["__SchoolName__"].ToString() + "|" + getCookies["__UserType__"].ToString();
                lblSchoolName.Text = getCookies["__SchoolName__"].ToString();
                Session["__IsOnline__"] = getCookies["__IsOnline__"].ToString();
                
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            if ((Request.Cookies["userInfoSchool"] != null))
            {
                Response.Cookies["userInfoSchool"].Expires = DateTime.Now.AddDays(-30);
            }
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/"+Routing.DashboardRouteUrl);
            //Response.Redirect(GetRouteUrl("LoginRoute"));

        }
    }
}