using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechBlogWebsite.Help;
using TechBlogWebsite.Models;

namespace TechBlogWebsite.Areas.Admin.Controllers
{
    public class PostsController : Controller
    {
        private TechBlogDBNetEntities db = new TechBlogDBNetEntities();

        // GET: Admin/Posts
        public ActionResult Index(long? id = null)
        {
            getCategory(id);
            return View();
        }

        public void getCategory(long? selectedId = null)
        {
            ViewBag.Categories = new SelectList(db.Categories.Where(x => x.Hide == false)
                .OrderBy(x => x.Order), "CategoryID", "Name", selectedId);
        }

        // GET: Admin/Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.AuthorID = new SelectList(db.Users, "UserID", "Username");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "PostID,Title,Content,AuthorID,CategoryID,PublishedDate,ModifiedDate,Tags,FeaturedImage,Status,ViewCount,Link,Meta,Hide,Order,DateBegin")] Post post, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        //filename = Guid.NewGuid().ToString() + img.FileName;
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/upload/"), filename);
                        img.SaveAs(path);
                        post.FeaturedImage = filename; //Lưu ý
                    }
                    else
                    {
                        post.FeaturedImage = "logo.png";
                    }
                    post.DateBegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    post.PublishedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    post.ModifiedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()) ;
                    post.Meta = Functions.ConvertToUnSign(post.Meta); //convert Tiếng Việt không dấu
                    post.Order = getMaxOrder(post.CategoryID);
                    db.Posts.Add(post);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Index", "Posts", new { id = post.CategoryID });
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(post);
        }

        public ActionResult getPosts(long? id)
        {
            if (id == null)
            {
                var v = db.Posts.OrderBy(x => x.Order).ToList();
                return PartialView(v);
            }
            var m = db.Posts.Where(x => x.CategoryID == id).OrderBy(x => x.Order).ToList();
            return PartialView(m);
        }

        // GET: Admin/Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", post.CategoryID);
            ViewBag.AuthorID = new SelectList(db.Users, "UserID", "Username", post.AuthorID);
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] // Lưu ý: ValidateInput(false) sẽ tắt kiểm tra dữ liệu đầu vào, hãy sử dụng nó cẩn thận
        public ActionResult Edit([Bind(Include = "PostID,Title,Content,AuthorID,CategoryID,PublishedDate,ModifiedDate,Tags,FeaturedImage,Status,ViewCount,Link,Meta,Hide,Order,DateBegin")] Post post, HttpPostedFileBase img)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Lấy bài viết hiện tại từ database
                    var existingPost = db.Posts.Find(post.PostID);
                    if (existingPost == null)
                    {
                        return HttpNotFound();
                    }

                    if (img != null)
                    {
                        // Xử lý tải lên hình ảnh mới
                        var filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + Path.GetFileName(img.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/upload/"), filename);
                        img.SaveAs(path);
                        post.FeaturedImage = filename;
                    }
                    else
                    {
                        // Giữ nguyên hình ảnh hiện tại
                        post.FeaturedImage = existingPost.FeaturedImage;
                    }
                    post.ModifiedDate = DateTime.Now;

                    // Giữ các giá trị không được cập nhật
                    post.PublishedDate = existingPost.PublishedDate;
                    post.AuthorID = existingPost.AuthorID;

                    db.Entry(existingPost).CurrentValues.SetValues(post);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                // Xử lý các lỗi DbEntityValidationException ở đây
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Debug.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", post.CategoryID);
            ViewBag.AuthorID = new SelectList(db.Users, "UserID", "Username", post.AuthorID);
            return View(post);
        }




        // GET: Admin/Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public int getMaxOrder(long? CategoryId)
        {
            if (CategoryId == null)
                return 1;
            return db.Posts.Where(x => x.CategoryID == CategoryId).Count();
        }
        //ViewBag.getMaxOrder = getMaxOrder(product.categoryid) + 1;
    }
}
