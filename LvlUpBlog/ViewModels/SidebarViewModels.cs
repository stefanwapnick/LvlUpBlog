using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LvlUpBlog.Models; 

namespace LvlUpBlog.ViewModels
{

    public class SidebarTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int PostCount { get; set; }

        public SidebarTag(int id, string name, string slug, int postCount)
        {
            this.Id = id;
            this.Name = name;
            this.Slug = slug;
            this.PostCount = postCount; 
        }
        public SidebarTag() { }
    }

    public class SidebarIndex
    {
        public bool IsLoggedIn { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
        public int SitePosts { get; set; }
        public int SiteComments { get; set; }
        public int UserPosts { get; set; }
        public int UserComments { get; set; }
        public Level SiteLevel { get; set; }
        public Level UserLevel { get; set; }
        public IEnumerable<SidebarTag> Tags { get; set; }

    }
}