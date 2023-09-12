using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.HR.Payroll
{
    public partial class PayrollHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["hasperm"].ToString() != null) lblMessage.InnerText = "warning->You have not any privilege for this page.Please set privilege.";
            }
            catch { }
        }
    }
}