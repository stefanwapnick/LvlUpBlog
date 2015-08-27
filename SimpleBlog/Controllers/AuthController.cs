using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SimpleBlog.ViewModels;
using NHibernate.Linq;
using SimpleBlog.Models;
using SimpleBlog.Infrastructure;

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
            UserCache.ClearUser(); 
            return RedirectToRoute("Home");
        }

        [HttpPost]
        public ActionResult Login(AuthLogin model, string returnUrl)
        {
            // Fetch user by name
            User selectedUser = DatabaseManager.Session.Query<User>().FirstOrDefault(u => u.Name == model.Name);
            
            // Check if user exists and if password valid
            if (selectedUser == null || !HashUtility.VerifyPassword(model.Password, selectedUser.Password))
                ModelState.AddModelError("Name", "Username or password invalid"); 

            if (!ModelState.IsValid)
                return View(model);

            // Set login session
            FormsAuthentication.SetAuthCookie(selectedUser.Name, true);
            UserCache.CurrentUser = selectedUser;  

            if (!String.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl); 

            return RedirectToRoute("home");
        }

    }
}