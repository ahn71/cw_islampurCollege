using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComplexScriptingSystem;
using DS.DAL.AdviitDAL;
using System.Data;
using System.Data.SqlClient;

namespace DS.Admin
{
    public partial class AddThana : System.Web.UI.Page
    {
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    loadDistricts();
                    loadThanaList("");
                }
            }
            catch { }
        }

        private void loadDistricts()
        {
            DataTable dt = new DataTable();
            sqlDB.fillDataTable("select districtName from Distritcts", dt);
            dlDistrict.DataValueField = "DistrictName";
            dlDistrict.DataSource = dt;
            dlDistrict.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblThanaId.Value.Length == 0) saveThana();
            else updateThanas();
        }
        private void saveThana()
        {
            try
            {

                string[] getColumns = { "DistrictId", "ThanaName" };
                string[] getValues = { getDistrictId(), txtThana.Text.Trim() };
                if (ComplexScriptingSystem.SQLOperation.forSaveValue("Thanas", getColumns, getValues, sqlDB.connection) == true)
                {
                    lblMessage.InnerText = "success-> Save Successfull";
                    loadThanaList("");
                    clear();
                }

            }
            catch { }
        }

        private Boolean updateThanas()
        {
            try
            {
                getDistrictId();
                SqlCommand cmd = new SqlCommand(" update Thanas  Set DistrictId=@DistrictId, ThanaName=@ThanaName where ThanaId=@ThanaId ", sqlDB.connection);

                cmd.Parameters.AddWithValue("@ThanaId", lblThanaId.Value.ToString());
                cmd.Parameters.AddWithValue("@DistrictId",dt.Rows[0].ItemArray[0].ToString());
                cmd.Parameters.AddWithValue("@ThanaName", txtThana.Text.Trim());

                cmd.ExecuteNonQuery();
                loadThanaList("");
                lblMessage.InnerText = "success->Update Successfull";
                clear();
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        private string getDistrictId()
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("select DistrictId from Distritcts where DistrictName='" + dlDistrict.Text.Trim() + "'", dt);
                return dt.Rows[0].ItemArray[0].ToString();
            }
            catch { return ""; }
        }


        private void clear()
        {
            try
            {
                txtThana.Text = "";
                btnSave.Text = "Save";
                txtThana.Focus();
            }
            catch { }
        }



        private void loadThanaList(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select ThanaId,DistrictName,ThanaName from v_ThanaDetails  Order by ThanaId ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);

            int totalRows = dt.Rows.Count;

            string divInfo = "";

            divThanaList.Controls.Add(new LiteralControl(divInfo));

            if (totalRows == 0)
            {
                divInfo = "<div class='noData'>No Thana/Upazila available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divThanaList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            
            divInfo = " <table id='tblThana' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>ThanaName</th>";
            divInfo += "<th>DistrictName</th>";
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {

                id = dt.Rows[x]["ThanaId"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td>" + dt.Rows[x]["ThanaName"].ToString() + "</td>";
                divInfo += "<td>" + dt.Rows[x]["DistrictName"].ToString() + "</td>";

                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editThana(" + id + ");'  />";


            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";

            divInfo += "</table>";

            divInfo += "<div class='dataTables_wrapper'><div class='head'> </div></div>";
            divThanaList.Controls.Add(new LiteralControl(divInfo));

        }

    }
}