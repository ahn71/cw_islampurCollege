using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DS.BLL.ControlPanel;

namespace DS.UI.Administration.HR.Employee
{
    public partial class EmpProfile : System.Web.UI.Page
    {
        static string teacherId = "";
        string sqlCmd ="";
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    try
                    {
                        if (Request.QueryString["hasperm"].ToString() != null) lblMessage.InnerText = "warning->You don't have permission to Update.";
                    }
                    catch { }                   
                    teacherId = Request.QueryString["teacherId"].Replace("?"," ");
                    loadEmployeeInfoInfo();
                    loadEducationInfo();
                    loadExperienceInfo();
                    loadOthersInfo();
                }
        }
        private void loadEducationInfo()
        {
            try {
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@EID", teacherId) };
                sqlCmd = " select EID,EIExamName,EIDepertment,EIBoard,EIPassingYear,EIResult from EmployeeEducation Where EID=@EID order by SLNo";
                sqlDB.fillDataTable(sqlCmd, prms, dt);
                gvEducationalInfo.DataSource = dt;
                gvEducationalInfo.DataBind();
                Session["__EmpEducationalInfo__"] = dt;
            }            
            catch (Exception ex) { }
        }
        private void loadExperienceInfo()
        {
            try {
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@EID", teacherId) };
                sqlCmd = " select EID,ExIInstName,ExIDesignation,convert(varchar(10), ExIDDateFrom,105) as ExIDDateFrom,convert(varchar(10), ExIDateTO,105) as  ExIDateTO,ExIDuration from EmployeeExperience Where EID=@EID order by SLNo";
                sqlDB.fillDataTable(sqlCmd, prms, dt);
                gvExperienceInfo.DataSource = dt;
                gvExperienceInfo.DataBind();
                Session["__EmpExperienceInfo__"] = dt;
            }            
            catch (Exception ex) { }
        }
        private void loadOthersInfo()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@EID", teacherId) };
                sqlCmd = " select * from EmployeeOthersInfo Where EID=@EID order by SL";
                sqlDB.fillDataTable(sqlCmd, prms, dt);
                gvOthersInfo.DataSource = dt;
                gvOthersInfo.DataBind();
                Session["__EmpOthersInfo__"] = dt;
            }
            catch (Exception ex) { }
        }
        private void loadEmployeeInfoInfo()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@EID", teacherId) };
                 sqlCmd = "Select EID, ECardNo,  convert(varchar(11),EJoiningDate,106) as EJoiningDate,convert(varchar(10),EJoiningDate,120) as EJoiningDate1, EName, EGender, EFathersName, EMothersName, DId, DesId, EReligion, EMaritalStatus, EPhone, EMobile, EEmail,  convert(varchar(11),EBirthday,106) as EBirthday, EPresentAddress, EParmanentAddress, EBloodGroup,ELastDegree, EExaminer, ENationality, EPictureName,DName,DesName,EStatus,TCodeNo,IsFaculty,EmployeeTypeID,EmployeeType,ShiftId,ShiftName,ForAllShift,EIIClassTeacher,ClassName,ECName,ECMobile,ECRelation, PrDistrictName, PerDistrictName,PrPostOfficeName,PerPostOfficeName,PrThanaName,PerThanaName,ESignName from v_EmployeeInfo where EID=@EID ";
                sqlDB.fillDataTable(sqlCmd, prms, dt);


                lblCardNo.InnerText = dt.Rows[0]["ECardNo"].ToString();
                lblJoiningDate.InnerText = dt.Rows[0]["EJoiningDate"].ToString();
                lblName1.InnerText = dt.Rows[0]["EName"].ToString();
                lblGender.InnerText = dt.Rows[0]["EGender"].ToString();
                lblFatherName.InnerText = dt.Rows[0]["EFathersName"].ToString();
                lblMotherName.InnerText = dt.Rows[0]["EMothersName"].ToString();

                lblReligion.InnerText = dt.Rows[0]["EReligion"].ToString();
                lblMaritalStatus.InnerText = dt.Rows[0]["EMaritalStatus"].ToString();
                lblPhone.InnerText = dt.Rows[0]["EPhone"].ToString();
                lblMobile.InnerText = dt.Rows[0]["EMobile"].ToString();
                lblEmail.InnerText = dt.Rows[0]["EEmail"].ToString();
                lblBirthDay.InnerText = dt.Rows[0]["EBirthday"].ToString();
                lblPresentAddress.InnerText = dt.Rows[0]["EPresentAddress"].ToString();
                lblPermanentAddress.InnerText = dt.Rows[0]["EParmanentAddress"].ToString();
                lblBloodGroup.InnerText = dt.Rows[0]["EBloodGroup"].ToString();
                lblLastDegree.InnerText = dt.Rows[0]["ELastDegree"].ToString();

                lblNationality.InnerText = dt.Rows[0]["ENationality"].ToString();
                lblDepartment.InnerText = dt.Rows[0]["DName"].ToString();
                lblDesignation.InnerText = dt.Rows[0]["DesName"].ToString();

                if (dt.Rows[0]["EExaminer"].ToString() == "True") lblExaminer.InnerText = "Yes";
                else lblExaminer.InnerText = "No";

                if (dt.Rows[0]["IsFaculty"].ToString() == "True")
                {
                    trTCode.Visible = true;
                    lblTCodeNo.InnerText = dt.Rows[0]["TCodeNo"].ToString();
                    lblTitle.InnerText = "Faculty";                    
                    Session["__Title__"] = "EMPLOYEE";
                }
                else
                {
                    trTCode.Visible = false;
                    lblTitle.InnerText = "Staff";                    
                    Session["__Title__"] = "STAFF";
                }

                //  lblEStatus.InnerText = dt.Rows[0]["EStatus"].ToString();
                lblEmpType.InnerText = dt.Rows[0]["EmployeeType"].ToString();
                lblShift.InnerText = dt.Rows[0]["ShiftName"].ToString();
                lblJobType.InnerText = dt.Rows[0]["EStatus"].ToString();
                lblClassTeacherOf.InnerText = dt.Rows[0]["ClassName"].ToString();

                lblEmergencyContactName.InnerText = dt.Rows[0]["ECName"].ToString();
                lblEmergencyContactMobile.InnerText = dt.Rows[0]["ECMobile"].ToString();
                lblEmergencyContactRelation .InnerText = dt.Rows[0]["ECRelation"].ToString();

                lblPresentDistrict.InnerText = dt.Rows[0]["PrDistrictName"].ToString();
                lblPermanentDistrict.InnerText = dt.Rows[0]["PerDistrictName"].ToString();
                lblPresentPostOffice  .InnerText = dt.Rows[0]["PrPostOfficeName"].ToString();
                lblPermanentPostOffice .InnerText = dt.Rows[0]["PerPostOfficeName"].ToString();
                lblPresentThanaUpazila .InnerText = dt.Rows[0]["PrThanaName"].ToString();
                lblPermanentThanaUpazila .InnerText = dt.Rows[0]["PerThanaName"].ToString();

                try {
                    string[] duration = Classes.commonTask.getTimeDuration(DateTime.Parse(dt.Rows[0]["EJoiningDate1"].ToString()));
                    lblDurationInThisInstitute.InnerText = duration[0] + " Years " + duration[1] + " Months " + duration[2] + " Days";
                } catch(Exception ex) { } 
               
                string url = @"/Images/teacherProfileImage/" + Path.GetFileName(dt.Rows[0]["EPictureName"].ToString());
                stImage.ImageUrl = url;
                Session["__HRProfile__"] = dt;

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
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=HRProfile');", true);  //Open New Tab for Sever side code
               
            }
            catch { }
        }

        protected void EditEmp_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Administration/HR/Employee/EmpRegForm.aspx?TeacherId=" + teacherId + "&Edit=True");
        }

        protected void btnwithoutImage_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=HRwithoutImageProfile');", true);  //Open New Tab for Sever side code
        }        
    }
}