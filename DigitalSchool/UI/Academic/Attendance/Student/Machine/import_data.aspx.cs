using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using DS.BLL.Attendace;
using DS.PropertyEntities.Model.Attendance;
using DS.DAL.AdviitDAL;
using ComplexScriptingSystem;
using System.IO;
using DS.BLL.ManagedBatch;
using System.Data.SqlClient;
using DS.DAL;
using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedClass;

namespace DS.UI.Academics.Attendance.Student.Machine
{
    public partial class import_data : System.Web.UI.Page
    {
        DataTable dt;
        DataTable dtStudentInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
                if (!IsPostBack)
                {
                    Classes.commonTask.loadShift(ddlShift,"0");
                    Classes.commonTask.loadShift(ddlPartialShift,"0");
                    BatchEntry.GetDropdownlist(ddlFullImportBatch, true);
                    BatchEntry.GetDropdownlist(ddlPartialImportBatch, true);
                    ShiftEntry.GetDropDownList(ddlShiftList);
                    BatchEntry.GetDropdownlist(ddlBatch, true);
                    SheetInfoEntry.loadMonths(ddlMonths);

                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "import_data.aspx", btnImport, btnPartialImport)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                }            
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {           
            selectedDate = txtAttendanceDate.Text;
            lblMessage.InnerText = "";
            ViewState["__BatchId__"] = ddlFullImportBatch.SelectedItem.Value;
            ViewState["__ShiftId__"] = ddlShift.SelectedItem.Value;
            // load all student according to shift
            dtStudentInfo = new DataTable();
            string attDate = txtAttendanceDate.Text.Trim().Substring(6, 4) + "-" + txtAttendanceDate.Text.Trim().Substring(3, 2) + "-" + txtAttendanceDate.Text.Trim().Substring(0, 2);
            sqlDB.fillDataTable("select Distinct RollNo,StudentId,CONVERT(varchar(10),AdmissionDate,105) as AdmissionDate, ConfigId as  ShiftId,ClsGrpId,ClsSecId from v_CurrentStudentInfo where IsActive='True' And ConfigId='" + ViewState["__ShiftId__"].ToString() + "' AND BatchId=" + ddlFullImportBatch.SelectedItem.Value + "", dtStudentInfo);

            //transfer al student info for set weekend or others holydays
            sqlDB.fillDataTable("select Convert(varchar(11),OffDate,105) as OffDate,Purpose from OffdaySettings where OffDate='" + attDate + "' and ( ShiftID="+ddlShift.SelectedValue+" or ShiftID is null)", dt = new DataTable());

            if (dt.Rows.Count > 0)
            {
                setWeekend_Others_Holyday(attDate, dt.Rows[0]["Purpose"].ToString(),true);
                return;
            }

            //-----------Import File------------ 
            if (Session["__IsOnline__"].ToString().Equals("True") && fileupload.HasFile == true)
                ImportAttendance(attDate,true);           
            //----------------------------------
            // Intermediat media for transfer all attendance record
            //setDailyAttendanceInTransferMedia(attDate, true);

            // Method calling for set daily attendance as record
            setDailyAttendanceRecord(attDate, true);
        }
        private void ImportAttendance(string attDate,bool ForAllStudents) 
        {
            string filename = Path.GetFileName(fileupload.FileName);
            File.Delete(HttpContext.Current.Server.MapPath("~/AccessFile/") + filename);
            fileupload.SaveAs(HttpContext.Current.Server.MapPath("~/AccessFile/") + filename);
            OleDbConnection cont = new OleDbConnection();
            string getFilePaht = HttpContext.Current.Server.MapPath("//AccessFile//" + filename);
            string connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + getFilePaht + "";
            cont.ConnectionString = connection;
            cont.Open();
            OleDbDataAdapter da;
            string sqlcmd1="";
            string sqlcmd2="";
            if (ForAllStudents)            
            {
                sqlcmd1="select USERINFO.UserID,CHECKTIME,CHECKTYPE,USERINFO.VERIFYCODE,SENSORID,Memoinfo,WorkCode,sn,UserExtFmt,USERINFO.BADGENUMBER from CHECKINOUT inner join USERINFO  on CHECKINOUT.USERID=USERINFO.USERID where   Format(CHECKTIME,'yyyy-mm-dd')='" + attDate + "'";
                sqlcmd2 = "delete CHECKINOUTOnline";
            }
            else
            {
                sqlcmd1="select USERINFO.UserID,CHECKTIME,CHECKTYPE,USERINFO.VERIFYCODE,SENSORID,Memoinfo,WorkCode,sn,UserExtFmt,USERINFO.BADGENUMBER from CHECKINOUT inner join USERINFO  on CHECKINOUT.USERID=USERINFO.USERID where  Badgenumber='" + txtCardNo.Text.Trim() + "' and Format(CHECKTIME,'yyyy-mm-dd')='" + attDate + "'";
                sqlcmd2="delete CHECKINOUTOnline where  Badgenumber='" + txtCardNo.Text.Trim() + "'";
            }          
            da = new OleDbDataAdapter(sqlcmd1, cont);  // here selecteddate format =yyyyMMdd            
            DataTable dtPunch = new DataTable();
            da.Fill(dtPunch);
            cont.Close();
            SqlCommand cmd1;
            cmd1 = new SqlCommand(sqlcmd2, DbConnection.Connection);
            cmd1.ExecuteNonQuery();
            SqlCommand cmd;
            //----------------------------------------------- entered punch data into CHECKINOUTOnline table------------------------------------------------
            foreach (DataRow dr in dtPunch.Rows)
            {

                cmd = new SqlCommand("insert into CHECKINOUTOnline(UserID,CHECKTIME,CHECKTYPE,VERIFYCODE,SENSORID,Memoinfo,WorkCode,sn,UserExtFmt,BADGENUMBER) " +
                    " values " +
                    "(" + dr["UserID"].ToString() + ",'" + dr["CHECKTIME"].ToString() + "','" + dr["CHECKTYPE"].ToString() + "'," + dr["VERIFYCODE"].ToString()
                    + ",'" + dr["SENSORID"].ToString() + "','" + dr["Memoinfo"].ToString() + "','" + dr["WorkCode"].ToString() + "','" + dr["sn"].ToString() + "'," + dr["UserExtFmt"].ToString() + "," + dr["BADGENUMBER"].ToString() + ")", DbConnection.Connection);
                cmd.ExecuteNonQuery();

            }
        }


