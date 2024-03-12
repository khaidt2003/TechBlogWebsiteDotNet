using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechBlogWebsite.Models;

namespace TechBlogWebsite.Controllers
{
    public class PostController : Controller
    {
        TechBlogDBNetEntities1 _db = new TechBlogDBNetEntities1();
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(long id, string metatitle)
        {
            var v = from t in _db.Posts
                    where t.PostID == id && t.Meta == metatitle
                    select t;
            return View(v.FirstOrDefault());
        }

    }
}