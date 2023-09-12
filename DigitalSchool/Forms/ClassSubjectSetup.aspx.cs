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
    public partial class ClassSubjectSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    Classes.commonTask.LoadSubClass(ddlClassName);
                    Classes.commonTask.LoadSubject(ddlSubject);
                    LoadClassList();
                    loadClassSubjectDetails("");

                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ClassSubValidation() == false)
            {
                loadClassSubjectDetails("");
                return;
            }

            if (CSId.Value.ToString().Length == 0)
            {
                if(saveClassSubject()==true)
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SaveSuccess();", true);
            }
            else
            {
                if(updateClassSubject()==true)
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
            }
        }
        private Boolean ClassSubValidation()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select ClassID,SubId From ClassSubject where ClassID=" + ddlClassName.SelectedValue + " and SubId="+ddlSubject.SelectedValue+"", dt);
                if (dt.Rows.Count > 0)
                {
                    lblMessage.InnerText = "warning->Already Inserted " + ddlSubject.SelectedItem.Text;
                    return false;
                }
                return true;
            }
            catch { return false; }
        }
        private void LoadClassList()
        {
            DataTable dt = new DataTable();
            sqlDB.fillDataTable("Select Distinct ClassID,ClassName From v_ClassSubject ", dt);
            ddlClassList.DataSource = dt;
            ddlClassList.DataTextField = "ClassName";
            ddlClassList.DataValueField = "ClassID";
            ddlClassList.DataBind();
        }
        private Boolean saveClassSubject()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into  ClassSubject values (@ClassID, @SubId,@DependencySubId,@SubCode) ", sqlDB.connection);

                cmd.Parameters.AddWithValue("@ClassID", ddlClassName.SelectedValue);
                cmd.Parameters.AddWithValue("@SubId", ddlSubject.SelectedValue);
                if (trDependencysub.Visible == true)
                {
                    cmd.Parameters.AddWithValue("@DependencySubId", ddldependencysub.SelectedValue);
                }
                else cmd.Parameters.AddWithValue("@DependencySubId", 0);
                if (txtSubCode.Text.ToString().Length == 0) cmd.Parameters.AddWithValue("@SubCode", 0);
                else cmd.Parameters.AddWithValue("@SubCode", txtSubCode.Text.Trim());

                int result = (int)cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    loadClassSubjectDetails("");
                    return true;
                }
                else
                {
                    LoadClassList();
                    loadClassSubjectDetails("");
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
        private Boolean updateClassSubject()
        {
            try
            {
                SqlCommand cmd = new SqlCommand(" update ClassSubject  Set ClassID=@ClassID, SubId=@SubId where CSId=@CSId ", sqlDB.connection);

                cmd.Parameters.AddWithValue("@CSId",CSId.Value.ToString());
                cmd.Parameters.AddWithValue("@ClassID", ddlClassName.SelectedValue);
                cmd.Parameters.AddWithValue("@SubId", ddlSubject.SelectedValue);
                int result=(int)cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    loadClassSubjectDetails("");
                    return true;
                }
                else
                {
                    loadClassSubjectDetails("");
                    lblMessage.InnerText = "error-> Unable to Update";
                    return false;
                }

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private void loadClassSubjectDetails(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select * from V_ClassSubject where ClassID="+ddlClassList.SelectedValue+" Order by CSId ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);

            int totalRows = dt.Rows.Count;
            string divInfo = "";

            if (totalRows == 0)
            {
                divInfo = "<div class='noData'>No Data available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divClassSubject.Controls.Add(new LiteralControl(divInfo));
                return;
            }

            divInfo = " <table id='tblClassList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Class Name</th>";
            divInfo += "<th>Subject Name</th>";
            divInfo += "<th>Edit</th>";

            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                id = dt.Rows[x]["CSId"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td >" + dt.Rows[x]["ClassName"].ToString() + "</td>";
                divInfo += "<td >" + dt.Rows[x]["SubName"].ToString() + "</td>";
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editClassSubject(" + id + ");'  />";
            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";

            divInfo += "</table>";

            divClassSubject.Controls.Add(new LiteralControl(divInfo));

        }

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            LoadDependencySub();
        }
        private void LoadDependencySub()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select Dependency From NewSubject where SubId="+ddlSubject.SelectedValue+"", dt);
                if (dt.Rows[0]["Dependency"].ToString() == "True")
                {
                    trDependencysub.Visible = true;
                    sqlDB.fillDataTable("Select SubId,SubName From NewSubject where Dependency='False'", dt = new DataTable());
                    ddldependencysub.DataSource = dt;
                    ddldependencysub.DataValueField = "SubId";
                    ddldependencysub.DataTextField = "SubName";
                    ddldependencysub.DataBind();

                }
                else trDependencysub.Visible = false;
            }
            catch { }
        }

        protected void ddlClassList_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadClassSubjectDetails("");
        }

        protected void ddlClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadClassSubjectDetails("");
            for (byte i = 0; i < ddlClassList.Items.Count; i++)
            {
                if (ddlClassName.SelectedItem.Text == ddlClassList.Items[i].Text)
                {
                    ddlClassList.SelectedIndex = i;
                }
            }
        }
    }
}