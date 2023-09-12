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
    public partial class NewSubject : System.Web.UI.Page
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
                    LoadCompulsorySubject("");
                    LoadOptionalSubject("");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblSubId.Value.ToString().Length == 0)
            {
                if(saveNewSubject()==true)
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SaveSuccess();", true);
            }
            else
            {
                if(updateNewSubject()==true)
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
            }
        }
        private Boolean saveNewSubject()
        {
            try
            {

                SqlCommand cmd = new SqlCommand("Insert into  NewSubject  values(@SubName,@SubTotalMarks,@SubUnit,@IsOptional,@Ordering,@Dependency) ", sqlDB.connection);
                cmd.Parameters.AddWithValue("@SubName", txtSubName.Text.Trim());
                cmd.Parameters.AddWithValue("@SubTotalMarks", txtSubTotalMarks.Text.Trim());
                cmd.Parameters.AddWithValue("@SubUnit", 0);
                if (chkIsOptional.Checked)
                cmd.Parameters.AddWithValue("@IsOptional", 1);
                else
                cmd.Parameters.AddWithValue("@IsOptional",0);
                if (txtOrder.Text.ToString().Length == 0) cmd.Parameters.AddWithValue("@Ordering",0);
                else  cmd.Parameters.AddWithValue("@Ordering",txtOrder.Text.Trim());
                if (chkdependency.Checked)
                {
                    cmd.Parameters.AddWithValue("@Dependency",1);
                }
                else cmd.Parameters.AddWithValue("@Dependency", 0);
                int result = (int)cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    lblMessage.InnerText = "success->Successfully saved";
                    LoadCompulsorySubject("");
                    LoadOptionalSubject("");
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
        private Boolean updateNewSubject()
        {
            try
            {
                SqlCommand cmd = new SqlCommand(" update NewSubject  Set SubName=@SubName, SubTotalMarks=@SubTotalMarks, SubUnit=@SubUnit, IsOptional=@IsOptional,Ordering=@Ordering,Dependency=@Dependency where SubId=@SubId ", sqlDB.connection);
                cmd.Parameters.AddWithValue("@SubId", lblSubId.Value.ToString());
                cmd.Parameters.AddWithValue("@SubName", txtSubName.Text.Trim());
                cmd.Parameters.AddWithValue("@SubTotalMarks", txtSubTotalMarks.Text.Trim());
                cmd.Parameters.AddWithValue("@SubUnit", 0);
                if(chkIsOptional.Checked)
                cmd.Parameters.AddWithValue("@IsOptional",1);
                else
                cmd.Parameters.AddWithValue("@IsOptional",0);
                if (txtOrder.Text.ToString().Length == 0) cmd.Parameters.AddWithValue("@Ordering", 0);
                else cmd.Parameters.AddWithValue("@Ordering", txtOrder.Text.Trim());
                if (chkdependency.Checked)
                {
                    cmd.Parameters.AddWithValue("@Dependency", 1);
                }
                else cmd.Parameters.AddWithValue("@Dependency", 0);
                int result=(int)cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    lblMessage.InnerText = "success->Successfully Updated";
                    LoadCompulsorySubject("");
                    LoadOptionalSubject("");
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
        private void LoadCompulsorySubject(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select SubId,SubName,Dependency,SubTotalMarks,Ordering from NewSubject where IsOptional='false'  Order by Ordering ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);
            int totalRows = dt.Rows.Count;
            string divInfo = "";
            if (totalRows == 0)
            {
                divInfo = "<div class='noData'>Compulsory Subject not available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divSubjectList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            divInfo = " <table id='tblClassList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Subject Name</th>";
            divInfo += "<th style='text-align:center'>Marks</th>";
            divInfo += "<th style='width:70px;text-align:center'>Order</th>";
            divInfo += "<th style='width:60px;text-align:center'>Dependency</th>";
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            string id = "";
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                id = dt.Rows[x]["SubId"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td >" + dt.Rows[x]["SubName"].ToString() + "</td>";
                divInfo += "<td style='text-align:center'>" + dt.Rows[x]["SubTotalMarks"].ToString() + "</td>";
                divInfo += "<td style='width:70px;text-align:center'>" + dt.Rows[x]["Ordering"].ToString() + "</td>";
                if (dt.Rows[x]["Dependency"].ToString() == "True")
                {
                    divInfo += "<td style='width:60px;text-align:center'><input type='checkbox' id='chkid" + id + "' checked='checked' disabled='disabled' /></td>";
                }
                else divInfo += "<td style='width:60px;text-align:center'> <input id='chkid" + id + "' type='checkbox' disabled='disabled' /></td>";

                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editSubject(" + id + ");'  />";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divSubjectList.Controls.Add(new LiteralControl(divInfo));
        }
        private void LoadOptionalSubject(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select SubId,SubName,Dependency,SubTotalMarks,Ordering from NewSubject where IsOptional='true'  Order by Ordering ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);
            int totalRows = dt.Rows.Count;
            string divInfo = "";
            if (totalRows == 0)
            {
                divInfo = "<div class='noData'>Optional Subject not available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divOptionlist.Controls.Add(new LiteralControl(divInfo));
                return;
            }

            divInfo = " <table id='tblClassList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Subject Name</th>";
            divInfo += "<th style='text-align:center'>Marks</th>";
            divInfo += "<th style='width:60px;text-align:center'>Order</th>";
            divInfo += "<th style='width:60px;text-align:center'>Dependency</th>";
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            string id = "";
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                id = dt.Rows[x]["SubId"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td >" + dt.Rows[x]["SubName"].ToString() + "</td>";
                divInfo += "<td style='text-align:center'>" + dt.Rows[x]["SubTotalMarks"].ToString() + "</td>";
                divInfo += "<td style='width:60px;text-align:center'>" + dt.Rows[x]["Ordering"].ToString() + "</td>";
                if (dt.Rows[x]["Dependency"].ToString() == "True")
                {
                    divInfo += "<td style='width:60px;text-align:center'><input type='checkbox' id='chkid" + id + "' checked='checked' disabled='disabled' /></td>";
                }
                else divInfo += "<td style='width:60px;text-align:center'> <input id='chkid" + id + "' type='checkbox' disabled='disabled' /></td>";

                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editOptionlSubject(" + id + ");'  />";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divOptionlist.Controls.Add(new LiteralControl(divInfo));
        }

    }
}