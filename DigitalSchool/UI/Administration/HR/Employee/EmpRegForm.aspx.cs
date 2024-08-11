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
using DS.BLL.GeneralSettings;
using DS.BLL.ControlPanel;
using DS.DAL;
using DS.BLL.ManagedClass;
using System.Web.UI.HtmlControls;
using DS.Classes;

namespace DS.UI.Administration.HR.Employee
{
    public partial class EmpRegForm : System.Web.UI.Page
    {
        static string imageName = "";
        static string imageName2 = "";
        static string teacherId = "";
        ClassGroupEntry clsgrpEntry;
        DataTable dt = new DataTable();
        DataTable dtt = new DataTable();
        DataTable dtOthers = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            
                lblMessage.InnerText = "";
                if (!IsPostBack)
                {
                    btnSave.Text = "Save";
                    btnAdd.Text = "Add";
                    btnAddEducation.Text = "Add";
                    string EditMode = "";
                    try { EditMode = (Request.QueryString["Edit"].ToString() != null) ? "Yes" : "No"; }                    catch { }
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "EmpRegForm.aspx", btnSave, EditMode)) Response.Redirect(Request.UrlReferrer.ToString() + "?&hasperm=no");
                    Classes.commonTask.loadEmployeeType(rblEmpType);
                    getDesignationsList();
                    getDepartmentsList();                    
                    ShiftEntry.GetDropDownList(ddlShift);
                    StafforFacultyAccess();
                    ClassEntry.GetEntitiesData(dlClassTeacher);

                    dt.Columns.Add("SL", typeof(int));
                    dt.Columns.Add("ExIInstName", typeof(string));
                    dt.Columns.Add("ExIDesignation", typeof(string));
                    dt.Columns.Add("ExIDDateFrom", typeof(string));
                    dt.Columns.Add("ExIDateTO", typeof(string));
                    dt.Columns.Add("ExIDuration", typeof(string));
                    dt.Columns["SL"].AutoIncrement = true;
                    dt.Columns["SL"].AutoIncrementSeed = 1;
                    dt.Columns["SL"].AutoIncrementStep = 1;
                    ViewState["vs"] = dt;
                    experienceList.DataSource = ViewState["vs"] as DataTable;
                    experienceList.DataBind();

                    dtt.Columns.Add("SL", typeof(int));
                    dtt.Columns.Add("EIExamName", typeof(string));
                    dtt.Columns.Add("EIDepertment", typeof(string));
                    dtt.Columns.Add("EIBoard", typeof(string));
                    dtt.Columns.Add("EIPassingYear", typeof(string));
                    dtt.Columns.Add("EIResult", typeof(string));
                    dtt.Columns["SL"].AutoIncrement = true;
                    dtt.Columns["SL"].AutoIncrementSeed = 1;
                    dtt.Columns["SL"].AutoIncrementStep = 1;
                    ViewState["vss"] = dtt;
                    educationlist.DataSource = ViewState["vss"] as DataTable;
                    educationlist.DataBind();

                    dtOthers.Columns.Add("SL", typeof(int));
                    dtOthers.Columns.Add("OthersInfo", typeof(string));

                    dtOthers.Columns["SL"].AutoIncrement = true;
                    dtOthers.Columns["SL"].AutoIncrementSeed = 1;
                    dtOthers.Columns["SL"].AutoIncrementStep = 1;
                    ViewState["vsOthers"] = dtOthers;
                    gvOthersInfo.DataSource = ViewState["vsOthers"] as DataTable;
                    gvOthersInfo.DataBind();

