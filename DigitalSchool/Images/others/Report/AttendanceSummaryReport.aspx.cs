using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class AttendanceSummaryReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblBatch.Text = Session["__AttendanceSheet__"].ToString();
                DataTable dt = (DataTable)Session["__AttendanceSummaryReport__"];
                gvAttendanceSheetSummary.DataSource = dt;
                gvAttendanceSheetSummary.DataBind();
            }
            catch { }
        }
    }
}