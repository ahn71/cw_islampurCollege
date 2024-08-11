using DS.BLL.StudentGuide;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Adviser
{
    public partial class AdviserHome : System.Web.UI.Page
    {
        StudentGuideEntry stdGrdEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    LoadAdviserInfo();
                }
        }
        private void LoadAdviserInfo()
        {
            try
            {
                if (stdGrdEntry == null)
                {
                    stdGrdEntry = new StudentGuideEntry();
                }
                DataTable dt = new DataTable();
                dt = stdGrdEntry.LoadAdviserInfo(Session["__EID__"].ToString());
                if (dt.Rows.Count > 0)
                {
                    lblPicName.Text = dt.Rows[0]["EName"].ToString();
                    lblCardNo.Text = dt.Rows[0]["ECardNo"].ToString();
                    lblTCode.Text = dt.Rows[0]["TCodeNo"].ToString();
                    lblDepartment.Text = dt.Rows[0]["DName"].ToString();
                    lblDesignation.Text = dt.Rows[0]["DesName"].ToString();
                  
                    string url = "";
                    if (dt.Rows[0]["EPictureName"].ToString() != string.Empty)
                    {
                        url = @"/Images/teacherProfileImage/" + Path.GetFileName(dt.Rows[0]["EPictureName"].ToString());
                    }
                    else
                    {
                        url = "http://www.placehold.it/300x300/EFEFEF/999&text=no+image";
                    }
                    stImage.ImageUrl = url;
                }
            }
            catch { }
        }

        protected void A6_ServerClick(object sender, EventArgs e)
        {
            LoadAdviserProfile();
        }

        protected void A1_ServerClick(object sender, EventArgs e)
        {
            LoadAdviserProfile();
        }
        private void LoadAdviserProfile()
        {
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@EID", Session["__EID__"].ToString()) };
                sqlDB.fillDataTable("Select EID, ECardNo,  convert(varchar(11),EJoiningDate,106) as EJoiningDate, EName, EGender, EFathersName, EMothersName, DId, DesId, "
                + "EReligion, EMaritalStatus, EPhone, EMobile, EEmail,  convert(varchar(11),EBirthday,106) as EBirthday, EPresentAddress, EParmanentAddress, EBloodGroup,"
                + "ELastDegree, EExaminer, ENationality, EPictureName,DName,DesName,EStatus,TCodeNo,IsFaculty from v_EmployeeInfo where EID=@EID ", prms, dt);
                Session["__HRProfile__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=HRProfile');", true);  //Open New Tab for Sever side code

            }
            catch
            {

            }
        }

        protected void A9_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Adviser/StudentResult.aspx?ID=EID-" + Session["__EID__"].ToString() + "");
        }

        protected void A10_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Adviser/AdviserWiseStdAttDetails.aspx?ID=EID-" + Session["__EID__"].ToString() + "");
        }

    }
}