using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Forms
{
    public partial class QuestionPattern : System.Web.UI.Page
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
                    loadQuestionPattern("");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblQPId.Value.ToString().Length == 0)
            {
               if(saveQuestionPattern()==true)
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SaveSuccess();", true);
            }
            else
            {
               if(updateQuestionPattern()==true)
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
            }
        }
        private Boolean saveQuestionPattern()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into  QuestionPattern values (@QPName) ", sqlDB.connection);
                cmd.Parameters.AddWithValue("@QPName", txtQPName.Text.Trim());
                int result = (int)cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    lblMessage.InnerText = "success->Successfully saved";
                    loadQuestionPattern("");
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
        private Boolean updateQuestionPattern()
        {
            try
            {

                SqlCommand cmd = new SqlCommand(" update QuestionPattern  Set QPName=@QPName where QPId=@QPId ", sqlDB.connection);
                cmd.Parameters.AddWithValue("@QPId", lblQPId.Value.ToString());
                cmd.Parameters.AddWithValue("@QPName", txtQPName.Text.Trim());
                int result=(int)cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    lblMessage.InnerText = "success->Successfully Updated";
                    loadQuestionPattern("");
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
        private void loadQuestionPattern(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select * from QuestionPattern  Order by QPId ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);

            int totalRows = dt.Rows.Count;
            string divInfo = "";

            if (totalRows == 0)
            {
                divInfo = "<div class='noData'>No Question Pattern available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divQuestionPattern.Controls.Add(new LiteralControl(divInfo));
                return;
            }

            divInfo = " <table id='tblClassList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Question Pattern Name</th>";
            divInfo += "<th>Edit</th>";

            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                id = dt.Rows[x]["QPId"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td >" + dt.Rows[x]["QPName"].ToString() + "</td>";
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editQuestionPattern(" + id + ");'  />";
            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divQuestionPattern.Controls.Add(new LiteralControl(divInfo));

        }
    }
}