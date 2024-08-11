using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.DSWS
{
    public partial class DSWSHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aAdministration.HRef = "~/" + Classes.Routing.AdministrationRouteUrl;

                aNotices.HRef = "~/" + Classes.Routing.NoticeListRouteUrl;
                aSpeeches.HRef = "~/" + Classes.Routing.AddSpeechesRouteUrl;
                aSlider.HRef = "~/" + Classes.Routing.SliderListRouteUrl;
                aQuickLink.HRef = "~/" + Classes.Routing.QuickLinkListRouteUrl;                
                aWSGeneralSettings.HRef = "~/" + Classes.Routing.WSGeneralSettingsRouteUrl;

            }
        }
    }
}