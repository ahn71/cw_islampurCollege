using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class FacultyStaffAttendanceReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string employeeId = Request.QueryString["employeeId"];
                if (employeeId != null)
                {
                    lblReportName.Text = Session["__ReportType__"].ToString();
                    lblDepartment.Text = Session["__Department__"].ToString();
                    lblDesignation.Text = Session["Designation"].ToString();

                }
                else if (Session["__FacultyReport__"].ToString() != null)
                {
                    lblReportName.Text = Session["__ReportType__"].ToString();
                    lblDepartment.Text = Session["__Department__"].ToString();
                    lblDesignation.Text = Session["Designation"].ToString();
                    string divInfo = Session["__FacultyReport__"].ToString();
                    divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
                    Session["__FacultyReport__"] = null;
                }
                else
                {
                    lblReportName.Text = "Attendance Information";
                    lblDepartment.Text = Session["__Department__"].ToString();
                    lblDesignation.Text = Session["Designation"].ToString();
                    string divInfo = Session["__FacultyReport__"].ToString();
                    divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
                }


            }
            catch { }
        }
    }
}