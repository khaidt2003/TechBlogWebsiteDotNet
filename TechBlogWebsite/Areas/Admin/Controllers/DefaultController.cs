using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechBlogWebsite.Areas.Admin.Controllers
{
    [AdminAuthorize]
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
        public ActionResult TablePosts()
        {
            return View();
        }
        public ActionResult TableUser()
        {
            return View();
        }
        public ActionResult TableBannerUser()
        {
            return View();
        }
        public ActionResult TableSubMenus()
        {
            return View();
        }
        public ActionResult TableImagePosts()
        {
            return View();
        }
        public ActionResult TableCategories()
        {
            return View();
        }
        public ActionResult TableJobs()
        {
            return View();
        }
        public ActionResult Err404()
        {
            return View();
        }
    }
}