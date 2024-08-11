using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.DAL.ComplexScripting;
using DS.BLL.ControlPanel;
using DS.DAL;

namespace DS.UI.Administration.Settings.GeneralSettings
{
    public partial class AddDistrict : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                lblMessage.InnerText = "";
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddDistrict.aspx", btnSaveDistrict)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    loadDistrictList("");                   
                }
        }

        private void updateDistrict()
        {
            try
            {
                string[] getColumns = { "DistrictName", "DistrictNameBn" };
                string[] getValues = { txtDistrict.Text.Trim(),txtDistrictBn.Text.Trim() };
                string getIdentifierValue = lblDistrictId.Value.ToString();
                if (ComplexScriptingSystem.SQLOperation.forUpdateValue("Distritcts", getColumns, getValues, "DistrictId", getIdentifierValue,DbConnection.Connection) == true)
                {
                    loadDistrictList("");
                    lblMessage.InnerText = "success-> Update Successfully";
                    lblDistrictId.Value = "";
                    txtDistrict.Text = "";
                    btnSaveDistrict.Text = "Save";
                }
            }
          catch (Exception ex)
            {
                lblMessage.InnerText = "error-> "+ex.Message+"";
            }
        }

        protected void btnSaveDistrict_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loaddatatable();", true);
            if (lblDistrictId.Value.Length == 0)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadDistrictList(""); return; }
                saveDistrict();
            }
            else updateDistrict();
        }

        private void saveDistrict()
        {
            try
            {
                string[] getColumns = { "DistrictName" , "DistrictNameBn" };
                string[] getValues = { txtDistrict.Text.Trim(),txtDistrictBn.Text.Trim() };
                if (SQLOperation.forSaveValue("Distritcts", getColumns, getValues, DbConnection.Connection) == true)
                {
                    lblMessage.InnerText = "success->Add Successfully";
                    txtDistrict.Text = "";
                    btnSaveDistrict.Text = "Save";
                    loadDistrictList("");
                }
                else lblMessage.InnerText = "Unable to Save";
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error-> "+ex.Message+"";
            }
        }
        private void loadDistrictList(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select DistrictId, DistrictName,DistrictNameBn from Distritcts  Order by DistrictName ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);

            int totalRows = dt.Rows.Count;
            string divInfo = "";          

            divInfo = " <table id='tblDistrictList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>District Name</th>";
            divInfo += "<th>জেলা</th>";
            if (Session["__Update__"].ToString().Equals("true"))
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            if (totalRows == 0)
            {
                divInfo += "</tbody>";
                divInfo += "</table>";
                divDistrictList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {

                id = dt.Rows[x]["DistrictId"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td  >" + dt.Rows[x]["DistrictName"].ToString() + "</td>";
                divInfo += "<td  >" + dt.Rows[x]["DistrictNameBn"].ToString() + "</td>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editDistrict(" + id + ");'  />";
            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";

            divInfo += "</table>";
            divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

            divDistrictList.Controls.Add(new LiteralControl(divInfo));

        }       
    }
}