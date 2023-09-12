using DS.BLL.ControlPanel;
using DS.DAL.AdviitDAL;
using DS.DAL.ComplexScripting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academics.Attendance.Leave
{
    public partial class leave_configuration : System.Web.UI.Page
    {
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "leave_configuration.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");               
                    loadLeaveConfiguration();
                }
        }

        private void loadLeaveConfiguration()
        {
            try
            {

                sqlDB.fillDataTable("select * from leave_configuration ", dt = new DataTable());
                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Leave Config List available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divLeaveConfigList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                divInfo = " <table id='tblLeaveConfigList' class='display'> ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Leave Name</th>";
                divInfo += "<th>Days</th>";
                divInfo += "<th>Leave Nature</th>";
                divInfo += "<th>DeductionAllowed</th>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<th>Edit</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";
                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    id = dt.Rows[x]["LeaveId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + dt.Rows[x]["LeaveName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["LeaveDays"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["LeaveNature"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["DeductionAllowed"].ToString() + "</td>";
                    if (Session["__Update__"].ToString().Equals("true"))
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editLeaveConfig(" + id + ");'  />";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divLeaveConfigList.Controls.Add(new LiteralControl(divInfo));

            }
            catch { }
        }
        private void saveLeaveConfucuration()
        {
            try
            {
                byte hasDeduction = (chkDeductionAllowed.Checked) ? (byte)1 : (byte)0;
                string[] getColumns = { "LeaveName", "ShortName", "LeaveDays", "LeaveNature", "DeductionAllowed" };
                string[] getValues = { ddlLeaveTypes.SelectedItem.ToString(), ddlLeaveTypes.SelectedValue.ToString(), txtLeaveDays.Text.Trim(), txtLeaveNature.Text.Trim(), hasDeduction.ToString() };
                if (SQLOperation.forSaveValue("leave_configuration", getColumns, getValues, sqlDB.connection) == true)
                {
                    lblMessage.InnerText = "success->Successfully Saved Leave Configured";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "clearIt();", true);
                    loadLeaveConfiguration();
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "warning->Already Added";
            }
        }

        private void updateLeaveConfiguration()
        {
            try
            {
                byte hasDeduction = (chkDeductionAllowed.Checked) ? (byte)1 : (byte)0;

                string[] getColumns = { "LeaveDays", "LeaveNature", "DeductionAllowed" };
                string[] getValues = { txtLeaveDays.Text.Trim(), txtLeaveNature.Text.Trim(), hasDeduction.ToString() };
                if (SQLOperation.forUpdateValue("leave_configuration", getColumns, getValues, "LeaveId", hfLeaveNameId.Value.ToString(), sqlDB.connection) == true)
                {
                    lblMessage.InnerText = "success->Successfully Updated Leave Configured";
                    loadLeaveConfiguration();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "clearIt();", true);

                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "warning->" + ex.Message;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (hfLeaveNameId.Value.ToString().Equals(" ")) 
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadLeaveConfiguration(); return; }
                saveLeaveConfucuration(); 
            }
            else updateLeaveConfiguration();
        }
    }
}