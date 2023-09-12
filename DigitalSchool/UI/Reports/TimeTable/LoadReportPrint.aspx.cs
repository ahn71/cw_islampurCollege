using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.TimeTable
{
    public partial class LoadReportPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {              
                
                DataTable dtSclInfo = new DataTable();               
                dtSclInfo = Classes.commonTask.LoadShoolInfo();
                hSchoolName.InnerText = dtSclInfo.Rows[0]["SchoolName"].ToString();
                aAddress.InnerText = dtSclInfo.Rows[0]["Address"].ToString();
                pTitle.InnerText = "Teacher's Load Report - " + DateTime.Now.Year.ToString();
                string div = Session["__LoadReport__"].ToString();
                divLoadReport.Controls.Add(new LiteralControl(Session["__LoadReport__"].ToString()));
            }
            catch { }
        }
    }
}