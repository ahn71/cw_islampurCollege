using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.Attendace;

namespace DS.UI.Reports.Attendance
{
    public partial class LeaveReport : System.Web.UI.Page
    {
        string DepartmentList = "";
        string DesignationList = "";
        string DateRange = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    if (!IsPostBack)
                    {
                       // if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "MonthWiseAttendanceSheetSummary.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                       // txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                        ShiftEntry.GetDropDownList(ddlShift);
                        Classes.commonTask.LoadDeprtmentAtttedence(ddlDepartment);
                        Classes.commonTask.LoadDesignation(ddlDesignation);
                        ShiftEntry.GetDropDownList(ddlShiftList);
                        Classes.commonTask.LoadDeprtmentAtttedence(ddlDepartmentList);
                        Classes.commonTask.LoadDesignation(ddlDesignationList);
                        ShiftEntry.GetDropDownList(ddlShift_L);
                        Classes.commonTask.LoadDeprtmentAtttedence(ddlDepartment_L);
                        Classes.commonTask.LoadDesignation(ddlDesignation_L);
                        Classes.commonTask.LoadDeprtmentAtttedence(ddlDepartment_Ap);
                        Classes.commonTask.LoadDesignation(ddlDesignation_Ap);
                        ShiftEntry.GetDropDownList(ddlShift_Ap);
                        ForLeaveReport.loadYear(ddlYear);

                    }
            }
            catch { }
            lblMessage.InnerText = "";
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            
            if (rblReportType_B.SelectedValue == "0") { 
                if (ddlShift.SelectedValue == "0") { lblMessage.InnerText = "warning->Please Select Any Shift"; ddlShift.Focus(); return; }
            if (ddlDepartment.SelectedItem.Text == "All") DepartmentList = GetAlllist(ddlDepartment);
            else DepartmentList = ddlDepartment.SelectedValue;
            if (ddlDesignation.SelectedItem.Text == "All") DesignationList = GetAlllist(ddlDesignation);
            else DesignationList= ddlDesignation.SelectedValue;
            }
            //----------------Validation---------------
            if (txtCardNo_B.Text == "" && rblReportType_B.SelectedValue == "1") { lblMessage.InnerText = "warning->Please type Valid Card No !"; txtCardNo_B.Focus(); return; }   
            if (txtFromDate.Text == "") { lblMessage.InnerText = "warning->Please Select From Date"; txtFromDate.Focus(); return; }
            if (txtToDate.Text == "") { lblMessage.InnerText = "warning->Please Select To Date"; txtToDate.Focus(); return; }
            //-----------------------------------------
            if (ForLeaveReport.generateLeaveBalanceReport(ddlShift.SelectedValue, DepartmentList, DesignationList,txtFromDate.Text.Trim(),txtToDate.Text.Trim(),rblReportType_B.SelectedValue,txtCardNo_B.Text)){
                DateRange = txtFromDate.Text + " To " + txtToDate.Text;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=LeaveBalanceReport-"+DateRange.Replace('-','/')+"-"+rblReportType_B.SelectedValue+"');", true);
            }
            else lblMessage.InnerText = "warning-> Any record are not available";
            
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

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            //----------------Validation---------------
           
