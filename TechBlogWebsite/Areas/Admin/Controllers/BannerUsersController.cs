using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechBlogWebsite.Models;

namespace TechBlogWebsite.Areas.Admin.Controllers
{
    public class BannerUsersController : Controller
    {
        private TechBlogDBNetEntities db = new TechBlogDBNetEntities();

        // GET: Admin/BannerUsers
        public ActionResult Index()
        {
            var bannerUsers = db.BannerUsers.Include(b => b.User);
            return View(bannerUsers.ToList());
        }

        // GET: Admin/BannerUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BannerUser bannerUser = db.BannerUsers.Find(id);
            if (bannerUser == null)
            {
                return HttpNotFound();
            }
            return View(bannerUser);
        }

        // GET: Admin/BannerUsers/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username");
            return View();
        }

        // POST: Admin/BannerUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BannerID,UserID,BannerImage,Meta,Hide,Order,DateBegin")] BannerUser bannerUser, HttpPostedFileBase img)
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
                        bannerUser.BannerImage = filename; //Lưu ý
                    }
                    else
                    {
                        bannerUser.BannerImage = "logo.jpg";
                    }
                    db.BannerUsers.Add(bannerUser);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", bannerUser.UserID);
                return View(bannerUser);
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // GET: Admin/BannerUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BannerUser bannerUser = db.BannerUsers.Find(id);
            if (bannerUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", bannerUser.UserID);
            return View(bannerUser);
        }

        // POST: Admin/BannerUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BannerID,UserID,BannerImage,Meta,Hide,Order,DateBegin")] BannerUser bannerUser, HttpPostedFileBase img)
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
                        path = Path.Combine(Server.MapPath("~/Content/upload"), filename);
                        img.SaveAs(path);
                        bannerUser.BannerImage = filename; //Lưu ý
                    }
                    db.Entry(bannerUser).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", bannerUser.UserID);
                return View(bannerUser);
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: Admin/BannerUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BannerUser bannerUser = db.BannerUsers.Find(id);
            if (bannerUser == null)
            {
                return HttpNotFound();
            }
            return View(bannerUser);
        }

        // POST: Admin/BannerUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BannerUser bannerUser = db.BannerUsers.Find(id);
            db.BannerUsers.Remove(bannerUser);
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
