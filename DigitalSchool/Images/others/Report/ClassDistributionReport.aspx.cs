using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class ClassDistributionReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblYear.Text = DateTime.Now.Year.ToString();
                lblTeacherName.Text ="Teacher Name : " + Session["__TeacherName__"].ToString();
                lblShift.Text = "Shift : " + Session["__Shift__"].ToString();
                divClassInfo.Controls.Add(new LiteralControl(Session["__ReportClassDis__"].ToString()));
            }
            catch { }
        }
    }
}