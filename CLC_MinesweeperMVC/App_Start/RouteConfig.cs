﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Recipe_Shop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Home",
                url: "{Home}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Login",
                url: "{Login}",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Registration",
                url: "{Registration}",
                defaults: new { controller = "Registration", action = "User", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "MineSweep",
                url: "{MineSweep}",
                defaults: new { controller = "Game", action = "", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Settings",
                url: "{Settings}",
                defaults: new { controller = "Settings", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Dashboard",
                url: "{Dashboard}",
                defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}