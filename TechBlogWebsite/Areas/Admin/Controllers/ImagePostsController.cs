using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechBlogWebsite.Models;

namespace TechBlogWebsite.Areas.Admin.Controllers
{
    public class ImagePostsController : Controller
    {
        private TechBlogDBNetEntities db = new TechBlogDBNetEntities();

        // GET: Admin/ImagePosts
        public ActionResult Index()
        {
            var imagePosts = db.ImagePosts.Include(i => i.Post);
            return View(imagePosts.ToList());
        }

        // GET: Admin/ImagePosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagePost imagePost = db.ImagePosts.Find(id);
            if (imagePost == null)
            {
                return HttpNotFound();
            }
            return View(imagePost);
        }

        // GET: Admin/ImagePosts/Create
        public ActionResult Create()
        {
            ViewBag.PostID = new SelectList(db.Posts, "PostID", "Title");
            return View();
        }

        // POST: Admin/ImagePosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PostID,FeaturedImage2,FeaturedImage3,Meta,Hide,Order,DateBegin")] ImagePost imagePost)
        {
            if (ModelState.IsValid)
            {
                db.ImagePosts.Add(imagePost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PostID = new SelectList(db.Posts, "PostID", "Title", imagePost.PostID);
            return View(imagePost);
        }

        // GET: Admin/ImagePosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagePost imagePost = db.ImagePosts.Find(id);
            if (imagePost == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostID = new SelectList(db.Posts, "PostID", "Title", imagePost.PostID);
            return View(imagePost);
        }

        // POST: Admin/ImagePosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PostID,FeaturedImage2,FeaturedImage3,Meta,Hide,Order,DateBegin")] ImagePost imagePost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imagePost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostID = new SelectList(db.Posts, "PostID", "Title", imagePost.PostID);
            return View(imagePost);
        }

        // GET: Admin/ImagePosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagePost imagePost = db.ImagePosts.Find(id);
            if (imagePost == null)
            {
                return HttpNotFound();
            }
            return View(imagePost);
        }

        // POST: Admin/ImagePosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImagePost imagePost = db.ImagePosts.Find(id);
            db.ImagePosts.Remove(imagePost);
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
    }
}
