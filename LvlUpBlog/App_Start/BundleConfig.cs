﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace LvlUpBlog
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Create tag editor bundle
            bundles.Add(new StyleBundle("~/admin/styles")
                .Include("~/content/styles/bootstrap.css")
                .Include("~/content/styles/global.css")
                .Include("~/content/styles/admin.css"));

            // Create site css bundle
            bundles.Add(new StyleBundle("~/styles")
                .Include("~/content/styles/bootstrap.css")
                .Include("~/content/styles/global.css")
                .Include("~/content/styles/frontend.css"));

            /*
            // Create admin script bundle
            bundles.Add(new ScriptBundle("~/admin/scripts")
                .Include("~/scripts/jquery-2.1.4.js")
                .Include("~/scripts/jquery.validate.js")
                .Include("~/scripts/jquery.unobtrusive.js")
                .Include("~/scripts/bootstrap.js")
                .Include("~/areas/admin/scripts/form.js")); */

            // Create admin script bundle
            bundles.Add(new ScriptBundle("~/admin/scripts")
                .Include("~/areas/admin/scripts/form.js")); 

            // Admin script for tag selection
            bundles.Add(new ScriptBundle("~/admin/posts/scripts")
                .Include("~/areas/admin/scripts/posteditor.js"));

            // Create site script bundle
            bundles.Add(new ScriptBundle("~/scripts")
                .Include("~/scripts/jquery-2.1.4.js")
                .Include("~/scripts/jquery.validate.js")
                .Include("~/scripts/jquery.unobtrusive.js")
                .Include("~/scripts/bootstrap.js"));

        }
    }
}