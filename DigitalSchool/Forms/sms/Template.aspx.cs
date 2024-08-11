using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Forms.sms
{
    public partial class Template : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            Session["__UserId__"] = "oitl";
            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    loadTemplate("");
                }
            }
        }
        private void loadTemplate(string sqlcmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select *  from SMS_Template  Order by TId ";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlcmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";
                divTemplate.Controls.Clear();
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Template available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divTemplate.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                divInfo = " <table id='tblDesignationList' class='display'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Title</th>";
                divInfo += "<th>Body</th>";
                divInfo += "<th>Edit</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";
                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    id = dt.Rows[x]["TId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td>" + dt.Rows[x]["Title"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["Body"].ToString() + "</td>";
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editTemplate(" + id + ");'  />";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divTemplate.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (txtTitel.Text == "")
            {
                lblMessage.InnerText = "warning->Please Type Titel";
                txtTitel.Focus();
                return;
            }
            if (lblTId.Value.ToString()=="0")
            saveTemplate();
            else
            updateTemplate();
        }
        private void saveTemplate()
        {
            try
            {
                string[] getColumns = { "Title", "Body" };
                string[] getValues = { txtTitel.Text.Trim(),txtBody.Text.Trim() };
                if (ComplexScriptingSystem.SQLOperation.forSaveValue("SMS_Template", getColumns, getValues, sqlDB.connection) == true)
                {
                    loadTemplate("");
                    clear();
                    lblMessage.InnerText = "success->Successfully save";
                }
            }
            catch { }
        }
        private void updateTemplate()
        {
            try
            {
                string[] getColumns = { "Title", "Body" };
                string[] getValues = { txtTitel.Text.Trim(), txtBody.Text.Trim() };
                string getIdentifierValue = lblTId.Value.ToString();
                if (ComplexScriptingSystem.SQLOperation.forUpdateValue("SMS_Template", getColumns, getValues, "TId", getIdentifierValue, sqlDB.connection) == true)
                {
                    clear();
                    loadTemplate("");
                    lblMessage.InnerText = "success->Successfully updated";
                }
            }
            catch { }
        }
        private void clear()
        {
            txtBody.Text = "";
            txtTitel.Text = "";
            lblTId.Value = "0";
            btnSave.Text = "Save";
            
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            lblMessage.InnerText = "";
        }
    }
}