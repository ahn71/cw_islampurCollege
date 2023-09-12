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
    public partial class AddDepartment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                lblMessage.InnerText = "";
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddDepartment.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    LoadDepartment("");
                }
        }
        private Boolean saveDepartments()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("saveDepartments",DbConnection.Connection );
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
                    lblMessage.InnerText = "success->Save Successfully";
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
            dt = CRUD.ReturnTableNull(sqlcmd);

            int totalRows = dt.Rows.Count;
            string divInfo = "";

            divInfo = " <table id='tblClassList' class='table table-striped table-bordered dt-responsive nowrap'cellspacing='0' Width='100%' > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Department Name</th>";
            divInfo += "<th>Status</th>";
            divInfo += "<th>IsTeacher</th>";
            if (Session["__Update__"].ToString().Equals("true"))
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            if (totalRows == 0)
            {
                divInfo += "</tbody></table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divDepartmentList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                id = dt.Rows[x]["DId"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td >" + dt.Rows[x]["DName"].ToString() + "</td>";
                divInfo += "<td>" + dt.Rows[x]["DStatus"].ToString() + "</td>";
                divInfo += "<td>" + dt.Rows[x]["IsTeacher"].ToString() + "</td>";
                if (Session["__Update__"].ToString().Equals("true"))
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
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loaddatatable();", true); 
            if (lblDepartmentId.Value.ToString().Length == 0)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; LoadDepartment(""); return; }
                 saveDepartments();
            }                
            else
                updateDepartments();
        }
        private Boolean updateDepartments()
        {
            try
            {

                SqlCommand cmd = new SqlCommand(" update Departments_HR  Set DName=@DName, DStatus=@DStatus, IsTeacher=@IsTeacher where DId=@DId ", DbConnection.Connection);
                cmd.Parameters.AddWithValue("@DId", lblDepartmentId.Value.ToString());
                cmd.Parameters.AddWithValue("@DName", txtDepartment.Text.Trim());

                if (chkStatus.Checked == true) cmd.Parameters.AddWithValue("@DStatus", 1);
                else cmd.Parameters.AddWithValue("@DStatus", 0);

                if (chkIsTeacher.Checked == true) cmd.Parameters.AddWithValue("@IsTeacher", 1);
                else cmd.Parameters.AddWithValue("@IsTeacher", 0);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    lblMessage.InnerText = "success->Update Successfully";
                    LoadDepartment("");
                    lblDepartmentId.Value = "";
                    btnSave.Text = "Save";
                    txtDepartment.Text = "";
                    chkStatus.Checked = true;
                    chkIsTeacher.Checked = true;
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