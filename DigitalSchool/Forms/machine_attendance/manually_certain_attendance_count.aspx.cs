using DS.DAL.AdviitDAL;
using ComplexScriptingSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Forms.machine_attendance
{
    public partial class manually_certain_attendance_count : System.Web.UI.Page
    {
        DataTable dt;
        DataTable dtStudentInfo;
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
                    //   Classes.commonTask.loadShift(ddlShift);
                }
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            loadPersonalInfo_ForCountSingleAttendance();
        }

        private void loadPersonalInfo_ForCountSingleAttendance()
        {
            try
            {
                sqlDB.fillDataTable("select StudentId,Format(AdmissionDate,'dd-MM-yyyy') as AdmissionDate from StudentProfile where AdmissionNo=" + txtAdmissionNo.Text.Trim() + "", dtStudentInfo = new DataTable());
                if (!CompareAdmissionDateAndIndate((byte)0)) return;
                saveSingleAttendance();
                
            }
            catch { }
        }

       
        private bool CompareAdmissionDateAndIndate(byte i)
        {
            try
            {
                DateTime InDate = new DateTime(int.Parse(txtAttendanceDate.Text.Trim().Substring(6, 4)), int.Parse(txtAttendanceDate.Text.Trim().Substring(3, 2)), int.Parse(txtAttendanceDate.Text.Trim().Substring(0, 2)));
                DateTime AdmissionDate = new DateTime(int.Parse(dtStudentInfo.Rows[i]["AdmissionDate"].ToString().Substring(6, 4)), int.Parse(dtStudentInfo.Rows[i]["AdmissionDate"].ToString().Substring(3, 2)), int.Parse(dtStudentInfo.Rows[i]["AdmissionDate"].ToString().Substring(0, 2)));

                if (InDate >= AdmissionDate) return true;
                else
                {
                    lblMessage.InnerText = "error->This student is not admitted on this date.";
                    return false;
                }
            }
            catch { return false; }
        }

        private void saveSingleAttendance()
        {
            
            txtInHur.Text = (txtInHur.Text.Trim().Length < 2) ? "0" + txtInHur.Text.Trim() : txtInHur.Text.Trim();
            txtInMin.Text = (txtInMin.Text.Trim().Length < 2) ? "0" + txtInMin.Text.Trim() : txtInMin.Text.Trim();
            txtInSec.Text = (txtInSec.Text.Trim().Length < 2) ? "0" + txtInSec.Text.Trim() : txtInSec.Text.Trim();

            txtOutHur.Text = (txtOutHur.Text.Trim().Length < 2) ? "0" + txtOutHur.Text.Trim() : txtOutHur.Text.Trim();
            txtOutMin.Text = (txtOutMin.Text.Trim().Length < 2) ? "0" + txtOutMin.Text.Trim() : txtOutMin.Text.Trim();
            txtOutSec.Text = (txtOutSec.Text.Trim().Length < 2) ? "0" + txtOutSec.Text.Trim() : txtOutSec.Text.Trim();

            string DailyStartTimeALT_CloseTime = txtInHur.Text.Trim() + ":" + txtInMin.Text.Trim() + ":" + txtInSec.Text.Trim() + ":" + txtOutHur.Text.Trim() + ":" + txtOutMin.Text.Trim() + ":" + txtOutSec.Text.Trim();
            
            try
            {
                string[] getColumns = { "StudentId", "AttDate", "InHur", "InMin", "InSec", "OutHur", "OutMin", "OutSec", "AttStatus", "StateStatus", "DailyStartTimeALT_CloseTime", "AttManual" };
                string[] getValues = { dtStudentInfo.Rows[0]["StudentId"].ToString(), convertDateTime.getCertainCulture(txtAttendanceDate.Text.Trim()).ToString(), txtInHur.Text, txtInMin.Text, txtInSec.Text, txtOutHur.Text.Trim(), txtOutMin.Text.Trim(), txtOutSec.Text.Trim(), ddlAttendanceTemplate.SelectedValue.ToString(), ddlAttendanceTemplate.SelectedItem.ToString(), DailyStartTimeALT_CloseTime, "Manual Attendance" };
                if (SQLOperation.forSaveValue("DailyAttendanceRecord", getColumns, getValues, sqlDB.connection))
                {
                    lblMessage.InnerText = "success->Successfully Attendance Counted";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "ClearInputBox();", true);
                }
                            
            }
            catch { }
        }
    }
}