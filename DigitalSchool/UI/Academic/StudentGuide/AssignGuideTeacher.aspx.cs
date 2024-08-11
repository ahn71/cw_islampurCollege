using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using DS.BLL.HR;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.BLL.StudentGuide;
using DS.PropertyEntities.Model.StudentGuide;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academic.StudentGuide
{
    public partial class AssignGuideTeacher : System.Web.UI.Page
    {
        CurrentStdEntry crntStd;
        protected void Page_Load(object sender, EventArgs e)
        {

                if (!IsPostBack)
                {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AssignGuideTeacher.aspx", btnSearch)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                ShiftEntry.GetDropDownList(ddlShiftList);
                    BatchEntry.GetDropdownlist(ddlBatch, true);
                    EmployeeEntry.LoadTeacher(ddlTeacher);
                }
            lblMessage.InnerText = "";
        }
        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {


            ddlSection.Items.Clear();
            BatchEntry.loadGroupByBatchId(ddlgroup, ddlBatch.SelectedValue.ToString());

            if (ddlgroup.Items.Count == 1)
            {
                divGroup.Visible = false;
                // ClassSectionEntry.GetSectionListByBatchId(ddlSection, ddlBatch.SelectedValue.ToString());
                ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection, ddlBatch.SelectedValue.ToString(), ddlgroup.SelectedItem.Value);
            }
            else
            {
                ddlgroup.Enabled = true;
                divGroup.Visible = true;

            }

        }

        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection, ddlBatch.SelectedValue.ToString(), ddlgroup.SelectedItem.Value);
        }

        protected void gvStudentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
                    e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                }
            }
            catch { }
        }

        protected void hdChk_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)gvStudentList.HeaderRow.FindControl("hdChk");
                if (chk.Checked)
                {
                    foreach (GridViewRow row in gvStudentList.Rows)
                    {
                        chk = (CheckBox)row.Cells[5].FindControl("chkStatus");
                        chk.Checked = true;                      
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvStudentList.Rows)
                    {
                        chk = (CheckBox)row.Cells[5].FindControl("chkStatus");
                        chk.Checked = false;  
                       
                    }
                }


            }
            catch { }
        }

        protected void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
                int index_row = gvr.RowIndex;

                CheckBox chk = (CheckBox)gvStudentList.Rows[index_row].Cells[5].FindControl("chkStatus");

                byte Action = (chk.Checked) ? (byte)1 : (byte)0;
               
                //--for checked and select header rows----------------------------------------
                byte checkedRowsAmount = 0;
                CheckedRowsAmount(5, "chkStatus", out  checkedRowsAmount);
                chk = (CheckBox)gvStudentList.HeaderRow.FindControl("hdChk");

                if (checkedRowsAmount == gvStudentList.Rows.Count)
                {

                    chk.Checked = true;
                }
                else { chk.Checked = false; }
                //----------------------------------------------------------------------------
            }
            catch { }
        }
        private void CheckedRowsAmount(byte cIndex, string ControlName, out byte checkedRowsAmount)
        {
            try
            {
                byte i = 0;
                foreach (GridViewRow gvr in gvStudentList.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.Cells[cIndex].FindControl(ControlName);
                    if (chk.Checked) i++;
                }
                checkedRowsAmount = i;
            }
            catch { checkedRowsAmount = 0; }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadUnAssignStudentList();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loadStudentInfo();", true);
        }
        private void LoadUnAssignStudentList()
        {
            try
            {
                if (crntStd == null)
                {
                    crntStd = new CurrentStdEntry();
                }
                DataTable dt = new DataTable();
                dt = crntStd.GetUnassignStudentList(ddlShiftList.SelectedValue, ddlBatch.SelectedValue, ddlgroup.SelectedValue, ddlSection.SelectedValue);
                gvStudentList.DataSource = dt;
                gvStudentList.DataBind();
                gvStudentList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch { }
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            AssignTeacher();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loadStudentInfo();", true);
        }
        private void AssignTeacher()
        {
            try
            {
                foreach (GridViewRow row in gvStudentList.Rows)
                {
                    CheckBox chkStatus = row.FindControl("chkStatus") as CheckBox;
                    if (chkStatus.Checked)
                    {
                        HiddenField stdId = row.FindControl("hidestdID") as HiddenField;
                        StudentGuideEntities entities = new StudentGuideEntities();
                        entities.StudentId = int.Parse(stdId.Value);
                        entities.EID = int.Parse(ddlTeacher.SelectedValue);
                        entities.GuideStatus = true;
                        StudentGuideEntry stdEntry = new StudentGuideEntry();
                        stdEntry.AddEntities = entities;
                        bool result= stdEntry.Insert();
                    }
                }
                LoadUnAssignStudentList();
            }
            catch { }
        }

        protected void gvStudentList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadUnAssignStudentList();
            gvStudentList.PageIndex = e.NewPageIndex;
            gvStudentList.DataBind();
        }        
    }
}