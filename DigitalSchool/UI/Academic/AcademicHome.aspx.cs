using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academics
{
    public partial class AcademicHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aStudent.HRef = "~/" + Classes.Routing.StudentHomeRouteUrl;
                ExamHome.HRef = "~/" + Classes.Routing.ExaminationHomeRouteUrl;
            }
        }
    }
}