using DS.BLL.Attendace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Adviser
{
    public partial class AdviserLeave : System.Web.UI.Page
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
                        ForLeaveReport.loadYear(ddlYear);

                    }
            }
            catch { }
            lblMessage.InnerText = "";
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
          
            if (txtFromDate.Text == "") { lblMessage.InnerText = "warning->Please Select From Date"; txtFromDate.Focus(); return; }
            if (txtToDate.Text == "") { lblMessage.InnerText = "warning->Please Select To Date"; txtToDate.Focus(); return; }
            //-----------------------------------------
            if (ForLeaveReport.generateLeaveBalanceReport(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), Session["__EID__"].ToString()))
            {
                DateRange = txtFromDate.Text + " To " + txtToDate.Text;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=LeaveBalanceReport-" + DateRange.Replace('-', '/') + "-1');", true);
            }
            else lblMessage.InnerText = "warning-> Any record are not available";

        }       
        //Yearly Leave Status Report
        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            //----------------Validation---------------           
            if (ForLeaveReport.generatYearlyLeaveStatus(ddlYear.SelectedValue,Session["__EID__"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=YearlyLeaveStatus-1');", true);
            }
            else lblMessage.InnerText = "warning-> Any record are not available";
        }
        //------------------------------------Leave List Report--------------------------------------------
       
       
        protected void btnPrint_L_Click(object sender, EventArgs e)
        {
        
            //----------------Validation---------------           
            if (txtFromDate_L.Text == "") { lblMessage.InnerText = "warning->Please Select From Date"; txtFromDate_L.Focus(); return; }
            if (txtToDate_L.Text == "") { lblMessage.InnerText = "warning->Please Select To Date"; txtToDate_L.Focus(); return; }
            //-----------------------------------------
            if (ForLeaveReport.generateLeaveListReport(txtFromDate_L.Text.Trim(), txtToDate_L.Text.Trim(),Session["__EID__"].ToString()))
            {
                DateRange = txtFromDate_L.Text + " To " + txtToDate_L.Text;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=LeaveListReport-" + DateRange.Replace('-', '/') + "-1');", true);
            }
            else lblMessage.InnerText = "warning-> Any record are not available";
        }
        //----------------------------Approve/Rejected List Report--------------------------------
        protected void btnPrint_Ap_Click(object sender, EventArgs e)
        {
            if (txtFromDate_Ap.Text == "") { lblMessage.InnerText = "warning->Please Select From Date"; txtFromDate_Ap.Focus(); return; }
            if (txtToDate_Ap.Text == "") { lblMessage.InnerText = "warning->Please Select To Date"; txtToDate_Ap.Focus(); return; }
            //-----------------------------------------
            if (ForLeaveReport.generateLeaveApprovedRejectedReport(txtFromDate_Ap.Text.Trim(), txtToDate_Ap.Text.Trim(),Session["__EID__"].ToString(),rblApprovedRejected.SelectedValue))
            {
                DateRange = txtFromDate_Ap.Text + " To " + txtToDate_Ap.Text;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=LeaveApprovedRejectedReport-" + DateRange.Replace('-', '/') + "-" + rblApprovedRejected.SelectedItem.Text + "');", true);
            }
            else lblMessage.InnerText = "warning-> Any record are not available";
        }      
      
    }
}