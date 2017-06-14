using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Timers;

namespace Demolition
{
    public class MvcApplication : HttpApplication
    {
        public const string DefaultRoute = "/Demos/Index";

        public static void RegisterRoutes(RouteCollection routes)
        {
            object defaultRoute = new { controller = "Demos", action = "Index", id = "" };

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            // For the ASP.NET dev server
            routes.MapRoute("DevServerDefault", "Default.aspx", defaultRoute);

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                defaultRoute
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}