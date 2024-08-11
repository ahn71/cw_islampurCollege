using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class DueListReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {                
                lblBatch.Text = Session["__Class__"].ToString() + " (" + Session["__Section__"].ToString() + ")";

                string divInfo = Session["__DueListReport__"].ToString();
                divDueList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
    }
}