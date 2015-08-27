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

        public ActionResult New()
        {
            return View("Form", new PostsForm { IsNew = true }); 
        }

        public ActionResult Edit(int id)
        {
            Post post = DatabaseManager.Session.Load<Post>(id);
            if (post == null)
                return HttpNotFound();

            return View("Form", new PostsForm
            {
                IsNew = false,
                PostId = post.Id,
                Content = post.Content,
                Slug = post.Slug,
                Title = post.Title 
            });
        }

        [HttpPost]
        public ActionResult Form(PostsForm model)
        {
            model.IsNew = (model.PostId == null);

            if (! ModelState.IsValid)
                return View(model);

            Post post;

            if (model.IsNew)       // Create new post
            {
                post = new Post() { CreatedAt = DateTime.UtcNow, User = UserCache.CurrentUser };
            }
            else           // Update existing post
            {
                post = DatabaseManager.Session.Load<Post>(model.PostId);
                if (post == null)
                    return HttpNotFound();

                post.UpdatedAt = DateTime.UtcNow; 
            }

            post.Title = model.Title;
            post.Slug = model.Slug;
            post.Content = model.Content;

            DatabaseManager.Session.SaveOrUpdate(post);

            return RedirectToAction("index"); 
        }

        [HttpPost]
        public ActionResult Trash(int id)
        {
            Post post = DatabaseManager.Session.Load<Post>(id);
            if (post == null)
                return HttpNotFound();

            post.DeletedAt = DateTime.UtcNow;
            DatabaseManager.Session.Update(post);
            return RedirectToAction("index"); 
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Post post = DatabaseManager.Session.Load<Post>(id);
            if (post == null)
                return HttpNotFound();

            DatabaseManager.Session.Delete(post);
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Restore(int id)
        {
            Post post = DatabaseManager.Session.Load<Post>(id);
            if (post == null)
                return HttpNotFound();

            post.DeletedAt = null;
            DatabaseManager.Session.Update(post);
            return RedirectToAction("index");
        }
    }
}