using DS.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace DS
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
          
            // Code that runs on application startup
            Application["NoOfVisitors"] = 0;

            Routing.RegisterRoutes(RouteTable.Routes);
        }
        
        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            Application.Lock();
            Application["NoOfVisitors"] = (int)Application["NoOfVisitors"] + 1;
            Application.UnLock();

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }

    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute("API Default", "api/{controller}/{id}",
              new { id = RouteParameter.Optional });
        }
    }
}
