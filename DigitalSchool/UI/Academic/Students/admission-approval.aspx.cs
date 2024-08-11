using ComplexScriptingSystem;
using DS.BLL;
using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedClass;
using DS.BLL.SMS;
using DS.Classes;
using DS.DAL;
using DS.PropertyEntities.Model.Admission;
using DS.PropertyEntities.Model.SMS;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academic.Students
{
    public partial class admission_approval : System.Web.UI.Page
    {
        DataTable dt;
        CurrentStdEntry currentStdEntry;
        StdAdmFormEntry stdAdmFormEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AdmissionDetails.aspx", btnSearch)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                //---url bind---
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aAcademicHome.HRef = "~/" + Classes.Routing.AcademicRouteUrl;
                aStudentHome.HRef = "~/" + Classes.Routing.StudentHomeRouteUrl;
                //---url bind end---
                commonTask.loadYearFromBatch(ddlYear);
                ddlYear.SelectedValue = TimeZoneBD.getCurrentTimeBD().Year.ToString();
                ShiftEntry.GetShiftListWithAll(ddlShift);                
                
                ClassEntry.GetEntitiesDataWithAll(ddlClass);
                if (ddlClass != null && ddlClass.SelectedValue != "00")
                {
                    ClassGroupEntry.GetDropDownWithAll(ddlGroup, int.Parse(ddlClass.SelectedValue.ToString()));
                    if(ddlGroup != null && ddlGroup.SelectedValue != "00")
                        ClassSectionEntry.GetSectionListWithAll(ddlSection, int.Parse(ddlClass.SelectedValue), ddlGroup.SelectedValue);
                }
                    
                load_admmission_student();
            }
        }
        private void load_admmission_student()
        {
            try
            {
                string conditions = " and adm.AdmissionYear=" + ddlYear.SelectedValue+"";
                if (ddlClass.SelectedValue != "00")
                {
                    conditions += " and adm.ClassId=" + ddlClass.SelectedValue;

                    if (ddlGroup.SelectedValue != "0" && ddlGroup.SelectedValue != "00")
                        conditions += " and adm.ClsGrpID=" + ddlGroup.SelectedValue;
                    if (ddlSection.SelectedValue != "00")
                        conditions += " and adm.ClsSecID=" + ddlSection.SelectedValue;

                }
                    

                dt = new DataTable();
                dt = StdAdmFormEntry.getAdmissionList(conditions);
                gvStudentList.DataSource = dt;
                gvStudentList.DataBind();
            }
            catch (Exception ex) { }
        }

        protected void gvStudentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                int rIndex = int.Parse(e.CommandArgument.ToString());
                string sl= gvStudentList.DataKeys[rIndex].Values[0].ToString();
                string manSubIds= gvStudentList.DataKeys[rIndex].Values[1].ToString();
                string opSubId= gvStudentList.DataKeys[rIndex].Values[2].ToString();
                TextBox txtRollNo = (TextBox)gvStudentList.Rows[rIndex].FindControl("txtRollNo");
                Label lblBatchName = (Label)gvStudentList.Rows[rIndex].FindControl("lblBatchName");
                int roll;
                try
                {
                    roll = int.Parse(txtRollNo.Text.Trim());
                }
                catch(Exception ex)
                {
                    lblMessage.InnerText = "error->Please enter valid Roll No.";                   
                    return;
                }
                TextBox txtNote = (TextBox)gvStudentList.Rows[rIndex].FindControl("txtNote");
                if (save_into_currentstudent(sl, roll, manSubIds, opSubId))
                {
                    if (stdAdmFormEntry == null)
                        stdAdmFormEntry = new StdAdmFormEntry();
                    if (stdAdmFormEntry.updateApprovalInfo(sl, "0", "1", Session["__UserId__"].ToString(), TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd HH:mm:ss"), txtNote.Text.Trim()))
                    {
                        gvStudentList.Rows[rIndex].Visible = false;
                        lblMessage.InnerText = "success->Successfully Approved.";
                        sendSMS(lblBatchName.Text.Trim());
                    }                        
                }
                else
                    lblMessage.InnerText = "error->Unalbe to Approve.";

            }
            else if (e.CommandName == "Reject")
            {
                int rIndex = int.Parse(e.CommandArgument.ToString());
                string sl = gvStudentList.DataKeys[rIndex].Values[0].ToString();                
                TextBox txtNote = (TextBox)gvStudentList.Rows[rIndex].FindControl("txtNote");                
                if (stdAdmFormEntry == null)
                    stdAdmFormEntry = new StdAdmFormEntry();
                if (stdAdmFormEntry.updateApprovalInfo(sl, "0", "0", Session["__UserId__"].ToString(), TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd HH:mm:ss"), txtNote.Text.Trim()))
                {
                    gvStudentList.Rows[rIndex].Visible = false;
                    lblMessage.InnerText = "success->Successfully Rejected.";
                }                    
                else
                    lblMessage.InnerText = "error->Unalbe to Reject.";
            }
        }
        private void sendSMS(string BatchName)
        {
            string MobileNo = ViewState["__Mobile__"].ToString();           
            MobileNo = MobileNo.Replace("+88", "");
            string Msg = string.Format("Congratulations on your admission to Govt. Islampur College! You've been accepted for the "+ BatchName + " and your class roll is "+ ViewState["__Roll__"] .ToString()+ " and admission no is "+ ViewState["__AdmissionNo__"].ToString());
            //string Msg = string.Format("Islampur College received the payment for '" + CategoryName + "'. Your Invoice No : '" + OrderNo + "'.Thank you.");
            if (MobileNo.Length == 11 && "017,019,018,016,015,013,014".Contains(MobileNo.Substring(0, 3)))
            {
                
                string resopse = API.SMSSend(Msg, MobileNo);                
                string[] r = resopse.Split('|');
                SMSEntites smsEntities = new SMSEntites();
                smsEntities.ID = 1;
                smsEntities.MobileNo = dt.Rows[0]["Mobile"].ToString();
                smsEntities.Status = API.MsgStatus(int.Parse(r[0]));
                smsEntities.MessageBody = Msg;
                smsEntities.Purpose = "AdmissionApproval";
                smsEntities.SentTime = DateTime.Now;
                List<SMSEntites> smsList = new List<SMSEntites>();
                smsList.Add(smsEntities);
                SMSReportEntry smsReport = new SMSReportEntry();
                smsReport.BulkInsert(smsList);
            }
        }
        private bool save_into_currentstudent(string sl,int roll,string manSubIds,string opSubId)
        {
            try
            {
                using (CurrentStdEntities entities = GetFormData(sl, roll))
                {
                    if (currentStdEntry == null)
                    {
                        currentStdEntry = new CurrentStdEntry();
                    }
                    currentStdEntry.AddEntities = entities;
                    int studentId = currentStdEntry.Insert();
                    if (studentId > 0)
                    {
                        saveImg(studentId, ViewState["__ImageName__"].ToString(), entities.AdmissionNo+".Jpeg");
                        if(manSubIds!="" && opSubId!="")
                        SaveGroupSubjects(studentId, entities.BatchID,manSubIds,opSubId);

                    }
                    else
                    {
                       
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

        private CurrentStdEntities GetFormData(string sl, int roll)
        {
            dt = new DataTable();
            dt = StdAdmFormEntry.getAdmissionFormInfoForApproval(sl);
            CurrentStdEntities currentStdEntities = new CurrentStdEntities();
            ViewState["__Roll__"]= currentStdEntities.RollNo = roll;
            ViewState["__AdmissionNo__"]=  currentStdEntities.AdmissionNo = int.Parse(dt.Rows[0]["AdmissionFormNo"].ToString());          
            currentStdEntities.AdmissionDate =DateTime.Parse(dt.Rows[0]["AdmissionDate"].ToString());
            ViewState["__ImageName__"]= dt.Rows[0]["ImageName"].ToString();
            currentStdEntities.FullName = commonTask.Replase(dt.Rows[0]["FullName"].ToString(), '\'', "\''");
            currentStdEntities.FullNameBn = commonTask.Replase(dt.Rows[0]["FullNameBn"].ToString(), '\'', "\''") ;
            currentStdEntities.ClassID = int.Parse(dt.Rows[0]["ClassID"].ToString());            
            currentStdEntities.ClsGrpID = int.Parse(dt.Rows[0]["ClsGrpID"].ToString());
            currentStdEntities.ClsSecID = int.Parse(dt.Rows[0]["ClsSecID"].ToString());
            currentStdEntities.Gender = dt.Rows[0]["Gender"].ToString();      
            currentStdEntities.Religion = dt.Rows[0]["Religion"].ToString();
            currentStdEntities.ConfigId = int.Parse(dt.Rows[0]["ShiftId"].ToString());   
            currentStdEntities.DateOfBirth = DateTime.Parse(dt.Rows[0]["DateOfBirth"].ToString());   
           ViewState["__Mobile__"]=  currentStdEntities.Mobile = dt.Rows[0]["Mobile"].ToString();           
            currentStdEntities.BloodGroup = dt.Rows[0]["BloodGroup"].ToString();
            currentStdEntities.Session = dt.Rows[0]["Session"].ToString();

            currentStdEntities.FathersName = commonTask.Replase(dt.Rows[0]["FathersName"].ToString(), '\'', "\''") ;
            currentStdEntities.FathersNameBn = commonTask.Replase(dt.Rows[0]["FathersNameBn"].ToString(), '\'', "\''") ;
            currentStdEntities.FathersProfession = commonTask.Replase(dt.Rows[0]["FathersProfession"].ToString(), '\'', "\''") ;
            currentStdEntities.FathersProfessionBn = commonTask.Replase(dt.Rows[0]["FathersProfessionBn"].ToString(), '\'', "\''") ;            
            currentStdEntities.FathersMobile = dt.Rows[0]["FathersMobile"].ToString();            
            currentStdEntities.MothersName = commonTask.Replase(dt.Rows[0]["MothersName"].ToString(), '\'', "\''");
            currentStdEntities.MothersNameBn = commonTask.Replase(dt.Rows[0]["MothersNameBn"].ToString(), '\'', "\''");
            currentStdEntities.MothersProfession = commonTask.Replase(dt.Rows[0]["MothersProfession"].ToString(), '\'', "\''") ;
            currentStdEntities.MothersProfessionBn = commonTask.Replase(dt.Rows[0]["MothersProfessionBn"].ToString(), '\'', "\''") ;           
            currentStdEntities.MothersMobile = dt.Rows[0]["MothersMobile"].ToString();            
            currentStdEntities.ParentsAddress = commonTask.Replase(dt.Rows[0]["ParentsAddress"].ToString(), '\'', "\''");
            currentStdEntities.ParentsAddressBn = commonTask.Replase(dt.Rows[0]["ParentsAddressBn"].ToString(), '\'', "\''");
            currentStdEntities.ParentsPostOfficeId = int.Parse(dt.Rows[0]["ParentsPostOfficeId"].ToString());
            currentStdEntities.ParentsThanaId = int.Parse(dt.Rows[0]["ParentsThanaId"].ToString());
            currentStdEntities.ParentsDistrictId = int.Parse(dt.Rows[0]["ParentsDistrictId"].ToString());
            currentStdEntities.GuardianName = commonTask.Replase(dt.Rows[0]["GuardianName"].ToString(), '\'', "\''") ;
            currentStdEntities.GuardianRelation = commonTask.Replase(dt.Rows[0]["GuardianRelation"].ToString(), '\'', "\''");
            currentStdEntities.GuardianMobileNo = dt.Rows[0]["GuardianMobileNo"].ToString();
            currentStdEntities.GuardianAddress = commonTask.Replase(dt.Rows[0]["GuardianAddress"].ToString(), '\'', "\''");

            currentStdEntities.PAVillage = commonTask.Replase(dt.Rows[0]["PermanentAddress"].ToString(), '\'', "\''") ;
            currentStdEntities.PAVillageBn = commonTask.Replase(dt.Rows[0]["PermanentAddressBn"].ToString(), '\'', "\''") ;
            currentStdEntities.PAPostOfficeID = int.Parse(dt.Rows[0]["PermanentPostOfficeId"].ToString());
            currentStdEntities.PThanaId = int.Parse(dt.Rows[0]["PermanentThanaId"].ToString());
            currentStdEntities.PDistrictId = int.Parse(dt.Rows[0]["PermanentDistrictId"].ToString());
            
            currentStdEntities.TAViIlage = commonTask.Replase(dt.Rows[0]["PresentAddress"].ToString(), '\'', "\''");
            currentStdEntities.TAViIlageBn = commonTask.Replase(dt.Rows[0]["PresentAddressBn"].ToString(), '\'', "\''") ;
            currentStdEntities.TAPostOfficeID = int.Parse(dt.Rows[0]["PresentPostOfficeId"].ToString());
            currentStdEntities.TThanaId = int.Parse(dt.Rows[0]["PresentThanaId"].ToString());
            currentStdEntities.TDistrictId = int.Parse(dt.Rows[0]["PresentDistrictId"].ToString());            
            currentStdEntities.MotherTongue = "Bangla";
            currentStdEntities.Nationality = "Bangladeshi";
            currentStdEntities.PreviousExamType ="SSC";
            currentStdEntities.PSCBoard = dt.Rows[0]["PreSCBoard"].ToString();
            currentStdEntities.PSCPassingYear = int.Parse(dt.Rows[0]["PreSCPassingYear"].ToString());
            currentStdEntities.PSCJSCRegistration = dt.Rows[0]["PreSCRegistration"].ToString();
            currentStdEntities.PSCRollNo =long.Parse(dt.Rows[0]["PreSCRollNo"].ToString());
            currentStdEntities.PSCGPA = double.Parse(dt.Rows[0]["PreSCGPA"].ToString());
            
            
            currentStdEntities.PreviousSchoolName = commonTask.Replase(dt.Rows[0]["PreSchoolName"].ToString(), '\'', "\''") ;                     
            currentStdEntities.TCCollegeName = commonTask.Replase(dt.Rows[0]["TCCollege"].ToString(), '\'', "\''");
           
            if (dt.Rows[0]["TCDate"].ToString() == "") currentStdEntities.TCDate = null;
            else currentStdEntities.TCDate = DateTime.Parse(dt.Rows[0]["TCDate"].ToString());
            currentStdEntities.ImageName = dt.Rows[0]["AdmissionFormNo"].ToString()+".Jpeg";
            currentStdEntities.IsActive = true;         
            currentStdEntities.Status = "New";
            currentStdEntities.PaymentStatus = true;
            currentStdEntities.BatchID = int.Parse(dt.Rows[0]["BatchID"].ToString());
            currentStdEntities.StartBatchID = int.Parse(dt.Rows[0]["BatchID"].ToString());             
            currentStdEntities.CreateBy = int.Parse(Session["__UserId__"].ToString());
            currentStdEntities.CreateOn = TimeZoneBD.getCurrentTimeBD();
            return currentStdEntities;
        }
        private void saveImg(int StudentId,string admission_image,string profile_image)
        {
            try
            {
               
                string sourceFile = Server.MapPath("/Images/studentAdmissionImages/" + admission_image);
                string destFile = Server.MapPath("/Images/profileImages/" + profile_image);
                System.IO.File.Copy(sourceFile, destFile, true);
                if (currentStdEntry == null)
                    currentStdEntry = new CurrentStdEntry();
                currentStdEntry.updateImageName(StudentId, profile_image);

            }
            catch { }
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassGroupEntry.GetDropDownWithAll(ddlGroup, int.Parse(ddlClass.SelectedValue.ToString()));
            if (ddlGroup != null && ddlGroup.SelectedValue != "00")
                ClassSectionEntry.GetSectionListWithAll(ddlSection, int.Parse(ddlClass.SelectedValue), ddlGroup.SelectedValue);
            else
            {
                if (ddlSection != null)
                    ddlSection.Items.Clear();
                ddlSection.Items.Insert(0, new ListItem("All", "00"));
            }
        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassSectionEntry.GetSectionListWithAll(ddlSection, int.Parse(ddlClass.SelectedValue), ddlGroup.SelectedValue);
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            load_admmission_student();
        }

        protected void gvStudentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string manSubIds = gvStudentList.DataKeys[e.Row.RowIndex]["ManSubId"].ToString();

                if (manSubIds != "")
                {
                    Label lblMansubid = (Label)e.Row.FindControl("lblMansubid");
                  
                    lblMansubid.Text = GroupSubject.getSubsName(manSubIds); 
                }



            }
        }

    

        //Save Data GroupSubjectSetup table and GroupSubjectSetupDetails Table
        private void SaveGroupSubjects(int StudentId,int BatchId, string manSubIds,string OpSubId) 
        {
           
            int SgSubId = CRUD.GetMaxID("INSERT INTO [dbo].[StudentGroupSubSetup] ([StudentId], [BatchId])VALUES ("+ StudentId + ","+ BatchId + ");SELECT SCOPE_IDENTITY()");

            if (SgSubId > 0)
            {
                List<string> _manSubIds = manSubIds.Split(',').ToList();
                foreach (string subId in _manSubIds)
                {
                    CRUD.ExecuteQuery(@"INSERT INTO [dbo].[StudentGroupSubSetupDetails]
           ([SGSubId]
           ,[SubId]
           ,[MSStatus])
     VALUES
           ("+ SgSubId + @","+ subId + ",1)");
                }
                CRUD.ExecuteQuery(@"INSERT INTO [dbo].[StudentGroupSubSetupDetails]
           ([SGSubId]
           ,[SubId]
           ,[MSStatus])
     VALUES
           (" + SgSubId + @"," + OpSubId + ",0)");
            }


        }
    }
}