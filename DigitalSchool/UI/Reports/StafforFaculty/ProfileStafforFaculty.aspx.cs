using DS.BLL.ControlPanel;
using DS.BLL.HR;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.StafforFaculty
{
    public partial class ProfileStafforFaculty : System.Web.UI.Page
    {
        string sqlCmd = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ProfileStafforFaculty.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                Classes.commonTask.loadEmployeeType(rblEmpType);
                EmployeeEntry.LoadCardNo(ddlCardNo,rblEmpType.SelectedValue);
            }
        }

        protected void rblEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            EmployeeEntry.LoadCardNo(ddlCardNo, rblEmpType.SelectedValue);
        }

        private void loadEducationInfo()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@EID", ddlCardNo.SelectedValue) };
                sqlCmd = " select EID,EIExamName,EIDepertment,EIBoard,EIPassingYear,EIResult from EmployeeEducation Where EID=@EID order by SLNo";
                sqlDB.fillDataTable(sqlCmd, prms, dt);
                
                Session["__EmpEducationalInfo__"] = dt;
            }
            catch (Exception ex) { }
        }
        private void loadExperienceInfo()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@EID", ddlCardNo.SelectedValue) };
                sqlCmd = " select EID,ExIInstName,ExIDesignation,convert(varchar(10), ExIDDateFrom,105) as ExIDDateFrom,convert(varchar(10), ExIDateTO,105) as  ExIDateTO,ExIDuration from EmployeeExperience Where EID=@EID order by SLNo";
                sqlDB.fillDataTable(sqlCmd, prms, dt);
               
                Session["__EmpExperienceInfo__"] = dt;
            }
            catch (Exception ex) { }
        }
        private void loadOthersInfo()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@EID", ddlCardNo.SelectedValue) };
                sqlCmd = " select * from EmployeeOthersInfo Where EID=@EID order by SL";
                sqlDB.fillDataTable(sqlCmd, prms, dt);
               
                Session["__EmpOthersInfo__"] = dt;
            }
            catch (Exception ex) { }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
             
            DataTable dt = new DataTable();
            SqlParameter[] prms = { new SqlParameter("@EID", ddlCardNo.SelectedValue) };
                sqlCmd = "Select EID, ECardNo,  convert(varchar(11),EJoiningDate,106) as EJoiningDate,convert(varchar(10),EJoiningDate,120) as EJoiningDate1, EName, EGender, EFathersName, EMothersName, DId, DesId, EReligion, EMaritalStatus, EPhone, EMobile, EEmail,  convert(varchar(11),EBirthday,106) as EBirthday, EPresentAddress, EParmanentAddress, EBloodGroup,ELastDegree, EExaminer, ENationality, EPictureName,DName,DesName,EStatus,TCodeNo,IsFaculty,EmployeeTypeID,EmployeeType,ShiftId,ShiftName,ForAllShift,EIIClassTeacher,ClassName,ECName,ECMobile,ECRelation, PrDistrictName, PerDistrictName,PrPostOfficeName,PerPostOfficeName,PrThanaName,PerThanaName,ESignName from v_EmployeeInfo where EID=@EID ";
                sqlDB.fillDataTable(sqlCmd, prms, dt);
            Session["__HRProfile__"] = dt;
                loadEducationInfo();
                loadExperienceInfo();
                loadOthersInfo();
            if (rblImageStatus.SelectedValue=="1")
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=HRProfile');", true);  //Open New Tab for Sever side code
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=HRwithoutImageProfile');", true);  //Open New Tab for Sever side code
            }            
            }
            catch
            {

            }

                     
        }       
    }
}