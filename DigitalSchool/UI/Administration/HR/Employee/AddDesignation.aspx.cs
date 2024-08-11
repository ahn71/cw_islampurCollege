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

namespace DS.UI.Administration.HR.Employee
{
    public partial class AddDesignation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddDesignation.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    loadDesignationList("");
                }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblDesignationId.Value.ToString().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loaddatatable();", true);
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadDesignationList(""); return; }
                saveDesignations();
            }
            else
            {
                updateDesignations();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
            }

            loadDesignationList("");


        }

        private Boolean updateDesignations()
        {
            try
            {
                SqlCommand cmd = new SqlCommand(" update Designations  Set DesName=@DesName where DesId=@DesId ", DbConnection.Connection);

                cmd.Parameters.AddWithValue("@DesId", lblDesignationId.Value.ToString());
                cmd.Parameters.AddWithValue("@DesName", txtDes_Name.Text.Trim());

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        private Boolean saveDesignations()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("saveDesignations", DbConnection.Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DesName", txtDes_Name.Text.Trim());

                int result = (int)cmd.ExecuteScalar();
                btnSave.Text = "Save";
                if (result > 0) lblMessage.InnerText = "success->Save Successfully";
                else lblMessage.InnerText = "error->Unable to save";

                return true;

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }


        protected void btnNew_Click(object sender, EventArgs e)
        {
            txtDes_Name.Text = "";
            btnSave.Text = "Save";
        }

        private void loadDesignationList(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select DesId, DesName from Designations  Order by DesId ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);

            int totalRows = dt.Rows.Count;
            string divInfo = "";            

            divInfo = " <table id='tblDesignationList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Designation</th>";
            if (Session["__Update__"].ToString().Equals("true"))
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            if (totalRows == 0)
            {
                divInfo += "</tbody></table>";                
                divDesignationList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {

                id = dt.Rows[x]["DesId"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td >" + dt.Rows[x]["DesName"].ToString() + "</td>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editEmployee(" + id + ");'  />";
            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";

            divInfo += "</table>";
            divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

            divDesignationList.Controls.Add(new LiteralControl(divInfo));

        }
    }
}