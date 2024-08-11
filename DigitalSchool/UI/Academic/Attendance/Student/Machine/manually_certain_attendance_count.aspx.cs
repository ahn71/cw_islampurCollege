using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using ComplexScriptingSystem;
using System.Data;
using System.Data.SqlClient;
using DS.BLL.ControlPanel;

namespace DS.UI.Academics.Attendance.Student.Machine
{
    public partial class manually_certain_attendance_count : System.Web.UI.Page
    {
        DataTable dt; DataTable dtStudentInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    trToDate.Visible = false;
                    tdFromDate.InnerText = "Date";
                    
                }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                sqlDB.fillDataTable("select StudentId,Format(AdmissionDate,'dd-MM-yyyy') as AdmissionDate from CurrentStudentInfo where AdmissionNo=" 
                + txtECardNo.Text.Trim() + "", dtStudentInfo = new DataTable());
                AttendanceCount();
            }
            catch { }
        }

        private void AttendanceCount()
        {
            string[] FromDates = txtFromDate.Text.Trim().Split('-');

            if (!rblAttendanceCountType.SelectedValue.ToString().Equals("Single"))
            {
                if (txtToDate.Text.Trim().Length < 8)
                {
                    lblMessage.InnerText = "error->Please select to date"; txtToDate.Focus(); return;
                }

                string[] ToDates = txtToDate.Text.Trim().Split('-');
                DateTime FromDate = new DateTime(int.Parse(FromDates[2]), int.Parse(FromDates[1]), int.Parse(FromDates[0]));
                DateTime ToDate = new DateTime(int.Parse(ToDates[2]), int.Parse(ToDates[1]), int.Parse(ToDates[0]));

                if (!CompareJoiningDateAndIndate((byte)0, txtFromDate.Text.Trim())) return;  // just test attendance date and admission date 

                while (FromDate <= ToDate)
                {
                    string AttStatus = ddlAttendanceTemplate.SelectedValue.ToString().Trim(), StateStatus = ddlAttendanceTemplate.SelectedItem.ToString().Trim();
                    string[] LeaveDates = FromDate.ToString().Split('/');


                    // for find weekend or holyday
                    sqlDB.fillDataTable("select Format(OffDate,'dd-MM-yyyy') as OffDate,Purpose from OffdaySettings where OffDate='" 
                    + LeaveDates[2].Substring(0, 4) + "-" + LeaveDates[0] + "-" + LeaveDates[1] + "'", dt = new DataTable());
                    if (dt.Rows.Count > 0)
                    {
                        AttStatus = (dt.Rows[0]["PurPose"].ToString().Equals("Weekly Holiday")) ? "w" : "h";
                        StateStatus = dt.Rows[0]["PurPose"].ToString();
                    }
                    if (FromDate == ToDate) saveAttendance(LeaveDates[2].Substring(0, 4) + "-" + LeaveDates[0] + "-" + LeaveDates[1], AttStatus, StateStatus, true);
                    else saveAttendance(LeaveDates[2].Substring(0, 4) + "-" + LeaveDates[0] + "-" + LeaveDates[1], AttStatus, StateStatus,false);
                    FromDate = FromDate.AddDays(1);
                }

            }
            else
            {
                string AttStatus = ddlAttendanceTemplate.SelectedValue.ToString().Trim(), StateStatus = ddlAttendanceTemplate.SelectedItem.ToString().Trim();

                if (!CompareJoiningDateAndIndate((byte)0, txtFromDate.Text.Trim())) return;

                // for find weekend or holyday
                sqlDB.fillDataTable("select Format(OffDate,'dd-MM-yyyy') as OffDate,Purpose from OffdaySettings where OffDate='" + FromDates[2] 
                + "-" + FromDates[1] + "-" + FromDates[1] + "'", dt = new DataTable());
                if (dt.Rows.Count > 0)
                {
                    AttStatus = (dt.Rows[0]["PurPose"].ToString().Equals("Weekly Holiday")) ? "w" : "h";
                    StateStatus = dt.Rows[0]["PurPose"].ToString();
                }
                saveAttendance(txtFromDate.Text.Trim(), AttStatus, StateStatus,true);
            }
        }

        private void saveAttendance(string AttDate, string AttStatus, string StateStatus, bool getLastMoment)
        {
            txtInHur.Text = (txtInHur.Text.Trim().Length < 2) ? "0" + txtInHur.Text.Trim() : txtInHur.Text.Trim();
            txtInMin.Text = (txtInMin.Text.Trim().Length < 2) ? "0" + txtInMin.Text.Trim() : txtInMin.Text.Trim();
            txtInSec.Text = (txtInSec.Text.Trim().Length < 2) ? "0" + txtInSec.Text.Trim() : txtInSec.Text.Trim();

            txtOutHur.Text = (txtOutHur.Text.Trim().Length < 2) ? "0" + txtOutHur.Text.Trim() : txtOutHur.Text.Trim();
            txtOutMin.Text = (txtOutMin.Text.Trim().Length < 2) ? "0" + txtOutMin.Text.Trim() : txtOutMin.Text.Trim();
            txtOutSec.Text = (txtOutSec.Text.Trim().Length < 2) ? "0" + txtOutSec.Text.Trim() : txtOutSec.Text.Trim();

            string DailyStartTimeALT_CloseTime = txtInHur.Text.Trim() + ":" + txtInMin.Text.Trim() + ":" + txtInSec.Text.Trim() 
            + ":" + txtOutHur.Text.Trim() + ":" + txtOutMin.Text.Trim() + ":" + txtOutSec.Text.Trim();
            deleteExistsAttRecod(AttDate, dtStudentInfo.Rows[0]["StudentId"].ToString());  // for delete exists attendance record 
            try
            {
                string[] getColumns = { "StudentId", "AttDate", "InHur", "InMin", "InSec", "OutHur", "OutMin", "OutSec", 
                "AttStatus", "StateStatus", "DailyStartTimeALT_CloseTime", "AttManual" };
                string[] getValues = { dtStudentInfo.Rows[0]["StudentId"].ToString(), convertDateTime.getCertainCulture(AttDate).ToString(), 
                txtInHur.Text, txtInMin.Text, txtInSec.Text, txtOutHur.Text, txtOutMin.Text, txtOutSec.Text, AttStatus, StateStatus, 
                DailyStartTimeALT_CloseTime, "Manual Attendance" };
                if (SQLOperation.forSaveValue("DailyAttendanceRecord", getColumns, getValues, sqlDB.connection))
                {
                    lblMessage.InnerText = "success->Successfully Attendance Counted";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "ClearInputBox();", true);
                }

            }
            catch { }
        }

        private bool CompareJoiningDateAndIndate(byte i, string AttDate)
        {
            try
            {
                DateTime InDate = new DateTime(int.Parse(AttDate.Trim().Substring(6, 4)), int.Parse(AttDate.Trim().Substring(3, 2)), 
                int.Parse(AttDate.Trim().Substring(0, 2)));
                DateTime AdmissionDate = new DateTime(int.Parse(dtStudentInfo.Rows[i]["AdmissionDate"].ToString().Substring(6, 4)), 
                int.Parse(dtStudentInfo.Rows[i]["AdmissionDate"].ToString().Substring(3, 2)), 
                int.Parse(dtStudentInfo.Rows[i]["AdmissionDate"].ToString().Substring(0, 2)));

                if (InDate >= AdmissionDate) return true;
                else
                {
                    lblMessage.InnerText = "error->This student is not admitted on this date.";
                    return false;
                }
            }
            catch { return false; }
        }

        private void deleteExistsAttRecod(string AttDate, string StudentId)
        {
            try
            {
                string[] getAttDate = AttDate.Split('-');
                getAttDate[0] = getAttDate[2] + "-" + getAttDate[1] + "-" + getAttDate[0];
                SqlCommand cmd = new SqlCommand("delete from DailyAttendanceRecord where AttDate='" 
                + getAttDate[0] + "' AND StudentId=" + StudentId + "", sqlDB.connection);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }

        protected void rblAttendanceCountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblAttendanceCountType.SelectedIndex == 0)
            {
                tdFromDate.InnerText = "Date";
                trToDate.Visible = false;
            }
            else
            {
                tdFromDate.InnerText = "From Date";
                trToDate.Visible = true;
            }
        }
    }
}