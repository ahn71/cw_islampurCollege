using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.BLL.ManagedClass;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.Classes;
using DS.BLL;

namespace DS.UI.Academics.Students
{
    public partial class CurrentStudentInfo : System.Web.UI.Page
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
                    if (Session["__MsgStdInfo__"] !=null)
                    {
                        lblMessage.InnerText = Session["__MsgStdInfo__"].ToString();
                    }
                    Session["__MsgStdInfo__"] = null;
                ShiftEntry.GetShiftListWithAll(dlShift);
                commonTask.loadYearFromBatch(ddlYear);
                ddlYear.SelectedValue = TimeZoneBD.getCurrentTimeBD().Year.ToString();
               
                ClassEntry.GetEntitiesDataWithAll(ddlClass);
                if (ddlClass != null && ddlClass.SelectedValue != "00")
                {
                    ViewState["__BatchId__"] = commonTask.get_batchid(ddlClass.SelectedValue,ddlYear.SelectedValue);
                    ClassGroupEntry.GetDropDownWithAll(dlGroup, int.Parse(ddlClass.SelectedValue.ToString()));
                    if (dlGroup != null && dlGroup.SelectedValue != "00")
                        ClassSectionEntry.GetSectionList(dlSection, int.Parse(ddlClass.SelectedValue), dlGroup.SelectedValue);
                }
                
                loadCurrentStudentInfo("");
            }
        }
       
        protected void dlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                
                loadCurrentStudentInfo("");
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load(1);", true);
            }
            catch { }

        }
        private void loadCurrentStudentInfo(string sqlCmd)
        {
            try
            {
                if (dlShift.SelectedValue == "0")
                {
                    lblMessage.InnerText = "warning-> please, select any shift.";
                    dlShift.Focus();
                    return;
                }
                string conditions = " Where Year='" + ddlYear.SelectedValue + "'";
                if(dlShift.SelectedValue!="00")
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
                gvStudentList.DataSource = dt;
                gvStudentList.DataBind();
            }
            catch { }
        }
        private void loadCurrentStudentInfo1(string sqlCmd)
        {
            try
            {
                if (dlShift.SelectedValue == "0")
                {
                    lblMessage.InnerText = "warning-> please, select any shift.";
                    dlShift.Focus();
                    return;
                }
                string conditions = " Where Year='" + ddlYear.SelectedValue + "'";
                if (ddlClass.SelectedValue != "00")
                {
                    conditions += " and ClassId=" + ddlClass.SelectedValue;

                    if (dlGroup.SelectedValue != "0" && dlGroup.SelectedValue != "00")
                        conditions += " and ClsGrpID=" + dlGroup.SelectedValue;
                    if (dlSection.SelectedValue != "00")
                        conditions += " and ClsSecID=" + dlSection.SelectedValue;

                }

                DataTable dt = new DataTable();
                
                dt = currentstdEntry.GetCurrentStudent(conditions);               
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                divInfo = " <table id='tblStudentInfo' class='table table-striped table-bordered dt-responsive nowrap' cellspacing='0' width='100%' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                divInfo += "<th>Full Name</th>";
                divInfo += "<th>Class</th>";
                divInfo += "<th style='text-align:center;'>Group</th>";
                divInfo += "<th style='text-align:center;'>Section</th>";
                divInfo += "<th style='text-align:center;'>Shift</th>";
                divInfo += "<th style='text-align:center;'>Roll</th>";
                divInfo += "<th style='text-align:center;'>Gender</th>";
                divInfo += "<th>Guardian Mobile No</th>";
                if (Session["__View__"].ToString().Equals("true"))
                    divInfo += "<th class='size'>View</th>";
                divInfo += "<th class='size'>Edit</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                if (totalRows == 0)
                {
                    divInfo += "</tbody></table>";                    
               //     divStudentDetails.Controls.Add(new LiteralControl(divInfo));
                    return;
                }                
                string id = "";
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    int sl = x + 1;
                    id = dt.Rows[x]["StudentId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["FullName"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["ClassName"].ToString() + "</td>";
                    if (dt.Rows[x]["GroupName"].ToString() == "")
                    {
                        divInfo += "<td class='text-center'>No Group</td>";
                    }
                    else
                    {
                        divInfo += "<td class='text-center'>" + dt.Rows[x]["GroupName"].ToString() + "</td>";
                    }
                    divInfo += "<td class='text-center'>" + dt.Rows[x]["SectionName"].ToString() + "</td>";
                    divInfo += "<td class='text-center'>" + dt.Rows[x]["ShiftName"].ToString() + "</td>";                    
                    divInfo += "<td class='text-center'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "<td class='text-center'>" + dt.Rows[x]["Gender"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["GuardianMobileNo"].ToString() + "</td>";
                    if (Session["__View__"].ToString().Equals("true"))
                    divInfo += "<td class='size' style='max-width:20px;'>" + "<img src='/Images/gridImages/view.png' onclick='viewStudent(" + id + ");'  />";
                   // divInfo += "<td class='size' style='max-width:20px;'>" + "<img src='/Images/gridImages/edit.png'  onclick='editStudent(" + id + ");'  />";
                    divInfo += "<td class='size' style='max-width:20px;'>" + "<a  href='/UI/Academic/Students/OldStudentEntry.aspx?StudentId=" + id + "&Edit=True'>Edit</a>";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
               // divStudentDetails.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void gvStudentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                int rIndex = int.Parse(e.CommandArgument.ToString());
                string StudentId = gvStudentList.DataKeys[rIndex].Values[0].ToString();
                StudentId = commonTask.Base64Encode(StudentId);
                Response.Redirect(GetRouteUrl("StudentProfileRoute", new { id = StudentId}));
               // Response.Redirect("/UI/Academic/Students/StdProfile.aspx?StudentId=" + StudentId);
             //   ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Academic/Students/StdProfile.aspx?StudentId=" + StudentId+"');", true);  //Open New Tab for Sever side code
                                                                                                                                                                                                     // Response.Write("<script>window.open('/UI/Academic/Students/StdProfile.aspx?StudentId=" + StudentId+"');</script>");
            }
            else if(e.CommandName == "Change")
            {
                int rIndex = int.Parse(e.CommandArgument.ToString());
                string StudentId = gvStudentList.DataKeys[rIndex].Values[0].ToString();
                StudentId = commonTask.Base64Encode(StudentId);
                string _requestFrom = commonTask.Base64Encode("List");
                Response.Redirect(GetRouteUrl("StudentEditRoute", new { id = StudentId, requestFrom= _requestFrom }));
               // Response.Redirect("/UI/Academic/Students/student-entry.aspx?StudentId=" + StudentId + "&Edit=True");

               //Response.Redirect("/UI/Academic/Students/OldStudentEntry.aspx?StudentId=" + StudentId + "&Edit=True");
              
                                                                                                                                                                            // Response.Write("<script>window.open('/UI/Academic/Students/OldStudentEntry.aspx?StudentId=" + StudentId + "&Edit=True','_blank');</script>");
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
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load(0);", true);
        }
    }
}