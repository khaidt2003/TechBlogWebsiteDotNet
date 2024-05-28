using System;
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
                new { controller = "Default", action = "Jobs", meta = UrlParameter.Optional },
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
                    { "type", "jobs-cong-nghe" }
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
            
            routes.MapRoute("TechTechnology", "{type}",
                new { controller = "Default", action = "TechTechnology", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "Cong-Nghe" }
                },
                namespaces: new[] { "TechBlogWebsite.Controllers" }
            );
            routes.MapRoute("TechScience", "{type}",
                new { controller = "Default", action = "TechSciences", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "Khoa-hoc" }
                },
                namespaces: new[] { "TechBlogWebsite.Controllers" }
            );
            routes.MapRoute("Techsingle", "{type}/{meta}/{metatitle}-{id}",
                new { controller = "Post", action = "Detail", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "blog" }
                },
                namespaces: new[] { "TechBlogWebsite.Controllers" }
            );
            routes.MapRoute("Techa", "{type}/{meta}/{metatitle}-{id}",
                new { controller = "Post", action = "getPreAndPostPost", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "blog" }
                },
                namespaces: new[] { "TechBlogWebsite.Controllers" }
            );
            routes.MapRoute("Author", "{type}/{metatitle}-{id}",
                new { controller = "Author", action = "Author", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "Author" }
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
