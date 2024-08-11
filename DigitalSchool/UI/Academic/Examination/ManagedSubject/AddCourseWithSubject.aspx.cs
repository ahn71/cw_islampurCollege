using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.PropertyEntities.Model.Examinition;
using DS.BLL.ManagedSubject;
using DS.PropertyEntities.Model.ManagedSubject;
using DS.BLL.ControlPanel;

namespace DS.UI.Academic.Examination.ManagedSubject
{
    public partial class AddCourseWithSubject : System.Web.UI.Page
    {
        CourseEntry course_entry;
        bool result;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddCourseWithSubject.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                DataBindForView();
                SubjectEntry.GetSujectList(ddlSubjectName);
            }
        }

        private CourseEntity GetData
        {
            get
            {
                try
                {
                    CourseEntity ce = new CourseEntity();
                    ce.CourseId = (lblCourseId.Value.ToString() == "") ? 0 : int.Parse(lblCourseId.Value.ToString());
                    ce.CourseName = txtCourseName.Text.Trim();
                    ce.Ordering = int.Parse(txtOrder.Text.Trim());
                    ce.SubId = int.Parse(ddlSubjectName.SelectedValue.ToString());
                    ce.IsActive = chkIsActive.Checked;
                    return ce;
                }
                catch { return null; }
            }
        }

        private void saveCourse()
        {
            try
            {
                using (CourseEntity ce = GetData)
                {
                    if (course_entry == null) course_entry = new CourseEntry();
                    course_entry.SetValues = ce;
                    result = course_entry.Insert();
                    if (result)
                    {
                        lblMessage.InnerText = "success->Successfully Saved";
                        Clear();
                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "clearIt();", true);
                        DataBindForView();

                    }
                    else
                    {
                        lblMessage.InnerText = "error->Please Check Duplicate Course !";
                        DataBindForView();
                    }

                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText=("error->"+ex.Message);
            }
        }

        private void updateCourse()
        {
            try
            {
                using (CourseEntity ce = GetData)
                {
                    if (course_entry == null) course_entry = new CourseEntry();
                    course_entry.SetValues = ce;
                    result = course_entry.Update();
                    if (result)
                    {
                        lblMessage.InnerText = "success->Successfully Updated";
                        Clear();
                     //   ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "clearIt();", true);
                        DataBindForView();
                    }
                
                }
            }
            catch { }
        }

        private void DataBindForView()
        {
            try
            {
                List<CourseEntity> GetCourseList = CourseEntry.GetCourseList;
                string divInfo = "";
                if (GetCourseList.Count == 0)
                {
                    divInfo = "<div class='noData'>Compulsory Subject not available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divSubjectList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                divInfo = " <table id='tblClassList' class='table table-striped table-bordered dt-responsive nowrap' cellspacing='0' width='100%' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Subject</th>";
                divInfo += "<th>Course Name</th>";
                divInfo += "<th style='width:70px;text-align:center;visible:False'>Order</th>";
                divInfo += "<th style='width:70px;text-align:center;visible:False'>Active</th>";
                if (Session["__Update__"].ToString().Equals("true")) divInfo += "<th>Edit</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                string id = "";
                for (int x = 0; x < GetCourseList.Count; x++)
                {
                    id = GetCourseList[x].CourseId.ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + GetCourseList[x].subName + "</td>";
                    divInfo += "<td >" + GetCourseList[x].CourseName + "</td>";
                    divInfo += "<td style='text-align:center'>" + GetCourseList[x].Ordering.ToString() + "</td>";
                    divInfo +=  "<td style='text-align:center'>" +((GetCourseList[x].IsActive)?"Yes":"NO") + "</td>";
                    if (Session["__Update__"].ToString().Equals("true"))
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editSubject(" + id + ");'  />";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divSubjectList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            if (lblCourseId.Value.ToString().Length == 0)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; DataBindForView(); return; }
                saveCourse();
            }
            else
            {
                updateCourse();
            }
        }
        private void Clear()        
        {
            lblCourseId.Value="";
            txtCourseName.Text="";
            txtOrder.Text="";
            btnSave.Text = "Save";
        }
    }
}