using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.PropertyEntities.Model.Examinition;
using DS.BLL.Examinition;
using DS.BLL.ControlPanel;

namespace DS.UI.Academics.Examination
{
    public partial class AddExam : System.Web.UI.Page
    {

        ExamTypeEntry examTypeEntry;
        bool result;
        protected void Page_Load(object sender, EventArgs e)
        {
               
            if (!IsPostBack)
            {
                //---url bind---
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aAcademicHome.HRef = "~/" + Classes.Routing.AcademicRouteUrl;
                aExamHome.HRef = "~/" + Classes.Routing.ExaminationHomeRouteUrl;                
                //---url bind end---
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddExam.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                DataBindForView();
            }
            
        }

        private void DataBindForView()
        {


            if (examTypeEntry == null) examTypeEntry = new ExamTypeEntry();
            List<ExamTypeEntities> loadExamTypeList = examTypeEntry.GetAllExamTypeList;   // for get all exam list
            string divInfo = "";

            if (loadExamTypeList.Count == 0)
            {
                divInfo = "<div class='noData'>No Exam available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divExamList.Controls.Add(new LiteralControl(divInfo));
                return;
            }

            divInfo = " <table id='tblClassList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Exam Name</th>";
            divInfo += "<th>Serial</th>";
            divInfo += "<th>Type</th>";
            divInfo += "<th>Active</th>";
            if (Session["__Update__"].ToString().Equals("true"))
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            string id = "";

            for (int x = 0; x < loadExamTypeList.Count; x++)
            {

                id = loadExamTypeList[x].ExId.ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td ><span id=exname" + id + ">" + loadExamTypeList[x].ExName+ "</span></td>";
                divInfo += "<td ><span id=ordering" + id + ">" + loadExamTypeList[x].Ordering + "</span></td>";
               
                if (loadExamTypeList[x].semesterexam == null) 
                {
                    divInfo += "<td ><span id=semesterexam" + id + ">Quiz</span></td>";
                }
                else if (loadExamTypeList[x].semesterexam == true)
                {
                    divInfo += "<td ><span id=semesterexam" + id + ">Semester</span></td>";
                }
                else
                {
                    divInfo += "<td ><span id=semesterexam" + id + ">Others</span></td>";
                }
                divInfo += "<td ><span id=IsActive" + id + ">" + ((loadExamTypeList[x].IsActive) ? "Yes" : "No") + "</span></td>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editExam(" + id + ");'  />";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";         
            divExamList.Controls.Add(new LiteralControl(divInfo));
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblExId.Value.ToString().Length == 0)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; DataBindForView(); return; }
                if (saveExam() == true)
                {
                    DataBindForView();

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SavedSuccess();", true);
                }
            }
            else
            {
                if (updateExam() == true)
                {
                    DataBindForView();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                }
            }
        }

       

        private ExamTypeEntities GetExamTypeData()
        {
            try
            {
                ExamTypeEntities examType = new ExamTypeEntities();


                examType.ExId = (lblExId.Value.ToString() == "") ? 0 : int.Parse(lblExId.Value.ToString()); ;
                examType.ExName = txtEx_Name.Text.Trim();
                examType.Ordering = int.Parse(txtSerial.Text);
                if (rblType.SelectedValue == "0")
                    examType.semesterexam = false;
                else if (rblType.SelectedValue == "1")
                    examType.semesterexam = true;
                else
                   examType.semesterexam = null;
                examType.IsActive = chkIsActive.Checked;
                
               
                return examType;
            }
            catch { return null; }
        }

        private Boolean saveExam()
        {
            try
            {
                using (ExamTypeEntities examTypeEntities = GetExamTypeData())
                {
                    if (examTypeEntry==null) examTypeEntry = new ExamTypeEntry();
                    examTypeEntry.SetEntities = examTypeEntities;                   
                    result = examTypeEntry.Insert();

                    if (!result)
                    {
                        lblMessage.InnerText = "error-> Unable to save";
                        return false;
                    }
                    return true;

                }     
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        private Boolean updateExam()
        {
            try
            {

                using (ExamTypeEntities examTypeEntities = GetExamTypeData())
                {
                    if (examTypeEntry == null) examTypeEntry = new ExamTypeEntry();
                    examTypeEntry.SetEntities = examTypeEntities;
                    result = examTypeEntry.Update();

                    if (!result)
                    {
                        lblMessage.InnerText = "error-> Unable to update";
                        return false;
                    }
                    return true;
                
                }

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
    }
}