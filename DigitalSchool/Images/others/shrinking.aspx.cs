using adviitRuntimeScripting;
using ComplexScriptingSystem;
using DS.BLL.Attendace;
using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.others
{
    public partial class shrinking : System.Web.UI.Page
    {
        DataTable dtEmpInfo = new DataTable();
        bool ShrinkSuccess = true;
        string selectedDate;
        DataTable dt;
        DataTable dtStudentInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            ProgressBar1.Minimum = 0;
            ProgressBar1.Maximum = 100;
            ProgressBar1.BackColor = System.Drawing.Color.Blue;
            ProgressBar1.ForeColor = Color.White;
            ProgressBar1.Height = new Unit(20);
            ProgressBar1.Width = new Unit(100);
           

            if (!IsPostBack)
            {
               
              
                SqlConnection local_con = new SqlConnection();
                local_con.ConnectionString = WebConfigurationManager.ConnectionStrings["local"].ConnectionString;
                local_con.Open();

                SqlConnection remote_con = new SqlConnection();
                //remote_con.ConnectionString = WebConfigurationManager.ConnectionStrings["remote"].ConnectionString;
                Classes.RemoteServerConnection.GetRemoteServerConnection();
                remote_con = Classes.RemoteServerConnection.remote_con;

                //try
                //{
                //   DatabaseBackupRestore.DatabaseBackup("DSCC", "D:\\DatabaseBank\\DSCC" + DateTime.Now.Month + "." + DateTime.Now.Year + "",local_con);
                //}
                //catch { }

                SqlCommand cmd;

                cmd = new SqlCommand("insert into shrinkInfo (UserId) values('" + Session["__UserId__"].ToString() + "')", local_con);
                cmd.ExecuteNonQuery();
                local_con.Close();
               
                   /*
                    
                    cmd = new SqlCommand("Update CHECKINOUT set IsSink='True' where UserId='" + dtAttData.Rows[r]["UserId"].ToString() + "' AND Format(CHECKTIME,'yyyy-MM-dd')='" + DateTime.Now.ToString("yyyy-MM-dd") + "'", local_con);
                    cmd.ExecuteNonQuery();



                    cmd = new SqlCommand("insert into CHECKINOUT (USERID,CHECKTIME,CHECKTYPE,VERIFYCODE,SENSORID,WorkCode,sn,UserExtFmt) " +
                         " values ('" + dtAttData.Rows[r]["USERID"].ToString() + "','" + dtAttData.Rows[r]["CHECKTIME"].ToString() + "','I','0'," +
                         " '" + dtAttData.Rows[r]["SENSORID"].ToString() + "','0','" + dtAttData.Rows[r]["SN"].ToString() + "','" + dtAttData.Rows[r]["UserExtFmt"].ToString() + "') ", remote_con);
                    cmd.ExecuteNonQuery();
                    */
                local_con.Open();
                string sql = string.Format("SELECT [ConfigId],[ShiftName],[StartTime],[CloseTime]," +
                               "[LateTime] FROM [dbo].[ShiftConfiguration]");
                DataTable dtshift = new DataTable();
                da = new SqlDataAdapter(sql,local_con);
                da.Fill(dtshift);
                local_con.Close();
                for (int i = 0; i < dtshift.Rows.Count; i++)
                {
                    local_con.Open();
                    selectedDate = DateTime.Now.ToString("dd-MM-yyyy");
                    sql = string.Format("SELECT [BatchId],[BatchName],[IsUsed],[Year],[ClassID] "
            + "FROM [dbo].[BatchInfo] where [IsUsed]='True' ORDER BY [ClassID]");
                    DataTable dtbatch = new DataTable();
                    dtbatch = CRUD.ReturnTableNull(sql);
                   // sqlDB.fillDataTable(sql, dtbatch);
                    for (int k = 0; k < dtbatch.Rows.Count;k++ )
                    {
                        ViewState["__BatchId__"] = dtbatch.Rows[k]["BatchId"].ToString();
                        ViewState["__ShiftId__"] = dtshift.Rows[i]["ConfigId"].ToString();
                        // load all student according to shift
                        dtStudentInfo = new DataTable();
                        string attDate = selectedDate.Substring(6, 4) + "-" + selectedDate.Substring(3, 2) + "-" + selectedDate.Substring(0, 2);
                        dtStudentInfo = CRUD.ReturnTableNull("select Distinct RollNo,StudentId,CONVERT(varchar(10),AdmissionDate,105) as AdmissionDate, ConfigId as  ShiftId,ClsGrpId,ClsSecId from v_CurrentStudentInfo where IsActive='True' And ConfigId='" + ViewState["__ShiftId__"].ToString() + "' AND BatchId=" + ViewState["__BatchId__"].ToString() + "");
                        if (dtStudentInfo.Rows.Count == 0) continue;

                        //transfer al student info for set weekend or others holydays
                        dt = new DataTable();
                        dt = CRUD.ReturnTableNull("select Convert(varchar(11),OffDate,105) as OffDate,Purpose from OffdaySettings where OffDate='" + attDate + "'");

                        if (dt.Rows.Count > 0)
                        {
                            setWeekend_Others_Holyday(attDate, dt.Rows[0]["Purpose"].ToString(),local_con);
                            return;
                        }

                        // Intermediat media for transfer all attendance record
                        //setDailyAttendanceInTransferMedia(attDate, true,local_con);

                        // Method calling for set daily attendance as record
                        setDailyAttendanceRecord(attDate, true,local_con);
                    }

                        
                }
                    
                local_con.Close();
                remote_con.Close();
              //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "CloseWindow();", true);  //Open New Tab for Sever side code
              //Response.Redirect("/default.aspx");
            }

            
        }
        SqlDataAdapter da;
        private void setDailyAttendanceInTransferMedia(string attDate, bool ForAllStudents,SqlConnection local)
        {
            try
            {


                DataTable dt = new DataTable();

                if (ForAllStudents) dt = CRUD.ReturnTableNull("select * from v_CHECKINOUT where CONVERT(varchar(10),[CHECKTIME],126)='" + attDate + "' order by UserId");
                local.Close();
                local.Open();
                SQLOperation.forDelete("AttendanceTransferMedia", local);
               
                for (int s = 0; s < dt.Rows.Count; s++)
                {
                    local.Close();
                    local.Open();
                    string[] getColumns = { "Student_Emp_AdmNo", "AttDate", "Hur", "Min", "Sec" };
                    string[] getValues = { dt.Rows[s]["Badgenumber"].ToString(), convertDateTime.getCertainCulture(selectedDate).ToString(), Convert.ToDateTime(dt.Rows[s]["CHECKTIME"].ToString()).Hour.ToString(), Convert.ToDateTime(dt.Rows[s]["CHECKTIME"].ToString()).Minute.ToString(), Convert.ToDateTime(dt.Rows[s]["CHECKTIME"].ToString()).Second.ToString() };
                    SQLOperation.forSaveValue("AttendanceTransferMedia", getColumns, getValues, local);
                }
            }
            catch (Exception ex)
            {
                //lblMessage.InnerText = "warning->" + ex.Message;
            }
        }

        int i;
        int j = 0;
        private void setDailyAttendanceRecord(string attDate, bool ForAllStudents,SqlConnection local)
        {
            try
            {

                CheckFineSettings();
                string sql="";
                // SQLOperation.forDeleteRecordByIdentifier("DailyAttendanceRecord", "AttDate", attDate, sqlDB.connection);
                SqlCommand cmd;
                if (ForAllStudents) sql = string.Format("delete from DailyAttendanceRecord where CONVERT(varchar(10),[AttDate],126)='" + attDate + "' and BatchId='" + ViewState["__BatchId__"].ToString() + "' and ShiftId='" + ViewState["__ShiftId__"].ToString() + "' AND AttManual Is Null");
                else sql =string.Format("delete from DailyAttendanceRecord where CONVERT(varchar(10),[AttDate],126)='" + attDate + "' AND StudentId=" + dtStudentInfo.Rows[0]["StudentId"].ToString() + " AND AttManual Is Null");
               CRUD.ExecuteQuery(sql);

                // for get shift information
                DataTable dtShift = new DataTable();
                dtShift = CRUD.ReturnTableNull("select StartTime,CloseTime,LateTime,AbsentTime from ShiftConfiguration where ConfigId='" + ViewState["__ShiftId__"].ToString() + "'");

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
                       // if (!ForAllStudents) lblMessage.InnerText = "error->This student is not admitted in this date.";
                        return;
                    }
                    else i = j;
                    dt = new DataTable();
                    dt = CRUD.ReturnTableNull("select distinct Badgenumber,left(CONVERT(VARCHAR(5),CHECKTIME,108),2) as Hur,right(CONVERT(VARCHAR(5),CHECKTIME,108),2) as Min,right(CONVERT(VARCHAR(8),CHECKTIME,108),2) as Sec, CONVERT(varchar(8),CHECKTIME,108) as PunchTime from v_CHECKINOUT where convert(varchar(11),CHECKTIME,121)='" + attDate + "' AND " +
                        "Badgenumber=convert(varchar(50),(select AdmissionNo from TBL_STD_Admission_INFO where StudentId=" + dtStudentInfo.Rows[i]["StudentId"].ToString() + ")) " +
                        "");
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
                        local.Close();
                        local.Open();
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
                        SQLOperation.forSaveValue("DailyAttendanceRecord", getColumns, getValues, local);
                    }
                    else
                    {
                        local.Close();
                        local.Open();
                        string[] getColumns = { "StudentId", "RollNo", "BatchId", "ShiftId", "ClsSecId", "ClsGrpId", "AttDate", "InHur", "InMin", "InSec", "OutHur", "OutMin", "OutSec", "AttStatus", "StateStatus", "DailyStartTimeALT_CloseTime" };
                        string[] getValues = { dtStudentInfo.Rows[i]["StudentId"].ToString(), dtStudentInfo.Rows[i]["RollNo"].ToString(),ViewState["__BatchId__"].ToString(), ViewState["__ShiftId__"].ToString(), dtStudentInfo.Rows[i]["ClsSecId"].ToString(), dtStudentInfo.Rows[i]["ClsGrpId"].ToString(), 
                                              convertDateTime.getCertainCulture(selectedDate).ToString(), "00", "00", "00", "00", "00", "00", "a", "Absent", "00:00:00:00:00:00" };
                        SQLOperation.forSaveValue("DailyAttendanceRecord", getColumns, getValues, local);

                        if (ViewState["__IsActive__"].Equals("True"))
                        {
                            StudentAbsentDetailsEntry.Delete(attDate, dtStudentInfo.Rows[i]["StudentId"].ToString());
                            StudentAbsentDetailsEntry.Insert(ViewState["__BatchId__"].ToString(), dtStudentInfo.Rows[i]["StudentId"].ToString(), attDate, ViewState["__AbsentFineAmount__"].ToString(), "0");
                        }
                    }
                }
                if (dtStudentInfo.Rows.Count > 0)
                {
                    //lblMessage.InnerText = "success->Successfully attendance counted";
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "ClearInputBox();", true);
                }
            }
            catch (Exception ex)
            {
                //lblMessage.InnerText = ex.Message;
            }
        }



        private void setWeekend_Others_Holyday(string attDate, string purpose,SqlConnection local)
        {
            try
            {
                local.Close();
                local.Open();
                SQLOperation.forDeleteRecordByIdentifier("DailyAttendanceRecord", "AttDate", attDate, local);
                string attStatus = (purpose.Trim().Equals("Weekly Holiday")) ? "w" : "h";
                for (i = 0; i < dtStudentInfo.Rows.Count; i++)
                {
                    local.Close();
                    local.Open();
                    if (!CompareAdmissionDateAndIndate(i)) return; // // check Admission date and attendance date
                    string[] getColumns = { "StudentId", "RollNo", "BatchId", "ShiftId", "ClsSecId", "ClsGrpId", "AttDate", "InHur", "InMin", "InSec", "OutHur", "OutMin", "OutSec", "AttStatus", "StateStatus", "DailyStartTimeALT_CloseTime" };
                    string[] getValues = { dtStudentInfo.Rows[i]["StudentId"].ToString(),dtStudentInfo.Rows[i]["RollNo"].ToString(),ViewState["__BatchId__"].ToString(), ViewState["__ShiftId__"].ToString(), dtStudentInfo.Rows[i]["ClsSecId"].ToString(), dtStudentInfo.Rows[i]["ClsGrpId"].ToString(),
                                          convertDateTime.getCertainCulture(DateTime.Now.ToString("dd-MM-yyyy")).ToString(), "00", "00", "00", "00", "00", "00", attStatus, purpose, "00:00:00:00:00:00" };
                    SQLOperation.forSaveValue("DailyAttendanceRecord", getColumns, getValues, local);

                }
                //lblMessage.InnerText = "success->Successfully attendance counted";
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "ClearInputBox();", true);
            }
            catch (Exception ex)
            {
                //lblMessage.InnerText = "warning->" + ex.Message;
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
        private void CheckFineSettings()
        {
            dt = new DataTable();
            dt=CRUD.ReturnTableNull("select AbsentFineAmount,IsActive from AbsentFine where IsActive='True'");
            ViewState["__IsActive__"] = (dt.Rows.Count > 0) ? dt.Rows[0]["IsActive"].ToString() : "False";
            ViewState["__AbsentFineAmount__"] = (dt.Rows.Count > 0) ? dt.Rows[0]["AbsentFineAmount"].ToString() : "0";
        }
       
    }
}