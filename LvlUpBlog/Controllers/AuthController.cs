using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LvlUpBlog.ViewModels;
using NHibernate.Linq;
using LvlUpBlog.Models;
using LvlUpBlog.Infrastructure;

namespace LvlUpBlog.Controllers
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
            User selectedUser = DatabaseManager.Session.Query<User>().Fetch(u => u.Roles).Where(u => u.Name == model.Name).FirstOrDefault(); 

            // Check if user exists and if password valid
            if (selectedUser == null || !HashUtility.VerifyPassword(model.Password, selectedUser.Password))
            {
                ModelState.Clear(); 
                ModelState.AddModelError("Name", "Invalid username or password");
                ModelState.AddModelError("Password", "");
                model.Password = ""; 
            }

            if (!ModelState.IsValid)
                return View(model);
            
            // Set login session
            UserCache.CurrentUser = selectedUser;  
            FormsAuthentication.SetAuthCookie(selectedUser.Name, true);
            
            if (!String.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl); 

            return RedirectToRoute("home");
        }

    }
}