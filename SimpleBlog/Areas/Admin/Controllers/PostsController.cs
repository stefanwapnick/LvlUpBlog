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

            List<int> postIds = DatabaseManager.Session.Query<Post>()
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * POSTS_PER_PAGE)               // if one page 2, skip 1 * 5 = 5 posts in database. Take POSTS_PER_PAGE count starting on this page
                .Take(POSTS_PER_PAGE)
                .Select(p => p.Id)
                .ToList(); 

            List<Post> pagePosts = DatabaseManager.Session.Query<Post>()
                .Where(p => postIds.Contains(p.Id))
                .OrderByDescending(p => p.CreatedAt)
                .FetchMany(p => p.Tags)             // Fetch tags associated with post
                .Fetch(p => p.User)                 // Fetch user associated with post
                .ToList();

            return View(new PostsIndex()
            {
                Posts = new PagedData<Post>(pagePosts, totalPostCount, page, POSTS_PER_PAGE)
            }); 
        }

        public ActionResult New()
        {
            return View("Form", new PostsForm
            {
                IsNew = true,
                Tags = DatabaseManager.Session.Query<Tag>().Select(t => new TagCheckbox
                {
                    Id = t.Id,
                    Name = t.Name, 
                    IsChecked = false
                }).ToList()
            }); 
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
                Title = post.Title,
                Tags = DatabaseManager.Session.Query<Tag>().Select(t => new TagCheckbox     // for each tag in database...
                {
                    Id = t.Id,
                    Name = t.Name,
                    IsChecked = post.Tags.Contains(t)
                }).ToList()
            });
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Form(PostsForm model)
        {
            model.IsNew = (model.PostId == null);

            if (! ModelState.IsValid)
                return View(model);

            var selectedTags = SyncTags(model.Tags).ToList();  

            Post post;

            if (model.IsNew)       // Create new post
            {
                post = new Post() { CreatedAt = DateTime.UtcNow, User = UserCache.CurrentUser };

                foreach (Tag tag in selectedTags)
                    post.Tags.Add(tag); 
                
            }
            else           // Update existing post
            {
                post = DatabaseManager.Session.Load<Post>(model.PostId);
                if (post == null)
                    return HttpNotFound();

                post.UpdatedAt = DateTime.UtcNow;

                // Update tags
                foreach (Tag toAdd in selectedTags.Where(t => !post.Tags.Contains(t)))
                    post.Tags.Add(toAdd);

                foreach (Tag toRemove in post.Tags.Where(t => !selectedTags.Contains(t)).ToList())
                    post.Tags.Remove(toRemove); 

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

        private IEnumerable<Tag> SyncTags(IEnumerable<TagCheckbox> tags)
        {
            foreach (var tag in tags.Where(t => t.IsChecked))
            {
                if (tag.Id != null)
                {
                    yield return DatabaseManager.Session.Load<Tag>(tag.Id);
                    continue; 
                }

                Tag existingTag = DatabaseManager.Session.Query<Tag>().FirstOrDefault(t => t.Name == tag.Name);

                if (existingTag != null)
                {
                    yield return existingTag;
                    continue;
                }

                Tag newTag = new Tag
                {
                    Name = tag.Name,
                    Slug = tag.Name.MakeSlug()
                };

                DatabaseManager.Session.Save(newTag);
                yield return newTag; 
            }
        }

    }
}