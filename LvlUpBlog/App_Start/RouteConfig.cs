using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LvlUpBlog.Controllers;

namespace LvlUpBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var namespaces = new[] { typeof(PostsController).Namespace };

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Home Route
            //------------------------------------------------------------
            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Posts", action = "Index"},
                namespaces : namespaces
            );

            // Login Route
            //------------------------------------------------------------
            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Auth", action = "Login" },
                namespaces : namespaces
            );

            // Logout Route
            //------------------------------------------------------------
            routes.MapRoute(
                name: "Logout", 
                url: "logout",
                defaults: new {controller="Auth", action="Logout"},
                namespaces: namespaces
            );

            // Post View Route
            //------------------------------------------------------------
            routes.MapRoute("Post", "post/{id}-{slug}", new { controller = "Posts", action = "Show" }, namespaces);
            //routes.MapRoute("Post", "post/{id}-{slug}", new { controller = "Posts", action = "Show" }, namespaces);

            // Tags Route
            //------------------------------------------------------------
            routes.MapRoute("RealTag", "tag/{idAndSlug}", new { controller = "Posts", action = "Tag" }, namespaces);
            routes.MapRoute("Tag", "tag/{id}-{slug}", new { controller = "Posts", action = "Tag" }, namespaces);

            // Sidebar sub-controller
            //------------------------------------------------------------
            routes.MapRoute("Sidebar", "", new { controller = "SideBar", action = "Index" }, namespaces);

            routes.MapRoute("Error404", "errors/404", new { controller = "Errors", action = "FourHundredError" }, namespaces);
            routes.MapRoute("Error500", "errors/500", new { controller = "Errors", action = "FiveHundredError" }, namespaces); 
        }
    }
}