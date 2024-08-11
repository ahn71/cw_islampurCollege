using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;

namespace DS.Forms
{
    public partial class EmployeeView : System.Web.UI.Page
    {
       static string teacherId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    teacherId = Request.QueryString["teacherId"];
                    loadEmployeeInfoInfo();
                }
            }
        }

        private void loadEmployeeInfoInfo()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@EID", teacherId) };
                sqlDB.fillDataTable("Select EID, ECardNo,  convert(varchar(11),EJoiningDate,106) as EJoiningDate, EName, EGender, EFathersName, EMothersName, DId, DesId, "
                +"EReligion, EMaritalStatus, EPhone, EMobile, EEmail,  convert(varchar(11),EBirthday,106) as EBirthday, EPresentAddress, EParmanentAddress, EBloodGroup,"
                +"ELastDegree, EExaminer, ENationality, EPictureName,DName,DesName,EStatus,IsTeacher from v_EmployeeInfo where EID=@EID ", prms, dt);


                lblCardNo.Text = dt.Rows[0]["ECardNo"].ToString();
                lblJoiningDate.Text = dt.Rows[0]["EJoiningDate"].ToString();
                lblName.Text = dt.Rows[0]["EName"].ToString();
                lblGender.Text = dt.Rows[0]["EGender"].ToString();
                lblFatherName.Text = dt.Rows[0]["EFathersName"].ToString();
                lblMotherName.Text = dt.Rows[0]["EMothersName"].ToString();

                lblReligion.Text = dt.Rows[0]["EReligion"].ToString();
                lblMaritalStatus.Text = dt.Rows[0]["EMaritalStatus"].ToString();
                lblPhone.Text = dt.Rows[0]["EPhone"].ToString();
                lblMobile.Text = dt.Rows[0]["EMobile"].ToString();
                lblEmail.Text = dt.Rows[0]["EEmail"].ToString();
                lblBirthDay.Text = dt.Rows[0]["EBirthday"].ToString();
                lblPresentAddress.Text = dt.Rows[0]["EPresentAddress"].ToString();
                lblParmanentAddress.Text = dt.Rows[0]["EParmanentAddress"].ToString();
                lblBloodGroup.Text = dt.Rows[0]["EBloodGroup"].ToString();
                lblLastDegree.Text = dt.Rows[0]["ELastDegree"].ToString(); 

                lblNationality.Text = dt.Rows[0]["ENationality"].ToString();
                lblDepartment.Text = dt.Rows[0]["DName"].ToString();
                lblDesignation.Text = dt.Rows[0]["DesName"].ToString();

                if (dt.Rows[0]["EExaminer"].ToString() == "True")  lblExaminer.Text = "Yes";
                else lblExaminer.Text = "No";

                if (dt.Rows[0]["IsTeacher"].ToString() == "True")
                {
                    lblTitle.Text = "Employee";
                    lblTitle1.Text = "Employee";
                    Session["__Title__"] = "EMPLOYEE";
                }
                else
                {
                    lblTitle.Text = "Staff";
                    lblTitle1.Text = "Staff";
                    Session["__Title__"] = "STAFF";
                }

                lblEStatus.Text = dt.Rows[0]["EStatus"].ToString();
                string url = @"/Images/teacherProfileImage/" + Path.GetFileName(dt.Rows[0]["EPictureName"].ToString());
                stImage.ImageUrl = url;
               
            }
            catch (Exception ex)
            {
                //lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        protected void btnPrintpreview_Click(object sender, EventArgs e)
        {
            try
            {
               // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/IndividualEmployee.aspx');", true);  //Open New Tab for Sever side code
                Response.Redirect("/Report/IndividualEmployee.aspx?employeeId=" + teacherId);
            }
            catch { }
        }

    }
}