using DS.DAL.AdviitDAL;
using ComplexScriptingSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Forms.machine_attendance
{
    public partial class emp_import_data : System.Web.UI.Page
    {
        DataTable dt;
        DataTable dtEmpInfo;
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
                    Classes.commonTask.loadShift(ddlShift);
                }
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            selectedDate = txtAttendanceDate.Text;
            lblMessage.InnerText = "";
            // load all student according to shift
            dtEmpInfo = new DataTable();
            string attDate = txtAttendanceDate.Text.Trim().Substring(6, 4) + "-" + txtAttendanceDate.Text.Trim().Substring(3, 2) + "-" + txtAttendanceDate.Text.Trim().Substring(0, 2);
            sqlDB.fillDataTable("select Distinct EID,ECardNo,Format(EJoiningDate,'dd-MM-yyyy') as EJoiningDate from EmployeeInfo where IsActive='True' ", dtEmpInfo);

            //transfer all Employee info for set weekend or others holydays
            sqlDB.fillDataTable("select Convert(varchar(11),OffDate,105) as OffDate,Purpose from OffdaySettings where OffDate='" + attDate + "'", dt = new DataTable());

            if (dt.Rows.Count > 0)
            {
                setWeekend_Others_Holyday(attDate, dt.Rows[0]["Purpose"].ToString());
                return;
            }

            // Intermediat media for transfer all attendance record
            setDailyAttendanceInTransferMedia(attDate, true);

            // Method calling for set daily attendance as record
            setDailyAttendanceRecord(attDate, true);
        }

        string selectedDate;
        private void setDailyAttendanceInTransferMedia(string attDate, bool ForAllStudents)
        {
            try
            {
                string connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Program Files\\ZKTime5.0\\att2000.mdb";
                OleDbConnection cont = new OleDbConnection(connection);
                cont.Open();

                if (ForAllStudents) setEmpIdAndCardNo(cont);

                DataTable dt = new DataTable();
                OleDbDataAdapter da;
                if (ForAllStudents) da = new OleDbDataAdapter("select * from v_CHECKINOUT where Format([CHECKTIME],'yyyy-mm-dd')='" + attDate + "' AND  Badgenumber in (select ECardNo from EId_ECardNo  ) order by UserId", cont);
                else da = new OleDbDataAdapter("select * from v_CHECKINOUT where Format([CHECKTIME],'yyyy-mm-dd')='" + attDate + "' AND Badgenumber='" + txtCardNo.Text.Trim() + "' order by UserId", cont);
                da.Fill(dt);
                cont.Close();
                SQLOperation.forDelete("AttendanceTransferMedia", sqlDB.connection);

                for (int s = 0; s < dt.Rows.Count; s++)
                {
                    string a = Convert.ToDateTime(dt.Rows[s]["CHECKTIME"].ToString()).Hour.ToString();
                    string[] getColumns = { "Student_Emp_AdmNo", "AttDate", "Hur", "Min", "Sec" };
                    string[] getValues = { dt.Rows[s]["Badgenumber"].ToString(), convertDateTime.getCertainCulture(selectedDate).ToString(), Convert.ToDateTime(dt.Rows[s]["CHECKTIME"].ToString()).Hour.ToString(), Convert.ToDateTime(dt.Rows[s]["CHECKTIME"].ToString()).Minute.ToString(), Convert.ToDateTime(dt.Rows[s]["CHECKTIME"].ToString()).Second.ToString() };
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
                cmd = new OleDbCommand("delete from EId_ECardNo",cont);
                cmd.ExecuteNonQuery();
                for(int i=0;i<dtEmpInfo.Rows.Count;i++)
                {

                     cmd = new OleDbCommand("insert into EId_ECardNo (EId,ECardNo) values ("+dtEmpInfo.Rows[i]["EId"].ToString()+","+dtEmpInfo.Rows[i]["ECardNo"].ToString()+")",cont);
                     cmd.ExecuteNonQuery();
                    

                }
            }
            catch { }
        }

        int i;
        int j = 0;
        private void setDailyAttendanceRecord(string attDate, bool ForAllStudents)
        {
            try
            {
                SQLOperation.forDeleteRecordByIdentifier("DailyAttendanceRecord", "AttDate", attDate, sqlDB.connection);

                // for get shift information
                DataTable dtShift = new DataTable();
                sqlDB.fillDataTable("select StartTime,CloseTime,LateTime from ShiftConfiguration", dtShift);

                int CH = Convert.ToDateTime(dtShift.Rows[0]["StartTime"].ToString()).Hour;  // for get office start hour
                int CM = Convert.ToDateTime(dtShift.Rows[0]["StartTime"].ToString()).Minute;  // for get office start minute
                int CS = Convert.ToDateTime(dtShift.Rows[0]["StartTime"].ToString()).Second;  // for get office start second
                int CAM = Convert.ToInt32(dtShift.Rows[0]["LateTime"].ToString());  // for get acceptable late time

                int COutH = Convert.ToDateTime(dtShift.Rows[0]["CloseTime"].ToString()).Hour;  // for get office start hour
                int COutM = Convert.ToDateTime(dtShift.Rows[0]["CloseTime"].ToString()).Minute;  // for get office start hour

                string DailyStartTimeALT_CloseTime = CH + ":" + CM + ":" + CS + ":" + CAM + ":" + COutH + ":" + COutM;

                for (i = 0; i < dtEmpInfo.Rows.Count; i++)
                {
                    if (dtEmpInfo.Rows[i]["EId"].ToString() == "20417")
                    {

                    }
                    if (!CompareAdmissionDateAndIndate(i)) // check Admission date and attendance date
                    {
                        if (!ForAllStudents) lblMessage.InnerText = "error->This teacher or Staff is not joined in this date.";
                        return;
                    }
                    else i = j;
                    sqlDB.fillDataTable("select * from AttendanceTransferMedia where AttDate='" + attDate + "' AND Student_Emp_AdmNo=(select ECardNo from EmployeeInfo where EId=" + dtEmpInfo.Rows[i]["EId"].ToString() + ") order by Hur,Min,Sec", dt = new DataTable());



                    if (dt.Rows.Count > 0)
                    {

                        string InHur = (dt.Rows[0]["Hur"].ToString().Trim().Length == 1) ? "0" + dt.Rows[0]["Hur"].ToString().Trim() : dt.Rows[0]["Hur"].ToString().Trim();
                        string InMin = (dt.Rows[0]["Min"].ToString().Trim().Length == 1) ? "0" + dt.Rows[0]["Min"].ToString().Trim() : dt.Rows[0]["Min"].ToString().Trim();
                        string InSec = (dt.Rows[0]["Sec"].ToString().Trim().Length == 1) ? "0" + dt.Rows[0]["Sec"].ToString().Trim() : dt.Rows[0]["Sec"].ToString().Trim();

                        string OutHur = (dt.Rows[dt.Rows.Count - 1]["Hur"].ToString().Trim().Length == 1) ? "0" + dt.Rows[dt.Rows.Count - 1]["Hur"].ToString().Trim() : dt.Rows[dt.Rows.Count - 1]["Hur"].ToString().Trim();
                        string OutMin = (dt.Rows[dt.Rows.Count - 1]["Min"].ToString().Trim().Length == 1) ? "0" + dt.Rows[dt.Rows.Count - 1]["Min"].ToString().Trim() : dt.Rows[dt.Rows.Count - 1]["Min"].ToString().Trim();
                        string OutSec = (dt.Rows[dt.Rows.Count - 1]["Sec"].ToString().Trim().Length == 1) ? "0" + dt.Rows[dt.Rows.Count - 1]["Sec"].ToString().Trim() : dt.Rows[dt.Rows.Count - 1]["Sec"].ToString().Trim();


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



                            char attStatus = (isPresent) ? 'p' : 'l';

                            string[] getColumns = { "EId", "AttDate", "InHur", "InMin", "InSec", "OutHur", "OutMin", "OutSec", "AttStatus", "StateStatus", "DailyStartTimeALT_CloseTime" };
                            string[] getValues = { dtEmpInfo.Rows[i]["EId"].ToString(), convertDateTime.getCertainCulture(selectedDate).ToString(), InHur, InMin, InSec, OutHur, OutMin, OutSec, attStatus.ToString(), "Present", DailyStartTimeALT_CloseTime };
                            SQLOperation.forSaveValue("DailyAttendanceRecord", getColumns, getValues, sqlDB.connection);


                        }
                    }
                    
                    else
                    {
                        string AttStatus = "a", StateStatus = "Absent";

                        DataTable dtLeaveInfo=new DataTable ();
                        sqlDB.fillDataTable("select LACode From Leave_Application_Details where EID=" + dtEmpInfo.Rows[i]["EId"].ToString() + " AND LeaveDate = '"+attDate+"'",dtLeaveInfo);
                        if (dtLeaveInfo.Rows.Count > 0)
                        {
                            System.Data.SqlClient.SqlCommand cmd;

                            sqlDB.fillDataTable("select FORMAT(ToDate,'dd-MM-yyyy') as ToDate,LeaveId,LeaveName from v_Leave_Application where LACode=" + dtLeaveInfo.Rows[0]["LACode"].ToString() + "", dt = new DataTable());
                            if (selectedDate.Equals(dt.Rows[0]["ToDate"].ToString()))
                            {
                                
                                cmd= new System.Data.SqlClient.SqlCommand("Update Leave_Application set IsProcessessed='0' where LACode= " + dt.Rows[0]["LACode"].ToString() + "", sqlDB.connection);
                                cmd.ExecuteNonQuery();
                                
                            }

                            cmd = new System.Data.SqlClient.SqlCommand("Update Leave_Application_Details set used='1' where LeaveDate='" + attDate + "'", sqlDB.connection);
                            cmd.ExecuteNonQuery();

                            AttStatus = "lv"; StateStatus = dt.Rows[0]["LeaveName"].ToString();
                        }

                        

                        string[] getColumns = { "EId", "AttDate", "InHur", "InMin", "InSec", "OutHur", "OutMin", "OutSec", "AttStatus", "StateStatus", "DailyStartTimeALT_CloseTime" };
                        string[] getValues = { dtEmpInfo.Rows[i]["EId"].ToString(), convertDateTime.getCertainCulture(selectedDate).ToString(), "00", "00", "00", "00", "00", "00",AttStatus,StateStatus, "00:00:00:00:00:00" };
                        SQLOperation.forSaveValue("DailyAttendanceRecord", getColumns, getValues, sqlDB.connection);
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
                lblMessage.InnerText ="error->"+ ex.Message;
            }
        }



        private void setWeekend_Others_Holyday(string attDate, string purpose)
        {
            try
            {
                SQLOperation.forDeleteRecordByIdentifier("DailyAttendanceRecord", "AttDate", attDate, sqlDB.connection);
                string attStatus = (purpose.Trim().Equals("Weekly Holiday")) ? "w" : "h";
                for (i = 0; i < dtEmpInfo.Rows.Count; i++)
                {
                    if (!CompareAdmissionDateAndIndate(i)) return; // // check Admission date and attendance date
                    string[] getColumns = { "EID", "AttDate", "InHur", "InMin", "InSec", "OutHur", "OutMin", "OutSec", "AttStatus", "StateStatus", "DailyStartTimeALT_CloseTime" };
                    string[] getValues = { dtEmpInfo.Rows[i]["EId"].ToString(), convertDateTime.getCertainCulture(txtAttendanceDate.Text.Trim()).ToString(), "00", "00", "00", "00", "00", "00", attStatus, purpose, "00:00:00:00:00:00" };
                    SQLOperation.forSaveValue("DailyAttendanceRecord", getColumns, getValues, sqlDB.connection);

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
            selectedDate = txtPartialAttDate.Text;
            lblMessage.InnerText = "";
            dtEmpInfo = new DataTable();
            string attDate = txtPartialAttDate.Text.Trim().Substring(6, 4) + "-" + txtPartialAttDate.Text.Trim().Substring(3, 2) + "-" + txtPartialAttDate.Text.Trim().Substring(0, 2);
            sqlDB.fillDataTable("select Distinct EId,Format(EJoiningDate,'dd-MM-yyyy') as EJoiningDate from EmployeeInfo where ECardNo=" + txtCardNo.Text.Trim() + " AND IsActive='True' ", dtEmpInfo);

            //transfer al student info for set weekend or others holydays
            sqlDB.fillDataTable("select Convert(varchar(11),OffDate,105) as OffDate,Purpose from OffdaySettings where OffDate='" + attDate + "'", dt = new DataTable());

            if (dt.Rows.Count > 0)
            {
                setWeekend_Others_Holyday(attDate, dt.Rows[0]["Purpose"].ToString());
                return;
            }

            // Intermediat media for transfer all attendance record
            setDailyAttendanceInTransferMedia(attDate, false);

            // Method calling for set daily attendance as record
            setDailyAttendanceRecord(attDate, false);
        }

       
    }
}