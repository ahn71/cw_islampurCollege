using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration
{
    public partial class AdministrationHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aWebsite.HRef = "~/" + Classes.Routing.WSHomeRouteUrl;                
            }
        }
    }
}