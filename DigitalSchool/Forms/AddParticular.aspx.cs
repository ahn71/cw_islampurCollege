using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Admin
{
    public partial class AddFeesType : System.Web.UI.Page
    {
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
                    LoadFessType("");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblFId.Value.ToString().Length==0)
                saveFeesType();
            else
                updateFeesType();
        }
        private Boolean saveFeesType()
        {
            try
            {

                SqlCommand cmd = new SqlCommand("Insert into  ParticularsInfo   values (@PName) ", sqlDB.connection);

                cmd.Parameters.AddWithValue("@PName", txtFeesType.Text.Trim());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    lblMessage.InnerText = "success->Successfully saved";
                    txtFeesType.Text = "";
                    lblFId.Value = "";
                    LoadFessType("");
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "saveSuccess();", true);
                }

                return true;

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                LoadFessType("");
                return false;
            }
        }
        private Boolean updateFeesType()
        {
            try
            {

                SqlCommand cmd = new SqlCommand(" update ParticularsInfo  Set PName=@PName where PId=@PId ", sqlDB.connection);

                cmd.Parameters.AddWithValue("@PId", lblFId.Value.ToString());
                cmd.Parameters.AddWithValue("@PName", txtFeesType.Text.Trim());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    txtFeesType.Text = "";
                    LoadFessType("");
                    btnSave.Text = "Save";
                    lblFId.Value = "";
                    txtFeesType.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                }

                return true;

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                LoadFessType("");
                return false;
            }
        }
        private void LoadFessType(string sqlcmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select *  from ParticularsInfo  Order by PId ";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlcmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Allowance Type available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divFeesType.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                divInfo = " <table id='tblDesignationList' class='display'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Fees Type</th>";
                divInfo += "<th>Edit</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";
                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["PId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td>" + dt.Rows[x]["PName"].ToString() + "</td>";

                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editFeesType(" + id + ");'  />";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divFeesType.Controls.Add(new LiteralControl(divInfo));
            }
            catch{}
        }      
    }
}