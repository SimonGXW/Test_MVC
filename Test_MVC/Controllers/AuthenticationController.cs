using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Test_MVC.Models;
using Test_MVC.ViewModels;

namespace Test_MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Index()
        {
            return View();

        }
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }


        //[HttpPost]
        //public ActionResult DoLogin(UserDetails u)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
        //        if (bal.IsValidUser(u))
        //        {
        //            FormsAuthentication.SetAuthCookie(u.UserName, false);
        //            return RedirectToAction("Index", "Employee");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("CredentialError", "Invalid Username or Password");
        //            return View("Login");
        //        }
        //    }
        //    else
        //    {
        //        return View("Login");
        //    }
        //}
        [HttpPost]
        public ActionResult DoLogin(UserDetails u)
        {
            if (ModelState.IsValid)
            {
                EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
                //New Code Start
                UserStatus status = bal.GetUserValidity(u);
                bool IsAdmin = false;
                if (status == UserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = true;
                }
                else if (status == UserStatus.AuthentucatedUser)
                {
                    IsAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                    return View("Login");
                }
                FormsAuthentication.SetAuthCookie(u.UserName, false);
                Session["IsAdmin"] = IsAdmin;
                return RedirectToAction("Index", "Employee");
                //New Code End
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}