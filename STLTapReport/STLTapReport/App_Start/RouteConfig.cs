﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace STLTapReport
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "AccountRoute",
                url: "account/{action}/",
                defaults: new { controller = "Account", action = "SignUp" }
            );

            routes.MapRoute(
                name: "AdminRoute",
                url: "admin/{action}/",
                defaults: new { controller = "Admin", action = "AdminWelcome" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{action}/",
                defaults: new { controller = "Home", action = "Welcome" }
            );
        }
    }
}