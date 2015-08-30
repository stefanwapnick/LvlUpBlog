using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LvlUpBlog.ViewModels;
using LvlUpBlog.Models;
using NHibernate.Linq; 

namespace LvlUpBlog.ViewModels
{
    public class SidebarController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            int sitePosts = DatabaseManager.Session.Query<Post>().Count();
            int userPosts = UserCache.CurrentUser != null ? DatabaseManager.Session.Query<Post>().Count(x => x.User.Id == UserCache.CurrentUser.Id) : 0;
            

            return View(new SidebarIndex
            {
                IsLoggedIn = UserCache.CurrentUser != null,
                Username = UserCache.CurrentUser != null ? UserCache.CurrentUser.Name : "",
                IsAdmin = UserCache.CurrentUser != null ? UserCache.CurrentUser.Roles.Select(r => r.RoleName).Contains("admin") : false, //User.IsInRole("admin"),

                Tags = DatabaseManager.Session.Query<Tag>().Select(t => new {t.Id, t.Name, t.Slug, PostCount = t.Posts.Count()})
                                        .Where(t => t.PostCount > 0).OrderByDescending(p => p.PostCount)
                                        .Select(t => new SidebarTag(t.Id, t.Name, t.Slug, t.PostCount)).ToList(),

                SiteLevel = LevelFetcher.getLevel(DatabaseManager.Session.Query<Post>().Count()),
                UserLevel = UserCache.CurrentUser != null ? LevelFetcher.getLevel(userPosts, forSite: false) : null,
                
                SitePosts = sitePosts,
                UserPosts = userPosts, 
                SiteComments = 0, 
                UserComments = 0
            }); 
        }
    }
}
