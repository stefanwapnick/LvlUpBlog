using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SimpleBlog.ViewModels;  

namespace SimpleBlog.Controllers
{

    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return View(new AuthLogin()); 
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Home");
        }

        [HttpPost]
        public ActionResult Login(AuthLogin model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);


            FormsAuthentication.SetAuthCookie(model.UserName, true);

            if (!String.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl); 

            return RedirectToRoute("home");
        }

    }
}