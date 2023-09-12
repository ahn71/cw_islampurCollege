using DS.BLL.Admission;
using DS.BLL.Attendace;
using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Adviser
{
    public partial class AdviserWiseStdAttDetails : System.Web.UI.Page
    {
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {                   
                    txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    txtMonthName.Text = DateTime.Now.ToString("MMMM-yyyy");
                    txtFdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    txtTdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                     string[] query = Request.QueryString["ID"].ToString().Split('-');
                     ViewState["__ReportStatus__"] = query[0];
                     ViewState["__ID__"] = query[1];
                     if (query[0].Equals("Std"))
                     {
                         LoadFineAmount();
                     }                   
            }
            lblMessage.InnerText = "";
        }
        private void LoadFineAmount()
        {
            try
            {
                AttendanceFineEntry afine = new AttendanceFineEntry();
                dt = afine.LoadAbsentFine(ViewState["__ID__"].ToString());
                if (dt.Rows.Count > 0)
                {
                    lblFineAmount.Text = "Your Current Absent Fine " + dt.Rows[0]["AbsentFine"].ToString() + " TK";
                }
            }
            catch { }
        }

        protected void btnPrint_D_Click(object sender, EventArgs e)
        {
            LoadDailyAttendanceReportData();
        }
        private void LoadDailyAttendanceReportData()
        {
            try
            {
                string sqlCmd = "";
                string ReportTitel = "";
                string ReportType = "";
                DataTable dt = new DataTable();
                if (rblReportType.SelectedIndex == 0) // Daily Attendance Status
                {
                    if (ViewState["__ReportStatus__"].ToString() == "Std")
                    {
                        sqlCmd = "select StudentId, FullName,RollNo,AttStatus,StateStatus,inHur,InMin,ShiftId,ShiftName,ClsSecId,SectionName,ClsGrpId,GroupName,BatchId,BatchName,FORMAT(AttDate,'MM-yyyy') as Month ,FORMAT(AttDate,'dd-MM-yyyy') as AttDate from v_DailyAttendanceRecordForReport where "
                    + "StudentId='" + ViewState["__ID__"].ToString() + "' and Convert(datetime,AttDate,105) between "
                    + "Convert(datetime,'" + txtDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105) ORDER BY FORMAT(AttDate,'MM-yyyy')";
                    }
                    else
                    {
                        sqlCmd = "select StudentId, FullName,RollNo,AttStatus,StateStatus,inHur,InMin,OutHur,OutMin,ShiftId,ShiftName,ClsSecId,SectionName,ClsGrpId,GroupName,BatchId,BatchName,FORMAT(AttDate,'MM-yyyy') as Month,FORMAT(AttDate,'dd-MM-yyyy') as AttDate from v_DailyAttendanceRecordForReport where "
                        + "StudentId in (SELECT StudentId FROM tbl_Guide_Teacher WHERE EID='" + ViewState["__ID__"].ToString() + "') and Convert(datetime,AttDate,105) between "
                        + "Convert(datetime,'" + txtDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105) ORDER BY AttDate";
                    }
                    ReportTitel = "Daily Attendance Status";
                    ReportType = "Status";
                }
                else
                {
                    if (ViewState["__ReportStatus__"].ToString() == "Std")
                    {
                        sqlCmd = "select StudentId, FullName,RollNo,AttStatus,StateStatus,inHur,InMin,OutHur,OutMin,ShiftId,ShiftName,ClsSecId,SectionName,ClsGrpId,GroupName,BatchId,BatchName,FORMAT(AttDate,'MM-yyyy') as Month ,FORMAT(AttDate,'dd-MM-yyyy') as AttDate from v_DailyAttendanceRecordForReport where  StudentId='" + ViewState["__ID__"].ToString() + "' and Convert(datetime,AttDate,105) between "
                        + "Convert(datetime,'" + txtDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105) ORDER BY AttDate";
                    }
                    else
                    {
                        sqlCmd = "select StudentId, FullName,RollNo,AttStatus,StateStatus,inHur,InMin,OutHur,OutMin,ShiftId,ShiftName,ClsSecId,SectionName,ClsGrpId,GroupName,BatchId,BatchName,FORMAT(AttDate,'MM-yyyy') as Month ,FORMAT(AttDate,'dd-MM-yyyy') as AttDate  from v_DailyAttendanceRecordForReport where  StudentId in (SELECT StudentId FROM tbl_Guide_Teacher WHERE EID='" + ViewState["__ID__"].ToString() + "') and Convert(datetime,AttDate,105) between "
                        + "Convert(datetime,'" + txtDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105) ORDER BY AttDate";
                    }
                    ReportTitel = "Daily Log In Out Time";
                    ReportType = "InOut";
                }
                //sqlDB.fillDataTable(sqlCmd, dt = new DataTable());
                dt = CRUD.ReturnTableNull(sqlCmd);
                if (dt.Rows.Count < 1)
                {
                    lblMessage.InnerText = "warning-> Any " + rblReportType.SelectedItem.Text + " record are not founded"; return;
                }
                Session["__DailyAttendance__"] = dt;               
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=StudentDailyAttendance- - - - -" + txtDate.Text + "-" + ReportTitel + "-" + ReportType + "-" + txtToDate.Text + "');", true);
            }
            catch { }

        }

        protected void rblRepotMontly_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblRepotMontly.SelectedValue == "0")
            {
                tblDateRange.Visible = false;
                tdMonth.Visible = true;
                tdMonth.InnerText = "Month";
                txtMonthName.Visible = true;
            }
            else if (rblRepotMontly.SelectedValue == "1")
            {
                tblDateRange.Visible = true;
                tdMonth.Visible = false;
                txtMonthName.Visible = false;
            }
            else
            {
                tblDateRange.Visible = true;
                tdMonth.Visible = false;
                txtMonthName.Visible = false;
            }
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            if (rblRepotMontly.SelectedValue == "0") // For Monthly Attendance Sheet Report
            {


                dt = new DataTable();
                if (ViewState["__ReportStatus__"].ToString() == "Std")
                {
                    dt = ForAttendanceReport.returntDataTableForAttSheet(ViewState["__ID__"].ToString(), txtMonthName.Text);
                }
                else
                {
                    dt = ForAttendanceReport.returntDataTableForAttSheetAdviser(ViewState["__ID__"].ToString(), txtMonthName.Text);
                }
                if (dt == null || dt.Rows.Count < 1)
                {
                    lblMessage.InnerText = "warning-> Any attendance Record are not founded!";
                    return;
                }

                Session["__AttendanceSheet__"] = dt;               
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=StdAttendanceSheet- - - - -" + txtMonthName.Text + "');", true);
                //Open New Tab for Sever side code 
            }
            else if (rblRepotMontly.SelectedValue == "1")
            {
                if (txtFdate.Text.Length == 0) { lblMessage.InnerText = "warning->Please select from date"; txtFdate.Focus(); return; }
                if (txtTdate.Text.Length == 0) { lblMessage.InnerText = "warning->Please select to date"; txtTdate.Focus(); return; }
                dt = new DataTable();
                if (ViewState["__ReportStatus__"].ToString() == "Std")
                {
                    dt = ForAttendanceReport.return_dt_for_AttSummary(txtFdate.Text.Trim(), txtTdate.Text.Trim(), ViewState["__ID__"].ToString());
                }
                else
                {
                    dt = ForAttendanceReport.return_dt_for_AttSummaryAdviserWise(txtFdate.Text.Trim(), txtTdate.Text.Trim(), ViewState["__ID__"].ToString());
                }
                if (dt == null || dt.Rows.Count < 1)
                {
                    lblMessage.InnerText = "warning-> Any attendance Record are not founded!";
                    return;
                }
                Session["__AttendanceSummaryReport__"] = dt;              
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=StdAttendanceSummaryReport- - - - - " + txtFdate.Text.Trim() + "-" + txtTdate.Text.Trim() + "');", true);
            }
            else
            {
                if (txtFdate.Text.Length == 0) { lblMessage.InnerText = "warning->Please select from date"; txtFdate.Focus(); return; }
                if (txtTdate.Text.Length == 0) { lblMessage.InnerText = "warning->Please select to date"; txtTdate.Focus(); return; }

                dt = new DataTable();
                if (ViewState["__ReportStatus__"].ToString() == "Std")
                {
                    dt = ForAttendanceReport.return_dt_AbsentDetails(txtFdate.Text, txtTdate.Text, "", "", "", ViewState["__ID__"].ToString());
                }
                else
                {
                    dt = ForAttendanceReport.return_dt_AbsentDetailsAdviserWise(txtFdate.Text, txtTdate.Text, "", "", "", ViewState["__ID__"].ToString());
                }
                if (dt == null || dt.Rows.Count < 1)
                {
                    lblMessage.InnerText = "warning->  Absent records are not available";
                    return;
                }
                Session["__AbsentDetails__"] = dt;                //...............

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=StdIndivisualAbsentDetails- - - - " + txtFdate.Text.Trim() + "-" + txtTdate.Text.Trim() + "--');", true);  //Open New Tab for Sever side code
            }
        }
    }
}