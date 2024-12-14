using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PostSphere
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SalaInterativa",
                url: "comentarios",
                defaults: new { controller = "Comment", action = "Index" }
            );

            routes.MapRoute(
                name: "NewComment",
                url: "comentarios/novo",
                defaults: new { controller = "Comment", action = "Create" }
            );

        }
    }
}
