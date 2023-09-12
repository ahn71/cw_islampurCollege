using DS.BLL.Admission;
using DS.BLL.Examinition;
using DS.BLL.GeneralSettings;
using DS.BLL.HR;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.BLL.StudentGuide;
using DS.PropertyEntities.Model.Examinition;
using DS.PropertyEntities.Model.StudentGuide;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academic.Examination
{
    public partial class MonthlyTest : System.Web.UI.Page
    {
        CurrentStdEntry crntStd;
        Tbl_Exam_MontlyTestEntry tbl_exam_monthlytest;
        Tbl_Exam_MonthlyTestDetailsEntry tbl_exam_monthlytestdtl;        
        List<Tbl_Exam_MonthlyTestDetailsEntities> exammonthlytestlist = new List<Tbl_Exam_MonthlyTestDetailsEntities>();
        private int result = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ShiftEntry.GetDropDownList(ddlShiftList);
                BatchEntry.GetDropdownlist(ddlBatch, true);                
            }
            lblMessage.InnerText = "";
        }
        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {


            ddlSection.Items.Clear();
            BatchEntry.loadGroupByBatchId(ddlgroup, ddlBatch.SelectedValue.ToString());

            if (ddlgroup.Items.Count == 1)
            {
                //divGroup.Visible = false;
                // ClassSectionEntry.GetSectionListByBatchId(ddlSection, ddlBatch.SelectedValue.ToString());
                ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection, ddlBatch.SelectedValue.ToString(), ddlgroup.SelectedItem.Value);
            }
            else
            {
                //ddlgroup.Enabled = true;
                //divGroup.Visible = true;

            }
            ExamInfoEntry.GetExamIdList(ddlExamId, ddlBatch.SelectedValue.ToString(),false);

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

                 
        private void LoadUnAssignStudentList()
        {
            try
            {
                if (crntStd == null)
                {
                    crntStd = new CurrentStdEntry();
                }
                DataTable dt = new DataTable();
                dt = crntStd.GetUnassignStudentListM(ddlShiftList.SelectedValue, ddlBatch.SelectedValue, ddlgroup.SelectedValue, ddlSection.SelectedValue);

                DataColumn dc = new DataColumn("Obtainmarks");
                dc.DataType = typeof(int);
                dc.DefaultValue = 0;

                dt.Columns.Remove("Gender");
                dt.Columns.Add(dc);
                
                gvStudentList.DataSource = dt;
                gvStudentList.DataBind();
                gvStudentList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch { }
        }
       
        private void SubmitMonthlyTest()
        {
            try
            {
                tbl_exam_monthlytest = new Tbl_Exam_MontlyTestEntry();
                tbl_exam_monthlytestdtl = new Tbl_Exam_MonthlyTestDetailsEntry();
                Tbl_Exam_MontlyTestEntities entities = new Tbl_Exam_MontlyTestEntities();
                entities.ShiftId = int.Parse(ddlShiftList.SelectedValue);
                entities.BatchId = int.Parse(ddlBatch.SelectedValue);
                entities.ClsGrpID = int.Parse(ddlgroup.SelectedValue);
                entities.ClsSecId = int.Parse(ddlSection.SelectedValue);
                entities.ExInId = ddlExamId.SelectedValue;
                entities.Patternmarks = float.Parse(txtpatternmarks.Text);
                entities.Passmarks = float.Parse(txtpassmarks.Text);
                tbl_exam_monthlytest.AddEntities = entities;
                tbl_exam_monthlytest.Delete();
                result = tbl_exam_monthlytest.Insert();
                foreach (GridViewRow row in gvStudentList.Rows)
                {
                   
                        HiddenField stdId = row.FindControl("hidestdID") as HiddenField;
                        Label txtroll = row.FindControl("lblRoll") as Label;
                        TextBox txtobtainmarks = row.FindControl("txtobtainedmarks") as TextBox;
                        Tbl_Exam_MonthlyTestDetailsEntities entities2 = new Tbl_Exam_MonthlyTestDetailsEntities();
                        entities2.MonthlyTest_Id = result;
                        entities2.StudentId = int.Parse(gvStudentList.DataKeys[row.RowIndex].Value.ToString());
                        entities2.RollNo = int.Parse(txtroll.Text);
                        entities2.Obtainmarks = float.Parse(txtobtainmarks.Text == "" ? "0" : txtobtainmarks.Text);
                        tbl_exam_monthlytestdtl.AddEntities = entities2;
                        bool result2 = tbl_exam_monthlytestdtl.Insert();
                }               
            }
            catch { }
        }
        

        protected void ddlExamId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbl_exam_monthlytestdtl == null)
            {
                tbl_exam_monthlytestdtl = new Tbl_Exam_MonthlyTestDetailsEntry();
            }
            DataTable dt=new DataTable();
            dt = tbl_exam_monthlytestdtl.GetMonthlyTest(ddlShiftList.SelectedValue,ddlBatch.SelectedValue,ddlgroup.SelectedValue,ddlSection.SelectedValue,ddlExamId.SelectedValue);
            if(dt==null)
            {
                LoadUnAssignStudentList();
                txtpassmarks.Text = "0";
                txtpatternmarks.Text = "0";
            }
            else if (dt.Rows.Count==0)
            {
                LoadUnAssignStudentList();
                txtpassmarks.Text = "0";
                txtpatternmarks.Text = "0";
            }
            else
            {
                gvStudentList.DataSource = dt;
                gvStudentList.DataBind();
                gvStudentList.HeaderRow.TableSection = TableRowSection.TableHeader;
                txtpassmarks.Text = dt.Rows[0]["Passmarks"].ToString();
                txtpatternmarks.Text = dt.Rows[0]["Patternmarks"].ToString();
            }
           
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loadStudentInfo();", true);
        }
        protected void btnAssign_Click(object sender, EventArgs e)
        {

            SubmitMonthlyTest();
            if (tbl_exam_monthlytestdtl == null)
            {
                tbl_exam_monthlytestdtl = new Tbl_Exam_MonthlyTestDetailsEntry();
            }
            DataTable dt = new DataTable();
            dt = tbl_exam_monthlytestdtl.GetMonthlyTest(ddlShiftList.SelectedValue, ddlBatch.SelectedValue, ddlgroup.SelectedValue, ddlSection.SelectedValue, ddlExamId.SelectedValue);
            gvStudentList.DataSource = dt;
            gvStudentList.DataBind();
            gvStudentList.HeaderRow.TableSection = TableRowSection.TableHeader;
            txtpassmarks.Text = dt.Rows[0]["Passmarks"].ToString();
            txtpatternmarks.Text = dt.Rows[0]["Patternmarks"].ToString();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loadMessage();", true);
        }
    }
}