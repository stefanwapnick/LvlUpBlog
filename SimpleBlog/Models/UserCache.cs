using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    /// <summary>
    /// Stores cache of currently logged in user
    /// </summary>
    public static class UserCache
    {
        private static string USER_KEY = "UserCacheKeyConstant";

        public static User CurrentUser
        {
            get { return HttpContext.Current.Session[USER_KEY] as User; }
            set { HttpContext.Current.Session[USER_KEY] = value; }
        }

        public static void ClearUser()
        {
            CurrentUser = null; 
        }
    }
}