using DS.BLL.Examinition;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academic.Examination
{
    public partial class ExamineeSelection : System.Web.UI.Page
    {
        DataTable dt;
        ExamineeEntry examineeEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                        //---url bind---
                        aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                        aAcademicHome.HRef = "~/" + Classes.Routing.AcademicRouteUrl;
                        aExamHome.HRef = "~/" + Classes.Routing.ExaminationHomeRouteUrl;
                        //---url bind end---

                        Button btnPreviewMarksheet = new Button();
                        Button btnDetailsMarks = new Button();
                        if (!BLL.ControlPanel.PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "ExamRoutine.aspx", btnSave, chkForCoutAsFinalResult, btnPreviewMarksheet, btnDetailsMarks, btnPrintPreview)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                        ShiftEntry.GetDropDownList(ddlShift);
                        BatchEntry.GetDropdownlist(ddlBatch, true);
                    }
                }
            }
            catch { }
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddlSection.Items.Clear();
            BatchEntry.loadGroupByBatchId(ddlGroup, ddlBatch.SelectedValue.ToString());

            if (ddlGroup.Items.Count == 1)
            {
                ddlGroup.Enabled = false;
                ClassSectionEntry.GetSectionListByBatchId(ddlSection, ddlBatch.SelectedValue.ToString());
            }
            else
            {
                ddlGroup.Enabled = true;

            }

            //ExamInfoEntry.GetExamIdList(ddlExamId, ddlBatch.SelectedValue.ToString(),true);
            ExamInfoEntry.GetExamIdListWithExInSl(ddlExamId, ddlBatch.SelectedValue.ToString());
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection, ddlBatch.SelectedValue.ToString(), ddlGroup.SelectedItem.Value);
            ExamInfoEntry.GetExamIdListWithExInSl(ddlExamId, ddlBatch.SelectedValue.ToString(), ddlGroup.SelectedItem.Value);
            loadRoutine();
        }

        protected void ddlExamId_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadRoutine();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            save();
            loadRoutine();
            
        }       
     
        private void save()
        {
            try
            {
                foreach (GridViewRow row in gvExamRoutine.Rows)
                {
                    CheckBox ckb = (CheckBox)row.FindControl("ckbStatus");
                    if (ckb.Checked)
                    {
                        if (examineeEntry == null)
                            examineeEntry = new ExamineeEntry();
                        examineeEntry.insert(ddlExamId.SelectedValue,gvExamRoutine.DataKeys[row.RowIndex].Values[1].ToString(), gvExamRoutine.DataKeys[row.RowIndex].Values[2].ToString(), gvExamRoutine.DataKeys[row.RowIndex].Values[3].ToString(), gvExamRoutine.DataKeys[row.RowIndex].Values[4].ToString());
                    }
                    else
                    {
                        if (examineeEntry == null)
                            examineeEntry = new ExamineeEntry();
                        examineeEntry.delete( gvExamRoutine.DataKeys[row.RowIndex].Values[0].ToString());
                    }
                }
                lblMessage.InnerText = "success-> Successfully Submited";
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }
        
        private void loadRoutine()
        {
            try
            {
                dt = new DataTable();
                if (examineeEntry == null)
                    examineeEntry = new ExamineeEntry();
                dt = examineeEntry.loadExaminee(ddlShift.SelectedValue,ddlBatch.SelectedValue,ddlGroup.SelectedValue,ddlExamId.SelectedValue,ddlSection.SelectedValue);
                gvExamRoutine.DataSource = dt;
                gvExamRoutine.DataBind();
            }
            catch (Exception ex)
            { }
        }
        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {

        }

        protected void gvExamRoutine_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Change")
                {
                    int rIndex = int.Parse(e.CommandArgument.ToString());
                    

                }
                else if (e.CommandName == "Remove")
                {

                    
                }
            }
            catch (Exception ex) { }
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadRoutine();
        }
        protected void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckbAll = (CheckBox)gvExamRoutine.HeaderRow.FindControl("ckbAll");
            foreach (GridViewRow row in gvExamRoutine.Rows)
            {
                CheckBox ckb = (CheckBox)row.FindControl("ckbStatus");
                ckb.Checked = ckbAll.Checked;
            }
        }
    }
}