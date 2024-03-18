using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechBlogWebsite.Models;
using System.Web.Mvc;

namespace TechBlogWebsite.Controllers
{
    public class TempController : Controller
    {
        TechBlogDBNetEntities1 _db = new TechBlogDBNetEntities1();

        // GET: Temp
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getMenu() { 
            var v = from t in _db.menus where t.hide== false orderby t.order ascending select t;
            return PartialView(v.ToList());
        }
        public ActionResult GetPopularPosts()
        {
            var topPosts = _db.Posts
                .OrderByDescending(t => t.ViewCount)
                .ThenByDescending(t => t.PublishedDate)
                .Take(3)
                .ToList();
            return PartialView(topPosts);
        }

        public ActionResult RecentNews()
        {
            var latestPosts = _db.Posts
            .OrderByDescending(t => t.PublishedDate)
            .Take(10)
            .ToList();
            return PartialView(latestPosts.ToList());
        }

        public ActionResult getPopularBlogVideo()
        {
            var topPosts = _db.Posts
                .Where(p => p.CategoryID == 4) // Assuming CategoryId is the property for category
                .OrderByDescending(t => t.ViewCount)
                .Take(3)
                .ToList();
            return PartialView(topPosts);
        }
        public ActionResult getPopularAllPosts()
        {
            var topPosts = _db.Posts
                .OrderByDescending(t => t.ViewCount)
                .Take(3)
                .ToList();
            return PartialView(topPosts);
        }
        public ActionResult getRecentReviews()
        {
            var topPosts = _db.Posts
                .Where(p => p.CategoryID == 4) // Assuming CategoryId is the property for category
                .OrderByDescending(t => t.ViewCount)
                .Take(3)
                .ToList();
            return PartialView(topPosts);
        }

    }
}