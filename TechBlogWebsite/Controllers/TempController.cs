using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechBlogWebsite.Models;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace TechBlogWebsite.Controllers
{
    public class TempController : Controller
    {
        TechBlogDBNetEntities _db = new TechBlogDBNetEntities();

        // GET: Temp
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getMenu() {
            var menus = _db.menus
                       .Where(m => (bool)!m.hide)
                       .OrderBy(m => m.order)
                       .Include(m => m.SubMenus)
                       .ToList();
            return PartialView(menus.ToList());
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

        public ActionResult getPopularBlogJob()
        {
            var topPosts = _db.Posts
                .Where(p => p.CategoryID == 5) // Assuming CategoryId is the property for category
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
        public ActionResult getAllPostGadgets()
        {
            var topPosts = _db.Posts
                .Where(p => p.CategoryID == 4) // Assuming CategoryId is the property for category
                .ToList();
            return PartialView(topPosts);
        }
        public ActionResult getAllPostJobs()
        {
            var topPosts = _db.Posts
                .Where(p => p.CategoryID == 5) // Assuming CategoryId is the property for category
                .OrderByDescending(p => p.PostID) // Sort by CreatedAt in descending order
                .ToList();
            return PartialView(topPosts);
        }

        public ActionResult getAllPostReviews()
        {
            var topPosts = _db.Posts
                .Where(p => p.CategoryID == 3) // Assuming CategoryId is the property for category
                .Take(10)
                .ToList();
            return PartialView(topPosts);
        }
        public ActionResult getAllPostTechnologys()
        {
            var topPosts = _db.Posts
                .Where(p => p.CategoryID == 2) // Assuming CategoryId is the property for category
                .Take(10)
                .ToList();
            return PartialView(topPosts);
        }
        public ActionResult getAllPostSciences()
        {
            var topPosts = _db.Posts
                .Where(p => p.CategoryID == 1) // Assuming CategoryId is the property for category
                .Take(10)
                .ToList();
            return PartialView(topPosts);
        }
        public ActionResult GetPopularPostsOfTechnology()
        {
            var topPosts = _db.Posts
                .Where(p => p.CategoryID == 2) 
                .OrderByDescending(t => t.ViewCount)
                .Take(4)
                .ToList();
            return PartialView(topPosts);
        }
        public ActionResult GetPopularPostsOfScience()
        {
            var topPosts = _db.Posts
                .Where(p => p.CategoryID == 1)
                .OrderByDescending(t => t.ViewCount)
                .Take(4)
                .ToList();
            return PartialView(topPosts);
        }
    }
}