                    districtBind();
                    loadEmployeeInfoInfo();
                    if (Session["__afterSave__"]!=null)
                    {
                        if (Session["__afterSave__"].ToString() == "true")
                        {
                        ViewState["__afterSave__"] = "false";
                        lblMessage.InnerText = "success->Save Successfully";
                        }                  
                    }
                  
                }
               
        }

        private void districtBind()
        {
            SqlCommand c = new SqlCommand("Select * from Distritcts", DbConnection.Connection);
            SqlDataAdapter ad = new SqlDataAdapter(c);
            DataTable d = new DataTable();
            ad.Fill(d);
            ddlpAddress.DataSource = d;
            ddlpAddress.DataBind();
            ddlpAddress.Items.Insert(0, new ListItem("...Select...", "0"));
            ddlDistrict.DataSource = d;
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("...Select...", "0"));
            
        }

        private void loadEmployeeInfoInfo()
        {
            try
            {

                Session["eid"] = teacherId = Request.QueryString["TeacherId"];
                if (teacherId == "") return;
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@EID", teacherId) };
                sqlDB.fillDataTable("Select EID, ECardNo,  convert(varchar(10),EJoiningDate,105) as EJoiningDate, EName, TCodeNo, EGender, EFathersName, EMothersName, DId,"
                + "DesId, EReligion, EMaritalStatus, EPhone,REPLACE(EMobile,'+88', '') EMobile, EEmail,  convert(varchar(10),EBirthday,105) as EBirthday, EPresentAddress, EParmanentAddress, "
                + "EBloodGroup, ELastDegree,EOITraining, EExaminer,ECName,ECRelation,ECMobile, ENationality, EPictureName,IsActive,EStatus,Shift,ShiftId,IsFaculty,ForAllShift,VP,EOIMSOffice,"
                + "EOIDigitalContent,EOIMMClass,EOIAccessInt,EIIClassTeacher,EIIClassTeacher, PrDistrict ,PrThana,PrPostOffice,PerDistrict,PerThana,PerPostOffice,ESignName,EIIXISection,EIIXIISection,ENameBN,EmployeeTypeID from EmployeeInfo where EID=@EID ", prms, dt);
                if (dt.Rows.Count > 0)
                {
                    btnSave.Text = "Update";
                    //rblEmpType.SelectedValue= (dt.Rows[0]["IsFaculty"].ToString()=="True")?"1":"0";
                    rblEmpType.SelectedValue= dt.Rows[0]["EmployeeTypeID"].ToString();
                    rblviceprincipal.SelectedValue = (dt.Rows[0]["VP"].ToString() == "True") ? "1" : "0";
                    txtE_CardNo.Text = dt.Rows[0]["ECardNo"].ToString();
                    txtE_JoiningDate.Text = dt.Rows[0]["EJoiningDate"].ToString();
                   
                    
                    txtE_Name.Text = dt.Rows[0]["EName"].ToString();
                    txtTCodeNo.Text = dt.Rows[0]["TCodeNo"].ToString();
                    dlGender.Text = dt.Rows[0]["EGender"].ToString();
                    txtE_FathersName.Text = dt.Rows[0]["EFathersName"].ToString();
                    txtE_MothersName.Text = dt.Rows[0]["EMothersName"].ToString();
                    txtECName.Text = dt.Rows[0]["ECName"].ToString();
                    txtECMobile.Text = dt.Rows[0]["ECMobile"].ToString();
                    txtECRelation.Text = dt.Rows[0]["ECRelation"].ToString();
                    txttraining.Text = dt.Rows[0]["EOITraining"].ToString();

                    DataTable dtdep = new DataTable();
                    SqlParameter[] prmsdep = { new SqlParameter("@DId", dt.Rows[0]["DId"].ToString()) };
                    sqlDB.fillDataTable("select DName from Departments_HR where DId=@DId", prmsdep, dtdep);
                    dlDepartments.Text = dtdep.Rows[0]["DName"].ToString();


                    DataTable dtdesg = new DataTable();
                    SqlParameter[] prmsdesg = { new SqlParameter("@DesId", dt.Rows[0]["DesId"].ToString()) };
                    sqlDB.fillDataTable("select DesName from Designations where DesId=@DesId", prmsdesg, dtdesg);
                    dlDesignation.Text = dtdesg.Rows[0]["DesName"].ToString();

                   

                    dlReligion.Text = dt.Rows[0]["EReligion"].ToString();
                    dlMaritalStatus.Text = dt.Rows[0]["EMaritalStatus"].ToString();
                    txtE_Phone.Text = dt.Rows[0]["EPhone"].ToString();
                    txtE_Mobile.Text = dt.Rows[0]["EMobile"].ToString();
                    txtE_Email.Text = dt.Rows[0]["EEmail"].ToString();
                    txtE_Birthday.Text = dt.Rows[0]["EBirthday"].ToString();
                    CalendarExtender1.TargetControlID = "txtE_Birthday";
                    txtE_PresentAddress.Text = dt.Rows[0]["EPresentAddress"].ToString();
                    txtE_ParmanentAddress.Text = dt.Rows[0]["EParmanentAddress"].ToString();
                    dlBloodGroup.Text = dt.Rows[0]["EBloodGroup"].ToString();
                    txtE_LastDegree.Text = dt.Rows[0]["ELastDegree"].ToString();
                    ddlShift.SelectedValue = dt.Rows[0]["ShiftId"].ToString();
                    long p =long.Parse( teacherId);
                  
                    edusationlistEdit(p);
                    experilistEdit(p);
                    OthersInfoEdit(p);
                    dlClassTeacher.SelectedValue = dt.Rows[0]["EIIClassTeacher"].ToString();
                    dlToSectionXI.Text = dt.Rows[0]["EIIXISection"].ToString();
                    dlToSectionXII.Text = dt.Rows[0]["EIIXIISection"].ToString();
                   
                    if (clsgrpEntry == null)
                    {
                        clsgrpEntry = new ClassGroupEntry();
                    }
                   // clsgrpEntry.GetDropDownListClsGrpId(int.Parse(dlClassTeacher.SelectedValue==""?"0":dlClassTeacher.SelectedValue), dlGroup);
                    //ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(dlClassTeacher.SelectedValue), dlGroup.SelectedValue);

                    if (dt.Rows[0]["EExaminer"].ToString() == "True") chkExaminer.Checked = true;
                    else chkExaminer.Checked = false;
                    //if (dt.Rows[0]["EOIMSOffice"].ToString() == "True") chkEOIDigitalContent.Checked = true;
                    //else chkEOIDigitalContent.Checked = false;
                    //if (dt.Rows[0]["EOIDigitalContent"].ToString() == "True") chkEOIMSOffice.Checked = true;
                    //else chkEOIMSOffice.Checked = false;
                    //if (dt.Rows[0]["EOIMMClass"].ToString() == "True") chkEOIMMClass.Checked = true;
                    //else chkEOIMMClass.Checked = false;
                    //if (dt.Rows[0]["EOIAccessInt"].ToString() == "True") chkEOIAccessYes.Checked = true;
                    //else chkEOIAccessYes.Checked = false;
                    if (dt.Rows[0]["IsActive"].ToString() == "True") chkIsActive.Checked = true;
                    else chkIsActive.Checked = false;
                    if (dt.Rows[0]["ForAllShift"].ToString() == "True") chkForAllShift.Checked = true;
                    else chkForAllShift.Checked = false;

                    dlEStatus.Text = dt.Rows[0]["EStatus"].ToString();
                    // txtE_Nationality.Text = dt.Rows[0]["E_Nationality"].ToString();
                   ViewState["__imageName__"]= imageName = dt.Rows[0]["EPictureName"].ToString();
                   ViewState["__imageName2__"] = imageName2 = dt.Rows[0]["ESignName"].ToString();


                    string url = @"/Images/teacherProfileImage/" + Path.GetFileName(dt.Rows[0]["EPictureName"].ToString());
                    imgProfile.ImageUrl = url;
                    string url2 = @"/Images/EmpSign/" + Path.GetFileName(dt.Rows[0]["ESignName"].ToString());
                    EmpSignimgProfile.ImageUrl = url2;
                    string h = dt.Rows[0]["TCodeNo"].ToString();
                    ddlDistrict.SelectedValue = dt.Rows[0]["PrDistrict"].ToString();
                    ddlpAddress.SelectedValue = dt.Rows[0]["PerDistrict"].ToString();
                    loadthana(ddlDistrict.SelectedValue,ddlthana);
                    loadthana(ddlpAddress.SelectedValue,ddlpThana);
                    ddlthana.SelectedValue = dt.Rows[0]["PrThana"].ToString();
                    ddlpThana.SelectedValue = dt.Rows[0]["PerThana"].ToString();
                    loadPostOffice(ddlthana.SelectedValue,ddlPostOffice);
                    loadPostOffice(ddlpThana.SelectedValue,ddlpPostOffice);
                    ddlPostOffice.SelectedValue = dt.Rows[0]["PrPostOffice"].ToString();
                    ddlpPostOffice.SelectedValue = dt.Rows[0]["PerPostOffice"].ToString();
                    txtENameBN.Text = dt.Rows[0]["ENameBN"].ToString();
                    timeDuration(commonTask.ddMMyyyyToyyyyMMdd(txtE_JoiningDate.Text));
                }

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        private void postBind()
        {
            throw new NotImplementedException();
        }

        private void thanaBind(string empId)
        {
            string s = "SELECT EmployeeInfo.EID, Distritcts.DistrictId, Distritcts.DistrictName, Post_Office.PostOfficeID, Post_Office.PostOfficeName, Thanas.ThanaId, Thanas.ThanaName,"
                      + "EmployeeInfo.PrDistrict, EmployeeInfo.PrThana, EmployeeInfo.PrPostOffice, EmployeeInfo.PerDistrict, EmployeeInfo.PerThana, EmployeeInfo.PerPostOffice"
                      + "FROM   EmployeeInfo left outer JOIN Distritcts ON EmployeeInfo.PrDistrict = Distritcts.DistrictId left outer JOIN"
                      + "Thanas ON EmployeeInfo.PrThana = Thanas.DistrictId left outer JOIN Post_Office on EmployeeInfo.PrPostOffice = Post_Office.PostOfficeID Where EID='"+empId+"'";	
				
            SqlCommand c = new SqlCommand(s, DbConnection.Connection);
            SqlDataAdapter ad = new SqlDataAdapter(c);
            DataTable d = new DataTable();
            ad.Fill(d);
            ddlpThana.DataSource = d;
            ddlpThana.DataBind();
        }

        private void experilistEdit(long p)
        {
            string s = "select ExIInstName,ExIDesignation,convert(varchar(10),ExIDDateFrom,105) as ExIDDateFrom,convert(varchar(10),ExIDateTO,105) as ExIDateTO,ExIDuration from EmployeeExperience where EID=@eid";
            SqlCommand c = new SqlCommand(s, DbConnection.Connection);
            c.Parameters.AddWithValue("@eid", p);
            SqlDataAdapter a = new SqlDataAdapter(c);
            
            a.Fill(dt);
            ViewState["vs"] = dt;
            if (dt.Rows.Count > 0)
            {
                experienceList.DataSource = ViewState["vs"] as DataTable;
               experienceList.DataBind();
            }
        }

        public void edusationlistEdit(long t)
        {

            string s = "select EIExamName,EIDepertment,EIBoard,EIPassingYear,EIResult from EmployeeEducation where EID=@eid";
            SqlCommand c = new SqlCommand(s, DbConnection.Connection);
            c.Parameters.AddWithValue("@eid",t);
            SqlDataAdapter a = new SqlDataAdapter(c);
            a.Fill(dtt);
            ViewState["vss"] = dtt;
             
            if (dtt.Rows.Count>0)
            {
                educationlist.DataSource = ViewState["vss"] as DataTable;
                educationlist.DataBind();
            }
            
        }
        public void OthersInfoEdit(long t)
        {

            string s = "select * from EmployeeOthersInfo where EID=@eid";
            SqlCommand c = new SqlCommand(s, DbConnection.Connection);
            c.Parameters.AddWithValue("@eid", t);
            SqlDataAdapter a = new SqlDataAdapter(c);
            a.Fill(dtOthers);
            ViewState["vsOthers"] = dtOthers;

            if (dtOthers.Rows.Count > 0)
            {
                gvOthersInfo.DataSource = ViewState["vsOthers"] as DataTable;
                gvOthersInfo.DataBind();
            }

        }

        private void getDepartmentsList()
        {
            try
            {
                dlDepartments.Items.Clear();
                dlDepartments.Items.Add("");
                sqlDB.loadDropDownList("Select DName from Departments_HR   Order by DName ", dlDepartments);
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        private void getDesignationsList()
        {
            try
            {
                dlDesignation.Items.Clear();
                dlDesignation.Items.Add("");
                sqlDB.loadDropDownList("Select DesName from Designations   Order by DesName ", dlDesignation);
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            //if (rblEmpType.SelectedValue == "1" && txtTCodeNo.Text == "") 
            //{
               

               
            //}
            //else
            //{
            //    lblMessage.InnerText = "warning->Enter Teacher Code"; txtTCodeNo.Focus(); return;
            //}
            if (btnSave.Text == "Save")
            {
                saveEmployeeInfo();
            }

            else {
                experienceList.Visible = false;
              
                if (updateEmployeeInfo()==true)
                {
                    int eid = int.Parse(Session["eid"].ToString());
                    if (deleteEDUExpericence(eid) ==true)
                    {                        
                        saveExperience(eid);
                        saveEducation(eid);
                        saveOthersInfo(eid);
                        Response.Redirect("~/UI/Administration/HR/Employee/EmpDetails.aspx");                       
                    }
                    
                }
                btnSave.Text = "Save";
            }
            
        }

        private Boolean deleteEDUExpericence(int eid)
        {
            try
            {
                CRUD.ExecuteQuery("Delete EmployeeEducation where EID=" + eid + "");
                CRUD.ExecuteQuery("Delete EmployeeExperience where EID=" + eid + "");    
                CRUD.ExecuteQuery("Delete EmployeeOthersInfo where EID=" + eid + "");    
               
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        protected void rblEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            StafforFacultyAccess();
        }
        private void StafforFacultyAccess() 
        {
            if (rblEmpType.SelectedValue == "0")
            {
                txtTCodeNo.Text = ""; txtTCodeNo.Enabled = false; chkExaminer.Enabled = false; chkExaminer.Checked = false;
            }
            else
            {
                txtTCodeNo.Enabled = true; chkExaminer.Enabled = true; chkExaminer.Checked = true;
            }
        }
        private Boolean updateEmployeeInfo()
        {
            try
            {
                if (rblEmpType.SelectedValue == "1") { 
                DataTable dtTCodeNo = new DataTable();
                sqlDB.fillDataTable("Select TCodeNo,EID From EmployeeInfo Where TCodeNo='" + txtTCodeNo.Text.Trim() + "' ", dtTCodeNo);
                if (dtTCodeNo.Rows.Count > 0 && dtTCodeNo.Rows[0]["EID"].ToString() != teacherId)
                {
                    lblMessage.InnerText = "Already this Teacher Code No Exist ";
                    return false;
                }
                }
                if (txtE_Mobile.Text.Trim().Length != 11)
                {
                    lblMessage.InnerText = "warning-> Mobile No Must be 11 Digits";
                    txtE_Mobile.Focus();
                    return false;
                }
                if (!"017,019,018,016,015,013,014".Contains(txtE_Mobile.Text.Trim().Substring(0, 3)))
                {
                    lblMessage.InnerText = "warning-> Mobile No is Invalid";
                    txtE_Mobile.Focus();
                    return false;
                }
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@DName", dlDepartments.Text) };
                sqlDB.fillDataTable("Select DId from Departments_HR where DName=@DName ", prms, dt);

                DataTable dtdes = new DataTable();
                SqlParameter[] prmsd = { new SqlParameter("@DesName", dlDesignation.Text) };
                sqlDB.fillDataTable("Select DesId from Designations where DesName=@DesName ", prmsd, dtdes);

                //if (FileUpload1.HasFile == true && FileUpload2.HasFile==true)
                //{
                //    SqlCommand cmd = new SqlCommand(" update EmployeeInfo  Set ECardNo=@ECardNo, EJoiningDate=@EJoiningDate, EName=@EName, TCodeNo=@TCodeNo, "
                //    + "EGender=@EGender, EFathersName=@EFathersName, EMothersName=@EMothersName, DId=@DId, DesId=@DesId, EReligion=@EReligion, EMaritalStatus=@EMaritalStatus,"
                //    + "EPhone=@EPhone, EMobile=@EMobile, EEmail=@EEmail, EBirthday=@EBirthday, EPresentAddress=@EPresentAddress, EParmanentAddress=@EParmanentAddress, "
                //    + "EBloodGroup=@EBloodGroup, ELastDegree=@ELastDegree, EExaminer=@EExaminer, ENationality=@ENationality, EPictureName=@EPictureName,IsActive=@IsActive,"
                //    + "EStatus=@EStatus,Shift=@Shift,ShiftId=@ShiftId,IsFaculty=@IsFaculty,ForAllShift=@ForAllShift,VP=@VP,"
                //    + "EOIMSOffice=@EOIMSOffice,EOIDigitalContent=@EOIDigitalContent,EOIMMClass=@EOIMMClass,EOIAccessInt=@EOIAccessInt,"
                //    + "EIIClassTeacher=@EIIClassTeacher,ESignName=@ESignName,ENameBN=@ENameBN,ECName=@ECName,ECRelation=@ECRelation,"
                //    + "ECMobile=@ECMobile,EOITraining=@EOITraining,EIIXISection=@EIIXISection,EIIXIISection=@EIIXIISection,PrDistrict=@PrDistrict,PrThana=@PrThana,PrPostOffice=@PrPostOffice,PerDistrict=@PerDistrict,PerThana=@PerThana,PerPostOffice=@PerPostOffice"
                //    +" where EID=@EID ", DbConnection.Connection);

                //    cmd.Parameters.AddWithValue("@EID", Request.QueryString["TeacherId"]);
                //    cmd.Parameters.AddWithValue("@ECardNo", txtE_CardNo.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EJoiningDate",commonTask.ddMMyyyyToyyyyMMdd(txtE_JoiningDate.Text.Trim()));
                //    cmd.Parameters.AddWithValue("@EName", txtE_Name.Text.Trim());
                //    cmd.Parameters.AddWithValue("@TCodeNo", txtTCodeNo.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EGender", dlGender.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EFathersName", txtE_FathersName.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EMothersName", txtE_MothersName.Text.Trim());
                //    cmd.Parameters.AddWithValue("@DId", dt.Rows[0]["DId"].ToString());
                //    cmd.Parameters.AddWithValue("@DesId", dtdes.Rows[0]["DesId"].ToString());
                //    cmd.Parameters.AddWithValue("@EReligion", dlReligion.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EMaritalStatus", dlMaritalStatus.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EPhone", txtE_Phone.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EMobile",lblMobile.Text+txtE_Mobile.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EEmail", txtE_Email.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EBirthday", commonTask.ddMMyyyyToyyyyMMdd(txtE_Birthday.Text.Trim()));
                //    cmd.Parameters.AddWithValue("@EPresentAddress", txtE_PresentAddress.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EParmanentAddress", txtE_ParmanentAddress.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EBloodGroup", dlBloodGroup.Text.Trim());
                //    cmd.Parameters.AddWithValue("@ELastDegree", txtE_LastDegree.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EIIClassTeacher", dlClassTeacher.Text.Trim());
                //   // cmd.Parameters.AddWithValue("@EIIGroup", dlGroup.Text.Trim());
                //  // cmd.Parameters.AddWithValue("@EIISection", dlSection.Text.Trim());                   
                   
                //    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EExaminer", 1);
                //    else cmd.Parameters.AddWithValue("@EExaminer", 0);

                //    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EOIMSOffice", 1);
                //    else cmd.Parameters.AddWithValue("@EOIMSOffice", 0);
                //    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EOIDigitalContent", 1);
                //    else cmd.Parameters.AddWithValue("@EOIDigitalContent", 0);
                //    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EOIMMClass", 1);
                //    else cmd.Parameters.AddWithValue("@EOIMMClass", 0);
                //    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EOIAccessInt", 1);
                //    else cmd.Parameters.AddWithValue("@EOIAccessInt", 0);

                //    cmd.Parameters.AddWithValue("@ENationality", dlNationality.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EPictureName", teacherId + FileUpload1.FileName);
                //    cmd.Parameters.AddWithValue("@ESignName", teacherId + FileUpload2.FileName);
                //    if (chkIsActive.Checked)
                //    {
                //        cmd.Parameters.AddWithValue("@IsActive", 1);
                //    }
                //    else
                //    {
                //        cmd.Parameters.AddWithValue("@IsActive", 0);
                //    }
                //    if (chkForAllShift.Checked)
                //    {
                //        cmd.Parameters.AddWithValue("@ForAllShift", 1);
                //    }
                //    else
                //    {
                //        cmd.Parameters.AddWithValue("@ForAllShift", 0);
                //    }

                //    cmd.Parameters.AddWithValue("@EStatus", dlEStatus.Text.Trim());
                //    cmd.Parameters.AddWithValue("@ShiftId",ddlShift.SelectedValue);
                //    cmd.Parameters.AddWithValue("@Shift", ddlShift.SelectedItem.Text);
                //    cmd.Parameters.AddWithValue("@IsFaculty", rblEmpType.SelectedValue);
                //    cmd.Parameters.AddWithValue("@VP", rblviceprincipal.SelectedValue);
                //    cmd.Parameters.AddWithValue("@ENameBN", txtENameBN.Text);
                //    cmd.Parameters.AddWithValue("@ECName", txtECName.Text);
                //    cmd.Parameters.AddWithValue("@ECRelation", txtECRelation.Text);
                //    cmd.Parameters.AddWithValue("@ECMobile",txtECMobile.Text);
                //    cmd.Parameters.AddWithValue("@EOITraining", txttraining.Text);
                //    cmd.Parameters.AddWithValue("@EIIXISection", dlToSectionXI.Text);
                //    cmd.Parameters.AddWithValue("@EIIXIISection", dlToSectionXII.Text);
                //    cmd.Parameters.AddWithValue("@PrDistrict", ddlDistrict.SelectedValue);
                //    cmd.Parameters.AddWithValue("@PrThana", ddlthana.SelectedValue);
                //    cmd.Parameters.AddWithValue("@PrPostOffice", ddlPostOffice.SelectedValue);
                //    cmd.Parameters.AddWithValue("@PerDistrict", ddlpAddress.SelectedValue);
                //    cmd.Parameters.AddWithValue("@PerThana", ddlpThana.SelectedValue);
                //    cmd.Parameters.AddWithValue("@PerPostOffice", ddlpPostOffice.SelectedValue);
                //    cmd.ExecuteNonQuery();

                    
                //    if (imageName2 != "")
                //    {
                //        System.IO.File.Delete(Request.PhysicalApplicationPath + "/Images/EmpSign/" + imageName2);
                //    }
                //    string filename2 = Path.GetFileName(FileUpload2.PostedFile.FileName);
                //    FileUpload2.SaveAs(Server.MapPath("/Images/EmpSign/" + teacherId + filename2));    //Save images into Images folder

                //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                //    Response.Redirect("~/UI/Administration/HR/Employee/EmpDetails.aspx");
                //    return true;
                //}
                //else if (FileUpload1.HasFile == true && FileUpload2.HasFile==false)
                //{
                //    SqlCommand cmd = new SqlCommand(" update EmployeeInfo  Set ECardNo=@ECardNo, EJoiningDate=@EJoiningDate, EName=@EName, TCodeNo=@TCodeNo, "
                //    + "EGender=@EGender, EFathersName=@EFathersName, EMothersName=@EMothersName, DId=@DId, DesId=@DesId, EReligion=@EReligion, EMaritalStatus=@EMaritalStatus,"
                //    + "EPhone=@EPhone, EMobile=@EMobile, EEmail=@EEmail, EBirthday=@EBirthday, EPresentAddress=@EPresentAddress, EParmanentAddress=@EParmanentAddress, "
                //    + "EBloodGroup=@EBloodGroup, ELastDegree=@ELastDegree, EExaminer=@EExaminer, ENationality=@ENationality, EPictureName=@EPictureName,IsActive=@IsActive,"
                //    + "EStatus=@EStatus,Shift=@Shift,ShiftId=@ShiftId,IsFaculty=@IsFaculty,ForAllShift=@ForAllShift,VP=@VP,"
                //    + "EOIMSOffice=@EOIMSOffice,EOIDigitalContent=@EOIDigitalContent,EOIMMClass=@EOIMMClass,EOIAccessInt=@EOIAccessInt,"
                //    + "EIIClassTeacher=@EIIClassTeacher,ENameBN=@ENameBN,ECName=@ECName,ECRelation=@ECRelation,ECMobile=@ECMobile,EOITraining=@EOITraining,EIIXISection=@EIIXISection,EIIXIISection=@EIIXIISection,PrDistrict=@PrDistrict,PrThana=@PrThana,PrPostOffice=@PrPostOffice,PerDistrict=@PerDistrict,PerThana=@PerThana,PerPostOffice=@PerPostOffice where EID=@EID ", DbConnection.Connection);

                //    cmd.Parameters.AddWithValue("@EID", Request.QueryString["TeacherId"]);
                //    cmd.Parameters.AddWithValue("@ECardNo", txtE_CardNo.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EJoiningDate", commonTask.ddMMyyyyToyyyyMMdd(txtE_JoiningDate.Text.Trim()));
                //    cmd.Parameters.AddWithValue("@EName", txtE_Name.Text.Trim());
                //    cmd.Parameters.AddWithValue("@TCodeNo", txtTCodeNo.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EGender", dlGender.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EFathersName", txtE_FathersName.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EMothersName", txtE_MothersName.Text.Trim());
                //    cmd.Parameters.AddWithValue("@DId", dt.Rows[0]["DId"].ToString());
                //    cmd.Parameters.AddWithValue("@DesId", dtdes.Rows[0]["DesId"].ToString());
                //    cmd.Parameters.AddWithValue("@EReligion", dlReligion.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EMaritalStatus", dlMaritalStatus.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EPhone", txtE_Phone.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EMobile", lblMobile.Text + txtE_Mobile.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EEmail", txtE_Email.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EBirthday", commonTask.ddMMyyyyToyyyyMMdd(txtE_Birthday.Text.Trim()));
                //    cmd.Parameters.AddWithValue("@EPresentAddress", txtE_PresentAddress.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EParmanentAddress", txtE_ParmanentAddress.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EBloodGroup", dlBloodGroup.Text.Trim());
                //    cmd.Parameters.AddWithValue("@ELastDegree", txtE_LastDegree.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EIIClassTeacher", dlClassTeacher.Text.Trim());
                //    // cmd.Parameters.AddWithValue("@EIIGroup", dlGroup.Text.Trim());
                //    // cmd.Parameters.AddWithValue("@EIISection", dlSection.Text.Trim());                    

                //    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EExaminer", 1);
                //    else cmd.Parameters.AddWithValue("@EExaminer", 0);

                //    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EOIMSOffice", 1);
                //    else cmd.Parameters.AddWithValue("@EOIMSOffice", 0);
                //    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EOIDigitalContent", 1);
                //    else cmd.Parameters.AddWithValue("@EOIDigitalContent", 0);
                //    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EOIMMClass", 1);
                //    else cmd.Parameters.AddWithValue("@EOIMMClass", 0);
                //    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EOIAccessInt", 1);
                //    else cmd.Parameters.AddWithValue("@EOIAccessInt", 0);

                //    cmd.Parameters.AddWithValue("@ENationality", dlNationality.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EPictureName", teacherId + FileUpload1.FileName);                    
                //    if (chkIsActive.Checked)
                //    {
                //        cmd.Parameters.AddWithValue("@IsActive", 1);
                //    }
                //    else
                //    {
                //        cmd.Parameters.AddWithValue("@IsActive", 0);
                //    }
                //    if (chkForAllShift.Checked)
                //    {
                //        cmd.Parameters.AddWithValue("@ForAllShift", 1);
                //    }
                //    else
                //    {
                //        cmd.Parameters.AddWithValue("@ForAllShift", 0);
                //    }

                //    cmd.Parameters.AddWithValue("@EStatus", dlEStatus.Text.Trim());
                //    cmd.Parameters.AddWithValue("@ShiftId", ddlShift.SelectedValue);
                //    cmd.Parameters.AddWithValue("@Shift", ddlShift.SelectedItem.Text);
                //    cmd.Parameters.AddWithValue("@IsFaculty", rblEmpType.SelectedValue);
                //    cmd.Parameters.AddWithValue("@VP", rblviceprincipal.SelectedValue);
                //    cmd.Parameters.AddWithValue("@ENameBN", txtENameBN.Text);
                //    cmd.Parameters.AddWithValue("@ECName", txtECName.Text);
                //    cmd.Parameters.AddWithValue("@ECRelation", txtECRelation.Text);
                //    cmd.Parameters.AddWithValue("@ECMobile", txtECMobile.Text);
                //    cmd.Parameters.AddWithValue("@EOITraining", txttraining.Text);
                //    cmd.Parameters.AddWithValue("@EIIXISection", dlToSectionXI.Text);
                //    cmd.Parameters.AddWithValue("@EIIXIISection", dlToSectionXII.Text);
                //    cmd.Parameters.AddWithValue("@PrDistrict", ddlDistrict.SelectedValue);
                //    cmd.Parameters.AddWithValue("@PrThana", ddlthana.SelectedValue);
                //    cmd.Parameters.AddWithValue("@PrPostOffice", ddlPostOffice.SelectedValue);
                //    cmd.Parameters.AddWithValue("@PerDistrict", ddlpAddress.SelectedValue);
                //    cmd.Parameters.AddWithValue("@PerThana", ddlpThana.SelectedValue);
                //    cmd.Parameters.AddWithValue("@PerPostOffice", ddlpPostOffice.SelectedValue);
                //    cmd.ExecuteNonQuery();

                //    if (imageName != "")
                //    {
                //        System.IO.File.Delete(Request.PhysicalApplicationPath + "/Images/teacherProfileImage/" + imageName);
                //    }
                //    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //    FileUpload1.SaveAs(Server.MapPath("/Images/teacherProfileImage/" + teacherId + filename));    //Save images into Images folder                  

                //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                //    Response.Redirect("~/UI/Administration/HR/Employee/EmpDetails.aspx");
                //    return true;
                //}
                //else if (FileUpload1.HasFile == false && FileUpload2.HasFile == true)
                //{
                //    SqlCommand cmd = new SqlCommand(" update EmployeeInfo  Set ECardNo=@ECardNo, EJoiningDate=@EJoiningDate, EName=@EName, TCodeNo=@TCodeNo, "
                //   + "EGender=@EGender, EFathersName=@EFathersName, EMothersName=@EMothersName, DId=@DId, DesId=@DesId, EReligion=@EReligion, EMaritalStatus=@EMaritalStatus,"
                //   + "EPhone=@EPhone, EMobile=@EMobile, EEmail=@EEmail, EBirthday=@EBirthday, EPresentAddress=@EPresentAddress, EParmanentAddress=@EParmanentAddress, "
                //   + "EBloodGroup=@EBloodGroup, ELastDegree=@ELastDegree, EExaminer=@EExaminer, ENationality=@ENationality,IsActive=@IsActive,"
                //   + "EStatus=@EStatus,Shift=@Shift,ShiftId=@ShiftId,IsFaculty=@IsFaculty,ForAllShift=@ForAllShift,VP=@VP,"
                //   + "EOIMSOffice=@EOIMSOffice,EOIDigitalContent=@EOIDigitalContent,EOIMMClass=@EOIMMClass,EOIAccessInt=@EOIAccessInt,"
                //   + "EIIClassTeacher=@EIIClassTeacher,ESignName=@ESignName,ENameBN=@ENameBN,ECName=@ECName,ECRelation=@ECRelation,ECMobile=@ECMobile,EOITraining=@EOITraining,EIIXISection=@EIIXISection,EIIXIISection=@EIIXIISection,PrDistrict=@PrDistrict,PrThana=@PrThana,PrPostOffice=@PrPostOffice,PerDistrict=@PerDistrict,PerThana=@PerThana,PerPostOffice=@PerPostOffice where EID=@EID ", DbConnection.Connection);

                //    cmd.Parameters.AddWithValue("@EID", Request.QueryString["TeacherId"]);
                //    cmd.Parameters.AddWithValue("@ECardNo", txtE_CardNo.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EJoiningDate", commonTask.ddMMyyyyToyyyyMMdd(txtE_JoiningDate.Text.Trim()));
                //    cmd.Parameters.AddWithValue("@EName", txtE_Name.Text.Trim());
                //    cmd.Parameters.AddWithValue("@TCodeNo", txtTCodeNo.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EGender", dlGender.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EFathersName", txtE_FathersName.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EMothersName", txtE_MothersName.Text.Trim());
                //    cmd.Parameters.AddWithValue("@DId", dt.Rows[0]["DId"].ToString());
                //    cmd.Parameters.AddWithValue("@DesId", dtdes.Rows[0]["DesId"].ToString());
                //    cmd.Parameters.AddWithValue("@EReligion", dlReligion.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EMaritalStatus", dlMaritalStatus.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EPhone", txtE_Phone.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EMobile", lblMobile.Text + txtE_Mobile.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EEmail", txtE_Email.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EBirthday", commonTask.ddMMyyyyToyyyyMMdd(txtE_Birthday.Text.Trim()));
                //    cmd.Parameters.AddWithValue("@EPresentAddress", txtE_PresentAddress.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EParmanentAddress", txtE_ParmanentAddress.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EBloodGroup", dlBloodGroup.Text.Trim());
                //    cmd.Parameters.AddWithValue("@ELastDegree", txtE_LastDegree.Text.Trim());
                //    cmd.Parameters.AddWithValue("@EIIClassTeacher", dlClassTeacher.Text.Trim());
                //    // cmd.Parameters.AddWithValue("@EIIGroup", dlGroup.Text.Trim());
                //    // cmd.Parameters.AddWithValue("@EIISection", dlSection.Text.Trim());                    

                //    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EExaminer", 1);
                //    else cmd.Parameters.AddWithValue("@EExaminer", 0);

                //    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EOIMSOffice", 1);
                //    else cmd.Parameters.AddWithValue("@EOIMSOffice", 0);
                //    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EOIDigitalContent", 1);
                //    else cmd.Parameters.AddWithValue("@EOIDigitalContent", 0);
                //    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EOIMMClass", 1);
                //    else cmd.Parameters.AddWithValue("@EOIMMClass", 0);
                //    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EOIAccessInt", 1);
                //    else cmd.Parameters.AddWithValue("@EOIAccessInt", 0);

                //    cmd.Parameters.AddWithValue("@ENationality", dlNationality.Text.Trim());
                    
                //    cmd.Parameters.AddWithValue("@ESignName", teacherId + FileUpload2.FileName);
                //    if (chkIsActive.Checked)
                //    {
                //        cmd.Parameters.AddWithValue("@IsActive", 1);
                //    }
                //    else
                //    {
                //        cmd.Parameters.AddWithValue("@IsActive", 0);
                //    }
                //    if (chkForAllShift.Checked)
                //    {
                //        cmd.Parameters.AddWithValue("@ForAllShift", 1);
                //    }
                //    else
                //    {
                //        cmd.Parameters.AddWithValue("@ForAllShift", 0);
                //    }

                //    cmd.Parameters.AddWithValue("@EStatus", dlEStatus.Text.Trim());
                //    cmd.Parameters.AddWithValue("@ShiftId", ddlShift.SelectedValue);
                //    cmd.Parameters.AddWithValue("@Shift", ddlShift.SelectedItem.Text);
                //    cmd.Parameters.AddWithValue("@IsFaculty", rblEmpType.SelectedValue);
                //    cmd.Parameters.AddWithValue("@VP", rblviceprincipal.SelectedValue);
                //    cmd.Parameters.AddWithValue("@ENameBN", txtENameBN.Text);
                //    cmd.Parameters.AddWithValue("@ECName", txtECName.Text);
                //    cmd.Parameters.AddWithValue("@ECRelation", txtECRelation.Text);
                //    cmd.Parameters.AddWithValue("@ECMobile", txtECMobile.Text);
                //    cmd.Parameters.AddWithValue("@EOITraining", txttraining.Text);
                //    cmd.Parameters.AddWithValue("@EIIXISection", dlToSectionXI.Text);
                //    cmd.Parameters.AddWithValue("@EIIXIISection", dlToSectionXII.Text);
                //    cmd.Parameters.AddWithValue("@PrDistrict", ddlDistrict.SelectedValue);
                //    cmd.Parameters.AddWithValue("@PrThana", ddlthana.SelectedValue);
                //    cmd.Parameters.AddWithValue("@PrPostOffice", ddlPostOffice.SelectedValue);
                //    cmd.Parameters.AddWithValue("@PerDistrict", ddlpAddress.SelectedValue);
                //    cmd.Parameters.AddWithValue("@PerThana", ddlpThana.SelectedValue);
                //    cmd.Parameters.AddWithValue("@PerPostOffice", ddlpPostOffice.SelectedValue);
                //    cmd.ExecuteNonQuery();                  
                //    if (imageName2 != "")
                //    {
                //        System.IO.File.Delete(Request.PhysicalApplicationPath + "/Images/EmpSign/" + imageName2);
                //    }
                //    string filename2 = Path.GetFileName(FileUpload2.PostedFile.FileName);
                //    FileUpload2.SaveAs(Server.MapPath("/Images/EmpSign/" + teacherId + filename2));    //Save images into Images folder

                //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                //    Response.Redirect("~/UI/Administration/HR/Employee/EmpDetails.aspx");
                //    return true;
                //}
                //else
                //{
                    SqlCommand cmd = new SqlCommand(" update EmployeeInfo  Set ECardNo=@ECardNo, EJoiningDate=@EJoiningDate, EName=@EName, TCodeNo=@TCodeNo, "
                    + "EGender=@EGender, EFathersName=@EFathersName, EMothersName=@EMothersName, DId=@DId, DesId=@DesId, EReligion=@EReligion, EMaritalStatus=@EMaritalStatus,"
                    + "EPhone=@EPhone, EMobile=@EMobile, EEmail=@EEmail, EBirthday=@EBirthday, EPresentAddress=@EPresentAddress, EParmanentAddress=@EParmanentAddress, "
                    + "EBloodGroup=@EBloodGroup, ELastDegree=@ELastDegree, EExaminer=@EExaminer, ENationality=@ENationality,IsActive=@IsActive,EStatus=@EStatus, "
                    + "ShiftId=@ShiftId,Shift=@Shift,ForAllShift=@ForAllShift,VP=@VP,"
                    + "EOIMSOffice=@EOIMSOffice,EOIDigitalContent=@EOIDigitalContent,EOIMMClass=@EOIMMClass,EOIAccessInt=@EOIAccessInt,"
                    + "EIIClassTeacher=@EIIClassTeacher,ENameBN=@ENameBN,ECName=@ECName,ECRelation=@ECRelation,ECMobile=@ECMobile,EOITraining=@EOITraining,EIIXISection=@EIIXISection,EIIXIISection=@EIIXIISection,PrDistrict=@PrDistrict,PrThana=@PrThana,PrPostOffice=@PrPostOffice,PerDistrict=@PerDistrict,PerThana=@PerThana,PerPostOffice=@PerPostOffice,IsFaculty=@IsFaculty,EmployeeTypeID=@EmployeeTypeID where EID=@EID ", DbConnection.Connection);
                    
                    cmd.Parameters.AddWithValue("@EID", Request.QueryString["TeacherId"]);
                    cmd.Parameters.AddWithValue("@ECardNo", txtE_CardNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@EJoiningDate", commonTask.ddMMyyyyToyyyyMMdd(txtE_JoiningDate.Text.Trim()));
                    cmd.Parameters.AddWithValue("@EName", txtE_Name.Text.Trim());
                    cmd.Parameters.AddWithValue("@TCodeNo", txtTCodeNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@EGender", dlGender.Text.Trim());
                    cmd.Parameters.AddWithValue("@EFathersName", txtE_FathersName.Text.Trim());
                    cmd.Parameters.AddWithValue("@EMothersName", txtE_MothersName.Text.Trim());
                    cmd.Parameters.AddWithValue("@DId", dt.Rows[0]["DId"].ToString());
                    cmd.Parameters.AddWithValue("@DesId", dtdes.Rows[0]["DesId"].ToString());
                    cmd.Parameters.AddWithValue("@EReligion", dlReligion.Text.Trim());
                    cmd.Parameters.AddWithValue("@EMaritalStatus", dlMaritalStatus.Text.Trim());
                    cmd.Parameters.AddWithValue("@EPhone", txtE_Phone.Text.Trim());
                    cmd.Parameters.AddWithValue("@EMobile", lblMobile.Text + txtE_Mobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@EEmail", txtE_Email.Text.Trim());
                    cmd.Parameters.AddWithValue("@EBirthday", commonTask.ddMMyyyyToyyyyMMdd(txtE_Birthday.Text.Trim()));
                    cmd.Parameters.AddWithValue("@EPresentAddress", txtE_PresentAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@EParmanentAddress", txtE_ParmanentAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@EBloodGroup", dlBloodGroup.Text.Trim());
                    cmd.Parameters.AddWithValue("@ELastDegree", txtE_LastDegree.Text.Trim());
                    cmd.Parameters.AddWithValue("@EIIClassTeacher", dlClassTeacher.Text.Trim());
                   // cmd.Parameters.AddWithValue("@EIIGroup", dlGroup.Text.Trim());
                   // cmd.Parameters.AddWithValue("@EIISection", dlSection.Text.Trim());                   

                    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EExaminer", 1);
                    else cmd.Parameters.AddWithValue("@EExaminer", 0);

                    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EOIMSOffice", 1);
                    else cmd.Parameters.AddWithValue("@EOIMSOffice", 0);
                    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EOIDigitalContent", 1);
                    else cmd.Parameters.AddWithValue("@EOIDigitalContent", 0);
                    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EOIMMClass", 1);
                    else cmd.Parameters.AddWithValue("@EOIMMClass", 0);
                    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EOIAccessInt", 1);
                    else cmd.Parameters.AddWithValue("@EOIAccessInt", 0);

                    cmd.Parameters.AddWithValue("@ENationality", dlNationality.Text.Trim());
                    if (chkIsActive.Checked)
                    {
                        cmd.Parameters.AddWithValue("@IsActive", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@IsActive", 0);
                    }                  

                    cmd.Parameters.AddWithValue("@EStatus", dlEStatus.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShiftId", ddlShift.SelectedValue);
                    cmd.Parameters.AddWithValue("@Shift", ddlShift.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@IsFaculty",(rblEmpType.SelectedValue=="1")?"1":"0");
                    cmd.Parameters.AddWithValue("@VP", rblviceprincipal.SelectedValue);
                    cmd.Parameters.AddWithValue("@ENameBN", txtENameBN.Text);
                    cmd.Parameters.AddWithValue("@ECName", txtECName.Text);
                    cmd.Parameters.AddWithValue("@ECRelation", txtECRelation.Text);
                    cmd.Parameters.AddWithValue("@ECMobile", txtECMobile.Text);
                    cmd.Parameters.AddWithValue("@EOITraining", txttraining.Text);
                    cmd.Parameters.AddWithValue("@EIIXISection", dlToSectionXI.Text);
                    cmd.Parameters.AddWithValue("@EIIXIISection", dlToSectionXII.Text);
                    cmd.Parameters.AddWithValue("@PrDistrict", ddlDistrict.SelectedValue);
                    cmd.Parameters.AddWithValue("@PrThana", ddlthana.SelectedValue);
                    cmd.Parameters.AddWithValue("@PrPostOffice", ddlPostOffice.SelectedValue);
                    cmd.Parameters.AddWithValue("@PerDistrict", ddlpAddress.SelectedValue);
                    cmd.Parameters.AddWithValue("@PerThana", ddlpThana.SelectedValue);
                    cmd.Parameters.AddWithValue("@PerPostOffice", ddlpPostOffice.SelectedValue);
                    if (chkForAllShift.Checked)
                    {
                        cmd.Parameters.AddWithValue("@ForAllShift", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ForAllShift", 0);
                    }
                    cmd.Parameters.AddWithValue("@EmployeeTypeID", rblEmpType.SelectedValue);
                    int a = int.Parse(cmd.ExecuteNonQuery().ToString());
                    if (FileUpload1.HasFile)
                    {
                        deleteImg(Server.MapPath("/Images/teacherProfileImage/" + ViewState["__imageName__"].ToString()));
                        saveImg(int.Parse(teacherId));
                    }
                    if (FileUpload2.HasFile)
                    {
                        deleteImg(Server.MapPath("/Images/EmpSign/" + ViewState["__imageName2__"].ToString()));
                        saveImgSign(int.Parse(teacherId));
                    }                        
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);                   
                    return true;
                //}
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private void saveTeacherImg()
        {
            try {
                if (imageName != "")
                {
                    System.IO.File.Delete(Request.PhysicalApplicationPath + "/Images/teacherProfileImage/" + imageName);
                }
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("/Images/teacherProfileImage/" + teacherId + filename));    //Save images into Images folder 
            } catch { }
        }
       
        private Boolean saveEmployeeInfo()
        {
            try
            {
                if (checkMandatoryData() == false) return false;
                if (rblEmpType.SelectedValue == "1")
                {
                    DataTable dtTCodeNo = new DataTable();
                    dtTCodeNo = CRUD.ReturnTableNull("Select TCodeNo From EmployeeInfo Where TCodeNo='" + txtTCodeNo.Text.Trim() + "' ");
                    if (dtTCodeNo.Rows.Count > 0)
                    {
                        lblMessage.InnerText = "warning->Already this Teacher Code No Exist";
                        txtE_CardNo.Focus();
                        return false;
                    }
                }
                DataTable dtCardNo = new DataTable();
                dtCardNo = CRUD.ReturnTableNull("Select ECardNo From EmployeeInfo Where ECardNo='" + txtE_CardNo.Text.Trim() + "' ");
                if (dtCardNo.Rows.Count > 0)
                {
                    lblMessage.InnerText = "warning->Already this Card No Exist";
                    return false;
                }
                if(txtE_Mobile.Text.Trim().Length!=11)
                {
                    lblMessage.InnerText = "warning-> Mobile No Must be 11 Digits";
                    txtE_Mobile.Focus();
                    return false;
                }
                if ( !"017,019,018,016,015".Contains(txtE_Mobile.Text.Trim().Substring(0, 3)))
                {
                    lblMessage.InnerText = "warning-> Mobile No is Invalid";
                    txtE_Mobile.Focus();
                    return false;
                }

                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@DName", dlDepartments.Text) };
                sqlDB.fillDataTable("Select DId from Departments_HR where DName=@DName ", prms, dt);

                DataTable dtdes = new DataTable();
                SqlParameter[] prmsd = { new SqlParameter("@DesName", dlDesignation.Text) };
                sqlDB.fillDataTable("Select DesId from Designations where DesName=@DesName ", prmsd, dtdes);



                SqlCommand cmd = new SqlCommand("saveEmployeeInfo", DbConnection.Connection);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@ECardNo", txtE_CardNo.Text.Trim());
                cmd.Parameters.AddWithValue("@EJoiningDate", commonTask.ddMMyyyyToyyyyMMdd(txtE_JoiningDate.Text.Trim()));
                cmd.Parameters.AddWithValue("@EName", txtE_Name.Text.Trim());
                cmd.Parameters.AddWithValue("@TCodeNo", txtTCodeNo.Text.Trim());
                cmd.Parameters.AddWithValue("@EGender", dlGender.Text.Trim());
                cmd.Parameters.AddWithValue("@EFathersName", txtE_FathersName.Text.Trim());
                cmd.Parameters.AddWithValue("@EMothersName", txtE_MothersName.Text.Trim());
                cmd.Parameters.AddWithValue("@DId", dt.Rows[0]["DId"].ToString());
                cmd.Parameters.AddWithValue("@DesId", dtdes.Rows[0]["DesId"].ToString());
                cmd.Parameters.AddWithValue("@EReligion", dlReligion.Text.Trim());
                cmd.Parameters.AddWithValue("@EMaritalStatus", dlMaritalStatus.Text.Trim());
                cmd.Parameters.AddWithValue("@EPhone", txtE_Phone.Text.Trim());
                cmd.Parameters.AddWithValue("@EMobile",lblMobile.Text+txtE_Mobile.Text.Trim());
                cmd.Parameters.AddWithValue("@EEmail", txtE_Email.Text.Trim());
                cmd.Parameters.AddWithValue("@EBirthday",commonTask.ddMMyyyyToyyyyMMdd(txtE_Birthday.Text));
                cmd.Parameters.AddWithValue("@EPresentAddress", txtE_PresentAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@EParmanentAddress", txtE_ParmanentAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@EBloodGroup", dlBloodGroup.Text.Trim());
                cmd.Parameters.AddWithValue("@ELastDegree", txtE_LastDegree.Text.Trim());
                cmd.Parameters.AddWithValue("@EExaminer", 1);
                cmd.Parameters.AddWithValue("@ENationality", dlNationality.Text.Trim());
                cmd.Parameters.AddWithValue("@EPictureName", "");
                cmd.Parameters.AddWithValue("@ENameBN", txtENameBN.Text.Trim());
                cmd.Parameters.AddWithValue("@ECName", txtECName.Text.Trim());
                cmd.Parameters.AddWithValue("@ECRelation", txtECRelation.Text.Trim());
                cmd.Parameters.AddWithValue("@ECMobile", txtECMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@EOITraining", txttraining.Text.Trim());
                cmd.Parameters.AddWithValue("@Shift", ddlShift.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@ShiftId",ddlShift.SelectedValue);
                cmd.Parameters.AddWithValue("@IsFaculty", rblEmpType.SelectedValue);
                cmd.Parameters.AddWithValue("@VP", rblviceprincipal.SelectedValue);
                cmd.Parameters.AddWithValue("@EIIClassTeacher", dlClassTeacher.SelectedValue);
               //cmd.Parameters.AddWithValue("@EIIGroup", dlGroup.SelectedValue);
               //cmd.Parameters.AddWithValue("@EIISubjectId", dlSubject.SelectedValue);
                cmd.Parameters.AddWithValue("@PrDistrict", ddlDistrict.SelectedValue);
                cmd.Parameters.AddWithValue("@PrThana", ddlthana.SelectedValue);
                cmd.Parameters.AddWithValue("@PrPostOffice", ddlPostOffice.SelectedValue);
                cmd.Parameters.AddWithValue("@PerDistrict", ddlpAddress.SelectedValue);
                cmd.Parameters.AddWithValue("@PerThana", ddlpThana.SelectedValue);
                cmd.Parameters.AddWithValue("@PerPostOffice", ddlpPostOffice.SelectedValue);
                cmd.Parameters.AddWithValue("@EIIXISection", dlToSectionXI.Text.Trim());
                cmd.Parameters.AddWithValue("@EIIXIISection", dlToSectionXII.Text.Trim());
                cmd.Parameters.AddWithValue("@ESignName", "");
                if (chkIsActive.Checked)
                {
                    cmd.Parameters.AddWithValue("@IsActive", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@IsActive", 0);
                }

                cmd.Parameters.AddWithValue("@EStatus", dlEStatus.Text.Trim());
                if (chkForAllShift.Checked)
                {
                    cmd.Parameters.AddWithValue("@ForAllShift", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ForAllShift", 0);
                }
                //if (chkEOIMSOffice.Checked)
                //{
                //    cmd.Parameters.AddWithValue("@EOIMSOffice", 1);
                //}
                //else
                //{
                    cmd.Parameters.AddWithValue("@EOIMSOffice", 0);
                //}
                //if (chkEOIDigitalContent.Checked)
                //{
                //    cmd.Parameters.AddWithValue("@EOIDigitalContent", 1);
                //}
                //else
                //{
                    cmd.Parameters.AddWithValue("@EOIDigitalContent", 0);
                //}
                //if (chkEOIMMClass.Checked)
                //{
                //   cmd.Parameters.AddWithValue("@EOIMMClass", 1);
                //}
                //else
                //{
                    cmd.Parameters.AddWithValue("@EOIMMClass", 0);
                //}
                //if (chkEOIAccessYes.Checked)
                //{
                 //   cmd.Parameters.AddWithValue("@EOIAccessInt", 1);
                //}
                //else
                //{
                    cmd.Parameters.AddWithValue("@EOIAccessInt", 0);
                //}
                cmd.Parameters.AddWithValue("@EmployeeTypeID", rblEmpType.SelectedValue);
                string r  = cmd.ExecuteScalar().ToString();
                int result = int.Parse(r);
                if (result>0)
                {
                    saveExperience(result);
                    saveEducation(result);
                    saveOthersInfo(result);
                    if (FileUpload1.HasFile)
                    saveImg(result);
                    if (FileUpload2.HasFile)
                        saveImgSign(result);

                    clearTextBox();
                    Session["__afterSave__"] = "true";
                    Response.Redirect("~/UI/Administration/HR/Employee/EmpRegForm.aspx");
                    //lblMessage.InnerText = "success->Save Successfully";
                }
               
                else lblMessage.InnerText = "error->Unable to save";                

                return true;

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
      
        Boolean checkMandatoryData()
        {
            try
            {
                if (txtE_CardNo.Text == "")
                {
                    lblMessage.InnerText = "warning->" + "Input Card Number";
                    return false;
                }
                if (txtE_Name.Text == "")
                {
                    lblMessage.InnerText = "warning->" + "Input Name";
                    return false;
                }
                if (txtTCodeNo.Text == "" && rblEmpType.SelectedValue=="1")
                {
                    lblMessage.InnerText = "warning->" + "Input Teacher Code No.";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch { return false; }
        }

        private void saveImg(int EID)
        {
            try
            {
              
                //Get Filename from fileupload control
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //Save images into Images folder
                FileUpload1.SaveAs(Server.MapPath("/Images/teacherProfileImage/" + EID + filename));

                SqlCommand cmd = new SqlCommand("update EmployeeInfo set EPictureName=@EPictureName where EID=@EID", DbConnection.Connection);
                cmd.Parameters.AddWithValue("@EID", EID);
                cmd.Parameters.AddWithValue("@EPictureName", EID + filename);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }
        private void saveImgSign(int EID)
        {
            try
            {
                //Get Filename from fileupload control
                string filename = Path.GetFileName(FileUpload2.PostedFile.FileName);
                //Save images into Images folder
                FileUpload2.SaveAs(Server.MapPath("/Images/EmpSign/" + EID + filename));

                SqlCommand cmd = new SqlCommand("update EmployeeInfo set ESignName=@ESignName where EID=@EID", DbConnection.Connection);
                cmd.Parameters.AddWithValue("@EID", EID);
                cmd.Parameters.AddWithValue("@ESignName", EID + filename);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }
        private void deleteImg(string location)
        {
            try {
                if (File.Exists(location))
                    File.Delete(location);
            }
            catch(Exception ex) { }
        }

        private void saveExperience(int eid)
        {
            foreach (GridViewRow ex in experienceList.Rows)
            {

                string insname = ex.Cells[1].Text;
                string desig = ex.Cells[2].Text;
                string dateTodate = commonTask.ddMMyyyyToyyyyMMdd(ex.Cells[3].Text);
                string toDate = commonTask.ddMMyyyyToyyyyMMdd(ex.Cells[4].Text);
                string totalDuration = ex.Cells[5].Text;
                //long eid = getEID();
                SqlCommand cmd = new SqlCommand("insert into EmployeeExperience values(" + eid + ",'" + insname + "','" + desig + "','" + dateTodate + "','" + toDate + "','" + totalDuration + "')", DbConnection.Connection);           
               
                cmd.ExecuteNonQuery();
                
            }
        }
      
        private void saveEducation(int eid)
        {
            foreach (GridViewRow ex in educationlist.Rows)
            {
                string examname = ex.Cells[1].Text;
                string depertment = ex.Cells[2].Text;
                string board = ex.Cells[3].Text;
                string passingyear = ex.Cells[4].Text;
                string result = ex.Cells[5].Text;
               
                SqlCommand cmd = new SqlCommand("insert into EmployeeEducation values(@eid,@examname,@depertment,@board,@passingyear,@result)", DbConnection.Connection);
                cmd.Parameters.AddWithValue("@eid", eid);
                cmd.Parameters.AddWithValue("@examname", examname);
                cmd.Parameters.AddWithValue("@depertment", depertment);
                cmd.Parameters.AddWithValue("@board", board);
                cmd.Parameters.AddWithValue("@passingyear", passingyear);
                cmd.Parameters.AddWithValue("@result", result);

              
                cmd.ExecuteNonQuery();
              
            }
        }
        private void saveOthersInfo(int eid)
        {
            foreach (GridViewRow ex in gvOthersInfo.Rows)
            {
                string OthersInfo = ex.Cells[1].Text;
                SqlCommand cmd = new SqlCommand("insert into EmployeeOthersInfo values(@EID,@OthersInfo)", DbConnection.Connection);
                cmd.Parameters.AddWithValue("@EID", eid);
                cmd.Parameters.AddWithValue("@OthersInfo", OthersInfo);
                cmd.ExecuteNonQuery();

            }
        }
        private void clearTextBox()
        {
            try
            {
                txtE_Birthday.Text = "";
                txtE_CardNo.Text = "";
                txtE_Email.Text = "";
                txtE_FathersName.Text = "";
                txtE_JoiningDate.Text = "";
                txtE_LastDegree.Text = "";
                txtE_Mobile.Text = "";
                txtE_MothersName.Text = "";
                txtE_Name.Text = "";
                txtTCodeNo.Text = "";
                txtE_ParmanentAddress.Text = "";
                txtE_Phone.Text = "";
                txtE_PresentAddress.Text = "";
                chkIsActive.Checked = true;
                txtE_LastDegree.Text = "";
                chkForAllShift.Checked = true;
                imgProfile.ImageUrl = "/Images/profileImages/noProfileImage.jpg";
            }
            catch { }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearTextBox();
        }

        //protected void dlClassTeacher_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "tabControl33();", true);
        //    if (clsgrpEntry == null)
        //    {
        //        clsgrpEntry = new ClassGroupEntry();
        //    }
        //    clsgrpEntry.GetDropDownListClsGrpId(int.Parse(dlClassTeacher.SelectedValue), dlGroup);
        //  int   clsid=int.Parse(dlClassTeacher.SelectedValue);
        //  string s = "SELECT  Classes.ClassID, Tbl_Group.GroupID, Tbl_Group.GroupName Classes INNER JOIN Tbl_Class_Group ON Classes.ClassID = Tbl_Class_Group.ClassID INNER JOIN Tbl_Group ON Tbl_Class_Group.GroupID = Tbl_Group.GroupID where Classes.ClassID=" + clsid + "";
        //  SqlCommand cmd = new SqlCommand(s,DbConnection.Connection);
        //  SqlDataAdapter ad = new SqlDataAdapter(cmd);
        //  DataTable d = new DataTable();
        //  ad.Fill(d);
        //  if (d.Rows.Count>0)
        //  {
        //      dlGroup.DataTextField = d.Rows[0][2].ToString();
        //      dlGroup.DataValueField = d.Rows[0][1].ToString();
        //  }
        //  else
        //  {
        //      dlGroup.DataTextField = "No group";
        //      dlGroup.DataValueField = "0";
        //  }
        //    //ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(dlClassTeacher.SelectedValue), dlGroup.SelectedValue);
        //}

        //protected void dlGroup_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
        //    //ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(dlClassTeacher.SelectedValue), dlGroup.SelectedValue);
        //}


       
        
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "tabControl();", true);          
           
            if (btnAdd.Text=="Update")
            {

            long i=Convert.ToInt64( Session["S1"]);

                DataTable dttup = new DataTable();
                dttup = ViewState["vs"] as DataTable;

                foreach (DataRow p in dttup.Rows)
                {
                    string iii = (p["SL"].ToString());
                    if (iii == i.ToString())
                    {
                        p["ExIInstName"] = txtinstituteName.Text;
                        p["ExIDesignation"] = txtDesidnation.Text;
                        p["ExIDDateFrom"] = txtDateFromTo.Text;
                        p["ExIDateTO"] = txt_toDate.Text;
                        p["ExIDuration"] = txtTotalDuration.Text;
                        experienceList.DataSource = ViewState["vs"] as DataTable;
                        experienceList.DataBind();
                        btnAdd.Text = "Add";
                        clearExp();
                    }
                }                
            }
            else
            {

         
            dt = ViewState["vs"] as DataTable;
            DataRow dr = dt.NewRow();
            dr[1] = txtinstituteName.Text;
            dr[2] = txtDesidnation.Text;
            dr[3] = txtDateFromTo.Text;
            dr[4] = txt_toDate.Text;
            dr[5] = txtTotalDuration.Text;
            dt.Rows.Add(dr);
            ViewState["vs"] = dt;
            experienceList.DataSource = ViewState["vs"] as DataTable;
            experienceList.DataBind();
            clearExp();
            }
        }

        private void clearExp()
        {
           txtinstituteName.Text="";
           txtDesidnation.Text="";
           txtDateFromTo.Text="";
           txt_toDate.Text="";
           txtTotalDuration.Text="";
        }
        private void clearOthers()
        {
            txtOthersInfo.Text = "";
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "tabControl();", true);          
            DataTable dt = ViewState["vs"] as DataTable;
            LinkButton clickedbutton = (LinkButton)sender;
            GridViewRow row = (GridViewRow)clickedbutton.NamingContainer;
            int idx = row.RowIndex;

            string idd = experienceList.Rows[idx].Cells[0].Text;
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (dr["SL"].ToString() == idd.ToString())
                    dr.Delete();
            }
            dt.AcceptChanges();
            experienceList.DataSource = dt;
            experienceList.DataBind();
            clearEdu();
        }

        protected void btnAddEducation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "tabControl2();", true);
            
            if (btnAddEducation.Text=="Update")
            {
                string i = Session["S"].ToString();
                DataTable dttup = new DataTable();
                dttup = ViewState["vss"] as DataTable;

                foreach (DataRow p in dttup.Rows) 
                {
                    string iii = (p["SL"].ToString());
                    if (iii == i) 
                    {
                        p["EIExamName"] = txtEIExamName.Text;
                        p["EIDepertment"] = txtEIDepertment.Text;
                        p["EIBoard"] = txtEIBoard.Text;
                        p["EIPassingYear"] = txtEIPassingYear.Text;
                        p["EIResult"] = txtEIResult.Text;
                        educationlist.DataSource = ViewState["vss"] as DataTable;
                        educationlist.DataBind();
                        btnAddEducation.Text = "Add";
                        clearEdu();
                    }
                }
            }
            else
            {
                dtt = ViewState["vss"] as DataTable;
                DataRow drr = dtt.NewRow();
                dtt.Columns["SL"].AutoIncrement = true;
                dtt.Columns["SL"].AutoIncrementSeed = 1;
                dtt.Columns["SL"].AutoIncrementStep = 1;
                drr[1] = txtEIExamName.Text;
                drr[2] = txtEIDepertment.Text;
                drr[3] = txtEIBoard.Text;
                drr[4] = txtEIPassingYear.Text;
                drr[5] = txtEIResult.Text;
                dtt.Rows.Add(drr);
                ViewState["vss"] = dtt;
                educationlist.DataSource = ViewState["vss"] as DataTable;
                educationlist.DataBind();
                clearEdu();
            }
        }

        private void clearEdu()
        {
            txtEIExamName.Text = "";
            txtEIDepertment.Text = "";
            txtEIBoard.Text = "";
            txtEIPassingYear.Text = "";
            txtEIResult.Text = "";
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "tabControl2();", true);
            DataTable dt = ViewState["vss"] as DataTable;
            LinkButton clickedbutton = (LinkButton)sender;
            GridViewRow row = (GridViewRow)clickedbutton.NamingContainer;
            int idx = row.RowIndex;

            string idd = educationlist.Rows[idx].Cells[0].Text;
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (dr["SL"].ToString() == idd.ToString())
                    dr.Delete();
            }
            dt.AcceptChanges();
            ViewState["vss"] = dt;
            educationlist.DataSource = dt;
            educationlist.DataBind();
        }
        protected void lnkeduedit_Click(object sender, EventArgs e)
        {
            btnAddEducation.Text = "Update";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "tabControl2();", true);

            LinkButton clickedbutton = (LinkButton)sender; //get clicked button
            GridViewRow row = (GridViewRow)clickedbutton.NamingContainer; //get the row where the button clicked
            int idx = row.RowIndex;

           string idd =educationlist.Rows[idx].Cells[0].Text;

            Session["S"] = idd;
            txtEIExamName.Text = educationlist.Rows[idx].Cells[1].Text == "&nbsp;" ? "" : educationlist.Rows[idx].Cells[1].Text;
            txtEIDepertment.Text = educationlist.Rows[idx].Cells[2].Text == "&nbsp;" ? "" : educationlist.Rows[idx].Cells[2].Text;
            txtEIBoard.Text = educationlist.Rows[idx].Cells[3].Text == "&nbsp;" ? "" : educationlist.Rows[idx].Cells[3].Text;
            txtEIPassingYear.Text = educationlist.Rows[idx].Cells[4].Text == "&nbsp;" ? "" : educationlist.Rows[idx].Cells[4].Text;
            txtEIResult.Text = educationlist.Rows[idx].Cells[5].Text == "&nbsp;" ? "" : educationlist.Rows[idx].Cells[5].Text;
        }


        protected void btnlinkEdit_Click(object sender, EventArgs e)
        {
            btnAdd.Text = "Update";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "tabControl();", true);

            LinkButton clickedbutton = (LinkButton)sender; //get clicked button
            GridViewRow row = (GridViewRow)clickedbutton.NamingContainer; //get the row where the button clicked
            int idx = row.RowIndex;

            string idd = experienceList.Rows[idx].Cells[0].Text;

            Session["S1"] = idd;
            txtinstituteName.Text = experienceList.Rows[idx].Cells[1].Text == "&nbsp;" ? "" : experienceList.Rows[idx].Cells[1].Text;
            txtDesidnation.Text = experienceList.Rows[idx].Cells[2].Text == "&nbsp;" ? "" : experienceList.Rows[idx].Cells[2].Text;
            txtDateFromTo.Text = experienceList.Rows[idx].Cells[3].Text == "&nbsp;" ? "" : experienceList.Rows[idx].Cells[3].Text;
            txt_toDate.Text = experienceList.Rows[idx].Cells[4].Text == "&nbsp;" ? "" : experienceList.Rows[idx].Cells[4].Text;
            txtTotalDuration.Text = experienceList.Rows[idx].Cells[5].Text == "&nbsp;" ? "" : experienceList.Rows[idx].Cells[5].Text;
        }

       
        protected void txtE_JoiningDate_TextChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "tabControl33();", true);
            timeDuration(commonTask.ddMMyyyyToyyyyMMdd(txtE_JoiningDate.Text));

           
        }
        private void timeDuration(string date)
        {
            try
            {


                string[] duration = commonTask.getTimeDuration(DateTime.Parse(date));
                dlcYear.Text = duration[0] + " Years";
                dlcMonth.Text = duration[1] + " Months";
                dlcDay.Text = duration[2] + " Days";
            }
            catch { }
        }
      

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadthana(ddlDistrict.SelectedValue,ddlthana);
        }
        private void loadthana(string distid,DropDownList ddl)
        {
            int i = Int32.Parse(distid);
            SqlCommand c = new SqlCommand("Select * from Thanas where DistrictId=" + i + "", DbConnection.Connection);
            SqlDataAdapter ad = new SqlDataAdapter(c);
            DataTable d = new DataTable();
            ad.Fill(d);
            ddl.DataSource = d;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("...Select...", "0"));
        }

        protected void ddlpAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadthana(ddlpAddress.SelectedValue,ddlpThana);           
            
        }

        protected void ddlpThana_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadPostOffice(ddlpThana.SelectedValue, ddlpPostOffice);
        }
        private void loadPostOffice(string thanaid,DropDownList ddl)
        {
            int i = Int32.Parse(thanaid);
            SqlCommand c = new SqlCommand("Select * from Post_Office where ThanaId=" + i + "", DbConnection.Connection);
            SqlDataAdapter ad = new SqlDataAdapter(c);
            DataTable d = new DataTable();
            ad.Fill(d);
            ddl.DataSource = d;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("...Select...", "0"));
        }

        protected void ddlthana_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadPostOffice(ddlthana.SelectedValue, ddlPostOffice);
            
        }

        protected void txt_toDate_TextChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "tabControl();", true);
            try
            {

                DateTime dt = DateTime.Parse(commonTask.ddMMyyyyToyyyyMMdd( txtDateFromTo.Text));
                DateTime today = DateTime.Parse(commonTask.ddMMyyyyToyyyyMMdd( txt_toDate.Text));
                Calculator2(dt, today);
            }
            catch { }  
        }
        protected void txtDateFromTo_TextChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "tabControl();", true);
            try
            {

                DateTime dt = DateTime.Parse(commonTask.ddMMyyyyToyyyyMMdd(txtDateFromTo.Text));
                DateTime today = DateTime.Parse(commonTask.ddMMyyyyToyyyyMMdd(txt_toDate.Text));
                Calculator2(dt, today);
            }
            catch { }
        }
        private void Calculator2(DateTime dt, DateTime today)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "tabControl();", true);  
            int Years = new DateTime(today.Subtract(dt).Ticks).Year - 1;
            DateTime PastYearDate = dt.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == today)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= today)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = today.Subtract(PastYearDate.AddMonths(Months)).Days;          
            txtTotalDuration.Text = Years.ToString() + " Years" + " " + Months.ToString() + " Months" + " " + Days.ToString() + " Days";
           
        }

        protected void chkSameAddress_CheckedChanged(object sender, EventArgs e)
        {
            if(chkSameAddress.Checked)
            {
                txtE_ParmanentAddress.Text = txtE_PresentAddress.Text;
                ddlpAddress.SelectedValue = ddlDistrict.SelectedValue;
                loadthana(ddlpAddress.SelectedValue, ddlpThana);
                ddlpThana.SelectedValue = ddlthana.SelectedValue;
                loadPostOffice(ddlpThana.SelectedValue, ddlpPostOffice);
                ddlpPostOffice.SelectedValue = ddlPostOffice.SelectedValue;
            }
            else
            {
                txtE_ParmanentAddress.Text = "";
                ddlpAddress.SelectedValue = "0";
                ddlpThana.SelectedValue = "0";
                ddlpPostOffice.SelectedValue = "0";
            }
        }

        protected void btnAddOthersInfo_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "tabControl4();", true);

            if (btnAddOthersInfo.Text == "Update")
            {

                long i = Convert.ToInt64(Session["S_Others"]);

                DataTable dttup = new DataTable();
                dttup = ViewState["vsOthers"] as DataTable;

                foreach (DataRow p in dttup.Rows)
                {
                    string iii = (p["SL"].ToString());
                    if (iii == i.ToString())
                    {
                        p["OthersInfo"] = txtOthersInfo.Text;                        
                        gvOthersInfo.DataSource = ViewState["vsOthers"] as DataTable;
                        gvOthersInfo.DataBind();
                        btnAddOthersInfo.Text = "Add";
                        clearOthers();
                    }
                }
            }
            else
            {


                dtOthers = ViewState["vsOthers"] as DataTable;
                DataRow dr = dtOthers.NewRow();
                dr[1] = txtOthersInfo.Text;
                dtOthers.Rows.Add(dr);
                ViewState["vsOthers"] = dtOthers;
                gvOthersInfo.DataSource = ViewState["vsOthers"] as DataTable;
                gvOthersInfo.DataBind();
                clearOthers();
            }
        }

        protected void lnkDeleteOthers_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "tabControl4();", true);
            DataTable dt = ViewState["vsOthers"] as DataTable;
            LinkButton clickedbutton = (LinkButton)sender;
            GridViewRow row = (GridViewRow)clickedbutton.NamingContainer;
            int idx = row.RowIndex;

            string idd = gvOthersInfo.Rows[idx].Cells[0].Text;
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (dr["SL"].ToString() == idd.ToString())
                    dr.Delete();
            }
            dt.AcceptChanges();
            gvOthersInfo.DataSource = dt;
            gvOthersInfo.DataBind();
            clearOthers();
        }

        protected void lnkEditOthers_Click(object sender, EventArgs e)
        {
            btnAddOthersInfo.Text = "Update";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "tabControl4();", true);

            LinkButton clickedbutton = (LinkButton)sender; //get clicked button
            GridViewRow row = (GridViewRow)clickedbutton.NamingContainer; //get the row where the button clicked
            int idx = row.RowIndex;

            string idd = gvOthersInfo.Rows[idx].Cells[0].Text;

            Session["S_Others"] = idd;
            txtOthersInfo.Text = gvOthersInfo.Rows[idx].Cells[1].Text == "&nbsp;" ? "" : gvOthersInfo.Rows[idx].Cells[1].Text;
            
        }
    }
}