using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.Admission;
using System.Data;
using DS.BLL.ManagedSubject;
using DS.Classes;
using DS.BLL.ManagedClass;

namespace DS.UI.DSWS
{
    public partial class StudentContact : System.Web.UI.Page
    {
        string divInfo = "";
        StdGroupSubSetupDetailsEntry subjectList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                commonTask.loadYearFromBatch(ddlYear);
                ClassEntry.GetEntitiesData(ddlClass);
                txtAdmNo.Focus();
            }
        }
       
        private void loadStudentContacts() 
        {
            try
            {
               
                CurrentStdEntry std = new CurrentStdEntry();
                DataTable dt = new DataTable();
                dt = std.GetStudentInfoByAdmissionNo(ddlClass.SelectedItem.Text+ddlYear.SelectedItem.Text, txtAdmNo.Text.Trim());
                if (dt == null || dt.Rows.Count < 1)
                {
                   // divInfo = "<h4 class='title'>Student Not Found</h4>";
                    hMessage.Visible = true;
                }
                else
                {                   
                    if (subjectList == null)
                    {
                        subjectList = new StdGroupSubSetupDetailsEntry();
                    }
                    string subjects = "";
                    DataTable dtsubjects = subjectList.GetStudentSubjects(dt.Rows[0]["BatchId"].ToString(), dt.Rows[0]["ClassID"].ToString(), dt.Rows[0]["ClsGrpId"].ToString(), dt.Rows[0]["StudentID"].ToString());
                    if (dtsubjects.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtsubjects.Rows.Count; i++)
                        {
                            subjects += "<li>"+ dtsubjects.Rows[i]["SubName"].ToString()+" " + dtsubjects.Rows[i]["CourseName"].ToString() +"(" + dtsubjects.Rows[i]["SubCode"].ToString() + ")"+((dtsubjects.Rows[i]["Compulsory"].ToString()=="False")? "[Optional]":"")+"</li>";
                        }
                    }
                    hMessage.Visible = false;
                    string imgName = (dt.Rows[0]["ImageName"].ToString() == "") ? "" : dt.Rows[0]["ImageName"].ToString();
                    string Status ="Regular"; 
                    string Color="Green";
                    if(dt.Rows[0]["StdStatus"].ToString().Equals("False"))
                    {
                        Status="Irregular";
                         Color="Red";
                    }
                    divInfo = "<img class='img-responsive' src='../../Images/profileImages/" + imgName + "' alt='...' />" +
                        "<h4 class='title'>" + dt.Rows[0]["FullName"].ToString() + "</h4>" +
                                   "<div class='tblDiv'>"+
                                "<table class='tblStudentInfo'>" +
                                    "<tr><td>Shift</td><td>: </td><td>" + dt.Rows[0]["ShiftName"].ToString() + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>" +
                                    "<tr ><td>Class</td><td>: </td><td colspan='3' >" + dt.Rows[0]["ClassName"].ToString() + "</td><td>&nbsp;</td></tr>" +
                                    "<tr><td>Group</td><td>: </td><td>" + dt.Rows[0]["GroupName"].ToString() + "</td>" +
               " <td>Section</td><td>:</td><td>" + dt.Rows[0]["SectionName"].ToString() + "</td></tr>" +
                                    "<tr><td>Roll</td><td>: </td><td>" + dt.Rows[0]["RollNo"].ToString() + "</td>" +
               " <td>Status</td><td>:</td><td style='Color:"+Color+"'>" + Status + "</td></tr>" +
                              "  </table> </div>";

                    divInfo = @"<div class='text-center'>
                <p><img style = 'height: 120px' class='img-responsive' src='../../Images/profileImages/" + imgName + @"' alt='...'></p>
                </div>
        <h4 class='title'><strong>Name of Student :</strong><span style = 'color: #000;'>" + dt.Rows[0]["FullName"].ToString() + @"</span></h4>
        <div class='tblDiv'><table class='tblStudentInfo'><tbody>
<tr>
<td style = 'width: 19%'>Father's Name</td><td style ='width: 1%'>:</td><td style = 'width: 30%'><h5>" + dt.Rows[0]["FathersName"].ToString() + @"</h5></td>
<td style = 'width: 19%'>Mother's Name</td><td style = 'width: 1%'>:</td><td style = 'width: 30%'><h5>" + dt.Rows[0]["MothersName"].ToString() + @"</h5></td>
</tr>
<tr><td>Session</td><td>:</td><td>" + dt.Rows[0]["Session"].ToString() + @"</td><td>Class Roll</td><td>:</td><td>" + dt.Rows[0]["RollNo"].ToString() + @"</td></tr>
<tr><td>Class</td><td>:</td><td>" + dt.Rows[0]["ClassName"].ToString() + @"</td><td>Group</td><td>:</td><td>" + dt.Rows[0]["GroupName"].ToString() + @"</td></tr>
<tr><td>Shift</td><td>:</td><td>" + dt.Rows[0]["ShiftName"].ToString() + @"</td><td>Section</td><td>:</td><td>" + dt.Rows[0]["SectionName"].ToString() + @"</td></tr>
<tr><td>Date of Birth</td><td>:</td><td>" + dt.Rows[0]["DateOfBirth"].ToString() + @"</td><td>Religion</td><td>:</td><td>" + dt.Rows[0]["Religion"].ToString() + @"</td></tr>
<tr><td>Subject</td><td>:</td><td colspan = '4'>
    <ul style= 'list-style: none; padding-left: 0;'>
        "+ subjects + @"
    </ul>
</td></tr>
</tbody>
</table>
</div>";
                }
                ForLeftSideMenuList_divStudentContacts.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            loadStudentContacts();
        }
    }
}