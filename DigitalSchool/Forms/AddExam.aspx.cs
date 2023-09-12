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
    public partial class AddExam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!IsPostBack) loadExam("");
            }
        }

        private void loadExam(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select * from ExamType  Order by ExId ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);

            int totalRows = dt.Rows.Count;
            string divInfo = "";

            if (totalRows == 0)
            {
                divInfo = "<div class='noData'>No Class available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divExamList.Controls.Add(new LiteralControl(divInfo));
                return;
            }

            divInfo = " <table id='tblClassList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Exam Name</th>";
            divInfo += "<th>Dependency</th>";
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                id = dt.Rows[x]["ExId"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td ><span id=exname" + id + ">" + dt.Rows[x]["ExName"].ToString() + "</span></td>";
                if (dt.Rows[x]["ExDependency"].ToString() == "True")
                {
                    divInfo += "<td ><input type='checkbox' id='chkid" + id + "' checked='checked' disabled='disabled' /></td>";
                }
                else
                {
                    divInfo += "<td > <input id='chkid" + id + "' type='checkbox' disabled='disabled' /></td>";
                }

                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editExam(" + id + ");'  />";
            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";

            divInfo += "</table>";
            //divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

            divExamList.Controls.Add(new LiteralControl(divInfo));
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblExId.Value.ToString().Length == 0)
            {
                if (saveExam() == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SavedSuccess();", true);
                }
            }
            else
            {
                if (updateExam() == true)
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
            }
        }

        private Boolean updateExam()
        {
            try
            {

                SqlCommand cmd = new SqlCommand(" update ExamType  Set ExName=@ExName,ExDependency=@ExDependency where ExId=@ExId ", sqlDB.connection);

                cmd.Parameters.AddWithValue("@ExId", lblExId.Value.ToString());
                cmd.Parameters.AddWithValue("@ExName", txtEx_Name.Text.Trim());
                if (chkDependency.Checked)
                {
                    cmd.Parameters.AddWithValue("@ExDependency", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ExDependency", 0);
                }

                cmd.ExecuteNonQuery();
                loadExam("");
                return true;

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private Boolean saveExam()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("saveExam", sqlDB.connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExName", txtEx_Name.Text.Trim());
                if (chkDependency.Checked)
                {
                    cmd.Parameters.AddWithValue("@ExDependency", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ExDependency", 0);
                }

                int result = (int)cmd.ExecuteScalar();
                btnSave.Text = "Save";
                loadExam("");
                if (result > 0)
                {
              
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
    }
}