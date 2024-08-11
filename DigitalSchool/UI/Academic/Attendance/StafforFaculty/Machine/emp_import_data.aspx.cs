using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL;
using DS.DAL.AdviitDAL;
using System.Data.OleDb;
using DS.DAL.ComplexScripting;
using System.IO;
using System.Data.SqlClient;
using DS.BLL.ControlPanel;
using DS.BLL.Attendace;
using DS.BLL.GeneralSettings;

namespace DS.UI.Academics.Attendance.StafforFaculty.Machine
{
    public partial class emp_import_data : System.Web.UI.Page
    {
        DataTable dt;
        DataTable dtEmpInfo;
        string sqlCmd = "";
        string ReportTitel = "";
        string ReportType = "";
        string DepartmentList = "";
        string DesignationList = "";
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    Classes.commonTask.loadShift(ddlShift,"1");
                    SheetInfoEntry.loadMonths(dlSheetName);
                    ShiftEntry.GetDropDownList(ddlShiftList);
                    Classes.commonTask.LoadDeprtmentAtttedence(dlDepartment);
                    Classes.commonTask.LoadDesignation(dlDesignation);

                   // if (PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "emp_import_data.aspx", btnImport, btnPartialImport)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                }
        }

        private bool PratialValidationBag()
        {
            try
            {
                //if (!FileUpload1.HasFile)
                //{
                //    lblMessage.InnerText = "error->Please select an access file";
                //    FileUpload1.Focus(); return false;
                //}
                if (ddlShift.SelectedItem.Value == "0")
                {
                    lblMessage.InnerText = "error->Please select a shift";
                    ddlShift.Focus(); return false;
                }
                return true;

            }
            catch { return false; }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (!PratialValidationBag()) return;
            selectedDate = txtAttendanceDate.Text;
            lblMessage.InnerText = "";
            // load all student according to shift
            dtEmpInfo = new DataTable();
            string attDate = txtAttendanceDate.Text.Trim().Substring(6, 4) + "-" + txtAttendanceDate.Text.Trim().Substring(3, 2) + "-" + txtAttendanceDate.Text.Trim().Substring(0, 2);
            sqlDB.fillDataTable("select Distinct EID,ECardNo,Format(EJoiningDate,'dd-MM-yyyy') as EJoiningDate,DId,DesId,ShiftId,VP from EmployeeInfo where IsActive='True' AND ShiftId=" + ddlShift.SelectedItem.Value.ToString() + "", dtEmpInfo);

            //transfer all Employee info for set weekend or others holydays
            sqlDB.fillDataTable("select Convert(varchar(11),OffDate,105) as OffDate,Purpose from OffdaySettings where OffDate='" + attDate + "'", dt = new DataTable());

            if (dt.Rows.Count > 0)
            {
                setWeekend_Others_Holyday(attDate, dt.Rows[0]["Purpose"].ToString());
                return;
            }
            //-----------Import File------------ 
            if (Session["__IsOnline__"].ToString().Equals("True") && fileupload.HasFile == true)
                ImportAttendance(attDate, true);
            //----------------------------------
            // Intermediat media for transfer all attendance record
            //setDailyAttendanceInTransferMedia(attDate, true);

            // Method calling for set daily attendance as record
            setDailyAttendanceRecord(attDate, true);
        }
        private void ImportAttendance(string attDate, bool ForAllStudents)
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
            string sqlcmd1 = "";
            string sqlcmd2 = "";
            if (ForAllStudents)
            {
                sqlcmd1 = "select USERINFO.UserID,Format(CheckTime,'yyyy-MM-dd HH:mm:ss') as CHECKTIME,CHECKTYPE,USERINFO.VERIFYCODE,SENSORID,Memoinfo,WorkCode,sn,UserExtFmt,USERINFO.BADGENUMBER from CHECKINOUT inner join USERINFO  on CHECKINOUT.USERID=USERINFO.USERID where   Format(CHECKTIME,'yyyy-mm-dd')='" + attDate + "'";
                sqlcmd2 = "delete CHECKINOUTOnline";
            }
            else
            {
                sqlcmd1 = "select USERINFO.UserID,Format(CheckTime,'yyyy-MM-dd HH:mm:ss') as CHECKTIME,CHECKTYPE,USERINFO.VERIFYCODE,SENSORID,Memoinfo,WorkCode,sn,UserExtFmt,USERINFO.BADGENUMBER from CHECKINOUT inner join USERINFO  on CHECKINOUT.USERID=USERINFO.USERID where  Badgenumber='" + txtCardNo.Text.Trim() + "' and Format(CHECKTIME,'yyyy-mm-dd')='" + attDate + "'";
                sqlcmd2 = "delete CHECKINOUTOnline where  Badgenumber='" + txtCardNo.Text.Trim() + "'";
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
        private void setDailyAttendanceInTransferMedia(string attDate, bool ForAllEmployees)
        {
            try
            {
                string filename = "";
                if (FileUpload1.HasFile)
                {
                    // file saved in Server path Access file 
                    filename = Path.GetFileName(FileUpload1.FileName);
                    File.Delete(Server.MapPath("~/AccessFile/") + filename);
                    FileUpload1.SaveAs(Server.MapPath("~/AccessFile/") + filename);
                }
                else
                {
                    lblMessage.InnerText = "error->Please Select Your Access File !"; return;
                }

                string getFilePaht = Server.MapPath("//AccessFile//" + filename);
                string connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + getFilePaht + "";

             //   string connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Program Files\\ZKTime5.0\\att2000.mdb";
                OleDbConnection cont = new OleDbConnection(connection);
                cont.Open();

                if (ForAllEmployees) setEmpIdAndCardNo(cont);    // for save EId and EmpCardNo accroding to employee or student for temporary 

                DataTable dt = new DataTable();
                OleDbDataAdapter da;
                if (ForAllEmployees) da = new OleDbDataAdapter("select * from v_CHECKINOUT where Format([CHECKTIME],'yyyy-mm-dd')='" + attDate + "' AND  Badgenumber in (select EmpCardNo from TempEmpCardNo  ) order by UserId", cont);
                else da = new OleDbDataAdapter("select * from v_CHECKINOUT where Format([CHECKTIME],'yyyy-mm-dd')='" + attDate + "' AND Badgenumber='" + txtCardNo.Text.Trim() + "' order by UserId", cont);
                da.Fill(dt);
                cont.Close();
                SQLOperation.forDelete("AttendanceTransferMedia", sqlDB.connection);
                string[] d = selectedDate.Split('-');
                string sDate = d[2] + "-" + d[1] + "-" + d[0];
                for (int s = 0; s < dt.Rows.Count; s++)
                {
                    string a = Convert.ToDateTime(dt.Rows[s]["CHECKTIME"].ToString()).Hour.ToString();
                    string[] getColumns = { "Student_Emp_AdmNo", "AttDate", "Hur", "Min", "Sec" };
                    string[] getValues = { dt.Rows[s]["Badgenumber"].ToString(), sDate, Convert.ToDateTime(dt.Rows[s]["CHECKTIME"].ToString()).Hour.ToString(), Convert.ToDateTime(dt.Rows[s]["CHECKTIME"].ToString()).Minute.ToString(), Convert.ToDateTime(dt.Rows[s]["CHECKTIME"].ToString()).Second.ToString() };
                    SQLOperation.forSaveValue("AttendanceTransferMedia", getColumns, getValues, sqlDB.connection);

                }

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "warning->" + ex.Message;
            }
        }

        private void setEmpIdAndCardNo(OleDbConnection cont)
        {
            try
            {
                OleDbCommand cmd;
                cmd = new OleDbCommand("delete from TempEmpCardNo", cont);
                cmd.ExecuteNonQuery();
                for (int i = 0; i < dtEmpInfo.Rows.Count; i++)
                {
                    cmd = new OleDbCommand("insert into TempEmpCardNo (EmpId,EmpCardNo) values (" + dtEmpInfo.Rows[i]["EId"].ToString() + "," + dtEmpInfo.Rows[i]["ECardNo"].ToString() + ")", cont);
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }

     //   private bool HasManual

        int i;
        int j = 0;
        private void setDailyAttendanceRecord(string attDate, bool ForAllEmployees)
        {
            try
            {
                DataTable dtShift = new DataTable();
                if(ForAllEmployees)
                {
                    SqlCommand cmd1 = new SqlCommand("delete from DailyAttendanceRecord where Format(AttDate,'yyyy-MM-dd')='" + attDate + "' AND (AttManual Is Null or AttManual='') and ShiftId='" + ddlShift.SelectedItem.Value + "'", DbConnection.Connection);
                    cmd1.ExecuteNonQuery();
                    // for get shift information                    
                    sqlDB.fillDataTable("select StartTime,CloseTime,LateTime,AbsentTime from ShiftConfiguration where ConfigId=" + ddlShift.SelectedItem.Value + "", dtShift);
                }
                else
                {
                    SqlCommand cmd1 = new SqlCommand("delete from DailyAttendanceRecord where Format(AttDate,'yyyy-MM-dd')='" + attDate + "' AND (AttManual Is Null or AttManual='') and EId='" + dtEmpInfo.Rows[0]["EId"].ToString() + "'", DbConnection.Connection);
                    cmd1.ExecuteNonQuery();
                    // for get shift information                    
                    sqlDB.fillDataTable("select StartTime,CloseTime,LateTime,AbsentTime from ShiftConfiguration where ConfigId=" + dtEmpInfo.Rows[0]["ShiftId"].ToString() + "", dtShift);
                }
                

                int CH = Convert.ToDateTime(dtShift.Rows[0]["StartTime"].ToString()).Hour;  // for get office start hour
                int CM = Convert.ToDateTime(dtShift.Rows[0]["StartTime"].ToString()).Minute;  // for get office start minute
                int CS = Convert.ToDateTime(dtShift.Rows[0]["StartTime"].ToString()).Second;  // for get office start second
                int CAM = Convert.ToInt32(dtShift.Rows[0]["LateTime"].ToString());  // for get acceptable late time
                int CAT = Convert.ToInt32(dtShift.Rows[0]["AbsentTime"].ToString());  // for get acceptable Absent time

                int COutH = Convert.ToDateTime(dtShift.Rows[0]["CloseTime"].ToString()).Hour;  // for get office start hour
                int COutM = Convert.ToDateTime(dtShift.Rows[0]["CloseTime"].ToString()).Minute;  // for get office start hour

                string DailyStartTimeALT_CloseTime = CH + ":" + CM + ":" + CS + ":" + CAM + ":" + COutH + ":" + COutM;

                for (i = 0; i < dtEmpInfo.Rows.Count; i++)
                {
                     
                   
                    if (!CompareAdmissionDateAndIndate(i)) // check Admission date and attendance date
                    {
                        // if are partial type
                        if (!ForAllEmployees)
                        {
                            lblMessage.InnerText = "error->This teacher or Staff is not joined in this date.";
                            return;
                        }
                    }
                    else i = j;
                      if (Session["__IsOnline__"].ToString().Equals("True"))
                    sqlDB.fillDataTable("select distinct Badgenumber,left(CONVERT(VARCHAR(5),CHECKTIME,108),2) as Hur,right(CONVERT(VARCHAR(5),CHECKTIME,108),2) as Min,right(CONVERT(VARCHAR(8),CHECKTIME,108),2) as Sec, CONVERT(varchar(8),CHECKTIME,108) as PunchTime from CHECKINOUTOnline where convert(varchar(11),CHECKTIME,121)='" + attDate + "' AND Badgenumber=convert(varchar(50),(select ECardNo from EmployeeInfo where EId=" + dtEmpInfo.Rows[i]["EId"].ToString() + "))", dt = new DataTable());
                    else
                    sqlDB.fillDataTable("select distinct Badgenumber,left(CONVERT(VARCHAR(5),CHECKTIME,108),2) as Hur,right(CONVERT(VARCHAR(5),CHECKTIME,108),2) as Min,right(CONVERT(VARCHAR(8),CHECKTIME,108),2) as Sec, CONVERT(varchar(8),CHECKTIME,108) as PunchTime from v_CHECKINOUT where convert(varchar(11),CHECKTIME,121)='" + attDate + "' AND Badgenumber=convert(varchar(50),(select ECardNo from EmployeeInfo where EId=" + dtEmpInfo.Rows[i]["EId"].ToString() + "))", dt = new DataTable());

                    string[] d = selectedDate.Split('-');
                    string sDate = d[2] + "-" + d[1] + "-" + d[0];

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows.Count > 1)
                        {

                        }
                        string InHur = (dt.Rows[0]["Hur"].ToString().Trim().Length == 1) ? "0" + dt.Rows[0]["Hur"].ToString().Trim() : dt.Rows[0]["Hur"].ToString().Trim();
                        string InMin = (dt.Rows[0]["Min"].ToString().Trim().Length == 1) ? "0" + dt.Rows[0]["Min"].ToString().Trim() : dt.Rows[0]["Min"].ToString().Trim();
                        string InSec = (dt.Rows[0]["Sec"].ToString().Trim().Length == 1) ? "0" + dt.Rows[0]["Sec"].ToString().Trim() : dt.Rows[0]["Sec"].ToString().Trim();

                        string OutHur = (dt.Rows[dt.Rows.Count - 1]["Hur"].ToString().Trim().Length == 1) ? "0" + dt.Rows[dt.Rows.Count - 1]["Hur"].ToString().Trim() : dt.Rows[dt.Rows.Count - 1]["Hur"].ToString().Trim();
                        string OutMin = (dt.Rows[dt.Rows.Count - 1]["Min"].ToString().Trim().Length == 1) ? "0" + dt.Rows[dt.Rows.Count - 1]["Min"].ToString().Trim() : dt.Rows[dt.Rows.Count - 1]["Min"].ToString().Trim();
                        string OutSec = (dt.Rows[dt.Rows.Count - 1]["Sec"].ToString().Trim().Length == 1) ? "0" + dt.Rows[dt.Rows.Count - 1]["Sec"].ToString().Trim() : dt.Rows[dt.Rows.Count - 1]["Sec"].ToString().Trim();

                        if (InHur == OutHur && InMin==OutMin)
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
                        try
                        {
                            string attStatus = (isPresent) ? "p" : "l";
                            string StateStatus = "Present";
                            if (Convert.ToInt16(dt.Rows[0]["Hur"].ToString()) >= CH)
                            {
                                var latetime = (TimeSpan.Parse(InHur + ":" + InMin + ":" + InSec) - TimeSpan.Parse(CH.ToString() + ":" + CM.ToString() + ":" + CS.ToString())).TotalMinutes;
                                if (CAT < latetime)
                                    attStatus = "a";
                            }
                            if(bool.Parse(dtEmpInfo.Rows[i]["VP"].ToString())==true)
                            {
                                attStatus = "N/A";
                                StateStatus = "N/A";
                            }
                            string[] getColumns = { "EId", "AttDate", "InHur", "InMin", "InSec", "OutHur", "OutMin", "OutSec", "AttStatus", "StateStatus", "DailyStartTimeALT_CloseTime","DId","DesId","ShiftId" };
                            string[] getValues = { dtEmpInfo.Rows[i]["EId"].ToString(), sDate, InHur, InMin, InSec, OutHur, OutMin, OutSec, attStatus.ToString(), StateStatus, DailyStartTimeALT_CloseTime, dtEmpInfo.Rows[i]["DId"].ToString(), dtEmpInfo.Rows[i]["DesId"].ToString(), ddlShift.SelectedItem.Value };
                            SQLOperation.forSaveValue("DailyAttendanceRecord", getColumns, getValues, sqlDB.connection);
                        }
                        catch { }
                    }
                    else
                    {
                        string AttStatus = "a", StateStatus = "Absent";

                        DataTable dtLeaveInfo = new DataTable();
                        sqlDB.fillDataTable("select LACode From Leave_Application_Details where EID=" + dtEmpInfo.Rows[i]["EId"].ToString() + " AND LeaveDate = '" + attDate + "'", dtLeaveInfo);
                        if (dtLeaveInfo.Rows.Count > 0)
                        {
                            System.Data.SqlClient.SqlCommand cmd;

                            sqlDB.fillDataTable("select FORMAT(ToDate,'dd-MM-yyyy') as ToDate,LeaveId,LeaveName from v_Leave_Application where LACode=" + dtLeaveInfo.Rows[0]["LACode"].ToString() + "", dt = new DataTable());
                            if (selectedDate.Equals(dt.Rows[0]["ToDate"].ToString()))
                            {

                                cmd = new System.Data.SqlClient.SqlCommand("Update Leave_Application set IsProcessessed='0' where LACode= " + dt.Rows[0]["LACode"].ToString() + "", sqlDB.connection);
                                cmd.ExecuteNonQuery();

                            }

                            cmd = new System.Data.SqlClient.SqlCommand("Update Leave_Application_Details set used='1' where LACode= " + dtLeaveInfo.Rows[0]["LACode"].ToString() + " AND LeaveDate='" + attDate + "'", sqlDB.connection);
                            cmd.ExecuteNonQuery();

                            AttStatus = "lv"; StateStatus = dt.Rows[0]["LeaveName"].ToString();
                        }
                        if (bool.Parse(dtEmpInfo.Rows[i]["VP"].ToString()) == true)
                        {
                            AttStatus = "N/A";
                            StateStatus = "N/A";
                        }

                        try
                        {
                            string[] getColumns = { "EId", "AttDate", "InHur", "InMin", "InSec", "OutHur", "OutMin", "OutSec", "AttStatus", "StateStatus", "DailyStartTimeALT_CloseTime", "DId", "DesId", "ShiftId" };
                            string[] getValues = { dtEmpInfo.Rows[i]["EId"].ToString(), sDate, "00", "00", "00", "00", "00", "00", AttStatus, StateStatus, "00:00:00:00:00:00", dtEmpInfo.Rows[i]["DId"].ToString(), dtEmpInfo.Rows[i]["DesId"].ToString(), dtEmpInfo.Rows[i]["ShiftId"].ToString() };
                            SQLOperation.forSaveValue("DailyAttendanceRecord", getColumns, getValues, sqlDB.connection);
                        }
                        catch { }
                    }

                }
                if (dtEmpInfo.Rows.Count > 0)
                {
                    lblMessage.InnerText = "success->Successfully attendance counted";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "ClearInputBox();", true);
                }

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }



        private void setWeekend_Others_Holyday(string attDate, string purpose)
        {
            try
            {
                //SQLOperation.forDeleteRecordByIdentifier("DailyAttendanceRecord", "AttDate", attDate, sqlDB.connection);
                SqlCommand cmd1 = new SqlCommand("delete from DailyAttendanceRecord where Format(AttDate,'yyyy-MM-dd')='" + attDate + "' AND AttManual Is Null and ShiftId='" + ddlShift.SelectedItem.Value + "'", DbConnection.Connection);
                cmd1.ExecuteNonQuery();
                string attStatus = (purpose.Trim().Equals("Weekly Holiday")) ? "w" : "h";
                string temAttstatus = attStatus;
                string temReason = purpose;
                for (i = 0; i < dtEmpInfo.Rows.Count; i++)
                {
                    if (!CompareAdmissionDateAndIndate(i)) return; // // check Admission date and attendance date
                    {
                        dt = new DataTable();
                        sqlDB.fillDataTable("select la.LACode,la.LeaveName from Leave_Application la where la.IsApproved='true' AND la.LACode=(select LACode from Leave_Application_Details  where EId='" + dtEmpInfo.Rows[i]["EId"].ToString() + "' AND LeaveDate='" + ViewState["__AttDate__"].ToString() + "')", dt);
                        attStatus = (dt.Rows.Count > 0) ? "lv" : temAttstatus;
                        purpose = (dt.Rows.Count > 0) ? dt.Rows[0]["LeaveName"].ToString() : temReason;

                        if (bool.Parse(dtEmpInfo.Rows[i]["VP"].ToString()) == true)
                        {
                            attStatus = "N/A";
                            purpose = "N/A";
                        }
                        string[] d = txtAttendanceDate.Text.Trim().Split('-');
                        string sDate = d[2] + "-" + d[1] + "-" + d[0];
                        // for save Attendance 
                        string[] getColumns = { "EID", "AttDate", "InHur", "InMin", "InSec", "OutHur", "OutMin", "OutSec", "AttStatus", "StateStatus", "DailyStartTimeALT_CloseTime","DId","DesId" };
                        string[] getValues = { dtEmpInfo.Rows[i]["EId"].ToString(), sDate, "00", "00", "00", "00", "00", "00", attStatus, purpose, "00:00:00:00:00:00", dtEmpInfo.Rows[i]["DId"].ToString(), dtEmpInfo.Rows[i]["DesId"].ToString() };
                        SQLOperation.forSaveValue("DailyAttendanceRecord", getColumns, getValues, sqlDB.connection);


                        if (dt.Rows.Count > 0)
                        {
                            // find Todate of this leave
                            sqlDB.fillDataTable("select FORMAT(ToDate,'dd-MM-yyyy') as ToDate,LeaveId,LeaveName,LACode from Leave_Application where LACode=" + dt.Rows[0]["LACode"].ToString() + "", dt = new DataTable());

                            System.Data.SqlClient.SqlCommand cmd;

                            // if Todate is equal of current select days then below code is execute
                            if (selectedDate.Equals(dt.Rows[0]["ToDate"].ToString()))
                            {

                                cmd = new System.Data.SqlClient.SqlCommand("Update Leave_Application set IsProcessessed='0' where LACode= " + dt.Rows[0]["LACode"].ToString() + "", sqlDB.connection);
                                cmd.ExecuteNonQuery();

                            }

                            cmd = new System.Data.SqlClient.SqlCommand("Update Leave_Application_Details set used='1' where LeaveDate='" + attDate + "' AND LACode=" + dt.Rows[0]["LACode"].ToString() + "", sqlDB.connection);
                            cmd.ExecuteNonQuery();
                        }

                    }
                    

                }

                if (dtEmpInfo.Rows.Count > 0)
                {
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("Update Leave_Application_Details set used='1' where LeaveDate='" + attDate + "'", sqlDB.connection);
                    cmd.ExecuteNonQuery();

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
                DateTime AdmissionDate = new DateTime(int.Parse(dtEmpInfo.Rows[i]["EJoiningDate"].ToString().Substring(6, 4)), int.Parse(dtEmpInfo.Rows[i]["EJoiningDate"].ToString().Substring(3, 2)), int.Parse(dtEmpInfo.Rows[i]["EJoiningDate"].ToString().Substring(0, 2)));

                if (InDate >= AdmissionDate)
                {
                    Datestatus = true;
                    return true;

                }
                else
                {
                    i++;
                    if (i < dtEmpInfo.Rows.Count)
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
           // if (!PratialValidationBag()) return;
            selectedDate = txtPartialAttDate.Text;
            lblMessage.InnerText = "";
            dtEmpInfo = new DataTable();
            
            string attDate = txtPartialAttDate.Text.Trim().Substring(6, 4) + "-" + txtPartialAttDate.Text.Trim().Substring(3, 2) + "-" + txtPartialAttDate.Text.Trim().Substring(0, 2);
            sqlDB.fillDataTable("select Distinct EId,Format(EJoiningDate,'dd-MM-yyyy') as EJoiningDate,DId,DesId,ShiftId,VP from EmployeeInfo where ECardNo=" + txtCardNo.Text.Trim() + " AND IsActive='True' ", dtEmpInfo);

            //transfer al student info for set weekend or others holydays
            sqlDB.fillDataTable("select Convert(varchar(11),OffDate,105) as OffDate,Purpose from OffdaySettings where OffDate='" + attDate + "'", dt = new DataTable());

            if (dt.Rows.Count > 0)
            {
                setWeekend_Others_Holyday(attDate, dt.Rows[0]["Purpose"].ToString());
                return;
            }
            //-----------Import File------------ 
            if (Session["__IsOnline__"].ToString().Equals("True") && fileupload.HasFile == true)
                ImportAttendance(attDate, false);
            //----------------------------------
            // Intermediat media for transfer all attendance record
           // setDailyAttendanceInTransferMedia(attDate, false);

            // Method calling for set daily attendance as record
            setDailyAttendanceRecord(attDate, false);
        }
        private void LoadDailyAttendanceReportData(string reportType)
        {
            //-----------Validation--------------           
            if (ddlShiftList.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Shift!"; ddlShiftList.Focus(); return; }        
           
            DataTable dt = new DataTable();        
            DepartmentList = (dlDepartment.SelectedItem.Text == "All") ? GetAlllist(dlDepartment) : dlDepartment.SelectedValue;
            DesignationList = (dlDesignation.SelectedItem.Text == "All") ? GetAlllist(dlDesignation) : dlDesignation.SelectedValue;
            if (reportType == "attendance") // Daily Attendance Status
            {
                sqlCmd = "select ECardNo,EName,DName,DesName,AttStatus,Format(AttDates,'dd-MM-yyyy') as AttDates from v_DailyEmployeeAttendanceRecord where DId in(" + DepartmentList + ") and DesId in(" + DesignationList + ") and ShiftId='" + ddlShiftList.SelectedValue + "' and Format (AttDates,'dd-MM-yyyy')='" + DateTime.Now.ToString("dd-MM-yyyy") + "'";
                ReportTitel = "Daily Attendance Status";
                ReportType = "Status";
            }
            else if (reportType == "present") // Daily Present status
            {
                sqlCmd = "select ECardNo,EName,DName,DesName,AttStatus,Format(AttDates,'dd-MM-yyyy') as AttDates from v_DailyEmployeeAttendanceRecord where  AttStatus='p' and DId in(" + DepartmentList + ") and DesId in(" + DesignationList + ") and ShiftId='" + ddlShiftList.SelectedValue + "' and Format (AttDates,'dd-MM-yyyy')='" + DateTime.Now.ToString("dd-MM-yyyy") + "'";
                ReportTitel = "Daily Present Status";
                ReportType = "PresentAbsent";
            }
            else if (reportType == "absent") // Daily Absent Staust
            {
                sqlCmd = "select ECardNo,EName,DName,DesName,AttStatus,Format(AttDates,'dd-MM-yyyy') as AttDates from v_DailyEmployeeAttendanceRecord where  AttStatus='a' and DId in(" + DepartmentList + ") and DesId in(" + DesignationList + ") and ShiftId='" + ddlShiftList.SelectedValue + "' and Format (AttDates,'dd-MM-yyyy')='" + DateTime.Now.ToString("dd-MM-yyyy") + "'";
                ReportTitel = "Daily Absent Status";
                ReportType = "PresentAbsent";
            }

            //sqlDB.fillDataTable(sqlCmd, dt = new DataTable());
            dt = CRUD.ReturnTableNull(sqlCmd);
            if (dt.Rows.Count < 1)
            {               
                btnPrintPreview.Enabled = false;
                btnPrintPreview.CssClass = "";
                gvAttList.DataSource = null;
                gvAttList.DataBind();
                return;
            }
            btnPrintPreview.Enabled = true;
            btnPrintPreview.CssClass = "btn btn-primary litleMargin";
            gvAttList.Visible = true;            
            gvAttList.DataSource = dt;
            gvAttList.DataBind();
            Session["__DailyEmpAttendance__"] = dt;
            ViewState["__ReportTitle__"] = ReportTitel;
            ViewState["__ReportType__"] = ReportType;
            ViewState["__Report__"] = "";
           
        }
        private string GetAlllist(DropDownList ddlSftList)
        {
            try
            {
                string setPredicate = "";
                for (byte b = 0; b < ddlSftList.Items.Count; b++)
                {
                    setPredicate += ddlSftList.Items[b].Value.ToString() + ",";
                }

                setPredicate = setPredicate.Remove(setPredicate.LastIndexOf(','));
                return setPredicate;
            }
            catch { return " "; }

        }
        protected void btnTodayAttendanceList_Click(object sender, EventArgs e)
        {
            LoadDailyAttendanceReportData("attendance");
        }

        protected void btnTodayPresentList_Click(object sender, EventArgs e)
        {
            LoadDailyAttendanceReportData("present");
        }

        protected void btnTodayAbsentList_Click(object sender, EventArgs e)
        {
            LoadDailyAttendanceReportData("absent");
        }

        protected void btnPrintPreview_Click1(object sender, EventArgs e)
        {
            if (ViewState["__Report__"].ToString() == "")
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DailyEmpAttendance-" + ViewState["__ReportTitle__"].ToString() + "-" + ViewState["__ReportType__"].ToString() + "-" + ddlShiftList.SelectedItem.Text + "-" + "Staff and Fatulty" + "');", true);
            else
                GeneratMonthlyEmpAttendanceSheet();
          
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvAttList.DataSource = null;
            gvAttList.DataBind();
            //sqlDB.fillDataTable("Select * From Faculty_Staff_AttendanceSheet_" + dlSheetName.SelectedItem.Text + "", dt);
            //DataTable dt = new DataTable();
            //FilteringByDepartmentDesignationName(dt);
            //reportGenerateForFiltering(); 
            if (ddlShiftList.SelectedValue == "0") { lblMessage.InnerText = "warning->Please select a Shift!"; ddlShiftList.Focus(); return; }
            if (dlSheetName.SelectedValue == "0") { lblMessage.InnerText = "warning->Please select a Month!"; dlSheetName.Focus(); return; }
            dt = new DataTable();
            DepartmentList = (dlDepartment.SelectedItem.Text == "All") ? GetAlllist(dlDepartment) : dlDepartment.SelectedValue;
            DesignationList = (dlDesignation.SelectedItem.Text == "All") ? GetAlllist(dlDesignation) : dlDesignation.SelectedValue;
            dt = ForAttendanceReport.returnDatatableForEmpAttSheet(ddlShiftList.SelectedValue, dlSheetName.SelectedValue, DepartmentList, DesignationList);
            //dt = EmpSheetInfoEntry.getEmpAttendanceSheet(dlDepartment.SelectedItem.Value.ToString(),dlSheetName.SelectedValue);
            if (dt == null || dt.Rows.Count < 1)
            {
                lblMessage.InnerText= "warning->Any Staff/Facult attendanc record are not available in " + dlSheetName.SelectedItem.Text + " ";
                btnPrintPreview.Enabled = false;
                btnPrintPreview.CssClass = "";
                return;
            }
            lblMessage.InnerText = "warning->Monthly Staff/Faculty attendance sheet of " + dlSheetName.SelectedItem.Text + "";
            ViewState["__Report__"] = "MonthlyAttSheet";
            //lblMonthName.Text = "Attendance Sheet at " + dlSheetName.SelectedItem.Text;
            //lblDepName.Text = "Department : " + dlDepartment.SelectedItem.Text;
            //lblDesName.Text = "Designation : " + dlDesignation.SelectedItem.Text;
            btnPrintPreview.Enabled = true;
            btnPrintPreview.CssClass = "btn btn-primary litleMargin";
            loadShiftInfo();
            loadAttendanceSheet(dt);
            //..........................For Sheet ..............................                
        }
        private void loadShiftInfo()
        {
            try
            {
                DataTable dtShiftTime = new DataTable();
                dtShiftTime = CRUD.ReturnTableNull("select StartTime,CloseTime,LateTime from ShiftConfiguration where ConfigId =" + ddlShiftList.SelectedItem.Value + "");
                ViewState["__StartTime__"] = dtShiftTime.Rows[0]["StartTime"].ToString();

                ViewState["__CloseTime__"] = dtShiftTime.Rows[0]["CloseTime"].ToString();

                ViewState["__LateTime__"] = dtShiftTime.Rows[0]["LateTime"].ToString();
            }
            catch { }
        }
        private void loadAttendanceSheet(DataTable dtStudentInf)
        {
            try
            {
                lblMessage.InnerText = "";
                // AttendanceSheetTitle.InnerText = "";

                DataView dv = new DataView(dtStudentInf);
                dt = dv.ToTable(false, "EId", "ECardNo", "EName", "DId", "1_Code", "2_Code", "3_Code", "4_Code", "5_Code", "6_Code", "7_Code", "8_Code", "9_Code", "10_Code",
                    "11_Code", "12_Code", "13_Code", "14_Code", "15_Code", "16_Code", "17_Code", "18_Code", "19_Code", "20_Code",
                    "21_Code", "22_Code", "23_Code", "24_Code", "25_Code", "26_Code", "27_Code", "28_Code", "29_Code", "30_Code", "31_Code");



                //AttendanceSheetTitle.Style["Color"] = "#1fb5ad";
                //AttendanceSheetTitle.InnerText = "Attendance sheet of Faculty and staff " + dlMonths.Text + "";


                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(dt.Rows.Count));
                string tbl = "";
                string tblInputElement = "";
                int DaysInMonth = DateTime.DaysInMonth(int.Parse(dlSheetName.SelectedItem.Value.Substring(3, 4)), int.Parse(dlSheetName.SelectedItem.Value.Substring(0, 2)));

                DataTable dtOffdays = new DataTable();
                sqlDB.fillDataTable("select Format(OffDate,'dd') as OffDate,Purpose from OffdaySettings where Format(OffDate,'MM-yyyy')='" + dlSheetName.SelectedItem.Value + "' order by OffDate ", dtOffdays);

                for (byte b = 4; b < (DaysInMonth + 4); b++)
                {
                    string[] col = dt.Columns[b].ToString().Split('_');
                    string col1 = col[0];
                    col1 = new String(col1.Where(Char.IsNumber).ToArray());
                    tbl += "<th style='width: 76px;text-align:center'>" + col1 + "</th>";
                }

                string tableInfo = "";
                tableInfo = "<table id='tblStudentAttendance'   >";
                tableInfo += " <th style='width: 70px; text-align:center'>CardNo</th> <th style='width: 280px'>Name</th>" + tbl + "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tblInputElement = "";
                    int row = i + 1;
                    DataTable dtTemp = dtOffdays.Copy();
                    for (byte b = 4; b < (DaysInMonth + 4); b++)   // this loop generate every student inputbox 
                    {
                        string attStatus = string.Empty;
                        if (dt.Rows[i].ItemArray[b].ToString().Equals("120") || dt.Rows[i].ItemArray[b].ToString().Equals("0"))
                            attStatus = string.Empty;
                        else if (dt.Rows[i].ItemArray[b].ToString().Equals("112")) attStatus = "p";
                        else if (dt.Rows[i].ItemArray[b].ToString().Equals("97")) attStatus = "a";
                        else if (dt.Rows[i].ItemArray[b].ToString().Equals("108")) attStatus = "l";

                        //string attStatus = string.Empty;
                        if (dtTemp.Rows.Count > 0)
                        {
                            bool isStatus = false;
                            for (byte x = 0; x < dtTemp.Rows.Count; x++)
                            {
                                if (int.Parse((b - 3).ToString()) == int.Parse(dtTemp.Rows[x]["OffDate"].ToString()))
                                {
                                    attStatus = (dtTemp.Rows[x]["Purpose"].ToString().Equals("Weekly Holiday")) ? "w" : "h";
                                    tblInputElement += "<td  style='width: 50px'> <input AutosizeMode ='false' readonly='true' autocomplete='off' readonly='false' style='background-color:#980000 ;color:White; text-align:center'  tabindex=" + row + " onchange='saveData(this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + (b - 3).ToString() + "_" + dlSheetName.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + dlDepartment.SelectedItem.Value + ":" + ddlShiftList.SelectedItem.Value + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + "> </td>";  // this line for hilight weekly liholyday 

                                    isStatus = true;
                                    dtTemp.Rows.RemoveAt(x);
                                    break;
                                }
                            }

                            if (!isStatus)
                            {
                                if (dt.Rows[i].ItemArray[b].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px'> <input  style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text'  id='DailyAttendanceRecord:" + (b - 3).ToString() + "_" + dlSheetName.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + dlDepartment.SelectedItem.Value + ":" + ddlShiftList.SelectedItem.Value + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + " > </td>";
                                else tblInputElement += "<td style='width: 50px'> <input style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + (b - 3).ToString() + "_" + dlSheetName.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + "' value=" + attStatus + "> </td>";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i].ItemArray[b].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px'> <input  style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + (b - 3).ToString() + "_" + dlSheetName.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + "' value=" + attStatus + "> </td>";
                            else tblInputElement += "<td style='width: 50px'> <input style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + (b - 3).ToString() + "_" + dlSheetName.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + "' value=" + attStatus + "> </td>";
                        }
                        row += dt.Rows.Count;
                    }
                    tableInfo += "<tr> <td style='width: 80px;text-align:center'> " + dt.Rows[i]["ECardNo"].ToString() + "</td>  <td style='width: 60px'>" + dt.Rows[i]["EName"].ToString() + "</td>" + tblInputElement + "</tr>";
                }
                tableInfo += "</table>";
                divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(tableInfo));
                // divTable.Visible = true;

            }
            catch
            {
                //AttendanceSheetTitle.Style["Color"] = "Red";
                // AttendanceSheetTitle.InnerText = "Sorry this attendance sheet is not created";
                // divTable.Visible = false;
            }
        }
        private void GeneratMonthlyEmpAttendanceSheet()
        {
            //----------validation-------------------------------
            if (ddlShiftList.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Shift!"; ddlShiftList.Focus(); return; }
            if (dlSheetName.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Month!"; dlSheetName.Focus(); return; }
            //-----------------------------------------------------

            if (dlDepartment.SelectedItem.Text == "All") DepartmentList = GetAlllist(dlDepartment);
            else DepartmentList = dlDepartment.SelectedValue;
            if (dlDesignation.SelectedItem.Text == "All") DesignationList = GetAlllist(dlDesignation);
            else DepartmentList = dlDesignation.SelectedValue;
            sqlCmd = " SELECT  EName as FullName, ECardNo as RollNo,  SUM(CASE DATEPART(day, AttDates) WHEN 1 THEN code ELSE 0 END) AS [1], SUM(CASE DATEPART(day, AttDates) " +
                        " WHEN 2 THEN code ELSE 0 END) AS [2], SUM(CASE DATEPART(day, AttDates) WHEN 3 THEN code ELSE 0 END) AS [3], SUM(CASE DATEPART(day, AttDates) WHEN 4 THEN code ELSE 0 END) AS [4], " +
                        " SUM(CASE DATEPART(day, AttDates) WHEN 5 THEN code ELSE 0 END) AS [5], SUM(CASE DATEPART(day, AttDates) WHEN 6 THEN code ELSE 0 END) AS [6], SUM(CASE DATEPART(day, AttDates) " +
                        " WHEN 7 THEN code ELSE 0 END) AS [7], SUM(CASE DATEPART(day, AttDates) WHEN 8 THEN code ELSE 0 END) AS [8], SUM(CASE DATEPART(day, AttDates) WHEN 9 THEN code ELSE 0 END) AS [9]," +
                        " SUM(CASE DATEPART(day, AttDates) WHEN 10 THEN code ELSE 0 END) AS [10], SUM(CASE DATEPART(day, AttDates) WHEN 11 THEN code ELSE 0 END) AS [11], SUM(CASE DATEPART(day, AttDates) " +
                        " WHEN 12 THEN code ELSE 0 END) AS [12], SUM(CASE DATEPART(day, AttDates) WHEN 13 THEN code ELSE 0 END) AS [13], SUM(CASE DATEPART(day, AttDates) WHEN 14 THEN code ELSE 0 END) AS [14], " +
                        " SUM(CASE DATEPART(day, AttDates) WHEN 15 THEN code ELSE 0 END) AS [15], SUM(CASE DATEPART(day, AttDates) WHEN 16 THEN code ELSE 0 END) AS [16], SUM(CASE DATEPART(day, AttDates) " +
                        " WHEN 17 THEN code ELSE 0 END) AS [17], SUM(CASE DATEPART(day, AttDates) WHEN 18 THEN code ELSE 0 END) AS [18], SUM(CASE DATEPART(day, AttDates) WHEN 19 THEN code ELSE 0 END) AS [19], " +
                        " SUM(CASE DATEPART(day, AttDates) WHEN 20 THEN code ELSE 0 END) AS [20], SUM(CASE DATEPART(day, AttDates) WHEN 21 THEN code ELSE 0 END) AS [21], SUM(CASE DATEPART(day, AttDates) " +
                        " WHEN 22 THEN code ELSE 0 END) AS [22], SUM(CASE DATEPART(day, AttDates) WHEN 23 THEN code ELSE 0 END) AS [23], SUM(CASE DATEPART(day, AttDates) WHEN 24 THEN code ELSE 0 END) AS [24]," +
                        " SUM(CASE DATEPART(day, AttDates) WHEN 25 THEN code ELSE 0 END) AS [25], SUM(CASE DATEPART(day, AttDates) WHEN 26 THEN code ELSE 0 END) AS [26], SUM(CASE DATEPART(day, AttDates) " +
                        " WHEN 27 THEN code ELSE 0 END) AS [27], SUM(CASE DATEPART(day, AttDates) WHEN 28 THEN code ELSE 0 END) AS [28], SUM(CASE DATEPART(day, AttDates) WHEN 29 THEN code ELSE 0 END) AS [29]," +
                        " SUM(CASE DATEPART(day, AttDates) WHEN 30 THEN code ELSE 0 END) AS [30], SUM(CASE DATEPART(day, AttDates) WHEN 31 THEN code ELSE 0 END) AS [31], SUM(CASE Code WHEN 112 THEN 1 ELSE 0 END) " +
                        " AS P, SUM(CASE Code WHEN 97 THEN 1 ELSE 0 END) AS A, SUM(CASE Code WHEN 104 THEN 1 ELSE 0 END) AS H, SUM(CASE Code WHEN 119 THEN 1 ELSE 0 END) AS W, " +
                        " SUM(CASE Code WHEN 226 THEN 1 ELSE 0 END) AS LV, DName as BatchId " +
                        " FROM dbo.v_DailyEmployeeAttendanceRecord " +
                        " where DId in(" + DepartmentList + ") and DesId in(" + DesignationList + ")and ShiftId='" + ddlShiftList.SelectedValue + "' and FORMAT(AttDates,'MM-yyyy')='" + dlSheetName.SelectedValue + "'" +
                        " GROUP BY EName,ECardNo,DName";
            dt = CRUD.ReturnTableNull(sqlCmd);
            if (dt == null || dt.Rows.Count < 1)
            {
                lblMessage.InnerText = "warning-> Any attendance Record are not founded!";
                return;
            }
            Session["__EmpAttendanceSheet__"] = dt;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=EmpAttendanceSheet-" + dlSheetName.SelectedItem.Text + "-" + ddlShiftList.SelectedItem.Text + "-" + " " + "');", true);
            //Open New Tab for Sever side code  
        }
    }
}