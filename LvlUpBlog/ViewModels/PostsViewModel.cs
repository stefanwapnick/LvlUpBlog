using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using LvlUpBlog.Infrastructure;
using LvlUpBlog.Models; 

/* 
 *  File contains all view models for controller Auth
 */
namespace LvlUpBlog.ViewModels
{

    public class PostsIndex
    {
        public PagedData<Post> Posts { get; set; }
    }

    public class PostsShow
    {
        public Post Post { get; set; }
    }

    public class PostsTag
    {
        public Tag Tag { get; set; }
        public PagedData<Post> Posts { get; set; }
    }
}