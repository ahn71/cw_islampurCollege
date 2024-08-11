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
    public partial class AddDepartment : System.Web.UI.Page
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
                    LoadDepartment("");
                }
            }
        }
        private Boolean saveDepartments()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("saveDepartments", sqlDB.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DName", txtDepartment.Text.Trim());
                if (chkStatus.Checked == true) cmd.Parameters.AddWithValue("@DStatus", 1);                
                else cmd.Parameters.AddWithValue("@DStatus", 0);

                if (chkIsTeacher.Checked == true) cmd.Parameters.AddWithValue("@IsTeacher", 1);
                else cmd.Parameters.AddWithValue("@IsTeacher", 0);

                int result = (int)
                cmd.ExecuteScalar();


                if (result > 0)
                {
                    lblMessage.InnerText = "success->Successfully saved";
                    txtDepartment.Text = "";
                    LoadDepartment("");
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

        private void LoadDepartment(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select *  from Departments_HR  Order by DId ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);

            int totalRows = dt.Rows.Count;
            string divInfo = "";

            if (totalRows == 0)
            {
                divInfo = "<div class='noData'>No Class available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divDepartmentList.Controls.Add(new LiteralControl(divInfo));
                return;
            }

            divInfo = " <table id='tblClassList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Department Name</th>";
            divInfo += "<th>Status</th>";
            divInfo += "<th>IsTeacher</th>";
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                id = dt.Rows[x]["DId"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td >" + dt.Rows[x]["DName"].ToString() + "</td>";
                divInfo += "<td>" + dt.Rows[x]["DStatus"].ToString() + "</td>";
                divInfo += "<td>" + dt.Rows[x]["IsTeacher"].ToString() + "</td>";
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editDepartment(" + id + ");'  />";
            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";

            divInfo += "</table>";
            divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

            divDepartmentList.Controls.Add(new LiteralControl(divInfo));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblDepartmentId.Value.ToString().Length == 0)
                saveDepartments();
            else
                updateDepartments();
        }
        private Boolean updateDepartments()
        {
            try
            {

                SqlCommand cmd = new SqlCommand(" update Departments_HR  Set DName=@DName, DStatus=@DStatus, IsTeacher=@IsTeacher where DId=@DId ", sqlDB.connection);
                cmd.Parameters.AddWithValue("@DId", lblDepartmentId.Value.ToString());
                cmd.Parameters.AddWithValue("@DName", txtDepartment.Text.Trim());

                if (chkStatus.Checked == true) cmd.Parameters.AddWithValue("@DStatus", 1);
                else cmd.Parameters.AddWithValue("@DStatus", 0);

                if (chkIsTeacher.Checked == true) cmd.Parameters.AddWithValue("@IsTeacher", 1);
                else cmd.Parameters.AddWithValue("@IsTeacher", 0);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    lblMessage.InnerText = "success->Successfully Updated";
                    LoadDepartment("");
                    btnSave.Text = "Save";
                    txtDepartment.Text = "";
                    chkStatus.Checked = true;
                }

                return true;

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

    }
}