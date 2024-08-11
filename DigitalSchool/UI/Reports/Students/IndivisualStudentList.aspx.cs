using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.Students
{
    public partial class IndivisualStudentList : System.Web.UI.Page
    {
        ClassGroupEntry clsgrpEntry;
        CurrentStdEntry currentstdEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {                
                    if (!IsPostBack)
                    {
                        if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "IndivisualStudentList.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no"); 
                        BatchEntry.GetDropdownlist(dlBatch, "True");
                        dlBatch.Items.Insert(1, new ListItem("All", "All"));
                        dlBatch.SelectedValue = "All";
                        dlSection.Items.Insert(0, new ListItem("All", "All"));
                        dlSection.SelectedValue = "All";
                        ShiftEntry.GetDropDownList(dlShift);
                        dlShift.Items.Insert(1, new ListItem("All", "All"));
                        dlShift.SelectedValue = "All";
                    }
                lblMessage.InnerText = "";
            }
            catch { }
        }
        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            string[] BatchClsID = dlBatch.SelectedValue.Split('_');
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), dlGroup);
            ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), dlGroup.SelectedValue);
            dlGroup.Items.Insert(1, new ListItem("All", "All"));
            if (dlGroup.Enabled == true)
            dlGroup.SelectedValue = "All";
            dlSection.Items.Insert(1, new ListItem("All", "All"));
        }

        protected void dlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            string[] BatchClsID = dlBatch.SelectedValue.Split('_');
            string GroupId = "0";
            if (dlGroup.SelectedValue != "All")
            {
                GroupId = dlGroup.SelectedValue;
            }
            ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), GroupId);
            dlSection.Items.Insert(1, new ListItem("All", "All"));
        }

        protected void dlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            string[] BatchClsID = dlBatch.SelectedValue.Split('_');
            if (currentstdEntry == null)
            {
                currentstdEntry = new CurrentStdEntry();
            }
            string grpId="0";
            if (dlGroup.SelectedValue != "All")
            {
                grpId = dlGroup.SelectedValue;
            }
            currentstdEntry.GetRollNo(dlRoll, dlShift.SelectedValue, BatchClsID[0], grpId, dlSection.SelectedValue);
            dlRoll.Items.Insert(1,new ListItem("All","All"));
            dlRoll.SelectedValue = "All";
        }
        private void loadStudentInfo(string sqlCmd)
        {
            try
            {                
                string condition = "";
                               
                DataTable dt = new DataTable();
                if (currentstdEntry == null)
                {
                    currentstdEntry = new CurrentStdEntry();
                }

                if (dlRoll.SelectedValue != "All")
                {
                    condition = " WHERE StudentId='" + dlRoll.SelectedValue + "'";
                }
                else
                {
                    condition = currentstdEntry.GetSearchCondition(dlShift.SelectedValue, dlBatch.SelectedValue, dlGroup.SelectedValue, dlSection.SelectedValue);
                }

                dt = currentstdEntry.GetCurrentStudentProfile(condition);   
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Student available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divIndivisualStudentList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    divInfo = " <table id='tblStudentInfo' class='table table-striped table-bordered dt-responsive nowrap' cellspacing='0' width='100%'  > ";
                    divInfo += "<thead>";
                    divInfo += "<tbody>";
                    string id = "";
                    id = dt.Rows[x]["StudentId"].ToString();
                    divInfo += "<tr>";
                    divInfo += "<td>Admission No </td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["AdmissionNo"].ToString() + "</td>";
                    if (dt.Rows[x]["AdmissionNo"].ToString() == "")
                    {
                        divInfo += "<td rowspan='6' stye='width:200px'> " + " <img src='http://www.placehold.it/291x170/EFEFEF/AAAAAA&text=no+image'/> </td>";
                    }
                    else
                    {
                       string url = @"/Images/profileImages/" + Path.GetFileName(dt.Rows[0]["ImageName"].ToString());
                        divInfo += "<td rowspan='5' stye='width:200px'> " + " <img src='"+url+"'/> </td>";
                    }
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Admission Date</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["AdmissionDate"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Student Name</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Student Name Bangla</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["FullNameBn"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Shift</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["ShiftName"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Batch</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["BatchName"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Class</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["ClassName"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Group</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["GroupName"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Section</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["SectionName"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Roll No</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Gender</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["Gender"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Date Of Birth</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["DateOfBirth"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Religion</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["Religion"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Mobile</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["Mobile"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Admission Year</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["Session"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Blood Group</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["BloodGroup"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>ID No</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["IdCard"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Student Type</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["StdTypeName"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Father's Name</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["FathersName"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Father's Name Bangla</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["FatherNameBn"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Father's Profession</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["FathersProfession"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Father's Email</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["FatherEmail"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Father's Designation</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["FatherDesg"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Father's Organization</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["FatherOrg"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Fathers YearlyI ncome</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["FathersYearlyIncome"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Father's Phone</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["FatherPhone"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Father's Mobile</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["FathersMobile"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Mother's Name</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["MothersName"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Mother's Name Bangla</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["MotherNameBn"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Mother's Profession</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["MothersProfession"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Mother's Email</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["MotherEmail"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Mother's Designation</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["MotherDesg"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Mother's Organization</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["MotherOrg"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Mother's Yearly Income</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["MothersYearlyIncome"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Mother's Phone</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["MotherPhone"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Mother's Mobile</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["MothersMoible"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Guardian Name</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["GuardianName"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Guardian Relation</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["GuardianRelation"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Guardian MobileNo</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["GuardianMobileNo"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Guardian Address</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["GuardianAddress"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Permanent Village</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["PAVillage"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Permanent Post Office</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["PAPostOfficeName"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Permanent PAThana</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["PAThana"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Permanent District</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["PADistrict"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Adress</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["TAViIlage"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Present Post Office</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["TAPostOfficeName"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Present Thana/Upazila</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["TAThana"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Present District</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["TADistrict"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Prevous Exam Type</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["PreviousExamType"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Prevous Exam Board</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["PSCBoard"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Passing Year</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["PSCPassingYear"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Registration No</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["PSCJSCRegistration"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Roll</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["SSCRoll"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>GPA</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["PSCGPA"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>School Name</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["PreviousSchoolName"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Bus Name</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["BusName"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Location</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["LocationName"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Bus Stand</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["PlaceName"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>TC College Name</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["TCCollegeName"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Preferred Class</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["TCClass"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>TC Semister</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["TCSemister"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr>";
                    divInfo += "<td>Certified Date</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td colspan='2'>" + dt.Rows[x]["TCDate"].ToString() + "</td>";
                    divInfo += "</tr>";
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";
                    divInfo += "</table>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div> <br/><br/><br/><br><br><br>";
                    divIndivisualStudentList.Controls.Add(new LiteralControl(divInfo));
                    Session["_IndivisualStudentList_"] = dt;
                }
            }
            catch { }
        }
        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {           
            string condition = "";

            DataTable dt = new DataTable();
            if (currentstdEntry == null)
            {
                currentstdEntry = new CurrentStdEntry();
            }

            if (dlRoll.SelectedValue != "All")
            {
                condition += " WHERE StudentId='" + dlRoll.SelectedValue + "'";
            }
            else
            {
                condition = currentstdEntry.GetSearchCondition(dlShift.SelectedValue, dlBatch.SelectedValue, dlGroup.SelectedValue, dlSection.SelectedValue);
            }

            dt = currentstdEntry.GetCurrentStudentProfile(condition); 
            Session["_StudentProfile_"] = dt;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=StudentProfile');", true);  //Open New Tab for Sever side code
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            loadStudentInfo("");
        }
       
    }
}