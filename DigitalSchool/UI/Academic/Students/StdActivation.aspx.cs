using DS.BLL;
using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academic.Students
{
    public partial class StdActivation : System.Web.UI.Page
    {
        ClassGroupEntry clsgrpEntry;
        CurrentStdEntry currentstdEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie getCookies = Request.Cookies["userInfoSchool"];
            Session["__UserTypeId__"] = getCookies["__UserTypeId__"].ToString();
            lblMessage.InnerText = string.Empty;
            if (!IsPostBack)
            {
                //---url bind---
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aAcademicHome.HRef = "~/" + Classes.Routing.AcademicRouteUrl;
                aStudentHome.HRef = "~/" + Classes.Routing.StudentHomeRouteUrl;
                //---url bind end---
                try
                {
                    if (Request.QueryString["hasperm"].ToString() != null) lblMessage.InnerText = "warning->You don't have permission to Update.";
                }
                catch { }
                if (!PrivilegeOperation.SetPrivilegeControl(float.Parse(Session["__UserTypeId__"].ToString()), "CurrentStudentInfo.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?&hasperm=no");
                if (Session["__MsgStdInfo__"] != null)
                {
                    lblMessage.InnerText = Session["__MsgStdInfo__"].ToString();
                }
                Session["__MsgStdInfo__"] = null;
                //BatchEntry.GetDropdownlist(dlBatch, "True");
                //dlBatch.Items.Insert(1, new ListItem("All", "All"));
                //dlBatch.SelectedValue = "All";
                //dlSection.Items.Insert(0, new ListItem("All", "All"));
                //dlSection.SelectedValue = "All";
                //ShiftEntry.GetDropDownList(dlShift);
                //dlShift.Items.RemoveAt(0);
                //dlShift.Items.Insert(0, new ListItem("All", "All"));
                //dlShift.SelectedValue = "All";
                ShiftEntry.GetShiftListWithAll(dlShift);
                commonTask.loadYearFromBatch(ddlYear);
                ddlYear.SelectedValue = TimeZoneBD.getCurrentTimeBD().Year.ToString();

                ClassEntry.GetEntitiesDataWithAll(ddlClass);
                if (ddlClass != null && ddlClass.SelectedValue != "00")
                {
                    ViewState["__BatchId__"] = commonTask.get_batchid(ddlClass.SelectedValue, ddlYear.SelectedValue);
                    ClassGroupEntry.GetDropDownWithAll(dlGroup, int.Parse(ddlClass.SelectedValue.ToString()));
                    if (dlGroup != null && dlGroup.SelectedValue != "00")
                        ClassSectionEntry.GetSectionList(dlSection, int.Parse(ddlClass.SelectedValue), dlGroup.SelectedValue);
                }
                loadCurrentStudentInfo();
            }
        }

        //protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string[] BatchClsID = dlBatch.SelectedValue.Split('_');
        //    if (BatchClsID[0] == "All")
        //    {
        //        dlGroup.Items.Clear();
        //        dlGroup.Items.Insert(0, new ListItem("All", "All"));
        //        dlSection.Items.Clear();
        //        dlSection.Items.Insert(0, new ListItem("All", "All"));
        //        return;
        //    }
        //    if (clsgrpEntry == null)
        //    {
        //        clsgrpEntry = new ClassGroupEntry();
        //    }
        //    clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), dlGroup);
        //    ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), dlGroup.SelectedValue);
        //    dlGroup.Items.Insert(1, new ListItem("All", "All"));
        //    dlGroup.SelectedValue = "All";
        //    dlSection.Items.Insert(1, new ListItem("All", "All"));
        //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load(0);", true);
        //}

        protected void dlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string[] BatchClsID = dlBatch.SelectedValue.Split('_');
            //string GroupId = "0";
            //if (dlGroup.SelectedValue != "All")
            //{
            //    GroupId = dlGroup.SelectedValue;
            //}
            //ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), GroupId);
            //dlSection.Items.Insert(1, new ListItem("All", "All"));
            if (dlGroup != null && dlGroup.SelectedValue != "00")
                ClassSectionEntry.GetSectionList(dlSection, int.Parse(ddlClass.SelectedValue), dlGroup.SelectedValue);
            else
            {
                if (dlSection != null)
                    dlSection.Items.Clear();
                dlSection.Items.Insert(0, new ListItem("All", "00"));
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load(0);", true);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
               
                loadCurrentStudentInfo();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load(1);", true);
            }
            catch { }
        }
        private void loadCurrentStudentInfo()
        {
            try
            {
                //if (dlShift.SelectedValue == "0")
                //{
                //    lblMessage.InnerText = "warning-> please, select any shift.";
                //    dlShift.Focus();
                //    return;
                //}
                //string[] BatchClsID = dlBatch.SelectedValue.Split('_');
                //string condition = "";

                //DataTable dt = new DataTable();
                //if (currentstdEntry == null)
                //{
                //    currentstdEntry = new CurrentStdEntry();
                //}
                //condition = currentstdEntry.GetSearchCondition(dlShift.SelectedValue, dlBatch.SelectedValue, dlGroup.SelectedValue, dlSection.SelectedValue);
                //if (condition != "")
                //{
                //    condition += " AND BatchId!='0' and IsActive="+rblActiveInActive.SelectedValue+" ";
                //}
                //else
                //{
                //    condition = "WHERE BatchId!='0' and IsActive=" + rblActiveInActive.SelectedValue + " ";
                //}
                string conditions = " Where IsActive=" + rblActiveInActive.SelectedValue + " and Year='" + ddlYear.SelectedValue + "'";
                if (dlShift.SelectedValue != "00")
                    conditions += " and ConfigId=" + dlShift.SelectedValue;
                if (ddlClass.SelectedValue != "00")
                {
                    conditions += " and ClassId=" + ddlClass.SelectedValue;

                    if (dlGroup.SelectedValue != "0" && dlGroup.SelectedValue != "00")
                        conditions += " and ClsGrpID=" + dlGroup.SelectedValue;
                    if (dlSection.SelectedValue != "00")
                        conditions += " and ClsSecID=" + dlSection.SelectedValue;

                }
                DataTable dt = new DataTable();
                if (currentstdEntry == null)
                    currentstdEntry = new CurrentStdEntry();
                dt = currentstdEntry.GetCurrentStudent(conditions);
                if (dt != null && dt.Rows.Count > 0)
                {
                    gvForApprovedList.DataSource = dt;
                    gvForApprovedList.DataBind();
                    if (rblActiveInActive.SelectedValue == "0")
                    {
                        gvForApprovedList.Columns[9].Visible = false;
                        gvForApprovedList.Columns[10].Visible = true;
                        
                    }
                    else
                    {
                        gvForApprovedList.Columns[10].Visible = false;
                        gvForApprovedList.Columns[9].Visible = true;
                    }
                        
                }
                else
                {
                    gvForApprovedList.DataSource = null;
                    gvForApprovedList.DataBind();
                }
               
            }
            catch { }
        }

        protected void gvForApprovedList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "InActive")
            {
                int rIndex = int.Parse(e.CommandArgument.ToString());
                string StudentID = gvForApprovedList.DataKeys[rIndex].Values[0].ToString();
                string BatchID = gvForApprovedList.DataKeys[rIndex].Values[1].ToString();
                TextBox txtNote = (TextBox)gvForApprovedList.Rows[rIndex].FindControl("txtNote");

                if (currentstdEntry == null)
                    currentstdEntry = new CurrentStdEntry();
                if (currentstdEntry.UpdateCurrentStudentActive(StudentID, "0"))
                {
                    currentstdEntry.InsertToActivationLog(StudentID, BatchID, txtNote.Text.Trim(), "0");
                    lblMessage.InnerText = "success-> Successfully Inactivated.";
                    gvForApprovedList.Rows[rIndex].Visible = false;
                }




            }
            else if (e.CommandName == "Active")
            {
                int rIndex = int.Parse(e.CommandArgument.ToString());
                string StudentID = gvForApprovedList.DataKeys[rIndex].Values[0].ToString();
                string BatchID = gvForApprovedList.DataKeys[rIndex].Values[1].ToString();
                TextBox txtNote = (TextBox)gvForApprovedList.Rows[rIndex].FindControl("txtNote");

                if (currentstdEntry == null)
                    currentstdEntry = new CurrentStdEntry();
                if (currentstdEntry.UpdateCurrentStudentActive(StudentID, "1"))
                {
                    currentstdEntry.InsertToActivationLog(StudentID, BatchID, txtNote.Text.Trim(), "1");
                    lblMessage.InnerText = "success-> Successfully Activated.";
                    gvForApprovedList.Rows[rIndex].Visible = false;
                }
            }
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlClass.SelectedValue != "00")
            {
                ClassGroupEntry.GetDropDownWithAll(dlGroup, int.Parse(ddlClass.SelectedValue.ToString()));
                if (dlGroup != null && dlGroup.SelectedValue != "00")
                    ClassSectionEntry.GetSectionList(dlSection, int.Parse(ddlClass.SelectedValue), dlGroup.SelectedValue);
                else
                {
                    if (dlSection != null)
                        dlSection.Items.Clear();
                    dlSection.Items.Insert(0, new ListItem("All", "00"));
                }
            }
            else
            {
                if (dlGroup != null)
                    dlGroup.Items.Clear();
                dlGroup.Items.Insert(0, new ListItem("All", "00"));
                if (dlSection != null)
                    dlSection.Items.Clear();
                dlSection.Items.Insert(0, new ListItem("All", "00"));
            }
            
        }
    }
}