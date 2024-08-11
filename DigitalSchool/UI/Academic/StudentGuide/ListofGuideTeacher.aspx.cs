using DS.BLL.ControlPanel;
using DS.BLL.StudentGuide;
using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academic.StudentGuide
{
    public partial class ListofGuideTeacher : System.Web.UI.Page
    {
        StudentGuideEntry stdgdEntry;
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                Button btnSave = new Button();
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ListofGuideTeacher.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                if (stdgdEntry == null)
                    {
                        stdgdEntry = new StudentGuideEntry();
                    }
                    stdgdEntry.LoadDepartment(ddlDept);
                    LoadAdviserList();
                }
            lblMessage.InnerText = "";
        }
        private void LoadAdviserList()
        {
            if (stdgdEntry == null)
            {
                stdgdEntry = new StudentGuideEntry();
            }
            dt = stdgdEntry.LoadAdviserList(ddlDept.SelectedValue);
            ddlAdviserName.DataSource = dt;
            ddlAdviserName.DataTextField = "EName";
            ddlAdviserName.DataValueField = "EID";
            ddlAdviserName.DataBind();
            ddlAdviserName.Items.Insert(0,new ListItem("...Select...","0"));
        }
        protected void gvAdviserList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                string EID = e.CommandArgument.ToString();
                LoadStudentList(EID);
            }
        }
        private void LoadStudentList(string EID)
        {
            try
            {
                if (stdgdEntry == null)
                {
                    stdgdEntry = new StudentGuideEntry();
                }
                dt = new DataTable();
                dt = stdgdEntry.LoadStudentList(EID);
                gvStudentList.DataSource = dt;
                gvStudentList.DataBind();
                gvStudentList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch { }
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAdviserList();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loadStudentInfo();", true);
        }

        protected void ddlAdviserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudentList(ddlAdviserName.SelectedValue);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loadStudentInfo();", true);
        }

        protected void gvStudentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string StudentId = e.CommandArgument.ToString();
                CRUD.ExecuteQuery("Delete FROM tbl_Guide_Teacher WHERE StudentId='"+StudentId+"'");                
            }
        }

        protected void gvStudentList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LoadStudentList(ddlAdviserName.SelectedValue);
        }
      
    }
}