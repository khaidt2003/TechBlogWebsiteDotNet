using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechBlogWebsite.Areas.Admin.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Admin/Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Buttons()
        {
            return View();
        }
        public ActionResult Cards()
        {
            return View();
        }
        public ActionResult Table()
        {
            return View();
        }
    }
}