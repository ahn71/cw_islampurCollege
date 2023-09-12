using ComplexScriptingSystem;
using DS.BLL;
using DS.BLL.Admission;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedClass;
using DS.Classes;
using DS.PropertyEntities.Model.Admission;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.DSWS
{
    public partial class AdmissionForm : System.Web.UI.Page
    {
        ClassGroupEntry clsgrpEntry;
        StdAdmFormEntry stdAdmFormEntry;
        DataTable dt;
        AdmStdFormInfoEntities entities;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                
                if (!StdAdmFormEntry.isAdmissionOpen())
                   Response.Redirect("UI/DSWS/adm-msg.aspx");
                
                ClassEntry.GetAdmissionClasses(ddlClass);
                DropDownList[] ddlDistrict = { ddlPermanentDistrict, ddlPresentDistrict,ddlParentsDistrict };
                DistrictEntry.GetDropDownList(ddlDistrict);                
                ShiftEntry.GetShiftList(ddlShift);
                while (true)
                {
                    DropDownList[] ddlPassingYear = { ddlPreviousExamPassingYear, ddlPreviousExamPassingYearHSC };
                    commonTask.loadPassingYearForAdmission(ddlPassingYear);
                    if (ddlPreviousExamPassingYear == null || ddlPreviousExamPassingYear.Items.Count == 0)
                    {
                        commonTask.loadPassingYearForAdmission(ddlPassingYear);
                    }
                    else
                        break;

                }
                //commonTask.loadPassingYearForAdmission(ddlPreviousExamPassingYear);
                // commonTask.loadPassingYearForAdmission(ddlPreviousExamPassingYearHSC);
               
                while (true) {
                    DropDownList[] ddlBoards = { ddlPreviousExamBoard, ddlPreviousExamBoardHSC, ddlPreviousExamBoardHonours };
                    commonTask.loadBoard(ddlBoards);
                    if (ddlPreviousExamBoard == null || ddlPreviousExamBoard.Items.Count == 0)
                    {
                        commonTask.loadBoard(ddlBoards);
                    }
                    else
                        break;

                    //DropDownList[] ddlPassingYear = { ddlPreviousExamPassingYear, ddlPreviousExamPassingYearHSC };
                    //commonTask.loadPassingYearForAdmission(ddlPassingYear);
                    

                    
                }


                
                
            }
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (ddlClass.SelectedItem.Text.Trim().ToLower().Contains("honours"))
            {
               // divNUAdmissionRoll.Visible = true;
                divHSCInfo.Visible = true;
                divHonoursInfo.Visible = false;
            }
            else if (ddlClass.SelectedItem.Text.Trim().ToLower().Contains("degree"))
            {
              //  divNUAdmissionRoll.Visible = false;
                divHSCInfo.Visible = true;
                divHonoursInfo.Visible = false;
            }
            else if (ddlClass.SelectedItem.Text.Trim().ToLower().Contains("masters"))
            {
              //  divNUAdmissionRoll.Visible = true;
                divHSCInfo.Visible = true;                
                divHonoursInfo.Visible = true;
            }
            else
            {
             //   divNUAdmissionRoll.Visible = false;
                divHSCInfo.Visible = false;
                divHonoursInfo.Visible = false;
            }

            string[] ids = ddlClass.SelectedValue.Split('_');
            ViewState["__ClassId__"] = ids[0];
            ViewState["__AdmissionYear__"] = ids[1];

            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(ViewState["__ClassId__"].ToString()), ddlGroup);
            if(ddlGroup.SelectedValue!="0")
            ClassSectionEntry.GetSectionList(ddlSection, int.Parse(ViewState["__ClassId__"].ToString()), ddlGroup.SelectedValue);
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassSectionEntry.GetSectionList(ddlSection, int.Parse(ViewState["__ClassId__"].ToString()), ddlGroup.SelectedValue);
        }

        protected void ddlPermanentDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThanaEntry.GetDropDownList(int.Parse(ddlPermanentDistrict.SelectedValue), ddlPermanentUpazila);
        }

        protected void ddlPermanentUpazila_SelectedIndexChanged(object sender, EventArgs e)
        {
            Classes.commonTask.loadPostoffice(ddlPermanentPostOffice ,ddlPermanentDistrict.SelectedValue, ddlPermanentUpazila.SelectedValue);
        }

        protected void ddlPresentDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThanaEntry.GetDropDownList(int.Parse(ddlPresentDistrict.SelectedValue), ddlPresentUpazila);
        }

        protected void ddlPresentUpazila_SelectedIndexChanged(object sender, EventArgs e)
        {
            Classes.commonTask.loadPostoffice(ddlPresentPostOffice , ddlPresentDistrict.SelectedValue, ddlPresentUpazila.SelectedValue);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            try
            {
                DateTime dateOfBirth = DateTime.Parse(commonTask.ddMMyyyyToyyyyMMdd(txtDateOfBirth.Text.Trim()));
                ViewState["__dateOfBirth__"]= dateOfBirth.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {

                lblMessage.InnerText = "error-> Please,enter valid Date of Birth !";
                txtDateOfBirth.Focus();
                return;
            }
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
            saveStudentAdmission();
        }
        private Boolean saveStudentAdmission()
        {
            try
            {
                
                using (AdmStdFormInfoEntities entities = GetFormData())
                {
                    if (entities == null)
                        return false;
                    if (stdAdmFormEntry == null)
                    {
                        stdAdmFormEntry = new StdAdmFormEntry();
                    }
                    stdAdmFormEntry.AddEntities = entities;
                    int sl = stdAdmFormEntry.Insert(divHSCInfo.Visible,divHonoursInfo.Visible);
                    if (sl > 0)
                    {
                        if (FileUpload1.HasFile)
                        {
                            saveImg(sl.ToString());
                        }
                        preview(sl.ToString());
                        return true;


                    }
                    else
                    {
                        return false;
                    }
                }

                
            }
            catch (Exception ex)
            {            
                return false;
            }
        }
        
        private AdmStdFormInfoEntities GetFormData()
        {
            try {
                


                entities = new AdmStdFormInfoEntities();
                entities.AdmissionDate = TimeZoneBD.getCurrentTimeBD();

                int AdmissionFormNo = StdAdmFormEntry.getAdmissionFormNo(entities.AdmissionDate.Year);
                if(AdmissionFormNo == 0)
                    return entities = null;                
                entities.MoneyReceiptNo = txtMoneyReceiptNo.Text.Trim();
                entities.AdmissionFormNo = AdmissionFormNo;
                entities.FullName = commonTask.Replase(txtStudentName.Text.Trim(), '\'', "\''"); 
                entities.FullNameBn = commonTask.Replase(txtStudentNameBn.Text.Trim(), '\'', "\''"); 
                entities.Gender = ddlGender.SelectedValue;
                entities.DateOfBirth = DateTime.Parse(ViewState["__dateOfBirth__"].ToString());
                entities.Religion = ddlReligion.SelectedValue;
                entities.ShiftId = int.Parse(ddlShift.SelectedValue);
                entities.ClassID = int.Parse(ViewState["__ClassId__"].ToString());
                entities.ClsGrpID = int.Parse(ddlGroup.SelectedValue);
                entities.ClsSecID = int.Parse(ddlSection.SelectedValue);
                entities.Mobile = "+88" + txtStudentMobile.Text.Trim();
                entities.BloodGroup = ddlBloodGroup.SelectedValue;

                entities.FathersName = commonTask.Replase(txtFatherName.Text.Trim(), '\'', "\''"); 
                entities.FathersNameBn = commonTask.Replase(txtFatherNameBn.Text.Trim(), '\'', "\''"); 
                entities.FathersMobile=   "+88" +txtFatherMobile.Text.Trim();
                entities.FathersProfession = commonTask.Replase(txtFatherOccupation.Text.Trim(), '\'', "\''"); 
                entities.FathersProfessionBn = commonTask.Replase(txtFatherOccupationBn.Text.Trim(), '\'', "\''"); 

                entities.MothersName = commonTask.Replase(txtMotherName.Text.Trim(), '\'', "\''"); 
                entities.MothersNameBn = commonTask.Replase(txtMotherNameBn.Text.Trim(), '\'', "\''"); 
                entities.MothersMobile = (!txtMotherMobile.Text.Trim().Equals("") ? "+88" + txtMotherMobile.Text.Trim() : "");
                entities.MothersProfession = commonTask.Replase(txtMotherOccupation.Text.Trim(), '\'', "\''"); 
                entities.MothersProfessionBn = commonTask.Replase(txtMotherOccupationBn.Text.Trim(), '\'', "\''"); 

                entities.ParentsAddress = commonTask.Replase(txtParentsVillage.Text.Trim(), '\'', "\''"); 
                entities.ParentsAddressBn = commonTask.Replase(txtParentsVillageBn.Text.Trim(), '\'', "\''"); 
                entities.ParentsPostOfficeId =int.Parse(ddlParentsPostOffice.SelectedValue);
                entities.ParentsThanaId =int.Parse(ddlParentsUpazila.SelectedValue);
                entities.ParentsDistrictId =int.Parse(ddlParentsDistrict.SelectedValue);
                
                entities.GuardianName = commonTask.Replase(txtGuardianName.Text.Trim(), '\'', "\''"); 
                entities.GuardianRelation = commonTask.Replase(txtGuardianRelation.Text.Trim(), '\'', "\''"); 
                entities.GuardianMobileNo = "+88" + txtGuardianMobile.Text.Trim();
                entities.GuardianAddress = commonTask.Replase(txtGuardianAddress.Text.Trim(), '\'', "\''"); 

                entities.PermanentAddress = commonTask.Replase(txtPermanentVillage.Text.Trim(), '\'', "\''"); 
                entities.PermanentAddressBn = commonTask.Replase(txtPermanentVillageBn.Text.Trim(), '\'', "\''"); 
                entities.PermanentDistrictId =int.Parse(ddlPermanentDistrict.SelectedValue);
                entities.PermanentThanaId =int.Parse(ddlPermanentUpazila.SelectedValue);
                entities.PermanentPostOfficeId =int.Parse(ddlPermanentPostOffice.SelectedValue);

                entities.PresentAddress = commonTask.Replase(txtPresentVillage.Text.Trim(), '\'', "\''"); 
                entities.PresentAddressBn = commonTask.Replase(txtPresentVillageBn.Text.Trim(), '\'', "\''"); 
                entities.PresentDistrictId = int.Parse(ddlPresentDistrict.SelectedValue);
                entities.PresentThanaId = int.Parse(ddlPresentUpazila.SelectedValue);
                entities.PresentPostOfficeId = int.Parse(ddlPresentPostOffice.SelectedValue);

                
                entities.PreSchoolName = commonTask.Replase(txtPreviousExamSchoolName.Text.Trim(), '\'', "\''");
                entities.PreSCBoard = ddlPreviousExamBoard.SelectedValue;
                entities.PreSCPassingYear = int.Parse(ddlPreviousExamPassingYear.SelectedValue);
                entities.PreSCRegistration =long.Parse(txtPreviousExamRegistrationNo.Text.Trim());
                entities.PreSCRollNo = long.Parse(txtPreviousExamRollNo.Text.Trim());
                entities.PreSCGPA = float.Parse(txtPreviousExamGPA.Text.Trim());

                if (divHSCInfo.Visible)
                {
                    entities.PreSchoolNameHSC = commonTask.Replase(txtPreviousExamSchoolNameHSC.Text.Trim(), '\'', "\''");
                    entities.PreSCBoardHSC = ddlPreviousExamBoardHSC.SelectedValue;
                    entities.PreSCPassingYearHSC = int.Parse(ddlPreviousExamPassingYearHSC.SelectedValue);
                    entities.PreSCRegistrationHSC = long.Parse(txtPreviousExamRegistrationNoHSC.Text.Trim());
                    entities.PreSCRollNoHSC = long.Parse(txtPreviousExamRollNoHSC.Text.Trim());
                    entities.PreSCGPAHSC = float.Parse(txtPreviousExamGPAHSC.Text.Trim());
                }
                if (divHonoursInfo.Visible)
                {
                    entities.PreSchoolNameHonours = commonTask.Replase(txtPreviousExamSchoolNameHonours.Text.Trim(), '\'', "\''");
                    entities.PreSCBoardHonours = ddlPreviousExamBoardHonours.SelectedValue;
                    entities.PreSCPassingYearHonours = int.Parse(ddlPreviousExamPassingYearHonours.SelectedValue);
                    entities.PreSCRegistrationHonours = long.Parse(txtPreviousExamRegistrationNoHonours.Text.Trim());
                    entities.PreSCRollNoHonours = long.Parse(txtPreviousExamRollNoHonours.Text.Trim());
                    entities.PreSCGPAHonours = float.Parse(txtPreviousExamGPAHonours.Text.Trim());
                }

                //else
                //{
                //    entities.PreSchoolNameHSC = DBNull.Value;
                //    entities.PreSCBoardHSC = ddlPreviousExamBoardHSC.SelectedValue;
                //    entities.PreSCPassingYearHSC = int.Parse(ddlPreviousExamPassingYearHSC.SelectedValue);
                //    entities.PreSCRegistrationHSC = long.Parse(txtPreviousExamRegistrationNoHSC.Text.Trim());
                //    entities.PreSCRollNoHSC = long.Parse(txtPreviousExamRollNoHSC.Text.Trim());
                //    entities.PreSCGPAHSC = float.Parse(txtPreviousExamGPAHSC.Text.Trim());
                //}
                entities.TCCollege = commonTask.Replase(txtTCCollegeName.Text.Trim(), '\'', "\''"); 
                entities.TCDate = ViewState["__tcDate__"].ToString();

                entities.IsActive = true;
                //entities.AdmissionYear = 2022;
                entities.AdmissionYear =int.Parse( ViewState["__AdmissionYear__"].ToString());
                if (divNUAdmissionRoll.Visible)
                    entities.NUAdmissionRoll = txtNUAdmissionRoll.Text.Trim();
                else
                    entities.NUAdmissionRoll = "";
                return entities;
            }
            catch (Exception ex) { lblMessage.InnerText = "error->ln:236, GetFormData() | "+ex.Message; return entities = null; }
            

        }

        protected void ddlParentsDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThanaEntry.GetDropDownList(int.Parse(ddlParentsDistrict.SelectedValue),ddlParentsUpazila);
        }

        protected void ddlParentsUpazila_SelectedIndexChanged(object sender, EventArgs e)
        {
            Classes.commonTask.loadPostoffice(ddlParentsPostOffice, ddlParentsDistrict.SelectedValue,ddlParentsUpazila.SelectedValue);
        }
        private void saveImg(string sl)
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
                        thumbnail.Save(Server.MapPath("/Images/studentAdmissionImages/" + sl+".Jpeg"));
                        if (stdAdmFormEntry == null)
                            stdAdmFormEntry = new StdAdmFormEntry();
                        stdAdmFormEntry.updateImageName(sl,sl+ ".Jpeg");
                    }
                }


            }
            catch { }
        }

        private bool ThumbnailCallback()
        {
            return false;
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
            try {
                 txtGuardianAddress.Text = txtParentsVillage.Text.Trim()  +((!ddlParentsPostOffice.SelectedValue.Equals("0"))? ","+ddlParentsPostOffice.SelectedItem.Text.Trim() :"") + ((!ddlParentsUpazila.SelectedValue.Equals("0")) ? ","+ddlParentsUpazila.SelectedItem.Text.Trim()  : "") + ((!ddlParentsDistrict.SelectedValue.Equals("0")) ? "," + ddlParentsDistrict.SelectedItem.Text.Trim(): "");
            } catch(Exception ex) { }
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
                    commonTask.loadPostoffice(ddlPresentPostOffice, ddlPresentDistrict.SelectedValue,ddlPresentUpazila.SelectedValue);
                    ddlPresentPostOffice.SelectedValue = ddlPermanentPostOffice.SelectedValue;
                }
                else
                {
                    txtPresentVillage.Text = txtPermanentVillage.Text.Trim();
                    txtPresentVillageBn.Text = txtPermanentVillageBn.Text.Trim();
                    ddlPresentDistrict.SelectedValue = ddlPermanentDistrict.SelectedValue;                    
                    ddlPresentUpazila.SelectedValue = ddlPermanentUpazila.SelectedValue;                    
                    ddlPresentPostOffice.SelectedValue = ddlPermanentPostOffice.SelectedValue;
                }
            }
            catch { }
        }
        private void preview(string sl)
        {
            Response.Redirect("/UI/DSWS/admission-form.aspx?SL=" + sl);
        }
      
    }
}