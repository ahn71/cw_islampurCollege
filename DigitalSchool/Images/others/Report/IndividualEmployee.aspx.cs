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
    public partial class IndividualEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblYear.Text = System.DateTime.Now.Year.ToString();
                loadTeacherInfo("");
                lblTitle.Text = Session["__Title__"].ToString();
            }
            catch { }
        }


        private void loadTeacherInfo(string sqlCmd)
        {
            try
            {
                string employeeId = Request.QueryString["employeeId"];
                DataTable dt = new DataTable();
                if (employeeId == null)
                {
                    if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "select EID,ECardNo,convert(varchar(11),EJoiningDate,106) as EJoiningDate,EName,EGender,EFathersName,EMothersName,DName,DesName,EReligion,EMaritalStatus,ECardNo,EPhone,EMobile,EEmail,EBirthday,EPresentAddress,EParmanentAddress,EBloodGroup,ELastDegree,EExaminer,ENationality,EPictureName from v_EmployeeInfo";
                    sqlDB.fillDataTable(sqlCmd, dt);
                }
                else
                {
                    if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "select EID,ECardNo,convert(varchar(11),EJoiningDate,106) as EJoiningDate,EName,EGender,EFathersName,EMothersName,DName,DesName,EReligion,EMaritalStatus,ECardNo,EPhone,EMobile,EEmail,EBirthday,EPresentAddress,EParmanentAddress,EBloodGroup,ELastDegree,EExaminer,ENationality,EPictureName  from v_EmployeeInfo where EID ='" + employeeId + "' ";
                    sqlDB.fillDataTable(sqlCmd, dt);
                }

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Teacher available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    IndividualEmployeeReport.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    divInfo = " <table id='tblTeacherInfo'  > ";
                    divInfo += "<thead>";



                    divInfo += "</thead>";

                    divInfo += "<tbody>";

                    string id = "";

                    //for (int x = 0; x < dt.Rows.Count; x++)
                    //{
                    id = dt.Rows[x]["EID"].ToString();

                    divInfo += "<tr>";
                    divInfo += "<td>Card No</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["ECardNo"].ToString() + "</td>";


                    divInfo += "<td rowspan='6' stye='width:200px'> " + " <img src='/Images/teacherProfileImage/" + dt.Rows[x]["EPictureName"].ToString() + "'/> </td>";
                    divInfo += "</tr>";


                    divInfo += "<tr>";
                    divInfo += "<td>Joining Date</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td  >" + dt.Rows[x]["EJoiningDate"].ToString() + "</td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Employee Name</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["EName"].ToString() + "</td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Gender</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["EGender"].ToString() + "</td>";

                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Father's Name</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["EFathersName"].ToString() + "</td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Mother's Name</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["EMothersName"].ToString() + "</td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Department name </td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["DName"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Designation  </td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["DesName"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td>Religion  </td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["EReligion"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";


                    divInfo += "<tr>";
                    divInfo += "<td>Marital Status  </td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["EMaritalStatus"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td> Phone </td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["EPhone"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td> Mobile </td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["EMobile"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td> Email </td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["EEmail"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td> Birth Day </td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["EBirthday"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td> Present Address</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["EPresentAddress"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td> Parmanent Address</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["EParmanentAddress"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td> Blood Group</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["EBloodGroup"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td> Last Degree</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["ELastDegree"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td> Examiner</td>";
                    divInfo += "<td>:</td>";

                    if (dt.Rows[x]["EExaminer"].ToString() == "True")
                    {
                        divInfo += "<td >" + "Yes" + "</td>"; ;
                    }
                    else
                    {
                        divInfo += "<td >" + "No" + "</td>";
                    }
                    
                    divInfo += "<td></td>";
                    divInfo += "</tr>";

                    divInfo += "<tr>";
                    divInfo += "<td> Nationality</td>";
                    divInfo += "<td>:</td>";
                    divInfo += "<td >" + dt.Rows[x]["ENationality"].ToString() + "</td>";
                    divInfo += "<td></td>";
                    divInfo += "</tr>";


                    divInfo += "<tr id='r_" + id + "'>";


                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";

                    divInfo += "</table>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div> <br/><br/><br/><br><br><br>";

                    IndividualEmployeeReport.Controls.Add(new LiteralControl(divInfo));

                }
            }
            catch { }
        }


    }
}