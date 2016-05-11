﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IndexDocClinicos
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{page}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, page = UrlParameter.Optional }
            );

            /*routes.MapRoute(
                name: "Pagination",
                url: "{controller}/{action}/{id}/{page}",
                defaults: new { controller = "Home", action = "Index", id = "*:*", page=0 }
            );*/
        }
    }
}