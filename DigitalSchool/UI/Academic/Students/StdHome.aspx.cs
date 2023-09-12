using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academics.Students
{
    public partial class StdHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //---url bind---
                    aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                    aAcademicHome.HRef = "~/" + Classes.Routing.AcademicRouteUrl;

                    aStudentAdd.HRef = "~/" + Classes.Routing.StudentAddRouteUrl;
                    aStudentList.HRef = "~/" + Classes.Routing.StudentListRouteUrl;
                    aStudentActivation.HRef = "~/" + Classes.Routing.StudentActivationRouteUrl;
                    aStudentSectionChange.HRef = "~/" + Classes.Routing.StudentSectionChangeRouteUrl;
                    aStudentPromotionRoute.HRef = "~/" + Classes.Routing.StudentPromotionRouteUrl;
                    aStudentAdmissionApproval.HRef = "~/" + Classes.Routing.StudentAdmissionApprovalRouteUrl;

                    //---url bind end---


                    if (Request.QueryString["hasperm"].ToString() != null) lblMessage.InnerText = "warning->You have not any privilege for this page.Please set privilege.";
                }
                catch { }
            }
        }
    }
}