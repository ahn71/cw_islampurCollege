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
    public partial class MonthWiseAttendanceSheetSummary : System.Web.UI.Page
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

        private void loadAttendanceSummary()
        {
            try
            {
                Session["__AttendanceSheet__"] = dlSheetName.Text;
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select StudentProfile.FullName,CurrentStudentInfo.RollNo, " + Session["__AttendanceSheet__"] + ".* from " 
                + Session["__AttendanceSheet__"] + " , StudentProfile,CurrentStudentInfo where " + Session["__AttendanceSheet__"] + ".StudentId=StudentProfile.StudentId "
                +"and " + Session["__AttendanceSheet__"] + ".StudentId=CurrentStudentInfo.StudentId Order By CurrentStudentInfo.RollNo ", dt);
                dt.Columns.Add("Total Present");           //Add New Columns Total Present
                dt.Columns.Add("Total Absent");           //Add New Columns Total Absent
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
                    dt.Rows[i]["Total Present"] = totalpresent;     //Add New Row Total Present
                    dt.Rows[i]["Total Absent"] = totalabsent;      //Add New Row Total Absent
                }
                dt.Columns.Remove("StudentId");
               DataView dv = new DataView(dt);
               DataTable dtsummary = dv.ToTable(true, "FullName", "RollNo", "Total Present", "Total Absent");
               Session["__AttendanceSummaryReport__"] = dtsummary;
               gvAttendanceSummary.DataSource = dtsummary;
               gvAttendanceSummary.DataBind();
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadAttendanceSummary();
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/Report/AttendanceSummaryReport.aspx");
            }
            catch { }
        }


    }
}