        string selectedDate;
        private void setDailyAttendanceInTransferMedia(string attDate, bool ForAllStudents)
        {
            try
            {
              

                DataTable dt = new DataTable();
                SqlDataAdapter da;
                if (Session["__IsOnline__"].ToString().Equals("True"))
                {
                    if (ForAllStudents) da = new SqlDataAdapter("select * from CHECKINOUTOnline where CONVERT(varchar(10),[CHECKTIME],126)='" + attDate + "' order by UserId", DbConnection.Connection);
                    else da = new SqlDataAdapter("select * from CHECKINOUTOnline where CONVERT(varchar(10),[CHECKTIME],126)='" + attDate + "' AND Badgenumber='" + txtCardNo.Text.Trim() + "' order by UserId", DbConnection.Connection);
                    da.Fill(dt);
                }
                else 
                {
                    if (ForAllStudents) da = new SqlDataAdapter("select * from v_CHECKINOUT where CONVERT(varchar(10),[CHECKTIME],126)='" + attDate + "' order by UserId", DbConnection.Connection);
                    else da = new SqlDataAdapter("select * from v_CHECKINOUT where CONVERT(varchar(10),[CHECKTIME],126)='" + attDate + "' AND Badgenumber='" + txtCardNo.Text.Trim() + "' order by UserId", DbConnection.Connection);
                    da.Fill(dt);
                }
               

                SQLOperation.forDelete("AttendanceTransferMedia", DbConnection.Connection);

                for (int s = 0; s < dt.Rows.Count; s++)
                {                   
                    string[] getColumns = { "Student_Emp_AdmNo", "AttDate", "Hur", "Min", "Sec" };
                    string[] getValues = { dt.Rows[s]["Badgenumber"].ToString(), convertDateTime.getCertainCulture(selectedDate).ToString(), Convert.ToDateTime(dt.Rows[s]["CHECKTIME"].ToString()).Hour.ToString(), Convert.ToDateTime(dt.Rows[s]["CHECKTIME"].ToString()).Minute.ToString(), Convert.ToDateTime(dt.Rows[s]["CHECKTIME"].ToString()).Second.ToString() };
                    SQLOperation.forSaveValue("AttendanceTransferMedia", getColumns, getValues, DbConnection.Connection);
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "warning->" + ex.Message;
            }
        }

        int i;
        int j = 0;
        private void setDailyAttendanceRecord(string attDate, bool ForAllStudents)
        {
            try
            {

                CheckFineSettings();
                // SQLOperation.forDeleteRecordByIdentifier("DailyAttendanceRecord", "AttDate", attDate, sqlDB.connection);
               SqlCommand  cmd;
               if (ForAllStudents) cmd = new SqlCommand("delete from DailyAttendanceRecord where CONVERT(varchar(10),[AttDate],126)='" + attDate + "' AND AttManual Is Null and ShiftId='" + ViewState["__ShiftId__"].ToString() + "' and BatchId='" + ViewState["__BatchId__"].ToString() + "'", DbConnection.Connection);
               else cmd = new SqlCommand("delete from DailyAttendanceRecord where CONVERT(varchar(10),[AttDate],126)='" + attDate + "' AND StudentId=" + dtStudentInfo.Rows[0]["StudentId"].ToString() + " AND AttManual Is Null", DbConnection.Connection);
                cmd.ExecuteNonQuery();

                // for get shift information
                DataTable dtShift = new DataTable();
                sqlDB.fillDataTable("select StartTime,CloseTime,LateTime,AbsentTime from ShiftConfiguration where ConfigId='" + ViewState["__ShiftId__"].ToString() + "'", dtShift);

                int CH = Convert.ToDateTime(dtShift.Rows[0]["StartTime"].ToString()).Hour;  // for get office start hour
                int CM = Convert.ToDateTime(dtShift.Rows[0]["StartTime"].ToString()).Minute;  // for get office start minute
                int CS = Convert.ToDateTime(dtShift.Rows[0]["StartTime"].ToString()).Second;  // for get office start second
                int CAM = Convert.ToInt32(dtShift.Rows[0]["LateTime"].ToString());  // for get acceptable late time
                int CAT = Convert.ToInt32(dtShift.Rows[0]["AbsentTime"].ToString());  // for get acceptable Absent time

                int COutH = Convert.ToDateTime(dtShift.Rows[0]["CloseTime"].ToString()).Hour;  // for get office start hour
                int COutM = Convert.ToDateTime(dtShift.Rows[0]["CloseTime"].ToString()).Minute;  // for get office start hour

                string DailyStartTimeALT_CloseTime = CH + ":" + CM + ":" + CS + ":" + CAM + ":" + COutH + ":" + COutM;

                for (i = 0; i < dtStudentInfo.Rows.Count; i++)
                {                    
                    if (!CompareAdmissionDateAndIndate(i)) // check Admission date and attendance date
                    {
                        if (!ForAllStudents) lblMessage.InnerText = "error->This student is not admitted in this date.";
                        return;
                    }
                    else i = j;
                     if (Session["__IsOnline__"].ToString().Equals("True"))
                         sqlDB.fillDataTable("select distinct Badgenumber,left(CONVERT(VARCHAR(5),CHECKTIME,108),2) as Hur,right(CONVERT(VARCHAR(5),CHECKTIME,108),2) as Min,right(CONVERT(VARCHAR(8),CHECKTIME,108),2) as Sec, CONVERT(varchar(8),CHECKTIME,108) as PunchTime from CHECKINOUTOnline where convert(varchar(11),CHECKTIME,121)='" + attDate + "' AND " +
                      "Badgenumber=convert(varchar(50),(select AdmissionNo from TBL_STD_Admission_INFO where StudentId=" + dtStudentInfo.Rows[i]["StudentId"].ToString() + ")) " +
                      "", dt = new DataTable());
                         else
                    sqlDB.fillDataTable("select distinct Badgenumber,left(CONVERT(VARCHAR(5),CHECKTIME,108),2) as Hur,right(CONVERT(VARCHAR(5),CHECKTIME,108),2) as Min,right(CONVERT(VARCHAR(8),CHECKTIME,108),2) as Sec, CONVERT(varchar(8),CHECKTIME,108) as PunchTime from v_CHECKINOUT where convert(varchar(11),CHECKTIME,121)='" + attDate + "' AND " +
                        "Badgenumber=convert(varchar(50),(select AdmissionNo from TBL_STD_Admission_INFO where StudentId=" + dtStudentInfo.Rows[i]["StudentId"].ToString() + ")) " +
                        "", dt = new DataTable());
                    if (dt.Rows.Count > 0)
                    {
                        string InHur = (dt.Rows[0]["Hur"].ToString().Trim().Length == 1) ? "0" + dt.Rows[0]["Hur"].ToString().Trim() : dt.Rows[0]["Hur"].ToString().Trim();
                        string InMin = (dt.Rows[0]["Min"].ToString().Trim().Length == 1) ? "0" + dt.Rows[0]["Min"].ToString().Trim() : dt.Rows[0]["Min"].ToString().Trim();
                        string InSec = (dt.Rows[0]["Sec"].ToString().Trim().Length == 1) ? "0" + dt.Rows[0]["Sec"].ToString().Trim() : dt.Rows[0]["Sec"].ToString().Trim();

                        string OutHur = (dt.Rows[dt.Rows.Count - 1]["Hur"].ToString().Trim().Length == 1) ? "0" + dt.Rows[dt.Rows.Count - 1]["Hur"].ToString().Trim() : dt.Rows[dt.Rows.Count - 1]["Hur"].ToString().Trim();
                        string OutMin = (dt.Rows[dt.Rows.Count - 1]["Min"].ToString().Trim().Length == 1) ? "0" + dt.Rows[dt.Rows.Count - 1]["Min"].ToString().Trim() : dt.Rows[dt.Rows.Count - 1]["Min"].ToString().Trim();
                        string OutSec = (dt.Rows[dt.Rows.Count - 1]["Sec"].ToString().Trim().Length == 1) ? "0" + dt.Rows[dt.Rows.Count - 1]["Sec"].ToString().Trim() : dt.Rows[dt.Rows.Count - 1]["Sec"].ToString().Trim();

                        if (InHur == OutHur)
                        {
                            OutHur = "00"; OutMin = "00"; OutSec = "00";
                        }

                        bool isPresent = false;  // for temporary solve

                        if (Convert.ToInt16(dt.Rows[0]["Hur"].ToString()) <= CH)
                        {
                            isPresent = true;
                            if (int.Parse(dt.Rows[0]["Hur"].ToString()) <= CH - 1) isPresent = true;
                            else if (int.Parse(dt.Rows[0]["Min"].ToString()) < CM) isPresent = true;  // now compare acceptable minutes

                            else if (int.Parse(dt.Rows[0]["Min"].ToString()) < CM + CAM) isPresent = true;
                            else if (int.Parse(dt.Rows[0]["Min"].ToString()) > CM + CAM) isPresent = false;

                            else if (int.Parse(dt.Rows[0]["Min"].ToString()) == CM + CAM)
                            {
                                if (int.Parse(dt.Rows[0]["Sec"].ToString()) <= CS) isPresent = true;
                                else isPresent = false;
                            }                          
                        }
                        char attStatus = (isPresent) ? 'p' : 'l';
                        if (Convert.ToInt16(dt.Rows[0]["Hur"].ToString()) >= CH)
                        {
                            var latetime = (TimeSpan.Parse(InHur + ":" + InMin + ":" + InSec) - TimeSpan.Parse(CH.ToString() + ":" + CM.ToString() + ":" + CS.ToString())).TotalMinutes;
                            if (CAT < latetime)
                                attStatus = 'a';
                        }

                        
                        string[] getColumns = { "StudentId", "RollNo", "BatchId", "ShiftId", "ClsSecId", "ClsGrpId", "AttDate", "InHur", "InMin", "InSec", "OutHur", "OutMin", "OutSec", "AttStatus", "StateStatus", "DailyStartTimeALT_CloseTime" };
                        string[] getValues = { dtStudentInfo.Rows[i]["StudentId"].ToString(), dtStudentInfo.Rows[i]["RollNo"].ToString(),ViewState["__BatchId__"].ToString(), ViewState["__ShiftId__"].ToString(), dtStudentInfo.Rows[i]["ClsSecId"].ToString(), dtStudentInfo.Rows[i]["ClsGrpId"].ToString(), 
                                                  convertDateTime.getCertainCulture(selectedDate).ToString(), InHur, InMin, InSec, OutHur, OutMin, OutSec, attStatus.ToString(), "Present", DailyStartTimeALT_CloseTime };
                        SQLOperation.forSaveValue("DailyAttendanceRecord", getColumns, getValues, DbConnection.Connection);
                    }
                    else
                    {
                        string[] getColumns = { "StudentId", "RollNo", "BatchId", "ShiftId", "ClsSecId", "ClsGrpId", "AttDate", "InHur", "InMin", "InSec", "OutHur", "OutMin", "OutSec", "AttStatus", "StateStatus", "DailyStartTimeALT_CloseTime" };
                        string[] getValues = { dtStudentInfo.Rows[i]["StudentId"].ToString(), dtStudentInfo.Rows[i]["RollNo"].ToString(),ViewState["__BatchId__"].ToString(), ViewState["__ShiftId__"].ToString(), dtStudentInfo.Rows[i]["ClsSecId"].ToString(), dtStudentInfo.Rows[i]["ClsGrpId"].ToString(), 
                                              convertDateTime.getCertainCulture(selectedDate).ToString(), "00", "00", "00", "00", "00", "00", "a", "Absent", "00:00:00:00:00:00" };
                        SQLOperation.forSaveValue("DailyAttendanceRecord", getColumns, getValues, DbConnection.Connection);

                        if (ViewState["__IsActive__"].Equals("True"))
                        {
                            StudentAbsentDetailsEntry.Delete(attDate,dtStudentInfo.Rows[i]["StudentId"].ToString());
                            StudentAbsentDetailsEntry.Insert(ViewState["__BatchId__"].ToString(), dtStudentInfo.Rows[i]["StudentId"].ToString(), attDate, ViewState["__AbsentFineAmount__"].ToString(), "0");
                        }
                    }
                }
                if (dtStudentInfo.Rows.Count > 0)
                {
                    lblMessage.InnerText = "success->Successfully attendance counted";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "ClearInputBox();", true);
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = ex.Message;
            }
        }



        private void setWeekend_Others_Holyday(string attDate, string purpose, bool ForAllStudents)
        {
            try
            {
               // SQLOperation.forDeleteRecordByIdentifier("DailyAttendanceRecord", "AttDate", attDate, DbConnection.Connection);
                SqlCommand cmd;
                if (ForAllStudents) cmd = new SqlCommand("delete from DailyAttendanceRecord where CONVERT(varchar(10),[AttDate],126)='" + attDate + "' AND AttManual Is Null and ShiftId='" + ViewState["__ShiftId__"].ToString() + "' and BatchId='" + ViewState["__BatchId__"].ToString() + "'", DbConnection.Connection);
                else cmd = new SqlCommand("delete from DailyAttendanceRecord where CONVERT(varchar(10),[AttDate],126)='" + attDate + "' AND StudentId=" + dtStudentInfo.Rows[0]["StudentId"].ToString() + " AND AttManual Is Null", DbConnection.Connection);
                cmd.ExecuteNonQuery();
                string attStatus = (purpose.Trim().Equals("Weekly Holiday")) ? "w" : "h";
                for (i = 0; i < dtStudentInfo.Rows.Count; i++)
                {
                    if (!CompareAdmissionDateAndIndate(i)) return; // // check Admission date and attendance date
                    string[] getColumns = { "StudentId", "RollNo", "BatchId", "ShiftId", "ClsSecId", "ClsGrpId", "AttDate", "InHur", "InMin", "InSec", "OutHur", "OutMin", "OutSec", "AttStatus", "StateStatus", "DailyStartTimeALT_CloseTime" };
                    string[] getValues = { dtStudentInfo.Rows[i]["StudentId"].ToString(),dtStudentInfo.Rows[i]["RollNo"].ToString(),ViewState["__BatchId__"].ToString(), ViewState["__ShiftId__"].ToString(), dtStudentInfo.Rows[i]["ClsSecId"].ToString(), dtStudentInfo.Rows[i]["ClsGrpId"].ToString(),
                                          convertDateTime.getCertainCulture(txtAttendanceDate.Text.Trim()).ToString(), "00", "00", "00", "00", "00", "00", attStatus, purpose, "00:00:00:00:00:00" };
                    SQLOperation.forSaveValue("DailyAttendanceRecord", getColumns, getValues, DbConnection.Connection);

                }
                lblMessage.InnerText = "success->Successfully attendance counted";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "ClearInputBox();", true);
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "warning->" + ex.Message;
            }
        }

