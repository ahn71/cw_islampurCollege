using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DS.Report
{
    public partial class AllReportPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                loadReport();
            }
            catch { }
        }

        private void loadReport()
        {
            try
            {
                string divInfo = Session["_ReportQuery_"].ToString();
                lblYear.Text = Session["__Title__"].ToString();
                divAllReport.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

    }
}