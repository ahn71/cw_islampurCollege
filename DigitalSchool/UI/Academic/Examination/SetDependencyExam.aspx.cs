using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.ManagedBatch;
using DS.BLL.Examinition;
using DS.PropertyEntities.Model.Examinition;

namespace DS.UI.Academic.Examination
{
    public partial class SetDependencyExam : System.Web.UI.Page
    {
        ExamDependencyInfoEntry examDependencyInfoEntry;
        bool result;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                BatchEntry.GetBatchInfoForExamDependency(ddlBatch);
                DataBindForView();
            }
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ExamInfoEntry.GetExamIdList(ddlExamId, ddlBatch.SelectedValue.ToString(),true);
               
            }
            catch { }
        }

        protected void ddlExamId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlExamId.SelectedIndex != 0)
                {
                    ExamInfoEntry.GetExamIdListForSetDependency(chkExamTypeList, ddlBatch.SelectedValue.ToString(), ddlExamId.SelectedValue.ToString());
                    trSelectAll.Visible = true;
                    chkExamTypeList.Visible = true;
                  //  chkSelectAll_CheckedChanged(sender,e);
                }
                else
                {
                    trSelectAll.Visible = false;
                    chkExamTypeList.Items.Clear();
                    chkExamTypeList.Visible = false;
                }
            }
            catch { }
        }
    
        protected void chkExamTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                chkSelectAll.Checked = false;
            }
            catch { }
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSelectAll.Checked)
                {
                    for (byte b = 0; b < chkExamTypeList.Items.Count; b++)
                    {
                        chkExamTypeList.Items[b].Selected = true;
                    }
                }
                else
                {
                    for (byte b = 0; b < chkExamTypeList.Items.Count; b++)
                    {
                        chkExamTypeList.Items[b].Selected = false;
                    }
                }
            }
            catch { }
        }

        private ExamDependencyInfoEntity GetData()
        {
            try
            {
                ExamDependencyInfoEntity edie = new ExamDependencyInfoEntity();
                edie.DeId = (lblExId.Value.ToString() == "") ? 0: int.Parse(lblExId.Value.ToString());
                edie.ParentExInId = ddlExamId.SelectedValue.ToString();
                edie.DependencyIExamId = chkExamTypeList;
                edie.IsFinal = cbIsFinal.Checked;
                return edie;
            }
            catch { return null; }
        }

        private bool InputValidation()
        {
            try
            {
                if (ddlBatch.SelectedIndex == 0)
                {
                    lblMessage.InnerText = "error->Please select a batch"; ddlBatch.Focus(); return false;
                }
                else if (ddlExamId.SelectedIndex == 0)
                {
                    lblMessage.InnerText = "error->Please select an exam id as parent exam "; ddlExamId.Focus(); return false;
                }
                else if (chkExamTypeList.SelectedIndex == -1)
                {
                    lblMessage.InnerText = "error->Please select a an exam id as a dependency"; chkExamTypeList.Focus(); return false;
                }

                return true;
            }
            catch { return false; }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (InputValidation()) saveDependencyExamInfo();
        }

        private void saveDependencyExamInfo()
        {
            using (ExamDependencyInfoEntity edie = GetData())
            {
                if (examDependencyInfoEntry == null) examDependencyInfoEntry = new ExamDependencyInfoEntry();
                examDependencyInfoEntry.setValue = edie;
                result = examDependencyInfoEntry.Insert();

                if (result)
                {
                    lblMessage.InnerText = "success->Successfully save";
                    DataBindForView();
                    BatchEntry.GetBatchInfoForExamDependency(ddlBatch);
                    Allclear();
                }

            }
        }

        void Allclear()
        {
            try
            {
                chkExamTypeList.Items.Clear();
                chkExamTypeList.Visible = false;
                chkSelectAll.Visible = false;
                chkSelectAll.Checked = false;
                ddlExamId.SelectedIndex = 0;
                
            }
            catch { }
        
        }

        private void DataBindForView()
        {

            try
            {
                List<ExamDependencyInfoEntity> GetDependencyExamList = ExamDependencyInfoEntry.GetDependencyExamList; // for get all exam list
                gvExamIdList.DataSource = GetDependencyExamList;
                gvExamIdList.DataBind();
                return;
            }
            catch { }
        }

        protected void gvExamIdList_OnRowCommand(object sender,GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Delete"))
                {
                    int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                    if (examDependencyInfoEntry == null) examDependencyInfoEntry = new ExamDependencyInfoEntry();
                    result=examDependencyInfoEntry.Delete(gvExamIdList.Rows[rowIndex].Cells[0].Text);
                    if (result)
                    {
                        lblMessage.InnerText = "success->Successfully deleted";
                        gvExamIdList.Rows[rowIndex].Visible = false;
                        BatchEntry.GetBatchInfoForExamDependency(ddlBatch);
                       
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->"+ex.Message;
            }
       
        }

        protected void gvExamIdList_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           // DataBindForView();
        }

        protected void gvExamIdList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataBindForView();
                gvExamIdList.PageIndex = e.NewPageIndex;
                gvExamIdList.DataBind();
            }
            catch { }
        }

       

       

        
    }
}