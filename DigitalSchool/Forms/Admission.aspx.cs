using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using System.IO;
using ComplexScriptingSystem;
using System.Drawing;
using System.Data.SqlTypes;

namespace DS.Forms
{
    public partial class Adminssion : System.Web.UI.Page
    {
        static string imageName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {                 
                    loadClass();
                    loadSection();
                    loadBoard();
                    loadDistrict();
                    loadDistricts();
                    dviGuardian.Visible = false;
                    loadStudentProfileInfo();
                }
            }
        }
        private void loadStudentProfileInfo()
        {
            try
            {
                string stid = Request.QueryString["StudentId"];
                if (stid == null) return;

                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@StudentId", stid) };
                sqlDB.fillDataTable("Select StudentId, AdmissionNo,  Format(AdmissionDate,'dd-MM-yyyy') as AdmissionDate, ClassName, SectionName, RollNo, FullName, Gender,"
                +"Format(DateOfBirth,'dd-MM-yyyy') as DateOfBirth, BloodGroup, Mobile,BloodGroup, ImageName, FathersName, FathersProfession, FathersYearlyIncome,"
                +"MothersName, MothersProfession, MothersYearlyIncome,FathersMobile,MothersMoible,HomePhone, PAVillage, PAPostOffice, PAThana, PADistrict, TAViIlage, "
                +"TAPostOffice, TAThana, TADistrict, GuardianName, GuardianRelation, GuardianMobileNo, GuardianAddress, MotherTongue, Nationality, PreviousSchoolName,"
                +"TransferCertifiedNo,  Format(CertifiedDate,'dd-MM-yyyy') as CertifiedDate, PreferredClass, PSCGPA, PSCRollNo, PSCBoard, PSCPassingYear, PSCJSCRegistration,"
                +"IsActive, Comments, Session, Religion, Shift,FatherEmail,MotherEmail from StudentProfile where StudentId=@StudentId ", prms, dt);
                if (dt.Rows[0]["GuardianName"].ToString() != "")
                {
                    dviGuardian.Visible = true;
                    chkGuardian.Checked = true;
                }
                else
                {
                    dviGuardian.Visible = false;
                    chkGuardian.Checked = false;
                }
                txtAdmissionNo.Text = dt.Rows[0]["AdmissionNo"].ToString();
                txtAdmissionDate.Text = dt.Rows[0]["AdmissionDate"].ToString();
                ddlClass.Text = dt.Rows[0]["ClassName"].ToString();
                ddlSection.Text = dt.Rows[0]["SectionName"].ToString();
                txtRoll.Text = dt.Rows[0]["RollNo"].ToString();
                txtStudentName.Text = dt.Rows[0]["FullName"].ToString();
                ddlGender.Text= dt.Rows[0]["Gender"].ToString();
                txtDateOfBirth.Text = dt.Rows[0]["DateOfBirth"].ToString();
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                dlBloodGroup.Text = dt.Rows[0]["BloodGroup"].ToString();
                dlReligion.Text = dt.Rows[0]["Religion"].ToString();
                dlShift.Text = dt.Rows[0]["Shift"].ToString();
                txtFatherName.Text = dt.Rows[0]["FathersName"].ToString();
                txtFatherOccupation.Text = dt.Rows[0]["FathersProfession"].ToString();
                txtFatherYearlyIncome.Text = dt.Rows[0]["FathersYearlyIncome"].ToString();
                txtMotherName.Text = dt.Rows[0]["MothersName"].ToString();
                txtMotherOccupation.Text = dt.Rows[0]["MothersProfession"].ToString();
                txtMotherYearlyIncome.Text = dt.Rows[0]["MothersYearlyIncome"].ToString();
                txtFathersMobile.Text = dt.Rows[0]["FathersMobile"].ToString();
                txtMothersMobile.Text = dt.Rows[0]["MothersMoible"].ToString();
                txtHomePhone.Text = dt.Rows[0]["HomePhone"].ToString();
                txtFatherEmail.Text = dt.Rows[0]["FatherEmail"].ToString();
                txtMotherEmail.Text = dt.Rows[0]["MotherEmail"].ToString();
                txtPAVillage.Text = dt.Rows[0]["PAVillage"].ToString();
                txtPAPostOffice.Text = dt.Rows[0]["PAPostOffice"].ToString();
                ddlPAThana.Items.Add( dt.Rows[0]["PAThana"].ToString());
                ddlPADistrict.Text = dt.Rows[0]["PADistrict"].ToString();
                txtTAVillage.Text = dt.Rows[0]["TAViIlage"].ToString();
                txtTAPostOffice.Text= dt.Rows[0]["TAPostOffice"].ToString();
                ddlTAThana.Items.Add( dt.Rows[0]["TAThana"].ToString());
                ddlTADistrict.Text = dt.Rows[0]["TADistrict"].ToString();
                txtGuardianName.Text = dt.Rows[0]["GuardianName"].ToString();
                txtGuardianRelation.Text = dt.Rows[0]["GuardianRelation"].ToString();
                txtGurdianMobile.Text = dt.Rows[0]["GuardianMobileNo"].ToString();
                txtGuardianAddress.Text = dt.Rows[0]["GuardianAddress"].ToString();
                txtPreviousSchoolName.Text = dt.Rows[0]["PreviousSchoolName"].ToString();
                if (dt.Rows[0]["PreviousSchoolName"].ToString() == "") chkNotApplicable.Checked = true;
                else chkNotApplicable.Checked = false;
                txtTransferCNo.Text = dt.Rows[0]["TransferCertifiedNo"].ToString();
                txtTrDate.Text = dt.Rows[0]["CertifiedDate"].ToString();
                ddlThatClass.Text = dt.Rows[0]["PreferredClass"].ToString();
                // lblGpa.Text = dt.Rows[0]["PSCGPA"].ToString();
                txtPSCRoll.Text = dt.Rows[0]["PSCRollNo"].ToString();
                txtGpa.Text = dt.Rows[0]["PSCGPA"].ToString();
                ddlBoard.Text= dt.Rows[0]["PSCBoard"].ToString();
                txtRegistration.Text = dt.Rows[0]["PSCJSCRegistration"].ToString();
                ddlSession.Text = dt.Rows[0]["Session"].ToString();
                imageName = dt.Rows[0]["ImageName"].ToString();
                btnSave.Text = "Update";
                string url = @"/Images/profileImages/" + Path.GetFileName(dt.Rows[0]["ImageName"].ToString());
                imgProfile.ImageUrl = url;               
            }
            catch (Exception ex)
            {
                //lblMessage.InnerText = "error->" + ex.Message;
            }
        }
        private void deleteImage()
        {
            try
            {
                //Get Filename from fileupload control
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //Save images into Images folder
                FileUpload1.SaveAs(Server.MapPath("/Images/profileImages/" + filename));
                if (System.IO.File.Exists(filename))
                {
                    System.IO.File.Delete(filename);
                }
              //  cmd = new SqlCommand("Delete From ", dbc.Connection);
            }
            catch { }
        }

        private void loadClass() //Show Class Name
        {
            try
            {
                sqlDB.bindDropDownList("select ClassName from Classes order by ClassId", "ClassName",ddlClass);
                sqlDB.bindDropDownList("select ClassName from Classes order by ClassId", "ClassName", ddlThatClass);
            }
            catch { }
        }

        private void loadBoard() //Show Board Name
        {
            try
            {
                sqlDB.bindDropDownList("select BoardName from Boards", "BoardName", ddlBoard);
            }
            catch { }
        }

        private void loadSection() //Show Section Name
        {
            try
            {
                sqlDB.bindDropDownList("select SectionName from Sections", "SectionName", ddlSection);
            }
            catch { }
        }

        private void loadDistrict()  //Show Thana/Upazila name whre District name for Present Address
        {
            try
            {
               // ddlPADistrict.Items.Add("---Select---");
                sqlDB.loadDropDownList("select DistrictName from Distritcts as td", ddlPADistrict);

                //ddlTADistrict.Items.Add("---Select---");
                sqlDB.loadDropDownList("select DistrictName from Distritcts as td", ddlTADistrict);
            }
            catch { }
        }

        private void loadPAThanas()  //Show Thana/Upazila name whre District name for Permanent Address
        {
            try
            {
                sqlDB.bindDropDownList("select td.ThanaName from v_ThanaDetails as td where DistrictName='" + ddlPADistrict.SelectedItem.Text + "' ", "ThanaName", ddlPAThana);
            }
            catch { }
        }

        private void loadTAThanas()  //Show Thana/Upazila name whre District name for Present Address
        {
            try
            {
                sqlDB.bindDropDownList("select ThanaName from v_ThanaDetails  where DistrictName='" + ddlTADistrict.SelectedItem.Text + "' ", "ThanaName", ddlTAThana);
            }
            catch { }
        }

        private void saveImg()
        {
            try
            {
                //Get Filename from fileupload control
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //Save images into Images folder
                FileUpload1.SaveAs(Server.MapPath("/Images/profileImages/" +txtAdmissionNo.Text.Trim() + filename));
                
            }
            catch { }      
        }

        private Boolean saveStudentProfile()
        {
            try
            {
                System.Data.SqlTypes.SqlDateTime getDate;
                getDate = SqlDateTime.Null;
                DataTable dtChkSameRoll = new DataTable();
                sqlDB.fillDataTable("Select RollNo From StudentProfile Where RollNo='" + txtRoll.Text.Trim() + "' And SectionName='" + ddlSection.SelectedItem.Text + "'"
                +"And Shift='" + dlShift.SelectedItem.Text + "' And ClassName='"+ddlClass.SelectedItem.Text+"' ", dtChkSameRoll);
                if (dtChkSameRoll.Rows.Count > 0)
                {
                    txtRoll.Focus();
                    txtRoll.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F0E1");
                    txtRoll.ForeColor = Color.Red;
                    lblMessage.InnerText = "This roll already exist";
                    return false;
                }
                SqlCommand cmd = new SqlCommand("saveStudentProfile", sqlDB.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlDB.connectDB();
                cmd.Parameters.AddWithValue("@AdmissionNo", txtAdmissionNo.Text.Trim());
                if (txtAdmissionDate.Text == "") cmd.Parameters.AddWithValue("@AdmissionDate", getDate);
                else cmd.Parameters.AddWithValue("@AdmissionDate", convertDateTime.getCertainCulture(txtAdmissionDate.Text.Trim()));
                cmd.Parameters.AddWithValue("@ClassName", ddlClass.Text.Trim());
                cmd.Parameters.AddWithValue("@SectionName", ddlSection.Text.Trim());
                cmd.Parameters.AddWithValue("@RollNo", txtRoll.Text.Trim());
                cmd.Parameters.AddWithValue("@FullName", txtStudentName.Text.Trim());
                cmd.Parameters.AddWithValue("@Gender", ddlGender.Text.Trim());
                if (txtDateOfBirth.Text == "") cmd.Parameters.AddWithValue("@DateOfBirth", getDate);
                else cmd.Parameters.AddWithValue("@DateOfBirth", convertDateTime.getCertainCulture(txtDateOfBirth.Text.Trim()));
                if (dlBloodGroup.Text == "Unknown") cmd.Parameters.AddWithValue("@BloodGroup", "");
                else cmd.Parameters.AddWithValue("@BloodGroup", dlBloodGroup.Text.Trim());
                cmd.Parameters.AddWithValue("@Religion", dlReligion.Text.Trim());
                cmd.Parameters.AddWithValue("@Shift", dlShift.Text.Trim());                
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@Session", ddlSession.Text.Trim());
                cmd.Parameters.AddWithValue("@ImageName", txtAdmissionNo.Text.Trim()+ FileUpload1.FileName);
                cmd.Parameters.AddWithValue("@FathersName", txtFatherName.Text.Trim());
                cmd.Parameters.AddWithValue("@FathersProfession", txtFatherOccupation.Text.Trim());
                if(txtFatherYearlyIncome.Text!="") cmd.Parameters.AddWithValue("@FathersYearlyIncome", txtFatherYearlyIncome.Text.Trim());
                else cmd.Parameters.AddWithValue("@FathersYearlyIncome", "0");
                cmd.Parameters.AddWithValue("@FathersMobile", txtFathersMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@FatherEmail", txtFatherEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@MothersName", txtMotherName.Text.Trim());
                cmd.Parameters.AddWithValue("@MothersProfession", txtMotherOccupation.Text.Trim());
                if (txtMotherYearlyIncome.Text != "") cmd.Parameters.AddWithValue("@MothersYearlyIncome", txtMotherYearlyIncome.Text.Trim());
                else cmd.Parameters.AddWithValue("@MothersYearlyIncome", "0");            
                cmd.Parameters.AddWithValue("@MothersMoible", txtMothersMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@MotherEmail", txtMotherEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@HomePhone", txtHomePhone.Text.Trim());              
                cmd.Parameters.AddWithValue("@PAVillage", txtPAVillage.Text.Trim());
                cmd.Parameters.AddWithValue("@PAPostOffice", txtPAPostOffice.Text.Trim());
                cmd.Parameters.AddWithValue("@PAThana", ddlPAThana.Text.Trim());
                cmd.Parameters.AddWithValue("@PADistrict", ddlPADistrict.Text.Trim());
                cmd.Parameters.AddWithValue("@TAViIlage", txtTAVillage.Text.Trim());
                cmd.Parameters.AddWithValue("@TAPostOffice", txtTAPostOffice.Text.Trim());
                cmd.Parameters.AddWithValue("@TAThana", ddlTAThana.Text.Trim());
                cmd.Parameters.AddWithValue("@TADistrict", ddlTADistrict.Text.Trim());
                cmd.Parameters.AddWithValue("@GuardianName", txtGuardianName.Text.Trim());
                cmd.Parameters.AddWithValue("@GuardianRelation", txtGuardianRelation.Text.Trim());
                cmd.Parameters.AddWithValue("@GuardianMobileNo", txtGurdianMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@GuardianAddress", txtGuardianAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@MotherTongue", "Bangla");
                cmd.Parameters.AddWithValue("@Nationality", "Bangladeshi");
                if (chkNotApplicable.Checked == false)
                {
                    cmd.Parameters.AddWithValue("@PreviousExamType", ddlExam.Text);
                    cmd.Parameters.AddWithValue("@PreviousSchoolName", txtPreviousSchoolName.Text.Trim());
                    if (txtTransferCNo.Text != "") cmd.Parameters.AddWithValue("@TransferCertifiedNo", txtTransferCNo.Text.Trim());
                    else cmd.Parameters.AddWithValue("@TransferCertifiedNo", "0");
                    if (txtTrDate.Text == "") cmd.Parameters.AddWithValue("@CertifiedDate", getDate);
                    else cmd.Parameters.AddWithValue("@CertifiedDate", convertDateTime.getCertainCulture(txtTrDate.Text.Trim()));
                    cmd.Parameters.AddWithValue("@PreferredClass", ddlThatClass.Text.Trim());
                    cmd.Parameters.AddWithValue("@PSCGPA", txtGpa.Text.Trim());
                    cmd.Parameters.AddWithValue("@PSCRollNo", txtPSCRoll.Text.Trim());
                    cmd.Parameters.AddWithValue("@PSCBoard", ddlBoard.Text.Trim());
                    cmd.Parameters.AddWithValue("@PSCPassingYear", ddlPassingYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@PSCJSCRegistration", txtRegistration.Text.Trim());
                    cmd.Parameters.AddWithValue("@IsActive", "1");
                    cmd.Parameters.AddWithValue("@Comments", "");                  
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PreviousExamType", "");
                    cmd.Parameters.AddWithValue("@PreviousSchoolName", "");
                    cmd.Parameters.AddWithValue("@TransferCertifiedNo", "0");
                    cmd.Parameters.AddWithValue("@CertifiedDate", getDate);
                    cmd.Parameters.AddWithValue("@PreferredClass", "");
                    cmd.Parameters.AddWithValue("@PSCGPA", "0");
                    cmd.Parameters.AddWithValue("@PSCRollNo", "0");
                    cmd.Parameters.AddWithValue("@PSCBoard", "");
                    cmd.Parameters.AddWithValue("@PSCPassingYear", "0");
                    cmd.Parameters.AddWithValue("@PSCJSCRegistration", "");
                    cmd.Parameters.AddWithValue("@IsActive", "1");
                    cmd.Parameters.AddWithValue("@Comments", "");
                }
                int result = (int)cmd.ExecuteScalar();
                saveImg(); // for student photo save
                saveCurrentStudentInfo();// for student Current info save
                if (result > 0) lblMessage.InnerText = "success->Successfully saved";
                else lblMessage.InnerText = "error->Unable to save";
                clearInputs();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "saveSuccess();", true);
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        private Boolean saveCurrentStudentInfo()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("select max(StudentId) as StudentId from StudentProfile", dt);
                SqlCommand cmd = new SqlCommand("Insert into  CurrentStudentInfo (StudentId,ClassName, SectionName, RollNo, Mobile,BloodGroup, FathersYearlyIncome,"
                +"MothersYearlyIncome, FathersMobile,MothersMoible,HomePhone, TAViIlage, TAPostOffice, TAThana, TADistrict, GuardianName, GuardianRelation, GuardianMobileNo,"
                +"GuardianAddress, IsActive, Comments, Session, Religion, Shift)  values (@StudentId,@ClassName, @SectionName, @RollNo, @Mobile,@BloodGroup,"
                +"@FathersYearlyIncome, @MothersYearlyIncome, @FathersMobile, @MothersMoible, @HomePhone, @TAViIlage, @TAPostOffice, @TAThana, @TADistrict, @GuardianName,"
                +"@GuardianRelation, @GuardianMobileNo, @GuardianAddress, @IsActive, @Comments, @Session, @Religion, @Shift) ", sqlDB.connection);
                cmd.Parameters.AddWithValue("@StudentId", dt.Rows[0]["StudentId"]);
                cmd.Parameters.AddWithValue("@ClassName", ddlClass.Text);
                cmd.Parameters.AddWithValue("@SectionName", ddlSection.Text);
                cmd.Parameters.AddWithValue("@RollNo", txtRoll.Text.Trim());
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@Session", ddlSession.Text.Trim());
                if (dlBloodGroup.Text == "Unknown") cmd.Parameters.AddWithValue("@BloodGroup", "");
                else cmd.Parameters.AddWithValue("@BloodGroup", dlBloodGroup.Text.Trim());
                cmd.Parameters.AddWithValue("@Religion", dlReligion.Text.Trim());
                cmd.Parameters.AddWithValue("@Shift", dlShift.Text.Trim());
                if (txtFatherYearlyIncome.Text != "") cmd.Parameters.AddWithValue("@FathersYearlyIncome", txtFatherYearlyIncome.Text.Trim());
                else cmd.Parameters.AddWithValue("@FathersYearlyIncome", "0");
                if (txtMotherYearlyIncome.Text != "") cmd.Parameters.AddWithValue("@MothersYearlyIncome", txtMotherYearlyIncome.Text.Trim());
                else cmd.Parameters.AddWithValue("@MothersYearlyIncome", "0");
                cmd.Parameters.AddWithValue("@FathersMobile", txtFathersMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@MothersMoible", txtMothersMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@HomePhone", txtHomePhone.Text.Trim());
                cmd.Parameters.AddWithValue("@TAViIlage", txtTAVillage.Text.Trim());
                cmd.Parameters.AddWithValue("@TAPostOffice", txtTAPostOffice.Text.Trim());
                cmd.Parameters.AddWithValue("@TAThana", ddlTAThana.Text);
                cmd.Parameters.AddWithValue("@TADistrict", ddlTADistrict.Text);
                cmd.Parameters.AddWithValue("@GuardianName", txtGuardianName.Text.Trim());
                cmd.Parameters.AddWithValue("@GuardianRelation", txtGuardianRelation.Text.Trim());
                cmd.Parameters.AddWithValue("@GuardianMobileNo", txtGurdianMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@GuardianAddress", txtGuardianAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@IsActive", "1");
                cmd.Parameters.AddWithValue("@Comments", "");
                int result = (int)cmd.ExecuteNonQuery();
                if (result > 0) lblMessage.InnerText = "success->Successfully saved";
                else lblMessage.InnerText = "error->Unable to save";
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        private Boolean updateStudentProfile()
        {
            try
            {
                System.Data.SqlTypes.SqlDateTime getDate;
                getDate = SqlDateTime.Null;
                string stid = Request.QueryString["StudentId"];
                if (FileUpload1.FileName != "")
                {
                    SqlCommand cmd = new SqlCommand("update StudentProfile  Set AdmissionDate=@AdmissionDate, ClassName=@ClassName, SectionName=@SectionName,"
                    +"RollNo=@RollNo, FullName=@FullName, Gender=@Gender, DateOfBirth=@DateOfBirth, Mobile=@Mobile, BloodGroup=@BloodGroup, ImageName=@ImageName," 
                    +" FathersName=@FathersName, FathersProfession=@FathersProfession, FathersYearlyIncome=@FathersYearlyIncome, MothersName=@MothersName,"
                    +"MothersProfession=@MothersProfession, MothersYearlyIncome=@MothersYearlyIncome, FathersMobile=@FathersMobile, MothersMoible=@MothersMoible,"
                    +" HomePhone=@HomePhone, PAVillage=@PAVillage, PAPostOffice=@PAPostOffice, PAThana=@PAThana, PADistrict=@PADistrict, TAViIlage=@TAViIlage,"
                    +" TAPostOffice=@TAPostOffice, TAThana=@TAThana, TADistrict=@TADistrict, GuardianName=@GuardianName, GuardianRelation=@GuardianRelation," 
                    +" GuardianMobileNo=@GuardianMobileNo, GuardianAddress=@GuardianAddress,  PreviousSchoolName=@PreviousSchoolName, TransferCertifiedNo=@TransferCertifiedNo,"
                    +" CertifiedDate=@CertifiedDate, PreferredClass=@PreferredClass, PSCGPA=@PSCGPA, PSCRollNo=@PSCRollNo, PSCBoard=@PSCBoard, PSCPassingYear=@PSCPassingYear,"
                    +"PSCJSCRegistration=@PSCJSCRegistration, Session=@Session, Religion=@Religion, Shift=@Shift,FatherEmail=@FatherEmail,MotherEmail=@MotherEmail where"
                    +" StudentId=@StudentId ", sqlDB.connection);
                    cmd.Parameters.AddWithValue("@StudentId", stid);
                    if (txtAdmissionDate.Text == "") cmd.Parameters.AddWithValue("@AdmissionDate", getDate);
                    else cmd.Parameters.AddWithValue("@AdmissionDate", convertDateTime.getCertainCulture(txtAdmissionDate.Text.Trim()));
                    cmd.Parameters.AddWithValue("@ClassName", ddlClass.Text.Trim());
                    cmd.Parameters.AddWithValue("@SectionName", ddlSection.Text.Trim());
                    cmd.Parameters.AddWithValue("@RollNo", txtRoll.Text.Trim());
                    cmd.Parameters.AddWithValue("@FullName", txtStudentName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gender", ddlGender.Text.Trim());
                    if (txtDateOfBirth.Text == "") cmd.Parameters.AddWithValue("@DateOfBirth", getDate);
                    else cmd.Parameters.AddWithValue("@DateOfBirth", convertDateTime.getCertainCulture(txtDateOfBirth.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@Religion", dlReligion.Text.Trim());
                    cmd.Parameters.AddWithValue("@Shift", dlShift.Text.Trim());
                    if (dlBloodGroup.Text == "") cmd.Parameters.AddWithValue("@BloodGroup", "");
                    else cmd.Parameters.AddWithValue("@BloodGroup", dlBloodGroup.Text.Trim());
                    cmd.Parameters.AddWithValue("@Session", ddlSession.Text.Trim());
                    cmd.Parameters.AddWithValue("@ImageName", txtAdmissionNo.Text.Trim() + FileUpload1.FileName);
                    cmd.Parameters.AddWithValue("@FathersName", txtFatherName.Text.Trim());
                    cmd.Parameters.AddWithValue("@FathersProfession", txtFatherOccupation.Text.Trim());
                    cmd.Parameters.AddWithValue("@FathersYearlyIncome", txtFatherYearlyIncome.Text.Trim());
                    cmd.Parameters.AddWithValue("@FathersMobile", txtFathersMobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@MothersMoible", txtMothersMobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@HomePhone", txtHomePhone.Text.Trim());
                    cmd.Parameters.AddWithValue("@MothersName", txtMotherName.Text.Trim());
                    cmd.Parameters.AddWithValue("@MothersProfession", txtMotherOccupation.Text.Trim());
                    cmd.Parameters.AddWithValue("@MothersYearlyIncome", txtMotherYearlyIncome.Text.Trim());
                    cmd.Parameters.AddWithValue("@PAVillage", txtPAVillage.Text.Trim());
                    cmd.Parameters.AddWithValue("@PAPostOffice", txtPAPostOffice.Text.Trim());
                    cmd.Parameters.AddWithValue("@PAThana", ddlPAThana.Text.Trim());
                    cmd.Parameters.AddWithValue("@PADistrict", ddlPADistrict.Text.Trim());
                    cmd.Parameters.AddWithValue("@TAViIlage", txtTAVillage.Text.Trim());
                    cmd.Parameters.AddWithValue("@TAPostOffice", txtTAPostOffice.Text.Trim());
                    cmd.Parameters.AddWithValue("@TAThana", ddlTAThana.Text.Trim());
                    cmd.Parameters.AddWithValue("@TADistrict", ddlTADistrict.Text.Trim());
                    cmd.Parameters.AddWithValue("@GuardianName", txtGuardianName.Text.Trim());
                    cmd.Parameters.AddWithValue("@GuardianRelation", txtGuardianRelation.Text.Trim());
                    cmd.Parameters.AddWithValue("@GuardianMobileNo", txtGurdianMobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@GuardianAddress", txtGuardianAddress.Text.Trim());
                    if (chkNotApplicable.Checked == false)
                    {
                        cmd.Parameters.AddWithValue("@PreviousSchoolName", txtPreviousSchoolName.Text.Trim());
                        if (txtTransferCNo.Text != "") cmd.Parameters.AddWithValue("@TransferCertifiedNo", txtTransferCNo.Text.Trim());
                        else cmd.Parameters.AddWithValue("@TransferCertifiedNo", "0");
                        if (txtTrDate.Text == "") cmd.Parameters.AddWithValue("@CertifiedDate", getDate);
                        else cmd.Parameters.AddWithValue("@CertifiedDate", convertDateTime.getCertainCulture(txtTrDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("@PreferredClass", ddlThatClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@PSCGPA", txtGpa.Text.Trim());
                        cmd.Parameters.AddWithValue("@PSCRollNo", txtPSCRoll.Text.Trim());
                        cmd.Parameters.AddWithValue("@PSCBoard", ddlBoard.Text.Trim());
                        cmd.Parameters.AddWithValue("@PSCPassingYear", ddlPassingYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@PSCJSCRegistration", txtRegistration.Text.Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PreviousSchoolName", "");
                        cmd.Parameters.AddWithValue("@TransferCertifiedNo", "0");
                        cmd.Parameters.AddWithValue("@CertifiedDate", getDate);
                        cmd.Parameters.AddWithValue("@PreferredClass", "");
                        cmd.Parameters.AddWithValue("@PSCGPA", "0");
                        cmd.Parameters.AddWithValue("@PSCRollNo", "0");
                        cmd.Parameters.AddWithValue("@PSCBoard","");
                        cmd.Parameters.AddWithValue("@PSCPassingYear", "0");
                        cmd.Parameters.AddWithValue("@PSCJSCRegistration", "");
                    }
                    cmd.Parameters.AddWithValue("@FatherEmail", txtFatherEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@MotherEmail", txtMotherEmail.Text.Trim());
                    if (imageName != "")
                    {
                        System.IO.File.Delete(Request.PhysicalApplicationPath + "/Images/profileImages/" + imageName);
                    }
                    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    FileUpload1.SaveAs(Server.MapPath("/Images/profileImages/" + txtAdmissionNo.Text.Trim() + filename));    //Save images into Images folder
                    cmd.ExecuteNonQuery();
                    lblMessage.InnerText = "success->Update Success";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                    return true;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("update StudentProfile  Set AdmissionDate=@AdmissionDate, ClassName=@ClassName, SectionName=@SectionName, RollNo=@RollNo,"
                    +"FullName=@FullName, Gender=@Gender, DateOfBirth=@DateOfBirth, Mobile=@Mobile, BloodGroup=@BloodGroup, FathersName=@FathersName, FathersProfession="
                    +"@FathersProfession, FathersYearlyIncome=@FathersYearlyIncome, MothersName=@MothersName, MothersProfession=@MothersProfession, MothersYearlyIncome="
                    +"@MothersYearlyIncome,  FathersMobile=@FathersMobile, MothersMoible=@MothersMoible, HomePhone=@HomePhone, PAVillage=@PAVillage, PAPostOffice="
                    +"@PAPostOffice, PAThana=@PAThana, PADistrict=@PADistrict, TAViIlage=@TAViIlage, TAPostOffice=@TAPostOffice, TAThana=@TAThana, TADistrict=@TADistrict,"
                    +"GuardianName=@GuardianName, GuardianRelation=@GuardianRelation, GuardianMobileNo=@GuardianMobileNo, GuardianAddress=@GuardianAddress,"
                    +"PreviousSchoolName=@PreviousSchoolName, TransferCertifiedNo=@TransferCertifiedNo, CertifiedDate=@CertifiedDate, PreferredClass=@PreferredClass,"
                    +"PSCGPA=@PSCGPA, PSCRollNo=@PSCRollNo, PSCBoard=@PSCBoard, PSCPassingYear=@PSCPassingYear, PSCJSCRegistration=@PSCJSCRegistration, Session=@Session,"
                    +"Religion=@Religion, Shift=@Shift,FatherEmail=@FatherEmail,MotherEmail=@MotherEmail where StudentId=@StudentId ", sqlDB.connection);
                    cmd.Parameters.AddWithValue("@StudentId", stid);
                    if (txtAdmissionDate.Text == "") cmd.Parameters.AddWithValue("@AdmissionDate", getDate);
                    else cmd.Parameters.AddWithValue("@AdmissionDate", convertDateTime.getCertainCulture(txtAdmissionDate.Text.Trim()));
                    cmd.Parameters.AddWithValue("@ClassName", ddlClass.Text.Trim());
                    cmd.Parameters.AddWithValue("@SectionName", ddlSection.Text.Trim());
                    cmd.Parameters.AddWithValue("@RollNo", txtRoll.Text.Trim());
                    cmd.Parameters.AddWithValue("@FullName", txtStudentName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gender", ddlGender.Text.Trim());
                    if (txtDateOfBirth.Text == "") cmd.Parameters.AddWithValue("@DateOfBirth", getDate);
                    else cmd.Parameters.AddWithValue("@DateOfBirth", convertDateTime.getCertainCulture(txtDateOfBirth.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                    if (dlBloodGroup.Text == "") cmd.Parameters.AddWithValue("@BloodGroup", "");
                    else cmd.Parameters.AddWithValue("@BloodGroup", dlBloodGroup.Text.Trim());
                    cmd.Parameters.AddWithValue("@Religion", dlReligion.Text.Trim());
                    cmd.Parameters.AddWithValue("@Shift", dlShift.Text.Trim());
                    cmd.Parameters.AddWithValue("@Session", ddlSession.Text);
                    cmd.Parameters.AddWithValue("@FathersName", txtFatherName.Text.Trim());
                    cmd.Parameters.AddWithValue("@FathersProfession", txtFatherOccupation.Text.Trim());
                    cmd.Parameters.AddWithValue("@FathersYearlyIncome", txtFatherYearlyIncome.Text.Trim());                   
                    cmd.Parameters.AddWithValue("@MothersName", txtMotherName.Text.Trim());
                    cmd.Parameters.AddWithValue("@MothersProfession", txtMotherOccupation.Text.Trim());
                    cmd.Parameters.AddWithValue("@MothersYearlyIncome", txtMotherYearlyIncome.Text.Trim());
                    cmd.Parameters.AddWithValue("@FathersMobile", txtFathersMobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@MothersMoible", txtMothersMobile.Text.Trim());                   
                    cmd.Parameters.AddWithValue("@HomePhone", txtHomePhone.Text.Trim());
                    cmd.Parameters.AddWithValue("@PAVillage", txtPAVillage.Text.Trim());
                    cmd.Parameters.AddWithValue("@PAPostOffice", txtPAPostOffice.Text.Trim());
                    cmd.Parameters.AddWithValue("@PAThana", ddlPAThana.Text.Trim());
                    cmd.Parameters.AddWithValue("@PADistrict", ddlPADistrict.Text.Trim());
                    cmd.Parameters.AddWithValue("@TAViIlage", txtTAVillage.Text.Trim());
                    cmd.Parameters.AddWithValue("@TAPostOffice", txtTAPostOffice.Text.Trim());
                    cmd.Parameters.AddWithValue("@TAThana", ddlTAThana.Text.Trim());
                    cmd.Parameters.AddWithValue("@TADistrict", ddlTADistrict.Text.Trim());
                    cmd.Parameters.AddWithValue("@GuardianName", txtGuardianName.Text.Trim());
                    cmd.Parameters.AddWithValue("@GuardianRelation", txtGuardianRelation.Text.Trim());
                    cmd.Parameters.AddWithValue("@GuardianMobileNo", txtGurdianMobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@GuardianAddress", txtGuardianAddress.Text.Trim());
                    if (chkNotApplicable.Checked == false)
                    {
                        cmd.Parameters.AddWithValue("@PreviousSchoolName", txtPreviousSchoolName.Text.Trim());
                        if (txtTransferCNo.Text != "") cmd.Parameters.AddWithValue("@TransferCertifiedNo", txtTransferCNo.Text.Trim());
                        else cmd.Parameters.AddWithValue("@TransferCertifiedNo", "0");
                        if (txtTrDate.Text == "") cmd.Parameters.AddWithValue("@CertifiedDate", getDate);
                        else cmd.Parameters.AddWithValue("@CertifiedDate", convertDateTime.getCertainCulture(txtTrDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("@PreferredClass", ddlThatClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@PSCGPA", txtGpa.Text.Trim());
                        cmd.Parameters.AddWithValue("@PSCRollNo", txtPSCRoll.Text.Trim());
                        cmd.Parameters.AddWithValue("@PSCBoard", ddlBoard.Text.Trim());
                        cmd.Parameters.AddWithValue("@PSCPassingYear", ddlPassingYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@PSCJSCRegistration", txtRegistration.Text.Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PreviousSchoolName", "");
                        cmd.Parameters.AddWithValue("@TransferCertifiedNo", "0");
                        cmd.Parameters.AddWithValue("@CertifiedDate", getDate);
                        cmd.Parameters.AddWithValue("@PreferredClass", "");
                        cmd.Parameters.AddWithValue("@PSCGPA", "0");
                        cmd.Parameters.AddWithValue("@PSCRollNo", "0");
                        cmd.Parameters.AddWithValue("@PSCBoard", "");
                        cmd.Parameters.AddWithValue("@PSCPassingYear", "0");
                        cmd.Parameters.AddWithValue("@PSCJSCRegistration", "");
                    }
                    cmd.Parameters.AddWithValue("@FatherEmail", txtFatherEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@MotherEmail", txtMotherEmail.Text.Trim());
                    cmd.ExecuteNonQuery();
                    updateCurrentStudentClass();
                    lblMessage.InnerText = "success->Update Success";
                    return true;
                }

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        private void updateCurrentStudentClass()
        {
            try
            {
                string stid = Request.QueryString["StudentId"];

                SqlCommand cmd = new SqlCommand("Update CurrentStudentInfo Set ClassName=@ClassName where StudentId=@StudentId", sqlDB.connection);
                cmd.Parameters.AddWithValue("@StudentId", stid);
                cmd.Parameters.AddWithValue("@ClassName",ddlClass.SelectedItem.Text);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                if (saveStudentProfile() == true)
                {
                    clearInputs();
                    lblMessage.InnerText = "success->Successfully saved";
                }
            }
            else if (btnSave.Text == "Update")
            {
                if (updateStudentProfile() == true)
                {
                    clearInputs();
                    Response.Redirect("/Forms/AdmissionDetails.aspx");
                    lblMessage.InnerText = "success->Successfully Updated";
                }
            }
        }

        protected void ddlPADistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadPAThanas();
        }

        protected void ddlTADistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadTAThanas();
        }

        private void loadDistricts()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("select * from Distritcts", dt);
            }
            catch { }
        }

        private void clearInputs()
        {
            try
            {
                txtAdmissionNo.Text = "";
                txtAdmissionDate.Text = "";             
                txtRoll.Text = "";
                txtStudentName.Text = "";             
                txtDateOfBirth.Text = "";
                txtMobile.Text = "";
                txtFatherName.Text = "";
                txtFatherOccupation.Text = "";
                txtFatherYearlyIncome.Text = "";
                txtMotherName.Text = "";
                txtMotherOccupation.Text = "";
                txtMotherYearlyIncome.Text = "";
                txtFathersMobile.Text = "";
                txtMothersMobile.Text = "";
                txtHomePhone.Text = "";
                txtPAVillage.Text = "";
                txtPAPostOffice.Text = "";              
                txtTAVillage.Text = "";
                txtTAPostOffice.Text = "";             
                txtGuardianName.Text = "";
                txtGuardianRelation.Text = "";
                txtGurdianMobile.Text = "";
                txtGuardianAddress.Text = "";
                txtPreviousSchoolName.Text = "";
                txtTransferCNo.Text = "";
                txtTrDate.Text = "";
                txtFatherEmail.Text = "";
                txtMotherEmail.Text = ""; 
                txtPSCRoll.Text = "";
                txtGpa.Text = "";
                imgProfile.ImageUrl = "/Images/profileImages/noProfileImage.jpg";
                imageName = "";
                btnSave.Text = "Save";
             
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearInputs();
        }

        protected void Guardian_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkGuardian.Checked)
                {
                    chkGuardian.Text = " Have Guardian";
                    dviGuardian.Visible = true;
                }
                else
                {
                    dviGuardian.Visible = false;
                    chkGuardian.Text = " Have Guardian ?";
                    txtGuardianName.Text = "";
                    txtGuardianRelation.Text = "";
                    txtGurdianMobile.Text = "";
                    txtGuardianAddress.Text = "";
                }
            }
            catch { }
        }

        protected void chkSameAddress_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSameAddress.Checked)
                {
                    txtTAVillage.Text = txtPAVillage.Text;
                    txtTAPostOffice.Text = txtPAPostOffice.Text;
                    ddlTAThana.Items.Add(ddlPAThana.Text);
                    ddlTAThana.Text = ddlTAThana.Text;
                    ddlTADistrict.Text = ddlPADistrict.Text;
                }
                else
                {
                    txtTAVillage.Text = "";
                    txtTAPostOffice.Text = "";
                    ddlTAThana.Items.Clear();
                    ddlTAThana.Text = "";

                }
            }
            catch { }
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSectionClayseWise();
        }

        private void loadSectionClayseWise()
        {
            try
            {
                DataTable dt;
                SQLOperation.selectBySetCommandInDatatable("Select ClassOrder From Classes where " +
                    " ClassName='" + ddlClass.SelectedItem.Text.Trim() + "'", dt = new DataTable(), sqlDB.connection);
                if (byte.Parse(dt.Rows[0]["ClassOrder"].ToString()) >= 9)
                {
                    ddlSection.Items.Clear();
                    ddlSection.Items.Add("...Select...");
                    ddlSection.Items.Add("Science");
                    ddlSection.Items.Add("Commerce");
                    ddlSection.Items.Add("Arts");
                    ddlSection.SelectedIndex = ddlSection.Items.Count - ddlSection.Items.Count;
                }
                else
                {
                    ddlSection.Items.Clear();
                    sqlDB.loadDropDownList("Select  SectionName from Sections where SectionName<>'Science' AND SectionName<>'Commerce' " +
                        " AND SectionName<>'Arts' Order by SectionName", ddlSection);
                    ddlSection.Items.Add("...Select...");
                    ddlSection.SelectedIndex = ddlSection.Items.Count - 1;
                }
            }
            catch { }
        }

    }
}