using DS.BLL;
using DS.BLL.Attendace;
using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.Attendance
{
    public partial class MonthWiseAttendanceSheetSummary : System.Web.UI.Page
    {
        DataTable dt; //= new DataTable();
        string DepartmentList = "";
        string DesignationList = "";
        string Emptype="";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    if (!IsPostBack)
                    {
                        if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "MonthWiseAttendanceSheetSummary.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                        txtToDate.Text = txtDate.Text = TimeZoneBD.getCurrentTimeBD("dd-MM-yyyy");                   
                        ShiftEntry.GetDropDownList(ddlShift_D);
                        ddlShift_D.Items.RemoveAt(0); ddlShift_D.Items.Insert(0, new ListItem("All", "0"));
                        Classes.commonTask.LoadDeprtmentAtttedence(ddlDepartment_D);
                        Classes.commonTask.LoadDesignation(ddlDesignation_D); 
                        SheetInfoEntry.loadMonths(ddlMonth);
                        ShiftEntry.GetDropDownList(ddlShiftList);
                        ddlShiftList.Items.RemoveAt(0); ddlShiftList.Items.Insert(0, new ListItem("All", "0"));
                        Classes.commonTask.LoadDeprtmentAtttedence(ddlDepartment);
                        Classes.commonTask.LoadDesignation(ddlDesignation);
                        LoadCardNo_D();
                        LoadCardNo();
                    }
                }
            catch { }
            lblMessage.InnerText = "";
        }
      
        private void  LoadAttSummary(string ShiftId, string BatchId,string ClsGrpId,string ClsSecId,string Month)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT DISTINCT dbo.DailyAttendanceRecord.StudentId, dbo.CurrentStudentInfo.FullName , dbo.DailyAttendanceRecord.RollNo," +
                        " SUM(CAST(CASE WHEN ATTStatus = 'p' THEN 1 ELSE 0 END AS int)) AS [Total Present],"+
                        " SUM(CAST(CASE WHEN ATTStatus = 'a' THEN 1 ELSE 0 END AS int)) AS [Total Absent]"+                         
                        " FROM dbo.DailyAttendanceRecord INNER JOIN dbo.CurrentStudentInfo ON"+
                        " dbo.DailyAttendanceRecord.StudentId = dbo.CurrentStudentInfo.StudentId "+
                        " where Format(AttDate,'MM-yyyy')='" + Month + "'and DailyAttendanceRecord.ShiftId='" + ShiftId + "'and DailyAttendanceRecord.BatchId='" + BatchId + "' and DailyAttendanceRecord.ClsGrpId='" + ClsGrpId + "' and DailyAttendanceRecord.ClsSecId='" + ClsSecId + "' " +
                        " GROUP BY dbo.DailyAttendanceRecord.RollNo, dbo.DailyAttendanceRecord.StudentId, dbo.CurrentStudentInfo.FullName";
            sqlDB.fillDataTable(sql, dt);
            dt.Columns.Remove("StudentId");
            gvAttendanceSummary.DataSource = dt;
            gvAttendanceSummary.DataBind();
            if (dt.Rows.Count > 0)
            {
                btnPrintPreview.Enabled = true;
                btnPrintPreview.CssClass = "btn btn-success pull-right";
                Session["__AttendanceSummaryReport__"] = dt;
            }
            else 
            {
                lblMessage.InnerText = "warning-> Any attendance are not founded in this month";
                btnPrintPreview.Enabled = false;
                btnPrintPreview.CssClass = "";
            }
            
           // return dt;
        }
        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
               
                
              //  if (ddlShiftList.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Shift!"; ddlShiftList.Focus(); return; }
                Emptype = (rblEmpType.SelectedValue == "2") ? "Teacher and Staff" : rblEmpType.SelectedItem.Text;
                if (ddlCardNo.SelectedIndex == -1)
                {
                    lblMessage.InnerText = "warning->Any" +Emptype+"'s are not available!";
                    return;
                }
                if (rblRepotMontly.SelectedValue == "0") 
                {
                    //----------validation-------------------------------                    
                    if (ddlMonth.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Month!"; ddlMonth.Focus(); return; }
                    //-----------------------------------------------------

                    if (ddlDepartment.SelectedItem.Text == "All") DepartmentList = GetAlllist(ddlDepartment);
                    else DepartmentList = ddlDepartment.SelectedValue;
                    if (ddlDesignation.SelectedItem.Text == "All") DesignationList = GetAlllist(ddlDesignation);
                    else DepartmentList = ddlDesignation.SelectedValue;
                    dt = new DataTable();
                    dt=ForAttendanceReport.return_dt_EmpAttSheet(DepartmentList,DesignationList,ddlShiftList.SelectedValue, ddlMonth.SelectedValue,rblEmpType.SelectedValue,ddlCardNo.SelectedValue);
                    if (dt == null || dt.Rows.Count < 1)
                    {
                        lblMessage.InnerText = "warning-> Any attendance records are not founded!";
                        return;
                    }
                    Session["__EmpAttendanceSheet__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=EmpAttendanceSheet-" + ddlMonth.SelectedItem.Text + "-" + ddlShiftList.SelectedItem.Text + "-"+Emptype+"');", true);
                    //Open New Tab for Sever side code
                }
                else if (rblRepotMontly.SelectedValue == "1")
                {
                    if (txtFdate.Text.Length == 0) { lblMessage.InnerText = "warning->Please select from date"; txtFdate.Focus(); return; }
                    if (txtTdate.Text.Length == 0) { lblMessage.InnerText = "warning->Please select to date"; txtTdate.Focus(); return; }
                    if (ddlDepartment.SelectedItem.Text == "All") DepartmentList = GetAlllist(ddlDepartment);
                    else DepartmentList = ddlDepartment.SelectedValue;
                    if (ddlDesignation.SelectedItem.Text == "All") DesignationList = GetAlllist(ddlDesignation);
                    else DepartmentList = ddlDesignation.SelectedValue;

                    dt = new DataTable();
                    dt = ForAttendanceReport.return_dt_for_EmpAttSummary(ddlShiftList.SelectedValue, DepartmentList, DesignationList, txtFdate.Text, txtTdate.Text,rblEmpType.SelectedValue,ddlCardNo.SelectedValue);
                    if (dt == null || dt.Rows.Count < 1)
                    {
                        lblMessage.InnerText = "warning-> Any attendance Record are not founded!";
                        return;
                    }                   
                    Session["__EmpAttendanceSummaryReport__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=EmpAttendanceSummaryReport-" + ddlShiftList.SelectedItem.Text + "-" + txtFdate.Text.Trim() + "-" + txtFdate.Text.Trim() + "-"+Emptype+"');", true);
                }
                else 
                {
                    if (txtFdate.Text.Length == 0) { lblMessage.InnerText = "warning->Please select from date"; txtFdate.Focus(); return; }
                    if (txtTdate.Text.Length == 0) { lblMessage.InnerText = "warning->Please select to date"; txtTdate.Focus(); return; }
                    string EidList = (ddlCardNo.SelectedValue == "0") ? GetAlllist(ddlCardNo) : ddlCardNo.SelectedValue;
                    dt = new DataTable();
                    dt = ForAttendanceReport.return_dt_EmpAbsentDetails(ddlShiftList.SelectedValue,txtFdate.Text, txtTdate.Text, EidList);
                    if (dt == null || dt.Rows.Count < 1)
                    {
                        lblMessage.InnerText = "warning->  Absent records are not available";
                        return;
                    }
                    Session["__EmpAbsentDetails__"] = dt;
                    //...............                    
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=IndivisualEmpAbsentDetails-" +txtFdate.Text.Trim() +"-"+txtTdate.Text.Trim()+"-"+ddlShiftList.SelectedItem.Text+"-"+Emptype+"');", true);  //Open New Tab for Sever side code
                }
               // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me","goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=AttendanceSummaryReport-"+""+"');", true);  
                //Open New Tab for Sever side code
            }
            catch { }
        }
        private DataTable GetDatatabel(DataTable dt) 
        {
            return dt;
        }
        private string GetAlllist(DropDownList dl) // For Department And Designation All Id.
        {
            try
            {
                string setPredicate = "";
                for (byte b = 0; b < dl.Items.Count; b++)
                {
                    setPredicate += dl.Items[b].Value.ToString() + ",";
                }

                setPredicate = setPredicate.Remove(setPredicate.LastIndexOf(','));
                return setPredicate;
            }
            catch { return " "; }

        }
        protected void rblRepotMontly_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);

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
                LoadCardNo();                
                ddlMonth.Visible = false;
            }
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            if (ddlDepartment.SelectedItem.Text == "All")
            {
                Classes.commonTask.LoadDesignation(ddlDesignation);
            }
            else
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select Distinct DesId,DesName From v_EmployeeInfo where DId=" + ddlDepartment.SelectedValue + "", dt);
                ddlDesignation.DataSource = dt;
                ddlDesignation.DataTextField = "DesName";
                ddlDesignation.DataValueField = "DesId";
                ddlDesignation.DataBind();
                if (dt.Rows.Count > 1)
                ddlDesignation.Items.Insert(ddlDesignation.Items.Count, new ListItem("All", "0"));
                // dlDesignation.Items.Insert(0, new ListItem("", "0"));
                ddlDesignation.SelectedIndex = ddlDesignation.Items.Count - 1;
            }
          //  if (rblRepotMontly.SelectedValue == "2")
                LoadCardNo();
        }   

        protected void ddlDepartment_D_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            if (ddlDepartment_D.SelectedItem.Text == "All")
            {
                Classes.commonTask.LoadDesignation(ddlDesignation_D);
            }
            else
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select Distinct DesId,DesName From v_EmployeeInfo where DId=" + ddlDepartment_D.SelectedValue + "", dt);
                ddlDesignation_D.DataSource = dt;
                ddlDesignation_D.DataTextField = "DesName";
                ddlDesignation_D.DataValueField = "DesId";
                ddlDesignation_D.DataBind();
                if(dt.Rows.Count>1)
                ddlDesignation_D.Items.Insert(ddlDesignation_D.Items.Count, new ListItem("All", "0"));
                // dlDesignation.Items.Insert(0, new ListItem("", "0"));
                ddlDesignation_D.SelectedIndex = ddlDesignation_D.Items.Count - 1;
            }
            LoadCardNo_D();
        }
        private void generatDailyAttendance() 
        {
             
             string ReportTitel = "";
             string ReportType = "";             
             string reportType = "";
            // if (ddlShift_D.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Shift!"; ddlShift_D.Focus(); return; }
             if (txtDate.Text == "") { lblMessage.InnerText = "warning-> Please select From Date!"; txtDate.Focus(); return; }
             if (txtToDate.Text == "") { lblMessage.InnerText = "warning-> Please select To Date!"; txtToDate.Focus(); return; }
             DepartmentList = (ddlDepartment_D.SelectedItem.Text == "All") ? GetAlllist(ddlDepartment_D) : ddlDepartment_D.SelectedValue;
             DesignationList = (ddlDesignation_D.SelectedItem.Text == "All") ? GetAlllist(ddlDesignation_D) : ddlDesignation_D.SelectedValue;

             Emptype = (rblEmpType_D.SelectedValue == "2") ? "Teacher and Staff" : rblEmpType_D.SelectedItem.Text;
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
             dt = ForAttendanceReport.return_dt_DailyEmpAttReport(rblReportType.SelectedValue,DepartmentList, DesignationList, ddlShift_D.SelectedValue, txtDate.Text.Trim(),txtToDate.Text.Trim(),rblEmpType_D.SelectedValue,ddlCardNo_D.SelectedValue);
             if (dt == null || dt.Rows.Count < 1) 
             {
                 lblMessage.InnerText = "warning-> Any " + reportType + " record are not founded"; 
                 return;
             }
             string DateRange = (txtDate.Text == txtToDate.Text) ? txtDate.Text : txtDate.Text + " To " + txtToDate.Text;
             string IsIndividual = (ddlCardNo_D.SelectedValue == "0") ? "No" : "Yes";
             Session["__DailyEmpAttendance__"] = dt;
             ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DailyEmpAttendance-" + ReportTitel + "-" + ReportType + "-" + ddlShift_D.SelectedItem.Text + "-" + Emptype + "-" + DateRange.Replace('-','/') + "-"+IsIndividual+"');", true);
        }

        protected void btnPrint_D_Click(object sender, EventArgs e)
        {
            if (ddlCardNo_D.SelectedIndex==-1)
            {
                lblMessage.InnerText = "warning->Staff/Teacher's are not available!";
                return;
            }
            generatDailyAttendance();
        }
        protected void ddlShiftList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
           // if(rblRepotMontly.SelectedValue=="2")
            LoadCardNo();
        }     
        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
           // if (rblRepotMontly.SelectedValue == "2")
                LoadCardNo();
        }
        protected void rblEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
           // if (rblRepotMontly.SelectedValue == "2")
                LoadCardNo();
        }
        private void LoadCardNo()
        {
            //if (ddlShiftList.SelectedValue == "0")
            //{ lblMessage.InnerText = "warning-> Please select a Shift!"; return; }            
            DepartmentList = (ddlDepartment.SelectedItem.Text == "All") ? GetAlllist(ddlDepartment) : ddlDepartment.SelectedValue;
            DesignationList = (ddlDesignation.SelectedItem.Text == "All") ? GetAlllist(ddlDesignation) : ddlDesignation.SelectedValue;
            ForAttendanceReport.LoadCardNo_ForAtt(ddlCardNo, DepartmentList, DesignationList, ddlShiftList.SelectedValue, rblEmpType.SelectedValue);
            if (ddlCardNo.Items.Count < 1)
                lblMessage.InnerText = "warning->Staff/Teacher's are not available!";
        }
        private void LoadCardNo_D()
        {
            //if (ddlShift_D.SelectedValue == "0")
            //{ lblMessage.InnerText = "warning-> Please select a Shift!"; return; }            
            DepartmentList = (ddlDepartment_D.SelectedItem.Text == "All") ? GetAlllist(ddlDepartment_D) : ddlDepartment_D.SelectedValue;
            DesignationList = (ddlDesignation_D.SelectedItem.Text == "All") ? GetAlllist(ddlDesignation_D) : ddlDesignation_D.SelectedValue;
            ForAttendanceReport.LoadCardNo_ForAtt(ddlCardNo_D, DepartmentList, DesignationList, ddlShift_D.SelectedValue, rblEmpType_D.SelectedValue);
            if (ddlCardNo_D.Items.Count < 1)
                lblMessage.InnerText = "warning->Staff/Teacher's are not available!";              
                                
        }

        protected void ddlShift_D_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            LoadCardNo_D();
        }

        protected void ddlDesignation_D_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            LoadCardNo_D();
        }

        protected void rblEmpType_D_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            LoadCardNo_D();
        }
    }
}