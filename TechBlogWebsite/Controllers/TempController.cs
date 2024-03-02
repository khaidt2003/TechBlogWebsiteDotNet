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
        TechBlogDBNetEntities _db = new TechBlogDBNetEntities();

        // GET: Temp
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getMenu() { 
            var v = from t in _db.menus where t.hide== true orderby t.order ascending select t;
            return PartialView(v.ToList());
        }

    }
}