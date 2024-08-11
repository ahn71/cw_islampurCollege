using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Forms
{
    public partial class StudentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    Classes.commonTask.loadClass(dlClass);
                    sqlDB.loadDropDownList("Select  SectionName from Sections  Order by SectionName", dlSection);
                    loadStudentInfo("");
                }
            }
        }
        private void loadStudentInfo(string sqlCmd)
        {
            try
            {
                if (dlClass.Text == "All" && dlSection.Text == "All" && dlShift.Text == "All" ) sqlCmd = "Select  StudentId,AdmissionNo, convert(varchar(11),AdmissionDate,"
                +"106) as  AdmissionDate, ClassName, SectionName, RollNo, FullName, Gender, convert(varchar(11),DateOfBirth,106) as  DateOfBirth, BloodGroup, Mobile, "
                +"ImageName, FathersName, FathersProfession, FathersYearlyIncome, MothersName, MothersProfession, MothersYearlyIncome, FathersMobile, MothersMoible, "
                +"HomePhone, PAVillage, PAPostOffice, PAThana, PADistrict, TAViIlage, TAPostOffice, TAThana, TADistrict, GuardianName, GuardianRelation, GuardianMobileNo, "
                +"GuardianAddress, MotherTongue, Nationality, PreviousSchoolName, TransferCertifiedNo, CertifiedDate, PreferredClass, PSCGPA, PSCRollNo, PSCBoard, "
                +"PSCPassingYear from v_CurrentStudentInfo ";

                else if (dlClass.Text == "All" && dlSection.Text == "All" && dlShift.Text != "All" ) sqlCmd = "Select  StudentId,AdmissionNo, convert(varchar(11),"
                +"AdmissionDate,106) as  AdmissionDate, ClassName, SectionName, RollNo, FullName, Gender, convert(varchar(11),DateOfBirth,106) as  DateOfBirth, BloodGroup,"
                +"Mobile, ImageName, FathersName, FathersProfession, FathersYearlyIncome, MothersName, MothersProfession, MothersYearlyIncome, FathersMobile, MothersMoible,"
                +"HomePhone, PAVillage, PAPostOffice, PAThana, PADistrict, TAViIlage, TAPostOffice, TAThana, TADistrict, GuardianName, GuardianRelation, GuardianMobileNo, "
                +"GuardianAddress, MotherTongue, Nationality, PreviousSchoolName, TransferCertifiedNo, CertifiedDate, PreferredClass, PSCGPA, PSCRollNo, PSCBoard, "
                +"PSCPassingYear from v_CurrentStudentInfo  where Shift='" + dlShift.Text.Trim() + "' ";

                else if (dlClass.Text == "All" && dlSection.Text != "All" && dlShift.Text == "All" ) sqlCmd = "Select  StudentId,AdmissionNo, convert(varchar(11),"
                +"AdmissionDate,106) as  AdmissionDate, ClassName, SectionName, RollNo, FullName, Gender, convert(varchar(11),DateOfBirth,106) as  DateOfBirth, BloodGroup, "
                +"Mobile, ImageName, FathersName, FathersProfession, FathersYearlyIncome, MothersName, MothersProfession, MothersYearlyIncome, FathersMobile, MothersMoible,"
                +"HomePhone, PAVillage, PAPostOffice, PAThana, PADistrict, TAViIlage, TAPostOffice, TAThana, TADistrict, GuardianName, GuardianRelation, GuardianMobileNo, "
                +"GuardianAddress, MotherTongue, Nationality, PreviousSchoolName, TransferCertifiedNo, CertifiedDate, PreferredClass, PSCGPA, PSCRollNo, PSCBoard, "
                +"PSCPassingYear from v_CurrentStudentInfo  where SectionName='" + dlSection.Text + "' ";

                else if (dlClass.Text != "All" && dlSection.Text == "All" && dlShift.Text == "All" ) sqlCmd = "Select  StudentId,AdmissionNo, convert(varchar(11),"
                +"AdmissionDate,106) as  AdmissionDate, ClassName, SectionName, RollNo, FullName, Gender, convert(varchar(11),DateOfBirth,106) as  DateOfBirth, BloodGroup,"
                +"Mobile, ImageName, FathersName, FathersProfession, FathersYearlyIncome, MothersName, MothersProfession, MothersYearlyIncome, FathersMobile, MothersMoible,"
                +"HomePhone, PAVillage, PAPostOffice, PAThana, PADistrict, TAViIlage, TAPostOffice, TAThana, TADistrict, GuardianName, GuardianRelation, GuardianMobileNo, "
                +"GuardianAddress, MotherTongue, Nationality, PreviousSchoolName, TransferCertifiedNo, CertifiedDate, PreferredClass, PSCGPA, PSCRollNo, PSCBoard, "
                +"PSCPassingYear from v_CurrentStudentInfo  where ClassName='" + dlClass.Text + "' ";

                else if (dlClass.Text != "All" && dlSection.Text != "All" && dlShift.Text != "All" ) sqlCmd = "Select  StudentId,AdmissionNo, convert(varchar(11),"
                +"AdmissionDate,106) as  AdmissionDate, ClassName, SectionName, RollNo, FullName, Gender, convert(varchar(11),DateOfBirth,106) as  DateOfBirth, BloodGroup,"
                +"Mobile, ImageName, FathersName, FathersProfession, FathersYearlyIncome, MothersName, MothersProfession, MothersYearlyIncome, FathersMobile, MothersMoible,"
                +"HomePhone, PAVillage, PAPostOffice, PAThana, PADistrict, TAViIlage, TAPostOffice, TAThana, TADistrict, GuardianName, GuardianRelation, GuardianMobileNo, "
                +"GuardianAddress, MotherTongue, Nationality, PreviousSchoolName, TransferCertifiedNo, CertifiedDate, PreferredClass, PSCGPA, PSCRollNo, PSCBoard, "
                +"PSCPassingYear from v_CurrentStudentInfo  where ClassName='" + dlClass.Text + "' and SectionName='" + dlSection.Text + "' and Shift='" 
                + dlShift.Text.Trim() + "' ";

                else if (dlClass.Text == "All" && dlSection.Text != "All" && dlShift.Text != "All" ) sqlCmd = "Select  StudentId,AdmissionNo, convert(varchar(11),"
                +"AdmissionDate,106) as  AdmissionDate, ClassName, SectionName, RollNo, FullName, Gender, convert(varchar(11),DateOfBirth,106) as  DateOfBirth, BloodGroup,"
                +"Mobile, ImageName, FathersName, FathersProfession, FathersYearlyIncome, MothersName, MothersProfession, MothersYearlyIncome, FathersMobile, MothersMoible,"
                +"HomePhone, PAVillage, PAPostOffice, PAThana, PADistrict, TAViIlage, TAPostOffice, TAThana, TADistrict, GuardianName, GuardianRelation, GuardianMobileNo,"
                +"GuardianAddress, MotherTongue, Nationality, PreviousSchoolName, TransferCertifiedNo, CertifiedDate, PreferredClass, PSCGPA, PSCRollNo, PSCBoard,"
                +"PSCPassingYear from v_CurrentStudentInfo  where SectionName='" + dlSection.Text + "'  and Shift='" + dlShift.Text.Trim() + "' ";

                else if (dlClass.Text != "All" && dlSection.Text == "All" && dlShift.Text != "All" ) sqlCmd = "Select  StudentId,AdmissionNo, convert(varchar(11),"
                +"AdmissionDate,106) as  AdmissionDate, ClassName, SectionName, RollNo, FullName, Gender, convert(varchar(11),DateOfBirth,106) as  DateOfBirth, BloodGroup,"
                +"Mobile, ImageName, FathersName, FathersProfession, FathersYearlyIncome, MothersName, MothersProfession, MothersYearlyIncome, FathersMobile, MothersMoible,"
                +"HomePhone, PAVillage, PAPostOffice, PAThana, PADistrict, TAViIlage, TAPostOffice, TAThana, TADistrict, GuardianName, GuardianRelation, GuardianMobileNo,"
                +"GuardianAddress, MotherTongue, Nationality, PreviousSchoolName, TransferCertifiedNo, CertifiedDate, PreferredClass, PSCGPA, PSCRollNo, PSCBoard,"
                +"PSCPassingYear from v_CurrentStudentInfo  where  ClassName='" + dlClass.Text + "' and Shift='" + dlShift.Text.Trim() + "' ";

                else if (dlClass.Text != "All" && dlSection.Text != "All" && dlShift.Text == "All" ) sqlCmd = "Select  StudentId,AdmissionNo, convert(varchar(11),"
                +"AdmissionDate,106) as  AdmissionDate, ClassName, SectionName, RollNo, FullName, Gender, convert(varchar(11),DateOfBirth,106) as  DateOfBirth, BloodGroup,"
                +"Mobile, ImageName, FathersName, FathersProfession, FathersYearlyIncome, MothersName, MothersProfession, MothersYearlyIncome, FathersMobile, MothersMoible,"
                +"HomePhone, PAVillage, PAPostOffice, PAThana, PADistrict, TAViIlage, TAPostOffice, TAThana, TADistrict, GuardianName, GuardianRelation, GuardianMobileNo,"
                +"GuardianAddress, MotherTongue, Nationality, PreviousSchoolName, TransferCertifiedNo, CertifiedDate, PreferredClass, PSCGPA, PSCRollNo, PSCBoard,"
                +"PSCPassingYear from v_CurrentStudentInfo  where  ClassName='" + dlClass.Text + "' and SectionName='" + dlSection.Text + "' ";

                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Student available</div>";
                    divInfo += "<div><div class='head'></div></div>";
                    divStudentList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Name</th>";
                divInfo += "<th style='text-align:center'>Roll No</th>";
                divInfo += "<th style='text-align:center'>Class</th>";
                divInfo += "<th style='text-align:center'>Section</th>";
                divInfo += "<th style='text-align:center'>Gender</th>";
                divInfo += "<th>Mobile</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["StudentId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td  >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                    divInfo += "<td style='text-align:center;width:70px'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "<td style='text-align:center;width:80px'>" + dt.Rows[x]["ClassName"].ToString() + "</td>";
                    divInfo += "<td style='text-align:center;width:70px'>" + dt.Rows[x]["SectionName"].ToString() + "</td>";
                    divInfo += "<td style='text-align:center;width:55px'>" + dt.Rows[x]["Gender"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Mobile"].ToString() + "</td>";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divStudentList.Controls.Add(new LiteralControl(divInfo));
                Session["_StudentList_"] = dt;
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadStudentInfo("");
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["_StudentList_"];
            if (dt==null)
            {
                lblMessage.InnerText = "warning->Please Search By Class and Section";
                return;
            }
           // Response.Redirect("/Report/StudentList.aspx");
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/StudentList.aspx');", true);  //Open New Tab for Sever side code
        }
    }
}