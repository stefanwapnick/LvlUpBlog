using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleBlog.Models;
using NHibernate.Linq;
using SimpleBlog.Areas.Admin.ViewModels; 

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles="admin")]
    public class PostsController : Controller
    {
        public ActionResult Index(int page = 1)
        {
            const int POSTS_PER_PAGE = 5;

            // Get total number of posts in database
            int totalPostCount = DatabaseManager.Session.Query<Post>().Count();

            List<Post> pagePosts = DatabaseManager.Session.Query<Post>()
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * POSTS_PER_PAGE)               // if one page 2, skip 1 * 5 = 5 posts in database. Take POSTS_PER_PAGE count starting on this page
                .Take(POSTS_PER_PAGE)                            
                .ToList();


            return View(new PostsIndex()
            {
                Posts = new PagedData<Post>(pagePosts, totalPostCount, page, POSTS_PER_PAGE)
            }); 
        }
    }
}