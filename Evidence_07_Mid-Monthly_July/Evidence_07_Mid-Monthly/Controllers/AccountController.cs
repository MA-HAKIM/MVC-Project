using Evidence_07_Mid_Monthly.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evidence_07_Mid_Monthly.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(IdentityModel model)
        {
            var store = new UserStore<Microsoft.AspNet.Identity.EntityFramework.IdentityUser>();
            var manager = new UserManager<IdentityUser>(store);
            var user = manager.Find(model.UserName, model.Password);
            if (user != null)
            {
                var authmanager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authmanager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
                Response.Redirect("/Home/Index");
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
            }
            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(IdentityModel model)
        {
            var store = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(store);
            var user = new IdentityUser { UserName = model.UserName };
            IdentityResult result = manager.Create(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}