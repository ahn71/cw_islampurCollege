using DS.BLL.Attendace;
using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Adviser
{
    public partial class AdviserAttendance : System.Web.UI.Page
    {
        DataTable dt; //= new DataTable();
        string DepartmentList = "";
        string DesignationList = "";
        string Emptype = "Faculty";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMessage.InnerText = "";
                    if (!IsPostBack)
                    {                       
                        txtToDate.Text = txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy");                      
                        SheetInfoEntry.loadMonths(ddlMonth);                   
                    }
            }
            catch { }
            lblMessage.InnerText = "";
        }
      
        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                //  if (ddlShiftList.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Shift!"; ddlShiftList.Focus(); return; }
             
                if (rblRepotMontly.SelectedValue == "0")
                {
                    //----------validation-------------------------------                    
                    if (ddlMonth.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Month!"; ddlMonth.Focus(); return; }
                    //-----------------------------------------------------

                 
                    dt = new DataTable();
                    dt = ForAttendanceReport.return_dt_EmpAttSheet(ddlMonth.SelectedValue,Session["__EID__"].ToString());
                    if (dt == null || dt.Rows.Count < 1)
                    {
                        lblMessage.InnerText = "warning-> Any attendance records are not founded!";
                        return;
                    }
                    Session["__EmpAttendanceSheet__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=EmpAttendanceSheet-" + ddlMonth.SelectedItem.Text + "- -" + Emptype + "');", true);
                    //Open New Tab for Sever side code
                }
                else if (rblRepotMontly.SelectedValue == "1")
                {
                    if (txtFdate.Text.Length == 0) { lblMessage.InnerText = "warning->Please select from date"; txtFdate.Focus(); return; }
                    if (txtTdate.Text.Length == 0) { lblMessage.InnerText = "warning->Please select to date"; txtTdate.Focus(); return; }
                   

                    dt = new DataTable();
                    dt = ForAttendanceReport.return_dt_for_EmpAttSummary(txtFdate.Text, txtTdate.Text,Session["__EID__"].ToString());
                    if (dt == null || dt.Rows.Count < 1)
                    {
                        lblMessage.InnerText = "warning-> Any attendance Record are not founded!";
                        return;
                    }
                    Session["__EmpAttendanceSummaryReport__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=EmpAttendanceSummaryReport- -" + txtFdate.Text.Trim() + "-" + txtFdate.Text.Trim() + "-" + Emptype + "');", true);
                }
                else
                {
                    if (txtFdate.Text.Length == 0) { lblMessage.InnerText = "warning->Please select from date"; txtFdate.Focus(); return; }
                    if (txtTdate.Text.Length == 0) { lblMessage.InnerText = "warning->Please select to date"; txtTdate.Focus(); return; }
                  
                    dt = new DataTable();
                    dt = ForAttendanceReport.return_dt_EmpAbsentDetails(txtFdate.Text, txtTdate.Text, Session["__EID__"].ToString());
                    if (dt == null || dt.Rows.Count < 1)
                    {
                        lblMessage.InnerText = "warning->  Absent records are not available";
                        return;
                    }
                    Session["__EmpAbsentDetails__"] = dt;
                    //...............                    
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=IndivisualEmpAbsentDetails-" + txtFdate.Text.Trim() + "-" + txtTdate.Text.Trim() + "- -" + Emptype + "');", true);  //Open New Tab for Sever side code
                }
                // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me","goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=AttendanceSummaryReport-"+""+"');", true);  
                //Open New Tab for Sever side code
            }
            catch { }
        }
        
       
        protected void rblRepotMontly_SelectedIndexChanged(object sender, EventArgs e)
        {         

            if (rblRepotMontly.SelectedValue == "0")
            {
                tblDateRange.Visible = false;
                SheetInfoEntry.loadMonths(ddlMonth);
                ddlMonth.Visible = true;
                tdMonth.Visible = true;
            }
            else if (rblRepotMontly.SelectedValue == "1")
            {
                tblDateRange.Visible = true;
                tdMonth.Visible = false;
                ddlMonth.Visible = false;
            }
            else
            {
                ddlMonth.Items.Clear();
                tblDateRange.Visible = true;
                tdMonth.Visible = false;              
                ddlMonth.Visible = false;
            }
        }
       

     
        private void generatDailyAttendance()
        {

            string ReportTitel = "";
            string ReportType = "";
            string reportType = "";
            // if (ddlShift_D.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Shift!"; ddlShift_D.Focus(); return; }
            if (txtDate.Text == "") { lblMessage.InnerText = "warning-> Please select From Date!"; txtDate.Focus(); return; }
            if (txtToDate.Text == "") { lblMessage.InnerText = "warning-> Please select To Date!"; txtToDate.Focus(); return; }
           
            if (rblReportType.SelectedValue == "0") // Daily Attendance Status
            {
                ReportTitel = "Daily Attendance Status";
                ReportType = "Status";
                reportType = "attendance";
            }
            else if (rblReportType.SelectedValue == "1") // Daily Present status
            {
                ReportTitel = "Daily Present Status";
                ReportType = "PresentAbsent";
                reportType = "present";
            }
            else if (rblReportType.SelectedValue == "2") // Daily Absent Staust
            {
                ReportTitel = "Daily Absent Status";
                ReportType = "PresentAbsent";
                reportType = "absent";
            }
            else
            {
                ReportTitel = "Daily Log In Out Time";
                ReportType = "LogInOut";
                reportType = "Log In-Out";
            }
            dt = new DataTable();
            dt = ForAttendanceReport.return_dt_DailyEmpAttReport(rblReportType.SelectedValue, txtDate.Text.Trim(), txtToDate.Text.Trim(),Session["__EID__"].ToString());
            if (dt == null || dt.Rows.Count < 1)
            {
                lblMessage.InnerText = "warning-> Any " + reportType + " record are not founded";
                return;
            }
            string DateRange = (txtDate.Text == txtToDate.Text) ? txtDate.Text : txtDate.Text + " To " + txtToDate.Text;
            Session["__DailyEmpAttendance__"] = dt;
           // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DailyEmpAttendance-" + ReportTitel + "-" + ReportType + "- -" + Emptype +"-" +"Faculty"+"-"+ DateRange.Replace('-', '/') + "');", true);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DailyEmpAttendance-" + ReportTitel + "-" + ReportType + "- -" + Emptype + "-" + DateRange.Replace('-', '/') + "-Yes');", true);
        }

        protected void btnPrint_D_Click(object sender, EventArgs e)
        {         
            generatDailyAttendance();
        }     
    }
}