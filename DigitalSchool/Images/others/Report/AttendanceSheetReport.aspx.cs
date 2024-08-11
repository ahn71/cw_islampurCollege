using adviitRuntimeScripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DS.Report
{
    public partial class MonthWiseAttendanceSheeReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblBatch.Text = Session["__AttendanceSheet__"].ToString();
                DataTable dt = (DataTable)Session["__AttendanceSheetReport__"];
                gvAttendanceSheet.DataSource = dt;
                gvAttendanceSheet.DataBind();
            }
            catch { }
        }

    }
}