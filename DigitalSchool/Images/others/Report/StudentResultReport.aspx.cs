using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class StudentResultReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                DataTable dtp = (DataTable)Session["__PassList__"];
                DataTable dtf = (DataTable)Session["__FailList__"];
                lblYear.Text = System.DateTime.Now.Year.ToString();
                lblClass.Text = Session["__ClassInfo__"].ToString();
                if (dtp.Rows.Count > 0)
                {
                    gvPassListOfStudent.DataSource = dtp;
                    gvPassListOfStudent.DataBind();
                    gvPassListOfStudent.Caption = "Pass Student List";
                }
                if (dtf.Rows.Count > 0)
                {
                    gvFailListOfStudent.DataSource = dtf;
                    gvFailListOfStudent.DataBind();
                    gvFailListOfStudent.Caption = "Fail Student List";
                }
            }
            catch { }

            try
            {
                string failSubReport = Session["__FaillSubject__"].ToString();
                divLoadFailSubject.Controls.Add(new LiteralControl(failSubReport));
            }
            catch { }
        }
    }
}