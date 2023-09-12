using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.TimeTable
{
    public partial class ClassRoutine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblBatch.Text ="Batch:"+ Request.QueryString["Batch"];
            lblShift.Text ="Shift:"+ Request.QueryString["Shift"];           
            DayTimePanel.Controls.Add(new LiteralControl(Session["_ClassRoutine_"].ToString()));   
        }
    }
}