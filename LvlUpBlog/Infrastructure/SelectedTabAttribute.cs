using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LvlUpBlog.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SelectedTabAttribute : ActionFilterAttribute
    {
        private readonly string _selectedTab;
        public SelectedTabAttribute(string selectedTab)
        {
            this._selectedTab = selectedTab; 
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
 	         filterContext.Controller.ViewBag.SelectedTab = _selectedTab; 
        }


    }
}