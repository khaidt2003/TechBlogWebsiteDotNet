﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TechBlogWebsite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Home", "{type}",
                new { controller = "Default", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "trang-chu" }
                },
                namespaces: new[] { "TechBlogWebsite.Controllers" }
            );
            routes.MapRoute("Review", "{type}",
                new { controller = "Default", action = "TechReivews", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "blog-review" }
                },
                namespaces: new[] { "TechBlogWebsite.Controllers" }
            );
            routes.MapRoute("Contact", "{type}",
                new { controller = "Default", action = "Contact", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "lien-he" }
                },
                namespaces: new[] { "TechBlogWebsite.Controllers" }
            );
            routes.MapRoute("Video", "{type}",
                new { controller = "Default", action = "TechVideo", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "video-cong-nghe" }
                },
                namespaces: new[] { "TechBlogWebsite.Controllers" }
            );
            routes.MapRoute("Gadgets", "{type}",
                new { controller = "Default", action = "TechCategory", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "tien-ich" }
                },
                namespaces: new[] { "TechBlogWebsite.Controllers" }
            );
            routes.MapRoute("Techsingle", "{type}",
                new { controller = "Default", action = "TechSingle", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "detail" }
                },
                namespaces: new[] { "TechBlogWebsite.Controllers" }
            );
            routes.MapRoute("SignIn", "{type}",
                new { controller = "Account", action = "Login", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "login" }
                },
                namespaces: new[] { "TechBlogWebsite.Controllers" }
            );
            routes.MapRoute("SignUp", "{type}",
                new { controller = "Account", action = "register", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "signup" }
                },
                namespaces: new[] { "TechBlogWebsite.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] {"TechBlogWebsite.Controllers"}
                
            );
            
        }
    }
}
