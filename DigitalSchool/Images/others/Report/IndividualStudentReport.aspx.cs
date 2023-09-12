using adviitRuntimeScripting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class IndivisualStudentReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblYear.Text = System.DateTime.Now.Year.ToString();
                DataTable dt = new DataTable();
                dt = (DataTable)Session["_IndivisualStudentList_"];
                loadStudentInfo(dt);
            }
            catch { }
        }


        private void loadStudentInfo(DataTable dt)
        {
            try
            {

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Student available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    IndividualStudentReport.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    divInfo = " <table id='tblStudentInfo' class='display'  > ";
                    divInfo += "<thead>";

                    divInfo += "<tbody>";

                    string id = "";

                    id = dt.Rows[x]["StudentId"].ToString();

                    divInfo += "<tr>";
                    divInfo += "<td>Admission No </td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["AdmissionNo"].ToString() + "</td>";


                    divInfo += "<td rowspan='6' stye='width:200px'> " + " <img src='/Images/profileImages/" + dt.Rows[x]["ImageName"].ToString() + "'/> </td>";
                    divInfo += "</tr>";


                    divInfo += "<tr>";
                    divInfo += "<td>Admission Date</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["AdmissionDate"].ToString() + "</td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Class</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["ClassName"].ToString() + "</td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Section</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["SectionName"].ToString() + "</td>";

                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Roll No</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Full Name</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Gender</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["Gender"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Date Of Birth</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["DateOfBirth"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Blood Group</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["BloodGroup"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";


                    divInfo += "<tr>";
                    divInfo += "<td>Mobile</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["Mobile"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Father's Name</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["FathersName"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Father's Profession</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["FathersProfession"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Fathers YearlyI ncome</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["FathersYearlyIncome"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Father's Mobile</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["FathersMobile"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";


                    divInfo += "<tr>";
                    divInfo += "<td>Mother's Name</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["MothersName"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Mother's Profession</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["MothersProfession"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Mother's Yearly Income</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["MothersYearlyIncome"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Mother's Mobile</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["MothersMoible"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Home Phone</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["HomePhone"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";


                    divInfo += "<tr>";
                    divInfo += "<td>Parmanent Village</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["PAVillage"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Parmanent Post Office</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["PAPostOffice"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Parmanent PAThana</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["PAThana"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Parmanent District</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["PADistrict"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Present ViIlage</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["TAViIlage"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Present Post Office</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["TAPostOffice"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Present Thana</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["TAThana"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Present District</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["TADistrict"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Guardian Name</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td  >" + dt.Rows[x]["GuardianName"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Guardian Relation</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["GuardianRelation"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Guardian MobileNo</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["GuardianMobileNo"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Guardian Address</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td  >" + dt.Rows[x]["GuardianAddress"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Mother Tongue</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["MotherTongue"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Nationality</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["Nationality"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Previous School Name</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["PreviousSchoolName"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Transfer Certified No</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["TransferCertifiedNo"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Certified Date</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["CertifiedDate"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Preferred Class</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["PreferredClass"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>PSC GPA</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["PSCGPA"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>PSC Roll No</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td>" + dt.Rows[x]["PSCRollNo"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>PSC Board</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td  >" + dt.Rows[x]["PSCBoard"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>PSC Passing Year</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["PSCPassingYear"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";



                    divInfo += "<tr id='r_" + id + "'>";


                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";

                    divInfo += "</table>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div> <br/><br/><br/><br><br><br>";

                    IndividualStudentReport.Controls.Add(new LiteralControl(divInfo));

                }
            }
            catch { }
        }

    }
}