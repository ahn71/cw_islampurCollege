using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using System.Data;
using System.Data.SqlClient;
using DS.DAL.ComplexScripting;
using DS.DAL;
using DS.Classes;

namespace DS
{
    public partial class UserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                string a = ComplexLetters.getEntangledLetters("+Jp0yhRwQV4=");
                 a = ComplexLetters.getEntangledLetters("IMAeMid9qzCREVUs/se95w==");
                sqlDB.connectDB();
                txtUsername.Text = string.Empty;
                txtPassword.Text = string.Empty;
                if (Session["__ResultStdID__"] != null)
                {
                    rblUserType.SelectedValue = "Student";
                }
            }
        }       

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (isLogin() == true)
            {
               
                try
                {
                    //DateTime datetimeopen = Convert.ToDateTime("08/30/2018 11:59:59 PM");
                    //datetimeopen = datetimeopen.AddYears(1);
                    //DateTime datetimeexpire = DateTime.Now;
                    //if (datetimeopen < datetimeexpire)
                    //{
                    //    lblexpiredmessage.Visible = true;
                    //    lblexpiredmessage.Text = "1 year validity has expired";
                    //    return;
                    //}
                }
                catch { }
                if (Session["__UserType__"].ToString() == "Student")
                {
                    if (Session["__ResultStdID__"] != null)
                    {
                        Response.Redirect("/UI/StudentManage/StudentExamination.aspx");
                    }
                    else
                    {
                        Response.Redirect("/UI/StudentManage/StudentManage.aspx");
                    }
                }
                else if (Session["__UserType__"].ToString() == "Adviser")
                {
                    Response.Redirect("/UI/Adviser/AdviserHome.aspx");
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "print report", "goURL('/Dashboard.aspx');", true);
                   // Response.Redirect(GetRouteUrl("DashboardRoute"));
                  Response.Redirect("~/"+Routing.DashboardRouteUrl, true);

                    string a = "";
                }
            }
        }

        private Boolean isLogin()
        {
            try
            {
                HttpCookie setCookies = new HttpCookie("userInfoSchool");

                //setCookies.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Add(setCookies);
                Session["__lastname__"] = "";

                DataTable dt = new DataTable();
                if (rblUserType.SelectedItem.Value.ToString().Equals("Faculty"))
                    SQLOperation.selectBySetCommandInDatatable("select FirstName+' '+LastName as FullName,UserId,UserType,Username,UserTypeId from v_useraccount  where " +
                                         " UserName='" + ComplexLetters.getTangledLetters(txtUsername.Text.Trim()) + "' and UserPassword='" + ComplexLetters.getTangledLetters(txtPassword.Text.Trim()) + "' AND IsAdviser='False'  AND Status=1", dt, DS.DAL.DbConnection.Connection);

                else if (rblUserType.SelectedItem.Value.ToString().Equals("Student"))
                {
                    SQLOperation.selectBySetCommandInDatatable("select FullName as LastName,FullName,StudentAccId as UserId,Username,StudentId from v_UserAccount_Student  where " +
                                     " UserName='" + ComplexLetters.getTangledLetters(txtUsername.Text.Trim()) + "' and UserPassword='" + ComplexLetters.getTangledLetters(txtPassword.Text.Trim()) + "'", dt, DS.DAL.DbConnection.Connection);
                    if (Session["__ResultStdID__"] != null)
                    {
                        if (Session["__ResultStdID__"].ToString() != dt.Rows[0]["StudentId"].ToString())
                        {
                            lblMessage.InnerText = "warning->Request Student And Login Student does not match";
                            return false;
                        }
                    }
                }
                else SQLOperation.selectBySetCommandInDatatable("select EName as FullName,EID,UserId,UserType,Username,UserTypeId from v_useraccount  where " +
                                     " UserName='" + ComplexLetters.getTangledLetters(txtUsername.Text.Trim()) + "' and UserPassword='" + ComplexLetters.getTangledLetters(txtPassword.Text.Trim()) + "' AND IsAdviser='True' AND Status=1", dt, DS.DAL.DbConnection.Connection);

                if (dt.Rows.Count == 0)
                {
                    lblMessage.InnerText = "warning->Username or Password Invalid";
                    return false;
                }

                if (dt.Rows[0]["Username"].ToString().CompareTo(ComplexLetters.getTangledLetters(txtUsername.Text.Trim())) == 0)
                {
                    setCookies["__UserId__"] = dt.Rows[0]["UserId"].ToString();
                    // Session["__UserId__"] = dt.Rows[0]["UserId"].ToString();
                    if (rblUserType.SelectedItem.Value.ToString().Equals("Faculty"))
                    {
                        setCookies["__UserType__"] = dt.Rows[0]["UserType"].ToString();
                        Session["__UserType__"] = dt.Rows[0]["UserType"].ToString();
                    }
                    else if (rblUserType.SelectedItem.Value.ToString().Equals("Adviser"))
                    {
                        setCookies["__UserType__"] = "Adviser";
                        Session["__UserType__"] = "Adviser";
                    }
                    else
                    {
                        setCookies["__UserType__"] = "Student";
                        Session["__UserType__"] = "Student";
                    }

                    setCookies["__FullName__"] = dt.Rows[0]["FullName"].ToString();
                    //Session["__FullName__"] = dt.Rows[0]["FullName"].ToString();
                    setCookies["__UserTypeId__"] = (rblUserType.SelectedItem.Value.ToString().Equals("Faculty")) ? dt.Rows[0]["UserTypeId"].ToString() : "0";
                    //Session["__UserTypeId__"] =(rblUserType.SelectedItem.Value.ToString().Equals("Faculty")) ? dt.Rows[0]["UserTypeId"].ToString():"0";
                    setCookies["__StudentId__"] = (rblUserType.SelectedItem.Value.ToString().Equals("Faculty")) ? dt.Rows[0]["UserTypeId"].ToString() : "0";
                    //Session["__StudentId__"] = (rblUserType.SelectedItem.Value.ToString().Equals("Student")) ?dt.Rows[0]["StudentId"].ToString():"0" ;
                    setCookies["__EID__"] = (rblUserType.SelectedItem.Value.ToString().Equals("Adviser")) ? dt.Rows[0]["EID"].ToString() : "0";
                    //Session["__EID__"] = (rblUserType.SelectedItem.Value.ToString().Equals("Adviser")) ?dt.Rows[0]["EID"].ToString():"0";
                    dt = new DataTable();
                    SQLOperation.selectBySetCommandInDatatable("select SchoolName,Address,IsOnline from School_Setup ", dt, DS.DAL.DbConnection.Connection);
                    if (dt.Rows.Count > 0)
                    {
                        setCookies["__SchoolName__"] = dt.Rows[0]["SchoolName"].ToString();
                        //Session["__SchoolName__"] = dt.Rows[0]["SchoolName"].ToString();
                        setCookies["__SchoolAddress__"] = dt.Rows[0]["Address"].ToString();
                        setCookies["__IsOnline__"] = dt.Rows[0]["IsOnline"].ToString();
                        //Session["__SchoolAddress__"] = dt.Rows[0]["Address"].ToString();
                    }
                    else
                    {
                        setCookies["__SchoolName__"] = "";
                        //Session["__SchoolName__"] = dt.Rows[0]["SchoolName"].ToString();
                        setCookies["__SchoolAddress__"] = "";
                        setCookies["__IsOnline__"] = "False";
                        //Session["__SchoolAddress__"] = dt.Rows[0]["Address"].ToString();
                    }
                    Response.Cookies.Add(setCookies);
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