using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleBlog.ViewModels;
using SimpleBlog.Models;
using NHibernate.Linq; 

namespace SimpleBlog.ViewModels
{
    public class SidebarController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            return View(new SidebarIndex
            {
                IsLoggedIn = UserCache.CurrentUser != null,
                Username = UserCache.CurrentUser != null ? UserCache.CurrentUser.Name : "",
                IsAdmin = User.IsInRole("admin"),

                Tags = DatabaseManager.Session.Query<Tag>().Select(t => new {t.Id, t.Name, t.Slug, PostCount = t.Posts.Count()})
                                        .Where(t => t.PostCount > 0).OrderByDescending(p => p.PostCount)
                                        .Select(t => new SidebarTag(t.Id, t.Name, t.Slug, t.PostCount)).ToList() 
            }); 
        }
    }
}
