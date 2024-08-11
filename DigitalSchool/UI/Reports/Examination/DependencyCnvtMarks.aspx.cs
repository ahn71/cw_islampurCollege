using DS.BLL.Examinition;
using DS.BLL.Finance;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.Examination
{
    public partial class DependencyCnvtMarks : System.Web.UI.Page
    {
        FeesCollectionEntry fc;
        Class_ClasswiseMarksheet_TotalResultProcess_Entry totalResult;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShiftEntry.GetDropDownList(ddlShiftList);
                ddlShiftList.Items.Insert(1,new ListItem("All","All"));
                BatchEntry.GetDropdownlist(ddlBatch, "True");
            }
            lblMessage.InnerText = "";
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlSection.Items.Clear();
                ddlRoll.Items.Clear();
                ddlgroup.Items.Clear();
                string[] batchID = ddlBatch.SelectedValue.Split('_');
                BatchEntry.loadGroupByBatchId(ddlgroup, batchID[0]);                

                if (ddlgroup.Items.Count == 1)
                {
                    ddlgroup.Items.Insert(1, new ListItem("All", "All"));
                    divGroup.Visible = false;
                    ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection, batchID[0], ddlgroup.SelectedItem.Value);
                    ddlSection.Items.Insert(1,new ListItem("All","All"));
                }
                else
                {
                    ddlgroup.Items.Insert(1, new ListItem("All", "All"));
                    ddlgroup.Enabled = true;
                    divGroup.Visible = true;

                }
            }
            catch { }
        }

        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBatch.SelectedValue == "All")
            {

                ddlSection.Items.Clear();
                ddlSection.Items.Insert(0,new ListItem("All","All"));
                ddlRoll.Items.Clear();
                ddlRoll.Items.Insert(0,new ListItem("All","All"));
                return;
            }
            string[] batchID = ddlBatch.SelectedValue.Split('_');
            ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection, batchID[0], ddlgroup.SelectedItem.Value);
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlRoll.Items.Clear();
                if (ddlSection.SelectedValue == "All")
                {
                    ddlRoll.Items.Insert(0,new ListItem("All","All"));
                    return;
                }
                string condition = "";
                if (fc == null)
                {
                    fc = new FeesCollectionEntry();
                }
                condition = fc.GetSearchCondition(ddlShiftList.SelectedValue, ddlBatch.SelectedValue, ddlgroup.SelectedValue, ddlSection.SelectedValue);
                
                DataTable dt = new DataTable();
                string className = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                dt = CRUD.ReturnTableNull("SELECT DISTINCT StudentID, RollNo FROM "
                + "Class_" + className + "MarksSheet "+condition+"");
                ddlRoll.DataSource = dt;
                ddlRoll.DataTextField = "RollNo";
                ddlRoll.DataValueField = "StudentID";
                ddlRoll.DataBind();
                ddlRoll.Items.Insert(0,new ListItem("...Select...","0"));
                ddlRoll.Items.Insert(1, new ListItem("All", "All"));

            }
            catch { }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadDependencyCnvtMarks();
        }
        private void LoadDependencyCnvtMarks()
        {
            try
            {
                if (totalResult == null)
                {
                    totalResult = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
                }
                string[] batchID = ddlBatch.SelectedValue.Split('_');
                string condition="";
                if (ddlShiftList.SelectedValue != "All")
                {
                    condition = "WHERE clsMS.ShiftId='" + ddlShiftList.SelectedValue + "'";
                }                
                if (ddlSection.SelectedValue != "All")
                {
                    if (condition != "")
                    {
                        condition += " AND clsMS.ClsSecId='" + ddlSection.SelectedValue + "'";
                    }
                    else
                    {
                        condition = " WHERE clsMS.ClsSecId='" + ddlSection.SelectedValue + "'";
                    }
                }
                if (ddlRoll.SelectedValue != "All")
                {
                    if (condition != "")
                    {
                        condition += " AND clsMS.StudentId='" + ddlRoll.SelectedValue + "'";
                    }
                    else
                    {
                        condition = " WHERE clsMS.StudentId='" + ddlRoll.SelectedValue + "'";
                    }
                }
                if (ddlgroup.Enabled == true)
                {
                    if (ddlgroup.SelectedValue != "All")
                    {
                        if (condition != "")
                        {
                            condition += " AND clsMS.clsgrpID='" + ddlgroup.SelectedValue + "'";
                        }
                        else
                        {
                            condition = " WHERE clsMS.clsgrpID='" + ddlgroup.SelectedValue + "'";
                        }
                    }
                }
                if (condition != "")
                {
                    condition += " AND clsMS.BatchID='" + batchID[0] + "' AND clsMS.ConvertToPercentage>0";
                }
                else
                {
                    condition = " WHERE clsMS.BatchID='" + batchID[0] + "' AND clsMS.ConvertToPercentage>0";
                }
                string className = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                DataTable dt = totalResult.LoadDependencyCnvtMarksList(className,condition);
                if (dt.Rows.Count > 0)
                {
                    Session["__DependencyCnvtMarks__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DependencyCnvtMarks');", true);  //Open New Tab for Sever side code
                }
                else
                {
                    lblMessage.InnerText = "warning->No Dependency Exam";
                }
            }
            catch { }
        }
    }
}