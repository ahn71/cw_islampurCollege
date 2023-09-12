using DS.DAL.AdviitDAL;
using DS.DAL.ComplexScripting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academics.Attendance.StafforFaculty.Machine
{
    public partial class manually_certain_emp_attendance_count : System.Web.UI.Page
    {
        DataTable dt; DataTable dtEmpInfo;
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
                sqlDB.fillDataTable("select EID,Format(EJoiningDate,'dd-MM-yyyy') as EJoiningDate from EmployeeInfo where ECardNo=" + txtECardNo.Text.Trim() + "", dtEmpInfo = new DataTable());
                AttendanceCount();
            }
            catch { }
        }

        private void AttendanceCount()
        {
            string[] FromDates = txtFromDate.Text.Trim().Split('-');

            if (!rblAttendanceCountType.SelectedValue.ToString().Equals("Single"))   // for count multiple date type attendance
            {
                if (txtToDate.Text.Trim().Length < 8)
                {
                    lblMessage.InnerText = "error->Please select to date"; txtToDate.Focus(); return;
                }

                string[] ToDates = txtToDate.Text.Trim().Split('-');
                DateTime FromDate = new DateTime(int.Parse(FromDates[2]), int.Parse(FromDates[1]), int.Parse(FromDates[0]));
                DateTime ToDate = new DateTime(int.Parse(ToDates[2]), int.Parse(ToDates[1]), int.Parse(ToDates[0]));

                if (!CompareJoiningDateAndIndate((byte)0, txtFromDate.Text.Trim())) return;
                while (FromDate <= ToDate)
                {
                    string AttStatus = ddlAttendanceTemplate.SelectedValue.ToString().Trim(), StateStatus = ddlAttendanceTemplate.SelectedItem.ToString().Trim();
                    string[] LeaveDates = FromDate.ToString().Split('/');

                    // for test any leave is exists of selected date

                    sqlDB.fillDataTable("Select * from Leave_Application_Details where LeaveDate ='" + LeaveDates[2].Substring(0, 4) + "-" + LeaveDates[0] + "-" + LeaveDates[1] + " '", dt = new DataTable());
                    if (dt.Rows.Count < 0)
                    {
                        AttStatus = "lv"; StateStatus = dt.Rows[0]["LeaveName"].ToString();
                    }
                    else
                    {
                        // for test any weekend or holyday is exists of selected date
                        sqlDB.fillDataTable("select Format(OffDate,'dd-MM-yyyy') as OffDate,Purpose from OffdaySettings where OffDate='" + LeaveDates[2].Substring(0, 4) + "-" + LeaveDates[0] + "-" + LeaveDates[1] + "'", dt = new DataTable());
                        if (dt.Rows.Count > 0)
                        {
                            AttStatus = (dt.Rows[0]["PurPose"].ToString().Equals("Weekly Holiday")) ? "w" : "h";
                            StateStatus = dt.Rows[0]["PurPose"].ToString();
                        }
                    }
                    if (FromDate == ToDate) saveAttendance(LeaveDates[2].Substring(0, 4) + "-" + LeaveDates[0] + "-" + LeaveDates[1], AttStatus, StateStatus,true);
                    else saveAttendance(LeaveDates[2].Substring(0, 4) + "-" + LeaveDates[0] + "-" + LeaveDates[1], AttStatus, StateStatus,false);
                    FromDate = FromDate.AddDays(1);
                }

            }
            else
            {
                string AttStatus = ddlAttendanceTemplate.SelectedValue.ToString().Trim(), StateStatus = ddlAttendanceTemplate.SelectedItem.ToString().Trim();

                if (!CompareJoiningDateAndIndate((byte)0, txtFromDate.Text.Trim())) return;

                // for test any leave is exists of selected date

                sqlDB.fillDataTable("Select * from Leave_Application_Details where LeaveDate ='" + FromDates[2] + "-" + FromDates[1] + "-" + FromDates[0] + " '", dt = new DataTable());
                if (dt.Rows.Count > 0)
                {
                    AttStatus = "lv"; StateStatus = dt.Rows[0]["LeaveName"].ToString();
                }
                else
                {
                    // for test any weekend or holyday is exists of selected date
                    sqlDB.fillDataTable("select Format(OffDate,'dd-MM-yyyy') as OffDate,Purpose from OffdaySettings where OffDate='" + FromDates[2] + "-" + FromDates[1] + "-" + FromDates[0] + "'", dt = new DataTable());
                    if (dt.Rows.Count > 0)
                    {
                        AttStatus = (dt.Rows[0]["PurPose"].ToString().Equals("Weekly Holiday")) ? "w" : "h";
                        StateStatus = dt.Rows[0]["PurPose"].ToString();
                    }
                }
                saveAttendance(txtFromDate.Text.Trim(), AttStatus, StateStatus,true);
            }
        }

        private void saveAttendance(string AttDate, string AttStatus, string StateStatus,bool getLastMoment)  // getLastMoment for multiple date 
        {
            txtInHur.Text = (txtInHur.Text.Trim().Length < 2) ? "0" + txtInHur.Text.Trim() : txtInHur.Text.Trim();
            txtInMin.Text = (txtInMin.Text.Trim().Length < 2) ? "0" + txtInMin.Text.Trim() : txtInMin.Text.Trim();
            txtInSec.Text = (txtInSec.Text.Trim().Length < 2) ? "0" + txtInSec.Text.Trim() : txtInSec.Text.Trim();

            txtOutHur.Text = (txtOutHur.Text.Trim().Length < 2) ? "0" + txtOutHur.Text.Trim() : txtOutHur.Text.Trim();
            txtOutMin.Text = (txtOutMin.Text.Trim().Length < 2) ? "0" + txtOutMin.Text.Trim() : txtOutMin.Text.Trim();
            txtOutSec.Text = (txtOutSec.Text.Trim().Length < 2) ? "0" + txtOutSec.Text.Trim() : txtOutSec.Text.Trim();

            string DailyStartTimeALT_CloseTime = txtInHur.Text.Trim() + ":" + txtInMin.Text.Trim() + ":" + txtInSec.Text.Trim() + ":" + txtOutHur.Text.Trim() + ":" + txtOutMin.Text.Trim() + ":" + txtOutSec.Text.Trim();

            deleteExistsAttRecod(AttDate,dtEmpInfo.Rows[0]["EID"].ToString());  // for delete exists attendance record 

            if (txtInHur.Text.Trim() == txtOutHur.Text.Trim())
            {
                txtOutHur.Text = "00"; txtOutMin.Text = "00"; txtOutSec.Text = "00";
            }
            try
            {
                string[] getColumns = { "EID", "AttDate", "InHur", "InMin", "InSec", "OutHur", "OutMin", "OutSec", "AttStatus", "StateStatus", "DailyStartTimeALT_CloseTime", "AttManual" };
                string[] getValues = { dtEmpInfo.Rows[0]["EID"].ToString(), convertDateTime.getCertainCulture(AttDate).ToString(), txtInHur.Text, txtInMin.Text, txtInSec.Text, txtOutHur.Text, txtOutMin.Text, txtOutSec.Text, AttStatus, StateStatus, DailyStartTimeALT_CloseTime, "Manual Attendance" };
                if (SQLOperation.forSaveValue("DailyAttendanceRecord", getColumns, getValues, sqlDB.connection))
                {
                    if (getLastMoment)
                    {
                        lblMessage.InnerText = "success->Successfully Attendance Counted";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "ClearInputBox();", true);
                    }
                }

            }
            catch { }
        }

        private void deleteExistsAttRecod(string AttDate,string EId)
        {
            try
            {
                string [] getAttDate = AttDate.Split('-');
                getAttDate[0]=getAttDate[2]+"-"+getAttDate[1]+"-"+getAttDate[0];
                SqlCommand cmd = new SqlCommand("delete from DailyAttendanceRecord where AttDate='" + getAttDate[0]+ "' AND EId=" + EId + "", sqlDB.connection);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }

        private bool CompareJoiningDateAndIndate(byte i, string AttDate)
        {
            try
            {
                DateTime InDate = new DateTime(int.Parse(AttDate.Trim().Substring(6, 4)), int.Parse(AttDate.Trim().Substring(3, 2)), int.Parse(AttDate.Trim().Substring(0, 2)));
                DateTime AdmissionDate = new DateTime(int.Parse(dtEmpInfo.Rows[i]["EJoiningDate"].ToString().Substring(6, 4)), int.Parse(dtEmpInfo.Rows[i]["EJoiningDate"].ToString().Substring(3, 2)), int.Parse(dtEmpInfo.Rows[i]["EJoiningDate"].ToString().Substring(0, 2)));

                if (InDate >= AdmissionDate) return true;
                else
                {
                    lblMessage.InnerText = "error->This student is not admitted on this date.";
                    return false;
                }
            }
            catch { return false; }
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