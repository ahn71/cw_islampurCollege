using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class AttendanceDetailsReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblReportName.Text = Session["__ReportType__"].ToString();
                lblBatch.Text = Session["__Batch__"].ToString();
                lblShift.Text = Session["__Shift__"].ToString();
                lblSection.Text = Session["__Section__"].ToString();
                string divInfo = Session["__AttendanceDetails__"].ToString();
                divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
    }
}