        bool Datestatus = false;
        private bool CompareAdmissionDateAndIndate(int i)
        {
            try
            {
                Datestatus = false;
                j = i;

                DateTime InDate = new DateTime(int.Parse(selectedDate.Substring(6, 4)), int.Parse(selectedDate.Substring(3, 2)), int.Parse(selectedDate.Substring(0, 2)));
                DateTime AdmissionDate = new DateTime(int.Parse(dtStudentInfo.Rows[i]["AdmissionDate"].ToString().Substring(6, 4)), int.Parse(dtStudentInfo.Rows[i]["AdmissionDate"].ToString().Substring(3, 2)), int.Parse(dtStudentInfo.Rows[i]["AdmissionDate"].ToString().Substring(0, 2)));

                if (InDate >= AdmissionDate)
                {
                    Datestatus = true;
                    return true;

                }
                else
                {
                    i++;
                    if (i < dtStudentInfo.Rows.Count)
                    {

                        CompareAdmissionDateAndIndate(i);
                    }
                    if (Datestatus) return true;
                    else return false;
                }
            }
            catch { return false; }
        }

        protected void btnPartialImport_Click(object sender, EventArgs e)
        {
           
            selectedDate = txtPartialAttDate.Text;
            lblMessage.InnerText = "";
            ViewState["__ShiftId__"] = ddlPartialShift.SelectedItem.Value;
            ViewState["__BatchId__"] = ddlPartialImportBatch.SelectedItem.Value;

            // check or load all current student-------------------------------------
            dtStudentInfo = new DataTable();
            string attDate = txtPartialAttDate.Text.Trim().Substring(6, 4) + "-" + txtPartialAttDate.Text.Trim().Substring(3, 2) + "-" + txtPartialAttDate.Text.Trim().Substring(0, 2);
            sqlDB.fillDataTable("select Distinct RollNo,StudentId,CONVERT(varchar(10),AdmissionDate,105) as AdmissionDate,ConfigId as  ShiftId,ClsGrpId,ClsSecId from v_CurrentStudentInfo where AdmissionNo=" + txtCardNo.Text.Trim() + " AND IsActive='True' AND BatchId= " + ddlPartialImportBatch.SelectedItem.Value + " AND ConfigId=" + ddlPartialShift.SelectedItem.Value + "", dtStudentInfo);
            //-----------------------------------------------------------------------

            if (dtStudentInfo.Rows.Count > 0)
            {
                //transfer al student info for set weekend or others holydays
                sqlDB.fillDataTable("select Convert(varchar(11),OffDate,105) as OffDate,Purpose from OffdaySettings where OffDate='" + attDate + "' and ( ShiftID="+ddlPartialShift.SelectedValue+" or ShiftID is null)", dt = new DataTable());

                if (dt.Rows.Count > 0)
                {
                    setWeekend_Others_Holyday(attDate, dt.Rows[0]["Purpose"].ToString(),false);
                    return;
                }
                //-----------Import File------------ 
                if (Session["__IsOnline__"].ToString().Equals("True") && fileupload.HasFile == true)
                    ImportAttendance(attDate, false);
                //----------------------------------

                // Intermediat media for transfer all attendance record
              //  setDailyAttendanceInTransferMedia(attDate, false);

                // Method calling for set daily attendance as record
                setDailyAttendanceRecord(attDate, false);
            }
            else
            {
                lblMessage.InnerText = "error->Any student is not exists in this shift of the batch";
            }
        }

