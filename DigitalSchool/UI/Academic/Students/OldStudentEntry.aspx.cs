using DS.BLL;
using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.Classes;
using DS.DAL;
using DS.DAL.AdviitDAL;
using DS.DAL.ComplexScripting;
using DS.PropertyEntities.Model.Admission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academic.Students
{
    public partial class OldStudentEntry : System.Web.UI.Page
    {
        CurrentStdEntry EntryStudent;
        static string imageName = "";
        ClassGroupEntry clsgrpEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie getCookies = Request.Cookies["userInfoSchool"];
            Session["__UserTypeId__"] = getCookies["__UserTypeId__"].ToString();
            Session["__UserId__"] = getCookies["__UserId__"].ToString();
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                string EditMode = "";
                try { EditMode = (Request.QueryString["StudentId"].ToString() != null) ? "Yes" : "No"; }
                catch { }
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "OldStudentEntry.aspx", btnSave, EditMode)) Response.Redirect(Request.UrlReferrer.ToString() + "&hasperm=no");
                if (Session["__MsgoldStdInfo__"] != null)
                {
                    lblMessage.InnerText = Session["__MsgoldStdInfo__"].ToString();
                }
                Session["__MsgoldStdInfo__"] = null;
                // BatchEntry.GetDropdownlist(ddlBatch, true);
                ClassEntry.GetEntitiesData(ddlClass);
                //ClassEntry.GetEntitiesData(ddlThatClass);
                stdtypeEntry.GetEntitiesData(ddlStdType);
                ddlStdType.SelectedValue = "2";
                commonTask.loadBoard(ddlBoard);
                commonTask.loadPassingYearForAdmission(ddlBoard);

                DistrictEntry.GetDropDownList(ddlPADistrict);
                DistrictEntry.GetDropDownList(ddlTADistrict);
                ShiftEntry.GetDropDownList(dlShift);


                txtAdmissionDate.Text = TimeZoneBD.getCurrentTimeBD("dd-MM-yyyy");
                ddlSession.Items.Add(DateTime.Now.Year.ToString());
                int session = DateTime.Now.Year;
                session++;
                ddlSession.Items.Add(session.ToString());
                ddlSession.Text = (session - 1).ToString();
                chkNotApplicable.Checked = false;
                chkTCNotApplicable.Checked = true;
                txtAdmissionNo.Text = CurrentStdEntry.GetMaxID().ToString();
                BusInformationEntry.GetEntitiesData(ddlBusName);
                LocationEntry.GetEntitiesData(ddlLocation);
                PlaceInformationEntry.GetEntitiesData(ddlPlace);
                PlaceInformationEntry.GetEntitiesData(ddlPlace);
                loadStudentProfileInfo();
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
                sqlDB.fillDataTable("Select cs.BatchID,cs.StudentId, AdmissionNo,  Format(AdmissionDate,'dd-MM-yyyy') as AdmissionDate, cs.ClassID, cs.ClsSecID, cs.RollNo, FullName, Gender,"
                + "Format(DateOfBirth,'dd-MM-yyyy') as DateOfBirth, BloodGroup,SUBSTRING(Mobile,4,12)as Mobile,BloodGroup, FathersName, FathersProfession, FathersYearlyIncome,cs.ClsGrpID,"
                + "MothersName, MothersProfession, MothersYearlyIncome,SUBSTRING(FathersMobile,4,12)as FathersMobile,SUBSTRING(MothersMoible,4,12)as MothersMoible,"
                + "SUBSTRING(HomePhone,4,12) as HomePhone, PAVillage, PAPostOffice, PThanaId,PDistrictId, TAViIlage, ImageName,"
                + "TAPostOffice, TThanaId, TDistrictId, GuardianName, GuardianRelation, REPLACE(GuardianMobileNo,'+88', '') "
                + " as GuardianMobileNo, GuardianAddress, MotherTongue, Nationality, PreviousSchoolName,"
                + "TransferCertifiedNo,  Format(CertifiedDate,'dd-MM-yyyy') as CertifiedDate, PreferredClass, PSCGPA, PSCRollNo, PSCBoard, PSCPassingYear, PSCJSCRegistration,"
                + "IsActive, Comments, cs.Session, Religion, ConfigId,FatherEmail,MotherEmail,BatchName,FullNameBn,FatherNameBn,MotherNameBn,cs.IdCard,cs.StdTypeId,cs.FatherDesg,cs.FatherOrg,cs.FatherPhone,cs.MotherDesg,cs.MotherOrg,cs.MotherPhone,cs.BusID,cs.LocationID,cs.PlaceID,cs.PAPostOfficeID,cs.TAPostOfficeID,cs.SSCRoll,cs.TCClass,cs.TCSemister,cs.TCCollegeName,Format(cs.TCDate,'dd-MM-yyyy') as TCDate,cs.PreviousExamType from CurrentStudentInfo cs INNER JOIN  TBL_STD_Admission_INFO asi ON cs.StudentId = asi.StudentID where cs.StudentId=@StudentId ", prms, dt);
                lblstdId.Value = stid;
                txtAdmissionNo.Text = dt.Rows[0]["AdmissionNo"].ToString();
                txtAdmissionDate.Text = dt.Rows[0]["AdmissionDate"].ToString();
                ddlClass.SelectedValue = dt.Rows[0]["ClassID"].ToString();
                if (clsgrpEntry == null)
                {
                    clsgrpEntry = new ClassGroupEntry();
                }
                clsgrpEntry.GetDropDownListClsGrpId(int.Parse(ddlClass.SelectedValue), ddlGroup);
                ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(ddlClass.SelectedValue), ddlGroup.SelectedValue);
                BatchEntry.GetDropdownlist(ddlBatch, int.Parse(ddlClass.SelectedValue), true);
                ddlBatch.SelectedValue = dt.Rows[0]["BatchID"].ToString();
                ddlGroup.SelectedValue = dt.Rows[0]["ClsGrpID"].ToString();
                ddlSection.SelectedValue = dt.Rows[0]["ClsSecID"].ToString();
                txtRoll.Text = dt.Rows[0]["RollNo"].ToString();
                txtStudentName.Text = dt.Rows[0]["FullName"].ToString();
                ddlGender.Text = dt.Rows[0]["Gender"].ToString();
                txtDateOfBirth.Text = dt.Rows[0]["DateOfBirth"].ToString();
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                dlBloodGroup.Text = dt.Rows[0]["BloodGroup"].ToString();
                dlReligion.Text = dt.Rows[0]["Religion"].ToString();
                dlShift.SelectedValue = dt.Rows[0]["ConfigId"].ToString();
                txtFatherName.Text = dt.Rows[0]["FathersName"].ToString();
                ddlFatherOccupation.Text = dt.Rows[0]["FathersProfession"].ToString();
                txtFatherYearlyIncome.Text = dt.Rows[0]["FathersYearlyIncome"].ToString();
                txtMotherName.Text = dt.Rows[0]["MothersName"].ToString();
                ddlMotherOccupation.Text = dt.Rows[0]["MothersProfession"].ToString();
                txtMotherYearlyIncome.Text = dt.Rows[0]["MothersYearlyIncome"].ToString();
                txtFathersMobile.Text = dt.Rows[0]["FathersMobile"].ToString();
                txtMothersMobile.Text = dt.Rows[0]["MothersMoible"].ToString();
                txtHomePhone.Text = dt.Rows[0]["HomePhone"].ToString();
                txtFatherEmail.Text = dt.Rows[0]["FatherEmail"].ToString();
                txtMotherEmail.Text = dt.Rows[0]["MotherEmail"].ToString();
                txtPAVillage.Text = dt.Rows[0]["PAVillage"].ToString();
                //txtPAPostOffice.Text = dt.Rows[0]["PAPostOffice"].ToString();
                ddlPADistrict.SelectedValue = dt.Rows[0]["PDistrictId"].ToString();
                ThanaEntry.GetDropDownList(int.Parse(ddlPADistrict.SelectedValue), ddlPAThana);
                ddlPAThana.SelectedValue = dt.Rows[0]["PThanaId"].ToString();
                txtTAVillage.Text = dt.Rows[0]["TAViIlage"].ToString();
                //txtTAPostOffice.Text = dt.Rows[0]["TAPostOffice"].ToString();
                ddlTADistrict.SelectedValue = dt.Rows[0]["TDistrictId"].ToString();
                ThanaEntry.GetDropDownList(int.Parse(ddlTADistrict.SelectedValue), ddlTAThana);
                ddlTAThana.SelectedValue = dt.Rows[0]["TThanaId"].ToString();
                txtGuardianName.Text = dt.Rows[0]["GuardianName"].ToString();
                ddlRelation.SelectedItem.Text = dt.Rows[0]["GuardianRelation"].ToString();
                txtGurdianMobile.Text = dt.Rows[0]["GuardianMobileNo"].ToString();
                txtGuardianAddress.Text = dt.Rows[0]["GuardianAddress"].ToString();
                txtPreviousSchoolName.Text = dt.Rows[0]["PreviousSchoolName"].ToString();
                if (dt.Rows[0]["PreviousSchoolName"].ToString() == "") chkNotApplicable.Checked = true;
                else chkNotApplicable.Checked = false;
                //txtTransferCNo.Text = dt.Rows[0]["TransferCertifiedNo"].ToString();
                //txtTrDate.Text = dt.Rows[0]["CertifiedDate"].ToString();
                //ddlThatClass.SelectedItem.Text = dt.Rows[0]["PreferredClass"].ToString();
                // lblGpa.Text = dt.Rows[0]["PSCGPA"].ToString();
                //txtPSCRoll.Text = dt.Rows[0]["PSCRollNo"].ToString();
                txtGpa.Text = dt.Rows[0]["PSCGPA"].ToString();
                ddlBoard.Text = dt.Rows[0]["PSCBoard"].ToString();
                txtRegistration.Text = dt.Rows[0]["PSCJSCRegistration"].ToString();
                ddlSession.SelectedItem.Text = dt.Rows[0]["Session"].ToString();
                imageName = dt.Rows[0]["ImageName"].ToString();
                txtFullNameBn.Text = dt.Rows[0]["FullNameBn"].ToString();
                txtFatherNameBn.Text = dt.Rows[0]["FatherNameBn"].ToString();
                txtMotherNameBn.Text = dt.Rows[0]["MotherNameBn"].ToString();
                txtIdCard.Text = dt.Rows[0]["IdCard"].ToString();
                ddlStdType.SelectedValue = dt.Rows[0]["StdTypeId"].ToString();

                txtFatherDesignation.Text = dt.Rows[0]["FatherDesg"].ToString();
                txtFatherOrganization.Text = dt.Rows[0]["FatherOrg"].ToString();
                txtFP.Text = dt.Rows[0]["FatherPhone"].ToString();
                txtMotherDesignation.Text = dt.Rows[0]["MotherDesg"].ToString();
                txtMotherOrganization.Text = dt.Rows[0]["MotherOrg"].ToString();
                txtMP.Text = dt.Rows[0]["MotherPhone"].ToString();

                ddlBusName.SelectedValue = dt.Rows[0]["BusID"].ToString();
                ddlLocation.SelectedValue = dt.Rows[0]["LocationID"].ToString();
                ddlPlace.SelectedValue = dt.Rows[0]["PlaceID"].ToString();
                loadpostoffice(ddlPAPostOffice, ddlPADistrict.SelectedValue, ddlPAThana.SelectedValue);
                loadpostoffice(ddlTAPostOffice, ddlTADistrict.SelectedValue, ddlTAThana.SelectedValue);
                ddlPAPostOffice.SelectedValue = dt.Rows[0]["PAPostOfficeID"].ToString();
                ddlTAPostOffice.SelectedValue = dt.Rows[0]["TAPostOfficeID"].ToString();
                txtSSCRoll.Text = dt.Rows[0]["SSCRoll"].ToString();
                ddlTCClass.Text = dt.Rows[0]["TCClass"].ToString();
                ddlTCSemister.Text = dt.Rows[0]["TCSemister"].ToString();

                txtTCCollegeName.Text = dt.Rows[0]["TCCollegeName"].ToString();


                txtTCDate.Text = dt.Rows[0]["TCDate"].ToString();
                ddlExam.Text = dt.Rows[0]["PreviousExamType"].ToString();



                btnSave.Text = "Update";
                if (imageName != "")
                {
                    string url = @"/Images/profileImages/" + Path.GetFileName(dt.Rows[0]["ImageName"].ToString());
                    imgProfile.ImageUrl = url;
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }
        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            BatchEntry.GetDropdownlist(ddlBatch, int.Parse(ddlClass.SelectedValue.ToString()), true);
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(ddlClass.SelectedValue), ddlGroup);
            ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(ddlClass.SelectedValue), ddlGroup.SelectedValue);

            //ddlThatClass.SelectedValue = ddlClass.SelectedValue;
        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(ddlClass.SelectedValue), ddlGroup.SelectedValue);
        }
        protected void chkFather_CheckedChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            chkOther.Checked = false;
            chkMother.Checked = false;
            txtGuardianName.Text = txtFatherName.Text;
            ddlRelation.Text = "Father";
            txtGurdianMobile.Text = txtFathersMobile.Text;
        }
        protected void chkMother_CheckedChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            chkOther.Checked = false;
            chkFather.Checked = false;
            txtGuardianName.Text = txtMotherName.Text;
            ddlRelation.Text = "Mother";
            txtGurdianMobile.Text = txtMothersMobile.Text;
        }
        protected void chkOther_CheckedChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            chkMother.Checked = false;
            chkFather.Checked = false;
            txtGuardianName.Text = "";
            txtGurdianMobile.Text = "";
        }
        protected void ddlPADistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            ThanaEntry.GetDropDownList(int.Parse(ddlPADistrict.SelectedValue), ddlPAThana);
        }
        protected void ddlTADistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            ThanaEntry.GetDropDownList(int.Parse(ddlTADistrict.SelectedValue), ddlTAThana);
        }
        protected void chkSameAddress_CheckedChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            try
            {
                if (chkSameAddress.Checked)
                {
                    txtTAVillage.Text = txtPAVillage.Text;
                    ddlTADistrict.SelectedValue = ddlPADistrict.SelectedValue;
                    ThanaEntry.GetDropDownList(int.Parse(ddlTADistrict.SelectedValue), ddlTAThana);
                    ddlTAThana.SelectedValue = ddlPAThana.SelectedValue;
                    loadpostoffice(ddlTAPostOffice, ddlTADistrict.SelectedValue, ddlTAThana.SelectedValue);
                    ddlTAPostOffice.SelectedValue = ddlPAPostOffice.SelectedValue;
                }
                else
                {
                    txtTAVillage.Text = "";
                    ddlTAPostOffice.SelectedValue = "0";
                    ddlTAThana.SelectedValue = "0";
                    ddlTADistrict.SelectedValue = "0";

                }
            }
            catch { }
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
            }
            catch { }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            if (btnSave.Text == "Save")
            {
                if (SaveValidation() == false)
                {
                    lblMessage.InnerText = "warning-> This(" + txtRoll.Text + ") Roll Already Assign";
                    txtRoll.Focus();
                    return;
                }
                if (txtGurdianMobile.Text.Trim().Length != 11)
                {
                    lblMessage.InnerText = "warning-> Mobile No Must be 11 Digits";
                    txtGurdianMobile.Focus();
                    return;
                }
                //if (!"017,019,018,016,015".Contains(txtGurdianMobile.Text.Trim().Substring(0, 3)))
                //{
                //    lblMessage.InnerText = "warning-> Mobile No is Invalid";
                //    txtGurdianMobile.Focus();
                //    return ;
                //}
                //  if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; return; }
                if (saveStudentProfile() == true)
                {
                    //clearInputs();
                    Session["__MsgoldStdInfo__"] = "success->Successfully saved";
                    Response.Redirect("~/UI/Academic/Students/OldStudentEntry.aspx");
                    // lblMessage.InnerText = "success->Successfully saved";
                }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtGurdianMobile.Text.Trim().Length != 11)
                {
                    lblMessage.InnerText = "warning->Gurdian Mobile No Must be 11 Digits";
                    txtGurdianMobile.Focus();
                    return;
                }
                //if (!"017,019,018,016,015".Contains(txtGurdianMobile.Text.Trim().Substring(0, 3)))
                //{
                //    lblMessage.InnerText = "warning->Gurdian Mobile No is Invalid";
                //    txtGurdianMobile.Focus();
                //    return;
                //}

                if (UpdateStudentProfile() == true)
                {
                    clearInputs();
                    Response.Redirect("~/UI/Academic/Students/CurrentStudentInfo.aspx");
                    lblMessage.InnerText = "success->Successfully Updated";
                }
            }
        }
        private Boolean SaveValidation()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("SELECT StudentId FROM CurrentStudentInfo WHERE ConfigId='" + dlShift.SelectedValue + "' AND RollNo='"
                    + txtRoll.Text + "' And BatchID='" + ddlBatch.SelectedValue + "' AND ClsGrpID='"
                    + ddlGroup.SelectedValue + "' AND ClsSecID='" + ddlSection.SelectedValue + "' ");
                if (dt.Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch { return false; }
        }
        private Boolean saveStudentProfile()
        {
            try
            {
                using (CurrentStdEntities entities = GetFormData())
                {
                    if (EntryStudent == null)
                    {
                        EntryStudent = new CurrentStdEntry();
                    }
                    EntryStudent.AddEntities = entities;
                    int result = EntryStudent.Insert();
                    if (result >0)
                    {
                        saveImg(); // for student photo save                       
                        clearInputs();
                    }
                    else
                    {
                        lblMessage.InnerText = "error->Unable to save";
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private CurrentStdEntities GetFormData()
        {
            if (btnSave.Text == "Save")
            {
                txtAdmissionNo.Text = CurrentStdEntry.GetMaxID().ToString();
            }
            CurrentStdEntities admissionEntry = new CurrentStdEntities();
            admissionEntry.StudentID = int.Parse(lblstdId.Value);
            admissionEntry.AdmissionNo = int.Parse(txtAdmissionNo.Text.Trim());
            //  admissionEntry.AdmissionDate = convertDateTime.getCertainCulture(convertDateTime.sDMYtoYMD(txtAdmissionDate.Text.Trim()));
            // admissionEntry.AdmissionDate = convertDateTime.getCertainCulture(txtAdmissionDate.Text.Trim());
            admissionEntry.AdmissionDate = convertDateTime.DMYtoYMD(txtAdmissionDate.Text.Trim());

            admissionEntry.FullName = txtStudentName.Text.Trim();
            admissionEntry.ClassID = int.Parse(ddlClass.SelectedValue);
            admissionEntry.ClassName = ddlClass.SelectedItem.Text;
            admissionEntry.ClsGrpID = int.Parse(ddlGroup.SelectedValue);
            admissionEntry.ClsSecID = int.Parse(ddlSection.SelectedValue);
            if (ddlSection.SelectedValue == "0") admissionEntry.SectionName = "";
            else admissionEntry.SectionName = ddlSection.SelectedItem.Text;
            if (txtRoll.Text == "") admissionEntry.RollNo = 0;
            else admissionEntry.RollNo = int.Parse(txtRoll.Text);
            admissionEntry.Religion = dlReligion.SelectedItem.Text;
            admissionEntry.ConfigId = int.Parse(dlShift.SelectedValue);
            admissionEntry.Shift = dlShift.SelectedItem.Text;
            if (txtDateOfBirth.Text == "") admissionEntry.DateOfBirth = null;
            else admissionEntry.DateOfBirth = convertDateTime.DMYtoYMD(txtDateOfBirth.Text.Trim());
            //  else admissionEntry.DateOfBirth = convertDateTime.getCertainCulture(convertDateTime.sDMYtoYMD(txtDateOfBirth.Text.Trim()));             
            //  else admissionEntry.DateOfBirth = convertDateTime.getCertainCulture(txtDateOfBirth.Text.Trim());

            admissionEntry.Gender = ddlGender.SelectedItem.Text;
            admissionEntry.Mobile = lblMobile.Text + txtMobile.Text.Trim();
            admissionEntry.Session = ddlSession.SelectedItem.Text;
            admissionEntry.BloodGroup = dlBloodGroup.SelectedItem.Text;
            admissionEntry.FathersName = txtFatherName.Text.Trim();
            admissionEntry.FathersProfession = ddlFatherOccupation.Text;
            admissionEntry.FathersYearlyIncome = int.Parse(txtFatherYearlyIncome.Text.Trim());
            admissionEntry.FathersMobile = txtFM.Text + txtFathersMobile.Text.Trim();
            admissionEntry.FatherEmail = txtFatherEmail.Text.Trim();
            admissionEntry.MothersName = txtMotherName.Text.Trim();
            admissionEntry.MothersProfession = ddlMotherOccupation.Text.Trim();
            admissionEntry.MothersYearlyIncome = int.Parse(txtMotherYearlyIncome.Text.Trim());
            admissionEntry.MothersMobile = txtMM.Text + txtMothersMobile.Text.Trim();
            admissionEntry.MotherEmail = txtMotherEmail.Text.Trim();
            admissionEntry.HomePhone = txtHP.Text + txtHomePhone.Text.Trim();
            admissionEntry.GuardianName = txtGuardianName.Text.Trim();
            admissionEntry.GuardianRelation = ddlRelation.SelectedItem.Text;
            admissionEntry.GuardianMobileNo = lblgdMobile.Text + txtGurdianMobile.Text.Trim();
            admissionEntry.GuardianAddress = txtGuardianAddress.Text.Trim();
            admissionEntry.PAVillage = txtPAVillage.Text.Trim();
            //admissionEntry.PAPostOffice = txtPAPostOffice.Text.Trim();
            if (ddlPAThana.SelectedValue == "0")
            {
                admissionEntry.PThanaId = 0;
                admissionEntry.PAThana = "";
            }
            else
            {
                admissionEntry.PThanaId = int.Parse(ddlPAThana.SelectedValue);
                admissionEntry.PAThana = ddlPAThana.SelectedItem.Text;
            }
            if (ddlPADistrict.SelectedValue == "0")
            {
                admissionEntry.PDistrictId = 0;
                admissionEntry.PADistrict = "";
            }
            else
            {
                admissionEntry.PDistrictId = int.Parse(ddlPADistrict.SelectedValue);
                admissionEntry.PADistrict = ddlPADistrict.SelectedItem.Text;
            }
            admissionEntry.TAViIlage = txtTAVillage.Text.Trim();
            //admissionEntry.TAPostOffice = txtPAPostOffice.Text.Trim();
            if (ddlTAThana.SelectedValue == "0")
            {
                admissionEntry.TThanaId = 0;
                admissionEntry.TAThana = "";
            }
            else
            {
                admissionEntry.TThanaId = int.Parse(ddlTAThana.SelectedValue);
                admissionEntry.TAThana = ddlTAThana.SelectedItem.Text.Trim();
            }
            if (ddlTADistrict.SelectedValue == "0")
            {
                admissionEntry.TDistrictId = 0;
                admissionEntry.TADistrict = "";
            }
            else
            {
                admissionEntry.TDistrictId = int.Parse(ddlTADistrict.SelectedValue);
                admissionEntry.TADistrict = ddlTADistrict.SelectedItem.Text.Trim();
            }
            admissionEntry.MotherTongue = "Bangla";
            admissionEntry.Nationality = "Bangladeshi";
            admissionEntry.PreviousExamType = ddlExam.SelectedItem.Text;
            //if (txtPSCRoll.Text == "") admissionEntry.PSCRollNo = 0;
            //else admissionEntry.PSCRollNo = int.Parse(txtPSCRoll.Text.Trim());
            admissionEntry.PSCPassingYear = int.Parse(ddlPassingYear.SelectedItem.Text);
            if (txtGpa.Text == "") admissionEntry.PSCGPA = 0;
            else admissionEntry.PSCGPA = double.Parse(txtGpa.Text.Trim());
            admissionEntry.PSCBoard = ddlBoard.Text;
            //if (txtTrDate.Text == "") admissionEntry.CertifiedDate = null;
            if (txtRoll.Text == "") admissionEntry.RollNo = 0;
            else admissionEntry.RollNo = int.Parse(txtRoll.Text);
            //if (txtTrDate.Text == "") admissionEntry.CertifiedDate = null;
            //else admissionEntry.CertifiedDate = convertDateTime.getCertainCulture(txtTrDate.Text.Trim());
            admissionEntry.PreviousSchoolName = txtPreviousSchoolName.Text.Trim();
            admissionEntry.PSCJSCRegistration = txtRegistration.Text.Trim();
            //if (txtTransferCNo.Text == "") admissionEntry.TransferCertifiedNo = 0;
            //else admissionEntry.TransferCertifiedNo = int.Parse(txtTransferCNo.Text.Trim());
            //if (ddlThatClass.SelectedValue == "0") admissionEntry.PreferredClass = "";
            //else admissionEntry.PreferredClass = ddlThatClass.SelectedItem.Text.Trim();
            admissionEntry.IsActive = true;
            admissionEntry.Comments = "";
            admissionEntry.BatchID = int.Parse(ddlBatch.SelectedValue);
            admissionEntry.BatchName = ddlBatch.SelectedItem.Text.Trim();
            admissionEntry.Status = "Old";
            admissionEntry.PaymentStatus = true;
            admissionEntry.StartBatchID = int.Parse(ddlBatch.SelectedValue);
            if (FileUpload1.HasFile == true)
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                admissionEntry.ImageName = txtAdmissionNo.Text + filename;
            }
            else
            {
                if (btnSave.Text == "Save")
                    admissionEntry.ImageName = "";
                else admissionEntry.ImageName = imageName;
            }
            admissionEntry.FullNameBn = txtFullNameBn.Text;
            admissionEntry.FathersNameBn = txtFatherNameBn.Text;
            admissionEntry.MothersNameBn = txtMotherNameBn.Text;
            admissionEntry.IdCard = txtRoll.Text;
            admissionEntry.StdTypeId = int.Parse(ddlStdType.SelectedValue);
            admissionEntry.FatherDesg = txtFatherDesignation.Text.Trim();
            admissionEntry.FatherOrg = txtFatherOrganization.Text.Trim();
            admissionEntry.FatherPhone = txtFP.Text.Trim();
            admissionEntry.MotherDesg = txtMotherDesignation.Text;
            admissionEntry.MotherOrg = txtMotherOrganization.Text;
            admissionEntry.MotherPhone = txtMP.Text;
            admissionEntry.BusID = int.Parse(ddlBusName.SelectedValue);
            admissionEntry.LocationID = int.Parse(ddlLocation.SelectedValue);
            admissionEntry.PlaceID = int.Parse(ddlPlace.SelectedValue);
            admissionEntry.TAPostOfficeID = int.Parse(ddlTAPostOffice.SelectedValue);
            admissionEntry.PAPostOfficeID = int.Parse(ddlPAPostOffice.SelectedValue);
            if (txtSSCRoll.Text == "")
            {
                admissionEntry.SSCRoll = 0;
            }
            else
                admissionEntry.SSCRoll = int.Parse(txtSSCRoll.Text);
            admissionEntry.FatherOrg = txtFatherOrganization.Text.Trim();
            admissionEntry.TCClass = ddlTCClass.SelectedItem.Text;
            admissionEntry.TCSemister = ddlTCSemister.SelectedItem.Text;
            admissionEntry.TCCollegeName = txtTCCollegeName.Text.Trim();
            if (txtTCDate.Text == "") admissionEntry.TCDate = null;
            else admissionEntry.TCDate = convertDateTime.DMYtoYMD(txtTCDate.Text.Trim());
            admissionEntry.CreateBy = int.Parse(Session["__UserId__"].ToString());
            admissionEntry.CreateOn = DateTime.Now;
            //  else admissionEntry.TCDate = convertDateTime.getCertainCulture(txtTCDate.Text.Trim());

            return admissionEntry;
        }
        private void saveImg()
        {
            try
            {
                //Get Filename from fileupload control
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //Save images into Images folder
                System.Drawing.Image image = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                int width = 100;
                int height = 100;
                using (System.Drawing.Image thumbnail = image.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        thumbnail.Save(Server.MapPath("/Images/profileImages/" + txtAdmissionNo.Text.Trim() + filename), System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
                //FileUpload1.SaveAs(Server.MapPath("/Images/profileImages/" + txtAdmissionNo.Text.Trim() + filename));

            }
            catch { }
        }
        public bool ThumbnailCallback()
        {
            return false;
        }
        private Boolean UpdateStudentProfile()
        {
            try
            {
                using (CurrentStdEntities entities = GetFormData())
                {
                    if (EntryStudent == null)
                    {
                        EntryStudent = new CurrentStdEntry();
                    }
                    EntryStudent.AddEntities = entities;
                    bool result = EntryStudent.Update();
                    if (result == true)
                    {
                        if (FileUpload1.HasFile == true)
                        {
                            if (imageName != "")
                                System.IO.File.Delete(Request.PhysicalApplicationPath + "/Images/profileImages/" + imageName);
                            saveImg(); // for student photo save
                        }
                        clearInputs();
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "saveSuccess();", true);
                    }
                    else
                    {
                        lblMessage.InnerText = "error->Unable to update";
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private void clearInputs()
        {
            try
            {
                txtAdmissionNo.Text = CurrentStdEntry.GetMaxID().ToString();
                txtAdmissionDate.Text = "";
                txtRoll.Text = "";
                txtStudentName.Text = "";
                txtDateOfBirth.Text = "";
                txtMobile.Text = "";
                txtFatherName.Text = "";
                //txtFatherOccupation.Text = "";
                txtFatherYearlyIncome.Text = "";
                txtMotherName.Text = "";
                //txtMotherOccupation.Text = "";
                txtMotherYearlyIncome.Text = "";
                txtFathersMobile.Text = "";
                txtMothersMobile.Text = "";
                txtHomePhone.Text = "";
                txtPAVillage.Text = "";

                txtTAVillage.Text = "";

                txtGuardianName.Text = "";
                txtGurdianMobile.Text = "";
                txtGuardianAddress.Text = "";
                txtPreviousSchoolName.Text = "";
                //txtTransferCNo.Text = "";
                //txtTrDate.Text = "";
                txtFatherEmail.Text = "";
                txtMotherEmail.Text = "";
                //txtPSCRoll.Text = "";
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
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            clearInputs();
        }

        protected void ddlPAThana_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            loadpostoffice(ddlPAPostOffice, ddlPADistrict.SelectedValue, ddlPAThana.SelectedValue);
        }
        private void loadpostoffice(DropDownList dl, string did, string tid)
        {
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull("select PostOfficeID,PostOfficeName from Post_Office where DistrictId='" + did + "' and ThanaId='" + tid + "'");
            dl.DataTextField = "PostOfficeName";
            dl.DataValueField = "PostOfficeID";
            dl.DataSource = dt;
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
        }

        protected void ddlPAPostOffice_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            loadpostoffice(ddlTAPostOffice, ddlTADistrict.SelectedValue, ddlTAThana.SelectedValue);
        }

        protected void ddlTAThana_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            loadpostoffice(ddlTAPostOffice, ddlTADistrict.SelectedValue, ddlTAThana.SelectedValue);
        }

        protected void txtRoll_TextChanged(object sender, EventArgs e)
        {
            txtIdCard.Text = txtRoll.Text;
        }

        protected void chkTCNotApplicable_CheckedChanged(object sender, EventArgs e)
        {
            chkTCFrom.Checked = false;
            chkTCTo.Checked = false;
        }

        protected void chkTCTo_CheckedChanged(object sender, EventArgs e)
        {
            chkTCFrom.Checked = false;
            chkTCNotApplicable.Checked = false;

        }

        protected void chkTCFrom_CheckedChanged(object sender, EventArgs e)
        {
            chkTCNotApplicable.Checked = false;
            chkTCTo.Checked = false;
        }
    }
}