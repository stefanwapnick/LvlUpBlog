using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;
using SimpleBlog.Models;
using SimpleBlog.ViewModels;
using System.Text.RegularExpressions; 

namespace SimpleBlog.Controllers
{
    public class PostsController : Controller
    {

        private const int POSTS_PER_PAGE = 10; 

        public ActionResult Index(int page = 1)
        {
            var baseQuery =  DatabaseManager.Session.Query<Post>()
                                    .Where(p => p.DeletedAt == null)
                                    .OrderByDescending(p => p.CreatedAt);

            int totalPosts = baseQuery.Count(); 

            List<int> postIds = baseQuery.Skip((page - 1) * POSTS_PER_PAGE)
                                    .Take(POSTS_PER_PAGE)
                                    .Select(p => p.Id)
                                    .ToList();

            List<Post> posts = baseQuery.Where(p => postIds.Contains(p.Id))
                                    .FetchMany(p => p.Tags)
                                    .Fetch(p => p.User)
                                    .ToList();

            return View(new PostsIndex
            {
                Posts = new PagedData<Post>(posts, totalPosts, page, POSTS_PER_PAGE)
            });
        }

        public ActionResult Tag(string idAndSlug, int page = 1)
        {
            Tuple<int, string> parts = SeperateIdAndSlug(idAndSlug);
            if (parts == null)
                return HttpNotFound();

            Tag tag = DatabaseManager.Session.Load<Tag>(parts.Item1);
            if (tag == null)
                return HttpNotFound(); 

            if(!tag.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToRoutePermanent("tag", new {id = parts.Item1, slug = parts.Item2}); 

            int totalPosts = tag.Posts.Count(); 
            List<int> postIds = tag.Posts.Skip((page-1) * POSTS_PER_PAGE)
                                .Take(POSTS_PER_PAGE)
                                .Where(p => p.DeletedAt == null)
                                .Select(p => p.Id)
                                .ToList();

            List<Post> posts = DatabaseManager.Session.Query<Post>()
                                .OrderByDescending(p => p.CreatedAt)
                                .Where(p => postIds.Contains(p.Id))
                                .FetchMany(p => p.Tags)
                                .Fetch(p => p.User)
                                .ToList();

            return View(new PostsTag
            {
                Tag = tag,
                Posts = new PagedData<Post>(posts, totalPosts, page, POSTS_PER_PAGE) 
            }); 

        }

        public ActionResult Show(string idAndSlug)
        {
            Tuple<int, string> parts = SeperateIdAndSlug(idAndSlug);
            if (parts == null)
                return HttpNotFound();

            Post post = DatabaseManager.Session.Load<Post>(parts.Item1);

            if (post == null || post.IsDeleted)
                return HttpNotFound();

            // If invalid slug, redirect to correct slug
            if (!post.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToRoutePermanent("Post", new { id = parts.Item1, slug = post.Slug });

            return View(new PostsShow
            {
                Post = post
            });

        }

        private Tuple<int, string> SeperateIdAndSlug(string idAndSlug)
        {
            var matches = Regex.Match(idAndSlug, @"^(\d+)\-(.*)?$");
            if (!matches.Success)
                return null;

            int id = int.Parse(matches.Result("$1"));
            string slug = matches.Result("$2");
            return Tuple.Create(id, slug); 

        }

    }
}