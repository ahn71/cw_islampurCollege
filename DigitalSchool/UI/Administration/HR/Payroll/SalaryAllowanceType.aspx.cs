using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.BLL.ControlPanel;
using DS.DAL;

namespace DS.UI.Administration.HR.Payroll
{
    public partial class SalaryAllowanceType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                lblMessage.InnerText = "";
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "SalaryAllowanceType.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    loadDesignationList("");
                }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblAId.Value.ToString().Length == 0)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadDesignationList(""); return; }
                if (saveAllowanceType() == true)
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SavedSuccess();", true);
            }
            else
            {
               if(updateAllowanceType())
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
            }

        }
        private Boolean saveAllowanceType()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("saveAllowanceType",DbConnection.Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AType", txtAllowanceType.Text.Trim());
                cmd.Parameters.AddWithValue("@APercentage", txtPercentage.Text.Trim());
                if (chkStatus.Checked == true) cmd.Parameters.AddWithValue("@AStatus", 1);
                else cmd.Parameters.AddWithValue("@AStatus", 0);
                int result = (int)cmd.ExecuteScalar();
                if (result > 0)
                {
                    lblMessage.InnerText = "success->Successfully saved";
                    loadDesignationList("");
                }
                else lblMessage.InnerText = "error->Unable to save";
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        private void saveAllowanceLog(string Aid)
        {
            try
            {
                Session["__username__"] = "0";
                string UserId = (Session["__username__"].ToString().Equals("")) ? Session["__username__"].ToString() : "0";
                string[] getColumns = { "AId", "ALNewPercentage", "ALOldPercentage", "ALDate", "ALChangedBy" };
                string[] getValues = { Aid, txtPercentage.Text, lblOldPercentage.Value.ToString(), DateTime.Now.ToString("yyyy/MM/dd"), UserId };
                if (ComplexScriptingSystem.SQLOperation.forSaveValue("AllowanceLog", getColumns, getValues, DbConnection.Connection) == true) { }
            }
            catch { }
        }

        private void loadDesignationList(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select *  from AllowanceType  Order by AId ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);
            int totalRows = dt.Rows.Count;
            string divInfo = "";
            if (totalRows == 0)
            {
                divInfo = "<div class='noData'>No Allowance Type available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divAllowanceType.Controls.Add(new LiteralControl(divInfo));
                return;
            }

            divInfo = " <table id='tblDesignationList' class='table table-striped table-bordered dt-responsive nowrap'cellspacing='0' Width='100%' > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Allowance Type</th>";
            divInfo += "<th>Percentage</th>";
            divInfo += "<th>Status</th>";
            if (Session["__Update__"].ToString().Equals("true"))
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            string id = "";
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                id = dt.Rows[x]["AId"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td>" + dt.Rows[x]["AType"].ToString() + "</td>";
                divInfo += "<td>" + dt.Rows[x]["APercentage"].ToString() + "</td>";
                divInfo += "<td>" + dt.Rows[x]["AStatus"].ToString() + "</td>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editEmployee(" + id + ");'  />";
            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divAllowanceType.Controls.Add(new LiteralControl(divInfo));

        }
        private Boolean updateAllowanceType()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("updateAllowanceType", DbConnection.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AId", lblAId.Value.ToString());
                cmd.Parameters.AddWithValue("@AType", txtAllowanceType.Text);
                cmd.Parameters.AddWithValue("@APercentage", txtPercentage.Text.Trim());
                if (chkStatus.Checked == true) cmd.Parameters.AddWithValue("@AStatus", 1);
                else cmd.Parameters.AddWithValue("@AStatus", 0);
                int result = (int)cmd.ExecuteScalar();
                if (result > 0)
                {
                    saveAllowanceLog(lblAId.Value.ToString());
                    lblMessage.InnerText = "success->Successfully Updated";
                    lblAId.Value = "";
                    loadDesignationList("");
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
    }
}