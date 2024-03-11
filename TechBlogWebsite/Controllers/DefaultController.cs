using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechBlogWebsite.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult TechSingle()
        {
            return View();
        }
        public ActionResult TechCategory()
        {
            return View();
        }
        public ActionResult TechAuthor()
        {
            return View();
        }
        public ActionResult TechVideo()
        {
            return View();
        }
        public ActionResult TechReivews()
        {
            return View();
        }
        
    }
}