using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ComplexScriptingSystem;
using DS.DAL.AdviitDAL;

namespace DS.Admin
{
    public partial class AddDistrict : System.Web.UI.Page
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
                    loadDistrictList("");
                }
            }
        }    

        private void updateDistrict()
        {
            try
            {
                string[] getColumns = { "DistrictName" };
                string[] getValues = {txtDistrict.Text.Trim()};
                string getIdentifierValue=lblDistrictId.Value.ToString();
                if (ComplexScriptingSystem.SQLOperation.forUpdateValue("Distritcts", getColumns, getValues, "DistrictId", getIdentifierValue, sqlDB.connection) == true)
                {
                    loadDistrictList("");
                    lblMessage.InnerText = "success-> Update Successfully";
                    txtDistrict.Text = "";
                    btnSaveDistrict.Text = "Save";
                }
            }
            catch { }
        }

        protected void btnSaveDistrict_Click(object sender, EventArgs e)
        {
            if (lblDistrictId.Value.Length == 0) saveDistrict();
            else updateDistrict();
        }

        private void saveDistrict()
        {
            try
            {
                string[] getColumns = {"DistrictName" };
                string[] getValues = {txtDistrict.Text.Trim()};
                if (SQLOperation.forSaveValue("Distritcts", getColumns, getValues, sqlDB.connection) == true)
                {
                    txtDistrict.Text = "";
                    btnSaveDistrict.Text = "Save";
                    loadDistrictList("");
                }
            }
            catch { }
        }
        private void loadDistrictList(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select DistrictId, DistrictName from Distritcts  Order by DistrictId ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);

            int totalRows = dt.Rows.Count;
            string divInfo = "";

            if (totalRows == 0)
            {
                divInfo = "<div class='noData'>No District available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divDistrictList.Controls.Add(new LiteralControl(divInfo));
                return;
            }

            divInfo = " <table id='tblDistrictList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>District Name</th>";
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {

                id = dt.Rows[x]["DistrictId"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td  >" + dt.Rows[x]["DistrictName"].ToString() + "</td>";

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