using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academics.Attendance.StafforFaculty
{
    public partial class StafforFaculty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["hasperm"].ToString() != null) lblMessage.InnerText = "warning->You have not any privilege for this page.Please set privilege.";
                }
                catch { }
            }
        }
    }
}