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

namespace DS.UI.Administration.Finance.FeeManaged
{
    public partial class AddParticular : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddParticular.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    LoadFessType("");
                }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblFId.Value.ToString().Length == 0)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; LoadFessType(""); return; }
                saveFeesType();
            }
            else
                updateFeesType();
        }
        private Boolean saveFeesType()
        {
            try
            {

                SqlCommand cmd = new SqlCommand("Insert into  ParticularsInfo   values (@PName) ", DbConnection.Connection);

                cmd.Parameters.AddWithValue("@PName", txtFeesType.Text.Trim());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    lblMessage.InnerText = "success->Save Successfully ";
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

                SqlCommand cmd = new SqlCommand(" update ParticularsInfo  Set PName=@PName where PId=@PId ", DbConnection.Connection);

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
                divInfo = " <table id='tblDesignationList' ='displaclassy'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Fees Type</th>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<th>Edit</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                if (totalRows == 0)
                {
                    divInfo += "</table>";                    
                    divFeesType.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                divInfo += "<tbody>";
                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["PId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td>" + dt.Rows[x]["PName"].ToString() + "</td>";
                    if (Session["__Update__"].ToString().Equals("true"))
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editFeesType(" + id + ");'  />";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";                
                divFeesType.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }  
    }
}