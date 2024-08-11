using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using System.Data.SqlClient;
using System.Data;

namespace DS
{
    public partial class StudentFineSetting : System.Web.UI.Page
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
                    LoadFinePurpose();
                    loadStudentFineSettings("");
                }
            }
        }
        private void LoadFinePurpose()
        {
            sqlDB.bindDropDownList("Select FinePurpose From FinePurposeEntry", "FinePurpose", ddlFinePurpose);
        }
        private Boolean saveStudentFineSettings()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into  StudentFineSettings values (@FinePurpose, @Amount) ", sqlDB.connection);
                cmd.Parameters.AddWithValue("@FinePurpose", ddlFinePurpose.Text.Trim());
                cmd.Parameters.AddWithValue("@Amount", txtAmount.Text.Trim());
                int result = (int)cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    lblMessage.InnerText = "success->Successfully saved";
                    loadStudentFineSettings("");
                    return true;
                }
                else
                {
                    lblMessage.InnerText = "error->Unable to save";
                    return false;
                }

               

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private Boolean updateStudentFineSettings()
        {
            try
            {
                SqlCommand cmd = new SqlCommand(" update StudentFineSettings  Set FinePurpose=@FinePurpose, Amount=@Amount where SFSId=@SFSId ", sqlDB.connection);
                cmd.Parameters.AddWithValue("@SFSId", lblSFSId.Value.ToString());
                cmd.Parameters.AddWithValue("@FinePurpose", ddlFinePurpose.Text.Trim());
                cmd.Parameters.AddWithValue("@Amount", txtAmount.Text.Trim());
                int result=(int)cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    lblMessage.InnerText = "success->Successfully Updated";
                    loadStudentFineSettings("");
                    return true;
                }
                else
                {
                    lblMessage.InnerText = "error->Unable to Update";
                    return false;
                }

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private void loadStudentFineSettings(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select SFSId, FinePurpose,Amount from StudentFineSettings  Order by SFSId ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);
            int totalRows = dt.Rows.Count;
            string divInfo = "";
            if (totalRows == 0)
            {
                divInfo = "<div class='noData'>No  Student Fine Settings</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divStudentFineSettings.Controls.Add(new LiteralControl(divInfo));
                return;
            }

            divInfo = " <table id='tblDesignationList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Fine Purpose</th>";
            divInfo += "<th>Fine Amount</th>";
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                id = dt.Rows[x]["SFSId"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td >" + dt.Rows[x]["FinePurpose"].ToString() + "</td>";
                divInfo += "<td >" + dt.Rows[x]["Amount"].ToString() + "</td>";
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editFineAmount(" + id + ");'  />";
            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";         
            divStudentFineSettings.Controls.Add(new LiteralControl(divInfo));

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblSFSId.Value.ToString().Length == 0)
                saveStudentFineSettings();
            else
            {
                updateStudentFineSettings();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
            }
        }
    }
}