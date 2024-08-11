using adviitRuntimeScripting;
using ComplexScriptingSystem;
using DS.BLL;
using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedClass;
using DS.Classes;
using DS.DAL;
using DS.PropertyEntities.Model.Admission;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Routing;

namespace DS.UI.Academic.Students
{
    public partial class student_entry : System.Web.UI.Page
    {
        StdAdmFormEntry stdAdmFormEntry;
        ClassGroupEntry clsgrpEntry;
        CurrentStdEntry currentStdEntry;
        
        CurrentStdEntities entities;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                //---url bind---
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aAcademicHome.HRef = "~/" + Classes.Routing.AcademicRouteUrl;
                aStudentHome.HRef = "~/" + Classes.Routing.StudentHomeRouteUrl;
                //---url bind end---




                string EditMode = "No";
                ViewState["__requestFrom__"] = "";
                string[] path = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
              

                //try { EditMode = (Request.QueryString["StudentId"].ToString() != null) ? "Yes" : "No"; }
                //catch { }
                try {
                    ViewState["__requestFrom__"] = Request.QueryString["requestFrom"].ToString();
                } catch {
                    ViewState["__requestFrom__"] = "";
                }
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "OldStudentEntry.aspx",btnSubmit, EditMode)) Response.Redirect(Request.UrlReferrer.ToString() + "&hasperm=no");
                txtAdmissionDate.Text = TimeZoneBD.getCurrentTimeBD().ToString("dd-MM-yyyy");
                ClassEntry.GetEntitiesData(ddlClass);
                DropDownList[] ddlDistrict = { ddlPermanentDistrict, ddlPresentDistrict, ddlParentsDistrict };
                DistrictEntry.GetDropDownList(ddlDistrict);
                ShiftEntry.GetShiftList(ddlShift);
                commonTask.loadPassingYearForAdmission(ddlPreviousExamPassingYear);
                commonTask.loadYearFromBatch(ddlYear);
                commonTask.loadBoard(ddlPreviousExamBoard);
                //while (true)
                //{
                //    commonTask.loadBoard(ddlPreviousExamBoard);
                //    if (ddlPreviousExamBoard == null || ddlPreviousExamBoard.Items.Count == 0)
                //    {
                //        commonTask.loadBoard(ddlPreviousExamBoard);
                //    }
                //    else
                //        break;
                //}


                if (path.Contains("edit"))
                {
                    EditMode = "Yes";
                   // string sid = path[path.Length - 1].ToString();
                    string sid =RouteData.Values["id"].ToString();
                    sid = commonTask.Base64Decode(sid);
                    ViewState["__StudentID__"] = sid;
                    string requestFrom = RouteData.Values["requestFrom"].ToString();
                    requestFrom = commonTask.Base64Decode(requestFrom);
                    ViewState["__requestFrom__"] = requestFrom;
                    load_studentinfo();
                }

            }
        }
        private void load_studentinfo()
        {
            try
            {
                //string stid = Request.QueryString["StudentId"];
                string stid = ViewState["__StudentID__"].ToString();
               
                if (stid == null) return;

                DataTable dt = new DataTable();
                
              dt=CRUD.ReturnTableNull(@"Select cs.BatchID,cs.StudentId,asi.AdmissionNo,ImageName, FullName,FullNameBn, Gender,Format(DateOfBirth,'dd-MM-yyyy') as DateOfBirth,Religion, BloodGroup,SUBSTRING(Mobile,4,12)as Mobile,ConfigId,b.Year, cs.ClassID,cs.ClsGrpID, cs.ClsSecID, cs.RollNo,Format(AdmissionDate,'dd-MM-yyyy') as AdmissionDate,FathersName,FatherNameBn,SUBSTRING(FathersMobile,4,12)as FathersMobile,FathersProfession,FathersProfessionBn,MothersName,MotherNameBn,SUBSTRING(MothersMoible,4,12)as MothersMoible, MothersProfession,MothersProfessionBn,ParentsDistrictId,ParentsThanaId,ParentsPostOfficeId,ParentsAddress,ParentsAddressBn, PAVillage,PAVillageBn, PAPostOfficeID, PThanaId,PDistrictId, TAViIlage,TAViIlageBn,TAPostOfficeID, TThanaId, TDistrictId, GuardianName, GuardianRelation, REPLACE(GuardianMobileNo,'+88', '')  as GuardianMobileNo, GuardianAddress, MotherTongue, Nationality, PreviousSchoolName, PSCGPA, PSCRollNo, PSCBoard, PSCPassingYear, PSCJSCRegistration,IsActive, Comments,cs.TCCollegeName,case when convert(varchar(10),cs.TCDate,105)='01-01-1900' then '' else convert(varchar(10),cs.TCDate,105) end as TCDate,cs.PreviousExamType 
                From CurrentStudentInfo cs INNER JOIN  TBL_STD_Admission_INFO asi ON cs.StudentId = asi.StudentID inner join BatchInfo b on cs.BatchID=b.BatchId  where cs.StudentId=" + stid);

                ViewState["__AdmissionFormNo__"] = dt.Rows[0]["AdmissionNo"].ToString();
                ViewState["__StudentID__"]= dt.Rows[0]["StudentId"].ToString();
                ViewState["__ImageName__"] = dt.Rows[0]["ImageName"].ToString();
                txtStudentName.Text = dt.Rows[0]["FullName"].ToString();
                txtStudentNameBn.Text = dt.Rows[0]["FullNameBn"].ToString();
                ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
                txtDateOfBirth.Text = dt.Rows[0]["DateOfBirth"].ToString();
                ddlReligion.SelectedValue = dt.Rows[0]["Religion"].ToString();
                ddlBloodGroup.SelectedValue = dt.Rows[0]["BloodGroup"].ToString();
                txtStudentMobile.Text = dt.Rows[0]["Mobile"].ToString();
                ddlShift.SelectedValue = dt.Rows[0]["ConfigId"].ToString();
                ddlYear.SelectedValue= dt.Rows[0]["Year"].ToString();
                ddlClass.SelectedValue = dt.Rows[0]["ClassID"].ToString();
                if (clsgrpEntry == null)
                {
                    clsgrpEntry = new ClassGroupEntry();
                }
                clsgrpEntry.GetDropDownListClsGrpId(int.Parse(ddlClass.SelectedValue), ddlGroup);
                ddlGroup.SelectedValue = dt.Rows[0]["ClsGrpID"].ToString();
                ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(ddlClass.SelectedValue), ddlGroup.SelectedValue);
                ddlSection.SelectedValue = dt.Rows[0]["ClsSecID"].ToString();
                txtRollNo.Text = dt.Rows[0]["RollNo"].ToString();
                txtAdmissionDate.Text = dt.Rows[0]["AdmissionDate"].ToString();

                txtFatherName.Text = dt.Rows[0]["FathersName"].ToString();
                txtFatherNameBn.Text = dt.Rows[0]["FatherNameBn"].ToString();
                txtFatherMobile.Text = dt.Rows[0]["FathersMobile"].ToString();                
                txtFatherOccupation.Text = dt.Rows[0]["FathersProfession"].ToString();
                txtFatherOccupationBn.Text = dt.Rows[0]["FathersProfessionBn"].ToString();
                
                txtMotherName.Text = dt.Rows[0]["MothersName"].ToString();
                txtMotherNameBn.Text = dt.Rows[0]["MotherNameBn"].ToString();
                txtMotherMobile.Text = dt.Rows[0]["MothersMoible"].ToString();
                txtMotherOccupation.Text = dt.Rows[0]["MothersProfession"].ToString();
                txtMotherOccupationBn.Text = dt.Rows[0]["MothersProfessionBn"].ToString();

                ddlParentsDistrict.SelectedValue =dt.Rows[0]["ParentsDistrictId"].ToString();
                ThanaEntry.GetDropDownList(int.Parse(ddlParentsDistrict.SelectedValue), ddlParentsUpazila);
                ddlParentsUpazila.SelectedValue = dt.Rows[0]["ParentsThanaId"].ToString();
                Classes.commonTask.loadPostoffice(ddlParentsPostOffice, ddlParentsDistrict.SelectedValue, ddlParentsUpazila.SelectedValue);
                ddlParentsPostOffice.SelectedValue =dt.Rows[0]["ParentsPostOfficeId"].ToString();
                txtParentsVillage.Text = dt.Rows[0]["ParentsAddress"].ToString();
                txtParentsVillageBn.Text = dt.Rows[0]["ParentsAddressBn"].ToString();

                txtGuardianName.Text = dt.Rows[0]["GuardianName"].ToString();
                txtGuardianRelation.Text = dt.Rows[0]["GuardianRelation"].ToString();
                txtGuardianMobile.Text = dt.Rows[0]["GuardianMobileNo"].ToString();
                txtGuardianAddress.Text = dt.Rows[0]["GuardianAddress"].ToString();

                ddlPermanentDistrict.SelectedValue = dt.Rows[0]["PDistrictId"].ToString();
                ThanaEntry.GetDropDownList(int.Parse(ddlPermanentDistrict.SelectedValue), ddlPermanentUpazila);
                ddlPermanentUpazila.SelectedValue = dt.Rows[0]["PThanaId"].ToString();
                Classes.commonTask.loadPostoffice(ddlPermanentPostOffice, ddlPermanentDistrict.SelectedValue, ddlPermanentUpazila.SelectedValue);
                ddlPermanentPostOffice.SelectedValue = dt.Rows[0]["PAPostOfficeID"].ToString();
                txtPermanentVillage.Text = dt.Rows[0]["PAVillage"].ToString();
                txtPermanentVillageBn.Text = dt.Rows[0]["PAVillageBn"].ToString();

                ddlPresentDistrict.SelectedValue = dt.Rows[0]["TDistrictId"].ToString();
                ThanaEntry.GetDropDownList(int.Parse(ddlPresentDistrict.SelectedValue), ddlPresentUpazila);
                ddlPresentUpazila.SelectedValue = dt.Rows[0]["TThanaId"].ToString();
                Classes.commonTask.loadPostoffice(ddlPresentPostOffice, ddlPresentDistrict.SelectedValue, ddlPresentUpazila.SelectedValue);
                ddlPresentPostOffice.SelectedValue = dt.Rows[0]["TAPostOfficeID"].ToString();
                txtPresentVillage.Text = dt.Rows[0]["TAViIlage"].ToString();
                txtPresentVillageBn.Text = dt.Rows[0]["TAViIlageBn"].ToString();

                
                
               
                if (dt.Rows[0]["PreviousSchoolName"].ToString() == "")ckbPreviousInstituteInfo.Checked = true;
                else ckbPreviousInstituteInfo.Checked = false;
                txtPreviousExamSchoolName.Text = dt.Rows[0]["PreviousSchoolName"].ToString();
                ddlPreviousExamBoard.SelectedValue = dt.Rows[0]["PSCBoard"].ToString();
                ddlPreviousExamPassingYear.SelectedValue = dt.Rows[0]["PSCPassingYear"].ToString();
                txtPreviousExamRegistrationNo.Text = dt.Rows[0]["PSCJSCRegistration"].ToString();
                txtPreviousExamRollNo.Text= dt.Rows[0]["PSCRollNo"].ToString();
                txtPreviousExamGPA.Text = dt.Rows[0]["PSCGPA"].ToString();

                if (dt.Rows[0]["TCCollegeName"].ToString().Trim() == "") ckbTCInfo.Checked = true;
                else ckbTCInfo.Checked = false;

                txtTCCollegeName.Text = dt.Rows[0]["TCCollegeName"].ToString();
                txtTCDate.Text = dt.Rows[0]["TCDate"].ToString();
                btnSubmit.Text= "Update";
                btnClear.Visible = false;
                if (dt.Rows[0]["ImageName"].ToString() != "")
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
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(ddlClass.SelectedValue), ddlGroup);
            if (ddlGroup.SelectedValue != "0")
                ClassSectionEntry.GetSectionList(ddlSection, int.Parse(ddlClass.SelectedValue), ddlGroup.SelectedValue);
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassSectionEntry.GetSectionList(ddlSection, int.Parse(ddlClass.SelectedValue), ddlGroup.SelectedValue);
        }

        protected void ddlPermanentDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThanaEntry.GetDropDownList(int.Parse(ddlPermanentDistrict.SelectedValue), ddlPermanentUpazila);
        }

        protected void ddlPermanentUpazila_SelectedIndexChanged(object sender, EventArgs e)
        {
            Classes.commonTask.loadPostoffice(ddlPermanentPostOffice, ddlPermanentDistrict.SelectedValue, ddlPermanentUpazila.SelectedValue);
        }

        protected void ddlPresentDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThanaEntry.GetDropDownList(int.Parse(ddlPresentDistrict.SelectedValue), ddlPresentUpazila);
        }

        protected void ddlPresentUpazila_SelectedIndexChanged(object sender, EventArgs e)
        {
            Classes.commonTask.loadPostoffice(ddlPresentPostOffice, ddlPresentDistrict.SelectedValue, ddlPresentUpazila.SelectedValue);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                DateTime dateOfBirth = DateTime.Parse(commonTask.ddMMyyyyToyyyyMMdd(txtDateOfBirth.Text.Trim()));
                ViewState["__dateOfBirth__"] = dateOfBirth.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {

                lblMessage.InnerText = "error-> Please,enter valid Date of Birth !";
                txtDateOfBirth.Focus();
                return;
            }
            try
            {
                DateTime admissionDate = DateTime.Parse(commonTask.ddMMyyyyToyyyyMMdd(txtAdmissionDate.Text.Trim()));
                ViewState["__admissionDate__"] = admissionDate.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {

                lblMessage.InnerText = "error-> Please,enter valid Admission Date !";
                txtAdmissionDate.Focus();
                return;
            }
            if (!ckbTCInfo.Checked)
            {
                try
                {
                    ViewState["__tcDate__"] = "";
                    if (txtTCDate.Text.Trim() != "")
                    {
                        DateTime tcDate = DateTime.Parse(commonTask.ddMMyyyyToyyyyMMdd(txtTCDate.Text.Trim()));
                        ViewState["__tcDate__"] = tcDate.ToString("yyyy-MM-dd");
                    }

                }
                catch (Exception ex)
                {

                    lblMessage.InnerText = "error-> Please,enter valid TC Date !";
                    txtTCDate.Focus();
                    return;
                }
            }
            if (btnSubmit.Text.Trim() == "Save")
            {
                if (save_into_currentstudent())
                {
                    allClear();
                    lblMessage.InnerText = "success-> Successfully Saved.";
                }
                    
            }                
            else
            {
                if (update_into_currentstudent())
                {

                    if (ViewState["__requestFrom__"].ToString().Equals("Profile"))
                    {
                        string StudentId = ViewState["__StudentID__"].ToString();
                        StudentId = commonTask.Base64Encode(StudentId);
                        Response.Redirect(GetRouteUrl("StudentProfileRoute", new { id = StudentId }));
                        //Response.Redirect("~/UI/Academic/Students/StdProfile.aspx?StudentId=" + ViewState["__StudentID__"].ToString());
                    }

                    else
                    {
                        Response.Redirect("~/" + Classes.Routing.StudentListRouteUrl);
                       //Response.Redirect("~/UI/Academic/Students/CurrentStudentInfo.aspx");
                    }
                        

                }
                    
            }
            
        }
        

        

        protected void ddlParentsDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThanaEntry.GetDropDownList(int.Parse(ddlParentsDistrict.SelectedValue), ddlParentsUpazila);
        }

        protected void ddlParentsUpazila_SelectedIndexChanged(object sender, EventArgs e)
        {
            Classes.commonTask.loadPostoffice(ddlParentsPostOffice, ddlParentsDistrict.SelectedValue, ddlParentsUpazila.SelectedValue);
        }
     

        protected void chkFather_CheckedChanged(object sender, EventArgs e)
        {
            chkOther.Checked = false;
            chkMother.Checked = false;
            txtGuardianName.Text = txtFatherName.Text.Trim();
            txtGuardianRelation.Text = "Father";
            txtGuardianMobile.Text = txtFatherMobile.Text.Trim();
            set_guardian_address();
        }
        private void set_guardian_address()
        {
            try
            {
                txtGuardianAddress.Text = txtParentsVillage.Text.Trim() + ((!ddlParentsPostOffice.SelectedValue.Equals("0")) ? "," + ddlParentsPostOffice.SelectedItem.Text.Trim() : "") + ((!ddlParentsUpazila.SelectedValue.Equals("0")) ? "," + ddlParentsUpazila.SelectedItem.Text.Trim() : "") + ((!ddlParentsDistrict.SelectedValue.Equals("0")) ? "," + ddlParentsDistrict.SelectedItem.Text.Trim() : "");
            }
            catch (Exception ex) { }
        }

        protected void chkMother_CheckedChanged(object sender, EventArgs e)
        {
            chkOther.Checked = false;
            chkFather.Checked = false;
            txtGuardianName.Text = txtMotherName.Text;
            txtGuardianRelation.Text = "Mother";
            txtGuardianMobile.Text = txtMotherMobile.Text.Trim();
            set_guardian_address();
        }

        protected void chkOther_CheckedChanged(object sender, EventArgs e)
        {
            chkFather.Checked = false;
            chkMother.Checked = false;
            txtGuardianName.Text = "";
            txtGuardianRelation.Text = "";
            txtGuardianMobile.Text = "";
            txtGuardianAddress.Text = "";
        }

        protected void ckbSameAsPermanentAddress_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckbSameAsPermanentAddress.Checked)
                {
                    txtPresentVillage.Text = txtPermanentVillage.Text.Trim();
                    txtPresentVillageBn.Text = txtPermanentVillageBn.Text.Trim();
                    ddlPresentDistrict.SelectedValue = ddlPermanentDistrict.SelectedValue;
                    ThanaEntry.GetDropDownList(int.Parse(ddlPresentDistrict.SelectedValue), ddlPresentUpazila);
                    ddlPresentUpazila.SelectedValue = ddlPermanentUpazila.SelectedValue;
                    commonTask.loadPostoffice(ddlPresentPostOffice, ddlPresentDistrict.SelectedValue, ddlPresentUpazila.SelectedValue);
                    ddlPresentPostOffice.SelectedValue = ddlPermanentPostOffice.SelectedValue;
                }                
            }
            catch { }
        }
        private void preview(string sl)
        {
            Response.Redirect("admission-form.aspx?SL=" + sl);
        }

        private bool save_into_currentstudent()
        {
            try
            {
                using (CurrentStdEntities entities = get_form_data())
                {
                    if (currentStdEntry == null)
                    {
                        currentStdEntry = new CurrentStdEntry();
                    }
                    currentStdEntry.AddEntities = entities;
                    int sl = currentStdEntry.Insert();
                    if (sl >0)
                    {
                        saveImg(sl,entities.AdmissionNo.ToString());
                    }
                    else
                    {
                        if ( stdAdmFormEntry  == null)
                        {
                            stdAdmFormEntry = new StdAdmFormEntry();
                        }
                        stdAdmFormEntry.delete(ViewState["__sl__"].ToString());
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
        private Boolean update_into_currentstudent()
        {
            try
            {
                using (CurrentStdEntities entities = get_form_data())
                {
                    if (currentStdEntry == null)
                    {
                        currentStdEntry = new CurrentStdEntry();
                    }
                    currentStdEntry.AddEntities = entities;
                    bool result = currentStdEntry.Update();
                    if (result == true)
                    {
                        if (FileUpload1.HasFile == true)
                        {                            
                            if (!ViewState["__ImageName__"].Equals(""))
                                System.IO.File.Delete(Request.PhysicalApplicationPath + "/Images/profileImages/" + ViewState["__ImageName__"].ToString());
                            saveImg(int.Parse( ViewState["__StudentID__"].ToString()), ViewState["__AdmissionFormNo__"].ToString()); // 
                        }                        
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
        private CurrentStdEntities get_form_data()
        {
           
            DateTime AdmissionDate = DateTime.Parse(ViewState["__admissionDate__"].ToString());
            int AdmissionFormNo;
            string StudentID = "0";
                 
           

            int BatchId = commonTask.get_batchid(ddlClass.SelectedValue,ddlYear.SelectedValue);
            entities  = new CurrentStdEntities();
            if (btnSubmit.Text == "Save")
            {
                AdmissionFormNo = StdAdmFormEntry.getAdmissionFormNo(AdmissionDate.Year);
                if (stdAdmFormEntry == null)
                    stdAdmFormEntry = new StdAdmFormEntry();
                int sl = stdAdmFormEntry.Insert(AdmissionFormNo, AdmissionDate.ToString("yyyy-MM-dd"));
                if (sl == 0)
                    return entities = null;
                ViewState["__sl__"] = sl;
                entities.CreateBy = int.Parse(Session["__UserId__"].ToString());
                entities.CreateOn = TimeZoneBD.getCurrentTimeBD();
            }
            else
            {
                AdmissionFormNo = int.Parse(ViewState["__AdmissionFormNo__"].ToString());
                StudentID = ViewState["__StudentID__"].ToString();
                entities.UpdateBy = int.Parse(Session["__UserId__"].ToString());
                entities.UpdateOn = TimeZoneBD.getCurrentTimeBD();
            }

            entities.StudentID = int.Parse(StudentID);
            entities.RollNo =int.Parse(txtRollNo.Text.Trim());
            entities.AdmissionNo = AdmissionFormNo;
            entities.AdmissionDate = AdmissionDate;            
            entities.FullName = commonTask.Replase(txtStudentName.Text.Trim(), '\'', "\''");
            entities.FullNameBn = commonTask.Replase(txtStudentNameBn.Text.Trim(), '\'', "\''");
            entities.ClassID = int.Parse(ddlClass.SelectedValue);
            entities.ClsGrpID = int.Parse(ddlGroup.SelectedValue);
            entities.ClsSecID = int.Parse(ddlSection.SelectedValue);
            entities.Gender = ddlGender.SelectedValue;
            entities.Religion = ddlReligion.SelectedValue;
            entities.ConfigId = int.Parse(ddlShift.SelectedValue);
            entities.DateOfBirth = DateTime.Parse(ViewState["__dateOfBirth__"].ToString());
            entities.Mobile = "+88" + txtStudentMobile.Text.Trim();
            entities.BloodGroup = ddlBloodGroup.SelectedValue;
            entities.Session = "";

            entities.FathersName = commonTask.Replase(txtFatherName.Text.Trim(), '\'', "\''");
            entities.FathersNameBn = commonTask.Replase(txtFatherNameBn.Text.Trim(), '\'', "\''");
            entities.FathersMobile = "+88" + txtFatherMobile.Text.Trim();
            entities.FathersProfession = commonTask.Replase(txtFatherOccupation.Text.Trim(), '\'', "\''");
            entities.FathersProfessionBn = commonTask.Replase(txtFatherOccupationBn.Text.Trim(), '\'', "\''");

            entities.MothersName = commonTask.Replase(txtMotherName.Text.Trim(), '\'', "\''");
            entities.MothersNameBn = commonTask.Replase(txtMotherNameBn.Text.Trim(), '\'', "\''");
            entities.MothersMobile = (!txtMotherMobile.Text.Trim().Equals("") ? "+88" + txtMotherMobile.Text.Trim() : "");
            entities.MothersProfession = commonTask.Replase(txtMotherOccupation.Text.Trim(), '\'', "\''");
            entities.MothersProfessionBn = commonTask.Replase(txtMotherOccupationBn.Text.Trim(), '\'', "\''");

            entities.ParentsAddress = commonTask.Replase(txtParentsVillage.Text.Trim(), '\'', "\''");
            entities.ParentsAddressBn = commonTask.Replase(txtParentsVillageBn.Text.Trim(), '\'', "\''");
            entities.ParentsPostOfficeId = int.Parse(ddlParentsPostOffice.SelectedValue);
            entities.ParentsThanaId = int.Parse(ddlParentsUpazila.SelectedValue);
            entities.ParentsDistrictId = int.Parse(ddlParentsDistrict.SelectedValue);

            entities.GuardianName = commonTask.Replase(txtGuardianName.Text.Trim(), '\'', "\''");
            entities.GuardianRelation = commonTask.Replase(txtGuardianRelation.Text.Trim(), '\'', "\''");
            entities.GuardianMobileNo = "+88" + txtGuardianMobile.Text.Trim();
            entities.GuardianAddress = commonTask.Replase(txtGuardianAddress.Text.Trim(), '\'', "\''");

            entities.PAVillage = commonTask.Replase(txtPermanentVillage.Text.Trim(), '\'', "\''");
            entities.PAVillageBn = commonTask.Replase(txtPermanentVillageBn.Text.Trim(), '\'', "\''");
            entities.PAPostOfficeID = int.Parse(ddlPermanentPostOffice.SelectedValue);
            entities.PThanaId = int.Parse(ddlPermanentUpazila.SelectedValue);
            entities.PDistrictId = int.Parse(ddlPermanentDistrict.SelectedValue);

            entities.TAViIlage = commonTask.Replase(txtPresentVillage.Text.Trim(), '\'', "\''");
            entities.TAViIlageBn = commonTask.Replase(txtPresentVillageBn.Text.Trim(), '\'', "\''");
            entities.TAPostOfficeID = int.Parse(ddlPresentPostOffice.SelectedValue);
            entities.TThanaId = int.Parse(ddlPresentUpazila.SelectedValue);
            entities.TDistrictId = int.Parse(ddlPresentDistrict.SelectedValue);
            entities.MotherTongue = "Bangla";
            entities.Nationality = "Bangladeshi";
            if (ckbPreviousInstituteInfo.Checked)
            {
                entities.PreviousExamType =null;
                entities.PSCBoard = null;
                entities.PSCPassingYear = null;
                entities.PSCJSCRegistration = null;
                entities.PSCRollNo = null;
                entities.PSCGPA =null;
                entities.PreviousSchoolName = "";
            }
            else
            {
                entities.PreviousExamType = "SSC";
                entities.PSCBoard = ddlPreviousExamBoard.SelectedValue;
                entities.PSCPassingYear = int.Parse(ddlPreviousExamPassingYear.SelectedValue);
                entities.PSCJSCRegistration = txtPreviousExamRegistrationNo.Text.Trim();
                entities.PSCRollNo = int.Parse(txtPreviousExamRollNo.Text.Trim());
                entities.PSCGPA = double.Parse(txtPreviousExamGPA.Text.Trim());
                entities.PreviousSchoolName = commonTask.Replase(txtPreviousExamSchoolName.Text.Trim(), '\'', "\''");
            }           

            if (ckbTCInfo.Checked)
            {
                entities.TCCollegeName = null;
                entities.TCDate = null;
            }
            else
            {
                entities.TCCollegeName = commonTask.Replase(txtTCCollegeName.Text.Trim(), '\'', "\''");
                entities.TCDate = DateTime.Parse(ViewState["__tcDate__"].ToString());
            }
            

          
            entities.IsActive = true;
            entities.Status = "Old";
            entities.PaymentStatus = true;
            entities.BatchID = BatchId;
            entities.StartBatchID = BatchId;
            
            return entities;
        }
       
        private void saveImg( int sl,string admissionno)
        {
            try
            {
                //Save images into Images folder
                System.Drawing.Image image = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                // image.Save(Server.MapPath("/Images/studentAdmissionImages/" + sl+".Jpeg"));
                int width = 155;
                int height = 185;
                using (System.Drawing.Image thumbnail = image.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        thumbnail.Save(Server.MapPath("/Images/profileImages/" + admissionno + ".Jpeg"));
                        if (currentStdEntry == null)
                            currentStdEntry = new CurrentStdEntry();
                         currentStdEntry.updateImageName(sl, admissionno + ".Jpeg");
                    }
                }


            }
            catch { }
        }

        private bool ThumbnailCallback()
        {
            return false;
        }
        private void allClear()
        {
            ViewState["__sl__"] = "";
            ViewState["__AdmissionFormNo__"] ="";
        //    ViewState["__StudentID__"] = "";
            ViewState["__ImageName__"] ="";
            txtStudentName.Text = "";
            txtStudentNameBn.Text = "";
            ddlGender.SelectedValue = "0";
            txtDateOfBirth.Text = "";
            ddlReligion.SelectedValue = "0";
            ddlBloodGroup.SelectedValue = "0";
            txtStudentMobile.Text ="";
            if(ddlShift!=null & ddlShift.Items.Count>1)
                ddlShift.SelectedValue = "0";
            if (ddlYear != null & ddlYear.Items.Count > 1)
                ddlYear.SelectedValue = "0";
            if (ddlClass != null & ddlClass.Items.Count > 1)
                ddlClass.SelectedValue = "0";
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(ddlClass.SelectedValue), ddlGroup);
            if (ddlGroup != null & ddlGroup.Items.Count > 1)
                ddlGroup.SelectedValue = "0";
            ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(ddlClass.SelectedValue), ddlGroup.SelectedValue);
            if (ddlSection != null & ddlSection.Items.Count > 1)
                ddlSection.SelectedValue = "0";
            txtRollNo.Text = "0";
            txtAdmissionDate.Text = TimeZoneBD.getCurrentTimeBD().ToString("dd-MM-yyyy");

            txtFatherName.Text = "";
            txtFatherNameBn.Text = "";
            txtFatherMobile.Text = "";
            txtFatherOccupation.Text = "";
            txtFatherOccupationBn.Text = "";

            txtMotherName.Text = "";
            txtMotherNameBn.Text = "";
            txtMotherMobile.Text = "";
            txtMotherOccupation.Text = "";
            txtMotherOccupationBn.Text ="";

            if (ddlParentsDistrict != null & ddlParentsDistrict.Items.Count > 1)
                ddlParentsDistrict.SelectedValue = "0";
            ThanaEntry.GetDropDownList(int.Parse(ddlParentsDistrict.SelectedValue), ddlParentsUpazila);
            if (ddlParentsUpazila != null & ddlParentsUpazila.Items.Count > 1)
                ddlParentsUpazila.SelectedValue ="0";
            Classes.commonTask.loadPostoffice(ddlParentsPostOffice, ddlParentsDistrict.SelectedValue, ddlParentsUpazila.SelectedValue);
            if (ddlParentsPostOffice != null & ddlParentsPostOffice.Items.Count > 1)
                ddlParentsPostOffice.SelectedValue ="0";
            txtParentsVillage.Text ="";
            txtParentsVillageBn.Text ="";

            txtGuardianName.Text ="";
            txtGuardianRelation.Text ="";
            txtGuardianMobile.Text ="";
            txtGuardianAddress.Text ="";

            if (ddlPermanentDistrict != null & ddlPermanentDistrict.Items.Count > 1)
                ddlPermanentDistrict.SelectedValue = "0";
            ThanaEntry.GetDropDownList(int.Parse(ddlPermanentDistrict.SelectedValue), ddlPermanentUpazila);
            if (ddlPermanentUpazila != null & ddlPermanentUpazila.Items.Count > 1)
                ddlPermanentUpazila.SelectedValue = "0";
            Classes.commonTask.loadPostoffice(ddlPermanentPostOffice, ddlPermanentDistrict.SelectedValue, ddlPermanentUpazila.SelectedValue);
            if (ddlPermanentPostOffice != null & ddlPermanentPostOffice.Items.Count > 1)
                ddlPermanentPostOffice.SelectedValue = "0";
            txtPermanentVillage.Text = "";
            txtPermanentVillageBn.Text = "";

            ddlPresentDistrict.SelectedValue ="0";
            ThanaEntry.GetDropDownList(int.Parse(ddlPresentDistrict.SelectedValue), ddlPresentUpazila);
            if (ddlPermanentPostOffice != null & ddlPermanentPostOffice.Items.Count > 1)
                ddlPermanentPostOffice.SelectedValue ="0";
            Classes.commonTask.loadPostoffice(ddlPresentPostOffice, ddlPresentDistrict.SelectedValue, ddlPresentUpazila.SelectedValue);
            if (ddlPresentPostOffice != null & ddlPresentPostOffice.Items.Count > 1)
                ddlPresentPostOffice.SelectedValue = "0";
            txtPresentVillage.Text ="";
            txtPresentVillageBn.Text = "";

            ckbPreviousInstituteInfo.Checked = false;
            txtPreviousExamSchoolName.Text ="";
            ddlPreviousExamBoard.SelectedValue ="0";
            ddlPreviousExamPassingYear.SelectedValue = "0";
            txtPreviousExamRegistrationNo.Text = "";
            txtPreviousExamRollNo.Text ="";
            txtPreviousExamGPA.Text = "";

            chkFather.Checked = false;
            chkMother.Checked = false;
            chkOther.Checked = false;

            ckbTCInfo.Checked = true;
            txtTCCollegeName.Text = "";
            txtTCCollegeName.Enabled = false;
            txtTCDate.Text ="";
            txtTCDate.Enabled = false;
            btnSubmit.Text = "Save";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            allClear();
        }        

        protected void ckbSameAsParentsAddress_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckbSameAsParentsAddress.Checked)
                {
                    txtPermanentVillage.Text = txtParentsVillage.Text.Trim();
                    txtPermanentVillageBn.Text = txtParentsVillageBn.Text.Trim();
                    ddlPermanentDistrict.SelectedValue = ddlParentsDistrict.SelectedValue;
                    ThanaEntry.GetDropDownList(int.Parse(ddlPermanentDistrict.SelectedValue), ddlPermanentUpazila);
                    ddlPermanentUpazila.SelectedValue = ddlParentsUpazila.SelectedValue;
                    commonTask.loadPostoffice(ddlPermanentPostOffice, ddlPermanentDistrict.SelectedValue, ddlPermanentUpazila.SelectedValue);
                    ddlPermanentPostOffice.SelectedValue = ddlParentsPostOffice.SelectedValue;
                }
            }
            catch { }
        }

        protected void ckbTCInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbTCInfo.Checked == true)
            {
                txtTCCollegeName.Enabled = false;
                txtTCDate.Enabled = false;
            }
            else
            {
                txtTCCollegeName.Enabled = true;
                txtTCDate.Enabled = true;
            }
        }
    }
}