using DS.BLL.Examinition;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academic.Examination
{
    public partial class ExamRoutine : System.Web.UI.Page
    {
        ExamRoutineEntry examRoutineEntry;
        DataTable dt;
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
            
            BatchEntry.loadGroupByBatchId(ddlGroup, ddlBatch.SelectedValue.ToString());

            if (ddlGroup.Items.Count == 1)
            {
                ddlGroup.Enabled = false;               
            }
            else
            {
                ddlGroup.Enabled = true;

            }

            //ExamInfoEntry.GetExamIdListWithExInSl(ddlExamId, ddlBatch.SelectedValue.ToString(),true);
            ExamInfoEntry.GetExamIdListWithExInSl(ddlExamId, ddlBatch.SelectedValue.ToString());
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExamInfoEntry.GetExamIdListWithExInSl(ddlExamId, ddlBatch.SelectedValue.ToString(), ddlGroup.SelectedItem.Value);
        }

        protected void ddlExamId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                Exam.getSubjects(ddlSubject, ddlBatch.SelectedValue, ddlGroup.SelectedValue, ddlGroup.Enabled, ddlExamId.SelectedValue);
                loadRoutine();
            }
            catch { }
        }

       

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Update")
                update();
            else
                save();

        }
        private void clear()
        {

            ViewState["__ExamRoutineID__"] ="0";
            btnSave.Text = "Save";
            ddlExamId.Enabled = true;
            ddlGroup.Enabled = true;
            ddlBatch.Enabled = true;
            ddlShift.Enabled = true;
            txtDate.Text = "";
            ddlStartTimeHH.SelectedValue = "0";
            ddlStartTimeMM.SelectedValue = "00";
            ddlEndTimeHH.SelectedValue = "0";
            ddlEndTimeMM.SelectedValue = "00";
        }
        private void save()
        {
            try {
                if (examRoutineEntry == null)
                    examRoutineEntry = new ExamRoutineEntry();
                string _examDate = commonTask.ddMMyyyyToyyyyMMdd(txtDate.Text.Trim());                
                DateTime startTime = DateTime.Parse(_examDate+" "+ddlStartTimeHH.SelectedValue + ":" + ddlStartTimeMM.SelectedValue + ":00 " + ddlStartTimeTT.SelectedValue);
                DateTime endTime = DateTime.Parse(_examDate + " " + ddlEndTimeHH.SelectedValue + ":" + ddlEndTimeMM.SelectedValue + ":00 " + ddlEndTimeTT.SelectedValue);
                string[] subID = ddlSubject.SelectedValue.Split('_');
                if (examRoutineEntry.insert(startTime.ToString("yyyy-MM-dd"), startTime.ToString("dddd"), startTime.ToString("HH:mm:hh"), endTime.ToString("HH:mm:hh"), ddlExamId.SelectedValue, ddlBatch.SelectedValue, ddlGroup.SelectedValue, subID[0], subID[1], ddlShift.SelectedValue))
                {
                    lblMessage.InnerText = "success->Successfully Saved.";
                    loadRoutine();
                }
                else
                {
                    lblMessage.InnerText = "error->Unable to Save.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->"+ ex.Message;
            }
        }
        private void update()
        {
            try
            {
                if (examRoutineEntry == null)
                    examRoutineEntry = new ExamRoutineEntry();
                string _examDate = commonTask.ddMMyyyyToyyyyMMdd(txtDate.Text.Trim());
                DateTime startTime = DateTime.Parse(_examDate + " " + ddlStartTimeHH.SelectedValue + ":" + ddlStartTimeMM.SelectedValue + ":00 " + ddlStartTimeTT.SelectedValue);
                DateTime endTime = DateTime.Parse(_examDate + " " + ddlEndTimeHH.SelectedValue + ":" + ddlEndTimeMM.SelectedValue + ":00 " + ddlEndTimeTT.SelectedValue);
                string[] subID = ddlSubject.SelectedValue.Split('_');
                if (examRoutineEntry.update(ViewState["__ExamRoutineID__"].ToString(),startTime.ToString("yyyy-MM-dd"), startTime.ToString("dddd"), startTime.ToString("HH:mm:hh"), endTime.ToString("HH:mm:hh"), subID[0], subID[1]))
                {
                    lblMessage.InnerText = "success->Successfully Update.";                    
                    loadRoutine();
                    clear();
                }
                else
                {
                    lblMessage.InnerText = "error->Unable to update.";
                }
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
                if (examRoutineEntry == null)
                    examRoutineEntry = new ExamRoutineEntry();
                //dt = examRoutineEntry.getExamRoutine(ddlExamId.SelectedValue);
                dt = ExamCommon.getExamRoutine(ddlExamId.SelectedValue);
                gvExamRoutine.DataSource = dt;
                gvExamRoutine.DataBind();
            }
            catch (Exception ex)
            { }
        }
        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            if (ddlBatch.SelectedIndex < 1)
            {
                lblMessage.InnerText = "warning-> Please select the Batch.";
                ddlBatch.Focus();
                return;
            }
            if (ddlExamId.SelectedIndex < 1)
            {
                lblMessage.InnerText = "warning-> Please select the Exam.";
                ddlExamId.Focus();
                return;
            }
            ExamRoutineReport();
        }
        private void ExamRoutineReport()
        {
            try {
                
                DataTable dt = new DataTable();
                dt = ExamCommon.getExamRoutine(ddlExamId.SelectedValue);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Session["__ExamRoutine__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=ExamRoutine');", true);  //Open New Tab for Sever side code
                }
                else
                    lblMessage.InnerText = "warning-> Data not found.";
            } catch(Exception ex) { }
        }

        protected void gvExamRoutine_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try {
            if (e.CommandName == "Change")
            {
                int rIndex = int.Parse(e.CommandArgument.ToString());
                    
                ddlSubject.SelectedValue = gvExamRoutine.DataKeys[rIndex].Values[1].ToString() + "_" + gvExamRoutine.DataKeys[rIndex].Values[2].ToString();
                ddlExamId.SelectedValue = gvExamRoutine.DataKeys[rIndex].Values[3].ToString();
                ddlGroup.SelectedValue = gvExamRoutine.DataKeys[rIndex].Values[4].ToString();
                ddlBatch.SelectedValue= gvExamRoutine.DataKeys[rIndex].Values[5].ToString();
                ddlShift.SelectedValue = gvExamRoutine.DataKeys[rIndex].Values[6].ToString();
                txtDate.Text = gvExamRoutine.Rows[rIndex].Cells[2].Text.Trim();
                DateTime startTime= DateTime.Parse("2019-03-01 "+gvExamRoutine.Rows[rIndex].Cells[4].Text.Trim());
                DateTime endTime = DateTime.Parse("2019-03-01 "+ gvExamRoutine.Rows[rIndex].Cells[5].Text.Trim());
                    ddlStartTimeHH.SelectedValue = startTime.ToString("hh");
                    ddlStartTimeMM.SelectedValue = startTime.ToString("mm");
                    ddlStartTimeTT.SelectedValue = startTime.ToString("tt");

                    ddlEndTimeHH.SelectedValue = endTime.ToString("hh");
                    ddlEndTimeMM.SelectedValue = endTime.ToString("mm");
                    ddlEndTimeTT.SelectedValue = endTime.ToString("tt");

                    ViewState["__ExamRoutineID__"] = gvExamRoutine.DataKeys[rIndex].Values[0].ToString();
                    btnSave.Text = "Update";
                    ddlExamId.Enabled = false;
                    ddlGroup.Enabled = false;
                    ddlBatch.Enabled = false;
                    ddlShift.Enabled = false;

            }
            else if (e.CommandName == "Remove")
            {
               
                if (examRoutineEntry == null)
                    examRoutineEntry = new ExamRoutineEntry();
                int rIndex = int.Parse(e.CommandArgument.ToString());
                string ExamRoutineID= gvExamRoutine.DataKeys[rIndex].Values[0].ToString();
                if (examRoutineEntry.Delete(ExamRoutineID))
                {
                    lblMessage.InnerText = "success-> Successfully Deleted";
                    gvExamRoutine.Rows[rIndex].Visible = false;
                }
                else
                    lblMessage.InnerText = "success-> Unable to Delete";
            }
            }
            catch (Exception ex) { }
        }
    }
}