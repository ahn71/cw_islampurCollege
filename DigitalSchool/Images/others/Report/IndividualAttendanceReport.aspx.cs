using adviitRuntimeScripting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class IndividualAttendanceReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMonthName.Text = Session["__Month__"].ToString();              
                lblBatchName.Text = "Batch : " + Session["__BatchAttendance__"].ToString();
                lblShiftName.Text = "Shift : " + Session["__ShiftAttendance__"].ToString();
                lblSectionName.Text = "Section : " + Session["__SectionAttendance__"].ToString();

                if (Session["__dataTableDateRange__"] != null) generateAttendanceSheetByDateRange();
                else loadAttendanceReport();
                
            }
            catch { }
        }


        private void loadAttendanceReport()
        {
            try
            {
                DataTable dt = new DataTable();

                string findTbl = "AttendanceSheet_" + new String(Session["__BatchAttendance__"].ToString().Where(Char.IsLetter).ToArray()) + "_" + Session["__SectionAttendance__"].ToString() + "_" + Session["__MonthNameAttendance__"] + "";
                string attendanceQuery = "SELECT StudentProfile.FullName,StudentProfile.AdmissionNo,CurrentStudentInfo.RollNo, " + findTbl + ".* FROM StudentProfile INNER JOIN CurrentStudentInfo ON StudentProfile.StudentId = CurrentStudentInfo.StudentId INNER JOIN " + findTbl + " ON StudentProfile.StudentId = " + findTbl + ".StudentId where CurrentStudentInfo.BatchName='" + Session["__BatchAttendance__"].ToString() + "' and CurrentStudentInfo.Shift='" + Session["__ShiftAttendance__"].ToString() + "' and CurrentStudentInfo.SectionName='" + Session["__SectionAttendance__"].ToString() + "' and CurrentStudentInfo.StudentId=" + Request.QueryString["StudentId"] + " Order by CurrentStudentInfo.RollNo ";
                sqlDB.fillDataTable(attendanceQuery, dt);
                int TP = 0;
                int TA = 0;
                string divInfo;
                for (byte k = 0; k < dt.Columns.Count; k++)
                {

                    if (dt.Rows[0][dt.Columns[k].ToString()].ToString() == "a")
                    {
                        TA += 1;
                    }
                    else if (dt.Rows[0][dt.Columns[k].ToString()].ToString() == "p")
                    {
                        TP += 1;
                    }
                    
                }

                divInfo = " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                divInfo += "<tr>";
                divInfo += "<td style='width:5% ; padding-left:5px' >Admission No </td>";
                divInfo += "<td style='width:15% ; padding-left:5px' > " + dt.Rows[0]["AdmissionNo"] + " </td>";
                divInfo += "</tr>";

                divInfo += "<tr>";
                divInfo += "<td style='width:5% ;  padding-left:5px' >Roll No </td>";
                divInfo += "<td style='width:15% ;  padding-left:5px' > " + dt.Rows[0]["RollNo"] + " </td>";
                divInfo += "</tr>";

                divInfo += "<tr>";
                divInfo += "<td style='width:5% ; padding-left:5px' >Name </td>";
                divInfo += "<td style='width:15% ; padding-left:5px' > " + dt.Rows[0]["FullName"] + " </td>";
                divInfo += "</tr>";

                divInfo += "<tr>";
                divInfo += "<td style='width:5% ;  padding-left:5px' >Active Days </td>";
                divInfo += "<td style='width:15% ; padding-left:5px' > " + (TP+TA) + " </td>";
                divInfo += "</tr>";

                divInfo += "<tr>";
                divInfo += "<td style='width:5% ;  padding-left:5px' >Attendance </td>";
                divInfo += "<td style='width:15% ;  padding-left:5px' > " + TP  + " </td>";
                divInfo += "</tr>";

                divInfo += "<tr>";
                divInfo += "<td style='width:5% ;  padding-left:5px' >Absent </td>";
                divInfo += "<td style='width:15% ; padding-left:5px' > " + TA + " </td>";
                divInfo += "</tr>";

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divIndividualAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }


        int TP = 0;
        int TA = 0;
        int totalActiveDays = 0;
        int totalPresent = 0;
        int totalAbsent = 0;
        private void generateAttendanceSheetByDateRange()
        {
            try
            {
                 DataSet ds=(DataSet)Session["__dataTableDateRange__"];
                 DataTable dtMonth=(DataTable)Session["__MonthName__"];
                 string divInfo;

                 divInfo = "<h4 style=' margin: 0px;'>Attendance Information</h4>";
                 divInfo += " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                 divInfo += "<thead>";
                 divInfo += "<tr>";
                 divInfo += "<th >Month</th>";
                 divInfo += "<th style='text-align:center'>Active Days</th>";
                 divInfo += "<th style='text-align:center'>Attendance</th>";
                 divInfo += "<th style='text-align:center'>Absent</th>";
                 divInfo += "</tr>";
                 divInfo += "</thead>";

                 divInfo += "<tbody>";
                 
                 for (int j = 0; j < ds.Tables.Count; j++)
                 {
                     if (ds.Tables[j].Rows.Count == 0) goto Outer;

                     DataTable dt = ds.Tables[j].Select("").CopyToDataTable();
                     DataTable dtCertainStudent = dt.Select("StudentId=" + Request.QueryString["StudentId"].ToString() + "").CopyToDataTable();

                     lblMonthName.Text = "Admission No : " + dtCertainStudent.Rows[0]["AdmissionNo"].ToString();
                     lblRollNo.Text ="Roll No : "+ dtCertainStudent.Rows[0]["RollNo"].ToString();
                     lblName.Text ="Name : " + dtCertainStudent.Rows[0]["FullName"].ToString();

                     for (byte k = 0; k < dtCertainStudent.Columns.Count; k++)
                     {
                         if (dtCertainStudent.Rows[0][k].ToString() == "a")
                         {
                             TA += 1;
                         }
                         else if (dtCertainStudent.Rows[0][k].ToString() == "p")
                         {
                             TP += 1;
                         }
                     }

                     for (int x = 0; x < dtCertainStudent.Rows.Count; x++)
                     {
                         divInfo += "<tr>";
                         divInfo += "<td style='width:5% ; padding-left:5px' > " + dtMonth.Rows[j]["MonthName"] + " </td>";
                         divInfo += "<td style='width:15% ; text-align:center; padding-left:5px' > " + (TP + TA) + "</td>";
                         divInfo += "<td style='width:5% ; text-align:center; padding-left:5px' > " + TP + " </td>";
                         divInfo += "<td style='width:15% ; text-align:center; padding-left:5px' > " + TA + " </td>";                       
                     }
                     totalPresent += TP;
                     totalAbsent += TA;
                     totalActiveDays += TP + TA;
                     TP = 0;
                     TA = 0;

                     Outer:
                         continue;
                 }

                 divInfo += "</tbody>";
                 divInfo += "<tfoot>";

                 divInfo += "</table>";
                 divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                 divInfo += "<table class='display' style='width:595px;margin:0px auto; border:1px solid #DDD9D9' >";
                 divInfo += "<tbody>";
                 divInfo += "<tr>";
                 divInfo += "<td style='width:45px ; padding-left:5px' > Total : </td>";
                 divInfo += "<td style='width:102px ; text-align:center; padding-left:5px' > " + totalActiveDays + "</td>";
                 divInfo += "<td style='width:49px ; text-align:center; padding-left:5px' > " + totalPresent + " </td>";
                 divInfo += "<td style='width:100px ; text-align:center; padding-left:5px' > " + totalAbsent + " </td>";
                 divInfo += "</tr>";
                 divInfo += "</tbody>";
                 divInfo += "</table>";

                 divIndividualAttendaceSheet.Controls.Add(new LiteralControl(divInfo));

            }
            catch { }
        }
    }
}