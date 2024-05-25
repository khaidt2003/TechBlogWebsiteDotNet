using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TechBlogWebsite.Models;

namespace TechBlogWebsite.Controllers
{
    public class AccountController : Controller
    {
        TechBlogDBNetEntities _db = new TechBlogDBNetEntities();

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    _user.Avatar = "/Content/upload/Avatar/avartar_default.png";
                    _user.RoleID = 1;
                    _user.IsActive = true;
                    _user.Meta = "/users/" + _user.FirstName + _user.LastName;
                    _user.Hide = false;

                    // Disable validation before saving
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Users.Add(_user);
                    _db.SaveChanges();

                    // Retrieve the newly created user from the database
                    var newUser = _db.Users.FirstOrDefault(s => s.Email == _user.Email);

                    // Add user information to session
                    Session["FullName"] = newUser.FirstName + " " + newUser.LastName;
                    Session["AvatarUrl"] = newUser.Avatar;
                    Session["idUser"] = newUser.UserID;
                    Session["Email"] = newUser.Email;

                    return RedirectToAction("Index", "Default");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();
        }


        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Default");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = _db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    // add session
                    var user = data.FirstOrDefault();
                    Session["FullName"] = user.FirstName + " " + user.LastName;
                    Session["AvatarUrl"] = user.Avatar;
                    Session["idUser"] = user.UserID;
                    Session["Email"] = user.Email;
                    if (Session["ReturnUrl"] != null)
                    {
                        var returnUrl = Session["ReturnUrl"].ToString();
                        Session["ReturnUrl"] = null; // xóa session
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Default");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return View();
                }
            }
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }
        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

    }
}