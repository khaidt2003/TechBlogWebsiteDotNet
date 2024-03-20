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
        TechBlogDBNetEntities _db = new TechBlogDBNetEntities();
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
        public ActionResult getPreAndPostPost(int postId)
        {
            ViewBag.PostId = postId;
            var selectedPost = _db.Posts.FirstOrDefault(p => p.PostID == postId);
            if (selectedPost == null)
            {
                return PartialView(null);
            }

            var previousPost = _db.Posts
                .Where(p => p.CategoryID == selectedPost.CategoryID && p.PostID < postId)
                .OrderByDescending(p => p.PostID)
                .FirstOrDefault();

            var nextPost = _db.Posts
                .Where(p => p.CategoryID == selectedPost.CategoryID && p.PostID > postId)
                .OrderBy(p => p.PostID)
                .FirstOrDefault();

            if (previousPost == null && nextPost == null)
            {
                // Trả về một danh sách trống nếu không có bài đăng nào phía trước hoặc phía sau
                return PartialView(new List<Post>());
            }

            var result = new List<Post>();
            if (previousPost != null)
            {
                result.Add(previousPost);
            }
            if (nextPost != null)
            {
                result.Add(nextPost);
            }

            return PartialView(result);
        }
        public ActionResult RandomPost()
        {
            var allPosts = _db.Posts.ToList();

            var random = new Random();
            var randomPosts = allPosts.OrderBy(x => random.Next()).Take(2).ToList();

            return PartialView(randomPosts);
        }

    }
}