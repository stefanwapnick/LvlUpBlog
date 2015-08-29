using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LvlUpBlog.Infrastructure
{
    /// <summary>
    /// Custom global filter for each controller action
    /// Invoked before and after a controller action
    /// Begins and then releases a database transaction. If transaction is in error, does a rollback
    /// </summary>
    public class TransactionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DatabaseManager.Session.BeginTransaction();
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
                DatabaseManager.Session.Transaction.Commit();
            else
                DatabaseManager.Session.Transaction.Rollback(); 
            
        }
    }
}