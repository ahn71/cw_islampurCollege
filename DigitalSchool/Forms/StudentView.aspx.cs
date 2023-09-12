using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DS.DAL.AdviitDAL;
using System.IO;
namespace DS.Forms
{
    public partial class StudentView : System.Web.UI.Page
    {
        static string stid = "";

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
                    loadStudentProfileInfo();
                }
            }
        }

        private void loadStudentProfileInfo()
        {
            try
            {
                stid = Request.QueryString["StudentId"];
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@StudentId", stid) };
                sqlDB.fillDataTable("Select StudentId, AdmissionNo,  convert(varchar(11),AdmissionDate,106) as AdmissionDate, ClassName, SectionName, RollNo, FullName, Gender,  convert(varchar(11),DateOfBirth,106) as DateOfBirth, BloodGroup, Mobile, ImageName, FathersName, FathersProfession, FathersYearlyIncome, MothersName, MothersProfession, MothersYearlyIncome,FathersMobile,MothersMoible,HomePhone, PAVillage, PAPostOffice, PAThana, PADistrict, TAViIlage, TAPostOffice, TAThana, TADistrict, GuardianName, GuardianRelation, GuardianMobileNo, GuardianAddress, MotherTongue, Nationality, PreviousSchoolName, TransferCertifiedNo,  convert(varchar(11),CertifiedDate,106) as CertifiedDate, PreferredClass, PSCGPA, PSCRollNo, PSCBoard, PSCPassingYear, PSCJSCRegistration, IsActive, Comments, Religion, Shift from StudentProfile where StudentId=@StudentId ", prms, dt);

                lblAdmissionNo.Text = dt.Rows[0]["AdmissionNo"].ToString();
                lblAdmissionDate.Text = dt.Rows[0]["AdmissionDate"].ToString();
                lblAdmissionClass.Text = dt.Rows[0]["ClassName"].ToString();
                lblSection.Text = dt.Rows[0]["SectionName"].ToString();
                lblStRoll.Text = dt.Rows[0]["RollNo"].ToString();
                lblStudentName.Text= dt.Rows[0]["FullName"].ToString();
                lblGender.Text = dt.Rows[0]["Gender"].ToString();
                lblDateOfBirth.Text = dt.Rows[0]["DateOfBirth"].ToString();

                lblMobile.Text = dt.Rows[0]["Mobile"].ToString();
                lblBloodGroup.Text = dt.Rows[0]["BloodGroup"].ToString();

                lblReligion.Text = dt.Rows[0]["Religion"].ToString();
                lblShift.Text = dt.Rows[0]["Shift"].ToString();

                lblFatherName.Text = dt.Rows[0]["FathersName"].ToString();
                lblFatherOccupation.Text = dt.Rows[0]["FathersProfession"].ToString();
                lblFatherYearlyIncome.Text = dt.Rows[0]["FathersYearlyIncome"].ToString();
                lblFathersMobile.Text = dt.Rows[0]["FathersMobile"].ToString();

                lblMotherName.Text= dt.Rows[0]["MothersName"].ToString();
                lblMotherOccupation.Text = dt.Rows[0]["MothersProfession"].ToString();
                lblMotherYearlyIncome.Text = dt.Rows[0]["MothersYearlyIncome"].ToString();
                lblMothersMobile.Text = dt.Rows[0]["MothersMoible"].ToString();

                lblHomePhone.Text = dt.Rows[0]["HomePhone"].ToString();

                lblPaVillage.Text = dt.Rows[0]["PAVillage"].ToString();
                lblPaPostOffice.Text = dt.Rows[0]["PAPostOffice"].ToString();
                lblPaThana.Text = dt.Rows[0]["PAThana"].ToString();
                lblPaDistrict.Text = dt.Rows[0]["PADistrict"].ToString();
                lblTaVillage.Text = dt.Rows[0]["TAViIlage"].ToString();
                lblTaPostOffice.Text= dt.Rows[0]["TAPostOffice"].ToString();
                lblTaThana.Text = dt.Rows[0]["TAThana"].ToString();
                lblTaDistrict.Text = dt.Rows[0]["TADistrict"].ToString();

                lblGuardianName.Text = dt.Rows[0]["GuardianName"].ToString();
                lblRelation.Text = dt.Rows[0]["GuardianRelation"].ToString();
                lblGuardianMobile.Text = dt.Rows[0]["GuardianMobileNo"].ToString();
                lblGuardianAddress.Text = dt.Rows[0]["GuardianAddress"].ToString();

                lblPreviousSchoolName.Text = dt.Rows[0]["PreviousSchoolName"].ToString();
                lblTransferCertificateNo.Text = dt.Rows[0]["TransferCertifiedNo"].ToString();
                lblTransferDate.Text = dt.Rows[0]["CertifiedDate"].ToString();
                lblThatClassWouldbeAdmission.Text = dt.Rows[0]["PreferredClass"].ToString();
               // lblGpa.Text = dt.Rows[0]["PSCGPA"].ToString();
                lblRoll.Text = dt.Rows[0]["PSCRollNo"].ToString();
                lblBoard.Text = dt.Rows[0]["PSCBoard"].ToString();
                lblRegistrationPSCJSC.Text = dt.Rows[0]["PSCJSCRegistration"].ToString();
                string url = "http://www.placehold.it/29x29/EFEFEF/999&text=no+image";
                //if(dt.Rows[0]["ImageName"].ToString() != string.Empty)
                //{
                //    url = @"/Images/profileImages/" + Path.GetFileName(dt.Rows[0]["ImageName"].ToString());
                //}                 
                stImage.ImageUrl = url;
                Session["_IndivisualStudentList_"] = dt;

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
                Response.Redirect("/Report/IndividualStudentReport.aspx?studentId=" + stid);
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/IndividualStudentReport.aspx?studentId=' + stid);", true);  //Open New Tab for Sever side code
            }
            catch { }
        }



    }
}