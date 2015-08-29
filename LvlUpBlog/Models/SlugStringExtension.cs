using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions; 

namespace LvlUpBlog.Models
{
    /// <summary>
    /// Contains extension methods for the string class
    /// Used to turn an arbitrary string into a slug
    /// </summary>
    public static class SlugStringExtension
    {

        public static string MakeSlug(this string str){

            str = Regex.Replace(str, @"[^a-zA-Z0-9\s]", "");
            str = str.ToLower();
            str = Regex.Replace(str, @"\s", "-");
            return str; 
        }

    }
}