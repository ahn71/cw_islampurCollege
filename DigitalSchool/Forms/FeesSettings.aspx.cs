using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
namespace DS.Admin
{
    public partial class FeesSettings : System.Web.UI.Page
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
                    LoadClass();
                    LoadFessType();
                    loadFeesSettingsList("");
                }
            }
        }

        private void LoadClass()
        {
            sqlDB.bindDropDownList("select ClassName from Classes", "ClassName", ddlClassName);
        }

        private void LoadFessType()
        {
            sqlDB.bindDropDownList("select FType from FeesType", "FType", ddlFessType);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblFeesSettId.Value.ToString().Length==0)
                saveFeesSettings();
            else
                updateFeesSettings();
        }

        private Boolean saveFeesSettings()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("saveFeesSettings", sqlDB.connection);
                cmd.CommandType = CommandType.StoredProcedure;

                DataTable dt=new DataTable();
                sqlDB.fillDataTable("Select ClassID From Classes where ClassName='"+ddlClassName.Text+"'",dt);
                int ClassId=int.Parse(dt.Rows[0]["ClassID"].ToString());
                dt=new DataTable();
                sqlDB.fillDataTable("Select FId From FeesType where FType='"+ddlFessType.Text+"'",dt);
                int FeesId = int.Parse(dt.Rows[0]["FId"].ToString());

                cmd.Parameters.AddWithValue("@ClassID", ClassId);
                cmd.Parameters.AddWithValue("@FId",FeesId);
                cmd.Parameters.AddWithValue("@FAmount", txtAmount.Text.Trim());

                int result = (int)cmd.ExecuteScalar();

                if (result > 0)
                {
                    loadFeesSettingsList("");
                    lblMessage.InnerText = "success->Successfully saved";
                    txtAmount.Text = "";
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = "";
                btnSave.Text = "Save";
                ddlClassName.Text = "";
                ddlFessType.Text = "";
            }
            catch { }
        }


        private Boolean updateFeesSettings()
        {
            try
            {              
                SqlCommand cmd = new SqlCommand("updateFeesSettings", sqlDB.connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FSId", lblFeesSettId.Value.ToString());
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select ClassID From Classes where ClassName='" + ddlClassName.Text + "'", dt);
                int ClassId = int.Parse(dt.Rows[0]["ClassID"].ToString());
                dt = new DataTable();
                sqlDB.fillDataTable("Select FId From FeesType where FType='" + ddlFessType.Text + "'", dt);
                int FeesId = int.Parse(dt.Rows[0]["FId"].ToString());

                cmd.Parameters.AddWithValue("@ClassID", ClassId);
                cmd.Parameters.AddWithValue("@FId", FeesId);
                cmd.Parameters.AddWithValue("@FAmount", txtAmount.Text.Trim());
                int Result = (int)cmd.ExecuteScalar();
                if (Result==1)
                {
                    lblMessage.InnerText = "success->Successfully Updated";
                    txtAmount.Text = "";
                    btnSave.Text = "Save";
                    loadFeesSettingsList("");
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                }

                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }


        private void loadFeesSettingsList(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select FSId, ClassName, FType, FAmount from v_FeesSettings  Order by FSId ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);

            int totalRows = dt.Rows.Count;
            string divInfo = "";

            if (totalRows == 0)
            {
                divInfo = "<div class='noData'>No Fees Settings available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divFeesSettingsList.Controls.Add(new LiteralControl(divInfo));
                return;
            }

            divInfo = " <table id='tblDesignationList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Class Name</th>";
            divInfo += "<th>Fees Type</th>";
            divInfo += "<th>Fees Amount</th>";
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {

                id = dt.Rows[x]["FSId"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td >" + dt.Rows[x]["ClassName"].ToString() + "</td>";
                divInfo += "<td >" + dt.Rows[x]["FType"].ToString() + "</td>";
                divInfo += "<td >" + dt.Rows[x]["FAmount"].ToString() + "</td>";

                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editFeesSettings(" + id + ");'  />";
            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";

            divInfo += "</table>";
            divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

            divFeesSettingsList.Controls.Add(new LiteralControl(divInfo));

        }
    }
}