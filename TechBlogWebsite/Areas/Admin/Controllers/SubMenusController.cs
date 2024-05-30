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
    public class SubMenusController : Controller
    {
        private TechBlogDBNetEntities db = new TechBlogDBNetEntities();

        // GET: Admin/SubMenus
        public ActionResult Index()
        {
            var subMenus = db.SubMenus.Include(s => s.menu);
            return View(subMenus.ToList());
        }

        // GET: Admin/SubMenus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMenu subMenu = db.SubMenus.Find(id);
            if (subMenu == null)
            {
                return HttpNotFound();
            }
            return View(subMenu);
        }

        // GET: Admin/SubMenus/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.menus, "ID", "name");
            return View();
        }

        // POST: Admin/SubMenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubMenuID,name,link,meta,hide,order,datebegin,ID")] SubMenu subMenu)
        {
            if (ModelState.IsValid)
            {
                subMenu.datebegin = DateTime.Now; // Set datebegin to the current date and time
                db.SubMenus.Add(subMenu);
                var maxOrder = db.SubMenus.Max(s => (int?)s.order) ?? 0;
                subMenu.order = maxOrder + 1; // Set order to the next highest value
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.menus, "ID", "name", subMenu.ID);
            return View(subMenu);
        }

        // GET: Admin/SubMenus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMenu subMenu = db.SubMenus.Find(id);
            if (subMenu == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.menus, "ID", "name", subMenu.ID);
            return View(subMenu);
        }

        // POST: Admin/SubMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubMenuID,name,link,meta,hide,order,datebegin,ID")] SubMenu subMenu)
        {
            if (ModelState.IsValid)
            {
                subMenu.datebegin = DateTime.Now; // Set datebegin to the current date and time
                db.Entry(subMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.menus, "ID", "name", subMenu.ID);
            return View(subMenu);
        }

        // GET: Admin/SubMenus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMenu subMenu = db.SubMenus.Find(id);
            if (subMenu == null)
            {
                return HttpNotFound();
            }
            return View(subMenu);
        }

        // POST: Admin/SubMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubMenu subMenu = db.SubMenus.Find(id);
            db.SubMenus.Remove(subMenu);
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
