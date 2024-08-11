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
    public partial class MonthWiseAttendanceShee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["__UserId__"] == null)
                {
                    Response.Redirect("~/UserLogin.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        Classes.commonTask.loadAttendanceSheet(dlSheetName);
                    }
                }
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadAttendanceSheet();
        }

        private void loadAttendanceSheet()
        {
            try
            {
                Session["__AttendanceSheet__"] = dlSheetName.Text;
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select StudentProfile.FullName,CurrentStudentInfo.RollNo, " + Session["__AttendanceSheet__"] + ".* from " + Session["__AttendanceSheet__"] + " , StudentProfile,CurrentStudentInfo where " + Session["__AttendanceSheet__"] + ".StudentId=StudentProfile.StudentId and " + Session["__AttendanceSheet__"] + ".StudentId=CurrentStudentInfo.StudentId Order By CurrentStudentInfo.RollNo ASC ", dt);
                dt.Columns.Add("T.P");           //Add New Columns Total Present
                dt.Columns.Add("T.A");           //Add New Columns Total Absent
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int totalabsent = 0, totalpresent = 0;
                    for (int x = 0; x < dt.Columns.Count; x++)
                    {
                        if (dt.Rows[i].ItemArray[x].ToString().ToUpper() == "a".ToUpper())
                        {
                            totalabsent++;
                        }
                        else if (dt.Rows[i].ItemArray[x].ToString().ToUpper() == "p".ToUpper())
                        {
                            totalpresent++;
                        }
                    }
                    dt.Rows[i]["T.P"] = totalpresent;     //Add New Row Total Present
                    dt.Rows[i]["T.A"] = totalabsent;      //Add New Row Total Absent
                }
                dt.Columns.Remove("StudentId");
                Session["__AttendanceSheetReport__"] = dt;
                gvAttendanceSheet.DataSource = dt;
                gvAttendanceSheet.DataBind();
            }
            catch { }
        }

        private void loadMonthWiseAttendanceSheet()
        {
            try
            {
                string sqlCmd = "select * from " + dlSheetName.Text + "  ";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Student Attendance available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
                    btnPrintPreview.Visible = false;
                    return;
                }
                divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                divInfo += "<th style='width:260px'>Full Name</th>";
                divInfo += "<th class='numeric' style='width:120px'>Roll</th>";
                divInfo += "<th style='width:260px'>Guardian Name</th>";
                divInfo += "<th>Guardian Relation</th>";
                divInfo += "<th class='numeric'>Guardian Mobile</th>";
                divInfo += "<th>Guardian Address</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    int sl = x + 1;
                    divInfo += "<tr >";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["GuardianName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["GuardianRelation"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["GuardianMobileNo"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["GuardianAddress"].ToString() + "</td>";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Report/AttendanceSheetReport.aspx");
        }
    }
}