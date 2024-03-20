using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechBlogWebsite.Models;

namespace TechBlogWebsite.Controllers
{
    public class AuthorController : Controller
    {
        TechBlogDBNetEntities _db = new TechBlogDBNetEntities();
        // GET: Author
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Author(long id, string metatitle)
        {
            var v = from t in _db.Users
                    where t.UserID == id && t.Meta == metatitle
                    select t;
            return View(v.FirstOrDefault());
        }
        public ActionResult getAllAuthorPosts(long id)
        {
            var authorPosts = _db.Posts.Where(p => p.AuthorID == id).ToList();
            return PartialView(authorPosts);
        }

    }
}