using adviitRuntimeScripting;
using DS.BLL.Admission;
using DS.BLL.Examinition;
using DS.BLL.Finance;
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

namespace DS.UI.Administration.Finance
{
    public partial class AllowStudentForPayment : System.Web.UI.Page
    {
        ClassGroupEntry clsgrpEntry;
        CurrentStdEntry currentstdEntry;
        AcccountsettingEntry accountsettingEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BatchEntry.GetDropdownlist(ddlBatch, "True");
                UncheckAllCheckboxes();
            }
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
                if (clsgrpEntry == null)
                {
                    clsgrpEntry = new ClassGroupEntry();
                }
                clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), ddlGroup);

                if (ddlGroup.Enabled == false)
                {
                    //  ExamInfoEntry.GetExamIdListWithoutQuiz(ddlExamId, BatchClsID[0]);
                   // ExamInfoEntry.GetExamIdListWithExInSl(ddlExamId, BatchClsID[0]);
                    ClassSectionEntry.GetEntitiesDataWithAll(ddlSection, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
                }

                LoadBatchwiseFeeCat(ddlBatch.SelectedValue, ddlCategory);

            }
            catch { }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
            // ExamInfoEntry.GetExamIdListWithoutQuiz(ddlExamId, BatchClsID[0], ddlGroup.SelectedValue);
            //ExamInfoEntry.GetExamIdListWithExInSl(ddlExamId, BatchClsID[0], ddlGroup.SelectedValue);
            ClassSectionEntry.GetEntitiesDataWithAll(ddlSection, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadCurrentStudentInfo();
        }

        private void loadCurrentStudentInfo()
        {
            try
            {
                if (ddlBatch.SelectedValue == "0")
                {
                    lblMessage.InnerText = "warning-> please, select any shift.";
                    ddlBatch.Focus();
                    return;
                }
                string conditions = "";
                if (ddlBatch.SelectedValue != "0")
                {
                    string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
                    conditions += " Where BatchId='" + int.Parse(BatchClsID[0]) + "'";
                }
                    


                //conditions += " and ClassId=" + ddlClass.SelectedValue;

                if (ddlGroup.SelectedValue != "0")
                    conditions += " and ClsGrpID=" + ddlGroup.SelectedValue;
                if (ddlSection.SelectedValue != "0")
                    conditions += " and ClsSecID=" + ddlSection.SelectedValue;

           
                DataTable dt = new DataTable();
                if (currentstdEntry == null)
                    currentstdEntry = new CurrentStdEntry();
                dt = currentstdEntry.GetCurrentStudent(conditions);
                gvstudentList.DataSource = dt;
                gvstudentList.DataBind();
            }
            catch { }
        }

        protected void hdChk_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)gvstudentList.HeaderRow.FindControl("hdChk");
                if (chk.Checked)
                {
                    foreach (GridViewRow row in gvstudentList.Rows)
                    {
                        chk = (CheckBox)row.Cells[4].FindControl("chkStatus");
                        chk.Checked = true;

                    }
                }
                else
                {
                    foreach (GridViewRow row in gvstudentList.Rows)
                    {
                        chk = (CheckBox)row.Cells[4].FindControl("chkStatus");
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

                CheckBox chk = (CheckBox)gvstudentList.Rows[index_row].Cells[9].FindControl("chkStatus");

                byte Action = (chk.Checked) ? (byte)1 : (byte)0;

                //--for checked and select header rows----------------------------------------
                byte checkedRowsAmount = 0;
                CheckedRowsAmount(4, "chkStatus", out checkedRowsAmount);
                chk = (CheckBox)gvstudentList.HeaderRow.FindControl("hdChk");

                if (checkedRowsAmount == gvstudentList.Rows.Count)
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
                foreach (GridViewRow gvr in gvstudentList.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.Cells[cIndex].FindControl(ControlName);
                    if (chk.Checked) i++;
                }
                checkedRowsAmount = i;
            }
            catch { checkedRowsAmount = 0; }
        }



        private void SaveData(string categoryId)
        {
            try
            {
                accountsettingEntry = new AcccountsettingEntry();
                foreach (GridViewRow row in gvstudentList.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkStatus = (CheckBox)row.FindControl("chkStatus");
                        if (chkStatus != null && chkStatus.Checked)
                        {
                            string BatchId = gvstudentList.DataKeys[row.RowIndex]["BatchId"].ToString();
                            string StudentId = gvstudentList.DataKeys[row.RowIndex]["StudentId"].ToString();
                            string ClsSecId = gvstudentList.DataKeys[row.RowIndex]["ClsSecId"].ToString();
                            string ClsGrpID = gvstudentList.DataKeys[row.RowIndex]["ClsGrpID"].ToString();



                
                            string AdmissionNo = row.Cells[1].Text.Trim();



                            TextBox txRemarks = (TextBox)row.FindControl("txRemarks");  //Remarks
                            string remarks = txRemarks != null ? txRemarks.Text.Trim() : string.Empty;

                            bool isSucced = accountsettingEntry.InsertStudentPaymentRestriction(BatchId, ClsGrpID, ClsSecId, StudentId, AdmissionNo, categoryId, "allow", remarks);
                            //int sn = saveNewIncrementData(empId);
                      
                               


                        }
                        lblMessage.InnerText = "success-> Data Saved Successfully.";
                    }
                }
            }
            catch (Exception ex)
            {

                lblMessage.InnerText = "success-> Data Saved Failed.";
            }

        }

        protected void gvstudentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private void UncheckAllCheckboxes()
        {
            try
            {
                // Uncheck header checkbox
                CheckBox headerCheckbox = (CheckBox)gvstudentList.HeaderRow.FindControl("hdChk");
                if (headerCheckbox != null)
                {
                    headerCheckbox.Checked = false;
                }

                // Uncheck all checkboxes in rows
                foreach (GridViewRow row in gvstudentList.Rows)
                {
                    CheckBox chk = (CheckBox)row.Cells[4].FindControl("chkStatus");
                    if (chk != null)
                    {
                        chk.Checked = false;
                    }
                }
            }
            catch { }
        }

        private void LoadBatchwiseFeeCat(string batchId, DropDownList dl)
        {
          DataTable  dt = new DataTable();
        
            string[] batchclsID = batchId.Split('_');
            string hh = "SELECT FeeCatId,FeeCatName FROM FeesCategoryInfo WHERE BatchId = '" + batchclsID[0] + "'  order by FeeCatId ASC";
            dt = CRUD.ReturnTableNull(hh);
            dl.DataSource = dt;
            dl.DataTextField = "FeeCatName";
            dl.DataValueField = "FeeCatId";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedValue != null && ddlCategory.SelectedValue != "0")
            {
                SaveData(ddlCategory.SelectedValue.ToString());
            }
            else
            {
                lblMessage.InnerText = "warning-> Please, select any category.";
                ddlCategory.Focus();
            }
        }

        protected void gvstudentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SaveRow")
            {
                try
                {
                    accountsettingEntry = new AcccountsettingEntry();
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gvstudentList.Rows[rowIndex];

                    string BatchId = gvstudentList.DataKeys[row.RowIndex]["BatchId"].ToString();
                    string StudentId = gvstudentList.DataKeys[row.RowIndex]["StudentId"].ToString();
                    string ClsSecId = gvstudentList.DataKeys[row.RowIndex]["ClsSecId"].ToString();
                    string ClsGrpID = gvstudentList.DataKeys[row.RowIndex]["ClsGrpID"].ToString();




                    string AdmissionNo = row.Cells[1].Text.Trim();

                    TextBox txRemarks = (TextBox)row.FindControl("txRemarks");
                    CheckBox chkStatus = (CheckBox)row.FindControl("chkStatus");
                    string remarks = txRemarks.Text.Trim();

                    string categoryId = ddlCategory.SelectedValue.ToString();
                    bool isSucced = accountsettingEntry.InsertStudentPaymentRestriction(BatchId, ClsGrpID, ClsSecId, StudentId, AdmissionNo, categoryId, "allow", remarks);
                    lblMessage.InnerText = "success-> Data Saved Successfully.";
                }
                catch (Exception ex)
                {

                    lblMessage.InnerText = "success-> Data Saved Failed.";
                }
               
            }
         }
    }
}