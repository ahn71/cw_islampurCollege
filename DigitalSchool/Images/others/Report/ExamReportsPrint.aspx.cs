using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class ExamReportsPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblProgressReport.Text = Session["__lblProgress__"].ToString();
                lblName.Text = Session["__lblName__"].ToString();
                lblClass.Text = Session["__lblClass__"].ToString();
                lblShift.Text = Session["__lblShift__"].ToString();
                string divInfo = Session["__Reports__"].ToString();
                divProgressReport.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }


    }
}