            if (txtCardNo.Text == "" && rblReportType.SelectedValue == "1") { lblMessage.InnerText = "warning->Please type Valid Card No !"; txtCardNo.Focus(); return; }    
            //-----------------------------------------
            if (rblReportType.SelectedValue == "0") {
                if (ddlShiftList.SelectedValue == "0") { lblMessage.InnerText = "warning->Please Select Any Shift"; ddlShift.Focus(); return; }
            if (ddlDepartmentList.SelectedItem.Text == "All") DepartmentList = GetAlllist(ddlDepartmentList);
            else DepartmentList = ddlDepartmentList.SelectedValue;
            if (ddlDesignationList.SelectedItem.Text == "All") DesignationList = GetAlllist(ddlDesignationList);
            else DesignationList = ddlDesignationList.SelectedValue;
            }
            if (ForLeaveReport.generatYearlyLeaveStatus(ddlYear.SelectedValue, ddlShiftList.SelectedValue, DepartmentList, DesignationList, txtCardNo.Text, rblReportType.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=YearlyLeaveStatus-" + rblReportType.SelectedValue + "');", true);
            }
            else lblMessage.InnerText = "warning-> Any record are not available";
        }
        protected void rblReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblReportType.SelectedValue == "1")
            {
                tdtxtCardNo.Visible = true; txtCardNo.Text = ""; txtCardNo.Focus();
                lnkNew.Visible = true;
            }
            else { tdtxtCardNo.Visible = false; lnkNew.Visible = false; }
        }
        protected void rblReportType_B_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblReportType_B.SelectedValue == "1")
            {
                tdtxtCardNo_B.Visible = true; txtCardNo_B.Text = ""; txtCardNo_B.Focus();
                lnkNew_B.Visible = true;
            }
            else { tdtxtCardNo_B.Visible = false; lnkNew_B.Visible = false; }
        }

        protected void lnkNew_Click(object sender, EventArgs e)
        {
            txtCardNo.Text = "";
            txtCardNo.Focus();
        }
        protected void lnkNew_B_Click(object sender, EventArgs e)
        {
            txtCardNo_B.Text = "";
            txtCardNo_B.Focus();
        }
        //------------------------------------Leave List Report--------------------------------------------
        protected void lnkNew_L_Click(object sender, EventArgs e)
        {
            txtCardNo_L.Text = "";
            txtCardNo_L.Focus();
        }
        protected void rblReportType_L_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblReportType_L.SelectedValue == "1")
            {
                tdtxtCardNo_L.Visible = true; txtCardNo_L.Text = ""; txtCardNo_L.Focus();
                lnkNew_L.Visible = true;
            }
            else { tdtxtCardNo_L.Visible = false; lnkNew_L.Visible = false; }
        }
        protected void btnPrint_L_Click(object sender, EventArgs e)
        {
            if (rblReportType_L.SelectedValue == "0")
            {
                if (ddlShift_L.SelectedValue == "0") { lblMessage.InnerText = "warning->Please Select Any Shift"; ddlShift_L.Focus(); return; }
                if (ddlDepartment_L.SelectedItem.Text == "All") DepartmentList = GetAlllist(ddlDepartment_L);
                else DepartmentList = ddlDepartment_L.SelectedValue;
                if (ddlDesignation_L.SelectedItem.Text == "All") DesignationList = GetAlllist(ddlDesignation_L);
                else DesignationList = ddlDesignation_L.SelectedValue;
            }
            //----------------Validation---------------
            if (txtCardNo_L.Text == "" && rblReportType_L.SelectedValue == "1") { lblMessage.InnerText = "warning->Please type Valid Card No !"; txtCardNo_L.Focus(); return; }
            if (txtFromDate_L.Text == "") { lblMessage.InnerText = "warning->Please Select From Date"; txtFromDate_L.Focus(); return; }
            if (txtToDate_L.Text == "") { lblMessage.InnerText = "warning->Please Select To Date"; txtToDate_L.Focus(); return; }
            //-----------------------------------------
            if (ForLeaveReport.generateLeaveListReport(ddlShift_L.SelectedValue, DepartmentList, DesignationList, txtFromDate_L.Text.Trim(), txtToDate_L.Text.Trim(), rblReportType_L.SelectedValue, txtCardNo_L.Text))
            {
                DateRange = txtFromDate_L.Text + " To " + txtToDate_L.Text;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=LeaveListReport-" + DateRange.Replace('-', '/') + "-" + rblReportType_L.SelectedValue + "');", true);
            }
            else lblMessage.InnerText = "warning-> Any record are not available";
        }
        //----------------------------Approve/Rejected List Report--------------------------------
        protected void btnPrint_Ap_Click(object sender, EventArgs e)
        {

            if (rblReportType_Ap.SelectedValue == "0")
            {
                if (ddlShift_Ap.SelectedValue == "0") { lblMessage.InnerText = "warning->Please Select Any Shift"; ddlShift_Ap.Focus(); return; }
                if (ddlDepartment_Ap.SelectedItem.Text == "All") DepartmentList = GetAlllist(ddlDepartment_Ap);
                else DepartmentList = ddlDepartment_Ap.SelectedValue;
                if (ddlDesignation_Ap.SelectedItem.Text == "All") DesignationList = GetAlllist(ddlDesignation_Ap);
                else DesignationList = ddlDesignation_Ap.SelectedValue;
            }
            //----------------Validation---------------
            if (txtCardNo_Ap.Text == "" && rblReportType_Ap.SelectedValue == "1") { lblMessage.InnerText = "warning->Please type Valid Card No !"; txtCardNo_Ap.Focus(); return; }
            if (txtFromDate_Ap.Text == "") { lblMessage.InnerText = "warning->Please Select From Date"; txtFromDate_Ap.Focus(); return; }
            if (txtToDate_Ap.Text == "") { lblMessage.InnerText = "warning->Please Select To Date"; txtToDate_Ap.Focus(); return; }
            //-----------------------------------------
            if (ForLeaveReport.generateLeaveApprovedRejectedReport(ddlShift_Ap.SelectedValue, DepartmentList, DesignationList, txtFromDate_Ap.Text.Trim(), txtToDate_Ap.Text.Trim(), rblReportType_Ap.SelectedValue, txtCardNo_Ap.Text,rblApprovedRejected.SelectedValue))
            {
                DateRange = txtFromDate_Ap.Text + " To " + txtToDate_Ap.Text;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=LeaveApprovedRejectedReport-" + DateRange.Replace('-', '/') + "-" + rblApprovedRejected.SelectedItem.Text + "');", true);
            }
            else lblMessage.InnerText = "warning-> Any record are not available";
        }
        protected void rblReportType_Ap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblReportType_Ap.SelectedValue == "1")
            {
                tdtxtCardNo_Ap.Visible = true; txtCardNo_Ap.Text = ""; txtCardNo_Ap.Focus();
                lnkNew_Ap.Visible = true;
            }
            else { tdtxtCardNo_Ap.Visible = false; lnkNew_Ap.Visible = false; }
        }
        protected void lnkNew_Ap_Click(object sender, EventArgs e)
        {
            txtCardNo_Ap.Text = "";
            txtCardNo_Ap.Focus();
        }
        //________________________________________________________________________________________
       
    }
}