using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
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
            // Lưu URL hiện tại vào session trước khi chuyển hướng đến trang đăng nhập
            if (Session["idUser"] == null)
            {
                Session["ReturnUrl"] = Request.Url.AbsoluteUri;
            }
            Post post = _db.Posts.Find(id);
            var v = _db.Posts.FirstOrDefault(t => t.PostID == id && t.Meta == metatitle);
            if (v == null)
            {
                return HttpNotFound();
            }

            // Lấy link website từ cơ sở dữ liệu
            var websiteLink = v.Link;

            ViewBag.WebsiteLink = websiteLink;
            ViewBag.PostID = id;
            post.ViewCount++;
            _db.Entry(post).State = EntityState.Modified;
            _db.SaveChanges();
            return View(v);
        }
        public ActionResult GetPreAndPostPost(int postId)
        {

            ViewBag.PostId = postId;
            var selectedPost = _db.Posts.FirstOrDefault(p => p.PostID == postId);

            // Check if the requested post exists
            if (selectedPost == null)
            {
                // Return an error message indicating the post does not exist.
                return PartialView("Error", "Post not found.");
            }

            var previousPost = _db.Posts
                .Where(p => p.PostID < postId)
                .OrderByDescending(p => p.PostID)
                .FirstOrDefault();

            var nextPost = _db.Posts
                .Where(p => p.PostID > postId)
                .OrderBy(p => p.PostID)
                .FirstOrDefault();

            var result = new List<Post>();
            if (previousPost != null)
            {
                result.Add(previousPost);
            }
            if (nextPost != null)
            {
                result.Add(nextPost);
            }

            // Check if there are no previous or next posts
            if (result.Count == 0)
            {
                // Handle the case where there are no adjacent posts by returning a message.
                return PartialView("Info", "There are no adjacent posts.");
            }

            // Return the list of adjacent posts
            return PartialView(result);
        }



        public ActionResult RandomPost()
        {
            var allPosts = _db.Posts.ToList();

            var random = new Random();
            var randomPosts = allPosts.OrderBy(x => random.Next()).Take(2).ToList();

            return PartialView(randomPosts);
        }
        public ActionResult GetComments(int postId)
        {
            // Lấy tất cả các bình luận của bài viết với PostID là postId
            var comments = _db.Comments.Where(c => c.PostID == postId).ToList();
            return PartialView(comments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveComment(Comment model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy UserID từ session
                    var userId = Convert.ToInt32(Session["idUser"]);
                    model.UserID = userId;

                    // Thiết lập các thuộc tính bổ sung
                    model.PublishedDate = DateTime.Now; // Hoặc logic khác để thiết lập ngày xuất bản
                    model.IsApproved = false; // Thiết lập trạng thái phê duyệt mặc định
                    model.Meta = ""; // Thiết lập thông tin meta nếu cần
                    model.Hide = false; // Thiết lập trạng thái ẩn mặc định
                    model.Order = 0; // Thiết lập thứ tự mặc định
                    model.DateBegin = DateTime.Now; // Hoặc logic khác để thiết lập ngày bắt đầu

                    // Thêm bình luận vào cơ sở dữ liệu
                    _db.Comments.Add(model);
                    _db.SaveChanges();

                    return Redirect(model.Link);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    // Ghi nhật ký lỗi cạnh tranh
                    System.Diagnostics.Debug.WriteLine("Concurrency error saving comment: " + ex.Message);
                    ViewBag.ErrorMessage = "Có lỗi xảy ra khi lưu bình luận do dữ liệu bị thay đổi. Vui lòng thử lại.";
                }
                catch (DbUpdateException ex)
                {
                    // Ghi nhật ký lỗi cập nhật
                    System.Diagnostics.Debug.WriteLine("Update error saving comment: " + ex.Message);
                    ViewBag.ErrorMessage = "Có lỗi xảy ra khi lưu bình luận. Vui lòng thử lại.";
                }
                catch (Exception ex)
                {
                    // Ghi nhật ký lỗi khác
                    System.Diagnostics.Debug.WriteLine("Error saving comment: " + ex.Message);
                    ViewBag.ErrorMessage = "Có lỗi xảy ra khi lưu bình luận. Vui lòng thử lại.";
                }

                System.Diagnostics.Debug.WriteLine("Model Data: " +
                    $"CommentID: {model.CommentID}, " +
                    $"PostID: {model.PostID}, " +
                    $"UserID: {model.UserID}, " +
                    $"Content: {model.Content}, " +
                    $"PublishedDate: {model.PublishedDate}, " +
                    $"IsApproved: {model.IsApproved}, " +
                    $"Link: {model.Link}, " +
                    $"Meta: {model.Meta}, " +
                    $"Hide: {model.Hide}, " +
                    $"Order: {model.Order}, " +
                    $"DateBegin: {model.DateBegin}");

                // Trả về thông báo lỗi hoặc view với dữ liệu model để gỡ lỗi
                ViewBag.ModelData = model;
                return View("Error"); // Đảm bảo bạn có một view "Error" để hiển thị thông báo lỗi
            }

            // Xử lý trường hợp model state không hợp lệ
            ViewBag.ErrorMessage = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại và thử lại.";
            return RedirectToAction("Detail", new { id = model.PostID, error = "Invalid data" });
        }





    }
}