        private void CheckFineSettings()
        {
            
            sqlDB.fillDataTable("select AbsentFineAmount,IsActive from AbsentFine where IsActive='True'",dt=new DataTable ());
            ViewState["__IsActive__"] = (dt.Rows.Count > 0) ? dt.Rows[0]["IsActive"].ToString() : "False";
            ViewState["__AbsentFineAmount__"] = (dt.Rows.Count > 0) ? dt.Rows[0]["AbsentFineAmount"].ToString() : "0";
        }
       
        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddlSection.Items.Clear();
            BatchEntry.loadGroupByBatchId(ddlgroup, ddlBatch.SelectedValue.ToString());

            if (ddlgroup.Items.Count == 1)
            {
               divGroup.Visible = false;
                // ClassSectionEntry.GetSectionListByBatchId(ddlSection, ddlBatch.SelectedValue.ToString());
                ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection, ddlBatch.SelectedValue.ToString(), ddlgroup.SelectedItem.Value);
            }
            else
            {
                divGroup.Visible = true;
            }
        }

        protected void btnTodayAttendanceList_Click(object sender, EventArgs e)
        {
            LoadDailyAttendanceReportData("attendance");
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {           
           GenerateAttendanceSheet();
        }

        protected void btnTodayPresentList_Click(object sender, EventArgs e)
        {
            LoadDailyAttendanceReportData("present");
        }

        protected void btnTodayAbsentList_Click(object sender, EventArgs e)
        {
            LoadDailyAttendanceReportData("absent");
        }

        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection, ddlBatch.SelectedValue.ToString(), ddlgroup.SelectedItem.Value);
        }
        private void GenerateAttendanceSheet()
        {


            //--------------------------------------------------
            DataTable dt = new DataTable();
            string SqlCmd = "SELECT   FullName, RollNo, SUM(CASE DATEPART(day, AttDate) WHEN 1 THEN code ELSE 0 END) AS [1], SUM(CASE DATEPART(day, AttDate) " +
                        " WHEN 2 THEN code ELSE 0 END) AS [2], SUM(CASE DATEPART(day, AttDate) WHEN 3 THEN code ELSE 0 END) AS [3], SUM(CASE DATEPART(day, AttDate) WHEN 4 THEN code ELSE 0 END) AS [4], " +
                        " SUM(CASE DATEPART(day, AttDate) WHEN 5 THEN code ELSE 0 END) AS [5], SUM(CASE DATEPART(day, AttDate) WHEN 6 THEN code ELSE 0 END) AS [6], SUM(CASE DATEPART(day, AttDate) " +
                        " WHEN 7 THEN code ELSE 0 END) AS [7], SUM(CASE DATEPART(day, AttDate) WHEN 8 THEN code ELSE 0 END) AS [8], SUM(CASE DATEPART(day, AttDate) WHEN 9 THEN code ELSE 0 END) AS [9], " +
                        " SUM(CASE DATEPART(day, AttDate) WHEN 10 THEN code ELSE 0 END) AS [10], SUM(CASE DATEPART(day, AttDate) WHEN 11 THEN code ELSE 0 END) AS [11], SUM(CASE DATEPART(day, AttDate) " +
                        " WHEN 12 THEN code ELSE 0 END) AS [12], SUM(CASE DATEPART(day, AttDate) WHEN 13 THEN code ELSE 0 END) AS [13], SUM(CASE DATEPART(day, AttDate) WHEN 14 THEN code ELSE 0 END) AS [14], " +
                        " SUM(CASE DATEPART(day, AttDate) WHEN 15 THEN code ELSE 0 END) AS [15], SUM(CASE DATEPART(day, AttDate) WHEN 16 THEN code ELSE 0 END) AS [16], SUM(CASE DATEPART(day, AttDate) " +
                        " WHEN 17 THEN code ELSE 0 END) AS [17], SUM(CASE DATEPART(day, AttDate) WHEN 18 THEN code ELSE 0 END) AS [18], SUM(CASE DATEPART(day, AttDate) WHEN 19 THEN code ELSE 0 END) AS [19], " +
                        " SUM(CASE DATEPART(day, AttDate) WHEN 20 THEN code ELSE 0 END) AS [20], SUM(CASE DATEPART(day, AttDate) WHEN 21 THEN code ELSE 0 END) AS [21], SUM(CASE DATEPART(day, AttDate) " +
                        " WHEN 22 THEN code ELSE 0 END) AS [22], SUM(CASE DATEPART(day, AttDate) WHEN 23 THEN code ELSE 0 END) AS [23], SUM(CASE DATEPART(day, AttDate) WHEN 24 THEN code ELSE 0 END) AS [24], " +
                        " SUM(CASE DATEPART(day, AttDate) WHEN 25 THEN code ELSE 0 END) AS [25], SUM(CASE DATEPART(day, AttDate) WHEN 26 THEN code ELSE 0 END) AS [26], SUM(CASE DATEPART(day, AttDate) " +
                        " WHEN 27 THEN code ELSE 0 END) AS [27], SUM(CASE DATEPART(day, AttDate) WHEN 28 THEN code ELSE 0 END) AS [28], SUM(CASE DATEPART(day, AttDate) WHEN 29 THEN code ELSE 0 END) AS [29], " +
                        " SUM(CASE DATEPART(day, AttDate) WHEN 30 THEN code ELSE 0 END) AS [30], SUM(CASE DATEPART(day, AttDate) WHEN 31 THEN code ELSE 0 END) AS [31], SUM(CASE Code WHEN 112 THEN 1 ELSE 0 END) " +
                        " AS P, SUM(CASE Code WHEN 97 THEN 1 ELSE 0 END) AS A, SUM(CASE Code WHEN 104 THEN 1 ELSE 0 END) AS H, SUM(CASE Code WHEN 119 THEN 1 ELSE 0 END) AS W, " +
                        " SUM(CASE Code WHEN 226 THEN 1 ELSE 0 END) AS LV " +
                        " FROM            dbo.v_DailyAttendanceRecordForReport" +
                        " where ShiftId='" + ddlShiftList.SelectedValue + "' and BatchId='" + ddlBatch.SelectedValue + "' and " +
                        " ClsGrpId='" + ddlgroup.SelectedValue + "' and ClsSecId='" + ddlSection.SelectedValue + "' and Format(attdate,'MM-yyyy')='" + ddlMonths.SelectedValue + "'" +
                        " GROUP BY StudentId, FullName, RollNo";
            sqlDB.fillDataTable(SqlCmd, dt);
            if (dt.Rows.Count < 1)
            {
                lblMessage.InnerText = "warning-> Any attendance Record are not founded!";
                return;
            }
            string Group = (ddlgroup.SelectedValue == "0") ? "" : "Group : " + ddlgroup.SelectedItem.Text + ",";
            Session["__AttendanceSheet__"] = dt;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=AttendanceSheet-" + ddlShiftList.SelectedItem.Text + "-" + ddlBatch.SelectedItem.Text + "-" + Group + "-" + ddlSection.SelectedItem.Text + "-" + ddlMonths.SelectedItem.Text + "');", true);
            //Open New Tab for Sever side code  
        }
        private void LoadDailyAttendanceReportData(string reportType)
        {
            //-----------Validation--------------
            if (ddlShiftList.SelectedValue == "0")
            { lblMessage.InnerText = "warning-> Please select a Shift!"; return; }
            if (ddlBatch.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Batch!"; return; }
            if (divGroup.Visible == true && ddlgroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group!"; return; }
            if (ddlSection.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section!"; return; }
            //--------------------------------------------------
            string sqlCmd = "";
            string ReportTitel = "", ReportType = "";
            DataTable dt = new DataTable();
            if (reportType == "attendance") // Daily Attendance Status
            {
                sqlCmd = "select FullName,RollNo,AttStatus,StateStatus,inHur,InMin,OutHur,OutMin from v_DailyAttendanceRecordForReport where ShiftId='" + ddlShiftList.SelectedValue + "'and BatchId='" + ddlBatch.SelectedValue + "' and ClsGrpId='" + ddlgroup.SelectedValue + "' and ClsSecId='" + ddlSection.SelectedValue + "' and FORMAT(Attdate,'dd-MM-yyyy')='" + DateTime.Now.ToString("dd-MM-yyyy") + "'";
                ReportTitel = "Daily Attendance Status";
                ReportType = "Status";
            }
            else if (reportType == "present") // Daily Present status
            {
                sqlCmd = "select FullName,RollNo,AttStatus,StateStatus,inHur,InMin,OutHur,OutMin from v_DailyAttendanceRecordForReport where AttStatus='p' or AttStatus='l' and  ShiftId='" + ddlShiftList.SelectedValue + "'and BatchId='" + ddlBatch.SelectedValue + "' and ClsGrpId='" + ddlgroup.SelectedValue + "' and ClsSecId='" + ddlSection.SelectedValue + "' and FORMAT(Attdate,'dd-MM-yyyy')='" + DateTime.Now.ToString("dd-MM-yyyy") + "'";
                ReportTitel = "Daily Present Status";
                ReportType = "PresentAbsent";
            }
            else if (reportType == "absent") // Daily Absent Staust
            {
                sqlCmd = "select FullName,RollNo,AttStatus,StateStatus,inHur,InMin,OutHur,OutMin from v_DailyAttendanceRecordForReport where AttStatus='a' and  ShiftId='" + ddlShiftList.SelectedValue + "'and BatchId='" + ddlBatch.SelectedValue + "' and ClsGrpId='" + ddlgroup.SelectedValue + "' and ClsSecId='" + ddlSection.SelectedValue + "' and FORMAT(Attdate,'dd-MM-yyyy')='" + DateTime.Now.ToString("dd-MM-yyyy") + "'";
                ReportTitel = "Daily Absent Status";
                ReportType = "PresentAbsent";
            }

            //sqlDB.fillDataTable(sqlCmd, dt = new DataTable());
            dt = CRUD.ReturnTableNull(sqlCmd);
            if (dt.Rows.Count < 1)
            {
                lblMessage.InnerText = "warning->Any " + reportType + " record are not founded";
                return;
            }
            Session["__DailyAttendance__"] = dt;
            ViewState["__ReportTitle__"] = ReportTitel;
            ViewState["__ReportType__"] = ReportType;
            ViewState["__Report__"] = "";
            string GroupName = (ddlgroup.SelectedValue == "0") ? "" : " Group : " + ddlgroup.SelectedItem.Text + ", ";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DailyAttendance-" + ddlShiftList.SelectedItem.Text + "-" + ddlBatch.SelectedItem.Text + "-" + GroupName + "-" + ddlSection.SelectedItem.Text + "-" + DateTime.Now.ToString("dd/MM/yyyy") + "-" + ViewState["__ReportTitle__"].ToString() + "-" + ViewState["__ReportType__"].ToString() + "');", true);
        }

    }
}