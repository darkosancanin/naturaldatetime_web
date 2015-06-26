using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NaturalDateTime.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "admin",
                defaults: new { controller = "Admin", action = "Login" }
            );

            routes.MapRoute(
                name: "QuestionFromUrl",
                url: "q/{q}",
                defaults: new { controller = "Home", action = "Question" }
            );

            routes.MapRoute(
                name: "QuestionFromQuerystring",
                url: "q",
                defaults: new { controller = "Home", action = "Question" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
