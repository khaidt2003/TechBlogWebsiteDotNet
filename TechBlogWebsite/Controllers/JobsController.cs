using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechBlogWebsite.Models;

namespace TechBlogWebsite.Controllers
{
    public class JobsController : Controller
    {
        TechBlogDBNetEntities _db = new TechBlogDBNetEntities();
        // GET: Jobs
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(string companyName, string fullName, string currentPosition, string email, string phone, string howHeard, string jobDescription)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var job = new job
                    {
                        company_name = companyName,
                        full_name = fullName,
                        current_position = currentPosition,
                        email = email,
                        phone = phone,
                        how_heard = howHeard,
                        job_description = jobDescription,
                        user_id = 2, // Thay thế bằng ID của người dùng thực
                        created_at = DateTime.Now,
                        is_posted = false
                    };

                    _db.jobs.Add(job);
                    _db.SaveChanges();

                    return RedirectToAction("Success");
                }
                catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                    var fullErrorMessage = string.Join("; ", errorMessages);
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                    Debug.WriteLine(exceptionMessage);
                    ModelState.AddModelError("", exceptionMessage);
                }
            }

            return View("Index");
        }

        public ActionResult Success()
        {
            return View();
        }

    }
}